Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Threading.Tasks
Imports SFML.Audio

Namespace Audio
    Friend Module modAudio
        Private Enum FadeType As Byte
            Off
            FadeOut
            FadeIn
        End Enum

        Friend Music As List(Of String)
        Friend Sound As List(Of String)
        Private sndPlayer As Dictionary(Of Integer, Sound)
        Private mscPlayer As Music
        Private curMusic As String
        Private nxtMusic As String
        Private fadeState As FadeType
        Private mscLoop As Boolean

        Friend Sub Initialize()
            Music = New List(Of String)
            Sound = New List(Of String)
            sndPlayer = New Dictionary(Of Integer, Sound)

            ReloadMusic()
            ReloadSound()
        End Sub

        Friend Sub Destroy()
            StopMusic()
            StopSound()

            If Not Music Is Nothing Then
                Music.Clear()
                Music = Nothing
            End If


            If Not Sound Is Nothing Then
                Sound.Clear()
                Sound = Nothing
            End If

            If Not sndPlayer Is Nothing Then
                sndPlayer.Clear()
                sndPlayer = Nothing
            End If
        End Sub

        Friend Sub ReloadMusic()
            If Music Is Nothing Then Return
            Music.Clear()

            Dim di As New DirectoryInfo(Path_Music)
            Dim fi = di.GetFiles()

            For Each f In fi
                If f.Name.Trim.ToLower().EndsWith(".ogg") OrElse 
                   f.Name.Trim.ToLower().EndsWith(".flac") Then
                    Music.Add(f.Name)
                End If
            Next
        End Sub

        Friend Sub ReloadSound()
            If Sound Is Nothing Then Return
            Sound.Clear()

            Dim di As New DirectoryInfo(Path_Sound)
            Dim fi = di.GetFiles()

            For Each f In fi
                If f.Name.Trim.ToLower().EndsWith(".ogg") OrElse 
                   f.Name.Trim.ToLower().EndsWith(".flac") OrElse
                    f.Name.Trim.ToLower().EndsWith(".wav") Then
                    Sound.Add(f.Name)
                End If
            Next
        End Sub

        '###############'
        '###  Music  ###'
        '###############'

        Friend Async Sub PlayMusic(fn As String, Optional ByVal looped As Boolean = True, Optional ByVal swapSpeed As Single = 5)

            If Not mscPlayer Is Nothing Then
                If mscPlayer.Status = SoundStatus.Paused Then
                    If curMusic = fn AndAlso File.Exists(Path_Music() & fn) Then
                        mscPlayer.Play()
                        Exit Sub
                    End If
                End If
            End If

            If curMusic = fn OrElse Not File.Exists(Path_Music() & fn) Then Return

            nxtMusic = fn
            mscLoop = looped

            fadeState = FadeType.FadeOut
            await (Fade(swapSpeed))
        End Sub

        Private Async function Fade(speed As Integer) As Task(of boolean)
            If mscPlayer Is Nothing Then return nothing
            Dim newVolume As Integer

            Select Case fadeState
                Case FadeType.Off
                    StopMusic()
                    curMusic = ""
                    nxtMusic = ""
                    return nothing

                Case FadeType.FadeOut
                    newVolume = mscPlayer.Volume - speed

                    If newVolume < speed Then
                        StopMusic()
                        curMusic = nxtMusic
                        nxtMusic = ""

                        If curMusic = "" Then
                            fadeState = FadeType.Off
                            return nothing
                        End If

                        mscPlayer = New Music(Path_Music() & curMusic)
                        mscPlayer.Volume() = 0
                        mscPlayer.Loop() = mscLoop
                        mscPlayer.Play()

                        fadeState = FadeType.FadeIn
                    Else
                        mscPlayer.Volume = newVolume
                    End If

                Case FadeType.FadeIn
                    newVolume = mscPlayer.Volume + speed

                    If newVolume >= Configuration.Settings.MusicVolume Then
                        mscPlayer.Volume = Configuration.Settings.MusicVolume
                        fadeState = FadeType.Off
                        return nothing
                    Else
                        mscPlayer.Volume = newVolume
                    End If

            End Select

            return Await Fade(speed)
        End Function

        Friend Sub SetMusicVolume(volume As Single, Optional save As Boolean = False)
            SyncLock mscPlayer
                mscPlayer.Volume = volume
            End SyncLock

            If Not save Then Return
            Configuration.Settings.MusicVolume = volume
            Configuration.SaveSettings()
        End Sub

        Friend Sub StopMusic()
            If mscPlayer Is Nothing Then Exit Sub
            mscPlayer.Stop()
            mscPlayer.Dispose()
            mscPlayer = Nothing
            curMusic = ""
        End Sub

        '###############'
        '###  Sound  ###'
        '###############'

        Friend Sub PlaySound(fn As String, Optional looped As Boolean = False)
            If sndPlayer Is Nothing OrElse Not File.Exists(Path_Sound() & fn) Then Exit Sub
            ClearDeadSounds()

            Dim snd As New Sound(New SoundBuffer(Environment.CurrentDirectory & Path_Sound() & fn))
            snd.Loop() = looped
            snd.Volume() = Configuration.Settings.SoundVolume
            snd.Play()
            sndPlayer.Add(sndPlayer.Count, snd)
        End Sub

        Friend Sub SetSoundVolume(ByVal volume As Single)
            SyncLock sndPlayer
                For Each snd As Sound In sndPlayer.Values
                    snd.Volume = volume
                Next
            End SyncLock
        End Sub

        Friend Sub StopSound()
            If sndPlayer Is Nothing Then Exit Sub

            For Each snd As Sound In sndPlayer.Values
                snd.Stop()
                snd.Dispose()
            Next

            sndPlayer.Clear()
        End Sub

        Friend Sub ClearDeadSounds()
            If sndPlayer Is Nothing Then Exit Sub

            Dim count = sndPlayer.Count - 1
            For i = count To 0 Step -1
                If sndPlayer(i) Is Nothing Then
                    sndPlayer.Remove(i)
                ElseIf sndPlayer(i).Status = SoundStatus.Stopped Then
                    sndPlayer(i).Stop()
                    sndPlayer(i).Dispose()
                    sndPlayer(i) = Nothing
                    sndPlayer.Remove(i)
                End If
            Next
        End Sub

    End Module
End Namespace