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

    Friend Sub SendRequestEditMap()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestEditMap)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Friend Sub SendMap()
        Dim x As Integer, y As Integer, i As Integer
        Dim data() As Byte
        Dim buffer As New ByteStream(4)
        CanMoveNow = False

        buffer.WriteString((Trim$(Map.Name)))
        buffer.WriteString((Trim$(Map.Music)))
        buffer.WriteInt32(Map.Moral)
        buffer.WriteInt32(Map.Tileset)
        buffer.WriteInt32(Map.Up)
        buffer.WriteInt32(Map.Down)
        buffer.WriteInt32(Map.Left)
        buffer.WriteInt32(Map.Right)
        buffer.WriteInt32(Map.BootMap)
        buffer.WriteInt32(Map.BootX)
        buffer.WriteInt32(Map.BootY)
        buffer.WriteInt32(Map.MaxX)
        buffer.WriteInt32(Map.MaxY)
        buffer.WriteInt32(Map.WeatherType)
        buffer.WriteInt32(Map.Fogindex)
        buffer.WriteInt32(Map.WeatherIntensity)
        buffer.WriteInt32(Map.FogAlpha)
        buffer.WriteInt32(Map.FogSpeed)
        buffer.WriteInt32(Map.HasMapTint)
        buffer.WriteInt32(Map.MapTintR)
        buffer.WriteInt32(Map.MapTintG)
        buffer.WriteInt32(Map.MapTintB)
        buffer.WriteInt32(Map.MapTintA)
        buffer.WriteInt32(Map.Instanced)
        buffer.WriteInt32(Map.Panorama)
        buffer.WriteInt32(Map.Parallax)

        For i = 1 To MAX_MAP_NPCS
            buffer.WriteInt32(Map.Npc(i))
        Next

        For x = 0 To Map.MaxX
            For y = 0 To Map.MaxY
                buffer.WriteInt32(Map.Tile(x, y).Data1)
                buffer.WriteInt32(Map.Tile(x, y).Data2)
                buffer.WriteInt32(Map.Tile(x, y).Data3)
                buffer.WriteInt32(Map.Tile(x, y).DirBlock)
                For i = 0 To LayerType.Count - 1
                    buffer.WriteInt32(Map.Tile(x, y).Layer(i).Tileset)
                    buffer.WriteInt32(Map.Tile(x, y).Layer(i).X)
                    buffer.WriteInt32(Map.Tile(x, y).Layer(i).Y)
                    buffer.WriteInt32(Map.Tile(x, y).Layer(i).AutoTile)
                Next
                buffer.WriteInt32(Map.Tile(x, y).Type)
            Next
        Next

        'Event Data
        buffer.WriteInt32(Map.EventCount)
        If Map.EventCount > 0 Then
            For i = 1 To Map.EventCount
                With Map.Events(i)
                    buffer.WriteString((Trim$(.Name)))
                    buffer.WriteInt32(.Globals)
                    buffer.WriteInt32(.X)
                    buffer.WriteInt32(.Y)
                    buffer.WriteInt32(.PageCount)
                End With
                If Map.Events(i).PageCount > 0 Then
                    For x = 1 To Map.Events(i).PageCount
                        With Map.Events(i).Pages(x)
                            buffer.WriteInt32(.ChkVariable)
                            buffer.WriteInt32(.Variableindex)
                            buffer.WriteInt32(.VariableCondition)
                            buffer.WriteInt32(.VariableCompare)
                            buffer.WriteInt32(.ChkSwitch)
                            buffer.WriteInt32(.Switchindex)
                            buffer.WriteInt32(.SwitchCompare)
                            buffer.WriteInt32(.ChkHasItem)
                            buffer.WriteInt32(.HasItemindex)
                            buffer.WriteInt32(.HasItemAmount)
                            buffer.WriteInt32(.ChkSelfSwitch)
                            buffer.WriteInt32(.SelfSwitchindex)
                            buffer.WriteInt32(.SelfSwitchCompare)
                            buffer.WriteInt32(.GraphicType)
                            buffer.WriteInt32(.Graphic)
                            buffer.WriteInt32(.GraphicX)
                            buffer.WriteInt32(.GraphicY)
                            buffer.WriteInt32(.GraphicX2)
                            buffer.WriteInt32(.GraphicY2)
                            buffer.WriteInt32(.MoveType)
                            buffer.WriteInt32(.MoveSpeed)
                            buffer.WriteInt32(.MoveFreq)
                            buffer.WriteInt32(Map.Events(i).Pages(x).MoveRouteCount)
                            buffer.WriteInt32(.IgnoreMoveRoute)
                            buffer.WriteInt32(.RepeatMoveRoute)
                            If .MoveRouteCount > 0 Then
                                For y = 1 To .MoveRouteCount
                                    buffer.WriteInt32(.MoveRoute(y).Index)
                                    buffer.WriteInt32(.MoveRoute(y).Data1)
                                    buffer.WriteInt32(.MoveRoute(y).Data2)
                                    buffer.WriteInt32(.MoveRoute(y).Data3)
                                    buffer.WriteInt32(.MoveRoute(y).Data4)
                                    buffer.WriteInt32(.MoveRoute(y).Data5)
                                    buffer.WriteInt32(.MoveRoute(y).Data6)
                                Next
                            End If
                            buffer.WriteInt32(.WalkAnim)
                            buffer.WriteInt32(.DirFix)
                            buffer.WriteInt32(.WalkThrough)
                            buffer.WriteInt32(.ShowName)
                            buffer.WriteInt32(.Trigger)
                            buffer.WriteInt32(.CommandListCount)
                            buffer.WriteInt32(.Position)
                            buffer.WriteInt32(.Questnum)

                            buffer.WriteInt32(.ChkPlayerGender)
                        End With
                        If Map.Events(i).Pages(x).CommandListCount > 0 Then
                            For y = 1 To Map.Events(i).Pages(x).CommandListCount
                                buffer.WriteInt32(Map.Events(i).Pages(x).CommandList(y).CommandCount)
                                buffer.WriteInt32(Map.Events(i).Pages(x).CommandList(y).ParentList)
                                If Map.Events(i).Pages(x).CommandList(y).CommandCount > 0 Then
                                    For z = 1 To Map.Events(i).Pages(x).CommandList(y).CommandCount
                                        With Map.Events(i).Pages(x).CommandList(y).Commands(z)
                                            buffer.WriteInt32(.Index)
                                            buffer.WriteString((Trim$(.Text1)))
                                            buffer.WriteString((Trim$(.Text2)))
                                            buffer.WriteString((Trim$(.Text3)))
                                            buffer.WriteString((Trim$(.Text4)))
                                            buffer.WriteString((Trim$(.Text5)))
                                            buffer.WriteInt32(.Data1)
                                            buffer.WriteInt32(.Data2)
                                            buffer.WriteInt32(.Data3)
                                            buffer.WriteInt32(.Data4)
                                            buffer.WriteInt32(.Data5)
                                            buffer.WriteInt32(.Data6)
                                            buffer.WriteInt32(.ConditionalBranch.CommandList)
                                            buffer.WriteInt32(.ConditionalBranch.Condition)
                                            buffer.WriteInt32(.ConditionalBranch.Data1)
                                            buffer.WriteInt32(.ConditionalBranch.Data2)
                                            buffer.WriteInt32(.ConditionalBranch.Data3)
                                            buffer.WriteInt32(.ConditionalBranch.ElseCommandList)
                                            buffer.WriteInt32(.MoveRouteCount)
                                            If .MoveRouteCount > 0 Then
                                                For w = 1 To .MoveRouteCount
                                                    buffer.WriteInt32(.MoveRoute(w).Index)
                                                    buffer.WriteInt32(.MoveRoute(w).Data1)
                                                    buffer.WriteInt32(.MoveRoute(w).Data2)
                                                    buffer.WriteInt32(.MoveRoute(w).Data3)
                                                    buffer.WriteInt32(.MoveRoute(w).Data4)
                                                    buffer.WriteInt32(.MoveRoute(w).Data5)
                                                    buffer.WriteInt32(.MoveRoute(w).Data6)
                                                Next
                                            End If
                                        End With
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If
        'End Event Data

        data = buffer.ToArray

        buffer = New ByteStream(4)
        buffer.WriteInt32(ClientPackets.CSaveMap)
        buffer.WriteBlock(Compression.CompressBytes(data))

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
        'buffer.Writestring((text)
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

    Sub SendRequestItems()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestItems)

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

    Friend Sub SendPlayerRequestNewMap()
        If GettingMap Then Exit Sub

        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestNewMap)
        buffer.WriteInt32(GetPlayerDir(Myindex))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub SendRequestResources()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestResources)

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

    Sub SendRequestShops()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestShops)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendRequestAnimations()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestAnimations)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendMapRespawn()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CMapRespawn)

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
        'buffer.Writestring((text)
        buffer.WriteString((text))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub PlayerMsg(text As String, msgTo As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CPlayerMsg)
        buffer.WriteString((msgTo))
        'buffer.Writestring((text)
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

    Friend Sub BuyItem(shopslot As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CBuyItem)
        buffer.WriteInt32(shopslot)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SellItem(invslot As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSellItem)
        buffer.WriteInt32(invslot)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub DepositItem(invslot As Integer, amount As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CDepositItem)
        buffer.WriteInt32(invslot)
        buffer.WriteInt32(amount)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub WithdrawItem(bankslot As Integer, amount As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CWithdrawItem)
        buffer.WriteInt32(bankslot)
        buffer.WriteInt32(amount)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub ChangeBankSlots(oldSlot As Integer, newSlot As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CChangeBankSlots)
        buffer.WriteInt32(oldSlot)
        buffer.WriteInt32(newSlot)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub CloseBank()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CCloseBank)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

        InBank = False
        PnlBankVisible = False
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

    Sub SendTradeRequest(name As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CTradeInvite)

        buffer.WriteString((name))

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()

    End Sub

    Sub SendTradeInviteAccept(awnser As Byte)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CTradeInviteAccept)

        Buffer.WriteInt32(Awnser)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()

    End Sub

    Friend Sub TradeItem(invslot As Integer, amount As Integer)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CTradeItem)
        Buffer.WriteInt32(invslot)
        Buffer.WriteInt32(Amount)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Friend Sub UntradeItem(invslot As Integer)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CUntradeItem)
        Buffer.WriteInt32(invslot)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Friend Sub AcceptTrade()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CAcceptTrade)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Friend Sub DeclineTrade()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CDeclineTrade)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
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
