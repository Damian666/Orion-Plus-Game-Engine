<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShop
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmShop))
        Me.DarkGroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstIndex = New System.Windows.Forms.ListBox()
        Me.DarkGroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.DarkGroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnDeleteTrade = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.nudCostValue = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel8 = New System.Windows.Forms.Label()
        Me.cmbCostItem = New System.Windows.Forms.ComboBox()
        Me.DarkLabel7 = New System.Windows.Forms.Label()
        Me.nudItemValue = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel6 = New System.Windows.Forms.Label()
        Me.cmbItem = New System.Windows.Forms.ComboBox()
        Me.DarkLabel5 = New System.Windows.Forms.Label()
        Me.lstTradeItem = New System.Windows.Forms.ListBox()
        Me.DarkLabel4 = New System.Windows.Forms.Label()
        Me.nudBuy = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel3 = New System.Windows.Forms.Label()
        Me.nudFace = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel2 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.DarkLabel1 = New System.Windows.Forms.Label()
        Me.picFace = New System.Windows.Forms.PictureBox()
        Me.DarkGroupBox1.SuspendLayout()
        Me.DarkGroupBox2.SuspendLayout()
        Me.DarkGroupBox3.SuspendLayout()
        CType(Me.nudCostValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudItemValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudBuy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudFace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DarkGroupBox1
        '
        Me.DarkGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox1.Controls.Add(Me.lstIndex)
        Me.DarkGroupBox1.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.DarkGroupBox1.Name = "DarkGroupBox1"
        Me.DarkGroupBox1.Size = New System.Drawing.Size(209, 398)
        Me.DarkGroupBox1.TabIndex = 0
        Me.DarkGroupBox1.TabStop = False
        Me.DarkGroupBox1.Text = "Shop List"
        '
        'lstIndex
        '
        Me.lstIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstIndex.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstIndex.FormattingEnabled = True
        Me.lstIndex.Location = New System.Drawing.Point(6, 19)
        Me.lstIndex.Name = "lstIndex"
        Me.lstIndex.Size = New System.Drawing.Size(196, 366)
        Me.lstIndex.TabIndex = 1
        '
        'DarkGroupBox2
        '
        Me.DarkGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox2.Controls.Add(Me.btnCancel)
        Me.DarkGroupBox2.Controls.Add(Me.btnDelete)
        Me.DarkGroupBox2.Controls.Add(Me.btnSave)
        Me.DarkGroupBox2.Controls.Add(Me.DarkGroupBox3)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel4)
        Me.DarkGroupBox2.Controls.Add(Me.nudBuy)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel3)
        Me.DarkGroupBox2.Controls.Add(Me.nudFace)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel2)
        Me.DarkGroupBox2.Controls.Add(Me.txtName)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel1)
        Me.DarkGroupBox2.Controls.Add(Me.picFace)
        Me.DarkGroupBox2.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox2.Location = New System.Drawing.Point(218, 3)
        Me.DarkGroupBox2.Name = "DarkGroupBox2"
        Me.DarkGroupBox2.Size = New System.Drawing.Size(414, 398)
        Me.DarkGroupBox2.TabIndex = 1
        Me.DarkGroupBox2.TabStop = False
        Me.DarkGroupBox2.Text = "Shop Properties"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(333, 365)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 55
        Me.btnCancel.Text = "Cancel"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(170, 365)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Padding = New System.Windows.Forms.Padding(5)
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 54
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(6, 365)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 53
        Me.btnSave.Text = "Save"
        '
        'DarkGroupBox3
        '
        Me.DarkGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox3.Controls.Add(Me.btnDeleteTrade)
        Me.DarkGroupBox3.Controls.Add(Me.btnUpdate)
        Me.DarkGroupBox3.Controls.Add(Me.nudCostValue)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel8)
        Me.DarkGroupBox3.Controls.Add(Me.cmbCostItem)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel7)
        Me.DarkGroupBox3.Controls.Add(Me.nudItemValue)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel6)
        Me.DarkGroupBox3.Controls.Add(Me.cmbItem)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel5)
        Me.DarkGroupBox3.Controls.Add(Me.lstTradeItem)
        Me.DarkGroupBox3.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox3.Location = New System.Drawing.Point(6, 121)
        Me.DarkGroupBox3.Name = "DarkGroupBox3"
        Me.DarkGroupBox3.Size = New System.Drawing.Size(401, 238)
        Me.DarkGroupBox3.TabIndex = 52
        Me.DarkGroupBox3.TabStop = False
        Me.DarkGroupBox3.Text = "Items the Shop Sells"
        '
        'btnDeleteTrade
        '
        Me.btnDeleteTrade.Location = New System.Drawing.Point(203, 211)
        Me.btnDeleteTrade.Name = "btnDeleteTrade"
        Me.btnDeleteTrade.Padding = New System.Windows.Forms.Padding(5)
        Me.btnDeleteTrade.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteTrade.TabIndex = 53
        Me.btnDeleteTrade.Text = "Delete"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(122, 211)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Padding = New System.Windows.Forms.Padding(5)
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 52
        Me.btnUpdate.Text = "Update"
        '
        'nudCostValue
        '
        Me.nudCostValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudCostValue.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudCostValue.Location = New System.Drawing.Point(297, 184)
        Me.nudCostValue.Name = "nudCostValue"
        Me.nudCostValue.Size = New System.Drawing.Size(98, 20)
        Me.nudCostValue.TabIndex = 51
        Me.nudCostValue.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'DarkLabel8
        '
        Me.DarkLabel8.AutoSize = True
        Me.DarkLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel8.Location = New System.Drawing.Point(245, 186)
        Me.DarkLabel8.Name = "DarkLabel8"
        Me.DarkLabel8.Size = New System.Drawing.Size(46, 13)
        Me.DarkLabel8.TabIndex = 50
        Me.DarkLabel8.Text = "Amount:"
        '
        'cmbCostItem
        '
        Me.cmbCostItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbCostItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCostItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCostItem.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbCostItem.FormattingEnabled = True
        Me.cmbCostItem.Location = New System.Drawing.Point(74, 184)
        Me.cmbCostItem.Name = "cmbCostItem"
        Me.cmbCostItem.Size = New System.Drawing.Size(165, 21)
        Me.cmbCostItem.TabIndex = 49
        Me.cmbCostItem.Text = Nothing
        '
        'DarkLabel7
        '
        Me.DarkLabel7.AutoSize = True
        Me.DarkLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel7.Location = New System.Drawing.Point(6, 187)
        Me.DarkLabel7.Name = "DarkLabel7"
        Me.DarkLabel7.Size = New System.Drawing.Size(54, 13)
        Me.DarkLabel7.TabIndex = 48
        Me.DarkLabel7.Text = "Item Cost:"
        '
        'nudItemValue
        '
        Me.nudItemValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudItemValue.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudItemValue.Location = New System.Drawing.Point(297, 158)
        Me.nudItemValue.Name = "nudItemValue"
        Me.nudItemValue.Size = New System.Drawing.Size(98, 20)
        Me.nudItemValue.TabIndex = 47
        Me.nudItemValue.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'DarkLabel6
        '
        Me.DarkLabel6.AutoSize = True
        Me.DarkLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel6.Location = New System.Drawing.Point(245, 160)
        Me.DarkLabel6.Name = "DarkLabel6"
        Me.DarkLabel6.Size = New System.Drawing.Size(46, 13)
        Me.DarkLabel6.TabIndex = 46
        Me.DarkLabel6.Text = "Amount:"
        '
        'cmbItem
        '
        Me.cmbItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cmbItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbItem.ForeColor = System.Drawing.Color.Gainsboro
        Me.cmbItem.FormattingEnabled = True
        Me.cmbItem.Location = New System.Drawing.Point(74, 157)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(165, 21)
        Me.cmbItem.TabIndex = 45
        Me.cmbItem.Text = Nothing
        '
        'DarkLabel5
        '
        Me.DarkLabel5.AutoSize = True
        Me.DarkLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel5.Location = New System.Drawing.Point(6, 160)
        Me.DarkLabel5.Name = "DarkLabel5"
        Me.DarkLabel5.Size = New System.Drawing.Size(62, 13)
        Me.DarkLabel5.TabIndex = 44
        Me.DarkLabel5.Text = "Item to Sell:"
        '
        'lstTradeItem
        '
        Me.lstTradeItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstTradeItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstTradeItem.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstTradeItem.FormattingEnabled = True
        Me.lstTradeItem.Items.AddRange(New Object() {"1.", "2.", "3.", "4.", "5.", "6.", "7.", "8."})
        Me.lstTradeItem.Location = New System.Drawing.Point(6, 19)
        Me.lstTradeItem.Name = "lstTradeItem"
        Me.lstTradeItem.Size = New System.Drawing.Size(389, 132)
        Me.lstTradeItem.TabIndex = 43
        '
        'DarkLabel4
        '
        Me.DarkLabel4.AutoSize = True
        Me.DarkLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel4.Location = New System.Drawing.Point(279, 87)
        Me.DarkLabel4.Name = "DarkLabel4"
        Me.DarkLabel4.Size = New System.Drawing.Size(98, 13)
        Me.DarkLabel4.TabIndex = 51
        Me.DarkLabel4.Text = "% of the Item Value"
        '
        'nudBuy
        '
        Me.nudBuy.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudBuy.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudBuy.Location = New System.Drawing.Point(193, 85)
        Me.nudBuy.Name = "nudBuy"
        Me.nudBuy.Size = New System.Drawing.Size(80, 20)
        Me.nudBuy.TabIndex = 50
        Me.nudBuy.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'DarkLabel3
        '
        Me.DarkLabel3.AutoSize = True
        Me.DarkLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel3.Location = New System.Drawing.Point(108, 87)
        Me.DarkLabel3.Name = "DarkLabel3"
        Me.DarkLabel3.Size = New System.Drawing.Size(79, 13)
        Me.DarkLabel3.TabIndex = 49
        Me.DarkLabel3.Text = "BuyBack Rate:"
        '
        'nudFace
        '
        Me.nudFace.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudFace.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudFace.Location = New System.Drawing.Point(180, 51)
        Me.nudFace.Name = "nudFace"
        Me.nudFace.Size = New System.Drawing.Size(93, 20)
        Me.nudFace.TabIndex = 48
        Me.nudFace.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'DarkLabel2
        '
        Me.DarkLabel2.AutoSize = True
        Me.DarkLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel2.Location = New System.Drawing.Point(108, 53)
        Me.DarkLabel2.Name = "DarkLabel2"
        Me.DarkLabel2.Size = New System.Drawing.Size(34, 13)
        Me.DarkLabel2.TabIndex = 47
        Me.DarkLabel2.Text = "Face:"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(180, 19)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(227, 20)
        Me.txtName.TabIndex = 46
        '
        'DarkLabel1
        '
        Me.DarkLabel1.AutoSize = True
        Me.DarkLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel1.Location = New System.Drawing.Point(108, 21)
        Me.DarkLabel1.Name = "DarkLabel1"
        Me.DarkLabel1.Size = New System.Drawing.Size(66, 13)
        Me.DarkLabel1.TabIndex = 45
        Me.DarkLabel1.Text = "Shop Name:"
        '
        'picFace
        '
        Me.picFace.BackColor = System.Drawing.Color.Black
        Me.picFace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picFace.Location = New System.Drawing.Point(6, 19)
        Me.picFace.Name = "picFace"
        Me.picFace.Size = New System.Drawing.Size(96, 96)
        Me.picFace.TabIndex = 44
        Me.picFace.TabStop = False
        '
        'frmShop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(638, 408)
        Me.ControlBox = False
        Me.Controls.Add(Me.DarkGroupBox2)
        Me.Controls.Add(Me.DarkGroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmShop"
        Me.Text = "Shop Editor"
        Me.DarkGroupBox1.ResumeLayout(False)
        Me.DarkGroupBox2.ResumeLayout(False)
        Me.DarkGroupBox2.PerformLayout()
        Me.DarkGroupBox3.ResumeLayout(False)
        Me.DarkGroupBox3.PerformLayout()
        CType(Me.nudCostValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudItemValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudBuy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudFace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DarkGroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstIndex As ListBox
    Friend WithEvents DarkGroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents DarkLabel1 As System.Windows.Forms.Label
    Friend WithEvents picFace As PictureBox
    Friend WithEvents nudFace As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel2 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel4 As System.Windows.Forms.Label
    Friend WithEvents nudBuy As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel3 As System.Windows.Forms.Label
    Friend WithEvents DarkGroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lstTradeItem As ListBox
    Friend WithEvents cmbItem As System.Windows.Forms.ComboBox
    Friend WithEvents DarkLabel5 As System.Windows.Forms.Label
    Friend WithEvents nudItemValue As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel6 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel7 As System.Windows.Forms.Label
    Friend WithEvents cmbCostItem As System.Windows.Forms.ComboBox
    Friend WithEvents nudCostValue As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel8 As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnDeleteTrade As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
End Class
