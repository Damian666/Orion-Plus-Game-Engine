Imports System.Linq
Imports ASFW

Module S_Players

#Region "PlayerCombat"

    Function CanPlayerAttackPlayer(Attacker As Integer, Victim As Integer, Optional IsSkill As Boolean = False) As Boolean

        If Not IsSkill Then
            ' Check attack timer
            If GetPlayerEquipment(Attacker, EquipmentType.Weapon) > 0 Then
                If GetTimeMs() < TempPlayer(Attacker).AttackTimer + Item(GetPlayerEquipment(Attacker, EquipmentType.Weapon)).Speed Then Exit Function
            Else
                If GetTimeMs() < TempPlayer(Attacker).AttackTimer + 1000 Then Exit Function
            End If
        End If

        ' Check for subscript out of range
        If Not IsPlaying(Victim) Then Exit Function

        ' Make sure they are on the same map
        If Not GetPlayerMap(Attacker) = GetPlayerMap(Victim) Then Exit Function

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(Victim).GettingMap = True Then Exit Function

        If Not IsSkill Then
            ' Check if at same coordinates
            Select Case GetPlayerDir(Attacker)
                Case DirectionType.Up

                    If Not ((GetPlayerY(Victim) + 1 = GetPlayerY(Attacker)) AndAlso (GetPlayerX(Victim) = GetPlayerX(Attacker))) Then Exit Function
                Case DirectionType.Down

                    If Not ((GetPlayerY(Victim) - 1 = GetPlayerY(Attacker)) AndAlso (GetPlayerX(Victim) = GetPlayerX(Attacker))) Then Exit Function
                Case DirectionType.Left

                    If Not ((GetPlayerY(Victim) = GetPlayerY(Attacker)) AndAlso (GetPlayerX(Victim) + 1 = GetPlayerX(Attacker))) Then Exit Function
                Case DirectionType.Right

                    If Not ((GetPlayerY(Victim) = GetPlayerY(Attacker)) AndAlso (GetPlayerX(Victim) - 1 = GetPlayerX(Attacker))) Then Exit Function
                Case Else
                    Exit Function
            End Select
        End If

        ' Check if map is attackable
        If Not Map(GetPlayerMap(Attacker)).Moral = MapMoralType.None Then
            If GetPlayerPK(Victim) = False Then
                PlayerMsg(Attacker, "This is a safe zone!", ColorType.BrightRed)
                Exit Function
            End If
        End If

        ' Make sure they have more then 0 hp
        If GetPlayerVital(Victim, VitalType.HP) <= 0 Then Exit Function

        ' Check to make sure that they dont have access
        If GetPlayerAccess(Attacker) > AdminType.Monitor Then
            PlayerMsg(Attacker, "You cannot attack any player for thou art an admin!", ColorType.BrightRed)
            Exit Function
        End If

        ' Check to make sure the victim isn't an admin
        If GetPlayerAccess(Victim) > AdminType.Monitor Then
            PlayerMsg(Attacker, "You cannot attack " & GetPlayerName(Victim) & "!", ColorType.BrightRed)
            Exit Function
        End If

        ' Make sure attacker is high enough level
        If GetPlayerLevel(Attacker) < 10 Then
            PlayerMsg(Attacker, "You are below level 10, you cannot attack another player yet!", ColorType.BrightRed)
            Exit Function
        End If

        ' Make sure victim is high enough level
        If GetPlayerLevel(Victim) < 10 Then
            PlayerMsg(Attacker, GetPlayerName(Victim) & " is below level 10, you cannot attack this player yet!", ColorType.BrightRed)
            Exit Function
        End If

        CanPlayerAttackPlayer = True
    End Function

    Function CanPlayerBlockHit(index As Integer) As Boolean
        Dim i As Integer
        Dim n As Integer
        Dim ShieldSlot As Integer
        ShieldSlot = GetPlayerEquipment(index, EquipmentType.Shield)

        CanPlayerBlockHit = False

        If ShieldSlot > 0 Then
            n = Int(Rnd() * 2)

            If n = 1 Then
                i = (GetPlayerStat(index, StatType.Endurance) \ 2) + (GetPlayerLevel(index) \ 2)
                n = Int(Rnd() * 100) + 1

                If n <= i Then
                    CanPlayerBlockHit = True
                End If
            End If
        End If

    End Function

    Function CanPlayerCriticalHit(index As Integer) As Boolean
        On Error Resume Next
        Dim i As Integer
        Dim n As Integer

        If GetPlayerEquipment(index, EquipmentType.Weapon) > 0 Then
            n = (Rnd()) * 2

            If n = 1 Then
                i = (GetPlayerStat(index, StatType.Strength) \ 2) + (GetPlayerLevel(index) \ 2)
                n = Int(Rnd() * 100) + 1

                If n <= i Then
                    CanPlayerCriticalHit = True
                End If
            End If
        End If

    End Function

    Function GetPlayerDamage(index As Integer) As Integer
        Dim weaponNum As Integer

        GetPlayerDamage = 0

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse index <= 0 OrElse index > MAX_PLAYERS Then
            Exit Function
        End If

        If GetPlayerEquipment(index, EquipmentType.Weapon) > 0 Then
            weaponNum = GetPlayerEquipment(index, EquipmentType.Weapon)
            GetPlayerDamage = (GetPlayerStat(index, StatType.Strength) * 2) + (Item(weaponNum).Data2 * 2) + (GetPlayerLevel(index) * 3) + Random(0, 20)
        Else
            GetPlayerDamage = (GetPlayerStat(index, StatType.Strength) * 2) + (GetPlayerLevel(index) * 3) + Random(0, 20)
        End If

    End Function

    Function GetPlayerProtection(index As Integer) As Integer
        Dim Armor As Integer, Helm As Integer, Shoes As Integer, Gloves As Integer
        GetPlayerProtection = 0

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse index <= 0 OrElse index > MAX_PLAYERS Then
            Exit Function
        End If

        Armor = GetPlayerEquipment(index, EquipmentType.Armor)
        Helm = GetPlayerEquipment(index, EquipmentType.Helmet)
        Shoes = GetPlayerEquipment(index, EquipmentType.Shoes)
        Gloves = GetPlayerEquipment(index, EquipmentType.Gloves)
        GetPlayerProtection = (GetPlayerStat(index, StatType.Endurance) \ 5)

        If Armor > 0 Then
            GetPlayerProtection += Item(Armor).Data2
        End If

        If Helm > 0 Then
            GetPlayerProtection += Item(Helm).Data2
        End If

        If Shoes > 0 Then
            GetPlayerProtection += Item(Shoes).Data2
        End If

        If Gloves > 0 Then
            GetPlayerProtection += Item(Gloves).Data2
        End If

    End Function

    Sub AttackPlayer(Attacker As Integer, Victim As Integer, Damage As Integer, Optional skillnum As Integer = 0, Optional npcnum As Integer = 0)
        Dim exp As Integer, mapNum As Integer
        Dim n As Integer
        Dim buffer As ByteStream

        If npcnum = 0 Then
            ' Check for subscript out of range
            If IsPlaying(Attacker) = False OrElse IsPlaying(Victim) = False OrElse Damage < 0 Then
                Exit Sub
            End If

            ' Check for weapon

            If GetPlayerEquipment(Attacker, EquipmentType.Weapon) > 0 Then
                n = GetPlayerEquipment(Attacker, EquipmentType.Weapon)
            End If

            ' Send this packet so they can see the person attacking
            buffer = New ByteStream(4)
            buffer.WriteInt32(ServerPackets.SAttack)
            buffer.WriteInt32(Attacker)
            SendDataToMapBut(Attacker, GetPlayerMap(Attacker), buffer.Data, buffer.Head)
            buffer.Dispose()

            If Damage >= GetPlayerVital(Victim, VitalType.HP) Then

                SendActionMsg(GetPlayerMap(Victim), "-" & Damage, ColorType.BrightRed, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))

                ' Player is dead
                GlobalMsg(GetPlayerName(Victim) & " has been killed by " & GetPlayerName(Attacker))
                ' Calculate exp to give attacker
                exp = (GetPlayerExp(Victim) \ 10)

                ' Make sure we dont get less then 0
                If exp < 0 Then
                    exp = 0
                End If

                If exp = 0 Then
                    PlayerMsg(Victim, "You lost no exp.", ColorType.BrightGreen)
                    PlayerMsg(Attacker, "You received no exp.", ColorType.BrightRed)
                Else
                    SetPlayerExp(Victim, GetPlayerExp(Victim) - exp)
                    SendExp(Victim)
                    PlayerMsg(Victim, "You lost " & exp & " exp.", ColorType.BrightRed)
                    SetPlayerExp(Attacker, GetPlayerExp(Attacker) + exp)
                    SendExp(Attacker)
                    PlayerMsg(Attacker, "You received " & exp & " exp.", ColorType.BrightGreen)
                End If

                ' Check for a level up
                CheckPlayerLevelUp(Attacker)

                ' Check if target is player who died and if so set target to 0
                If TempPlayer(Attacker).TargetType = TargetType.Player Then
                    If TempPlayer(Attacker).Target = Victim Then
                        TempPlayer(Attacker).Target = 0
                        TempPlayer(Attacker).TargetType = TargetType.None
                    End If
                End If

                If GetPlayerPK(Victim) = False Then
                    If GetPlayerPK(Attacker) = False Then
                        SetPlayerPK(Attacker, True)
                        SendPlayerData(Attacker)
                        GlobalMsg(GetPlayerName(Attacker) & " has been deemed a Player Killer!!!")
                    End If
                Else
                    GlobalMsg(GetPlayerName(Victim) & " has paid the price for being a Player Killer!!!")
                End If

                OnDeath(Victim)
            Else
                ' Player not dead, just do the damage
                SetPlayerVital(Victim, VitalType.HP, GetPlayerVital(Victim, VitalType.HP) - Damage)
                SendVital(Victim, VitalType.HP)
                SendActionMsg(GetPlayerMap(Victim), "-" & Damage, ColorType.BrightRed, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))

                'if a stunning skill, stun the player
                If skillnum > 0 Then
                    If Skill(skillnum).StunDuration > 0 Then StunPlayer(Victim, skillnum)
                End If
            End If

            ' Reset attack timer
            TempPlayer(Attacker).AttackTimer = GetTimeMs()
        Else ' npc to player
            ' Check for subscript out of range
            If IsPlaying(Victim) = False OrElse Damage < 0 Then Exit Sub

            mapNum = GetPlayerMap(Victim)

            ' Send this packet so they can see the person attacking
            buffer = New ByteStream(4)
            buffer.WriteInt32(ServerPackets.SNpcAttack)
            buffer.WriteInt32(Attacker)
            SendDataToMap(mapNum, buffer.Data, buffer.Head)
            buffer.Dispose()

            If Damage >= GetPlayerVital(Victim, VitalType.HP) Then

                SendActionMsg(mapNum, "-" & Damage, ColorType.BrightRed, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))

                ' Player is dead
                GlobalMsg(GetPlayerName(Victim) & " has been killed by " & Npc(MapNpc(mapNum).Npc(Attacker).Num).Name)

                ' Check if target is player who died and if so set target to 0
                If TempPlayer(Attacker).TargetType = TargetType.Player Then
                    If TempPlayer(Attacker).Target = Victim Then
                        TempPlayer(Attacker).Target = 0
                        TempPlayer(Attacker).TargetType = TargetType.None
                    End If
                End If

                OnDeath(Victim)
            Else
                ' Player not dead, just do the damage
                SetPlayerVital(Victim, VitalType.HP, GetPlayerVital(Victim, VitalType.HP) - Damage)
                SendVital(Victim, VitalType.HP)
                SendActionMsg(mapNum, "-" & Damage, ColorType.BrightRed, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))

                'if a stunning skill, stun the player
                If skillnum > 0 Then
                    If Skill(skillnum).StunDuration > 0 Then StunPlayer(Victim, skillnum)
                End If
            End If

            ' Reset attack timer
            MapNpc(mapNum).Npc(Attacker).AttackTimer = GetTimeMs()
        End If

    End Sub

    Friend Sub StunPlayer(index As Integer, skillnum As Integer)
        ' check if it's a stunning skill
        If Skill(skillnum).StunDuration > 0 Then
            ' set the values on index
            TempPlayer(index).StunDuration = Skill(skillnum).StunDuration
            TempPlayer(index).StunTimer = GetTimeMs()
            ' send it to the index
            SendStunned(index)
            ' tell him he's stunned
            PlayerMsg(index, "You have been stunned!", ColorType.Yellow)
        End If
    End Sub

    Function CanPlayerAttackNpc(Attacker As Integer, MapNpcNum As Integer, Optional IsSkill As Boolean = False) As Boolean
        Dim mapNum As Integer
        Dim NpcNum As Integer
        Dim atkX As Integer
        Dim atkY As Integer
        Dim attackspeed As Integer

        ' Check for subscript out of range
        If IsPlaying(Attacker) = False OrElse MapNpcNum <= 0 OrElse MapNpcNum > MAX_MAP_NPCS Then
            Exit Function
        End If

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(Attacker)).Npc(MapNpcNum).Num <= 0 Then
            Exit Function
        End If

        mapNum = GetPlayerMap(Attacker)
        NpcNum = MapNpc(mapNum).Npc(MapNpcNum).Num

        ' Make sure the npc isn't already dead
        If MapNpc(mapNum).Npc(MapNpcNum).Vital(VitalType.HP) <= 0 Then
            Exit Function
        End If

        ' Make sure they are on the same map
        If IsPlaying(Attacker) Then

            ' attack speed from weapon
            If GetPlayerEquipment(Attacker, EquipmentType.Weapon) > 0 Then
                attackspeed = Item(GetPlayerEquipment(Attacker, EquipmentType.Weapon)).Speed
            Else
                attackspeed = 1000
            End If

            If NpcNum > 0 AndAlso GetTimeMs() > TempPlayer(Attacker).AttackTimer + attackspeed Then

                ' exit out early
                If IsSkill Then
                    If Npc(NpcNum).Behaviour <> NpcBehavior.Friendly AndAlso Npc(NpcNum).Behaviour <> NpcBehavior.ShopKeeper Then
                        CanPlayerAttackNpc = True
                        Exit Function
                    End If
                End If

                ' Check if at same coordinates
                Select Case GetPlayerDir(Attacker)
                    Case DirectionType.Up
                        atkX = GetPlayerX(Attacker)
                        atkY = GetPlayerY(Attacker) - 1
                    Case DirectionType.Down
                        atkX = GetPlayerX(Attacker)
                        atkY = GetPlayerY(Attacker) + 1
                    Case DirectionType.Left
                        atkX = GetPlayerX(Attacker) - 1
                        atkY = GetPlayerY(Attacker)
                    Case DirectionType.Right
                        atkX = GetPlayerX(Attacker) + 1
                        atkY = GetPlayerY(Attacker)
                End Select

                If atkX = MapNpc(mapNum).Npc(MapNpcNum).X Then
                    If atkY = MapNpc(mapNum).Npc(MapNpcNum).Y Then
                        If Npc(NpcNum).Behaviour <> NpcBehavior.Friendly AndAlso Npc(NpcNum).Behaviour <> NpcBehavior.ShopKeeper AndAlso Npc(NpcNum).Behaviour <> NpcBehavior.Quest Then
                            CanPlayerAttackNpc = True
                        Else
                            If Npc(NpcNum).Behaviour = NpcBehavior.Quest Then
                                If QuestCompleted(Attacker, Npc(NpcNum).QuestNum) Then
                                    PlayerMsg(Attacker, Trim$(Npc(NpcNum).Name) & ": " & Trim$(Npc(NpcNum).AttackSay), ColorType.Yellow)
                                    Exit Function
                                ElseIf Not CanStartQuest(Attacker, Npc(NpcNum).QuestNum) AndAlso Not QuestInProgress(Attacker, Npc(NpcNum).QuestNum) Then
                                    CheckTasks(Attacker, QuestType.Talk, NpcNum)
                                    CheckTasks(Attacker, QuestType.Give, NpcNum)
                                    CheckTasks(Attacker, QuestType.Fetch, NpcNum)
                                    Exit Function
                                ElseIf QuestInProgress(Attacker, Npc(NpcNum).QuestNum) Then
                                    CheckTasks(Attacker, QuestType.Talk, NpcNum)
                                    CheckTasks(Attacker, QuestType.Give, NpcNum)
                                    CheckTasks(Attacker, QuestType.Fetch, NpcNum)
                                    Exit Function
                                Else
                                    ShowQuest(Attacker, Npc(NpcNum).QuestNum)
                                    Exit Function
                                End If
                            ElseIf Npc(NpcNum).Behaviour = NpcBehavior.Friendly OrElse Npc(NpcNum).Behaviour = NpcBehavior.ShopKeeper Then
                                CheckTasks(Attacker, QuestType.Talk, NpcNum)
                                CheckTasks(Attacker, QuestType.Give, NpcNum)
                                CheckTasks(Attacker, QuestType.Fetch, NpcNum)
                                'Exit Function
                            End If
                            If Len(Trim$(Npc(NpcNum).AttackSay)) > 0 Then
                                PlayerMsg(Attacker, Trim$(Npc(NpcNum).Name) & ": " & Trim$(Npc(NpcNum).AttackSay), ColorType.Yellow)
                            End If
                        End If
                    End If
                End If
            End If
        End If

    End Function

    Friend Sub StunNPC(index As Integer, mapNum As Integer, skillnum As Integer)
        ' check if it's a stunning skill
        If Skill(skillnum).StunDuration > 0 Then
            ' set the values on index
            MapNpc(mapNum).Npc(index).StunDuration = Skill(skillnum).StunDuration
            MapNpc(mapNum).Npc(index).StunTimer = GetTimeMs()
        End If
    End Sub

    Sub PlayerAttackNpc(Attacker As Integer, MapNpcNum As Integer, Damage As Integer)
        ' Check for subscript out of range
        If IsPlaying(Attacker) = False OrElse MapNpcNum <= 0 OrElse MapNpcNum > MAX_MAP_NPCS OrElse Damage < 0 Then Exit Sub

        Dim MapNum = GetPlayerMap(Attacker)
        Dim NpcNum = MapNpc(MapNum).Npc(MapNpcNum).Num
        Dim Name = Npc(NpcNum).Name.Trim()

        ' Check for weapon
        Dim Weapon = 0
        If GetPlayerEquipment(Attacker, EquipmentType.Weapon) > 0 Then
            Weapon = GetPlayerEquipment(Attacker, EquipmentType.Weapon)
        End If

        ' Deal damage to our NPC.
        MapNpc(MapNum).Npc(MapNpcNum).Vital(VitalType.HP) = MapNpc(MapNum).Npc(MapNpcNum).Vital(VitalType.HP) - Damage

        ' Set the NPC target to the player so they can come after them.
        MapNpc(MapNum).Npc(MapNpcNum).TargetType = TargetType.Player
        MapNpc(MapNum).Npc(MapNpcNum).Target = Attacker

        ' Check for any mobs on the map with the Guard behaviour so they can come after our player.
        If Npc(MapNpc(MapNum).Npc(MapNpcNum).Num).Behaviour = NpcBehavior.Guard Then
            For Each Guard In MapNpc(MapNum).Npc.Where(Function(x) x.Num = MapNpc(MapNum).Npc(MapNpcNum).Num).Select(Function(x, y) y + 1).ToArray()
                MapNpc(MapNum).Npc(Guard).Target = Attacker
                MapNpc(MapNum).Npc(Guard).TargetType = TargetType.Player
            Next
        End If

        ' Send our general visual stuff.
        SendActionMsg(MapNum, "-" & Damage, ColorType.BrightRed, 1, (MapNpc(MapNum).Npc(MapNpcNum).X * 32), (MapNpc(MapNum).Npc(MapNpcNum).Y * 32))
        SendBlood(GetPlayerMap(Attacker), MapNpc(MapNum).Npc(MapNpcNum).X, MapNpc(MapNum).Npc(MapNpcNum).Y)
        SendPlayerAttack(Attacker)
        If Weapon > 0 Then
            SendAnimation(MapNum, Item(GetPlayerEquipment(Attacker, EquipmentType.Weapon)).Animation, 0, 0, TargetType.Npc, MapNpcNum)
        End If

        ' Reset our attack timer.
        TempPlayer(Attacker).AttackTimer = GetTimeMs()

        If Not IsNpcDead(MapNum, MapNpcNum) Then
            ' Check if our NPC has something to share with our player.
            If MapNpc(MapNum).Npc(MapNpcNum).Target = 0 Then
                If Len(Trim$(Npc(NpcNum).AttackSay)) > 0 Then
                    PlayerMsg(Attacker, String.Format("{0} says: '{1}'", Npc(NpcNum).Name.Trim(), Npc(NpcNum).AttackSay.Trim()), ColorType.Yellow)
                End If
            End If

            SendMapNpcTo(MapNum, MapNpcNum)
        Else
            HandlePlayerKillNpc(MapNum, Attacker, MapNpcNum)
        End If
    End Sub

    Function IsInRange(range As Integer, x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer) As Boolean
        Dim nVal As Integer
        IsInRange = False
        nVal = Math.Sqrt((x1 - x2) ^ 2 + (y1 - y2) ^ 2)
        If nVal <= range Then IsInRange = True
    End Function

    Friend Sub SpellPlayer_Effect(Vital As Byte, increment As Boolean, index As Integer, Damage As Integer, Skillnum As Integer)
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

            SendAnimation(GetPlayerMap(index), Skill(Skillnum).SkillAnim, 0, 0, TargetType.Player, index)
            SendActionMsg(GetPlayerMap(index), sSymbol & Damage, Colour, ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32)

            ' send the sound
            'SendMapSound Index, GetPlayerX(Index), GetPlayerY(Index), SoundEntity.seSpell, Spellnum

            If increment Then
                SetPlayerVital(index, Vital, GetPlayerVital(index, Vital) + Damage)

                If Skill(Skillnum).Duration > 0 Then
                    'AddHoT_Player(Index, Spellnum)
                End If

            ElseIf Not increment Then
                SetPlayerVital(index, Vital, GetPlayerVital(index, Vital) - Damage)
            End If

            SendVital(index, Vital)

        End If

    End Sub

    Friend Function CanPlayerDodge(index As Integer) As Boolean
        Dim rate As Integer, rndNum As Integer

        CanPlayerDodge = False

        rate = GetPlayerStat(index, StatType.Luck) / 4
        rndNum = Random(1, 100)

        If rndNum <= rate Then
            CanPlayerDodge = True
        End If

    End Function

    Friend Function CanPlayerParry(index As Integer) As Boolean
        Dim rate As Integer, rndNum As Integer

        CanPlayerParry = False

        rate = GetPlayerStat(index, StatType.Luck) / 6
        rndNum = Random(1, 100)

        If rndNum <= rate Then
            CanPlayerParry = True
        End If

    End Function

    Friend Sub TryPlayerAttackPlayer(Attacker As Integer, Victim As Integer)
        Dim mapNum As Integer
        Dim Damage As Integer, i As Integer, armor As Integer

        Damage = 0

        ' Can we attack the player?
        If CanPlayerAttackPlayer(Attacker, Victim) Then

            mapNum = GetPlayerMap(Attacker)

            ' check if NPC can avoid the attack
            If CanPlayerDodge(Victim) Then
                SendActionMsg(mapNum, "Dodge!", ColorType.Pink, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))
                Exit Sub
            End If

            If CanPlayerParry(Victim) Then
                SendActionMsg(mapNum, "Parry!", ColorType.Pink, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            Damage = GetPlayerDamage(Attacker)

            If CanPlayerBlockHit(Victim) Then
                SendActionMsg(mapNum, "Block!", ColorType.BrightCyan, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))
                Damage = 0
                Exit Sub
            Else

                For i = 1 To EquipmentType.Count - 1
                    If GetPlayerEquipment(Victim, i) > 0 Then
                        armor += Item(GetPlayerEquipment(Victim, i)).Data2
                    End If
                Next

                ' take away armour
                Damage -= (GetPlayerStat(Victim, StatType.Spirit) * 2) + (GetPlayerLevel(Victim) * 3) + armor

                ' * 1.5 if it's a crit!
                If CanPlayerCriticalHit(Attacker) Then
                    Damage *= 1.5
                    SendActionMsg(mapNum, "Critical!", ColorType.BrightCyan, 1, GetPlayerX(Attacker) * 32, GetPlayerY(Attacker) * 32)
                End If
            End If

            If Damage > 0 Then
                PlayerAttackPlayer(Attacker, Victim, Damage)
            Else
                PlayerMsg(Attacker, "Your attack does nothing.", ColorType.BrightRed)
            End If

        End If

    End Sub

    Sub PlayerAttackPlayer(Attacker As Integer, Victim As Integer, Damage As Integer)
        ' Check for subscript out of range
        If IsPlaying(Attacker) = False OrElse IsPlaying(Victim) = False OrElse Damage <= 0 Then
            Exit Sub
        End If

        ' Check if our assailant has a weapon.
        Dim Weapon = 0
        If GetPlayerEquipment(Attacker, EquipmentType.Weapon) > 0 Then
            Weapon = GetPlayerEquipment(Attacker, EquipmentType.Weapon)
        End If

        ' Stop our player's regeneration abilities.
        TempPlayer(Attacker).StopRegen = True
        TempPlayer(Attacker).StopRegenTimer = GetTimeMs()

        ' Deal damage to our player.
        SetPlayerVital(Victim, VitalType.HP, GetPlayerVital(Victim, VitalType.HP) - Damage)

        ' Send all the visuals to our player.
        If Weapon > 0 Then
            SendAnimation(GetPlayerMap(Victim), Item(Weapon).Animation, 0, 0, TargetType.Player, Victim)
        End If
        SendActionMsg(GetPlayerMap(Victim), "-" & Damage, ColorType.BrightRed, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))
        SendBlood(GetPlayerMap(Victim), GetPlayerX(Victim), GetPlayerY(Victim))

        ' set the regen timer
        TempPlayer(Victim).StopRegen = True
        TempPlayer(Victim).StopRegenTimer = GetTimeMs()

        ' Reset attack timer
        TempPlayer(Attacker).AttackTimer = GetTimeMs()

        If Not IsPlayerDead(Victim) Then
            ' Send our player's new vitals to everyone that needs them.
            SendVital(Victim, VitalType.HP)
            If TempPlayer(Victim).InParty > 0 Then SendPartyVitals(TempPlayer(Victim).InParty, Victim)
        Else
            ' Handle our dead player.
            HandlePlayerKillPlayer(Attacker, Victim)
        End If
    End Sub

    Friend Sub TryPlayerAttackNpc(index As Integer, mapnpcnum As Integer)

        Dim npcnum As Integer

        Dim mapNum As Integer

        Dim Damage As Integer

        Damage = 0

        ' Can we attack the npc?
        If CanPlayerAttackNpc(index, mapnpcnum) Then

            mapNum = GetPlayerMap(index)
            npcnum = MapNpc(mapNum).Npc(mapnpcnum).Num

            ' check if NPC can avoid the attack
            If CanNpcDodge(npcnum) Then
                SendActionMsg(mapNum, "Dodge!", ColorType.Pink, 1, (MapNpc(mapNum).Npc(mapnpcnum).X * 32), (MapNpc(mapNum).Npc(mapnpcnum).Y * 32))
                Exit Sub
            End If

            If CanNpcParry(npcnum) Then
                SendActionMsg(mapNum, "Parry!", ColorType.Pink, 1, (MapNpc(mapNum).Npc(mapnpcnum).X * 32), (MapNpc(mapNum).Npc(mapnpcnum).Y * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            Damage = GetPlayerDamage(index)

            If CanNpcBlock(npcnum) Then
                SendActionMsg(mapNum, "Block!", ColorType.BrightCyan, 1, (MapNpc(mapNum).Npc(mapnpcnum).X * 32), (MapNpc(mapNum).Npc(mapnpcnum).Y * 32))
                Damage = 0
                Exit Sub
            Else

                Damage -= ((Npc(npcnum).Stat(StatType.Spirit) * 2) + (Npc(npcnum).Level * 3))

                ' * 1.5 if it's a crit!
                If CanPlayerCriticalHit(index) Then
                    Damage *= 1.5
                    SendActionMsg(mapNum, "Critical!", ColorType.BrightCyan, 1, (GetPlayerX(index) * 32), (GetPlayerY(index) * 32))
                End If

            End If

            TempPlayer(index).Target = mapnpcnum
            TempPlayer(index).TargetType = TargetType.Npc
            SendTarget(index, mapnpcnum, TargetType.Npc)

            If Damage > 0 Then
                PlayerAttackNpc(index, mapnpcnum, Damage)
            Else
                PlayerMsg(index, "Your attack does nothing.", ColorType.BrightRed)
            End If

        End If

    End Sub

    Friend Function IsPlayerDead(index As Integer)
        IsPlayerDead = False
        If index < 0 OrElse index > MAX_PLAYERS OrElse Not TempPlayer(index).InGame Then Exit Function
        If GetPlayerVital(index, VitalType.HP) <= 0 Then IsPlayerDead = True
    End Function

    Friend Sub HandlePlayerKillPlayer(Attacker As Integer, Victim As Integer)
        ' Notify everyone that our player has bit the dust.
        GlobalMsg(String.Format("{0} has been killed by {1}!", GetPlayerName(Victim), GetPlayerName(Attacker)))

        ' Hand out player experience
        HandlePlayerKillExperience(Attacker, Victim)

        ' Handle our PK outcomes.
        HandlePlayerKilledPK(Attacker, Victim)

        ' Remove our player from everyone's target list.
        For Each p In TempPlayer.Where(Function(x, i) x.InGame AndAlso GetPlayerMap(i + 1) = GetPlayerMap(Victim) AndAlso x.TargetType = TargetType.Player AndAlso x.Target = Victim).Select(Function(x, i) i + 1).ToArray()
            TempPlayer(p).Target = 0
            TempPlayer(p).TargetType = TargetType.None
            SendTarget(p, 0, TargetType.None)
        Next

        ' Actually kill the player.
        OnDeath(Victim)

        ' Handle our quest system stuff.
        CheckTasks(Attacker, QuestType.Kill, 0)
    End Sub

    Friend Sub HandlePlayerKillNpc(mapNum As Integer, index As Integer, MapNpcNum As Integer)
        ' Set our attacker's target to nothing.
        SendTarget(index, 0, TargetType.None)

        ' Hand out player experience
        HandleNpcKillExperience(index, MapNpc(mapNum).Npc(MapNpcNum).Num)

        ' Drop items if we can.
        DropNpcItems(mapNum, MapNpcNum)

        ' Handle quest tasks related to NPC death
        CheckTasks(index, QuestType.Slay, MapNpc(mapNum).Npc(MapNpcNum).Num)

        ' Set our NPC's data to default so we know it's dead.
        MapNpc(mapNum).Npc(MapNpcNum).Num = 0
        MapNpc(mapNum).Npc(MapNpcNum).SpawnWait = GetTimeMs()
        MapNpc(mapNum).Npc(MapNpcNum).Vital(VitalType.HP) = 0

        ' Notify all our clients that the NPC has died.
        SendNpcDead(mapNum, MapNpcNum)

        ' Check if our dead NPC is targetted by another player and remove their targets.
        For Each p In TempPlayer.Where(Function(x, i) x.InGame AndAlso GetPlayerMap(i + 1) = mapNum AndAlso x.TargetType = TargetType.Npc AndAlso x.Target = MapNpcNum).Select(Function(x, i) i + 1).ToArray()
            TempPlayer(p).Target = 0
            TempPlayer(p).TargetType = TargetType.None
            SendTarget(p, 0, TargetType.None)
        Next
    End Sub

    Friend Sub HandlePlayerKilledPK(Attacker As Integer, Victim As Integer)
        ' TODO: Redo this method, it is horrendous.
        Dim z As Integer, eqcount As Integer, invcount, j As Integer
        If GetPlayerPK(Victim) = 0 Then
            If GetPlayerPK(Attacker) = 0 Then
                SetPlayerPK(Attacker, 1)
                SendPlayerData(Attacker)
                GlobalMsg(GetPlayerName(Attacker) & " has been deemed a Player Killer!!!")
            End If
        Else
            GlobalMsg(GetPlayerName(Victim) & " has paid the price for being a Player Killer!!!")
        End If

        If GetPlayerLevel(Victim) >= 10 Then

            For z = 1 To MAX_INV
                If GetPlayerInvItemNum(Victim, z) > 0 Then
                    invcount += 1
                End If
            Next

            For z = 1 To EquipmentType.Count - 1
                If GetPlayerEquipment(Victim, z) > 0 Then
                    eqcount += 1
                End If
            Next
            z = Random(1, invcount + eqcount)

            If z = 0 Then z = 1
            If z > invcount + eqcount Then z = invcount + eqcount
            If z > invcount Then
                z -= invcount

                For x = 1 To EquipmentType.Count - 1
                    If GetPlayerEquipment(Victim, x) > 0 Then
                        j += 1

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
                        j += 1

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
    End Sub

#End Region

#Region "Data"

    Function GetPlayerLogin(index As Integer) As String
        GetPlayerLogin = Trim$(Player(index).Login)
    End Function

    Function GetPlayerName(index As Integer) As String
        GetPlayerName = ""
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerName = Player(index).Character(TempPlayer(index).CurChar).Name.Trim()
    End Function

    Sub SetPlayerAccess(index As Integer, Access As Integer)
        Player(index).Access = Access
    End Sub

    Sub SetPlayerSprite(index As Integer, Sprite As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Sprite = Sprite
    End Sub

    Function GetPlayerMaxVital(index As Integer, Vital As VitalType) As Integer

        GetPlayerMaxVital = 0

        If index > MAX_PLAYERS Then Exit Function

        Select Case Vital
            Case VitalType.HP
                GetPlayerMaxVital = (Player(index).Character(TempPlayer(index).CurChar).Level + (GetPlayerStat(index, StatType.Vitality) \ 2) + Classes(Player(index).Character(TempPlayer(index).CurChar).Classes).Stat(StatType.Vitality)) * 2
            Case VitalType.MP
                GetPlayerMaxVital = (Player(index).Character(TempPlayer(index).CurChar).Level + (GetPlayerStat(index, StatType.Intelligence) \ 2) + Classes(Player(index).Character(TempPlayer(index).CurChar).Classes).Stat(StatType.Intelligence)) * 2
            Case VitalType.SP
                GetPlayerMaxVital = (Player(index).Character(TempPlayer(index).CurChar).Level + (GetPlayerStat(index, StatType.Spirit) \ 2) + Classes(Player(index).Character(TempPlayer(index).CurChar).Classes).Stat(StatType.Spirit)) * 2
        End Select

    End Function

    Friend Function GetPlayerStat(index As Integer, Stat As StatType) As Integer
        Dim x As Integer, i As Integer

        GetPlayerStat = 0

        If index > MAX_PLAYERS Then Exit Function

        x = Player(index).Character(TempPlayer(index).CurChar).Stat(Stat)

        For i = 1 To EquipmentType.Count - 1
            If Player(index).Character(TempPlayer(index).CurChar).Equipment(i) > 0 Then
                If Item(Player(index).Character(TempPlayer(index).CurChar).Equipment(i)).Add_Stat(Stat) > 0 Then
                    x += Item(Player(index).Character(TempPlayer(index).CurChar).Equipment(i)).Add_Stat(Stat)
                End If
            End If
        Next

        GetPlayerStat = x
    End Function

    Function GetPlayerAccess(index As Integer) As Integer
        GetPlayerAccess = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerAccess = Player(index).Access
    End Function

    Function GetPlayerMap(index As Integer) As Integer
        GetPlayerMap = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerMap = Player(index).Character(TempPlayer(index).CurChar).Map
    End Function

    Function GetPlayerX(index As Integer) As Integer
        GetPlayerX = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerX = Player(index).Character(TempPlayer(index).CurChar).X
    End Function

    Function GetPlayerY(index As Integer) As Integer
        GetPlayerY = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerY = Player(index).Character(TempPlayer(index).CurChar).Y
    End Function

    Function GetPlayerDir(index As Integer) As Integer
        GetPlayerDir = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerDir = Player(index).Character(TempPlayer(index).CurChar).Dir
    End Function

    Function GetPlayerSprite(index As Integer) As Integer
        GetPlayerSprite = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerSprite = Player(index).Character(TempPlayer(index).CurChar).Sprite
    End Function

    Function GetPlayerPK(index As Integer) As Integer
        GetPlayerPK = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerPK = Player(index).Character(TempPlayer(index).CurChar).Pk
    End Function

    Function GetPlayerEquipment(index As Integer, EquipmentSlot As EquipmentType) As Integer
        GetPlayerEquipment = 0
        If index > MAX_PLAYERS Then Exit Function
        If EquipmentSlot = 0 Then Exit Function
        GetPlayerEquipment = Player(index).Character(TempPlayer(index).CurChar).Equipment(EquipmentSlot)
    End Function

    Sub SetPlayerEquipment(index As Integer, InvNum As Integer, EquipmentSlot As EquipmentType)
        Player(index).Character(TempPlayer(index).CurChar).Equipment(EquipmentSlot) = InvNum
    End Sub

    Sub SetPlayerDir(index As Integer, Dir As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Dir = Dir
    End Sub

    Sub SetPlayerVital(index As Integer, Vital As VitalType, Value As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Vital(Vital) = Value

        If GetPlayerVital(index, Vital) > GetPlayerMaxVital(index, Vital) Then
            Player(index).Character(TempPlayer(index).CurChar).Vital(Vital) = GetPlayerMaxVital(index, Vital)
        End If

        If GetPlayerVital(index, Vital) < 0 Then
            Player(index).Character(TempPlayer(index).CurChar).Vital(Vital) = 0
        End If

    End Sub

    Friend Function IsDirBlocked(ByRef Blockvar As Byte, ByRef Dir As Byte) As Boolean
        Return Not (Not Blockvar AndAlso (2 ^ Dir))
    End Function

    Function GetPlayerVital(index As Integer, Vital As VitalType) As Integer
        GetPlayerVital = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerVital = Player(index).Character(TempPlayer(index).CurChar).Vital(Vital)
    End Function

    Function GetPlayerLevel(index As Integer) As Integer
        GetPlayerLevel = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerLevel = Player(index).Character(TempPlayer(index).CurChar).Level
    End Function

    Function GetPlayerPOINTS(index As Integer) As Integer
        GetPlayerPOINTS = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerPOINTS = Player(index).Character(TempPlayer(index).CurChar).Points
    End Function

    Function GetPlayerNextLevel(index As Integer) As Integer
        GetPlayerNextLevel = ((GetPlayerLevel(index) + 1) * (GetPlayerStat(index, StatType.Strength) + GetPlayerStat(index, StatType.Endurance) + GetPlayerStat(index, StatType.Intelligence) + GetPlayerStat(index, StatType.Spirit) + GetPlayerPOINTS(index)) + StatPtsPerLvl) * Classes(GetPlayerClass(index)).BaseExp '25
    End Function

    Function GetPlayerExp(index As Integer) As Integer
        GetPlayerExp = Player(index).Character(TempPlayer(index).CurChar).Exp
    End Function

    Sub SetPlayerMap(index As Integer, mapNum As Integer)
        If mapNum > 0 AndAlso mapNum <= MAX_CACHED_MAPS Then
            Player(index).Character(TempPlayer(index).CurChar).Map = mapNum
        End If
    End Sub

    Sub SetPlayerX(index As Integer, X As Integer)
        Player(index).Character(TempPlayer(index).CurChar).X = X
    End Sub

    Sub SetPlayerY(index As Integer, Y As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Y = Y
    End Sub

    Sub SetPlayerExp(index As Integer, Exp As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Exp = Exp
    End Sub

    Friend Function GetPlayerRawStat(index As Integer, Stat As StatType) As Integer
        GetPlayerRawStat = 0
        If index > MAX_PLAYERS Then Exit Function

        GetPlayerRawStat = Player(index).Character(TempPlayer(index).CurChar).Stat(Stat)
    End Function

    Friend Sub SetPlayerStat(index As Integer, Stat As StatType, Value As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Stat(Stat) = Value
    End Sub

    Sub SetPlayerLevel(index As Integer, Level As Integer)

        If Level > MAX_LEVELS Then Exit Sub
        Player(index).Character(TempPlayer(index).CurChar).Level = Level
    End Sub

    Sub SetPlayerPOINTS(index As Integer, Points As Integer)
        If Player(index).Character(TempPlayer(index).CurChar).Points + Points > 255 Then
            Player(index).Character(TempPlayer(index).CurChar).Points = 255
        Else
            Player(index).Character(TempPlayer(index).CurChar).Points = Points
        End If
    End Sub

    Sub CheckPlayerLevelUp(index As Integer)
        Dim expRollover As Integer
        Dim level_count As Integer

        level_count = 0

        Do While GetPlayerExp(index) >= GetPlayerNextLevel(index)
            expRollover = GetPlayerExp(index) - GetPlayerNextLevel(index)
            SetPlayerLevel(index, GetPlayerLevel(index) + 1)
            SetPlayerPOINTS(index, GetPlayerPOINTS(index) + 3)
            SetPlayerExp(index, expRollover)
            level_count += 1
        Loop

        If level_count > 0 Then
            If level_count = 1 Then
                'singular
                GlobalMsg(GetPlayerName(index) & " has gained " & level_count & " level!")
            Else
                'plural
                GlobalMsg(GetPlayerName(index) & " has gained " & level_count & " levels!")
            End If
            SendExp(index)
            SendPlayerData(index)
        End If
    End Sub

    Function GetPlayerClass(index As Integer) As Integer
        GetPlayerClass = Player(index).Character(TempPlayer(index).CurChar).Classes
    End Function

    Sub SetPlayerPK(index As Integer, PK As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Pk = PK
    End Sub

#End Region

#Region "Incoming Packets"

    Friend Sub HandleUseChar(index As Integer)
        If Not IsPlaying(index) Then
            JoinGame(index)
            Dim text = String.Format("{0} | {1} has began playing {2}.", GetPlayerLogin(index), GetPlayerName(index), Settings.GameName)
            Addlog(text, PLAYER_LOG)
            Console.WriteLine(text)
        End If
    End Sub

#End Region

#Region "Outgoing Packets"

    Sub SendLeaveMap(index As Integer, mapNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SLeftMap)
        buffer.WriteInt32(index)
        SendDataToMapBut(index, mapNum, buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

#End Region

#Region "Movement"

    Sub PlayerWarp(index As Integer, MapNum As Integer, X As Integer, Y As Integer, Optional HouseTeleport As Boolean = False, Optional NoInstance As Boolean = False)
        Dim OldMap As Integer
        Dim i As Integer
        Dim buffer As ByteStream

        If Map(MapNum).Instanced = 1 And NoInstance = False Then
            MapNum = CreateInstance(MapNum) ' AndAlso MAP_NUMBER_MASK)
            If MapNum = -1 Then
                'Couldn't create instanced map!
                MapNum = GetPlayerMap(index)
                X = GetPlayerX(index)
                Y = GetPlayerY(index)
                AlertMsg(index, "Unable to create a cached map!")
            Else
                'store old info, for returning to entrance of instance
                If Not TempPlayer(index).InInstance = 1 Then
                    TempPlayer(index).TmpMap = GetPlayerMap(index)
                    TempPlayer(index).TmpX = GetPlayerX(index)
                    TempPlayer(index).TmpY = GetPlayerY(index)
                    TempPlayer(index).InInstance = 1
                End If
                MapNum += MAX_MAPS
            End If
        End If

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse MapNum <= 0 OrElse MapNum > MAX_CACHED_MAPS Then Exit Sub

        ' Check if you are out of bounds
        If X > Map(MapNum).MaxX Then X = Map(MapNum).MaxX
        If Y > Map(MapNum).MaxY Then Y = Map(MapNum).MaxY

        TempPlayer(index).EventProcessingCount = 0
        TempPlayer(index).EventMap.CurrentEvents = 0

        If HouseTeleport = False Then
            Player(index).Character(TempPlayer(index).CurChar).InHouse = 0
        End If

        If Player(index).Character(TempPlayer(index).CurChar).InHouse > 0 Then
            If IsPlaying(Player(index).Character(TempPlayer(index).CurChar).InHouse) Then
                If Player(index).Character(TempPlayer(index).CurChar).InHouse <> Player(index).Character(TempPlayer(index).CurChar).InHouse Then
                    Player(index).Character(TempPlayer(index).CurChar).InHouse = 0
                    PlayerWarp(index, Player(index).Character(TempPlayer(index).CurChar).LastMap, Player(index).Character(TempPlayer(index).CurChar).LastX, Player(index).Character(TempPlayer(index).CurChar).LastY)
                    Exit Sub
                Else
                    SendFurnitureToHouse(Player(index).Character(TempPlayer(index).CurChar).InHouse)
                End If
            End If
        End If

        'clear target
        TempPlayer(index).Target = 0
        TempPlayer(index).TargetType = TargetType.None
        SendTarget(index, 0, TargetType.None)

        ' Save old map to send erase player data to
        OldMap = GetPlayerMap(index)

        If OldMap <> MapNum Then
            SendLeaveMap(index, OldMap)
        End If

        SetPlayerMap(index, MapNum)
        SetPlayerX(index, X)
        SetPlayerY(index, Y)
        If PetAlive(index) Then
            SetPetX(index, X)
            SetPetY(index, Y)
            TempPlayer(index).PetTarget = 0
            TempPlayer(index).PetTargetType = 0
            SendPetXy(index, X, Y)
        End If

        SendPlayerXY(index)

        ' send equipment of all people on new map
        If GetTotalMapPlayers(MapNum) > 0 Then
            For i = 1 To GetPlayersOnline()
                If IsPlaying(i) Then
                    If GetPlayerMap(i) = MapNum Then
                        SendMapEquipmentTo(i, index)
                    End If
                End If
            Next
        End If

        ' Now we check if there were any players left on the map the player just left, and if not stop processing npcs
        If GetTotalMapPlayers(OldMap) = 0 Then
            PlayersOnMap(OldMap) = False

            If IsInstancedMap(OldMap) Then
                DestroyInstancedMap(OldMap - MAX_MAPS)
            End If

            ' Regenerate all NPCs' health
            For i = 1 To MAX_MAP_NPCS

                If MapNpc(OldMap).Npc(i).Num > 0 Then
                    MapNpc(OldMap).Npc(i).Vital(VitalType.HP) = GetNpcMaxVital(MapNpc(OldMap).Npc(i).Num, VitalType.HP)
                End If

            Next

        End If

        ' Sets it so we know to process npcs on the map
        PlayersOnMap(MapNum) = True
        TempPlayer(index).GettingMap = True

        CheckTasks(index, QuestType.Reach, MapNum)

        buffer = New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SCheckForMap)
        buffer.WriteInt32(MapNum)
        buffer.WriteInt32(Map(MapNum).Revision)
        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub

    Sub PlayerMove(index As Integer, Dir As Integer, Movement As Integer, ExpectingWarp As Boolean)
        Dim mapNum As Integer, Buffer As ByteStream
        Dim x As Integer, y As Integer, begineventprocessing As Boolean
        Dim Moved As Boolean, DidWarp As Boolean
        Dim NewMapX As Byte, NewMapY As Byte
        Dim VitalType As Integer, Colour As Integer, amount As Integer

        'Debug.Print("Server-PlayerMove")

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse Dir < DirectionType.Up OrElse Dir > DirectionType.Right OrElse Movement < 1 OrElse Movement > 2 Then
            Exit Sub
        End If

        SetPlayerDir(index, Dir)
        Moved = False
        mapNum = GetPlayerMap(index)

        Select Case Dir
            Case DirectionType.Up

                ' Check to make sure not outside of boundries
                If GetPlayerY(index) > 0 Then

                    ' Check to make sure that the tile is walkable
                    If Not IsDirBlocked(Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index)).DirBlock, DirectionType.Up + 1) Then
                        If Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index) - 1).Type <> TileType.Blocked Then
                            If Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index) - 1).Type <> TileType.Resource Then

                                ' Check to see if the tile is a key and if it is check if its opened
                                If Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index) - 1).Type <> TileType.Key OrElse (Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index) - 1).Type = TileType.Key AndAlso TempTile(GetPlayerMap(index)).DoorOpen(GetPlayerX(index), GetPlayerY(index) - 1) = True) Then
                                    SetPlayerY(index, GetPlayerY(index) - 1)
                                    SendPlayerMove(index, Movement)
                                    Moved = True
                                End If

                                'check for event
                                For i = 1 To TempPlayer(index).EventMap.CurrentEvents
                                    If TempPlayer(index).EventMap.EventPages(i).X = GetPlayerX(index) AndAlso TempPlayer(index).EventMap.EventPages(i).Y = GetPlayerY(index) - 1 Then
                                        If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).Trigger = 1 Then
                                            'PlayerMsg(Index, "OnTouch event", ColorType.Red)
                                            'Process this event, it is on-touch and everything checks out.
                                            If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).CommandListCount > 0 Then

                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).CurList = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).CurSlot = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).EventId = TempPlayer(index).EventMap.EventPages(i).EventId
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).PageId = TempPlayer(index).EventMap.EventPages(i).PageId
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).WaitingForResponse = 0
                                                ReDim TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).ListLeftOff(Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).CommandListCount)

                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).Active = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).ActionTimer = GetTimeMs()
                                            End If
                                        End If
                                    End If
                                Next

                            End If
                        End If
                    End If
                Else

                    ' Check to see if we can move them to the another map
                    If Map(GetPlayerMap(index)).Up > 0 Then
                        NewMapY = Map(Map(GetPlayerMap(index)).Up).MaxY
                        PlayerWarp(index, Map(GetPlayerMap(index)).Up, GetPlayerX(index), NewMapY)
                        DidWarp = True
                        Moved = True
                    End If
                End If

            Case DirectionType.Down

                ' Check to make sure not outside of boundries
                If GetPlayerY(index) < Map(mapNum).MaxY Then

                    ' Check to make sure that the tile is walkable
                    If Not IsDirBlocked(Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index)).DirBlock, DirectionType.Down + 1) Then
                        If Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index) + 1).Type <> TileType.Blocked Then
                            If Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index) + 1).Type <> TileType.Resource Then

                                ' Check to see if the tile is a key and if it is check if its opened
                                If Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index) + 1).Type <> TileType.Key OrElse (Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index) + 1).Type = TileType.Key AndAlso TempTile(GetPlayerMap(index)).DoorOpen(GetPlayerX(index), GetPlayerY(index) + 1) = True) Then
                                    SetPlayerY(index, GetPlayerY(index) + 1)
                                    SendPlayerMove(index, Movement)
                                    Moved = True
                                End If

                                'check for event
                                For i = 1 To TempPlayer(index).EventMap.CurrentEvents
                                    If TempPlayer(index).EventMap.EventPages(i).X = GetPlayerX(index) AndAlso TempPlayer(index).EventMap.EventPages(i).Y = GetPlayerY(index) + 1 Then
                                        If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).Trigger = 1 Then
                                            'PlayerMsg(Index, "OnTouch event", ColorType.Red)
                                            'Process this event, it is on-touch and everything checks out.
                                            If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).CommandListCount > 0 Then

                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).CurList = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).CurSlot = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).EventId = TempPlayer(index).EventMap.EventPages(i).EventId
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).PageId = TempPlayer(index).EventMap.EventPages(i).PageId
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).WaitingForResponse = 0
                                                ReDim TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).ListLeftOff(Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).CommandListCount)

                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).Active = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).ActionTimer = GetTimeMs()
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If
                Else

                    ' Check to see if we can move them to the another map
                    If Map(GetPlayerMap(index)).Down > 0 Then
                        PlayerWarp(index, Map(GetPlayerMap(index)).Down, GetPlayerX(index), 0)
                        DidWarp = True
                        Moved = True
                    End If
                End If

            Case DirectionType.Left

                ' Check to make sure not outside of boundries
                If GetPlayerX(index) > 0 Then

                    ' Check to make sure that the tile is walkable
                    If Not IsDirBlocked(Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index)).DirBlock, DirectionType.Left + 1) Then
                        If Map(GetPlayerMap(index)).Tile(GetPlayerX(index) - 1, GetPlayerY(index)).Type <> TileType.Blocked Then
                            If Map(GetPlayerMap(index)).Tile(GetPlayerX(index) - 1, GetPlayerY(index)).Type <> TileType.Resource Then

                                ' Check to see if the tile is a key and if it is check if its opened
                                If Map(GetPlayerMap(index)).Tile(GetPlayerX(index) - 1, GetPlayerY(index)).Type <> TileType.Key OrElse (Map(GetPlayerMap(index)).Tile(GetPlayerX(index) - 1, GetPlayerY(index)).Type = TileType.Key AndAlso TempTile(GetPlayerMap(index)).DoorOpen(GetPlayerX(index) - 1, GetPlayerY(index)) = True) Then
                                    SetPlayerX(index, GetPlayerX(index) - 1)
                                    SendPlayerMove(index, Movement)
                                    Moved = True
                                End If

                                'check for event
                                For i = 1 To TempPlayer(index).EventMap.CurrentEvents
                                    If TempPlayer(index).EventMap.EventPages(i).X = GetPlayerX(index) - 1 AndAlso TempPlayer(index).EventMap.EventPages(i).Y = GetPlayerY(index) Then
                                        If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).Trigger = 1 Then
                                            'PlayerMsg(Index, "OnTouch event", ColorType.Red)
                                            'Process this event, it is on-touch and everything checks out.
                                            If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).CommandListCount > 0 Then

                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).CurList = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).CurSlot = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).EventId = TempPlayer(index).EventMap.EventPages(i).EventId
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).PageId = TempPlayer(index).EventMap.EventPages(i).PageId
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).WaitingForResponse = 0
                                                ReDim TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).ListLeftOff(Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).CommandListCount)

                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).Active = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).ActionTimer = GetTimeMs()
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If
                Else

                    ' Check to see if we can move them to the another map
                    If Map(GetPlayerMap(index)).Left > 0 Then
                        NewMapX = Map(Map(GetPlayerMap(index)).Left).MaxX
                        PlayerWarp(index, Map(GetPlayerMap(index)).Left, NewMapX, GetPlayerY(index))
                        DidWarp = True
                        Moved = True
                    End If
                End If

            Case DirectionType.Right

                ' Check to make sure not outside of boundries
                If GetPlayerX(index) < Map(mapNum).MaxX Then

                    ' Check to make sure that the tile is walkable
                    If Not IsDirBlocked(Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index)).DirBlock, DirectionType.Right + 1) Then
                        If Map(GetPlayerMap(index)).Tile(GetPlayerX(index) + 1, GetPlayerY(index)).Type <> TileType.Blocked Then
                            If Map(GetPlayerMap(index)).Tile(GetPlayerX(index) + 1, GetPlayerY(index)).Type <> TileType.Resource Then

                                ' Check to see if the tile is a key and if it is check if its opened
                                If Map(GetPlayerMap(index)).Tile(GetPlayerX(index) + 1, GetPlayerY(index)).Type <> TileType.Key OrElse (Map(GetPlayerMap(index)).Tile(GetPlayerX(index) + 1, GetPlayerY(index)).Type = TileType.Key AndAlso TempTile(GetPlayerMap(index)).DoorOpen(GetPlayerX(index) + 1, GetPlayerY(index)) = True) Then
                                    SetPlayerX(index, GetPlayerX(index) + 1)
                                    SendPlayerMove(index, Movement)
                                    Moved = True
                                End If

                                'check for event
                                For i = 1 To TempPlayer(index).EventMap.CurrentEvents
                                    If TempPlayer(index).EventMap.EventPages(i).X = GetPlayerX(index) AndAlso TempPlayer(index).EventMap.EventPages(i).Y = GetPlayerY(index) Then
                                        If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).Trigger = 1 Then
                                            'PlayerMsg(Index, "OnTouch event", ColorType.Red)
                                            'Process this event, it is on-touch and everything checks out.
                                            If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).CommandListCount > 0 Then

                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).CurList = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).CurSlot = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).EventId = TempPlayer(index).EventMap.EventPages(i).EventId
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).PageId = TempPlayer(index).EventMap.EventPages(i).PageId
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).WaitingForResponse = 0
                                                ReDim TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).ListLeftOff(Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).CommandListCount)

                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).Active = 1
                                                TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).ActionTimer = GetTimeMs()
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If
                Else

                    ' Check to see if we can move them to the another map
                    If Map(GetPlayerMap(index)).Right > 0 Then
                        PlayerWarp(index, Map(GetPlayerMap(index)).Right, 0, GetPlayerY(index))
                        DidWarp = True
                        Moved = True
                    End If
                End If
        End Select

        With Map(GetPlayerMap(index)).Tile(GetPlayerX(index), GetPlayerY(index))
            ' Check to see if the tile is a warp tile, and if so warp them
            If .Type = TileType.Warp Then
                mapNum = .Data1
                x = .Data2
                y = .Data3

                'If (MapNum AndAlso INSTANCED_MAP_MASK) > 0 Then
                If Map(mapNum).Instanced = 1 Then
                    If TempPlayer(index).InParty Then
                        PartyWarp(index, mapNum, x, y)
                    Else
                        PlayerWarp(index, mapNum, x, y)
                    End If
                Else
                    PlayerWarp(index, mapNum, x, y)
                End If

                DidWarp = True
                Moved = True
            End If

            ' Check to see if the tile is a door tile, and if so warp them
            If .Type = TileType.Door Then
                mapNum = .Data1
                x = .Data2
                y = .Data3
                ' send the animation to the map
                SendDoorAnimation(GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index))

                If Map(mapNum).Instanced = 1 Then
                    If TempPlayer(index).InParty Then
                        PartyWarp(index, mapNum, x, y)
                    Else
                        PlayerWarp(index, mapNum, x, y)
                    End If
                Else
                    PlayerWarp(index, mapNum, x, y)
                End If
                DidWarp = True
                Moved = True
            End If

            ' Check for key trigger open
            If .Type = TileType.KeyOpen Then
                x = .Data1
                y = .Data2

                If Map(GetPlayerMap(index)).Tile(x, y).Type = TileType.Key AndAlso TempTile(GetPlayerMap(index)).DoorOpen(x, y) = False Then
                    TempTile(GetPlayerMap(index)).DoorOpen(x, y) = True
                    TempTile(GetPlayerMap(index)).DoorTimer = GetTimeMs()
                    SendMapKey(index, x, y, 1)
                    MapMsg(GetPlayerMap(index), "A door has been unlocked.", ColorType.White)
                End If
            End If

            ' Check for a shop, and if so open it
            If .Type = TileType.Shop Then
                x = .Data1
                If x > 0 Then ' shop exists?
                    If Len(Trim$(Shop(x).Name)) > 0 Then ' name exists?
                        SendOpenShop(index, x)
                        TempPlayer(index).InShop = x ' stops movement and the like
                    End If
                End If
            End If

            ' Check to see if the tile is a bank, and if so send bank
            If .Type = TileType.Bank Then
                SendBank(index)
                TempPlayer(index).InBank = True
                Moved = True
            End If

            ' Check if it's a heal tile
            If .Type = TileType.Heal Then
                VitalType = .Data1
                amount = .Data2
                If Not GetPlayerVital(index, VitalType) = GetPlayerMaxVital(index, VitalType) Then
                    If VitalType = modEnumerators.VitalType.HP Then
                        Colour = ColorType.BrightGreen
                    Else
                        Colour = ColorType.BrightBlue
                    End If
                    SendActionMsg(GetPlayerMap(index), "+" & amount, Colour, ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32, 1)
                    SetPlayerVital(index, VitalType, GetPlayerVital(index, VitalType) + amount)
                    PlayerMsg(index, "You feel rejuvinating forces coarsing through your body.", ColorType.BrightGreen)
                    SendVital(index, VitalType)
                    ' send vitals to party if in one
                    If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)
                End If
                Moved = True
            End If

            ' Check if it's a trap tile
            If .Type = TileType.Trap Then
                amount = .Data1
                SendActionMsg(GetPlayerMap(index), "-" & amount, ColorType.BrightRed, ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32, 1)
                If GetPlayerVital(index, modEnumerators.VitalType.HP) - amount <= 0 Then
                    KillPlayer(index)
                    PlayerMsg(index, "You've been killed by a trap.", ColorType.BrightRed)
                Else
                    SetPlayerVital(index, modEnumerators.VitalType.HP, GetPlayerVital(index, modEnumerators.VitalType.HP) - amount)
                    PlayerMsg(index, "You've been injured by a trap.", ColorType.BrightRed)
                    SendVital(index, modEnumerators.VitalType.HP)
                    ' send vitals to party if in one
                    If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)
                End If
                Moved = True
            End If

            'Housing
            If .Type = TileType.House Then
                If Player(index).Character(TempPlayer(index).CurChar).House.Houseindex = .Data1 Then
                    'Do warping and such to the player's house :/
                    Player(index).Character(TempPlayer(index).CurChar).LastMap = GetPlayerMap(index)
                    Player(index).Character(TempPlayer(index).CurChar).LastX = GetPlayerX(index)
                    Player(index).Character(TempPlayer(index).CurChar).LastY = GetPlayerY(index)
                    Player(index).Character(TempPlayer(index).CurChar).InHouse = index
                    Dim data = PlayerData(index)
                    Socket.SendDataTo(index, data, data.Length)
                    PlayerWarp(index, HouseConfig(Player(index).Character(TempPlayer(index).CurChar).House.Houseindex).BaseMap, HouseConfig(Player(index).Character(TempPlayer(index).CurChar).House.Houseindex).X, HouseConfig(Player(index).Character(TempPlayer(index).CurChar).House.Houseindex).Y, True)
                    DidWarp = True
                    Exit Sub
                Else
                    'Send the buy sequence and see what happens. (To be recreated in events.)
                    Buffer = New ByteStream(4)
                    Buffer.WriteInt32(ServerPackets.SBuyHouse)
                    Buffer.WriteInt32(.Data1)
                    Socket.SendDataTo(index, Buffer.Data, Buffer.Head)
                    Buffer.Dispose()
                    TempPlayer(index).BuyHouseindex = .Data1
                End If
            End If

            'crafting
            If .Type = TileType.Craft Then
                TempPlayer(index).IsCrafting = True
                SendPlayerRecipes(index)
                SendOpenCraft(index)
                Moved = True
            End If

        End With

        If Moved = True Then
            If Player(index).Character(TempPlayer(index).CurChar).InHouse > 0 Then
                If Player(index).Character(TempPlayer(index).CurChar).X = HouseConfig(Player(Player(index).Character(TempPlayer(index).CurChar).InHouse).Character(TempPlayer(index).CurChar).House.Houseindex).X Then
                    If Player(index).Character(TempPlayer(index).CurChar).Y = HouseConfig(Player(Player(index).Character(TempPlayer(index).CurChar).InHouse).Character(TempPlayer(index).CurChar).House.Houseindex).Y Then
                        PlayerWarp(index, Player(index).Character(TempPlayer(index).CurChar).LastMap, Player(index).Character(TempPlayer(index).CurChar).LastX, Player(index).Character(TempPlayer(index).CurChar).LastY)
                        DidWarp = True
                    End If
                End If
            End If
        End If

        ' They tried to hack
        If Moved = False OrElse (ExpectingWarp AndAlso Not DidWarp) Then
            PlayerWarp(index, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index))
        End If

        x = GetPlayerX(index)
        y = GetPlayerY(index)

        If Moved = True Then
            If TempPlayer(index).EventMap.CurrentEvents > 0 Then
                For i = 1 To TempPlayer(index).EventMap.CurrentEvents
                    If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Globals = 1 Then
                        If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).X = x AndAlso Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Y = y AndAlso Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).Trigger = 1 AndAlso TempPlayer(index).EventMap.EventPages(i).Visible = 1 Then begineventprocessing = True
                    Else
                        If TempPlayer(index).EventMap.EventPages(i).X = x AndAlso TempPlayer(index).EventMap.EventPages(i).Y = y AndAlso Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).Trigger = 1 AndAlso TempPlayer(index).EventMap.EventPages(i).Visible = 1 Then begineventprocessing = True
                    End If
                    begineventprocessing = False
                    If begineventprocessing = True Then
                        'Process this event, it is on-touch and everything checks out.
                        If Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).CommandListCount > 0 Then
                            TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).Active = 1
                            TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).ActionTimer = GetTimeMs()
                            TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).CurList = 1
                            TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).CurSlot = 1
                            TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).EventId = TempPlayer(index).EventMap.EventPages(i).EventId
                            TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).PageId = TempPlayer(index).EventMap.EventPages(i).PageId
                            TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).WaitingForResponse = 0
                            ReDim TempPlayer(index).EventProcessing(TempPlayer(index).EventMap.EventPages(i).EventId).ListLeftOff(Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Pages(TempPlayer(index).EventMap.EventPages(i).PageId).CommandListCount)
                        End If
                        begineventprocessing = False
                    End If
                Next
            End If
        End If

    End Sub

#End Region

#Region "Inventory"

    Function HasItem(index As Integer, ItemNum As Integer) As Integer
        Dim i As Integer

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse ItemNum <= 0 OrElse ItemNum > MAX_ITEMS Then
            Exit Function
        End If

        For i = 1 To MAX_INV
            ' Check to see if the player has the item
            If GetPlayerInvItemNum(index, i) = ItemNum Then
                If Item(ItemNum).Type = ItemType.Currency OrElse Item(ItemNum).Stackable = 1 Then
                    HasItem = GetPlayerInvItemValue(index, i)
                Else
                    HasItem = 1
                End If
                Exit Function
            End If
        Next

    End Function

    Function FindItemSlot(index As Integer, ItemNum As Integer) As Integer
        Dim i As Integer

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse ItemNum <= 0 OrElse ItemNum > MAX_ITEMS Then
            Exit Function
        End If

        For i = 1 To MAX_INV
            ' Check to see if the player has the item
            If GetPlayerInvItemNum(index, i) = ItemNum Then
                FindItemSlot = i
                Exit Function
            End If
        Next

    End Function

    Function GetPlayerInvItemNum(index As Integer, InvSlot As Integer) As Integer
        GetPlayerInvItemNum = 0
        If index > MAX_PLAYERS Then Exit Function
        If InvSlot = 0 Then Exit Function

        GetPlayerInvItemNum = Player(index).Character(TempPlayer(index).CurChar).Inv(InvSlot).Num
    End Function

    Function GetPlayerInvItemValue(index As Integer, InvSlot As Integer) As Integer
        GetPlayerInvItemValue = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerInvItemValue = Player(index).Character(TempPlayer(index).CurChar).Inv(InvSlot).Value
    End Function

    Sub PlayerMapGetItem(index As Integer)
        Dim i As Integer, itemnum As Integer
        Dim n As Integer
        Dim mapNum As Integer
        Dim Msg As String

        If Not IsPlaying(index) Then Exit Sub
        mapNum = GetPlayerMap(index)

        For i = 1 To MAX_MAP_ITEMS

            ' See if theres even an item here
            If (MapItem(mapNum, i).Num > 0) And (MapItem(mapNum, i).Num <= MAX_ITEMS) Then
                ' our drop?
                If CanPlayerPickupItem(index, i) Then
                    ' Check if item is at the same location as the player
                    If (MapItem(mapNum, i).X = GetPlayerX(index)) Then
                        If (MapItem(mapNum, i).Y = GetPlayerY(index)) Then
                            ' Find open slot
                            n = FindOpenInvSlot(index, MapItem(mapNum, i).Num)

                            ' Open slot available?
                            If n <> 0 Then
                                ' Set item in players inventor
                                itemnum = MapItem(mapNum, i).Num

                                If Item(itemnum).Randomize <> 0 Then
                                    If Trim(MapItem(mapNum, i).RandData.Prefix) <> "" OrElse Trim(MapItem(mapNum, i).RandData.Suffix) <> "" Then
                                        Player(index).Character(TempPlayer(index).CurChar).RandInv(n).Prefix = MapItem(mapNum, i).RandData.Prefix
                                        Player(index).Character(TempPlayer(index).CurChar).RandInv(n).Suffix = MapItem(mapNum, i).RandData.Suffix
                                        Player(index).Character(TempPlayer(index).CurChar).RandInv(n).Rarity = MapItem(mapNum, i).RandData.Rarity
                                        Player(index).Character(TempPlayer(index).CurChar).RandInv(n).Damage = MapItem(mapNum, i).RandData.Damage
                                        Player(index).Character(TempPlayer(index).CurChar).RandInv(n).Speed = MapItem(mapNum, i).RandData.Speed
                                        For m = 1 To StatType.Count - 1
                                            Player(index).Character(TempPlayer(index).CurChar).RandInv(n).Stat(m) = MapItem(GetPlayerMap(index), i).RandData.Stat(m)
                                        Next m
                                    Else ' Nothing has been generated yet!
                                        GivePlayerRandomItem(index, itemnum, n)
                                    End If
                                End If

                                SetPlayerInvItemNum(index, n, MapItem(mapNum, i).Num)

                                If Item(GetPlayerInvItemNum(index, n)).Type = ItemType.Currency OrElse Item(GetPlayerInvItemNum(index, n)).Stackable = 1 Then
                                    SetPlayerInvItemValue(index, n, GetPlayerInvItemValue(index, n) + MapItem(mapNum, i).Value)
                                    Msg = MapItem(mapNum, i).Value & " " & Trim$(Item(GetPlayerInvItemNum(index, n)).Name)
                                Else
                                    SetPlayerInvItemValue(index, n, 0)
                                    Msg = CheckGrammar(Trim$(Item(GetPlayerInvItemNum(index, n)).Name), 1)
                                End If

                                ' Erase item from the map
                                MapItem(mapNum, i).Num = 0
                                MapItem(mapNum, i).Value = 0
                                MapItem(mapNum, i).X = 0
                                MapItem(mapNum, i).Y = 0

                                SendInventoryUpdate(index, n)
                                SpawnItemSlot(i, 0, 0, GetPlayerMap(index), 0, 0)

                                SendActionMsg(GetPlayerMap(index), Msg, ColorType.White, 1, (GetPlayerX(index) * 32), (GetPlayerY(index) * 32))
                                CheckTasks(index, QuestType.Gather, GetItemNum(Trim$(Item(GetPlayerInvItemNum(index, n)).Name)))
                                Exit For
                            Else
                                PlayerMsg(index, "Your inventory is full.", ColorType.BrightRed)
                                Exit For
                            End If
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Function CanPlayerPickupItem(index As Integer, mapItemNum As Integer) As Boolean
        Dim mapnum As Integer

        mapnum = GetPlayerMap(index)

        ' no lock or locked to player?
        If MapItem(mapnum, mapItemNum).PlayerName = vbNullString Or MapItem(mapnum, mapItemNum).PlayerName = GetPlayerName(index).Trim Then
            CanPlayerPickupItem = True
            Exit Function
        End If

        CanPlayerPickupItem = False
    End Function

    Sub SetPlayerInvItemValue(index As Integer, InvSlot As Integer, ItemValue As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Inv(InvSlot).Value = ItemValue
    End Sub

    Sub SetPlayerInvItemNum(index As Integer, invSlot As Integer, itemNum As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Inv(invSlot).Num = itemNum
    End Sub

    Function FindOpenInvSlot(index As Integer, ItemNum As Integer) As Integer
        Dim i As Integer

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse ItemNum <= 0 OrElse ItemNum > MAX_ITEMS Then
            Exit Function
        End If

        If Item(ItemNum).Type = ItemType.Currency OrElse Item(ItemNum).Stackable = 1 Then
            ' If currency then check to see if they already have an instance of the item and add it to that
            For i = 1 To MAX_INV
                If GetPlayerInvItemNum(index, i) = ItemNum Then
                    FindOpenInvSlot = i
                    Exit Function
                End If
            Next
        End If

        For i = 1 To MAX_INV
            ' Try to find an open free slot
            If GetPlayerInvItemNum(index, i) = 0 Then
                FindOpenInvSlot = i
                Exit Function
            End If
        Next

    End Function

    Function TakeInvItem(index As Integer, ItemNum As Integer, ItemVal As Integer) As Boolean
        Dim i As Integer

        TakeInvItem = False

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse ItemNum <= 0 OrElse ItemNum > MAX_ITEMS Then
            Exit Function
        End If

        For i = 1 To MAX_INV

            ' Check to see if the player has the item
            If GetPlayerInvItemNum(index, i) = ItemNum Then
                If Item(ItemNum).Type = ItemType.Currency OrElse Item(ItemNum).Stackable = 1 Then

                    ' Is what we are trying to take away more then what they have?  If so just set it to zero
                    If ItemVal >= GetPlayerInvItemValue(index, i) Then
                        TakeInvItem = True
                    Else
                        SetPlayerInvItemValue(index, i, GetPlayerInvItemValue(index, i) - ItemVal)
                        SendInventoryUpdate(index, i)
                    End If
                Else
                    TakeInvItem = True
                End If

                If TakeInvItem Then
                    SetPlayerInvItemNum(index, i, 0)
                    SetPlayerInvItemValue(index, i, 0)
                    ' Send the inventory update
                    SendInventoryUpdate(index, i)
                    Exit Function
                End If
            End If

        Next

    End Function

    Function GiveInvItem(index As Integer, ItemNum As Integer, ItemVal As Integer, Optional SendUpdate As Boolean = True) As Boolean
        Dim i As Integer

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse ItemNum <= 0 OrElse ItemNum > MAX_ITEMS Then
            GiveInvItem = False
            Exit Function
        End If

        i = FindOpenInvSlot(index, ItemNum)

        ' Check to see if inventory is full
        If i <> 0 Then
            SetPlayerInvItemNum(index, i, ItemNum)
            SetPlayerInvItemValue(index, i, GetPlayerInvItemValue(index, i) + ItemVal)
            If SendUpdate Then SendInventoryUpdate(index, i)
            GiveInvItem = True
        Else
            PlayerMsg(index, "Your inventory is full.", ColorType.BrightRed)
            GiveInvItem = False
        End If

    End Function

    Sub PlayerMapDropItem(index As Integer, InvNum As Integer, Amount As Integer)
        Dim i As Integer

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse InvNum <= 0 OrElse InvNum > MAX_INV Then
            Exit Sub
        End If

        ' check the player isn't doing something
        If TempPlayer(index).InBank OrElse TempPlayer(index).InShop OrElse TempPlayer(index).InTrade > 0 Then Exit Sub

        If (GetPlayerInvItemNum(index, InvNum) > 0) Then
            If (GetPlayerInvItemNum(index, InvNum) <= MAX_ITEMS) Then
                i = FindOpenMapItemSlot(GetPlayerMap(index))

                If i <> 0 Then
                    MapItem(GetPlayerMap(index), i).Num = GetPlayerInvItemNum(index, InvNum)
                    MapItem(GetPlayerMap(index), i).X = GetPlayerX(index)
                    MapItem(GetPlayerMap(index), i).Y = GetPlayerY(index)
                    MapItem(GetPlayerMap(index), i).PlayerName = Trim$(GetPlayerName(index))
                    MapItem(GetPlayerMap(index), i).PlayerTimer = GetTimeMs() + ITEM_SPAWN_TIME
                    MapItem(GetPlayerMap(index), i).CanDespawn = True
                    MapItem(GetPlayerMap(index), i).DespawnTimer = GetTimeMs() + ITEM_DESPAWN_TIME

                    If Item(GetPlayerInvItemNum(index, InvNum)).Type = ItemType.Currency OrElse Item(GetPlayerInvItemNum(index, InvNum)).Stackable = 1 Then

                        ' Check if its more then they have and if so drop it all
                        If Amount >= GetPlayerInvItemValue(index, InvNum) Then
                            MapItem(GetPlayerMap(index), i).Value = GetPlayerInvItemValue(index, InvNum)
                            SetPlayerInvItemNum(index, InvNum, 0)
                            SetPlayerInvItemValue(index, InvNum, 0)
                            Amount = GetPlayerInvItemValue(index, InvNum)
                        Else
                            MapItem(GetPlayerMap(index), i).Value = Amount
                            SetPlayerInvItemValue(index, InvNum, GetPlayerInvItemValue(index, InvNum) - Amount)
                        End If
                        MapMsg(GetPlayerMap(index), String.Format("{0} has dropped {1} ({2}x).", GetPlayerName(index), CheckGrammar(Trim$(Item(GetPlayerInvItemNum(index, InvNum)).Name)), Amount), ColorType.Yellow)
                    Else
                        ' Its not a currency object so this is easy
                        MapItem(GetPlayerMap(index), i).Value = 0
                        ' send message

                        MapMsg(GetPlayerMap(index), String.Format("{0} has dropped {1}.", GetPlayerName(index), CheckGrammar(Trim$(Item(GetPlayerInvItemNum(index, InvNum)).Name))), ColorType.Yellow)
                        SetPlayerInvItemNum(index, InvNum, 0)
                        SetPlayerInvItemValue(index, InvNum, 0)
                    End If

                    ' Send inventory update
                    SendInventoryUpdate(index, InvNum)
                    ' Spawn the item before we set the num or we'll get a different free map item slot
                    SpawnItemSlot(i, MapItem(GetPlayerMap(index), i).Num, Amount, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index))
                Else
                    PlayerMsg(index, "Too many items already on the ground.", ColorType.Yellow)
                End If
            End If
        End If

    End Sub

    Function TakeInvSlot(index As Integer, InvSlot As Integer, ItemVal As Integer) As Boolean
        Dim itemNum

        TakeInvSlot = False

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse InvSlot <= 0 OrElse InvSlot > MAX_ITEMS Then Exit Function

        itemNum = GetPlayerInvItemNum(index, InvSlot)

        If Item(itemNum).Type = ItemType.Currency OrElse Item(itemNum).Stackable = 1 Then

            ' Is what we are trying to take away more then what they have?  If so just set it to zero
            If ItemVal >= GetPlayerInvItemValue(index, InvSlot) Then
                TakeInvSlot = True
            Else
                SetPlayerInvItemValue(index, InvSlot, GetPlayerInvItemValue(index, InvSlot) - ItemVal)
            End If
        Else
            TakeInvSlot = True
        End If

        If TakeInvSlot Then
            SetPlayerInvItemNum(index, InvSlot, 0)
            SetPlayerInvItemValue(index, InvSlot, 0)
            Exit Function
        End If

    End Function

    Friend Sub UseItem(index As Integer, InvNum As Integer)
        Dim InvItemNum As Integer, i As Integer, n As Integer, x As Integer, y As Integer, tempitem As Integer
        Dim m As Integer, tempdata(StatType.Count + 3) As Integer, tempstr(2) As String

        ' Prevent hacking
        If InvNum < 1 OrElse InvNum > MAX_ITEMS Then Exit Sub

        If (GetPlayerInvItemNum(index, InvNum) > 0) AndAlso (GetPlayerInvItemNum(index, InvNum) <= MAX_ITEMS) Then
            InvItemNum = GetPlayerInvItemNum(index, InvNum)

            n = Item(InvItemNum).Data2

            ' Find out what kind of item it is
            Select Case Item(InvItemNum).Type
                Case ItemType.Equipment
                    For i = 1 To StatType.Count - 1
                        If GetPlayerStat(index, i) < Item(InvItemNum).Stat_Req(i) Then
                            PlayerMsg(index, "You do not meet the stat requirements to equip this item.", ColorType.BrightRed)
                            Exit Sub
                        End If
                    Next

                    ' Make sure they are the right level
                    i = Item(InvItemNum).LevelReq

                    If i > GetPlayerLevel(index) Then
                        PlayerMsg(index, "You do not meet the level requirements to equip this item.", ColorType.BrightRed)
                        Exit Sub
                    End If

                    ' Make sure they are the right class
                    If Not Item(InvItemNum).ClassReq = GetPlayerClass(index) AndAlso Not Item(InvItemNum).ClassReq = 0 Then
                        PlayerMsg(index, "You do not meet the class requirements to equip this item.", ColorType.BrightRed)
                        Exit Sub
                    End If

                    ' access requirement
                    If Not GetPlayerAccess(index) >= Item(InvItemNum).AccessReq Then
                        PlayerMsg(index, "You do not meet the access requirement to equip this item.", ColorType.BrightRed)
                        Exit Sub
                    End If

                    'if that went fine, we progress the subtype

                    Select Case Item(InvItemNum).SubType
                        Case EquipmentType.Weapon

                            If Item(InvItemNum).TwoHanded > 0 Then
                                If GetPlayerEquipment(index, EquipmentType.Shield) > 0 Then
                                    PlayerMsg(index, "This is a 2Handed weapon! Please unequip shield first.", ColorType.BrightRed)
                                    Exit Sub
                                End If
                            End If

                            If GetPlayerEquipment(index, EquipmentType.Weapon) > 0 Then
                                tempitem = GetPlayerEquipment(index, EquipmentType.Weapon)
                                tempstr(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Prefix
                                tempstr(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Suffix
                                tempdata(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Damage
                                tempdata(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Speed
                                tempdata(3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Rarity
                                For i = 1 To StatType.Count - 1
                                    tempdata(i + 3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Stat(i)
                                Next
                            End If

                            SetPlayerEquipment(index, InvItemNum, EquipmentType.Weapon)

                            ' Transfer the Inventory data to the Equipment data
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Prefix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Prefix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Suffix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Suffix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Damage = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Damage
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Speed = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Speed
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Rarity = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Rarity

                            For i = 1 To StatType.Count - 1
                                Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Weapon).Stat(i) = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Stat(i)
                            Next

                            If Item(InvItemNum).Randomize <> 0 Then
                                PlayerMsg(index, "You equip " & tempstr(1) & " " & CheckGrammar(Item(InvItemNum).Name) & " " & tempstr(2), ColorType.BrightGreen)
                            Else
                                PlayerMsg(index, "You equip " & CheckGrammar(Item(InvItemNum).Name), ColorType.BrightGreen)
                            End If

                            SetPlayerInvItemNum(index, InvNum, 0)
                            SetPlayerInvItemValue(index, InvNum, 0)
                            ClearRandInv(index, InvNum)

                            If tempitem > 0 Then ' give back the stored item
                                m = FindOpenInvSlot(index, tempitem)
                                SetPlayerInvItemNum(index, m, tempitem)
                                SetPlayerInvItemValue(index, m, 0)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Prefix = tempstr(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Suffix = tempstr(2)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Damage = tempdata(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Speed = tempdata(2)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Rarity = tempdata(3)

                                For i = 1 To StatType.Count - 1
                                    Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Stat(i) = tempdata(i + 3)
                                Next

                                tempitem = 0
                            End If

                            SendWornEquipment(index)
                            SendMapEquipment(index)
                            SendInventory(index)
                            SendInventoryUpdate(index, InvNum)
                            SendStats(index)

                            ' send vitals
                            SendVitals(index)

                            ' send vitals to party if in one
                            If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)

                        Case EquipmentType.Armor

                            If GetPlayerEquipment(index, EquipmentType.Armor) > 0 Then
                                tempitem = GetPlayerEquipment(index, EquipmentType.Armor)
                                tempstr(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Prefix
                                tempstr(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Suffix
                                tempdata(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Damage
                                tempdata(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Speed
                                tempdata(3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Rarity
                                For i = 1 To StatType.Count - 1
                                    tempdata(i + 3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Stat(i)
                                Next
                            End If

                            SetPlayerEquipment(index, InvItemNum, EquipmentType.Armor)

                            ' Transfer the Inventory data to the Equipment data
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Prefix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Prefix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Suffix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Suffix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Damage = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Damage
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Speed = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Speed
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Rarity = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Rarity

                            For i = 1 To StatType.Count - 1
                                Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Armor).Stat(i) = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Stat(i)
                            Next

                            PlayerMsg(index, "You equip " & CheckGrammar(Item(InvItemNum).Name), ColorType.BrightGreen)
                            TakeInvItem(index, InvItemNum, 0)
                            ClearRandInv(index, InvNum)

                            If tempitem > 0 Then ' Return their old equipment to their inventory.
                                m = FindOpenInvSlot(index, tempitem)
                                SetPlayerInvItemNum(index, m, tempitem)
                                SetPlayerInvItemValue(index, m, 0)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Prefix = tempstr(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Suffix = tempstr(2)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Damage = tempdata(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Speed = tempdata(2)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Rarity = tempdata(3)

                                For i = 1 To StatType.Count - 1
                                    Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Stat(i) = tempdata(i + 3)
                                Next i

                                tempitem = 0
                            End If

                            SendWornEquipment(index)
                            SendMapEquipment(index)

                            SendInventory(index)
                            SendStats(index)

                            ' send vitals
                            SendVitals(index)

                            ' send vitals to party if in one
                            If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)

                        Case EquipmentType.Helmet

                            If GetPlayerEquipment(index, EquipmentType.Helmet) > 0 Then
                                tempitem = GetPlayerEquipment(index, EquipmentType.Helmet)
                                tempstr(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Prefix
                                tempstr(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Suffix
                                tempdata(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Damage
                                tempdata(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Speed
                                tempdata(3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Rarity
                                For i = 1 To StatType.Count - 1
                                    tempdata(i + 3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Stat(i)
                                Next i
                            End If

                            SetPlayerEquipment(index, InvItemNum, EquipmentType.Helmet)

                            ' Transfer the Inventory data to the Equipment data
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Prefix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Prefix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Suffix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Suffix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Damage = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Damage
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Speed = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Speed
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Rarity = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Rarity

                            For i = 1 To StatType.Count - 1
                                Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Helmet).Stat(i) = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Stat(i)
                            Next

                            PlayerMsg(index, "You equip " & CheckGrammar(Item(InvItemNum).Name), ColorType.BrightGreen)
                            TakeInvItem(index, InvItemNum, 1)
                            ClearRandInv(index, InvNum)

                            If tempitem > 0 Then ' give back the stored item
                                m = FindOpenInvSlot(index, tempitem)
                                SetPlayerInvItemNum(index, m, tempitem)
                                SetPlayerInvItemValue(index, m, 0)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Prefix = tempstr(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Suffix = tempstr(2)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Damage = tempdata(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Speed = tempdata(2)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Rarity = tempdata(3)

                                For i = 1 To StatType.Count - 1
                                    Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Stat(i) = tempdata(i + 3)
                                Next

                                tempitem = 0
                            End If

                            SendWornEquipment(index)
                            SendMapEquipment(index)
                            SendInventory(index)
                            SendStats(index)

                            ' send vitals
                            SendVitals(index)

                            ' send vitals to party if in one
                            If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)

                        Case EquipmentType.Shield
                            If Item(GetPlayerEquipment(index, EquipmentType.Weapon)).TwoHanded > 0 Then
                                PlayerMsg(index, "Please unequip your 2handed weapon first.", ColorType.BrightRed)
                                Exit Sub
                            End If

                            If GetPlayerEquipment(index, EquipmentType.Shield) > 0 Then
                                tempitem = GetPlayerEquipment(index, EquipmentType.Shield)
                                tempstr(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Prefix
                                tempstr(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Suffix
                                tempdata(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Damage
                                tempdata(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Speed
                                tempdata(3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Rarity
                                For i = 1 To StatType.Count - 1
                                    tempdata(i + 3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Stat(i)
                                Next i
                            End If

                            SetPlayerEquipment(index, InvItemNum, EquipmentType.Shield)

                            ' Transfer the Inventory data to the Equipment data
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Prefix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Prefix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Suffix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Suffix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Damage = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Damage
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Speed = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Speed
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Rarity = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Rarity

                            For i = 1 To StatType.Count - 1
                                Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shield).Stat(i) = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Stat(i)
                            Next

                            PlayerMsg(index, "You equip " & CheckGrammar(Item(InvItemNum).Name), ColorType.BrightGreen)
                            TakeInvItem(index, InvItemNum, 1)
                            ClearRandInv(index, InvNum)

                            If tempitem > 0 Then ' give back the stored item
                                m = FindOpenInvSlot(index, tempitem)
                                SetPlayerInvItemNum(index, m, tempitem)
                                SetPlayerInvItemValue(index, m, 0)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Prefix = tempstr(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Suffix = tempstr(2)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Damage = tempdata(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Speed = tempdata(2)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Rarity = tempdata(3)

                                For i = 1 To StatType.Count - 1
                                    Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Stat(i) = tempdata(i + 3)
                                Next

                                tempitem = 0
                            End If

                            SendWornEquipment(index)
                            SendMapEquipment(index)
                            SendInventory(index)
                            SendStats(index)

                            ' send vitals
                            SendVitals(index)

                            ' send vitals to party if in one
                            If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)

                        Case EquipmentType.Shoes
                            If GetPlayerEquipment(index, EquipmentType.Shoes) > 0 Then
                                tempitem = GetPlayerEquipment(index, EquipmentType.Shoes)
                                tempstr(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Prefix
                                tempstr(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Suffix
                                tempdata(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Damage
                                tempdata(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Speed
                                tempdata(3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Rarity
                                For i = 1 To StatType.Count - 1
                                    tempdata(i + 3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Stat(i)
                                Next i
                            End If

                            SetPlayerEquipment(index, InvItemNum, EquipmentType.Shoes)

                            ' Transfer the Inventory data to the Equipment data
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Prefix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Prefix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Suffix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Suffix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Damage = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Damage
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Speed = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Speed
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Rarity = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Rarity

                            For i = 1 To StatType.Count - 1
                                Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Shoes).Stat(i) = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Stat(i)
                            Next

                            PlayerMsg(index, "You equip " & CheckGrammar(Item(InvItemNum).Name), ColorType.BrightGreen)
                            TakeInvItem(index, InvItemNum, 1)
                            ClearRandInv(index, InvNum)

                            If tempitem > 0 Then ' give back the stored item
                                m = FindOpenInvSlot(index, tempitem)
                                SetPlayerInvItemNum(index, m, tempitem)
                                SetPlayerInvItemValue(index, m, 0)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Prefix = tempstr(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Suffix = tempstr(2)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Damage = tempdata(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Speed = tempdata(2)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Rarity = tempdata(3)

                                For i = 1 To StatType.Count - 1
                                    Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Stat(i) = tempdata(i + 3)
                                Next

                                tempitem = 0
                            End If

                            SendWornEquipment(index)
                            SendMapEquipment(index)
                            SendInventory(index)
                            SendStats(index)

                            ' send vitals
                            SendVitals(index)

                            ' send vitals to party if in one
                            If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)

                        Case EquipmentType.Gloves
                            If GetPlayerEquipment(index, EquipmentType.Gloves) > 0 Then
                                tempitem = GetPlayerEquipment(index, EquipmentType.Gloves)
                                tempstr(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Prefix
                                tempstr(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Suffix
                                tempdata(1) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Damage
                                tempdata(2) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Speed
                                tempdata(3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Rarity
                                For i = 1 To StatType.Count - 1
                                    tempdata(i + 3) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Stat(i)
                                Next i
                            End If

                            SetPlayerEquipment(index, InvItemNum, EquipmentType.Gloves)

                            ' Transfer the Inventory data to the Equipment data
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Prefix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Prefix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Suffix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Suffix
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Damage = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Damage
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Speed = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Speed
                            Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Rarity = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Rarity

                            For i = 1 To StatType.Count - 1
                                Player(index).Character(TempPlayer(index).CurChar).RandEquip(EquipmentType.Gloves).Stat(i) = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Stat(i)
                            Next

                            PlayerMsg(index, "You equip " & CheckGrammar(Item(InvItemNum).Name), ColorType.BrightGreen)
                            TakeInvItem(index, InvItemNum, 1)
                            ClearRandInv(index, InvNum)

                            If tempitem > 0 Then ' give back the stored item
                                m = FindOpenInvSlot(index, tempitem)
                                SetPlayerInvItemNum(index, m, tempitem)
                                SetPlayerInvItemValue(index, m, 0)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Prefix = tempstr(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Suffix = tempstr(2)

                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Damage = tempdata(1)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Speed = tempdata(2)
                                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Rarity = tempdata(3)

                                For i = 1 To StatType.Count - 1
                                    Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Stat(i) = tempdata(i + 3)
                                Next

                                tempitem = 0
                            End If

                            SendWornEquipment(index)
                            SendMapEquipment(index)
                            SendInventory(index)
                            SendStats(index)

                            ' send vitals
                            SendVitals(index)

                            ' send vitals to party if in one
                            If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)
                    End Select

                Case ItemType.Consumable

                    For i = 1 To StatType.Count - 1
                        If GetPlayerStat(index, i) < Item(InvItemNum).Stat_Req(i) Then
                            PlayerMsg(index, "You do not meet the stat requirements to use this item.", ColorType.BrightRed)
                            Exit Sub
                        End If
                    Next

                    ' Make sure they are the right level
                    i = Item(InvItemNum).LevelReq

                    If i > GetPlayerLevel(index) Then
                        PlayerMsg(index, "You do not meet the level requirements to use this item.", ColorType.BrightRed)
                        Exit Sub
                    End If

                    ' Make sure they are the right class
                    If Not Item(InvItemNum).ClassReq = GetPlayerClass(index) AndAlso Not Item(InvItemNum).ClassReq = 0 Then
                        PlayerMsg(index, "You do not meet the class requirements to use this item.", ColorType.BrightRed)
                        Exit Sub
                    End If

                    ' access requirement
                    If Not GetPlayerAccess(index) >= Item(InvItemNum).AccessReq Then
                        PlayerMsg(index, "You do not meet the access requirement to use this item.", ColorType.BrightRed)
                        Exit Sub
                    End If

                    'if that went fine, we progress the subtype

                    Select Case Item(InvItemNum).SubType
                        Case ConsumableType.Hp
                            SendActionMsg(GetPlayerMap(index), "+" & Item(InvItemNum).Data1, ColorType.BrightGreen, ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32)
                            SendAnimation(GetPlayerMap(index), Item(InvItemNum).Animation, 0, 0, TargetType.Player, index)
                            SetPlayerVital(index, VitalType.HP, GetPlayerVital(index, VitalType.HP) + Item(InvItemNum).Data1)
                            If Item(InvItemNum).Stackable = 1 Then
                                TakeInvItem(index, InvItemNum, 1)
                            Else
                                TakeInvItem(index, InvItemNum, 0)
                            End If
                            SendVital(index, VitalType.HP)

                            ' send vitals to party if in one
                            If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)

                        Case ConsumableType.Mp
                            SendActionMsg(GetPlayerMap(index), "+" & Item(InvItemNum).Data1, ColorType.BrightBlue, ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32)
                            SendAnimation(GetPlayerMap(index), Item(InvItemNum).Animation, 0, 0, TargetType.Player, index)
                            SetPlayerVital(index, VitalType.MP, GetPlayerVital(index, VitalType.MP) + Item(InvItemNum).Data1)
                            If Item(InvItemNum).Stackable = 1 Then
                                TakeInvItem(index, InvItemNum, 1)
                            Else
                                TakeInvItem(index, InvItemNum, 0)
                            End If
                            SendVital(index, VitalType.MP)

                            ' send vitals to party if in one
                            If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)

                        Case ConsumableType.Mp
                            SendAnimation(GetPlayerMap(index), Item(InvItemNum).Animation, 0, 0, TargetType.Player, index)
                            SetPlayerVital(index, VitalType.SP, GetPlayerVital(index, VitalType.SP) + Item(InvItemNum).Data1)
                            If Item(InvItemNum).Stackable = 1 Then
                                TakeInvItem(index, InvItemNum, 1)
                            Else
                                TakeInvItem(index, InvItemNum, 0)
                            End If
                            SendVital(index, VitalType.SP)

                            ' send vitals to party if in one
                            If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)

                        Case ConsumableType.Exp

                    End Select

                Case ItemType.Key
                    InvItemNum = GetPlayerInvItemNum(index, InvNum)

                    For i = 1 To StatType.Count - 1
                        If GetPlayerStat(index, i) < Item(InvItemNum).Stat_Req(i) Then
                            PlayerMsg(index, "You do not meet the stat requirements to use this item.", ColorType.BrightRed)
                            Exit Sub
                        End If
                    Next

                    ' Make sure they are the right level
                    i = Item(InvItemNum).LevelReq

                    If i > GetPlayerLevel(index) Then
                        PlayerMsg(index, "You do not meet the level requirements to use this item.", ColorType.BrightRed)
                        Exit Sub
                    End If

                    ' Make sure they are the right class
                    If Not Item(InvItemNum).ClassReq = GetPlayerClass(index) AndAlso Not Item(InvItemNum).ClassReq = 0 Then
                        PlayerMsg(index, "You do not meet the class requirements to use this item.", ColorType.BrightRed)
                        Exit Sub
                    End If

                    Select Case GetPlayerDir(index)
                        Case DirectionType.Up

                            If GetPlayerY(index) > 0 Then
                                x = GetPlayerX(index)
                                y = GetPlayerY(index) - 1
                            Else
                                Exit Sub
                            End If

                        Case DirectionType.Down

                            If GetPlayerY(index) < Map(GetPlayerMap(index)).MaxY Then
                                x = GetPlayerX(index)
                                y = GetPlayerY(index) + 1
                            Else
                                Exit Sub
                            End If

                        Case DirectionType.Left

                            If GetPlayerX(index) > 0 Then
                                x = GetPlayerX(index) - 1
                                y = GetPlayerY(index)
                            Else
                                Exit Sub
                            End If

                        Case DirectionType.Right

                            If GetPlayerX(index) < Map(GetPlayerMap(index)).MaxX Then
                                x = GetPlayerX(index) + 1
                                y = GetPlayerY(index)
                            Else
                                Exit Sub
                            End If

                    End Select

                    ' Check if a key exists
                    If Map(GetPlayerMap(index)).Tile(x, y).Type = TileType.Key Then

                        ' Check if the key they are using matches the map key
                        If InvItemNum = Map(GetPlayerMap(index)).Tile(x, y).Data1 Then
                            TempTile(GetPlayerMap(index)).DoorOpen(x, y) = True
                            TempTile(GetPlayerMap(index)).DoorTimer = GetTimeMs()
                            SendMapKey(index, x, y, 1)
                            MapMsg(GetPlayerMap(index), "A door has been unlocked.", ColorType.Yellow)

                            SendAnimation(GetPlayerMap(index), Item(InvItemNum).Animation, x, y)

                            ' Check if we are supposed to take away the item
                            If Map(GetPlayerMap(index)).Tile(x, y).Data2 = 1 Then
                                TakeInvItem(index, InvItemNum, 0)
                                PlayerMsg(index, "The key is destroyed in the lock.", ColorType.Yellow)
                            End If
                        End If
                    End If

                Case ItemType.Skill
                    InvItemNum = GetPlayerInvItemNum(index, InvNum)

                    For i = 1 To StatType.Count - 1
                        If GetPlayerStat(index, i) < Item(InvItemNum).Stat_Req(i) Then
                            PlayerMsg(index, "You do not meet the stat requirements to use this item.", ColorType.BrightRed)
                            Exit Sub
                        End If
                    Next

                    ' Make sure they are the right class
                    If Not Item(InvItemNum).ClassReq = GetPlayerClass(index) AndAlso Not Item(InvItemNum).ClassReq = 0 Then
                        PlayerMsg(index, "You do not meet the class requirements to use this item.", ColorType.BrightRed)
                        Exit Sub
                    End If

                    ' Get the skill num
                    n = Item(InvItemNum).Data1

                    If n > 0 Then

                        ' Make sure they are the right class
                        If Skill(n).ClassReq = GetPlayerClass(index) OrElse Skill(n).ClassReq = 0 Then
                            ' Make sure they are the right level
                            i = Skill(n).LevelReq

                            If i <= GetPlayerLevel(index) Then
                                i = FindOpenSkillSlot(index)

                                ' Make sure they have an open skill slot
                                If i > 0 Then

                                    ' Make sure they dont already have the skill
                                    If Not HasSkill(index, n) Then
                                        SetPlayerSkill(index, i, n)
                                        SendAnimation(GetPlayerMap(index), Item(InvItemNum).Animation, 0, 0, TargetType.Player, index)
                                        TakeInvItem(index, InvItemNum, 0)
                                        PlayerMsg(index, "You study the skill carefully.", ColorType.Yellow)
                                        PlayerMsg(index, "You have learned a new skill!", ColorType.BrightGreen)
                                    Else
                                        PlayerMsg(index, "You have already learned this skill!", ColorType.BrightRed)
                                    End If
                                Else
                                    PlayerMsg(index, "You have learned all that you can learn!", ColorType.BrightRed)
                                End If
                            Else
                                PlayerMsg(index, "You must be level " & i & " to learn this skill.", ColorType.Yellow)
                            End If
                        Else
                            PlayerMsg(index, "This skill can only be learned by " & CheckGrammar(GetClassName(Skill(n).ClassReq)) & ".", ColorType.Yellow)
                        End If
                    Else
                        PlayerMsg(index, "This scroll is not connected to a skill, please inform an admin!", ColorType.BrightRed)
                    End If
                Case ItemType.Furniture
                    PlayerMsg(index, "To place furniture, simply click on it in your inventory, then click in your house where you want it.", ColorType.Yellow)

                Case ItemType.Recipe

                    PlayerMsg(index, "Lets learn this recipe :)", ColorType.BrightGreen)
                    ' Get the recipe num
                    n = Item(InvItemNum).Data1
                    LearnRecipe(index, n, InvNum)
                Case ItemType.Pet
                    If Item(InvItemNum).Stackable = 1 Then
                        TakeInvItem(index, InvItemNum, 1)
                    Else
                        TakeInvItem(index, InvItemNum, 0)
                    End If
                    n = Item(InvItemNum).Data1
                    AdoptPet(index, n)
            End Select

        End If
    End Sub

    Sub PlayerSwitchInvSlots(index As Integer, OldSlot As Integer, NewSlot As Integer)
        Dim OldNum As Integer, OldValue As Integer, OldRarity As Integer, OldPrefix As String
        Dim OldSuffix As String, OldSpeed As Integer, OldDamage As Integer
        Dim NewNum As Integer, NewValue As Integer, NewRarity As Integer, NewPrefix As String
        Dim NewSuffix As String, NewSpeed As Integer, NewDamage As Integer
        Dim NewStats(StatType.Count - 1) As Integer
        Dim OldStats(StatType.Count - 1) As Integer

        If OldSlot = 0 OrElse NewSlot = 0 Then Exit Sub

        OldNum = GetPlayerInvItemNum(index, OldSlot)
        OldValue = GetPlayerInvItemValue(index, OldSlot)
        NewNum = GetPlayerInvItemNum(index, NewSlot)
        NewValue = GetPlayerInvItemValue(index, NewSlot)

        If OldNum = NewNum AndAlso Item(NewNum).Stackable = 1 Then ' same item, if we can stack it, lets do that :P
            SetPlayerInvItemNum(index, NewSlot, NewNum)
            SetPlayerInvItemValue(index, NewSlot, OldValue + NewValue)
            SetPlayerInvItemNum(index, OldSlot, 0)
            SetPlayerInvItemValue(index, OldSlot, 0)
        Else
            SetPlayerInvItemNum(index, NewSlot, OldNum)
            SetPlayerInvItemValue(index, NewSlot, OldValue)
            SetPlayerInvItemNum(index, OldSlot, NewNum)
            SetPlayerInvItemValue(index, OldSlot, NewValue)
        End If

        ' RandomInv
        With Player(index).Character(TempPlayer(index).CurChar).RandInv(NewSlot)
            NewPrefix = .Prefix
            NewSuffix = .Suffix
            NewDamage = .Damage
            NewSpeed = .Speed
            NewRarity = .Rarity
            For i = 1 To StatType.Count - 1
                NewStats(i) = .Stat(i)
            Next i
        End With

        With Player(index).Character(TempPlayer(index).CurChar).RandInv(OldSlot)
            OldPrefix = .Prefix
            OldSuffix = .Suffix
            OldDamage = .Damage
            OldSpeed = .Speed
            OldRarity = .Rarity
            For i = 1 To StatType.Count - 1
                OldStats(i) = .Stat(i)
            Next i
        End With

        With Player(index).Character(TempPlayer(index).CurChar).RandInv(NewSlot)
            .Prefix = OldPrefix
            .Suffix = OldSuffix
            .Damage = OldDamage
            .Speed = OldSpeed
            .Rarity = OldRarity
            For i = 1 To StatType.Count - 1
                .Stat(i) = OldStats(i)
            Next i
        End With

        With Player(index).Character(TempPlayer(index).CurChar).RandInv(OldSlot)
            .Prefix = NewPrefix
            .Suffix = NewSuffix
            .Damage = NewDamage
            .Speed = NewSpeed
            .Rarity = NewRarity
            For i = 1 To StatType.Count - 1
                .Stat(i) = NewStats(i)
            Next i
        End With

        SendInventory(index)
    End Sub

#End Region

#Region "Equipment"

    Sub CheckEquippedItems(index As Integer)
        Dim itemNum As Integer
        Dim i As Integer

        ' We want to check incase an admin takes away an object but they had it equipped
        For i = 1 To EquipmentType.Count - 1
            itemNum = GetPlayerEquipment(index, i)

            If itemNum > 0 Then

                Select Case i
                    Case EquipmentType.Weapon

                        If Item(itemNum).SubType <> EquipmentType.Weapon Then SetPlayerEquipment(index, 0, i)
                    Case EquipmentType.Armor

                        If Item(itemNum).SubType <> EquipmentType.Armor Then SetPlayerEquipment(index, 0, i)
                    Case EquipmentType.Helmet

                        If Item(itemNum).SubType <> EquipmentType.Helmet Then SetPlayerEquipment(index, 0, i)
                    Case EquipmentType.Shield

                        If Item(itemNum).SubType <> EquipmentType.Shield Then SetPlayerEquipment(index, 0, i)
                    Case EquipmentType.Shoes

                        If Item(itemNum).SubType <> EquipmentType.Shoes Then SetPlayerEquipment(index, 0, i)
                    Case EquipmentType.Gloves

                        If Item(itemNum).SubType <> EquipmentType.Gloves Then SetPlayerEquipment(index, 0, i)
                End Select
            Else
                SetPlayerEquipment(index, 0, i)
            End If

        Next

    End Sub

    Sub PlayerUnequipItem(index As Integer, EqSlot As Integer)
        Dim i As Integer, m As Integer, itemnum As Integer

        If EqSlot <= 0 OrElse EqSlot > EquipmentType.Count - 1 Then Exit Sub ' exit out early if error'd

        If FindOpenInvSlot(index, GetPlayerEquipment(index, EqSlot)) > 0 Then
            itemnum = GetPlayerEquipment(index, EqSlot)

            m = FindOpenInvSlot(index, Player(index).Character(TempPlayer(index).CurChar).Equipment(EqSlot))
            SetPlayerInvItemNum(index, m, Player(index).Character(TempPlayer(index).CurChar).Equipment(EqSlot))
            SetPlayerInvItemValue(index, m, 0)

            Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Prefix = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EqSlot).Prefix
            Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Suffix = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EqSlot).Suffix
            Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Damage = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EqSlot).Damage
            Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Speed = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EqSlot).Speed
            Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Rarity = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EqSlot).Rarity
            For i = 1 To StatType.Count - 1
                Player(index).Character(TempPlayer(index).CurChar).RandInv(m).Stat(i) = Player(index).Character(TempPlayer(index).CurChar).RandEquip(EqSlot).Stat(i)
            Next

            ClearRandEq(index, EqSlot)

            PlayerMsg(index, "You unequip " & CheckGrammar(Item(GetPlayerEquipment(index, EqSlot)).Name), ColorType.Yellow)
            ' remove equipment
            SetPlayerEquipment(index, 0, EqSlot)
            SendWornEquipment(index)
            SendMapEquipment(index)
            SendStats(index)
            SendInventory(index)
            ' send vitals
            SendVitals(index)

            ' send vitals to party if in one
            If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)
        Else
            PlayerMsg(index, "Your inventory is full.", ColorType.BrightRed)
        End If

    End Sub

#End Region

#Region "Misc"

    Sub JoinGame(index As Integer)
        Dim i As Integer

        ' Set the flag so we know the person is in the game
        TempPlayer(index).InGame = True

        ' Notify everyone that a player has joined the game.
        GlobalMsg(String.Format("{0} has joined {1}!", GetPlayerName(index), Settings.GameName))

        ' Send an ok to client to start receiving in game data
        SendLoadCharOk(index)

        ' Set some data related to housing instances.
        If Player(index).Character(TempPlayer(index).CurChar).InHouse Then
            Player(index).Character(TempPlayer(index).CurChar).InHouse = 0
            Player(index).Character(TempPlayer(index).CurChar).X = Player(index).Character(TempPlayer(index).CurChar).LastX
            Player(index).Character(TempPlayer(index).CurChar).Y = Player(index).Character(TempPlayer(index).CurChar).LastY
            Player(index).Character(TempPlayer(index).CurChar).Map = Player(index).Character(TempPlayer(index).CurChar).LastMap
        End If

        ' Send all the required game data to the user.
        SendTotalOnlineTo(index)
        CheckEquippedItems(index)
        SendGameData(index)
        SendInventory(index)
        SendWornEquipment(index)
        SendMapEquipment(index)
        SendProjectiles(index)
        SendVitals(index)
        SendExp(index)
        SendQuests(index)
        SendPlayerQuests(index)
        SendMapNames(index)
        SendHotbar(index)
        SendPlayerSkills(index)
        SendRecipes(index)
        SendStats(index)
        SendJoinMap(index)
        SendHouseConfigs(index)
        SendPets(index)
        SendUpdatePlayerPet(index, True)
        SendTimeTo(index)
        SendGameClockTo(index)

        For i = 0 To ResourceCache(GetPlayerMap(index)).ResourceCount
            SendResourceCacheTo(index, i)
        Next

        SendTotalOnlineToAll()

        ' Warp the player to his saved location
        PlayerWarp(index, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index))

        ' Send welcome messages
        SendWelcome(index)

        ' Send the flag so they know they can start doing stuff
        SendInGame(index)

        UpdateCaption()
    End Sub

    Sub LeftGame(index As Integer)
        Dim i As Integer
        Dim tradeTarget As Integer

        If TempPlayer(index).InGame Then
            SendLeftMap(index)
            TempPlayer(index).InGame = False

            ' Check if player was the only player on the map and stop npc processing if so
            If GetPlayerMap(index) > 0 Then
                If GetTotalMapPlayers(GetPlayerMap(index)) < 1 Then
                    PlayersOnMap(GetPlayerMap(index)) = False
                    If IsInstancedMap(GetPlayerMap(index)) Then
                        DestroyInstancedMap(GetPlayerMap(index) - MAX_MAPS)

                        If TempPlayer(index).InInstance = 1 Then
                            SetPlayerMap(index, TempPlayer(index).TmpMap)
                            SetPlayerX(index, TempPlayer(index).TmpX)
                            SetPlayerY(index, TempPlayer(index).TmpY)
                            TempPlayer(index).InInstance = 0
                        End If
                    End If
                End If
            End If

            ' Check if the player was in a party, and if so cancel it out so the other player doesn't continue to get half exp
            ' leave party.
            Party_PlayerLeave(index)

            ' cancel any trade they're in
            If TempPlayer(index).InTrade > 0 Then
                tradeTarget = TempPlayer(index).InTrade
                PlayerMsg(tradeTarget, String.Format("{0} has declined the trade.", GetPlayerName(index)), ColorType.BrightRed)
                ' clear out trade
                For i = 1 To MAX_INV
                    TempPlayer(tradeTarget).TradeOffer(i).Num = 0
                    TempPlayer(tradeTarget).TradeOffer(i).Value = 0
                Next
                TempPlayer(tradeTarget).InTrade = 0
                SendCloseTrade(tradeTarget)
            End If

            'pet
            'ReleasePet(Index)
            ReCallPet(index)

            SavePlayer(index)
            SaveBank(index)

            ' Send a global message that he/she left
            GlobalMsg(String.Format("{0} has left {1}!", GetPlayerName(index), Settings.GameName))

            Console.WriteLine(String.Format("{0} has left {1}!", GetPlayerName(index), Settings.GameName))

            TempPlayer(index) = Nothing
            ReDim TempPlayer(i).SkillCd(MAX_PLAYER_SKILLS)
            ReDim TempPlayer(i).TradeOffer(MAX_INV)
        End If

        SendTotalOnlineToAll()

        ClearPlayer(index)
        ClearBank(index)

        UpdateCaption()
    End Sub

    Friend Sub KillPlayer(index As Integer)
        Dim exp As Integer

        ' Calculate exp to give attacker
        exp = GetPlayerExp(index) \ 3

        ' Make sure we dont get less then 0
        If exp < 0 Then exp = 0
        If exp = 0 Then
            PlayerMsg(index, "You've lost no experience.", ColorType.BrightGreen)
        Else
            SetPlayerExp(index, GetPlayerExp(index) - exp)
            SendExp(index)
            PlayerMsg(index, String.Format("You've lost {0} experience.", exp), ColorType.BrightRed)
        End If

        OnDeath(index)
    End Sub

    Sub OnDeath(index As Integer)
        'Dim i As Integer

        ' Set HP to nothing
        SetPlayerVital(index, VitalType.HP, 0)

        ' Warp player away
        SetPlayerDir(index, DirectionType.Down)

        With Map(GetPlayerMap(index))
            ' to the bootmap if it is set
            If .BootMap > 0 Then
                PlayerWarp(index, .BootMap, .BootX, .BootY)
            Else
                PlayerWarp(index, Settings.StartMap, Settings.StartX, Settings.StartY)
            End If
        End With

        ' Clear skill casting
        TempPlayer(index).SkillBuffer = 0
        TempPlayer(index).SkillBufferTimer = 0
        SendClearSkillBuffer(index)

        ' Restore vitals
        SetPlayerVital(index, VitalType.HP, GetPlayerMaxVital(index, VitalType.HP))
        SetPlayerVital(index, VitalType.MP, GetPlayerMaxVital(index, VitalType.MP))
        SetPlayerVital(index, VitalType.SP, GetPlayerMaxVital(index, VitalType.SP))
        SendVitals(index)

        ' send vitals to party if in one
        If TempPlayer(index).InParty > 0 Then SendPartyVitals(TempPlayer(index).InParty, index)

        ' If the player the attacker killed was a pk then take it away
        If GetPlayerPK(index) = True Then
            SetPlayerPK(index, False)
            SendPlayerData(index)
        End If

    End Sub

    Function GetPlayerVitalRegen(index As Integer, Vital As VitalType) As Integer
        Dim i As Integer

        ' Prevent subscript out of range
        If IsPlaying(index) = False OrElse index <= 0 OrElse index > MAX_PLAYERS Then
            GetPlayerVitalRegen = 0
            Exit Function
        End If

        Select Case Vital
            Case VitalType.HP
                i = (GetPlayerStat(index, StatType.Vitality) \ 2)
            Case VitalType.MP
                i = (GetPlayerStat(index, StatType.Spirit) \ 2)
            Case VitalType.SP
                i = (GetPlayerStat(index, StatType.Spirit) \ 2)
        End Select

        If i < 2 Then i = 2
        GetPlayerVitalRegen = i
    End Function

    Friend Sub HandleNpcKillExperience(index As Integer, NpcNum As Integer)
        ' Get the experience we'll have to hand out. If it's negative, just ignore this method.
        Dim Experience = Npc(NpcNum).Exp
        If Experience <= 0 Then Exit Sub

        ' Is our player in a party? If so, hand out exp to everyone.
        If IsPlayerInParty(index) Then
            Party_ShareExp(GetPlayerParty(index), Experience, index, GetPlayerMap(index))
        Else
            GivePlayerExp(index, Experience)
        End If
    End Sub

    Friend Sub HandlePlayerKillExperience(Attacker As Integer, Victim As Integer)
        ' Calculate exp to give attacker
        Dim exp = (GetPlayerExp(Victim) \ 10)

        ' Make sure we dont get less then 0
        If exp < 0 Then
            exp = 0
        End If

        If exp = 0 Then
            PlayerMsg(Victim, "You've lost no exp.", ColorType.BrightRed)
            PlayerMsg(Attacker, "You've received no exp.", ColorType.BrightBlue)
        Else
            SetPlayerExp(Victim, GetPlayerExp(Victim) - exp)
            SendExp(Victim)
            PlayerMsg(Victim, String.Format("You've lost {0} exp.", exp), ColorType.BrightRed)

            ' check if we're in a party
            If IsPlayerInParty(Attacker) > 0 Then
                ' pass through party exp share function
                Party_ShareExp(GetPlayerParty(Attacker), exp, Attacker, GetPlayerMap(Attacker))
            Else
                ' not in party, get exp for self
                GivePlayerExp(Attacker, exp)
            End If
        End If
    End Sub

#End Region

#Region "Skills"

    Function FindOpenSkillSlot(index As Integer) As Integer
        Dim i As Integer

        For i = 1 To MAX_PLAYER_SKILLS

            If GetPlayerSkill(index, i) = 0 Then
                FindOpenSkillSlot = i
                Exit Function
            End If

        Next

    End Function

    Function GetPlayerSkill(index As Integer, Skillslot As Integer) As Integer
        GetPlayerSkill = 0
        If index > MAX_PLAYERS Then Exit Function

        GetPlayerSkill = Player(index).Character(TempPlayer(index).CurChar).Skill(Skillslot)
    End Function

    Friend Function GetPlayerSkillSlot(index As Integer, SkillId As Integer) As Integer
        GetPlayerSkillSlot = -1
        If index < 0 OrElse index > MAX_PLAYERS Then Exit Function
        Dim data = Player(index).Character(TempPlayer(index).CurChar).Skill.Where(Function(x) x = SkillId).ToArray()
        If data.Length > 0 Then
            GetPlayerSkillSlot = data.Single()
        End If
    End Function

    Function HasSkill(index As Integer, Skillnum As Integer) As Boolean
        Dim i As Integer

        For i = 1 To MAX_PLAYER_SKILLS

            If GetPlayerSkill(index, i) = Skillnum Then
                HasSkill = True
                Exit Function
            End If

        Next

    End Function

    Sub SetPlayerSkill(index As Integer, Skillslot As Integer, Skillnum As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Skill(Skillslot) = Skillnum
    End Sub

    Friend Sub BufferSkill(index As Integer, Skillslot As Integer)
        Dim skillnum As Integer
        Dim MPCost As Integer
        Dim LevelReq As Integer
        Dim mapNum As Integer
        Dim SkillCastType As Integer
        Dim ClassReq As Integer
        Dim AccessReq As Integer
        Dim range As Integer
        Dim HasBuffered As Boolean

        Dim TargetType As TargetType
        Dim Target As Integer

        ' Prevent subscript out of range
        If Skillslot <= 0 OrElse Skillslot > MAX_PLAYER_SKILLS Then Exit Sub

        skillnum = GetPlayerSkill(index, Skillslot)
        mapNum = GetPlayerMap(index)

        If skillnum <= 0 OrElse skillnum > MAX_SKILLS Then Exit Sub

        ' Make sure player has the skill
        If Not HasSkill(index, skillnum) Then Exit Sub

        ' see if cooldown has finished
        If TempPlayer(index).SkillCd(Skillslot) > GetTimeMs() Then
            PlayerMsg(index, "Skill hasn't cooled down yet!", ColorType.Yellow)
            Exit Sub
        End If

        MPCost = Skill(skillnum).MpCost

        ' Check if they have enough MP
        If GetPlayerVital(index, VitalType.MP) < MPCost Then
            PlayerMsg(index, "Not enough mana!", ColorType.Yellow)
            Exit Sub
        End If

        LevelReq = Skill(skillnum).LevelReq

        ' Make sure they are the right level
        If LevelReq > GetPlayerLevel(index) Then
            PlayerMsg(index, "You must be level " & LevelReq & " to use this skill.", ColorType.BrightRed)
            Exit Sub
        End If

        AccessReq = Skill(skillnum).AccessReq

        ' make sure they have the right access
        If AccessReq > GetPlayerAccess(index) Then
            PlayerMsg(index, "You must be an administrator to use this skill.", ColorType.BrightRed)
            Exit Sub
        End If

        ClassReq = Skill(skillnum).ClassReq

        ' make sure the classreq > 0
        If ClassReq > 0 Then ' 0 = no req
            If ClassReq <> GetPlayerClass(index) Then
                PlayerMsg(index, "Only " & CheckGrammar(Trim$(Classes(ClassReq).Name)) & " can use this skill.", ColorType.Yellow)
                Exit Sub
            End If
        End If

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

        TargetType = TempPlayer(index).TargetType
        Target = TempPlayer(index).Target
        range = Skill(skillnum).Range
        HasBuffered = False

        Select Case SkillCastType
            Case 0, 1 ' self-cast & self-cast AOE
                HasBuffered = True
            Case 2, 3 ' targeted & targeted AOE
                ' check if have target
                If Not Target > 0 Then
                    PlayerMsg(index, "You do not have a target.", ColorType.BrightRed)
                End If
                If TargetType = TargetType.Player Then
                    'Housing
                    If Player(Target).Character(TempPlayer(Target).CurChar).InHouse = Player(index).Character(TempPlayer(index).CurChar).InHouse Then
                        If CanPlayerAttackPlayer(index, Target, True) Then
                            HasBuffered = True
                        End If
                    End If
                    ' if have target, check in range
                    If Not IsInRange(range, GetPlayerX(index), GetPlayerY(index), GetPlayerX(Target), GetPlayerY(Target)) Then
                        PlayerMsg(index, "Target not in range.", ColorType.BrightRed)
                    Else
                        ' go through skill types
                        If Skill(skillnum).Type <> SkillType.DamageHp AndAlso Skill(skillnum).Type <> SkillType.DamageMp Then
                            HasBuffered = True
                        Else
                            If CanPlayerAttackPlayer(index, Target, True) Then
                                HasBuffered = True
                            End If
                        End If
                    End If
                ElseIf TargetType = TargetType.Npc Then
                    ' if have target, check in range
                    If Not IsInRange(range, GetPlayerX(index), GetPlayerY(index), MapNpc(mapNum).Npc(Target).X, MapNpc(mapNum).Npc(Target).Y) Then
                        PlayerMsg(index, "Target not in range.", ColorType.BrightRed)
                        HasBuffered = False
                    Else
                        ' go through skill types
                        If Skill(skillnum).Type <> SkillType.DamageHp AndAlso Skill(skillnum).Type <> SkillType.DamageMp Then
                            HasBuffered = True
                        Else
                            If CanPlayerAttackNpc(index, Target, True) Then
                                HasBuffered = True
                            End If
                        End If
                    End If
                End If
        End Select

        If HasBuffered Then
            SendAnimation(mapNum, Skill(skillnum).CastAnim, 0, 0, TargetType.Player, index)
            TempPlayer(index).SkillBuffer = Skillslot
            TempPlayer(index).SkillBufferTimer = GetTimeMs()
            Exit Sub
        Else
            SendClearSkillBuffer(index)
        End If
    End Sub

#End Region

#Region "Bank"

    Sub GiveBankItem(index As Integer, InvSlot As Integer, Amount As Integer)
        Dim BankSlot As Integer, itemnum As Integer

        If InvSlot < 0 OrElse InvSlot > MAX_INV Then Exit Sub

        If GetPlayerInvItemValue(index, InvSlot) < 0 Then Exit Sub
        If GetPlayerInvItemValue(index, InvSlot) < Amount Then Exit Sub

        BankSlot = FindOpenBankSlot(index, GetPlayerInvItemNum(index, InvSlot))
        itemnum = GetPlayerInvItemNum(index, InvSlot)

        If BankSlot > 0 Then
            If Item(GetPlayerInvItemNum(index, InvSlot)).Type = ItemType.Currency OrElse Item(GetPlayerInvItemNum(index, InvSlot)).Stackable = 1 Then
                If GetPlayerBankItemNum(index, BankSlot) = GetPlayerInvItemNum(index, InvSlot) Then
                    SetPlayerBankItemValue(index, BankSlot, GetPlayerBankItemValue(index, BankSlot) + Amount)
                    TakeInvItem(index, GetPlayerInvItemNum(index, InvSlot), Amount)
                Else
                    SetPlayerBankItemNum(index, BankSlot, GetPlayerInvItemNum(index, InvSlot))
                    SetPlayerBankItemValue(index, BankSlot, Amount)
                    TakeInvItem(index, GetPlayerInvItemNum(index, InvSlot), Amount)
                End If
            Else
                If GetPlayerBankItemNum(index, BankSlot) = GetPlayerInvItemNum(index, InvSlot) AndAlso Item(itemnum).Randomize = 0 Then
                    SetPlayerBankItemValue(index, BankSlot, GetPlayerBankItemValue(index, BankSlot) + 1)
                    TakeInvItem(index, GetPlayerInvItemNum(index, InvSlot), 0)
                Else
                    Bank(index).ItemRand(BankSlot).Prefix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvSlot).Prefix
                    Bank(index).ItemRand(BankSlot).Suffix = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvSlot).Suffix
                    Bank(index).ItemRand(BankSlot).Rarity = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvSlot).Rarity
                    Bank(index).ItemRand(BankSlot).Damage = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvSlot).Damage
                    Bank(index).ItemRand(BankSlot).Speed = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvSlot).Speed

                    For i = 1 To StatType.Count - 1
                        Bank(index).ItemRand(BankSlot).Stat(i) = Player(index).Character(TempPlayer(index).CurChar).RandInv(InvSlot).Stat(i)
                    Next

                    SetPlayerBankItemNum(index, BankSlot, itemnum)
                    SetPlayerBankItemValue(index, BankSlot, 1)
                    ClearRandInv(index, InvSlot)
                    TakeInvItem(index, GetPlayerInvItemNum(index, InvSlot), 0)
                End If
            End If
        End If

        SaveBank(index)
        SavePlayer(index)
        SendBank(index)

    End Sub

    Function GetPlayerBankItemNum(index As Integer, BankSlot As Byte) As Integer
        GetPlayerBankItemNum = Bank(index).Item(BankSlot).Num
    End Function

    Sub SetPlayerBankItemNum(index As Integer, BankSlot As Byte, ItemNum As Integer)
        Bank(index).Item(BankSlot).Num = ItemNum
    End Sub

    Function GetPlayerBankItemValue(index As Integer, BankSlot As Byte) As Integer
        GetPlayerBankItemValue = Bank(index).Item(BankSlot).Value
    End Function

    Sub SetPlayerBankItemValue(index As Integer, BankSlot As Byte, ItemValue As Integer)
        Bank(index).Item(BankSlot).Value = ItemValue
    End Sub

    Function FindOpenBankSlot(index As Integer, ItemNum As Integer) As Byte
        Dim i As Integer

        If Not IsPlaying(index) Then Exit Function
        If ItemNum <= 0 OrElse ItemNum > MAX_ITEMS Then Exit Function

        If Item(ItemNum).Type = ItemType.Currency OrElse Item(ItemNum).Stackable = 1 Then
            For i = 1 To MAX_BANK
                If GetPlayerBankItemNum(index, i) = ItemNum Then
                    FindOpenBankSlot = i
                    Exit Function
                End If
            Next
        End If

        For i = 1 To MAX_BANK
            If GetPlayerBankItemNum(index, i) = 0 Then
                FindOpenBankSlot = i
                Exit Function
            End If
        Next

    End Function

    Sub TakeBankItem(index As Integer, BankSlot As Integer, Amount As Integer)
        Dim invSlot

        If BankSlot < 0 OrElse BankSlot > MAX_BANK Then Exit Sub

        If GetPlayerBankItemValue(index, BankSlot) < 0 Then Exit Sub

        If GetPlayerBankItemValue(index, BankSlot) < Amount Then Exit Sub

        invSlot = FindOpenInvSlot(index, GetPlayerBankItemNum(index, BankSlot))

        If invSlot > 0 Then
            If Item(GetPlayerBankItemNum(index, BankSlot)).Type = ItemType.Currency OrElse Item(GetPlayerBankItemNum(index, BankSlot)).Stackable = 1 Then
                GiveInvItem(index, GetPlayerBankItemNum(index, BankSlot), Amount)
                SetPlayerBankItemValue(index, BankSlot, GetPlayerBankItemValue(index, BankSlot) - Amount)
                If GetPlayerBankItemValue(index, BankSlot) <= 0 Then
                    SetPlayerBankItemNum(index, BankSlot, 0)
                    SetPlayerBankItemValue(index, BankSlot, 0)
                End If
            Else
                If GetPlayerBankItemNum(index, BankSlot) = GetPlayerInvItemNum(index, invSlot) AndAlso Item(GetPlayerBankItemNum(index, BankSlot)).Randomize = 0 Then
                    If GetPlayerBankItemValue(index, BankSlot) > 1 Then
                        GiveInvItem(index, GetPlayerBankItemNum(index, BankSlot), 0)
                        SetPlayerBankItemValue(index, BankSlot, GetPlayerBankItemValue(index, BankSlot) - 1)

                    End If
                Else
                    Player(index).Character(TempPlayer(index).CurChar).RandInv(invSlot).Prefix = Bank(index).ItemRand(BankSlot).Prefix
                    Player(index).Character(TempPlayer(index).CurChar).RandInv(invSlot).Suffix = Bank(index).ItemRand(BankSlot).Suffix
                    Player(index).Character(TempPlayer(index).CurChar).RandInv(invSlot).Rarity = Bank(index).ItemRand(BankSlot).Rarity
                    Player(index).Character(TempPlayer(index).CurChar).RandInv(invSlot).Damage = Bank(index).ItemRand(BankSlot).Damage
                    Player(index).Character(TempPlayer(index).CurChar).RandInv(invSlot).Speed = Bank(index).ItemRand(BankSlot).Speed
                    For i = 1 To StatType.Count - 1
                        Player(index).Character(TempPlayer(index).CurChar).RandInv(invSlot).Stat(i) = Bank(index).ItemRand(BankSlot).Stat(i)
                    Next i

                    GiveInvItem(index, GetPlayerBankItemNum(index, BankSlot), 0)
                    SetPlayerBankItemNum(index, BankSlot, 0)
                    SetPlayerBankItemValue(index, BankSlot, 0)
                    ClearRandBank(index, BankSlot)

                End If
            End If

        End If

        SaveBank(index)
        SavePlayer(index)
        SendBank(index)

    End Sub

    Sub PlayerSwitchBankSlots(index As Integer, OldSlot As Integer, NewSlot As Integer)
        Dim OldNum As Integer, OldValue As Integer, NewNum As Integer, NewValue As Integer
        Dim i As Integer, NewStats() As Integer, OldStats() As Integer
        Dim NewRarity As Integer, OldRarity As Integer, NewPrefix As String, OldPrefix As String, NewSuffix As String
        Dim OldSuffix As String, NewSpeed As Integer, OldSpeed As Integer, NewDamage As Integer, OldDamage As Integer

        If OldSlot = 0 OrElse NewSlot = 0 Then Exit Sub

        OldNum = GetPlayerBankItemNum(index, OldSlot)
        OldValue = GetPlayerBankItemValue(index, OldSlot)
        NewNum = GetPlayerBankItemNum(index, NewSlot)
        NewValue = GetPlayerBankItemValue(index, NewSlot)

        SetPlayerBankItemNum(index, NewSlot, OldNum)
        SetPlayerBankItemValue(index, NewSlot, OldValue)

        SetPlayerBankItemNum(index, OldSlot, NewNum)
        SetPlayerBankItemValue(index, OldSlot, NewValue)

        ReDim OldStats(StatType.Count - 1)
        ReDim NewStats(StatType.Count - 1)

        ' RandomInv
        With Bank(index).ItemRand(NewSlot)
            NewPrefix = .Prefix
            NewSuffix = .Suffix
            NewDamage = .Damage
            NewSpeed = .Speed
            NewRarity = .Rarity
            For i = 1 To StatType.Count - 1
                NewStats(i) = .Stat(i)
            Next i
        End With

        With Bank(index).ItemRand(OldSlot)
            OldPrefix = .Prefix
            OldSuffix = .Suffix
            OldDamage = .Damage
            OldSpeed = .Speed
            OldRarity = .Rarity
            For i = 1 To StatType.Count - 1
                OldStats(i) = .Stat(i)
            Next i
        End With

        With Bank(index).ItemRand(NewSlot)
            .Prefix = OldPrefix
            .Suffix = OldSuffix
            .Damage = OldDamage
            .Speed = OldSpeed
            .Rarity = OldRarity
            For i = 1 To StatType.Count - 1
                .Stat(i) = OldStats(i)
            Next i
        End With

        With Bank(index).ItemRand(OldSlot)
            .Prefix = NewPrefix
            .Suffix = NewSuffix
            .Damage = NewDamage
            .Speed = NewSpeed
            .Rarity = NewRarity
            For i = 1 To StatType.Count - 1
                .Stat(i) = NewStats(i)
            Next i
        End With

        SendBank(index)
    End Sub

#End Region

End Module