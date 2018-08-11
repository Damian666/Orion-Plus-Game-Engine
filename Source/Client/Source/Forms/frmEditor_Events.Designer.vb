Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEditor_Events
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
        Dim TreeNode59 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Show Text")
        Dim TreeNode60 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Show Choices")
        Dim TreeNode61 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Add Chatbox Text")
        Dim TreeNode62 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Show ChatBubble")
        Dim TreeNode63 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Messages", New System.Windows.Forms.TreeNode() {TreeNode59, TreeNode60, TreeNode61, TreeNode62})
        Dim TreeNode64 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Player Variable")
        Dim TreeNode65 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Player Switch")
        Dim TreeNode66 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Self Switch")
        Dim TreeNode67 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Event Processing", New System.Windows.Forms.TreeNode() {TreeNode64, TreeNode65, TreeNode66})
        Dim TreeNode68 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Conditional Branch")
        Dim TreeNode69 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stop Event Processing")
        Dim TreeNode70 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Label")
        Dim TreeNode71 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("GoTo Label")
        Dim TreeNode72 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Flow Control", New System.Windows.Forms.TreeNode() {TreeNode68, TreeNode69, TreeNode70, TreeNode71})
        Dim TreeNode73 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Items")
        Dim TreeNode74 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Restore HP")
        Dim TreeNode75 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Restore MP")
        Dim TreeNode76 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Level Up")
        Dim TreeNode77 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Level")
        Dim TreeNode78 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Skills")
        Dim TreeNode79 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Class")
        Dim TreeNode80 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Sprite")
        Dim TreeNode81 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Gender")
        Dim TreeNode82 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change PK")
        Dim TreeNode83 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Give Experience")
        Dim TreeNode84 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Player Options", New System.Windows.Forms.TreeNode() {TreeNode73, TreeNode74, TreeNode75, TreeNode76, TreeNode77, TreeNode78, TreeNode79, TreeNode80, TreeNode81, TreeNode82, TreeNode83})
        Dim TreeNode85 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Warp Player")
        Dim TreeNode86 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Move Route")
        Dim TreeNode87 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Wait for Route Completion")
        Dim TreeNode88 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Force Spawn Npc")
        Dim TreeNode89 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Hold Player")
        Dim TreeNode90 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Release Player")
        Dim TreeNode91 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Movement", New System.Windows.Forms.TreeNode() {TreeNode85, TreeNode86, TreeNode87, TreeNode88, TreeNode89, TreeNode90})
        Dim TreeNode92 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Animation")
        Dim TreeNode93 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Animation", New System.Windows.Forms.TreeNode() {TreeNode92})
        Dim TreeNode94 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Begin Quest")
        Dim TreeNode95 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Complete Task")
        Dim TreeNode96 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("End Quest")
        Dim TreeNode97 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Questing", New System.Windows.Forms.TreeNode() {TreeNode94, TreeNode95, TreeNode96})
        Dim TreeNode98 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Fog")
        Dim TreeNode99 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Weather")
        Dim TreeNode100 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Map Tinting")
        Dim TreeNode101 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Map Functions", New System.Windows.Forms.TreeNode() {TreeNode98, TreeNode99, TreeNode100})
        Dim TreeNode102 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Play BGM")
        Dim TreeNode103 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stop BGM")
        Dim TreeNode104 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Play Sound")
        Dim TreeNode105 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stop Sounds")
        Dim TreeNode106 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Music and Sound", New System.Windows.Forms.TreeNode() {TreeNode102, TreeNode103, TreeNode104, TreeNode105})
        Dim TreeNode107 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Wait...")
        Dim TreeNode108 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Access")
        Dim TreeNode109 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Custom Script")
        Dim TreeNode110 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Etc...", New System.Windows.Forms.TreeNode() {TreeNode107, TreeNode108, TreeNode109})
        Dim TreeNode111 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Open Bank")
        Dim TreeNode112 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Open Shop")
        Dim TreeNode113 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Shop and Bank", New System.Windows.Forms.TreeNode() {TreeNode111, TreeNode112})
        Dim TreeNode114 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fade In")
        Dim TreeNode115 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fade Out")
        Dim TreeNode116 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("CutScene Options", New System.Windows.Forms.TreeNode() {TreeNode114, TreeNode115})
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEditor_Events))
        Dim ListViewGroup10 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Movement", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup11 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Wait", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup12 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Turning", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup13 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Speed", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup14 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Walk Animation", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup15 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Fixed Direction", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup16 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("WalkThrough", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup17 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Set Position", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup18 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Set Graphic", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem44 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move Up")
        Dim ListViewItem45 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move Down")
        Dim ListViewItem46 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move left")
        Dim ListViewItem47 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move Right")
        Dim ListViewItem48 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move Randomly")
        Dim ListViewItem49 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move To Player***")
        Dim ListViewItem50 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move from Player***")
        Dim ListViewItem51 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Step Forwards")
        Dim ListViewItem52 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Step Backwards")
        Dim ListViewItem53 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Wait 100Ms")
        Dim ListViewItem54 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Wait 500Ms")
        Dim ListViewItem55 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Wait 1000Ms")
        Dim ListViewItem56 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn Up")
        Dim ListViewItem57 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn Down")
        Dim ListViewItem58 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn Left")
        Dim ListViewItem59 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn Right")
        Dim ListViewItem60 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn 90DG Right")
        Dim ListViewItem61 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn 90DG Left")
        Dim ListViewItem62 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn 180DG")
        Dim ListViewItem63 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn Randomly")
        Dim ListViewItem64 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn To Player***")
        Dim ListViewItem65 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn From Player***")
        Dim ListViewItem66 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed 8x Slower")
        Dim ListViewItem67 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed 4x Slower")
        Dim ListViewItem68 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed 2x Slower")
        Dim ListViewItem69 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed To Normal")
        Dim ListViewItem70 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed 2x Faster")
        Dim ListViewItem71 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed 4x Faster")
        Dim ListViewItem72 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Freq. To Lowest")
        Dim ListViewItem73 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Freq. To Lower")
        Dim ListViewItem74 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Freq. To Normal")
        Dim ListViewItem75 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Freq. To Higher")
        Dim ListViewItem76 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Freq. To Highest")
        Dim ListViewItem77 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Walking Animation ON")
        Dim ListViewItem78 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Walking Animation OFF")
        Dim ListViewItem79 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Fixed Direction ON")
        Dim ListViewItem80 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Fixed Direction OFF")
        Dim ListViewItem81 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Walkthrough ON")
        Dim ListViewItem82 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Walkthrough ON")
        Dim ListViewItem83 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Position Below Player")
        Dim ListViewItem84 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set PositionWith Player")
        Dim ListViewItem85 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Position Above Player")
        Dim ListViewItem86 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Graphic...")
        Me.tvCommands = New System.Windows.Forms.TreeView()
        Me.fraPageSetUp = New System.Windows.Forms.GroupBox()
        Me.chkGlobal = New System.Windows.Forms.CheckBox()
        Me.btnClearPage = New System.Windows.Forms.Button()
        Me.btnDeletePage = New System.Windows.Forms.Button()
        Me.btnPastePage = New System.Windows.Forms.Button()
        Me.btnCopyPage = New System.Windows.Forms.Button()
        Me.btnNewPage = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.DarkLabel1 = New System.Windows.Forms.Label()
        Me.tabPages = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.pnlTabPage = New System.Windows.Forms.Panel()
        Me.fraCommands = New System.Windows.Forms.Panel()
        Me.btnCancelCommand = New System.Windows.Forms.Button()
        Me.DarkGroupBox8 = New System.Windows.Forms.GroupBox()
        Me.btnClearCommand = New System.Windows.Forms.Button()
        Me.btnDeleteCommand = New System.Windows.Forms.Button()
        Me.btnEditCommand = New System.Windows.Forms.Button()
        Me.btnAddCommand = New System.Windows.Forms.Button()
        Me.lstCommands = New System.Windows.Forms.ListBox()
        Me.DarkLabel10 = New System.Windows.Forms.Label()
        Me.DarkLabel9 = New System.Windows.Forms.Label()
        Me.DarkGroupBox7 = New System.Windows.Forms.GroupBox()
        Me.cmbEventQuest = New System.Windows.Forms.ComboBox()
        Me.DarkLabel8 = New System.Windows.Forms.Label()
        Me.DarkGroupBox5 = New System.Windows.Forms.GroupBox()
        Me.cmbTrigger = New System.Windows.Forms.ComboBox()
        Me.DarkGroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cmbPositioning = New System.Windows.Forms.ComboBox()
        Me.DarkGroupBox3 = New System.Windows.Forms.GroupBox()
        Me.DarkLabel7 = New System.Windows.Forms.Label()
        Me.cmbMoveFreq = New System.Windows.Forms.ComboBox()
        Me.DarkLabel6 = New System.Windows.Forms.Label()
        Me.cmbMoveSpeed = New System.Windows.Forms.ComboBox()
        Me.btnMoveRoute = New System.Windows.Forms.Button()
        Me.cmbMoveType = New System.Windows.Forms.ComboBox()
        Me.DarkLabel5 = New System.Windows.Forms.Label()
        Me.DarkGroupBox2 = New System.Windows.Forms.GroupBox()
        Me.picGraphic = New System.Windows.Forms.PictureBox()
        Me.DarkGroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbSelfSwitchCompare = New System.Windows.Forms.ComboBox()
        Me.DarkLabel4 = New System.Windows.Forms.Label()
        Me.cmbSelfSwitch = New System.Windows.Forms.ComboBox()
        Me.chkSelfSwitch = New System.Windows.Forms.CheckBox()
        Me.cmbHasItem = New System.Windows.Forms.ComboBox()
        Me.chkHasItem = New System.Windows.Forms.CheckBox()
        Me.cmbPlayerSwitchCompare = New System.Windows.Forms.ComboBox()
        Me.DarkLabel3 = New System.Windows.Forms.Label()
        Me.cmbPlayerSwitch = New System.Windows.Forms.ComboBox()
        Me.chkPlayerSwitch = New System.Windows.Forms.CheckBox()
        Me.nudPlayerVariable = New System.Windows.Forms.NumericUpDown()
        Me.cmbPlayervarCompare = New System.Windows.Forms.ComboBox()
        Me.DarkLabel2 = New System.Windows.Forms.Label()
        Me.cmbPlayerVar = New System.Windows.Forms.ComboBox()
        Me.chkPlayerVar = New System.Windows.Forms.CheckBox()
        Me.DarkGroupBox6 = New System.Windows.Forms.GroupBox()
        Me.chkShowName = New System.Windows.Forms.CheckBox()
        Me.chkWalkThrough = New System.Windows.Forms.CheckBox()
        Me.chkDirFix = New System.Windows.Forms.CheckBox()
        Me.chkWalkAnim = New System.Windows.Forms.CheckBox()
        Me.btnLabeling = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.fraMoveRoute = New System.Windows.Forms.GroupBox()
        Me.btnMoveRouteOk = New System.Windows.Forms.Button()
        Me.btnMoveRouteCancel = New System.Windows.Forms.Button()
        Me.chkRepeatRoute = New System.Windows.Forms.CheckBox()
        Me.chkIgnoreMove = New System.Windows.Forms.CheckBox()
        Me.DarkGroupBox10 = New System.Windows.Forms.GroupBox()
        Me.lstvwMoveRoute = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstMoveRoute = New System.Windows.Forms.ListBox()
        Me.cmbEvent = New System.Windows.Forms.ComboBox()
        Me.fraGraphic = New System.Windows.Forms.GroupBox()
        Me.pnlGraphicSel = New System.Windows.Forms.Panel()
        Me.picGraphicSel = New System.Windows.Forms.PictureBox()
        Me.btnGraphicOk = New System.Windows.Forms.Button()
        Me.btnGraphicCancel = New System.Windows.Forms.Button()
        Me.DarkLabel13 = New System.Windows.Forms.Label()
        Me.nudGraphic = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel12 = New System.Windows.Forms.Label()
        Me.cmbGraphic = New System.Windows.Forms.ComboBox()
        Me.DarkLabel11 = New System.Windows.Forms.Label()
        Me.fraDialogue = New System.Windows.Forms.GroupBox()
        Me.fraPlayAnimation = New System.Windows.Forms.GroupBox()
        Me.btnPlayAnimationOk = New System.Windows.Forms.Button()
        Me.btnPlayAnimationCancel = New System.Windows.Forms.Button()
        Me.lblPlayAnimY = New System.Windows.Forms.Label()
        Me.lblPlayAnimX = New System.Windows.Forms.Label()
        Me.cmbPlayAnimEvent = New System.Windows.Forms.ComboBox()
        Me.DarkLabel62 = New System.Windows.Forms.Label()
        Me.cmbAnimTargetType = New System.Windows.Forms.ComboBox()
        Me.nudPlayAnimTileY = New System.Windows.Forms.NumericUpDown()
        Me.nudPlayAnimTileX = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel61 = New System.Windows.Forms.Label()
        Me.cmbPlayAnim = New System.Windows.Forms.ComboBox()
        Me.fraMoveRouteWait = New System.Windows.Forms.GroupBox()
        Me.btnMoveWaitCancel = New System.Windows.Forms.Button()
        Me.btnMoveWaitOk = New System.Windows.Forms.Button()
        Me.DarkLabel79 = New System.Windows.Forms.Label()
        Me.cmbMoveWait = New System.Windows.Forms.ComboBox()
        Me.fraCustomScript = New System.Windows.Forms.GroupBox()
        Me.nudCustomScript = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel78 = New System.Windows.Forms.Label()
        Me.btnCustomScriptCancel = New System.Windows.Forms.Button()
        Me.btnCustomScriptOk = New System.Windows.Forms.Button()
        Me.fraSetWeather = New System.Windows.Forms.GroupBox()
        Me.btnSetWeatherOk = New System.Windows.Forms.Button()
        Me.btnSetWeatherCancel = New System.Windows.Forms.Button()
        Me.DarkLabel76 = New System.Windows.Forms.Label()
        Me.nudWeatherIntensity = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel75 = New System.Windows.Forms.Label()
        Me.CmbWeather = New System.Windows.Forms.ComboBox()
        Me.fraSpawnNpc = New System.Windows.Forms.GroupBox()
        Me.btnSpawnNpcOk = New System.Windows.Forms.Button()
        Me.btnSpawnNpcCancel = New System.Windows.Forms.Button()
        Me.cmbSpawnNpc = New System.Windows.Forms.ComboBox()
        Me.fraGiveExp = New System.Windows.Forms.GroupBox()
        Me.btnGiveExpOk = New System.Windows.Forms.Button()
        Me.btnGiveExpCancel = New System.Windows.Forms.Button()
        Me.nudGiveExp = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel77 = New System.Windows.Forms.Label()
        Me.fraEndQuest = New System.Windows.Forms.GroupBox()
        Me.btnEndQuestOk = New System.Windows.Forms.Button()
        Me.btnEndQuestCancel = New System.Windows.Forms.Button()
        Me.cmbEndQuest = New System.Windows.Forms.ComboBox()
        Me.fraSetAccess = New System.Windows.Forms.GroupBox()
        Me.btnSetAccessOk = New System.Windows.Forms.Button()
        Me.btnSetAccessCancel = New System.Windows.Forms.Button()
        Me.cmbSetAccess = New System.Windows.Forms.ComboBox()
        Me.fraSetWait = New System.Windows.Forms.GroupBox()
        Me.btnSetWaitOk = New System.Windows.Forms.Button()
        Me.btnSetWaitCancel = New System.Windows.Forms.Button()
        Me.DarkLabel74 = New System.Windows.Forms.Label()
        Me.DarkLabel72 = New System.Windows.Forms.Label()
        Me.DarkLabel73 = New System.Windows.Forms.Label()
        Me.nudWaitAmount = New System.Windows.Forms.NumericUpDown()
        Me.fraShowPic = New System.Windows.Forms.GroupBox()
        Me.btnShowPicOk = New System.Windows.Forms.Button()
        Me.btnShowPicCancel = New System.Windows.Forms.Button()
        Me.DarkLabel71 = New System.Windows.Forms.Label()
        Me.DarkLabel70 = New System.Windows.Forms.Label()
        Me.DarkLabel67 = New System.Windows.Forms.Label()
        Me.DarkLabel68 = New System.Windows.Forms.Label()
        Me.nudPicOffsetY = New System.Windows.Forms.NumericUpDown()
        Me.nudPicOffsetX = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel69 = New System.Windows.Forms.Label()
        Me.cmbPicLoc = New System.Windows.Forms.ComboBox()
        Me.nudShowPicture = New System.Windows.Forms.NumericUpDown()
        Me.picShowPic = New System.Windows.Forms.PictureBox()
        Me.cmbPicIndex = New System.Windows.Forms.ComboBox()
        Me.DarkLabel66 = New System.Windows.Forms.Label()
        Me.fraOpenShop = New System.Windows.Forms.GroupBox()
        Me.btnOpenShopOk = New System.Windows.Forms.Button()
        Me.btnOpenShopCancel = New System.Windows.Forms.Button()
        Me.cmbOpenShop = New System.Windows.Forms.ComboBox()
        Me.fraChangeLevel = New System.Windows.Forms.GroupBox()
        Me.btnChangeLevelOk = New System.Windows.Forms.Button()
        Me.btnChangeLevelCancel = New System.Windows.Forms.Button()
        Me.DarkLabel65 = New System.Windows.Forms.Label()
        Me.nudChangeLevel = New System.Windows.Forms.NumericUpDown()
        Me.fraChangeGender = New System.Windows.Forms.GroupBox()
        Me.btnChangeGenderOk = New System.Windows.Forms.Button()
        Me.btnChangeGenderCancel = New System.Windows.Forms.Button()
        Me.optChangeSexFemale = New System.Windows.Forms.RadioButton()
        Me.optChangeSexMale = New System.Windows.Forms.RadioButton()
        Me.fraGoToLabel = New System.Windows.Forms.GroupBox()
        Me.btnGoToLabelOk = New System.Windows.Forms.Button()
        Me.btnGoToLabelCancel = New System.Windows.Forms.Button()
        Me.txtGotoLabel = New System.Windows.Forms.TextBox()
        Me.DarkLabel60 = New System.Windows.Forms.Label()
        Me.fraHidePic = New System.Windows.Forms.GroupBox()
        Me.btnHidePicOk = New System.Windows.Forms.Button()
        Me.btnHidePicCancel = New System.Windows.Forms.Button()
        Me.nudHidePic = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel59 = New System.Windows.Forms.Label()
        Me.fraBeginQuest = New System.Windows.Forms.GroupBox()
        Me.btnBeginQuestOk = New System.Windows.Forms.Button()
        Me.btnBeginQuestCancel = New System.Windows.Forms.Button()
        Me.cmbBeginQuest = New System.Windows.Forms.ComboBox()
        Me.DarkLabel58 = New System.Windows.Forms.Label()
        Me.fraShowChoices = New System.Windows.Forms.GroupBox()
        Me.txtChoices4 = New System.Windows.Forms.TextBox()
        Me.txtChoices3 = New System.Windows.Forms.TextBox()
        Me.txtChoices2 = New System.Windows.Forms.TextBox()
        Me.txtChoices1 = New System.Windows.Forms.TextBox()
        Me.DarkLabel56 = New System.Windows.Forms.Label()
        Me.DarkLabel57 = New System.Windows.Forms.Label()
        Me.DarkLabel55 = New System.Windows.Forms.Label()
        Me.DarkLabel54 = New System.Windows.Forms.Label()
        Me.DarkLabel52 = New System.Windows.Forms.Label()
        Me.txtChoicePrompt = New System.Windows.Forms.TextBox()
        Me.btnShowChoicesOk = New System.Windows.Forms.Button()
        Me.picShowChoicesFace = New System.Windows.Forms.PictureBox()
        Me.btnShowChoicesCancel = New System.Windows.Forms.Button()
        Me.DarkLabel53 = New System.Windows.Forms.Label()
        Me.nudShowChoicesFace = New System.Windows.Forms.NumericUpDown()
        Me.fraPlayerVariable = New System.Windows.Forms.GroupBox()
        Me.nudVariableData2 = New System.Windows.Forms.NumericUpDown()
        Me.optVariableAction2 = New System.Windows.Forms.RadioButton()
        Me.btnPlayerVarOk = New System.Windows.Forms.Button()
        Me.btnPlayerVarCancel = New System.Windows.Forms.Button()
        Me.DarkLabel51 = New System.Windows.Forms.Label()
        Me.DarkLabel50 = New System.Windows.Forms.Label()
        Me.nudVariableData4 = New System.Windows.Forms.NumericUpDown()
        Me.nudVariableData3 = New System.Windows.Forms.NumericUpDown()
        Me.optVariableAction3 = New System.Windows.Forms.RadioButton()
        Me.optVariableAction1 = New System.Windows.Forms.RadioButton()
        Me.nudVariableData1 = New System.Windows.Forms.NumericUpDown()
        Me.nudVariableData0 = New System.Windows.Forms.NumericUpDown()
        Me.optVariableAction0 = New System.Windows.Forms.RadioButton()
        Me.cmbVariable = New System.Windows.Forms.ComboBox()
        Me.DarkLabel49 = New System.Windows.Forms.Label()
        Me.fraChangeSprite = New System.Windows.Forms.GroupBox()
        Me.btnChangeSpriteOk = New System.Windows.Forms.Button()
        Me.btnChangeSpriteCancel = New System.Windows.Forms.Button()
        Me.DarkLabel48 = New System.Windows.Forms.Label()
        Me.nudChangeSprite = New System.Windows.Forms.NumericUpDown()
        Me.picChangeSprite = New System.Windows.Forms.PictureBox()
        Me.fraSetSelfSwitch = New System.Windows.Forms.GroupBox()
        Me.btnSelfswitchOk = New System.Windows.Forms.Button()
        Me.btnSelfswitchCancel = New System.Windows.Forms.Button()
        Me.DarkLabel47 = New System.Windows.Forms.Label()
        Me.cmbSetSelfSwitchTo = New System.Windows.Forms.ComboBox()
        Me.DarkLabel46 = New System.Windows.Forms.Label()
        Me.cmbSetSelfSwitch = New System.Windows.Forms.ComboBox()
        Me.fraMapTint = New System.Windows.Forms.GroupBox()
        Me.btnMapTintOk = New System.Windows.Forms.Button()
        Me.btnMapTintCancel = New System.Windows.Forms.Button()
        Me.DarkLabel42 = New System.Windows.Forms.Label()
        Me.nudMapTintData3 = New System.Windows.Forms.NumericUpDown()
        Me.nudMapTintData2 = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel43 = New System.Windows.Forms.Label()
        Me.DarkLabel44 = New System.Windows.Forms.Label()
        Me.nudMapTintData1 = New System.Windows.Forms.NumericUpDown()
        Me.nudMapTintData0 = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel45 = New System.Windows.Forms.Label()
        Me.fraShowChatBubble = New System.Windows.Forms.GroupBox()
        Me.btnShowChatBubbleOk = New System.Windows.Forms.Button()
        Me.btnShowChatBubbleCancel = New System.Windows.Forms.Button()
        Me.DarkLabel41 = New System.Windows.Forms.Label()
        Me.cmbChatBubbleTarget = New System.Windows.Forms.ComboBox()
        Me.cmbChatBubbleTargetType = New System.Windows.Forms.ComboBox()
        Me.DarkLabel40 = New System.Windows.Forms.Label()
        Me.txtChatbubbleText = New System.Windows.Forms.TextBox()
        Me.DarkLabel39 = New System.Windows.Forms.Label()
        Me.fraPlaySound = New System.Windows.Forms.GroupBox()
        Me.btnPlaySoundOk = New System.Windows.Forms.Button()
        Me.btnPlaySoundCancel = New System.Windows.Forms.Button()
        Me.cmbPlaySound = New System.Windows.Forms.ComboBox()
        Me.fraChangePK = New System.Windows.Forms.GroupBox()
        Me.btnChangePkOk = New System.Windows.Forms.Button()
        Me.btnChangePkCancel = New System.Windows.Forms.Button()
        Me.cmbSetPK = New System.Windows.Forms.ComboBox()
        Me.fraCreateLabel = New System.Windows.Forms.GroupBox()
        Me.btnCreatelabelOk = New System.Windows.Forms.Button()
        Me.btnCreatelabelCancel = New System.Windows.Forms.Button()
        Me.txtLabelName = New System.Windows.Forms.TextBox()
        Me.lblLabelName = New System.Windows.Forms.Label()
        Me.fraChangeClass = New System.Windows.Forms.GroupBox()
        Me.btnChangeClassOk = New System.Windows.Forms.Button()
        Me.btnChangeClassCancel = New System.Windows.Forms.Button()
        Me.cmbChangeClass = New System.Windows.Forms.ComboBox()
        Me.DarkLabel38 = New System.Windows.Forms.Label()
        Me.fraChangeSkills = New System.Windows.Forms.GroupBox()
        Me.btnChangeSkillsOk = New System.Windows.Forms.Button()
        Me.btnChangeSkillsCancel = New System.Windows.Forms.Button()
        Me.optChangeSkillsRemove = New System.Windows.Forms.RadioButton()
        Me.optChangeSkillsAdd = New System.Windows.Forms.RadioButton()
        Me.cmbChangeSkills = New System.Windows.Forms.ComboBox()
        Me.DarkLabel37 = New System.Windows.Forms.Label()
        Me.fraCompleteTask = New System.Windows.Forms.GroupBox()
        Me.btnCompleteQuestTaskOk = New System.Windows.Forms.Button()
        Me.btnCompleteQuestTaskCancel = New System.Windows.Forms.Button()
        Me.DarkLabel35 = New System.Windows.Forms.Label()
        Me.DarkLabel36 = New System.Windows.Forms.Label()
        Me.nudCompleteQuestTask = New System.Windows.Forms.NumericUpDown()
        Me.cmbCompleteQuest = New System.Windows.Forms.ComboBox()
        Me.fraPlayerWarp = New System.Windows.Forms.GroupBox()
        Me.btnPlayerWarpOk = New System.Windows.Forms.Button()
        Me.btnPlayerWarpCancel = New System.Windows.Forms.Button()
        Me.DarkLabel31 = New System.Windows.Forms.Label()
        Me.cmbWarpPlayerDir = New System.Windows.Forms.ComboBox()
        Me.nudWPY = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel32 = New System.Windows.Forms.Label()
        Me.nudWPX = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel33 = New System.Windows.Forms.Label()
        Me.nudWPMap = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel34 = New System.Windows.Forms.Label()
        Me.fraSetFog = New System.Windows.Forms.GroupBox()
        Me.btnSetFogOk = New System.Windows.Forms.Button()
        Me.btnSetFogCancel = New System.Windows.Forms.Button()
        Me.DarkLabel30 = New System.Windows.Forms.Label()
        Me.DarkLabel29 = New System.Windows.Forms.Label()
        Me.DarkLabel28 = New System.Windows.Forms.Label()
        Me.nudFogData2 = New System.Windows.Forms.NumericUpDown()
        Me.nudFogData1 = New System.Windows.Forms.NumericUpDown()
        Me.nudFogData0 = New System.Windows.Forms.NumericUpDown()
        Me.fraShowText = New System.Windows.Forms.GroupBox()
        Me.DarkLabel27 = New System.Windows.Forms.Label()
        Me.txtShowText = New System.Windows.Forms.TextBox()
        Me.btnShowTextCancel = New System.Windows.Forms.Button()
        Me.btnShowTextOk = New System.Windows.Forms.Button()
        Me.picShowTextFace = New System.Windows.Forms.PictureBox()
        Me.DarkLabel26 = New System.Windows.Forms.Label()
        Me.nudShowTextFace = New System.Windows.Forms.NumericUpDown()
        Me.fraAddText = New System.Windows.Forms.GroupBox()
        Me.btnAddTextOk = New System.Windows.Forms.Button()
        Me.btnAddTextCancel = New System.Windows.Forms.Button()
        Me.optAddText_Global = New System.Windows.Forms.RadioButton()
        Me.optAddText_Map = New System.Windows.Forms.RadioButton()
        Me.optAddText_Player = New System.Windows.Forms.RadioButton()
        Me.DarkLabel25 = New System.Windows.Forms.Label()
        Me.txtAddText_Text = New System.Windows.Forms.TextBox()
        Me.DarkLabel24 = New System.Windows.Forms.Label()
        Me.fraPlayerSwitch = New System.Windows.Forms.GroupBox()
        Me.btnSetPlayerSwitchOk = New System.Windows.Forms.Button()
        Me.btnSetPlayerswitchCancel = New System.Windows.Forms.Button()
        Me.cmbPlayerSwitchSet = New System.Windows.Forms.ComboBox()
        Me.DarkLabel23 = New System.Windows.Forms.Label()
        Me.cmbSwitch = New System.Windows.Forms.ComboBox()
        Me.DarkLabel22 = New System.Windows.Forms.Label()
        Me.fraChangeItems = New System.Windows.Forms.GroupBox()
        Me.btnChangeItemsOk = New System.Windows.Forms.Button()
        Me.btnChangeItemsCancel = New System.Windows.Forms.Button()
        Me.nudChangeItemsAmount = New System.Windows.Forms.NumericUpDown()
        Me.optChangeItemRemove = New System.Windows.Forms.RadioButton()
        Me.optChangeItemAdd = New System.Windows.Forms.RadioButton()
        Me.optChangeItemSet = New System.Windows.Forms.RadioButton()
        Me.cmbChangeItemIndex = New System.Windows.Forms.ComboBox()
        Me.DarkLabel21 = New System.Windows.Forms.Label()
        Me.fraConditionalBranch = New System.Windows.Forms.GroupBox()
        Me.btnConditionalBranchOk = New System.Windows.Forms.Button()
        Me.btnConditionalBranchCancel = New System.Windows.Forms.Button()
        Me.cmbCondition_Gender = New System.Windows.Forms.ComboBox()
        Me.optCondition8 = New System.Windows.Forms.RadioButton()
        Me.fraConditions_Quest = New System.Windows.Forms.GroupBox()
        Me.DarkLabel20 = New System.Windows.Forms.Label()
        Me.nudCondition_QuestTask = New System.Windows.Forms.NumericUpDown()
        Me.cmbCondition_General = New System.Windows.Forms.ComboBox()
        Me.DarkLabel19 = New System.Windows.Forms.Label()
        Me.optCondition_Quest1 = New System.Windows.Forms.RadioButton()
        Me.optCondition_Quest0 = New System.Windows.Forms.RadioButton()
        Me.nudCondition_Quest = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel18 = New System.Windows.Forms.Label()
        Me.optCondition7 = New System.Windows.Forms.RadioButton()
        Me.cmbCondition_SelfSwitchCondition = New System.Windows.Forms.ComboBox()
        Me.DarkLabel17 = New System.Windows.Forms.Label()
        Me.cmbCondition_SelfSwitch = New System.Windows.Forms.ComboBox()
        Me.optCondition6 = New System.Windows.Forms.RadioButton()
        Me.nudCondition_LevelAmount = New System.Windows.Forms.NumericUpDown()
        Me.optCondition5 = New System.Windows.Forms.RadioButton()
        Me.cmbCondition_LevelCompare = New System.Windows.Forms.ComboBox()
        Me.cmbCondition_LearntSkill = New System.Windows.Forms.ComboBox()
        Me.optCondition4 = New System.Windows.Forms.RadioButton()
        Me.cmbCondition_ClassIs = New System.Windows.Forms.ComboBox()
        Me.optCondition3 = New System.Windows.Forms.RadioButton()
        Me.nudCondition_HasItem = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel16 = New System.Windows.Forms.Label()
        Me.cmbCondition_HasItem = New System.Windows.Forms.ComboBox()
        Me.optCondition2 = New System.Windows.Forms.RadioButton()
        Me.optCondition1 = New System.Windows.Forms.RadioButton()
        Me.DarkLabel15 = New System.Windows.Forms.Label()
        Me.cmbCondtion_PlayerSwitchCondition = New System.Windows.Forms.ComboBox()
        Me.cmbCondition_PlayerSwitch = New System.Windows.Forms.ComboBox()
        Me.nudCondition_PlayerVarCondition = New System.Windows.Forms.NumericUpDown()
        Me.cmbCondition_PlayerVarCompare = New System.Windows.Forms.ComboBox()
        Me.DarkLabel14 = New System.Windows.Forms.Label()
        Me.cmbCondition_PlayerVarIndex = New System.Windows.Forms.ComboBox()
        Me.optCondition0 = New System.Windows.Forms.RadioButton()
        Me.fraPlayBGM = New System.Windows.Forms.GroupBox()
        Me.btnPlayBgmOk = New System.Windows.Forms.Button()
        Me.btnPlayBgmCancel = New System.Windows.Forms.Button()
        Me.cmbPlayBGM = New System.Windows.Forms.ComboBox()
        Me.pnlVariableSwitches = New System.Windows.Forms.Panel()
        Me.FraRenaming = New System.Windows.Forms.GroupBox()
        Me.btnRename_Cancel = New System.Windows.Forms.Button()
        Me.btnRename_Ok = New System.Windows.Forms.Button()
        Me.fraRandom10 = New System.Windows.Forms.GroupBox()
        Me.txtRename = New System.Windows.Forms.TextBox()
        Me.lblEditing = New System.Windows.Forms.Label()
        Me.fraLabeling = New System.Windows.Forms.GroupBox()
        Me.lstSwitches = New System.Windows.Forms.ListBox()
        Me.lstVariables = New System.Windows.Forms.ListBox()
        Me.btnLabel_Cancel = New System.Windows.Forms.Button()
        Me.lblRandomLabel36 = New System.Windows.Forms.Label()
        Me.btnRenameVariable = New System.Windows.Forms.Button()
        Me.lblRandomLabel25 = New System.Windows.Forms.Label()
        Me.btnRenameSwitch = New System.Windows.Forms.Button()
        Me.btnLabel_Ok = New System.Windows.Forms.Button()
        Me.optCondition9 = New System.Windows.Forms.RadioButton()
        Me.cmbCondition_Time = New System.Windows.Forms.ComboBox()
        Me.fraPageSetUp.SuspendLayout()
        Me.tabPages.SuspendLayout()
        Me.pnlTabPage.SuspendLayout()
        Me.fraCommands.SuspendLayout()
        Me.DarkGroupBox8.SuspendLayout()
        Me.DarkGroupBox7.SuspendLayout()
        Me.DarkGroupBox5.SuspendLayout()
        Me.DarkGroupBox4.SuspendLayout()
        Me.DarkGroupBox3.SuspendLayout()
        Me.DarkGroupBox2.SuspendLayout()
        CType(Me.picGraphic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DarkGroupBox1.SuspendLayout()
        CType(Me.nudPlayerVariable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DarkGroupBox6.SuspendLayout()
        Me.fraMoveRoute.SuspendLayout()
        Me.DarkGroupBox10.SuspendLayout()
        Me.fraGraphic.SuspendLayout()
        Me.pnlGraphicSel.SuspendLayout()
        CType(Me.picGraphicSel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudGraphic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraDialogue.SuspendLayout()
        Me.fraPlayAnimation.SuspendLayout()
        CType(Me.nudPlayAnimTileY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudPlayAnimTileX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraMoveRouteWait.SuspendLayout()
        Me.fraCustomScript.SuspendLayout()
        CType(Me.nudCustomScript, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSetWeather.SuspendLayout()
        CType(Me.nudWeatherIntensity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSpawnNpc.SuspendLayout()
        Me.fraGiveExp.SuspendLayout()
        CType(Me.nudGiveExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraEndQuest.SuspendLayout()
        Me.fraSetAccess.SuspendLayout()
        Me.fraSetWait.SuspendLayout()
        CType(Me.nudWaitAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraShowPic.SuspendLayout()
        CType(Me.nudPicOffsetY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudPicOffsetX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudShowPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picShowPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraOpenShop.SuspendLayout()
        Me.fraChangeLevel.SuspendLayout()
        CType(Me.nudChangeLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraChangeGender.SuspendLayout()
        Me.fraGoToLabel.SuspendLayout()
        Me.fraHidePic.SuspendLayout()
        CType(Me.nudHidePic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraBeginQuest.SuspendLayout()
        Me.fraShowChoices.SuspendLayout()
        CType(Me.picShowChoicesFace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudShowChoicesFace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraPlayerVariable.SuspendLayout()
        CType(Me.nudVariableData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudVariableData4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudVariableData3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudVariableData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudVariableData0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraChangeSprite.SuspendLayout()
        CType(Me.nudChangeSprite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picChangeSprite, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSetSelfSwitch.SuspendLayout()
        Me.fraMapTint.SuspendLayout()
        CType(Me.nudMapTintData3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMapTintData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMapTintData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMapTintData0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraShowChatBubble.SuspendLayout()
        Me.fraPlaySound.SuspendLayout()
        Me.fraChangePK.SuspendLayout()
        Me.fraCreateLabel.SuspendLayout()
        Me.fraChangeClass.SuspendLayout()
        Me.fraChangeSkills.SuspendLayout()
        Me.fraCompleteTask.SuspendLayout()
        CType(Me.nudCompleteQuestTask, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraPlayerWarp.SuspendLayout()
        CType(Me.nudWPY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWPX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWPMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSetFog.SuspendLayout()
        CType(Me.nudFogData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudFogData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudFogData0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraShowText.SuspendLayout()
        CType(Me.picShowTextFace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudShowTextFace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAddText.SuspendLayout()
        Me.fraPlayerSwitch.SuspendLayout()
        Me.fraChangeItems.SuspendLayout()
        CType(Me.nudChangeItemsAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraConditionalBranch.SuspendLayout()
        Me.fraConditions_Quest.SuspendLayout()
        CType(Me.nudCondition_QuestTask, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCondition_Quest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCondition_LevelAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCondition_HasItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCondition_PlayerVarCondition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraPlayBGM.SuspendLayout()
        Me.pnlVariableSwitches.SuspendLayout()
        Me.FraRenaming.SuspendLayout()
        Me.fraRandom10.SuspendLayout()
        Me.fraLabeling.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvCommands
        '
        Me.tvCommands.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.tvCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tvCommands.ForeColor = System.Drawing.Color.Gainsboro
        Me.tvCommands.Location = New System.Drawing.Point(6, 3)
        Me.tvCommands.Name = "tvCommands"
        TreeNode59.Name = "Node1"
        TreeNode59.Text = "Show Text"
        TreeNode60.Name = "Node2"
        TreeNode60.Text = "Show Choices"
        TreeNode61.Name = "Node3"
        TreeNode61.Text = "Add Chatbox Text"
        TreeNode62.Name = "Node5"
        TreeNode62.Text = "Show ChatBubble"
        TreeNode63.Name = "NodeMessages"
        TreeNode63.Text = "Messages"
        TreeNode64.Name = "Node1"
        TreeNode64.Text = "Set Player Variable"
        TreeNode65.Name = "Node2"
        TreeNode65.Text = "Set Player Switch"
        TreeNode66.Name = "Node3"
        TreeNode66.Text = "Set Self Switch"
        TreeNode67.Name = "NodeProcessing"
        TreeNode67.Text = "Event Processing"
        TreeNode68.Name = "Node1"
        TreeNode68.Text = "Conditional Branch"
        TreeNode69.Name = "Node2"
        TreeNode69.Text = "Stop Event Processing"
        TreeNode70.Name = "Node3"
        TreeNode70.Text = "Label"
        TreeNode71.Name = "Node4"
        TreeNode71.Text = "GoTo Label"
        TreeNode72.Name = "NodeFlowControl"
        TreeNode72.Text = "Flow Control"
        TreeNode73.Name = "Node1"
        TreeNode73.Text = "Change Items"
        TreeNode74.Name = "Node2"
        TreeNode74.Text = "Restore HP"
        TreeNode75.Name = "Node3"
        TreeNode75.Text = "Restore MP"
        TreeNode76.Name = "Node4"
        TreeNode76.Text = "Level Up"
        TreeNode77.Name = "Node5"
        TreeNode77.Text = "Change Level"
        TreeNode78.Name = "Node6"
        TreeNode78.Text = "Change Skills"
        TreeNode79.Name = "Node7"
        TreeNode79.Text = "Change Class"
        TreeNode80.Name = "Node8"
        TreeNode80.Text = "Change Sprite"
        TreeNode81.Name = "Node9"
        TreeNode81.Text = "Change Gender"
        TreeNode82.Name = "Node10"
        TreeNode82.Text = "Change PK"
        TreeNode83.Name = "Node11"
        TreeNode83.Text = "Give Experience"
        TreeNode84.Name = "NodePlayerOptions"
        TreeNode84.Text = "Player Options"
        TreeNode85.Name = "Node1"
        TreeNode85.Text = "Warp Player"
        TreeNode86.Name = "Node2"
        TreeNode86.Text = "Set Move Route"
        TreeNode87.Name = "Node3"
        TreeNode87.Text = "Wait for Route Completion"
        TreeNode88.Name = "Node4"
        TreeNode88.Text = "Force Spawn Npc"
        TreeNode89.Name = "Node5"
        TreeNode89.Text = "Hold Player"
        TreeNode90.Name = "Node6"
        TreeNode90.Text = "Release Player"
        TreeNode91.Name = "NodeMovement"
        TreeNode91.Text = "Movement"
        TreeNode92.Name = "Node1"
        TreeNode92.Text = "Animation"
        TreeNode93.Name = "NodeAnimation"
        TreeNode93.Text = "Animation"
        TreeNode94.Name = "Node1"
        TreeNode94.Text = "Begin Quest"
        TreeNode95.Name = "Node2"
        TreeNode95.Text = "Complete Task"
        TreeNode96.Name = "Node3"
        TreeNode96.Text = "End Quest"
        TreeNode97.Name = "NodeQuesting"
        TreeNode97.Text = "Questing"
        TreeNode98.Name = "Node1"
        TreeNode98.Text = "Set Fog"
        TreeNode99.Name = "Node2"
        TreeNode99.Text = "Set Weather"
        TreeNode100.Name = "Node3"
        TreeNode100.Text = "Set Map Tinting"
        TreeNode101.Name = "NodeMapFunctions"
        TreeNode101.Text = "Map Functions"
        TreeNode102.Name = "Node1"
        TreeNode102.Text = "Play BGM"
        TreeNode103.Name = "Node2"
        TreeNode103.Text = "Stop BGM"
        TreeNode104.Name = "Node3"
        TreeNode104.Text = "Play Sound"
        TreeNode105.Name = "Node4"
        TreeNode105.Text = "Stop Sounds"
        TreeNode106.Name = "NodeSound"
        TreeNode106.Text = "Music and Sound"
        TreeNode107.Name = "Node1"
        TreeNode107.Text = "Wait..."
        TreeNode108.Name = "Node2"
        TreeNode108.Text = "Set Access"
        TreeNode109.Name = "Node3"
        TreeNode109.Text = "Custom Script"
        TreeNode110.Name = "NodeEtc"
        TreeNode110.Text = "Etc..."
        TreeNode111.Name = "Node1"
        TreeNode111.Text = "Open Bank"
        TreeNode112.Name = "Node2"
        TreeNode112.Text = "Open Shop"
        TreeNode113.Name = "NodeShopBank"
        TreeNode113.Text = "Shop and Bank"
        TreeNode114.Name = "Node1"
        TreeNode114.Text = "Fade In"
        TreeNode115.Name = "Node2"
        TreeNode115.Text = "Fade Out"
        TreeNode116.Name = "Node0"
        TreeNode116.Text = "CutScene Options"
        Me.tvCommands.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode63, TreeNode67, TreeNode72, TreeNode84, TreeNode91, TreeNode93, TreeNode97, TreeNode101, TreeNode106, TreeNode110, TreeNode113, TreeNode116})
        Me.tvCommands.Size = New System.Drawing.Size(381, 443)
        Me.tvCommands.TabIndex = 1
        '
        'fraPageSetUp
        '
        Me.fraPageSetUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPageSetUp.Controls.Add(Me.chkGlobal)
        Me.fraPageSetUp.Controls.Add(Me.btnClearPage)
        Me.fraPageSetUp.Controls.Add(Me.btnDeletePage)
        Me.fraPageSetUp.Controls.Add(Me.btnPastePage)
        Me.fraPageSetUp.Controls.Add(Me.btnCopyPage)
        Me.fraPageSetUp.Controls.Add(Me.btnNewPage)
        Me.fraPageSetUp.Controls.Add(Me.txtName)
        Me.fraPageSetUp.Controls.Add(Me.DarkLabel1)
        Me.fraPageSetUp.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPageSetUp.Location = New System.Drawing.Point(3, 3)
        Me.fraPageSetUp.Name = "fraPageSetUp"
        Me.fraPageSetUp.Size = New System.Drawing.Size(791, 50)
        Me.fraPageSetUp.TabIndex = 2
        Me.fraPageSetUp.TabStop = False
        Me.fraPageSetUp.Text = "General"
        '
        'chkGlobal
        '
        Me.chkGlobal.AutoSize = True
        Me.chkGlobal.Location = New System.Drawing.Point(280, 20)
        Me.chkGlobal.Name = "chkGlobal"
        Me.chkGlobal.Size = New System.Drawing.Size(87, 17)
        Me.chkGlobal.TabIndex = 7
        Me.chkGlobal.Text = "Global Event"
        '
        'btnClearPage
        '
        Me.btnClearPage.Location = New System.Drawing.Point(707, 16)
        Me.btnClearPage.Name = "btnClearPage"
        Me.btnClearPage.Padding = New System.Windows.Forms.Padding(5)
        Me.btnClearPage.Size = New System.Drawing.Size(75, 23)
        Me.btnClearPage.TabIndex = 6
        Me.btnClearPage.Text = "Clear Page"
        '
        'btnDeletePage
        '
        Me.btnDeletePage.Location = New System.Drawing.Point(622, 16)
        Me.btnDeletePage.Name = "btnDeletePage"
        Me.btnDeletePage.Padding = New System.Windows.Forms.Padding(5)
        Me.btnDeletePage.Size = New System.Drawing.Size(79, 23)
        Me.btnDeletePage.TabIndex = 5
        Me.btnDeletePage.Text = "Delete Page"
        '
        'btnPastePage
        '
        Me.btnPastePage.Location = New System.Drawing.Point(541, 16)
        Me.btnPastePage.Name = "btnPastePage"
        Me.btnPastePage.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPastePage.Size = New System.Drawing.Size(75, 23)
        Me.btnPastePage.TabIndex = 4
        Me.btnPastePage.Text = "Paste Page"
        '
        'btnCopyPage
        '
        Me.btnCopyPage.Location = New System.Drawing.Point(460, 16)
        Me.btnCopyPage.Name = "btnCopyPage"
        Me.btnCopyPage.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCopyPage.Size = New System.Drawing.Size(75, 23)
        Me.btnCopyPage.TabIndex = 3
        Me.btnCopyPage.Text = "Copy Page"
        '
        'btnNewPage
        '
        Me.btnNewPage.Location = New System.Drawing.Point(379, 16)
        Me.btnNewPage.Name = "btnNewPage"
        Me.btnNewPage.Padding = New System.Windows.Forms.Padding(5)
        Me.btnNewPage.Size = New System.Drawing.Size(75, 23)
        Me.btnNewPage.TabIndex = 2
        Me.btnNewPage.Text = "New Page"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(84, 19)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(190, 20)
        Me.txtName.TabIndex = 1
        '
        'DarkLabel1
        '
        Me.DarkLabel1.AutoSize = True
        Me.DarkLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel1.Location = New System.Drawing.Point(9, 21)
        Me.DarkLabel1.Name = "DarkLabel1"
        Me.DarkLabel1.Size = New System.Drawing.Size(69, 13)
        Me.DarkLabel1.TabIndex = 0
        Me.DarkLabel1.Text = "Event Name:"
        '
        'tabPages
        '
        Me.tabPages.Controls.Add(Me.TabPage1)
        Me.tabPages.Location = New System.Drawing.Point(12, 59)
        Me.tabPages.Name = "tabPages"
        Me.tabPages.SelectedIndex = 0
        Me.tabPages.Size = New System.Drawing.Size(709, 19)
        Me.tabPages.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DimGray
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(701, 0)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'pnlTabPage
        '
        Me.pnlTabPage.Controls.Add(Me.fraCommands)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox8)
        Me.pnlTabPage.Controls.Add(Me.lstCommands)
        Me.pnlTabPage.Controls.Add(Me.DarkLabel10)
        Me.pnlTabPage.Controls.Add(Me.DarkLabel9)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox7)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox5)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox4)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox3)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox2)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox1)
        Me.pnlTabPage.Location = New System.Drawing.Point(3, 81)
        Me.pnlTabPage.Name = "pnlTabPage"
        Me.pnlTabPage.Size = New System.Drawing.Size(791, 497)
        Me.pnlTabPage.TabIndex = 4
        '
        'fraCommands
        '
        Me.fraCommands.Controls.Add(Me.btnCancelCommand)
        Me.fraCommands.Controls.Add(Me.tvCommands)
        Me.fraCommands.Location = New System.Drawing.Point(389, 6)
        Me.fraCommands.Name = "fraCommands"
        Me.fraCommands.Size = New System.Drawing.Size(393, 482)
        Me.fraCommands.TabIndex = 6
        Me.fraCommands.Visible = False
        '
        'btnCancelCommand
        '
        Me.btnCancelCommand.Location = New System.Drawing.Point(312, 452)
        Me.btnCancelCommand.Name = "btnCancelCommand"
        Me.btnCancelCommand.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCancelCommand.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelCommand.TabIndex = 2
        Me.btnCancelCommand.Text = "Cancel"
        '
        'DarkGroupBox8
        '
        Me.DarkGroupBox8.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox8.Controls.Add(Me.btnClearCommand)
        Me.DarkGroupBox8.Controls.Add(Me.btnDeleteCommand)
        Me.DarkGroupBox8.Controls.Add(Me.btnEditCommand)
        Me.DarkGroupBox8.Controls.Add(Me.btnAddCommand)
        Me.DarkGroupBox8.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox8.Location = New System.Drawing.Point(389, 439)
        Me.DarkGroupBox8.Name = "DarkGroupBox8"
        Me.DarkGroupBox8.Size = New System.Drawing.Size(393, 49)
        Me.DarkGroupBox8.TabIndex = 9
        Me.DarkGroupBox8.TabStop = False
        Me.DarkGroupBox8.Text = "Commands"
        '
        'btnClearCommand
        '
        Me.btnClearCommand.Location = New System.Drawing.Point(312, 19)
        Me.btnClearCommand.Name = "btnClearCommand"
        Me.btnClearCommand.Padding = New System.Windows.Forms.Padding(5)
        Me.btnClearCommand.Size = New System.Drawing.Size(75, 23)
        Me.btnClearCommand.TabIndex = 3
        Me.btnClearCommand.Text = "Clear"
        '
        'btnDeleteCommand
        '
        Me.btnDeleteCommand.Location = New System.Drawing.Point(212, 19)
        Me.btnDeleteCommand.Name = "btnDeleteCommand"
        Me.btnDeleteCommand.Padding = New System.Windows.Forms.Padding(5)
        Me.btnDeleteCommand.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteCommand.TabIndex = 2
        Me.btnDeleteCommand.Text = "Delete"
        '
        'btnEditCommand
        '
        Me.btnEditCommand.Location = New System.Drawing.Point(108, 19)
        Me.btnEditCommand.Name = "btnEditCommand"
        Me.btnEditCommand.Padding = New System.Windows.Forms.Padding(5)
        Me.btnEditCommand.Size = New System.Drawing.Size(75, 23)
        Me.btnEditCommand.TabIndex = 1
        Me.btnEditCommand.Text = "Edit"
        '
        'btnAddCommand
        '
        Me.btnAddCommand.Location = New System.Drawing.Point(6, 19)
        Me.btnAddCommand.Name = "btnAddCommand"
        Me.btnAddCommand.Padding = New System.Windows.Forms.Padding(5)
        Me.btnAddCommand.Size = New System.Drawing.Size(75, 23)
        Me.btnAddCommand.TabIndex = 0
        Me.btnAddCommand.Text = "Add"
        '
        'lstCommands
        '
        Me.lstCommands.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstCommands.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstCommands.FormattingEnabled = True
        Me.lstCommands.Location = New System.Drawing.Point(389, 6)
        Me.lstCommands.Name = "lstCommands"
        Me.lstCommands.Size = New System.Drawing.Size(393, 431)
        Me.lstCommands.TabIndex = 8
        '
        'DarkLabel10
        '
        Me.DarkLabel10.ForeColor = System.Drawing.Color.Red
        Me.DarkLabel10.Location = New System.Drawing.Point(182, 458)
        Me.DarkLabel10.Name = "DarkLabel10"
        Me.DarkLabel10.Size = New System.Drawing.Size(200, 30)
        Me.DarkLabel10.TabIndex = 7
        Me.DarkLabel10.Text = "** If global, only the first page will be processed. For shop keepers etc."
        '
        'DarkLabel9
        '
        Me.DarkLabel9.ForeColor = System.Drawing.Color.Red
        Me.DarkLabel9.Location = New System.Drawing.Point(182, 424)
        Me.DarkLabel9.Name = "DarkLabel9"
        Me.DarkLabel9.Size = New System.Drawing.Size(201, 34)
        Me.DarkLabel9.TabIndex = 6
        Me.DarkLabel9.Text = "* Self Switches are always global and will reset on server restart."
        '
        'DarkGroupBox7
        '
        Me.DarkGroupBox7.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox7.Controls.Add(Me.cmbEventQuest)
        Me.DarkGroupBox7.Controls.Add(Me.DarkLabel8)
        Me.DarkGroupBox7.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox7.Location = New System.Drawing.Point(182, 376)
        Me.DarkGroupBox7.Name = "DarkGroupBox7"
        Me.DarkGroupBox7.Size = New System.Drawing.Size(200, 45)
        Me.DarkGroupBox7.TabIndex = 5
        Me.DarkGroupBox7.TabStop = False
        Me.DarkGroupBox7.Text = "Quest Icon"
        '
        'cmbEventQuest
        '
        Me.cmbEventQuest.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbEventQuest.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbEventQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEventQuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbEventQuest.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbEventQuest.FormattingEnabled = True
        Me.cmbEventQuest.Location = New System.Drawing.Point(51, 17)
        Me.cmbEventQuest.Name = "cmbEventQuest"
        Me.cmbEventQuest.Size = New System.Drawing.Size(143, 21)
        Me.cmbEventQuest.TabIndex = 1
        Me.cmbEventQuest.Text = Nothing
        '
        'DarkLabel8
        '
        Me.DarkLabel8.AutoSize = True
        Me.DarkLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel8.Location = New System.Drawing.Point(7, 20)
        Me.DarkLabel8.Name = "DarkLabel8"
        Me.DarkLabel8.Size = New System.Drawing.Size(38, 13)
        Me.DarkLabel8.TabIndex = 0
        Me.DarkLabel8.Text = "Quest:"
        '
        'DarkGroupBox5
        '
        Me.DarkGroupBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox5.Controls.Add(Me.cmbTrigger)
        Me.DarkGroupBox5.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox5.Location = New System.Drawing.Point(182, 321)
        Me.DarkGroupBox5.Name = "DarkGroupBox5"
        Me.DarkGroupBox5.Size = New System.Drawing.Size(200, 49)
        Me.DarkGroupBox5.TabIndex = 4
        Me.DarkGroupBox5.TabStop = False
        Me.DarkGroupBox5.Text = "Trigger"
        '
        'cmbTrigger
        '
        Me.cmbTrigger.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbTrigger.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbTrigger.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbTrigger.FormattingEnabled = True
        Me.cmbTrigger.Items.AddRange(New Object() {"Action Button", "Player Touch", "Parallel Process"})
        Me.cmbTrigger.Location = New System.Drawing.Point(6, 19)
        Me.cmbTrigger.Name = "cmbTrigger"
        Me.cmbTrigger.Size = New System.Drawing.Size(189, 21)
        Me.cmbTrigger.TabIndex = 0
        Me.cmbTrigger.Text = Nothing
        '
        'DarkGroupBox4
        '
        Me.DarkGroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox4.Controls.Add(Me.cmbPositioning)
        Me.DarkGroupBox4.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox4.Location = New System.Drawing.Point(182, 267)
        Me.DarkGroupBox4.Name = "DarkGroupBox4"
        Me.DarkGroupBox4.Size = New System.Drawing.Size(200, 48)
        Me.DarkGroupBox4.TabIndex = 3
        Me.DarkGroupBox4.TabStop = False
        Me.DarkGroupBox4.Text = "Positioning"
        '
        'cmbPositioning
        '
        Me.cmbPositioning.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPositioning.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPositioning.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPositioning.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPositioning.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPositioning.FormattingEnabled = True
        Me.cmbPositioning.Items.AddRange(New Object() {"Below Characters", "Same as Characters", "Above Characters"})
        Me.cmbPositioning.Location = New System.Drawing.Point(6, 19)
        Me.cmbPositioning.Name = "cmbPositioning"
        Me.cmbPositioning.Size = New System.Drawing.Size(189, 21)
        Me.cmbPositioning.TabIndex = 0
        Me.cmbPositioning.Text = Nothing
        '
        'DarkGroupBox3
        '
        Me.DarkGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel7)
        Me.DarkGroupBox3.Controls.Add(Me.cmbMoveFreq)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel6)
        Me.DarkGroupBox3.Controls.Add(Me.cmbMoveSpeed)
        Me.DarkGroupBox3.Controls.Add(Me.btnMoveRoute)
        Me.DarkGroupBox3.Controls.Add(Me.cmbMoveType)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel5)
        Me.DarkGroupBox3.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox3.Location = New System.Drawing.Point(183, 138)
        Me.DarkGroupBox3.Name = "DarkGroupBox3"
        Me.DarkGroupBox3.Size = New System.Drawing.Size(200, 123)
        Me.DarkGroupBox3.TabIndex = 2
        Me.DarkGroupBox3.TabStop = False
        Me.DarkGroupBox3.Text = "Movement"
        '
        'DarkLabel7
        '
        Me.DarkLabel7.AutoSize = True
        Me.DarkLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel7.Location = New System.Drawing.Point(6, 100)
        Me.DarkLabel7.Name = "DarkLabel7"
        Me.DarkLabel7.Size = New System.Drawing.Size(57, 13)
        Me.DarkLabel7.TabIndex = 6
        Me.DarkLabel7.Text = "Frequency"
        '
        'cmbMoveFreq
        '
        Me.cmbMoveFreq.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbMoveFreq.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoveFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMoveFreq.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbMoveFreq.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbMoveFreq.FormattingEnabled = True
        Me.cmbMoveFreq.Items.AddRange(New Object() {"Lowest", "Lower", "Normal", "Higher", "Highest"})
        Me.cmbMoveFreq.Location = New System.Drawing.Point(69, 97)
        Me.cmbMoveFreq.Name = "cmbMoveFreq"
        Me.cmbMoveFreq.Size = New System.Drawing.Size(125, 21)
        Me.cmbMoveFreq.TabIndex = 5
        Me.cmbMoveFreq.Text = Nothing
        '
        'DarkLabel6
        '
        Me.DarkLabel6.AutoSize = True
        Me.DarkLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel6.Location = New System.Drawing.Point(6, 73)
        Me.DarkLabel6.Name = "DarkLabel6"
        Me.DarkLabel6.Size = New System.Drawing.Size(41, 13)
        Me.DarkLabel6.TabIndex = 4
        Me.DarkLabel6.Text = "Speed:"
        '
        'cmbMoveSpeed
        '
        Me.cmbMoveSpeed.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbMoveSpeed.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoveSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMoveSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbMoveSpeed.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbMoveSpeed.FormattingEnabled = True
        Me.cmbMoveSpeed.Items.AddRange(New Object() {"8x Slower", "4x Slower", "2x Slower", "Normal", "2x Faster", "4x Faster"})
        Me.cmbMoveSpeed.Location = New System.Drawing.Point(69, 70)
        Me.cmbMoveSpeed.Name = "cmbMoveSpeed"
        Me.cmbMoveSpeed.Size = New System.Drawing.Size(125, 21)
        Me.cmbMoveSpeed.TabIndex = 3
        Me.cmbMoveSpeed.Text = Nothing
        '
        'btnMoveRoute
        '
        Me.btnMoveRoute.Location = New System.Drawing.Point(119, 41)
        Me.btnMoveRoute.Name = "btnMoveRoute"
        Me.btnMoveRoute.Padding = New System.Windows.Forms.Padding(5)
        Me.btnMoveRoute.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveRoute.TabIndex = 2
        Me.btnMoveRoute.Text = "Move Route"
        '
        'cmbMoveType
        '
        Me.cmbMoveType.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbMoveType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMoveType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbMoveType.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbMoveType.FormattingEnabled = True
        Me.cmbMoveType.Items.AddRange(New Object() {"Fixed Position", "Random", "Move Route"})
        Me.cmbMoveType.Location = New System.Drawing.Point(69, 14)
        Me.cmbMoveType.Name = "cmbMoveType"
        Me.cmbMoveType.Size = New System.Drawing.Size(125, 21)
        Me.cmbMoveType.TabIndex = 1
        Me.cmbMoveType.Text = Nothing
        '
        'DarkLabel5
        '
        Me.DarkLabel5.AutoSize = True
        Me.DarkLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel5.Location = New System.Drawing.Point(6, 17)
        Me.DarkLabel5.Name = "DarkLabel5"
        Me.DarkLabel5.Size = New System.Drawing.Size(34, 13)
        Me.DarkLabel5.TabIndex = 0
        Me.DarkLabel5.Text = "Type:"
        '
        'DarkGroupBox2
        '
        Me.DarkGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox2.Controls.Add(Me.picGraphic)
        Me.DarkGroupBox2.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox2.Location = New System.Drawing.Point(3, 138)
        Me.DarkGroupBox2.Name = "DarkGroupBox2"
        Me.DarkGroupBox2.Size = New System.Drawing.Size(173, 232)
        Me.DarkGroupBox2.TabIndex = 1
        Me.DarkGroupBox2.TabStop = False
        Me.DarkGroupBox2.Text = "Graphic"
        '
        'picGraphic
        '
        Me.picGraphic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picGraphic.Location = New System.Drawing.Point(6, 19)
        Me.picGraphic.Name = "picGraphic"
        Me.picGraphic.Size = New System.Drawing.Size(161, 207)
        Me.picGraphic.TabIndex = 1
        Me.picGraphic.TabStop = False
        '
        'DarkGroupBox1
        '
        Me.DarkGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox1.Controls.Add(Me.cmbSelfSwitchCompare)
        Me.DarkGroupBox1.Controls.Add(Me.DarkLabel4)
        Me.DarkGroupBox1.Controls.Add(Me.cmbSelfSwitch)
        Me.DarkGroupBox1.Controls.Add(Me.chkSelfSwitch)
        Me.DarkGroupBox1.Controls.Add(Me.cmbHasItem)
        Me.DarkGroupBox1.Controls.Add(Me.chkHasItem)
        Me.DarkGroupBox1.Controls.Add(Me.cmbPlayerSwitchCompare)
        Me.DarkGroupBox1.Controls.Add(Me.DarkLabel3)
        Me.DarkGroupBox1.Controls.Add(Me.cmbPlayerSwitch)
        Me.DarkGroupBox1.Controls.Add(Me.chkPlayerSwitch)
        Me.DarkGroupBox1.Controls.Add(Me.nudPlayerVariable)
        Me.DarkGroupBox1.Controls.Add(Me.cmbPlayervarCompare)
        Me.DarkGroupBox1.Controls.Add(Me.DarkLabel2)
        Me.DarkGroupBox1.Controls.Add(Me.cmbPlayerVar)
        Me.DarkGroupBox1.Controls.Add(Me.chkPlayerVar)
        Me.DarkGroupBox1.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox1.Location = New System.Drawing.Point(3, 6)
        Me.DarkGroupBox1.Name = "DarkGroupBox1"
        Me.DarkGroupBox1.Size = New System.Drawing.Size(380, 126)
        Me.DarkGroupBox1.TabIndex = 0
        Me.DarkGroupBox1.TabStop = False
        Me.DarkGroupBox1.Text = "Conditions"
        '
        'cmbSelfSwitchCompare
        '
        Me.cmbSelfSwitchCompare.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbSelfSwitchCompare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSelfSwitchCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSelfSwitchCompare.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSelfSwitchCompare.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbSelfSwitchCompare.FormattingEnabled = True
        Me.cmbSelfSwitchCompare.Items.AddRange(New Object() {"False = 0", "True = 1"})
        Me.cmbSelfSwitchCompare.Location = New System.Drawing.Point(223, 98)
        Me.cmbSelfSwitchCompare.Name = "cmbSelfSwitchCompare"
        Me.cmbSelfSwitchCompare.Size = New System.Drawing.Size(89, 21)
        Me.cmbSelfSwitchCompare.TabIndex = 14
        Me.cmbSelfSwitchCompare.Text = Nothing
        '
        'DarkLabel4
        '
        Me.DarkLabel4.AutoSize = True
        Me.DarkLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel4.Location = New System.Drawing.Point(203, 101)
        Me.DarkLabel4.Name = "DarkLabel4"
        Me.DarkLabel4.Size = New System.Drawing.Size(14, 13)
        Me.DarkLabel4.TabIndex = 13
        Me.DarkLabel4.Text = "is"
        '
        'cmbSelfSwitch
        '
        Me.cmbSelfSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbSelfSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSelfSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSelfSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSelfSwitch.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbSelfSwitch.FormattingEnabled = True
        Me.cmbSelfSwitch.Items.AddRange(New Object() {"None", "1 - A", "2 - B", "3 - C", "4 - D"})
        Me.cmbSelfSwitch.Location = New System.Drawing.Point(108, 98)
        Me.cmbSelfSwitch.Name = "cmbSelfSwitch"
        Me.cmbSelfSwitch.Size = New System.Drawing.Size(89, 21)
        Me.cmbSelfSwitch.TabIndex = 12
        Me.cmbSelfSwitch.Text = Nothing
        '
        'chkSelfSwitch
        '
        Me.chkSelfSwitch.AutoSize = True
        Me.chkSelfSwitch.Location = New System.Drawing.Point(6, 100)
        Me.chkSelfSwitch.Name = "chkSelfSwitch"
        Me.chkSelfSwitch.Size = New System.Drawing.Size(83, 17)
        Me.chkSelfSwitch.TabIndex = 11
        Me.chkSelfSwitch.Text = "Self Switch*"
        '
        'cmbHasItem
        '
        Me.cmbHasItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbHasItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbHasItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHasItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbHasItem.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbHasItem.FormattingEnabled = True
        Me.cmbHasItem.Location = New System.Drawing.Point(108, 71)
        Me.cmbHasItem.Name = "cmbHasItem"
        Me.cmbHasItem.Size = New System.Drawing.Size(204, 21)
        Me.cmbHasItem.TabIndex = 10
        Me.cmbHasItem.Text = Nothing
        '
        'chkHasItem
        '
        Me.chkHasItem.AutoSize = True
        Me.chkHasItem.Location = New System.Drawing.Point(6, 73)
        Me.chkHasItem.Name = "chkHasItem"
        Me.chkHasItem.Size = New System.Drawing.Size(98, 17)
        Me.chkHasItem.TabIndex = 9
        Me.chkHasItem.Text = "Player has Item"
        '
        'cmbPlayerSwitchCompare
        '
        Me.cmbPlayerSwitchCompare.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPlayerSwitchCompare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayerSwitchCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlayerSwitchCompare.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPlayerSwitchCompare.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPlayerSwitchCompare.FormattingEnabled = True
        Me.cmbPlayerSwitchCompare.Items.AddRange(New Object() {"False = 0", "True = 1"})
        Me.cmbPlayerSwitchCompare.Location = New System.Drawing.Point(223, 44)
        Me.cmbPlayerSwitchCompare.Name = "cmbPlayerSwitchCompare"
        Me.cmbPlayerSwitchCompare.Size = New System.Drawing.Size(89, 21)
        Me.cmbPlayerSwitchCompare.TabIndex = 8
        Me.cmbPlayerSwitchCompare.Text = Nothing
        '
        'DarkLabel3
        '
        Me.DarkLabel3.AutoSize = True
        Me.DarkLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel3.Location = New System.Drawing.Point(203, 47)
        Me.DarkLabel3.Name = "DarkLabel3"
        Me.DarkLabel3.Size = New System.Drawing.Size(14, 13)
        Me.DarkLabel3.TabIndex = 7
        Me.DarkLabel3.Text = "is"
        '
        'cmbPlayerSwitch
        '
        Me.cmbPlayerSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPlayerSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayerSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlayerSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPlayerSwitch.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPlayerSwitch.FormattingEnabled = True
        Me.cmbPlayerSwitch.Location = New System.Drawing.Point(108, 44)
        Me.cmbPlayerSwitch.Name = "cmbPlayerSwitch"
        Me.cmbPlayerSwitch.Size = New System.Drawing.Size(89, 21)
        Me.cmbPlayerSwitch.TabIndex = 6
        Me.cmbPlayerSwitch.Text = Nothing
        '
        'chkPlayerSwitch
        '
        Me.chkPlayerSwitch.AutoSize = True
        Me.chkPlayerSwitch.Location = New System.Drawing.Point(6, 46)
        Me.chkPlayerSwitch.Name = "chkPlayerSwitch"
        Me.chkPlayerSwitch.Size = New System.Drawing.Size(90, 17)
        Me.chkPlayerSwitch.TabIndex = 5
        Me.chkPlayerSwitch.Text = "Player Switch"
        '
        'nudPlayerVariable
        '
        Me.nudPlayerVariable.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudPlayerVariable.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudPlayerVariable.Location = New System.Drawing.Point(318, 18)
        Me.nudPlayerVariable.Name = "nudPlayerVariable"
        Me.nudPlayerVariable.Size = New System.Drawing.Size(56, 20)
        Me.nudPlayerVariable.TabIndex = 4
        '
        'cmbPlayervarCompare
        '
        Me.cmbPlayervarCompare.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPlayervarCompare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayervarCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlayervarCompare.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPlayervarCompare.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPlayervarCompare.FormattingEnabled = True
        Me.cmbPlayervarCompare.Items.AddRange(New Object() {"Equal To", "Great Than OrElse Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal"})
        Me.cmbPlayervarCompare.Location = New System.Drawing.Point(223, 17)
        Me.cmbPlayervarCompare.Name = "cmbPlayervarCompare"
        Me.cmbPlayervarCompare.Size = New System.Drawing.Size(89, 21)
        Me.cmbPlayervarCompare.TabIndex = 3
        Me.cmbPlayervarCompare.Text = Nothing
        '
        'DarkLabel2
        '
        Me.DarkLabel2.AutoSize = True
        Me.DarkLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel2.Location = New System.Drawing.Point(203, 23)
        Me.DarkLabel2.Name = "DarkLabel2"
        Me.DarkLabel2.Size = New System.Drawing.Size(14, 13)
        Me.DarkLabel2.TabIndex = 2
        Me.DarkLabel2.Text = "is"
        '
        'cmbPlayerVar
        '
        Me.cmbPlayerVar.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPlayerVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayerVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlayerVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPlayerVar.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPlayerVar.FormattingEnabled = True
        Me.cmbPlayerVar.Location = New System.Drawing.Point(108, 17)
        Me.cmbPlayerVar.Name = "cmbPlayerVar"
        Me.cmbPlayerVar.Size = New System.Drawing.Size(89, 21)
        Me.cmbPlayerVar.TabIndex = 1
        Me.cmbPlayerVar.Text = Nothing
        '
        'chkPlayerVar
        '
        Me.chkPlayerVar.AutoSize = True
        Me.chkPlayerVar.Location = New System.Drawing.Point(6, 19)
        Me.chkPlayerVar.Name = "chkPlayerVar"
        Me.chkPlayerVar.Size = New System.Drawing.Size(96, 17)
        Me.chkPlayerVar.TabIndex = 0
        Me.chkPlayerVar.Text = "Player Variable"
        '
        'DarkGroupBox6
        '
        Me.DarkGroupBox6.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox6.Controls.Add(Me.chkShowName)
        Me.DarkGroupBox6.Controls.Add(Me.chkWalkThrough)
        Me.DarkGroupBox6.Controls.Add(Me.chkDirFix)
        Me.DarkGroupBox6.Controls.Add(Me.chkWalkAnim)
        Me.DarkGroupBox6.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox6.Location = New System.Drawing.Point(3, 457)
        Me.DarkGroupBox6.Name = "DarkGroupBox6"
        Me.DarkGroupBox6.Size = New System.Drawing.Size(176, 112)
        Me.DarkGroupBox6.TabIndex = 5
        Me.DarkGroupBox6.TabStop = False
        Me.DarkGroupBox6.Text = "Options"
        '
        'chkShowName
        '
        Me.chkShowName.AutoSize = True
        Me.chkShowName.Location = New System.Drawing.Point(6, 88)
        Me.chkShowName.Name = "chkShowName"
        Me.chkShowName.Size = New System.Drawing.Size(84, 17)
        Me.chkShowName.TabIndex = 3
        Me.chkShowName.Text = "Show Name"
        '
        'chkWalkThrough
        '
        Me.chkWalkThrough.AutoSize = True
        Me.chkWalkThrough.Location = New System.Drawing.Point(6, 65)
        Me.chkWalkThrough.Name = "chkWalkThrough"
        Me.chkWalkThrough.Size = New System.Drawing.Size(94, 17)
        Me.chkWalkThrough.TabIndex = 2
        Me.chkWalkThrough.Text = "Walk Through"
        '
        'chkDirFix
        '
        Me.chkDirFix.AutoSize = True
        Me.chkDirFix.Location = New System.Drawing.Point(6, 42)
        Me.chkDirFix.Name = "chkDirFix"
        Me.chkDirFix.Size = New System.Drawing.Size(96, 17)
        Me.chkDirFix.TabIndex = 1
        Me.chkDirFix.Text = "Direction Fixed"
        '
        'chkWalkAnim
        '
        Me.chkWalkAnim.AutoSize = True
        Me.chkWalkAnim.Location = New System.Drawing.Point(6, 19)
        Me.chkWalkAnim.Name = "chkWalkAnim"
        Me.chkWalkAnim.Size = New System.Drawing.Size(117, 17)
        Me.chkWalkAnim.TabIndex = 0
        Me.chkWalkAnim.Text = "No Walk Animation"
        '
        'btnLabeling
        '
        Me.btnLabeling.Location = New System.Drawing.Point(3, 584)
        Me.btnLabeling.Name = "btnLabeling"
        Me.btnLabeling.Padding = New System.Windows.Forms.Padding(5)
        Me.btnLabeling.Size = New System.Drawing.Size(170, 23)
        Me.btnLabeling.TabIndex = 6
        Me.btnLabeling.Text = "Edit Variables/Switches"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(719, 584)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(638, 584)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 8
        Me.btnOk.Text = "Ok"
        '
        'fraMoveRoute
        '
        Me.fraMoveRoute.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraMoveRoute.Controls.Add(Me.btnMoveRouteOk)
        Me.fraMoveRoute.Controls.Add(Me.btnMoveRouteCancel)
        Me.fraMoveRoute.Controls.Add(Me.chkRepeatRoute)
        Me.fraMoveRoute.Controls.Add(Me.chkIgnoreMove)
        Me.fraMoveRoute.Controls.Add(Me.DarkGroupBox10)
        Me.fraMoveRoute.Controls.Add(Me.lstMoveRoute)
        Me.fraMoveRoute.Controls.Add(Me.cmbEvent)
        Me.fraMoveRoute.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraMoveRoute.Location = New System.Drawing.Point(800, 12)
        Me.fraMoveRoute.Name = "fraMoveRoute"
        Me.fraMoveRoute.Size = New System.Drawing.Size(93, 85)
        Me.fraMoveRoute.TabIndex = 0
        Me.fraMoveRoute.TabStop = False
        Me.fraMoveRoute.Text = "Move Route"
        Me.fraMoveRoute.Visible = False
        '
        'btnMoveRouteOk
        '
        Me.btnMoveRouteOk.Location = New System.Drawing.Point(642, 431)
        Me.btnMoveRouteOk.Name = "btnMoveRouteOk"
        Me.btnMoveRouteOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnMoveRouteOk.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveRouteOk.TabIndex = 7
        Me.btnMoveRouteOk.Text = "Ok"
        '
        'btnMoveRouteCancel
        '
        Me.btnMoveRouteCancel.Location = New System.Drawing.Point(723, 431)
        Me.btnMoveRouteCancel.Name = "btnMoveRouteCancel"
        Me.btnMoveRouteCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnMoveRouteCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveRouteCancel.TabIndex = 6
        Me.btnMoveRouteCancel.Text = "Cancel"
        '
        'chkRepeatRoute
        '
        Me.chkRepeatRoute.AutoSize = True
        Me.chkRepeatRoute.Location = New System.Drawing.Point(6, 454)
        Me.chkRepeatRoute.Name = "chkRepeatRoute"
        Me.chkRepeatRoute.Size = New System.Drawing.Size(93, 17)
        Me.chkRepeatRoute.TabIndex = 5
        Me.chkRepeatRoute.Text = "Repeat Route"
        '
        'chkIgnoreMove
        '
        Me.chkIgnoreMove.AutoSize = True
        Me.chkIgnoreMove.Location = New System.Drawing.Point(6, 431)
        Me.chkIgnoreMove.Name = "chkIgnoreMove"
        Me.chkIgnoreMove.Size = New System.Drawing.Size(149, 17)
        Me.chkIgnoreMove.TabIndex = 4
        Me.chkIgnoreMove.Text = "Ignore if event can't move"
        '
        'DarkGroupBox10
        '
        Me.DarkGroupBox10.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox10.Controls.Add(Me.lstvwMoveRoute)
        Me.DarkGroupBox10.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox10.Location = New System.Drawing.Point(203, 10)
        Me.DarkGroupBox10.Name = "DarkGroupBox10"
        Me.DarkGroupBox10.Size = New System.Drawing.Size(595, 415)
        Me.DarkGroupBox10.TabIndex = 3
        Me.DarkGroupBox10.TabStop = False
        Me.DarkGroupBox10.Text = "Commands"
        '
        'lstvwMoveRoute
        '
        Me.lstvwMoveRoute.AutoArrange = False
        Me.lstvwMoveRoute.BackColor = System.Drawing.Color.DimGray
        Me.lstvwMoveRoute.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstvwMoveRoute.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lstvwMoveRoute.Dock = System.Windows.Forms.DockStyle.Top
        Me.lstvwMoveRoute.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstvwMoveRoute.ForeColor = System.Drawing.Color.Gainsboro
        ListViewGroup10.Header = "Movement"
        ListViewGroup10.Name = "lstVgMovement"
        ListViewGroup11.Header = "Wait"
        ListViewGroup11.Name = "lstVgWait"
        ListViewGroup12.Header = "Turning"
        ListViewGroup12.Name = "lstVgTurn"
        ListViewGroup13.Header = "Speed"
        ListViewGroup13.Name = "lstVgSpeed"
        ListViewGroup14.Header = "Walk Animation"
        ListViewGroup14.Name = "lstVgWalk"
        ListViewGroup15.Header = "Fixed Direction"
        ListViewGroup15.Name = "lstVgDirection"
        ListViewGroup16.Header = "WalkThrough"
        ListViewGroup16.Name = "lstVgWalkThrough"
        ListViewGroup17.Header = "Set Position"
        ListViewGroup17.Name = "lstVgSetposition"
        ListViewGroup18.Header = "Set Graphic"
        ListViewGroup18.Name = "lstVgSetGraphic"
        Me.lstvwMoveRoute.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup10, ListViewGroup11, ListViewGroup12, ListViewGroup13, ListViewGroup14, ListViewGroup15, ListViewGroup16, ListViewGroup17, ListViewGroup18})
        Me.lstvwMoveRoute.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        ListViewItem44.Group = ListViewGroup10
        ListViewItem45.Group = ListViewGroup10
        ListViewItem45.IndentCount = 1
        ListViewItem46.Group = ListViewGroup10
        ListViewItem47.Group = ListViewGroup10
        ListViewItem47.IndentCount = 1
        ListViewItem48.Group = ListViewGroup10
        ListViewItem49.Group = ListViewGroup10
        ListViewItem50.Group = ListViewGroup10
        ListViewItem51.Group = ListViewGroup10
        ListViewItem52.Group = ListViewGroup10
        ListViewItem53.Group = ListViewGroup11
        ListViewItem54.Group = ListViewGroup11
        ListViewItem55.Group = ListViewGroup11
        ListViewItem56.Group = ListViewGroup12
        ListViewItem57.Group = ListViewGroup12
        ListViewItem58.Group = ListViewGroup12
        ListViewItem59.Group = ListViewGroup12
        ListViewItem60.Group = ListViewGroup12
        ListViewItem61.Group = ListViewGroup12
        ListViewItem62.Group = ListViewGroup12
        ListViewItem63.Group = ListViewGroup12
        ListViewItem64.Group = ListViewGroup12
        ListViewItem65.Group = ListViewGroup12
        ListViewItem66.Group = ListViewGroup13
        ListViewItem67.Group = ListViewGroup13
        ListViewItem68.Group = ListViewGroup13
        ListViewItem69.Group = ListViewGroup13
        ListViewItem70.Group = ListViewGroup13
        ListViewItem71.Group = ListViewGroup13
        ListViewItem72.Group = ListViewGroup13
        ListViewItem73.Group = ListViewGroup13
        ListViewItem74.Group = ListViewGroup13
        ListViewItem75.Group = ListViewGroup13
        ListViewItem76.Group = ListViewGroup13
        ListViewItem77.Group = ListViewGroup14
        ListViewItem78.Group = ListViewGroup14
        ListViewItem79.Group = ListViewGroup15
        ListViewItem80.Group = ListViewGroup15
        ListViewItem81.Group = ListViewGroup16
        ListViewItem82.Group = ListViewGroup16
        ListViewItem83.Group = ListViewGroup17
        ListViewItem84.Group = ListViewGroup17
        ListViewItem85.Group = ListViewGroup17
        ListViewItem86.Group = ListViewGroup18
        Me.lstvwMoveRoute.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem44, ListViewItem45, ListViewItem46, ListViewItem47, ListViewItem48, ListViewItem49, ListViewItem50, ListViewItem51, ListViewItem52, ListViewItem53, ListViewItem54, ListViewItem55, ListViewItem56, ListViewItem57, ListViewItem58, ListViewItem59, ListViewItem60, ListViewItem61, ListViewItem62, ListViewItem63, ListViewItem64, ListViewItem65, ListViewItem66, ListViewItem67, ListViewItem68, ListViewItem69, ListViewItem70, ListViewItem71, ListViewItem72, ListViewItem73, ListViewItem74, ListViewItem75, ListViewItem76, ListViewItem77, ListViewItem78, ListViewItem79, ListViewItem80, ListViewItem81, ListViewItem82, ListViewItem83, ListViewItem84, ListViewItem85, ListViewItem86})
        Me.lstvwMoveRoute.LabelWrap = False
        Me.lstvwMoveRoute.Location = New System.Drawing.Point(3, 16)
        Me.lstvwMoveRoute.MultiSelect = False
        Me.lstvwMoveRoute.Name = "lstvwMoveRoute"
        Me.lstvwMoveRoute.Size = New System.Drawing.Size(589, 397)
        Me.lstvwMoveRoute.TabIndex = 5
        Me.lstvwMoveRoute.UseCompatibleStateImageBehavior = False
        Me.lstvwMoveRoute.View = System.Windows.Forms.View.Tile
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = ""
        Me.ColumnHeader3.Width = 150
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = ""
        Me.ColumnHeader4.Width = 150
        '
        'lstMoveRoute
        '
        Me.lstMoveRoute.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstMoveRoute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstMoveRoute.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstMoveRoute.FormattingEnabled = True
        Me.lstMoveRoute.Location = New System.Drawing.Point(6, 46)
        Me.lstMoveRoute.Name = "lstMoveRoute"
        Me.lstMoveRoute.Size = New System.Drawing.Size(191, 379)
        Me.lstMoveRoute.TabIndex = 2
        '
        'cmbEvent
        '
        Me.cmbEvent.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbEvent.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbEvent.FormattingEnabled = True
        Me.cmbEvent.Location = New System.Drawing.Point(6, 19)
        Me.cmbEvent.Name = "cmbEvent"
        Me.cmbEvent.Size = New System.Drawing.Size(191, 21)
        Me.cmbEvent.TabIndex = 0
        Me.cmbEvent.Text = Nothing
        '
        'fraGraphic
        '
        Me.fraGraphic.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraGraphic.Controls.Add(Me.pnlGraphicSel)
        Me.fraGraphic.Controls.Add(Me.btnGraphicOk)
        Me.fraGraphic.Controls.Add(Me.btnGraphicCancel)
        Me.fraGraphic.Controls.Add(Me.DarkLabel13)
        Me.fraGraphic.Controls.Add(Me.nudGraphic)
        Me.fraGraphic.Controls.Add(Me.DarkLabel12)
        Me.fraGraphic.Controls.Add(Me.cmbGraphic)
        Me.fraGraphic.Controls.Add(Me.DarkLabel11)
        Me.fraGraphic.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraGraphic.Location = New System.Drawing.Point(806, 113)
        Me.fraGraphic.Name = "fraGraphic"
        Me.fraGraphic.Size = New System.Drawing.Size(78, 72)
        Me.fraGraphic.TabIndex = 9
        Me.fraGraphic.TabStop = False
        Me.fraGraphic.Text = "Graphic Selection"
        Me.fraGraphic.Visible = False
        '
        'pnlGraphicSel
        '
        Me.pnlGraphicSel.AutoScroll = True
        Me.pnlGraphicSel.Controls.Add(Me.picGraphicSel)
        Me.pnlGraphicSel.Location = New System.Drawing.Point(6, 45)
        Me.pnlGraphicSel.Name = "pnlGraphicSel"
        Me.pnlGraphicSel.Size = New System.Drawing.Size(808, 519)
        Me.pnlGraphicSel.TabIndex = 9
        '
        'picGraphicSel
        '
        Me.picGraphicSel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picGraphicSel.Location = New System.Drawing.Point(0, 0)
        Me.picGraphicSel.Name = "picGraphicSel"
        Me.picGraphicSel.Size = New System.Drawing.Size(802, 514)
        Me.picGraphicSel.TabIndex = 5
        Me.picGraphicSel.TabStop = False
        '
        'btnGraphicOk
        '
        Me.btnGraphicOk.Location = New System.Drawing.Point(652, 570)
        Me.btnGraphicOk.Name = "btnGraphicOk"
        Me.btnGraphicOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnGraphicOk.Size = New System.Drawing.Size(75, 23)
        Me.btnGraphicOk.TabIndex = 8
        Me.btnGraphicOk.Text = "Ok"
        '
        'btnGraphicCancel
        '
        Me.btnGraphicCancel.Location = New System.Drawing.Point(733, 570)
        Me.btnGraphicCancel.Name = "btnGraphicCancel"
        Me.btnGraphicCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnGraphicCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnGraphicCancel.TabIndex = 7
        Me.btnGraphicCancel.Text = "Cancel"
        '
        'DarkLabel13
        '
        Me.DarkLabel13.AutoSize = True
        Me.DarkLabel13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel13.Location = New System.Drawing.Point(9, 571)
        Me.DarkLabel13.Name = "DarkLabel13"
        Me.DarkLabel13.Size = New System.Drawing.Size(158, 13)
        Me.DarkLabel13.TabIndex = 6
        Me.DarkLabel13.Text = "Hold Shift to select multiple tiles."
        '
        'nudGraphic
        '
        Me.nudGraphic.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudGraphic.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudGraphic.Location = New System.Drawing.Point(380, 19)
        Me.nudGraphic.Name = "nudGraphic"
        Me.nudGraphic.Size = New System.Drawing.Size(120, 20)
        Me.nudGraphic.TabIndex = 3
        '
        'DarkLabel12
        '
        Me.DarkLabel12.AutoSize = True
        Me.DarkLabel12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel12.Location = New System.Drawing.Point(327, 21)
        Me.DarkLabel12.Name = "DarkLabel12"
        Me.DarkLabel12.Size = New System.Drawing.Size(47, 13)
        Me.DarkLabel12.TabIndex = 2
        Me.DarkLabel12.Text = "Number:"
        '
        'cmbGraphic
        '
        Me.cmbGraphic.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbGraphic.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbGraphic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGraphic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbGraphic.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbGraphic.FormattingEnabled = True
        Me.cmbGraphic.Items.AddRange(New Object() {"None", "Character", "Tileset"})
        Me.cmbGraphic.Location = New System.Drawing.Point(104, 18)
        Me.cmbGraphic.Name = "cmbGraphic"
        Me.cmbGraphic.Size = New System.Drawing.Size(217, 21)
        Me.cmbGraphic.TabIndex = 1
        Me.cmbGraphic.Text = Nothing
        '
        'DarkLabel11
        '
        Me.DarkLabel11.AutoSize = True
        Me.DarkLabel11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel11.Location = New System.Drawing.Point(19, 21)
        Me.DarkLabel11.Name = "DarkLabel11"
        Me.DarkLabel11.Size = New System.Drawing.Size(79, 13)
        Me.DarkLabel11.TabIndex = 0
        Me.DarkLabel11.Text = "Graphics Type:"
        '
        'fraDialogue
        '
        Me.fraDialogue.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraDialogue.Controls.Add(Me.fraConditionalBranch)
        Me.fraDialogue.Controls.Add(Me.fraPlayAnimation)
        Me.fraDialogue.Controls.Add(Me.fraMoveRouteWait)
        Me.fraDialogue.Controls.Add(Me.fraCustomScript)
        Me.fraDialogue.Controls.Add(Me.fraSetWeather)
        Me.fraDialogue.Controls.Add(Me.fraSpawnNpc)
        Me.fraDialogue.Controls.Add(Me.fraGiveExp)
        Me.fraDialogue.Controls.Add(Me.fraEndQuest)
        Me.fraDialogue.Controls.Add(Me.fraSetAccess)
        Me.fraDialogue.Controls.Add(Me.fraSetWait)
        Me.fraDialogue.Controls.Add(Me.fraShowPic)
        Me.fraDialogue.Controls.Add(Me.fraOpenShop)
        Me.fraDialogue.Controls.Add(Me.fraChangeLevel)
        Me.fraDialogue.Controls.Add(Me.fraChangeGender)
        Me.fraDialogue.Controls.Add(Me.fraGoToLabel)
        Me.fraDialogue.Controls.Add(Me.fraHidePic)
        Me.fraDialogue.Controls.Add(Me.fraBeginQuest)
        Me.fraDialogue.Controls.Add(Me.fraShowChoices)
        Me.fraDialogue.Controls.Add(Me.fraPlayerVariable)
        Me.fraDialogue.Controls.Add(Me.fraChangeSprite)
        Me.fraDialogue.Controls.Add(Me.fraSetSelfSwitch)
        Me.fraDialogue.Controls.Add(Me.fraMapTint)
        Me.fraDialogue.Controls.Add(Me.fraShowChatBubble)
        Me.fraDialogue.Controls.Add(Me.fraPlaySound)
        Me.fraDialogue.Controls.Add(Me.fraChangePK)
        Me.fraDialogue.Controls.Add(Me.fraCreateLabel)
        Me.fraDialogue.Controls.Add(Me.fraChangeClass)
        Me.fraDialogue.Controls.Add(Me.fraChangeSkills)
        Me.fraDialogue.Controls.Add(Me.fraCompleteTask)
        Me.fraDialogue.Controls.Add(Me.fraPlayerWarp)
        Me.fraDialogue.Controls.Add(Me.fraSetFog)
        Me.fraDialogue.Controls.Add(Me.fraShowText)
        Me.fraDialogue.Controls.Add(Me.fraAddText)
        Me.fraDialogue.Controls.Add(Me.fraPlayerSwitch)
        Me.fraDialogue.Controls.Add(Me.fraChangeItems)
        Me.fraDialogue.Controls.Add(Me.fraPlayBGM)
        Me.fraDialogue.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraDialogue.Location = New System.Drawing.Point(905, 12)
        Me.fraDialogue.Name = "fraDialogue"
        Me.fraDialogue.Size = New System.Drawing.Size(665, 595)
        Me.fraDialogue.TabIndex = 10
        Me.fraDialogue.TabStop = False
        Me.fraDialogue.Visible = False
        '
        'fraPlayAnimation
        '
        Me.fraPlayAnimation.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlayAnimation.Controls.Add(Me.btnPlayAnimationOk)
        Me.fraPlayAnimation.Controls.Add(Me.btnPlayAnimationCancel)
        Me.fraPlayAnimation.Controls.Add(Me.lblPlayAnimY)
        Me.fraPlayAnimation.Controls.Add(Me.lblPlayAnimX)
        Me.fraPlayAnimation.Controls.Add(Me.cmbPlayAnimEvent)
        Me.fraPlayAnimation.Controls.Add(Me.DarkLabel62)
        Me.fraPlayAnimation.Controls.Add(Me.cmbAnimTargetType)
        Me.fraPlayAnimation.Controls.Add(Me.nudPlayAnimTileY)
        Me.fraPlayAnimation.Controls.Add(Me.nudPlayAnimTileX)
        Me.fraPlayAnimation.Controls.Add(Me.DarkLabel61)
        Me.fraPlayAnimation.Controls.Add(Me.cmbPlayAnim)
        Me.fraPlayAnimation.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlayAnimation.Location = New System.Drawing.Point(401, 257)
        Me.fraPlayAnimation.Name = "fraPlayAnimation"
        Me.fraPlayAnimation.Size = New System.Drawing.Size(248, 162)
        Me.fraPlayAnimation.TabIndex = 36
        Me.fraPlayAnimation.TabStop = False
        Me.fraPlayAnimation.Text = "Play Animation"
        Me.fraPlayAnimation.Visible = False
        '
        'btnPlayAnimationOk
        '
        Me.btnPlayAnimationOk.Location = New System.Drawing.Point(86, 132)
        Me.btnPlayAnimationOk.Name = "btnPlayAnimationOk"
        Me.btnPlayAnimationOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPlayAnimationOk.Size = New System.Drawing.Size(75, 23)
        Me.btnPlayAnimationOk.TabIndex = 36
        Me.btnPlayAnimationOk.Text = "Ok"
        '
        'btnPlayAnimationCancel
        '
        Me.btnPlayAnimationCancel.Location = New System.Drawing.Point(167, 132)
        Me.btnPlayAnimationCancel.Name = "btnPlayAnimationCancel"
        Me.btnPlayAnimationCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPlayAnimationCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnPlayAnimationCancel.TabIndex = 35
        Me.btnPlayAnimationCancel.Text = "Cancel"
        '
        'lblPlayAnimY
        '
        Me.lblPlayAnimY.AutoSize = True
        Me.lblPlayAnimY.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.lblPlayAnimY.Location = New System.Drawing.Point(131, 106)
        Me.lblPlayAnimY.Name = "lblPlayAnimY"
        Me.lblPlayAnimY.Size = New System.Drawing.Size(61, 13)
        Me.lblPlayAnimY.TabIndex = 34
        Me.lblPlayAnimY.Text = "Map Tile Y:"
        '
        'lblPlayAnimX
        '
        Me.lblPlayAnimX.AutoSize = True
        Me.lblPlayAnimX.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.lblPlayAnimX.Location = New System.Drawing.Point(6, 106)
        Me.lblPlayAnimX.Name = "lblPlayAnimX"
        Me.lblPlayAnimX.Size = New System.Drawing.Size(61, 13)
        Me.lblPlayAnimX.TabIndex = 33
        Me.lblPlayAnimX.Text = "Map Tile X:"
        '
        'cmbPlayAnimEvent
        '
        Me.cmbPlayAnimEvent.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPlayAnimEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayAnimEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlayAnimEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPlayAnimEvent.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPlayAnimEvent.FormattingEnabled = True
        Me.cmbPlayAnimEvent.Location = New System.Drawing.Point(83, 73)
        Me.cmbPlayAnimEvent.Name = "cmbPlayAnimEvent"
        Me.cmbPlayAnimEvent.Size = New System.Drawing.Size(159, 21)
        Me.cmbPlayAnimEvent.TabIndex = 32
        Me.cmbPlayAnimEvent.Text = Nothing
        '
        'DarkLabel62
        '
        Me.DarkLabel62.AutoSize = True
        Me.DarkLabel62.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel62.Location = New System.Drawing.Point(4, 49)
        Me.DarkLabel62.Name = "DarkLabel62"
        Me.DarkLabel62.Size = New System.Drawing.Size(65, 13)
        Me.DarkLabel62.TabIndex = 31
        Me.DarkLabel62.Text = "Target Type"
        '
        'cmbAnimTargetType
        '
        Me.cmbAnimTargetType.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbAnimTargetType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbAnimTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnimTargetType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbAnimTargetType.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbAnimTargetType.FormattingEnabled = True
        Me.cmbAnimTargetType.Items.AddRange(New Object() {"Player", "Event", "Tile"})
        Me.cmbAnimTargetType.Location = New System.Drawing.Point(83, 46)
        Me.cmbAnimTargetType.Name = "cmbAnimTargetType"
        Me.cmbAnimTargetType.Size = New System.Drawing.Size(159, 21)
        Me.cmbAnimTargetType.TabIndex = 30
        Me.cmbAnimTargetType.Text = Nothing
        '
        'nudPlayAnimTileY
        '
        Me.nudPlayAnimTileY.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudPlayAnimTileY.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudPlayAnimTileY.Location = New System.Drawing.Point(198, 104)
        Me.nudPlayAnimTileY.Name = "nudPlayAnimTileY"
        Me.nudPlayAnimTileY.Size = New System.Drawing.Size(44, 20)
        Me.nudPlayAnimTileY.TabIndex = 29
        '
        'nudPlayAnimTileX
        '
        Me.nudPlayAnimTileX.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudPlayAnimTileX.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudPlayAnimTileX.Location = New System.Drawing.Point(73, 104)
        Me.nudPlayAnimTileX.Name = "nudPlayAnimTileX"
        Me.nudPlayAnimTileX.Size = New System.Drawing.Size(44, 20)
        Me.nudPlayAnimTileX.TabIndex = 28
        '
        'DarkLabel61
        '
        Me.DarkLabel61.AutoSize = True
        Me.DarkLabel61.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel61.Location = New System.Drawing.Point(6, 22)
        Me.DarkLabel61.Name = "DarkLabel61"
        Me.DarkLabel61.Size = New System.Drawing.Size(56, 13)
        Me.DarkLabel61.TabIndex = 1
        Me.DarkLabel61.Text = "Animation:"
        '
        'cmbPlayAnim
        '
        Me.cmbPlayAnim.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPlayAnim.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayAnim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlayAnim.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPlayAnim.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPlayAnim.FormattingEnabled = True
        Me.cmbPlayAnim.Location = New System.Drawing.Point(62, 19)
        Me.cmbPlayAnim.Name = "cmbPlayAnim"
        Me.cmbPlayAnim.Size = New System.Drawing.Size(180, 21)
        Me.cmbPlayAnim.TabIndex = 0
        Me.cmbPlayAnim.Text = Nothing
        '
        'fraMoveRouteWait
        '
        Me.fraMoveRouteWait.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraMoveRouteWait.Controls.Add(Me.btnMoveWaitCancel)
        Me.fraMoveRouteWait.Controls.Add(Me.btnMoveWaitOk)
        Me.fraMoveRouteWait.Controls.Add(Me.DarkLabel79)
        Me.fraMoveRouteWait.Controls.Add(Me.cmbMoveWait)
        Me.fraMoveRouteWait.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraMoveRouteWait.Location = New System.Drawing.Point(401, 495)
        Me.fraMoveRouteWait.Name = "fraMoveRouteWait"
        Me.fraMoveRouteWait.Size = New System.Drawing.Size(248, 75)
        Me.fraMoveRouteWait.TabIndex = 48
        Me.fraMoveRouteWait.TabStop = False
        Me.fraMoveRouteWait.Text = "Move Route Wait"
        Me.fraMoveRouteWait.Visible = False
        '
        'btnMoveWaitCancel
        '
        Me.btnMoveWaitCancel.Location = New System.Drawing.Point(167, 46)
        Me.btnMoveWaitCancel.Name = "btnMoveWaitCancel"
        Me.btnMoveWaitCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnMoveWaitCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveWaitCancel.TabIndex = 26
        Me.btnMoveWaitCancel.Text = "Cancel"
        '
        'btnMoveWaitOk
        '
        Me.btnMoveWaitOk.Location = New System.Drawing.Point(86, 46)
        Me.btnMoveWaitOk.Name = "btnMoveWaitOk"
        Me.btnMoveWaitOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnMoveWaitOk.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveWaitOk.TabIndex = 27
        Me.btnMoveWaitOk.Text = "Ok"
        '
        'DarkLabel79
        '
        Me.DarkLabel79.AutoSize = True
        Me.DarkLabel79.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel79.Location = New System.Drawing.Point(7, 22)
        Me.DarkLabel79.Name = "DarkLabel79"
        Me.DarkLabel79.Size = New System.Drawing.Size(38, 13)
        Me.DarkLabel79.TabIndex = 1
        Me.DarkLabel79.Text = "Event:"
        '
        'cmbMoveWait
        '
        Me.cmbMoveWait.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbMoveWait.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoveWait.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMoveWait.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbMoveWait.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbMoveWait.FormattingEnabled = True
        Me.cmbMoveWait.Location = New System.Drawing.Point(51, 19)
        Me.cmbMoveWait.Name = "cmbMoveWait"
        Me.cmbMoveWait.Size = New System.Drawing.Size(191, 21)
        Me.cmbMoveWait.TabIndex = 0
        Me.cmbMoveWait.Text = Nothing
        '
        'fraCustomScript
        '
        Me.fraCustomScript.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraCustomScript.Controls.Add(Me.nudCustomScript)
        Me.fraCustomScript.Controls.Add(Me.DarkLabel78)
        Me.fraCustomScript.Controls.Add(Me.btnCustomScriptCancel)
        Me.fraCustomScript.Controls.Add(Me.btnCustomScriptOk)
        Me.fraCustomScript.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraCustomScript.Location = New System.Drawing.Point(401, 396)
        Me.fraCustomScript.Name = "fraCustomScript"
        Me.fraCustomScript.Size = New System.Drawing.Size(248, 95)
        Me.fraCustomScript.TabIndex = 47
        Me.fraCustomScript.TabStop = False
        Me.fraCustomScript.Text = "Execute Custom Script"
        Me.fraCustomScript.Visible = False
        '
        'nudCustomScript
        '
        Me.nudCustomScript.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudCustomScript.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudCustomScript.Location = New System.Drawing.Point(67, 19)
        Me.nudCustomScript.Name = "nudCustomScript"
        Me.nudCustomScript.Size = New System.Drawing.Size(169, 20)
        Me.nudCustomScript.TabIndex = 1
        '
        'DarkLabel78
        '
        Me.DarkLabel78.AutoSize = True
        Me.DarkLabel78.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel78.Location = New System.Drawing.Point(10, 21)
        Me.DarkLabel78.Name = "DarkLabel78"
        Me.DarkLabel78.Size = New System.Drawing.Size(34, 13)
        Me.DarkLabel78.TabIndex = 0
        Me.DarkLabel78.Text = "Case:"
        '
        'btnCustomScriptCancel
        '
        Me.btnCustomScriptCancel.Location = New System.Drawing.Point(161, 45)
        Me.btnCustomScriptCancel.Name = "btnCustomScriptCancel"
        Me.btnCustomScriptCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCustomScriptCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCustomScriptCancel.TabIndex = 24
        Me.btnCustomScriptCancel.Text = "Cancel"
        '
        'btnCustomScriptOk
        '
        Me.btnCustomScriptOk.Location = New System.Drawing.Point(80, 45)
        Me.btnCustomScriptOk.Name = "btnCustomScriptOk"
        Me.btnCustomScriptOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCustomScriptOk.Size = New System.Drawing.Size(75, 23)
        Me.btnCustomScriptOk.TabIndex = 25
        Me.btnCustomScriptOk.Text = "Ok"
        '
        'fraSetWeather
        '
        Me.fraSetWeather.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSetWeather.Controls.Add(Me.btnSetWeatherOk)
        Me.fraSetWeather.Controls.Add(Me.btnSetWeatherCancel)
        Me.fraSetWeather.Controls.Add(Me.DarkLabel76)
        Me.fraSetWeather.Controls.Add(Me.nudWeatherIntensity)
        Me.fraSetWeather.Controls.Add(Me.DarkLabel75)
        Me.fraSetWeather.Controls.Add(Me.CmbWeather)
        Me.fraSetWeather.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSetWeather.Location = New System.Drawing.Point(401, 352)
        Me.fraSetWeather.Name = "fraSetWeather"
        Me.fraSetWeather.Size = New System.Drawing.Size(248, 95)
        Me.fraSetWeather.TabIndex = 44
        Me.fraSetWeather.TabStop = False
        Me.fraSetWeather.Text = "Set Weather"
        Me.fraSetWeather.Visible = False
        '
        'btnSetWeatherOk
        '
        Me.btnSetWeatherOk.Location = New System.Drawing.Point(46, 66)
        Me.btnSetWeatherOk.Name = "btnSetWeatherOk"
        Me.btnSetWeatherOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSetWeatherOk.Size = New System.Drawing.Size(75, 23)
        Me.btnSetWeatherOk.TabIndex = 34
        Me.btnSetWeatherOk.Text = "Ok"
        '
        'btnSetWeatherCancel
        '
        Me.btnSetWeatherCancel.Location = New System.Drawing.Point(127, 66)
        Me.btnSetWeatherCancel.Name = "btnSetWeatherCancel"
        Me.btnSetWeatherCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSetWeatherCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnSetWeatherCancel.TabIndex = 33
        Me.btnSetWeatherCancel.Text = "Cancel"
        '
        'DarkLabel76
        '
        Me.DarkLabel76.AutoSize = True
        Me.DarkLabel76.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel76.Location = New System.Drawing.Point(8, 44)
        Me.DarkLabel76.Name = "DarkLabel76"
        Me.DarkLabel76.Size = New System.Drawing.Size(49, 13)
        Me.DarkLabel76.TabIndex = 32
        Me.DarkLabel76.Text = "Intensity:"
        '
        'nudWeatherIntensity
        '
        Me.nudWeatherIntensity.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudWeatherIntensity.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudWeatherIntensity.Location = New System.Drawing.Point(87, 41)
        Me.nudWeatherIntensity.Name = "nudWeatherIntensity"
        Me.nudWeatherIntensity.Size = New System.Drawing.Size(155, 20)
        Me.nudWeatherIntensity.TabIndex = 31
        '
        'DarkLabel75
        '
        Me.DarkLabel75.AutoSize = True
        Me.DarkLabel75.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel75.Location = New System.Drawing.Point(6, 18)
        Me.DarkLabel75.Name = "DarkLabel75"
        Me.DarkLabel75.Size = New System.Drawing.Size(75, 13)
        Me.DarkLabel75.TabIndex = 1
        Me.DarkLabel75.Text = "Weather Type"
        '
        'CmbWeather
        '
        Me.CmbWeather.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.CmbWeather.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbWeather.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbWeather.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmbWeather.ForeColor = System.Drawing.Color.Gainsboro
        Me.CmbWeather.FormattingEnabled = True
        Me.CmbWeather.Items.AddRange(New Object() {"None", "Rain", "Snow", "Hail", "Sand Storm", "Storm"})
        Me.CmbWeather.Location = New System.Drawing.Point(86, 15)
        Me.CmbWeather.Name = "CmbWeather"
        Me.CmbWeather.Size = New System.Drawing.Size(155, 21)
        Me.CmbWeather.TabIndex = 0
        Me.CmbWeather.Text = Nothing
        '
        'fraSpawnNpc
        '
        Me.fraSpawnNpc.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSpawnNpc.Controls.Add(Me.btnSpawnNpcOk)
        Me.fraSpawnNpc.Controls.Add(Me.btnSpawnNpcCancel)
        Me.fraSpawnNpc.Controls.Add(Me.cmbSpawnNpc)
        Me.fraSpawnNpc.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSpawnNpc.Location = New System.Drawing.Point(401, 412)
        Me.fraSpawnNpc.Name = "fraSpawnNpc"
        Me.fraSpawnNpc.Size = New System.Drawing.Size(248, 77)
        Me.fraSpawnNpc.TabIndex = 46
        Me.fraSpawnNpc.TabStop = False
        Me.fraSpawnNpc.Text = "Spawn Npc"
        Me.fraSpawnNpc.Visible = False
        '
        'btnSpawnNpcOk
        '
        Me.btnSpawnNpcOk.Location = New System.Drawing.Point(46, 47)
        Me.btnSpawnNpcOk.Name = "btnSpawnNpcOk"
        Me.btnSpawnNpcOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSpawnNpcOk.Size = New System.Drawing.Size(75, 23)
        Me.btnSpawnNpcOk.TabIndex = 27
        Me.btnSpawnNpcOk.Text = "Ok"
        '
        'btnSpawnNpcCancel
        '
        Me.btnSpawnNpcCancel.Location = New System.Drawing.Point(127, 47)
        Me.btnSpawnNpcCancel.Name = "btnSpawnNpcCancel"
        Me.btnSpawnNpcCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSpawnNpcCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnSpawnNpcCancel.TabIndex = 26
        Me.btnSpawnNpcCancel.Text = "Cancel"
        '
        'cmbSpawnNpc
        '
        Me.cmbSpawnNpc.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbSpawnNpc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSpawnNpc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpawnNpc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSpawnNpc.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbSpawnNpc.FormattingEnabled = True
        Me.cmbSpawnNpc.Location = New System.Drawing.Point(6, 19)
        Me.cmbSpawnNpc.Name = "cmbSpawnNpc"
        Me.cmbSpawnNpc.Size = New System.Drawing.Size(234, 21)
        Me.cmbSpawnNpc.TabIndex = 0
        Me.cmbSpawnNpc.Text = Nothing
        '
        'fraGiveExp
        '
        Me.fraGiveExp.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraGiveExp.Controls.Add(Me.btnGiveExpOk)
        Me.fraGiveExp.Controls.Add(Me.btnGiveExpCancel)
        Me.fraGiveExp.Controls.Add(Me.nudGiveExp)
        Me.fraGiveExp.Controls.Add(Me.DarkLabel77)
        Me.fraGiveExp.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraGiveExp.Location = New System.Drawing.Point(401, 352)
        Me.fraGiveExp.Name = "fraGiveExp"
        Me.fraGiveExp.Size = New System.Drawing.Size(248, 73)
        Me.fraGiveExp.TabIndex = 45
        Me.fraGiveExp.TabStop = False
        Me.fraGiveExp.Text = "Give Experience"
        Me.fraGiveExp.Visible = False
        '
        'btnGiveExpOk
        '
        Me.btnGiveExpOk.Location = New System.Drawing.Point(50, 45)
        Me.btnGiveExpOk.Name = "btnGiveExpOk"
        Me.btnGiveExpOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnGiveExpOk.Size = New System.Drawing.Size(75, 23)
        Me.btnGiveExpOk.TabIndex = 27
        Me.btnGiveExpOk.Text = "Ok"
        '
        'btnGiveExpCancel
        '
        Me.btnGiveExpCancel.Location = New System.Drawing.Point(131, 45)
        Me.btnGiveExpCancel.Name = "btnGiveExpCancel"
        Me.btnGiveExpCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnGiveExpCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnGiveExpCancel.TabIndex = 26
        Me.btnGiveExpCancel.Text = "Cancel"
        '
        'nudGiveExp
        '
        Me.nudGiveExp.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudGiveExp.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudGiveExp.Location = New System.Drawing.Point(77, 19)
        Me.nudGiveExp.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudGiveExp.Name = "nudGiveExp"
        Me.nudGiveExp.Size = New System.Drawing.Size(165, 20)
        Me.nudGiveExp.TabIndex = 20
        '
        'DarkLabel77
        '
        Me.DarkLabel77.AutoSize = True
        Me.DarkLabel77.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel77.Location = New System.Drawing.Point(6, 21)
        Me.DarkLabel77.Name = "DarkLabel77"
        Me.DarkLabel77.Size = New System.Drawing.Size(53, 13)
        Me.DarkLabel77.TabIndex = 0
        Me.DarkLabel77.Text = "Give Exp:"
        '
        'fraEndQuest
        '
        Me.fraEndQuest.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraEndQuest.Controls.Add(Me.btnEndQuestOk)
        Me.fraEndQuest.Controls.Add(Me.btnEndQuestCancel)
        Me.fraEndQuest.Controls.Add(Me.cmbEndQuest)
        Me.fraEndQuest.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraEndQuest.Location = New System.Drawing.Point(401, 416)
        Me.fraEndQuest.Name = "fraEndQuest"
        Me.fraEndQuest.Size = New System.Drawing.Size(248, 73)
        Me.fraEndQuest.TabIndex = 43
        Me.fraEndQuest.TabStop = False
        Me.fraEndQuest.Text = "End Quest"
        Me.fraEndQuest.Visible = False
        '
        'btnEndQuestOk
        '
        Me.btnEndQuestOk.Location = New System.Drawing.Point(46, 44)
        Me.btnEndQuestOk.Name = "btnEndQuestOk"
        Me.btnEndQuestOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnEndQuestOk.Size = New System.Drawing.Size(75, 23)
        Me.btnEndQuestOk.TabIndex = 30
        Me.btnEndQuestOk.Text = "Ok"
        '
        'btnEndQuestCancel
        '
        Me.btnEndQuestCancel.Location = New System.Drawing.Point(127, 44)
        Me.btnEndQuestCancel.Name = "btnEndQuestCancel"
        Me.btnEndQuestCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnEndQuestCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnEndQuestCancel.TabIndex = 29
        Me.btnEndQuestCancel.Text = "Cancel"
        '
        'cmbEndQuest
        '
        Me.cmbEndQuest.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbEndQuest.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbEndQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEndQuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbEndQuest.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbEndQuest.FormattingEnabled = True
        Me.cmbEndQuest.Location = New System.Drawing.Point(33, 15)
        Me.cmbEndQuest.Name = "cmbEndQuest"
        Me.cmbEndQuest.Size = New System.Drawing.Size(188, 21)
        Me.cmbEndQuest.TabIndex = 28
        Me.cmbEndQuest.Text = Nothing
        '
        'fraSetAccess
        '
        Me.fraSetAccess.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSetAccess.Controls.Add(Me.btnSetAccessOk)
        Me.fraSetAccess.Controls.Add(Me.btnSetAccessCancel)
        Me.fraSetAccess.Controls.Add(Me.cmbSetAccess)
        Me.fraSetAccess.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSetAccess.Location = New System.Drawing.Point(401, 353)
        Me.fraSetAccess.Name = "fraSetAccess"
        Me.fraSetAccess.Size = New System.Drawing.Size(248, 80)
        Me.fraSetAccess.TabIndex = 42
        Me.fraSetAccess.TabStop = False
        Me.fraSetAccess.Text = "Set Access"
        Me.fraSetAccess.Visible = False
        '
        'btnSetAccessOk
        '
        Me.btnSetAccessOk.Location = New System.Drawing.Point(46, 48)
        Me.btnSetAccessOk.Name = "btnSetAccessOk"
        Me.btnSetAccessOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSetAccessOk.Size = New System.Drawing.Size(75, 23)
        Me.btnSetAccessOk.TabIndex = 27
        Me.btnSetAccessOk.Text = "Ok"
        '
        'btnSetAccessCancel
        '
        Me.btnSetAccessCancel.Location = New System.Drawing.Point(127, 48)
        Me.btnSetAccessCancel.Name = "btnSetAccessCancel"
        Me.btnSetAccessCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSetAccessCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnSetAccessCancel.TabIndex = 26
        Me.btnSetAccessCancel.Text = "Cancel"
        '
        'cmbSetAccess
        '
        Me.cmbSetAccess.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbSetAccess.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSetAccess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSetAccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSetAccess.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbSetAccess.FormattingEnabled = True
        Me.cmbSetAccess.Items.AddRange(New Object() {"0: Player", "1: Monitor", "2: Mapper", "3: Developer", "4: Creator"})
        Me.cmbSetAccess.Location = New System.Drawing.Point(33, 19)
        Me.cmbSetAccess.Name = "cmbSetAccess"
        Me.cmbSetAccess.Size = New System.Drawing.Size(188, 21)
        Me.cmbSetAccess.TabIndex = 0
        Me.cmbSetAccess.Text = Nothing
        '
        'fraSetWait
        '
        Me.fraSetWait.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSetWait.Controls.Add(Me.btnSetWaitOk)
        Me.fraSetWait.Controls.Add(Me.btnSetWaitCancel)
        Me.fraSetWait.Controls.Add(Me.DarkLabel74)
        Me.fraSetWait.Controls.Add(Me.DarkLabel72)
        Me.fraSetWait.Controls.Add(Me.DarkLabel73)
        Me.fraSetWait.Controls.Add(Me.nudWaitAmount)
        Me.fraSetWait.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSetWait.Location = New System.Drawing.Point(401, 264)
        Me.fraSetWait.Name = "fraSetWait"
        Me.fraSetWait.Size = New System.Drawing.Size(248, 89)
        Me.fraSetWait.TabIndex = 41
        Me.fraSetWait.TabStop = False
        Me.fraSetWait.Text = "Wait..."
        Me.fraSetWait.Visible = False
        '
        'btnSetWaitOk
        '
        Me.btnSetWaitOk.Location = New System.Drawing.Point(50, 58)
        Me.btnSetWaitOk.Name = "btnSetWaitOk"
        Me.btnSetWaitOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSetWaitOk.Size = New System.Drawing.Size(75, 23)
        Me.btnSetWaitOk.TabIndex = 37
        Me.btnSetWaitOk.Text = "Ok"
        '
        'btnSetWaitCancel
        '
        Me.btnSetWaitCancel.Location = New System.Drawing.Point(131, 58)
        Me.btnSetWaitCancel.Name = "btnSetWaitCancel"
        Me.btnSetWaitCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSetWaitCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnSetWaitCancel.TabIndex = 36
        Me.btnSetWaitCancel.Text = "Cancel"
        '
        'DarkLabel74
        '
        Me.DarkLabel74.AutoSize = True
        Me.DarkLabel74.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel74.Location = New System.Drawing.Point(70, 42)
        Me.DarkLabel74.Name = "DarkLabel74"
        Me.DarkLabel74.Size = New System.Drawing.Size(113, 13)
        Me.DarkLabel74.TabIndex = 35
        Me.DarkLabel74.Text = "Hint: 1000 Ms = 1 Sec"
        '
        'DarkLabel72
        '
        Me.DarkLabel72.AutoSize = True
        Me.DarkLabel72.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel72.Location = New System.Drawing.Point(218, 23)
        Me.DarkLabel72.Name = "DarkLabel72"
        Me.DarkLabel72.Size = New System.Drawing.Size(21, 13)
        Me.DarkLabel72.TabIndex = 34
        Me.DarkLabel72.Text = "Ms"
        '
        'DarkLabel73
        '
        Me.DarkLabel73.AutoSize = True
        Me.DarkLabel73.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel73.Location = New System.Drawing.Point(15, 23)
        Me.DarkLabel73.Name = "DarkLabel73"
        Me.DarkLabel73.Size = New System.Drawing.Size(29, 13)
        Me.DarkLabel73.TabIndex = 33
        Me.DarkLabel73.Text = "Wait"
        '
        'nudWaitAmount
        '
        Me.nudWaitAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudWaitAmount.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudWaitAmount.Location = New System.Drawing.Point(50, 19)
        Me.nudWaitAmount.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudWaitAmount.Name = "nudWaitAmount"
        Me.nudWaitAmount.Size = New System.Drawing.Size(163, 20)
        Me.nudWaitAmount.TabIndex = 32
        '
        'fraShowPic
        '
        Me.fraShowPic.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraShowPic.Controls.Add(Me.btnShowPicOk)
        Me.fraShowPic.Controls.Add(Me.btnShowPicCancel)
        Me.fraShowPic.Controls.Add(Me.DarkLabel71)
        Me.fraShowPic.Controls.Add(Me.DarkLabel70)
        Me.fraShowPic.Controls.Add(Me.DarkLabel67)
        Me.fraShowPic.Controls.Add(Me.DarkLabel68)
        Me.fraShowPic.Controls.Add(Me.nudPicOffsetY)
        Me.fraShowPic.Controls.Add(Me.nudPicOffsetX)
        Me.fraShowPic.Controls.Add(Me.DarkLabel69)
        Me.fraShowPic.Controls.Add(Me.cmbPicLoc)
        Me.fraShowPic.Controls.Add(Me.nudShowPicture)
        Me.fraShowPic.Controls.Add(Me.picShowPic)
        Me.fraShowPic.Controls.Add(Me.cmbPicIndex)
        Me.fraShowPic.Controls.Add(Me.DarkLabel66)
        Me.fraShowPic.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraShowPic.Location = New System.Drawing.Point(401, 266)
        Me.fraShowPic.Name = "fraShowPic"
        Me.fraShowPic.Size = New System.Drawing.Size(248, 223)
        Me.fraShowPic.TabIndex = 40
        Me.fraShowPic.TabStop = False
        Me.fraShowPic.Text = "Show Picture"
        Me.fraShowPic.Visible = False
        '
        'btnShowPicOk
        '
        Me.btnShowPicOk.Location = New System.Drawing.Point(86, 194)
        Me.btnShowPicOk.Name = "btnShowPicOk"
        Me.btnShowPicOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnShowPicOk.Size = New System.Drawing.Size(75, 23)
        Me.btnShowPicOk.TabIndex = 55
        Me.btnShowPicOk.Text = "Ok"
        '
        'btnShowPicCancel
        '
        Me.btnShowPicCancel.Location = New System.Drawing.Point(167, 194)
        Me.btnShowPicCancel.Name = "btnShowPicCancel"
        Me.btnShowPicCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnShowPicCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnShowPicCancel.TabIndex = 54
        Me.btnShowPicCancel.Text = "Cancel"
        '
        'DarkLabel71
        '
        Me.DarkLabel71.AutoSize = True
        Me.DarkLabel71.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel71.Location = New System.Drawing.Point(10, 139)
        Me.DarkLabel71.Name = "DarkLabel71"
        Me.DarkLabel71.Size = New System.Drawing.Size(105, 13)
        Me.DarkLabel71.TabIndex = 53
        Me.DarkLabel71.Text = "Offset from Location:"
        '
        'DarkLabel70
        '
        Me.DarkLabel70.AutoSize = True
        Me.DarkLabel70.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel70.Location = New System.Drawing.Point(112, 80)
        Me.DarkLabel70.Name = "DarkLabel70"
        Me.DarkLabel70.Size = New System.Drawing.Size(48, 13)
        Me.DarkLabel70.TabIndex = 52
        Me.DarkLabel70.Text = "Location"
        '
        'DarkLabel67
        '
        Me.DarkLabel67.AutoSize = True
        Me.DarkLabel67.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel67.Location = New System.Drawing.Point(137, 162)
        Me.DarkLabel67.Name = "DarkLabel67"
        Me.DarkLabel67.Size = New System.Drawing.Size(17, 13)
        Me.DarkLabel67.TabIndex = 51
        Me.DarkLabel67.Text = "Y:"
        '
        'DarkLabel68
        '
        Me.DarkLabel68.AutoSize = True
        Me.DarkLabel68.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel68.Location = New System.Drawing.Point(10, 164)
        Me.DarkLabel68.Name = "DarkLabel68"
        Me.DarkLabel68.Size = New System.Drawing.Size(17, 13)
        Me.DarkLabel68.TabIndex = 50
        Me.DarkLabel68.Text = "X:"
        '
        'nudPicOffsetY
        '
        Me.nudPicOffsetY.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudPicOffsetY.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudPicOffsetY.Location = New System.Drawing.Point(182, 160)
        Me.nudPicOffsetY.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudPicOffsetY.Name = "nudPicOffsetY"
        Me.nudPicOffsetY.Size = New System.Drawing.Size(57, 20)
        Me.nudPicOffsetY.TabIndex = 49
        '
        'nudPicOffsetX
        '
        Me.nudPicOffsetX.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudPicOffsetX.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudPicOffsetX.Location = New System.Drawing.Point(52, 160)
        Me.nudPicOffsetX.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudPicOffsetX.Name = "nudPicOffsetX"
        Me.nudPicOffsetX.Size = New System.Drawing.Size(57, 20)
        Me.nudPicOffsetX.TabIndex = 48
        '
        'DarkLabel69
        '
        Me.DarkLabel69.AutoSize = True
        Me.DarkLabel69.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel69.Location = New System.Drawing.Point(116, 46)
        Me.DarkLabel69.Name = "DarkLabel69"
        Me.DarkLabel69.Size = New System.Drawing.Size(43, 13)
        Me.DarkLabel69.TabIndex = 47
        Me.DarkLabel69.Text = "Picture:"
        '
        'cmbPicLoc
        '
        Me.cmbPicLoc.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPicLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPicLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPicLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPicLoc.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPicLoc.FormattingEnabled = True
        Me.cmbPicLoc.Items.AddRange(New Object() {"Top Left of Screen", "Center Screen", "Centered on Player"})
        Me.cmbPicLoc.Location = New System.Drawing.Point(115, 98)
        Me.cmbPicLoc.Name = "cmbPicLoc"
        Me.cmbPicLoc.Size = New System.Drawing.Size(124, 21)
        Me.cmbPicLoc.TabIndex = 46
        Me.cmbPicLoc.Text = Nothing
        '
        'nudShowPicture
        '
        Me.nudShowPicture.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudShowPicture.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudShowPicture.Location = New System.Drawing.Point(165, 44)
        Me.nudShowPicture.Name = "nudShowPicture"
        Me.nudShowPicture.Size = New System.Drawing.Size(75, 20)
        Me.nudShowPicture.TabIndex = 45
        '
        'picShowPic
        '
        Me.picShowPic.BackColor = System.Drawing.Color.Black
        Me.picShowPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picShowPic.Location = New System.Drawing.Point(9, 43)
        Me.picShowPic.Name = "picShowPic"
        Me.picShowPic.Size = New System.Drawing.Size(100, 93)
        Me.picShowPic.TabIndex = 42
        Me.picShowPic.TabStop = False
        '
        'cmbPicIndex
        '
        Me.cmbPicIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPicIndex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPicIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPicIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPicIndex.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPicIndex.FormattingEnabled = True
        Me.cmbPicIndex.Location = New System.Drawing.Point(78, 17)
        Me.cmbPicIndex.Name = "cmbPicIndex"
        Me.cmbPicIndex.Size = New System.Drawing.Size(162, 21)
        Me.cmbPicIndex.TabIndex = 1
        Me.cmbPicIndex.Text = Nothing
        '
        'DarkLabel66
        '
        Me.DarkLabel66.AutoSize = True
        Me.DarkLabel66.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel66.Location = New System.Drawing.Point(6, 20)
        Me.DarkLabel66.Name = "DarkLabel66"
        Me.DarkLabel66.Size = New System.Drawing.Size(72, 13)
        Me.DarkLabel66.TabIndex = 0
        Me.DarkLabel66.Text = "Picture Index:"
        '
        'fraOpenShop
        '
        Me.fraOpenShop.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraOpenShop.Controls.Add(Me.btnOpenShopOk)
        Me.fraOpenShop.Controls.Add(Me.btnOpenShopCancel)
        Me.fraOpenShop.Controls.Add(Me.cmbOpenShop)
        Me.fraOpenShop.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraOpenShop.Location = New System.Drawing.Point(403, 217)
        Me.fraOpenShop.Name = "fraOpenShop"
        Me.fraOpenShop.Size = New System.Drawing.Size(246, 79)
        Me.fraOpenShop.TabIndex = 39
        Me.fraOpenShop.TabStop = False
        Me.fraOpenShop.Text = "Open Shop"
        Me.fraOpenShop.Visible = False
        '
        'btnOpenShopOk
        '
        Me.btnOpenShopOk.Location = New System.Drawing.Point(44, 47)
        Me.btnOpenShopOk.Name = "btnOpenShopOk"
        Me.btnOpenShopOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnOpenShopOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenShopOk.TabIndex = 27
        Me.btnOpenShopOk.Text = "Ok"
        '
        'btnOpenShopCancel
        '
        Me.btnOpenShopCancel.Location = New System.Drawing.Point(125, 47)
        Me.btnOpenShopCancel.Name = "btnOpenShopCancel"
        Me.btnOpenShopCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnOpenShopCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenShopCancel.TabIndex = 26
        Me.btnOpenShopCancel.Text = "Cancel"
        '
        'cmbOpenShop
        '
        Me.cmbOpenShop.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbOpenShop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbOpenShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOpenShop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbOpenShop.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbOpenShop.FormattingEnabled = True
        Me.cmbOpenShop.Location = New System.Drawing.Point(9, 20)
        Me.cmbOpenShop.Name = "cmbOpenShop"
        Me.cmbOpenShop.Size = New System.Drawing.Size(226, 21)
        Me.cmbOpenShop.TabIndex = 0
        Me.cmbOpenShop.Text = Nothing
        '
        'fraChangeLevel
        '
        Me.fraChangeLevel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeLevel.Controls.Add(Me.btnChangeLevelOk)
        Me.fraChangeLevel.Controls.Add(Me.btnChangeLevelCancel)
        Me.fraChangeLevel.Controls.Add(Me.DarkLabel65)
        Me.fraChangeLevel.Controls.Add(Me.nudChangeLevel)
        Me.fraChangeLevel.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeLevel.Location = New System.Drawing.Point(401, 293)
        Me.fraChangeLevel.Name = "fraChangeLevel"
        Me.fraChangeLevel.Size = New System.Drawing.Size(248, 72)
        Me.fraChangeLevel.TabIndex = 38
        Me.fraChangeLevel.TabStop = False
        Me.fraChangeLevel.Text = "Change Level"
        Me.fraChangeLevel.Visible = False
        '
        'btnChangeLevelOk
        '
        Me.btnChangeLevelOk.Location = New System.Drawing.Point(46, 45)
        Me.btnChangeLevelOk.Name = "btnChangeLevelOk"
        Me.btnChangeLevelOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeLevelOk.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeLevelOk.TabIndex = 27
        Me.btnChangeLevelOk.Text = "Ok"
        '
        'btnChangeLevelCancel
        '
        Me.btnChangeLevelCancel.Location = New System.Drawing.Point(127, 45)
        Me.btnChangeLevelCancel.Name = "btnChangeLevelCancel"
        Me.btnChangeLevelCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeLevelCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeLevelCancel.TabIndex = 26
        Me.btnChangeLevelCancel.Text = "Cancel"
        '
        'DarkLabel65
        '
        Me.DarkLabel65.AutoSize = True
        Me.DarkLabel65.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel65.Location = New System.Drawing.Point(7, 21)
        Me.DarkLabel65.Name = "DarkLabel65"
        Me.DarkLabel65.Size = New System.Drawing.Size(36, 13)
        Me.DarkLabel65.TabIndex = 24
        Me.DarkLabel65.Text = "Level:"
        '
        'nudChangeLevel
        '
        Me.nudChangeLevel.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudChangeLevel.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudChangeLevel.Location = New System.Drawing.Point(60, 19)
        Me.nudChangeLevel.Name = "nudChangeLevel"
        Me.nudChangeLevel.Size = New System.Drawing.Size(120, 20)
        Me.nudChangeLevel.TabIndex = 23
        '
        'fraChangeGender
        '
        Me.fraChangeGender.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeGender.Controls.Add(Me.btnChangeGenderOk)
        Me.fraChangeGender.Controls.Add(Me.btnChangeGenderCancel)
        Me.fraChangeGender.Controls.Add(Me.optChangeSexFemale)
        Me.fraChangeGender.Controls.Add(Me.optChangeSexMale)
        Me.fraChangeGender.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeGender.Location = New System.Drawing.Point(401, 364)
        Me.fraChangeGender.Name = "fraChangeGender"
        Me.fraChangeGender.Size = New System.Drawing.Size(248, 72)
        Me.fraChangeGender.TabIndex = 37
        Me.fraChangeGender.TabStop = False
        Me.fraChangeGender.Text = "Change Player Gender"
        Me.fraChangeGender.Visible = False
        '
        'btnChangeGenderOk
        '
        Me.btnChangeGenderOk.Location = New System.Drawing.Point(39, 42)
        Me.btnChangeGenderOk.Name = "btnChangeGenderOk"
        Me.btnChangeGenderOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeGenderOk.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeGenderOk.TabIndex = 27
        Me.btnChangeGenderOk.Text = "Ok"
        '
        'btnChangeGenderCancel
        '
        Me.btnChangeGenderCancel.Location = New System.Drawing.Point(120, 42)
        Me.btnChangeGenderCancel.Name = "btnChangeGenderCancel"
        Me.btnChangeGenderCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeGenderCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeGenderCancel.TabIndex = 26
        Me.btnChangeGenderCancel.Text = "Cancel"
        '
        'optChangeSexFemale
        '
        Me.optChangeSexFemale.AutoSize = True
        Me.optChangeSexFemale.Location = New System.Drawing.Point(141, 19)
        Me.optChangeSexFemale.Name = "optChangeSexFemale"
        Me.optChangeSexFemale.Size = New System.Drawing.Size(59, 17)
        Me.optChangeSexFemale.TabIndex = 1
        Me.optChangeSexFemale.TabStop = True
        Me.optChangeSexFemale.Text = "Female"
        '
        'optChangeSexMale
        '
        Me.optChangeSexMale.AutoSize = True
        Me.optChangeSexMale.Location = New System.Drawing.Point(52, 19)
        Me.optChangeSexMale.Name = "optChangeSexMale"
        Me.optChangeSexMale.Size = New System.Drawing.Size(48, 17)
        Me.optChangeSexMale.TabIndex = 0
        Me.optChangeSexMale.TabStop = True
        Me.optChangeSexMale.Text = "Male"
        '
        'fraGoToLabel
        '
        Me.fraGoToLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraGoToLabel.Controls.Add(Me.btnGoToLabelOk)
        Me.fraGoToLabel.Controls.Add(Me.btnGoToLabelCancel)
        Me.fraGoToLabel.Controls.Add(Me.txtGotoLabel)
        Me.fraGoToLabel.Controls.Add(Me.DarkLabel60)
        Me.fraGoToLabel.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraGoToLabel.Location = New System.Drawing.Point(401, 255)
        Me.fraGoToLabel.Name = "fraGoToLabel"
        Me.fraGoToLabel.Size = New System.Drawing.Size(248, 73)
        Me.fraGoToLabel.TabIndex = 35
        Me.fraGoToLabel.TabStop = False
        Me.fraGoToLabel.Text = "GoTo Label"
        Me.fraGoToLabel.Visible = False
        '
        'btnGoToLabelOk
        '
        Me.btnGoToLabelOk.Location = New System.Drawing.Point(86, 44)
        Me.btnGoToLabelOk.Name = "btnGoToLabelOk"
        Me.btnGoToLabelOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnGoToLabelOk.Size = New System.Drawing.Size(75, 23)
        Me.btnGoToLabelOk.TabIndex = 27
        Me.btnGoToLabelOk.Text = "Ok"
        '
        'btnGoToLabelCancel
        '
        Me.btnGoToLabelCancel.Location = New System.Drawing.Point(167, 44)
        Me.btnGoToLabelCancel.Name = "btnGoToLabelCancel"
        Me.btnGoToLabelCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnGoToLabelCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnGoToLabelCancel.TabIndex = 26
        Me.btnGoToLabelCancel.Text = "Cancel"
        '
        'txtGotoLabel
        '
        Me.txtGotoLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtGotoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGotoLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtGotoLabel.Location = New System.Drawing.Point(78, 18)
        Me.txtGotoLabel.Name = "txtGotoLabel"
        Me.txtGotoLabel.Size = New System.Drawing.Size(164, 20)
        Me.txtGotoLabel.TabIndex = 1
        '
        'DarkLabel60
        '
        Me.DarkLabel60.AutoSize = True
        Me.DarkLabel60.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel60.Location = New System.Drawing.Point(3, 20)
        Me.DarkLabel60.Name = "DarkLabel60"
        Me.DarkLabel60.Size = New System.Drawing.Size(67, 13)
        Me.DarkLabel60.TabIndex = 0
        Me.DarkLabel60.Text = "Label Name:"
        '
        'fraHidePic
        '
        Me.fraHidePic.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraHidePic.Controls.Add(Me.btnHidePicOk)
        Me.fraHidePic.Controls.Add(Me.btnHidePicCancel)
        Me.fraHidePic.Controls.Add(Me.nudHidePic)
        Me.fraHidePic.Controls.Add(Me.DarkLabel59)
        Me.fraHidePic.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraHidePic.Location = New System.Drawing.Point(401, 184)
        Me.fraHidePic.Name = "fraHidePic"
        Me.fraHidePic.Size = New System.Drawing.Size(248, 71)
        Me.fraHidePic.TabIndex = 34
        Me.fraHidePic.TabStop = False
        Me.fraHidePic.Text = "Hide Picture"
        Me.fraHidePic.Visible = False
        '
        'btnHidePicOk
        '
        Me.btnHidePicOk.Location = New System.Drawing.Point(86, 40)
        Me.btnHidePicOk.Name = "btnHidePicOk"
        Me.btnHidePicOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnHidePicOk.Size = New System.Drawing.Size(75, 23)
        Me.btnHidePicOk.TabIndex = 27
        Me.btnHidePicOk.Text = "Ok"
        '
        'btnHidePicCancel
        '
        Me.btnHidePicCancel.Location = New System.Drawing.Point(167, 40)
        Me.btnHidePicCancel.Name = "btnHidePicCancel"
        Me.btnHidePicCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnHidePicCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnHidePicCancel.TabIndex = 26
        Me.btnHidePicCancel.Text = "Cancel"
        '
        'nudHidePic
        '
        Me.nudHidePic.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudHidePic.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudHidePic.Location = New System.Drawing.Point(84, 14)
        Me.nudHidePic.Name = "nudHidePic"
        Me.nudHidePic.Size = New System.Drawing.Size(158, 20)
        Me.nudHidePic.TabIndex = 1
        '
        'DarkLabel59
        '
        Me.DarkLabel59.AutoSize = True
        Me.DarkLabel59.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel59.Location = New System.Drawing.Point(6, 16)
        Me.DarkLabel59.Name = "DarkLabel59"
        Me.DarkLabel59.Size = New System.Drawing.Size(72, 13)
        Me.DarkLabel59.TabIndex = 0
        Me.DarkLabel59.Text = "Picture Index:"
        '
        'fraBeginQuest
        '
        Me.fraBeginQuest.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraBeginQuest.Controls.Add(Me.btnBeginQuestOk)
        Me.fraBeginQuest.Controls.Add(Me.btnBeginQuestCancel)
        Me.fraBeginQuest.Controls.Add(Me.cmbBeginQuest)
        Me.fraBeginQuest.Controls.Add(Me.DarkLabel58)
        Me.fraBeginQuest.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraBeginQuest.Location = New System.Drawing.Point(401, 105)
        Me.fraBeginQuest.Name = "fraBeginQuest"
        Me.fraBeginQuest.Size = New System.Drawing.Size(248, 79)
        Me.fraBeginQuest.TabIndex = 33
        Me.fraBeginQuest.TabStop = False
        Me.fraBeginQuest.Text = "Begin Quest"
        Me.fraBeginQuest.Visible = False
        '
        'btnBeginQuestOk
        '
        Me.btnBeginQuestOk.Location = New System.Drawing.Point(86, 47)
        Me.btnBeginQuestOk.Name = "btnBeginQuestOk"
        Me.btnBeginQuestOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnBeginQuestOk.Size = New System.Drawing.Size(75, 23)
        Me.btnBeginQuestOk.TabIndex = 27
        Me.btnBeginQuestOk.Text = "Ok"
        '
        'btnBeginQuestCancel
        '
        Me.btnBeginQuestCancel.Location = New System.Drawing.Point(167, 47)
        Me.btnBeginQuestCancel.Name = "btnBeginQuestCancel"
        Me.btnBeginQuestCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnBeginQuestCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnBeginQuestCancel.TabIndex = 26
        Me.btnBeginQuestCancel.Text = "Cancel"
        '
        'cmbBeginQuest
        '
        Me.cmbBeginQuest.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbBeginQuest.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbBeginQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBeginQuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbBeginQuest.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbBeginQuest.FormattingEnabled = True
        Me.cmbBeginQuest.Location = New System.Drawing.Point(50, 20)
        Me.cmbBeginQuest.Name = "cmbBeginQuest"
        Me.cmbBeginQuest.Size = New System.Drawing.Size(190, 21)
        Me.cmbBeginQuest.TabIndex = 1
        Me.cmbBeginQuest.Text = Nothing
        '
        'DarkLabel58
        '
        Me.DarkLabel58.AutoSize = True
        Me.DarkLabel58.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel58.Location = New System.Drawing.Point(6, 23)
        Me.DarkLabel58.Name = "DarkLabel58"
        Me.DarkLabel58.Size = New System.Drawing.Size(38, 13)
        Me.DarkLabel58.TabIndex = 0
        Me.DarkLabel58.Text = "Quest:"
        '
        'fraShowChoices
        '
        Me.fraShowChoices.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraShowChoices.Controls.Add(Me.txtChoices4)
        Me.fraShowChoices.Controls.Add(Me.txtChoices3)
        Me.fraShowChoices.Controls.Add(Me.txtChoices2)
        Me.fraShowChoices.Controls.Add(Me.txtChoices1)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel56)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel57)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel55)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel54)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel52)
        Me.fraShowChoices.Controls.Add(Me.txtChoicePrompt)
        Me.fraShowChoices.Controls.Add(Me.btnShowChoicesOk)
        Me.fraShowChoices.Controls.Add(Me.picShowChoicesFace)
        Me.fraShowChoices.Controls.Add(Me.btnShowChoicesCancel)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel53)
        Me.fraShowChoices.Controls.Add(Me.nudShowChoicesFace)
        Me.fraShowChoices.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraShowChoices.Location = New System.Drawing.Point(401, 103)
        Me.fraShowChoices.Name = "fraShowChoices"
        Me.fraShowChoices.Size = New System.Drawing.Size(248, 333)
        Me.fraShowChoices.TabIndex = 32
        Me.fraShowChoices.TabStop = False
        Me.fraShowChoices.Text = "Show Choices"
        Me.fraShowChoices.Visible = False
        '
        'txtChoices4
        '
        Me.txtChoices4.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChoices4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChoices4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChoices4.Location = New System.Drawing.Point(141, 174)
        Me.txtChoices4.Name = "txtChoices4"
        Me.txtChoices4.Size = New System.Drawing.Size(100, 20)
        Me.txtChoices4.TabIndex = 34
        '
        'txtChoices3
        '
        Me.txtChoices3.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChoices3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChoices3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChoices3.Location = New System.Drawing.Point(6, 173)
        Me.txtChoices3.Name = "txtChoices3"
        Me.txtChoices3.Size = New System.Drawing.Size(100, 20)
        Me.txtChoices3.TabIndex = 33
        '
        'txtChoices2
        '
        Me.txtChoices2.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChoices2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChoices2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChoices2.Location = New System.Drawing.Point(141, 134)
        Me.txtChoices2.Name = "txtChoices2"
        Me.txtChoices2.Size = New System.Drawing.Size(100, 20)
        Me.txtChoices2.TabIndex = 32
        '
        'txtChoices1
        '
        Me.txtChoices1.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChoices1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChoices1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChoices1.Location = New System.Drawing.Point(6, 134)
        Me.txtChoices1.Name = "txtChoices1"
        Me.txtChoices1.Size = New System.Drawing.Size(100, 20)
        Me.txtChoices1.TabIndex = 31
        '
        'DarkLabel56
        '
        Me.DarkLabel56.AutoSize = True
        Me.DarkLabel56.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel56.Location = New System.Drawing.Point(138, 157)
        Me.DarkLabel56.Name = "DarkLabel56"
        Me.DarkLabel56.Size = New System.Drawing.Size(49, 13)
        Me.DarkLabel56.TabIndex = 30
        Me.DarkLabel56.Text = "Choice 4"
        '
        'DarkLabel57
        '
        Me.DarkLabel57.AutoSize = True
        Me.DarkLabel57.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel57.Location = New System.Drawing.Point(7, 157)
        Me.DarkLabel57.Name = "DarkLabel57"
        Me.DarkLabel57.Size = New System.Drawing.Size(49, 13)
        Me.DarkLabel57.TabIndex = 29
        Me.DarkLabel57.Text = "Choice 3"
        '
        'DarkLabel55
        '
        Me.DarkLabel55.AutoSize = True
        Me.DarkLabel55.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel55.Location = New System.Drawing.Point(138, 118)
        Me.DarkLabel55.Name = "DarkLabel55"
        Me.DarkLabel55.Size = New System.Drawing.Size(49, 13)
        Me.DarkLabel55.TabIndex = 28
        Me.DarkLabel55.Text = "Choice 2"
        '
        'DarkLabel54
        '
        Me.DarkLabel54.AutoSize = True
        Me.DarkLabel54.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel54.Location = New System.Drawing.Point(6, 118)
        Me.DarkLabel54.Name = "DarkLabel54"
        Me.DarkLabel54.Size = New System.Drawing.Size(49, 13)
        Me.DarkLabel54.TabIndex = 27
        Me.DarkLabel54.Text = "Choice 1"
        '
        'DarkLabel52
        '
        Me.DarkLabel52.AutoSize = True
        Me.DarkLabel52.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel52.Location = New System.Drawing.Point(7, 19)
        Me.DarkLabel52.Name = "DarkLabel52"
        Me.DarkLabel52.Size = New System.Drawing.Size(40, 13)
        Me.DarkLabel52.TabIndex = 26
        Me.DarkLabel52.Text = "Prompt"
        '
        'txtChoicePrompt
        '
        Me.txtChoicePrompt.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChoicePrompt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChoicePrompt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChoicePrompt.Location = New System.Drawing.Point(9, 38)
        Me.txtChoicePrompt.Multiline = True
        Me.txtChoicePrompt.Name = "txtChoicePrompt"
        Me.txtChoicePrompt.Size = New System.Drawing.Size(228, 77)
        Me.txtChoicePrompt.TabIndex = 21
        '
        'btnShowChoicesOk
        '
        Me.btnShowChoicesOk.Location = New System.Drawing.Point(84, 305)
        Me.btnShowChoicesOk.Name = "btnShowChoicesOk"
        Me.btnShowChoicesOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnShowChoicesOk.Size = New System.Drawing.Size(75, 23)
        Me.btnShowChoicesOk.TabIndex = 25
        Me.btnShowChoicesOk.Text = "Ok"
        '
        'picShowChoicesFace
        '
        Me.picShowChoicesFace.BackColor = System.Drawing.Color.Black
        Me.picShowChoicesFace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picShowChoicesFace.Location = New System.Drawing.Point(6, 199)
        Me.picShowChoicesFace.Name = "picShowChoicesFace"
        Me.picShowChoicesFace.Size = New System.Drawing.Size(100, 93)
        Me.picShowChoicesFace.TabIndex = 2
        Me.picShowChoicesFace.TabStop = False
        '
        'btnShowChoicesCancel
        '
        Me.btnShowChoicesCancel.Location = New System.Drawing.Point(165, 305)
        Me.btnShowChoicesCancel.Name = "btnShowChoicesCancel"
        Me.btnShowChoicesCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnShowChoicesCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnShowChoicesCancel.TabIndex = 24
        Me.btnShowChoicesCancel.Text = "Cancel"
        '
        'DarkLabel53
        '
        Me.DarkLabel53.AutoSize = True
        Me.DarkLabel53.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel53.Location = New System.Drawing.Point(109, 274)
        Me.DarkLabel53.Name = "DarkLabel53"
        Me.DarkLabel53.Size = New System.Drawing.Size(34, 13)
        Me.DarkLabel53.TabIndex = 22
        Me.DarkLabel53.Text = "Face:"
        '
        'nudShowChoicesFace
        '
        Me.nudShowChoicesFace.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudShowChoicesFace.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudShowChoicesFace.Location = New System.Drawing.Point(146, 272)
        Me.nudShowChoicesFace.Name = "nudShowChoicesFace"
        Me.nudShowChoicesFace.Size = New System.Drawing.Size(92, 20)
        Me.nudShowChoicesFace.TabIndex = 23
        '
        'fraPlayerVariable
        '
        Me.fraPlayerVariable.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlayerVariable.Controls.Add(Me.nudVariableData2)
        Me.fraPlayerVariable.Controls.Add(Me.optVariableAction2)
        Me.fraPlayerVariable.Controls.Add(Me.btnPlayerVarOk)
        Me.fraPlayerVariable.Controls.Add(Me.btnPlayerVarCancel)
        Me.fraPlayerVariable.Controls.Add(Me.DarkLabel51)
        Me.fraPlayerVariable.Controls.Add(Me.DarkLabel50)
        Me.fraPlayerVariable.Controls.Add(Me.nudVariableData4)
        Me.fraPlayerVariable.Controls.Add(Me.nudVariableData3)
        Me.fraPlayerVariable.Controls.Add(Me.optVariableAction3)
        Me.fraPlayerVariable.Controls.Add(Me.optVariableAction1)
        Me.fraPlayerVariable.Controls.Add(Me.nudVariableData1)
        Me.fraPlayerVariable.Controls.Add(Me.nudVariableData0)
        Me.fraPlayerVariable.Controls.Add(Me.optVariableAction0)
        Me.fraPlayerVariable.Controls.Add(Me.cmbVariable)
        Me.fraPlayerVariable.Controls.Add(Me.DarkLabel49)
        Me.fraPlayerVariable.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlayerVariable.Location = New System.Drawing.Point(401, 282)
        Me.fraPlayerVariable.Name = "fraPlayerVariable"
        Me.fraPlayerVariable.Size = New System.Drawing.Size(246, 154)
        Me.fraPlayerVariable.TabIndex = 31
        Me.fraPlayerVariable.TabStop = False
        Me.fraPlayerVariable.Text = "Player Variable"
        Me.fraPlayerVariable.Visible = False
        '
        'nudVariableData2
        '
        Me.nudVariableData2.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudVariableData2.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudVariableData2.Location = New System.Drawing.Point(120, 72)
        Me.nudVariableData2.Name = "nudVariableData2"
        Me.nudVariableData2.Size = New System.Drawing.Size(120, 20)
        Me.nudVariableData2.TabIndex = 29
        '
        'optVariableAction2
        '
        Me.optVariableAction2.AutoSize = True
        Me.optVariableAction2.Location = New System.Drawing.Point(6, 72)
        Me.optVariableAction2.Name = "optVariableAction2"
        Me.optVariableAction2.Size = New System.Drawing.Size(65, 17)
        Me.optVariableAction2.TabIndex = 28
        Me.optVariableAction2.TabStop = True
        Me.optVariableAction2.Text = "Subtract"
        '
        'btnPlayerVarOk
        '
        Me.btnPlayerVarOk.Location = New System.Drawing.Point(84, 124)
        Me.btnPlayerVarOk.Name = "btnPlayerVarOk"
        Me.btnPlayerVarOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPlayerVarOk.Size = New System.Drawing.Size(75, 23)
        Me.btnPlayerVarOk.TabIndex = 27
        Me.btnPlayerVarOk.Text = "Ok"
        '
        'btnPlayerVarCancel
        '
        Me.btnPlayerVarCancel.Location = New System.Drawing.Point(165, 124)
        Me.btnPlayerVarCancel.Name = "btnPlayerVarCancel"
        Me.btnPlayerVarCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPlayerVarCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnPlayerVarCancel.TabIndex = 26
        Me.btnPlayerVarCancel.Text = "Cancel"
        '
        'DarkLabel51
        '
        Me.DarkLabel51.AutoSize = True
        Me.DarkLabel51.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel51.Location = New System.Drawing.Point(75, 100)
        Me.DarkLabel51.Name = "DarkLabel51"
        Me.DarkLabel51.Size = New System.Drawing.Size(30, 13)
        Me.DarkLabel51.TabIndex = 16
        Me.DarkLabel51.Text = "Low:"
        '
        'DarkLabel50
        '
        Me.DarkLabel50.AutoSize = True
        Me.DarkLabel50.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel50.Location = New System.Drawing.Point(158, 100)
        Me.DarkLabel50.Name = "DarkLabel50"
        Me.DarkLabel50.Size = New System.Drawing.Size(32, 13)
        Me.DarkLabel50.TabIndex = 15
        Me.DarkLabel50.Text = "High:"
        '
        'nudVariableData4
        '
        Me.nudVariableData4.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudVariableData4.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudVariableData4.Location = New System.Drawing.Point(196, 98)
        Me.nudVariableData4.Name = "nudVariableData4"
        Me.nudVariableData4.Size = New System.Drawing.Size(44, 20)
        Me.nudVariableData4.TabIndex = 14
        '
        'nudVariableData3
        '
        Me.nudVariableData3.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudVariableData3.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudVariableData3.Location = New System.Drawing.Point(111, 98)
        Me.nudVariableData3.Name = "nudVariableData3"
        Me.nudVariableData3.Size = New System.Drawing.Size(44, 20)
        Me.nudVariableData3.TabIndex = 13
        '
        'optVariableAction3
        '
        Me.optVariableAction3.AutoSize = True
        Me.optVariableAction3.Location = New System.Drawing.Point(6, 98)
        Me.optVariableAction3.Name = "optVariableAction3"
        Me.optVariableAction3.Size = New System.Drawing.Size(65, 17)
        Me.optVariableAction3.TabIndex = 12
        Me.optVariableAction3.TabStop = True
        Me.optVariableAction3.Text = "Random"
        '
        'optVariableAction1
        '
        Me.optVariableAction1.AutoSize = True
        Me.optVariableAction1.Location = New System.Drawing.Point(146, 46)
        Me.optVariableAction1.Name = "optVariableAction1"
        Me.optVariableAction1.Size = New System.Drawing.Size(44, 17)
        Me.optVariableAction1.TabIndex = 11
        Me.optVariableAction1.TabStop = True
        Me.optVariableAction1.Text = "Add"
        '
        'nudVariableData1
        '
        Me.nudVariableData1.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudVariableData1.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudVariableData1.Location = New System.Drawing.Point(196, 46)
        Me.nudVariableData1.Name = "nudVariableData1"
        Me.nudVariableData1.Size = New System.Drawing.Size(44, 20)
        Me.nudVariableData1.TabIndex = 10
        '
        'nudVariableData0
        '
        Me.nudVariableData0.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudVariableData0.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudVariableData0.Location = New System.Drawing.Point(62, 46)
        Me.nudVariableData0.Name = "nudVariableData0"
        Me.nudVariableData0.Size = New System.Drawing.Size(44, 20)
        Me.nudVariableData0.TabIndex = 9
        '
        'optVariableAction0
        '
        Me.optVariableAction0.AutoSize = True
        Me.optVariableAction0.Location = New System.Drawing.Point(6, 46)
        Me.optVariableAction0.Name = "optVariableAction0"
        Me.optVariableAction0.Size = New System.Drawing.Size(41, 17)
        Me.optVariableAction0.TabIndex = 2
        Me.optVariableAction0.TabStop = True
        Me.optVariableAction0.Text = "Set"
        '
        'cmbVariable
        '
        Me.cmbVariable.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbVariable.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbVariable.FormattingEnabled = True
        Me.cmbVariable.Location = New System.Drawing.Point(60, 19)
        Me.cmbVariable.Name = "cmbVariable"
        Me.cmbVariable.Size = New System.Drawing.Size(179, 21)
        Me.cmbVariable.TabIndex = 1
        Me.cmbVariable.Text = Nothing
        '
        'DarkLabel49
        '
        Me.DarkLabel49.AutoSize = True
        Me.DarkLabel49.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel49.Location = New System.Drawing.Point(6, 22)
        Me.DarkLabel49.Name = "DarkLabel49"
        Me.DarkLabel49.Size = New System.Drawing.Size(48, 13)
        Me.DarkLabel49.TabIndex = 0
        Me.DarkLabel49.Text = "Variable:"
        '
        'fraChangeSprite
        '
        Me.fraChangeSprite.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeSprite.Controls.Add(Me.btnChangeSpriteOk)
        Me.fraChangeSprite.Controls.Add(Me.btnChangeSpriteCancel)
        Me.fraChangeSprite.Controls.Add(Me.DarkLabel48)
        Me.fraChangeSprite.Controls.Add(Me.nudChangeSprite)
        Me.fraChangeSprite.Controls.Add(Me.picChangeSprite)
        Me.fraChangeSprite.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeSprite.Location = New System.Drawing.Point(401, 280)
        Me.fraChangeSprite.Name = "fraChangeSprite"
        Me.fraChangeSprite.Size = New System.Drawing.Size(246, 117)
        Me.fraChangeSprite.TabIndex = 30
        Me.fraChangeSprite.TabStop = False
        Me.fraChangeSprite.Text = "Change Sprite"
        Me.fraChangeSprite.Visible = False
        '
        'btnChangeSpriteOk
        '
        Me.btnChangeSpriteOk.Location = New System.Drawing.Point(84, 89)
        Me.btnChangeSpriteOk.Name = "btnChangeSpriteOk"
        Me.btnChangeSpriteOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeSpriteOk.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeSpriteOk.TabIndex = 30
        Me.btnChangeSpriteOk.Text = "Ok"
        '
        'btnChangeSpriteCancel
        '
        Me.btnChangeSpriteCancel.Location = New System.Drawing.Point(165, 89)
        Me.btnChangeSpriteCancel.Name = "btnChangeSpriteCancel"
        Me.btnChangeSpriteCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeSpriteCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeSpriteCancel.TabIndex = 29
        Me.btnChangeSpriteCancel.Text = "Cancel"
        '
        'DarkLabel48
        '
        Me.DarkLabel48.AutoSize = True
        Me.DarkLabel48.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel48.Location = New System.Drawing.Point(80, 67)
        Me.DarkLabel48.Name = "DarkLabel48"
        Me.DarkLabel48.Size = New System.Drawing.Size(34, 13)
        Me.DarkLabel48.TabIndex = 28
        Me.DarkLabel48.Text = "Sprite"
        '
        'nudChangeSprite
        '
        Me.nudChangeSprite.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudChangeSprite.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudChangeSprite.Location = New System.Drawing.Point(120, 63)
        Me.nudChangeSprite.Name = "nudChangeSprite"
        Me.nudChangeSprite.Size = New System.Drawing.Size(120, 20)
        Me.nudChangeSprite.TabIndex = 27
        '
        'picChangeSprite
        '
        Me.picChangeSprite.BackColor = System.Drawing.Color.Black
        Me.picChangeSprite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picChangeSprite.Location = New System.Drawing.Point(6, 19)
        Me.picChangeSprite.Name = "picChangeSprite"
        Me.picChangeSprite.Size = New System.Drawing.Size(70, 93)
        Me.picChangeSprite.TabIndex = 3
        Me.picChangeSprite.TabStop = False
        '
        'fraSetSelfSwitch
        '
        Me.fraSetSelfSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSetSelfSwitch.Controls.Add(Me.btnSelfswitchOk)
        Me.fraSetSelfSwitch.Controls.Add(Me.btnSelfswitchCancel)
        Me.fraSetSelfSwitch.Controls.Add(Me.DarkLabel47)
        Me.fraSetSelfSwitch.Controls.Add(Me.cmbSetSelfSwitchTo)
        Me.fraSetSelfSwitch.Controls.Add(Me.DarkLabel46)
        Me.fraSetSelfSwitch.Controls.Add(Me.cmbSetSelfSwitch)
        Me.fraSetSelfSwitch.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSetSelfSwitch.Location = New System.Drawing.Point(401, 180)
        Me.fraSetSelfSwitch.Name = "fraSetSelfSwitch"
        Me.fraSetSelfSwitch.Size = New System.Drawing.Size(246, 100)
        Me.fraSetSelfSwitch.TabIndex = 29
        Me.fraSetSelfSwitch.TabStop = False
        Me.fraSetSelfSwitch.Text = "Self Switches"
        Me.fraSetSelfSwitch.Visible = False
        '
        'btnSelfswitchOk
        '
        Me.btnSelfswitchOk.Location = New System.Drawing.Point(84, 73)
        Me.btnSelfswitchOk.Name = "btnSelfswitchOk"
        Me.btnSelfswitchOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSelfswitchOk.Size = New System.Drawing.Size(75, 23)
        Me.btnSelfswitchOk.TabIndex = 27
        Me.btnSelfswitchOk.Text = "Ok"
        '
        'btnSelfswitchCancel
        '
        Me.btnSelfswitchCancel.Location = New System.Drawing.Point(165, 73)
        Me.btnSelfswitchCancel.Name = "btnSelfswitchCancel"
        Me.btnSelfswitchCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSelfswitchCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnSelfswitchCancel.TabIndex = 26
        Me.btnSelfswitchCancel.Text = "Cancel"
        '
        'DarkLabel47
        '
        Me.DarkLabel47.AutoSize = True
        Me.DarkLabel47.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel47.Location = New System.Drawing.Point(6, 49)
        Me.DarkLabel47.Name = "DarkLabel47"
        Me.DarkLabel47.Size = New System.Drawing.Size(39, 13)
        Me.DarkLabel47.TabIndex = 3
        Me.DarkLabel47.Text = "Set To"
        '
        'cmbSetSelfSwitchTo
        '
        Me.cmbSetSelfSwitchTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbSetSelfSwitchTo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSetSelfSwitchTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSetSelfSwitchTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSetSelfSwitchTo.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbSetSelfSwitchTo.FormattingEnabled = True
        Me.cmbSetSelfSwitchTo.Items.AddRange(New Object() {"Off", "On"})
        Me.cmbSetSelfSwitchTo.Location = New System.Drawing.Point(72, 46)
        Me.cmbSetSelfSwitchTo.Name = "cmbSetSelfSwitchTo"
        Me.cmbSetSelfSwitchTo.Size = New System.Drawing.Size(168, 21)
        Me.cmbSetSelfSwitchTo.TabIndex = 2
        Me.cmbSetSelfSwitchTo.Text = Nothing
        '
        'DarkLabel46
        '
        Me.DarkLabel46.AutoSize = True
        Me.DarkLabel46.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel46.Location = New System.Drawing.Point(6, 22)
        Me.DarkLabel46.Name = "DarkLabel46"
        Me.DarkLabel46.Size = New System.Drawing.Size(63, 13)
        Me.DarkLabel46.TabIndex = 1
        Me.DarkLabel46.Text = "Self Switch:"
        '
        'cmbSetSelfSwitch
        '
        Me.cmbSetSelfSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbSetSelfSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSetSelfSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSetSelfSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSetSelfSwitch.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbSetSelfSwitch.FormattingEnabled = True
        Me.cmbSetSelfSwitch.Location = New System.Drawing.Point(72, 19)
        Me.cmbSetSelfSwitch.Name = "cmbSetSelfSwitch"
        Me.cmbSetSelfSwitch.Size = New System.Drawing.Size(168, 21)
        Me.cmbSetSelfSwitch.TabIndex = 0
        Me.cmbSetSelfSwitch.Text = Nothing
        '
        'fraMapTint
        '
        Me.fraMapTint.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraMapTint.Controls.Add(Me.btnMapTintOk)
        Me.fraMapTint.Controls.Add(Me.btnMapTintCancel)
        Me.fraMapTint.Controls.Add(Me.DarkLabel42)
        Me.fraMapTint.Controls.Add(Me.nudMapTintData3)
        Me.fraMapTint.Controls.Add(Me.nudMapTintData2)
        Me.fraMapTint.Controls.Add(Me.DarkLabel43)
        Me.fraMapTint.Controls.Add(Me.DarkLabel44)
        Me.fraMapTint.Controls.Add(Me.nudMapTintData1)
        Me.fraMapTint.Controls.Add(Me.nudMapTintData0)
        Me.fraMapTint.Controls.Add(Me.DarkLabel45)
        Me.fraMapTint.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraMapTint.Location = New System.Drawing.Point(401, 181)
        Me.fraMapTint.Name = "fraMapTint"
        Me.fraMapTint.Size = New System.Drawing.Size(246, 145)
        Me.fraMapTint.TabIndex = 28
        Me.fraMapTint.TabStop = False
        Me.fraMapTint.Text = "Map Tinting"
        Me.fraMapTint.Visible = False
        '
        'btnMapTintOk
        '
        Me.btnMapTintOk.Location = New System.Drawing.Point(84, 115)
        Me.btnMapTintOk.Name = "btnMapTintOk"
        Me.btnMapTintOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnMapTintOk.Size = New System.Drawing.Size(75, 23)
        Me.btnMapTintOk.TabIndex = 45
        Me.btnMapTintOk.Text = "Ok"
        '
        'btnMapTintCancel
        '
        Me.btnMapTintCancel.Location = New System.Drawing.Point(165, 115)
        Me.btnMapTintCancel.Name = "btnMapTintCancel"
        Me.btnMapTintCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnMapTintCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnMapTintCancel.TabIndex = 44
        Me.btnMapTintCancel.Text = "Cancel"
        '
        'DarkLabel42
        '
        Me.DarkLabel42.AutoSize = True
        Me.DarkLabel42.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel42.Location = New System.Drawing.Point(5, 93)
        Me.DarkLabel42.Name = "DarkLabel42"
        Me.DarkLabel42.Size = New System.Drawing.Size(46, 13)
        Me.DarkLabel42.TabIndex = 43
        Me.DarkLabel42.Text = "Opacity:"
        '
        'nudMapTintData3
        '
        Me.nudMapTintData3.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudMapTintData3.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudMapTintData3.Location = New System.Drawing.Point(95, 89)
        Me.nudMapTintData3.Name = "nudMapTintData3"
        Me.nudMapTintData3.Size = New System.Drawing.Size(144, 20)
        Me.nudMapTintData3.TabIndex = 42
        '
        'nudMapTintData2
        '
        Me.nudMapTintData2.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudMapTintData2.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudMapTintData2.Location = New System.Drawing.Point(95, 64)
        Me.nudMapTintData2.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudMapTintData2.Name = "nudMapTintData2"
        Me.nudMapTintData2.Size = New System.Drawing.Size(144, 20)
        Me.nudMapTintData2.TabIndex = 41
        '
        'DarkLabel43
        '
        Me.DarkLabel43.AutoSize = True
        Me.DarkLabel43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel43.Location = New System.Drawing.Point(5, 66)
        Me.DarkLabel43.Name = "DarkLabel43"
        Me.DarkLabel43.Size = New System.Drawing.Size(31, 13)
        Me.DarkLabel43.TabIndex = 40
        Me.DarkLabel43.Text = "Blue:"
        '
        'DarkLabel44
        '
        Me.DarkLabel44.AutoSize = True
        Me.DarkLabel44.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel44.Location = New System.Drawing.Point(4, 43)
        Me.DarkLabel44.Name = "DarkLabel44"
        Me.DarkLabel44.Size = New System.Drawing.Size(39, 13)
        Me.DarkLabel44.TabIndex = 39
        Me.DarkLabel44.Text = "Green:"
        '
        'nudMapTintData1
        '
        Me.nudMapTintData1.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudMapTintData1.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudMapTintData1.Location = New System.Drawing.Point(95, 39)
        Me.nudMapTintData1.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudMapTintData1.Name = "nudMapTintData1"
        Me.nudMapTintData1.Size = New System.Drawing.Size(144, 20)
        Me.nudMapTintData1.TabIndex = 38
        '
        'nudMapTintData0
        '
        Me.nudMapTintData0.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudMapTintData0.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudMapTintData0.Location = New System.Drawing.Point(95, 14)
        Me.nudMapTintData0.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudMapTintData0.Name = "nudMapTintData0"
        Me.nudMapTintData0.Size = New System.Drawing.Size(144, 20)
        Me.nudMapTintData0.TabIndex = 37
        '
        'DarkLabel45
        '
        Me.DarkLabel45.AutoSize = True
        Me.DarkLabel45.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel45.Location = New System.Drawing.Point(5, 16)
        Me.DarkLabel45.Name = "DarkLabel45"
        Me.DarkLabel45.Size = New System.Drawing.Size(30, 13)
        Me.DarkLabel45.TabIndex = 36
        Me.DarkLabel45.Text = "Red:"
        '
        'fraShowChatBubble
        '
        Me.fraShowChatBubble.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraShowChatBubble.Controls.Add(Me.btnShowChatBubbleOk)
        Me.fraShowChatBubble.Controls.Add(Me.btnShowChatBubbleCancel)
        Me.fraShowChatBubble.Controls.Add(Me.DarkLabel41)
        Me.fraShowChatBubble.Controls.Add(Me.cmbChatBubbleTarget)
        Me.fraShowChatBubble.Controls.Add(Me.cmbChatBubbleTargetType)
        Me.fraShowChatBubble.Controls.Add(Me.DarkLabel40)
        Me.fraShowChatBubble.Controls.Add(Me.txtChatbubbleText)
        Me.fraShowChatBubble.Controls.Add(Me.DarkLabel39)
        Me.fraShowChatBubble.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraShowChatBubble.Location = New System.Drawing.Point(401, 181)
        Me.fraShowChatBubble.Name = "fraShowChatBubble"
        Me.fraShowChatBubble.Size = New System.Drawing.Size(246, 141)
        Me.fraShowChatBubble.TabIndex = 27
        Me.fraShowChatBubble.TabStop = False
        Me.fraShowChatBubble.Text = "Show ChatBubble"
        Me.fraShowChatBubble.Visible = False
        '
        'btnShowChatBubbleOk
        '
        Me.btnShowChatBubbleOk.Location = New System.Drawing.Point(84, 112)
        Me.btnShowChatBubbleOk.Name = "btnShowChatBubbleOk"
        Me.btnShowChatBubbleOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnShowChatBubbleOk.Size = New System.Drawing.Size(75, 23)
        Me.btnShowChatBubbleOk.TabIndex = 31
        Me.btnShowChatBubbleOk.Text = "Ok"
        '
        'btnShowChatBubbleCancel
        '
        Me.btnShowChatBubbleCancel.Location = New System.Drawing.Point(165, 112)
        Me.btnShowChatBubbleCancel.Name = "btnShowChatBubbleCancel"
        Me.btnShowChatBubbleCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnShowChatBubbleCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnShowChatBubbleCancel.TabIndex = 30
        Me.btnShowChatBubbleCancel.Text = "Cancel"
        '
        'DarkLabel41
        '
        Me.DarkLabel41.AutoSize = True
        Me.DarkLabel41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel41.Location = New System.Drawing.Point(6, 88)
        Me.DarkLabel41.Name = "DarkLabel41"
        Me.DarkLabel41.Size = New System.Drawing.Size(36, 13)
        Me.DarkLabel41.TabIndex = 29
        Me.DarkLabel41.Text = "Index:"
        '
        'cmbChatBubbleTarget
        '
        Me.cmbChatBubbleTarget.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbChatBubbleTarget.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbChatBubbleTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChatBubbleTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbChatBubbleTarget.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbChatBubbleTarget.FormattingEnabled = True
        Me.cmbChatBubbleTarget.Location = New System.Drawing.Point(81, 85)
        Me.cmbChatBubbleTarget.Name = "cmbChatBubbleTarget"
        Me.cmbChatBubbleTarget.Size = New System.Drawing.Size(159, 21)
        Me.cmbChatBubbleTarget.TabIndex = 28
        Me.cmbChatBubbleTarget.Text = Nothing
        '
        'cmbChatBubbleTargetType
        '
        Me.cmbChatBubbleTargetType.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbChatBubbleTargetType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbChatBubbleTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChatBubbleTargetType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbChatBubbleTargetType.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbChatBubbleTargetType.FormattingEnabled = True
        Me.cmbChatBubbleTargetType.Items.AddRange(New Object() {"Player", "Npc", "Event"})
        Me.cmbChatBubbleTargetType.Location = New System.Drawing.Point(81, 58)
        Me.cmbChatBubbleTargetType.Name = "cmbChatBubbleTargetType"
        Me.cmbChatBubbleTargetType.Size = New System.Drawing.Size(159, 21)
        Me.cmbChatBubbleTargetType.TabIndex = 27
        Me.cmbChatBubbleTargetType.Text = Nothing
        '
        'DarkLabel40
        '
        Me.DarkLabel40.AutoSize = True
        Me.DarkLabel40.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel40.Location = New System.Drawing.Point(6, 61)
        Me.DarkLabel40.Name = "DarkLabel40"
        Me.DarkLabel40.Size = New System.Drawing.Size(68, 13)
        Me.DarkLabel40.TabIndex = 2
        Me.DarkLabel40.Text = "Target Type:"
        '
        'txtChatbubbleText
        '
        Me.txtChatbubbleText.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChatbubbleText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChatbubbleText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChatbubbleText.Location = New System.Drawing.Point(6, 32)
        Me.txtChatbubbleText.Name = "txtChatbubbleText"
        Me.txtChatbubbleText.Size = New System.Drawing.Size(234, 20)
        Me.txtChatbubbleText.TabIndex = 1
        '
        'DarkLabel39
        '
        Me.DarkLabel39.AutoSize = True
        Me.DarkLabel39.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel39.Location = New System.Drawing.Point(6, 16)
        Me.DarkLabel39.Name = "DarkLabel39"
        Me.DarkLabel39.Size = New System.Drawing.Size(86, 13)
        Me.DarkLabel39.TabIndex = 0
        Me.DarkLabel39.Text = "ChatBubble Text"
        '
        'fraPlaySound
        '
        Me.fraPlaySound.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlaySound.Controls.Add(Me.btnPlaySoundOk)
        Me.fraPlaySound.Controls.Add(Me.btnPlaySoundCancel)
        Me.fraPlaySound.Controls.Add(Me.cmbPlaySound)
        Me.fraPlaySound.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlaySound.Location = New System.Drawing.Point(401, 179)
        Me.fraPlaySound.Name = "fraPlaySound"
        Me.fraPlaySound.Size = New System.Drawing.Size(246, 76)
        Me.fraPlaySound.TabIndex = 26
        Me.fraPlaySound.TabStop = False
        Me.fraPlaySound.Text = "Play Sound"
        Me.fraPlaySound.Visible = False
        '
        'btnPlaySoundOk
        '
        Me.btnPlaySoundOk.Location = New System.Drawing.Point(84, 46)
        Me.btnPlaySoundOk.Name = "btnPlaySoundOk"
        Me.btnPlaySoundOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPlaySoundOk.Size = New System.Drawing.Size(75, 23)
        Me.btnPlaySoundOk.TabIndex = 27
        Me.btnPlaySoundOk.Text = "Ok"
        '
        'btnPlaySoundCancel
        '
        Me.btnPlaySoundCancel.Location = New System.Drawing.Point(165, 46)
        Me.btnPlaySoundCancel.Name = "btnPlaySoundCancel"
        Me.btnPlaySoundCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPlaySoundCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnPlaySoundCancel.TabIndex = 26
        Me.btnPlaySoundCancel.Text = "Cancel"
        '
        'cmbPlaySound
        '
        Me.cmbPlaySound.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPlaySound.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlaySound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlaySound.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPlaySound.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPlaySound.FormattingEnabled = True
        Me.cmbPlaySound.Location = New System.Drawing.Point(6, 19)
        Me.cmbPlaySound.Name = "cmbPlaySound"
        Me.cmbPlaySound.Size = New System.Drawing.Size(234, 21)
        Me.cmbPlaySound.TabIndex = 0
        Me.cmbPlaySound.Text = Nothing
        '
        'fraChangePK
        '
        Me.fraChangePK.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangePK.Controls.Add(Me.btnChangePkOk)
        Me.fraChangePK.Controls.Add(Me.btnChangePkCancel)
        Me.fraChangePK.Controls.Add(Me.cmbSetPK)
        Me.fraChangePK.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangePK.Location = New System.Drawing.Point(401, 104)
        Me.fraChangePK.Name = "fraChangePK"
        Me.fraChangePK.Size = New System.Drawing.Size(246, 75)
        Me.fraChangePK.TabIndex = 25
        Me.fraChangePK.TabStop = False
        Me.fraChangePK.Text = "Set Player PK"
        Me.fraChangePK.Visible = False
        '
        'btnChangePkOk
        '
        Me.btnChangePkOk.Location = New System.Drawing.Point(80, 46)
        Me.btnChangePkOk.Name = "btnChangePkOk"
        Me.btnChangePkOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangePkOk.Size = New System.Drawing.Size(75, 23)
        Me.btnChangePkOk.TabIndex = 27
        Me.btnChangePkOk.Text = "Ok"
        '
        'btnChangePkCancel
        '
        Me.btnChangePkCancel.Location = New System.Drawing.Point(161, 46)
        Me.btnChangePkCancel.Name = "btnChangePkCancel"
        Me.btnChangePkCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangePkCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnChangePkCancel.TabIndex = 26
        Me.btnChangePkCancel.Text = "Cancel"
        '
        'cmbSetPK
        '
        Me.cmbSetPK.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbSetPK.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSetPK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSetPK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSetPK.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbSetPK.FormattingEnabled = True
        Me.cmbSetPK.Items.AddRange(New Object() {"No", "Yes"})
        Me.cmbSetPK.Location = New System.Drawing.Point(10, 19)
        Me.cmbSetPK.Name = "cmbSetPK"
        Me.cmbSetPK.Size = New System.Drawing.Size(226, 21)
        Me.cmbSetPK.TabIndex = 18
        Me.cmbSetPK.Text = Nothing
        '
        'fraCreateLabel
        '
        Me.fraCreateLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraCreateLabel.Controls.Add(Me.btnCreatelabelOk)
        Me.fraCreateLabel.Controls.Add(Me.btnCreatelabelCancel)
        Me.fraCreateLabel.Controls.Add(Me.txtLabelName)
        Me.fraCreateLabel.Controls.Add(Me.lblLabelName)
        Me.fraCreateLabel.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraCreateLabel.Location = New System.Drawing.Point(401, 132)
        Me.fraCreateLabel.Name = "fraCreateLabel"
        Me.fraCreateLabel.Size = New System.Drawing.Size(246, 74)
        Me.fraCreateLabel.TabIndex = 24
        Me.fraCreateLabel.TabStop = False
        Me.fraCreateLabel.Text = "Create Label"
        Me.fraCreateLabel.Visible = False
        '
        'btnCreatelabelOk
        '
        Me.btnCreatelabelOk.Location = New System.Drawing.Point(84, 45)
        Me.btnCreatelabelOk.Name = "btnCreatelabelOk"
        Me.btnCreatelabelOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCreatelabelOk.Size = New System.Drawing.Size(75, 23)
        Me.btnCreatelabelOk.TabIndex = 27
        Me.btnCreatelabelOk.Text = "Ok"
        '
        'btnCreatelabelCancel
        '
        Me.btnCreatelabelCancel.Location = New System.Drawing.Point(165, 45)
        Me.btnCreatelabelCancel.Name = "btnCreatelabelCancel"
        Me.btnCreatelabelCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCreatelabelCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCreatelabelCancel.TabIndex = 26
        Me.btnCreatelabelCancel.Text = "Cancel"
        '
        'txtLabelName
        '
        Me.txtLabelName.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtLabelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLabelName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtLabelName.Location = New System.Drawing.Point(80, 19)
        Me.txtLabelName.Name = "txtLabelName"
        Me.txtLabelName.Size = New System.Drawing.Size(160, 20)
        Me.txtLabelName.TabIndex = 1
        '
        'lblLabelName
        '
        Me.lblLabelName.AutoSize = True
        Me.lblLabelName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.lblLabelName.Location = New System.Drawing.Point(7, 21)
        Me.lblLabelName.Name = "lblLabelName"
        Me.lblLabelName.Size = New System.Drawing.Size(67, 13)
        Me.lblLabelName.TabIndex = 0
        Me.lblLabelName.Text = "Label Name:"
        '
        'fraChangeClass
        '
        Me.fraChangeClass.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeClass.Controls.Add(Me.btnChangeClassOk)
        Me.fraChangeClass.Controls.Add(Me.btnChangeClassCancel)
        Me.fraChangeClass.Controls.Add(Me.cmbChangeClass)
        Me.fraChangeClass.Controls.Add(Me.DarkLabel38)
        Me.fraChangeClass.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeClass.Location = New System.Drawing.Point(401, 109)
        Me.fraChangeClass.Name = "fraChangeClass"
        Me.fraChangeClass.Size = New System.Drawing.Size(246, 76)
        Me.fraChangeClass.TabIndex = 23
        Me.fraChangeClass.TabStop = False
        Me.fraChangeClass.Text = "Change Player Class"
        Me.fraChangeClass.Visible = False
        '
        'btnChangeClassOk
        '
        Me.btnChangeClassOk.Location = New System.Drawing.Point(84, 46)
        Me.btnChangeClassOk.Name = "btnChangeClassOk"
        Me.btnChangeClassOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeClassOk.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeClassOk.TabIndex = 27
        Me.btnChangeClassOk.Text = "Ok"
        '
        'btnChangeClassCancel
        '
        Me.btnChangeClassCancel.Location = New System.Drawing.Point(165, 46)
        Me.btnChangeClassCancel.Name = "btnChangeClassCancel"
        Me.btnChangeClassCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeClassCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeClassCancel.TabIndex = 26
        Me.btnChangeClassCancel.Text = "Cancel"
        '
        'cmbChangeClass
        '
        Me.cmbChangeClass.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbChangeClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbChangeClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChangeClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbChangeClass.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbChangeClass.FormattingEnabled = True
        Me.cmbChangeClass.Location = New System.Drawing.Point(49, 19)
        Me.cmbChangeClass.Name = "cmbChangeClass"
        Me.cmbChangeClass.Size = New System.Drawing.Size(191, 21)
        Me.cmbChangeClass.TabIndex = 1
        Me.cmbChangeClass.Text = Nothing
        '
        'DarkLabel38
        '
        Me.DarkLabel38.AutoSize = True
        Me.DarkLabel38.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel38.Location = New System.Drawing.Point(8, 22)
        Me.DarkLabel38.Name = "DarkLabel38"
        Me.DarkLabel38.Size = New System.Drawing.Size(35, 13)
        Me.DarkLabel38.TabIndex = 0
        Me.DarkLabel38.Text = "Class:"
        '
        'fraChangeSkills
        '
        Me.fraChangeSkills.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeSkills.Controls.Add(Me.btnChangeSkillsOk)
        Me.fraChangeSkills.Controls.Add(Me.btnChangeSkillsCancel)
        Me.fraChangeSkills.Controls.Add(Me.optChangeSkillsRemove)
        Me.fraChangeSkills.Controls.Add(Me.optChangeSkillsAdd)
        Me.fraChangeSkills.Controls.Add(Me.cmbChangeSkills)
        Me.fraChangeSkills.Controls.Add(Me.DarkLabel37)
        Me.fraChangeSkills.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeSkills.Location = New System.Drawing.Point(401, 108)
        Me.fraChangeSkills.Name = "fraChangeSkills"
        Me.fraChangeSkills.Size = New System.Drawing.Size(246, 98)
        Me.fraChangeSkills.TabIndex = 22
        Me.fraChangeSkills.TabStop = False
        Me.fraChangeSkills.Text = "Change Player Skills"
        Me.fraChangeSkills.Visible = False
        '
        'btnChangeSkillsOk
        '
        Me.btnChangeSkillsOk.Location = New System.Drawing.Point(84, 67)
        Me.btnChangeSkillsOk.Name = "btnChangeSkillsOk"
        Me.btnChangeSkillsOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeSkillsOk.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeSkillsOk.TabIndex = 27
        Me.btnChangeSkillsOk.Text = "Ok"
        '
        'btnChangeSkillsCancel
        '
        Me.btnChangeSkillsCancel.Location = New System.Drawing.Point(165, 67)
        Me.btnChangeSkillsCancel.Name = "btnChangeSkillsCancel"
        Me.btnChangeSkillsCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeSkillsCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeSkillsCancel.TabIndex = 26
        Me.btnChangeSkillsCancel.Text = "Cancel"
        '
        'optChangeSkillsRemove
        '
        Me.optChangeSkillsRemove.AutoSize = True
        Me.optChangeSkillsRemove.Location = New System.Drawing.Point(147, 44)
        Me.optChangeSkillsRemove.Name = "optChangeSkillsRemove"
        Me.optChangeSkillsRemove.Size = New System.Drawing.Size(55, 17)
        Me.optChangeSkillsRemove.TabIndex = 3
        Me.optChangeSkillsRemove.TabStop = True
        Me.optChangeSkillsRemove.Text = "Forget"
        '
        'optChangeSkillsAdd
        '
        Me.optChangeSkillsAdd.AutoSize = True
        Me.optChangeSkillsAdd.Location = New System.Drawing.Point(65, 44)
        Me.optChangeSkillsAdd.Name = "optChangeSkillsAdd"
        Me.optChangeSkillsAdd.Size = New System.Drawing.Size(56, 17)
        Me.optChangeSkillsAdd.TabIndex = 2
        Me.optChangeSkillsAdd.TabStop = True
        Me.optChangeSkillsAdd.Text = "Teach"
        '
        'cmbChangeSkills
        '
        Me.cmbChangeSkills.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbChangeSkills.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbChangeSkills.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChangeSkills.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbChangeSkills.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbChangeSkills.FormattingEnabled = True
        Me.cmbChangeSkills.Location = New System.Drawing.Point(41, 17)
        Me.cmbChangeSkills.Name = "cmbChangeSkills"
        Me.cmbChangeSkills.Size = New System.Drawing.Size(198, 21)
        Me.cmbChangeSkills.TabIndex = 1
        Me.cmbChangeSkills.Text = Nothing
        '
        'DarkLabel37
        '
        Me.DarkLabel37.AutoSize = True
        Me.DarkLabel37.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel37.Location = New System.Drawing.Point(6, 20)
        Me.DarkLabel37.Name = "DarkLabel37"
        Me.DarkLabel37.Size = New System.Drawing.Size(29, 13)
        Me.DarkLabel37.TabIndex = 0
        Me.DarkLabel37.Text = "Skill:"
        '
        'fraCompleteTask
        '
        Me.fraCompleteTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraCompleteTask.Controls.Add(Me.btnCompleteQuestTaskOk)
        Me.fraCompleteTask.Controls.Add(Me.btnCompleteQuestTaskCancel)
        Me.fraCompleteTask.Controls.Add(Me.DarkLabel35)
        Me.fraCompleteTask.Controls.Add(Me.DarkLabel36)
        Me.fraCompleteTask.Controls.Add(Me.nudCompleteQuestTask)
        Me.fraCompleteTask.Controls.Add(Me.cmbCompleteQuest)
        Me.fraCompleteTask.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraCompleteTask.Location = New System.Drawing.Point(401, 3)
        Me.fraCompleteTask.Name = "fraCompleteTask"
        Me.fraCompleteTask.Size = New System.Drawing.Size(246, 100)
        Me.fraCompleteTask.TabIndex = 20
        Me.fraCompleteTask.TabStop = False
        Me.fraCompleteTask.Text = "Complete Quest Task"
        Me.fraCompleteTask.Visible = False
        '
        'btnCompleteQuestTaskOk
        '
        Me.btnCompleteQuestTaskOk.Location = New System.Drawing.Point(84, 74)
        Me.btnCompleteQuestTaskOk.Name = "btnCompleteQuestTaskOk"
        Me.btnCompleteQuestTaskOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCompleteQuestTaskOk.Size = New System.Drawing.Size(75, 23)
        Me.btnCompleteQuestTaskOk.TabIndex = 27
        Me.btnCompleteQuestTaskOk.Text = "Ok"
        '
        'btnCompleteQuestTaskCancel
        '
        Me.btnCompleteQuestTaskCancel.Location = New System.Drawing.Point(165, 74)
        Me.btnCompleteQuestTaskCancel.Name = "btnCompleteQuestTaskCancel"
        Me.btnCompleteQuestTaskCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCompleteQuestTaskCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCompleteQuestTaskCancel.TabIndex = 26
        Me.btnCompleteQuestTaskCancel.Text = "Cancel"
        '
        'DarkLabel35
        '
        Me.DarkLabel35.AutoSize = True
        Me.DarkLabel35.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel35.Location = New System.Drawing.Point(10, 50)
        Me.DarkLabel35.Name = "DarkLabel35"
        Me.DarkLabel35.Size = New System.Drawing.Size(34, 13)
        Me.DarkLabel35.TabIndex = 23
        Me.DarkLabel35.Text = "Task:"
        '
        'DarkLabel36
        '
        Me.DarkLabel36.AutoSize = True
        Me.DarkLabel36.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel36.Location = New System.Drawing.Point(10, 22)
        Me.DarkLabel36.Name = "DarkLabel36"
        Me.DarkLabel36.Size = New System.Drawing.Size(38, 13)
        Me.DarkLabel36.TabIndex = 22
        Me.DarkLabel36.Text = "Quest:"
        '
        'nudCompleteQuestTask
        '
        Me.nudCompleteQuestTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudCompleteQuestTask.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudCompleteQuestTask.Location = New System.Drawing.Point(60, 48)
        Me.nudCompleteQuestTask.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudCompleteQuestTask.Name = "nudCompleteQuestTask"
        Me.nudCompleteQuestTask.Size = New System.Drawing.Size(179, 20)
        Me.nudCompleteQuestTask.TabIndex = 21
        '
        'cmbCompleteQuest
        '
        Me.cmbCompleteQuest.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCompleteQuest.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCompleteQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompleteQuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCompleteQuest.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCompleteQuest.FormattingEnabled = True
        Me.cmbCompleteQuest.Location = New System.Drawing.Point(60, 19)
        Me.cmbCompleteQuest.Name = "cmbCompleteQuest"
        Me.cmbCompleteQuest.Size = New System.Drawing.Size(179, 21)
        Me.cmbCompleteQuest.TabIndex = 20
        Me.cmbCompleteQuest.Text = Nothing
        '
        'fraPlayerWarp
        '
        Me.fraPlayerWarp.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlayerWarp.Controls.Add(Me.btnPlayerWarpOk)
        Me.fraPlayerWarp.Controls.Add(Me.btnPlayerWarpCancel)
        Me.fraPlayerWarp.Controls.Add(Me.DarkLabel31)
        Me.fraPlayerWarp.Controls.Add(Me.cmbWarpPlayerDir)
        Me.fraPlayerWarp.Controls.Add(Me.nudWPY)
        Me.fraPlayerWarp.Controls.Add(Me.DarkLabel32)
        Me.fraPlayerWarp.Controls.Add(Me.nudWPX)
        Me.fraPlayerWarp.Controls.Add(Me.DarkLabel33)
        Me.fraPlayerWarp.Controls.Add(Me.nudWPMap)
        Me.fraPlayerWarp.Controls.Add(Me.DarkLabel34)
        Me.fraPlayerWarp.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlayerWarp.Location = New System.Drawing.Point(401, 6)
        Me.fraPlayerWarp.Name = "fraPlayerWarp"
        Me.fraPlayerWarp.Size = New System.Drawing.Size(246, 97)
        Me.fraPlayerWarp.TabIndex = 19
        Me.fraPlayerWarp.TabStop = False
        Me.fraPlayerWarp.Text = "Warp Player"
        Me.fraPlayerWarp.Visible = False
        '
        'btnPlayerWarpOk
        '
        Me.btnPlayerWarpOk.Location = New System.Drawing.Point(83, 68)
        Me.btnPlayerWarpOk.Name = "btnPlayerWarpOk"
        Me.btnPlayerWarpOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPlayerWarpOk.Size = New System.Drawing.Size(75, 23)
        Me.btnPlayerWarpOk.TabIndex = 46
        Me.btnPlayerWarpOk.Text = "Ok"
        '
        'btnPlayerWarpCancel
        '
        Me.btnPlayerWarpCancel.Location = New System.Drawing.Point(164, 68)
        Me.btnPlayerWarpCancel.Name = "btnPlayerWarpCancel"
        Me.btnPlayerWarpCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPlayerWarpCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnPlayerWarpCancel.TabIndex = 45
        Me.btnPlayerWarpCancel.Text = "Cancel"
        '
        'DarkLabel31
        '
        Me.DarkLabel31.AutoSize = True
        Me.DarkLabel31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel31.Location = New System.Drawing.Point(8, 44)
        Me.DarkLabel31.Name = "DarkLabel31"
        Me.DarkLabel31.Size = New System.Drawing.Size(52, 13)
        Me.DarkLabel31.TabIndex = 44
        Me.DarkLabel31.Text = "Direction:"
        '
        'cmbWarpPlayerDir
        '
        Me.cmbWarpPlayerDir.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbWarpPlayerDir.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbWarpPlayerDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWarpPlayerDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbWarpPlayerDir.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbWarpPlayerDir.FormattingEnabled = True
        Me.cmbWarpPlayerDir.Items.AddRange(New Object() {"Retain Direction", "Up", "Down", "Left", "Right"})
        Me.cmbWarpPlayerDir.Location = New System.Drawing.Point(96, 41)
        Me.cmbWarpPlayerDir.Name = "cmbWarpPlayerDir"
        Me.cmbWarpPlayerDir.Size = New System.Drawing.Size(143, 21)
        Me.cmbWarpPlayerDir.TabIndex = 43
        Me.cmbWarpPlayerDir.Text = Nothing
        '
        'nudWPY
        '
        Me.nudWPY.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudWPY.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudWPY.Location = New System.Drawing.Point(200, 15)
        Me.nudWPY.Name = "nudWPY"
        Me.nudWPY.Size = New System.Drawing.Size(39, 20)
        Me.nudWPY.TabIndex = 42
        '
        'DarkLabel32
        '
        Me.DarkLabel32.AutoSize = True
        Me.DarkLabel32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel32.Location = New System.Drawing.Point(177, 17)
        Me.DarkLabel32.Name = "DarkLabel32"
        Me.DarkLabel32.Size = New System.Drawing.Size(17, 13)
        Me.DarkLabel32.TabIndex = 41
        Me.DarkLabel32.Text = "Y:"
        '
        'nudWPX
        '
        Me.nudWPX.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudWPX.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudWPX.Location = New System.Drawing.Point(130, 15)
        Me.nudWPX.Name = "nudWPX"
        Me.nudWPX.Size = New System.Drawing.Size(39, 20)
        Me.nudWPX.TabIndex = 40
        '
        'DarkLabel33
        '
        Me.DarkLabel33.AutoSize = True
        Me.DarkLabel33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel33.Location = New System.Drawing.Point(107, 17)
        Me.DarkLabel33.Name = "DarkLabel33"
        Me.DarkLabel33.Size = New System.Drawing.Size(17, 13)
        Me.DarkLabel33.TabIndex = 39
        Me.DarkLabel33.Text = "X:"
        '
        'nudWPMap
        '
        Me.nudWPMap.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudWPMap.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudWPMap.Location = New System.Drawing.Point(43, 15)
        Me.nudWPMap.Name = "nudWPMap"
        Me.nudWPMap.Size = New System.Drawing.Size(58, 20)
        Me.nudWPMap.TabIndex = 38
        '
        'DarkLabel34
        '
        Me.DarkLabel34.AutoSize = True
        Me.DarkLabel34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel34.Location = New System.Drawing.Point(6, 17)
        Me.DarkLabel34.Name = "DarkLabel34"
        Me.DarkLabel34.Size = New System.Drawing.Size(31, 13)
        Me.DarkLabel34.TabIndex = 37
        Me.DarkLabel34.Text = "Map:"
        '
        'fraSetFog
        '
        Me.fraSetFog.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSetFog.Controls.Add(Me.btnSetFogOk)
        Me.fraSetFog.Controls.Add(Me.btnSetFogCancel)
        Me.fraSetFog.Controls.Add(Me.DarkLabel30)
        Me.fraSetFog.Controls.Add(Me.DarkLabel29)
        Me.fraSetFog.Controls.Add(Me.DarkLabel28)
        Me.fraSetFog.Controls.Add(Me.nudFogData2)
        Me.fraSetFog.Controls.Add(Me.nudFogData1)
        Me.fraSetFog.Controls.Add(Me.nudFogData0)
        Me.fraSetFog.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSetFog.Location = New System.Drawing.Point(401, 7)
        Me.fraSetFog.Name = "fraSetFog"
        Me.fraSetFog.Size = New System.Drawing.Size(246, 96)
        Me.fraSetFog.TabIndex = 18
        Me.fraSetFog.TabStop = False
        Me.fraSetFog.Text = "Set Fog"
        Me.fraSetFog.Visible = False
        '
        'btnSetFogOk
        '
        Me.btnSetFogOk.Location = New System.Drawing.Point(84, 67)
        Me.btnSetFogOk.Name = "btnSetFogOk"
        Me.btnSetFogOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSetFogOk.Size = New System.Drawing.Size(75, 23)
        Me.btnSetFogOk.TabIndex = 41
        Me.btnSetFogOk.Text = "Ok"
        '
        'btnSetFogCancel
        '
        Me.btnSetFogCancel.Location = New System.Drawing.Point(165, 67)
        Me.btnSetFogCancel.Name = "btnSetFogCancel"
        Me.btnSetFogCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSetFogCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnSetFogCancel.TabIndex = 40
        Me.btnSetFogCancel.Text = "Cancel"
        '
        'DarkLabel30
        '
        Me.DarkLabel30.AutoSize = True
        Me.DarkLabel30.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel30.Location = New System.Drawing.Point(124, 42)
        Me.DarkLabel30.Name = "DarkLabel30"
        Me.DarkLabel30.Size = New System.Drawing.Size(67, 13)
        Me.DarkLabel30.TabIndex = 39
        Me.DarkLabel30.Text = "Fog Opacity:"
        '
        'DarkLabel29
        '
        Me.DarkLabel29.AutoSize = True
        Me.DarkLabel29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel29.Location = New System.Drawing.Point(7, 42)
        Me.DarkLabel29.Name = "DarkLabel29"
        Me.DarkLabel29.Size = New System.Drawing.Size(62, 13)
        Me.DarkLabel29.TabIndex = 38
        Me.DarkLabel29.Text = "Fog Speed:"
        '
        'DarkLabel28
        '
        Me.DarkLabel28.AutoSize = True
        Me.DarkLabel28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel28.Location = New System.Drawing.Point(7, 15)
        Me.DarkLabel28.Name = "DarkLabel28"
        Me.DarkLabel28.Size = New System.Drawing.Size(28, 13)
        Me.DarkLabel28.TabIndex = 37
        Me.DarkLabel28.Text = "Fog:"
        '
        'nudFogData2
        '
        Me.nudFogData2.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudFogData2.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudFogData2.Location = New System.Drawing.Point(191, 39)
        Me.nudFogData2.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudFogData2.Name = "nudFogData2"
        Me.nudFogData2.Size = New System.Drawing.Size(49, 20)
        Me.nudFogData2.TabIndex = 36
        '
        'nudFogData1
        '
        Me.nudFogData1.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudFogData1.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudFogData1.Location = New System.Drawing.Point(72, 40)
        Me.nudFogData1.Name = "nudFogData1"
        Me.nudFogData1.Size = New System.Drawing.Size(48, 20)
        Me.nudFogData1.TabIndex = 35
        '
        'nudFogData0
        '
        Me.nudFogData0.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudFogData0.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudFogData0.Location = New System.Drawing.Point(97, 12)
        Me.nudFogData0.Name = "nudFogData0"
        Me.nudFogData0.Size = New System.Drawing.Size(143, 20)
        Me.nudFogData0.TabIndex = 34
        '
        'fraShowText
        '
        Me.fraShowText.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraShowText.Controls.Add(Me.DarkLabel27)
        Me.fraShowText.Controls.Add(Me.txtShowText)
        Me.fraShowText.Controls.Add(Me.btnShowTextCancel)
        Me.fraShowText.Controls.Add(Me.btnShowTextOk)
        Me.fraShowText.Controls.Add(Me.picShowTextFace)
        Me.fraShowText.Controls.Add(Me.DarkLabel26)
        Me.fraShowText.Controls.Add(Me.nudShowTextFace)
        Me.fraShowText.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraShowText.Location = New System.Drawing.Point(6, 304)
        Me.fraShowText.Name = "fraShowText"
        Me.fraShowText.Size = New System.Drawing.Size(248, 284)
        Me.fraShowText.TabIndex = 17
        Me.fraShowText.TabStop = False
        Me.fraShowText.Text = "Show Text"
        Me.fraShowText.Visible = False
        '
        'DarkLabel27
        '
        Me.DarkLabel27.AutoSize = True
        Me.DarkLabel27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel27.Location = New System.Drawing.Point(7, 19)
        Me.DarkLabel27.Name = "DarkLabel27"
        Me.DarkLabel27.Size = New System.Drawing.Size(28, 13)
        Me.DarkLabel27.TabIndex = 26
        Me.DarkLabel27.Text = "Text"
        '
        'txtShowText
        '
        Me.txtShowText.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtShowText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShowText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtShowText.Location = New System.Drawing.Point(9, 38)
        Me.txtShowText.Multiline = True
        Me.txtShowText.Name = "txtShowText"
        Me.txtShowText.Size = New System.Drawing.Size(228, 105)
        Me.txtShowText.TabIndex = 21
        '
        'btnShowTextCancel
        '
        Me.btnShowTextCancel.Location = New System.Drawing.Point(167, 252)
        Me.btnShowTextCancel.Name = "btnShowTextCancel"
        Me.btnShowTextCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnShowTextCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnShowTextCancel.TabIndex = 24
        Me.btnShowTextCancel.Text = "Cancel"
        '
        'btnShowTextOk
        '
        Me.btnShowTextOk.Location = New System.Drawing.Point(86, 252)
        Me.btnShowTextOk.Name = "btnShowTextOk"
        Me.btnShowTextOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnShowTextOk.Size = New System.Drawing.Size(75, 23)
        Me.btnShowTextOk.TabIndex = 25
        Me.btnShowTextOk.Text = "Ok"
        '
        'picShowTextFace
        '
        Me.picShowTextFace.BackColor = System.Drawing.Color.Black
        Me.picShowTextFace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picShowTextFace.Location = New System.Drawing.Point(7, 149)
        Me.picShowTextFace.Name = "picShowTextFace"
        Me.picShowTextFace.Size = New System.Drawing.Size(100, 93)
        Me.picShowTextFace.TabIndex = 2
        Me.picShowTextFace.TabStop = False
        '
        'DarkLabel26
        '
        Me.DarkLabel26.AutoSize = True
        Me.DarkLabel26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel26.Location = New System.Drawing.Point(110, 224)
        Me.DarkLabel26.Name = "DarkLabel26"
        Me.DarkLabel26.Size = New System.Drawing.Size(34, 13)
        Me.DarkLabel26.TabIndex = 22
        Me.DarkLabel26.Text = "Face:"
        '
        'nudShowTextFace
        '
        Me.nudShowTextFace.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudShowTextFace.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudShowTextFace.Location = New System.Drawing.Point(147, 222)
        Me.nudShowTextFace.Name = "nudShowTextFace"
        Me.nudShowTextFace.Size = New System.Drawing.Size(92, 20)
        Me.nudShowTextFace.TabIndex = 23
        '
        'fraAddText
        '
        Me.fraAddText.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraAddText.Controls.Add(Me.btnAddTextOk)
        Me.fraAddText.Controls.Add(Me.btnAddTextCancel)
        Me.fraAddText.Controls.Add(Me.optAddText_Global)
        Me.fraAddText.Controls.Add(Me.optAddText_Map)
        Me.fraAddText.Controls.Add(Me.optAddText_Player)
        Me.fraAddText.Controls.Add(Me.DarkLabel25)
        Me.fraAddText.Controls.Add(Me.txtAddText_Text)
        Me.fraAddText.Controls.Add(Me.DarkLabel24)
        Me.fraAddText.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraAddText.Location = New System.Drawing.Point(6, 363)
        Me.fraAddText.Name = "fraAddText"
        Me.fraAddText.Size = New System.Drawing.Size(233, 187)
        Me.fraAddText.TabIndex = 3
        Me.fraAddText.TabStop = False
        Me.fraAddText.Text = "Add Text"
        Me.fraAddText.Visible = False
        '
        'btnAddTextOk
        '
        Me.btnAddTextOk.Location = New System.Drawing.Point(55, 156)
        Me.btnAddTextOk.Name = "btnAddTextOk"
        Me.btnAddTextOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnAddTextOk.Size = New System.Drawing.Size(75, 23)
        Me.btnAddTextOk.TabIndex = 9
        Me.btnAddTextOk.Text = "Ok"
        '
        'btnAddTextCancel
        '
        Me.btnAddTextCancel.Location = New System.Drawing.Point(136, 156)
        Me.btnAddTextCancel.Name = "btnAddTextCancel"
        Me.btnAddTextCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnAddTextCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnAddTextCancel.TabIndex = 8
        Me.btnAddTextCancel.Text = "Cancel"
        '
        'optAddText_Global
        '
        Me.optAddText_Global.AutoSize = True
        Me.optAddText_Global.Location = New System.Drawing.Point(173, 133)
        Me.optAddText_Global.Name = "optAddText_Global"
        Me.optAddText_Global.Size = New System.Drawing.Size(55, 17)
        Me.optAddText_Global.TabIndex = 5
        Me.optAddText_Global.TabStop = True
        Me.optAddText_Global.Text = "Global"
        '
        'optAddText_Map
        '
        Me.optAddText_Map.AutoSize = True
        Me.optAddText_Map.Location = New System.Drawing.Point(121, 133)
        Me.optAddText_Map.Name = "optAddText_Map"
        Me.optAddText_Map.Size = New System.Drawing.Size(46, 17)
        Me.optAddText_Map.TabIndex = 4
        Me.optAddText_Map.TabStop = True
        Me.optAddText_Map.Text = "Map"
        '
        'optAddText_Player
        '
        Me.optAddText_Player.AutoSize = True
        Me.optAddText_Player.Location = New System.Drawing.Point(61, 133)
        Me.optAddText_Player.Name = "optAddText_Player"
        Me.optAddText_Player.Size = New System.Drawing.Size(54, 17)
        Me.optAddText_Player.TabIndex = 3
        Me.optAddText_Player.TabStop = True
        Me.optAddText_Player.Text = "Player"
        '
        'DarkLabel25
        '
        Me.DarkLabel25.AutoSize = True
        Me.DarkLabel25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel25.Location = New System.Drawing.Point(6, 135)
        Me.DarkLabel25.Name = "DarkLabel25"
        Me.DarkLabel25.Size = New System.Drawing.Size(49, 13)
        Me.DarkLabel25.TabIndex = 2
        Me.DarkLabel25.Text = "Channel:"
        '
        'txtAddText_Text
        '
        Me.txtAddText_Text.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtAddText_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddText_Text.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtAddText_Text.Location = New System.Drawing.Point(6, 31)
        Me.txtAddText_Text.Multiline = True
        Me.txtAddText_Text.Name = "txtAddText_Text"
        Me.txtAddText_Text.Size = New System.Drawing.Size(222, 96)
        Me.txtAddText_Text.TabIndex = 1
        '
        'DarkLabel24
        '
        Me.DarkLabel24.AutoSize = True
        Me.DarkLabel24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel24.Location = New System.Drawing.Point(6, 15)
        Me.DarkLabel24.Name = "DarkLabel24"
        Me.DarkLabel24.Size = New System.Drawing.Size(28, 13)
        Me.DarkLabel24.TabIndex = 0
        Me.DarkLabel24.Text = "Text"
        '
        'fraPlayerSwitch
        '
        Me.fraPlayerSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlayerSwitch.Controls.Add(Me.btnSetPlayerSwitchOk)
        Me.fraPlayerSwitch.Controls.Add(Me.btnSetPlayerswitchCancel)
        Me.fraPlayerSwitch.Controls.Add(Me.cmbPlayerSwitchSet)
        Me.fraPlayerSwitch.Controls.Add(Me.DarkLabel23)
        Me.fraPlayerSwitch.Controls.Add(Me.cmbSwitch)
        Me.fraPlayerSwitch.Controls.Add(Me.DarkLabel22)
        Me.fraPlayerSwitch.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlayerSwitch.Location = New System.Drawing.Point(213, 390)
        Me.fraPlayerSwitch.Name = "fraPlayerSwitch"
        Me.fraPlayerSwitch.Size = New System.Drawing.Size(182, 100)
        Me.fraPlayerSwitch.TabIndex = 2
        Me.fraPlayerSwitch.TabStop = False
        Me.fraPlayerSwitch.Text = "Change Items"
        Me.fraPlayerSwitch.Visible = False
        '
        'btnSetPlayerSwitchOk
        '
        Me.btnSetPlayerSwitchOk.Location = New System.Drawing.Point(20, 72)
        Me.btnSetPlayerSwitchOk.Name = "btnSetPlayerSwitchOk"
        Me.btnSetPlayerSwitchOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSetPlayerSwitchOk.Size = New System.Drawing.Size(75, 23)
        Me.btnSetPlayerSwitchOk.TabIndex = 9
        Me.btnSetPlayerSwitchOk.Text = "Ok"
        '
        'btnSetPlayerswitchCancel
        '
        Me.btnSetPlayerswitchCancel.Location = New System.Drawing.Point(101, 72)
        Me.btnSetPlayerswitchCancel.Name = "btnSetPlayerswitchCancel"
        Me.btnSetPlayerswitchCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSetPlayerswitchCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnSetPlayerswitchCancel.TabIndex = 8
        Me.btnSetPlayerswitchCancel.Text = "Cancel"
        '
        'cmbPlayerSwitchSet
        '
        Me.cmbPlayerSwitchSet.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPlayerSwitchSet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayerSwitchSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlayerSwitchSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPlayerSwitchSet.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPlayerSwitchSet.FormattingEnabled = True
        Me.cmbPlayerSwitchSet.Items.AddRange(New Object() {"False", "True"})
        Me.cmbPlayerSwitchSet.Location = New System.Drawing.Point(51, 41)
        Me.cmbPlayerSwitchSet.Name = "cmbPlayerSwitchSet"
        Me.cmbPlayerSwitchSet.Size = New System.Drawing.Size(125, 21)
        Me.cmbPlayerSwitchSet.TabIndex = 3
        Me.cmbPlayerSwitchSet.Text = Nothing
        '
        'DarkLabel23
        '
        Me.DarkLabel23.AutoSize = True
        Me.DarkLabel23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel23.Location = New System.Drawing.Point(6, 46)
        Me.DarkLabel23.Name = "DarkLabel23"
        Me.DarkLabel23.Size = New System.Drawing.Size(35, 13)
        Me.DarkLabel23.TabIndex = 2
        Me.DarkLabel23.Text = "Set to"
        '
        'cmbSwitch
        '
        Me.cmbSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSwitch.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbSwitch.FormattingEnabled = True
        Me.cmbSwitch.Location = New System.Drawing.Point(51, 13)
        Me.cmbSwitch.Name = "cmbSwitch"
        Me.cmbSwitch.Size = New System.Drawing.Size(125, 21)
        Me.cmbSwitch.TabIndex = 1
        Me.cmbSwitch.Text = Nothing
        '
        'DarkLabel22
        '
        Me.DarkLabel22.AutoSize = True
        Me.DarkLabel22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel22.Location = New System.Drawing.Point(6, 16)
        Me.DarkLabel22.Name = "DarkLabel22"
        Me.DarkLabel22.Size = New System.Drawing.Size(39, 13)
        Me.DarkLabel22.TabIndex = 0
        Me.DarkLabel22.Text = "Switch"
        '
        'fraChangeItems
        '
        Me.fraChangeItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeItems.Controls.Add(Me.btnChangeItemsOk)
        Me.fraChangeItems.Controls.Add(Me.btnChangeItemsCancel)
        Me.fraChangeItems.Controls.Add(Me.nudChangeItemsAmount)
        Me.fraChangeItems.Controls.Add(Me.optChangeItemRemove)
        Me.fraChangeItems.Controls.Add(Me.optChangeItemAdd)
        Me.fraChangeItems.Controls.Add(Me.optChangeItemSet)
        Me.fraChangeItems.Controls.Add(Me.cmbChangeItemIndex)
        Me.fraChangeItems.Controls.Add(Me.DarkLabel21)
        Me.fraChangeItems.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeItems.Location = New System.Drawing.Point(6, 390)
        Me.fraChangeItems.Name = "fraChangeItems"
        Me.fraChangeItems.Size = New System.Drawing.Size(187, 120)
        Me.fraChangeItems.TabIndex = 1
        Me.fraChangeItems.TabStop = False
        Me.fraChangeItems.Text = "Change Items"
        Me.fraChangeItems.Visible = False
        '
        'btnChangeItemsOk
        '
        Me.btnChangeItemsOk.Location = New System.Drawing.Point(25, 91)
        Me.btnChangeItemsOk.Name = "btnChangeItemsOk"
        Me.btnChangeItemsOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeItemsOk.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeItemsOk.TabIndex = 7
        Me.btnChangeItemsOk.Text = "Ok"
        '
        'btnChangeItemsCancel
        '
        Me.btnChangeItemsCancel.Location = New System.Drawing.Point(106, 91)
        Me.btnChangeItemsCancel.Name = "btnChangeItemsCancel"
        Me.btnChangeItemsCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnChangeItemsCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeItemsCancel.TabIndex = 6
        Me.btnChangeItemsCancel.Text = "Cancel"
        '
        'nudChangeItemsAmount
        '
        Me.nudChangeItemsAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudChangeItemsAmount.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudChangeItemsAmount.Location = New System.Drawing.Point(9, 65)
        Me.nudChangeItemsAmount.Name = "nudChangeItemsAmount"
        Me.nudChangeItemsAmount.Size = New System.Drawing.Size(172, 20)
        Me.nudChangeItemsAmount.TabIndex = 5
        '
        'optChangeItemRemove
        '
        Me.optChangeItemRemove.AutoSize = True
        Me.optChangeItemRemove.Location = New System.Drawing.Point(121, 42)
        Me.optChangeItemRemove.Name = "optChangeItemRemove"
        Me.optChangeItemRemove.Size = New System.Drawing.Size(50, 17)
        Me.optChangeItemRemove.TabIndex = 4
        Me.optChangeItemRemove.TabStop = True
        Me.optChangeItemRemove.Text = "Take"
        '
        'optChangeItemAdd
        '
        Me.optChangeItemAdd.AutoSize = True
        Me.optChangeItemAdd.Location = New System.Drawing.Point(68, 42)
        Me.optChangeItemAdd.Name = "optChangeItemAdd"
        Me.optChangeItemAdd.Size = New System.Drawing.Size(47, 17)
        Me.optChangeItemAdd.TabIndex = 3
        Me.optChangeItemAdd.TabStop = True
        Me.optChangeItemAdd.Text = "Give"
        '
        'optChangeItemSet
        '
        Me.optChangeItemSet.AutoSize = True
        Me.optChangeItemSet.Location = New System.Drawing.Point(9, 42)
        Me.optChangeItemSet.Name = "optChangeItemSet"
        Me.optChangeItemSet.Size = New System.Drawing.Size(53, 17)
        Me.optChangeItemSet.TabIndex = 2
        Me.optChangeItemSet.TabStop = True
        Me.optChangeItemSet.Text = "Set to"
        '
        'cmbChangeItemIndex
        '
        Me.cmbChangeItemIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbChangeItemIndex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbChangeItemIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChangeItemIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbChangeItemIndex.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbChangeItemIndex.FormattingEnabled = True
        Me.cmbChangeItemIndex.Location = New System.Drawing.Point(42, 13)
        Me.cmbChangeItemIndex.Name = "cmbChangeItemIndex"
        Me.cmbChangeItemIndex.Size = New System.Drawing.Size(139, 21)
        Me.cmbChangeItemIndex.TabIndex = 1
        Me.cmbChangeItemIndex.Text = Nothing
        '
        'DarkLabel21
        '
        Me.DarkLabel21.AutoSize = True
        Me.DarkLabel21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel21.Location = New System.Drawing.Point(6, 16)
        Me.DarkLabel21.Name = "DarkLabel21"
        Me.DarkLabel21.Size = New System.Drawing.Size(30, 13)
        Me.DarkLabel21.TabIndex = 0
        Me.DarkLabel21.Text = "Item:"
        '
        'fraConditionalBranch
        '
        Me.fraConditionalBranch.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_Time)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition9)
        Me.fraConditionalBranch.Controls.Add(Me.btnConditionalBranchOk)
        Me.fraConditionalBranch.Controls.Add(Me.btnConditionalBranchCancel)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_Gender)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition8)
        Me.fraConditionalBranch.Controls.Add(Me.fraConditions_Quest)
        Me.fraConditionalBranch.Controls.Add(Me.nudCondition_Quest)
        Me.fraConditionalBranch.Controls.Add(Me.DarkLabel18)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition7)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_SelfSwitchCondition)
        Me.fraConditionalBranch.Controls.Add(Me.DarkLabel17)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_SelfSwitch)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition6)
        Me.fraConditionalBranch.Controls.Add(Me.nudCondition_LevelAmount)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition5)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_LevelCompare)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_LearntSkill)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition4)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_ClassIs)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition3)
        Me.fraConditionalBranch.Controls.Add(Me.nudCondition_HasItem)
        Me.fraConditionalBranch.Controls.Add(Me.DarkLabel16)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_HasItem)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition2)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition1)
        Me.fraConditionalBranch.Controls.Add(Me.DarkLabel15)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondtion_PlayerSwitchCondition)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_PlayerSwitch)
        Me.fraConditionalBranch.Controls.Add(Me.nudCondition_PlayerVarCondition)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_PlayerVarCompare)
        Me.fraConditionalBranch.Controls.Add(Me.DarkLabel14)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_PlayerVarIndex)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition0)
        Me.fraConditionalBranch.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraConditionalBranch.Location = New System.Drawing.Point(6, 7)
        Me.fraConditionalBranch.Name = "fraConditionalBranch"
        Me.fraConditionalBranch.Size = New System.Drawing.Size(389, 447)
        Me.fraConditionalBranch.TabIndex = 0
        Me.fraConditionalBranch.TabStop = False
        Me.fraConditionalBranch.Text = "Conditional Branch"
        Me.fraConditionalBranch.Visible = False
        '
        'btnConditionalBranchOk
        '
        Me.btnConditionalBranchOk.Location = New System.Drawing.Point(226, 416)
        Me.btnConditionalBranchOk.Name = "btnConditionalBranchOk"
        Me.btnConditionalBranchOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnConditionalBranchOk.Size = New System.Drawing.Size(75, 23)
        Me.btnConditionalBranchOk.TabIndex = 31
        Me.btnConditionalBranchOk.Text = "Ok"
        '
        'btnConditionalBranchCancel
        '
        Me.btnConditionalBranchCancel.Location = New System.Drawing.Point(307, 416)
        Me.btnConditionalBranchCancel.Name = "btnConditionalBranchCancel"
        Me.btnConditionalBranchCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnConditionalBranchCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnConditionalBranchCancel.TabIndex = 30
        Me.btnConditionalBranchCancel.Text = "Cancel"
        '
        'cmbCondition_Gender
        '
        Me.cmbCondition_Gender.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_Gender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_Gender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_Gender.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_Gender.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_Gender.FormattingEnabled = True
        Me.cmbCondition_Gender.Items.AddRange(New Object() {"Male", "Female"})
        Me.cmbCondition_Gender.Location = New System.Drawing.Point(239, 318)
        Me.cmbCondition_Gender.Name = "cmbCondition_Gender"
        Me.cmbCondition_Gender.Size = New System.Drawing.Size(144, 21)
        Me.cmbCondition_Gender.TabIndex = 29
        Me.cmbCondition_Gender.Text = Nothing
        '
        'optCondition8
        '
        Me.optCondition8.AutoSize = True
        Me.optCondition8.Location = New System.Drawing.Point(6, 319)
        Me.optCondition8.Name = "optCondition8"
        Me.optCondition8.Size = New System.Drawing.Size(102, 17)
        Me.optCondition8.TabIndex = 28
        Me.optCondition8.TabStop = True
        Me.optCondition8.Text = "Player Gender is"
        '
        'fraConditions_Quest
        '
        Me.fraConditions_Quest.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraConditions_Quest.Controls.Add(Me.DarkLabel20)
        Me.fraConditions_Quest.Controls.Add(Me.nudCondition_QuestTask)
        Me.fraConditions_Quest.Controls.Add(Me.cmbCondition_General)
        Me.fraConditions_Quest.Controls.Add(Me.DarkLabel19)
        Me.fraConditions_Quest.Controls.Add(Me.optCondition_Quest1)
        Me.fraConditions_Quest.Controls.Add(Me.optCondition_Quest0)
        Me.fraConditions_Quest.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraConditions_Quest.Location = New System.Drawing.Point(6, 236)
        Me.fraConditions_Quest.Name = "fraConditions_Quest"
        Me.fraConditions_Quest.Size = New System.Drawing.Size(376, 77)
        Me.fraConditions_Quest.TabIndex = 27
        Me.fraConditions_Quest.TabStop = False
        Me.fraConditions_Quest.Text = "Quest Conditions"
        '
        'DarkLabel20
        '
        Me.DarkLabel20.AutoSize = True
        Me.DarkLabel20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel20.Location = New System.Drawing.Point(123, 47)
        Me.DarkLabel20.Name = "DarkLabel20"
        Me.DarkLabel20.Size = New System.Drawing.Size(93, 13)
        Me.DarkLabel20.TabIndex = 5
        Me.DarkLabel20.Text = "Player is on task..."
        '
        'nudCondition_QuestTask
        '
        Me.nudCondition_QuestTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudCondition_QuestTask.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudCondition_QuestTask.Location = New System.Drawing.Point(233, 45)
        Me.nudCondition_QuestTask.Name = "nudCondition_QuestTask"
        Me.nudCondition_QuestTask.Size = New System.Drawing.Size(137, 20)
        Me.nudCondition_QuestTask.TabIndex = 4
        '
        'cmbCondition_General
        '
        Me.cmbCondition_General.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_General.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_General.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_General.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_General.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_General.FormattingEnabled = True
        Me.cmbCondition_General.Items.AddRange(New Object() {"Not Started", "Started", "Completed", "Can Start", "Can End"})
        Me.cmbCondition_General.Location = New System.Drawing.Point(233, 18)
        Me.cmbCondition_General.Name = "cmbCondition_General"
        Me.cmbCondition_General.Size = New System.Drawing.Size(137, 21)
        Me.cmbCondition_General.TabIndex = 3
        Me.cmbCondition_General.Text = Nothing
        '
        'DarkLabel19
        '
        Me.DarkLabel19.AutoSize = True
        Me.DarkLabel19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel19.Location = New System.Drawing.Point(123, 21)
        Me.DarkLabel19.Name = "DarkLabel19"
        Me.DarkLabel19.Size = New System.Drawing.Size(104, 13)
        Me.DarkLabel19.TabIndex = 2
        Me.DarkLabel19.Text = "If selected quest is..."
        '
        'optCondition_Quest1
        '
        Me.optCondition_Quest1.AutoSize = True
        Me.optCondition_Quest1.Location = New System.Drawing.Point(6, 45)
        Me.optCondition_Quest1.Name = "optCondition_Quest1"
        Me.optCondition_Quest1.Size = New System.Drawing.Size(49, 17)
        Me.optCondition_Quest1.TabIndex = 1
        Me.optCondition_Quest1.TabStop = True
        Me.optCondition_Quest1.Text = "Task"
        '
        'optCondition_Quest0
        '
        Me.optCondition_Quest0.AutoSize = True
        Me.optCondition_Quest0.Location = New System.Drawing.Point(6, 19)
        Me.optCondition_Quest0.Name = "optCondition_Quest0"
        Me.optCondition_Quest0.Size = New System.Drawing.Size(62, 17)
        Me.optCondition_Quest0.TabIndex = 0
        Me.optCondition_Quest0.TabStop = True
        Me.optCondition_Quest0.Text = "General"
        '
        'nudCondition_Quest
        '
        Me.nudCondition_Quest.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudCondition_Quest.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudCondition_Quest.Location = New System.Drawing.Point(262, 210)
        Me.nudCondition_Quest.Name = "nudCondition_Quest"
        Me.nudCondition_Quest.Size = New System.Drawing.Size(120, 20)
        Me.nudCondition_Quest.TabIndex = 26
        '
        'DarkLabel18
        '
        Me.DarkLabel18.AutoSize = True
        Me.DarkLabel18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel18.Location = New System.Drawing.Point(218, 213)
        Me.DarkLabel18.Name = "DarkLabel18"
        Me.DarkLabel18.Size = New System.Drawing.Size(38, 13)
        Me.DarkLabel18.TabIndex = 25
        Me.DarkLabel18.Text = "Quest:"
        '
        'optCondition7
        '
        Me.optCondition7.AutoSize = True
        Me.optCondition7.Location = New System.Drawing.Point(6, 210)
        Me.optCondition7.Name = "optCondition7"
        Me.optCondition7.Size = New System.Drawing.Size(86, 17)
        Me.optCondition7.TabIndex = 24
        Me.optCondition7.TabStop = True
        Me.optCondition7.Text = "Quest Status"
        '
        'cmbCondition_SelfSwitchCondition
        '
        Me.cmbCondition_SelfSwitchCondition.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_SelfSwitchCondition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_SelfSwitchCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_SelfSwitchCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_SelfSwitchCondition.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_SelfSwitchCondition.FormattingEnabled = True
        Me.cmbCondition_SelfSwitchCondition.Items.AddRange(New Object() {"False", "True"})
        Me.cmbCondition_SelfSwitchCondition.Location = New System.Drawing.Point(262, 183)
        Me.cmbCondition_SelfSwitchCondition.Name = "cmbCondition_SelfSwitchCondition"
        Me.cmbCondition_SelfSwitchCondition.Size = New System.Drawing.Size(121, 21)
        Me.cmbCondition_SelfSwitchCondition.TabIndex = 23
        Me.cmbCondition_SelfSwitchCondition.Text = Nothing
        '
        'DarkLabel17
        '
        Me.DarkLabel17.AutoSize = True
        Me.DarkLabel17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel17.Location = New System.Drawing.Point(234, 186)
        Me.DarkLabel17.Name = "DarkLabel17"
        Me.DarkLabel17.Size = New System.Drawing.Size(14, 13)
        Me.DarkLabel17.TabIndex = 22
        Me.DarkLabel17.Text = "is"
        '
        'cmbCondition_SelfSwitch
        '
        Me.cmbCondition_SelfSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_SelfSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_SelfSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_SelfSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_SelfSwitch.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_SelfSwitch.FormattingEnabled = True
        Me.cmbCondition_SelfSwitch.Location = New System.Drawing.Point(107, 183)
        Me.cmbCondition_SelfSwitch.Name = "cmbCondition_SelfSwitch"
        Me.cmbCondition_SelfSwitch.Size = New System.Drawing.Size(121, 21)
        Me.cmbCondition_SelfSwitch.TabIndex = 21
        Me.cmbCondition_SelfSwitch.Text = Nothing
        '
        'optCondition6
        '
        Me.optCondition6.AutoSize = True
        Me.optCondition6.Location = New System.Drawing.Point(6, 184)
        Me.optCondition6.Name = "optCondition6"
        Me.optCondition6.Size = New System.Drawing.Size(78, 17)
        Me.optCondition6.TabIndex = 20
        Me.optCondition6.TabStop = True
        Me.optCondition6.Text = "Self Switch"
        '
        'nudCondition_LevelAmount
        '
        Me.nudCondition_LevelAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudCondition_LevelAmount.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudCondition_LevelAmount.Location = New System.Drawing.Point(269, 157)
        Me.nudCondition_LevelAmount.Name = "nudCondition_LevelAmount"
        Me.nudCondition_LevelAmount.Size = New System.Drawing.Size(113, 20)
        Me.nudCondition_LevelAmount.TabIndex = 19
        '
        'optCondition5
        '
        Me.optCondition5.AutoSize = True
        Me.optCondition5.Location = New System.Drawing.Point(6, 157)
        Me.optCondition5.Name = "optCondition5"
        Me.optCondition5.Size = New System.Drawing.Size(61, 17)
        Me.optCondition5.TabIndex = 18
        Me.optCondition5.TabStop = True
        Me.optCondition5.Text = "Level is"
        '
        'cmbCondition_LevelCompare
        '
        Me.cmbCondition_LevelCompare.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_LevelCompare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_LevelCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_LevelCompare.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_LevelCompare.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_LevelCompare.FormattingEnabled = True
        Me.cmbCondition_LevelCompare.Items.AddRange(New Object() {"Equal To", "Great Than OrElse Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal"})
        Me.cmbCondition_LevelCompare.Location = New System.Drawing.Point(107, 156)
        Me.cmbCondition_LevelCompare.Name = "cmbCondition_LevelCompare"
        Me.cmbCondition_LevelCompare.Size = New System.Drawing.Size(156, 21)
        Me.cmbCondition_LevelCompare.TabIndex = 17
        Me.cmbCondition_LevelCompare.Text = Nothing
        '
        'cmbCondition_LearntSkill
        '
        Me.cmbCondition_LearntSkill.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_LearntSkill.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_LearntSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_LearntSkill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_LearntSkill.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_LearntSkill.FormattingEnabled = True
        Me.cmbCondition_LearntSkill.Location = New System.Drawing.Point(107, 129)
        Me.cmbCondition_LearntSkill.Name = "cmbCondition_LearntSkill"
        Me.cmbCondition_LearntSkill.Size = New System.Drawing.Size(276, 21)
        Me.cmbCondition_LearntSkill.TabIndex = 16
        Me.cmbCondition_LearntSkill.Text = Nothing
        '
        'optCondition4
        '
        Me.optCondition4.AutoSize = True
        Me.optCondition4.Location = New System.Drawing.Point(6, 130)
        Me.optCondition4.Name = "optCondition4"
        Me.optCondition4.Size = New System.Drawing.Size(79, 17)
        Me.optCondition4.TabIndex = 15
        Me.optCondition4.TabStop = True
        Me.optCondition4.Text = "Knows Skill"
        '
        'cmbCondition_ClassIs
        '
        Me.cmbCondition_ClassIs.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_ClassIs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_ClassIs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_ClassIs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_ClassIs.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_ClassIs.FormattingEnabled = True
        Me.cmbCondition_ClassIs.Location = New System.Drawing.Point(107, 102)
        Me.cmbCondition_ClassIs.Name = "cmbCondition_ClassIs"
        Me.cmbCondition_ClassIs.Size = New System.Drawing.Size(276, 21)
        Me.cmbCondition_ClassIs.TabIndex = 14
        Me.cmbCondition_ClassIs.Text = Nothing
        '
        'optCondition3
        '
        Me.optCondition3.AutoSize = True
        Me.optCondition3.Location = New System.Drawing.Point(6, 103)
        Me.optCondition3.Name = "optCondition3"
        Me.optCondition3.Size = New System.Drawing.Size(61, 17)
        Me.optCondition3.TabIndex = 13
        Me.optCondition3.TabStop = True
        Me.optCondition3.Text = "Class Is"
        '
        'nudCondition_HasItem
        '
        Me.nudCondition_HasItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudCondition_HasItem.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudCondition_HasItem.Location = New System.Drawing.Point(262, 76)
        Me.nudCondition_HasItem.Name = "nudCondition_HasItem"
        Me.nudCondition_HasItem.Size = New System.Drawing.Size(120, 20)
        Me.nudCondition_HasItem.TabIndex = 12
        '
        'DarkLabel16
        '
        Me.DarkLabel16.AutoSize = True
        Me.DarkLabel16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel16.Location = New System.Drawing.Point(234, 78)
        Me.DarkLabel16.Name = "DarkLabel16"
        Me.DarkLabel16.Size = New System.Drawing.Size(14, 13)
        Me.DarkLabel16.TabIndex = 11
        Me.DarkLabel16.Text = "X"
        '
        'cmbCondition_HasItem
        '
        Me.cmbCondition_HasItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_HasItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_HasItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_HasItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_HasItem.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_HasItem.FormattingEnabled = True
        Me.cmbCondition_HasItem.Location = New System.Drawing.Point(107, 75)
        Me.cmbCondition_HasItem.Name = "cmbCondition_HasItem"
        Me.cmbCondition_HasItem.Size = New System.Drawing.Size(121, 21)
        Me.cmbCondition_HasItem.TabIndex = 10
        Me.cmbCondition_HasItem.Text = Nothing
        '
        'optCondition2
        '
        Me.optCondition2.AutoSize = True
        Me.optCondition2.Location = New System.Drawing.Point(6, 76)
        Me.optCondition2.Name = "optCondition2"
        Me.optCondition2.Size = New System.Drawing.Size(67, 17)
        Me.optCondition2.TabIndex = 9
        Me.optCondition2.TabStop = True
        Me.optCondition2.Text = "Has Item"
        '
        'optCondition1
        '
        Me.optCondition1.AutoSize = True
        Me.optCondition1.Location = New System.Drawing.Point(6, 49)
        Me.optCondition1.Name = "optCondition1"
        Me.optCondition1.Size = New System.Drawing.Size(89, 17)
        Me.optCondition1.TabIndex = 8
        Me.optCondition1.TabStop = True
        Me.optCondition1.Text = "Player Switch"
        '
        'DarkLabel15
        '
        Me.DarkLabel15.AutoSize = True
        Me.DarkLabel15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel15.Location = New System.Drawing.Point(234, 51)
        Me.DarkLabel15.Name = "DarkLabel15"
        Me.DarkLabel15.Size = New System.Drawing.Size(14, 13)
        Me.DarkLabel15.TabIndex = 7
        Me.DarkLabel15.Text = "is"
        '
        'cmbCondtion_PlayerSwitchCondition
        '
        Me.cmbCondtion_PlayerSwitchCondition.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondtion_PlayerSwitchCondition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondtion_PlayerSwitchCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondtion_PlayerSwitchCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondtion_PlayerSwitchCondition.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondtion_PlayerSwitchCondition.FormattingEnabled = True
        Me.cmbCondtion_PlayerSwitchCondition.Items.AddRange(New Object() {"False", "True"})
        Me.cmbCondtion_PlayerSwitchCondition.Location = New System.Drawing.Point(262, 48)
        Me.cmbCondtion_PlayerSwitchCondition.Name = "cmbCondtion_PlayerSwitchCondition"
        Me.cmbCondtion_PlayerSwitchCondition.Size = New System.Drawing.Size(121, 21)
        Me.cmbCondtion_PlayerSwitchCondition.TabIndex = 6
        Me.cmbCondtion_PlayerSwitchCondition.Text = Nothing
        '
        'cmbCondition_PlayerSwitch
        '
        Me.cmbCondition_PlayerSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_PlayerSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_PlayerSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_PlayerSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_PlayerSwitch.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_PlayerSwitch.FormattingEnabled = True
        Me.cmbCondition_PlayerSwitch.Location = New System.Drawing.Point(107, 48)
        Me.cmbCondition_PlayerSwitch.Name = "cmbCondition_PlayerSwitch"
        Me.cmbCondition_PlayerSwitch.Size = New System.Drawing.Size(121, 21)
        Me.cmbCondition_PlayerSwitch.TabIndex = 5
        Me.cmbCondition_PlayerSwitch.Text = Nothing
        '
        'nudCondition_PlayerVarCondition
        '
        Me.nudCondition_PlayerVarCondition.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudCondition_PlayerVarCondition.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudCondition_PlayerVarCondition.Location = New System.Drawing.Point(335, 22)
        Me.nudCondition_PlayerVarCondition.Name = "nudCondition_PlayerVarCondition"
        Me.nudCondition_PlayerVarCondition.Size = New System.Drawing.Size(47, 20)
        Me.nudCondition_PlayerVarCondition.TabIndex = 4
        '
        'cmbCondition_PlayerVarCompare
        '
        Me.cmbCondition_PlayerVarCompare.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_PlayerVarCompare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_PlayerVarCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_PlayerVarCompare.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_PlayerVarCompare.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_PlayerVarCompare.FormattingEnabled = True
        Me.cmbCondition_PlayerVarCompare.Items.AddRange(New Object() {"Equal To", "Great Than OrElse Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal"})
        Me.cmbCondition_PlayerVarCompare.Location = New System.Drawing.Point(236, 21)
        Me.cmbCondition_PlayerVarCompare.Name = "cmbCondition_PlayerVarCompare"
        Me.cmbCondition_PlayerVarCompare.Size = New System.Drawing.Size(88, 21)
        Me.cmbCondition_PlayerVarCompare.TabIndex = 3
        Me.cmbCondition_PlayerVarCompare.Text = Nothing
        '
        'DarkLabel14
        '
        Me.DarkLabel14.AutoSize = True
        Me.DarkLabel14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel14.Location = New System.Drawing.Point(216, 24)
        Me.DarkLabel14.Name = "DarkLabel14"
        Me.DarkLabel14.Size = New System.Drawing.Size(14, 13)
        Me.DarkLabel14.TabIndex = 2
        Me.DarkLabel14.Text = "is"
        '
        'cmbCondition_PlayerVarIndex
        '
        Me.cmbCondition_PlayerVarIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_PlayerVarIndex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_PlayerVarIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_PlayerVarIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_PlayerVarIndex.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_PlayerVarIndex.FormattingEnabled = True
        Me.cmbCondition_PlayerVarIndex.Location = New System.Drawing.Point(107, 21)
        Me.cmbCondition_PlayerVarIndex.Name = "cmbCondition_PlayerVarIndex"
        Me.cmbCondition_PlayerVarIndex.Size = New System.Drawing.Size(103, 21)
        Me.cmbCondition_PlayerVarIndex.TabIndex = 1
        Me.cmbCondition_PlayerVarIndex.Text = Nothing
        '
        'optCondition0
        '
        Me.optCondition0.AutoSize = True
        Me.optCondition0.Location = New System.Drawing.Point(6, 22)
        Me.optCondition0.Name = "optCondition0"
        Me.optCondition0.Size = New System.Drawing.Size(95, 17)
        Me.optCondition0.TabIndex = 0
        Me.optCondition0.TabStop = True
        Me.optCondition0.Text = "Player Variable"
        '
        'fraPlayBGM
        '
        Me.fraPlayBGM.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlayBGM.Controls.Add(Me.btnPlayBgmOk)
        Me.fraPlayBGM.Controls.Add(Me.btnPlayBgmCancel)
        Me.fraPlayBGM.Controls.Add(Me.cmbPlayBGM)
        Me.fraPlayBGM.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlayBGM.Location = New System.Drawing.Point(401, 1)
        Me.fraPlayBGM.Name = "fraPlayBGM"
        Me.fraPlayBGM.Size = New System.Drawing.Size(246, 75)
        Me.fraPlayBGM.TabIndex = 21
        Me.fraPlayBGM.TabStop = False
        Me.fraPlayBGM.Text = "Play BGM"
        Me.fraPlayBGM.Visible = False
        '
        'btnPlayBgmOk
        '
        Me.btnPlayBgmOk.Location = New System.Drawing.Point(46, 46)
        Me.btnPlayBgmOk.Name = "btnPlayBgmOk"
        Me.btnPlayBgmOk.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPlayBgmOk.Size = New System.Drawing.Size(75, 23)
        Me.btnPlayBgmOk.TabIndex = 27
        Me.btnPlayBgmOk.Text = "Ok"
        '
        'btnPlayBgmCancel
        '
        Me.btnPlayBgmCancel.Location = New System.Drawing.Point(127, 46)
        Me.btnPlayBgmCancel.Name = "btnPlayBgmCancel"
        Me.btnPlayBgmCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnPlayBgmCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnPlayBgmCancel.TabIndex = 26
        Me.btnPlayBgmCancel.Text = "Cancel"
        '
        'cmbPlayBGM
        '
        Me.cmbPlayBGM.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbPlayBGM.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayBGM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlayBGM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPlayBGM.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbPlayBGM.FormattingEnabled = True
        Me.cmbPlayBGM.Location = New System.Drawing.Point(6, 19)
        Me.cmbPlayBGM.Name = "cmbPlayBGM"
        Me.cmbPlayBGM.Size = New System.Drawing.Size(233, 21)
        Me.cmbPlayBGM.TabIndex = 0
        Me.cmbPlayBGM.Text = Nothing
        '
        'pnlVariableSwitches
        '
        Me.pnlVariableSwitches.Controls.Add(Me.FraRenaming)
        Me.pnlVariableSwitches.Controls.Add(Me.fraLabeling)
        Me.pnlVariableSwitches.Location = New System.Drawing.Point(800, 201)
        Me.pnlVariableSwitches.Name = "pnlVariableSwitches"
        Me.pnlVariableSwitches.Size = New System.Drawing.Size(93, 91)
        Me.pnlVariableSwitches.TabIndex = 11
        '
        'FraRenaming
        '
        Me.FraRenaming.Controls.Add(Me.btnRename_Cancel)
        Me.FraRenaming.Controls.Add(Me.btnRename_Ok)
        Me.FraRenaming.Controls.Add(Me.fraRandom10)
        Me.FraRenaming.ForeColor = System.Drawing.Color.Gainsboro
        Me.FraRenaming.Location = New System.Drawing.Point(236, 429)
        Me.FraRenaming.Name = "FraRenaming"
        Me.FraRenaming.Size = New System.Drawing.Size(364, 143)
        Me.FraRenaming.TabIndex = 8
        Me.FraRenaming.TabStop = False
        Me.FraRenaming.Text = "Renaming Variable/Switch"
        Me.FraRenaming.Visible = False
        '
        'btnRename_Cancel
        '
        Me.btnRename_Cancel.ForeColor = System.Drawing.Color.Black
        Me.btnRename_Cancel.Location = New System.Drawing.Point(229, 102)
        Me.btnRename_Cancel.Name = "btnRename_Cancel"
        Me.btnRename_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.btnRename_Cancel.TabIndex = 2
        Me.btnRename_Cancel.Text = "Cancel"
        Me.btnRename_Cancel.UseVisualStyleBackColor = True
        '
        'btnRename_Ok
        '
        Me.btnRename_Ok.ForeColor = System.Drawing.Color.Black
        Me.btnRename_Ok.Location = New System.Drawing.Point(54, 102)
        Me.btnRename_Ok.Name = "btnRename_Ok"
        Me.btnRename_Ok.Size = New System.Drawing.Size(75, 23)
        Me.btnRename_Ok.TabIndex = 1
        Me.btnRename_Ok.Text = "Ok"
        Me.btnRename_Ok.UseVisualStyleBackColor = True
        '
        'fraRandom10
        '
        Me.fraRandom10.Controls.Add(Me.txtRename)
        Me.fraRandom10.Controls.Add(Me.lblEditing)
        Me.fraRandom10.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraRandom10.Location = New System.Drawing.Point(6, 19)
        Me.fraRandom10.Name = "fraRandom10"
        Me.fraRandom10.Size = New System.Drawing.Size(352, 77)
        Me.fraRandom10.TabIndex = 0
        Me.fraRandom10.TabStop = False
        Me.fraRandom10.Text = "Editing Variable/Switch"
        '
        'txtRename
        '
        Me.txtRename.Location = New System.Drawing.Point(6, 41)
        Me.txtRename.Name = "txtRename"
        Me.txtRename.Size = New System.Drawing.Size(340, 20)
        Me.txtRename.TabIndex = 1
        '
        'lblEditing
        '
        Me.lblEditing.AutoSize = True
        Me.lblEditing.Location = New System.Drawing.Point(3, 25)
        Me.lblEditing.Name = "lblEditing"
        Me.lblEditing.Size = New System.Drawing.Size(100, 13)
        Me.lblEditing.TabIndex = 0
        Me.lblEditing.Text = "Naming Variable #1"
        '
        'fraLabeling
        '
        Me.fraLabeling.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraLabeling.Controls.Add(Me.lstSwitches)
        Me.fraLabeling.Controls.Add(Me.lstVariables)
        Me.fraLabeling.Controls.Add(Me.btnLabel_Cancel)
        Me.fraLabeling.Controls.Add(Me.lblRandomLabel36)
        Me.fraLabeling.Controls.Add(Me.btnRenameVariable)
        Me.fraLabeling.Controls.Add(Me.lblRandomLabel25)
        Me.fraLabeling.Controls.Add(Me.btnRenameSwitch)
        Me.fraLabeling.Controls.Add(Me.btnLabel_Ok)
        Me.fraLabeling.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraLabeling.Location = New System.Drawing.Point(195, 29)
        Me.fraLabeling.Name = "fraLabeling"
        Me.fraLabeling.Size = New System.Drawing.Size(456, 387)
        Me.fraLabeling.TabIndex = 0
        Me.fraLabeling.TabStop = False
        Me.fraLabeling.Text = "Label Variables and  Switches   "
        '
        'lstSwitches
        '
        Me.lstSwitches.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstSwitches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstSwitches.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstSwitches.FormattingEnabled = True
        Me.lstSwitches.Location = New System.Drawing.Point(236, 39)
        Me.lstSwitches.Name = "lstSwitches"
        Me.lstSwitches.Size = New System.Drawing.Size(205, 288)
        Me.lstSwitches.TabIndex = 7
        '
        'lstVariables
        '
        Me.lstVariables.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstVariables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstVariables.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstVariables.FormattingEnabled = True
        Me.lstVariables.Location = New System.Drawing.Point(14, 39)
        Me.lstVariables.Name = "lstVariables"
        Me.lstVariables.Size = New System.Drawing.Size(205, 288)
        Me.lstVariables.TabIndex = 6
        '
        'btnLabel_Cancel
        '
        Me.btnLabel_Cancel.ForeColor = System.Drawing.Color.Black
        Me.btnLabel_Cancel.Location = New System.Drawing.Point(236, 341)
        Me.btnLabel_Cancel.Name = "btnLabel_Cancel"
        Me.btnLabel_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.btnLabel_Cancel.TabIndex = 12
        Me.btnLabel_Cancel.Text = "Cancel"
        Me.btnLabel_Cancel.UseVisualStyleBackColor = True
        '
        'lblRandomLabel36
        '
        Me.lblRandomLabel36.AutoSize = True
        Me.lblRandomLabel36.Location = New System.Drawing.Point(293, 23)
        Me.lblRandomLabel36.Name = "lblRandomLabel36"
        Me.lblRandomLabel36.Size = New System.Drawing.Size(82, 13)
        Me.lblRandomLabel36.TabIndex = 5
        Me.lblRandomLabel36.Text = "Player Switches"
        '
        'btnRenameVariable
        '
        Me.btnRenameVariable.ForeColor = System.Drawing.Color.Black
        Me.btnRenameVariable.Location = New System.Drawing.Point(14, 341)
        Me.btnRenameVariable.Name = "btnRenameVariable"
        Me.btnRenameVariable.Size = New System.Drawing.Size(106, 23)
        Me.btnRenameVariable.TabIndex = 9
        Me.btnRenameVariable.Text = "Rename Variable"
        Me.btnRenameVariable.UseVisualStyleBackColor = True
        '
        'lblRandomLabel25
        '
        Me.lblRandomLabel25.AutoSize = True
        Me.lblRandomLabel25.Location = New System.Drawing.Point(80, 21)
        Me.lblRandomLabel25.Name = "lblRandomLabel25"
        Me.lblRandomLabel25.Size = New System.Drawing.Size(82, 13)
        Me.lblRandomLabel25.TabIndex = 4
        Me.lblRandomLabel25.Text = "Player Variables"
        '
        'btnRenameSwitch
        '
        Me.btnRenameSwitch.ForeColor = System.Drawing.Color.Black
        Me.btnRenameSwitch.Location = New System.Drawing.Point(332, 341)
        Me.btnRenameSwitch.Name = "btnRenameSwitch"
        Me.btnRenameSwitch.Size = New System.Drawing.Size(109, 23)
        Me.btnRenameSwitch.TabIndex = 10
        Me.btnRenameSwitch.Text = "Rename Switch"
        Me.btnRenameSwitch.UseVisualStyleBackColor = True
        '
        'btnLabel_Ok
        '
        Me.btnLabel_Ok.ForeColor = System.Drawing.Color.Black
        Me.btnLabel_Ok.Location = New System.Drawing.Point(144, 341)
        Me.btnLabel_Ok.Name = "btnLabel_Ok"
        Me.btnLabel_Ok.Size = New System.Drawing.Size(75, 23)
        Me.btnLabel_Ok.TabIndex = 11
        Me.btnLabel_Ok.Text = "Ok"
        Me.btnLabel_Ok.UseVisualStyleBackColor = True
        '
        'optCondition9
        '
        Me.optCondition9.AutoSize = True
        Me.optCondition9.Location = New System.Drawing.Point(6, 346)
        Me.optCondition9.Name = "optCondition9"
        Me.optCondition9.Size = New System.Drawing.Size(95, 17)
        Me.optCondition9.TabIndex = 32
        Me.optCondition9.TabStop = True
        Me.optCondition9.Text = "Time of Day is:"
        '
        'cmbCondition_Time
        '
        Me.cmbCondition_Time.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCondition_Time.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cmbCondition_Time.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition_Time.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCondition_Time.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCondition_Time.FormattingEnabled = True
        Me.cmbCondition_Time.Items.AddRange(New Object() {"Day", "Night", "Dawn", "Dusk"})
        Me.cmbCondition_Time.Location = New System.Drawing.Point(239, 345)
        Me.cmbCondition_Time.Name = "cmbCondition_Time"
        Me.cmbCondition_Time.Size = New System.Drawing.Size(144, 21)
        Me.cmbCondition_Time.TabIndex = 33
        '
        'FrmEditor_Events
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1588, 614)
        Me.ControlBox = False
        Me.Controls.Add(Me.fraGraphic)
        Me.Controls.Add(Me.pnlVariableSwitches)
        Me.Controls.Add(Me.fraDialogue)
        Me.Controls.Add(Me.fraMoveRoute)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnLabeling)
        Me.Controls.Add(Me.DarkGroupBox6)
        Me.Controls.Add(Me.pnlTabPage)
        Me.Controls.Add(Me.tabPages)
        Me.Controls.Add(Me.fraPageSetUp)
        Me.ForeColor = System.Drawing.Color.Gainsboro
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmEditor_Events"
        Me.Text = "Event Editor"
        Me.fraPageSetUp.ResumeLayout(False)
        Me.fraPageSetUp.PerformLayout()
        Me.tabPages.ResumeLayout(False)
        Me.pnlTabPage.ResumeLayout(False)
        Me.fraCommands.ResumeLayout(False)
        Me.DarkGroupBox8.ResumeLayout(False)
        Me.DarkGroupBox7.ResumeLayout(False)
        Me.DarkGroupBox7.PerformLayout()
        Me.DarkGroupBox5.ResumeLayout(False)
        Me.DarkGroupBox4.ResumeLayout(False)
        Me.DarkGroupBox3.ResumeLayout(False)
        Me.DarkGroupBox3.PerformLayout()
        Me.DarkGroupBox2.ResumeLayout(False)
        CType(Me.picGraphic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DarkGroupBox1.ResumeLayout(False)
        Me.DarkGroupBox1.PerformLayout()
        CType(Me.nudPlayerVariable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DarkGroupBox6.ResumeLayout(False)
        Me.DarkGroupBox6.PerformLayout()
        Me.fraMoveRoute.ResumeLayout(False)
        Me.fraMoveRoute.PerformLayout()
        Me.DarkGroupBox10.ResumeLayout(False)
        Me.fraGraphic.ResumeLayout(False)
        Me.fraGraphic.PerformLayout()
        Me.pnlGraphicSel.ResumeLayout(False)
        CType(Me.picGraphicSel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudGraphic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraDialogue.ResumeLayout(False)
        Me.fraPlayAnimation.ResumeLayout(False)
        Me.fraPlayAnimation.PerformLayout()
        CType(Me.nudPlayAnimTileY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudPlayAnimTileX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraMoveRouteWait.ResumeLayout(False)
        Me.fraMoveRouteWait.PerformLayout()
        Me.fraCustomScript.ResumeLayout(False)
        Me.fraCustomScript.PerformLayout()
        CType(Me.nudCustomScript, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraSetWeather.ResumeLayout(False)
        Me.fraSetWeather.PerformLayout()
        CType(Me.nudWeatherIntensity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraSpawnNpc.ResumeLayout(False)
        Me.fraGiveExp.ResumeLayout(False)
        Me.fraGiveExp.PerformLayout()
        CType(Me.nudGiveExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraEndQuest.ResumeLayout(False)
        Me.fraSetAccess.ResumeLayout(False)
        Me.fraSetWait.ResumeLayout(False)
        Me.fraSetWait.PerformLayout()
        CType(Me.nudWaitAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraShowPic.ResumeLayout(False)
        Me.fraShowPic.PerformLayout()
        CType(Me.nudPicOffsetY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudPicOffsetX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudShowPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picShowPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraOpenShop.ResumeLayout(False)
        Me.fraChangeLevel.ResumeLayout(False)
        Me.fraChangeLevel.PerformLayout()
        CType(Me.nudChangeLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraChangeGender.ResumeLayout(False)
        Me.fraChangeGender.PerformLayout()
        Me.fraGoToLabel.ResumeLayout(False)
        Me.fraGoToLabel.PerformLayout()
        Me.fraHidePic.ResumeLayout(False)
        Me.fraHidePic.PerformLayout()
        CType(Me.nudHidePic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraBeginQuest.ResumeLayout(False)
        Me.fraBeginQuest.PerformLayout()
        Me.fraShowChoices.ResumeLayout(False)
        Me.fraShowChoices.PerformLayout()
        CType(Me.picShowChoicesFace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudShowChoicesFace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraPlayerVariable.ResumeLayout(False)
        Me.fraPlayerVariable.PerformLayout()
        CType(Me.nudVariableData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudVariableData4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudVariableData3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudVariableData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudVariableData0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraChangeSprite.ResumeLayout(False)
        Me.fraChangeSprite.PerformLayout()
        CType(Me.nudChangeSprite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picChangeSprite, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraSetSelfSwitch.ResumeLayout(False)
        Me.fraSetSelfSwitch.PerformLayout()
        Me.fraMapTint.ResumeLayout(False)
        Me.fraMapTint.PerformLayout()
        CType(Me.nudMapTintData3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMapTintData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMapTintData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMapTintData0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraShowChatBubble.ResumeLayout(False)
        Me.fraShowChatBubble.PerformLayout()
        Me.fraPlaySound.ResumeLayout(False)
        Me.fraChangePK.ResumeLayout(False)
        Me.fraCreateLabel.ResumeLayout(False)
        Me.fraCreateLabel.PerformLayout()
        Me.fraChangeClass.ResumeLayout(False)
        Me.fraChangeClass.PerformLayout()
        Me.fraChangeSkills.ResumeLayout(False)
        Me.fraChangeSkills.PerformLayout()
        Me.fraCompleteTask.ResumeLayout(False)
        Me.fraCompleteTask.PerformLayout()
        CType(Me.nudCompleteQuestTask, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraPlayerWarp.ResumeLayout(False)
        Me.fraPlayerWarp.PerformLayout()
        CType(Me.nudWPY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWPX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWPMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraSetFog.ResumeLayout(False)
        Me.fraSetFog.PerformLayout()
        CType(Me.nudFogData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudFogData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudFogData0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraShowText.ResumeLayout(False)
        Me.fraShowText.PerformLayout()
        CType(Me.picShowTextFace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudShowTextFace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAddText.ResumeLayout(False)
        Me.fraAddText.PerformLayout()
        Me.fraPlayerSwitch.ResumeLayout(False)
        Me.fraPlayerSwitch.PerformLayout()
        Me.fraChangeItems.ResumeLayout(False)
        Me.fraChangeItems.PerformLayout()
        CType(Me.nudChangeItemsAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraConditionalBranch.ResumeLayout(False)
        Me.fraConditionalBranch.PerformLayout()
        Me.fraConditions_Quest.ResumeLayout(False)
        Me.fraConditions_Quest.PerformLayout()
        CType(Me.nudCondition_QuestTask, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCondition_Quest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCondition_LevelAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCondition_HasItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCondition_PlayerVarCondition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraPlayBGM.ResumeLayout(False)
        Me.pnlVariableSwitches.ResumeLayout(False)
        Me.FraRenaming.ResumeLayout(False)
        Me.fraRandom10.ResumeLayout(False)
        Me.fraRandom10.PerformLayout()
        Me.fraLabeling.ResumeLayout(False)
        Me.fraLabeling.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tvCommands As TreeView
    Friend WithEvents fraPageSetUp As System.Windows.Forms.GroupBox
    Friend WithEvents tabPages As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents DarkLabel1 As System.Windows.Forms.Label
    Friend WithEvents btnNewPage As System.Windows.Forms.Button
    Friend WithEvents btnCopyPage As System.Windows.Forms.Button
    Friend WithEvents btnPastePage As System.Windows.Forms.Button
    Friend WithEvents btnClearPage As System.Windows.Forms.Button
    Friend WithEvents btnDeletePage As System.Windows.Forms.Button
    Friend WithEvents pnlTabPage As Panel
    Friend WithEvents DarkGroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkPlayerVar As System.Windows.Forms.CheckBox
    Friend WithEvents cmbPlayerVar As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel2 As System.Windows.Forms.Label
    Friend WithEvents cmbPlayervarCompare As System.Windows.Forms.ComboBox
    Friend WithEvents nudPlayerVariable As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkPlayerSwitch As System.Windows.Forms.CheckBox
    Friend WithEvents cmbPlayerSwitch As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPlayerSwitchCompare As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel3 As System.Windows.Forms.Label
    Friend WithEvents cmbHasItem As System.Windows.Forms.ComboBox
    Friend WithEvents chkHasItem As System.Windows.Forms.CheckBox
    Friend WithEvents cmbSelfSwitch As System.Windows.Forms.ComboBox
    Friend WithEvents chkSelfSwitch As System.Windows.Forms.CheckBox
    Friend WithEvents cmbSelfSwitchCompare As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel4 As System.Windows.Forms.Label
    Friend WithEvents DarkGroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents picGraphic As PictureBox
    Friend WithEvents DarkGroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkGlobal As System.Windows.Forms.CheckBox
    Friend WithEvents DarkLabel5 As System.Windows.Forms.Label
    Friend WithEvents cmbMoveType As System.Windows.Forms.ComboBox
    Friend WithEvents btnMoveRoute As System.Windows.Forms.Button
    Friend WithEvents DarkLabel6 As System.Windows.Forms.Label
    Friend WithEvents cmbMoveSpeed As System.Windows.Forms.ComboBox
    Friend WithEvents cmbMoveFreq As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel7 As System.Windows.Forms.Label
    Friend WithEvents DarkGroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPositioning As System.Windows.Forms.ComboBox
    Friend WithEvents DarkGroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbTrigger As System.Windows.Forms.ComboBox
    Friend WithEvents DarkGroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents chkWalkAnim As System.Windows.Forms.CheckBox
    Friend WithEvents chkDirFix As System.Windows.Forms.CheckBox
    Friend WithEvents chkWalkThrough As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowName As System.Windows.Forms.CheckBox
    Friend WithEvents DarkGroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbEventQuest As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel8 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel10 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel9 As System.Windows.Forms.Label
    Friend WithEvents lstCommands As ListBox
    Friend WithEvents DarkGroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddCommand As System.Windows.Forms.Button
    Friend WithEvents btnDeleteCommand As System.Windows.Forms.Button
    Friend WithEvents btnEditCommand As System.Windows.Forms.Button
    Friend WithEvents btnClearCommand As System.Windows.Forms.Button
    Friend WithEvents fraCommands As Panel
    Friend WithEvents btnLabeling As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancelCommand As System.Windows.Forms.Button
    Friend WithEvents fraMoveRoute As System.Windows.Forms.GroupBox
    Friend WithEvents cmbEvent As System.Windows.Forms.ComboBox
    Friend WithEvents lstMoveRoute As ListBox
    Friend WithEvents DarkGroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents lstvwMoveRoute As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents chkRepeatRoute As System.Windows.Forms.CheckBox
    Friend WithEvents chkIgnoreMove As System.Windows.Forms.CheckBox
    Friend WithEvents btnMoveRouteOk As System.Windows.Forms.Button
    Friend WithEvents btnMoveRouteCancel As System.Windows.Forms.Button
    Friend WithEvents fraGraphic As System.Windows.Forms.GroupBox
    Friend WithEvents DarkLabel11 As System.Windows.Forms.Label
    Friend WithEvents cmbGraphic As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel12 As System.Windows.Forms.Label
    Friend WithEvents nudGraphic As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel13 As System.Windows.Forms.Label
    Friend WithEvents picGraphicSel As PictureBox
    Friend WithEvents btnGraphicOk As System.Windows.Forms.Button
    Friend WithEvents btnGraphicCancel As System.Windows.Forms.Button
    Friend WithEvents fraDialogue As System.Windows.Forms.GroupBox
    Friend WithEvents fraConditionalBranch As System.Windows.Forms.GroupBox
    Friend WithEvents optCondition0 As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCondition_PlayerVarIndex As System.Windows.Forms.ComboBox
    Friend WithEvents nudCondition_PlayerVarCondition As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbCondition_PlayerVarCompare As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel14 As System.Windows.Forms.Label
    Friend WithEvents optCondition1 As System.Windows.Forms.RadioButton
    Friend WithEvents DarkLabel15 As System.Windows.Forms.Label
    Friend WithEvents cmbCondtion_PlayerSwitchCondition As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCondition_PlayerSwitch As System.Windows.Forms.ComboBox
    Friend WithEvents optCondition2 As System.Windows.Forms.RadioButton
    Friend WithEvents nudCondition_HasItem As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel16 As System.Windows.Forms.Label
    Friend WithEvents cmbCondition_HasItem As System.Windows.Forms.ComboBox
    Friend WithEvents optCondition3 As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCondition_ClassIs As System.Windows.Forms.ComboBox
    Friend WithEvents optCondition4 As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCondition_LearntSkill As System.Windows.Forms.ComboBox
    Friend WithEvents optCondition5 As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCondition_LevelCompare As System.Windows.Forms.ComboBox
    Friend WithEvents nudCondition_LevelAmount As System.Windows.Forms.NumericUpDown
    Friend WithEvents optCondition6 As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCondition_SelfSwitchCondition As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel17 As System.Windows.Forms.Label
    Friend WithEvents cmbCondition_SelfSwitch As System.Windows.Forms.ComboBox
    Friend WithEvents optCondition7 As System.Windows.Forms.RadioButton
    Friend WithEvents nudCondition_Quest As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel18 As System.Windows.Forms.Label
    Friend WithEvents fraConditions_Quest As System.Windows.Forms.GroupBox
    Friend WithEvents DarkLabel19 As System.Windows.Forms.Label
    Friend WithEvents optCondition_Quest1 As System.Windows.Forms.RadioButton
    Friend WithEvents optCondition_Quest0 As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCondition_General As System.Windows.Forms.ComboBox
    Friend WithEvents nudCondition_QuestTask As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel20 As System.Windows.Forms.Label
    Friend WithEvents optCondition8 As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCondition_Gender As System.Windows.Forms.ComboBox
    Friend WithEvents btnConditionalBranchOk As System.Windows.Forms.Button
    Friend WithEvents btnConditionalBranchCancel As System.Windows.Forms.Button
    Friend WithEvents fraChangeItems As System.Windows.Forms.GroupBox
    Friend WithEvents fraPlayerSwitch As System.Windows.Forms.GroupBox
    Friend WithEvents cmbChangeItemIndex As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel21 As System.Windows.Forms.Label
    Friend WithEvents optChangeItemSet As System.Windows.Forms.RadioButton
    Friend WithEvents optChangeItemRemove As System.Windows.Forms.RadioButton
    Friend WithEvents optChangeItemAdd As System.Windows.Forms.RadioButton
    Friend WithEvents nudChangeItemsAmount As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnChangeItemsOk As System.Windows.Forms.Button
    Friend WithEvents btnChangeItemsCancel As System.Windows.Forms.Button
    Friend WithEvents cmbSwitch As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel22 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel23 As System.Windows.Forms.Label
    Friend WithEvents cmbPlayerSwitchSet As System.Windows.Forms.ComboBox
    Friend WithEvents btnSetPlayerSwitchOk As System.Windows.Forms.Button
    Friend WithEvents btnSetPlayerswitchCancel As System.Windows.Forms.Button
    Friend WithEvents fraAddText As System.Windows.Forms.GroupBox
    Friend WithEvents txtAddText_Text As System.Windows.Forms.TextBox
    Friend WithEvents DarkLabel24 As System.Windows.Forms.Label
    Friend WithEvents optAddText_Player As System.Windows.Forms.RadioButton
    Friend WithEvents DarkLabel25 As System.Windows.Forms.Label
    Friend WithEvents optAddText_Map As System.Windows.Forms.RadioButton
    Friend WithEvents btnAddTextOk As System.Windows.Forms.Button
    Friend WithEvents btnAddTextCancel As System.Windows.Forms.Button
    Friend WithEvents optAddText_Global As System.Windows.Forms.RadioButton
    Friend WithEvents btnShowTextOk As System.Windows.Forms.Button
    Friend WithEvents btnShowTextCancel As System.Windows.Forms.Button
    Friend WithEvents nudShowTextFace As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel26 As System.Windows.Forms.Label
    Friend WithEvents txtShowText As System.Windows.Forms.TextBox
    Friend WithEvents picShowTextFace As PictureBox
    Friend WithEvents DarkLabel27 As System.Windows.Forms.Label
    Friend WithEvents fraShowText As System.Windows.Forms.GroupBox
    Friend WithEvents fraSetFog As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetFogOk As System.Windows.Forms.Button
    Friend WithEvents btnSetFogCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel30 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel29 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel28 As System.Windows.Forms.Label
    Friend WithEvents nudFogData2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudFogData1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudFogData0 As System.Windows.Forms.NumericUpDown
    Friend WithEvents fraPlayerWarp As System.Windows.Forms.GroupBox
    Friend WithEvents btnPlayerWarpOk As System.Windows.Forms.Button
    Friend WithEvents btnPlayerWarpCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel31 As System.Windows.Forms.Label
    Friend WithEvents cmbWarpPlayerDir As System.Windows.Forms.ComboBox
    Friend WithEvents nudWPY As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel32 As System.Windows.Forms.Label
    Friend WithEvents nudWPX As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel33 As System.Windows.Forms.Label
    Friend WithEvents nudWPMap As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel34 As System.Windows.Forms.Label
    Friend WithEvents fraCompleteTask As System.Windows.Forms.GroupBox
    Friend WithEvents btnCompleteQuestTaskOk As System.Windows.Forms.Button
    Friend WithEvents btnCompleteQuestTaskCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel35 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel36 As System.Windows.Forms.Label
    Friend WithEvents nudCompleteQuestTask As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbCompleteQuest As System.Windows.Forms.ComboBox
    Friend WithEvents fraPlayBGM As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPlayBGM As System.Windows.Forms.ComboBox
    Friend WithEvents btnPlayBgmOk As System.Windows.Forms.Button
    Friend WithEvents btnPlayBgmCancel As System.Windows.Forms.Button
    Friend WithEvents fraChangeSkills As System.Windows.Forms.GroupBox
    Friend WithEvents cmbChangeSkills As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel37 As System.Windows.Forms.Label
    Friend WithEvents optChangeSkillsAdd As System.Windows.Forms.RadioButton
    Friend WithEvents btnChangeSkillsOk As System.Windows.Forms.Button
    Friend WithEvents btnChangeSkillsCancel As System.Windows.Forms.Button
    Friend WithEvents optChangeSkillsRemove As System.Windows.Forms.RadioButton
    Friend WithEvents fraChangeClass As System.Windows.Forms.GroupBox
    Friend WithEvents cmbChangeClass As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel38 As System.Windows.Forms.Label
    Friend WithEvents btnChangeClassOk As System.Windows.Forms.Button
    Friend WithEvents btnChangeClassCancel As System.Windows.Forms.Button
    Friend WithEvents fraCreateLabel As System.Windows.Forms.GroupBox
    Friend WithEvents lblLabelName As System.Windows.Forms.Label
    Friend WithEvents txtLabelName As System.Windows.Forms.TextBox
    Friend WithEvents btnCreatelabelOk As System.Windows.Forms.Button
    Friend WithEvents btnCreatelabelCancel As System.Windows.Forms.Button
    Friend WithEvents fraChangePK As System.Windows.Forms.GroupBox
    Friend WithEvents btnChangePkOk As System.Windows.Forms.Button
    Friend WithEvents btnChangePkCancel As System.Windows.Forms.Button
    Friend WithEvents cmbSetPK As System.Windows.Forms.ComboBox
    Friend WithEvents fraPlaySound As System.Windows.Forms.GroupBox
    Friend WithEvents btnPlaySoundOk As System.Windows.Forms.Button
    Friend WithEvents btnPlaySoundCancel As System.Windows.Forms.Button
    Friend WithEvents cmbPlaySound As System.Windows.Forms.ComboBox
    Friend WithEvents fraShowChatBubble As System.Windows.Forms.GroupBox
    Friend WithEvents DarkLabel39 As System.Windows.Forms.Label
    Friend WithEvents txtChatbubbleText As System.Windows.Forms.TextBox
    Friend WithEvents DarkLabel40 As System.Windows.Forms.Label
    Friend WithEvents cmbChatBubbleTarget As System.Windows.Forms.ComboBox
    Friend WithEvents cmbChatBubbleTargetType As System.Windows.Forms.ComboBox
    Friend WithEvents btnShowChatBubbleOk As System.Windows.Forms.Button
    Friend WithEvents btnShowChatBubbleCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel41 As System.Windows.Forms.Label
    Friend WithEvents fraMapTint As System.Windows.Forms.GroupBox
    Friend WithEvents btnMapTintOk As System.Windows.Forms.Button
    Friend WithEvents btnMapTintCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel42 As System.Windows.Forms.Label
    Friend WithEvents nudMapTintData3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudMapTintData2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel43 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel44 As System.Windows.Forms.Label
    Friend WithEvents nudMapTintData1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudMapTintData0 As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel45 As System.Windows.Forms.Label
    Friend WithEvents fraSetSelfSwitch As System.Windows.Forms.GroupBox
    Friend WithEvents cmbSetSelfSwitch As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel46 As System.Windows.Forms.Label
    Friend WithEvents btnSelfswitchOk As System.Windows.Forms.Button
    Friend WithEvents btnSelfswitchCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel47 As System.Windows.Forms.Label
    Friend WithEvents cmbSetSelfSwitchTo As System.Windows.Forms.ComboBox
    Friend WithEvents fraChangeSprite As System.Windows.Forms.GroupBox
    Friend WithEvents picChangeSprite As PictureBox
    Friend WithEvents btnChangeSpriteOk As System.Windows.Forms.Button
    Friend WithEvents btnChangeSpriteCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel48 As System.Windows.Forms.Label
    Friend WithEvents nudChangeSprite As System.Windows.Forms.NumericUpDown
    Friend WithEvents fraPlayerVariable As System.Windows.Forms.GroupBox
    Friend WithEvents cmbVariable As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel49 As System.Windows.Forms.Label
    Friend WithEvents optVariableAction0 As System.Windows.Forms.RadioButton
    Friend WithEvents optVariableAction1 As System.Windows.Forms.RadioButton
    Friend WithEvents nudVariableData1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudVariableData0 As System.Windows.Forms.NumericUpDown
    Friend WithEvents optVariableAction3 As System.Windows.Forms.RadioButton
    Friend WithEvents nudVariableData3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents optVariableAction2 As System.Windows.Forms.RadioButton
    Friend WithEvents btnPlayerVarOk As System.Windows.Forms.Button
    Friend WithEvents btnPlayerVarCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel51 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel50 As System.Windows.Forms.Label
    Friend WithEvents nudVariableData4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudVariableData2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents fraShowChoices As System.Windows.Forms.GroupBox
    Friend WithEvents DarkLabel52 As System.Windows.Forms.Label
    Friend WithEvents txtChoicePrompt As System.Windows.Forms.TextBox
    Friend WithEvents btnShowChoicesOk As System.Windows.Forms.Button
    Friend WithEvents picShowChoicesFace As PictureBox
    Friend WithEvents btnShowChoicesCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel53 As System.Windows.Forms.Label
    Friend WithEvents nudShowChoicesFace As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel56 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel57 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel55 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel54 As System.Windows.Forms.Label
    Friend WithEvents txtChoices4 As System.Windows.Forms.TextBox
    Friend WithEvents txtChoices3 As System.Windows.Forms.TextBox
    Friend WithEvents txtChoices2 As System.Windows.Forms.TextBox
    Friend WithEvents txtChoices1 As System.Windows.Forms.TextBox
    Friend WithEvents fraBeginQuest As System.Windows.Forms.GroupBox
    Friend WithEvents cmbBeginQuest As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel58 As System.Windows.Forms.Label
    Friend WithEvents btnBeginQuestOk As System.Windows.Forms.Button
    Friend WithEvents btnBeginQuestCancel As System.Windows.Forms.Button
    Friend WithEvents fraHidePic As System.Windows.Forms.GroupBox
    Friend WithEvents nudHidePic As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel59 As System.Windows.Forms.Label
    Friend WithEvents btnHidePicOk As System.Windows.Forms.Button
    Friend WithEvents btnHidePicCancel As System.Windows.Forms.Button
    Friend WithEvents fraGoToLabel As System.Windows.Forms.GroupBox
    Friend WithEvents txtGotoLabel As System.Windows.Forms.TextBox
    Friend WithEvents DarkLabel60 As System.Windows.Forms.Label
    Friend WithEvents btnGoToLabelOk As System.Windows.Forms.Button
    Friend WithEvents btnGoToLabelCancel As System.Windows.Forms.Button
    Friend WithEvents fraPlayAnimation As System.Windows.Forms.GroupBox
    Friend WithEvents DarkLabel61 As System.Windows.Forms.Label
    Friend WithEvents cmbPlayAnim As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel62 As System.Windows.Forms.Label
    Friend WithEvents cmbAnimTargetType As System.Windows.Forms.ComboBox
    Friend WithEvents nudPlayAnimTileY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudPlayAnimTileX As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbPlayAnimEvent As System.Windows.Forms.ComboBox
    Friend WithEvents btnPlayAnimationOk As System.Windows.Forms.Button
    Friend WithEvents btnPlayAnimationCancel As System.Windows.Forms.Button
    Friend WithEvents lblPlayAnimY As System.Windows.Forms.Label
    Friend WithEvents lblPlayAnimX As System.Windows.Forms.Label
    Friend WithEvents fraChangeGender As System.Windows.Forms.GroupBox
    Friend WithEvents btnChangeGenderOk As System.Windows.Forms.Button
    Friend WithEvents btnChangeGenderCancel As System.Windows.Forms.Button
    Friend WithEvents optChangeSexFemale As System.Windows.Forms.RadioButton
    Friend WithEvents optChangeSexMale As System.Windows.Forms.RadioButton
    Friend WithEvents fraChangeLevel As System.Windows.Forms.GroupBox
    Friend WithEvents btnChangeLevelOk As System.Windows.Forms.Button
    Friend WithEvents btnChangeLevelCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel65 As System.Windows.Forms.Label
    Friend WithEvents nudChangeLevel As System.Windows.Forms.NumericUpDown
    Friend WithEvents fraOpenShop As System.Windows.Forms.GroupBox
    Friend WithEvents cmbOpenShop As System.Windows.Forms.ComboBox
    Friend WithEvents btnOpenShopOk As System.Windows.Forms.Button
    Friend WithEvents btnOpenShopCancel As System.Windows.Forms.Button
    Friend WithEvents fraShowPic As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPicIndex As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel66 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel67 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel68 As System.Windows.Forms.Label
    Friend WithEvents nudPicOffsetY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudPicOffsetX As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel69 As System.Windows.Forms.Label
    Friend WithEvents cmbPicLoc As System.Windows.Forms.ComboBox
    Friend WithEvents nudShowPicture As System.Windows.Forms.NumericUpDown
    Friend WithEvents picShowPic As PictureBox
    Friend WithEvents btnShowPicOk As System.Windows.Forms.Button
    Friend WithEvents btnShowPicCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel71 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel70 As System.Windows.Forms.Label
    Friend WithEvents fraSetWait As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetWaitOk As System.Windows.Forms.Button
    Friend WithEvents btnSetWaitCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel74 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel72 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel73 As System.Windows.Forms.Label
    Friend WithEvents nudWaitAmount As System.Windows.Forms.NumericUpDown
    Friend WithEvents fraSetAccess As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetAccessOk As System.Windows.Forms.Button
    Friend WithEvents btnSetAccessCancel As System.Windows.Forms.Button
    Friend WithEvents cmbSetAccess As System.Windows.Forms.ComboBox
    Friend WithEvents fraEndQuest As System.Windows.Forms.GroupBox
    Friend WithEvents btnEndQuestOk As System.Windows.Forms.Button
    Friend WithEvents btnEndQuestCancel As System.Windows.Forms.Button
    Friend WithEvents cmbEndQuest As System.Windows.Forms.ComboBox
    Friend WithEvents fraSetWeather As System.Windows.Forms.GroupBox
    Friend WithEvents DarkLabel75 As System.Windows.Forms.Label
    Friend WithEvents CmbWeather As System.Windows.Forms.ComboBox
    Friend WithEvents btnSetWeatherOk As System.Windows.Forms.Button
    Friend WithEvents btnSetWeatherCancel As System.Windows.Forms.Button
    Friend WithEvents DarkLabel76 As System.Windows.Forms.Label
    Friend WithEvents nudWeatherIntensity As System.Windows.Forms.NumericUpDown
    Friend WithEvents fraGiveExp As System.Windows.Forms.GroupBox
    Friend WithEvents DarkLabel77 As System.Windows.Forms.Label
    Friend WithEvents fraSpawnNpc As System.Windows.Forms.GroupBox
    Friend WithEvents cmbSpawnNpc As System.Windows.Forms.ComboBox
    Friend WithEvents btnGiveExpOk As System.Windows.Forms.Button
    Friend WithEvents btnGiveExpCancel As System.Windows.Forms.Button
    Friend WithEvents nudGiveExp As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnSpawnNpcOk As System.Windows.Forms.Button
    Friend WithEvents btnSpawnNpcCancel As System.Windows.Forms.Button
    Friend WithEvents fraCustomScript As System.Windows.Forms.GroupBox
    Friend WithEvents nudCustomScript As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel78 As System.Windows.Forms.Label
    Friend WithEvents btnCustomScriptCancel As System.Windows.Forms.Button
    Friend WithEvents btnCustomScriptOk As System.Windows.Forms.Button
    Friend WithEvents fraMoveRouteWait As System.Windows.Forms.GroupBox
    Friend WithEvents btnMoveWaitCancel As System.Windows.Forms.Button
    Friend WithEvents btnMoveWaitOk As System.Windows.Forms.Button
    Friend WithEvents DarkLabel79 As System.Windows.Forms.Label
    Friend WithEvents cmbMoveWait As System.Windows.Forms.ComboBox
    Friend WithEvents pnlVariableSwitches As Panel
    Friend WithEvents fraLabeling As System.Windows.Forms.GroupBox
    Friend WithEvents lstSwitches As ListBox
    Friend WithEvents lstVariables As ListBox
    Friend WithEvents lblRandomLabel36 As Label
    Friend WithEvents lblRandomLabel25 As Label
    Friend WithEvents FraRenaming As GroupBox
    Friend WithEvents btnRename_Cancel As Button
    Friend WithEvents btnRename_Ok As Button
    Friend WithEvents fraRandom10 As GroupBox
    Friend WithEvents txtRename As TextBox
    Friend WithEvents lblEditing As Label
    Friend WithEvents btnLabel_Cancel As Button
    Friend WithEvents btnRenameVariable As Button
    Friend WithEvents btnRenameSwitch As Button
    Friend WithEvents btnLabel_Ok As Button
    Friend WithEvents pnlGraphicSel As Panel
    Friend WithEvents cmbCondition_Time As System.Windows.Forms.ComboBox
    Friend WithEvents optCondition9 As System.Windows.Forms.RadioButton
End Class
