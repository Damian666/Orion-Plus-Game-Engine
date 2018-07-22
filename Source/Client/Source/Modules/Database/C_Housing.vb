Imports System.IO
Imports System.Windows.Forms
Imports ASFW
Imports SFML.Graphics
Imports SFML.Window

Friend Module C_Housing
#Region "Globals & Types"
    Friend MaxHouses As Integer = 100

    Friend FurnitureCount As Integer
    Friend FurnitureHouse As Integer
    Friend FurnitureSelected As Integer
    Friend HouseTileindex as integer

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
        Dim Houseindex as integer
        Dim FurnitureCount As Integer
        Dim Furniture() As FurnitureRec
    End Structure
#End Region

#Region "Incoming Packets"
    Sub Packet_HouseConfigurations(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        For i = 1 To MaxHouses
            HouseConfig(i).ConfigName = buffer.ReadString
            HouseConfig(i).BaseMap = Buffer.ReadInt32
            HouseConfig(i).MaxFurniture = Buffer.ReadInt32
            HouseConfig(i).Price = Buffer.ReadInt32
        Next

        Buffer.Dispose()

    End Sub

    Sub Packet_HouseOffer(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32

        Buffer.Dispose()

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

        Buffer.Dispose()

    End Sub

    Sub Packet_Visit(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32

        DialogType = DialogueTypeVisit

        DialogMsg1 = "You have been invited to visit " & Trim$(GetPlayerName(i)) & "'s house."
        DialogMsg2 = ""
        DialogMsg3 = ""

        Buffer.Dispose()

        UpdateDialog = True

    End Sub

    Sub Packet_Furniture(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        FurnitureHouse = Buffer.ReadInt32
        FurnitureCount = Buffer.ReadInt32

        ReDim Furniture(FurnitureCount)
        If FurnitureCount > 0 Then
            For i = 1 To FurnitureCount
                Furniture(i).ItemNum = Buffer.ReadInt32
                Furniture(i).X = Buffer.ReadInt32
                Furniture(i).Y = Buffer.ReadInt32
            Next
        End If

        Buffer.Dispose()

    End Sub

    Sub Packet_EditHouses(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        Dim i As Integer
        For i = 1 To MaxHouses
            With House(i)
                .ConfigName = Trim$(Buffer.ReadString)
                .BaseMap = Buffer.ReadInt32
                .X = Buffer.ReadInt32
                .Y = Buffer.ReadInt32
                .Price = Buffer.ReadInt32
                .MaxFurniture = Buffer.ReadInt32
            End With
        Next

        HouseEdit = True

        Buffer.Dispose()

    End Sub
#End Region

#Region "Outgoing Packets"
    Friend Sub SendRequestEditHouse()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(EditorPackets.RequestEditHouse)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()

    End Sub

    Friend Sub SendBuyHouse(accepted As Byte)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CBuyHouse)
        Buffer.WriteInt32(Accepted)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Friend Sub SendInvite(name As String)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CVisit)
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

        While File.Exists(Application.StartupPath & GfxPath & "Furniture\" & i & GfxExt)
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
        With SkillIconsGfxInfo(i)
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