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
        label1 = New Label()
        lblTotal = New Label()
        Me.SuspendLayout()
        ' 
        ' label1
        ' 
        label1.Location = New Point(384, 9)
        label1.Name = "label1"
        label1.Size = New Size(107, 23)
        label1.TabIndex = 0
        label1.Text = "Demo Total"
        label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblTotal
        ' 
        lblTotal.BorderStyle = BorderStyle.Fixed3D
        lblTotal.Location = New Point(497, 9)
        lblTotal.Name = "lblTotal"
        lblTotal.Size = New Size(56, 23)
        lblTotal.TabIndex = 1
        lblTotal.Text = "0"
        lblTotal.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' AddressFooter
        ' 
        Me.BackColor = Color.Gainsboro
        Me.Controls.Add(lblTotal)
        Me.Controls.Add(label1)
        Me.Name = "AddressFooter"
        Me.Size = New Size(580, 40)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label

End Class
