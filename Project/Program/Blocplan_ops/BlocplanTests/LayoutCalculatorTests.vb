Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BlocplanLogic

<TestClass()>
Public Class LayoutCalculatorTests
    <TestMethod()>
    Public Sub TestCalculateLayout_NormalCase()
        ' Arrange
        Dim layoutCalculator As New LayoutCalculator()
        Dim areas As New List(Of Single) From {100, 150, 200}
        Dim ratioX As Single = 1
        Dim ratioY As Single = 1
        Dim c1 As Integer = 1
        Dim c2 As Integer = 1
        Dim c3 As Integer = 1
        Dim departmentIndices As New List(Of Integer) From {0, 1, 2}
        Dim scores As Single(,) = {{0, 0.5, 0.8}, {0.5, 0, 0.3}, {0.8, 0.3, 0}}

        ' Act
        Dim result As LayoutCalculator.LayoutResult = layoutCalculator.CalculateLayout(areas, ratioX, ratioY, c1, c2, c3, departmentIndices, scores)

        ' Assert
        Assert.IsNotNull(result)
        Assert.AreEqual(3, result.XCoordinates.GetLength(0))
        Assert.AreEqual(3, result.YCoordinates.GetLength(0))
    End Sub

    <TestMethod()>
    Public Sub TestGenerateRandomLayout_NormalCase()
        ' Arrange
        Dim layoutCalculator As New LayoutCalculator()
        Dim depNo As Integer = 10

        ' Act
        Dim result As Tuple(Of Integer, Integer, Integer) = layoutCalculator.GenerateRandomLayout(depNo)

        ' Assert
        Assert.IsNotNull(result)
        Assert.AreEqual(depNo, result.Item1 + result.Item2 + result.Item3)
    End Sub

    <TestMethod()>
    Public Sub TestGenerateRandomLayout_SmallCase()
        ' Arrange
        Dim layoutCalculator As New LayoutCalculator()
        Dim depNo As Integer = 5

        ' Act
        Dim result As Tuple(Of Integer, Integer, Integer) = layoutCalculator.GenerateRandomLayout(depNo)

        ' Assert
        Assert.IsNotNull(result)
        Assert.AreEqual(depNo, result.Item1 + result.Item2 + result.Item3)
    End Sub

    <TestMethod()>
    Public Sub TestGenerateRandomLayout_LargeCase()
        ' Arrange
        Dim layoutCalculator As New LayoutCalculator()
        Dim depNo As Integer = 15

        ' Act
        Dim result As Tuple(Of Integer, Integer, Integer) = layoutCalculator.GenerateRandomLayout(depNo)

        ' Assert
        Assert.IsNotNull(result)
        Assert.AreEqual(depNo, result.Item1 + result.Item2 + result.Item3)
    End Sub
End Class
