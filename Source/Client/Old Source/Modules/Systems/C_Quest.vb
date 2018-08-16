Imports ASFW
Imports SFML.Graphics

Friend Module C_Quest

#Region "Global Info"

    'Constants
    Friend Const MaxQuests As Integer = 250

    Friend Const EditorTasks As Byte = 7

    Friend Const QUEST_TYPE_GOSLAY As Byte = 1
    Friend Const QUEST_TYPE_GOGATHER As Byte = 2
    Friend Const QUEST_TYPE_GOTALK As Byte = 3
    Friend Const QUEST_TYPE_GOREACH As Byte = 4
    Friend Const QUEST_TYPE_GOGIVE As Byte = 5
    Friend Const QUEST_TYPE_GOKILL As Byte = 6
    Friend Const QUEST_TYPE_GOTRAIN As Byte = 7
    Friend Const QUEST_TYPE_GOGET As Byte = 8

    Friend Const QUEST_NOT_STARTED As Byte = 0
    Friend Const QUEST_STARTED As Byte = 1
    Friend Const QUEST_COMPLETED As Byte = 2
    Friend Const QUEST_COMPLETED_BUT As Byte = 3

    Friend QuestLogPage As Integer
    Friend QuestNames(MaxActivequests) As String

    Friend QuestChanged(MaxQuests) As Boolean

    Friend QuestEditorShow As Boolean

    'questlog variables

    Friend Const MaxActivequests = 10

    Friend QuestLogX As Integer = 150
    Friend QuestLogY As Integer = 100

    Friend PnlQuestLogVisible As Boolean
    Friend SelectedQuest As Integer
    Friend QuestTaskLogText As String = ""
    Friend ActualTaskText As String = ""
    Friend QuestDialogText As String = ""
    Friend QuestStatus2Text As String = ""
    Friend AbandonQuestText As String = ""
    Friend AbandonQuestVisible As Boolean
    Friend QuestRequirementsText As String = ""
    Friend QuestRewardsText() As String

    'here we store temp info because off UpdateUI >.<
    Friend UpdateQuestWindow As Boolean

    Friend UpdateQuestChat As Boolean
    Friend QuestNum As Integer
    Friend QuestNumForStart As Integer
    Friend QuestMessage As String
    Friend QuestAcceptTag As Integer

    'Types
    Friend Quest(MaxQuests) As QuestRec

    Friend Structure PlayerQuestRec
        Dim Status As Integer '0=not started, 1=started, 2=completed, 3=completed but repeatable
        Dim ActualTask As Integer
        Dim CurrentCount As Integer 'Used to handle the Amount property
    End Structure

    Friend Structure TaskRec
        Dim Order As Integer
        Dim Npc As Integer
        Dim Item As Integer
        Dim Map As Integer
        Dim Resource As Integer
        Dim Amount As Integer
        Dim Speech As String
        Dim TaskLog As String
        Dim QuestEnd As Byte
        Dim TaskType As Integer
    End Structure

    Friend Structure QuestRec
        Dim Name As String
        Dim QuestLog As String
        Dim Repeat As Byte
        Dim Cancelable As Byte

        Dim ReqCount As Integer
        Dim Requirement() As Integer '1=item, 2=quest, 3=class
        Dim RequirementIndex() As Integer

        Dim QuestGiveItem As Integer 'Todo: make this dynamic
        Dim QuestGiveItemValue As Integer
        Dim QuestRemoveItem As Integer
        Dim QuestRemoveItemValue As Integer

        Dim Chat() As String

        Dim RewardCount As Integer
        Dim RewardItem() As Integer
        Dim RewardItemAmount() As Integer
        Dim RewardExp As Integer

        Dim TaskCount As Integer
        Dim Task() As TaskRec
    End Structure

#End Region

#Region "Quest Editor"

    Friend Sub QuestEditorInit()

        If frmQuest.Visible = False Then Exit Sub
        Editorindex = frmQuest.lstIndex.SelectedIndex + 1

        With frmQuest
            .txtName.Text = Quest(Editorindex).Name.Trim

            If Quest(Editorindex).Repeat = 1 Then
                .chkRepeat.Checked = True
            Else
                .chkRepeat.Checked = False
            End If

            .txtStartText.Text = Quest(Editorindex).Chat(1).Trim
            .txtProgressText.Text = Quest(Editorindex).Chat(2).Trim
            .txtEndText.Text = Quest(Editorindex).Chat(3).Trim

            .cmbStartItem.Items.Clear()
            .cmbItemReq.Items.Clear()
            .cmbEndItem.Items.Clear()
            .cmbItemReward.Items.Clear()
            .cmbStartItem.Items.Add("None")
            .cmbItemReq.Items.Add("None")
            .cmbEndItem.Items.Add("None")
            .cmbItemReward.Items.Add("None")

            For i = 1 To MAX_ITEMS
                .cmbStartItem.Items.Add(i & ": " & Item(i).Name)
                .cmbItemReq.Items.Add(i & ": " & Item(i).Name)
                .cmbEndItem.Items.Add(i & ": " & Item(i).Name)
                .cmbItemReward.Items.Add(i & ": " & Item(i).Name)
            Next

            .cmbStartItem.SelectedIndex = 0
            .cmbItemReq.SelectedIndex = 0
            .cmbEndItem.SelectedIndex = 0
            .cmbItemReward.SelectedIndex = 0

            .cmbClassReq.Items.Clear()
            .cmbClassReq.Items.Add("None")
            For i = 1 To MaxClasses
                .cmbClassReq.Items.Add(Classes(i).Name.Trim)
            Next

            .cmbStartItem.SelectedIndex = Quest(Editorindex).QuestGiveItem
            .cmbEndItem.SelectedIndex = Quest(Editorindex).QuestRemoveItem

            .nudGiveAmount.Value = Quest(Editorindex).QuestGiveItemValue

            .nudTakeAmount.Value = Quest(Editorindex).QuestRemoveItemValue

            .lstRewards.Items.Clear()
            For i = 1 To Quest(Editorindex).RewardCount
                .lstRewards.Items.Add(
                    i & ":" & Quest(Editorindex).RewardItemAmount(i) & " X " &
                    Item(Quest(Editorindex).RewardItem(i)).Name.Trim)
            Next

            .nudExpReward.Value = Quest(Editorindex).RewardExp

            .lstRequirements.Items.Clear()

            For i = 1 To Quest(Editorindex).ReqCount

                Select Case Quest(Editorindex).Requirement(i)
                    Case 1
                        .lstRequirements.Items.Add(
                            i & ":" & "Item Requirement: " & Item(Quest(Editorindex).RequirementIndex(i)).Name.Trim)
                    Case 2
                        .lstRequirements.Items.Add(
                            i & ":" & "Quest Requirement: " & Quest(Quest(Editorindex).RequirementIndex(i)).Name.Trim)
                    Case 3
                        .lstRequirements.Items.Add(
                            i & ":" & "Class Requirement: " & Classes(Quest(Editorindex).RequirementIndex(i)).Name.Trim)
                    Case Else
                        .lstRequirements.Items.Add(i & ":")
                End Select
            Next

            .lstTasks.Items.Clear()
            For i = 1 To Quest(Editorindex).TaskCount
                frmQuest.lstTasks.Items.Add(i & ":" & Quest(Editorindex).Task(i).TaskLog)
            Next

            .rdbNoneReq.Checked = True
        End With

        QuestChanged(Editorindex) = True
    End Sub

    Friend Sub QuestEditorOk()
        Dim I As Integer

        For I = 1 To MaxQuests
            If QuestChanged(I) Then
                SendSaveQuest(I)
            End If
        Next

        frmQuest.Dispose()
        Editor = 0
        ClearChanged_Quest()
    End Sub

    Friend Sub QuestEditorCancel()
        Editor = 0
        frmQuest.Dispose()
        ClearChanged_Quest()
        ClearQuests()
        SendRequestQuests()
    End Sub

    Friend Sub ClearChanged_Quest()
        Dim I As Integer

        For I = 0 To MaxQuests
            QuestChanged(I) = False
        Next
    End Sub

#End Region

#Region "DataBase"

    Sub ClearQuest(questNum As Integer)
        Dim I As Integer

        ' clear the Quest
        Quest(questNum).Name = ""
        Quest(questNum).QuestLog = ""
        Quest(questNum).Repeat = 0
        Quest(questNum).Cancelable = 0

        Quest(questNum).ReqCount = 0
        ReDim Quest(questNum).Requirement(Quest(questNum).ReqCount)
        ReDim Quest(questNum).RequirementIndex(Quest(questNum).ReqCount)
        For I = 1 To Quest(questNum).ReqCount
            Quest(questNum).Requirement(I) = 0
            Quest(questNum).RequirementIndex(I) = 0
        Next

        Quest(questNum).QuestGiveItem = 0
        Quest(questNum).QuestGiveItemValue = 0
        Quest(questNum).QuestRemoveItem = 0
        Quest(questNum).QuestRemoveItemValue = 0

        ReDim Quest(questNum).Chat(3)
        For I = 1 To 3
            Quest(questNum).Chat(I) = ""
        Next

        Quest(questNum).RewardCount = 0
        ReDim Quest(questNum).RewardItem(Quest(questNum).RewardCount)
        ReDim Quest(questNum).RewardItemAmount(Quest(questNum).RewardCount)
        For I = 1 To Quest(questNum).RewardCount
            Quest(questNum).RewardItem(I) = 0
            Quest(questNum).RewardItemAmount(I) = 0
        Next
        Quest(questNum).RewardExp = 0

        Quest(questNum).TaskCount = 0
        ReDim Quest(questNum).Task(Quest(questNum).TaskCount)
        For I = 1 To Quest(questNum).TaskCount
            Quest(questNum).Task(I).Order = 0
            Quest(questNum).Task(I).Npc = 0
            Quest(questNum).Task(I).Item = 0
            Quest(questNum).Task(I).Map = 0
            Quest(questNum).Task(I).Resource = 0
            Quest(questNum).Task(I).Amount = 0
            Quest(questNum).Task(I).Speech = ""
            Quest(questNum).Task(I).TaskLog = ""
            Quest(questNum).Task(I).QuestEnd = 0
            Quest(questNum).Task(I).TaskType = 0
        Next
    End Sub

    Sub ClearQuests()
        Dim I As Integer

        ReDim Quest(MaxQuests)

        For I = 1 To MaxQuests
            ClearQuest(I)
        Next
    End Sub

#End Region

#Region "Incoming Packets"

    Friend Sub Packet_QuestEditor(ByRef data() As Byte)
        QuestEditorShow = True
    End Sub

    Friend Sub Packet_UpdateQuest(ByRef data() As Byte)
        Dim questNum As Integer
        Dim buffer As New ByteStream(data)
        questNum = buffer.ReadInt32

        ' Update the Quest
        Quest(questNum).Name = buffer.ReadString
        Quest(questNum).QuestLog = buffer.ReadString
        Quest(questNum).Repeat = buffer.ReadInt32
        Quest(questNum).Cancelable = buffer.ReadInt32

        Quest(questNum).ReqCount = buffer.ReadInt32
        ReDim Quest(questNum).Requirement(Quest(questNum).ReqCount)
        ReDim Quest(questNum).RequirementIndex(Quest(questNum).ReqCount)
        For I = 1 To Quest(questNum).ReqCount
            Quest(questNum).Requirement(I) = buffer.ReadInt32
            Quest(questNum).RequirementIndex(I) = buffer.ReadInt32
        Next

        Quest(questNum).QuestGiveItem = buffer.ReadInt32
        Quest(questNum).QuestGiveItemValue = buffer.ReadInt32
        Quest(questNum).QuestRemoveItem = buffer.ReadInt32
        Quest(questNum).QuestRemoveItemValue = buffer.ReadInt32

        For I = 1 To 3
            Quest(questNum).Chat(I) = buffer.ReadString
        Next

        Quest(questNum).RewardCount = buffer.ReadInt32
        ReDim Quest(questNum).RewardItem(Quest(questNum).RewardCount)
        ReDim Quest(questNum).RewardItemAmount(Quest(questNum).RewardCount)
        For i = 1 To Quest(questNum).RewardCount
            Quest(questNum).RewardItem(i) = buffer.ReadInt32
            Quest(questNum).RewardItemAmount(i) = buffer.ReadInt32
        Next

        Quest(questNum).RewardExp = buffer.ReadInt32

        Quest(questNum).TaskCount = buffer.ReadInt32
        ReDim Quest(questNum).Task(Quest(questNum).TaskCount)
        For I = 1 To Quest(questNum).TaskCount
            Quest(questNum).Task(I).Order = buffer.ReadInt32
            Quest(questNum).Task(I).Npc = buffer.ReadInt32
            Quest(questNum).Task(I).Item = buffer.ReadInt32
            Quest(questNum).Task(I).Map = buffer.ReadInt32
            Quest(questNum).Task(I).Resource = buffer.ReadInt32
            Quest(questNum).Task(I).Amount = buffer.ReadInt32
            Quest(questNum).Task(I).Speech = buffer.ReadString
            Quest(questNum).Task(I).TaskLog = buffer.ReadString
            Quest(questNum).Task(I).QuestEnd = buffer.ReadInt32
            Quest(questNum).Task(I).TaskType = buffer.ReadInt32
        Next

        buffer.Dispose()
    End Sub

    Friend Sub Packet_PlayerQuest(ByRef data() As Byte)
        Dim questNum As Integer
        Dim buffer As New ByteStream(data)
        questNum = buffer.ReadInt32

        Player(Myindex).PlayerQuest(questNum).Status = buffer.ReadInt32
        Player(Myindex).PlayerQuest(questNum).ActualTask = buffer.ReadInt32
        Player(Myindex).PlayerQuest(questNum).CurrentCount = buffer.ReadInt32

        RefreshQuestLog()

        buffer.Dispose()
    End Sub

    Friend Sub Packet_PlayerQuests(ByRef data() As Byte)
        Dim I As Integer
        Dim buffer As New ByteStream(data)
        For I = 1 To MaxQuests
            Player(Myindex).PlayerQuest(I).Status = buffer.ReadInt32
            Player(Myindex).PlayerQuest(I).ActualTask = buffer.ReadInt32
            Player(Myindex).PlayerQuest(I).CurrentCount = buffer.ReadInt32
        Next

        RefreshQuestLog()

        buffer.Dispose()
    End Sub

    Friend Sub Packet_QuestMessage(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        QuestNum = buffer.ReadInt32
        QuestMessage = buffer.ReadString.Trim
        QuestMessage = QuestMessage.Replace("$playername$", GetPlayerName(Myindex))
        QuestNumForStart = buffer.ReadInt32

        UpdateQuestChat = True

        buffer.Dispose()
    End Sub

#End Region

#Region "Outgoing Packets"

    Friend Sub SendRequestEditQuest()
        Dim buffer As ByteStream

        buffer = New ByteStream(4)
        buffer.WriteInt32(EditorPackets.RequestEditQuest)
        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveQuest(QuestNum As Integer)
        Dim buffer As ByteStream

        buffer = New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveQuest)
        buffer.WriteInt32(QuestNum)

        buffer.WriteString((Quest(QuestNum).Name.Trim))
        buffer.WriteString((Quest(QuestNum).QuestLog.Trim))
        buffer.WriteInt32(Quest(QuestNum).Repeat)
        buffer.WriteInt32(Quest(QuestNum).Cancelable)

        buffer.WriteInt32(Quest(QuestNum).ReqCount)
        For I = 1 To Quest(QuestNum).ReqCount
            buffer.WriteInt32(Quest(QuestNum).Requirement(I))
            buffer.WriteInt32(Quest(QuestNum).RequirementIndex(I))
        Next

        buffer.WriteInt32(Quest(QuestNum).QuestGiveItem)
        buffer.WriteInt32(Quest(QuestNum).QuestGiveItemValue)
        buffer.WriteInt32(Quest(QuestNum).QuestRemoveItem)
        buffer.WriteInt32(Quest(QuestNum).QuestRemoveItemValue)

        For I = 1 To 3
            buffer.WriteString((Quest(QuestNum).Chat(I).Trim))
        Next

        buffer.WriteInt32(Quest(QuestNum).RewardCount)
        For i = 1 To Quest(QuestNum).RewardCount
            buffer.WriteInt32(Quest(QuestNum).RewardItem(i))
            buffer.WriteInt32(Quest(QuestNum).RewardItemAmount(i))
        Next

        buffer.WriteInt32(Quest(QuestNum).RewardExp)

        buffer.WriteInt32(Quest(QuestNum).TaskCount)
        For I = 1 To Quest(QuestNum).TaskCount
            buffer.WriteInt32(Quest(QuestNum).Task(I).Order)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Npc)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Item)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Map)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Resource)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Amount)
            buffer.WriteString((Quest(QuestNum).Task(I).Speech.Trim))
            buffer.WriteString((Quest(QuestNum).Task(I).TaskLog.Trim))
            buffer.WriteInt32(Quest(QuestNum).Task(I).QuestEnd)
            buffer.WriteInt32(Quest(QuestNum).Task(I).TaskType)
        Next

        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Sub SendRequestQuests()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestQuests)

        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub UpdateQuestLog()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CQuestLogUpdate)

        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub PlayerHandleQuest(questNum As Integer, order As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CPlayerHandleQuest)
        buffer.WriteInt32(questNum)
        buffer.WriteInt32(order) '1=accept quest, 2=cancel quest

        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub QuestReset(questNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CQuestReset)
        buffer.WriteInt32(questNum)

        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

#End Region

#Region "Support Functions"

    'Tells if the quest is in progress or not
    Friend Function QuestInProgress(questNum As Integer) As Boolean
        QuestInProgress = False
        If questNum < 1 OrElse questNum > MaxQuests Then Exit Function

        If Player(Myindex).PlayerQuest(questNum).Status = QuestStatusType.Started Then 'Status=1 means started
            QuestInProgress = True
        End If
    End Function

    Friend Function QuestCompleted(questNum As Integer) As Boolean
        QuestCompleted = False
        If questNum < 1 OrElse questNum > MaxQuests Then Exit Function

        If _
            Player(Myindex).PlayerQuest(questNum).Status = QuestStatusType.Completed OrElse
            Player(Myindex).PlayerQuest(questNum).Status = QuestStatusType.Repeatable Then
            QuestCompleted = True
        End If
    End Function

    Friend Function GetQuestNum(questName As String) As Integer
        Dim I As Integer
        GetQuestNum = 0

        For I = 1 To MaxQuests
            If Quest(I).Name.Trim = questName.Trim Then
                GetQuestNum = I
                Exit For
            End If
        Next
    End Function

#End Region

#Region "Misc Functions"

    Friend Function CanStartQuest(questNum As Integer) As Boolean
        Dim i As Integer

        CanStartQuest = False

        If questNum < 1 OrElse questNum > MaxQuests Then Exit Function
        If QuestInProgress(questNum) Then Exit Function

        'Check if player has the quest 0 (not started) or 3 (completed but it can be started again)
        If _
            Player(Myindex).PlayerQuest(questNum).Status = QuestStatusType.NotStarted OrElse
            Player(Myindex).PlayerQuest(questNum).Status = QuestStatusType.Repeatable Then

            For i = 1 To Quest(questNum).ReqCount
                'Check if item is needed
                If Quest(questNum).Requirement(i) = 1 Then
                    If Quest(questNum).RequirementIndex(i) > 0 AndAlso Quest(questNum).RequirementIndex(i) <= MAX_ITEMS _
                        Then
                        If HasItem(Myindex, Quest(questNum).RequirementIndex(i)) = 0 Then
                            Exit Function
                        End If
                    End If
                End If

                'Check if previous quest is needed
                If Quest(questNum).Requirement(i) = 2 Then
                    If Quest(questNum).RequirementIndex(i) > 0 AndAlso Quest(questNum).RequirementIndex(i) <= MaxQuests _
                        Then
                        If _
                            Player(Myindex).PlayerQuest(Quest(questNum).RequirementIndex(i)).Status =
                            QuestStatusType.NotStarted OrElse
                            Player(Myindex).PlayerQuest(Quest(questNum).RequirementIndex(i)).Status =
                            QuestStatusType.Started Then
                            Exit Function
                        End If
                    End If
                End If

            Next

            'Go on :)
            CanStartQuest = True
        Else
            CanStartQuest = False
        End If
    End Function

    Friend Function CanEndQuest(index As Integer, questNum As Integer) As Boolean
        CanEndQuest = False

        If Player(index).PlayerQuest(questNum).ActualTask >= Quest(questNum).Task.Length Then
            CanEndQuest = True
        End If
        If Quest(questNum).Task(Player(index).PlayerQuest(questNum).ActualTask).QuestEnd = 1 Then
            CanEndQuest = True
        End If
    End Function

    Function HasItem(index As Integer, itemNum As Integer) As Integer
        Dim i As Integer

        ' Check for subscript out of range
        If IsPlaying(index) = False OrElse itemNum <= 0 OrElse itemNum > MAX_ITEMS Then
            Exit Function
        End If

        For i = 1 To MAX_INV

            ' Check to see if the player has the item
            If GetPlayerInvItemNum(index, i) = itemNum Then
                If Item(itemNum).Type = ItemType.Currency OrElse Item(itemNum).Stackable = 1 Then
                    HasItem = GetPlayerInvItemValue(index, i)
                Else
                    HasItem = 1
                End If

                Exit Function
            End If

        Next
    End Function

    Friend Sub RefreshQuestLog()
        Dim I As Integer, x As Integer

        For I = 1 To MaxActivequests
            QuestNames(I) = ""
        Next

        x = 1

        For I = 1 To MaxQuests
            If QuestInProgress(I) AndAlso x < MaxActivequests Then
                QuestNames(x) = Quest(I).Name.Trim
                x = x + 1
            End If
        Next
    End Sub

    ' //////////////////////// // Visual Interaction // ////////////////////////

    Friend Sub LoadQuestlogBox()
        Dim questNum As Integer, curTask As Integer, I As Integer

        If SelectedQuest = 0 Then Exit Sub

        For I = 1 To MaxQuests
            If QuestNames(SelectedQuest).Trim = Quest(I).Name.Trim Then
                questNum = I
            End If
        Next

        If questNum = 0 Then Exit Sub

        curTask = Player(Myindex).PlayerQuest(questNum).ActualTask

        If curTask >= Quest(questNum).Task.Length Then Exit Sub

        'Quest Log (Main Task)
        QuestTaskLogText = Quest(questNum).QuestLog.Trim

        'Actual Task
        QuestTaskLogText = Quest(questNum).Task(curTask).TaskLog.Trim

        'Last dialog
        If Player(Myindex).PlayerQuest(questNum).ActualTask > 1 Then
            If Quest(questNum).Task(curTask - 1).Speech.Trim.Length > 0 Then
                QuestDialogText = Quest(questNum).Task(curTask - 1).Speech.Trim.Replace("$playername$",
                                                                                        GetPlayerName(Myindex))
            Else
                QuestDialogText = Quest(questNum).Chat(1).Replace("$playername$", GetPlayerName(Myindex)).Trim
            End If
        Else
            QuestDialogText = Quest(questNum).Chat(1).Replace("$playername$", GetPlayerName(Myindex)).Trim
        End If

        'Quest Status
        If Player(Myindex).PlayerQuest(questNum).Status = QuestStatusType.Started Then
            QuestStatus2Text = Configuration.Language.Quests.queststarted
            AbandonQuestText = Configuration.Language.Quests.questcancel
            AbandonQuestVisible = True
        ElseIf Player(Myindex).PlayerQuest(questNum).Status = QuestStatusType.Completed Then
            QuestStatus2Text = Configuration.Language.Quests.questcomplete
            AbandonQuestVisible = False
        Else
            QuestStatus2Text = "???"
            AbandonQuestVisible = False
        End If

        Select Case Quest(questNum).Task(curTask).TaskType
            'defeat x amount of Npc
            Case QuestType.Slay
                Dim curCount As Integer = Player(Myindex).PlayerQuest(questNum).CurrentCount
                Dim maxAmount As Integer = Quest(questNum).Task(curTask).Amount
                Dim npcName As String = Npc(Quest(questNum).Task(curTask).Npc).Name
                ActualTaskText = String.Format(Configuration.Language.Quests.questgoslay, curCount, maxAmount, npcName) _
                '"Defeat " & CurCount & "/" & MaxAmount & " " & NpcName
                'gather x amount of items
            Case QuestType.Collect
                Dim curCount As Integer = Player(Myindex).PlayerQuest(questNum).CurrentCount
                Dim maxAmount As Integer = Quest(questNum).Task(curTask).Amount
                Dim itemName As String = Item(Quest(questNum).Task(curTask).Item).Name
                ActualTaskText = String.Format(Configuration.Language.Quests.questgocollect, curCount, maxAmount,
                                               itemName)'"Collect " & CurCount & "/" & MaxAmount & " " & ItemName
                'go talk to npc
            Case QuestType.Talk
                Dim npcName As String = Npc(Quest(questNum).Task(curTask).Npc).Name
                ActualTaskText = String.Format(Configuration.Language.Quests.questtalkto, npcName) _
                '"Go talk to  " & NpcName
                'reach certain map
            Case QuestType.Reach
                Dim mapName As String = MapNames(Quest(questNum).Task(curTask).Map)
                ActualTaskText = String.Format(Configuration.Language.Quests.questgoto, mapName)'"Go to " & MapName
            Case QuestType.Give
                'give x amount of items to npc
                Dim npcName As String = Npc(Quest(questNum).Task(curTask).Npc).Name
                Dim curCount As Integer = Player(Myindex).PlayerQuest(questNum).CurrentCount
                Dim maxAmount As Integer = Quest(questNum).Task(curTask).Amount
                Dim itemName As String = Item(Quest(questNum).Task(curTask).Item).Name
                ActualTaskText = String.Format(Configuration.Language.Quests.questgive, npcName, itemName, curCount,
                                               maxAmount) _
                '"Give " & NpcName & " the " & ItemName & CurCount & "/" & MaxAmount & " they requested"
                'defeat certain amount of players
            Case QuestType.Kill
                Dim curCount As Integer = Player(Myindex).PlayerQuest(questNum).CurrentCount
                Dim maxAmount As Integer = Quest(questNum).Task(curTask).Amount
                ActualTaskText = String.Format(Configuration.Language.Quests.questkill, curCount, maxAmount) _
                '"Defeat " & CurCount & "/" & MaxAmount & " Players in Battle"
                'go collect resources
            Case QuestType.Gather
                Dim curCount As Integer = Player(Myindex).PlayerQuest(questNum).CurrentCount
                Dim maxAmount As Integer = Quest(questNum).Task(curTask).Amount
                Dim resourceName As String = Resource(Quest(questNum).Task(curTask).Resource).Name
                ActualTaskText = String.Format(Configuration.Language.Quests.questgather, curCount, maxAmount,
                                               resourceName)'"Gather " & CurCount & "/" & MaxAmount & " " & ResourceName
                'fetch x amount of items from npc
            Case QuestType.Fetch
                Dim npcName As String = Item(Quest(questNum).Task(curTask).Npc).Name
                Dim maxAmount As Integer = Quest(questNum).Task(curTask).Amount
                Dim itemName As String = Item(Quest(questNum).Task(curTask).Item).Name
                ActualTaskText = String.Format(Configuration.Language.Quests.questfetch, itemName, maxAmount, npcName) _
                '"Fetch " & ItemName & "X" & MaxAmount & " from " & NpcName
            Case Else
                'ToDo
                ActualTaskText = "errr..."
        End Select

        'Rewards
        ReDim QuestRewardsText(Quest(questNum).RewardCount + 1)
        For I = 1 To Quest(questNum).RewardCount
            QuestRewardsText(I) = Item(Quest(questNum).RewardItem(I)).Name & " X" & Quest(questNum).RewardItemAmount(I)
        Next
        QuestRewardsText(I) = Quest(questNum).RewardExp & " EXP"
    End Sub

    Friend Sub DrawQuestLog()
        Dim i As Integer, y As Integer

        y = 10

        'first render panel
        RenderSprite(QuestSprite, GameWindow, QuestLogX, QuestLogY, 0, 0, QuestGfxInfo.Width, QuestGfxInfo.Height)

        'draw quest names
        For i = 1 To MaxActivequests
            If QuestNames(i).Trim.Length > 0 Then
                DrawText(QuestLogX + 7, QuestLogY + y, QuestNames(i).Trim, Color.White, Color.Black, GameWindow)
                y = y + 20
            End If
        Next

        If SelectedQuest <= 0 Then Exit Sub

        'quest log text
        y = 0
        For Each str As String In WordWrap(QuestTaskLogText.Trim, 35, WrapMode.Characters, WrapType.BreakWord)
            'description
            DrawText(QuestLogX + 204, QuestLogY + 30 + y, str, Color.White, Color.Black, GameWindow)
            y = y + 15
        Next

        y = 0
        For Each str As String In WordWrap(ActualTaskText.Trim, 40, WrapMode.Characters, WrapType.BreakWord)
            'description
            DrawText(QuestLogX + 204, QuestLogY + 147 + y, str, Color.White, Color.Black, GameWindow)
            y = y + 15
        Next

        y = 0
        For Each str As String In WordWrap(QuestDialogText.Trim, 40, WrapMode.Characters, WrapType.BreakWord)
            'description
            DrawText(QuestLogX + 204, QuestLogY + 218 + y, str, Color.White, Color.Black, GameWindow)
            y = y + 15
        Next
        DrawText(QuestLogX + 280, QuestLogY + 263, QuestStatus2Text.Trim, Color.White, Color.Black, GameWindow)

        'DrawText(QuestLogX + 285, QuestLogY + 288, QuestRequirementsText.Trim, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        y = 0
        For i = 1 To QuestRewardsText.Length - 1
            'description
            DrawText(QuestLogX + 255, QuestLogY + 292 + y, QuestRewardsText(i).Trim, Color.White, Color.Black,
                     GameWindow)
            y = y + 15
        Next
    End Sub

    Friend Sub ResetQuestLog()

        QuestTaskLogText = ""
        ActualTaskText = ""
        QuestDialogText = ""
        QuestStatus2Text = ""
        AbandonQuestText = ""
        AbandonQuestVisible = False
        QuestRequirementsText = ""
        ReDim QuestRewardsText(0)
        PnlQuestLogVisible = False

        SelectedQuest = 0
    End Sub

    Friend Sub LoadRequirement(QuestNum As Integer, ReqNum As Integer)
        Dim i As Integer

        With frmQuest
            'Populate combo boxes
            .cmbItemReq.Items.Clear()
            .cmbItemReq.Items.Add("None")

            For i = 1 To MAX_ITEMS
                .cmbItemReq.Items.Add(i & ": " & Item(i).Name)
            Next

            .cmbQuestReq.Items.Clear()
            .cmbQuestReq.Items.Add("None")

            For i = 1 To MaxQuests
                .cmbQuestReq.Items.Add(i & ": " & Quest(i).Name)
            Next

            .cmbClassReq.Items.Clear()
            .cmbClassReq.Items.Add("None")

            For i = 1 To MaxClasses
                .cmbClassReq.Items.Add(i & ": " & Classes(i).Name)
            Next

            .cmbItemReq.Enabled = False
            .cmbQuestReq.Enabled = False
            .cmbClassReq.Enabled = False

            Select Case Quest(QuestNum).Requirement(ReqNum)
                Case 0
                    .rdbNoneReq.Checked = True
                Case 1
                    .rdbItemReq.Checked = True
                    .cmbItemReq.Enabled = True
                    .cmbItemReq.SelectedIndex = Quest(QuestNum).RequirementIndex(ReqNum)
                Case 2
                    .rdbQuestReq.Checked = True
                    .cmbQuestReq.Enabled = True
                    .cmbQuestReq.SelectedIndex = Quest(QuestNum).RequirementIndex(ReqNum)
                Case 3
                    .rdbClassReq.Checked = True
                    .cmbClassReq.Enabled = True
                    .cmbClassReq.SelectedIndex = Quest(QuestNum).RequirementIndex(ReqNum)
            End Select

        End With
    End Sub

    'Subroutine that load the desired task in the form
    Friend Sub LoadTask(QuestNum As Integer, TaskNum As Integer)
        Dim TaskToLoad As TaskRec
        TaskToLoad = Quest(QuestNum).Task(TaskNum)

        With frmQuest
            'Load the task type
            Select Case TaskToLoad.Order
                Case 0
                    .optTask0.Checked = True
                Case 1
                    .optTask1.Checked = True
                Case 2
                    .optTask2.Checked = True
                Case 3
                    .optTask3.Checked = True
                Case 4
                    .optTask4.Checked = True
                Case 5
                    .optTask5.Checked = True
                Case 6
                    .optTask6.Checked = True
                Case 7
                    .optTask7.Checked = True
            End Select

            'Load textboxes
            .txtTaskLog.Text = "" & TaskToLoad.TaskLog.Trim

            'Populate combo boxes
            .cmbNpc.Items.Clear()
            .cmbNpc.Items.Add("None")

            For i = 1 To MAX_NPCS
                .cmbNpc.Items.Add(i & ": " & Npc(i).Name)
            Next

            .cmbItem.Items.Clear()
            .cmbItem.Items.Add("None")

            For i = 1 To MAX_ITEMS
                .cmbItem.Items.Add(i & ": " & Item(i).Name)
            Next

            .cmbMap.Items.Clear()
            .cmbMap.Items.Add("None")

            For i = 1 To MAX_MAPS
                .cmbMap.Items.Add(i)
            Next

            .cmbResource.Items.Clear()
            .cmbResource.Items.Add("None")

            For i = 1 To MAX_RESOURCES
                .cmbResource.Items.Add(i & ": " & Resource(i).Name)
            Next

            'Set combo to 0 and disable them so they can be enabled when needed
            .cmbNpc.SelectedIndex = 0
            .cmbItem.SelectedIndex = 0
            .cmbMap.SelectedIndex = 0
            .cmbResource.SelectedIndex = 0
            .nudAmount.Value = 0

            .cmbNpc.Enabled = False
            .cmbItem.Enabled = False
            .cmbMap.Enabled = False
            .cmbResource.Enabled = False
            .nudAmount.Enabled = False

            If TaskToLoad.QuestEnd = 1 Then
                .chkEnd.Checked = True
            Else
                .chkEnd.Checked = False
            End If

            Select Case TaskToLoad.Order
                Case 0 'Nothing

                Case QUEST_TYPE_GOSLAY '1
                    .cmbNpc.Enabled = True
                    .cmbNpc.SelectedIndex = TaskToLoad.Npc
                    .nudAmount.Enabled = True
                    .nudAmount.Value = TaskToLoad.Amount

                Case QUEST_TYPE_GOGATHER '2
                    .cmbItem.Enabled = True
                    .cmbItem.SelectedIndex = TaskToLoad.Item
                    .nudAmount.Enabled = True
                    .nudAmount.Value = TaskToLoad.Amount

                Case QUEST_TYPE_GOTALK '3
                    .cmbNpc.Enabled = True
                    .cmbNpc.SelectedIndex = TaskToLoad.Npc

                Case QUEST_TYPE_GOREACH '4
                    .cmbMap.Enabled = True
                    .cmbMap.SelectedIndex = TaskToLoad.Map

                Case QUEST_TYPE_GOGIVE '5
                    .cmbItem.Enabled = True
                    .cmbItem.SelectedIndex = TaskToLoad.Item
                    .nudAmount.Enabled = True
                    .nudAmount.Value = TaskToLoad.Amount
                    .cmbNpc.Enabled = True
                    .cmbNpc.SelectedIndex = TaskToLoad.Npc
                    .txtTaskSpeech.Text = "" & TaskToLoad.Speech.Trim

                Case QUEST_TYPE_GOTRAIN '6
                    .cmbResource.Enabled = True
                    .cmbResource.SelectedIndex = TaskToLoad.Resource
                    .nudAmount.Enabled = True
                    .nudAmount.Value = TaskToLoad.Amount

                Case QUEST_TYPE_GOGET '7
                    .cmbNpc.Enabled = True
                    .cmbNpc.SelectedIndex = TaskToLoad.Npc
                    .cmbItem.Enabled = True
                    .cmbItem.SelectedIndex = TaskToLoad.Item
                    .nudAmount.Enabled = True
                    .nudAmount.Value = TaskToLoad.Amount
                    .txtTaskSpeech.Text = "" & TaskToLoad.Speech.Trim
            End Select

            .lblTaskNum.Text = "Task Number: " & TaskNum
        End With
    End Sub

#End Region

End Module