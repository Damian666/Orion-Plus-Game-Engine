Imports System.IO
Imports ASFW
Imports SFML.Graphics
Imports SFML.Window

Friend Module E_Housing

#Region "Globals & Types"

    Friend MAX_HOUSES As Integer = 100

    Friend FurnitureCount As Integer
    Friend FurnitureHouse As Integer
    Friend FurnitureSelected As Integer
    Friend HouseTileindex As Integer

    Friend House() As HouseRec
    Friend HouseConfig() As HouseRec
    Friend Furniture() As FurnitureRec
    Friend NumFurniture As Integer
    Friend House_Changed(MAX_HOUSES) As Boolean
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

#Region "Incoming Packets"

    Sub Packet_HouseConfigurations(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)

        For i = 1 To MAX_HOUSES
            HouseConfig(i).ConfigName = buffer.ReadString
            HouseConfig(i).BaseMap = buffer.ReadInt32
            HouseConfig(i).MaxFurniture = buffer.ReadInt32
            HouseConfig(i).Price = buffer.ReadInt32
        Next
        buffer.Dispose()

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
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        For i = 1 To MAX_HOUSES
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
        Dim buffer As ByteStream

        buffer = New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditHouse)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub

    Friend Sub SendBuyHouse(Accepted As Byte)
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CBuyHouse)
        buffer.WriteInt32(Accepted)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Friend Sub SendInvite(Name As String)
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CVisit)
        buffer.WriteString((Name))
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Friend Sub SendVisit(Accepted As Byte)
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CAcceptVisit)
        buffer.WriteInt32(Accepted)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

#End Region

#Region "Editor"

    Friend Sub HouseEditorInit()

        If FrmHouse.Visible = False Then Exit Sub

        Editorindex = FrmHouse.lstIndex.SelectedIndex + 1

        With House(Editorindex)
            FrmHouse.txtName.Text = Trim$(.ConfigName)
            If .BaseMap = 0 Then .BaseMap = 1
            FrmHouse.nudBaseMap.Value = .BaseMap
            If .X = 0 Then .X = 1
            FrmHouse.nudX.Value = .X
            If .Y = 0 Then .Y = 1
            FrmHouse.nudY.Value = .Y
            FrmHouse.nudPrice.Value = .Price
            FrmHouse.nudFurniture.Value = .MaxFurniture
        End With

        House_Changed(Editorindex) = True

    End Sub

    Friend Sub HouseEditorCancel()

        Editor = 0
        FrmHouse.Dispose()

        ClearChanged_House()

    End Sub

    Friend Sub HouseEditorOk()
        Dim i As Integer, Buffer As ByteStream, count As Integer
        Buffer = New ByteStream(4)

        Buffer.WriteInt32(EditorPackets.SaveHouses)

        For i = 1 To MAX_HOUSES
            If House_Changed(i) Then count = count + 1
        Next

        Buffer.WriteInt32(count)

        If count > 0 Then
            For i = 1 To MAX_HOUSES
                If House_Changed(i) Then
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
        FrmHouse.Dispose()
        Editor = 0

        ClearChanged_House()

    End Sub

    Friend Sub ClearChanged_House()

        For i = 1 To MAX_HOUSES
            House_Changed(i) = Nothing
        Next i

        ReDim House_Changed(MAX_HOUSES)
    End Sub

#End Region

#Region "Drawing"

    Friend Sub CheckFurniture()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "Furniture\" & i & GFX_EXT)
            NumFurniture = NumFurniture + 1
            i = i + 1
        End While

        If NumFurniture = 0 Then Exit Sub
    End Sub

    Friend Sub DrawFurniture(index As Integer, Layer As Integer)
        Dim i As Integer, ItemNum As Integer
        Dim X As Integer, Y As Integer, Width As Integer, Height As Integer, X1 As Integer, Y1 As Integer

        ItemNum = Furniture(index).ItemNum

        If Item(ItemNum).Type <> ItemType.Furniture Then Exit Sub

        i = Item(ItemNum).Data2

        If FurnitureGFXInfo(i).IsLoaded = False Then
            LoadTexture(i, 10)
        End If

        'seeying we still use it, lets update timer
        With SkillIconsGFXInfo(i)
            .TextureTimer = GetTickCount() + 100000
        End With

        Width = Item(ItemNum).FurnitureWidth
        Height = Item(ItemNum).FurnitureHeight

        If Width > 4 Then Width = 4
        If Height > 4 Then Height = 4
        If i <= 0 OrElse i > NumFurniture Then Exit Sub

        ' make sure it's not out of map
        If Furniture(index).X > Map.MaxX Then Exit Sub
        If Furniture(index).Y > Map.MaxY Then Exit Sub

        For X1 = 0 To Width - 1
            For Y1 = 0 To Height
                If Item(Furniture(index).ItemNum).FurnitureFringe(X1, Y1) = Layer Then
                    ' Set base x + y, then the offset due to size
                    X = (Furniture(index).X * 32) + (X1 * 32)
                    Y = (Furniture(index).Y * 32 - (Height * 32)) + (Y1 * 32)
                    X = ConvertMapX(X)
                    Y = ConvertMapY(Y)

                    Dim tmpSprite As Sprite = New Sprite(FurnitureGFX(i)) With {
                        .TextureRect = New IntRect(0 + (X1 * 32), 0 + (Y1 * 32), 32, 32),
                        .Position = New Vector2f(X, Y)
                    }
                    GameWindow.Draw(tmpSprite)
                End If
            Next
        Next

    End Sub

#End Region

End Module