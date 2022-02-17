Public Class vectorScore
    Dim firstPoint As New Point(10, 20)
    Dim lblNo, lblDept, lblScore, lblTot As Label
    Dim textNo(18), textDept(18), textVal(18, 18), score(18), totVal(18) As TextBox
    'Private scoreVal(18, 18) As Integer
    Private thisCount As Integer = scoreForm.count
    Shared column, row As Integer
    Private Sub scoreForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(450, 590)
        lblNo = New Label() With {.Location = New Point(firstPoint.X, firstPoint.Y), .Text = "No"}
        lblDept = New Label() With {.Location = New Point(firstPoint.X + 25, firstPoint.Y), .Text = "Department"}
        lblScore = New Label() With {.Location = New Point(firstPoint.X + 130, firstPoint.Y), .Text = "Score"}
        lblTot = New Label() With {.Location = New Point(firstPoint.X + 300, firstPoint.Y), .Text = "Value"}
        Me.Controls.Add(lblNo) : Me.Controls.Add(lblDept) : Me.Controls.Add(lblScore) : Me.Controls.Add(lblTot)
        For n = 1 To thisCount
            textNo(n) = New TextBox() With {.Location = New Point(firstPoint.X, firstPoint.Y + n * 20), .Width = 20, .Text = n}
            textDept(n) = New TextBox() With {.Location = New Point(firstPoint.X + 25, firstPoint.Y + n * 20), .Width = 100, .Text = formDept.txtDept(n).Text}
            score(n) = New TextBox() With {.Location = New Point(firstPoint.X + 130, firstPoint.Y + n * 20), .Width = 150, .ReadOnly = True}
            totVal(n) = New TextBox() With {.Location = New Point(firstPoint.X + 300, firstPoint.Y + n * 20), .Width = 50, .ReadOnly = True, .Text = 0}
            Me.Controls.Add(textNo(n)) : Me.Controls.Add(textDept(n))
            Me.Controls.Add(score(n)) : Me.Controls.Add(totVal(n))
        Next
        Dim firstHorizPoint As Point = New Point(firstPoint.X + 130, firstPoint.Y - 10)
        Dim btnContinue As New Button With {.Location = New Point(firstPoint.X + 100, firstPoint.Y + 460), .Width = 75, .Height = 30, .Text = "Continue"}
        'Dim btnCount As New Button With {.Location = New Point(firstPoint.X + 200, firstPoint.Y + 460), .Width = 75, .Height = 30, .Text = "Count Score"}
        AddHandler btnContinue.Click, AddressOf ratio_form
        Me.Controls.Add(btnContinue) 'Me.Controls.Add(btnCount)   'Me.Controls.Add(enterCode)
        count_click()
    End Sub
    Private Sub textVal_value(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar <> ControlChars.Back Then
            Dim allowedChars As String = "aiueoxAIUEOX"
            If allowedChars.IndexOf(e.KeyChar) = -1 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub count_click()
        'Clearing the score and total value before counting
        For i = 1 To thisCount
            score(i).Text = ""
            totVal(i).Text = "0"
        Next
        'thisCount score Value for Columns
        Dim n As Integer
        For i = 1 To thisCount
            For j = 2 To thisCount
                If i >= j Then
                    'nothing
                Else
                    For k = i To j
                        If i = k Then
                            score(j).Text += scoreForm.scoreVal(i, j) & ","
                            n = Convert.ToInt32(totVal(j).Text)
                            n += scoreForm.scoreVal(i, j)
                            totVal(j).Text = n
                        End If
                    Next
                End If
            Next
        Next
        'Count score Value for Rows
        For j = 2 To thisCount
            For i = 1 To thisCount
                If i >= j Then
                    'nothing
                Else
                    For k = i To j - 1
                        If i = k Then
                            score(i).Text += scoreForm.scoreVal(i, j) & ","
                            n = Convert.ToInt32(totVal(i).Text)
                            n += scoreForm.scoreVal(i, j)
                            totVal(i).Text = n
                        End If
                    Next
                End If
            Next
        Next
    End Sub

    Private Sub ratio_form(sender As Object, e As EventArgs)
        ratioForm.Show()
    End Sub

End Class
