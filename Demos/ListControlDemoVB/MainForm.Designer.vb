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
        Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(MainForm))
        dlMenu = New DataList()
        Me.SuspendLayout()
        ' 
        ' dlMenu
        ' 
        dlMenu.AllowAdditions = False
        dlMenu.AllowDeletes = False
        dlMenu.AllowEdits = False
        dlMenu.BackColor = SystemColors.Window
        dlMenu.Dock = DockStyle.Fill
        dlMenu.Location = New Point(0, 0)
        dlMenu.Name = "dlMenu"
        dlMenu.NavigationControlsVisible = False
        dlMenu.RowHeadersVisible = False
        dlMenu.SeparatorsVisible = False
        dlMenu.Size = New Size(982, 721)
        dlMenu.TabIndex = 1
        ' 
        ' MainForm
        ' 
        Me.ClientSize = New Size(982, 721)
        Me.Controls.Add(dlMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Me.MaximumSize = New Size(1000, 2048)
        Me.MinimumSize = New Size(1000, 615)
        Me.Name = "MainForm"
        Me.SizeGripStyle = SizeGripStyle.Show
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "EWSoftware List Controls Visual Basic Demo"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dlMenu As EWSoftware.ListControls.DataList
End Class
