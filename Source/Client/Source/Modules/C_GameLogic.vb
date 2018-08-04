Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports ASFW

Module C_GameLogic
    Friend GameRand As New Random()

    Sub GameLoop()
        Dim i As Integer
        Dim dest As Point = New Point(FrmGame.PointToScreen(FrmGame.picscreen.Location))
        Dim g As Graphics = FrmGame.picscreen.CreateGraphics
        Dim starttime As Integer, tick As Integer, fogtmr As Integer
        Dim tmpfps As Integer, tmplps As Integer, walkTimer As Integer, frameTime As Integer
        Dim tmr10000 As Integer, tmr1000 As Integer, tmrweather As Integer
        Dim tmr100 As Integer, tmr500 As Integer, tmrconnect As Integer
        Dim rendercount As Integer, fadetmr As Integer

        starttime = GetTickCount()
        FrmMenu.lblNextChar.Left = Lblnextcharleft

        Do
            If GameDestroyed Then End

            DirDown = VbKeyDown
            DirUp = VbKeyUp
            DirLeft = VbKeyLeft
            DirRight = VbKeyRight

            If Frmmenuvisible = True Then
                If tmrconnect < GetTickCount() Then
                    If Socket.IsConnected() = True Then
                        FrmMenu.lblServerStatus.ForeColor = Color.LightGreen
                        FrmMenu.lblServerStatus.Text = Strings.Get("mainmenu", "serveronline")
                    Else
                        i = i + 1
                        If i = 5 Then
                            Connect()
                            FrmMenu.lblServerStatus.Text = Strings.Get("mainmenu", "serverreconnect")
                            FrmMenu.lblServerStatus.ForeColor = Color.Orange
                            i = 0
                        Else
                            FrmMenu.lblServerStatus.Text = Strings.Get("mainmenu", "serveroffline")
                            FrmMenu.lblServerStatus.ForeColor = Color.Red
                        End If
                    End If
                    tmrconnect = GetTickCount() + 500
                End If
            End If

            'Update the UI
            UpdateUi()

            If GameStarted() = True Then
                tick = GetTickCount()
                ElapsedTime = tick - frameTime ' Set the time difference for time-based movement

                frameTime = tick
                Frmmaingamevisible = True

                'Calculate FPS
                If starttime < tick Then
                    Fps = tmpfps
                    Lps = tmplps
                    tmpfps = 0
                    tmplps = 0
                    starttime = tick + 1000
                End If
                tmplps = tmplps + 1
                tmpfps = tmpfps + 1

                ' Update inv animation
                If NumItems > 0 Then
                    If tmr100 < tick Then

                        If InBank Then DrawBank()
                        If InShop Then DrawShop()
                        If InTrade Then DrawTrade()

                        tmr100 = tick + 100
                    End If
                End If

                If ShowAnimTimer < tick Then
                    ShowAnimLayers = Not ShowAnimLayers
                    ShowAnimTimer = tick + 500
                End If

                For i = 1 To Byte.MaxValue
                    CheckAnimInstance(i)
                Next

                If tick > EventChatTimer Then
                    If EventText = "" Then
                        If EventChat = True Then
                            EventChat = False
                            PnlEventChatVisible = False
                        End If
                    End If
                End If

                If tmr10000 < tick Then
                    If Options.HighEnd = 0 Then
                        'clear any unused gfx
                        ClearGfx()
                    End If

                    GetPing()
                    DrawPing()

                    tmr10000 = tick + 10000
                End If

                If tmr1000 < tick Then
                    Time.Instance.Tick()

                    tmr1000 = tick + 1000
                End If

                'crafting timer
                If CraftTimerEnabled Then
                    If CraftTimer < tick Then
                        CraftProgressValue = CraftProgressValue + (100 / Recipe(GetRecipeIndex(RecipeNames(SelectedRecipe))).CreateTime)

                        If CraftProgressValue >= 100 Then
                            CraftTimerEnabled = False
                        End If
                        CraftTimer = tick + 1000
                    End If
                End If

                'screenshake timer
                If ShakeTimerEnabled Then
                    If ShakeTimer < tick Then
                        If ShakeCount < 10 Then
                            If LastDir = 0 Then
                                FrmGame.picscreen.Location = New Point(FrmGame.picscreen.Location.X + 20, FrmGame.picscreen.Location.Y)
                                LastDir = 1
                            Else
                                FrmGame.picscreen.Location = New Point(FrmGame.picscreen.Location.X - 20, FrmGame.picscreen.Location.Y)
                                LastDir = 0
                            End If
                        Else
                            FrmGame.picscreen.Location = New Point(0, 0)
                            ShakeCount = 0
                            ShakeTimerEnabled = False
                        End If

                        ShakeCount += 1

                        ShakeTimer = tick + 50
                    End If
                End If

                ' check if trade timed out
                If TradeRequest = True Then
                    If TradeTimer < tick Then
                        AddText(Strings.Get("trade", "tradetimeout"), ColorType.Yellow)
                        TradeRequest = False
                        TradeTimer = 0
                    End If
                End If

                ' check if we need to end the CD icon
                If NumSkillIcons > 0 Then
                    For i = 1 To MAX_PLAYER_SKILLS
                        If PlayerSkills(i) > 0 Then
                            If SkillCd(i) > 0 Then
                                If SkillCd(i) + (Skill(PlayerSkills(i)).CdTime * 1000) < tick Then
                                    SkillCd(i) = 0
                                    DrawPlayerSkills()
                                End If
                            End If
                        End If
                    Next
                End If

                ' check if we need to unlock the player's skill casting restriction
                If SkillBuffer > 0 Then
                    If SkillBufferTimer + (Skill(PlayerSkills(SkillBuffer)).CastTime * 1000) < tick Then
                        SkillBuffer = 0
                        SkillBufferTimer = 0
                    End If
                End If
                ' check if we need to unlock the pets's spell casting restriction
                If PetSkillBuffer > 0 Then
                    If PetSkillBufferTimer + (Skill(Pet(Player(Myindex).Pet.Num).Skill(PetSkillBuffer)).CastTime * 1000) < tick Then
                        PetSkillBuffer = 0
                        PetSkillBufferTimer = 0
                    End If
                End If

                SyncLock MapLock
                    If CanMoveNow Then
                        CheckMovement() ' Check if player is trying to move
                        CheckAttack()   ' Check to see if player is trying to attack
                    End If

                    ' Process input before rendering, otherwise input will be behind by 1 frame
                    If walkTimer < tick Then

                        For i = 1 To TotalOnline 'MAX_PLAYERS
                            If IsPlaying(i) Then
                                ProcessMovement(i)
                                If PetAlive(i) Then
                                    ProcessPetMovement(i)
                                End If
                            End If
                        Next

                        ' Process npc movements (actually move them)
                        For i = 1 To MAX_MAP_NPCS
                            If Map.Npc(i) > 0 Then
                                ProcessNpcMovement(i)
                            End If
                        Next i

                        If Map.CurrentEvents > 0 Then
                            For i = 1 To Map.CurrentEvents
                                ProcessEventMovement(i)
                            Next i
                        End If

                        walkTimer = tick + 30 ' edit this value to change WalkTimer
                    End If

                    ' fog scrolling
                    If fogtmr < tick Then
                        If CurrentFogSpeed > 0 Then
                            ' move
                            FogOffsetX = FogOffsetX - 1
                            FogOffsetY = FogOffsetY - 1
                            ' reset
                            If FogOffsetX < -255 Then FogOffsetX = 1
                            If FogOffsetY < -255 Then FogOffsetY = 1
                            fogtmr = tick + 255 - CurrentFogSpeed
                        End If
                    End If

                    If tmr500 < tick Then
                        ' animate waterfalls
                        Select Case WaterfallFrame
                            Case 0
                                WaterfallFrame = 1
                            Case 1
                                WaterfallFrame = 2
                            Case 2
                                WaterfallFrame = 0
                        End Select
                        ' animate autotiles
                        Select Case AutoTileFrame
                            Case 0
                                AutoTileFrame = 1
                            Case 1
                                AutoTileFrame = 2
                            Case 2
                                AutoTileFrame = 0
                        End Select

                        tmr500 = tick + 500
                    End If

                    If FadeInSwitch = True Then
                        FadeIn()
                    End If

                    If FadeOutSwitch = True Then
                        FadeOut()
                    End If

                    If InMapEditor Then FrmEditor_MapEditor.EditorMap_DrawTileset()

                    Application.DoEvents()

                    If GettingMap Then
                        Dim font As New Font(Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + "\" + FontName, FontSize)
                        g.DrawString(Strings.Get("gamegui", "maprecieve"), font, Brushes.DarkCyan, FrmGame.picscreen.Width - 130, 5)
                    End If

                End SyncLock
            End If

            If tmrweather < tick Then
                ProcessWeather()
                tmrweather = tick + 50
            End If

            If fadetmr < tick Then
                If FadeType <> 2 Then
                    If FadeType = 1 Then
                        If FadeAmount = 255 Then
                        Else
                            FadeAmount = FadeAmount + 5
                        End If
                    ElseIf FadeType = 0 Then
                        If FadeAmount = 0 Then
                            UseFade = False
                        Else
                            FadeAmount = FadeAmount - 5
                        End If
                    End If
                End If
                fadetmr = tick + 30
            End If

            If rendercount < tick Then
                'Actual Game Loop Stuff :/
                Render_Graphics()
                tmplps = tmplps + 1
                rendercount = tick + 16
            End If

            Application.DoEvents()

            If Options.HighEnd = 1 Then
                Thread.Yield()
            Else
                Thread.Sleep(1)
            End If

        Loop
    End Sub

    Sub ClearTempTile()
        Dim x As Integer
        Dim y As Integer
        ReDim TempTile(Map.MaxX, Map.MaxY)

        For X = 0 To Map.MaxX
            For Y = 0 To Map.MaxY
                TempTile(X, Y).DoorOpen = 0
            Next
        Next

    End Sub

    Sub ProcessNpcMovement(mapNpcNum As Integer)

        ' Check if NPC is walking, and if so process moving them over
        If MapNpc(MapNpcNum).Moving = MovementType.Walking Then

            Select Case MapNpc(MapNpcNum).Dir
                Case DirectionType.Up
                    MapNpc(MapNpcNum).YOffset = MapNpc(MapNpcNum).YOffset - ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
                    If MapNpc(MapNpcNum).YOffset < 0 Then MapNpc(MapNpcNum).YOffset = 0

                Case DirectionType.Down
                    MapNpc(MapNpcNum).YOffset = MapNpc(MapNpcNum).YOffset + ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
                    If MapNpc(MapNpcNum).YOffset > 0 Then MapNpc(MapNpcNum).YOffset = 0

                Case DirectionType.Left
                    MapNpc(MapNpcNum).XOffset = MapNpc(MapNpcNum).XOffset - ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
                    If MapNpc(MapNpcNum).XOffset < 0 Then MapNpc(MapNpcNum).XOffset = 0

                Case DirectionType.Right
                    MapNpc(MapNpcNum).XOffset = MapNpc(MapNpcNum).XOffset + ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
                    If MapNpc(MapNpcNum).XOffset > 0 Then MapNpc(MapNpcNum).XOffset = 0

            End Select

            ' Check if completed walking over to the next tile
            If MapNpc(MapNpcNum).Moving > 0 Then
                If MapNpc(MapNpcNum).Dir = DirectionType.Right OrElse MapNpc(MapNpcNum).Dir = DirectionType.Down Then
                    If (MapNpc(MapNpcNum).XOffset >= 0) AndAlso (MapNpc(MapNpcNum).YOffset >= 0) Then
                        MapNpc(MapNpcNum).Moving = 0
                        If MapNpc(MapNpcNum).Steps = 1 Then
                            MapNpc(MapNpcNum).Steps = 3
                        Else
                            MapNpc(MapNpcNum).Steps = 1
                        End If
                    End If
                Else
                    If (MapNpc(MapNpcNum).XOffset <= 0) AndAlso (MapNpc(MapNpcNum).YOffset <= 0) Then
                        MapNpc(MapNpcNum).Moving = 0
                        If MapNpc(MapNpcNum).Steps = 1 Then
                            MapNpc(MapNpcNum).Steps = 3
                        Else
                            MapNpc(MapNpcNum).Steps = 1
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Sub DrawPing()

        PingToDraw = Ping

        Select Case Ping
            Case -1
                PingToDraw = Strings.Get("gamegui", "pingsync")
            Case 0 To 5
                PingToDraw = Strings.Get("gamegui", "pinglocal")
        End Select

    End Sub

    Friend Function IsInBounds()
        IsInBounds = False

        If (CurX >= 0) AndAlso (CurX <= Map.MaxX) Then
            If (CurY >= 0) AndAlso (CurY <= Map.MaxY) Then
                IsInBounds = True
            End If
        End If

    End Function

    Function GameStarted() As Boolean
        GameStarted = False
        If InGame = False Then Exit Function
        If MapData = False Then Exit Function
        If PlayerData = False Then Exit Function
        GameStarted = True
        pnlloadvisible = False
    End Function

    Friend Sub CreateActionMsg(message As String, color As Integer, msgType As Byte, x As Integer, y As Integer)

        ActionMsgIndex = ActionMsgIndex + 1
        If ActionMsgIndex >= Byte.MaxValue Then ActionMsgIndex = 1

        With ActionMsg(ActionMsgIndex)
            .message = message
            .color = color
            .Type = MsgType
            .Created = GetTickCount()
            .Scroll = 1
            .X = X
            .Y = Y
        End With

        If ActionMsg(ActionMsgIndex).Type = ActionMsgType.Scroll Then
            ActionMsg(ActionMsgIndex).Y = ActionMsg(ActionMsgIndex).Y + Rand(-2, 6)
            ActionMsg(ActionMsgIndex).X = ActionMsg(ActionMsgIndex).X + Rand(-8, 8)
        End If

    End Sub

    Friend Function Rand(maxNumber As Integer, Optional minNumber As Integer = 0) As Integer
        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return GameRand.Next(MinNumber, MaxNumber)
    End Function

    ' BitWise Operators for directional blocking
    Friend Sub SetDirBlock(ByRef blockvar As Byte, ByRef dir As Byte, block As Boolean)
        If block Then
            blockvar = blockvar Or (2 ^ Dir)
        Else
            blockvar = blockvar And Not (2 ^ Dir)
        End If
    End Sub

    Friend Function IsDirBlocked(ByRef blockvar As Byte, ByRef dir As Byte) As Boolean
        Return Not (Not blockvar AndAlso (2 ^ Dir))
    End Function

    Friend Function ConvertCurrency(amount As Integer) As String

        If Int(Amount) < 10000 Then
            ConvertCurrency = Amount
        ElseIf Int(Amount) < 999999 Then
            ConvertCurrency = Int(Amount / 1000) & "k"
        ElseIf Int(Amount) < 999999999 Then
            ConvertCurrency = Int(Amount / 1000000) & "m"
        Else
            ConvertCurrency = Int(Amount / 1000000000) & "b"
        End If

    End Function

    Sub HandlePressEnter()
        Dim chatText As String
        Dim name As String
        Dim i As Integer
        Dim n As Integer
        Dim command() As String
        dim buffer as ByteStream
        ChatText = Trim$(ChatInput.CurrentMessage)
        Name = ""

        If Len(ChatText) = 0 Then Exit Sub
        ChatInput.CurrentMessage = LCase$(ChatText)

        If EventChat = True Then
            If EventChatType = 0 Then
                Buffer = New ByteStream(4)
                Buffer.WriteInt32(ClientPackets.CEventChatReply)
                Buffer.WriteInt32(EventReplyID)
                Buffer.WriteInt32(EventReplyPage)
                Buffer.WriteInt32(0)
                Socket.SendData(Buffer.Data, Buffer.Head)
                Buffer.Dispose()
                ClearEventChat()
                InEvent = False
                Exit Sub
            End If
        End If

        ' Broadcast message
        If Left$(ChatText, 1) = "'" Then
            ChatText = Mid$(ChatText, 2, Len(ChatText) - 1)

            If Len(ChatText) > 0 Then
                BroadcastMsg(ChatText) '("Привет, русский чат")
            End If

            ChatInput.CurrentMessage = ""
            Exit Sub
        End If

        ' party message
        If Left$(ChatText, 1) = "-" Then
            ChatInput.CurrentMessage = Mid$(ChatText, 2, Len(ChatText) - 1)

            If Len(ChatText) > 0 Then
                SendPartyChatMsg(ChatInput.CurrentMessage)
            End If

            ChatInput.CurrentMessage = ""
            Exit Sub
        End If

        ' Player message
        If Left$(ChatText, 1) = "!" Then
            ChatText = Mid$(ChatText, 2, Len(ChatText) - 1)
            Name = ""

            ' Get the desired player from the user text
            For i = 1 To Len(ChatText)

                If Mid$(ChatText, i, 1) <> Space(1) Then
                    Name = Name & Mid$(ChatText, i, 1)
                Else
                    Exit For
                End If

            Next

            ChatInput.CurrentMessage = Trim$(Mid$(ChatText, i, Len(ChatText) - 1))

            ' Make sure they are actually sending something
            If Len(ChatInput.CurrentMessage) > 0 Then
                ' Send the message to the player
                PlayerMsg(ChatInput.CurrentMessage, Name)
            Else
                AddText(Strings.Get("chatcommand", "playermsg"), ColorType.Yellow)
            End If

            ChatInput.CurrentMessage = ""
            Exit Sub
        End If

        If Left$(ChatInput.CurrentMessage, 1) = "/" Then
            Command = Split(ChatInput.CurrentMessage, Space(1))

            Select Case Command(0)
                Case "/emote"
                    ' Checks to make sure we have more than one string in the array
                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("chatcommand", "emote"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If Not IsNumeric(Command(1)) Then
                        AddText(Strings.Get("chatcommand", "emote"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    SendUseEmote(Command(1))

                Case "/help"
                    AddText(Strings.Get("chatcommand", "help1"), ColorType.Yellow)
                    AddText(Strings.Get("chatcommand", "help2"), ColorType.Yellow)
                    AddText(Strings.Get("chatcommand", "help3"), ColorType.Yellow)
                    AddText(Strings.Get("chatcommand", "help4"), ColorType.Yellow)
                    AddText(Strings.Get("chatcommand", "help5"), ColorType.Yellow)

                Case "/houseinvite"

                    ' Checks to make sure we have more than one string in the array
                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("chatcommand", "houseinvite"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If IsNumeric(Command(1)) Then
                        AddText(Strings.Get("chatcommand", "houseinvite"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    SendInvite(Command(1))

                Case "/sellhouse"
                    Buffer = New ByteStream(4)
                    Buffer.WriteInt32(ClientPackets.CSellHouse)
                    Socket.SendData(Buffer.Data, Buffer.Head)
                    Buffer.Dispose()
                Case "/info"

                    ' Checks to make sure we have more than one string in the array
                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("chatcommand", "info"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If IsNumeric(Command(1)) Then
                        AddText(Strings.Get("chatcommand", "info"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    Buffer = New ByteStream(4)
                    Buffer.WriteInt32(ClientPackets.CPlayerInfoRequest)
                    buffer.WriteString((command(1)))
                    Socket.SendData(Buffer.Data, Buffer.Head)
                    Buffer.Dispose()
                ' Whos Online
                Case "/who"
                    SendWhosOnline()
                ' Checking fps
                Case "/fps"
                    BFPS = Not BFPS
                Case "/lps"
                    BLPS = Not BLPS
                ' Request stats
                Case "/stats"
                    Buffer = New ByteStream(4)
                    Buffer.WriteInt32(ClientPackets.CGetStats)
                    Socket.SendData(Buffer.Data, Buffer.Head)
                    Buffer.Dispose()
                Case "/party"
                    ' Make sure they are actually sending something
                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("chatcommand", "party"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If IsNumeric(Command(1)) Then
                        AddText(Strings.Get("chatcommand", "party"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    SendPartyRequest(Command(1))

                ' Join party
                Case "/join"
                    SendAcceptParty()
                ' Leave party
                Case "/leave"
                    SendLeaveParty()

                'release pet
                Case "/releasepet"
                    SendReleasePet()

                ' // Monitor Admin Commands //

                Case "/questreset"
                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("adminchatcommand", "questreset"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If Not IsNumeric(Command(1)) Then
                        AddText(Strings.Get("adminchatcommand", "questreset"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    n = Command(1)

                    ' Check to make sure its a valid map #
                    If n > 0 AndAlso n <= MaxQuests Then
                        QuestReset(n)
                    Else
                        AddText(Strings.Get("adminchatcommand", "wrongquestnr"), QColorType.AlertColor)
                    End If

                ' Admin Help
                Case "/admin"

                    If GetPlayerAccess(MyIndex) < AdminType.Monitor Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    AddText(Strings.Get("adminchatcommand", "admin1"), ColorType.Yellow)
                    AddText(Strings.Get("adminchatcommand", "adminglobal"), ColorType.Yellow)
                    AddText(Strings.Get("adminchatcommand", "adminprivate"), ColorType.Yellow)
                    AddText(Strings.Get("adminchatcommand", "admin2"), ColorType.Yellow)
                ' Kicking a player
                Case "/kick"

                    If GetPlayerAccess(MyIndex) < AdminType.Monitor Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("adminchatcommand", "kick"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If IsNumeric(Command(1)) Then
                        AddText(Strings.Get("adminchatcommand", "kick"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    SendKick(Command(1))
                ' // Mapper Admin Commands //
                ' Location
                Case "/loc"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    BLoc = Not BLoc
                ' Map Editor
                Case "/mapeditor"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    SendRequestEditMap()
                ' Warping to a player
                Case "/warpmeto"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("adminchatcommand", "warpmeto"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If IsNumeric(Command(1)) Then
                        AddText(Strings.Get("adminchatcommand", "warpmeto"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    WarpMeTo(Command(1))
                ' Warping a player to you
                Case "/warptome"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("adminchatcommand", "warptome"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If IsNumeric(Command(1)) Then
                        AddText(Strings.Get("adminchatcommand", "warptome"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    WarpToMe(Command(1))
                ' Warping to a map
                Case "/warpto"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("adminchatcommand", "warpto"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If Not IsNumeric(Command(1)) Then
                        AddText(Strings.Get("adminchatcommand", "warpto"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    n = Command(1)

                    ' Check to make sure its a valid map #
                    If n > 0 AndAlso n <= MAX_MAPS Then
                        WarpTo(n)
                    Else
                        AddText(Strings.Get("adminchatcommand", "wrongmapnr"), QColorType.AlertColor)
                    End If

                ' Setting sprite
                Case "/setsprite"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("adminchatcommand", "setsprite"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If Not IsNumeric(Command(1)) Then
                        AddText(Strings.Get("adminchatcommand", "setsprite"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    SendSetSprite(Command(1))
                ' Map report
                Case "/mapreport"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    SendRequestMapreport()
                ' Respawn request
                Case "/respawn"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    SendMapRespawn()
                ' MOTD change
                Case "/motd"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("adminchatcommand", "motd"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    SendMOTDChange(Right$(ChatText, Len(ChatText) - 5))
                ' Check the ban list
                Case "/banlist"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    SendBanList()
                ' Banning a player
                Case "/ban"

                    If GetPlayerAccess(MyIndex) < AdminType.Mapper Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    If UBound(Command) < 1 Then
                        AddText(Strings.Get("adminchatcommand", "ban"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    SendBan(Command(1))
                ' // Developer Admin Commands //

                ' // Creator Admin Commands //
                ' Giving another player access
                Case "/setaccess"

                    If GetPlayerAccess(MyIndex) < AdminType.Creator Then
                        AddText(Strings.Get("adminchatcommand", "accesswarning"), QColorType.AlertColor)
                        GoTo Continue1
                    End If

                    If UBound(Command) < 2 Then
                        AddText(Strings.Get("adminchatcommand", "setaccess"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    If IsNumeric(Command(1)) OrElse Not IsNumeric(Command(2)) Then
                        AddText(Strings.Get("adminchatcommand", "setaccess"), ColorType.Yellow)
                        GoTo Continue1
                    End If

                    SendSetAccess(Command(1), CLng(Command(2)))
                Case Else
                    AddText(Strings.Get("chatcommand", "wrongcmd"), QColorType.AlertColor)
            End Select

            'continue label where we go instead of exiting the sub
Continue1:
            ChatInput.CurrentMessage = ""
            Exit Sub
        End If

        ' Say message
        If Len(ChatText) > 0 Then
            SayMsg(ChatText)
        End If

        ChatInput.CurrentMessage = ""
    End Sub

    Sub CheckMapGetItem()
        dim buffer as New ByteStream(4)
        Buffer = New ByteStream(4)

        If GetTickCount() > Player(MyIndex).MapGetTimer + 250 Then
            If Trim$(ChatInput.CurrentMessage) = "" Then
                Player(MyIndex).MapGetTimer = GetTickCount()
                Buffer.WriteInt32(ClientPackets.CMapGetItem)
                Socket.SendData(Buffer.Data, Buffer.Head)
            End If
        End If

        Buffer.Dispose()
    End Sub

    Friend Sub UpdateDescWindow(itemnum As Integer, amount As Integer, invNum As Integer, windowType As Byte)
        Dim theName As String = "", tmpRarity As Integer

        If Item(itemnum).Randomize <> 0 AndAlso InvNum <> 0 Then
            If WindowType = 0 Then ' inventory
                theName = Trim(Player(MyIndex).RandInv(InvNum).Prefix) & " " & Trim(Item(itemnum).Name) & " " & Trim(Player(MyIndex).RandInv(InvNum).Suffix)
                tmpRarity = Player(MyIndex).RandInv(InvNum).Rarity
            ElseIf WindowType = 1 Then ' equip
                theName = Trim(Player(MyIndex).RandEquip(InvNum).Prefix) & " " & Trim(Item(itemnum).Name) & " " & Trim(Player(MyIndex).RandEquip(InvNum).Suffix)
                tmpRarity = Player(MyIndex).RandEquip(InvNum).Rarity
            ElseIf WindowType = 2 Then ' bank
                theName = Trim(Bank.ItemRand(InvNum).Prefix) & " " & Trim(Item(itemnum).Name) & " " & Trim(Bank.ItemRand(InvNum).Suffix)
                tmpRarity = Bank.ItemRand(InvNum).Rarity
            ElseIf WindowType = 3 Then ' shop
                theName = Trim(Player(MyIndex).RandEquip(InvNum).Prefix) & " " & Trim(Item(itemnum).Name) & " " & Trim(Player(MyIndex).RandEquip(InvNum).Suffix)
                tmpRarity = Player(MyIndex).RandEquip(InvNum).Rarity
            ElseIf WindowType = 4 Then ' trade
                theName = Trim(Player(MyIndex).RandEquip(InvNum).Prefix) & " " & Trim(Item(itemnum).Name) & " " & Trim(Player(MyIndex).RandEquip(InvNum).Suffix)
                tmpRarity = Player(MyIndex).RandEquip(InvNum).Rarity
            End If
        Else
            theName = Trim$(Item(itemnum).Name)
            tmpRarity = Item(itemnum).Rarity
        End If

        ItemDescName = theName

        ItemDescItemNum = itemnum

        If LastItemDesc = itemnum Then Exit Sub

        ' set the name
        Select Case tmpRarity
            Case 0 ' White
                ItemDescRarityColor = ItemRarityColor0
                ItemDescRarityBackColor = SFML.Graphics.Color.Black
            Case 1 ' green
                ItemDescRarityColor = ItemRarityColor1
                ItemDescRarityBackColor = SFML.Graphics.Color.Black
            Case 2 ' blue
                ItemDescRarityColor = ItemRarityColor2
                ItemDescRarityBackColor = SFML.Graphics.Color.Black
            Case 3 ' red
                ItemDescRarityColor = ItemRarityColor3
                ItemDescRarityBackColor = SFML.Graphics.Color.Black
            Case 4 ' purple
                ItemDescRarityColor = ItemRarityColor4
                ItemDescRarityBackColor = SFML.Graphics.Color.Black
            Case 5 'gold
                ItemDescRarityColor = ItemRarityColor5
                ItemDescRarityBackColor = SFML.Graphics.Color.Black
        End Select

        ItemDescDescription = Item(itemnum).Description

        ' For the stats label
        Select Case Item(itemnum).Type
            Case ItemType.None
                ItemDescInfo = Strings.Get("itemdescription", "notavail")
                ItemDescType = Strings.Get("itemdescription", "notavail")

            Case ItemType.Equipment
                Select Case Item(itemnum).SubType

                    Case EquipmentType.Weapon
                        If Item(itemnum).Randomize <> 0 Then
                            If WindowType = 0 Then
                                ItemDescInfo = "Damage: " & Player(MyIndex).RandInv(InvNum).Damage
                            Else
                                ItemDescInfo = "Damage: " & Player(MyIndex).RandEquip(InvNum).Damage
                            End If
                        Else
                            ItemDescInfo = "Damage: " & Item(itemnum).Data2
                        End If
                        ItemDescType = "Weapon"
                    Case EquipmentType.Armor
                        ItemDescInfo = "Defence: " & Item(itemnum).Data2
                        ItemDescType = "Armor"
                    Case EquipmentType.Helmet
                        ItemDescInfo = "Defence: " & Item(itemnum).Data2
                        ItemDescType = "Helmet"
                    Case EquipmentType.Shield
                        ItemDescInfo = "Defence: " & Item(itemnum).Data2
                        ItemDescType = "Shield"
                    Case EquipmentType.Shoes
                        ItemDescInfo = "Defence: " & Item(itemnum).Data2
                        ItemDescType = "Shoes"
                    Case EquipmentType.Gloves
                        ItemDescInfo = "Defence: " & Item(itemnum).Data2
                        ItemDescType = "Gloves"
                End Select

            Case ItemType.Consumable
                Select Case Item(itemnum).SubType
                    Case ConsumableType.Hp
                        ItemDescInfo = Strings.Get("itemdescription", "restore") & Item(itemnum).Data2
                        ItemDescType = Strings.Get("itemdescription", "potion")
                    Case ConsumableType.Mp
                        ItemDescInfo = Strings.Get("itemdescription", "restore") & Item(itemnum).Data2
                        ItemDescType = Strings.Get("itemdescription", "potion")
                    Case ConsumableType.Sp
                        ItemDescInfo = Strings.Get("itemdescription", "restore") & Item(itemnum).Data2
                        ItemDescType = Strings.Get("itemdescription", "potion")
                    Case ConsumableType.Exp
                        ItemDescInfo = Strings.Get("itemdescription", "amount") & Item(itemnum).Data2
                        ItemDescType = Strings.Get("itemdescription", "potion")
                End Select

            Case ItemType.Key
                ItemDescInfo = Strings.Get("itemdescription", "notavail")
                ItemDescType = Strings.Get("itemdescription", "key")
            Case ItemType.Currency
                ItemDescInfo = Strings.Get("itemdescription", "notavail")
                ItemDescType = Strings.Get("itemdescription", "currency")
            Case ItemType.Skill
                ItemDescInfo = Strings.Get("itemdescription", "notavail")
                ItemDescType = Strings.Get("itemdescription", "skill")
            Case ItemType.Furniture
                ItemDescInfo = Strings.Get("itemdescription", "furniture")
        End Select

        ' Currency
        ItemDescCost = Item(itemnum).Price & "g"

        ' If currency, exit out before all the other shit
        If Item(itemnum).Type = ItemType.Currency OrElse Item(itemnum).Type = ItemType.None Then
            ' Clear other labels
            ItemDescLevel = Strings.Get("itemdescription", "notavail")
            ItemDescSpeed = Strings.Get("itemdescription", "notavail")

            ItemDescStr = Strings.Get("itemdescription", "notavail")
            ItemDescEnd = Strings.Get("itemdescription", "notavail")
            ItemDescInt = Strings.Get("itemdescription", "notavail")
            ItemDescSpr = Strings.Get("itemdescription", "notavail")
            ItemDescVit = Strings.Get("itemdescription", "notavail")
            ItemDescLuck = Strings.Get("itemdescription", "notavail")
            Exit Sub
        End If

        ' Potions + crap
        ItemDescLevel = Item(itemnum).LevelReq

        ' Exit out for everything else 'scept equipment
        If Item(itemnum).Type <> ItemType.Equipment Then
            ' Clear other labels
            ItemDescSpeed = Strings.Get("itemdescription", "notavail")

            ItemDescStr = Strings.Get("itemdescription", "notavail")
            ItemDescEnd = Strings.Get("itemdescription", "notavail")
            ItemDescInt = Strings.Get("itemdescription", "notavail")
            ItemDescSpr = Strings.Get("itemdescription", "notavail")
            ItemDescVit = Strings.Get("itemdescription", "notavail")
            ItemDescLuck = Strings.Get("itemdescription", "notavail")
            Exit Sub
        End If

        ' Equipment specific
        If Item(itemnum).Randomize <> 0 Then
            If WindowType = 0 Then
                If Player(MyIndex).RandInv(InvNum).Stat(StatType.Strength) > 0 Then
                    ItemDescStr = "+" & Player(MyIndex).RandInv(InvNum).Stat(StatType.Strength)
                Else
                    ItemDescStr = Strings.Get("itemdescription", "none")
                End If
            Else
                If Player(MyIndex).RandEquip(InvNum).Stat(StatType.Strength) > 0 Then
                    ItemDescStr = "+" & Player(MyIndex).RandEquip(InvNum).Stat(StatType.Strength)
                Else
                    ItemDescStr = Strings.Get("itemdescription", "none")
                End If
            End If

        Else
            If Item(itemnum).Add_Stat(StatType.Strength) > 0 Then
                ItemDescStr = "+" & Item(itemnum).Add_Stat(StatType.Strength)
            Else
                ItemDescStr = Strings.Get("itemdescription", "none")
            End If
        End If

        If Item(itemnum).Randomize <> 0 Then
            If WindowType = 0 Then
                If Player(MyIndex).RandInv(InvNum).Stat(StatType.Vitality) > 0 Then
                    ItemDescVit = "+" & Player(MyIndex).RandInv(InvNum).Stat(StatType.Vitality)
                Else
                    ItemDescVit = Strings.Get("itemdescription", "none")
                End If
            Else
                If Player(MyIndex).RandEquip(InvNum).Stat(StatType.Vitality) > 0 Then
                    ItemDescVit = "+" & Player(MyIndex).RandEquip(InvNum).Stat(StatType.Vitality)
                Else
                    ItemDescVit = Strings.Get("itemdescription", "none")
                End If
            End If
        Else
            If Item(itemnum).Add_Stat(StatType.Vitality) > 0 Then
                ItemDescVit = "+" & Item(itemnum).Add_Stat(StatType.Vitality)
            Else
                ItemDescVit = Strings.Get("itemdescription", "none")
            End If
        End If

        If Item(itemnum).Randomize <> 0 Then
            If WindowType = 0 Then
                If Player(MyIndex).RandInv(InvNum).Stat(StatType.Intelligence) > 0 Then
                    ItemDescInt = "+" & Player(MyIndex).RandInv(InvNum).Stat(StatType.Intelligence)
                Else
                    ItemDescInt = Strings.Get("itemdescription", "none")
                End If
            Else
                If Player(MyIndex).RandEquip(InvNum).Stat(StatType.Intelligence) > 0 Then
                    ItemDescInt = "+" & Player(MyIndex).RandEquip(InvNum).Stat(StatType.Intelligence)
                Else
                    ItemDescInt = Strings.Get("itemdescription", "none")
                End If
            End If
        Else
            If Item(itemnum).Add_Stat(StatType.Intelligence) > 0 Then
                ItemDescInt = "+" & Item(itemnum).Add_Stat(StatType.Intelligence)
            Else
                ItemDescInt = Strings.Get("itemdescription", "none")
            End If
        End If

        If Item(itemnum).Randomize <> 0 Then
            If WindowType = 0 Then
                If Player(MyIndex).RandInv(InvNum).Stat(StatType.Endurance) > 0 Then
                    ItemDescEnd = "+" & Player(MyIndex).RandInv(InvNum).Stat(StatType.Endurance)
                Else
                    ItemDescEnd = Strings.Get("itemdescription", "none")
                End If
            Else
                If Player(MyIndex).RandEquip(InvNum).Stat(StatType.Endurance) > 0 Then
                    ItemDescEnd = "+" & Player(MyIndex).RandEquip(InvNum).Stat(StatType.Endurance)
                Else
                    ItemDescEnd = Strings.Get("itemdescription", "none")
                End If
            End If

        Else
            If Item(itemnum).Add_Stat(StatType.Endurance) > 0 Then
                ItemDescEnd = "+" & Item(itemnum).Add_Stat(StatType.Endurance)
            Else
                ItemDescEnd = Strings.Get("itemdescription", "none")
            End If
        End If

        If Item(itemnum).Randomize <> 0 Then
            If WindowType = 0 Then
                If Player(MyIndex).RandInv(InvNum).Stat(StatType.Luck) > 0 Then
                    ItemDescLuck = "+" & Player(MyIndex).RandInv(InvNum).Stat(StatType.Luck)
                Else
                    ItemDescLuck = Strings.Get("itemdescription", "none")
                End If
            Else
                If Player(MyIndex).RandEquip(InvNum).Stat(StatType.Luck) > 0 Then
                    ItemDescLuck = "+" & Player(MyIndex).RandEquip(InvNum).Stat(StatType.Luck)
                Else
                    ItemDescLuck = Strings.Get("itemdescription", "none")
                End If
            End If

        Else
            If Item(itemnum).Add_Stat(StatType.Luck) > 0 Then
                ItemDescLuck = "+" & Item(itemnum).Add_Stat(StatType.Luck)
            Else
                ItemDescLuck = Strings.Get("itemdescription", "none")
            End If
        End If

        If Item(itemnum).Randomize <> 0 Then
            If WindowType = 0 Then
                If Player(MyIndex).RandInv(InvNum).Stat(StatType.Spirit) > 0 Then
                    ItemDescSpr = "+" & Player(MyIndex).RandInv(InvNum).Stat(StatType.Spirit)
                Else
                    ItemDescSpr = Strings.Get("itemdescription", "none")
                End If
            Else
                If Player(MyIndex).RandEquip(InvNum).Stat(StatType.Spirit) > 0 Then
                    ItemDescSpr = "+" & Player(MyIndex).RandEquip(InvNum).Stat(StatType.Spirit)
                Else
                    ItemDescSpr = Strings.Get("itemdescription", "none")
                End If
            End If

        Else
            If Item(itemnum).Add_Stat(StatType.Spirit) > 0 Then
                ItemDescSpr = "+" & Item(itemnum).Add_Stat(StatType.Spirit)
            Else
                ItemDescSpr = Strings.Get("itemdescription", "none")
            End If
        End If

        If Item(itemnum).Randomize <> 0 Then
            If WindowType = 0 Then
                If Item(itemnum).SubType = EquipmentType.Weapon Then
                    ItemDescSpeed = Player(MyIndex).RandInv(InvNum).Speed / 1000 & Strings.Get("itemdescription", "secs")
                Else
                    ItemDescSpeed = Strings.Get("itemdescription", "notavail")
                End If
            Else
                If Item(itemnum).SubType = EquipmentType.Weapon Then
                    ItemDescSpeed = Player(MyIndex).RandEquip(InvNum).Speed / 1000 & Strings.Get("itemdescription", "secs")
                Else
                    ItemDescSpeed = Strings.Get("itemdescription", "notavail")
                End If
            End If

        Else
            If Item(itemnum).SubType = EquipmentType.Weapon Then
                ItemDescSpeed = Item(itemnum).Speed / 1000 & Strings.Get("itemdescription", "secs")
            Else
                ItemDescSpeed = Strings.Get("itemdescription", "notavail")
            End If
        End If

    End Sub

    Friend Sub OpenShop(shopnum As Integer)
        InShop = shopnum
        ShopAction = 0
    End Sub

    Friend Function GetBankItemNum(bankslot As Byte) As Integer
        GetBankItemNum = 0

        If bankslot = 0 Then
            GetBankItemNum = 0
            Exit Function
        End If

        If bankslot > MAX_BANK Then
            GetBankItemNum = 0
            Exit Function
        End If

        GetBankItemNum = Bank.Item(bankslot).Num
    End Function

    Friend Sub SetBankItemNum(bankslot As Byte, itemnum As Integer)
        Bank.Item(Bankslot).Num = itemnum
    End Sub

    Friend Function GetBankItemValue(bankslot As Byte) As Integer
        GetBankItemValue = Bank.Item(Bankslot).Value
    End Function

    Friend Sub SetBankItemValue(bankslot As Byte, itemValue As Integer)
        Bank.Item(Bankslot).Value = ItemValue
    End Sub

    Friend Sub ClearActionMsg(index As Byte)
        ActionMsg(Index).message = ""
        ActionMsg(Index).Created = 0
        ActionMsg(Index).Type = 0
        ActionMsg(Index).color = 0
        ActionMsg(Index).Scroll = 0
        ActionMsg(Index).X = 0
        ActionMsg(Index).Y = 0
    End Sub

    Friend Sub UpdateSkillWindow(skillnum As Integer)

        If LastSkillDesc = skillnum Then Exit Sub

        SkillDescName = Skill(skillnum).Name

        Select Case Skill(skillnum).Type
            Case SkillType.DamageHp
                SkillDescType = Strings.Get("skilldescription", "damagehp")
                SkillDescVital = Strings.Get("skilldescription", "damage")
            Case SkillType.DamageMp
                SkillDescType = Strings.Get("skilldescription", "damagemp")
                SkillDescVital = Strings.Get("skilldescription", "damage")
            Case SkillType.HealHp
                SkillDescType = Strings.Get("skilldescription", "healhp")
                SkillDescVital = Strings.Get("skilldescription", "heal")
            Case SkillType.HealMp
                SkillDescType = Strings.Get("skilldescription", "healmp")
                SkillDescVital = Strings.Get("skilldescription", "heal")
            Case SkillType.Warp
                SkillDescType = Strings.Get("skilldescription", "warp")
        End Select

        SkillDescReqMp = Skill(skillnum).MpCost
        SkillDescReqLvl = Skill(skillnum).LevelReq
        SkillDescReqAccess = Skill(skillnum).AccessReq

        If Skill(skillnum).ClassReq > 0 Then
            SkillDescReqClass = Trim$(Classes(Skill(skillnum).ClassReq).Name)
        Else
            SkillDescReqClass = Strings.Get("skilldescription", "none")
        End If

        SkillDescCastTime = Skill(skillnum).CastTime & "s"
        SkillDescCoolDown = Skill(skillnum).CdTime & "s"
        SkillDescDamage = Skill(skillnum).Vital

        If Skill(skillnum).IsAoE Then
            SkillDescAOE = Skill(skillnum).AoE & Strings.Get("skilldescription", "tiles")
        Else
            SkillDescAOE = Strings.Get("skilldescription", "no")
        End If

        If Skill(skillnum).Range > 0 Then
            SkillDescRange = Skill(skillnum).Range & Strings.Get("skilldescription", "tiles")
        Else
            SkillDescRange = Strings.Get("skilldescription", "selfcast")
        End If

    End Sub

    Friend Sub CheckAnimInstance(index as integer)
        Dim looptime As Integer
        Dim layer As Integer, sound As String
        Dim frameCount As Integer

        ' if doesn't exist then exit sub
        If AnimInstance(Index).Animation <= 0 Then Exit Sub
        If AnimInstance(Index).Animation >= MAX_ANIMATIONS Then Exit Sub

        Sound = Animation(AnimInstance(Index).Animation).Sound

        For Layer = 0 To 1
            If AnimInstance(Index).Used(Layer) Then
                looptime = Animation(AnimInstance(Index).Animation).LoopTime(Layer)
                FrameCount = Animation(AnimInstance(Index).Animation).Frames(Layer)


                ' if zero'd then set so we don't have extra loop and/or frame
                If AnimInstance(Index).FrameIndex(Layer) = 0 Then AnimInstance(Index).FrameIndex(Layer) = 1
                If AnimInstance(Index).LoopIndex(Layer) = 0 Then AnimInstance(Index).LoopIndex(Layer) = 1

                ' check if frame timer is set, and needs to have a frame change
                If AnimInstance(Index).Timer(Layer) + looptime <= GetTickCount() Then
                    ' check if out of range
                    If AnimInstance(Index).FrameIndex(Layer) >= FrameCount Then
                        AnimInstance(Index).LoopIndex(Layer) = AnimInstance(Index).LoopIndex(Layer) + 1
                        If AnimInstance(Index).LoopIndex(Layer) > Animation(AnimInstance(Index).Animation).LoopCount(Layer) Then
                            AnimInstance(Index).Used(Layer) = False
                        Else
                            AnimInstance(Index).FrameIndex(Layer) = 1
                        End If
                    Else
                        AnimInstance(Index).FrameIndex(Layer) = AnimInstance(Index).FrameIndex(Layer) + 1
                    End If
                    AnimInstance(Index).Timer(Layer) = GetTickCount()
                End If
            End If
        Next

        ' if neither layer is used, clear
        If AnimInstance(Index).Used(0) = False AndAlso AnimInstance(Index).Used(1) = False Then
            ClearAnimInstance(Index)
        Else
            If Sound <> "" Then PlaySound(Sound)
        End If
    End Sub

    Friend Sub UpdateDrawMapName()
        Dim g As Graphics = Graphics.FromImage(New Bitmap(1, 1))
        'Dim width As Integer
        'width = g.MeasureString(Trim$(Map.Name), New Font(FONT_NAME, FONT_SIZE, FontStyle.Bold, GraphicsUnit.Pixel)).Width
        'DrawMapNameX = ((SCREEN_MAPX + 1) * PicX / 2) - width + 32
        'DrawMapNameY = 1

        Select Case Map.Moral
            Case MapMoralType.None
                DrawMapNameColor = SFML.Graphics.Color.Red
            Case MapMoralType.Safe
                DrawMapNameColor = SFML.Graphics.Color.Green
            Case Else
                DrawMapNameColor = SFML.Graphics.Color.White
        End Select
        g.Dispose()
    End Sub

    Friend Sub AddChatBubble(target As Integer, targetType As Byte, msg As String, colour As Integer)
        Dim i As Integer, index as integer

        ' set the global index

        chatBubbleIndex = chatBubbleIndex + 1
        If chatBubbleIndex < 1 OrElse chatBubbleIndex > Byte.MaxValue Then chatBubbleIndex = 1
        ' default to new bubble
        Index = chatBubbleIndex
        ' loop through and see if that player/npc already has a chat bubble
        For i = 1 To Byte.MaxValue
            If chatBubble(i).targetType = targetType Then
                If chatBubble(i).target = target Then
                    ' reset master index
                    If chatBubbleIndex > 1 Then chatBubbleIndex = chatBubbleIndex - 1
                    ' we use this one now, yes?
                    Index = i
                    Exit For
                End If
            End If
        Next
        ' set the bubble up
        With chatBubble(Index)
            .target = target
            .targetType = targetType
            .Msg = Msg
            .colour = colour
            .Timer = GetTickCount()
            .active = True
        End With

    End Sub

End Module