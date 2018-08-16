Imports System
Imports System.IO
Imports Serial = ASFW.IO.Serialization

Public Class SettingsDef
#If CLIENT Then
    Public Username As String = ""
    Public Password As String = ""
    Public SavePass As Boolean = False

    Public MenuMusic As String = ""
    Public MusicVolume As Single = 100
    Public SoundVolume As Single = 100

    Public Ip As String = "localhost"
    Public Port As Integer = 7001

    Public ScreenSize As Byte = 0
    Public HighEnd As Byte = 0
    Public ShowNpcBar As Byte = 0
#ElseIf SERVER Then
    Public GameName As String = "Orion ORPG Engine"
    Public Welcome As String = "Welcome to the Orion ORPG Engine."
    Public Website As String = "http://ascensiongamedev.com/index.php"
    Public Port As Integer = 7001

    Public StartMap As Integer = 1
    Public StartX As Integer = 13
    Public StartY As Integer = 7
#End If
End Class

Namespace Configuration
    Friend Module modSettings
        Public Settings As New SettingsDef

        ''' <summary>
        ''' Checks for path, file, and object existance and creates them if missing.
        ''' </summary>
        Private Sub CheckIO(cd As String, cf As String)
            If Not Directory.Exists(cd) Then Directory.CreateDirectory(cd)
            If Not File.Exists(cd & cf) Then File.Create(cd & cf).Dispose()
            If Settings Is Nothing Then Settings = New SettingsDef
        End Sub

        ''' <summary>
        ''' Loads settings file.
        ''' </summary>
        Friend Sub LoadSettings()
            Dim cd = Environment.CurrentDirectory
            Dim cf = "\Settings.xml"

#If CLIENT Then
            ' Use local path if App Dir contains no override.
            If Not File.Exists(cd & cf) Then cd = Path_Local()
#End If

            Try ' Load the file
                Settings = Serial.LoadXml(Of SettingsDef)(cf)
            Catch ' The file is missing or incompatible so overwrite it.
                If File.Exists(cd & cf) Then File.Delete(cd & cf)
                SaveSettings()
            End Try
        End Sub

        ''' <summary>
        ''' Saves settings file.
        ''' </summary>
        Friend Sub SaveSettings()
#If CLIENT Then
            Dim cd = Environment.CurrentDirectory
#ElseIf SERVER Then
            Dim cd = Path_Local()
#End If
            Dim cf = "\Settings.xml"

            CheckIO(cd, cf)
            Serial.SaveXml(Of SettingsDef)(cd & cf, Settings)
        End Sub

    End Module
End Namespace