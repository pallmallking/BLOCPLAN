Public Class productForm
    Public Shared txtNo(18), txtProd(18), txtFlow(18) As TextBox
    Private lblNo, lblProd, lblFlow, lblTitle As Label
    Private noPos, prodPos, flowPos, continuePos, printPos, backPos As Point
    Private btnContinue, btnPrint, btnBack As Button

    Private Sub productForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(500, 625) : Me.CenterToScreen()
        noPos = New Point(30, 60)
        lblNo = New Label With {.Text = "No", .Location = noPos, .Width = 30}
        prodPos = New Point(noPos.X + lblNo.Width + 10, noPos.Y)
        lblProd = New Label With {.Text = "Product", .Location = New Point(prodPos)}
        flowPos = New Point(prodPos.X + lblProd.Width + 10, noPos.Y)
        lblFlow = New Label With {.Text = "Flow", .Location = New Point(flowPos), .Width = 50}
        lblTitle = New Label With {.Text = "Product Flow", .Location = New Point(flowPos.X - 30, flowPos.Y - 50), .Width = 150, .Font = New Font("Arial", 14)}
        Me.Controls.Add(lblNo) : Me.Controls.Add(lblProd) : Me.Controls.Add(lblFlow) : Me.Controls.Add(lblTitle)
        Dim i As Integer
        For i = 1 To 14
            txtNo(i) = New TextBox : txtProd(i) = New TextBox : txtFlow(i) = New TextBox
            txtNo(i).Size = New Drawing.Size(30, 20) : txtProd(i).Size = New Drawing.Size(100, 20)
            txtNo(i).Location = New Point(noPos.X, noPos.Y + 25 * (i)) : txtNo(i).Text = i.ToString : txtNo(i).ReadOnly = True
            txtProd(i).Location = New Point(prodPos.X, prodPos.Y + 25 * (i)) : txtProd(i).Name = "prod" & i
            txtFlow(i).Location = New Point(flowPos.X, flowPos.Y + 25 * (i)) : txtFlow(i).Name = i : txtFlow(i).Width = 200
            Me.Controls.Add(txtNo(i)) : Me.Controls.Add(txtProd(i)) : Me.Controls.Add(txtFlow(i))
        Next
        btnContinue = New Button With {.Location = New Point(prodPos.X + 30, prodPos.Y + 25 * 16), .Width = 75, .Height = 30, .Text = "Continue"}
        btnPrint = New Button With {.Location = New Point(prodPos.X + 130, prodPos.Y + 25 * 16), .Width = 75, .Height = 30, .Text = "Print"}
        btnBack = New Button With {.Location = New Point(prodPos.X + 230, prodPos.Y + 25 * 16), .Width = 75, .Height = 30, .Text = "Back"}
        Me.Controls.Add(btnContinue) : Me.Controls.Add(btnPrint) : Me.Controls.Add(btnBack)
    End Sub
End Class