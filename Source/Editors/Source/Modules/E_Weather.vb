Imports System.IO
Imports SFML.Audio
Imports SFML.Graphics
Imports SFML.Window

Friend Module E_Weather
    Friend Const MAX_WEATHER_PARTICLES As Integer = 100

    Friend WeatherParticle(MAX_WEATHER_PARTICLES) As WeatherParticleRec
    Friend WeatherSoundPlayer As Sound
    Friend CurWeatherMusic As String

    Friend Structure WeatherParticleRec
        Dim type As Integer
        Dim X As Integer
        Dim Y As Integer
        Dim Velocity As Integer
        Dim InUse As Integer
    End Structure

    Sub ProcessWeather()
        Dim i As Integer, x As Integer

        If CurrentWeather > 0 Then
            If CurrentWeather = WeatherType.Rain OrElse CurrentWeather = WeatherType.Storm Then
                PlayWeatherSound("Rain.ogg", True)
            End If
            x = Rand(1, 101 - CurrentWeatherIntensity)
            If x = 1 Then
                'Add a new particle
                For i = 1 To MAX_WEATHER_PARTICLES
                    If WeatherParticle(i).InUse = 0 Then
                        If Rand(1, 3) = 1 Then
                            WeatherParticle(i).InUse = 1
                            WeatherParticle(i).type = CurrentWeather
                            WeatherParticle(i).Velocity = Rand(8, 14)
                            WeatherParticle(i).X =  - 32
                            WeatherParticle(i).Y = ( Rand(-32, frmMapEditor.rsMap.Size.Height))
                        Else
                            WeatherParticle(i).InUse = 1
                            WeatherParticle(i).type = CurrentWeather
                            WeatherParticle(i).Velocity = Rand(10, 15)
                            WeatherParticle(i).X = (  Rand(-32, frmMapEditor.rsMap.Size.Width))
                            WeatherParticle(i).Y =  - 32
                        End If
                        'Exit For
                    End If
                Next
            End If
        Else
            StopWeatherSound()
        End If
        If CurrentWeather = WeatherType.Storm Then
            x = Rand(1, 400 - CurrentWeatherIntensity)
            If x = 1 Then
                'Draw Thunder
                DrawThunder = Rand(15, 22)
                PlayExtraSound("Thunder.ogg")
            End If
        End If
        For i = 1 To MAX_WEATHER_PARTICLES
            If WeatherParticle(i).InUse = 1 Then
                If WeatherParticle(i).X > Map.MaxX * 32 OrElse WeatherParticle(i).Y > Map.MaxY * 32 Then
                    WeatherParticle(i).InUse = 0
                Else
                    WeatherParticle(i).X = WeatherParticle(i).X + WeatherParticle(i).Velocity
                    WeatherParticle(i).Y = WeatherParticle(i).Y + WeatherParticle(i).Velocity
                End If
            End If
        Next

    End Sub

    Friend Sub DrawThunderEffect()
        If InMapEditor Then Exit Sub

        If DrawThunder > 0 Then
            Dim tmpSprite As Sprite
            tmpSprite = New Sprite(New Texture(New SFML.Graphics.Image(frmMapEditor.rsMap.Size.Width, frmMapEditor.rsMap.Size.Height, SFML.Graphics.Color.White))) With {
                .Color = New Color(255, 255, 255, 150),
                .TextureRect = New IntRect(0, 0, frmMapEditor.rsMap.Size.Width, frmMapEditor.rsMap.Size.Height),
                .Position = New Vector2f(0, 0)
            }

            frmMapEditor.rsMap.Draw(tmpSprite) '

            DrawThunder = DrawThunder - 1

            tmpSprite.Dispose()
        End If
    End Sub

    Friend Sub DrawWeather()
        Dim i As Integer, SpriteLeft As Integer

        'If InMapEditor Then Exit Sub

        For i = 1 To MAX_WEATHER_PARTICLES
            If WeatherParticle(i).InUse Then
                If WeatherParticle(i).type = WeatherType.Storm Then
                    SpriteLeft = 0
                Else
                    SpriteLeft = WeatherParticle(i).type - 1
                End If
                RenderSprite(WeatherSprite, frmMapEditor.rsMap, WeatherParticle(i).X, WeatherParticle(i).Y, SpriteLeft * 32, 0, 32, 32)
            End If
        Next

    End Sub

    Friend Sub DrawFog()
        Dim fogNum As Integer

        'If InMapEditor Then Exit Sub

        fogNum = CurrentFog
        If fogNum <= 0 OrElse fogNum > NumFogs Then Exit Sub

        Dim horz As Integer = 0
        Dim vert As Integer = 0

        For x = 0 To Map.MaxX + 1
            For y = 0 To Map.MaxY + 1
                If IsValidMapPoint(x, y) Then
                    horz = -x
                    vert = -y
                End If
            Next
        Next

        If FogGFXInfo(fogNum).IsLoaded = False Then
            LoadTexture(fogNum, 8)
        End If

        'seeying we still use it, lets update timer
        With FogGFXInfo(fogNum)
            .TextureTimer = GetTickCount() + 100000
        End With

        Dim tmpSprite As Sprite
        tmpSprite = New Sprite(FogGFX(fogNum)) With {
            .Color = New Color(255, 255, 255, CurrentFogOpacity),
            .TextureRect = New IntRect(0, 0, frmMapEditor.rsMap.Size.Width + 200, frmMapEditor.rsMap.Size.Height + 200),
            .Position = New Vector2f((horz * 2.5) + 50, (vert * 3.5) + 50),
            .Scale = (New Vector2f(CDbl((frmMapEditor.rsMap.Size.Width + 200) / FogGFXInfo(fogNum).width), CDbl((frmMapEditor.rsMap.Size.Height + 200) / FogGFXInfo(fogNum).height)))
        }

        frmMapEditor.rsMap.Draw(tmpSprite) '

    End Sub

    Sub PlayWeatherSound(FileName As String, Optional Looped As Boolean = False)
        If Not Options.Sound = 1 OrElse Not File.Exists(Application.StartupPath & SOUND_PATH & FileName) Then Exit Sub
        If CurWeatherMusic = FileName Then Exit Sub

        dim buffer as SoundBuffer
        If WeatherSoundPlayer Is Nothing Then
            WeatherSoundPlayer = New Sound()
        Else
            WeatherSoundPlayer.Stop()
        End If

        buffer = New SoundBuffer(Application.StartupPath & SOUND_PATH & FileName)
        WeatherSoundPlayer.SoundBuffer = buffer
        WeatherSoundPlayer.Loop() = Looped
        WeatherSoundPlayer.Volume() = MaxVolume
        WeatherSoundPlayer.Play()

        CurWeatherMusic = FileName
    End Sub

    Sub StopWeatherSound()
        If WeatherSoundPlayer Is Nothing Then Exit Sub
        WeatherSoundPlayer.Dispose()
        WeatherSoundPlayer = Nothing

        CurWeatherMusic = ""
    End Sub
End Module