Imports System
Imports System.Drawing
Imports System.IO

Friend Class frmClasses

#Region "Frm Controls"

    Private Sub FrmEditor_Classes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nudMaleSprite.Maximum = NumCharacters
        nudFemaleSprite.Maximum = NumCharacters

        DrawPreview()
    End Sub

    Private Sub LstIndex_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstIndex.SelectedIndexChanged
        If lstIndex.SelectedIndex < 0 Then Exit Sub

        Editorindex = lstIndex.SelectedIndex + 1

        LoadClassInfo = True
    End Sub

    Private Sub BtnAddClass_Click(sender As Object, e As EventArgs) Handles btnAddClass.Click
        MaxClasses = MaxClasses + 1

        ReDim Preserve Classes(MaxClasses)

        Classes(MaxClasses).Name = "New Class"

        ReDim Classes(MaxClasses).Stat(StatType.Count - 1)

        ReDim Classes(MaxClasses).Vital(VitalType.Count - 1)

        ReDim Classes(MaxClasses).MaleSprite(1)
        ReDim Classes(MaxClasses).FemaleSprite(1)

        For i = 1 To StatType.Count - 1
            Classes(MaxClasses).Stat(i) = 1
        Next

        ReDim Classes(MaxClasses).StartItem(5)
        ReDim Classes(MaxClasses).StartValue(5)

        Classes(MaxClasses).StartMap = 1
        Classes(MaxClasses).StartX = 1
        Classes(MaxClasses).StartY = 1

        ClassEditorInit()
    End Sub

    Private Sub BtnRemoveClass_Click(sender As Object, e As EventArgs) Handles btnRemoveClass.Click
        Dim i As Integer

        'If its The Last class, its simple, just remove and redim
        If Editorindex = MaxClasses Then
            MaxClasses = MaxClasses - 1
            ReDim Preserve Classes(MaxClasses)
        Else
            'but if its somewhere in the middle, or beginning, it gets harder xD
            For i = 1 To MaxClasses
                'we shift everything thats beneath the selected class, up 1 slot
                Classes(Editorindex) = Classes(Editorindex + 1)
            Next

            'and then we remove it, and redim
            MaxClasses = MaxClasses - 1
            ReDim Preserve Classes(MaxClasses)
        End If

        ClassEditorInit()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ClassesEditorOk()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClassesEditorCancel()
    End Sub

    Private Sub TxtDescription_TextChanged(sender As Object, e As EventArgs) Handles txtDescription.TextChanged
        Classes(Editorindex).Desc = txtDescription.Text
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer
        If Editorindex = 0 OrElse Editorindex > MaxClasses Then Exit Sub

        tmpindex = lstIndex.SelectedIndex
        Classes(Editorindex).Name = txtName.Text.Trim
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Classes(Editorindex).Name.Trim)
        lstIndex.SelectedIndex = tmpindex
    End Sub
#End Region

#Region "Sprites"
    Private Sub BtnAddMaleSprite_Click(sender As Object, e As EventArgs) Handles btnAddMaleSprite.Click
        Dim tmpamount As Byte
        If Editorindex = 0 OrElse Editorindex > MaxClasses Then Exit Sub

        tmpamount = Classes(Editorindex).MaleSprite.GetUpperBound(0)

        ReDim Preserve Classes(Editorindex).MaleSprite(tmpamount + 1)

        Classes(Editorindex).MaleSprite(tmpamount + 1) = 1

        LoadClassInfo = True
    End Sub

    Private Sub BtnDeleteMaleSprite_Click(sender As Object, e As EventArgs) Handles btnDeleteMaleSprite.Click
        Dim tmpamount As Byte
        If Editorindex = 0 OrElse Editorindex > MaxClasses Then Exit Sub

        tmpamount = Classes(Editorindex).MaleSprite.GetUpperBound(0)

        ReDim Preserve Classes(Editorindex).MaleSprite(tmpamount - 1)

        LoadClassInfo = True
    End Sub

    Private Sub BtnAddFemaleSprite_Click(sender As Object, e As EventArgs) Handles btnAddFemaleSprite.Click
        Dim tmpamount As Byte
        If Editorindex = 0 OrElse Editorindex > MaxClasses Then Exit Sub

        tmpamount = Classes(Editorindex).FemaleSprite.GetUpperBound(0)

        ReDim Preserve Classes(Editorindex).FemaleSprite(tmpamount + 1)

        Classes(Editorindex).FemaleSprite(tmpamount + 1) = 1

        LoadClassInfo = True
    End Sub

    Private Sub BtnDeleteFemaleSprite_Click(sender As Object, e As EventArgs) Handles btnDeleteFemaleSprite.Click
        Dim tmpamount As Byte
        If Editorindex = 0 OrElse Editorindex > MaxClasses Then Exit Sub

        tmpamount = Classes(Editorindex).FemaleSprite.GetUpperBound(0)

        ReDim Preserve Classes(Editorindex).FemaleSprite(tmpamount - 1)

        LoadClassInfo = True
    End Sub

    Private Sub NudMaleSprite_ValueChanged(sender As Object, e As EventArgs) Handles nudMaleSprite.Click
        If cmbMaleSprite.SelectedIndex < 0 Then Exit Sub

        Classes(Editorindex).MaleSprite(cmbMaleSprite.SelectedIndex) = nudMaleSprite.Value

        DrawPreview()
    End Sub

    Private Sub NudFemaleSprite_ValueChanged(sender As Object, e As EventArgs) Handles nudFemaleSprite.Click
        If cmbFemaleSprite.SelectedIndex < 0 Then Exit Sub

        Classes(Editorindex).FemaleSprite(cmbFemaleSprite.SelectedIndex) = nudFemaleSprite.Value

        DrawPreview()
    End Sub

    Private Sub CmbMaleSprite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMaleSprite.SelectedIndexChanged
        nudMaleSprite.Value = Classes(Editorindex).MaleSprite(cmbMaleSprite.SelectedIndex)
        DrawPreview()
    End Sub

    Private Sub CmbFemaleSprite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFemaleSprite.SelectedIndexChanged
        nudFemaleSprite.Value = Classes(Editorindex).FemaleSprite(cmbFemaleSprite.SelectedIndex)
        DrawPreview()
    End Sub

    Sub DrawPreview()

        If File.Exists(Environment.CurrentDirectory & GFXPATH & "Characters\" & nudMaleSprite.Value & GFXEXT) Then
            picMale.Width = Image.FromFile(Environment.CurrentDirectory & GfxPath & "characters\" & nudMaleSprite.Value & GFXEXT).Width \ 4
            picMale.Height = Image.FromFile(Environment.CurrentDirectory & GfxPath & "characters\" & nudMaleSprite.Value & GFXEXT).Height \ 4
            picMale.BackgroundImage = Image.FromFile(Environment.CurrentDirectory & GfxPath & "Characters\" & nudMaleSprite.Value & GFXEXT)
        End If

        If File.Exists(Environment.CurrentDirectory & GfxPath & "Characters\" & nudFemaleSprite.Value & GFXEXT) Then
            picFemale.Width = Image.FromFile(Environment.CurrentDirectory & GfxPath & "characters\" & nudFemaleSprite.Value & GFXEXT).Width \ 4
            picFemale.Height = Image.FromFile(Environment.CurrentDirectory & GfxPath & "characters\" & nudFemaleSprite.Value & GFXEXT).Height \ 4
            picFemale.BackgroundImage = Image.FromFile(Environment.CurrentDirectory & GfxPath & "Characters\" & nudFemaleSprite.Value & GFXEXT)
        End If

    End Sub

    Private Sub PicMale_Paint(sender As Object, e As EventArgs) Handles picMale.Paint
        'nope
    End Sub

    Private Sub PicFemale_Paint(sender As Object, e As EventArgs) Handles picFemale.Paint
        'nope
    End Sub
#End Region

#Region "Stats"
    Private Sub NumStrength_ValueChanged(sender As Object, e As EventArgs) Handles nudStrength.Click
        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        Classes(Editorindex).Stat(StatType.Strength) = nudStrength.Value
    End Sub

    Private Sub NumLuck_ValueChanged(sender As Object, e As EventArgs) Handles nudLuck.Click
        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        Classes(Editorindex).Stat(StatType.Luck) = nudLuck.Value
    End Sub

    Private Sub NumEndurance_ValueChanged(sender As Object, e As EventArgs) Handles nudEndurance.Click
        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        Classes(Editorindex).Stat(StatType.Endurance) = nudEndurance.Value
    End Sub

    Private Sub NumIntelligence_ValueChanged(sender As Object, e As EventArgs) Handles nudIntelligence.Click
        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        Classes(Editorindex).Stat(StatType.Intelligence) = nudIntelligence.Value
    End Sub

    Private Sub NumVitality_ValueChanged(sender As Object, e As EventArgs) Handles nudVitality.Click
        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        Classes(Editorindex).Stat(StatType.Vitality) = nudVitality.Value
    End Sub

    Private Sub NumSpirit_ValueChanged(sender As Object, e As EventArgs) Handles nudSpirit.Click
        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        Classes(Editorindex).Stat(StatType.Spirit) = nudSpirit.Value
    End Sub

    Private Sub NumBaseExp_ValueChanged(sender As Object, e As EventArgs) Handles nudBaseExp.Click
        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        Classes(Editorindex).BaseExp = nudBaseExp.Value
    End Sub

#End Region

#Region "Start Items"
    Private Sub BtnItemAdd_Click(sender As Object, e As EventArgs) Handles btnItemAdd.Click
        If lstStartItems.SelectedIndex < 0 OrElse cmbItems.SelectedIndex < 0 Then Exit Sub

        Classes(Editorindex).StartItem(lstStartItems.SelectedIndex + 1) = cmbItems.SelectedIndex
        Classes(Editorindex).StartValue(lstStartItems.SelectedIndex + 1) = nudItemAmount.Value

        LoadClassInfo = True
    End Sub

#End Region

#Region "Starting Point"
    Private Sub NumStartMap_ValueChanged(sender As Object, e As EventArgs) Handles nudStartMap.Click
        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        Classes(Editorindex).StartMap = nudStartMap.Value
    End Sub

    Private Sub NumStartX_ValueChanged(sender As Object, e As EventArgs) Handles nudStartX.Click
        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        Classes(Editorindex).StartX = nudStartX.Value
    End Sub

    Private Sub NumStartY_ValueChanged(sender As Object, e As EventArgs) Handles nudStartY.Click
        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        Classes(Editorindex).StartY = nudStartY.Value
    End Sub

#End Region

End Class