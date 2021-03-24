Imports Asfw

Friend Module C_Time

    Sub Packet_Clock(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        Time.Instance.GameSpeed = buffer.ReadInt32()
        Time.Instance.Time = New Date(BitConverter.ToInt64(buffer.ReadBytes(), 0))

        buffer.Dispose()
    End Sub

    Sub Packet_Time(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)

        Time.Instance.TimeOfDay = buffer.ReadByte

        Select Case Time.Instance.TimeOfDay
            Case TimeOfDay.Dawn
                AddText("A chilling, refreshing, breeze has come with the morning.", ColorType.BrightBlue)
                Exit Select

            Case TimeOfDay.Day
                AddText("Day has dawned in this region.", ColorType.Yellow)
                Exit Select

            Case TimeOfDay.Dusk
                AddText("Dusk has begun darkening the skies...", ColorType.BrightRed)
                Exit Select

            Case Else
                AddText("Night has fallen upon the weary travelers.", ColorType.DarkGray)
                Exit Select
        End Select

        buffer.Dispose()
    End Sub

End Module