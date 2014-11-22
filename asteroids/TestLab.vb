' NOTE: THIS IS A ONLY TEMPORARY FORM FOR EXPERIMENTING S*** TO IMPLEMENT IN THE GAME

Public Class TestLab
    Dim mouseLocation As Point
    Dim asteroid = New List(Of Point)
    Dim asteroidXY = New Point(420, 210)

    Private Sub TestLab_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        asteroid.Add(mouseLocation)
        TextBox1.Text = "{ "
        For i = 0 To asteroid.count - 1
            TextBox1.Text += "New Point(asteroidX + " + (asteroid(i).X - asteroidXY.X).ToString() +
                ", asteroidY + " + (asteroid(i).Y - asteroidXY.Y).ToString() + "), "
        Next
        TextBox1.Text += "}"
    End Sub

    Private Sub TestLab_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        mouseLocation = e.Location
        Me.Refresh()
    End Sub


    Private Sub TestLab_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim g = e.Graphics
        Dim p As New Pen(Brushes.DarkBlue)
        p.Width = 3
        If asteroid.Count = 0 Then
            g.DrawLine(p, asteroidXY, mouseLocation)
        ElseIf asteroid.Count > 1 Then
            g.DrawLines(p, asteroid.toarray())
            If asteroid.Count >= 3 Then
                g.FillPolygon(Brushes.Beige, asteroid.toarray())
                g.DrawPolygon(p, asteroid.toarray())
            End If
            g.DrawLine(p, asteroid(asteroid.Count - 1), mouseLocation)
        End If
        If asteroid.Count > 0 Then
            g.DrawLine(p, asteroid(asteroid.Count - 1), mouseLocation)
        End If
    End Sub
End Class