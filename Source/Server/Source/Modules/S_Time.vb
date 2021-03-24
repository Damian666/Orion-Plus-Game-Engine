Imports Asfw

Friend Module modTime

    Sub InitTime()
        ' Add handlers to time events
        AddHandler Time.Instance.OnTimeChanged, AddressOf HandleTimeChanged
        AddHandler Time.Instance.OnTimeOfDayChanged, AddressOf HandleTimeOfDayChanged
        AddHandler Time.Instance.OnTimeSync, AddressOf HandleTimeSync

        ' Prepare the time instance
        Time.Instance.Time = Date.Now
        Time.Instance.GameSpeed = 1
    End Sub

    Sub HandleTimeChanged(ByRef source As Time)
        UpdateCaption()
    End Sub

    Sub HandleTimeOfDayChanged(ByRef source As Time)
        SendTimeToAll()
        ClearAllMapNpcs()
        SpawnAllMapNpcs()
    End Sub

    Sub HandleTimeSync(ByRef source As Time)
        SendGameClockToAll()
    End Sub

    Sub SendGameClockTo(index As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SClock)
        buffer.WriteInt32(Time.Instance.GameSpeed)
        buffer.WriteBytes(BitConverter.GetBytes(Time.Instance.Time.Ticks))
        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        AddDebug("Sent SMSG: SClock")

        AddDebug(" Player: " & GetPlayerName(index) & " : " & " GameSpeed: " & Time.Instance.GameSpeed & " Instance Time Ticks: " & Time.Instance.Time.Ticks)

        buffer.Dispose()
    End Sub

    Sub SendGameClockToAll()
        Dim I As Integer

        For I = 1 To GetPlayersOnline()
            If IsPlaying(I) Then
                SendGameClockTo(I)
            End If
        Next
    End Sub

    Sub SendTimeTo(index As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.STime)
        buffer.WriteByte(Time.Instance.TimeOfDay)
        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        AddDebug("Sent SMSG: STime")

        AddDebug(" Player: " & GetPlayerName(index) & " : " & " Time Of Day: " & Time.Instance.TimeOfDay)

        buffer.Dispose()
    End Sub

    Sub SendTimeToAll()
        Dim I As Integer

        For I = 1 To GetPlayersOnline()
            If IsPlaying(I) Then
                SendTimeTo(I)
            End If
        Next

    End Sub

End Module