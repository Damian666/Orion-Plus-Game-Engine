Imports System.IO
Imports System.Windows.Forms
Imports SFML.Audio
Imports SFML.Graphics
Imports SFML.Window

Friend Module C_Weather

#Region "Types and Globals"

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

    Friend FogOffsetX As Integer
    Friend FogOffsetY As Integer

    Friend CurrentWeather As Integer
    Friend CurrentWeatherIntensity As Integer
    Friend CurrentFog As Integer
    Friend CurrentFogSpeed As Integer
    Friend CurrentFogOpacity As Integer
    Friend CurrentTintR As Integer
    Friend CurrentTintG As Integer
    Friend CurrentTintB As Integer
    Friend CurrentTintA As Integer
    Friend DrawThunder As Integer

#End Region

#Region "Drawing"

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
                If WeatherParticle(i).Type = WeatherType.Storm Then
                    spriteLeft = 0
                Else
                    spriteLeft = WeatherParticle(i).Type - 1
                End If

                RenderSprite(WeatherSprite, GameWindow, ConvertMapX(WeatherParticle(i).X), ConvertMapY(WeatherParticle(i).Y), spriteLeft * 32, 0, 32, 32)
            End If
        Next

    End Sub

    Friend Sub DrawFog()
        Dim fogNum As Integer

        fogNum = CurrentFog
        If fogNum <= 0 OrElse fogNum > NumFogs Then Exit Sub

        If FogGfxInfo(fogNum).IsLoaded = False Then
            LoadTexture(fogNum, 8)
        End If

        'seeying we still use it, lets update timer
        With FogGfxInfo(fogNum)
            .TextureTimer = GetTickCount() + 100000
        End With

        FogGfx(fogNum).Repeated = True
        FogGfx(fogNum).Smooth = True

        FogSprite(fogNum).Color = New Color(255, 255, 255, CurrentFogOpacity)
        FogSprite(fogNum).TextureRect = New IntRect(0, 0, GameWindow.Size.X + 200, GameWindow.Size.Y + 200)
        FogSprite(fogNum).Position = New Vector2f((FogOffsetX * 2.5) - 50, (FogOffsetY * 3.5) - 50)
        FogSprite(fogNum).Scale = (New Vector2f(CDbl((GameWindow.Size.X + 200) / FogGfxInfo(fogNum).Width), CDbl((GameWindow.Size.Y + 200) / FogGfxInfo(fogNum).Height)))

        GameWindow.Draw(FogSprite(fogNum))

    End Sub

#End Region

#Region "Functions"

    Sub ProcessWeather()
        Dim i As Integer, x As Integer

        If CurrentWeather > 0 And CurrentWeather < WeatherType.Fog Then
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
                            WeatherParticle(i).Type = CurrentWeather
                            WeatherParticle(i).Velocity = Rand(8, 14)
                            WeatherParticle(i).X = (TileView.Left * 32) - 32
                            WeatherParticle(i).Y = ((TileView.Top * 32) + Rand(-32, GameWindow.Size.Y))
                        Else
                            WeatherParticle(i).InUse = 1
                            WeatherParticle(i).Type = CurrentWeather
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

#End Region

#Region "Sound"

    Sub PlayWeatherSound(fileName As String, Optional looped As Boolean = False)
        If Not Settings.Sound = 1 OrElse Not File.Exists(Path.Sounds & fileName) Then Exit Sub
        If CurWeatherMusic = fileName Then Exit Sub

        Dim buffer As SoundBuffer
        If WeatherSoundPlayer Is Nothing Then
            WeatherSoundPlayer = New Sound()
        Else
            WeatherSoundPlayer.Stop()
        End If

        buffer = New SoundBuffer(Path.Sounds & fileName)
        WeatherSoundPlayer.SoundBuffer = buffer
        If looped = True Then
            WeatherSoundPlayer.Loop() = True
        Else
            WeatherSoundPlayer.Loop() = False
        End If
        WeatherSoundPlayer.Volume() = MaxVolume
        WeatherSoundPlayer.Play()

        CurWeatherMusic = fileName
    End Sub

    Sub StopWeatherSound()
        If WeatherSoundPlayer Is Nothing Then Exit Sub
        WeatherSoundPlayer.Dispose()
        WeatherSoundPlayer = Nothing

        CurWeatherMusic = ""
    End Sub

#End Region

End Module