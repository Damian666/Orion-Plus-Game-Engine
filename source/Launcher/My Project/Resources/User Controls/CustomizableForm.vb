Friend Class DragableBorderlessForm : Inherits Form
    Private _x As Integer
    Private _y As Integer
    Private _isDown As Boolean

    Public Property MoveableTop As Integer = 0
    Public Property MoveableLeft As Integer = 0
    Public Property MoveableRight As Integer = 0
    Public Property MoveableBottom As Integer = 0

    Private Sub Form_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If FormBorderStyle <> FormBorderStyle.None Then Return

        If e.X > MoveableLeft AndAlso
           e.X < Width - MoveableRight AndAlso
           e.Y > MoveableTop AndAlso
           e.Y < Height - MoveableBottom Then Return

        _isDown = True
        _x = e.X
        _y = e.Y
    End Sub

    Private Sub Form_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If Not _isDown Then Return
        Left = Cursor.Position.X - _x
        Top = Cursor.Position.Y - _y
    End Sub

    Private Sub Form_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        _isDown = False
        _x = 0
        _y = 0
    End Sub
End Class