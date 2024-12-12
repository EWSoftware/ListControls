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
        tvItems = New TreeView()
        chkExcludeDiscontinued = New CheckBox()
        btnSelect = New Button()
        btnCancel = New Button()
        Me.SuspendLayout()
        ' 
        ' tvItems
        ' 
        tvItems.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tvItems.HideSelection = False
        tvItems.Location = New Point(8, 3)
        tvItems.Name = "tvItems"
        tvItems.Size = New Size(284, 228)
        tvItems.TabIndex = 0
        ' 
        ' chkExcludeDiscontinued
        ' 
        chkExcludeDiscontinued.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        chkExcludeDiscontinued.Location = New Point(8, 237)
        chkExcludeDiscontinued.Name = "chkExcludeDiscontinued"
        chkExcludeDiscontinued.Size = New Size(284, 24)
        chkExcludeDiscontinued.TabIndex = 1
        chkExcludeDiscontinued.Text = "&Exclude discontinued products"
        ' 
        ' btnSelect
        ' 
        btnSelect.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnSelect.Location = New Point(146, 267)
        btnSelect.Name = "btnSelect"
        btnSelect.Size = New Size(70, 30)
        btnSelect.TabIndex = 2
        btnSelect.Text = "&Select"
        ' 
        ' btnCancel
        ' 
        btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnCancel.Location = New Point(222, 267)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(70, 30)
        btnCancel.TabIndex = 3
        btnCancel.Text = "&Cancel"
        ' 
        ' TreeViewDropDown
        ' 
        Me.Controls.Add(btnCancel)
        Me.Controls.Add(btnSelect)
        Me.Controls.Add(chkExcludeDiscontinued)
        Me.Controls.Add(tvItems)
        Me.MinimumSize = New Size(300, 150)
        Me.Name = "TreeViewDropDown"
        Me.Size = New Size(300, 300)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents chkExcludeDiscontinued As System.Windows.Forms.CheckBox
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents tvItems As System.Windows.Forms.TreeView
    Friend WithEvents btnCancel As System.Windows.Forms.Button

End Class

