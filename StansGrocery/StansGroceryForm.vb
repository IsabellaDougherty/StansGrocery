Public Class StansGroceryForm
    Dim itemList(3, 9000) As String
    Dim groceries() As String


    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fileName As String = My.Computer.FileSystem.GetName("C:\Users\bella\Downloads\Github School Assignments\StansGroceryStore\StansGrocery\Resources\Grocery.txt")
        Dim fileNumber As Integer = FreeFile()
        Dim temp As String
        Dim currentRow As Integer

        SplashScreenForm.Show()
        Me.Hide()

        Do Until EOF(fileNumber)
            temp = LineInput(fileNumber)
            groceries = Split(temp, ",")

            Me.itemList(0, currentRow) = Strings.Right(groceries(0), Len(groceries(0)) - 3)
            Me.itemList(1, currentRow) = groceries(1)
            Me.itemList(2, currentRow) = groceries(2)
            Me.itemList(3, currentRow) = groceries(3)
            currentRow += 1
        Loop

        Try
            FileOpen(fileNumber, fileName, OpenMode.Input)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutMenuItem.Click
        Me.Hide()
        AboutForm.Show()
    End Sub

    Private Sub ExitMenuItem_Click_1(sender As Object, e As EventArgs) Handles ExitMenuItem.Click
        Dim yesNo As Integer
        yesNo = MsgBox(“Are you sure you would like to quit?”, vbQuestion + vbYesNo + vbDefaultButton2, “Exit Menu”)
        If yesNo = vbYes Then
            Me.Close()
            SplashScreenForm.Close()
            AboutForm.Close()
            End
        End If
    End Sub
End Class
