<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLauncher : Inherits DragableBorderlessForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tmrNews = New System.Windows.Forms.Timer(Me.components)
        Me.lblNews = New System.Windows.Forms.Label()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.btnPlay = New ColorButton()
        Me.btnClose = New ColorButton()
        Me.btnMinimize = New ColorButton()
        Me.barDownload = New ColorProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrNews
        '
        Me.tmrNews.Enabled = True
        Me.tmrNews.Interval = 400
        '
        'lblNews
        '
        Me.lblNews.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNews.BackColor = System.Drawing.Color.Transparent
        Me.lblNews.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNews.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNews.ForeColor = System.Drawing.Color.White
        Me.lblNews.Location = New System.Drawing.Point(20, 150)
        Me.lblNews.Name = "lblNews"
        Me.lblNews.Size = New System.Drawing.Size(760, 300)
        Me.lblNews.TabIndex = 5
        Me.lblNews.Text = "Fetching News..."
        Me.lblNews.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picLogo
        '
        Me.picLogo.BackColor = System.Drawing.Color.Transparent
        Me.picLogo.BackgroundImage = Global.Engine.My.Resources.Resources.Logo
        Me.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picLogo.Location = New System.Drawing.Point(250, 20)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(300, 104)
        Me.picLogo.TabIndex = 2
        Me.picLogo.TabStop = False
        '
        'btnPlay
        '
        Me.btnPlay.BackColor = System.Drawing.Color.Black
        Me.btnPlay.Enabled = False
        Me.btnPlay.Font = New System.Drawing.Font("Arial Black", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlay.Location = New System.Drawing.Point(250, 530)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(300, 50)
        Me.btnPlay.TabIndex = 6
        Me.btnPlay.Text = "Play"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(758, 20)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(22, 22)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "X"
        '
        'btnMinimize
        '
        Me.btnMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMinimize.BackColor = System.Drawing.Color.Black
        Me.btnMinimize.Location = New System.Drawing.Point(730, 20)
        Me.btnMinimize.Name = "btnMinimize"
        Me.btnMinimize.Size = New System.Drawing.Size(22, 22)
        Me.btnMinimize.TabIndex = 8
        Me.btnMinimize.Text = "_"
        '
        'barDownload
        '
        Me.barDownload.BackColor = System.Drawing.Color.Black
        Me.barDownload.BorderColor = System.Drawing.Color.DimGray
        Me.barDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.barDownload.Location = New System.Drawing.Point(40, 500)
        Me.barDownload.Maximum = 100
        Me.barDownload.Minimum = 0
        Me.barDownload.Name = "barDownload"
        Me.barDownload.Size = New System.Drawing.Size(720, 23)
        Me.barDownload.StepCount = 10
        Me.barDownload.TabIndex = 9
        Me.barDownload.Value = 50
        Me.barDownload.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Location = New System.Drawing.Point(40, 450)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(720, 47)
        Me.lblStatus.TabIndex = 10
        Me.lblStatus.Text = "[Status Here]"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblStatus.Visible = False
        '
        'frmLauncher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.Engine.My.Resources.Resources.Background
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.barDownload)
        Me.Controls.Add(Me.btnMinimize)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPlay)
        Me.Controls.Add(Me.lblNews)
        Me.Controls.Add(Me.picLogo)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Gray
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MoveableBottom = 300
        Me.MoveableLeft = 400
        Me.MoveableRight = 400
        Me.MoveableTop = 300
        Me.Name = "frmLauncher"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Project Unlimited"
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tmrNews As Timer
    Friend WithEvents picLogo As PictureBox
    Friend WithEvents lblNews As Label
    Friend WithEvents btnPlay As ColorButton
    Friend WithEvents btnClose As ColorButton
    Friend WithEvents btnMinimize As ColorButton
    Friend WithEvents barDownload As ColorProgressBar
    Friend WithEvents lblStatus As Label
End Class
