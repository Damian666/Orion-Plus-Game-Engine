Imports Asfw
Imports Ini = Asfw.IO.TextFile

Module S_AutoMap
    ' Automapper System
    ' Version: 1.0
    ' Author: Lucas Tardivo (boasfesta)
    ' Map analysis and tips: Richard Johnson, Luan Meireles (Alenzinho)

#Region "Globals And Types"

    Private _mapOrientation() As MapOrientationRec

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
    Private _resources() As String

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

    ''' <summary>
    ''' Loads TilePrefab from the Automapper.ini
    ''' </summary>
    Sub LoadTilePrefab()
        Dim prefab As Integer, layer As Integer
        Dim cf = Path.Database & "AutoMapper.ini"

        ReDim Tile(TilePrefab.Count - 1)
        For prefab = 1 To TilePrefab.Count - 1

            ReDim Tile(prefab).Layer(LayerType.Count - 1)
            For layer = 1 To LayerType.Count - 1
                Tile(prefab).Layer(layer).Tileset = Val(Ini.Read(cf, "Prefab" & prefab, "Layer" & layer & "Tileset"))
                Tile(prefab).Layer(layer).X = Val(Ini.Read(cf, "Prefab" & prefab, "Layer" & layer & "X"))
                Tile(prefab).Layer(layer).Y = Val(Ini.Read(cf, "Prefab" & prefab, "Layer" & layer & "Y"))
                Tile(prefab).Layer(layer).AutoTile = Val(Ini.Read(cf, "Prefab" & prefab, "Layer" & layer & "Autotile"))
            Next layer
            Tile(prefab).Type = Val(Ini.Read(cf, "Prefab" & prefab, "Type"))
        Next prefab

        ResourcesNum = Ini.Read(cf, "Resources", "ResourcesNum")
        _resources = Split(ResourcesNum, ";")

    End Sub

    ''' <summary>
    ''' Load details to the rec
    ''' </summary>
    ''' <param name="prefab">Which TilePrefab to use.</param>
    ''' <param name="tileset">Tileset number to use.</param>
    ''' <param name="x">The X coordinate, where the tiles start on the tilesheet.</param>
    ''' <param name="y">The Y coordinate, where the tiles start on the tilesheet.</param>
    ''' <param name="tileType">Which TileType to use, if any, blocked, None, etc</param>
    ''' <param name="endX">The X coordinate, where the tiles end on the tilesheet.</param>
    ''' <param name="endY">The Y coordinate, where the tiles end on the tilesheet.</param>
    Sub LoadDetail(prefab As TilePrefab, tileset As Integer, x As Integer, y As Integer, Optional tileType As Integer = 0, Optional endX As Integer = 0, Optional endY As Integer = 0)
        If endX = 0 Then endX = x
        If endY = 0 Then endY = y

        Dim pX As Integer, pY As Integer
        For pX = x To endX
            For pY = y To endY
                AddDetail(prefab, tileset, pX, pY, tileType)
            Next
        Next

    End Sub

    ''' <summary>
    ''' Load details to memory for mapping.
    ''' </summary>
    ''' <param name="prefab">Which TilePrefab to use.</param>
    ''' <param name="tileset">Tileset number to use.</param>
    ''' <param name="x">The X coordinate, where the tiles start on the tilesheet.</param>
    ''' <param name="y">The Y coordinate, where the tiles start on the tilesheet.</param>
    ''' <param name="tileType">Which TileType to use, if any, blocked, None, etc.</param>
    Sub AddDetail(prefab As TilePrefab, tileset As Integer, x As Integer, y As Integer, tileType As Integer)
        Dim detailCount As Integer

        detailCount = UBound(Detail) + 1

        ReDim Preserve Detail(detailCount)
        ReDim Preserve Detail(detailCount).Tile.Layer(LayerType.Count - 1)

        Detail(detailCount).DetailBase = prefab
        Detail(detailCount).Tile.Type = tileType
        Detail(detailCount).Tile.Layer(LayerType.Mask2).Tileset = tileset
        Detail(detailCount).Tile.Layer(LayerType.Mask2).X = x
        Detail(detailCount).Tile.Layer(LayerType.Mask2).Y = y
    End Sub

    ''' <summary>
    ''' Here a user can define which details to add
    ''' </summary>
    Sub LoadDetails()
        ReDim Detail(1)

        'Detail config area
        'Use: LoadDetail TilePrefab, Tileset, StartTilesetX, StartTilesetY, TileType, EndTilesetX, EndTilesetY
        LoadDetail(TilePrefab.Grass, 9, 0, 0, TileType.None, 9, 15)

        LoadDetail(TilePrefab.Sand, 10, 0, 13, TileType.None, 7, 14)
        LoadDetail(TilePrefab.Sand, 11, 0, 0, TileType.None, 1, 1)
    End Sub

    ''' <summary>
    ''' Check for details
    ''' </summary>
    ''' <param name="prefab"></param>
    ''' <returns></returns>
    Function HaveDetails(prefab As TilePrefab) As Boolean
        HaveDetails = Not (prefab = TilePrefab.Water OrElse prefab = TilePrefab.River)
    End Function

#End Region

#Region "Incoming Packets"

    Sub Packet_RequestAutoMap(index As Integer, ByRef data() As Byte)
        AddDebug("Recieved EMSG: RequestAutoMap")

        If GetPlayerAccess(index) = AdminType.Player Then Exit Sub

        SendAutoMapper(index)
    End Sub

    Sub Packet_SaveAutoMap(index As Integer, ByRef data() As Byte)
        Dim Layer As Integer
        Dim buffer As New ByteStream(data)
        Dim cf = Path.Database & "AutoMapper.ini"

        AddDebug("Recieved EMSG: SaveAutoMap")

        If GetPlayerAccess(index) = AdminType.Player Then Exit Sub

        MapStart = buffer.ReadInt32
        MapSize = buffer.ReadInt32
        MapX = buffer.ReadInt32
        MapY = buffer.ReadInt32
        SandBorder = buffer.ReadInt32
        DetailFreq = buffer.ReadInt32
        ResourceFreq = buffer.ReadInt32

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

        StartAutomapper(MapStart, MapSize, MapX, MapY)

    End Sub

#End Region

#Region "Outgoing Packets"

    Sub SendAutoMapper(index As Integer)
        Dim buffer As ByteStream, Prefab As Integer
        Dim cf = Path.Database & "AutoMapper.ini"
        buffer = New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SAutoMapper)

        AddDebug("Sent SMSG: SAutoMapper")

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

        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

#End Region

#Region "Creation Functions"

    Sub AddTile(prefab As TilePrefab, mapNum As Integer, x As Integer, y As Integer)
        Dim tileDest As TileStruct
        Dim cleanNextTiles As Boolean
        Dim i As Integer

        'DetailFreq = Val(frmAutoMapper.txtDetail.Text)

        tileDest = Map(mapNum).Tile(x, y)
        tileDest.Type = Tile(prefab).Type

        ReDim Preserve tileDest.Layer(LayerType.Count - 1)

        For i = 1 To LayerType.Count - 1
            If Tile(prefab).Layer(i).Tileset <> 0 OrElse cleanNextTiles Then
                tileDest.Layer(i) = Tile(prefab).Layer(i)
                If Not HaveDetails(prefab) Then cleanNextTiles = True
            End If
        Next i

        If prefab = TilePrefab.Grass OrElse prefab = TilePrefab.Sand Then
            If DetailsChecked = True Then
                If Random(1, DetailFreq) = 1 Then
                    Dim detailNum As Integer
                    Dim details() As Integer
                    ReDim details(1)
                    For i = 1 To UBound(Detail)
                        If Detail(i).DetailBase = prefab Then
                            ReDim Preserve details(UBound(details) + 1)
                            details(UBound(details)) = i
                        End If
                    Next i
                    If UBound(details) > 1 Then
                        detailNum = details(Random(2, UBound(details)))
                        If Detail(detailNum).DetailBase = prefab Then
                            tileDest.Layer(3) = Detail(detailNum).Tile.Layer(3)
                            tileDest.Type = Detail(detailNum).Tile.Type
                        End If
                    End If
                End If
            End If
        End If

        Map(mapNum).Tile(x, y) = tileDest
        _mapOrientation(mapNum).Tile(x, y) = prefab
    End Sub

    Function CanPlaceResource(mapNum As Integer, X As Integer, Y As Integer) As Boolean
        If _mapOrientation(mapNum).Tile(X, Y) = TilePrefab.Grass OrElse _mapOrientation(mapNum).Tile(X, Y) = TilePrefab.Overgrass OrElse (_mapOrientation(mapNum).Tile(X, Y) = TilePrefab.Mountain AndAlso Not Map(mapNum).Tile(X, Y).Type = TileType.Blocked) Then
            CanPlaceResource = True
        End If
    End Function

    Function CanPlaceOvergrass(mapNum As Integer, X As Integer, Y As Integer) As Boolean
        If _mapOrientation(mapNum).Tile(X, Y) = TilePrefab.Grass OrElse (_mapOrientation(mapNum).Tile(X, Y) = TilePrefab.Mountain AndAlso Not Map(mapNum).Tile(X, Y).Type = TileType.Blocked) Then
            CanPlaceOvergrass = True
        End If
    End Function

    Sub MakeResource(mapNum As Integer)
        Dim x As Integer, y As Integer

        For x = 1 To Map(mapNum).MaxX - 1
            For y = 1 To Map(mapNum).MaxY - 1
                If CanPlaceResource(mapNum, x, y) AndAlso CanPlaceResource(mapNum, x - 1, y) AndAlso CanPlaceResource(mapNum, x + 1, y) AndAlso CanPlaceResource(mapNum, x, y - 1) AndAlso CanPlaceResource(mapNum, x, y + 1) Then
                    Dim resourceNum As Integer

                    If _resources(0) Is "" Then Exit Sub

                    If Random(1, ResourceFreq) = 1 Then
                        resourceNum = Val(_resources(Random(1, UBound(_resources))))
                        Map(mapNum).Tile(x, y).Type = TileType.Resource
                        Map(mapNum).Tile(x, y).Data1 = resourceNum
                    End If
                End If
            Next y
        Next x
    End Sub

    Sub MakeResources(mapStart As Integer, size As Integer)
        Dim i As Integer
        Dim totalMaps As Integer
        Dim tick As Integer

        Console.WriteLine("Working...")
        Application.DoEvents()
        tick = GetTimeMs()
        totalMaps = size * size

        For i = mapStart To mapStart + totalMaps - 1
            MakeResource(i)
            CacheResources(i)
        Next i

        tick = GetTimeMs() - tick
        Console.WriteLine("Done and cached resources in " & CDbl(tick / 1000) & "s")
        Application.DoEvents()
    End Sub

    Sub MakeOvergrasses(mapStart As Integer, size As Integer)
        Dim i As Integer
        Dim totalMaps As Integer
        Dim tick As Integer

        Console.WriteLine("Working...")
        Application.DoEvents()
        tick = GetTimeMs()
        totalMaps = size * size

        For i = mapStart To mapStart + totalMaps - 1
            MakeOvergrass(i)
        Next i

        tick = GetTimeMs() - tick
        Console.WriteLine("Done overgrasses in " & CDbl(tick / 1000) & "s")
        Application.DoEvents()
    End Sub

    Sub MakeOvergrass(mapNum As Integer)
        Dim startX As Integer, startY As Integer
        Dim totalOvergrass As Integer
        'Dim MapSize As Integer
        Dim x As Integer, y As Integer
        Dim grassCount As Integer
        Dim overgrassCount As Integer
        Dim nextDir As Integer
        Dim brushSize As Integer
        Dim foundBorder As Boolean

        With Map(mapNum)
            For x = 0 To .MaxX
                For y = 0 To .MaxY
                    If CanPlaceOvergrass(mapNum, x, y) Then
                        grassCount = grassCount + 1
                    End If
                Next y
            Next x

            totalOvergrass = Random(Int(grassCount / 100), Int(grassCount / 50))

            Do Until overgrassCount >= totalOvergrass
                Dim totalWalk As Integer
                brushSize = Random(1, 2)
                startX = Random(0, .MaxX)
                startY = Random(0, .MaxY)

                If CanPlaceOvergrass(mapNum, startX, startY) Then
                    PaintOvergrass(mapNum, startX, startY, brushSize, brushSize)
                    x = startX
                    y = startY
                    totalWalk = 1
                    nextDir = 0
                    Do While nextDir <> 5 AndAlso totalWalk < 15
                        If foundBorder Then
                            nextDir = Random(0, 5)
                        Else
                            nextDir = Random(0, 4)
                        End If
                        Select Case nextDir
                            Case DirectionType.Up : y = y - 1
                            Case DirectionType.Down : y = y + 1
                            Case DirectionType.Left : x = x - 1
                            Case DirectionType.Right : x = x + 1
                        End Select
                        If nextDir < 5 Then
                            If x > 0 AndAlso x < .MaxX Then
                                If y > 0 AndAlso y < .MaxY Then
                                    If CanPlaceOvergrass(mapNum, x, y) Then
                                        brushSize = Random(0, 2)
                                        PaintOvergrass(mapNum, x, y, brushSize, brushSize)
                                        totalWalk = totalWalk + 1
                                        foundBorder = True
                                    Else
                                        If _mapOrientation(mapNum).Tile(x, y) = TilePrefab.Overgrass Then
                                            foundBorder = False
                                        Else
                                            nextDir = 5
                                        End If
                                    End If
                                Else
                                    nextDir = 5
                                End If
                            Else
                                nextDir = 5
                            End If
                        End If
                    Loop
                    overgrassCount = overgrassCount + 1
                End If
            Loop

        End With

    End Sub

    Sub PaintOvergrass(mapNum As Integer, x As Integer, y As Integer, brushSizeX As Integer, brushSizeY As Integer)
        Dim pX As Integer, pY As Integer

        For pX = x - brushSizeX To x + brushSizeX
            For pY = y - brushSizeY To y + brushSizeY
                If pX >= 0 AndAlso pX <= Map(mapNum).MaxX Then
                    If pY >= 0 AndAlso pY <= Map(mapNum).MaxY Then
                        If _mapOrientation(mapNum).Tile(pX, pY) <> TilePrefab.Overgrass Then
                            If CanPlaceOvergrass(mapNum, pX, pY) Then
                                If Random(1, 100) >= 50 Then
                                    AddTile(TilePrefab.Overgrass, mapNum, pX, pY)
                                End If
                            End If
                        End If
                    End If
                End If
            Next pY
        Next pX
    End Sub

    Sub PaintTile(prefab As TilePrefab, mapNum As Integer, x As Integer, y As Integer, brushSizeX As Integer, brushSizeY As Integer, Optional humanMade As Boolean = True, Optional onlyTo As TilePrefab = 0)
        Dim pX As Integer, pY As Integer
        For pX = x - brushSizeX To x + brushSizeX
            For pY = y - brushSizeY To y + brushSizeY
                If pX >= 0 AndAlso pX <= Map(mapNum).MaxX Then
                    If pY >= 0 AndAlso pY <= Map(mapNum).MaxY Then
                        If _mapOrientation(mapNum).Tile(pX, pY) <> prefab Then
                            Dim canPaint As Boolean
                            canPaint = True
                            If onlyTo <> 0 Then
                                If _mapOrientation(mapNum).Tile(pX, pY) <> onlyTo Then canPaint = False
                            End If
                            If canPaint Then
                                If humanMade Then
                                    AddTile(prefab, mapNum, pX, pY)
                                Else
                                    If Random(1, 100) >= 50 Then
                                        AddTile(prefab, mapNum, pX, pY)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next pY
        Next pX
    End Sub

    Sub PaintRiver(mapNum As Integer, X As Integer, Y As Integer, riverDir As Byte, riverWidth As Integer)
        Dim pX As Integer, pY As Integer
        If riverDir = DirectionType.Down Then
            For pX = X - riverWidth To X + riverWidth
                If pX > 0 AndAlso pX < Map(mapNum).MaxX Then
                    AddTile(TilePrefab.River, mapNum, pX, Y)
                End If
            Next pX
        End If
        If riverDir = DirectionType.Left OrElse riverDir = DirectionType.Right Then
            For pY = Y - riverWidth To Y + riverWidth
                If pY > 0 AndAlso pY < Map(mapNum).MaxY Then
                    AddTile(TilePrefab.River, mapNum, X, pY)
                End If
            Next pY
        End If
    End Sub

    Sub MakeRivers(mapStart As Integer, size As Integer)
        'Dim RiverType As Integer
        Dim riverMap As Integer
        Dim totalRivers As Integer
        Dim totalMaps As Integer
        Dim madeRivers As Integer
        Dim riverX As Integer, riverY As Integer
        Dim riverWidth As Integer, riverHeight As Integer
        Dim riverDir As Byte
        Dim riverBorder As Integer
        Dim riverFlowWidth As Integer
        Dim riverEnd As Boolean
        Dim riverSteps As Integer
        Dim tick As Integer

        Console.WriteLine("Working...")
        Application.DoEvents()
        tick = GetTimeMs()
        riverBorder = 4
        madeRivers = 0
        totalMaps = size * size
        totalRivers = Int(totalMaps / 4)

        Do While madeRivers <= totalRivers
            'Start river
SelectMap:
            riverMap = Random(mapStart, mapStart + totalMaps - 1)
            If _mapOrientation(riverMap).Prefab <> MapPrefab.Common Then GoTo SelectMap
            riverX = Random(riverBorder, Map(riverMap).MaxX - riverBorder)
            riverY = Random(riverBorder, Map(riverMap).MaxY - riverBorder)
            riverWidth = Random(2, 3)
            riverHeight = Random(2, 3)
            AddTile(TilePrefab.River, riverMap, riverX, riverY)
            PaintTile(TilePrefab.River, riverMap, riverX, riverY, riverWidth, riverHeight, False)
            riverDir = Random(1, 3)
            riverEnd = False
            riverSteps = 0
            riverFlowWidth = Random(1, 3)
            Do Until _mapOrientation(riverMap).Tile(riverX, riverY) <> TilePrefab.River
                If riverDir = DirectionType.Left Then
                    riverX = riverX - 1
                    If riverX < 0 Then
                        riverX = 0
                        riverDir = DirectionType.Right
                    End If
                End If
                If riverDir = DirectionType.Down Then
                    riverY = riverY + 1
                    If riverY > Map(riverMap).MaxY Then
                        riverY = Map(riverMap).MaxY
                        riverDir = Random(2, 3)
                    End If
                End If
                If riverDir = DirectionType.Right Then
                    riverX = riverX + 1
                    If riverX > Map(riverMap).MaxX Then
                        riverX = Map(riverMap).MaxX
                        riverDir = DirectionType.Left
                    End If
                End If
            Loop
            Do While Not riverEnd
                riverSteps = riverSteps + 1
                If riverX < 0 Then riverX = 0
                If riverX > Map(riverMap).MaxX Then riverX = Map(riverMap).MaxX
                If riverY < 0 Then riverY = 0
                If riverY > Map(riverMap).MaxY Then riverY = Map(riverMap).MaxY
                PaintRiver(riverMap, riverX, riverY, riverDir, riverFlowWidth)
                Select Case riverDir
                    Case DirectionType.Left : riverY = riverY + Random(-1, 1)
                    Case DirectionType.Down : riverX = riverX + Random(-1, 1)
                    Case DirectionType.Right : riverY = riverY + Random(-1, 1)
                End Select

                If Random(1, 100) < 5 Then 'Change dir
                    riverDir = Random(1, 3)
                End If
                Select Case riverDir
                    Case DirectionType.Left : riverX = riverX - 1
                    Case DirectionType.Down : riverY = riverY + 1
                    Case DirectionType.Right : riverX = riverX + 1
                End Select
                If riverDir = DirectionType.Down Then
                    If _mapOrientation(Map(riverMap).Down).Prefab = MapPrefab.Common Then
                        If riverY > Map(riverMap).MaxY Then
                            riverMap = Map(riverMap).Down
                            riverY = 0
                        End If
                    Else
                        If riverY > Map(riverMap).MaxY / 2 Then
                            PaintTile(TilePrefab.River, riverMap, riverX, riverY, Random(2, 3), Random(3, 4), False)
                            riverEnd = True
                        End If
                    End If
                End If
                If riverDir = DirectionType.Left Then
                    If _mapOrientation(Map(riverMap).Left).Prefab = MapPrefab.Common Then
                        If riverX < 0 Then
                            'MapCache_Create RiverMap
                            riverMap = Map(riverMap).Left
                            riverX = Map(riverMap).MaxX
                        End If
                    Else
                        If riverX < Map(riverMap).MaxX / 2 Then
                            PaintTile(TilePrefab.River, riverMap, riverX, riverY, Random(2, 3), Random(3, 4), False)
                            riverEnd = True
                        End If
                    End If
                End If
                If riverDir = DirectionType.Right Then
                    If _mapOrientation(Map(riverMap).Right).Prefab = MapPrefab.Common Then
                        If riverX > Map(riverMap).MaxX Then
                            'MapCache_Create RiverMap
                            riverMap = Map(riverMap).Right
                            riverX = 0
                        End If
                    Else
                        If riverX > Map(riverMap).MaxX / 2 Then
                            PaintTile(TilePrefab.River, riverMap, riverX, riverY, Random(2, 3), Random(3, 4), False)
                            riverEnd = True
                        End If
                    End If
                End If
            Loop
            madeRivers = madeRivers + 1
        Loop

        tick = GetTimeMs() - tick
        Console.WriteLine("Done " & totalRivers & " rivers in " & CDbl(tick / 1000) & "s")
        Application.DoEvents()
    End Sub

    Sub PlaceMountain(mapNum As Integer, x As Integer, y As Integer, mountainPrefab As MountainTile)
        Dim oldX As Integer, oldY As Integer

        oldX = Tile(TilePrefab.Mountain).Layer(2).X
        oldY = Tile(TilePrefab.Mountain).Layer(2).Y
        Tile(TilePrefab.Mountain).Layer(2).X = oldX + (mountainPrefab Mod 3)
        Tile(TilePrefab.Mountain).Layer(2).Y = oldY + (Int(mountainPrefab / 3))
        AddTile(TilePrefab.Mountain, mapNum, x, y)
        Tile(TilePrefab.Mountain).Layer(2).X = oldX
        Tile(TilePrefab.Mountain).Layer(2).Y = oldY
    End Sub

    Sub MarkMountain(mapNum As Integer, x As Integer, y As Integer, width As Integer, height As Integer)
        Dim pX As Integer, pY As Integer
        For pX = x - Int(width / 2) To x + Int(width / 2)
            For pY = y - Int(height / 2) To y + Int(height / 2)
                If pX > MountainBorder AndAlso pX < Map(mapNum).MaxX - MountainBorder Then
                    If pY > MountainBorder AndAlso pY < Map(mapNum).MaxY - MountainBorder Then
                        _mapOrientation(mapNum).Tile(pX, pY) = TilePrefab.Mountain
                    End If
                End If
            Next pY
        Next pX
    End Sub

    Sub MakeMapMountains(mapNum As Integer)
        Dim mountainMinAreaWidth As Integer, MountainMinAreaHeight As Integer
        Dim mountainMinSize As Integer, MountainMinArea As Integer
        Dim mountainSize As Integer
        Dim x As Integer, y As Integer
        'Dim ScanX As Integer, ScanY As Integer
        'Dim FoundPlace As Boolean
        Dim totalGrass As Integer
        Dim totalMountains As Integer
        Dim i As Integer
        Dim positionTries As Integer
        Dim mountainSteps As Integer
        mountainMinAreaWidth = 5
        MountainMinAreaHeight = 5
        MountainMinArea = 4
        mountainMinSize = 10

        For x = MountainBorder To Map(mapNum).MaxX - MountainBorder
            For y = MountainBorder To Map(mapNum).MaxY - MountainBorder
                If _mapOrientation(mapNum).Tile(x, y) = TilePrefab.Grass Then
                    totalGrass = totalGrass + 1
                End If
            Next y
        Next x

        totalMountains = Random(0, (totalGrass / (mountainMinAreaWidth * MountainMinAreaHeight)))

        If totalMountains > 0 Then
            For i = 1 To totalMountains
                positionTries = 0
Retry:
                If positionTries < 5 Then
                    x = Random(mountainMinAreaWidth + MountainBorder, Map(mapNum).MaxX - mountainMinAreaWidth - MountainBorder)
                    y = Random(MountainMinAreaHeight + MountainBorder, Map(mapNum).MaxY - MountainMinAreaHeight - MountainBorder)
                    If Not ValidMountainPosition(mapNum, x, y, mountainMinAreaWidth, MountainMinAreaHeight) Then
                        positionTries = positionTries + 1
                        GoTo Retry
                    End If
                    MarkMountain(mapNum, x, y, mountainMinAreaWidth, MountainMinAreaHeight)

                    mountainSteps = 0
                    mountainSize = Random(mountainMinSize, mountainMinSize * (totalMountains / i))

                    Do While mountainSteps < mountainSize
                        Dim OldX As Integer, OldY As Integer
                        OldX = x
                        OldY = y
                        x = x + (Random(0, 2) - 1)
                        y = y + (Random(0, 2) - 1)
                        If ValidMountainPosition(mapNum, x, y, 3, 5) Then
                            MarkMountain(mapNum, x, y, 3, 5)
                        Else
                            'Return
                            x = OldX
                            y = OldY
                        End If
                        mountainSteps = mountainSteps + 1
                    Loop

                End If
            Next i

            'Fill Mountain
            For x = MountainBorder To Map(mapNum).MaxX - MountainBorder
                For y = MountainBorder To Map(mapNum).MaxY - MountainBorder
                    If _mapOrientation(mapNum).Tile(x, y) = TilePrefab.Mountain Then
                        Dim mountainPrefab As MountainTile
                        mountainPrefab = GetMountainPrefab(mapNum, x, y)
                        'Exceptions
                        If Not _mapOrientation(mapNum).Tile(x, y - 1) <> TilePrefab.Mountain Then
                            If ((GetMountainPrefab(mapNum, x - 1, y) = MountainTile.MiddleFoot OrElse GetMountainPrefab(mapNum, x - 1, y) = MountainTile.LeftFoot) OrElse (GetMountainPrefab(mapNum, x - 1, y) = MountainTile.LeftBody OrElse GetMountainPrefab(mapNum, x - 1, y) = MountainTile.MiddleBody)) AndAlso Not (mountainPrefab = MountainTile.MiddleBody OrElse mountainPrefab = MountainTile.MiddleFoot OrElse mountainPrefab = MountainTile.RightBody OrElse mountainPrefab = MountainTile.RightFoot) Then
                                mountainPrefab = MountainTile.MidLeftBorder
                            End If
                            If GetMountainPrefab(mapNum, x, y + 1) = MountainTile.LeftFoot Then
                                mountainPrefab = MountainTile.LeftBody
                                GoTo Important
                            End If
                            If GetMountainPrefab(mapNum, x, y + 2) = MountainTile.LeftFoot Then
                                mountainPrefab = MountainTile.BottomLeftBorder
                                GoTo Important
                            End If
                            If ((GetMountainPrefab(mapNum, x + 1, y) = MountainTile.MiddleFoot OrElse GetMountainPrefab(mapNum, x + 1, y) = MountainTile.RightFoot) OrElse (GetMountainPrefab(mapNum, x + 1, y) = MountainTile.RightBody OrElse GetMountainPrefab(mapNum, x + 1, y) = MountainTile.MiddleBody)) AndAlso Not (mountainPrefab = MountainTile.MiddleBody OrElse mountainPrefab = MountainTile.MiddleFoot OrElse mountainPrefab = MountainTile.LeftBody OrElse mountainPrefab = MountainTile.LeftFoot) Then
                                mountainPrefab = MountainTile.MidRightBorder
                            End If
                            If GetMountainPrefab(mapNum, x, y + 1) = MountainTile.RightFoot Then
                                mountainPrefab = MountainTile.RightBody
                                GoTo Important
                            End If
                            If GetMountainPrefab(mapNum, x, y + 2) = MountainTile.RightFoot Then
                                mountainPrefab = MountainTile.BottomRightBorder
                                GoTo Important
                            End If
                        End If

Important:
                        If mountainPrefab >= 0 Then PlaceMountain(mapNum, x, y, mountainPrefab)
                    End If
                Next y
            Next x
        End If
    End Sub

    Function GetMountainPrefab(mapNum As Integer, x As Integer, y As Integer) As MountainTile
        Dim verticalPos As Byte
        Dim mountainPrefab As MountainTile
        If _mapOrientation(mapNum).Tile(x, y) = TilePrefab.Mountain Then
            verticalPos = 1
            If _mapOrientation(mapNum).Tile(x - 1, y) <> TilePrefab.Mountain Then
                verticalPos = 0
            End If
            If _mapOrientation(mapNum).Tile(x + 1, y) <> TilePrefab.Mountain Then
                verticalPos = 2
            End If
            mountainPrefab = -1
            If _mapOrientation(mapNum).Tile(x, y - 1) = TilePrefab.Mountain Then
                'Its not the top
                If y + 3 < Map(mapNum).MaxY Then
                    If _mapOrientation(mapNum).Tile(x, y + 3) <> TilePrefab.Mountain AndAlso _mapOrientation(mapNum).Tile(x, y + 2) = TilePrefab.Mountain Then
                        'Inferior
                        Select Case verticalPos
                            Case 0 : mountainPrefab = MountainTile.BottomLeftBorder
                            Case 1 : mountainPrefab = MountainTile.BottomMidBorder
                            Case 2 : mountainPrefab = MountainTile.BottomRightBorder
                        End Select
                    Else
                        If _mapOrientation(mapNum).Tile(x, y + 2) <> TilePrefab.Mountain AndAlso _mapOrientation(mapNum).Tile(x, y + 1) = TilePrefab.Mountain Then
                            'Body
                            Select Case verticalPos
                                Case 0 : mountainPrefab = MountainTile.LeftBody
                                Case 1 : mountainPrefab = MountainTile.MiddleBody
                                Case 2 : mountainPrefab = MountainTile.RightBody
                            End Select
                        Else
                            If _mapOrientation(mapNum).Tile(x, y + 1) <> TilePrefab.Mountain Then
                                'Foots
                                Select Case verticalPos
                                    Case 0 : mountainPrefab = MountainTile.LeftFoot
                                    Case 1 : mountainPrefab = MountainTile.MiddleFoot
                                    Case 2 : mountainPrefab = MountainTile.RightFoot
                                End Select
                            Else
                                'Mid
                                Select Case verticalPos
                                    Case 0 : mountainPrefab = MountainTile.MidLeftBorder
                                    Case 2 : mountainPrefab = MountainTile.MidRightBorder
                                End Select
                            End If
                        End If
                    End If
                End If
            Else
                'Its top
                Select Case verticalPos
                    Case 0 : mountainPrefab = MountainTile.UpLeftBorder
                    Case 1 : mountainPrefab = MountainTile.UpMidBorder
                    Case 2 : mountainPrefab = MountainTile.UpRightBorder
                End Select
            End If
            GetMountainPrefab = mountainPrefab
        Else
            GetMountainPrefab = -1
        End If
    End Function

    Function ValidMountainPosition(mapNum As Integer, x As Integer, y As Integer, width As Integer, height As Integer) As Boolean
        Dim pX As Integer, pY As Integer
        ValidMountainPosition = True
        For pX = x - Int(width / 2) To x + Int(width / 2)
            For pY = y - Int(height / 2) To y + Int(height / 2)
                If pX < 1 OrElse pX > Map(mapNum).MaxX - 1 Then ValidMountainPosition = False
                If pY < 1 OrElse pY >= Map(mapNum).MaxY - 3 Then ValidMountainPosition = False
                If ValidMountainPosition Then
                    If _mapOrientation(mapNum).Tile(pX, pY) <> TilePrefab.Grass AndAlso _mapOrientation(mapNum).Tile(pX, pY) <> TilePrefab.Overgrass AndAlso _mapOrientation(mapNum).Tile(pX, pY) <> TilePrefab.Mountain Then ValidMountainPosition = False
                End If
            Next pY
        Next pX
    End Function

    Sub MakeMountains(mapStart As Integer, size As Integer)
        Dim i As Integer
        Dim totalMaps As Integer
        Dim tick As Integer
        Dim mapCount As Integer
        Console.WriteLine("Working...")
        Application.DoEvents()
        tick = GetTimeMs()
        totalMaps = size * size
        mapCount = 0
        For i = mapStart To mapStart + totalMaps - 1
            If _mapOrientation(i).Prefab = MapPrefab.Common Then
                MakeMapMountains(i)
                mapCount = mapCount + 1
            End If
        Next i
        tick = GetTimeMs() - tick
        Console.WriteLine("Done mountains in " & (mapCount) & " maps in " & CDbl(tick / 1000) & "s")
        Application.DoEvents()
    End Sub

    Sub MakeMap(mapNum As Integer, prefab As MapPrefab)
        Dim x As Integer, y As Integer
        Dim tileX As Integer, tileY As Integer
        Dim tileStartX As Integer, tileStartY As Integer
        Dim tileEndX As Integer, tileEndY As Integer
        Dim change As Integer
        Dim changed As Boolean

        _mapOrientation(mapNum).Prefab = prefab

        With Map(mapNum)
            If prefab <> MapPrefab.Common Then
                For x = 0 To .MaxX
                    For y = 0 To .MaxY
                        AddTile(TilePrefab.Water, mapNum, x, y)
                    Next y
                Next x
            Else
                For x = 0 To .MaxX
                    For y = 0 To .MaxY
                        AddTile(TilePrefab.Grass, mapNum, x, y)
                    Next y
                Next x
            End If
            If prefab = MapPrefab.UpLeftQuarter Then
                tileStartX = Int(.MaxX / 2) - Random(0, Int(.MaxX / 4))
                tileStartY = .MaxY
                tileEndX = .MaxX
                tileEndY = Int(.MaxY / 2) - Random(0, Int(.MaxY / 4))
                tileX = tileStartX

                For y = tileStartY To tileEndY Step -1
                    If y <> tileStartY Then tileX = tileX + Random(0, 2)
                    If tileX >= tileEndX Then
                        tileEndY = y
                        Exit For
                    Else
                        For x = tileX To tileEndX
                            If x < tileX + SandBorder OrElse y < tileEndY + SandBorder Then
                                AddTile(TilePrefab.Sand, mapNum, x, y)
                            Else
                                AddTile(TilePrefab.Grass, mapNum, x, y)
                            End If
                        Next x
                    End If
                Next y
            End If
            If prefab = MapPrefab.UpBorder Then
                tileStartX = 0
                tileStartY = _mapOrientation(.Left).TileEndY
                tileEndX = .MaxX
                tileY = tileStartY
                changed = True
                For x = tileStartX To tileEndX
                    If changed = False Then
                        change = Random(-1, 1)
                        If change <> 0 Then
                            changed = True
                            tileY = tileY + change
                        End If
                    Else
                        changed = False
                    End If
                    If tileY < Int(.MaxY / 4) Then tileY = Int(.MaxY / 4)
                    If tileY > Int(.MaxY / 2) + Int(.MaxY / 4) Then tileY = Int(.MaxY / 2) + Int(.MaxY / 4)
                    For y = tileY To .MaxY
                        If y < tileY + SandBorder Then
                            AddTile(TilePrefab.Sand, mapNum, x, y)
                        Else
                            AddTile(TilePrefab.Grass, mapNum, x, y)
                        End If
                    Next y
                Next x

                _mapOrientation(mapNum).TileEndY = tileY
            End If
            If prefab = MapPrefab.UpRightQuarter Then
                tileStartX = Random(4, 8)
                tileStartY = _mapOrientation(.Left).TileEndY
                tileEndX = 0
                tileEndY = .MaxY

                tileX = tileStartX
                For y = tileStartY To tileEndY
                    If y <> tileStartY Then tileX = tileX + Random(0, 2)
                    If tileX > .MaxX Then tileX = .MaxX
                    For x = tileX To tileEndX Step -1
                        If x > tileX - SandBorder OrElse y < tileY + SandBorder Then
                            AddTile(TilePrefab.Sand, mapNum, x, y)
                        Else
                            AddTile(TilePrefab.Grass, mapNum, x, y)
                        End If
                    Next x
                Next y

                tileEndX = tileX
            End If
            If prefab = MapPrefab.LeftBorder Then
                If .Up = MapStart Then
                    tileStartX = _mapOrientation(.Up).TileStartX
                Else
                    tileStartX = _mapOrientation(.Up).TileEndX
                End If
                tileStartY = 0
                tileEndX = .MaxX
                tileEndY = .MaxY
                tileX = tileStartX
                changed = True
                For y = tileStartY To tileEndY
                    If changed = False Then
                        change = Random(-1, 1)
                        If change <> 0 Then
                            changed = True
                            tileX = tileX + change
                        End If
                    Else
                        changed = False
                    End If
                    If tileX < Int(.MaxX / 4) Then tileX = Int(.MaxX / 4)
                    If tileX > Int(.MaxX / 2) + Int(.MaxX / 4) Then tileX = Int(.MaxX / 2) + Int(.MaxX / 4)
                    For x = tileX To tileEndX
                        If x < tileX + SandBorder Then
                            AddTile(TilePrefab.Sand, mapNum, x, y)
                        Else
                            AddTile(TilePrefab.Grass, mapNum, x, y)
                        End If
                    Next x
                Next y

                _mapOrientation(mapNum).TileEndX = tileX
            End If
            If prefab = MapPrefab.RightBorder Then
                If .Up = MapStart Then
                    tileStartX = _mapOrientation(.Up).TileStartX
                Else
                    tileStartX = _mapOrientation(.Up).TileEndX
                End If
                tileStartY = 0
                tileEndX = .MaxX
                tileEndY = .MaxY
                tileX = tileStartX
                changed = True
                For y = tileStartY To tileEndY
                    If changed = False Then
                        change = Random(-1, 1)
                        If change <> 0 Then
                            changed = True
                            tileX = tileX + change
                        End If
                    Else
                        changed = False
                    End If
                    If tileX < Int(.MaxX / 4) Then tileX = Int(.MaxX / 4)
                    If tileX > Int(.MaxX / 2) + Int(.MaxX / 4) Then tileX = Int(.MaxX / 2) + Int(.MaxX / 4)
                    For x = tileX To 0 Step -1
                        If x > tileX - SandBorder Then
                            AddTile(TilePrefab.Sand, mapNum, x, y)
                        Else
                            AddTile(TilePrefab.Grass, mapNum, x, y)
                        End If
                    Next x
                Next y

                _mapOrientation(mapNum).TileEndX = tileX
            End If
            If prefab = MapPrefab.DownLeftQuarter Then
                tileStartX = _mapOrientation(.Up).TileEndX
                tileEndX = .MaxX
                tileStartY = 0
                tileEndY = Int(.MaxY / 2) + Random(0, Int(.MaxY / 4))

                tileX = tileStartX
                For y = tileStartY To tileEndY
                    If y <> tileStartY Then tileX = tileX + Random(0, 2)
                    If tileX >= tileEndX Then
                        tileEndY = y
                        Exit For
                    Else
                        For x = tileX To tileEndX
                            If x < tileX + SandBorder OrElse y > tileEndY - SandBorder Then
                                AddTile(TilePrefab.Sand, mapNum, x, y)
                            Else
                                AddTile(TilePrefab.Grass, mapNum, x, y)
                            End If
                        Next x
                    End If
                Next y
            End If
            If prefab = MapPrefab.BottomBorder Then
                tileStartX = 0
                tileEndX = .MaxX
                tileStartY = _mapOrientation(.Left).TileEndY

                tileY = tileStartY
                changed = True
                For x = tileStartX To tileEndX
                    If changed = False Then
                        change = Random(-1, 1)
                        If change <> 0 Then
                            changed = True
                            tileY = tileY + change
                        End If
                    Else
                        changed = False
                    End If
                    If tileY < Int(.MaxY / 4) Then tileY = Int(.MaxY / 4)
                    If tileY > Int(.MaxY / 2) + Int(.MaxY / 4) Then tileY = Int(.MaxY / 2) + Int(.MaxY / 4)
                    For y = tileY To 0 Step -1
                        If y > tileY - SandBorder Then
                            AddTile(TilePrefab.Sand, mapNum, x, y)
                        Else
                            AddTile(TilePrefab.Grass, mapNum, x, y)
                        End If
                    Next y
                Next x

                _mapOrientation(mapNum).TileEndY = tileY
            End If
            If prefab = MapPrefab.DownRightQuarter Then
                tileStartY = _mapOrientation(.Left).TileEndY
                tileEndY = 0
                tileStartX = 0
                tileEndX = _mapOrientation(.Up).TileEndX
                tileY = tileStartY

                For x = tileStartX To tileEndX
                    If x <> tileStartX Then tileY = tileY - Random(0, 1)
                    For y = tileY To tileEndY Step -1
                        If y > tileY - SandBorder OrElse x > tileEndX - SandBorder Then
                            AddTile(TilePrefab.Sand, mapNum, x, y)
                        Else
                            AddTile(TilePrefab.Grass, mapNum, x, y)
                        End If
                    Next y
                Next x
            End If
        End With

        If _mapOrientation(mapNum).TileStartX = 0 Then _mapOrientation(mapNum).TileStartX = tileStartX
        If _mapOrientation(mapNum).TileStartY = 0 Then _mapOrientation(mapNum).TileStartY = tileStartY
        If _mapOrientation(mapNum).TileEndX = 0 Then _mapOrientation(mapNum).TileEndX = tileEndX
        If _mapOrientation(mapNum).TileEndY = 0 Then _mapOrientation(mapNum).TileEndY = tileEndY

    End Sub

    Sub MakePath(mapNum As Integer, x As Integer, y As Integer, dir As Byte, Optional steps As Integer = 1)
        Dim pathEnd As Boolean
        Dim brushX As Integer, brushY As Integer
        Dim i As Byte
        If Not _mapOrientation(mapNum).Prefab = MapPrefab.Common Then Exit Sub
        pathEnd = False
        Do While Not pathEnd
            If steps Mod Map(mapNum).MaxX = 0 Then
                Dim oldDir As Integer
                oldDir = dir
ChangeDir:
                dir = Random(0, 3)
                'Avoid invert direction
                If (oldDir = DirectionType.Up AndAlso dir = DirectionType.Down) OrElse (oldDir = DirectionType.Down AndAlso dir = DirectionType.Up) OrElse (oldDir = DirectionType.Right AndAlso dir = DirectionType.Left) OrElse (oldDir = DirectionType.Left AndAlso dir = DirectionType.Right) Then GoTo ChangeDir
            End If
            Select Case dir
                Case DirectionType.Up
                    brushX = 1
                    brushY = 0
                    y = y - 1
                    x = x + Random(0, 2) - 1
                    If x <= 1 Then x = 1
                    If x >= Map(mapNum).MaxX - 1 Then x = Map(mapNum).MaxX - 1
                Case DirectionType.Down
                    brushX = 1
                    brushY = 0
                    y = y + 1
                    x = x + Random(0, 2) - 1
                    If x <= 1 Then x = 1
                    If x >= Map(mapNum).MaxX - 1 Then x = Map(mapNum).MaxX - 1
                Case DirectionType.Left
                    brushX = 0
                    brushY = 1
                    y = y + Random(0, 2) - 1
                    x = x - 1
                    If y <= 1 Then y = 1
                    If y >= Map(mapNum).MaxY - 1 Then y = Map(mapNum).MaxY - 1
                Case DirectionType.Right
                    brushX = 0
                    brushY = 1
                    y = y + Random(0, 2) - 1
                    x = x + 1
                    If y <= 1 Then y = 1
                    If y >= Map(mapNum).MaxY - 1 Then y = Map(mapNum).MaxY - 1
            End Select
            If x <= 0 Then
                If Map(mapNum).Left > 0 Then
                    If _mapOrientation(Map(mapNum).Left).Prefab = MapPrefab.Common Then
                        PaintTile(TilePrefab.Passing, mapNum, x, y, brushX, brushY, , TilePrefab.Grass)
                        PaintTile(TilePrefab.Passing, Map(mapNum).Left, Val(Map(mapNum).MaxX), y, brushX, brushY, , TilePrefab.Grass)
                        MakePath(Map(mapNum).Left, Map(mapNum).MaxX, y, dir, steps)
                    End If
                End If
                Exit Sub
            End If
            If x >= Map(mapNum).MaxX Then
                If Map(mapNum).Right > 0 Then
                    If _mapOrientation(Map(mapNum).Right).Prefab = MapPrefab.Common Then
                        PaintTile(TilePrefab.Passing, mapNum, x, y, brushX, brushY, , TilePrefab.Grass)
                        PaintTile(TilePrefab.Passing, Map(mapNum).Right, 0, y, brushX, brushY, , TilePrefab.Grass)
                        MakePath(Map(mapNum).Right, 0, y, dir, steps)
                    End If
                End If
                Exit Sub
            End If
            If y <= 0 Then
                If Map(mapNum).Up > 0 Then
                    If _mapOrientation(Map(mapNum).Up).Prefab = MapPrefab.Common Then
                        PaintTile(TilePrefab.Passing, mapNum, x, y, brushX, brushY, , TilePrefab.Grass)
                        PaintTile(TilePrefab.Passing, Map(mapNum).Up, x, Val(Map(mapNum).MaxY), brushX, brushY, , TilePrefab.Grass)
                        MakePath(Map(mapNum).Up, x, Map(mapNum).MaxY, dir, steps)
                    End If
                End If
                Exit Sub
            End If
            If y >= Map(mapNum).MaxY Then
                If Map(mapNum).Down > 0 Then
                    If _mapOrientation(Map(mapNum).Down).Prefab = MapPrefab.Common Then
                        PaintTile(TilePrefab.Passing, mapNum, x, y, brushX, brushY, , TilePrefab.Grass)
                        PaintTile(TilePrefab.Passing, Map(mapNum).Down, x, 0, brushX, brushY, , TilePrefab.Grass)
                        MakePath(Map(mapNum).Down, x, 0, dir, steps)
                    End If
                End If
                Exit Sub
            End If

            If CheckPath(mapNum, x, y, dir) Then
                PaintTile(TilePrefab.Passing, mapNum, x, y, brushX, brushY, , TilePrefab.Grass)
                steps = steps + 1
            Else
                For i = 0 To 3
                    If i <> dir Then
                        If CheckPath(mapNum, x, y, i) Then
                            dir = i
                            Exit For
                        End If
                    End If
                Next i
            End If
        Loop
    End Sub

    Function CheckPath(mapNum As Integer, X As Integer, Y As Integer, Dir As Byte) As Boolean
        Dim sizeX As Integer, sizeY As Integer
        Select Case Dir
            Case DirectionType.Up, DirectionType.Down : sizeX = 1
            Case DirectionType.Left, DirectionType.Right : sizeY = 1
        End Select

        CheckPath = True

        Dim pX As Integer, pY As Integer
        For pX = X - sizeX To X + sizeX
            For pY = Y - sizeY To Y + sizeY
                If pX >= 0 AndAlso pX <= Map(mapNum).MaxX Then
                    If pY >= 0 AndAlso pY <= Map(mapNum).MaxY Then
                        If _mapOrientation(mapNum).Tile(pX, pY) <> TilePrefab.Grass AndAlso _mapOrientation(mapNum).Tile(pX, pY) <> TilePrefab.Passing Then
                            CheckPath = False
                            Exit Function
                        End If
                    End If
                End If
            Next pY
        Next pX
    End Function

    Function SearchForPreviousPaths(mapNum As Integer, X As Integer, Y As Integer) As Boolean
        Dim pX As Integer, pY As Integer
        For pX = X - 10 To X + 10
            For pY = Y - 10 To Y + 10
                If pX >= 0 AndAlso pX <= Map(mapNum).MaxX Then
                    If pY >= 0 AndAlso pY <= Map(mapNum).MaxY Then
                        If _mapOrientation(mapNum).Tile(pX, pY) = TilePrefab.Passing Then
                            SearchForPreviousPaths = True
                            Exit Function
                        End If
                    End If
                End If
            Next pY
        Next pX
    End Function

    Sub MakeMapPaths(mapNum As Integer)
        Dim x As Integer, y As Integer
        Dim startX() As Integer = {0}, startY() As Integer = {0}
        Dim locationCount As Integer
        Dim totalPaths As Integer
        Dim maxTries As Integer
        Dim tries As Integer
        Dim tick As Integer

        Console.WriteLine("Working...")
        Application.DoEvents()
        tick = GetTimeMs()

        maxTries = 30
        totalPaths = Random(Map(mapNum).MaxX / 20, Map(mapNum).MaxX / 10)

        Do While locationCount < totalPaths AndAlso tries < maxTries
            x = Random(1, Map(mapNum).MaxX - 1)
            y = Random(1, Map(mapNum).MaxY - 1)
            If _mapOrientation(mapNum).Tile(x, y) = TilePrefab.Grass AndAlso _mapOrientation(mapNum).Tile(x + 1, y) = TilePrefab.Grass AndAlso _mapOrientation(mapNum).Tile(x, y + 1) = TilePrefab.Grass AndAlso _mapOrientation(mapNum).Tile(x + 1, y + 1) = TilePrefab.Grass Then
                If Not SearchForPreviousPaths(mapNum, x, y) Then
                    PaintTile(TilePrefab.Passing, mapNum, x, y, 1, 1, , TilePrefab.Grass)
                    locationCount = locationCount + 1
                    ReDim Preserve startX(locationCount)
                    ReDim Preserve startY(locationCount)
                    startX(locationCount) = x
                    startY(locationCount) = y
                End If
            End If
            tries = tries + 1
        Loop

        If locationCount > 0 Then
            Dim i As Integer
            Dim dir As Integer
            For i = 1 To locationCount
                If startX(i) < Map(mapNum).MaxX / 2 Then
                    If startY(i) < Map(mapNum).MaxY / 2 Then
                        If Random(1, 2) = 1 Then
                            dir = DirectionType.Left
                        Else
                            dir = DirectionType.Up
                        End If
                    Else
                        If Random(1, 2) = 1 Then
                            dir = DirectionType.Left
                        Else
                            dir = DirectionType.Down
                        End If
                    End If
                Else
                    If startY(i) < Map(mapNum).MaxY / 2 Then
                        If Random(1, 2) = 1 Then
                            dir = DirectionType.Right
                        Else
                            dir = DirectionType.Up
                        End If
                    Else
                        If Random(1, 2) = 1 Then
                            dir = DirectionType.Right
                        Else
                            dir = DirectionType.Down
                        End If
                    End If
                End If
                MakePath(mapNum, startX(i) + 1, startY(i), dir)
            Next i
        End If

        tick = GetTimeMs() - tick
        Console.WriteLine("Done " & totalPaths & " paths in " & CDbl(tick / 1000) & "s")
        Application.DoEvents()
    End Sub

    Sub MakePaths(mapStart As Integer, size As Integer)
        Dim totalMaps As Integer = size * size

        If totalMaps Mod 2 = 1 Then
            MakeMapPaths(Int(totalMaps / 2) + 1)
        Else
            MakeMapPaths(Int(totalMaps / 2) - (size / 2))
            MakeMapPaths(Int(totalMaps / 2) - (size / 2) + 1)
            MakeMapPaths(Int(totalMaps / 2) - (size / 2) + size)
            MakeMapPaths(Int(totalMaps / 2) - (size / 2) + size + 1)
        End If

    End Sub

    Sub StartAutomapper(mapStart As Integer, size As Integer, mapX As Integer, mapY As Integer)
        Dim startTick As Integer
        Dim tick As Integer
        startTick = GetTimeMs()
        LoadTilePrefab()
        LoadDetails()

        Dim mapNum As Integer
        Dim totalMaps As Integer = (size * size)

        ReDim _mapOrientation(mapStart + totalMaps)

        tick = GetTimeMs()

        For mapNum = mapStart To mapStart + totalMaps - 1
            ClearMap(mapNum)

            Map(mapNum).MaxX = mapX
            Map(mapNum).MaxY = mapY
            ReDim Map(mapNum).Tile(Map(mapNum).MaxX, Map(mapNum).MaxY)
            ReDim _mapOrientation(mapNum).Tile(Map(mapNum).MaxX, Map(mapNum).MaxY)
            ClearTempTile(mapNum)

            ' // Down teleport \\
            If mapNum <= mapStart - 1 + totalMaps - size Then
                Map(mapNum).Down = mapNum + size
            End If
            ' \\ Down teleport //

            ' // Left teleport \\
            If mapNum - mapStart + 1 Mod size <> 1 Then
                Map(mapNum).Left = mapNum - 1
            End If
            ' \\ Left teleport //

            ' // Up teleport \\
            If mapNum - mapStart + 1 > size Then
                Map(mapNum).Up = mapNum - size
            End If
            ' \\ Up teleport //

            ' // Right teleport \\
            If mapNum - mapStart + 1 Mod size <> 0 Then
                Map(mapNum).Right = mapNum + 1
            End If
            ' \\ Right teleport //

            Dim Prefab As MapPrefab
            Prefab = MapPrefab.Undefined
            If mapNum = mapStart Then
                Prefab = MapPrefab.UpLeftQuarter
            End If
            If mapNum > mapStart AndAlso mapNum < mapStart - 1 + size Then
                Prefab = MapPrefab.UpBorder
            End If
            If mapNum = mapStart - 1 + size Then
                Prefab = MapPrefab.UpRightQuarter
            End If
            If mapNum > mapStart - 1 + size AndAlso mapNum <= mapStart - 1 + totalMaps - size Then
                If (mapNum - mapStart + 1) Mod size = 1 Then
                    Prefab = MapPrefab.LeftBorder
                Else
                    If (mapNum - mapStart + 1) Mod size = 0 Then
                        Prefab = MapPrefab.RightBorder
                    Else
                        Prefab = MapPrefab.Common
                    End If
                End If
            End If
            If mapNum > mapStart - 1 + totalMaps - size Then
                If (mapNum - mapStart + 1) Mod size = 1 Then
                    Prefab = MapPrefab.DownLeftQuarter
                Else
                    If (mapNum - mapStart + 1) Mod size = 0 Then
                        Prefab = MapPrefab.DownRightQuarter
                    Else
                        Prefab = MapPrefab.BottomBorder
                    End If
                End If
            End If

            MakeMap(mapNum, Prefab)
        Next mapNum

        tick = GetTimeMs() - tick
        Console.WriteLine("Done " & totalMaps & " maps models in " & CDbl(tick / 1000) & "s")
        Application.DoEvents()

        If PathsChecked = True Then MakePaths(mapStart, size)
        If RiversChecked = True Then MakeRivers(mapStart, size)
        If MountainsChecked = True Then MakeMountains(mapStart, size)
        If OverGrassChecked = True Then MakeOvergrasses(mapStart, size)
        If ResourcesChecked = True Then MakeResources(mapStart, size)

        tick = GetTimeMs()
        Console.WriteLine("Working...")
        Application.DoEvents()

        For mapNum = mapStart To mapStart + totalMaps - 1
            SaveMap(mapNum)
            'MapCache_Create mapnum
        Next mapNum

        tick = GetTimeMs() - tick
        startTick = GetTimeMs() - startTick

        Console.WriteLine("Cached all maps in " & CDbl(tick / 1000) & "s (" & ((tick / startTick) * 100) & "%)")
        Console.WriteLine("Done " & totalMaps & " maps in " & CDbl(startTick / 1000) & "s")

    End Sub

#End Region

End Module