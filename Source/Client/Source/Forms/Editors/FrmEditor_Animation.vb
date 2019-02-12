Friend Class FrmEditor_Animation

    Private Sub NudSprite0_ValueChanged(sender As Object, e As EventArgs) Handles nudSprite0.Click
        Animation(Editorindex).Sprite(0) = nudSprite0.Value
    End Sub

    Private Sub NudSprite1_ValueChanged(sender As Object, e As EventArgs) Handles nudSprite1.Click
        Animation(Editorindex).Sprite(1) = nudSprite1.Value
    End Sub

    Private Sub NudLoopCount0_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopCount0.Click
        Animation(Editorindex).LoopCount(0) = nudLoopCount0.Value
    End Sub

    Private Sub NudLoopCount1_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopCount1.Click
        Animation(Editorindex).LoopCount(1) = nudLoopCount1.Value
    End Sub

    Private Sub NudFrameCount0_ValueChanged(sender As Object, e As EventArgs) Handles nudFrameCount0.Click
        Animation(Editorindex).Frames(0) = nudFrameCount0.Value
    End Sub

    Private Sub NudFrameCount1_ValueChanged(sender As Object, e As EventArgs) Handles nudFrameCount1.Click
        Animation(Editorindex).Frames(1) = nudFrameCount1.Value
    End Sub

    Private Sub NudLoopTime0_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopTime0.Click
        Animation(Editorindex).LoopTime(0) = nudLoopTime0.Value
    End Sub

    Private Sub NudLoopTime1_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopTime1.Click
        Animation(Editorindex).LoopTime(1) = nudLoopTime1.Value
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        AnimationEditorOk()
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer
        If Editorindex = 0 OrElse Editorindex > MAX_ANIMATIONS Then Exit Sub
        tmpindex = lstIndex.SelectedIndex
        Animation(Editorindex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Animation(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub LstIndex_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstIndex.MouseClick
        AnimationEditorInit()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        If Editorindex = 0 OrElse Editorindex > MAX_ANIMATIONS Then Exit Sub

        ClearAnimation(Editorindex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Animation(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex

        AnimationEditorInit()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        AnimationEditorCancel()
    End Sub

    Private Sub FrmEditor_Animation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nudSprite0.Maximum = NumAnimations
        nudSprite1.Maximum = NumAnimations
    End Sub

    Private Sub CmbSound_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSound.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ANIMATIONS Then Exit Sub

        Animation(Editorindex).Sound = cmbSound.SelectedItem.ToString
    End Sub

End Class