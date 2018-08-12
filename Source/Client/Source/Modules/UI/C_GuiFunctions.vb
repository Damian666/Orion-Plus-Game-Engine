Imports System.Drawing
Imports System.Windows.Forms
Imports ASFW

Friend Module C_GuiFunctions
    Friend Sub CheckGuiMove(x As Integer, y As Integer)
        Dim eqNum As Integer, invNum As Integer, skillslot As Integer
        Dim bankitem As Integer, shopslot As Integer, tradeNum As Integer

        ShowItemDesc = False
        'Charpanel
        If pnlCharacterVisible Then
            If X > CharWindowX AndAlso X < CharWindowX + CharPanelGFXInfo.Width Then
                If Y > CharWindowY AndAlso Y < CharWindowY + CharPanelGFXInfo.Height Then
                    eqNum = IsEqItem(X, Y)
                    If eqNum <> 0 Then
                        UpdateDescWindow(GetPlayerEquipment(MyIndex, eqNum), 0, eqNum, 1)
                        LastItemDesc = GetPlayerEquipment(MyIndex, eqNum) ' set it so you don't re-set values
                        ShowItemDesc = True
                        Exit Sub
                    Else
                        ShowItemDesc = False
                        LastItemDesc = 0 ' no item was last loaded
                    End If
                End If
            End If
        End If

        'inventory
        If pnlInventoryVisible Then
            If AboveInvpanel(X, Y) Then
                InvX = X
                InvY = Y

                If DragInvSlotNum > 0 Then
                    If InTrade Then Exit Sub
                    If InBank OrElse InShop Then Exit Sub
                    DrawInventoryItem(X, Y)
                    ShowItemDesc = False
                    LastItemDesc = 0 ' no item was last loaded
                Else
                    InvNum = IsInvItem(X, Y)

                    If InvNum <> 0 Then
                        ' exit out if we're offering that item
                        For i = 1 To MAX_INV
                            If TradeYourOffer(i).Num = InvNum Then
                                Exit Sub
                            End If
                        Next
                        UpdateDescWindow(GetPlayerInvItemNum(MyIndex, InvNum), GetPlayerInvItemValue(MyIndex, InvNum), InvNum, 0)
                        LastItemDesc = GetPlayerInvItemNum(MyIndex, InvNum) ' set it so you don't re-set values
                        ShowItemDesc = True
                        Exit Sub
                    Else
                        ShowItemDesc = False
                        LastItemDesc = 0 ' no item was last loaded
                    End If
                End If
            End If
        End If

        'skills
        If pnlSkillsVisible = True Then
            If AboveSkillpanel(X, Y) Then
                SkillX = X
                SkillY = Y

                If DragSkillSlotNum > 0 Then
                    If InTrade Then Exit Sub
                    If InBank OrElse InShop Then Exit Sub
                    DrawSkillItem(X, Y)
                    LastSkillDesc = 0 ' no item was last loaded
                    ShowSkillDesc = False
                Else
                    skillslot = IsPlayerSkill(X, Y)

                    If skillslot <> 0 Then
                        UpdateSkillWindow(PlayerSkills(skillslot))
                        LastSkillDesc = PlayerSkills(skillslot)
                        ShowSkillDesc = True
                        Exit Sub
                    Else
                        LastSkillDesc = 0
                        ShowSkillDesc = False
                    End If
                End If

            End If
        End If

        'bank
        If pnlBankVisible = True Then
            If AboveBankpanel(X, Y) Then
                BankX = X
                BankY = Y

                If DragBankSlotNum > 0 Then
                    DrawBankItem(X, Y)
                Else
                    bankitem = IsBankItem(X, Y)

                    If bankitem <> 0 Then

                        UpdateDescWindow(Bank.Item(bankitem).Num, Bank.Item(bankitem).Value, bankitem, 2)
                        ShowItemDesc = True
                        Exit Sub
                    Else
                        ShowItemDesc = False
                        LastItemDesc = 0 ' no item was last loaded
                    End If
                End If

            End If
        End If

        'shop
        If pnlShopVisible = True Then
            If AboveShoppanel(X, Y) Then
                shopslot = IsShopItem(X, Y)

                If shopslot <> 0 Then

                    UpdateDescWindow(Shop(InShop).TradeItem(shopslot).Item, Shop(InShop).TradeItem(shopslot).ItemValue, shopslot, 3)
                    LastItemDesc = Shop(InShop).TradeItem(shopslot).Item
                    ShowItemDesc = True
                    Exit Sub
                Else
                    ShowItemDesc = False
                    LastItemDesc = 0
                End If

            End If
        End If

        'trade
        If pnlTradeVisible = True Then
            If AboveTradepanel(X, Y) Then
                TradeX = X
                TradeY = Y

                'ours
                TradeNum = IsTradeItem(X, Y, True)

                If TradeNum <> 0 Then
                    UpdateDescWindow(GetPlayerInvItemNum(MyIndex, TradeYourOffer(TradeNum).Num), TradeYourOffer(TradeNum).Value, TradeNum, 4)
                    LastItemDesc = GetPlayerInvItemNum(MyIndex, TradeYourOffer(TradeNum).Num) ' set it so you don't re-set values
                    ShowItemDesc = True
                    Exit Sub
                Else
                    ShowItemDesc = False
                    LastItemDesc = 0
                End If

                'theirs
                TradeNum = IsTradeItem(X, Y, False)

                If TradeNum <> 0 Then
                    UpdateDescWindow(TradeTheirOffer(TradeNum).Num, TradeTheirOffer(TradeNum).Value, TradeNum, 4)
                    LastItemDesc = TradeTheirOffer(TradeNum).Num ' set it so you don't re-set values
                    ShowItemDesc = True
                    Exit Sub
                Else
                    ShowItemDesc = False
                    LastItemDesc = 0
                End If
            End If
        End If

    End Sub

    Friend Function CheckGuiClick(x As Integer, y As Integer, e As MouseEventArgs) As Boolean
        Dim eqNum As Integer, invNum As Integer
        Dim slotnum As Integer, hotbarslot As Integer
        dim buffer as ByteStream

        CheckGuiClick = False
        'action panel
        If HUDVisible AndAlso HideGui = False Then
            If AboveActionPanel(X, Y) Then
                ' left click
                If e.Button = MouseButtons.Left Then
                    'Inventory
                    If X > ActionPanelX + InvBtnX AndAlso X < ActionPanelX + InvBtnX + 48 AndAlso Y > ActionPanelY + InvBtnY AndAlso Y < ActionPanelY + InvBtnY + 32 Then
                        PlaySound("Click.ogg")
                        pnlInventoryVisible = Not pnlInventoryVisible
                        pnlCharacterVisible = False
                        pnlSkillsVisible = False
                        CheckGuiClick = True
                        'Skills
                    ElseIf X > ActionPanelX + SkillBtnX AndAlso X < ActionPanelX + SkillBtnX + 48 AndAlso Y > ActionPanelY + SkillBtnY AndAlso Y < ActionPanelY + SkillBtnY + 32 Then
                        PlaySound("Click.ogg")
                        Buffer = New ByteStream(4)
                        Buffer.WriteInt32(ClientPacket.CSkills)
                        Network.SendData(buffer.ToPacket)
                        Buffer.Dispose()
                        pnlSkillsVisible = Not pnlSkillsVisible
                        pnlInventoryVisible = False
                        pnlCharacterVisible = False
                        CheckGuiClick = True
                        'Char
                    ElseIf X > ActionPanelX + CharBtnX AndAlso X < ActionPanelX + CharBtnX + 48 AndAlso Y > ActionPanelY + CharBtnY AndAlso Y < ActionPanelY + CharBtnY + 32 Then
                        PlaySound("Click.ogg")
                        Network.SendRequestPlayerData()
                        pnlCharacterVisible = Not pnlCharacterVisible
                        pnlInventoryVisible = False
                        pnlSkillsVisible = False
                        CheckGuiClick = True
                        'Quest
                    ElseIf X > ActionPanelX + QuestBtnX AndAlso X < ActionPanelX + QuestBtnX + 48 AndAlso Y > ActionPanelY + QuestBtnY AndAlso Y < ActionPanelY + QuestBtnY + 32 Then
                        UpdateQuestLog()
                        ' show the window
                        pnlInventoryVisible = False
                        pnlCharacterVisible = False
                        RefreshQuestLog()
                        pnlQuestLogVisible = Not pnlQuestLogVisible
                        CheckGuiClick = True
                        'Options
                    ElseIf X > ActionPanelX + OptBtnX AndAlso X < ActionPanelX + OptBtnX + 48 AndAlso Y > ActionPanelY + OptBtnY AndAlso Y < ActionPanelY + OptBtnY + 32 Then
                        PlaySound("Click.ogg")
                        pnlCharacterVisible = False
                        pnlInventoryVisible = False
                        pnlSkillsVisible = False

                        OptionsVisible = Not OptionsVisible
                        frmOptions.BringToFront()
                        CheckGuiClick = True
                        'Exit
                    ElseIf X > ActionPanelX + ExitBtnX AndAlso X < ActionPanelX + ExitBtnX + 48 AndAlso Y > ActionPanelY + ExitBtnY AndAlso Y < ActionPanelY + ExitBtnY + 32 Then
                        PlaySound("Click.ogg")
                        frmAdmin.Dispose()
                        Network.SendLeaveGame()
                        'DestroyGame()

                        CheckGuiClick = True
                    End If
                End If
            End If

            'hotbar
            If AboveHotbar(X, Y) Then

                hotbarslot = IsHotBarSlot(e.Location.X, e.Location.Y)

                If e.Button = MouseButtons.Left Then
                    If hotbarslot > 0 Then
                        slotnum = Player(MyIndex).Hotbar(hotbarslot).Slot

                        If slotnum <> 0 Then
                            PlaySound("Click.ogg")
                            SendUseHotbarSlot(hotbarslot)
                        End If

                        CheckGuiClick = True
                    End If
                ElseIf e.Button = MouseButtons.Right Then ' right click
                    If Player(MyIndex).Hotbar(hotbarslot).Slot > 0 Then
                        'forget hotbar skill
                        Dim result1 As DialogResult = MessageBox.Show("Want to Delete this from your hotbar?", GameName, MessageBoxButtons.YesNo)
                        If result1 = DialogResult.Yes Then
                            SendDeleteHotbar(IsHotBarSlot(e.Location.X, e.Location.Y))
                        End If

                        CheckGuiClick = True
                    Else
                        Buffer = New ByteStream(4)
                        Buffer.WriteInt32(ClientPacket.CSkills)
                        Network.SendData(buffer.ToPacket)
                        Buffer.Dispose()
                        pnlSkillsVisible = True
                        AddText("Click on the skill you want to place here", QColorType.TellColor)
                        SelSkillSlot = True
                        SelHotbarSlot = IsHotBarSlot(e.Location.X, e.Location.Y)
                    End If
                End If
                CheckGuiClick = True
            End If

            If AbovePetbar(X, Y) Then
                If Player(MyIndex).Pet.Num > 0 Then
                    hotbarslot = IsPetBarSlot(e.Location.X, e.Location.Y)

                    If e.Button = MouseButtons.Left Then
                        If hotbarslot > 0 Then
                            If hotbarslot >= 1 AndAlso hotbarslot <= 3 Then
                                If hotbarslot = 1 Then
                                    'summon
                                    SendSummonPet()
                                ElseIf hotbarslot = 2 Then
                                    SendPetBehaviour(PetAttackBehaviourAttackonsight)
                                ElseIf hotbarslot = 3 Then
                                    SendPetBehaviour(PetAttackBehaviourGuard)
                                End If

                            ElseIf hotbarslot >= 4 AndAlso hotbarslot <= 7 Then
                                slotnum = Player(MyIndex).Pet.Skill(hotbarslot - 3)

                                If slotnum <> 0 Then
                                    PlaySound("Click.ogg")
                                    SendUsePetSkill(slotnum)
                                End If
                            End If

                            CheckGuiClick = True
                        End If
                    End If

                    CheckGuiClick = True
                End If
            End If

        End If

        'Charpanel
        If pnlCharacterVisible Then
            If AboveCharpanel(X, Y) Then
                ' left click
                If e.Button = MouseButtons.Left Then

                    'lets see if they want to upgrade
                    'Strenght
                    If X > CharWindowX + StrengthUpgradeX AndAlso X < CharWindowX + StrengthUpgradeX + 10 AndAlso Y > CharWindowY + StrengthUpgradeY AndAlso Y < CharWindowY + StrengthUpgradeY + 10 Then
                        If Not GetPlayerPOINTS(MyIndex) = 0 Then
                            PlaySound("Click.ogg")
                            Network.SendTrainStat(1)
                        End If
                    End If
                    'Endurance
                    If X > CharWindowX + EnduranceUpgradeX AndAlso X < CharWindowX + EnduranceUpgradeX + 10 AndAlso Y > CharWindowY + EnduranceUpgradeY AndAlso Y < CharWindowY + EnduranceUpgradeY + 10 Then
                        If Not GetPlayerPOINTS(MyIndex) = 0 Then
                            PlaySound("Click.ogg")
                            Network.SendTrainStat(2)
                        End If
                    End If
                    'Vitality
                    If X > CharWindowX + VitalityUpgradeX AndAlso X < CharWindowX + VitalityUpgradeX + 10 AndAlso Y > CharWindowY + VitalityUpgradeY AndAlso Y < CharWindowY + VitalityUpgradeY + 10 Then
                        If Not GetPlayerPOINTS(MyIndex) = 0 Then
                            PlaySound("Click.ogg")
                            Network.SendTrainStat(3)
                        End If
                    End If
                    'WillPower
                    If X > CharWindowX + LuckUpgradeX AndAlso X < CharWindowX + LuckUpgradeX + 10 AndAlso Y > CharWindowY + LuckUpgradeY AndAlso Y < CharWindowY + LuckUpgradeY + 10 Then
                        If Not GetPlayerPOINTS(MyIndex) = 0 Then
                            PlaySound("Click.ogg")
                            Network.SendTrainStat(4)
                        End If
                    End If
                    'Intellect
                    If X > CharWindowX + IntellectUpgradeX AndAlso X < CharWindowX + IntellectUpgradeX + 10 AndAlso Y > CharWindowY + IntellectUpgradeY AndAlso Y < CharWindowY + IntellectUpgradeY + 10 Then
                        If Not GetPlayerPOINTS(MyIndex) = 0 Then
                            PlaySound("Click.ogg")
                            Network.SendTrainStat(5)
                        End If
                    End If
                    'Spirit
                    If X > CharWindowX + SpiritUpgradeX AndAlso X < CharWindowX + SpiritUpgradeX + 10 AndAlso Y > CharWindowY + SpiritUpgradeY AndAlso Y < CharWindowY + SpiritUpgradeY + 10 Then
                        If Not GetPlayerPOINTS(MyIndex) = 0 Then
                            PlaySound("Click.ogg")
                            Network.SendTrainStat(6)
                        End If
                    End If
                    CheckGuiClick = True
                ElseIf e.Button = MouseButtons.Right Then
                    'first check for equip
                    EqNum = IsEqItem(X, Y)

                    If EqNum <> 0 Then
                        PlaySound("Click.ogg")
                        Dim result1 As DialogResult = MessageBox.Show("Want to Unequip this?", GameName, MessageBoxButtons.YesNo)
                        If result1 = DialogResult.Yes Then
                            Network.SendUnequip(EqNum)
                        End If
                        CheckGuiClick = True
                    End If
                End If
            End If

            'Inventory panel
        ElseIf pnlInventoryVisible Then
            If AboveInvpanel(X, Y) Then
                InvNum = IsInvItem(e.Location.X, e.Location.Y)

                If e.Button = MouseButtons.Left Then
                    If InvNum <> 0 Then
                        If InTrade Then Exit Function
                        If InBank OrElse InShop Then Exit Function

                        If Item(GetPlayerInvItemNum(MyIndex, InvNum)).Type = ItemType.Furniture Then
                            PlaySound("Click.ogg")
                            FurnitureSelected = InvNum
                            CheckGuiClick = True
                        End If

                    End If
                End If
            End If
        End If

        If DialogPanelVisible Then
            'ok button
            If X > DialogPanelX + OkButtonX AndAlso X < DialogPanelX + OkButtonX + ButtonGFXInfo.Width AndAlso Y > DialogPanelY + OkButtonY AndAlso Y < DialogPanelY + OkButtonY + ButtonGFXInfo.Height Then
                VbKeyDown = False
                VbKeyUp = False
                VbKeyLeft = False
                VbKeyRight = False

                If DialogType = DialogueTypeBuyhome Then 'house offer
                    SendBuyHouse(1)
                ElseIf DialogType = DialogueTypeVisit Then
                    SendVisit(1)
                ElseIf DialogType = DialogueTypeParty Then
                    SendAcceptParty()
                ElseIf DialogType = DialogueTypeQuest Then
                    If QuestAcceptTag > 0 Then
                        PlayerHandleQuest(QuestAcceptTag, 1)
                        QuestAcceptTag = 0
                        RefreshQuestLog()
                    End If
                ElseIf DialogType = DialogueTypeTrade Then
                    SendTradeInviteAccept(1)
                End If

                PlaySound("Click.ogg")
                DialogPanelVisible = False
            End If
            'cancel button
            If X > DialogPanelX + CancelButtonX AndAlso X < DialogPanelX + CancelButtonX + ButtonGFXInfo.Width AndAlso Y > DialogPanelY + CancelButtonY AndAlso Y < DialogPanelY + CancelButtonY + ButtonGFXInfo.Height Then
                VbKeyDown = False
                VbKeyUp = False
                VbKeyLeft = False
                VbKeyRight = False

                If DialogType = DialogueTypeBuyhome Then 'house offer declined
                    SendBuyHouse(0)
                ElseIf DialogueTypeVisit Then 'visit declined
                    SendVisit(0)
                ElseIf DialogueTypeParty Then 'party declined
                    SendLeaveParty()
                ElseIf DialogueTypeQuest Then 'quest declined
                    QuestAcceptTag = 0
                ElseIf DialogType = DialogueTypeTrade Then
                    SendTradeInviteAccept(0)
                End If
                PlaySound("Click.ogg")
                DialogPanelVisible = False
            End If
            CheckGuiClick = True
        End If

        If pnlBankVisible = True Then
            If AboveBankpanel(X, Y) Then
                If X > BankWindowX + 140 AndAlso X < BankWindowX + 140 + GetTextWidth("Close Bank", 15) Then
                    If Y > BankWindowY + BankPanelGFXInfo.Height - 15 AndAlso Y < BankWindowY + BankPanelGFXInfo.Height Then
                        PlaySound("Click.ogg")
                        CloseBank()
                    End If
                End If
                CheckGuiClick = True
            End If
        End If

        'trade
        If pnlTradeVisible = True Then
            If AboveTradepanel(X, Y) Then
                'accept button
                If X > TradeWindowX + TradeButtonAcceptX AndAlso X < TradeWindowX + TradeButtonAcceptX + ButtonGFXInfo.Width Then
                    If Y > TradeWindowY + TradeButtonAcceptY AndAlso Y < TradeWindowY + TradeButtonAcceptY + ButtonGFXInfo.Height Then
                        PlaySound("Click.ogg")
                        AcceptTrade()
                    End If
                End If

                'decline button
                If X > TradeWindowX + TradeButtonDeclineX AndAlso X < TradeWindowX + TradeButtonDeclineX + ButtonGFXInfo.Width Then
                    If Y > TradeWindowY + TradeButtonDeclineY AndAlso Y < TradeWindowY + TradeButtonDeclineY + ButtonGFXInfo.Height Then
                        PlaySound("Click.ogg")
                        DeclineTrade()
                    End If
                End If

                CheckGuiClick = True
            End If

        End If

        'eventchat
        If pnlEventChatVisible = True Then
            If AboveEventChat(X, Y) Then
                'Response1
                If EventChoiceVisible(1) Then
                    If X > EventChatX + 10 AndAlso X < EventChatX + 10 + GetTextWidth(EventChoices(1)) Then
                        If Y > EventChatY + 124 AndAlso Y < EventChatY + 124 + 13 Then
                            PlaySound("Click.ogg")
                            Buffer = New ByteStream(4)
                            Buffer.WriteInt32(ClientPacket.CEventChatReply)
                            Buffer.WriteInt32(EventReplyID)
                            Buffer.WriteInt32(EventReplyPage)
                            Buffer.WriteInt32(1)
                            Network.SendData(buffer.ToPacket)
                            Buffer.Dispose()
                            ClearEventChat()
                            InEvent = False
                        End If
                    End If
                End If

                'Response2
                If EventChoiceVisible(2) Then
                    If X > EventChatX + 10 AndAlso X < EventChatX + 10 + GetTextWidth(EventChoices(2)) Then
                        If Y > EventChatY + 146 AndAlso Y < EventChatY + 146 + 13 Then
                            PlaySound("Click.ogg")
                            Buffer = New ByteStream(4)
                            Buffer.WriteInt32(ClientPacket.CEventChatReply)
                            Buffer.WriteInt32(EventReplyID)
                            Buffer.WriteInt32(EventReplyPage)
                            Buffer.WriteInt32(2)
                            Network.SendData(buffer.ToPacket)
                            Buffer.Dispose()
                            ClearEventChat()
                            InEvent = False
                        End If
                    End If
                End If

                'Response3
                If EventChoiceVisible(3) Then
                    If X > EventChatX + 226 AndAlso X < EventChatX + 226 + GetTextWidth(EventChoices(3)) Then
                        If Y > EventChatY + 124 AndAlso Y < EventChatY + 124 + 13 Then
                            PlaySound("Click.ogg")
                            Buffer = New ByteStream(4)
                            Buffer.WriteInt32(ClientPacket.CEventChatReply)
                            Buffer.WriteInt32(EventReplyID)
                            Buffer.WriteInt32(EventReplyPage)
                            Buffer.WriteInt32(3)
                            Network.SendData(buffer.ToPacket)
                            Buffer.Dispose()
                            ClearEventChat()
                            InEvent = False
                        End If
                    End If
                End If

                'Response4
                If EventChoiceVisible(4) Then
                    If X > EventChatX + 226 AndAlso X < EventChatX + 226 + GetTextWidth(EventChoices(4)) Then
                        If Y > EventChatY + 146 AndAlso Y < EventChatY + 146 + 13 Then
                            PlaySound("Click.ogg")
                            Buffer = New ByteStream(4)
                            Buffer.WriteInt32(ClientPacket.CEventChatReply)
                            Buffer.WriteInt32(EventReplyID)
                            Buffer.WriteInt32(EventReplyPage)
                            Buffer.WriteInt32(4)
                            Network.SendData(buffer.ToPacket)
                            Buffer.Dispose()
                            ClearEventChat()
                            InEvent = False
                        End If
                    End If
                End If

                'continue
                If EventChatType <> 1 Then
                    If X > EventChatX + 410 AndAlso X < EventChatX + 410 + GetTextWidth("Continue") Then
                        If Y > EventChatY + 156 AndAlso Y < EventChatY + 156 + 13 Then
                            PlaySound("Click.ogg")
                            Buffer = New ByteStream(4)
                            Buffer.WriteInt32(ClientPacket.CEventChatReply)
                            Buffer.WriteInt32(EventReplyID)
                            Buffer.WriteInt32(EventReplyPage)
                            Buffer.WriteInt32(0)
                            Network.SendData(buffer.ToPacket)
                            Buffer.Dispose()
                            ClearEventChat()
                            InEvent = False
                        End If
                    End If
                End If
                CheckGuiClick = True
            End If
        End If

        'right click
        If pnlRClickVisible = True Then
            If AboveRClickPanel(X, Y) Then
                'trade
                If X > RClickX + (RClickGFXInfo.Width \ 2) - (GetTextWidth("Invite to Trade") \ 2) AndAlso X < RClickX + (RClickGFXInfo.Width \ 2) - (GetTextWidth("Invite to Trade") \ 2) + GetTextWidth("Invite to Trade") Then
                    If Y > RClickY + 35 AndAlso Y < RClickY + 35 + 12 Then
                        If myTarget > 0 Then
                            SendTradeRequest(Player(myTarget).Name)
                        End If
                        pnlRClickVisible = False
                    End If
                End If

                'party
                If X > RClickX + (RClickGFXInfo.Width \ 2) - (GetTextWidth("Invite to Party") \ 2) AndAlso X < RClickX + (RClickGFXInfo.Width \ 2) - (GetTextWidth("Invite to Party") \ 2) + GetTextWidth("Invite to Party") Then
                    If Y > RClickY + 60 AndAlso Y < RClickY + 60 + 12 Then
                        If myTarget > 0 Then
                            SendPartyRequest(Player(myTarget).Name)
                        End If
                        pnlRClickVisible = False
                    End If
                End If

                'House
                If X > RClickX + (RClickGFXInfo.Width \ 2) - (GetTextWidth("Invite to House") \ 2) AndAlso X < RClickX + (RClickGFXInfo.Width \ 2) - (GetTextWidth("Invite to House") \ 2) + GetTextWidth("Invite to House") Then
                    If Y > RClickY + 85 AndAlso Y < RClickY + 85 + 12 Then
                        If myTarget > 0 Then
                            SendInvite(Player(myTarget).Name)
                        End If
                        pnlRClickVisible = False
                    End If
                End If

                CheckGuiClick = True
            End If
        End If

        If pnlQuestLogVisible Then
            If AboveQuestPanel(X, Y) Then
                'check if they press the list
                Dim tmpy As Integer = 10
                For i = 1 To MaxActivequests
                    If QuestNames(i).Trim.Length > 0 Then
                        If X > (QuestLogX + 7) AndAlso X < (QuestLogX + 7) + (GetTextWidth(QuestNames(i))) Then
                            If Y > (QuestLogY + tmpy) AndAlso Y < (QuestLogY + tmpy + 13) Then
                                SelectedQuest = i
                                LoadQuestlogBox()
                            End If
                        End If
                        tmpy = tmpy + 20
                    End If
                Next

                'close button
                If X > (QuestLogX + 195) AndAlso X < (QuestLogX + 290) Then
                    If Y > (QuestLogY + 358) AndAlso Y < (QuestLogY + 375) Then
                        ResetQuestLog()
                    End If

                    CheckGuiClick = True
                End If
            End If
        End If

        If pnlCraftVisible Then
            If AboveCraftPanel(X, Y) Then
                'check if they press the list
                Dim tmpy As Integer = 10
                For i = 1 To MAX_RECIPE
                    If RecipeNames(i).Trim.Length > 0 Then
                        If X > (CraftPanelX + 12) AndAlso X < (CraftPanelX + 12) + (GetTextWidth(RecipeNames(i))) Then
                            If Y > (CraftPanelY + tmpy) AndAlso Y < (CraftPanelY + tmpy + 13) Then
                                SelectedRecipe = i
                                CraftingInit()
                            End If
                        End If
                        tmpy = tmpy + 20
                    End If
                Next

                'start button
                If X > (CraftPanelX + 256) AndAlso X < (CraftPanelX + 330) Then
                    If Y > (CraftPanelY + 415) AndAlso Y < (CraftPanelY + 437) Then
                        If SelectedRecipe > 0 Then
                            CraftProgressValue = 0
                            SendCraftIt(RecipeNames(SelectedRecipe), CraftAmountValue)
                        End If
                    End If
                End If

                'close button
                If X > (CraftPanelX + 614) AndAlso X < (CraftPanelX + 689) Then
                    If Y > (CraftPanelY + 472) AndAlso Y < (CraftPanelY + 494) Then
                        ResetCraftPanel()
                        pnlCraftVisible = False
                        InCraft = False
                        SendCloseCraft()
                    End If
                End If

                'minus
                If X > (CraftPanelX + 340) AndAlso X < (CraftPanelX + 340 + 10) Then
                    If Y > (CraftPanelY + 422) AndAlso Y < (CraftPanelY + 422 + 10) Then
                        If CraftAmountValue > 1 Then
                            CraftAmountValue = CraftAmountValue - 1
                        End If
                    End If
                End If

                'plus
                If X > (CraftPanelX + 392) AndAlso X < (CraftPanelX + 392 + 10) Then
                    If Y > (CraftPanelY + 422) AndAlso Y < (CraftPanelY + 422 + 10) Then
                        If CraftAmountValue < 100 Then
                            CraftAmountValue = CraftAmountValue + 1
                        End If
                    End If
                End If

                CheckGuiClick = True
            End If
        End If

    End Function

    Friend Function CheckGuiDoubleClick(x As Integer, y As Integer, e As MouseEventArgs) As Boolean
        Dim invNum As Integer, skillnum As Integer, bankItem As Integer
        Dim value As Integer, tradeNum As Integer
        Dim multiplier As Double
        Dim i As Integer

        If pnlInventoryVisible Then
            If AboveInvpanel(X, Y) Then
                DragInvSlotNum = 0
                InvNum = IsInvItem(InvX, InvY)

                If InvNum <> 0 Then

                    ' are we in a shop?
                    If InShop > 0 Then
                        Select Case ShopAction
                            Case 0 ' nothing, give value
                                multiplier = Shop(InShop).BuyRate / 100
                                Value = Item(GetPlayerInvItemNum(MyIndex, InvNum)).Price * multiplier
                                If Value > 0 Then
                                    AddText("You can sell this item for " & Value & " gold.", QColorType.TellColor)
                                Else
                                    AddText("The shop does not want this item.", QColorType.AlertColor)
                                End If
                            Case 2 ' 2 = sell
                                SellItem(InvNum)
                        End Select

                        Exit Function
                    End If

                    ' in bank?
                    If InBank Then
                        If Item(GetPlayerInvItemNum(MyIndex, InvNum)).Type = ItemType.Currency OrElse Item(GetPlayerInvItemNum(MyIndex, InvNum)).Stackable = 1 Then
                            CurrencyMenu = 2 ' deposit
                            frmGame.lblCurrency.Text = "How many do you want to deposit?"
                            tmpCurrencyItem = InvNum
                            frmGame.txtCurrency.Text = ""
                            frmGame.pnlCurrency.Visible = True
                            frmGame.pnlCurrency.BringToFront()
                            frmGame.txtCurrency.Focus()
                            Exit Function
                        End If
                        DepositItem(InvNum, 0)
                        Exit Function
                    End If

                    ' in trade?
                    If InTrade = True Then
                        ' exit out if we're offering that item
                        For i = 1 To MAX_INV
                            If TradeYourOffer(i).Num = InvNum Then
                                Exit Function
                            End If
                        Next
                        If Item(GetPlayerInvItemNum(MyIndex, InvNum)).Type = ItemType.Currency OrElse Item(GetPlayerInvItemNum(MyIndex, InvNum)).Stackable = 1 Then
                            CurrencyMenu = 4 ' trade
                            frmGame.lblCurrency.Text = "How many do you want to trade?"
                            tmpCurrencyItem = InvNum
                            frmGame.txtCurrency.Text = ""
                            frmGame.pnlCurrency.Visible = True
                            frmGame.pnlCurrency.BringToFront()
                            frmGame.txtCurrency.Focus()
                            Exit Function
                        End If
                        TradeItem(InvNum, 0)
                        Exit Function
                    End If

                    ' use item if not doing anything else
                    If Item(GetPlayerInvItemNum(MyIndex, InvNum)).Type = ItemType.None Then Exit Function
                    Network.SendUseItem(InvNum)
                    Exit Function
                End If
            End If
        End If

        'Skill panel
        If pnlSkillsVisible = True Then
            If AboveSkillpanel(X, Y) Then

                skillnum = IsPlayerSkill(SkillX, SkillY)

                If skillnum <> 0 Then
                    PlayerCastSkill(skillnum)
                    Exit Function
                End If
            End If
        End If

        'Bank panel
        If pnlBankVisible = True Then
            If AboveBankpanel(X, Y) Then

                DragBankSlotNum = 0

                BankItem = IsBankItem(BankX, BankY)
                If BankItem <> 0 Then
                    If GetBankItemNum(BankItem) = ItemType.None Then Exit Function

                    If Item(GetBankItemNum(BankItem)).Type = ItemType.Currency OrElse Item(GetBankItemNum(BankItem)).Stackable = 1 Then
                        CurrencyMenu = 3 ' withdraw
                        frmGame.lblCurrency.Text = "How many do you want to withdraw?"
                        tmpCurrencyItem = BankItem
                        frmGame.txtCurrency.Text = ""
                        frmGame.pnlCurrency.Visible = True
                        frmGame.txtCurrency.Focus()
                        Exit Function
                    End If

                    WithdrawItem(BankItem, 0)
                    Exit Function
                End If
            End If
        End If

        'trade panel
        If pnlTradeVisible = True Then
            'ours?
            If AboveTradepanel(X, Y) Then
                TradeNum = IsTradeItem(TradeX, TradeY, True)

                If TradeNum <> 0 Then
                    UntradeItem(TradeNum)
                End If
            End If
        End If

    End Function

    Friend Function CheckGuiMouseUp(x As Integer, y As Integer, e As MouseEventArgs) As Boolean
        Dim i As Integer, recPos As Rectangle, buffer As ByteStream
        Dim hotbarslot As Integer

        'Inventory
        If pnlInventoryVisible Then
            If AboveInvpanel(X, Y) Then
                If InTrade > 0 Then Exit Function
                If InBank OrElse InShop Then Exit Function

                If DragInvSlotNum > 0 Then

                    For i = 1 To MAX_INV

                        With recPos
                            .Y = InvWindowY + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                            .Height = PicY
                            .X = InvWindowX + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                            .Width = PicX
                        End With

                        If e.Location.X >= recPos.Left AndAlso e.Location.X <= recPos.Right Then
                            If e.Location.Y >= recPos.Top AndAlso e.Location.Y <= recPos.Bottom Then '
                                If DragInvSlotNum <> i Then
                                    Network.SendChangeInvSlots(DragInvSlotNum, i)
                                    Exit For
                                End If
                            End If
                        End If

                    Next

                End If

                DragInvSlotNum = 0
            ElseIf AboveHotbar(X, Y) Then
                If DragInvSlotNum > 0 Then
                    hotbarslot = IsHotBarSlot(e.Location.X, e.Location.Y)
                    If hotbarslot > 0 Then
                        SendSetHotbarSlot(hotbarslot, DragInvSlotNum, 2)
                    End If
                End If

                DragInvSlotNum = 0
            Else
                If FurnitureSelected > 0 Then
                    If Player(MyIndex).InHouse = MyIndex Then
                        If Item(PlayerInv(FurnitureSelected).Num).Type = ItemType.Furniture Then
                            Buffer = New ByteStream(4)
                            Buffer.WriteInt32(ClientPacket.CPlaceFurniture)
                            i = CurX
                            Buffer.WriteInt32(i)
                            i = CurY
                            Buffer.WriteInt32(i)
                            Buffer.WriteInt32(FurnitureSelected)
                            Network.SendData(buffer.ToPacket)
                            Buffer.Dispose()

                            FurnitureSelected = 0
                        End If
                    End If
                End If
            End If
        End If

        'skills
        If pnlSkillsVisible Then
            If AboveSkillpanel(X, Y) Then
                If InTrade > 0 Then Exit Function
                If InBank OrElse InShop Then Exit Function

                If DragSkillSlotNum > 0 Then

                    For i = 1 To MAX_PLAYER_SKILLS

                        With recPos
                            .Y = SkillWindowY + SkillTop + ((SkillOffsetY + 32) * ((i - 1) \ SkillColumns))
                            .Height = PicY
                            .X = SkillWindowX + SkillLeft + ((SkillOffsetX + 32) * (((i - 1) Mod SkillColumns)))
                            .Width = PicX
                        End With

                        If e.Location.X >= recPos.Left AndAlso e.Location.X <= recPos.Right Then
                            If e.Location.Y >= recPos.Top AndAlso e.Location.Y <= recPos.Bottom Then '
                                If DragSkillSlotNum <> i Then
                                    'SendChangeSkillSlots(DragSkillSlotNum, i)
                                    Exit For
                                End If
                            End If
                        End If

                    Next

                End If

                DragSkillSlotNum = 0
            ElseIf AboveHotbar(X, Y) Then
                If DragSkillSlotNum > 0 Then
                    hotbarslot = IsHotBarSlot(e.Location.X, e.Location.Y)
                    If hotbarslot > 0 Then
                        SendSetHotbarSlot(hotbarslot, DragSkillSlotNum, 1)
                    End If
                End If

                DragSkillSlotNum = 0
            End If
        End If

        'bank
        If pnlBankVisible = True Then
            If AboveBankpanel(X, Y) Then
                ' TODO : Add sub to change bankslots client side first so there's no delay in switching
                If DragBankSlotNum > 0 Then
                    For i = 1 To MAX_BANK
                        With recPos
                            .Y = BankWindowY + BankTop + ((BankOffsetY + 32) * ((i - 1) \ BankColumns))
                            .Height = PicY
                            .X = BankWindowX + BankLeft + ((BankOffsetX + 32) * (((i - 1) Mod BankColumns)))
                            .Width = PicX
                        End With

                        If X >= recPos.Left AndAlso X <= recPos.Right Then
                            If Y >= recPos.Top AndAlso Y <= recPos.Bottom Then
                                If DragBankSlotNum <> i Then
                                    ChangeBankSlots(DragBankSlotNum, i)
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                End If

                DragBankSlotNum = 0
            End If
        End If

    End Function

    Friend Function CheckGuiMouseDown(x As Integer, y As Integer, e As MouseEventArgs) As Boolean
        Dim invNum As Integer, skillnum As Integer, bankNum As Integer, shopItem As Integer

        'Inventory
        If pnlInventoryVisible Then
            If AboveInvpanel(X, Y) Then
                InvNum = IsInvItem(e.Location.X, e.Location.Y)

                If e.Button = MouseButtons.Left Then
                    If InvNum <> 0 Then
                        If InTrade Then Exit Function
                        If InBank OrElse InShop Then Exit Function
                        DragInvSlotNum = InvNum
                    End If
                ElseIf e.Button = MouseButtons.Right Then
                    If Not InBank AndAlso Not InShop AndAlso Not InTrade Then
                        If InvNum <> 0 Then
                            If Item(GetPlayerInvItemNum(MyIndex, InvNum)).Type = ItemType.Currency OrElse Item(GetPlayerInvItemNum(MyIndex, InvNum)).Stackable = 1 Then
                                If GetPlayerInvItemValue(MyIndex, InvNum) > 0 Then
                                    CurrencyMenu = 1 ' drop
                                    frmGame.lblCurrency.Text = "How many do you want to drop?"
                                    tmpCurrencyItem = InvNum
                                    frmGame.txtCurrency.Text = ""
                                    frmGame.pnlCurrency.Visible = True
                                    frmGame.txtCurrency.Focus()
                                End If
                            Else
                                Network.SendDropItem(InvNum, 0)
                            End If
                        End If
                    End If
                End If
            End If
        End If

        'skills
        If pnlSkillsVisible = True Then
            If AboveSkillpanel(X, Y) Then
                skillnum = IsPlayerSkill(e.Location.X, e.Location.Y)
                If e.Button = MouseButtons.Left Then
                    If skillnum <> 0 Then
                        If InTrade Then Exit Function

                        DragSkillSlotNum = skillnum

                        If SelSkillSlot = True Then
                            SendSetHotbarSlot(SelHotbarSlot, skillnum, 1)
                        End If
                    End If
                ElseIf e.Button = MouseButtons.Right Then ' right click

                    If skillnum <> 0 Then
                        Dim result1 As DialogResult = MessageBox.Show("Want to forget this skill?", GameName, MessageBoxButtons.YesNo)
                        If result1 = DialogResult.Yes Then
                            Network.ForgetSkill(skillnum)
                            Exit Function
                        End If
                    End If
                End If
            End If
        End If

        'Bank
        If pnlBankVisible = True Then
            If AboveBankpanel(X, Y) Then
                bankNum = IsBankItem(X, Y)

                If bankNum <> 0 Then

                    If e.Button = MouseButtons.Left Then
                        DragBankSlotNum = bankNum
                    End If

                End If
            End If
        End If

        'Shop
        If pnlShopVisible = True Then
            If AboveShoppanel(X, Y) Then
                shopItem = IsShopItem(X, Y)

                If shopItem > 0 Then
                    Select Case ShopAction
                        Case 0 ' no action, give cost
                            With Shop(InShop).TradeItem(shopItem)
                                AddText("You can buy this item for " & .CostValue & " " & Item(.CostItem).Name.Trim & ".", ColorType.Yellow)
                            End With
                        Case 1 ' buy item
                            ' buy item code
                            BuyItem(shopItem)
                    End Select
                Else
                    ' check for buy button
                    If X > ShopWindowX + ShopButtonBuyX AndAlso X < ShopWindowX + ShopButtonBuyX + ButtonGFXInfo.Width Then
                        If Y > ShopWindowY + ShopButtonBuyY AndAlso Y < ShopWindowY + ShopButtonBuyY + ButtonGFXInfo.Height Then
                            If ShopAction = 1 Then Exit Function
                            ShopAction = 1 ' buying an item
                            AddText("Click on the item in the shop you wish to buy.", ColorType.Yellow)
                        End If
                    End If
                    ' check for sell button
                    If X > ShopWindowX + ShopButtonSellX AndAlso X < ShopWindowX + ShopButtonSellX + ButtonGFXInfo.Width Then
                        If Y > ShopWindowY + ShopButtonSellY AndAlso Y < ShopWindowY + ShopButtonSellY + ButtonGFXInfo.Height Then
                            If ShopAction = 2 Then Exit Function
                            ShopAction = 2 ' selling an item
                            AddText("Double-click on the item in your inventory you wish to sell.", ColorType.Yellow)
                        End If
                    End If
                    ' check for close button
                    If X > ShopWindowX + ShopButtonCloseX AndAlso X < ShopWindowX + ShopButtonCloseX + ButtonGFXInfo.Width Then
                        If Y > ShopWindowY + ShopButtonCloseY AndAlso Y < ShopWindowY + ShopButtonCloseY + ButtonGFXInfo.Height Then
                            dim buffer as ByteStream
                            Buffer = New ByteStream(4)
                            Buffer.WriteInt32(ClientPacket.CCloseShop)
                            Network.SendData(buffer.ToPacket)
                            Buffer.Dispose()
                            pnlShopVisible = False
                            InShop = 0
                            ShopAction = 0
                        End If
                    End If
                End If
            End If
        End If

        If HUDVisible = True Then
            If AboveChatScrollUp(X, Y) Then
                If ScrollMod + FirstLineIndex < MaxChatDisplayLines Then
                    ScrollMod = ScrollMod + 1
                End If
            End If
            If AboveChatScrollDown(X, Y) Then
                If ScrollMod - 1 >= 0 Then
                    ScrollMod = ScrollMod - 1
                End If
            End If
        End If

    End Function

#Region "Support Functions"
    Function IsEqItem(x As Single, y As Single) As Integer
        Dim tempRec As Rect
        Dim i As Integer
        IsEqItem = 0

        For i = 1 To EquipmentType.Count - 1

            If GetPlayerEquipment(MyIndex, i) > 0 AndAlso GetPlayerEquipment(MyIndex, i) <= MAX_ITEMS Then

                With tempRec
                    .Top = CharWindowY + EqTop + ((EqOffsetY + 32) * ((i - 1) \ EqColumns))
                    .Bottom = .Top + PicY
                    .Left = CharWindowX + EqLeft + ((EqOffsetX + 32) * (((i - 1) Mod EqColumns)))
                    .Right = .Left + PicX
                End With

                If X >= tempRec.Left AndAlso X <= tempRec.Right Then
                    If Y >= tempRec.Top AndAlso Y <= tempRec.Bottom Then
                        IsEqItem = i
                        Exit Function
                    End If
                End If
            End If

        Next

    End Function

    Function IsInvItem(x As Single, y As Single) As Integer
        Dim tempRec As Rect
        Dim i As Integer
        IsInvItem = 0

        For i = 1 To MAX_INV

            If GetPlayerInvItemNum(MyIndex, i) > 0 AndAlso GetPlayerInvItemNum(MyIndex, i) <= MAX_ITEMS Then

                With tempRec
                    .Top = InvWindowY + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                    .Bottom = .Top + PicY
                    .Left = InvWindowX + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                    .Right = .Left + PicX
                End With

                If X >= tempRec.Left AndAlso X <= tempRec.Right Then
                    If Y >= tempRec.Top AndAlso Y <= tempRec.Bottom Then
                        IsInvItem = i
                        Exit Function
                    End If
                End If
            End If

        Next

    End Function

    Function IsPlayerSkill(x As Single, y As Single) As Integer
        Dim tempRec As Rect
        Dim i As Integer

        IsPlayerSkill = 0

        For i = 1 To MAX_PLAYER_SKILLS

            If PlayerSkills(i) > 0 AndAlso PlayerSkills(i) <= MAX_PLAYER_SKILLS Then

                With tempRec
                    .Top = SkillWindowY + SkillTop + ((SkillOffsetY + 32) * ((i - 1) \ SkillColumns))
                    .Bottom = .Top + PicY
                    .Left = SkillWindowX + SkillLeft + ((SkillOffsetX + 32) * (((i - 1) Mod SkillColumns)))
                    .Right = .Left + PicX
                End With

                If X >= tempRec.Left AndAlso X <= tempRec.Right Then
                    If Y >= tempRec.Top AndAlso Y <= tempRec.Bottom Then
                        IsPlayerSkill = i
                        Exit Function
                    End If
                End If
            End If

        Next

    End Function

    Function IsBankItem(x As Single, y As Single) As Integer
        Dim tempRec As Rect
        Dim i As Integer

        IsBankItem = 0

        For i = 1 To MAX_BANK
            If GetBankItemNum(i) > 0 AndAlso GetBankItemNum(i) <= MAX_ITEMS Then

                With tempRec
                    .Top = BankWindowY + BankTop + ((BankOffsetY + 32) * ((i - 1) \ BankColumns))
                    .Bottom = .Top + PicY
                    .Left = BankWindowX + BankLeft + ((BankOffsetX + 32) * (((i - 1) Mod BankColumns)))
                    .Right = .Left + PicX
                End With

                If X >= tempRec.Left AndAlso X <= tempRec.Right Then
                    If Y >= tempRec.Top AndAlso Y <= tempRec.Bottom Then

                        IsBankItem = i
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function

    Function IsShopItem(x As Single, y As Single) As Integer
        Dim tempRec As Rectangle
        Dim i As Integer
        IsShopItem = 0

        For i = 1 To MAX_TRADES

            If Shop(InShop).TradeItem(i).Item > 0 AndAlso Shop(InShop).TradeItem(i).Item <= MAX_ITEMS Then
                With tempRec
                    .Y = ShopWindowY + ShopTop + ((ShopOffsetY + 32) * ((i - 1) \ ShopColumns))
                    .Height = PicY
                    .X = ShopWindowX + ShopLeft + ((ShopOffsetX + 32) * (((i - 1) Mod ShopColumns)))
                    .Width = PicX
                End With

                If X >= tempRec.Left AndAlso X <= tempRec.Right Then
                    If Y >= tempRec.Top AndAlso Y <= tempRec.Bottom Then
                        IsShopItem = i
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function

    Function IsTradeItem(x As Single, y As Single, yours As Boolean) As Integer
        Dim tempRec As Rect
        Dim i As Integer
        Dim itemnum As Integer

        IsTradeItem = 0

        For i = 1 To MAX_INV

            If Yours Then
                itemnum = GetPlayerInvItemNum(MyIndex, TradeYourOffer(i).Num)

                With tempRec
                    .Top = TradeWindowY + OurTradeY + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                    .Bottom = .Top + PicY
                    .Left = TradeWindowX + OurTradeX + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                    .Right = .Left + PicX
                End With
            Else
                itemnum = TradeTheirOffer(i).Num

                With tempRec
                    .Top = TradeWindowY + TheirTradeY + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                    .Bottom = .Top + PicY
                    .Left = TradeWindowX + TheirTradeX + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                    .Right = .Left + PicX
                End With
            End If

            If itemnum > 0 AndAlso itemnum <= MAX_ITEMS Then

                If X >= tempRec.Left AndAlso X <= tempRec.Right Then
                    If Y >= tempRec.Top AndAlso Y <= tempRec.Bottom Then
                        IsTradeItem = i
                        Exit Function
                    End If
                End If

            End If

        Next

    End Function

    Function AboveActionPanel(x As Single, y As Single) As Boolean
        AboveActionPanel = False

        If X > ActionPanelX AndAlso X < ActionPanelX + ActionPanelGFXInfo.Width Then
            If Y > ActionPanelY AndAlso Y < ActionPanelY + ActionPanelGFXInfo.Height Then
                AboveActionPanel = True
            End If
        End If
    End Function

    Function AboveHotbar(x As Single, y As Single) As Boolean
        AboveHotbar = False

        If X > HotbarX AndAlso X < HotbarX + HotBarGFXInfo.Width Then
            If Y > HotbarY AndAlso Y < HotbarY + HotBarGFXInfo.Height Then
                AboveHotbar = True
            End If
        End If
    End Function

    Function AbovePetbar(x As Single, y As Single) As Boolean
        AbovePetbar = False

        If X > PetbarX AndAlso X < PetbarX + PetbarGFXInfo.Width Then
            If Y > PetbarY AndAlso Y < PetbarY + HotBarGFXInfo.Height Then
                AbovePetbar = True
            End If
        End If
    End Function

    Function AboveInvpanel(x As Single, y As Single) As Boolean
        AboveInvpanel = False

        If X > InvWindowX AndAlso X < InvWindowX + InvPanelGFXInfo.Width Then
            If Y > InvWindowY AndAlso Y < InvWindowY + InvPanelGFXInfo.Height Then
                AboveInvpanel = True
            End If
        End If
    End Function

    Function AboveCharpanel(x As Single, y As Single) As Boolean
        AboveCharpanel = False

        If X > CharWindowX AndAlso X < CharWindowX + CharPanelGFXInfo.Width Then
            If Y > CharWindowY AndAlso Y < CharWindowY + CharPanelGFXInfo.Height Then
                AboveCharpanel = True
            End If
        End If
    End Function

    Function AboveSkillpanel(x As Single, y As Single) As Boolean
        AboveSkillpanel = False

        If X > SkillWindowX AndAlso X < SkillWindowX + SkillPanelGFXInfo.Width Then
            If Y > SkillWindowY AndAlso Y < SkillWindowY + SkillPanelGFXInfo.Height Then
                AboveSkillpanel = True
            End If
        End If
    End Function

    Function AboveBankpanel(x As Single, y As Single) As Boolean
        AboveBankpanel = False

        If X > BankWindowX AndAlso X < BankWindowX + BankPanelGFXInfo.Width Then
            If Y > BankWindowY AndAlso Y < BankWindowY + BankPanelGFXInfo.Height Then
                AboveBankpanel = True
            End If
        End If
    End Function

    Function AboveShoppanel(x As Single, y As Single) As Boolean
        AboveShoppanel = False

        If X > ShopWindowX AndAlso X < ShopWindowX + ShopPanelGFXInfo.Width Then
            If Y > ShopWindowY AndAlso Y < ShopWindowY + ShopPanelGFXInfo.Height Then
                AboveShoppanel = True
            End If
        End If
    End Function

    Function AboveTradepanel(x As Single, y As Single) As Boolean
        AboveTradepanel = False

        If X > TradeWindowX AndAlso X < TradeWindowX + TradePanelGFXInfo.Width Then
            If Y > TradeWindowY AndAlso Y < TradeWindowY + TradePanelGFXInfo.Height Then
                AboveTradepanel = True
            End If
        End If
    End Function

    Function AboveEventChat(x As Single, y As Single) As Boolean
        AboveEventChat = False

        If X > EventChatX AndAlso X < EventChatX + EventChatGFXInfo.Width Then
            If Y > EventChatY AndAlso Y < EventChatY + EventChatGFXInfo.Height Then
                AboveEventChat = True
            End If
        End If
    End Function

    Function AboveChatScrollUp(x As Single, y As Single) As Boolean
        AboveChatScrollUp = False

        If X > ChatWindowX + ChatWindowGFXInfo.Width - 24 AndAlso X < ChatWindowX + ChatWindowGFXInfo.Width Then
            If Y > ChatWindowY AndAlso Y < ChatWindowY + 24 Then 'ChatWindowGFXInfo.height Then
                AboveChatScrollUp = True
            End If
        End If
    End Function

    Function AboveChatScrollDown(x As Single, y As Single) As Boolean
        AboveChatScrollDown = False

        If X > ChatWindowX + ChatWindowGFXInfo.Width - 24 AndAlso X < ChatWindowX + ChatWindowGFXInfo.Width Then
            If Y > ChatWindowY + ChatWindowGFXInfo.Height - 24 AndAlso Y < ChatWindowY + ChatWindowGFXInfo.Height Then
                AboveChatScrollDown = True
            End If
        End If
    End Function

    Function AboveRClickPanel(x As Single, y As Single) As Boolean
        AboveRClickPanel = False

        If X > RClickX AndAlso X < RClickX + RClickGFXInfo.Width Then
            If Y > RClickY AndAlso Y < RClickY + RClickGFXInfo.Height Then
                AboveRClickPanel = True
            End If
        End If
    End Function

    Function AboveQuestPanel(x As Single, y As Single) As Boolean
        AboveQuestPanel = False

        If X > QuestLogX AndAlso X < QuestLogX + QuestGFXInfo.Width Then
            If Y > QuestLogY AndAlso Y < QuestLogY + QuestGFXInfo.Height Then
                AboveQuestPanel = True
            End If
        End If
    End Function

    Function AboveCraftPanel(x As Single, y As Single) As Boolean
        AboveCraftPanel = False

        If X > CraftPanelX AndAlso X < CraftPanelX + CraftGFXInfo.Width Then
            If Y > CraftPanelY AndAlso Y < CraftPanelY + CraftGFXInfo.Height Then
                AboveCraftPanel = True
            End If
        End If
    End Function
#End Region

End Module











































































































































































































































































































































































