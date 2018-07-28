Friend Class FrmLogin
    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Main()
    End Sub

    Private Sub FrmLogin_UnLoad(sender As Object, e As EventArgs) Handles MyBase.Closing
        CloseEditor()
    End Sub

    Private Sub TmrConnect_Tick(sender As Object, e As EventArgs) Handles tmrConnect.Tick
        Static i As Integer
        If Socket.IsConnected() = True Then
            lblConnectionStatus.ForeColor = Color.Green
            lblConnectionStatus.Text = "Online..."
            tmrConnect.Enabled = False
        Else
            lblConnectionStatus.ForeColor = Color.Red
            i = i + 1
            If i = 5 Then
                Connect()
                lblConnectionStatus.Text = "Reconnecting..."
                lblConnectionStatus.ForeColor = Color.Orange
                i = 0
            Else
                lblConnectionStatus.Text = "Offline..."
            End If
        End If
    End Sub

    Friend Function IsLoginLegal(Username As String, Password As String) As Boolean
        If Len(Trim$(Username)) >= 3 Then
            If Len(Trim$(Password)) >= 3 Then
                IsLoginLegal = True
            Else
                IsLoginLegal = False
            End If
        Else
            IsLoginLegal = False
        End If

    End Function

    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Socket.IsConnected() Then
            If IsLoginLegal(txtLogin.Text, txtPassword.Text) Then
                SendEditorLogin(txtLogin.Text, txtPassword.Text)
            End If
        End If
    End Sub

#Region "Editors"
    Private Sub BtnMapEditor_Click(sender As Object, e As EventArgs) Handles btnMapEditor.Click
        SendEditorRequestMap(1)
    End Sub

    Private Sub BtnItemEditor_Click(sender As Object, e As EventArgs) Handles btnItemEditor.Click
        'SendRequestItems()
        'SendRequestEditItem()
    End Sub

    Private Sub BtnResourceEditor_Click(sender As Object, e As EventArgs) Handles btnResourceEditor.Click
        SendRequestResources()
        SendRequestEditResource()
    End Sub

    Private Sub BtnNPCEditor_Click(sender As Object, e As EventArgs) Handles btnNPCEditor.Click
        SendRequestNPCS()
        SendRequestEditNpc()
    End Sub

    Private Sub BtnSkillEditor_Click(sender As Object, e As EventArgs) Handles btnSkillEditor.Click
        SendRequestSkills()
        SendRequestEditSkill()
    End Sub

    Private Sub BtnShopEditor_Click(sender As Object, e As EventArgs) Handles btnShopEditor.Click
        SendRequestShops()
        SendRequestEditShop()
    End Sub

    Private Sub BtnAnimationEditor_Click(sender As Object, e As EventArgs) Handles btnAnimationEditor.Click
        SendRequestAnimations()
        SendRequestEditAnimation()
    End Sub

    Private Sub BtnQuest_Click(sender As Object, e As EventArgs) Handles btnQuest.Click
        'SendRequestQuests()
        'SendRequestEditQuest()
    End Sub

    Private Sub BtnhouseEditor_Click(sender As Object, e As EventArgs) Handles btnhouseEditor.Click
        'SendRequestEditHouse()
    End Sub

    Private Sub BtnProjectiles_Click(sender As Object, e As EventArgs) Handles btnProjectiles.Click
        'SendRequestProjectiles()
        'SendRequestEditProjectiles()
    End Sub

    Private Sub BtnClassEditor_Click(sender As Object, e As EventArgs) Handles btnClassEditor.Click
        SendRequestClasses()
        SendRequestEditClass()
    End Sub

    Private Sub BtnAutoMapper_Click(sender As Object, e As EventArgs) Handles btnAutoMapper.Click
        'SendRequestAutoMapper()
    End Sub

    Private Sub BtnRecipeEditor_Click(sender As Object, e As EventArgs) Handles btnRecipeEditor.Click
        'SendRequestRecipes()
        'SendRequestEditRecipes()
    End Sub

    Private Sub BtnPetEditor_Click(sender As Object, e As EventArgs) Handles btnPetEditor.Click
        'SendRequestPets()
        'SendRequestEditPet()
    End Sub

#End Region

End Class