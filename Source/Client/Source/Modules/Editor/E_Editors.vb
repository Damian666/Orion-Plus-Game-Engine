Imports System
Imports System.Drawing
Imports System.IO

Module E_Editors

#Region "Animation Editor"
    Friend Sub AnimationEditorInit()

        If FrmAnimation.Visible = False Then Exit Sub

        Editorindex = FrmAnimation.lstIndex.SelectedIndex + 1

        With Animation(Editorindex)

            ' find the music we have set
            FrmAnimation.cmbSound.Items.Clear()
            FrmAnimation.cmbSound.Items.Add("None")

            If SoundCache.GetUpperBound(0) > 0 Then
                For i = 1 To SoundCache.GetUpperBound(0)
                    FrmAnimation.cmbSound.Items.Add(SoundCache(i))
                Next
            End If

            If Animation(Editorindex).Sound.Trim = "None" OrElse Animation(Editorindex).Sound.Trim = "" Then
                FrmAnimation.cmbSound.SelectedIndex = 0
            Else
                For i = 1 To FrmAnimation.cmbSound.Items.Count
                    If FrmAnimation.cmbSound.Items(i - 1).ToString = .Sound.Trim Then
                        FrmAnimation.cmbSound.SelectedIndex = i - 1
                        Exit For
                    End If
                Next
            End If
            FrmAnimation.txtName.Text = .Name.Trim

            FrmAnimation.nudSprite0.Value = .Sprite(0)
            FrmAnimation.nudFrameCount0.Value = .Frames(0)
            FrmAnimation.nudLoopCount0.Value = .LoopCount(0)
            FrmAnimation.nudLoopTime0.Value = .LoopTime(0)

            FrmAnimation.nudSprite1.Value = .Sprite(1)
            FrmAnimation.nudFrameCount1.Value = .Frames(1)
            FrmAnimation.nudLoopCount1.Value = .LoopCount(1)
            FrmAnimation.nudLoopTime1.Value = .LoopTime(1)

            Editorindex = FrmAnimation.lstIndex.SelectedIndex + 1
        End With

        EditorAnim_DrawAnim()
        Animation_Changed(Editorindex) = True
    End Sub

    Friend Sub AnimationEditorOk()
        Dim i As Integer

        For i = 1 To MAX_ANIMATIONS
            If Animation_Changed(i) Then
                Network.SendSaveAnimation(i)
            End If
        Next

        FrmAnimation.Visible = False
        Editor = 0
        ClearChanged_Animation()
    End Sub

    Friend Sub AnimationEditorCancel()
        Editor = 0
        FrmAnimation.Visible = False
        ClearChanged_Animation()
        ClearAnimations()
        Network.SendRequestAnimations()
    End Sub

    Friend Sub ClearChanged_Animation()
        For i = 0 To MAX_ANIMATIONS
            Animation_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Item Editor"

#End Region

#Region "Npc Editor"
    Friend Sub NpcEditorInit()
        Dim i As Integer

        If frmNPC.Visible = False Then Exit Sub
        Editorindex = frmNPC.lstIndex.SelectedIndex + 1
        frmNPC.cmbDropSlot.SelectedIndex = 0
        If Npc(Editorindex).AttackSay Is Nothing Then Npc(Editorindex).AttackSay = ""
        If Npc(Editorindex).Name Is Nothing Then Npc(Editorindex).Name = ""

        With frmNPC
            'populate combo boxes
            .cmbAnimation.Items.Clear()
            .cmbAnimation.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                .cmbAnimation.Items.Add(i & ": " & Animation(i).Name)
            Next

            .cmbQuest.Items.Clear()
            .cmbQuest.Items.Add("None")
            For i = 1 To MAXQUESTS
                .cmbQuest.Items.Add(i & ": " & Quest(i).Name)
            Next

            .cmbItem.Items.Clear()
            .cmbItem.Items.Add("None")
            For i = 1 To MAX_ITEMS
                .cmbItem.Items.Add(i & ": " & Item(i).Name)
            Next

            .txtName.Text = Npc(Editorindex).Name.Trim
            .txtAttackSay.Text = Npc(Editorindex).AttackSay.Trim

            If Npc(Editorindex).Sprite < 0 OrElse Npc(Editorindex).Sprite > .nudSprite.Maximum Then Npc(Editorindex).Sprite = 0
            .nudSprite.Value = Npc(Editorindex).Sprite
            .nudSpawnSecs.Value = Npc(Editorindex).SpawnSecs
            .cmbBehaviour.SelectedIndex = Npc(Editorindex).Behaviour
            .cmbFaction.SelectedIndex = Npc(Editorindex).Faction
            .nudRange.Value = Npc(Editorindex).Range
            .nudChance.Value = Npc(Editorindex).DropChance(frmNPC.cmbDropSlot.SelectedIndex + 1)
            .cmbItem.SelectedIndex = Npc(Editorindex).DropItem(frmNPC.cmbDropSlot.SelectedIndex + 1)

            .nudAmount.Value = Npc(Editorindex).DropItemValue(frmNPC.cmbDropSlot.SelectedIndex + 1)

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
                If Skill(i).Name.Length > 0 Then
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
                Network.SendSaveNpc(i)
            End If
        Next

        frmNPC.Visible = False
        Editor = 0
        ClearChanged_NPC()
    End Sub

    Friend Sub NpcEditorCancel()
        Editor = 0
        frmNPC.Visible = False
        ClearChanged_NPC()
        ClearNpcs()
        Network.SendRequestNPCS()
    End Sub

    Friend Sub ClearChanged_NPC()
        For i = 1 To MAX_NPCS
            NPC_Changed(i) = False
        Next
    End Sub
#End Region

#Region "Resource Editor"
    Friend Sub ResourceEditorInit()
        Dim i As Integer

        If frmResource.Visible = False Then Exit Sub
        Editorindex = frmResource.lstIndex.SelectedIndex + 1

        With frmResource
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
            .txtName.Text = Resource(Editorindex).Name.Trim
            .txtMessage.Text = Resource(Editorindex).SuccessMessage.Trim
            .txtMessage2.Text = Resource(Editorindex).EmptyMessage.Trim
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


        frmResource.Visible = True

        EditorResource_DrawSprite()

        Resource_Changed(Editorindex) = True
    End Sub

    Friend Sub ResourceEditorOk()
        Dim i As Integer

        For i = 1 To MAX_RESOURCES
            If Resource_Changed(i) Then
                Network.SendSaveResource(i)
            End If
        Next

        frmResource.Visible = False
        Editor = 0
        'ClearChanged_Resource()
    End Sub

    Friend Sub ResourceEditorCancel()
        Editor = 0
        frmResource.Visible = False
        'ClearChanged_Resource()
        ClearResources()
        SendRequestResources()
    End Sub
#End Region

#Region "Skill Editor"
    Friend Sub SkillEditorInit()
        Dim i As Integer

        If frmSkill.Visible = False Then Exit Sub
        Editorindex = frmSkill.lstIndex.SelectedIndex + 1

        If Skill(Editorindex).Name Is Nothing Then Skill(Editorindex).Name = ""

        With frmSkill
            ' set max values
            .nudAoE.Maximum = Byte.MaxValue
            .nudRange.Maximum = Byte.MaxValue
            .nudMap.Maximum = MAX_MAPS

            ' build class combo
            .cmbClass.Items.Clear()
            .cmbClass.Items.Add("None")
            For i = 1 To MaxClasses
                .cmbClass.Items.Add(Classes(i).Name.Trim)
            Next
            .cmbClass.SelectedIndex = 0

            .cmbProjectile.Items.Clear()
            .cmbProjectile.Items.Add("None")
            For i = 1 To MaxProjectiles
                .cmbProjectile.Items.Add(Projectiles(i).Name.Trim)
            Next
            .cmbProjectile.SelectedIndex = 0

            .cmbAnimCast.Items.Clear()
            .cmbAnimCast.Items.Add("None")
            .cmbAnim.Items.Clear()
            .cmbAnim.Items.Add("None")
            For i = 1 To MAX_ANIMATIONS
                .cmbAnimCast.Items.Add(Animation(i).Name.Trim)
                .cmbAnim.Items.Add(Animation(i).Name.Trim)
            Next
            .cmbAnimCast.SelectedIndex = 0
            .cmbAnim.SelectedIndex = 0

            ' set values
            .txtName.Text = Skill(Editorindex).Name.Trim
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
                Network.SendSaveSkill(i)
            End If
        Next

        frmSkill.Visible = False
        Editor = 0
        ClearChanged_Skill()
    End Sub

    Friend Sub SkillEditorCancel()
        Editor = 0
        frmSkill.Visible = False
        ClearChanged_Skill()
        ClearSkills()
        Network.SendRequestSkills()
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

        If frmShop.Visible = False Then Exit Sub
        Editorindex = frmShop.lstIndex.SelectedIndex + 1

        frmShop.txtName.Text = Shop(Editorindex).Name.Trim
        If Shop(Editorindex).BuyRate > 0 Then
            frmShop.nudBuy.Value = Shop(Editorindex).BuyRate
        Else
            frmShop.nudBuy.Value = 100
        End If

        frmShop.nudFace.Value = Shop(Editorindex).Face
        If File.Exists(Environment.CurrentDirectory & GfxPath & "Faces\" & Shop(Editorindex).Face & GfxExt) Then
            frmShop.picFace.BackgroundImage = Image.FromFile(Environment.CurrentDirectory & GfxPath & "Faces\" & Shop(Editorindex).Face & GfxExt)
        End If

        frmShop.cmbItem.Items.Clear()
        frmShop.cmbItem.Items.Add("None")
        frmShop.cmbCostItem.Items.Clear()
        frmShop.cmbCostItem.Items.Add("None")

        For i = 1 To MAX_ITEMS
            frmShop.cmbItem.Items.Add(i & ": " & Item(i).Name.Trim)
            frmShop.cmbCostItem.Items.Add(i & ": " & Item(i).Name.Trim)
        Next

        frmShop.cmbItem.SelectedIndex = 0
        frmShop.cmbCostItem.SelectedIndex = 0

        UpdateShopTrade()

        Shop_Changed(Editorindex) = True
    End Sub

    Friend Sub UpdateShopTrade()
        Dim i As Integer
        frmShop.lstTradeItem.Items.Clear()

        For i = 1 To MAX_TRADES
            With Shop(Editorindex).TradeItem(i)
                ' if none, show as none
                If .Item = 0 AndAlso .CostItem = 0 Then
                    frmShop.lstTradeItem.Items.Add("Empty Trade Slot")
                Else
                    frmShop.lstTradeItem.Items.Add(i & ": " & .ItemValue & "x " & Item(.Item).Name.Trim & " for " & .CostValue & "x " & Item(.CostItem).Name.Trim)
                End If
            End With
        Next

        frmShop.lstTradeItem.SelectedIndex = 0
    End Sub

    Friend Sub ShopEditorOk()
        Dim i As Integer

        For i = 1 To MAX_SHOPS
            If Shop_Changed(i) Then
                Network.SendSaveShop(i)
            End If
        Next

        frmShop.Visible = False
        Editor = 0
        ClearChanged_Shop()
    End Sub

    Friend Sub ShopEditorCancel()
        Editor = 0
        frmShop.Visible = False
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
        Network.SendSaveClasses()

        frmClasses.Visible = False
        Editor = 0
    End Sub

    Friend Sub ClassesEditorCancel()
        Network.SendRequestClasses()
        frmClasses.Visible = False
        Editor = 0
    End Sub

    Friend Sub ClassEditorInit()
        Dim i As Integer

        frmClasses.lstIndex.Items.Clear()

        For i = 1 To MaxClasses
            frmClasses.lstIndex.Items.Add(Classes(i).Name.Trim)
        Next

        Editor = EDITOR_CLASSES

        frmClasses.nudMaleSprite.Maximum = NumCharacters
        frmClasses.nudFemaleSprite.Maximum = NumCharacters

        frmClasses.cmbItems.Items.Clear()

        frmClasses.cmbItems.Items.Add("None")
        For i = 1 To MAX_ITEMS
            frmClasses.cmbItems.Items.Add(Item(i).Name.Trim)
        Next

        frmClasses.lstIndex.SelectedIndex = 0

        frmClasses.Visible = True
    End Sub

    Friend Sub LoadClass()
        Dim i As Integer

        If Editorindex <= 0 OrElse Editorindex > MaxClasses Then Exit Sub

        frmClasses.txtName.Text = Classes(Editorindex).Name
        frmClasses.txtDescription.Text = Classes(Editorindex).Desc

        frmClasses.cmbMaleSprite.Items.Clear()

        For i = 0 To Classes(Editorindex).MaleSprite.GetUpperBound(0)
            frmClasses.cmbMaleSprite.Items.Add("Sprite " & i + 1)
        Next

        frmClasses.cmbFemaleSprite.Items.Clear()

        For i = 0 To Classes(Editorindex).FemaleSprite.GetUpperBound(0)
            frmClasses.cmbFemaleSprite.Items.Add("Sprite " & i + 1)
        Next

        frmClasses.nudMaleSprite.Value = Classes(Editorindex).MaleSprite(0)
        frmClasses.nudFemaleSprite.Value = Classes(Editorindex).FemaleSprite(0)

        frmClasses.cmbMaleSprite.SelectedIndex = 0
        frmClasses.cmbFemaleSprite.SelectedIndex = 0

        frmClasses.DrawPreview()

        For i = 1 To StatType.Count - 1
            If Classes(Editorindex).Stat(i) = 0 Then Classes(Editorindex).Stat(i) = 1
        Next

        frmClasses.nudStrength.Value = Classes(Editorindex).Stat(StatType.Strength)
        frmClasses.nudLuck.Value = Classes(Editorindex).Stat(StatType.Luck)
        frmClasses.nudEndurance.Value = Classes(Editorindex).Stat(StatType.Endurance)
        frmClasses.nudIntelligence.Value = Classes(Editorindex).Stat(StatType.Intelligence)
        frmClasses.nudVitality.Value = Classes(Editorindex).Stat(StatType.Vitality)
        frmClasses.nudSpirit.Value = Classes(Editorindex).Stat(StatType.Spirit)

        If Classes(Editorindex).BaseExp < 10 Then
            frmClasses.nudBaseExp.Value = 10
        Else
            frmClasses.nudBaseExp.Value = Classes(Editorindex).BaseExp
        End If

        frmClasses.lstStartItems.Items.Clear()

        For i = 1 To 5
            If Classes(Editorindex).StartItem(i) > 0 Then
                frmClasses.lstStartItems.Items.Add(Item(Classes(Editorindex).StartItem(i)).Name & " X " & Classes(Editorindex).StartValue(i))
            Else
                frmClasses.lstStartItems.Items.Add("None")
            End If
        Next

        frmClasses.nudStartMap.Value = Classes(Editorindex).StartMap
        frmClasses.nudStartX.Value = Classes(Editorindex).StartX
        frmClasses.nudStartY.Value = Classes(Editorindex).StartY
    End Sub

#End Region

End Module









































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































