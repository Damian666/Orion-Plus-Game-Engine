Friend Class FrmOptions

#Region "Options"

    Private Sub scrlVolume_ValueChanged(sender As Object, e As EventArgs) Handles scrlVolume.ValueChanged
        Settings.Volume = scrlVolume.Value

        MaxVolume = Settings.Volume

        lblVolume.Text = "Volume: " & Settings.Volume

        If Not MusicPlayer Is Nothing Then MusicPlayer.Volume() = MaxVolume

    End Sub

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        'music
        If optMOn.Checked = True Then
            Settings.Music = True
            ' start music playing
            PlayMusic(Trim$(Map.Music))
        Else
            Settings.Music = False
            ' stop music playing
            StopMusic()
            CurMusic = ""
        End If

        'sound
        If optSOn.Checked = True Then
            Settings.Sound = True
        Else
            Settings.Sound = False
            StopSound()
        End If

        'screensize
        Settings.ScreenSize = cmbScreenSize.SelectedIndex

        If chkHighEnd.Checked Then
            Settings.HighEnd = 1
        Else
            Settings.HighEnd = 0
        End If

        If chkNpcBars.Checked Then
            Settings.ShowNpcBar = 1
        Else
            Settings.ShowNpcBar = 0
        End If

        ' save to config.ini
        SaveSettings()

        RePositionGui()

        Me.Visible = False
    End Sub

#End Region

End Class