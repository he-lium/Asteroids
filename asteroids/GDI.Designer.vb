﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.lblVx = New System.Windows.Forms.Label()
        Me.lblVy = New System.Windows.Forms.Label()
        Me.lblUpdates = New System.Windows.Forms.Label()
        Me.nextLevelTimer = New System.Windows.Forms.Timer(Me.components)
        Me.crashTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblLives = New System.Windows.Forms.Label()
        Me.lblScore = New System.Windows.Forms.Label()
        Me.panelMainMenu = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.picGravity = New System.Windows.Forms.PictureBox()
        Me.btnSound = New System.Windows.Forms.Button()
        Me.panelMainMenu.SuspendLayout()
        CType(Me.picGravity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'updateTimer
        '
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
        'nextLevelTimer
        '
        Me.nextLevelTimer.Interval = 1400
        '
        'crashTimer
        '
        Me.crashTimer.Interval = 1400
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft New Tai Lue", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(168, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(304, 46)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Press Esc to pause"
        '
        'lblLives
        '
        Me.lblLives.AutoSize = True
        Me.lblLives.BackColor = System.Drawing.Color.Transparent
        Me.lblLives.Font = New System.Drawing.Font("Microsoft New Tai Lue", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLives.ForeColor = System.Drawing.Color.White
        Me.lblLives.Location = New System.Drawing.Point(168, 65)
        Me.lblLives.Name = "lblLives"
        Me.lblLives.Size = New System.Drawing.Size(198, 46)
        Me.lblLives.TabIndex = 7
        Me.lblLives.Text = "Lives Left: 2"
        '
        'lblScore
        '
        Me.lblScore.AutoSize = True
        Me.lblScore.BackColor = System.Drawing.Color.Transparent
        Me.lblScore.Font = New System.Drawing.Font("Microsoft New Tai Lue", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScore.ForeColor = System.Drawing.Color.White
        Me.lblScore.Location = New System.Drawing.Point(168, 111)
        Me.lblScore.Name = "lblScore"
        Me.lblScore.Size = New System.Drawing.Size(143, 46)
        Me.lblScore.TabIndex = 8
        Me.lblScore.Text = "Score: 0"
        '
        'panelMainMenu
        '
        Me.panelMainMenu.BackColor = System.Drawing.Color.Black
        Me.panelMainMenu.BackgroundImage = Global.asteroids.My.Resources.Resources.Image04
        Me.panelMainMenu.Controls.Add(Me.btnSound)
        Me.panelMainMenu.Controls.Add(Me.Button3)
        Me.panelMainMenu.Controls.Add(Me.Button2)
        Me.panelMainMenu.Controls.Add(Me.Button1)
        Me.panelMainMenu.Controls.Add(Me.Label2)
        Me.panelMainMenu.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.panelMainMenu.Location = New System.Drawing.Point(123, 263)
        Me.panelMainMenu.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.panelMainMenu.Name = "panelMainMenu"
        Me.panelMainMenu.Size = New System.Drawing.Size(732, 461)
        Me.panelMainMenu.TabIndex = 9
        '
        'Button3
        '
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.128!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(152, 287)
        Me.Button3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(405, 62)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Quit"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.128!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(152, 210)
        Me.Button2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(405, 62)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Restart"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.128!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(152, 135)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(405, 62)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Play"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.GhostWhite
        Me.Label2.Location = New System.Drawing.Point(237, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(249, 61)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Asteroids"
        '
        'picGravity
        '
        Me.picGravity.BackColor = System.Drawing.Color.Transparent
        Me.picGravity.Image = Global.asteroids.My.Resources.Resources.gravity_well
        Me.picGravity.Location = New System.Drawing.Point(751, 316)
        Me.picGravity.Margin = New System.Windows.Forms.Padding(4)
        Me.picGravity.Name = "picGravity"
        Me.picGravity.Size = New System.Drawing.Size(480, 443)
        Me.picGravity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picGravity.TabIndex = 2
        Me.picGravity.TabStop = False
        '
        'btnSound
        '
        Me.btnSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSound.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSound.Location = New System.Drawing.Point(152, 367)
        Me.btnSound.Name = "btnSound"
        Me.btnSound.Size = New System.Drawing.Size(405, 62)
        Me.btnSound.TabIndex = 4
        Me.btnSound.Text = "Sound On"
        Me.btnSound.UseVisualStyleBackColor = True
        '
        'GDI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Navy
        Me.ClientSize = New System.Drawing.Size(1424, 807)
        Me.Controls.Add(Me.panelMainMenu)
        Me.Controls.Add(Me.lblScore)
        Me.Controls.Add(Me.lblLives)
        Me.Controls.Add(Me.Label1)
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
        Me.panelMainMenu.ResumeLayout(False)
        Me.panelMainMenu.PerformLayout()
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
    Friend WithEvents nextLevelTimer As System.Windows.Forms.Timer
    Friend WithEvents crashTimer As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblLives As System.Windows.Forms.Label
    Friend WithEvents lblScore As System.Windows.Forms.Label
    Friend WithEvents panelMainMenu As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnSound As System.Windows.Forms.Button
End Class
