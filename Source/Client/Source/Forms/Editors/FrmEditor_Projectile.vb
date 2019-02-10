Friend Class frmEditor_Projectile

    Private Sub FrmEditor_Projectile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nudPic.Maximum = NumProjectiles
    End Sub

    Private Sub LstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        ProjectileEditorInit()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ProjectileEditorOk()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ProjectileEditorCancel()
    End Sub

    Private Sub TxtName_TextChanged(sender As System.Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer

        If Editorindex < 1 OrElse Editorindex > MaxProjectiles Then Exit Sub

        tmpindex = lstIndex.SelectedIndex
        Projectiles(Editorindex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Projectiles(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub NudPic_ValueChanged(sender As Object, e As EventArgs) Handles nudPic.Click
        If Editorindex < 1 OrElse Editorindex > MaxProjectiles Then Exit Sub

        Projectiles(Editorindex).Sprite = nudPic.Value
    End Sub

    Private Sub NudRange_ValueChanged(sender As Object, e As EventArgs) Handles nudRange.Click
        If Editorindex < 1 OrElse Editorindex > MaxProjectiles Then Exit Sub

        Projectiles(Editorindex).Range = nudRange.Value
    End Sub

    Private Sub NudSpeed_ValueChanged(sender As Object, e As EventArgs) Handles nudSpeed.Click
        If Editorindex < 1 OrElse Editorindex > MaxProjectiles Then Exit Sub

        Projectiles(Editorindex).Speed = nudSpeed.Value
    End Sub

    Private Sub NudDamage_ValueChanged(sender As Object, e As EventArgs) Handles nudDamage.Click
        If Editorindex < 1 OrElse Editorindex > MaxProjectiles Then Exit Sub

        Projectiles(Editorindex).Damage = nudDamage.Value
    End Sub

End Class