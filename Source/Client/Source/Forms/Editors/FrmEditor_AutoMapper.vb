Imports Ini = Asfw.IO.TextFile

Friend Class frmEditor_AutoMapper

#Region "Frm Code"

    Private Sub FrmAutoMapper_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlResources.Top = 0
        pnlResources.Left = 0
        pnlTileConfig.Top = 0
        pnlTileConfig.Left = 0
        Width = 409
    End Sub

    Private Sub TilesetsToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles TilesetsToolStripMenuItem.Click
        pnlTileConfig.Visible = True
        pnlTileConfig.BringToFront()
    End Sub

    Private Sub ResourcesToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ResourcesToolStripMenuItem.Click
        Dim Resources() As String
        Dim i As Integer

        pnlResources.Visible = True
        pnlResources.BringToFront()

        Resources = Split(ResourcesNum, ";")

        lstResources.Items.Clear()

        For i = 0 To UBound(Resources)
            lstResources.Items.Add(Resources(i))
        Next
    End Sub

    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        MapStart = Val(txtMapStart.Text)
        MapSize = Val(txtMapSize.Text)
        MapX = Val(txtMapX.Text)
        MapY = Val(txtMapY.Text)
        SandBorder = Val(txtSandBorder.Text)
        DetailFreq = Val(txtDetail.Text)
        ResourceFreq = Val(txtResourceFreq.Text)

        SendSaveAutoMapper()

        Me.Dispose()
    End Sub

#End Region

#Region "Resources"

    Private Sub LstResources_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstResources.SelectedIndexChanged
        If lstResources.SelectedIndex < 0 Then Exit Sub
        txtResource.Text = lstResources.Items.Item(lstResources.SelectedIndex)
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAddResource.Click
        lstResources.Items.Add(Val(txtResource.Text))
    End Sub

    Private Sub BtnRemove_Click(sender As Object, e As EventArgs) Handles btnRemoveResource.Click
        If lstResources.SelectedIndex < 0 Then Exit Sub
        lstResources.Items.RemoveAt(lstResources.SelectedIndex)
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdateResource.Click

        If lstResources.SelectedIndex < 0 Then Exit Sub

        lstResources.Items.Item(lstResources.SelectedIndex) = txtResource.Text
    End Sub

    Private Sub BtnCloseResource_Click(sender As Object, e As EventArgs) Handles btnCloseResource.Click
        pnlResources.Visible = False
    End Sub

    Private Sub BtnSaveResource_Click(sender As Object, e As EventArgs) Handles btnSaveResource.Click
        Dim i As Integer
        Dim ResourceStr As String = ""
        Dim cf = Path.Contents & "AutoMapper.ini"

        For i = 0 To lstResources.Items.Count - 1
            ResourceStr = CStr(ResourceStr & lstResources.Items(i))
            If i < lstResources.Items.Count - 1 Then ResourceStr = ResourceStr & ";"
        Next

        Ini.Write(cf, "Resources", "ResourcesNum", ResourceStr)

        pnlResources.Visible = False
    End Sub

#End Region

#Region "TileSet"

    Private Sub CmbPrefab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPrefab.SelectedIndexChanged
        Dim Layer As Integer

        For Layer = 1 To LayerType.Count - 1
            If Tile(cmbPrefab.SelectedIndex + 1).Layer(Layer).Tileset > 0 Then
                Exit For
            End If
        Next

        cmbLayer.SelectedIndex = Layer - 1
        CmbLayer_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub CmbLayer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLayer.SelectedIndexChanged
        Dim Prefab As Integer
        Dim Layer As Integer
        Prefab = cmbPrefab.SelectedIndex + 1
        Layer = cmbLayer.SelectedIndex + 1
        txtTileset.Text = Tile(Prefab).Layer(Layer).Tileset
        txtTileX.Text = Tile(Prefab).Layer(Layer).X
        txtTileY.Text = Tile(Prefab).Layer(Layer).Y
        txtAutotile.Text = Tile(Prefab).Layer(Layer).AutoTile

        If Tile(Prefab).Type = TileType.Blocked Then
            chkBlocked.Checked = True
        Else
            chkBlocked.Checked = False
        End If
    End Sub

    Private Sub BtnTileSetClose_Click(sender As Object, e As EventArgs) Handles btnTileSetClose.Click
        pnlTileConfig.Visible = False
    End Sub

    Private Sub BtnTileSetSave_Click(sender As Object, e As EventArgs) Handles btnTileSetSave.Click
        Dim Prefab As Integer, Layer As Integer
        Dim cf = Path.Contents & "AutoMapper.ini"

        Prefab = cmbPrefab.SelectedIndex + 1

        For Layer = 1 To 5
            If Tile(Prefab).Layer(Layer).Tileset > 0 Then
                Ini.Write(cf, "Prefab" & Prefab, "Layer" & Layer & "Tileset", Val(Tile(Prefab).Layer(Layer).Tileset))
                Ini.Write(cf, "Prefab" & Prefab, "Layer" & Layer & "X", Val(Tile(Prefab).Layer(Layer).X))
                Ini.Write(cf, "Prefab" & Prefab, "Layer" & Layer & "Y", Val(Tile(Prefab).Layer(Layer).Y))
                Ini.Write(cf, "Prefab" & Prefab, "Layer" & Layer & "Autotile", Val(Tile(Prefab).Layer(Layer).AutoTile))
            End If
        Next Layer

        Ini.Write(cf, "Prefab" & Prefab, "Type", Val(Tile(Prefab).Type))

        pnlTileConfig.Visible = False

        LoadTilePrefab()
    End Sub

#End Region

End Class