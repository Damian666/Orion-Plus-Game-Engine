Imports System.IO
Imports Asfw
Imports Asfw.IO

Friend Module S_Animations

#Region "Database"

    Sub SaveAnimations()
        Dim i As Integer

        For i = 1 To MAX_ANIMATIONS
            SaveAnimation(i)
        Next

    End Sub

    Sub SaveAnimation(AnimationNum As Integer)
        Dim filename As String
        Dim x As Integer

        filename = Path.Animation(AnimationNum)

        Dim writer As New ByteStream(100)

        writer.WriteString(Animation(AnimationNum).Name)
        writer.WriteString(Animation(AnimationNum).Sound)

        For x = 0 To UBound(Animation(AnimationNum).Sprite)
            writer.WriteInt32(Animation(AnimationNum).Sprite(x))
        Next

        For x = 0 To UBound(Animation(AnimationNum).Frames)
            writer.WriteInt32(Animation(AnimationNum).Frames(x))
        Next

        For x = 0 To UBound(Animation(AnimationNum).LoopCount)
            writer.WriteInt32(Animation(AnimationNum).LoopCount(x))
        Next

        For x = 0 To UBound(Animation(AnimationNum).LoopTime)
            writer.WriteInt32(Animation(AnimationNum).LoopTime(x))
        Next

        ByteFile.Save(filename, writer)
    End Sub

    Sub LoadAnimations()
        Dim i As Integer

        CheckAnimations()

        For i = 1 To MAX_ANIMATIONS
            LoadAnimation(i)
        Next

    End Sub

    Sub LoadAnimation(AnimationNum As Integer)
        Dim filename As String

        filename = Path.Animation(AnimationNum)
        Dim reader As New ByteStream()
        ByteFile.Load(filename, reader)

        Animation(AnimationNum).Name = reader.ReadString()
        Animation(AnimationNum).Sound = reader.ReadString()

        For x = 0 To UBound(Animation(AnimationNum).Sprite)
            Animation(AnimationNum).Sprite(x) = reader.ReadInt32()
        Next

        For x = 0 To UBound(Animation(AnimationNum).Frames)
            Animation(AnimationNum).Frames(x) = reader.ReadInt32()
        Next

        For x = 0 To UBound(Animation(AnimationNum).LoopCount)
            Animation(AnimationNum).LoopCount(x) = reader.ReadInt32()
        Next

        For x = 0 To UBound(Animation(AnimationNum).LoopTime)
            Animation(AnimationNum).LoopTime(x) = reader.ReadInt32()
        Next

        If Animation(AnimationNum).Name Is Nothing Then Animation(AnimationNum).Name = ""
    End Sub

    Sub CheckAnimations()
        Dim i As Integer

        For i = 1 To MAX_ANIMATIONS

            If Not File.Exists(Path.Animation(i)) Then
                SaveAnimation(i)
                Application.DoEvents()
            End If

        Next
    End Sub

    Sub ClearAnimation(index As Integer)
        Animation(index) = Nothing
        Animation(index).Name = ""
        Animation(index).Sound = ""
        ReDim Animation(index).Sprite(1)
        ReDim Animation(index).Frames(1)
        ReDim Animation(index).LoopCount(1)
        ReDim Animation(index).LoopTime(1)
    End Sub

    Sub ClearAnimations()
        For i = 1 To MAX_ANIMATIONS
            ClearAnimation(i)
        Next
    End Sub

    Function AnimationsData() As Byte()
        Dim buffer As New ByteStream(4)

        For i = 1 To MAX_ANIMATIONS
            If Not Len(Trim$(Animation(i).Name)) > 0 Then Continue For
            buffer.WriteBlock(AnimationData(i))
        Next

        Return buffer.ToArray
    End Function

    Function AnimationData(AnimationNum As Integer) As Byte()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(AnimationNum)
        For i = 0 To UBound(Animation(AnimationNum).Frames)
            buffer.WriteInt32(Animation(AnimationNum).Frames(i))
        Next

        For i = 0 To UBound(Animation(AnimationNum).LoopCount)
            buffer.WriteInt32(Animation(AnimationNum).LoopCount(i))
        Next

        For i = 0 To UBound(Animation(AnimationNum).LoopTime)
            buffer.WriteInt32(Animation(AnimationNum).LoopTime(i))
        Next

        buffer.WriteString((Animation(AnimationNum).Name))
        buffer.WriteString((Animation(AnimationNum).Sound))

        For i = 0 To UBound(Animation(AnimationNum).Sprite)
            buffer.WriteInt32(Animation(AnimationNum).Sprite(i))
        Next

        Return buffer.ToArray
    End Function

#End Region

#Region "Incoming Packets"

    Sub Packet_EditAnimation(index As Integer, ByRef data() As Byte)
        AddDebug("Recieved EMSG: RequestEditAnimation")

        ' Prevent hacking
        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        Dim Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SAnimationEditor)
        Socket.SendDataTo(index, Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Sub Packet_SaveAnimation(index As Integer, ByRef data() As Byte)
        Dim AnimNum As Integer
        Dim buffer As New ByteStream(data)

        AddDebug("Recieved EMSG: SaveAnimation")

        AnimNum = buffer.ReadInt32

        ' Update the Animation
        For i = 0 To UBound(Animation(AnimNum).Frames)
            Animation(AnimNum).Frames(i) = buffer.ReadInt32()
        Next

        For i = 0 To UBound(Animation(AnimNum).LoopCount)
            Animation(AnimNum).LoopCount(i) = buffer.ReadInt32()
        Next

        For i = 0 To UBound(Animation(AnimNum).LoopTime)
            Animation(AnimNum).LoopTime(i) = buffer.ReadInt32()
        Next

        Animation(AnimNum).Name = buffer.ReadString()
        Animation(AnimNum).Sound = buffer.ReadString()

        If Animation(AnimNum).Name Is Nothing Then Animation(AnimNum).Name = ""
        If Animation(AnimNum).Sound Is Nothing Then Animation(AnimNum).Sound = ""

        For i = 0 To UBound(Animation(AnimNum).Sprite)
            Animation(AnimNum).Sprite(i) = buffer.ReadInt32()
        Next

        buffer.Dispose()

        ' Save it
        SaveAnimation(AnimNum)
        SendUpdateAnimationToAll(AnimNum)
        Addlog(GetPlayerLogin(index) & " saved Animation #" & AnimNum & ".", ADMIN_LOG)

    End Sub

    Sub Packet_RequestAnimations(index As Integer, ByRef data() As Byte)
        AddDebug("Recieved CMSG: CRequestAnimations")

        SendAnimations(index)
    End Sub

#End Region

#Region "Outgoing Packets"

    Sub SendAnimation(mapNum As Integer, Anim As Integer, X As Integer, Y As Integer, Optional LockType As Byte = 0, Optional Lockindex As Integer = 0)
        Dim buffer As New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SAnimation)
        buffer.WriteInt32(Anim)
        buffer.WriteInt32(X)
        buffer.WriteInt32(Y)
        buffer.WriteInt32(LockType)
        buffer.WriteInt32(Lockindex)

        AddDebug("Sent SMSG: SAnimation")

        SendDataToMap(mapNum, buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Sub SendAnimations(index As Integer)
        Dim i As Integer

        For i = 1 To MAX_ANIMATIONS

            If Len(Trim$(Animation(i).Name)) > 0 Then
                SendUpdateAnimationTo(index, i)
            End If

        Next

    End Sub

    Sub SendUpdateAnimationTo(index As Integer, AnimationNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SUpdateAnimation)

        buffer.WriteBlock(AnimationData(AnimationNum))

        'buffer.WriteInt32(AnimationNum)

        'For i = 0 To UBound(Animation(AnimationNum).Frames)
        '    buffer.WriteInt32(Animation(AnimationNum).Frames(i))
        'Next

        'For i = 0 To UBound(Animation(AnimationNum).LoopCount)
        '    buffer.WriteInt32(Animation(AnimationNum).LoopCount(i))
        'Next

        'For i = 0 To UBound(Animation(AnimationNum).LoopTime)
        '    buffer.WriteInt32(Animation(AnimationNum).LoopTime(i))
        'Next

        'buffer.WriteString((Animation(AnimationNum).Name))
        'buffer.WriteString((Animation(AnimationNum).Sound))

        'For i = 0 To UBound(Animation(AnimationNum).Sprite)
        '    buffer.WriteInt32(Animation(AnimationNum).Sprite(i))
        'Next

        AddDebug("Sent SMSG: SUpdateAnimation")

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendUpdateAnimationToAll(AnimationNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SUpdateAnimation)

        buffer.WriteBlock(AnimationData(AnimationNum))

        'buffer.WriteInt32(AnimationNum)

        'For i = 0 To UBound(Animation(AnimationNum).Frames)
        '    buffer.WriteInt32(Animation(AnimationNum).Frames(i))
        'Next

        'For i = 0 To UBound(Animation(AnimationNum).LoopCount)
        '    buffer.WriteInt32(Animation(AnimationNum).LoopCount(i))
        'Next

        'For i = 0 To UBound(Animation(AnimationNum).LoopTime)
        '    buffer.WriteInt32(Animation(AnimationNum).LoopTime(i))
        'Next

        'buffer.WriteString((Animation(AnimationNum).Name))
        'buffer.WriteString((Animation(AnimationNum).Sound))

        'For i = 0 To UBound(Animation(AnimationNum).Sprite)
        '    buffer.WriteInt32(Animation(AnimationNum).Sprite(i))
        'Next

        AddDebug("Sent SMSG: SUpdateAnimation To All")

        SendDataToAll(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

#End Region

End Module