Public Class LayoutCalculator
    Public Class LayoutResult
        Public Property XCoordinates As Single(,)
        Public Property YCoordinates As Single(,)
        Public Property Centers As System.Drawing.Point()
        Public Property LowerBound As Single
        Public Property UpperBound As Single
        Public Property RScore As Single
    End Class

    Public Function CalculateLayout(
        areas As List(Of Single),
        ratioX As Single, ratioY As Single,
        c1 As Integer, c2 As Integer, c3 As Integer,
        departmentIndices As List(Of Integer),
        scores As Single(,)) As LayoutResult
        Dim depNo As Integer = areas.Count
        Dim calib As Single = 540 / Math.Sqrt(areas.Sum())
        Dim myWidth As Single = Math.Sqrt((ratioX / ratioY) * areas.Sum())
        Dim myHeight As Single = Math.Sqrt((ratioY / ratioX) * areas.Sum())

        Dim wid(3), len(depNo), wi(depNo) As Single
        Dim x(depNo, 2), y(depNo, 2) As Single
        Dim center(depNo) As System.Drawing.Point
        Dim ctx(depNo), cty(depNo) As Single
        Dim lBound, uBound, rScore, relDist As Single

        Dim index As Integer = 0
        wid(0) = 0 : wid(1) = 0 : wid(2) = 0
        For k = 0 To c1 - 1
            wid(0) += areas(departmentIndices(index))
            index += 1
        Next
        For l = 0 To c2 - 1
            wid(1) += areas(departmentIndices(index))
            index += 1
        Next
        For m = 0 To c3 - 1
            wid(2) += areas(departmentIndices(index))
            index += 1
        Next

        wid(0) = Math.Round(wid(0) / myWidth, 3)
        wid(1) = Math.Round(wid(1) / myWidth, 3)
        wid(2) = Math.Round(wid(2) / myWidth, 3)

        index = 0
        For k = 0 To c1 - 1
            len(departmentIndices(index)) = areas(departmentIndices(index)) / wid(0)
            wi(departmentIndices(index)) = wid(0)
            index += 1
        Next
        For l = 0 To c2 - 1
            len(departmentIndices(index)) = areas(departmentIndices(index)) / wid(1)
            wi(departmentIndices(index)) = wid(1)
            index += 1
        Next
        For m = 0 To c3 - 1
            len(departmentIndices(index)) = areas(departmentIndices(index)) / wid(2)
            wi(departmentIndices(index)) = wid(2)
            index += 1
        Next

        x(0, 0) = 0 : y(0, 0) = 0
        index = 0
        For k = 0 To c1 - 1
            If k = 0 Then
                x(index, 0) = x(0, 0)
                y(index, 0) = y(0, 0)
            Else
                x(index, 0) = x(index - 1, 1)
                y(index, 0) = y(index - 1, 0)
            End If
            x(index, 1) = x(index, 0) + len(departmentIndices(index)) * calib
            y(index, 1) = y(index, 0) + wid(0) * calib
            index += 1
        Next
        For l = 0 To c2 - 1
            If l = 0 Then
                x(index, 0) = x(0, 0)
                y(index, 0) = y(0, 0) + wid(0) * calib
            Else
                x(index, 0) = x(index - 1, 1)
                y(index, 0) = y(0, 0) + wid(0) * calib
            End If
            x(index, 1) = x(index, 0) + len(departmentIndices(index)) * calib
            y(index, 1) = y(index, 0) + wid(1) * calib
            index += 1
        Next
        For m = 0 To c3 - 1
            If m = 0 Then
                x(index, 0) = x(0, 0)
                y(index, 0) = y(0, 0) + (wid(0) + wid(1)) * calib
            Else
                x(index, 0) = x(index - 1, 1)
                y(index, 0) = y(0, 0) + (wid(0) + wid(1)) * calib
            End If
            x(index, 1) = x(index, 0) + len(departmentIndices(index)) * calib
            y(index, 1) = y(index, 0) + wid(2) * calib
            index += 1
        Next

        For i = 0 To depNo - 1
            center(i) = New System.Drawing.Point(x(i, 0) + (x(i, 1) - x(i, 0)) / 2, y(i, 0) + (y(i, 1) - y(i, 0)) / 2)
            ctx(i) = center(i).X / calib
            cty(i) = center(i).Y / calib
        Next

        Dim distList As New List(Of Single)()
        Dim relList As New List(Of Single)()
        For i = 0 To depNo - 2
            For j = i + 1 To depNo - 1
                relList.Add(scores(i, j))
                distList.Add(CSng(Math.Abs(ctx(i) - ctx(j)) + Math.Abs(cty(i) - cty(j))))
            Next
        Next

        relDist = 0
        For i = 0 To relList.Count - 1
            relDist += relList(i) * distList(i)
        Next

        distList.Sort()
        relList.Sort()
        relList.Reverse()

        lBound = 0
        For i = 0 To relList.Count - 1
            lBound += relList(i) * distList(i)
        Next

        distList.Reverse()
        uBound = 0
        For i = 0 To relList.Count - 1
            uBound += relList(i) * distList(i)
        Next

        rScore = 1 - (relDist - lBound) / (uBound - lBound)

        Dim result As New LayoutResult()
        result.XCoordinates = x
        result.YCoordinates = y
        result.Centers = center
        result.LowerBound = lBound
        result.UpperBound = uBound
        result.RScore = rScore
        Return result
    End Function

    Public Function GenerateRandomLayout(depNo As Integer) As Tuple(Of Integer, Integer, Integer)
        Dim rnd As New Random()
        Dim c1, c2, c3 As Integer
        If depNo > 12 Then
            c1 = rnd.Next(depNo - 12, 7) ' Corrected upper bound
            If (c1 < 6) Then
                c2 = 6
            Else
                c2 = rnd.Next(depNo - 12, 7) ' Corrected upper bound
            End If
            c3 = depNo - c1 - c2
        ElseIf depNo > 6 And depNo <= 12 Then
            c1 = rnd.Next(1, 7) ' Corrected upper bound
            If ((depNo - c1) >= 6) Then
                c2 = rnd.Next(1, 7) ' Corrected upper bound
            Else
                c2 = rnd.Next(1, depNo - c1 + 1) ' Corrected upper bound
            End If
            c3 = depNo - c1 - c2
        ElseIf depNo <= 6 Then
            c1 = rnd.Next(1, depNo + 1) ' Corrected upper bound
            If (c1 = depNo) Then
                c2 = 0
            Else
                c2 = rnd.Next(1, depNo - c1 + 1) ' Corrected upper bound
            End If
            If ((depNo - c1 - c2) < 0) Then
                c3 = 0
            Else
                c3 = depNo - c1 - c2
            End If
        End If
        Return New Tuple(Of Integer, Integer, Integer)(c1, c2, c3)
    End Function
End Class
