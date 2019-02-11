Imports System.IO
Imports System.Windows.Forms
Imports SFML.Audio

Module C_Sound

    'Music + Sound Players
    Friend SoundPlayer As Sound

    Friend ExtraSoundPlayer As Sound
    Friend MusicPlayer As Music
    Friend PreviewPlayer As Music
    Friend MusicCache(100) As String
    Friend SoundCache(100) As String

    Friend FadeInSwitch As Boolean
    Friend FadeOutSwitch As Boolean
    Friend CurMusic As String
    Friend MaxVolume As Single

    Sub PlayMusic(fileName As String)
        If Settings.Music = 0 OrElse Not File.Exists(Path.Music & fileName) Then Exit Sub
        If fileName = CurMusic Then Exit Sub

        If MusicPlayer Is Nothing Then
            Try
                MusicPlayer = New Music(Path.Music & fileName)
                MusicPlayer.Loop() = True
                MusicPlayer.Volume() = 0
                MusicPlayer.Play()
                CurMusic = fileName
                FadeInSwitch = True
            Catch ex As Exception

            End Try
        Else
            Try
                CurMusic = fileName
                FadeOutSwitch = True
            Catch ex As Exception

            End Try
        End If
    End Sub

    Sub StopMusic()
        If MusicPlayer Is Nothing Then Exit Sub
        MusicPlayer.Stop()
        MusicPlayer.Dispose()
        MusicPlayer = Nothing
        CurMusic = ""
    End Sub

    Sub PlayPreview(fileName As String)
        If Settings.Music = 0 OrElse Not File.Exists(Path.Music & fileName) Then Exit Sub

        If PreviewPlayer Is Nothing Then
            Try
                PreviewPlayer = New Music(Path.Music & fileName)
                PreviewPlayer.Loop() = True
                PreviewPlayer.Volume() = 75
                PreviewPlayer.Play()
            Catch ex As Exception

            End Try
        Else
            Try
                StopPreview()
                PreviewPlayer = New Music(Path.Music & fileName)
                PreviewPlayer.Loop() = True
                PreviewPlayer.Volume() = 75
                PreviewPlayer.Play()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Sub StopPreview()
        If PreviewPlayer Is Nothing Then Exit Sub
        PreviewPlayer.Stop()
        PreviewPlayer.Dispose()
        PreviewPlayer = Nothing
    End Sub

    Sub PlaySound(fileName As String, Optional looped As Boolean = False)
        If Settings.Sound = 0 OrElse Not File.Exists(Path.Sounds & fileName) Then Exit Sub

        Dim buffer As SoundBuffer
        If SoundPlayer Is Nothing Then
            SoundPlayer = New Sound()
            buffer = New SoundBuffer(Path.Sounds & fileName)
            SoundPlayer.SoundBuffer = buffer
            If looped = True Then
                SoundPlayer.Loop() = True
            Else
                SoundPlayer.Loop() = False
            End If
            SoundPlayer.Volume() = MaxVolume
            SoundPlayer.Play()
        Else
            SoundPlayer.Stop()
            buffer = New SoundBuffer(Path.Sounds & fileName)
            SoundPlayer.SoundBuffer = buffer
            If looped = True Then
                SoundPlayer.Loop() = True
            Else
                SoundPlayer.Loop() = False
            End If
            SoundPlayer.Volume() = MaxVolume
            SoundPlayer.Play()
        End If
    End Sub

    Sub StopSound()
        If SoundPlayer Is Nothing Then Exit Sub
        SoundPlayer.Dispose()
        SoundPlayer = Nothing
    End Sub

    Sub PlayExtraSound(fileName As String, Optional looped As Boolean = False)
        If Settings.Sound = 0 OrElse Not File.Exists(Path.Sounds & fileName) Then Exit Sub

        Dim buffer As SoundBuffer
        If ExtraSoundPlayer Is Nothing Then
            ExtraSoundPlayer = New Sound()
            buffer = New SoundBuffer(Path.Sounds & fileName)
            ExtraSoundPlayer.SoundBuffer = buffer
            If looped = True Then
                ExtraSoundPlayer.Loop() = True
            Else
                ExtraSoundPlayer.Loop() = False
            End If
            ExtraSoundPlayer.Volume() = MaxVolume
            ExtraSoundPlayer.Play()
        Else
            ExtraSoundPlayer.Stop()
            buffer = New SoundBuffer(Path.Sounds & fileName)
            ExtraSoundPlayer.SoundBuffer = buffer
            If looped = True Then
                ExtraSoundPlayer.Loop() = True
            Else
                ExtraSoundPlayer.Loop() = False
            End If
            ExtraSoundPlayer.Volume() = MaxVolume
            ExtraSoundPlayer.Play()
        End If
    End Sub

    Sub StopExtraSound()
        If ExtraSoundPlayer Is Nothing Then Exit Sub
        ExtraSoundPlayer.Dispose()
        ExtraSoundPlayer = Nothing
    End Sub

    Sub FadeIn()

        If MusicPlayer Is Nothing Then Exit Sub

        If MusicPlayer.Volume() >= MaxVolume Then FadeInSwitch = False
        MusicPlayer.Volume() = MusicPlayer.Volume() + 3

    End Sub

    Sub FadeOut()
        Dim tmpmusic As String
        If MusicPlayer Is Nothing Then Exit Sub

        If MusicPlayer.Volume() = 0 OrElse MusicPlayer.Volume() < 3 Then
            FadeOutSwitch = False
            If CurMusic = "" Then
                StopMusic()
            Else
                tmpmusic = CurMusic
                StopMusic()
                PlayMusic(tmpmusic)
            End If
        End If
        If MusicPlayer Is Nothing Then Exit Sub

        MusicPlayer.Volume() = MusicPlayer.Volume() - 3

    End Sub

End Module