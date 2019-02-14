Imports System.IO

Module S_General
    'Friend Declare Function GetQueueStatus Lib "user32" (fuFlags As Integer) As Integer
    Friend ServerDestroyed As Boolean
    Friend MyIPAddress As String
    Friend myStopWatch As New Stopwatch()

    Friend Function GetTimeMs() As Integer
        Return myStopWatch.ElapsedMilliseconds
    End Function

    Sub InitServer()
        Dim i As Integer, F As Integer, x As Integer
        Dim time1 As Integer, time2 As Integer

        myStopWatch.Start()

        If Debugger.IsAttached Then
            ' Since there is a debugger attached,
            ' assume we are running from the IDE
            Debugging = True
        Else
            ' Assume we aren't running from the IDE
            Dim currentDomain As AppDomain = AppDomain.CurrentDomain
            AddHandler currentDomain.UnhandledException, AddressOf ErrorHandler
        End If

        LoadSettings()

        Console.Title = "Orion+ Server"
        Console.SetWindowSize(120, 20)

        handler = New ConsoleEventDelegate(AddressOf ConsoleEventCallback)
        SetConsoleCtrlHandler(handler, True)

        time1 = GetTimeMs()

        ' Initialize the random-number generator
        Randomize()

        ' LOAD ENCRYPTION
        Dim fi = Application.StartupPath & "\AsyncKeys.xml"
        If Not File.Exists(fi) Then
            EKeyPair.GenerateKeys()
            EKeyPair.ExportKey(fi, True) ' True exports private key too.
            ' Remember never pass private to client!
            ' Exporting the Key above saves it as a file for later reuse.
        Else
            EKeyPair.ImportKey(fi)
        End If
        ' END LOAD ENCRYPTION

        ReDim Map(MAX_CACHED_MAPS)

        ReDim MapNpc(MAX_CACHED_MAPS)
        For i = 0 To MAX_CACHED_MAPS
            ReDim MapNpc(i).Npc(MAX_MAP_NPCS)
            ReDim Map(i).Npc(MAX_MAP_NPCS)
        Next

        'quests
        ReDim Quest(MAX_QUESTS)
        ClearQuests()

        'event
        ReDim Switches(MAX_SWITCHES)
        ReDim Variables(MAX_VARIABLES)
        ReDim TempEventMap(MAX_CACHED_MAPS)

        'Housing
        ReDim HouseConfig(MAX_HOUSES)

        For i = 0 To MAX_CACHED_MAPS
            For x = 0 To MAX_MAP_NPCS
                ReDim MapNpc(i).Npc(x).Vital(VitalType.Count)
            Next
        Next

        ReDim Bank(MAX_PLAYERS)
        ReDim Classes(MAX_CLASSES)

        For i = 1 To MAX_PLAYERS
            ReDim Bank(i).Item(MAX_BANK)
            ReDim Bank(i).ItemRand(MAX_BANK)
            For x = 1 To MAX_BANK
                ReDim Bank(i).ItemRand(x).Stat(StatType.Count - 1)
            Next
        Next

        ReDim Player(MAX_PLAYERS)

        For i = 1 To MAX_PLAYERS
            'multi char
            ReDim Player(i).Character(MAX_CHARS)
            For x = 1 To MAX_CHARS
                ReDim Player(i).Character(x).Switches(MAX_SWITCHES)
                ReDim Player(i).Character(x).Variables(MAX_VARIABLES)
                ReDim Player(i).Character(x).Vital(VitalType.Count - 1)
                ReDim Player(i).Character(x).Stat(StatType.Count - 1)
                ReDim Player(i).Character(x).Equipment(EquipmentType.Count - 1)
                ReDim Player(i).Character(x).Inv(MAX_INV)
                ReDim Player(i).Character(x).Skill(MAX_PLAYER_SKILLS)
                ReDim Player(i).Character(x).PlayerQuest(MAX_QUESTS)

                ReDim Player(i).Character(x).RandEquip(EquipmentType.Count - 1)
                ReDim Player(i).Character(x).RandInv(MAX_INV)
                For y = 1 To EquipmentType.Count - 1
                    ReDim Player(i).Character(x).RandEquip(y).Stat(StatType.Count - 1)
                Next
                For y = 1 To MAX_INV
                    ReDim Player(i).Character(x).RandInv(y).Stat(StatType.Count - 1)
                Next
            Next
        Next

        ReDim TempPlayer(MAX_PLAYERS)

        For i = 1 To MAX_PLAYERS
            ReDim TempPlayer(i).SkillCd(MAX_PLAYER_SKILLS)
            ReDim TempPlayer(i).PetSkillCd(4)
        Next

        For i = 1 To MAX_PLAYERS
            ReDim TempPlayer(i).TradeOffer(MAX_INV)
        Next

        LoadTilePrefab()

        ReDim Classes(Max_Classes)
        For i = 0 To Max_Classes
            ReDim Classes(i).Stat(StatType.Count - 1)
            ReDim Classes(i).StartItem(5)
            ReDim Classes(i).StartValue(5)
        Next

        For i = 0 To MAX_ITEMS
            ReDim Item(i).Add_Stat(StatType.Count - 1)
            ReDim Item(i).Stat_Req(StatType.Count - 1)
            ReDim Item(i).FurnitureBlocks(3, 3)
            ReDim Item(i).FurnitureFringe(3, 3)
        Next
        ReDim Npc(MAX_NPCS).Stat(StatType.Count - 1)

        ReDim Shop(MAX_SHOPS).TradeItem(MAX_TRADES)

        ReDim Animation(MAX_ANIMATIONS).Sprite(1)
        ReDim Animation(MAX_ANIMATIONS).Frames(1)
        ReDim Animation(MAX_ANIMATIONS).LoopCount(1)
        ReDim Animation(MAX_ANIMATIONS).LoopTime(1)

        ReDim MapProjectiles(MAX_CACHED_MAPS, MAX_PROJECTILES)
        ReDim Projectiles(MAX_PROJECTILES)

        'parties
        ClearParties()

        'pets
        ReDim Pet(MAX_PETS)
        ClearPets()

        ' Check if the directory is there, if its not make it
        CheckDir(Path.Database)
        CheckDir(Path.Items)
        CheckDir(Path.Maps)
        CheckDir(Path.Npcs)
        CheckDir(Path.Shops)
        CheckDir(Path.Skills)
        CheckDir(Path.accounts)
        CheckDir(Path.resources)
        CheckDir(Path.Animations)
        CheckDir(Path.logs)
        CheckDir(Path.Quests)
        CheckDir(Path.Recipes)
        CheckDir(Path.Pets)
        CheckDir(Path.Projectiles)
        CheckDir(Path.Quests)

        ' Get that network READY SUN! ~ SpiceyWOlf
        InitNetwork()

        ' Init all the player sockets
        Console.WriteLine("Initializing player array...")

        For x = 1 To MAX_PLAYERS
            ClearPlayer(x)
        Next

        ' Serves as a constructor
        ClearGameData()
        LoadGameData()
        Console.WriteLine("Spawning map items...")
        SpawnAllMapsItems()
        Console.WriteLine("Spawning map npcs...")
        SpawnAllMapNpcs()

        ' Check if the master charlist file exists for checking duplicate names, and if it doesnt make it
        If Not File.Exists("data\accounts\charlist.txt") Then
            F = FreeFile()
        End If

        'resource system
        LoadSkillExp()

        InitTime()

        UpdateCaption()
        time2 = GetTimeMs()

        Console.Clear()
        Console.WriteLine("  ____       _                        _____                          ")
        Console.WriteLine(" / __ \     (_)                _     / ____|                         ")
        Console.WriteLine("| |  | |_ __ _  ___  _ __    _| |_  | (___   ___ _ ____   _____ _ __ ")
        Console.WriteLine("| |  | | '__| |/ _ \| '_ \  |_   _|  \___ \ / _ \ '__\ \ / / _ \ '__|")
        Console.WriteLine("| |__| | |  | | (_) | | | |   |_|    ____) |  __/ |   \ V /  __/ |   ")
        Console.WriteLine(" \____/|_|  |_|\___/|_| |_|         |_____/ \___|_|    \_/ \___|_|   ")

        Console.WriteLine("")

        Console.WriteLine("Initialization complete. Server loaded in " & time2 - time1 & "ms.")
        Console.WriteLine("")
        Console.WriteLine("Use /help for the available commands.")

        MyIPAddress = GetIP()

        UpdateCaption()

        ' reset shutdown value
        isShuttingDown = False

        ' Start listener now that everything is loaded
        Socket.StartListening(Settings.Port, 5)

        ' Starts the server loop
        ServerLoop()

    End Sub

    Private Function ConsoleEventCallback(eventType As Integer) As Boolean
        If eventType = 2 Then
            Console.WriteLine("Console window closing, death imminent")
            'cleanup and close
            DestroyServer()
        End If
        Return False
    End Function

    Private handler As ConsoleEventDelegate

    ' Keeps it from getting garbage collected
    ' Pinvoke
    Private Delegate Function ConsoleEventDelegate(eventType As Integer) As Boolean

    <Runtime.InteropServices.DllImport("kernel32.dll", SetLastError:=True)>
    Private Function SetConsoleCtrlHandler(callback As ConsoleEventDelegate, add As Boolean) As Boolean
    End Function

    Sub UpdateCaption()
        Console.Title = String.Format("{0} <IP {1}:{2}> ({3} Players Online) - Current Errors: {4} - Time: {5}", Settings.GameName, MyIPAddress, Settings.Port, GetPlayersOnline(), ErrorCount, Time.Instance.ToString())
    End Sub

    Sub DestroyServer()
        Socket.StopListening()

        Console.WriteLine("Saving players online...")
        SaveAllPlayersOnline()

        Console.WriteLine("Unloading players...")
        For i As Integer = 1 To MAX_PLAYERS
            SendLeftGame(i)
            LeftGame(i)
        Next

        DestroyNetwork()
        ClearGameData()

#If DEBUG Then
        Application.Exit()
        Application.ExitThread()
        Process.GetCurrentProcess.Kill()
#Else
        Environment.Exit(0)
#End If
    End Sub

    Friend Sub ClearGameData()
        Console.WriteLine("Clearing temp tile fields...") : ClearTempTiles()
        Console.WriteLine("Clearing Maps...") : ClearMaps()
        Console.WriteLine("Clearing Map Items...") : ClearMapItems()
        Console.WriteLine("Clearing Map Npc's...") : ClearAllMapNpcs()
        Console.WriteLine("Clearing Npc's...") : ClearNpcs()
        Console.WriteLine("Clearing Resources...") : ClearResources()
        Console.WriteLine("Clearing Items...") : ClearItems()
        Console.WriteLine("Clearing Shops...") : ClearShops()
        Console.WriteLine("Clearing Skills...") : ClearSkills()
        Console.WriteLine("Clearing Animations...") : ClearAnimations()
        Console.WriteLine("Clearing Quests...") : ClearQuests()
        Console.WriteLine("Clearing map projectiles...") : ClearMapProjectiles()
        Console.WriteLine("Clearing projectiles...") : ClearProjectiles()
        Console.WriteLine("Clearing Recipes...") : ClearRecipes()
        Console.WriteLine("Clearing pets...") : ClearPets()
    End Sub

    Private Sub LoadGameData()
        Console.WriteLine("Loading Classes...") : LoadClasses()
        Console.WriteLine("Loading Maps...") : LoadMaps()
        Console.WriteLine("Loading Items...") : LoadItems()
        Console.WriteLine("Loading Npc's...") : LoadNpcs()
        Console.WriteLine("Loading Resources...") : LoadResources()
        Console.WriteLine("Loading Shops...") : LoadShops()
        Console.WriteLine("Loading Skills...") : LoadSkills()
        Console.WriteLine("Loading Animations...") : LoadAnimations()
        Console.WriteLine("Loading Quests...") : LoadQuests()
        Console.WriteLine("Loading House Configurations...") : LoadHouses()
        Console.WriteLine("Loading Switches...") : LoadSwitches()
        Console.WriteLine("Loading Variables...") : LoadVariables()
        Console.WriteLine("Spawning global events...") : SpawnAllMapGlobalEvents()
        Console.WriteLine("Loading projectiles...") : LoadProjectiles()
        Console.WriteLine("Loading Recipes...") : LoadRecipes()
        Console.WriteLine("Loading Pets...") : LoadPets()
    End Sub

    ' Used for checking validity of names
    Function IsNameLegal(sInput As Integer) As Boolean

        If (sInput >= 65 AndAlso sInput <= 90) OrElse (sInput >= 97 AndAlso sInput <= 122) OrElse (sInput = 95) OrElse (sInput = 32) OrElse (sInput >= 48 AndAlso sInput <= 57) Then
            Return True
        Else
            Return False
        End If

    End Function

    Friend Sub CheckDir(path As String)
        If Not Directory.Exists(path) Then Directory.CreateDirectory(path)
    End Sub

    Sub ErrorHandler(sender As Object, args As UnhandledExceptionEventArgs)
        Dim e As Exception = DirectCast(args.ExceptionObject, Exception)
        Dim myFilePath As String = Path.Logs & "ErrorLog.log"

        Using sw As New StreamWriter(File.Open(myFilePath, FileMode.Append))
            sw.WriteLine(DateTime.Now)
            sw.WriteLine(GetExceptionInfo(e))
        End Using

        ErrorCount = ErrorCount + 1

        UpdateCaption()
    End Sub

    Friend Function GetExceptionInfo(ex As Exception) As String
        Dim Result As String
        Dim hr As Integer = Runtime.InteropServices.Marshal.GetHRForException(ex)
        Result = ex.GetType.ToString & "(0x" & hr.ToString("X8") & "): " & ex.Message & Environment.NewLine & ex.StackTrace & Environment.NewLine
        Dim st As New StackTrace(ex, True)
        For Each sf As StackFrame In st.GetFrames
            If sf.GetFileLineNumber() > 0 Then
                Result &= "Line:" & sf.GetFileLineNumber() & " Filename: " & System.IO.Path.GetFileName(sf.GetFileName) & Environment.NewLine
            End If
        Next
        Return Result
    End Function

    Friend Sub AddDebug(Msg As String)
        If DebugTxt = True Then
            Addlog(Msg, PACKET_LOG)
            Console.WriteLine(Msg)
        End If

    End Sub

End Module