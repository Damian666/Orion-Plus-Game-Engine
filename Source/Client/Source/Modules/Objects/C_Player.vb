Imports ASFW

Module C_Player

    Function IsPlaying(index As Integer) As Boolean

        ' if the player doesn't exist, the name will equal 0
        If Len(GetPlayerName(index)) > 0 Then
            IsPlaying = True
        End If

    End Function

    Function GetPlayerName(index As Integer) As String
        GetPlayerName = ""
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerName = Trim$(Player(index).Name)
    End Function

    Sub CheckAttack()
        Dim attackspeed As Integer, x As Integer, y As Integer
        Dim buffer As New ByteStream(4)

        If VbKeyControl Then
            If InEvent = True Then Exit Sub
            If SkillBuffer > 0 Then Exit Sub ' currently casting a skill, can't attack
            If StunDuration > 0 Then Exit Sub ' stunned, can't attack

            ' speed from weapon
            If GetPlayerEquipment(Myindex, EquipmentType.Weapon) > 0 Then
                attackspeed = Item(GetPlayerEquipment(Myindex, EquipmentType.Weapon)).Speed * 1000
            Else
                attackspeed = 1000
            End If

            If Player(Myindex).AttackTimer + attackspeed < GetTickCount() Then
                If Player(Myindex).Attacking = 0 Then

                    With Player(Myindex)
                        .Attacking = 1
                        .AttackTimer = GetTickCount()
                    End With

                    SendAttack()
                End If
            End If

            Select Case Player(Myindex).Dir
                Case DirectionType.Up
                    x = GetPlayerX(Myindex)
                    y = GetPlayerY(Myindex) - 1
                Case DirectionType.Down
                    x = GetPlayerX(Myindex)
                    y = GetPlayerY(Myindex) + 1
                Case DirectionType.Left
                    x = GetPlayerX(Myindex) - 1
                    y = GetPlayerY(Myindex)
                Case DirectionType.Right
                    x = GetPlayerX(Myindex) + 1
                    y = GetPlayerY(Myindex)
            End Select

            If GetTickCount() > Player(Myindex).EventTimer Then
                For i = 1 To Map.CurrentEvents
                    If Map.MapEvents(i).Visible = 1 Then
                        If Map.MapEvents(i).X = x AndAlso Map.MapEvents(i).Y = y Then
                            buffer = New ByteStream(4)
                            buffer.WriteInt32(ClientPackets.CEvent)
                            buffer.WriteInt32(i)
                            Socket.SendData(buffer.Data, buffer.Head)
                            buffer.Dispose()
                            Player(Myindex).EventTimer = GetTickCount() + 200
                        End If
                    End If
                Next
            End If
        End If

    End Sub

    Sub CheckMovement()

        If IsTryingToMove() AndAlso CanMove() Then
            ' Check if player has the shift key down for running
            If VbKeyShift Then
                Player(Myindex).Moving = MovementType.Running
            Else
                Player(Myindex).Moving = MovementType.Walking
            End If

            If Map.Tile(GetPlayerX(Myindex), GetPlayerY(Myindex)).Type = TileType.Door Then
                With TempTile(GetPlayerX(Myindex), GetPlayerY(Myindex))
                    .DoorFrame = 1
                    .DoorAnimate = 1 ' 0 = nothing| 1 = opening | 2 = closing
                    .DoorTimer = GetTickCount()
                End With
            End If

            Select Case GetPlayerDir(Myindex)
                Case DirectionType.Up
                    SendPlayerMove()
                    Player(Myindex).YOffset = PicY
                    SetPlayerY(Myindex, GetPlayerY(Myindex) - 1)
                Case DirectionType.Down
                    SendPlayerMove()
                    Player(Myindex).YOffset = PicY * -1
                    SetPlayerY(Myindex, GetPlayerY(Myindex) + 1)
                Case DirectionType.Left
                    SendPlayerMove()
                    Player(Myindex).XOffset = PicX
                    SetPlayerX(Myindex, GetPlayerX(Myindex) - 1)
                Case DirectionType.Right
                    SendPlayerMove()
                    Player(Myindex).XOffset = PicX * -1
                    SetPlayerX(Myindex, GetPlayerX(Myindex) + 1)
            End Select

            If Player(Myindex).XOffset = 0 AndAlso Player(Myindex).YOffset = 0 Then
                If Map.Tile(GetPlayerX(Myindex), GetPlayerY(Myindex)).Type = TileType.Warp Then
                    GettingMap = True
                End If
            End If

        End If
    End Sub

    Function IsTryingToMove() As Boolean

        If DirUp OrElse DirDown OrElse DirLeft OrElse DirRight Then
            IsTryingToMove = True
        End If

    End Function

    Function CanMove() As Boolean
        Dim d As Integer
        CanMove = True

        If HoldPlayer = True Then
            CanMove = False
            Exit Function
        End If

        If GettingMap = True Then
            CanMove = False
            Exit Function
        End If

        ' Make sure they aren't trying to move when they are already moving
        If Player(Myindex).Moving <> 0 Then
            CanMove = False
            Exit Function
        End If

        ' Make sure they haven't just casted a skill
        If SkillBuffer > 0 Then
            CanMove = False
            Exit Function
        End If

        ' make sure they're not stunned
        If StunDuration > 0 Then
            CanMove = False
            Exit Function
        End If

        If InEvent Then
            CanMove = False
            Exit Function
        End If

        ' craft
        If InCraft Then
            CanMove = False
            Exit Function
        End If

        ' make sure they're not in a shop
        If InShop > 0 Then
            CanMove = False
            Exit Function
        End If

        If InTrade Then
            CanMove = False
            Exit Function
        End If

        ' not in bank
        If InBank > 0 Then
            CanMove = False
            Exit Function
        End If

        d = GetPlayerDir(Myindex)

        If DirUp Then
            SetPlayerDir(Myindex, DirectionType.Up)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerY(Myindex) > 0 Then
                If CheckDirection(DirectionType.Up) Then
                    CanMove = False

                    ' Set the new direction if they weren't facing that direction
                    If d <> DirectionType.Up Then
                        SendPlayerDir()
                    End If

                    Exit Function
                End If
            Else

                ' Check if they can warp to a new map
                If Map.Up > 0 Then
                    SendPlayerRequestNewMap()
                    GettingMap = True
                    CanMoveNow = False
                End If

                CanMove = False
                Exit Function
            End If
        End If

        If DirDown Then
            SetPlayerDir(Myindex, DirectionType.Down)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerY(Myindex) < Map.MaxY Then
                If CheckDirection(DirectionType.Down) Then
                    CanMove = False

                    ' Set the new direction if they weren't facing that direction
                    If d <> DirectionType.Down Then
                        SendPlayerDir()
                    End If

                    Exit Function
                End If
            Else

                ' Check if they can warp to a new map
                If Map.Down > 0 Then
                    SendPlayerRequestNewMap()
                    GettingMap = True
                    CanMoveNow = False
                End If

                CanMove = False
                Exit Function
            End If
        End If

        If DirLeft Then
            SetPlayerDir(Myindex, DirectionType.Left)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerX(Myindex) > 0 Then
                If CheckDirection(DirectionType.Left) Then
                    CanMove = False

                    ' Set the new direction if they weren't facing that direction
                    If d <> DirectionType.Left Then
                        SendPlayerDir()
                    End If

                    Exit Function
                End If
            Else

                ' Check if they can warp to a new map
                If Map.Left > 0 Then
                    SendPlayerRequestNewMap()
                    GettingMap = True
                    CanMoveNow = False
                End If

                CanMove = False
                Exit Function
            End If
        End If

        If DirRight Then
            SetPlayerDir(Myindex, DirectionType.Right)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerX(Myindex) < Map.MaxX Then
                If CheckDirection(DirectionType.Right) Then
                    CanMove = False

                    ' Set the new direction if they weren't facing that direction
                    If d <> DirectionType.Right Then
                        SendPlayerDir()
                    End If

                    Exit Function
                End If
            Else

                ' Check if they can warp to a new map
                If Map.Right > 0 Then
                    SendPlayerRequestNewMap()
                    GettingMap = True
                    CanMoveNow = False
                End If

                CanMove = False
                Exit Function
            End If
        End If

    End Function

    Function CheckDirection(direction As Byte) As Boolean
        Dim x As Integer, y As Integer
        Dim i As Integer, z As Integer

        CheckDirection = False

        ' check directional blocking
        If IsDirBlocked(Map.Tile(GetPlayerX(Myindex), GetPlayerY(Myindex)).DirBlock, direction + 1) Then
            CheckDirection = True
            Exit Function
        End If

        Select Case direction
            Case Enums.DirectionType.Up
                x = GetPlayerX(Myindex)
                y = GetPlayerY(Myindex) - 1
            Case Enums.DirectionType.Down
                x = GetPlayerX(Myindex)
                y = GetPlayerY(Myindex) + 1
            Case Enums.DirectionType.Left
                x = GetPlayerX(Myindex) - 1
                y = GetPlayerY(Myindex)
            Case Enums.DirectionType.Right
                x = GetPlayerX(Myindex) + 1
                y = GetPlayerY(Myindex)
        End Select

        ' Check to see if the map tile is blocked or not
        If Map.Tile(x, y).Type = TileType.Blocked Then
            CheckDirection = True
            Exit Function
        End If

        ' Check to see if the map tile is tree or not
        If Map.Tile(x, y).Type = TileType.Resource Then
            CheckDirection = True
            Exit Function
        End If

        ' Check to see if the key door is open or not
        If Map.Tile(x, y).Type = TileType.Key Then
            ' This actually checks if its open or not
            If TempTile(x, y).DoorOpen = False Then
                CheckDirection = True
                Exit Function
            End If
        End If

        If FurnitureHouse > 0 AndAlso FurnitureHouse = Player(Myindex).InHouse Then
            If FurnitureCount > 0 Then
                For i = 1 To FurnitureCount
                    If Item(Furniture(i).ItemNum).Data3 = 0 Then
                        If x >= Furniture(i).X AndAlso x <= Furniture(i).X + Item(Furniture(i).ItemNum).FurnitureWidth - 1 Then
                            If y <= Furniture(i).Y AndAlso y >= Furniture(i).Y - Item(Furniture(i).ItemNum).FurnitureHeight Then
                                z = Item(Furniture(i).ItemNum).FurnitureBlocks(x - Furniture(i).X, ((Furniture(i).Y - y) * -1) + Item(Furniture(i).ItemNum).FurnitureHeight)
                                If z = 1 Then CheckDirection = True : Exit Function
                            End If
                        End If
                    End If
                Next
            End If
        End If

        ' Check to see if a player is already on that tile
        For i = 1 To MAX_PLAYERS
            If IsPlaying(i) AndAlso GetPlayerMap(i) = GetPlayerMap(Myindex) Then
                If Player(i).InHouse = Player(Myindex).InHouse Then
                    If GetPlayerX(i) = x Then
                        If GetPlayerY(i) = y Then
                            CheckDirection = True
                            Exit Function
                        ElseIf Player(i).Pet.X = x AndAlso Player(i).Pet.Alive = True Then
                            If Player(i).Pet.Y = y Then
                                CheckDirection = True
                                Exit Function
                            End If
                        End If
                    ElseIf Player(i).Pet.X = x AndAlso Player(i).Pet.Y = y AndAlso Player(i).Pet.Alive = True Then
                        CheckDirection = True
                        Exit Function
                    End If
                End If
            End If
        Next

        ' Check to see if a npc is already on that tile
        For i = 1 To MAX_MAP_NPCS
            If MapNpc(i).Num > 0 AndAlso MapNpc(i).X = x AndAlso MapNpc(i).Y = y Then
                CheckDirection = True
                Exit Function
            End If
        Next

        For i = 1 To Map.CurrentEvents
            If Map.MapEvents(i).Visible = 1 Then
                If Map.MapEvents(i).X = x AndAlso Map.MapEvents(i).Y = y Then
                    If Map.MapEvents(i).WalkThrough = 0 Then
                        CheckDirection = True
                        Exit Function
                    End If
                End If
            End If
        Next

    End Function

    Sub ProcessMovement(index As Integer)
        Dim movementSpeed As Integer

        ' Check if player is walking, and if so process moving them over
        Select Case Player(index).Moving
            Case MovementType.Walking : movementSpeed = ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
            Case MovementType.Running : movementSpeed = ((ElapsedTime / 1000) * (RunSpeed * SizeX))
            Case Else : Exit Sub
        End Select

        Select Case GetPlayerDir(index)
            Case DirectionType.Up
                Player(index).YOffset = Player(index).YOffset - movementSpeed
                If Player(index).YOffset < 0 Then Player(index).YOffset = 0
            Case DirectionType.Down
                Player(index).YOffset = Player(index).YOffset + movementSpeed
                If Player(index).YOffset > 0 Then Player(index).YOffset = 0
            Case DirectionType.Left
                Player(index).XOffset = Player(index).XOffset - movementSpeed
                If Player(index).XOffset < 0 Then Player(index).XOffset = 0
            Case DirectionType.Right
                Player(index).XOffset = Player(index).XOffset + movementSpeed
                If Player(index).XOffset > 0 Then Player(index).XOffset = 0
        End Select

        ' Check if completed walking over to the next tile
        If Player(index).Moving > 0 Then
            If GetPlayerDir(index) = DirectionType.Right OrElse GetPlayerDir(index) = DirectionType.Down Then
                If (Player(index).XOffset >= 0) AndAlso (Player(index).YOffset >= 0) Then
                    Player(index).Moving = 0
                    If Player(index).Steps = 1 Then
                        Player(index).Steps = 3
                    Else
                        Player(index).Steps = 1
                    End If
                End If
            Else
                If (Player(index).XOffset <= 0) AndAlso (Player(index).YOffset <= 0) Then
                    Player(index).Moving = 0
                    If Player(index).Steps = 1 Then
                        Player(index).Steps = 3
                    Else
                        Player(index).Steps = 1
                    End If
                End If
            End If
        End If

    End Sub

    Function GetPlayerDir(index As Integer) As Integer

        If index > MAX_PLAYERS Then Exit Function
        GetPlayerDir = Player(index).Dir
    End Function

    Function GetPlayerGatherSkillLvl(index As Integer, skillSlot As Integer) As Integer

        GetPlayerGatherSkillLvl = 0

        If index > MAX_PLAYERS Then Exit Function

        GetPlayerGatherSkillLvl = Player(index).GatherSkills(skillSlot).SkillLevel
    End Function

    Function GetPlayerGatherSkillExp(index As Integer, skillSlot As Integer) As Integer

        GetPlayerGatherSkillExp = 0

        If index > MAX_PLAYERS Then Exit Function

        GetPlayerGatherSkillExp = Player(index).GatherSkills(skillSlot).SkillCurExp
    End Function

    Function GetPlayerGatherSkillMaxExp(index As Integer, skillSlot As Integer) As Integer

        GetPlayerGatherSkillMaxExp = 0

        If index > MAX_PLAYERS Then Exit Function

        GetPlayerGatherSkillMaxExp = Player(index).GatherSkills(skillSlot).SkillNextLvlExp
    End Function

    Friend Sub PlayerCastSkill(skillslot As Integer)
        Dim buffer As New ByteStream(4)

        ' Check for subscript out of range
        If skillslot < 1 OrElse skillslot > MAX_PLAYER_SKILLS Then Exit Sub

        If SkillCd(skillslot) > 0 Then
            AddText("Skill has not cooled down yet!", QColorType.AlertColor)
            Exit Sub
        End If

        ' Check if player has enough MP
        If GetPlayerVital(Myindex, VitalType.MP) < Skill(PlayerSkills(skillslot)).MpCost Then
            AddText("Not enough MP to cast " & Trim$(Skill(PlayerSkills(skillslot)).Name) & ".", QColorType.AlertColor)
            Exit Sub
        End If

        If PlayerSkills(skillslot) > 0 Then
            If GetTickCount() > Player(Myindex).AttackTimer + 1000 Then
                If Player(Myindex).Moving = 0 Then
                    buffer.WriteInt32(ClientPackets.CCast)
                    buffer.WriteInt32(skillslot)

                    Socket.SendData(buffer.Data, buffer.Head)
                    buffer.Dispose()

                    SkillBuffer = skillslot
                    SkillBufferTimer = GetTickCount()
                Else
                    AddText("Cannot cast while walking!", QColorType.AlertColor)
                End If
            End If
        Else
            AddText("No skill here.", QColorType.AlertColor)
        End If

    End Sub

    Sub SetPlayerMap(index As Integer, mapNum As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Map = mapNum
    End Sub

    Function GetPlayerInvItemNum(index As Integer, invslot As Integer) As Integer
        GetPlayerInvItemNum = 0
        If index > MAX_PLAYERS Then Exit Function
        If invslot = 0 Then Exit Function
        GetPlayerInvItemNum = PlayerInv(invslot).Num
    End Function

    Sub SetPlayerName(index As Integer, name As String)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Name = name
    End Sub

    Sub SetPlayerClass(index As Integer, classnum As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Classes = classnum
    End Sub

    Sub SetPlayerPoints(index As Integer, points As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Points = points
    End Sub

    Sub SetPlayerStat(index As Integer, stat As StatType, value As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        If value <= 0 Then value = 1
        If value > Byte.MaxValue Then value = Byte.MaxValue
        Player(index).Stat(stat) = value
    End Sub

    Sub SetPlayerInvItemNum(index As Integer, invslot As Integer, itemnum As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        PlayerInv(invslot).Num = itemnum
    End Sub

    Function GetPlayerInvItemValue(index As Integer, invslot As Integer) As Integer
        GetPlayerInvItemValue = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerInvItemValue = PlayerInv(invslot).Value
    End Function

    Sub SetPlayerInvItemValue(index As Integer, invslot As Integer, itemValue As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        PlayerInv(invslot).Value = itemValue
    End Sub

    Function GetPlayerPoints(index As Integer) As Integer
        GetPlayerPoints = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerPoints = Player(index).Points
    End Function

    Sub SetPlayerAccess(index As Integer, access As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Access = access
    End Sub

    Sub SetPlayerPk(index As Integer, pk As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Pk = pk
    End Sub

    Sub SetPlayerVital(index As Integer, vital As VitalType, value As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Vital(vital) = value

        If GetPlayerVital(index, vital) > GetPlayerMaxVital(index, vital) Then
            Player(index).Vital(vital) = GetPlayerMaxVital(index, vital)
        End If
    End Sub

    Function GetPlayerMaxVital(index As Integer, vital As VitalType) As Integer
        GetPlayerMaxVital = 0
        If index > MAX_PLAYERS Then Exit Function

        Select Case vital
            Case VitalType.HP
                GetPlayerMaxVital = Player(index).MaxHp
            Case VitalType.MP
                GetPlayerMaxVital = Player(index).MaxMp
            Case VitalType.SP
                GetPlayerMaxVital = Player(index).MaxSp
        End Select

    End Function

    Sub SetPlayerX(index As Integer, x As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).X = x
    End Sub

    Sub SetPlayerY(index As Integer, y As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Y = y
    End Sub

    Sub SetPlayerSprite(index As Integer, sprite As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Sprite = sprite
    End Sub

    Sub SetPlayerExp(index As Integer, exp As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Exp = exp
    End Sub

    Sub SetPlayerLevel(index As Integer, level As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Level = level
    End Sub

    Sub SetPlayerDir(index As Integer, dir As Integer)
        If index > MAX_PLAYERS Then Exit Sub
        Player(index).Dir = dir
    End Sub

    Function GetPlayerVital(index As Integer, vital As VitalType) As Integer
        GetPlayerVital = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerVital = Player(index).Vital(vital)
    End Function

    Function GetPlayerSprite(index As Integer) As Integer
        GetPlayerSprite = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerSprite = Player(index).Sprite
    End Function

    Function GetPlayerClass(index As Integer) As Integer
        GetPlayerClass = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerClass = Player(index).Classes
    End Function

    Function GetPlayerMap(index As Integer) As Integer
        GetPlayerMap = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerMap = Player(index).Map
    End Function

    Function GetPlayerLevel(index As Integer) As Integer
        GetPlayerLevel = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerLevel = Player(index).Level
    End Function

    Function GetPlayerEquipment(index As Integer, equipmentSlot As EquipmentType) As Byte
        GetPlayerEquipment = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerEquipment = Player(index).Equipment(equipmentSlot)
    End Function

    Function GetPlayerStat(index As Integer, stat As StatType) As Integer
        GetPlayerStat = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerStat = Player(index).Stat(stat)
    End Function

    Function GetPlayerExp(index As Integer) As Integer
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerExp = Player(index).Exp
    End Function

    Function GetPlayerX(index As Integer) As Integer
        GetPlayerX = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerX = Player(index).X
    End Function

    Function GetPlayerY(index As Integer) As Integer
        GetPlayerY = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerY = Player(index).Y
    End Function

    Function GetPlayerAccess(index As Integer) As Integer
        GetPlayerAccess = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerAccess = Player(index).Access
    End Function

    Function GetPlayerPk(index As Integer) As Integer
        GetPlayerPk = 0
        If index > MAX_PLAYERS Then Exit Function
        GetPlayerPk = Player(index).Pk
    End Function

    Sub SetPlayerEquipment(index As Integer, invNum As Integer, equipmentSlot As EquipmentType)
        If index < 1 OrElse index > MAX_PLAYERS Then Exit Sub
        Player(index).Equipment(equipmentSlot) = invNum
    End Sub

    Sub ClearPlayer(index As Integer)
        Player(index).Name = ""
        Player(index).Access = 0
        Player(index).Attacking = 0
        Player(index).AttackTimer = 0
        Player(index).Classes = 0
        Player(index).Dir = 0

        ReDim Player(index).Equipment(EquipmentType.Count - 1)
        For y = 1 To EquipmentType.Count - 1
            Player(index).Equipment(y) = 0
        Next

        Player(index).Exp = 0
        Player(index).Level = 0
        Player(index).Map = 0
        Player(index).MapGetTimer = 0
        Player(index).MaxHp = 0
        Player(index).MaxMp = 0
        Player(index).MaxSp = 0
        Player(index).Moving = 0
        Player(index).Pk = 0
        Player(index).Points = 0
        Player(index).Sprite = 0

        ReDim Player(index).Stat(StatType.Count - 1)
        For x = 1 To StatType.Count - 1
            Player(index).Stat(x) = 0
        Next

        Player(index).Steps = 0

        ReDim Player(index).Vital(VitalType.Count - 1)
        For i = 1 To VitalType.Count - 1
            Player(index).Vital(i) = 0
        Next

        Player(index).X = 0
        Player(index).XOffset = 0
        Player(index).Y = 0
        Player(index).YOffset = 0

        ReDim Player(index).RandEquip(EquipmentType.Count - 1)
        For y = 1 To EquipmentType.Count - 1
            ReDim Player(index).RandEquip(y).Stat(StatType.Count - 1)
            For x = 1 To StatType.Count - 1
                Player(index).RandEquip(y).Stat(x) = 0
            Next
        Next

        ReDim Player(index).RandInv(MAX_INV)
        For y = 1 To MAX_INV
            ReDim Player(index).RandInv(y).Stat(StatType.Count - 1)
            For x = 1 To StatType.Count - 1
                Player(index).RandInv(y).Stat(x) = 0
            Next
        Next

        ReDim Player(index).PlayerQuest(MaxQuests)

        ReDim Player(index).Hotbar(MaxHotbar)

        ReDim Player(index).GatherSkills(ResourceSkills.Count - 1)

        ReDim Player(index).RecipeLearned(MAX_RECIPE)

        'pets
        Player(index).Pet.Num = 0
        Player(index).Pet.Health = 0
        Player(index).Pet.Mana = 0
        Player(index).Pet.Level = 0

        ReDim Player(index).Pet.Stat(StatType.Count - 1)
        For i = 1 To StatType.Count - 1
            Player(index).Pet.Stat(i) = 0
        Next

        ReDim Player(index).Pet.Skill(4)
        For i = 1 To 4
            Player(index).Pet.Skill(i) = 0
        Next

        Player(index).Pet.X = 0
        Player(index).Pet.Y = 0
        Player(index).Pet.Dir = 0
        Player(index).Pet.MaxHp = 0
        Player(index).Pet.MaxMp = 0
        Player(index).Pet.Alive = 0
        Player(index).Pet.AttackBehaviour = 0
        Player(index).Pet.Exp = 0
        Player(index).Pet.Tnl = 0
    End Sub

End Module