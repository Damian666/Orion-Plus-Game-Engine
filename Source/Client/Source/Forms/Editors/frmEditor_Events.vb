Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class FrmEditor_Events

#Region "Frm Code"

    Sub ClearConditionFrame()
        Dim i As Integer

        cmbCondition_PlayerVarIndex.Enabled = False
        cmbCondition_PlayerVarIndex.Items.Clear()

        For i = 1 To MaxVariables
            cmbCondition_PlayerVarIndex.Items.Add(i & ". " & Variables(i))
        Next
        cmbCondition_PlayerVarIndex.SelectedIndex = 0
        cmbCondition_PlayerVarCompare.SelectedIndex = 0
        cmbCondition_PlayerVarCompare.Enabled = False
        nudCondition_PlayerVarCondition.Enabled = False
        nudCondition_PlayerVarCondition.Value = 0
        cmbCondition_PlayerSwitch.Enabled = False
        cmbCondition_PlayerSwitch.Items.Clear()

        For i = 1 To MaxSwitches
            cmbCondition_PlayerSwitch.Items.Add(i & ". " & Switches(i))
        Next
        cmbCondition_PlayerSwitch.SelectedIndex = 0
        cmbCondtion_PlayerSwitchCondition.Enabled = False
        cmbCondtion_PlayerSwitchCondition.SelectedIndex = 0
        cmbCondition_HasItem.Enabled = False
        cmbCondition_HasItem.Items.Clear()

        For i = 1 To MAX_ITEMS
            cmbCondition_HasItem.Items.Add(i & ". " & Trim$(Item(i).Name))
        Next
        cmbCondition_HasItem.SelectedIndex = 0
        nudCondition_HasItem.Enabled = False
        nudCondition_HasItem.Value = 1
        cmbCondition_ClassIs.Enabled = False
        cmbCondition_ClassIs.Items.Clear()

        For i = 1 To MAX_CLASSES
            cmbCondition_ClassIs.Items.Add(i & ". " & CStr(Classes(i).Name))
        Next
        cmbCondition_ClassIs.SelectedIndex = 0
        cmbCondition_LearntSkill.Enabled = False
        cmbCondition_LearntSkill.Items.Clear()

        For i = 1 To MAX_SKILLS
            cmbCondition_LearntSkill.Items.Add(i & ". " & Trim$(Skill(i).Name))
        Next
        cmbCondition_LearntSkill.SelectedIndex = 0
        cmbCondition_LevelCompare.Enabled = False
        cmbCondition_LevelCompare.SelectedIndex = 0
        nudCondition_LevelAmount.Enabled = False
        nudCondition_LevelAmount.Value = 0
        If cmbCondition_SelfSwitch.Items.Count > 0 Then
            cmbCondition_SelfSwitch.SelectedIndex = 0
        End If

        cmbCondition_SelfSwitch.Enabled = False

        If cmbCondition_SelfSwitchCondition.Items.Count > 0 Then
            cmbCondition_SelfSwitchCondition.SelectedIndex = 0
        End If

        cmbCondition_SelfSwitchCondition.Enabled = False
        nudCondition_Quest.Enabled = False
        nudCondition_Quest.Value = 1
        fraConditions_Quest.Visible = False
        optCondition_Quest0.Checked = True
        cmbCondition_General.Enabled = True
        cmbCondition_General.SelectedIndex = 0
        nudCondition_QuestTask.Value = 1

        cmbCondition_Gender.Enabled = False

        cmbCondition_Time.Enabled = False
    End Sub

    Public Sub InitEventEditorForm()
        Dim i As Integer

        nudShowTextFace.Maximum = NumFaces
        nudShowChoicesFace.Maximum = NumFaces

        nudWPMap.Maximum = MAX_MAPS

        cmbSwitch.Items.Clear()

        For i = 1 To MaxSwitches
            cmbSwitch.Items.Add(i & ". " & Switches(i))
        Next
        cmbSwitch.SelectedIndex = 0
        cmbVariable.Items.Clear()

        For i = 1 To MaxVariables
            cmbVariable.Items.Add(i & ". " & Variables(i))
        Next
        cmbVariable.SelectedIndex = 0
        cmbChangeItemIndex.Items.Clear()

        For i = 1 To MAX_ITEMS
            cmbChangeItemIndex.Items.Add(Trim$(Item(i).Name))
        Next
        cmbChangeItemIndex.SelectedIndex = 0
        nudChangeLevel.Minimum = 1
        nudChangeLevel.Maximum = MAX_LEVELS
        nudChangeLevel.Value = 1
        cmbChangeSkills.Items.Clear()

        For i = 1 To MAX_SKILLS
            cmbChangeSkills.Items.Add(Trim$(Skill(i).Name))
        Next
        cmbChangeSkills.SelectedIndex = 0
        cmbChangeClass.Items.Clear()

        If MAX_CLASSES > 0 Then
            For i = 1 To MAX_CLASSES
                cmbChangeClass.Items.Add(Trim$(Classes(i).Name))
            Next
            cmbChangeClass.SelectedIndex = 0
        End If
        nudChangeSprite.Maximum = NumCharacters
        cmbPlayAnim.Items.Clear()

        For i = 1 To MAX_ANIMATIONS
            cmbPlayAnim.Items.Add(i & ". " & Trim$(Animation(i).Name))
        Next
        cmbPlayAnim.SelectedIndex = 0

        cmbPlayBGM.Items.Clear()

        If UBound(MusicCache) > 0 Then
            For i = 1 To UBound(MusicCache)
                cmbPlayBGM.Items.Add(MusicCache(i))
            Next
            cmbPlayBGM.SelectedIndex = 0
        Else

        End If
        cmbPlaySound.Items.Clear()

        If UBound(SoundCache) > 0 Then
            For i = 1 To UBound(SoundCache)
                cmbPlaySound.Items.Add(SoundCache(i))
            Next
            cmbPlaySound.SelectedIndex = 0
        Else

        End If
        cmbOpenShop.Items.Clear()

        For i = 1 To MAX_SHOPS
            cmbOpenShop.Items.Add(i & ". " & Trim$(Shop(i).Name))
        Next
        cmbOpenShop.SelectedIndex = 0
        cmbSpawnNpc.Items.Clear()

        For i = 1 To MAX_MAP_NPCS
            If Map.Npc(i) > 0 Then
                cmbSpawnNpc.Items.Add(i & ". " & Trim$(Npc(Map.Npc(i)).Name))
            Else
                cmbSpawnNpc.Items.Add(i & ". ")
            End If
        Next

        'set up all quest combo's
        cmbBeginQuest.Items.Clear()
        cmbCompleteQuest.Items.Clear()
        cmbEndQuest.Items.Clear()

        cmbBeginQuest.Items.Add("None")
        cmbCompleteQuest.Items.Add("None")
        cmbEndQuest.Items.Add("None")

        For i = 1 To MaxQuests
            cmbBeginQuest.Items.Add(i & ". " & Trim$(Quest(i).Name))
            cmbCompleteQuest.Items.Add(i & ". " & Trim$(Quest(i).Name))
            cmbEndQuest.Items.Add(i & ". " & Trim$(Quest(i).Name))
        Next

        cmbSpawnNpc.SelectedIndex = 0
        nudFogData0.Maximum = NumFogs
        cmbEventQuest.Items.Clear()
        cmbEventQuest.Items.Add("None")
        For i = 1 To MaxQuests
            cmbEventQuest.Items.Add(i & ". " & Trim$(Quest(i).Name))
        Next

        'If NumPics > 0 Then
        '    btnCommands45.Enabled = True
        '    scrlShowPicture.Maximum = NumPics
        '    cmbPicIndex.SelectedIndex = 0
        'Else

        'End If

        fraDialogue.Visible = False

        EditorEvent_DrawGraphic()
    End Sub

    Private Sub FrmEditor_Events_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Width = 824
        fraDialogue.Width = Width
        fraDialogue.Height = Height
        fraDialogue.Top = 0
        fraDialogue.Left = 0

        fraMoveRoute.Width = Width
        fraMoveRoute.Height = Height
        fraMoveRoute.Top = 0
        fraMoveRoute.Left = 0

        fraGraphic.Width = Width
        fraGraphic.Height = Height
        fraGraphic.Top = 0
        fraGraphic.Left = 0
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        EventEditorOK()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dispose()
    End Sub

    Private Sub TvCommands_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvCommands.AfterSelect
        Dim x As Integer

        fraDialogue.Width = Me.Width
        fraDialogue.Height = Me.Height

        fraDialogue.BringToFront()

        'MsgBox(tvCommands.SelectedNode.Text)

        Select Case tvCommands.SelectedNode.Text
           'Messages

            'show text
            Case "Show Text"
                txtShowText.Text = ""
                fraDialogue.Visible = True
                fraShowText.Visible = True
                nudShowTextFace.Value = 0
                nudShowTextFace.Maximum = NumFaces
                fraCommands.Visible = False
            'show choices
            Case "Show Choices"
                txtChoicePrompt.Text = ""
                txtChoices1.Text = ""
                txtChoices2.Text = ""
                txtChoices3.Text = ""
                txtChoices4.Text = ""
                nudShowChoicesFace.Value = 0
                fraDialogue.Visible = True
                fraShowChoices.Visible = True
                fraCommands.Visible = False
            'chatbox text
            Case "Add Chatbox Text"
                txtAddText_Text.Text = ""
                optAddText_Player.Checked = True
                fraDialogue.Visible = True
                fraAddText.Visible = True
                fraCommands.Visible = False
            'chat bubble
            Case "Show ChatBubble"
                txtChatbubbleText.Text = ""
                cmbChatBubbleTargetType.SelectedIndex = 0
                cmbChatBubbleTarget.Visible = False
                fraDialogue.Visible = True
                fraShowChatBubble.Visible = True
                fraCommands.Visible = False
        'event progression
            'player variable
            Case "Set Player Variable"
                nudVariableData0.Value = 0
                nudVariableData1.Value = 0
                nudVariableData2.Value = 0
                nudVariableData3.Value = 0
                nudVariableData4.Value = 0

                cmbVariable.SelectedIndex = 0
                optVariableAction0.Checked = True
                fraDialogue.Visible = True
                fraPlayerVariable.Visible = True
                fraCommands.Visible = False
            'player switch
            Case "Set Player Switch"
                cmbPlayerSwitchSet.SelectedIndex = 0
                cmbSwitch.SelectedIndex = 0
                fraDialogue.Visible = True
                fraPlayerSwitch.Visible = True
                fraCommands.Visible = False
            'self switch
            Case "Set Self Switch"
                cmbSetSelfSwitchTo.SelectedIndex = 0
                fraDialogue.Visible = True
                fraSetSelfSwitch.Visible = True
                fraCommands.Visible = False
        'flow control

            'conditional branch
            Case "Conditional Branch"
                fraDialogue.Visible = True
                fraConditionalBranch.Visible = True
                optCondition0.Checked = True
                ClearConditionFrame()
                cmbCondition_PlayerVarIndex.Enabled = True
                cmbCondition_PlayerVarCompare.Enabled = True
                nudCondition_PlayerVarCondition.Enabled = True
                fraCommands.Visible = False
            'Exit Event Process
            Case "Stop Event Processing"
                AddCommand(EventType.EvExitProcess)
                fraCommands.Visible = False
                fraDialogue.Visible = False
            'Label
            Case "Label"
                txtLabelName.Text = ""
                fraCreateLabel.Visible = True
                fraCommands.Visible = False
                fraDialogue.Visible = True
            'GoTo Label
            Case "GoTo Label"
                txtGotoLabel.Text = ""
                fraGoToLabel.Visible = True
                fraCommands.Visible = False
                fraDialogue.Visible = True
        'Player Control

            'Change Items
            Case "Change Items"
                cmbChangeItemIndex.SelectedIndex = 0
                optChangeItemSet.Checked = True
                nudChangeItemsAmount.Value = 0
                fraDialogue.Visible = True
                fraChangeItems.Visible = True
                fraCommands.Visible = False
            'Restore Hp
            Case "Restore HP"
                AddCommand(EventType.EvRestoreHp)
                fraCommands.Visible = False
                fraDialogue.Visible = False
            'Restore Mp
            Case "Restore Mp"
                AddCommand(EventType.EvRestoreMp)
                fraCommands.Visible = False
                fraDialogue.Visible = False
            'Level Up
            Case "Level Up"
                AddCommand(EventType.EvLevelUp)
                fraCommands.Visible = False
                fraDialogue.Visible = False
            'Change Level
            Case "Change Level"
                nudChangeLevel.Value = 1
                fraDialogue.Visible = True
                fraChangeLevel.Visible = True
                fraCommands.Visible = False
            'Change Skills
            Case "Change Skills"
                cmbChangeSkills.SelectedIndex = 0
                fraDialogue.Visible = True
                fraChangeSkills.Visible = True
                fraCommands.Visible = False
            'Change Class
            Case "Change Class"
                If MAX_CLASSES > 0 Then
                    If cmbChangeClass.Items.Count = 0 Then
                        cmbChangeClass.Items.Clear()

                        For i = 1 To MAX_CLASSES
                            cmbChangeClass.Items.Add(Trim$(Classes(i).Name))
                        Next
                        cmbChangeClass.SelectedIndex = 0
                    End If
                End If
                fraDialogue.Visible = True
                fraChangeClass.Visible = True
                fraCommands.Visible = False
            'Change Sprite
            Case "Change Sprite"
                nudChangeSprite.Value = 1
                fraDialogue.Visible = True
                fraChangeSprite.Visible = True
                fraCommands.Visible = False
            'Change Gender
            Case "Change Gender"
                optChangeSexMale.Checked = True
                fraDialogue.Visible = True
                fraChangeGender.Visible = True
                fraCommands.Visible = False
            'Change PK
            Case "Change PK"
                cmbSetPK.SelectedIndex = 0
                fraDialogue.Visible = True
                fraChangePK.Visible = True
                fraCommands.Visible = False
            'Give Exp
            Case "Give Experience"
                nudGiveExp.Value = 0
                fraDialogue.Visible = True
                fraGiveExp.Visible = True
                fraCommands.Visible = False
        'Movement

            'Warp Player
            Case "Warp Player"
                nudWPMap.Value = 0
                nudWPX.Value = 0
                nudWPY.Value = 0
                cmbWarpPlayerDir.SelectedIndex = 0
                fraDialogue.Visible = True
                fraPlayerWarp.Visible = True
                fraCommands.Visible = False
            'Set Move Route
            Case "Set Move Route"
                fraMoveRoute.Visible = True
                lstMoveRoute.Items.Clear()
                cmbEvent.Items.Clear()
                ReDim ListOfEvents(0 To Map.EventCount)
                ListOfEvents(0) = EditorEvent
                cmbEvent.Items.Add("This Event")
                cmbEvent.SelectedIndex = 0
                cmbEvent.Enabled = True
                For i = 1 To Map.EventCount
                    If i <> EditorEvent Then
                        cmbEvent.Items.Add(Trim$(Map.Events(i).Name))
                        x = x + 1
                        ListOfEvents(x) = i
                    End If
                Next
                IsMoveRouteCommand = True
                chkIgnoreMove.Checked = 0
                chkRepeatRoute.Checked = 0
                TempMoveRouteCount = 0
                ReDim TempMoveRoute(0)
                fraMoveRoute.Visible = True
                fraMoveRoute.BringToFront()
                fraCommands.Visible = False
            'Wait for Route Completion
            Case "Wait for Route Completion"
                cmbMoveWait.Items.Clear()
                ReDim ListOfEvents(0 To Map.EventCount)
                ListOfEvents(0) = EditorEvent
                cmbMoveWait.Items.Add("This Event")
                cmbMoveWait.SelectedIndex = 0
                cmbMoveWait.Enabled = True
                For i = 1 To Map.EventCount
                    If i <> EditorEvent Then
                        cmbMoveWait.Items.Add(Trim$(Map.Events(i).Name))
                        x = x + 1
                        ListOfEvents(x) = i
                    End If
                Next
                fraDialogue.Visible = True
                fraMoveRouteWait.Visible = True
                fraCommands.Visible = False
            'Force Spawn Npc
            Case "Force Spawn Npc"
                'lets populate the combobox
                cmbSpawnNpc.Items.Clear()
                For i = 1 To MAX_NPCS
                    cmbSpawnNpc.Items.Add(Trim(Npc(i).Name))
                Next
                cmbSpawnNpc.SelectedIndex = 0
                fraDialogue.Visible = True
                fraSpawnNpc.Visible = True
                fraCommands.Visible = False
            'Hold Player
            Case "Hold Player"
                AddCommand(EventType.EvHoldPlayer)
                fraCommands.Visible = False
                fraDialogue.Visible = False
            'Release Player
            Case "Release Player"
                AddCommand(EventType.EvReleasePlayer)
                fraCommands.Visible = False
                fraDialogue.Visible = False
        'Animation

            'Play Animation
            Case "Play Animation"
                cmbPlayAnimEvent.Items.Clear()

                For i = 1 To Map.EventCount
                    cmbPlayAnimEvent.Items.Add(i & ". " & Trim$(Map.Events(i).Name))
                Next
                cmbPlayAnimEvent.SelectedIndex = 0
                cmbAnimTargetType.SelectedIndex = 0
                cmbPlayAnim.SelectedIndex = 0
                nudPlayAnimTileX.Value = 0
                nudPlayAnimTileY.Value = 0
                nudPlayAnimTileX.Maximum = Map.MaxX
                nudPlayAnimTileY.Maximum = Map.MaxY
                fraDialogue.Visible = True
                fraPlayAnimation.Visible = True
                fraCommands.Visible = False
                lblPlayAnimX.Visible = False
                lblPlayAnimY.Visible = False
                nudPlayAnimTileX.Visible = False
                nudPlayAnimTileY.Visible = False
                cmbPlayAnimEvent.Visible = False
        'Quests

            'Begin Quest
            Case "Begin Quest"
                cmbBeginQuest.SelectedIndex = 0
                fraDialogue.Visible = True
                fraBeginQuest.Visible = True
                fraCommands.Visible = False
            'Complete Task
            Case "Complete Task"
                cmbCompleteQuest.SelectedIndex = 0
                nudCompleteQuestTask.Value = 0
                fraDialogue.Visible = True
                fraCompleteTask.Visible = True
                fraCommands.Visible = False
            'End Quest
            Case "End Quest"
                cmbEndQuest.SelectedIndex = 0
                fraDialogue.Visible = True
                fraEndQuest.Visible = True
                fraCommands.Visible = False
        'Map Functions

            'Set Fog
            Case "Set Fog"
                nudFogData0.Value = 0
                nudFogData1.Value = 0
                nudFogData2.Value = 0
                fraDialogue.Visible = True
                fraSetFog.Visible = True
                fraCommands.Visible = False
            'Set Weather
            Case "Set Weather"
                CmbWeather.SelectedIndex = 0
                nudWeatherIntensity.Value = 0
                fraDialogue.Visible = True
                fraSetWeather.Visible = True
                fraCommands.Visible = False
            'Set Map Tinting
            Case "Set Map Tinting"
                nudMapTintData0.Value = 0
                nudMapTintData1.Value = 0
                nudMapTintData2.Value = 0
                nudMapTintData3.Value = 0
                fraDialogue.Visible = True
                fraMapTint.Visible = True
                fraCommands.Visible = False
        'Music and Sound

            'PlayBGM
            Case "Play BGM"
                cmbPlayBGM.SelectedIndex = 0
                fraDialogue.Visible = True
                fraPlayBGM.Visible = True
                fraCommands.Visible = False
            'Stop BGM
            Case "Stop BGM"
                AddCommand(EventType.EvFadeoutBgm)
                fraCommands.Visible = False
                fraDialogue.Visible = False
            'Play Sound
            Case "Play Sound"
                cmbPlaySound.SelectedIndex = 0
                fraDialogue.Visible = True
                fraPlaySound.Visible = True
                fraCommands.Visible = False
            'Stop Sounds
            Case "Stop Sounds"
                AddCommand(EventType.EvStopSound)
                fraCommands.Visible = False
                fraDialogue.Visible = False
        'Etc...

            'Wait...
            Case "Wait..."
                nudWaitAmount.Value = 1
                fraDialogue.Visible = True
                fraSetWait.Visible = True
                fraCommands.Visible = False
            'Set Access
            Case "Set Access"
                cmbSetAccess.SelectedIndex = 0
                fraDialogue.Visible = True
                fraSetAccess.Visible = True
                fraCommands.Visible = False
            'Custom Script
            Case "Custom Script"
                nudCustomScript.Value = 0
                fraDialogue.Visible = True
                fraCustomScript.Visible = True
                fraCommands.Visible = False

            'Shop, bank etc

            'Open bank
            Case "Open bank"
                AddCommand(EventType.EvOpenBank)
                fraCommands.Visible = False
                fraDialogue.Visible = False
            'Open shop
            Case "Open shop"
                fraDialogue.Visible = True
                fraOpenShop.Visible = True
                cmbOpenShop.SelectedIndex = 0
                fraCommands.Visible = False
            'Open Mail
            Case 45
                AddCommand(EventType.EvOpenMail)
                fraCommands.Visible = False
                fraDialogue.Visible = False
        'cutscene options

            'Fade in
            Case "Fade In"
                AddCommand(EventType.EvFadeIn)
                fraCommands.Visible = False
                fraDialogue.Visible = False
            'Fade out
            Case "Fade Out"
                AddCommand(EventType.EvFadeOut)
                fraCommands.Visible = False
                fraDialogue.Visible = False
            'Flash white
            Case 48
                AddCommand(EventType.EvFlashWhite)
                fraCommands.Visible = False
                fraDialogue.Visible = False
            'Show pic
            Case 49
                cmbPicIndex.SelectedIndex = 0
                nudShowPicture.Value = 1
                cmbPicLoc.SelectedIndex = 0
                nudPicOffsetX.Value = 0
                nudPicOffsetY.Value = 0
                fraDialogue.Visible = True
                fraShowPic.Visible = True
                fraCommands.Visible = False
            'Hide pic
            Case 50
                nudHidePic.Value = 0
                fraDialogue.Visible = True
                fraHidePic.Visible = True
                fraCommands.Visible = False

        End Select
    End Sub

    Private Sub BtnCancelCommand_Click(sender As Object, e As EventArgs) Handles btnCancelCommand.Click
        fraCommands.Visible = False
    End Sub

#End Region

#Region "Page Buttons"

    Private Sub TabPages_Click(sender As Object, e As EventArgs) Handles tabPages.Click
        CurPageNum = tabPages.SelectedIndex + 1
        EventEditorLoadPage(CurPageNum)
    End Sub

    Private Sub BtnNewPage_Click(sender As Object, e As EventArgs) Handles btnNewPage.Click
        Dim pageCount As Integer, i As Integer

        If chkGlobal.Checked = True Then
            MsgBox("You cannot have multiple pages on global events!")
            Exit Sub
        End If

        pageCount = TmpEvent.PageCount + 1

        ' redim the array
        ReDim Preserve TmpEvent.Pages(pageCount)

        TmpEvent.PageCount = pageCount

        ' set the tabs
        tabPages.TabPages.Clear()

        For i = 1 To TmpEvent.PageCount
            tabPages.TabPages.Add(Str(i))
        Next
        btnDeletePage.Enabled = True
    End Sub

    Private Sub BtnCopyPage_Click(sender As Object, e As EventArgs) Handles btnCopyPage.Click
        CopyEventPage = TmpEvent.Pages(CurPageNum)
        btnPastePage.Enabled = True
    End Sub

    Private Sub BtnPastePage_Click(sender As Object, e As EventArgs) Handles btnPastePage.Click
        TmpEvent.Pages(CurPageNum) = CopyEventPage
        EventEditorLoadPage(CurPageNum)
    End Sub

    Private Sub BtnDeletePage_Click(sender As Object, e As EventArgs) Handles btnDeletePage.Click
        TmpEvent.Pages(CurPageNum) = Nothing
        ' move everything else down a notch
        If CurPageNum < TmpEvent.PageCount Then
            For i = CurPageNum To TmpEvent.PageCount - 1
                TmpEvent.Pages(i + 1) = TmpEvent.Pages(i)
            Next
        End If
        TmpEvent.PageCount = TmpEvent.PageCount - 1
        ' set the tabs
        tabPages.TabPages.Clear()

        For i = 1 To TmpEvent.PageCount
            tabPages.TabPages.Add("0", Str(i), "")
        Next
        ' set the tab back
        If CurPageNum <= TmpEvent.PageCount Then
            tabPages.SelectedIndex = tabPages.TabPages.IndexOfKey(CurPageNum)
        Else
            tabPages.SelectedIndex = tabPages.TabPages.IndexOfKey(TmpEvent.PageCount)
        End If
        ' make sure we disable
        If TmpEvent.PageCount <= 1 Then
            btnDeletePage.Enabled = False
        End If

    End Sub

    Private Sub BtnClearPage_Click(sender As Object, e As EventArgs) Handles btnClearPage.Click
        TmpEvent.Pages(CurPageNum) = Nothing
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        TmpEvent.Name = Trim$(txtName.Text)
    End Sub

#End Region

#Region "Conditions"

    Private Sub ChkPlayerVar_CheckedChanged(sender As Object, e As EventArgs) Handles chkPlayerVar.CheckedChanged
        If chkPlayerVar.Checked = True Then
            cmbPlayerVar.Enabled = False
            nudPlayerVariable.Enabled = False
            cmbPlayervarCompare.Enabled = False
            TmpEvent.Pages(CurPageNum).ChkVariable = 0
        Else
            cmbPlayerVar.Enabled = True
            nudPlayerVariable.Enabled = True
            cmbPlayervarCompare.Enabled = True
            TmpEvent.Pages(CurPageNum).ChkVariable = 1
        End If
    End Sub

    Private Sub CmbPlayerVar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlayerVar.SelectedIndexChanged
        If cmbPlayerVar.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).Variableindex = cmbPlayerVar.SelectedIndex
    End Sub

    Private Sub CmbPlayervarCompare_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlayervarCompare.SelectedIndexChanged
        If cmbPlayervarCompare.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).VariableCompare = cmbPlayervarCompare.SelectedIndex
    End Sub

    Private Sub NudPlayerVariable_ValueChanged(sender As Object, e As EventArgs) Handles nudPlayerVariable.ValueChanged
        TmpEvent.Pages(CurPageNum).VariableCondition = nudPlayerVariable.Value
    End Sub

    Private Sub ChkPlayerSwitch_CheckedChanged(sender As Object, e As EventArgs) Handles chkPlayerSwitch.CheckedChanged
        If chkPlayerSwitch.Checked = True Then
            cmbPlayerSwitch.Enabled = True
            cmbPlayerSwitchCompare.Enabled = True
            TmpEvent.Pages(CurPageNum).ChkSwitch = 1
        Else
            cmbPlayerSwitch.Enabled = False
            cmbPlayerSwitchCompare.Enabled = False
            TmpEvent.Pages(CurPageNum).ChkSwitch = 0
        End If
    End Sub

    Private Sub CmbPlayerSwitch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlayerSwitch.SelectedIndexChanged
        If cmbPlayerSwitch.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).Switchindex = cmbPlayerSwitch.SelectedIndex
    End Sub

    Private Sub CmbPlayerSwitchCompare_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlayerSwitchCompare.SelectedIndexChanged
        If cmbPlayerSwitchCompare.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).SwitchCompare = cmbPlayerSwitchCompare.SelectedIndex
    End Sub

    Private Sub ChkHasItem_CheckedChanged(sender As Object, e As EventArgs) Handles chkHasItem.CheckedChanged
        If chkHasItem.Checked = True Then
            TmpEvent.Pages(CurPageNum).ChkHasItem = 1
            cmbHasItem.Enabled = True
        Else
            TmpEvent.Pages(CurPageNum).ChkHasItem = 0
            cmbHasItem.Enabled = False
        End If

    End Sub

    Private Sub CmbHasItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbHasItem.SelectedIndexChanged
        If cmbHasItem.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).HasItemindex = cmbHasItem.SelectedIndex
        TmpEvent.Pages(CurPageNum).HasItemAmount = nudCondition_HasItem.Value
    End Sub

    Private Sub ChkSelfSwitch_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelfSwitch.CheckedChanged
        If chkSelfSwitch.Checked = True Then
            cmbSelfSwitch.Enabled = True
            cmbSelfSwitchCompare.Enabled = True
            TmpEvent.Pages(CurPageNum).ChkSelfSwitch = 1
        Else
            cmbSelfSwitch.Enabled = False
            cmbSelfSwitchCompare.Enabled = False
            TmpEvent.Pages(CurPageNum).ChkSelfSwitch = 0
        End If
    End Sub

    Private Sub CmbSelfSwitch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSelfSwitch.SelectedIndexChanged
        If cmbSelfSwitch.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).SelfSwitchindex = cmbSelfSwitch.SelectedIndex
    End Sub

    Private Sub CmbSelfSwitchCompare_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSelfSwitchCompare.SelectedIndexChanged
        If cmbSelfSwitchCompare.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).SelfSwitchCompare = cmbSelfSwitchCompare.SelectedIndex
    End Sub

#End Region

#Region "Graphic"

    Private Sub PicGraphic_Click(sender As Object, e As EventArgs) Handles picGraphic.Click
        fraGraphic.Top = 0
        fraGraphic.Left = 0
        fraGraphic.Width = Me.Width
        fraGraphic.Height = Me.Height
        fraGraphic.BringToFront()
        fraGraphic.Visible = True
        GraphicSelType = 0
    End Sub

    Private Sub CmbGraphic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGraphic.SelectedIndexChanged
        If cmbGraphic.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).GraphicType = cmbGraphic.SelectedIndex
        ' set the max on the scrollbar
        Select Case cmbGraphic.SelectedIndex
            Case 0 ' None
                nudGraphic.Value = 1
                nudGraphic.Enabled = False
            Case 1 ' character
                nudGraphic.Maximum = NumCharacters
                nudGraphic.Enabled = True
            Case 2 ' Tileset
                nudGraphic.Maximum = NumTileSets
                nudGraphic.Enabled = True
        End Select

        If TmpEvent.Pages(CurPageNum).GraphicType = 1 Then
            If Me.nudGraphic.Value <= 0 OrElse Me.nudGraphic.Value > NumCharacters Then Exit Sub

        ElseIf TmpEvent.Pages(CurPageNum).GraphicType = 2 Then
            If Me.nudGraphic.Value <= 0 OrElse Me.nudGraphic.Value > NumTileSets Then Exit Sub

        End If
        EditorEvent_DrawGraphic()
    End Sub

    Private Sub NudGraphic_ValueChanged(sender As Object, e As EventArgs) Handles nudGraphic.ValueChanged
        EditorEvent_DrawGraphic()
    End Sub

    Private Sub PicGraphicSel_Click(sender As Object, e As MouseEventArgs) Handles picGraphicSel.Click
        Dim X As Integer
        Dim Y As Integer

        X = e.Location.X
        Y = e.Location.Y

        Dim selW As Integer = Math.Ceiling(X \ PicX) - GraphicSelX
        Dim selH As Integer = Math.Ceiling(Y \ PicY) - GraphicSelY

        If Me.cmbGraphic.SelectedIndex = 2 Then
            'Tileset... hard one....
            If Control.ModifierKeys = Keys.Shift Then
                If GraphicSelX > -1 AndAlso GraphicSelY > -1 Then
                    If selW >= 0 AndAlso selH >= 0 Then
                        GraphicSelX2 = selW + 1
                        GraphicSelY2 = selH + 1
                    End If
                End If
            Else
                GraphicSelX = Math.Ceiling(X \ PicX)
                GraphicSelY = Math.Ceiling(Y \ PicY)
                GraphicSelX2 = 1
                GraphicSelY2 = 1
            End If
        ElseIf Me.cmbGraphic.SelectedIndex = 1 Then
            GraphicSelX = X
            GraphicSelY = Y
            GraphicSelX2 = 0
            GraphicSelY2 = 0
            If Me.nudGraphic.Value <= 0 OrElse Me.nudGraphic.Value > NumCharacters Then Exit Sub
            For i = 0 To 3
                If GraphicSelX >= ((CharacterGfxInfo(Me.nudGraphic.Value).Width / 4) * i) AndAlso GraphicSelX < ((CharacterGfxInfo(Me.nudGraphic.Value).Width / 4) * (i + 1)) Then
                    GraphicSelX = i
                End If
            Next
            For i = 0 To 3
                If GraphicSelY >= ((CharacterGfxInfo(Me.nudGraphic.Value).Height / 4) * i) AndAlso GraphicSelY < ((CharacterGfxInfo(Me.nudGraphic.Value).Height / 4) * (i + 1)) Then
                    GraphicSelY = i
                End If
            Next
        End If
        EditorEvent_DrawGraphic()
    End Sub

    Private Sub BtnGraphicOk_Click(sender As Object, e As EventArgs) Handles btnGraphicOk.Click
        If GraphicSelType = 0 Then
            TmpEvent.Pages(CurPageNum).GraphicType = cmbGraphic.SelectedIndex
            TmpEvent.Pages(CurPageNum).Graphic = nudGraphic.Value
            TmpEvent.Pages(CurPageNum).GraphicX = GraphicSelX
            TmpEvent.Pages(CurPageNum).GraphicY = GraphicSelY
            TmpEvent.Pages(CurPageNum).GraphicX2 = GraphicSelX2
            TmpEvent.Pages(CurPageNum).GraphicY2 = GraphicSelY2
        Else
            AddMoveRouteCommand(42)
            GraphicSelType = 0
        End If
        fraGraphic.Visible = False
    End Sub

    Private Sub BtnGraphicCancel_Click(sender As Object, e As EventArgs) Handles btnGraphicCancel.Click
        fraGraphic.Visible = False
    End Sub

#End Region

#Region "Movement"

    Private Sub CmbMoveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMoveType.SelectedIndexChanged
        If cmbMoveType.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).MoveType = cmbMoveType.SelectedIndex
        If cmbMoveType.SelectedIndex = 2 Then
            btnMoveRoute.Enabled = True
        Else
            btnMoveRoute.Enabled = False
        End If
    End Sub

    Private Sub BtnMoveRoute_Click(sender As Object, e As EventArgs) Handles btnMoveRoute.Click
        fraMoveRoute.BringToFront()
        lstMoveRoute.Items.Clear()
        cmbEvent.Items.Clear()
        cmbEvent.Items.Add("This Event")
        cmbEvent.SelectedIndex = 0
        cmbEvent.Enabled = False
        IsMoveRouteCommand = False
        chkIgnoreMove.Checked = TmpEvent.Pages(CurPageNum).IgnoreMoveRoute
        chkRepeatRoute.Checked = TmpEvent.Pages(CurPageNum).RepeatMoveRoute
        TempMoveRouteCount = TmpEvent.Pages(CurPageNum).MoveRouteCount

        'Will it let me do this?
        TempMoveRoute = TmpEvent.Pages(CurPageNum).MoveRoute
        For i = 1 To TempMoveRouteCount
            Select Case TempMoveRoute(i).Index
                Case 1
                    lstMoveRoute.Items.Add("Move Up")
                Case 2
                    lstMoveRoute.Items.Add("Move Down")
                Case 3
                    lstMoveRoute.Items.Add("Move Left")
                Case 4
                    lstMoveRoute.Items.Add("Move Right")
                Case 5
                    lstMoveRoute.Items.Add("Move Randomly")
                Case 6
                    lstMoveRoute.Items.Add("Move Towards Player")
                Case 7
                    lstMoveRoute.Items.Add("Move Away From Player")
                Case 8
                    lstMoveRoute.Items.Add("Step Forward")
                Case 9
                    lstMoveRoute.Items.Add("Step Back")
                Case 10
                    lstMoveRoute.Items.Add("Wait 100ms")
                Case 11
                    lstMoveRoute.Items.Add("Wait 500ms")
                Case 12
                    lstMoveRoute.Items.Add("Wait 1000ms")
                Case 13
                    lstMoveRoute.Items.Add("Turn Up")
                Case 14
                    lstMoveRoute.Items.Add("Turn Down")
                Case 15
                    lstMoveRoute.Items.Add("Turn Left")
                Case 16
                    lstMoveRoute.Items.Add("Turn Right")
                Case 17
                    lstMoveRoute.Items.Add("Turn 90 Degrees To the Right")
                Case 18
                    lstMoveRoute.Items.Add("Turn 90 Degrees To the Left")
                Case 19
                    lstMoveRoute.Items.Add("Turn Around 180 Degrees")
                Case 20
                    lstMoveRoute.Items.Add("Turn Randomly")
                Case 21
                    lstMoveRoute.Items.Add("Turn Towards Player")
                Case 22
                    lstMoveRoute.Items.Add("Turn Away from Player")
                Case 23
                    lstMoveRoute.Items.Add("Set Speed 8x Slower")
                Case 24
                    lstMoveRoute.Items.Add("Set Speed 4x Slower")
                Case 25
                    lstMoveRoute.Items.Add("Set Speed 2x Slower")
                Case 26
                    lstMoveRoute.Items.Add("Set Speed to Normal")
                Case 27
                    lstMoveRoute.Items.Add("Set Speed 2x Faster")
                Case 28
                    lstMoveRoute.Items.Add("Set Speed 4x Faster")
                Case 29
                    lstMoveRoute.Items.Add("Set Frequency Lowest")
                Case 30
                    lstMoveRoute.Items.Add("Set Frequency Lower")
                Case 31
                    lstMoveRoute.Items.Add("Set Frequency Normal")
                Case 32
                    lstMoveRoute.Items.Add("Set Frequency Higher")
                Case 33
                    lstMoveRoute.Items.Add("Set Frequency Highest")
                Case 34
                    lstMoveRoute.Items.Add("Turn On Walking Animation")
                Case 35
                    lstMoveRoute.Items.Add("Turn Off Walking Animation")
                Case 36
                    lstMoveRoute.Items.Add("Turn On Fixed Direction")
                Case 37
                    lstMoveRoute.Items.Add("Turn Off Fixed Direction")
                Case 38
                    lstMoveRoute.Items.Add("Turn On Walk Through")
                Case 39
                    lstMoveRoute.Items.Add("Turn Off Walk Through")
                Case 40
                    lstMoveRoute.Items.Add("Set Position Below Player")
                Case 41
                    lstMoveRoute.Items.Add("Set Position at Player Level")
                Case 42
                    lstMoveRoute.Items.Add("Set Position Above Player")
                Case 43
                    lstMoveRoute.Items.Add("Set Graphic")
            End Select
        Next

        fraMoveRoute.Visible = True

    End Sub

    Private Sub CmbMoveSpeed_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMoveSpeed.SelectedIndexChanged
        If cmbMoveSpeed.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).MoveSpeed = cmbMoveSpeed.SelectedIndex
    End Sub

    Private Sub CmbMoveFreq_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMoveFreq.SelectedIndexChanged
        If cmbMoveFreq.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).MoveFreq = cmbMoveFreq.SelectedIndex
    End Sub

#End Region

#Region "Positioning"

    Private Sub CmbPositioning_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPositioning.SelectedIndexChanged
        If cmbPositioning.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).Position = cmbPositioning.SelectedIndex
    End Sub

#End Region

#Region "Trigger"

    Private Sub CmbTrigger_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTrigger.SelectedIndexChanged
        If cmbTrigger.SelectedIndex = -1 Then Exit Sub
        TmpEvent.Pages(CurPageNum).Trigger = cmbTrigger.SelectedIndex
    End Sub

#End Region

#Region "Global"

    Private Sub ChkGlobal_CheckedChanged(sender As Object, e As EventArgs) Handles chkGlobal.CheckedChanged
        If TmpEvent.PageCount > 1 Then
            If MsgBox("If you set the event to global you will lose all pages except for your first one. Do you want to continue?", vbYesNo) = vbNo Then
                Exit Sub
            End If
        End If
        If chkGlobal.Checked = True Then
            TmpEvent.Globals = 1
        Else
            TmpEvent.Globals = 0
        End If

        TmpEvent.PageCount = 1
        CurPageNum = 1
        Me.tabPages.TabPages.Clear()

        For i = 1 To TmpEvent.PageCount
            Me.tabPages.TabPages.Add("0", Str(i), "0")
        Next
        EventEditorLoadPage(CurPageNum)
    End Sub

#End Region

#Region "Quest"

    Private Sub CmbEventQuest_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEventQuest.SelectedIndexChanged
        TmpEvent.Pages(CurPageNum).Questnum = cmbEventQuest.SelectedIndex
    End Sub

#End Region

#Region "Options"

    Private Sub ChkWalkAnim_CheckedChanged(sender As Object, e As EventArgs) Handles chkWalkAnim.CheckedChanged
        If chkWalkAnim.Checked = True Then
            TmpEvent.Pages(CurPageNum).WalkAnim = 1
        Else
            TmpEvent.Pages(CurPageNum).WalkAnim = 0
        End If

    End Sub

    Private Sub ChkDirFix_CheckedChanged(sender As Object, e As EventArgs) Handles chkDirFix.CheckedChanged
        If chkDirFix.Checked = True Then
            TmpEvent.Pages(CurPageNum).DirFix = 1
        Else
            TmpEvent.Pages(CurPageNum).DirFix = 0
        End If

    End Sub

    Private Sub ChkWalkThrough_CheckedChanged(sender As Object, e As EventArgs) Handles chkWalkThrough.CheckedChanged
        If chkWalkThrough.Checked = True Then
            TmpEvent.Pages(CurPageNum).WalkThrough = 1
        Else
            TmpEvent.Pages(CurPageNum).WalkThrough = 0
        End If

    End Sub

    Private Sub ChkShowName_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowName.CheckedChanged
        If chkShowName.Checked = True Then
            TmpEvent.Pages(CurPageNum).ShowName = 1
        Else
            TmpEvent.Pages(CurPageNum).ShowName = 0
        End If

    End Sub

#End Region

#Region "Commands"

    Private Sub LstCommands_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCommands.SelectedIndexChanged
        CurCommand = lstCommands.SelectedIndex + 1
    End Sub

    Private Sub BtnAddCommand_Click(sender As Object, e As EventArgs) Handles btnAddCommand.Click
        If lstCommands.SelectedIndex > -1 Then
            IsEdit = False
            'tabPages.SelectedTab = TabPage
            fraCommands.Visible = True
        End If
    End Sub

    Private Sub BtnEditCommand_Click(sender As Object, e As EventArgs) Handles btnEditCommand.Click
        If lstCommands.SelectedIndex > -1 Then
            EditEventCommand()
        End If
    End Sub

    Private Sub BtnDeleteComand_Click(sender As Object, e As EventArgs) Handles btnDeleteCommand.Click
        If lstCommands.SelectedIndex > -1 Then
            DeleteEventCommand()
        End If

    End Sub

    Private Sub BtnClearCommand_Click(sender As Object, e As EventArgs) Handles btnClearCommand.Click
        If MsgBox("Are you sure you want to clear all event commands?", vbYesNo, "Clear Event Commands?") = vbYes Then
            ClearEventCommands()
        End If
    End Sub

#End Region

#Region "Variables/Switches"

    '    'Renaming Variables/Switches
    Private Sub BtnLabeling_Click(sender As Object, e As EventArgs) Handles btnLabeling.Click
        pnlVariableSwitches.Visible = True
        pnlVariableSwitches.BringToFront()
        pnlVariableSwitches.Top = 0
        pnlVariableSwitches.Left = 0
        pnlVariableSwitches.Width = Width
        pnlVariableSwitches.Height = Height
        lstSwitches.Items.Clear()

        For i = 1 To MaxSwitches
            lstSwitches.Items.Add(CStr(i) & ". " & Trim$(Switches(i)))
        Next
        lstSwitches.SelectedIndex = 0
        lstVariables.Items.Clear()

        For i = 1 To MaxVariables
            lstVariables.Items.Add(CStr(i) & ". " & Trim$(Variables(i)))
        Next
        lstVariables.SelectedIndex = 0

    End Sub

    Private Sub BtnRename_Ok_Click(sender As Object, e As EventArgs) Handles btnRename_Ok.Click
        Select Case RenameType
            Case 1
                'Variable
                If Renameindex > 0 AndAlso Renameindex <= MaxVariables + 1 Then
                    Variables(Renameindex) = txtRename.Text
                    FraRenaming.Visible = False
                    fraLabeling.Visible = True
                    RenameType = 0
                    Renameindex = 0
                End If
            Case 2
                'Switch
                If Renameindex > 0 AndAlso Renameindex <= MaxSwitches + 1 Then
                    Switches(Renameindex) = txtRename.Text
                    FraRenaming.Visible = False
                    fraLabeling.Visible = True
                    RenameType = 0
                    Renameindex = 0
                End If
        End Select
        lstSwitches.Items.Clear()

        For i = 1 To MaxSwitches
            lstSwitches.Items.Add(CStr(i) & ". " & Trim$(Switches(i)))
        Next
        lstSwitches.SelectedIndex = 0
        lstVariables.Items.Clear()

        For i = 1 To MaxVariables
            lstVariables.Items.Add(CStr(i) & ". " & Trim$(Variables(i)))
        Next
        lstVariables.SelectedIndex = 0
    End Sub

    Private Sub BtnRename_Cancel_Click(sender As Object, e As EventArgs) Handles btnRename_Cancel.Click
        FraRenaming.Visible = False
        RenameType = 0
        Renameindex = 0
        lstSwitches.Items.Clear()

        For i = 1 To MaxSwitches
            lstSwitches.Items.Add(CStr(i) & ". " & Trim$(Switches(i)))
        Next
        lstSwitches.SelectedIndex = 0
        lstVariables.Items.Clear()

        For i = 1 To MaxVariables
            lstVariables.Items.Add(CStr(i) & ". " & Trim$(Variables(i)))
        Next
        lstVariables.SelectedIndex = 0
    End Sub

    Private Sub TxtRename_TextChanged(sender As Object, e As EventArgs) Handles txtRename.TextChanged
        TmpEvent.Name = Trim$(txtName.Text)
    End Sub

    Private Sub LstVariables_DoubleClick(sender As Object, e As EventArgs) Handles lstVariables.DoubleClick
        If lstVariables.SelectedIndex > -1 AndAlso lstVariables.SelectedIndex < MaxVariables Then
            FraRenaming.Visible = True
            fraLabeling.Visible = False
            lblEditing.Text = "Editing Variable #" & CStr(lstVariables.SelectedIndex + 1)
            txtRename.Text = Variables(lstVariables.SelectedIndex + 1)
            RenameType = 1
            Renameindex = lstVariables.SelectedIndex + 1
        End If
    End Sub

    Private Sub LstSwitches_DoubleClick(sender As Object, e As EventArgs) Handles lstSwitches.DoubleClick
        If lstSwitches.SelectedIndex > -1 AndAlso lstSwitches.SelectedIndex < MaxSwitches Then
            FraRenaming.Visible = True
            fraLabeling.Visible = False
            lblEditing.Text = "Editing Switch #" & CStr(lstSwitches.SelectedIndex + 1)
            txtRename.Text = Switches(lstSwitches.SelectedIndex + 1)
            RenameType = 2
            Renameindex = lstSwitches.SelectedIndex + 1
        End If
    End Sub

    Private Sub BtnRenameVariable_Click(sender As Object, e As EventArgs) Handles btnRenameVariable.Click
        If lstVariables.SelectedIndex > -1 AndAlso lstVariables.SelectedIndex < MaxVariables Then
            FraRenaming.Visible = True
            fraLabeling.Visible = False
            lblEditing.Text = "Editing Variable #" & CStr(lstVariables.SelectedIndex + 1)
            txtRename.Text = Variables(lstVariables.SelectedIndex + 1)
            RenameType = 1
            Renameindex = lstVariables.SelectedIndex + 1
        End If
    End Sub

    Private Sub BtnRenameSwitch_Click(sender As Object, e As EventArgs) Handles btnRenameSwitch.Click
        If lstSwitches.SelectedIndex > -1 AndAlso lstSwitches.SelectedIndex < MaxSwitches Then
            FraRenaming.Visible = True
            lblEditing.Text = "Editing Switch #" & CStr(lstSwitches.SelectedIndex + 1)
            txtRename.Text = Switches(lstSwitches.SelectedIndex + 1)
            RenameType = 2
            Renameindex = lstSwitches.SelectedIndex + 1
        End If
    End Sub

    Private Sub BtnLabel_Ok_Click(sender As Object, e As EventArgs) Handles btnLabel_Ok.Click
        pnlVariableSwitches.Visible = False
        SendSwitchesAndVariables()
    End Sub

    Private Sub BtnLabel_Cancel_Click(sender As Object, e As EventArgs) Handles btnLabel_Cancel.Click
        pnlVariableSwitches.Visible = False
        RequestSwitchesAndVariables()
    End Sub

#End Region

#Region "Move Route"

    'MoveRoute Commands
    Private Sub LstvwMoveRoute_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstvwMoveRoute.Click
        If lstvwMoveRoute.SelectedItems.Count = 0 Then Exit Sub

        Select Case lstvwMoveRoute.SelectedItems(0).Index + 1
            'Set Graphic
            Case 43
                fraGraphic.Width = 841
                fraGraphic.Height = 585
                fraGraphic.Visible = True
                fraGraphic.BringToFront()
                GraphicSelType = 1
            Case Else
                AddMoveRouteCommand(lstvwMoveRoute.SelectedItems(0).Index)
        End Select
    End Sub

    Private Sub LstMoveRoute_KeyDown(sender As Object, e As KeyEventArgs) Handles lstMoveRoute.KeyDown
        If e.KeyCode = Keys.Delete Then
            'remove move route command lol
            If lstMoveRoute.SelectedIndex > -1 Then
                RemoveMoveRouteCommand(lstMoveRoute.SelectedIndex)
            End If
        End If
    End Sub

    Sub AddMoveRouteCommand(Index As Integer)
        Dim i As Integer, X As Integer

        Index = Index + 1
        If lstMoveRoute.SelectedIndex > -1 Then
            i = lstMoveRoute.SelectedIndex + 1
            TempMoveRouteCount = TempMoveRouteCount + 1
            ReDim Preserve TempMoveRoute(TempMoveRouteCount)
            For X = TempMoveRouteCount - 1 To i Step -1
                TempMoveRoute(X + 1) = TempMoveRoute(X)
            Next
            TempMoveRoute(i).Index = Index
            'if set graphic then...
            If Index = 43 Then
                TempMoveRoute(i).Data1 = cmbGraphic.SelectedIndex
                TempMoveRoute(i).Data2 = nudGraphic.Value
                TempMoveRoute(i).Data3 = GraphicSelX
                TempMoveRoute(i).Data4 = GraphicSelX2
                TempMoveRoute(i).Data5 = GraphicSelY
                TempMoveRoute(i).Data6 = GraphicSelY2
            End If
            PopulateMoveRouteList()
        Else
            TempMoveRouteCount = TempMoveRouteCount + 1
            ReDim Preserve TempMoveRoute(TempMoveRouteCount)
            TempMoveRoute(TempMoveRouteCount).Index = Index
            PopulateMoveRouteList()
            'if set graphic then....
            If Index = 43 Then
                TempMoveRoute(TempMoveRouteCount).Data1 = cmbGraphic.SelectedIndex
                TempMoveRoute(TempMoveRouteCount).Data2 = nudGraphic.Value
                TempMoveRoute(TempMoveRouteCount).Data3 = GraphicSelX
                TempMoveRoute(TempMoveRouteCount).Data4 = GraphicSelX2
                TempMoveRoute(TempMoveRouteCount).Data5 = GraphicSelY
                TempMoveRoute(TempMoveRouteCount).Data6 = GraphicSelY2
            End If
        End If

    End Sub

    Sub RemoveMoveRouteCommand(Index As Integer)
        Dim i As Integer

        Index = Index + 1
        If Index > 0 AndAlso Index <= TempMoveRouteCount Then
            For i = Index + 1 To TempMoveRouteCount
                TempMoveRoute(i - 1) = TempMoveRoute(i)
            Next
            TempMoveRouteCount = TempMoveRouteCount - 1
            If TempMoveRouteCount = 0 Then
                ReDim TempMoveRoute(0)
            Else
                ReDim Preserve TempMoveRoute(TempMoveRouteCount)
            End If
            PopulateMoveRouteList()
        End If

    End Sub

    Sub PopulateMoveRouteList()
        Dim i As Integer

        lstMoveRoute.Items.Clear()

        For i = 1 To TempMoveRouteCount
            Select Case TempMoveRoute(i).Index
                Case 1
                    lstMoveRoute.Items.Add("Move Up")
                Case 2
                    lstMoveRoute.Items.Add("Move Down")
                Case 3
                    lstMoveRoute.Items.Add("Move Left")
                Case 4
                    lstMoveRoute.Items.Add("Move Right")
                Case 5
                    lstMoveRoute.Items.Add("Move Randomly")
                Case 6
                    lstMoveRoute.Items.Add("Move Towards Player")
                Case 7
                    lstMoveRoute.Items.Add("Move Away From Player")
                Case 8
                    lstMoveRoute.Items.Add("Step Forward")
                Case 9
                    lstMoveRoute.Items.Add("Step Back")
                Case 10
                    lstMoveRoute.Items.Add("Wait 100ms")
                Case 11
                    lstMoveRoute.Items.Add("Wait 500ms")
                Case 12
                    lstMoveRoute.Items.Add("Wait 1000ms")
                Case 13
                    lstMoveRoute.Items.Add("Turn Up")
                Case 14
                    lstMoveRoute.Items.Add("Turn Down")
                Case 15
                    lstMoveRoute.Items.Add("Turn Left")
                Case 16
                    lstMoveRoute.Items.Add("Turn Right")
                Case 17
                    lstMoveRoute.Items.Add("Turn 90 Degrees To the Right")
                Case 18
                    lstMoveRoute.Items.Add("Turn 90 Degrees To the Left")
                Case 19
                    lstMoveRoute.Items.Add("Turn Around 180 Degrees")
                Case 20
                    lstMoveRoute.Items.Add("Turn Randomly")
                Case 21
                    lstMoveRoute.Items.Add("Turn Towards Player")
                Case 22
                    lstMoveRoute.Items.Add("Turn Away from Player")
                Case 23
                    lstMoveRoute.Items.Add("Set Speed 8x Slower")
                Case 24
                    lstMoveRoute.Items.Add("Set Speed 4x Slower")
                Case 25
                    lstMoveRoute.Items.Add("Set Speed 2x Slower")
                Case 26
                    lstMoveRoute.Items.Add("Set Speed to Normal")
                Case 27
                    lstMoveRoute.Items.Add("Set Speed 2x Faster")
                Case 28
                    lstMoveRoute.Items.Add("Set Speed 4x Faster")
                Case 29
                    lstMoveRoute.Items.Add("Set Frequency Lowest")
                Case 30
                    lstMoveRoute.Items.Add("Set Frequency Lower")
                Case 31
                    lstMoveRoute.Items.Add("Set Frequency Normal")
                Case 32
                    lstMoveRoute.Items.Add("Set Frequency Higher")
                Case 33
                    lstMoveRoute.Items.Add("Set Frequency Highest")
                Case 34
                    lstMoveRoute.Items.Add("Turn On Walking Animation")
                Case 35
                    lstMoveRoute.Items.Add("Turn Off Walking Animation")
                Case 36
                    lstMoveRoute.Items.Add("Turn On Fixed Direction")
                Case 37
                    lstMoveRoute.Items.Add("Turn Off Fixed Direction")
                Case 38
                    lstMoveRoute.Items.Add("Turn On Walk Through")
                Case 39
                    lstMoveRoute.Items.Add("Turn Off Walk Through")
                Case 40
                    lstMoveRoute.Items.Add("Set Position Below Player")
                Case 41
                    lstMoveRoute.Items.Add("Set Position at Player Level")
                Case 42
                    lstMoveRoute.Items.Add("Set Position Above Player")
                Case 43
                    lstMoveRoute.Items.Add("Set Graphic")
            End Select
        Next

    End Sub

    Private Sub ChkIgnoreMove_CheckedChanged(sender As Object, e As EventArgs) Handles chkIgnoreMove.CheckedChanged
        If chkIgnoreMove.Checked = True Then
            TmpEvent.Pages(CurPageNum).IgnoreMoveRoute = 1
        Else
            TmpEvent.Pages(CurPageNum).IgnoreMoveRoute = 0
        End If
    End Sub

    Private Sub ChkRepeatRoute_CheckedChanged(sender As Object, e As EventArgs) Handles chkRepeatRoute.CheckedChanged
        If chkRepeatRoute.Checked = True Then
            TmpEvent.Pages(CurPageNum).RepeatMoveRoute = 1
        Else
            TmpEvent.Pages(CurPageNum).RepeatMoveRoute = 0
        End If
    End Sub

    Private Sub BtnMoveRouteOk_Click(sender As Object, e As EventArgs) Handles btnMoveRouteOk.Click
        If IsMoveRouteCommand = True Then
            If Not IsEdit Then
                AddCommand(EventType.EvSetMoveRoute)
            Else
                EditCommand()
            End If
            TempMoveRouteCount = 0
            ReDim TempMoveRoute(0)
            fraMoveRoute.Visible = False
        Else
            TmpEvent.Pages(CurPageNum).MoveRouteCount = TempMoveRouteCount
            TmpEvent.Pages(CurPageNum).MoveRoute = TempMoveRoute
            TempMoveRouteCount = 0
            ReDim TempMoveRoute(0)
            fraMoveRoute.Visible = False
        End If
    End Sub

    Private Sub BtnMoveRouteCancel_Click(sender As Object, e As EventArgs) Handles btnMoveRouteCancel.Click
        TempMoveRouteCount = 0
        ReDim TempMoveRoute(0)
        fraMoveRoute.Visible = False
    End Sub

#End Region

#Region "CommandFrames"

#Region "Show Text"

    Private Sub NudShowTextFace_ValueChanged(sender As Object, e As EventArgs) Handles nudShowTextFace.ValueChanged
        If nudShowTextFace.Value > 0 Then
            If File.Exists(Path.Graphics & "Faces\" & nudShowTextFace.Value & GfxExt) Then
                picShowTextFace.BackgroundImage = Image.FromFile(Path.Graphics & "Faces\" & nudShowTextFace.Value & GfxExt)
            End If
        Else
            picShowTextFace.BackgroundImage = Nothing
        End If
    End Sub

    Private Sub BtnShowTextOk_Click(sender As Object, e As EventArgs) Handles btnShowTextOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvShowText)
        Else
            EditCommand()
        End If

        ' hide
        fraDialogue.Visible = False
        fraShowText.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnShowTextCancel_Click(sender As Object, e As EventArgs) Handles btnShowTextCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraShowText.Visible = False
    End Sub

#End Region

#Region "Add Text"

    Private Sub BtnAddTextOk_Click(sender As Object, e As EventArgs) Handles btnAddTextOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvAddText)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraAddText.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnAddTextCancel_Click(sender As Object, e As EventArgs) Handles btnAddTextCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraAddText.Visible = False
    End Sub

#End Region

#Region "Show Choices"

    Private Sub NudShowChoicesFace_ValueChanged(sender As Object, e As EventArgs) Handles nudShowChoicesFace.ValueChanged
        If nudShowChoicesFace.Value > 0 Then
            If File.Exists(Path.Graphics & "Faces\" & nudShowChoicesFace.Value & GfxExt) Then
                picShowChoicesFace.BackgroundImage = Image.FromFile(Path.Graphics & "Faces\" & nudShowChoicesFace.Value & GfxExt)
            End If
        Else
            picShowChoicesFace.Text = "Face: None"
            picShowChoicesFace.BackgroundImage = Nothing
        End If
    End Sub

    Private Sub BtnShowChoicesOk_Click(sender As Object, e As EventArgs) Handles btnShowChoicesOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvShowChoices)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraShowChoices.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnShowChoicesCancel_Click(sender As Object, e As EventArgs) Handles btnShowChoicesCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraShowChoices.Visible = False
    End Sub

#End Region

#Region "Show Chatbubble"

    Private Sub CmbChatBubbleTargetType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChatBubbleTargetType.SelectedIndexChanged
        If cmbChatBubbleTargetType.SelectedIndex = 0 Then
            cmbChatBubbleTarget.Visible = False
        ElseIf cmbChatBubbleTargetType.SelectedIndex = 1 Then
            cmbChatBubbleTarget.Visible = True
            cmbChatBubbleTarget.Items.Clear()

            For i = 1 To MAX_MAP_NPCS
                If Map.Npc(i) <= 0 Then
                    cmbChatBubbleTarget.Items.Add(i & ". ")
                Else
                    cmbChatBubbleTarget.Items.Add(i & ". " & Trim$(Npc(Map.Npc(i)).Name))
                End If
            Next
            cmbChatBubbleTarget.SelectedIndex = 0
        ElseIf cmbChatBubbleTargetType.SelectedIndex = 2 Then
            cmbChatBubbleTarget.Visible = True
            cmbChatBubbleTarget.Items.Clear()

            For i = 1 To Map.EventCount
                cmbChatBubbleTarget.Items.Add(i & ". " & Trim$(Map.Events(i).Name))
            Next
            cmbChatBubbleTarget.SelectedIndex = 0
        End If

    End Sub

    Private Sub BtnShowChatBubbleOK_Click(sender As Object, e As EventArgs) Handles btnShowChatBubbleOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvShowChatBubble)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraShowChatBubble.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnShowChatBubbleCancel_Click(sender As Object, e As EventArgs) Handles btnShowChatBubbleCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraShowChatBubble.Visible = False
    End Sub

#End Region

#Region "Set Player Variable"

    Private Sub OptVariableAction0_CheckedChanged(sender As Object, e As EventArgs) Handles optVariableAction0.CheckedChanged
        If optVariableAction0.Checked = True Then
            nudVariableData0.Enabled = True
            nudVariableData0.Value = 0
            nudVariableData1.Enabled = False
            nudVariableData1.Value = 0
            nudVariableData2.Enabled = False
            nudVariableData2.Value = 0
            nudVariableData3.Enabled = False
            nudVariableData3.Value = 0
            nudVariableData4.Enabled = False
            nudVariableData4.Value = 0
        End If
    End Sub

    Private Sub OptVariableAction1_CheckedChanged(sender As Object, e As EventArgs) Handles optVariableAction1.CheckedChanged
        If optVariableAction1.Checked = True Then
            nudVariableData0.Enabled = False
            nudVariableData0.Value = 0
            nudVariableData1.Enabled = True
            nudVariableData1.Value = 0
            nudVariableData2.Enabled = False
            nudVariableData2.Value = 0
            nudVariableData3.Enabled = False
            nudVariableData3.Value = 0
            nudVariableData4.Enabled = False
            nudVariableData4.Value = 0
        End If
    End Sub

    Private Sub OptVariableAction2_CheckedChanged(sender As Object, e As EventArgs) Handles optVariableAction2.CheckedChanged
        If optVariableAction2.Checked = True Then
            nudVariableData0.Enabled = False
            nudVariableData0.Value = 0
            nudVariableData1.Enabled = False
            nudVariableData1.Value = 0
            nudVariableData2.Enabled = True
            nudVariableData2.Value = 0
            nudVariableData3.Enabled = False
            nudVariableData3.Value = 0
            nudVariableData4.Enabled = False
            nudVariableData4.Value = 0
        End If
    End Sub

    Private Sub OptVariableAction3_CheckedChanged(sender As Object, e As EventArgs) Handles optVariableAction3.CheckedChanged
        If optVariableAction2.Checked = True Then
            nudVariableData0.Enabled = False
            nudVariableData0.Value = 0
            nudVariableData1.Enabled = False
            nudVariableData1.Value = 0
            nudVariableData2.Enabled = False
            nudVariableData2.Value = 0
            nudVariableData3.Enabled = True
            nudVariableData3.Value = 0
            nudVariableData4.Enabled = True
            nudVariableData4.Value = 0
        End If
    End Sub

    Private Sub BtnPlayerVarOk_Click(sender As Object, e As EventArgs) Handles btnPlayerVarOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvPlayerVar)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraPlayerVariable.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnPlayerVarCancel_Click(sender As Object, e As EventArgs) Handles btnPlayerVarCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraPlayerVariable.Visible = False
    End Sub

#End Region

#Region "Set Player Switch"

    Private Sub BtnSetPlayerSwitchOk_Click(sender As Object, e As EventArgs) Handles btnSetPlayerSwitchOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvPlayerSwitch)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraPlayerSwitch.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnSetPlayerswitchCancel_Click(sender As Object, e As EventArgs) Handles btnSetPlayerswitchCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraPlayerSwitch.Visible = False
    End Sub

#End Region

#Region "Set Self Switch"

    Private Sub BtnSelfswitchOk_Click(sender As Object, e As EventArgs) Handles btnSelfswitchOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvSelfSwitch)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraSetSelfSwitch.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnSelfswitchCancel_Click(sender As Object, e As EventArgs) Handles btnSelfswitchCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraSetSelfSwitch.Visible = False
    End Sub

#End Region

#Region "Conditional Branch"

    Private Sub OptCondition_Index0_CheckedChanged(sender As Object, e As EventArgs) Handles optCondition0.CheckedChanged
        If Not optCondition0.Checked Then Exit Sub

        ClearConditionFrame()

        cmbCondition_PlayerVarIndex.Enabled = True
        cmbCondition_PlayerVarCompare.Enabled = True
        nudCondition_PlayerVarCondition.Enabled = True
    End Sub

    Private Sub OptCondition1_CheckedChanged(sender As Object, e As EventArgs) Handles optCondition1.CheckedChanged
        If Not optCondition1.Checked Then Exit Sub

        ClearConditionFrame()

        cmbCondition_PlayerSwitch.Enabled = True
        cmbCondtion_PlayerSwitchCondition.Enabled = True
    End Sub

    Private Sub OptCondition2_CheckedChanged(sender As Object, e As EventArgs) Handles optCondition2.CheckedChanged
        If Not optCondition2.Checked Then Exit Sub

        ClearConditionFrame()

        cmbCondition_HasItem.Enabled = True
        nudCondition_HasItem.Enabled = True
    End Sub

    Private Sub OptCondition3_CheckedChanged(sender As Object, e As EventArgs) Handles optCondition3.CheckedChanged
        If Not optCondition3.Checked Then Exit Sub

        ClearConditionFrame()

        cmbCondition_ClassIs.Enabled = True
    End Sub

    Private Sub OptCondition4_CheckedChanged(sender As Object, e As EventArgs) Handles optCondition4.CheckedChanged
        If Not optCondition4.Checked Then Exit Sub

        cmbCondition_LearntSkill.Enabled = True
    End Sub

    Private Sub OptCondition5_CheckedChanged(sender As Object, e As EventArgs) Handles optCondition5.CheckedChanged
        If Not optCondition5.Checked Then Exit Sub

        ClearConditionFrame()

        cmbCondition_LevelCompare.Enabled = True
        nudCondition_LevelAmount.Enabled = True
    End Sub

    Private Sub OptCondition6_CheckedChanged(sender As Object, e As EventArgs) Handles optCondition6.CheckedChanged
        If Not optCondition6.Checked Then Exit Sub

        ClearConditionFrame()

        cmbCondition_SelfSwitch.Enabled = True
        cmbCondition_SelfSwitchCondition.Enabled = True
    End Sub

    Private Sub OptCondition7_CheckedChanged(sender As Object, e As EventArgs) Handles optCondition7.CheckedChanged
        If Not optCondition7.Checked Then Exit Sub

        ClearConditionFrame()

        fraConditions_Quest.Visible = True
        nudCondition_Quest.Enabled = True
    End Sub

    Private Sub OptCondition8_CheckedChanged(sender As Object, e As EventArgs) Handles optCondition8.CheckedChanged
        If Not optCondition8.Checked Then Exit Sub

        ClearConditionFrame()

        cmbCondition_Gender.Enabled = True
    End Sub

    Private Sub OptCondition9_CheckedChanged(sender As Object, e As EventArgs) Handles optCondition9.CheckedChanged
        If Not optCondition9.Checked Then Exit Sub

        ClearConditionFrame()

        cmbCondition_Time.Enabled = True
    End Sub

    Private Sub BtnConditionalBranchOk_Click(sender As Object, e As EventArgs) Handles btnConditionalBranchOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvCondition)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraCommands.Visible = False
        fraConditionalBranch.Visible = False
    End Sub

    Private Sub BtnConditionalBranchCancel_Click(sender As Object, e As EventArgs) Handles btnConditionalBranchCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraConditionalBranch.Visible = False
    End Sub

#End Region

#Region "Create Label"

    Private Sub BtnCreatelabelOk_Click(sender As Object, e As EventArgs) Handles btnCreatelabelOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvLabel)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraCreateLabel.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnCreateLabelCancel_Click(sender As Object, e As EventArgs) Handles btnCreatelabelCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraCreateLabel.Visible = False
    End Sub

#End Region

#Region "GoTo Label"

    Private Sub BtnGoToLabelOk_Click(sender As Object, e As EventArgs) Handles btnGoToLabelOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvGotoLabel)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraGoToLabel.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnGoToLabelCancel_Click(sender As Object, e As EventArgs) Handles btnGoToLabelCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraGoToLabel.Visible = False
    End Sub

#End Region

#Region "Change Items"

    Private Sub BtnChangeItemsOk_Click(sender As Object, e As EventArgs) Handles btnChangeItemsOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvChangeItems)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraCommands.Visible = False
        fraChangeItems.Visible = False
    End Sub

    Private Sub BtnChangeItemsCancel_Click(sender As Object, e As EventArgs) Handles btnChangeItemsCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraChangeItems.Visible = False
    End Sub

    Private Sub CmbChangeItemIndex_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChangeItemIndex.SelectedIndexChanged
        TmpEvent.Pages(CurPageNum).Questnum = cmbEventQuest.SelectedIndex
    End Sub

#End Region

#Region "Change Level"

    Private Sub BtnChangeLevelOK_Click(sender As Object, e As EventArgs) Handles btnChangeLevelOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvChangeLevel)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraChangeLevel.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnChangeLevelCancel_Click(sender As Object, e As EventArgs) Handles btnChangeLevelCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraChangeLevel.Visible = False
    End Sub

#End Region

#Region "Change Skills"

    Private Sub BtnChangeSkillsOK_Click(sender As Object, e As EventArgs) Handles btnChangeSkillsOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvChangeSkills)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraChangeSkills.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnChangeSkillsCancel_Click(sender As Object, e As EventArgs) Handles btnChangeSkillsCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraChangeSkills.Visible = False
    End Sub

#End Region

#Region "Change Class"

    Private Sub BtnChangeClassOK_Click(sender As Object, e As EventArgs) Handles btnChangeClassOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvChangeClass)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraChangeClass.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnChangeClassCancel_Click(sender As Object, e As EventArgs) Handles btnChangeClassCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraChangeClass.Visible = False
    End Sub

#End Region

#Region "Change Sprite"

    Private Sub BtnChangeSpriteOK_Click(sender As Object, e As EventArgs) Handles btnChangeSpriteOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvChangeSprite)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraChangeSprite.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnChangeSpriteCancel_Click(sender As Object, e As EventArgs) Handles btnChangeSpriteCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraChangeSprite.Visible = False
    End Sub

#End Region

#Region "Change Gender"

    Private Sub BtnChangeGenderOK_Click(sender As Object, e As EventArgs) Handles btnChangeGenderOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvChangeSex)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraChangeGender.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnChangeGenderCancel_Click(sender As Object, e As EventArgs) Handles btnChangeGenderCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraChangeGender.Visible = False
    End Sub

#End Region

#Region "Change PK"

    Private Sub BtnChangePkOK_Click(sender As Object, e As EventArgs) Handles btnChangePkOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvChangePk)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraChangePK.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnChangePkCancel_Click(sender As Object, e As EventArgs) Handles btnChangePkCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraChangePK.Visible = False
    End Sub

#End Region

#Region "Give Exp"

    Private Sub BtnGiveExpOK_Click(sender As Object, e As EventArgs) Handles btnGiveExpOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvGiveExp)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraGiveExp.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnGiveExpCancel_Click(sender As Object, e As EventArgs) Handles btnGiveExpCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraGiveExp.Visible = False
    End Sub

#End Region

#Region "Player Warp"

    Private Sub BtnPlayerWarpOK_Click(sender As Object, e As EventArgs) Handles btnPlayerWarpOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvWarpPlayer)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraPlayerWarp.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnPlayerWarpCancel_Click(sender As Object, e As EventArgs) Handles btnPlayerWarpCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraPlayerWarp.Visible = False
    End Sub

#End Region

#Region "Route Completion"

    Private Sub BtnMoveWaitOK_Click(sender As Object, e As EventArgs) Handles btnMoveWaitOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvWaitMovement)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraMoveRouteWait.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnMoveWaitCancel_Click(sender As Object, e As EventArgs) Handles btnMoveWaitCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraMoveRouteWait.Visible = False
    End Sub

#End Region

#Region "Spawn Npc"

    Private Sub BtnSpawnNpcOK_Click(sender As Object, e As EventArgs) Handles btnSpawnNpcOk.Click
        If IsEdit = False Then
            AddCommand(EventType.EvSpawnNpc)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraSpawnNpc.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnSpawnNpcCancel_Click(sender As Object, e As EventArgs) Handles btnSpawnNpcCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraSpawnNpc.Visible = False
    End Sub

#End Region

#Region "Play Animation"

    Private Sub OptPlayAnimPlayer_CheckedChanged(sender As Object, e As EventArgs)
        lblPlayAnimX.Visible = False
        lblPlayAnimY.Visible = False
        nudPlayAnimTileX.Visible = False
        nudPlayAnimTileY.Visible = False
        cmbPlayAnimEvent.Visible = False
    End Sub

    Private Sub OptPlayAnimEvent_CheckedChanged(sender As Object, e As EventArgs)
        lblPlayAnimX.Visible = False
        lblPlayAnimY.Visible = False
        nudPlayAnimTileX.Visible = False
        nudPlayAnimTileY.Visible = False
        cmbPlayAnimEvent.Visible = True
    End Sub

    Private Sub OptPlayAnimTile_CheckedChanged(sender As Object, e As EventArgs)
        lblPlayAnimX.Visible = True
        lblPlayAnimY.Visible = True
        nudPlayAnimTileX.Visible = True
        nudPlayAnimTileY.Visible = True
        cmbPlayAnimEvent.Visible = False
    End Sub

    Private Sub BtnPlayAnimationOK_Click(sender As Object, e As EventArgs) Handles btnPlayAnimationOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvPlayAnimation)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraPlayAnimation.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnPlayAnimationCancel_Click(sender As Object, e As EventArgs) Handles btnPlayAnimationCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraPlayAnimation.Visible = False
    End Sub

#End Region

#Region "Begin Quest"

    Private Sub BtnBeginQuestOK_Click(sender As Object, e As EventArgs) Handles btnBeginQuestOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvBeginQuest)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraBeginQuest.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnBeginQuestCancel_Click(sender As Object, e As EventArgs) Handles btnBeginQuestCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraBeginQuest.Visible = False
    End Sub

#End Region

#Region "Complete QuestTask"

    Private Sub BtnCompleteQuestTaskOK_Click(sender As Object, e As EventArgs) Handles btnCompleteQuestTaskOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvQuestTask)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraCompleteTask.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnCompleteQuestTaskCancel_Click(sender As Object, e As EventArgs) Handles btnCompleteQuestTaskCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraCompleteTask.Visible = False
    End Sub

#End Region

#Region "End Quest"

    Private Sub BtnEndQuestOK_Click(sender As Object, e As EventArgs) Handles btnEndQuestOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvEndQuest)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraEndQuest.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnEndQuestCancel_Click(sender As Object, e As EventArgs) Handles btnEndQuestCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraEndQuest.Visible = False
    End Sub

#End Region

#Region "Set Fog"

    Private Sub BtnSetFogOK_Click(sender As Object, e As EventArgs) Handles btnSetFogOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvSetFog)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraSetFog.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnSetFogCancel_Click(sender As Object, e As EventArgs) Handles btnSetFogCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraSetFog.Visible = False
    End Sub

#End Region

#Region "Set Weather"

    Private Sub BtnSetWeatherOK_Click(sender As Object, e As EventArgs) Handles btnSetWeatherOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvSetWeather)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraSetWeather.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnSetWeatherCancel_Click(sender As Object, e As EventArgs) Handles btnSetWeatherCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraSetWeather.Visible = False
    End Sub

#End Region

#Region "Set Map Tint"

    Private Sub BtnMapTintOK_Click(sender As Object, e As EventArgs) Handles btnMapTintOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvSetTint)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraMapTint.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnMapTintCancel_Click(sender As Object, e As EventArgs) Handles btnMapTintCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraMapTint.Visible = False
    End Sub

#End Region

#Region "Play BGM"

    Private Sub BtnPlayBgmOK_Click(sender As Object, e As EventArgs) Handles btnPlayBgmOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvPlayBgm)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraPlayBGM.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnPlayBgmCancel_Click(sender As Object, e As EventArgs) Handles btnPlayBgmCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraPlayBGM.Visible = False
    End Sub

#End Region

#Region "Play Sound"

    Private Sub BtnPlaySoundOK_Click(sender As Object, e As EventArgs) Handles btnPlaySoundOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvPlaySound)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraPlaySound.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnPlaySoundCancel_Click(sender As Object, e As EventArgs) Handles btnPlaySoundCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraPlaySound.Visible = False
    End Sub

#End Region

#Region "Wait"

    Private Sub BtnSetWaitOK_Click(sender As Object, e As EventArgs) Handles btnSetWaitOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvWait)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraSetWait.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnSetWaitCancel_Click(sender As Object, e As EventArgs) Handles btnSetWaitCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraSetWait.Visible = False
    End Sub

#End Region

#Region "Set Access"

    Private Sub BtnSetAccessOK_Click(sender As Object, e As EventArgs) Handles btnSetAccessOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvSetAccess)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraSetAccess.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnSetAccessCancel_Click(sender As Object, e As EventArgs) Handles btnSetAccessCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraSetAccess.Visible = False
    End Sub

#End Region

#Region "Custom Script"

    Private Sub BtnCustomScriptOK_Click(sender As Object, e As EventArgs) Handles btnCustomScriptOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvCustomScript)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraCustomScript.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnCustomScriptCancel_Click(sender As Object, e As EventArgs) Handles btnCustomScriptCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraCustomScript.Visible = False
    End Sub

#End Region

#Region "Show Pic"

    Private Sub BtnShowPicOK_Click(sender As Object, e As EventArgs) Handles btnShowPicOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvShowPicture)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraShowPic.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnShowPicCancel_Click(sender As Object, e As EventArgs) Handles btnShowPicCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraShowPic.Visible = False
    End Sub

#End Region

#Region "Hide Pic"

    Private Sub BtnHidePicOK_Click(sender As Object, e As EventArgs) Handles btnHidePicOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvHidePicture)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraHidePic.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnHidePicCancel_Click(sender As Object, e As EventArgs) Handles btnHidePicCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraHidePic.Visible = False
    End Sub

#End Region

#Region "Open Shop"

    Private Sub BtnOpenShopOK_Click(sender As Object, e As EventArgs) Handles btnOpenShopOk.Click
        If Not IsEdit Then
            AddCommand(EventType.EvOpenShop)
        Else
            EditCommand()
        End If
        ' hide
        fraDialogue.Visible = False
        fraOpenShop.Visible = False
        fraCommands.Visible = False
    End Sub

    Private Sub BtnOpenShopCancel_Click(sender As Object, e As EventArgs) Handles btnOpenShopCancel.Click
        If Not IsEdit Then fraCommands.Visible = True Else fraCommands.Visible = False
        fraDialogue.Visible = False
        fraOpenShop.Visible = False
    End Sub

    Private Sub PicGraphicSel_Click(sender As Object, e As EventArgs) Handles picGraphicSel.Click

    End Sub

#End Region

#End Region

End Class