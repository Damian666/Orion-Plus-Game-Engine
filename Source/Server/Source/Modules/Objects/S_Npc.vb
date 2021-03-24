Imports Asfw

Module S_Npc

#Region "Spawning"

    Sub SpawnAllMapNpcs()
        Dim i As Integer

        For i = 1 To MAX_CACHED_MAPS
            SpawnMapNpcs(i)
        Next

    End Sub

    Sub SpawnMapNpcs(mapNum As Integer)
        Dim i As Integer

        For i = 1 To MAX_MAP_NPCS
            SpawnNpc(i, mapNum)
        Next

    End Sub

    Friend Sub SpawnNpc(mapNpcNum As Integer, mapNum As Integer)
        Dim buffer As New ByteStream(4)
        Dim npcNum As Integer
        Dim x As Integer
        Dim y As Integer
        Dim i = 0
        Dim spawned As Boolean

        ' Check for subscript out of range
        If mapNpcNum <= 0 OrElse mapNpcNum > MAX_MAP_NPCS OrElse mapNum <= 0 OrElse mapNum > MAX_CACHED_MAPS Then Exit Sub

        npcNum = Map(mapNum).Npc(mapNpcNum)

        If npcNum > 0 Then
            If Not Npc(npcNum).SpawnTime = Time.Instance.TimeOfDay AndAlso Not Npc(npcNum).SpawnTime = 4 Then Exit Sub

            MapNpc(mapNum).Npc(mapNpcNum).Num = npcNum
            MapNpc(mapNum).Npc(mapNpcNum).Target = 0
            MapNpc(mapNum).Npc(mapNpcNum).TargetType = 0 ' clear

            MapNpc(mapNum).Npc(mapNpcNum).Vital(VitalType.HP) = GetNpcMaxVital(npcNum, VitalType.HP)
            MapNpc(mapNum).Npc(mapNpcNum).Vital(VitalType.MP) = GetNpcMaxVital(npcNum, VitalType.MP)
            MapNpc(mapNum).Npc(mapNpcNum).Vital(VitalType.SP) = GetNpcMaxVital(npcNum, VitalType.SP)

            MapNpc(mapNum).Npc(mapNpcNum).Dir = Int(Rnd() * 4)

            'Check if theres a spawn tile for the specific npc
            For x = 0 To Map(mapNum).MaxX
                For y = 0 To Map(mapNum).MaxY
                    If Map(mapNum).Tile(x, y).Type = TileType.NpcSpawn Then
                        If Map(mapNum).Tile(x, y).Data1 = mapNpcNum Then
                            MapNpc(mapNum).Npc(mapNpcNum).X = x
                            MapNpc(mapNum).Npc(mapNpcNum).Y = y
                            MapNpc(mapNum).Npc(mapNpcNum).Dir = Map(mapNum).Tile(x, y).Data2
                            spawned = True
                            Exit For
                        End If
                    End If
                Next y
            Next x

            If Not spawned Then
                ' Well try 100 times to randomly place the sprite
                While i < 100
                    x = Random(0, Map(mapNum).MaxX)
                    y = Random(0, Map(mapNum).MaxY)

                    If x > Map(mapNum).MaxX Then x = Map(mapNum).MaxX
                    If y > Map(mapNum).MaxY Then y = Map(mapNum).MaxY

                    ' Check if the tile is walkable
                    If NpcTileIsOpen(mapNum, x, y) Then
                        MapNpc(mapNum).Npc(mapNpcNum).X = x
                        MapNpc(mapNum).Npc(mapNpcNum).Y = y
                        spawned = True
                        Exit While
                    End If
                    i += 1
                End While
            End If

            ' Didn't spawn, so now we'll just try to find a free tile
            If Not spawned Then
                For x = 0 To Map(mapNum).MaxX
                    For y = 0 To Map(mapNum).MaxY
                        If NpcTileIsOpen(mapNum, x, y) Then
                            MapNpc(mapNum).Npc(mapNpcNum).X = x
                            MapNpc(mapNum).Npc(mapNpcNum).Y = y
                            spawned = True
                        End If
                    Next
                Next
            End If

            ' If we suceeded in spawning then send it to everyone
            If spawned Then
                buffer.WriteInt32(ServerPackets.SSpawnNpc)
                buffer.WriteInt32(mapNpcNum)
                buffer.WriteInt32(MapNpc(mapNum).Npc(mapNpcNum).Num)
                buffer.WriteInt32(MapNpc(mapNum).Npc(mapNpcNum).X)
                buffer.WriteInt32(MapNpc(mapNum).Npc(mapNpcNum).Y)
                buffer.WriteInt32(MapNpc(mapNum).Npc(mapNpcNum).Dir)

                AddDebug("Recieved SMSG: SSpawnNpc")

                For i = 1 To VitalType.Count - 1
                    buffer.WriteInt32(MapNpc(mapNum).Npc(mapNpcNum).Vital(i))
                Next

                SendDataToMap(mapNum, buffer.Data, buffer.Head)
            End If

            SendMapNpcVitals(mapNum, mapNpcNum)
        End If

        buffer.Dispose()
    End Sub

#End Region

#Region "Movement"

    Friend Function NpcTileIsOpen(mapNum As Integer, x As Integer, y As Integer) As Boolean
        Dim LoopI As Integer
        NpcTileIsOpen = True

        If PlayersOnMap(mapNum) Then
            For LoopI = 1 To Socket.HighIndex
                If GetPlayerMap(LoopI) = mapNum AndAlso GetPlayerX(LoopI) = x AndAlso GetPlayerY(LoopI) = y Then
                    NpcTileIsOpen = False
                    Exit Function
                End If
            Next
        End If

        For LoopI = 1 To MAX_MAP_NPCS
            If MapNpc(mapNum).Npc(LoopI).Num > 0 AndAlso MapNpc(mapNum).Npc(LoopI).X = x AndAlso MapNpc(mapNum).Npc(LoopI).Y = y Then
                NpcTileIsOpen = False
                Exit Function
            End If
        Next

        If Map(mapNum).Tile(x, y).Type <> TileType.None AndAlso Map(mapNum).Tile(x, y).Type <> TileType.NpcSpawn AndAlso Map(mapNum).Tile(x, y).Type <> TileType.Item Then
            NpcTileIsOpen = False
        End If

    End Function

    Function CanNpcMove(mapNum As Integer, MapNpcNum As Integer, Dir As Byte) As Boolean
        Dim i As Integer
        Dim n As Integer
        Dim x As Integer
        Dim y As Integer

        ' Check for subscript out of range
        If mapNum <= 0 OrElse mapNum > MAX_CACHED_MAPS OrElse MapNpcNum <= 0 OrElse MapNpcNum > MAX_MAP_NPCS OrElse Dir < DirectionType.Up OrElse Dir > DirectionType.Right Then
            Exit Function
        End If

        x = MapNpc(mapNum).Npc(MapNpcNum).X
        y = MapNpc(mapNum).Npc(MapNpcNum).Y
        CanNpcMove = True

        Select Case Dir
            Case DirectionType.Up

                ' Check to make sure not outside of boundries
                If y > 0 Then
                    n = Map(mapNum).Tile(x, y - 1).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.Item AndAlso n <> TileType.NpcSpawn Then
                        CanNpcMove = False
                        Exit Function
                    End If

                    ' Check to make sure that there is not a player in the way
                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = mapNum) AndAlso (GetPlayerX(i) = MapNpc(mapNum).Npc(MapNpcNum).X) AndAlso (GetPlayerY(i) = MapNpc(mapNum).Npc(MapNpcNum).Y - 1) Then
                                CanNpcMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (i <> MapNpcNum) AndAlso (MapNpc(mapNum).Npc(i).Num > 0) AndAlso (MapNpc(mapNum).Npc(i).X = MapNpc(mapNum).Npc(MapNpcNum).X) AndAlso (MapNpc(mapNum).Npc(i).Y = MapNpc(mapNum).Npc(MapNpcNum).Y - 1) Then
                            CanNpcMove = False
                            Exit Function
                        End If
                    Next
                Else
                    CanNpcMove = False
                End If

            Case DirectionType.Down

                ' Check to make sure not outside of boundries
                If y < Map(mapNum).MaxY Then
                    n = Map(mapNum).Tile(x, y + 1).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.Item AndAlso n <> TileType.NpcSpawn Then
                        CanNpcMove = False
                        Exit Function
                    End If

                    ' Check to make sure that there is not a player in the way
                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = mapNum) AndAlso (GetPlayerX(i) = MapNpc(mapNum).Npc(MapNpcNum).X) AndAlso (GetPlayerY(i) = MapNpc(mapNum).Npc(MapNpcNum).Y + 1) Then
                                CanNpcMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (i <> MapNpcNum) AndAlso (MapNpc(mapNum).Npc(i).Num > 0) AndAlso (MapNpc(mapNum).Npc(i).X = MapNpc(mapNum).Npc(MapNpcNum).X) AndAlso (MapNpc(mapNum).Npc(i).Y = MapNpc(mapNum).Npc(MapNpcNum).Y + 1) Then
                            CanNpcMove = False
                            Exit Function
                        End If
                    Next
                Else
                    CanNpcMove = False
                End If

            Case DirectionType.Left

                ' Check to make sure not outside of boundries
                If x > 0 Then
                    n = Map(mapNum).Tile(x - 1, y).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.Item AndAlso n <> TileType.NpcSpawn Then
                        CanNpcMove = False
                        Exit Function
                    End If

                    ' Check to make sure that there is not a player in the way
                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = mapNum) AndAlso (GetPlayerX(i) = MapNpc(mapNum).Npc(MapNpcNum).X - 1) AndAlso (GetPlayerY(i) = MapNpc(mapNum).Npc(MapNpcNum).Y) Then
                                CanNpcMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (i <> MapNpcNum) AndAlso (MapNpc(mapNum).Npc(i).Num > 0) AndAlso (MapNpc(mapNum).Npc(i).X = MapNpc(mapNum).Npc(MapNpcNum).X - 1) AndAlso (MapNpc(mapNum).Npc(i).Y = MapNpc(mapNum).Npc(MapNpcNum).Y) Then
                            CanNpcMove = False
                            Exit Function
                        End If
                    Next
                Else
                    CanNpcMove = False
                End If

            Case DirectionType.Right

                ' Check to make sure not outside of boundries
                If x < Map(mapNum).MaxX Then
                    n = Map(mapNum).Tile(x + 1, y).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.Item AndAlso n <> TileType.NpcSpawn Then
                        CanNpcMove = False
                        Exit Function
                    End If

                    ' Check to make sure that there is not a player in the way
                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = mapNum) AndAlso (GetPlayerX(i) = MapNpc(mapNum).Npc(MapNpcNum).X + 1) AndAlso (GetPlayerY(i) = MapNpc(mapNum).Npc(MapNpcNum).Y) Then
                                CanNpcMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (i <> MapNpcNum) AndAlso (MapNpc(mapNum).Npc(i).Num > 0) AndAlso (MapNpc(mapNum).Npc(i).X = MapNpc(mapNum).Npc(MapNpcNum).X + 1) AndAlso (MapNpc(mapNum).Npc(i).Y = MapNpc(mapNum).Npc(MapNpcNum).Y) Then
                            CanNpcMove = False
                            Exit Function
                        End If
                    Next
                Else
                    CanNpcMove = False
                End If

        End Select

        If MapNpc(mapNum).Npc(MapNpcNum).SkillBuffer > 0 Then CanNpcMove = False

    End Function

    Sub NpcMove(mapNum As Integer, MapNpcNum As Integer, Dir As Integer, Movement As Integer)
        Dim buffer As New ByteStream(4)

        ' Check for subscript out of range
        If mapNum <= 0 OrElse mapNum > MAX_CACHED_MAPS OrElse MapNpcNum <= 0 OrElse MapNpcNum > MAX_MAP_NPCS OrElse Dir < DirectionType.Up OrElse Dir > DirectionType.Right OrElse Movement < 1 OrElse Movement > 2 Then
            Exit Sub
        End If

        MapNpc(mapNum).Npc(MapNpcNum).Dir = Dir

        Select Case Dir
            Case DirectionType.Up
                MapNpc(mapNum).Npc(MapNpcNum).Y = MapNpc(mapNum).Npc(MapNpcNum).Y - 1

                buffer.WriteInt32(ServerPackets.SNpcMove)
                buffer.WriteInt32(MapNpcNum)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).X)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).Y)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).Dir)
                buffer.WriteInt32(Movement)

                Addlog("Sent SMSG: SNpcMove Up", PACKET_LOG)
                Console.WriteLine("Sent SMSG: SNpcMove Up")

                SendDataToMap(mapNum, buffer.Data, buffer.Head)
            Case DirectionType.Down
                MapNpc(mapNum).Npc(MapNpcNum).Y = MapNpc(mapNum).Npc(MapNpcNum).Y + 1

                buffer.WriteInt32(ServerPackets.SNpcMove)
                buffer.WriteInt32(MapNpcNum)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).X)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).Y)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).Dir)
                buffer.WriteInt32(Movement)

                Addlog("Sent SMSG: SNpcMove Down", PACKET_LOG)
                Console.WriteLine("Sent SMSG: SNpcMove Down")

                SendDataToMap(mapNum, buffer.Data, buffer.Head)
            Case DirectionType.Left
                MapNpc(mapNum).Npc(MapNpcNum).X = MapNpc(mapNum).Npc(MapNpcNum).X - 1

                buffer.WriteInt32(ServerPackets.SNpcMove)
                buffer.WriteInt32(MapNpcNum)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).X)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).Y)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).Dir)
                buffer.WriteInt32(Movement)

                Addlog("Sent SMSG: SNpcMove Left", PACKET_LOG)
                Console.WriteLine("Sent SMSG: SNpcMove Left")

                SendDataToMap(mapNum, buffer.Data, buffer.Head)
            Case DirectionType.Right
                MapNpc(mapNum).Npc(MapNpcNum).X = MapNpc(mapNum).Npc(MapNpcNum).X + 1

                buffer.WriteInt32(ServerPackets.SNpcMove)
                buffer.WriteInt32(MapNpcNum)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).X)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).Y)
                buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).Dir)
                buffer.WriteInt32(Movement)

                Addlog("Sent SMSG: SNpcMove Right", PACKET_LOG)
                Console.WriteLine("Sent SMSG: SNpcMove Right")

                SendDataToMap(mapNum, buffer.Data, buffer.Head)
        End Select

        buffer.Dispose()
    End Sub

    Sub NpcDir(mapNum As Integer, MapNpcNum As Integer, Dir As Integer)
        Dim buffer As New ByteStream(4)

        ' Check for subscript out of range
        If mapNum <= 0 OrElse mapNum > MAX_CACHED_MAPS OrElse MapNpcNum <= 0 OrElse MapNpcNum > MAX_MAP_NPCS OrElse Dir < DirectionType.Up OrElse Dir > DirectionType.Right Then
            Exit Sub
        End If

        MapNpc(mapNum).Npc(MapNpcNum).Dir = Dir

        buffer.WriteInt32(ServerPackets.SNpcDir)
        buffer.WriteInt32(MapNpcNum)
        buffer.WriteInt32(Dir)

        Addlog("Sent SMSG: SNpcDir", PACKET_LOG)
        Console.WriteLine("Sent SMSG: SNpcDir")

        SendDataToMap(mapNum, buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

#End Region

#Region "Npcombat"

    Friend Sub TryNpcAttackPlayer(mapnpcnum As Integer, index As Integer)

        Dim mapNum As Integer, npcnum As Integer, Damage As Integer, i As Integer, armor As Integer

        ' Can the npc attack the player?

        If CanNpcAttackPlayer(mapnpcnum, index) Then
            mapNum = GetPlayerMap(index)
            npcnum = MapNpc(mapNum).Npc(mapnpcnum).Num

            ' check if PLAYER can avoid the attack
            If CanPlayerDodge(index) Then
                SendActionMsg(mapNum, "Dodge!", ColorType.Pink, 1, (Player(index).Character(TempPlayer(index).CurChar).X * 32), (Player(index).Character(TempPlayer(index).CurChar).Y * 32))
                Exit Sub
            End If

            If CanPlayerParry(index) Then
                SendActionMsg(mapNum, "Parry!", ColorType.Pink, 1, (Player(index).Character(TempPlayer(index).CurChar).X * 32), (Player(index).Character(TempPlayer(index).CurChar).Y * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            Damage = GetNpcDamage(npcnum)

            If CanPlayerBlockHit(index) Then
                SendActionMsg(mapNum, "Block!", ColorType.Pink, 1, (Player(index).Character(TempPlayer(index).CurChar).X * 32), (Player(index).Character(TempPlayer(index).CurChar).Y * 32))
                Exit Sub
            Else

                For i = 2 To EquipmentType.Count - 1 ' start at 2, so we skip weapon
                    If GetPlayerEquipment(index, i) > 0 Then
                        armor = armor + Item(GetPlayerEquipment(index, i)).Data2
                    End If
                Next
                ' take away armour
                Damage = Damage - ((GetPlayerStat(index, StatType.Spirit) * 2) + (GetPlayerLevel(index) * 2) + armor)

                ' * 1.5 if crit hit
                If CanNpcCrit(npcnum) Then
                    Damage = Damage * 1.5
                    SendActionMsg(mapNum, "Critical!", ColorType.BrightCyan, 1, (MapNpc(mapNum).Npc(mapnpcnum).X * 32), (MapNpc(mapNum).Npc(mapnpcnum).Y * 32))
                End If

            End If

            If Damage > 0 Then
                NpcAttackPlayer(mapnpcnum, index, Damage)
            End If

        End If

    End Sub

    Function CanNpcAttackPlayer(MapNpcNum As Integer, index As Integer) As Boolean
        Dim mapNum As Integer
        Dim NpcNum As Integer

        ' Check for subscript out of range
        If MapNpcNum <= 0 OrElse MapNpcNum > MAX_MAP_NPCS OrElse Not IsPlaying(index) Then
            Exit Function
        End If

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(index)).Npc(MapNpcNum).Num <= 0 Then
            Exit Function
        End If

        mapNum = GetPlayerMap(index)
        NpcNum = MapNpc(mapNum).Npc(MapNpcNum).Num

        ' Make sure the npc isn't already dead
        If MapNpc(mapNum).Npc(MapNpcNum).Vital(VitalType.HP) <= 0 Then
            Exit Function
        End If

        ' Make sure npcs dont attack more then once a second
        If GetTimeMs() < MapNpc(mapNum).Npc(MapNpcNum).AttackTimer + 1000 Then
            Exit Function
        End If

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(index).GettingMap = True Then
            Exit Function
        End If

        MapNpc(mapNum).Npc(MapNpcNum).AttackTimer = GetTimeMs()

        ' Make sure they are on the same map
        If IsPlaying(index) Then
            If NpcNum > 0 Then

                ' Check if at same coordinates
                If (GetPlayerY(index) + 1 = MapNpc(mapNum).Npc(MapNpcNum).Y) AndAlso (GetPlayerX(index) = MapNpc(mapNum).Npc(MapNpcNum).X) Then
                    CanNpcAttackPlayer = True
                Else

                    If (GetPlayerY(index) - 1 = MapNpc(mapNum).Npc(MapNpcNum).Y) AndAlso (GetPlayerX(index) = MapNpc(mapNum).Npc(MapNpcNum).X) Then
                        CanNpcAttackPlayer = True
                    Else

                        If (GetPlayerY(index) = MapNpc(mapNum).Npc(MapNpcNum).Y) AndAlso (GetPlayerX(index) + 1 = MapNpc(mapNum).Npc(MapNpcNum).X) Then
                            CanNpcAttackPlayer = True
                        Else

                            If (GetPlayerY(index) = MapNpc(mapNum).Npc(MapNpcNum).Y) AndAlso (GetPlayerX(index) - 1 = MapNpc(mapNum).Npc(MapNpcNum).X) Then
                                CanNpcAttackPlayer = True
                            End If
                        End If
                    End If
                End If
            End If
        End If

    End Function

    Function CanNpcAttackNpc(mapNum As Integer, Attacker As Integer, Victim As Integer) As Boolean
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
        If MapNpc(mapNum).Npc(Attacker).Num <= 0 Then
            Exit Function
        End If

        ' Check for subscript out of range
        If MapNpc(mapNum).Npc(Victim).Num <= 0 Then
            Exit Function
        End If

        aNpcNum = MapNpc(mapNum).Npc(Attacker).Num
        vNpcNum = MapNpc(mapNum).Npc(Victim).Num

        If aNpcNum <= 0 Then Exit Function
        If vNpcNum <= 0 Then Exit Function

        ' Make sure the npcs arent already dead
        If MapNpc(mapNum).Npc(Attacker).Vital(VitalType.HP) <= 0 Then
            Exit Function
        End If

        ' Make sure the npc isn't already dead
        If MapNpc(mapNum).Npc(Victim).Vital(VitalType.HP) <= 0 Then
            Exit Function
        End If

        ' Make sure npcs dont attack more then once a second
        If GetTimeMs() < MapNpc(mapNum).Npc(Attacker).AttackTimer + 1000 Then
            Exit Function
        End If

        MapNpc(mapNum).Npc(Attacker).AttackTimer = GetTimeMs()

        AttackerX = MapNpc(mapNum).Npc(Attacker).X
        AttackerY = MapNpc(mapNum).Npc(Attacker).Y
        VictimX = MapNpc(mapNum).Npc(Victim).X
        VictimY = MapNpc(mapNum).Npc(Victim).Y

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
        Dim Name As String, mapNum As Integer
        Dim z As Integer, InvCount As Integer, EqCount As Integer, j As Integer, x As Integer
        Dim buffer As New ByteStream(4)

        ' Check for subscript out of range

        If MapNpcNum <= 0 OrElse MapNpcNum > MAX_MAP_NPCS OrElse IsPlaying(Victim) = False Then Exit Sub

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(Victim)).Npc(MapNpcNum).Num <= 0 Then Exit Sub

        mapNum = GetPlayerMap(Victim)
        Name = Trim$(Npc(MapNpc(mapNum).Npc(MapNpcNum).Num).Name)

        ' Send this packet so they can see the npc attacking
        buffer.WriteInt32(ServerPackets.SNpcAttack)
        buffer.WriteInt32(MapNpcNum)
        SendDataToMap(mapNum, buffer.Data, buffer.Head)
        buffer.Dispose()

        If Damage <= 0 Then Exit Sub

        ' set the regen timer
        MapNpc(mapNum).Npc(MapNpcNum).StopRegen = True
        MapNpc(mapNum).Npc(MapNpcNum).StopRegenTimer = GetTimeMs()

        If Damage >= GetPlayerVital(Victim, VitalType.HP) Then
            ' Say damage
            SendActionMsg(GetPlayerMap(Victim), "-" & GetPlayerVital(Victim, VitalType.HP), ColorType.BrightRed, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))

            ' Set NPC target to 0
            MapNpc(mapNum).Npc(MapNpcNum).Target = 0
            MapNpc(mapNum).Npc(MapNpcNum).TargetType = 0

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
            SendAnimation(mapNum, Npc(MapNpc(GetPlayerMap(Victim)).Npc(MapNpcNum).Num).Animation, 0, 0, TargetType.Player, Victim)

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

    Sub NpcAttackNpc(mapNum As Integer, Attacker As Integer, Victim As Integer, Damage As Integer)
        Dim buffer As New ByteStream(4)
        Dim aNpcNum As Integer
        Dim vNpcNum As Integer
        Dim n As Integer

        If Attacker <= 0 OrElse Attacker > MAX_MAP_NPCS Then Exit Sub
        If Victim <= 0 OrElse Victim > MAX_MAP_NPCS Then Exit Sub

        If Damage <= 0 Then Exit Sub

        aNpcNum = MapNpc(mapNum).Npc(Attacker).Num
        vNpcNum = MapNpc(mapNum).Npc(Victim).Num

        If aNpcNum <= 0 Then Exit Sub
        If vNpcNum <= 0 Then Exit Sub

        ' Send this packet so they can see the person attacking
        buffer.WriteInt32(ServerPackets.SNpcAttack)
        buffer.WriteInt32(Attacker)
        SendDataToMap(mapNum, buffer.Data, buffer.Head)
        buffer.Dispose()

        If Damage >= MapNpc(mapNum).Npc(Victim).Vital(VitalType.HP) Then
            SendActionMsg(mapNum, "-" & Damage, ColorType.BrightRed, 1, (MapNpc(mapNum).Npc(Victim).X * 32), (MapNpc(mapNum).Npc(Victim).Y * 32))
            SendBlood(mapNum, MapNpc(mapNum).Npc(Victim).X, MapNpc(mapNum).Npc(Victim).Y)

            ' npc is dead.

            ' Set NPC target to 0
            MapNpc(mapNum).Npc(Attacker).Target = 0
            MapNpc(mapNum).Npc(Attacker).TargetType = 0

            ' Drop the goods if they get it
            Dim tmpitem = Random(1, 5)
            n = Int(Rnd() * Npc(vNpcNum).DropChance(tmpitem)) + 1
            If n = 1 Then
                SpawnItem(Npc(vNpcNum).DropItem(tmpitem), Npc(vNpcNum).DropItemValue(tmpitem), mapNum, MapNpc(mapNum).Npc(Victim).X, MapNpc(mapNum).Npc(Victim).Y)
            End If

            ' Reset victim's stuff so it dies in loop
            MapNpc(mapNum).Npc(Victim).Num = 0
            MapNpc(mapNum).Npc(Victim).SpawnWait = GetTimeMs()
            MapNpc(mapNum).Npc(Victim).Vital(VitalType.HP) = 0

            ' send npc death packet to map
            buffer = New ByteStream(4)
            buffer.WriteInt32(ServerPackets.SNpcDead)
            buffer.WriteInt32(Victim)
            SendDataToMap(mapNum, buffer.Data, buffer.Head)
            buffer.Dispose()
        Else
            ' npc not dead, just do the damage
            MapNpc(mapNum).Npc(Victim).Vital(VitalType.HP) = MapNpc(mapNum).Npc(Victim).Vital(VitalType.HP) - Damage
            ' Say damage
            SendActionMsg(mapNum, "-" & Damage, ColorType.BrightRed, 1, (MapNpc(mapNum).Npc(Victim).X * 32), (MapNpc(mapNum).Npc(Victim).Y * 32))
            SendBlood(mapNum, MapNpc(mapNum).Npc(Victim).X, MapNpc(mapNum).Npc(Victim).Y)
        End If

    End Sub

    Friend Sub KnockBackNpc(index As Integer, NpcNum As Integer, Optional IsSkill As Integer = 0)
        If IsSkill > 0 Then
            For i = 1 To Skill(IsSkill).KnockBackTiles
                If CanNpcMove(GetPlayerMap(index), NpcNum, GetPlayerDir(index)) Then
                    NpcMove(GetPlayerMap(index), NpcNum, GetPlayerDir(index), MovementType.Walking)
                End If
            Next
            MapNpc(GetPlayerMap(index)).Npc(NpcNum).StunDuration = 1
            MapNpc(GetPlayerMap(index)).Npc(NpcNum).StunTimer = GetTimeMs()
        Else
            If Item(GetPlayerEquipment(index, EquipmentType.Weapon)).KnockBack = 1 Then
                For i = 1 To Item(GetPlayerEquipment(index, EquipmentType.Weapon)).KnockBackTiles
                    If CanNpcMove(GetPlayerMap(index), NpcNum, GetPlayerDir(index)) Then
                        NpcMove(GetPlayerMap(index), NpcNum, GetPlayerDir(index), MovementType.Walking)
                    End If
                Next
                MapNpc(GetPlayerMap(index)).Npc(NpcNum).StunDuration = 1
                MapNpc(GetPlayerMap(index)).Npc(NpcNum).StunTimer = GetTimeMs()
            End If
        End If
    End Sub

    Friend Function RandomNpcAttack(mapNum As Integer, MapNpcNum As Integer) As Integer
        Dim i As Integer, SkillList As New List(Of Byte)

        RandomNpcAttack = 0

        If MapNpc(mapNum).Npc(MapNpcNum).SkillBuffer > 0 Then Exit Function

        For i = 1 To MAX_NPC_SKILLS
            If Npc(MapNpc(mapNum).Npc(MapNpcNum).Num).Skill(i) > 0 Then
                SkillList.Add(Npc(MapNpc(mapNum).Npc(MapNpcNum).Num).Skill(i))
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

    Friend Sub BufferNpcSkill(mapNum As Integer, MapNpcNum As Integer, skillslot As Integer)
        Dim skillnum As Integer
        Dim MPCost As Integer
        Dim SkillCastType As Integer
        Dim range As Integer
        Dim HasBuffered As Boolean

        Dim TargetType As Byte
        Dim Target As Integer

        ' Prevent subscript out of range
        If skillslot <= 0 OrElse skillslot > MAX_NPC_SKILLS Then Exit Sub

        skillnum = GetNpcSkill(MapNpc(mapNum).Npc(MapNpcNum).Num, skillslot)

        If skillnum <= 0 OrElse skillnum > MAX_SKILLS Then Exit Sub

        ' see if cooldown has finished
        If MapNpc(mapNum).Npc(MapNpcNum).SkillCd(skillslot) > GetTimeMs() Then
            TryNpcAttackPlayer(MapNpcNum, MapNpc(mapNum).Npc(MapNpcNum).Target)
            Exit Sub
        End If

        MPCost = Skill(skillnum).MpCost

        ' Check if they have enough MP
        If MapNpc(mapNum).Npc(MapNpcNum).Vital(VitalType.MP) < MPCost Then Exit Sub

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

        TargetType = MapNpc(mapNum).Npc(MapNpcNum).TargetType
        Target = MapNpc(mapNum).Npc(MapNpcNum).Target
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
                If TargetType = modEnumerators.TargetType.Player Then
                    ' if have target, check in range
                    If Not IsInRange(range, MapNpc(mapNum).Npc(MapNpcNum).X, MapNpc(mapNum).Npc(MapNpcNum).Y, GetPlayerX(Target), GetPlayerY(Target)) Then
                        Exit Sub
                    Else
                        HasBuffered = True
                    End If
                ElseIf TargetType = modEnumerators.TargetType.Npc Then
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
            SendAnimation(mapNum, Skill(skillnum).CastAnim, 0, 0, modEnumerators.TargetType.Player, Target)
            MapNpc(mapNum).Npc(MapNpcNum).SkillBuffer = skillslot
            MapNpc(mapNum).Npc(MapNpcNum).SkillBufferTimer = GetTimeMs()
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

    Friend Sub SpellNpc_Effect(Vital As Byte, increment As Boolean, index As Integer, Damage As Integer, Skillnum As Integer, mapNum As Integer)
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

            SendAnimation(mapNum, Skill(Skillnum).SkillAnim, 0, 0, TargetType.Npc, index)
            SendActionMsg(mapNum, sSymbol & Damage, Colour, ActionMsgType.Scroll, MapNpc(mapNum).Npc(index).X * 32, MapNpc(mapNum).Npc(index).Y * 32)

            ' send the sound
            'SendMapSound(Index, MapNpc(MapNum).Npc(Index).x, MapNpc(MapNum).Npc(Index).y, SoundEntity.seSpell, Skillnum)

            If increment Then
                MapNpc(mapNum).Npc(index).Vital(Vital) = MapNpc(mapNum).Npc(index).Vital(Vital) + Damage

                If Skill(Skillnum).Duration > 0 Then
                    'AddHoT_Npc(MapNum, Index, Skillnum, 0)
                End If

            ElseIf Not increment Then
                MapNpc(mapNum).Npc(index).Vital(Vital) = MapNpc(mapNum).Npc(index).Vital(Vital) - Damage
            End If

        End If

    End Sub

    Friend Function IsNpcDead(mapNum As Integer, MapNpcNum As Integer)
        IsNpcDead = False
        If mapNum < 0 OrElse mapNum > MAX_MAPS OrElse MapNpcNum < 0 OrElse MapNpcNum > MAX_MAP_NPCS Then Exit Function
        If MapNpc(mapNum).Npc(MapNpcNum).Vital(VitalType.HP) <= 0 Then IsNpcDead = True
    End Function

    Friend Sub DropNpcItems(mapNum As Integer, MapNpcNum As Integer)
        Dim NpcNum = MapNpc(mapNum).Npc(MapNpcNum).Num
        Dim tmpitem = Random(1, 5)
        Dim n = Int(Rnd() * Npc(NpcNum).DropChance(tmpitem)) + 1

        If n = 1 Then
            SpawnItem(Npc(NpcNum).DropItem(tmpitem), Npc(NpcNum).DropItemValue(tmpitem), mapNum, MapNpc(mapNum).Npc(MapNpcNum).X, MapNpc(mapNum).Npc(MapNpcNum).Y)
        End If
    End Sub

#End Region

#Region "Outgoing Packets"

    Sub SendMapNpcsToMap(mapNum As Integer)
        Dim i As Integer
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SMapNpcData)

        AddDebug("Sent SMSG: SMapNpcData")

        For i = 1 To MAX_MAP_NPCS
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).Num)
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).X)
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).Y)
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).Dir)
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).Vital(VitalType.HP))
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).Vital(VitalType.MP))
        Next

        SendDataToMap(mapNum, buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

#End Region

#Region "Incoming Packets"

    Sub Packet_EditNpc(index As Integer, ByRef data() As Byte)
        AddDebug("Recieved EMSG: RequestEditNpc")

        ' Prevent hacking
        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        Dim Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SNpcEditor)
        Socket.SendDataTo(index, Buffer.Data, Buffer.Head)

        AddDebug("Sent SMSG: SNpcEditor")

        Buffer.Dispose()
    End Sub

    Sub Packet_SaveNPC(index As Integer, ByRef data() As Byte)
        Dim NpcNum As Integer, i As Integer
        Dim buffer As New ByteStream(data)

        AddDebug("Recieved EMSG: SaveNpc")

        ' Prevent hacking
        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        NpcNum = buffer.ReadInt32

        ' Update the Npc
        Npc(NpcNum).Animation = buffer.ReadInt32()
        Npc(NpcNum).AttackSay = buffer.ReadString()
        Npc(NpcNum).Behaviour = buffer.ReadInt32()
        For i = 1 To 5
            Npc(NpcNum).DropChance(i) = buffer.ReadInt32()
            Npc(NpcNum).DropItem(i) = buffer.ReadInt32()
            Npc(NpcNum).DropItemValue(i) = buffer.ReadInt32()
        Next

        Npc(NpcNum).Exp = buffer.ReadInt32()
        Npc(NpcNum).Faction = buffer.ReadInt32()
        Npc(NpcNum).Hp = buffer.ReadInt32()
        Npc(NpcNum).Name = buffer.ReadString()
        Npc(NpcNum).Range = buffer.ReadInt32()
        Npc(NpcNum).SpawnTime = buffer.ReadInt32()
        Npc(NpcNum).SpawnSecs = buffer.ReadInt32()
        Npc(NpcNum).Sprite = buffer.ReadInt32()

        For i = 0 To StatType.Count - 1
            Npc(NpcNum).Stat(i) = buffer.ReadInt32()
        Next

        Npc(NpcNum).QuestNum = buffer.ReadInt32()

        For i = 1 To MAX_NPC_SKILLS
            Npc(NpcNum).Skill(i) = buffer.ReadInt32()
        Next

        Npc(NpcNum).Level = buffer.ReadInt32()
        Npc(NpcNum).Damage = buffer.ReadInt32()

        ' Save it
        SendUpdateNpcToAll(NpcNum)
        SaveNpc(NpcNum)
        Addlog(GetPlayerLogin(index) & " saved Npc #" & NpcNum & ".", ADMIN_LOG)

        buffer.Dispose()
    End Sub

    Sub SendNpcs(index As Integer)
        Dim i As Integer

        For i = 1 To MAX_NPCS
            If Len(Trim$(Npc(i).Name)) > 0 Then
                SendUpdateNpcTo(index, i)
            End If
        Next

    End Sub

    Sub SendUpdateNpcTo(index As Integer, NpcNum As Integer)
        Dim buffer As ByteStream, i As Integer
        buffer = New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SUpdateNpc)

        AddDebug("Sent SMSG: SUpdateNpc")

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

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendUpdateNpcToAll(NpcNum As Integer)
        Dim buffer As ByteStream, i As Integer
        buffer = New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SUpdateNpc)

        AddDebug("Sent SMSG: SUpdateNpc To All")

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

        SendDataToAll(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendMapNpcsTo(index As Integer, mapNum As Integer)
        Dim i As Integer
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SMapNpcData)

        AddDebug("Sent SMSG: SMapNpcData")

        For i = 1 To MAX_MAP_NPCS
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).Num)
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).X)
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).Y)
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).Dir)
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).Vital(VitalType.HP))
            buffer.WriteInt32(MapNpc(mapNum).Npc(i).Vital(VitalType.MP))
        Next

        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Sub SendMapNpcTo(mapNum As Integer, MapNpcNum As Integer)
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SMapNpcUpdate)

        AddDebug("Sent SMSG: SMapNpcUpdate")

        buffer.WriteInt32(MapNpcNum)

        With MapNpc(mapNum).Npc(MapNpcNum)
            buffer.WriteInt32(.Num)
            buffer.WriteInt32(.X)
            buffer.WriteInt32(.Y)
            buffer.WriteInt32(.Dir)
            buffer.WriteInt32(.Vital(VitalType.HP))
            buffer.WriteInt32(.Vital(VitalType.MP))
        End With

        SendDataToMap(mapNum, buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Sub SendMapNpcVitals(mapNum As Integer, MapNpcNum As Byte)
        Dim i As Integer
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SMapNpcVitals)
        buffer.WriteInt32(MapNpcNum)

        AddDebug("Sent SMSG: SMapNpcVitals")

        For i = 1 To VitalType.Count - 1
            buffer.WriteInt32(MapNpc(mapNum).Npc(MapNpcNum).Vital(i))
        Next

        SendDataToMap(mapNum, buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Sub SendNpcAttack(index As Integer, NpcNum As Integer)
        Dim Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SAttack)

        AddDebug("Sent SMSG: SNpcAttack")

        Buffer.WriteInt32(NpcNum)
        SendDataToMap(GetPlayerMap(index), Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Sub SendNpcDead(mapNum As Integer, index As Integer)
        Dim Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SNpcDead)

        AddDebug("Sent SMSG: SNpcDead")

        Buffer.WriteInt32(index)
        SendDataToMap(mapNum, Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

#End Region

End Module