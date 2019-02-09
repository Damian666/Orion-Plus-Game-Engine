Imports System.IO

Module E_Editors

#Region "Animation Editor"

    Friend Sub AnimationEditorInit()

        If FrmAnimation.Visible = False Then Exit Sub

        Editorindex = FrmAnimation.lstIndex.SelectedIndex + 1

        With Animation(Editorindex)

            ' find the music we have set
            FrmAnimation.cmbSound.Items.Clear()
            FrmAnimation.cmbSound.Items.Add("None")

            If UBound(SoundCache) > 0 Then
                For i = 1 To UBound(SoundCache)
                    FrmAnimation.cmbSound.Items.Add(SoundCache(i))
                Next
            End If

            If Trim$(Animation(Editorindex).Sound) = "None" OrElse Trim$(Animation(Editorindex).Sound) = "" Then
                FrmAnimation.cmbSound.SelectedIndex = 0
            Else
                For i = 1 To FrmAnimation.cmbSound.Items.Count
                    If FrmAnimation.cmbSound.Items(i - 1).ToString = Trim$(.Sound) Then
                        FrmAnimation.cmbSound.SelectedIndex = i - 1
                        Exit For
                    End If
                Next
            End If
            FrmAnimation.txtName.Text = Trim$(.Name)

            FrmAnimation.nudSprite0.Value = .Sprite(0)
            FrmAnimation.nudFrameCount0.Value = .Frames(0)
            FrmAnimation.nudLoopCount0.Value = .LoopCount(0)
            FrmAnimation.nudLoopTime0.Value = .LoopTime(0)

            FrmAnimation.nudSprite1.Value = .Sprite(1)
            FrmAnimation.nudFrameCount1.Value = .Frames(1)
            FrmAnimation.nudLoopCount1.Value = .LoopCount(1)
            FrmAnimation.nudLoopTime1.Value = .LoopTime(1)

            Editorindex = FrmAnimation.lstIndex.SelectedIndex + 1
        End With

        EditorAnim_DrawAnim()
        Animation_Changed(Editorindex) = True
    End Sub

    Friend Sub AnimationEditorOk()
        Dim i As Integer

        For i = 1 To MAX_ANIMATIONS
            If Animation_Changed(i) Then
                SendSaveAnimation(i)
            End If
        Next

        FrmAnimation.Visible = False
        Editor = 0
        ClearChanged_Animation()
    End Sub

    Friend Sub AnimationEditorCancel()
        Editor = 0
        FrmAnimation.Visible = False
        ClearChanged_Animation()
        ClearAnimations()
        SendRequestAnimations()
    End Sub

    Friend Sub ClearChanged_Animation()
        For i = 0 To MAX_ANIMATIONS
            Animation_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Map Editor"

    Friend Sub MapPropertiesInit()
        Dim X As Integer, Y As Integer, i As Integer

        frmMapEditor.txtName.Text = Trim$(Map.Name)

        ' find the music we have set
        frmMapEditor.lstMusic.Items.Clear()
        frmMapEditor.lstMusic.Items.Add("None")

        If UBound(MusicCache) > 0 Then
            For i = 1 To UBound(MusicCache)
                frmMapEditor.lstMusic.Items.Add(MusicCache(i))
            Next
        End If

        If Trim$(Map.Music) = "None" Then
            frmMapEditor.lstMusic.SelectedIndex = 0
        Else
            For i = 1 To frmMapEditor.lstMusic.Items.Count
                If frmMapEditor.lstMusic.Items(i - 1).ToString = Trim$(Map.Music) Then
                    frmMapEditor.lstMusic.SelectedIndex = i - 1
                    Exit For
                End If
            Next
        End If

        ' rest of it
        frmMapEditor.nudUp.Value = Map.Up
        frmMapEditor.nudDown.Value = Map.Down
        frmMapEditor.nudLeft.Value = Map.Left
        frmMapEditor.nudRight.Value = Map.Right
        frmMapEditor.cmbMoral.SelectedIndex = Map.Moral
        frmMapEditor.nudSpawnMap.Value = Map.BootMap
        frmMapEditor.nudSpawnX.Value = Map.BootX
        frmMapEditor.nudSpawnY.Value = Map.BootY

        If Map.Instanced = 1 Then
            frmMapEditor.chkInstance.Checked = True
        Else
            frmMapEditor.chkInstance.Checked = False
        End If

        frmMapEditor.lstMapNpc.Items.Clear()

        For X = 1 To MAX_MAP_NPCS
            If Map.Npc(X) = 0 Then
                frmMapEditor.lstMapNpc.Items.Add("No NPC")
            Else
                frmMapEditor.lstMapNpc.Items.Add(X & ": " & Trim$(Npc(Map.Npc(X)).Name))
            End If

        Next

        frmMapEditor.cmbNpcList.Items.Clear()
        frmMapEditor.cmbNpcList.Items.Add("No NPC")

        For Y = 1 To MAX_NPCS
            frmMapEditor.cmbNpcList.Items.Add(Y & ": " & Trim$(Npc(Y).Name))
        Next

        frmMapEditor.lblMap.Text = "Current Map: " & Map.mapNum
        frmMapEditor.nudMaxX.Value = Map.MaxX
        frmMapEditor.nudMaxY.Value = Map.MaxY

        frmMapEditor.cmbTileSets.SelectedIndex = 0
        frmMapEditor.cmbLayers.SelectedIndex = 0
        frmMapEditor.cmbAutoTile.SelectedIndex = 0

        frmMapEditor.cmbWeather.SelectedIndex = Map.WeatherType
        frmMapEditor.nudFog.Value = Map.Fogindex
        frmMapEditor.nudIntensity.Value = Map.WeatherIntensity

        SelectedTab = 1

        GameWindow.SetView(New SFML.Graphics.View(New SFML.Graphics.FloatRect(0, 0, frmMapEditor.picScreen.Width, frmMapEditor.picScreen.Height)))

        frmMapEditor.tslCurMap.Text = "Map: " & Map.mapNum

        ' show the form
        frmMapEditor.Visible = True

        GameStarted = True

        frmMapEditor.picScreen.Focus()

        InitMapEditor = False
    End Sub

    Friend Sub MapEditorInit()
        ' we're in the map editor
        InMapEditor = True

        ' set the scrolly bars
        If Map.tileset = 0 Then Map.tileset = 1
        If Map.tileset > NumTileSets Then Map.tileset = 1

        EditorTileSelStart = New Point(0, 0)
        EditorTileSelEnd = New Point(1, 1)

        'clear memory
        'ReDim TileSetImgsLoaded(NumTileSets)
        'For i = 0 To NumTileSets
        '    TileSetImgsLoaded(i) = False
        'Next

        ' set the scrollbars
        frmMapEditor.scrlPictureY.Maximum = (frmMapEditor.picBackSelect.Height \ PIC_Y) \ 2 ' \2 is new, lets test
        frmMapEditor.scrlPictureX.Maximum = (frmMapEditor.picBackSelect.Width \ PIC_X) \ 2

        'set map names
        frmMapEditor.cmbMapList.Items.Clear()
        FrmVisualWarp.lstMaps.Items.Clear()

        For i = 1 To MAX_MAPS
            frmMapEditor.cmbMapList.Items.Add(i & ": " & MapNames(i))
            FrmVisualWarp.lstMaps.Items.Add(i & ": " & MapNames(i))
        Next

        If Map.mapNum > 0 Then
            frmMapEditor.cmbMapList.SelectedIndex = Map.mapNum - 1
        Else
            frmMapEditor.cmbMapList.SelectedIndex = 0
        End If

        ' set shops for the shop attribute
        frmMapEditor.cmbShop.Items.Add("None")
        For i = 1 To MAX_SHOPS
            frmMapEditor.cmbShop.Items.Add(i & ": " & Shop(i).Name)
        Next
        ' we're not in a shop
        frmMapEditor.cmbShop.SelectedIndex = 0

        frmMapEditor.optBlocked.Checked = True

        frmMapEditor.cmbTileSets.Items.Clear()
        For i = 1 To NumTileSets
            frmMapEditor.cmbTileSets.Items.Add("Tileset " & i)
        Next

        frmMapEditor.cmbTileSets.SelectedIndex = 0
        frmMapEditor.cmbLayers.SelectedIndex = 0

        InitMapProperties = True

        If MapData = True Then GettingMap = False

    End Sub

    Friend Sub MapEditorTileScroll()
        picbacktop = (frmMapEditor.scrlPictureY.Value * PIC_Y)
        picbackleft = (frmMapEditor.scrlPictureX.Value * PIC_X)

    End Sub

    Friend Sub MapEditorChooseTile(Button As Integer, X As Single, Y As Single)

        If Button = MouseButtons.Left Then 'Left Mouse Button

            EditorTileWidth = 1
            EditorTileHeight = 1

            If frmMapEditor.cmbAutoTile.SelectedIndex > 0 Then
                Select Case frmMapEditor.cmbAutoTile.SelectedIndex
                    Case 1 ' autotile
                        EditorTileWidth = 2
                        EditorTileHeight = 3
                    Case 2 ' fake autotile
                        EditorTileWidth = 1
                        EditorTileHeight = 1
                    Case 3 ' animated
                        EditorTileWidth = 6
                        EditorTileHeight = 3
                    Case 4 ' cliff
                        EditorTileWidth = 2
                        EditorTileHeight = 2
                    Case 5 ' waterfall
                        EditorTileWidth = 2
                        EditorTileHeight = 3
                End Select
            End If

            EditorTileX = X \ PIC_X
            EditorTileY = Y \ PIC_Y

            EditorTileSelStart = New Point(EditorTileX, EditorTileY)
            EditorTileSelEnd = New Point(EditorTileX + EditorTileWidth, EditorTileY + EditorTileHeight)

        End If

    End Sub

    Friend Sub MapEditorDrag(Button As Integer, X As Single, Y As Single)

        If Button = MouseButtons.Left Then 'Left Mouse Button
            ' convert the pixel number to tile number
            X = (X \ PIC_X) + 1
            Y = (Y \ PIC_Y) + 1
            ' check it's not out of bounds
            If X < 0 Then X = 0
            If X > frmMapEditor.picBackSelect.Width / PIC_X Then X = frmMapEditor.picBackSelect.Width / PIC_X
            If Y < 0 Then Y = 0
            If Y > frmMapEditor.picBackSelect.Height / PIC_Y Then Y = frmMapEditor.picBackSelect.Height / PIC_Y
            ' find out what to set the width + height of map editor to
            If X > EditorTileX Then ' drag right
                'EditorTileWidth = X
                EditorTileWidth = X - EditorTileX
            Else ' drag left
                ' TO DO
            End If
            If Y > EditorTileY Then ' drag down
                'EditorTileHeight = Y
                EditorTileHeight = Y - EditorTileY
            Else ' drag up
                ' TO DO
            End If

            EditorTileSelStart = New Point(EditorTileX, EditorTileY)
            EditorTileSelEnd = New Point(EditorTileWidth, EditorTileHeight)
        End If

    End Sub

    Friend Sub MapEditorMouseDown(Button As Integer, X As Integer, Y As Integer, Optional movedMouse As Boolean = True)
        Dim i As Integer
        Dim CurLayer As Integer

        CurLayer = frmMapEditor.cmbLayers.SelectedIndex + 1

        If Not IsInBounds() Then Exit Sub
        If Button = MouseButtons.Left Then
            If SelectedTab = 1 Then 'single tile
                If EditorTileWidth = 1 AndAlso EditorTileHeight = 1 Then

                    MapEditorSetTile(CurX, CurY, CurLayer, False, frmMapEditor.cmbAutoTile.SelectedIndex)
                Else ' multi tile!
                    If frmMapEditor.cmbAutoTile.SelectedIndex = 0 Then
                        MapEditorSetTile(CurX, CurY, CurLayer, True)
                    Else
                        MapEditorSetTile(CurX, CurY, CurLayer, , frmMapEditor.cmbAutoTile.SelectedIndex)
                    End If
                End If
            ElseIf SelectedTab = 2 Then
                With Map.Tile(CurX, CurY)
                    ' blocked tile
                    If frmMapEditor.optBlocked.Checked = True Then .Type = TileType.Blocked
                    ' warp tile
                    If frmMapEditor.optWarp.Checked = True Then
                        .Type = TileType.Warp
                        .Data1 = EditorWarpMap
                        .Data2 = EditorWarpX
                        .Data3 = EditorWarpY
                    End If
                    ' item spawn
                    If frmMapEditor.optItem.Checked = True Then
                        .Type = TileType.Item
                        .Data1 = ItemEditorNum
                        .Data2 = ItemEditorValue
                        .Data3 = 0
                    End If
                    ' npc avoid
                    If frmMapEditor.optNpcAvoid.Checked = True Then
                        .Type = TileType.NpcAvoid
                        .Data1 = 0
                        .Data2 = 0
                        .Data3 = 0
                    End If
                    ' key
                    If frmMapEditor.optKey.Checked = True Then
                        .Type = TileType.Key
                        .Data1 = KeyEditorNum
                        .Data2 = KeyEditorTake
                        .Data3 = 0
                    End If
                    ' key open
                    If frmMapEditor.optKeyOpen.Checked = True Then
                        .Type = TileType.KeyOpen
                        .Data1 = KeyOpenEditorX
                        .Data2 = KeyOpenEditorY
                        .Data3 = 0
                    End If
                    ' resource
                    If frmMapEditor.optResource.Checked = True Then
                        .Type = TileType.Resource
                        .Data1 = ResourceEditorNum
                        .Data2 = 0
                        .Data3 = 0
                    End If
                    ' door
                    If frmMapEditor.optDoor.Checked = True Then
                        .Type = TileType.Door
                        .Data1 = EditorWarpMap
                        .Data2 = EditorWarpX
                        .Data3 = EditorWarpY
                    End If
                    ' npc spawn
                    If frmMapEditor.optNpcSpawn.Checked = True Then
                        .Type = TileType.NpcSpawn
                        .Data1 = SpawnNpcNum
                        .Data2 = SpawnNpcDir
                        .Data3 = 0
                    End If
                    ' shop
                    If frmMapEditor.optShop.Checked = True Then
                        .Type = TileType.Shop
                        .Data1 = EditorShop
                        .Data2 = 0
                        .Data3 = 0
                    End If
                    ' bank
                    If frmMapEditor.optBank.Checked = True Then
                        .Type = TileType.Bank
                        .Data1 = 0
                        .Data2 = 0
                        .Data3 = 0
                    End If
                    ' heal
                    If frmMapEditor.optHeal.Checked = True Then
                        .Type = TileType.Heal
                        .Data1 = MapEditorHealType
                        .Data2 = MapEditorHealAmount
                        .Data3 = 0
                    End If
                    ' trap
                    If frmMapEditor.optTrap.Checked = True Then
                        .Type = TileType.Trap
                        .Data1 = MapEditorHealAmount
                        .Data2 = 0
                        .Data3 = 0
                    End If
                    'Housing
                    If frmMapEditor.optHouse.Checked Then
                        .Type = TileType.House
                        .Data1 = HouseTileindex
                        .Data2 = 0
                        .Data3 = 0
                    End If
                    'craft tile
                    If frmMapEditor.optCraft.Checked Then
                        .Type = TileType.Craft
                        .Data1 = 0
                        .Data2 = 0
                        .Data3 = 0
                    End If
                    If frmMapEditor.optLight.Checked Then
                        .Type = TileType.Light
                        .Data1 = 0
                        .Data2 = 0
                        .Data3 = 0
                    End If
                End With
            ElseIf SelectedTab = 4 Then
                If movedMouse Then Exit Sub
                ' find what tile it is
                X = X - ((X \ PIC_X) * PIC_X)
                Y = Y - ((Y \ PIC_Y) * PIC_Y)
                ' see if it hits an arrow
                For i = 1 To 4
                    If X >= DirArrowX(i) AndAlso X <= DirArrowX(i) + 8 Then
                        If Y >= DirArrowY(i) AndAlso Y <= DirArrowY(i) + 8 Then
                            ' flip the value.
                            SetDirBlock(Map.Tile(CurX, CurY).DirBlock, (i), Not IsDirBlocked(Map.Tile(CurX, CurY).DirBlock, (i)))
                            Exit Sub
                        End If
                    End If
                Next
            ElseIf SelectedTab = 5 Then
                If frmEvents.Visible = False Then
                    AddEvent(CurX, CurY)
                End If
            End If
        End If

        If Button = MouseButtons.Right Then
            If SelectedTab = 1 Then

                With Map.Tile(CurX, CurY)
                    ' clear layer
                    .Layer(CurLayer).X = 0
                    .Layer(CurLayer).Y = 0
                    .Layer(CurLayer).Tileset = 0
                    If .Layer(CurLayer).AutoTile > 0 Then
                        .Layer(CurLayer).AutoTile = 0
                        ' do a re-init so we can see our changes
                        InitAutotiles()
                    End If
                    CacheRenderState(X, Y, CurLayer)
                End With

            ElseIf SelectedTab = 2 Then
                With Map.Tile(CurX, CurY)
                    ' clear attribute
                    .Type = 0
                    .Data1 = 0
                    .Data2 = 0
                    .Data3 = 0
                End With
            ElseIf SelectedTab = 5 Then
                DeleteEvent(CurX, CurY)
            End If
        End If

    End Sub

    Friend Sub MapEditorCancel()
        InMapEditor = False
        frmMapEditor.Visible = False
        GettingMap = True

        InitAutotiles()

    End Sub

    Friend Sub MapEditorSend()
        SendEditorMap()
        'InMapEditor = False
        'frmMapEditor.Visible = False
        GettingMap = True

    End Sub

    Friend Sub MapEditorSetTile(X As Integer, Y As Integer, CurLayer As Integer, Optional multitile As Boolean = False, Optional theAutotile As Byte = 0)
        Dim x2 As Integer, y2 As Integer

        If theAutotile > 0 Then
            With Map.Tile(X, Y)
                ' set layer
                .Layer(CurLayer).X = EditorTileX
                .Layer(CurLayer).Y = EditorTileY
                .Layer(CurLayer).Tileset = frmMapEditor.cmbTileSets.SelectedIndex + 1
                .Layer(CurLayer).AutoTile = theAutotile
                CacheRenderState(X, Y, CurLayer)
            End With
            ' do a re-init so we can see our changes
            InitAutotiles()
            Exit Sub
        End If

        If Not multitile Then ' single
            With Map.Tile(X, Y)
                ' set layer
                .Layer(CurLayer).X = EditorTileX
                .Layer(CurLayer).Y = EditorTileY
                .Layer(CurLayer).Tileset = frmMapEditor.cmbTileSets.SelectedIndex + 1
                .Layer(CurLayer).AutoTile = 0
                CacheRenderState(X, Y, CurLayer)
            End With
        Else ' multitile
            y2 = 0 ' starting tile for y axis
            For Y = CurY To CurY + EditorTileHeight - 1
                x2 = 0 ' re-set x count every y loop
                For X = CurX To CurX + EditorTileWidth - 1
                    If X >= 0 AndAlso X <= Map.MaxX Then
                        If Y >= 0 AndAlso Y <= Map.MaxY Then
                            With Map.Tile(X, Y)
                                .Layer(CurLayer).X = EditorTileX + x2
                                .Layer(CurLayer).Y = EditorTileY + y2
                                .Layer(CurLayer).Tileset = frmMapEditor.cmbTileSets.SelectedIndex + 1
                                .Layer(CurLayer).AutoTile = 0
                                CacheRenderState(X, Y, CurLayer)
                            End With
                        End If
                    End If
                    x2 = x2 + 1
                Next
                y2 = y2 + 1
            Next
        End If
    End Sub

    Friend Sub MapEditorClearLayer()
        Dim X As Integer
        Dim Y As Integer
        Dim CurLayer As Integer

        CurLayer = frmMapEditor.cmbLayers.SelectedIndex + 1

        If CurLayer = 0 Then Exit Sub

        ' ask to clear layer
        If MsgBox("Are you sure you wish to clear this layer?", vbYesNo, "MapEditor") = vbYes Then
            For X = 0 To Map.MaxX
                For Y = 0 To Map.MaxY
                    With Map.Tile(X, Y)
                        .Layer(CurLayer).X = 0
                        .Layer(CurLayer).Y = 0
                        .Layer(CurLayer).Tileset = 0
                        .Layer(CurLayer).AutoTile = 0
                        CacheRenderState(X, Y, CurLayer)
                    End With
                Next
            Next
        End If
    End Sub

    Friend Sub MapEditorFillLayer(Optional theAutotile As Byte = 0)
        Dim X As Integer
        Dim Y As Integer
        Dim CurLayer As Integer

        CurLayer = frmMapEditor.cmbLayers.SelectedIndex + 1

        If MsgBox("Are you sure you wish to fill this layer?", vbYesNo, "Map Editor") = vbYes Then
            If theAutotile > 0 Then
                For X = 0 To Map.MaxX
                    For Y = 0 To Map.MaxY
                        Map.Tile(X, Y).Layer(CurLayer).X = EditorTileX
                        Map.Tile(X, Y).Layer(CurLayer).Y = EditorTileY
                        Map.Tile(X, Y).Layer(CurLayer).Tileset = frmMapEditor.cmbTileSets.SelectedIndex + 1
                        Map.Tile(X, Y).Layer(CurLayer).AutoTile = theAutotile
                        CacheRenderState(X, Y, CurLayer)
                    Next
                Next

                ' do a re-init so we can see our changes
                InitAutotiles()
            Else
                For X = 0 To Map.MaxX
                    For Y = 0 To Map.MaxY
                        Map.Tile(X, Y).Layer(CurLayer).X = EditorTileX
                        Map.Tile(X, Y).Layer(CurLayer).Y = EditorTileY
                        Map.Tile(X, Y).Layer(CurLayer).Tileset = frmMapEditor.cmbTileSets.SelectedIndex + 1
                        CacheRenderState(X, Y, CurLayer)
                    Next
                Next
            End If
        End If
    End Sub

    Friend Sub ClearAttributeDialogue()

        With frmMapEditor
            .fraNpcSpawn.Visible = False
            .fraResource.Visible = False
            .fraMapItem.Visible = False
            .fraMapKey.Visible = False
            .fraKeyOpen.Visible = False
            .fraMapWarp.Visible = False
            .fraShop.Visible = False
            .fraHeal.Visible = False
            .fraTrap.Visible = False
            .fraBuyHouse.Visible = False
        End With

    End Sub

    Friend Sub MapEditorClearAttribs()
        Dim X As Integer
        Dim Y As Integer

        If MsgBox("Are you sure you wish to clear the attributes on this map?", vbYesNo, "MapEditor") = vbYes Then

            For X = 0 To Map.MaxX
                For Y = 0 To Map.MaxY
                    Map.Tile(X, Y).Type = 0
                Next
            Next

        End If

    End Sub

    Friend Sub MapEditorLeaveMap()

        If InMapEditor Then
            If MsgBox("Save changes to current map?", vbYesNo) = vbYes Then
                MapEditorSend()
            Else
                MapEditorCancel()
            End If
        End If

    End Sub

#End Region



#Region "Npc Editor"

    Friend Sub NpcEditorInit()
        Dim i As Integer

        If frmNPC.Visible = False Then Exit Sub
        Editorindex = frmNPC.lstIndex.SelectedIndex + 1
        frmNPC.cmbDropSlot.SelectedIndex = 0
        If Npc(Editorindex).AttackSay Is Nothing Then Npc(Editorindex).AttackSay = ""
        If Npc(Editorindex).Name Is Nothing Then Npc(Editorindex).Name = ""

        With frmNPC
            'populate combo boxes
            .cmbAnimation.Items.Clear()
            .cmbAnimation.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                .cmbAnimation.Items.Add(i & ": " & Animation(i).Name)
            Next

            .cmbQuest.Items.Clear()
            .cmbQuest.Items.Add("None")
            For i = 1 To MAX_QUESTS
                .cmbQuest.Items.Add(i & ": " & Quest(i).Name)
            Next

            .cmbItem.Items.Clear()
            .cmbItem.Items.Add("None")
            For i = 1 To MAX_ITEMS
                .cmbItem.Items.Add(i & ": " & Item(i).Name)
            Next

            .txtName.Text = Trim$(Npc(Editorindex).Name)
            .txtAttackSay.Text = Trim$(Npc(Editorindex).AttackSay)

            If Npc(Editorindex).Sprite < 0 OrElse Npc(Editorindex).Sprite > .nudSprite.Maximum Then Npc(Editorindex).Sprite = 0
            .nudSprite.Value = Npc(Editorindex).Sprite
            .nudSpawnSecs.Value = Npc(Editorindex).SpawnSecs
            .cmbBehaviour.SelectedIndex = Npc(Editorindex).Behaviour
            .cmbFaction.SelectedIndex = Npc(Editorindex).Faction
            .nudRange.Value = Npc(Editorindex).Range
            .nudChance.Value = Npc(Editorindex).DropChance(frmNPC.cmbDropSlot.SelectedIndex + 1)
            .cmbItem.SelectedIndex = Npc(Editorindex).DropItem(frmNPC.cmbDropSlot.SelectedIndex + 1)

            .nudAmount.Value = Npc(Editorindex).DropItemValue(frmNPC.cmbDropSlot.SelectedIndex + 1)

            .nudHp.Value = Npc(Editorindex).Hp
            .nudExp.Value = Npc(Editorindex).Exp
            .nudLevel.Value = Npc(Editorindex).Level
            .nudDamage.Value = Npc(Editorindex).Damage

            .cmbQuest.SelectedIndex = Npc(Editorindex).QuestNum
            .cmbSpawnPeriod.SelectedIndex = Npc(Editorindex).SpawnTime

            .nudStrength.Value = Npc(Editorindex).Stat(StatType.Strength)
            .nudEndurance.Value = Npc(Editorindex).Stat(StatType.Endurance)
            .nudIntelligence.Value = Npc(Editorindex).Stat(StatType.Intelligence)
            .nudSpirit.Value = Npc(Editorindex).Stat(StatType.Spirit)
            .nudLuck.Value = Npc(Editorindex).Stat(StatType.Luck)
            .nudVitality.Value = Npc(Editorindex).Stat(StatType.Vitality)

            .cmbSkill1.Items.Clear()
            .cmbSkill2.Items.Clear()
            .cmbSkill3.Items.Clear()
            .cmbSkill4.Items.Clear()
            .cmbSkill5.Items.Clear()
            .cmbSkill6.Items.Clear()

            .cmbSkill1.Items.Add("None")
            .cmbSkill2.Items.Add("None")
            .cmbSkill3.Items.Add("None")
            .cmbSkill4.Items.Add("None")
            .cmbSkill5.Items.Add("None")
            .cmbSkill6.Items.Add("None")

            For i = 1 To MAX_SKILLS
                If Len(Skill(i).Name) > 0 Then
                    .cmbSkill1.Items.Add(Skill(i).Name)
                    .cmbSkill2.Items.Add(Skill(i).Name)
                    .cmbSkill3.Items.Add(Skill(i).Name)
                    .cmbSkill4.Items.Add(Skill(i).Name)
                    .cmbSkill5.Items.Add(Skill(i).Name)
                    .cmbSkill6.Items.Add(Skill(i).Name)
                End If
            Next

            .cmbSkill1.SelectedIndex = Npc(Editorindex).Skill(1)
            .cmbSkill2.SelectedIndex = Npc(Editorindex).Skill(2)
            .cmbSkill3.SelectedIndex = Npc(Editorindex).Skill(3)
            .cmbSkill4.SelectedIndex = Npc(Editorindex).Skill(4)
            .cmbSkill5.SelectedIndex = Npc(Editorindex).Skill(5)
            .cmbSkill6.SelectedIndex = Npc(Editorindex).Skill(6)
        End With

        EditorNpc_DrawSprite()
        NPC_Changed(Editorindex) = True
    End Sub

    Friend Sub NpcEditorOk()
        Dim i As Integer

        For i = 1 To MAX_NPCS
            If NPC_Changed(i) Then
                SendSaveNpc(i)
            End If
        Next

        frmNPC.Visible = False
        Editor = 0
        ClearChanged_NPC()
    End Sub

    Friend Sub NpcEditorCancel()
        Editor = 0
        frmNPC.Visible = False
        ClearChanged_NPC()
        ClearNpcs()
        SendRequestNPCS()
    End Sub

    Friend Sub ClearChanged_NPC()
        For i = 1 To MAX_NPCS
            NPC_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Resource Editor"

    Friend Sub ResourceEditorInit()
        Dim i As Integer

        If FrmResource.Visible = False Then Exit Sub
        Editorindex = FrmResource.lstIndex.SelectedIndex + 1

        With FrmResource
            'populate combo boxes
            .cmbRewardItem.Items.Clear()
            .cmbRewardItem.Items.Add("None")
            For i = 1 To MAX_ITEMS
                .cmbRewardItem.Items.Add(i & ": " & Item(i).Name)
            Next

            .cmbAnimation.Items.Clear()
            .cmbAnimation.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                .cmbAnimation.Items.Add(i & ": " & Animation(i).Name)
            Next

            .nudExhaustedPic.Maximum = NumResources
            .nudNormalPic.Maximum = NumResources
            .nudRespawn.Maximum = 1000000
            .txtName.Text = Trim$(Resource(Editorindex).Name)
            .txtMessage.Text = Trim$(Resource(Editorindex).SuccessMessage)
            .txtMessage2.Text = Trim$(Resource(Editorindex).EmptyMessage)
            .cmbType.SelectedIndex = Resource(Editorindex).ResourceType
            .nudNormalPic.Value = Resource(Editorindex).ResourceImage
            .nudExhaustedPic.Value = Resource(Editorindex).ExhaustedImage
            .cmbRewardItem.SelectedIndex = Resource(Editorindex).ItemReward
            .nudRewardExp.Value = Resource(Editorindex).ExpReward
            .cmbTool.SelectedIndex = Resource(Editorindex).ToolRequired
            .nudHealth.Value = Resource(Editorindex).Health
            .nudRespawn.Value = Resource(Editorindex).RespawnTime
            .cmbAnimation.SelectedIndex = Resource(Editorindex).Animation
            .nudLvlReq.Value = Resource(Editorindex).LvlRequired
        End With

        FrmResource.Visible = True

        EditorResource_DrawSprite()

        Resource_Changed(Editorindex) = True
    End Sub

    Friend Sub ResourceEditorOk()
        Dim i As Integer

        For i = 1 To MAX_RESOURCES
            If Resource_Changed(i) Then
                SendSaveResource(i)
            End If
        Next

        FrmResource.Visible = False
        Editor = 0
        ClearChanged_Resource()
    End Sub

    Friend Sub ResourceEditorCancel()
        Editor = 0
        FrmResource.Visible = False
        ClearChanged_Resource()
        ClearResources()
        SendRequestResources()
    End Sub

#End Region

#Region "Skill Editor"

    Friend Sub SkillEditorInit()
        Dim i As Integer

        If frmSkill.Visible = False Then Exit Sub
        Editorindex = frmSkill.lstIndex.SelectedIndex + 1

        If Skill(Editorindex).Name Is Nothing Then Skill(Editorindex).Name = ""

        With frmSkill
            ' set max values
            .nudAoE.Maximum = Byte.MaxValue
            .nudRange.Maximum = Byte.MaxValue
            .nudMap.Maximum = MAX_MAPS

            ' build class combo
            .cmbClass.Items.Clear()
            .cmbClass.Items.Add("None")
            For i = 1 To Max_Classes
                .cmbClass.Items.Add(Trim$(Classes(i).Name))
            Next
            .cmbClass.SelectedIndex = 0

            .cmbProjectile.Items.Clear()
            .cmbProjectile.Items.Add("None")
            For i = 1 To MAX_PROJECTILES
                .cmbProjectile.Items.Add(Trim$(Projectiles(i).Name))
            Next
            .cmbProjectile.SelectedIndex = 0

            .cmbAnimCast.Items.Clear()
            .cmbAnimCast.Items.Add("None")
            .cmbAnim.Items.Clear()
            .cmbAnim.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                .cmbAnimCast.Items.Add(Trim$(Animation(i).Name))
                .cmbAnim.Items.Add(Trim$(Animation(i).Name))
            Next
            .cmbAnimCast.SelectedIndex = 0
            .cmbAnim.SelectedIndex = 0

            ' set values
            .txtName.Text = Trim$(Skill(Editorindex).Name)
            .cmbType.SelectedIndex = Skill(Editorindex).Type
            .nudMp.Value = Skill(Editorindex).MpCost
            .nudLevel.Value = Skill(Editorindex).LevelReq
            .cmbAccessReq.SelectedIndex = Skill(Editorindex).AccessReq
            .cmbClass.SelectedIndex = Skill(Editorindex).ClassReq
            .nudCast.Value = Skill(Editorindex).CastTime
            .nudCool.Value = Skill(Editorindex).CdTime
            .nudIcon.Value = Skill(Editorindex).Icon
            .nudMap.Value = Skill(Editorindex).Map
            .nudX.Value = Skill(Editorindex).X
            .nudY.Value = Skill(Editorindex).Y
            .cmbDir.SelectedIndex = Skill(Editorindex).Dir
            .nudVital.Value = Skill(Editorindex).Vital
            .nudDuration.Value = Skill(Editorindex).Duration
            .nudInterval.Value = Skill(Editorindex).Interval
            .nudRange.Value = Skill(Editorindex).Range

            .chkAoE.Checked = Skill(Editorindex).IsAoE

            .nudAoE.Value = Skill(Editorindex).AoE
            .cmbAnimCast.SelectedIndex = Skill(Editorindex).CastAnim
            .cmbAnim.SelectedIndex = Skill(Editorindex).SkillAnim
            .nudStun.Value = Skill(Editorindex).StunDuration

            If Skill(Editorindex).IsProjectile = 1 Then
                .chkProjectile.Checked = True
            Else
                .chkProjectile.Checked = False
            End If
            .cmbProjectile.SelectedIndex = Skill(Editorindex).Projectile

            If Skill(Editorindex).KnockBack = 1 Then
                .chkKnockBack.Checked = True
            Else
                .chkKnockBack.Checked = False
            End If
            .cmbKnockBackTiles.SelectedIndex = Skill(Editorindex).KnockBackTiles
        End With

        EditorSkill_BltIcon()

        Skill_Changed(Editorindex) = True
    End Sub

    Friend Sub SkillEditorOk()
        Dim i As Integer

        For i = 1 To MAX_SKILLS
            If Skill_Changed(i) Then
                SendSaveSkill(i)
            End If
        Next

        frmSkill.Visible = False
        Editor = 0
        ClearChanged_Skill()
    End Sub

    Friend Sub SkillEditorCancel()
        Editor = 0
        frmSkill.Visible = False
        ClearChanged_Skill()
        ClearSkills()
        SendRequestSkills()
    End Sub

    Friend Sub ClearChanged_Skill()
        For i = 1 To MAX_SKILLS
            Skill_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Shop editor"

    Friend Sub ShopEditorInit()
        Dim i As Integer

        If FrmShop.Visible = False Then Exit Sub
        Editorindex = FrmShop.lstIndex.SelectedIndex + 1

        FrmShop.txtName.Text = Trim$(Shop(Editorindex).Name)
        If Shop(Editorindex).BuyRate > 0 Then
            FrmShop.nudBuy.Value = Shop(Editorindex).BuyRate
        Else
            FrmShop.nudBuy.Value = 100
        End If

        FrmShop.nudFace.Value = Shop(Editorindex).Face
        If File.Exists(Application.StartupPath & GFX_PATH & "Faces\" & Shop(Editorindex).Face & GFX_EXT) Then
            FrmShop.picFace.BackgroundImage = Image.FromFile(Application.StartupPath & GFX_PATH & "Faces\" & Shop(Editorindex).Face & GFX_EXT)
        End If

        FrmShop.cmbItem.Items.Clear()
        FrmShop.cmbItem.Items.Add("None")
        FrmShop.cmbCostItem.Items.Clear()
        FrmShop.cmbCostItem.Items.Add("None")

        For i = 1 To MAX_ITEMS
            FrmShop.cmbItem.Items.Add(i & ": " & Trim$(Item(i).Name))
            FrmShop.cmbCostItem.Items.Add(i & ": " & Trim$(Item(i).Name))
        Next

        FrmShop.cmbItem.SelectedIndex = 0
        FrmShop.cmbCostItem.SelectedIndex = 0

        UpdateShopTrade()

        Shop_Changed(Editorindex) = True
    End Sub

    Friend Sub UpdateShopTrade()
        Dim i As Integer
        FrmShop.lstTradeItem.Items.Clear()

        For i = 1 To MAX_TRADES
            With Shop(Editorindex).TradeItem(i)
                ' if none, show as none
                If .Item = 0 AndAlso .CostItem = 0 Then
                    FrmShop.lstTradeItem.Items.Add("Empty Trade Slot")
                Else
                    FrmShop.lstTradeItem.Items.Add(i & ": " & .ItemValue & "x " & Trim$(Item(.Item).Name) & " for " & .CostValue & "x " & Trim$(Item(.CostItem).Name))
                End If
            End With
        Next

        FrmShop.lstTradeItem.SelectedIndex = 0
    End Sub

    Friend Sub ShopEditorOk()
        Dim i As Integer

        For i = 1 To MAX_SHOPS
            If Shop_Changed(i) Then
                SendSaveShop(i)
            End If
        Next

        FrmShop.Visible = False
        Editor = 0
        ClearChanged_Shop()
    End Sub

    Friend Sub ShopEditorCancel()
        Editor = 0
        FrmShop.Visible = False
        ClearChanged_Shop()
        ClearShops()
        SendRequestShops()
    End Sub

    Friend Sub ClearChanged_Shop()
        For i = 1 To MAX_SHOPS
            Shop_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Class Editor"

    Friend Sub ClassesEditorOk()
        SendSaveClasses()

        frmClasses.Visible = False
        Editor = 0
    End Sub

    Friend Sub ClassesEditorCancel()
        SendRequestClasses()
        frmClasses.Visible = False
        Editor = 0
    End Sub

    Friend Sub ClassEditorInit()
        Dim i As Integer

        frmClasses.lstIndex.Items.Clear()

        For i = 1 To Max_Classes
            frmClasses.lstIndex.Items.Add(Trim(Classes(i).Name))
        Next

        Editor = EDITOR_CLASSES

        frmClasses.nudMaleSprite.Maximum = NumCharacters
        frmClasses.nudFemaleSprite.Maximum = NumCharacters

        frmClasses.cmbItems.Items.Clear()

        frmClasses.cmbItems.Items.Add("None")
        For i = 1 To MAX_ITEMS
            frmClasses.cmbItems.Items.Add(Trim(Item(i).Name))
        Next

        frmClasses.lstIndex.SelectedIndex = 0

        frmClasses.Visible = True
    End Sub

    Friend Sub LoadClass()
        Dim i As Integer

        If Editorindex <= 0 OrElse Editorindex > Max_Classes Then Exit Sub

        frmClasses.txtName.Text = Classes(Editorindex).Name
        frmClasses.txtDescription.Text = Classes(Editorindex).Desc

        frmClasses.cmbMaleSprite.Items.Clear()

        For i = 0 To UBound(Classes(Editorindex).MaleSprite)
            frmClasses.cmbMaleSprite.Items.Add("Sprite " & i + 1)
        Next

        frmClasses.cmbFemaleSprite.Items.Clear()

        For i = 0 To UBound(Classes(Editorindex).FemaleSprite)
            frmClasses.cmbFemaleSprite.Items.Add("Sprite " & i + 1)
        Next

        frmClasses.nudMaleSprite.Value = Classes(Editorindex).MaleSprite(0)
        frmClasses.nudFemaleSprite.Value = Classes(Editorindex).FemaleSprite(0)

        frmClasses.cmbMaleSprite.SelectedIndex = 0
        frmClasses.cmbFemaleSprite.SelectedIndex = 0

        frmClasses.DrawPreview()

        For i = 1 To StatType.Count - 1
            If Classes(Editorindex).Stat(i) = 0 Then Classes(Editorindex).Stat(i) = 1
        Next

        frmClasses.nudStrength.Value = Classes(Editorindex).Stat(StatType.Strength)
        frmClasses.nudLuck.Value = Classes(Editorindex).Stat(StatType.Luck)
        frmClasses.nudEndurance.Value = Classes(Editorindex).Stat(StatType.Endurance)
        frmClasses.nudIntelligence.Value = Classes(Editorindex).Stat(StatType.Intelligence)
        frmClasses.nudVitality.Value = Classes(Editorindex).Stat(StatType.Vitality)
        frmClasses.nudSpirit.Value = Classes(Editorindex).Stat(StatType.Spirit)

        If Classes(Editorindex).BaseExp < 10 Then
            frmClasses.nudBaseExp.Value = 10
        Else
            frmClasses.nudBaseExp.Value = Classes(Editorindex).BaseExp
        End If

        frmClasses.lstStartItems.Items.Clear()

        For i = 1 To 5
            If Classes(Editorindex).StartItem(i) > 0 Then
                frmClasses.lstStartItems.Items.Add(Item(Classes(Editorindex).StartItem(i)).Name & " X " & Classes(Editorindex).StartValue(i))
            Else
                frmClasses.lstStartItems.Items.Add("None")
            End If
        Next

        frmClasses.nudStartMap.Value = Classes(Editorindex).StartMap
        frmClasses.nudStartX.Value = Classes(Editorindex).StartX
        frmClasses.nudStartY.Value = Classes(Editorindex).StartY
    End Sub

#End Region

End Module