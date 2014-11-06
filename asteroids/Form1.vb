Public Class Form1
    Const NUM_ASTEROIDS As Integer = 1
    Const MAX_SPEED As Integer = 50
    Const ACCELERATION As Double = 0.5
    Const TORQUE As Double = 0.05
    Const MIN_ASTEROID_SPEED As Integer = -20
    Const MAX_ASTEROID_SPEED As Integer = 20
    Const MAX_ASTEROID_SIZE As Integer = 100
    Const GRAVITY As Integer = 4000

    Private vx As Double = 0
    Private vy As Double = 0
    Private x As Double
    Private y As Double
    Private direction As Double = Math.PI * 0.5
    Private degrees As Integer = 90
    Dim upkey = False, leftkey = False, rightkey = False
    Private asteroids(NUM_ASTEROIDS) As asteroid

    Private Structure asteroid
        Public vx As Integer
        Public vy As Integer
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
        Me.Text = degrees
        If upkey Then
            vy -= Math.Sin(direction) * ACCELERATION
            vx += Math.Cos(direction) * ACCELERATION
        End If
        Dim dist As Double = GRAVITY / Math.Max(((y - ((PictureBox2.Top + PictureBox2.Bottom) / 2)) ^ 2) + ((x - ((PictureBox2.Right + PictureBox2.Left) / 2)) ^ 2), 1)
        Dim deg As Double = Math.Atan2((y - ((PictureBox2.Top + PictureBox2.Bottom) / 2)), (x - ((PictureBox2.Right + PictureBox2.Left) / 2)))
        vy -= Math.Sin(deg) * dist
        vx -= Math.Cos(deg) * dist
        vx = Math.Min(MAX_SPEED, vx)
        vy = Math.Min(MAX_SPEED, vy)
        y += vy
        y = truemod(y, Me.Bottom - Me.Top)
        x += vx
        x = truemod(x, Me.Right - Me.Left)
        PictureBox1.Left = x - PictureBox1.Width / 2
        PictureBox1.Top = y - PictureBox1.Height / 2
        For i = 0 To NUM_ASTEROIDS - 1
            move_asteroid(asteroids(i))
        Next
    End Sub

    Private Sub move_asteroid(ByVal tmp As asteroid)
        tmp.picture.Top += tmp.vy
        tmp.picture.Top = truemod(tmp.picture.Top, Me.Bottom - Me.Top)
        tmp.picture.Left += tmp.vx
        tmp.picture.Left = truemod(tmp.picture.Left, Me.Right - Me.Left)
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
        asteroid1.picture.Left = Math.Floor((Rnd() * Me.Right - Me.Left))
        asteroid1.picture.Size = New Size(size, size)
        Me.Controls.Add(asteroid1.picture)
        Return asteroid1
    End Function

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        x = (PictureBox1.Right + PictureBox1.Left) / 2
        y = (PictureBox1.Top + PictureBox1.Bottom) / 2
        Randomize()
        For i = 0 To NUM_ASTEROIDS - 1
            asteroids(i) = make_asteroid(MAX_ASTEROID_SIZE)
        Next
    End Sub
End Class
