Public Class AboutForm

    Private Sub AboutForm_Load(sender As Object, e As EventArgs)
        Me.Activate()
        StansGroceryForm.Hide()
    End Sub

    Private Sub AboutOkayButton_Click_1(sender As Object, e As EventArgs) Handles AboutOkayButton.Click
        Me.Hide()
        StansGroceryForm.Show()
    End Sub
End Class