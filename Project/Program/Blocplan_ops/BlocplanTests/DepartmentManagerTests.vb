Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BlocplanLogic

<TestClass()>
Public Class DepartmentManagerTests
    <TestMethod()>
    Public Sub TestCalculateStatistics_NormalCase()
        ' Arrange
        Dim deptManager As New DepartmentManager()
        Dim areas As New List(Of String) From {"10", "20", "30"}

        ' Act
        Dim stats As DepartmentManager.DepartmentStatistics = deptManager.CalculateStatistics(areas)

        ' Assert
        Assert.AreEqual(60, stats.Total)
        Assert.AreEqual(20, stats.Average)
        Assert.AreEqual(10, stats.StandardDeviation)
    End Sub

    <TestMethod()>
    Public Sub TestCalculateStatistics_EmptyAndInvalidValues()
        ' Arrange
        Dim deptManager As New DepartmentManager()
        Dim areas As New List(Of String) From {"10", "", "abc", "20"}

        ' Act
        Dim stats As DepartmentManager.DepartmentStatistics = deptManager.CalculateStatistics(areas)

        ' Assert
        Assert.AreEqual(30, stats.Total)
        Assert.AreEqual(15, stats.Average)
        Assert.AreEqual(7.07, stats.StandardDeviation)
    End Sub

    <TestMethod()>
    Public Sub TestCalculateStatistics_SingleValue()
        ' Arrange
        Dim deptManager As New DepartmentManager()
        Dim areas As New List(Of String) From {"10"}

        ' Act
        Dim stats As DepartmentManager.DepartmentStatistics = deptManager.CalculateStatistics(areas)

        ' Assert
        Assert.AreEqual(10, stats.Total)
        Assert.AreEqual(10, stats.Average)
        Assert.AreEqual(0, stats.StandardDeviation)
    End Sub

    <TestMethod()>
    Public Sub TestCalculateStatistics_NoValues()
        ' Arrange
        Dim deptManager As New DepartmentManager()
        Dim areas As New List(Of String)()

        ' Act
        Dim stats As DepartmentManager.DepartmentStatistics = deptManager.CalculateStatistics(areas)

        ' Assert
        Assert.AreEqual(0, stats.Total)
        Assert.AreEqual(0, stats.Average)
        Assert.AreEqual(0, stats.StandardDeviation)
    End Sub
End Class
