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
    Dim tempNum As Integer
    Dim list As New List(Of String)
    Dim onlyOne As New List(Of String)
    Dim items As New List(Of String)
    Dim categories As New List(Of String)
    Dim aisleNumbers As New List(Of Integer)
    Dim lines As String()
    Dim food As New List(Of (Aisle As String, Category As String, Item As String))
    Dim category As String
    Dim aisle As String
    Dim item As String
    Dim values As String()
    Dim currentLength As Integer


    'Loading the form and adding information to combo box
    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load


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
        lines = IO.File.ReadAllLines(filePath)
        For Each line As String In lines
            values = line.Split(",")
            If (values(0).Replace("$$ITM", "").Replace("""", "").Trim()).Length = 0 Then
            Else
                item = values(0).Replace("$$ITM", "").Replace("""", "").Trim() 'get the item name, remove any unnecessary characters and trim whitespace
            End If
        Next
        FilterByAisleRadioButton.Checked = True
    End Sub

    'Add items to display box
    Private Sub AddItems(item As String)
        If Not items.Contains(item) Then
            items.Add(item)
            FilterComboBox.Items.Add(item)
        End If
    End Sub

    'If the aisle number isn't already added will add it
    Private Sub AisleFilterAddIfChecked(aisle As String)
        categories.Clear()
        If FilterByAisleRadioButton.Checked = True Then
            If Not aisleNumbers.Contains(CInt(aisle)) Then
                SortAisles(aisle)
            End If
        End If
    End Sub

    'Adds the aisle numbers that have been filtered and sorted to the combo box
    Private Sub AisleFilterAdd()
        For i = 0 To aisleNumbers.Count - 1
            FilterComboBox.Items.Add(aisleNumbers(i))
        Next
    End Sub

    'Sorts the aisle numbers so they are in order numerically rather then in order of how they were added from the file
    Private Sub SortAisles(aisle As String)
        Try
            aisleNumbers.Add(CInt(aisle))
            currentLength = aisleNumbers.Count - 1
            For i = 0 To currentLength
                If aisleNumbers.Count > 0 And aisleNumbers(i) > CInt(aisle) Then
                    tempNum = aisleNumbers(currentLength)
                    aisleNumbers(currentLength) = aisleNumbers(i)
                    aisleNumbers(i) = tempNum
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    'Adds the catagories to the combo box so long as that catagory hasn't already been added before
    Private Sub CatagoryFilterAdd(category As String)
        aisleNumbers.Clear()
        If FilterByCatagoryRadioButton.Checked = True Then
            If category = vbNullString Then

            ElseIf Not categories.Contains(category) And category IsNot "" Then
                categories.Add(category)
                FilterComboBox.Items.Add(category)
            End If
        End If
    End Sub

    'Displays about form
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutMenuItem.Click
        Me.Hide()
        AboutForm.Show()
    End Sub

    'Closes all forms of the program
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

    'Makes it so only aisle numbers are displayed in the combo box
    Private Sub FilterByAisleRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles FilterByAisleRadioButton.CheckedChanged

        If FilterByAisleRadioButton.Checked = True Then
            FilterComboBox.Items.Clear()
            For Each line As String In lines
                values = line.Split(",")
                If (values(1).Replace("##LOC", "").Replace("""", "").Trim()).Length = 0 Then
                Else
                    aisle = values(1).Replace("##LOC", "").Replace("""", "").Trim() 'get the aisle number, remove any unnecessary characters and trim whitespace
                    AisleFilterAddIfChecked(aisle)
                End If
            Next
            AisleFilterAdd()
        End If
    End Sub

    'Makes it so only catagories are displayed in the combo box
    Private Sub FilterByCatagoryRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles FilterByCatagoryRadioButton.CheckedChanged

        If FilterByCatagoryRadioButton.Checked = True Then
            FilterComboBox.Items.Clear()
            For Each line As String In lines
                values = line.Split(",")
                If (values(2).Replace("%%CAT", "").Replace("""", "").Trim()).Length = 0 Then
                Else
                    category = values(2).Replace("%%CAT", "").Replace("""", "").Trim() 'get the category, remove any unnecessary characters and trim whitespace
                    CatagoryFilterAdd(category)
                End If
            Next
        End If
    End Sub

End Class