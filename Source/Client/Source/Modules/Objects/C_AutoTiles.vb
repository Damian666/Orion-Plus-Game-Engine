Friend Module C_AutoTiles

#Region "Globals and Types"

    ' Autotiles
    Friend Const AutoInner As Byte = 1

    Friend Const AutoOuter As Byte = 2
    Friend Const AutoHorizontal As Byte = 3
    Friend Const AutoVertical As Byte = 4
    Friend Const AutoFill As Byte = 5

    ' Autotile types
    Friend Const AutotileNone As Byte = 0

    Friend Const AutotileNormal As Byte = 1
    Friend Const AutotileFake As Byte = 2
    Friend Const AutotileAnim As Byte = 3
    Friend Const AutotileCliff As Byte = 4
    Friend Const AutotileWaterfall As Byte = 5

    ' Rendering
    Friend Const RenderStateNone As Integer = 0

    Friend Const RenderStateNormal As Integer = 1
    Friend Const RenderStateAutotile As Integer = 2

    ' autotiling
    Friend AutoIn(4) As PointRec

    Friend AutoNw(4) As PointRec
    Friend AutoNe(4) As PointRec
    Friend AutoSw(4) As PointRec
    Friend AutoSe(4) As PointRec

    ' Map animations
    Friend WaterfallFrame As Integer

    Friend AutoTileFrame As Integer

    Friend Autotile(,) As AutotileRec

    Friend Structure PointRec
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Friend Structure QuarterTileRec
        Dim QuarterTile() As PointRec '1 To 4
        Dim RenderState As Byte
        Dim SrcX() As Integer '1 To 4
        Dim SrcY() As Integer '1 To 4
    End Structure

    Friend Structure AutotileRec
        Dim Layer() As QuarterTileRec '1 To MapLayer.Count - 1
        Dim ExLayer() As QuarterTileRec '1 To ExMapLayer.Count - 1
    End Structure

#End Region

    Sub ClearAutotiles()
        Dim x As Integer, y As Integer, i As Integer

        ReDim Autotile(Map.MaxX, Map.MaxY)

        For x = 0 To Map.MaxX
            For y = 0 To Map.MaxY
                ReDim Autotile(x, y).Layer(LayerType.Count - 1)
                For i = 0 To LayerType.Count - 1
                    ReDim Autotile(x, y).Layer(i).SrcX(4)
                    ReDim Autotile(x, y).Layer(i).SrcY(4)
                    ReDim Autotile(x, y).Layer(i).QuarterTile(4)
                Next
            Next
        Next
    End Sub

    '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    '   All of this code is for auto tiles and the math behind generating them.
    '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    Friend Sub PlaceAutotile(layerNum As Integer, x As Integer, y As Integer, tileQuarter As Byte, autoTileLetter As String)

        If layerNum > LayerType.Count - 1 Then
            layerNum = layerNum - (LayerType.Count - 1)
            With Autotile(x, y).ExLayer(layerNum).QuarterTile(tileQuarter)
                Select Case autoTileLetter
                    Case "a"
                        .X = AutoIn(1).X
                        .Y = AutoIn(1).Y
                    Case "b"
                        .X = AutoIn(2).X
                        .Y = AutoIn(2).Y
                    Case "c"
                        .X = AutoIn(3).X
                        .Y = AutoIn(3).Y
                    Case "d"
                        .X = AutoIn(4).X
                        .Y = AutoIn(4).Y
                    Case "e"
                        .X = AutoNw(1).X
                        .Y = AutoNw(1).Y
                    Case "f"
                        .X = AutoNw(2).X
                        .Y = AutoNw(2).Y
                    Case "g"
                        .X = AutoNw(3).X
                        .Y = AutoNw(3).Y
                    Case "h"
                        .X = AutoNw(4).X
                        .Y = AutoNw(4).Y
                    Case "i"
                        .X = AutoNe(1).X
                        .Y = AutoNe(1).Y
                    Case "j"
                        .X = AutoNe(2).X
                        .Y = AutoNe(2).Y
                    Case "k"
                        .X = AutoNe(3).X
                        .Y = AutoNe(3).Y
                    Case "l"
                        .X = AutoNe(4).X
                        .Y = AutoNe(4).Y
                    Case "m"
                        .X = AutoSw(1).X
                        .Y = AutoSw(1).Y
                    Case "n"
                        .X = AutoSw(2).X
                        .Y = AutoSw(2).Y
                    Case "o"
                        .X = AutoSw(3).X
                        .Y = AutoSw(3).Y
                    Case "p"
                        .X = AutoSw(4).X
                        .Y = AutoSw(4).Y
                    Case "q"
                        .X = AutoSe(1).X
                        .Y = AutoSe(1).Y
                    Case "r"
                        .X = AutoSe(2).X
                        .Y = AutoSe(2).Y
                    Case "s"
                        .X = AutoSe(3).X
                        .Y = AutoSe(3).Y
                    Case "t"
                        .X = AutoSe(4).X
                        .Y = AutoSe(4).Y
                End Select
            End With
        Else
            With Autotile(x, y).Layer(layerNum).QuarterTile(tileQuarter)
                Select Case autoTileLetter
                    Case "a"
                        .X = AutoIn(1).X
                        .Y = AutoIn(1).Y
                    Case "b"
                        .X = AutoIn(2).X
                        .Y = AutoIn(2).Y
                    Case "c"
                        .X = AutoIn(3).X
                        .Y = AutoIn(3).Y
                    Case "d"
                        .X = AutoIn(4).X
                        .Y = AutoIn(4).Y
                    Case "e"
                        .X = AutoNw(1).X
                        .Y = AutoNw(1).Y
                    Case "f"
                        .X = AutoNw(2).X
                        .Y = AutoNw(2).Y
                    Case "g"
                        .X = AutoNw(3).X
                        .Y = AutoNw(3).Y
                    Case "h"
                        .X = AutoNw(4).X
                        .Y = AutoNw(4).Y
                    Case "i"
                        .X = AutoNe(1).X
                        .Y = AutoNe(1).Y
                    Case "j"
                        .X = AutoNe(2).X
                        .Y = AutoNe(2).Y
                    Case "k"
                        .X = AutoNe(3).X
                        .Y = AutoNe(3).Y
                    Case "l"
                        .X = AutoNe(4).X
                        .Y = AutoNe(4).Y
                    Case "m"
                        .X = AutoSw(1).X
                        .Y = AutoSw(1).Y
                    Case "n"
                        .X = AutoSw(2).X
                        .Y = AutoSw(2).Y
                    Case "o"
                        .X = AutoSw(3).X
                        .Y = AutoSw(3).Y
                    Case "p"
                        .X = AutoSw(4).X
                        .Y = AutoSw(4).Y
                    Case "q"
                        .X = AutoSe(1).X
                        .Y = AutoSe(1).Y
                    Case "r"
                        .X = AutoSe(2).X
                        .Y = AutoSe(2).Y
                    Case "s"
                        .X = AutoSe(3).X
                        .Y = AutoSe(3).Y
                    Case "t"
                        .X = AutoSe(4).X
                        .Y = AutoSe(4).Y
                End Select
            End With
        End If

    End Sub

    Friend Sub InitAutotiles()
        Dim x As Integer, y As Integer, layerNum As Integer
        ' Procedure used to cache autotile positions. All positioning is
        ' independant from the tileset. Calculations are convoluted and annoying.
        ' Maths is not my strong point. Luckily we're caching them so it's a one-off
        ' thing when the map is originally loaded. As such optimisation isn't an issue.
        ' For simplicity's sake we cache all subtile SOURCE positions in to an array.
        ' We also give letters to each subtile for easy rendering tweaks. ;]
        ' First, we need to re-size the array

        ReDim Autotile(Map.MaxX, Map.MaxY)
        For x = 0 To Map.MaxX
            For y = 0 To Map.MaxY
                ReDim Autotile(x, y).Layer(LayerType.Count - 1)
                For i = 0 To LayerType.Count - 1
                    ReDim Autotile(x, y).Layer(i).SrcX(4)
                    ReDim Autotile(x, y).Layer(i).SrcY(4)
                    ReDim Autotile(x, y).Layer(i).QuarterTile(4)
                Next
            Next
        Next

        ' Inner tiles (Top right subtile region)
        ' NW - a
        AutoIn(1).X = 32
        AutoIn(1).Y = 0
        ' NE - b
        AutoIn(2).X = 48
        AutoIn(2).Y = 0
        ' SW - c
        AutoIn(3).X = 32
        AutoIn(3).Y = 16
        ' SE - d
        AutoIn(4).X = 48
        AutoIn(4).Y = 16
        ' Outer Tiles - NW (bottom subtile region)
        ' NW - e
        AutoNw(1).X = 0
        AutoNw(1).Y = 32
        ' NE - f
        AutoNw(2).X = 16
        AutoNw(2).Y = 32
        ' SW - g
        AutoNw(3).X = 0
        AutoNw(3).Y = 48
        ' SE - h
        AutoNw(4).X = 16
        AutoNw(4).Y = 48
        ' Outer Tiles - NE (bottom subtile region)
        ' NW - i
        AutoNe(1).X = 32
        AutoNe(1).Y = 32
        ' NE - g
        AutoNe(2).X = 48
        AutoNe(2).Y = 32
        ' SW - k
        AutoNe(3).X = 32
        AutoNe(3).Y = 48
        ' SE - l
        AutoNe(4).X = 48
        AutoNe(4).Y = 48
        ' Outer Tiles - SW (bottom subtile region)
        ' NW - m
        AutoSw(1).X = 0
        AutoSw(1).Y = 64
        ' NE - n
        AutoSw(2).X = 16
        AutoSw(2).Y = 64
        ' SW - o
        AutoSw(3).X = 0
        AutoSw(3).Y = 80
        ' SE - p
        AutoSw(4).X = 16
        AutoSw(4).Y = 80
        ' Outer Tiles - SE (bottom subtile region)
        ' NW - q
        AutoSe(1).X = 32
        AutoSe(1).Y = 64
        ' NE - r
        AutoSe(2).X = 48
        AutoSe(2).Y = 64
        ' SW - s
        AutoSe(3).X = 32
        AutoSe(3).Y = 80
        ' SE - t
        AutoSe(4).X = 48
        AutoSe(4).Y = 80

        For x = 0 To Map.MaxX
            For y = 0 To Map.MaxY
                For layerNum = 1 To LayerType.Count - 1
                    ' calculate the subtile positions and place them
                    CalculateAutotile(x, y, layerNum)
                    ' cache the rendering state of the tiles and set them
                    CacheRenderState(x, y, layerNum)
                Next
            Next
        Next

    End Sub

    Friend Sub CacheRenderState(x As Integer, y As Integer, layerNum As Integer)
        Dim quarterNum As Integer

        ' exit out early

        If x < 0 OrElse x > Map.MaxX OrElse y < 0 OrElse y > Map.MaxY Then Exit Sub

        With Map.Tile(x, y)
            ' check if the tile can be rendered
            If .Layer(layerNum).Tileset <= 0 OrElse .Layer(layerNum).Tileset > NumTileSets Then
                Autotile(x, y).Layer(layerNum).RenderState = RenderStateNone
                Exit Sub
            End If
            ' check if it's a key - hide mask if key is closed
            If layerNum = LayerType.Mask Then
                If .Type = TileType.Key Then
                    If TempTile(x, y).DoorOpen = False Then
                        Autotile(x, y).Layer(layerNum).RenderState = RenderStateNone
                        Exit Sub
                    Else
                        Autotile(x, y).Layer(layerNum).RenderState = RenderStateNormal
                        Exit Sub
                    End If
                End If
            End If
            ' check if it needs to be rendered as an autotile
            If .Layer(layerNum).AutoTile = AutotileNone OrElse .Layer(layerNum).AutoTile = AutotileFake Then
                'ReDim Autotile(X, Y).Layer(MapLayer.Count - 1)
                ' default to... default
                Autotile(x, y).Layer(layerNum).RenderState = RenderStateNormal
            Else
                Autotile(x, y).Layer(layerNum).RenderState = RenderStateAutotile
                ' cache tileset positioning
                For quarterNum = 1 To 4
                    Autotile(x, y).Layer(layerNum).SrcX(quarterNum) = (Map.Tile(x, y).Layer(layerNum).X * 32) + Autotile(x, y).Layer(layerNum).QuarterTile(quarterNum).X
                    Autotile(x, y).Layer(layerNum).SrcY(quarterNum) = (Map.Tile(x, y).Layer(layerNum).Y * 32) + Autotile(x, y).Layer(layerNum).QuarterTile(quarterNum).Y
                Next
            End If
        End With
        ' End If

    End Sub

    Friend Sub CalculateAutotile(x As Integer, y As Integer, layerNum As Integer)
        ' Right, so we've split the tile block in to an easy to remember
        ' collection of letters. We now need to do the calculations to find
        ' out which little lettered block needs to be rendered. We do this
        ' by reading the surrounding tiles to check for matches.
        ' First we check to make sure an autotile situation is actually there.
        ' Then we calculate exactly which situation has arisen.
        ' The situations are "inner", "outer", "horizontal", "vertical" and "fill".
        ' Exit out if we don't have an autotile

        If Map.Tile(x, y).Layer(layerNum).AutoTile = 0 Then Exit Sub
        ' Okay, we have autotiling but which one?
        Select Case Map.Tile(x, y).Layer(layerNum).AutoTile
                ' Normal or animated - same difference
            Case AutotileNormal, AutotileAnim
                ' North West Quarter
                CalculateNW_Normal(layerNum, x, y)
                ' North East Quarter
                CalculateNE_Normal(layerNum, x, y)
                ' South West Quarter
                CalculateSW_Normal(layerNum, x, y)
                ' South East Quarter
                CalculateSE_Normal(layerNum, x, y)
                ' Cliff
            Case AutotileCliff
                ' North West Quarter
                CalculateNW_Cliff(layerNum, x, y)
                ' North East Quarter
                CalculateNE_Cliff(layerNum, x, y)
                ' South West Quarter
                CalculateSW_Cliff(layerNum, x, y)
                ' South East Quarter
                CalculateSE_Cliff(layerNum, x, y)
                ' Waterfalls
            Case AutotileWaterfall
                ' North West Quarter
                CalculateNW_Waterfall(layerNum, x, y)
                ' North East Quarter
                CalculateNE_Waterfall(layerNum, x, y)
                ' South West Quarter
                CalculateSW_Waterfall(layerNum, x, y)
                ' South East Quarter
                CalculateSE_Waterfall(layerNum, x, y)
                ' Anything else
        End Select
        ' End If

    End Sub

    ' Normal autotiling
    Friend Sub CalculateNW_Normal(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile(3) As Boolean
        Dim situation As Byte

        ' North West

        If CheckTileMatch(layerNum, x, y, x - 1, y - 1) Then tmpTile(1) = True
        ' North
        If CheckTileMatch(layerNum, x, y, x, y - 1) Then tmpTile(2) = True
        ' West
        If CheckTileMatch(layerNum, x, y, x - 1, y) Then tmpTile(3) = True
        ' Calculate Situation - Inner
        If Not tmpTile(2) AndAlso Not tmpTile(3) Then situation = AutoInner
        ' Horizontal
        If Not tmpTile(2) AndAlso tmpTile(3) Then situation = AutoHorizontal
        ' Vertical
        If tmpTile(2) AndAlso Not tmpTile(3) Then situation = AutoVertical
        ' Outer
        If Not tmpTile(1) AndAlso tmpTile(2) AndAlso tmpTile(3) Then situation = AutoOuter
        ' Fill
        If tmpTile(1) AndAlso tmpTile(2) AndAlso tmpTile(3) Then situation = AutoFill
        ' Actually place the subtile
        Select Case situation
            Case AutoInner
                PlaceAutotile(layerNum, x, y, 1, "e")
            Case AutoOuter
                PlaceAutotile(layerNum, x, y, 1, "a")
            Case AutoHorizontal
                PlaceAutotile(layerNum, x, y, 1, "i")
            Case AutoVertical
                PlaceAutotile(layerNum, x, y, 1, "m")
            Case AutoFill
                PlaceAutotile(layerNum, x, y, 1, "q")
        End Select

    End Sub

    Friend Sub CalculateNE_Normal(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile(3) As Boolean
        Dim situation As Byte

        ' North

        If CheckTileMatch(layerNum, x, y, x, y - 1) Then tmpTile(1) = True
        ' North East
        If CheckTileMatch(layerNum, x, y, x + 1, y - 1) Then tmpTile(2) = True
        ' East
        If CheckTileMatch(layerNum, x, y, x + 1, y) Then tmpTile(3) = True
        ' Calculate Situation - Inner
        If Not tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoInner
        ' Horizontal
        If Not tmpTile(1) AndAlso tmpTile(3) Then situation = AutoHorizontal
        ' Vertical
        If tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoVertical
        ' Outer
        If tmpTile(1) AndAlso Not tmpTile(2) AndAlso tmpTile(3) Then situation = AutoOuter
        ' Fill
        If tmpTile(1) AndAlso tmpTile(2) AndAlso tmpTile(3) Then situation = AutoFill
        ' Actually place the subtile
        Select Case situation
            Case AutoInner
                PlaceAutotile(layerNum, x, y, 2, "j")
            Case AutoOuter
                PlaceAutotile(layerNum, x, y, 2, "b")
            Case AutoHorizontal
                PlaceAutotile(layerNum, x, y, 2, "f")
            Case AutoVertical
                PlaceAutotile(layerNum, x, y, 2, "r")
            Case AutoFill
                PlaceAutotile(layerNum, x, y, 2, "n")
        End Select

    End Sub

    Friend Sub CalculateSW_Normal(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile(3) As Boolean
        Dim situation As Byte

        ' West

        If CheckTileMatch(layerNum, x, y, x - 1, y) Then tmpTile(1) = True
        ' South West
        If CheckTileMatch(layerNum, x, y, x - 1, y + 1) Then tmpTile(2) = True
        ' South
        If CheckTileMatch(layerNum, x, y, x, y + 1) Then tmpTile(3) = True
        ' Calculate Situation - Inner
        If Not tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoInner
        ' Horizontal
        If tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoHorizontal
        ' Vertical
        If Not tmpTile(1) AndAlso tmpTile(3) Then situation = AutoVertical
        ' Outer
        If tmpTile(1) AndAlso Not tmpTile(2) AndAlso tmpTile(3) Then situation = AutoOuter
        ' Fill
        If tmpTile(1) AndAlso tmpTile(2) AndAlso tmpTile(3) Then situation = AutoFill
        ' Actually place the subtile
        Select Case situation
            Case AutoInner
                PlaceAutotile(layerNum, x, y, 3, "o")
            Case AutoOuter
                PlaceAutotile(layerNum, x, y, 3, "c")
            Case AutoHorizontal
                PlaceAutotile(layerNum, x, y, 3, "s")
            Case AutoVertical
                PlaceAutotile(layerNum, x, y, 3, "g")
            Case AutoFill
                PlaceAutotile(layerNum, x, y, 3, "k")
        End Select

    End Sub

    Friend Sub CalculateSE_Normal(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile(3) As Boolean
        Dim situation As Byte

        ' South

        If CheckTileMatch(layerNum, x, y, x, y + 1) Then tmpTile(1) = True
        ' South East
        If CheckTileMatch(layerNum, x, y, x + 1, y + 1) Then tmpTile(2) = True
        ' East
        If CheckTileMatch(layerNum, x, y, x + 1, y) Then tmpTile(3) = True
        ' Calculate Situation - Inner
        If Not tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoInner
        ' Horizontal
        If Not tmpTile(1) AndAlso tmpTile(3) Then situation = AutoHorizontal
        ' Vertical
        If tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoVertical
        ' Outer
        If tmpTile(1) AndAlso Not tmpTile(2) AndAlso tmpTile(3) Then situation = AutoOuter
        ' Fill
        If tmpTile(1) AndAlso tmpTile(2) AndAlso tmpTile(3) Then situation = AutoFill
        ' Actually place the subtile
        Select Case situation
            Case AutoInner
                PlaceAutotile(layerNum, x, y, 4, "t")
            Case AutoOuter
                PlaceAutotile(layerNum, x, y, 4, "d")
            Case AutoHorizontal
                PlaceAutotile(layerNum, x, y, 4, "p")
            Case AutoVertical
                PlaceAutotile(layerNum, x, y, 4, "l")
            Case AutoFill
                PlaceAutotile(layerNum, x, y, 4, "h")
        End Select

    End Sub

    ' Waterfall autotiling
    Friend Sub CalculateNW_Waterfall(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile As Boolean
        ' West

        If CheckTileMatch(layerNum, x, y, x - 1, y) Then tmpTile = True
        ' Actually place the subtile
        If tmpTile Then
            ' Extended
            PlaceAutotile(layerNum, x, y, 1, "i")
        Else
            ' Edge
            PlaceAutotile(layerNum, x, y, 1, "e")
        End If

    End Sub

    Friend Sub CalculateNE_Waterfall(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile As Boolean
        ' East

        If CheckTileMatch(layerNum, x, y, x + 1, y) Then tmpTile = True
        ' Actually place the subtile
        If tmpTile Then
            ' Extended
            PlaceAutotile(layerNum, x, y, 2, "f")
        Else
            ' Edge
            PlaceAutotile(layerNum, x, y, 2, "j")
        End If

    End Sub

    Friend Sub CalculateSW_Waterfall(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile As Boolean
        ' West

        If CheckTileMatch(layerNum, x, y, x - 1, y) Then tmpTile = True
        ' Actually place the subtile
        If tmpTile Then
            ' Extended
            PlaceAutotile(layerNum, x, y, 3, "k")
        Else
            ' Edge
            PlaceAutotile(layerNum, x, y, 3, "g")
        End If

    End Sub

    Friend Sub CalculateSE_Waterfall(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile As Boolean
        ' East

        If CheckTileMatch(layerNum, x, y, x + 1, y) Then tmpTile = True
        ' Actually place the subtile
        If tmpTile Then
            ' Extended
            PlaceAutotile(layerNum, x, y, 4, "h")
        Else
            ' Edge
            PlaceAutotile(layerNum, x, y, 4, "l")
        End If

    End Sub

    ' Cliff autotiling
    Friend Sub CalculateNW_Cliff(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile(3) As Boolean
        Dim situation As Byte

        ' North West

        If CheckTileMatch(layerNum, x, y, x - 1, y - 1) Then tmpTile(1) = True
        ' North
        If CheckTileMatch(layerNum, x, y, x, y - 1) Then tmpTile(2) = True
        ' West
        If CheckTileMatch(layerNum, x, y, x - 1, y) Then tmpTile(3) = True
        situation = AutoFill
        ' Calculate Situation - Horizontal
        If Not tmpTile(2) AndAlso tmpTile(3) Then situation = AutoHorizontal
        ' Vertical
        If tmpTile(2) AndAlso Not tmpTile(3) Then situation = AutoVertical
        ' Fill
        If tmpTile(1) AndAlso tmpTile(2) AndAlso tmpTile(3) Then situation = AutoFill
        ' Inner
        If Not tmpTile(2) AndAlso Not tmpTile(3) Then situation = AutoInner
        ' Actually place the subtile
        Select Case situation
            Case AutoInner
                PlaceAutotile(layerNum, x, y, 1, "e")
            Case AutoHorizontal
                PlaceAutotile(layerNum, x, y, 1, "i")
            Case AutoVertical
                PlaceAutotile(layerNum, x, y, 1, "m")
            Case AutoFill
                PlaceAutotile(layerNum, x, y, 1, "q")
        End Select

    End Sub

    Friend Sub CalculateNE_Cliff(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile(3) As Boolean
        Dim situation As Byte

        ' North

        If CheckTileMatch(layerNum, x, y, x, y - 1) Then tmpTile(1) = True
        ' North East
        If CheckTileMatch(layerNum, x, y, x + 1, y - 1) Then tmpTile(2) = True
        ' East
        If CheckTileMatch(layerNum, x, y, x + 1, y) Then tmpTile(3) = True
        situation = AutoFill
        ' Calculate Situation - Horizontal
        If Not tmpTile(1) AndAlso tmpTile(3) Then situation = AutoHorizontal
        ' Vertical
        If tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoVertical
        ' Fill
        If tmpTile(1) AndAlso tmpTile(2) AndAlso tmpTile(3) Then situation = AutoFill
        ' Inner
        If Not tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoInner
        ' Actually place the subtile
        Select Case situation
            Case AutoInner
                PlaceAutotile(layerNum, x, y, 2, "j")
            Case AutoHorizontal
                PlaceAutotile(layerNum, x, y, 2, "f")
            Case AutoVertical
                PlaceAutotile(layerNum, x, y, 2, "r")
            Case AutoFill
                PlaceAutotile(layerNum, x, y, 2, "n")
        End Select

    End Sub

    Friend Sub CalculateSW_Cliff(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile(3) As Boolean
        Dim situation As Byte

        ' West

        If CheckTileMatch(layerNum, x, y, x - 1, y) Then tmpTile(1) = True
        ' South West
        If CheckTileMatch(layerNum, x, y, x - 1, y + 1) Then tmpTile(2) = True
        ' South
        If CheckTileMatch(layerNum, x, y, x, y + 1) Then tmpTile(3) = True
        situation = AutoFill
        ' Calculate Situation - Horizontal
        If tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoHorizontal
        ' Vertical
        If Not tmpTile(1) AndAlso tmpTile(3) Then situation = AutoVertical
        ' Fill
        If tmpTile(1) AndAlso tmpTile(2) AndAlso tmpTile(3) Then situation = AutoFill
        ' Inner
        If Not tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoInner
        ' Actually place the subtile
        Select Case situation
            Case AutoInner
                PlaceAutotile(layerNum, x, y, 3, "o")
            Case AutoHorizontal
                PlaceAutotile(layerNum, x, y, 3, "s")
            Case AutoVertical
                PlaceAutotile(layerNum, x, y, 3, "g")
            Case AutoFill
                PlaceAutotile(layerNum, x, y, 3, "k")
        End Select

    End Sub

    Friend Sub CalculateSE_Cliff(layerNum As Integer, x As Integer, y As Integer)
        Dim tmpTile(3) As Boolean
        Dim situation As Byte

        ' South

        If CheckTileMatch(layerNum, x, y, x, y + 1) Then tmpTile(1) = True
        ' South East
        If CheckTileMatch(layerNum, x, y, x + 1, y + 1) Then tmpTile(2) = True
        ' East
        If CheckTileMatch(layerNum, x, y, x + 1, y) Then tmpTile(3) = True
        situation = AutoFill
        ' Calculate Situation -  Horizontal
        If Not tmpTile(1) AndAlso tmpTile(3) Then situation = AutoHorizontal
        ' Vertical
        If tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoVertical
        ' Fill
        If tmpTile(1) AndAlso tmpTile(2) AndAlso tmpTile(3) Then situation = AutoFill
        ' Inner
        If Not tmpTile(1) AndAlso Not tmpTile(3) Then situation = AutoInner
        ' Actually place the subtile
        Select Case situation
            Case AutoInner
                PlaceAutotile(layerNum, x, y, 4, "t")
            Case AutoHorizontal
                PlaceAutotile(layerNum, x, y, 4, "p")
            Case AutoVertical
                PlaceAutotile(layerNum, x, y, 4, "l")
            Case AutoFill
                PlaceAutotile(layerNum, x, y, 4, "h")
        End Select

    End Sub

    Friend Function CheckTileMatch(layerNum As Integer, x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer) As Boolean
        ' we'll exit out early if true
        'Dim exTile As Boolean

        'If layerNum > MapLayer.Count - 1 Then exTile = True : layerNum = layerNum - (MapLayer.Count - 1)
        CheckTileMatch = True
        ' if it's off the map then set it as autotile and exit out early
        If x2 < 0 OrElse x2 > Map.MaxX OrElse y2 < 0 OrElse y2 > Map.MaxY Then
            CheckTileMatch = True
            Exit Function
        End If

        ' fakes ALWAYS return true
        If Map.Tile(x2, y2).Layer(layerNum).AutoTile = AutotileFake Then
            CheckTileMatch = True
            Exit Function
        End If
        ' End If

        ' check neighbour is an autotile
        If Map.Tile(x2, y2).Layer(layerNum).AutoTile = 0 Then
            CheckTileMatch = False
            Exit Function
        End If
        ' End If

        ' check we're a matching
        If Map.Tile(x1, y1).Layer(layerNum).Tileset <> Map.Tile(x2, y2).Layer(layerNum).Tileset Then
            CheckTileMatch = False
            Exit Function
        End If

        ' check tiles match
        If Map.Tile(x1, y1).Layer(layerNum).X <> Map.Tile(x2, y2).Layer(layerNum).X Then
            CheckTileMatch = False
            Exit Function
        Else
            If Map.Tile(x1, y1).Layer(layerNum).Y <> Map.Tile(x2, y2).Layer(layerNum).Y Then
                CheckTileMatch = False
                Exit Function
            End If
        End If
    End Function

    Friend Sub DrawAutoTile(layerNum As Integer, destX As Integer, destY As Integer, quarterNum As Integer, x As Integer, y As Integer, Optional forceFrame As Integer = 0, Optional strict As Boolean = True)
        Dim yOffset As Integer, xOffset As Integer
        'Dim tmpSprite As Sprite

        If GettingMap Then Exit Sub

        ' calculate the offset
        If forceFrame > 0 Then
            Select Case forceFrame - 1
                Case 0
                    WaterfallFrame = 1
                Case 1
                    WaterfallFrame = 2
                Case 2
                    WaterfallFrame = 0
            End Select
            ' animate autotiles
            Select Case forceFrame - 1
                Case 0
                    AutoTileFrame = 1
                Case 1
                    AutoTileFrame = 2
                Case 2
                    AutoTileFrame = 0
            End Select
        End If

        Select Case Map.Tile(x, y).Layer(layerNum).AutoTile
            Case AutotileWaterfall
                yOffset = (WaterfallFrame - 1) * 32
            Case AutotileAnim
                xOffset = AutoTileFrame * 64
            Case AutotileCliff
                yOffset = -32
        End Select

        ' Draw the quarter
        'TileSetSprite(Map.Tile(X, Y).Layer(layerNum).Tileset).TextureRect = New IntRect(Autotile(X, Y).Layer(layerNum).srcX(quarterNum) + XOffset, Autotile(X, Y).Layer(layerNum).srcY(quarterNum) + YOffset, 16, 16)
        'TileSetSprite(Map.Tile(X, Y).Layer(layerNum).Tileset).Position = New SFML.Window.Vector2f(destX, destY)

        'GameWindow.Draw(TileSetSprite(Map.Tile(X, Y).Layer(layerNum).Tileset))
        If Map.Tile(x, y).Layer Is Nothing Then Exit Sub
        RenderSprite(TileSetSprite(Map.Tile(x, y).Layer(layerNum).Tileset), GameWindow, destX, destY, Autotile(x, y).Layer(layerNum).SrcX(quarterNum) + xOffset, Autotile(x, y).Layer(layerNum).SrcY(quarterNum) + yOffset, 16, 16)
    End Sub

End Module