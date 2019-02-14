Module moddisabled


    Friend Sub DrawBars()
        Dim tmpY As Integer
        Dim tmpX As Integer
        Dim barWidth As Integer
        Dim rec(1) As Rectangle

        If GettingMap Then Return

        ' check for casting time bar
        If SkillBuffer > 0 Then
            ' lock to player
            tmpX = GetPlayerX(Myindex) * PicX + Player(Myindex).XOffset
            tmpY = GetPlayerY(Myindex) * PicY + Player(Myindex).YOffset + 35
            If Skill(PlayerSkills(SkillBuffer)).CastTime = 0 Then Skill(PlayerSkills(SkillBuffer)).CastTime = 1
            ' calculate the width to fill
            barWidth = ((GetTickCount() - SkillBufferTimer) / ((GetTickCount() - SkillBufferTimer) + (Skill(PlayerSkills(SkillBuffer)).CastTime * 1000)) * 64)
            ' draw bars
            rec(1) = New Rectangle(MapPositionX(tmpX), MapPositionY(tmpY), barWidth, 4)
            Dim rectShape As New RectangleShape(New Vector2(barWidth, 4)) With {
                .Position = New Vector2(MapPositionX(tmpX), MapPositionY(tmpY)),
                .FillColor = Microsoft.Xna.Framework.Color.Cyan
            }
            GameWindow.Draw(rectShape)
        End If

        If Settings.ShowNpcBar = 1 Then
            ' check for hp bar
            For i = 1 To MAX_MAP_NPCS
                If Map.Npc Is Nothing Then Return
                If Map.Npc(i) > 0 Then
                    If Npc(MapNpc(i).Num).Behaviour = NpcBehavior.AttackOnSight OrElse Npc(MapNpc(i).Num).Behaviour = NpcBehavior.AttackWhenAttacked OrElse Npc(MapNpc(i).Num).Behaviour = NpcBehavior.Guard Then
                        ' lock to npc
                        tmpX = MapNpc(i).X * PicX + MapNpc(i).XOffset
                        tmpY = MapNpc(i).Y * PicY + MapNpc(i).YOffset + 35
                        If MapNpc(i).Vital(VitalType.HP) > 0 Then
                            ' calculate the width to fill
                            barWidth = ((MapNpc(i).Vital(VitalType.HP) / (Npc(MapNpc(i).Num).Hp) * 32))
                            ' draw bars
                            rec(1) = New Rectangle(MapPositionX(tmpX), MapPositionY(tmpY), barWidth, 4)
                            Dim rectShape As New RectangleShape(New Vector2(barWidth, 4)) With {
                                .Position = New Vector2(MapPositionX(tmpX), MapPositionY(tmpY - 75)),
                                .FillColor = Microsoft.Xna.Framework.Color.Red
                            }
                            GameWindow.Draw(rectShape)

                            If MapNpc(i).Vital(VitalType.MP) > 0 Then
                                ' calculate the width to fill
                                barWidth = ((MapNpc(i).Vital(VitalType.MP) / (Npc(MapNpc(i).Num).Stat(StatType.Intelligence) * 2) * 32))
                                ' draw bars
                                rec(1) = New Rectangle(MapPositionX(tmpX), MapPositionY(tmpY), barWidth, 4)
                                Dim rectShape2 As New RectangleShape(New Vector2(barWidth, 4)) With {
                                    .Position = New Vector2(MapPositionX(tmpX), MapPositionY(tmpY - 80)),
                                    .FillColor = Microsoft.Xna.Framework.Color.Blue
                                }
                                GameWindow.Draw(rectShape2)
                            End If
                        End If
                    End If
                End If
            Next
        End If

        If PetAlive(Myindex) Then
            ' draw own health bar
            If Player(Myindex).Pet.Health > 0 AndAlso Player(Myindex).Pet.Health <= Player(Myindex).Pet.MaxHp Then
                'Debug.Print("pethealth:" & Player(Myindex).Pet.Health)
                ' lock to Player
                tmpX = Player(Myindex).Pet.X * PicX + Player(Myindex).Pet.XOffset
                tmpY = Player(Myindex).Pet.Y * PicX + Player(Myindex).Pet.YOffset + 35
                ' calculate the width to fill
                barWidth = ((Player(Myindex).Pet.Health) / (Player(Myindex).Pet.MaxHp)) * 32
                ' draw bars
                rec(1) = New Rectangle(MapPositionX(tmpX), MapPositionY(tmpY), barWidth, 4)
                Dim rectShape As New RectangleShape(New Vector2(barWidth, 4)) With {
                    .Position = New Vector2(MapPositionX(tmpX), MapPositionY(tmpY - 75)),
                    .FillColor = Microsoft.Xna.Framework.Color.Red
                }
                GameWindow.Draw(rectShape)
            End If
        End If
        ' check for pet casting time bar
        If PetSkillBuffer > 0 Then
            If Skill(Pet(Player(Myindex).Pet.Num).Skill(PetSkillBuffer)).CastTime > 0 Then
                ' lock to pet
                tmpX = Player(Myindex).Pet.X * PicX + Player(Myindex).Pet.XOffset
                tmpY = Player(Myindex).Pet.Y * PicY + Player(Myindex).Pet.YOffset + 35

                ' calculate the width to fill
                barWidth = (GetTickCount() - PetSkillBufferTimer) / ((Skill(Pet(Player(Myindex).Pet.Num).Skill(PetSkillBuffer)).CastTime * 1000)) * 64
                ' draw bar background
                rec(1) = New Rectangle(MapPositionX(tmpX), MapPositionY(tmpY), barWidth, 4)
                Dim rectShape As New RectangleShape(New Vector2(barWidth, 4)) With {
                    .Position = New Vector2(MapPositionX(tmpX), MapPositionY(tmpY)),
                    .FillColor = Microsoft.Xna.Framework.Color.Cyan
                }
                GameWindow.Draw(rectShape)
            End If
        End If
    End Sub

    Friend Sub DrawChatBubble(index As Integer)
        Dim theArray As List(Of String), x As Integer, y As Integer, i As Integer, maxWidth As Integer, x2 As Integer, y2 As Integer

        With ChatBubble(index)
            If .TargetType = TargetType.Player Then
                ' it's a player
                If GetPlayerMap(.Target) = GetPlayerMap(Myindex) Then
                    ' it's on our map - get co-ords
                    x = Graphics.MapPositionX((Player(.Target).X * 32) + Player(.Target).XOffset) + 16
                    y = Graphics.MapPositionY((Player(.Target).Y * 32) + Player(.Target).YOffset) - 40
                End If
            ElseIf .TargetType = TargetType.Npc Then
                ' it's on our map - get co-ords
                x = Graphics.MapPositionX((MapNpc(.Target).X * 32) + MapNpc(.Target).XOffset) + 16
                y = Graphics.MapPositionY((MapNpc(.Target).Y * 32) + MapNpc(.Target).YOffset) - 40
            ElseIf .TargetType = TargetType.Event Then
                x = Graphics.MapPositionX((Map.MapEvents(.Target).X * 32) + Map.MapEvents(.Target).XOffset) + 16
                y = Graphics.MapPositionY((Map.MapEvents(.Target).Y * 32) + Map.MapEvents(.Target).YOffset) - 40
            End If
            ' word wrap the text
            theArray = WordWrap(.Msg, ChatBubbleWidth, WrapMode.Font)
            ' find max width
            For i = 0 To theArray.Count - 1
                If GetTextWidth(theArray(i)) > maxWidth Then maxWidth = GetTextWidth(theArray(i))
            Next
            ' calculate the new position
            x2 = x - (maxWidth \ 2)
            y2 = y - (theArray.Count * 12)

            ' render bubble - top left
            Graphics.RenderTextures(ChatBubbleGfx, GameWindow, x2 - 9, y2 - 5, 0, 0, 9, 5, 9, 5)
            ' top right
            RenderTextures(ChatBubbleGfx, GameWindow, x2 + maxWidth, y2 - 5, 119, 0, 9, 5, 9, 5)
            ' top
            RenderTextures(ChatBubbleGfx, GameWindow, x2, y2 - 5, 10, 0, maxWidth, 5, 5, 5)
            ' bottom left
            RenderTextures(ChatBubbleGfx, GameWindow, x2 - 9, y, 0, 19, 9, 6, 9, 6)
            ' bottom right
            RenderTextures(ChatBubbleGfx, GameWindow, x2 + maxWidth, y, 119, 19, 9, 6, 9, 6)
            ' bottom - left half
            RenderTextures(ChatBubbleGfx, GameWindow, x2, y, 10, 19, (maxWidth \ 2) - 5, 6, 9, 6)
            ' bottom - right half
            RenderTextures(ChatBubbleGfx, GameWindow, x2 + (maxWidth \ 2) + 6, y, 10, 19, (maxWidth \ 2) - 5, 6, 9, 6)
            ' left
            RenderTextures(ChatBubbleGfx, GameWindow, x2 - 9, y2, 0, 6, 9, (theArray.Count * 12), 9, 1)
            ' right
            RenderTextures(ChatBubbleGfx, GameWindow, x2 + maxWidth, y2, 119, 6, 9, (theArray.Count * 12), 9, 1)
            ' center
            RenderTextures(ChatBubbleGfx, GameWindow, x2, y2, 9, 5, maxWidth, (theArray.Count * 12), 1, 1)
            ' little pointy bit
            RenderTextures(ChatBubbleGfx, GameWindow, x - 5, y, 58, 19, 11, 11, 11, 11)

            ' render each line centralised
            For i = 0 To theArray.Count - 1
                DrawText(x - (GetTextWidth(theArray(i)) / 2), y2, theArray(i), ToSfmlColor(Drawing.ColorTranslator.FromOle(QBColor(.Colour))), Color.Black, GameWindow)
                y2 = y2 + 12
            Next
            ' check if it's timed out - close it if so
            If .Timer + 5000 < GetTickCount() Then
                .Active = False
            End If
        End With

    End Sub



    Friend Sub EditorItem_DrawItem()
        Dim itemnum As Integer
        itemnum = frmEditor_Item.nudPic.Value

        If itemnum < 1 OrElse itemnum > NumItems Then
            frmEditor_Item.picItem.BackgroundImage = Nothing
            Return
        End If

        If File.Exists(Path.Graphics & "items\" & itemnum & GfxExt) Then
            frmEditor_Item.picItem.BackgroundImage = Drawing.Image.FromFile(Path.Graphics & "items\" & itemnum & GfxExt)
        End If

    End Sub

    Friend Sub EditorItem_DrawPaperdoll()
        Dim Sprite As Integer

        Sprite = frmEditor_Item.nudPaperdoll.Value

        If Sprite < 1 OrElse Sprite > NumPaperdolls Then
            frmEditor_Item.picPaperdoll.BackgroundImage = Nothing
            Return
        End If

        If File.Exists(Path.Graphics & "paperdolls\" & Sprite & GfxExt) Then
            frmEditor_Item.picPaperdoll.BackgroundImage = Drawing.Image.FromFile(Path.Graphics & "paperdolls\" & Sprite & GfxExt)
        End If
    End Sub

    Friend Sub EditorItem_DrawFurniture()
        Dim Furniturenum As Integer
        Dim sRECT As Rectangle
        Dim dRECT As Rectangle
        Furniturenum = frmEditor_Item.nudFurniture.Value

        If Furniturenum < 1 OrElse Furniturenum > NumFurniture Then
            EditorItem_Furniture.Clear(ToSfmlColor(frmEditor_Item.picFurniture.BackColor))
            EditorItem_Furniture.Display()
            Return
        End If

        If FurnitureGfxInfo(Furniturenum).IsLoaded = False Then
            LoadTexture(Furniturenum, 10)
        End If

        'seeying we still use it, lets update timer
        With FurnitureGfxInfo(Furniturenum)
            .TextureTimer = GetTickCount() + 100000
        End With

        ' rect for source
        With sRECT
            .Y = 0
            .Height = FurnitureGfxInfo(Furniturenum).Height
            .X = 0
            .Width = FurnitureGfxInfo(Furniturenum).Width
        End With

        ' same for destination as source
        dRECT = sRECT

        EditorItem_Furniture.Clear(ToSfmlColor(frmEditor_Item.picFurniture.BackColor))

        RenderSprite(FurnitureSprite(Furniturenum), EditorItem_Furniture, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)

        If frmEditor_Item.optSetBlocks.Checked = True Then
            For X = 0 To 3
                For Y = 0 To 3
                    If X <= (FurnitureGfxInfo(Furniturenum).Width / 32) - 1 Then
                        If Y <= (FurnitureGfxInfo(Furniturenum).Height / 32) - 1 Then
                            If Item(Editorindex).FurnitureBlocks(X, Y) = 1 Then
                                DrawText(X * 32 + 8, Y * 32 + 8, "X", Color.Red, Color.Black, EditorItem_Furniture)
                            Else
                                DrawText(X * 32 + 8, Y * 32 + 8, "O", Color.Blue, Color.Black, EditorItem_Furniture)
                            End If
                        End If
                    End If
                Next
            Next
        ElseIf frmEditor_Item.optSetFringe.Checked = True Then
            For X = 0 To 3
                For Y = 0 To 3
                    If X <= (FurnitureGfxInfo(Furniturenum).Width / 32) - 1 Then
                        If Y <= (FurnitureGfxInfo(Furniturenum).Height / 32) Then
                            If Item(Editorindex).FurnitureFringe(X, Y) = 1 Then
                                DrawText(X * 32 + 8, Y * 32 + 8, "O", Color.Blue, Color.Black, EditorItem_Furniture)
                            End If
                        End If
                    End If
                Next
            Next
        End If
        EditorItem_Furniture.Display()
    End Sub

    Friend Sub EditorNpc_DrawSprite()
        Dim Sprite As Integer

        Sprite = frmEditor_NPC.nudSprite.Value

        If Sprite < 1 OrElse Sprite > NumCharacters Then
            frmEditor_NPC.picSprite.BackgroundImage = Nothing
            Return
        End If

        If File.Exists(Path.Graphics & "characters\" & Sprite & GfxExt) Then
            frmEditor_NPC.picSprite.Width = Drawing.Image.FromFile(Path.Graphics & "characters\" & Sprite & GfxExt).Width / 4
            frmEditor_NPC.picSprite.Height = Drawing.Image.FromFile(Path.Graphics & "characters\" & Sprite & GfxExt).Height / 4
            frmEditor_NPC.picSprite.BackgroundImage = Drawing.Image.FromFile(Path.Graphics & "characters\" & Sprite & GfxExt)
        End If
    End Sub

    Friend Sub EditorResource_DrawSprite()
        Dim Sprite As Integer

        ' normal sprite
        Sprite = frmEditor_Resource.nudNormalPic.Value

        If Sprite < 1 OrElse Sprite > NumResources Then
            frmEditor_Resource.picNormalpic.BackgroundImage = Nothing
        Else
            If File.Exists(Path.Graphics & "resources\" & Sprite & GfxExt) Then
                frmEditor_Resource.picNormalpic.BackgroundImage = Drawing.Image.FromFile(Path.Graphics & "resources\" & Sprite & GfxExt)
            End If
        End If

        ' exhausted sprite
        Sprite = frmEditor_Resource.nudExhaustedPic.Value

        If Sprite < 1 OrElse Sprite > NumResources Then
            frmEditor_Resource.picExhaustedPic.BackgroundImage = Nothing
        Else
            If File.Exists(Path.Graphics & "resources\" & Sprite & GfxExt) Then
                frmEditor_Resource.picExhaustedPic.BackgroundImage = Drawing.Image.FromFile(Path.Graphics & "resources\" & Sprite & GfxExt)
            End If
        End If
    End Sub

    Friend Sub EditorSkill_BltIcon()
        Dim iconnum As Integer
        Dim sRECT As Rectangle
        Dim dRECT As Rectangle
        iconnum = frmEditor_Skill.nudIcon.Value

        If iconnum < 1 OrElse iconnum > NumSkillIcons Then
            EditorSkill_Icon.Clear(ToSfmlColor(frmEditor_Skill.picSprite.BackColor))
            EditorSkill_Icon.Display()
            Return
        End If

        If SkillIconsGfxInfo(iconnum).IsLoaded = False Then
            LoadTexture(iconnum, 9)
        End If

        'seeying we still use it, lets update timer
        With SkillIconsGfxInfo(iconnum)
            .TextureTimer = GetTickCount() + 100000
        End With

        With sRECT
            .Y = 0
            .Height = PicY
            .X = 0
            .Width = PicX
        End With

        'drect is the same, so just copy it
        dRECT = sRECT

        EditorSkill_Icon.Clear(ToSfmlColor(frmEditor_Skill.picSprite.BackColor))

        RenderSprite(SkillIconsSprite(iconnum), EditorSkill_Icon, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)

        EditorSkill_Icon.Display()
    End Sub

    Friend Sub EditorAnim_DrawAnim()
        Dim Animationnum As Integer
        Dim sRECT As Rectangle
        Dim dRECT As Rectangle
        Dim width As Integer, height As Integer
        Dim looptime As Integer
        Dim FrameCount As Integer
        Dim ShouldRender As Boolean

        Animationnum = FrmEditor_Animation.nudSprite0.Value

        If Animationnum < 1 OrElse Animationnum > NumAnimations Then
            EditorAnimation_Anim1.Clear(ToSfmlColor(FrmEditor_Animation.picSprite0.BackColor))
            EditorAnimation_Anim1.Display()
        Else
            If AnimationsGfxInfo(Animationnum).IsLoaded = False Then
                LoadTexture(Animationnum, 6)
            End If

            'seeying we still use it, lets update timer
            With AnimationsGfxInfo(Animationnum)
                .TextureTimer = GetTickCount() + 100000
            End With

            looptime = FrmEditor_Animation.nudLoopTime0.Value
            FrameCount = FrmEditor_Animation.nudFrameCount0.Value

            ShouldRender = False

            ' check if we need to render new frame
            If AnimEditorTimer(0) + looptime <= GetTickCount() Then
                ' check if out of range
                If AnimEditorFrame(0) >= FrameCount Then
                    AnimEditorFrame(0) = 1
                Else
                    AnimEditorFrame(0) = AnimEditorFrame(0) + 1
                End If
                AnimEditorTimer(0) = GetTickCount()
                ShouldRender = True
            End If

            If ShouldRender Then
                If FrmEditor_Animation.nudFrameCount0.Value > 0 Then
                    ' total width divided by frame count
                    height = AnimationsGfxInfo(Animationnum).Height
                    width = AnimationsGfxInfo(Animationnum).Width / FrmEditor_Animation.nudFrameCount0.Value

                    With sRECT
                        .Y = 0
                        .Height = height
                        .X = (AnimEditorFrame(0) - 1) * width
                        .Width = width
                    End With

                    With dRECT
                        .Y = 0
                        .Height = height
                        .X = 0
                        .Width = width
                    End With

                    EditorAnimation_Anim1.Clear(ToSfmlColor(FrmEditor_Animation.picSprite0.BackColor))

                    RenderSprite(AnimationsSprite(Animationnum), EditorAnimation_Anim1, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)

                    EditorAnimation_Anim1.Display()
                End If
            End If
        End If

        Animationnum = FrmEditor_Animation.nudSprite1.Value

        If Animationnum < 1 OrElse Animationnum > NumAnimations Then
            EditorAnimation_Anim2.Clear(ToSfmlColor(FrmEditor_Animation.picSprite1.BackColor))
            EditorAnimation_Anim2.Display()
        Else
            If AnimationsGfxInfo(Animationnum).IsLoaded = False Then
                LoadTexture(Animationnum, 6)
            End If

            'seeying we still use it, lets update timer
            With AnimationsGfxInfo(Animationnum)
                .TextureTimer = GetTickCount() + 100000
            End With

            looptime = FrmEditor_Animation.nudLoopTime1.Value
            FrameCount = FrmEditor_Animation.nudFrameCount1.Value

            ShouldRender = False

            ' check if we need to render new frame
            If AnimEditorTimer(1) + looptime <= GetTickCount() Then
                ' check if out of range
                If AnimEditorFrame(1) >= FrameCount Then
                    AnimEditorFrame(1) = 1
                Else
                    AnimEditorFrame(1) = AnimEditorFrame(1) + 1
                End If
                AnimEditorTimer(1) = GetTickCount()
                ShouldRender = True
            End If

            If ShouldRender Then
                If FrmEditor_Animation.nudFrameCount1.Value > 0 Then
                    ' total width divided by frame count
                    height = AnimationsGfxInfo(Animationnum).Height
                    width = AnimationsGfxInfo(Animationnum).Width / FrmEditor_Animation.nudFrameCount1.Value

                    With sRECT
                        .Y = 0
                        .Height = height
                        .X = (AnimEditorFrame(1) - 1) * width
                        .Width = width
                    End With

                    With dRECT
                        .Y = 0
                        .Height = height
                        .X = 0
                        .Width = width
                    End With

                    EditorAnimation_Anim2.Clear(ToSfmlColor(FrmEditor_Animation.picSprite1.BackColor))

                    RenderSprite(AnimationsSprite(Animationnum), EditorAnimation_Anim2, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)
                    EditorAnimation_Anim2.Display()

                End If
            End If
        End If
    End Sub


#Region " Unfiltered "

#Region "initialisation"

    Friend GameWindow As RenderWindow
    Friend TilesetWindow As RenderWindow

    Friend EditorItem_Furniture As RenderWindow
    Friend EditorSkill_Icon As RenderWindow
    Friend EditorAnimation_Anim1 As RenderWindow
    Friend EditorAnimation_Anim2 As RenderWindow

    Friend MapTintGfx As New RenderTexture(1152, 864)
    Friend MapTintSprite As Sprite
    Friend MapFadeSprite As Sprite
    Friend NightGfx As New RenderTexture(1152, 864)
    Friend NightSprite As Sprite

    Sub InitGraphics()

        GameWindow = New RenderWindow(FrmGame.picscreen.Handle)
        TilesetWindow = New RenderWindow(FrmEditor_MapEditor.picBackSelect.Handle)

        EditorItem_Furniture = New RenderWindow(frmEditor_Item.picFurniture.Handle)
        EditorSkill_Icon = New RenderWindow(frmEditor_Skill.picSprite.Handle)
        EditorAnimation_Anim1 = New RenderWindow(FrmEditor_Animation.picSprite0.Handle)
        EditorAnimation_Anim2 = New RenderWindow(FrmEditor_Animation.picSprite1.Handle)

    End Sub

#End Region

    Sub DrawChat()
        Dim i As Integer, x As Integer, y As Integer
        Dim text As String

        'first draw back image
        RenderTexture(PanelChatBox, ChatPosition)

        y = 5
        x = 5

        FirstLineindex = (Chat.Count - MaxChatDisplayLines) - ScrollMod 'First element is the 5th from the last in the list
        If FirstLineindex < 0 Then FirstLineindex = 0 'if the list has less than 5 elements, the first is the 0th index or first element

        LastLineindex = (FirstLineindex + MaxChatDisplayLines) ' - ScrollMod
        If (LastLineindex >= Chat.Count) Then LastLineindex = Chat.Count - 1  'Based off of index 0, so the last element should be Chat.Count -1

        'only loop tru last entries
        For i = FirstLineindex To LastLineindex
            text = Chat(i).Text

            If text <> "" Then ' or not
                RenderText(text, Font_Default, ChatPosition + New Vector2f(x, y), GetSfmlColor(Chat(i).Color))
                y = y + ChatLineSpacing + 1
            End If

        Next

        'My Text
        'first draw back image
        RenderTexture(PanelChatInput, ChatInputPosition + New Vector2f(0, -5))

        If Len(ChatInput.CurrentMessage) > 0 Then
            Dim subText As String = ChatInput.CurrentMessage
            While GetTextWidth(subText) > PanelChatInput.Size.X - ChatEntryPadding
                subText = subText.Substring(1)
            End While
            RenderText(subText, Font_Default, ChatInputPosition + New Vector2f(5, -3))
        End If
    End Sub

    Friend Sub DrawNpc(mapNpcNum As Integer)
        Dim anim As Byte
        Dim x As Integer
        Dim y As Integer
        Dim sprite As Integer, spriteleft As Integer
        Dim destrec As FloatRect
        Dim srcrec As FloatRect
        Dim attackspeed As Integer

        If MapNpc(mapNpcNum).Num = 0 Then Return ' no npc set

        If MapNpc(mapNpcNum).X < Viewport.Left OrElse MapNpc(mapNpcNum).X > Viewport.Right Then Return
        If MapNpc(mapNpcNum).Y < Viewport.Top OrElse MapNpc(mapNpcNum).Y > Viewport.Bottom Then Return

        sprite = Npc(MapNpc(mapNpcNum).Num).Sprite

        If sprite < 1 OrElse sprite > UBound(Npcs) Then Return

        attackspeed = 1000

        ' Reset frame
        anim = 0

        ' Check for attacking animation
        If MapNpc(mapNpcNum).AttackTimer + (attackspeed / 2) > GetTickCount() Then
            If MapNpc(mapNpcNum).Attacking = 1 Then
                anim = 3
            End If
        Else
            ' If not attacking, walk normally
            Select Case MapNpc(mapNpcNum).Dir
                Case DirectionType.Up
                    If (MapNpc(mapNpcNum).YOffset > 8) Then anim = MapNpc(mapNpcNum).Steps
                Case DirectionType.Down
                    If (MapNpc(mapNpcNum).YOffset < -8) Then anim = MapNpc(mapNpcNum).Steps
                Case DirectionType.Left
                    If (MapNpc(mapNpcNum).XOffset > 8) Then anim = MapNpc(mapNpcNum).Steps
                Case DirectionType.Right
                    If (MapNpc(mapNpcNum).XOffset < -8) Then anim = MapNpc(mapNpcNum).Steps
            End Select
        End If

        ' Check to see if we want to stop making him attack
        With MapNpc(mapNpcNum)
            If .AttackTimer + attackspeed < GetTickCount() Then
                .Attacking = 0
                .AttackTimer = 0
            End If
        End With

        ' Set the left
        Select Case MapNpc(mapNpcNum).Dir
            Case DirectionType.Up
                spriteleft = 3
            Case DirectionType.Right
                spriteleft = 2
            Case DirectionType.Down
                spriteleft = 0
            Case DirectionType.Left
                spriteleft = 1
        End Select

        srcrec = New FloatRect((anim) * (Npcs(sprite).Size.X / 4), spriteleft * (Npcs(sprite).Size.Y / 4), (Npcs(sprite).Size.X / 4), (Npcs(sprite).Size.Y / 4))

        ' Calculate the X
        x = MapNpc(mapNpcNum).X * PicX + MapNpc(mapNpcNum).XOffset - ((Npcs(sprite).Size.X / 4 - 32) / 2)

        ' Is the player's height more than 32..?
        If (Npcs(sprite).Size.Y / 4) > 32 Then
            ' Create a 32 pixel offset for larger sprites
            y = MapNpc(mapNpcNum).Y * PicY + MapNpc(mapNpcNum).YOffset - ((Npcs(sprite).Size.Y / 4) - 32)
        Else
            ' Proceed as normal
            y = MapNpc(mapNpcNum).Y * PicY + MapNpc(mapNpcNum).YOffset
        End If

        destrec = New FloatRect(x, y, Npcs(sprite).Size.X / 4, Npcs(sprite).Size.Y / 4)

        RenderTexture(Npcs(sprite), srcrec, New Vector2f(x, y))

        If Npc(MapNpc(mapNpcNum).Num).Behaviour = NpcBehavior.Quest Then
            If CanStartQuest(Npc(MapNpc(mapNpcNum).Num).QuestNum) Then
                If Player(Myindex).PlayerQuest(Npc(MapNpc(mapNpcNum).Num).QuestNum).Status = QuestStatusType.NotStarted Then
                    DrawEmotes(5, New Vector2f(x, y))
                End If
            ElseIf Player(Myindex).PlayerQuest(Npc(MapNpc(mapNpcNum).Num).QuestNum).Status = QuestStatusType.Started Then
                DrawEmotes(9, New Vector2f(x, y))
            End If
        End If

    End Sub

    Friend Sub DrawFurnitureOutline()
        Dim size = New Vector2f(Item(GetPlayerInvItemNum(Myindex, FurnitureSelected)).FurnitureWidth * PicX,
                Item(GetPlayerInvItemNum(Myindex, FurnitureSelected)).FurnitureHeight * PicY)
        RenderRectangle(MapPosition(CurPos), size, 0.6F, Color.Blue)
    End Sub

    Friend Sub DrawMapTint()
        If Map.HasMapTint = 0 Then Return

        MapTintGfx.Clear(New Color(CurrentTintR, CurrentTintG, CurrentTintB, CurrentTintA))

        MapTintSprite = New Sprite(MapTintGfx.Texture) With {
            .TextureRect = New IntRect(0, 0, GameWindow.Size.X, GameWindow.Size.Y),
            .Position = New Vector2f(0, 0)
        }

        MapTintGfx.Display()
        RenderDrawable(MapTintSprite)

    End Sub

    Friend Sub DrawMapFade()
        If UseFade = False Then Return

        MapFadeSprite = New Sprite(New Texture(New Image(GameWindow.Size.X, GameWindow.Size.Y, Color.Black))) With {
            .Color = New Color(0, 0, 0, FadeAmount),
            .TextureRect = New IntRect(0, 0, GameWindow.Size.X, GameWindow.Size.Y),
            .Position = New Vector2f(0, 0)
        }

        GameWindow.Draw(MapFadeSprite)

    End Sub

    Friend Sub DrawDialogPanel()
        'first render panel
        RenderSprite(EventChatSprite, GameWindow, DialogPanelX, DialogPanelY, 0, 0, EventChatGfxInfo.Width, EventChatGfxInfo.Height)

        DrawText(DialogPanelX + 175, DialogPanelY + 10, Trim(DialogMsg1), Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)

        If Len(DialogMsg2) > 0 Then
            DrawText(DialogPanelX + 60, DialogPanelY + 30, Trim(DialogMsg2), Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
        End If

        If Len(DialogMsg3) > 0 Then
            DrawText(DialogPanelX + 60, DialogPanelY + 50, Trim(DialogMsg3), Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
        End If

        If DialogType = DialogueTypeQuest Then
            If QuestAcceptTag > 0 Then
                'render accept button
                DrawButton(DialogButton1Text, DialogPanelX + OkButtonX, DialogPanelY + OkButtonY, 0)
                DrawButton(DialogButton2Text, DialogPanelX + CancelButtonX, DialogPanelY + CancelButtonY, 0)
            Else
                'render cancel button
                DrawButton(DialogButton2Text, DialogPanelX + CancelButtonX - 140, DialogPanelY + CancelButtonY, 0)
            End If
        Else
            'render ok button
            DrawButton(DialogButton1Text, DialogPanelX + OkButtonX, DialogPanelY + OkButtonY, 0)

            'render cancel button
            DrawButton(DialogButton2Text, DialogPanelX + CancelButtonX, DialogPanelY + CancelButtonY, 0)
        End If

    End Sub

    Sub DrawActionMsg(index As Integer)
        Dim x As Integer, y As Integer, i As Integer, time As Integer

        ' how long we want each message to appear
        Select Case ActionMsg(index).Type
            Case ActionMsgType.Static
                time = 1500

                If ActionMsg(index).Y > 0 Then
                    x = ActionMsg(index).X + Int(PicX \ 2) - ((Len(Trim$(ActionMsg(index).Message)) \ 2) * 8)
                    y = ActionMsg(index).Y - Int(PicY \ 2) - 2
                Else
                    x = ActionMsg(index).X + Int(PicX \ 2) - ((Len(Trim$(ActionMsg(index).Message)) \ 2) * 8)
                    y = ActionMsg(index).Y - Int(PicY \ 2) + 18
                End If

            Case ActionMsgType.Scroll
                time = 1500

                If ActionMsg(index).Y > 0 Then
                    x = ActionMsg(index).X + Int(PicX \ 2) - ((Len(Trim$(ActionMsg(index).Message)) \ 2) * 8)
                    y = ActionMsg(index).Y - Int(PicY \ 2) - 2 - (ActionMsg(index).Scroll * 0.6)
                    ActionMsg(index).Scroll = ActionMsg(index).Scroll + 1
                Else
                    x = ActionMsg(index).X + Int(PicX \ 2) - ((Len(Trim$(ActionMsg(index).Message)) \ 2) * 8)
                    y = ActionMsg(index).Y - Int(PicY \ 2) + 18 + (ActionMsg(index).Scroll * 0.6)
                    ActionMsg(index).Scroll = ActionMsg(index).Scroll + 1
                End If

            Case ActionMsgType.Screen
                time = 3000

                ' This will kill any action screen messages that there in the system
                For i = Byte.MaxValue To 1 Step -1
                    If ActionMsg(i).Type = ActionMsgType.Screen Then
                        If i <> index Then
                            ClearActionMsg(index)
                            index = i
                        End If
                    End If
                Next
                x = (FrmGame.picscreen.Width \ 2) - ((Len(Trim$(ActionMsg(index).Message)) \ 2) * 8)
                y = 425

        End Select

        x = Graphics.MapPositionX(x)
        y = Graphics.MapPositionY(y)

        If GetTickCount() < ActionMsg(index).Created + time Then
            DrawText(x, y, ActionMsg(index).Message, GetSfmlColor(ActionMsg(index).Color), (Color.Black), GameWindow)
        Else
            ClearActionMsg(index)
        End If

    End Sub



    Sub DrawNight()
        Dim x As Integer, y As Integer

        If Map.Moral = MapMoralType.Indoors Then Return

        Select Case Time.Instance.TimeOfDay
            Case TimeOfDay.Dawn
                NightGfx.Clear(New Color(0, 0, 0, 100))
                Exit Select

            Case TimeOfDay.Dusk
                NightGfx.Clear(New Color(0, 0, 0, 150))
                Exit Select

            Case TimeOfDay.Night
                NightGfx.Clear(New Color(0, 0, 0, 200))
                Exit Select

            Case Else
                Return
        End Select

        For x = Viewport.Left To Viewport.Right + 1
            For y = Viewport.Top To Viewport.Bottom + 1
                If IsValidMapPoint(x, y) Then
                    If Map.Tile(x, y).Type = TileType.Light Then
                        Dim x1 = MapPositionX(x * 32) + 16 - Light.Size.X / 2
                        Dim y1 = MapPositionY(y * 32) + 16 - Light.Size.Y / 2

                        'Create the light texture to multiply over the dark texture.
                        Dim LightSprite As New Sprite(Light)
                        LightSprite.Position = New Vector2f(x1, y1)
                        LightSprite.Color = Color.Red
                        NightGfx.Draw(LightSprite, New RenderStates(BlendMode.Multiply))

                    End If
                End If
            Next
        Next

        NightSprite = New Sprite(NightGfx.Texture)

        NightGfx.Display()
        RenderDrawable(NightSprite)
    End Sub


    Friend Sub DrawPlayer(index As Integer)
        Dim anim As Byte, x As Integer, y As Integer
        Dim spritenum As Integer, spriteleft As Integer
        Dim attackspeed As Integer, attackSprite As Byte
        Dim srcrec As Rectangle

        spritenum = GetPlayerSprite(index)

        attackSprite = 0

        If spritenum < 1 OrElse spritenum > NumCharacters Then Return

        ' speed from weapon
        If GetPlayerEquipment(index, EquipmentType.Weapon) > 0 Then
            attackspeed = Item(GetPlayerEquipment(index, EquipmentType.Weapon)).Speed
        Else
            attackspeed = 1000
        End If

        ' Reset frame
        anim = 0

        ' Check for attacking animation
        If Player(index).AttackTimer + (attackspeed / 2) > GetTickCount() Then
            If Player(index).Attacking = 1 Then
                If attackSprite = 1 Then
                    anim = 4
                Else
                    anim = 3
                End If
            End If
        Else
            ' If not attacking, walk normally
            Select Case GetPlayerDir(index)
                Case DirectionType.Up

                    If (Player(index).YOffset > 8) Then anim = Player(index).Steps
                Case DirectionType.Down

                    If (Player(index).YOffset < -8) Then anim = Player(index).Steps
                Case DirectionType.Left

                    If (Player(index).XOffset > 8) Then anim = Player(index).Steps
                Case DirectionType.Right

                    If (Player(index).XOffset < -8) Then anim = Player(index).Steps
            End Select

        End If

        ' Check to see if we want to stop making him attack
        With Player(index)
            If .AttackTimer + attackspeed < GetTickCount() Then
                .Attacking = 0
                .AttackTimer = 0
            End If

        End With

        ' Set the left
        Select Case GetPlayerDir(index)
            Case DirectionType.Up
                spriteleft = 3
            Case DirectionType.Right
                spriteleft = 2
            Case DirectionType.Down
                spriteleft = 0
            Case DirectionType.Left
                spriteleft = 1
        End Select

        If attackSprite = 1 Then
            srcrec = New Rectangle((anim) * (Graphics.Characters(spritenum).Size.X / 5), spriteleft * (Graphics.Characters(spritenum).Size.Y / 4), (Graphics.Characters(spritenum).Size.X / 5), (Graphics.Characters(spritenum).Size.Y / 4))
        Else
            srcrec = New Rectangle((anim) * (Graphics.Characters(spritenum).Size.X / 4), spriteleft * (Graphics.Characters(spritenum).Size.Y / 4), (Graphics.Characters(spritenum).Size.X / 4), (Graphics.Characters(spritenum).Size.Y / 4))
        End If

        ' Calculate the X
        If attackSprite = 1 Then
            x = GetPlayerX(index) * PicX + Player(index).XOffset - ((Graphics.Characters(spritenum).Size.X / 5 - 32) / 2)
        Else
            x = GetPlayerX(index) * PicX + Player(index).XOffset - ((Graphics.Characters(spritenum).Size.X / 4 - 32) / 2)
        End If

        ' Is the player's height more than 32..?
        If (Graphics.Characters(spritenum).Size.Y) > 32 Then
            ' Create a 32 pixel offset for larger sprites
            y = GetPlayerY(index) * PicY + Player(index).YOffset - ((Graphics.Characters(spritenum).Size.Y / 4) - 32)
        Else
            ' Proceed as normal
            y = GetPlayerY(index) * PicY + Player(index).YOffset
        End If

        ' render the actual sprite
        Graphics.DrawCharacter(spritenum, x, y, srcrec)

        'check for paperdolling
        For i = 1 To EquipmentType.Count - 1
            If GetPlayerEquipment(index, i) > 0 Then
                If Item(GetPlayerEquipment(index, i)).Paperdoll > 0 Then
                    DrawPaperdoll(x, y, Item(GetPlayerEquipment(index, i)).Paperdoll, anim, spriteleft)
                End If
            End If
        Next

        ' Check to see if we want to stop showing emote
        With Player(index)
            If .EmoteTimer < GetTickCount() Then
                .Emote = 0
                .EmoteTimer = 0
            End If
        End With

        'check for emotes
        'Player(Index).Emote = 4
        If Player(index).Emote > 0 Then
            DrawEmotes(x, y, Player(index).Emote)
        End If
    End Sub

    Friend Sub DrawPlayerName(index As Integer)
        Dim textX As Integer
        Dim textY As Integer
        Dim color As SFML.Graphics.Color, backcolor As SFML.Graphics.Color
        Dim name As String

        ' Check access level
        If GetPlayerPk(index) = False Then

            Select Case GetPlayerAccess(index)
                Case AdminType.Player
                    color = SFML.Graphics.Color.Red
                    backcolor = SFML.Graphics.Color.Black
                Case AdminType.Monitor
                    color = SFML.Graphics.Color.Black
                    backcolor = SFML.Graphics.Color.White
                Case AdminType.Mapper
                    color = SFML.Graphics.Color.Cyan
                    backcolor = SFML.Graphics.Color.Black
                Case AdminType.Developer
                    color = SFML.Graphics.Color.Green
                    backcolor = SFML.Graphics.Color.Black
                Case AdminType.Creator
                    color = SFML.Graphics.Color.Yellow
                    backcolor = SFML.Graphics.Color.Black
            End Select
        Else
            color = SFML.Graphics.Color.Red
        End If

        name = Trim$(Player(index).Name)
        ' calc pos
        textX = ConvertMapX(GetPlayerX(index) * PicX) + Player(index).XOffset + (PicX \ 2)
        textX = textX - (GetTextWidth((Trim$(name))) / 2)
        If GetPlayerSprite(index) < 1 OrElse GetPlayerSprite(index) > NumCharacters Then
            textY = ConvertMapY(GetPlayerY(index) * PicY) + Player(index).YOffset - 16
        Else
            ' Determine location for text
            textY = ConvertMapY(GetPlayerY(index) * PicY) + Player(index).YOffset - (Graphics.Characters(GetPlayerSprite(index)).Height / 4) + 16
        End If

        ' Draw name
        DrawText(textX, textY, Trim$(name), color, backcolor, GameWindow)
    End Sub

    Sub DrawEquipment()
        Dim i As Integer, itemnum As Integer, itempic As Integer, tmprarity As Byte
        Dim rec As Rectangle, recPos As Rectangle, playersprite As Integer
        Dim tmpSprite2 As Sprite = New Sprite(CharPanelGfx)
        Dim tempRarityColor As SFML.Graphics.Color

        If NumItems = 0 Then Return

        'first render panel
        RenderSprite(CharPanelSprite, GameWindow, CharWindowX, CharWindowY, 0, 0, CharPanelGfxInfo.Width, CharPanelGfxInfo.Height)

        'lets get player sprite to render
        playersprite = GetPlayerSprite(Myindex)

        With rec
            .Y = 0
            .Height = Graphics.Characters(playersprite).Height / 4
            .X = 0
            .Width = Graphics.Characters(playersprite).Width / 4
        End With

        RenderSprite(CharacterSprite(playersprite), GameWindow, CharWindowX + CharPanelGfxInfo.Width / 4 - rec.Width / 2, CharWindowY + CharPanelGfxInfo.Height / 2 - rec.Height / 2, rec.X, rec.Y, rec.Width, rec.Height)

        For i = 1 To EquipmentType.Count - 1
            itemnum = GetPlayerEquipment(Myindex, i)

            If itemnum > 0 Then

                itempic = Item(itemnum).Pic

                If ItemsGfxInfo(itempic).IsLoaded = False Then
                    LoadTexture(itempic, 4)
                End If

                'seeying we still use it, lets update timer
                With ItemsGfxInfo(itempic)
                    .TextureTimer = GetTickCount() + 100000
                End With

                With rec
                    .Y = 0
                    .Height = 32
                    .X = 0
                    .Width = 32
                End With

                With recPos
                    .Y = CharWindowY + EqTop + ((EqOffsetY + 32) * ((i - 1) \ EqColumns))
                    .Height = PicY
                    .X = CharWindowX + EqLeft + 1 + ((EqOffsetX + 32 - 2) * (((i - 1) Mod EqColumns)))
                    .Width = PicX
                End With

                ItemsSprite(itempic).TextureRect = New IntRect(rec.X, rec.Y, rec.Width, rec.Height)
                ItemsSprite(itempic).Position = New Vector2f(recPos.X, recPos.Y)
                GameWindow.Draw(ItemsSprite(itempic))

                ' set the name
                If Item(itemnum).Randomize <> 0 Then
                    tmprarity = Player(Myindex).RandEquip(i).Rarity
                Else
                    tmprarity = Item(itemnum).Rarity
                End If

                Select Case tmprarity
                    Case 0 ' White
                        tempRarityColor = ItemRarityColor0
                    Case 1 ' green
                        tempRarityColor = ItemRarityColor1
                    Case 2 ' blue
                        tempRarityColor = ItemRarityColor2
                    Case 3 ' maroon
                        tempRarityColor = ItemRarityColor3
                    Case 4 ' purple
                        tempRarityColor = ItemRarityColor4
                    Case 5 'gold
                        tempRarityColor = ItemRarityColor5
                End Select

                Dim rec2 As New RectangleShape With {
                    .OutlineColor = New SFML.Graphics.Color(tempRarityColor),
                    .OutlineThickness = 2,
                    .FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent),
                    .Size = New Vector2f(30, 30),
                    .Position = New Vector2f(recPos.X, recPos.Y)
                }
                GameWindow.Draw(rec2)
            End If

        Next

        ' Set the character windows
        'name
        DrawText(CharWindowX + 10, CharWindowY + 14, Language.Character.Name & GetPlayerName(Myindex), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'class
        DrawText(CharWindowX + 10, CharWindowY + 33, Language.Character.ClassType & Trim(Classes(GetPlayerClass(Myindex)).Name), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'level
        DrawText(CharWindowX + 150, CharWindowY + 14, Language.Character.Level & GetPlayerLevel(Myindex), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'points
        DrawText(CharWindowX + 6, CharWindowY + 200, Language.Character.Points & GetPlayerPoints(Myindex), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        'Header
        DrawText(CharWindowX + 250, CharWindowY + 14, Language.Character.StatsLabel, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        'strength stat
        DrawText(CharWindowX + 210, CharWindowY + 30, Language.Character.Strength & GetPlayerStat(Myindex, StatType.Strength), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 11)
        'endurance stat
        DrawText(CharWindowX + 210, CharWindowY + 50, Language.Character.Endurance & GetPlayerStat(Myindex, StatType.Endurance), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 11)
        'vitality stat
        DrawText(CharWindowX + 210, CharWindowY + 70, Language.Character.Vitality & GetPlayerStat(Myindex, StatType.Vitality), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 11)
        'intelligence stat
        DrawText(CharWindowX + 210, CharWindowY + 90, Language.Character.Intelligence & GetPlayerStat(Myindex, StatType.Intelligence), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 11)
        'luck stat
        DrawText(CharWindowX + 210, CharWindowY + 110, Language.Character.Luck & GetPlayerStat(Myindex, StatType.Luck), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 11)
        'spirit stat
        DrawText(CharWindowX + 210, CharWindowY + 130, Language.Character.Spirit & GetPlayerStat(Myindex, StatType.Spirit), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 11)

        If GetPlayerPoints(Myindex) > 0 Then
            'strength upgrade
            RenderSprite(CharPanelPlusSprite, GameWindow, CharWindowX + StrengthUpgradeX, CharWindowY + StrengthUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
            'endurance upgrade
            RenderSprite(CharPanelPlusSprite, GameWindow, CharWindowX + EnduranceUpgradeX, CharWindowY + EnduranceUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
            'vitality upgrade
            RenderSprite(CharPanelPlusSprite, GameWindow, CharWindowX + VitalityUpgradeX, CharWindowY + VitalityUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
            'intelligence upgrade
            RenderSprite(CharPanelPlusSprite, GameWindow, CharWindowX + IntellectUpgradeX, CharWindowY + IntellectUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
            'willpower upgrade
            RenderSprite(CharPanelPlusSprite, GameWindow, CharWindowX + LuckUpgradeX, CharWindowY + LuckUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
            'spirit upgrade
            RenderSprite(CharPanelPlusSprite, GameWindow, CharWindowX + SpiritUpgradeX, CharWindowY + SpiritUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
        End If

        'gather skills
        'Header
        DrawText(CharWindowX + 250, CharWindowY + 145, Language.Character.SkillLabel, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'herbalist skill
        DrawText(CharWindowX + 210, CharWindowY + 164, String.Format(Language.Character.Herbalist & GetPlayerGatherSkillLvl(Myindex, ResourceSkills.Herbalist)) & Language.Character.Exp & GetPlayerGatherSkillExp(Myindex, ResourceSkills.Herbalist) & "/" & GetPlayerGatherSkillMaxExp(Myindex, ResourceSkills.Herbalist), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 11)
        'woodcutter
        DrawText(CharWindowX + 210, CharWindowY + 184, String.Format(Language.Character.Woodcutter & GetPlayerGatherSkillLvl(Myindex, ResourceSkills.WoodCutter)) & Language.Character.Exp & GetPlayerGatherSkillExp(Myindex, ResourceSkills.WoodCutter) & "/" & GetPlayerGatherSkillMaxExp(Myindex, ResourceSkills.WoodCutter), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 11)
        'miner
        DrawText(CharWindowX + 210, CharWindowY + 204, String.Format(Language.Character.Miner & GetPlayerGatherSkillLvl(Myindex, ResourceSkills.Miner)) & Language.Character.Exp & GetPlayerGatherSkillExp(Myindex, ResourceSkills.Miner) & "/" & GetPlayerGatherSkillMaxExp(Myindex, ResourceSkills.Miner), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 11)
        'fisherman
        DrawText(CharWindowX + 210, CharWindowY + 224, String.Format(Language.Character.Fisherman & GetPlayerGatherSkillLvl(Myindex, ResourceSkills.Fisherman)) & Language.Character.Exp & GetPlayerGatherSkillExp(Myindex, ResourceSkills.Fisherman) & "/" & GetPlayerGatherSkillMaxExp(Myindex, ResourceSkills.Fisherman), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 11)
    End Sub


    Friend Sub CreateActionMsg(message As String, color As Integer, msgType As Byte, x As Integer, y As Integer)

        ActionMsgIndex = ActionMsgIndex + 1
        If ActionMsgIndex >= Byte.MaxValue Then ActionMsgIndex = 1

        With ActionMsg(ActionMsgIndex)
            .Message = message
            .Color = color
            .Type = msgType
            .Created = GetTickCount()
            .Scroll = 1
            .X = x
            .Y = y
        End With

        If ActionMsg(ActionMsgIndex).Type = ActionMsgType.Scroll Then
            ActionMsg(ActionMsgIndex).Y = ActionMsg(ActionMsgIndex).Y + Rand(-2, 6)
            ActionMsg(ActionMsgIndex).X = ActionMsg(ActionMsgIndex).X + Rand(-8, 8)
        End If

    End Sub

    Friend Sub ClearActionMsg(index As Byte)
        ActionMsg(index).Message = ""
        ActionMsg(index).Created = 0
        ActionMsg(index).Type = 0
        ActionMsg(index).Color = 0
        ActionMsg(index).Scroll = 0
        ActionMsg(index).X = 0
        ActionMsg(index).Y = 0
    End Sub
#End Region
End Module