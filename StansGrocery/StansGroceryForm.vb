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
Imports System.Windows.Forms.LinkLabel
Imports System.Windows
Imports System.Net.WebRequestMethods

Public Class StansGroceryForm
    Dim fileDialog As OpenFileDialog
    Dim filePath As String = "E:\GitHub\StansGrocery\StansGrocery\Resources\Grocery.txt"
    Dim repeat As Boolean = True
    Dim list As New List(Of String)
    Dim onlyOne As New List(Of String)
    Dim items As New HashSet(Of String)
    Dim categories As New HashSet(Of String)
    Dim aisleNumbers As New HashSet(Of String)
    Dim lines As String() = IO.File.ReadAllLines(filePath)
    Dim food As New List(Of (Aisle As String, Category As String, Item As String))
    Dim category As String
    Dim aisle As String
    Dim item As String
    Dim values As String()
    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FilterByAisleRadioButton.Checked = True

        While repeat
            Try
                FileSystem.FileOpen(1, filePath, OpenMode.Input) 'opens the grocery.txt or whatever file is chosen by user
                While Not EOF(1)
                    Dim line As String = LineInput(1)
                    list.Add(line)
                End While
                FileSystem.FileClose(1) 'closes the file
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

        For Each line As String In lines
            values = line.Split(",")
            If (values(0).Replace("$$ITM", "").Replace("*", "").Trim()).Length = 0 Then
            Else
                item = values(0).Replace("$$ITM", "").Replace("*", "").Trim() 'get the item name, remove any unnecessary characters and trim whitespace
            End If
        Next
    End Sub

    Private Sub AddItems(item As String)
        If Not items.Contains(item) Then
            items.Add(item)
            FilterComboBox.Items.Add(item)
        End If
    End Sub
    Private Sub AisleFilterAdd(aisle As String)
        categories.Clear()
        If FilterByAisleRadioButton.Checked = True Then
            If Not aisleNumbers.Contains(aisle) Then
                aisleNumbers.Add(aisle)
                FilterComboBox.Items.Add(aisle)
            End If
        End If
    End Sub
    Private Sub CatagoryFilterAdd(category As String)
        aisleNumbers.Clear()
        If FilterByCatagoryRadioButton.Checked = True Then
            If Not categories.Contains(category) Then
                categories.Add(category)
                FilterComboBox.Items.Add(category)
            End If
        End If
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

    Private Sub FilterByAisleRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles FilterByAisleRadioButton.CheckedChanged

        If FilterByAisleRadioButton.Checked = True Then
            FilterComboBox.Items.Clear()
            For Each line As String In lines
                values = line.Split(",")
                If (values(1).Replace("##LOC", "").Replace("*", "").Trim()).Length = 0 Then
                Else
                    aisle = values(1).Replace("##LOC", "").Replace("*", "").Trim() 'get the aisle number, remove any unnecessary characters and trim whitespace
                    AisleFilterAdd(aisle)
                End If
            Next
        End If
    End Sub

    Private Sub FilterByCatagoryRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles FilterByCatagoryRadioButton.CheckedChanged

        If FilterByCatagoryRadioButton.Checked = True Then
            FilterComboBox.Items.Clear()
            For Each line As String In lines
                values = line.Split(",")
                If (values(2).Replace("%%CAT", "").Replace("*", "").Trim()).Length = 0 Then
                Else
                    category = values(2).Replace("%%CAT", "").Replace("*", "").Trim() 'get the category, remove any unnecessary characters and trim whitespace
                    CatagoryFilterAdd(category)
                End If
            Next
        End If
    End Sub

End Class
