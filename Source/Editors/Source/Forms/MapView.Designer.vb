<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MapEditorView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MapEditorView))
        Me.rsMap = New SFML.UI.SfControl()
        Me.SuspendLayout()
        '
        'rsMap
        '
        Me.rsMap.AutoDraw = False
        Me.rsMap.AutoDrawInterval = 32
        Me.rsMap.BackColor = System.Drawing.Color.CornflowerBlue
        Me.rsMap.BackgroundImage = CType(resources.GetObject("rsMap.BackgroundImage"), System.Drawing.Image)
        Me.rsMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.rsMap.Location = New System.Drawing.Point(0, 1)
        Me.rsMap.Name = "rsMap"
        Me.rsMap.Size = New System.Drawing.Size(96, 96)
        Me.rsMap.TabIndex = 1
        '
        'MapEditorView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.rsMap)
        Me.Name = "MapEditorView"
        Me.Text = "MapView"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents rsMap As SFML.UI.SfControl
End Class
