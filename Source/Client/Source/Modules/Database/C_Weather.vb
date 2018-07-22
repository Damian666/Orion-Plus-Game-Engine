Imports System.IO
Imports System.Windows.Forms
Imports SFML.Audio
Imports SFML.Graphics
Imports SFML.Window

Friend Module C_Weather
    Friend Const MaxWeatherParticles As Integer = 100

    Friend WeatherParticle(MaxWeatherParticles) As WeatherParticleRec
    Friend WeatherSoundPlayer As Sound
    Friend CurWeatherMusic As String

    Friend Structure WeatherParticleRec
        Dim Type As Integer
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
                For i = 1 To MaxWeatherParticles
                    If WeatherParticle(i).InUse = 0 Then
                        If Rand(1, 3) = 1 Then
                            WeatherParticle(i).InUse = 1
                            WeatherParticle(i).type = CurrentWeather
                            WeatherParticle(i).Velocity = Rand(8, 14)
                            WeatherParticle(i).X = (TileView.Left * 32) - 32
                            WeatherParticle(i).Y = ((TileView.Top * 32) + Rand(-32, GameWindow.Size.Y))
                        Else
                            WeatherParticle(i).InUse = 1
                            WeatherParticle(i).type = CurrentWeather
                            WeatherParticle(i).Velocity = Rand(10, 15)
                            WeatherParticle(i).X = ((TileView.Left * 32) + Rand(-32, GameWindow.Size.X))
                            WeatherParticle(i).Y = (TileView.Top * 32) - 32
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
        For i = 1 To MaxWeatherParticles
            If WeatherParticle(i).InUse = 1 Then
                If WeatherParticle(i).X > TileView.Right * 32 OrElse WeatherParticle(i).Y > TileView.Bottom * 32 Then
                    WeatherParticle(i).InUse = 0
                Else
                    WeatherParticle(i).X = WeatherParticle(i).X + WeatherParticle(i).Velocity
                    WeatherParticle(i).Y = WeatherParticle(i).Y + WeatherParticle(i).Velocity
                End If
            End If
        Next

    End Sub

    Friend Sub DrawThunderEffect()

        If DrawThunder > 0 Then
            Dim tmpSprite As Sprite
            tmpSprite = New Sprite(New Texture(New SFML.Graphics.Image(GameWindow.Size.X, GameWindow.Size.Y, SFML.Graphics.Color.White))) With {
                .Color = New Color(255, 255, 255, 150),
                .TextureRect = New IntRect(0, 0, GameWindow.Size.X, GameWindow.Size.Y),
                .Position = New Vector2f(0, 0)
            }

            GameWindow.Draw(tmpSprite) '

            DrawThunder = DrawThunder - 1

            tmpSprite.Dispose()
        End If
    End Sub

    Friend Sub DrawWeather()
        Dim i As Integer, spriteLeft As Integer

        For i = 1 To MaxWeatherParticles
            If WeatherParticle(i).InUse Then
                If WeatherParticle(i).type = WeatherType.Storm Then
                    SpriteLeft = 0
                Else
                    SpriteLeft = WeatherParticle(i).type - 1
                End If

                RenderSprite(WeatherSprite, GameWindow, ConvertMapX(WeatherParticle(i).X), ConvertMapY(WeatherParticle(i).Y), SpriteLeft * 32, 0, 32, 32)
            End If
        Next

    End Sub

    Friend Sub DrawFog()
        Dim fogNum As Integer

        fogNum = CurrentFog
        If fogNum <= 0 OrElse fogNum > NumFogs Then Exit Sub

        If FogGFXInfo(fogNum).IsLoaded = False Then
            LoadTexture(fogNum, 8)
        End If

        'seeying we still use it, lets update timer
        With FogGFXInfo(fogNum)
            .TextureTimer = GetTickCount() + 100000
        End With

        FogGFX(fogNum).Repeated = True
        FogGFX(fogNum).Smooth = True

        FogSprite(fogNum).Color = New Color(255, 255, 255, CurrentFogOpacity)
        FogSprite(fogNum).TextureRect = New IntRect(0, 0, GameWindow.Size.X + 200, GameWindow.Size.Y + 200)
        FogSprite(fogNum).Position = New Vector2f((fogOffsetX * 2.5) - 50, (fogOffsetY * 3.5) - 50)
        FogSprite(fogNum).Scale = (New Vector2f(CDbl((GameWindow.Size.X + 200) / FogGFXInfo(fogNum).Width), CDbl((GameWindow.Size.Y + 200) / FogGFXInfo(fogNum).Height)))

        GameWindow.Draw(FogSprite(fogNum))

    End Sub

    Sub PlayWeatherSound(fileName As String, Optional looped As Boolean = False)
        If Not Options.Sound = 1 OrElse Not File.Exists(Application.StartupPath & SoundPath & FileName) Then Exit Sub
        If CurWeatherMusic = FileName Then Exit Sub

        dim buffer as SoundBuffer
        If WeatherSoundPlayer Is Nothing Then
            WeatherSoundPlayer = New Sound()
        Else
            WeatherSoundPlayer.Stop()
        End If

        buffer = New SoundBuffer(Application.StartupPath & SoundPath & FileName)
        WeatherSoundPlayer.SoundBuffer = buffer
        If Looped = True Then
            WeatherSoundPlayer.Loop() = True
        Else
            WeatherSoundPlayer.Loop() = False
        End If
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