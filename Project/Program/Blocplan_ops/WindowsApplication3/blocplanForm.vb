Imports BlocplanLogic

Public Class formDept
    'Menetapkan variable isian (textbox) untuk nomor, namaDept, area, average, std dev, dan total area
    'Dibuat public shared agar variabel2 tsb (terutama nama dept dan areanya) bisa diakses dari form lainnya
    Public Shared txtNo(18), txtDept(18), txtArea(18), txtAvg, txtDev, txtTotal As TextBox
    'Menetapkan variable2 yang bertipe label (hanya text)
    Private lblNo, lblDept, lblArea, lblTotal, lblEnter, lblAvg, lblDev As Label
    'Menetapkan variable2 untuk menentukan posisi obyek di dlm form (Penting untuk script yang dibuat sendiri / tidak visual)
    Private noPos, deptPos, areaPos, lblTotalPos, totalPos, problemPos, enterPos, avgPos, devPos, continuePos, printPos, backPos As Point
    'Menetapkan variable2 untuk tipe button
    Private btnContinue, btnOpen, btnBack As Button
    Private openLayoutDialog As New OpenFileDialog
    Private department(), areawide(), vector(), bay(), deptIndex(), ratio() As String
    Private depNo As Integer
    Public Shared vec As Array : Public Shared open_status As Boolean = False

    Private departmentManager As New DepartmentManager()
    Private layoutFileManager As New LayoutFileManager()

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Menetapkan ukurang dari Form dan meletakknya di tengah layar
        Me.Size = New Size(500, 625) : Me.CenterToScreen()
        'Menetapkan posisi Label Number
        noPos = New Point(180, 30)
        'Mendefinisilan label untuk Number dengan posisi di atas (noPos)
        Dim lblNo As New Label With {.Text = "Number", .Location = noPos, .Width = 50}
        'Menetapkan posisi label department
        deptPos = New Point(noPos.X + lblNo.Width + 10, noPos.Y)
        'Mendefinisikan label Department
        Dim lblDept As New Label With {.Text = "Department", .Location = New Point(deptPos)}
        'Menetapkan posisi label Area
        areaPos = New Point(deptPos.X + lblDept.Width + 10, noPos.Y)
        'Mendefinisikan label Area
        Dim lblArea As New Label With {.Text = "Area", .Location = New Point(areaPos), .Width = 50}
        'Menambabkan label Number, Department, Area ke form yang aktif (Me)
        Me.Controls.Add(lblNo) : Me.Controls.Add(lblDept) : Me.Controls.Add(lblArea)
        'Menetapkan variable i untuk iterasi pembuatan textbox Number,Department dan Area (18)
        'Ini sifatnya optional --> tadinya untuk pengujian, hingga perlu diuji nilai "i" tsb
        Dim i As Integer
        'Memulai iterasi 1..18
        For i = 1 To 18
            'Menetapkan variable Number, Dept, Area (Kata New itu menandakan ini adalah obyek yang baru dibuat dgn definisi baru pula)
            txtNo(i) = New TextBox : txtDept(i) = New TextBox : txtArea(i) = New TextBox
            'Menetapkan ukuran tiap obyek
            txtNo(i).Size = New Drawing.Size(50, 20) : txtDept(i).Size = New Drawing.Size(100, 20) : txtArea(i).Size = txtNo(i).Size
            'Menetapkan lokasi tiap obyek --> ada yang dibuat ReadOnly
            txtNo(i).Location = New Point(noPos.X, noPos.Y + 25 * (i)) : txtNo(i).Text = i.ToString : txtNo(i).ReadOnly = True
            txtDept(i).Location = New Point(deptPos.X, deptPos.Y + 25 * (i)) : txtDept(i).Name = "dept" & i
            txtArea(i).Location = New Point(areaPos.X, areaPos.Y + 25 * (i)) : txtArea(i).Name = i
            'Menambahkan obyek ke form
            Me.Controls.Add(txtNo(i)) : Me.Controls.Add(txtDept(i)) : Me.Controls.Add(txtArea(i))
            'Menambankan Event ke pengisian text Area
            '1. Saat ditekan tombol (Keypress) dan menjalankan sub prosedur area_keypressed(yang ini dinamai sendiri)
            '2. Saat kursor meninggalkan textbox Area, menjalankan sub area_textchanged
            AddHandler txtArea(i).KeyPress, AddressOf area_keypressed
            AddHandler txtArea(i).Leave, AddressOf area_textchanged
        Next
        'Yang berikut ini prinsipnya sama dengan di atas ... Penambahan event hanya pada button Continue
        lblTotalPos = New Point(deptPos.X, deptPos.Y + 25 * i) : totalPos = New Point(areaPos.X, areaPos.Y + 25 * i)
        Dim lblTotal As New Label With {.Text = "Total", .Location = lblTotalPos, .Width = 100, .TextAlign = ContentAlignment.MiddleRight}
        txtTotal = New TextBox() With {.Location = totalPos, .Width = 100, .Text = ""}
        Me.Controls.Add(lblTotal) : Me.Controls.Add(txtTotal)
        ' For object out of input department
        problemPos = New Point(10, 100)
        Dim lblProblem As New Label With {.Text = "New problem", .Location = problemPos}
        enterPos = New Point(problemPos.X, problemPos.Y + 220)
        Dim lblEnter As New Label With {.Text = "Enter or modify problem data", .Location = enterPos}
        avgPos = New Point(problemPos.X, problemPos.Y + 360)
        Dim lblAvg As New Label With {.Text = "Average", .Location = avgPos, .Width = 80}
        txtAvg = New TextBox() With {.Location = New Point(avgPos.X + 90, avgPos.Y), .Width = 60, .Height = 20}
        devPos = New Point(problemPos.X, problemPos.Y + 390)
        Dim lblDev As New Label With {.Text = "Std Dev. Area", .Location = devPos, .Width = 80}
        txtDev = New TextBox() With {.Location = New Point(devPos.X + 90, devPos.Y), .Width = 60, .Height = 20}
        btnContinue = New Button With {.Location = New Point(devPos.X + 110, devPos.Y + 50), .Width = 75, .Height = 30, .Text = "Continue"}
        btnOpen = New Button With {.Location = New Point(devPos.X + 210, devPos.Y + 50), .Width = 75, .Height = 30, .Text = "Open"}
        AddHandler btnOpen.Click, AddressOf open_layout
        btnBack = New Button With {.Location = New Point(devPos.X + 310, devPos.Y + 50), .Width = 75, .Height = 30, .Text = "Back"}
        AddHandler btnContinue.Click, AddressOf continue_click
        Me.Controls.Add(lblProblem) : Me.Controls.Add(lblEnter) : Me.Controls.Add(lblEnter) : Me.Controls.Add(lblAvg) : Me.Controls.Add(txtAvg)
        Me.Controls.Add(lblDev) : Me.Controls.Add(txtDev) : Me.Controls.Add(btnContinue) : Me.Controls.Add(btnOpen) : Me.Controls.Add(btnBack)
    End Sub
    'Berikut ini adalah sub area_keypressed --> untuk menguji tombol apa yang ditekan saat mengisi textbox area
    'Ada 2 tipe parameter
    '1. ByVal sender As Object --> Mewakili obyek yang diisi saat ini
    '2. ByVal e As System.Windows.Forms.KeyPressEventArgs --> Mewakili tombol apa yang ditekan
    Private Sub area_keypressed(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Jika tombol yang ditekan bukan BackSpace maka lakukan pengujian berikut
        If e.KeyChar <> ControlChars.Back Then
            'Menetapkan karakter apa saja yang boleh ditekan (abi kasih . dan , karena kemarin bermasalah ketika mengisi dng nilai pecahan)
            'Abi pakai 4.5 --> terbacanya 45 --> setelah abi ganti 4,5 baru dibaca pecahan
            'Ini yang bisa didiskusikan dengan dosen --> masalah localization (kode untuk tiap negara berbeda)
            Dim allowedChars As String = "0123456789.,"
            'Jika yang masuk seperti yang disyaratkan di atas maka karakter diterima (e.Handled=True)
            'Tombol yang lain gak bisa masuk kecuali Backspace tadi
            If allowedChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub
    'Berikut ini adalah sub untuk pengujian pengisian Area
    'Yang dipengaruhi antara lain textbox Department, Total, Average dan Std Deviation
    'Parameternya disamping objectnya sendiri, juga e sebagai EvenArgs (Evennya = Leave ... saat kursor meninggalkan textbox)
    Private Sub area_textchanged(sender As Object, e As EventArgs)
        'Menetapkan variabel area sebagai textbox yang menampung isian saat ini (sender)
        Dim area As TextBox = sender
        'Jika kursor meninggalkan textbox area, maka diberi peringatan (MessageBox), lalu meletakkan kursor di textbox department
        If txtDept(area.Name).Text = "" Then
            MessageBox.Show("Department Name must be filled")
            txtDept(area.Name).Focus()
        End If

        Dim areaTexts As New List(Of String)
        For i As Integer = 1 To 18
            areaTexts.Add(txtArea(i).Text)
        Next

        Dim stats As DepartmentManager.DepartmentStatistics = departmentManager.CalculateStatistics(areaTexts)

        txtTotal.Text = stats.Total.ToString()
        txtAvg.Text = stats.Average.ToString()
        txtDev.Text = stats.StandardDeviation.ToString()
    End Sub

    Private Sub continue_click(sender As Object, e As EventArgs)
        scoreForm.Show()
    End Sub

    Private Sub open_layout(sender As Object, e As EventArgs)
        openLayoutDialog.Filter = "TXT Files (*.txt*)|*.txt"
        If openLayoutDialog.ShowDialog = DialogResult.OK Then
            Dim fileLines As String() = IO.File.ReadAllLines(openLayoutDialog.FileName)
            Dim layoutData As LayoutFileManager.LayoutData = layoutFileManager.ParseLayoutFile(fileLines)

            depNo = layoutData.Departments.Count
            For i = 0 To depNo - 1
                txtDept(i + 1).Text = layoutData.Departments(i)
                txtArea(i + 1).Text = layoutData.Areas(i)
            Next

            vec = layoutData.Vector.ToArray()
            open_status = True

            ratioForm.ratioX = layoutData.RatioX
            ratioForm.ratioY = layoutData.RatioY
        End If
    End Sub

End Class
