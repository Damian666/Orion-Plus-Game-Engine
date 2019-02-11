Imports System.IO

Module C_Editors

#Region "Animation Editor"

    Friend Sub AnimationEditorInit()

        If FrmEditor_Animation.Visible = False Then Exit Sub

        Editorindex = FrmEditor_Animation.lstIndex.SelectedIndex + 1

        With Animation(Editorindex)

            ' find the music we have set
            FrmEditor_Animation.cmbSound.Items.Clear()
            FrmEditor_Animation.cmbSound.Items.Add("None")

            If UBound(SoundCache) > 0 Then
                For i = 1 To UBound(SoundCache)
                    FrmEditor_Animation.cmbSound.Items.Add(SoundCache(i))
                Next
            End If

            If Trim$(Animation(Editorindex).Sound) = "None" OrElse Trim$(Animation(Editorindex).Sound) = "" Then
                FrmEditor_Animation.cmbSound.SelectedIndex = 0
            Else
                For i = 1 To FrmEditor_Animation.cmbSound.Items.Count
                    If FrmEditor_Animation.cmbSound.Items(i - 1).ToString = Trim$(.Sound) Then
                        FrmEditor_Animation.cmbSound.SelectedIndex = i - 1
                        Exit For
                    End If
                Next
            End If
            FrmEditor_Animation.txtName.Text = Trim$(.Name)

            FrmEditor_Animation.nudSprite0.Value = .Sprite(0)
            FrmEditor_Animation.nudFrameCount0.Value = .Frames(0)
            FrmEditor_Animation.nudLoopCount0.Value = .LoopCount(0)
            FrmEditor_Animation.nudLoopTime0.Value = .LoopTime(0)

            FrmEditor_Animation.nudSprite1.Value = .Sprite(1)
            FrmEditor_Animation.nudFrameCount1.Value = .Frames(1)
            FrmEditor_Animation.nudLoopCount1.Value = .LoopCount(1)
            FrmEditor_Animation.nudLoopTime1.Value = .LoopTime(1)

            Editorindex = FrmEditor_Animation.lstIndex.SelectedIndex + 1
        End With

        EditorAnim_DrawAnim()
        Animation_Changed(Editorindex) = True
    End Sub

    Friend Sub AnimationEditorOk()
        Dim i As Integer

        For i = 1 To MAX_ANIMATIONS
            If Animation_Changed(i) Then
                SendSaveAnimation(i)
            End If
        Next

        FrmEditor_Animation.Visible = False
        Editor = 0
        ClearChanged_Animation()
    End Sub

    Friend Sub AnimationEditorCancel()
        Editor = 0
        FrmEditor_Animation.Visible = False
        ClearChanged_Animation()
        ClearAnimations()
        SendRequestAnimations()
    End Sub

    Friend Sub ClearChanged_Animation()
        For i = 0 To MAX_ANIMATIONS
            Animation_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Npc Editor"

    Friend Sub NpcEditorInit()
        Dim i As Integer

        If frmEditor_NPC.Visible = False Then Exit Sub
        Editorindex = frmEditor_NPC.lstIndex.SelectedIndex + 1
        frmEditor_NPC.cmbDropSlot.SelectedIndex = 0
        If Npc(Editorindex).AttackSay Is Nothing Then Npc(Editorindex).AttackSay = ""
        If Npc(Editorindex).Name Is Nothing Then Npc(Editorindex).Name = ""

        With frmEditor_NPC
            'populate combo boxes
            .cmbAnimation.Items.Clear()
            .cmbAnimation.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                .cmbAnimation.Items.Add(i & ": " & Animation(i).Name)
            Next

            .cmbQuest.Items.Clear()
            .cmbQuest.Items.Add("None")
            For i = 1 To MaxQuests
                .cmbQuest.Items.Add(i & ": " & Quest(i).Name)
            Next

            .cmbItem.Items.Clear()
            .cmbItem.Items.Add("None")
            For i = 1 To MAX_ITEMS
                .cmbItem.Items.Add(i & ": " & Item(i).Name)
            Next

            .txtName.Text = Trim$(Npc(Editorindex).Name)
            .txtAttackSay.Text = Trim$(Npc(Editorindex).AttackSay)

            If Npc(Editorindex).Sprite < 0 OrElse Npc(Editorindex).Sprite > .nudSprite.Maximum Then Npc(Editorindex).Sprite = 0
            .nudSprite.Value = Npc(Editorindex).Sprite
            .nudSpawnSecs.Value = Npc(Editorindex).SpawnSecs
            .cmbBehaviour.SelectedIndex = Npc(Editorindex).Behaviour
            .cmbFaction.SelectedIndex = Npc(Editorindex).Faction
            .nudRange.Value = Npc(Editorindex).Range
            .nudChance.Value = Npc(Editorindex).DropChance(frmEditor_NPC.cmbDropSlot.SelectedIndex + 1)
            .cmbItem.SelectedIndex = Npc(Editorindex).DropItem(frmEditor_NPC.cmbDropSlot.SelectedIndex + 1)

            .nudAmount.Value = Npc(Editorindex).DropItemValue(frmEditor_NPC.cmbDropSlot.SelectedIndex + 1)

            .nudHp.Value = Npc(Editorindex).Hp
            .nudExp.Value = Npc(Editorindex).Exp
            .nudLevel.Value = Npc(Editorindex).Level
            .nudDamage.Value = Npc(Editorindex).Damage

            .cmbQuest.SelectedIndex = Npc(Editorindex).QuestNum
            .cmbSpawnPeriod.SelectedIndex = Npc(Editorindex).SpawnTime

            .nudStrength.Value = Npc(Editorindex).Stat(StatType.Strength)
            .nudEndurance.Value = Npc(Editorindex).Stat(StatType.Endurance)
            .nudIntelligence.Value = Npc(Editorindex).Stat(StatType.Intelligence)
            .nudSpirit.Value = Npc(Editorindex).Stat(StatType.Spirit)
            .nudLuck.Value = Npc(Editorindex).Stat(StatType.Luck)
            .nudVitality.Value = Npc(Editorindex).Stat(StatType.Vitality)

            .cmbSkill1.Items.Clear()
            .cmbSkill2.Items.Clear()
            .cmbSkill3.Items.Clear()
            .cmbSkill4.Items.Clear()
            .cmbSkill5.Items.Clear()
            .cmbSkill6.Items.Clear()

            .cmbSkill1.Items.Add("None")
            .cmbSkill2.Items.Add("None")
            .cmbSkill3.Items.Add("None")
            .cmbSkill4.Items.Add("None")
            .cmbSkill5.Items.Add("None")
            .cmbSkill6.Items.Add("None")

            For i = 1 To MAX_SKILLS
                If Len(Skill(i).Name) > 0 Then
                    .cmbSkill1.Items.Add(Skill(i).Name)
                    .cmbSkill2.Items.Add(Skill(i).Name)
                    .cmbSkill3.Items.Add(Skill(i).Name)
                    .cmbSkill4.Items.Add(Skill(i).Name)
                    .cmbSkill5.Items.Add(Skill(i).Name)
                    .cmbSkill6.Items.Add(Skill(i).Name)
                End If
            Next

            .cmbSkill1.SelectedIndex = Npc(Editorindex).Skill(1)
            .cmbSkill2.SelectedIndex = Npc(Editorindex).Skill(2)
            .cmbSkill3.SelectedIndex = Npc(Editorindex).Skill(3)
            .cmbSkill4.SelectedIndex = Npc(Editorindex).Skill(4)
            .cmbSkill5.SelectedIndex = Npc(Editorindex).Skill(5)
            .cmbSkill6.SelectedIndex = Npc(Editorindex).Skill(6)
        End With

        EditorNpc_DrawSprite()
        NPC_Changed(Editorindex) = True
    End Sub

    Friend Sub NpcEditorOk()
        Dim i As Integer

        For i = 1 To MAX_NPCS
            If NPC_Changed(i) Then
                SendSaveNpc(i)
            End If
        Next

        frmEditor_NPC.Visible = False
        Editor = 0
        ClearChanged_NPC()
    End Sub

    Friend Sub NpcEditorCancel()
        Editor = 0
        frmEditor_NPC.Visible = False
        ClearChanged_NPC()
        ClearNpcs()
        SendRequestNpcs()
    End Sub

    Friend Sub ClearChanged_NPC()
        For i = 1 To MAX_NPCS
            NPC_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Resource Editor"
    Friend Sub ClearChanged_Resource()
        For i = 1 To MAX_RESOURCES
            Resource_Changed(i) = Nothing
        Next i
        ReDim Resource_Changed(MAX_RESOURCES)
    End Sub

    Friend Sub ResourceEditorInit()
        Dim i As Integer

        If frmEditor_Resource.Visible = False Then Exit Sub
        Editorindex = frmEditor_Resource.lstIndex.SelectedIndex + 1

        With frmEditor_Resource
            'populate combo boxes
            .cmbRewardItem.Items.Clear()
            .cmbRewardItem.Items.Add("None")
            For i = 1 To MAX_ITEMS
                .cmbRewardItem.Items.Add(i & ": " & Item(i).Name)
            Next

            .cmbAnimation.Items.Clear()
            .cmbAnimation.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                .cmbAnimation.Items.Add(i & ": " & Animation(i).Name)
            Next

            .nudExhaustedPic.Maximum = NumResources
            .nudNormalPic.Maximum = NumResources
            .nudRespawn.Maximum = 1000000
            .txtName.Text = Trim$(Resource(Editorindex).Name)
            .txtMessage.Text = Trim$(Resource(Editorindex).SuccessMessage)
            .txtMessage2.Text = Trim$(Resource(Editorindex).EmptyMessage)
            .cmbType.SelectedIndex = Resource(Editorindex).ResourceType
            .nudNormalPic.Value = Resource(Editorindex).ResourceImage
            .nudExhaustedPic.Value = Resource(Editorindex).ExhaustedImage
            .cmbRewardItem.SelectedIndex = Resource(Editorindex).ItemReward
            .nudRewardExp.Value = Resource(Editorindex).ExpReward
            .cmbTool.SelectedIndex = Resource(Editorindex).ToolRequired
            .nudHealth.Value = Resource(Editorindex).Health
            .nudRespawn.Value = Resource(Editorindex).RespawnTime
            .cmbAnimation.SelectedIndex = Resource(Editorindex).Animation
            .nudLvlReq.Value = Resource(Editorindex).LvlRequired
        End With

        frmEditor_Resource.Visible = True

        EditorResource_DrawSprite()

        Resource_Changed(Editorindex) = True
    End Sub

    Friend Sub ResourceEditorOk()
        Dim i As Integer

        For i = 1 To MAX_RESOURCES
            If Resource_Changed(i) Then
                SendSaveResource(i)
            End If
        Next

        frmEditor_Resource.Visible = False
        Editor = 0
        ClearChanged_Resource()
    End Sub

    Friend Sub ResourceEditorCancel()
        Editor = 0
        frmEditor_Resource.Visible = False
        ClearChanged_Resource()
        ClearResources()
        SendRequestResources()
    End Sub

#End Region

#Region "Skill Editor"

    Friend Sub SkillEditorInit()
        Dim i As Integer

        If frmEditor_Skill.Visible = False Then Exit Sub
        Editorindex = frmEditor_Skill.lstIndex.SelectedIndex + 1

        If Skill(Editorindex).Name Is Nothing Then Skill(Editorindex).Name = ""

        With frmEditor_Skill
            ' set max values
            .nudAoE.Maximum = Byte.MaxValue
            .nudRange.Maximum = Byte.MaxValue
            .nudMap.Maximum = MAX_MAPS

            ' build class combo
            .cmbClass.Items.Clear()
            .cmbClass.Items.Add("None")
            For i = 1 To MAX_CLASSES
                .cmbClass.Items.Add(Trim$(Classes(i).Name))
            Next
            .cmbClass.SelectedIndex = 0

            .cmbProjectile.Items.Clear()
            .cmbProjectile.Items.Add("None")
            For i = 1 To MaxProjectiles
                .cmbProjectile.Items.Add(Trim$(Projectiles(i).Name))
            Next
            .cmbProjectile.SelectedIndex = 0

            .cmbAnimCast.Items.Clear()
            .cmbAnimCast.Items.Add("None")
            .cmbAnim.Items.Clear()
            .cmbAnim.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                .cmbAnimCast.Items.Add(Trim$(Animation(i).Name))
                .cmbAnim.Items.Add(Trim$(Animation(i).Name))
            Next
            .cmbAnimCast.SelectedIndex = 0
            .cmbAnim.SelectedIndex = 0

            ' set values
            .txtName.Text = Trim$(Skill(Editorindex).Name)
            .cmbType.SelectedIndex = Skill(Editorindex).Type
            .nudMp.Value = Skill(Editorindex).MpCost
            .nudLevel.Value = Skill(Editorindex).LevelReq
            .cmbAccessReq.SelectedIndex = Skill(Editorindex).AccessReq
            .cmbClass.SelectedIndex = Skill(Editorindex).ClassReq
            .nudCast.Value = Skill(Editorindex).CastTime
            .nudCool.Value = Skill(Editorindex).CdTime
            .nudIcon.Value = Skill(Editorindex).Icon
            .nudMap.Value = Skill(Editorindex).Map
            .nudX.Value = Skill(Editorindex).X
            .nudY.Value = Skill(Editorindex).Y
            .cmbDir.SelectedIndex = Skill(Editorindex).Dir
            .nudVital.Value = Skill(Editorindex).Vital
            .nudDuration.Value = Skill(Editorindex).Duration
            .nudInterval.Value = Skill(Editorindex).Interval
            .nudRange.Value = Skill(Editorindex).Range

            .chkAoE.Checked = Skill(Editorindex).IsAoE

            .nudAoE.Value = Skill(Editorindex).AoE
            .cmbAnimCast.SelectedIndex = Skill(Editorindex).CastAnim
            .cmbAnim.SelectedIndex = Skill(Editorindex).SkillAnim
            .nudStun.Value = Skill(Editorindex).StunDuration

            If Skill(Editorindex).IsProjectile = 1 Then
                .chkProjectile.Checked = True
            Else
                .chkProjectile.Checked = False
            End If
            .cmbProjectile.SelectedIndex = Skill(Editorindex).Projectile

            If Skill(Editorindex).KnockBack = 1 Then
                .chkKnockBack.Checked = True
            Else
                .chkKnockBack.Checked = False
            End If
            .cmbKnockBackTiles.SelectedIndex = Skill(Editorindex).KnockBackTiles
        End With

        EditorSkill_BltIcon()

        Skill_Changed(Editorindex) = True
    End Sub

    Friend Sub SkillEditorOk()
        Dim i As Integer

        For i = 1 To MAX_SKILLS
            If Skill_Changed(i) Then
                SendSaveSkill(i)
            End If
        Next

        frmEditor_Skill.Visible = False
        Editor = 0
        ClearChanged_Skill()
    End Sub

    Friend Sub SkillEditorCancel()
        Editor = 0
        frmEditor_Skill.Visible = False
        ClearChanged_Skill()
        ClearSkills()
        SendRequestSkills()
    End Sub

    Friend Sub ClearChanged_Skill()
        For i = 1 To MAX_SKILLS
            Skill_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Shop editor"

    Friend Sub ShopEditorInit()
        Dim i As Integer

        If frmEditor_Shop.Visible = False Then Exit Sub
        Editorindex = frmEditor_Shop.lstIndex.SelectedIndex + 1

        frmEditor_Shop.txtName.Text = Trim$(Shop(Editorindex).Name)
        If Shop(Editorindex).BuyRate > 0 Then
            frmEditor_Shop.nudBuy.Value = Shop(Editorindex).BuyRate
        Else
            frmEditor_Shop.nudBuy.Value = 100
        End If

        frmEditor_Shop.nudFace.Value = Shop(Editorindex).Face
        If File.Exists(Path.Graphics & "Faces\" & Shop(Editorindex).Face & GfxExt) Then
            frmEditor_Shop.picFace.BackgroundImage = Image.FromFile(Path.Graphics & "Faces\" & Shop(Editorindex).Face & GfxExt)
        End If

        frmEditor_Shop.cmbItem.Items.Clear()
        frmEditor_Shop.cmbItem.Items.Add("None")
        frmEditor_Shop.cmbCostItem.Items.Clear()
        frmEditor_Shop.cmbCostItem.Items.Add("None")

        For i = 1 To MAX_ITEMS
            frmEditor_Shop.cmbItem.Items.Add(i & ": " & Trim$(Item(i).Name))
            frmEditor_Shop.cmbCostItem.Items.Add(i & ": " & Trim$(Item(i).Name))
        Next

        frmEditor_Shop.cmbItem.SelectedIndex = 0
        frmEditor_Shop.cmbCostItem.SelectedIndex = 0

        UpdateShopTrade()

        Shop_Changed(Editorindex) = True
    End Sub

    Friend Sub UpdateShopTrade()
        Dim i As Integer
        frmEditor_Shop.lstTradeItem.Items.Clear()

        For i = 1 To MAX_TRADES
            With Shop(Editorindex).TradeItem(i)
                ' if none, show as none
                If .Item = 0 AndAlso .CostItem = 0 Then
                    frmEditor_Shop.lstTradeItem.Items.Add("Empty Trade Slot")
                Else
                    frmEditor_Shop.lstTradeItem.Items.Add(i & ": " & .ItemValue & "x " & Trim$(Item(.Item).Name) & " for " & .CostValue & "x " & Trim$(Item(.CostItem).Name))
                End If
            End With
        Next

        frmEditor_Shop.lstTradeItem.SelectedIndex = 0
    End Sub

    Friend Sub ShopEditorOk()
        Dim i As Integer

        For i = 1 To MAX_SHOPS
            If Shop_Changed(i) Then
                SendSaveShop(i)
            End If
        Next

        frmEditor_Shop.Visible = False
        Editor = 0
        ClearChanged_Shop()
    End Sub

    Friend Sub ShopEditorCancel()
        Editor = 0
        frmEditor_Shop.Visible = False
        ClearChanged_Shop()
        ClearShops()
        SendRequestShops()
    End Sub

    Friend Sub ClearChanged_Shop()
        For i = 1 To MAX_SHOPS
            Shop_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Class Editor"

    Friend Sub ClassesEditorOk()
        SendSaveClasses()

        frmEditor_Classes.Visible = False
        Editor = 0
    End Sub

    Friend Sub ClassesEditorCancel()
        SendRequestClasses()
        frmEditor_Classes.Visible = False
        Editor = 0
    End Sub

    Friend Sub ClassEditorInit()
        Dim i As Integer

        frmEditor_Classes.lstIndex.Items.Clear()

        For i = 1 To MAX_CLASSES
            frmEditor_Classes.lstIndex.Items.Add(Trim(Classes(i).Name))
        Next

        Editor = EDITOR_CLASSES

        frmEditor_Classes.nudMaleSprite.Maximum = NumCharacters
        frmEditor_Classes.nudFemaleSprite.Maximum = NumCharacters

        frmEditor_Classes.cmbItems.Items.Clear()

        frmEditor_Classes.cmbItems.Items.Add("None")
        For i = 1 To MAX_ITEMS
            frmEditor_Classes.cmbItems.Items.Add(Trim(Item(i).Name))
        Next

        frmEditor_Classes.lstIndex.SelectedIndex = 0

        frmEditor_Classes.Visible = True
    End Sub

    Friend Sub LoadClass()
        Dim i As Integer

        If Editorindex <= 0 OrElse Editorindex > MAX_CLASSES Then Exit Sub

        frmEditor_Classes.txtName.Text = Classes(Editorindex).Name
        frmEditor_Classes.txtDescription.Text = Classes(Editorindex).Desc

        frmEditor_Classes.cmbMaleSprite.Items.Clear()

        For i = 0 To UBound(Classes(Editorindex).MaleSprite)
            frmEditor_Classes.cmbMaleSprite.Items.Add("Sprite " & i + 1)
        Next

        frmEditor_Classes.cmbFemaleSprite.Items.Clear()

        For i = 0 To UBound(Classes(Editorindex).FemaleSprite)
            frmEditor_Classes.cmbFemaleSprite.Items.Add("Sprite " & i + 1)
        Next

        frmEditor_Classes.nudMaleSprite.Value = Classes(Editorindex).MaleSprite(0)
        frmEditor_Classes.nudFemaleSprite.Value = Classes(Editorindex).FemaleSprite(0)

        frmEditor_Classes.cmbMaleSprite.SelectedIndex = 0
        frmEditor_Classes.cmbFemaleSprite.SelectedIndex = 0

        frmEditor_Classes.DrawPreview()

        For i = 1 To StatType.Count - 1
            If Classes(Editorindex).Stat(i) = 0 Then Classes(Editorindex).Stat(i) = 1
        Next

        frmEditor_Classes.nudStrength.Value = Classes(Editorindex).Stat(StatType.Strength)
        frmEditor_Classes.nudLuck.Value = Classes(Editorindex).Stat(StatType.Luck)
        frmEditor_Classes.nudEndurance.Value = Classes(Editorindex).Stat(StatType.Endurance)
        frmEditor_Classes.nudIntelligence.Value = Classes(Editorindex).Stat(StatType.Intelligence)
        frmEditor_Classes.nudVitality.Value = Classes(Editorindex).Stat(StatType.Vitality)
        frmEditor_Classes.nudSpirit.Value = Classes(Editorindex).Stat(StatType.Spirit)

        If Classes(Editorindex).BaseExp < 10 Then
            frmEditor_Classes.nudBaseExp.Value = 10
        Else
            frmEditor_Classes.nudBaseExp.Value = Classes(Editorindex).BaseExp
        End If

        frmEditor_Classes.lstStartItems.Items.Clear()

        For i = 1 To 5
            If Classes(Editorindex).StartItem(i) > 0 Then
                frmEditor_Classes.lstStartItems.Items.Add(Item(Classes(Editorindex).StartItem(i)).Name & " X " & Classes(Editorindex).StartValue(i))
            Else
                frmEditor_Classes.lstStartItems.Items.Add("None")
            End If
        Next

        frmEditor_Classes.nudStartMap.Value = Classes(Editorindex).StartMap
        frmEditor_Classes.nudStartX.Value = Classes(Editorindex).StartX
        frmEditor_Classes.nudStartY.Value = Classes(Editorindex).StartY
    End Sub

#End Region

#Region "Item"

    Friend Sub ItemEditorPreInit()
        Dim i As Integer

        With frmEditor_Item
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

        If frmEditor_Item.Visible = False Then Exit Sub
        Editorindex = frmEditor_Item.lstIndex.SelectedIndex + 1

        With Item(Editorindex)
            'populate combo boxes
            frmEditor_Item.cmbAnimation.Items.Clear()
            frmEditor_Item.cmbAnimation.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                frmEditor_Item.cmbAnimation.Items.Add(i & ": " & Animation(i).Name)
            Next

            frmEditor_Item.cmbAmmo.Items.Clear()
            frmEditor_Item.cmbAmmo.Items.Add("None")
            For i = 1 To MAX_ITEMS
                frmEditor_Item.cmbAmmo.Items.Add(i & ": " & Item(i).Name)
            Next

            frmEditor_Item.cmbProjectile.Items.Clear()
            frmEditor_Item.cmbProjectile.Items.Add("None")
            For i = 1 To MaxProjectiles
                frmEditor_Item.cmbProjectile.Items.Add(i & ": " & Projectiles(i).Name)
            Next

            frmEditor_Item.cmbSkills.Items.Clear()
            frmEditor_Item.cmbSkills.Items.Add("None")
            For i = 1 To MAX_SKILLS
                frmEditor_Item.cmbSkills.Items.Add(i & ": " & Skill(i).Name)
            Next

            frmEditor_Item.cmbPet.Items.Clear()
            frmEditor_Item.cmbPet.Items.Add("None")
            For i = 1 To MAX_PETS
                frmEditor_Item.cmbPet.Items.Add(i & ": " & Pet(i).Name)
            Next

            frmEditor_Item.cmbRecipe.Items.Clear()
            frmEditor_Item.cmbRecipe.Items.Add("None")
            For i = 1 To MAX_RECIPE
                frmEditor_Item.cmbRecipe.Items.Add(i & ": " & Recipe(i).Name)
            Next

            frmEditor_Item.txtName.Text = Trim$(.Name)
            frmEditor_Item.txtDescription.Text = Trim$(.Description)

            If .Pic > frmEditor_Item.nudPic.Maximum Then .Pic = 0
            frmEditor_Item.nudPic.Value = .Pic
            If .Type > ItemType.Count - 1 Then .Type = 0
            frmEditor_Item.cmbType.SelectedIndex = .Type
            frmEditor_Item.cmbAnimation.SelectedIndex = .Animation

            If .ItemLevel = 0 Then .ItemLevel = 1
            frmEditor_Item.nudItemLvl.Value = .ItemLevel

            ' Type specific settings
            If (frmEditor_Item.cmbType.SelectedIndex = ItemType.Equipment) Then
                frmEditor_Item.fraEquipment.Visible = True
                frmEditor_Item.cmbProjectile.SelectedIndex = .Data1
                frmEditor_Item.nudDamage.Value = .Data2
                frmEditor_Item.cmbTool.SelectedIndex = .Data3

                frmEditor_Item.cmbSubType.SelectedIndex = .SubType

                If .Speed < 100 Then .Speed = 100
                If .Speed > frmEditor_Item.nudSpeed.Maximum Then .Speed = frmEditor_Item.nudSpeed.Maximum
                frmEditor_Item.nudSpeed.Value = .Speed

                frmEditor_Item.nudStrength.Value = .Add_Stat(StatType.Strength)
                frmEditor_Item.nudEndurance.Value = .Add_Stat(StatType.Endurance)
                frmEditor_Item.nudIntelligence.Value = .Add_Stat(StatType.Intelligence)
                frmEditor_Item.nudVitality.Value = .Add_Stat(StatType.Vitality)
                frmEditor_Item.nudLuck.Value = .Add_Stat(StatType.Luck)
                frmEditor_Item.nudSpirit.Value = .Add_Stat(StatType.Spirit)

                If .KnockBack = 1 Then
                    frmEditor_Item.chkKnockBack.Checked = True
                Else
                    frmEditor_Item.chkKnockBack.Checked = False
                End If
                frmEditor_Item.cmbKnockBackTiles.SelectedIndex = .KnockBackTiles

                If .Randomize = 1 Then
                    frmEditor_Item.chkRandomize.Checked = True
                Else
                    frmEditor_Item.chkRandomize.Checked = False
                End If

                'If .RandomMin = 0 Then .RandomMin = 1
                'frmEditor_Item.numMin.Value = .RandomMin

                'If .RandomMax <= 1 Then .RandomMax = 2
                'frmEditor_Item.numMax.Value = .RandomMax

                frmEditor_Item.nudPaperdoll.Value = .Paperdoll

                frmEditor_Item.cmbProjectile.SelectedIndex = .Projectile
                frmEditor_Item.cmbAmmo.SelectedIndex = .Ammo
            Else
                frmEditor_Item.fraEquipment.Visible = False
            End If

            If (frmEditor_Item.cmbType.SelectedIndex = ItemType.Consumable) Then
                frmEditor_Item.fraVitals.Visible = True
                frmEditor_Item.nudVitalMod.Value = .Data1
            Else
                frmEditor_Item.fraVitals.Visible = False
            End If

            If (frmEditor_Item.cmbType.SelectedIndex = ItemType.Skill) Then
                frmEditor_Item.fraSkill.Visible = True
                frmEditor_Item.cmbSkills.SelectedIndex = .Data1
            Else
                frmEditor_Item.fraSkill.Visible = False
            End If

            If frmEditor_Item.cmbType.SelectedIndex = ItemType.Furniture Then
                frmEditor_Item.fraFurniture.Visible = True
                If Item(Editorindex).Data2 > 0 AndAlso Item(Editorindex).Data2 <= NumFurniture Then
                    frmEditor_Item.nudFurniture.Value = Item(Editorindex).Data2
                Else
                    frmEditor_Item.nudFurniture.Value = 1
                End If
                frmEditor_Item.cmbFurnitureType.SelectedIndex = Item(Editorindex).Data1
            Else
                frmEditor_Item.fraFurniture.Visible = False
            End If

            If (frmEditor_Item.cmbType.SelectedIndex = ItemType.Pet) Then
                frmEditor_Item.fraPet.Visible = True
                frmEditor_Item.cmbPet.SelectedIndex = .Data1
            Else
                frmEditor_Item.fraPet.Visible = False
            End If

            ' Basic requirements
            frmEditor_Item.cmbAccessReq.SelectedIndex = .AccessReq
            frmEditor_Item.nudLevelReq.Value = .LevelReq

            frmEditor_Item.nudStrReq.Value = .Stat_Req(StatType.Strength)
            frmEditor_Item.nudVitReq.Value = .Stat_Req(StatType.Vitality)
            frmEditor_Item.nudLuckReq.Value = .Stat_Req(StatType.Luck)
            frmEditor_Item.nudEndReq.Value = .Stat_Req(StatType.Endurance)
            frmEditor_Item.nudIntReq.Value = .Stat_Req(StatType.Intelligence)
            frmEditor_Item.nudSprReq.Value = .Stat_Req(StatType.Spirit)

            ' Build cmbClassReq
            frmEditor_Item.cmbClassReq.Items.Clear()
            frmEditor_Item.cmbClassReq.Items.Add("None")

            For i = 1 To MAX_CLASSES
                frmEditor_Item.cmbClassReq.Items.Add(Classes(i).Name)
            Next

            frmEditor_Item.cmbClassReq.SelectedIndex = .ClassReq
            ' Info
            frmEditor_Item.nudPrice.Value = .Price
            frmEditor_Item.cmbBind.SelectedIndex = .BindType
            frmEditor_Item.nudRarity.Value = .Rarity

            If .Stackable = 1 Then
                frmEditor_Item.chkStackable.Checked = True
            Else
                frmEditor_Item.chkStackable.Checked = False
            End If

            Editorindex = frmEditor_Item.lstIndex.SelectedIndex + 1
        End With

        frmEditor_Item.nudPic.Maximum = NumItems

        If NumPaperdolls > 0 Then
            frmEditor_Item.nudPaperdoll.Maximum = NumPaperdolls + 1
        End If

        EditorItem_DrawItem()
        EditorItem_DrawPaperdoll()
        EditorItem_DrawFurniture()
        Item_Changed(Editorindex) = True

    End Sub

    Friend Sub ItemEditorCancel()
        Editor = 0
        frmEditor_Item.Visible = False
        ClearChangedItem()
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

        frmEditor_Item.Visible = False
        Editor = 0
        ClearChangedItem()
    End Sub

#End Region
End Module