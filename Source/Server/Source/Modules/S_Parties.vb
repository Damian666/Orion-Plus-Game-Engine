Imports Asfw

Module S_Parties

#Region "Types and Globals"

    Friend Party(MAX_PARTIES) As PartyRec

    Friend Structure PartyRec
        Dim Leader As Integer
        Dim Member() As Integer
        Dim MemberCount As Integer
    End Structure

#End Region

#Region "Outgoing Packets"

    Sub SendDataToParty(partyNum As Integer, ByRef data() As Byte)
        Dim i As Integer

        For i = 1 To Party(partyNum).MemberCount
            If Party(partyNum).Member(i) > 0 Then
                Socket.SendDataTo(Party(partyNum).Member(i), data, data.Length)
            End If
        Next
    End Sub

    Sub SendPartyInvite(index As Integer, target As Integer)
        Dim buffer As New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SPartyInvite)

        Addlog("Sent SMSG: SPartyInvite", PACKET_LOG)
        Console.WriteLine("Sent SMSG: SPartyInvite")

        buffer.WriteString((Trim$(Player(target).Character(TempPlayer(target).CurChar).Name)))

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendPartyUpdate(partyNum As Integer)
        Dim buffer As New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SPartyUpdate)

        Addlog("Sent SMSG: SPartyUpdate", PACKET_LOG)
        Console.WriteLine("Sent SMSG: SPartyUpdate")

        buffer.WriteInt32(1)
        buffer.WriteInt32(Party(partyNum).Leader)
        For i = 1 To MAX_PARTY_MEMBERS
            buffer.WriteInt32(Party(partyNum).Member(i))
        Next
        buffer.WriteInt32(Party(partyNum).MemberCount)

        SendDataToParty(partyNum, buffer.ToArray())
        buffer.Dispose()
    End Sub

    Sub SendPartyUpdateTo(index As Integer)
        Dim buffer As New ByteStream(4), i As Integer, partyNum As Integer

        buffer.WriteInt32(ServerPackets.SPartyUpdate)

        Addlog("Sent SMSG: SPartyUpdate To Players", PACKET_LOG)
        Console.WriteLine("Sent SMSG: SPartyUpdate To Players")

        ' check if we're in a party
        partyNum = TempPlayer(index).InParty
        If partyNum > 0 Then
            ' send party data
            buffer.WriteInt32(1)
            buffer.WriteInt32(Party(partyNum).Leader)
            For i = 1 To MAX_PARTY_MEMBERS
                buffer.WriteInt32(Party(partyNum).Member(i))
            Next
            buffer.WriteInt32(Party(partyNum).MemberCount)
        Else
            ' send clear command
            buffer.WriteInt32(0)
        End If

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendPartyVitals(partyNum As Integer, index As Integer)
        Dim buffer As ByteStream, i As Integer

        buffer = New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SPartyVitals)
        buffer.WriteInt32(index)

        Addlog("Sent SMSG: SPartyVitals", PACKET_LOG)
        Console.WriteLine("Sent SMSG: SPartyVitals")

        For i = 1 To VitalType.Count - 1
            buffer.WriteInt32(GetPlayerMaxVital(index, i))
            buffer.WriteInt32(Player(index).Character(TempPlayer(index).CurChar).Vital(i))
        Next

        SendDataToParty(partyNum, buffer.ToArray)
        buffer.Dispose()
    End Sub

#End Region

#Region "Incoming Packets"

    Friend Sub Packet_PartyRquest(index As Integer, ByRef data() As Byte)
        Addlog("Recieved CMSG: CRequestParty", PACKET_LOG)
        Console.WriteLine("Recieved CMSG: CRequestParty")

        ' Prevent partying with self
        If TempPlayer(index).Target = index Then Exit Sub
        ' make sure it's a valid target
        If TempPlayer(index).TargetType <> TargetType.Player Then Exit Sub

        ' make sure they're connected and on the same map
        If Not Socket.IsConnected(TempPlayer(index).Target) OrElse Not IsPlaying(TempPlayer(index).Target) Then Exit Sub
        If GetPlayerMap(TempPlayer(index).Target) <> GetPlayerMap(index) Then Exit Sub

        ' init the request
        Party_Invite(index, TempPlayer(index).Target)
    End Sub

    Friend Sub Packet_AcceptParty(index As Integer, ByRef data() As Byte)
        Addlog("Recieved CMSG: CAcceptParty", PACKET_LOG)
        Console.WriteLine("Recieved CMSG: CAcceptParty")

        Party_InviteAccept(TempPlayer(index).PartyInvite, index)
    End Sub

    Friend Sub Packet_DeclineParty(index As Integer, ByRef data() As Byte)
        Addlog("Recieved CMSG: CDeclineParty", PACKET_LOG)
        Console.WriteLine("Recieved CMSG: CDeclineParty")

        Party_InviteDecline(TempPlayer(index).PartyInvite, index)
    End Sub

    Friend Sub Packet_LeaveParty(index As Integer, ByRef data() As Byte)
        Addlog("Recieved CMSG: CLeaveParty", PACKET_LOG)
        Console.WriteLine("Recieved CMSG: CLeaveParty")

        Party_PlayerLeave(index)
    End Sub

    Friend Sub Packet_PartyChatMsg(index As Integer, ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        Addlog("Recieved CMSG: CPartyChatMsg", PACKET_LOG)
        Console.WriteLine("Recieved CMSG: CPartyChatMsg")

        PartyMsg(index, buffer.ReadString)

        buffer.Dispose()
    End Sub

#End Region

    Sub ClearParties()
        Dim i As Integer

        For i = 1 To MAX_PARTIES
            ClearParty(i)
        Next

    End Sub

    Sub ClearParty(partyNum As Integer)
        Party(partyNum) = Nothing
        Party(partyNum).Leader = 0
        Party(partyNum).MemberCount = 0
        ReDim Party(partyNum).Member(MAX_PARTY_MEMBERS)
    End Sub

    Friend Sub PartyMsg(partyNum As Integer, msg As String)
        Dim i As Integer

        ' send message to all people
        For i = 1 To MAX_PARTY_MEMBERS
            ' exist?
            If Party(partyNum).Member(i) > 0 Then
                ' make sure they're logged on
                If Socket.IsConnected(Party(partyNum).Member(i)) AndAlso IsPlaying(Party(partyNum).Member(i)) Then
                    PlayerMsg(Party(partyNum).Member(i), msg, ColorType.BrightBlue)
                End If
            End If
        Next
    End Sub

    Private Sub Party_RemoveFromParty(index As Integer, partyNum As Integer)
        For i = 1 To MAX_PARTY_MEMBERS
            If Party(partyNum).Member(i) = index Then
                Party(partyNum).Member(i) = 0
                TempPlayer(index).InParty = 0
                TempPlayer(index).PartyInvite = 0
                Exit For
            End If
        Next
        Party_CountMembers(partyNum)
        SendPartyUpdate(partyNum)
        SendPartyUpdateTo(index)
    End Sub

    Friend Sub Party_PlayerLeave(index As Integer)
        Dim partyNum As Integer, i As Integer

        partyNum = TempPlayer(index).InParty

        If partyNum > 0 Then
            ' find out how many members we have
            Party_CountMembers(partyNum)
            ' make sure there's more than 2 people
            If Party(partyNum).MemberCount > 2 Then

                ' check if leader
                If Party(partyNum).Leader = index Then
                    ' set next person down as leader
                    For i = 1 To MAX_PARTY_MEMBERS
                        If Party(partyNum).Member(i) > 0 AndAlso Party(partyNum).Member(i) <> index Then
                            Party(partyNum).Leader = Party(partyNum).Member(i)
                            PartyMsg(partyNum, String.Format("{0} is now the party leader.", GetPlayerName(i)))
                            Exit For
                        End If
                    Next
                    ' leave party
                    PartyMsg(partyNum, String.Format("{0} has left the party.", GetPlayerName(index)))
                    Party_RemoveFromParty(index, partyNum)
                Else
                    ' not the leader, just leave
                    PartyMsg(partyNum, String.Format("{0} has left the party.", GetPlayerName(index)))
                    Party_RemoveFromParty(index, partyNum)
                End If
            Else
                ' find out how many members we have
                Party_CountMembers(partyNum)
                ' only 2 people, disband
                PartyMsg(partyNum, "The party has been disbanded.")

                ' clear out everyone's party
                For i = 1 To MAX_PARTY_MEMBERS
                    index = Party(partyNum).Member(i)
                    ' player exist?
                    If index > 0 Then
                        Party_RemoveFromParty(index, partyNum)
                    End If
                Next
                ' clear out the party itself
                ClearParty(partyNum)
            End If
        End If
    End Sub

    Friend Sub Party_Invite(index As Integer, target As Integer)
        Dim partyNum As Integer, i As Integer

        ' check if the person is a valid target
        If Not Socket.IsConnected(target) OrElse Not IsPlaying(target) Then Exit Sub

        ' make sure they're not busy
        If TempPlayer(target).PartyInvite > 0 OrElse TempPlayer(target).TradeRequest > 0 Then
            ' they've already got a request for trade/party
            PlayerMsg(index, "This player is busy.", ColorType.BrightRed)
            ' exit out early
            Exit Sub
        End If
        ' make syure they're not in a party
        If TempPlayer(target).InParty > 0 Then
            ' they're already in a party
            PlayerMsg(index, "This player is already in a party.", ColorType.BrightRed)
            'exit out early
            Exit Sub
        End If

        ' check if we're in a party
        If TempPlayer(index).InParty > 0 Then
            partyNum = TempPlayer(index).InParty
            ' make sure we're the leader
            If Party(partyNum).Leader = index Then
                ' got a blank slot?
                For i = 1 To MAX_PARTY_MEMBERS
                    If Party(partyNum).Member(i) = 0 Then
                        ' send the invitation
                        SendPartyInvite(target, index)
                        ' set the invite target
                        TempPlayer(target).PartyInvite = index
                        ' let them know
                        PlayerMsg(index, "Invitation sent.", ColorType.Yellow)
                        Exit Sub
                    End If
                Next
                ' no room
                PlayerMsg(index, "Party is full.", ColorType.BrightRed)
                Exit Sub
            Else
                ' not the leader
                PlayerMsg(index, "You are not the party leader.", ColorType.BrightRed)
                Exit Sub
            End If
        Else
            ' not in a party - doesn't matter!
            SendPartyInvite(target, index)
            ' set the invite target
            TempPlayer(target).PartyInvite = index
            ' let them know
            PlayerMsg(index, "Invitation sent.", ColorType.Yellow)
            Exit Sub
        End If
    End Sub

    Friend Sub Party_InviteAccept(index As Integer, target As Integer)
        Dim partyNum As Integer, i As Integer

        ' check if already in a party
        If TempPlayer(index).InParty > 0 Then
            ' get the partynumber
            partyNum = TempPlayer(index).InParty
            ' got a blank slot?
            For i = 1 To MAX_PARTY_MEMBERS
                If Party(partyNum).Member(i) = 0 Then
                    'add to the party
                    Party(partyNum).Member(i) = target
                    ' recount party
                    Party_CountMembers(partyNum)
                    ' send update to all - including new player
                    SendPartyUpdate(partyNum)
                    SendPartyVitals(partyNum, target)
                    ' let everyone know they've joined
                    PartyMsg(partyNum, String.Format("{0} has joined the party.", GetPlayerName(target)))
                    ' add them in
                    TempPlayer(target).InParty = partyNum
                    Exit Sub
                End If
            Next
            ' no empty slots - let them know
            PlayerMsg(index, "Party is full.", ColorType.BrightRed)
            PlayerMsg(target, "Party is full.", ColorType.BrightRed)
            Exit Sub
        Else
            ' not in a party. Create one with the new person.
            For i = 1 To MAX_PARTIES
                ' find blank party
                If Not Party(i).Leader > 0 Then
                    partyNum = i
                    Exit For
                End If
            Next
            ' create the party
            Party(partyNum).MemberCount = 2
            Party(partyNum).Leader = index
            Party(partyNum).Member(1) = index
            Party(partyNum).Member(2) = target
            SendPartyUpdate(partyNum)
            SendPartyVitals(partyNum, index)
            SendPartyVitals(partyNum, target)

            ' let them know it's created
            PartyMsg(partyNum, "Party created.")
            PartyMsg(partyNum, String.Format("{0} has joined the party.", GetPlayerName(index)))

            ' clear the invitation
            TempPlayer(target).PartyInvite = 0

            ' add them to the party
            TempPlayer(index).InParty = partyNum
            TempPlayer(target).InParty = partyNum
            Exit Sub
        End If
    End Sub

    Friend Sub Party_InviteDecline(index As Integer, target As Integer)
        PlayerMsg(index, String.Format("{0} has declined to join your party.", GetPlayerName(target)), ColorType.BrightRed)
        PlayerMsg(target, "You declined to join the party.", ColorType.Yellow)
        ' clear the invitation
        TempPlayer(target).PartyInvite = 0
    End Sub

    Friend Sub Party_CountMembers(partyNum As Integer)
        Dim i As Integer, highindex As Integer, x As Integer

        ' find the high index
        For i = MAX_PARTY_MEMBERS To 1 Step -1
            If Party(partyNum).Member(i) > 0 Then
                highindex = i
                Exit For
            End If
        Next
        ' count the members
        For i = 1 To MAX_PARTY_MEMBERS
            ' we've got a blank member
            If Party(partyNum).Member(i) = 0 Then
                ' is it lower than the high index?
                If i < highindex Then
                    ' move everyone down a slot
                    For x = i To MAX_PARTY_MEMBERS - 1
                        Party(partyNum).Member(x) = Party(partyNum).Member(x + 1)
                        Party(partyNum).Member(x + 1) = 0
                    Next
                Else
                    ' not lower - highindex is count
                    Party(partyNum).MemberCount = highindex
                    Exit Sub
                End If
            End If
            ' check if we've reached the max
            If i = MAX_PARTY_MEMBERS Then
                If highindex = i Then
                    Party(partyNum).MemberCount = MAX_PARTY_MEMBERS
                    Exit Sub
                End If
            End If
        Next
        ' if we're here it means that we need to re-count again
        Party_CountMembers(partyNum)
    End Sub

    Friend Sub Party_ShareExp(partyNum As Integer, exp As Integer, index As Integer, mapNum As Integer)
        Dim expShare As Integer, leftOver As Integer, i As Integer, tmpindex As Integer, loseMemberCount As Byte

        ' check if it's worth sharing
        If Not exp >= Party(partyNum).MemberCount Then
            ' no party - keep exp for self
            GivePlayerExp(index, exp)
            Exit Sub
        End If

        ' check members in others maps
        For i = 1 To MAX_PARTY_MEMBERS
            tmpindex = Party(partyNum).Member(i)
            If tmpindex > 0 Then
                If Socket.IsConnected(tmpindex) AndAlso IsPlaying(tmpindex) Then
                    If GetPlayerMap(tmpindex) <> mapNum Then
                        loseMemberCount = loseMemberCount + 1
                    End If
                End If
            End If
        Next

        ' find out the equal share
        expShare = exp \ (Party(partyNum).MemberCount - loseMemberCount)
        leftOver = exp Mod (Party(partyNum).MemberCount - loseMemberCount)

        ' loop through and give everyone exp
        For i = 1 To MAX_PARTY_MEMBERS
            tmpindex = Party(partyNum).Member(i)
            ' existing member?
            If tmpindex > 0 Then
                ' playing?
                If Socket.IsConnected(tmpindex) AndAlso IsPlaying(tmpindex) Then
                    If GetPlayerMap(tmpindex) = mapNum Then
                        ' give them their share
                        GivePlayerExp(tmpindex, expShare)
                    End If
                End If
            End If
        Next

        ' give the remainder to a random member
        If Not leftOver = 0 Then
            tmpindex = Party(partyNum).Member(Random(1, Party(partyNum).MemberCount))
            ' give the exp
            GivePlayerExp(tmpindex, leftOver)
        End If

    End Sub

    Sub PartyWarp(index As Integer, mapNum As Integer, x As Integer, y As Integer)
        Dim i As Integer

        If TempPlayer(index).InParty Then
            If Party(TempPlayer(index).InParty).Leader Then
                For i = 1 To Party(TempPlayer(index).InParty).MemberCount
                    PlayerWarp(Party(TempPlayer(index).InParty).Member(i), mapNum, x, y)
                Next
            End If
        End If

    End Sub

    Friend Function IsPlayerInParty(index As Integer) As Boolean
        If index < 0 OrElse index > MAX_PLAYERS OrElse Not TempPlayer(index).InGame Then Exit Function
        If TempPlayer(index).InParty > 0 Then IsPlayerInParty = True
    End Function

    Friend Function GetPlayerParty(index As Integer) As Integer
        If index < 0 OrElse index > MAX_PLAYERS OrElse Not TempPlayer(index).InGame Then Exit Function
        GetPlayerParty = TempPlayer(index).InParty
    End Function

End Module