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
        lblDemoDesc = New ClickableLabel()
        btnDemo = New Button()
        lblDemoName = New ClickableLabel()
        lblDemoImage = New ClickableLabel()
        Me.SuspendLayout()
        ' 
        ' lblDemoDesc
        ' 
        lblDemoDesc.Location = New Point(100, 25)
        lblDemoDesc.Name = "lblDemoDesc"
        lblDemoDesc.Size = New Size(753, 112)
        lblDemoDesc.TabIndex = 2
        lblDemoDesc.Text = "Demo Description"
        lblDemoDesc.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnDemo
        ' 
        btnDemo.BackColor = SystemColors.Control
        btnDemo.Location = New Point(859, 65)
        btnDemo.Name = "btnDemo"
        btnDemo.Size = New Size(75, 32)
        btnDemo.TabIndex = 3
        btnDemo.Text = "&Demo"
        btnDemo.UseVisualStyleBackColor = False
        ' 
        ' lblDemoName
        ' 
        lblDemoName.BackColor = SystemColors.ActiveCaption
        lblDemoName.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblDemoName.ForeColor = SystemColors.ActiveCaptionText
        lblDemoName.Location = New Point(0, 0)
        lblDemoName.Name = "lblDemoName"
        lblDemoName.Size = New Size(950, 23)
        lblDemoName.TabIndex = 0
        lblDemoName.Text = "Demo Name"
        lblDemoName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblDemoImage
        ' 
        lblDemoImage.BackColor = SystemColors.ControlLight
        lblDemoImage.Location = New Point(26, 65)
        lblDemoImage.Name = "lblDemoImage"
        lblDemoImage.Size = New Size(32, 32)
        lblDemoImage.TabIndex = 1
        ' 
        ' MenuRow
        ' 
        Me.BackColor = SystemColors.Window
        Me.Controls.Add(lblDemoDesc)
        Me.Controls.Add(btnDemo)
        Me.Controls.Add(lblDemoName)
        Me.Controls.Add(lblDemoImage)
        Me.Name = "MenuRow"
        Me.Size = New Size(950, 141)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblDemoDesc As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents btnDemo As System.Windows.Forms.Button
    Friend WithEvents lblDemoName As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents lblDemoImage As EWSoftware.ListControls.ClickableLabel
End Class
