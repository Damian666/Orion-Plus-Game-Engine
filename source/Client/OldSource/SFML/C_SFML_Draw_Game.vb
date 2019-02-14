Imports SFML.Graphics
Imports SFML.System

Namespace Graphics
    Friend Module modDrawGame

#Region " Graphical Definitions "

        Friend Structure AlertDef
            Dim Message As String
            Dim Created As Integer
            Dim Type As Integer
            Dim Color As Integer
            Dim Scroll As Integer
            Dim X As Integer
            Dim Y As Integer
            Dim Timer As Integer
        End Structure

        Friend Structure BloodDef
            Dim Position As Vector2f
            Dim Frame As Integer
            Dim Timer As Integer

            Friend Sub New(x As Integer, y As Integer)
                Position = New Vector2f(x, y)
                Frame = Rand(1, 3)
                Timer = GetTickCount() + 20000
            End Sub
        End Structure

#End Region

        ' Instanced data
        Friend AlertInstance As New List(Of AlertDef)
        Friend BloodInstance As New List(Of BloodDef)

        ' Instance timers
        Private Const AlertTimer As Integer = 20000

        Friend Sub RenderGame(target As RenderTarget)
            If GettingMap Then Return

            ' Process new view data
            UpdateView()

            ' Cache outter bounds
            Dim minX = Viewport.Left - 1
            Dim maxX = Viewport.Right + 1
            Dim minY = Viewport.Top - 1
            Dim maxY = Viewport.Bottom + 1
            If minX < 0 Then minX = 0
            If minY < 0 Then minY = 0
            If maxX > Map.MaxX Then maxX = Map.MaxX
            If maxY > Map.MaxY Then maxY = Map.MaxY

            '###########################
            '###  Perform Rendering  ###
            '###########################

            SpriteBatch.Begin(target)

            ' Draw background stuff
            DrawPanorama(Map.Panorama)
            DrawParallax(Map.Parallax)

            ' Render map layers below game objects
            For layer = LayerType.Ground To LayerType.Mask2
                DrawMapLayer(minX, maxX, minY, maxY, layer)
            Next

            DrawFurniture(0)

            ' events
            If Map.CurrentEvents > 0 AndAlso Map.CurrentEvents <= Map.EventCount Then

                For I = 1 To Map.CurrentEvents
                    If Map.MapEvents(I).Position = 0 Then
                        DrawEvent(I)
                    End If
                Next
            End If

            DrawBlood()
            DrawItems()
            DrawDoors()

            ' draw animations
            For I = 1 To Byte.MaxValue
                If AnimInstance(I).Used(0) Then
                    DrawAnimation(I, 0)
                End If
            Next

            '########################################
            RenderGameObjects(minX, maxX, minY, maxY)
            '########################################

            'projectiles
            If NumProjectiles > 0 Then
                For I = 1 To MaxProjectiles
                    If MapProjectiles(I).ProjectileNum > 0 Then
                        DrawProjectile(I)
                    End If
                Next
            End If

            ' animations
            For I = 1 To Byte.MaxValue
                If AnimInstance(I - 1).Used(1) Then
                    DrawAnimation(I - 1, 1)
                End If
            Next

            'events
            If Map.CurrentEvents > 0 AndAlso Map.CurrentEvents <= Map.EventCount Then
                For I = 1 To Map.CurrentEvents
                    If Map.MapEvents(I).Position = 2 Then
                        DrawEvent(I)
                    End If
                Next
            End If

            ' Render map layers below game objects
            For layer = LayerType.Fringe To LayerType.Fringe2
                DrawMapLayer(minX, maxX, minY, maxY, layer)
            Next

            DrawFurniture(1)

            'TODO: DrawNight()
            DrawWeather()
            DrawThunderEffect()
            'TODO:  DrawMapTint()

            ' Draw out a square at mouse cursor
            If MapGrid = True AndAlso InMapEditor Then
                'TODO:  DrawGrid()
            End If

            If FrmEditor_MapEditor.tabpages.SelectedTab Is FrmEditor_MapEditor.tpDirBlock Then
                For x = Viewport.Left To Viewport.Right
                    For y = Viewport.Top To Viewport.Bottom
                        If IsValidMapPoint(x, y) Then
                            'TODO: DrawDirections(x, y)
                        End If
                    Next
                Next
            End If

            If InMapEditor Then FrmEditor_MapEditor.DrawTileOutline()

            'furniture
            If FurnitureSelected > 0 Then
                If Player(Myindex).InHouse = Myindex Then
                    'TODO: DrawFurnitureOutline()
                End If
            End If

            ' draw cursor, player X and Y locations
            'TODO: DrawText(1, HudWindowY + HudPanelGfxInfo.Height + 1, Trim$(String.Format(Language.Game.MapCurLoc, CurX, CurY)), Microsoft.Xna.Framework.Color.Yellow, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'TODO: DrawText(1, HudWindowY + HudPanelGfxInfo.Height + 15, Trim$(String.Format(Language.Game.MapLoc, GetPlayerX(Myindex), GetPlayerY(Myindex))), Microsoft.Xna.Framework.Color.Yellow, Microsoft.Xna.Framework.Color.Black, GameWindow)
            'TODO: DrawText(1, HudWindowY + HudPanelGfxInfo.Height + 30, Trim$(String.Format(Language.Game.MapCurMap, GetPlayerMap(Myindex))), Microsoft.Xna.Framework.Color.Yellow, Microsoft.Xna.Framework.Color.Black, GameWindow)

            ' draw player names
            For I = 1 To TotalOnline 'MAX_PLAYERS
                If IsPlaying(I) AndAlso GetPlayerMap(I) = GetPlayerMap(Myindex) Then
                    'TODO: DrawPlayerName(I)
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
                'TODO: If ChatBubble(I).Active Then
                'TODO:  DrawChatBubble(I)
                'TODO:  End If
            Next

            'action msg
            For I = 1 To Byte.MaxValue
                'TODO:  DrawActionMsg(I)
            Next

            ' Blit out map attributes
            If InMapEditor Then
                DrawMapAttributes()

                If FrmEditor_MapEditor.tabpages.SelectedTab Is FrmEditor_MapEditor.tpEvents Then
                    DrawEvents()
                    EditorEvent_DrawGraphic()
                End If
            End If



            'TODO: DrawBars()
            DrawParty()
            'TODO: DrawMapFade()

            SpriteBatch.End()

        End Sub

        Private Sub RenderGameObjects(minX As Integer, maxX As Integer,
                                      minY As Integer, maxY As Integer)
            For y = minY To maxY

                ' Players
                For I = 1 To TotalOnline 'MAX_PLAYERS
                    If IsPlaying(I) AndAlso GetPlayerMap(I) = GetPlayerMap(Myindex) Then
                        If Player(I).Y = y Then
                            'TODO: DrawPlayer(I)
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
                        'TODO: DrawNpc(I)
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
                        'TODO: DrawTarget(Player(MyTarget).X * 32 - 16 + Player(MyTarget).XOffset, Player(MyTarget).Y * 32 + Player(MyTarget).YOffset)
                    ElseIf MyTargetType = TargetType.Npc Then
                        'TODO: DrawTarget(MapNpc(MyTarget).X * 32 - 16 + MapNpc(MyTarget).XOffset, MapNpc(MyTarget).Y * 32 + MapNpc(MyTarget).YOffset)
                    ElseIf MyTargetType = TargetType.Pet Then
                        'TODO: DrawTarget(Player(MyTarget).Pet.X * 32 - 16 + Player(MyTarget).Pet.XOffset, (Player(MyTarget).Pet.Y * 32) + Player(MyTarget).Pet.YOffset)
                    End If
                End If

                For I = 1 To TotalOnline 'MAX_PLAYERS
                    If IsPlaying(I) Then
                        If Player(I).Map = Player(Myindex).Map Then
                            If CurPos.X = Player(I).X AndAlso CurPos.Y = Player(I).Y Then
                                If MyTargetType = TargetType.Player AndAlso MyTarget = I Then
                                    ' dont render lol
                                Else
                                    'TODO: DrawHover(Player(I).X * 32 - 16, Player(I).Y * 32 + Player(I).YOffset)
                                End If
                            End If

                        End If
                    End If
                Next

                ' Resources
                If ResourcesInit Then
                    If ResourceIndex > 0 Then
                        For I = 1 To ResourceIndex
                            If MapResource(I).Y = y Then
                                DrawMapResource(I)
                            End If
                        Next
                    End If
                End If
            Next
        End Sub

#Region " Character/Npc Sprite "

        Friend Sub DrawMapShadow(position As Vector2f)
            position = MapPosition(position) + New Vector2f(0, 6)
            RenderTexture(Shadow, position)
        End Sub

        Friend Sub DrawMapSprite(texture As Texture, position As Vector2f,
                              direction As DirectionType, frame As Integer)

            Dim size As New Vector2f(texture.Size.X / 4, texture.Size.Y / 4)
            Dim source As New FloatRect(size.X * frame,
                                       size.Y * (direction - 1),
                                       size.X, size.Y)

            RenderTexture(texture, source, MapPosition(position))
        End Sub

        Friend Sub DrawCharacter(index As Integer, position As Vector2f,
                                 direction As DirectionType, frame As Integer)
            If index < 1 OrElse index > UBound(Characters) Then Return
            If position.X < Viewport.Left OrElse
               position.X > Viewport.Right OrElse
               position.Y < Viewport.Top OrElse
               position.Y > Viewport.Bottom Then Return

            DrawMapShadow(position)
            DrawMapSprite(Characters(index), position, direction, frame)
        End Sub

        Friend Sub DrawPaperdoll(index As Integer, position As Vector2f,
                                 direction As DirectionType, frame As Integer)
            If index < 1 OrElse index > UBound(Paperdolls) Then Return

            If position.X < Viewport.Left OrElse
               position.X > Viewport.Right OrElse
               position.Y < Viewport.Top OrElse
               position.Y > Viewport.Bottom Then Return

            DrawMapSprite(Paperdolls(index), position, direction, frame)
        End Sub

#End Region

        Friend Sub DrawBlood()
            Dim count = BloodInstance.Count
            If count < 1 Then Return

            Dim deadBloods As New List(Of Integer)
            Dim tick = GetTickCount()
            For i = 0 To count

                ' Add to cleanup if timer expired
                If BloodInstance(i).Timer < tick Then
                    deadBloods.Add(i)
                    Continue For
                End If

                ' Check if render is in bounds
                Dim position = BloodInstance(i).Position
                If position.X < Viewport.Left OrElse
                   position.X > Viewport.Right OrElse
                   position.Y < Viewport.Top OrElse
                   position.Y > Viewport.Bottom Then Continue For

                RenderTexture(Blood, New FloatRect(BloodInstance(i).Frame * PicX, 0,
                                                   PicX, PicY), MapPosition(position))
            Next

            ' Remove dead bloods
            If deadBloods.Count > 0 Then
                count = deadBloods.Count - 1

                For i = count To -1 Step -1
                    BloodInstance.RemoveAt(i)
                Next
            End If

            ' Dispose the list
            deadBloods.Clear()
            deadBloods = Nothing
        End Sub

        Friend Sub DrawDirections(position As Vector2f)
            Dim loc As New Vector2f(MapPositionX(position.X * PicX),
                                   MapPositionY(position.Y * PicY))

            ' Draw Grid
            Dim destination = loc
            Dim source As New FloatRect(0, 24, PicX, PicY)
            RenderTexture(Directions, source, destination)

            ' Draw Directional Arrows
            Dim halfPic As New Vector2f(PicX / 2, PicY / 2)
            Dim quarterPic As New Vector2f(PicX / 4, PicY / 4)

            For i = DirectionType.Up To DirectionType.Right
                Dim blocked = IsDirBlocked(Map.Tile(position.X, position.Y).DirBlock, i)
                Dim blockRow = If(blocked, halfPic.Y, quarterPic.Y)

                destination = New Vector2f(loc.X + DirArrowX(i),
                                       loc.Y + DirArrowY(i) + blockRow)

                source = New FloatRect(DirArrowX(i),
                                        DirArrowY(i),
                                        quarterPic.X,
                                        quarterPic.Y)

                RenderTexture(Directions, source, destination)
            Next
        End Sub

        Friend Sub DrawEmotes(index As Integer, position As Vector2f)
            If index < 1 OrElse index > UBound(Emotes) Then Return

            Dim size As New Vector2f(Emotes(index).Size.X, Emotes(index).Size.Y)
            Dim half = size / 2

            Dim source As New FloatRect(Val(ShowAnimLayers) * half.X, 0, half.X, size.Y)
            position = MapPosition(position)
            position.Y -= size.Y - 16

            RenderTexture(Emotes(index), source, position)
        End Sub

        Friend Sub DrawGrid(minX As Integer, minY As Integer,
                            maxX As Integer, maxY As Integer)
            Dim primitive As New RectangleShape(New Vector2f(PicX, PicY)) With {
                .OutlineThickness = 0.6F,
                .OutlineColor = Color.White
            }

            For y = minY To maxY
                For x = minX To maxX
                    If IsValidMapPoint(x, y) Then
                        primitive.Position = New Vector2f(x * PicX, y * PicY)
                        RenderDrawable(primitive)
                    End If
                Next
            Next
        End Sub

        Friend Sub DrawItems()
            Static timer As Integer = GetTickCount() + 100
            Dim spriteCount = UBound(Items)

            If GetTickCount() > timer Then
                For i = 1 To MAX_MAP_ITEMS

                    ' Get Info
                    Dim index = MapItem(i).Num
                    If index < 1 OrElse index > MAX_ITEMS Then Continue For

                    Dim sprite = Item(index).Pic
                    If sprite < 1 OrElse sprite > spriteCount Then Continue For

                    ' Calculate Frame
                    Dim maxFrames = Items(sprite).Size.X / PicX - 1
                    If MapItem(i).Frame < maxFrames Then
                        MapItem(i).Frame += 1
                    Else
                        MapItem(i).Frame = 0
                    End If

                    ' Cache for heavy usage
                    Dim instance = MapItem(i)

                    ' Check if in render bounds
                    If instance.X < Viewport.Left OrElse
                       instance.X > Viewport.Right OrElse
                       instance.Y < Viewport.Top OrElse
                       instance.Y > Viewport.Bottom Then Continue For

                    ' Render
                    Dim position As New Vector2f(MapPositionX(instance.X * PicX),
                                                 MapPositionY(instance.Y * PicY))
                    Dim source As New FloatRect(instance.Frame * PicX, 0, PicX, PicY)
                    RenderTexture(Items(sprite), source, position)

                Next : timer = GetTickCount() + 100
            End If
        End Sub

        Friend Sub DrawMapLayer(minX As Integer, minY As Integer,
                                maxX As Integer, maxY As Integer, layer As LayerType)

            Dim count = UBound(Tilesets)
            Dim halfX = PicX / 2
            Dim halfY = PicY / 2

            For y = minY To maxY
                For x = minX To maxX

                    Dim tile = Map.Tile(x, y).Layer(layer)
                    Dim sprite = tile.Tileset

                    If sprite < 1 OrElse sprite > count Then Continue For

                    Dim state = Autotile(x, y).Layer(layer).RenderState
                    Dim source As New FloatRect(tile.X * PicX, tile.Y * PicY, PicX, PicY)
                    Dim position = MapPosition(x, y)

                    If state = RenderStateNormal Then
                        RenderTexture(Tilesets(sprite), source, position)
                    ElseIf state = RenderStateAutotile Then
                        DrawAutoTile(layer, position, 1, x, y, 0, False)
                        DrawAutoTile(layer, position + New Vector2f(halfX, 0), 2, x, y, 0, False)
                        DrawAutoTile(layer, position + New Vector2f(0, halfY), 3, x, y, 0, False)
                        DrawAutoTile(layer, position + New Vector2f(halfX, halfY), 4, x, y, 0, False)
                    End If

                Next
            Next

        End Sub

        Friend Sub DrawPanorama(index As Integer)
            If Map.Moral = MapMoralType.Indoors Then Return
            If index < 1 OrElse index > UBound(Panoramas) Then Return

            RenderTexture(Panoramas(index), VectorZero)
        End Sub

        Friend Sub DrawParallax(index As Integer)
            If Map.Moral = MapMoralType.Indoors Then Return
            If index < 1 OrElse index > UBound(Parallaxes) Then Return

            Dim position = MapPosition(GetPlayerPosition(Myindex))
            position.X = (position.X * 2.5) - 50
            position.Y = (position.Y * 2.5) - 50

            RenderTexture(Parallaxes(index), position)
        End Sub

        Friend Sub DrawTarget(position As Vector2f, hover As Boolean)
            Dim frame = Target.Size.X / 2
            Dim source As New FloatRect(frame * hover, 0, frame, Target.Size.Y)
            RenderTexture(Target, source, MapPosition(position))
        End Sub

    End Module
End Namespace