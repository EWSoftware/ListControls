<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PhoneRow
    Inherits EWSoftware.ListControls.TemplateControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.txtPhoneNumber = New System.Windows.Forms.TextBox
        Me.epErrors = New System.Windows.Forms.ErrorProvider
        Me.btnDelete = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtPhoneNumber
        '
        Me.txtPhoneNumber.Location = New System.Drawing.Point(4, 9)
        Me.txtPhoneNumber.MaxLength = 20
        Me.txtPhoneNumber.Name = "txtPhoneNumber"
        Me.txtPhoneNumber.Size = New System.Drawing.Size(156, 22)
        Me.txtPhoneNumber.TabIndex = 0
        Me.txtPhoneNumber.Text = ""
        '
        'epErrors
        '
        Me.epErrors.ContainerControl = Me
        '
        'btnDelete
        '
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnDelete.Location = New System.Drawing.Point(176, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 25)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "&Delete"
        '
        'PhoneRow
        '
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.txtPhoneNumber)
        Me.Name = "PhoneRow"
        Me.Size = New System.Drawing.Size(256, 40)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents epErrors As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtPhoneNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button

End Class

