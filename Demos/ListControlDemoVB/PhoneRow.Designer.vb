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
        components = New Container()
        txtPhoneNumber = New MaskedTextBox()
        epErrors = New ErrorProvider(components)
        btnDelete = New Button()
        CType(epErrors, ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' txtPhoneNumber
        ' 
        txtPhoneNumber.Location = New Point(4, 9)
        txtPhoneNumber.Mask = "(000) 000-0000"
        txtPhoneNumber.Name = "txtPhoneNumber"
        txtPhoneNumber.Size = New Size(156, 27)
        txtPhoneNumber.TabIndex = 0
        ' 
        ' epErrors
        ' 
        epErrors.ContainerControl = Me
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(178, 6)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(75, 32)
        btnDelete.TabIndex = 1
        btnDelete.Text = "&Delete"
        ' 
        ' PhoneRow
        ' 
        Me.Controls.Add(btnDelete)
        Me.Controls.Add(txtPhoneNumber)
        Me.Name = "PhoneRow"
        Me.Size = New Size(256, 40)
        CType(epErrors, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents epErrors As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtPhoneNumber As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button

End Class

