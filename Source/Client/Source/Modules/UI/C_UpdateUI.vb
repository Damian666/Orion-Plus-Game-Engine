Module C_UpdateUI

#Region "Defines"

    Friend GameDestroyed As Boolean
    Friend ReloadFrmMain As Boolean
    Friend PnlRegisterVisible As Boolean
    Friend PnlCharCreateVisible As Boolean
    Friend PnlLoginVisible As Boolean
    Friend PnlCreditsVisible As Boolean
    Friend Frmmenuvisible As Boolean
    Friend Frmmaingamevisible As Boolean
    Friend Pnlloadvisible As Boolean
    Friend Lblnextcharleft As Integer
    Friend Cmbclass() As String
    Friend TxtChatAdd As String
    Friend ChkSavePassChecked As Boolean
    Friend TempUserName As String
    Friend TempPassword As String
    Friend PnlCharSelectVisible As Boolean
    Friend DrawCharSelect As Boolean

    'Mapreport
    Friend UpdateMapnames As Boolean

    Friend Adminvisible As Boolean

    'GUI drawing
    Friend HudVisible As Boolean = True

    Friend PnlCharacterVisible As Boolean
    Friend PnlInventoryVisible As Boolean
    Friend PnlSkillsVisible As Boolean
    Friend PnlBankVisible As Boolean
    Friend PnlShopVisible As Boolean
    Friend PnlTradeVisible As Boolean
    Friend PnlEventChatVisible As Boolean
    Friend PnlRClickVisible As Boolean
    Friend OptionsVisible As Boolean

    Friend VbKeyRight As Boolean
    Friend VbKeyLeft As Boolean
    Friend VbKeyUp As Boolean
    Friend VbKeyDown As Boolean
    Friend VbKeyShift As Boolean
    Friend VbKeyControl As Boolean
    Friend VbKeyAlt As Boolean

    Friend PicHpWidth As Integer
    Friend PicManaWidth As Integer
    Friend PicExpWidth As Integer

    Friend LblHpText As String
    Friend LblManaText As String
    Friend LblExpText As String

    Friend UpdateCharacterPanel As Boolean

    Friend NeedToOpenShop As Boolean
    Friend NeedToOpenShopNum As Integer
    Friend NeedToOpenBank As Boolean
    Friend NeedToOpenTrade As Boolean
    Friend NeedtoCloseTrade As Boolean
    Friend NeedtoUpdateTrade As Boolean

    Friend InitMapProperties As Boolean

    Friend Tradername As String

    'UI Panels Coordinates
    Friend HudWindowX As Integer = 0

    Friend HudWindowY As Integer = 0
    Friend HudFaceX As Integer = 4
    Friend HudFaceY As Integer = 4

    'bars
    Friend HudhpBarX As Integer = 110

    Friend HudhpBarY As Integer = 10
    Friend HudmpBarX As Integer = 110
    Friend HudmpBarY As Integer = 30
    Friend HudexpBarX As Integer = 110
    Friend HudexpBarY As Integer = 50

    'Set the Chat Position

    Friend MyChatX As Integer = 1
    Friend MyChatY As Integer = FrmGame.Height - 55

    Friend ChatWindowX As Integer = 1
    Friend ChatWindowY As Integer = 705

    Friend ShowItemDesc As Boolean
    Friend ItemDescItemNum As Integer
    Friend ItemDescName As String
    Friend ItemDescDescription As String
    Friend ItemDescValue As Integer
    Friend ItemDescInfo As String
    Friend ItemDescType As String
    Friend ItemDescCost As String
    Friend ItemDescLevel As String
    Friend ItemDescSpeed As String
    Friend ItemDescStr As String
    Friend ItemDescEnd As String
    Friend ItemDescInt As String
    Friend ItemDescSpr As String
    Friend ItemDescVit As String
    Friend ItemDescLuck As String
    Friend ItemDescRarityColor As SFML.Graphics.Color
    Friend ItemDescRarityBackColor As SFML.Graphics.Color

    'Action Panel Coordinates
    Friend ActionPanelX As Integer = 942

    Friend ActionPanelY As Integer = 755

    Friend InvBtnX As Integer = 16
    Friend InvBtnY As Integer = 16
    Friend SkillBtnX As Integer = 80
    Friend SkillBtnY As Integer = 16
    Friend CharBtnX As Integer = 144
    Friend CharBtnY As Integer = 16

    Friend QuestBtnX As Integer = 25
    Friend QuestBtnY As Integer = 64
    Friend OptBtnX As Integer = 88
    Friend OptBtnY As Integer = 64
    Friend ExitBtnX As Integer = 144
    Friend ExitBtnY As Integer = 64

    'Character window Coordinates
    Friend CharWindowX As Integer = 943

    Friend CharWindowY As Integer = 475
    Friend Const EqTop As Byte = 85
    Friend Const EqLeft As Byte = 8
    Friend Const EqOffsetX As Byte = 125
    Friend Const EqOffsetY As Byte = 5
    Friend Const EqColumns As Byte = 2

    Friend StrengthUpgradeX As Integer = 370
    Friend StrengthUpgradeY As Integer = 33
    Friend EnduranceUpgradeX As Integer = 370
    Friend EnduranceUpgradeY As Integer = 53
    Friend VitalityUpgradeX As Integer = 370
    Friend VitalityUpgradeY As Integer = 72
    Friend IntellectUpgradeX As Integer = 370
    Friend IntellectUpgradeY As Integer = 91
    Friend LuckUpgradeX As Integer = 370
    Friend LuckUpgradeY As Integer = 110
    Friend SpiritUpgradeX As Integer = 370
    Friend SpiritUpgradeY As Integer = 129

    'Hotbar Coordinates
    Friend HotbarX As Integer = 489

    Friend HotbarY As Integer = 825

    'pet bar
    Friend PetbarX As Integer = 489

    Friend PetbarY As Integer = 800
    Friend PetStatX As Integer = 943
    Friend PetStatY As Integer = 575

    'Inventory window Coordinates
    Friend InvWindowX As Integer = 943

    Friend InvWindowY As Integer = 475
    Friend Const InvTop As Byte = 9
    Friend Const InvLeft As Byte = 10
    Friend Const InvOffsetY As Byte = 5
    Friend Const InvOffsetX As Byte = 6
    Friend Const InvColumns As Byte = 5

    'Skill window Coordinates
    Friend SkillWindowX As Integer = 943

    Friend SkillWindowY As Integer = 475

    ' skills constants
    Friend Const SkillTop As Byte = 9

    Friend Const SkillLeft As Byte = 10
    Friend Const SkillOffsetY As Byte = 5
    Friend Const SkillOffsetX As Byte = 6
    Friend Const SkillColumns As Byte = 5

    Friend ShowSkillDesc As Boolean
    Friend SkillDescSize As Byte
    Friend SkillDescSkillNum As Integer
    Friend SkillDescName As String
    Friend SkillDescVital As String
    Friend SkillDescInfo As String
    Friend SkillDescType As String
    Friend SkillDescCastTime As String
    Friend SkillDescCoolDown As String
    Friend SkillDescDamage As String
    Friend SkillDescAoe As String
    Friend SkillDescRange As String
    Friend SkillDescReqMp As String
    Friend SkillDescReqLvl As String
    Friend SkillDescReqClass As String
    Friend SkillDescReqAccess As String

    'dialog panel
    Friend DialogPanelVisible As Boolean

    Friend DialogPanelX As Integer = 250
    Friend DialogPanelY As Integer = 400
    Friend OkButtonX As Integer = 50
    Friend OkButtonY As Integer = 130
    Friend CancelButtonX As Integer = 200
    Friend CancelButtonY As Integer = 130

    'bank window Coordinates
    Friend BankWindowX As Integer = 319
    Friend BankWindowY As Integer = 105

    ' Bank constants
    Friend Const BankTop As Byte = 30
    Friend Const BankLeft As Byte = 16
    Friend Const BankOffsetY As Byte = 5
    Friend Const BankOffsetX As Byte = 6
    Friend Const BankColumns As Byte = 9

    ' shop coordinates
    Friend ShopWindowX As Integer = 250

    Friend ShopWindowY As Integer = 125
    Friend ShopFaceX As Integer = 60
    Friend ShopFaceY As Integer = 60

    Friend ShopButtonBuyX As Integer = 150
    Friend ShopButtonBuyY As Integer = 140

    Friend ShopButtonSellX As Integer = 150
    Friend ShopButtonSellY As Integer = 190

    Friend ShopButtonCloseX As Integer = 10
    Friend ShopButtonCloseY As Integer = 215

    ' shop constants
    Friend Const ShopTop As Byte = 46

    Friend Const ShopLeft As Integer = 271
    Friend Const ShopOffsetY As Byte = 5
    Friend Const ShopOffsetX As Byte = 5
    Friend Const ShopColumns As Byte = 6

    'trade constants
    Friend Const TradeWindowX As Integer = 200

    Friend Const TradeWindowY As Byte = 100
    Friend Const OurTradeX As Integer = 2
    Friend Const OurTradeY As Byte = 17
    Friend Const TheirTradeX As Integer = 201
    Friend Const TheirTradeY As Byte = 17

    Friend TradeButtonAcceptX As Integer = 50
    Friend TradeButtonAcceptY As Integer = 320

    Friend TradeButtonDeclineX As Integer = 250
    Friend TradeButtonDeclineY As Integer = 320

    'event chat constants
    Friend Const EventChatX As Integer = 250

    Friend Const EventChatY As Byte = 210
    Friend EventChatTextX As Integer = 113
    Friend EventChatTextY As Integer = 14

    'right click menu
    Friend RClickname As String

    Friend RClickX As Integer
    Friend RClickY As Integer

    Friend DrawChar As Boolean

    Friend CraftPanelX As Integer = 25
    Friend CraftPanelY As Integer = 25
    Friend LoadClassInfo As Boolean

#End Region

    Sub UpdateUi()

        If ReloadFrmMain = True Then
            ReloadFrmMain = False
        End If

        If UpdateNews = True Then
            FrmMenu.lblNews.Text = News
            FrmMenu.Text = Settings.GameName
            FrmGame.Text = Settings.GameName
            UpdateNews = False
        End If

        If PnlRegisterVisible <> FrmMenu.pnlRegister.Visible Then
            FrmMenu.pnlRegister.Visible = PnlRegisterVisible
            FrmMenu.pnlRegister.BringToFront()
        End If

        If DrawChar = True Then
            FrmMenu.DrawCharacter()
            DrawChar = False
        End If

        If PnlCharCreateVisible <> FrmMenu.pnlNewChar.Visible Then
            FrmMenu.pnlNewChar.Visible = PnlCharCreateVisible
            FrmMenu.pnlNewChar.BringToFront()
            DrawChar = True
        End If

        If Lblnextcharleft <> FrmMenu.lblNextChar.Left Then
            FrmMenu.lblNextChar.Left = Lblnextcharleft
        End If

        If Not Cmbclass Is Nothing Then
            FrmMenu.cmbClass.Items.Clear()

            For i = 1 To UBound(Cmbclass)
                FrmMenu.cmbClass.Items.Add(Cmbclass(i))
            Next

            FrmMenu.cmbClass.SelectedIndex = 0

            FrmMenu.rdoMale.Checked = True

            FrmMenu.txtCharName.Focus()

            Cmbclass = Nothing
        End If

        If PnlLoginVisible <> FrmMenu.pnlLogin.Visible Then
            FrmMenu.pnlLogin.Visible = PnlLoginVisible
            If PnlLoginVisible Then
                FrmMenu.txtLogin.Focus()
            End If
        End If

        If PnlCreditsVisible <> FrmMenu.pnlCredits.Visible Then
            FrmMenu.pnlCredits.Visible = PnlCreditsVisible
        End If

        If Frmmenuvisible <> FrmMenu.Visible Then
            FrmMenu.Visible = Frmmenuvisible
        End If

        If DrawCharSelect Then
            FrmMenu.DrawCharacterSelect()
            DrawCharSelect = False
        End If

        If PnlCharSelectVisible <> FrmMenu.pnlCharSelect.Visible Then
            FrmMenu.pnlCharSelect.Visible = PnlCharSelectVisible
            If PnlCharSelectVisible Then
                DrawCharSelect = True
            End If
        End If

        If Frmmaingamevisible <> FrmGame.Visible Then
            FrmGame.Visible = Frmmaingamevisible
        End If


        If InitPetEditor = True Then
            With frmEditor_Pet
                Editor = EDITOR_PET
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_PETS
                    .lstIndex.Items.Add(i & ": " & Trim$(Pet(i).Name))
                Next

                .cmbEvolve.Items.Clear()

                .cmbEvolve.Items.Add("None")

                ' Add the names
                For i = 1 To MAX_PETS
                    .cmbEvolve.Items.Add(i & ": " & Trim$(Pet(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                .cmbEvolve.SelectedIndex = 0
                PetEditorInit()
            End With
            InitPetEditor = False
        End If

        If QuestEditorShow = True Then
            With frmEditor_Quest
                Editor = EditorTasks
                .lstIndex.Items.Clear()
                .cmbQuestReq.Items.Clear()
                .cmbQuestReq.Items.Add("None")
                ' Add the names
                For I = 1 To MaxQuests
                    .lstIndex.Items.Add(I & ": " & Trim$(Quest(I).Name))
                    .cmbQuestReq.Items.Add(I & ": " & Trim$(Quest(I).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                QuestEditorInit()
            End With
            QuestEditorShow = False
        End If

        If InitAnimationEditor = True Then
            With FrmEditor_Animation
                Editor = EDITOR_ANIMATION
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_ANIMATIONS
                    .lstIndex.Items.Add(i & ": " & Trim$(Animation(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                AnimationEditorInit()
            End With
            InitAnimationEditor = False
        End If

        If InitItemEditor = True Then
            ItemEditorPreInit()
            InitItemEditor = False
        End If

        If InitRecipeEditor = True Then
            RecipeEditorPreInit()
            InitRecipeEditor = False
        End If

        If InitClassEditor = True Then
            ClassEditorInit()
            InitClassEditor = False
        End If

        If LoadClassInfo = True Then
            LoadClass()
            LoadClassInfo = False
        End If

        If InitResourceEditor = True Then
            Dim i As Integer

            With frmEditor_Resource
                Editor = EDITOR_RESOURCE
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_RESOURCES
                    If Resource(i).Name Is Nothing Then Resource(i).Name = ""
                    If Resource(i).SuccessMessage Is Nothing Then Resource(i).SuccessMessage = ""
                    If Resource(i).EmptyMessage Is Nothing Then Resource(i).EmptyMessage = ""
                    .lstIndex.Items.Add(i & ": " & Trim$(Resource(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                ResourceEditorInit()
            End With
            InitResourceEditor = False
        End If

        If InitNPCEditor = True Then
            With frmEditor_NPC
                Editor = EDITOR_NPC
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_NPCS
                    .lstIndex.Items.Add(i & ": " & Trim$(Npc(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                NpcEditorInit()
            End With
            InitNPCEditor = False
        End If

        If InitSkillEditor = True Then
            With frmEditor_Skill
                Editor = EDITOR_SKILL
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_SKILLS
                    .lstIndex.Items.Add(i & ": " & Trim$(Skill(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                SkillEditorInit()
            End With
            InitSkillEditor = False
        End If

        If InitShopEditor = True Then
            With frmEditor_Shop
                Editor = EDITOR_SHOP
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_SHOPS
                    .lstIndex.Items.Add(i & ": " & Trim$(Shop(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                ShopEditorInit()
            End With
            InitShopEditor = False
        End If

        If InitAnimationEditor = True Then
            With FrmEditor_Animation
                Editor = EDITOR_ANIMATION
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_ANIMATIONS
                    .lstIndex.Items.Add(i & ": " & Trim$(Animation(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                AnimationEditorInit()
            End With
            InitAnimationEditor = False
        End If

        If HouseEdit = True Then
            With frmEditor_House
                Editor = EDITOR_HOUSE
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MaxHouses
                    .lstIndex.Items.Add(i & ": " & Trim$(House(i).ConfigName))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
            End With

            HouseEditorInit()

            HouseEdit = False
        End If

        If InitProjectileEditor = True Then
            With frmEditor_Projectile
                Editor = EditorProjectile
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MaxProjectiles
                    .lstIndex.Items.Add(i & ": " & Trim$(Projectiles(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                ProjectileEditorInit()
            End With

            InitProjectileEditor = False
        End If

        If frmEditor_Projectile.Visible Then
            EditorProjectile_DrawProjectile()
        End If

        If InitAutoMapper = True Then
            OpenAutomapper()
            InitAutoMapper = False
        End If

        If InitCrafting = True Then
            CraftingInit()
            InitCrafting = False
        End If

        If NeedToOpenShop = True Then
            OpenShop(NeedToOpenShopNum)
            NeedToOpenShop = False
            NeedToOpenShopNum = 0
            PnlShopVisible = True
        End If

        If NeedToOpenBank = True Then
            InBank = True
            PnlBankVisible = True
            DrawBank()
            NeedToOpenBank = False
        End If

        If NeedToOpenTrade = True Then
            InTrade = True
            PnlTradeVisible = True

            NeedToOpenTrade = False
        End If

        If NeedtoCloseTrade = True Then
            InTrade = False
            PnlTradeVisible = False

            NeedtoCloseTrade = False
        End If

        If NeedtoUpdateTrade = True Then
            DrawTrade()
            NeedtoUpdateTrade = False
        End If

        If UpdateCharacterPanel = True Then
            UpdateCharacterPanel = False
        End If

        If Pnlloadvisible <> FrmMenu.pnlLoad.Visible Then
            FrmMenu.pnlLoad.Visible = Pnlloadvisible
        End If

        If UpdateMapnames = True Then
            Dim x As Integer

            FrmAdmin.lstMaps.Items.Clear()

            For x = 1 To MAX_MAPS
                FrmAdmin.lstMaps.Items.Add(x)
                FrmAdmin.lstMaps.Items(x - 1).SubItems.Add(MapNames(x))
            Next

            UpdateMapnames = False
        End If

        If Adminvisible = True Then
            FrmAdmin.Visible = Not FrmAdmin.Visible
            Adminvisible = False
        End If

        If UpdateQuestChat = True Then
            DialogMsg1 = "Quest: " & Trim$(Quest(QuestNum).Name)
            DialogMsg2 = QuestMessage

            DialogType = DialogueTypeQuest

            If QuestNumForStart > 0 AndAlso QuestNumForStart <= MaxQuests Then
                QuestAcceptTag = QuestNumForStart
            End If

            UpdateDialog = True

            UpdateQuestChat = False
        End If

        If UpdateQuestWindow = True Then
            LoadQuestlogBox()
            UpdateQuestWindow = False
        End If

        If UpdateDialog = True Then
            If DialogType = DialogueTypeBuyhome OrElse DialogType = DialogueTypeVisit Then 'house offer & visit
                DialogButton1Text = "Accept"
                DialogButton2Text = "Decline"
                DialogPanelVisible = True
            ElseIf DialogType = DialogueTypeParty OrElse DialogType = DialogueTypeTrade Then
                DialogButton1Text = "Accept"
                DialogButton2Text = "Decline"
                DialogPanelVisible = True
            ElseIf DialogType = DialogueTypeQuest Then
                DialogButton1Text = "Accept"
                DialogButton2Text = "Ok"
                If QuestAcceptTag > 0 Then
                    DialogButton2Text = "Decline"
                End If
                DialogPanelVisible = True
            End If

            UpdateDialog = False
        End If

        If EventChat = True Then
            PnlEventChatVisible = True
            EventChat = False
        End If

        If ShowRClick = True Then
            RClickname = Player(MyTarget).Name
            RClickX = ConvertMapX(CurX * PicX)
            RClickY = ConvertMapY(CurY * PicY)
            PnlRClickVisible = True

            ShowRClick = False
        End If

        If InitMapEditor = True Then
            FrmEditor_MapEditor.MapEditorInit()
            InitMapEditor = False
        End If

        If InitMapProperties = True Then
            FrmEditor_MapEditor.MapPropertiesInit()
            InitMapProperties = False
        End If

        If InitEventEditorForm = True Then
            FrmEditor_Events.InitEventEditorForm()

            ' populate form
            With FrmEditor_Events
                ' set the tabs
                .tabPages.TabPages.Clear()

                For i = 1 To TmpEvent.PageCount
                    .tabPages.TabPages.Add(Str(i))
                Next
                ' items
                .cmbHasItem.Items.Clear()
                .cmbHasItem.Items.Add("None")
                For i = 1 To MAX_ITEMS
                    .cmbHasItem.Items.Add(i & ": " & Trim$(Item(i).Name))
                Next
                ' variables
                .cmbPlayerVar.Items.Clear()
                .cmbPlayerVar.Items.Add("None")
                For i = 1 To MaxVariables
                    .cmbPlayerVar.Items.Add(i & ". " & Variables(i))
                Next
                ' variables
                .cmbPlayerSwitch.Items.Clear()
                .cmbPlayerSwitch.Items.Add("None")
                For i = 1 To MaxSwitches
                    .cmbPlayerSwitch.Items.Add(i & ". " & Switches(i))
                Next
                ' name
                .txtName.Text = TmpEvent.Name
                ' enable delete button
                If TmpEvent.PageCount > 1 Then
                    .btnDeletePage.Enabled = True
                Else
                    .btnDeletePage.Enabled = False
                End If
                .btnPastePage.Enabled = False
                ' Load page 1 to start off with
                CurPageNum = 1
                EventEditorLoadPage(CurPageNum)

                .nudShowTextFace.Maximum = NumFaces
                .nudShowChoicesFace.Maximum = NumFaces
            End With
            ' show the editor
            FrmEditor_Events.Show()

            InitEventEditorForm = False
        End If

        If OptionsVisible = True Then

            ' show in GUI
            If Settings.Music = 1 Then
                FrmOptions.optMOn.Checked = True
            Else
                FrmOptions.optMOff.Checked = True
            End If

            If Settings.Music = 1 Then
                FrmOptions.optSOn.Checked = True
            Else
                FrmOptions.optSOff.Checked = True
            End If

            FrmOptions.lblVolume.Text = "Volume: " & Settings.Volume
            FrmOptions.scrlVolume.Value = Settings.Volume

            FrmOptions.cmbScreenSize.SelectedIndex = Settings.ScreenSize

            If Settings.HighEnd = 1 Then
                FrmOptions.chkHighEnd.Checked = True
            Else
                FrmOptions.chkHighEnd.Checked = False
            End If

            If Settings.ShowNpcBar = 1 Then
                FrmOptions.chkNpcBars.Checked = True
            Else
                FrmOptions.chkNpcBars.Checked = False
            End If

            FrmOptions.Visible = True
            OptionsVisible = False
        End If
    End Sub

End Module