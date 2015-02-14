<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GDI
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
        Me.components = New System.ComponentModel.Container()
        Me.updateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.lblFPS = New System.Windows.Forms.Label()
        Me.lblDegrees = New System.Windows.Forms.Label()
        Me.picGravity = New System.Windows.Forms.PictureBox()
        Me.lblVx = New System.Windows.Forms.Label()
        Me.lblVy = New System.Windows.Forms.Label()
        Me.lblUpdates = New System.Windows.Forms.Label()
        CType(Me.picGravity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'updateTimer
        '
        Me.updateTimer.Enabled = True
        Me.updateTimer.Interval = 1
        '
        'lblFPS
        '
        Me.lblFPS.AutoSize = True
        Me.lblFPS.Location = New System.Drawing.Point(17, 16)
        Me.lblFPS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFPS.Name = "lblFPS"
        Me.lblFPS.Size = New System.Drawing.Size(49, 16)
        Me.lblFPS.TabIndex = 0
        Me.lblFPS.Text = "Label1"
        '
        'lblDegrees
        '
        Me.lblDegrees.AutoSize = True
        Me.lblDegrees.Location = New System.Drawing.Point(17, 49)
        Me.lblDegrees.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDegrees.Name = "lblDegrees"
        Me.lblDegrees.Size = New System.Drawing.Size(49, 16)
        Me.lblDegrees.TabIndex = 1
        Me.lblDegrees.Text = "Label2"
        '
        'picGravity
        '
        Me.picGravity.BackColor = System.Drawing.Color.Transparent
        Me.picGravity.Image = Global.asteroids.My.Resources.Resources.gravity_well
        Me.picGravity.Location = New System.Drawing.Point(751, 316)
        Me.picGravity.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.picGravity.Name = "picGravity"
        Me.picGravity.Size = New System.Drawing.Size(480, 443)
        Me.picGravity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picGravity.TabIndex = 2
        Me.picGravity.TabStop = False
        '
        'lblVx
        '
        Me.lblVx.AutoSize = True
        Me.lblVx.Location = New System.Drawing.Point(16, 81)
        Me.lblVx.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVx.Name = "lblVx"
        Me.lblVx.Size = New System.Drawing.Size(21, 16)
        Me.lblVx.TabIndex = 3
        Me.lblVx.Text = "vx"
        '
        'lblVy
        '
        Me.lblVy.AutoSize = True
        Me.lblVy.Location = New System.Drawing.Point(17, 112)
        Me.lblVy.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVy.Name = "lblVy"
        Me.lblVy.Size = New System.Drawing.Size(22, 16)
        Me.lblVy.TabIndex = 4
        Me.lblVy.Text = "vy"
        '
        'lblUpdates
        '
        Me.lblUpdates.AutoSize = True
        Me.lblUpdates.Location = New System.Drawing.Point(16, 146)
        Me.lblUpdates.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUpdates.Name = "lblUpdates"
        Me.lblUpdates.Size = New System.Drawing.Size(23, 16)
        Me.lblUpdates.TabIndex = 5
        Me.lblUpdates.Text = "....."
        '
        'GDI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1424, 807)
        Me.Controls.Add(Me.lblUpdates)
        Me.Controls.Add(Me.lblVy)
        Me.Controls.Add(Me.lblVx)
        Me.Controls.Add(Me.picGravity)
        Me.Controls.Add(Me.lblDegrees)
        Me.Controls.Add(Me.lblFPS)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "GDI"
        Me.Text = "GDI"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.picGravity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents updateTimer As System.Windows.Forms.Timer
    Friend WithEvents lblFPS As System.Windows.Forms.Label
    Friend WithEvents lblDegrees As System.Windows.Forms.Label
    Friend WithEvents picGravity As System.Windows.Forms.PictureBox
    Friend WithEvents lblVx As System.Windows.Forms.Label
    Friend WithEvents lblVy As System.Windows.Forms.Label
    Friend WithEvents lblUpdates As System.Windows.Forms.Label
End Class
