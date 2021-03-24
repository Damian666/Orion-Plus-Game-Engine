Imports System.IO
Imports System.Windows.Forms
Imports Asfw
Imports SFML.Graphics
Imports SFML.Window

Friend Module C_Housing

#Region "Globals & Types"

    Friend MaxHouses As Integer = 100

    Friend FurnitureCount As Integer
    Friend FurnitureHouse As Integer
    Friend FurnitureSelected As Integer
    Friend HouseTileindex As Integer

    Friend House() As HouseRec
    Friend HouseConfig() As HouseRec
    Friend Furniture() As FurnitureRec
    Friend NumFurniture As Integer
    Friend HouseChanged(MaxHouses) As Boolean
    Friend HouseEdit As Boolean

    Structure HouseRec
        Dim ConfigName As String
        Dim BaseMap As Integer
        Dim Price As Integer
        Dim MaxFurniture As Integer
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Structure FurnitureRec
        Dim ItemNum As Integer
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Structure PlayerHouseRec
        Dim Houseindex As Integer
        Dim FurnitureCount As Integer
        Dim Furniture() As FurnitureRec
    End Structure

#End Region

#Region "Editor"

    Friend Sub HouseEditorInit()

        If frmEditor_House.Visible = False Then Exit Sub

        Editorindex = frmEditor_House.lstIndex.SelectedIndex + 1

        With House(Editorindex)
            frmEditor_House.txtName.Text = Trim$(.ConfigName)
            If .BaseMap = 0 Then .BaseMap = 1
            frmEditor_House.nudBaseMap.Value = .BaseMap
            If .X = 0 Then .X = 1
            frmEditor_House.nudX.Value = .X
            If .Y = 0 Then .Y = 1
            frmEditor_House.nudY.Value = .Y
            frmEditor_House.nudPrice.Value = .Price
            frmEditor_House.nudFurniture.Value = .MaxFurniture
        End With

        HouseChanged(Editorindex) = True

    End Sub

    Friend Sub HouseEditorCancel()

        Editor = 0
        frmEditor_House.Dispose()

        ClearChanged_House()

    End Sub

    Friend Sub HouseEditorOk()
        Dim i As Integer, Buffer As ByteStream, count As Integer
        Buffer = New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CSaveHouses)

        For i = 1 To MaxHouses
            If HouseChanged(i) Then count = count + 1
        Next

        Buffer.WriteInt32(count)

        If count > 0 Then
            For i = 1 To MaxHouses
                If HouseChanged(i) Then
                    Buffer.WriteInt32(i)
                    Buffer.WriteString((Trim$(House(i).ConfigName)))
                    Buffer.WriteInt32(House(i).BaseMap)
                    Buffer.WriteInt32(House(i).X)
                    Buffer.WriteInt32(House(i).Y)
                    Buffer.WriteInt32(House(i).Price)
                    Buffer.WriteInt32(House(i).MaxFurniture)
                End If
            Next
        End If

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
        frmEditor_House.Dispose()
        Editor = 0

        ClearChanged_House()

    End Sub

    Friend Sub ClearChanged_House()

        For i = 1 To MaxHouses
            HouseChanged(i) = Nothing
        Next i

        ReDim HouseChanged(MaxHouses)
    End Sub

#End Region

#Region "Incoming Packets"

    Sub Packet_HouseConfigurations(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        For i = 1 To MaxHouses
            HouseConfig(i).ConfigName = buffer.ReadString
            HouseConfig(i).BaseMap = buffer.ReadInt32
            HouseConfig(i).MaxFurniture = buffer.ReadInt32
            HouseConfig(i).Price = buffer.ReadInt32
        Next

        buffer.Dispose()

    End Sub

    Sub Packet_HouseOffer(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32

        buffer.Dispose()

        DialogType = DialogueTypeBuyhome
        If HouseConfig(i).MaxFurniture > 0 Then
            ' ask to buy house
            DialogMsg1 = "Would you like to buy the house: " & Trim$(HouseConfig(i).ConfigName)
            DialogMsg2 = "Cost: " & HouseConfig(i).Price
            DialogMsg3 = "Furniture Limit: " & HouseConfig(i).MaxFurniture
        Else
            DialogMsg1 = "Would you like to buy the house: " & Trim$(HouseConfig(i).ConfigName)
            DialogMsg2 = "Cost: " & HouseConfig(i).Price
            DialogMsg3 = "Furniture Limit: None."
        End If

        UpdateDialog = True

        buffer.Dispose()

    End Sub

    Sub Packet_Visit(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32

        DialogType = DialogueTypeVisit

        DialogMsg1 = "You have been invited to visit " & Trim$(GetPlayerName(i)) & "'s house."
        DialogMsg2 = ""
        DialogMsg3 = ""

        buffer.Dispose()

        UpdateDialog = True

    End Sub

    Sub Packet_Furniture(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        FurnitureHouse = buffer.ReadInt32
        FurnitureCount = buffer.ReadInt32

        ReDim Furniture(FurnitureCount)
        If FurnitureCount > 0 Then
            For i = 1 To FurnitureCount
                Furniture(i).ItemNum = buffer.ReadInt32
                Furniture(i).X = buffer.ReadInt32
                Furniture(i).Y = buffer.ReadInt32
            Next
        End If

        buffer.Dispose()

    End Sub

    Sub Packet_EditHouses(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        Dim i As Integer
        For i = 1 To MaxHouses
            With House(i)
                .ConfigName = Trim$(buffer.ReadString)
                .BaseMap = buffer.ReadInt32
                .X = buffer.ReadInt32
                .Y = buffer.ReadInt32
                .Price = buffer.ReadInt32
                .MaxFurniture = buffer.ReadInt32
            End With
        Next

        HouseEdit = True

        buffer.Dispose()

    End Sub

#End Region

#Region "Outgoing Packets"

    Friend Sub SendRequestEditHouse()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestEditHouse)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Friend Sub SendBuyHouse(accepted As Byte)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CBuyHouse)
        buffer.WriteInt32(accepted)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendInvite(name As String)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CVisit)
        buffer.WriteString((name))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendVisit(accepted As Byte)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CAcceptVisit)
        buffer.WriteInt32(accepted)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

#End Region

#Region "Drawing"

    Friend Sub CheckFurniture()
        Dim i As Integer
        i = 1

        While File.Exists(Path.Graphics & "Furniture\" & i & GfxExt)
            NumFurniture = NumFurniture + 1
            i = i + 1
        End While

        If NumFurniture = 0 Then Exit Sub
    End Sub

    Friend Sub DrawFurniture(index As Integer, layer As Integer)
        Dim i As Integer, itemNum As Integer
        Dim x As Integer, y As Integer, width As Integer, height As Integer, x1 As Integer, y1 As Integer

        itemNum = Furniture(index).ItemNum

        If Item(itemNum).Type <> ItemType.Furniture Then Exit Sub

        i = Item(itemNum).Data2

        If FurnitureGfxInfo(i).IsLoaded = False Then
            LoadTexture(i, 10)
        End If

        'seeying we still use it, lets update timer
        With FurnitureGfxInfo(i)
            .TextureTimer = GetTickCount() + 100000
        End With

        width = Item(itemNum).FurnitureWidth
        height = Item(itemNum).FurnitureHeight

        If width > 4 Then width = 4
        If height > 4 Then height = 4
        If i <= 0 OrElse i > NumFurniture Then Exit Sub

        ' make sure it's not out of map
        If Furniture(index).X > Map.MaxX Then Exit Sub
        If Furniture(index).Y > Map.MaxY Then Exit Sub

        For x1 = 0 To width - 1
            For y1 = 0 To height
                If Item(Furniture(index).ItemNum).FurnitureFringe(x1, y1) = layer Then
                    ' Set base x + y, then the offset due to size
                    x = (Furniture(index).X * 32) + (x1 * 32)
                    y = (Furniture(index).Y * 32 - (height * 32)) + (y1 * 32)
                    x = ConvertMapX(x)
                    y = ConvertMapY(y)

                    Dim tmpSprite As Sprite = New Sprite(FurnitureGfx(i)) With {
                        .TextureRect = New IntRect(0 + (x1 * 32), 0 + (y1 * 32), 32, 32),
                        .Position = New Vector2f(x, y)
                    }
                    GameWindow.Draw(tmpSprite)
                End If
            Next
        Next

    End Sub

#End Region

End Module