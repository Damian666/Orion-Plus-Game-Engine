Imports System
Imports System.IO
Imports Serial = ASFW.IO.Serialization

Public Class SettingsDef
#If CLIENT Then
    Public Username As String = ""
    Public Password As String = ""
    Public SavePass As Boolean = false

    Public MenuMusic As String = ""
    Public Music As Byte = 1
    Public Sound As Byte = 1
    Public Volume As Single = 100

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
        ''' Loads settings file.
        ''' </summary>
        Friend Sub LoadSettings()
            Dim cf As String = Path_Local() & "\Settings.xml"

            ' If we don't have one then create and use it.
            If Not File.Exists(cf) Then
                File.Create(cf).Dispose()
                Serial.SaveXml(Of SettingsDef)(cf, New SettingsDef)
                Return
            End If

            ' We had a file so use it.
            Settings = CType(Serial.LoadXml(Of SettingsDef)(cf), SettingsDef)
        End Sub

        ''' <summary>
        ''' Saves settings file.
        ''' </summary>
        Friend Sub SaveSettings()
            ' Save/Overwrite the settings file.
            Dim cf = Environment.CurrentDirectory & "\Settings.xml"
            Serial.SaveXml(Of SettingsDef)(cf, Settings)
        End Sub
    End Module
End Namespace