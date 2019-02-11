Imports System.IO
Imports System.Xml.Serialization
Imports ASFW.IO.Serialization

Public Class SettingsDef

#If CLIENT Then

    Public Language As String = "English"

    Public Username As String = ""
    Public Password As String = ""
    Public SavePass As Boolean = False

    Public MenuMusic As String = ""
    Public Music As Boolean = True
    Public Sound As Boolean = True
    Public Volume As Single = 100.0F

    Public ScreenSize As Byte = 0
    Public HighEnd As Byte = 0
    Public ShowNpcBar As Byte = 1

    <XmlIgnore()> Public Ip As String = "127.0.0.1"
    <XmlIgnore()> Public Port As Integer = 7001

    <XmlIgnore()> Public GameName As String = "Orion+"
    <XmlIgnore()> Public Website As String = "http://ascensiongamedev.com/index.php"

#ElseIf SERVER Then
    Public GameName As String = "Orion+"
    Public Website As String = "http://ascensiongamedev.com/index.php"

    Public Welcome As String = "Welcome to Orion+, enjoy your stay!"
    Public Port As Integer = 7001

    Public StartMap As Integer = 1
    Public StartX As Integer = 13
    Public StartY As Integer = 7
#End If

End Class

Friend Module modSettings
    Public Settings As New SettingsDef

    Friend Sub LoadSettings()
        Dim cf As String = Path.Config()
        If Not Directory.Exists(cf) Then
            Directory.CreateDirectory(cf)
        End If : cf = cf & "\Settings.xml"

        If Not File.Exists(cf) Then
            File.Create(cf).Dispose()
            SaveXml(Of SettingsDef)(cf, New SettingsDef)
        End If : Settings = LoadXml(Of SettingsDef)(cf)

#If CLIENT Then ' Update Gui
        FrmOptions.optMOn.Checked = Settings.Music
        FrmOptions.optSOn.Checked = Settings.Sound
        FrmOptions.lblVolume.Text = "Volume: " & Settings.Volume
        FrmOptions.scrlVolume.Value = Settings.Volume

        FrmOptions.cmbScreenSize.SelectedIndex = Settings.ScreenSize
#End If
    End Sub

    Friend Sub SaveSettings()
        Dim cf As String = Path.Config()
        If Not Directory.Exists(cf) Then
            Directory.CreateDirectory(cf)
        End If : cf = cf & "\Settings.xml"

        SaveXml(Of SettingsDef)(cf, Settings)
    End Sub

End Module