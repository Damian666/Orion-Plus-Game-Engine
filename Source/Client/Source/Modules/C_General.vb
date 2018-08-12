Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms

Module C_General

    Friend Started As Boolean

    Friend Function GetTickCount() As Integer
        Return Environment.TickCount
    End Function

    Sub Startup()
        SFML.Portable.Activate()

        SetStatus(Strings.Get("loadscreen", "loading"))

        ' Generate Random Seed
        Randomize()

        FrmMenu.Visible = True

        ReDim CharSelection(3)

        ReDim Player(MAX_PLAYERS)

        For i = 1 To MAX_PLAYERS
            ClearPlayer(i)
        Next

        ClearAutotiles()

        'Housing
        ReDim House(MaxHouses)
        ReDim HouseConfig(MaxHouses)

        'quests
        ClearQuests()

        'npc's
        ClearNpcs()
        ReDim Map.Npc(MAX_MAP_NPCS)
        ReDim MapNpc(MAX_MAP_NPCS)
        For i = 0 To MAX_MAP_NPCS
            For x = 0 To VitalType.Count - 1
                ReDim MapNpc(i).Vital(x)
            Next
        Next

        ClearShops()

        ClearAnimations()

        ClearAnimInstances()

        ClearBank()

        ReDim MapProjectiles(MaxProjectiles)
        ReDim Projectiles(MaxProjectiles)

        ClearItems()

        'craft
        ClearRecipes()

        'party
        ClearParty()

        'pets
        ClearPets()

        GettingMap = True

        ' Update the form with the game's name before it's loaded
        FrmGame.Text = GameName

        SetStatus(Strings.Get("loadscreen", "options"))

        ' load options
        Configuration.LoadSettings()
        
        SetStatus(Strings.Get("loadscreen", "network"))

        FrmMenu.Text = GameName
        
        SetStatus(Strings.Get("loadscreen", "graphics"))
        CheckTilesets()
        CheckCharacters()
        CheckPaperdolls()
        CheckAnimations()
        CheckItems()
        CheckResources()
        CheckSkillIcons()
        CheckFaces()
        CheckFog()
        CacheMusic()
        CacheSound()
        CheckEmotes()
        CheckPanoramas()
        CheckFurniture()
        CheckProjectiles()
        CheckParallax()

        InitGraphics()

        ' check if we have main-menu music
        If Configuration.Settings.Music = 1 AndAlso Configuration.Settings.MenuMusic.Trim.Length > 0 Then
            PlayMusic(Configuration.Settings.MenuMusic.Trim)
            MusicPlayer.Volume() = 100
        End If

        ' Reset values
        Ping = -1

        ' set values for directional blocking arrows
        DirArrowX(1) = 12 ' up
        DirArrowY(1) = 0
        DirArrowX(2) = 12 ' down
        DirArrowY(2) = 23
        DirArrowX(3) = 0 ' left
        DirArrowY(3) = 12
        DirArrowX(4) = 23 ' right
        DirArrowY(4) = 12

        'set gui switches
        HudVisible = True

        SetStatus(Strings.Get("loadscreen", "starting"))
        Started = True
        Frmmenuvisible = True
        Pnlloadvisible = False

        PnlInventoryVisible = True

        Network.Initialize()

        GameLoop()
    End Sub

    Friend Function IsLoginLegal(username As String, password As String) As Boolean
        Return username.Trim.Length >= 3 AndAlso password.Trim.Length >= 3
    End Function

    Friend Function IsStringLegal(value As String) As Boolean
        For Each c In value
            If Not Char.IsLetterOrDigit(c) Then
                MessageBox.Show(Strings.Get("mainmenu", "stringlegal"), GameName, MessageBoxButtons.Ok )
                Return False
            End If
        Next

        Return True
    End Function

    Sub GameInit()
        Pnlloadvisible = False

        ' Set the focus
        FrmGame.picscreen.Focus()

        'stop the song playing
        StopMusic()
    End Sub

    Friend Sub SetStatus(caption As String)
        FrmMenu.lblStatus.Text = caption
    End Sub

    Friend Sub MenuState(state As Integer)
        Pnlloadvisible = True
        Frmmenuvisible = False
        Select Case state
            Case MenuStateAddchar
                PnlCharCreateVisible = False
                PnlLoginVisible = False
                PnlRegisterVisible = False
                PnlCreditsVisible = False

                If ConnectToServer(1) Then
                    SetStatus(Strings.Get("mainmenu", "sendaddchar"))

                    If FrmMenu.rdoMale.Checked = True Then
                        Network.SendAddChar(SelectedChar, FrmMenu.txtCharName.Text, SexType.Male, FrmMenu.cmbClass.SelectedIndex + 1, NewCharSprite)
                    Else
                        Network.SendAddChar(SelectedChar, FrmMenu.txtCharName.Text, SexType.Female, FrmMenu.cmbClass.SelectedIndex + 1, NewCharSprite)
                    End If
                End If

            Case MenuStateNewaccount
                PnlLoginVisible = False
                PnlCharCreateVisible = False
                PnlRegisterVisible = False
                PnlCreditsVisible = False

                If ConnectToServer(1) Then
                    SetStatus(Strings.Get("mainmenu", "sendnewacc"))
                    Network.SendNewAccount(FrmMenu.txtRuser.Text, FrmMenu.txtRPass.Text)
                End If

            Case MenuStateLogin
                PnlLoginVisible = False
                PnlCharCreateVisible = False
                PnlRegisterVisible = False
                PnlCreditsVisible = False
                TempUserName = FrmMenu.txtLogin.Text
                TempPassword = FrmMenu.txtPassword.Text

                If ConnectToServer(1) Then
                    SetStatus(Strings.Get("mainmenu", "sendlogin"))
                    Network.SendLogin(FrmMenu.txtLogin.Text, FrmMenu.txtPassword.Text)
                    Exit Sub
                End If
        End Select

    End Sub

    Friend Function ConnectToServer(i As Integer) As Boolean
        Dim until As Integer
        ConnectToServer = False

        ' Check to see if we are already connected, if so just exit
        If Network.IsConnected() Then
            ConnectToServer = True
            Exit Function
        End If

        If i = 4 Then Exit Function
        until = GetTickCount() + 3500

        Network.Connect(Configuration.Settings.Ip, Configuration.Settings.Port)

        SetStatus(Strings.Get("mainmenu", "connectserver", i))

        ' Wait until connected or a few seconds have passed and report the server being down
        Do While (Not Network.IsConnected()) AndAlso (GetTickCount() <= until)
            Application.DoEvents()
            Thread.Sleep(10)
        Loop

        ' return value
        If Network.IsConnected() Then
            ConnectToServer = True
        End If

        If Not ConnectToServer Then
            ConnectToServer(i + 1)
        End If

    End Function

    Friend Sub RePositionGui()

        'first change the tiles
        If Configuration.Settings.ScreenSize = 0 Then ' 800x600
            ScreenMapx = 25
            ScreenMapy = 19
        ElseIf Configuration.Settings.ScreenSize = 1 Then '1024x768
            ScreenMapx = 31
            ScreenMapy = 24
        ElseIf Configuration.Settings.ScreenSize = 2 Then
            ScreenMapx = 35
            ScreenMapy = 26
        End If

        'then the window
        FrmGame.ClientSize = New Drawing.Size((ScreenMapx) * PicX + PicX, (ScreenMapy) * PicY + PicY)
        FrmGame.picscreen.Width = (ScreenMapx) * PicX + PicX
        FrmGame.picscreen.Height = (ScreenMapy) * PicY + PicY

        HalfX = ((ScreenMapx) \ 2) * PicX
        HalfY = ((ScreenMapy) \ 2) * PicY
        ScreenX = (ScreenMapx) * PicX
        ScreenY = (ScreenMapy) * PicY

        GameWindow.SetView(New SFML.Graphics.View(New SFML.Graphics.FloatRect(0, 0, (ScreenMapx) * PicX + PicX, (ScreenMapy) * PicY + PicY)))

        'Then we can recalculate the positions

        'chatwindow
        ChatWindowX = 1
        ChatWindowY = FrmGame.Height - ChatWindowGfxInfo.Height - 65

        MyChatX = 1
        MyChatY = FrmGame.Height - 60

        'hotbar
        If Configuration.Settings.ScreenSize = 0 Then
            HotbarX = HudWindowX + HudPanelGfxInfo.Width + 20
            HotbarY = 5

            'petbar
            PetbarX = HotbarX
            PetbarY = HotbarY + 34
        Else
            HotbarX = ChatWindowX + MyChatWindowGfxInfo.Width + 50
            HotbarY = FrmGame.Height - HotBarGfxInfo.Height - 45

            'petbar
            PetbarX = HotbarX
            PetbarY = HotbarY - 34
        End If

        'action panel
        ActionPanelX = FrmGame.Width - ActionPanelGfxInfo.Width - 25
        ActionPanelY = FrmGame.Height - ActionPanelGfxInfo.Height - 45

        'Char Window
        CharWindowX = FrmGame.Width - CharPanelGfxInfo.Width - 26
        CharWindowY = FrmGame.Height - CharPanelGfxInfo.Height - ActionPanelGfxInfo.Height - 50

        'inv Window
        InvWindowX = FrmGame.Width - InvPanelGfxInfo.Width - 26
        InvWindowY = FrmGame.Height - InvPanelGfxInfo.Height - ActionPanelGfxInfo.Height - 50

        'skill window
        SkillWindowX = FrmGame.Width - SkillPanelGfxInfo.Width - 26
        SkillWindowY = FrmGame.Height - SkillPanelGfxInfo.Height - ActionPanelGfxInfo.Height - 50

        'petstat window
        PetStatX = PetbarX
        PetStatY = PetbarY - PetStatsGfxInfo.Height - 10
    End Sub

    Friend Sub Terminate()
        'SendLeaveGame()
        ' break out of GameLoop
        InGame = False

        DestroyGraphics()
        GameDestroyed = True
        Network.Destroy()
        Application.Exit()
        End
    End Sub

    Friend Sub CheckDir(dirPath As String)

        If Not IO.Directory.Exists(dirPath) Then
            IO.Directory.CreateDirectory(dirPath)
        End If

    End Sub

    Friend Function GetExceptionInfo(ex As Exception) As String
        Dim result As String
        Dim hr As Integer = Runtime.InteropServices.Marshal.GetHRForException(ex)
        result = ex.GetType.ToString & "(0x" & hr.ToString("X8") & "): " & ex.Message & Environment.NewLine & ex.StackTrace & Environment.NewLine
        Dim st As StackTrace = New StackTrace(ex, True)
        For Each sf As StackFrame In st.GetFrames
            If sf.GetFileLineNumber() > 0 Then
                result &= "Line:" & sf.GetFileLineNumber() & " Filename: " & IO.Path.GetFileName(sf.GetFileName) & Environment.NewLine
            End If
        Next
        Return result
    End Function

    Private randomGenerator As Random
    Friend Sub Randomize()
        randomGenerator = New Random
    End Sub

    Friend Function Rand(maxVal As Integer, Optional ByVal minVal As Integer = 0) As Integer
        If minVal < maxVal Then Return randomGenerator.Next(minVal, maxVal)
        Return randomGenerator.Next(maxVal, minVal)
    End Function
End Module