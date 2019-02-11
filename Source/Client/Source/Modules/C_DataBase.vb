Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

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

        While File.Exists(Path.Graphics & "characters\" & i & GfxExt)
            NumCharacters = NumCharacters + 1
            i = i + 1
        End While

        If NumCharacters = 0 Then Exit Sub
    End Sub

    Friend Sub CheckPaperdolls()
        Dim i As Integer
        i = 1

        While File.Exists(Path.Graphics & "paperdolls\" & i & GfxExt)
            NumPaperdolls = NumPaperdolls + 1
            i = i + 1
        End While

        If NumPaperdolls = 0 Then Exit Sub
    End Sub

    Friend Sub CheckAnimations()
        Dim i As Integer
        i = 1

        While File.Exists(Path.Graphics & "animations\" & i & GfxExt)
            NumAnimations = NumAnimations + 1
            i = i + 1
        End While

        If NumAnimations = 0 Then Exit Sub
    End Sub

    Friend Sub CheckSkillIcons()
        Dim i As Integer
        i = 1

        While File.Exists(Path.Graphics & "SkillIcons\" & i & GfxExt)
            NumSkillIcons = NumSkillIcons + 1
            i = i + 1
        End While

        If NumSkillIcons = 0 Then Exit Sub
    End Sub

    Friend Sub CheckFaces()
        Dim i As Integer
        i = 1

        While File.Exists(Path.Graphics & "Faces\" & i & GfxExt)
            NumFaces = NumFaces + 1
            i = i + 1
        End While

        If NumFaces = 0 Then Exit Sub
    End Sub

    Friend Sub CheckFog()
        Dim i As Integer
        i = 1

        While File.Exists(Path.Graphics & "Fogs\" & i & GfxExt)
            NumFogs = NumFogs + 1
            i = i + 1
        End While

        If NumFogs = 0 Then Exit Sub
    End Sub

    Friend Sub CheckEmotes()
        Dim i As Integer
        i = 1

        While File.Exists(Path.Graphics & "Emotes\" & i & GfxExt)
            NumEmotes = NumEmotes + 1
            i = i + 1
        End While

        If NumEmotes = 0 Then Exit Sub
    End Sub

    Friend Sub CheckPanoramas()
        Dim i As Integer
        i = 1

        While File.Exists(Path.Graphics & "Panoramas\" & i & GfxExt)
            NumPanorama = NumPanorama + 1
            i = i + 1
        End While

        If NumPanorama = 0 Then Exit Sub
    End Sub

    Friend Sub CheckParallax()
        Dim i As Integer
        i = 1

        While File.Exists(Path.Graphics & "Parallax\" & i & GfxExt)
            NumParallax = NumParallax + 1
            i = i + 1
        End While

        If NumParallax = 0 Then Exit Sub
    End Sub

    Friend Sub CacheMusic()
        Dim files As String() = Directory.GetFiles(Path.Music, "*.ogg")
        Dim maxNum As String = Directory.GetFiles(Path.Music, "*.ogg").Count
        Dim counter As Integer = 1

        For Each FileName In files
            ReDim Preserve MusicCache(counter)

            MusicCache(counter) = System.IO.Path.GetFileName(FileName)
            counter = counter + 1
            Application.DoEvents()
        Next

    End Sub

    Friend Sub CacheSound()
        Dim files As String() = Directory.GetFiles(Path.Sounds, "*.ogg")
        Dim maxNum As String = Directory.GetFiles(Path.Sounds, "*.ogg").Count
        Dim counter As Integer = 1

        For Each FileName In files
            ReDim Preserve SoundCache(counter)

            SoundCache(counter) = System.IO.Path.GetFileName(FileName)
            counter = counter + 1
            Application.DoEvents()
        Next

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
        Npc(index) = New NpcStruct With {
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

    Friend Sub ClearChangedItem()
        For i = 1 To MAX_ITEMS
            Item_Changed(i) = Nothing
        Next i
        ReDim Item_Changed(MAX_ITEMS)
    End Sub

#Region "Skills"

    Sub ClearSkills()
        Dim i As Integer

        For i = 1 To MAX_SKILLS
            ClearSkill(i)
        Next

    End Sub

    Sub ClearSkill(index As Integer)
        Skill(index) = Nothing
        Skill(index) = New SkillStruct With {
            .Name = ""
        }
    End Sub

#End Region

End Module