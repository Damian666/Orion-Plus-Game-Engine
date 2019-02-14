Imports SFML.Audio
Imports System.IO

Namespace Audio
    Friend Module modAudio
        Private MediaPlayer As Music
        Private SfxPlayer As Dictionary(Of Integer, Sound)

        Friend Sub Initialize()
            CacheSongs()
            CacheSounds()

            SfxPlayer = New Dictionary(Of Integer, Sound)
        End Sub

        Friend Sub Destroy()
            ClearSongs()
            ClearSounds()

            StopSong()
            StopSound()
        End Sub

#Region " Songs "

        Private Enum FadeType As Byte
            Stopped
            Increase
            Decrease
            SwapSong
        End Enum

        Friend Songs As List(Of String)
        Private _song As String
        Private _queueu As String
        Private _fadeVolume As Single
        Private _fadeMode As FadeType

        Friend Sub CacheSongs()
            ClearSongs() : Songs = New List(Of String)
            Dim fis = New DirectoryInfo(Path.Songs).GetFiles("*.xnb")
            For Each fi In fis
                Songs.Add(fi.Name.ToLower())
            Next
        End Sub

        Friend Sub ClearSongs()
            If Songs Is Nothing Then Return
            Songs.Clear() : Songs = Nothing
        End Sub

        Friend Sub PlaySong(song As String)
            song = song.ToLower()
            If Not Settings.MusicOn OrElse
               Not Songs.Contains(song) OrElse
               song = _song Then Return

            _queueu = song

            If _song = "" Then
                SetFade(FadeType.Increase)
            Else
                SetFade(FadeType.SwapSong)
            End If
        End Sub

        Friend Sub SetMusicVolume(volume As Single)
            If MediaPlayer Is Nothing Then Return
            volume = If(volume > 1.0F, 1.0F, If(volume < 0F, 0F, volume))
            MediaPlayer.Volume = (Settings.MusicVolume * 100.0F) * volume
            Settings.MusicVolume = volume
        End Sub

        Friend Sub StopSong()
            SetFade(FadeType.Decrease)
        End Sub

        Private Sub SetFade(type As FadeType)
            _fadeMode = type

            If type = FadeType.Decrease Then
                _fadeVolume = 1.0F
            ElseIf type = FadeType.Increase Then
                _fadeVolume = 0F
            End If
        End Sub

        Friend Sub ProcessFade()
            Select Case _fadeMode
                Case FadeType.Stopped ' Do nothing
                    Return

                Case FadeType.Increase ' Starts new Song
                    If MediaPlayer Is Nothing Then
                        MediaPlayer = New Music(Path.Songs & _queueu)
                        MediaPlayer.Loop = True
                        MediaPlayer.Volume = 0F
                        MediaPlayer.Play()

                        _song = _queueu
                        _queueu = ""
                    End If

                    _fadeVolume += 0.05F
                    SetMusicVolume(_fadeVolume)
                    If _fadeVolume >= 1.0F Then
                        _fadeMode = FadeType.Stopped
                    End If

                Case FadeType.Decrease ' Stop Song
                    _song = ""
                    _queueu = ""

                    If MediaPlayer Is Nothing Then
                        _fadeMode = FadeType.Stopped
                    End If

                    _fadeVolume -= 0.05F
                    SetMusicVolume(_fadeVolume)
                    If _fadeVolume > 0F Then Return

                    MediaPlayer.Stop()
                    MediaPlayer.Dispose()
                    MediaPlayer = Nothing

                Case FadeType.SwapSong ' Transition song
                    If MediaPlayer Is Nothing Then Return

                    _fadeVolume -= 0.05F
                    SetMusicVolume(_fadeVolume)

                    If _fadeVolume <= 0F Then
                        _fadeMode = FadeType.Increase
                    End If

            End Select
        End Sub
#End Region

#Region " Sounds "

        Friend Sounds As List(Of String)

        Friend Sub CacheSounds()
            ClearSounds() : Sounds = New List(Of String)

            Dim fis = New DirectoryInfo(Path.Sounds).GetFiles("*.xnb")

            For Each fi In fis
                Sounds.Add(fi.Name.ToLower())
            Next
        End Sub

        Friend Sub ClearSounds()
            If Sounds Is Nothing Then Return
            Sounds.Clear() : Sounds = Nothing
        End Sub

        Friend Sub PlaySound(sfx As String)
            If Not Settings.SoundOn OrElse
               Not Sounds.Contains(sfx) Then Return

            ClearDeadSounds()

            Dim snd As New Sound(New SoundBuffer(Path.Sounds & sfx))
            snd.Loop() = False
            snd.Volume() = Settings.SoundVolume
            snd.Play()
            SfxPlayer.Add(SfxPlayer.Count, snd)
        End Sub

        Friend Sub SetSoundVolume(ByVal volume As Single)
            volume = If(volume > 1.0F, 1.0F, If(volume < 0F, 0F, volume))

            SyncLock SfxPlayer
                For Each snd As Sound In SfxPlayer.Values
                    snd.Volume = (Settings.SoundVolume * 100.0F) * volume
                Next
            End SyncLock
        End Sub

        Friend Sub StopSound()
            If SfxPlayer Is Nothing Then Return

            For Each snd As Sound In SfxPlayer.Values
                snd.Stop()
                snd.Dispose()
            Next

            SfxPlayer.Clear()
        End Sub

        Friend Sub ClearDeadSounds()
            If SfxPlayer Is Nothing Then Return

            Dim count = SfxPlayer.Count - 1
            For i = count To 0 Step -1
                If SfxPlayer(i) Is Nothing Then
                    SfxPlayer.Remove(i)
                ElseIf SfxPlayer(i).Status = SoundStatus.Stopped Then
                    SfxPlayer(i).Stop()
                    SfxPlayer(i).Dispose()
                    SfxPlayer(i) = Nothing
                    SfxPlayer.Remove(i)
                End If
            Next
        End Sub

#End Region
    End Module
End Namespace