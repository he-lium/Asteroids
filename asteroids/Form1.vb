Public Class Form1
    Const NUM_ASTEROIDS As Integer = 0
    Const MAX_SPEED As Integer = 100
    Const ACCELERATION As Double = 0.5
    Const TORQUE As Double = 0.05
    Const MIN_ASTEROID_SPEED As Integer = -20
    Const MAX_ASTEROID_SPEED As Integer = 20
    Const MAX_ASTEROID_SIZE As Integer = 100
    Const GRAVITY As Integer = 10000

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

    Private Sub Form1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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


    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If leftkey Then
            direction += Math.PI * TORQUE
        End If
        If rightkey Then
            direction -= Math.PI * TORQUE
        End If
        degrees = truemod(direction * (180 / Math.PI), 360)
        Label1.Text = degrees
        If upkey Then
            spaceship.vy -= Math.Sin(direction) * ACCELERATION
            spaceship.vx += Math.Cos(direction) * ACCELERATION
        End If
        Dim dist As Double = GRAVITY / Math.Max(((spaceship.y - ((PictureBox2.Top + PictureBox2.Bottom) / 2)) ^ 2) + ((spaceship.x - ((PictureBox2.Right + PictureBox2.Left) / 2)) ^ 2), 1)
        Dim deg As Double = Math.Atan2((spaceship.y - ((PictureBox2.Top + PictureBox2.Bottom) / 2)), (spaceship.x - ((PictureBox2.Right + PictureBox2.Left) / 2)))
        spaceship.vy -= Math.Sin(deg) * dist
        spaceship.vx -= Math.Cos(deg) * dist
        spaceship.vx = Math.Min(MAX_SPEED, spaceship.vx)
        spaceship.vy = Math.Min(MAX_SPEED, spaceship.vy)
        spaceship.vx = Math.Max(-MAX_SPEED, spaceship.vx)
        spaceship.vy = Math.Max(-MAX_SPEED, spaceship.vy)
        move_asteroid(spaceship)
        For i = 0 To NUM_ASTEROIDS - 1
            move_asteroid(asteroids(i))
        Next
        fps += 1
    End Sub

    Private Sub move_asteroid(ByRef tmp As asteroid)
        tmp.y += tmp.vy
        tmp.y = truemod(tmp.y, Me.Bottom - Me.Top)
        tmp.x += tmp.vx
        tmp.x = truemod(tmp.x, Me.Right - Me.Left)
        tmp.picture.Left = tmp.x - tmp.picture.Width / 2
        tmp.picture.Top = tmp.y - tmp.picture.Height / 2
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
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
        asteroid1.vx = Math.Floor(Rnd() * (MAX_ASTEROID_SPEED - MIN_ASTEROID_SPEED)) + MIN_ASTEROID_SPEED
        asteroid1.vy = Math.Floor(Rnd() * (MAX_ASTEROID_SPEED - MIN_ASTEROID_SPEED)) + MIN_ASTEROID_SPEED
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

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        spaceship = New asteroid
        spaceship.picture = PictureBox1
        spaceship.x = (spaceship.picture.Right + spaceship.picture.Left) / 2
        spaceship.y = (spaceship.picture.Top + spaceship.picture.Bottom) / 2
        spaceship.size = 0
        spaceship.vx = 0
        spaceship.vy = -5
        Randomize()
        For i = 0 To NUM_ASTEROIDS - 1
            asteroids(i) = make_asteroid(MAX_ASTEROID_SIZE)
        Next
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        Label2.Text = fps
        fps = 0
    End Sub
End Class
