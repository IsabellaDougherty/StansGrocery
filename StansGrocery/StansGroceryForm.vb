﻿'Isabella Dougherty
'RCET0265
'Spring 2023
'Stans Grocery
'https://github.com/IsabellaDougherty/StansGrocery.git

Option Strict On
Option Explicit On

Imports System.IO.Enumeration
Imports System.IO
Imports Microsoft.VisualBasic.Devices
Imports System.Windows
Imports System.Windows.Markup

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
    Dim itm As String
    Dim values As String()
    Dim products As String()
    Dim currentLength As Integer
    Dim countOne As Integer = 0
    Dim countTwo As Integer = 0
    Dim catalog(,) As String



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
                CreateCatalog(values)
                ReDim Preserve products(countOne)
                products(countOne) = values(0).Replace("$$ITM", "").Replace("""", "").Trim() 'get the item name, remove any unnecessary characters and trim whitespace
                DisplayListBox.Items.Add(products(countOne))
                countOne += 1
            End If
        Next
        ShowAllRadioButton.Checked = True
        DisplayListBox.Sorted = True
    End Sub

    'Takes the in put of the search text button and pulls up all items available from the 
    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        If SearchTextBox.Text = "zzz" Then
            Dim yesNo As Integer
            yesNo = MsgBox("Are you sure you would like to quit?", CType(vbQuestion + vbYesNo + vbDefaultButton2, MsgBoxStyle), "Exit Menu")
            If yesNo = vbYes Then
                Me.Close()
                SplashScreenForm.Close()
                AboutForm.Close()
                End
            End If
        Else
            DisplayListBox.Items.Clear()
            If products IsNot Nothing AndAlso products.Length > 0 Then
                For Each product In products
                    If product.IndexOf(SearchTextBox.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 Then
                        DisplayListBox.Items.Add(product)
                    End If
                Next
                If DisplayListBox.Items.Count = 0 Then
                    MsgBox("No matching items found.")
                End If
                DisplayListBox.Refresh()
            End If
        End If
        FilterItems()
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

    Private Sub ShowAllRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles ShowAllRadioButton.CheckedChanged
        FilterComboBox.Items.Clear()
        categories.Clear()
        aisleNumbers.Clear()
        For Each line As String In lines
            values = line.Split(",")
            If (values(2).Replace("%%CAT", "").Replace("""", "").Trim()).Length = 0 Then
            Else
                category = values(2).Replace("%%CAT", "").Replace("""", "").Trim() 'get the category, remove any unnecessary characters and trim whitespace
                CatagoryFilterAdd(category)
            End If
        Next
        FilterComboBox.Sorted = True
        FilterComboBox.Sorted = False
        For Each line As String In lines
            values = line.Split(",")
            If (values(1).Replace("##LOC", "").Replace("""", "").Trim()).Length = 0 Then
            Else
                aisle = values(1).Replace("##LOC", "").Replace("""", "").Trim() 'get the aisle number, remove any unnecessary characters and trim whitespace
                AisleFilterAddIfChecked(aisle)
            End If
        Next
        AisleFilterAdd()
    End Sub

    'Makes it so only aisle numbers are displayed in the combo box
    Private Sub FilterByAisleRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles FilterByAisleRadioButton.CheckedChanged

        If FilterByAisleRadioButton.Checked = True Then
            FilterComboBox.Sorted = False
            FilterComboBox.Items.Clear()
            categories.Clear()
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
            aisleNumbers.Clear()
            categories.Clear()
            FilterComboBox.Items.Clear()
            For Each line As String In lines
                values = line.Split(",")
                If (values(2).Replace("%%CAT", "").Replace("""", "").Trim()).Length = 0 Then
                Else
                    category = values(2).Replace("%%CAT", "").Replace("""", "").Trim() 'get the category, remove any unnecessary characters and trim whitespace
                    CatagoryFilterAdd(category)
                End If
            Next
            FilterComboBox.Sorted = True
        End If

    End Sub

    'Shows what aisle and category the item selected is located in
    Private Sub DisplayListBox_ItemSelected(sender As Object, e As EventArgs) Handles DisplayListBox.SelectedValueChanged
        Dim itemNumber As New Integer
        Dim itemSelected As String = "(Not being assigned)"
        Dim aisleStated As String = "(Not being assigned)"
        Dim catagoryStated As String = "(Not being assigned)"
        Dim listBoxText As String = CStr(DisplayListBox.SelectedItem)
        Dim displayText = "You will find "

        For i = 0 To catalog.GetLength(1) - 1
            If catalog(0, i) = listBoxText Then
                itemNumber = i
            End If
        Next

        If catalog(0, itemNumber) Is "" Then
            itemSelected = "(item not found)"
        Else
            itemSelected = catalog(0, itemNumber)
        End If

        If catalog(1, itemNumber) Is "" Then
            aisleStated = "(aisle not found)"
        Else
            aisleStated = catalog(1, itemNumber)
        End If

        If catalog(2, itemNumber) Is "" Then
            catagoryStated = "(catagory not found)"
        Else
            catagoryStated = catalog(2, itemNumber)
        End If

        displayText += itemSelected + " on aisle " + aisleStated + " with the " + catagoryStated

        DisplayLabel.Text = displayText
    End Sub

    'Filters items being displayed in the list box
    Private Sub FilterItems()
        Dim filterName As String = CStr(FilterComboBox.SelectedItem)
        Dim isAisle As Boolean = False
        Dim isCategory As Boolean = False
        If filterName IsNot Nothing Then
            Try
                tempNum = CInt(filterName)
                isAisle = True
                tempNum = Nothing
            Catch ex As Exception
                isCategory = True
            End Try
        End If

        Dim itemsToRemove As New List(Of Object)
        For i = DisplayListBox.Items.Count - 1 To 0 Step -1
            Dim product As String = DisplayListBox.Items(i).ToString()
            For j = 0 To catalog.GetLength(1) - 1
                If isAisle AndAlso filterName <> catalog(1, j) Then
                    If product = catalog(0, j) Then
                        itemsToRemove.Add(DisplayListBox.Items(i))
                    End If
                End If
                If isCategory AndAlso filterName <> catalog(2, j) Then
                    If product = catalog(0, j) Then
                        itemsToRemove.Add(DisplayListBox.Items(i))
                    End If
                End If
            Next
        Next
        For Each itemToRemove In itemsToRemove
            DisplayListBox.Items.Remove(itemToRemove)
        Next

        If DisplayListBox.Items.Count = 0 Then
            MsgBox("No items available that match your search")
        End If

    End Sub

    'Creates a 2D array that holds all the items, aisles, and catagories that are given in the file.
    Private Sub CreateCatalog(values() As String)
        If values(0) Is Nothing Then
            values(0) = ""
        ElseIf values(1) Is Nothing Then
            values(1) = ""
        ElseIf values(2) Is Nothing Then
            values(2) = ""
        End If
        ReDim Preserve catalog(2, countTwo)
        catalog(0, countTwo) = values(0).Replace("$$ITM", "").Replace("""", "").Trim()
        catalog(1, countTwo) = values(1).Replace("##LOC", "").Replace("""", "").Trim()
        catalog(2, countTwo) = values(2).Replace("%%CAT", "").Replace("""", "").Trim()
        countTwo += 1
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
        'categories.Clear()
        If Not aisleNumbers.Contains(CInt(aisle)) Then
            SortAisles(aisle)
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
        'aisleNumbers.Clear()
        If category = vbNullString Then

        ElseIf Not categories.Contains(category) And category IsNot "" Then
            categories.Add(category)
            FilterComboBox.Items.Add(category)
        End If
    End Sub

    'Adds items from the file to a list
    Private Sub ItemList()
        For i = 0 To products.Length - 1
            DisplayListBox.Items.Add(products(i))
        Next
    End Sub

End Class