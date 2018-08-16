#If CLIENT Then
Imports System
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

        ''' <summary>
        ''' Checks for path, file, and object existance and creates them if missing.
        ''' </summary>
        Private Sub CheckIO(cd As String, cf As String)
            If Not Directory.Exists(cd) Then Directory.CreateDirectory(cd)
            If Not File.Exists(cd & cf) Then File.Create(cd & cf).Dispose()
            If Controls Is Nothing Then Controls = New ControlsDef
        End Sub

        ''' <summary>
        ''' Loads controls file.
        ''' </summary>
        Friend Sub LoadControls()
            Dim cd = Environment.CurrentDirectory
            Dim cf = "\Controls.xml"

            ' Use local path if App Dir contains no override.
            If Not File.Exists(cd & cf) Then cd = Path_Local()

            Try ' Load the file
                Controls = LoadXml(Of ControlsDef)(cf)
            Catch ' The file is missing or incompatible so overwrite it.
                If File.Exists(cd & cf) Then File.Delete(cd & cf)
                SaveControls()
            End Try
        End Sub

        ''' <summary>
        ''' Saves controls file.
        ''' </summary>
        Friend Sub SaveControls()
            Dim cd = Environment.CurrentDirectory
            Dim cf = "\Controls.xml"

            CheckIO(cd, cf)
            SaveXml(Of ControlsDef)(cd & cf, Controls)
        End Sub
    End Module
End Namespace
#End If