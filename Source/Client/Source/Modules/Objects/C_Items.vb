Imports System
Imports System.Drawing
Imports System.IO
Imports ASFW

Module C_Items
#Region "Globals & Types"
    ' inv drag + drop
    Friend DragInvSlotNum As Integer
    Friend InvX As Integer
    Friend InvY As Integer

    Friend InvItemFrame(MAX_INV) As Byte ' Used for animated items
    Friend LastItemDesc As Integer ' Stores the last item we showed in desc
#End Region

#Region "DataBase"
    Friend Sub ClearItem(index As Integer)
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

        ReDim Item(MAX_ITEMS)

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

    Friend Sub Packet_UpdateItem(ByRef data() As Byte)
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
        Item(n).Name = buffer.ReadString().Trim
        Item(n).Paperdoll = buffer.ReadInt32()
        Item(n).Pic = buffer.ReadInt32()
        Item(n).Price = buffer.ReadInt32()
        Item(n).Rarity = buffer.ReadInt32()
        Item(n).Speed = buffer.ReadInt32()

        Item(n).Randomize = buffer.ReadInt32()
        Item(n).RandomMin = buffer.ReadInt32()
        Item(n).RandomMax = buffer.ReadInt32()

        Item(n).Stackable = buffer.ReadInt32()
        Item(n).Description = buffer.ReadString().Trim

        For i = 0 To StatType.Count - 1
            Item(n).Stat_Req(i) = buffer.ReadInt32()
        Next

        Item(n).Type = buffer.ReadInt32()
        Item(n).SubType = buffer.ReadInt32

        'Housing
        Item(n).FurnitureWidth = buffer.ReadInt32()
        Item(n).FurnitureHeight = buffer.ReadInt32()

        For a = 0 To 3
            For b = 0 To 3
                Item(n).FurnitureBlocks(a, b) = buffer.ReadInt32()
                Item(n).FurnitureFringe(a, b) = buffer.ReadInt32()
            Next
        Next

        Item(n).KnockBack = buffer.ReadInt32()
        Item(n).KnockBackTiles = buffer.ReadInt32()

        Item(n).Projectile = buffer.ReadInt32()
        Item(n).Ammo = buffer.ReadInt32()

        buffer.Dispose()

        ' changes to inventory, need to clear any drop menu
        FrmGame.pnlCurrency.Visible = False
        FrmGame.txtCurrency.Text = ""
        TmpCurrencyItem = 0
        CurrencyMenu = 0 ' clear

    End Sub
#End Region

#Region "Outgoing Packets"
    Sub SendRequestItems()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestItems)

        Network.SendData(buffer.ToPacket)
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
        buffer.WriteString((Item(itemNum).Name.Trim))
        buffer.WriteInt32(Item(itemNum).Paperdoll)
        buffer.WriteInt32(Item(itemNum).Pic)
        buffer.WriteInt32(Item(itemNum).Price)
        buffer.WriteInt32(Item(itemNum).Rarity)
        buffer.WriteInt32(Item(itemNum).Speed)

        buffer.WriteInt32(Item(itemNum).Randomize)
        buffer.WriteInt32(Item(itemNum).RandomMin)
        buffer.WriteInt32(Item(itemNum).RandomMax)

        buffer.WriteInt32(Item(itemNum).Stackable)
        buffer.WriteString((Item(itemNum).Description.Trim))

        For i = 0 To StatType.Count - 1
            buffer.WriteInt32(Item(itemNum).Stat_Req(i))
        Next

        buffer.WriteInt32(Item(itemNum).Type)
        buffer.WriteInt32(Item(itemNum).SubType)

        buffer.WriteInt32(Item(itemNum).ItemLevel)

        'Housing
        buffer.WriteInt32(Item(itemNum).FurnitureWidth)
        buffer.WriteInt32(Item(itemNum).FurnitureHeight)

        For i = 0 To 3
            For x = 0 To 3
                buffer.WriteInt32(Item(itemNum).FurnitureBlocks(i, x))
                buffer.WriteInt32(Item(itemNum).FurnitureFringe(i, x))
            Next
        Next

        buffer.WriteInt32(Item(itemNum).KnockBack)
        buffer.WriteInt32(Item(itemNum).KnockBackTiles)

        buffer.WriteInt32(Item(itemNum).Projectile)
        buffer.WriteInt32(Item(itemNum).Ammo)

        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendRequestEditItem()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditItem)
        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub
#End Region


#Region "Editor"
    Friend Sub ItemEditorPreInit()
        Dim i As Integer

        With FrmItem
            Editor = EDITOR_ITEM
            .lstIndex.Items.Clear()

            ' Add the names
            For i = 1 To MAX_ITEMS
                .lstIndex.Items.Add(i & ": " & Item(i).Name.Trim)
            Next

            .Show()
            .lstIndex.SelectedIndex = 0
            ItemEditorInit()
        End With
    End Sub

    Friend Sub ItemEditorInit()
        Dim i As Integer

        If FrmItem.Visible = False Then Exit Sub
        Editorindex = FrmItem.lstIndex.SelectedIndex + 1

        With Item(Editorindex)
            'populate combo boxes
            FrmItem.cmbAnimation.Items.Clear()
            FrmItem.cmbAnimation.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                FrmItem.cmbAnimation.Items.Add(i & ": " & Animation(i).Name)
            Next

            FrmItem.cmbAmmo.Items.Clear()
            FrmItem.cmbAmmo.Items.Add("None")
            For i = 1 To MAX_ITEMS
                FrmItem.cmbAmmo.Items.Add(i & ": " & Item(i).Name)
            Next

            FrmItem.cmbProjectile.Items.Clear()
            FrmItem.cmbProjectile.Items.Add("None")
            For i = 1 To MaxProjectiles
                FrmItem.cmbProjectile.Items.Add(i & ": " & Projectiles(i).Name)
            Next

            FrmItem.cmbSkills.Items.Clear()
            FrmItem.cmbSkills.Items.Add("None")
            For i = 1 To MAX_SKILLS
                FrmItem.cmbSkills.Items.Add(i & ": " & Skill(i).Name)
            Next

            FrmItem.cmbPet.Items.Clear()
            FrmItem.cmbPet.Items.Add("None")
            For i = 1 To MAX_PETS
                FrmItem.cmbPet.Items.Add(i & ": " & Pet(i).Name)
            Next

            FrmItem.cmbRecipe.Items.Clear()
            FrmItem.cmbRecipe.Items.Add("None")
            For i = 1 To MAX_RECIPE
                FrmItem.cmbRecipe.Items.Add(i & ": " & Recipe(i).Name)
            Next

            FrmItem.txtName.Text = .Name.Trim
            FrmItem.txtDescription.Text = .Description.Trim

            If .Pic > FrmItem.nudPic.Maximum Then .Pic = 0
            FrmItem.nudPic.Value = .Pic
            If .Type > ItemType.Count - 1 Then .Type = 0
            FrmItem.cmbType.SelectedIndex = .Type
            FrmItem.cmbAnimation.SelectedIndex = .Animation

            If .ItemLevel = 0 Then .ItemLevel = 1
            FrmItem.nudItemLvl.Value = .ItemLevel

            ' Type specific settings
            If (FrmItem.cmbType.SelectedIndex = ItemType.Equipment) Then
                FrmItem.fraEquipment.Visible = True
                FrmItem.cmbProjectile.SelectedIndex = .Data1
                FrmItem.nudDamage.Value = .Data2
                FrmItem.cmbTool.SelectedIndex = .Data3

                FrmItem.cmbSubType.SelectedIndex = .SubType

                If .Speed < 100 Then .Speed = 100
                If .Speed > FrmItem.nudSpeed.Maximum Then .Speed = FrmItem.nudSpeed.Maximum
                FrmItem.nudSpeed.Value = .Speed

                FrmItem.nudStrength.Value = .Add_Stat(StatType.Strength)
                FrmItem.nudEndurance.Value = .Add_Stat(StatType.Endurance)
                FrmItem.nudIntelligence.Value = .Add_Stat(StatType.Intelligence)
                FrmItem.nudVitality.Value = .Add_Stat(StatType.Vitality)
                FrmItem.nudLuck.Value = .Add_Stat(StatType.Luck)
                FrmItem.nudSpirit.Value = .Add_Stat(StatType.Spirit)

                If .KnockBack = 1 Then
                    FrmItem.chkKnockBack.Checked = True
                Else
                    FrmItem.chkKnockBack.Checked = False
                End If
                FrmItem.cmbKnockBackTiles.SelectedIndex = .KnockBackTiles

                If .Randomize = 1 Then
                    FrmItem.chkRandomize.Checked = True
                Else
                    FrmItem.chkRandomize.Checked = False
                End If

                'If .RandomMin = 0 Then .RandomMin = 1
                'frmEditor_Item.numMin.Value = .RandomMin

                'If .RandomMax <= 1 Then .RandomMax = 2
                'frmEditor_Item.numMax.Value = .RandomMax

                FrmItem.nudPaperdoll.Value = .Paperdoll

                FrmItem.cmbProjectile.SelectedIndex = .Projectile
                FrmItem.cmbAmmo.SelectedIndex = .Ammo
            Else
                FrmItem.fraEquipment.Visible = False
            End If

            If (FrmItem.cmbType.SelectedIndex = ItemType.Consumable) Then
                FrmItem.fraVitals.Visible = True
                FrmItem.nudVitalMod.Value = .Data1
            Else
                FrmItem.fraVitals.Visible = False
            End If

            If (FrmItem.cmbType.SelectedIndex = ItemType.Skill) Then
                FrmItem.fraSkill.Visible = True
                FrmItem.cmbSkills.SelectedIndex = .Data1
            Else
                FrmItem.fraSkill.Visible = False
            End If

            If FrmItem.cmbType.SelectedIndex = ItemType.Furniture Then
                FrmItem.fraFurniture.Visible = True
                If Item(Editorindex).Data2 > 0 AndAlso Item(Editorindex).Data2 <= NumFurniture Then
                    FrmItem.nudFurniture.Value = Item(Editorindex).Data2
                Else
                    FrmItem.nudFurniture.Value = 1
                End If
                FrmItem.cmbFurnitureType.SelectedIndex = Item(Editorindex).Data1
            Else
                FrmItem.fraFurniture.Visible = False
            End If

            If (FrmItem.cmbType.SelectedIndex = ItemType.Pet) Then
                FrmItem.fraPet.Visible = True
                FrmItem.cmbPet.SelectedIndex = .Data1
            Else
                FrmItem.fraPet.Visible = False
            End If

            ' Basic requirements
            FrmItem.cmbAccessReq.SelectedIndex = .AccessReq
            FrmItem.nudLevelReq.Value = .LevelReq

            FrmItem.nudStrReq.Value = .Stat_Req(StatType.Strength)
            FrmItem.nudVitReq.Value = .Stat_Req(StatType.Vitality)
            FrmItem.nudLuckReq.Value = .Stat_Req(StatType.Luck)
            FrmItem.nudEndReq.Value = .Stat_Req(StatType.Endurance)
            FrmItem.nudIntReq.Value = .Stat_Req(StatType.Intelligence)
            FrmItem.nudSprReq.Value = .Stat_Req(StatType.Spirit)

            ' Build cmbClassReq
            FrmItem.cmbClassReq.Items.Clear()
            FrmItem.cmbClassReq.Items.Add("None")

            For i = 1 To MaxClasses
                FrmItem.cmbClassReq.Items.Add(Classes(i).Name)
            Next

            FrmItem.cmbClassReq.SelectedIndex = .ClassReq
            ' Info
            FrmItem.nudPrice.Value = .Price
            FrmItem.cmbBind.SelectedIndex = .BindType
            FrmItem.nudRarity.Value = .Rarity

            If .Stackable = 1 Then
                FrmItem.chkStackable.Checked = True
            Else
                FrmItem.chkStackable.Checked = False
            End If

            Editorindex = FrmItem.lstIndex.SelectedIndex + 1
        End With

        FrmItem.nudPic.Maximum = NumItems

        If NumPaperdolls > 0 Then
            FrmItem.nudPaperdoll.Maximum = NumPaperdolls + 1
        End If

        EditorItem_DrawItem()
        EditorItem_DrawPaperdoll()
        EditorItem_DrawFurniture()
        Item_Changed(Editorindex) = True

    End Sub

    Friend Sub ItemEditorCancel()
        Editor = 0
        FrmItem.Visible = False
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

        FrmItem.Visible = False
        Editor = 0
        ClearChanged_Item()
    End Sub
#End Region

#Region "Drawing"
    Friend Sub CheckItems()
        Dim i As Integer
        i = 1

        While File.Exists(Environment.CurrentDirectory & GfxPath & "Items\" & i & GfxExt)
            NumItems = NumItems + 1
            i = i + 1
        End While

        If NumItems = 0 Then Exit Sub
    End Sub

    Friend Sub EditorItem_DrawItem()
        Dim itemnum As Integer
        itemnum = FrmItem.nudPic.Value

        If itemnum < 1 OrElse itemnum > NumItems Then
            FrmItem.picItem.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Environment.CurrentDirectory & GfxPath & "items\" & itemnum & GfxExt) Then
            FrmItem.picItem.BackgroundImage = Drawing.Image.FromFile(Environment.CurrentDirectory & GfxPath & "items\" & itemnum & GfxExt)
        End If

    End Sub

    Friend Sub EditorItem_DrawPaperdoll()
        Dim Sprite As Integer

        Sprite = FrmItem.nudPaperdoll.Value

        If Sprite < 1 OrElse Sprite > NumPaperdolls Then
            FrmItem.picPaperdoll.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Environment.CurrentDirectory & GfxPath & "paperdolls\" & Sprite & GfxExt) Then
            FrmItem.picPaperdoll.BackgroundImage = Drawing.Image.FromFile(Environment.CurrentDirectory & GfxPath & "paperdolls\" & Sprite & GfxExt)
        End If
    End Sub

    Friend Sub EditorItem_DrawFurniture()
        Dim Furniturenum As Integer
        Dim sRECT As Rectangle
        Dim dRECT As Rectangle
        Furniturenum = FrmItem.nudFurniture.Value

        If Furniturenum < 1 OrElse Furniturenum > NumFurniture Then
            EditorItem_Furniture.Clear(ToSfmlColor(FrmItem.picFurniture.BackColor))
            EditorItem_Furniture.Display()
            Exit Sub
        End If

        If FurnitureGfxInfo(Furniturenum).IsLoaded = False Then
            LoadTexture(Furniturenum, 10)
        End If

        'seeying we still use it, lets update timer
        With FurnitureGfxInfo(Furniturenum)
            .TextureTimer = GetTickCount() + 100000
        End With

        ' rect for source
        With sRECT
            .Y = 0
            .Height = FurnitureGfxInfo(Furniturenum).Height
            .X = 0
            .Width = FurnitureGfxInfo(Furniturenum).Width
        End With

        ' same for destination as source
        dRECT = sRECT

        EditorItem_Furniture.Clear(ToSfmlColor(FrmItem.picFurniture.BackColor))

        RenderSprite(FurnitureSprite(Furniturenum), EditorItem_Furniture, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)

        If FrmItem.optSetBlocks.Checked = True Then
            For X = 0 To 3
                For Y = 0 To 3
                    If X <= (FurnitureGfxInfo(Furniturenum).Width / 32) - 1 Then
                        If Y <= (FurnitureGfxInfo(Furniturenum).Height / 32) - 1 Then
                            If Item(Editorindex).FurnitureBlocks(X, Y) = 1 Then
                                DrawText(X * 32 + 8, Y * 32 + 8, "X", SFML.Graphics.Color.Red, SFML.Graphics.Color.Black, EditorItem_Furniture)
                            Else
                                DrawText(X * 32 + 8, Y * 32 + 8, "O", SFML.Graphics.Color.Blue, SFML.Graphics.Color.Black, EditorItem_Furniture)
                            End If
                        End If
                    End If
                Next
            Next
        ElseIf FrmItem.optSetFringe.Checked = True Then
            For X = 0 To 3
                For Y = 0 To 3
                    If X <= (FurnitureGfxInfo(Furniturenum).Width / 32) - 1 Then
                        If Y <= (FurnitureGfxInfo(Furniturenum).Height / 32) Then
                            If Item(Editorindex).FurnitureFringe(X, Y) = 1 Then
                                DrawText(X * 32 + 8, Y * 32 + 8, "O", SFML.Graphics.Color.Blue, SFML.Graphics.Color.Black, EditorItem_Furniture)
                            End If
                        End If
                    End If
                Next
            Next
        End If
        EditorItem_Furniture.Display()
    End Sub
#End Region

End Module







































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































