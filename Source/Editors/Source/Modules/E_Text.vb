Imports System.Text
Imports SFML.Graphics
Imports SFML.Window

Module E_Text
    Friend Const MaxChatDisplayLines As Byte = 8
    Friend Const ChatLineSpacing As Byte = 12 ' Should be same height as font
    Friend Const MyChatTextLimit As Integer = 55
    Friend Const MyAmountValueLimit As Integer = 3
    Friend Const AllChatLineWidth As Integer = 55
    Friend FirstLineindex as integer = 0
    Friend LastLineindex as integer = 0
    Friend ScrollMod As Integer = 0

    ' Game text buffer
    Friend MyText As String = ""

    Friend Sub DrawText(X As Integer, y As Integer, text As String, color As Color, BackColor As Color, ByRef target As RenderWindow, Optional TextSize As Byte = FONT_SIZE)
        Dim mystring As Text = New Text(text, SFMLGameFont) With {
            .CharacterSize = TextSize,
            .Color = BackColor,
            .Position = New Vector2f(X - 1, y - 1)
        }
        target.Draw(mystring)

        mystring.Position = New Vector2f(X - 1, y + 1)
        target.Draw(mystring)

        mystring.Position = New Vector2f(X + 1, y + 1)
        target.Draw(mystring)

        mystring.Position = New Vector2f(X + 1, y + -1)
        target.Draw(mystring)

        mystring.Color = color
        mystring.Position = New Vector2f(X, y)
        target.Draw(mystring)

    End Sub

    Friend Function GetTextWidth(text As String, Optional textsize As Byte = FONT_SIZE) As Integer
        Dim mystring As Text = New Text(text, SFMLGameFont)
        Dim textBounds As FloatRect
        mystring.CharacterSize = textsize
        textBounds = mystring.GetLocalBounds()
        Return textBounds.Width
    End Function

    Friend Function GetSFMLColor(Color As Byte) As Color
        Select Case Color
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

    Friend Function WordWrap(str As String, width As Integer) As List(Of String)

        Dim words As String() = Explode(str, SplitChars)
        Dim curLineLength As Integer = 0
        Dim strBuilder As New StringBuilder()
        Dim i As Integer = 0
        Dim rtnString As New List(Of String)

        While i < words.Length
            Dim word As String = words(i)

            ' If adding the new word to the current line would be too Integer,
            ' then put it on a new line (and split it up if it's too Integer).
            If curLineLength + word.Length > width Then

                ' Only move down to a new line if we have text on the current line.
                ' Avoids situation where wrapped whitespace causes emptylines in text.
                If curLineLength > 0 Then
                    strBuilder.Append("|")
                    curLineLength = 0
                End If

                ' If the current word is too Integer to fit on a line even on it's own then
                ' split the word up.
                While word.Length > width
                    strBuilder.Append(word.Substring(0, width - 1) + "-")
                    word = word.Substring(width - 1)
                    strBuilder.Append("|")
                End While

                ' Remove leading whitespace from the word so the new line starts flush to the left.
                word = word.TrimStart()

            End If

            strBuilder.Append(word)
            curLineLength += word.Length
            i += 1
        End While

        Dim lines As String() = strBuilder.ToString.Split("|")
        For Each line As String In lines
            'line = Replace(line, "|", "")
            rtnString.Add(line.Replace("|", "")) ' & vbNewLine)
        Next

        Return rtnString

    End Function

    Friend Function Explode(str As String, splitChars As Char()) As String()

        Dim parts As New List(Of String)()
        Dim startindex as integer = 0
        Explode = Nothing
        While True
            Dim index as integer = str.IndexOfAny(splitChars, startIndex)

            If index = -1 Then
                parts.Add(str.Substring(startIndex))
                Return parts.ToArray()
            End If

            Dim word As String = str.Substring(startIndex, index - startIndex)
            Dim nextChar As Char = str.Substring(index, 1)(0)
            ' Dashes and the likes should stick to the word occuring before it. Whitespace doesn't have to.
            If Char.IsWhiteSpace(nextChar) Then
                parts.Add(word)
                parts.Add(nextChar.ToString())
            Else
                parts.Add(word + nextChar)
            End If

            startIndex = index + 1
        End While

    End Function

    'Friend Function KeyPressed(e As KeyEventArgs) As String

    '    Dim keyValue As String = ""

    '    If e.KeyCode = 32 Then ' Space
    '        keyValue = ChrW(e.KeyCode)

    '    ElseIf e.KeyCode >= 65 AndAlso e.KeyCode <= 90 Then ' Letters
    '        If e.Shift Then
    '            keyValue = ChrW(e.KeyCode)
    '        Else
    '            keyValue = ChrW(e.KeyCode + 32)
    '        End If

    '    ElseIf e.KeyCode = Keys.D0 Then
    '        If e.Shift Then
    '            keyValue = ")"
    '        Else
    '            keyValue = "0"
    '        End If

    '    ElseIf e.KeyCode = Keys.D1 Then
    '        If e.Shift Then
    '            keyValue = "!"
    '        Else
    '            keyValue = "1"
    '        End If

    '    ElseIf e.KeyCode = Keys.D2 Then
    '        If e.Shift Then
    '            keyValue = "@"
    '        Else
    '            keyValue = "2"
    '        End If

    '    ElseIf e.KeyCode = Keys.D3 Then
    '        If e.Shift Then
    '            keyValue = "#"
    '        Else
    '            keyValue = "3"
    '        End If

    '    ElseIf e.KeyCode = Keys.D4 Then
    '        If e.Shift Then
    '            keyValue = "$"
    '        Else
    '            keyValue = "4"
    '        End If

    '    ElseIf e.KeyCode = Keys.D5 Then
    '        If e.Shift Then
    '            keyValue = "%"
    '        Else
    '            keyValue = "5"
    '        End If

    '    ElseIf e.KeyCode = Keys.D6 Then
    '        If e.Shift Then
    '            keyValue = "^"
    '        Else
    '            keyValue = "6"
    '        End If

    '    ElseIf e.KeyCode = Keys.D7 Then
    '        If e.Shift Then
    '            keyValue = "&"
    '        Else
    '            keyValue = "7"
    '        End If

    '    ElseIf e.KeyCode = Keys.D8 Then
    '        If e.Shift Then
    '            keyValue = "*"
    '        Else
    '            keyValue = "8"
    '        End If

    '    ElseIf e.KeyCode = Keys.D9 Then
    '        If e.Shift Then
    '            keyValue = "("
    '        Else
    '            keyValue = "9"
    '        End If

    '    ElseIf e.KeyCode = Keys.OemPeriod Then
    '        If e.Shift Then
    '            keyValue = ">"
    '        Else
    '            keyValue = "."
    '        End If

    '    ElseIf e.KeyCode = Keys.OemPipe Then
    '        If e.Shift Then
    '            'keyValue= "|"
    '        Else
    '            keyValue = "\"
    '        End If

    '    ElseIf e.KeyCode = Keys.OemCloseBrackets Then
    '        If e.Shift Then
    '            keyValue = "}"
    '        Else
    '            keyValue = "]"
    '        End If

    '    ElseIf e.KeyCode = Keys.OemMinus Then
    '        If e.Shift Then
    '            keyValue = "_"
    '        Else
    '            keyValue = "-"
    '        End If

    '    ElseIf e.KeyCode = Keys.OemOpenBrackets Then
    '        If e.Shift Then
    '            keyValue = "{"
    '        Else
    '            keyValue = "["
    '        End If

    '    ElseIf e.KeyCode = Keys.OemQuestion Then
    '        If e.Shift Then
    '            keyValue = "?"
    '        Else
    '            keyValue = "/"
    '        End If

    '    ElseIf e.KeyCode = Keys.OemQuotes Then
    '        If e.Shift Then
    '            keyValue = Chr(34)
    '        Else
    '            keyValue = "'"
    '        End If

    '    ElseIf e.KeyCode = Keys.OemSemicolon Then
    '        If e.Shift Then
    '            keyValue = ":"
    '        Else
    '            keyValue = ";"
    '        End If

    '    ElseIf e.KeyCode = Keys.Oemcomma Then
    '        If e.Shift Then
    '            keyValue = "<"
    '        Else
    '            keyValue = ","
    '        End If

    '    ElseIf e.KeyCode = Keys.Oemplus Then
    '        If e.Shift Then
    '            keyValue = "+"
    '        Else
    '            keyValue = "="
    '        End If

    '    ElseIf e.KeyCode = Keys.Oemtilde Then
    '        If e.Shift Then
    '            keyValue = "~"
    '        Else
    '            keyValue = "`"
    '        End If

    '    End If

    '    Return keyValue

    'End Function
End Module
























































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































