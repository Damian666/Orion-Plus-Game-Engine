Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Winforms

Namespace Graphics
    Friend Module modGui

#Region " Draw Buttons "

        Friend Sub DrawButton(text As String, destination As Vector2, hover As Boolean)
            Dim btn = If(hover, Button_Hover, Button_Normal)
            Dim fontSize = Font_Default.MeasureString(text)

            RenderTexture(btn, destination)
            RenderText(text, New Vector2(destination.X + btn.Width / 2 - fontSize.X \ 2,
                                         destination.Y + btn.Height / 2 - fontSize.Y / 2))
        End Sub

        Friend Sub DrawButton(font As SpriteFont, text As String, destination As Vector2, hover As Boolean)
            Dim btn = If(hover, Button_Hover, Button_Normal)
            Dim fontSize = font.MeasureString(text)

            RenderTexture(btn, destination)
            RenderText(font, text, New Vector2(destination.X + btn.Width / 2 - fontSize.X \ 2,
                                               destination.Y + btn.Height / 2 - fontSize.Y / 2))
        End Sub

        Friend Sub DrawButton(fontColor As Color, text As String, destination As Vector2, hover As Boolean)
            Dim btn = If(hover, Button_Hover, Button_Normal)
            Dim fontSize = Font_Default.MeasureString(text)

            RenderTexture(btn, destination)
            RenderText(text, New Vector2(destination.X + btn.Width / 2 - fontSize.X \ 2,
                                         destination.Y + btn.Height / 2 - fontSize.Y / 2), fontColor)
        End Sub

        Friend Sub DrawButton(font As SpriteFont, fontColor As Color,
                              text As String, destination As Vector2, hover As Boolean)
            Dim btn = If(hover, Button_Hover, Button_Normal)
            Dim fontSize = font.MeasureString(text)

            RenderTexture(btn, destination)
            RenderText(font, text, New Vector2(destination.X + btn.Width / 2 - fontSize.X \ 2,
                                               destination.Y + btn.Height / 2 - fontSize.Y / 2), fontColor)
        End Sub

#End Region

#Region " Draw Cursor "

        Friend Sub DrawCursor()
            DrawCursorBank()
            DrawCursorItem()
            DrawCursorSkill()
            RenderTexture(Cursor, CursorPos)
        End Sub

        Friend Sub DrawCursorBank()
            Dim index = Bank.Item(DragBankSlotNum).Index
            If index < 1 OrElse index > MAX_ITEMS Then Return

            Dim sprite = Item(index).Pic
            If sprite < 1 OrElse sprite > UBound(Items) Then Exit Sub

            Dim position = CursorPos + New Vector2(16, 16)
            RenderTexture(Items(sprite), position,
                          New Rectangle(BankItemFrame(index) * PicX, 0, PicX, PicY))

            ' Stack Text
            Dim amount = Bank.Item(DragBankSlotNum).Amount
            If amount > 1 Then
                position += New Vector2(-4, 22)

                If amount >= 1000000000 Then ' Billion
                    amount = Math.Floor(amount / 1000000000)
                    RenderText(amount & "B", position, Color.Blue)
                ElseIf amount >= 1000000 Then ' Million
                    amount = Math.Floor(amount / 1000000)
                    RenderText(amount & "M", position, Color.Green)
                ElseIf amount >= 1000 Then ' Thousand
                    amount = Math.Floor(amount / 1000)
                    RenderText(amount & "K", position, Color.Yellow)
                Else
                    RenderText(amount, position, Color.White)
                End If
            End If
        End Sub

        Friend Sub DrawCursorItem()
            Dim index = PlayerInv(DragInvSlotNum).Index
            If index < 1 OrElse index > MAX_ITEMS Then Return

            Dim sprite = Item(index).Pic
            If sprite < 1 OrElse sprite > UBound(Items) Then Exit Sub

            Dim position = CursorPos + New Vector2(16, 16)
            RenderTexture(Items(sprite), position,
                          New Rectangle(InvItemFrame(index) * PicX, 0, PicX, PicY))

            ' Stack Text
            Dim amount = PlayerInv(DragInvSlotNum).Amount
            If amount > 1 Then
                position += New Vector2(-4, 22)

                If amount >= 1000000000 Then ' Billion
                    amount = Math.Floor(amount / 1000000000)
                    RenderText(amount & "B", position, Color.Blue)
                ElseIf amount >= 1000000 Then ' Million
                    amount = Math.Floor(amount / 1000000)
                    RenderText(amount & "M", position, Color.Green)
                ElseIf amount >= 1000 Then ' Thousand
                    amount = Math.Floor(amount / 1000)
                    RenderText(amount & "K", position, Color.Yellow)
                Else
                    RenderText(amount, position, Color.White)
                End If
            End If
        End Sub

        Friend Sub DrawCursorSkill()
            Dim index = DragSkillSlotNum
            If index < 1 OrElse index > MAX_SKILLS Then Exit Sub

            Dim sprite = Skill(index).Icon
            If sprite < 1 OrElse sprite > UBound(Skills) Then Exit Sub

            Dim position = CursorPos + New Vector2(16, 16)
            RenderTexture(Skills(index), position)
        End Sub

#End Region

        Friend Sub DrawHud()
            ' Panel
            RenderTexture(PanelHud, New Vector2(HudWindowX, HudWindowY))

            ' Face
            If Player(Myindex).Sprite <= UBound(Faces) Then
                RenderTexture(Faces(Player(Myindex).Sprite),
                              New Vector2(HudFaceX, HudFaceY))
            End If

            '###################
            '###  Stat Bars  ###
            '###################

            ' Hp
            Dim valueLeft = GetPlayerVital(Myindex, VitalType.HP)
            Dim valueRight = GetPlayerMaxVital(Myindex, VitalType.HP)

            RenderTexture(BarHp,
                          New Vector2(HudWindowX + HudhpBarX, HudWindowY + HudhpBarY + 4),
                          New Rectangle(0, 0, BarHp.Width * (valueLeft / valueRight), BarHp.Height))

            RenderText(valueLeft & "/" & valueRight, ' This is text
                       New Vector2(HudWindowX + HudhpBarX + 65, HudWindowY + HudhpBarY + 4))

            ' Mp
            valueLeft = GetPlayerVital(Myindex, VitalType.MP)
            valueRight = GetPlayerMaxVital(Myindex, VitalType.MP)

            RenderTexture(BarMp,
                          New Vector2(HudWindowX + HudmpBarX, HudWindowY + HudmpBarY + 4),
                          New Rectangle(0, 0, BarHp.Width * (valueLeft / valueRight), BarHp.Height))

            RenderText(valueLeft & "/" & valueRight, ' This is text
                       New Vector2(HudWindowX + HudmpBarX + 65, HudWindowY + HudmpBarY + 4))

            ' Exp
            valueLeft = GetPlayerExp(Myindex)
            valueRight = NextlevelExp

            RenderTexture(BarMp,
                          New Vector2(HudWindowX + HudexpBarX, HudWindowY + HudexpBarY + 4),
                          New Rectangle(0, 0, BarHp.Width * (valueLeft / valueRight), BarHp.Height))

            RenderText(valueLeft & "/" & valueRight, ' This is text
                       New Vector2(HudWindowX + HudexpBarX + 65, HudWindowY + HudexpBarY + 4))

            '######################
            '###  General Text  ###
            '######################

            ' Fps
            RenderText(Language.Game.Fps & Fps,
                       New Vector2(HudWindowX + HudhpBarX + BarHp.Width + 10,
                                   HudWindowY + HudhpBarY + 4))

            ' Ping
            RenderText(Language.Game.Ping & PingToDraw,
                       New Vector2(HudWindowX + HudmpBarX + BarMp.Width + 10,
                                   HudWindowY + HudmpBarY + 4))

            ' Time
            RenderText(Language.Game.Time & Time.Instance.ToString("h:mm"),
                       New Vector2(HudWindowX + HudexpBarX + BarExp.Width + 10,
                                   HudWindowY + HudexpBarY + 4))

            ' Lps
            RenderText(Language.Game.Lps & Lps,
                       New Vector2(HudWindowX + HudexpBarX + BarExp.Width + 10,
                                   HudWindowY + HudexpBarY + 20))

            ' Draw map name
            Select Case Map.Moral
                Case MapMoralType.None
                    RenderText(Language.Game.MapName & Map.Name,
                               New Vector2(110, 70), Color.Red)
                Case MapMoralType.Safe
                    RenderText(Language.Game.MapName & Map.Name,
                               New Vector2(110, 70), Color.Green)
                Case Else
                    RenderText(Language.Game.MapName & Map.Name,
                               New Vector2(110, 70), Color.White)
            End Select
        End Sub

        Sub DrawActionPanel()
            RenderTexture(PanelAction, New Vector2(ActionPanelX, ActionPanelY))
        End Sub

        Friend Sub DrawBank()
            Static timer As Integer = GetTickCount() + 100
            Dim spriteCount = UBound(Items)

            ' Panel
            RenderTexture(PanelBank, New Vector2(BankWindowX, BankWindowY))

            ' Header Label
            RenderText("Bank", New Vector2(BankWindowX + 140, BankWindowY + 6))

            ' Close Label
            RenderText("Close", New Vector2(BankWindowX + 140, BankWindowY + PanelBank.Height - 20))

            ' Items
            If GetTickCount() > timer Then
                For i = 1 To MAX_BANK

                    ' Get Info
                    Dim index = Bank.Item(i).Index
                    If index < 1 OrElse index > MAX_ITEMS Then Continue For

                    Dim sprite = Item(index).Pic
                    If sprite < 1 OrElse sprite > spriteCount Then Continue For

                    ' Calculate Frame
                    Dim maxFrames = Items(sprite).Width / PicX - 1
                    If BankItemFrame(i) < maxFrames Then
                        BankItemFrame(i) += 1
                    Else
                        BankItemFrame(i) = 0
                    End If

                    ' Render Item
                    Dim source = New Rectangle(BankItemFrame(i) * PicX, 0, PicX, PicY)
                    Dim position = New Vector2(BankWindowX + BankLeft, BankWindowY + BankTop)
                    position += New Vector2((BankOffsetX + PicX) * ((i - 1) Mod BankColumns),
                                            (BankOffsetY + PicY) * ((i - 1) \ BankColumns))
                    RenderTexture(Items(sprite), position, source)

                    ' Stack Text
                    Dim amount = Bank.Item(i).Amount
                    If amount > 1 Then
                        position += New Vector2(-4, 22)

                        If amount >= 1000000000 Then ' Billion
                            amount = Math.Floor(amount / 1000000000)
                            RenderText(amount & "B", position, Color.Blue)
                        ElseIf amount >= 1000000 Then ' Million
                            amount = Math.Floor(amount / 1000000)
                            RenderText(amount & "M", position, Color.Green)
                        ElseIf amount >= 1000 Then ' Thousand
                            amount = Math.Floor(amount / 1000)
                            RenderText(amount & "K", position, Color.Yellow)
                        Else
                            RenderText(amount, position, Color.White)
                        End If
                    End If

                Next : timer = GetTickCount() + 100
            End If
        End Sub

        Friend Sub DrawDescription(type As DescriptionType)
            Dim view = UniversalBackend.GraphicsDevice.Viewport.Bounds
            Dim position = CursorPos

            ' Do placement adjustments
            If position.X + PanelDescription.Width > view.Right Then

            End If
            If position.Y + PanelDescription.Height > view.Bottom Then

            End If

            Select Case type
                    Case 
            End Select
                If PnlCharacterVisible = True Then
                    xoffset = CharWindowX
                    yoffset = CharWindowY
                End If
                If PnlInventoryVisible = True Then
                    xoffset = InvWindowX
                    yoffset = InvWindowY
                End If
                If PnlBankVisible = True Then
                    xoffset = BankWindowX
                    yoffset = BankWindowY
                End If
                If PnlShopVisible = True Then
                    xoffset = ShopWindowX
                    yoffset = ShopWindowY
                End If
                If PnlTradeVisible = True Then
                    xoffset = TradeWindowX
                    yoffset = TradeWindowY
                End If

                'first render panel
                RenderSprite(DescriptionSprite, GameWindow, xoffset - DescriptionGfxInfo.Width, yoffset, 0, 0, DescriptionGfxInfo.Width, DescriptionGfxInfo.Height)

                'name
                For Each str As String In WordWrap(ItemDescName, 22, WrapMode.Characters, WrapType.BreakWord)
                    'description
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 12 + y, str, ItemDescRarityColor, ItemDescRarityBackColor, GameWindow)
                    y = y + 15
                Next

                If ShiftDown OrElse VbKeyShift = True Then
                    'info
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 56, ItemDescInfo, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)

                    'cost
                    'DrawText(Xoffset - DescriptionGFXInfo.width + 10, Yoffset + 74, "Worth: " & ItemDescCost, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                    'type
                    'DrawText(Xoffset - DescriptionGFXInfo.width + 10, Yoffset + 90, "Type: " & ItemDescType, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                    'speed
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 74, "Speed: " & ItemDescSpeed, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                    'level
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 90, "Level required: " & ItemDescLevel, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                    'bonuses
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 118, "=Bonuses=", Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                    'strength
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 134, "Strenght: " & ItemDescStr, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                    'vitality
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 150, "Vitality: " & ItemDescVit, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                    'intelligence
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 166, "Intelligence: " & ItemDescInt, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                    'endurance
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 182, "Endurance: " & ItemDescEnd, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                    'luck
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 198, "Luck: " & ItemDescLuck, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                    'spirit
                    DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 214, "Spirit: " & ItemDescSpr, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                Else
                    For Each str As String In WordWrap(ItemDescDescription, 22, WrapMode.Characters, WrapType.BreakWord)
                        'description
                        DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 44 + y, str, Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Black, GameWindow)
                        y = y + 15
                    Next
                End If

        End Sub

        Friend Sub DrawInventory()
            Static timer As Integer = GetTickCount() + 100
            Dim spriteCount = UBound(Items)

            ' Panel
            RenderTexture(PanelInventory, New Vector2(InvWindowX, InvWindowY))

            ' Items
            If GetTickCount() > timer Then
                For i = 1 To MAX_INV

                    ' Get Info
                    Dim index = PlayerInv(i).Index
                    If index < 1 OrElse index > MAX_ITEMS Then Continue For

                    Dim sprite = Item(index).Pic
                    If sprite < 1 OrElse sprite > spriteCount Then Continue For

                    ' exit out if we're offering item in a trade.
                    Dim isTraded = False
                    If InTrade Then
                        For x = 1 To MAX_INV
                            If TradeYourOffer(x).Index = i Then
                                isTraded = True
                                Exit For
                            End If
                        Next
                    End If

                    ' Calculate Frame
                    Dim maxFrames = Items(sprite).Width / PicX - 1
                    If InvItemFrame(i) < maxFrames Then
                        InvItemFrame(i) += 1
                    Else
                        InvItemFrame(i) = 0
                    End If

                    ' Render Item
                    Dim source = New Rectangle(InvItemFrame(i) * PicX, 0, PicX, PicY)
                    Dim position = New Vector2(InvWindowX + InvLeft, InvWindowY + InvTop)
                    position += New Vector2((InvOffsetX + PicX) * (i - 1) Mod InvColumns,
                                            (InvOffsetY + PicY) * ((i - 1) \ InvColumns))

                    If isTraded Then ' Render grayed out if traded
                        RenderTexture(Items(sprite), position, source, Color.DarkGray)
                    Else
                        RenderTexture(Items(sprite), position, source)
                    End If

                    ' Stack Text
                    Dim amount = PlayerInv(i).Amount
                    If amount > 1 Then
                        position += New Vector2(-4, 22)

                        If amount >= 1000000000 Then ' Billion
                            amount = Math.Floor(amount / 1000000000)
                            RenderText(amount & "B", position, Color.Blue)
                        ElseIf amount >= 1000000 Then ' Million
                            amount = Math.Floor(amount / 1000000)
                            RenderText(amount & "M", position, Color.Green)
                        ElseIf amount >= 1000 Then ' Thousand
                            amount = Math.Floor(amount / 1000)
                            RenderText(amount & "K", position, Color.Yellow)
                        Else
                            RenderText(amount, position, Color.White)
                        End If
                    End If

                Next : timer = GetTickCount() + 100
            End If
        End Sub

        Friend Sub DrawSkills()
            Dim spriteCount = UBound(Skills)

            ' Panel
            RenderTexture(PanelSkill, New Vector2(SkillWindowX, SkillWindowY))

            ' Skills
            For i = 1 To MAX_PLAYER_SKILLS

                ' Get Info
                Dim index = PlayerSkills(i)
                If index < 1 OrElse index > MAX_SKILLS Then Continue For

                Dim sprite = Skill(index).Icon
                If sprite < 1 OrElse sprite > spriteCount Then Continue For

                ' Render Item
                Dim position = New Vector2(SkillWindowX + SkillLeft, SkillWindowY + SkillTop)
                position += New Vector2((SkillOffsetX + 32) * ((i - 1) Mod SkillColumns),
                                        (SkillOffsetY + 32) * ((i - 1) \ SkillColumns))

                If SkillCd(i) > 0 Then ' On Cooldown
                    RenderTexture(Skills(sprite), position, Color.DarkGray)
                Else ' Not on cooldown
                    RenderTexture(Skills(sprite), position)
                End If

            Next
        End Sub

    End Module
End Namespace