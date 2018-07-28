<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMapEditor
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsCurFps = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PicTileset = New System.Windows.Forms.PictureBox()
        Me.PicScreen = New System.Windows.Forms.PictureBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PicTileset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicScreen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Splitter1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.StatusStrip1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PicTileset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.PicScreen)
        Me.SplitContainer1.Size = New System.Drawing.Size(1146, 456)
        Me.SplitContainer1.SplitterDistance = 367
        Me.SplitContainer1.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 341)
        Me.Splitter1.MinSize = 0
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(367, 93)
        Me.Splitter1.TabIndex = 2
        Me.Splitter1.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsCurFps})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 434)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(367, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tsCurFps
        '
        Me.tsCurFps.Name = "tsCurFps"
        Me.tsCurFps.Size = New System.Drawing.Size(120, 17)
        Me.tsCurFps.Text = "ToolStripStatusLabel1"
        '
        'PicTileset
        '
        Me.PicTileset.BackColor = System.Drawing.Color.Black
        Me.PicTileset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PicTileset.Location = New System.Drawing.Point(0, 0)
        Me.PicTileset.Name = "PicTileset"
        Me.PicTileset.Size = New System.Drawing.Size(367, 456)
        Me.PicTileset.TabIndex = 0
        Me.PicTileset.TabStop = False
        '
        'PicScreen
        '
        Me.PicScreen.BackColor = System.Drawing.Color.Black
        Me.PicScreen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PicScreen.Location = New System.Drawing.Point(0, 0)
        Me.PicScreen.Name = "PicScreen"
        Me.PicScreen.Size = New System.Drawing.Size(775, 456)
        Me.PicScreen.TabIndex = 0
        Me.PicScreen.TabStop = False
        '
        'FrmMapEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1146, 456)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMapEditor"
        Me.Text = "Map Editor"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PicTileset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicScreen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents PicTileset As PictureBox
    Friend WithEvents PicScreen As PictureBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents tsCurFps As ToolStripStatusLabel
    Friend WithEvents Splitter1 As Splitter
End Class
