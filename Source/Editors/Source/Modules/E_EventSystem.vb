Imports ASFW
Imports SFML.Graphics
Imports SFML.Window

Friend Module E_EventSystem

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
    Friend Renameindex As Integer
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
        Dim ChkVariable As Integer

        Dim Variableindex As Integer
        Dim VariableCondition As Integer
        Dim VariableCompare As Integer
        Dim ChkSwitch As Integer
        Dim Switchindex As Integer
        Dim SwitchCompare As Integer
        Dim ChkHasItem As Integer
        Dim HasItemindex As Integer
        Dim HasItemAmount As Integer
        Dim ChkSelfSwitch As Integer
        Dim SelfSwitchindex As Integer
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

#Region "EventEditor"

    'Event Editor Stuffz Also includes event functions from the map editor (copy/paste/delete)

    Sub CopyEvent_Map(x As Integer, y As Integer)
        Dim count As Integer, i As Integer

        count = Map.EventCount
        If count = 0 Then Exit Sub
        For i = 1 To count
            If Map.Events(i).X = x AndAlso Map.Events(i).Y = y Then
                ' copy it
                CopyEvent = Map.Events(i)
                ' exit
                Exit Sub
            End If
        Next

    End Sub

    Sub PasteEvent_Map(x As Integer, y As Integer)
        Dim count As Integer, i As Integer, eventNum As Integer

        count = Map.EventCount
        If count > 0 Then
            For i = 1 To count
                If Map.Events(i).X = x AndAlso Map.Events(i).Y = y Then
                    ' already an event - paste over it
                    eventNum = i
                End If
            Next
        End If

        ' couldn't find one - create one
        If eventNum = 0 Then
            ' increment count
            AddEvent(x, y, True)
            eventNum = count + 1
        End If

        ' copy it
        Map.Events(eventNum) = CopyEvent
        ' set position
        Map.Events(eventNum).X = x
        Map.Events(eventNum).Y = y

    End Sub

    Sub DeleteEvent(x As Integer, y As Integer)
        Dim count As Integer, i As Integer, lowindex As Integer

        If Not InMapEditor Then Exit Sub
        If frmEvents.Visible = True Then Exit Sub
        count = Map.EventCount
        For i = 1 To count
            If Map.Events(i).X = x AndAlso Map.Events(i).Y = y Then
                ' delete it
                ClearEvent(i)
                lowindex = i
                Exit For
            End If
        Next
        ' not found anything
        If lowindex = 0 Then Exit Sub
        ' move everything down an index
        For i = lowindex To count - 1
            Map.Events(i) = Map.Events(i + 1)
        Next
        ' delete the last index
        ClearEvent(count)
        ' set the new count
        Map.EventCount = count - 1
        Map.CurrentEvents = count - 1

    End Sub

    Sub AddEvent(x As Integer, y As Integer, Optional cancelLoad As Boolean = False)
        Dim count As Integer, pageCount As Integer, i As Integer

        count = Map.EventCount + 1
        ' make sure there's not already an event
        If count - 1 > 0 Then
            For i = 1 To count - 1
                If Map.Events(i).X = x AndAlso Map.Events(i).Y = y Then
                    ' already an event - edit it
                    If Not cancelLoad Then EventEditorInit(i)
                    Exit Sub
                End If
            Next
        End If
        ' increment count
        Map.EventCount = count
        ReDim Preserve Map.Events(count)
        ' set the new event
        Map.Events(count).X = x
        Map.Events(count).Y = y
        ' give it a new page
        pageCount = Map.Events(count).PageCount + 1
        Map.Events(count).PageCount = pageCount
        ReDim Preserve Map.Events(count).Pages(pageCount)
        ' load the editor
        If Not cancelLoad Then EventEditorInit(count)

    End Sub

    Sub ClearEvent(eventNum As Integer)
        If eventNum > Map.EventCount OrElse eventNum > UBound(Map.MapEvents) Then Exit Sub
        With Map.Events(eventNum)
            .Name = ""
            .PageCount = 0
            ReDim .Pages(0)
            .Globals = 0
            .X = 0
            .Y = 0
        End With
        With Map.MapEvents(eventNum)
            .Name = ""
            .Dir = 0
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
            .Questnum = 0
        End With

    End Sub

    Sub EventEditorInit(eventNum As Integer)
        'Dim i As Integer

        EditorEvent = eventNum

        TmpEvent = Map.Events(eventNum)
        InitEventEditorForm = True

    End Sub

    Sub EventEditorLoadPage(pageNum As Integer)
        ' populate form

        With TmpEvent.Pages(pageNum)
            GraphicSelX = .GraphicX
            GraphicSelY = .GraphicY
            GraphicSelX2 = .GraphicX2
            GraphicSelY2 = .GraphicY2
            frmEvents.cmbGraphic.SelectedIndex = .GraphicType
            frmEvents.cmbHasItem.SelectedIndex = .HasItemindex
            If .HasItemAmount = 0 Then
                frmEvents.nudCondition_HasItem.Value = 1
            Else
                frmEvents.nudCondition_HasItem.Value = .HasItemAmount
            End If
            frmEvents.cmbMoveFreq.SelectedIndex = .MoveFreq
            frmEvents.cmbMoveSpeed.SelectedIndex = .MoveSpeed
            frmEvents.cmbMoveType.SelectedIndex = .MoveType
            frmEvents.cmbPlayerVar.SelectedIndex = .Variableindex
            frmEvents.cmbPlayerSwitch.SelectedIndex = .Switchindex
            frmEvents.cmbSelfSwitch.SelectedIndex = .SelfSwitchindex
            frmEvents.cmbSelfSwitchCompare.SelectedIndex = .SelfSwitchCompare
            frmEvents.cmbPlayerSwitchCompare.SelectedIndex = .SwitchCompare
            frmEvents.cmbPlayervarCompare.SelectedIndex = .VariableCompare
            frmEvents.chkGlobal.Checked = TmpEvent.Globals
            frmEvents.cmbTrigger.SelectedIndex = .Trigger
            frmEvents.chkDirFix.Checked = .DirFix
            frmEvents.chkHasItem.Checked = .ChkHasItem
            frmEvents.chkPlayerVar.Checked = .ChkVariable
            frmEvents.chkPlayerSwitch.Checked = .ChkSwitch
            frmEvents.chkSelfSwitch.Checked = .ChkSelfSwitch
            frmEvents.chkWalkAnim.Checked = .WalkAnim
            frmEvents.chkWalkThrough.Checked = .WalkThrough
            frmEvents.chkShowName.Checked = .ShowName
            frmEvents.nudPlayerVariable.Value = .VariableCondition
            frmEvents.nudGraphic.Value = .Graphic
            If frmEvents.cmbEventQuest.Items.Count > 0 Then
                If .Questnum >= 0 AndAlso .Questnum <= frmEvents.cmbEventQuest.Items.Count Then
                    frmEvents.cmbEventQuest.SelectedIndex = .Questnum
                End If
            End If
            If frmEvents.cmbEventQuest.SelectedIndex = -1 Then frmEvents.cmbEventQuest.SelectedIndex = 0
            If .ChkHasItem = 0 Then
                frmEvents.cmbHasItem.Enabled = False
            Else
                frmEvents.cmbHasItem.Enabled = True
            End If
            If .ChkSelfSwitch = 0 Then
                frmEvents.cmbSelfSwitch.Enabled = False
                frmEvents.cmbSelfSwitchCompare.Enabled = False
            Else
                frmEvents.cmbSelfSwitch.Enabled = True
                frmEvents.cmbSelfSwitchCompare.Enabled = True
            End If
            If .ChkSwitch = 0 Then
                frmEvents.cmbPlayerSwitch.Enabled = False
                frmEvents.cmbPlayerSwitchCompare.Enabled = False
            Else
                frmEvents.cmbPlayerSwitch.Enabled = True
                frmEvents.cmbPlayerSwitchCompare.Enabled = True
            End If
            If .ChkVariable = 0 Then
                frmEvents.cmbPlayerVar.Enabled = False
                frmEvents.nudPlayerVariable.Enabled = False
                frmEvents.cmbPlayervarCompare.Enabled = False
            Else
                frmEvents.cmbPlayerVar.Enabled = True
                frmEvents.nudPlayerVariable.Enabled = True
                frmEvents.cmbPlayervarCompare.Enabled = True
            End If
            If frmEvents.cmbMoveType.SelectedIndex = 2 Then
                frmEvents.btnMoveRoute.Enabled = True
            Else
                frmEvents.btnMoveRoute.Enabled = False
            End If
            frmEvents.cmbPositioning.SelectedIndex = .Position
            ' show the commands
            EventListCommands()

            EditorEvent_DrawGraphic()
        End With

    End Sub

    Sub EventEditorOk()
        ' copy the event data from the temp event

        Map.Events(EditorEvent) = TmpEvent
        ' unload the form
        frmEvents.Dispose()

    End Sub

    Friend Sub EventListCommands()
        Dim i As Integer, curlist As Integer, x As Integer, indent As String = "", listleftoff() As Integer, conditionalstage() As Integer

        frmEvents.lstCommands.Items.Clear()

        If TmpEvent.Pages(CurPageNum).CommandListCount > 0 Then
            ReDim listleftoff(TmpEvent.Pages(CurPageNum).CommandListCount)
            ReDim conditionalstage(TmpEvent.Pages(CurPageNum).CommandListCount)
            'Start Up at 1
            curlist = 1
            x = -1
newlist:
            For i = 1 To TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
                If listleftoff(curlist) > 0 Then
                    If (TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(listleftoff(curlist)).Index = EventType.EvCondition OrElse TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(listleftoff(curlist)).Index = EventType.EvShowChoices) AndAlso conditionalstage(curlist) <> 0 Then
                        i = listleftoff(curlist)
                    ElseIf listleftoff(curlist) >= i Then
                        i = listleftoff(curlist) + 1
                    End If
                End If
                If i <= TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then
                    If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Index = EventType.EvCondition Then
                        x = x + 1
                        Select Case conditionalstage(curlist)
                            Case 0
                                ReDim Preserve EventList(x)
                                EventList(x).CommandList = curlist
                                EventList(x).CommandNum = i
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Condition
                                    Case 0
                                        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2
                                            Case 0
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] == " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                            Case 1
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] >= " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                            Case 2
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] <= " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                            Case 3
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] > " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                            Case 4
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] < " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                            Case 5
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] != " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                        End Select
                                    Case 1
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 0 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Switch [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Switches(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] == " & "True")
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 1 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Switch [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Switches(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] == " & "False")
                                        End If
                                    Case 2
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Has Item [" & Trim$(Item(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1).Name) & "] x" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2)
                                    Case 3
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Class Is [" & Trim$(Classes(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1).Name) & "]")
                                    Case 4
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Knows Skill [" & Trim$(Skill(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1).Name) & "]")
                                    Case 5
                                        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2
                                            Case 0
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is == " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                            Case 1
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is >= " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                            Case 2
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is <= " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                            Case 3
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is > " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                            Case 4
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is < " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                            Case 5
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is NOT " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                        End Select
                                    Case 6
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 0 Then
                                            Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1
                                                Case 0
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [A] == " & "True")
                                                Case 1
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [B] == " & "True")
                                                Case 2
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [C] == " & "True")
                                                Case 3
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [D] == " & "True")
                                            End Select
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 1 Then
                                            Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1
                                                Case 0
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [A] == " & "False")
                                                Case 1
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [B] == " & "False")
                                                Case 2
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [C] == " & "False")
                                                Case 3
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [D] == " & "False")
                                            End Select
                                        End If
                                    Case 7
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 0 Then
                                            Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3
                                                Case 0
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] not started.")
                                                Case 1
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] is started.")
                                                Case 2
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] is completed.")
                                                Case 3
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] can be started.")
                                                Case 4
                                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] can be ended. (All tasks complete)")
                                            End Select
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 1 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] in progress and on task #" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                        End If
                                    Case 8
                                        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1
                                            Case SexType.Male
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Gender is Male")
                                            Case SexType.Female
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's  Gender is Female")
                                        End Select
                                    Case 9
                                        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1
                                            Case TimeOfDay.Day
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Time of Day is Day")
                                            Case TimeOfDay.Night
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Time of Day is Night")
                                            Case TimeOfDay.Dawn
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Time of Day is Dawn")
                                            Case TimeOfDay.Dusk
                                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Time of Day is Dusk")
                                        End Select
                                End Select
                                indent = indent & "       "
                                listleftoff(curlist) = i
                                conditionalstage(curlist) = 1
                                curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.CommandList
                                GoTo newlist
                            Case 1
                                ReDim Preserve EventList(x)
                                EventList(x).CommandList = curlist
                                EventList(x).CommandNum = 0
                                frmEvents.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "Else")
                                listleftoff(curlist) = i
                                conditionalstage(curlist) = 2
                                curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.ElseCommandList
                                GoTo newlist
                            Case 2
                                ReDim Preserve EventList(x)
                                EventList(x).CommandList = curlist
                                EventList(x).CommandNum = 0
                                frmEvents.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "End Branch")
                                indent = Mid(indent, 1, Len(indent) - 7)
                                listleftoff(curlist) = i
                                conditionalstage(curlist) = 0
                        End Select
                    ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Index = EventType.EvShowChoices Then
                        x = x + 1
                        Select Case conditionalstage(curlist)
                            Case 0
                                ReDim Preserve EventList(x)
                                EventList(x).CommandList = curlist
                                EventList(x).CommandNum = i
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data5 > 0 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Choices - Prompt: " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - Face: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data5)
                                Else
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Choices - Prompt: " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - No Face")
                                End If
                                indent = indent & "       "
                                listleftoff(curlist) = i
                                conditionalstage(curlist) = 1
                                GoTo newlist
                            Case 1
                                If Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text2) <> "" Then
                                    ReDim Preserve EventList(x)
                                    EventList(x).CommandList = curlist
                                    EventList(x).CommandNum = 0
                                    frmEvents.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "When [" & Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text2) & "]")
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 2
                                    curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1
                                    GoTo newlist
                                Else
                                    x = x - 1
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 2
                                    curlist = curlist
                                    GoTo newlist
                                End If
                            Case 2
                                If Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text3) <> "" Then
                                    ReDim Preserve EventList(x)
                                    EventList(x).CommandList = curlist
                                    EventList(x).CommandNum = 0
                                    frmEvents.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "When [" & Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text3) & "]")
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 3
                                    curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2
                                    GoTo newlist
                                Else
                                    x = x - 1
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 3
                                    curlist = curlist
                                    GoTo newlist
                                End If
                            Case 3
                                If Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text4) <> "" Then
                                    ReDim Preserve EventList(x)
                                    EventList(x).CommandList = curlist
                                    EventList(x).CommandNum = 0
                                    frmEvents.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "When [" & Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text4) & "]")
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 4
                                    curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3
                                    GoTo newlist
                                Else
                                    x = x - 1
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 4
                                    curlist = curlist
                                    GoTo newlist
                                End If
                            Case 4
                                If Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text5) <> "" Then
                                    ReDim Preserve EventList(x)
                                    EventList(x).CommandList = curlist
                                    EventList(x).CommandNum = 0
                                    frmEvents.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "When [" & Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text5) & "]")
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 5
                                    curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4
                                    GoTo newlist
                                Else
                                    x = x - 1
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 5
                                    curlist = curlist
                                    GoTo newlist
                                End If
                            Case 5
                                ReDim Preserve EventList(x)
                                EventList(x).CommandList = curlist
                                EventList(x).CommandNum = 0
                                frmEvents.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "Branch End")
                                indent = Mid(indent, 1, Len(indent) - 7)
                                listleftoff(curlist) = i
                                conditionalstage(curlist) = 0
                        End Select
                    Else
                        x = x + 1
                        ReDim Preserve EventList(x)
                        EventList(x).CommandList = curlist
                        EventList(x).CommandNum = i
                        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Index
                            Case EventType.EvAddText
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2
                                    Case 0
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Add Text - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - Color: " & GetColorString(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & " - Chat Type: Player")
                                    Case 1
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Add Text - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - Color: " & GetColorString(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & " - Chat Type: Map")
                                    Case 2
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Add Text - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - Color: " & GetColorString(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & " - Chat Type: Global")
                                End Select
                            Case EventType.EvShowText
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 = 0 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Text - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - No Face")
                                Else
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Text - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - Face: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1)
                                End If
                            Case EventType.EvPlayerVar
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2
                                    Case 0
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] == " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3)
                                    Case 1
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] + " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3)
                                    Case 2
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] - " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3)
                                    Case 3
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] Random Between " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & " and " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4)
                                End Select
                            Case EventType.EvPlayerSwitch
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Switch [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & ". " & Switches(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] == True")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Switch [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & ". " & Switches(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] == False")
                                End If
                            Case EventType.EvSelfSwitch
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1
                                    Case 0
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [A] to ON")
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [A] to OFF")
                                        End If
                                    Case 1
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [B] to ON")
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [B] to OFF")
                                        End If
                                    Case 2
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [C] to ON")
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [C] to OFF")
                                        End If
                                    Case 3
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [D] to ON")
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [D] to OFF")
                                        End If
                                End Select
                            Case EventType.EvExitProcess
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Exit Event Processing")
                            Case EventType.EvChangeItems
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Item Amount of [" & Trim$(Item(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "] to " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3)
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Give Player " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & " " & Trim$(Item(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "(s)")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 2 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Take " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & " " & Trim$(Item(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "(s) from Player.")
                                End If
                            Case EventType.EvRestoreHp
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Restore Player HP")
                            Case EventType.EvRestoreMp
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Restore Player MP")
                            Case EventType.EvLevelUp
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Level Up Player")
                            Case EventType.EvChangeLevel
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Level to " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1)
                            Case EventType.EvChangeSkills
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Teach Player Skill [" & Trim$(Skill(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "]")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Remove Player Skill [" & Trim$(Skill(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "]")
                                End If
                            Case EventType.EvChangeClass
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Class to " & Trim$(Classes(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name))
                            Case EventType.EvChangeSprite
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Sprite to " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1)
                            Case EventType.EvChangeSex
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 = 0 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Sex to Male.")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 = 1 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Sex to Female.")
                                End If
                            Case EventType.EvChangePk
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 = 0 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player PK to No.")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 = 1 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player PK to Yes.")
                                End If
                            Case EventType.EvWarpPlayer
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4 = 0 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Warp Player To Map: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & ") while retaining direction.")
                                Else
                                    Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4 - 1
                                        Case DirectionType.Up
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Warp Player To Map: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & ") facing upward.")
                                        Case DirectionType.Down
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Warp Player To Map: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & ") facing downward.")
                                        Case DirectionType.Left
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Warp Player To Map: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & ") facing left.")
                                        Case DirectionType.Right
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Warp Player To Map: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & ") facing right.")
                                    End Select
                                End If
                            Case EventType.EvSetMoveRoute
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 <= Map.EventCount Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Move Route for Event #" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " [" & Trim$(Map.Events(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "]")
                                Else
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Move Route for COULD NOT FIND EVENT!")
                                End If
                            Case EventType.EvPlayAnimation
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Play Animation " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " [" & Trim$(Animation(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "]" & " on Player")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Play Animation " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " [" & Trim$(Animation(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "]" & " on Event #" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & " [" & Trim$(Map.Events(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3).Name) & "]")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 2 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Play Animation " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " [" & Trim$(Animation(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "]" & " on Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4 & ")")
                                End If
                            Case EventType.EvCustomScript
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Execute Custom Script Case: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1)
                            Case EventType.EvPlayBgm
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Play BGM [" & Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1) & "]")
                            Case EventType.EvFadeoutBgm
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Fadeout BGM")
                            Case EventType.EvPlaySound
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Play Sound [" & Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1) & "]")
                            Case EventType.EvStopSound
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Stop Sound")
                            Case EventType.EvOpenBank
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Open Bank")
                            Case EventType.EvOpenMail
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Open Mail Box")
                            Case EventType.EvOpenShop
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Open Shop [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ". " & Trim$(Shop(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "]")
                            Case EventType.EvSetAccess
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Player Access [" & frmEvents.cmbSetAccess.Items(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "]")
                            Case EventType.EvGiveExp
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Give Player " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Experience.")
                            Case EventType.EvShowChatBubble
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1
                                    Case TargetType.Player
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Chat Bubble - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - On Player")
                                    Case TargetType.Npc
                                        If Map.Npc(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) <= 0 Then
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Chat Bubble - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - On NPC [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & ". ]")
                                        Else
                                            frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Chat Bubble - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - On NPC [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & ". " & Trim$(Npc(Map.Npc(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2)).Name) & "]")
                                        End If
                                    Case TargetType.Event
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Chat Bubble - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - On Event [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & ". " & Trim$(Map.Events(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2).Name) & "]")
                                End Select
                            Case EventType.EvLabel
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Label: [" & Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1) & "]")
                            Case EventType.EvGotoLabel
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Jump to Label: [" & Trim$(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1) & "]")
                            Case EventType.EvSpawnNpc
                                If Map.Npc(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) <= 0 Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Spawn NPC: [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ". " & "]")
                                Else
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Spawn NPC: [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ". " & Trim$(Npc(Map.Npc(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1)).Name) & "]")
                                End If
                            Case EventType.EvFadeIn
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Fade In")
                            Case EventType.EvFadeOut
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Fade Out")
                            Case EventType.EvFlashWhite
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Flash White")
                            Case EventType.EvSetFog
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Fog [Fog: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & " Speed: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & " Opacity: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3) & "]")
                            Case EventType.EvSetWeather
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1
                                    Case WeatherType.None
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Weather [None]")
                                    Case WeatherType.Rain
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Weather [Rain - Intensity: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & "]")
                                    Case WeatherType.Snow
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Weather [Snow - Intensity: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & "]")
                                    Case WeatherType.Sandstorm
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Weather [Sand Storm - Intensity: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & "]")
                                    Case WeatherType.Storm
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Weather [Storm - Intensity: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & "]")
                                End Select
                            Case EventType.EvSetTint
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Set Map Tint RGBA [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "," & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & "," & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3) & "," & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4) & "]")
                            Case EventType.EvWait
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Wait " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & " Ms")
                            Case EventType.EvBeginQuest
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Begin Quest: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ". " & Trim$(Quest(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name))
                            Case EventType.EvEndQuest
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "End Quest: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ". " & Trim$(Quest(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name))
                            Case EventType.EvQuestTask
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Complete Quest Task: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ". " & Trim$(Quest(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & " - Task# " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2)
                            Case EventType.EvShowPicture
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3
                                    Case 1
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Picture " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 + 1) & ": Pic=" & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & " Top Left, X: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4) & " Y: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data5))
                                    Case 2
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Picture " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 + 1) & ": Pic=" & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & " Center Screen, X: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4) & " Y: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data5))
                                    Case 3
                                        frmEvents.lstCommands.Items.Add(indent & "@>" & "Show Picture " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 + 1) & ": Pic=" & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & " On Player, X: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4) & " Y: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data5))
                                End Select
                            Case EventType.EvHidePicture
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Hide Picture " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 + 1))
                            Case EventType.EvWaitMovement
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 <= Map.EventCount Then
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Wait for Event #" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " [" & Trim$(Map.Events(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name) & "] to complete move route.")
                                Else
                                    frmEvents.lstCommands.Items.Add(indent & "@>" & "Wait for COULD NOT FIND EVENT to complete move route.")
                                End If
                            Case EventType.EvHoldPlayer
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Hold Player [Do not allow player to move.]")
                            Case EventType.EvReleasePlayer
                                frmEvents.lstCommands.Items.Add(indent & "@>" & "Release Player [Allow player to turn and move again.]")
                            Case Else
                                'Ghost
                                x = x - 1
                                If x = -1 Then
                                    ReDim EventList(0)
                                Else
                                    ReDim Preserve EventList(x)
                                End If
                        End Select
                    End If
                End If
            Next
            If curlist > 1 Then
                x = x + 1
                ReDim Preserve EventList(x)
                EventList(x).CommandList = curlist
                EventList(x).CommandNum = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount + 1
                frmEvents.lstCommands.Items.Add(indent & "@> ")
                curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).ParentList
                GoTo newlist
            End If
        End If
        frmEvents.lstCommands.Items.Add(indent & "@> ")

        Dim z As Integer
        x = 0
        For i = 0 To frmEvents.lstCommands.Items.Count - 1
            'X = frmEditor_Events.TextWidth(frmEditor_Events.lstCommands.Items.Item(i).ToString)
            If x > z Then z = x
        Next

        ScrollCommands(z)

    End Sub

    Friend Sub ScrollCommands(size As Integer)

        'Call SendMessage(frmEditor_Events.lstCommands.hwnd, LB_SETHORIZONTALEXTENT, (size) + 6, 0&)

    End Sub

    Sub ListCommandAdd(s As String)

        frmEvents.lstCommands.Items.Add(s)

    End Sub

    Sub AddCommand(index As Integer)
        Dim curlist As Integer, i As Integer, x As Integer, curslot As Integer, p As Integer, oldCommandList As CommandListRec

        If TmpEvent.Pages(CurPageNum).CommandListCount = 0 Then
            TmpEvent.Pages(CurPageNum).CommandListCount = 1
            ReDim TmpEvent.Pages(CurPageNum).CommandList(1)
        End If

        If frmEvents.lstCommands.SelectedIndex = frmEvents.lstCommands.Items.Count - 1 Then
            curlist = 1
        Else
            curlist = EventList(frmEvents.lstCommands.SelectedIndex).CommandList
        End If
        If TmpEvent.Pages(CurPageNum).CommandListCount = 0 Then
            TmpEvent.Pages(CurPageNum).CommandListCount = 1
            ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist)
        End If
        oldCommandList = TmpEvent.Pages(CurPageNum).CommandList(curlist)
        TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount + 1
        p = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
        If p <= 0 Then
            ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(0)
        Else
            ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(p)
            TmpEvent.Pages(CurPageNum).CommandList(curlist).ParentList = oldCommandList.ParentList
            TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = p
            For i = 1 To p - 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i) = oldCommandList.Commands(i)
            Next
        End If
        If frmEvents.lstCommands.SelectedIndex = frmEvents.lstCommands.Items.Count - 1 Then
            curslot = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
        Else
            i = EventList(frmEvents.lstCommands.SelectedIndex).CommandNum
            If i < TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then
                For x = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount - 1 To i Step -1
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(x + 1) = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(x)
                Next
                curslot = EventList(frmEvents.lstCommands.SelectedIndex).CommandNum
            Else
                curslot = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
            End If
        End If

        Select Case index
            Case EventType.EvAddText
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtAddText_Text.Text
                'tmpEvent.Pages(curPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Events.scrlAddText_Colour.Value
                If frmEvents.optAddText_Player.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEvents.optAddText_Map.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                ElseIf frmEvents.optAddText_Global.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                End If
            Case EventType.EvCondition
                'This is the part where the whole entire source goes to hell :D
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandListCount = TmpEvent.Pages(CurPageNum).CommandListCount + 2
                ReDim Preserve TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount)
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.CommandList = TmpEvent.Pages(CurPageNum).CommandListCount - 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.ElseCommandList = TmpEvent.Pages(CurPageNum).CommandListCount
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.CommandList).ParentList = curlist
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.ElseCommandList).ParentList = curlist

                If frmEvents.optCondition0.Checked = True Then x = 0
                If frmEvents.optCondition1.Checked = True Then x = 1
                If frmEvents.optCondition2.Checked = True Then x = 2
                If frmEvents.optCondition3.Checked = True Then x = 3
                If frmEvents.optCondition4.Checked = True Then x = 4
                If frmEvents.optCondition5.Checked = True Then x = 5
                If frmEvents.optCondition6.Checked = True Then x = 6
                If frmEvents.optCondition7.Checked = True Then x = 7
                If frmEvents.optCondition8.Checked = True Then x = 8
                If frmEvents.optCondition9.Checked = True Then x = 9

                Select Case x
                    Case 0 'Player Var
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 0
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_PlayerVarIndex.SelectedIndex + 1
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEvents.cmbCondition_PlayerVarCompare.SelectedIndex
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3 = frmEvents.nudCondition_PlayerVarCondition.Value
                    Case 1 'Player Switch
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 1
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_PlayerSwitch.SelectedIndex + 1
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEvents.cmbCondtion_PlayerSwitchCondition.SelectedIndex
                    Case 2 'Has Item
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 2
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_HasItem.SelectedIndex + 1
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEvents.nudCondition_HasItem.Value
                    Case 3 'Class Is
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 3
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_ClassIs.SelectedIndex + 1
                    Case 4 'Learnt Skill
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 4
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_LearntSkill.SelectedIndex + 1
                    Case 5 'Level Is
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 5
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.nudCondition_LevelAmount.Value
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEvents.cmbCondition_LevelCompare.SelectedIndex
                    Case 6 'Self Switch
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 6
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_SelfSwitch.SelectedIndex
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEvents.cmbCondition_SelfSwitchCondition.SelectedIndex
                    Case 7 'Quest Shiz
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 7
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.nudCondition_Quest.Value
                        If frmEvents.optCondition_Quest0.Checked Then
                            TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = 0
                            TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3 = frmEvents.cmbCondition_General.SelectedIndex
                        Else
                            TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = 1
                            TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3 = frmEvents.nudCondition_QuestTask.Value
                        End If
                    Case 8 'Gender
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 8
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_Gender.SelectedIndex
                    Case 9 'time of day
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 9
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_Time.SelectedIndex
                End Select

            Case EventType.EvShowText
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                Dim tmptxt As String = ""
                For i = 0 To UBound(frmEvents.txtShowText.Lines)
                    tmptxt = tmptxt & frmEvents.txtShowText.Lines(i)
                Next
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = tmptxt
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudShowTextFace.Value

            Case EventType.EvShowChoices
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtChoicePrompt.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text2 = frmEvents.txtChoices1.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text3 = frmEvents.txtChoices2.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text4 = frmEvents.txtChoices3.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text5 = frmEvents.txtChoices4.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data5 = frmEvents.nudShowChoicesFace.Value
                TmpEvent.Pages(CurPageNum).CommandListCount = TmpEvent.Pages(CurPageNum).CommandListCount + 4
                ReDim Preserve TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount)
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = TmpEvent.Pages(CurPageNum).CommandListCount - 3
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = TmpEvent.Pages(CurPageNum).CommandListCount - 2
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = TmpEvent.Pages(CurPageNum).CommandListCount - 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = TmpEvent.Pages(CurPageNum).CommandListCount
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount - 3).ParentList = curlist
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount - 2).ParentList = curlist
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount - 1).ParentList = curlist
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount).ParentList = curlist

            Case EventType.EvPlayerVar
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbVariable.SelectedIndex + 1

                If frmEvents.optVariableAction0.Checked = True Then i = 0
                If frmEvents.optVariableAction1.Checked = True Then i = 1
                If frmEvents.optVariableAction2.Checked = True Then i = 2
                If frmEvents.optVariableAction3.Checked = True Then i = 3

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = i
                If i = 3 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudVariableData3.Value
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEvents.nudVariableData4.Value
                ElseIf i = 0 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudVariableData0.Value
                ElseIf i = 1 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudVariableData1.Value
                ElseIf i = 2 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudVariableData2.Value
                End If

            Case EventType.EvPlayerSwitch
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbSwitch.SelectedIndex + 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.cmbPlayerSwitchSet.SelectedIndex

            Case EventType.EvSelfSwitch
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbSetSelfSwitch.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.cmbSetSelfSwitchTo.SelectedIndex

            Case EventType.EvExitProcess
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvChangeItems
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbChangeItemIndex.SelectedIndex + 1
                If frmEvents.optChangeItemSet.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEvents.optChangeItemAdd.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                ElseIf frmEvents.optChangeItemRemove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                End If
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudChangeItemsAmount.Value

            Case EventType.EvRestoreHp
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvRestoreMp
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvLevelUp
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvChangeLevel
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudChangeLevel.Value

            Case EventType.EvChangeSkills
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbChangeSkills.SelectedIndex + 1
                If frmEvents.optChangeSkillsAdd.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEvents.optChangeSkillsRemove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                End If

            Case EventType.EvChangeClass
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbChangeClass.SelectedIndex + 1

            Case EventType.EvChangeSprite
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudChangeSprite.Value

            Case EventType.EvChangeSex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                If frmEvents.optChangeSexMale.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = 0
                ElseIf frmEvents.optChangeSexFemale.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = 1
                End If

            Case EventType.EvChangePk
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbSetPK.SelectedIndex

            Case EventType.EvWarpPlayer
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudWPMap.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudWPX.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudWPY.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEvents.cmbWarpPlayerDir.SelectedIndex

            Case EventType.EvSetMoveRoute
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = ListOfEvents(frmEvents.cmbEvent.SelectedIndex)
                If frmEvents.chkIgnoreMove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                Else
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                End If

                If frmEvents.chkRepeatRoute.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = 1
                Else
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = 0
                End If

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRouteCount = TempMoveRouteCount
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRoute = TempMoveRoute

            Case EventType.EvPlayAnimation
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbPlayAnim.SelectedIndex + 1
                If frmEvents.cmbAnimTargetType.SelectedIndex = 0 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEvents.cmbAnimTargetType.SelectedIndex = 1 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.cmbPlayAnimEvent.SelectedIndex + 1
                ElseIf frmEvents.cmbAnimTargetType.SelectedIndex = 2 = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudPlayAnimTileX.Value
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEvents.nudPlayAnimTileY.Value
                End If

            Case EventType.EvCustomScript
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudCustomScript.Value

            Case EventType.EvPlayBgm
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = MusicCache(frmEvents.cmbPlayBGM.SelectedIndex + 1)

            Case EventType.EvFadeoutBgm
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvPlaySound
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = SoundCache(frmEvents.cmbPlaySound.SelectedIndex + 1)

            Case EventType.EvStopSound
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvOpenBank
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvOpenMail
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvOpenShop
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbOpenShop.SelectedIndex + 1

            Case EventType.EvSetAccess
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbSetAccess.SelectedIndex

            Case EventType.EvGiveExp
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudGiveExp.Value

            Case EventType.EvShowChatBubble
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtChatbubbleText.Text

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbChatBubbleTargetType.SelectedIndex + 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.cmbChatBubbleTarget.SelectedIndex + 1

            Case EventType.EvLabel
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtLabelName.Text

            Case EventType.EvGotoLabel
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtGotoLabel.Text

            Case EventType.EvSpawnNpc
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbSpawnNpc.SelectedIndex + 1

            Case EventType.EvFadeIn
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvFadeOut
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvFlashWhite
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvSetFog
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudFogData0.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudFogData1.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudFogData2.Value

            Case EventType.EvSetWeather
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.CmbWeather.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudWeatherIntensity.Value

            Case EventType.EvSetTint
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudMapTintData0.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudMapTintData1.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudMapTintData2.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEvents.nudMapTintData3.Value

            Case EventType.EvWait
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudWaitAmount.Value

            Case EventType.EvBeginQuest
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbBeginQuest.SelectedIndex + 1

            Case EventType.EvEndQuest
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbEndQuest.SelectedIndex + 1

            Case EventType.EvQuestTask
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbCompleteQuest.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudCompleteQuestTask.Value

            Case EventType.EvShowPicture
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbPicIndex.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudShowPicture.Value

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.cmbPicLoc.SelectedIndex + 1

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEvents.nudPicOffsetX.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data5 = frmEvents.nudPicOffsetY.Value

            Case EventType.EvHidePicture
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudHidePic.Value

            Case EventType.EvWaitMovement
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = ListOfEvents(frmEvents.cmbMoveWait.SelectedIndex)

            Case EventType.EvHoldPlayer
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index

            Case EventType.EvReleasePlayer
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = index
        End Select
        EventListCommands()

    End Sub

    Friend Sub EditEventCommand()
        Dim i As Integer, x As Integer, curlist As Integer, curslot As Integer

        i = frmEvents.lstCommands.SelectedIndex
        If i = -1 Then Exit Sub
        If i > UBound(EventList) Then Exit Sub

        frmEvents.fraConditionalBranch.Visible = False
        frmEvents.fraDialogue.BringToFront()

        curlist = EventList(i).CommandList
        curslot = EventList(i).CommandNum
        If curlist = 0 Then Exit Sub
        If curslot = 0 Then Exit Sub
        If curlist > TmpEvent.Pages(CurPageNum).CommandListCount Then Exit Sub
        If curslot > TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then Exit Sub
        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index
            Case EventType.EvAddText
                IsEdit = True
                frmEvents.txtAddText_Text.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                'frmEditor_Events.scrlAddText_Colour.Value = tmpEvent.Pages(curPageNum).CommandList(curlist).Commands(curslot).Data1
                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                    Case 0
                        frmEvents.optAddText_Player.Checked = True
                    Case 1
                        frmEvents.optAddText_Map.Checked = True
                    Case 2
                        frmEvents.optAddText_Global.Checked = True
                End Select
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraAddText.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvCondition
                IsEdit = True
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraConditionalBranch.Visible = True
                frmEvents.fraCommands.Visible = False
                frmEvents.ClearConditionFrame()

                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition
                    Case 0
                        frmEvents.optCondition0.Checked = True
                    Case 1
                        frmEvents.optCondition1.Checked = True
                    Case 2
                        frmEvents.optCondition2.Checked = True
                    Case 3
                        frmEvents.optCondition3.Checked = True
                    Case 4
                        frmEvents.optCondition4.Checked = True
                    Case 5
                        frmEvents.optCondition5.Checked = True
                    Case 6
                        frmEvents.optCondition6.Checked = True
                    Case 7
                        frmEvents.optCondition7.Checked = True
                    Case 8
                        frmEvents.optCondition8.Checked = True
                    Case 9
                        frmEvents.optCondition9.Checked = True
                End Select

                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition
                    Case 0
                        frmEvents.cmbCondition_PlayerVarIndex.Enabled = True
                        frmEvents.cmbCondition_PlayerVarCompare.Enabled = True
                        frmEvents.nudCondition_PlayerVarCondition.Enabled = True
                        frmEvents.cmbCondition_PlayerVarIndex.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 - 1
                        frmEvents.cmbCondition_PlayerVarCompare.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2
                        frmEvents.nudCondition_PlayerVarCondition.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3
                    Case 1
                        frmEvents.cmbCondition_PlayerSwitch.Enabled = True
                        frmEvents.cmbCondtion_PlayerSwitchCondition.Enabled = True
                        frmEvents.cmbCondition_PlayerSwitch.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 - 1
                        frmEvents.cmbCondtion_PlayerSwitchCondition.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2
                    Case 2
                        frmEvents.cmbCondition_HasItem.Enabled = True
                        frmEvents.nudCondition_HasItem.Enabled = True
                        frmEvents.cmbCondition_HasItem.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 - 1
                        frmEvents.nudCondition_HasItem.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2
                    Case 3
                        frmEvents.cmbCondition_ClassIs.Enabled = True
                        frmEvents.cmbCondition_ClassIs.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 - 1
                    Case 4
                        frmEvents.cmbCondition_LearntSkill.Enabled = True
                        frmEvents.cmbCondition_LearntSkill.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 - 1
                    Case 5
                        frmEvents.cmbCondition_LevelCompare.Enabled = True
                        frmEvents.nudCondition_LevelAmount.Enabled = True
                        frmEvents.nudCondition_LevelAmount.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1
                        frmEvents.cmbCondition_LevelCompare.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2
                    Case 6
                        frmEvents.cmbCondition_SelfSwitch.Enabled = True
                        frmEvents.cmbCondition_SelfSwitchCondition.Enabled = True
                        frmEvents.cmbCondition_SelfSwitch.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1
                        frmEvents.cmbCondition_SelfSwitchCondition.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2
                    Case 7
                        frmEvents.nudCondition_Quest.Enabled = True
                        frmEvents.nudCondition_Quest.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1
                        frmEvents.fraConditions_Quest.Visible = True
                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = 0 Then
                            frmEvents.optCondition_Quest0.Checked = True
                            frmEvents.cmbCondition_General.Enabled = True
                            frmEvents.cmbCondition_General.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3
                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = 1 Then
                            frmEvents.optCondition_Quest1.Checked = True
                            frmEvents.nudCondition_QuestTask.Enabled = True
                            frmEvents.nudCondition_QuestTask.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3
                        End If
                    Case 8
                        frmEvents.cmbCondition_Gender.Enabled = True
                        frmEvents.cmbCondition_Gender.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1
                    Case 9
                        frmEvents.cmbCondition_Time.Enabled = True
                        frmEvents.cmbCondition_Time.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1
                End Select
            Case EventType.EvShowText
                IsEdit = True
                frmEvents.txtShowText.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                frmEvents.nudShowTextFace.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraShowText.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvShowChoices
                IsEdit = True
                frmEvents.txtChoicePrompt.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                frmEvents.txtChoices1.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text2
                frmEvents.txtChoices2.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text3
                frmEvents.txtChoices3.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text4
                frmEvents.txtChoices4.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text5
                frmEvents.nudShowChoicesFace.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data5
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraShowChoices.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvPlayerVar
                IsEdit = True
                frmEvents.cmbVariable.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                    Case 0
                        frmEvents.optVariableAction0.Checked = True
                        frmEvents.nudVariableData0.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                    Case 1
                        frmEvents.optVariableAction1.Checked = True
                        frmEvents.nudVariableData1.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                    Case 2
                        frmEvents.optVariableAction2.Checked = True
                        frmEvents.nudVariableData2.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                    Case 3
                        frmEvents.optVariableAction3.Checked = True
                        frmEvents.nudVariableData3.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                        frmEvents.nudVariableData4.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4
                End Select
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraPlayerVariable.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvPlayerSwitch
                IsEdit = True
                frmEvents.cmbSwitch.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEvents.cmbPlayerSwitchSet.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraPlayerSwitch.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvSelfSwitch
                IsEdit = True
                frmEvents.cmbSetSelfSwitch.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.cmbSetSelfSwitchTo.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraSetSelfSwitch.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvChangeItems
                IsEdit = True
                frmEvents.cmbChangeItemIndex.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0 Then
                    frmEvents.optChangeItemSet.Checked = True
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1 Then
                    frmEvents.optChangeItemAdd.Checked = True
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2 Then
                    frmEvents.optChangeItemRemove.Checked = True
                End If
                frmEvents.nudChangeItemsAmount.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraChangeItems.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvChangeLevel
                IsEdit = True
                frmEvents.nudChangeLevel.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraChangeLevel.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvChangeSkills
                IsEdit = True
                frmEvents.cmbChangeSkills.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0 Then
                    frmEvents.optChangeSkillsAdd.Checked = True
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1 Then
                    frmEvents.optChangeSkillsRemove.Checked = True
                End If
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraChangeSkills.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvChangeClass
                IsEdit = True
                frmEvents.cmbChangeClass.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraChangeClass.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvChangeSprite
                IsEdit = True
                frmEvents.nudChangeSprite.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraChangeSprite.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvChangeSex
                IsEdit = True
                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = 0 Then
                    frmEvents.optChangeSexMale.Checked = True
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = 1 Then
                    frmEvents.optChangeSexFemale.Checked = True
                End If
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraChangeGender.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvChangePk
                IsEdit = True

                frmEvents.cmbSetPK.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1

                frmEvents.fraDialogue.Visible = True
                frmEvents.fraChangePK.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvWarpPlayer
                IsEdit = True
                frmEvents.nudWPMap.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.nudWPX.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEvents.nudWPY.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                frmEvents.cmbWarpPlayerDir.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraPlayerWarp.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvSetMoveRoute
                IsEdit = True
                frmEvents.fraMoveRoute.Visible = True
                frmEvents.fraMoveRoute.BringToFront()
                frmEvents.lstMoveRoute.Items.Clear()
                frmEvents.cmbEvent.Items.Clear()
                ReDim ListOfEvents(Map.EventCount)
                ListOfEvents(0) = EditorEvent
                frmEvents.cmbEvent.Items.Add("This Event")
                frmEvents.cmbEvent.SelectedIndex = 0
                frmEvents.cmbEvent.Enabled = True
                For i = 1 To Map.EventCount
                    If i <> EditorEvent Then
                        frmEvents.cmbEvent.Items.Add(Trim$(Map.Events(i).Name))
                        x = x + 1
                        ListOfEvents(x) = i
                        If i = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 Then frmEvents.cmbEvent.SelectedIndex = x
                    End If
                Next

                IsMoveRouteCommand = True
                frmEvents.chkIgnoreMove.Checked = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEvents.chkRepeatRoute.Checked = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                TempMoveRouteCount = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRouteCount
                TempMoveRoute = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRoute
                For i = 1 To TempMoveRouteCount
                    Select Case TempMoveRoute(i).Index
                        Case 1
                            frmEvents.lstMoveRoute.Items.Add("Move Up")
                        Case 2
                            frmEvents.lstMoveRoute.Items.Add("Move Down")
                        Case 3
                            frmEvents.lstMoveRoute.Items.Add("Move Left")
                        Case 4
                            frmEvents.lstMoveRoute.Items.Add("Move Right")
                        Case 5
                            frmEvents.lstMoveRoute.Items.Add("Move Randomly")
                        Case 6
                            frmEvents.lstMoveRoute.Items.Add("Move Towards Player")
                        Case 7
                            frmEvents.lstMoveRoute.Items.Add("Move Away From Player")
                        Case 8
                            frmEvents.lstMoveRoute.Items.Add("Step Forward")
                        Case 9
                            frmEvents.lstMoveRoute.Items.Add("Step Back")
                        Case 10
                            frmEvents.lstMoveRoute.Items.Add("Wait 100ms")
                        Case 11
                            frmEvents.lstMoveRoute.Items.Add("Wait 500ms")
                        Case 12
                            frmEvents.lstMoveRoute.Items.Add("Wait 1000ms")
                        Case 13
                            frmEvents.lstMoveRoute.Items.Add("Turn Up")
                        Case 14
                            frmEvents.lstMoveRoute.Items.Add("Turn Down")
                        Case 15
                            frmEvents.lstMoveRoute.Items.Add("Turn Left")
                        Case 16
                            frmEvents.lstMoveRoute.Items.Add("Turn Right")
                        Case 17
                            frmEvents.lstMoveRoute.Items.Add("Turn 90 Degrees To the Right")
                        Case 18
                            frmEvents.lstMoveRoute.Items.Add("Turn 90 Degrees To the Left")
                        Case 19
                            frmEvents.lstMoveRoute.Items.Add("Turn Around 180 Degrees")
                        Case 20
                            frmEvents.lstMoveRoute.Items.Add("Turn Randomly")
                        Case 21
                            frmEvents.lstMoveRoute.Items.Add("Turn Towards Player")
                        Case 22
                            frmEvents.lstMoveRoute.Items.Add("Turn Away from Player")
                        Case 23
                            frmEvents.lstMoveRoute.Items.Add("Set Speed 8x Slower")
                        Case 24
                            frmEvents.lstMoveRoute.Items.Add("Set Speed 4x Slower")
                        Case 25
                            frmEvents.lstMoveRoute.Items.Add("Set Speed 2x Slower")
                        Case 26
                            frmEvents.lstMoveRoute.Items.Add("Set Speed to Normal")
                        Case 27
                            frmEvents.lstMoveRoute.Items.Add("Set Speed 2x Faster")
                        Case 28
                            frmEvents.lstMoveRoute.Items.Add("Set Speed 4x Faster")
                        Case 29
                            frmEvents.lstMoveRoute.Items.Add("Set Frequency Lowest")
                        Case 30
                            frmEvents.lstMoveRoute.Items.Add("Set Frequency Lower")
                        Case 31
                            frmEvents.lstMoveRoute.Items.Add("Set Frequency Normal")
                        Case 32
                            frmEvents.lstMoveRoute.Items.Add("Set Frequency Higher")
                        Case 33
                            frmEvents.lstMoveRoute.Items.Add("Set Frequency Highest")
                        Case 34
                            frmEvents.lstMoveRoute.Items.Add("Turn On Walking Animation")
                        Case 35
                            frmEvents.lstMoveRoute.Items.Add("Turn Off Walking Animation")
                        Case 36
                            frmEvents.lstMoveRoute.Items.Add("Turn On Fixed Direction")
                        Case 37
                            frmEvents.lstMoveRoute.Items.Add("Turn Off Fixed Direction")
                        Case 38
                            frmEvents.lstMoveRoute.Items.Add("Turn On Walk Through")
                        Case 39
                            frmEvents.lstMoveRoute.Items.Add("Turn Off Walk Through")
                        Case 40
                            frmEvents.lstMoveRoute.Items.Add("Set Position Below Player")
                        Case 41
                            frmEvents.lstMoveRoute.Items.Add("Set Position at Player Level")
                        Case 42
                            frmEvents.lstMoveRoute.Items.Add("Set Position Above Player")
                        Case 43
                            frmEvents.lstMoveRoute.Items.Add("Set Graphic")
                    End Select
                Next
                frmEvents.fraMoveRoute.Width = 841
                frmEvents.fraMoveRoute.Height = 636
                frmEvents.fraMoveRoute.Visible = True
                frmEvents.fraDialogue.Visible = False
                frmEvents.fraCommands.Visible = False
            Case EventType.EvPlayAnimation
                IsEdit = True
                frmEvents.lblPlayAnimX.Visible = False
                frmEvents.lblPlayAnimY.Visible = False
                frmEvents.nudPlayAnimTileX.Visible = False
                frmEvents.nudPlayAnimTileY.Visible = False
                frmEvents.cmbPlayAnimEvent.Visible = False
                frmEvents.cmbPlayAnim.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEvents.cmbPlayAnimEvent.Items.Clear()
                For i = 1 To Map.EventCount
                    frmEvents.cmbPlayAnimEvent.Items.Add(i & ". " & Trim$(Map.Events(i).Name))
                Next
                frmEvents.cmbPlayAnimEvent.SelectedIndex = 0
                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0 Then
                    frmEvents.cmbAnimTargetType.SelectedIndex = 0
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1 Then
                    frmEvents.cmbAnimTargetType.SelectedIndex = 1
                    frmEvents.cmbPlayAnimEvent.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 - 1
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2 Then
                    frmEvents.cmbAnimTargetType.SelectedIndex = 2
                    frmEvents.nudPlayAnimTileX.Maximum = Map.MaxX
                    frmEvents.nudPlayAnimTileY.Maximum = Map.MaxY
                    frmEvents.nudPlayAnimTileX.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                    frmEvents.nudPlayAnimTileY.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4
                End If
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraPlayAnimation.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvCustomScript
                IsEdit = True
                frmEvents.nudCustomScript.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraCustomScript.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvPlayBgm
                IsEdit = True
                For i = 1 To UBound(MusicCache)
                    If MusicCache(i) = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 Then
                        frmEvents.cmbPlayBGM.SelectedIndex = i - 1
                    End If
                Next
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraPlayBGM.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvPlaySound
                IsEdit = True
                For i = 1 To UBound(SoundCache)
                    If SoundCache(i) = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 Then
                        frmEvents.cmbPlaySound.SelectedIndex = i - 1
                    End If
                Next
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraPlaySound.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvOpenShop
                IsEdit = True
                frmEvents.cmbOpenShop.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraOpenShop.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvSetAccess
                IsEdit = True
                frmEvents.cmbSetAccess.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraSetAccess.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvGiveExp
                IsEdit = True
                frmEvents.nudGiveExp.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraGiveExp.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvShowChatBubble
                IsEdit = True
                frmEvents.txtChatbubbleText.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                frmEvents.cmbChatBubbleTargetType.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEvents.cmbChatBubbleTarget.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 - 1

                frmEvents.fraDialogue.Visible = True
                frmEvents.fraShowChatBubble.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvLabel
                IsEdit = True
                frmEvents.txtLabelName.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraCreateLabel.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvGotoLabel
                IsEdit = True
                frmEvents.txtGotoLabel.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraGoToLabel.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvSpawnNpc
                IsEdit = True
                frmEvents.cmbSpawnNpc.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraSpawnNpc.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvSetFog
                IsEdit = True
                frmEvents.nudFogData0.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.nudFogData1.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEvents.nudFogData2.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraSetFog.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvSetWeather
                IsEdit = True
                frmEvents.CmbWeather.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.nudWeatherIntensity.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraSetWeather.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvSetTint
                IsEdit = True
                frmEvents.nudMapTintData0.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.nudMapTintData1.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEvents.nudMapTintData2.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                frmEvents.nudMapTintData3.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraMapTint.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvWait
                IsEdit = True
                frmEvents.nudWaitAmount.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraSetWait.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvBeginQuest
                IsEdit = True
                frmEvents.cmbBeginQuest.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraBeginQuest.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvEndQuest
                IsEdit = True
                frmEvents.cmbEndQuest.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraEndQuest.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvQuestTask
                IsEdit = True
                frmEvents.cmbCompleteQuest.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.nudCompleteQuestTask.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraCompleteTask.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvShowPicture
                IsEdit = True
                frmEvents.cmbPicIndex.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.nudShowPicture.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2

                frmEvents.cmbPicLoc.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 - 1

                frmEvents.nudPicOffsetX.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4
                frmEvents.nudPicOffsetY.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data5
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraShowPic.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvHidePicture
                IsEdit = True
                frmEvents.nudHidePic.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraHidePic.Visible = True
                frmEvents.fraCommands.Visible = False
            Case EventType.EvWaitMovement
                IsEdit = True
                frmEvents.fraDialogue.Visible = True
                frmEvents.fraMoveRouteWait.Visible = True
                frmEvents.fraCommands.Visible = False
                frmEvents.cmbMoveWait.Items.Clear()
                ReDim ListOfEvents(Map.EventCount)
                ListOfEvents(0) = EditorEvent
                frmEvents.cmbMoveWait.Items.Add("This Event")
                frmEvents.cmbMoveWait.SelectedIndex = 0
                For i = 1 To Map.EventCount
                    If i <> EditorEvent Then
                        frmEvents.cmbMoveWait.Items.Add(Trim$(Map.Events(i).Name))
                        x = x + 1
                        ListOfEvents(x) = i
                        If i = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 Then frmEvents.cmbMoveWait.SelectedIndex = x
                    End If
                Next
        End Select

    End Sub

    Friend Sub DeleteEventCommand()
        Dim i As Integer, x As Integer, curlist As Integer, curslot As Integer, p As Integer, oldCommandList As CommandListRec

        i = frmEvents.lstCommands.SelectedIndex
        If i = -1 Then Exit Sub
        If i > UBound(EventList) Then Exit Sub
        curlist = EventList(i).CommandList
        curslot = EventList(i).CommandNum
        If curlist = 0 Then Exit Sub
        If curslot = 0 Then Exit Sub
        If curlist > TmpEvent.Pages(CurPageNum).CommandListCount Then Exit Sub
        If curslot > TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then Exit Sub
        If curslot = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then
            TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount - 1
            p = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
            If p <= 0 Then
                ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(0)
            Else
                oldCommandList = TmpEvent.Pages(CurPageNum).CommandList(curlist)
                ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(p)
                x = 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).ParentList = oldCommandList.ParentList
                TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = p
                For i = 1 To p + 1
                    If i <> curslot Then
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(x) = oldCommandList.Commands(i)
                        x = x + 1
                    End If
                Next
            End If
        Else
            TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount - 1
            p = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
            oldCommandList = TmpEvent.Pages(CurPageNum).CommandList(curlist)
            x = 1
            If p <= 0 Then
                ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(0)
            Else
                ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(p)
                TmpEvent.Pages(CurPageNum).CommandList(curlist).ParentList = oldCommandList.ParentList
                TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = p
                For i = 1 To p + 1
                    If i <> curslot Then
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(x) = oldCommandList.Commands(i)
                        x = x + 1
                    End If
                Next
            End If
        End If
        EventListCommands()

    End Sub

    Friend Sub ClearEventCommands()

        ReDim TmpEvent.Pages(CurPageNum).CommandList(1)
        TmpEvent.Pages(CurPageNum).CommandListCount = 1
        EventListCommands()

    End Sub

    Friend Sub EditCommand()
        Dim i As Integer, curlist As Integer, curslot As Integer

        i = frmEvents.lstCommands.SelectedIndex
        If i = -1 Then Exit Sub
        If i > UBound(EventList) Then Exit Sub

        curlist = EventList(i).CommandList
        curslot = EventList(i).CommandNum
        If curlist = 0 Then Exit Sub
        If curslot = 0 Then Exit Sub
        If curlist > TmpEvent.Pages(CurPageNum).CommandListCount Then Exit Sub
        If curslot > TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then Exit Sub
        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index
            Case EventType.EvAddText
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtAddText_Text.Text
                'tmpEvent.Pages(curPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Events.scrlAddText_Colour.Value
                If frmEvents.optAddText_Player.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEvents.optAddText_Map.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                ElseIf frmEvents.optAddText_Global.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                End If
            Case EventType.EvCondition
                If frmEvents.optCondition0.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 0
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_PlayerVarIndex.SelectedIndex + 1
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEvents.cmbCondition_PlayerVarCompare.SelectedIndex
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3 = frmEvents.nudCondition_PlayerVarCondition.Value
                ElseIf frmEvents.optCondition1.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 1
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_PlayerSwitch.SelectedIndex + 1
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEvents.cmbCondtion_PlayerSwitchCondition.SelectedIndex
                ElseIf frmEvents.optCondition2.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 2
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_HasItem.SelectedIndex + 1
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEvents.nudCondition_HasItem.Value
                ElseIf frmEvents.optCondition3.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 3
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_ClassIs.SelectedIndex + 1
                ElseIf frmEvents.optCondition4.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 4
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_LearntSkill.SelectedIndex + 1
                ElseIf frmEvents.optCondition5.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 5
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.nudCondition_LevelAmount.Value
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEvents.cmbCondition_LevelCompare.SelectedIndex
                ElseIf frmEvents.optCondition6.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 6
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_SelfSwitch.SelectedIndex
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEvents.cmbCondition_SelfSwitchCondition.SelectedIndex
                ElseIf frmEvents.optCondition7.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 7
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.nudCondition_Quest.Value
                    If frmEvents.optCondition_Quest0.Checked Then
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = 0
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3 = frmEvents.cmbCondition_General.SelectedIndex
                    Else
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = 1
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3 = frmEvents.nudCondition_QuestTask.Value
                    End If
                ElseIf frmEvents.optCondition8.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 8
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_Gender.SelectedIndex
                ElseIf frmEvents.optCondition9.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 9
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEvents.cmbCondition_Time.SelectedIndex
                End If
            Case EventType.EvShowText
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtShowText.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudShowTextFace.Value
            Case EventType.EvShowChoices
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtChoicePrompt.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text2 = frmEvents.txtChoices1.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text3 = frmEvents.txtChoices2.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text4 = frmEvents.txtChoices3.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text5 = frmEvents.txtChoices4.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data5 = frmEvents.nudShowChoicesFace.Value
            Case EventType.EvPlayerVar
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbVariable.SelectedIndex + 1
                If frmEvents.optVariableAction0.Checked = True Then i = 0
                If frmEvents.optVariableAction1.Checked = True Then i = 1
                If frmEvents.optVariableAction2.Checked = True Then i = 2
                If frmEvents.optVariableAction3.Checked = True Then i = 3
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = i
                If i = 0 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudVariableData0.Value
                ElseIf i = 1 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudVariableData1.Value
                ElseIf i = 2 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudVariableData2.Value
                ElseIf i = 3 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudVariableData3.Value
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEvents.nudVariableData4.Value
                End If
            Case EventType.EvPlayerSwitch
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbSwitch.SelectedIndex + 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.cmbPlayerSwitchSet.SelectedIndex
            Case EventType.EvSelfSwitch
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbSetSelfSwitch.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.cmbSetSelfSwitchTo.SelectedIndex
            Case EventType.EvChangeItems
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbChangeItemIndex.SelectedIndex + 1
                If frmEvents.optChangeItemSet.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEvents.optChangeItemAdd.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                ElseIf frmEvents.optChangeItemRemove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                End If
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudChangeItemsAmount.Value
            Case EventType.EvChangeLevel
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudChangeLevel.Value
            Case EventType.EvChangeSkills
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbChangeSkills.SelectedIndex + 1
                If frmEvents.optChangeSkillsAdd.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEvents.optChangeSkillsRemove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                End If
            Case EventType.EvChangeClass
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbChangeClass.SelectedIndex + 1
            Case EventType.EvChangeSprite
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudChangeSprite.Value
            Case EventType.EvChangeSex
                If frmEvents.optChangeSexMale.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = 0
                ElseIf frmEvents.optChangeSexFemale.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = 1
                End If
            Case EventType.EvChangePk
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbSetPK.SelectedIndex

            Case EventType.EvWarpPlayer
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudWPMap.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudWPX.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudWPY.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEvents.cmbWarpPlayerDir.SelectedIndex
            Case EventType.EvSetMoveRoute
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = ListOfEvents(frmEvents.cmbEvent.SelectedIndex)
                If frmEvents.chkIgnoreMove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                Else
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                End If

                If frmEvents.chkRepeatRoute.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = 1
                Else
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = 0
                End If
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRouteCount = TempMoveRouteCount
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRoute = TempMoveRoute
            Case EventType.EvPlayAnimation
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbPlayAnim.SelectedIndex + 1
                If frmEvents.cmbAnimTargetType.SelectedIndex = 0 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEvents.cmbAnimTargetType.SelectedIndex = 1 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.cmbPlayAnimEvent.SelectedIndex + 1
                ElseIf frmEvents.cmbAnimTargetType.SelectedIndex = 2 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudPlayAnimTileX.Value
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEvents.nudPlayAnimTileY.Value
                End If
            Case EventType.EvCustomScript
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudCustomScript.Value
            Case EventType.EvPlayBgm
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = MusicCache(frmEvents.cmbPlayBGM.SelectedIndex + 1)
            Case EventType.EvPlaySound
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = SoundCache(frmEvents.cmbPlaySound.SelectedIndex + 1)
            Case EventType.EvOpenShop
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbOpenShop.SelectedIndex + 1
            Case EventType.EvSetAccess
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbSetAccess.SelectedIndex
            Case EventType.EvGiveExp
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudGiveExp.Value
            Case EventType.EvShowChatBubble
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtChatbubbleText.Text

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbChatBubbleTargetType.SelectedIndex + 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.cmbChatBubbleTarget.SelectedIndex + 1

            Case EventType.EvLabel
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtLabelName.Text
            Case EventType.EvGotoLabel
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEvents.txtGotoLabel.Text
            Case EventType.EvSpawnNpc
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbSpawnNpc.SelectedIndex + 1
            Case EventType.EvSetFog
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudFogData0.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudFogData1.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudFogData2.Value
            Case EventType.EvSetWeather
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.CmbWeather.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudWeatherIntensity.Value
            Case EventType.EvSetTint
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudMapTintData0.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudMapTintData1.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.nudMapTintData2.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEvents.nudMapTintData3.Value
            Case EventType.EvWait
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudWaitAmount.Value
            Case EventType.EvBeginQuest
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbBeginQuest.SelectedIndex
            Case EventType.EvEndQuest
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbEndQuest.SelectedIndex
            Case EventType.EvQuestTask
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbCompleteQuest.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudCompleteQuestTask.Value
            Case EventType.EvShowPicture
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.cmbPicIndex.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEvents.nudShowPicture.Value

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEvents.cmbPicLoc.SelectedIndex + 1

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEvents.nudPicOffsetX.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data5 = frmEvents.nudPicOffsetY.Value
            Case EventType.EvHidePicture
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEvents.nudHidePic.Value
            Case EventType.EvWaitMovement
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = ListOfEvents(frmEvents.cmbMoveWait.SelectedIndex)
        End Select
        EventListCommands()

    End Sub

    Sub RequestSwitchesAndVariables()
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestSwitchesAndVariables)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Sub SendSwitchesAndVariables()
        Dim i As Integer
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSwitchesAndVariables)
        For i = 1 To MaxSwitches
            buffer.WriteString((Trim$(Switches(i))))
        Next
        For i = 1 To MaxVariables
            buffer.WriteString((Trim$(Variables(i))))
        Next
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

#End Region

#Region "Incoming Packets"

    Sub Packet_SpawnEvent(ByRef data() As Byte)
        Dim id As Integer
        Dim buffer As New ByteStream(data)
        id = buffer.ReadInt32
        If id > Map.CurrentEvents Then
            Map.CurrentEvents = id
            ReDim Preserve Map.MapEvents(Map.CurrentEvents)
        End If

        With Map.MapEvents(id)
            .Name = buffer.ReadString
            .Dir = buffer.ReadInt32
            .ShowDir = .Dir
            .GraphicNum = buffer.ReadInt32
            .GraphicType = buffer.ReadInt32
            .GraphicX = buffer.ReadInt32
            .GraphicX2 = buffer.ReadInt32
            .GraphicY = buffer.ReadInt32
            .GraphicY2 = buffer.ReadInt32
            .MovementSpeed = buffer.ReadInt32
            .Moving = 0
            .X = buffer.ReadInt32
            .Y = buffer.ReadInt32
            .XOffset = 0
            .YOffset = 0
            .Position = buffer.ReadInt32
            .Visible = buffer.ReadInt32
            .WalkAnim = buffer.ReadInt32
            .DirFix = buffer.ReadInt32
            .WalkThrough = buffer.ReadInt32
            .ShowName = buffer.ReadInt32
            .Questnum = buffer.ReadInt32
        End With
        buffer.Dispose()

    End Sub

    Sub Packet_EventMove(ByRef data() As Byte)
        Dim id As Integer
        Dim x As Integer
        Dim y As Integer
        Dim dir As Integer, showDir As Integer
        Dim movementSpeed As Integer
        Dim buffer As New ByteStream(data)
        id = buffer.ReadInt32
        x = buffer.ReadInt32
        y = buffer.ReadInt32
        dir = buffer.ReadInt32
        showDir = buffer.ReadInt32
        movementSpeed = buffer.ReadInt32
        If id > Map.CurrentEvents Then Exit Sub

        With Map.MapEvents(id)
            .X = x
            .Y = y
            .Dir = dir
            .XOffset = 0
            .YOffset = 0
            .Moving = 1
            .ShowDir = showDir
            .MovementSpeed = movementSpeed

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

    Sub Packet_EventDir(ByRef data() As Byte)
        Dim i As Integer
        Dim dir As Byte
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32
        dir = buffer.ReadInt32
        If i > Map.CurrentEvents Then Exit Sub

        With Map.MapEvents(i)
            .Dir = dir
            .ShowDir = dir
            .XOffset = 0
            .YOffset = 0
            .Moving = 0
        End With

    End Sub

    Sub Packet_SwitchesAndVariables(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        For i = 1 To MaxSwitches
            Switches(i) = buffer.ReadString
        Next
        For i = 1 To MaxVariables
            Variables(i) = buffer.ReadString
        Next

        buffer.Dispose()

    End Sub

    Sub Packet_MapEventData(ByRef data() As Byte)
        Dim i As Integer, x As Integer, y As Integer, z As Integer, w As Integer

        Dim buffer As New ByteStream(data)
        'Event Data!
        Map.EventCount = buffer.ReadInt32
        If Map.EventCount > 0 Then
            ReDim Map.Events(Map.EventCount)
            For i = 1 To Map.EventCount
                With Map.Events(i)
                    .Name = buffer.ReadString
                    .Globals = buffer.ReadInt32
                    .X = buffer.ReadInt32
                    .Y = buffer.ReadInt32
                    .PageCount = buffer.ReadInt32
                End With
                If Map.Events(i).PageCount > 0 Then
                    ReDim Map.Events(i).Pages(Map.Events(i).PageCount)
                    For x = 1 To Map.Events(i).PageCount
                        With Map.Events(i).Pages(x)
                            .ChkVariable = buffer.ReadInt32
                            .Variableindex = buffer.ReadInt32
                            .VariableCondition = buffer.ReadInt32
                            .VariableCompare = buffer.ReadInt32
                            .ChkSwitch = buffer.ReadInt32
                            .Switchindex = buffer.ReadInt32
                            .SwitchCompare = buffer.ReadInt32
                            .ChkHasItem = buffer.ReadInt32
                            .HasItemindex = buffer.ReadInt32
                            .HasItemAmount = buffer.ReadInt32
                            .ChkSelfSwitch = buffer.ReadInt32
                            .SelfSwitchindex = buffer.ReadInt32
                            .SelfSwitchCompare = buffer.ReadInt32
                            .GraphicType = buffer.ReadInt32
                            .Graphic = buffer.ReadInt32
                            .GraphicX = buffer.ReadInt32
                            .GraphicY = buffer.ReadInt32
                            .GraphicX2 = buffer.ReadInt32
                            .GraphicY2 = buffer.ReadInt32
                            .MoveType = buffer.ReadInt32
                            .MoveSpeed = buffer.ReadInt32
                            .MoveFreq = buffer.ReadInt32
                            .MoveRouteCount = buffer.ReadInt32
                            .IgnoreMoveRoute = buffer.ReadInt32
                            .RepeatMoveRoute = buffer.ReadInt32
                            If .MoveRouteCount > 0 Then
                                ReDim Map.Events(i).Pages(x).MoveRoute(.MoveRouteCount)
                                For y = 1 To .MoveRouteCount
                                    .MoveRoute(y).Index = buffer.ReadInt32
                                    .MoveRoute(y).Data1 = buffer.ReadInt32
                                    .MoveRoute(y).Data2 = buffer.ReadInt32
                                    .MoveRoute(y).Data3 = buffer.ReadInt32
                                    .MoveRoute(y).Data4 = buffer.ReadInt32
                                    .MoveRoute(y).Data5 = buffer.ReadInt32
                                    .MoveRoute(y).Data6 = buffer.ReadInt32
                                Next
                            End If
                            .WalkAnim = buffer.ReadInt32
                            .DirFix = buffer.ReadInt32
                            .WalkThrough = buffer.ReadInt32
                            .ShowName = buffer.ReadInt32
                            .Trigger = buffer.ReadInt32
                            .CommandListCount = buffer.ReadInt32
                            .Position = buffer.ReadInt32
                            .Questnum = buffer.ReadInt32
                        End With
                        If Map.Events(i).Pages(x).CommandListCount > 0 Then
                            ReDim Map.Events(i).Pages(x).CommandList(Map.Events(i).Pages(x).CommandListCount)
                            For y = 1 To Map.Events(i).Pages(x).CommandListCount
                                Map.Events(i).Pages(x).CommandList(y).CommandCount = buffer.ReadInt32
                                Map.Events(i).Pages(x).CommandList(y).ParentList = buffer.ReadInt32
                                If Map.Events(i).Pages(x).CommandList(y).CommandCount > 0 Then
                                    ReDim Map.Events(i).Pages(x).CommandList(y).Commands(Map.Events(i).Pages(x).CommandList(y).CommandCount)
                                    For z = 1 To Map.Events(i).Pages(x).CommandList(y).CommandCount
                                        With Map.Events(i).Pages(x).CommandList(y).Commands(z)
                                            .Index = buffer.ReadInt32
                                            .Text1 = buffer.ReadString
                                            .Text2 = buffer.ReadString
                                            .Text3 = buffer.ReadString
                                            .Text4 = buffer.ReadString
                                            .Text5 = buffer.ReadString
                                            .Data1 = buffer.ReadInt32
                                            .Data2 = buffer.ReadInt32
                                            .Data3 = buffer.ReadInt32
                                            .Data4 = buffer.ReadInt32
                                            .Data5 = buffer.ReadInt32
                                            .Data6 = buffer.ReadInt32
                                            .ConditionalBranch.CommandList = buffer.ReadInt32
                                            .ConditionalBranch.Condition = buffer.ReadInt32
                                            .ConditionalBranch.Data1 = buffer.ReadInt32
                                            .ConditionalBranch.Data2 = buffer.ReadInt32
                                            .ConditionalBranch.Data3 = buffer.ReadInt32
                                            .ConditionalBranch.ElseCommandList = buffer.ReadInt32
                                            .MoveRouteCount = buffer.ReadInt32
                                            If .MoveRouteCount > 0 Then
                                                ReDim Preserve .MoveRoute(.MoveRouteCount)
                                                For w = 1 To .MoveRouteCount
                                                    .MoveRoute(w).Index = buffer.ReadInt32
                                                    .MoveRoute(w).Data1 = buffer.ReadInt32
                                                    .MoveRoute(w).Data2 = buffer.ReadInt32
                                                    .MoveRoute(w).Data3 = buffer.ReadInt32
                                                    .MoveRoute(w).Data4 = buffer.ReadInt32
                                                    .MoveRoute(w).Data5 = buffer.ReadInt32
                                                    .MoveRoute(w).Data6 = buffer.ReadInt32
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
        buffer.Dispose()

    End Sub

    Sub Packet_EventChat(ByRef data() As Byte)
        Dim i As Integer
        Dim choices As Integer
        Dim buffer As New ByteStream(data)
        EventReplyId = buffer.ReadInt32
        EventReplyPage = buffer.ReadInt32
        EventChatFace = buffer.ReadInt32
        EventText = buffer.ReadString
        If EventText = "" Then EventText = " "
        EventChat = True
        ShowEventLbl = True
        choices = buffer.ReadInt32
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
                EventChoices(i) = buffer.ReadString
                EventChoiceVisible(i) = True
            Next
        End If
        AnotherChat = buffer.ReadInt32

        buffer.Dispose()

    End Sub

    Sub Packet_EventStart(ByRef data() As Byte)
        InEvent = True
    End Sub

    Sub Packet_EventEnd(ByRef data() As Byte)
        InEvent = False
    End Sub

    Sub Packet_HoldPlayer(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        If buffer.ReadInt32 = 0 Then
            HoldPlayer = True
        Else
            HoldPlayer = False
        End If

        buffer.Dispose()

    End Sub

#End Region

#Region "Drawing..."

    Friend Sub EditorEvent_DrawGraphic()
        Dim sRect As Rect
        Dim dRect As Rect
        Dim targetBitmap As Bitmap 'Bitmap we draw to
        Dim sourceBitmap As Bitmap 'This is our sprite or tileset that we are drawing from
        Dim g As Graphics 'This is our graphics class that helps us draw to the targetBitmap

        If frmEvents.picGraphicSel.Visible Then
            Select Case frmEvents.cmbGraphic.SelectedIndex
                Case 0
                    'None
                    frmEvents.picGraphicSel.BackgroundImage = Nothing
                Case 1
                    If frmEvents.nudGraphic.Value > 0 AndAlso frmEvents.nudGraphic.Value <= NumCharacters Then
                        'Load character from Contents into our sourceBitmap
                        sourceBitmap = New Bitmap(Application.StartupPath & "/Data/graphics/characters/" & frmEvents.nudGraphic.Value & ".png")
                        targetBitmap = New Bitmap(sourceBitmap.Width, sourceBitmap.Height) 'Create our target Bitmap

                        g = Graphics.FromImage(targetBitmap)

                        Dim sourceRect As New Rectangle(0, 0, sourceBitmap.Width / 4, sourceBitmap.Height / 4)  'This is the section we are pulling from the source graphic
                        Dim destRect As New Rectangle(0, 0, targetBitmap.Width, targetBitmap.Height)     'This is the rectangle in the target graphic we want to render to

                        g.DrawImage(sourceBitmap, destRect, sourceRect, GraphicsUnit.Pixel)

                        g.DrawRectangle(Pens.Red, New Rectangle(GraphicSelX * PIC_X, GraphicSelY * PIC_Y, GraphicSelX2 * PIC_X, GraphicSelY2 * PIC_Y))

                        g.Dispose()

                        frmEvents.picGraphicSel.Width = targetBitmap.Width
                        frmEvents.picGraphicSel.Height = targetBitmap.Height
                        frmEvents.picGraphicSel.Visible = True
                        frmEvents.picGraphicSel.BackgroundImage = targetBitmap
                        frmEvents.picGraphic.BackgroundImage = targetBitmap
                    Else
                        frmEvents.picGraphicSel.BackgroundImage = Nothing
                        Exit Sub
                    End If
                Case 2
                    If frmEvents.nudGraphic.Value > 0 AndAlso frmEvents.nudGraphic.Value <= NumTileSets Then
                        'Load tilesheet from Contents into our sourceBitmap
                        sourceBitmap = New Bitmap(Application.StartupPath & "/Data/graphics/tilesets/" & frmEvents.nudGraphic.Value & ".png")
                        targetBitmap = New Bitmap(sourceBitmap.Width, sourceBitmap.Height) 'Create our target Bitmap

                        If TmpEvent.Pages(CurPageNum).GraphicX2 = 0 AndAlso TmpEvent.Pages(CurPageNum).GraphicY2 = 0 Then
                            sRect.Top = TmpEvent.Pages(CurPageNum).GraphicY * 32
                            sRect.Left = TmpEvent.Pages(CurPageNum).GraphicX * 32
                            sRect.Bottom = sRect.Top + 32
                            sRect.Right = sRect.Left + 32

                            With dRect
                                dRect.Top = (193 / 2) - ((sRect.Bottom - sRect.Top) / 2)
                                dRect.Bottom = dRect.Top + (sRect.Bottom - sRect.Top)
                                dRect.Left = (120 / 2) - ((sRect.Right - sRect.Left) / 2)
                                dRect.Right = dRect.Left + (sRect.Right - sRect.Left)
                            End With
                        Else
                            sRect.Top = TmpEvent.Pages(CurPageNum).GraphicY * 32
                            sRect.Left = TmpEvent.Pages(CurPageNum).GraphicX * 32
                            sRect.Bottom = sRect.Top + ((TmpEvent.Pages(CurPageNum).GraphicY2 - TmpEvent.Pages(CurPageNum).GraphicY) * 32)
                            sRect.Right = sRect.Left + ((TmpEvent.Pages(CurPageNum).GraphicX2 - TmpEvent.Pages(CurPageNum).GraphicX) * 32)

                            With dRect
                                dRect.Top = (193 / 2) - ((sRect.Bottom - sRect.Top) / 2)
                                dRect.Bottom = dRect.Top + (sRect.Bottom - sRect.Top)
                                dRect.Left = (120 / 2) - ((sRect.Right - sRect.Left) / 2)
                                dRect.Right = dRect.Left + (sRect.Right - sRect.Left)
                            End With

                        End If

                        g = Graphics.FromImage(targetBitmap)

                        Dim sourceRect As New Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height)  'This is the section we are pulling from the source graphic
                        Dim destRect As New Rectangle(0, 0, targetBitmap.Width, targetBitmap.Height)     'This is the rectangle in the target graphic we want to render to

                        g.DrawImage(sourceBitmap, destRect, sourceRect, GraphicsUnit.Pixel)

                        g.DrawRectangle(Pens.Red, New Rectangle(GraphicSelX * PIC_X, GraphicSelY * PIC_Y, (GraphicSelX2) * PIC_X, (GraphicSelY2) * PIC_Y))

                        g.Dispose()

                        frmEvents.picGraphicSel.Width = targetBitmap.Width
                        frmEvents.picGraphicSel.Height = targetBitmap.Height
                        frmEvents.picGraphicSel.Visible = True
                        frmEvents.picGraphicSel.BackgroundImage = targetBitmap
                        ' frmEditor_Events.pnlGraphicSelect.Width = targetBitmap.Width
                        'frmEditor_Events.pnlGraphicSelect.Height = targetBitmap.Height
                    Else
                        frmEvents.picGraphicSel.BackgroundImage = Nothing
                        Exit Sub
                    End If
            End Select
        Else
            If TmpEvent.PageCount > 0 Then
                Select Case TmpEvent.Pages(CurPageNum).GraphicType
                    Case 0
                        frmEvents.picGraphicSel.BackgroundImage = Nothing
                    Case 1
                        If TmpEvent.Pages(CurPageNum).Graphic > 0 AndAlso TmpEvent.Pages(CurPageNum).Graphic <= NumCharacters Then
                            'Load character from Contents into our sourceBitmap
                            sourceBitmap = New Bitmap(Application.StartupPath & GFX_PATH & "\characters\" & TmpEvent.Pages(CurPageNum).Graphic & ".png")
                            targetBitmap = New Bitmap(sourceBitmap.Width, sourceBitmap.Height) 'Create our target Bitmap

                            g = Graphics.FromImage(targetBitmap)

                            Dim sourceRect As New Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height)  'This is the section we are pulling from the source graphic
                            Dim destRect As New Rectangle(0, 0, targetBitmap.Width, targetBitmap.Height)     'This is the rectangle in the target graphic we want to render to

                            g.DrawImage(sourceBitmap, destRect, sourceRect, GraphicsUnit.Pixel)

                            g.Dispose()

                            frmEvents.picGraphic.Width = targetBitmap.Width
                            frmEvents.picGraphic.Height = targetBitmap.Height
                            frmEvents.picGraphic.BackgroundImage = targetBitmap
                        Else
                            frmEvents.picGraphic.BackgroundImage = Nothing
                            Exit Sub
                        End If
                    Case 2
                        If TmpEvent.Pages(CurPageNum).Graphic > 0 AndAlso TmpEvent.Pages(CurPageNum).Graphic <= NumTileSets Then
                            'Load tilesheet from Contents into our sourceBitmap
                            sourceBitmap = New Bitmap(Application.StartupPath & GFX_PATH & "tilesets\" & TmpEvent.Pages(CurPageNum).Graphic & ".png")
                            targetBitmap = New Bitmap(sourceBitmap.Width, sourceBitmap.Height) 'Create our target Bitmap

                            If TmpEvent.Pages(CurPageNum).GraphicX2 = 0 AndAlso TmpEvent.Pages(CurPageNum).GraphicY2 = 0 Then
                                sRect.Top = TmpEvent.Pages(CurPageNum).GraphicY * 32
                                sRect.Left = TmpEvent.Pages(CurPageNum).GraphicX * 32
                                sRect.Bottom = sRect.Top + 32
                                sRect.Right = sRect.Left + 32

                                With dRect
                                    dRect.Top = 0
                                    dRect.Bottom = PIC_Y
                                    dRect.Left = 0
                                    dRect.Right = PIC_X
                                End With
                            Else
                                sRect.Top = TmpEvent.Pages(CurPageNum).GraphicY * 32
                                sRect.Left = TmpEvent.Pages(CurPageNum).GraphicX * 32
                                sRect.Bottom = TmpEvent.Pages(CurPageNum).GraphicY2 * 32
                                sRect.Right = TmpEvent.Pages(CurPageNum).GraphicX2 * 32

                                With dRect
                                    dRect.Top = 0
                                    dRect.Bottom = sRect.Bottom
                                    dRect.Left = 0
                                    dRect.Right = sRect.Right
                                End With

                            End If

                            g = Graphics.FromImage(targetBitmap)

                            Dim sourceRect As New Rectangle(sRect.Left, sRect.Top, sRect.Right, sRect.Bottom)  'This is the section we are pulling from the source graphic
                            Dim destRect As New Rectangle(dRect.Left, dRect.Top, dRect.Right, dRect.Bottom)     'This is the rectangle in the target graphic we want to render to

                            g.DrawImage(sourceBitmap, destRect, sourceRect, GraphicsUnit.Pixel)

                            g.Dispose()

                            frmEvents.picGraphic.Width = targetBitmap.Width
                            frmEvents.picGraphic.Height = targetBitmap.Height
                            frmEvents.picGraphic.BackgroundImage = targetBitmap
                        End If
                End Select
            End If
        End If

    End Sub

    Friend Sub DrawEvents()
        Dim rec As Rectangle
        Dim width As Integer, height As Integer, i As Integer, x As Integer, y As Integer
        Dim tX As Integer
        Dim tY As Integer

        If Map.EventCount <= 0 Then Exit Sub
        For i = 1 To Map.EventCount
            width = 32
            height = 32
            x = Map.Events(i).X * 32
            y = Map.Events(i).Y * 32
            If Map.Events(i).PageCount <= 0 Then
                With rec
                    .Y = 0
                    .Height = PIC_Y
                    .X = 0
                    .Width = PIC_X
                End With

                Dim rec2 As New RectangleShape With {
                    .OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.Blue),
                    .OutlineThickness = 0.6,
                    .FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent),
                    .Size = New Vector2f(rec.Width, rec.Height),
                    .Position = New Vector2f(ConvertMapX(CurX * PIC_X), ConvertMapY(CurY * PIC_Y))
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
                    tX = ((x) - 4) + (PIC_X * 0.5)
                    tY = ((y) - 7) + (PIC_Y * 0.5)
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
                            .Y = (Map.Events(i).Pages(1).GraphicY * (CharacterGFXInfo(Map.Events(i).Pages(1).Graphic).height / 4))
                            .Height = .Y + PIC_Y
                            .X = (Map.Events(i).Pages(1).GraphicX * (CharacterGFXInfo(Map.Events(i).Pages(1).Graphic).width / 4))
                            .Width = .X + PIC_X
                        End With

                        Dim tmpSprite As Sprite = New Sprite(CharacterGFX(Map.Events(i).Pages(1).Graphic)) With {
                            .TextureRect = New IntRect(rec.X, rec.Y, rec.Width, rec.Height),
                            .Position = New Vector2f(ConvertMapX(Map.Events(i).X * PIC_X), ConvertMapY(Map.Events(i).Y * PIC_Y))
                        }
                        GameWindow.Draw(tmpSprite)
                    Else
                        With rec
                            .Y = 0
                            .Height = PIC_Y
                            .X = 0
                            .Width = PIC_X
                        End With

                        Dim rec2 As New RectangleShape With {
                            .OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.Blue),
                            .OutlineThickness = 0.6,
                            .FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent),
                            .Size = New Vector2f(rec.Width, rec.Height),
                            .Position = New Vector2f(ConvertMapX(CurX * PIC_X), ConvertMapY(CurY * PIC_Y))
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

                        Dim rec2 As New RectangleShape With {
                            .OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.Blue),
                            .OutlineThickness = 0.6,
                            .FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent),
                            .Size = New Vector2f(rec.Width, rec.Height),
                            .Position = New Vector2f(ConvertMapX(CurX * PIC_X), ConvertMapY(CurY * PIC_Y))
                        }
                        GameWindow.Draw(rec2)
                    End If
            End Select
nextevent:
        Next

    End Sub

    Friend Sub DrawEvent(id As Integer) ' draw on map, outside the editor
        Dim x As Integer, y As Integer, width As Integer, height As Integer, sRect As Rectangle, anim As Integer, spritetop As Integer

        If Map.MapEvents(id).Visible = 0 Then Exit Sub
        If InMapEditor Then Exit Sub
        Select Case Map.MapEvents(id).GraphicType
            Case 0
                Exit Sub
            Case 1
                If Map.MapEvents(id).GraphicNum <= 0 OrElse Map.MapEvents(id).GraphicNum > NumCharacters Then Exit Sub

                ' Reset frame
                If Map.MapEvents(id).Steps = 3 Then
                    anim = 0
                ElseIf Map.MapEvents(id).Steps = 1 Then
                    anim = 2
                End If

                Select Case Map.MapEvents(id).Dir
                    Case DirectionType.Up
                        If (Map.MapEvents(id).YOffset > 8) Then anim = Map.MapEvents(id).Steps
                    Case DirectionType.Down
                        If (Map.MapEvents(id).YOffset < -8) Then anim = Map.MapEvents(id).Steps
                    Case DirectionType.Left
                        If (Map.MapEvents(id).XOffset > 8) Then anim = Map.MapEvents(id).Steps
                    Case DirectionType.Right
                        If (Map.MapEvents(id).XOffset < -8) Then anim = Map.MapEvents(id).Steps
                End Select

                ' Set the left
                Select Case Map.MapEvents(id).ShowDir
                    Case DirectionType.Up
                        spritetop = 3
                    Case DirectionType.Right
                        spritetop = 2
                    Case DirectionType.Down
                        spritetop = 0
                    Case DirectionType.Left
                        spritetop = 1
                End Select

                If Map.MapEvents(id).WalkAnim = 1 Then anim = 0
                If Map.MapEvents(id).Moving = 0 Then anim = Map.MapEvents(id).GraphicX

                width = CharacterGFXInfo(Map.MapEvents(id).GraphicNum).width / 4
                height = CharacterGFXInfo(Map.MapEvents(id).GraphicNum).height / 4

                sRect = New Rectangle((anim) * (CharacterGFXInfo(Map.MapEvents(id).GraphicNum).width / 4), spritetop * (CharacterGFXInfo(Map.MapEvents(id).GraphicNum).height / 4), (CharacterGFXInfo(Map.MapEvents(id).GraphicNum).width / 4), (CharacterGFXInfo(Map.MapEvents(id).GraphicNum).height / 4))
                ' Calculate the X
                x = Map.MapEvents(id).X * PIC_X + Map.MapEvents(id).XOffset - ((CharacterGFXInfo(Map.MapEvents(id).GraphicNum).width / 4 - 32) / 2)

                ' Is the player's height more than 32..?
                If (CharacterGFXInfo(Map.MapEvents(id).GraphicNum).height * 4) > 32 Then
                    ' Create a 32 pixel offset for larger sprites
                    y = Map.MapEvents(id).Y * PIC_Y + Map.MapEvents(id).YOffset - ((CharacterGFXInfo(Map.MapEvents(id).GraphicNum).height / 4) - 32)
                Else
                    ' Proceed as normal
                    y = Map.MapEvents(id).Y * PIC_Y + Map.MapEvents(id).YOffset
                End If
                ' render the actual sprite
                DrawCharacter(Map.MapEvents(id).GraphicNum, x, y, sRect)
            Case 2
                If Map.MapEvents(id).GraphicNum < 1 OrElse Map.MapEvents(id).GraphicNum > NumTileSets Then Exit Sub
                If Map.MapEvents(id).GraphicY2 > 0 OrElse Map.MapEvents(id).GraphicX2 > 0 Then
                    With sRect
                        .X = Map.MapEvents(id).GraphicX * 32
                        .Y = Map.MapEvents(id).GraphicY * 32
                        .Width = Map.MapEvents(id).GraphicX2 * 32
                        .Height = Map.MapEvents(id).GraphicY2 * 32
                    End With
                Else
                    With sRect
                        .X = Map.MapEvents(id).GraphicY * 32
                        .Height = .Top + 32
                        .Y = Map.MapEvents(id).GraphicX * 32
                        .Width = .Left + 32
                    End With
                End If

                If TileSetTextureInfo(Map.MapEvents(id).GraphicNum).IsLoaded = False Then
                    LoadTexture(Map.MapEvents(id).GraphicNum, 1)
                End If
                ' we use it, lets update timer
                With TileSetTextureInfo(Map.MapEvents(id).GraphicNum)
                    .TextureTimer = GetTickCount() + 100000
                End With

                x = Map.MapEvents(id).X * 32
                y = Map.MapEvents(id).Y * 32
                x = x - ((sRect.Right - sRect.Left) / 2)
                y = y - (sRect.Bottom - sRect.Top) + 32

                If Map.MapEvents(id).GraphicY2 > 1 Then
                    RenderSprite(TileSetSprite(Map.MapEvents(id).GraphicNum), GameWindow, ConvertMapX(Map.MapEvents(id).X * PIC_X), ConvertMapY(Map.MapEvents(id).Y * PIC_Y) - PIC_Y, sRect.Left, sRect.Top, sRect.Width, sRect.Height)
                Else
                    RenderSprite(TileSetSprite(Map.MapEvents(id).GraphicNum), GameWindow, ConvertMapX(Map.MapEvents(id).X * PIC_X), ConvertMapY(Map.MapEvents(id).Y * PIC_Y), sRect.Left, sRect.Top, sRect.Width, sRect.Height)
                End If

        End Select

    End Sub

#End Region

#Region "Misc"

    Friend Function GetColorString(color As Integer)

        Select Case color
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

    Friend Sub ResetEventdata()
        For i = 0 To Map.EventCount
            ReDim Map.MapEvents(Map.EventCount)
            Map.CurrentEvents = 0
            With Map.MapEvents(i)
                .Name = ""
                .Dir = 0
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
                .Questnum = 0
            End With
        Next
    End Sub

#End Region

End Module