Friend Class FrmAdmin

    Private Sub FrmAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' set values for admin panel
        cmbSpawnItem.Items.Clear()

        ' Add the names
        For i = 1 To MAX_ITEMS
            cmbSpawnItem.Items.Add(i & ": " & Trim$(Item(i).Name))
        Next
    End Sub

#Region "Moderation"

    Private Sub BtnAdminWarpTo_Click(sender As Object, e As EventArgs) Handles btnAdminWarpTo.Click

        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        ' Check to make sure its a valid map #
        If nudAdminMap.Value > 0 AndAlso nudAdminMap.Value <= MAX_MAPS Then
            WarpTo(nudAdminMap.Value)
        Else
            AddText("Invalid map number.", ColorType.BrightRed)
        End If
    End Sub

    Private Sub BtnAdminBan_Click(sender As Object, e As EventArgs) Handles btnAdminBan.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        If Len(Trim$(txtAdminName.Text)) < 1 Then Return

        SendBan(Trim$(txtAdminName.Text))
    End Sub

    Private Sub BtnAdminKick_Click(sender As Object, e As EventArgs) Handles btnAdminKick.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        If Len(Trim$(txtAdminName.Text)) < 1 Then Return

        SendKick(Trim$(txtAdminName.Text))
    End Sub

    Private Sub BtnAdminWarp2Me_Click(sender As Object, e As EventArgs) Handles btnAdminWarp2Me.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        If Len(Trim$(txtAdminName.Text)) < 1 Then Return

        If IsNumeric(Trim$(txtAdminName.Text)) Then Return

        WarpToMe(Trim$(txtAdminName.Text))
    End Sub

    Private Sub BtnAdminWarpMe2_Click(sender As Object, e As EventArgs) Handles btnAdminWarpMe2.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        If Len(Trim$(txtAdminName.Text)) < 1 Then
            Return
        End If

        If IsNumeric(Trim$(txtAdminName.Text)) Then
            Return
        End If

        WarpMeTo(Trim$(txtAdminName.Text))
    End Sub

    Private Sub BtnAdminSetAccess_Click(sender As Object, e As EventArgs) Handles btnAdminSetAccess.Click
        If GetPlayerAccess(Myindex) < AdminType.Creator Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        If Len(Trim$(txtAdminName.Text)) < 2 Then
            Return
        End If

        If IsNumeric(Trim$(txtAdminName.Text)) OrElse cmbAccess.SelectedIndex < 0 Then
            Return
        End If

        SendSetAccess(Trim$(txtAdminName.Text), cmbAccess.SelectedIndex)
    End Sub

    Private Sub BtnAdminSetSprite_Click(sender As Object, e As EventArgs) Handles btnAdminSetSprite.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        If nudAdminSprite.Value < 1 Then Return

        SendSetSprite(nudAdminSprite.Value)
    End Sub

#End Region

#Region "Editors"

    Private Sub btnAnimationEditor_Click(sender As Object, e As EventArgs) Handles btnAnimationEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestAnimations()
        SendRequestEditAnimation()
    End Sub

    Private Sub btnClassEditor_Click(sender As Object, e As EventArgs) Handles btnClassEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestClasses()
        SendRequestEditClass()
    End Sub

    Private Sub btnhouseEditor_Click(sender As Object, e As EventArgs) Handles btnhouseEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestEditHouse()
    End Sub

    Private Sub btnItemEditor_Click(sender As Object, e As EventArgs) Handles btnItemEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestItems()
        SendRequestEditItem()
    End Sub

    Private Sub btnMapAuto_Click(sender As Object, e As EventArgs) Handles btnAutoMapper.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestAutoMapper()
    End Sub

    Private Sub BtnMapEditor_Click(sender As Object, e As EventArgs) Handles btnMapEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestEditMap()
    End Sub

    Private Sub btnNPCEditor_Click(sender As Object, e As EventArgs) Handles btnNPCEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestNpcs()
        SendRequestEditNpc()
    End Sub

    Private Sub btnPetEditor_Click(sender As Object, e As EventArgs) Handles btnPetEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestPets()
        SendRequestEditPet()
    End Sub

    Private Sub btnProjectiles_Click(sender As Object, e As EventArgs) Handles btnProjectiles.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestProjectiles()
        SendRequestEditProjectiles()
    End Sub

    Private Sub btnQuest_Click(sender As Object, e As EventArgs) Handles btnQuest.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestQuests()
        SendRequestEditQuest()
    End Sub

    Private Sub btnRecipeEditor_Click(sender As Object, e As EventArgs) Handles btnRecipeEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestRecipes()
        SendRequestEditRecipes()
    End Sub

    Private Sub btnResourceEditor_Click(sender As Object, e As EventArgs) Handles btnResourceEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestResources()
        SendRequestEditResource()
    End Sub

    Private Sub btnShopEditor_Click(sender As Object, e As EventArgs) Handles btnShopEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestShops()
        SendRequestEditShop()
    End Sub

    Private Sub btnSkillEditor_Click(sender As Object, e As EventArgs) Handles btnSkillEditor.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestSkills()
        SendRequestEditSkill()
    End Sub

#End Region

#Region "Map Report"

    Private Sub BtnMapReport_Click(sender As Object, e As EventArgs) Handles btnMapReport.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If
        SendRequestMapreport()
    End Sub

    Private Sub LstMaps_DoubleClick(sender As Object, e As EventArgs) Handles lstMaps.DoubleClick
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        ' Check to make sure its a valid map #
        If lstMaps.FocusedItem.Index + 1 > 0 AndAlso lstMaps.FocusedItem.Index + 1 <= MAX_MAPS Then
            WarpTo(lstMaps.FocusedItem.Index + 1)
        Else
            AddText("Invalid map number: " & lstMaps.FocusedItem.Index + 1, QColorType.AlertColor)
        End If
    End Sub

#End Region

#Region "Misc"

    Private Sub CmbSpawnItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSpawnItem.SelectedIndexChanged
        If Item(cmbSpawnItem.SelectedIndex + 1).Type = ItemType.Currency OrElse Item(cmbSpawnItem.SelectedIndex + 1).Stackable = 1 Then
            nudSpawnItemAmount.Enabled = True
            Return
        End If
        nudSpawnItemAmount.Enabled = False
    End Sub

    Private Sub BtnSpawnItem_Click(sender As Object, e As EventArgs) Handles btnSpawnItem.Click
        If GetPlayerAccess(Myindex) < AdminType.Creator Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendSpawnItem(cmbSpawnItem.SelectedIndex + 1, nudSpawnItemAmount.Value)
    End Sub

    Private Sub BtnLevelUp_Click(sender As Object, e As EventArgs) Handles btnLevelUp.Click
        If GetPlayerAccess(Myindex) < AdminType.Developer Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendRequestLevelUp()

    End Sub

    Private Sub BtnALoc_Click(sender As Object, e As EventArgs) Handles btnALoc.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        BLoc = Not BLoc
    End Sub

    Private Sub BtnRespawn_Click(sender As Object, e As EventArgs) Handles btnRespawn.Click
        If GetPlayerAccess(Myindex) < AdminType.Mapper Then
            AddText("You need to be a high enough staff member to do this!", QColorType.AlertColor)
            Return
        End If

        SendMapRespawn()
    End Sub
#End Region

End Class