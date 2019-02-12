Friend Class frmEditor_Quest
    Dim SelectedTask As Integer

    Private Sub FrmEditor_Quest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Width = 740

        fraRequirements.Location = fraQuestList.Location
        fraRequirements.Visible = False
        fraTasks.Location = fraQuestList.Location
        fraTasks.Visible = False

        nudAmount.Maximum = 999999

        nudGiveAmount.Maximum = Byte.MaxValue
        nudTakeAmount.Maximum = Byte.MaxValue
        nudItemRewValue.Maximum = 999999

    End Sub

    Private Sub LstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        QuestEditorInit()
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer

        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        tmpindex = lstIndex.SelectedIndex
        Quest(Editorindex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Quest(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        If Len(Trim$(txtName.Text)) = 0 Then
            MsgBox("Name required.")
        Else
            QuestEditorOk()
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        QuestEditorCancel()
    End Sub

    Private Sub TxtStartText_TextChanged(sender As Object, e As EventArgs) Handles txtStartText.TextChanged
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        Quest(Editorindex).Chat(1) = Trim$(txtStartText.Text)
    End Sub

    Private Sub TxtProgressText_TextChanged(sender As Object, e As EventArgs) Handles txtProgressText.TextChanged
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        Quest(Editorindex).Chat(2) = Trim$(txtProgressText.Text)
    End Sub

    Private Sub TxtEndText_TextChanged(sender As Object, e As EventArgs) Handles txtEndText.TextChanged
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        Quest(Editorindex).Chat(3) = Trim$(txtEndText.Text)
    End Sub

    Private Sub CmbStartItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStartItem.SelectedIndexChanged
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        Quest(Editorindex).QuestGiveItem = cmbStartItem.SelectedIndex
    End Sub

    Private Sub CmbEndItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEndItem.SelectedIndexChanged
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        Quest(Editorindex).QuestRemoveItem = cmbEndItem.SelectedIndex
    End Sub

    Private Sub NudGiveAmount_ValueChanged(sender As Object, e As EventArgs) Handles nudGiveAmount.ValueChanged
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        Quest(Editorindex).QuestGiveItemValue = cmbEndItem.SelectedIndex
    End Sub

    Private Sub NudTakeAmount_ValueChanged(sender As Object, e As EventArgs) Handles nudTakeAmount.ValueChanged
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        Quest(Editorindex).QuestRemoveItemValue = nudTakeAmount.Value
    End Sub

    Private Sub ChkRepeat_CheckedChanged(sender As Object, e As EventArgs) Handles chkRepeat.CheckedChanged
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        If chkRepeat.Checked = True Then
            Quest(Editorindex).Repeat = 1
        Else
            Quest(Editorindex).Repeat = 0
        End If
    End Sub

    Private Sub ChkQuestCancel_CheckedChanged(sender As Object, e As EventArgs) Handles chkQuestCancel.CheckedChanged
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        If chkQuestCancel.Checked = True Then
            Quest(Editorindex).Cancelable = 1
        Else
            Quest(Editorindex).Cancelable = 0
        End If
    End Sub

#Region "Rewards"

    Private Sub NudExpReward_ValueChanged(sender As Object, e As EventArgs) Handles nudExpReward.ValueChanged
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        Quest(Editorindex).RewardExp = nudExpReward.Value
    End Sub

    Private Sub BtnAddReward_Click(sender As Object, e As EventArgs) Handles btnAddReward.Click
        If Editorindex <= 0 OrElse Editorindex > MaxQuests Then Exit Sub

        If cmbItemReward.SelectedIndex < 0 Then Exit Sub

        Quest(Editorindex).RewardCount = Quest(Editorindex).RewardCount + 1

        ReDim Preserve Quest(Editorindex).RewardItem(Quest(Editorindex).RewardCount)
        ReDim Preserve Quest(Editorindex).RewardItemAmount(Quest(Editorindex).RewardCount)

        Quest(Editorindex).RewardItem(Quest(Editorindex).RewardCount) = cmbItemReward.SelectedIndex
        Quest(Editorindex).RewardItemAmount(Quest(Editorindex).RewardCount) = nudItemRewValue.Value

        lstRewards.Items.Clear()
        For i = 1 To Quest(Editorindex).RewardCount
            lstRewards.Items.Add(i & ":" & Quest(Editorindex).RewardItemAmount(i) & " X " & Trim(Item(Quest(Editorindex).RewardItem(i)).Name))
        Next
    End Sub

    Private Sub BtnRemoveReward_Click(sender As Object, e As EventArgs) Handles btnRemoveReward.Click
        Dim tmpRewardItem() As Integer, tmpRewardItemIndex() As Integer

        If lstRewards.SelectedIndex < 0 Then Exit Sub
        If Quest(Editorindex).RewardCount <= 0 Then Exit Sub

        ReDim tmpRewardItem(Quest(Editorindex).RewardCount - 1)
        ReDim tmpRewardItemIndex(Quest(Editorindex).RewardCount - 1)

        For i = 1 To Quest(Editorindex).RewardCount
            If Not i = lstRewards.SelectedIndex + 1 Then
                tmpRewardItem(i) = Quest(Editorindex).RewardItem(i)
                tmpRewardItemIndex(i) = Quest(Editorindex).RewardItemAmount(i)
            End If
        Next

        Quest(Editorindex).RewardCount = Quest(Editorindex).RewardCount - 1

        ReDim Quest(Editorindex).RewardItem(Quest(Editorindex).RewardCount)
        ReDim Quest(Editorindex).RewardItemAmount(Quest(Editorindex).RewardCount)

        For i = 1 To Quest(Editorindex).RewardCount
            Quest(Editorindex).RewardItem(i) = tmpRewardItem(i)
            Quest(Editorindex).RewardItemAmount(i) = tmpRewardItemIndex(i)
        Next

        lstRewards.Items.Clear()
        For i = 1 To Quest(Editorindex).RewardCount
            lstRewards.Items.Add(i & ":" & Quest(Editorindex).RewardItemAmount(i) & " X " & Trim(Item(Quest(Editorindex).RewardItem(i)).Name))
        Next

    End Sub

#End Region

#Region "Tasks"

    Private Sub LstTasks_DoubleClick(sender As Object, e As EventArgs) Handles lstTasks.DoubleClick
        If lstTasks.SelectedIndex < 0 Then Exit Sub

        SelectedTask = lstTasks.SelectedIndex + 1
        LoadTask(Editorindex, SelectedTask)
        fraTasks.Visible = True
        fraTasks.BringToFront()
    End Sub

    Private Sub BtnAddTask_Click(sender As Object, e As EventArgs) Handles btnAddTask.Click
        Quest(Editorindex).TaskCount = Quest(Editorindex).TaskCount + 1

        ReDim Preserve Quest(Editorindex).Task(Quest(Editorindex).TaskCount)

        SelectedTask = Quest(Editorindex).TaskCount

        LoadTask(Editorindex, SelectedTask)

        fraTasks.Visible = True
        fraTasks.BringToFront()
    End Sub

    Private Sub BtnRemoveTask_Click(sender As Object, e As EventArgs) Handles btnRemoveTask.Click
        Dim i As Integer, tmptask() As TaskRec

        If lstTasks.SelectedIndex < 0 Then Exit Sub
        If Quest(Editorindex).TaskCount <= 0 Then Exit Sub

        ReDim tmptask(Quest(Editorindex).TaskCount - 1)

        For i = 1 To Quest(Editorindex).TaskCount
            If Not i = lstTasks.SelectedIndex + 1 Then
                tmptask(i) = Quest(Editorindex).Task(i)
            End If
        Next

        Quest(Editorindex).TaskCount = Quest(Editorindex).TaskCount - 1

        ReDim Quest(Editorindex).Task(Quest(Editorindex).TaskCount)

        For i = 1 To Quest(Editorindex).TaskCount
            If Not i = lstTasks.SelectedIndex + 1 Then
                Quest(Editorindex).Task(i) = tmptask(i)
            End If
        Next

        lstTasks.Items.Clear()
        For i = 1 To Quest(Editorindex).TaskCount
            lstTasks.Items.Add(i & ":" & Quest(Editorindex).Task(i).TaskLog)
        Next

    End Sub

    Private Sub BtnSaveTask_Click(sender As Object, e As EventArgs) Handles btnSaveTask.Click

        If lstTasks.SelectedIndex < 0 Then
            SelectedTask = Quest(Editorindex).TaskCount
        Else
            SelectedTask = lstTasks.SelectedIndex + 1
        End If

        Quest(Editorindex).Task(SelectedTask).TaskLog = Trim$(txtTaskLog.Text)
        Quest(Editorindex).Task(SelectedTask).Speech = txtTaskSpeech.Text

        If chkEnd.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).QuestEnd = 1
        Else
            Quest(Editorindex).Task(SelectedTask).QuestEnd = 0
        End If

        Quest(Editorindex).Task(SelectedTask).Npc = cmbNpc.SelectedIndex
        Quest(Editorindex).Task(SelectedTask).Item = cmbItem.SelectedIndex
        Quest(Editorindex).Task(SelectedTask).Map = cmbMap.SelectedIndex
        Quest(Editorindex).Task(SelectedTask).Resource = cmbResource.SelectedIndex
        Quest(Editorindex).Task(SelectedTask).Amount = nudAmount.Value

        If optTask0.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 0
        ElseIf optTask1.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 1
        ElseIf optTask2.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 2
        ElseIf optTask3.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 3
        ElseIf optTask4.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 4
        ElseIf optTask5.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 5
        ElseIf optTask6.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 6
        ElseIf optTask7.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 7
        End If

        lstTasks.Items.Clear()
        For i = 1 To Quest(Editorindex).TaskCount
            lstTasks.Items.Add(i & ":" & Quest(Editorindex).Task(i).TaskLog)
        Next

        fraTasks.Visible = False
    End Sub

    Private Sub BtnCancelTask_Click(sender As Object, e As EventArgs) Handles btnCancelTask.Click
        Quest(Editorindex).TaskCount = Quest(Editorindex).TaskCount - 1

        ReDim Quest(Editorindex).Task(Quest(Editorindex).TaskCount)

        SelectedTask = Quest(Editorindex).TaskCount
        fraTasks.Visible = False
    End Sub

    Private Sub OptTask0_CheckedChanged(sender As Object, e As EventArgs) Handles optTask0.CheckedChanged
        If optTask0.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 0
            Quest(Editorindex).Task(SelectedTask).TaskType = 0
            LoadTask(Editorindex, SelectedTask)
        End If
    End Sub

    Private Sub OptTask1_CheckedChanged(sender As Object, e As EventArgs) Handles optTask1.CheckedChanged
        If optTask1.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 1
            Quest(Editorindex).Task(SelectedTask).TaskType = QuestType.Slay
            LoadTask(Editorindex, SelectedTask)
            cmbNpc.Enabled = True
        Else
            cmbNpc.Enabled = False
        End If
    End Sub

    Private Sub OptTask2_CheckedChanged(sender As Object, e As EventArgs) Handles optTask2.CheckedChanged
        If optTask2.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 2
            Quest(Editorindex).Task(SelectedTask).TaskType = QuestType.Collect
            LoadTask(Editorindex, SelectedTask)
            cmbItem.Enabled = True
        Else
            cmbItem.Enabled = False
        End If
    End Sub

    Private Sub OptTask3_CheckedChanged(sender As Object, e As EventArgs) Handles optTask3.CheckedChanged
        If optTask3.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 3
            Quest(Editorindex).Task(SelectedTask).TaskType = QuestType.Talk
            LoadTask(Editorindex, SelectedTask)
            cmbNpc.Enabled = True
        Else
            cmbNpc.Enabled = False
        End If
    End Sub

    Private Sub OptTask4_CheckedChanged(sender As Object, e As EventArgs) Handles optTask4.CheckedChanged
        If optTask4.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 4
            Quest(Editorindex).Task(SelectedTask).TaskType = QuestType.Reach
            LoadTask(Editorindex, SelectedTask)
            cmbMap.Enabled = True
        Else
            cmbMap.Enabled = False
        End If
    End Sub

    Private Sub OptTask5_CheckedChanged(sender As Object, e As EventArgs) Handles optTask5.CheckedChanged
        If optTask5.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 5
            Quest(Editorindex).Task(SelectedTask).TaskType = QuestType.Give
            LoadTask(Editorindex, SelectedTask)
            cmbItem.Enabled = True
        Else
            cmbItem.Enabled = False
        End If
    End Sub

    Private Sub OptTask6_CheckedChanged(sender As Object, e As EventArgs) Handles optTask6.CheckedChanged
        If optTask6.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 6
            Quest(Editorindex).Task(SelectedTask).TaskType = QuestType.Kill
            LoadTask(Editorindex, SelectedTask)
            cmbResource.Enabled = True
            nudAmount.Enabled = True
        Else
            cmbResource.Enabled = False
        End If
    End Sub

    Private Sub OptTask7_CheckedChanged(sender As Object, e As EventArgs) Handles optTask7.CheckedChanged
        If optTask7.Checked = True Then
            Quest(Editorindex).Task(SelectedTask).Order = 7
            Quest(Editorindex).Task(SelectedTask).TaskType = QuestType.Gather
            LoadTask(Editorindex, SelectedTask)
            cmbNpc.Enabled = True
        Else
            cmbNpc.Enabled = False
        End If
    End Sub

#End Region

#Region "Requirements"

    Private Sub BtnAddRequirement_Click(sender As Object, e As EventArgs) Handles btnAddRequirement.Click
        Quest(Editorindex).ReqCount = Quest(Editorindex).ReqCount + 1

        ReDim Quest(Editorindex).Requirement(Quest(Editorindex).ReqCount)
        ReDim Quest(Editorindex).RequirementIndex(Quest(Editorindex).ReqCount)

        fraRequirements.Visible = True
        fraRequirements.BringToFront()
    End Sub

    Private Sub BtnRemoveRequirement_Click(sender As Object, e As EventArgs) Handles btnRemoveRequirement.Click
        Dim i As Integer, tmpRequirement() As Integer, tmpRequirementIndex() As Integer

        If lstRequirements.SelectedIndex < 0 Then Exit Sub

        ReDim tmpRequirement(Quest(Editorindex).ReqCount - 1)
        ReDim tmpRequirementIndex(Quest(Editorindex).ReqCount - 1)

        For i = 1 To Quest(Editorindex).ReqCount
            If Not i = lstRequirements.SelectedIndex + 1 Then
                tmpRequirement(i) = Quest(Editorindex).Requirement(i)
                tmpRequirementIndex(i) = Quest(Editorindex).RequirementIndex(i)
            End If
        Next

        Quest(Editorindex).ReqCount = Quest(Editorindex).ReqCount - 1

        ReDim Quest(Editorindex).Requirement(Quest(Editorindex).ReqCount)
        ReDim Quest(Editorindex).RequirementIndex(Quest(Editorindex).ReqCount)

        For i = 1 To Quest(Editorindex).ReqCount
            If Not i = lstRequirements.SelectedIndex + 1 Then
                Quest(Editorindex).Requirement(i) = tmpRequirement(i)
                Quest(Editorindex).RequirementIndex(i) = tmpRequirementIndex(i)
            End If
        Next

        lstRequirements.Items.Clear()
        For i = 1 To Quest(Editorindex).ReqCount
            Select Case Quest(Editorindex).Requirement(i)
                Case 1
                    lstRequirements.Items.Add(i & ":" & "Item Requirement: " & Trim(Item(Quest(Editorindex).RequirementIndex(i)).Name))
                Case 2
                    lstRequirements.Items.Add(i & ":" & "Quest Requirement: " & Trim(Quest(Quest(Editorindex).RequirementIndex(i)).Name))
                Case 3
                    lstRequirements.Items.Add(i & ":" & "Class Requirement: " & Trim(Classes(Quest(Editorindex).RequirementIndex(i)).Name))
                Case Else
                    lstRequirements.Items.Add(i & ":")
            End Select

        Next
    End Sub

    Private Sub LstRequirements_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRequirements.SelectedIndexChanged
        If lstRequirements.SelectedIndex < 0 Then Exit Sub

        LoadRequirement(Editorindex, lstRequirements.SelectedIndex + 1)
        fraRequirements.Visible = True
        fraRequirements.BringToFront()
    End Sub

    Private Sub BtnRequirementSave_Click(sender As Object, e As EventArgs) Handles btnRequirementSave.Click
        If rdbNoneReq.Checked = True Then
            Quest(Editorindex).Requirement(Quest(Editorindex).ReqCount) = 0
            Quest(Editorindex).RequirementIndex(Quest(Editorindex).ReqCount) = 0
        ElseIf rdbItemReq.Checked = True Then
            Quest(Editorindex).Requirement(Quest(Editorindex).ReqCount) = 1
            Quest(Editorindex).RequirementIndex(Quest(Editorindex).ReqCount) = cmbItemReq.SelectedIndex
        ElseIf rdbQuestReq.Checked = True Then
            Quest(Editorindex).Requirement(Quest(Editorindex).ReqCount) = 2
            Quest(Editorindex).RequirementIndex(Quest(Editorindex).ReqCount) = cmbQuestReq.SelectedIndex
        ElseIf rdbClassReq.Checked = True Then
            Quest(Editorindex).Requirement(Quest(Editorindex).ReqCount) = 3
            Quest(Editorindex).RequirementIndex(Quest(Editorindex).ReqCount) = cmbClassReq.SelectedIndex
        End If

        lstRequirements.Items.Clear()
        For i = 1 To Quest(Editorindex).ReqCount
            Select Case Quest(Editorindex).Requirement(i)
                Case 1
                    lstRequirements.Items.Add(i & ":" & "Item Requirement: " & Trim(Item(Quest(Editorindex).RequirementIndex(i)).Name))
                Case 2
                    lstRequirements.Items.Add(i & ":" & "Quest Requirement: " & Trim(Quest(Quest(Editorindex).RequirementIndex(i)).Name))
                Case 3
                    lstRequirements.Items.Add(i & ":" & "Class Requirement: " & Trim(Classes(Quest(Editorindex).RequirementIndex(i)).Name))
                Case Else
                    lstRequirements.Items.Add(i & ":")
            End Select

        Next

        fraRequirements.Visible = False
    End Sub

    Private Sub BtnRequirementCancel_Click(sender As Object, e As EventArgs) Handles btnRequirementCancel.Click
        fraRequirements.Visible = False
    End Sub

    Private Sub RdbNoneReq_CheckedChanged(sender As Object, e As EventArgs) Handles rdbNoneReq.CheckedChanged
        cmbItemReq.SelectedIndex = 0
        cmbItemReq.Enabled = False

        cmbQuestReq.SelectedIndex = 0
        cmbQuestReq.Enabled = False

        cmbClassReq.SelectedIndex = 0
        cmbClassReq.Enabled = False
    End Sub

    Private Sub RdbItemReq_CheckedChanged(sender As Object, e As EventArgs) Handles rdbItemReq.CheckedChanged
        cmbItemReq.Enabled = True
    End Sub

    Private Sub RdbQuestReq_CheckedChanged(sender As Object, e As EventArgs) Handles rdbQuestReq.CheckedChanged
        cmbQuestReq.Enabled = True
    End Sub

    Private Sub RdbClassReq_CheckedChanged(sender As Object, e As EventArgs) Handles rdbClassReq.CheckedChanged
        cmbClassReq.Enabled = True
    End Sub

#End Region

End Class