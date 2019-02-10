Friend Class frmEditor_Item

#Region "Form Code"

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ItemEditorOk()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ItemEditorCancel()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        ClearItem(Editorindex + 1)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Item(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex

        ItemEditorInit()
    End Sub

    Private Sub LstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub
        ItemEditorInit()
        PicFurniture_MouseDown(sender, e)
    End Sub

    Private Sub PicItem_Paint(sender As Object, e As PaintEventArgs) Handles picItem.Paint
        'Dont let it auto paint ;)
    End Sub

    Private Sub PicPaperdoll_Paint(sender As Object, e As PaintEventArgs) Handles picPaperdoll.Paint
        'Dont let it auto paint :0
    End Sub

    Private Sub PicFurniture_Paint(sender As Object, e As PaintEventArgs) Handles picFurniture.Paint
        'Dont let it auto paint ;)
    End Sub

    Private Sub FrmEditor_Item_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nudPic.Maximum = NumItems
        nudPaperdoll.Maximum = NumPaperdolls
        nudFurniture.Maximum = NumFurniture
        cmbFurnitureType.SelectedIndex = 0
    End Sub

    Private Sub BtnBasics_Click(sender As Object, e As EventArgs) Handles btnBasics.Click
        fraBasics.Visible = True
        fraRequirements.Visible = False
    End Sub

    Private Sub BtnRequirements_Click(sender As Object, e As EventArgs) Handles btnRequirements.Click
        fraBasics.Visible = False
        fraRequirements.Visible = True
    End Sub

#End Region

#Region "Basics"

    Private Sub NudPic_ValueChanged(sender As Object, e As EventArgs) Handles nudPic.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Pic = nudPic.Value
        EditorItem_DrawItem()
    End Sub

    Private Sub CmbBind_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBind.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).BindType = cmbBind.SelectedIndex
    End Sub

    Private Sub NudRarity_ValueChanged(sender As Object, e As EventArgs) Handles nudRarity.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Rarity = nudRarity.Value
    End Sub

    Private Sub CmbAnimation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAnimation.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Animation = cmbAnimation.SelectedIndex
    End Sub

    Private Sub CmbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbType.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        cmbSubType.Enabled = False

        If (cmbType.SelectedIndex = ItemType.Equipment) Then
            fraEquipment.Visible = True

            ' Build subtype cmb
            cmbSubType.Items.Clear()
            cmbSubType.Items.Add("None")

            cmbSubType.Items.Add("Weapon")
            cmbSubType.Items.Add("Armor")
            cmbSubType.Items.Add("Helmet")
            cmbSubType.Items.Add("Shield")
            cmbSubType.Items.Add("Shoes")
            cmbSubType.Items.Add("Gloves")

            cmbSubType.Enabled = True
            cmbSubType.SelectedIndex = Item(Editorindex).SubType
        Else
            fraEquipment.Visible = False
        End If

        If (cmbType.SelectedIndex = ItemType.Consumable) Then
            fraVitals.Visible = True

            ' Build subtype cmb
            cmbSubType.Items.Clear()
            cmbSubType.Items.Add("None")

            cmbSubType.Items.Add("Hp")
            cmbSubType.Items.Add("Mp")
            cmbSubType.Items.Add("Sp")
            cmbSubType.Items.Add("Exp")

            cmbSubType.Enabled = True
            cmbSubType.SelectedIndex = Item(Editorindex).SubType
        Else
            fraVitals.Visible = False
        End If

        If (cmbType.SelectedIndex = ItemType.Skill) Then
            fraSkill.Visible = True
        Else
            fraSkill.Visible = False
        End If

        If cmbType.SelectedIndex = ItemType.Furniture Then
            fraFurniture.Visible = True
        Else
            fraFurniture.Visible = False
        End If

        If cmbType.SelectedIndex = ItemType.Recipe Then
            fraRecipe.Visible = True
        Else
            fraRecipe.Visible = False
        End If

        If cmbType.SelectedIndex = ItemType.Pet Then
            fraPet.Visible = True
        Else
            fraPet.Visible = False
        End If

        Item(Editorindex).Type = cmbType.SelectedIndex
    End Sub

    Private Sub NudVitalMod_ValueChanged(sender As Object, e As EventArgs) Handles nudVitalMod.Click
        If Editorindex <= 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Data1 = nudVitalMod.Value
    End Sub

    Private Sub CmbSkills_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkills.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Data1 = cmbSkills.SelectedIndex
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub
        tmpindex = lstIndex.SelectedIndex
        Item(Editorindex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(Editorindex - 1)
        lstIndex.Items.Insert(Editorindex - 1, Editorindex & ": " & Item(Editorindex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub NudPrice_ValueChanged(sender As Object, e As EventArgs) Handles nudPrice.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Price = nudPrice.Value
    End Sub

    Private Sub ChkStackable_CheckedChanged(sender As Object, e As EventArgs) Handles chkStackable.CheckedChanged
        If chkStackable.Checked = True Then
            Item(Editorindex).Stackable = 1
        Else
            Item(Editorindex).Stackable = 0
        End If
    End Sub

    Private Sub CmbRecipe_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRecipe.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Data1 = cmbRecipe.SelectedIndex
    End Sub

    Private Sub TxtDescription_TextChanged(sender As Object, e As EventArgs) Handles txtDescription.TextChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Description = Trim$(txtDescription.Text)
    End Sub

    Private Sub CmbSubType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSubType.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).SubType = cmbSubType.SelectedIndex
    End Sub

    Private Sub NudItemLvl_ValueChanged(sender As Object, e As EventArgs) Handles nudItemLvl.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).ItemLevel = nudItemLvl.Value
    End Sub

    Private Sub CmbPet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPet.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Data1 = cmbPet.SelectedIndex
    End Sub

#End Region

#Region "Requirements"

    Private Sub CmbClassReq_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClassReq.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).ClassReq = cmbClassReq.SelectedIndex
    End Sub

    Private Sub CmbAccessReq_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAccessReq.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).AccessReq = cmbAccessReq.SelectedIndex
    End Sub

    Private Sub NudLevelReq_ValueChanged(sender As Object, e As EventArgs) Handles nudLevelReq.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).LevelReq = nudLevelReq.Value
    End Sub

    Private Sub NudStrReq_ValueChanged(sender As Object, e As EventArgs) Handles nudStrReq.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Stat_Req(StatType.Strength) = nudStrReq.Value
    End Sub

    Private Sub NudEndReq_ValueChanged(sender As Object, e As EventArgs) Handles nudEndReq.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Stat_Req(StatType.Endurance) = nudEndReq.Value
    End Sub

    Private Sub NudVitReq_ValueChanged(sender As Object, e As EventArgs) Handles nudVitReq.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Stat_Req(StatType.Vitality) = nudVitReq.Value
    End Sub

    Private Sub NudLuckReq_ValueChanged(sender As Object, e As EventArgs) Handles nudLuckReq.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Stat_Req(StatType.Luck) = nudLuckReq.Value
    End Sub

    Private Sub NudIntReq_ValueChanged(sender As Object, e As EventArgs) Handles nudIntReq.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Stat_Req(StatType.Intelligence) = nudIntReq.Value
    End Sub

    Private Sub NudSprReq_ValueChanged(sender As Object, e As EventArgs) Handles nudSprReq.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Stat_Req(StatType.Spirit) = nudSprReq.Value
    End Sub

#End Region

#Region "Equipment"

    Private Sub CmbTool_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTool.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub
        Item(Editorindex).Data3 = cmbTool.SelectedIndex
    End Sub

    Private Sub NudDamage_ValueChanged(sender As Object, e As EventArgs) Handles nudDamage.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Data2 = nudDamage.Value
    End Sub

    Private Sub NudSpeed_ValueChanged(sender As Object, e As EventArgs) Handles nudSpeed.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub
        lblSpeed.Text = "Speed: " & nudSpeed.Value / 1000 & " sec"
        Item(Editorindex).Speed = nudSpeed.Value
    End Sub

    Private Sub NudPaperdoll_ValueChanged(sender As Object, e As EventArgs) Handles nudPaperdoll.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Paperdoll = nudPaperdoll.Value
        EditorItem_DrawPaperdoll()
    End Sub

    Private Sub NudStrength_ValueChanged(sender As Object, e As EventArgs) Handles nudStrength.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Add_Stat(StatType.Strength) = nudStrength.Value
    End Sub

    Private Sub NudLuck_ValueChanged(sender As Object, e As EventArgs) Handles nudLuck.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Add_Stat(StatType.Luck) = nudLuck.Value
    End Sub

    Private Sub NudEndurance_ValueChanged(sender As Object, e As EventArgs) Handles nudEndurance.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Add_Stat(StatType.Endurance) = nudEndurance.Value
    End Sub

    Private Sub NudIntelligence_ValueChanged(sender As Object, e As EventArgs) Handles nudIntelligence.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Add_Stat(StatType.Intelligence) = nudIntelligence.Value
    End Sub

    Private Sub NudVitality_ValueChanged(sender As Object, e As EventArgs) Handles nudVitality.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Add_Stat(StatType.Vitality) = nudVitality.Value
    End Sub

    Private Sub NudSpirit_ValueChanged(sender As Object, e As EventArgs) Handles nudSpirit.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Add_Stat(StatType.Spirit) = nudSpirit.Value
    End Sub

    Private Sub ChkKnockBack_CheckedChanged(sender As Object, e As EventArgs) Handles chkKnockBack.CheckedChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        If chkKnockBack.Checked = True Then
            Item(Editorindex).KnockBack = 1
        Else
            Item(Editorindex).KnockBack = 0
        End If
    End Sub

    Private Sub CmbKnockBackTiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbKnockBackTiles.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).KnockBackTiles = cmbKnockBackTiles.SelectedIndex
    End Sub

    Private Sub ChkRandomize_CheckedChanged(sender As Object, e As EventArgs) Handles chkRandomize.CheckedChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        If chkRandomize.Checked = True Then
            Item(Editorindex).Randomize = 1
        Else
            Item(Editorindex).Randomize = 0
        End If
    End Sub

    Private Sub CmbProjectile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProjectile.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Projectile = cmbProjectile.SelectedIndex
    End Sub

    Private Sub CmbAmmo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAmmo.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Ammo = cmbAmmo.SelectedIndex
    End Sub

#End Region

#Region "Furniture"

    Private Sub CmbFurnitureType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFurnitureType.SelectedIndexChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub
        Item(Editorindex).Data1 = cmbFurnitureType.SelectedIndex
    End Sub

    Private Sub OptNoFurnitureEditing_CheckedChanged(sender As Object, e As EventArgs) Handles optNoFurnitureEditing.CheckedChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub
        If optNoFurnitureEditing.Checked = True Then
            lblSetOption.Text = "No Editing: Lets you look at the image without setting any options (blocks/fringes)."
        End If
        EditorItem_DrawFurniture()
    End Sub

    Private Sub OptSetBlocks_CheckedChanged(sender As Object, e As EventArgs) Handles optSetBlocks.CheckedChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub
        If optSetBlocks.Checked = True Then
            lblSetOption.Text = "Set Blocks: Os are passable and Xs are not. Simply place Xs where you do not want the player to walk."
        End If
        EditorItem_DrawFurniture()
    End Sub

    Private Sub OptSetFringe_CheckedChanged(sender As Object, e As EventArgs) Handles optSetFringe.CheckedChanged
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub
        If optSetFringe.Checked = True Then
            lblSetOption.Text = "Set Fringe: Os are draw on the fringe layer (the player can walk behind)."
        End If
        EditorItem_DrawFurniture()
    End Sub

    Private Sub NudFurniture_ValueChanged(sender As Object, e As EventArgs) Handles nudFurniture.Click
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub

        Item(Editorindex).Data2 = nudFurniture.Value

        If FurnitureGFXInfo(nudFurniture.Value).IsLoaded = False Then
            LoadTexture(nudFurniture.Value, 10)
        End If

        'seeying we still use it, lets update timer
        With FurnitureGFXInfo(nudFurniture.Value)
            .TextureTimer = GetTickCount() + 100000
        End With

        If nudFurniture.Value > 0 AndAlso nudFurniture.Value <= NumFurniture Then
            Item(Editorindex).FurnitureWidth = FurnitureGFXInfo(nudFurniture.Value).width / 32
            Item(Editorindex).FurnitureHeight = FurnitureGFXInfo(nudFurniture.Value).height / 32
            If Item(Editorindex).FurnitureHeight > 1 Then Item(Editorindex).FurnitureHeight = Item(Editorindex).FurnitureHeight - 1
        Else
            Item(Editorindex).FurnitureWidth = 1
            Item(Editorindex).FurnitureHeight = 1
        End If

        EditorItem_DrawFurniture()
    End Sub

    Private Sub PicFurniture_MouseDown(sender As Object, e As MouseEventArgs) Handles picFurniture.MouseDown
        If Editorindex = 0 OrElse Editorindex > MAX_ITEMS Then Exit Sub
        Dim X As Integer, Y As Integer
        X = e.X / 32
        Y = e.Y / 32

        If X > 3 Then Exit Sub
        If Y > 3 Then Exit Sub

        If optSetBlocks.Checked = True Then
            If Item(Editorindex).FurnitureBlocks(X, Y) = 1 Then
                Item(Editorindex).FurnitureBlocks(X, Y) = 0
            Else
                Item(Editorindex).FurnitureBlocks(X, Y) = 1
            End If
        ElseIf optSetFringe.Checked = True Then
            If Item(Editorindex).FurnitureFringe(X, Y) = 1 Then
                Item(Editorindex).FurnitureFringe(X, Y) = 0
            Else
                Item(Editorindex).FurnitureFringe(X, Y) = 1
            End If
        End If
        EditorItem_DrawFurniture()
    End Sub

#End Region

End Class