Imports ASFW

Module C_Player
    Function IsPlaying(index as integer) As Boolean

        ' if the player doesn't exist, the name will equal 0
        If Len(GetPlayerName(Index)) > 0 Then
            IsPlaying = True
        End If

    End Function

    Function GetPlayerName(index as integer) As String
        GetPlayerName = ""
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerName = Trim$(Player(Index).Name)
    End Function

    Sub CheckAttack()
        Dim attackspeed As Integer, x As Integer, y As Integer
        dim buffer as New ByteStream(4)

        If VbKeyControl Then
            If InEvent = True Then Exit Sub
            If SkillBuffer > 0 Then Exit Sub ' currently casting a skill, can't attack
            If StunDuration > 0 Then Exit Sub ' stunned, can't attack

            ' speed from weapon
            If GetPlayerEquipment(MyIndex, EquipmentType.Weapon) > 0 Then
                attackspeed = Item(GetPlayerEquipment(MyIndex, EquipmentType.Weapon)).Speed * 1000
            Else
                attackspeed = 1000
            End If

            If Player(MyIndex).AttackTimer + attackspeed < GetTickCount() Then
                If Player(MyIndex).Attacking = 0 Then

                    With Player(MyIndex)
                        .Attacking = 1
                        .AttackTimer = GetTickCount()
                    End With

                    SendAttack()
                End If
            End If

            Select Case Player(MyIndex).Dir
                Case DirectionType.Up
                    X = GetPlayerX(MyIndex)
                    Y = GetPlayerY(MyIndex) - 1
                Case DirectionType.Down
                    X = GetPlayerX(MyIndex)
                    Y = GetPlayerY(MyIndex) + 1
                Case DirectionType.Left
                    X = GetPlayerX(MyIndex) - 1
                    Y = GetPlayerY(MyIndex)
                Case DirectionType.Right
                    X = GetPlayerX(MyIndex) + 1
                    Y = GetPlayerY(MyIndex)
            End Select

            If GetTickCount() > Player(MyIndex).EventTimer Then
                For i = 1 To Map.CurrentEvents
                    If Map.MapEvents(i).Visible = 1 Then
                        If Map.MapEvents(i).X = X AndAlso Map.MapEvents(i).Y = Y Then
                            Buffer = New ByteStream(4)
                            Buffer.WriteInt32(ClientPackets.CEvent)
                            Buffer.WriteInt32(i)
                            Socket.SendData(Buffer.Data, Buffer.Head)
                            Buffer.Dispose()
                            Player(MyIndex).EventTimer = GetTickCount() + 200
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
                Player(MyIndex).Moving = MovementType.Running
            Else
                Player(MyIndex).Moving = MovementType.Walking
            End If

            If Map.Tile(GetPlayerX(MyIndex), GetPlayerY(MyIndex)).Type = TileType.Door Then
                With TempTile(GetPlayerX(MyIndex), GetPlayerY(MyIndex))
                    .DoorFrame = 1
                    .DoorAnimate = 1 ' 0 = nothing| 1 = opening | 2 = closing
                    .DoorTimer = GetTickCount()
                End With
            End If

            Select Case GetPlayerDir(MyIndex)
                Case DirectionType.Up
                    SendPlayerMove()
                    Player(MyIndex).YOffset = PicY
                    SetPlayerY(MyIndex, GetPlayerY(MyIndex) - 1)
                Case DirectionType.Down
                    SendPlayerMove()
                    Player(MyIndex).YOffset = PicY * -1
                    SetPlayerY(MyIndex, GetPlayerY(MyIndex) + 1)
                Case DirectionType.Left
                    SendPlayerMove()
                    Player(MyIndex).XOffset = PicX
                    SetPlayerX(MyIndex, GetPlayerX(MyIndex) - 1)
                Case DirectionType.Right
                    SendPlayerMove()
                    Player(MyIndex).XOffset = PicX * -1
                    SetPlayerX(MyIndex, GetPlayerX(MyIndex) + 1)
            End Select

            If Player(MyIndex).XOffset = 0 AndAlso Player(MyIndex).YOffset = 0 Then
                If Map.Tile(GetPlayerX(MyIndex), GetPlayerY(MyIndex)).Type = TileType.Warp Then
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
        If Player(MyIndex).Moving <> 0 Then
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

        d = GetPlayerDir(MyIndex)

        If DirUp Then
            SetPlayerDir(MyIndex, DirectionType.Up)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerY(MyIndex) > 0 Then
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
            SetPlayerDir(MyIndex, DirectionType.Down)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerY(MyIndex) < Map.MaxY Then
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
            SetPlayerDir(MyIndex, DirectionType.Left)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerX(MyIndex) > 0 Then
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
            SetPlayerDir(MyIndex, DirectionType.Right)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerX(MyIndex) < Map.MaxX Then
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
        If IsDirBlocked(Map.Tile(GetPlayerX(MyIndex), GetPlayerY(MyIndex)).DirBlock, Direction + 1) Then
            CheckDirection = True
            Exit Function
        End If

        Select Case Direction
            Case Enums.DirectionType.Up
                X = GetPlayerX(MyIndex)
                Y = GetPlayerY(MyIndex) - 1
            Case Enums.DirectionType.Down
                X = GetPlayerX(MyIndex)
                Y = GetPlayerY(MyIndex) + 1
            Case Enums.DirectionType.Left
                X = GetPlayerX(MyIndex) - 1
                Y = GetPlayerY(MyIndex)
            Case Enums.DirectionType.Right
                X = GetPlayerX(MyIndex) + 1
                Y = GetPlayerY(MyIndex)
        End Select

        ' Check to see if the map tile is blocked or not
        If Map.Tile(X, Y).Type = TileType.Blocked Then
            CheckDirection = True
            Exit Function
        End If

        ' Check to see if the map tile is tree or not
        If Map.Tile(X, Y).Type = TileType.Resource Then
            CheckDirection = True
            Exit Function
        End If

        ' Check to see if the key door is open or not
        If Map.Tile(X, Y).Type = TileType.Key Then
            ' This actually checks if its open or not
            If TempTile(X, Y).DoorOpen = False Then
                CheckDirection = True
                Exit Function
            End If
        End If

        If FurnitureHouse > 0 AndAlso FurnitureHouse = Player(MyIndex).InHouse Then
            If FurnitureCount > 0 Then
                For i = 1 To FurnitureCount
                    If Item(Furniture(i).ItemNum).Data3 = 0 Then
                        If X >= Furniture(i).X AndAlso X <= Furniture(i).X + Item(Furniture(i).ItemNum).FurnitureWidth - 1 Then
                            If Y <= Furniture(i).Y AndAlso Y >= Furniture(i).Y - Item(Furniture(i).ItemNum).FurnitureHeight Then
                                z = Item(Furniture(i).ItemNum).FurnitureBlocks(X - Furniture(i).X, ((Furniture(i).Y - Y) * -1) + Item(Furniture(i).ItemNum).FurnitureHeight)
                                If z = 1 Then CheckDirection = True : Exit Function
                            End If
                        End If
                    End If
                Next
            End If
        End If

        ' Check to see if a player is already on that tile
        For i = 1 To MAX_PLAYERS
            If IsPlaying(i) AndAlso GetPlayerMap(i) = GetPlayerMap(MyIndex) Then
                If Player(i).InHouse = Player(MyIndex).InHouse Then
                    If GetPlayerX(i) = X Then
                        If GetPlayerY(i) = Y Then
                            CheckDirection = True
                            Exit Function
                        ElseIf Player(i).Pet.X = X AndAlso Player(i).Pet.Alive = True Then
                            If Player(i).Pet.Y = Y Then
                                CheckDirection = True
                                Exit Function
                            End If
                        End If
                    ElseIf Player(i).Pet.X = X AndAlso Player(i).Pet.Y = Y AndAlso Player(i).Pet.Alive = True Then
                        CheckDirection = True
                        Exit Function
                    End If
                End If
            End If
        Next

        ' Check to see if a npc is already on that tile
        For i = 1 To MAX_MAP_NPCS
            If MapNpc(i).Num > 0 AndAlso MapNpc(i).X = X AndAlso MapNpc(i).Y = Y Then
                CheckDirection = True
                Exit Function
            End If
        Next

        For i = 1 To Map.CurrentEvents
            If Map.MapEvents(i).Visible = 1 Then
                If Map.MapEvents(i).X = X AndAlso Map.MapEvents(i).Y = Y Then
                    If Map.MapEvents(i).WalkThrough = 0 Then
                        CheckDirection = True
                        Exit Function
                    End If
                End If
            End If
        Next

    End Function

    Sub ProcessMovement(index as integer)
        Dim movementSpeed As Integer

        ' Check if player is walking, and if so process moving them over
        Select Case Player(Index).Moving
            Case MovementType.Walking : MovementSpeed = ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
            Case MovementType.Running : MovementSpeed = ((ElapsedTime / 1000) * (RunSpeed * SizeX))
            Case Else : Exit Sub
        End Select

        Select Case GetPlayerDir(Index)
            Case DirectionType.Up
                Player(Index).YOffset = Player(Index).YOffset - MovementSpeed
                If Player(Index).YOffset < 0 Then Player(Index).YOffset = 0
            Case DirectionType.Down
                Player(Index).YOffset = Player(Index).YOffset + MovementSpeed
                If Player(Index).YOffset > 0 Then Player(Index).YOffset = 0
            Case DirectionType.Left
                Player(Index).XOffset = Player(Index).XOffset - MovementSpeed
                If Player(Index).XOffset < 0 Then Player(Index).XOffset = 0
            Case DirectionType.Right
                Player(Index).XOffset = Player(Index).XOffset + MovementSpeed
                If Player(Index).XOffset > 0 Then Player(Index).XOffset = 0
        End Select

        ' Check if completed walking over to the next tile
        If Player(Index).Moving > 0 Then
            If GetPlayerDir(Index) = DirectionType.Right OrElse GetPlayerDir(Index) = DirectionType.Down Then
                If (Player(Index).XOffset >= 0) AndAlso (Player(Index).YOffset >= 0) Then
                    Player(Index).Moving = 0
                    If Player(Index).Steps = 1 Then
                        Player(Index).Steps = 3
                    Else
                        Player(Index).Steps = 1
                    End If
                End If
            Else
                If (Player(Index).XOffset <= 0) AndAlso (Player(Index).YOffset <= 0) Then
                    Player(Index).Moving = 0
                    If Player(Index).Steps = 1 Then
                        Player(Index).Steps = 3
                    Else
                        Player(Index).Steps = 1
                    End If
                End If
            End If
        End If

    End Sub

    Function GetPlayerDir(index as integer) As Integer

        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerDir = Player(Index).Dir
    End Function

    Function GetPlayerGatherSkillLvl(index as integer, skillSlot As Integer) As Integer

        GetPlayerGatherSkillLvl = 0

        If Index > MAX_PLAYERS Then Exit Function

        GetPlayerGatherSkillLvl = Player(Index).GatherSkills(SkillSlot).SkillLevel
    End Function

    Function GetPlayerGatherSkillExp(index as integer, skillSlot As Integer) As Integer

        GetPlayerGatherSkillExp = 0

        If Index > MAX_PLAYERS Then Exit Function

        GetPlayerGatherSkillExp = Player(Index).GatherSkills(SkillSlot).SkillCurExp
    End Function

    Function GetPlayerGatherSkillMaxExp(index as integer, skillSlot As Integer) As Integer

        GetPlayerGatherSkillMaxExp = 0

        If Index > MAX_PLAYERS Then Exit Function

        GetPlayerGatherSkillMaxExp = Player(Index).GatherSkills(SkillSlot).SkillNextLvlExp
    End Function

    Friend Sub PlayerCastSkill(skillslot As Integer)
        dim buffer as New ByteStream(4)

        ' Check for subscript out of range
        If skillslot < 1 OrElse skillslot > MAX_PLAYER_SKILLS Then Exit Sub

        If SkillCD(skillslot) > 0 Then
            AddText("Skill has not cooled down yet!", QColorType.AlertColor)
            Exit Sub
        End If

        ' Check if player has enough MP
        If GetPlayerVital(MyIndex, VitalType.MP) < Skill(PlayerSkills(skillslot)).MpCost Then
            AddText("Not enough MP to cast " & Trim$(Skill(PlayerSkills(skillslot)).Name) & ".", QColorType.AlertColor)
            Exit Sub
        End If

        If PlayerSkills(skillslot) > 0 Then
            If GetTickCount() > Player(MyIndex).AttackTimer + 1000 Then
                If Player(MyIndex).Moving = 0 Then
                    Buffer.WriteInt32(ClientPackets.CCast)
                    Buffer.WriteInt32(skillslot)

                    Socket.SendData(Buffer.Data, Buffer.Head)
                    Buffer.Dispose()

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

    Sub SetPlayerMap(index as integer, mapNum as Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).Map = MapNum
    End Sub

    Function GetPlayerInvItemNum(index as integer, invslot As Integer) As Integer
        GetPlayerInvItemNum = 0
        If Index > MAX_PLAYERS Then Exit Function
        If invslot = 0 Then Exit Function
        GetPlayerInvItemNum = PlayerInv(invslot).Num
    End Function

    Sub SetPlayerName(index as integer, name As String)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).Name = Name
    End Sub

    Sub SetPlayerClass(index as integer, classnum As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).Classes = Classnum
    End Sub

    Sub SetPlayerPoints(index as integer, points As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).POINTS = POINTS
    End Sub

    Sub SetPlayerStat(index as integer, stat As StatType, value As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        If Value <= 0 Then Value = 1
        If Value > Byte.MaxValue Then Value = Byte.MaxValue
        Player(Index).Stat(Stat) = Value
    End Sub

    Sub SetPlayerInvItemNum(index as integer, invslot As Integer, itemnum As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        PlayerInv(invslot).Num = itemnum
    End Sub

    Function GetPlayerInvItemValue(index as integer, invslot As Integer) As Integer
        GetPlayerInvItemValue = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerInvItemValue = PlayerInv(invslot).Value
    End Function

    Sub SetPlayerInvItemValue(index as integer, invslot As Integer, itemValue As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        PlayerInv(invslot).Value = ItemValue
    End Sub

    Function GetPlayerPoints(index as integer) As Integer
        GetPlayerPOINTS = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerPOINTS = Player(Index).POINTS
    End Function

    Sub SetPlayerAccess(index as integer, access As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).Access = Access
    End Sub

    Sub SetPlayerPk(index as integer, pk As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).PK = PK
    End Sub

    Sub SetPlayerVital(index as integer, vital As VitalType, value As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).Vital(Vital) = Value

        If GetPlayerVital(Index, Vital) > GetPlayerMaxVital(Index, Vital) Then
            Player(Index).Vital(Vital) = GetPlayerMaxVital(Index, Vital)
        End If
    End Sub

    Function GetPlayerMaxVital(index as integer, vital As VitalType) As Integer
        GetPlayerMaxVital = 0
        If Index > MAX_PLAYERS Then Exit Function

        Select Case Vital
            Case VitalType.HP
                GetPlayerMaxVital = Player(Index).MaxHP
            Case VitalType.MP
                GetPlayerMaxVital = Player(Index).MaxMP
            Case VitalType.SP
                GetPlayerMaxVital = Player(Index).MaxSP
        End Select

    End Function

    Sub SetPlayerX(index as integer, x As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).X = X
    End Sub

    Sub SetPlayerY(index as integer, y As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).Y = Y
    End Sub

    Sub SetPlayerSprite(index as integer, sprite As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).Sprite = Sprite
    End Sub

    Sub SetPlayerExp(index as integer, exp As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).EXP = EXP
    End Sub

    Sub SetPlayerLevel(index as integer, level As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).Level = Level
    End Sub

    Sub SetPlayerDir(index as integer, dir As Integer)
        If Index > MAX_PLAYERS Then Exit Sub
        Player(Index).Dir = Dir
    End Sub

    Function GetPlayerVital(index as integer, vital As VitalType) As Integer
        GetPlayerVital = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerVital = Player(Index).Vital(Vital)
    End Function

    Function GetPlayerSprite(index as integer) As Integer
        GetPlayerSprite = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerSprite = Player(Index).Sprite
    End Function

    Function GetPlayerClass(index as integer) As Integer
        GetPlayerClass = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerClass = Player(Index).Classes
    End Function

    Function GetPlayerMap(index as integer) As Integer
        GetPlayerMap = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerMap = Player(Index).Map
    End Function

    Function GetPlayerLevel(index as integer) As Integer
        GetPlayerLevel = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerLevel = Player(Index).Level
    End Function

    Function GetPlayerEquipment(index as integer, equipmentSlot As EquipmentType) As Byte
        GetPlayerEquipment = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerEquipment = Player(Index).Equipment(EquipmentSlot)
    End Function

    Function GetPlayerStat(index as integer, stat As StatType) As Integer
        GetPlayerStat = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerStat = Player(Index).Stat(Stat)
    End Function

    Function GetPlayerExp(index as integer) As Integer
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerExp = Player(Index).EXP
    End Function

    Function GetPlayerX(index as integer) As Integer
        GetPlayerX = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerX = Player(Index).X
    End Function

    Function GetPlayerY(index as integer) As Integer
        GetPlayerY = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerY = Player(Index).Y
    End Function

    Function GetPlayerAccess(index as integer) As Integer
        GetPlayerAccess = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerAccess = Player(Index).Access
    End Function

    Function GetPlayerPk(index as integer) As Integer
        GetPlayerPK = 0
        If Index > MAX_PLAYERS Then Exit Function
        GetPlayerPK = Player(Index).PK
    End Function

    Sub SetPlayerEquipment(index as integer, invNum As Integer, equipmentSlot As EquipmentType)
        If Index < 1 OrElse Index > MAX_PLAYERS Then Exit Sub
        Player(Index).Equipment(EquipmentSlot) = InvNum
    End Sub

    Sub ClearPlayer(index as integer)
        Player(Index).Name = ""
        Player(Index).Access = 0
        Player(Index).Attacking = 0
        Player(Index).AttackTimer = 0
        Player(Index).Classes = 0
        Player(Index).Dir = 0

        ReDim Player(Index).Equipment(EquipmentType.Count - 1)
        For y = 1 To EquipmentType.Count - 1
            Player(Index).Equipment(y) = 0
        Next

        Player(Index).EXP = 0
        Player(Index).Level = 0
        Player(Index).Map = 0
        Player(Index).MapGetTimer = 0
        Player(Index).MaxHP = 0
        Player(Index).MaxMP = 0
        Player(Index).MaxSP = 0
        Player(Index).Moving = 0
        Player(Index).PK = 0
        Player(Index).POINTS = 0
        Player(Index).Sprite = 0

        ReDim Player(Index).Stat(StatType.Count - 1)
        For x = 1 To StatType.Count - 1
            Player(Index).Stat(x) = 0
        Next

        Player(Index).Steps = 0

        ReDim Player(Index).Vital(VitalType.Count - 1)
        For i = 1 To VitalType.Count - 1
            Player(Index).Vital(i) = 0
        Next

        Player(Index).X = 0
        Player(Index).XOffset = 0
        Player(Index).Y = 0
        Player(Index).YOffset = 0

        ReDim Player(Index).RandEquip(EquipmentType.Count - 1)
        For y = 1 To EquipmentType.Count - 1
            ReDim Player(Index).RandEquip(y).Stat(StatType.Count - 1)
            For x = 1 To StatType.Count - 1
                Player(Index).RandEquip(y).Stat(x) = 0
            Next
        Next

        ReDim Player(Index).RandInv(MAX_INV)
        For y = 1 To MAX_INV
            ReDim Player(Index).RandInv(y).Stat(StatType.Count - 1)
            For x = 1 To StatType.Count - 1
                Player(Index).RandInv(y).Stat(x) = 0
            Next
        Next

        ReDim Player(Index).PlayerQuest(MaxQuests)

        ReDim Player(Index).Hotbar(MaxHotbar)

        ReDim Player(Index).GatherSkills(ResourceSkills.Count - 1)

        ReDim Player(Index).RecipeLearned(MAX_RECIPE)

        'pets
        Player(Index).Pet.Num = 0
        Player(Index).Pet.Health = 0
        Player(Index).Pet.Mana = 0
        Player(Index).Pet.Level = 0

        ReDim Player(Index).Pet.Stat(StatType.Count - 1)
        For i = 1 To StatType.Count - 1
            Player(Index).Pet.Stat(i) = 0
        Next

        ReDim Player(Index).Pet.Skill(4)
        For i = 1 To 4
            Player(Index).Pet.Skill(i) = 0
        Next

        Player(Index).Pet.X = 0
        Player(Index).Pet.Y = 0
        Player(Index).Pet.Dir = 0
        Player(Index).Pet.MaxHp = 0
        Player(Index).Pet.MaxMP = 0
        Player(Index).Pet.Alive = 0
        Player(Index).Pet.AttackBehaviour = 0
        Player(Index).Pet.Exp = 0
        Player(Index).Pet.TNL = 0
    End Sub
End Module