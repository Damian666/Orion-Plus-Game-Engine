Imports ASFW

Module S_Npc
#Region "Npcombat"
    Friend Sub TryNpcAttackPlayer(mapnpcnum As Integer, index as integer)

        Dim mapNum as Integer, npcnum As Integer, Damage As Integer, i As Integer, armor As Integer

        ' Can the npc attack the player?

        If CanNpcAttackPlayer(mapnpcnum, Index) Then
            MapNum = GetPlayerMap(Index)
            npcnum = MapNpc(MapNum).Npc(mapnpcnum).Num

            ' check if PLAYER can avoid the attack
            If CanPlayerDodge(Index) Then
                SendActionMsg(MapNum, "Dodge!", ColorType.Pink, 1, (Player(Index).Character(TempPlayer(Index).CurChar).X * 32), (Player(Index).Character(TempPlayer(Index).CurChar).Y * 32))
                Exit Sub
            End If

            If CanPlayerParry(Index) Then
                SendActionMsg(MapNum, "Parry!", ColorType.Pink, 1, (Player(Index).Character(TempPlayer(Index).CurChar).X * 32), (Player(Index).Character(TempPlayer(Index).CurChar).Y * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            Damage = GetNpcDamage(npcnum)

            If CanPlayerBlockHit(Index) Then
                SendActionMsg(MapNum, "Block!", ColorType.Pink, 1, (Player(Index).Character(TempPlayer(Index).CurChar).X * 32), (Player(Index).Character(TempPlayer(Index).CurChar).Y * 32))
                Exit Sub
            Else

                For i = 2 To EquipmentType.Count - 1 ' start at 2, so we skip weapon
                    If GetPlayerEquipment(Index, i) > 0 Then
                        armor = armor + Item(GetPlayerEquipment(Index, i)).Data2
                    End If
                Next
                ' take away armour
                Damage = Damage - ((GetPlayerStat(Index, StatType.Spirit) * 2) + (GetPlayerLevel(Index) * 2) + armor)

                ' * 1.5 if crit hit
                If CanNpcCrit(npcnum) Then
                    Damage = Damage * 1.5
                    SendActionMsg(MapNum, "Critical!", ColorType.BrightCyan, 1, (MapNpc(MapNum).Npc(mapnpcnum).X * 32), (MapNpc(MapNum).Npc(mapnpcnum).Y * 32))
                End If

            End If

            If Damage > 0 Then
                NpcAttackPlayer(mapnpcnum, Index, Damage)
            End If

        End If

    End Sub

    Function CanNpcAttackPlayer(MapNpcNum As Integer, index as integer) As Boolean
        Dim mapNum as Integer
        Dim NpcNum As Integer

        ' Check for subscript out of range
        If MapNpcNum <= 0 OrElse MapNpcNum > MAX_MAP_NPCS OrElse Not IsPlaying(Index) Then
            Exit Function
        End If

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(Index)).Npc(MapNpcNum).Num <= 0 Then
            Exit Function
        End If

        MapNum = GetPlayerMap(Index)
        NpcNum = MapNpc(MapNum).Npc(MapNpcNum).Num

        ' Make sure the npc isn't already dead
        If MapNpc(MapNum).Npc(MapNpcNum).Vital(VitalType.HP) <= 0 Then
            Exit Function
        End If

        ' Make sure npcs dont attack more then once a second
        If GetTimeMs() < MapNpc(MapNum).Npc(MapNpcNum).AttackTimer + 1000 Then
            Exit Function
        End If

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(Index).GettingMap = True Then
            Exit Function
        End If

        MapNpc(MapNum).Npc(MapNpcNum).AttackTimer = GetTimeMs()

        ' Make sure they are on the same map
        If IsPlaying(Index) Then
            If NpcNum > 0 Then

                ' Check if at same coordinates
                If (GetPlayerY(Index) + 1 = MapNpc(MapNum).Npc(MapNpcNum).Y) AndAlso (GetPlayerX(Index) = MapNpc(MapNum).Npc(MapNpcNum).X) Then
                    CanNpcAttackPlayer = True
                Else

                    If (GetPlayerY(Index) - 1 = MapNpc(MapNum).Npc(MapNpcNum).Y) AndAlso (GetPlayerX(Index) = MapNpc(MapNum).Npc(MapNpcNum).X) Then
                        CanNpcAttackPlayer = True
                    Else

                        If (GetPlayerY(Index) = MapNpc(MapNum).Npc(MapNpcNum).Y) AndAlso (GetPlayerX(Index) + 1 = MapNpc(MapNum).Npc(MapNpcNum).X) Then
                            CanNpcAttackPlayer = True
                        Else

                            If (GetPlayerY(Index) = MapNpc(MapNum).Npc(MapNpcNum).Y) AndAlso (GetPlayerX(Index) - 1 = MapNpc(MapNum).Npc(MapNpcNum).X) Then
                                CanNpcAttackPlayer = True
                            End If
                        End If
                    End If
                End If
            End If
        End If

    End Function

    Function CanNpcAttackNpc(mapNum as Integer, Attacker As Integer, Victim As Integer) As Boolean
        Dim aNpcNum As Integer, vNpcNum As Integer, VictimX As Integer
        Dim VictimY As Integer, AttackerX As Integer, AttackerY As Integer

        CanNpcAttackNpc = False

        ' Check for subscript out of range
        If Attacker <= 0 OrElse Attacker > MAX_MAP_NPCS Then
            Exit Function
        End If

        If Victim <= 0 OrElse Victim > MAX_MAP_NPCS Then
            Exit Function
        End If

        ' Check for subscript out of range
        If MapNpc(MapNum).Npc(Attacker).Num <= 0 Then
            Exit Function
        End If

        ' Check for subscript out of range
        If MapNpc(MapNum).Npc(Victim).Num <= 0 Then
            Exit Function
        End If

        aNpcNum = MapNpc(MapNum).Npc(Attacker).Num
        vNpcNum = MapNpc(MapNum).Npc(Victim).Num

        If aNpcNum <= 0 Then Exit Function
        If vNpcNum <= 0 Then Exit Function

        ' Make sure the npcs arent already dead
        If MapNpc(MapNum).Npc(Attacker).Vital(VitalType.HP) <= 0 Then
            Exit Function
        End If

        ' Make sure the npc isn't already dead
        If MapNpc(MapNum).Npc(Victim).Vital(VitalType.HP) <= 0 Then
            Exit Function
        End If

        ' Make sure npcs dont attack more then once a second
        If GetTimeMs() < MapNpc(MapNum).Npc(Attacker).AttackTimer + 1000 Then
            Exit Function
        End If

        MapNpc(MapNum).Npc(Attacker).AttackTimer = GetTimeMs()

        AttackerX = MapNpc(MapNum).Npc(Attacker).X
        AttackerY = MapNpc(MapNum).Npc(Attacker).Y
        VictimX = MapNpc(MapNum).Npc(Victim).X
        VictimY = MapNpc(MapNum).Npc(Victim).Y

        ' Check if at same coordinates
        If (VictimY + 1 = AttackerY) AndAlso (VictimX = AttackerX) Then
            CanNpcAttackNpc = True
        Else

            If (VictimY - 1 = AttackerY) AndAlso (VictimX = AttackerX) Then
                CanNpcAttackNpc = True
            Else

                If (VictimY = AttackerY) AndAlso (VictimX + 1 = AttackerX) Then
                    CanNpcAttackNpc = True
                Else

                    If (VictimY = AttackerY) AndAlso (VictimX - 1 = AttackerX) Then
                        CanNpcAttackNpc = True
                    End If
                End If
            End If
        End If

    End Function

    Friend Sub NpcAttackPlayer(MapNpcNum As Integer, Victim As Integer, Damage As Integer)
        Dim Name As String, mapNum as Integer
        Dim z As Integer, InvCount As Integer, EqCount As Integer, j As Integer, x As Integer
        dim buffer as New ByteStream(4)

        ' Check for subscript out of range

        If MapNpcNum <= 0 OrElse MapNpcNum > MAX_MAP_NPCS OrElse IsPlaying(Victim) = False Then Exit Sub

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(Victim)).Npc(MapNpcNum).Num <= 0 Then Exit Sub

        MapNum = GetPlayerMap(Victim)
        Name = Trim$(Npc(MapNpc(MapNum).Npc(MapNpcNum).Num).Name)

        ' Send this packet so they can see the npc attacking
        Buffer.WriteInt32(ServerPackets.SNpcAttack)
        Buffer.WriteInt32(MapNpcNum)
        SendDataToMap(MapNum, Buffer.Data, Buffer.Head)
        Buffer.Dispose()

        If Damage <= 0 Then Exit Sub

        ' set the regen timer
        MapNpc(MapNum).Npc(MapNpcNum).StopRegen = True
        MapNpc(MapNum).Npc(MapNpcNum).StopRegenTimer = GetTimeMs()

        If Damage >= GetPlayerVital(Victim, VitalType.HP) Then
            ' Say damage
            SendActionMsg(GetPlayerMap(Victim), "-" & GetPlayerVital(Victim, VitalType.HP), ColorType.BrightRed, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))

            ' Set NPC target to 0
            MapNpc(MapNum).Npc(MapNpcNum).Target = 0
            MapNpc(MapNum).Npc(MapNpcNum).TargetType = 0

            If GetPlayerLevel(Victim) >= 10 Then

                For z = 1 To MAX_INV
                    If GetPlayerInvItemNum(Victim, z) > 0 Then
                        InvCount = InvCount + 1
                    End If
                Next

                For z = 1 To EquipmentType.Count - 1
                    If GetPlayerEquipment(Victim, z) > 0 Then
                        EqCount = EqCount + 1
                    End If
                Next
                z = Random(1, InvCount + EqCount)

                If z = 0 Then z = 1
                If z > InvCount + EqCount Then z = InvCount + EqCount
                If z > InvCount Then
                    z = z - InvCount

                    For x = 1 To EquipmentType.Count - 1

                        If GetPlayerEquipment(Victim, x) > 0 Then
                            j = j + 1

                            If j = z Then
                                'Here it is, drop this piece of equipment!
                                PlayerMsg(Victim, "In death you lost grip on your " & Trim$(Item(GetPlayerEquipment(Victim, x)).Name), ColorType.BrightRed)
                                SpawnItem(GetPlayerEquipment(Victim, x), 1, GetPlayerMap(Victim), GetPlayerX(Victim), GetPlayerY(Victim))
                                SetPlayerEquipment(Victim, 0, x)
                                SendWornEquipment(Victim)
                                SendMapEquipment(Victim)
                            End If
                        End If
                    Next
                Else
                    For x = 1 To MAX_INV
                        If GetPlayerInvItemNum(Victim, x) > 0 Then
                            j = j + 1

                            If j = z Then
                                'Here it is, drop this item!
                                PlayerMsg(Victim, "In death you lost grip on your " & Trim$(Item(GetPlayerInvItemNum(Victim, x)).Name), ColorType.BrightRed)
                                SpawnItem(GetPlayerInvItemNum(Victim, x), GetPlayerInvItemValue(Victim, x), GetPlayerMap(Victim), GetPlayerX(Victim), GetPlayerY(Victim))
                                SetPlayerInvItemNum(Victim, x, 0)
                                SetPlayerInvItemValue(Victim, x, 0)
                                SendInventory(Victim)
                            End If
                        End If
                    Next
                End If
            End If

            ' kill player
            KillPlayer(Victim)

            ' Player is dead
            GlobalMsg(GetPlayerName(Victim) & " has been killed by " & Name)
        Else
            ' Player not dead, just do the damage
            SetPlayerVital(Victim, VitalType.HP, GetPlayerVital(Victim, VitalType.HP) - Damage)
            SendVital(Victim, VitalType.HP)
            SendAnimation(MapNum, Npc(MapNpc(GetPlayerMap(Victim)).Npc(MapNpcNum).Num).Animation, 0, 0, TargetType.Player, Victim)

            ' send vitals to party if in one
            If TempPlayer(Victim).InParty > 0 Then SendPartyVitals(TempPlayer(Victim).InParty, Victim)

            ' send the sound
            'SendMapSound Victim, GetPlayerX(Victim), GetPlayerY(Victim), SoundEntity.seNpc, MapNpc(MapNum).Npc(MapNpcNum).Num

            ' Say damage
            SendActionMsg(GetPlayerMap(Victim), "-" & Damage, ColorType.BrightRed, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))
            SendBlood(GetPlayerMap(Victim), GetPlayerX(Victim), GetPlayerY(Victim))

            ' set the regen timer
            TempPlayer(Victim).StopRegen = True
            TempPlayer(Victim).StopRegenTimer = GetTimeMs()

        End If

    End Sub

    Sub NpcAttackNpc(mapNum as Integer, Attacker As Integer, Victim As Integer, Damage As Integer)
        dim buffer as New ByteStream(4)
        Dim aNpcNum As Integer
        Dim vNpcNum As Integer
        Dim n As Integer

        If Attacker <= 0 OrElse Attacker > MAX_MAP_NPCS Then Exit Sub
        If Victim <= 0 OrElse Victim > MAX_MAP_NPCS Then Exit Sub

        If Damage <= 0 Then Exit Sub

        aNpcNum = MapNpc(MapNum).Npc(Attacker).Num
        vNpcNum = MapNpc(MapNum).Npc(Victim).Num

        If aNpcNum <= 0 Then Exit Sub
        If vNpcNum <= 0 Then Exit Sub

        ' Send this packet so they can see the person attacking
        Buffer.WriteInt32(ServerPackets.SNpcAttack)
        Buffer.WriteInt32(Attacker)
        SendDataToMap(MapNum, Buffer.Data, Buffer.Head)
        Buffer.Dispose()

        If Damage >= MapNpc(MapNum).Npc(Victim).Vital(VitalType.HP) Then
            SendActionMsg(MapNum, "-" & Damage, ColorType.BrightRed, 1, (MapNpc(MapNum).Npc(Victim).X * 32), (MapNpc(MapNum).Npc(Victim).Y * 32))
            SendBlood(MapNum, MapNpc(MapNum).Npc(Victim).X, MapNpc(MapNum).Npc(Victim).Y)

            ' npc is dead.

            ' Set NPC target to 0
            MapNpc(MapNum).Npc(Attacker).Target = 0
            MapNpc(MapNum).Npc(Attacker).TargetType = 0

            ' Drop the goods if they get it
            Dim tmpitem = Random(1, 5)
            n = Int(Rnd() * Npc(vNpcNum).DropChance(tmpitem)) + 1
            If n = 1 Then
                SpawnItem(Npc(vNpcNum).DropItem(tmpitem), Npc(vNpcNum).DropItemValue(tmpitem), MapNum, MapNpc(MapNum).Npc(Victim).X, MapNpc(MapNum).Npc(Victim).Y)
            End If

            ' Reset victim's stuff so it dies in loop
            MapNpc(MapNum).Npc(Victim).Num = 0
            MapNpc(MapNum).Npc(Victim).SpawnWait = GetTimeMs()
            MapNpc(MapNum).Npc(Victim).Vital(VitalType.HP) = 0

            ' send npc death packet to map
            Buffer = New ByteStream(4)
            Buffer.WriteInt32(ServerPackets.SNpcDead)
            Buffer.WriteInt32(Victim)
            SendDataToMap(MapNum, Buffer.Data, Buffer.Head)
            Buffer.Dispose()
        Else
            ' npc not dead, just do the damage
            MapNpc(MapNum).Npc(Victim).Vital(VitalType.HP) = MapNpc(MapNum).Npc(Victim).Vital(VitalType.HP) - Damage
            ' Say damage
            SendActionMsg(MapNum, "-" & Damage, ColorType.BrightRed, 1, (MapNpc(MapNum).Npc(Victim).X * 32), (MapNpc(MapNum).Npc(Victim).Y * 32))
            SendBlood(MapNum, MapNpc(MapNum).Npc(Victim).X, MapNpc(MapNum).Npc(Victim).Y)
        End If

    End Sub

    Friend Sub KnockBackNpc(index as integer, NpcNum As Integer, Optional IsSkill As Integer = 0)
        If IsSkill > 0 Then
            For i = 1 To Skill(IsSkill).KnockBackTiles
                If CanNpcMove(GetPlayerMap(Index), NpcNum, GetPlayerDir(Index)) Then
                    NpcMove(GetPlayerMap(Index), NpcNum, GetPlayerDir(Index), MovementType.Walking)
                End If
            Next
            MapNpc(GetPlayerMap(Index)).Npc(NpcNum).StunDuration = 1
            MapNpc(GetPlayerMap(Index)).Npc(NpcNum).StunTimer = GetTimeMs()
        Else
            If Item(GetPlayerEquipment(Index, EquipmentType.Weapon)).KnockBack = 1 Then
                For i = 1 To Item(GetPlayerEquipment(Index, EquipmentType.Weapon)).KnockBackTiles
                    If CanNpcMove(GetPlayerMap(Index), NpcNum, GetPlayerDir(Index)) Then
                        NpcMove(GetPlayerMap(Index), NpcNum, GetPlayerDir(Index), MovementType.Walking)
                    End If
                Next
                MapNpc(GetPlayerMap(Index)).Npc(NpcNum).StunDuration = 1
                MapNpc(GetPlayerMap(Index)).Npc(NpcNum).StunTimer = GetTimeMs()
            End If
        End If
    End Sub

    Friend Function RandomNpcAttack(mapNum as Integer, MapNpcNum As Integer) As Integer
        Dim i As Integer, SkillList As New List(Of Byte)

        RandomNpcAttack = 0

        If MapNpc(MapNum).Npc(MapNpcNum).SkillBuffer > 0 Then Exit Function

        For i = 1 To MAX_NPC_SKILLS
            If Npc(MapNpc(MapNum).Npc(MapNpcNum).Num).Skill(i) > 0 Then
                SkillList.Add(Npc(MapNpc(MapNum).Npc(MapNpcNum).Num).Skill(i))
            End If
        Next

        If SkillList.Count > 1 Then
            RandomNpcAttack = SkillList(Random(0, SkillList.Count - 1))
        Else
            RandomNpcAttack = 0
        End If

    End Function

    Friend Function GetNpcSkill(NpcNum As Integer, skillslot As Integer) As Integer
        GetNpcSkill = Npc(NpcNum).Skill(skillslot)
    End Function

    Friend Sub BufferNpcSkill(mapNum as Integer, MapNpcNum As Integer, skillslot As Integer)
        Dim skillnum As Integer
        Dim MPCost As Integer
        Dim SkillCastType As Integer
        Dim range As Integer
        Dim HasBuffered As Boolean

        Dim TargetType As Byte
        Dim Target As Integer

        ' Prevent subscript out of range
        If skillslot <= 0 OrElse skillslot > MAX_NPC_SKILLS Then Exit Sub

        skillnum = GetNpcSkill(MapNpc(MapNum).Npc(MapNpcNum).Num, skillslot)

        If skillnum <= 0 OrElse skillnum > MAX_SKILLS Then Exit Sub

        ' see if cooldown has finished
        If MapNpc(MapNum).Npc(MapNpcNum).SkillCD(skillslot) > GetTimeMs() Then
            TryNpcAttackPlayer(MapNpcNum, MapNpc(MapNum).Npc(MapNpcNum).Target)
            Exit Sub
        End If

        MPCost = Skill(skillnum).MpCost

        ' Check if they have enough MP
        If MapNpc(MapNum).Npc(MapNpcNum).Vital(VitalType.MP) < MPCost Then Exit Sub

        ' find out what kind of skill it is! self cast, target or AOE
        If Skill(skillnum).Range > 0 Then
            ' ranged attack, single target or aoe?
            If Not Skill(skillnum).IsAoE Then
                SkillCastType = 2 ' targetted
            Else
                SkillCastType = 3 ' targetted aoe
            End If
        Else
            If Not Skill(skillnum).IsAoE Then
                SkillCastType = 0 ' self-cast
            Else
                SkillCastType = 1 ' self-cast AoE
            End If
        End If

        TargetType = MapNpc(MapNum).Npc(MapNpcNum).TargetType
        Target = MapNpc(MapNum).Npc(MapNpcNum).Target
        range = Skill(skillnum).Range
        HasBuffered = False

        Select Case SkillCastType
            Case 0, 1 ' self-cast & self-cast AOE
                HasBuffered = True
            Case 2, 3 ' targeted & targeted AOE
                ' check if have target
                If Not Target > 0 Then
                    Exit Sub
                End If
                If TargetType = Enums.TargetType.Player Then
                    ' if have target, check in range
                    If Not IsInRange(range, MapNpc(MapNum).Npc(MapNpcNum).X, MapNpc(MapNum).Npc(MapNpcNum).Y, GetPlayerX(Target), GetPlayerY(Target)) Then
                        Exit Sub
                    Else
                        HasBuffered = True
                    End If
                ElseIf TargetType = Enums.TargetType.Npc Then
                    '' if have target, check in range
                    'If Not isInRange(range, GetPlayerX(Index), GetPlayerY(Index), MapNpc(MapNum).Npc(Target).x, MapNpc(MapNum).Npc(Target).y) Then
                    '    PlayerMsg(Index, "Target not in range.")
                    '    HasBuffered = False
                    'Else
                    '    ' go through skill types
                    '    If Skill(skillnum).Type <> SkillType.DAMAGEHP AndAlso Skill(skillnum).Type <> SkillType.DAMAGEMP Then
                    '        HasBuffered = True
                    '    Else
                    '        If CanAttackNpc(Index, Target, True) Then
                    '            HasBuffered = True
                    '        End If
                    '    End If
                    'End If
                End If
        End Select

        If HasBuffered Then
            SendAnimation(MapNum, Skill(skillnum).CastAnim, 0, 0, Enums.TargetType.Player, Target)
            MapNpc(MapNum).Npc(MapNpcNum).SkillBuffer = skillslot
            MapNpc(MapNum).Npc(MapNpcNum).SkillBufferTimer = GetTimeMs()
            Exit Sub
        End If
    End Sub

    Friend Function CanNpcBlock(npcnum As Integer) As Boolean
        Dim rate As Integer
        Dim stat As Integer
        Dim rndNum As Integer

        CanNpcBlock = False

        stat = Npc(npcnum).Stat(StatType.Luck) / 5  'guessed shield agility
        rate = stat / 12.08
        rndNum = Random(1, 100)

        If rndNum <= rate Then CanNpcBlock = True

    End Function

    Friend Function CanNpcCrit(npcnum As Integer) As Boolean
        Dim rate As Integer
        Dim rndNum As Integer

        CanNpcCrit = False

        rate = Npc(npcnum).Stat(StatType.Luck) / 3
        rndNum = Random(1, 100)

        If rndNum <= rate Then CanNpcCrit = True

    End Function

    Friend Function CanNpcDodge(npcnum As Integer) As Boolean
        Dim rate As Integer
        Dim rndNum As Integer

        CanNpcDodge = False

        rate = Npc(npcnum).Stat(StatType.Luck) / 4
        rndNum = Random(1, 100)

        If rndNum <= rate Then CanNpcDodge = True

    End Function

    Friend Function CanNpcParry(npcnum As Integer) As Boolean
        Dim rate As Integer
        Dim rndNum As Integer

        CanNpcParry = False

        rate = Npc(npcnum).Stat(StatType.Luck) / 6
        rndNum = Random(1, 100)

        If rndNum <= rate Then CanNpcParry = True

    End Function

    Function GetNpcDamage(npcnum As Integer) As Integer

        GetNpcDamage = (Npc(npcnum).Stat(StatType.Strength) * 2) + (Npc(npcnum).Damage * 2) + (Npc(npcnum).Level * 3) + Random(1, 20)

    End Function

    Friend Sub SpellNpc_Effect(Vital As Byte, increment As Boolean, index as integer, Damage As Integer, Skillnum As Integer, mapNum as Integer)
        Dim sSymbol As String
        Dim Colour As Integer

        If Damage > 0 Then
            If increment Then
                sSymbol = "+"
                If Vital = VitalType.HP Then Colour = ColorType.BrightGreen
                If Vital = VitalType.MP Then Colour = ColorType.BrightBlue
            Else
                sSymbol = "-"
                Colour = ColorType.Blue
            End If

            SendAnimation(MapNum, Skill(Skillnum).SkillAnim, 0, 0, TargetType.Npc, Index)
            SendActionMsg(MapNum, sSymbol & Damage, Colour, ActionMsgType.Scroll, MapNpc(MapNum).Npc(Index).X * 32, MapNpc(MapNum).Npc(Index).Y * 32)

            ' send the sound
            'SendMapSound(Index, MapNpc(MapNum).Npc(Index).x, MapNpc(MapNum).Npc(Index).y, SoundEntity.seSpell, Skillnum)

            If increment Then
                MapNpc(MapNum).Npc(Index).Vital(Vital) = MapNpc(MapNum).Npc(Index).Vital(Vital) + Damage

                If Skill(Skillnum).Duration > 0 Then
                    'AddHoT_Npc(MapNum, Index, Skillnum, 0)
                End If

            ElseIf Not increment Then
                MapNpc(MapNum).Npc(Index).Vital(Vital) = MapNpc(MapNum).Npc(Index).Vital(Vital) - Damage
            End If

        End If

    End Sub

    Friend Function IsNpcDead(mapNum as Integer, MapNpcNum As Integer)
        IsNpcDead = False
        If MapNum < 0 OrElse MapNum > MAX_MAPS OrElse MapNpcNum < 0 OrElse MapNpcNum > MAX_MAP_NPCS Then Exit Function
        If MapNpc(MapNum).Npc(MapNpcNum).Vital(VitalType.HP) <= 0 Then IsNpcDead = True
    End Function

    Friend Sub DropNpcItems(mapNum as Integer, MapNpcNum As Integer)
        Dim NpcNum = MapNpc(MapNum).Npc(MapNpcNum).Num
        Dim tmpitem = Random(1, 5)
        Dim n = Int(Rnd() * Npc(NpcNum).DropChance(tmpitem)) + 1

        If n = 1 Then
            SpawnItem(Npc(NpcNum).DropItem(tmpitem), Npc(NpcNum).DropItemValue(tmpitem), MapNum, MapNpc(MapNum).Npc(MapNpcNum).X, MapNpc(MapNum).Npc(MapNpcNum).Y)
        End If
    End Sub


#End Region

End Module
