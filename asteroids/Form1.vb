Public Class Form1

    Private vx As Double = 0
    Private vy As Double = 0
    Private direction As Double = Math.PI * 0.5
    Private degrees As Integer = 90
    Dim upkey = False, leftkey = False, rightkey = False
    Private asteroids(10) As asteroid

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
        ElseIf e.KeyCode = Keys.Left Then
            leftkey = True
        ElseIf e.KeyCode = Keys.Right Then
            rightkey = True
        End If
    End Sub


    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If leftkey Then
            direction += Math.PI * 0.05
        End If
        If rightkey Then
            direction -= Math.PI * 0.05
        End If
        degrees = truemod(direction * (180 / Math.PI), 360)
        Me.Text = degrees
        If upkey Then
            vy -= Math.Sin(direction) * 0.5
            vx += Math.Cos(direction) * 0.5
        End If
        PictureBox1.Top += vy
        PictureBox1.Top = truemod(PictureBox1.Top, 507)
        PictureBox1.Left += vx
        PictureBox1.Left = truemod(PictureBox1.Left, 785)
        vx = vx * 0.98
        vy = vy * 0.98
        move_asteroid(asteroids(0))
        move_asteroid(asteroids(1))
        move_asteroid(asteroids(2))
    End Sub

    Private Sub move_asteroid(ByVal tmp As asteroid)
        tmp.picture.Top += tmp.vy
        tmp.picture.Top = truemod(tmp.picture.Top, 507)
        tmp.picture.Left += tmp.vx
        tmp.picture.Left = truemod(tmp.picture.Left, 785)
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Up Then
            upkey = False
        ElseIf e.KeyCode = Keys.Left Then
            leftkey = False
        ElseIf e.KeyCode = Keys.Right Then
            rightkey = False
        End If
    End Sub

    Private Function make_asteroid(ByVal size As Integer) As asteroid
        Dim asteroid1 As asteroid = New asteroid
        asteroid1.vx = Math.Floor(Rnd() * 40) - 20
        asteroid1.vy = Math.Floor(Rnd() * 40) - 20
        asteroid1.size = size
        asteroid1.picture = New PictureBox
        asteroid1.picture.BackColor = Color.HotPink
        asteroid1.picture.Top = Math.Floor((Rnd() * 507))
        asteroid1.picture.Left = Math.Floor((Rnd() * 785))
        asteroid1.picture.Size = New Size(size, size)
        Me.Controls.Add(asteroid1.picture)
        Return asteroid1
    End Function

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Randomize()
        asteroids(0) = make_asteroid(100)
        asteroids(2) = make_asteroid(100)
        asteroids(1) = make_asteroid(100)
    End Sub
End Class
