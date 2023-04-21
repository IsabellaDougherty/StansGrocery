'Isabella Dougherty
'RCET0265
'Spring 2023
'Stans Grocery
'https://github.com/IsabellaDougherty/StansGrocery.git

Option Strict On
Option Explicit On

Public Class SplashScreenForm
    Private Sub SplashScreenForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StansGroceryForm.Visible = False
        SplashTimer.Enabled = True
    End Sub

    Private Sub SplashScreenForm_Click(sender As Object, e As EventArgs) Handles Me.Click, SplashTimer.Tick
        SplashTimer.Enabled = False
        StansGroceryForm.Visible = True
        Me.Hide()
    End Sub

End Class