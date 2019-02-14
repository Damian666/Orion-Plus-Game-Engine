Friend Class ColorButton : Inherits Control
    Private ReadOnly _dRect(10) As RectangleF
    Private ReadOnly _sRect(10) As RectangleF
    Private _textBounds As Rectangle
    Private _img As Image
    Private _btnDown As Boolean
    Private _btnHover As Boolean

    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set
            MyBase.BackColor = Value

            Dim bmp = My.Resources.Skin_Button
            Dim color As Color
            Dim r, g, b As Byte
            For x = 0 To bmp.Width - 1
                For y = 0 To bmp.Height - 1
                    color = bmp.GetPixel(x, y)
                    r = CByte(color.R * 0.5 + BackColor.R * 0.5)
                    g = CByte(color.G * 0.5 + BackColor.G * 0.5)
                    b = CByte(color.B * 0.5 + BackColor.B * 0.5)
                    bmp.SetPixel(x, y, Color.FromArgb(255, r, g, b))
                Next
            Next

            _img = bmp
        End Set
    End Property

    Private Sub SetArea()
        Dim d = If(_btnDown, 9.0F, 18.0F)
        Dim w = If(Width - 6 >= 0, Width - 6.0F, Width)
        Dim h = If(Height - 6 >= 0, Height - 6.0F, Height)

        _dRect(0) = New RectangleF(0.0F, 0.0F, 3.0F, 3.0F)
        _dRect(1) = New RectangleF(3.0F, 0.0F, w, 3.0F)
        _dRect(2) = New RectangleF(Width - 3.0F, 0.0F, 3.0F, 3.0F)
        _dRect(3) = New RectangleF(0.0F, 3.0F, 3.0F, h)
        _dRect(4) = New RectangleF(3.0F, 3.0F, w, h)
        _dRect(5) = New RectangleF(Width - 3.0F, 3.0F, 3.0F, h)
        _dRect(6) = New RectangleF(0.0F, Height - 3.0F, 3.0F, 3.0F)
        _dRect(7) = New RectangleF(3.0F, Height - 3.0F, w, 3.0F)
        _dRect(8) = New RectangleF(Width - 3.0F, Height - 3.0F, 3.0F, 3.0F)

        _sRect(0) = New RectangleF(d, 0.0F, 3.0F, 3.0F)
        _sRect(1) = New RectangleF(d + 3.0F, 0.0F, 3.0F, 3.0F)
        _sRect(2) = New RectangleF(d + 6.0F, 0.0F, 3.0F, 3.0F)
        _sRect(3) = New RectangleF(d, 3.0F, 3.0F, 3.0F)
        _sRect(4) = New RectangleF(d + 3.0F, 3.0F, 3.0F, 3.0F)
        _sRect(5) = New RectangleF(d + 6.0F, 3.0F, 3.0F, 3.0F)
        _sRect(6) = New RectangleF(d, 6.0F, 3.0F, 3.0F)
        _sRect(7) = New RectangleF(d + 3.0F, 6.0F, 3.0F, 3.0F)
        _sRect(8) = New RectangleF(d + 6.0F, 6.0F, 3.0F, 3.0F)

        _textBounds = New Rectangle(4, 4, Width - 8, Height - 8)

        Refresh()
    End Sub

    Protected Overrides Sub OnCreateControl()
        BackColor = BackColor
        SetArea()

        MyBase.OnCreateControl()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        e.Graphics.DrawImage(_img, _dRect(0), _sRect(0), GraphicsUnit.Pixel)
        e.Graphics.DrawImage(_img, _dRect(1), _sRect(1), GraphicsUnit.Pixel)
        e.Graphics.DrawImage(_img, _dRect(2), _sRect(2), GraphicsUnit.Pixel)
        e.Graphics.DrawImage(_img, _dRect(3), _sRect(3), GraphicsUnit.Pixel)
        e.Graphics.DrawImage(_img, _dRect(4), _sRect(4), GraphicsUnit.Pixel)
        e.Graphics.DrawImage(_img, _dRect(5), _sRect(5), GraphicsUnit.Pixel)
        e.Graphics.DrawImage(_img, _dRect(6), _sRect(6), GraphicsUnit.Pixel)
        e.Graphics.DrawImage(_img, _dRect(7), _sRect(7), GraphicsUnit.Pixel)
        e.Graphics.DrawImage(_img, _dRect(8), _sRect(8), GraphicsUnit.Pixel)

        Using format As New StringFormat()
            format.Alignment = StringAlignment.Center
            format.LineAlignment = StringAlignment.Center

            If (Enabled) Then
                e.Graphics.DrawString(Text, Font, New SolidBrush(ForeColor), _textBounds, format)
            Else
                e.Graphics.DrawString(Text, Font, New SolidBrush(Color.Gray), _textBounds, format)
            End If
        End Using
    End Sub

    Private Sub Button_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        _btnDown = True
        SetArea()
    End Sub

    Private Sub Button_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        _btnDown = False
        SetArea()
    End Sub

    Private Sub Button_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        _btnHover = True
        SetArea()
    End Sub

    Private Sub Button_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        _btnDown = False
        _btnHover = False
        SetArea()
    End Sub

    Private Sub Button_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetArea()
    End Sub

    Private Sub Button_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        SetArea()
    End Sub
End Class