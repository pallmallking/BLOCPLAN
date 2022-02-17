Public Class ratioForm
    Private firstPos As Point = New Point(20, 10)
    Private WithEvents btn135_1, btn1_1, btn2_1, btn1_2, btnSpy, btnBack, btnExe As Button
    Private lblSel1, lblSel2, lblSel3, lblSel4, lblSel5, lblChoosen, lblRatioY, lblRatioX, lblX As Label
    Public Shared ratioX, ratioY As String
    Private calib As Integer = 80
    Private Sub ratioForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(500, 500) : ratioX = 0.0 : ratioY = 0.0
        Dim lblTitle As Label = New Label() With {.Location = New Point(firstPos.X + 100, firstPos.Y), .Width = 300, .Text = "Select a length (horizontal) to width (vertical) ratio"}
        Dim lblSel1 As Label = New Label() With {.Location = New Point(firstPos.X + (1 * calib) / 2, firstPos.Y + 1 * calib + 60), .Width = 50, .Text = "Sel. 1", .Name = "oke"}
        Dim lblSel2 As Label = New Label() With {.Location = New Point(firstPos.X + (1.5 * calib) / 2, firstPos.Y + 2 * calib + 140), .Width = 50, .Text = "Sel. 2"}
        Dim lblSel3 As Label = New Label() With {.Location = New Point(firstPos.X + calib * 1.35 + 70 + (0.6 * calib) / 2, firstPos.Y + 1 * calib + 60), .Width = 50, .Text = "Sel. 3"}
        Dim lblSel4 As Label = New Label() With {.Location = New Point(firstPos.X + 2 * calib + 20 + (0.6 * calib) / 2, firstPos.Y + 3 * calib + 120), .Width = 50, .Text = "Sel. 4"}
        Dim lblSel5 As Label = New Label() With {.Location = New Point(firstPos.X + 3 * calib + 70 + (1 * calib) / 2, firstPos.Y + 2 * calib + 80), .Width = 50, .Text = "Sel. 5"}
        Me.Controls.Add(lblTitle) : Me.Controls.Add(lblSel1) : Me.Controls.Add(lblSel2) : Me.Controls.Add(lblSel3) : Me.Controls.Add(lblSel4) : Me.Controls.Add(lblSel5)
        btn135_1 = New Button() With {.Location = New Point(firstPos.X, firstPos.Y + 50), .Size = New Size(1.35 * calib, 1 * calib), .Text = "1.35 x 1.00"}
        btn1_1 = New Button() With {.Location = New Point(firstPos.X + calib * 1.35 + 70, firstPos.Y + 50), .Size = New Size(1 * calib, 1 * calib), .Text = "1.00 x 1.00"}
        btn2_1 = New Button() With {.Location = New Point(firstPos.X, firstPos.Y + 1 * calib + 130), .Size = New Size(2 * calib, 1 * calib), .Text = "2.00 x 1.00"}
        btn1_2 = New Button() With {.Location = New Point(firstPos.X + 2 * calib + 20, firstPos.Y + 1 * calib + 110), .Size = New Size(1 * calib, 2 * calib), .Text = "1.00 x2.00"}
        btnSpy = New Button() With {.Location = New Point(firstPos.X + 3 * calib + 70, firstPos.Y + 1 * calib + 70), .Size = New Size(1.5 * calib, 1 * calib), .Text = "Specify Ratio"}
        btnBack = New Button() With {.Location = New Point(firstPos.X + 2 * calib + 20, firstPos.Y + 3 * calib + 170), .Size = New Size(1 * calib, 30), .Text = "Back"}
        btnExe = New Button() With {.Location = New Point(firstPos.X + 2 * calib + 120, firstPos.Y + 3 * calib + 170), .Size = New Size(1 * calib, 30), .Text = "Execute"}
        AddHandler btn1_1.Click, AddressOf btn_click
        Me.Controls.Add(btn135_1) : Me.Controls.Add(btn1_1) : Me.Controls.Add(btn2_1) : Me.Controls.Add(btn1_2) : Me.Controls.Add(btnSpy) : Me.Controls.Add(btnBack) : Me.Controls.Add(btnExe)
        lblChoosen = New Label() With {.Location = New Point(firstPos.X + 3 * calib + 50, firstPos.Y + 2 * calib + 130), .Width = 150, .Text = "Choosen Ratio :", .Font = New Font("Arial", 13, FontStyle.Bold)}
        lblRatioX = New Label() With {.Location = New Point(firstPos.X + 3 * calib + 60, firstPos.Y + 2 * calib + 170), .Width = 50, .Text = ratioX, .Font = New Font("Arial", 13, FontStyle.Bold)}
        lblX = New Label() With {.Location = New Point(firstPos.X + 3 * calib + 110, firstPos.Y + 2 * calib + 170), .Width = 20, .Text = "X", .Font = New Font("Arial", 13, FontStyle.Bold)}
        lblRatioY = New Label() With {.Location = New Point(firstPos.X + 3 * calib + 150, firstPos.Y + 2 * calib + 170), .Width = 50, .Text = ratioY, .Font = New Font("Arial", 13, FontStyle.Bold)}
        Me.Controls.Add(lblChoosen) : Me.Controls.Add(lblRatioX) : Me.Controls.Add(lblX) : Me.Controls.Add(lblRatioY)
    End Sub
    Sub close_form() Handles btnBack.Click
        Me.Close()
    End Sub
    Sub execute_rectangle() Handles btnExe.Click
        ratioX = CSng(lblRatioX.Text) : ratioY = CSng(lblRatioY.Text)
        'areaForm.Show()
        If (ratioX = 0 Or ratioY = 0) Then
            MessageBox.Show("Area Ratio must be specified")
        Else
            layoutForm.Show()
        End If
    End Sub
    Sub btn_click(sender As Object, e As EventArgs) Handles btn1_1.Click, btn1_2.Click, btn135_1.Click, btn2_1.Click
        Dim btn As Button = sender
        Dim str = btn.Text.Split("x") : Dim ratioX = Replace(str(0), ".", ",") : Dim ratioY = Replace(str(1), ".", ",")
        lblRatioX.Text = ratioX
        lblRatioY.Text = ratioY
        'MessageBox.Show(ratioX.ToString)
    End Sub
    Sub spy_click(sender As Object, e As EventArgs) Handles btnSpy.Click
        Dim InputX As String = InputBox("Input length(horizontal) ratio :") : ratioX = CSng(Replace(InputX, ".", ","))
        Dim InputY As String = InputBox("Input widht(vertical) ratio :") : ratioY = CSng(Replace(InputY, ".", ","))
        lblRatioX.Text = ratioX
        lblRatioY.Text = ratioY
    End Sub
End Class
