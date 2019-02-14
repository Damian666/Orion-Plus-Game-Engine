Imports SFML.Graphics
Imports SFML.System

Module C_Text
    Friend Const MaxChatDisplayLines As Byte = 8
    Friend Const ChatLineSpacing As Byte = FontSize ' Should be same height as font
    Friend Const MyChatTextLimit As Integer = 40
    Friend Const MyAmountValueLimit As Integer = 3
    Friend Const AllChatLineWidth As Integer = 40
    Friend Const ChatboxPadding As Integer = 10 + 16 + 2 ' 10 = left and right border padding +2 each (3+2+3+2), 16 = scrollbar width, +2 for padding between scrollbar and text
    Friend Const ChatEntryPadding As Integer = 10 ' 5 on left and right
    Friend FirstLineindex As Integer = 0
    Friend LastLineindex As Integer = 0
    Friend ScrollMod As Integer = 0

    Friend Sub DrawNpcName(mapNpcNum As Integer)
        Dim textX As Integer
        Dim textY As Integer
        Dim color As Color, backcolor As Color
        Dim npcNum As Integer

        npcNum = MapNpc(mapNpcNum).Num

        Select Case Npc(npcNum).Behaviour
            Case 0 ' attack on sight
                color = Color.Red
                backcolor = Color.Black
            Case 1, 4 ' attack when attacked + guard
                color = Color.Green
                backcolor = Color.Black
            Case 2, 3, 5 ' friendly + shopkeeper + quest
                color = Color.Yellow
                backcolor = Color.Black
        End Select

        textX = Graphics.MapPositionX(MapNpc(mapNpcNum).X * PicX) + MapNpc(mapNpcNum).XOffset + (PicX \ 2) - GetTextWidth((Trim$(Npc(npcNum).Name))) / 2
        If Npc(npcNum).Sprite < 1 OrElse Npc(npcNum).Sprite > UBound(Graphics.Characters) Then
            textY = Graphics.MapPositionY(MapNpc(mapNpcNum).Y * PicY) + MapNpc(mapNpcNum).YOffset - 16
        Else
            textY = Graphics.MapPositionY(MapNpc(mapNpcNum).Y * PicY) + MapNpc(mapNpcNum).YOffset - (Graphics.Characters(Npc(npcNum).Sprite).Size.Y / 4) + 16
        End If

        ' Draw name
        DrawText(textX, textY, Trim$(Npc(npcNum).Name), color, backcolor)
    End Sub

    Friend Sub DrawEventName(index As Integer)
        Dim textX As Integer
        Dim textY As Integer
        Dim color As Color, backcolor As Color
        Dim name As String

        color = Color.Yellow
        backcolor = Color.Black

        name = Trim$(Map.MapEvents(index).Name)

        ' calc pos
        textX = Graphics.MapPositionX(Map.MapEvents(index).X * PicX) + Map.MapEvents(index).XOffset + (PicX \ 2) - GetTextWidth(Trim$(name)) \ 2
        If Map.MapEvents(index).GraphicType = 0 Then
            textY = Graphics.MapPositionY(Map.MapEvents(index).Y * PicY) + Map.MapEvents(index).YOffset - 16
        ElseIf Map.MapEvents(index).GraphicType = 1 Then
            If Map.MapEvents(index).GraphicNum < 1 OrElse Map.MapEvents(index).GraphicNum > UBound(Graphics.Characters) Then
                textY = Graphics.MapPositionY(Map.MapEvents(index).Y * PicY) + Map.MapEvents(index).YOffset - 16
            Else
                ' Determine location for text
                textY = Graphics.MapPositionY(Map.MapEvents(index).Y * PicY) + Map.MapEvents(index).YOffset - (Graphics.Characters(Map.MapEvents(index).GraphicNum).Size.Y \ 4) + 16
            End If
        ElseIf Map.MapEvents(index).GraphicType = 2 Then
            If Map.MapEvents(index).GraphicY2 > 0 Then
                textX = textX + (Map.MapEvents(index).GraphicY2 * PicY) \ 2 - 16
                textY = Graphics.MapPositionY(Map.MapEvents(index).Y * PicY) + Map.MapEvents(index).YOffset - (Map.MapEvents(index).GraphicY2 * PicY) + 16
            Else
                textY = Graphics.MapPositionY(Map.MapEvents(index).Y * PicY) + Map.MapEvents(index).YOffset - 32 + 16
            End If
        End If

        ' Draw name
        DrawText(textX, textY, Trim$(name), color, backcolor)

    End Sub

    Sub DrawText(x As Integer, y As Integer, text As String, color1 As Color, color2 As Color)
        Graphics.RenderText(text, Graphics.Font_Default, New Vector2f(x, y), color1)
    End Sub

    Public Sub DrawMapAttributes()
        Dim X As Integer
        Dim y As Integer
        Dim tX As Integer
        Dim tY As Integer

        If FrmEditor_MapEditor.tabpages.SelectedTab Is FrmEditor_MapEditor.tpAttributes Then
            For X = TileView.Left To TileView.Right
                For y = TileView.Top To TileView.Bottom
                    If Graphics.IsValidMapPoint(X, y) Then
                        With Map.Tile(X, y)
                            tX = ((Graphics.MapPositionX(X * PicX)) - 4) + (PicX * 0.5)
                            tY = ((Graphics.MapPositionY(y * PicY)) - 7) + (PicY * 0.5)
                            Select Case .Type
                                Case TileType.Blocked
                                    DrawText(tX, tY, "B", (Color.Red), (Color.Black))
                                Case TileType.Warp
                                    DrawText(tX, tY, "W", (Color.Blue), (Color.Black))
                                Case TileType.Item
                                    DrawText(tX, tY, "I", (Color.White), (Color.Black))
                                Case TileType.NpcAvoid
                                    DrawText(tX, tY, "N", (Color.White), (Color.Black))
                                Case TileType.Key
                                    DrawText(tX, tY, "K", (Color.White), (Color.Black))
                                Case TileType.KeyOpen
                                    DrawText(tX, tY, "KO", (Color.White), (Color.Black))
                                Case TileType.Resource
                                    DrawText(tX, tY, "R", (Color.Green), (Color.Black))
                                Case TileType.Door
                                    DrawText(tX, tY, "D", (Color.Black), (Color.Red))
                                Case TileType.NpcSpawn
                                    DrawText(tX, tY, "S", (Color.Yellow), (Color.Black))
                                Case TileType.Shop
                                    DrawText(tX, tY, "SH", (Color.Blue), (Color.Black))
                                Case TileType.Bank
                                    DrawText(tX, tY, "BA", (Color.Blue), (Color.Black))
                                Case TileType.Heal
                                    DrawText(tX, tY, "H", (Color.Green), (Color.Black))
                                Case TileType.Trap
                                    DrawText(tX, tY, "T", (Color.Red), (Color.Black))
                                Case TileType.House
                                    DrawText(tX, tY, "H", (Color.Green), (Color.Black))
                                Case TileType.Craft
                                    DrawText(tX, tY, "C", (Color.Green), (Color.Black))
                                Case TileType.Light
                                    DrawText(tX, tY, "L", (Color.Yellow), (Color.Black))
                            End Select
                        End With
                    End If
                Next
            Next
        End If

    End Sub

    Private ReadOnly WidthTester As Text = New Text("", Graphics.Font_Default)

    Friend Function GetTextWidth(text As String, Optional textSize As Byte = FontSize) As Integer
        WidthTester.DisplayedString = text
        WidthTester.CharacterSize = textSize
        Return WidthTester.GetLocalBounds().Width
    End Function

    Friend Sub AddText(msg As String, color As Integer)
        If TxtChatAdd = "" Then
            TxtChatAdd = TxtChatAdd & msg
            AddChatRec(msg, color)
        Else
            For Each str As String In WordWrap(msg, Graphics.PanelChatInput.Size.X - ChatboxPadding, WrapMode.Font)
                TxtChatAdd = TxtChatAdd & vbNewLine & str
                AddChatRec(str, color)
            Next

        End If
    End Sub

    Friend Sub AddChatRec(msg As String, color As Integer)
        Dim struct As ChatRec
        struct.Text = msg
        struct.Color = color
        Chat.Add(struct)
    End Sub

    Friend Function GetSfmlColor(color As Byte) As Color
        Select Case color
            Case ColorType.Black
                Return SFML.Graphics.Color.Black
            Case ColorType.Blue
                Return New Color(73, 151, 208)
            Case ColorType.Green
                Return New Color(102, 255, 0, 180)
            Case ColorType.Cyan
                Return New Color(0, 139, 139)
            Case ColorType.Red
                Return New Color(255, 0, 0, 180)
            Case ColorType.Magenta
                Return SFML.Graphics.Color.Magenta
            Case ColorType.Brown
                Return New Color(139, 69, 19)
            Case ColorType.Gray
                Return New Color(211, 211, 211)
            Case ColorType.DarkGray
                Return New Color(169, 169, 169)
            Case ColorType.BrightBlue
                Return New Color(0, 191, 255)
            Case ColorType.BrightGreen
                Return New Color(0, 255, 0)
            Case ColorType.BrightCyan
                Return New Color(0, 255, 255)
            Case ColorType.BrightRed
                Return New Color(255, 0, 0)
            Case ColorType.Pink
                Return New Color(255, 192, 203)
            Case ColorType.Yellow
                Return SFML.Graphics.Color.Yellow
            Case ColorType.White
                Return SFML.Graphics.Color.White
            Case Else
                Return SFML.Graphics.Color.White
        End Select
    End Function

    Friend SplitChars As Char() = New Char() {" "c, "-"c, ControlChars.Tab}

    Friend Enum WrapMode
        Characters
        Font
    End Enum

    Friend Enum WrapType
        None
        BreakWord
        Whitespace
        Smart
    End Enum

    Friend Function WordWrap(ByRef str As String, ByRef width As Integer, Optional ByRef mode As WrapMode = WrapMode.Font, Optional ByRef type As WrapType = WrapType.Smart, Optional ByRef size As Byte = FontSize) As List(Of String)
        Dim lines As New List(Of String)
        Dim line As String = ""
        Dim nextLine As String = ""

        If Not str = "" Then
            For Each word In Explode(str, SplitChars)
                Dim trim = word.Trim()
                Dim currentType = type
                Do
                    Dim baseLine = If(line.Length < 1, "", line + " ")
                    Dim newLine = If(nextLine.Length < 1, baseLine + trim, nextLine)
                    nextLine = ""

                    Select Case If(mode = WrapMode.Font, GetTextWidth(newLine, size), newLine.Length)
                        Case < width
                            line = newLine
                            Exit Select

                        Case = width
                            lines.Add(newLine)
                            line = ""
                            Exit Select

                        Case Else
                            Select Case currentType
                                Case WrapType.None
                                    line = newLine
                                    Exit Select

                                Case WrapType.Whitespace
                                    lines.Add(If(line.Length < 1, newLine, line))
                                    line = If(line.Length < 1, "", trim)
                                    Exit Select

                                Case WrapType.BreakWord
                                    Dim remaining = trim
                                    Do
                                        If If(mode = WrapMode.Font, GetTextWidth(baseLine, size), baseLine.Length) > width Then
                                            lines.Add(line)
                                            baseLine = ""
                                            line = ""
                                        End If

                                        Dim i = remaining.Length - 1
                                        While (-1 < i)
                                            Select Case mode
                                                Case WrapMode.Font
                                                    If Not (width < GetTextWidth(baseLine + remaining.Substring(0, i) + "-", size)) Then
                                                        Exit While
                                                    End If
                                                    Exit Select

                                                Case WrapMode.Characters
                                                    If Not (width < (baseLine + remaining.Substring(0, i) + "-").Length) Then
                                                        Exit While
                                                    End If
                                                    Exit Select
                                            End Select
                                            i -= 1
                                        End While

                                        line = baseLine + remaining.Substring(0, i + 1) + If(remaining.Length <= i + 1, "", "-")
                                        lines.Add(line)
                                        line = ""
                                        baseLine = ""
                                        remaining = remaining.Substring(i + 1)
                                    Loop While (remaining.Length > 0) AndAlso (width < If(mode = WrapMode.Font, GetTextWidth(remaining, size), remaining.Length))
                                    line = remaining
                                    Exit Select

                                Case WrapType.Smart
                                    If (line.Length < 1) OrElse (width < If(mode = WrapMode.Font, GetTextWidth(trim, size), trim.Length)) Then
                                        currentType = WrapType.BreakWord
                                    Else
                                        currentType = WrapType.Whitespace
                                    End If
                                    nextLine = newLine

                                    Exit Select

                            End Select
                            Exit Select
                    End Select
                Loop While (nextLine.Length > 0)
            Next
        End If

        If (line.Length > 0) Then
            lines.Add(line)
        End If

        Return lines
    End Function

    Friend Function Explode(str As String, splitChars As Char()) As String()

        Dim parts As New List(Of String)()
        Dim startindex As Integer = 0
        Explode = Nothing

        If str = Nothing Then Exit Function

        While True
            Dim index As Integer = str.IndexOfAny(splitChars, startindex)

            If index = -1 Then
                parts.Add(str.Substring(startindex))
                Return parts.ToArray()
            End If

            Dim word As String = str.Substring(startindex, index - startindex)
            Dim nextChar As Char = str.Substring(index, 1)(0)
            ' Dashes and the likes should stick to the word occuring before it. Whitespace doesn't have to.
            If Char.IsWhiteSpace(nextChar) Then
                parts.Add(word)
                parts.Add(nextChar.ToString())
            Else
                parts.Add(word + nextChar)
            End If

            startindex = index + 1
        End While

    End Function


End Module