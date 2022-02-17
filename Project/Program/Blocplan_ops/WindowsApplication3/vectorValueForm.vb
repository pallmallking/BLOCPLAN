Public Class vectorValueForm
    Private scoreA, scoreE, scoreI, scoreO, scoreU, scoreX As TextBox
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim firstPos As Point = New Point(100, 50)
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
        scoreA = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 20), .Width = 25, .Text = "10"}
        scoreE = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 45), .Width = 25, .Text = "5"}
        scoreI = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 70), .Width = 25, .Text = "2"}
        scoreO = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 95), .Width = 25, .Text = "1"}
        scoreU = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 120), .Width = 25, .Text = "0"}
        scoreX = New TextBox With {.Location = New Point(firstPos.X + 210, firstPos.Y + 145), .Width = 25, .Text = "-10"}
        Me.Controls.Add(scoreA) : Me.Controls.Add(scoreE) : Me.Controls.Add(scoreI) : Me.Controls.Add(scoreO) : Me.Controls.Add(scoreU) : Me.Controls.Add(scoreX)
        Dim btnContinue As New Button With {.Location = New Point(firstPos.X + 30, firstPos.Y + 250), .Width = 75, .Height = 35, .Text = "Continue"}
        Dim btnRestore As New Button With {.Location = New Point(firstPos.X + 130, firstPos.Y + 250), .Width = 75, .Height = 35, .Text = "Restore Default"}
        Dim btnPrint As New Button With {.Location = New Point(firstPos.X + 230, firstPos.Y + 250), .Width = 75, .Height = 35, .Text = "Print"}
        AddHandler btnRestore.Click, AddressOf restore_default
        Me.Controls.Add(btnContinue) : Me.Controls.Add(btnRestore) : Me.Controls.Add(btnPrint)
    End Sub

    Private Sub restore_default(sender As Object, e As EventArgs)
        scoreA.Text = "10"
        scoreE.Text = "5"
        scoreI.Text = "2"
        scoreO.Text = "1"
        scoreU.Text = "0"
        scoreX.Text = "-10"
    End Sub

End Class