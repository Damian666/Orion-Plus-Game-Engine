Friend Class frmEditor_Recipe

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        RecipeEditorOk()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        RecipeEditorCancel()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        If Editorindex = 0 OrElse Editorindex > MAX_RECIPE Then Exit Sub

        ClearRecipe(Editorindex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Recipe(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex

        lstIngredients.Items.Clear()

        RecipeEditorInit()
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer
        If Editorindex = 0 OrElse Editorindex > MAX_RECIPE Then Exit Sub
        tmpindex = lstIndex.SelectedIndex
        Recipe(Editorindex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Recipe(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub LstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        If Editorindex = 0 OrElse Editorindex > MAX_RECIPE Then Exit Sub
        RecipeEditorInit()
    End Sub

    Private Sub BtnIngredientAdd_Click(sender As Object, e As EventArgs) Handles btnIngredientAdd.Click
        If lstIngredients.SelectedIndex < 0 OrElse cmbIngredient.SelectedIndex = 0 Then Exit Sub

        Recipe(Editorindex).Ingredients(lstIngredients.SelectedIndex + 1).ItemNum = cmbIngredient.SelectedIndex
        Recipe(Editorindex).Ingredients(lstIngredients.SelectedIndex + 1).Value = numItemAmount.Value

        UpdateIngredient()

    End Sub

    Private Sub CmbMakeItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMakeItem.SelectedIndexChanged
        Recipe(Editorindex).MakeItemNum = cmbMakeItem.SelectedIndex
    End Sub

    Private Sub CmbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbType.SelectedIndexChanged
        Recipe(Editorindex).RecipeType = cmbType.SelectedIndex
    End Sub

    Private Sub NudAmount_ValueChanged(sender As Object, e As EventArgs) Handles nudAmount.ValueChanged
        Recipe(Editorindex).MakeItemAmount = nudAmount.Value
    End Sub

    Private Sub NudCreateTime_ValueChanged(sender As Object, e As EventArgs) Handles nudCreateTime.ValueChanged
        Recipe(Editorindex).CreateTime = nudCreateTime.Value
    End Sub

End Class