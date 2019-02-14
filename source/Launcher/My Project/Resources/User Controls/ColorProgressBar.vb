Friend Class ColorProgressBar : Inherits Control

    Private _backBrush As Brush
    Private _borderPen As Pen
    Private _borderColor As Color = Color.DarkSlateGray
    Private _foreBrush As Brush
    Private _min As Integer = 0
    Private _max As Integer = 100
    Private _val As Integer = 0
    Public Property StepCount As Integer = 10

    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set
            MyBase.BackColor = Value
            _backBrush = New SolidBrush(Value)
            Invalidate()
        End Set
    End Property

    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set
            _borderColor = Value
            _borderPen = New Pen(Value)
            Invalidate()
        End Set
    End Property

    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set
            MyBase.ForeColor = Value
            _foreBrush = New SolidBrush(Value)
            Invalidate()
        End Set
    End Property

    Public Property Minimum As Integer
        Get
            Return _min
        End Get
        Set
            If _val < Value Then _val = Value
            _min = Value
            Invalidate()
        End Set
    End Property

    Public Property Maximum As Integer
        Get
            Return _max
        End Get
        Set
            If _val > Value Then _val = Value
            _max = Value
            Invalidate()
        End Set
    End Property

    Public Property Value As Integer
        Get
            Return _val
        End Get
        Set
            If Value < _min Then
                _val = _min
            ElseIf Value > _max Then
                _val = _max
            Else
                _val = Value
            End If
            Invalidate()
        End Set
    End Property

    Public Sub PerformStep()
        Value += StepCount
    End Sub

    Public Sub PerformStep(amount As Integer)
        Value += amount
    End Sub

    Protected Overrides Sub OnCreateControl()
        If _backBrush Is Nothing Then
            _backBrush = New SolidBrush(MyBase.BackColor)
        End If

        If _borderPen Is Nothing Then
            _borderPen = New Pen(_borderColor)
        End If

        If _foreBrush Is Nothing Then
            _foreBrush = New SolidBrush(MyBase.ForeColor)
        End If

        MyBase.OnCreateControl()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim rect = e.ClipRectangle
        rect.Width -= 1
        rect.Height -= 1

        e.Graphics.FillRectangle(_backBrush, rect)
        e.Graphics.DrawRectangle(_borderPen, rect)

        rect = New Rectangle(1, 1, rect.Width - 1, rect.Height - 1)

        e.Graphics.FillRectangle(_foreBrush, 1, 1,
            CInt(rect.Width * (Value / Maximum)), rect.Height)
    End Sub
End Class