Imports System.Drawing
Imports Asfw
Imports SFML.Graphics
Imports SFML.Window

Module C_Parties

#Region "Types and Globals"

    Friend Party As PartyRec

    Friend Structure PartyRec
        Dim Leader As Integer
        Dim Member() As Integer
        Dim MemberCount As Integer
    End Structure

#End Region

#Region "Database"

    Sub ClearParty()
        Party = New PartyRec With {
            .Leader = 0,
            .MemberCount = 0
        }
        ReDim Party.Member(MAX_PARTY_MEMBERS)
    End Sub

#End Region

#Region "Incoming Packets"

    Sub Packet_PartyInvite(ByRef data() As Byte)
        Dim name As String
        Dim buffer As New ByteStream(data)
        name = buffer.ReadString

        DialogType = DialogueTypeParty

        DialogMsg1 = "Party Invite"
        DialogMsg2 = Trim$(name) & " has invited you to a party. Would you like to join?"

        UpdateDialog = True

        buffer.Dispose()
    End Sub

    Sub Packet_PartyUpdate(ByRef data() As Byte)
        Dim I As Integer, inParty As Integer
        Dim buffer As New ByteStream(data)
        inParty = buffer.ReadInt32

        ' exit out if we're not in a party
        If inParty = 0 Then
            ClearParty()
            ' exit out early
            buffer.Dispose()
            Exit Sub
        End If

        ' carry on otherwise
        Party.Leader = buffer.ReadInt32
        For I = 1 To MAX_PARTY_MEMBERS
            Party.Member(I) = buffer.ReadInt32
        Next
        Party.MemberCount = buffer.ReadInt32

        buffer.Dispose()
    End Sub

    Sub Packet_PartyVitals(ByRef data() As Byte)
        Dim playerNum As Integer, partyindex As Integer
        Dim buffer As New ByteStream(data)
        ' which player?
        playerNum = buffer.ReadInt32

        ' find the party number
        For I = 1 To MAX_PARTY_MEMBERS
            If Party.Member(I) = playerNum Then
                partyindex = I
            End If
        Next

        ' exit out if wrong data
        If partyindex <= 0 OrElse partyindex > MAX_PARTY_MEMBERS Then Exit Sub

        ' set vitals
        Player(playerNum).MaxHp = buffer.ReadInt32
        Player(playerNum).Vital(VitalType.HP) = buffer.ReadInt32

        Player(playerNum).MaxMp = buffer.ReadInt32
        Player(playerNum).Vital(VitalType.MP) = buffer.ReadInt32

        Player(playerNum).MaxSp = buffer.ReadInt32
        Player(playerNum).Vital(VitalType.SP) = buffer.ReadInt32

        buffer.Dispose()
    End Sub

#End Region

#Region "Outgoing Packets"

    Friend Sub SendPartyRequest(name As String)
        Dim buffer As New ByteStream(4)
        buffer.WriteInt32(ClientPackets.CRequestParty)
        buffer.WriteString((name))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendAcceptParty()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CAcceptParty)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendDeclineParty()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CDeclineParty)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendLeaveParty()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CLeaveParty)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendPartyChatMsg(text As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CPartyChatMsg)
        buffer.WriteString((text))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

#End Region

#Region "Drawing"

    Friend Sub DrawParty()
        Dim I As Integer, x As Integer, y As Integer, barwidth As Integer, playerNum As Integer, theName As String
        Dim rec(1) As Rectangle

        ' render the window

        ' draw the bars
        If Party.Leader > 0 Then ' make sure we're in a party
            ' draw leader
            playerNum = Party.Leader
            ' name
            theName = Trim$(GetPlayerName(playerNum))
            ' draw name
            y = 100
            x = 10
            DrawText(x, y, theName, SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)

            ' draw hp
            If Player(playerNum).Vital(VitalType.HP) > 0 Then
                ' calculate the width to fill
                barwidth = ((Player(playerNum).Vital(VitalType.HP) / (GetPlayerMaxVital(playerNum, VitalType.HP)) * 64))
                ' draw bars
                rec(1) = New Rectangle(x, y, barwidth, 6)
                Dim rectShape As New RectangleShape(New Vector2f(barwidth, 6)) With {
                    .Position = New Vector2f(x, y + 15),
                    .FillColor = SFML.Graphics.Color.Red
                }
                GameWindow.Draw(rectShape)
            End If
            ' draw mp
            If Player(playerNum).Vital(VitalType.MP) > 0 Then
                ' calculate the width to fill
                barwidth = ((Player(playerNum).Vital(VitalType.MP) / (GetPlayerMaxVital(playerNum, VitalType.MP)) * 64))
                ' draw bars
                rec(1) = New Rectangle(x, y, barwidth, 6)
                Dim rectShape2 As New RectangleShape(New Vector2f(barwidth, 6)) With {
                    .Position = New Vector2f(x, y + 24),
                    .FillColor = SFML.Graphics.Color.Blue
                }
                GameWindow.Draw(rectShape2)
            End If

            ' draw members
            For I = 1 To MAX_PARTY_MEMBERS
                If Party.Member(I) > 0 Then
                    If Party.Member(I) <> Party.Leader Then
                        ' cache the index
                        playerNum = Party.Member(I)
                        ' name
                        theName = Trim$(GetPlayerName(playerNum))
                        ' draw name
                        y = 100 + ((I - 1) * 30)

                        DrawText(x, y, theName, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
                        ' draw hp
                        y = 115 + ((I - 1) * 30)

                        ' make sure we actually have the data before rendering
                        If GetPlayerVital(playerNum, VitalType.HP) > 0 AndAlso GetPlayerMaxVital(playerNum, VitalType.HP) > 0 Then
                            barwidth = ((Player(playerNum).Vital(VitalType.HP) / (GetPlayerMaxVital(playerNum, VitalType.HP)) * 64))
                        End If
                        rec(1) = New Rectangle(x, y, barwidth, 6)
                        Dim rectShape As New RectangleShape(New Vector2f(barwidth, 6)) With {
                            .Position = New Vector2f(x, y),
                            .FillColor = SFML.Graphics.Color.Red
                        }
                        GameWindow.Draw(rectShape)
                        ' draw mp
                        y = 115 + ((I - 1) * 30)
                        ' make sure we actually have the data before rendering
                        If GetPlayerVital(playerNum, VitalType.MP) > 0 AndAlso GetPlayerMaxVital(playerNum, VitalType.MP) > 0 Then
                            barwidth = ((Player(playerNum).Vital(VitalType.MP) / (GetPlayerMaxVital(playerNum, VitalType.MP)) * 64))
                        End If
                        rec(1) = New Rectangle(x, y, barwidth, 6)
                        Dim rectShape2 As New RectangleShape(New Vector2f(barwidth, 6)) With {
                            .Position = New Vector2f(x, y + 8),
                            .FillColor = SFML.Graphics.Color.Blue
                        }
                        GameWindow.Draw(rectShape2)
                    End If
                End If
            Next
        End If
    End Sub

#End Region

End Module