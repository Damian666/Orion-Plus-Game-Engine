﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEditor_Pet
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstIndex = New System.Windows.Forms.ListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblSkill4 = New System.Windows.Forms.Label()
        Me.lblSkill3 = New System.Windows.Forms.Label()
        Me.scrlSkill4 = New System.Windows.Forms.HScrollBar()
        Me.scrlSkill3 = New System.Windows.Forms.HScrollBar()
        Me.lblSkill2 = New System.Windows.Forms.Label()
        Me.lblSkill1 = New System.Windows.Forms.Label()
        Me.scrlSkill2 = New System.Windows.Forms.HScrollBar()
        Me.scrlSkill1 = New System.Windows.Forms.HScrollBar()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.pnlPetlevel = New System.Windows.Forms.Panel()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.cmbEvolve = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.scrlEvolveLvl = New System.Windows.Forms.HScrollBar()
        Me.lblEvolveLvl = New System.Windows.Forms.Label()
        Me.chkEvolve = New System.Windows.Forms.CheckBox()
        Me.lblmaxlevel = New System.Windows.Forms.Label()
        Me.lblPetPnts = New System.Windows.Forms.Label()
        Me.lblPetExp = New System.Windows.Forms.Label()
        Me.scrlMaxLevel = New System.Windows.Forms.HScrollBar()
        Me.scrlPetPnts = New System.Windows.Forms.HScrollBar()
        Me.scrlPetExp = New System.Windows.Forms.HScrollBar()
        Me.optDoNotLevel = New System.Windows.Forms.RadioButton()
        Me.optLevel = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.pnlCustomStats = New System.Windows.Forms.Panel()
        Me.lblLevel = New System.Windows.Forms.Label()
        Me.scrlLevel = New System.Windows.Forms.HScrollBar()
        Me.scrlStrength = New System.Windows.Forms.HScrollBar()
        Me.lblSpirit = New System.Windows.Forms.Label()
        Me.lblStrength = New System.Windows.Forms.Label()
        Me.scrlSpirit = New System.Windows.Forms.HScrollBar()
        Me.lblIntelligence = New System.Windows.Forms.Label()
        Me.lblVitality = New System.Windows.Forms.Label()
        Me.scrlLuck = New System.Windows.Forms.HScrollBar()
        Me.scrlVitality = New System.Windows.Forms.HScrollBar()
        Me.lblLuck = New System.Windows.Forms.Label()
        Me.scrlIntelligence = New System.Windows.Forms.HScrollBar()
        Me.scrlEndurance = New System.Windows.Forms.HScrollBar()
        Me.lblEndurance = New System.Windows.Forms.Label()
        Me.optAdoptStats = New System.Windows.Forms.RadioButton()
        Me.optCustomStats = New System.Windows.Forms.RadioButton()
        Me.scrlRange = New System.Windows.Forms.HScrollBar()
        Me.lblRange = New System.Windows.Forms.Label()
        Me.scrlSprite = New System.Windows.Forms.HScrollBar()
        Me.lblSprite = New System.Windows.Forms.Label()
        Me.picSprite = New System.Windows.Forms.PictureBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.pnlPetlevel.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.pnlCustomStats.SuspendLayout()
        CType(Me.picSprite, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstIndex)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(208, 503)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pet List"
        '
        'lstIndex
        '
        Me.lstIndex.FormattingEnabled = True
        Me.lstIndex.Location = New System.Drawing.Point(6, 19)
        Me.lstIndex.Name = "lstIndex"
        Me.lstIndex.Size = New System.Drawing.Size(198, 472)
        Me.lstIndex.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.scrlRange)
        Me.GroupBox2.Controls.Add(Me.lblRange)
        Me.GroupBox2.Controls.Add(Me.scrlSprite)
        Me.GroupBox2.Controls.Add(Me.lblSprite)
        Me.GroupBox2.Controls.Add(Me.picSprite)
        Me.GroupBox2.Controls.Add(Me.txtName)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(213, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(389, 537)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pet Properties"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblSkill4)
        Me.GroupBox5.Controls.Add(Me.lblSkill3)
        Me.GroupBox5.Controls.Add(Me.scrlSkill4)
        Me.GroupBox5.Controls.Add(Me.scrlSkill3)
        Me.GroupBox5.Controls.Add(Me.lblSkill2)
        Me.GroupBox5.Controls.Add(Me.lblSkill1)
        Me.GroupBox5.Controls.Add(Me.scrlSkill2)
        Me.GroupBox5.Controls.Add(Me.scrlSkill1)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 436)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(375, 89)
        Me.GroupBox5.TabIndex = 9
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Start Skills"
        '
        'lblSkill4
        '
        Me.lblSkill4.AutoSize = True
        Me.lblSkill4.Location = New System.Drawing.Point(214, 67)
        Me.lblSkill4.Name = "lblSkill4"
        Me.lblSkill4.Size = New System.Drawing.Size(67, 13)
        Me.lblSkill4.TabIndex = 11
        Me.lblSkill4.Text = "Skill 4: None"
        '
        'lblSkill3
        '
        Me.lblSkill3.AutoSize = True
        Me.lblSkill3.Location = New System.Drawing.Point(6, 67)
        Me.lblSkill3.Name = "lblSkill3"
        Me.lblSkill3.Size = New System.Drawing.Size(67, 13)
        Me.lblSkill3.TabIndex = 10
        Me.lblSkill3.Text = "Skill 3: None"
        '
        'scrlSkill4
        '
        Me.scrlSkill4.LargeChange = 1
        Me.scrlSkill4.Location = New System.Drawing.Point(217, 50)
        Me.scrlSkill4.Name = "scrlSkill4"
        Me.scrlSkill4.Size = New System.Drawing.Size(154, 17)
        Me.scrlSkill4.TabIndex = 9
        Me.scrlSkill4.Value = 50
        '
        'scrlSkill3
        '
        Me.scrlSkill3.LargeChange = 1
        Me.scrlSkill3.Location = New System.Drawing.Point(6, 50)
        Me.scrlSkill3.Name = "scrlSkill3"
        Me.scrlSkill3.Size = New System.Drawing.Size(167, 17)
        Me.scrlSkill3.TabIndex = 8
        Me.scrlSkill3.Value = 50
        '
        'lblSkill2
        '
        Me.lblSkill2.AutoSize = True
        Me.lblSkill2.Location = New System.Drawing.Point(214, 33)
        Me.lblSkill2.Name = "lblSkill2"
        Me.lblSkill2.Size = New System.Drawing.Size(67, 13)
        Me.lblSkill2.TabIndex = 7
        Me.lblSkill2.Text = "Skill 2: None"
        '
        'lblSkill1
        '
        Me.lblSkill1.AutoSize = True
        Me.lblSkill1.Location = New System.Drawing.Point(6, 33)
        Me.lblSkill1.Name = "lblSkill1"
        Me.lblSkill1.Size = New System.Drawing.Size(67, 13)
        Me.lblSkill1.TabIndex = 6
        Me.lblSkill1.Text = "Skill 1: None"
        '
        'scrlSkill2
        '
        Me.scrlSkill2.LargeChange = 1
        Me.scrlSkill2.Location = New System.Drawing.Point(217, 16)
        Me.scrlSkill2.Name = "scrlSkill2"
        Me.scrlSkill2.Size = New System.Drawing.Size(152, 17)
        Me.scrlSkill2.TabIndex = 5
        Me.scrlSkill2.Value = 50
        '
        'scrlSkill1
        '
        Me.scrlSkill1.LargeChange = 1
        Me.scrlSkill1.Location = New System.Drawing.Point(6, 16)
        Me.scrlSkill1.Name = "scrlSkill1"
        Me.scrlSkill1.Size = New System.Drawing.Size(167, 17)
        Me.scrlSkill1.TabIndex = 4
        Me.scrlSkill1.Value = 50
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.pnlPetlevel)
        Me.GroupBox4.Controls.Add(Me.optDoNotLevel)
        Me.GroupBox4.Controls.Add(Me.optLevel)
        Me.GroupBox4.Location = New System.Drawing.Point(4, 238)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(377, 192)
        Me.GroupBox4.TabIndex = 8
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Leveling"
        '
        'pnlPetlevel
        '
        Me.pnlPetlevel.Controls.Add(Me.GroupBox6)
        Me.pnlPetlevel.Controls.Add(Me.lblmaxlevel)
        Me.pnlPetlevel.Controls.Add(Me.lblPetPnts)
        Me.pnlPetlevel.Controls.Add(Me.lblPetExp)
        Me.pnlPetlevel.Controls.Add(Me.scrlMaxLevel)
        Me.pnlPetlevel.Controls.Add(Me.scrlPetPnts)
        Me.pnlPetlevel.Controls.Add(Me.scrlPetExp)
        Me.pnlPetlevel.Location = New System.Drawing.Point(6, 42)
        Me.pnlPetlevel.Name = "pnlPetlevel"
        Me.pnlPetlevel.Size = New System.Drawing.Size(365, 145)
        Me.pnlPetlevel.TabIndex = 2
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.cmbEvolve)
        Me.GroupBox6.Controls.Add(Me.Label2)
        Me.GroupBox6.Controls.Add(Me.scrlEvolveLvl)
        Me.GroupBox6.Controls.Add(Me.lblEvolveLvl)
        Me.GroupBox6.Controls.Add(Me.chkEvolve)
        Me.GroupBox6.Location = New System.Drawing.Point(3, 65)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(359, 75)
        Me.GroupBox6.TabIndex = 9
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Evolution"
        '
        'cmbEvolve
        '
        Me.cmbEvolve.FormattingEnabled = True
        Me.cmbEvolve.Location = New System.Drawing.Point(124, 45)
        Me.cmbEvolve.Name = "cmbEvolve"
        Me.cmbEvolve.Size = New System.Drawing.Size(229, 21)
        Me.cmbEvolve.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Evolves intoo:"
        '
        'scrlEvolveLvl
        '
        Me.scrlEvolveLvl.LargeChange = 1
        Me.scrlEvolveLvl.Location = New System.Drawing.Point(234, 19)
        Me.scrlEvolveLvl.Name = "scrlEvolveLvl"
        Me.scrlEvolveLvl.Size = New System.Drawing.Size(119, 17)
        Me.scrlEvolveLvl.TabIndex = 2
        '
        'lblEvolveLvl
        '
        Me.lblEvolveLvl.AutoSize = True
        Me.lblEvolveLvl.Location = New System.Drawing.Point(121, 20)
        Me.lblEvolveLvl.Name = "lblEvolveLvl"
        Me.lblEvolveLvl.Size = New System.Drawing.Size(101, 13)
        Me.lblEvolveLvl.TabIndex = 1
        Me.lblEvolveLvl.Text = "Evolves on Level: 1"
        '
        'chkEvolve
        '
        Me.chkEvolve.AutoSize = True
        Me.chkEvolve.Location = New System.Drawing.Point(6, 19)
        Me.chkEvolve.Name = "chkEvolve"
        Me.chkEvolve.Size = New System.Drawing.Size(106, 17)
        Me.chkEvolve.TabIndex = 0
        Me.chkEvolve.Text = "Pet Can Evolve?"
        Me.chkEvolve.UseVisualStyleBackColor = True
        '
        'lblmaxlevel
        '
        Me.lblmaxlevel.AutoSize = True
        Me.lblmaxlevel.Location = New System.Drawing.Point(249, 43)
        Me.lblmaxlevel.Name = "lblmaxlevel"
        Me.lblmaxlevel.Size = New System.Drawing.Size(68, 13)
        Me.lblmaxlevel.TabIndex = 8
        Me.lblmaxlevel.Text = "Max Level: 1"
        '
        'lblPetPnts
        '
        Me.lblPetPnts.AutoSize = True
        Me.lblPetPnts.Location = New System.Drawing.Point(124, 43)
        Me.lblPetPnts.Name = "lblPetPnts"
        Me.lblPetPnts.Size = New System.Drawing.Size(96, 13)
        Me.lblPetPnts.TabIndex = 7
        Me.lblPetPnts.Text = "Points Per Level: 5"
        '
        'lblPetExp
        '
        Me.lblPetExp.AutoSize = True
        Me.lblPetExp.Location = New System.Drawing.Point(7, 43)
        Me.lblPetExp.Name = "lblPetExp"
        Me.lblPetExp.Size = New System.Drawing.Size(85, 13)
        Me.lblPetExp.TabIndex = 6
        Me.lblPetExp.Text = "Exp. Gain: 100%"
        '
        'scrlMaxLevel
        '
        Me.scrlMaxLevel.LargeChange = 1
        Me.scrlMaxLevel.Location = New System.Drawing.Point(252, 17)
        Me.scrlMaxLevel.Name = "scrlMaxLevel"
        Me.scrlMaxLevel.Size = New System.Drawing.Size(104, 17)
        Me.scrlMaxLevel.TabIndex = 5
        '
        'scrlPetPnts
        '
        Me.scrlPetPnts.Location = New System.Drawing.Point(127, 17)
        Me.scrlPetPnts.Name = "scrlPetPnts"
        Me.scrlPetPnts.Size = New System.Drawing.Size(104, 17)
        Me.scrlPetPnts.TabIndex = 4
        '
        'scrlPetExp
        '
        Me.scrlPetExp.LargeChange = 1
        Me.scrlPetExp.Location = New System.Drawing.Point(10, 17)
        Me.scrlPetExp.Name = "scrlPetExp"
        Me.scrlPetExp.Size = New System.Drawing.Size(104, 17)
        Me.scrlPetExp.TabIndex = 3
        Me.scrlPetExp.Value = 50
        '
        'optDoNotLevel
        '
        Me.optDoNotLevel.AutoSize = True
        Me.optDoNotLevel.Location = New System.Drawing.Point(194, 19)
        Me.optDoNotLevel.Name = "optDoNotLevel"
        Me.optDoNotLevel.Size = New System.Drawing.Size(111, 17)
        Me.optDoNotLevel.TabIndex = 1
        Me.optDoNotLevel.TabStop = True
        Me.optDoNotLevel.Text = "Does not LevelUp"
        Me.optDoNotLevel.UseVisualStyleBackColor = True
        '
        'optLevel
        '
        Me.optLevel.AutoSize = True
        Me.optLevel.Location = New System.Drawing.Point(6, 19)
        Me.optLevel.Name = "optLevel"
        Me.optLevel.Size = New System.Drawing.Size(121, 17)
        Me.optLevel.TabIndex = 0
        Me.optLevel.TabStop = True
        Me.optLevel.Text = "Level by Experience"
        Me.optLevel.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.pnlCustomStats)
        Me.GroupBox3.Controls.Add(Me.optAdoptStats)
        Me.GroupBox3.Controls.Add(Me.optCustomStats)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 70)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(377, 162)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Starting Stats"
        '
        'pnlCustomStats
        '
        Me.pnlCustomStats.Controls.Add(Me.lblLevel)
        Me.pnlCustomStats.Controls.Add(Me.scrlLevel)
        Me.pnlCustomStats.Controls.Add(Me.scrlStrength)
        Me.pnlCustomStats.Controls.Add(Me.lblSpirit)
        Me.pnlCustomStats.Controls.Add(Me.lblStrength)
        Me.pnlCustomStats.Controls.Add(Me.scrlSpirit)
        Me.pnlCustomStats.Controls.Add(Me.lblIntelligence)
        Me.pnlCustomStats.Controls.Add(Me.lblVitality)
        Me.pnlCustomStats.Controls.Add(Me.scrlLuck)
        Me.pnlCustomStats.Controls.Add(Me.scrlVitality)
        Me.pnlCustomStats.Controls.Add(Me.lblLuck)
        Me.pnlCustomStats.Controls.Add(Me.scrlIntelligence)
        Me.pnlCustomStats.Controls.Add(Me.scrlEndurance)
        Me.pnlCustomStats.Controls.Add(Me.lblEndurance)
        Me.pnlCustomStats.Location = New System.Drawing.Point(6, 42)
        Me.pnlCustomStats.Name = "pnlCustomStats"
        Me.pnlCustomStats.Size = New System.Drawing.Size(365, 112)
        Me.pnlCustomStats.TabIndex = 14
        '
        'lblLevel
        '
        Me.lblLevel.AutoSize = True
        Me.lblLevel.Location = New System.Drawing.Point(7, 89)
        Me.lblLevel.Name = "lblLevel"
        Me.lblLevel.Size = New System.Drawing.Size(45, 13)
        Me.lblLevel.TabIndex = 15
        Me.lblLevel.Text = "Level: 0"
        '
        'scrlLevel
        '
        Me.scrlLevel.LargeChange = 1
        Me.scrlLevel.Location = New System.Drawing.Point(127, 85)
        Me.scrlLevel.Name = "scrlLevel"
        Me.scrlLevel.Size = New System.Drawing.Size(104, 17)
        Me.scrlLevel.TabIndex = 14
        '
        'scrlStrength
        '
        Me.scrlStrength.Location = New System.Drawing.Point(10, 9)
        Me.scrlStrength.Name = "scrlStrength"
        Me.scrlStrength.Size = New System.Drawing.Size(104, 17)
        Me.scrlStrength.TabIndex = 2
        '
        'lblSpirit
        '
        Me.lblSpirit.AutoSize = True
        Me.lblSpirit.Location = New System.Drawing.Point(249, 65)
        Me.lblSpirit.Name = "lblSpirit"
        Me.lblSpirit.Size = New System.Drawing.Size(42, 13)
        Me.lblSpirit.TabIndex = 13
        Me.lblSpirit.Text = "Spirit: 0"
        '
        'lblStrength
        '
        Me.lblStrength.AutoSize = True
        Me.lblStrength.Location = New System.Drawing.Point(7, 26)
        Me.lblStrength.Name = "lblStrength"
        Me.lblStrength.Size = New System.Drawing.Size(59, 13)
        Me.lblStrength.TabIndex = 5
        Me.lblStrength.Text = "Strength: 0"
        '
        'scrlSpirit
        '
        Me.scrlSpirit.Location = New System.Drawing.Point(252, 48)
        Me.scrlSpirit.Name = "scrlSpirit"
        Me.scrlSpirit.Size = New System.Drawing.Size(104, 17)
        Me.scrlSpirit.TabIndex = 10
        '
        'lblIntelligence
        '
        Me.lblIntelligence.AutoSize = True
        Me.lblIntelligence.Location = New System.Drawing.Point(124, 65)
        Me.lblIntelligence.Name = "lblIntelligence"
        Me.lblIntelligence.Size = New System.Drawing.Size(73, 13)
        Me.lblIntelligence.TabIndex = 12
        Me.lblIntelligence.Text = "Intelligence: 0"
        '
        'lblVitality
        '
        Me.lblVitality.AutoSize = True
        Me.lblVitality.Location = New System.Drawing.Point(249, 26)
        Me.lblVitality.Name = "lblVitality"
        Me.lblVitality.Size = New System.Drawing.Size(49, 13)
        Me.lblVitality.TabIndex = 7
        Me.lblVitality.Text = "Vitality: 0"
        '
        'scrlLuck
        '
        Me.scrlLuck.Location = New System.Drawing.Point(10, 48)
        Me.scrlLuck.Name = "scrlLuck"
        Me.scrlLuck.Size = New System.Drawing.Size(104, 17)
        Me.scrlLuck.TabIndex = 8
        '
        'scrlVitality
        '
        Me.scrlVitality.Location = New System.Drawing.Point(252, 9)
        Me.scrlVitality.Name = "scrlVitality"
        Me.scrlVitality.Size = New System.Drawing.Size(104, 17)
        Me.scrlVitality.TabIndex = 4
        '
        'lblLuck
        '
        Me.lblLuck.AutoSize = True
        Me.lblLuck.Location = New System.Drawing.Point(7, 65)
        Me.lblLuck.Name = "lblLuck"
        Me.lblLuck.Size = New System.Drawing.Size(43, 13)
        Me.lblLuck.TabIndex = 11
        Me.lblLuck.Text = "Luck: 0"
        '
        'scrlIntelligence
        '
        Me.scrlIntelligence.Location = New System.Drawing.Point(127, 48)
        Me.scrlIntelligence.Name = "scrlIntelligence"
        Me.scrlIntelligence.Size = New System.Drawing.Size(104, 17)
        Me.scrlIntelligence.TabIndex = 9
        '
        'scrlEndurance
        '
        Me.scrlEndurance.Location = New System.Drawing.Point(127, 9)
        Me.scrlEndurance.Name = "scrlEndurance"
        Me.scrlEndurance.Size = New System.Drawing.Size(104, 17)
        Me.scrlEndurance.TabIndex = 3
        '
        'lblEndurance
        '
        Me.lblEndurance.AutoSize = True
        Me.lblEndurance.Location = New System.Drawing.Point(124, 26)
        Me.lblEndurance.Name = "lblEndurance"
        Me.lblEndurance.Size = New System.Drawing.Size(71, 13)
        Me.lblEndurance.TabIndex = 6
        Me.lblEndurance.Text = "Endurance: 0"
        '
        'optAdoptStats
        '
        Me.optAdoptStats.AutoSize = True
        Me.optAdoptStats.Location = New System.Drawing.Point(177, 19)
        Me.optAdoptStats.Name = "optAdoptStats"
        Me.optAdoptStats.Size = New System.Drawing.Size(121, 17)
        Me.optAdoptStats.TabIndex = 1
        Me.optAdoptStats.TabStop = True
        Me.optAdoptStats.Text = "Adopt Owner's Stats"
        Me.optAdoptStats.UseVisualStyleBackColor = True
        '
        'optCustomStats
        '
        Me.optCustomStats.AutoSize = True
        Me.optCustomStats.Location = New System.Drawing.Point(6, 19)
        Me.optCustomStats.Name = "optCustomStats"
        Me.optCustomStats.Size = New System.Drawing.Size(87, 17)
        Me.optCustomStats.TabIndex = 0
        Me.optCustomStats.TabStop = True
        Me.optCustomStats.Text = "Custom Stats"
        Me.optCustomStats.UseVisualStyleBackColor = True
        '
        'scrlRange
        '
        Me.scrlRange.LargeChange = 1
        Me.scrlRange.Location = New System.Drawing.Point(246, 47)
        Me.scrlRange.Name = "scrlRange"
        Me.scrlRange.Size = New System.Drawing.Size(85, 17)
        Me.scrlRange.TabIndex = 6
        '
        'lblRange
        '
        Me.lblRange.AutoSize = True
        Me.lblRange.Location = New System.Drawing.Point(180, 50)
        Me.lblRange.Name = "lblRange"
        Me.lblRange.Size = New System.Drawing.Size(51, 13)
        Me.lblRange.TabIndex = 5
        Me.lblRange.Text = "Range: 0"
        '
        'scrlSprite
        '
        Me.scrlSprite.LargeChange = 1
        Me.scrlSprite.Location = New System.Drawing.Point(77, 47)
        Me.scrlSprite.Name = "scrlSprite"
        Me.scrlSprite.Size = New System.Drawing.Size(85, 17)
        Me.scrlSprite.TabIndex = 4
        '
        'lblSprite
        '
        Me.lblSprite.AutoSize = True
        Me.lblSprite.Location = New System.Drawing.Point(6, 50)
        Me.lblSprite.Name = "lblSprite"
        Me.lblSprite.Size = New System.Drawing.Size(46, 13)
        Me.lblSprite.TabIndex = 3
        Me.lblSprite.Text = "Sprite: 0"
        '
        'picSprite
        '
        Me.picSprite.BackColor = System.Drawing.Color.Black
        Me.picSprite.Location = New System.Drawing.Point(335, 16)
        Me.picSprite.Name = "picSprite"
        Me.picSprite.Size = New System.Drawing.Size(48, 48)
        Me.picSprite.TabIndex = 2
        Me.picSprite.TabStop = False
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(50, 16)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(279, 20)
        Me.txtName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name:"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(3, 512)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(104, 23)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(113, 512)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(95, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'FrmEditor_Pet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(606, 542)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmEditor_Pet"
        Me.Text = "Pet Editor"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.pnlPetlevel.ResumeLayout(False)
        Me.pnlPetlevel.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.pnlCustomStats.ResumeLayout(False)
        Me.pnlCustomStats.PerformLayout()
        CType(Me.picSprite, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lstIndex As ListBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents scrlSprite As HScrollBar
    Friend WithEvents lblSprite As Label
    Friend WithEvents picSprite As PictureBox
    Friend WithEvents txtName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents scrlRange As HScrollBar
    Friend WithEvents lblRange As Label
    Friend WithEvents scrlVitality As HScrollBar
    Friend WithEvents scrlEndurance As HScrollBar
    Friend WithEvents scrlStrength As HScrollBar
    Friend WithEvents optAdoptStats As RadioButton
    Friend WithEvents optCustomStats As RadioButton
    Friend WithEvents lblSpirit As Label
    Friend WithEvents lblIntelligence As Label
    Friend WithEvents lblLuck As Label
    Friend WithEvents scrlSpirit As HScrollBar
    Friend WithEvents scrlIntelligence As HScrollBar
    Friend WithEvents scrlLuck As HScrollBar
    Friend WithEvents lblVitality As Label
    Friend WithEvents lblEndurance As Label
    Friend WithEvents lblStrength As Label
    Friend WithEvents pnlCustomStats As Panel
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents optDoNotLevel As RadioButton
    Friend WithEvents optLevel As RadioButton
    Friend WithEvents pnlPetlevel As Panel
    Friend WithEvents lblmaxlevel As Label
    Friend WithEvents lblPetPnts As Label
    Friend WithEvents lblPetExp As Label
    Friend WithEvents scrlMaxLevel As HScrollBar
    Friend WithEvents scrlPetPnts As HScrollBar
    Friend WithEvents scrlPetExp As HScrollBar
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents lblSkill2 As Label
    Friend WithEvents lblSkill1 As Label
    Friend WithEvents scrlSkill2 As HScrollBar
    Friend WithEvents scrlSkill1 As HScrollBar
    Friend WithEvents lblSkill4 As Label
    Friend WithEvents lblSkill3 As Label
    Friend WithEvents scrlSkill4 As HScrollBar
    Friend WithEvents scrlSkill3 As HScrollBar
    Friend WithEvents lblLevel As Label
    Friend WithEvents scrlLevel As HScrollBar
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents cmbEvolve As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents scrlEvolveLvl As HScrollBar
    Friend WithEvents lblEvolveLvl As Label
    Friend WithEvents chkEvolve As CheckBox
End Class
