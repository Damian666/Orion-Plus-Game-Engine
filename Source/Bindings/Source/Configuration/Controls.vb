#If CLIENT Then
Imports System.IO
Imports ASFW.IO.Serialization
Imports Keys = SFML.Window.Keyboard.Key

Public Class ControlsDef
    Public MoveUp As Integer = Keys.W
    Public MoveDown As Integer = Keys.S
    Public MoveLeft As Integer = Keys.A
    Public MoveRight As Integer = Keys.D

    Public Crouch As Integer = Keys.LAlt
    Public Jump As Integer = Keys.LControl
    Public Run As Integer = Keys.LShift
    Public Attack As Integer = Keys.Space
    Public Interact As Integer = Keys.Space

    Public Inventory As Integer = Keys.B
    Public Equipment As Integer = Keys.V
    Public Character As Integer = Keys.C
    Public Guild As Integer = Keys.J
    Public Spells As Integer = Keys.K
    Public Quests As Integer = Keys.L
    Public Options As Integer = Keys.O
End Class

Namespace Configuration
    Friend Module modControls
        Public Controls As New ControlsDef

        Friend Sub LoadControls()
            Dim cf As String = Path_Local() & "\Controls.xml"

            If Not Directory.Exists(Path_Local()) Then Directory.CreateDirectory(Path_Local())

            If Not File.Exists(cf) Then
                File.Create(cf).Dispose()
                SaveXml(Of ControlsDef)(cf, New ControlsDef)
            End If

            Controls = LoadXml(Of ControlsDef)(cf)
        End Sub

        Friend Sub SaveControls()
            Dim cf As String = Path_Local() & "\Controls.xml"
            SaveXml(Of ControlsDef)(cf, Controls)
        End Sub
    End Module
End Namespace
#End If