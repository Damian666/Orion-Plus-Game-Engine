Imports System.Drawing
Imports Asfw

Friend Module C_HotBar
    Friend SelHotbarSlot As Integer
    Friend SelSkillSlot As Boolean

    Friend Const MaxHotbar As Byte = 7

    'hotbar constants
    Friend Const HotbarTop As Byte = 2

    Friend Const HotbarLeft As Byte = 2
    Friend Const HotbarOffsetX As Byte = 2

    Friend Structure HotbarRec
        Dim Slot As Integer
        Dim SType As Byte
    End Structure

    Friend Function IsHotBarSlot(x As Single, y As Single) As Integer
        Dim tempRec As Rect
        Dim i As Integer

        IsHotBarSlot = 0

        For i = 1 To MaxHotbar
            With tempRec
                .Top = HotbarY + HotbarTop
                .Bottom = .Top + PicY
                .Left = HotbarX + HotbarLeft + ((HotbarOffsetX + 32) * (((i - 1) Mod MaxHotbar)))
                .Right = .Left + PicX
            End With

            If x >= tempRec.Left AndAlso x <= tempRec.Right Then
                If y >= tempRec.Top AndAlso y <= tempRec.Bottom Then
                    IsHotBarSlot = i
                    Exit Function
                End If
            End If
        Next

    End Function

    Friend Sub Packet_Hotbar(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        For i = 1 To MaxHotbar
            Player(Myindex).Hotbar(i).Slot = buffer.ReadInt32
            Player(Myindex).Hotbar(i).SType = buffer.ReadInt32
        Next

        buffer.Dispose()
    End Sub

    Friend Sub SendSetHotbarSlot(slot As Integer, num As Integer, type As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSetHotbarSlot)

        buffer.WriteInt32(slot)
        buffer.WriteInt32(num)
        buffer.WriteInt32(type)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendDeleteHotbar(slot As Integer)
        Dim buffer As New ByteStream(4)
        buffer.WriteInt32(ClientPackets.CDeleteHotbarSlot)

        buffer.WriteInt32(slot)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendUseHotbarSlot(slot As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CUseHotbarSlot)

        buffer.WriteInt32(slot)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub DrawHotbar()
        Dim i As Integer, num As Integer, pic As Integer
        Dim rec As Rectangle, recPos As Rectangle

        RenderSprite(HotBarSprite, GameWindow, HotbarX, HotbarY, 0, 0, HotBarGfxInfo.Width, HotBarGfxInfo.Height)

        For i = 1 To MaxHotbar
            If Player(Myindex).Hotbar(i).SType = 1 Then
                num = PlayerSkills(Player(Myindex).Hotbar(i).Slot)

                If num > 0 Then
                    pic = Skill(num).Icon

                    If SkillIconsGfxInfo(pic).IsLoaded = False Then
                        LoadTexture(pic, 9)
                    End If

                    'seeying we still use it, lets update timer
                    With SkillIconsGfxInfo(pic)
                        .TextureTimer = GetTickCount() + 100000
                    End With

                    With rec
                        .Y = 0
                        .Height = 32
                        .X = 0
                        .Width = 32
                    End With

                    If Not SkillCd(i) = 0 Then
                        rec.X = 32
                        rec.Width = 32
                    End If

                    With recPos
                        .Y = HotbarY + HotbarTop
                        .Height = PicY
                        .X = HotbarX + HotbarLeft + ((HotbarOffsetX + 32) * (((i - 1))))
                        .Width = PicX
                    End With

                    RenderSprite(SkillIconsSprite(pic), GameWindow, recPos.X, recPos.Y, rec.X, rec.Y, rec.Width, rec.Height)
                End If

            ElseIf Player(Myindex).Hotbar(i).SType = 2 Then
                num = PlayerInv(Player(Myindex).Hotbar(i).Slot).Num

                If num > 0 Then
                    pic = Item(num).Pic

                    If ItemsGfxInfo(pic).IsLoaded = False Then
                        LoadTexture(pic, 4)
                    End If

                    'seeying we still use it, lets update timer
                    With ItemsGfxInfo(pic)
                        .TextureTimer = GetTickCount() + 100000
                    End With

                    With rec
                        .Y = 0
                        .Height = 32
                        .X = 0
                        .Width = 32
                    End With

                    With recPos
                        .Y = HotbarY + HotbarTop
                        .Height = PicY
                        .X = HotbarX + HotbarLeft + ((HotbarOffsetX + 32) * (((i - 1))))
                        .Width = PicX
                    End With

                    RenderSprite(ItemsSprite(pic), GameWindow, recPos.X, recPos.Y, rec.X, rec.Y, rec.Width, rec.Height)
                End If
            End If
        Next

    End Sub

End Module