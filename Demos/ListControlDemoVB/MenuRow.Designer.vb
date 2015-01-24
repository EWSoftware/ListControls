<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MenuRow
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
        Me.lblDemoDesc = New EWSoftware.ListControls.ClickableLabel
        Me.btnDemo = New System.Windows.Forms.Button
        Me.lblDemoName = New EWSoftware.ListControls.ClickableLabel
        Me.lblDemoImage = New EWSoftware.ListControls.ClickableLabel
        Me.SuspendLayout()
        '
        'lblDemoDesc
        '
        Me.lblDemoDesc.Location = New System.Drawing.Point(45, 25)
        Me.lblDemoDesc.Name = "lblDemoDesc"
        Me.lblDemoDesc.Size = New System.Drawing.Size(565, 112)
        Me.lblDemoDesc.TabIndex = 2
        Me.lblDemoDesc.Text = "Demo Description"
        Me.lblDemoDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnDemo
        '
        Me.btnDemo.BackColor = System.Drawing.SystemColors.Control
        Me.btnDemo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnDemo.Location = New System.Drawing.Point(618, 62)
        Me.btnDemo.Name = "btnDemo"
        Me.btnDemo.Size = New System.Drawing.Size(75, 32)
        Me.btnDemo.TabIndex = 3
        Me.btnDemo.Text = "&Demo"
        '
        'lblDemoName
        '
        Me.lblDemoName.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblDemoName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDemoName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblDemoName.Location = New System.Drawing.Point(0, 0)
        Me.lblDemoName.Name = "lblDemoName"
        Me.lblDemoName.Size = New System.Drawing.Size(700, 23)
        Me.lblDemoName.TabIndex = 0
        Me.lblDemoName.Text = "Demo Name"
        Me.lblDemoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDemoImage
        '
        Me.lblDemoImage.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblDemoImage.Location = New System.Drawing.Point(6, 62)
        Me.lblDemoImage.Name = "lblDemoImage"
        Me.lblDemoImage.Size = New System.Drawing.Size(32, 32)
        Me.lblDemoImage.TabIndex = 1
        '
        'MenuRow
        '
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.Controls.Add(Me.lblDemoDesc)
        Me.Controls.Add(Me.btnDemo)
        Me.Controls.Add(Me.lblDemoName)
        Me.Controls.Add(Me.lblDemoImage)
        Me.Name = "MenuRow"
        Me.Size = New System.Drawing.Size(700, 141)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblDemoDesc As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents btnDemo As System.Windows.Forms.Button
    Friend WithEvents lblDemoName As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents lblDemoImage As EWSoftware.ListControls.ClickableLabel
End Class
