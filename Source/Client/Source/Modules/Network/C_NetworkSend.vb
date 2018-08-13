Imports System.Windows.Forms
Imports ASFW
Imports ASFW.IO

Namespace Network
    Partial Friend Module modNetwork
        Friend Sub SendNewAccount(name As String, password As String)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CNewAccount)
            buffer.WriteString((Configuration.Encryption.EncryptString(name)))
            buffer.WriteString((Configuration.Encryption.EncryptString(password)))
            Socket.SendData(buffer.ToPacket)

            buffer.Dispose()
        End Sub

        Friend Sub SendAddChar(slot As Integer, name As String, sex As Integer, classNum As Integer, sprite As Integer)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CAddChar)
            buffer.WriteInt32(slot)
            buffer.WriteString((name))
            buffer.WriteInt32(sex)
            buffer.WriteInt32(classNum)
            buffer.WriteInt32(sprite)
            Socket.SendData(buffer.ToPacket)

            buffer.Dispose()
        End Sub

        Friend Sub SendLogin(name As String, password As String)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CLogin)
            buffer.WriteString(Configuration.Encryption.EncryptString(name))
            buffer.WriteString(Configuration.Encryption.EncryptString(password))
            buffer.WriteString(Configuration.Encryption.EncryptString(Application.ProductVersion))
            Socket.SendData(buffer.ToPacket)

            buffer.Dispose()
        End Sub

        Sub GetPing()
            Dim buffer As New ByteStream(4)
            PingStart = GetTickCount()

            buffer.WriteInt32(ClientPacket.CCheckPing)
            Socket.SendData(buffer.ToPacket)

            buffer.Dispose()
        End Sub

        Friend Sub SendPlayerMove()
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CPlayerMove)
            buffer.WriteInt32(GetPlayerDir(Myindex))
            buffer.WriteInt32(Player(Myindex).Moving)
            buffer.WriteInt32(Player(Myindex).X)
            buffer.WriteInt32(Player(Myindex).Y)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SayMsg(text As String)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CSayMsg)
            buffer.WriteString((text))

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendKick(name As String)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CKickPlayer)
            buffer.WriteString((name))

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendBan(name As String)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CBanPlayer)
            buffer.WriteString((name))

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub WarpMeTo(name As String)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CWarpMeTo)
            buffer.WriteString((name))

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub WarpToMe(name As String)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CWarpToMe)
            buffer.WriteString((name))

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub WarpTo(mapNum As Integer)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CWarpTo)
            buffer.WriteInt32(mapNum)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendRequestLevelUp()
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CRequestLevelUp)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Sub SendSpawnItem(tmpItem As Integer, tmpAmount As Integer)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CSpawnItem)
            buffer.WriteInt32(tmpItem)
            buffer.WriteInt32(tmpAmount)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendSetSprite(spriteNum As Integer)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CSetSprite)
            buffer.WriteInt32(spriteNum)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendSetAccess(name As String, access As Byte)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CSetAccess)
            buffer.WriteString((name))
            buffer.WriteInt32(access)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Sub SendAttack()
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CAttack)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendPlayerDir()
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CPlayerDir)
            buffer.WriteInt32(GetPlayerDir(Myindex))

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub
        
        Sub SendTrainStat(statNum As Byte)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CTrainStat)
            buffer.WriteInt32(statNum)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Sub SendRequestPlayerData()
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CRequestPlayerData)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub BroadcastMsg(text As String)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CBroadcastMsg)
            buffer.WriteString(text.Trim)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub PlayerMsg(text As String, msgTo As String)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CPlayerMsg)
            buffer.WriteString((msgTo))
            buffer.WriteString((text))

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendWhosOnline()
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CWhosOnline)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendMotdChange(welcome As String)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CSetMotd)
            buffer.WriteString((welcome))

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendBanList()
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CBanList)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Sub SendChangeInvSlots(oldSlot As Integer, newSlot As Integer)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CSwapInvSlots)
            buffer.WriteInt32(oldSlot)
            buffer.WriteInt32(newSlot)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendUseItem(invNum As Integer)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CUseItem)
            buffer.WriteInt32(invNum)

            Socket.SendData(buffer.ToPacket)
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

            buffer.WriteInt32(ClientPacket.CMapDropItem)
            buffer.WriteInt32(invNum)
            buffer.WriteInt32(amount)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub



        Sub PlayerSearch(curX As Integer, curY As Integer, rClick As Byte)
            Dim buffer As New ByteStream(4)

            If IsInBounds() Then
                buffer.WriteInt32(ClientPacket.CSearch)
                buffer.WriteInt32(curX)
                buffer.WriteInt32(curY)
                buffer.WriteInt32(rClick)
                Socket.SendData(buffer.ToPacket)
            End If

            buffer.Dispose()
        End Sub

        Friend Sub AdminWarp(x As Integer, y As Integer)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CAdminWarp)
            buffer.WriteInt32(x)
            buffer.WriteInt32(y)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub SendLeaveGame()
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CQuit)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Sub SendUnequip(eqNum As Integer)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CUnequip)
            buffer.WriteInt32(eqNum)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

        Friend Sub ForgetSkill(skillslot As Integer)
            Dim buffer As New ByteStream(4)

            ' Check for subscript out of range
            If skillslot < 1 OrElse skillslot > MAX_PLAYER_SKILLS Then Exit Sub

            ' dont let them forget a skill which is in CD
            If SkillCd(skillslot) > 0 Then
                AddText("Cannot forget a skill which is cooling down!", QColorType.AlertColor)
                Exit Sub
            End If

            ' dont let them forget a skill which is buffered
            If SkillBuffer = skillslot Then
                AddText("Cannot forget a skill which you are casting!", QColorType.AlertColor)
                Exit Sub
            End If

            If PlayerSkills(skillslot) > 0 Then
                buffer.WriteInt32(ClientPacket.CForgetSkill)
                buffer.WriteInt32(skillslot)
                Socket.SendData(buffer.ToPacket)
            Else
                AddText("No skill found.", QColorType.AlertColor)
            End If

            buffer.Dispose()
        End Sub
        
        Friend Sub SendRequestAdmin()
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CAdmin)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub
        
        Friend Sub SendUseEmote(emote As Integer)
            Dim buffer As New ByteStream(4)

            buffer.WriteInt32(ClientPacket.CEmote)
            buffer.WriteInt32(emote)

            Socket.SendData(buffer.ToPacket)
            buffer.Dispose()
        End Sub

            Friend Sub SendRequestEditResource()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditResource)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Sub SendRequestResources()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestResources)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveResource(ResourceNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveResource)

        buffer.WriteInt32(ResourceNum)
        buffer.WriteInt32(Resource(ResourceNum).Animation)
        buffer.WriteString((Resource(ResourceNum).EmptyMessage.Trim))
        buffer.WriteInt32(Resource(ResourceNum).ExhaustedImage)
        buffer.WriteInt32(Resource(ResourceNum).Health)
        buffer.WriteInt32(Resource(ResourceNum).ExpReward)
        buffer.WriteInt32(Resource(ResourceNum).ItemReward)
        buffer.WriteString((Resource(ResourceNum).Name.Trim))
        buffer.WriteInt32(Resource(ResourceNum).ResourceImage)
        buffer.WriteInt32(Resource(ResourceNum).ResourceType)
        buffer.WriteInt32(Resource(ResourceNum).RespawnTime)
        buffer.WriteString((Resource(ResourceNum).SuccessMessage.Trim))
        buffer.WriteInt32(Resource(ResourceNum).LvlRequired)
        buffer.WriteInt32(Resource(ResourceNum).ToolRequired)
        buffer.WriteInt32(Resource(ResourceNum).Walkthrough)

        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditNpc()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditNpc)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveNpc(NpcNum As Integer)
        Dim buffer As New ByteStream(4), i As Integer

        buffer.WriteInt32(EditorPackets.SaveNpc)
        buffer.WriteInt32(NpcNum)

        buffer.WriteInt32(Npc(NpcNum).Animation)
        buffer.WriteString((Npc(NpcNum).AttackSay))
        buffer.WriteInt32(Npc(NpcNum).Behaviour)
        For i = 1 To 5
            buffer.WriteInt32(Npc(NpcNum).DropChance(i))
            buffer.WriteInt32(Npc(NpcNum).DropItem(i))
            buffer.WriteInt32(Npc(NpcNum).DropItemValue(i))
        Next

        buffer.WriteInt32(Npc(NpcNum).Exp)
        buffer.WriteInt32(Npc(NpcNum).Faction)
        buffer.WriteInt32(Npc(NpcNum).Hp)
        buffer.WriteString((Npc(NpcNum).Name))
        buffer.WriteInt32(Npc(NpcNum).Range)
        buffer.WriteInt32(Npc(NpcNum).SpawnTime)
        buffer.WriteInt32(Npc(NpcNum).SpawnSecs)
        buffer.WriteInt32(Npc(NpcNum).Sprite)

        For i = 0 To StatType.Count - 1
            buffer.WriteInt32(Npc(NpcNum).Stat(i))
        Next

        buffer.WriteInt32(Npc(NpcNum).QuestNum)

        For i = 1 To MAX_NPC_SKILLS
            buffer.WriteInt32(Npc(NpcNum).Skill(i))
        Next

        buffer.WriteInt32(Npc(NpcNum).Level)
        buffer.WriteInt32(Npc(NpcNum).Damage)

        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Sub SendRequestNPCS()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestNPCS)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditSkill()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditSkill)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Sub SendRequestSkills()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestSkills)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveSkill(skillnum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveSkill)
        buffer.WriteInt32(skillnum)

        buffer.WriteInt32(Skill(skillnum).AccessReq)
        buffer.WriteInt32(Skill(skillnum).AoE)
        buffer.WriteInt32(Skill(skillnum).CastAnim)
        buffer.WriteInt32(Skill(skillnum).CastTime)
        buffer.WriteInt32(Skill(skillnum).CdTime)
        buffer.WriteInt32(Skill(skillnum).ClassReq)
        buffer.WriteInt32(Skill(skillnum).Dir)
        buffer.WriteInt32(Skill(skillnum).Duration)
        buffer.WriteInt32(Skill(skillnum).Icon)
        buffer.WriteInt32(Skill(skillnum).Interval)
        buffer.WriteInt32(Skill(skillnum).IsAoE)
        buffer.WriteInt32(Skill(skillnum).LevelReq)
        buffer.WriteInt32(Skill(skillnum).Map)
        buffer.WriteInt32(Skill(skillnum).MpCost)
        buffer.WriteString((Skill(skillnum).Name))
        buffer.WriteInt32(Skill(skillnum).Range)
        buffer.WriteInt32(Skill(skillnum).SkillAnim)
        buffer.WriteInt32(Skill(skillnum).StunDuration)
        buffer.WriteInt32(Skill(skillnum).Type)
        buffer.WriteInt32(Skill(skillnum).Vital)
        buffer.WriteInt32(Skill(skillnum).X)
        buffer.WriteInt32(Skill(skillnum).Y)

        buffer.WriteInt32(Skill(skillnum).IsProjectile)
        buffer.WriteInt32(Skill(skillnum).Projectile)

        buffer.WriteInt32(Skill(skillnum).KnockBack)
        buffer.WriteInt32(Skill(skillnum).KnockBackTiles)

        Socket.SendData(buffer.ToPacket)

        buffer.Dispose()
    End Sub

    Sub SendRequestShops()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestShops)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveShop(shopnum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveShop)
        buffer.WriteInt32(shopnum)

        buffer.WriteInt32(Shop(shopnum).BuyRate)
        buffer.WriteString((Shop(shopnum).Name))
        buffer.WriteInt32(Shop(shopnum).Face)

        For i = 0 To MAX_TRADES
            buffer.WriteInt32(Shop(shopnum).TradeItem(i).CostItem)
            buffer.WriteInt32(Shop(shopnum).TradeItem(i).CostValue)
            buffer.WriteInt32(Shop(shopnum).TradeItem(i).Item)
            buffer.WriteInt32(Shop(shopnum).TradeItem(i).ItemValue)
        Next

        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()

    End Sub

    Friend Sub SendRequestEditShop()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditShop)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveAnimation(Animationnum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveAnimation)
        buffer.WriteInt32(Animationnum)

        For i = 0 To Animation(Animationnum).Frames.GetUpperBound(0)
            buffer.WriteInt32(Animation(Animationnum).Frames(i))
        Next

        For i = 0 To Animation(Animationnum).LoopCount.GetUpperBound(0)
            buffer.WriteInt32(Animation(Animationnum).LoopCount(i))
        Next

        For i = 0 To Animation(Animationnum).LoopTime.GetUpperBound(0)
            buffer.WriteInt32(Animation(Animationnum).LoopTime(i))
        Next

        buffer.WriteString((Animation(Animationnum).Name.Trim))
        buffer.WriteString((Animation(Animationnum).Sound.Trim))

        For i = 0 To Animation(Animationnum).Sprite.GetUpperBound(0)
            buffer.WriteInt32(Animation(Animationnum).Sprite(i))
        Next

        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Sub SendRequestAnimations()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestAnimations)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditAnimation()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditAnimation)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestMapreport()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CMapReport)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestClasses()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestClasses)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditClass()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditClasses)
        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveClasses()
        Dim i As Integer, n As Integer, q As Integer
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveClasses)

        buffer.WriteInt32(MaxClasses)

        For i = 1 To MaxClasses
            buffer.WriteString((Classes(i).Name.Trim))
            buffer.WriteString((Classes(i).Desc.Trim))

            ' set sprite array size
            n = Classes(i).MaleSprite.GetUpperBound(0)

            ' send array size
            buffer.WriteInt32(n)

            ' loop around sending each sprite
            For q = 0 To n
                buffer.WriteInt32(Classes(i).MaleSprite(q))
            Next

            ' set sprite array size
            n = Classes(i).FemaleSprite.GetUpperBound(0)

            ' send array size
            buffer.WriteInt32(n)

            ' loop around sending each sprite
            For q = 0 To n
                buffer.WriteInt32(Classes(i).FemaleSprite(q))
            Next

            buffer.WriteInt32(Classes(i).Stat(StatType.Strength))
            buffer.WriteInt32(Classes(i).Stat(StatType.Endurance))
            buffer.WriteInt32(Classes(i).Stat(StatType.Vitality))
            buffer.WriteInt32(Classes(i).Stat(StatType.Intelligence))
            buffer.WriteInt32(Classes(i).Stat(StatType.Luck))
            buffer.WriteInt32(Classes(i).Stat(StatType.Spirit))

            For q = 1 To 5
                buffer.WriteInt32(Classes(i).StartItem(q))
                buffer.WriteInt32(Classes(i).StartValue(q))
            Next

            buffer.WriteInt32(Classes(i).StartMap)
            buffer.WriteInt32(Classes(i).StartX)
            buffer.WriteInt32(Classes(i).StartY)

            buffer.WriteInt32(Classes(i).BaseExp)
        Next

        Socket.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub
    End Module
End Namespace
