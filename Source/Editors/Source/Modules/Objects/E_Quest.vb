Imports ASFW

Friend Module E_Quest
#Region "Global Info"
    'Constants
    Friend Const MAX_QUESTS As Integer = 250
    'Friend Const MAX_REQUIREMENTS As Byte = 10
    'Friend Const MAX_TASKS As Byte = 10
    Friend Const EDITOR_TASKS As Byte = 7

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
    Friend QuestNames(MAX_ACTIVEQUESTS) As String

    Friend Quest_Changed(MAX_QUESTS) As Boolean

    Friend QuestEditorShow As Boolean

    'questlog variables

    Friend Const MAX_ACTIVEQUESTS = 10

    Friend QuestLogX As Integer = 150
    Friend QuestLogY As Integer = 100

    Friend pnlQuestLogVisible As Boolean
    Friend SelectedQuest As String
    Friend QuestTaskLogText As String = ""
    Friend ActualTaskText As String = ""
    Friend QuestDialogText As String = ""
    Friend QuestStatus2Text As String = ""
    Friend AbandonQuestText As String = ""
    Friend AbandonQuestVisible As Boolean
    Friend QuestRequirementsText As String = ""
    Friend QuestRewardsText As String = ""

    'here we store temp info because off UpdateUI >.<
    Friend UpdateQuestWindow As Boolean
    Friend UpdateQuestChat As Boolean
    Friend QuestNum As Integer
    Friend QuestNumForStart As Integer
    Friend QuestMessage As String
    Friend QuestAcceptTag As Integer

    'Types
    Friend Quest(MAX_QUESTS) As QuestRec

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
        EditorIndex = frmQuest.lstIndex.SelectedIndex + 1

        With frmQuest
            .txtName.Text = Trim$(Quest(EditorIndex).Name)

            If Quest(EditorIndex).Repeat = 1 Then
                .chkRepeat.Checked = True
            Else
                .chkRepeat.Checked = False
            End If

            .txtStartText.Text = Trim$(Quest(EditorIndex).Chat(1))
            .txtProgressText.Text = Trim$(Quest(EditorIndex).Chat(2))
            .txtEndText.Text = Trim$(Quest(EditorIndex).Chat(3))

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
            For i = 1 To Max_Classes
                .cmbClassReq.Items.Add(Trim(Classes(i).Name))
            Next

            .cmbStartItem.SelectedIndex = Quest(EditorIndex).QuestGiveItem
            .cmbEndItem.SelectedIndex = Quest(EditorIndex).QuestRemoveItem

            .nudGiveAmount.Value = Quest(EditorIndex).QuestGiveItemValue

            .nudTakeAmount.Value = Quest(EditorIndex).QuestRemoveItemValue

            .lstRewards.Items.Clear()
            For i = 1 To Quest(EditorIndex).RewardCount
                .lstRewards.Items.Add(i & ":" & Quest(EditorIndex).RewardItemAmount(i) & " X " & Trim(Item(Quest(EditorIndex).RewardItem(i)).Name))
            Next

            .nudExpReward.Value = Quest(EditorIndex).RewardExp


            .lstRequirements.Items.Clear()

            For i = 1 To Quest(EditorIndex).ReqCount

                Select Case Quest(EditorIndex).Requirement(i)
                    Case 1
                        .lstRequirements.Items.Add(i & ":" & "Item Requirement: " & Trim(Item(Quest(EditorIndex).RequirementIndex(i)).Name))
                    Case 2
                        .lstRequirements.Items.Add(i & ":" & "Quest Requirement: " & Trim(Quest(Quest(EditorIndex).RequirementIndex(i)).Name))
                    Case 3
                        .lstRequirements.Items.Add(i & ":" & "Class Requirement: " & Trim(Classes(Quest(EditorIndex).RequirementIndex(i)).Name))
                    Case Else
                        .lstRequirements.Items.Add(i & ":")
                End Select
            Next

            .lstTasks.Items.Clear()
            For i = 1 To Quest(EditorIndex).TaskCount
                frmQuest.lstTasks.Items.Add(i & ":" & Quest(EditorIndex).Task(i).TaskLog)
            Next

            .rdbNoneReq.Checked = True
        End With

        Quest_Changed(EditorIndex) = True

    End Sub

    Friend Sub QuestEditorOk()
        Dim I As Integer

        For I = 1 To MAX_QUESTS
            If Quest_Changed(I) Then
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

        For I = 0 To MAX_QUESTS
            Quest_Changed(I) = False
        Next
    End Sub
#End Region

#Region "DataBase"
    Sub ClearQuest(QuestNum As Integer)
        Dim I As Integer

        ' clear the Quest
        Quest(QuestNum).Name = ""
        Quest(QuestNum).QuestLog = ""
        Quest(QuestNum).Repeat = 0
        Quest(QuestNum).Cancelable = 0

        Quest(QuestNum).ReqCount = 0
        ReDim Quest(QuestNum).Requirement(Quest(QuestNum).ReqCount)
        ReDim Quest(QuestNum).RequirementIndex(Quest(QuestNum).ReqCount)
        For I = 1 To Quest(QuestNum).ReqCount
            Quest(QuestNum).Requirement(I) = 0
            Quest(QuestNum).RequirementIndex(I) = 0
        Next

        Quest(QuestNum).QuestGiveItem = 0
        Quest(QuestNum).QuestGiveItemValue = 0
        Quest(QuestNum).QuestRemoveItem = 0
        Quest(QuestNum).QuestRemoveItemValue = 0

        ReDim Quest(QuestNum).Chat(3)
        For I = 1 To 3
            Quest(QuestNum).Chat(I) = ""
        Next

        Quest(QuestNum).RewardCount = 0
        ReDim Quest(QuestNum).RewardItem(Quest(QuestNum).RewardCount)
        ReDim Quest(QuestNum).RewardItemAmount(Quest(QuestNum).RewardCount)
        For I = 1 To Quest(QuestNum).RewardCount
            Quest(QuestNum).RewardItem(I) = 0
            Quest(QuestNum).RewardItemAmount(I) = 0
        Next
        Quest(QuestNum).RewardExp = 0

        Quest(QuestNum).TaskCount = 0
        ReDim Quest(QuestNum).Task(Quest(QuestNum).TaskCount)
        For I = 1 To Quest(QuestNum).TaskCount
            Quest(QuestNum).Task(I).Order = 0
            Quest(QuestNum).Task(I).Npc = 0
            Quest(QuestNum).Task(I).Item = 0
            Quest(QuestNum).Task(I).Map = 0
            Quest(QuestNum).Task(I).Resource = 0
            Quest(QuestNum).Task(I).Amount = 0
            Quest(QuestNum).Task(I).Speech = ""
            Quest(QuestNum).Task(I).TaskLog = ""
            Quest(QuestNum).Task(I).QuestEnd = 0
            Quest(QuestNum).Task(I).TaskType = 0
        Next

    End Sub

    Sub ClearQuests()
        Dim I As Integer

        For I = 1 To MAX_QUESTS
            ClearQuest(I)
        Next
    End Sub
#End Region

#Region "Incoming Packets"
    Friend Sub Packet_QuestEditor(ByRef data() As Byte)
        QuestEditorShow = True
    End Sub

    Friend Sub Packet_UpdateQuest(ByRef data() As Byte)
        Dim QuestNum As Integer
        dim buffer as New ByteStream(Data)
        QuestNum = Buffer.ReadInt32

        ' Update the Quest
        Quest(QuestNum).Name = Buffer.ReadString
        Quest(QuestNum).QuestLog = Buffer.ReadString
        Quest(QuestNum).Repeat = Buffer.ReadInt32
        Quest(QuestNum).Cancelable = Buffer.ReadInt32

        Quest(QuestNum).ReqCount = Buffer.ReadInt32
        ReDim Quest(QuestNum).Requirement(Quest(QuestNum).ReqCount)
        ReDim Quest(QuestNum).RequirementIndex(Quest(QuestNum).ReqCount)
        For I = 1 To Quest(QuestNum).ReqCount
            Quest(QuestNum).Requirement(I) = Buffer.ReadInt32
            Quest(QuestNum).RequirementIndex(I) = Buffer.ReadInt32
        Next

        Quest(QuestNum).QuestGiveItem = Buffer.ReadInt32
        Quest(QuestNum).QuestGiveItemValue = Buffer.ReadInt32
        Quest(QuestNum).QuestRemoveItem = Buffer.ReadInt32
        Quest(QuestNum).QuestRemoveItemValue = Buffer.ReadInt32

        For I = 1 To 3
            Quest(QuestNum).Chat(I) = Buffer.ReadString
        Next

        Quest(QuestNum).RewardCount = Buffer.ReadInt32
        ReDim Quest(QuestNum).RewardItem(Quest(QuestNum).RewardCount)
        ReDim Quest(QuestNum).RewardItemAmount(Quest(QuestNum).RewardCount)
        For i = 1 To Quest(QuestNum).RewardCount
            Quest(QuestNum).RewardItem(i) = Buffer.ReadInt32
            Quest(QuestNum).RewardItemAmount(i) = Buffer.ReadInt32
        Next

        Quest(QuestNum).RewardExp = Buffer.ReadInt32

        Quest(QuestNum).TaskCount = Buffer.ReadInt32
        ReDim Quest(QuestNum).Task(Quest(QuestNum).TaskCount)
        For I = 1 To Quest(QuestNum).TaskCount
            Quest(QuestNum).Task(I).Order = Buffer.ReadInt32
            Quest(QuestNum).Task(I).Npc = Buffer.ReadInt32
            Quest(QuestNum).Task(I).Item = Buffer.ReadInt32
            Quest(QuestNum).Task(I).Map = Buffer.ReadInt32
            Quest(QuestNum).Task(I).Resource = Buffer.ReadInt32
            Quest(QuestNum).Task(I).Amount = Buffer.ReadInt32
            Quest(QuestNum).Task(I).Speech = Buffer.ReadString
            Quest(QuestNum).Task(I).TaskLog = Buffer.ReadString
            Quest(QuestNum).Task(I).QuestEnd = Buffer.ReadInt32
            Quest(QuestNum).Task(I).TaskType = Buffer.ReadInt32
        Next

        Buffer.Dispose()
    End Sub

#End Region

#Region "Outgoing Packets"
    Friend Sub SendRequestEditQuest()
        dim buffer as ByteStream

        Buffer = New ByteStream(4)
        Buffer.WriteInt32(EditorPackets.RequestEditQuest)
        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()

    End Sub

    Friend Sub SendSaveQuest(QuestNum As Integer)
        dim buffer as ByteStream

        Buffer = New ByteStream(4)

        Buffer.WriteInt32(EditorPackets.SaveQuest)
        Buffer.WriteInt32(QuestNum)

        buffer.WriteString((Trim(Quest(QuestNum).Name)))
        buffer.WriteString((Trim(Quest(QuestNum).QuestLog)))
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
            buffer.WriteString((Trim(Quest(QuestNum).Chat(I))))
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
            buffer.WriteString((Trim(Quest(QuestNum).Task(I).Speech)))
            buffer.WriteString((Trim(Quest(QuestNum).Task(I).TaskLog)))
            buffer.WriteInt32(Quest(QuestNum).Task(I).QuestEnd)
            Buffer.WriteInt32(Quest(QuestNum).Task(I).TaskType)
        Next

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()

    End Sub

    Sub SendRequestQuests()
        dim buffer as ByteStream

        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ClientPackets.CRequestQuests)
        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()

    End Sub

    Friend Sub UpdateQuestLog()
        dim buffer as ByteStream

        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ClientPackets.CQuestLogUpdate)
        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()

    End Sub

    Friend Sub PlayerHandleQuest(QuestNum As Integer, Order As Integer)
        dim buffer as ByteStream

        Buffer = New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CPlayerHandleQuest)
        Buffer.WriteInt32(QuestNum)
        Buffer.WriteInt32(Order) '1=accept quest, 2=cancel quest
        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Friend Sub QuestReset(QuestNum As Integer)
        dim buffer as ByteStream

        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ClientPackets.CQuestReset)
        Buffer.WriteInt32(QuestNum)
        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()

    End Sub
#End Region

#Region "Support Functions"

    Friend Function GetQuestNum(QuestName As String) As Integer
        Dim I As Integer
        GetQuestNum = 0

        For I = 1 To MAX_QUESTS
            If Trim$(Quest(I).Name) = Trim$(QuestName) Then
                GetQuestNum = I
                Exit For
            End If
        Next
    End Function

#End Region

#Region "Misc Functions"
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

            For i = 1 To MAX_QUESTS
                .cmbQuestReq.Items.Add(i & ": " & Quest(i).Name)
            Next

            .cmbClassReq.Items.Clear()
            .cmbClassReq.Items.Add("None")

            For i = 1 To Max_Classes
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
            .txtTaskLog.Text = "" & Trim$(TaskToLoad.TaskLog)

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
                    .txtTaskSpeech.Text = "" & Trim$(TaskToLoad.Speech)

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
                    .txtTaskSpeech.Text = "" & Trim$(TaskToLoad.Speech)
            End Select

            .lblTaskNum.Text = "Task Number: " & TaskNum
        End With
    End Sub
#End Region

End Module
