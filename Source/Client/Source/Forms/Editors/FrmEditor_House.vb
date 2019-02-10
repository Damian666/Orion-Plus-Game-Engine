Friend Class frmEditor_House

    Private Sub LstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        HouseEditorInit()
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer

        If Editorindex <= 0 Then Exit Sub

        tmpindex = lstIndex.SelectedIndex
        House(Editorindex).ConfigName = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & House(Editorindex).ConfigName)
        lstIndex.SelectedIndex = tmpindex

    End Sub

    Private Sub NudBaseMap_ValueChanged(sender As Object, e As EventArgs) Handles nudBaseMap.KeyUp, nudBaseMap.Click
        If Editorindex <= 0 Then Exit Sub

        If nudBaseMap.Value < 1 OrElse nudBaseMap.Value > MAX_MAPS Then Exit Sub
        House(Editorindex).BaseMap = nudBaseMap.Value
    End Sub

    Private Sub NudX_ValueChanged(sender As Object, e As EventArgs) Handles nudX.KeyUp, nudX.Click
        If Editorindex <= 0 Then Exit Sub

        If nudX.Value < 0 OrElse nudX.Value > 255 Then Exit Sub
        House(Editorindex).X = nudX.Value

    End Sub

    Private Sub NudY_ValueChanged(sender As Object, e As EventArgs) Handles nudY.KeyUp, nudY.Click
        If Editorindex <= 0 Then Exit Sub

        If nudY.Value < 0 OrElse nudY.Value > 255 Then Exit Sub
        House(Editorindex).Y = nudY.Value

    End Sub

    Private Sub NudPrice_ValueChanged(sender As Object, e As EventArgs) Handles nudPrice.KeyUp, nudPrice.Click
        If Editorindex <= 0 Then Exit Sub

        House(Editorindex).Price = nudPrice.Value

    End Sub

    Private Sub NudFurniture_ValueChanged(sender As Object, e As EventArgs) Handles nudFurniture.KeyUp, nudFurniture.Click
        If Editorindex <= 0 Then Exit Sub

        If nudFurniture.Value < 0 Then Exit Sub
        House(Editorindex).MaxFurniture = nudFurniture.Value
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Len(Trim$(txtName.Text)) = 0 Then
            MsgBox("Name required.")
            Exit Sub
        End If

        If nudBaseMap.Value = 0 Then
            MsgBox("Base map required.")
            Exit Sub
        End If

        HouseEditorOk()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        HouseEditorCancel()
    End Sub

End Class