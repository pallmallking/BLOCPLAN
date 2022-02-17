Public Class vectorValue
    Shared firstPos As Point = New Point(100, 50)
    Dim WithEvents txtscoreA, txtscoreE, txtscoreI, txtscoreO, txtscoreU, txtscoreX As TextBox
    Dim WithEvents btnContinue, btnRestore, btnSave, btnClose As Button
    Private Sub vectorValue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(450, 400)
        Dim lblCode, lblAbs, lblEss, lblImp, lblOrd, lblUnim, lblUndes As Label
        Dim codeA, codeE, codeI, codeO, codeU, codeX As TextBox
        lblCode = New Label With {.Location = New Point(firstPos.X + 150, firstPos.Y), .Width = 200, .Text = "Code" & Space(10) & "Score"}
        lblAbs = New Label With {.Location = New Point(firstPos.X, firstPos.Y + 20), .Width = 150, .Text = "Absolutely Essential"}
        lblEss = New Label With {.Location = New Point(firstPos.X, firstPos.Y + 45), .Width = 150, .Text = "Essential"}
        lblImp = New Label With {.Location = New Point(firstPos.X, firstPos.Y + 70), .Width = 150, .Text = "Important"}
        lblOrd = New Label With {.Location = New Point(firstPos.X, firstPos.Y + 95), .Width = 150, .Text = "Ordinary Importance"}
        lblUnim = New Label With {.Location = New Point(firstPos.X, firstPos.Y + 120), .Width = 150, .Text = "Unimportant"}
        lblUndes = New Label With {.Location = New Point(firstPos.X, firstPos.Y + 145), .Width = 150, .Text = "Undesirable"}
        Me.Controls.Add(lblCode) : Me.Controls.Add(lblAbs) : Me.Controls.Add(lblEss) : Me.Controls.Add(lblImp)
        Me.Controls.Add(lblOrd) : Me.Controls.Add(lblUnim) : Me.Controls.Add(lblUndes)
        codeA = New TextBox With {.Location = New Point(firstPos.X + 155, firstPos.Y + 20), .Width = 25, .Text = "A", .ReadOnly = True}
        codeE = New TextBox With {.Location = New Point(firstPos.X + 155, firstPos.Y + 45), .Width = 25, .Text = "E", .ReadOnly = True}
        codeI = New TextBox With {.Location = New Point(firstPos.X + 155, firstPos.Y + 70), .Width = 25, .Text = "I", .ReadOnly = True}
        codeO = New TextBox With {.Location = New Point(firstPos.X + 155, firstPos.Y + 95), .Width = 25, .Text = "O", .ReadOnly = True}
        codeU = New TextBox With {.Location = New Point(firstPos.X + 155, firstPos.Y + 120), .Width = 25, .Text = "U", .ReadOnly = True}
        codeX = New TextBox With {.Location = New Point(firstPos.X + 155, firstPos.Y + 145), .Width = 25, .Text = "X", .ReadOnly = True}
        Me.Controls.Add(codeA) : Me.Controls.Add(codeE) : Me.Controls.Add(codeI) : Me.Controls.Add(codeO) : Me.Controls.Add(codeU) : Me.Controls.Add(codeX)
        txtscoreA = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 20), .Width = 25, .Text = scoreForm.scoreA, .Name = "A"}
        txtscoreE = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 45), .Width = 25, .Text = scoreForm.scoreE, .Name = "E"}
        txtscoreI = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 70), .Width = 25, .Text = scoreForm.scoreI, .Name = "I"}
        txtscoreO = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 95), .Width = 25, .Text = scoreForm.scoreO, .Name = "O"}
        txtscoreU = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 120), .Width = 25, .Text = scoreForm.scoreU, .Name = "U"}
        txtscoreX = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 145), .Width = 25, .Text = scoreForm.scoreX, .Name = "X"}
        Me.Controls.Add(txtscoreA) : Me.Controls.Add(txtscoreE) : Me.Controls.Add(txtscoreI) : Me.Controls.Add(txtscoreO) : Me.Controls.Add(txtscoreU) : Me.Controls.Add(txtscoreX)
        Dim btnContinue As New Button With {.Location = New Point(firstPos.X, firstPos.Y + 250), .Width = 75, .Height = 35, .Text = "Continue"}
        Dim btnRestore As New Button With {.Location = New Point(firstPos.X + 85, firstPos.Y + 250), .Width = 75, .Height = 35, .Text = "Restore Default"}
        Dim btnSave As New Button With {.Location = New Point(firstPos.X + 170, firstPos.Y + 250), .Width = 75, .Height = 35, .Text = "Save"}
        AddHandler btnSave.Click, AddressOf save_score
        Dim btnClose As New Button With {.Location = New Point(firstPos.X + 255, firstPos.Y + 250), .Width = 75, .Height = 35, .Text = "Close"}
        AddHandler btnClose.Click, AddressOf close_form
        AddHandler btnRestore.Click, AddressOf restore_default
        Me.Controls.Add(btnContinue) : Me.Controls.Add(btnRestore) : Me.Controls.Add(btnSave) : Me.Controls.Add(btnClose)
    End Sub
    Private Sub restore_default(sender As Object, e As EventArgs)
        txtscoreA.Text = "10"
        txtscoreE.Text = "5"
        txtscoreI.Text = "2"
        txtscoreO.Text = "1"
        txtscoreU.Text = "0"
        txtscoreX.Text = "-10"
    End Sub
    Sub save_score(sender As Object, e As EventArgs) Handles btnSave.Click
        For i = 1 To scoreForm.count
            For j = 2 To scoreForm.count
                If i >= j Then
                    'nothing
                Else
                    Select Case scoreForm.textVal(i, j).Text
                        Case "A"
                            scoreForm.scoreVal(i, j) = CInt(txtscoreA.Text)
                        Case "E"
                            scoreForm.scoreVal(i, j) = CInt(txtscoreE.Text)
                        Case "I"
                            scoreForm.scoreVal(i, j) = CInt(txtscoreI.Text)
                        Case "O"
                            scoreForm.scoreVal(i, j) = CInt(txtscoreO.Text)
                        Case "U"
                            scoreForm.scoreVal(i, j) = CInt(txtscoreU.Text)
                        Case "X"
                            scoreForm.scoreVal(i, j) = CInt(txtscoreX.Text)
                    End Select
                End If
            Next
        Next
        scoreForm.scoreA = CInt(txtscoreA.Text)
        scoreForm.scoreE = CInt(txtscoreE.Text)
        scoreForm.scoreI = CInt(txtscoreI.Text)
        scoreForm.scoreO = CInt(txtscoreO.Text)
        scoreForm.scoreU = CInt(txtscoreU.Text)
        scoreForm.scoreX = CInt(txtscoreX.Text)
        MessageBox.Show("Score has been saved")
    End Sub
    Sub close_form(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class