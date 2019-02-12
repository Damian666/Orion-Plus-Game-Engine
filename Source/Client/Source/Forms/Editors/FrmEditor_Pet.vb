Imports System.IO

Friend Class frmEditor_Pet

#Region "Basics"

    Private Sub FrmEditor_Pet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EditorPet_DrawPet()

        nudSprite.Maximum = NumCharacters
        nudRange.Maximum = 50
        nudLevel.Maximum = MAX_LEVELS
        nudMaxLevel.Maximum = MAX_LEVELS

    End Sub

    Private Sub LstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        If Editorindex = 0 OrElse Editorindex > MAX_PETS Then Exit Sub
        PetEditorInit()
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub
        tmpindex = lstIndex.SelectedIndex
        Pet(Editorindex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Pet(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub NudSprite_ValueChanged(sender As Object, e As EventArgs) Handles nudSprite.Click
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Sprite = nudSprite.Value

        EditorPet_DrawPet()
    End Sub

    Friend Sub EditorPet_DrawPet()
        Dim petnum As Integer

        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        petnum = nudSprite.Value

        If petnum < 1 OrElse petnum > NumCharacters Then
            picSprite.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Path.Graphics & "Characters\" & petnum & GfxExt) Then
            picSprite.BackgroundImage = Image.FromFile(Path.Graphics & "Characters\" & petnum & GfxExt)
        End If

    End Sub

    Private Sub NudRange_ValueChanged(sender As Object, e As EventArgs) Handles nudRange.Click
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Range = nudRange.Value
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        PetEditorOk()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        PetEditorCancel()
    End Sub

#End Region

#Region "Stats"

    Private Sub OptCustomStats_CheckedChanged(sender As Object, e As EventArgs) Handles optCustomStats.CheckedChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        If optCustomStats.Checked = True Then
            pnlCustomStats.Visible = True
            Pet(Editorindex).StatType = 1
        Else
            pnlCustomStats.Visible = False
            Pet(Editorindex).StatType = 0
        End If
    End Sub

    Private Sub OptAdoptStats_CheckedChanged(sender As Object, e As EventArgs) Handles optAdoptStats.CheckedChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        If optAdoptStats.Checked = True Then
            pnlCustomStats.Visible = False
            Pet(Editorindex).StatType = 0
        Else
            pnlCustomStats.Visible = True
            Pet(Editorindex).StatType = 1
        End If
    End Sub

    Private Sub NudStrength_ValueChanged(sender As Object, e As EventArgs) Handles nudStrength.ValueChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Stat(StatType.Strength) = nudStrength.Value
    End Sub

    Private Sub NudEndurance_ValueChanged(sender As Object, e As EventArgs) Handles nudEndurance.ValueChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Stat(StatType.Endurance) = nudEndurance.Value
    End Sub

    Private Sub NudVitality_ValueChanged(sender As Object, e As EventArgs) Handles nudVitality.ValueChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Stat(StatType.Vitality) = nudVitality.Value
    End Sub

    Private Sub NudLuck_ValueChanged(sender As Object, e As EventArgs) Handles nudLuck.ValueChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Stat(StatType.Luck) = nudLuck.Value
    End Sub

    Private Sub NudIntelligence_ValueChanged(sender As Object, e As EventArgs) Handles nudIntelligence.ValueChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Stat(StatType.Intelligence) = nudIntelligence.Value
    End Sub

    Private Sub NudSpirit_ValueChanged(sender As Object, e As EventArgs) Handles nudSpirit.ValueChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Stat(StatType.Spirit) = nudSpirit.Value
    End Sub

    Private Sub NudLevel_ValueChanged(sender As Object, e As EventArgs) Handles nudLevel.ValueChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Level = nudLevel.Value
    End Sub

#End Region

#Region "Leveling"

    Private Sub NudPetExp_ValueChanged(sender As Object, e As EventArgs) Handles nudPetExp.Click
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).ExpGain = nudPetExp.Value
    End Sub

    Private Sub NudPetPnts_ValueChanged(sender As Object, e As EventArgs) Handles nudPetPnts.Click
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).LevelPnts = nudPetPnts.Value
    End Sub

    Private Sub NudMaxLevel_ValueChanged(sender As Object, e As EventArgs) Handles nudMaxLevel.Click
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).MaxLevel = nudMaxLevel.Value
    End Sub

    Private Sub OptLevel_CheckedChanged(sender As Object, e As EventArgs) Handles optLevel.Click
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        If optLevel.Checked = True Then
            pnlPetlevel.Visible = True
            Pet(Editorindex).LevelingType = 1
        End If
    End Sub

    Private Sub OptDoNotLevel_CheckedChanged(sender As Object, e As EventArgs) Handles optDoNotLevel.Click
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        If optDoNotLevel.Checked = True Then
            pnlPetlevel.Visible = False
            Pet(Editorindex).LevelingType = 0
        End If
    End Sub

#End Region

#Region "Skills"

    Private Sub CmbSkill1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkill1.SelectedIndexChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Skill(1) = cmbSkill1.SelectedIndex
    End Sub

    Private Sub CmbSkill2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkill2.SelectedIndexChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Skill(2) = cmbSkill2.SelectedIndex
    End Sub

    Private Sub CmbSkill3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkill3.SelectedIndexChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Skill(3) = cmbSkill3.SelectedIndex
    End Sub

    Private Sub CmbSkill4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkill4.SelectedIndexChanged
        If Editorindex <= 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).Skill(4) = cmbSkill4.SelectedIndex
    End Sub

#End Region

#Region "Evolving"

    Private Sub ChkEvolve_CheckedChanged(sender As Object, e As EventArgs) Handles chkEvolve.CheckedChanged
        If Editorindex = 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        If chkEvolve.Checked = True Then
            Pet(Editorindex).Evolvable = 1
        Else
            Pet(Editorindex).Evolvable = 0
        End If

    End Sub

    Private Sub NudEvolveLvl_ValueChanged(sender As Object, e As EventArgs) Handles nudEvolveLvl.ValueChanged
        If Editorindex = 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).EvolveLevel = nudEvolveLvl.Value
    End Sub

    Private Sub CmbEvolve_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEvolve.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_PETS Then Exit Sub

        Pet(Editorindex).EvolveNum = cmbEvolve.SelectedIndex
    End Sub

#End Region

End Class