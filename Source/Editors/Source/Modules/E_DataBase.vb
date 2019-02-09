Imports System.IO

Module ClientDataBase
    Friend GameRand As New Random()

    Sub ClearTempTile()
        Dim X As Integer
        Dim Y As Integer
        ReDim TempTile(Map.MaxX, Map.MaxY)

        For X = 0 To Map.MaxX
            For Y = 0 To Map.MaxY
                TempTile(X, Y).DoorOpen = 0
            Next
        Next

    End Sub

    Friend Function IsInBounds()
        IsInBounds = False
        If (CurX >= 0) Then
            If (CurX <= Map.MaxX) Then
                If (CurY >= 0) Then
                    If (CurY <= Map.MaxY) Then
                        IsInBounds = True
                    End If
                End If
            End If
        End If

    End Function

    Friend Function GetTickCount()
        Return Environment.TickCount
    End Function

    Friend Function Rand(MaxNumber As Integer, Optional MinNumber As Integer = 0) As Integer
        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return GameRand.Next(MinNumber, MaxNumber)
    End Function

    Friend Sub CheckTilesets()
        Dim i As Integer
        Dim tmp As Bitmap
        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "\tilesets\" & i & GFX_EXT)
            NumTileSets = NumTileSets + 1
            i = i + 1
        End While

        ReDim TilesetsClr(NumTileSets)

        For i = 1 To NumTileSets
            tmp = New Bitmap(Application.StartupPath & GFX_PATH & "\tilesets\" & i & GFX_EXT)
            TilesetsClr(NumTileSets) = tmp.GetPixel(0, 0)
        Next
        If NumTileSets = 0 Then Exit Sub

    End Sub

    Friend Sub CheckCharacters()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "characters\" & i & GFX_EXT)
            NumCharacters = NumCharacters + 1
            i = i + 1
        End While

        If NumCharacters = 0 Then Exit Sub
    End Sub

    Friend Sub CheckPaperdolls()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "paperdolls\" & i & GFX_EXT)
            NumPaperdolls = NumPaperdolls + 1
            i = i + 1
        End While

        If NumPaperdolls = 0 Then Exit Sub
    End Sub

    Friend Sub CheckAnimations()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "animations\" & i & GFX_EXT)
            NumAnimations = NumAnimations + 1
            i = i + 1
        End While

        If NumAnimations = 0 Then Exit Sub
    End Sub

    Friend Sub CheckResources()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "Resources\" & i & GFX_EXT)
            NumResources = NumResources + 1
            i = i + 1
        End While

        If NumResources = 0 Then Exit Sub
    End Sub

    Friend Sub CheckSkillIcons()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "SkillIcons\" & i & GFX_EXT)
            NumSkillIcons = NumSkillIcons + 1
            i = i + 1
        End While

        If NumSkillIcons = 0 Then Exit Sub
    End Sub

    Friend Sub CheckFaces()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "Faces\" & i & GFX_EXT)
            NumFaces = NumFaces + 1
            i = i + 1
        End While

        If NumFaces = 0 Then Exit Sub
    End Sub

    Friend Sub CheckFog()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "Fogs\" & i & GFX_EXT)
            NumFogs = NumFogs + 1
            i = i + 1
        End While

        If NumFogs = 0 Then Exit Sub
    End Sub

    Friend Sub CacheMusic()
        Dim Files As String() = Directory.GetFiles(Application.StartupPath & MUSIC_PATH, "*.ogg")
        Dim MaxNum As String = Directory.GetFiles(Application.StartupPath & MUSIC_PATH, "*.ogg").Count
        Dim Counter As Integer = 1

        For Each FileName In Files
            ReDim Preserve MusicCache(Counter)

            MusicCache(Counter) = Path.GetFileName(FileName)
            Counter = Counter + 1
            Application.DoEvents()
        Next

    End Sub

    Friend Sub CacheSound()
        Dim Files As String() = Directory.GetFiles(Application.StartupPath & SOUND_PATH, "*.ogg")
        Dim MaxNum As String = Directory.GetFiles(Application.StartupPath & SOUND_PATH, "*.ogg").Count
        Dim Counter As Integer = 1

        For Each FileName In Files
            ReDim Preserve SoundCache(Counter)

            SoundCache(Counter) = Path.GetFileName(FileName)
            Counter = Counter + 1
            Application.DoEvents()
        Next

    End Sub

    Friend Function GetFileContents(FullPath As String, Optional ByRef ErrInfo As String = "") As String
        Dim strContents As String
        Dim objReader As StreamReader
        strContents = ""
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return strContents
    End Function

    Sub ClearMap()
        SyncLock MapLock
            Map.mapNum = 0
            Map.Name = ""
            Map.tileset = 1
            Map.MaxX = SCREEN_MAPX
            Map.MaxY = SCREEN_MAPY
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

            For x = 0 To SCREEN_MAPX
                For y = 0 To SCREEN_MAPY
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

    Friend Sub ClearChanged_Resource()
        For i = 1 To MAX_RESOURCES
            Resource_Changed(i) = Nothing
        Next i
        ReDim Resource_Changed(MAX_RESOURCES)
    End Sub

    Sub ClearResource(index As Integer)
        Resource(index) = Nothing
        Resource(index) = New ResourceStruct With {
            .Name = ""
        }
    End Sub

    Sub ClearResources()
        Dim i As Integer

        For i = 1 To MAX_RESOURCES
            ClearResource(i)
        Next

    End Sub

    Sub ClearNpcs()
        Dim i As Integer

        For i = 1 To MAX_NPCS
            ClearNpc(i)
        Next

    End Sub

    Sub ClearNpc(index As Integer)
        Npc(index) = Nothing
        Npc(index).Name = ""
        Npc(index).AttackSay = ""
        ReDim Npc(index).Stat(StatType.Count - 1)
        ReDim Npc(index).Skill(MAX_NPC_SKILLS)

        ReDim Npc(index).DropItem(5)
        ReDim Npc(index).DropItemValue(5)
        ReDim Npc(index).DropChance(5)
    End Sub

    Sub ClearAnimation(index As Integer)
        Animation(index) = Nothing
        Animation(index) = New AnimationStruct
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

        For i = 1 To MAX_ANIMATIONS
            ClearAnimation(i)
        Next

    End Sub

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

    Sub ClearShop(index As Integer)
        Shop(index) = Nothing
        Shop(index) = New ShopStruct With {
            .Name = ""
        }
        ReDim Shop(index).TradeItem(MAX_TRADES)
    End Sub

    Sub ClearShops()
        Dim i As Integer

        For i = 1 To MAX_SHOPS
            ClearShop(i)
        Next

    End Sub

End Module