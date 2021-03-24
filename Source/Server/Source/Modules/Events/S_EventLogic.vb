Imports Asfw

Friend Module S_EventLogic

    Friend Sub RemoveDeadEvents()
        Dim i As Integer, mapNum As Integer, x As Integer, id As Integer, page As Integer, compare As Integer

        For i = 1 To GetPlayersOnline()
            If Gettingmap = True Then Exit Sub

            If IsPlaying(i) = False Then
                TempPlayer(i).EventMap.CurrentEvents = 0
                Exit Sub
            End If

            If TempPlayer(i).EventMap.CurrentEvents > 0 Then 'And Map(MapNum).Events IsNot Nothing Then
                mapNum = GetPlayerMap(i)

                For x = 1 To TempPlayer(i).EventMap.CurrentEvents
                    id = TempPlayer(i).EventMap.EventPages(x).EventId
                    page = TempPlayer(i).EventMap.EventPages(x).PageId

                    If Map(mapNum).Events(id).PageCount >= page Then

                        'See if there is any reason to delete this event....
                        'In other words, go back through conditions and make sure they all check up.
                        If TempPlayer(i).EventMap.EventPages(x).Visible = 1 Then
                            If Map(mapNum).Events(id).Pages(page).ChkHasItem = 1 Then
                                If HasItem(i, Map(mapNum).Events(id).Pages(page).HasItemindex) = 0 Then
                                    TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                End If
                            End If

                            If Map(mapNum).Events(id).Pages(page).ChkSelfSwitch = 1 Then
                                If Map(mapNum).Events(id).Pages(page).SelfSwitchCompare = 0 Then
                                    compare = 1
                                Else
                                    compare = 0
                                End If
                                If Map(mapNum).Events(id).Globals = 1 Then
                                    If Map(mapNum).Events(id).SelfSwitches(Map(mapNum).Events(id).Pages(page).SelfSwitchindex) <> compare Then
                                        TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                    End If
                                Else
                                    If TempPlayer(i).EventMap.EventPages(id).SelfSwitches(Map(mapNum).Events(id).Pages(page).SelfSwitchindex) <> compare Then
                                        TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                    End If
                                End If
                            End If

                            If Map(mapNum).Events(id).Pages(page).ChkVariable = 1 Then
                                Select Case Map(mapNum).Events(id).Pages(page).VariableCompare
                                    Case 0
                                        If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(page).Variableindex) <> Map(mapNum).Events(id).Pages(page).VariableCondition Then
                                            TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                        End If
                                    Case 1
                                        If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(page).Variableindex) < Map(mapNum).Events(id).Pages(page).VariableCondition Then
                                            TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                        End If
                                    Case 2
                                        If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(page).Variableindex) > Map(mapNum).Events(id).Pages(page).VariableCondition Then
                                            TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                        End If
                                    Case 3
                                        If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(page).Variableindex) <= Map(mapNum).Events(id).Pages(page).VariableCondition Then
                                            TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                        End If
                                    Case 4
                                        If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(page).Variableindex) >= Map(mapNum).Events(id).Pages(page).VariableCondition Then
                                            TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                        End If
                                    Case 5
                                        If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(page).Variableindex) = Map(mapNum).Events(id).Pages(page).VariableCondition Then
                                            TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                        End If
                                End Select
                            End If

                            If Map(mapNum).Events(id).Pages(page).ChkSwitch = 1 Then
                                If Map(mapNum).Events(id).Pages(page).SwitchCompare = 1 Then 'we expect true
                                    If Player(i).Character(TempPlayer(i).CurChar).Switches(Map(mapNum).Events(id).Pages(page).Switchindex) = 0 Then ' we see false so we despawn the event
                                        TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                    End If
                                Else
                                    If Player(i).Character(TempPlayer(i).CurChar).Switches(Map(mapNum).Events(id).Pages(page).Switchindex) = 1 Then ' we expect false and we see true so we despawn the event
                                        TempPlayer(i).EventMap.EventPages(x).Visible = 0
                                    End If
                                End If
                            End If

                            If Map(mapNum).Events(id).Globals = 1 AndAlso TempPlayer(i).EventMap.EventPages(x).Visible = 0 Then TempEventMap(mapNum).Events(id).Active = 0

                            If TempPlayer(i).EventMap.EventPages(x).Visible = 0 Then
                                Dim Buffer As New ByteStream(4)
                                Buffer.WriteInt32(ServerPackets.SSpawnEvent)
                                Buffer.WriteInt32(id)
                                With TempPlayer(i).EventMap.EventPages(x)
                                    Buffer.WriteString((Trim(Map(GetPlayerMap(i)).Events(.EventId).Name)))
                                    Buffer.WriteInt32(.Dir)
                                    Buffer.WriteInt32(.GraphicNum)
                                    Buffer.WriteInt32(.GraphicType)
                                    Buffer.WriteInt32(.GraphicX)
                                    Buffer.WriteInt32(.GraphicX2)
                                    Buffer.WriteInt32(.GraphicY)
                                    Buffer.WriteInt32(.GraphicY2)
                                    Buffer.WriteInt32(.MovementSpeed)
                                    Buffer.WriteInt32(.X)
                                    Buffer.WriteInt32(.Y)
                                    Buffer.WriteInt32(.Position)
                                    Buffer.WriteInt32(.Visible)
                                    Buffer.WriteInt32(Map(mapNum).Events(id).Pages(page).WalkAnim)
                                    Buffer.WriteInt32(Map(mapNum).Events(id).Pages(page).DirFix)
                                    Buffer.WriteInt32(Map(mapNum).Events(id).Pages(page).WalkThrough)
                                    Buffer.WriteInt32(Map(mapNum).Events(id).Pages(page).ShowName)
                                    Buffer.WriteInt32(.QuestNum)
                                End With
                                Socket.SendDataTo(i, Buffer.Data, Buffer.Head)

                                Addlog("Sent SMSG: SSpawnEvent Remove Dead Events", PACKET_LOG)
                                Console.WriteLine("Sent SMSG: SSpawnEvent Remove Dead Events")

                                Buffer.Dispose()
                            End If
                        End If
                    End If
                Next
            End If
        Next

    End Sub

    Friend Sub SpawnNewEvents()
        Dim pageID As Integer, id As Integer, compare As Integer, i As Integer, mapNum As Integer
        Dim n As Integer, x As Integer, z As Integer, spawnevent As Boolean, p As Integer

        'That was only removing events... now we gotta worry about spawning them again, luckily, it is almost the same exact thing, but backwards!

        For i = 1 To GetPlayersOnline()
            If Gettingmap = True Then Exit Sub

            If TempPlayer(i).EventMap.CurrentEvents > 0 Then
                mapNum = GetPlayerMap(i)
                If mapNum = 0 Then Exit Sub

                For x = 1 To TempPlayer(i).EventMap.CurrentEvents
                    id = TempPlayer(i).EventMap.EventPages(x).EventId
                    pageID = TempPlayer(i).EventMap.EventPages(x).PageId

                    If TempPlayer(i).EventMap.EventPages(x).Visible = 0 Then pageID = 0

                    'If (Map(MapNum).Events Is Nothing) Then Continue For
                    For z = Map(mapNum).Events(id).PageCount To 1 Step -1

                        spawnevent = True

                        If Map(mapNum).Events(id).Pages(z).ChkHasItem = 1 Then
                            If HasItem(i, Map(mapNum).Events(id).Pages(z).HasItemindex) = 0 Then
                                spawnevent = False
                            End If
                        End If

                        If Map(mapNum).Events(id).Pages(z).ChkSelfSwitch = 1 Then
                            If Map(mapNum).Events(id).Pages(z).SelfSwitchCompare = 0 Then
                                compare = 1
                            Else
                                compare = 0
                            End If
                            If Map(mapNum).Events(id).Globals = 1 Then
                                If Map(mapNum).Events(id).SelfSwitches(Map(mapNum).Events(id).Pages(z).SelfSwitchindex) <> compare Then
                                    spawnevent = False
                                End If
                            Else
                                If TempPlayer(i).EventMap.EventPages(id).SelfSwitches(Map(mapNum).Events(id).Pages(z).SelfSwitchindex) <> compare Then
                                    spawnevent = False
                                End If
                            End If
                        End If

                        If Map(mapNum).Events(id).Pages(z).ChkVariable = 1 Then
                            Select Case Map(mapNum).Events(id).Pages(z).VariableCompare
                                Case 0
                                    If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(z).Variableindex) <> Map(mapNum).Events(id).Pages(z).VariableCondition Then
                                        spawnevent = False
                                    End If
                                Case 1
                                    If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(z).Variableindex) < Map(mapNum).Events(id).Pages(z).VariableCondition Then
                                        spawnevent = False
                                    End If
                                Case 2
                                    If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(z).Variableindex) > Map(mapNum).Events(id).Pages(z).VariableCondition Then
                                        spawnevent = False
                                    End If
                                Case 3
                                    If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(z).Variableindex) <= Map(mapNum).Events(id).Pages(z).VariableCondition Then
                                        spawnevent = False
                                    End If
                                Case 4
                                    If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(z).Variableindex) >= Map(mapNum).Events(id).Pages(z).VariableCondition Then
                                        spawnevent = False
                                    End If
                                Case 5
                                    If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(mapNum).Events(id).Pages(z).Variableindex) = Map(mapNum).Events(id).Pages(z).VariableCondition Then
                                        spawnevent = False
                                    End If
                            End Select
                        End If

                        If Map(mapNum).Events(id).Pages(z).ChkSwitch = 1 Then
                            If Map(mapNum).Events(id).Pages(z).SwitchCompare = 0 Then 'we want false
                                If Player(i).Character(TempPlayer(i).CurChar).Switches(Map(mapNum).Events(id).Pages(z).Switchindex) = 1 Then 'and switch is true
                                    spawnevent = False 'do not spawn
                                End If
                            Else
                                If Player(i).Character(TempPlayer(i).CurChar).Switches(Map(mapNum).Events(id).Pages(z).Switchindex) = 0 Then ' else we want true and the switch is false
                                    spawnevent = False
                                End If
                            End If
                        End If

                        If spawnevent = True Then
                            If TempPlayer(i).EventMap.EventPages(x).Visible = 1 Then
                                If z <= pageID Then
                                    spawnevent = False
                                End If
                            End If
                        End If

                        If spawnevent = True Then

                            If TempPlayer(i).EventProcessingCount > 0 Then
                                For n = 1 To UBound(TempPlayer(i).EventProcessing)
                                    If TempPlayer(i).EventProcessing(n).EventId = id Then
                                        TempPlayer(i).EventProcessing(n).Active = 0
                                    End If
                                Next
                            End If

                            With TempPlayer(i).EventMap.EventPages(id)
                                If Map(mapNum).Events(id).Pages(z).GraphicType = 1 Then
                                    Select Case Map(mapNum).Events(id).Pages(z).GraphicY
                                        Case 0
                                            .Dir = DirectionType.Down
                                        Case 1
                                            .Dir = DirectionType.Left
                                        Case 2
                                            .Dir = DirectionType.Right
                                        Case 3
                                            .Dir = DirectionType.Up
                                    End Select
                                Else
                                    .Dir = 0
                                End If
                                .GraphicNum = Map(mapNum).Events(id).Pages(z).Graphic
                                .GraphicType = Map(mapNum).Events(id).Pages(z).GraphicType
                                .GraphicX = Map(mapNum).Events(id).Pages(z).GraphicX
                                .GraphicY = Map(mapNum).Events(id).Pages(z).GraphicY
                                .GraphicX2 = Map(mapNum).Events(id).Pages(z).GraphicX2
                                .GraphicY2 = Map(mapNum).Events(id).Pages(z).GraphicY2
                                .QuestNum = Map(mapNum).Events(id).Pages(z).QuestNum
                                Select Case Map(mapNum).Events(id).Pages(z).MoveSpeed
                                    Case 0
                                        .MovementSpeed = 2
                                    Case 1
                                        .MovementSpeed = 3
                                    Case 2
                                        .MovementSpeed = 4
                                    Case 3
                                        .MovementSpeed = 6
                                    Case 4
                                        .MovementSpeed = 12
                                    Case 5
                                        .MovementSpeed = 24
                                End Select
                                .Position = Map(mapNum).Events(id).Pages(z).Position
                                .EventId = id
                                .PageId = z
                                .Visible = 1

                                .MoveType = Map(mapNum).Events(id).Pages(z).MoveType
                                If .MoveType = 2 Then
                                    .MoveRouteCount = Map(mapNum).Events(id).Pages(z).MoveRouteCount
                                    If .MoveRouteCount > 0 Then
                                        ReDim .MoveRoute(Map(mapNum).Events(id).Pages(z).MoveRouteCount)
                                        For p = 0 To Map(mapNum).Events(id).Pages(z).MoveRouteCount
                                            .MoveRoute(p) = Map(mapNum).Events(id).Pages(z).MoveRoute(p)
                                        Next
                                        .MoveRouteComplete = 0
                                    Else
                                        .MoveRouteComplete = 1
                                    End If
                                Else
                                    .MoveRouteComplete = 1
                                End If

                                .RepeatMoveRoute = Map(mapNum).Events(id).Pages(z).RepeatMoveRoute
                                .IgnoreIfCannotMove = Map(mapNum).Events(id).Pages(z).IgnoreMoveRoute

                                .MoveFreq = Map(mapNum).Events(id).Pages(z).MoveFreq
                                .MoveSpeed = Map(mapNum).Events(id).Pages(z).MoveSpeed

                                .WalkThrough = Map(mapNum).Events(id).Pages(z).WalkThrough
                                .ShowName = Map(mapNum).Events(id).Pages(z).ShowName
                                .WalkingAnim = Map(mapNum).Events(id).Pages(z).WalkAnim
                                .FixedDir = Map(mapNum).Events(id).Pages(z).DirFix

                            End With

                            If Map(mapNum).Events(id).Globals = 1 Then
                                If spawnevent Then TempEventMap(mapNum).Events(id).Active = z : TempEventMap(mapNum).Events(id).Position = Map(mapNum).Events(id).Pages(z).Position
                            End If

                            Dim Buffer = New ByteStream(4)
                            Buffer.WriteInt32(ServerPackets.SSpawnEvent)
                            Buffer.WriteInt32(id)
                            With TempPlayer(i).EventMap.EventPages(x)
                                Buffer.WriteString((Trim(Map(GetPlayerMap(i)).Events(.EventId).Name)))
                                Buffer.WriteInt32(.Dir)
                                Buffer.WriteInt32(.GraphicNum)
                                Buffer.WriteInt32(.GraphicType)
                                Buffer.WriteInt32(.GraphicX)
                                Buffer.WriteInt32(.GraphicX2)
                                Buffer.WriteInt32(.GraphicY)
                                Buffer.WriteInt32(.GraphicY2)
                                Buffer.WriteInt32(.MovementSpeed)
                                Buffer.WriteInt32(.X)
                                Buffer.WriteInt32(.Y)
                                Buffer.WriteInt32(.Position)
                                Buffer.WriteInt32(.Visible)
                                Buffer.WriteInt32(Map(mapNum).Events(id).Pages(z).WalkAnim)
                                Buffer.WriteInt32(Map(mapNum).Events(id).Pages(z).DirFix)
                                Buffer.WriteInt32(Map(mapNum).Events(id).Pages(z).WalkThrough)
                                Buffer.WriteInt32(Map(mapNum).Events(id).Pages(z).ShowName)
                                Buffer.WriteInt32(Map(mapNum).Events(id).Pages(z).QuestNum)
                                Buffer.WriteInt32(.QuestNum)
                            End With
                            Socket.SendDataTo(i, Buffer.Data, Buffer.Head)

                            AddDebug("Sent SMSG: SSpawnEvent Spawn New Events")

                            Buffer.Dispose()
                            z = 1
                        End If
                    Next
                Next
            End If
        Next

    End Sub

    Friend Sub ProcessEventMovement()
        Dim rand As Integer, x As Integer, i As Integer, playerID As Integer, eventID As Integer, WalkThrough As Integer, isglobal As Boolean, mapNum As Integer
        Dim actualmovespeed As Integer, Buffer As ByteStream, z As Integer, sendupdate As Boolean
        Dim donotprocessmoveroute As Boolean, pageNum As Integer

        'Process Movement if needed for each player/each map/each event....

        For i = 1 To MAX_MAPS
            If Gettingmap = True Then Exit Sub

            If PlayersOnMap(i) Then
                'Manage Global Events First, then all the others.....
                If TempEventMap(i).EventCount > 0 Then
                    For x = 1 To TempEventMap(i).EventCount
                        If TempEventMap(i).Events(x).Active > 0 Then
                            pageNum = 1
                            If TempEventMap(i).Events(x).MoveTimer <= GetTimeMs() Then
                                'Real event! Lets process it!
                                Select Case TempEventMap(i).Events(x).MoveType
                                    Case 0
                                        'Nothing, fixed position
                                    Case 1 'Random, move randomly if possible...
                                        rand = Random(0, 3)
                                        If CanEventMove(0, i, TempEventMap(i).Events(x).X, TempEventMap(i).Events(x).Y, x, TempEventMap(i).Events(x).WalkThrough, rand, True) Then
                                            Select Case TempEventMap(i).Events(x).MoveSpeed
                                                Case 0
                                                    EventMove(0, i, x, rand, 2, True)
                                                Case 1
                                                    EventMove(0, i, x, rand, 3, True)
                                                Case 2
                                                    EventMove(0, i, x, rand, 4, True)
                                                Case 3
                                                    EventMove(0, i, x, rand, 6, True)
                                                Case 4
                                                    EventMove(0, i, x, rand, 12, True)
                                                Case 5
                                                    EventMove(0, i, x, rand, 24, True)
                                            End Select
                                        Else
                                            EventDir(0, i, x, rand, True)
                                        End If
                                    Case 2 'Move Route
                                        With TempEventMap(i).Events(x)
                                            isglobal = True
                                            mapNum = i
                                            playerID = 0
                                            eventID = x
                                            WalkThrough = TempEventMap(i).Events(x).WalkThrough
                                            If .MoveRouteCount > 0 Then
                                                If .MoveRouteStep >= .MoveRouteCount AndAlso .RepeatMoveRoute = 1 Then
                                                    .MoveRouteStep = 0
                                                    .MoveRouteComplete = 1
                                                ElseIf .MoveRouteStep >= .MoveRouteCount AndAlso .RepeatMoveRoute = 0 Then
                                                    donotprocessmoveroute = True
                                                    .MoveRouteComplete = 1
                                                Else
                                                    .MoveRouteComplete = 0
                                                End If
                                                If donotprocessmoveroute = False Then
                                                    .MoveRouteStep = .MoveRouteStep + 1
                                                    Select Case .MoveSpeed
                                                        Case 0
                                                            actualmovespeed = 2
                                                        Case 1
                                                            actualmovespeed = 3
                                                        Case 2
                                                            actualmovespeed = 4
                                                        Case 3
                                                            actualmovespeed = 6
                                                        Case 4
                                                            actualmovespeed = 12
                                                        Case 5
                                                            actualmovespeed = 24
                                                    End Select
                                                    Select Case .MoveRoute(.MoveRouteStep).Index
                                                        Case 1
                                                            If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, DirectionType.Up, isglobal) Then
                                                                EventMove(playerID, mapNum, eventID, DirectionType.Up, actualmovespeed, isglobal)
                                                            Else
                                                                If .IgnoreIfCannotMove = 0 Then
                                                                    .MoveRouteStep = .MoveRouteStep - 1
                                                                End If
                                                            End If
                                                        Case 2
                                                            If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, DirectionType.Down, isglobal) Then
                                                                EventMove(playerID, mapNum, eventID, DirectionType.Down, actualmovespeed, isglobal)
                                                            Else
                                                                If .IgnoreIfCannotMove = 0 Then
                                                                    .MoveRouteStep = .MoveRouteStep - 1
                                                                End If
                                                            End If
                                                        Case 3
                                                            If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, DirectionType.Left, isglobal) Then
                                                                EventMove(playerID, mapNum, eventID, DirectionType.Left, actualmovespeed, isglobal)
                                                            Else
                                                                If .IgnoreIfCannotMove = 0 Then
                                                                    .MoveRouteStep = .MoveRouteStep - 1
                                                                End If
                                                            End If
                                                        Case 4
                                                            If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, DirectionType.Right, isglobal) Then
                                                                EventMove(playerID, mapNum, eventID, DirectionType.Right, actualmovespeed, isglobal)
                                                            Else
                                                                If .IgnoreIfCannotMove = 0 Then
                                                                    .MoveRouteStep = .MoveRouteStep - 1
                                                                End If
                                                            End If
                                                        Case 5
                                                            z = Random(0, 3)
                                                            If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, z, isglobal) Then
                                                                EventMove(playerID, mapNum, eventID, z, actualmovespeed, isglobal)
                                                            Else
                                                                If .IgnoreIfCannotMove = 0 Then
                                                                    .MoveRouteStep = .MoveRouteStep - 1
                                                                End If
                                                            End If
                                                        Case 6
                                                            If isglobal = False Then
                                                                If IsOneBlockAway(.X, .Y, GetPlayerX(playerID), GetPlayerY(playerID)) = True Then
                                                                    EventDir(playerID, GetPlayerMap(playerID), eventID, GetDirToPlayer(playerID, GetPlayerMap(playerID), eventID), False)
                                                                    If .IgnoreIfCannotMove = 0 Then
                                                                        .MoveRouteStep = .MoveRouteStep - 1
                                                                    End If
                                                                Else
                                                                    z = CanEventMoveTowardsPlayer(playerID, mapNum, eventID)
                                                                    If z >= 4 Then
                                                                        'No
                                                                        If .IgnoreIfCannotMove = 0 Then
                                                                            .MoveRouteStep = .MoveRouteStep - 1
                                                                        End If
                                                                    Else
                                                                        'i is the direct, lets go...
                                                                        If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, z, isglobal) Then
                                                                            EventMove(playerID, mapNum, eventID, z, actualmovespeed, isglobal)
                                                                        Else
                                                                            If .IgnoreIfCannotMove = 0 Then
                                                                                .MoveRouteStep = .MoveRouteStep - 1
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        Case 7
                                                            If isglobal = False Then
                                                                z = CanEventMoveAwayFromPlayer(playerID, mapNum, eventID)
                                                                If z >= 5 Then
                                                                    'No
                                                                Else
                                                                    'i is the direct, lets go...
                                                                    If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, z, isglobal) Then
                                                                        EventMove(playerID, mapNum, eventID, z, actualmovespeed, isglobal)
                                                                    Else
                                                                        If .IgnoreIfCannotMove = 0 Then
                                                                            .MoveRouteStep = .MoveRouteStep - 1
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        Case 8
                                                            If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, .Dir, isglobal) Then
                                                                EventMove(playerID, mapNum, eventID, .Dir, actualmovespeed, isglobal)
                                                            Else
                                                                If .IgnoreIfCannotMove = 0 Then
                                                                    .MoveRouteStep = .MoveRouteStep - 1
                                                                End If
                                                            End If
                                                        Case 9
                                                            Select Case .Dir
                                                                Case DirectionType.Up
                                                                    z = DirectionType.Down
                                                                Case DirectionType.Down
                                                                    z = DirectionType.Up
                                                                Case DirectionType.Left
                                                                    z = DirectionType.Right
                                                                Case DirectionType.Right
                                                                    z = DirectionType.Left
                                                            End Select
                                                            If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, z, isglobal) Then
                                                                EventMove(playerID, mapNum, eventID, z, actualmovespeed, isglobal)
                                                            Else
                                                                If .IgnoreIfCannotMove = 0 Then
                                                                    .MoveRouteStep = .MoveRouteStep - 1
                                                                End If
                                                            End If
                                                        Case 10
                                                            .MoveTimer = GetTimeMs() + 100
                                                        Case 11
                                                            .MoveTimer = GetTimeMs() + 500
                                                        Case 12
                                                            .MoveTimer = GetTimeMs() + 1000
                                                        Case 13
                                                            EventDir(playerID, mapNum, eventID, DirectionType.Up, isglobal)
                                                        Case 14
                                                            EventDir(playerID, mapNum, eventID, DirectionType.Down, isglobal)
                                                        Case 15
                                                            EventDir(playerID, mapNum, eventID, DirectionType.Left, isglobal)
                                                        Case 16
                                                            EventDir(playerID, mapNum, eventID, DirectionType.Right, isglobal)
                                                        Case 17
                                                            Select Case .Dir
                                                                Case DirectionType.Up
                                                                    z = DirectionType.Right
                                                                Case DirectionType.Right
                                                                    z = DirectionType.Down
                                                                Case DirectionType.Left
                                                                    z = DirectionType.Up
                                                                Case DirectionType.Down
                                                                    z = DirectionType.Left
                                                            End Select
                                                            EventDir(playerID, mapNum, eventID, z, isglobal)
                                                        Case 18
                                                            Select Case .Dir
                                                                Case DirectionType.Up
                                                                    z = DirectionType.Left
                                                                Case DirectionType.Right
                                                                    z = DirectionType.Up
                                                                Case DirectionType.Left
                                                                    z = DirectionType.Down
                                                                Case DirectionType.Down
                                                                    z = DirectionType.Right
                                                            End Select
                                                            EventDir(playerID, mapNum, eventID, z, isglobal)
                                                        Case 19
                                                            Select Case .Dir
                                                                Case DirectionType.Up
                                                                    z = DirectionType.Down
                                                                Case DirectionType.Right
                                                                    z = DirectionType.Left
                                                                Case DirectionType.Left
                                                                    z = DirectionType.Right
                                                                Case DirectionType.Down
                                                                    z = DirectionType.Up
                                                            End Select
                                                            EventDir(playerID, mapNum, eventID, z, isglobal)
                                                        Case 20
                                                            z = Random(0, 3)
                                                            EventDir(playerID, mapNum, eventID, z, isglobal)
                                                        Case 21
                                                            If isglobal = False Then
                                                                z = GetDirToPlayer(playerID, mapNum, eventID)
                                                                EventDir(playerID, mapNum, eventID, z, isglobal)
                                                            End If
                                                        Case 22
                                                            If isglobal = False Then
                                                                z = GetDirAwayFromPlayer(playerID, mapNum, eventID)
                                                                EventDir(playerID, mapNum, eventID, z, isglobal)
                                                            End If
                                                        Case 23
                                                            .MoveSpeed = 0
                                                        Case 24
                                                            .MoveSpeed = 1
                                                        Case 25
                                                            .MoveSpeed = 2
                                                        Case 26
                                                            .MoveSpeed = 3
                                                        Case 27
                                                            .MoveSpeed = 4
                                                        Case 28
                                                            .MoveSpeed = 5
                                                        Case 29
                                                            .MoveFreq = 0
                                                        Case 30
                                                            .MoveFreq = 1
                                                        Case 31
                                                            .MoveFreq = 2
                                                        Case 32
                                                            .MoveFreq = 3
                                                        Case 33
                                                            .MoveFreq = 4
                                                        Case 34
                                                            .WalkingAnim = 1
                                                            'Need to send update to client
                                                            sendupdate = True
                                                        Case 35
                                                            .WalkingAnim = 0
                                                            'Need to send update to client
                                                            sendupdate = True
                                                        Case 36
                                                            .FixedDir = 1
                                                            'Need to send update to client
                                                            sendupdate = True
                                                        Case 37
                                                            .FixedDir = 0
                                                            'Need to send update to client
                                                            sendupdate = True
                                                        Case 38
                                                            .WalkThrough = 1
                                                        Case 39
                                                            .WalkThrough = 0
                                                        Case 40
                                                            .Position = 0
                                                            'Need to send update to client
                                                            sendupdate = True
                                                        Case 41
                                                            .Position = 1
                                                            'Need to send update to client
                                                            sendupdate = True
                                                        Case 42
                                                            .Position = 2
                                                            'Need to send update to client
                                                            sendupdate = True
                                                        Case 43
                                                            .GraphicType = .MoveRoute(.MoveRouteStep).Data1
                                                            .GraphicNum = .MoveRoute(.MoveRouteStep).Data2
                                                            .GraphicX = .MoveRoute(.MoveRouteStep).Data3
                                                            .GraphicX2 = .MoveRoute(.MoveRouteStep).Data4
                                                            .GraphicY = .MoveRoute(.MoveRouteStep).Data5
                                                            .GraphicY2 = .MoveRoute(.MoveRouteStep).Data6
                                                            If .GraphicType = 1 Then
                                                                Select Case .GraphicY
                                                                    Case 0
                                                                        .Dir = DirectionType.Down
                                                                    Case 1
                                                                        .Dir = DirectionType.Left
                                                                    Case 2
                                                                        .Dir = DirectionType.Right
                                                                    Case 3
                                                                        .Dir = DirectionType.Up
                                                                End Select
                                                            End If
                                                            'Need to Send Update to client
                                                            sendupdate = True
                                                    End Select

                                                    If sendupdate Then
                                                        Buffer = New ByteStream(4)
                                                        Buffer.WriteInt32(ServerPackets.SSpawnEvent)
                                                        Buffer.WriteInt32(eventID)
                                                        With TempEventMap(i).Events(x)
                                                            Buffer.WriteString((Trim(Map(i).Events(x).Name)))
                                                            Buffer.WriteInt32(.Dir)
                                                            Buffer.WriteInt32(.GraphicNum)
                                                            Buffer.WriteInt32(.GraphicType)
                                                            Buffer.WriteInt32(.GraphicX)
                                                            Buffer.WriteInt32(.GraphicX2)
                                                            Buffer.WriteInt32(.GraphicY)
                                                            Buffer.WriteInt32(.GraphicY2)
                                                            Buffer.WriteInt32(.MoveSpeed)
                                                            Buffer.WriteInt32(.X)
                                                            Buffer.WriteInt32(.Y)
                                                            Buffer.WriteInt32(.Position)
                                                            Buffer.WriteInt32(.Active)
                                                            Buffer.WriteInt32(.WalkingAnim)
                                                            Buffer.WriteInt32(.FixedDir)
                                                            Buffer.WriteInt32(.WalkThrough)
                                                            Buffer.WriteInt32(.ShowName)
                                                            Buffer.WriteInt32(.QuestNum)
                                                        End With
                                                        SendDataToMap(i, Buffer.Data, Buffer.Head)

                                                        Addlog("Sent SMSG: SSpawnEvent Process Event Movement", PACKET_LOG)
                                                        Console.WriteLine("Sent SMSG: SSpawnEvent Process Event Movement")

                                                        Buffer.Dispose()
                                                    End If
                                                End If
                                                donotprocessmoveroute = False
                                            End If
                                        End With
                                End Select

                                Select Case TempEventMap(i).Events(x).MoveFreq
                                    Case 0
                                        TempEventMap(i).Events(x).MoveTimer = GetTimeMs() + 4000
                                    Case 1
                                        TempEventMap(i).Events(x).MoveTimer = GetTimeMs() + 2000
                                    Case 2
                                        TempEventMap(i).Events(x).MoveTimer = GetTimeMs() + 1000
                                    Case 3
                                        TempEventMap(i).Events(x).MoveTimer = GetTimeMs() + 500
                                    Case 4
                                        TempEventMap(i).Events(x).MoveTimer = GetTimeMs() + 250
                                End Select
                            End If
                        End If
                    Next
                End If
            End If
            Application.DoEvents()
        Next

    End Sub

    Friend Sub ProcessLocalEventMovement()
        Dim rand As Integer, x As Integer, i As Integer, playerID As Integer, eventID As Integer, WalkThrough As Integer
        Dim isglobal As Boolean, mapNum As Integer, actualmovespeed As Integer, Buffer As ByteStream, z As Integer, sendupdate As Boolean
        Dim donotprocessmoveroute As Boolean

        For i = 1 To GetPlayersOnline()

            If Gettingmap = True Then Exit Sub

            If IsPlaying(i) Then
                playerID = i
                If TempPlayer(i).EventMap.CurrentEvents > 0 Then
                    For x = 1 To TempPlayer(i).EventMap.CurrentEvents

                        If Gettingmap = True Then Exit Sub

                        If Map(GetPlayerMap(i)).Events(TempPlayer(i).EventMap.EventPages(x).EventId).Globals = 0 Then
                            If TempPlayer(i).EventMap.EventPages(x).Visible = 1 Then
                                If TempPlayer(i).EventMap.EventPages(x).MoveTimer <= GetTimeMs() Then
                                    'Real event! Lets process it!
                                    Select Case TempPlayer(i).EventMap.EventPages(x).MoveType
                                        Case 0
                                            'Nothing, fixed position
                                        Case 1 'Random, move randomly if possible...
                                            rand = Random(0, 3)
                                            playerID = i
                                            If CanEventMove(i, GetPlayerMap(i), TempPlayer(i).EventMap.EventPages(x).X, TempPlayer(i).EventMap.EventPages(x).Y, x, TempPlayer(i).EventMap.EventPages(x).WalkThrough, rand, False) Then
                                                Select Case TempPlayer(i).EventMap.EventPages(x).MoveSpeed
                                                    Case 0
                                                        EventMove(i, GetPlayerMap(i), x, rand, 2, False)
                                                    Case 1
                                                        EventMove(i, GetPlayerMap(i), x, rand, 3, False)
                                                    Case 2
                                                        EventMove(i, GetPlayerMap(i), x, rand, 4, False)
                                                    Case 3
                                                        EventMove(i, GetPlayerMap(i), x, rand, 6, False)
                                                    Case 4
                                                        EventMove(i, GetPlayerMap(i), x, rand, 12, False)
                                                    Case 5
                                                        EventMove(i, GetPlayerMap(i), x, rand, 24, False)
                                                End Select
                                            Else
                                                EventDir(0, GetPlayerMap(i), x, rand, True)
                                            End If
                                        Case 2 'Move Route
                                            With TempPlayer(i).EventMap.EventPages(x)
                                                isglobal = False
                                                sendupdate = False
                                                mapNum = GetPlayerMap(i)
                                                playerID = i
                                                eventID = x
                                                WalkThrough = .WalkThrough
                                                If TempPlayer(i).EventMap.EventPages(x).MoveRouteCount > 0 Then
                                                    If TempPlayer(i).EventMap.EventPages(x).MoveRouteStep >= TempPlayer(i).EventMap.EventPages(x).MoveRouteCount AndAlso TempPlayer(i).EventMap.EventPages(x).RepeatMoveRoute = 1 Then
                                                        .MoveRouteStep = 0
                                                        .MoveRouteComplete = 1
                                                    ElseIf .MoveRouteStep >= .MoveRouteCount AndAlso .RepeatMoveRoute = 0 Then
                                                        donotprocessmoveroute = True
                                                        .MoveRouteComplete = 1
                                                    Else
                                                        .MoveRouteComplete = 0
                                                    End If
                                                    If donotprocessmoveroute = False Then

                                                        Select Case TempPlayer(i).EventMap.EventPages(x).MoveSpeed
                                                            Case 0
                                                                actualmovespeed = 2
                                                            Case 1
                                                                actualmovespeed = 3
                                                            Case 2
                                                                actualmovespeed = 4
                                                            Case 3
                                                                actualmovespeed = 6
                                                            Case 4
                                                                actualmovespeed = 12
                                                            Case 5
                                                                actualmovespeed = 24
                                                        End Select
                                                        TempPlayer(i).EventMap.EventPages(x).MoveRouteStep = TempPlayer(i).EventMap.EventPages(x).MoveRouteStep + 1
                                                        Select Case TempPlayer(i).EventMap.EventPages(x).MoveRoute(TempPlayer(i).EventMap.EventPages(x).MoveRouteStep).Index
                                                            Case 1
                                                                If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, DirectionType.Up, isglobal) Then
                                                                    EventMove(playerID, mapNum, eventID, DirectionType.Up, actualmovespeed, isglobal)
                                                                Else
                                                                    If TempPlayer(i).EventMap.EventPages(x).IgnoreIfCannotMove = 0 Then
                                                                        .MoveRouteStep = .MoveRouteStep - 1
                                                                    End If
                                                                End If
                                                            Case 2
                                                                If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, DirectionType.Down, isglobal) Then
                                                                    EventMove(playerID, mapNum, eventID, DirectionType.Down, actualmovespeed, isglobal)
                                                                Else
                                                                    If .IgnoreIfCannotMove = 0 Then
                                                                        .MoveRouteStep = .MoveRouteStep - 1
                                                                    End If
                                                                End If
                                                            Case 3
                                                                If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, DirectionType.Left, isglobal) Then
                                                                    EventMove(playerID, mapNum, eventID, DirectionType.Left, actualmovespeed, isglobal)
                                                                Else
                                                                    If .IgnoreIfCannotMove = 0 Then
                                                                        .MoveRouteStep = .MoveRouteStep - 1
                                                                    End If
                                                                End If
                                                            Case 4
                                                                If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, DirectionType.Right, isglobal) Then
                                                                    EventMove(playerID, mapNum, eventID, DirectionType.Right, actualmovespeed, isglobal)
                                                                Else
                                                                    If .IgnoreIfCannotMove = 0 Then
                                                                        .MoveRouteStep = .MoveRouteStep - 1
                                                                    End If
                                                                End If
                                                            Case 5
                                                                z = Random(0, 3)
                                                                If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, z, isglobal) Then
                                                                    EventMove(playerID, mapNum, eventID, z, actualmovespeed, isglobal)
                                                                Else
                                                                    If .IgnoreIfCannotMove = 0 Then
                                                                        .MoveRouteStep = .MoveRouteStep - 1
                                                                    End If
                                                                End If
                                                            Case 6
                                                                If isglobal = False Then
                                                                    If IsOneBlockAway(.X, .Y, GetPlayerX(playerID), GetPlayerY(playerID)) = True Then
                                                                        EventDir(playerID, GetPlayerMap(playerID), eventID, GetDirToPlayer(playerID, GetPlayerMap(playerID), eventID), False)
                                                                        'Lets do cool stuff!
                                                                        If Map(GetPlayerMap(playerID)).Events(eventID).Pages(TempPlayer(playerID).EventMap.EventPages(eventID).PageId).Trigger = 1 Then
                                                                            If Map(mapNum).Events(eventID).Pages(TempPlayer(playerID).EventMap.EventPages(eventID).PageId).CommandListCount > 0 Then
                                                                                TempPlayer(playerID).EventProcessing(eventID).Active = 1
                                                                                TempPlayer(playerID).EventProcessing(eventID).ActionTimer = GetTimeMs()
                                                                                TempPlayer(playerID).EventProcessing(eventID).CurList = 1
                                                                                TempPlayer(playerID).EventProcessing(eventID).CurSlot = 1
                                                                                TempPlayer(playerID).EventProcessing(eventID).EventId = eventID
                                                                                TempPlayer(playerID).EventProcessing(eventID).PageId = TempPlayer(playerID).EventMap.EventPages(eventID).PageId
                                                                                TempPlayer(playerID).EventProcessing(eventID).WaitingForResponse = 0
                                                                                ReDim TempPlayer(playerID).EventProcessing(eventID).ListLeftOff(Map(GetPlayerMap(playerID)).Events(TempPlayer(playerID).EventMap.EventPages(eventID).EventId).Pages(TempPlayer(playerID).EventMap.EventPages(eventID).PageId).CommandListCount)
                                                                            End If
                                                                        End If
                                                                        If .IgnoreIfCannotMove = 0 Then
                                                                            .MoveRouteStep = .MoveRouteStep - 1
                                                                        End If
                                                                    Else
                                                                        z = CanEventMoveTowardsPlayer(playerID, mapNum, eventID)
                                                                        If z >= 4 Then
                                                                            'No
                                                                            If .IgnoreIfCannotMove = 0 Then
                                                                                .MoveRouteStep = .MoveRouteStep - 1
                                                                            End If
                                                                        Else
                                                                            'i is the direct, lets go...
                                                                            If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, z, isglobal) Then
                                                                                EventMove(playerID, mapNum, eventID, z, actualmovespeed, isglobal)
                                                                            Else
                                                                                If .IgnoreIfCannotMove = 0 Then
                                                                                    .MoveRouteStep = .MoveRouteStep - 1
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                            Case 7
                                                                If isglobal = False Then
                                                                    z = CanEventMoveAwayFromPlayer(playerID, mapNum, eventID)
                                                                    If z >= 5 Then
                                                                        'No
                                                                    Else
                                                                        'i is the direct, lets go...
                                                                        If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, z, isglobal) Then
                                                                            EventMove(playerID, mapNum, eventID, z, actualmovespeed, isglobal)
                                                                        Else
                                                                            If .IgnoreIfCannotMove = 0 Then
                                                                                .MoveRouteStep = .MoveRouteStep - 1
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                            Case 8
                                                                If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, .Dir, isglobal) Then
                                                                    EventMove(playerID, mapNum, eventID, .Dir, actualmovespeed, isglobal)
                                                                Else
                                                                    If .IgnoreIfCannotMove = 0 Then
                                                                        .MoveRouteStep = .MoveRouteStep - 1
                                                                    End If
                                                                End If
                                                            Case 9
                                                                Select Case .Dir
                                                                    Case DirectionType.Up
                                                                        z = DirectionType.Down
                                                                    Case DirectionType.Down
                                                                        z = DirectionType.Up
                                                                    Case DirectionType.Left
                                                                        z = DirectionType.Right
                                                                    Case DirectionType.Right
                                                                        z = DirectionType.Left
                                                                End Select
                                                                If CanEventMove(playerID, mapNum, .X, .Y, eventID, WalkThrough, z, isglobal) Then
                                                                    EventMove(playerID, mapNum, eventID, z, actualmovespeed, isglobal)
                                                                Else
                                                                    If .IgnoreIfCannotMove = 0 Then
                                                                        .MoveRouteStep = .MoveRouteStep - 1
                                                                    End If
                                                                End If
                                                            Case 10
                                                                .MoveTimer = GetTimeMs() + 100
                                                            Case 11
                                                                .MoveTimer = GetTimeMs() + 500
                                                            Case 12
                                                                .MoveTimer = GetTimeMs() + 1000
                                                            Case 13
                                                                EventDir(playerID, mapNum, eventID, DirectionType.Up, isglobal)
                                                            Case 14
                                                                EventDir(playerID, mapNum, eventID, DirectionType.Down, isglobal)
                                                            Case 15
                                                                EventDir(playerID, mapNum, eventID, DirectionType.Left, isglobal)
                                                            Case 16
                                                                EventDir(playerID, mapNum, eventID, DirectionType.Right, isglobal)
                                                            Case 17
                                                                Select Case .Dir
                                                                    Case DirectionType.Up
                                                                        z = DirectionType.Right
                                                                    Case DirectionType.Right
                                                                        z = DirectionType.Down
                                                                    Case DirectionType.Left
                                                                        z = DirectionType.Up
                                                                    Case DirectionType.Down
                                                                        z = DirectionType.Left
                                                                End Select
                                                                EventDir(playerID, mapNum, eventID, z, isglobal)
                                                            Case 18
                                                                Select Case .Dir
                                                                    Case DirectionType.Up
                                                                        z = DirectionType.Left
                                                                    Case DirectionType.Right
                                                                        z = DirectionType.Up
                                                                    Case DirectionType.Left
                                                                        z = DirectionType.Down
                                                                    Case DirectionType.Down
                                                                        z = DirectionType.Right
                                                                End Select
                                                                EventDir(playerID, mapNum, eventID, z, isglobal)
                                                            Case 19
                                                                Select Case .Dir
                                                                    Case DirectionType.Up
                                                                        z = DirectionType.Down
                                                                    Case DirectionType.Right
                                                                        z = DirectionType.Left
                                                                    Case DirectionType.Left
                                                                        z = DirectionType.Right
                                                                    Case DirectionType.Down
                                                                        z = DirectionType.Up
                                                                End Select
                                                                EventDir(playerID, mapNum, eventID, z, isglobal)
                                                            Case 20
                                                                z = Random(0, 3)
                                                                EventDir(playerID, mapNum, eventID, z, isglobal)
                                                            Case 21
                                                                If isglobal = False Then
                                                                    z = GetDirToPlayer(playerID, mapNum, eventID)
                                                                    EventDir(playerID, mapNum, eventID, z, isglobal)
                                                                End If
                                                            Case 22
                                                                If isglobal = False Then
                                                                    z = GetDirAwayFromPlayer(playerID, mapNum, eventID)
                                                                    EventDir(playerID, mapNum, eventID, z, isglobal)
                                                                End If
                                                            Case 23
                                                                .MoveSpeed = 0
                                                            Case 24
                                                                .MoveSpeed = 1
                                                            Case 25
                                                                .MoveSpeed = 2
                                                            Case 26
                                                                .MoveSpeed = 3
                                                            Case 27
                                                                .MoveSpeed = 4
                                                            Case 28
                                                                .MoveSpeed = 5
                                                            Case 29
                                                                .MoveFreq = 0
                                                            Case 30
                                                                .MoveFreq = 1
                                                            Case 31
                                                                .MoveFreq = 2
                                                            Case 32
                                                                .MoveFreq = 3
                                                            Case 33
                                                                .MoveFreq = 4
                                                            Case 34
                                                                .WalkingAnim = 1
                                                                'Need to send update to client
                                                                sendupdate = True
                                                            Case 35
                                                                .WalkingAnim = 0
                                                                'Need to send update to client
                                                                sendupdate = True
                                                            Case 36
                                                                .FixedDir = 1
                                                                'Need to send update to client
                                                                sendupdate = True
                                                            Case 37
                                                                .FixedDir = 0
                                                                'Need to send update to client
                                                                sendupdate = True
                                                            Case 38
                                                                .WalkThrough = 1
                                                            Case 39
                                                                .WalkThrough = 0
                                                            Case 40
                                                                .Position = 0
                                                                'Need to send update to client
                                                                sendupdate = True
                                                            Case 41
                                                                .Position = 1
                                                                'Need to send update to client
                                                                sendupdate = True
                                                            Case 42
                                                                .Position = 2
                                                                'Need to send update to client
                                                                sendupdate = True
                                                            Case 43
                                                                .GraphicType = .MoveRoute(.MoveRouteStep).Data1
                                                                .GraphicNum = .MoveRoute(.MoveRouteStep).Data2
                                                                .GraphicX = .MoveRoute(.MoveRouteStep).Data3
                                                                .GraphicX2 = .MoveRoute(.MoveRouteStep).Data4
                                                                .GraphicY = .MoveRoute(.MoveRouteStep).Data5
                                                                .GraphicY2 = .MoveRoute(.MoveRouteStep).Data6
                                                                If .GraphicType = 1 Then
                                                                    Select Case .GraphicY
                                                                        Case 0
                                                                            .Dir = DirectionType.Down
                                                                        Case 1
                                                                            .Dir = DirectionType.Left
                                                                        Case 2
                                                                            .Dir = DirectionType.Right
                                                                        Case 3
                                                                            .Dir = DirectionType.Up
                                                                    End Select
                                                                End If
                                                                'Need to Send Update to client
                                                                sendupdate = True
                                                        End Select

                                                        If sendupdate Then
                                                            Buffer = New ByteStream(4)
                                                            Buffer.WriteInt32(ServerPackets.SSpawnEvent)
                                                            Buffer.WriteInt32(TempPlayer(playerID).EventMap.EventPages(eventID).EventId)
                                                            With TempPlayer(playerID).EventMap.EventPages(eventID)
                                                                Buffer.WriteString((Trim(Map(GetPlayerMap(playerID)).Events(TempPlayer(playerID).EventMap.EventPages(eventID).EventId).Name)))
                                                                Buffer.WriteInt32(.Dir)
                                                                Buffer.WriteInt32(.GraphicNum)
                                                                Buffer.WriteInt32(.GraphicType)
                                                                Buffer.WriteInt32(.GraphicX)
                                                                Buffer.WriteInt32(.GraphicX2)
                                                                Buffer.WriteInt32(.GraphicY)
                                                                Buffer.WriteInt32(.GraphicY2)
                                                                Buffer.WriteInt32(.MoveSpeed)
                                                                Buffer.WriteInt32(.X)
                                                                Buffer.WriteInt32(.Y)
                                                                Buffer.WriteInt32(.Position)
                                                                Buffer.WriteInt32(.Visible)
                                                                Buffer.WriteInt32(.WalkingAnim)
                                                                Buffer.WriteInt32(.FixedDir)
                                                                Buffer.WriteInt32(.WalkThrough)
                                                                Buffer.WriteInt32(.ShowName)
                                                                Buffer.WriteInt32(.QuestNum)
                                                            End With
                                                            Socket.SendDataTo(playerID, Buffer.Data, Buffer.Head)
                                                            Buffer.Dispose()
                                                        End If
                                                    End If
                                                    donotprocessmoveroute = False
                                                End If
                                            End With
                                    End Select
                                    Select Case TempPlayer(playerID).EventMap.EventPages(x).MoveFreq
                                        Case 0
                                            TempPlayer(playerID).EventMap.EventPages(x).MoveTimer = GetTimeMs() + 4000
                                        Case 1
                                            TempPlayer(playerID).EventMap.EventPages(x).MoveTimer = GetTimeMs() + 2000
                                        Case 2
                                            TempPlayer(playerID).EventMap.EventPages(x).MoveTimer = GetTimeMs() + 1000
                                        Case 3
                                            TempPlayer(playerID).EventMap.EventPages(x).MoveTimer = GetTimeMs() + 500
                                        Case 4
                                            TempPlayer(playerID).EventMap.EventPages(x).MoveTimer = GetTimeMs() + 250
                                    End Select
                                End If
                            End If
                        End If
                    Next
                End If
            End If
            Application.DoEvents()
        Next

    End Sub

    Friend Sub ProcessEventCommands()
        Dim buffer As New ByteStream(4), i As Integer, x As Integer, removeEventProcess As Boolean, w As Integer, v As Integer, p As Integer
        Dim restartlist As Boolean, restartloop As Boolean, endprocess As Boolean

        'Now, we process the damn things for commands :P

        For i = 1 To GetPlayersOnline()
            If Gettingmap = True Then Exit Sub

            If IsPlaying(i) AndAlso TempPlayer(i).GettingMap = 0 Then
                For x = 1 To TempPlayer(i).EventMap.CurrentEvents
                    If TempPlayer(i).EventMap.EventPages(x).Visible Then
                        If Map(Player(i).Character(TempPlayer(i).CurChar).Map).Events(TempPlayer(i).EventMap.EventPages(x).EventId).Pages(TempPlayer(i).EventMap.EventPages(x).PageId).Trigger = 2 Then 'Parallel Process baby!
                            If TempPlayer(i).EventProcessingCount > 0 Then
                                If TempPlayer(i).EventProcessing(x).Active = 0 Then
                                    If Map(GetPlayerMap(i)).Events(TempPlayer(i).EventMap.EventPages(x).EventId).Pages(TempPlayer(i).EventMap.EventPages(x).PageId).CommandListCount > 0 Then
                                        'start new event processing
                                        TempPlayer(i).EventProcessing(TempPlayer(i).EventMap.EventPages(x).EventId).Active = 1
                                        With TempPlayer(i).EventProcessing(TempPlayer(i).EventMap.EventPages(x).EventId)
                                            .ActionTimer = GetTimeMs()
                                            .CurList = 1
                                            .CurSlot = 1
                                            .EventId = TempPlayer(i).EventMap.EventPages(x).EventId
                                            .PageId = TempPlayer(i).EventMap.EventPages(x).PageId
                                            .WaitingForResponse = 0
                                            ReDim .ListLeftOff(Map(GetPlayerMap(i)).Events(TempPlayer(i).EventMap.EventPages(x).EventId).Pages(TempPlayer(i).EventMap.EventPages(x).PageId).CommandListCount)
                                        End With
                                    End If
                                End If
                            Else
                                If Map(GetPlayerMap(i)).Events(TempPlayer(i).EventMap.EventPages(x).EventId).Pages(TempPlayer(i).EventMap.EventPages(x).PageId).CommandListCount > 0 Then
                                    'Clearly need to start it!
                                    TempPlayer(i).EventProcessing(TempPlayer(i).EventMap.EventPages(x).EventId).Active = 1
                                    With TempPlayer(i).EventProcessing(TempPlayer(i).EventMap.EventPages(x).EventId)
                                        .ActionTimer = GetTimeMs()
                                        .CurList = 1
                                        .CurSlot = 1
                                        .EventId = TempPlayer(i).EventMap.EventPages(x).EventId
                                        .PageId = TempPlayer(i).EventMap.EventPages(x).PageId
                                        .WaitingForResponse = 0
                                        ReDim .ListLeftOff(Map(GetPlayerMap(i)).Events(TempPlayer(i).EventMap.EventPages(x).EventId).Pages(TempPlayer(i).EventMap.EventPages(x).PageId).CommandListCount)
                                    End With
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Next

        'That is it for starting parallel processes :D now we just have to make the code that actually processes the events to their fullest
        For i = 1 To GetPlayersOnline()
            If Gettingmap = True Then Exit Sub

            If IsPlaying(i) AndAlso TempPlayer(i).GettingMap = 0 Then
                If TempPlayer(i).EventProcessingCount > 0 Then
                    restartloop = True
                    Do While restartloop = True
                        restartloop = False
                        For x = 1 To TempPlayer(i).EventProcessingCount
                            If TempPlayer(i).EventProcessing(x).Active = 1 Then
                                If x > TempPlayer(i).EventProcessingCount Then Exit For
                                With TempPlayer(i).EventProcessing(x)
                                    If TempPlayer(i).EventProcessingCount = 0 Then Exit Sub
                                    removeEventProcess = False
                                    If .WaitingForResponse = 2 Then
                                        If TempPlayer(i).InShop = 0 Then
                                            .WaitingForResponse = 0
                                        End If
                                    End If
                                    If .WaitingForResponse = 3 Then
                                        If TempPlayer(i).InBank = False Then
                                            .WaitingForResponse = 0
                                        End If
                                    End If
                                    If .WaitingForResponse = 4 Then
                                        'waiting for eventmovement to complete
                                        If .EventMovingType = 0 Then
                                            If TempPlayer(i).EventMap.EventPages(.EventMovingId).MoveRouteComplete = 1 Then
                                                .WaitingForResponse = 0
                                            End If
                                        Else
                                            If TempEventMap(GetPlayerMap(i)).Events(.EventMovingId).MoveRouteComplete = 1 Then
                                                .WaitingForResponse = 0
                                            End If
                                        End If
                                    End If
                                    If .WaitingForResponse = 0 Then
                                        If .ActionTimer <= GetTimeMs() Then
                                            restartlist = True
                                            endprocess = False
                                            Do While restartlist = True AndAlso endprocess = False AndAlso .WaitingForResponse = 0
                                                restartlist = False
                                                If .ListLeftOff(.CurList) > 0 Then
                                                    .CurSlot = .ListLeftOff(.CurList) + 1
                                                    .ListLeftOff(.CurList) = 0
                                                End If
                                                If .CurList > Map(Player(i).Character(TempPlayer(i).CurChar).Map).Events(.EventId).Pages(.PageId).CommandListCount Then
                                                    'Get rid of this event, it is bad
                                                    removeEventProcess = True
                                                    endprocess = True
                                                End If
                                                If .CurSlot > Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).CommandCount Then
                                                    If .CurList = 1 Then
                                                        'Get rid of this event, it is bad
                                                        removeEventProcess = True
                                                        endprocess = True
                                                    Else
                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).ParentList
                                                        .CurSlot = 1
                                                        restartlist = True
                                                    End If
                                                End If
                                                If restartlist = False AndAlso endprocess = False Then
                                                    'If we are still here, then we are good to process shit :D
                                                    'Debug.WriteLine(.CurSlot)
                                                    Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Index
                                                        Case EventType.EvAddText
                                                            Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2
                                                                Case 0
                                                                    PlayerMsg(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                                Case 1
                                                                    MapMsg(GetPlayerMap(i), Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                                Case 2
                                                                    GlobalMsg(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1) ' Map(GetPlayerMap(i)).Events(.EventID).Pages(.PageID).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                            End Select
                                                        Case EventType.EvShowText
                                                            buffer = New ByteStream(4)
                                                            buffer.WriteInt32(ServerPackets.SEventChat)
                                                            buffer.WriteInt32(.EventId)
                                                            buffer.WriteInt32(.PageId)
                                                            buffer.WriteInt32(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                            buffer.WriteString((Trim(ParseEventText(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1))))
                                                            buffer.WriteInt32(0)

                                                            AddDebug("Sent SMSG: SEventChat evShowText")

                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).CommandCount > .CurSlot Then
                                                                If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot + 1).Index = EventType.EvShowText OrElse Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot + 1).Index = EventType.EvShowChoices Then
                                                                    buffer.WriteInt32(1)
                                                                ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot + 1).Index = EventType.EvCondition Then
                                                                    buffer.WriteInt32(2)
                                                                Else
                                                                    buffer.WriteInt32(0)
                                                                End If
                                                            Else
                                                                buffer.WriteInt32(2)
                                                            End If
                                                            Socket.SendDataTo(i, buffer.Data, buffer.Head)
                                                            buffer.Dispose()
                                                            .WaitingForResponse = 1
                                                        Case EventType.EvShowChoices
                                                            buffer = New ByteStream(4)
                                                            buffer.WriteInt32(ServerPackets.SEventChat)
                                                            buffer.WriteInt32(.EventId)
                                                            buffer.WriteInt32(.PageId)
                                                            buffer.WriteInt32(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data5)
                                                            buffer.WriteString((Trim(ParseEventText(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1))))

                                                            AddDebug("Sent SMSG: SEventChat evShowChoices")

                                                            If Len(Trim$(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text2)) > 0 Then
                                                                w = 1
                                                                If Len(Trim$(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text3)) > 0 Then
                                                                    w = 2
                                                                    If Len(Trim$(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text4)) > 0 Then
                                                                        w = 3
                                                                        If Len(Trim$(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text5)) > 0 Then
                                                                            w = 4
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                            buffer.WriteInt32(w)
                                                            For v = 1 To w
                                                                Select Case v
                                                                    Case 1
                                                                        buffer.WriteString((Trim(ParseEventText(i, Trim$(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text2)))))
                                                                    Case 2
                                                                        buffer.WriteString((Trim(ParseEventText(i, Trim$(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text3)))))
                                                                    Case 3
                                                                        buffer.WriteString((Trim(ParseEventText(i, Trim$(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text4)))))
                                                                    Case 4
                                                                        buffer.WriteString((Trim(ParseEventText(i, Trim$(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text5)))))
                                                                End Select
                                                            Next
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).CommandCount > .CurSlot Then
                                                                If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot + 1).Index = EventType.EvShowText OrElse Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot + 1).Index = EventType.EvShowChoices Then
                                                                    buffer.WriteInt32(1)
                                                                ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot + 1).Index = EventType.EvCondition Then
                                                                    buffer.WriteInt32(2)
                                                                Else
                                                                    buffer.WriteInt32(0)
                                                                End If
                                                            Else
                                                                buffer.WriteInt32(2)
                                                            End If
                                                            Socket.SendDataTo(i, buffer.Data, buffer.Head)
                                                            buffer.Dispose()
                                                            .WaitingForResponse = 1
                                                        Case EventType.EvPlayerVar
                                                            Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2
                                                                Case 0
                                                                    Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3
                                                                Case 1
                                                                    Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) = Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) + Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3
                                                                Case 2
                                                                    Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) = Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) - Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3
                                                                Case 3
                                                                    Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) = Random(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data4)
                                                            End Select
                                                        Case EventType.EvPlayerSwitch
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 0 Then
                                                                Player(i).Character(TempPlayer(i).CurChar).Switches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) = 1
                                                            ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 1 Then
                                                                Player(i).Character(TempPlayer(i).CurChar).Switches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) = 0
                                                            End If
                                                        Case EventType.EvSelfSwitch
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Globals = 1 Then
                                                                If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 0 Then
                                                                    Map(GetPlayerMap(i)).Events(.EventId).SelfSwitches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 + 1) = 1
                                                                ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 1 Then
                                                                    Map(GetPlayerMap(i)).Events(.EventId).SelfSwitches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 + 1) = 0
                                                                End If
                                                            Else
                                                                If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 0 Then
                                                                    TempPlayer(i).EventMap.EventPages(.EventId).SelfSwitches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 + 1) = 1
                                                                ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 1 Then
                                                                    TempPlayer(i).EventMap.EventPages(.EventId).SelfSwitches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 + 1) = 0
                                                                End If
                                                            End If
                                                        Case EventType.EvCondition
                                                            Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Condition
                                                                Case 0
                                                                    Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data2
                                                                        Case 0
                                                                            If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data3 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 1
                                                                            If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) >= Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data3 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 2
                                                                            If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) <= Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data3 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 3
                                                                            If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) > Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data3 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 4
                                                                            If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) < Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data3 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 5
                                                                            If Player(i).Character(TempPlayer(i).CurChar).Variables(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) <> Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data3 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                    End Select
                                                                Case 1
                                                                    Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data2
                                                                        Case 0
                                                                            If Player(i).Character(TempPlayer(i).CurChar).Switches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) = 1 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 1
                                                                            If Player(i).Character(TempPlayer(i).CurChar).Switches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) = 0 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                    End Select
                                                                Case 2
                                                                    If HasItem(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) >= Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data2 Then
                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                        .CurSlot = 1
                                                                    Else
                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                        .CurSlot = 1
                                                                    End If
                                                                Case 3
                                                                    If Player(i).Character(TempPlayer(i).CurChar).Classes = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 Then
                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                        .CurSlot = 1
                                                                    Else
                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                        .CurSlot = 1
                                                                    End If
                                                                Case 4
                                                                    If HasSkill(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) = True Then
                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                        .CurSlot = 1
                                                                    Else
                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                        .CurSlot = 1
                                                                    End If
                                                                Case 5
                                                                    Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data2
                                                                        Case 0
                                                                            If GetPlayerLevel(i) = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 1
                                                                            If GetPlayerLevel(i) >= Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 2
                                                                            If GetPlayerLevel(i) <= Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 3
                                                                            If GetPlayerLevel(i) > Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 4
                                                                            If GetPlayerLevel(i) < Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        Case 5
                                                                            If GetPlayerLevel(i) <> Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                    End Select
                                                                Case 6
                                                                    If Map(GetPlayerMap(i)).Events(.EventId).Globals = 1 Then
                                                                        Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data2
                                                                            Case 0 'Self Switch is true
                                                                                If Map(GetPlayerMap(i)).Events(.EventId).SelfSwitches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 + 1) = 1 Then
                                                                                    .ListLeftOff(.CurList) = .CurSlot
                                                                                    .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                    .CurSlot = 1
                                                                                Else
                                                                                    .ListLeftOff(.CurList) = .CurSlot
                                                                                    .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                    .CurSlot = 1
                                                                                End If
                                                                            Case 1  'self switch is false
                                                                                If Map(GetPlayerMap(i)).Events(.EventId).SelfSwitches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 + 1) = 0 Then
                                                                                    .ListLeftOff(.CurList) = .CurSlot
                                                                                    .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                    .CurSlot = 1
                                                                                Else
                                                                                    .ListLeftOff(.CurList) = .CurSlot
                                                                                    .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                    .CurSlot = 1
                                                                                End If
                                                                        End Select
                                                                    Else
                                                                        Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data2
                                                                            Case 0 'Self Switch is true
                                                                                If TempPlayer(i).EventMap.EventPages(.EventId).SelfSwitches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 + 1) = 1 Then
                                                                                    .ListLeftOff(.CurList) = .CurSlot
                                                                                    .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                    .CurSlot = 1
                                                                                Else
                                                                                    .ListLeftOff(.CurList) = .CurSlot
                                                                                    .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                    .CurSlot = 1
                                                                                End If
                                                                            Case 1  'self switch is false
                                                                                If TempPlayer(i).EventMap.EventPages(.EventId).SelfSwitches(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 + 1) = 0 Then
                                                                                    .ListLeftOff(.CurList) = .CurSlot
                                                                                    .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                    .CurSlot = 1
                                                                                Else
                                                                                    .ListLeftOff(.CurList) = .CurSlot
                                                                                    .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                    .CurSlot = 1
                                                                                End If
                                                                        End Select
                                                                    End If
                                                                Case 7
                                                                    If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 > 0 AndAlso Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 <= MAX_QUESTS Then
                                                                        If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data2 = 0 Then
                                                                            Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data3
                                                                                Case 0
                                                                                    If Player(i).Character(TempPlayer(i).CurChar).PlayerQuest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1).Status = 0 Then
                                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                        .CurSlot = 1
                                                                                    Else
                                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                        .CurSlot = 1
                                                                                    End If
                                                                                Case 1
                                                                                    If Player(i).Character(TempPlayer(i).CurChar).PlayerQuest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1).Status = 1 Then
                                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                        .CurSlot = 1
                                                                                    Else
                                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                        .CurSlot = 1
                                                                                    End If
                                                                                Case 2
                                                                                    If Player(i).Character(TempPlayer(i).CurChar).PlayerQuest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1).Status = 2 OrElse Player(i).Character(TempPlayer(i).CurChar).PlayerQuest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1).Status = 3 Then
                                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                        .CurSlot = 1
                                                                                    Else
                                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                        .CurSlot = 1
                                                                                    End If
                                                                                Case 3
                                                                                    If CanStartQuest(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) Then
                                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                        .CurSlot = 1
                                                                                    Else
                                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                        .CurSlot = 1
                                                                                    End If
                                                                                Case 4
                                                                                    If CanEndQuest(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1) Then
                                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                        .CurSlot = 1
                                                                                    Else
                                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                        .CurSlot = 1
                                                                                    End If
                                                                            End Select
                                                                        ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data2 = 1 Then
                                                                            If Player(i).Character(TempPlayer(i).CurChar).PlayerQuest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1).ActualTask = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data3 Then
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                                .CurSlot = 1
                                                                            Else
                                                                                .ListLeftOff(.CurList) = .CurSlot
                                                                                .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                                .CurSlot = 1
                                                                            End If
                                                                        End If
                                                                    End If
                                                                Case 8
                                                                    If Player(i).Character(TempPlayer(i).CurChar).Sex = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 Then
                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                        .CurSlot = 1
                                                                    Else
                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                        .CurSlot = 1
                                                                    End If
                                                                Case 9
                                                                    If Time.Instance.TimeOfDay = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.Data1 Then
                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.CommandList
                                                                        .CurSlot = 1
                                                                    Else
                                                                        .ListLeftOff(.CurList) = .CurSlot
                                                                        .CurList = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).ConditionalBranch.ElseCommandList
                                                                        .CurSlot = 1
                                                                    End If
                                                            End Select
                                                            endprocess = True
                                                        Case EventType.EvExitProcess
                                                            removeEventProcess = True
                                                            endprocess = True
                                                        Case EventType.EvChangeItems
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 0 Then
                                                                If HasItem(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) > 0 Then
                                                                    SetPlayerInvItemValue(i, FindItemSlot(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1), Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3)
                                                                    CheckTasks(i, QuestType.Fetch, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                                    CheckTasks(i, QuestType.Give, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                                End If
                                                            ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 1 Then
                                                                GiveInvItem(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3, True)
                                                                CheckTasks(i, QuestType.Fetch, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                                CheckTasks(i, QuestType.Give, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                            ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 2 Then
                                                                Dim itemAmount As Integer
                                                                itemAmount = HasItem(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                                ' Check Amount
                                                                If itemAmount >= Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3 Then
                                                                    TakeInvItem(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3)
                                                                    CheckTasks(i, QuestType.Fetch, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                                    CheckTasks(i, QuestType.Give, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                                End If
                                                            End If
                                                            SendInventory(i)
                                                        Case EventType.EvRestoreHp
                                                            SetPlayerVital(i, VitalType.HP, GetPlayerMaxVital(i, VitalType.HP))
                                                            SendVital(i, VitalType.HP)
                                                        Case EventType.EvRestoreMp
                                                            SetPlayerVital(i, VitalType.MP, GetPlayerMaxVital(i, VitalType.MP))
                                                            SendVital(i, VitalType.MP)
                                                        Case EventType.EvLevelUp
                                                            SetPlayerExp(i, GetPlayerNextLevel(i))
                                                            CheckPlayerLevelUp(i)
                                                            SendExp(i)
                                                            SendPlayerData(i)
                                                        Case EventType.EvChangeLevel
                                                            SetPlayerLevel(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                            SetPlayerExp(i, 0)
                                                            SendExp(i)
                                                            SendPlayerData(i)
                                                        Case EventType.EvChangeSkills
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 0 Then
                                                                If FindOpenSkillSlot(i) > 0 Then
                                                                    If HasSkill(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) = False Then
                                                                        SetPlayerSkill(i, FindOpenSkillSlot(i), Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                                    Else
                                                                        'Error, already knows skill
                                                                    End If
                                                                Else
                                                                    'Error, no room for skills
                                                                End If
                                                            ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 1 Then
                                                                If HasSkill(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) = True Then
                                                                    For p = 1 To MAX_PLAYER_SKILLS
                                                                        If Player(i).Character(TempPlayer(i).CurChar).Skill(p) = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 Then
                                                                            SetPlayerSkill(i, p, 0)
                                                                        End If
                                                                    Next
                                                                End If
                                                            End If
                                                            SendPlayerSkills(i)
                                                        Case EventType.EvChangeClass
                                                            Player(i).Character(TempPlayer(i).CurChar).Classes = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1
                                                            SendPlayerData(i)
                                                        Case EventType.EvChangeSprite
                                                            SetPlayerSprite(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                            SendPlayerData(i)
                                                        Case EventType.EvChangeSex
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 = 0 Then
                                                                Player(i).Character(TempPlayer(i).CurChar).Sex = SexType.Male
                                                            ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 = 1 Then
                                                                Player(i).Character(TempPlayer(i).CurChar).Sex = SexType.Female
                                                            End If
                                                            SendPlayerData(i)
                                                        Case EventType.EvChangePk
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 = 0 Then
                                                                Player(i).Character(TempPlayer(i).CurChar).Pk = False
                                                            ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 = 1 Then
                                                                Player(i).Character(TempPlayer(i).CurChar).Pk = True
                                                            End If
                                                            SendPlayerData(i)
                                                        Case EventType.EvWarpPlayer
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data4 = 0 Then
                                                                PlayerWarp(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3)
                                                            Else
                                                                Player(i).Character(TempPlayer(i).CurChar).Dir = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data4 - 1
                                                                PlayerWarp(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3)
                                                            End If

                                                        Case EventType.EvSetMoveRoute
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 <= Map(GetPlayerMap(i)).EventCount Then
                                                                If Map(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).Globals = 1 Then
                                                                    TempEventMap(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).MoveType = 2
                                                                    TempEventMap(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).IgnoreIfCannotMove = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2
                                                                    TempEventMap(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).RepeatMoveRoute = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3
                                                                    TempEventMap(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).MoveRouteCount = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).MoveRouteCount
                                                                    TempEventMap(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).MoveRoute = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).MoveRoute
                                                                    TempEventMap(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).MoveRouteStep = 0
                                                                    TempEventMap(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).MoveRouteComplete = 0
                                                                Else
                                                                    TempPlayer(i).EventMap.EventPages(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).MoveType = 2
                                                                    TempPlayer(i).EventMap.EventPages(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).IgnoreIfCannotMove = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2
                                                                    TempPlayer(i).EventMap.EventPages(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).RepeatMoveRoute = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3
                                                                    TempPlayer(i).EventMap.EventPages(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).MoveRouteCount = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).MoveRouteCount
                                                                    TempPlayer(i).EventMap.EventPages(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).MoveRoute = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).MoveRoute
                                                                    TempPlayer(i).EventMap.EventPages(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).MoveRouteStep = 0
                                                                    TempPlayer(i).EventMap.EventPages(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).MoveRouteComplete = 0
                                                                End If
                                                            End If
                                                        Case EventType.EvPlayAnimation
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 0 Then
                                                                SendAnimation(GetPlayerMap(i), Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, GetPlayerX(i), GetPlayerY(i), TargetType.Player, i)
                                                            ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 1 Then
                                                                If Map(GetPlayerMap(i)).Events(.EventId).Globals = 1 Then
                                                                    SendAnimation(GetPlayerMap(i), Map(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).X, Map(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3).Y)
                                                                Else
                                                                    SendAnimation(GetPlayerMap(i), Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, TempPlayer(i).EventMap.EventPages(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3).X, TempPlayer(i).EventMap.EventPages(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3).Y, 0, 0)
                                                                End If
                                                            ElseIf Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 = 2 Then
                                                                SendAnimation(GetPlayerMap(i), Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data4, 0, 0)
                                                            End If
                                                        Case EventType.EvCustomScript
                                                            'Runs Through Cases for a script
                                                            CustomScript(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, GetPlayerMap(i), .EventId)
                                                        Case EventType.EvPlayBgm
                                                            buffer = New ByteStream(4)
                                                            buffer.WriteInt32(ServerPackets.SPlayBGM)
                                                            buffer.WriteString((Trim(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1)))
                                                            Socket.SendDataTo(i, buffer.Data, buffer.Head)

                                                            Addlog("Sent SMSG: SPlayBGM", PACKET_LOG)
                                                            Console.WriteLine("Sent SMSG: SPlayBGM")

                                                            buffer.Dispose()
                                                        Case EventType.EvFadeoutBgm
                                                            buffer = New ByteStream(4)
                                                            buffer.WriteInt32(ServerPackets.SFadeoutBGM)
                                                            Socket.SendDataTo(i, buffer.Data, buffer.Head)

                                                            Addlog("Sent SMSG: SFadeoutBGM", PACKET_LOG)
                                                            Console.WriteLine("Sent SMSG: SFadeoutBGM")

                                                            buffer.Dispose()
                                                        Case EventType.EvPlaySound
                                                            buffer = New ByteStream(4)
                                                            buffer.WriteInt32(ServerPackets.SPlaySound)
                                                            buffer.WriteString((Trim(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1)))
                                                            Socket.SendDataTo(i, buffer.Data, buffer.Head)

                                                            Addlog("Sent SMSG: SPlaySound", PACKET_LOG)
                                                            Console.WriteLine("Sent SMSG: SPlaySound")

                                                            buffer.Dispose()
                                                        Case EventType.EvStopSound
                                                            buffer = New ByteStream(4)
                                                            buffer.WriteInt32(ServerPackets.SStopSound)
                                                            Socket.SendDataTo(i, buffer.Data, buffer.Head)

                                                            Addlog("Sent SMSG: SStopSound", PACKET_LOG)
                                                            Console.WriteLine("Sent SMSG: SStopSound")

                                                            buffer.Dispose()
                                                        Case EventType.EvSetAccess
                                                            Player(i).Access = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1
                                                            SendPlayerData(i)
                                                        Case EventType.EvOpenShop
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 > 0 Then ' shop exists?
                                                                If Len(Trim$(Shop(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).Name)) > 0 Then ' name exists?
                                                                    SendOpenShop(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                                    TempPlayer(i).InShop = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 ' stops movement and the like
                                                                    .WaitingForResponse = 2
                                                                End If
                                                            End If
                                                        Case EventType.EvOpenBank
                                                            SendBank(i)
                                                            TempPlayer(i).InBank = True
                                                            .WaitingForResponse = 3
                                                        Case EventType.EvGiveExp
                                                            GivePlayerExp(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                        Case EventType.EvShowChatBubble
                                                            Select Case Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1
                                                                Case TargetType.Player
                                                                    SendChatBubble(GetPlayerMap(i), i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1, ColorType.Brown)
                                                                Case TargetType.Npc
                                                                    SendChatBubble(GetPlayerMap(i), Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1, ColorType.Brown)
                                                                Case TargetType.Event
                                                                    SendChatBubble(GetPlayerMap(i), Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1, ColorType.Brown)
                                                            End Select
                                                        Case EventType.EvLabel
                                                            'Do nothing, just a label
                                                        Case EventType.EvGotoLabel
                                                            'Find the label's list of commands and slot
                                                            FindEventLabel(Trim$(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Text1), GetPlayerMap(i), .EventId, .PageId, .CurSlot, .CurList, .ListLeftOff)
                                                        Case EventType.EvSpawnNpc
                                                            If Map(GetPlayerMap(i)).Npc(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) > 0 Then
                                                                SpawnNpc(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, GetPlayerMap(i))
                                                            End If
                                                        Case EventType.EvFadeIn
                                                            SendSpecialEffect(i, EffectTypeFadein)
                                                        Case EventType.EvFadeOut
                                                            SendSpecialEffect(i, EffectTypeFadeout)
                                                        Case EventType.EvFlashWhite
                                                            SendSpecialEffect(i, EffectTypeFlash)
                                                        Case EventType.EvSetFog
                                                            SendSpecialEffect(i, EffectTypeFog, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3)
                                                        Case EventType.EvSetWeather
                                                            SendSpecialEffect(i, EffectTypeWeather, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2)
                                                        Case EventType.EvSetTint
                                                            SendSpecialEffect(i, EffectTypeTint, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data4)
                                                        Case EventType.EvWait
                                                            .ActionTimer = GetTimeMs() + Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1
                                                        Case EventType.EvOpenMail
                                                            'SendMailBox(i)
                                                        Case EventType.EvBeginQuest
                                                            If CanStartQuest(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) Then
                                                                QuestMessage(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Quest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).Chat(1), Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                            End If
                                                        Case EventType.EvEndQuest
                                                            If CanEndQuest(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) Then
                                                                EndQuest(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1)
                                                            End If
                                                        Case EventType.EvQuestTask
                                                            If QuestInProgress(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1) Then
                                                                If Player(i).Character(TempPlayer(i).CurChar).PlayerQuest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).ActualTask = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2 Then
                                                                    If Quest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).Task(Player(i).Character(TempPlayer(i).CurChar).PlayerQuest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).ActualTask).TaskType = QuestType.TalkEvent OrElse Quest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).Task(Player(i).Character(TempPlayer(i).CurChar).PlayerQuest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).ActualTask).TaskType = QuestType.Fetch Then
                                                                        CheckTask(i, Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1, Quest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).Task(Player(i).Character(TempPlayer(i).CurChar).PlayerQuest(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).ActualTask).TaskType, -1)
                                                                    End If
                                                                End If
                                                            End If
                                                        Case EventType.EvShowPicture
                                                            buffer = New ByteStream(4)
                                                            buffer.WriteInt32(ServerPackets.SPic)
                                                            buffer.WriteInt32(0)
                                                            buffer.WriteInt32(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 + 1)
                                                            buffer.WriteInt32(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data2)
                                                            buffer.WriteInt32(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data3)
                                                            buffer.WriteInt32(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data4)
                                                            buffer.WriteInt32(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data5)
                                                            Socket.SendDataTo(i, buffer.Data, buffer.Head)

                                                            Addlog("Sent SMSG: SPic evShowPicture", PACKET_LOG)
                                                            Console.WriteLine("Sent SMSG: SPic evShowPicture")

                                                            buffer.Dispose()
                                                        Case EventType.EvHidePicture
                                                            buffer = New ByteStream(4)
                                                            buffer.WriteInt32(ServerPackets.SPic)
                                                            buffer.WriteInt32(1)
                                                            buffer.WriteInt32(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 + 1)
                                                            Socket.SendDataTo(i, buffer.Data, buffer.Head)

                                                            Addlog("Sent SMSG: SPic evHidePicture", PACKET_LOG)
                                                            Console.WriteLine("Sent SMSG: SPic evHidePicture")

                                                            buffer.Dispose()
                                                        Case EventType.EvWaitMovement
                                                            If Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1 <= Map(GetPlayerMap(i)).EventCount Then
                                                                If Map(GetPlayerMap(i)).Events(Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1).Globals = 1 Then
                                                                    .WaitingForResponse = 4
                                                                    .EventMovingId = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1
                                                                    .EventMovingType = 1
                                                                Else
                                                                    .WaitingForResponse = 4
                                                                    .EventMovingId = Map(GetPlayerMap(i)).Events(.EventId).Pages(.PageId).CommandList(.CurList).Commands(.CurSlot).Data1
                                                                    .EventMovingType = 0
                                                                End If
                                                            End If
                                                        Case EventType.EvHoldPlayer
                                                            buffer = New ByteStream(4)
                                                            buffer.WriteInt32(ServerPackets.SHoldPlayer)
                                                            buffer.WriteInt32(0)
                                                            Socket.SendDataTo(i, buffer.Data, buffer.Head)

                                                            Addlog("Sent SMSG: SHoldPlayer", PACKET_LOG)
                                                            Console.WriteLine("Sent SMSG: SHoldPlayer")

                                                            buffer.Dispose()
                                                        Case EventType.EvReleasePlayer
                                                            buffer = New ByteStream(4)
                                                            buffer.WriteInt32(ServerPackets.SHoldPlayer)
                                                            buffer.WriteInt32(1)
                                                            Socket.SendDataTo(i, buffer.Data, buffer.Head)

                                                            Addlog("Sent SMSG: SHoldPlayer Release", PACKET_LOG)
                                                            Console.WriteLine("Sent SMSG: SHoldPlayer Release")

                                                            buffer.Dispose()
                                                    End Select
                                                End If
                                            Loop
                                            If endprocess = False Then
                                                .CurSlot = .CurSlot + 1
                                            End If
                                        End If
                                    End If
                                End With
                            End If
                            If removeEventProcess = True Then
                                TempPlayer(i).EventProcessing(x).Active = 0
                                restartloop = True
                                removeEventProcess = False
                            End If
                        Next
                    Loop
                End If
            End If
        Next

    End Sub

    Friend Sub UpdateEventLogic()
        'Check Removing and Adding of Events (Did switches change or something?)

        If Gettingmap = True Then Exit Sub

        RemoveDeadEvents()

        If Gettingmap = True Then Exit Sub

        SpawnNewEvents()

        If Gettingmap = True Then Exit Sub

        ProcessEventMovement()

        If Gettingmap = True Then Exit Sub

        ProcessLocalEventMovement()

        If Gettingmap = True Then Exit Sub

        ProcessEventCommands()

    End Sub

    Function ParseEventText(index As Integer, txt As String) As String
        Dim i As Integer, x As Integer, newtxt As String, parsestring As String, z As Integer

        txt = Replace(txt, "/name", Trim$(Player(index).Character(TempPlayer(index).CurChar).Name))
        txt = Replace(txt, "/p", Trim$(Player(index).Character(TempPlayer(index).CurChar).Name))
        txt = Replace(txt, "$playername$", Trim$(Player(index).Character(TempPlayer(index).CurChar).Name))
        txt = Replace(txt, "$playerclass$", Trim$(Classes(Player(index).Character(TempPlayer(index).CurChar).Classes).Name))
        Do While InStr(1, txt, "/v") > 0
            x = InStr(1, txt, "/v")
            If x > 0 Then
                i = 0
                Do Until IsNumeric(Mid(txt, x + 2 + i, 1)) = False
                    i = i + 1
                Loop
                newtxt = Mid(txt, 1, x - 1)
                parsestring = Mid(txt, x + 2, i)
                z = Player(index).Character(TempPlayer(i).CurChar).Variables(Val(parsestring))
                newtxt = newtxt & CStr(z)
                newtxt = newtxt & Mid(txt, x + 2 + i, Len(txt) - (x + i))
                txt = newtxt
            End If
        Loop
        ParseEventText = txt

    End Function

    Sub FindEventLabel(Label As String, mapNum As Integer, eventID As Integer, pageID As Integer, ByRef CurSlot As Integer, ByRef CurList As Integer, ByRef ListLeftOff() As Integer)
        Dim tmpCurSlot As Integer, tmpCurList As Integer, CurrentListOption() As Integer
        Dim removeEventProcess As Boolean, tmpListLeftOff() As Integer, restartlist As Boolean, w As Integer

        'Store the Old data, just in case

        tmpCurSlot = CurSlot
        tmpCurList = CurList
        tmpListLeftOff = ListLeftOff

        ReDim ListLeftOff(Map(mapNum).Events(eventID).Pages(pageID).CommandListCount)
        ReDim CurrentListOption(Map(mapNum).Events(eventID).Pages(pageID).CommandListCount)
        CurList = 1
        CurSlot = 1

        Do Until removeEventProcess = True
            If ListLeftOff(CurList) > 0 Then
                CurSlot = ListLeftOff(CurList)
                ListLeftOff(CurList) = 0
            End If
            If CurList > Map(mapNum).Events(eventID).Pages(pageID).CommandListCount Then
                'Get rid of this event, it is bad
                removeEventProcess = True
            End If

            If CurSlot > Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).CommandCount Then
                If CurList = 1 Then
                    removeEventProcess = True
                Else
                    CurList = Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).ParentList
                    CurSlot = 1
                    restartlist = True
                End If
            End If

            If restartlist = False Then
                If removeEventProcess = False Then
                    'If we are still here, then we are good to process shit :D
                    Select Case Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).Index
                        Case EventType.EvShowChoices
                            If Len(Trim$(Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).Text2)) > 0 Then
                                w = 1
                                If Len(Trim$(Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).Text3)) > 0 Then
                                    w = 2
                                    If Len(Trim$(Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).Text4)) > 0 Then
                                        w = 3
                                        If Len(Trim$(Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).Text5)) > 0 Then
                                            w = 4
                                        End If
                                    End If
                                End If
                            End If
                            If w > 0 Then
                                If CurrentListOption(CurList) < w Then
                                    CurrentListOption(CurList) = CurrentListOption(CurList) + 1
                                    'Process
                                    ListLeftOff(CurList) = CurSlot
                                    Select Case CurrentListOption(CurList)
                                        Case 1
                                            CurList = Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).Data1
                                        Case 2
                                            CurList = Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).Data2
                                        Case 3
                                            CurList = Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).Data3
                                        Case 4
                                            CurList = Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).Data4
                                    End Select
                                    CurSlot = 0
                                Else
                                    CurrentListOption(CurList) = 0
                                    'continue on
                                End If
                            End If
                            w = 0
                        Case EventType.EvCondition
                            If CurrentListOption(CurList) = 0 Then
                                CurrentListOption(CurList) = 1
                                ListLeftOff(CurList) = CurSlot
                                CurList = Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).ConditionalBranch.CommandList
                                CurSlot = 0
                            ElseIf CurrentListOption(CurList) = 1 Then
                                CurrentListOption(CurList) = 2
                                ListLeftOff(CurList) = CurSlot
                                CurList = Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).ConditionalBranch.ElseCommandList
                                CurSlot = 0
                            ElseIf CurrentListOption(CurList) = 2 Then
                                CurrentListOption(CurList) = 0
                            End If
                        Case EventType.EvLabel
                            'Do nothing, just a label
                            If Trim$(Map(mapNum).Events(eventID).Pages(pageID).CommandList(CurList).Commands(CurSlot).Text1) = Trim$(Label) Then
                                Exit Sub
                            End If
                    End Select
                    CurSlot = CurSlot + 1
                End If
            End If
            restartlist = False
        Loop

        ListLeftOff = tmpListLeftOff
        CurList = tmpCurList
        CurSlot = tmpCurSlot

    End Sub

    Function FindNpcPath(mapNum As Integer, mapnpcnum As Integer, targetx As Integer, targety As Integer) As Integer
        Dim tim As Integer, sX As Integer, sY As Integer, pos(,) As Integer, reachable As Boolean, j As Integer, LastSum As Integer, Sum As Integer, FX As Integer, FY As Integer, i As Integer
        Dim path() As Point, LastX As Integer, LastY As Integer, did As Boolean

        'Initialization phase

        tim = 0

        sX = MapNpc(mapNum).Npc(mapnpcnum).X
        sY = MapNpc(mapNum).Npc(mapnpcnum).Y

        FX = targetx
        FY = targety

        If FX = -1 Then FX = 0
        If FY = -1 Then FY = 0

        ReDim pos(Map(mapNum).MaxX, Map(mapNum).MaxY)
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
                Next i
            Next j

            'If the reachable is STILL false, then
            If reachable = False Then
                'reset sum
                Sum = 0
                For j = 0 To Map(mapNum).MaxY
                    For i = 0 To Map(mapNum).MaxX
                        'we add up ALL the squares
                        Sum = Sum + pos(i, j)
                    Next i
                Next j

                'Now if the sum is euqal to the last sum, its not reachable, if it isnt, then we store
                'sum to lastsum
                If Sum = LastSum Then
                    FindNpcPath = 4
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
            If LastX < Map(mapNum).MaxX Then
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
                If LastY < Map(mapNum).MaxY Then
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
            FindNpcPath = DirectionType.Right
        ElseIf path(1).Y > LastY Then
            FindNpcPath = DirectionType.Down
        ElseIf path(1).Y < LastY Then
            FindNpcPath = DirectionType.Up
        ElseIf path(1).X < LastX Then
            FindNpcPath = DirectionType.Left
        End If

    End Function

    Sub SpawnAllMapGlobalEvents()
        Dim i As Integer

        For i = 0 To MAX_MAPS
            SpawnGlobalEvents(i)
        Next

    End Sub

    Sub SpawnGlobalEvents(mapNum As Integer)
        Dim i As Integer, z As Integer

        If Map(mapNum).EventCount > 0 Then
            TempEventMap(mapNum).EventCount = 0
            ReDim TempEventMap(mapNum).Events(0)
            For i = 1 To Map(mapNum).EventCount
                TempEventMap(mapNum).EventCount = TempEventMap(mapNum).EventCount + 1
                ReDim Preserve TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount)
                If Map(mapNum).Events(i).PageCount > 0 Then
                    If Map(mapNum).Events(i).Globals = 1 Then
                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).X = Map(mapNum).Events(i).X
                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).Y = Map(mapNum).Events(i).Y
                        If Map(mapNum).Events(i).Pages(1).GraphicType = 1 Then
                            Select Case Map(mapNum).Events(i).Pages(1).GraphicY
                                Case 0
                                    TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).Dir = DirectionType.Down
                                Case 1
                                    TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).Dir = DirectionType.Left
                                Case 2
                                    TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).Dir = DirectionType.Right
                                Case 3
                                    TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).Dir = DirectionType.Up
                            End Select
                        Else
                            TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).Dir = DirectionType.Down
                        End If
                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).Active = 1

                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).MoveType = Map(mapNum).Events(i).Pages(1).MoveType

                        If TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).MoveType = 2 Then
                            TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).MoveRouteCount = Map(mapNum).Events(i).Pages(1).MoveRouteCount
                            ReDim TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).MoveRoute(Map(mapNum).Events(i).Pages(1).MoveRouteCount)
                            For z = 0 To Map(mapNum).Events(i).Pages(1).MoveRouteCount
                                TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).MoveRoute(z) = Map(mapNum).Events(i).Pages(1).MoveRoute(z)
                            Next
                            TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).MoveRouteComplete = 0
                        Else
                            TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).MoveRouteComplete = 1
                        End If

                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).RepeatMoveRoute = Map(mapNum).Events(i).Pages(1).RepeatMoveRoute
                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).IgnoreIfCannotMove = Map(mapNum).Events(i).Pages(1).IgnoreMoveRoute

                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).MoveFreq = Map(mapNum).Events(i).Pages(1).MoveFreq
                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).MoveSpeed = Map(mapNum).Events(i).Pages(1).MoveSpeed

                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).WalkThrough = Map(mapNum).Events(i).Pages(1).WalkThrough
                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).FixedDir = Map(mapNum).Events(i).Pages(1).DirFix
                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).WalkingAnim = Map(mapNum).Events(i).Pages(1).WalkAnim
                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).ShowName = Map(mapNum).Events(i).Pages(1).ShowName
                        TempEventMap(mapNum).Events(TempEventMap(mapNum).EventCount).QuestNum = Map(mapNum).Events(i).Pages(1).QuestNum
                    End If
                End If
            Next
        End If

    End Sub

    Friend Sub SpawnMapEventsFor(index As Integer, mapNum As Integer)
        Dim i As Integer, z As Integer, spawncurrentevent As Boolean, p As Integer, compare As Integer

        TempPlayer(index).EventMap.CurrentEvents = 0
        ReDim TempPlayer(index).EventMap.EventPages(0)
        If Map(mapNum).EventCount > 0 Then
            ReDim TempPlayer(index).EventProcessing(Map(mapNum).EventCount)
            TempPlayer(index).EventProcessingCount = Map(mapNum).EventCount
        Else
            ReDim TempPlayer(index).EventProcessing(0)
            TempPlayer(index).EventProcessingCount = 0
        End If

        If Map(mapNum).EventCount <= 0 Then Exit Sub
        For i = 1 To Map(mapNum).EventCount
            If Map(mapNum).Events(i).PageCount > 0 Then
                For z = Map(mapNum).Events(i).PageCount To 1 Step -1
                    With Map(mapNum).Events(i).Pages(z)
                        spawncurrentevent = True

                        If .ChkVariable = 1 Then
                            Select Case .VariableCompare
                                Case 0
                                    If Player(index).Character(TempPlayer(index).CurChar).Variables(.Variableindex) <> .VariableCondition Then
                                        spawncurrentevent = False
                                    End If
                                Case 1
                                    If Player(index).Character(TempPlayer(index).CurChar).Variables(.Variableindex) < .VariableCondition Then
                                        spawncurrentevent = False
                                    End If
                                Case 2
                                    If Player(index).Character(TempPlayer(index).CurChar).Variables(.Variableindex) > .VariableCondition Then
                                        spawncurrentevent = False
                                    End If
                                Case 3
                                    If Player(index).Character(TempPlayer(index).CurChar).Variables(.Variableindex) <= .VariableCondition Then
                                        spawncurrentevent = False
                                    End If
                                Case 4
                                    If Player(index).Character(TempPlayer(index).CurChar).Variables(.Variableindex) >= .VariableCondition Then
                                        spawncurrentevent = False
                                    End If
                                Case 5
                                    If Player(index).Character(TempPlayer(index).CurChar).Variables(.Variableindex) = .VariableCondition Then
                                        spawncurrentevent = False
                                    End If
                            End Select
                        End If

                        'we are assuming the event will spawn, and are looking for ways to stop it
                        If .ChkSwitch = 1 Then
                            If .SwitchCompare = 1 Then 'we want true
                                If Player(index).Character(TempPlayer(index).CurChar).Switches(.Switchindex) = 0 Then 'it is false, so we stop the spawn
                                    spawncurrentevent = False
                                End If
                            Else
                                If Player(index).Character(TempPlayer(index).CurChar).Switches(.Switchindex) = 1 Then 'we want false and it is true so we stop the spawn
                                    spawncurrentevent = False
                                End If
                            End If
                        End If

                        If .ChkHasItem = 1 Then
                            If HasItem(index, .HasItemindex) = 0 Then
                                spawncurrentevent = False
                            End If
                        End If

                        If .ChkSelfSwitch = 1 Then
                            If .SelfSwitchCompare = 0 Then
                                compare = 1
                            Else
                                compare = 0
                            End If
                            If Map(mapNum).Events(i).Globals = 1 Then
                                If Map(mapNum).Events(i).SelfSwitches(.SelfSwitchindex) <> compare Then
                                    spawncurrentevent = False
                                End If
                            Else
                                If compare = 1 Then
                                    spawncurrentevent = False
                                End If
                            End If
                        End If

                        If spawncurrentevent = True OrElse (spawncurrentevent = False AndAlso z = 1) Then
                            'spawn the event... send data to player
                            TempPlayer(index).EventMap.CurrentEvents = TempPlayer(index).EventMap.CurrentEvents + 1
                            ReDim Preserve TempPlayer(index).EventMap.EventPages(TempPlayer(index).EventMap.CurrentEvents)
                            With TempPlayer(index).EventMap.EventPages(TempPlayer(index).EventMap.CurrentEvents)
                                If Map(mapNum).Events(i).Pages(z).GraphicType = 1 Then
                                    Select Case Map(mapNum).Events(i).Pages(z).GraphicY
                                        Case 0
                                            .Dir = DirectionType.Down
                                        Case 1
                                            .Dir = DirectionType.Left
                                        Case 2
                                            .Dir = DirectionType.Right
                                        Case 3
                                            .Dir = DirectionType.Up
                                    End Select
                                Else
                                    .Dir = 0
                                End If
                                .GraphicNum = Map(mapNum).Events(i).Pages(z).Graphic
                                .GraphicType = Map(mapNum).Events(i).Pages(z).GraphicType
                                .GraphicX = Map(mapNum).Events(i).Pages(z).GraphicX
                                .GraphicY = Map(mapNum).Events(i).Pages(z).GraphicY
                                .GraphicX2 = Map(mapNum).Events(i).Pages(z).GraphicX2
                                .GraphicY2 = Map(mapNum).Events(i).Pages(z).GraphicY2
                                Select Case Map(mapNum).Events(i).Pages(z).MoveSpeed
                                    Case 0
                                        .MovementSpeed = 2
                                    Case 1
                                        .MovementSpeed = 3
                                    Case 2
                                        .MovementSpeed = 4
                                    Case 3
                                        .MovementSpeed = 6
                                    Case 4
                                        .MovementSpeed = 12
                                    Case 5
                                        .MovementSpeed = 24
                                End Select
                                If Map(mapNum).Events(i).Globals Then
                                    .X = TempEventMap(mapNum).Events(i).X
                                    .Y = TempEventMap(mapNum).Events(i).Y
                                    .Dir = TempEventMap(mapNum).Events(i).Dir
                                    .MoveRouteStep = TempEventMap(mapNum).Events(i).MoveRouteStep
                                Else
                                    .X = Map(mapNum).Events(i).X
                                    .Y = Map(mapNum).Events(i).Y
                                    .MoveRouteStep = 0
                                End If
                                .Position = Map(mapNum).Events(i).Pages(z).Position
                                .EventId = i
                                .PageId = z
                                If spawncurrentevent = True Then
                                    .Visible = 1
                                Else
                                    .Visible = 0
                                End If

                                .MoveType = Map(mapNum).Events(i).Pages(z).MoveType
                                If .MoveType = 2 Then
                                    .MoveRouteCount = Map(mapNum).Events(i).Pages(z).MoveRouteCount
                                    ReDim .MoveRoute(Map(mapNum).Events(i).Pages(z).MoveRouteCount)
                                    If Map(mapNum).Events(i).Pages(z).MoveRouteCount > 0 Then
                                        For p = 0 To Map(mapNum).Events(i).Pages(z).MoveRouteCount
                                            .MoveRoute(p) = Map(mapNum).Events(i).Pages(z).MoveRoute(p)
                                        Next
                                    End If
                                    .MoveRouteComplete = 0
                                Else
                                    .MoveRouteComplete = 1
                                End If

                                .RepeatMoveRoute = Map(mapNum).Events(i).Pages(z).RepeatMoveRoute
                                .IgnoreIfCannotMove = Map(mapNum).Events(i).Pages(z).IgnoreMoveRoute

                                .MoveFreq = Map(mapNum).Events(i).Pages(z).MoveFreq
                                .MoveSpeed = Map(mapNum).Events(i).Pages(z).MoveSpeed

                                .WalkingAnim = Map(mapNum).Events(i).Pages(z).WalkAnim
                                .WalkThrough = Map(mapNum).Events(i).Pages(z).WalkThrough
                                .ShowName = Map(mapNum).Events(i).Pages(z).ShowName
                                .FixedDir = Map(mapNum).Events(i).Pages(z).DirFix
                                .QuestNum = Map(mapNum).Events(i).Pages(z).QuestNum
                            End With
                            Exit For
                        End If
                    End With
                Next
            End If
        Next

        Dim buffer As ByteStream
        If TempPlayer(index).EventMap.CurrentEvents > 0 Then
            For i = 1 To TempPlayer(index).EventMap.CurrentEvents
                buffer = New ByteStream(4)
                buffer.WriteInt32(ServerPackets.SSpawnEvent)
                buffer.WriteInt32(TempPlayer(index).EventMap.EventPages(i).EventId)
                With TempPlayer(index).EventMap.EventPages(i)
                    buffer.WriteString((Trim$(Map(GetPlayerMap(index)).Events(TempPlayer(index).EventMap.EventPages(i).EventId).Name)))
                    buffer.WriteInt32(.Dir)
                    buffer.WriteInt32(.GraphicNum)
                    buffer.WriteInt32(.GraphicType)
                    buffer.WriteInt32(.GraphicX)
                    buffer.WriteInt32(.GraphicX2)
                    buffer.WriteInt32(.GraphicY)
                    buffer.WriteInt32(.GraphicY2)
                    buffer.WriteInt32(.MovementSpeed)
                    buffer.WriteInt32(.X)
                    buffer.WriteInt32(.Y)
                    buffer.WriteInt32(.Position)
                    buffer.WriteInt32(.Visible)
                    buffer.WriteInt32(Map(mapNum).Events(.EventId).Pages(.PageId).WalkAnim)
                    buffer.WriteInt32(Map(mapNum).Events(.EventId).Pages(.PageId).DirFix)
                    buffer.WriteInt32(Map(mapNum).Events(.EventId).Pages(.PageId).WalkThrough)
                    buffer.WriteInt32(Map(mapNum).Events(.EventId).Pages(.PageId).ShowName)
                    buffer.WriteInt32(Map(mapNum).Events(.EventId).Pages(.PageId).QuestNum)
                End With
                Socket.SendDataTo(index, buffer.Data, buffer.Head)

                AddDebug("Sent SMSG: SSpawnEvent For Player")

                buffer.Dispose()
            Next
        End If

    End Sub

End Module