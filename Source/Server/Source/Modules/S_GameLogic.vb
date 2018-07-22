Imports ASFW

Module S_GameLogic
    Function GetTotalMapPlayers(mapNum As Integer) As Integer
        Dim i As Integer, n As Integer
        n = 0

        For i = 1 To GetPlayersOnline()
            If IsPlaying(i) AndAlso GetPlayerMap(i) = mapNum Then
                n = n + 1
            End If
        Next

        GetTotalMapPlayers = n
    End Function

    Friend Function GetPlayersOnline() As Integer
        Dim x As Integer
        x = 0
        For i As Integer = 1 To Socket.HighIndex
            If TempPlayer(i).InGame = True Then
                x = x + 1
            End If
        Next
        GetPlayersOnline = x
    End Function

    Function GetNpcMaxVital(NpcNum As Integer, Vital As VitalType) As Integer
        GetNpcMaxVital = 0

        ' Prevent subscript out of range
        If NpcNum <= 0 OrElse NpcNum > MAX_NPCS Then Exit Function

        Select Case Vital
            Case VitalType.HP
                GetNpcMaxVital = Npc(NpcNum).Hp
            Case VitalType.MP
                GetNpcMaxVital = Npc(NpcNum).Stat(StatType.Intelligence) * 2
            Case VitalType.SP
                GetNpcMaxVital = Npc(NpcNum).Stat(StatType.Spirit) * 2
        End Select

    End Function

    Function FindPlayer(Name As String) As Integer
        Dim i As Integer

        For i = 1 To GetPlayersOnline()
            If IsPlaying(i) Then
                ' Make sure we dont try to check a name thats to small
                If Len(GetPlayerName(i)) >= Len(Trim$(Name)) Then
                    If UCase$(Mid$(GetPlayerName(i), 1, Len(Trim$(Name)))) = UCase$(Trim$(Name)) Then
                        FindPlayer = i
                        Exit Function
                    End If
                End If
            End If
        Next

        FindPlayer = 0
    End Function

    Sub SpawnItem(itemNum As Integer, ItemVal As Integer, mapNum As Integer, x As Integer, y As Integer)
        Dim i As Integer

        ' Check for subscript out of range
        If itemNum < 1 OrElse itemNum > MAX_ITEMS OrElse mapNum <= 0 OrElse mapNum > MAX_CACHED_MAPS Then Exit Sub

        ' Find open map item slot
        i = FindOpenMapItemSlot(mapNum)

        If i = 0 Then Exit Sub

        SpawnItemSlot(i, itemNum, ItemVal, mapNum, x, y)
    End Sub

    Sub SpawnItemSlot(MapItemSlot As Integer, itemNum As Integer, ItemVal As Integer, mapNum As Integer, x As Integer, y As Integer)
        Dim i As Integer
        Dim buffer As New ByteStream(4)

        ' Check for subscript out of range
        If MapItemSlot <= 0 OrElse MapItemSlot > MAX_MAP_ITEMS OrElse itemNum < 0 OrElse itemNum > MAX_ITEMS OrElse mapNum <= 0 OrElse mapNum > MAX_CACHED_MAPS Then Exit Sub

        i = MapItemSlot

        If i <> 0 Then
            If itemNum >= 0 AndAlso itemNum <= MAX_ITEMS Then
                MapItem(mapNum, i).Num = itemNum
                MapItem(mapNum, i).Value = ItemVal
                MapItem(mapNum, i).X = x
                MapItem(mapNum, i).Y = y

                buffer.WriteInt32(ServerPackets.SSpawnItem)
                buffer.WriteInt32(i)
                buffer.WriteInt32(itemNum)
                buffer.WriteInt32(ItemVal)
                buffer.WriteInt32(x)
                buffer.WriteInt32(y)

                Addlog("Sent SMSG: SSpawnItem MapItemSlot", PACKET_LOG)
                Console.WriteLine("Sent SMSG: SSpawnItem MapItemSlot")

                SendDataToMap(mapNum, buffer.Data, buffer.Head)
            End If

        End If

        buffer.Dispose()
    End Sub

    Function FindOpenMapItemSlot(mapNum As Integer) As Integer
        Dim i As Integer
        FindOpenMapItemSlot = 0

        ' Check for subscript out of range
        If mapNum <= 0 OrElse mapNum > MAX_CACHED_MAPS Then Exit Function

        For i = 1 To MAX_MAP_ITEMS
            If MapItem(mapNum, i).Num = 0 Then
                FindOpenMapItemSlot = i
                Exit Function
            End If
        Next

    End Function

    Sub SpawnAllMapsItems()
        Dim i As Integer

        For i = 1 To MAX_CACHED_MAPS
            SpawnMapItems(i)
        Next

    End Sub

    Sub SpawnMapItems(mapNum As Integer)
        Dim x As Integer
        Dim y As Integer

        ' Check for subscript out of range
        If mapNum <= 0 OrElse mapNum > MAX_CACHED_MAPS Then Exit Sub

        ' Spawn what we have
        For x = 0 To Map(mapNum).MaxX
            For y = 0 To Map(mapNum).MaxY
                ' Check if the tile type is an item or a saved tile incase someone drops something
                If (Map(mapNum).Tile(x, y).Type = TileType.Item) Then

                    ' Check to see if its a currency and if they set the value to 0 set it to 1 automatically
                    If Item(Map(mapNum).Tile(x, y).Data1).Type = ItemType.Currency OrElse Item(Map(mapNum).Tile(x, y).Data1).Stackable = 1 Then
                        If Map(mapNum).Tile(x, y).Data2 <= 0 Then
                            SpawnItem(Map(mapNum).Tile(x, y).Data1, 1, mapNum, x, y)
                        Else
                            SpawnItem(Map(mapNum).Tile(x, y).Data1, Map(mapNum).Tile(x, y).Data2, mapNum, x, y)
                        End If
                    Else
                        SpawnItem(Map(mapNum).Tile(x, y).Data1, Map(mapNum).Tile(x, y).Data2, mapNum, x, y)
                    End If
                End If
            Next
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

                Addlog("Recieved SMSG: SSpawnNpc", PACKET_LOG)
                Console.WriteLine("Recieved SMSG: SSpawnNpc")

                For i = 1 To VitalType.Count - 1
                    buffer.WriteInt32(MapNpc(mapNum).Npc(mapNpcNum).Vital(i))
                Next

                SendDataToMap(mapNum, buffer.Data, buffer.Head)
            End If

            SendMapNpcVitals(mapNum, mapNpcNum)
        End If

        buffer.Dispose()
    End Sub

    Friend Function Random(low As Int32, high As Int32) As Integer
        Static randomNumGen As New Random
        Return randomNumGen.Next(low, high + 1)
    End Function

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

    Friend Function CheckGrammar(Word As String, Optional Caps As Byte = 0) As String
        Dim FirstLetter As String

        FirstLetter = LCase$(Left$(Word, 1))

        If FirstLetter = "$" Then
            CheckGrammar = (Mid$(Word, 2, Len(Word) - 1))
            Exit Function
        End If

        If FirstLetter Like "*[aeiou]*" Then
            If Caps Then CheckGrammar = "An " & Word Else CheckGrammar = "an " & Word
        Else
            If Caps Then CheckGrammar = "A " & Word Else CheckGrammar = "a " & Word
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

    Sub SendMapNpcsToMap(mapNum As Integer)
        Dim i As Integer
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SMapNpcData)

        Addlog("Sent SMSG: SMapNpcData", PACKET_LOG)
        Console.WriteLine("Sent SMSG: SMapNpcData")

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
End Module