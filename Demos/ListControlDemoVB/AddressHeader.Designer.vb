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
        label1 = New ClickableLabel()
        lblKey = New ClickableLabel()
        txtFindName = New TextBox()
        label2 = New ClickableLabel()
        Me.SuspendLayout()
        ' 
        ' label1
        ' 
        label1.Location = New Point(13, 9)
        label1.Name = "label1"
        label1.Size = New Size(31, 23)
        label1.TabIndex = 0
        label1.Text = "ID"
        label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblKey
        ' 
        lblKey.BorderStyle = BorderStyle.Fixed3D
        lblKey.Location = New Point(50, 9)
        lblKey.Name = "lblKey"
        lblKey.Size = New Size(64, 23)
        lblKey.TabIndex = 1
        lblKey.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtFindName
        ' 
        txtFindName.Location = New Point(248, 7)
        txtFindName.MaxLength = 30
        txtFindName.Name = "txtFindName"
        txtFindName.Size = New Size(212, 27)
        txtFindName.TabIndex = 3
        ' 
        ' label2
        ' 
        label2.Location = New Point(120, 9)
        label2.Name = "label2"
        label2.Size = New Size(122, 23)
        label2.TabIndex = 2
        label2.Text = "&Find Last Name"
        label2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' AddressHeader
        ' 
        Me.BackColor = Color.Gainsboro
        Me.Controls.Add(txtFindName)
        Me.Controls.Add(label2)
        Me.Controls.Add(label1)
        Me.Controls.Add(lblKey)
        Me.Name = "AddressHeader"
        Me.Size = New Size(580, 40)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblKey As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents label1 As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents txtFindName As System.Windows.Forms.TextBox
    Friend WithEvents label2 As EWSoftware.ListControls.ClickableLabel

End Class
