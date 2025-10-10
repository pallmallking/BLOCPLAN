Public Class DepartmentManager
    Public Class DepartmentStatistics
        Public Property Total As Double
        Public Property Average As Double
        Public Property StandardDeviation As Double
    End Class

    Public Function CalculateStatistics(areaTexts As List(Of String)) As DepartmentStatistics
        Dim total, totalSquare, stddev As Double
        total = 0
        totalSquare = 0
        Dim count As Integer = 0

        For Each areaText As String In areaTexts
            If String.IsNullOrWhiteSpace(areaText) Then
                ' Nothing
            Else
                Dim areaValue As Double
                If Double.TryParse(areaText, areaValue) Then
                    total += areaValue
                    totalSquare += areaValue * areaValue
                    count += 1
                End If
            End If
        Next

        If count > 1 Then
            stddev = Math.Sqrt((totalSquare - (total * total / count)) / (count -1))
        Else
            stddev = 0
        End If

        Dim stats As New DepartmentStatistics()
        stats.Total = Math.Round(total, 2)
        If count > 0 Then
            stats.Average = Math.Round(total / count, 2)
        Else
            stats.Average = 0
        End If
        stats.StandardDeviation = Math.Round(stddev, 2)

        Return stats
    End Function
End Class
