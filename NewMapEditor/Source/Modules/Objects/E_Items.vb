Imports System.IO
Imports ASFW

Friend Module E_Items
#Region "Database"
    Friend Sub ClearItem(index As Integer)
        index = index - 1
        Item(index) = Nothing
        Item(index) = New ItemRec
        For x = 0 To StatType.Count - 1
            ReDim Item(index).Add_Stat(x)
        Next
        For x = 0 To StatType.Count - 1
            ReDim Item(index).Stat_Req(x)
        Next

        ReDim Item(index).FurnitureBlocks(3, 3)
        ReDim Item(index).FurnitureFringe(3, 3)

        Item(index).Name = ""
    End Sub

    Friend Sub ClearChanged_Item()
        For i = 1 To MAX_ITEMS
            Item_Changed(i) = Nothing
        Next i
        ReDim Item_Changed(MAX_ITEMS)
    End Sub

    Sub ClearItems()
        Dim i As Integer

        For i = 1 To MAX_ITEMS
            ClearItem(i)
        Next

    End Sub
#End Region

#Region "Incoming Packets"
    Sub Packet_EditItem(ByRef data() As Byte)
        Dim buffer As ByteStream
        buffer = New ByteStream(data)
        InitItemEditor = True

        buffer.Dispose()
    End Sub

    Sub Packet_UpdateItem(ByRef data() As Byte)
        Dim n As Integer, i As Integer
        Dim buffer As New ByteStream(data)
        n = buffer.ReadInt32

        ' Update the item
        Item(n).AccessReq = buffer.ReadInt32()

        For i = 0 To StatType.Count - 1
            Item(n).Add_Stat(i) = buffer.ReadInt32()
        Next

        Item(n).Animation = buffer.ReadInt32()
        Item(n).BindType = buffer.ReadInt32()
        Item(n).ClassReq = buffer.ReadInt32()
        Item(n).Data1 = buffer.ReadInt32()
        Item(n).Data2 = buffer.ReadInt32()
        Item(n).Data3 = buffer.ReadInt32()
        Item(n).TwoHanded = buffer.ReadInt32()
        Item(n).LevelReq = buffer.ReadInt32()
        Item(n).Mastery = buffer.ReadInt32()
        Item(n).Name = Trim$(buffer.ReadString())
        Item(n).Paperdoll = buffer.ReadInt32()
        Item(n).Pic = buffer.ReadInt32()
        Item(n).Price = buffer.ReadInt32()
        Item(n).Rarity = buffer.ReadInt32()
        Item(n).Speed = buffer.ReadInt32()

        Item(n).Randomize = buffer.ReadInt32()
        Item(n).RandomMin = buffer.ReadInt32()
        Item(n).RandomMax = buffer.ReadInt32()

        Item(n).Stackable = buffer.ReadInt32()
        Item(n).Description = Trim$(buffer.ReadString())

        For i = 0 To StatType.Count - 1
            Item(n).Stat_Req(i) = buffer.ReadInt32()
        Next

        Item(n).Type = buffer.ReadInt32()
        Item(n).SubType = buffer.ReadInt32()

        Item(n).ItemLevel = buffer.ReadInt32()

        'Housing
        Item(n).FurnitureWidth = buffer.ReadInt32()
        Item(n).FurnitureHeight = buffer.ReadInt32()

        For a = 1 To 3
            For b = 1 To 3
                Item(n).FurnitureBlocks(a, b) = buffer.ReadInt32()
                Item(n).FurnitureFringe(a, b) = buffer.ReadInt32()
            Next
        Next

        Item(n).KnockBack = buffer.ReadInt32()
        Item(n).KnockBackTiles = buffer.ReadInt32()

        Item(n).Projectile = buffer.ReadInt32()
        Item(n).Ammo = buffer.ReadInt32()

        buffer.Dispose()

    End Sub
#End Region

#Region "Outgoing Packets"
    Sub SendRequestItems()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestItems)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendSaveItem(itemNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveItem)
        buffer.WriteInt32(itemNum)
        buffer.WriteInt32(Item(itemNum).AccessReq)

        For i = 0 To StatType.Count - 1
            buffer.WriteInt32(Item(itemNum).Add_Stat(i))
        Next

        buffer.WriteInt32(Item(itemNum).Animation)
        buffer.WriteInt32(Item(itemNum).BindType)
        buffer.WriteInt32(Item(itemNum).ClassReq)
        buffer.WriteInt32(Item(itemNum).Data1)
        buffer.WriteInt32(Item(itemNum).Data2)
        buffer.WriteInt32(Item(itemNum).Data3)
        buffer.WriteInt32(Item(itemNum).TwoHanded)
        buffer.WriteInt32(Item(itemNum).LevelReq)
        buffer.WriteInt32(Item(itemNum).Mastery)
        buffer.WriteString((Trim$(Item(itemNum).Name)))
        buffer.WriteInt32(Item(itemNum).Paperdoll)
        buffer.WriteInt32(Item(itemNum).Pic)
        buffer.WriteInt32(Item(itemNum).Price)
        buffer.WriteInt32(Item(itemNum).Rarity)
        buffer.WriteInt32(Item(itemNum).Speed)

        buffer.WriteInt32(Item(itemNum).Randomize)
        buffer.WriteInt32(Item(itemNum).RandomMin)
        buffer.WriteInt32(Item(itemNum).RandomMax)

        buffer.WriteInt32(Item(itemNum).Stackable)
        buffer.WriteString((Trim$(Item(itemNum).Description)))

        For i = 0 To StatType.Count - 1
            buffer.WriteInt32(Item(itemNum).Stat_Req(i))
        Next

        buffer.WriteInt32(Item(itemNum).Type)
        buffer.WriteInt32(Item(itemNum).SubType)

        buffer.WriteInt32(Item(itemNum).ItemLevel)

        'Housing
        buffer.WriteInt32(Item(itemNum).FurnitureWidth)
        buffer.WriteInt32(Item(itemNum).FurnitureHeight)

        For i = 1 To 3
            For x = 1 To 3
                buffer.WriteInt32(Item(itemNum).FurnitureBlocks(i, x))
                buffer.WriteInt32(Item(itemNum).FurnitureFringe(i, x))
            Next
        Next

        buffer.WriteInt32(Item(itemNum).KnockBack)
        buffer.WriteInt32(Item(itemNum).KnockBackTiles)

        buffer.WriteInt32(Item(itemNum).Projectile)
        buffer.WriteInt32(Item(itemNum).Ammo)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditItem()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditItem)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub
#End Region

#Region "Editor"
    Friend Sub ItemEditorPreInit()
        Dim i As Integer

        With frmItem
            Editor = EDITOR_ITEM
            .lstIndex.Items.Clear()

            ' Add the names
            For i = 1 To MAX_ITEMS
                .lstIndex.Items.Add(i & ": " & Trim$(Item(i).Name))
            Next

            .Show()
            .lstIndex.SelectedIndex = 0
            ItemEditorInit()
        End With
    End Sub

    Friend Sub ItemEditorInit()
        Dim i As Integer

        If frmItem.Visible = False Then Exit Sub
        Editorindex = frmItem.lstIndex.SelectedIndex + 1

        With Item(Editorindex)
            'populate combo boxes
            frmItem.cmbAnimation.Items.Clear()
            frmItem.cmbAnimation.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                frmItem.cmbAnimation.Items.Add(i & ": " & Animation(i).Name)
            Next

            frmItem.cmbAmmo.Items.Clear()
            frmItem.cmbAmmo.Items.Add("None")
            For i = 1 To MAX_ITEMS
                frmItem.cmbAmmo.Items.Add(i & ": " & Item(i).Name)
            Next

            frmItem.cmbProjectile.Items.Clear()
            frmItem.cmbProjectile.Items.Add("None")
            For i = 1 To MAX_PROJECTILES
                frmItem.cmbProjectile.Items.Add(i & ": " & Projectiles(i).Name)
            Next

            frmItem.cmbSkills.Items.Clear()
            frmItem.cmbSkills.Items.Add("None")
            For i = 1 To MAX_SKILLS
                frmItem.cmbSkills.Items.Add(i & ": " & Skill(i).Name)
            Next

            frmItem.cmbPet.Items.Clear()
            frmItem.cmbPet.Items.Add("None")
            For i = 1 To MAX_PETS
                frmItem.cmbPet.Items.Add(i & ": " & Pet(i).Name)
            Next

            frmItem.cmbRecipe.Items.Clear()
            frmItem.cmbRecipe.Items.Add("None")
            For i = 1 To MAX_RECIPE
                frmItem.cmbRecipe.Items.Add(i & ": " & Recipe(i).Name)
            Next

            frmItem.txtName.Text = Trim$(.Name)
            frmItem.txtDescription.Text = Trim$(.Description)

            If .Pic > frmItem.nudPic.Maximum Then .Pic = 0
            frmItem.nudPic.Value = .Pic
            If .Type > ItemType.Count - 1 Then .Type = 0
            frmItem.cmbType.SelectedIndex = .Type
            frmItem.cmbAnimation.SelectedIndex = .Animation

            If .ItemLevel = 0 Then .ItemLevel = 1
            frmItem.nudItemLvl.Value = .ItemLevel

            ' Type specific settings
            If (frmItem.cmbType.SelectedIndex = ItemType.Equipment) Then
                frmItem.fraEquipment.Visible = True
                frmItem.cmbProjectile.SelectedIndex = .Data1
                frmItem.nudDamage.Value = .Data2
                frmItem.cmbTool.SelectedIndex = .Data3

                frmItem.cmbSubType.SelectedIndex = .SubType

                If .Speed < 100 Then .Speed = 100
                If .Speed > frmItem.nudSpeed.Maximum Then .Speed = frmItem.nudSpeed.Maximum
                frmItem.nudSpeed.Value = .Speed

                frmItem.nudStrength.Value = .Add_Stat(StatType.Strength)
                frmItem.nudEndurance.Value = .Add_Stat(StatType.Endurance)
                frmItem.nudIntelligence.Value = .Add_Stat(StatType.Intelligence)
                frmItem.nudVitality.Value = .Add_Stat(StatType.Vitality)
                frmItem.nudLuck.Value = .Add_Stat(StatType.Luck)
                frmItem.nudSpirit.Value = .Add_Stat(StatType.Spirit)

                If .KnockBack = 1 Then
                    frmItem.chkKnockBack.Checked = True
                Else
                    frmItem.chkKnockBack.Checked = False
                End If
                frmItem.cmbKnockBackTiles.SelectedIndex = .KnockBackTiles

                If .Randomize = 1 Then
                    frmItem.chkRandomize.Checked = True
                Else
                    frmItem.chkRandomize.Checked = False
                End If

                'If .RandomMin = 0 Then .RandomMin = 1
                'frmEditor_Item.numMin.Value = .RandomMin

                'If .RandomMax <= 1 Then .RandomMax = 2
                'frmEditor_Item.numMax.Value = .RandomMax

                frmItem.nudPaperdoll.Value = .Paperdoll

                frmItem.cmbProjectile.SelectedIndex = .Projectile
                frmItem.cmbAmmo.SelectedIndex = .Ammo
            Else
                frmItem.fraEquipment.Visible = False
            End If

            If (frmItem.cmbType.SelectedIndex = ItemType.Consumable) Then
                frmItem.fraVitals.Visible = True
                frmItem.nudVitalMod.Value = .Data1
            Else
                frmItem.fraVitals.Visible = False
            End If

            If (frmItem.cmbType.SelectedIndex = ItemType.Skill) Then
                frmItem.fraSkill.Visible = True
                frmItem.cmbSkills.SelectedIndex = .Data1
            Else
                frmItem.fraSkill.Visible = False
            End If

            If frmItem.cmbType.SelectedIndex = ItemType.Furniture Then
                frmItem.fraFurniture.Visible = True
                If Item(Editorindex).Data2 > 0 AndAlso Item(Editorindex).Data2 <= NumFurniture Then
                    frmItem.nudFurniture.Value = Item(Editorindex).Data2
                Else
                    frmItem.nudFurniture.Value = 1
                End If
                frmItem.cmbFurnitureType.SelectedIndex = Item(Editorindex).Data1
            Else
                frmItem.fraFurniture.Visible = False
            End If

            If (frmItem.cmbType.SelectedIndex = ItemType.Pet) Then
                frmItem.fraPet.Visible = True
                frmItem.cmbPet.SelectedIndex = .Data1
            Else
                frmItem.fraPet.Visible = False
            End If

            ' Basic requirements
            frmItem.cmbAccessReq.SelectedIndex = .AccessReq
            frmItem.nudLevelReq.Value = .LevelReq

            frmItem.nudStrReq.Value = .Stat_Req(StatType.Strength)
            frmItem.nudVitReq.Value = .Stat_Req(StatType.Vitality)
            frmItem.nudLuckReq.Value = .Stat_Req(StatType.Luck)
            frmItem.nudEndReq.Value = .Stat_Req(StatType.Endurance)
            frmItem.nudIntReq.Value = .Stat_Req(StatType.Intelligence)
            frmItem.nudSprReq.Value = .Stat_Req(StatType.Spirit)

            ' Build cmbClassReq
            frmItem.cmbClassReq.Items.Clear()
            frmItem.cmbClassReq.Items.Add("None")

            For i = 1 To Max_Classes
                frmItem.cmbClassReq.Items.Add(Classes(i).Name)
            Next

            frmItem.cmbClassReq.SelectedIndex = .ClassReq
            ' Info
            frmItem.nudPrice.Value = .Price
            frmItem.cmbBind.SelectedIndex = .BindType
            frmItem.nudRarity.Value = .Rarity

            If .Stackable = 1 Then
                frmItem.chkStackable.Checked = True
            Else
                frmItem.chkStackable.Checked = False
            End If

            Editorindex = frmItem.lstIndex.SelectedIndex + 1
        End With

        frmItem.nudPic.Maximum = NumItems

        If NumPaperdolls > 0 Then
            frmItem.nudPaperdoll.Maximum = NumPaperdolls + 1
        End If

        EditorItem_DrawItem()
        EditorItem_DrawPaperdoll()
        EditorItem_DrawFurniture()
        Item_Changed(Editorindex) = True

    End Sub

    Friend Sub ItemEditorCancel()
        Editor = 0
        frmItem.Visible = False
        ClearChanged_Item()
        ClearItems()
        SendRequestItems()
    End Sub

    Friend Sub ItemEditorOk()
        Dim i As Integer

        For i = 1 To MAX_ITEMS
            If Item_Changed(i) Then
                SendSaveItem(i)
            End If
        Next

        frmItem.Visible = False
        Editor = 0
        ClearChanged_Item()
    End Sub
#End Region

#Region "Drawing"
    Friend Sub CheckItems()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "Items\" & i & GFX_EXT)
            NumItems = NumItems + 1
            i = i + 1
        End While

        If NumItems = 0 Then Exit Sub
    End Sub
#End Region
End Module
