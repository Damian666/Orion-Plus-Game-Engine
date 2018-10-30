Imports System.IO
Imports ASFW
Imports ASFW.IO
Imports ASFW.IO.FileIO

Module S_Pets
#Region "Declarations"

    Friend Pet() As PetRec

    ' PET constants
    Friend Const PetBehaviourFollow As Byte = 0 'The pet will attack all npcs around
    Friend Const PetBehaviourGoto As Byte = 1 'If attacked, the pet will fight back
    Friend Const PetAttackBehaviourAttackonsight As Byte = 2 'The pet will attack all npcs around
    Friend Const PetAttackBehaviourGuard As Byte = 3 'If attacked, the pet will fight back
    Friend Const PetAttackBehaviourDonothing As Byte = 4 'The pet will not attack even if attacked

    Friend Structure PetRec

        Dim Num As Integer
        Dim Name As String
        Dim Sprite As Integer

        Dim Range As Integer

        Dim Level As Integer

        Dim MaxLevel As Integer
        Dim ExpGain As Integer
        Dim LevelPnts As Integer

        Dim StatType As Byte '1 for set stats, 2 for relation to owner's stats
        Dim LevelingType As Byte '0 for leveling on own, 1 for not leveling

        Dim Stat() As Byte

        Dim Skill() As Integer

        Dim Evolvable As Byte
        Dim EvolveLevel As Integer
        Dim EvolveNum As Integer
    End Structure

    Friend Structure PlayerPetRec

        Dim Num As Integer
        Dim Health As Integer
        Dim Mana As Integer
        Dim Level As Integer
        Dim Stat() As Byte
        Dim Skill() As Integer
        Dim X As Integer
        Dim Y As Integer
        Dim Dir As Integer
        Dim Alive As Byte
        Dim AttackBehaviour As Integer
        Dim AdoptiveStats As Byte
        Dim Points As Integer
        Dim Exp As Integer

    End Structure
#End Region

#Region "Database"
    Sub SavePets()
        Dim i As Integer

        For i = 1 To MAX_PETS
            SavePet(i)
            Application.DoEvents()
        Next

    End Sub

    Sub SavePet(petNum As Integer)
        Dim filename As String, i As Integer

        filename = Application.StartupPath & "\data\pets\pet" & PetNum & ".dat"

        Dim writer As New ByteStream(100)

        writer.WriteInt32(Pet(PetNum).Num)
        writer.WriteString(Trim$(Pet(PetNum).Name))
        writer.WriteInt32(Pet(PetNum).Sprite)
        writer.WriteInt32(Pet(PetNum).Range)
        writer.WriteInt32(Pet(PetNum).Level)
        writer.WriteInt32(Pet(PetNum).MaxLevel)
        writer.WriteInt32(Pet(PetNum).ExpGain)
        writer.WriteInt32(Pet(PetNum).LevelPnts)

        writer.WriteByte(Pet(PetNum).StatType)
        writer.WriteByte(Pet(PetNum).LevelingType)

        For i = 1 To StatType.Count - 1
            writer.WriteByte(Pet(PetNum).Stat(i))
        Next

        For i = 1 To 4
            writer.WriteInt32(Pet(PetNum).Skill(i))
        Next

        writer.WriteByte(Pet(PetNum).Evolvable)
        writer.WriteInt32(Pet(PetNum).EvolveLevel)
        writer.WriteInt32(Pet(PetNum).EvolveNum)

        BinaryFile.Save(filename, writer)

    End Sub

    Sub LoadPets()
        Dim i As Integer

        ClearPets()
        CheckPets()

        For i = 1 To MAX_PETS
            LoadPet(i)
            Application.DoEvents()
        Next
        'SavePets()
    End Sub

    Sub LoadPet(petNum As Integer)
        Dim filename As String, i As Integer

        filename = Application.StartupPath & "\data\pets\pet" & PetNum & ".dat"

        Dim reader As New ByteStream()
        BinaryFile.Load(filename, reader)

        Pet(PetNum).Num = reader.ReadInt32()
        Pet(PetNum).Name = reader.ReadString()
        Pet(PetNum).Sprite = reader.ReadInt32()
        Pet(PetNum).Range = reader.ReadInt32()
        Pet(PetNum).Level = reader.ReadInt32()
        Pet(PetNum).MaxLevel = reader.ReadInt32()
        Pet(PetNum).ExpGain = reader.ReadInt32()
        Pet(PetNum).LevelPnts = reader.ReadInt32()

        Pet(PetNum).StatType = reader.ReadByte()
        Pet(PetNum).LevelingType = reader.ReadByte()

        ReDim Pet(PetNum).Stat(StatType.Count - 1)
        For i = 1 To StatType.Count - 1
            Pet(PetNum).Stat(i) = reader.ReadByte()
        Next

        ReDim Pet(PetNum).Skill(4)
        For i = 1 To 4
            Pet(PetNum).Skill(i) = reader.ReadInt32()
        Next

        Pet(PetNum).Evolvable = reader.ReadByte()
        Pet(PetNum).EvolveLevel = reader.ReadInt32()
        Pet(PetNum).EvolveNum = reader.ReadInt32()

    End Sub

    Sub CheckPets()
        For i = 1 To MAX_PETS
            If Not File.Exists(Application.StartupPath & "\Data\pets\pet" & i & ".dat") Then
                SavePet(i)
            End If
        Next
    End Sub

    Sub ClearPet(petNum As Integer)

        Pet(PetNum).Name = ""

        ReDim Pet(PetNum).Stat(StatType.Count - 1)
        ReDim Pet(PetNum).Skill(4)
    End Sub

    Sub ClearPets()
        Dim i As Integer

        ReDim Pet(MAX_PETS)
        For i = 1 To MAX_PETS
            ClearPet(i)
        Next

    End Sub
#End Region

#Region "Outgoing Packets"
    Sub SendPets(index as integer)
        Dim i As Integer

        For i = 1 To MAX_PETS
            If Len(Trim$(Pet(i).Name)) > 0 Then
                SendUpdatePetTo(Index, i)
            End If
        Next

    End Sub

    Sub SendUpdatePetToAll(petNum As Integer)
        Dim buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SUpdatePet)

        Buffer.WriteInt32(PetNum)

        With Pet(PetNum)
            Buffer.WriteInt32(.Num)
            buffer.WriteString((Trim$(.Name)))
            buffer.WriteInt32(.Sprite)
            buffer.WriteInt32(.Range)
            buffer.WriteInt32(.Level)
            buffer.WriteInt32(.MaxLevel)
            buffer.WriteInt32(.ExpGain)
            buffer.WriteInt32(.LevelPnts)
            buffer.WriteInt32(.StatType)
            buffer.WriteInt32(.LevelingType)

            For i = 1 To StatType.Count - 1
                buffer.WriteInt32(.Stat(i))
            Next

            For i = 1 To 4
                buffer.WriteInt32(.Skill(i))
            Next

            buffer.WriteInt32(.Evolvable)
            buffer.WriteInt32(.EvolveLevel)
            buffer.WriteInt32(.EvolveNum)
        End With

        SendDataToAll(buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub

    Sub SendUpdatePetTo(index As Integer, petNum As Integer)
        Dim buffer = New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SUpdatePet)

        buffer.WriteInt32(petNum)

        With Pet(petNum)
            buffer.WriteInt32(.Num)
            buffer.WriteString((Trim$(.Name)))
            buffer.WriteInt32(.Sprite)
            Buffer.WriteInt32(.Range)
            Buffer.WriteInt32(.Level)
            Buffer.WriteInt32(.MaxLevel)
            Buffer.WriteInt32(.ExpGain)
            Buffer.WriteInt32(.LevelPnts)
            Buffer.WriteInt32(.StatType)
            Buffer.WriteInt32(.LevelingType)

            For i = 1 To StatType.Count - 1
                Buffer.WriteInt32(.Stat(i))
            Next

            For i = 1 To 4
                Buffer.WriteInt32(.Skill(i))
            Next

            Buffer.WriteInt32(.Evolvable)
            Buffer.WriteInt32(.EvolveLevel)
            Buffer.WriteInt32(.EvolveNum)
        End With

        Socket.SendDataTo(Index, Buffer.Data, Buffer.Head)

        Buffer.Dispose()

    End Sub

    Friend Sub SendUpdatePlayerPet(index as integer, ownerOnly As Boolean)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ServerPackets.SUpdatePlayerPet)

        Buffer.WriteInt32(Index)

        Buffer.WriteInt32(GetPetNum(Index))
        Buffer.WriteInt32(GetPetVital(Index, VitalType.HP))
        Buffer.WriteInt32(GetPetVital(Index, VitalType.MP))
        Buffer.WriteInt32(GetPetLevel(Index))

        For i = 1 To StatType.Count - 1
            Buffer.WriteInt32(GetPetStat(Index, i))
        Next

        For i = 1 To 4
            Buffer.WriteInt32(Player(Index).Character(TempPlayer(Index).CurChar).Pet.Skill(i))
        Next

        Buffer.WriteInt32(GetPetX(Index))
        Buffer.WriteInt32(GetPetY(Index))
        Buffer.WriteInt32(GetPetDir(Index))

        Buffer.WriteInt32(GetPetMaxVital(Index, VitalType.HP))
        Buffer.WriteInt32(GetPetMaxVital(Index, VitalType.MP))

        Buffer.WriteInt32(Player(Index).Character(TempPlayer(Index).CurChar).Pet.Alive)

        Buffer.WriteInt32(GetPetBehaviour(Index))
        Buffer.WriteInt32(GetPetPoints(Index))
        Buffer.WriteInt32(GetPetExp(Index))
        Buffer.WriteInt32(GetPetNextLevel(Index))

        If OwnerOnly Then
            Socket.SendDataTo(Index, Buffer.Data, Buffer.Head)
        Else
            SendDataToMap(GetPlayerMap(Index), Buffer.Data, Buffer.Head)
        End If

        Buffer.Dispose()
    End Sub

    Sub SendPetAttack(index as integer, mapNum as Integer)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ServerPackets.SPetAttack)
        Buffer.WriteInt32(Index)
        SendDataToMap(MapNum, Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Sub SendPetXy(index as integer, x As Integer, y As Integer)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ServerPackets.SPetXY)
        Buffer.WriteInt32(Index)
        Buffer.WriteInt32(X)
        Buffer.WriteInt32(Y)
        SendDataToMap(GetPlayerMap(Index), Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Sub SendPetExp(index as integer)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ServerPackets.SPetExp)
        Buffer.WriteInt32(GetPetExp(Index))
        Buffer.WriteInt32(GetPetNextLevel(Index))
        Socket.SendDataTo(Index, Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

#End Region

#Region "Incoming Packets"

    Sub Packet_RequestEditPet(index as integer, ByRef data() As Byte)
        If GetPlayerAccess(Index) < AdminType.Developer Then Exit Sub

        Dim buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SPetEditor)
        Socket.SendDataTo(Index, Buffer.Data, Buffer.Head)

        Buffer.Dispose()

    End Sub

    Sub Packet_SavePet(index as integer, ByRef data() As Byte)
        Dim petNum As Integer
        Dim i As Integer

        ' Prevent hacking
        If GetPlayerAccess(Index) < AdminType.Developer Then Exit Sub

        dim buffer as New ByteStream(data)
        petNum = Buffer.ReadInt32

        ' Prevent hacking
        If petNum < 0 OrElse petNum > MAX_PETS Then Exit Sub

        With Pet(petNum)
            .Num = Buffer.ReadInt32
            .Name = Buffer.ReadString
            .Sprite = Buffer.ReadInt32
            .Range = Buffer.ReadInt32
            .Level = Buffer.ReadInt32
            .MaxLevel = Buffer.ReadInt32
            .ExpGain = Buffer.ReadInt32
            .LevelPnts = Buffer.ReadInt32
            .StatType = Buffer.ReadInt32
            .LevelingType = Buffer.ReadInt32

            For i = 1 To StatType.Count - 1
                .Stat(i) = Buffer.ReadInt32
            Next

            For i = 1 To 4
                .Skill(i) = Buffer.ReadInt32
            Next

            .Evolvable = Buffer.ReadInt32
            .EvolveLevel = Buffer.ReadInt32
            .EvolveNum = Buffer.ReadInt32
        End With

        ' Save it
        SendUpdatePetToAll(petNum)
        SavePet(petNum)
        Addlog(GetPlayerLogin(Index) & " saved Pet #" & petNum & ".", ADMIN_LOG)
        SendPets(Index)
    End Sub

    Sub Packet_RequestPets(index as integer, ByRef data() As Byte)

        SendPets(Index)

    End Sub

    Sub Packet_SummonPet(index as integer, ByRef data() As Byte)
        If PetAlive(Index) Then
            ReCallPet(Index)
        Else
            SummonPet(Index)
        End If
    End Sub

    Sub Packet_PetMove(index as integer, ByRef data() As Byte)
        Dim x As Integer, y As Integer, i As Integer
        dim buffer as New ByteStream(data)
        x = Buffer.ReadInt32
        y = Buffer.ReadInt32

        ' Prevent subscript out of range
        If x < 0 OrElse x > Map(GetPlayerMap(Index)).MaxX OrElse y < 0 OrElse y > Map(GetPlayerMap(Index)).MaxY Then Exit Sub

        ' Check for a player
        For i = 1 To GetPlayersOnline()

            If IsPlaying(i) Then
                If GetPlayerMap(Index) = GetPlayerMap(i) AndAlso GetPlayerX(i) = x AndAlso GetPlayerY(i) = y Then
                    If i = Index Then
                        ' Change target
                        If TempPlayer(Index).PetTargetType = TargetType.Player AndAlso TempPlayer(Index).PetTarget = i Then
                            TempPlayer(Index).PetTarget = 0
                            TempPlayer(Index).PetTargetType = TargetType.None
                            TempPlayer(Index).PetBehavior = PetBehaviourGoto
                            TempPlayer(Index).GoToX = x
                            TempPlayer(Index).GoToY = y
                            ' send target to player
                            PlayerMsg(Index, "Your pet is no longer following you.", ColorType.BrightGreen)
                        Else
                            TempPlayer(Index).PetTarget = i
                            TempPlayer(Index).PetTargetType = TargetType.Player
                            ' send target to player
                            TempPlayer(Index).PetBehavior = PetBehaviourFollow
                            PlayerMsg(Index, "Your " & Trim$(GetPetName(Index)) & " is now following you.", ColorType.BrightGreen)
                        End If
                    Else
                        ' Change target
                        If TempPlayer(Index).PetTargetType = TargetType.Player AndAlso TempPlayer(Index).PetTarget = i Then
                            TempPlayer(Index).PetTarget = 0
                            TempPlayer(Index).PetTargetType = TargetType.None
                            ' send target to player
                            PlayerMsg(Index, "Your pet is no longer targetting " & Trim$(GetPlayerName(i)) & ".", ColorType.BrightGreen)
                        Else
                            TempPlayer(Index).PetTarget = i
                            TempPlayer(Index).PetTargetType = TargetType.Player
                            ' send target to player
                            PlayerMsg(Index, "Your pet is now targetting " & Trim$(GetPlayerName(i)) & ".", ColorType.BrightGreen)
                        End If
                    End If
                    Exit Sub
                End If
            End If

            If PetAlive(i) AndAlso i <> Index Then
                If GetPetX(i) = x AndAlso GetPetY(i) = y Then
                    ' Change target
                    If TempPlayer(Index).PetTargetType = TargetType.Pet AndAlso TempPlayer(Index).PetTarget = i Then
                        TempPlayer(Index).PetTarget = 0
                        TempPlayer(Index).PetTargetType = TargetType.None
                        ' send target to player
                        PlayerMsg(Index, "Your pet is no longer targetting " & Trim$(GetPlayerName(i)) & "'s " & Trim$(GetPetName(i)) & ".", ColorType.BrightGreen)
                    Else
                        TempPlayer(Index).PetTarget = i
                        TempPlayer(Index).PetTargetType = TargetType.Pet
                        ' send target to player
                        PlayerMsg(Index, "Your pet is now targetting " & Trim$(GetPlayerName(i)) & "'s " & Trim$(GetPetName(i)) & ".", ColorType.BrightGreen)
                    End If
                    Exit Sub
                End If
            End If
        Next

        'Search For Target First
        ' Check for an npc
        For i = 1 To MAX_MAP_NPCS
            If MapNpc(GetPlayerMap(Index)).Npc(i).Num > 0 AndAlso MapNpc(GetPlayerMap(Index)).Npc(i).X = x AndAlso MapNpc(GetPlayerMap(Index)).Npc(i).Y = y Then
                If TempPlayer(Index).PetTarget = i AndAlso TempPlayer(Index).PetTargetType = TargetType.Npc Then
                    ' Change target
                    TempPlayer(Index).PetTarget = 0
                    TempPlayer(Index).PetTargetType = TargetType.None
                    ' send target to player
                    PlayerMsg(Index, "Your " & Trim$(GetPetName(Index)) & "'s target is no longer a " & Trim$(Npc(MapNpc(GetPlayerMap(Index)).Npc(i).Num).Name) & "!", ColorType.BrightGreen)
                    Exit Sub
                Else
                    ' Change target
                    TempPlayer(Index).PetTarget = i
                    TempPlayer(Index).PetTargetType = TargetType.Npc
                    ' send target to player
                    PlayerMsg(Index, "Your " & Trim$(GetPetName(Index)) & "'s target is now a " & Trim$(Npc(MapNpc(GetPlayerMap(Index)).Npc(i).Num).Name) & "!", ColorType.BrightGreen)
                    Exit Sub
                End If
            End If
        Next

        TempPlayer(Index).PetBehavior = PetBehaviourGoto
        TempPlayer(Index).PetTargetType = 0
        TempPlayer(Index).PetTarget = 0
        TempPlayer(Index).GoToX = x
        TempPlayer(Index).GoToY = y

        Buffer.Dispose()

    End Sub

    Sub Packet_SetPetBehaviour(index as integer, ByRef data() As Byte)
        Dim behaviour As Integer
        dim buffer as New ByteStream(data)
        behaviour = Buffer.ReadInt32

        If PetAlive(Index) Then
            Select Case behaviour
                Case PetAttackBehaviourAttackonsight
                    SetPetBehaviour(Index, PetAttackBehaviourAttackonsight)
                    SendActionMsg(GetPlayerMap(Index), "Agressive Mode!", ColorType.White, 0, GetPetX(Index) * 32, GetPetY(Index) * 32, Index)
                Case PetAttackBehaviourGuard
                    SetPetBehaviour(Index, PetAttackBehaviourGuard)
                    SendActionMsg(GetPlayerMap(Index), "Defensive Mode!", ColorType.White, 0, GetPetX(Index) * 32, GetPetY(Index) * 32, Index)
            End Select
        End If

        Buffer.Dispose()

    End Sub

    Sub Packet_ReleasePet(index as integer, ByRef data() As Byte)
        If GetPetNum(Index) > 0 Then ReleasePet(Index)
    End Sub

    Sub Packet_PetSkill(index as integer, ByRef data() As Byte)
        Dim n As Integer
        dim buffer as New ByteStream(data)
        ' Skill slot
        n = Buffer.ReadInt32

        Buffer.Dispose()

        ' set the skill buffer before castin
        BufferPetSkill(Index, n)

    End Sub

    Sub Packet_UsePetStatPoint(index as integer, ByRef data() As Byte)
        Dim pointType As Byte
        Dim sMes As String = ""
        dim buffer as New ByteStream(data)
        PointType = Buffer.ReadInt32
        Buffer.Dispose()

        ' Prevent hacking
        If (PointType < 0) OrElse (PointType > StatType.Count) Then Exit Sub

        If Not PetAlive(Index) Then Exit Sub

        ' Make sure they have points
        If GetPetPoints(Index) > 0 Then

            ' make sure they're not maxed#
            If GetPetStat(Index, PointType) >= 255 Then
                PlayerMsg(Index, "You cannot spend any more points on that stat for your pet.", ColorType.BrightRed)
                Exit Sub
            End If

            SetPetPoints(Index, GetPetPoints(Index) - 1)

            ' Everything is ok
            Select Case PointType
                Case StatType.Strength
                    SetPetStat(Index, PointType, GetPetStat(Index, PointType) + 1)
                    sMes = "Strength"
                Case StatType.Endurance
                    SetPetStat(Index, PointType, GetPetStat(Index, PointType) + 1)
                    sMes = "Endurance"
                Case StatType.Intelligence
                    SetPetStat(Index, PointType, GetPetStat(Index, PointType) + 1)
                    sMes = "Intelligence"
                Case StatType.Luck
                    SetPetStat(Index, PointType, GetPetStat(Index, PointType) + 1)
                    sMes = "Agility"
                Case StatType.Spirit
                    SetPetStat(Index, PointType, GetPetStat(Index, PointType) + 1)
                    sMes = "Willpower"
            End Select

            SendActionMsg(GetPlayerMap(Index), "+1 " & sMes, ColorType.White, 1, (GetPetX(Index) * 32), (GetPetY(Index) * 32))
        Else
            Exit Sub
        End If

        ' Send the update
        SendUpdatePlayerPet(Index, True)

    End Sub

#End Region

#Region "Pet Functions"

    Friend Sub UpdatePetAi()
        Dim didWalk As Boolean, givePetHpTimer As Integer, playerindex as integer
        Dim mapNum as Integer, tickCount As Integer, i As Integer, n As Integer
        Dim distanceX As Integer, distanceY As Integer, tmpdir As Integer
        Dim target As Integer, targetTypes As Byte, targetX As Integer, targetY As Integer, targetVerify As Boolean

        For MapNum = 1 To MAX_CACHED_MAPS
            For PlayerIndex = 1 To GetPlayersOnline()
                TickCount = GetTimeMs()

                If GetPlayerMap(PlayerIndex) = MapNum AndAlso PetAlive(PlayerIndex) Then
                    ' // This is used for ATTACKING ON SIGHT //

                    ' If the npc is a attack on sight, search for a player on the map
                    If GetPetBehaviour(PlayerIndex) <> PetAttackBehaviourDonothing Then

                        ' make sure it's not stunned
                        If Not TempPlayer(PlayerIndex).PetStunDuration > 0 Then

                            For i = 1 To Socket.HighIndex
                                If TempPlayer(PlayerIndex).PetTargetType > 0 Then
                                    If TempPlayer(PlayerIndex).PetTargetType = 1 AndAlso TempPlayer(PlayerIndex).PetTarget = PlayerIndex Then
                                    Else
                                        Exit For
                                    End If
                                End If

                                If IsPlaying(i) AndAlso i <> PlayerIndex Then
                                    If GetPlayerMap(i) = MapNum AndAlso GetPlayerAccess(i) <= AdminType.Monitor Then
                                        If PetAlive(i) Then
                                            n = GetPetRange(PlayerIndex)
                                            DistanceX = GetPetX(PlayerIndex) - GetPetX(i)
                                            DistanceY = GetPetY(PlayerIndex) - GetPetY(i)

                                            ' Make sure we get a positive value
                                            If DistanceX < 0 Then DistanceX = DistanceX * -1
                                            If DistanceY < 0 Then DistanceY = DistanceY * -1

                                            ' Are they in range?  if so GET'M!
                                            If DistanceX <= n AndAlso DistanceY <= n Then
                                                If GetPetBehaviour(PlayerIndex) = PetAttackBehaviourAttackonsight Then
                                                    TempPlayer(PlayerIndex).PetTargetType = TargetType.Pet ' pet
                                                    TempPlayer(PlayerIndex).PetTarget = i
                                                End If
                                            End If
                                        Else
                                            n = GetPetRange(PlayerIndex)
                                            DistanceX = GetPetX(PlayerIndex) - GetPlayerX(i)
                                            DistanceY = GetPetY(PlayerIndex) - GetPlayerY(i)

                                            ' Make sure we get a positive value
                                            If DistanceX < 0 Then DistanceX = DistanceX * -1
                                            If DistanceY < 0 Then DistanceY = DistanceY * -1

                                            ' Are they in range?  if so GET'M!
                                            If DistanceX <= n AndAlso DistanceY <= n Then
                                                If GetPetBehaviour(PlayerIndex) = PetAttackBehaviourAttackonsight Then
                                                    TempPlayer(PlayerIndex).PetTargetType = TargetType.Player ' player
                                                    TempPlayer(PlayerIndex).PetTarget = i
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Next

                            If TempPlayer(PlayerIndex).PetTargetType = 0 Then
                                For i = 1 To MAX_MAP_NPCS

                                    If TempPlayer(PlayerIndex).PetTargetType > 0 Then Exit For
                                    If PetAlive(PlayerIndex) Then
                                        n = GetPetRange(PlayerIndex)
                                        DistanceX = GetPetX(PlayerIndex) - MapNpc(GetPlayerMap(PlayerIndex)).Npc(i).X
                                        DistanceY = GetPetY(PlayerIndex) - MapNpc(GetPlayerMap(PlayerIndex)).Npc(i).Y

                                        ' Make sure we get a positive value
                                        If DistanceX < 0 Then DistanceX = DistanceX * -1
                                        If DistanceY < 0 Then DistanceY = DistanceY * -1

                                        ' Are they in range?  if so GET'M!
                                        If DistanceX <= n AndAlso DistanceY <= n Then
                                            If GetPetBehaviour(PlayerIndex) = PetAttackBehaviourAttackonsight Then
                                                TempPlayer(PlayerIndex).PetTargetType = TargetType.Npc ' npc
                                                TempPlayer(PlayerIndex).PetTarget = i
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If

                        targetVerify = False

                        ' // This is used for Pet walking/targetting //

                        ' Make sure theres a npc with the map
                        If TempPlayer(PlayerIndex).PetStunDuration > 0 Then
                            ' check if we can unstun them
                            If GetTimeMs() > TempPlayer(PlayerIndex).PetStunTimer + (TempPlayer(PlayerIndex).PetStunDuration * 1000) Then
                                TempPlayer(PlayerIndex).PetStunDuration = 0
                                TempPlayer(PlayerIndex).PetStunTimer = 0
                            End If
                        Else
                            Target = TempPlayer(PlayerIndex).PetTarget
                            TargetTypes = TempPlayer(PlayerIndex).PetTargetType

                            ' Check to see if its time for the npc to walk
                            If GetPetBehaviour(PlayerIndex) <> PetAttackBehaviourDonothing Then

                                If TargetTypes = TargetType.Player Then ' player
                                    ' Check to see if we are following a player or not
                                    If Target > 0 Then

                                        ' Check if the player is even playing, if so follow'm
                                        If IsPlaying(Target) AndAlso GetPlayerMap(Target) = MapNum Then
                                            If Target <> PlayerIndex Then
                                                DidWalk = False
                                                targetVerify = True
                                                TargetY = GetPlayerY(Target)
                                                TargetX = GetPlayerX(Target)
                                            End If
                                        Else
                                            TempPlayer(PlayerIndex).PetTargetType = TargetType.None ' clear
                                            TempPlayer(PlayerIndex).PetTarget = 0
                                        End If
                                    End If
                                ElseIf TargetTypes = TargetType.Npc Then 'npc
                                    If Target > 0 Then
                                        If MapNpc(MapNum).Npc(Target).Num > 0 Then
                                            DidWalk = False
                                            targetVerify = True
                                            TargetY = MapNpc(MapNum).Npc(Target).Y
                                            TargetX = MapNpc(MapNum).Npc(Target).X
                                        Else
                                            TempPlayer(PlayerIndex).PetTargetType = TargetType.None ' clear
                                            TempPlayer(PlayerIndex).PetTarget = 0
                                        End If
                                    End If
                                ElseIf TargetTypes = TargetType.Pet Then 'other pet
                                    If Target > 0 Then
                                        If IsPlaying(Target) AndAlso GetPlayerMap(Target) = MapNum AndAlso PetAlive(Target) Then
                                            DidWalk = False
                                            targetVerify = True
                                            TargetY = GetPetY(Target)
                                            TargetX = GetPetX(Target)
                                        Else
                                            TempPlayer(PlayerIndex).PetTargetType = TargetType.None ' clear
                                            TempPlayer(PlayerIndex).PetTarget = 0
                                        End If
                                    End If
                                End If
                            End If

                            If targetVerify Then
                                DidWalk = False

                                If IsOneBlockAway(GetPetX(PlayerIndex), GetPetY(PlayerIndex), TargetX, TargetY) Then
                                    If GetPetX(PlayerIndex) < TargetX Then
                                        PetDir(PlayerIndex, DirectionType.Right)
                                        DidWalk = True
                                    ElseIf GetPetX(PlayerIndex) > TargetX Then
                                        PetDir(PlayerIndex, DirectionType.Left)
                                        DidWalk = True
                                    ElseIf GetPetY(PlayerIndex) < TargetY Then
                                        PetDir(PlayerIndex, DirectionType.Up)
                                        DidWalk = True
                                    ElseIf GetPetY(PlayerIndex) > TargetY Then
                                        PetDir(PlayerIndex, DirectionType.Down)
                                        DidWalk = True
                                    End If

                                Else
                                    DidWalk = PetTryWalk(PlayerIndex, TargetX, TargetY)
                                End If

                            ElseIf TempPlayer(PlayerIndex).PetBehavior = PetBehaviourGoto AndAlso targetVerify = False Then

                                If GetPetX(PlayerIndex) = TempPlayer(PlayerIndex).GoToX AndAlso GetPetY(PlayerIndex) = TempPlayer(PlayerIndex).GoToY Then
                                    'Unblock these for the random turning
                                    'i = Int(Rnd * 4)
                                    'Call PetDir(x, i)
                                Else
                                    DidWalk = False
                                    TargetX = TempPlayer(PlayerIndex).GoToX
                                    TargetY = TempPlayer(PlayerIndex).GoToY
                                    DidWalk = PetTryWalk(PlayerIndex, TargetX, TargetY)

                                    If DidWalk = False Then
                                        tmpdir = Int(Rnd() * 4)

                                        If tmpdir = 1 Then
                                            tmpdir = Int(Rnd() * 4)
                                            If CanPetMove(PlayerIndex, MapNum, tmpdir) Then
                                                PetMove(PlayerIndex, MapNum, tmpdir, MovementType.Walking)
                                            End If
                                        End If
                                    End If
                                End If

                            ElseIf TempPlayer(PlayerIndex).PetBehavior = PetBehaviourFollow Then

                                If IsPetByPlayer(PlayerIndex) Then
                                    'Unblock these to enable random turning
                                    'i = Int(Rnd * 4)
                                    'Call PetDir(x, i)
                                Else
                                    DidWalk = False
                                    TargetX = GetPlayerX(PlayerIndex)
                                    TargetY = GetPlayerY(PlayerIndex)
                                    DidWalk = PetTryWalk(PlayerIndex, TargetX, TargetY)

                                    If DidWalk = False Then
                                        tmpdir = Int(Rnd() * 4)
                                        If tmpdir = 1 Then
                                            tmpdir = Int(Rnd() * 4)
                                            If CanPetMove(PlayerIndex, MapNum, tmpdir) Then
                                                PetMove(PlayerIndex, MapNum, tmpdir, MovementType.Walking)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        ' // This is used for pets to attack targets //

                        ' Make sure theres a npc with the map
                        Target = TempPlayer(PlayerIndex).PetTarget
                        TargetTypes = TempPlayer(PlayerIndex).PetTargetType

                        ' Check if the pet can attack the targeted player
                        If Target > 0 Then
                            If TargetTypes = TargetType.Player Then ' player
                                ' Is the target playing and on the same map?
                                If IsPlaying(Target) AndAlso GetPlayerMap(Target) = MapNum Then
                                    If PlayerIndex <> Target Then TryPetAttackPlayer(PlayerIndex, Target)
                                Else
                                    ' Player left map or game, set target to 0
                                    TempPlayer(PlayerIndex).PetTarget = 0
                                    TempPlayer(PlayerIndex).PetTargetType = TargetType.None ' clear

                                End If
                            ElseIf TargetTypes = TargetType.Npc Then 'npc
                                If MapNpc(GetPlayerMap(PlayerIndex)).Npc(TempPlayer(PlayerIndex).PetTarget).Num > 0 Then
                                    TryPetAttackNpc(PlayerIndex, TempPlayer(PlayerIndex).PetTarget)
                                Else
                                    ' Player left map or game, set target to 0
                                    TempPlayer(PlayerIndex).PetTarget = 0
                                    TempPlayer(PlayerIndex).PetTargetType = TargetType.None ' clear
                                End If
                            ElseIf TargetTypes = TargetType.Pet Then 'pet
                                ' Is the target playing and on the same map? AndAlso is pet alive??
                                If IsPlaying(Target) AndAlso GetPlayerMap(Target) = MapNum AndAlso PetAlive(Target) Then
                                    TryPetAttackPet(PlayerIndex, Target)
                                Else
                                    ' Player left map or game, set target to 0
                                    TempPlayer(PlayerIndex).PetTarget = 0
                                    TempPlayer(PlayerIndex).PetTargetType = TargetType.None ' clear
                                End If
                            End If
                        End If

                        ' ////////////////////////////////////////////
                        ' // This is used for regenerating Pet's HP //
                        ' ////////////////////////////////////////////
                        ' Check to see if we want to regen some of the npc's hp
                        If Not TempPlayer(PlayerIndex).PetstopRegen Then
                            If PetAlive(PlayerIndex) AndAlso TickCount > GivePetHPTimer + 10000 Then
                                If GetPetVital(PlayerIndex, VitalType.HP) > 0 Then
                                    SetPetVital(PlayerIndex, VitalType.HP, GetPetVital(PlayerIndex, VitalType.HP) + GetPetVitalRegen(PlayerIndex, VitalType.HP))
                                    SetPetVital(PlayerIndex, VitalType.MP, GetPetVital(PlayerIndex, VitalType.MP) + GetPetVitalRegen(PlayerIndex, VitalType.MP))

                                    ' Check if they have more then they should and if so just set it to max
                                    If GetPetVital(PlayerIndex, VitalType.HP) > GetPetMaxVital(PlayerIndex, VitalType.HP) Then
                                        SetPetVital(PlayerIndex, VitalType.HP, GetPetMaxVital(PlayerIndex, VitalType.HP))
                                    End If

                                    If GetPetVital(PlayerIndex, VitalType.MP) > GetPetMaxVital(PlayerIndex, VitalType.MP) Then
                                        SetPetVital(PlayerIndex, VitalType.MP, GetPetMaxVital(PlayerIndex, VitalType.MP))
                                    End If

                                    SendPetVital(PlayerIndex, VitalType.HP)
                                    SendPetVital(PlayerIndex, VitalType.MP)
                                End If
                            End If
                        End If
                    End If
                End If
                Application.DoEvents()
            Next
            Application.DoEvents()
        Next

        ' Make sure we reset the timer for npc hp regeneration
        If GetTimeMs() > GivePetHPTimer + 10000 Then
            GivePetHPTimer = GetTimeMs()
        End If
    End Sub

    Sub SummonPet(index as integer)
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Alive = 1
        PlayerMsg(Index, "You summoned your " & Trim$(GetPetName(Index)) & "!", ColorType.BrightGreen)
        SendUpdatePlayerPet(Index, False)
    End Sub

    Sub ReCallPet(index as integer)
        PlayerMsg(Index, "You recalled your " & Trim$(GetPetName(Index)) & "!", ColorType.BrightGreen)
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Alive = 0
        SendUpdatePlayerPet(Index, False)
    End Sub

    Sub ReleasePet(index as integer)
        Dim i As Integer

        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Alive = 0
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Num = 0
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.AttackBehaviour = 0
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Dir = 0
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Health = 0
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level = 0
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Mana = 0
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.X = 0
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Y = 0

        TempPlayer(Index).PetTarget = 0
        TempPlayer(Index).PetTargetType = 0
        TempPlayer(Index).GoToX = -1
        TempPlayer(Index).GoToY = -1

        For i = 1 To 4
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Skill(i) = 0
        Next

        For i = 1 To StatType.Count - 1
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Stat(i) = 0
        Next

        SendUpdatePlayerPet(Index, False)

        SavePlayer(Index)

        PlayerMsg(Index, "You released your pet!", ColorType.BrightGreen)

        For i = 1 To MAX_MAP_NPCS
            If MapNpc(GetPlayerMap(Index)).Npc(i).Vital(VitalType.HP) > 0 Then
                If MapNpc(GetPlayerMap(Index)).Npc(i).TargetType = TargetType.Pet Then
                    If MapNpc(GetPlayerMap(Index)).Npc(i).Target = Index Then
                        MapNpc(GetPlayerMap(Index)).Npc(i).TargetType = TargetType.Player
                        MapNpc(GetPlayerMap(Index)).Npc(i).Target = Index
                    End If
                End If
            End If
        Next

    End Sub

    Sub AdoptPet(index as integer, petNum As Integer)

        If GetPetNum(Index) = 0 Then
            PlayerMsg(Index, "You have adopted a " & Trim$(Pet(PetNum).Name), ColorType.BrightGreen)
        Else
            PlayerMsg(Index, "You allready have a " & Trim$(Pet(PetNum).Name) & ", release your old pet first!", ColorType.BrightGreen)
            Exit Sub
        End If

        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Num = PetNum

        For i = 1 To 4
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Skill(i) = Pet(PetNum).Skill(i)
        Next

        If Pet(PetNum).StatType = 0 Then
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Health = GetPlayerMaxVital(Index, VitalType.HP)
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Mana = GetPlayerMaxVital(Index, VitalType.MP)
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level = GetPlayerLevel(Index)

            For i = 1 To StatType.Count - 1
                Player(Index).Character(TempPlayer(Index).CurChar).Pet.Stat(i) = Player(Index).Character(TempPlayer(Index).CurChar).Stat(i)
            Next

            Player(Index).Character(TempPlayer(Index).CurChar).Pet.AdoptiveStats = 1
        Else
            For i = 1 To StatType.Count - 1
                Player(Index).Character(TempPlayer(Index).CurChar).Pet.Stat(i) = Pet(PetNum).Stat(i)
            Next

            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level = Pet(PetNum).Level
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.AdoptiveStats = 0
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Health = GetPetMaxVital(Index, VitalType.HP)
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Mana = GetPetMaxVital(Index, VitalType.MP)
        End If

        Player(Index).Character(TempPlayer(Index).CurChar).Pet.X = GetPlayerX(Index)
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Y = GetPlayerY(Index)

        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Alive = 1
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Points = 0
        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Exp = 0

        Player(Index).Character(TempPlayer(Index).CurChar).Pet.AttackBehaviour = PetAttackBehaviourGuard 'By default it will guard but this can be changed

        SavePlayer(Index)

        SendUpdatePlayerPet(Index, False)

    End Sub

    Sub PetMove(index as integer, mapNum as Integer, dir As Integer, movement As Integer)
        dim buffer as ByteStream

        If MapNum < 1 OrElse MapNum > MAX_MAPS OrElse Index <= 0 OrElse Index > MAX_PLAYERS OrElse Dir < DirectionType.Up OrElse Dir > DirectionType.Right OrElse movement < 1 OrElse movement > 2 Then
            Exit Sub
        End If

        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Dir = Dir

        Select Case Dir
            Case DirectionType.Up
                SetPetY(Index, GetPetY(Index) - 1)

            Case DirectionType.Down
                SetPetY(Index, GetPetY(Index) + 1)

            Case DirectionType.Left
                SetPetX(Index, GetPetX(Index) - 1)

            Case DirectionType.Right
                SetPetX(Index, GetPetX(Index) + 1)
        End Select

        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SPetMove)
        Buffer.WriteInt32(Index)
        Buffer.WriteInt32(GetPetX(Index))
        Buffer.WriteInt32(GetPetY(Index))
        Buffer.WriteInt32(GetPetDir(Index))
        Buffer.WriteInt32(movement)
        SendDataToMap(MapNum, Buffer.Data, Buffer.Head)
        Buffer.Dispose()

    End Sub

    Function CanPetMove(index as integer, mapNum as Integer, dir As Byte) As Boolean
        Dim i As Integer, n As Integer
        Dim x As Integer, y As Integer

        If MapNum < 1 OrElse MapNum > MAX_MAPS OrElse Index <= 0 OrElse Index > MAX_PLAYERS OrElse Dir < DirectionType.Up OrElse Dir > DirectionType.Right Then
            Exit Function
        End If

        If Index <= 0 OrElse Index > MAX_PLAYERS Then Exit Function

        x = GetPetX(Index)
        y = GetPetY(Index)

        If x < 0 OrElse x > Map(MapNum).MaxX Then Exit Function
        If y < 0 OrElse y > Map(MapNum).MaxY Then Exit Function

        CanPetMove = True

        If TempPlayer(Index).PetskillBuffer.Skill > 0 Then
            CanPetMove = False
            Exit Function
        End If

        Select Case Dir

            Case DirectionType.Up
                ' Check to make sure not outside of boundries
                If y > 0 Then
                    n = Map(MapNum).Tile(x, y - 1).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.NpcSpawn Then
                        CanPetMove = False
                        Exit Function
                    End If

                    ' Check to make sure that there is not a player in the way
                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = MapNum) AndAlso (GetPlayerX(i) = GetPetX(Index) + 1) AndAlso (GetPlayerY(i) = GetPetY(Index) - 1) Then
                                CanPetMove = False
                                Exit Function
                            ElseIf PetAlive(i) AndAlso (GetPlayerMap(i) = MapNum) AndAlso (GetPetX(i) = GetPetX(Index)) AndAlso (GetPetY(i) = GetPetY(Index) - 1) Then
                                CanPetMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (MapNpc(MapNum).Npc(i).Num > 0) AndAlso (MapNpc(MapNum).Npc(i).X = GetPetX(Index)) AndAlso (MapNpc(MapNum).Npc(i).Y = GetPetY(Index) - 1) Then
                            CanPetMove = False
                            Exit Function
                        End If
                    Next

                    ' Directional blocking
                    If IsDirBlocked(Map(MapNum).Tile(GetPetX(Index), GetPetY(Index)).DirBlock, DirectionType.Up + 1) Then
                        CanPetMove = False
                        Exit Function
                    End If
                Else
                    CanPetMove = False
                End If

            Case DirectionType.Down

                ' Check to make sure not outside of boundries
                If y < Map(MapNum).MaxY Then
                    n = Map(MapNum).Tile(x, y + 1).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.NpcSpawn Then
                        CanPetMove = False
                        Exit Function
                    End If

                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = MapNum) AndAlso (GetPlayerX(i) = GetPetX(Index)) AndAlso (GetPlayerY(i) = GetPetY(Index) + 1) Then
                                CanPetMove = False
                                Exit Function
                            ElseIf PetAlive(i) AndAlso (GetPlayerMap(i) = MapNum) AndAlso (GetPetX(i) = GetPetX(Index)) AndAlso (GetPetY(i) = GetPetY(Index) + 1) Then
                                CanPetMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (MapNpc(MapNum).Npc(i).Num > 0) AndAlso (MapNpc(MapNum).Npc(i).X = GetPetX(Index)) AndAlso (MapNpc(MapNum).Npc(i).Y = GetPetY(Index) + 1) Then
                            CanPetMove = False
                            Exit Function
                        End If
                    Next

                    ' Directional blocking
                    If IsDirBlocked(Map(MapNum).Tile(GetPetX(Index), GetPetY(Index)).DirBlock, DirectionType.Down + 1) Then
                        CanPetMove = False
                        Exit Function
                    End If
                Else
                    CanPetMove = False
                End If

            Case DirectionType.Left

                ' Check to make sure not outside of boundries
                If x > 0 Then
                    n = Map(MapNum).Tile(x - 1, y).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.NpcSpawn Then
                        CanPetMove = False
                        Exit Function
                    End If

                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = MapNum) AndAlso (GetPlayerX(i) = GetPetX(Index) - 1) AndAlso (GetPlayerY(i) = GetPetY(Index)) Then
                                CanPetMove = False
                                Exit Function
                            ElseIf PetAlive(i) AndAlso (GetPlayerMap(i) = MapNum) AndAlso (GetPetX(i) = GetPetX(Index) - 1) AndAlso (GetPetY(i) = GetPetY(Index)) Then
                                CanPetMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (MapNpc(MapNum).Npc(i).Num > 0) AndAlso (MapNpc(MapNum).Npc(i).X = GetPetX(Index) - 1) AndAlso (MapNpc(MapNum).Npc(i).Y = GetPetY(Index)) Then
                            CanPetMove = False
                            Exit Function
                        End If
                    Next

                    ' Directional blocking
                    If IsDirBlocked(Map(MapNum).Tile(GetPetX(Index), GetPetY(Index)).DirBlock, DirectionType.Left + 1) Then
                        CanPetMove = False
                        Exit Function
                    End If
                Else
                    CanPetMove = False
                End If

            Case DirectionType.Right

                ' Check to make sure not outside of boundries
                If x < Map(MapNum).MaxX Then
                    n = Map(MapNum).Tile(x + 1, y).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.NpcSpawn Then
                        CanPetMove = False
                        Exit Function
                    End If

                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = MapNum) AndAlso (GetPlayerX(i) = GetPetX(Index) + 1) AndAlso (GetPlayerY(i) = GetPetY(Index)) Then
                                CanPetMove = False
                                Exit Function
                            ElseIf PetAlive(i) AndAlso (GetPlayerMap(i) = MapNum) AndAlso (GetPetX(i) = GetPetX(Index) + 1) AndAlso (GetPetY(i) = GetPetY(Index)) Then
                                CanPetMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (MapNpc(MapNum).Npc(i).Num > 0) AndAlso (MapNpc(MapNum).Npc(i).X = GetPetX(Index) + 1) AndAlso (MapNpc(MapNum).Npc(i).Y = GetPetY(Index)) Then
                            CanPetMove = False
                            Exit Function
                        End If
                    Next

                    ' Directional blocking
                    If IsDirBlocked(Map(MapNum).Tile(GetPetX(Index), GetPetY(Index)).DirBlock, DirectionType.Right + 1) Then
                        CanPetMove = False
                        Exit Function
                    End If
                Else
                    CanPetMove = False
                End If

        End Select

    End Function

    Sub PetDir(index as integer, dir As Integer)
        dim buffer as ByteStream

        If Index <= 0 OrElse Index > MAX_PLAYERS OrElse Dir < DirectionType.Up OrElse Dir > DirectionType.Right Then Exit Sub

        If TempPlayer(Index).PetskillBuffer.Skill > 0 Then Exit Sub

        Player(Index).Character(TempPlayer(Index).CurChar).Pet.Dir = Dir

        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SPetDir)
        Buffer.WriteInt32(Index)
        Buffer.WriteInt32(Dir)
        SendDataToMap(GetPlayerMap(Index), Buffer.Data, Buffer.Head)

        Buffer.Dispose()

    End Sub

    Function PetTryWalk(index as integer, targetX As Integer, targetY As Integer) As Boolean
        Dim i As Integer, x As Integer, didwalk As Boolean
        Dim mapNum as Integer

        MapNum = GetPlayerMap(Index)
        x = Index

        If IsOneBlockAway(TargetX, TargetY, GetPetX(Index), GetPetY(Index)) = False Then

            If PathfindingType = 1 Then
                i = Int(Rnd() * 5)

                ' Lets move the pet
                Select Case i
                    Case 0
                        ' Up
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y > TargetY AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Up) Then
                                PetMove(x, MapNum, DirectionType.Up, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Down
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y < TargetY AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Down) Then
                                PetMove(x, MapNum, DirectionType.Down, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Left
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X > TargetX AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Left) Then
                                PetMove(x, MapNum, DirectionType.Left, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Right
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X < TargetX AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Right) Then
                                PetMove(x, MapNum, DirectionType.Right, MovementType.Walking)
                                didwalk = True
                            End If
                        End If
                    Case 1

                        ' Right
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X < TargetX AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Right) Then
                                PetMove(x, MapNum, DirectionType.Right, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Left
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X > TargetX AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Left) Then
                                PetMove(x, MapNum, DirectionType.Left, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Down
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y < TargetY AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Down) Then
                                PetMove(x, MapNum, DirectionType.Down, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Up
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y > TargetY AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Up) Then
                                PetMove(x, MapNum, DirectionType.Up, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                    Case 2

                        ' Down
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y < TargetY AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Down) Then
                                PetMove(x, MapNum, DirectionType.Down, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Up
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y > TargetY AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Up) Then
                                PetMove(x, MapNum, DirectionType.Up, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Right
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X < TargetX AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Right) Then
                                PetMove(x, MapNum, DirectionType.Right, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Left
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X > TargetX AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Left) Then
                                PetMove(x, MapNum, DirectionType.Left, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                    Case 3

                        ' Left
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X > TargetX AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Left) Then
                                Call PetMove(x, MapNum, DirectionType.Left, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Right
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X < TargetX AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Right) Then
                                PetMove(x, MapNum, DirectionType.Right, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Up
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y > TargetY AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Up) Then
                                PetMove(x, MapNum, DirectionType.Up, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Down
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y < TargetY AndAlso Not didwalk Then
                            If CanPetMove(x, MapNum, DirectionType.Down) Then
                                PetMove(x, MapNum, DirectionType.Down, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                End Select

                ' Check if we can't move and if Target is behind something and if we can just switch dirs
                If Not didwalk Then
                    If GetPetX(x) - 1 = TargetX AndAlso GetPetY(x) = TargetY Then

                        If GetPetDir(x) <> DirectionType.Left Then
                            PetDir(x, DirectionType.Left)
                        End If

                        didwalk = True
                    End If

                    If GetPetX(x) + 1 = TargetX AndAlso GetPetY(x) = TargetY Then

                        If GetPetDir(x) <> DirectionType.Right Then
                            PetDir(x, DirectionType.Right)
                        End If

                        didwalk = True
                    End If

                    If GetPetX(x) = TargetX AndAlso GetPetY(x) - 1 = TargetY Then

                        If GetPetDir(x) <> DirectionType.Up Then
                            PetDir(x, DirectionType.Up)
                        End If

                        didwalk = True
                    End If

                    If GetPetX(x) = TargetX AndAlso GetPetY(x) + 1 = TargetY Then

                        If GetPetDir(x) <> DirectionType.Down Then
                            PetDir(x, DirectionType.Down)
                        End If

                        didwalk = True
                    End If
                End If
            Else
                'Pathfind
                i = FindPetPath(MapNum, x, TargetX, TargetY)

                If i < 4 Then 'Returned an answer. Move the pet
                    If CanPetMove(x, MapNum, i) Then
                        PetMove(x, MapNum, i, MovementType.Walking)
                        didwalk = True
                    End If
                End If
            End If
        Else

            'Look to target
            If GetPetX(Index) > TempPlayer(Index).GoToX Then
                If CanPetMove(x, MapNum, DirectionType.Left) Then
                    PetMove(x, MapNum, DirectionType.Left, MovementType.Walking)
                    didwalk = True
                Else
                    PetDir(x, DirectionType.Left)
                    didwalk = True
                End If

            ElseIf GetPetX(Index) < TempPlayer(Index).GoToX Then

                If CanPetMove(x, MapNum, DirectionType.Right) Then
                    PetMove(x, MapNum, DirectionType.Right, MovementType.Walking)
                    didwalk = True
                Else
                    PetDir(x, DirectionType.Right)
                    didwalk = True
                End If

            ElseIf GetPetY(Index) > TempPlayer(Index).GoToY Then

                If CanPetMove(x, MapNum, DirectionType.Up) Then
                    PetMove(x, MapNum, DirectionType.Up, MovementType.Walking)
                    didwalk = True
                Else
                    PetDir(x, DirectionType.Up)
                    didwalk = True
                End If

            ElseIf GetPetY(Index) < TempPlayer(Index).GoToY Then

                If CanPetMove(x, MapNum, DirectionType.Down) Then
                    PetMove(x, MapNum, DirectionType.Down, MovementType.Walking)
                    didwalk = True
                Else
                    PetDir(x, DirectionType.Down)
                    didwalk = True
                End If
            End If
        End If

        ' We could not move so Target must be behind something, walk randomly.
        If Not didwalk Then
            i = Int(Rnd() * 2)

            If i = 1 Then
                i = Int(Rnd() * 4)

                If CanPetMove(x, MapNum, i) Then
                    PetMove(x, MapNum, i, MovementType.Walking)
                End If
            End If
        End If

        PetTryWalk = didwalk

    End Function

    Function FindPetPath(mapNum as Integer, index as integer, targetX As Integer, targetY As Integer) As Integer

        Dim tim As Integer, sX As Integer, sY As Integer, pos(,) As Integer, reachable As Boolean, j As Integer, lastSum As Integer, sum As Integer, fx As Integer, fy As Integer, i As Integer

        Dim path() As Point, lastX As Integer, lastY As Integer, did As Boolean

        'Initialization phase

        tim = 0
        sX = GetPetX(Index)
        sY = GetPetY(Index)

        FX = TargetX
        FY = TargetY

        If FX = -1 Then Exit Function
        If FY = -1 Then Exit Function

        ReDim pos(Map(MapNum).MaxX,Map(MapNum).MaxY)
        'pos = MapBlocks(MapNum).Blocks

        pos(sX, sY) = 100 + tim
        pos(FX, FY) = 2

        'reset reachable
        reachable = False

        'Do while reachable is false... if its set true in progress, we jump out
        'If the path is decided unreachable in process, we will use exit sub. Not proper,
        'but faster ;-)
        Do While reachable = False

            'we loop through all squares
            For j = 0 To Map(MapNum).MaxY
                For i = 0 To Map(MapNum).MaxX

                    'If j = 10 AndAlso i = 0 Then MsgBox "hi!"
                    'If they are to be extended, the pointer TIM is on them
                    If pos(i, j) = 100 + tim Then

                        'The part is to be extended, so do it
                        'We have to make sure that there is a pos(i+1,j) BEFORE we actually use it,
                        'because then we get error... If the square is on side, we dont test for this one!
                        If i < Map(MapNum).MaxX Then

                            'If there isnt a wall, or any other... thing
                            If pos(i + 1, j) = 0 Then
                                'Expand it, and make its pos equal to tim+1, so the next time we make this loop,
                                'It will exapand that square too! This is crucial part of the program
                                pos(i + 1, j) = 100 + tim + 1
                            ElseIf pos(i + 1, j) = 2 Then
                                'If the position is no 0 but its 2 (FINISH) then Reachable = true!!! We found end
                                reachable = True
                            End If
                        End If

                        'This is the same as the last one, as i said a lot of copy paste work and editing that
                        'This is simply another side that we have to test for... so instead of i+1 we have i-1
                        'Its actually pretty same then... I wont comment it therefore, because its only repeating
                        'same thing with minor changes to check sides
                        If i > 0 Then
                            If pos((i - 1), j) = 0 Then
                                pos(i - 1, j) = 100 + tim + 1
                            ElseIf pos(i - 1, j) = 2 Then
                                reachable = True
                            End If
                        End If

                        If j < Map(MapNum).MaxY Then
                            If pos(i, j + 1) = 0 Then
                                pos(i, j + 1) = 100 + tim + 1
                            ElseIf pos(i, j + 1) = 2 Then
                                reachable = True
                            End If
                        End If

                        If j > 0 Then
                            If pos(i, j - 1) = 0 Then
                                pos(i, j - 1) = 100 + tim + 1
                            ElseIf pos(i, j - 1) = 2 Then
                                reachable = True
                            End If
                        End If
                    End If

                    Application.DoEvents()
                Next
            Next

            'If the reachable is STILL false, then
            If reachable = False Then
                'reset sum
                Sum = 0

                For j = 0 To Map(MapNum).MaxY
                    For i = 0 To Map(MapNum).MaxX
                        'we add up ALL the squares
                        Sum = Sum + pos(i, j)
                    Next i
                Next j

                'Now if the sum is euqal to the last sum, its not reachable, if it isnt, then we store
                'sum to lastsum
                If Sum = LastSum Then
                    FindPetPath = 4
                    Exit Function
                Else
                    LastSum = Sum
                End If
            End If

            'we increase the pointer to point to the next squares to be expanded
            tim = tim + 1
        Loop

        'We work backwards to find the way...
        LastX = FX
        LastY = FY

        ReDim path(tim + 1)

        'The following code may be a little bit confusing but ill try my best to explain it.
        'We are working backwards to find ONE of the shortest ways back to Start.
        'So we repeat the loop until the LastX and LastY arent in start. Look in the code to see
        'how LastX and LasY change
        Do While LastX <> sX OrElse LastY <> sY
            'We decrease tim by one, and then we are finding any adjacent square to the final one, that
            'has that value. So lets say the tim would be 5, because it takes 5 steps to get to the target.
            'Now everytime we decrease that, so we make it 4, and we look for any adjacent square that has
            'that value. When we find it, we just color it yellow as for the solution
            tim = tim - 1
            'reset did to false
            did = False

            'If we arent on edge
            If LastX < Map(MapNum).MaxX Then

                'check the square on the right of the solution. Is it a tim-1 one? or just a blank one
                If pos(LastX + 1, LastY) = 100 + tim Then
                    'if it, then make it yellow, and change did to true
                    LastX = LastX + 1
                    did = True
                End If
            End If

            'This will then only work if the previous part didnt execute, and did is still false. THen
            'we want to check another square, the on left. Is it a tim-1 one ?
            If did = False Then
                If LastX > 0 Then
                    If pos(LastX - 1, LastY) = 100 + tim Then
                        LastX = LastX - 1
                        did = True
                    End If
                End If
            End If

            'We check the one below it
            If did = False Then
                If LastY < Map(MapNum).MaxY Then
                    If pos(LastX, LastY + 1) = 100 + tim Then
                        LastY = LastY + 1
                        did = True
                    End If
                End If
            End If

            'And above it. One of these have to be it, since we have found the solution, we know that already
            'there is a way back.
            If did = False Then
                If LastY > 0 Then
                    If pos(LastX, LastY - 1) = 100 + tim Then
                        LastY = LastY - 1
                    End If
                End If
            End If

            path(tim).X = LastX
            path(tim).Y = LastY

            'Now we loop back and decrease tim, and look for the next square with lower value
            Application.DoEvents()
        Loop

        'Ok we got a path. Now, lets look at the first step and see what direction we should take.
        If path(1).X > LastX Then
            FindPetPath = DirectionType.Right
        ElseIf path(1).Y > LastY Then
            FindPetPath = DirectionType.Down
        ElseIf path(1).Y < LastY Then
            FindPetPath = DirectionType.Up
        ElseIf path(1).X < LastX Then
            FindPetPath = DirectionType.Left
        End If

    End Function

    Function GetPetDamage(index as integer) As Integer
        GetPetDamage = 0

        ' Check for subscript out of range
        If IsPlaying(Index) = False OrElse Index <= 0 OrElse Index > MAX_PLAYERS OrElse Not PetAlive(Index) Then
            Exit Function
        End If

        GetPetDamage = (Player(Index).Character(TempPlayer(Index).CurChar).Pet.Stat(StatType.Strength) * 2) + (Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level * 3) + Random(0, 20)

    End Function

    Friend Function CanPetCrit(index as integer) As Boolean
        Dim rate As Integer
        Dim rndNum As Integer

        If Not PetAlive(Index) Then Exit Function

        CanPetCrit = False

        rate = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Stat(StatType.Luck) / 3
        rndNum = Random(1, 100)

        If rndNum <= rate Then CanPetCrit = True

    End Function
#End Region

#Region "Pet > Npc"
    Friend Sub TryPetAttackNpc(index as integer, mapNpcNum As Integer)
        Dim blockAmount As Integer
        Dim npcnum As Integer
        Dim mapNum as Integer
        Dim damage As Integer

        Damage = 0

        ' Can we attack the npc?
        If CanPetAttackNpc(Index, MapNpcNum) Then

            MapNum = GetPlayerMap(Index)
            npcnum = MapNpc(MapNum).Npc(MapNpcNum).Num

            ' check if NPC can avoid the attack
            If CanNpcDodge(npcnum) Then
                SendActionMsg(MapNum, "Dodge!", ColorType.Pink, 1, (MapNpc(MapNum).Npc(MapNpcNum).X * 32), (MapNpc(MapNum).Npc(MapNpcNum).Y * 32))
                Exit Sub
            End If

            If CanNpcParry(npcnum) Then
                SendActionMsg(MapNum, "Parry!", ColorType.Pink, 1, (MapNpc(MapNum).Npc(MapNpcNum).X * 32), (MapNpc(MapNum).Npc(MapNpcNum).Y * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            Damage = GetPetDamage(Index)

            ' if the npc blocks, take away the block amount
            blockAmount = CanNpcBlock(MapNpcNum)
            Damage = Damage - blockAmount

            ' take away armour
            Damage = Damage - Random(1, (Npc(npcnum).Stat(StatType.Luck) * 2))
            ' randomise from 1 to max hit
            Damage = Random(1, Damage)

            ' * 1.5 if it's a crit!
            If CanPetCrit(Index) Then
                Damage = Damage * 1.5
                SendActionMsg(MapNum, "Critical!", ColorType.BrightCyan, 1, (GetPlayerX(Index) * 32), (GetPlayerY(Index) * 32))
            End If

            If Damage > 0 Then
                PetAttackNpc(Index, MapNpcNum, Damage)
            Else
                PlayerMsg(Index, "Your pet's attack does nothing.", ColorType.BrightRed)
            End If

        End If

    End Sub

    Friend Function CanPetAttackNpc(attacker As Integer, mapnpcnum As Integer, Optional isSpell As Boolean = False) As Boolean
        Dim mapNum as Integer
        Dim npcnum As Integer
        Dim npcX As Integer
        Dim npcY As Integer
        Dim attackspeed As Integer

        If IsPlaying(Attacker) = False OrElse mapnpcnum <= 0 OrElse mapnpcnum > MAX_MAP_NPCS OrElse Not PetAlive(Attacker) Then
            Exit Function
        End If

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(Attacker)).Npc(mapnpcnum).Num <= 0 Then Exit Function

        MapNum = GetPlayerMap(Attacker)
        npcnum = MapNpc(MapNum).Npc(mapnpcnum).Num

        ' Make sure the npc isn't already dead
        If MapNpc(MapNum).Npc(mapnpcnum).Vital(VitalType.HP) <= 0 Then Exit Function

        ' Make sure they are on the same map
        If IsPlaying(Attacker) Then

            If TempPlayer(Attacker).PetskillBuffer.Skill > 0 AndAlso IsSpell = False Then Exit Function

            ' exit out early
            If IsSpell AndAlso npcnum > 0 Then
                If Npc(npcnum).Behaviour <> NpcBehavior.Friendly AndAlso Npc(npcnum).Behaviour <> NpcBehavior.ShopKeeper Then
                    CanPetAttackNpc = True
                    Exit Function
                End If
            End If

            attackspeed = 1000 'Pet cannot wield a weapon

            If npcnum > 0 AndAlso GetTimeMs() > TempPlayer(Attacker).PetAttackTimer + attackspeed Then

                ' Check if at same coordinates
                Select Case GetPetDir(Attacker)

                    Case DirectionType.Up
                        NpcX = MapNpc(MapNum).Npc(mapnpcnum).X
                        NpcY = MapNpc(MapNum).Npc(mapnpcnum).Y + 1

                    Case DirectionType.Down
                        NpcX = MapNpc(MapNum).Npc(mapnpcnum).X
                        NpcY = MapNpc(MapNum).Npc(mapnpcnum).Y - 1

                    Case DirectionType.Left
                        NpcX = MapNpc(MapNum).Npc(mapnpcnum).X + 1
                        NpcY = MapNpc(MapNum).Npc(mapnpcnum).Y

                    Case DirectionType.Right
                        NpcX = MapNpc(MapNum).Npc(mapnpcnum).X - 1
                        NpcY = MapNpc(MapNum).Npc(mapnpcnum).Y

                End Select

                If NpcX = GetPetX(Attacker) AndAlso NpcY = GetPetY(Attacker) Then
                    If Npc(npcnum).Behaviour <> NpcBehavior.Friendly AndAlso Npc(npcnum).Behaviour <> NpcBehavior.ShopKeeper Then
                        CanPetAttackNpc = True
                    Else
                        CanPetAttackNpc = False
                    End If
                End If
            End If
        End If

    End Function

    Friend Sub PetAttackNpc(attacker As Integer, mapnpcnum As Integer, damage As Integer, Optional skillnum As Integer = 0, Optional overTime As Boolean = False)
        Dim name As String, exp As Integer
        Dim n As Integer, i As Integer
        Dim mapNum as Integer, npcnum As Integer

        ' Check for subscript out of range
        If IsPlaying(Attacker) = False OrElse mapnpcnum <= 0 OrElse mapnpcnum > MAX_MAP_NPCS OrElse Damage < 0 OrElse Not PetAlive(Attacker) Then
            Exit Sub
        End If

        MapNum = GetPlayerMap(Attacker)
        npcnum = MapNpc(MapNum).Npc(mapnpcnum).Num
        Name = Trim$(Npc(npcnum).Name)

        If Skillnum = 0 Then
            ' Send this packet so they can see the pet attacking
            SendPetAttack(Attacker, MapNum)
        End If

        ' Check for weapon
        n = 0 'no weapon, pet :P

        ' set the regen timer
        TempPlayer(Attacker).PetstopRegen = True
        TempPlayer(Attacker).PetstopRegenTimer = GetTimeMs()

        If Damage >= MapNpc(MapNum).Npc(mapnpcnum).Vital(VitalType.HP) Then

            SendActionMsg(GetPlayerMap(Attacker), "-" & MapNpc(MapNum).Npc(mapnpcnum).Vital(VitalType.HP), ColorType.BrightRed, 1, (MapNpc(MapNum).Npc(mapnpcnum).X * 32), (MapNpc(MapNum).Npc(mapnpcnum).Y * 32))
            SendBlood(GetPlayerMap(Attacker), MapNpc(MapNum).Npc(mapnpcnum).X, MapNpc(MapNum).Npc(mapnpcnum).Y)

            ' Calculate exp to give attacker
            Exp = Npc(npcnum).Exp

            ' Make sure we dont get less then 0
            If Exp < 0 Then
                Exp = 1
            End If

            ' in party?
            If TempPlayer(Attacker).InParty > 0 Then
                ' pass through party sharing function
                Party_ShareExp(TempPlayer(Attacker).InParty, Exp, Attacker, MapNum)
            Else
                ' no party - keep exp for self
                GivePlayerEXP(Attacker, Exp)
            End If

            'For n = 1 To 20
            '    If MapNpc(MapNum).Npc(mapnpcnum).Num > 0 Then
            '        'SpawnItem(MapNpc(MapNum).Npc(mapnpcnum).Inventory(n).Num, MapNpc(MapNum).Npc(mapnpcnum).Inventory(n).Value, MapNum, MapNpc(MapNum).Npc(mapnpcnum).x, MapNpc(MapNum).Npc(mapnpcnum).y)
            '        'MapNpc(MapNum).Npc(mapnpcnum).Inventory(n).Value = 0
            '        'MapNpc(MapNum).Npc(mapnpcnum).Inventory(n).Num = 0
            '    End If
            'Next

            ' Now set HP to 0 so we know to actually kill them in the server loop (this prevents subscript out of range)
            MapNpc(MapNum).Npc(mapnpcnum).Num = 0
            MapNpc(MapNum).Npc(mapnpcnum).SpawnWait = GetTimeMs()
            MapNpc(MapNum).Npc(mapnpcnum).Vital(VitalType.HP) = 0
            MapNpc(MapNum).Npc(mapnpcnum).TargetType = 0
            MapNpc(MapNum).Npc(mapnpcnum).Target = 0

            ' clear DoTs and HoTs
            'For i = 1 To MAX_DOTS
            '    With MapNpc(MapNum).Npc(mapnpcnum).DoT(i)
            '        .Spell = 0
            '        .Timer = 0
            '        .Caster = 0
            '        .StartTime = 0
            '        .Used = False
            '    End With
            '    With MapNpc(MapNum).Npc(mapnpcnum).HoT(i)
            '        .Spell = 0
            '        .Timer = 0
            '        .Caster = 0
            '        .StartTime = 0
            '        .Used = False
            '    End With
            'Next

            ' send death to the map
            SendNpcDead(MapNum, mapnpcnum)

            'Loop through entire map and purge NPC from targets
            For i = 1 To Socket.HighIndex

                If IsPlaying(i) Then
                    If GetPlayerMap(i) = MapNum Then
                        If TempPlayer(i).TargetType = TargetType.Npc Then
                            If TempPlayer(i).Target = mapnpcnum Then
                                TempPlayer(i).Target = 0
                                TempPlayer(i).TargetType = TargetType.None
                                SendTarget(i, 0, TargetType.None)
                            End If
                        End If

                        If TempPlayer(i).PetTargetType = TargetType.Npc Then
                            If TempPlayer(i).PetTarget = mapnpcnum Then
                                TempPlayer(i).PetTarget = 0
                                TempPlayer(i).PetTargetType = TargetType.None
                            End If
                        End If
                    End If
                End If
            Next
        Else
            ' NPC not dead, just do the damage
            MapNpc(MapNum).Npc(mapnpcnum).Vital(VitalType.HP) = MapNpc(MapNum).Npc(mapnpcnum).Vital(VitalType.HP) - Damage

            ' Check for a weapon and say damage
            SendActionMsg(MapNum, "-" & Damage, ColorType.BrightRed, 1, (MapNpc(MapNum).Npc(mapnpcnum).X * 32), (MapNpc(MapNum).Npc(mapnpcnum).Y * 32))
            SendBlood(GetPlayerMap(Attacker), MapNpc(MapNum).Npc(mapnpcnum).X, MapNpc(MapNum).Npc(mapnpcnum).Y)

            ' send the sound
            'If Spellnum > 0 Then SendMapSound Attacker, MapNpc(MapNum).Npc(mapnpcnum).x, MapNpc(MapNum).Npc(mapnpcnum).y, SoundEntity.seSpell, Spellnum

            ' Set the NPC target to the player
            MapNpc(MapNum).Npc(mapnpcnum).TargetType = TargetType.Pet ' player's pet
            MapNpc(MapNum).Npc(mapnpcnum).Target = Attacker

            ' Now check for guard ai and if so have all onmap guards come after'm
            If Npc(MapNpc(MapNum).Npc(mapnpcnum).Num).Behaviour = NpcBehavior.Guard Then

                For i = 1 To MAX_MAP_NPCS

                    If MapNpc(MapNum).Npc(i).Num = MapNpc(MapNum).Npc(mapnpcnum).Num Then
                        MapNpc(MapNum).Npc(i).Target = Attacker
                        MapNpc(MapNum).Npc(i).TargetType = TargetType.Pet ' pet
                    End If
                Next
            End If

            ' set the regen timer
            MapNpc(MapNum).Npc(mapnpcnum).StopRegen = True
            MapNpc(MapNum).Npc(mapnpcnum).StopRegenTimer = GetTimeMs()

            ' if stunning spell, stun the npc
            If Skillnum > 0 Then
                If Skill(Skillnum).StunDuration > 0 Then StunNPC(mapnpcnum, MapNum, Skillnum)
                ' DoT
                If Skill(Skillnum).Duration > 0 Then
                    'AddDoT_Npc(MapNum, mapnpcnum, Skillnum, Attacker, 3)
                End If
            End If

            SendMapNpcVitals(MapNum, mapnpcnum)
        End If

        If Skillnum = 0 Then
            ' Reset attack timer
            TempPlayer(Attacker).PetAttackTimer = GetTimeMs()
        End If

    End Sub

#End Region

#Region "Npc > Pet"
    Friend Sub TryNpcAttackPet(mapNpcNum As Integer, index as integer)

        Dim mapNum as Integer, npcnum As Integer, damage As Integer

        ' Can the npc attack the pet?

        If CanNpcAttackPet(MapNpcNum, Index) Then
            MapNum = GetPlayerMap(Index)
            npcnum = MapNpc(MapNum).Npc(MapNpcNum).Num

            ' check if Pet can avoid the attack
            If CanPetDodge(Index) Then
                SendActionMsg(MapNum, "Dodge!", ColorType.Pink, ActionMsgType.Scroll, (GetPetX(Index) * 32), (GetPetY(Index) * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            Damage = GetNpcDamage(npcnum)

            ' take away armour
            Damage = Damage - ((GetPetStat(Index, StatType.Endurance) * 2) + (GetPetLevel(Index) * 2))

            ' * 1.5 if crit hit
            If CanNpcCrit(npcnum) Then
                Damage = Damage * 1.5
                SendActionMsg(MapNum, "Critical!", ColorType.BrightCyan, ActionMsgType.Scroll, (MapNpc(MapNum).Npc(MapNpcNum).X * 32), (MapNpc(MapNum).Npc(MapNpcNum).Y * 32))
            End If
        End If

        If Damage > 0 Then
            NpcAttackPet(MapNpcNum, Index, Damage)
        End If

    End Sub

    Function CanNpcAttackPet(mapNpcNum As Integer, index as integer) As Boolean
        Dim mapNum as Integer
        Dim npcnum As Integer

        CanNpcAttackPet = False

        If MapNpcNum <= 0 OrElse MapNpcNum > MAX_MAP_NPCS OrElse Not IsPlaying(Index) OrElse Not PetAlive(Index) Then
            Exit Function
        End If

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(Index)).Npc(MapNpcNum).Num <= 0 Then Exit Function

        MapNum = GetPlayerMap(Index)
        npcnum = MapNpc(MapNum).Npc(MapNpcNum).Num

        ' Make sure the npc isn't already dead
        If MapNpc(MapNum).Npc(MapNpcNum).Vital(VitalType.HP) <= 0 Then Exit Function

        ' Make sure npcs dont attack more then once a second
        If GetTimeMs() < MapNpc(MapNum).Npc(MapNpcNum).AttackTimer + 1000 Then Exit Function

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(Index).GettingMap = 1 Then Exit Function

        MapNpc(MapNum).Npc(MapNpcNum).AttackTimer = GetTimeMs()

        ' Make sure they are on the same map
        If IsPlaying(Index) AndAlso PetAlive(Index) Then
            If npcnum > 0 Then

                ' Check if at same coordinates
                If (GetPetY(Index) + 1 = MapNpc(MapNum).Npc(MapNpcNum).Y) AndAlso (GetPetX(Index) = MapNpc(MapNum).Npc(MapNpcNum).X) Then
                    CanNpcAttackPet = True
                Else

                    If (GetPetY(Index) - 1 = MapNpc(MapNum).Npc(MapNpcNum).Y) AndAlso (GetPetX(Index) = MapNpc(MapNum).Npc(MapNpcNum).X) Then
                        CanNpcAttackPet = True
                    Else

                        If (GetPetY(Index) = MapNpc(MapNum).Npc(MapNpcNum).Y) AndAlso (GetPetX(Index) + 1 = MapNpc(MapNum).Npc(MapNpcNum).X) Then
                            CanNpcAttackPet = True
                        Else

                            If (GetPetY(Index) = MapNpc(MapNum).Npc(MapNpcNum).Y) AndAlso (GetPetX(Index) - 1 = MapNpc(MapNum).Npc(MapNpcNum).X) Then
                                CanNpcAttackPet = True
                            End If
                        End If
                    End If
                End If
            End If
        End If

    End Function

    Sub NpcAttackPet(mapnpcnum As Integer, victim As Integer, damage As Integer)
        Dim name As String, mapNum as Integer

        ' Check for subscript out of range
        If mapnpcnum <= 0 OrElse mapnpcnum > MAX_MAP_NPCS OrElse IsPlaying(Victim) = False OrElse Not PetAlive(Victim) Then
            Exit Sub
        End If

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(Victim)).Npc(mapnpcnum).Num <= 0 Then Exit Sub

        MapNum = GetPlayerMap(Victim)
        Name = Trim$(Npc(MapNpc(MapNum).Npc(mapnpcnum).Num).Name)

        ' Send this packet so they can see the npc attacking
        SendNpcAttack(Victim, mapnpcnum)

        If Damage <= 0 Then Exit Sub

        ' set the regen timer
        MapNpc(MapNum).Npc(mapnpcnum).StopRegen = True
        MapNpc(MapNum).Npc(mapnpcnum).StopRegenTimer = GetTimeMs()

        If Damage >= GetPetVital(Victim, VitalType.HP) Then
            ' Say damage
            SendActionMsg(GetPlayerMap(Victim), "-" & GetPetVital(Victim, VitalType.HP), ColorType.BrightRed, ActionMsgType.Scroll, (GetPetX(Victim) * 32), (GetPetY(Victim) * 32))

            ' kill pet
            PlayerMsg(Victim, "Your " & Trim$(GetPetName(Victim)) & " was killed by a " & Trim$(Npc(MapNpc(MapNum).Npc(mapnpcnum).Num).Name) & ".", ColorType.BrightRed)
            ReCallPet(Victim)

            ' Now that pet is dead, go for owner
            MapNpc(MapNum).Npc(mapnpcnum).Target = Victim
            MapNpc(MapNum).Npc(mapnpcnum).TargetType = TargetType.Player
        Else
            ' Pet not dead, just do the damage
            SetPetVital(Victim, VitalType.HP, GetPetVital(Victim, VitalType.HP) - Damage)
            SendPetVital(Victim, VitalType.HP)
            SendAnimation(MapNum, Npc(MapNpc(GetPlayerMap(Victim)).Npc(mapnpcnum).Num).Animation, 0, 0, TargetType.Pet, Victim)

            ' Say damage
            SendActionMsg(GetPlayerMap(Victim), "-" & Damage, ColorType.BrightRed, ActionMsgType.Scroll, (GetPetX(Victim) * 32), (GetPetY(Victim) * 32))
            SendBlood(GetPlayerMap(Victim), GetPetX(Victim), GetPetY(Victim))

            ' set the regen timer
            TempPlayer(Victim).PetstopRegen = True
            TempPlayer(Victim).PetstopRegenTimer = GetTimeMs()

            'pet gets attacked, lets set this target
            TempPlayer(Victim).PetTarget = mapnpcnum
            TempPlayer(Victim).PetTargetType = TargetType.Npc
        End If

    End Sub
#End Region


    Function CanPetAttackPlayer(attacker As Integer, victim As Integer, Optional isSkill As Boolean = False) As Boolean

        If Not IsSkill Then
            If GetTimeMs() < TempPlayer(Attacker).PetAttackTimer + 1000 Then Exit Function
        End If

        ' Check for subscript out of range
        If Not IsPlaying(Victim) Then Exit Function

        ' Make sure they are on the same map
        If Not GetPlayerMap(Attacker) = GetPlayerMap(Victim) Then Exit Function

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(Victim).GettingMap = 1 Then Exit Function

        If TempPlayer(Attacker).PetskillBuffer.Skill > 0 AndAlso IsSkill = False Then Exit Function

        If Not IsSkill Then
            ' Check if at same coordinates
            Select Case GetPetDir(Attacker)
                Case DirectionType.Up
                    If Not (GetPlayerY(Victim) + 1 = GetPetY(Attacker)) AndAlso (GetPlayerX(Victim) = GetPetX(Attacker)) Then Exit Function

                Case DirectionType.Down
                    If Not (GetPlayerY(Victim) - 1 = GetPetY(Attacker)) AndAlso (GetPlayerX(Victim) = GetPetX(Attacker)) Then Exit Function

                Case DirectionType.Left
                    If Not (GetPlayerY(Victim) = GetPetY(Attacker)) AndAlso (GetPlayerX(Victim) + 1 = GetPetX(Attacker)) Then Exit Function

                Case DirectionType.Right
                    If Not (GetPlayerY(Victim) = GetPetY(Attacker)) AndAlso (GetPlayerX(Victim) - 1 = GetPetX(Attacker)) Then Exit Function

                Case Else
                    Exit Function
            End Select
        End If

        ' Check if map is attackable
        If Not Map(GetPlayerMap(Attacker)).Moral = MapMoralType.None Then
            If GetPlayerPK(Victim) = 0 Then
                Exit Function
            End If
        End If

        ' Make sure they have more then 0 hp
        If GetPlayerVital(Victim, VitalType.HP) <= 0 Then Exit Function

        ' Check to make sure that they dont have access
        If GetPlayerAccess(Attacker) > AdminType.Monitor Then
            PlayerMsg(Attacker, "Admins cannot attack other players.", ColorType.Yellow)
            Exit Function
        End If

        ' Check to make sure the victim isn't an admin
        If GetPlayerAccess(Victim) > AdminType.Monitor Then
            PlayerMsg(Attacker, "You cannot attack " & GetPlayerName(Victim) & "!", ColorType.Yellow)
            Exit Function
        End If

        ' Don't attack a party member
        If TempPlayer(Attacker).InParty > 0 AndAlso TempPlayer(Victim).InParty > 0 Then
            If TempPlayer(Attacker).InParty = TempPlayer(Victim).InParty Then
                PlayerMsg(Attacker, "You can't attack another party member!", ColorType.Yellow)
                Exit Function
            End If
        End If

        CanPetAttackPlayer = True

    End Function

    'Pet Vital Stuffs
    Sub SendPetVital(index as integer, vital As VitalType)
        dim buffer as ByteStream
        Buffer = New ByteStream(4)

        Buffer.WriteInt32(ServerPackets.SPetVital)

        Buffer.WriteInt32(Index)

        If Vital = VitalType.HP Then
            Buffer.WriteInt32(1)
        ElseIf Vital = VitalType.MP Then
            Buffer.WriteInt32(2)
        End If

        Select Case Vital
            Case VitalType.HP
                Buffer.WriteInt32(GetPetMaxVital(Index, VitalType.HP))
                buffer.WriteInt32(GetPetVital(index, VitalType.HP))

            Case VitalType.MP
                Buffer.WriteInt32(GetPetMaxVital(Index, VitalType.MP))
                Buffer.WriteInt32(GetPetVital(Index, VitalType.MP))
        End Select

        SendDataToMap(GetPlayerMap(Index), Buffer.Data, Buffer.Head)

        Buffer.Dispose()

    End Sub

    Friend Sub BufferPetSkill(index as integer, skillSlot As Integer)
        Dim skillnum As Integer, mpCost As Integer, levelReq As Integer
        Dim mapNum as Integer, skillCastType As Integer
        Dim accessReq As Integer, range As Integer, hasBuffered As Boolean
        Dim targetTypes As Byte, target As Integer

        ' Prevent subscript out of range

        If SkillSlot <= 0 OrElse SkillSlot > 4 Then Exit Sub

        Skillnum = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Skill(SkillSlot)
        MapNum = GetPlayerMap(Index)

        If Skillnum <= 0 OrElse Skillnum > MAX_SKILLS Then Exit Sub

        ' see if cooldown has finished
        If TempPlayer(Index).PetSkillCD(SkillSlot) > GetTimeMs() Then
            PlayerMsg(Index, Trim$(GetPetName(Index)) & "'s Skill hasn't cooled down yet!", ColorType.BrightRed)
            Exit Sub
        End If

        MPCost = Skill(Skillnum).MpCost

        ' Check if they have enough MP
        If GetPetVital(Index, VitalType.MP) < MPCost Then
            PlayerMsg(Index, "Your " & Trim$(GetPetName(Index)) & " does not have enough mana!", ColorType.BrightRed)
            Exit Sub
        End If

        LevelReq = Skill(Skillnum).LevelReq

        ' Make sure they are the right level
        If LevelReq > GetPetLevel(Index) Then
            PlayerMsg(Index, Trim$(GetPetName(Index)) & " must be level " & LevelReq & " to cast this skill.", ColorType.BrightRed)
            Exit Sub
        End If

        AccessReq = Skill(Skillnum).AccessReq

        ' make sure they have the right access
        If AccessReq > GetPlayerAccess(Index) Then
            PlayerMsg(Index, "You must be an administrator to cast this spell, even as a pet owner.", ColorType.BrightRed)
            Exit Sub
        End If

        ' find out what kind of spell it is! self cast, target or AOE
        If Skill(Skillnum).Range > 0 Then

            ' ranged attack, single target or aoe?
            If Not Skill(Skillnum).IsAoE Then
                SkillCastType = 2 ' targetted
            Else
                SkillCastType = 3 ' targetted aoe
            End If
        Else
            If Not Skill(Skillnum).IsAoE Then
                SkillCastType = 0 ' self-cast
            Else
                SkillCastType = 1 ' self-cast AoE
            End If
        End If

        TargetTypes = TempPlayer(Index).PetTargetType
        Target = TempPlayer(Index).PetTarget
        Range = Skill(Skillnum).Range
        HasBuffered = False

        Select Case SkillCastType

            'PET
            Case 0, 1, SkillType.Pet ' self-cast & self-cast AOE
                HasBuffered = True

            Case 2, 3 ' targeted & targeted AOE

                ' check if have target
                If Not Target > 0 Then
                    If SkillCastType = SkillType.HealHp OrElse SkillCastType = SkillType.HealMp Then
                        Target = Index
                        TargetTypes = TargetType.Pet
                    Else
                        PlayerMsg(Index, "Your " & Trim$(GetPetName(Index)) & " does not have a target.", ColorType.Yellow)
                    End If
                End If

                If TargetTypes = TargetType.Player Then

                    ' if have target, check in range
                    If Not IsInRange(Range, GetPetX(Index), GetPetY(Index), GetPlayerX(Target), GetPlayerY(Target)) Then
                        PlayerMsg(Index, "Target not in range of " & Trim$(GetPetName(Index)) & ".", ColorType.Yellow)
                    Else
                        ' go through spell types
                        If Skill(Skillnum).Type <> SkillType.DamageHp AndAlso Skill(Skillnum).Type <> SkillType.DamageMp Then
                            HasBuffered = True
                        Else
                            If CanPetAttackPlayer(Index, Target, True) Then
                                HasBuffered = True
                            End If
                        End If
                    End If

                ElseIf TargetTypes = TargetType.Npc Then

                    ' if have target, check in range
                    If Not IsInRange(Range, GetPetX(Index), GetPetY(Index), MapNpc(MapNum).Npc(Target).X, MapNpc(MapNum).Npc(Target).Y) Then
                        PlayerMsg(Index, "Target not in range of " & Trim$(GetPetName(Index)) & ".", ColorType.Yellow)
                        HasBuffered = False
                    Else
                        ' go through spell types
                        If Skill(Skillnum).Type <> SkillType.DamageHp AndAlso Skill(Skillnum).Type <> SkillType.DamageMp Then
                            HasBuffered = True
                        Else
                            If CanPetAttackNpc(Index, Target, True) Then
                                HasBuffered = True
                            End If
                        End If
                    End If

                    'PET
                ElseIf TargetTypes = TargetType.Pet Then

                    ' if have target, check in range
                    If Not IsInRange(Range, GetPetX(Index), GetPetY(Index), GetPetX(Target), GetPetY(Target)) Then
                        PlayerMsg(Index, "Target not in range of " & Trim$(GetPetName(Index)) & ".", ColorType.Yellow)
                        HasBuffered = False
                    Else
                        ' go through spell types
                        If Skill(Skillnum).Type <> SkillType.DamageHp AndAlso Skill(Skillnum).Type <> SkillType.DamageMp Then
                            HasBuffered = True
                        Else
                            If CanPetAttackPet(Index, Target, Skillnum) Then
                                HasBuffered = True
                            End If
                        End If
                    End If
                End If
        End Select

        If HasBuffered Then
            SendAnimation(MapNum, Skill(Skillnum).CastAnim, 0, 0, TargetType.Pet, Index)
            SendActionMsg(MapNum, "Casting " & Trim$(Skill(Skillnum).Name) & "!", ColorType.BrightRed, ActionMsgType.Scroll, GetPetX(Index) * 32, GetPetY(Index) * 32)
            TempPlayer(Index).PetskillBuffer.Skill = SkillSlot
            TempPlayer(Index).PetskillBuffer.Timer = GetTimeMs()
            TempPlayer(Index).PetskillBuffer.Target = Target
            TempPlayer(Index).PetskillBuffer.TargetTypes = TargetTypes
            Exit Sub
        Else
            SendClearPetSpellBuffer(Index)
        End If

    End Sub

    Sub SendClearPetSpellBuffer(index as integer)

        dim buffer as ByteStream

        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SClearPetSkillBuffer)

        Socket.SendDataTo(Index, Buffer.Data, Buffer.Head)

        Buffer.Dispose()

    End Sub

    Friend Sub PetCastSkill(index as integer, skillslot As Integer, target As Integer, targetTypes As Byte, Optional takeMana As Boolean = True)
        Dim skillnum As Integer, mpCost As Integer, levelReq As Integer
        Dim mapNum as Integer, vital As Integer, didCast As Boolean
        Dim accessReq As Integer, i As Integer
        Dim aoE As Integer, range As Integer, vitalType As Byte
        Dim increment As Boolean, x As Integer, y As Integer
        Dim skillCastType As Integer

        DidCast = False

        ' Prevent subscript out of range
        If Skillslot <= 0 OrElse Skillslot > 4 Then Exit Sub

        Skillnum = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Skill(Skillslot)
        MapNum = GetPlayerMap(Index)

        MPCost = Skill(Skillnum).MpCost

        ' Check if they have enough MP
        If Player(Index).Character(TempPlayer(Index).CurChar).Pet.Mana < MPCost Then
            PlayerMsg(Index, "Your " & Trim$(GetPetName(Index)) & " does not have enough mana!", ColorType.BrightRed)
            Exit Sub
        End If

        LevelReq = Skill(Skillnum).LevelReq

        ' Make sure they are the right level
        If LevelReq > Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level Then
            PlayerMsg(Index, Trim$(GetPetName(Index)) & " must be level " & LevelReq & " to cast this spell.", ColorType.BrightRed)
            Exit Sub
        End If

        AccessReq = Skill(Skillnum).AccessReq

        ' make sure they have the right access
        If AccessReq > GetPlayerAccess(Index) Then
            PlayerMsg(Index, "You must be an administrator for even your pet to cast this spell.", ColorType.BrightRed)
            Exit Sub
        End If

        ' find out what kind of spell it is! self cast, target or AOE
        If Skill(Skillnum).IsProjectile = True Then
            SkillCastType = 4 ' Projectile
        ElseIf Skill(Skillnum).Range > 0 Then
            ' ranged attack, single target or aoe?
            If Not Skill(Skillnum).IsAoE Then
                SkillCastType = 2 ' targetted
            Else
                SkillCastType = 3 ' targetted aoe
            End If
        Else
            If Not Skill(Skillnum).IsAoE Then
                SkillCastType = 0 ' self-cast
            Else
                SkillCastType = 1 ' self-cast AoE
            End If
        End If

        ' set the vital
        Vital = Skill(Skillnum).Vital
        AoE = Skill(Skillnum).AoE
        Range = Skill(Skillnum).Range

        Select Case SkillCastType
            Case 0 ' self-cast target
                Select Case Skill(Skillnum).Type
                    Case SkillType.HealHp
                        SkillPet_Effect(Enums.VitalType.HP, True, Index, Vital, Skillnum)
                        DidCast = True
                    Case SkillType.HealMp
                        SkillPet_Effect(Enums.VitalType.MP, True, Index, Vital, Skillnum)
                        DidCast = True
                End Select

            Case 1, 3 ' self-cast AOE & targetted AOE

                If SkillCastType = 1 Then
                    x = GetPetX(Index)
                    y = GetPetY(Index)
                ElseIf SkillCastType = 3 Then

                    If TargetTypes = 0 Then Exit Sub
                    If Target = 0 Then Exit Sub

                    If TargetTypes = TargetType.Player Then
                        x = GetPlayerX(Target)
                        y = GetPlayerY(Target)
                    ElseIf TargetTypes = TargetType.Npc Then
                        x = MapNpc(MapNum).Npc(Target).X
                        y = MapNpc(MapNum).Npc(Target).Y
                    ElseIf TargetTypes = TargetType.Pet Then
                        x = GetPetX(Target)
                        y = GetPetY(Target)
                    End If

                    If Not IsInRange(Range, GetPetX(Index), GetPetY(Index), x, y) Then
                        PlayerMsg(Index, Trim$(GetPetName(Index)) & "'s target not in range.", ColorType.Yellow)
                        SendClearPetSpellBuffer(Index)
                    End If
                End If

                Select Case Skill(Skillnum).Type

                    Case SkillType.DamageHp
                        DidCast = True

                        For i = 1 To GetPlayersOnline()
                            If IsPlaying(i) AndAlso i <> Index Then
                                If GetPlayerMap(i) = GetPlayerMap(Index) Then
                                    If IsInRange(AoE, x, y, GetPlayerX(i), GetPlayerY(i)) Then
                                        If CanPetAttackPlayer(Index, i, True) AndAlso Index <> Target Then
                                            SendAnimation(MapNum, Skill(Skillnum).SkillAnim, 0, 0, TargetType.Player, i)
                                            PetAttackPlayer(Index, i, Vital, Skillnum)
                                        End If
                                    End If

                                    If PetAlive(i) Then
                                        If IsInRange(AoE, x, y, GetPetX(i), GetPetY(i)) Then

                                            If CanPetAttackPet(Index, i, Skillnum) Then
                                                SendAnimation(MapNum, Skill(Skillnum).SkillAnim, 0, 0, TargetType.Pet, i)
                                                PetAttackPet(Index, i, Vital, Skillnum)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next

                        For i = 1 To MAX_MAP_NPCS
                            If MapNpc(MapNum).Npc(i).Num > 0 AndAlso MapNpc(MapNum).Npc(i).Vital(Enums.VitalType.HP) > 0 Then
                                If IsInRange(AoE, x, y, MapNpc(MapNum).Npc(i).X, MapNpc(MapNum).Npc(i).Y) Then
                                    If CanPetAttackNpc(Index, i, True) Then
                                        SendAnimation(MapNum, Skill(Skillnum).SkillAnim, 0, 0, TargetType.Npc, i)
                                        PetAttackNpc(Index, i, Vital, Skillnum)
                                    End If
                                End If
                            End If
                        Next

                    Case SkillType.HealHp, SkillType.HealMp, SkillType.DamageMp

                        If Skill(Skillnum).Type = SkillType.HealHp Then
                            VitalType = Enums.VitalType.HP
                            increment = True
                        ElseIf Skill(Skillnum).Type = SkillType.HealMp Then
                            VitalType = Enums.VitalType.MP
                            increment = True
                        ElseIf Skill(Skillnum).Type = SkillType.DamageMp Then
                            VitalType = Enums.VitalType.MP
                            increment = False
                        End If

                        DidCast = True

                        For i = 1 To GetPlayersOnline()
                            If IsPlaying(i) AndAlso GetPlayerMap(i) = GetPlayerMap(Index) Then
                                If IsInRange(AoE, x, y, GetPlayerX(i), GetPlayerY(i)) Then
                                    SpellPlayer_Effect(VitalType, increment, i, Vital, Skillnum)
                                End If

                                If PetAlive(i) Then
                                    If IsInRange(AoE, x, y, GetPetX(i), GetPetY(i)) Then
                                        SkillPet_Effect(VitalType, increment, i, Vital, Skillnum)
                                    End If
                                End If
                            End If
                        Next
                End Select

            Case 2 ' targetted

                If TargetTypes = 0 Then Exit Sub
                If Target = 0 Then Exit Sub

                If TargetTypes = TargetType.Player Then
                    x = GetPlayerX(Target)
                    y = GetPlayerY(Target)
                ElseIf TargetTypes = TargetType.Npc Then
                    x = MapNpc(MapNum).Npc(Target).X
                    y = MapNpc(MapNum).Npc(Target).Y
                ElseIf TargetTypes = TargetType.Pet Then
                    x = GetPetX(Target)
                    y = GetPetY(Target)
                End If

                If Not IsInRange(Range, GetPetX(Index), GetPetY(Index), x, y) Then
                    PlayerMsg(Index, "Target is not in range of your " & Trim$(GetPetName(Index)) & "!", ColorType.Yellow)
                    SendClearPetSpellBuffer(Index)
                    Exit Sub
                End If

                Select Case Skill(Skillnum).Type

                    Case SkillType.DamageHp

                        If TargetTypes = TargetType.Player Then
                            If CanPetAttackPlayer(Index, Target, True) AndAlso Index <> Target Then
                                If Vital > 0 Then
                                    SendAnimation(MapNum, Skill(Skillnum).SkillAnim, 0, 0, TargetType.Player, Target)
                                    PetAttackPlayer(Index, Target, Vital, Skillnum)
                                    DidCast = True
                                End If
                            End If
                        ElseIf TargetTypes = TargetType.Npc Then
                            If CanPetAttackNpc(Index, Target, True) Then
                                If Vital > 0 Then
                                    SendAnimation(MapNum, Skill(Skillnum).SkillAnim, 0, 0, TargetType.Npc, Target)
                                    PetAttackNpc(Index, Target, Vital, Skillnum)
                                    DidCast = True
                                End If
                            End If
                        ElseIf TargetTypes = TargetType.Pet Then
                            If CanPetAttackPet(Index, Target, Skillnum) Then
                                If Vital > 0 Then
                                    SendAnimation(MapNum, Skill(Skillnum).SkillAnim, 0, 0, TargetType.Pet, Target)
                                    PetAttackPet(Index, Target, Vital, Skillnum)
                                    DidCast = True
                                End If
                            End If
                        End If

                    Case SkillType.DamageMp, SkillType.HealMp, SkillType.HealHp

                        If Skill(Skillnum).Type = SkillType.DamageMp Then
                            VitalType = Enums.VitalType.MP
                            increment = False
                        ElseIf Skill(Skillnum).Type = SkillType.HealMp Then
                            VitalType = Enums.VitalType.MP
                            increment = True
                        ElseIf Skill(Skillnum).Type = SkillType.HealHp Then
                            VitalType = Enums.VitalType.HP
                            increment = True
                        End If

                        If TargetTypes = TargetType.Player Then
                            If Skill(Skillnum).Type = SkillType.DamageMp Then
                                If CanPetAttackPlayer(Index, Target, True) Then
                                    SpellPlayer_Effect(VitalType, increment, Target, Vital, Skillnum)
                                End If
                            Else
                                SpellPlayer_Effect(VitalType, increment, Target, Vital, Skillnum)
                            End If

                        ElseIf TargetTypes = TargetType.Npc Then

                            If Skill(Skillnum).Type = SkillType.DamageMp Then
                                If CanPetAttackNpc(Index, Target, True) Then
                                    SpellNpc_Effect(VitalType, increment, Target, Vital, Skillnum, MapNum)
                                End If
                            Else
                                If Skill(Skillnum).Type = SkillType.HealHp OrElse Skill(Skillnum).Type = SkillType.HealMp Then
                                    SkillPet_Effect(VitalType, increment, Index, Vital, Skillnum)
                                Else
                                    SpellNpc_Effect(VitalType, increment, Target, Vital, Skillnum, MapNum)
                                End If
                            End If

                        ElseIf TargetTypes = TargetType.Pet Then

                            If Skill(Skillnum).Type = SkillType.DamageMp Then
                                If CanPetAttackPet(Index, Target, Skillnum) Then
                                    SkillPet_Effect(VitalType, increment, Target, Vital, Skillnum)
                                End If
                            Else
                                SkillPet_Effect(VitalType, increment, Target, Vital, Skillnum)
                                SendPetVital(Target, Vital)
                            End If
                        End If
                End Select

            Case 4 ' Projectile
                PetFireProjectile(Index, Skillnum)
                DidCast = True
        End Select

        If DidCast Then
            If TakeMana Then SetPetVital(Index, Enums.VitalType.MP, GetPetVital(Index, Enums.VitalType.MP) - MPCost)
            SendPetVital(Index, Enums.VitalType.MP)
            SendPetVital(Index, Enums.VitalType.HP)

            TempPlayer(Index).PetSkillCD(Skillslot) = GetTimeMs() + (Skill(Skillnum).CdTime * 1000)

            SendActionMsg(MapNum, Trim$(Skill(Skillnum).Name) & "!", ColorType.BrightRed, ActionMsgType.Scroll, GetPetX(Index) * 32, GetPetY(Index) * 32)
        End If

    End Sub

    Friend Sub SkillPet_Effect(vital As Byte, increment As Boolean, index as integer, damage As Integer, skillnum As Integer)
        Dim sSymbol As String
        Dim colour As Integer

        If Damage > 0 Then
            If increment Then
                sSymbol = "+"
                If Vital = VitalType.HP Then Colour = ColorType.BrightGreen
                If Vital = VitalType.MP Then Colour = ColorType.BrightBlue
            Else
                sSymbol = "-"
                Colour = ColorType.Blue
            End If

            SendAnimation(GetPlayerMap(Index), Skill(Skillnum).SkillAnim, 0, 0, TargetType.Pet, Index)
            SendActionMsg(GetPlayerMap(Index), sSymbol & Damage, Colour, ActionMsgType.Scroll, GetPetX(Index) * 32, GetPetY(Index) * 32)

            ' send the sound
            'SendMapSound(Index, Player(Index).Character(TempPlayer(Index).CurChar).Pet.x, Player(Index).Character(TempPlayer(Index).CurChar).Pet.y, SoundEntity.seSpell, Skillnum)

            If increment Then
                SetPetVital(Index, VitalType.HP, GetPetVital(Index, VitalType.HP) + Damage)

                If Skill(Skillnum).Duration > 0 Then
                    AddHoT_Pet(Index, Skillnum)
                End If

            ElseIf Not increment Then
                If Vital = VitalType.HP Then
                    SetPetVital(Index, VitalType.HP, GetPetVital(Index, VitalType.HP) - Damage)
                ElseIf Vital = VitalType.MP Then
                    SetPetVital(Index, VitalType.MP, GetPetVital(Index, VitalType.MP) - Damage)
                End If
            End If
        End If

        If GetPetVital(Index, VitalType.HP) > GetPetMaxVital(Index, VitalType.HP) Then SetPetVital(Index, VitalType.HP, GetPetMaxVital(Index, VitalType.HP))

        If GetPetVital(Index, VitalType.MP) > GetPetMaxVital(Index, VitalType.MP) Then SetPetVital(Index, VitalType.MP, GetPetMaxVital(Index, VitalType.MP))

    End Sub

    Friend Sub AddHoT_Pet(index as integer, skillnum As Integer)
        Dim i As Integer

        For i = 1 To MAX_DOTS
            With TempPlayer(Index).PetHoT(i)

                If .Skill = Skillnum Then
                    .Timer = GetTimeMs()
                    .StartTime = GetTimeMs()
                    Exit Sub
                End If

                If .Used = False Then
                    .Skill = Skillnum
                    .Timer = GetTimeMs()
                    .Used = True
                    .StartTime = GetTimeMs()
                    Exit Sub
                End If
            End With
        Next

    End Sub

    Friend Sub AddDoT_Pet(index as integer, skillnum As Integer, caster As Integer, attackerType As Integer)
        Dim i As Integer

        If Not PetAlive(Index) Then Exit Sub

        For i = 1 To MAX_DOTS
            With TempPlayer(Index).PetDoT(i)
                If .Skill = Skillnum Then
                    .Timer = GetTimeMs()
                    .Caster = Caster
                    .StartTime = GetTimeMs()
                    .AttackerType = AttackerType
                    Exit Sub
                End If

                If .Used = False Then
                    .Skill = Skillnum
                    .Timer = GetTimeMs()
                    .Caster = Caster
                    .Used = True
                    .StartTime = GetTimeMs()
                    .AttackerType = AttackerType
                    Exit Sub
                End If
            End With
        Next

    End Sub

    Sub PetAttackPlayer(attacker As Integer, victim As Integer, damage As Integer, Optional skillNum As Integer = 0)
        Dim exp As Integer, n As Integer, i As Integer

        ' Check for subscript out of range

        If IsPlaying(Attacker) = False OrElse IsPlaying(Victim) = False OrElse Damage < 0 OrElse PetAlive(Attacker) = False Then
            Exit Sub
        End If

        ' Check for weapon
        n = 0 'No Weapon, PET!

        If SkillNum = 0 Then
            ' Send this packet so they can see the pet attacking
            SendPetAttack(Attacker, Victim)
        End If

        ' set the regen timer
        TempPlayer(Attacker).PetstopRegen = True
        TempPlayer(Attacker).PetstopRegenTimer = GetTimeMs()

        If Damage >= GetPlayerVital(Victim, VitalType.HP) Then
            SendActionMsg(GetPlayerMap(Victim), "-" & GetPlayerVital(Victim, VitalType.HP), ColorType.BrightRed, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))

            ' send the sound
            'If SkillNum > 0 Then SendMapSound(Victim, GetPlayerX(Victim), GetPlayerY(Victim), SoundEntity.seSpell, SkillNum)

            ' Player is dead
            GlobalMsg(GetPlayerName(Victim) & " has been killed by " & GetPlayerName(Attacker) & "'s " & Trim$(GetPetName(Attacker)) & ".")

            ' Calculate exp to give attacker
            Exp = (GetPlayerExp(Victim) \ 10)

            ' Make sure we dont get less then 0
            If Exp < 0 Then
                Exp = 0
            End If

            If Exp = 0 Then
                PlayerMsg(Victim, "You lost no exp.", ColorType.BrightGreen)
                PlayerMsg(Attacker, "You received no exp.", ColorType.BrightRed)
            Else
                SetPlayerExp(Victim, GetPlayerExp(Victim) - Exp)
                SendExp(Victim)
                PlayerMsg(Victim, "You lost " & Exp & " exp.", ColorType.BrightRed)

                ' check if we're in a party
                If TempPlayer(Attacker).InParty > 0 Then
                    ' pass through party exp share function
                    Party_ShareExp(TempPlayer(Attacker).InParty, Exp, Attacker, GetPlayerMap(Attacker))
                Else
                    ' not in party, get exp for self
                    GivePlayerEXP(Attacker, Exp)
                End If
            End If

            ' purge target info of anyone who targetted dead guy
            For i = 1 To Socket.HighIndex

                If IsPlaying(i) AndAlso Socket.IsConnected(i) Then
                    If GetPlayerMap(i) = GetPlayerMap(Attacker) Then
                        If TempPlayer(i).TargetType = TargetType.Player Then
                            If TempPlayer(i).Target = Victim Then
                                TempPlayer(i).Target = 0
                                TempPlayer(i).TargetType = TargetType.None
                                SendTarget(i, 0, TargetType.None)
                            End If
                        End If

                        If Player(i).Character(TempPlayer(i).CurChar).Pet.Alive = 1 Then
                            If TempPlayer(i).PetTargetType = TargetType.Player Then
                                If TempPlayer(i).PetTarget = Victim Then
                                    TempPlayer(i).PetTarget = 0
                                    TempPlayer(i).PetTargetType = TargetType.None
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            If GetPlayerPK(Victim) = 0 Then
                If GetPlayerPK(Attacker) = 0 Then
                    SetPlayerPK(Attacker, 1)
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

            ' send vitals to party if in one
            If TempPlayer(Victim).InParty > 0 Then SendPartyVitals(TempPlayer(Victim).InParty, Victim)

            ' send the sound
            'If SkillNum > 0 Then SendMapSound(Victim, GetPlayerX(Victim), GetPlayerY(Victim), SoundEntity.seSpell, SkillNum)

            SendActionMsg(GetPlayerMap(Victim), "-" & Damage, ColorType.BrightRed, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))
            SendBlood(GetPlayerMap(Victim), GetPlayerX(Victim), GetPlayerY(Victim))

            ' set the regen timer
            TempPlayer(Victim).StopRegen = True
            TempPlayer(Victim).StopRegenTimer = GetTimeMs()

            'if a stunning spell, stun the player
            If SkillNum > 0 Then
                If Skill(SkillNum).StunDuration > 0 Then StunPlayer(Victim, SkillNum)

                ' DoT
                If Skill(SkillNum).Duration > 0 Then
                    'AddDoT_Player(Victim, SkillNum, Attacker)
                End If
            End If
        End If

        ' Reset attack timer
        TempPlayer(Attacker).PetAttackTimer = GetTimeMs()

    End Sub

    Function CanPetAttackPet(attacker As Integer, victim As Integer, Optional isSkill As Integer = 0) As Boolean

        If Not IsSkill Then
            If GetTimeMs() < TempPlayer(Attacker).PetAttackTimer + 1000 Then Exit Function
        End If

        ' Check for subscript out of range
        If Not IsPlaying(Victim) OrElse Not IsPlaying(Attacker) Then Exit Function

        ' Make sure they are on the same map
        If Not GetPlayerMap(Attacker) = GetPlayerMap(Victim) Then Exit Function

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(Victim).GettingMap = 1 Then Exit Function

        If TempPlayer(Attacker).PetskillBuffer.Skill > 0 AndAlso IsSkill = False Then Exit Function

        If Not IsSkill Then

            ' Check if at same coordinates
            Select Case GetPetDir(Attacker)
                Case DirectionType.Up
                    If Not ((GetPetY(Victim) - 1 = GetPetY(Attacker)) AndAlso (GetPetX(Victim) = GetPetX(Attacker))) Then Exit Function

                Case DirectionType.Down
                    If Not ((GetPetY(Victim) + 1 = GetPetY(Attacker)) AndAlso (GetPetX(Victim) = GetPetX(Attacker))) Then Exit Function

                Case DirectionType.Left
                    If Not ((GetPetY(Victim) = GetPetY(Attacker)) AndAlso (GetPetX(Victim) + 1 = GetPetX(Attacker))) Then Exit Function

                Case DirectionType.Right
                    If Not ((GetPetY(Victim) = GetPetY(Attacker)) AndAlso (GetPetX(Victim) - 1 = GetPetX(Attacker))) Then Exit Function

                Case Else
                    Exit Function
            End Select
        End If

        ' Check if map is attackable
        If Not Map(GetPlayerMap(Attacker)).Moral = MapMoralType.None Then
            If GetPlayerPK(Victim) = 0 Then
                Exit Function
            End If
        End If

        ' Make sure they have more then 0 hp
        If Player(Victim).Character(TempPlayer(Victim).CurChar).Pet.Health <= 0 Then Exit Function

        ' Check to make sure that they dont have access
        If GetPlayerAccess(Attacker) > AdminType.Monitor Then
            PlayerMsg(Attacker, "Admins cannot attack other players.", ColorType.BrightRed)
            Exit Function
        End If

        ' Check to make sure the victim isn't an admin
        If GetPlayerAccess(Victim) > AdminType.Monitor Then
            PlayerMsg(Attacker, "You cannot attack " & GetPlayerName(Victim) & "!", ColorType.BrightRed)
            Exit Function
        End If

        ' Don't attack a party member
        If TempPlayer(Attacker).InParty > 0 AndAlso TempPlayer(Victim).InParty > 0 Then
            If TempPlayer(Attacker).InParty = TempPlayer(Victim).InParty Then
                PlayerMsg(Attacker, "You can't attack another party member!", ColorType.BrightRed)
                Exit Function
            End If
        End If

        If TempPlayer(Attacker).InParty > 0 AndAlso TempPlayer(Victim).InParty > 0 AndAlso TempPlayer(Attacker).InParty = TempPlayer(Victim).InParty Then
            If IsSkill > 0 Then
                If Skill(IsSkill).Type = SkillType.HealMp OrElse Skill(IsSkill).Type = SkillType.HealHp Then
                    'Carry On :D
                Else
                    Exit Function
                End If
            Else
                Exit Function
            End If
        End If

        CanPetAttackPet = True

    End Function

    Sub PetAttackPet(attacker As Integer, victim As Integer, damage As Integer, Optional skillnum As Integer = 0)
        Dim exp As Integer, n As Integer, i As Integer

        ' Check for subscript out of range

        If IsPlaying(Attacker) = False OrElse IsPlaying(Victim) = False OrElse Damage < 0 OrElse PetAlive(Attacker) = False OrElse PetAlive(Victim) = False Then
            Exit Sub
        End If

        ' Check for weapon
        n = 0 'No Weapon, PET!

        If Skillnum = 0 Then
            ' Send this packet so they can see the pet attacking
            SendPetAttack(Attacker, Victim)
        End If

        ' set the regen timer
        TempPlayer(Attacker).PetstopRegen = True
        TempPlayer(Attacker).PetstopRegenTimer = GetTimeMs()

        If Damage >= GetPetVital(Victim, VitalType.HP) Then
            SendActionMsg(GetPlayerMap(Victim), "-" & GetPetVital(Victim, VitalType.HP), ColorType.BrightRed, ActionMsgType.Scroll, (GetPetX(Victim) * 32), (GetPetY(Victim) * 32))

            ' send the sound
            'If Spellnum > 0 Then SendMapSound Victim, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.x, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.y, SoundEntity.seSpell, Spellnum

            ' Player is dead
            GlobalMsg(GetPlayerName(Victim) & " has been killed by " & GetPlayerName(Attacker) & "'s " & Trim$(GetPetName(Attacker)) & ".")

            ' Calculate exp to give attacker
            Exp = (GetPlayerExp(Victim) \ 10)

            ' Make sure we dont get less then 0
            If Exp < 0 Then Exp = 0

            If Exp = 0 Then
                PlayerMsg(Victim, "You lost no exp.", ColorType.BrightGreen)
                PlayerMsg(Attacker, "You received no exp.", ColorType.Yellow)
            Else
                SetPlayerExp(Victim, GetPlayerExp(Victim) - Exp)
                SendExp(Victim)
                PlayerMsg(Victim, "You lost " & Exp & " exp.", ColorType.BrightRed)

                ' check if we're in a party
                If TempPlayer(Attacker).InParty > 0 Then
                    ' pass through party exp share function
                    Party_ShareExp(TempPlayer(Attacker).InParty, Exp, Attacker, GetPlayerMap(Attacker))
                Else
                    ' not in party, get exp for self
                    GivePlayerEXP(Attacker, Exp)
                End If
            End If

            ' purge target info of anyone who targetted dead guy
            For i = 1 To Socket.HighIndex

                If IsPlaying(i) AndAlso Socket.IsConnected(i) Then
                    If GetPlayerMap(i) = GetPlayerMap(Attacker) Then
                        If TempPlayer(i).TargetType = TargetType.Player Then
                            If TempPlayer(i).Target = Victim Then
                                TempPlayer(i).Target = 0
                                TempPlayer(i).TargetType = TargetType.None
                                SendTarget(i, 0, TargetType.None)
                            End If
                        End If

                        If PetAlive(i) Then
                            If TempPlayer(i).PetTargetType = TargetType.Player Then
                                If TempPlayer(i).PetTarget = Victim Then
                                    TempPlayer(i).PetTarget = 0
                                    TempPlayer(i).PetTargetType = TargetType.None
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            If GetPlayerPK(Victim) = 0 Then
                If GetPlayerPK(Attacker) = 0 Then
                    SetPlayerPK(Attacker, 1)
                    SendPlayerData(Attacker)
                    GlobalMsg(GetPlayerName(Attacker) & " has been deemed a Player Killer!!!")
                End If
            Else
                GlobalMsg(GetPlayerName(Victim) & " has paid the price for being a Player Killer!!!")
            End If

            ' kill pet
            PlayerMsg(Victim, "Your " & Trim$(GetPetName(Victim)) & " was killed by " & Trim$(GetPlayerName(Attacker)) & "'s " & Trim$(GetPetName(Attacker)) & "!", ColorType.BrightRed)
            ReleasePet(Victim)
        Else
            ' Player not dead, just do the damage
            SetPetVital(Victim, VitalType.HP, GetPetVital(Victim, VitalType.HP) - Damage)
            SendPetVital(Victim, VitalType.HP)

            'Set pet to begin attacking the other pet if it isn't dead or dosent have another target
            If TempPlayer(Victim).PetTarget <= 0 AndAlso TempPlayer(Victim).PetBehavior <> PetBehaviourGoto Then
                TempPlayer(Victim).PetTarget = Attacker
                TempPlayer(Victim).PetTargetType = TargetType.Pet
            End If

            ' send the sound
            'If Spellnum > 0 Then SendMapSound Victim, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.x, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.y, SoundEntity.seSpell, Spellnum

            SendActionMsg(GetPlayerMap(Victim), "-" & Damage, ColorType.BrightRed, 1, (GetPetX(Victim) * 32), (GetPetY(Victim) * 32))
            SendBlood(GetPlayerMap(Victim), GetPetX(Victim), GetPetY(Victim))

            ' set the regen timer
            TempPlayer(Victim).PetstopRegen = True
            TempPlayer(Victim).PetstopRegenTimer = GetTimeMs()

            'if a stunning spell, stun the player
            If Skillnum > 0 Then
                If Skill(Skillnum).StunDuration > 0 Then StunPet(Victim, Skillnum)
                ' DoT
                If Skill(Skillnum).Duration > 0 Then
                    'AddDoT_Pet(Victim, Skillnum, Attacker, TargetType.Pet)
                End If
            End If
        End If

        ' Reset attack timer
        TempPlayer(Attacker).PetAttackTimer = GetTimeMs()

    End Sub

    Friend Sub StunPet(index as integer, skillnum As Integer)
        ' check if it's a stunning spell

        If PetAlive(Index) Then
            If Skill(Skillnum).StunDuration > 0 Then
                ' set the values on index
                TempPlayer(Index).PetStunDuration = Skill(Skillnum).StunDuration
                TempPlayer(Index).PetStunTimer = GetTimeMs()
                ' tell him he's stunned
                PlayerMsg(Index, "Your " & Trim$(GetPetName(Index)) & " has been stunned.", ColorType.Yellow)
            End If
        End If

    End Sub

    Friend Sub HandleDoT_Pet(index as integer, dotNum As Integer)

        With TempPlayer(Index).PetDoT(dotNum)

            If .Used AndAlso .Skill > 0 Then
                ' time to tick?
                If GetTimeMs() > .Timer + (Skill(.Skill).Interval * 1000) Then
                    If .AttackerType = TargetType.Pet Then
                        If CanPetAttackPet(.Caster, Index, .Skill) Then
                            PetAttackPet(.Caster, Index, Skill(.Skill).Vital)
                            SendPetVital(Index, VitalType.HP)
                            SendPetVital(Index, VitalType.MP)
                        End If
                    ElseIf .AttackerType = TargetType.Player Then
                        If CanPlayerAttackPet(.Caster, Index, .Skill) Then
                            PlayerAttackPet(.Caster, Index, Skill(.Skill).Vital)
                            SendPetVital(Index, VitalType.HP)
                            SendPetVital(Index, VitalType.MP)
                        End If
                    End If

                    .Timer = GetTimeMs()

                    ' check if DoT is still active - if player died it'll have been purged
                    If .Used AndAlso .Skill > 0 Then
                        ' destroy DoT if finished
                        If GetTimeMs() - .StartTime >= (Skill(.Skill).Duration * 1000) Then
                            .Used = False
                            .Skill = 0
                            .Timer = 0
                            .Caster = 0
                            .StartTime = 0
                        End If
                    End If
                End If
            End If
        End With

    End Sub

    Friend Sub HandleHoT_Pet(index as integer, hotNum As Integer)

        With TempPlayer(Index).PetHoT(hotNum)

            If .Used AndAlso .Skill > 0 Then
                ' time to tick?
                If GetTimeMs() > .Timer + (Skill(.Skill).Interval * 1000) Then
                    SendActionMsg(GetPlayerMap(Index), "+" & Skill(.Skill).Vital, ColorType.BrightGreen, ActionMsgType.Scroll, Player(Index).Character(TempPlayer(Index).CurChar).Pet.X * 32, Player(Index).Character(TempPlayer(Index).CurChar).Pet.Y * 32,)
                    SetPetVital(Index, VitalType.HP, GetPetVital(Index, VitalType.HP) + Skill(.Skill).Vital)

                    If GetPetVital(Index, VitalType.HP) > GetPetMaxVital(Index, VitalType.HP) Then SetPetVital(Index, VitalType.HP, GetPetMaxVital(Index, VitalType.HP))

                    If GetPetVital(Index, VitalType.MP) > GetPetMaxVital(Index, VitalType.MP) Then SetPetVital(Index, VitalType.MP, GetPetMaxVital(Index, VitalType.MP))

                    SendPetVital(Index, VitalType.HP)
                    SendPetVital(Index, VitalType.MP)
                    .Timer = GetTimeMs()

                    ' check if DoT is still active - if player died it'll have been purged
                    If .Used AndAlso .Skill > 0 Then
                        ' destroy hoT if finished
                        If GetTimeMs() - .StartTime >= (Skill(.Skill).Duration * 1000) Then
                            .Used = False
                            .Skill = 0
                            .Timer = 0
                            .Caster = 0
                            .StartTime = 0
                        End If
                    End If
                End If
            End If
        End With

    End Sub

    Friend Sub TryPetAttackPlayer(index as integer, victim As Integer)
        Dim mapNum as Integer, blockAmount As Integer, damage As Integer

        If GetPlayerMap(Index) <> GetPlayerMap(Victim) Then Exit Sub

        If Not PetAlive(Index) Then Exit Sub

        ' Can the npc attack the player?
        If CanPetAttackPlayer(Index, Victim) Then
            MapNum = GetPlayerMap(Index)

            ' check if PLAYER can avoid the attack
            If CanPlayerDodge(Victim) Then
                SendActionMsg(MapNum, "Dodge!", ColorType.Pink, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))
                Exit Sub
            End If

            If CanPlayerParry(Victim) Then
                SendActionMsg(MapNum, "Parry!", ColorType.Pink, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            Damage = GetPetDamage(Index)

            ' if the player blocks, take away the block amount
            blockAmount = CanPlayerBlockHit(Victim)
            Damage = Damage - blockAmount

            ' take away armour
            Damage = Damage - Random(1, (GetPetStat(Index, StatType.Luck)) * 2)

            ' randomise for up to 10% lower than max hit
            Damage = Random(1, Damage)

            ' * 1.5 if crit hit
            If CanPetCrit(Index) Then
                Damage = Damage * 1.5
                SendActionMsg(MapNum, "Critical!", ColorType.BrightCyan, 1, (GetPetX(Index) * 32), (GetPetY(Index) * 32))
            End If

            If Damage > 0 Then
                PetAttackPlayer(Index, Victim, Damage)
            End If

        End If

    End Sub

    Friend Function CanPetDodge(index as integer) As Boolean
        Dim rate As Integer, rndNum As Integer

        If Not PetAlive(Index) Then Exit Function

        CanPetDodge = False

        rate = GetPetStat(Index, StatType.Luck) / 4
        rndNum = Random(1, 100)

        If rndNum <= rate Then
            CanPetDodge = True
        End If

    End Function

    Friend Function CanPetParry(index as integer) As Boolean
        Dim rate As Integer, rndNum As Integer

        If Not PetAlive(Index) Then Exit Function

        CanPetParry = False

        rate = GetPetStat(Index, StatType.Luck) / 6
        rndNum = Random(1, 100)

        If rndNum <= rate Then
            CanPetParry = True
        End If

    End Function

    Friend Sub TryPetAttackPet(index as integer, victim As Integer)
        Dim mapNum as Integer, blockAmount As Integer, damage As Integer

        If GetPlayerMap(Index) <> GetPlayerMap(Victim) Then Exit Sub

        If Not PetAlive(Index) OrElse Not PetAlive(Victim) Then Exit Sub

        ' Can the npc attack the player?
        If CanPetAttackPet(Index, Victim) Then
            MapNum = GetPlayerMap(Index)

            ' check if Pet can avoid the attack
            If CanPetDodge(Victim) Then
                SendActionMsg(MapNum, "Dodge!", ColorType.Pink, 1, (GetPetX(Victim) * 32), (GetPetY(Victim) * 32))
                Exit Sub
            End If

            If CanPetParry(Victim) Then
                SendActionMsg(MapNum, "Parry!", ColorType.Pink, 1, (GetPetX(Victim) * 32), (GetPetY(Victim) * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            Damage = GetPetDamage(Index)

            ' if the player blocks, take away the block amount
            Damage = Damage - blockAmount

            ' take away armour
            Damage = Damage - Random(1, (Player(Index).Character(TempPlayer(Index).CurChar).Pet.Stat(StatType.Luck) * 2))

            ' randomise for up to 10% lower than max hit
            Damage = Random(1, Damage)

            ' * 1.5 if crit hit
            If CanPetCrit(Index) Then
                Damage = Damage * 1.5
                SendActionMsg(MapNum, "Critical!", ColorType.BrightCyan, 1, (GetPetX(Index) * 32), (GetPetY(Index) * 32))
            End If

            If Damage > 0 Then
                PetAttackPet(Index, Victim, Damage)
            End If

        End If

    End Sub

    Function CanPlayerAttackPet(attacker As Integer, victim As Integer, Optional isSkill As Boolean = False) As Boolean

        If IsSkill = False Then
            ' Check attack timer
            If GetPlayerEquipment(Attacker, EquipmentType.Weapon) > 0 Then
                If GetTimeMs() < TempPlayer(Attacker).AttackTimer + Item(GetPlayerEquipment(Attacker, EquipmentType.Weapon)).Speed Then Exit Function
            Else
                If GetTimeMs() < TempPlayer(Attacker).AttackTimer + 1000 Then Exit Function
            End If
        End If

        ' Check for subscript out of range
        If Not IsPlaying(Victim) Then Exit Function

        If Not PetAlive(Victim) Then Exit Function

        ' Make sure they are on the same map
        If Not GetPlayerMap(Attacker) = GetPlayerMap(Victim) Then Exit Function

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(Victim).GettingMap = 1 Then Exit Function

        If IsSkill = False Then

            ' Check if at same coordinates
            Select Case GetPlayerDir(Attacker)

                Case DirectionType.Up
                    If Not ((GetPetY(Victim) + 1 = GetPlayerY(Attacker)) AndAlso (GetPetX(Victim) = GetPlayerX(Attacker))) Then Exit Function

                Case DirectionType.Down
                    If Not ((GetPetY(Victim) - 1 = GetPlayerY(Attacker)) AndAlso (GetPetX(Victim) = GetPlayerX(Attacker))) Then Exit Function

                Case DirectionType.Left
                    If Not ((GetPetY(Victim) = GetPlayerY(Attacker)) AndAlso (GetPetX(Victim) + 1 = GetPlayerX(Attacker))) Then Exit Function

                Case DirectionType.Right
                    If Not ((GetPetY(Victim) = GetPlayerY(Attacker)) AndAlso (GetPetX(Victim) - 1 = GetPlayerX(Attacker))) Then Exit Function

                Case Else
                    Exit Function
            End Select
        End If

        ' Check if map is attackable
        If Not Map(GetPlayerMap(Attacker)).Moral = MapMoralType.None Then
            If GetPlayerPK(Victim) = 0 Then
                PlayerMsg(Attacker, "This is a safe zone!", ColorType.Yellow)
                Exit Function
            End If
        End If

        ' Make sure they have more then 0 hp
        If GetPetVital(Victim, VitalType.HP) <= 0 Then Exit Function

        ' Check to make sure that they dont have access
        If GetPlayerAccess(Attacker) > AdminType.Monitor Then
            PlayerMsg(Attacker, "Admins cannot attack other players.", ColorType.BrightRed)
            Exit Function
        End If

        ' Check to make sure the victim isn't an admin
        If GetPlayerAccess(Victim) > AdminType.Monitor Then
            PlayerMsg(Attacker, "You cannot attack " & GetPlayerName(Victim) & "s " & Trim$(GetPetName(Victim)) & "!", ColorType.BrightRed)
            Exit Function
        End If

        ' Don't attack a party member
        If TempPlayer(Attacker).InParty > 0 AndAlso TempPlayer(Victim).InParty > 0 Then
            If TempPlayer(Attacker).InParty = TempPlayer(Victim).InParty Then
                PlayerMsg(Attacker, "You can't attack another party member!", ColorType.BrightRed)
                Exit Function
            End If
        End If

        If TempPlayer(Attacker).InParty > 0 AndAlso TempPlayer(Victim).InParty > 0 AndAlso TempPlayer(Attacker).InParty = TempPlayer(Victim).InParty Then
            If IsSkill > 0 Then
                If Skill(IsSkill).Type = SkillType.HealMp OrElse Skill(IsSkill).Type = SkillType.HealHp Then
                    'Carry On :D
                Else
                    Exit Function
                End If
            Else
                Exit Function
            End If
        End If

        CanPlayerAttackPet = True

    End Function

    Sub PlayerAttackPet(attacker As Integer, victim As Integer, damage As Integer, Optional skillnum As Integer = 0)
        Dim exp As Integer, n As Integer, i As Integer

        ' Check for subscript out of range

        If IsPlaying(Attacker) = False OrElse IsPlaying(Victim) = False OrElse Damage < 0 OrElse Not PetAlive(Victim) Then Exit Sub
        ' Check for weapon
        n = 0

        If GetPlayerEquipment(Attacker, EquipmentType.Weapon) > 0 Then
            n = GetPlayerEquipment(Attacker, EquipmentType.Weapon)
        End If

        ' set the regen timer
        TempPlayer(Attacker).StopRegen = True
        TempPlayer(Attacker).StopRegenTimer = GetTimeMs()

        If Damage >= GetPetVital(Victim, VitalType.HP) Then
            SendActionMsg(GetPlayerMap(Victim), "-" & GetPetVital(Victim, VitalType.HP), ColorType.BrightRed, 1, (GetPetX(Victim) * 32), (GetPetY(Victim) * 32))

            ' send the sound
            'If Spellnum > 0 Then SendMapSound Victim, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.x, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.y, SoundEntity.seSpell, Spellnum

            ' Calculate exp to give attacker
            Exp = (GetPlayerExp(Victim) \ 10)

            ' Make sure we dont get less then 0
            If Exp < 0 Then Exp = 0

            If Exp = 0 Then
                PlayerMsg(Victim, "You lost no exp.", ColorType.BrightGreen)
                PlayerMsg(Attacker, "You received no exp.", ColorType.Yellow)
            Else
                SetPlayerExp(Victim, GetPlayerExp(Victim) - Exp)
                SendExp(Victim)
                PlayerMsg(Victim, "You lost " & Exp & " exp.", ColorType.BrightRed)

                ' check if we're in a party
                If TempPlayer(Attacker).InParty > 0 Then
                    ' pass through party exp share function
                    Party_ShareExp(TempPlayer(Attacker).InParty, Exp, Attacker, GetPlayerMap(Attacker))
                Else
                    ' not in party, get exp for self
                    GivePlayerEXP(Attacker, Exp)
                End If
            End If

            ' purge target info of anyone who targetted dead guy
            For i = 1 To GetPlayersOnline()
                If IsPlaying(i) AndAlso Socket.IsConnected(i) AndAlso GetPlayerMap(i) = GetPlayerMap(Attacker) Then
                    If TempPlayer(i).Target = TargetType.Pet AndAlso TempPlayer(i).Target = Victim Then
                        TempPlayer(i).Target = 0
                        TempPlayer(i).TargetType = TargetType.None
                        SendTarget(i, 0, TargetType.None)
                    End If
                End If
            Next

            PlayerMsg(Victim, ("Your " & Trim$(GetPetName(Victim)) & " was killed by  " & Trim$(GetPlayerName(Attacker)) & "."), ColorType.BrightRed)
            ReCallPet(Victim)
        Else
            ' Pet not dead, just do the damage
            SetPetVital(Victim, VitalType.HP, GetPetVital(Victim, VitalType.HP) - Damage)
            SendPetVital(Victim, VitalType.HP)

            'Set pet to begin attacking the other pet if it isn't dead or dosent have another target
            If TempPlayer(Victim).PetTarget <= 0 AndAlso TempPlayer(Victim).PetBehavior <> PetBehaviourGoto Then
                TempPlayer(Victim).PetTarget = Attacker
                TempPlayer(Victim).PetTargetType = TargetType.Player
            End If

            ' send the sound
            'If Spellnum > 0 Then SendMapSound Victim, GetPetX(Victim), GetPety(Victim), SoundEntity.seSpell, Spellnum

            SendActionMsg(GetPlayerMap(Victim), "-" & Damage, ColorType.BrightRed, 1, (GetPetX(Victim) * 32), (GetPetY(Victim) * 32))
            SendBlood(GetPlayerMap(Victim), GetPetX(Victim), GetPetY(Victim))

            ' set the regen timer
            TempPlayer(Victim).PetstopRegen = True
            TempPlayer(Victim).PetstopRegenTimer = GetTimeMs()

            'if a stunning spell, stun the player
            If Skillnum > 0 Then
                If Skill(Skillnum).StunDuration > 0 Then StunPet(Victim, Skillnum)

                ' DoT
                If Skill(Skillnum).Duration > 0 Then
                    AddDoT_Pet(Victim, Skillnum, Attacker, TargetType.Player)
                End If
            End If
        End If

        ' Reset attack timer
        TempPlayer(Attacker).AttackTimer = GetTimeMs()

    End Sub

    Function IsPetByPlayer(index as integer) As Boolean
        Dim x As Integer, y As Integer, x1 As Integer, y1 As Integer

        If Index <= 0 OrElse Index > MAX_PLAYERS OrElse Not PetAlive(Index) Then Exit Function

        IsPetByPlayer = False

        x = GetPlayerX(Index)
        y = GetPlayerY(Index)
        x1 = GetPetX(Index)
        y1 = GetPetY(Index)

        If x = x1 Then
            If y = y1 + 1 OrElse y = y1 - 1 Then
                IsPetByPlayer = True
            End If
        ElseIf y = y1 Then
            If x = x1 - 1 OrElse x = x1 + 1 Then
                IsPetByPlayer = True
            End If
        End If

    End Function

    Function GetPetVitalRegen(index as integer, vital As VitalType) As Integer
        Dim i As Integer

        If Index <= 0 OrElse Index > MAX_PLAYERS OrElse Not PetAlive(Index) Then
            GetPetVitalRegen = 0
            Exit Function
        End If

        Select Case Vital
            Case VitalType.HP
                i = (GetPlayerStat(Index, StatType.Spirit) * 0.8) + 6

            Case VitalType.MP
                i = (GetPlayerStat(Index, StatType.Spirit) / 4) + 12.5
        End Select

        GetPetVitalRegen = i

    End Function

    Friend Sub TryPlayerAttackPet(attacker As Integer, victim As Integer)
        Dim blockAmount As Integer, mapNum as Integer
        Dim damage As Integer

        Damage = 0

        If Not PetAlive(Victim) Then Exit Sub

        ' Can we attack the npc?
        If CanPlayerAttackPet(Attacker, Victim) Then

            MapNum = GetPlayerMap(Attacker)

            TempPlayer(Attacker).Target = Victim
            TempPlayer(Attacker).TargetType = TargetType.Pet

            ' check if NPC can avoid the attack
            If CanPetDodge(Victim) Then
                SendActionMsg(MapNum, "Dodge!", ColorType.Pink, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))
                Exit Sub
            End If

            If CanPetParry(Victim) Then
                SendActionMsg(MapNum, "Parry!", ColorType.Pink, 1, (GetPlayerX(Victim) * 32), (GetPlayerY(Victim) * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            Damage = GetPlayerDamage(Attacker)

            ' if the npc blocks, take away the block amount
            blockAmount = 0
            Damage = Damage - blockAmount

            ' take away armour
            Damage = Damage - Random(1, (GetPlayerStat(Victim, StatType.Luck) * 2))

            ' randomise for up to 10% lower than max hit
            Damage = Random(1, Damage)

            ' * 1.5 if can crit
            If CanPlayerCriticalHit(Attacker) Then
                Damage = Damage * 1.5
                SendActionMsg(MapNum, "Critical!", ColorType.BrightCyan, 1, (GetPlayerX(Attacker) * 32), (GetPlayerY(Attacker) * 32))
            End If

            If Damage > 0 Then
                PlayerAttackPet(Attacker, Victim, Damage)
            Else
                PlayerMsg(Attacker, "Your attack does nothing.", ColorType.BrightRed)
            End If
        End If

    End Sub

    Sub CheckPetLevelUp(index as integer)
        Dim expRollover As Integer, levelCount As Integer

        levelCount = 0

        Do While GetPetExp(Index) >= GetPetNextLevel(Index)
            expRollover = GetPetExp(Index) - GetPetNextLevel(Index)

            ' can level up?
            If GetPetLevel(Index) < 99 AndAlso GetPetLevel(Index) < Pet(Player(Index).Character(TempPlayer(Index).CurChar).Pet.Num).MaxLevel Then
                SetPetLevel(Index, GetPetLevel(Index) + 1)
            End If

            SetPetPoints(Index, GetPetPoints(Index) + Pet(Player(Index).Character(TempPlayer(Index).CurChar).Pet.Num).LevelPnts)
            SetPetExp(Index, expRollover)
            levelCount = levelCount + 1
        Loop

        If levelCount > 0 Then
            If levelCount = 1 Then
                'singular
                PlayerMsg(Index, "Your " & Trim$(GetPetName(Index)) & " has gained " & levelCount & " level!", ColorType.BrightGreen)
            Else
                'plural
                PlayerMsg(Index, "Your " & Trim$(GetPetName(Index)) & " has gained " & levelCount & " levels!", ColorType.BrightGreen)
            End If

            SendPlayerData(Index)

        End If

    End Sub

    Friend Sub PetFireProjectile(index as integer, spellnum As Integer)
        Dim projectileSlot As Integer, projectileNum As Integer
        Dim mapNum as Integer, i As Integer

        ' Prevent subscript out of range

        MapNum = GetPlayerMap(Index)

        'Find a free projectile
        For i = 1 To MAX_PROJECTILES
            If MapProjectiles(MapNum, i).ProjectileNum = 0 Then ' Free Projectile
                ProjectileSlot = i
                Exit For
            End If
        Next

        'Check for no projectile, if so just overwrite the first slot
        If ProjectileSlot = 0 Then ProjectileSlot = 1

        If Spellnum < 1 OrElse Spellnum > MAX_SKILLS Then Exit Sub

        ProjectileNum = Skill(Spellnum).Projectile

        With MapProjectiles(MapNum, ProjectileSlot)
            .ProjectileNum = ProjectileNum
            .Owner = Index
            .OwnerType = TargetType.Pet
            .Dir = Player(i).Character(TempPlayer(i).CurChar).Pet.Dir
            .X = Player(i).Character(TempPlayer(i).CurChar).Pet.X
            .Y = Player(i).Character(TempPlayer(i).CurChar).Pet.Y
            .Timer = GetTimeMs() + 60000
        End With

        SendProjectileToMap(MapNum, ProjectileSlot)

    End Sub

#Region "Data Functions"
    Friend Function PetAlive(index as integer) As Boolean
        PetAlive = False

        If Player(Index).Character(TempPlayer(Index).CurChar).Pet.Alive = 1 Then
            PetAlive = True
        End If

    End Function

    Friend Function GetPetName(index as integer) As String
        GetPetName = ""

        If PetAlive(Index) Then
            GetPetName = Pet(Player(Index).Character(TempPlayer(Index).CurChar).Pet.Num).Name
        End If

    End Function

    Friend Function GetPetNum(index as integer) As Integer
        GetPetNum = 0

        GetPetNum = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Num

    End Function

    Friend Function GetPetRange(index as integer) As Integer
        GetPetRange = 0

        If PetAlive(Index) Then
            GetPetRange = Pet(Player(Index).Character(TempPlayer(Index).CurChar).Pet.Num).Range
        End If

    End Function

    Friend Function GetPetLevel(index as integer) As Integer
        GetPetLevel = 0

        If PetAlive(Index) Then
            GetPetLevel = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level
        End If

    End Function

    Friend Sub SetPetLevel(index as integer, newlvl As Integer)
        If PetAlive(Index) Then
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level = Newlvl
        End If
    End Sub

    Friend Function GetPetX(index as integer) As Integer
        GetPetX = 0

        If PetAlive(Index) Then
            GetPetX = Player(Index).Character(TempPlayer(Index).CurChar).Pet.X
        End If

    End Function

    Friend Sub SetPetX(index as integer, x As Integer)
        If PetAlive(Index) Then
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.X = X
        End If
    End Sub

    Friend Function GetPetY(index as integer) As Integer
        GetPetY = 0

        If PetAlive(Index) Then
            GetPetY = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Y
        End If

    End Function

    Friend Sub SetPetY(index as integer, y As Integer)
        If PetAlive(Index) Then
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Y = Y
        End If
    End Sub

    Friend Function GetPetDir(index as integer) As Integer
        GetPetDir = 0

        If PetAlive(Index) Then
            GetPetDir = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Dir
        End If

    End Function

    Friend Function GetPetBehaviour(index as integer) As Integer
        GetPetBehaviour = 0

        If PetAlive(Index) Then
            GetPetBehaviour = Player(Index).Character(TempPlayer(Index).CurChar).Pet.AttackBehaviour
        End If

    End Function

    Friend Sub SetPetBehaviour(index as integer, behaviour As Byte)
        If PetAlive(Index) Then
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.AttackBehaviour = Behaviour
        End If
    End Sub

    Friend Function GetPetStat(index as integer, stat As StatType) As Integer
        GetPetStat = 0

        If PetAlive(Index) Then
            GetPetStat = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Stat(Stat)
        End If

    End Function

    Friend Sub SetPetStat(index as integer, stat As StatType, amount As Integer)

        If PetAlive(Index) Then
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Stat(Stat) = Amount
        End If

    End Sub

    Friend Function GetPetPoints(index as integer) As Integer
        GetPetPoints = 0

        If PetAlive(Index) Then
            GetPetPoints = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Points
        End If

    End Function

    Friend Sub SetPetPoints(index as integer, amount As Integer)

        If PetAlive(Index) Then
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Points = Amount
        End If

    End Sub

    Friend Function GetPetExp(index as integer) As Integer
        GetPetExp = 0

        If PetAlive(Index) Then
            GetPetExp = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Exp
        End If

    End Function

    Friend Sub SetPetExp(index as integer, amount As Integer)
        If PetAlive(Index) Then
            Player(Index).Character(TempPlayer(Index).CurChar).Pet.Exp = Amount
        End If
    End Sub

    Function GetPetVital(index as integer, vital As VitalType) As Integer

        If Index > MAX_PLAYERS Then Exit Function

        Select Case Vital
            Case VitalType.HP
                GetPetVital = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Health

            Case VitalType.MP
                GetPetVital = Player(Index).Character(TempPlayer(Index).CurChar).Pet.Mana
        End Select

    End Function

    Sub SetPetVital(index as integer, vital As VitalType, amount As Integer)

        If Index > MAX_PLAYERS Then Exit Sub

        Select Case Vital
            Case VitalType.HP
                Player(Index).Character(TempPlayer(Index).CurChar).Pet.Health = Amount

            Case VitalType.MP
                Player(Index).Character(TempPlayer(Index).CurChar).Pet.Mana = Amount
        End Select

    End Sub

    Function GetPetMaxVital(index as integer, vital As VitalType) As Integer

        If Index > MAX_PLAYERS Then Exit Function

        Select Case Vital
            Case VitalType.HP
                GetPetMaxVital = ((Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level * 4) + (Player(Index).Character(TempPlayer(Index).CurChar).Pet.Stat(StatType.Endurance) * 10)) + 150

            Case VitalType.MP
                GetPetMaxVital = ((Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level * 4) + (Player(Index).Character(TempPlayer(Index).CurChar).Pet.Stat(StatType.Spirit) / 2)) * 5 + 50
        End Select

    End Function

    Function GetPetNextLevel(index as integer) As Integer

        If PetAlive(Index) Then
            If Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level = Pet(Player(Index).Character(TempPlayer(Index).CurChar).Pet.Num).MaxLevel Then GetPetNextLevel = 0 : Exit Function
            GetPetNextLevel = (50 / 3) * ((Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level + 1) ^ 3 - (6 * (Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level + 1) ^ 2) + 17 * (Player(Index).Character(TempPlayer(Index).CurChar).Pet.Level + 1) - 12)
        End If

    End Function
#End Region

End Module