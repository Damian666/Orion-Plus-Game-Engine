Imports SFML.Graphics

Public Class MapEditorView
    Private Sub MapView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rsMap.Width = (Map.MaxX + 1) * PIC_X
        rsMap.Height = (Map.MaxY + 1) * PIC_Y
    End Sub

#Region "RsMap"

    Private Sub RsMap_Render(sender As Object, e As EventArgs) Handles rsMap.Render
        Dim X As Integer, Y As Integer, I As Integer

        'Don't Render IF
        If GettingMap Then Exit Sub
        'lets get going

        'update view around player
        UpdateCamera()

        rsMap.Width = (Map.MaxX + 1) * PIC_X
        rsMap.Height = (Map.MaxY + 1) * PIC_Y
        rsMap.RenderWindow.DispatchEvents()
        rsMap.View = New View(New FloatRect(0, 0, rsMap.Width, rsMap.Height))

        'clear any unused gfx
        ClearGFX()

        ' update animation editor
        'If Editor = EDITOR_ANIMATION Then
        '    EditorAnim_DrawAnim()
        'End If

        If InMapEditor AndAlso MapData = True Then
            ' blit lower tiles
            If NumTileSets > 0 Then
                For X = 0 To Map.MaxX + 1
                    For Y = 0 To Map.MaxY + 1
                        If IsValidMapPoint(X, Y) Then
                            DrawMapTile(X, Y)
                        End If
                    Next
                Next
            End If

            ' events
            If Map.CurrentEvents > 0 AndAlso Map.CurrentEvents <= Map.EventCount Then

                For I = 1 To Map.CurrentEvents
                    If Map.MapEvents(I).Position = 0 Then
                        DrawEvent(I)
                    End If
                Next
            End If

            ' Draw out the items
            If NumItems > 0 Then
                For I = 1 To MAX_MAP_ITEMS

                    If MapItem(I).Num > 0 Then
                        DrawItem(I)
                    End If

                Next
            End If

            'Draw sum d00rs.
            For X = 0 To Map.MaxX
                For Y = 0 To Map.MaxY

                    If IsValidMapPoint(X, Y) Then
                        If Map.Tile(X, Y).Type = TileType.Door Then
                            DrawDoor(X, Y)
                        End If
                    End If

                Next
            Next

            ' Y-based render. Renders Players, Npcs and Resources based on Y-axis.
            For Y = 0 To Map.MaxY

                If NumCharacters > 0 Then

                    ' Npcs
                    For I = 1 To MAX_MAP_NPCS
                        If MapNpc(I).Y = Y Then
                            DrawNpc(I)
                        End If
                    Next

                    ' events
                    If Map.CurrentEvents > 0 AndAlso Map.CurrentEvents <= Map.EventCount Then

                        For I = 1 To Map.CurrentEvents
                            If Map.MapEvents(I).Position = 1 Then
                                If Y = Map.MapEvents(I).Y Then
                                    DrawEvent(I)
                                End If
                            End If
                        Next
                    End If

                End If

                ' Resources
                If NumResources > 0 Then
                    If Resources_Init Then
                        If Resource_index > 0 Then
                            For I = 1 To Resource_index
                                If MapResource(I).Y = Y Then
                                    DrawMapResource(I)
                                End If
                            Next
                        End If
                    End If
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

            ' blit out upper tiles
            If NumTileSets > 0 Then
                For X = 0 To Map.MaxX + 1
                    For Y = 0 To Map.MaxY + 1
                        If IsValidMapPoint(X, Y) Then
                            DrawMapFringeTile(X, Y)
                        End If
                    Next
                Next
            End If

            DrawWeather()
            DrawThunderEffect()
            DrawMapTint()

            ' Draw out a square at mouse cursor
            If MapGrid = True Then
                DrawGrid()
            End If

            If SelectedTab = 4 Then
                For X = 0 To Map.MaxX
                    For Y = 0 To Map.MaxY
                        If IsValidMapPoint(X, Y) Then
                            DrawDirections(X, Y)
                        End If
                    Next
                Next
            End If

            'draw event names
            For I = 0 To Map.CurrentEvents
                If Map.MapEvents(I).Visible = 1 Then
                    If Map.MapEvents(I).ShowName = 1 Then
                        DrawEventName(I)
                    End If
                End If
            Next

            ' draw npc names
            For I = 1 To MAX_MAP_NPCS
                If MapNpc(I).Num > 0 Then
                    DrawNPCName(I)
                End If
            Next

            If CurrentFog > 0 Then
                DrawFog()
            End If

            ' Blit out map attributes
            If InMapEditor Then
                DrawMapAttributes()
                DrawTileOutline()
            End If

            If InMapEditor AndAlso SelectedTab = 5 Then
                DrawEvents()
                EditorEvent_DrawGraphic()
            End If

            ' Draw map name
            DrawMapName()
        End If
    End Sub

    Private Sub RsMap_MouseDown(sender As Object, e As MouseEventArgs) Handles rsMap.MouseDown
        If e.X > rsMap.Width - 32 OrElse e.Y > rsMap.Height - 32 Then Exit Sub
        MapEditorMouseDown(e.Button, e.X, e.Y, False)
    End Sub

    Private Sub RsMap_MouseMove(sender As Object, e As MouseEventArgs) Handles rsMap.MouseMove

        CurX = e.Location.X \ PIC_X
        CurY = e.Location.Y \ PIC_Y

        CurMouseX = e.Location.X
        CurMouseY = e.Location.Y

        If e.Button = MouseButtons.Left OrElse e.Button = MouseButtons.Right Then
            MapEditorMouseDown(e.Button, e.X, e.Y)
        End If

        'tslCurXY.Text = "X: " & CurX & " - " & " Y: " & CurY
    End Sub

    Private Sub RsMap_MouseUp(sender As Object, e As MouseEventArgs) Handles rsMap.MouseUp

        CurX = e.Location.X \ PIC_X
        CurY = e.Location.Y \ PIC_Y

    End Sub

#End Region
End Class