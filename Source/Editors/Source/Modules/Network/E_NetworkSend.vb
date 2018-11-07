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

    Friend Sub SendEditorMap()
        Dim X As Integer, Y As Integer, i As Integer
        Dim data() As Byte
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(Map.mapNum)

        buffer.WriteString((Trim$(Map.Name)))
        buffer.WriteString((Trim$(Map.Music)))
        buffer.WriteInt32(Map.Moral)
        buffer.WriteInt32(Map.tileset)
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

        For X = 0 To Map.MaxX
            For Y = 0 To Map.MaxY
                buffer.WriteInt32(Map.Tile(X, Y).Data1)
                buffer.WriteInt32(Map.Tile(X, Y).Data2)
                buffer.WriteInt32(Map.Tile(X, Y).Data3)
                buffer.WriteInt32(Map.Tile(X, Y).DirBlock)
                For i = 0 To LayerType.Count - 1
                    buffer.WriteInt32(Map.Tile(X, Y).Layer(i).Tileset)
                    buffer.WriteInt32(Map.Tile(X, Y).Layer(i).X)
                    buffer.WriteInt32(Map.Tile(X, Y).Layer(i).Y)
                    buffer.WriteInt32(Map.Tile(X, Y).Layer(i).AutoTile)
                Next
                buffer.WriteInt32(Map.Tile(X, Y).Type)
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
                    For X = 1 To Map.Events(i).PageCount
                        With Map.Events(i).Pages(X)
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
                            buffer.WriteInt32(Map.Events(i).Pages(X).MoveRouteCount)
                            buffer.WriteInt32(.IgnoreMoveRoute)
                            buffer.WriteInt32(.RepeatMoveRoute)
                            If .MoveRouteCount > 0 Then
                                For Y = 1 To .MoveRouteCount
                                    buffer.WriteInt32(.MoveRoute(Y).Index)
                                    buffer.WriteInt32(.MoveRoute(Y).Data1)
                                    buffer.WriteInt32(.MoveRoute(Y).Data2)
                                    buffer.WriteInt32(.MoveRoute(Y).Data3)
                                    buffer.WriteInt32(.MoveRoute(Y).Data4)
                                    buffer.WriteInt32(.MoveRoute(Y).Data5)
                                    buffer.WriteInt32(.MoveRoute(Y).Data6)
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
                        If Map.Events(i).Pages(X).CommandListCount > 0 Then
                            For Y = 1 To Map.Events(i).Pages(X).CommandListCount
                                buffer.WriteInt32(Map.Events(i).Pages(X).CommandList(Y).CommandCount)
                                buffer.WriteInt32(Map.Events(i).Pages(X).CommandList(Y).ParentList)
                                If Map.Events(i).Pages(X).CommandList(Y).CommandCount > 0 Then
                                    For z = 1 To Map.Events(i).Pages(X).CommandList(Y).CommandCount
                                        With Map.Events(i).Pages(X).CommandList(Y).Commands(z)
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
        buffer.WriteInt32(EditorPackets.EditorSaveMap)
        buffer.WriteBlock(Compression.CompressBytes(data))

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

        buffer.WriteInt32(ClientPackets.CRequestResources)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveResource(ResourceNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveResource)

        buffer.WriteInt32(ResourceNum)
        buffer.WriteInt32(Resource(ResourceNum).Animation)
        buffer.WriteString((Trim(Resource(ResourceNum).EmptyMessage)))
        buffer.WriteInt32(Resource(ResourceNum).ExhaustedImage)
        buffer.WriteInt32(Resource(ResourceNum).Health)
        buffer.WriteInt32(Resource(ResourceNum).ExpReward)
        buffer.WriteInt32(Resource(ResourceNum).ItemReward)
        buffer.WriteString((Trim(Resource(ResourceNum).Name)))
        buffer.WriteInt32(Resource(ResourceNum).ResourceImage)
        buffer.WriteInt32(Resource(ResourceNum).ResourceType)
        buffer.WriteInt32(Resource(ResourceNum).RespawnTime)
        buffer.WriteString((Trim(Resource(ResourceNum).SuccessMessage)))
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

        buffer.WriteInt32(ClientPackets.CRequestNPCS)
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

        buffer.WriteInt32(ClientPackets.CRequestSkills)
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

        buffer.WriteInt32(ClientPackets.CRequestShops)
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

        buffer.WriteString((Trim$(Animation(Animationnum).Name)))
        buffer.WriteString((Trim$(Animation(Animationnum).Sound)))

        For i = 0 To UBound(Animation(Animationnum).Sprite)
            buffer.WriteInt32(Animation(Animationnum).Sprite(i))
        Next

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendRequestAnimations()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestAnimations)
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

        buffer.WriteInt32(ClientPackets.CMapReport)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestClasses()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestClasses)
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
            buffer.WriteString((Trim$(Classes(i).Name)))
            buffer.WriteString((Trim$(Classes(i).Desc)))

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

        buffer.WriteInt32(ClientPackets.CQuit)
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