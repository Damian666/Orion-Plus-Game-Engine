Imports System

Friend Class FrmOptions

#Region "Options"

    Private Sub scrlVolume_ValueChanged(sender As Object, e As EventArgs) Handles scrlVolume.ValueChanged
        Audio.SetMusicVolume(scrlVolume.Value)
        lblVolume.Text = "Volume: " & scrlVolume.Value
    End Sub

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        Configuration.Settings.MusicVolume = scrlVolume.Value
        Configuration.Settings.ScreenSize = cmbScreenSize.SelectedIndex
        Configuration.Settings.HighEnd = chkHighEnd.Checked
        Configuration.Settings.ShowNpcBar = chkNpcBars.Checked

        Configuration.SaveSettings()

        Audio.PlayMusic(Map.Music.Trim)
        Audio.StopSound()
        RePositionGui()
        Visible = False
    End Sub

#End Region

End Class