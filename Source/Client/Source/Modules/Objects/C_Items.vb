Imports System.IO
Imports System.Windows.Forms
Imports Asfw

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

    Friend Sub CheckItems()
        Dim i As Integer
        i = 1

        While File.Exists(Path.Graphics & "Items\" & i & GfxExt)
            NumItems = NumItems + 1
            i = i + 1
        End While

        If NumItems = 0 Then Exit Sub
    End Sub

    Friend Sub ClearItem(index As Integer)
        Item(index) = Nothing
        Item(index) = New ItemStruct
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

    Sub ClearItems()
        Dim i As Integer

        ReDim Item(MAX_ITEMS)

        For i = 1 To MAX_ITEMS
            ClearItem(i)
        Next

    End Sub

#End Region

#Region "Incoming Packets"

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

        buffer.WriteInt32(ClientPackets.CRequestItems)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

#End Region

End Module