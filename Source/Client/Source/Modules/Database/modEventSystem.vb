﻿Imports System.Drawing
Imports System.Windows.Forms
Imports ASFW
Imports SFML.Graphics
Imports SFML.Window

Friend Module modEventSystem
#Region "Globals"
    ' Temp event storage
    Friend tmpEvent As EventRec
    Friend isEdit As Boolean

    Friend curPageNum As Integer
    Friend curCommand As Integer
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

    Friend EventReplyID As Integer
    Friend EventReplyPage As Integer
    Friend EventChatFace As Integer

    Friend RenameType As Integer
    Friend RenameIndex As Integer
    Friend EventChatTimer As Integer

    Friend EventChat As Boolean
    Friend EventText As String
    Friend ShowEventLbl As Boolean
    Friend EventChoices(0 To 4) As String
    Friend EventChoiceVisible(0 To 4) As Boolean
    Friend EventChatType As Integer
    Friend AnotherChat As Integer 'Determines if another showtext/showchoices is comming up, if so, dont close the event chatbox...

    'constants
    Friend Switches(MAX_SWITCHES) As String
    Friend Variables(MAX_VARIABLES) As String
    Friend Const MAX_SWITCHES As Integer = 500
    Friend Const MAX_VARIABLES As Integer = 500

    Friend cpEvent As EventRec
    Friend EventCopy As Boolean
    Friend EventPaste As Boolean
    Friend EventList() As EventListRec

    Friend InEvent As Boolean
    Friend HoldPlayer As Boolean
    Friend InitEventEditorForm As Boolean

#End Region

#Region "Types"
    Friend Structure EventCommandRec
        Dim Index As Integer
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
        Dim Index As Integer
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
        Dim chkVariable As Integer
        Dim VariableIndex As Integer
        Dim VariableCondition As Integer
        Dim VariableCompare As Integer
        Dim chkSwitch As Integer
        Dim SwitchIndex As Integer
        Dim SwitchCompare As Integer
        Dim chkHasItem As Integer
        Dim HasItemIndex As Integer
        Dim HasItemAmount As Integer
        Dim chkSelfSwitch As Integer
        Dim SelfSwitchIndex As Integer
        Dim SelfSwitchCompare As Integer
        Dim chkPlayerGender As Integer
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
        Dim dir As Integer
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
        Dim questnum As Integer
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
        Wait100ms
        Wait500ms
        Wait1000ms
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
        SetSpeed8xSlower
        SetSpeed4xSlower
        SetSpeed2xSlower
        SetSpeedNormal
        SetSpeed2xFaster
        SetSpeed4xFaster
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
        evAddText = 1
        evShowText
        evShowChoices
        ' Game Progression
        evPlayerVar
        evPlayerSwitch
        evSelfSwitch
        ' Flow Control
        evCondition
        evExitProcess
        ' Player
        evChangeItems
        evRestoreHP
        evRestoreMP
        evLevelUp
        evChangeLevel
        evChangeSkills
        evChangeClass
        evChangeSprite
        evChangeSex
        evChangePK
        ' Movement
        evWarpPlayer
        evSetMoveRoute
        ' Character
        evPlayAnimation
        ' Music and Sounds
        evPlayBGM
        evFadeoutBGM
        evPlaySound
        evStopSound
        'Etc...
        evCustomScript
        evSetAccess
        'Shop/Bank
        evOpenBank
        evOpenShop
        'New
        evGiveExp
        evShowChatBubble
        evLabel
        evGotoLabel
        evSpawnNpc
        evFadeIn
        evFadeOut
        evFlashWhite
        evSetFog
        evSetWeather
        evSetTint
        evWait
        evOpenMail
        evBeginQuest
        evEndQuest
        evQuestTask
        evShowPicture
        evHidePicture
        evWaitMovement
        evHoldPlayer
        evReleasePlayer
    End Enum
#End Region

#Region "Incoming Packets"
    Sub Packet_SpawnEvent(ByRef Data() As Byte)
        Dim id As Integer
        Dim Buffer As New ByteStream(Data)
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

    Sub Packet_EventMove(ByRef Data() As Byte)
        Dim id As Integer
        Dim X As Integer
        Dim Y As Integer
        Dim dir As Integer, ShowDir As Integer
        Dim MovementSpeed As Integer
        Dim Buffer As New ByteStream(Data)
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
                    .YOffset = PIC_Y
                Case DirectionType.Down
                    .YOffset = PIC_Y * -1
                Case DirectionType.Left
                    .XOffset = PIC_X
                Case DirectionType.Right
                    .XOffset = PIC_X * -1
            End Select

        End With

    End Sub

    Sub Packet_EventDir(ByRef Data() As Byte)
        Dim i As Integer
        Dim dir As Byte
        Dim Buffer As New ByteStream(Data)
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

    Sub Packet_SwitchesAndVariables(ByRef Data() As Byte)
        Dim i As Integer
        Dim Buffer As New ByteStream(Data)
        For i = 1 To MAX_SWITCHES
            Switches(i) = Buffer.ReadString
        Next
        For i = 1 To MAX_VARIABLES
            Variables(i) = Buffer.ReadString
        Next

        Buffer.Dispose()

    End Sub

    Sub Packet_MapEventData(ByRef Data() As Byte)
        Dim i As Integer, X As Integer, Y As Integer, z As Integer, w As Integer
        Dim Buffer As New ByteStream(Data)
        'Event Data!
        Map.EventCount = Buffer.ReadInt32
        If Map.EventCount > 0 Then
            ReDim Map.Events(0 To Map.EventCount)
            For i = 1 To Map.EventCount
                With Map.Events(i)
                    .Name = Buffer.ReadString
                    .Globals = Buffer.ReadInt32
                    .X = Buffer.ReadInt32
                    .Y = Buffer.ReadInt32
                    .PageCount = Buffer.ReadInt32
                End With
                If Map.Events(i).PageCount > 0 Then
                    ReDim Map.Events(i).Pages(0 To Map.Events(i).PageCount)
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
                                ReDim Map.Events(i).Pages(X).MoveRoute(0 To .MoveRouteCount)
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
                            ReDim Map.Events(i).Pages(X).CommandList(0 To Map.Events(i).Pages(X).CommandListCount)
                            For Y = 1 To Map.Events(i).Pages(X).CommandListCount
                                Map.Events(i).Pages(X).CommandList(Y).CommandCount = Buffer.ReadInt32
                                Map.Events(i).Pages(X).CommandList(Y).ParentList = Buffer.ReadInt32
                                If Map.Events(i).Pages(X).CommandList(Y).CommandCount > 0 Then
                                    ReDim Map.Events(i).Pages(X).CommandList(Y).Commands(0 To Map.Events(i).Pages(X).CommandList(Y).CommandCount)
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

    Sub Packet_EventChat(ByRef Data() As Byte)
        Dim i As Integer
        Dim choices As Integer
        Dim Buffer As New ByteStream(Data)
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

    Sub Packet_EventStart(ByRef Data() As Byte)
        InEvent = True
    End Sub

    Sub Packet_EventEnd(ByRef Data() As Byte)
        InEvent = False
    End Sub

    Sub Packet_HoldPlayer(ByRef Data() As Byte)
        Dim Buffer As New ByteStream(Data)
        If Buffer.ReadInt32 = 0 Then
            HoldPlayer = True
        Else
            HoldPlayer = False
        End If

        Buffer.Dispose()

    End Sub

    Sub Packet_PlayBGM(ByRef Data() As Byte)
        Dim music As String
        Dim Buffer As New ByteStream(Data)
        music = Buffer.ReadString

        PlayMusic(music)

        Buffer.Dispose()
    End Sub

    Sub Packet_FadeOutBGM(ByRef Data() As Byte)
        CurMusic = ""
        FadeOutSwitch = True
    End Sub

    Sub Packet_PlaySound(ByRef Data() As Byte)
        Dim sound As String
        Dim Buffer As New ByteStream(Data)
        sound = Buffer.ReadString

        PlaySound(sound)

        Buffer.Dispose()
    End Sub

    Sub Packet_StopSound(ByRef Data() As Byte)
        StopSound()
    End Sub

    Sub Packet_SpecialEffect(ByRef Data() As Byte)
        Dim effectType As Integer
        Dim Buffer As New ByteStream(Data)
        effectType = Buffer.ReadInt32

        Select Case effectType
            Case EFFECT_TYPE_FADEIN
                UseFade = True
                FadeType = 1
                FadeAmount = 0
            Case EFFECT_TYPE_FADEOUT
                UseFade = True
                FadeType = 0
                FadeAmount = 255
            Case EFFECT_TYPE_FLASH
                FlashTimer = GetTickCount() + 150
            Case EFFECT_TYPE_FOG
                CurrentFog = Buffer.ReadInt32
                CurrentFogSpeed = Buffer.ReadInt32
                CurrentFogOpacity = Buffer.ReadInt32
            Case EFFECT_TYPE_WEATHER
                CurrentWeather = Buffer.ReadInt32
                CurrentWeatherIntensity = Buffer.ReadInt32
            Case EFFECT_TYPE_TINT
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
        Dim Buffer As New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CRequestSwitchesAndVariables)
        Socket.SendData(Buffer.Data, Buffer.Head)

        Buffer.Dispose()
    End Sub

    Sub SendSwitchesAndVariables()
        Dim i As Integer
        Dim Buffer As New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CSwitchesAndVariables)

        For i = 1 To MAX_SWITCHES
            Buffer.WriteString(Trim$(Switches(i)))
        Next
        For i = 1 To MAX_VARIABLES
            Buffer.WriteString(Trim$(Variables(i)))
        Next

        Socket.SendData(Buffer.Data, Buffer.Head)

        Buffer.Dispose()
    End Sub
#End Region

#Region "Drawing..."

    Friend Sub DrawEvents()
        Dim rec As Rectangle
        Dim Width As Integer, Height As Integer, i As Integer, X As Integer, Y As Integer
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
                    .Height = PIC_Y
                    .X = 0
                    .Width = PIC_X
                End With
                Dim rec2 As New RectangleShape
                rec2.OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.Blue)
                rec2.OutlineThickness = 0.6
                rec2.FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent)
                rec2.Size = New Vector2f(rec.Width, rec.Height)
                rec2.Position = New Vector2f(ConvertMapX(CurX * PIC_X), ConvertMapY(CurY * PIC_Y))
                GameWindow.Draw(rec2)
                GoTo nextevent
            End If
            X = ConvertMapX(X)
            Y = ConvertMapY(Y)
            If i > Map.EventCount Then Exit Sub
            If 1 > Map.Events(i).PageCount Then Exit Sub
            Select Case Map.Events(i).Pages(1).GraphicType
                Case 0
                    tX = ((X) - 4) + (PIC_X * 0.5)
                    tY = ((Y) - 7) + (PIC_Y * 0.5)
                    DrawText(tX, tY, "EV", (SFML.Graphics.Color.Green), (SFML.Graphics.Color.Black), GameWindow)
                Case 1
                    If Map.Events(i).Pages(1).Graphic > 0 AndAlso Map.Events(i).Pages(1).Graphic <= NumCharacters Then
                        If CharacterGFXInfo(Map.Events(i).Pages(1).Graphic).IsLoaded = False Then
                            LoadTexture(Map.Events(i).Pages(1).Graphic, 2)
                        End If

                        'seeying we still use it, lets update timer
                        With CharacterGFXInfo(Map.Events(i).Pages(1).Graphic)
                            .TextureTimer = GetTickCount() + 100000
                        End With
                        With rec
                            .Y = (Map.Events(i).Pages(1).GraphicY * (CharacterGFXInfo(Map.Events(i).Pages(1).Graphic).Height / 4))
                            .Height = .Y + PIC_Y
                            .X = (Map.Events(i).Pages(1).GraphicX * (CharacterGFXInfo(Map.Events(i).Pages(1).Graphic).Width / 4))
                            .Width = .X + PIC_X
                        End With
                        Dim tmpSprite As Sprite = New Sprite(CharacterGFX(Map.Events(i).Pages(1).Graphic))
                        tmpSprite.TextureRect = New IntRect(rec.X, rec.Y, rec.Width, rec.Height)
                        tmpSprite.Position = New Vector2f(ConvertMapX(Map.Events(i).X * PIC_X), ConvertMapY(Map.Events(i).Y * PIC_Y))
                        GameWindow.Draw(tmpSprite)
                    Else
                        With rec
                            .Y = 0
                            .Height = PIC_Y
                            .X = 0
                            .Width = PIC_X
                        End With
                        Dim rec2 As New RectangleShape
                        rec2.OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.Blue)
                        rec2.OutlineThickness = 0.6
                        rec2.FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent)
                        rec2.Size = New Vector2f(rec.Width, rec.Height)
                        rec2.Position = New Vector2f(ConvertMapX(CurX * PIC_X), ConvertMapY(CurY * PIC_Y))
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
                            RenderSprite(TileSetSprite(Map.Events(i).Pages(1).Graphic), GameWindow, ConvertMapX(Map.Events(i).X * PIC_X), ConvertMapY(Map.Events(i).Y * PIC_Y) - PIC_Y, rec.X, rec.Y, rec.Width, rec.Height)
                        Else
                            RenderSprite(TileSetSprite(Map.Events(i).Pages(1).Graphic), GameWindow, ConvertMapX(Map.Events(i).X * PIC_X), ConvertMapY(Map.Events(i).Y * PIC_Y), rec.X, rec.Y, rec.Width, rec.Height)
                        End If

                    Else
                        With rec
                            .Y = 0
                            .Height = PIC_Y
                            .X = 0
                            .Width = PIC_X
                        End With
                        Dim rec2 As New RectangleShape
                        rec2.OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.Blue)
                        rec2.OutlineThickness = 0.6
                        rec2.FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent)
                        rec2.Size = New Vector2f(rec.Width, rec.Height)
                        rec2.Position = New Vector2f(ConvertMapX(CurX * PIC_X), ConvertMapY(CurY * PIC_Y))
                        GameWindow.Draw(rec2)
                    End If
            End Select
nextevent:
        Next

    End Sub

    Friend Sub DrawEvent(ByVal Id As Integer) ' draw on map, outside the editor
        Dim X As Integer, Y As Integer, Width As Integer, Height As Integer, sRect As Rectangle, Anim As Integer, spritetop As Integer

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
                X = Map.MapEvents(Id).X * PIC_X + Map.MapEvents(Id).XOffset - ((CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Width / 4 - 32) / 2)

                ' Is the player's height more than 32..?
                If (CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Height * 4) > 32 Then
                    ' Create a 32 pixel offset for larger sprites
                    Y = Map.MapEvents(Id).Y * PIC_Y + Map.MapEvents(Id).YOffset - ((CharacterGFXInfo(Map.MapEvents(Id).GraphicNum).Height / 4) - 32)
                Else
                    ' Proceed as normal
                    Y = Map.MapEvents(Id).Y * PIC_Y + Map.MapEvents(Id).YOffset
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
                    RenderSprite(TileSetSprite(Map.MapEvents(Id).GraphicNum), GameWindow, ConvertMapX(Map.MapEvents(Id).X * PIC_X), ConvertMapY(Map.MapEvents(Id).Y * PIC_Y) - PIC_Y, sRect.Left, sRect.Top, sRect.Width, sRect.Height)
                Else
                    RenderSprite(TileSetSprite(Map.MapEvents(Id).GraphicNum), GameWindow, ConvertMapX(Map.MapEvents(Id).X * PIC_X), ConvertMapY(Map.MapEvents(Id).Y * PIC_Y), sRect.Left, sRect.Top, sRect.Width, sRect.Height)
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

    Sub ProcessEventMovement(ByVal Id As Integer)

        If Id > Map.EventCount Then Exit Sub
        If Id > Map.MapEvents.Length Then Exit Sub

        If Map.MapEvents(Id).Moving = 1 Then
            Select Case Map.MapEvents(Id).dir
                Case DirectionType.Up
                    Map.MapEvents(Id).YOffset = Map.MapEvents(Id).YOffset - ((ElapsedTime / 1000) * (Map.MapEvents(Id).MovementSpeed * SIZE_X))
                    If Map.MapEvents(Id).YOffset < 0 Then Map.MapEvents(Id).YOffset = 0
                Case DirectionType.Down
                    Map.MapEvents(Id).YOffset = Map.MapEvents(Id).YOffset + ((ElapsedTime / 1000) * (Map.MapEvents(Id).MovementSpeed * SIZE_X))
                    If Map.MapEvents(Id).YOffset > 0 Then Map.MapEvents(Id).YOffset = 0
                Case DirectionType.Left
                    Map.MapEvents(Id).XOffset = Map.MapEvents(Id).XOffset - ((ElapsedTime / 1000) * (Map.MapEvents(Id).MovementSpeed * SIZE_X))
                    If Map.MapEvents(Id).XOffset < 0 Then Map.MapEvents(Id).XOffset = 0
                Case DirectionType.Right
                    Map.MapEvents(Id).XOffset = Map.MapEvents(Id).XOffset + ((ElapsedTime / 1000) * (Map.MapEvents(Id).MovementSpeed * SIZE_X))
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

    Friend Function GetColorString(ByVal Color As Integer)

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