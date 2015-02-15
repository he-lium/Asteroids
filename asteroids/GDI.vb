Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing

Structure SpaceObject
    Public x As Double
    Public y As Double
    Public vx As Double
    Public vy As Double
    'Asteroid property
    Public size As Integer
    Public type As Integer
    Public expelMode As Boolean
    'Missile property
    Public launchTime As Integer
End Structure

Public Class GDI

#Region "Initialise"
    Dim NUM_ASTEROIDS As Integer = 2
    Const MAX_SPEED As Integer = 9
    Const ACCELERATION As Double = 0.15
    Const TORQUE As Double = 0.03
    Const STARTING_ASTEROID_SPEED As Integer = 4
    Const MAX_ASTEROID_SPEED As Integer = 6
    Const ASTEROID_EXPEL_SPEED As Integer = 18
    Const MAX_ASTEROID_SIZE As Integer = 3
    Const GRAVITY As Integer = 3000
    Const MAX_GRAVITY As Integer = 10
    Const MAX_MISSILES As Integer = 5
    Const MISSILE_SIZE As Integer = 7
    Const MISSILE_COOLDOWN_PEROID As Integer = 300 / 16
    Const MAX_MISSILE_TIME As Integer = 2000 / 16
    Const MISSILE_SPEED As Integer = 8
    Const MAX_MISSILE_SPEED As Integer = 30
    Const EXPEL_DISTANCE As Integer = 50
    Const COLLISION_DISTANCE As Integer = 15
    Const COOLDOWN_PEROID As Integer = 200
    Const AMOUNT_OF_LIVES As Integer = 3

    Dim score As Integer
    Dim spaceship As SpaceObject
    Dim direction As Double = Math.PI * 0.5
    Dim degrees As Integer = 0
    Dim asteroids As New List(Of SpaceObject)()
    Dim missiles(MAX_MISSILES) As SpaceObject
    Dim missileLaunchCooldown As Integer = 0
    Dim upKey As Boolean = False
    Dim leftKey As Boolean = False
    Dim rightKey As Boolean = False
    Dim spaceKey As Boolean = False
    Dim escKey As Boolean = False
    Dim fps As Stopwatch = Stopwatch.StartNew
    Dim framecount As Integer = 1
    Dim gravityX As Integer
    Dim gravityY As Integer
    Dim r As Random
    Dim g As Graphics
    Dim imgSpaceship As Bitmap
    Dim dragGravityWell As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer
    Dim livesLeft As Integer
    Dim cooldown As Integer
    Dim crashed As Boolean = 0
    Dim fellIntoGravity As Boolean = 0
    Dim prelude As Boolean = 0

    Private Sub GDI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Private Sub NewGame() Handles MyBase.Load
        r = New Random()
        'New spaceship, center of screen, zero velocity
        spaceship = New SpaceObject
        spaceship.x = 300
        spaceship.y = 300
        spaceship.vx = 0
        spaceship.vy = 0
        Randomize()
        asteroids = New List(Of SpaceObject)()
        For i = 0 To NUM_ASTEROIDS - 1
            asteroids.Add(make_asteroid(MAX_ASTEROID_SIZE))
        Next
        For i = 0 To MAX_MISSILES
            missiles(i) = make_missile()
        Next
        cooldown = COOLDOWN_PEROID
        fps.Start()
        livesLeft = AMOUNT_OF_LIVES
        crashed = 0
        fellIntoGravity = 0
        missileLaunchCooldown = 0
        score = 0

    End Sub

    Private Sub nextLevel(sender As Object, e As EventArgs) Handles nextLevelTimer.Tick
        NUM_ASTEROIDS = NUM_ASTEROIDS + 2
        livesLeft += 1
        lblLives.Text = "Lives left: " + livesLeft.ToString()
        For i = 0 To NUM_ASTEROIDS - 1
            asteroids.Add(make_asteroid(MAX_ASTEROID_SIZE))
        Next
        cooldown = COOLDOWN_PEROID
        nextLevelTimer.Stop()
    End Sub

    Private Sub revive(sender As Object, e As EventArgs) Handles crashTimer.Tick
        spaceship.x = 300
        spaceship.y = 300
        spaceship.vx = 0
        spaceship.vy = 0
        cooldown = COOLDOWN_PEROID
        crashTimer.Stop()
        crashed = False
        fellIntoGravity = False
        livesLeft -= 1
        If livesLeft >= 0 Then
            lblLives.Text = "Lives left: " + livesLeft.ToString()
        End If
    End Sub

    Private Function make_asteroid(ByVal size As Integer) As SpaceObject
        Dim asteroid As SpaceObject = New SpaceObject
        asteroid.vx = r.NextDouble() * STARTING_ASTEROID_SPEED * 2 - STARTING_ASTEROID_SPEED
        asteroid.vy = r.NextDouble() * STARTING_ASTEROID_SPEED * 2 - STARTING_ASTEROID_SPEED
        asteroid.size = size
        asteroid.expelMode = False
        asteroid.x = r.Next(Me.Width)
        asteroid.y = r.Next(Me.Height)
        Return asteroid
    End Function

    Private Function make_missile() As SpaceObject
        Dim missile As New SpaceObject
        missile.vx = 0
        missile.vy = 0
        missile.launchTime = 0
    End Function

#End Region

    Private Function truemod(ByVal x As Integer, ByVal y As Integer) As Integer
        Return ((x Mod y) + y) Mod y
    End Function

    Private Function floatmod(ByVal x As Double, ByVal y As Integer) As Double
        Return ((x Mod y) + y) Mod y
    End Function

#Region "Key Controls"

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Up Then
            upKey = True
        End If
        If e.KeyCode = Keys.Left Then
            leftKey = True
        End If
        If e.KeyCode = Keys.Right Then
            rightKey = True
        End If
        If e.KeyCode = Keys.Space Then
            spaceKey = True
        End If
        If e.KeyCode = Keys.Escape Then
            escKey = True
        End If
    End Sub

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Up Then
            upKey = False
        End If
        If e.KeyCode = Keys.Left Then
            leftKey = False
        End If
        If e.KeyCode = Keys.Right Then
            rightKey = False
        End If
        If e.KeyCode = Keys.Space Then
            spaceKey = False
        End If
        If e.KeyCode = Keys.Escape Then
            escKey = False
        End If
    End Sub

#End Region

    Private Sub updateGame(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles updateTimer.Tick
        gravityX = picGravity.Left + picGravity.Width / 2
        gravityY = picGravity.Top + picGravity.Height / 2
        lblScore.Text = "Score: " + score.ToString()
        lblLives.Text = "Lives left: " + livesLeft.ToString()
        If livesLeft = -1 Then
            lblLives.Text = "0"
            updateTimer.Stop()
            Button2.Enabled = False
            panelMainMenu.Show()
            lblScore.Text = "Game Over! Score: " + score.ToString()
            prelude = 0
            NewGame()
        End If
        If leftKey = True Then
            direction += Math.PI * TORQUE
            cooldown = 0
        End If
        If rightKey = True Then
            direction -= Math.PI * TORQUE
            cooldown = 0
        End If
        degrees = truemod(direction * (180 / Math.PI), 360)
        If upKey Then
            spaceship.vy -= Math.Sin(direction) * ACCELERATION
            spaceship.vx += Math.Cos(direction) * ACCELERATION
            cooldown = 0
        End If
        'Move objects
        If cooldown = 0 Then
            CheckAsteroidCollision()
            ApplyGravity(spaceship, MAX_SPEED, 1)
        End If
        If crashed = False Then
            MoveObject(spaceship)
        End If
        For i = 0 To asteroids.Count - 1
            ApplyGravity(asteroids(i), MAX_ASTEROID_SPEED, 0.25)
            MoveObject(asteroids(i))
        Next
        If GRAVITY > 0 Then
            If checkGravityCollision(spaceship) = True And cooldown = 0 Then
                fellIntoGravity = True
                crashTimer.Start()
            End If
            For i = 0 To asteroids.Count - 1
                expel(asteroids(i))
            Next
        End If
        UpdateMissiles()
        If cooldown > 0 Then
            cooldown = cooldown - 1
        End If
        If asteroids.Count = 0 Then
            nextLevelTimer.Start()
        End If
        'Debugging outputs

        lblDegrees.Text = degrees.ToString() + "°"
        lblVx.Text = "vx: " + FormatNumber(spaceship.vx, 2)
        lblVy.Text = "vy: " + FormatNumber(spaceship.vy, 2)
        'Draw graphics
        Me.Invalidate()
        If framecount = 5 Then

            lblFPS.Text = (FormatNumber(5 / (fps.Elapsed.TotalMilliseconds / 1000), 2)).ToString() + " fps"
            fps.Reset()
            fps.Start()
            framecount = 1
        Else
            framecount += 1
        End If
        If escKey = True Then
            updateTimer.Stop()
            panelMainMenu.Show()
        End If
    End Sub

    Private Sub ApplyGravity(ByRef obj As SpaceObject, ByVal maxSpeed As Integer, ByVal strength As Single)
        If obj.expelMode = True Then
            strength = 0
        End If
        Dim distance As Double = (obj.x - gravityX) ^ 2 + (obj.y - gravityY) ^ 2
        Dim force As Double = GRAVITY * strength / Math.Max(distance, MAX_GRAVITY ^ 2)
        distance = Math.Sqrt(distance)
        Dim rise As Double = (obj.y - ((picGravity.Top + picGravity.Bottom) / 2))
        Dim run As Double = (obj.x - ((picGravity.Right + picGravity.Left) / 2))
        obj.vy -= (rise / distance) * force
        obj.vx -= (run / distance) * force
        obj.vx = Math.Min(maxSpeed, obj.vx)
        obj.vy = Math.Min(maxSpeed, obj.vy)
        obj.vx = Math.Max(-maxSpeed, obj.vx)
        obj.vy = Math.Max(-maxSpeed, obj.vy)
        If distance > EXPEL_DISTANCE Then
            obj.expelMode = False
        End If
    End Sub

    Private Sub MoveObject(ByRef obj As SpaceObject)
        obj.y += obj.vy
        obj.y = floatmod(obj.y, Me.Height)
        obj.x += obj.vx
        obj.x = floatmod(obj.x, Me.Width)
    End Sub

    Private Sub UpdateMissiles()
        'Run missile launch cooldown
        If missileLaunchCooldown > 0 Then
            missileLaunchCooldown = missileLaunchCooldown - 1
        End If
        'Move missiles
        For i = 0 To MAX_MISSILES
            If missiles(i).launchTime > 0 Then
                If missiles(i).launchTime > MAX_MISSILE_TIME Then
                    'Deactivate missile
                    missiles(i).launchTime = 0
                Else
                    'Move missile
                    ApplyGravity(missiles(i), MAX_MISSILE_SPEED, 0.75)
                    MoveObject(missiles(i))
                    missiles(i).launchTime = missiles(i).launchTime + 1
                    'Check missile collision
                    For a = 0 To asteroids.Count - 1
                        Dim dist = Math.Sqrt((asteroids(a).x - missiles(i).x) * (asteroids(a).x - missiles(i).x) + (asteroids(a).y - missiles(i).y) * (asteroids(a).y - missiles(i).y))
                        If asteroids(a).size = 3 And dist < 65 Then
                            score = score + 10
                            lblScore.Text = "Score: " + score.ToString()
                            SplitAsteroid(asteroids(a))
                            'Deactivate After destroying asteroid
                            missiles(i).launchTime = 0
                            Exit For 'equivalent to break
                        ElseIf asteroids(a).size = 2 And dist < 30 Then
                            score = score + 10
                            lblScore.Text = "Score: " + score.ToString()
                            SplitAsteroid(asteroids(a))
                            'Deactivate After destroying asteroid
                            missiles(i).launchTime = 0
                            Exit For
                        ElseIf dist < 30 Then
                            score = score + 10
                            lblScore.Text = "Score: " + score.ToString()
                            asteroids.RemoveAt(a)
                            'Deactivate After destroying asteroid
                            missiles(i).launchTime = 0
                            Exit For
                        End If
                    Next
                End If
            End If
        Next
        'Launch missiles
        If spaceKey = True And missileLaunchCooldown = 0 And fellIntoGravity = 0 And crashed = 0 Then
            cooldown = 0
            missileLaunchCooldown = MISSILE_COOLDOWN_PEROID
            Dim newMissile = FindInactiveMissile()
            If newMissile <> -1 Then
                missiles(newMissile).launchTime = 1
                missiles(newMissile).x = spaceship.x
                missiles(newMissile).y = spaceship.y
                missiles(newMissile).vx = spaceship.vx + Math.Cos(direction) * MISSILE_SPEED
                missiles(newMissile).vy = spaceship.vy - Math.Sin(direction) * MISSILE_SPEED
            End If
            lblUpdates.Text = "Launched Missile " + newMissile.ToString()
        End If
    End Sub

    Private Sub SplitAsteroid(ByRef asteroid As SpaceObject)
        asteroid.size = asteroid.size - 1
        asteroid.vx = -asteroid.vx
        Dim fragment As SpaceObject = asteroid
        'Make the two asteroids go in opposite directions
        asteroid.vx = -asteroid.vx
        asteroid.vy = -asteroid.vy
        asteroids.Add(fragment)
    End Sub

    Private Function FindInactiveMissile() As Integer
        For i = 1 To MAX_MISSILES
            If missiles(i).launchTime = 0 Then
                Return i
            End If
        Next
        Return -1
    End Function

    Private Sub expel(ByRef obj As SpaceObject)
        If checkGravityCollision(obj) Then
            obj.expelMode = True
        End If
    End Sub

    Private Sub CheckAsteroidCollision()
        For i = 0 To asteroids.Count - 1
            Dim dist = Math.Sqrt((asteroids(i).x - spaceship.x) * (asteroids(i).x - spaceship.x) + (asteroids(i).y - spaceship.y) * (asteroids(i).y - spaceship.y))
            If asteroids(i).size = 3 And dist < 65 Then
                crashed = True
                crashTimer.Start()
            ElseIf asteroids(i).size = 2 And dist < 30 Then
                crashed = True
                crashTimer.Start()
            ElseIf dist < 15 Then
                crashed = True
                crashTimer.Start()
            End If
        Next
    End Sub

    Private Function checkGravityCollision(ByVal obj As SpaceObject) As Boolean
        Dim distance = (obj.x - gravityX) ^ 2 + (obj.y - gravityY) ^ 2
        If distance < COLLISION_DISTANCE ^ 2 Then
            Return True
        Else
            Return False
        End If
    End Function

#Region "Drag and Drop Gravity Well"

    Private Sub picGravity_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles picGravity.MouseDown
        If prelude = 0 Then
            dragGravityWell = True
            mouseX = e.X
            mouseY = e.Y
        End If
    End Sub

    Private Sub picGravity_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles picGravity.MouseUp
        dragGravityWell = False
    End Sub

    Private Sub picGravity_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles picGravity.MouseMove
        If dragGravityWell = True Then
            picGravity.Left = picGravity.Left + e.X - mouseX
            picGravity.Top = picGravity.Top + e.Y - mouseY
            gravityX = picGravity.Left + picGravity.Width / 2
            gravityY = picGravity.Top + picGravity.Height / 2
        End If
    End Sub

#End Region

#Region "Graphics Drawing"
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

    Private Sub GDI_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.HighSpeed
        'Draw asteroids
        Dim asteroidBrush As New SolidBrush(Color.PaleVioletRed)
        Dim imgAsteroid As Rectangle
        For i = 0 To asteroids.Count - 1
            If asteroids(i).size = 3 Then
                imgAsteroid = New Rectangle(asteroids(i).x - 60, asteroids(i).y - 60, 120, 120)
                g.FillEllipse(asteroidBrush, imgAsteroid)
            ElseIf asteroids(i).size = 2 Then
                imgAsteroid = New Rectangle(asteroids(i).x - 25, asteroids(i).y - 25, 50, 50)
                g.FillEllipse(asteroidBrush, imgAsteroid)
            Else
                imgAsteroid = New Rectangle(asteroids(i).x - 7, asteroids(i).y - 7, 14, 14)
                g.FillEllipse(asteroidBrush, imgAsteroid)
            End If

        Next
        'asteroidBrush.Dispose()
        'Draw spaceship
        If cooldown Mod 2 = 0 Then
            If crashed = True Then
                imgSpaceship = My.Resources.explosion
            Else
                imgSpaceship = RotateImg(My.Resources.spaceship, truemod(90 - degrees, 360))
            End If
            g.DrawImage(imgSpaceship, New Point(spaceship.x - imgSpaceship.Width / 2, spaceship.y - imgSpaceship.Height / 2))
            imgSpaceship.Dispose()
        End If
        'Draw missiles
        Dim missileBrush As New SolidBrush(Color.Black)
        For i = 0 To MAX_MISSILES
            If missiles(i).launchTime > 0 Then
                g.FillEllipse(missileBrush, New Rectangle(missiles(i).x, missiles(i).y, 10, 10))
            End If
        Next
        missileBrush.Dispose()
    End Sub
#End Region

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        updateTimer.Start()
        panelMainMenu.Hide()
        prelude = 1
        Me.ActiveControl = Nothing
        Button2.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        panelMainMenu.Hide()
        updateTimer.Start()
        NewGame()
        Me.ActiveControl = Nothing
    End Sub
End Class