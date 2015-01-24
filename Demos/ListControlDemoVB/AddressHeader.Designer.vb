<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddressHeader
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
        Me.label1 = New EWSoftware.ListControls.ClickableLabel
        Me.lblKey = New EWSoftware.ListControls.ClickableLabel
        Me.txtFindName = New System.Windows.Forms.TextBox
        Me.label2 = New EWSoftware.ListControls.ClickableLabel
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(10, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(24, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "ID"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKey
        '
        Me.lblKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblKey.Location = New System.Drawing.Point(40, 9)
        Me.lblKey.Name = "lblKey"
        Me.lblKey.Size = New System.Drawing.Size(64, 23)
        Me.lblKey.TabIndex = 1
        Me.lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFindName
        '
        Me.txtFindName.Location = New System.Drawing.Point(232, 9)
        Me.txtFindName.MaxLength = 30
        Me.txtFindName.Name = "txtFindName"
        Me.txtFindName.Size = New System.Drawing.Size(136, 22)
        Me.txtFindName.TabIndex = 3
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(114, 9)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(112, 23)
        Me.label2.TabIndex = 2
        Me.label2.Text = "&Find Last Name"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AddressHeader
        '
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.Controls.Add(Me.txtFindName)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.lblKey)
        Me.Name = "AddressHeader"
        Me.Size = New System.Drawing.Size(504, 40)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblKey As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents label1 As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents txtFindName As System.Windows.Forms.TextBox
    Friend WithEvents label2 As EWSoftware.ListControls.ClickableLabel

End Class
