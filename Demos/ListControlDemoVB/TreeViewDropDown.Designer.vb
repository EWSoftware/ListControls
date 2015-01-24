<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TreeViewDropDown
    Inherits EWSoftware.ListControls.DropDownControl

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
        Me.tvItems = New System.Windows.Forms.TreeView
        Me.chkExcludeDiscontinued = New System.Windows.Forms.CheckBox
        Me.btnSelect = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'tvItems
        '
        Me.tvItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvItems.HideSelection = False
        Me.tvItems.Location = New System.Drawing.Point(8, 8)
        Me.tvItems.Name = "tvItems"
        Me.tvItems.Size = New System.Drawing.Size(234, 208)
        Me.tvItems.TabIndex = 0
        '
        'chkExcludeDiscontinued
        '
        Me.chkExcludeDiscontinued.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkExcludeDiscontinued.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkExcludeDiscontinued.Location = New System.Drawing.Point(8, 218)
        Me.chkExcludeDiscontinued.Name = "chkExcludeDiscontinued"
        Me.chkExcludeDiscontinued.Size = New System.Drawing.Size(216, 24)
        Me.chkExcludeDiscontinued.TabIndex = 1
        Me.chkExcludeDiscontinued.Text = "&Exclude discontinued products"
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnSelect.Location = New System.Drawing.Point(108, 244)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(64, 25)
        Me.btnSelect.TabIndex = 2
        Me.btnSelect.Text = "&Select"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnCancel.Location = New System.Drawing.Point(178, 244)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 25)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "&Cancel"
        '
        'TreeViewDropDown
        '
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.chkExcludeDiscontinued)
        Me.Controls.Add(Me.tvItems)
        Me.MinimumSize = New System.Drawing.Size(240, 150)
        Me.Name = "TreeViewDropDown"
        Me.Size = New System.Drawing.Size(250, 272)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents chkExcludeDiscontinued As System.Windows.Forms.CheckBox
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents tvItems As System.Windows.Forms.TreeView
    Friend WithEvents btnCancel As System.Windows.Forms.Button

End Class

