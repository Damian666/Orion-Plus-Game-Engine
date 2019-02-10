Friend Class frmEditor_NPC

#Region "Form Code"

    Private Sub FrmEditor_NPC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nudSprite.Maximum = NumCharacters

        cmbItem.Items.Clear()
        cmbItem.Items.Add("None")
        For i = 1 To MAX_ITEMS
            cmbItem.Items.Add(i & ": " & Item(i).Name)
        Next
    End Sub

    Private Sub LstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        NpcEditorInit()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        NpcEditorOk()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        If Editorindex <= 0 Then Exit Sub

        ClearNpc(Editorindex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Npc(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex

        NpcEditorInit()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        NpcEditorCancel()
    End Sub

#End Region

#Region "Properties"

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer

        If Editorindex = 0 Then Exit Sub

        tmpindex = lstIndex.SelectedIndex
        Npc(Editorindex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Npc(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub TxtAttackSay_TextChanged(sender As Object, e As EventArgs) Handles txtAttackSay.TextChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).AttackSay = txtAttackSay.Text
    End Sub

    Private Sub NudSprite_ValueChanged(sender As Object, e As EventArgs) Handles nudSprite.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Sprite = nudSprite.Value

        EditorNpc_DrawSprite()
    End Sub

    Private Sub NudRange_ValueChanged(sender As Object, e As EventArgs) Handles nudRange.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Range = nudRange.Value
    End Sub

    Private Sub CmbBehavior_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBehaviour.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Behaviour = cmbBehaviour.SelectedIndex
    End Sub

    Private Sub CmbFaction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFaction.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Faction = cmbFaction.SelectedIndex
    End Sub

    Private Sub CmbAnimation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAnimation.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Animation = cmbAnimation.SelectedIndex
    End Sub

    Private Sub NudSpawnSecs_ValueChanged(sender As Object, e As EventArgs) Handles nudSpawnSecs.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).SpawnSecs = nudSpawnSecs.Value
    End Sub

    Private Sub NudHp_ValueChanged(sender As Object, e As EventArgs) Handles nudHp.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Hp = nudHp.Value
    End Sub

    Private Sub NudExp_ValueChanged(sender As Object, e As EventArgs) Handles nudExp.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Exp = nudExp.Value
    End Sub

    Private Sub CmbQuest_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbQuest.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).QuestNum = cmbQuest.SelectedIndex
    End Sub

    Private Sub NudLevel_ValueChanged(sender As Object, e As EventArgs) Handles nudLevel.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Level = nudLevel.Value
    End Sub

    Private Sub NudDamage_ValueChanged(sender As Object, e As EventArgs) Handles nudDamage.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Damage = nudDamage.Value
    End Sub

    Private Sub CmbSpawnPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSpawnPeriod.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).SpawnTime = cmbSpawnPeriod.SelectedIndex
    End Sub

#End Region

#Region "Stats"

    Private Sub NudStrength_ValueChanged(sender As Object, e As EventArgs) Handles nudStrength.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Stat(StatType.Strength) = nudStrength.Value
    End Sub

    Private Sub NudEndurance_ValueChanged(sender As Object, e As EventArgs) Handles nudEndurance.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Stat(StatType.Endurance) = nudEndurance.Value
    End Sub

    Private Sub NudVitality_ValueChanged(sender As Object, e As EventArgs) Handles nudVitality.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Stat(StatType.Vitality) = nudVitality.Value
    End Sub

    Private Sub NudLuck_ValueChanged(sender As Object, e As EventArgs) Handles nudLuck.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Stat(StatType.Luck) = nudLuck.Value
    End Sub

    Private Sub NudIntelligence_ValueChanged(sender As Object, e As EventArgs) Handles nudIntelligence.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Stat(StatType.Intelligence) = nudIntelligence.Value
    End Sub

    Private Sub NudSpirit_ValueChanged(sender As Object, e As EventArgs) Handles nudSpirit.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Stat(StatType.Spirit) = nudSpirit.Value
    End Sub

#End Region

#Region "Drop Items"

    Private Sub CmbDropSlot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDropSlot.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        cmbItem.SelectedIndex = Npc(Editorindex).DropItem(cmbDropSlot.SelectedIndex + 1)

        nudAmount.Value = Npc(Editorindex).DropItemValue(cmbDropSlot.SelectedIndex + 1)

        nudChance.Value = Npc(Editorindex).DropChance(cmbDropSlot.SelectedIndex + 1)
    End Sub

    Private Sub CmbItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItem.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).DropItem(cmbDropSlot.SelectedIndex + 1) = cmbItem.SelectedIndex
    End Sub

    Private Sub ScrlValue_Scroll(sender As Object, e As EventArgs) Handles nudAmount.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).DropItemValue(cmbDropSlot.SelectedIndex + 1) = nudAmount.Value
    End Sub

    Private Sub NudChance_ValueChanged(sender As Object, e As EventArgs) Handles nudChance.ValueChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).DropChance(cmbDropSlot.SelectedIndex + 1) = nudChance.Value
    End Sub

#End Region

#Region "Skills"

    Private Sub CmbSkill1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkill1.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Skill(1) = cmbSkill1.SelectedIndex
    End Sub

    Private Sub CmbSkill2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkill2.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Skill(2) = cmbSkill2.SelectedIndex
    End Sub

    Private Sub CmbSkill3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkill3.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Skill(3) = cmbSkill3.SelectedIndex
    End Sub

    Private Sub CmbSkill4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkill4.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Skill(4) = cmbSkill4.SelectedIndex
    End Sub

    Private Sub CmbSkill5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkill5.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Skill(5) = cmbSkill5.SelectedIndex
    End Sub

    Private Sub CmbSkill6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkill6.SelectedIndexChanged
        If Editorindex <= 0 Then Exit Sub

        Npc(Editorindex).Skill(6) = cmbSkill6.SelectedIndex
    End Sub

#End Region

End Class