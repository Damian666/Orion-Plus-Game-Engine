Imports SFML.Graphics
Imports SFML.System

Namespace Graphics
    Friend Module modDrawGui

        ' Animation Cache
        Friend InventoryFrame(MAX_INV) As Byte
        Friend BankFrame(MAX_BANK) As Byte

        ' Colors
        Friend DescriptionColor As Color = ItemRarityColor(0)
        Friend ItemRarityColor() = {
            New Color(255, 255, 255), ' White
            New Color(102, 255, 0),   ' Green
            New Color(73, 151, 208),  ' Blue
            New Color(255, 0, 0),     ' Red
            New Color(159, 0, 197),   ' Purple
            New Color(255, 215, 0)    ' Gold
        }

        ' Panels
        Friend ActionPosition As New Vector2f(942, 755)

        Friend BankPosition As New Vector2f(319, 105)
        Friend BankItemPosition As New Vector2f(16, 30)
        Friend BankOffset As New Vector2f(5 + PicX, 6 + PicY)

        Friend ChatPosition As New Vector2f(1, 705)
        Friend ChatInputPosition As New Vector2f(1, FrmGame.Height - 55)

        Friend InventoryPosition As New Vector2f(943, 475)
        Friend InventoryItemPosition As New Vector2f(9, 10)
        Friend InventoryOffset As New Vector2f(5 + PicX, 6 + PicY)

        Friend MenuPosition As New Vector2f(0, 0)

        Friend HudPosition As New Vector2f(0, 0)
        Friend HudFacePosition As New Vector2f(4, 4)
        Friend HudHpPosition As New Vector2f(110, 10)
        Friend HudMpPosition As New Vector2f(110, 30)
        Friend HudExpPosition As New Vector2f(110, 50)

        Friend Sub RenderGui(target As RenderTarget)
            If HideGui = True Then Return

            SpriteBatch.Begin(target)

            DrawHud()
            DrawActionPanel()
            'TODO: DrawChat() 
            DrawHotbar()
            DrawPetBar()
            DrawPetStats()

            'TODO: If PnlCharacterVisible Then DrawEquipment()
            If PnlInventoryVisible Then DrawInventory()
            If PnlSkillsVisible Then DrawSkills()
            'TODO: If DialogPanelVisible Then DrawDialogPanel()
            If PnlBankVisible Then DrawBank()
            If PnlShopVisible Then DrawShop()
            If PnlTradeVisible Then DrawTrade()
            If PnlEventChatVisible Then DrawEventChat()
            If PnlRClickVisible Then DrawMenu()
            If PnlQuestLogVisible Then DrawQuestLog()
            If PnlCraftVisible Then DrawCraftPanel()

            DrawCursor()

            If ShowItemDesc Then DrawDescription(DescriptionType.Item)
            If ShowSkillDesc = True Then DrawDescription(DescriptionType.Skill)

            SpriteBatch.End()

        End Sub

#Region " Draw Buttons "

        Friend Sub DrawButton(text As String, size As UInteger, destination As Vector2f, hover As Boolean)
            Dim btn = If(hover, Button_Hover, Button_Normal)
            Dim word As New Text(text, Font_Default, size)
            Dim bounds = word.GetLocalBounds
            word.Position = destination + New Vector2f(btn.Size.X / 2 - bounds.Width / 2,
                                                       btn.Size.Y / 2 - bounds.Height / 2)


            RenderTexture(btn, destination)
            RenderDrawable(word) : word.Dispose()
        End Sub

        Friend Sub DrawButton(text As String, font As Font, size As UInteger,
                              destination As Vector2f, hover As Boolean)
            Dim btn = If(hover, Button_Hover, Button_Normal)
            Dim word As New Text(text, font, size)
            Dim bounds = word.GetLocalBounds
            word.Position = destination + New Vector2f(btn.Size.X / 2 - bounds.Width / 2,
                                                       btn.Size.Y / 2 - bounds.Height / 2)


            RenderTexture(btn, destination)
            RenderDrawable(word) : word.Dispose()
        End Sub

        Friend Sub DrawButton(text As String, fontColor As Color, size As UInteger,
                              destination As Vector2f, hover As Boolean)
            Dim btn = If(hover, Button_Hover, Button_Normal)
            Dim word As New Text(text, Font_Default, size)
            Dim bounds = word.GetLocalBounds
            word.Color = fontColor
            word.Position = destination + New Vector2f(btn.Size.X / 2 - bounds.Width / 2,
                                                       btn.Size.Y / 2 - bounds.Height / 2)


            RenderTexture(btn, destination)
            RenderDrawable(word) : word.Dispose()
        End Sub

        Friend Sub DrawButton(text As String, fontBorderThickness As Single,
                              fontBorderColor As Color, fontFillColor As Color,
                              size As UInteger, destination As Vector2f, hover As Boolean)
            Dim btn = If(hover, Button_Hover, Button_Normal)
            Dim word As New Text(text, Font_Default, size)
            Dim bounds = word.GetLocalBounds
            word.OutlineThickness = fontBorderThickness
            word.OutlineColor = fontBorderColor
            word.FillColor = fontFillColor
            word.Position = destination + New Vector2f(btn.Size.X / 2 - bounds.Width / 2,
                                                       btn.Size.Y / 2 - bounds.Height / 2)


            RenderTexture(btn, destination)
            RenderDrawable(word) : word.Dispose()
        End Sub

        Friend Sub DrawButton(text As String, font As Font, fontColor As Color,
                              size As UInteger, destination As Vector2f, hover As Boolean)
            Dim btn = If(hover, Button_Hover, Button_Normal)
            Dim word As New Text(text, font, size)
            Dim bounds = word.GetLocalBounds
            word.Color = fontColor
            word.Position = destination + New Vector2f(btn.Size.X / 2 - bounds.Width / 2,
                                                       btn.Size.Y / 2 - bounds.Height / 2)


            RenderTexture(btn, destination)
            RenderDrawable(word) : word.Dispose()
        End Sub

        Friend Sub DrawButton(text As String, font As Font, fontBorderThickness As Single,
                              fontBorderColor As Color, fontFillColor As Color,
                              size As UInteger, destination As Vector2f, hover As Boolean)
            Dim btn = If(hover, Button_Hover, Button_Normal)
            Dim word As New Text(text, font, size)
            Dim bounds = word.GetLocalBounds
            word.OutlineThickness = fontBorderThickness
            word.OutlineColor = fontBorderColor
            word.FillColor = fontFillColor
            word.Position = destination + New Vector2f(btn.Size.X / 2 - bounds.Width / 2,
                                                       btn.Size.Y / 2 - bounds.Height / 2)


            RenderTexture(btn, destination)
            RenderDrawable(word) : word.Dispose()
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
            If sprite < 1 OrElse sprite > UBound(Items) Then Return

            Dim position = CursorPos + New Vector2f(16, 16)
            RenderTexture(Items(sprite), New FloatRect(BankFrame(index) * PicX, 0,
                                                       PicX, PicY), position)

            ' Stack Text
            Dim amount = Bank.Item(DragBankSlotNum).Amount
            If amount > 1 Then
                position += New Vector2f(-4, 22)

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
            If sprite < 1 OrElse sprite > UBound(Items) Then Return

            Dim position = CursorPos + New Vector2f(16, 16)
            RenderTexture(Items(sprite), New FloatRect(InventoryFrame(index) * PicX, 0,
                                                       PicX, PicY), position)

            ' Stack Text
            Dim amount = PlayerInv(DragInvSlotNum).Amount
            If amount > 1 Then
                position += New Vector2f(-4, 22)

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
            If index < 1 OrElse index > MAX_SKILLS Then Return

            Dim sprite = Skill(index).Icon
            If sprite < 1 OrElse sprite > UBound(Skills) Then Return

            Dim position = CursorPos + New Vector2f(16, 16)
            RenderTexture(Skills(index), position)
        End Sub

#End Region

        Friend Sub DrawActionPanel()
            RenderTexture(PanelAction, ActionPosition)
        End Sub

        Friend Sub DrawBank()
            Static timer As Integer = GetTickCount() + 100
            Dim spriteCount = UBound(Items)

            ' Panel
            RenderTexture(PanelBank, BankPosition)

            ' Header Label
            RenderText("Bank", BankPosition + New Vector2f(140, 6))

            ' Close Label
            RenderText("Close", BankPosition + New Vector2f(140, PanelBank.Size.Y - 20))

            ' Items
            If GetTickCount() > timer Then
                For i = 1 To MAX_BANK

                    ' Get Info
                    Dim index = Bank.Item(i).Index
                    If index < 1 OrElse index > MAX_ITEMS Then Continue For

                    Dim sprite = Item(index).Pic
                    If sprite < 1 OrElse sprite > spriteCount Then Continue For

                    ' Calculate Frame
                    Dim maxFrames = Items(sprite).Size.X / PicX - 1
                    If BankFrame(i) < maxFrames Then
                        BankFrame(i) += 1
                    Else
                        BankFrame(i) = 0
                    End If

                    ' Render Item
                    Dim source As New FloatRect(BankFrame(i) * PicX, 0, PicX, PicY)
                    Dim position = BankPosition + BankItemPosition
                    Dim modIndex = i - 1
                    position += New Vector2f(BankOffset.X * (modIndex Mod BankColumns),
                                             BankOffset.Y * (modIndex \ BankColumns))
                    RenderTexture(Items(sprite), source, position)

                    ' Stack Text
                    DrawStackText(position, Bank.Item(i).Amount)

                Next : timer = GetTickCount() + 100
            End If
        End Sub

        Friend Sub DrawDescription(type As DescriptionType)
            Dim position = CursorPos + New Vector2f(16, 16)

            ' Do placement adjustments
            If position.X + PanelDescription.Size.X > Viewport.Right Then
                position.X -= PanelDescription.Size.X - 16
            End If
            If position.Y + PanelDescription.Size.Y > Viewport.Bottom Then
                position.Y -= PanelDescription.Size.Y - 16
            End If

            ' Panel
            RenderTexture(PanelDescription, position)

            ' Setup Text Object
            Dim text = CreateDefaultText()
            text.Position = position + New Vector2f(10, 12)
            text.Color = DescriptionColor
            Dim textOffset = New Vector2f(0, 15)

            ' Type of description
            Select Case type
                Case DescriptionType.Item

                    ' Name
                    For Each str As String In WordWrap(ItemDescName, 22,
                                                       WrapMode.Characters,
                                                       WrapType.BreakWord)
                        text.DisplayedString = str
                        text.Position += textOffset
                        RenderDrawable(text)
                    Next

                    If Not (ShiftDown OrElse VbKeyShift) Then

                        ' Description
                        text.Color = New Color(255, 255, 255) ' White
                        For Each str As String In WordWrap(ItemDescDescription, 22,
                                                           WrapMode.Characters,
                                                           WrapType.BreakWord)
                            text.DisplayedString = str
                            text.Position += textOffset
                            RenderDrawable(text)
                        Next

                    Else

                        text.Color = New Color(200, 200, 200) ' Light Gray

                        ' Info
                        text.DisplayedString = ItemDescInfo
                        text.Position += textOffset
                        RenderDrawable(text)

                        ' Speed
                        text.DisplayedString = "Speed: " & ItemDescSpeed
                        text.Position += textOffset
                        RenderDrawable(text)

                        ' Level
                        text.DisplayedString = "Level required: " & ItemDescLevel
                        text.Position += textOffset
                        RenderDrawable(text)

                        ' Bonuses Label
                        text.DisplayedString = "=Bonuses="
                        text.Position += textOffset
                        RenderDrawable(text)

                        ' Strength
                        text.DisplayedString = "Strenght: " & ItemDescStr
                        text.Position += textOffset
                        RenderDrawable(text)

                        ' Vitality
                        text.DisplayedString = "Vitality: " & ItemDescVit
                        text.Position += textOffset
                        RenderDrawable(text)

                        ' Intelligence
                        text.DisplayedString = "Intelligence: " & ItemDescInt
                        text.Position += textOffset
                        RenderDrawable(text)

                        ' Endurance
                        text.DisplayedString = "Endurance: " & ItemDescEnd
                        text.Position += textOffset
                        RenderDrawable(text)

                        ' Luck
                        text.DisplayedString = "Luck: " & ItemDescLuck
                        text.Position += textOffset
                        RenderDrawable(text)

                        ' Spirit
                        text.DisplayedString = "Spirit: " & ItemDescSpr
                        text.Position += textOffset
                        RenderDrawable(text)

                    End If

                Case DescriptionType.Skill

                    ' Name
                    For Each str As String In WordWrap(SkillDescName, 22,
                                                       WrapMode.Characters,
                                                       WrapType.BreakWord)
                        text.DisplayedString = str
                        text.Position += textOffset
                        RenderDrawable(text)
                    Next : text.Color = New Color(255, 255, 255) ' White

                    ' Info
                    text.DisplayedString = SkillDescInfo
                    text.Position += textOffset
                    RenderDrawable(text)

                    ' Cast time
                    text.DisplayedString = "Cast Time: " & SkillDescCastTime
                    text.Position += textOffset
                    RenderDrawable(text)

                    ' Cool Down time
                    text.DisplayedString = "Cool Down: " & SkillDescCoolDown
                    text.Position += textOffset
                    RenderDrawable(text)

                    ' Damage
                    text.DisplayedString = "Damage: " & SkillDescDamage
                    text.Position += textOffset
                    RenderDrawable(text)

                    ' AOE
                    text.DisplayedString = "AOE: " & SkillDescAoe
                    text.Position += textOffset
                    RenderDrawable(text)

                    ' Range
                    text.DisplayedString = "Range: " & SkillDescRange
                    text.Position += textOffset
                    RenderDrawable(text)

                    ' Requirements
                    text.DisplayedString = "=Requirements="
                    text.Position += textOffset
                    RenderDrawable(text)

                    ' Mp
                    text.DisplayedString = "Mp: " & SkillDescReqMp
                    text.Position += textOffset
                    RenderDrawable(text)

                    ' Level
                    text.DisplayedString = "Level: " & SkillDescReqLvl
                    text.Position += textOffset
                    RenderDrawable(text)

                    ' Access
                    text.DisplayedString = "Access: " & SkillDescReqAccess
                    text.Position += textOffset
                    RenderDrawable(text)

                    ' Class
                    text.DisplayedString = "Class: " & SkillDescReqClass
                    text.Position += textOffset
                    RenderDrawable(text)

            End Select
        End Sub

        Friend Sub DrawDoors()

            Dim tick = GetTickCount()
            Dim frameSize = Door.Size.X / 4

            For y = Viewport.Top To Viewport.Bottom
                For x = Viewport.Left To Viewport.Right

                    ' Make sure theres a door here
                    If Not IsValidMapPoint(x, y) OrElse
                       Map.Tile Is Nothing OrElse
                       Map.Tile(x, y).Type <> TileType.Door Then Continue For

                    ' Cache for lookup values
                    Dim tile = TempTile(x, y)

                    ' Process animation
                    If tile.DoorAnimate = 1 Then ' Door is opening

                        If tile.DoorTimer < tick Then
                            If tile.DoorFrame < 4 Then
                                TempTile(x, y).DoorFrame += 1
                            Else
                                TempTile(x, y).DoorAnimate = 2 ' Set to closing
                            End If
                        End If

                    ElseIf tile.DoorAnimate = 2 Then ' Door is closing

                        If tile.DoorTimer < GetTickCount() Then
                            If tile.DoorFrame > 1 Then
                                TempTile(x, y).DoorFrame -= 1
                            Else
                                TempTile(x, y).DoorAnimate = 0 ' Set to stop
                            End If
                        End If

                    End If

                    ' Update timer
                    TempTile(x, y).DoorTimer = tick + 100

                    ' Render
                    RenderTexture(Door, New FloatRect(tile.DoorFrame * frameSize, 0,
                                                      frameSize, Door.Size.Y),
                                  MapPosition(x, y - Door.Size.Y / 2))

                Next
            Next

        End Sub

        Friend Sub DrawFurniture(layer As Integer)
            If FurnitureCount < 1 Then Return

            For i = 1 To FurnitureCount
                Dim furnitureInstance = C_Housing.Furniture(i)
                Dim position = New Vector2f(furnitureInstance.X,
                                            furnitureInstance.Y)

                If Not IsValidMapPoint(position) Then Continue For

                Dim itemInstance = Item(furnitureInstance.ItemNum)
                If itemInstance.Type <> ItemType.Furniture Then Continue For

                Dim sprite = itemInstance.Data2
                If sprite < 1 OrElse sprite > UBound(Furniture) Then Continue For

                Dim sizeX = itemInstance.FurnitureWidth
                Dim sizeY = itemInstance.FurnitureHeight
                If sizeX > 4 Then sizeX = 4
                If sizeY > 4 Then sizeY = 4

                Dim width = furnitureInstance.X * PicX
                Dim height = furnitureInstance.Y * PicY - sizeY * PicY

                For y = 0 To sizeY - 1
                    For x = 0 To sizeX - 1
                        If Not itemInstance.FurnitureFringe(x, y) = layer Then Continue For

                        position = New Vector2f(width + x * PicX, height + y * PicY)
                        position = MapPosition(position)
                        RenderTexture(Furniture(i), New FloatRect(x * PicX, y * PicY,
                                                                  PicX, PicY), position)
                    Next
                Next
            Next
        End Sub

        Friend Sub DrawHud()
            ' Panel
            RenderTexture(PanelHud, HudPosition)

            ' Face
            If Player(Myindex).Sprite <= UBound(Faces) Then
                RenderTexture(Faces(Player(Myindex).Sprite), HudFacePosition)
            End If

            '###################
            '###  Stat Bars  ###
            '###################

            ' Hp
            Dim valueLeft = GetPlayerVital(Myindex, VitalType.HP)
            Dim valueRight = GetPlayerMaxVital(Myindex, VitalType.HP)
            Dim position = HudPosition + HudHpPosition

            RenderTexture(BarHp, New FloatRect(0, 0, BarHp.Size.X * (valueLeft / valueRight),
                                               BarHp.Size.Y), position)
            RenderText(valueLeft & "/" & valueRight, position + New Vector2f(65, 4))

            ' Mp
            valueLeft = GetPlayerVital(Myindex, VitalType.MP)
            valueRight = GetPlayerMaxVital(Myindex, VitalType.MP)
            position = HudPosition + HudMpPosition

            RenderTexture(BarMp, New FloatRect(0, 0, BarMp.Size.X * (valueLeft / valueRight),
                                               BarMp.Size.Y), position)
            RenderText(valueLeft & "/" & valueRight, position + New Vector2f(65, 4))


            ' Exp
            valueLeft = GetPlayerExp(Myindex)
            valueRight = NextlevelExp
            position = HudPosition + HudExpPosition

            RenderTexture(BarMp, New FloatRect(0, 0, BarExp.Size.X * (valueLeft / valueRight),
                                               BarExp.Size.Y), position)
            RenderText(valueLeft & "/" & valueRight, position + New Vector2f(65, 4))

            '######################
            '###  General Text  ###
            '######################

            ' Fps
            position = HudPosition + HudHpPosition + New Vector2f(10, 4)
            RenderText(Language.Game.Fps & Fps, position)

            ' Ping
            position = HudPosition + HudMpPosition + New Vector2f(10, 4)
            RenderText(Language.Game.Ping & PingToDraw, position)

            ' Time
            position = HudPosition + HudExpPosition + New Vector2f(10, 4)
            RenderText(Language.Game.Time & Time.Instance.ToString("h:mm"), position)

            ' Lps
            position = HudPosition + HudExpPosition + New Vector2f(10, 20)
            RenderText(Language.Game.Lps & Lps, position)

            ' Draw map name
            position = New Vector2f(110, 70)
            Select Case Map.Moral
                Case MapMoralType.None
                    RenderText(Language.Game.MapName & Map.Name, position, Color.Red)
                Case MapMoralType.Safe
                    RenderText(Language.Game.MapName & Map.Name, position, Color.Green)
                Case Else
                    RenderText(Language.Game.MapName & Map.Name, position, Color.White)
            End Select
        End Sub

        Friend Sub DrawInventory()
            Static timer As Integer = GetTickCount() + 100
            Dim spriteCount = UBound(Items)

            ' Panel
            RenderTexture(PanelInventory, InventoryPosition)

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
                    Dim maxFrames = Items(sprite).Size.X / PicX - 1
                    If InventoryFrame(i) < maxFrames Then
                        InventoryFrame(i) += 1
                    Else
                        InventoryFrame(i) = 0
                    End If

                    ' Render Item
                    Dim source As New FloatRect(InventoryFrame(i) * PicX, 0, PicX, PicY)
                    Dim position = InventoryPosition + InventoryItemPosition
                    Dim modIndex = i - 1
                    position += New Vector2f(InventoryOffset.X * (modIndex Mod InvColumns),
                                             InventoryOffset.Y * (modIndex \ InvColumns))

                    If isTraded Then ' Render grayed out if traded
                        RenderTexture(Items(sprite), source, position, ColorUnavailable)
                    Else
                        RenderTexture(Items(sprite), source, position)
                    End If

                    ' Stack Text
                    DrawStackText(position, PlayerInv(i).Amount)

                Next : timer = GetTickCount() + 100
            End If
        End Sub

        Friend Sub DrawMenu()

            ' Cache position
            Dim position = MenuPosition

            ' Panel
            RenderTexture(PanelMenu, MenuPosition)
            position.X += PanelMenu.Size.X / 2 ' Get center of menu

            ' Trade
            Dim text = "Invite to trade"
            Dim textOffset = position + New Vector2f(-GetTextWidth(text) \ 2, 35)
            RenderText(text, textOffset)

            ' Party
            text = "Invite to party"
            textOffset = position + New Vector2f(-GetTextWidth(text) \ 2, 60)
            RenderText(text, textOffset)

            ' House
            text = "Invite to house"
            textOffset = position + New Vector2f(-GetTextWidth(text) \ 2, 85)
            RenderText(text, textOffset)

        End Sub

        Friend Sub DrawSkills()
            Dim spriteCount = UBound(Skills)

            ' Panel
            RenderTexture(PanelSkill, New Vector2f(SkillWindowX, SkillWindowY))

            ' Skills
            For i = 1 To MAX_PLAYER_SKILLS

                ' Get Info
                Dim index = PlayerSkills(i)
                If index < 1 OrElse index > MAX_SKILLS Then Continue For

                Dim sprite = Skill(index).Icon
                If sprite < 1 OrElse sprite > spriteCount Then Continue For

                ' Render Item
                Dim position As New Vector2f(SkillWindowX + SkillLeft, SkillWindowY + SkillTop)
                position += New Vector2f((SkillOffsetX + 32) * ((i - 1) Mod SkillColumns),
                                        (SkillOffsetY + 32) * ((i - 1) \ SkillColumns))

                If SkillCd(i) > 0 Then ' On Cooldown
                    RenderTexture(Skills(sprite), position, ColorUnavailable)
                Else ' Not on cooldown
                    RenderTexture(Skills(sprite), position)
                End If

            Next
        End Sub

        Friend Sub DrawStackText(position As Vector2f, amount As Long)
            If amount > 1 Then
                position += New Vector2f(-4, 22)

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

    End Module
End Namespace