Public Class LayoutFileManager
    Public Class LayoutData
        Public Property Departments As List(Of String)
        Public Property Areas As List(Of String)
        Public Property Vector As List(Of String)
        Public Property Bay As List(Of String)
        Public Property DeptIndex As List(Of String)
        Public Property RatioX As String
        Public Property RatioY As String
    End Class

    Public Function ParseLayoutFile(fileLines As String()) As LayoutData
        Dim layoutData As New LayoutData()
        layoutData.Departments = New List(Of String)()
        layoutData.Areas = New List(Of String)()
        layoutData.Vector = New List(Of String)()
        layoutData.Bay = New List(Of String)()
        layoutData.DeptIndex = New List(Of String)()

        For Each line As String In fileLines
            Dim parts As String() = line.Split(":")
            If parts.Length = 2 Then
                Dim key As String = parts(0).Trim()
                Dim value As String = parts(1).Trim()

                Select Case key
                    Case "dept"
                        layoutData.Departments.AddRange(value.Split(","))
                    Case "area"
                        layoutData.Areas.AddRange(value.Split(","))
                    Case "vector"
                        layoutData.Vector.AddRange(value.Split(","))
                    Case "bay"
                        layoutData.Bay.AddRange(value.Split(","))
                    Case "deptIndex"
                        layoutData.DeptIndex.AddRange(value.Split(","))
                    Case "ratio"
                        Dim ratioParts As String() = value.Split("/")
                        If ratioParts.Length = 2 Then
                            layoutData.RatioX = ratioParts(0)
                            layoutData.RatioY = ratioParts(1)
                        End If
                End Select
            End If
        Next

        Return layoutData
    End Function

    Public Sub SaveLayoutFile(filePath As String, layoutData As LayoutData)
        Dim lines As New List(Of String)()
        lines.Add("dept:" & String.Join(",", layoutData.Departments))
        lines.Add("area:" & String.Join(",", layoutData.Areas))
        lines.Add("vector:" & String.Join(",", layoutData.Vector))
        lines.Add("bay:" & String.Join(",", layoutData.Bay))
        lines.Add("deptIndex:" & String.Join(",", layoutData.DeptIndex))
        lines.Add("ratio:" & layoutData.RatioX & "/" & layoutData.RatioY)
        System.IO.File.WriteAllLines(filePath, lines)
    End Sub

    Public Function OpenLayoutFile(filePath As String) As LayoutData
        Dim lines As String() = System.IO.File.ReadAllLines(filePath)
        Return ParseLayoutFile(lines)
    End Function
End Class
