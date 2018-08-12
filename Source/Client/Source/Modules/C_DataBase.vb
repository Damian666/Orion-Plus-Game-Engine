Imports System.IO
Imports System.Windows.Forms
Imports System.Linq

Imports Ini = ASFW.IO.FileIO.TextFile

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
        
        Dim path = Application.StartupPath & "\Data\Config.ini"
        If Not File.Exists(path) Then File.Create(path).Dispose

        Options.Password = ""
        Options.SavePass = False
        Options.Username = ""
        Options.Ip = "127.0.0.1"
        Options.Port = 7001
        Options.MenuMusic = ""
        Options.Music = 1
        Options.Sound = 1
        Options.Volume = 100
        Options.ScreenSize = 0
        Options.HighEnd = 0
        Options.ShowNpcBar = 0

        Ini.PutVar(path, "UserInfo", "Username", Trim$(Options.Username))
        Ini.PutVar(path, "UserInfo", "Password", Trim$(Options.Password))
        Ini.PutVar(path, "UserInfo", "SavePass", Trim$(Options.SavePass))

        Ini.PutVar(path, "Connection", "Ip", Trim$(Options.Ip))
        Ini.PutVar(path, "Connection", "Port", Trim$(Options.Port))

        Ini.PutVar(path, "Sfx", "MenuMusic", Trim$(Options.MenuMusic))
        Ini.PutVar(path, "Sfx", "Music", Trim$(Options.Music))
        Ini.PutVar(path, "Sfx", "Sound", Trim$(Options.Sound))
        Ini.PutVar(path, "Sfx", "Volume", Trim$(Options.Volume))

        Ini.PutVar(path, "Misc", "ScreenSize", Trim$(Options.ScreenSize))
        Ini.PutVar(path, "Misc", "HighEnd", Trim$(Options.HighEnd))
        Ini.PutVar(path, "Misc", "ShowNpcBar", Trim$(Options.ShowNpcBar))

    End Sub

    Friend Sub SaveOptions()
        
        Dim path = Application.StartupPath & "\Data\Config.ini"
        If Not File.Exists(path) Then File.Create(path).Dispose

        Ini.PutVar(path, "UserInfo", "Username", Trim$(Options.Username))
        Ini.PutVar(path, "UserInfo", "Password", Trim$(Options.Password))
        Ini.PutVar(path, "UserInfo", "SavePass", Trim$(Options.SavePass))

        Ini.PutVar(path, "Connection", "Ip", Trim$(Options.Ip))
        Ini.PutVar(path, "Connection", "Port", Trim$(Options.Port))

        Ini.PutVar(path, "Sfx", "MenuMusic", Trim$(Options.MenuMusic))
        Ini.PutVar(path, "Sfx", "Music", Trim$(Options.Music))
        Ini.PutVar(path, "Sfx", "Sound", Trim$(Options.Sound))
        Ini.PutVar(path, "Sfx", "Volume", Trim$(Options.Volume))

        Ini.PutVar(path, "Misc", "ScreenSize", Trim$(Options.ScreenSize))
        Ini.PutVar(path, "Misc", "HighEnd", Trim$(Options.HighEnd))
        Ini.PutVar(path, "Misc", "ShowNpcBar", Trim$(Options.ShowNpcBar))
        
    End Sub

    Friend Sub LoadOptions()
        
        Dim path = Application.StartupPath & "\Data\Config.ini"
        If Not File.Exists(path) Then File.Create(path).Dispose

        Options.Username = Ini.GetVar(path, "UserInfo", "Username")
        Options.Password = Ini.GetVar(path, "UserInfo", "Password")
        Options.SavePass = Ini.GetVar(path, "UserInfo", "SavePass")

        Options.Ip = Ini.GetVar(path, "Connection", "Ip")
        Options.Port = Val(Ini.GetVar(path, "Connection", "Port"))

        Options.MenuMusic = Ini.GetVar(path, "Sfx", "MenuMusic")
        Options.Music = Ini.GetVar(path, "Sfx", "Music")
        Options.Sound = Ini.GetVar(path, "Sfx", "Sound")
        Options.Volume = Val(Ini.GetVar(path, "Sfx", "Volume"))

        Options.ScreenSize = Ini.GetVar(path, "Misc", "ScreenSize")
        Options.HighEnd = Val(Ini.GetVar(path, "Misc", "HighEnd"))
        Options.ShowNpcBar = Val(Ini.GetVar(path, "Misc", "ShowNpcBar"))

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

#Region "Blood"

    Sub ClearBlood()
        For I = 1 To Byte.MaxValue
            Blood(I).Timer = 0
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

End Module