Imports System.IO
Imports ASFW
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

    Friend givePetHpTimer As Integer

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

        filename = Path.Pet(petNum)

        Dim writer As New ByteStream(100)

        writer.WriteInt32(Pet(petNum).Num)
        writer.WriteString(Pet(petNum).Name.Trim)
        writer.WriteInt32(Pet(petNum).Sprite)
        writer.WriteInt32(Pet(petNum).Range)
        writer.WriteInt32(Pet(petNum).Level)
        writer.WriteInt32(Pet(petNum).MaxLevel)
        writer.WriteInt32(Pet(petNum).ExpGain)
        writer.WriteInt32(Pet(petNum).LevelPnts)

        writer.WriteByte(Pet(petNum).StatType)
        writer.WriteByte(Pet(petNum).LevelingType)

        For i = 1 To StatType.Count - 1
            writer.WriteByte(Pet(petNum).Stat(i))
        Next

        For i = 1 To 4
            writer.WriteInt32(Pet(petNum).Skill(i))
        Next

        writer.WriteByte(Pet(petNum).Evolvable)
        writer.WriteInt32(Pet(petNum).EvolveLevel)
        writer.WriteInt32(Pet(petNum).EvolveNum)

        BinaryFile.Save(filename, writer)

    End Sub

    Sub LoadPets()
        Dim i As Integer

        ClearPets()
        CheckPets()

        For i = 1 To MAX_PETS
            LoadPet(i)
        Next
        'SavePets()
    End Sub

    Sub LoadPet(petNum As Integer)
        Dim reader As New ByteStream()
        Dim filename As String, i As Integer

        filename = Path.Pet(petNum)

        BinaryFile.Load(filename, reader)

        Pet(petNum).Num = reader.ReadInt32()
        Pet(petNum).Name = reader.ReadString()
        Pet(petNum).Sprite = reader.ReadInt32()
        Pet(petNum).Range = reader.ReadInt32()
        Pet(petNum).Level = reader.ReadInt32()
        Pet(petNum).MaxLevel = reader.ReadInt32()
        Pet(petNum).ExpGain = reader.ReadInt32()
        Pet(petNum).LevelPnts = reader.ReadInt32()

        Pet(petNum).StatType = reader.ReadByte()
        Pet(petNum).LevelingType = reader.ReadByte()

        ReDim Pet(petNum).Stat(StatType.Count - 1)
        For i = 1 To StatType.Count - 1
            Pet(petNum).Stat(i) = reader.ReadByte()
        Next

        ReDim Pet(petNum).Skill(4)
        For i = 1 To 4
            Pet(petNum).Skill(i) = reader.ReadInt32()
        Next

        Pet(petNum).Evolvable = reader.ReadByte()
        Pet(petNum).EvolveLevel = reader.ReadInt32()
        Pet(petNum).EvolveNum = reader.ReadInt32()

    End Sub

    Sub CheckPets()
        For i = 1 To MAX_PETS
            If Not File.Exists(Application.StartupPath & "\Data\pets\pet" & i & ".dat") Then
                SavePet(i)
            End If
        Next
    End Sub

    Sub ClearPet(petNum As Integer)

        Pet(petNum).Name = ""

        ReDim Pet(petNum).Stat(StatType.Count - 1)
        ReDim Pet(petNum).Skill(4)
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

    Sub SendPets(index As Integer)
        Dim i As Integer

        For i = 1 To MAX_PETS
            If Pet(i).Name.Length > 0 Then
                SendUpdatePetTo(index, i)
            End If
        Next

    End Sub

    Sub SendUpdatePetToAll(petNum As Integer)
        Dim buffer = New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SUpdatePet)

        buffer.WriteInt32(petNum)

        With Pet(petNum)
            buffer.WriteInt32(.Num)
            buffer.WriteString(.Name.Trim)
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
            buffer.WriteString(.Name.Trim)
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

        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub

    Friend Sub SendUpdatePlayerPet(index As Integer, ownerOnly As Boolean)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SUpdatePlayerPet)

        buffer.WriteInt32(index)

        buffer.WriteInt32(GetPetNum(index))
        buffer.WriteInt32(GetPetVital(index, VitalType.HP))
        buffer.WriteInt32(GetPetVital(index, VitalType.MP))
        buffer.WriteInt32(GetPetLevel(index))

        For i = 1 To StatType.Count - 1
            buffer.WriteInt32(GetPetStat(index, i))
        Next

        For i = 1 To 4
            buffer.WriteInt32(Player(index).Character(TempPlayer(index).CurChar).Pet.Skill(i))
        Next

        buffer.WriteInt32(GetPetX(index))
        buffer.WriteInt32(GetPetY(index))
        buffer.WriteInt32(GetPetDir(index))

        buffer.WriteInt32(GetPetMaxVital(index, VitalType.HP))
        buffer.WriteInt32(GetPetMaxVital(index, VitalType.MP))

        buffer.WriteInt32(Player(index).Character(TempPlayer(index).CurChar).Pet.Alive)

        buffer.WriteInt32(GetPetBehaviour(index))
        buffer.WriteInt32(GetPetPoints(index))
        buffer.WriteInt32(GetPetExp(index))
        buffer.WriteInt32(GetPetNextLevel(index))

        If ownerOnly Then
            Socket.SendDataTo(index, buffer.Data, buffer.Head)
        Else
            SendDataToMap(GetPlayerMap(index), buffer.Data, buffer.Head)
        End If

        buffer.Dispose()
    End Sub

    Sub SendPetAttack(index As Integer, mapNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SPetAttack)
        buffer.WriteInt32(index)
        SendDataToMap(mapNum, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendPetXy(index As Integer, x As Integer, y As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SPetXY)
        buffer.WriteInt32(index)
        buffer.WriteInt32(x)
        buffer.WriteInt32(y)
        SendDataToMap(GetPlayerMap(index), buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendPetExp(index As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SPetExp)
        buffer.WriteInt32(GetPetExp(index))
        buffer.WriteInt32(GetPetNextLevel(index))
        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendPetVital(index As Integer, vital As VitalType)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SPetVital)

        buffer.WriteInt32(index)

        If vital = VitalType.HP Then
            buffer.WriteInt32(1)
        ElseIf vital = VitalType.MP Then
            buffer.WriteInt32(2)
        End If

        Select Case vital
            Case VitalType.HP
                buffer.WriteInt32(GetPetMaxVital(index, VitalType.HP))
                buffer.WriteInt32(GetPetVital(index, VitalType.HP))

            Case VitalType.MP
                buffer.WriteInt32(GetPetMaxVital(index, VitalType.MP))
                buffer.WriteInt32(GetPetVital(index, VitalType.MP))
        End Select

        SendDataToMap(GetPlayerMap(index), buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub

    Sub SendClearPetSpellBuffer(index As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SClearPetSkillBuffer)

        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub

#End Region

#Region "Incoming Packets"

    Sub Packet_RequestEditPet(index As Integer, ByRef data() As Byte)
        Dim buffer = New ByteStream(4)

        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        buffer.WriteInt32(ServerPackets.SPetEditor)
        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub

    Sub Packet_SavePet(index As Integer, ByRef data() As Byte)
        Dim petNum As Integer, i As Integer
        Dim buffer As New ByteStream(data)

        ' Prevent hacking
        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        petNum = buffer.ReadInt32

        ' Prevent hacking
        If petNum < 0 OrElse petNum > MAX_PETS Then Exit Sub

        With Pet(petNum)
            .Num = buffer.ReadInt32
            .Name = buffer.ReadString
            .Sprite = buffer.ReadInt32
            .Range = buffer.ReadInt32
            .Level = buffer.ReadInt32
            .MaxLevel = buffer.ReadInt32
            .ExpGain = buffer.ReadInt32
            .LevelPnts = buffer.ReadInt32
            .StatType = buffer.ReadInt32
            .LevelingType = buffer.ReadInt32

            For i = 1 To StatType.Count - 1
                .Stat(i) = buffer.ReadInt32
            Next

            For i = 1 To 4
                .Skill(i) = buffer.ReadInt32
            Next

            .Evolvable = buffer.ReadInt32
            .EvolveLevel = buffer.ReadInt32
            .EvolveNum = buffer.ReadInt32
        End With

        ' Save it
        SendUpdatePetToAll(petNum)
        SavePet(petNum)
        Addlog(GetPlayerLogin(index) & " saved Pet #" & petNum & ".", ADMIN_LOG)
        SendPets(index)
    End Sub

    Sub Packet_RequestPets(index As Integer, ByRef data() As Byte)

        SendPets(index)

    End Sub

    Sub Packet_SummonPet(index As Integer, ByRef data() As Byte)
        If PetAlive(index) Then
            ReCallPet(index)
        Else
            SummonPet(index)
        End If
    End Sub

    Sub Packet_PetMove(index As Integer, ByRef data() As Byte)
        Dim x As Integer, y As Integer, i As Integer
        Dim buffer As New ByteStream(data)
        x = buffer.ReadInt32
        y = buffer.ReadInt32

        ' Prevent subscript out of range
        If x < 0 OrElse x > Map(GetPlayerMap(index)).MaxX OrElse y < 0 OrElse y > Map(GetPlayerMap(index)).MaxY Then Exit Sub

        ' Check for a player
        For i = 1 To GetPlayersOnline()

            If IsPlaying(i) Then
                If GetPlayerMap(index) = GetPlayerMap(i) AndAlso GetPlayerX(i) = x AndAlso GetPlayerY(i) = y Then
                    If i = index Then
                        ' Change target
                        If TempPlayer(index).PetTargetType = TargetType.Player AndAlso TempPlayer(index).PetTarget = i Then
                            TempPlayer(index).PetTarget = 0
                            TempPlayer(index).PetTargetType = TargetType.None
                            TempPlayer(index).PetBehavior = PetBehaviourGoto
                            TempPlayer(index).GoToX = x
                            TempPlayer(index).GoToY = y
                            ' send target to player
                            PlayerMsg(index, "Your pet is no longer following you.", ColorType.BrightGreen)
                        Else
                            TempPlayer(index).PetTarget = i
                            TempPlayer(index).PetTargetType = TargetType.Player
                            ' send target to player
                            TempPlayer(index).PetBehavior = PetBehaviourFollow
                            PlayerMsg(index, "Your " & GetPetName(index).Trim & " is now following you.", ColorType.BrightGreen)
                        End If
                    Else
                        ' Change target
                        If TempPlayer(index).PetTargetType = TargetType.Player AndAlso TempPlayer(index).PetTarget = i Then
                            TempPlayer(index).PetTarget = 0
                            TempPlayer(index).PetTargetType = TargetType.None
                            ' send target to player
                            PlayerMsg(index, "Your pet is no longer targetting " & GetPlayerName(i).Trim & ".", ColorType.BrightGreen)
                        Else
                            TempPlayer(index).PetTarget = i
                            TempPlayer(index).PetTargetType = TargetType.Player
                            ' send target to player
                            PlayerMsg(index, "Your pet is now targetting " & GetPlayerName(i).Trim & ".", ColorType.BrightGreen)
                        End If
                    End If
                    Exit Sub
                End If
            End If

            If PetAlive(i) AndAlso i <> index Then
                If GetPetX(i) = x AndAlso GetPetY(i) = y Then
                    ' Change target
                    If TempPlayer(index).PetTargetType = TargetType.Pet AndAlso TempPlayer(index).PetTarget = i Then
                        TempPlayer(index).PetTarget = 0
                        TempPlayer(index).PetTargetType = TargetType.None
                        ' send target to player
                        PlayerMsg(index, "Your pet is no longer targetting " & GetPlayerName(i).Trim & "'s " & GetPetName(i).Trim & ".", ColorType.BrightGreen)
                    Else
                        TempPlayer(index).PetTarget = i
                        TempPlayer(index).PetTargetType = TargetType.Pet
                        ' send target to player
                        PlayerMsg(index, "Your pet is now targetting " & GetPlayerName(i).Trim & "'s " & GetPetName(i).Trim & ".", ColorType.BrightGreen)
                    End If
                    Exit Sub
                End If
            End If
        Next

        'Search For Target First
        ' Check for an npc
        For i = 1 To MAX_MAP_NPCS
            If MapNpc(GetPlayerMap(index)).Npc(i).Num > 0 AndAlso MapNpc(GetPlayerMap(index)).Npc(i).X = x AndAlso MapNpc(GetPlayerMap(index)).Npc(i).Y = y Then
                If TempPlayer(index).PetTarget = i AndAlso TempPlayer(index).PetTargetType = TargetType.Npc Then
                    ' Change target
                    TempPlayer(index).PetTarget = 0
                    TempPlayer(index).PetTargetType = TargetType.None
                    ' send target to player
                    PlayerMsg(index, "Your " & GetPetName(index).Trim & "'s target is no longer a " & Npc(MapNpc(GetPlayerMap(index)).Npc(i).Num).Name.Trim & "!", ColorType.BrightGreen)
                    Exit Sub
                Else
                    ' Change target
                    TempPlayer(index).PetTarget = i
                    TempPlayer(index).PetTargetType = TargetType.Npc
                    ' send target to player
                    PlayerMsg(index, "Your " & GetPetName(index).Trim & "'s target is now a " & Npc(MapNpc(GetPlayerMap(index)).Npc(i).Num).Name.Trim & "!", ColorType.BrightGreen)
                    Exit Sub
                End If
            End If
        Next

        TempPlayer(index).PetBehavior = PetBehaviourGoto
        TempPlayer(index).PetTargetType = 0
        TempPlayer(index).PetTarget = 0
        TempPlayer(index).GoToX = x
        TempPlayer(index).GoToY = y

        buffer.Dispose()

    End Sub

    Sub Packet_SetPetBehaviour(index As Integer, ByRef data() As Byte)
        Dim behaviour As Integer
        Dim buffer As New ByteStream(data)
        behaviour = buffer.ReadInt32

        If PetAlive(index) Then
            Select Case behaviour
                Case PetAttackBehaviourAttackonsight
                    SetPetBehaviour(index, PetAttackBehaviourAttackonsight)
                    SendActionMsg(GetPlayerMap(index), "Agressive Mode!", ColorType.White, 0, GetPetX(index) * 32, GetPetY(index) * 32, index)
                Case PetAttackBehaviourGuard
                    SetPetBehaviour(index, PetAttackBehaviourGuard)
                    SendActionMsg(GetPlayerMap(index), "Defensive Mode!", ColorType.White, 0, GetPetX(index) * 32, GetPetY(index) * 32, index)
            End Select
        End If

        buffer.Dispose()

    End Sub

    Sub Packet_ReleasePet(index As Integer, ByRef data() As Byte)
        If GetPetNum(index) > 0 Then ReleasePet(index)
    End Sub

    Sub Packet_PetSkill(index As Integer, ByRef data() As Byte)
        Dim n As Integer
        Dim buffer As New ByteStream(data)
        ' Skill slot
        n = buffer.ReadInt32

        buffer.Dispose()

        ' set the skill buffer before castin
        BufferPetSkill(index, n)

    End Sub

    Sub Packet_UsePetStatPoint(index As Integer, ByRef data() As Byte)
        Dim pointType As Byte
        Dim sMes As String = ""
        Dim buffer As New ByteStream(data)
        pointType = buffer.ReadInt32
        buffer.Dispose()

        ' Prevent hacking
        If (pointType < 0) OrElse (pointType > StatType.Count) Then Exit Sub

        If Not PetAlive(index) Then Exit Sub

        ' Make sure they have points
        If GetPetPoints(index) > 0 Then

            ' make sure they're not maxed#
            If GetPetStat(index, pointType) >= 255 Then
                PlayerMsg(index, "You cannot spend any more points on that stat for your pet.", ColorType.BrightRed)
                Exit Sub
            End If

            SetPetPoints(index, GetPetPoints(index) - 1)

            ' Everything is ok
            Select Case pointType
                Case StatType.Strength
                    SetPetStat(index, pointType, GetPetStat(index, pointType) + 1)
                    sMes = "Strength"
                Case StatType.Endurance
                    SetPetStat(index, pointType, GetPetStat(index, pointType) + 1)
                    sMes = "Endurance"
                Case StatType.Intelligence
                    SetPetStat(index, pointType, GetPetStat(index, pointType) + 1)
                    sMes = "Intelligence"
                Case StatType.Luck
                    SetPetStat(index, pointType, GetPetStat(index, pointType) + 1)
                    sMes = "Agility"
                Case StatType.Spirit
                    SetPetStat(index, pointType, GetPetStat(index, pointType) + 1)
                    sMes = "Willpower"
            End Select

            SendActionMsg(GetPlayerMap(index), "+1 " & sMes, ColorType.White, 1, (GetPetX(index) * 32), (GetPetY(index) * 32))
        Else
            Exit Sub
        End If

        ' Send the update
        SendUpdatePlayerPet(index, True)

    End Sub

#End Region

#Region "Pet Functions"

    Friend Sub UpdatePetAi()
        Dim didWalk As Boolean, playerindex As Integer
        Dim mapNum As Integer, tickCount As Integer, i As Integer, n As Integer
        Dim distanceX As Integer, distanceY As Integer, tmpdir As Integer
        Dim target As Integer, targetTypes As Byte, targetX As Integer, targetY As Integer, targetVerify As Boolean

        For mapNum = 1 To MAX_CACHED_MAPS
            For playerindex = 1 To GetPlayersOnline()
                tickCount = GetTimeMs()

                If GetPlayerMap(playerindex) = mapNum AndAlso PetAlive(playerindex) Then
                    ' // This is used for ATTACKING ON SIGHT //

                    ' If the npc is a attack on sight, search for a player on the map
                    If GetPetBehaviour(playerindex) <> PetAttackBehaviourDonothing Then

                        ' make sure it's not stunned
                        If Not TempPlayer(playerindex).PetStunDuration > 0 Then

                            For i = 1 To Socket.HighIndex
                                If TempPlayer(playerindex).PetTargetType > 0 Then
                                    If TempPlayer(playerindex).PetTargetType = 1 AndAlso TempPlayer(playerindex).PetTarget = playerindex Then
                                    Else
                                        Exit For
                                    End If
                                End If

                                If IsPlaying(i) AndAlso i <> playerindex Then
                                    If GetPlayerMap(i) = mapNum AndAlso GetPlayerAccess(i) <= AdminType.Monitor Then
                                        If PetAlive(i) Then
                                            n = GetPetRange(playerindex)
                                            distanceX = GetPetX(playerindex) - GetPetX(i)
                                            distanceY = GetPetY(playerindex) - GetPetY(i)

                                            ' Make sure we get a positive value
                                            If distanceX < 0 Then distanceX *= -1
                                            If distanceY < 0 Then distanceY *= -1

                                            ' Are they in range?  if so GET'M!
                                            If distanceX <= n AndAlso distanceY <= n Then
                                                If GetPetBehaviour(playerindex) = PetAttackBehaviourAttackonsight Then
                                                    TempPlayer(playerindex).PetTargetType = TargetType.Pet ' pet
                                                    TempPlayer(playerindex).PetTarget = i
                                                End If
                                            End If
                                        Else
                                            n = GetPetRange(playerindex)
                                            distanceX = GetPetX(playerindex) - GetPlayerX(i)
                                            distanceY = GetPetY(playerindex) - GetPlayerY(i)

                                            ' Make sure we get a positive value
                                            If distanceX < 0 Then distanceX *= -1
                                            If distanceY < 0 Then distanceY *= -1

                                            ' Are they in range?  if so GET'M!
                                            If distanceX <= n AndAlso distanceY <= n Then
                                                If GetPetBehaviour(playerindex) = PetAttackBehaviourAttackonsight Then
                                                    TempPlayer(playerindex).PetTargetType = TargetType.Player ' player
                                                    TempPlayer(playerindex).PetTarget = i
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Next

                            If TempPlayer(playerindex).PetTargetType = 0 Then
                                For i = 1 To MAX_MAP_NPCS

                                    If TempPlayer(playerindex).PetTargetType > 0 Then Exit For
                                    If PetAlive(playerindex) Then
                                        n = GetPetRange(playerindex)
                                        distanceX = GetPetX(playerindex) - MapNpc(GetPlayerMap(playerindex)).Npc(i).X
                                        distanceY = GetPetY(playerindex) - MapNpc(GetPlayerMap(playerindex)).Npc(i).Y

                                        ' Make sure we get a positive value
                                        If distanceX < 0 Then distanceX *= -1
                                        If distanceY < 0 Then distanceY *= -1

                                        ' Are they in range?  if so GET'M!
                                        If distanceX <= n AndAlso distanceY <= n Then
                                            If GetPetBehaviour(playerindex) = PetAttackBehaviourAttackonsight Then
                                                TempPlayer(playerindex).PetTargetType = TargetType.Npc ' npc
                                                TempPlayer(playerindex).PetTarget = i
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If

                        targetVerify = False

                        ' // This is used for Pet walking/targetting //

                        ' Make sure theres a npc with the map
                        If TempPlayer(playerindex).PetStunDuration > 0 Then
                            ' check if we can unstun them
                            If GetTimeMs() > TempPlayer(playerindex).PetStunTimer + (TempPlayer(playerindex).PetStunDuration * 1000) Then
                                TempPlayer(playerindex).PetStunDuration = 0
                                TempPlayer(playerindex).PetStunTimer = 0
                            End If
                        Else
                            target = TempPlayer(playerindex).PetTarget
                            targetTypes = TempPlayer(playerindex).PetTargetType

                            ' Check to see if its time for the npc to walk
                            If GetPetBehaviour(playerindex) <> PetAttackBehaviourDonothing Then

                                If targetTypes = TargetType.Player Then ' player
                                    ' Check to see if we are following a player or not
                                    If target > 0 Then

                                        ' Check if the player is even playing, if so follow'm
                                        If IsPlaying(target) AndAlso GetPlayerMap(target) = mapNum Then
                                            If target <> playerindex Then
                                                didWalk = False
                                                targetVerify = True
                                                targetY = GetPlayerY(target)
                                                targetX = GetPlayerX(target)
                                            End If
                                        Else
                                            TempPlayer(playerindex).PetTargetType = TargetType.None ' clear
                                            TempPlayer(playerindex).PetTarget = 0
                                        End If
                                    End If
                                ElseIf targetTypes = TargetType.Npc Then 'npc
                                    If target > 0 Then
                                        If MapNpc(mapNum).Npc(target).Num > 0 Then
                                            didWalk = False
                                            targetVerify = True
                                            targetY = MapNpc(mapNum).Npc(target).Y
                                            targetX = MapNpc(mapNum).Npc(target).X
                                        Else
                                            TempPlayer(playerindex).PetTargetType = TargetType.None ' clear
                                            TempPlayer(playerindex).PetTarget = 0
                                        End If
                                    End If
                                ElseIf targetTypes = TargetType.Pet Then 'other pet
                                    If target > 0 Then
                                        If IsPlaying(target) AndAlso GetPlayerMap(target) = mapNum AndAlso PetAlive(target) Then
                                            didWalk = False
                                            targetVerify = True
                                            targetY = GetPetY(target)
                                            targetX = GetPetX(target)
                                        Else
                                            TempPlayer(playerindex).PetTargetType = TargetType.None ' clear
                                            TempPlayer(playerindex).PetTarget = 0
                                        End If
                                    End If
                                End If
                            End If

                            If targetVerify Then
                                didWalk = False

                                If IsOneBlockAway(GetPetX(playerindex), GetPetY(playerindex), targetX, targetY) Then
                                    If GetPetX(playerindex) < targetX Then
                                        PetDir(playerindex, DirectionType.Right)
                                        didWalk = True
                                    ElseIf GetPetX(playerindex) > targetX Then
                                        PetDir(playerindex, DirectionType.Left)
                                        didWalk = True
                                    ElseIf GetPetY(playerindex) < targetY Then
                                        PetDir(playerindex, DirectionType.Up)
                                        didWalk = True
                                    ElseIf GetPetY(playerindex) > targetY Then
                                        PetDir(playerindex, DirectionType.Down)
                                        didWalk = True
                                    End If
                                Else
                                    didWalk = PetTryWalk(playerindex, targetX, targetY)
                                End If

                            ElseIf TempPlayer(playerindex).PetBehavior = PetBehaviourGoto AndAlso targetVerify = False Then

                                If GetPetX(playerindex) = TempPlayer(playerindex).GoToX AndAlso GetPetY(playerindex) = TempPlayer(playerindex).GoToY Then
                                    'Unblock these for the random turning
                                    'i = Int(Rnd() * 4)
                                    'PetDir(playerindex, i)
                                Else
                                    didWalk = False
                                    targetX = TempPlayer(playerindex).GoToX
                                    targetY = TempPlayer(playerindex).GoToY
                                    didWalk = PetTryWalk(playerindex, targetX, targetY)

                                    If didWalk = False Then
                                        tmpdir = Int(Rnd() * 4)

                                        If tmpdir = 1 Then
                                            tmpdir = Int(Rnd() * 4)
                                            If CanPetMove(playerindex, mapNum, tmpdir) Then
                                                PetMove(playerindex, mapNum, tmpdir, MovementType.Walking)
                                            End If
                                        End If
                                    End If
                                End If

                            ElseIf TempPlayer(playerindex).PetBehavior = PetBehaviourFollow Then

                                If IsPetByPlayer(playerindex) Then
                                    'Unblock these to enable random turning
                                    'i = Int(Rnd() * 4)
                                    'PetDir(playerindex, i)
                                Else
                                    didWalk = False
                                    targetX = GetPlayerX(playerindex)
                                    targetY = GetPlayerY(playerindex)
                                    didWalk = PetTryWalk(playerindex, targetX, targetY)

                                    If didWalk = False Then
                                        tmpdir = Int(Rnd() * 4)
                                        If tmpdir = 1 Then
                                            tmpdir = Int(Rnd() * 4)
                                            If CanPetMove(playerindex, mapNum, tmpdir) Then
                                                PetMove(playerindex, mapNum, tmpdir, MovementType.Walking)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        ' // This is used for pets to attack targets //

                        ' Make sure theres a npc with the map
                        target = TempPlayer(playerindex).PetTarget
                        targetTypes = TempPlayer(playerindex).PetTargetType

                        ' Check if the pet can attack the targeted player
                        If target > 0 Then
                            If targetTypes = TargetType.Player Then ' player
                                ' Is the target playing and on the same map?
                                If IsPlaying(target) AndAlso GetPlayerMap(target) = mapNum Then
                                    If playerindex <> target Then TryPetAttackPlayer(playerindex, target)
                                Else
                                    ' Player left map or game, set target to 0
                                    TempPlayer(playerindex).PetTarget = 0
                                    TempPlayer(playerindex).PetTargetType = TargetType.None ' clear

                                End If
                            ElseIf targetTypes = TargetType.Npc Then 'npc
                                If MapNpc(GetPlayerMap(playerindex)).Npc(TempPlayer(playerindex).PetTarget).Num > 0 Then
                                    TryPetAttackNpc(playerindex, TempPlayer(playerindex).PetTarget)
                                Else
                                    ' Player left map or game, set target to 0
                                    TempPlayer(playerindex).PetTarget = 0
                                    TempPlayer(playerindex).PetTargetType = TargetType.None ' clear
                                End If
                            ElseIf targetTypes = TargetType.Pet Then 'pet
                                ' Is the target playing and on the same map? AndAlso is pet alive??
                                If IsPlaying(target) AndAlso GetPlayerMap(target) = mapNum AndAlso PetAlive(target) Then
                                    TryPetAttackPet(playerindex, target)
                                Else
                                    ' Player left map or game, set target to 0
                                    TempPlayer(playerindex).PetTarget = 0
                                    TempPlayer(playerindex).PetTargetType = TargetType.None ' clear
                                End If
                            End If
                        End If

                        ' ////////////////////////////////////////////
                        ' // This is used for regenerating Pet's HP //
                        ' ////////////////////////////////////////////
                        ' Check to see if we want to regen some of the npc's hp
                        If Not TempPlayer(playerindex).PetstopRegen Then
                            If PetAlive(playerindex) AndAlso tickCount > givePetHpTimer + 10000 Then
                                If GetPetVital(playerindex, VitalType.HP) > 0 Then
                                    SetPetVital(playerindex, VitalType.HP, GetPetVital(playerindex, VitalType.HP) + GetPetVitalRegen(playerindex, VitalType.HP))
                                    SetPetVital(playerindex, VitalType.MP, GetPetVital(playerindex, VitalType.MP) + GetPetVitalRegen(playerindex, VitalType.MP))

                                    ' Check if they have more then they should and if so just set it to max
                                    If GetPetVital(playerindex, VitalType.HP) > GetPetMaxVital(playerindex, VitalType.HP) Then
                                        SetPetVital(playerindex, VitalType.HP, GetPetMaxVital(playerindex, VitalType.HP))
                                    End If

                                    If GetPetVital(playerindex, VitalType.MP) > GetPetMaxVital(playerindex, VitalType.MP) Then
                                        SetPetVital(playerindex, VitalType.MP, GetPetMaxVital(playerindex, VitalType.MP))
                                    End If

                                    If Not GetPetVital(playerindex, VitalType.HP) = GetPetMaxVital(playerindex, VitalType.HP) Then
                                        SendPetVital(playerindex, VitalType.HP)
                                        SendPetVital(playerindex, VitalType.MP)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

            Next

        Next

        ' Make sure we reset the timer for npc hp regeneration
        If GetTimeMs() > givePetHpTimer + 10000 Then
            givePetHpTimer = GetTimeMs()
        End If
    End Sub

    Sub SummonPet(index As Integer)
        Player(index).Character(TempPlayer(index).CurChar).Pet.Alive = 1
        PlayerMsg(index, "You summoned your " & GetPetName(index).Trim & "!", ColorType.BrightGreen)
        SendUpdatePlayerPet(index, False)
    End Sub

    Sub ReCallPet(index As Integer)
        PlayerMsg(index, "You recalled your " & GetPetName(index).Trim & "!", ColorType.BrightGreen)
        Player(index).Character(TempPlayer(index).CurChar).Pet.Alive = 0
        SendUpdatePlayerPet(index, False)
    End Sub

    Sub ReleasePet(index As Integer)
        Dim i As Integer

        Player(index).Character(TempPlayer(index).CurChar).Pet.Alive = 0
        Player(index).Character(TempPlayer(index).CurChar).Pet.Num = 0
        Player(index).Character(TempPlayer(index).CurChar).Pet.AttackBehaviour = 0
        Player(index).Character(TempPlayer(index).CurChar).Pet.Dir = 0
        Player(index).Character(TempPlayer(index).CurChar).Pet.Health = 0
        Player(index).Character(TempPlayer(index).CurChar).Pet.Level = 0
        Player(index).Character(TempPlayer(index).CurChar).Pet.Mana = 0
        Player(index).Character(TempPlayer(index).CurChar).Pet.X = 0
        Player(index).Character(TempPlayer(index).CurChar).Pet.Y = 0

        TempPlayer(index).PetTarget = 0
        TempPlayer(index).PetTargetType = 0
        TempPlayer(index).GoToX = -1
        TempPlayer(index).GoToY = -1

        For i = 1 To 4
            Player(index).Character(TempPlayer(index).CurChar).Pet.Skill(i) = 0
        Next

        For i = 1 To StatType.Count - 1
            Player(index).Character(TempPlayer(index).CurChar).Pet.Stat(i) = 0
        Next

        SendUpdatePlayerPet(index, False)

        SavePlayer(index)

        PlayerMsg(index, "You released your pet!", ColorType.BrightGreen)

        For i = 1 To MAX_MAP_NPCS
            If MapNpc(GetPlayerMap(index)).Npc(i).Vital(VitalType.HP) > 0 Then
                If MapNpc(GetPlayerMap(index)).Npc(i).TargetType = TargetType.Pet Then
                    If MapNpc(GetPlayerMap(index)).Npc(i).Target = index Then
                        MapNpc(GetPlayerMap(index)).Npc(i).TargetType = TargetType.Player
                        MapNpc(GetPlayerMap(index)).Npc(i).Target = index
                    End If
                End If
            End If
        Next

    End Sub

    Sub AdoptPet(index As Integer, petNum As Integer)

        If GetPetNum(index) = 0 Then
            PlayerMsg(index, "You have adopted a " & Pet(petNum).Name.Trim, ColorType.BrightGreen)
        Else
            PlayerMsg(index, "You allready have a " & Pet(petNum).Name.Trim & ", release your old pet first!", ColorType.BrightGreen)
            Exit Sub
        End If

        Player(index).Character(TempPlayer(index).CurChar).Pet.Num = petNum

        For i = 1 To 4
            Player(index).Character(TempPlayer(index).CurChar).Pet.Skill(i) = Pet(petNum).Skill(i)
        Next

        If Pet(petNum).StatType = 0 Then
            Player(index).Character(TempPlayer(index).CurChar).Pet.Health = GetPlayerMaxVital(index, VitalType.HP)
            Player(index).Character(TempPlayer(index).CurChar).Pet.Mana = GetPlayerMaxVital(index, VitalType.MP)
            Player(index).Character(TempPlayer(index).CurChar).Pet.Level = GetPlayerLevel(index)

            For i = 1 To StatType.Count - 1
                Player(index).Character(TempPlayer(index).CurChar).Pet.Stat(i) = Player(index).Character(TempPlayer(index).CurChar).Stat(i)
            Next

            Player(index).Character(TempPlayer(index).CurChar).Pet.AdoptiveStats = 1
        Else
            For i = 1 To StatType.Count - 1
                Player(index).Character(TempPlayer(index).CurChar).Pet.Stat(i) = Pet(petNum).Stat(i)
            Next

            Player(index).Character(TempPlayer(index).CurChar).Pet.Level = Pet(petNum).Level
            Player(index).Character(TempPlayer(index).CurChar).Pet.AdoptiveStats = 0
            Player(index).Character(TempPlayer(index).CurChar).Pet.Health = GetPetMaxVital(index, VitalType.HP)
            Player(index).Character(TempPlayer(index).CurChar).Pet.Mana = GetPetMaxVital(index, VitalType.MP)
        End If

        Player(index).Character(TempPlayer(index).CurChar).Pet.X = GetPlayerX(index)
        Player(index).Character(TempPlayer(index).CurChar).Pet.Y = GetPlayerY(index)

        Player(index).Character(TempPlayer(index).CurChar).Pet.Alive = 1
        Player(index).Character(TempPlayer(index).CurChar).Pet.Points = 0
        Player(index).Character(TempPlayer(index).CurChar).Pet.Exp = 0

        Player(index).Character(TempPlayer(index).CurChar).Pet.AttackBehaviour = PetAttackBehaviourGuard 'By default it will guard but this can be changed

        SavePlayer(index)

        SendUpdatePlayerPet(index, False)

    End Sub

    Sub PetMove(index As Integer, mapNum As Integer, dir As Integer, movement As Integer)
        Dim buffer As New ByteStream(4)

        If mapNum < 1 OrElse mapNum > MAX_MAPS OrElse index <= 0 OrElse index > MAX_PLAYERS OrElse dir < DirectionType.Up OrElse dir > DirectionType.Right OrElse movement < 1 OrElse movement > 2 Then
            Exit Sub
        End If

        Player(index).Character(TempPlayer(index).CurChar).Pet.Dir = dir

        Select Case dir
            Case DirectionType.Up
                SetPetY(index, GetPetY(index) - 1)

            Case DirectionType.Down
                SetPetY(index, GetPetY(index) + 1)

            Case DirectionType.Left
                SetPetX(index, GetPetX(index) - 1)

            Case DirectionType.Right
                SetPetX(index, GetPetX(index) + 1)
        End Select

        buffer.WriteInt32(ServerPackets.SPetMove)
        buffer.WriteInt32(index)
        buffer.WriteInt32(GetPetX(index))
        buffer.WriteInt32(GetPetY(index))
        buffer.WriteInt32(GetPetDir(index))
        buffer.WriteInt32(movement)
        SendDataToMap(mapNum, buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Function CanPetMove(index As Integer, mapNum As Integer, dir As Byte) As Boolean
        Dim i As Integer, n As Integer
        Dim x As Integer, y As Integer

        If mapNum < 1 OrElse mapNum > MAX_MAPS OrElse index <= 0 OrElse index > MAX_PLAYERS OrElse dir < DirectionType.Up OrElse dir > DirectionType.Right Then
            Exit Function
        End If

        If index <= 0 OrElse index > MAX_PLAYERS Then Exit Function

        x = GetPetX(index)
        y = GetPetY(index)

        If x < 0 OrElse x > Map(mapNum).MaxX Then Exit Function
        If y < 0 OrElse y > Map(mapNum).MaxY Then Exit Function

        CanPetMove = True

        If TempPlayer(index).PetskillBuffer.Skill > 0 Then
            CanPetMove = False
            Exit Function
        End If

        Select Case dir

            Case DirectionType.Up
                ' Check to make sure not outside of boundries
                If y > 0 Then
                    n = Map(mapNum).Tile(x, y - 1).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.NpcSpawn Then
                        CanPetMove = False
                        Exit Function
                    End If

                    ' Check to make sure that there is not a player in the way
                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = mapNum) AndAlso (GetPlayerX(i) = GetPetX(index) + 1) AndAlso (GetPlayerY(i) = GetPetY(index) - 1) Then
                                CanPetMove = False
                                Exit Function
                            ElseIf PetAlive(i) AndAlso (GetPlayerMap(i) = mapNum) AndAlso (GetPetX(i) = GetPetX(index)) AndAlso (GetPetY(i) = GetPetY(index) - 1) Then
                                CanPetMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (MapNpc(mapNum).Npc(i).Num > 0) AndAlso (MapNpc(mapNum).Npc(i).X = GetPetX(index)) AndAlso (MapNpc(mapNum).Npc(i).Y = GetPetY(index) - 1) Then
                            CanPetMove = False
                            Exit Function
                        End If
                    Next

                    ' Directional blocking
                    If IsDirBlocked(Map(mapNum).Tile(GetPetX(index), GetPetY(index)).DirBlock, DirectionType.Up + 1) Then
                        CanPetMove = False
                        Exit Function
                    End If
                Else
                    CanPetMove = False
                End If

            Case DirectionType.Down

                ' Check to make sure not outside of boundries
                If y < Map(mapNum).MaxY Then
                    n = Map(mapNum).Tile(x, y + 1).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.NpcSpawn Then
                        CanPetMove = False
                        Exit Function
                    End If

                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = mapNum) AndAlso (GetPlayerX(i) = GetPetX(index)) AndAlso (GetPlayerY(i) = GetPetY(index) + 1) Then
                                CanPetMove = False
                                Exit Function
                            ElseIf PetAlive(i) AndAlso (GetPlayerMap(i) = mapNum) AndAlso (GetPetX(i) = GetPetX(index)) AndAlso (GetPetY(i) = GetPetY(index) + 1) Then
                                CanPetMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (MapNpc(mapNum).Npc(i).Num > 0) AndAlso (MapNpc(mapNum).Npc(i).X = GetPetX(index)) AndAlso (MapNpc(mapNum).Npc(i).Y = GetPetY(index) + 1) Then
                            CanPetMove = False
                            Exit Function
                        End If
                    Next

                    ' Directional blocking
                    If IsDirBlocked(Map(mapNum).Tile(GetPetX(index), GetPetY(index)).DirBlock, DirectionType.Down + 1) Then
                        CanPetMove = False
                        Exit Function
                    End If
                Else
                    CanPetMove = False
                End If

            Case DirectionType.Left

                ' Check to make sure not outside of boundries
                If x > 0 Then
                    n = Map(mapNum).Tile(x - 1, y).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.NpcSpawn Then
                        CanPetMove = False
                        Exit Function
                    End If

                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = mapNum) AndAlso (GetPlayerX(i) = GetPetX(index) - 1) AndAlso (GetPlayerY(i) = GetPetY(index)) Then
                                CanPetMove = False
                                Exit Function
                            ElseIf PetAlive(i) AndAlso (GetPlayerMap(i) = mapNum) AndAlso (GetPetX(i) = GetPetX(index) - 1) AndAlso (GetPetY(i) = GetPetY(index)) Then
                                CanPetMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (MapNpc(mapNum).Npc(i).Num > 0) AndAlso (MapNpc(mapNum).Npc(i).X = GetPetX(index) - 1) AndAlso (MapNpc(mapNum).Npc(i).Y = GetPetY(index)) Then
                            CanPetMove = False
                            Exit Function
                        End If
                    Next

                    ' Directional blocking
                    If IsDirBlocked(Map(mapNum).Tile(GetPetX(index), GetPetY(index)).DirBlock, DirectionType.Left + 1) Then
                        CanPetMove = False
                        Exit Function
                    End If
                Else
                    CanPetMove = False
                End If

            Case DirectionType.Right

                ' Check to make sure not outside of boundries
                If x < Map(mapNum).MaxX Then
                    n = Map(mapNum).Tile(x + 1, y).Type

                    ' Check to make sure that the tile is walkable
                    If n <> TileType.None AndAlso n <> TileType.NpcSpawn Then
                        CanPetMove = False
                        Exit Function
                    End If

                    For i = 1 To GetPlayersOnline()
                        If IsPlaying(i) Then
                            If (GetPlayerMap(i) = mapNum) AndAlso (GetPlayerX(i) = GetPetX(index) + 1) AndAlso (GetPlayerY(i) = GetPetY(index)) Then
                                CanPetMove = False
                                Exit Function
                            ElseIf PetAlive(i) AndAlso (GetPlayerMap(i) = mapNum) AndAlso (GetPetX(i) = GetPetX(index) + 1) AndAlso (GetPetY(i) = GetPetY(index)) Then
                                CanPetMove = False
                                Exit Function
                            End If
                        End If
                    Next

                    ' Check to make sure that there is not another npc in the way
                    For i = 1 To MAX_MAP_NPCS
                        If (MapNpc(mapNum).Npc(i).Num > 0) AndAlso (MapNpc(mapNum).Npc(i).X = GetPetX(index) + 1) AndAlso (MapNpc(mapNum).Npc(i).Y = GetPetY(index)) Then
                            CanPetMove = False
                            Exit Function
                        End If
                    Next

                    ' Directional blocking
                    If IsDirBlocked(Map(mapNum).Tile(GetPetX(index), GetPetY(index)).DirBlock, DirectionType.Right + 1) Then
                        CanPetMove = False
                        Exit Function
                    End If
                Else
                    CanPetMove = False
                End If

        End Select

    End Function

    Sub PetDir(index As Integer, dir As Integer)
        Dim buffer As New ByteStream(4)

        If index <= 0 OrElse index > MAX_PLAYERS OrElse dir < DirectionType.Up OrElse dir > DirectionType.Right Then Exit Sub

        If TempPlayer(index).PetskillBuffer.Skill > 0 Then Exit Sub

        Player(index).Character(TempPlayer(index).CurChar).Pet.Dir = dir

        buffer.WriteInt32(ServerPackets.SPetDir)
        buffer.WriteInt32(index)
        buffer.WriteInt32(dir)
        SendDataToMap(GetPlayerMap(index), buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub

    Function PetTryWalk(index As Integer, targetX As Integer, targetY As Integer) As Boolean
        Dim i As Integer, x As Integer, didwalk As Boolean
        Dim mapNum As Integer

        mapNum = GetPlayerMap(index)
        x = index

        If IsOneBlockAway(targetX, targetY, GetPetX(index), GetPetY(index)) = False Then

            If PathfindingType = 1 Then
                i = Int(Rnd() * 5)

                ' Lets move the pet
                Select Case i
                    Case 0
                        ' Up
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y > targetY AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Up) Then
                                PetMove(x, mapNum, DirectionType.Up, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Down
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y < targetY AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Down) Then
                                PetMove(x, mapNum, DirectionType.Down, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Left
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X > targetX AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Left) Then
                                PetMove(x, mapNum, DirectionType.Left, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Right
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X < targetX AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Right) Then
                                PetMove(x, mapNum, DirectionType.Right, MovementType.Walking)
                                didwalk = True
                            End If
                        End If
                    Case 1

                        ' Right
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X < targetX AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Right) Then
                                PetMove(x, mapNum, DirectionType.Right, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Left
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X > targetX AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Left) Then
                                PetMove(x, mapNum, DirectionType.Left, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Down
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y < targetY AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Down) Then
                                PetMove(x, mapNum, DirectionType.Down, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Up
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y > targetY AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Up) Then
                                PetMove(x, mapNum, DirectionType.Up, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                    Case 2

                        ' Down
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y < targetY AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Down) Then
                                PetMove(x, mapNum, DirectionType.Down, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Up
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y > targetY AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Up) Then
                                PetMove(x, mapNum, DirectionType.Up, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Right
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X < targetX AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Right) Then
                                PetMove(x, mapNum, DirectionType.Right, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Left
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X > targetX AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Left) Then
                                PetMove(x, mapNum, DirectionType.Left, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                    Case 3

                        ' Left
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X > targetX AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Left) Then
                                Call PetMove(x, mapNum, DirectionType.Left, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Right
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.X < targetX AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Right) Then
                                PetMove(x, mapNum, DirectionType.Right, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Up
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y > targetY AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Up) Then
                                PetMove(x, mapNum, DirectionType.Up, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                        ' Down
                        If Player(x).Character(TempPlayer(x).CurChar).Pet.Y < targetY AndAlso Not didwalk Then
                            If CanPetMove(x, mapNum, DirectionType.Down) Then
                                PetMove(x, mapNum, DirectionType.Down, MovementType.Walking)
                                didwalk = True
                            End If
                        End If

                End Select

                ' Check if we can't move and if Target is behind something and if we can just switch dirs
                If Not didwalk Then
                    If GetPetX(x) - 1 = targetX AndAlso GetPetY(x) = targetY Then

                        If GetPetDir(x) <> DirectionType.Left Then
                            PetDir(x, DirectionType.Left)
                        End If

                        didwalk = True
                    End If

                    If GetPetX(x) + 1 = targetX AndAlso GetPetY(x) = targetY Then

                        If GetPetDir(x) <> DirectionType.Right Then
                            PetDir(x, DirectionType.Right)
                        End If

                        didwalk = True
                    End If

                    If GetPetX(x) = targetX AndAlso GetPetY(x) - 1 = targetY Then

                        If GetPetDir(x) <> DirectionType.Up Then
                            PetDir(x, DirectionType.Up)
                        End If

                        didwalk = True
                    End If

                    If GetPetX(x) = targetX AndAlso GetPetY(x) + 1 = targetY Then

                        If GetPetDir(x) <> DirectionType.Down Then
                            PetDir(x, DirectionType.Down)
                        End If

                        didwalk = True
                    End If
                End If
            Else
                'Pathfind
                i = FindPetPath(mapNum, x, targetX, targetY)

                If i < 4 Then 'Returned an answer. Move the pet
                    If CanPetMove(x, mapNum, i) Then
                        PetMove(x, mapNum, i, MovementType.Walking)
                        didwalk = True
                    End If
                End If
            End If
        Else

            'Look to target
            If GetPetX(index) > TempPlayer(index).GoToX Then
                If CanPetMove(x, mapNum, DirectionType.Left) Then
                    PetMove(x, mapNum, DirectionType.Left, MovementType.Walking)
                    didwalk = True
                Else
                    PetDir(x, DirectionType.Left)
                    didwalk = True
                End If

            ElseIf GetPetX(index) < TempPlayer(index).GoToX Then

                If CanPetMove(x, mapNum, DirectionType.Right) Then
                    PetMove(x, mapNum, DirectionType.Right, MovementType.Walking)
                    didwalk = True
                Else
                    PetDir(x, DirectionType.Right)
                    didwalk = True
                End If

            ElseIf GetPetY(index) > TempPlayer(index).GoToY Then

                If CanPetMove(x, mapNum, DirectionType.Up) Then
                    PetMove(x, mapNum, DirectionType.Up, MovementType.Walking)
                    didwalk = True
                Else
                    PetDir(x, DirectionType.Up)
                    didwalk = True
                End If

            ElseIf GetPetY(index) < TempPlayer(index).GoToY Then

                If CanPetMove(x, mapNum, DirectionType.Down) Then
                    PetMove(x, mapNum, DirectionType.Down, MovementType.Walking)
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

                If CanPetMove(x, mapNum, i) Then
                    PetMove(x, mapNum, i, MovementType.Walking)
                End If
            End If
        End If

        PetTryWalk = didwalk

    End Function

    Function FindPetPath(mapNum As Integer, index As Integer, targetX As Integer, targetY As Integer) As Integer

        Dim tim As Integer, sX As Integer, sY As Integer, pos(,) As Integer, reachable As Boolean, j As Integer, lastSum As Integer, sum As Integer, fx As Integer, fy As Integer, i As Integer

        Dim path() As Point, lastX As Integer, lastY As Integer, did As Boolean

        'Initialization phase

        tim = 0
        sX = GetPetX(index)
        sY = GetPetY(index)

        fx = targetX
        fy = targetY

        If fx = -1 Then Exit Function
        If fy = -1 Then Exit Function

        ReDim pos(Map(mapNum).MaxX, Map(mapNum).MaxY)
        'pos = MapBlocks(MapNum).Blocks

        pos(sX, sY) = 100 + tim
        pos(fx, fy) = 2

        'reset reachable
        reachable = False

        'Do while reachable is false... if its set true in progress, we jump out
        'If the path is decided unreachable in process, we will use exit sub. Not proper,
        'but faster ;-)
        Do While reachable = False

            'we loop through all squares
            For j = 0 To Map(mapNum).MaxY
                For i = 0 To Map(mapNum).MaxX

                    'If j = 10 AndAlso i = 0 Then MsgBox "hi!"
                    'If they are to be extended, the pointer TIM is on them
                    If pos(i, j) = 100 + tim Then

                        'The part is to be extended, so do it
                        'We have to make sure that there is a pos(i+1,j) BEFORE we actually use it,
                        'because then we get error... If the square is on side, we dont test for this one!
                        If i < Map(mapNum).MaxX Then

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

                        If j < Map(mapNum).MaxY Then
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
                sum = 0

                For j = 0 To Map(mapNum).MaxY
                    For i = 0 To Map(mapNum).MaxX
                        'we add up ALL the squares
                        sum += pos(i, j)
                    Next i
                Next j

                'Now if the sum is euqal to the last sum, its not reachable, if it isnt, then we store
                'sum to lastsum
                If sum = lastSum Then
                    FindPetPath = 4
                    Exit Function
                Else
                    lastSum = sum
                End If
            End If

            'we increase the pointer to point to the next squares to be expanded
            tim += 1
        Loop

        'We work backwards to find the way...
        lastX = fx
        lastY = fy

        ReDim path(tim + 1)

        'The following code may be a little bit confusing but ill try my best to explain it.
        'We are working backwards to find ONE of the shortest ways back to Start.
        'So we repeat the loop until the LastX and LastY arent in start. Look in the code to see
        'how LastX and LasY change
        Do While lastX <> sX OrElse lastY <> sY
            'We decrease tim by one, and then we are finding any adjacent square to the final one, that
            'has that value. So lets say the tim would be 5, because it takes 5 steps to get to the target.
            'Now everytime we decrease that, so we make it 4, and we look for any adjacent square that has
            'that value. When we find it, we just color it yellow as for the solution
            tim -= 1
            'reset did to false
            did = False

            'If we arent on edge
            If lastX < Map(mapNum).MaxX Then

                'check the square on the right of the solution. Is it a tim-1 one? or just a blank one
                If pos(lastX + 1, lastY) = 100 + tim Then
                    'if it, then make it yellow, and change did to true
                    lastX += 1
                    did = True
                End If
            End If

            'This will then only work if the previous part didnt execute, and did is still false. THen
            'we want to check another square, the on left. Is it a tim-1 one ?
            If did = False Then
                If lastX > 0 Then
                    If pos(lastX - 1, lastY) = 100 + tim Then
                        lastX -= 1
                        did = True
                    End If
                End If
            End If

            'We check the one below it
            If did = False Then
                If lastY < Map(mapNum).MaxY Then
                    If pos(lastX, lastY + 1) = 100 + tim Then
                        lastY += 1
                        did = True
                    End If
                End If
            End If

            'And above it. One of these have to be it, since we have found the solution, we know that already
            'there is a way back.
            If did = False Then
                If lastY > 0 Then
                    If pos(lastX, lastY - 1) = 100 + tim Then
                        lastY -= 1
                    End If
                End If
            End If

            path(tim).X = lastX
            path(tim).Y = lastY

            'Now we loop back and decrease tim, and look for the next square with lower value
            Application.DoEvents()
        Loop

        'Ok we got a path. Now, lets look at the first step and see what direction we should take.
        If path(1).X > lastX Then
            FindPetPath = DirectionType.Right
        ElseIf path(1).Y > lastY Then
            FindPetPath = DirectionType.Down
        ElseIf path(1).Y < lastY Then
            FindPetPath = DirectionType.Up
        ElseIf path(1).X < lastX Then
            FindPetPath = DirectionType.Left
        End If

    End Function

    Function GetPetDamage(index As Integer) As Integer
        GetPetDamage = 0

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse index <= 0 OrElse index > MAX_PLAYERS OrElse Not PetAlive(index) Then
            Exit Function
        End If

        GetPetDamage = (Player(index).Character(TempPlayer(index).CurChar).Pet.Stat(StatType.Strength) * 2) + (Player(index).Character(TempPlayer(index).CurChar).Pet.Level * 3) + Random(0, 20)

    End Function

    Friend Function CanPetCrit(index As Integer) As Boolean
        Dim rate As Integer
        Dim rndNum As Integer

        If Not PetAlive(index) Then Exit Function

        CanPetCrit = False

        rate = Player(index).Character(TempPlayer(index).CurChar).Pet.Stat(StatType.Luck) / 3
        rndNum = Random(1, 100)

        If rndNum <= rate Then CanPetCrit = True

    End Function

    Function IsPetByPlayer(index As Integer) As Boolean
        Dim x As Integer, y As Integer, x1 As Integer, y1 As Integer

        If index <= 0 OrElse index > MAX_PLAYERS OrElse Not PetAlive(index) Then Exit Function

        IsPetByPlayer = False

        x = GetPlayerX(index)
        y = GetPlayerY(index)
        x1 = GetPetX(index)
        y1 = GetPetY(index)

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

    Function GetPetVitalRegen(index As Integer, vital As VitalType) As Integer
        Dim i As Integer

        If index <= 0 OrElse index > MAX_PLAYERS OrElse Not PetAlive(index) Then
            GetPetVitalRegen = 0
            Exit Function
        End If

        Select Case vital
            Case VitalType.HP
                i = (GetPlayerStat(index, StatType.Spirit) * 0.8) + 6

            Case VitalType.MP
                i = (GetPlayerStat(index, StatType.Spirit) / 4) + 12.5
        End Select

        GetPetVitalRegen = i

    End Function

    Sub CheckPetLevelUp(index As Integer)
        Dim expRollover As Integer, levelCount As Integer

        levelCount = 0

        Do While GetPetExp(index) >= GetPetNextLevel(index)
            expRollover = GetPetExp(index) - GetPetNextLevel(index)

            ' can level up?
            If GetPetLevel(index) < 99 AndAlso GetPetLevel(index) < Pet(Player(index).Character(TempPlayer(index).CurChar).Pet.Num).MaxLevel Then
                SetPetLevel(index, GetPetLevel(index) + 1)
            End If

            SetPetPoints(index, GetPetPoints(index) + Pet(Player(index).Character(TempPlayer(index).CurChar).Pet.Num).LevelPnts)
            SetPetExp(index, expRollover)
            levelCount += 1
        Loop

        If levelCount > 0 Then
            If levelCount = 1 Then
                'singular
                PlayerMsg(index, "Your " & GetPetName(index).Trim & " has gained " & levelCount & " level!", ColorType.BrightGreen)
            Else
                'plural
                PlayerMsg(index, "Your " & GetPetName(index).Trim & " has gained " & levelCount & " levels!", ColorType.BrightGreen)
            End If

            SendPlayerData(index)

        End If

    End Sub

    Friend Sub PetFireProjectile(index As Integer, spellnum As Integer)
        Dim projectileSlot As Integer, projectileNum As Integer
        Dim mapNum As Integer, i As Integer

        ' Prevent subscript out of range

        mapNum = GetPlayerMap(index)

        'Find a free projectile
        For i = 1 To MAX_PROJECTILES
            If MapProjectiles(mapNum, i).ProjectileNum = 0 Then ' Free Projectile
                projectileSlot = i
                Exit For
            End If
        Next

        'Check for no projectile, if so just overwrite the first slot
        If projectileSlot = 0 Then projectileSlot = 1

        If spellnum < 1 OrElse spellnum > MAX_SKILLS Then Exit Sub

        projectileNum = Skill(spellnum).Projectile

        With MapProjectiles(mapNum, projectileSlot)
            .ProjectileNum = projectileNum
            .Owner = index
            .OwnerType = TargetType.Pet
            .Dir = Player(i).Character(TempPlayer(i).CurChar).Pet.Dir
            .X = Player(i).Character(TempPlayer(i).CurChar).Pet.X
            .Y = Player(i).Character(TempPlayer(i).CurChar).Pet.Y
            .Timer = GetTimeMs() + 60000
        End With

        SendProjectileToMap(mapNum, projectileSlot)

    End Sub

#End Region

#Region "Pet > Npc"

    Friend Sub TryPetAttackNpc(index As Integer, mapNpcNum As Integer)
        Dim blockAmount As Integer
        Dim npcnum As Integer
        Dim mapNum As Integer
        Dim damage As Integer


        ' Can we attack the npc?
        If CanPetAttackNpc(index, mapNpcNum) Then

            mapNum = GetPlayerMap(index)
            npcnum = MapNpc(mapNum).Npc(mapNpcNum).Num

            ' check if NPC can avoid the attack
            If CanNpcDodge(npcnum) Then
                SendActionMsg(mapNum, "Dodge!", ColorType.Pink, 1, (MapNpc(mapNum).Npc(mapNpcNum).X * 32), (MapNpc(mapNum).Npc(mapNpcNum).Y * 32))
                Exit Sub
            End If

            If CanNpcParry(npcnum) Then
                SendActionMsg(mapNum, "Parry!", ColorType.Pink, 1, (MapNpc(mapNum).Npc(mapNpcNum).X * 32), (MapNpc(mapNum).Npc(mapNpcNum).Y * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            damage = GetPetDamage(index)

            ' if the npc blocks, take away the block amount
            blockAmount = CanNpcBlock(mapNpcNum)
            damage -= blockAmount

            ' take away armour
            damage -= Random(1, (Npc(npcnum).Stat(StatType.Luck) * 2))
            ' randomise from 1 to max hit
            damage = Random(1, damage)

            ' * 1.5 if it's a crit!
            If CanPetCrit(index) Then
                damage *= 1.5
                SendActionMsg(mapNum, "Critical!", ColorType.BrightCyan, 1, (GetPlayerX(index) * 32), (GetPlayerY(index) * 32))
            End If

            If damage > 0 Then
                PetAttackNpc(index, mapNpcNum, damage)
            Else
                PlayerMsg(index, "Your pet's attack does nothing.", ColorType.BrightRed)
            End If

        End If

    End Sub

    Friend Function CanPetAttackNpc(attacker As Integer, mapnpcnum As Integer, Optional isSpell As Boolean = False) As Boolean
        Dim mapNum As Integer
        Dim npcnum As Integer
        Dim npcX As Integer
        Dim npcY As Integer
        Dim attackspeed As Integer

        If IsPlaying(attacker) = False OrElse mapnpcnum <= 0 OrElse mapnpcnum > MAX_MAP_NPCS OrElse Not PetAlive(attacker) Then
            Exit Function
        End If

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(attacker)).Npc(mapnpcnum).Num <= 0 Then Exit Function

        mapNum = GetPlayerMap(attacker)
        npcnum = MapNpc(mapNum).Npc(mapnpcnum).Num

        ' Make sure the npc isn't already dead
        If MapNpc(mapNum).Npc(mapnpcnum).Vital(VitalType.HP) <= 0 Then Exit Function

        ' Make sure they are on the same map
        If IsPlaying(attacker) Then

            If TempPlayer(attacker).PetskillBuffer.Skill > 0 AndAlso isSpell = False Then Exit Function

            ' exit out early
            If isSpell AndAlso npcnum > 0 Then
                If Npc(npcnum).Behaviour <> NpcBehavior.Friendly AndAlso Npc(npcnum).Behaviour <> NpcBehavior.ShopKeeper Then
                    CanPetAttackNpc = True
                    Exit Function
                End If
            End If

            attackspeed = 1000 'Pet cannot wield a weapon

            If npcnum > 0 AndAlso GetTimeMs() > TempPlayer(attacker).PetAttackTimer + attackspeed Then

                ' Check if at same coordinates
                Select Case GetPetDir(attacker)

                    Case DirectionType.Up
                        npcX = MapNpc(mapNum).Npc(mapnpcnum).X
                        npcY = MapNpc(mapNum).Npc(mapnpcnum).Y + 1

                    Case DirectionType.Down
                        npcX = MapNpc(mapNum).Npc(mapnpcnum).X
                        npcY = MapNpc(mapNum).Npc(mapnpcnum).Y - 1

                    Case DirectionType.Left
                        npcX = MapNpc(mapNum).Npc(mapnpcnum).X + 1
                        npcY = MapNpc(mapNum).Npc(mapnpcnum).Y

                    Case DirectionType.Right
                        npcX = MapNpc(mapNum).Npc(mapnpcnum).X - 1
                        npcY = MapNpc(mapNum).Npc(mapnpcnum).Y

                End Select

                If npcX = GetPetX(attacker) AndAlso npcY = GetPetY(attacker) Then
                    If Npc(npcnum).Behaviour <> NpcBehavior.Friendly AndAlso Npc(npcnum).Behaviour <> NpcBehavior.ShopKeeper Then
                        CanPetAttackNpc = True
                    Else
                        CanPetAttackNpc = False
                    End If
                End If
            End If
        End If

    End Function

    Friend Sub PetAttackNpc(attacker As Integer, mapnpcnum As Integer, damage As Integer, Optional skillnum As Integer = 0) ', Optional overTime As Boolean = False)
        Dim name As String, exp As Integer
        Dim i As Integer, mapNum As Integer, npcnum As Integer

        ' Check for subscript out of range
        If IsPlaying(attacker) = False OrElse mapnpcnum <= 0 OrElse mapnpcnum > MAX_MAP_NPCS OrElse damage < 0 OrElse Not PetAlive(attacker) Then
            Exit Sub
        End If

        mapNum = GetPlayerMap(attacker)
        npcnum = MapNpc(mapNum).Npc(mapnpcnum).Num
        name = Trim$(Npc(npcnum).Name)

        If skillnum = 0 Then
            ' Send this packet so they can see the pet attacking
            SendPetAttack(attacker, mapNum)
        End If

        ' set the regen timer
        TempPlayer(attacker).PetstopRegen = True
        TempPlayer(attacker).PetstopRegenTimer = GetTimeMs()

        If damage >= MapNpc(mapNum).Npc(mapnpcnum).Vital(VitalType.HP) Then

            SendActionMsg(GetPlayerMap(attacker), "-" & MapNpc(mapNum).Npc(mapnpcnum).Vital(VitalType.HP), ColorType.BrightRed, 1, (MapNpc(mapNum).Npc(mapnpcnum).X * 32), (MapNpc(mapNum).Npc(mapnpcnum).Y * 32))
            SendBlood(GetPlayerMap(attacker), MapNpc(mapNum).Npc(mapnpcnum).X, MapNpc(mapNum).Npc(mapnpcnum).Y)

            ' Calculate exp to give attacker
            exp = Npc(npcnum).Exp

            ' Make sure we dont get less then 0
            If exp < 0 Then
                exp = 1
            End If

            ' in party?
            If TempPlayer(attacker).InParty > 0 Then
                ' pass through party sharing function
                Party_ShareExp(TempPlayer(attacker).InParty, exp, attacker, mapNum)
            Else
                ' no party - keep exp for self
                GivePlayerExp(attacker, exp)
            End If

            'For n = 1 To 20
            '    If MapNpc(MapNum).Npc(mapnpcnum).Num > 0 Then
            '        'SpawnItem(MapNpc(MapNum).Npc(mapnpcnum).Inventory(n).Num, MapNpc(MapNum).Npc(mapnpcnum).Inventory(n).Value, MapNum, MapNpc(MapNum).Npc(mapnpcnum).x, MapNpc(MapNum).Npc(mapnpcnum).y)
            '        'MapNpc(MapNum).Npc(mapnpcnum).Inventory(n).Value = 0
            '        'MapNpc(MapNum).Npc(mapnpcnum).Inventory(n).Num = 0
            '    End If
            'Next

            ' Now set HP to 0 so we know to actually kill them in the server loop (this prevents subscript out of range)
            MapNpc(mapNum).Npc(mapnpcnum).Num = 0
            MapNpc(mapNum).Npc(mapnpcnum).SpawnWait = GetTimeMs()
            MapNpc(mapNum).Npc(mapnpcnum).Vital(VitalType.HP) = 0
            MapNpc(mapNum).Npc(mapnpcnum).TargetType = 0
            MapNpc(mapNum).Npc(mapnpcnum).Target = 0

            ' clear DoTs and HoTs
            'For i = 1 To MAX_COTS
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
            SendNpcDead(mapNum, mapnpcnum)

            'Loop through entire map and purge NPC from targets
            For i = 1 To Socket.HighIndex

                If IsPlaying(i) Then
                    If GetPlayerMap(i) = mapNum Then
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
            MapNpc(mapNum).Npc(mapnpcnum).Vital(VitalType.HP) = MapNpc(mapNum).Npc(mapnpcnum).Vital(VitalType.HP) - damage

            ' Check for a weapon and say damage
            SendActionMsg(mapNum, "-" & damage, ColorType.BrightRed, 1, (MapNpc(mapNum).Npc(mapnpcnum).X * 32), (MapNpc(mapNum).Npc(mapnpcnum).Y * 32))
            SendBlood(GetPlayerMap(attacker), MapNpc(mapNum).Npc(mapnpcnum).X, MapNpc(mapNum).Npc(mapnpcnum).Y)

            ' send the sound
            'If Spellnum > 0 Then SendMapSound Attacker, MapNpc(MapNum).Npc(mapnpcnum).x, MapNpc(MapNum).Npc(mapnpcnum).y, SoundEntity.seSpell, Spellnum

            ' Set the NPC target to the player
            MapNpc(mapNum).Npc(mapnpcnum).TargetType = TargetType.Pet ' player's pet
            MapNpc(mapNum).Npc(mapnpcnum).Target = attacker

            ' Now check for guard ai and if so have all onmap guards come after'm
            If Npc(MapNpc(mapNum).Npc(mapnpcnum).Num).Behaviour = NpcBehavior.Guard Then

                For i = 1 To MAX_MAP_NPCS

                    If MapNpc(mapNum).Npc(i).Num = MapNpc(mapNum).Npc(mapnpcnum).Num Then
                        MapNpc(mapNum).Npc(i).Target = attacker
                        MapNpc(mapNum).Npc(i).TargetType = TargetType.Pet ' pet
                    End If
                Next
            End If

            ' set the regen timer
            MapNpc(mapNum).Npc(mapnpcnum).StopRegen = True
            MapNpc(mapNum).Npc(mapnpcnum).StopRegenTimer = GetTimeMs()

            ' if stunning spell, stun the npc
            If skillnum > 0 Then
                If Skill(skillnum).StunDuration > 0 Then StunNPC(mapnpcnum, mapNum, skillnum)
                ' DoT
                If Skill(skillnum).Duration > 0 Then
                    'AddDoT_Npc(MapNum, mapnpcnum, Skillnum, Attacker, 3)
                End If
            End If

            SendMapNpcVitals(mapNum, mapnpcnum)
        End If

        If skillnum = 0 Then
            ' Reset attack timer
            TempPlayer(attacker).PetAttackTimer = GetTimeMs()
        End If

    End Sub

#End Region

#Region "Npc > Pet"

    Friend Sub TryNpcAttackPet(mapNpcNum As Integer, index As Integer)

        Dim mapNum As Integer, npcnum As Integer, damage As Integer

        ' Can the npc attack the pet?

        If CanNpcAttackPet(mapNpcNum, index) Then
            mapNum = GetPlayerMap(index)
            npcnum = MapNpc(mapNum).Npc(mapNpcNum).Num

            ' check if Pet can avoid the attack
            If CanPetDodge(index) Then
                SendActionMsg(mapNum, "Dodge!", ColorType.Pink, ActionMsgType.Scroll, (GetPetX(index) * 32), (GetPetY(index) * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            damage = GetNpcDamage(npcnum)

            ' take away armour
            damage -= ((GetPetStat(index, StatType.Endurance) * 2) + (GetPetLevel(index) * 2))

            ' * 1.5 if crit hit
            If CanNpcCrit(npcnum) Then
                damage *= 1.5
                SendActionMsg(mapNum, "Critical!", ColorType.BrightCyan, ActionMsgType.Scroll, (MapNpc(mapNum).Npc(mapNpcNum).X * 32), (MapNpc(mapNum).Npc(mapNpcNum).Y * 32))
            End If
        End If

        If damage > 0 Then
            NpcAttackPet(mapNpcNum, index, damage)
        End If

    End Sub

    Function CanNpcAttackPet(mapNpcNum As Integer, index As Integer) As Boolean
        Dim mapNum As Integer
        Dim npcnum As Integer

        CanNpcAttackPet = False

        If mapNpcNum <= 0 OrElse mapNpcNum > MAX_MAP_NPCS OrElse Not IsPlaying(index) OrElse Not PetAlive(index) Then
            Exit Function
        End If

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(index)).Npc(mapNpcNum).Num <= 0 Then Exit Function

        mapNum = GetPlayerMap(index)
        npcnum = MapNpc(mapNum).Npc(mapNpcNum).Num

        ' Make sure the npc isn't already dead
        If MapNpc(mapNum).Npc(mapNpcNum).Vital(VitalType.HP) <= 0 Then Exit Function

        ' Make sure npcs dont attack more then once a second
        If GetTimeMs() < MapNpc(mapNum).Npc(mapNpcNum).AttackTimer + 1000 Then Exit Function

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(index).GettingMap = 1 Then Exit Function

        MapNpc(mapNum).Npc(mapNpcNum).AttackTimer = GetTimeMs()

        ' Make sure they are on the same map
        If IsPlaying(index) AndAlso PetAlive(index) Then
            If npcnum > 0 Then

                ' Check if at same coordinates
                If (GetPetY(index) + 1 = MapNpc(mapNum).Npc(mapNpcNum).Y) AndAlso (GetPetX(index) = MapNpc(mapNum).Npc(mapNpcNum).X) Then
                    CanNpcAttackPet = True
                Else

                    If (GetPetY(index) - 1 = MapNpc(mapNum).Npc(mapNpcNum).Y) AndAlso (GetPetX(index) = MapNpc(mapNum).Npc(mapNpcNum).X) Then
                        CanNpcAttackPet = True
                    Else

                        If (GetPetY(index) = MapNpc(mapNum).Npc(mapNpcNum).Y) AndAlso (GetPetX(index) + 1 = MapNpc(mapNum).Npc(mapNpcNum).X) Then
                            CanNpcAttackPet = True
                        Else

                            If (GetPetY(index) = MapNpc(mapNum).Npc(mapNpcNum).Y) AndAlso (GetPetX(index) - 1 = MapNpc(mapNum).Npc(mapNpcNum).X) Then
                                CanNpcAttackPet = True
                            End If
                        End If
                    End If
                End If
            End If
        End If

    End Function

    Sub NpcAttackPet(mapnpcnum As Integer, victim As Integer, damage As Integer)
        Dim name As String, mapNum As Integer

        ' Check for subscript out of range
        If mapnpcnum <= 0 OrElse mapnpcnum > MAX_MAP_NPCS OrElse IsPlaying(victim) = False OrElse Not PetAlive(victim) Then
            Exit Sub
        End If

        ' Check for subscript out of range
        If MapNpc(GetPlayerMap(victim)).Npc(mapnpcnum).Num <= 0 Then Exit Sub

        mapNum = GetPlayerMap(victim)
        name = Trim$(Npc(MapNpc(mapNum).Npc(mapnpcnum).Num).Name)

        ' Send this packet so they can see the npc attacking
        SendNpcAttack(victim, mapnpcnum)

        If damage <= 0 Then Exit Sub

        ' set the regen timer
        MapNpc(mapNum).Npc(mapnpcnum).StopRegen = True
        MapNpc(mapNum).Npc(mapnpcnum).StopRegenTimer = GetTimeMs()

        If damage >= GetPetVital(victim, VitalType.HP) Then
            ' Say damage
            SendActionMsg(GetPlayerMap(victim), "-" & GetPetVital(victim, VitalType.HP), ColorType.BrightRed, ActionMsgType.Scroll, (GetPetX(victim) * 32), (GetPetY(victim) * 32))

            ' kill pet
            PlayerMsg(victim, "Your " & Trim$(GetPetName(victim)) & " was killed by a " & Trim$(Npc(MapNpc(mapNum).Npc(mapnpcnum).Num).Name) & ".", ColorType.BrightRed)
            ReCallPet(victim)

            ' Now that pet is dead, go for owner
            MapNpc(mapNum).Npc(mapnpcnum).Target = victim
            MapNpc(mapNum).Npc(mapnpcnum).TargetType = TargetType.Player
        Else
            ' Pet not dead, just do the damage
            SetPetVital(victim, VitalType.HP, GetPetVital(victim, VitalType.HP) - damage)
            SendPetVital(victim, VitalType.HP)
            SendAnimation(mapNum, Npc(MapNpc(GetPlayerMap(victim)).Npc(mapnpcnum).Num).Animation, 0, 0, TargetType.Pet, victim)

            ' Say damage
            SendActionMsg(GetPlayerMap(victim), "-" & damage, ColorType.BrightRed, ActionMsgType.Scroll, (GetPetX(victim) * 32), (GetPetY(victim) * 32))
            SendBlood(GetPlayerMap(victim), GetPetX(victim), GetPetY(victim))

            ' set the regen timer
            TempPlayer(victim).PetstopRegen = True
            TempPlayer(victim).PetstopRegenTimer = GetTimeMs()

            'pet gets attacked, lets set this target
            TempPlayer(victim).PetTarget = mapnpcnum
            TempPlayer(victim).PetTargetType = TargetType.Npc
        End If

    End Sub

#End Region

#Region "Pet > Player"

    Function CanPetAttackPlayer(attacker As Integer, victim As Integer, Optional isSkill As Boolean = False) As Boolean

        If Not isSkill Then
            If GetTimeMs() < TempPlayer(attacker).PetAttackTimer + 1000 Then Exit Function
        End If

        ' Check for subscript out of range
        If Not IsPlaying(victim) Then Exit Function

        ' Make sure they are on the same map
        If Not GetPlayerMap(attacker) = GetPlayerMap(victim) Then Exit Function

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(victim).GettingMap = 1 Then Exit Function

        If TempPlayer(attacker).PetskillBuffer.Skill > 0 AndAlso isSkill = False Then Exit Function

        If Not isSkill Then
            ' Check if at same coordinates
            Select Case GetPetDir(attacker)
                Case DirectionType.Up
                    If Not (GetPlayerY(victim) + 1 = GetPetY(attacker)) AndAlso (GetPlayerX(victim) = GetPetX(attacker)) Then Exit Function

                Case DirectionType.Down
                    If Not (GetPlayerY(victim) - 1 = GetPetY(attacker)) AndAlso (GetPlayerX(victim) = GetPetX(attacker)) Then Exit Function

                Case DirectionType.Left
                    If Not (GetPlayerY(victim) = GetPetY(attacker)) AndAlso (GetPlayerX(victim) + 1 = GetPetX(attacker)) Then Exit Function

                Case DirectionType.Right
                    If Not (GetPlayerY(victim) = GetPetY(attacker)) AndAlso (GetPlayerX(victim) - 1 = GetPetX(attacker)) Then Exit Function

                Case Else
                    Exit Function
            End Select
        End If

        ' Check if map is attackable
        If Not Map(GetPlayerMap(attacker)).Moral = MapMoralType.None Then
            If GetPlayerPK(victim) = 0 Then
                Exit Function
            End If
        End If

        ' Make sure they have more then 0 hp
        If GetPlayerVital(victim, VitalType.HP) <= 0 Then Exit Function

        ' Check to make sure that they dont have access
        If GetPlayerAccess(attacker) > AdminType.Monitor Then
            PlayerMsg(attacker, "Admins cannot attack other players.", ColorType.Yellow)
            Exit Function
        End If

        ' Check to make sure the victim isn't an admin
        If GetPlayerAccess(victim) > AdminType.Monitor Then
            PlayerMsg(attacker, "You cannot attack " & GetPlayerName(victim) & "!", ColorType.Yellow)
            Exit Function
        End If

        ' Don't attack a party member
        If TempPlayer(attacker).InParty > 0 AndAlso TempPlayer(victim).InParty > 0 Then
            If TempPlayer(attacker).InParty = TempPlayer(victim).InParty Then
                PlayerMsg(attacker, "You can't attack another party member!", ColorType.Yellow)
                Exit Function
            End If
        End If

        CanPetAttackPlayer = True

    End Function

    Sub PetAttackPlayer(attacker As Integer, victim As Integer, damage As Integer, Optional skillNum As Integer = 0)
        Dim exp As Integer, i As Integer

        ' Check for subscript out of range

        If IsPlaying(attacker) = False OrElse IsPlaying(victim) = False OrElse damage < 0 OrElse PetAlive(attacker) = False Then
            Exit Sub
        End If

        If skillNum = 0 Then
            ' Send this packet so they can see the pet attacking
            SendPetAttack(attacker, victim)
        End If

        ' set the regen timer
        TempPlayer(attacker).PetstopRegen = True
        TempPlayer(attacker).PetstopRegenTimer = GetTimeMs()

        If damage >= GetPlayerVital(victim, VitalType.HP) Then
            SendActionMsg(GetPlayerMap(victim), "-" & GetPlayerVital(victim, VitalType.HP), ColorType.BrightRed, 1, (GetPlayerX(victim) * 32), (GetPlayerY(victim) * 32))

            ' send the sound
            'If SkillNum > 0 Then SendMapSound(Victim, GetPlayerX(Victim), GetPlayerY(Victim), SoundEntity.seSpell, SkillNum)

            ' Player is dead
            GlobalMsg(GetPlayerName(victim) & " has been killed by " & GetPlayerName(attacker) & "'s " & Trim$(GetPetName(attacker)) & ".")

            ' Calculate exp to give attacker
            exp = (GetPlayerExp(victim) \ 10)

            ' Make sure we dont get less then 0
            If exp < 0 Then
                exp = 0
            End If

            If exp = 0 Then
                PlayerMsg(victim, "You lost no exp.", ColorType.BrightGreen)
                PlayerMsg(attacker, "You received no exp.", ColorType.BrightRed)
            Else
                SetPlayerExp(victim, GetPlayerExp(victim) - exp)
                SendExp(victim)
                PlayerMsg(victim, "You lost " & exp & " exp.", ColorType.BrightRed)

                ' check if we're in a party
                If TempPlayer(attacker).InParty > 0 Then
                    ' pass through party exp share function
                    Party_ShareExp(TempPlayer(attacker).InParty, exp, attacker, GetPlayerMap(attacker))
                Else
                    ' not in party, get exp for self
                    GivePlayerExp(attacker, exp)
                End If
            End If

            ' purge target info of anyone who targetted dead guy
            For i = 1 To Socket.HighIndex

                If IsPlaying(i) AndAlso Socket.IsConnected(i) Then
                    If GetPlayerMap(i) = GetPlayerMap(attacker) Then
                        If TempPlayer(i).TargetType = TargetType.Player Then
                            If TempPlayer(i).Target = victim Then
                                TempPlayer(i).Target = 0
                                TempPlayer(i).TargetType = TargetType.None
                                SendTarget(i, 0, TargetType.None)
                            End If
                        End If

                        If Player(i).Character(TempPlayer(i).CurChar).Pet.Alive = 1 Then
                            If TempPlayer(i).PetTargetType = TargetType.Player Then
                                If TempPlayer(i).PetTarget = victim Then
                                    TempPlayer(i).PetTarget = 0
                                    TempPlayer(i).PetTargetType = TargetType.None
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            If GetPlayerPK(victim) = 0 Then
                If GetPlayerPK(attacker) = 0 Then
                    SetPlayerPK(attacker, 1)
                    SendPlayerData(attacker)
                    GlobalMsg(GetPlayerName(attacker) & " has been deemed a Player Killer!!!")
                End If
            Else
                GlobalMsg(GetPlayerName(victim) & " has paid the price for being a Player Killer!!!")
            End If

            OnDeath(victim)
        Else
            ' Player not dead, just do the damage
            SetPlayerVital(victim, VitalType.HP, GetPlayerVital(victim, VitalType.HP) - damage)
            SendVital(victim, VitalType.HP)

            ' send vitals to party if in one
            If TempPlayer(victim).InParty > 0 Then SendPartyVitals(TempPlayer(victim).InParty, victim)

            ' send the sound
            'If SkillNum > 0 Then SendMapSound(Victim, GetPlayerX(Victim), GetPlayerY(Victim), SoundEntity.seSpell, SkillNum)

            SendActionMsg(GetPlayerMap(victim), "-" & damage, ColorType.BrightRed, 1, (GetPlayerX(victim) * 32), (GetPlayerY(victim) * 32))
            SendBlood(GetPlayerMap(victim), GetPlayerX(victim), GetPlayerY(victim))

            ' set the regen timer
            TempPlayer(victim).StopRegen = True
            TempPlayer(victim).StopRegenTimer = GetTimeMs()

            'if a stunning spell, stun the player
            If skillNum > 0 Then
                If Skill(skillNum).StunDuration > 0 Then StunPlayer(victim, skillNum)

                ' DoT
                If Skill(skillNum).Duration > 0 Then
                    'AddDoT_Player(Victim, SkillNum, Attacker)
                End If
            End If
        End If

        ' Reset attack timer
        TempPlayer(attacker).PetAttackTimer = GetTimeMs()

    End Sub

    Friend Sub TryPetAttackPlayer(index As Integer, victim As Integer)
        Dim mapNum As Integer, blockAmount As Integer, damage As Integer

        If GetPlayerMap(index) <> GetPlayerMap(victim) Then Exit Sub

        If Not PetAlive(index) Then Exit Sub

        ' Can the npc attack the player?
        If CanPetAttackPlayer(index, victim) Then
            mapNum = GetPlayerMap(index)

            ' check if PLAYER can avoid the attack
            If CanPlayerDodge(victim) Then
                SendActionMsg(mapNum, "Dodge!", ColorType.Pink, 1, (GetPlayerX(victim) * 32), (GetPlayerY(victim) * 32))
                Exit Sub
            End If

            If CanPlayerParry(victim) Then
                SendActionMsg(mapNum, "Parry!", ColorType.Pink, 1, (GetPlayerX(victim) * 32), (GetPlayerY(victim) * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            damage = GetPetDamage(index)

            ' if the player blocks, take away the block amount
            blockAmount = CanPlayerBlockHit(victim)
            damage -= blockAmount

            ' take away armour
            damage -= Random(1, (GetPetStat(index, StatType.Luck)) * 2)

            ' randomise for up to 10% lower than max hit
            damage = Random(1, damage)

            ' * 1.5 if crit hit
            If CanPetCrit(index) Then
                damage *= 1.5
                SendActionMsg(mapNum, "Critical!", ColorType.BrightCyan, 1, (GetPetX(index) * 32), (GetPetY(index) * 32))
            End If

            If damage > 0 Then
                PetAttackPlayer(index, victim, damage)
            End If

        End If

    End Sub

#End Region

#Region "Pet > Pet"

    Function CanPetAttackPet(attacker As Integer, victim As Integer, Optional isSkill As Integer = 0) As Boolean

        If Not isSkill Then
            If GetTimeMs() < TempPlayer(attacker).PetAttackTimer + 1000 Then Exit Function
        End If

        ' Check for subscript out of range
        If Not IsPlaying(victim) OrElse Not IsPlaying(attacker) Then Exit Function

        ' Make sure they are on the same map
        If Not GetPlayerMap(attacker) = GetPlayerMap(victim) Then Exit Function

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(victim).GettingMap = 1 Then Exit Function

        If TempPlayer(attacker).PetskillBuffer.Skill > 0 AndAlso isSkill = False Then Exit Function

        If Not isSkill Then

            ' Check if at same coordinates
            Select Case GetPetDir(attacker)
                Case DirectionType.Up
                    If Not ((GetPetY(victim) - 1 = GetPetY(attacker)) AndAlso (GetPetX(victim) = GetPetX(attacker))) Then Exit Function

                Case DirectionType.Down
                    If Not ((GetPetY(victim) + 1 = GetPetY(attacker)) AndAlso (GetPetX(victim) = GetPetX(attacker))) Then Exit Function

                Case DirectionType.Left
                    If Not ((GetPetY(victim) = GetPetY(attacker)) AndAlso (GetPetX(victim) + 1 = GetPetX(attacker))) Then Exit Function

                Case DirectionType.Right
                    If Not ((GetPetY(victim) = GetPetY(attacker)) AndAlso (GetPetX(victim) - 1 = GetPetX(attacker))) Then Exit Function

                Case Else
                    Exit Function
            End Select
        End If

        ' Check if map is attackable
        If Not Map(GetPlayerMap(attacker)).Moral = MapMoralType.None Then
            If GetPlayerPK(victim) = 0 Then
                Exit Function
            End If
        End If

        ' Make sure they have more then 0 hp
        If Player(victim).Character(TempPlayer(victim).CurChar).Pet.Health <= 0 Then Exit Function

        ' Check to make sure that they dont have access
        If GetPlayerAccess(attacker) > AdminType.Monitor Then
            PlayerMsg(attacker, "Admins cannot attack other players.", ColorType.BrightRed)
            Exit Function
        End If

        ' Check to make sure the victim isn't an admin
        If GetPlayerAccess(victim) > AdminType.Monitor Then
            PlayerMsg(attacker, "You cannot attack " & GetPlayerName(victim) & "!", ColorType.BrightRed)
            Exit Function
        End If

        ' Don't attack a party member
        If TempPlayer(attacker).InParty > 0 AndAlso TempPlayer(victim).InParty > 0 Then
            If TempPlayer(attacker).InParty = TempPlayer(victim).InParty Then
                PlayerMsg(attacker, "You can't attack another party member!", ColorType.BrightRed)
                Exit Function
            End If
        End If

        If TempPlayer(attacker).InParty > 0 AndAlso TempPlayer(victim).InParty > 0 AndAlso TempPlayer(attacker).InParty = TempPlayer(victim).InParty Then
            If isSkill > 0 Then
                If Skill(isSkill).Type = SkillType.HealMp OrElse Skill(isSkill).Type = SkillType.HealHp Then
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
        Dim exp As Integer, i As Integer

        ' Check for subscript out of range

        If IsPlaying(attacker) = False OrElse IsPlaying(victim) = False OrElse damage < 0 OrElse PetAlive(attacker) = False OrElse PetAlive(victim) = False Then
            Exit Sub
        End If

        If skillnum = 0 Then
            ' Send this packet so they can see the pet attacking
            SendPetAttack(attacker, victim)
        End If

        ' set the regen timer
        TempPlayer(attacker).PetstopRegen = True
        TempPlayer(attacker).PetstopRegenTimer = GetTimeMs()

        If damage >= GetPetVital(victim, VitalType.HP) Then
            SendActionMsg(GetPlayerMap(victim), "-" & GetPetVital(victim, VitalType.HP), ColorType.BrightRed, ActionMsgType.Scroll, (GetPetX(victim) * 32), (GetPetY(victim) * 32))

            ' send the sound
            'If Spellnum > 0 Then SendMapSound Victim, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.x, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.y, SoundEntity.seSpell, Spellnum

            ' Player is dead
            GlobalMsg(GetPlayerName(victim) & " has been killed by " & GetPlayerName(attacker) & "'s " & Trim$(GetPetName(attacker)) & ".")

            ' Calculate exp to give attacker
            exp = (GetPlayerExp(victim) \ 10)

            ' Make sure we dont get less then 0
            If exp < 0 Then exp = 0

            If exp = 0 Then
                PlayerMsg(victim, "You lost no exp.", ColorType.BrightGreen)
                PlayerMsg(attacker, "You received no exp.", ColorType.Yellow)
            Else
                SetPlayerExp(victim, GetPlayerExp(victim) - exp)
                SendExp(victim)
                PlayerMsg(victim, "You lost " & exp & " exp.", ColorType.BrightRed)

                ' check if we're in a party
                If TempPlayer(attacker).InParty > 0 Then
                    ' pass through party exp share function
                    Party_ShareExp(TempPlayer(attacker).InParty, exp, attacker, GetPlayerMap(attacker))
                Else
                    ' not in party, get exp for self
                    GivePlayerExp(attacker, exp)
                End If
            End If

            ' purge target info of anyone who targetted dead guy
            For i = 1 To Socket.HighIndex

                If IsPlaying(i) AndAlso Socket.IsConnected(i) Then
                    If GetPlayerMap(i) = GetPlayerMap(attacker) Then
                        If TempPlayer(i).TargetType = TargetType.Player Then
                            If TempPlayer(i).Target = victim Then
                                TempPlayer(i).Target = 0
                                TempPlayer(i).TargetType = TargetType.None
                                SendTarget(i, 0, TargetType.None)
                            End If
                        End If

                        If PetAlive(i) Then
                            If TempPlayer(i).PetTargetType = TargetType.Player Then
                                If TempPlayer(i).PetTarget = victim Then
                                    TempPlayer(i).PetTarget = 0
                                    TempPlayer(i).PetTargetType = TargetType.None
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            If GetPlayerPK(victim) = 0 Then
                If GetPlayerPK(attacker) = 0 Then
                    SetPlayerPK(attacker, 1)
                    SendPlayerData(attacker)
                    GlobalMsg(GetPlayerName(attacker) & " has been deemed a Player Killer!!!")
                End If
            Else
                GlobalMsg(GetPlayerName(victim) & " has paid the price for being a Player Killer!!!")
            End If

            ' kill pet
            PlayerMsg(victim, "Your " & Trim$(GetPetName(victim)) & " was killed by " & Trim$(GetPlayerName(attacker)) & "'s " & Trim$(GetPetName(attacker)) & "!", ColorType.BrightRed)
            ReleasePet(victim)
        Else
            ' Player not dead, just do the damage
            SetPetVital(victim, VitalType.HP, GetPetVital(victim, VitalType.HP) - damage)
            SendPetVital(victim, VitalType.HP)

            'Set pet to begin attacking the other pet if it isn't dead or dosent have another target
            If TempPlayer(victim).PetTarget <= 0 AndAlso TempPlayer(victim).PetBehavior <> PetBehaviourGoto Then
                TempPlayer(victim).PetTarget = attacker
                TempPlayer(victim).PetTargetType = TargetType.Pet
            End If

            ' send the sound
            'If Spellnum > 0 Then SendMapSound Victim, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.x, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.y, SoundEntity.seSpell, Spellnum

            SendActionMsg(GetPlayerMap(victim), "-" & damage, ColorType.BrightRed, 1, (GetPetX(victim) * 32), (GetPetY(victim) * 32))
            SendBlood(GetPlayerMap(victim), GetPetX(victim), GetPetY(victim))

            ' set the regen timer
            TempPlayer(victim).PetstopRegen = True
            TempPlayer(victim).PetstopRegenTimer = GetTimeMs()

            'if a stunning spell, stun the player
            If skillnum > 0 Then
                If Skill(skillnum).StunDuration > 0 Then StunPet(victim, skillnum)
                ' DoT
                If Skill(skillnum).Duration > 0 Then
                    'AddDoT_Pet(Victim, Skillnum, Attacker, TargetType.Pet)
                End If
            End If
        End If

        ' Reset attack timer
        TempPlayer(attacker).PetAttackTimer = GetTimeMs()

    End Sub

    Friend Sub TryPetAttackPet(index As Integer, victim As Integer)
        Dim mapNum As Integer, blockAmount As Integer, damage As Integer

        If GetPlayerMap(index) <> GetPlayerMap(victim) Then Exit Sub

        If Not PetAlive(index) OrElse Not PetAlive(victim) Then Exit Sub

        ' Can the npc attack the player?
        If CanPetAttackPet(index, victim) Then
            mapNum = GetPlayerMap(index)

            ' check if Pet can avoid the attack
            If CanPetDodge(victim) Then
                SendActionMsg(mapNum, "Dodge!", ColorType.Pink, 1, (GetPetX(victim) * 32), (GetPetY(victim) * 32))
                Exit Sub
            End If

            If CanPetParry(victim) Then
                SendActionMsg(mapNum, "Parry!", ColorType.Pink, 1, (GetPetX(victim) * 32), (GetPetY(victim) * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            damage = GetPetDamage(index)

            ' if the player blocks, take away the block amount
            damage -= blockAmount

            ' take away armour
            damage -= Random(1, (Player(index).Character(TempPlayer(index).CurChar).Pet.Stat(StatType.Luck) * 2))

            ' randomise for up to 10% lower than max hit
            damage = Random(1, damage)

            ' * 1.5 if crit hit
            If CanPetCrit(index) Then
                damage *= 1.5
                SendActionMsg(mapNum, "Critical!", ColorType.BrightCyan, 1, (GetPetX(index) * 32), (GetPetY(index) * 32))
            End If

            If damage > 0 Then
                PetAttackPet(index, victim, damage)
            End If

        End If

    End Sub

#End Region

#Region "Skills"

    Friend Sub BufferPetSkill(index As Integer, skillSlot As Integer)
        Dim skillnum As Integer, mpCost As Integer, levelReq As Integer
        Dim mapNum As Integer, skillCastType As Integer
        Dim accessReq As Integer, range As Integer, hasBuffered As Boolean
        Dim targetTypes As Byte, target As Integer

        ' Prevent subscript out of range

        If skillSlot <= 0 OrElse skillSlot > 4 Then Exit Sub

        skillnum = Player(index).Character(TempPlayer(index).CurChar).Pet.Skill(skillSlot)
        mapNum = GetPlayerMap(index)

        If skillnum <= 0 OrElse skillnum > MAX_SKILLS Then Exit Sub

        ' see if cooldown has finished
        If TempPlayer(index).PetSkillCd(skillSlot) > GetTimeMs() Then
            PlayerMsg(index, Trim$(GetPetName(index)) & "'s Skill hasn't cooled down yet!", ColorType.BrightRed)
            Exit Sub
        End If

        mpCost = Skill(skillnum).MpCost

        ' Check if they have enough MP
        If GetPetVital(index, VitalType.MP) < mpCost Then
            PlayerMsg(index, "Your " & Trim$(GetPetName(index)) & " does not have enough mana!", ColorType.BrightRed)
            Exit Sub
        End If

        levelReq = Skill(skillnum).LevelReq

        ' Make sure they are the right level
        If levelReq > GetPetLevel(index) Then
            PlayerMsg(index, Trim$(GetPetName(index)) & " must be level " & levelReq & " to cast this skill.", ColorType.BrightRed)
            Exit Sub
        End If

        accessReq = Skill(skillnum).AccessReq

        ' make sure they have the right access
        If accessReq > GetPlayerAccess(index) Then
            PlayerMsg(index, "You must be an administrator to cast this spell, even as a pet owner.", ColorType.BrightRed)
            Exit Sub
        End If

        ' find out what kind of spell it is! self cast, target or AOE
        If Skill(skillnum).Range > 0 Then

            ' ranged attack, single target or aoe?
            If Not Skill(skillnum).IsAoE Then
                skillCastType = 2 ' targetted
            Else
                skillCastType = 3 ' targetted aoe
            End If
        Else
            If Not Skill(skillnum).IsAoE Then
                skillCastType = 0 ' self-cast
            Else
                skillCastType = 1 ' self-cast AoE
            End If
        End If

        targetTypes = TempPlayer(index).PetTargetType
        target = TempPlayer(index).PetTarget
        range = Skill(skillnum).Range
        hasBuffered = False

        Select Case skillCastType

            'PET
            Case 0, 1, SkillType.Pet ' self-cast & self-cast AOE
                hasBuffered = True

            Case 2, 3 ' targeted & targeted AOE

                ' check if have target
                If Not target > 0 Then
                    If skillCastType = SkillType.HealHp OrElse skillCastType = SkillType.HealMp Then
                        target = index
                        targetTypes = TargetType.Pet
                    Else
                        PlayerMsg(index, "Your " & Trim$(GetPetName(index)) & " does not have a target.", ColorType.Yellow)
                    End If
                End If

                If targetTypes = TargetType.Player Then

                    ' if have target, check in range
                    If Not IsInRange(range, GetPetX(index), GetPetY(index), GetPlayerX(target), GetPlayerY(target)) Then
                        PlayerMsg(index, "Target not in range of " & Trim$(GetPetName(index)) & ".", ColorType.Yellow)
                    Else
                        ' go through spell types
                        If Skill(skillnum).Type <> SkillType.DamageHp AndAlso Skill(skillnum).Type <> SkillType.DamageMp Then
                            hasBuffered = True
                        Else
                            If CanPetAttackPlayer(index, target, True) Then
                                hasBuffered = True
                            End If
                        End If
                    End If

                ElseIf targetTypes = TargetType.Npc Then

                    ' if have target, check in range
                    If Not IsInRange(range, GetPetX(index), GetPetY(index), MapNpc(mapNum).Npc(target).X, MapNpc(mapNum).Npc(target).Y) Then
                        PlayerMsg(index, "Target not in range of " & Trim$(GetPetName(index)) & ".", ColorType.Yellow)
                        hasBuffered = False
                    Else
                        ' go through spell types
                        If Skill(skillnum).Type <> SkillType.DamageHp AndAlso Skill(skillnum).Type <> SkillType.DamageMp Then
                            hasBuffered = True
                        Else
                            If CanPetAttackNpc(index, target, True) Then
                                hasBuffered = True
                            End If
                        End If
                    End If

                    'PET
                ElseIf targetTypes = TargetType.Pet Then

                    ' if have target, check in range
                    If Not IsInRange(range, GetPetX(index), GetPetY(index), GetPetX(target), GetPetY(target)) Then
                        PlayerMsg(index, "Target not in range of " & GetPetName(index).Trim & ".", ColorType.Yellow)
                        hasBuffered = False
                    Else
                        ' go through spell types
                        If Skill(skillnum).Type <> SkillType.DamageHp AndAlso Skill(skillnum).Type <> SkillType.DamageMp Then
                            hasBuffered = True
                        Else
                            If CanPetAttackPet(index, target, skillnum) Then
                                hasBuffered = True
                            End If
                        End If
                    End If
                End If
        End Select

        If hasBuffered Then
            SendAnimation(mapNum, Skill(skillnum).CastAnim, 0, 0, TargetType.Pet, index)
            SendActionMsg(mapNum, "Casting " & Trim$(Skill(skillnum).Name) & "!", ColorType.BrightRed, ActionMsgType.Scroll, GetPetX(index) * 32, GetPetY(index) * 32)
            TempPlayer(index).PetskillBuffer.Skill = skillSlot
            TempPlayer(index).PetskillBuffer.Timer = GetTimeMs()
            TempPlayer(index).PetskillBuffer.Target = target
            TempPlayer(index).PetskillBuffer.TargetTypes = targetTypes
            Exit Sub
        Else
            SendClearPetSpellBuffer(index)
        End If

    End Sub

    Friend Sub PetCastSkill(index As Integer, skillslot As Integer, target As Integer, targetTypes As Byte, Optional takeMana As Boolean = True)
        Dim skillnum As Integer, mpCost As Integer, levelReq As Integer
        Dim mapNum As Integer, vital As Integer, didCast As Boolean
        Dim accessReq As Integer, i As Integer
        Dim aoE As Integer, range As Integer, vitalType As Byte
        Dim increment As Boolean, x As Integer, y As Integer
        Dim skillCastType As Integer

        didCast = False

        ' Prevent subscript out of range
        If skillslot <= 0 OrElse skillslot > 4 Then Exit Sub

        skillnum = Player(index).Character(TempPlayer(index).CurChar).Pet.Skill(skillslot)
        mapNum = GetPlayerMap(index)

        mpCost = Skill(skillnum).MpCost

        ' Check if they have enough MP
        If Player(index).Character(TempPlayer(index).CurChar).Pet.Mana < mpCost Then
            PlayerMsg(index, "Your " & Trim$(GetPetName(index)) & " does not have enough mana!", ColorType.BrightRed)
            Exit Sub
        End If

        levelReq = Skill(skillnum).LevelReq

        ' Make sure they are the right level
        If levelReq > Player(index).Character(TempPlayer(index).CurChar).Pet.Level Then
            PlayerMsg(index, Trim$(GetPetName(index)) & " must be level " & levelReq & " to cast this spell.", ColorType.BrightRed)
            Exit Sub
        End If

        accessReq = Skill(skillnum).AccessReq

        ' make sure they have the right access
        If accessReq > GetPlayerAccess(index) Then
            PlayerMsg(index, "You must be an administrator for even your pet to cast this spell.", ColorType.BrightRed)
            Exit Sub
        End If

        ' find out what kind of spell it is! self cast, target or AOE
        If Skill(skillnum).IsProjectile = True Then
            skillCastType = 4 ' Projectile
        ElseIf Skill(skillnum).Range > 0 Then
            ' ranged attack, single target or aoe?
            If Not Skill(skillnum).IsAoE Then
                skillCastType = 2 ' targetted
            Else
                skillCastType = 3 ' targetted aoe
            End If
        Else
            If Not Skill(skillnum).IsAoE Then
                skillCastType = 0 ' self-cast
            Else
                skillCastType = 1 ' self-cast AoE
            End If
        End If

        ' set the vital
        vital = Skill(skillnum).Vital
        aoE = Skill(skillnum).AoE
        range = Skill(skillnum).Range

        Select Case skillCastType
            Case 0 ' self-cast target
                Select Case Skill(skillnum).Type
                    Case SkillType.HealHp
                        SkillPet_Effect(modEnumerators.VitalType.HP, True, index, vital, skillnum)
                        didCast = True
                    Case SkillType.HealMp
                        SkillPet_Effect(modEnumerators.VitalType.MP, True, index, vital, skillnum)
                        didCast = True
                End Select

            Case 1, 3 ' self-cast AOE & targetted AOE

                If skillCastType = 1 Then
                    x = GetPetX(index)
                    y = GetPetY(index)
                ElseIf skillCastType = 3 Then

                    If targetTypes = 0 Then Exit Sub
                    If target = 0 Then Exit Sub

                    If targetTypes = TargetType.Player Then
                        x = GetPlayerX(target)
                        y = GetPlayerY(target)
                    ElseIf targetTypes = TargetType.Npc Then
                        x = MapNpc(mapNum).Npc(target).X
                        y = MapNpc(mapNum).Npc(target).Y
                    ElseIf targetTypes = TargetType.Pet Then
                        x = GetPetX(target)
                        y = GetPetY(target)
                    End If

                    If Not IsInRange(range, GetPetX(index), GetPetY(index), x, y) Then
                        PlayerMsg(index, Trim$(GetPetName(index)) & "'s target not in range.", ColorType.Yellow)
                        SendClearPetSpellBuffer(index)
                    End If
                End If

                Select Case Skill(skillnum).Type

                    Case SkillType.DamageHp
                        didCast = True

                        For i = 1 To GetPlayersOnline()
                            If IsPlaying(i) AndAlso i <> index Then
                                If GetPlayerMap(i) = GetPlayerMap(index) Then
                                    If IsInRange(aoE, x, y, GetPlayerX(i), GetPlayerY(i)) Then
                                        If CanPetAttackPlayer(index, i, True) AndAlso index <> target Then
                                            SendAnimation(mapNum, Skill(skillnum).SkillAnim, 0, 0, TargetType.Player, i)
                                            PetAttackPlayer(index, i, vital, skillnum)
                                        End If
                                    End If

                                    If PetAlive(i) Then
                                        If IsInRange(aoE, x, y, GetPetX(i), GetPetY(i)) Then

                                            If CanPetAttackPet(index, i, skillnum) Then
                                                SendAnimation(mapNum, Skill(skillnum).SkillAnim, 0, 0, TargetType.Pet, i)
                                                PetAttackPet(index, i, vital, skillnum)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next

                        For i = 1 To MAX_MAP_NPCS
                            If MapNpc(mapNum).Npc(i).Num > 0 AndAlso MapNpc(mapNum).Npc(i).Vital(modEnumerators.VitalType.HP) > 0 Then
                                If IsInRange(aoE, x, y, MapNpc(mapNum).Npc(i).X, MapNpc(mapNum).Npc(i).Y) Then
                                    If CanPetAttackNpc(index, i, True) Then
                                        SendAnimation(mapNum, Skill(skillnum).SkillAnim, 0, 0, TargetType.Npc, i)
                                        PetAttackNpc(index, i, vital, skillnum)
                                    End If
                                End If
                            End If
                        Next

                    Case SkillType.HealHp, SkillType.HealMp, SkillType.DamageMp

                        If Skill(skillnum).Type = SkillType.HealHp Then
                            vitalType = modEnumerators.VitalType.HP
                            increment = True
                        ElseIf Skill(skillnum).Type = SkillType.HealMp Then
                            vitalType = modEnumerators.VitalType.MP
                            increment = True
                        ElseIf Skill(skillnum).Type = SkillType.DamageMp Then
                            vitalType = modEnumerators.VitalType.MP
                            increment = False
                        End If

                        didCast = True

                        For i = 1 To GetPlayersOnline()
                            If IsPlaying(i) AndAlso GetPlayerMap(i) = GetPlayerMap(index) Then
                                If IsInRange(aoE, x, y, GetPlayerX(i), GetPlayerY(i)) Then
                                    SpellPlayer_Effect(vitalType, increment, i, vital, skillnum)
                                End If

                                If PetAlive(i) Then
                                    If IsInRange(aoE, x, y, GetPetX(i), GetPetY(i)) Then
                                        SkillPet_Effect(vitalType, increment, i, vital, skillnum)
                                    End If
                                End If
                            End If
                        Next
                End Select

            Case 2 ' targetted

                If targetTypes = 0 Then Exit Sub
                If target = 0 Then Exit Sub

                If targetTypes = TargetType.Player Then
                    x = GetPlayerX(target)
                    y = GetPlayerY(target)
                ElseIf targetTypes = TargetType.Npc Then
                    x = MapNpc(mapNum).Npc(target).X
                    y = MapNpc(mapNum).Npc(target).Y
                ElseIf targetTypes = TargetType.Pet Then
                    x = GetPetX(target)
                    y = GetPetY(target)
                End If

                If Not IsInRange(range, GetPetX(index), GetPetY(index), x, y) Then
                    PlayerMsg(index, "Target is not in range of your " & Trim$(GetPetName(index)) & "!", ColorType.Yellow)
                    SendClearPetSpellBuffer(index)
                    Exit Sub
                End If

                Select Case Skill(skillnum).Type

                    Case SkillType.DamageHp

                        If targetTypes = TargetType.Player Then
                            If CanPetAttackPlayer(index, target, True) AndAlso index <> target Then
                                If vital > 0 Then
                                    SendAnimation(mapNum, Skill(skillnum).SkillAnim, 0, 0, TargetType.Player, target)
                                    PetAttackPlayer(index, target, vital, skillnum)
                                    didCast = True
                                End If
                            End If
                        ElseIf targetTypes = TargetType.Npc Then
                            If CanPetAttackNpc(index, target, True) Then
                                If vital > 0 Then
                                    SendAnimation(mapNum, Skill(skillnum).SkillAnim, 0, 0, TargetType.Npc, target)
                                    PetAttackNpc(index, target, vital, skillnum)
                                    didCast = True
                                End If
                            End If
                        ElseIf targetTypes = TargetType.Pet Then
                            If CanPetAttackPet(index, target, skillnum) Then
                                If vital > 0 Then
                                    SendAnimation(mapNum, Skill(skillnum).SkillAnim, 0, 0, TargetType.Pet, target)
                                    PetAttackPet(index, target, vital, skillnum)
                                    didCast = True
                                End If
                            End If
                        End If

                    Case SkillType.DamageMp, SkillType.HealMp, SkillType.HealHp

                        If Skill(skillnum).Type = SkillType.DamageMp Then
                            vitalType = modEnumerators.VitalType.MP
                            increment = False
                        ElseIf Skill(skillnum).Type = SkillType.HealMp Then
                            vitalType = modEnumerators.VitalType.MP
                            increment = True
                        ElseIf Skill(skillnum).Type = SkillType.HealHp Then
                            vitalType = modEnumerators.VitalType.HP
                            increment = True
                        End If

                        If targetTypes = TargetType.Player Then
                            If Skill(skillnum).Type = SkillType.DamageMp Then
                                If CanPetAttackPlayer(index, target, True) Then
                                    SpellPlayer_Effect(vitalType, increment, target, vital, skillnum)
                                End If
                            Else
                                SpellPlayer_Effect(vitalType, increment, target, vital, skillnum)
                            End If

                        ElseIf targetTypes = TargetType.Npc Then

                            If Skill(skillnum).Type = SkillType.DamageMp Then
                                If CanPetAttackNpc(index, target, True) Then
                                    SpellNpc_Effect(vitalType, increment, target, vital, skillnum, mapNum)
                                End If
                            Else
                                If Skill(skillnum).Type = SkillType.HealHp OrElse Skill(skillnum).Type = SkillType.HealMp Then
                                    SkillPet_Effect(vitalType, increment, index, vital, skillnum)
                                Else
                                    SpellNpc_Effect(vitalType, increment, target, vital, skillnum, mapNum)
                                End If
                            End If

                        ElseIf targetTypes = TargetType.Pet Then

                            If Skill(skillnum).Type = SkillType.DamageMp Then
                                If CanPetAttackPet(index, target, skillnum) Then
                                    SkillPet_Effect(vitalType, increment, target, vital, skillnum)
                                End If
                            Else
                                SkillPet_Effect(vitalType, increment, target, vital, skillnum)
                                SendPetVital(target, vital)
                            End If
                        End If
                End Select

            Case 4 ' Projectile
                PetFireProjectile(index, skillnum)
                didCast = True
        End Select

        If didCast Then
            If takeMana Then SetPetVital(index, modEnumerators.VitalType.MP, GetPetVital(index, modEnumerators.VitalType.MP) - mpCost)
            SendPetVital(index, modEnumerators.VitalType.MP)
            SendPetVital(index, modEnumerators.VitalType.HP)

            TempPlayer(index).PetSkillCd(skillslot) = GetTimeMs() + (Skill(skillnum).CdTime * 1000)

            SendActionMsg(mapNum, Trim$(Skill(skillnum).Name) & "!", ColorType.BrightRed, ActionMsgType.Scroll, GetPetX(index) * 32, GetPetY(index) * 32)
        End If

    End Sub

    Friend Sub SkillPet_Effect(vital As Byte, increment As Boolean, index As Integer, damage As Integer, skillnum As Integer)
        Dim sSymbol As String
        Dim colour As Integer

        If damage > 0 Then
            If increment Then
                sSymbol = "+"
                If vital = VitalType.HP Then colour = ColorType.BrightGreen
                If vital = VitalType.MP Then colour = ColorType.BrightBlue
            Else
                sSymbol = "-"
                colour = ColorType.Blue
            End If

            SendAnimation(GetPlayerMap(index), Skill(skillnum).SkillAnim, 0, 0, TargetType.Pet, index)
            SendActionMsg(GetPlayerMap(index), sSymbol & damage, colour, ActionMsgType.Scroll, GetPetX(index) * 32, GetPetY(index) * 32)

            ' send the sound
            'SendMapSound(Index, Player(Index).Character(TempPlayer(Index).CurChar).Pet.x, Player(Index).Character(TempPlayer(Index).CurChar).Pet.y, SoundEntity.seSpell, Skillnum)

            If increment Then
                SetPetVital(index, VitalType.HP, GetPetVital(index, VitalType.HP) + damage)

                If Skill(skillnum).Duration > 0 Then
                    AddHoT_Pet(index, skillnum)
                End If

            ElseIf Not increment Then
                If vital = VitalType.HP Then
                    SetPetVital(index, VitalType.HP, GetPetVital(index, VitalType.HP) - damage)
                ElseIf vital = VitalType.MP Then
                    SetPetVital(index, VitalType.MP, GetPetVital(index, VitalType.MP) - damage)
                End If
            End If
        End If

        If GetPetVital(index, VitalType.HP) > GetPetMaxVital(index, VitalType.HP) Then SetPetVital(index, VitalType.HP, GetPetMaxVital(index, VitalType.HP))

        If GetPetVital(index, VitalType.MP) > GetPetMaxVital(index, VitalType.MP) Then SetPetVital(index, VitalType.MP, GetPetMaxVital(index, VitalType.MP))

    End Sub

    Friend Sub AddHoT_Pet(index As Integer, skillnum As Integer)
        Dim i As Integer

        For i = 1 To MAX_COTS
            With TempPlayer(index).PetHoT(i)

                If .Skill = skillnum Then
                    .Timer = GetTimeMs()
                    .StartTime = GetTimeMs()
                    Exit Sub
                End If

                If .Used = False Then
                    .Skill = skillnum
                    .Timer = GetTimeMs()
                    .Used = True
                    .StartTime = GetTimeMs()
                    Exit Sub
                End If
            End With
        Next

    End Sub

    Friend Sub AddDoT_Pet(index As Integer, skillnum As Integer, caster As Integer, attackerType As Integer)
        Dim i As Integer

        If Not PetAlive(index) Then Exit Sub

        For i = 1 To MAX_COTS
            With TempPlayer(index).PetDoT(i)
                If .Skill = skillnum Then
                    .Timer = GetTimeMs()
                    .Caster = caster
                    .StartTime = GetTimeMs()
                    .AttackerType = attackerType
                    Exit Sub
                End If

                If .Used = False Then
                    .Skill = skillnum
                    .Timer = GetTimeMs()
                    .Caster = caster
                    .Used = True
                    .StartTime = GetTimeMs()
                    .AttackerType = attackerType
                    Exit Sub
                End If
            End With
        Next

    End Sub

    Friend Sub StunPet(index As Integer, skillnum As Integer)
        ' check if it's a stunning spell

        If PetAlive(index) Then
            If Skill(skillnum).StunDuration > 0 Then
                ' set the values on index
                TempPlayer(index).PetStunDuration = Skill(skillnum).StunDuration
                TempPlayer(index).PetStunTimer = GetTimeMs()
                ' tell him he's stunned
                PlayerMsg(index, "Your " & Trim$(GetPetName(index)) & " has been stunned.", ColorType.Yellow)
            End If
        End If

    End Sub

    Friend Sub HandleDoT_Pet(index As Integer, dotNum As Integer)

        With TempPlayer(index).PetDoT(dotNum)

            If .Used AndAlso .Skill > 0 Then
                ' time to tick?
                If GetTimeMs() > .Timer + (Skill(.Skill).Interval * 1000) Then
                    If .AttackerType = TargetType.Pet Then
                        If CanPetAttackPet(.Caster, index, .Skill) Then
                            PetAttackPet(.Caster, index, Skill(.Skill).Vital)
                            SendPetVital(index, VitalType.HP)
                            SendPetVital(index, VitalType.MP)
                        End If
                    ElseIf .AttackerType = TargetType.Player Then
                        If CanPlayerAttackPet(.Caster, index, .Skill) Then
                            PlayerAttackPet(.Caster, index, Skill(.Skill).Vital)
                            SendPetVital(index, VitalType.HP)
                            SendPetVital(index, VitalType.MP)
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

    Friend Sub HandleHoT_Pet(index As Integer, hotNum As Integer)

        With TempPlayer(index).PetHoT(hotNum)

            If .Used AndAlso .Skill > 0 Then
                ' time to tick?
                If GetTimeMs() > .Timer + (Skill(.Skill).Interval * 1000) Then
                    SendActionMsg(GetPlayerMap(index), "+" & Skill(.Skill).Vital, ColorType.BrightGreen, ActionMsgType.Scroll, Player(index).Character(TempPlayer(index).CurChar).Pet.X * 32, Player(index).Character(TempPlayer(index).CurChar).Pet.Y * 32,)
                    SetPetVital(index, VitalType.HP, GetPetVital(index, VitalType.HP) + Skill(.Skill).Vital)

                    If GetPetVital(index, VitalType.HP) > GetPetMaxVital(index, VitalType.HP) Then SetPetVital(index, VitalType.HP, GetPetMaxVital(index, VitalType.HP))

                    If GetPetVital(index, VitalType.MP) > GetPetMaxVital(index, VitalType.MP) Then SetPetVital(index, VitalType.MP, GetPetMaxVital(index, VitalType.MP))

                    SendPetVital(index, VitalType.HP)
                    SendPetVital(index, VitalType.MP)
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

    Friend Function CanPetDodge(index As Integer) As Boolean
        Dim rate As Integer, rndNum As Integer

        If Not PetAlive(index) Then Exit Function

        CanPetDodge = False

        rate = GetPetStat(index, StatType.Luck) / 4
        rndNum = Random(1, 100)

        If rndNum <= rate Then
            CanPetDodge = True
        End If

    End Function

    Friend Function CanPetParry(index As Integer) As Boolean
        Dim rate As Integer, rndNum As Integer

        If Not PetAlive(index) Then Exit Function

        CanPetParry = False

        rate = GetPetStat(index, StatType.Luck) / 6
        rndNum = Random(1, 100)

        If rndNum <= rate Then
            CanPetParry = True
        End If

    End Function

#End Region

#Region "Player > Pet"

    Function CanPlayerAttackPet(attacker As Integer, victim As Integer, Optional isSkill As Boolean = False) As Boolean

        If isSkill = False Then
            ' Check attack timer
            If GetPlayerEquipment(attacker, EquipmentType.Weapon) > 0 Then
                If GetTimeMs() < TempPlayer(attacker).AttackTimer + Item(GetPlayerEquipment(attacker, EquipmentType.Weapon)).Speed Then Exit Function
            Else
                If GetTimeMs() < TempPlayer(attacker).AttackTimer + 1000 Then Exit Function
            End If
        End If

        ' Check for subscript out of range
        If Not IsPlaying(victim) Then Exit Function

        If Not PetAlive(victim) Then Exit Function

        ' Make sure they are on the same map
        If Not GetPlayerMap(attacker) = GetPlayerMap(victim) Then Exit Function

        ' Make sure we dont attack the player if they are switching maps
        If TempPlayer(victim).GettingMap = 1 Then Exit Function

        If isSkill = False Then

            ' Check if at same coordinates
            Select Case GetPlayerDir(attacker)

                Case DirectionType.Up
                    If Not ((GetPetY(victim) + 1 = GetPlayerY(attacker)) AndAlso (GetPetX(victim) = GetPlayerX(attacker))) Then Exit Function

                Case DirectionType.Down
                    If Not ((GetPetY(victim) - 1 = GetPlayerY(attacker)) AndAlso (GetPetX(victim) = GetPlayerX(attacker))) Then Exit Function

                Case DirectionType.Left
                    If Not ((GetPetY(victim) = GetPlayerY(attacker)) AndAlso (GetPetX(victim) + 1 = GetPlayerX(attacker))) Then Exit Function

                Case DirectionType.Right
                    If Not ((GetPetY(victim) = GetPlayerY(attacker)) AndAlso (GetPetX(victim) - 1 = GetPlayerX(attacker))) Then Exit Function

                Case Else
                    Exit Function
            End Select
        End If

        ' Check if map is attackable
        If Not Map(GetPlayerMap(attacker)).Moral = MapMoralType.None Then
            If GetPlayerPK(victim) = 0 Then
                PlayerMsg(attacker, "This is a safe zone!", ColorType.Yellow)
                Exit Function
            End If
        End If

        ' Make sure they have more then 0 hp
        If GetPetVital(victim, VitalType.HP) <= 0 Then Exit Function

        ' Check to make sure that they dont have access
        If GetPlayerAccess(attacker) > AdminType.Monitor Then
            PlayerMsg(attacker, "Admins cannot attack other players.", ColorType.BrightRed)
            Exit Function
        End If

        ' Check to make sure the victim isn't an admin
        If GetPlayerAccess(victim) > AdminType.Monitor Then
            PlayerMsg(attacker, "You cannot attack " & GetPlayerName(victim) & "s " & Trim$(GetPetName(victim)) & "!", ColorType.BrightRed)
            Exit Function
        End If

        ' Don't attack a party member
        If TempPlayer(attacker).InParty > 0 AndAlso TempPlayer(victim).InParty > 0 Then
            If TempPlayer(attacker).InParty = TempPlayer(victim).InParty Then
                PlayerMsg(attacker, "You can't attack another party member!", ColorType.BrightRed)
                Exit Function
            End If
        End If

        If TempPlayer(attacker).InParty > 0 AndAlso TempPlayer(victim).InParty > 0 AndAlso TempPlayer(attacker).InParty = TempPlayer(victim).InParty Then
            If isSkill > 0 Then
                If Skill(isSkill).Type = SkillType.HealMp OrElse Skill(isSkill).Type = SkillType.HealHp Then
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

        If IsPlaying(attacker) = False OrElse IsPlaying(victim) = False OrElse damage < 0 OrElse Not PetAlive(victim) Then Exit Sub

        If GetPlayerEquipment(attacker, EquipmentType.Weapon) > 0 Then
            n = GetPlayerEquipment(attacker, EquipmentType.Weapon)
        End If

        ' set the regen timer
        TempPlayer(attacker).StopRegen = True
        TempPlayer(attacker).StopRegenTimer = GetTimeMs()

        If damage >= GetPetVital(victim, VitalType.HP) Then
            SendActionMsg(GetPlayerMap(victim), "-" & GetPetVital(victim, VitalType.HP), ColorType.BrightRed, 1, (GetPetX(victim) * 32), (GetPetY(victim) * 32))

            ' send the sound
            'If Spellnum > 0 Then SendMapSound Victim, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.x, Player(Victim).characters(TempPlayer(Victim).CurChar).Pet.y, SoundEntity.seSpell, Spellnum

            ' Calculate exp to give attacker
            exp = (GetPlayerExp(victim) \ 10)

            ' Make sure we dont get less then 0
            If exp < 0 Then exp = 0

            If exp = 0 Then
                PlayerMsg(victim, "You lost no exp.", ColorType.BrightGreen)
                PlayerMsg(attacker, "You received no exp.", ColorType.Yellow)
            Else
                SetPlayerExp(victim, GetPlayerExp(victim) - exp)
                SendExp(victim)
                PlayerMsg(victim, "You lost " & exp & " exp.", ColorType.BrightRed)

                ' check if we're in a party
                If TempPlayer(attacker).InParty > 0 Then
                    ' pass through party exp share function
                    Party_ShareExp(TempPlayer(attacker).InParty, exp, attacker, GetPlayerMap(attacker))
                Else
                    ' not in party, get exp for self
                    GivePlayerExp(attacker, exp)
                End If
            End If

            ' purge target info of anyone who targetted dead guy
            For i = 1 To GetPlayersOnline()
                If IsPlaying(i) AndAlso Socket.IsConnected(i) AndAlso GetPlayerMap(i) = GetPlayerMap(attacker) Then
                    If TempPlayer(i).Target = TargetType.Pet AndAlso TempPlayer(i).Target = victim Then
                        TempPlayer(i).Target = 0
                        TempPlayer(i).TargetType = TargetType.None
                        SendTarget(i, 0, TargetType.None)
                    End If
                End If
            Next

            PlayerMsg(victim, ("Your " & GetPetName(victim).Trim & " was killed by  " & GetPlayerName(attacker).Trim & "."), ColorType.BrightRed)
            ReCallPet(victim)
        Else
            ' Pet not dead, just do the damage
            SetPetVital(victim, VitalType.HP, GetPetVital(victim, VitalType.HP) - damage)
            SendPetVital(victim, VitalType.HP)

            'Set pet to begin attacking the other pet if it isn't dead or dosent have another target
            If TempPlayer(victim).PetTarget <= 0 AndAlso TempPlayer(victim).PetBehavior <> PetBehaviourGoto Then
                TempPlayer(victim).PetTarget = attacker
                TempPlayer(victim).PetTargetType = TargetType.Player
            End If

            ' send the sound
            'If Spellnum > 0 Then SendMapSound Victim, GetPetX(Victim), GetPety(Victim), SoundEntity.seSpell, Spellnum

            SendActionMsg(GetPlayerMap(victim), "-" & damage, ColorType.BrightRed, 1, (GetPetX(victim) * 32), (GetPetY(victim) * 32))
            SendBlood(GetPlayerMap(victim), GetPetX(victim), GetPetY(victim))

            ' set the regen timer
            TempPlayer(victim).PetstopRegen = True
            TempPlayer(victim).PetstopRegenTimer = GetTimeMs()

            'if a stunning spell, stun the player
            If skillnum > 0 Then
                If Skill(skillnum).StunDuration > 0 Then StunPet(victim, skillnum)

                ' DoT
                If Skill(skillnum).Duration > 0 Then
                    AddDoT_Pet(victim, skillnum, attacker, TargetType.Player)
                End If
            End If
        End If

        ' Reset attack timer
        TempPlayer(attacker).AttackTimer = GetTimeMs()

    End Sub

    Friend Sub TryPlayerAttackPet(attacker As Integer, victim As Integer)
        Dim blockAmount As Integer, mapNum As Integer
        If Not PetAlive(victim) Then Exit Sub

        ' Can we attack the npc?
        If CanPlayerAttackPet(attacker, victim) Then

            mapNum = GetPlayerMap(attacker)

            TempPlayer(attacker).Target = victim
            TempPlayer(attacker).TargetType = TargetType.Pet

            ' check if NPC can avoid the attack
            If CanPetDodge(victim) Then
                SendActionMsg(mapNum, "Dodge!", ColorType.Pink, 1, (GetPlayerX(victim) * 32), (GetPlayerY(victim) * 32))
                Exit Sub
            End If

            If CanPetParry(victim) Then
                SendActionMsg(mapNum, "Parry!", ColorType.Pink, 1, (GetPlayerX(victim) * 32), (GetPlayerY(victim) * 32))
                Exit Sub
            End If

            ' Get the damage we can do
            Dim damage As Integer = GetPlayerDamage(attacker)

            ' if the npc blocks, take away the block amount
            blockAmount = 0
            damage -= blockAmount

            ' take away armour
            damage -= Random(1, (GetPlayerStat(victim, StatType.Luck) * 2))

            ' randomise for up to 10% lower than max hit
            damage = Random(1, damage)

            ' * 1.5 if can crit
            If CanPlayerCriticalHit(attacker) Then
                damage *= 1.5
                SendActionMsg(mapNum, "Critical!", ColorType.BrightCyan, 1, (GetPlayerX(attacker) * 32), (GetPlayerY(attacker) * 32))
            End If

            If damage > 0 Then
                PlayerAttackPet(attacker, victim, damage)
            Else
                PlayerMsg(attacker, "Your attack does nothing.", ColorType.BrightRed)
            End If
        End If

    End Sub

#End Region

#Region "Data Functions"

    Friend Function PetAlive(index As Integer) As Boolean
        PetAlive = False

        If Player(index).Character(TempPlayer(index).CurChar).Pet.Alive = 1 Then
            PetAlive = True
        End If

    End Function

    Friend Function GetPetName(index As Integer) As String
        GetPetName = ""

        If PetAlive(index) Then
            GetPetName = Pet(Player(index).Character(TempPlayer(index).CurChar).Pet.Num).Name.Trim
        End If

    End Function

    Friend Function GetPetNum(index As Integer) As Integer
        GetPetNum = Player(index).Character(TempPlayer(index).CurChar).Pet.Num

    End Function

    Friend Function GetPetRange(index As Integer) As Integer
        GetPetRange = 0

        If PetAlive(index) Then
            GetPetRange = Pet(Player(index).Character(TempPlayer(index).CurChar).Pet.Num).Range
        End If

    End Function

    Friend Function GetPetLevel(index As Integer) As Integer
        GetPetLevel = 0

        If PetAlive(index) Then
            GetPetLevel = Player(index).Character(TempPlayer(index).CurChar).Pet.Level
        End If

    End Function

    Friend Sub SetPetLevel(index As Integer, newlvl As Integer)
        If PetAlive(index) Then
            Player(index).Character(TempPlayer(index).CurChar).Pet.Level = newlvl
        End If
    End Sub

    Friend Function GetPetX(index As Integer) As Integer
        GetPetX = 0

        If PetAlive(index) Then
            GetPetX = Player(index).Character(TempPlayer(index).CurChar).Pet.X
        End If

    End Function

    Friend Sub SetPetX(index As Integer, x As Integer)
        If PetAlive(index) Then
            Player(index).Character(TempPlayer(index).CurChar).Pet.X = x
        End If
    End Sub

    Friend Function GetPetY(index As Integer) As Integer
        GetPetY = 0

        If PetAlive(index) Then
            GetPetY = Player(index).Character(TempPlayer(index).CurChar).Pet.Y
        End If

    End Function

    Friend Sub SetPetY(index As Integer, y As Integer)
        If PetAlive(index) Then
            Player(index).Character(TempPlayer(index).CurChar).Pet.Y = y
        End If
    End Sub

    Friend Function GetPetDir(index As Integer) As Integer
        GetPetDir = 0

        If PetAlive(index) Then
            GetPetDir = Player(index).Character(TempPlayer(index).CurChar).Pet.Dir
        End If

    End Function

    Friend Function GetPetBehaviour(index As Integer) As Integer
        GetPetBehaviour = 0

        If PetAlive(index) Then
            GetPetBehaviour = Player(index).Character(TempPlayer(index).CurChar).Pet.AttackBehaviour
        End If

    End Function

    Friend Sub SetPetBehaviour(index As Integer, behaviour As Byte)
        If PetAlive(index) Then
            Player(index).Character(TempPlayer(index).CurChar).Pet.AttackBehaviour = behaviour
        End If
    End Sub

    Friend Function GetPetStat(index As Integer, stat As StatType) As Integer
        GetPetStat = 0

        If PetAlive(index) Then
            GetPetStat = Player(index).Character(TempPlayer(index).CurChar).Pet.Stat(stat)
        End If

    End Function

    Friend Sub SetPetStat(index As Integer, stat As StatType, amount As Integer)

        If PetAlive(index) Then
            Player(index).Character(TempPlayer(index).CurChar).Pet.Stat(stat) = amount
        End If

    End Sub

    Friend Function GetPetPoints(index As Integer) As Integer
        GetPetPoints = 0

        If PetAlive(index) Then
            GetPetPoints = Player(index).Character(TempPlayer(index).CurChar).Pet.Points
        End If

    End Function

    Friend Sub SetPetPoints(index As Integer, amount As Integer)

        If PetAlive(index) Then
            Player(index).Character(TempPlayer(index).CurChar).Pet.Points = amount
        End If

    End Sub

    Friend Function GetPetExp(index As Integer) As Integer
        GetPetExp = 0

        If PetAlive(index) Then
            GetPetExp = Player(index).Character(TempPlayer(index).CurChar).Pet.Exp
        End If

    End Function

    Friend Sub SetPetExp(index As Integer, amount As Integer)
        If PetAlive(index) Then
            Player(index).Character(TempPlayer(index).CurChar).Pet.Exp = amount
        End If
    End Sub

    Function GetPetVital(index As Integer, vital As VitalType) As Integer

        If index > MAX_PLAYERS Then Exit Function

        Select Case vital
            Case VitalType.HP
                GetPetVital = Player(index).Character(TempPlayer(index).CurChar).Pet.Health

            Case VitalType.MP
                GetPetVital = Player(index).Character(TempPlayer(index).CurChar).Pet.Mana
        End Select

    End Function

    Sub SetPetVital(index As Integer, vital As VitalType, amount As Integer)

        If index > MAX_PLAYERS Then Exit Sub

        Select Case vital
            Case VitalType.HP
                Player(index).Character(TempPlayer(index).CurChar).Pet.Health = amount

            Case VitalType.MP
                Player(index).Character(TempPlayer(index).CurChar).Pet.Mana = amount
        End Select

    End Sub

    Function GetPetMaxVital(index As Integer, vital As VitalType) As Integer

        If index > MAX_PLAYERS Then Exit Function

        Select Case vital
            Case VitalType.HP
                GetPetMaxVital = ((Player(index).Character(TempPlayer(index).CurChar).Pet.Level * 4) + (Player(index).Character(TempPlayer(index).CurChar).Pet.Stat(StatType.Endurance) * 10)) + 150

            Case VitalType.MP
                GetPetMaxVital = ((Player(index).Character(TempPlayer(index).CurChar).Pet.Level * 4) + (Player(index).Character(TempPlayer(index).CurChar).Pet.Stat(StatType.Spirit) / 2)) * 5 + 50
        End Select

    End Function

    Function GetPetNextLevel(index As Integer) As Integer

        If PetAlive(index) Then
            If Player(index).Character(TempPlayer(index).CurChar).Pet.Level = Pet(Player(index).Character(TempPlayer(index).CurChar).Pet.Num).MaxLevel Then GetPetNextLevel = 0 : Exit Function
            GetPetNextLevel = (50 / 3) * ((Player(index).Character(TempPlayer(index).CurChar).Pet.Level + 1) ^ 3 - (6 * (Player(index).Character(TempPlayer(index).CurChar).Pet.Level + 1) ^ 2) + 17 * (Player(index).Character(TempPlayer(index).CurChar).Pet.Level + 1) - 12)
        End If

    End Function

#End Region

End Module