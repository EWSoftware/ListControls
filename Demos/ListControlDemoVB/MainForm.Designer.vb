<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.dlMenu = New EWSoftware.ListControls.DataList()
        Me.SuspendLayout
        '
        'dlMenu
        '
        Me.dlMenu.AllowAdditions = false
        Me.dlMenu.AllowDeletes = false
        Me.dlMenu.AllowEdits = false
        Me.dlMenu.BackColor = System.Drawing.SystemColors.Window
        Me.dlMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dlMenu.Location = New System.Drawing.Point(0, 0)
        Me.dlMenu.Name = "dlMenu"
        Me.dlMenu.NavigationControlsVisible = false
        Me.dlMenu.RowHeadersVisible = false
        Me.dlMenu.SeparatorsVisible = false
        Me.dlMenu.Size = New System.Drawing.Size(732, 584)
        Me.dlMenu.TabIndex = 1
        '
        'MainForm
        '
        Me.ClientSize = New System.Drawing.Size(732, 584)
        Me.Controls.Add(Me.dlMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(750, 2048)
        Me.MinimumSize = New System.Drawing.Size(750, 615)
        Me.Name = "MainForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EWSoftware List Controls Demo"
        Me.ResumeLayout(false)

    End Sub

    Friend WithEvents dlMenu As EWSoftware.ListControls.DataList
End Class
