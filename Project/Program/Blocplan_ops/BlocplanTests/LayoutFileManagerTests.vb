Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BlocplanLogic
Imports System.IO

<TestClass()>
Public Class LayoutFileManagerTests
    <TestMethod()>
    Public Sub TestParseLayoutFile()
        ' Arrange
        Dim fileManager As New LayoutFileManager()
        Dim fileLines As String() = {
            "dept:d1,d2,d3",
            "area:100,150,200",
            "vector:0.5,0.8,0.3",
            "bay:1,1,1",
            "deptIndex:0,1,2",
            "ratio:1/1"
        }

        ' Act
        Dim layoutData As LayoutFileManager.LayoutData = fileManager.ParseLayoutFile(fileLines)

        ' Assert
        Assert.IsNotNull(layoutData)
        Assert.AreEqual(3, layoutData.Departments.Count)
        Assert.AreEqual("d1", layoutData.Departments(0))
        Assert.AreEqual("1", layoutData.RatioX)
        Assert.AreEqual("1", layoutData.RatioY)
    End Sub

    <TestMethod()>
    Public Sub TestSaveAndOpenLayoutFile()
        ' Arrange
        Dim fileManager As New LayoutFileManager()
        Dim layoutData As New LayoutFileManager.LayoutData()
        layoutData.Departments = New List(Of String) From {"d1", "d2"}
        layoutData.Areas = New List(Of String) From {"100", "200"}
        layoutData.Vector = New List(Of String) From {"0.5"}
        layoutData.Bay = New List(Of String) From {"1", "1"}
        layoutData.DeptIndex = New List(Of String) From {"0", "1"}
        layoutData.RatioX = "1"
        layoutData.RatioY = "1"
        Dim filePath As String = "test_layout.txt"

        ' Act
        fileManager.SaveLayoutFile(filePath, layoutData)
        Dim loadedLayoutData As LayoutFileManager.LayoutData = fileManager.OpenLayoutFile(filePath)

        ' Assert
        Assert.IsNotNull(loadedLayoutData)
        Assert.AreEqual(layoutData.Departments.Count, loadedLayoutData.Departments.Count)
        Assert.AreEqual(layoutData.Departments(0), loadedLayoutData.Departments(0))

        ' Clean up
        File.Delete(filePath)
    End Sub
End Class
