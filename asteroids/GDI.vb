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
    'Missile property
    Public launchTime As Integer
End Structure

Public Class GDI
    Const NUM_ASTEROIDS As Integer = 3
    Const MAX_SPEED As Integer = 14
    Const ACCELERATION As Double = 0.1
    Const TORQUE As Double = 0.03
    Const STARTING_ASTEROID_SPEED As Integer = 9
    Const MAX_ASTEROID_SPEED As Integer = 10
    Const MAX_ASTEROID_SIZE As Integer = 3
    Const GRAVITY As Integer = 9000
    Const MAX_GRAVITY As Integer = 10
    Const MAX_MISSILES As Integer = 5
    Const MISSILE_SIZE As Integer = 7
    Const MISSILE_COOLDOWN_PEROID As Integer = 150 / 16
    Const MAX_MISSILE_TIME As Integer = 1300 / 16
    Const MISSILE_SPEED As Integer = 8
    Const MAX_MISSILE_SPEED As Integer = 30

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
    Dim fps As Integer = 0
    Dim gravityX As Integer
    Dim gravityY As Integer
    Dim r As Random
    Dim g As Graphics
    Dim imgSpaceship As Bitmap

    Private Function truemod(ByVal x As Integer, ByVal y As Integer) As Integer
        Return ((x Mod y) + y) Mod y
    End Function

    Private Function floatmod(ByVal x As Double, ByVal y As Integer) As Double
        Return ((x Mod y) + y) Mod y
    End Function

    Private Sub GDI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        r = New Random()
        'New spaceship, center of screen, zero velocity
        spaceship = New SpaceObject
        spaceship.x = 0
        spaceship.y = 0
        spaceship.vx = 0
        spaceship.vy = 0
        Randomize()
        For i = 1 To NUM_ASTEROIDS
            asteroids.Add(make_asteroid(MAX_ASTEROID_SIZE))
        Next
        For i = 0 To MAX_MISSILES
            missiles(i) = make_missile()
        Next
        gravityX = picGravity.Left + picGravity.Width / 2
        gravityY = picGravity.Top + picGravity.Height / 2
    End Sub

    Private Function make_asteroid(ByVal size As Integer) As SpaceObject
        Dim asteroid As SpaceObject = New SpaceObject
        asteroid.vx = r.NextDouble() * STARTING_ASTEROID_SPEED * 2 - STARTING_ASTEROID_SPEED
        asteroid.vy = r.NextDouble() * STARTING_ASTEROID_SPEED * 2 - STARTING_ASTEROID_SPEED
        asteroid.size = size
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
        If e.KeyCode = Keys.Space Then
            spacekey = True
        End If
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
        If e.KeyCode = Keys.Space Then
            spacekey = False
        End If
    End Sub

    Private Sub updateGame(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles updateTimer.Tick
        If leftKey = True Then
            direction += Math.PI * TORQUE
        End If
        If rightKey = True Then
            direction -= Math.PI * TORQUE
        End If
        degrees = truemod(direction * (180 / Math.PI), 360)
        If upKey Then
            spaceship.vy -= Math.Sin(direction) * ACCELERATION
            spaceship.vx += Math.Cos(direction) * ACCELERATION
        End If
        'Move objects
        ApplyGravity(spaceship, MAX_SPEED, 1)
        MoveObject(spaceship)
        For i = 0 To asteroids.Count - 1
            ApplyGravity(asteroids(i), MAX_ASTEROID_SPEED, 0.25)
            MoveObject(asteroids(i))
        Next
        If GRAVITY > 0 Then
            checkGravityCollision()
        End If
        UpdateMissiles()
        fps += 1
        'Debugging outputs
        lblDegrees.Text = degrees.ToString() + "°"
        lblVx.Text = "vx: " + FormatNumber(spaceship.vx, 2)
        lblVy.Text = "vy: " + FormatNumber(spaceship.vy, 2)
        'Draw graphics
        Me.Invalidate()
    End Sub

    Private Sub ApplyGravity(ByRef obj As SpaceObject, ByVal maxSpeed As Integer, ByVal strength As Single)
        'disturbingly long lines
        Dim dist As Double = GRAVITY * strength / Math.Max(((obj.y - ((picGravity.Top + picGravity.Bottom) / 2)) ^ 2) + ((obj.x - ((picGravity.Right + picGravity.Left) / 2)) ^ 2), MAX_GRAVITY ^ 2)
        'Label1.Text = dist
        Dim deg As Double = Math.Atan2((obj.y - ((picGravity.Top + picGravity.Bottom) / 2)), (obj.x - ((picGravity.Right + picGravity.Left) / 2)))
        obj.vy -= Math.Sin(deg) * dist
        obj.vx -= Math.Cos(deg) * dist
        obj.vx = Math.Min(maxSpeed, obj.vx)
        obj.vy = Math.Min(maxSpeed, obj.vy)
        obj.vx = Math.Max(-maxSpeed, obj.vx)
        obj.vy = Math.Max(-maxSpeed, obj.vy)
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
                End If
            End If
        Next
        'Launch missiles
        If spaceKey = True And missileLaunchCooldown = 0 Then
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

    Private Function FindInactiveMissile() As Integer
        For i = 1 To MAX_MISSILES
            If missiles(i).launchTime = 0 Then
                Return i
            End If
        Next
        Return -1
    End Function

    Private Sub fpsTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fpsTimer.Tick
        lblFPS.Text = fps.ToString() + "fps"
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
        Dim distance = Math.Sqrt((spaceship.x - gravityX) ^ 2 + (spaceship.y - gravityY) ^ 2)
        If distance < 15 Then
            lblUpdates.Text = "fell into gravity well"
            'fpsTimer.Stop()
            'updateTimer.Stop()
        End If
    End Sub

    Private Sub GDI_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.HighSpeed
        'Draw asteroids
        Dim asteroidBrush As New SolidBrush(Color.Red)
        Dim imgAsteroid As Rectangle
        For i = 0 To asteroids.Count - 1
            imgAsteroid = New Rectangle(asteroids(i).x - 25, asteroids(i).y - 25, 50, 50)
            g.FillRectangle(asteroidBrush, imgAsteroid)
        Next
        'asteroidBrush.Dispose()
        'Draw spaceship
        imgSpaceship = RotateImg(My.Resources.spaceship, truemod(90 - degrees, 360))
        g.DrawImage(imgSpaceship, New Point(spaceship.x - imgSpaceship.Width / 2, spaceship.y - imgSpaceship.Height / 2))
        imgSpaceship.Dispose()
        'Draw missiles
        Dim missileBrush As New SolidBrush(Color.Black)
        For i = 0 To MAX_MISSILES
            If missiles(i).launchTime > 0 Then
                g.FillEllipse(missileBrush, New Rectangle(missiles(i).x, missiles(i).y, 10, 10))
            End If
        Next
        missileBrush.Dispose()
        'TEST
        Dim myPointArray As Point() = _
           {New Point(0, 0), New Point(50, 30), New Point(30, 60)}
        'g.DrawPolygon(myPen, myPointArray)
        g.FillPolygon(asteroidBrush, myPointArray)
    End Sub

    Private Sub UpdateAsteroidPoints(ByRef asteroid As SpaceObject)

    End Sub
End Class