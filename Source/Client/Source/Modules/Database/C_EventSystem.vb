Imports System.Drawing
Imports System.Windows.Forms
Imports ASFW
Imports SFML.Graphics
Imports SFML.Window

Friend Module C_EventSystem
#Region "Globals"
    ' Temp event storage
    Friend TmpEvent As EventRec
    Friend IsEdit As Boolean

    Friend CurPageNum As Integer
    Friend CurCommand As Integer
    Friend GraphicSelX As Integer
    Friend GraphicSelY As Integer
    Friend GraphicSelX2 As Integer
    Friend GraphicSelY2 As Integer

    Friend EventTileX As Integer
    Friend EventTileY As Integer

    Friend EditorEvent As Integer

    Friend GraphicSelType As Integer 'Are we selecting a graphic for a move route? A page sprite? What???
    Friend TempMoveRouteCount As Integer
    Friend TempMoveRoute() As MoveRouteRec
    Friend IsMoveRouteCommand As Boolean
    Friend ListOfEvents() As Integer

    Friend EventReplyId As Integer
    Friend EventReplyPage As Integer
    Friend EventChatFace As Integer

    Friend RenameType As Integer
    Friend Renameindex as integer
    Friend EventChatTimer As Integer

    Friend EventChat As Boolean
    Friend EventText As String
    Friend ShowEventLbl As Boolean
    Friend EventChoices(4) As String
    Friend EventChoiceVisible(4) As Boolean
    Friend EventChatType As Integer
    Friend AnotherChat As Integer 'Determines if another showtext/showchoices is comming up, if so, dont close the event chatbox...

    'constants
    Friend Switches(MaxSwitches) As String
    Friend Variables(MaxVariables) As String
    Friend Const MaxSwitches As Integer = 500
    Friend Const MaxVariables As Integer = 500

    Friend CpEvent As EventRec
    Friend EventCopy As Boolean
    Friend EventPaste As Boolean
    Friend EventList() As EventListRec

    Friend InEvent As Boolean
    Friend HoldPlayer As Boolean
    Friend InitEventEditorForm As Boolean

#End Region

#Region "Types"
    Friend Structure EventCommandRec
        Dim Index as integer
        Dim Text1 As String
        Dim Text2 As String
        Dim Text3 As String
        Dim Text4 As String
        Dim Text5 As String
        Dim Data1 As Integer
        Dim Data2 As Integer
        Dim Data3 As Integer
        Dim Data4 As Integer
        Dim Data5 As Integer
        Dim Data6 As Integer
        Dim ConditionalBranch As ConditionalBranchRec
        Dim MoveRouteCount As Integer
        Dim MoveRoute() As MoveRouteRec
    End Structure

    Friend Structure MoveRouteRec
        Dim Index as integer
        Dim Data1 As Integer
        Dim Data2 As Integer
        Dim Data3 As Integer
        Dim Data4 As Integer
        Dim Data5 As Integer
        Dim Data6 As Integer
    End Structure

    Friend Structure CommandListRec
        Dim CommandCount As Integer
        Dim ParentList As Integer
        Dim Commands() As EventCommandRec
    End Structure

    Friend Structure ConditionalBranchRec
        Dim Condition As Integer
        Dim Data1 As Integer
        Dim Data2 As Integer
        Dim Data3 As Integer
        Dim CommandList As Integer
        Dim ElseCommandList As Integer
    End Structure

    Friend Structure EventPageRec
        'These are condition variables that decide if the event even appears to the player.
        Dim ChkVariable As Integer
        Dim Variableindex as integer
        Dim VariableCondition As Integer
        Dim VariableCompare As Integer
        Dim ChkSwitch As Integer
        Dim Switchindex as integer
        Dim SwitchCompare As Integer
        Dim ChkHasItem As Integer
        Dim HasItemindex as integer
        Dim HasItemAmount As Integer
        Dim ChkSelfSwitch As Integer
        Dim SelfSwitchindex as integer
        Dim SelfSwitchCompare As Integer
        Dim ChkPlayerGender As Integer
        'End Conditions

        'Handles the Event Sprite
        Dim GraphicType As Byte
        Dim Graphic As Integer
        Dim GraphicX As Integer
        Dim GraphicY As Integer
        Dim GraphicX2 As Integer
        Dim GraphicY2 As Integer

        'Handles Movement - Move Routes to come soon.
        Dim MoveType As Byte
        Dim MoveSpeed As Byte
        Dim MoveFreq As Byte
        Dim MoveRouteCount As Integer
        Dim MoveRoute() As MoveRouteRec
        Dim IgnoreMoveRoute As Integer
        Dim RepeatMoveRoute As Integer

        'Guidelines for the event
        Dim WalkAnim As Byte
        Dim DirFix As Byte
        Dim WalkThrough As Byte
        Dim ShowName As Byte

        'Trigger for the event
        Dim Trigger As Byte

        'Commands for the event
        Dim CommandListCount As Integer
        Dim CommandList() As CommandListRec
        Dim Position As Byte
        Dim Questnum As Integer

        'Client Needed Only
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Friend Structure EventRec
        Dim Name As String
        Dim Globals As Integer
        Dim PageCount As Integer
        Dim Pages() As EventPageRec
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Friend Structure MapEventRec
        Dim Name As String
        Dim Dir As Integer
        Dim X As Integer
        Dim Y As Integer
        Dim GraphicType As Integer
        Dim GraphicX As Integer
        Dim GraphicY As Integer
        Dim GraphicX2 As Integer
        Dim GraphicY2 As Integer
        Dim GraphicNum As Integer
        Dim Moving As Integer
        Dim MovementSpeed As Integer
        Dim Position As Integer
        Dim XOffset As Integer
        Dim YOffset As Integer
        Dim Steps As Integer
        Dim Visible As Integer
        Dim WalkAnim As Integer
        Dim DirFix As Integer
        Dim ShowDir As Integer
        Dim WalkThrough As Integer
        Dim ShowName As Integer
        Dim Questnum As Integer
    End Structure

    Friend CopyEvent As EventRec
    Friend CopyEventPage As EventPageRec

    Friend Structure EventListRec
        Dim CommandList As Integer
        Dim CommandNum As Integer
    End Structure
#End Region

#Region "Enums"
    Friend Enum MoveRouteOpts
        MoveUp = 1
        MoveDown
        MoveLeft
        MoveRight
        MoveRandom
        MoveTowardsPlayer
        MoveAwayFromPlayer
        StepForward
        StepBack
        Wait100Ms
        Wait500Ms
        Wait1000Ms
        TurnUp
        TurnDown
        TurnLeft
        TurnRight
        Turn90Right
        Turn90Left
        Turn180
        TurnRandom
        TurnTowardPlayer
        TurnAwayFromPlayer
        SetSpeed8XSlower
        SetSpeed4XSlower
        SetSpeed2XSlower
        SetSpeedNormal
        SetSpeed2XFaster
        SetSpeed4XFaster
        SetFreqLowest
        SetFreqLower
        SetFreqNormal
        SetFreqHigher
        SetFreqHighest
        WalkingAnimOn
        WalkingAnimOff
        DirFixOn
        DirFixOff
        WalkThroughOn
        WalkThroughOff
        PositionBelowPlayer
        PositionWithPlayer
        PositionAbovePlayer
        ChangeGraphic
    End Enum

    ' Event Types
    Friend Enum EventType
        ' Message
        EvAddText = 1
        EvShowText
        EvShowChoices
        ' Game Progression
        EvPlayerVar
        EvPlayerSwitch
        EvSelfSwitch
        ' Flow Control
        EvCondition
        EvExitProcess
        ' Player
        EvChangeItems
        EvRestoreHp
        EvRestoreMp
        EvLevelUp
        EvChangeLevel
        EvChangeSkills
        EvChangeClass
        EvChangeSprite
        EvChangeSex
        EvChangePk
        ' Movement
        EvWarpPlayer
        EvSetMoveRoute
        ' Character
        EvPlayAnimation
        ' Music and Sounds
        EvPlayBgm
        EvFadeoutBgm
        EvPlaySound
        EvStopSound
        'Etc...
        EvCustomScript
        EvSetAccess
        'Shop/Bank
        EvOpenBank
        EvOpenShop
        'New
        EvGiveExp
        EvShowChatBubble
        EvLabel
        EvGotoLabel
        EvSpawnNpc
        EvFadeIn
        EvFadeOut
        EvFlashWhite
        EvSetFog
        EvSetWeather
        EvSetTint
        EvWait
        EvOpenMail
        EvBeginQuest
        EvEndQuest
        EvQuestTask
        EvShowPicture
        EvHidePicture
        EvWaitMovement
        EvHoldPlayer
        EvReleasePlayer
    End Enum
#End Region

#Region "Incoming Packets"
    Sub Packet_SpawnEvent(ByRef data() As Byte)
        Dim id As Integer
        dim buffer as New ByteStream(Data)
        id = Buffer.ReadInt32
        If id > Map.CurrentEvents Then
            Map.CurrentEvents = id
            ReDim Preserve Map.MapEvents(Map.CurrentEvents)
        End If

        With Map.MapEvents(id)
            .Name = Buffer.ReadString
            .dir = Buffer.ReadInt32
            .ShowDir = .dir
            .GraphicNum = Buffer.ReadInt32
            .GraphicType = Buffer.ReadInt32
            .GraphicX = Buffer.ReadInt32
            .GraphicX2 = Buffer.ReadInt32
            .GraphicY = Buffer.ReadInt32
            .GraphicY2 = Buffer.ReadInt32
            .MovementSpeed = Buffer.ReadInt32
            .Moving = 0
            .X = Buffer.ReadInt32
            .Y = Buffer.ReadInt32
            .XOffset = 0
            .YOffset = 0
            .Position = Buffer.ReadInt32
            .Visible = Buffer.ReadInt32
            .WalkAnim = Buffer.ReadInt32
            .DirFix = Buffer.ReadInt32
            .WalkThrough = Buffer.ReadInt32
            .ShowName = Buffer.ReadInt32
            .questnum = Buffer.ReadInt32
        End With
        Buffer.Dispose()

    End Sub

    Sub Packet_EventMove(ByRef data() As Byte)
        Dim id As Integer
        Dim x As Integer
        Dim y As Integer
        Dim dir As Integer, showDir As Integer
        Dim movementSpeed As Integer
        dim buffer as New ByteStream(Data)
        id = Buffer.ReadInt32
        X = Buffer.ReadInt32
        Y = Buffer.ReadInt32
        dir = Buffer.ReadInt32
        ShowDir = Buffer.ReadInt32
        MovementSpeed = Buffer.ReadInt32
        If id > Map.CurrentEvents Then Exit Sub

        With Map.MapEvents(id)
            .X = X
            .Y = Y
            .dir = dir
            .XOffset = 0
            .YOffset = 0
            .Moving = 1
            .ShowDir = ShowDir
            .MovementSpeed = MovementSpeed

            Select Case dir
                Case DirectionType.Up
                    .YOffset = PicY
                Case DirectionType.Down
                    .YOffset = PicY * -1
                Case DirectionType.Left
                    .XOffset = PicX
                Case DirectionType.Right
                    .XOffset = PicX * -1
            End Select

        End With

    End Sub

    Sub Packet_EventDir(ByRef data() As Byte)
        Dim i As Integer
        Dim dir As Byte
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32
        dir = Buffer.ReadInt32
        If i > Map.CurrentEvents Then Exit Sub

        With Map.MapEvents(i)
            .dir = dir
            .ShowDir = dir
            .XOffset = 0
            .YOffset = 0
            .Moving = 0
        End With

    End Sub

    Sub Packet_SwitchesAndVariables(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        For i = 1 To MaxSwitches
            Switches(i) = Buffer.ReadString
        Next
        For i = 1 To MaxVariables
            Variables(i) = Buffer.ReadString
        Next

        Buffer.Dispose()

    End Sub

    Sub Packet_MapEventData(ByRef data() As Byte)
        Dim i As Integer, x As Integer, y As Integer, z As Integer, w As Integer
        dim buffer as New ByteStream(Data)
        'Event Data!
        Map.EventCount = Buffer.ReadInt32
        If Map.EventCount > 0 Then
            ReDim Map.Events(Map.EventCount)
            For i = 1 To Map.EventCount
                With Map.Events(i)
                    .Name = Buffer.ReadString
                    .Globals = Buffer.ReadInt32
                    .X = Buffer.ReadInt32
                    .Y = Buffer.ReadInt32
                    .PageCount = Buffer.ReadInt32
                End With
                If Map.Events(i).PageCount > 0 Then
                    ReDim Map.Events(i).Pages(Map.Events(i).PageCount)
                    For X = 1 To Map.Events(i).PageCount
                        With Map.Events(i).Pages(X)
                            .chkVariable = Buffer.ReadInt32
                            .VariableIndex = Buffer.ReadInt32
                            .VariableCondition = Buffer.ReadInt32
                            .VariableCompare = Buffer.ReadInt32
                            .chkSwitch = Buffer.ReadInt32
                            .SwitchIndex = Buffer.ReadInt32
                            .SwitchCompare = Buffer.ReadInt32
                            .chkHasItem = Buffer.ReadInt32
                            .HasItemIndex = Buffer.ReadInt32
                            .HasItemAmount = Buffer.ReadInt32
                            .chkSelfSwitch = Buffer.ReadInt32
                            .SelfSwitchIndex = Buffer.ReadInt32
                            .SelfSwitchCompare = Buffer.ReadInt32
                            .GraphicType = Buffer.ReadInt32
                            .Graphic = Buffer.ReadInt32
                            .GraphicX = Buffer.ReadInt32
                            .GraphicY = Buffer.ReadInt32
                            .GraphicX2 = Buffer.ReadInt32
                            .GraphicY2 = Buffer.ReadInt32
                            .MoveType = Buffer.ReadInt32
                            .MoveSpeed = Buffer.ReadInt32
                            .MoveFreq = Buffer.ReadInt32
                            .MoveRouteCount = Buffer.ReadInt32
                            .IgnoreMoveRoute = Buffer.ReadInt32
                            .RepeatMoveRoute = Buffer.ReadInt32
                            If .MoveRouteCount > 0 Then
                                ReDim Map.Events(i).Pages(X).MoveRoute(.MoveRouteCount)
                                For Y = 1 To .MoveRouteCount
                                    .MoveRoute(Y).Index = Buffer.ReadInt32
                                    .MoveRoute(Y).Data1 = Buffer.ReadInt32
                                    .MoveRoute(Y).Data2 = Buffer.ReadInt32
                                    .MoveRoute(Y).Data3 = Buffer.ReadInt32
                                    .MoveRoute(Y).Data4 = Buffer.ReadInt32
                                    .MoveRoute(Y).Data5 = Buffer.ReadInt32
                                    .MoveRoute(Y).Data6 = Buffer.ReadInt32
                                Next
                            End If
                            .WalkAnim = Buffer.ReadInt32
                            .DirFix = Buffer.ReadInt32
                            .WalkThrough = Buffer.ReadInt32
                            .ShowName = Buffer.ReadInt32
                            .Trigger = Buffer.ReadInt32
                            .CommandListCount = Buffer.ReadInt32
                            .Position = Buffer.ReadInt32
                            .Questnum = Buffer.ReadInt32
                        End With
                        If Map.Events(i).Pages(X).CommandListCount > 0 Then
                            ReDim Map.Events(i).Pages(X).CommandList(Map.Events(i).Pages(X).CommandListCount)
                            For Y = 1 To Map.Events(i).Pages(X).CommandListCount
                                Map.Events(i).Pages(X).CommandList(Y).CommandCount = Buffer.ReadInt32
                                Map.Events(i).Pages(X).CommandList(Y).ParentList = Buffer.ReadInt32
                                If Map.Events(i).Pages(X).CommandList(Y).CommandCount > 0 Then
                                    ReDim Map.Events(i).Pages(X).CommandList(Y).Commands(Map.Events(i).Pages(X).CommandList(Y).CommandCount)
                                    For z = 1 To Map.Events(i).Pages(X).CommandList(Y).CommandCount
                                        With Map.Events(i).Pages(X).CommandList(Y).Commands(z)
                                            .Index = Buffer.ReadInt32
                                            .Text1 = Buffer.ReadString
                                            .Text2 = Buffer.ReadString
                                            .Text3 = Buffer.ReadString
                                            .Text4 = Buffer.ReadString
                                            .Text5 = Buffer.ReadString
                                            .Data1 = Buffer.ReadInt32
                                            .Data2 = Buffer.ReadInt32
                                            .Data3 = Buffer.ReadInt32
                                            .Data4 = Buffer.ReadInt32
                                            .Data5 = Buffer.ReadInt32
                                            .Data6 = Buffer.ReadInt32
                                            .ConditionalBranch.CommandList = Buffer.ReadInt32
                                            .ConditionalBranch.Condition = Buffer.ReadInt32
                                            .ConditionalBranch.Data1 = Buffer.ReadInt32
                                            .ConditionalBranch.Data2 = Buffer.ReadInt32
                                            .ConditionalBranch.Data3 = Buffer.ReadInt32
                                            .ConditionalBranch.ElseCommandList = Buffer.ReadInt32
                                            .MoveRouteCount = Buffer.ReadInt32
                                            If .MoveRouteCount > 0 Then
                                                ReDim Preserve .MoveRoute(.MoveRouteCount)
                                                For w = 1 To .MoveRouteCount
                                                    .MoveRoute(w).Index = Buffer.ReadInt32
                                                    .MoveRoute(w).Data1 = Buffer.ReadInt32
                                                    .MoveRoute(w).Data2 = Buffer.ReadInt32
                                                    .MoveRoute(w).Data3 = Buffer.ReadInt32
                                                    .MoveRoute(w).Data4 = Buffer.ReadInt32
                                                    .MoveRoute(w).Data5 = Buffer.ReadInt32
                                                    .MoveRoute(w).Data6 = Buffer.ReadInt32
                                                Next
                                            End If
                                        End With
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If
        'End Event Data
        Buffer.Dispose()

    End Sub

    Sub Packet_EventChat(ByRef data() As Byte)
        Dim i As Integer
        Dim choices As Integer
        dim buffer as New ByteStream(Data)
        EventReplyID = Buffer.ReadInt32
        EventReplyPage = Buffer.ReadInt32
        EventChatFace = Buffer.ReadInt32
        EventText = Buffer.ReadString
        If EventText = "" Then EventText = " "
        EventChat = True
        ShowEventLbl = True
        choices = Buffer.ReadInt32
        InEvent = True
        For i = 1 To 4
            EventChoices(i) = ""
            EventChoiceVisible(i) = False
        Next
        EventChatType = 0
        If choices = 0 Then
        Else
            EventChatType = 1
            For i = 1 To choices
                EventChoices(i) = Buffer.ReadString
                EventChoiceVisible(i) = True
            Next
        End If
        AnotherChat = Buffer.ReadInt32

        Buffer.Dispose()

    End Sub

    Sub Packet_EventStart(ByRef data() As Byte)
        InEvent = True
    End Sub

    Sub Packet_EventEnd(ByRef data() As Byte)
        InEvent = False
    End Sub

    Sub Packet_HoldPlayer(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        If Buffer.ReadInt32 = 0 Then
            HoldPlayer = True
        Else
            HoldPlayer = False
        End If

        Buffer.Dispose()

    End Sub

    Sub Packet_PlayBGM(ByRef data() As Byte)
        Dim music As String
        dim buffer as New ByteStream(Data)
        music = Buffer.ReadString

        PlayMusic(music)

        Buffer.Dispose()
    End Sub

    Sub Packet_FadeOutBGM(ByRef data() As Byte)
        CurMusic = ""
        FadeOutSwitch = True
    End Sub

    Sub Packet_PlaySound(ByRef data() As Byte)
        Dim sound As String
        dim buffer as New ByteStream(Data)
        sound = Buffer.ReadString

        PlaySound(sound)

        Buffer.Dispose()
    End Sub

    Sub Packet_StopSound(ByRef data() As Byte)
        StopSound()
    End Sub

    Sub Packet_SpecialEffect(ByRef data() As Byte)
        Dim effectType As Integer
        dim buffer as New ByteStream(Data)
        effectType = Buffer.ReadInt32

        Select Case effectType
            Case EffectTypeFadein
                UseFade = True
                FadeType = 1
                FadeAmount = 0
            Case EffectTypeFadeout
                UseFade = True
                FadeType = 0
                FadeAmount = 255
            Case EffectTypeFlash
                FlashTimer = GetTickCount() + 150
            Case EffectTypeFog
                CurrentFog = Buffer.ReadInt32
                CurrentFogSpeed = Buffer.ReadInt32
                CurrentFogOpacity = Buffer.ReadInt32
            Case EffectTypeWeather
                CurrentWeather = Buffer.ReadInt32
                CurrentWeatherIntensity = Buffer.ReadInt32
            Case EffectTypeTint
                Map.HasMapTint = 1
                CurrentTintR = Buffer.ReadInt32
                CurrentTintG = Buffer.ReadInt32
                CurrentTintB = Buffer.ReadInt32
                CurrentTintA = Buffer.ReadInt32
        End Select

        Buffer.Dispose()
    End Sub

#End Region

#Region "Outgoing Packets"
    Sub RequestSwitchesAndVariables()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CRequestSwitchesAndVariables)
        Socket.SendData(Buffer.Data, Buffer.Head)

        Buffer.Dispose()
    End Sub

    Sub SendSwitchesAndVariables()
        Dim i As Integer
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CSwitchesAndVariables)

        For i = 1 To MaxSwitches
            buffer.WriteString((Trim$(Switches(i))))
        Next
        For i = 1 To MaxVariables
            buffer.WriteString((Trim$(Variables(i))))
        Next

        Socket.SendData(Buffer.Data, Buffer.Head)

        Buffer.Dispose()
    End Sub
#End Region

#Region "Drawing..."

    Friend Sub DrawEvents()
        Dim rec As Rectangle
        Dim width As Integer, height As Integer, i As Integer, x As Integer, y As Integer
        Dim tX As Integer
        Dim tY As Integer

        If Map.EventCount <= 0 Then Exit Sub
        For i = 1 To Map.EventCount
            Width = 32
            Height = 32
            X = Map.Events(i).X * 32
            Y = Map.Events(i).Y * 32
            If Map.Events(i).PageCount <= 0 Then
                With rec
                    .Y = 0
                    .Height = PicY
                    .X = 0
                    .Width = PicX
                End With

                Dim rec2 As New RectangleShape With {
                    .OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.Blue),
                    .OutlineThickness = 0.6,
                    .FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent),
                    .Size = New Vector2f(rec.Width, rec.Height),
                    .Position = New Vector2f(ConvertMapX(CurX * PicX), ConvertMapY(CurY * PicY))
                }
                GameWindow.Draw(rec2)
                GoTo nextevent
            End If
            x = ConvertMapX(x)
            y = ConvertMapY(y)
            If i > Map.EventCount Then Exit Sub
            If 1 > Map.Events(i).PageCount Then Exit Sub
            Select Case Map.Events(i).Pages(1).GraphicType
                Case 0
                    tX = ((x) - 4) + (PicX * 0.5)
                    tY = ((y) - 7) + (PicY * 0.5)
                    DrawText(tX, tY, "EV", (SFML.Graphics.Color.Green), (SFML.Graphics.Color.Black), GameWindow)
                Case 1
                    If Map.Events(i).Pages(1).Graphic > 0 AndAlso Map.Events(i).Pages(1).Graphic <= NumCharacters Then
                        If CharacterGfxInfo(Map.Events(i).Pages(1).Graphic).IsLoaded = False Then
                            LoadTexture(Map.Events(i).Pages(1).Graphic, 2)
                        End If

                        'seeying we still use it, lets update timer
                        With CharacterGfxInfo(Map.Events(i).Pages(1).Graphic)
                            .TextureTimer = GetTickCount() + 100000
                        End With
                        With rec
                            .Y = (Map.Events(i).Pages(1).GraphicY * (CharacterGfxInfo(Map.Events(i).Pages(1).Graphic).Height / 4))
                            .Height = .Y + PicY
                            .X = (Map.Events(i).Pages(1).GraphicX * (CharacterGfxInfo(Map.Events(i).Pages(1).Graphic).Width / 4))
                            .Width = .X + PicX
                        End With

                        Dim tmpSprite As Sprite = New Sprite(CharacterGfx(Map.Events(i).Pages(1).Graphic)) With {
                            .TextureRect = New IntRect(rec.X, rec.Y, rec.Width, rec.Height),
                            .Position = New Vector2f(ConvertMapX(Map.Events(i).X * PicX), ConvertMapY(Map.Events(i).Y * PicY))
                        }
                        GameWindow.Draw(tmpSprite)
                    Else
                        With rec
                            .Y = 0
                            .Height = PicY
                            .X = 0
                            .Width = PicX
                        End With

                        Dim rec2 As New RectangleShape With {
                            .OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.Blue),
                            .OutlineThickness = 0.6,
                            .FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent),
                            .Size = New Vector2f(rec.Width, rec.Height),
                            .Position = New Vector2f(ConvertMapX(CurX * PicX), ConvertMapY(CurY * PicY))
                        }
                        GameWindow.Draw(rec2)
                    End If
                Case 2
                    If Map.Events(i).Pages(1).Graphic > 0 AndAlso Map.Events(i).Pages(1).Graphic <= NumTileSets Then
                        With rec
                            .X = Map.Events(i).Pages(1).GraphicX * 32
                            .Width = Map.Events(i).Pages(1).GraphicX2 * 32
                            .Y = Map.Events(i).Pages(1).GraphicY * 32
                            .Height = Map.Events(i).Pages(1).GraphicY2 * 32
                        End With

                        If TileSetTextureInfo(Map.Events(i).Pages(1).Graphic).IsLoaded = False Then
                            LoadTexture(Map.Events(i).Pages(1).Graphic, 1)
                        End If
                        ' we use it, lets update timer
                        With TileSetTextureInfo(Map.Events(i).Pages(1).Graphic)
                            .TextureTimer = GetTickCount() + 100000
                        End With

                        If rec.Height > 32 Then
                            RenderSprite(TileSetSprite(Map.Events(i).Pages(1).Graphic), GameWindow, ConvertMapX(Map.Events(i).X * PicX), ConvertMapY(Map.Events(i).Y * PicY) - PicY, rec.X, rec.Y, rec.Width, rec.Height)
                        Else
                            RenderSprite(TileSetSprite(Map.Events(i).Pages(1).Graphic), GameWindow, ConvertMapX(Map.Events(i).X * PicX), ConvertMapY(Map.Events(i).Y * PicY), rec.X, rec.Y, rec.Width, rec.Height)
                        End If

                    Else
                        With rec
                            .Y = 0
                            .Height = PicY
                            .X = 0
                            .Width = PicX
                        End With

                        Dim rec2 As New RectangleShape With {
                            .OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.Blue),
                            .OutlineThickness = 0.6,
                            .FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent),
                            .Size = New Vector2f(rec.Width, rec.Height),
                            .Position = New Vector2f(ConvertMapX(CurX * PicX), ConvertMapY(CurY * PicY))
                        }
                        GameWindow.Draw(rec2)
                    End If
            End Select
nextevent:
        Next

    End Sub

    Friend Sub DrawEvent(id As Integer) ' draw on map, outside the editor
        Dim x As Integer, y As Integer, width As Integer, height As Integer, sRect As Rectangle, anim As Integer, spritetop As Integer

        If Map.MapEvents(Id).Visible = 0 Then Exit Sub

        Select Case Map.MapEvents(Id).GraphicType
            Case 0
                Exit Sub
            Case 1
                If Map.MapEvents(Id).GraphicNum <= 0 OrElse Map.MapEvents(Id).GraphicNum > NumCharacters Then Exit Sub

                ' Reset frame
                If Map.MapEvents(Id).Steps = 3 Then
                    Anim = 0
                ElseIf Map.MapEvents(Id).Steps = 1 Then
                    Anim = 2
                End If

                Select Case Map.MapEvents(Id).dir
                    Case DirectionType.Up
                        If (Map.MapEvents(Id).YOffset > 8) Then Anim = Map.MapEvents(Id).Steps
                    Case DirectionType.Down
                        If (Map.MapEvents(Id).YOffset < -8) Then Anim = Map.MapEvents(Id).Steps
                    Case DirectionType.Left
                        If (Map.MapEvents(Id).XOffset > 8) Then Anim = Map.MapEvents(Id).Steps
                    Case DirectionType.Right
                        If (Map.MapEvents(Id).XOffset < -8) Then Anim = Map.MapEvents(Id).Steps
                End Select

                ' Set the left
                Select Case Map.MapEvents(Id).ShowDir
                    Case DirectionType.Up
                        spritetop = 3
                    Case DirectionType.Right
                        spritetop = 2
                    Case DirectionType.Down
                        spritetop = 0
                    Case DirectionType.Left
                        spritetop = 1
                End Select

                If Map.MapEvents(Id).WalkAnim = 1 Then Anim = 0
                If Map.MapEvents(Id).Moving = 0 Then Anim = Map.MapEvents(Id).GraphicX

                Width = CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Width / 4
                Height = CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Height / 4

                sRect = New Rectangle((Anim) * (CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Width / 4), spritetop * (CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Height / 4), (CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Width / 4), (CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Height / 4))
                ' Calculate the X
                X = Map.MapEvents(Id).X * PicX + Map.MapEvents(Id).XOffset - ((CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Width / 4 - 32) / 2)

                ' Is the player's height more than 32..?
                If (CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Height * 4) > 32 Then
                    ' Create a 32 pixel offset for larger sprites
                    Y = Map.MapEvents(Id).Y * PicY + Map.MapEvents(Id).YOffset - ((CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Height / 4) - 32)
                Else
                    ' Proceed as normal
                    Y = Map.MapEvents(Id).Y * PicY + Map.MapEvents(Id).YOffset
                End If
                ' render the actual sprite
                DrawCharacter(Map.MapEvents(Id).GraphicNum, X, Y, sRect)
            Case 2
                If Map.MapEvents(Id).GraphicNum < 1 OrElse Map.MapEvents(Id).GraphicNum > NumTileSets Then Exit Sub
                If Map.MapEvents(Id).GraphicY2 > 0 OrElse Map.MapEvents(Id).GraphicX2 > 0 Then
                    With sRect
                        .X = Map.MapEvents(Id).GraphicX * 32
                        .Y = Map.MapEvents(Id).GraphicY * 32
                        .Width = Map.MapEvents(Id).GraphicX2 * 32
                        .Height = Map.MapEvents(Id).GraphicY2 * 32
                    End With
                Else
                    With sRect
                        .X = Map.MapEvents(Id).GraphicY * 32
                        .Height = .Top + 32
                        .Y = Map.MapEvents(Id).GraphicX * 32
                        .Width = .Left + 32
                    End With
                End If

                If TileSetTextureInfo(Map.MapEvents(Id).GraphicNum).IsLoaded = False Then
                    LoadTexture(Map.MapEvents(Id).GraphicNum, 1)
                End If
                ' we use it, lets update timer
                With TileSetTextureInfo(Map.MapEvents(Id).GraphicNum)
                    .TextureTimer = GetTickCount() + 100000
                End With

                X = Map.MapEvents(Id).X * 32
                Y = Map.MapEvents(Id).Y * 32
                X = X - ((sRect.Right - sRect.Left) / 2)
                Y = Y - (sRect.Bottom - sRect.Top) + 32

                If Map.MapEvents(Id).GraphicY2 > 1 Then
                    RenderSprite(TileSetSprite(Map.MapEvents(Id).GraphicNum), GameWindow, ConvertMapX(Map.MapEvents(Id).X * PicX), ConvertMapY(Map.MapEvents(Id).Y * PicY) - PicY, sRect.Left, sRect.Top, sRect.Width, sRect.Height)
                Else
                    RenderSprite(TileSetSprite(Map.MapEvents(Id).GraphicNum), GameWindow, ConvertMapX(Map.MapEvents(Id).X * PicX), ConvertMapY(Map.MapEvents(Id).Y * PicY), sRect.Left, sRect.Top, sRect.Width, sRect.Height)
                End If

        End Select

    End Sub

    Friend Sub DrawEventChat()
        Dim temptext As String, txtArray As New List(Of String)
        Dim tmpY As Integer = 0

        'first render panel
        RenderSprite(EventChatSprite, GameWindow, EventChatX, EventChatY, 0, 0, EventChatGFXInfo.Width, EventChatGFXInfo.Height)

        With frmGame
            'face
            If EventChatFace > 0 AndAlso EventChatFace < NumFaces Then
                'render face
                If FacesGFXInfo(EventChatFace).IsLoaded = False Then
                    LoadTexture(EventChatFace, 7)
                End If

                'seeying we still use it, lets update timer
                With FacesGFXInfo(EventChatFace)
                    .TextureTimer = GetTickCount() + 100000
                End With
                RenderSprite(FacesSprite(EventChatFace), GameWindow, EventChatX + 12, EventChatY + 14, 0, 0, FacesGFXInfo(EventChatFace).Width, FacesGFXInfo(EventChatFace).Height)
                EventChatTextX = 113
            Else
                EventChatTextX = 14
            End If

            'EventPrompt
            txtArray = WordWrap(EventText, 45, WrapMode.Characters, WrapType.BreakWord)
            For i = 0 To txtArray.Count
                If i = txtArray.Count Then Exit For
                'draw text
                DrawText(EventChatX + EventChatTextX, EventChatY + EventChatTextY + tmpY, Trim$(txtArray(i).Replace(vbCrLf, "")), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 13)
                tmpY = tmpY + 20
            Next

            If EventChatType = 1 Then

                If EventChoiceVisible(1) Then
                    'Response1
                    temptext = EventChoices(1)
                    DrawText(EventChatX + 10, EventChatY + 124, Trim(temptext), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 13)
                End If

                If EventChoiceVisible(2) Then
                    'Response2
                    temptext = EventChoices(2)
                    DrawText(EventChatX + 10, EventChatY + 146, Trim(temptext), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 13)
                End If

                If EventChoiceVisible(3) Then
                    'Response3
                    temptext = EventChoices(3)
                    DrawText(EventChatX + 226, EventChatY + 124, Trim(temptext), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 13)
                End If

                If EventChoiceVisible(4) Then
                    'Response4
                    temptext = EventChoices(4)
                    DrawText(EventChatX + 226, EventChatY + 146, Trim(temptext), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 13)
                End If

            Else
                temptext = Strings.Get("events", "continue")
                DrawText(EventChatX + 410, EventChatY + 156, Trim(temptext), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow, 13)
            End If

        End With

    End Sub
#End Region

#Region "Misc"

    Sub ProcessEventMovement(id As Integer)

        If Id > Map.EventCount Then Exit Sub
        If Id > Map.MapEvents.Length Then Exit Sub

        If Map.MapEvents(Id).Moving = 1 Then
            Select Case Map.MapEvents(Id).dir
                Case DirectionType.Up
                    Map.MapEvents(Id).YOffset = Map.MapEvents(Id).YOffset - ((ElapsedTime / 1000) * (Map.MapEvents(Id).MovementSpeed * SizeX))
                    If Map.MapEvents(Id).YOffset < 0 Then Map.MapEvents(Id).YOffset = 0
                Case DirectionType.Down
                    Map.MapEvents(Id).YOffset = Map.MapEvents(Id).YOffset + ((ElapsedTime / 1000) * (Map.MapEvents(Id).MovementSpeed * SizeX))
                    If Map.MapEvents(Id).YOffset > 0 Then Map.MapEvents(Id).YOffset = 0
                Case DirectionType.Left
                    Map.MapEvents(Id).XOffset = Map.MapEvents(Id).XOffset - ((ElapsedTime / 1000) * (Map.MapEvents(Id).MovementSpeed * SizeX))
                    If Map.MapEvents(Id).XOffset < 0 Then Map.MapEvents(Id).XOffset = 0
                Case DirectionType.Right
                    Map.MapEvents(Id).XOffset = Map.MapEvents(Id).XOffset + ((ElapsedTime / 1000) * (Map.MapEvents(Id).MovementSpeed * SizeX))
                    If Map.MapEvents(Id).XOffset > 0 Then Map.MapEvents(Id).XOffset = 0
            End Select
            ' Check if completed walking over to the next tile
            If Map.MapEvents(Id).Moving > 0 Then
                If Map.MapEvents(Id).dir = DirectionType.Right OrElse Map.MapEvents(Id).dir = DirectionType.Down Then
                    If (Map.MapEvents(Id).XOffset >= 0) AndAlso (Map.MapEvents(Id).YOffset >= 0) Then
                        Map.MapEvents(Id).Moving = 0
                        If Map.MapEvents(Id).Steps = 1 Then
                            Map.MapEvents(Id).Steps = 3
                        Else
                            Map.MapEvents(Id).Steps = 1
                        End If
                    End If
                Else
                    If (Map.MapEvents(Id).XOffset <= 0) AndAlso (Map.MapEvents(Id).YOffset <= 0) Then
                        Map.MapEvents(Id).Moving = 0
                        If Map.MapEvents(Id).Steps = 1 Then
                            Map.MapEvents(Id).Steps = 3
                        Else
                            Map.MapEvents(Id).Steps = 1
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Friend Function GetColorString(color As Integer)

        Select Case Color
            Case 0
                GetColorString = "Black"
            Case 1
                GetColorString = "Blue"
            Case 2
                GetColorString = "Green"
            Case 3
                GetColorString = "Cyan"
            Case 4
                GetColorString = "Red"
            Case 5
                GetColorString = "Magenta"
            Case 6
                GetColorString = "Brown"
            Case 7
                GetColorString = "Grey"
            Case 8
                GetColorString = "Dark Grey"
            Case 9
                GetColorString = "Bright Blue"
            Case 10
                GetColorString = "Bright Green"
            Case 11
                GetColorString = "Bright Cyan"
            Case 12
                GetColorString = "Bright Red"
            Case 13
                GetColorString = "Pink"
            Case 14
                GetColorString = "Yellow"
            Case 15
                GetColorString = "White"
            Case Else
                GetColorString = "Black"
        End Select

    End Function

    Sub ClearEventChat()
        Dim i As Integer

        If AnotherChat = 1 Then
            For i = 1 To 4
                EventChoiceVisible(i) = False
            Next
            EventText = ""
            EventChatType = 1
            EventChatTimer = GetTickCount() + 100
        ElseIf AnotherChat = 2 Then
            For i = 1 To 4
                EventChoiceVisible(i) = False
            Next
            EventText = ""
            EventChatType = 1
            EventChatTimer = GetTickCount() + 100
        Else
            EventChat = False
        End If
        pnlEventChatVisible = False
    End Sub

    Friend Sub ResetEventdata()
        For i = 0 To Map.EventCount
            ReDim Map.MapEvents(Map.EventCount)
            Map.CurrentEvents = 0
            With Map.MapEvents(i)
                .Name = ""
                .dir = 0
                .ShowDir = 0
                .GraphicNum = 0
                .GraphicType = 0
                .GraphicX = 0
                .GraphicX2 = 0
                .GraphicY = 0
                .GraphicY2 = 0
                .MovementSpeed = 0
                .Moving = 0
                .X = 0
                .Y = 0
                .XOffset = 0
                .YOffset = 0
                .Position = 0
                .Visible = 0
                .WalkAnim = 0
                .DirFix = 0
                .WalkThrough = 0
                .ShowName = 0
                .questnum = 0
            End With
        Next
    End Sub
#End Region

End Module