Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Winforms

Namespace Graphics
    Friend Module modGame

#Region " Character/Npc Sprite Stuff "
        Friend Sub DrawMapShadow(texture As Texture2D, position As Vector2)
            Dim size As New Vector2(texture.Width, texture.Height)

            Dim destRect = New Rectangle(ConvertMapX(position.X),
                                         ConvertMapY(position.Y) + 6,
                                         size.X, size.Y)

            Dim srcRect = New Rectangle(0, 0, size.X, size.Y)

            RenderTexture(texture, destRect, srcRect)
        End Sub

        Friend Sub DrawMapSprite(texture As Texture2D, position As Vector2,
                              direction As DirectionType, frame As Integer)

            Dim size As New Vector2(texture.Width / 4, texture.Height / 4)

            Dim destRect = New Rectangle(ConvertMapX(position.X),
                                         ConvertMapY(position.Y),
                                         size.X, size.Y)

            Dim srcRect = New Rectangle(size.X * frame,
                                        size.Y * (direction - 1),
                                        size.X, size.Y)

            RenderTexture(texture, destRect, srcRect)
        End Sub

        Friend Sub DrawCharacter(index As Integer, position As Vector2,
                                 direction As DirectionType, frame As Integer)
            If index < 1 OrElse index > UBound(Characters) Then Exit Sub
            If position.X < Viewport.Left OrElse
               position.X > Viewport.Right OrElse
               position.Y < Viewport.Top OrElse
               position.Y > Viewport.Bottom Then Exit Sub

            DrawMapShadow(Shadow, position)
            DrawMapSprite(Characters(index), position, direction, frame)
        End Sub

        Friend Sub DrawPaperdoll(index As Integer, position As Vector2,
                                 direction As DirectionType, frame As Integer)
            If index < 1 OrElse index > UBound(Paperdolls) Then Exit Sub

            If position.X < Viewport.Left OrElse
               position.X > Viewport.Right OrElse
               position.Y < Viewport.Top OrElse
               position.Y > Viewport.Bottom Then Exit Sub

            DrawMapSprite(Paperdolls(index), position, direction, frame)
        End Sub

#End Region

        Friend Sub DrawDirections(position As Vector2)
            Dim loc = New Vector2(ConvertMapX(position.X * PicX),
                                  ConvertMapY(position.Y * PicY))

            ' Draw Grid
            Dim destination = loc
            Dim source = New Rectangle(0, 24, PicX, PicY)
            RenderTexture(Directions, destination, source)

            ' Draw Directional Arrows
            Dim halfPic = New Vector2(PicX / 2, PicY / 2)
            Dim quarterPic = New Vector2(PicX / 4, PicY / 4)

            For i = DirectionType.Up To DirectionType.Right
                Dim blocked = IsDirBlocked(Map.Tile(position.X, position.Y).DirBlock, i)
                Dim blockRow = If(blocked, halfPic.Y, quarterPic.Y)

                destination = New Vector2(loc.X + DirArrowX(i),
                                       loc.Y + DirArrowY(i) + blockRow)

                source = New Rectangle(DirArrowX(i),
                                        DirArrowY(i),
                                        quarterPic.X,
                                        quarterPic.Y)

                RenderTexture(Directions, destination, source)
            Next
        End Sub

        Friend Sub DrawEmotes(index As Integer, position As Vector2)
            If index < 1 OrElse index > UBound(Emotes) Then Exit Sub

            Dim size As New Vector2(Emotes(index).Width, Emotes(index).Height)
            Dim half = size / 2

            Dim source = New Rectangle(Val(ShowAnimLayers) * half.X, 0,
                                        half.X, size.Y)
            position = New Vector2(ConvertMapX(position.X),
                                   ConvertMapY(position.Y) - size.Y - 16)
            RenderTexture(Emotes(index), position, source)
        End Sub

        Friend Sub DrawGrid()
            For y = Viewport.Top To Viewport.Bottom
                For x = Viewport.Left To Viewport.Right

                    If Not IsValidMapPoint(x, y) Then Continue For

                    Dim bounds = New Rectangle(x * PicX, y * PicY,
                                               ConvertMapX((x - 1) * PicX),
                                               ConvertMapY((y - 1) * PicY))

                    RenderRectangle(bounds, 0.6F, Color.White)

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
                    Dim maxFrames = Items(sprite).Width / PicX - 1
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
                    Dim position = New Vector2(ConvertMapX(instance.X * PicX),
                                               ConvertMapY(instance.Y * PicY))
                    Dim source = New Rectangle(instance.Frame * PicX, 0, PicX, PicY)
                    RenderTexture(Items(sprite), position, source)

                Next : timer = GetTickCount() + 100
            End If
        End Sub

        Friend Sub DrawPanorama(index As Integer)
            If Map.Moral = MapMoralType.Indoors Then Exit Sub
            If index < 1 OrElse index > UBound(Panoramas) Then Exit Sub

            Dim view = UniversalBackend.GraphicsDevice.Viewport
            RenderTexture(Panoramas(index), Vector2.Zero, view.Bounds)
        End Sub

        Friend Sub DrawParallax(index As Integer)
            If Map.Moral = MapMoralType.Indoors Then Exit Sub
            If index < 1 OrElse index > UBound(Parallaxes) Then Exit Sub

            Dim position = New Vector2(ConvertMapX(GetPlayerX(Myindex)),
                                       ConvertMapY(GetPlayerY(Myindex)))
            position = New Vector2((position.X * 2.5) - 50, (position.Y * 2.5) - 50)
            RenderTexture(Parallaxes(index), position)
        End Sub

        Friend Sub DrawTargetHover(position As Vector2)
            Dim source = New Rectangle(0, 0, Target.Width / 2, Target.Height)
            position = New Vector2(ConvertMapX(position.X), ConvertMapY(position.Y))
            RenderTexture(Target, position, source)
        End Sub

    End Module
End Namespace