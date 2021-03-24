Imports Ini = Asfw.IO.TextFile

Module C_AutoMap
    ' Automapper System
    ' Version: 1.0
    ' Author: Lucas Tardivo (boasfesta)
    ' Map analysis and tips: Richard Johnson, Luan Meireles (Alenzinho)

#Region "Globals And Types"

    Friend MapStart As Integer = 1
    Friend MapSize As Integer = 4
    Friend MapX As Integer = 50
    Friend MapY As Integer = 50

    Friend SandBorder As Integer = 4
    Friend DetailFreq As Integer = 10
    Friend ResourceFreq As Integer = 20

    Friend DetailsChecked As Boolean = True
    Friend PathsChecked As Boolean = True
    Friend RiversChecked As Boolean = True
    Friend MountainsChecked As Boolean = True
    Friend OverGrassChecked As Boolean = True
    Friend ResourcesChecked As Boolean = True

    Enum TilePrefab
        Water = 1
        Sand
        Grass
        Passing
        Overgrass
        River
        Mountain
        Count
    End Enum

    'Distance between mountains and the map limit, so the player can walk freely when teleport between maps
    Private Const MountainBorder As Byte = 5

    Friend Tile(TilePrefab.Count - 1) As TileStruct
    Friend Detail() As DetailRec
    Friend ResourcesNum As String
    Private Resources() As String
    'Private ActualMap As Integer

    Enum MountainTile
        UpLeftBorder = 0
        UpMidBorder
        UpRightBorder
        MidLeftBorder
        Middle
        MidRightBorder
        BottomLeftBorder
        BottomMidBorder
        BottomRightBorder
        LeftBody
        MiddleBody
        RightBody
        LeftFoot
        MiddleFoot
        RightFoot
    End Enum

    Enum MapPrefab
        Undefined = 0
        UpLeftQuarter
        UpBorder
        UpRightQuarter
        RightBorder
        DownRightQuarter
        BottomBorder
        DownLeftQuarter
        LeftBorder
        Common
    End Enum

    Structure DetailRec
        Dim DetailBase As Byte
        Dim Tile As TileStruct
    End Structure

    Structure MapOrientationRec
        Dim Prefab As Integer
        Dim TileStartX As Integer
        Dim TileStartY As Integer
        Dim TileEndX As Integer
        Dim TileEndY As Integer
        Dim Tile(,) As TilePrefab
    End Structure

#End Region

#Region "Loading Functions"

    Sub OpenAutomapper()
        LoadTilePrefab()
        frmEditor_AutoMapper.Visible = True
    End Sub

    Sub LoadTilePrefab()
        Dim Prefab As Integer, Layer As Integer
        Dim cf = Path.Contents & "AutoMapper.ini"

        ReDim Tile(TilePrefab.Count - 1)
        For Prefab = 1 To TilePrefab.Count - 1

            ReDim Tile(Prefab).Layer(LayerType.Count - 1)
            For Layer = 1 To LayerType.Count - 1
                Tile(Prefab).Layer(Layer).Tileset = Val(Ini.Read(cf, "Prefab" & Prefab, "Layer" & Layer & "Tileset"))
                Tile(Prefab).Layer(Layer).X = Val(Ini.Read(cf, "Prefab" & Prefab, "Layer" & Layer & "X"))
                Tile(Prefab).Layer(Layer).Y = Val(Ini.Read(cf, "Prefab" & Prefab, "Layer" & Layer & "Y"))
                Tile(Prefab).Layer(Layer).AutoTile = Val(Ini.Read(cf, "Prefab" & Prefab, "Layer" & Layer & "Autotile"))
            Next
            Tile(Prefab).Type = Val(Ini.Read(cf, "Prefab" & Prefab, "Type"))
        Next

        ResourcesNum = Ini.Read(cf, "Resources", "ResourcesNum")
        Resources = Split(ResourcesNum, ";")
    End Sub

    Sub LoadDetail(Prefab As TilePrefab, Tileset As Integer, X As Integer, Y As Integer, Optional TileType As Integer = 0, Optional EndX As Integer = 0, Optional EndY As Integer = 0)
        If EndX = 0 Then EndX = X
        If EndY = 0 Then EndY = Y

        Dim pX As Integer, pY As Integer
        For pX = X To EndX
            For pY = Y To EndY
                AddDetail(Prefab, Tileset, pX, pY, TileType)
            Next pY
        Next pX

    End Sub

    Sub AddDetail(Prefab As TilePrefab, Tileset As Integer, X As Integer, Y As Integer, TileType As Integer)
        Dim DetailCount As Integer
        DetailCount = UBound(Detail) + 1

        ReDim Preserve Detail(DetailCount)
        ReDim Preserve Detail(DetailCount).Tile.Layer(LayerType.Count - 1)

        Detail(DetailCount).DetailBase = Prefab
        Detail(DetailCount).Tile.Type = TileType
        Detail(DetailCount).Tile.Layer(3).Tileset = Tileset
        Detail(DetailCount).Tile.Layer(3).X = X
        Detail(DetailCount).Tile.Layer(3).Y = Y
    End Sub

    Sub LoadDetails()
        ReDim Detail(1)

        'Detail config area
        'Use: LoadDetail TilePrefab, Tileset, StartTilesetX, StartTilesetY, TileType, EndTilesetX, EndTilesetY
        LoadDetail(TilePrefab.Grass, 9, 0, 0, TileType.None, 7, 7)
        LoadDetail(TilePrefab.Grass, 9, 0, 10, TileType.None, 6, 15)
        LoadDetail(TilePrefab.Grass, 9, 0, 13, TileType.None, 7, 14)
        LoadDetail(TilePrefab.Sand, 10, 0, 13, TileType.None, 7, 14)
        LoadDetail(TilePrefab.Sand, 11, 0, 0, TileType.None, 1, 1)
    End Sub

#End Region

#Region "Incoming Packets"

    Sub Packet_AutoMapper(ByRef data() As Byte)
        Dim Layer As Integer
        Dim buffer As New Asfw.ByteStream(data)
        MapStart = buffer.ReadInt32
        MapSize = buffer.ReadInt32
        MapX = buffer.ReadInt32
        MapY = buffer.ReadInt32
        SandBorder = buffer.ReadInt32
        DetailFreq = buffer.ReadInt32
        ResourceFreq = buffer.ReadInt32

        Dim cf = Path.Contents & "AutoMapper.ini"
        Ini.Write(cf, "Resources", "ResourcesNum", buffer.ReadString())

        For Prefab = 1 To TilePrefab.Count - 1
            ReDim Tile(Prefab).Layer(LayerType.Count - 1)

            Layer = buffer.ReadInt32()
            Ini.Write(cf, "Prefab" & Prefab, "Layer" & Layer & "Tileset", buffer.ReadInt32)
            Ini.Write(cf, "Prefab" & Prefab, "Layer" & Layer & "X", buffer.ReadInt32)
            Ini.Write(cf, "Prefab" & Prefab, "Layer" & Layer & "Y", buffer.ReadInt32)
            Ini.Write(cf, "Prefab" & Prefab, "Layer" & Layer & "Autotile", buffer.ReadInt32)

            Ini.Write(cf, "Prefab" & Prefab, "Type", buffer.ReadInt32)
        Next

        buffer.Dispose()

        InitAutoMapper = True

    End Sub

#End Region

#Region "Outgoing Packets"

    Friend Sub SendRequestAutoMapper()
        Dim buffer As New Asfw.ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestAutoMap)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendSaveAutoMapper()
        Dim cf = Path.Contents & "AutoMapper.ini"
        Dim buffer As New Asfw.ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSaveAutoMap)

        buffer.WriteInt32(MapStart)
        buffer.WriteInt32(MapSize)
        buffer.WriteInt32(MapX)
        buffer.WriteInt32(MapY)
        buffer.WriteInt32(SandBorder)
        buffer.WriteInt32(DetailFreq)
        buffer.WriteInt32(ResourceFreq)

        'send xml info
        buffer.WriteString((Ini.Read(cf, "Resources", "ResourcesNum")))

        For Prefab = 1 To TilePrefab.Count - 1
            For Layer = 1 To LayerType.Count - 1
                If Val(Ini.Read(cf, "Prefab" & Prefab, "Layer" & Layer & "Tileset")) > 0 Then
                    buffer.WriteInt32(Layer)
                    buffer.WriteInt32(Val(Ini.Read(cf, "Prefab" & Prefab, "Layer" & Layer & "Tileset")))
                    buffer.WriteInt32(Val(Ini.Read(cf, "Prefab" & Prefab, "Layer" & Layer & "X")))
                    buffer.WriteInt32(Val(Ini.Read(cf, "Prefab" & Prefab, "Layer" & Layer & "Y")))
                    buffer.WriteInt32(Val(Ini.Read(cf, "Prefab" & Prefab, "Layer" & Layer & "Autotile")))
                End If
            Next
            buffer.WriteInt32(Val(Ini.Read(cf, "Prefab" & Prefab, "Type")))
        Next

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

#End Region

End Module