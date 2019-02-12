<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAdmin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAdmin))
        Me.btnRespawn = New System.Windows.Forms.Button()
        Me.btnMapReport = New System.Windows.Forms.Button()
        Me.btnALoc = New System.Windows.Forms.Button()
        Me.btnSpawnItem = New System.Windows.Forms.Button()
        Me.lblSpawnItemAmount = New System.Windows.Forms.Label()
        Me.lblItemSpawn = New System.Windows.Forms.Label()
        Me.btnAdminSetSprite = New System.Windows.Forms.Button()
        Me.btnAdminWarpTo = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnAdminSetAccess = New System.Windows.Forms.Button()
        Me.btnAdminWarpMe2 = New System.Windows.Forms.Button()
        Me.btnAdminWarp2Me = New System.Windows.Forms.Button()
        Me.btnAdminBan = New System.Windows.Forms.Button()
        Me.btnAdminKick = New System.Windows.Forms.Button()
        Me.txtAdminName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstMaps = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabModeration = New System.Windows.Forms.TabPage()
        Me.nudAdminSprite = New System.Windows.Forms.NumericUpDown()
        Me.nudAdminMap = New System.Windows.Forms.NumericUpDown()
        Me.btnLevelUp = New System.Windows.Forms.Button()
        Me.cmbAccess = New System.Windows.Forms.ComboBox()
        Me.tabMapList = New System.Windows.Forms.TabPage()
        Me.tabMapTools = New System.Windows.Forms.TabPage()
        Me.nudSpawnItemAmount = New System.Windows.Forms.NumericUpDown()
        Me.cmbSpawnItem = New System.Windows.Forms.ComboBox()
        Me.tabEditors = New System.Windows.Forms.TabPage()
        Me.btnPetEditor = New System.Windows.Forms.Button()
        Me.btnAutoMapper = New System.Windows.Forms.Button()
        Me.btnClassEditor = New System.Windows.Forms.Button()
        Me.btnRecipeEditor = New System.Windows.Forms.Button()
        Me.btnProjectiles = New System.Windows.Forms.Button()
        Me.btnQuest = New System.Windows.Forms.Button()
        Me.btnhouseEditor = New System.Windows.Forms.Button()
        Me.btnMapEditor = New System.Windows.Forms.Button()
        Me.btnItemEditor = New System.Windows.Forms.Button()
        Me.btnResourceEditor = New System.Windows.Forms.Button()
        Me.btnNPCEditor = New System.Windows.Forms.Button()
        Me.btnSkillEditor = New System.Windows.Forms.Button()
        Me.btnShopEditor = New System.Windows.Forms.Button()
        Me.btnAnimationEditor = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.tabModeration.SuspendLayout()
        CType(Me.nudAdminSprite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudAdminMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMapList.SuspendLayout()
        Me.tabMapTools.SuspendLayout()
        CType(Me.nudSpawnItemAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabEditors.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRespawn
        '
        Me.btnRespawn.Location = New System.Drawing.Point(136, 16)
        Me.btnRespawn.Name = "btnRespawn"
        Me.btnRespawn.Size = New System.Drawing.Size(106, 22)
        Me.btnRespawn.TabIndex = 34
        Me.btnRespawn.Text = "Respawn Map"
        Me.btnRespawn.UseVisualStyleBackColor = True
        '
        'btnMapReport
        '
        Me.btnMapReport.Location = New System.Drawing.Point(6, 209)
        Me.btnMapReport.Name = "btnMapReport"
        Me.btnMapReport.Size = New System.Drawing.Size(238, 22)
        Me.btnMapReport.TabIndex = 33
        Me.btnMapReport.Text = "Refresh List"
        Me.btnMapReport.UseVisualStyleBackColor = True
        '
        'btnALoc
        '
        Me.btnALoc.Location = New System.Drawing.Point(14, 16)
        Me.btnALoc.Name = "btnALoc"
        Me.btnALoc.Size = New System.Drawing.Size(106, 22)
        Me.btnALoc.TabIndex = 31
        Me.btnALoc.Text = "Location"
        Me.btnALoc.UseVisualStyleBackColor = True
        '
        'btnSpawnItem
        '
        Me.btnSpawnItem.Location = New System.Drawing.Point(14, 145)
        Me.btnSpawnItem.Name = "btnSpawnItem"
        Me.btnSpawnItem.Size = New System.Drawing.Size(228, 22)
        Me.btnSpawnItem.TabIndex = 29
        Me.btnSpawnItem.Text = "Spawn Item"
        Me.btnSpawnItem.UseVisualStyleBackColor = True
        '
        'lblSpawnItemAmount
        '
        Me.lblSpawnItemAmount.AutoSize = True
        Me.lblSpawnItemAmount.Location = New System.Drawing.Point(31, 121)
        Me.lblSpawnItemAmount.Name = "lblSpawnItemAmount"
        Me.lblSpawnItemAmount.Size = New System.Drawing.Size(46, 13)
        Me.lblSpawnItemAmount.TabIndex = 26
        Me.lblSpawnItemAmount.Text = "Amount:"
        '
        'lblItemSpawn
        '
        Me.lblItemSpawn.AutoSize = True
        Me.lblItemSpawn.Location = New System.Drawing.Point(11, 95)
        Me.lblItemSpawn.Name = "lblItemSpawn"
        Me.lblItemSpawn.Size = New System.Drawing.Size(66, 13)
        Me.lblItemSpawn.TabIndex = 25
        Me.lblItemSpawn.Text = "Spawn Item:"
        '
        'btnAdminSetSprite
        '
        Me.btnAdminSetSprite.Location = New System.Drawing.Point(134, 206)
        Me.btnAdminSetSprite.Name = "btnAdminSetSprite"
        Me.btnAdminSetSprite.Size = New System.Drawing.Size(108, 24)
        Me.btnAdminSetSprite.TabIndex = 16
        Me.btnAdminSetSprite.Text = "Set Player Sprite"
        Me.btnAdminSetSprite.UseVisualStyleBackColor = True
        '
        'btnAdminWarpTo
        '
        Me.btnAdminWarpTo.Location = New System.Drawing.Point(134, 176)
        Me.btnAdminWarpTo.Name = "btnAdminWarpTo"
        Me.btnAdminWarpTo.Size = New System.Drawing.Size(108, 24)
        Me.btnAdminWarpTo.TabIndex = 15
        Me.btnAdminWarpTo.Text = "Warp To Map"
        Me.btnAdminWarpTo.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 210)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Sprite:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 182)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Map Number:"
        '
        'btnAdminSetAccess
        '
        Me.btnAdminSetAccess.Location = New System.Drawing.Point(9, 148)
        Me.btnAdminSetAccess.Name = "btnAdminSetAccess"
        Me.btnAdminSetAccess.Size = New System.Drawing.Size(233, 22)
        Me.btnAdminSetAccess.TabIndex = 9
        Me.btnAdminSetAccess.Text = "Set Access"
        Me.btnAdminSetAccess.UseVisualStyleBackColor = True
        '
        'btnAdminWarpMe2
        '
        Me.btnAdminWarpMe2.Location = New System.Drawing.Point(127, 62)
        Me.btnAdminWarpMe2.Name = "btnAdminWarpMe2"
        Me.btnAdminWarpMe2.Size = New System.Drawing.Size(115, 22)
        Me.btnAdminWarpMe2.TabIndex = 8
        Me.btnAdminWarpMe2.Text = "Warp Me To Player"
        Me.btnAdminWarpMe2.UseVisualStyleBackColor = True
        '
        'btnAdminWarp2Me
        '
        Me.btnAdminWarp2Me.Location = New System.Drawing.Point(6, 62)
        Me.btnAdminWarp2Me.Name = "btnAdminWarp2Me"
        Me.btnAdminWarp2Me.Size = New System.Drawing.Size(115, 22)
        Me.btnAdminWarp2Me.TabIndex = 7
        Me.btnAdminWarp2Me.Text = "Warp Player To Me"
        Me.btnAdminWarp2Me.UseVisualStyleBackColor = True
        '
        'btnAdminBan
        '
        Me.btnAdminBan.Location = New System.Drawing.Point(127, 34)
        Me.btnAdminBan.Name = "btnAdminBan"
        Me.btnAdminBan.Size = New System.Drawing.Size(115, 22)
        Me.btnAdminBan.TabIndex = 6
        Me.btnAdminBan.Text = "Ban Player"
        Me.btnAdminBan.UseVisualStyleBackColor = True
        '
        'btnAdminKick
        '
        Me.btnAdminKick.Location = New System.Drawing.Point(6, 34)
        Me.btnAdminKick.Name = "btnAdminKick"
        Me.btnAdminKick.Size = New System.Drawing.Size(115, 22)
        Me.btnAdminKick.TabIndex = 5
        Me.btnAdminKick.Text = "Kick Player"
        Me.btnAdminKick.UseVisualStyleBackColor = True
        '
        'txtAdminName
        '
        Me.txtAdminName.Location = New System.Drawing.Point(82, 8)
        Me.txtAdminName.Name = "txtAdminName"
        Me.txtAdminName.Size = New System.Drawing.Size(160, 20)
        Me.txtAdminName.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Access:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Player Name:"
        '
        'lstMaps
        '
        Me.lstMaps.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lstMaps.FullRowSelect = True
        Me.lstMaps.GridLines = True
        Me.lstMaps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstMaps.HideSelection = False
        Me.lstMaps.Location = New System.Drawing.Point(6, 6)
        Me.lstMaps.MultiSelect = False
        Me.lstMaps.Name = "lstMaps"
        Me.lstMaps.Size = New System.Drawing.Size(239, 197)
        Me.lstMaps.TabIndex = 4
        Me.lstMaps.UseCompatibleStateImageBehavior = False
        Me.lstMaps.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "#"
        Me.ColumnHeader1.Width = 30
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Name"
        Me.ColumnHeader2.Width = 200
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabModeration)
        Me.TabControl1.Controls.Add(Me.tabMapList)
        Me.TabControl1.Controls.Add(Me.tabMapTools)
        Me.TabControl1.Controls.Add(Me.tabEditors)
        Me.TabControl1.Location = New System.Drawing.Point(2, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(258, 265)
        Me.TabControl1.TabIndex = 38
        '
        'tabModeration
        '
        Me.tabModeration.Controls.Add(Me.nudAdminSprite)
        Me.tabModeration.Controls.Add(Me.nudAdminMap)
        Me.tabModeration.Controls.Add(Me.btnLevelUp)
        Me.tabModeration.Controls.Add(Me.cmbAccess)
        Me.tabModeration.Controls.Add(Me.Label2)
        Me.tabModeration.Controls.Add(Me.Label3)
        Me.tabModeration.Controls.Add(Me.txtAdminName)
        Me.tabModeration.Controls.Add(Me.btnAdminKick)
        Me.tabModeration.Controls.Add(Me.btnAdminBan)
        Me.tabModeration.Controls.Add(Me.btnAdminWarp2Me)
        Me.tabModeration.Controls.Add(Me.btnAdminWarpMe2)
        Me.tabModeration.Controls.Add(Me.btnAdminSetAccess)
        Me.tabModeration.Controls.Add(Me.Label4)
        Me.tabModeration.Controls.Add(Me.Label5)
        Me.tabModeration.Controls.Add(Me.btnAdminWarpTo)
        Me.tabModeration.Controls.Add(Me.btnAdminSetSprite)
        Me.tabModeration.Location = New System.Drawing.Point(4, 22)
        Me.tabModeration.Name = "tabModeration"
        Me.tabModeration.Padding = New System.Windows.Forms.Padding(3)
        Me.tabModeration.Size = New System.Drawing.Size(250, 239)
        Me.tabModeration.TabIndex = 0
        Me.tabModeration.Text = "Moderation"
        Me.tabModeration.UseVisualStyleBackColor = True
        '
        'nudAdminSprite
        '
        Me.nudAdminSprite.Location = New System.Drawing.Point(80, 208)
        Me.nudAdminSprite.Name = "nudAdminSprite"
        Me.nudAdminSprite.Size = New System.Drawing.Size(48, 20)
        Me.nudAdminSprite.TabIndex = 33
        '
        'nudAdminMap
        '
        Me.nudAdminMap.Location = New System.Drawing.Point(80, 178)
        Me.nudAdminMap.Name = "nudAdminMap"
        Me.nudAdminMap.Size = New System.Drawing.Size(48, 20)
        Me.nudAdminMap.TabIndex = 32
        '
        'btnLevelUp
        '
        Me.btnLevelUp.Location = New System.Drawing.Point(32, 90)
        Me.btnLevelUp.Name = "btnLevelUp"
        Me.btnLevelUp.Size = New System.Drawing.Size(188, 22)
        Me.btnLevelUp.TabIndex = 31
        Me.btnLevelUp.Text = "Level Up"
        Me.btnLevelUp.UseVisualStyleBackColor = True
        '
        'cmbAccess
        '
        Me.cmbAccess.FormattingEnabled = True
        Me.cmbAccess.Items.AddRange(New Object() {"Normal Player", "Monitor (GM)", "Mapper", "Developer", "Creator"})
        Me.cmbAccess.Location = New System.Drawing.Point(57, 121)
        Me.cmbAccess.Name = "cmbAccess"
        Me.cmbAccess.Size = New System.Drawing.Size(185, 21)
        Me.cmbAccess.TabIndex = 17
        '
        'tabMapList
        '
        Me.tabMapList.Controls.Add(Me.lstMaps)
        Me.tabMapList.Controls.Add(Me.btnMapReport)
        Me.tabMapList.Location = New System.Drawing.Point(4, 22)
        Me.tabMapList.Name = "tabMapList"
        Me.tabMapList.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMapList.Size = New System.Drawing.Size(250, 239)
        Me.tabMapList.TabIndex = 2
        Me.tabMapList.Text = "Map List"
        Me.tabMapList.UseVisualStyleBackColor = True
        '
        'tabMapTools
        '
        Me.tabMapTools.Controls.Add(Me.nudSpawnItemAmount)
        Me.tabMapTools.Controls.Add(Me.cmbSpawnItem)
        Me.tabMapTools.Controls.Add(Me.btnRespawn)
        Me.tabMapTools.Controls.Add(Me.btnALoc)
        Me.tabMapTools.Controls.Add(Me.lblItemSpawn)
        Me.tabMapTools.Controls.Add(Me.lblSpawnItemAmount)
        Me.tabMapTools.Controls.Add(Me.btnSpawnItem)
        Me.tabMapTools.Location = New System.Drawing.Point(4, 22)
        Me.tabMapTools.Name = "tabMapTools"
        Me.tabMapTools.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMapTools.Size = New System.Drawing.Size(250, 239)
        Me.tabMapTools.TabIndex = 3
        Me.tabMapTools.Text = "Map Tools"
        Me.tabMapTools.UseVisualStyleBackColor = True
        '
        'nudSpawnItemAmount
        '
        Me.nudSpawnItemAmount.Location = New System.Drawing.Point(122, 119)
        Me.nudSpawnItemAmount.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudSpawnItemAmount.Name = "nudSpawnItemAmount"
        Me.nudSpawnItemAmount.Size = New System.Drawing.Size(120, 20)
        Me.nudSpawnItemAmount.TabIndex = 37
        '
        'cmbSpawnItem
        '
        Me.cmbSpawnItem.FormattingEnabled = True
        Me.cmbSpawnItem.Location = New System.Drawing.Point(83, 92)
        Me.cmbSpawnItem.Name = "cmbSpawnItem"
        Me.cmbSpawnItem.Size = New System.Drawing.Size(159, 21)
        Me.cmbSpawnItem.TabIndex = 36
        '
        'tabEditors
        '
        Me.tabEditors.Controls.Add(Me.btnPetEditor)
        Me.tabEditors.Controls.Add(Me.btnAutoMapper)
        Me.tabEditors.Controls.Add(Me.btnClassEditor)
        Me.tabEditors.Controls.Add(Me.btnRecipeEditor)
        Me.tabEditors.Controls.Add(Me.btnProjectiles)
        Me.tabEditors.Controls.Add(Me.btnQuest)
        Me.tabEditors.Controls.Add(Me.btnhouseEditor)
        Me.tabEditors.Controls.Add(Me.btnMapEditor)
        Me.tabEditors.Controls.Add(Me.btnItemEditor)
        Me.tabEditors.Controls.Add(Me.btnResourceEditor)
        Me.tabEditors.Controls.Add(Me.btnNPCEditor)
        Me.tabEditors.Controls.Add(Me.btnSkillEditor)
        Me.tabEditors.Controls.Add(Me.btnShopEditor)
        Me.tabEditors.Controls.Add(Me.btnAnimationEditor)
        Me.tabEditors.Location = New System.Drawing.Point(4, 22)
        Me.tabEditors.Name = "tabEditors"
        Me.tabEditors.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEditors.Size = New System.Drawing.Size(250, 239)
        Me.tabEditors.TabIndex = 4
        Me.tabEditors.Text = "Editors"
        Me.tabEditors.UseVisualStyleBackColor = True
        '
        'btnPetEditor
        '
        Me.btnPetEditor.Location = New System.Drawing.Point(130, 6)
        Me.btnPetEditor.Name = "btnPetEditor"
        Me.btnPetEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnPetEditor.TabIndex = 68
        Me.btnPetEditor.Text = "Pet Editor"
        Me.btnPetEditor.UseVisualStyleBackColor = True
        '
        'btnAutoMapper
        '
        Me.btnAutoMapper.Location = New System.Drawing.Point(6, 130)
        Me.btnAutoMapper.Name = "btnAutoMapper"
        Me.btnAutoMapper.Size = New System.Drawing.Size(112, 25)
        Me.btnAutoMapper.TabIndex = 67
        Me.btnAutoMapper.Text = "Map Editor (Auto)"
        Me.btnAutoMapper.UseVisualStyleBackColor = True
        '
        'btnClassEditor
        '
        Me.btnClassEditor.Location = New System.Drawing.Point(6, 37)
        Me.btnClassEditor.Name = "btnClassEditor"
        Me.btnClassEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnClassEditor.TabIndex = 66
        Me.btnClassEditor.Text = "Class Editor"
        Me.btnClassEditor.UseVisualStyleBackColor = True
        '
        'btnRecipeEditor
        '
        Me.btnRecipeEditor.Location = New System.Drawing.Point(130, 99)
        Me.btnRecipeEditor.Name = "btnRecipeEditor"
        Me.btnRecipeEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnRecipeEditor.TabIndex = 65
        Me.btnRecipeEditor.Text = "Recipe Editor"
        Me.btnRecipeEditor.UseVisualStyleBackColor = True
        '
        'btnProjectiles
        '
        Me.btnProjectiles.Location = New System.Drawing.Point(130, 37)
        Me.btnProjectiles.Name = "btnProjectiles"
        Me.btnProjectiles.Size = New System.Drawing.Size(112, 25)
        Me.btnProjectiles.TabIndex = 64
        Me.btnProjectiles.Text = "Projectiles Editor"
        Me.btnProjectiles.UseVisualStyleBackColor = True
        '
        'btnQuest
        '
        Me.btnQuest.Location = New System.Drawing.Point(130, 68)
        Me.btnQuest.Name = "btnQuest"
        Me.btnQuest.Size = New System.Drawing.Size(112, 25)
        Me.btnQuest.TabIndex = 62
        Me.btnQuest.Text = "Quest Editor"
        Me.btnQuest.UseVisualStyleBackColor = True
        '
        'btnhouseEditor
        '
        Me.btnhouseEditor.Location = New System.Drawing.Point(6, 68)
        Me.btnhouseEditor.Name = "btnhouseEditor"
        Me.btnhouseEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnhouseEditor.TabIndex = 63
        Me.btnhouseEditor.Text = "Houses Editor"
        Me.btnhouseEditor.UseVisualStyleBackColor = True
        '
        'btnMapEditor
        '
        Me.btnMapEditor.Location = New System.Drawing.Point(6, 161)
        Me.btnMapEditor.Name = "btnMapEditor"
        Me.btnMapEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnMapEditor.TabIndex = 55
        Me.btnMapEditor.Text = "Map Editor"
        Me.btnMapEditor.UseVisualStyleBackColor = True
        '
        'btnItemEditor
        '
        Me.btnItemEditor.Location = New System.Drawing.Point(6, 99)
        Me.btnItemEditor.Name = "btnItemEditor"
        Me.btnItemEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnItemEditor.TabIndex = 56
        Me.btnItemEditor.Text = "Item Editor"
        Me.btnItemEditor.UseVisualStyleBackColor = True
        '
        'btnResourceEditor
        '
        Me.btnResourceEditor.Location = New System.Drawing.Point(130, 130)
        Me.btnResourceEditor.Name = "btnResourceEditor"
        Me.btnResourceEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnResourceEditor.TabIndex = 57
        Me.btnResourceEditor.Text = "Resource Editor"
        Me.btnResourceEditor.UseVisualStyleBackColor = True
        '
        'btnNPCEditor
        '
        Me.btnNPCEditor.Location = New System.Drawing.Point(6, 192)
        Me.btnNPCEditor.Name = "btnNPCEditor"
        Me.btnNPCEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnNPCEditor.TabIndex = 58
        Me.btnNPCEditor.Text = "NPC Editor"
        Me.btnNPCEditor.UseVisualStyleBackColor = True
        '
        'btnSkillEditor
        '
        Me.btnSkillEditor.Location = New System.Drawing.Point(130, 192)
        Me.btnSkillEditor.Name = "btnSkillEditor"
        Me.btnSkillEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnSkillEditor.TabIndex = 59
        Me.btnSkillEditor.Text = "Skill Editor"
        Me.btnSkillEditor.UseVisualStyleBackColor = True
        '
        'btnShopEditor
        '
        Me.btnShopEditor.Location = New System.Drawing.Point(130, 161)
        Me.btnShopEditor.Name = "btnShopEditor"
        Me.btnShopEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnShopEditor.TabIndex = 60
        Me.btnShopEditor.Text = "Shop Editor"
        Me.btnShopEditor.UseVisualStyleBackColor = True
        '
        'btnAnimationEditor
        '
        Me.btnAnimationEditor.Location = New System.Drawing.Point(6, 6)
        Me.btnAnimationEditor.Name = "btnAnimationEditor"
        Me.btnAnimationEditor.Size = New System.Drawing.Size(112, 25)
        Me.btnAnimationEditor.TabIndex = 61
        Me.btnAnimationEditor.Text = "Animation Editor"
        Me.btnAnimationEditor.UseVisualStyleBackColor = True
        '
        'FrmAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(260, 270)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAdmin"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Admin Panel"
        Me.TabControl1.ResumeLayout(False)
        Me.tabModeration.ResumeLayout(False)
        Me.tabModeration.PerformLayout()
        CType(Me.nudAdminSprite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudAdminMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMapList.ResumeLayout(False)
        Me.tabMapTools.ResumeLayout(False)
        Me.tabMapTools.PerformLayout()
        CType(Me.nudSpawnItemAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabEditors.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnRespawn As Windows.Forms.Button
    Friend WithEvents btnMapReport As Windows.Forms.Button
    Friend WithEvents btnALoc As Windows.Forms.Button
    Friend WithEvents btnSpawnItem As Windows.Forms.Button
    Friend WithEvents lblSpawnItemAmount As Windows.Forms.Label
    Friend WithEvents lblItemSpawn As Windows.Forms.Label
    Friend WithEvents btnAdminSetSprite As Windows.Forms.Button
    Friend WithEvents btnAdminWarpTo As Windows.Forms.Button
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents btnAdminSetAccess As Windows.Forms.Button
    Friend WithEvents btnAdminWarpMe2 As Windows.Forms.Button
    Friend WithEvents btnAdminWarp2Me As Windows.Forms.Button
    Friend WithEvents btnAdminBan As Windows.Forms.Button
    Friend WithEvents btnAdminKick As Windows.Forms.Button
    Friend WithEvents txtAdminName As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents lstMaps As Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As Windows.Forms.ColumnHeader
    Friend WithEvents TabControl1 As Windows.Forms.TabControl
    Friend WithEvents tabModeration As Windows.Forms.TabPage
    Friend WithEvents tabMapList As Windows.Forms.TabPage
    Friend WithEvents tabMapTools As Windows.Forms.TabPage
    Friend WithEvents cmbAccess As Windows.Forms.ComboBox
    Friend WithEvents cmbSpawnItem As Windows.Forms.ComboBox
    Friend WithEvents nudAdminSprite As Windows.Forms.NumericUpDown
    Friend WithEvents nudAdminMap As Windows.Forms.NumericUpDown
    Friend WithEvents btnLevelUp As Windows.Forms.Button
    Friend WithEvents nudSpawnItemAmount As Windows.Forms.NumericUpDown
    Friend WithEvents tabEditors As Windows.Forms.TabPage
    Friend WithEvents btnPetEditor As Windows.Forms.Button
    Friend WithEvents btnAutoMapper As Windows.Forms.Button
    Friend WithEvents btnClassEditor As Windows.Forms.Button
    Friend WithEvents btnRecipeEditor As Windows.Forms.Button
    Friend WithEvents btnProjectiles As Windows.Forms.Button
    Friend WithEvents btnQuest As Windows.Forms.Button
    Friend WithEvents btnhouseEditor As Windows.Forms.Button
    Friend WithEvents btnMapEditor As Windows.Forms.Button
    Friend WithEvents btnItemEditor As Windows.Forms.Button
    Friend WithEvents btnResourceEditor As Windows.Forms.Button
    Friend WithEvents btnNPCEditor As Windows.Forms.Button
    Friend WithEvents btnSkillEditor As Windows.Forms.Button
    Friend WithEvents btnShopEditor As Windows.Forms.Button
    Friend WithEvents btnAnimationEditor As Windows.Forms.Button
End Class
