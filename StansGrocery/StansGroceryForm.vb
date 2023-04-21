'Isabella Dougherty
'RCET0265
'Spring 2023
'Stans Grocery
'https://github.com/IsabellaDougherty/StansGrocery.git

Option Strict On
Option Explicit On

Imports System.IO.Enumeration
Imports System.IO
Imports Microsoft.VisualBasic.Devices

Public Class StansGroceryForm

    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fileDialog As OpenFileDialog
        Dim fileReader As String
        Dim filePath As String '= "C:\Users\bella\Downloads\Github School Assignments\StansGroceryStore\StansGrocery\Resources\Grocery.txt"
        Dim fileNumber As Integer = FreeFile()
        Dim repeat As Boolean = True
        Dim temp As String
        Dim currentRow As Integer

        SplashScreenForm.Show()
        Me.Hide()

        While repeat
            Try
                FileSystem.FileOpen(fileNumber, filePath, OpenMode.Input) 'opens the grocery.txt or whatever file is chosen by user
                repeat = False
            Catch ex As Exception
                Dim yesNo As Integer
                yesNo = MsgBox("File not found. Would you like to select one?", CType(vbQuestion + vbYesNo + vbDefaultButton2, MsgBoxStyle), "File Not Found")
                If yesNo = vbYes Then
                    fileDialog = OpenFileDialog()
                    fileDialog.ShowDialog()
                    filePath = fileDialog.FileName 'sets chosen file path
                Else
                    Me.Close()
                    SplashScreenForm.Close()
                    AboutForm.Close()
                    End
                End If
            End Try
        End While

        Dim lines() As String = File.ReadAllLines(filePath)
        Static groceries(lines.Length - 1, 2) As String
        For i As Integer = 0 To lines.Length - 1
            Dim values() As String = lines(i).Split(","c)
            groceries(i, 0) = values(0) ' item name
            groceries(i, 1) = values(1) ' item aisle
            groceries(i, 2) = values(2) ' item category
        Next

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutMenuItem.Click
        Me.Hide()
        AboutForm.Show()
    End Sub

    Private Sub ExitMenuItem_Click_1(sender As Object, e As EventArgs) Handles ExitMenuItem.Click
        Dim yesNo As Integer
        yesNo = MsgBox("Are you sure you would like to quit?", CType(vbQuestion + vbYesNo + vbDefaultButton2, MsgBoxStyle), "Exit Menu")
        If yesNo = vbYes Then
            Me.Close()
            SplashScreenForm.Close()
            AboutForm.Close()
            End
        End If
    End Sub
End Class
