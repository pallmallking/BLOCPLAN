Public Class scoreForm
    'Menetapkan variable2 spt form sebelumnya
    Dim firstPoint As New Point(10, 20)
    Dim blk, enterCode As Label
    Dim textNoV(18), textNoH(18), textDept(18) As TextBox
    Public Shared textVal(18, 18) As TextBox
    Public Shared count, scoreVal(18, 18), scoreA, scoreE, scoreI, scoreU, scoreO, scoreX As Integer
    Public Shared valid As Boolean
    Private tIndex As Integer
    Private Sub scoreForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        count = 0 : tIndex = 0
        scoreA = 10 : scoreE = 5 : scoreI = 2 : scoreO = 1 : scoreU = 0 : scoreX = -10
        'Menetapkan variable count sebagai penghitung textbox yang diisi nama department nya
        'Iterasi untuk menghitung Department Name yang diisi
        For n As Integer = 1 To 18
            If formDept.txtDept(n).Text <> "" Then
                count += 1
            End If
        Next
        'Menetapkan ukuran form
        Me.Size = New Size(650, 560)
        'Abi coba membuat Point awal untuk arah Vertikal dan Horizontal
        Dim firstHorizPoint As Point = New Point(firstPoint.X + 130, firstPoint.Y - 10)
        'Membuat textbox untuk Nomor secara Horizontal (Dimulai dari 2 karena tidak ada pasangan 1-1)
        For k As Integer = 2 To count
            textNoH(k) = New TextBox() With {.Location = New Point(firstHorizPoint.X + (k - 2) * 25, firstHorizPoint.Y), .Width = 20, .Text = k, _
                                             .Enabled = False}
            Me.Controls.Add(textNoH(k))
        Next
        'Membuat Textbox Nomor dan nama departement ke bawah (1..Count) / vertikal
        Dim thecount As Integer = 0
        For i As Integer = 1 To count
            textNoV(i) = New TextBox() With {.Location = New Point(firstPoint.X, firstPoint.Y + i * 20), .Width = 20, .Text = i, .Enabled = False}
            textDept(i) = New TextBox() With {.Location = New Point(firstPoint.X + 25, firstPoint.Y + i * 20), .Width = 100, .Text = formDept.txtDept(i).Text, .Enabled = False}
            Me.Controls.Add(textNoV(i)) : Me.Controls.Add(textDept(i))
            'Ke kanan (Horizontal) membuat textbox untuk isian Adjacent Score
            For j As Integer = 2 To count
                'Isian Adjacent Score selalu huruf KAPITAL
                textVal(i, j) = New TextBox() With {.Location = New Point(firstPoint.X + 130 + (j - 2) * 25, firstHorizPoint.Y + 10 + i * 20), .Width = 20, _
                                                    .MaxLength = 1, .CharacterCasing = CharacterCasing.Upper, .TabIndex = tIndex}
                blk = New Label With {.Location = New Point(firstPoint.X + 130 + (j - 2) * 25, firstHorizPoint.Y + 10 + i * 20), .Width = 20, .Text = "..."}
                If i >= j Then
                    Me.Controls.Add(blk)
                Else
                    tIndex += 1 : textVal(i, j).Name = i & "," & j
                    Me.Controls.Add(textVal(i, j))
                    'scoreA = 10 : scoreE = 5 : scoreI = 2 : scoreO = 1 : scoreU = 0 : scoreX = -10
                    If formDept.open_status Then
                        Select Case formDept.vec(thecount)
                            Case 10
                                textVal(i, j).Text = "A"
                                scoreVal(i, j) = 10
                            Case 5
                                textVal(i, j).Text = "E"
                                scoreVal(i, j) = 5
                            Case 2
                                textVal(i, j).Text = "I"
                                scoreVal(i, j) = 2
                            Case 1
                                textVal(i, j).Text = "O"
                                scoreVal(i, j) = 1
                            Case 0
                                textVal(i, j).Text = "U"
                                scoreVal(i, j) = 0
                            Case -10
                                textVal(i, j).Text = "X"
                                scoreVal(i, j) = -10
                        End Select
                        thecount += 1
                    End If
                    'Setiap kali diisikan string (A,E,I,O,U,X) diterjemahkan dengan angka yang disimpan dalam variabel array
                    AddHandler textVal(i, j).KeyPress, AddressOf textVal_value
                    AddHandler textVal(i, j).KeyUp, AddressOf scoreVal_value
                    AddHandler textVal(i, j).Click, AddressOf klik_this ' -- To test the validity of anything
                End If
            Next
        Next
        'Berikut ini membuat Label dan Textbox tambahan yang diperlukan dalam Form
        enterCode = New Label() With {.Location = New Point(firstPoint.X + 25, firstPoint.Y + 410), .Width = 1000, .Height = 25, _
                                      .Text = "Enter or change Code" & Space(19) & "A=Absokutely" & Space(35) & "I=Imediately" & Space(35) & _
                                       "U=Unimportant" & Chr(13) & Space(54) & "E=Essential" & Space(38) & "O=Ordinary" & Space(36) & "X=Undesirable"}
        Dim btnContinue As New Button With {.Location = New Point(firstPoint.X + 150, firstPoint.Y + 460), .Width = 75, .Height = 30, .Text = "Continue"}
        AddHandler btnContinue.Click, AddressOf continue_click
        'Dim btnPrint As New Button With {.Location = New Point(firstPoint.X + 250, firstPoint.Y + 460), .Width = 75, .Height = 30, .Text = "Print"}
        Dim btnScore As New Button With {.Location = New Point(firstPoint.X + 350, firstPoint.Y + 460), .Width = 75, .Height = 30, .Text = "Edit Score"}
        AddHandler btnScore.Click, AddressOf edit_score
        Me.Controls.Add(enterCode) : Me.Controls.Add(btnContinue) : Me.Controls.Add(btnScore) 'Me.Controls.Add(btnPrint)
    End Sub
    Private Sub continue_click(sender As Object, e As EventArgs)
        vectorScore.Show()
    End Sub
    'Sub prosedur untuk menguji pengisian yang Adjacent Score yang diijinkan 
    Private Sub textVal_value(sender As Object, e As KeyPressEventArgs)
        valid = False
        If e.KeyChar() <> ControlChars.Back Then
            Dim allowedChars As String = "aiueoxAIUEOX"
            If allowedChars.IndexOf(e.KeyChar) = -1 Then
                e.Handled = True
            Else
                valid = True
            End If
        End If
    End Sub
    Public Shared Sub scoreVal_value(sender As Object, e As KeyEventArgs)
        Dim thisbox As TextBox = sender
        Dim thisIndex As Integer = thisbox.TabIndex
        Dim nextIndex As Integer = thisIndex + 1
        Dim i = CInt(thisbox.Name.Substring(0, 1))
        Dim j = CInt(thisbox.Name.Substring(2, 1))
        Select Case thisbox.Text
            Case "A"
                scoreVal(i, j) = scoreA
            Case "E"
                scoreVal(i, j) = scoreE
            Case "I"
                scoreVal(i, j) = scoreI
            Case "O"
                scoreVal(i, j) = scoreO
            Case "U"
                scoreVal(i, j) = scoreU
            Case "X"
                scoreVal(i, j) = scoreX
        End Select
        If valid Then JumpToIndex(nextIndex)
    End Sub
    Public Shared Sub JumpToIndex(ByVal I As Integer)
        For Each control As Control In scoreForm.Controls
            If control.TabIndex = I Then
                control.Focus()
            End If
        Next
    End Sub
    ' sample of calling the subroutine

    Private Sub klik_this(sender As Object, e As EventArgs)
        Dim m As TextBox = sender
        'MessageBox.Show(valid)
    End Sub

    Private Sub edit_score(sender As Object, e As EventArgs)
        vectorValue.Show()
    End Sub

End Class