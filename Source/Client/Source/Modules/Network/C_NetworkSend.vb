Imports System.Windows.Forms
Imports ASFW
Imports ASFW.IO

Module C_NetworkSend
    Friend Sub SendNewAccount(name As String, password As String)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CNewAccount)
        buffer.WriteString((EKeyPair.EncryptString(name)))
        buffer.WriteString((EKeyPair.EncryptString(password)))
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Friend Sub SendAddChar(slot As Integer, name As String, sex As Integer, classNum As Integer, sprite As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CAddChar)
        buffer.WriteInt32(slot)
        buffer.WriteString((name))
        buffer.WriteInt32(sex)
        buffer.WriteInt32(classNum)
        buffer.WriteInt32(sprite)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Friend Sub SendLogin(name As String, password As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CLogin)
        buffer.WriteString((EKeyPair.EncryptString(name)))
        buffer.WriteString((EKeyPair.EncryptString(password)))
        buffer.WriteString((EKeyPair.EncryptString(Application.ProductVersion)))
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Sub GetPing()
        Dim buffer As New ByteStream(4)
        PingStart = GetTickCount()

        buffer.WriteInt32(ClientPackets.CCheckPing)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Friend Sub SendPlayerMove()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CPlayerMove)
        buffer.WriteInt32(GetPlayerDir(Myindex))
        buffer.WriteInt32(Player(Myindex).Moving)
        buffer.WriteInt32(Player(Myindex).X)
        buffer.WriteInt32(Player(Myindex).Y)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SayMsg(text As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSayMsg)
        buffer.WriteString((text))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendKick(name As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CKickPlayer)
        buffer.WriteString((name))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendBan(name As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CBanPlayer)
        buffer.WriteString((name))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub WarpMeTo(name As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CWarpMeTo)
        buffer.WriteString((name))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub WarpToMe(name As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CWarpToMe)
        buffer.WriteString((name))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub WarpTo(mapNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CWarpTo)
        buffer.WriteInt32(mapNum)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestLevelUp()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestLevelUp)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendSpawnItem(tmpItem As Integer, tmpAmount As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSpawnItem)
        buffer.WriteInt32(tmpItem)
        buffer.WriteInt32(tmpAmount)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendSetSprite(spriteNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSetSprite)
        buffer.WriteInt32(spriteNum)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendSetAccess(name As String, access As Byte)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSetAccess)
        buffer.WriteString((name))
        buffer.WriteInt32(access)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendAttack()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CAttack)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendPlayerDir()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CPlayerDir)
        buffer.WriteInt32(GetPlayerDir(Myindex))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendRequestNpcs()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestNPCS)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendRequestSkills()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestSkills)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub



    Sub SendRequestAnimations()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestAnimations)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendTrainStat(statNum As Byte)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CTrainStat)
        buffer.WriteInt32(statNum)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendRequestPlayerData()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestPlayerData)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub BroadcastMsg(text As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CBroadcastMsg)
        buffer.WriteString(text.Trim)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub PlayerMsg(text As String, msgTo As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CPlayerMsg)
        buffer.WriteString((msgTo))
        buffer.WriteString((text))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendWhosOnline()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CWhosOnline)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendMotdChange(motd As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSetMotd)
        buffer.WriteString((motd))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendBanList()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CBanList)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendChangeInvSlots(oldSlot As Integer, newSlot As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSwapInvSlots)
        buffer.WriteInt32(oldSlot)
        buffer.WriteInt32(newSlot)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendUseItem(invNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CUseItem)
        buffer.WriteInt32(invNum)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendDropItem(invNum As Integer, amount As Integer)
        Dim buffer As New ByteStream(4)

        If InBank OrElse InShop Then Exit Sub

        ' do basic checks
        If invNum < 1 OrElse invNum > MAX_INV Then Exit Sub
        If PlayerInv(invNum).Num < 1 OrElse PlayerInv(invNum).Num > MAX_ITEMS Then Exit Sub
        If Item(GetPlayerInvItemNum(Myindex, invNum)).Type = ItemType.Currency OrElse Item(GetPlayerInvItemNum(Myindex, invNum)).Stackable = 1 Then
            If amount < 1 OrElse amount > PlayerInv(invNum).Value Then Exit Sub
        End If

        buffer.WriteInt32(ClientPackets.CMapDropItem)
        buffer.WriteInt32(invNum)
        buffer.WriteInt32(amount)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub



    Sub PlayerSearch(curX As Integer, curY As Integer, rClick As Byte)
        Dim buffer As New ByteStream(4)

        If IsInBounds() Then
            buffer.WriteInt32(ClientPackets.CSearch)
            buffer.WriteInt32(curX)
            buffer.WriteInt32(curY)
            buffer.WriteInt32(rClick)
            Socket.SendData(buffer.Data, buffer.Head)
        End If

        buffer.Dispose()
    End Sub

    Friend Sub AdminWarp(x As Integer, y As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CAdminWarp)
        buffer.WriteInt32(x)
        buffer.WriteInt32(y)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendLeaveGame()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CQuit)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Sub SendUnequip(eqNum As Integer)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CUnequip)
        Buffer.WriteInt32(EqNum)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Friend Sub ForgetSkill(skillslot As Integer)
        dim buffer as New ByteStream(4)

        ' Check for subscript out of range
        If Skillslot < 1 OrElse Skillslot > MAX_PLAYER_SKILLS Then Exit Sub

        ' dont let them forget a skill which is in CD
        If SkillCD(Skillslot) > 0 Then
            AddText("Cannot forget a skill which is cooling down!", QColorType.AlertColor)
            Exit Sub
        End If

        ' dont let them forget a skill which is buffered
        If SkillBuffer = Skillslot Then
            AddText("Cannot forget a skill which you are casting!", QColorType.AlertColor)
            Exit Sub
        End If

        If PlayerSkills(Skillslot) > 0 Then
            Buffer.WriteInt32(ClientPackets.CForgetSkill)
            Buffer.WriteInt32(Skillslot)
            Socket.SendData(Buffer.Data, Buffer.Head)
        Else
            AddText("No skill found.", QColorType.AlertColor)
        End If

        Buffer.Dispose()
    End Sub

    Friend Sub SendRequestMapreport()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CMapReport)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Friend Sub SendRequestAdmin()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CAdmin)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Friend Sub SendRequestClasses()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CRequestClasses)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Friend Sub SendUseEmote(emote As Integer)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CEmote)
        Buffer.WriteInt32(Emote)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub
End Module
