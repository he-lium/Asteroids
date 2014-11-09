Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

Public Class Form1
    Const NUM_ASTEROIDS As Integer = 0 '10
    Const MAX_SPEED As Integer = 15
    Const ACCELERATION As Double = 0.1
    Const TORQUE As Double = 0.05
    Const STARTING_ASTEROID_SPEED As Integer = 20
    Const MAX_ASTEROID_SPEED As Integer = 40
    Const MAX_ASTEROID_SIZE As Integer = 100
    Const GRAVITY As Integer = 5000
    Const MAX_GRAVITY As Integer = 0

    Private fps As Integer = 0
    Private direction As Double = Math.PI * 0.5
    Private degrees As Integer = 90
    Dim upkey = False, leftkey = False, rightkey = False
    Private asteroids(NUM_ASTEROIDS) As asteroid
    Private spaceship As asteroid

    Private Structure asteroid
        Public x As Double
        Public y As Double
        Public vx As Double
        Public vy As Double
        Public size As Integer
        Public picture As System.Windows.Forms.PictureBox
    End Structure

    Private Function truemod(ByVal x As Integer, ByVal y As Integer) As Integer
        Return ((x Mod y) + y) Mod y
    End Function

    Private Function floatmod(ByVal x As Double, ByVal y As Integer) As Double
        Return ((x Mod y) + y) Mod y
    End Function

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Up Then
            upkey = True
        End If
        If e.KeyCode = Keys.Left Then
            leftkey = True
        End If
        If e.KeyCode = Keys.Right Then
            rightkey = True
        End If
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If leftkey Then
            direction += Math.PI * TORQUE / 2
        End If
        If rightkey Then
            direction -= Math.PI * TORQUE / 2
        End If
        degrees = truemod(direction * (180 / Math.PI), 360)
        Label1.Text = degrees
        If upkey Then
            spaceship.vy -= Math.Sin(direction) * ACCELERATION
            spaceship.vx += Math.Cos(direction) * ACCELERATION
        End If

        apply_gravity(spaceship, MAX_SPEED)
        For i = 0 To NUM_ASTEROIDS - 1
            apply_gravity(asteroids(i), MAX_ASTEROID_SPEED)
        Next
        move_asteroid(spaceship)
        spaceship.picture.Image = RotateImg(My.Resources.spaceship, Convert.ToSingle(truemod(90 - degrees, 360)))
        checkGravityCollision()
        Label3.Text = "vx: " + FormatNumber(spaceship.vx, 2)
        Label5.Text = "vy: " + FormatNumber(spaceship.vy, 2)
        For i = 0 To NUM_ASTEROIDS - 1
            move_asteroid(asteroids(i))
        Next
        fps += 1

    End Sub

    Private Sub apply_gravity(ByRef tmp As asteroid, ByVal maxSpeed As Integer)
        Dim dist As Double = GRAVITY / Math.Max(((tmp.y - ((PictureBox2.Top + PictureBox2.Bottom) / 2)) ^ 2) + ((tmp.x - ((PictureBox2.Right + PictureBox2.Left) / 2)) ^ 2), MAX_GRAVITY ^ 2)
        Dim deg As Double = Math.Atan2((tmp.y - ((PictureBox2.Top + PictureBox2.Bottom) / 2)), (tmp.x - ((PictureBox2.Right + PictureBox2.Left) / 2)))
        tmp.vy -= Math.Sin(deg) * dist
        tmp.vx -= Math.Cos(deg) * dist
        tmp.vx = Math.Min(maxSpeed, tmp.vx)
        tmp.vy = Math.Min(maxSpeed, tmp.vy)
        tmp.vx = Math.Max(-maxSpeed, tmp.vx)
        tmp.vy = Math.Max(-maxSpeed, tmp.vy)
    End Sub

    Private Sub move_asteroid(ByRef tmp As asteroid)
        tmp.y += tmp.vy
        tmp.y = floatmod(tmp.y, Me.Bottom - Me.Top)
        tmp.x += tmp.vx
        tmp.x = floatmod(tmp.x, Me.Right - Me.Left)
        tmp.picture.Left = tmp.x - tmp.picture.Width / 2
        tmp.picture.Top = tmp.y - tmp.picture.Height / 2
    End Sub

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Up Then
            upkey = False
        End If
        If e.KeyCode = Keys.Left Then
            leftkey = False
        End If
        If e.KeyCode = Keys.Right Then
            rightkey = False
        End If
    End Sub

    Private Function make_asteroid(ByVal size As Integer) As asteroid
        Dim asteroid1 As asteroid = New asteroid
        asteroid1.vx = Math.Floor(Rnd() * (STARTING_ASTEROID_SPEED + STARTING_ASTEROID_SPEED)) - STARTING_ASTEROID_SPEED
        asteroid1.vy = Math.Floor(Rnd() * (STARTING_ASTEROID_SPEED + STARTING_ASTEROID_SPEED)) - STARTING_ASTEROID_SPEED
        asteroid1.size = size
        asteroid1.picture = New PictureBox
        asteroid1.picture.BackColor = Color.HotPink
        asteroid1.picture.Top = Math.Floor((Rnd() * Me.Bottom - Me.Top))
        asteroid1.y = (asteroid1.picture.Top + asteroid1.picture.Top) / 2
        asteroid1.picture.Left = Math.Floor((Rnd() * Me.Right - Me.Left))
        asteroid1.x = (asteroid1.picture.Right + asteroid1.picture.Left) / 2
        asteroid1.picture.Size = New Size(size, size)
        Me.Controls.Add(asteroid1.picture)
        Return asteroid1
    End Function

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        spaceship = New asteroid
        spaceship.picture = PictureBox1
        spaceship.x = (spaceship.picture.Right + spaceship.picture.Left) / 2
        spaceship.y = (spaceship.picture.Top + spaceship.picture.Bottom) / 2
        spaceship.size = 0
        spaceship.vx = 0
        spaceship.vy = 0
        Randomize()
        For i = 0 To NUM_ASTEROIDS - 1
            asteroids(i) = make_asteroid(MAX_ASTEROID_SIZE)
        Next
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label2.Text = fps
        fps = 0
    End Sub

    'Source: the internet, will find later
    Public Function RotateImg(ByVal bmpimage As Bitmap, ByVal angle As Single) As Bitmap
        Dim w As Integer = bmpimage.Width
        Dim h As Integer = bmpimage.Height
        Dim pf As PixelFormat = Nothing
        pf = bmpimage.PixelFormat
        Dim tempImg As New Bitmap(w, h, pf)
        Dim g As Graphics = Graphics.FromImage(tempImg)
        g.DrawImageUnscaled(bmpimage, 1, 1)
        g.Dispose()
        Dim path As New GraphicsPath()
        path.AddRectangle(New RectangleF(0.0F, 0.0F, w, h))
        Dim mtrx As New Matrix()
        'Using System.Drawing.Drawing2D.Matrix class

        mtrx.Rotate(angle)
        Dim rct As RectangleF = path.GetBounds(mtrx)
        Dim newImg As New Bitmap(Convert.ToInt32(rct.Width), Convert.ToInt32(rct.Height), pf)
        g = Graphics.FromImage(newImg)
        g.TranslateTransform(-rct.X, -rct.Y)
        g.RotateTransform(angle)
        g.InterpolationMode = InterpolationMode.HighQualityBilinear
        g.DrawImageUnscaled(tempImg, 0, 0)
        g.Dispose()
        tempImg.Dispose()
        Return newImg
    End Function

    Private Sub checkGravityCollision()
        If spaceship.x > PictureBox2.Left + 20 And spaceship.x < PictureBox2.Left + PictureBox2.Width - 20 Then
            If spaceship.y > PictureBox2.Top + 20 And spaceship.y < PictureBox2.Top + PictureBox2.Height - 20 Then
                If spaceship.vx > 14 Or spaceship.vy > 14 Then
                    'Timer1.Stop()
                    'Timer2.Stop()
                    Label4.Text = "fell into gravity well"
                    PictureBox2.BackColor = Color.Red
                    'End If
                End If

            End If
        End If
    End Sub
End Class
