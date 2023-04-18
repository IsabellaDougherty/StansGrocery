<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreenForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SplashPictureBox = New System.Windows.Forms.PictureBox()
        Me.SplashTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.SplashPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplashPictureBox
        '
        Me.SplashPictureBox.BackColor = System.Drawing.Color.BlueViolet
        Me.SplashPictureBox.BackgroundImage = Global.StansGrocery.My.Resources.Resources.StansLogo
        Me.SplashPictureBox.ErrorImage = Global.StansGrocery.My.Resources.Resources.StansLogo
        Me.SplashPictureBox.InitialImage = Global.StansGrocery.My.Resources.Resources.StansLogo
        Me.SplashPictureBox.Location = New System.Drawing.Point(281, 72)
        Me.SplashPictureBox.Name = "SplashPictureBox"
        Me.SplashPictureBox.Size = New System.Drawing.Size(227, 231)
        Me.SplashPictureBox.TabIndex = 0
        Me.SplashPictureBox.TabStop = False
        Me.SplashPictureBox.WaitOnLoad = True
        '
        'SplashTimer
        '
        Me.SplashTimer.Interval = 2500
        '
        'SplashScreenForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.BlueViolet
        Me.ClientSize = New System.Drawing.Size(800, 420)
        Me.Controls.Add(Me.SplashPictureBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SplashScreenForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        CType(Me.SplashPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplashPictureBox As PictureBox
    Friend WithEvents SplashTimer As Timer
End Class
