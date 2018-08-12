Imports System

Friend Class FrmOptions
#Region "Options"

    Private Sub scrlVolume_ValueChanged(sender As Object, e As EventArgs) Handles scrlVolume.ValueChanged
        Configuration.Settings.Volume = scrlVolume.Value

        MaxVolume = Configuration.Settings.Volume

        lblVolume.Text = "Volume: " & Configuration.Settings.Volume

        If Not MusicPlayer Is Nothing Then MusicPlayer.Volume() = MaxVolume

    End Sub

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        'music
        If optMOn.Checked = True Then
            Configuration.Settings.Music = 1
            ' start music playing
            PlayMusic(Map.Music.Trim)
        Else
            Configuration.Settings.Music = 0
            ' stop music playing
            StopMusic()
            CurMusic = ""
        End If

        'sound
        If optSOn.Checked = True Then
            Configuration.Settings.Sound = 1
        Else
            Configuration.Settings.Sound = 0
            StopSound()
        End If

        'screensize
        Configuration.Settings.ScreenSize = cmbScreenSize.SelectedIndex

        If chkHighEnd.Checked Then
            Configuration.Settings.HighEnd = 1
        Else
            Configuration.Settings.HighEnd = 0
        End If

        If chkNpcBars.Checked Then
            Configuration.Settings.ShowNpcBar = 1
        Else
            Configuration.Settings.ShowNpcBar = 0
        End If

        Configuration.SaveSettings()

        RePositionGui()

        Me.Visible = False
    End Sub

#End Region
End Class