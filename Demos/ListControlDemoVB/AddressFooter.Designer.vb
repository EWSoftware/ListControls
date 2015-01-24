<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddressFooter
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
        Me.label1 = New System.Windows.Forms.Label
        Me.lblTotal = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(346, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(88, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Demo Total"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Location = New System.Drawing.Point(440, 9)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(56, 23)
        Me.lblTotal.TabIndex = 1
        Me.lblTotal.Text = "0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AddressFooter
        '
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.label1)
        Me.Name = "AddressFooter"
        Me.Size = New System.Drawing.Size(504, 40)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label

End Class
