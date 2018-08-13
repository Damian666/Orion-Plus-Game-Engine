Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProjectile
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
        Me.DarkGroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstIndex = New System.Windows.Forms.ListBox()
        Me.DarkGroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DarkLabel5 = New System.Windows.Forms.Label()
        Me.DarkLabel4 = New System.Windows.Forms.Label()
        Me.nudDamage = New System.Windows.Forms.NumericUpDown()
        Me.nudSpeed = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel3 = New System.Windows.Forms.Label()
        Me.nudRange = New System.Windows.Forms.NumericUpDown()
        Me.nudPic = New System.Windows.Forms.NumericUpDown()
        Me.DarkLabel2 = New System.Windows.Forms.Label()
        Me.picProjectile = New System.Windows.Forms.PictureBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.DarkLabel1 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.DarkGroupBox1.SuspendLayout()
        Me.DarkGroupBox2.SuspendLayout()
        CType(Me.nudDamage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picProjectile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DarkGroupBox1
        '
        Me.DarkGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox1.Controls.Add(Me.lstIndex)
        Me.DarkGroupBox1.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.DarkGroupBox1.Name = "DarkGroupBox1"
        Me.DarkGroupBox1.Size = New System.Drawing.Size(188, 312)
        Me.DarkGroupBox1.TabIndex = 0
        Me.DarkGroupBox1.TabStop = False
        Me.DarkGroupBox1.Text = "Projectile List"
        '
        'lstIndex
        '
        Me.lstIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstIndex.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstIndex.FormattingEnabled = True
        Me.lstIndex.Location = New System.Drawing.Point(6, 17)
        Me.lstIndex.Name = "lstIndex"
        Me.lstIndex.Size = New System.Drawing.Size(176, 288)
        Me.lstIndex.TabIndex = 1
        '
        'DarkGroupBox2
        '
        Me.DarkGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel5)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel4)
        Me.DarkGroupBox2.Controls.Add(Me.nudDamage)
        Me.DarkGroupBox2.Controls.Add(Me.nudSpeed)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel3)
        Me.DarkGroupBox2.Controls.Add(Me.nudRange)
        Me.DarkGroupBox2.Controls.Add(Me.nudPic)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel2)
        Me.DarkGroupBox2.Controls.Add(Me.picProjectile)
        Me.DarkGroupBox2.Controls.Add(Me.txtName)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel1)
        Me.DarkGroupBox2.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox2.Location = New System.Drawing.Point(197, 3)
        Me.DarkGroupBox2.Name = "DarkGroupBox2"
        Me.DarkGroupBox2.Size = New System.Drawing.Size(249, 273)
        Me.DarkGroupBox2.TabIndex = 1
        Me.DarkGroupBox2.TabStop = False
        Me.DarkGroupBox2.Text = "Projectile Properties"
        '
        'DarkLabel5
        '
        Me.DarkLabel5.AutoSize = True
        Me.DarkLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel5.Location = New System.Drawing.Point(6, 195)
        Me.DarkLabel5.Name = "DarkLabel5"
        Me.DarkLabel5.Size = New System.Drawing.Size(99, 13)
        Me.DarkLabel5.TabIndex = 11
        Me.DarkLabel5.Text = "Additional Damage:"
        '
        'DarkLabel4
        '
        Me.DarkLabel4.AutoSize = True
        Me.DarkLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel4.Location = New System.Drawing.Point(6, 169)
        Me.DarkLabel4.Name = "DarkLabel4"
        Me.DarkLabel4.Size = New System.Drawing.Size(41, 13)
        Me.DarkLabel4.TabIndex = 10
        Me.DarkLabel4.Text = "Speed:"
        '
        'nudDamage
        '
        Me.nudDamage.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudDamage.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudDamage.Location = New System.Drawing.Point(119, 193)
        Me.nudDamage.Name = "nudDamage"
        Me.nudDamage.Size = New System.Drawing.Size(120, 20)
        Me.nudDamage.TabIndex = 9
        '
        'nudSpeed
        '
        Me.nudSpeed.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudSpeed.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudSpeed.Location = New System.Drawing.Point(119, 167)
        Me.nudSpeed.Name = "nudSpeed"
        Me.nudSpeed.Size = New System.Drawing.Size(120, 20)
        Me.nudSpeed.TabIndex = 8
        '
        'DarkLabel3
        '
        Me.DarkLabel3.AutoSize = True
        Me.DarkLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel3.Location = New System.Drawing.Point(6, 143)
        Me.DarkLabel3.Name = "DarkLabel3"
        Me.DarkLabel3.Size = New System.Drawing.Size(42, 13)
        Me.DarkLabel3.TabIndex = 7
        Me.DarkLabel3.Text = "Range:"
        '
        'nudRange
        '
        Me.nudRange.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudRange.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudRange.Location = New System.Drawing.Point(119, 141)
        Me.nudRange.Name = "nudRange"
        Me.nudRange.Size = New System.Drawing.Size(120, 20)
        Me.nudRange.TabIndex = 6
        '
        'nudPic
        '
        Me.nudPic.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.nudPic.ForeColor = System.Drawing.Color.Gainsboro
        Me.nudPic.Location = New System.Drawing.Point(119, 115)
        Me.nudPic.Name = "nudPic"
        Me.nudPic.Size = New System.Drawing.Size(120, 20)
        Me.nudPic.TabIndex = 5
        '
        'DarkLabel2
        '
        Me.DarkLabel2.AutoSize = True
        Me.DarkLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel2.Location = New System.Drawing.Point(6, 117)
        Me.DarkLabel2.Name = "DarkLabel2"
        Me.DarkLabel2.Size = New System.Drawing.Size(43, 13)
        Me.DarkLabel2.TabIndex = 4
        Me.DarkLabel2.Text = "Picture:"
        '
        'picProjectile
        '
        Me.picProjectile.BackColor = System.Drawing.Color.Black
        Me.picProjectile.Location = New System.Drawing.Point(9, 45)
        Me.picProjectile.Name = "picProjectile"
        Me.picProjectile.Size = New System.Drawing.Size(230, 64)
        Me.picProjectile.TabIndex = 3
        Me.picProjectile.TabStop = False
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(96, 19)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(143, 20)
        Me.txtName.TabIndex = 1
        '
        'DarkLabel1
        '
        Me.DarkLabel1.AutoSize = True
        Me.DarkLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel1.Location = New System.Drawing.Point(6, 21)
        Me.DarkLabel1.Name = "DarkLabel1"
        Me.DarkLabel1.Size = New System.Drawing.Size(84, 13)
        Me.DarkLabel1.TabIndex = 0
        Me.DarkLabel1.Text = "Projectile Name:"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(197, 291)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Padding = New System.Windows.Forms.Padding(5)
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(371, 291)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Padding = New System.Windows.Forms.Padding(5)
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'frmEditor_Projectile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(452, 319)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.DarkGroupBox2)
        Me.Controls.Add(Me.DarkGroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmEditor_Projectile"
        Me.Text = "Projectile Editor"
        Me.DarkGroupBox1.ResumeLayout(False)
        Me.DarkGroupBox2.ResumeLayout(False)
        Me.DarkGroupBox2.PerformLayout()
        CType(Me.nudDamage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudSpeed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picProjectile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DarkGroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstIndex As ListBox
    Friend WithEvents DarkGroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents DarkLabel1 As System.Windows.Forms.Label
    Friend WithEvents picProjectile As PictureBox
    Friend WithEvents nudRange As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudPic As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel2 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel3 As System.Windows.Forms.Label
    Friend WithEvents nudDamage As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudSpeed As System.Windows.Forms.NumericUpDown
    Friend WithEvents DarkLabel4 As System.Windows.Forms.Label
    Friend WithEvents DarkLabel5 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
