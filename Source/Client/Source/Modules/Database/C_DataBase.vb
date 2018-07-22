Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Linq

Module C_DataBase
    Friend Function GetFileContents(fullPath As String, Optional ByRef errInfo As String = "") As String
        Dim strContents As String
        Dim objReader As StreamReader
        strContents = ""
        Try
            objReader = New StreamReader(fullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
        Catch ex As Exception
            errInfo = ex.Message
        End Try
        Return strContents
    End Function

#Region "Assets Check"
    Friend Sub CheckTilesets()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "\tilesets\" & i & GfxExt)
            NumTileSets = NumTileSets + 1
            i = i + 1
        End While

        If NumTileSets = 0 Then Exit Sub
    End Sub

    Friend Sub CheckCharacters()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "characters\" & i & GfxExt)
            NumCharacters = NumCharacters + 1
            i = i + 1
        End While

        If NumCharacters = 0 Then Exit Sub
    End Sub

    Friend Sub CheckPaperdolls()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "paperdolls\" & i & GfxExt)
            NumPaperdolls = NumPaperdolls + 1
            i = i + 1
        End While

        If NumPaperdolls = 0 Then Exit Sub
    End Sub

    Friend Sub CheckAnimations()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "animations\" & i & GfxExt)
            NumAnimations = NumAnimations + 1
            i = i + 1
        End While

        If NumAnimations = 0 Then Exit Sub
    End Sub

    Friend Sub CheckItems()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "Items\" & i & GfxExt)
            NumItems = NumItems + 1
            i = i + 1
        End While

        If NumItems = 0 Then Exit Sub
    End Sub

    Friend Sub CheckResources()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "Resources\" & i & GfxExt)
            NumResources = NumResources + 1
            i = i + 1
        End While

        If NumResources = 0 Then Exit Sub
    End Sub

    Friend Sub CheckSkillIcons()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "SkillIcons\" & i & GfxExt)
            NumSkillIcons = NumSkillIcons + 1
            i = i + 1
        End While

        If NumSkillIcons = 0 Then Exit Sub
    End Sub

    Friend Sub CheckFaces()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "Faces\" & i & GfxExt)
            NumFaces = NumFaces + 1
            i = i + 1
        End While

        If NumFaces = 0 Then Exit Sub
    End Sub

    Friend Sub CheckFog()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "Fogs\" & i & GfxExt)
            NumFogs = NumFogs + 1
            i = i + 1
        End While

        If NumFogs = 0 Then Exit Sub
    End Sub

    Friend Sub CheckEmotes()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "Emotes\" & i & GfxExt)
            NumEmotes = NumEmotes + 1
            i = i + 1
        End While

        If NumEmotes = 0 Then Exit Sub
    End Sub

    Friend Sub CheckPanoramas()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "Panoramas\" & i & GfxExt)
            NumPanorama = NumPanorama + 1
            i = i + 1
        End While

        If NumPanorama = 0 Then Exit Sub
    End Sub

    Friend Sub CheckParallax()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "Parallax\" & i & GfxExt)
            NumParallax = NumParallax + 1
            i = i + 1
        End While

        If NumParallax = 0 Then Exit Sub
    End Sub

    Friend Sub CacheMusic()
        Dim files As String() = Directory.GetFiles(Application.StartupPath & MusicPath, "*.ogg")
        Dim maxNum As String = Directory.GetFiles(Application.StartupPath & MusicPath, "*.ogg").Count
        Dim counter As Integer = 1

        For Each FileName In files
            ReDim Preserve MusicCache(counter)

            MusicCache(counter) = Path.GetFileName(FileName)
            counter = counter + 1
            Application.DoEvents()
        Next

    End Sub

    Friend Sub CacheSound()
        Dim files As String() = Directory.GetFiles(Application.StartupPath & SoundPath, "*.ogg")
        Dim maxNum As String = Directory.GetFiles(Application.StartupPath & SoundPath, "*.ogg").Count
        Dim counter As Integer = 1

        For Each FileName In files
            ReDim Preserve SoundCache(counter)

            SoundCache(counter) = Path.GetFileName(FileName)
            counter = counter + 1
            Application.DoEvents()
        Next

    End Sub
#End Region

#Region "Options"
    Friend Sub CreateOptions()
        Dim myXml As New XmlClass With {
            .Filename = Application.StartupPath & "\Data\Config.xml",
            .Root = "Options"
        }

        myXml.NewXmlDocument()

        Options.Password = ""
        Options.SavePass = False
        Options.Username = ""
        Options.Ip = "Localhost"
        Options.Port = 7001
        Options.MenuMusic = ""
        Options.Music = 1
        Options.Sound = 1
        Options.Volume = 100
        Options.ScreenSize = 0
        Options.HighEnd = 0
        Options.ShowNpcBar = 0

        myXml.LoadXml()

        myXml.WriteString("UserInfo", "Username", Trim$(Options.Username))
        myXml.WriteString("UserInfo", "Password", Trim$(Options.Password))
        myXml.WriteString("UserInfo", "SavePass", Trim$(Options.SavePass))

        myXml.WriteString("Connection", "Ip", Trim$(Options.Ip))
        myXml.WriteString("Connection", "Port", Trim$(Options.Port))

        myXml.WriteString("Sfx", "MenuMusic", Trim$(Options.MenuMusic))
        myXml.WriteString("Sfx", "Music", Trim$(Options.Music))
        myXml.WriteString("Sfx", "Sound", Trim$(Options.Sound))
        myXml.WriteString("Sfx", "Volume", Trim$(Options.Volume))

        myXml.WriteString("Misc", "ScreenSize", Trim$(Options.ScreenSize))
        myXml.WriteString("Misc", "HighEnd", Trim$(Options.HighEnd))
        myXml.WriteString("Misc", "ShowNpcBar", Trim$(Options.ShowNpcBar))

        myXml.CloseXml(True)
    End Sub

    Friend Sub SaveOptions()
        Dim myXml As New XmlClass With {
            .Filename = Application.StartupPath & "\Data\Config.xml",
            .Root = "Options"
        }

        myXml.LoadXml()

        myXml.WriteString("UserInfo", "Username", Trim$(Options.Username))
        myXml.WriteString("UserInfo", "Password", Trim$(Options.Password))
        myXml.WriteString("UserInfo", "SavePass", Trim$(Options.SavePass))

        myXml.WriteString("Connection", "Ip", Trim$(Options.Ip))
        myXml.WriteString("Connection", "Port", Trim$(Options.Port))

        myXml.WriteString("Sfx", "MenuMusic", Trim$(Options.MenuMusic))
        myXml.WriteString("Sfx", "Music", Trim$(Options.Music))
        myXml.WriteString("Sfx", "Sound", Trim$(Options.Sound))
        myXml.WriteString("Sfx", "Volume", Trim$(Options.Volume))

        myXml.WriteString("Misc", "ScreenSize", Trim$(Options.ScreenSize))
        myXml.WriteString("Misc", "HighEnd", Trim$(Options.HighEnd))
        myXml.WriteString("Misc", "ShowNpcBar", Trim$(Options.ShowNpcBar))

        myXml.CloseXml(True)
    End Sub

    Friend Sub LoadOptions()
        Dim myXml As New XmlClass With {
            .Filename = Application.StartupPath & "\Data\Config.xml",
            .Root = "Options"
        }

        myXml.LoadXml()
        Options.Username = myXml.ReadString("UserInfo", "Username", "")
        Options.Password = myXml.ReadString("UserInfo", "Password", "")
        Options.SavePass = myXml.ReadString("UserInfo", "SavePass", "False")

        Options.Ip = myXml.ReadString("Connection", "Ip", "127.0.0.1")
        Options.Port = Val(myXml.ReadString("Connection", "Port", "7001"))

        Options.MenuMusic = myXml.ReadString("Sfx", "MenuMusic", "")
        Options.Music = myXml.ReadString("Sfx", "Music", "1")
        Options.Sound = myXml.ReadString("Sfx", "Sound", "1")
        Options.Volume = Val(myXml.ReadString("Sfx", "Volume", "100"))

        Options.ScreenSize = myXml.ReadString("Misc", "ScreenSize", "0")
        Options.HighEnd = Val(myXml.ReadString("Misc", "HighEnd", "0"))
        Options.ShowNpcBar = Val(myXml.ReadString("Misc", "ShowNpcBar", "1"))
        myXml.CloseXml(True)

        ' show in GUI
        If Options.Music = 1 Then
            FrmOptions.optMOn.Checked = True
        Else
            FrmOptions.optMOff.Checked = False
        End If

        If Options.Music = 1 Then
            FrmOptions.optSOn.Checked = True
        Else
            FrmOptions.optSOff.Checked = False
        End If

        FrmOptions.lblVolume.Text = "Volume: " & Options.Volume
        FrmOptions.scrlVolume.Value = Options.Volume

        FrmOptions.cmbScreenSize.SelectedIndex = Options.ScreenSize

    End Sub
#End Region

#Region "Maps"
    Sub ClearMap()

        SyncLock MapLock
            Map.Name = ""
            Map.Tileset = 1
            Map.MaxX = ScreenMapx
            Map.MaxY = ScreenMapy
            Map.BootMap = 0
            Map.BootX = 0
            Map.BootY = 0
            Map.Down = 0
            Map.Left = 0
            Map.Moral = 0
            Map.Music = ""
            Map.Revision = 0
            Map.Right = 0
            Map.Up = 0

            ReDim Map.Npc(MAX_MAP_NPCS)
            ReDim Map.Tile(Map.MaxX, Map.MaxY)

            For x = 0 To ScreenMapx
                For y = 0 To ScreenMapy
                    ReDim Map.Tile(x, y).Layer(LayerType.Count - 1)
                    For l = 0 To LayerType.Count - 1
                        Map.Tile(x, y).Layer(l).Tileset = 0
                        Map.Tile(x, y).Layer(l).X = 0
                        Map.Tile(x, y).Layer(l).Y = 0
                        Map.Tile(x, y).Layer(l).AutoTile = 0
                    Next

                Next
            Next

        End SyncLock

    End Sub

    Sub ClearMapItems()
        Dim i As Integer

        For i = 1 To MAX_MAP_ITEMS
            ClearMapItem(i)
        Next

    End Sub

    Sub ClearMapItem(index As Integer)
        MapItem(index).Frame = 0
        MapItem(index).Num = 0
        MapItem(index).Value = 0
        MapItem(index).X = 0
        MapItem(index).Y = 0
    End Sub

    Sub ClearMapNpc(index As Integer)
        MapNpc(index).Attacking = 0
        MapNpc(index).AttackTimer = 0
        MapNpc(index).Dir = 0
        MapNpc(index).Map = 0
        MapNpc(index).Moving = 0
        MapNpc(index).Num = 0
        MapNpc(index).Steps = 0
        MapNpc(index).Target = 0
        MapNpc(index).TargetType = 0
        MapNpc(index).Vital(VitalType.HP) = 0
        MapNpc(index).Vital(VitalType.MP) = 0
        MapNpc(index).Vital(VitalType.SP) = 0
        MapNpc(index).X = 0
        MapNpc(index).XOffset = 0
        MapNpc(index).Y = 0
        MapNpc(index).YOffset = 0
    End Sub

    Sub ClearMapNpcs()
        Dim i As Integer

        For i = 1 To MAX_MAP_NPCS
            ClearMapNpc(i)
        Next

    End Sub

    Sub ClearBlood()
        For I = 1 To Byte.MaxValue
            Blood(I).Timer = 0
        Next
    End Sub

#End Region

#Region "Items"
    Friend Sub ClearItem(index As Integer)
        'Index = Index - 1
        Item(index) = Nothing
        Item(index) = New ItemRec
        For x = 0 To StatType.Count - 1
            ReDim Item(index).Add_Stat(x)
        Next
        For x = 0 To StatType.Count - 1
            ReDim Item(index).Stat_Req(x)
        Next

        ReDim Item(index).FurnitureBlocks(3, 3)
        ReDim Item(index).FurnitureFringe(3, 3)

        Item(index).Name = ""
    End Sub

    Friend Sub ClearChanged_Item()
        For i = 1 To MAX_ITEMS
            ItemChanged(i) = Nothing
        Next i
        ReDim ItemChanged(MAX_ITEMS)
    End Sub

    Sub ClearItems()
        Dim i As Integer

        ReDim Item(MAX_ITEMS)

        For i = 1 To MAX_ITEMS
            ClearItem(i)
        Next

    End Sub
#End Region

#Region "Resources"
    Friend Sub ClearChanged_Resource()
        For i = 1 To MAX_RESOURCES
            ResourceChanged(i) = Nothing
        Next i
        ReDim ResourceChanged(MAX_RESOURCES)
    End Sub

    Sub ClearResource(index As Integer)
        Resource(index) = Nothing
        Resource(index) = New ResourceRec With {
            .Name = ""
        }
    End Sub

    Sub ClearResources()
        Dim i As Integer

        For i = 1 To MAX_RESOURCES
            ClearResource(i)
        Next

    End Sub
#End Region

#Region "Npc's"
    Sub ClearNpcs()
        Dim i As Integer

        ReDim Npc(MAX_NPCS)

        For i = 1 To MAX_NPCS
            ClearNpc(i)
        Next

    End Sub

    Sub ClearNpc(index As Integer)
        Npc(index) = Nothing
        Npc(index) = New NpcRec With {
            .Name = "",
            .AttackSay = ""
        }
        For x = 0 To StatType.Count - 1
            ReDim Npc(index).Stat(x)
        Next

        ReDim Npc(index).DropChance(5)
        ReDim Npc(index).DropItem(5)
        ReDim Npc(index).DropItemValue(5)

        ReDim Npc(index).Skill(6)
    End Sub
#End Region

#Region "Animations"
    Sub ClearAnimation(index As Integer)
        Animation(index) = Nothing
        Animation(index) = New AnimationRec
        For x = 0 To 1
            ReDim Animation(index).Sprite(x)
        Next
        For x = 0 To 1
            ReDim Animation(index).Frames(x)
        Next
        For x = 0 To 1
            ReDim Animation(index).LoopCount(x)
        Next
        For x = 0 To 1
            ReDim Animation(index).LoopTime(x)
        Next
        Animation(index).Name = ""
    End Sub

    Sub ClearAnimations()
        Dim i As Integer

        ReDim Animation(MAX_ANIMATIONS)

        For i = 1 To MAX_ANIMATIONS
            ClearAnimation(i)
        Next

    End Sub

    Sub ClearAnimInstances()
        Dim i As Integer

        ReDim AnimInstance(MAX_ANIMATIONS)

        For i = 0 To MAX_ANIMATIONS
            For x = 0 To 1
                ReDim AnimInstance(i).Timer(x)
            Next
            For x = 0 To 1
                ReDim AnimInstance(i).Used(x)
            Next
            For x = 0 To 1
                ReDim AnimInstance(i).LoopIndex(x)
            Next
            For x = 0 To 1
                ReDim AnimInstance(i).FrameIndex(x)
            Next

            ClearAnimInstance(i)
        Next
    End Sub

    Sub ClearAnimInstance(index As Integer)
        AnimInstance(index).Animation = 0
        AnimInstance(index).X = 0
        AnimInstance(index).Y = 0

        For i = 0 To UBound(AnimInstance(index).Used)
            AnimInstance(index).Used(i) = False
        Next
        For i = 0 To UBound(AnimInstance(index).Timer)
            AnimInstance(index).Timer(i) = False
        Next
        For i = 0 To UBound(AnimInstance(index).FrameIndex)
            AnimInstance(index).FrameIndex(i) = False
        Next

        AnimInstance(index).LockType = 0
        AnimInstance(index).lockindex = 0
    End Sub
#End Region

#Region "Skills"
    Sub ClearSkills()
        Dim i As Integer

        For i = 1 To MAX_SKILLS
            ClearSkill(i)
        Next

    End Sub

    Sub ClearSkill(index As Integer)
        Skill(index) = Nothing
        Skill(index) = New SkillRec With {
            .Name = ""
        }
    End Sub
#End Region

#Region "Shops"
    Sub ClearShop(index As Integer)
        Shop(index) = Nothing
        Shop(index) = New ShopRec With {
            .Name = ""
        }
        ReDim Shop(index).TradeItem(MAX_TRADES)
        For x = 0 To MAX_TRADES
            ReDim Shop(index).TradeItem(x)
        Next
    End Sub

    Sub ClearShops()
        Dim i As Integer

        ReDim Shop(MAX_SHOPS)

        For i = 1 To MAX_SHOPS
            ClearShop(i)
        Next

    End Sub
#End Region

#Region "Bank"
    Sub ClearBank()
        ReDim Bank.Item(MAX_BANK)
        ReDim Bank.ItemRand(MAX_BANK)
        For x = 1 To MAX_BANK
            ReDim Bank.ItemRand(x).Stat(StatType.Count - 1)
        Next
    End Sub

#End Region

End Module