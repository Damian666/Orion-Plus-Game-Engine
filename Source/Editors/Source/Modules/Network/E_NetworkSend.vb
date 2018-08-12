Imports ASFW
Imports ASFW.IO

Module E_NetworkSend
    Friend Sub SendEditorLogin(Name As String, Password As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.EditorLogin)
        buffer.WriteString((EKeyPair.EncryptString(Name)))
        buffer.WriteString((EKeyPair.EncryptString(Password)))
        buffer.WriteString((EKeyPair.EncryptString(Application.ProductVersion)))
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditResource()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditResource)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendRequestResources()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestResources)
        Socket.SendData(buffer.Data, buffer.Head)
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

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditNpc()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditNpc)
        Socket.SendData(buffer.Data, buffer.Head)
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

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendRequestNPCS()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestNPCS)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditSkill()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditSkill)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendRequestSkills()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestSkills)
        Socket.SendData(buffer.Data, buffer.Head)
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

        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Sub SendRequestShops()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestShops)
        Socket.SendData(buffer.Data, buffer.Head)
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

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Friend Sub SendRequestEditShop()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditShop)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveAnimation(Animationnum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveAnimation)
        buffer.WriteInt32(Animationnum)

        For i = 0 To UBound(Animation(Animationnum).Frames)
            buffer.WriteInt32(Animation(Animationnum).Frames(i))
        Next

        For i = 0 To UBound(Animation(Animationnum).LoopCount)
            buffer.WriteInt32(Animation(Animationnum).LoopCount(i))
        Next

        For i = 0 To UBound(Animation(Animationnum).LoopTime)
            buffer.WriteInt32(Animation(Animationnum).LoopTime(i))
        Next

        buffer.WriteString((Animation(Animationnum).Name.Trim))
        buffer.WriteString((Animation(Animationnum).Sound.Trim))

        For i = 0 To UBound(Animation(Animationnum).Sprite)
            buffer.WriteInt32(Animation(Animationnum).Sprite(i))
        Next

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendRequestAnimations()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestAnimations)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditAnimation()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditAnimation)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestMapreport()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CMapReport)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestClasses()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestClasses)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditClass()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditClasses)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveClasses()
        Dim i As Integer, n As Integer, q As Integer
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveClasses)

        buffer.WriteInt32(Max_Classes)

        For i = 1 To Max_Classes
            buffer.WriteString((Classes(i).Name.Trim))
            buffer.WriteString((Classes(i).Desc.Trim))

            ' set sprite array size
            n = UBound(Classes(i).MaleSprite)

            ' send array size
            buffer.WriteInt32(n)

            ' loop around sending each sprite
            For q = 0 To n
                buffer.WriteInt32(Classes(i).MaleSprite(q))
            Next

            ' set sprite array size
            n = UBound(Classes(i).FemaleSprite)

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

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendLeaveGame()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CQuit)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendEditorRequestMap(mapNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.EditorRequestMap)
        buffer.WriteInt32(mapNum)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub


End Module
















































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































