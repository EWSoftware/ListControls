<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExtendedTreeViewTestForm
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExtendedTreeViewTestForm))
        Me.ilSmall = New System.Windows.Forms.ImageList(Me.components)
        Me.ilLarge = New System.Windows.Forms.ImageList(Me.components)
        Me.ilState = New System.Windows.Forms.ImageList(Me.components)
        Me.ilExpando = New System.Windows.Forms.ImageList(Me.components)
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCheckedText = New System.Windows.Forms.Button
        Me.btnCheckedNames = New System.Windows.Forms.Button
        Me.btnFindNode = New System.Windows.Forms.Button
        Me.btnEnumNodeSibs = New System.Windows.Forms.Button
        Me.btnEnumNode = New System.Windows.Forms.Button
        Me.btnEnumTree = New System.Windows.Forms.Button
        Me.chkLargeImages = New System.Windows.Forms.CheckBox
        Me.chkExpandoImages = New System.Windows.Forms.CheckBox
        Me.chkFormDrawNode = New System.Windows.Forms.CheckBox
        Me.chkStateImages = New System.Windows.Forms.CheckBox
        Me.gbFindByName = New System.Windows.Forms.GroupBox
        Me.txtFindName = New System.Windows.Forms.TextBox
        Me.gbEnumerate = New System.Windows.Forms.GroupBox
        Me.txtEnumResults = New System.Windows.Forms.TextBox
        Me.gbAppearance = New System.Windows.Forms.GroupBox
        Me.pgProps = New System.Windows.Forms.PropertyGrid
        Me.tvExtTree = New EWSoftware.ListControls.ExtendedTreeView
        Me.gbFindByName.SuspendLayout()
        Me.gbEnumerate.SuspendLayout()
        Me.gbAppearance.SuspendLayout()
        Me.SuspendLayout()
        '
        'ilSmall
        '
        Me.ilSmall.ImageStream = CType(resources.GetObject("ilSmall.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilSmall.TransparentColor = System.Drawing.Color.Magenta
        Me.ilSmall.Images.SetKeyName(0, "FolderClosed.bmp")
        Me.ilSmall.Images.SetKeyName(1, "FolderOpen.bmp")
        Me.ilSmall.Images.SetKeyName(2, "BookClosed.bmp")
        Me.ilSmall.Images.SetKeyName(3, "BookOpen.bmp")
        Me.ilSmall.Images.SetKeyName(4, "Document.bmp")
        '
        'ilLarge
        '
        Me.ilLarge.ImageStream = CType(resources.GetObject("ilLarge.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilLarge.TransparentColor = System.Drawing.Color.Magenta
        Me.ilLarge.Images.SetKeyName(0, "FolderClosed.bmp")
        Me.ilLarge.Images.SetKeyName(1, "FolderOpen.bmp")
        Me.ilLarge.Images.SetKeyName(2, "BookClosed.bmp")
        Me.ilLarge.Images.SetKeyName(3, "BookOpen.bmp")
        Me.ilLarge.Images.SetKeyName(4, "Document.bmp")
        '
        'ilState
        '
        Me.ilState.ImageStream = CType(resources.GetObject("ilState.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilState.TransparentColor = System.Drawing.Color.Magenta
        Me.ilState.Images.SetKeyName(0, "StateNo.bmp")
        Me.ilState.Images.SetKeyName(1, "StateYes.bmp")
        Me.ilState.Images.SetKeyName(2, "StateSerious.bmp")
        Me.ilState.Images.SetKeyName(3, "StateWarning.bmp")
        Me.ilState.Images.SetKeyName(4, "StateCritical.bmp")
        '
        'ilExpando
        '
        Me.ilExpando.ImageStream = CType(resources.GetObject("ilExpando.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilExpando.TransparentColor = System.Drawing.Color.Magenta
        Me.ilExpando.Images.SetKeyName(0, "Collapse.bmp")
        Me.ilExpando.Images.SetKeyName(1, "Expand.bmp")
        '
        'btnCheckedText
        '
        Me.btnCheckedText.Location = New System.Drawing.Point(849, 605)
        Me.btnCheckedText.Name = "btnCheckedText"
        Me.btnCheckedText.Size = New System.Drawing.Size(75, 25)
        Me.btnCheckedText.TabIndex = 13
        Me.btnCheckedText.Text = "Te&xt"
        Me.toolTip1.SetToolTip(Me.btnCheckedText, "Display the text for all checked nodes")
        Me.btnCheckedText.UseVisualStyleBackColor = True
        '
        'btnCheckedNames
        '
        Me.btnCheckedNames.Location = New System.Drawing.Point(716, 605)
        Me.btnCheckedNames.Name = "btnCheckedNames"
        Me.btnCheckedNames.Size = New System.Drawing.Size(75, 25)
        Me.btnCheckedNames.TabIndex = 12
        Me.btnCheckedNames.Text = "N&ames"
        Me.toolTip1.SetToolTip(Me.btnCheckedNames, "Display the names of all checked nodes")
        Me.btnCheckedNames.UseVisualStyleBackColor = True
        '
        'btnFindNode
        '
        Me.btnFindNode.Location = New System.Drawing.Point(6, 48)
        Me.btnFindNode.Name = "btnFindNode"
        Me.btnFindNode.Size = New System.Drawing.Size(75, 25)
        Me.btnFindNode.TabIndex = 1
        Me.btnFindNode.Text = "&Find"
        Me.toolTip1.SetToolTip(Me.btnFindNode, "Find a node by its name using the indexer")
        Me.btnFindNode.UseVisualStyleBackColor = True
        '
        'btnEnumNodeSibs
        '
        Me.btnEnumNodeSibs.Location = New System.Drawing.Point(6, 52)
        Me.btnEnumNodeSibs.Name = "btnEnumNodeSibs"
        Me.btnEnumNodeSibs.Size = New System.Drawing.Size(195, 25)
        Me.btnEnumNodeSibs.TabIndex = 2
        Me.btnEnumNodeSibs.Text = "N&ode and Siblings"
        Me.toolTip1.SetToolTip(Me.btnEnumNodeSibs, "Enumerate the selected node and its following siblings")
        Me.btnEnumNodeSibs.UseVisualStyleBackColor = True
        '
        'btnEnumNode
        '
        Me.btnEnumNode.Location = New System.Drawing.Point(87, 21)
        Me.btnEnumNode.Name = "btnEnumNode"
        Me.btnEnumNode.Size = New System.Drawing.Size(115, 25)
        Me.btnEnumNode.TabIndex = 1
        Me.btnEnumNode.Text = "&Node Only"
        Me.toolTip1.SetToolTip(Me.btnEnumNode, "Enumerate the selected node")
        Me.btnEnumNode.UseVisualStyleBackColor = True
        '
        'btnEnumTree
        '
        Me.btnEnumTree.Location = New System.Drawing.Point(6, 21)
        Me.btnEnumTree.Name = "btnEnumTree"
        Me.btnEnumTree.Size = New System.Drawing.Size(75, 25)
        Me.btnEnumTree.TabIndex = 0
        Me.btnEnumTree.Text = "&Tree"
        Me.toolTip1.SetToolTip(Me.btnEnumTree, "Enumerate the entire tree")
        Me.btnEnumTree.UseVisualStyleBackColor = True
        '
        'chkLargeImages
        '
        Me.chkLargeImages.Location = New System.Drawing.Point(11, 22)
        Me.chkLargeImages.Margin = New System.Windows.Forms.Padding(4)
        Me.chkLargeImages.Name = "chkLargeImages"
        Me.chkLargeImages.Size = New System.Drawing.Size(149, 21)
        Me.chkLargeImages.TabIndex = 0
        Me.chkLargeImages.Text = "&Large Images"
        Me.toolTip1.SetToolTip(Me.chkLargeImages, "Use large images")
        Me.chkLargeImages.UseVisualStyleBackColor = True
        '
        'chkExpandoImages
        '
        Me.chkExpandoImages.Location = New System.Drawing.Point(11, 80)
        Me.chkExpandoImages.Margin = New System.Windows.Forms.Padding(4)
        Me.chkExpandoImages.Name = "chkExpandoImages"
        Me.chkExpandoImages.Size = New System.Drawing.Size(149, 21)
        Me.chkExpandoImages.TabIndex = 2
        Me.chkExpandoImages.Text = "&Expando Images"
        Me.toolTip1.SetToolTip(Me.chkExpandoImages, "Use custom expando (+/-) images")
        Me.chkExpandoImages.UseVisualStyleBackColor = True
        '
        'chkFormDrawNode
        '
        Me.chkFormDrawNode.Location = New System.Drawing.Point(11, 109)
        Me.chkFormDrawNode.Margin = New System.Windows.Forms.Padding(4)
        Me.chkFormDrawNode.Name = "chkFormDrawNode"
        Me.chkFormDrawNode.Size = New System.Drawing.Size(190, 21)
        Me.chkFormDrawNode.TabIndex = 3
        Me.chkFormDrawNode.Text = "Form &DrawNode Events"
        Me.toolTip1.SetToolTip(Me.chkFormDrawNode, "Use the forms After and Before DrawNode events")
        Me.chkFormDrawNode.UseVisualStyleBackColor = True
        '
        'chkStateImages
        '
        Me.chkStateImages.Location = New System.Drawing.Point(11, 51)
        Me.chkStateImages.Margin = New System.Windows.Forms.Padding(4)
        Me.chkStateImages.Name = "chkStateImages"
        Me.chkStateImages.Size = New System.Drawing.Size(149, 21)
        Me.chkStateImages.TabIndex = 1
        Me.chkStateImages.Text = "&State Images"
        Me.toolTip1.SetToolTip(Me.chkStateImages, "Show state images")
        Me.chkStateImages.UseVisualStyleBackColor = True
        '
        'gbFindByName
        '
        Me.gbFindByName.Controls.Add(Me.btnFindNode)
        Me.gbFindByName.Controls.Add(Me.txtFindName)
        Me.gbFindByName.Location = New System.Drawing.Point(716, 520)
        Me.gbFindByName.Name = "gbFindByName"
        Me.gbFindByName.Size = New System.Drawing.Size(208, 79)
        Me.gbFindByName.TabIndex = 11
        Me.gbFindByName.TabStop = False
        Me.gbFindByName.Text = "Find &by Node Name"
        '
        'txtFindName
        '
        Me.txtFindName.Location = New System.Drawing.Point(6, 21)
        Me.txtFindName.Name = "txtFindName"
        Me.txtFindName.Size = New System.Drawing.Size(196, 22)
        Me.txtFindName.TabIndex = 0
        Me.txtFindName.Text = "Fld2Bk1Pg3"
        '
        'gbEnumerate
        '
        Me.gbEnumerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbEnumerate.Controls.Add(Me.txtEnumResults)
        Me.gbEnumerate.Controls.Add(Me.btnEnumNodeSibs)
        Me.gbEnumerate.Controls.Add(Me.btnEnumNode)
        Me.gbEnumerate.Controls.Add(Me.btnEnumTree)
        Me.gbEnumerate.Location = New System.Drawing.Point(716, 157)
        Me.gbEnumerate.Name = "gbEnumerate"
        Me.gbEnumerate.Size = New System.Drawing.Size(208, 357)
        Me.gbEnumerate.TabIndex = 10
        Me.gbEnumerate.TabStop = False
        Me.gbEnumerate.Text = "Enumerate"
        '
        'txtEnumResults
        '
        Me.txtEnumResults.Location = New System.Drawing.Point(6, 83)
        Me.txtEnumResults.Multiline = True
        Me.txtEnumResults.Name = "txtEnumResults"
        Me.txtEnumResults.ReadOnly = True
        Me.txtEnumResults.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtEnumResults.Size = New System.Drawing.Size(195, 268)
        Me.txtEnumResults.TabIndex = 3
        '
        'gbAppearance
        '
        Me.gbAppearance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbAppearance.Controls.Add(Me.chkLargeImages)
        Me.gbAppearance.Controls.Add(Me.chkExpandoImages)
        Me.gbAppearance.Controls.Add(Me.chkFormDrawNode)
        Me.gbAppearance.Controls.Add(Me.chkStateImages)
        Me.gbAppearance.Location = New System.Drawing.Point(716, 12)
        Me.gbAppearance.Name = "gbAppearance"
        Me.gbAppearance.Size = New System.Drawing.Size(208, 139)
        Me.gbAppearance.TabIndex = 9
        Me.gbAppearance.TabStop = False
        Me.gbAppearance.Text = "Appearance"
        '
        'pgProps
        '
        Me.pgProps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgProps.Location = New System.Drawing.Point(268, 12)
        Me.pgProps.Name = "pgProps"
        Me.pgProps.Size = New System.Drawing.Size(442, 620)
        Me.pgProps.TabIndex = 8
        '
        'tvExtTree
        '
        Me.tvExtTree.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvExtTree.DrawDefaultImages = False
        Me.tvExtTree.ImageIndex = 0
        Me.tvExtTree.ImageList = Me.ilSmall
        Me.tvExtTree.Location = New System.Drawing.Point(12, 12)
        Me.tvExtTree.Name = "tvExtTree"
        Me.tvExtTree.SelectedImageIndex = 1
        Me.tvExtTree.ShowNodeToolTips = True
        Me.tvExtTree.Size = New System.Drawing.Size(250, 620)
        Me.tvExtTree.TabIndex = 7
        '
        'ExtendedTreeViewTestForm
        '
        Me.ClientSize = New System.Drawing.Size(936, 644)
        Me.Controls.Add(Me.btnCheckedText)
        Me.Controls.Add(Me.btnCheckedNames)
        Me.Controls.Add(Me.gbFindByName)
        Me.Controls.Add(Me.gbEnumerate)
        Me.Controls.Add(Me.gbAppearance)
        Me.Controls.Add(Me.pgProps)
        Me.Controls.Add(Me.tvExtTree)
        Me.MinimumSize = New System.Drawing.Size(740, 209)
        Me.Name = "ExtendedTreeViewTestForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Extended Tree View Control"
        Me.gbFindByName.ResumeLayout(False)
        Me.gbFindByName.PerformLayout()
        Me.gbEnumerate.ResumeLayout(False)
        Me.gbEnumerate.PerformLayout()
        Me.gbAppearance.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ilSmall As System.Windows.Forms.ImageList
    Private WithEvents ilLarge As System.Windows.Forms.ImageList
    Private WithEvents ilState As System.Windows.Forms.ImageList
    Private WithEvents ilExpando As System.Windows.Forms.ImageList
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents btnCheckedText As System.Windows.Forms.Button
    Private WithEvents btnCheckedNames As System.Windows.Forms.Button
    Private WithEvents gbFindByName As System.Windows.Forms.GroupBox
    Private WithEvents btnFindNode As System.Windows.Forms.Button
    Private WithEvents txtFindName As System.Windows.Forms.TextBox
    Private WithEvents gbEnumerate As System.Windows.Forms.GroupBox
    Private WithEvents txtEnumResults As System.Windows.Forms.TextBox
    Private WithEvents btnEnumNodeSibs As System.Windows.Forms.Button
    Private WithEvents btnEnumNode As System.Windows.Forms.Button
    Private WithEvents btnEnumTree As System.Windows.Forms.Button
    Private WithEvents gbAppearance As System.Windows.Forms.GroupBox
    Private WithEvents chkLargeImages As System.Windows.Forms.CheckBox
    Private WithEvents chkExpandoImages As System.Windows.Forms.CheckBox
    Private WithEvents chkFormDrawNode As System.Windows.Forms.CheckBox
    Private WithEvents chkStateImages As System.Windows.Forms.CheckBox
    Private WithEvents pgProps As System.Windows.Forms.PropertyGrid
    Private WithEvents tvExtTree As EWSoftware.ListControls.ExtendedTreeView
End Class
