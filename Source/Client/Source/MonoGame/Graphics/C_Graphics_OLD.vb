Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports System.IO

Namespace Graphics
    Friend Module modRenderer


#Region "Declarations"

        Friend GameWindow As RenderWindow
        Friend TilesetWindow As RenderWindow

        Friend EditorItem_Furniture As RenderWindow
        Friend EditorSkill_Icon As RenderWindow
        Friend EditorAnimation_Anim1 As RenderWindow
        Friend EditorAnimation_Anim2 As RenderWindow

        Friend SfmlGameFont As SFML.Graphics.Font


        Friend MapTintGfx As New RenderTexture(1152, 864)
        Friend MapTintSprite As Sprite
        Friend MapFadeSprite As Sprite
        Friend NightGfx As New RenderTexture(1152, 864)
        Friend NightSprite As Sprite
        Friend NightGfxInfo As GraphicInfo
        Friend LightGfx As Texture
        Friend LightSprite As Sprite
        Friend LightGfxInfo As GraphicInfo
        Friend ShadowGfx As Texture
        Friend ShadowSprite As Sprite
        Friend ShadowGfxInfo As GraphicInfo

#End Region

#Region "initialisation"

        Sub InitGraphics()

            GameWindow = New RenderWindow(FrmGame.picscreen.Handle)
            TilesetWindow = New RenderWindow(FrmEditor_MapEditor.picBackSelect.Handle)

            EditorItem_Furniture = New RenderWindow(frmEditor_Item.picFurniture.Handle)
            EditorSkill_Icon = New RenderWindow(frmEditor_Skill.picSprite.Handle)
            EditorAnimation_Anim1 = New RenderWindow(FrmEditor_Animation.picSprite0.Handle)
            EditorAnimation_Anim2 = New RenderWindow(FrmEditor_Animation.picSprite1.Handle)

            SfmlGameFont = New SFML.Graphics.Font(Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + "\" + FontName)


            LightGfxInfo = New GraphicInfo
            If File.Exists(Path.Graphics & "Misc\Light" & GfxExt) Then
                LightGfx = New Texture(Path.Graphics & "Misc\Light" & GfxExt)
                LightSprite = New Sprite(LightGfx)

                'Cache the width and height
                LightGfxInfo.Width = LightGfx.Size.X
                LightGfxInfo.Height = LightGfx.Size.Y
            End If

            ShadowGfxInfo = New GraphicInfo
            If File.Exists(Path.Graphics & "Misc\Shadow" & GfxExt) Then
                ShadowGfx = New Texture(Path.Graphics & "Misc\Shadow" & GfxExt)
                ShadowSprite = New Sprite(ShadowGfx)

                'Cache the width and height
                ShadowGfxInfo.Width = ShadowGfx.Size.X
                ShadowGfxInfo.Height = ShadowGfx.Size.Y
            End If
        End Sub

#End Region

        Sub DrawChat()
            Dim i As Integer, x As Integer, y As Integer
            Dim text As String

            'first draw back image
            RenderSprite(ChatWindowSprite, GameWindow, ChatWindowX, ChatWindowY - 2, 0, 0, ChatWindowGfxInfo.Width, ChatWindowGfxInfo.Height)

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
                    DrawText(ChatWindowX + x, ChatWindowY + y, text, GetSfmlColor(Chat(i).Color), Microsoft.Xna.Framework.Color.Black, GameWindow)
                    y = y + ChatLineSpacing + 1
                End If

            Next

            'My Text
            'first draw back image
            RenderSprite(MyChatWindowSprite, GameWindow, MyChatX, MyChatY - 5, 0, 0, MyChatWindowGfxInfo.Width, MyChatWindowGfxInfo.Height)

            If Len(ChatInput.CurrentMessage) > 0 Then
                Dim subText As String = ChatInput.CurrentMessage
                While GetTextWidth(subText) > MyChatWindowGfxInfo.Width - ChatEntryPadding
                    subText = subText.Substring(1)
                End While
                DrawText(MyChatX + 5, MyChatY - 3, subText, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            End If
        End Sub

        Friend Sub RenderTextures(txture As Texture, target As RenderWindow, dX As Single, dY As Single, sx As Single, sy As Single, dWidth As Single, dHeight As Single, sWidth As Single, sHeight As Single)
            Dim tmpImage As Sprite = New Sprite(txture) With {
            .TextureRect = New IntRect(sx, sy, sWidth, sHeight),
            .Scale = New Vector2(dWidth / sWidth, dHeight / sHeight),
            .Position = New Vector2(dX, dY)
        }
            target.Draw(tmpImage)
        End Sub








        Friend Sub DrawNpc(mapNpcNum As Integer)
            Dim anim As Byte
            Dim x As Integer
            Dim y As Integer
            Dim sprite As Integer, spriteleft As Integer
            Dim destrec As Rectangle
            Dim srcrec As Rectangle
            Dim attackspeed As Integer

            If MapNpc(mapNpcNum).Num = 0 Then Exit Sub ' no npc set

            If MapNpc(mapNpcNum).X < Viewport.Left OrElse MapNpc(mapNpcNum).X > Viewport.Right Then Exit Sub
            If MapNpc(mapNpcNum).Y < Viewport.Top OrElse MapNpc(mapNpcNum).Y > Viewport.Bottom Then Exit Sub

            sprite = Npc(MapNpc(mapNpcNum).Num).Sprite

            If sprite < 1 OrElse sprite > NumCharacters Then Exit Sub

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

            srcrec = New Rectangle((anim) * (CharacterGfxInfo(sprite).Width / 4), spriteleft * (CharacterGfxInfo(sprite).Height / 4), (CharacterGfxInfo(sprite).Width / 4), (CharacterGfxInfo(sprite).Height / 4))

            ' Calculate the X
            x = MapNpc(mapNpcNum).X * PicX + MapNpc(mapNpcNum).XOffset - ((CharacterGfxInfo(sprite).Width / 4 - 32) / 2)

            ' Is the player's height more than 32..?
            If (CharacterGfxInfo(sprite).Height / 4) > 32 Then
                ' Create a 32 pixel offset for larger sprites
                y = MapNpc(mapNpcNum).Y * PicY + MapNpc(mapNpcNum).YOffset - ((CharacterGfxInfo(sprite).Height / 4) - 32)
            Else
                ' Proceed as normal
                y = MapNpc(mapNpcNum).Y * PicY + MapNpc(mapNpcNum).YOffset
            End If

            destrec = New Rectangle(x, y, CharacterGfxInfo(sprite).Width / 4, CharacterGfxInfo(sprite).Height / 4)

            DrawCharacter(sprite, x, y, srcrec)

            If Npc(MapNpc(mapNpcNum).Num).Behaviour = NpcBehavior.Quest Then
                If CanStartQuest(Npc(MapNpc(mapNpcNum).Num).QuestNum) Then
                    If Player(Myindex).PlayerQuest(Npc(MapNpc(mapNpcNum).Num).QuestNum).Status = QuestStatusType.NotStarted Then
                        DrawEmotes(x, y, 5)
                    End If
                ElseIf Player(Myindex).PlayerQuest(Npc(MapNpc(mapNpcNum).Num).QuestNum).Status = QuestStatusType.Started Then
                    DrawEmotes(x, y, 9)
                End If
            End If

        End Sub





        Friend Sub DrawBlood(index As Integer)
            Dim dest As Point = New Point(FrmGame.PointToScreen(FrmGame.picscreen.Location))
            Dim srcrec As Rectangle
            Dim destrec As Rectangle
            Dim x As Integer
            Dim y As Integer

            With Blood(index)
                If .X < Viewport.Left OrElse .X > Viewport.Right Then Exit Sub
                If .Y < Viewport.Top OrElse .Y > Viewport.Bottom Then Exit Sub

                ' check if we should be seeing it
                If .Timer + 20000 < GetTickCount() Then Exit Sub

                x = ConvertMapX(Blood(index).X * PicX)
                y = ConvertMapY(Blood(index).Y * PicY)

                srcrec = New Rectangle((.Sprite - 1) * PicX, 0, PicX, PicY)

                destrec = New Rectangle(ConvertMapX(.X * PicX), ConvertMapY(.Y * PicY), PicX, PicY)

                RenderSprite(BloodSprite, GameWindow, x, y, srcrec.X, srcrec.Y, srcrec.Width, srcrec.Height)

            End With

        End Sub


        Friend Sub Render_Graphics()
            Dim x As Integer, y As Integer, I As Integer

            'Don't Render IF
            If FrmGame.WindowState = FormWindowState.Minimized Then Exit Sub
            If GettingMap Then Exit Sub

            'lets get going

            'update view around player
            UpdateCamera()

            'let program do other things
            Application.DoEvents()

            'Clear each of our render targets
            GameWindow.DispatchEvents()
            GameWindow.Clear(Microsoft.Xna.Framework.Color.Black)

            'If CurMouseX > 0 AndAlso CurMouseX <= GameWindow.Size.X Then
            '    If CurMouseY > 0 AndAlso CurMouseY <= GameWindow.Size.Y Then
            '        GameWindow.SetMouseCursorVisible(False)
            '    End If
            'End If

            If NumPanorama > 0 AndAlso Map.Panorama > 0 Then
                DrawPanorama(Map.Panorama)
            End If

            If NumParallax > 0 AndAlso Map.Parallax > 0 Then
                DrawParallax(Map.Parallax)
            End If

            ' blit lower tiles
            If NumTileSets > 0 Then

                For x = Viewport.Left To Viewport.Right + 1
                    For y = Viewport.Top To Viewport.Bottom + 1
                        If IsValidMapPoint(x, y) Then
                            DrawMapTile(x, y)
                        End If
                    Next
                Next
            End If

            ' Furniture
            If FurnitureHouse > 0 Then
                If FurnitureHouse = Player(Myindex).InHouse Then
                    If FurnitureCount > 0 Then
                        For I = 1 To FurnitureCount
                            If Furniture(I).ItemNum > 0 Then
                                DrawFurniture(I, 0)
                            End If
                        Next
                    End If
                End If
            End If

            ' events
            If Map.CurrentEvents > 0 AndAlso Map.CurrentEvents <= Map.EventCount Then

                For I = 1 To Map.CurrentEvents
                    If Map.MapEvents(I).Position = 0 Then
                        DrawEvent(I)
                    End If
                Next
            End If

            'blood
            For I = 1 To Byte.MaxValue
                DrawBlood(I)
            Next

            ' Draw out the items
            If NumItems > 0 Then
                For I = 1 To MAX_MAP_ITEMS
                    If MapItem(I).Num > 0 Then
                        DrawItem(I)
                    End If
                Next
            End If

            'Draw sum d00rs.
            If GettingMap Then Exit Sub

            For x = Viewport.Left To Viewport.Right
                For y = Viewport.Top To Viewport.Bottom
                    If IsValidMapPoint(x, y) Then
                        If Map.Tile Is Nothing Then Exit Sub
                        If Map.Tile(x, y).Type = TileType.Door Then
                            DrawDoor(x, y)
                        End If
                    End If
                Next
            Next

            ' draw animations
            If NumAnimations > 0 Then
                For I = 1 To Byte.MaxValue
                    If AnimInstance(I).Used(0) Then
                        DrawAnimation(I, 0)
                    End If
                Next
            End If

            ' Y-based render. Renders Players, Npcs and Resources based on Y-axis.
            For y = 0 To Map.MaxY

                If NumCharacters > 0 Then
                    ' Players
                    For I = 1 To TotalOnline 'MAX_PLAYERS
                        If IsPlaying(I) AndAlso GetPlayerMap(I) = GetPlayerMap(Myindex) Then
                            If Player(I).Y = y Then
                                DrawPlayer(I)
                            End If
                            If PetAlive(I) Then
                                If Player(I).Pet.Y = y Then
                                    DrawPet(I)
                                End If
                            End If
                        End If
                    Next

                    ' Npcs
                    For I = 1 To MAX_MAP_NPCS
                        If MapNpc(I).Y = y Then
                            DrawNpc(I)
                        End If
                    Next

                    ' events
                    If Map.CurrentEvents > 0 AndAlso Map.CurrentEvents <= Map.EventCount Then
                        For I = 1 To Map.CurrentEvents
                            If Map.MapEvents(I).Position = 1 Then
                                If y = Map.MapEvents(I).Y Then
                                    DrawEvent(I)
                                End If
                            End If
                        Next
                    End If

                    ' Draw the target icon
                    If MyTarget > 0 Then
                        If MyTargetType = TargetType.Player Then
                            DrawTarget(Player(MyTarget).X * 32 - 16 + Player(MyTarget).XOffset, Player(MyTarget).Y * 32 + Player(MyTarget).YOffset)
                        ElseIf MyTargetType = TargetType.Npc Then
                            DrawTarget(MapNpc(MyTarget).X * 32 - 16 + MapNpc(MyTarget).XOffset, MapNpc(MyTarget).Y * 32 + MapNpc(MyTarget).YOffset)
                        ElseIf MyTargetType = TargetType.Pet Then
                            DrawTarget(Player(MyTarget).Pet.X * 32 - 16 + Player(MyTarget).Pet.XOffset, (Player(MyTarget).Pet.Y * 32) + Player(MyTarget).Pet.YOffset)
                        End If
                    End If

                    For I = 1 To TotalOnline 'MAX_PLAYERS
                        If IsPlaying(I) Then
                            If Player(I).Map = Player(Myindex).Map Then
                                If CurX = Player(I).X AndAlso CurY = Player(I).Y Then
                                    If MyTargetType = TargetType.Player AndAlso MyTarget = I Then
                                        ' dont render lol
                                    Else
                                        DrawHover(Player(I).X * 32 - 16, Player(I).Y * 32 + Player(I).YOffset)
                                    End If
                                End If

                            End If
                        End If
                    Next
                End If

                ' Resources
                If NumResources > 0 Then
                    If ResourcesInit Then
                        If ResourceIndex > 0 Then
                            For I = 1 To ResourceIndex
                                If MapResource(I).Y = y Then
                                    DrawMapResource(I)
                                End If
                            Next
                        End If
                    End If
                End If
            Next

            ' animations
            If NumAnimations > 0 Then
                For I = 1 To Byte.MaxValue
                    If AnimInstance(I - 1).Used(1) Then
                        DrawAnimation(I - 1, 1)
                    End If
                Next
            End If

            'projectiles
            If NumProjectiles > 0 Then
                For I = 1 To MaxProjectiles
                    If MapProjectiles(I).ProjectileNum > 0 Then
                        DrawProjectile(I)
                    End If
                Next
            End If

            'events
            If Map.CurrentEvents > 0 AndAlso Map.CurrentEvents <= Map.EventCount Then
                For I = 1 To Map.CurrentEvents
                    If Map.MapEvents(I).Position = 2 Then
                        DrawEvent(I)
                    End If
                Next
            End If

            ' blit out upper tiles
            If NumTileSets > 0 Then
                For x = Viewport.Left To Viewport.Right + 1
                    For y = Viewport.Top To Viewport.Bottom + 1
                        If IsValidMapPoint(x, y) Then
                            DrawMapFringeTile(x, y)
                        End If
                    Next
                Next
            End If

            ' Furniture
            If FurnitureHouse > 0 Then
                If FurnitureHouse = Player(Myindex).InHouse Then
                    If FurnitureCount > 0 Then
                        For I = 1 To FurnitureCount
                            If Furniture(I).ItemNum > 0 Then
                                DrawFurniture(I, 1)
                            End If
                        Next
                    End If
                End If
            End If

            DrawNight()

            DrawWeather()
            DrawThunderEffect()
            DrawMapTint()

            ' Draw out a square at mouse cursor
            If MapGrid = True AndAlso InMapEditor Then
                DrawGrid()
            End If

            If FrmEditor_MapEditor.tabpages.SelectedTab Is FrmEditor_MapEditor.tpDirBlock Then
                For x = Viewport.Left To Viewport.Right
                    For y = Viewport.Top To Viewport.Bottom
                        If IsValidMapPoint(x, y) Then
                            DrawDirections(x, y)
                        End If
                    Next
                Next
            End If

            If InMapEditor Then FrmEditor_MapEditor.DrawTileOutline()

            'furniture
            If FurnitureSelected > 0 Then
                If Player(Myindex).InHouse = Myindex Then
                    DrawFurnitureOutline()
                End If
            End If

            ' draw cursor, player X and Y locations
            If BLoc Then
                DrawText(1, HudWindowY + HudPanelGfxInfo.Height + 1, Trim$(String.Format(Language.Game.MapCurLoc, CurX, CurY)), Microsoft.Xna.Framework.Color.Yellow, Microsoft.Xna.Framework.Color.Black, GameWindow)
                DrawText(1, HudWindowY + HudPanelGfxInfo.Height + 15, Trim$(String.Format(Language.Game.MapLoc, GetPlayerX(Myindex), GetPlayerY(Myindex))), Microsoft.Xna.Framework.Color.Yellow, Microsoft.Xna.Framework.Color.Black, GameWindow)
                DrawText(1, HudWindowY + HudPanelGfxInfo.Height + 30, Trim$(String.Format(Language.Game.MapCurMap, GetPlayerMap(Myindex))), Microsoft.Xna.Framework.Color.Yellow, Microsoft.Xna.Framework.Color.Black, GameWindow)
            End If

            ' draw player names
            For I = 1 To TotalOnline 'MAX_PLAYERS
                If IsPlaying(I) AndAlso GetPlayerMap(I) = GetPlayerMap(Myindex) Then
                    DrawPlayerName(I)
                    If PetAlive(I) Then
                        DrawPlayerPetName(I)
                    End If
                End If
            Next

            'draw event names
            For I = 1 To Map.CurrentEvents
                If Map.MapEvents(I).Visible = 1 Then
                    If Map.MapEvents(I).ShowName = 1 Then
                        DrawEventName(I)
                    End If
                End If
            Next

            ' draw npc names
            For I = 1 To MAX_MAP_NPCS
                If MapNpc(I).Num > 0 Then
                    DrawNpcName(I)
                End If
            Next

            If CurrentFog > 0 Then
                DrawFog()
            End If

            ' draw the messages
            For I = 1 To Byte.MaxValue
                If ChatBubble(I).Active Then
                    DrawChatBubble(I)
                End If
            Next

            'action msg
            For I = 1 To Byte.MaxValue
                DrawActionMsg(I)
            Next

            ' Blit out map attributes
            If InMapEditor Then
                DrawMapAttributes()
            End If

            If InMapEditor AndAlso FrmEditor_MapEditor.tabpages.SelectedTab Is FrmEditor_MapEditor.tpEvents Then
                DrawEvents()
                EditorEvent_DrawGraphic()
            End If

            If GettingMap Then Exit Sub

            'draw hp and casting bars
            DrawBars()

            'party
            DrawParty()

            'Render GUI
            DrawGui()

            DrawMapFade()

            'and finally show everything on screen
            GameWindow.Display()
        End Sub




        Friend Sub DrawBars()
            Dim tmpY As Integer
            Dim tmpX As Integer
            Dim barWidth As Integer
            Dim rec(1) As Rectangle

            If GettingMap Then Exit Sub

            ' check for casting time bar
            If SkillBuffer > 0 Then
                ' lock to player
                tmpX = GetPlayerX(Myindex) * PicX + Player(Myindex).XOffset
                tmpY = GetPlayerY(Myindex) * PicY + Player(Myindex).YOffset + 35
                If Skill(PlayerSkills(SkillBuffer)).CastTime = 0 Then Skill(PlayerSkills(SkillBuffer)).CastTime = 1
                ' calculate the width to fill
                barWidth = ((GetTickCount() - SkillBufferTimer) / ((GetTickCount() - SkillBufferTimer) + (Skill(PlayerSkills(SkillBuffer)).CastTime * 1000)) * 64)
                ' draw bars
                rec(1) = New Rectangle(ConvertMapX(tmpX), ConvertMapY(tmpY), barWidth, 4)
                Dim rectShape As New RectangleShape(New Vector2(barWidth, 4)) With {
                .Position = New Vector2(ConvertMapX(tmpX), ConvertMapY(tmpY)),
                .FillColor = Microsoft.Xna.Framework.Color.Cyan
            }
                GameWindow.Draw(rectShape)
            End If

            If Settings.ShowNpcBar = 1 Then
                ' check for hp bar
                For i = 1 To MAX_MAP_NPCS
                    If Map.Npc Is Nothing Then Exit Sub
                    If Map.Npc(i) > 0 Then
                        If Npc(MapNpc(i).Num).Behaviour = NpcBehavior.AttackOnSight OrElse Npc(MapNpc(i).Num).Behaviour = NpcBehavior.AttackWhenAttacked OrElse Npc(MapNpc(i).Num).Behaviour = NpcBehavior.Guard Then
                            ' lock to npc
                            tmpX = MapNpc(i).X * PicX + MapNpc(i).XOffset
                            tmpY = MapNpc(i).Y * PicY + MapNpc(i).YOffset + 35
                            If MapNpc(i).Vital(VitalType.HP) > 0 Then
                                ' calculate the width to fill
                                barWidth = ((MapNpc(i).Vital(VitalType.HP) / (Npc(MapNpc(i).Num).Hp) * 32))
                                ' draw bars
                                rec(1) = New Rectangle(ConvertMapX(tmpX), ConvertMapY(tmpY), barWidth, 4)
                                Dim rectShape As New RectangleShape(New Vector2(barWidth, 4)) With {
                                .Position = New Vector2(ConvertMapX(tmpX), ConvertMapY(tmpY - 75)),
                                .FillColor = Microsoft.Xna.Framework.Color.Red
                            }
                                GameWindow.Draw(rectShape)

                                If MapNpc(i).Vital(VitalType.MP) > 0 Then
                                    ' calculate the width to fill
                                    barWidth = ((MapNpc(i).Vital(VitalType.MP) / (Npc(MapNpc(i).Num).Stat(StatType.Intelligence) * 2) * 32))
                                    ' draw bars
                                    rec(1) = New Rectangle(ConvertMapX(tmpX), ConvertMapY(tmpY), barWidth, 4)
                                    Dim rectShape2 As New RectangleShape(New Vector2(barWidth, 4)) With {
                                    .Position = New Vector2(ConvertMapX(tmpX), ConvertMapY(tmpY - 80)),
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
                    rec(1) = New Rectangle(ConvertMapX(tmpX), ConvertMapY(tmpY), barWidth, 4)
                    Dim rectShape As New RectangleShape(New Vector2(barWidth, 4)) With {
                    .Position = New Vector2(ConvertMapX(tmpX), ConvertMapY(tmpY - 75)),
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
                    rec(1) = New Rectangle(ConvertMapX(tmpX), ConvertMapY(tmpY), barWidth, 4)
                    Dim rectShape As New RectangleShape(New Vector2(barWidth, 4)) With {
                    .Position = New Vector2(ConvertMapX(tmpX), ConvertMapY(tmpY)),
                    .FillColor = Microsoft.Xna.Framework.Color.Cyan
                }
                    GameWindow.Draw(rectShape)
                End If
            End If
        End Sub

        Friend Sub DrawDoor(x As Integer, y As Integer)
            Dim rec As Rectangle

            Dim x2 As Integer, y2 As Integer

            ' sort out animation
            With TempTile(x, y)
                If .DoorAnimate = 1 Then ' opening
                    If .DoorTimer + 100 < GetTickCount() Then
                        If .DoorFrame < 4 Then
                            .DoorFrame = .DoorFrame + 1
                        Else
                            .DoorAnimate = 2 ' set to closing
                        End If
                        .DoorTimer = GetTickCount()
                    End If
                ElseIf .DoorAnimate = 2 Then ' closing
                    If .DoorTimer + 100 < GetTickCount() Then
                        If .DoorFrame > 1 Then
                            .DoorFrame = .DoorFrame - 1
                        Else
                            .DoorAnimate = 0 ' end animation
                        End If
                        .DoorTimer = GetTickCount()
                    End If
                End If

                If .DoorFrame = 0 Then .DoorFrame = 1
            End With

            With rec
                .Y = 0
                .Height = DoorGfxInfo.Height
                .X = ((TempTile(x, y).DoorFrame - 1) * DoorGfxInfo.Width / 4)
                .Width = DoorGfxInfo.Width / 4
            End With

            x2 = (x * PicX)
            y2 = (y * PicY) - (DoorGfxInfo.Height / 2) + 4

            RenderSprite(DoorSprite, GameWindow, ConvertMapX(x * PicX), ConvertMapY((y * PicY) - PicY), rec.X, rec.Y, rec.Width, rec.Height)

        End Sub



        Friend Sub DrawFurnitureOutline()
            Dim rec As Rectangle

            With rec
                .Y = 0
                .Height = Item(GetPlayerInvItemNum(Myindex, FurnitureSelected)).FurnitureHeight * PicY
                .X = 0
                .Width = Item(GetPlayerInvItemNum(Myindex, FurnitureSelected)).FurnitureWidth * PicX
            End With

            Dim rec2 As New RectangleShape With {
            .OutlineColor = New Microsoft.Xna.Framework.Color(Microsoft.Xna.Framework.Color.Blue),
            .OutlineThickness = 0.6,
            .FillColor = New Microsoft.Xna.Framework.Color(Microsoft.Xna.Framework.Color.Transparent),
            .Size = New Vector2(rec.Width, rec.Height),
            .Position = New Vector2(ConvertMapX(CurX * PicX), ConvertMapY(CurY * PicY))
        }
            GameWindow.Draw(rec2)
        End Sub



        Friend Sub DrawMapTint()

            If Map.HasMapTint = 0 Then Exit Sub

            MapTintGfx.Clear(New Microsoft.Xna.Framework.Color(CurrentTintR, CurrentTintG, CurrentTintB, CurrentTintA))

            'MapTintSprite.Color = New Microsoft.Xna.Framework.Color(CurrentTintR, CurrentTintG, CurrentTintB, CurrentTintA)
            MapTintSprite = New Sprite(MapTintGfx.Texture) With {
            .TextureRect = New IntRect(0, 0, GameWindow.Size.X, GameWindow.Size.Y),
            .Position = New Vector2(0, 0)
        }

            MapTintGfx.Display()

            GameWindow.Draw(MapTintSprite)

        End Sub

        Friend Sub DrawMapFade()
            If UseFade = False Then Exit Sub

            MapFadeSprite = New Sprite(New Texture(New SFML.Graphics.Image(GameWindow.Size.X, GameWindow.Size.Y, Microsoft.Xna.Framework.Color.Black))) With {
            .Color = New Microsoft.Xna.Framework.Color(0, 0, 0, FadeAmount),
            .TextureRect = New IntRect(0, 0, GameWindow.Size.X, GameWindow.Size.Y),
            .Position = New Vector2(0, 0)
        }

            GameWindow.Draw(MapFadeSprite)

        End Sub

        Friend Sub DrawTarget(x2 As Integer, y2 As Integer)
            Dim rec As Rectangle
            Dim x As Integer, y As Integer
            Dim width As Integer, height As Integer

            With rec
                .Y = 0
                .Height = TargetGfxInfo.Height
                .X = 0
                .Width = TargetGfxInfo.Width / 2
            End With

            x = ConvertMapX(x2)
            y = ConvertMapY(y2)
            width = (rec.Right - rec.Left)
            height = (rec.Bottom - rec.Top)

            RenderSprite(TargetSprite, GameWindow, x, y, rec.X, rec.Y, rec.Width, rec.Height)
        End Sub



        Friend Sub DrawSkillDesc()
            'first render panel
            RenderSprite(DescriptionSprite, GameWindow, SkillWindowX - DescriptionGfxInfo.Width, SkillWindowY, 0, 0, DescriptionGfxInfo.Width, DescriptionGfxInfo.Height)

            'name
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 12, SkillDescName, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'type
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 28, SkillDescInfo, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'cast time
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 44, "Cast Time: " & SkillDescCastTime, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'cool down
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 58, "CoolDown: " & SkillDescCoolDown, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'Damage
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 74, "Damage: " & SkillDescDamage, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'AOE
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 90, "Aoe: " & SkillDescAoe, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'range
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 104, "Range: " & SkillDescRange, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)

            'requirements
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 128, "=Requirements=", Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'Mp
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 144, "MP: " & SkillDescReqMp, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'level
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 160, "Level: " & SkillDescReqLvl, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'Access
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 176, "Access: " & SkillDescReqAccess, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'Class
            DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 192, "Class: " & SkillDescReqClass, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)

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

        Friend Sub DrawRClick()
            'first render panel
            RenderSprite(RClickSprite, GameWindow, RClickX, RClickY, 0, 0, RClickGfxInfo.Width, RClickGfxInfo.Height)

            DrawText(RClickX + (RClickGfxInfo.Width \ 2) - (GetTextWidth(RClickname) \ 2), RClickY + 10, RClickname, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)

            DrawText(RClickX + (RClickGfxInfo.Width \ 2) - (GetTextWidth("Invite to Trade") \ 2), RClickY + 35, "Invite to Trade", Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)

            DrawText(RClickX + (RClickGfxInfo.Width \ 2) - (GetTextWidth("Invite to Party") \ 2), RClickY + 60, "Invite to Party", Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)

            DrawText(RClickX + (RClickGfxInfo.Width \ 2) - (GetTextWidth("Invite to House") \ 2), RClickY + 85, "Invite to House", Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)

        End Sub

        Friend Sub DrawGui()
            'hide GUI when mapping...
            If HideGui = True Then Exit Sub

            If HudVisible = True Then
                DrawHud()
                DrawActionPanel()
                DrawChat()
                DrawHotbar()
                DrawPetBar()
                DrawPetStats()
            End If

            If PnlCharacterVisible = True Then
                DrawEquipment()
                If ShowItemDesc = True Then DrawItemDesc()
            End If

            If PnlInventoryVisible = True Then
                DrawInventory()
                If ShowItemDesc = True Then DrawItemDesc()
            End If

            If PnlSkillsVisible = True Then
                DrawPlayerSkills()
                If ShowSkillDesc = True Then DrawSkillDesc()
            End If

            If DialogPanelVisible = True Then
                DrawDialogPanel()
            End If

            If PnlBankVisible = True Then
                DrawBank()
            End If

            If PnlShopVisible = True Then
                DrawShop()
            End If

            If PnlTradeVisible = True Then
                DrawTrade()
            End If

            If PnlEventChatVisible = True Then
                DrawEventChat()
            End If

            If PnlRClickVisible = True Then
                DrawRClick()
            End If

            If PnlQuestLogVisible = True Then
                DrawQuestLog()
            End If

            If PnlCraftVisible = True Then
                DrawCraftPanel()
            End If

            DrawCursor()
        End Sub

        Sub DrawNight()
            Dim x As Integer, y As Integer

            If Map.Moral = MapMoralType.Indoors Then Exit Sub

            Select Case Time.Instance.TimeOfDay
                Case TimeOfDay.Dawn
                    NightGfx.Clear(New Microsoft.Xna.Framework.Color(0, 0, 0, 100))
                    Exit Select

                Case TimeOfDay.Dusk
                    NightGfx.Clear(New Microsoft.Xna.Framework.Color(0, 0, 0, 150))
                    Exit Select

                Case TimeOfDay.Night
                    NightGfx.Clear(New Microsoft.Xna.Framework.Color(0, 0, 0, 200))
                    Exit Select

                Case Else
                    Exit Sub
            End Select

            For x = Viewport.Left To Viewport.Right + 1
                For y = Viewport.Top To Viewport.Bottom + 1
                    If IsValidMapPoint(x, y) Then
                        If Map.Tile(x, y).Type = TileType.Light Then
                            Dim x1 = ConvertMapX(x * 32) + 16 - LightGfxInfo.Width / 2
                            Dim y1 = ConvertMapY(y * 32) + 16 - LightGfxInfo.Height / 2

                            'Create the light texture to multiply over the dark texture.
                            LightSprite.Position = New Vector2(x1, y1)
                            LightSprite.Color = Microsoft.Xna.Framework.Color.Red
                            NightGfx.Draw(LightSprite, New RenderStates(BlendMode.Multiply))

                            ''Create the light texture to multiply over the dark texture.
                            'LightSprite.Position = New Vector2(X1, Y1)
                            'LightAreaSprite.Position = New Vector2(X1, Y1)
                            ''LightSprite.Color = New Microsoft.Xna.Framework.Color(Microsoft.Xna.Framework.Color.Red)
                            ''LightAreaSprite.Color = New Microsoft.Xna.Framework.Color(Microsoft.Xna.Framework.Color.Red)
                            'NightGfx.Draw(LightSprite, New RenderStates(BlendMode.Multiply))
                            'NightGfx.Draw(LightAreaSprite, New RenderStates(BlendMode.Multiply))
                        End If
                    End If
                Next
            Next

            NightSprite = New Sprite(NightGfx.Texture)

            NightGfx.Display()
            GameWindow.Draw(NightSprite)
        End Sub

        Friend Sub EditorItem_DrawItem()
            Dim itemnum As Integer
            itemnum = frmEditor_Item.nudPic.Value

            If itemnum < 1 OrElse itemnum > NumItems Then
                frmEditor_Item.picItem.BackgroundImage = Nothing
                Exit Sub
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
                Exit Sub
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
                Exit Sub
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
                Exit Sub
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
                Exit Sub
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
    End Module
End Namespace