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
        components = New Container()
        Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(ExtendedTreeViewTestForm))
        ilSmall = New ImageList(components)
        ilLarge = New ImageList(components)
        ilState = New ImageList(components)
        ilExpando = New ImageList(components)
        toolTip1 = New ToolTip(components)
        btnCheckedText = New Button()
        btnCheckedNames = New Button()
        btnFindNode = New Button()
        btnEnumNodeSibs = New Button()
        btnEnumNode = New Button()
        btnEnumTree = New Button()
        chkLargeImages = New CheckBox()
        chkExpandoImages = New CheckBox()
        chkFormDrawNode = New CheckBox()
        chkStateImages = New CheckBox()
        gbFindByName = New GroupBox()
        txtFindName = New TextBox()
        gbEnumerate = New GroupBox()
        txtEnumResults = New TextBox()
        gbAppearance = New GroupBox()
        pgProps = New PropertyGrid()
        tvExtTree = New ExtendedTreeView()
        gbFindByName.SuspendLayout()
        gbEnumerate.SuspendLayout()
        gbAppearance.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' ilSmall
        ' 
        ilSmall.ColorDepth = ColorDepth.Depth24Bit
        ilSmall.ImageStream = CType(resources.GetObject("ilSmall.ImageStream"), ImageListStreamer)
        ilSmall.TransparentColor = Color.Magenta
        ilSmall.Images.SetKeyName(0, "FolderClosed.bmp")
        ilSmall.Images.SetKeyName(1, "FolderOpen.bmp")
        ilSmall.Images.SetKeyName(2, "BookClosed.bmp")
        ilSmall.Images.SetKeyName(3, "BookOpen.bmp")
        ilSmall.Images.SetKeyName(4, "Document.bmp")
        ' 
        ' ilLarge
        ' 
        ilLarge.ColorDepth = ColorDepth.Depth8Bit
        ilLarge.ImageStream = CType(resources.GetObject("ilLarge.ImageStream"), ImageListStreamer)
        ilLarge.TransparentColor = Color.Magenta
        ilLarge.Images.SetKeyName(0, "FolderClosed.bmp")
        ilLarge.Images.SetKeyName(1, "FolderOpen.bmp")
        ilLarge.Images.SetKeyName(2, "BookClosed.bmp")
        ilLarge.Images.SetKeyName(3, "BookOpen.bmp")
        ilLarge.Images.SetKeyName(4, "Document.bmp")
        ' 
        ' ilState
        ' 
        ilState.ColorDepth = ColorDepth.Depth24Bit
        ilState.ImageStream = CType(resources.GetObject("ilState.ImageStream"), ImageListStreamer)
        ilState.TransparentColor = Color.Magenta
        ilState.Images.SetKeyName(0, "StateNo.bmp")
        ilState.Images.SetKeyName(1, "StateYes.bmp")
        ilState.Images.SetKeyName(2, "StateSerious.bmp")
        ilState.Images.SetKeyName(3, "StateWarning.bmp")
        ilState.Images.SetKeyName(4, "StateCritical.bmp")
        ' 
        ' ilExpando
        ' 
        ilExpando.ColorDepth = ColorDepth.Depth24Bit
        ilExpando.ImageStream = CType(resources.GetObject("ilExpando.ImageStream"), ImageListStreamer)
        ilExpando.TransparentColor = Color.Magenta
        ilExpando.Images.SetKeyName(0, "Collapse.bmp")
        ilExpando.Images.SetKeyName(1, "Expand.bmp")
        ' 
        ' btnCheckedText
        ' 
        btnCheckedText.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnCheckedText.Location = New Point(849, 605)
        btnCheckedText.Name = "btnCheckedText"
        btnCheckedText.Size = New Size(75, 32)
        btnCheckedText.TabIndex = 13
        btnCheckedText.Text = "Te&xt"
        toolTip1.SetToolTip(btnCheckedText, "Display the text for all checked nodes")
        btnCheckedText.UseVisualStyleBackColor = True
        ' 
        ' btnCheckedNames
        ' 
        btnCheckedNames.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnCheckedNames.Location = New Point(716, 605)
        btnCheckedNames.Name = "btnCheckedNames"
        btnCheckedNames.Size = New Size(75, 32)
        btnCheckedNames.TabIndex = 12
        btnCheckedNames.Text = "N&ames"
        toolTip1.SetToolTip(btnCheckedNames, "Display the names of all checked nodes")
        btnCheckedNames.UseVisualStyleBackColor = True
        ' 
        ' btnFindNode
        ' 
        btnFindNode.Location = New Point(6, 54)
        btnFindNode.Name = "btnFindNode"
        btnFindNode.Size = New Size(75, 32)
        btnFindNode.TabIndex = 1
        btnFindNode.Text = "&Find"
        toolTip1.SetToolTip(btnFindNode, "Find a node by its name using the indexer")
        btnFindNode.UseVisualStyleBackColor = True
        ' 
        ' btnEnumNodeSibs
        ' 
        btnEnumNodeSibs.Location = New Point(6, 52)
        btnEnumNodeSibs.Name = "btnEnumNodeSibs"
        btnEnumNodeSibs.Size = New Size(195, 25)
        btnEnumNodeSibs.TabIndex = 2
        btnEnumNodeSibs.Text = "N&ode and Siblings"
        toolTip1.SetToolTip(btnEnumNodeSibs, "Enumerate the selected node and its following siblings")
        btnEnumNodeSibs.UseVisualStyleBackColor = True
        ' 
        ' btnEnumNode
        ' 
        btnEnumNode.Location = New Point(87, 21)
        btnEnumNode.Name = "btnEnumNode"
        btnEnumNode.Size = New Size(115, 25)
        btnEnumNode.TabIndex = 1
        btnEnumNode.Text = "&Node Only"
        toolTip1.SetToolTip(btnEnumNode, "Enumerate the selected node")
        btnEnumNode.UseVisualStyleBackColor = True
        ' 
        ' btnEnumTree
        ' 
        btnEnumTree.Location = New Point(6, 21)
        btnEnumTree.Name = "btnEnumTree"
        btnEnumTree.Size = New Size(75, 25)
        btnEnumTree.TabIndex = 0
        btnEnumTree.Text = "&Tree"
        toolTip1.SetToolTip(btnEnumTree, "Enumerate the entire tree")
        btnEnumTree.UseVisualStyleBackColor = True
        ' 
        ' chkLargeImages
        ' 
        chkLargeImages.Location = New Point(11, 22)
        chkLargeImages.Margin = New Padding(4)
        chkLargeImages.Name = "chkLargeImages"
        chkLargeImages.Size = New Size(149, 24)
        chkLargeImages.TabIndex = 0
        chkLargeImages.Text = "&Large Images"
        toolTip1.SetToolTip(chkLargeImages, "Use large images")
        chkLargeImages.UseVisualStyleBackColor = True
        ' 
        ' chkExpandoImages
        ' 
        chkExpandoImages.Location = New Point(11, 86)
        chkExpandoImages.Margin = New Padding(4)
        chkExpandoImages.Name = "chkExpandoImages"
        chkExpandoImages.Size = New Size(149, 24)
        chkExpandoImages.TabIndex = 2
        chkExpandoImages.Text = "&Expando Images"
        toolTip1.SetToolTip(chkExpandoImages, "Use custom expando (+/-) images")
        chkExpandoImages.UseVisualStyleBackColor = True
        ' 
        ' chkFormDrawNode
        ' 
        chkFormDrawNode.Location = New Point(11, 118)
        chkFormDrawNode.Margin = New Padding(4)
        chkFormDrawNode.Name = "chkFormDrawNode"
        chkFormDrawNode.Size = New Size(190, 24)
        chkFormDrawNode.TabIndex = 3
        chkFormDrawNode.Text = "Form &DrawNode Events"
        toolTip1.SetToolTip(chkFormDrawNode, "Use the forms After and Before DrawNode events")
        chkFormDrawNode.UseVisualStyleBackColor = True
        ' 
        ' chkStateImages
        ' 
        chkStateImages.Location = New Point(11, 54)
        chkStateImages.Margin = New Padding(4)
        chkStateImages.Name = "chkStateImages"
        chkStateImages.Size = New Size(149, 24)
        chkStateImages.TabIndex = 1
        chkStateImages.Text = "&State Images"
        toolTip1.SetToolTip(chkStateImages, "Show state images")
        chkStateImages.UseVisualStyleBackColor = True
        ' 
        ' gbFindByName
        ' 
        gbFindByName.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        gbFindByName.Controls.Add(btnFindNode)
        gbFindByName.Controls.Add(txtFindName)
        gbFindByName.Location = New Point(716, 507)
        gbFindByName.Name = "gbFindByName"
        gbFindByName.Size = New Size(208, 92)
        gbFindByName.TabIndex = 11
        gbFindByName.TabStop = False
        gbFindByName.Text = "Find &by Node Name"
        ' 
        ' txtFindName
        ' 
        txtFindName.Location = New Point(6, 21)
        txtFindName.Name = "txtFindName"
        txtFindName.Size = New Size(196, 27)
        txtFindName.TabIndex = 0
        txtFindName.Text = "Fld2Bk1Pg3"
        ' 
        ' gbEnumerate
        ' 
        gbEnumerate.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        gbEnumerate.Controls.Add(txtEnumResults)
        gbEnumerate.Controls.Add(btnEnumNodeSibs)
        gbEnumerate.Controls.Add(btnEnumNode)
        gbEnumerate.Controls.Add(btnEnumTree)
        gbEnumerate.Location = New Point(716, 171)
        gbEnumerate.Name = "gbEnumerate"
        gbEnumerate.Size = New Size(208, 330)
        gbEnumerate.TabIndex = 10
        gbEnumerate.TabStop = False
        gbEnumerate.Text = "Enumerate"
        ' 
        ' txtEnumResults
        ' 
        txtEnumResults.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        txtEnumResults.Location = New Point(6, 83)
        txtEnumResults.Multiline = True
        txtEnumResults.Name = "txtEnumResults"
        txtEnumResults.ReadOnly = True
        txtEnumResults.ScrollBars = ScrollBars.Both
        txtEnumResults.Size = New Size(195, 241)
        txtEnumResults.TabIndex = 3
        ' 
        ' gbAppearance
        ' 
        gbAppearance.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        gbAppearance.Controls.Add(chkLargeImages)
        gbAppearance.Controls.Add(chkExpandoImages)
        gbAppearance.Controls.Add(chkFormDrawNode)
        gbAppearance.Controls.Add(chkStateImages)
        gbAppearance.Location = New Point(716, 12)
        gbAppearance.Name = "gbAppearance"
        gbAppearance.Size = New Size(208, 153)
        gbAppearance.TabIndex = 9
        gbAppearance.TabStop = False
        gbAppearance.Text = "Appearance"
        ' 
        ' pgProps
        ' 
        pgProps.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        pgProps.Location = New Point(268, 12)
        pgProps.Name = "pgProps"
        pgProps.Size = New Size(442, 620)
        pgProps.TabIndex = 8
        ' 
        ' tvExtTree
        ' 
        tvExtTree.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tvExtTree.DrawDefaultImages = False
        tvExtTree.ImageIndex = 0
        tvExtTree.ImageList = ilSmall
        tvExtTree.Location = New Point(12, 12)
        tvExtTree.Name = "tvExtTree"
        tvExtTree.SelectedImageIndex = 1
        tvExtTree.ShowNodeToolTips = True
        tvExtTree.Size = New Size(250, 620)
        tvExtTree.TabIndex = 7
        ' 
        ' ExtendedTreeViewTestForm
        ' 
        Me.ClientSize = New Size(936, 644)
        Me.Controls.Add(btnCheckedText)
        Me.Controls.Add(btnCheckedNames)
        Me.Controls.Add(gbFindByName)
        Me.Controls.Add(gbEnumerate)
        Me.Controls.Add(gbAppearance)
        Me.Controls.Add(pgProps)
        Me.Controls.Add(tvExtTree)
        Me.MinimumSize = New Size(865, 500)
        Me.Name = "ExtendedTreeViewTestForm"
        Me.SizeGripStyle = SizeGripStyle.Show
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Test Extended Tree View Control"
        gbFindByName.ResumeLayout(False)
        gbFindByName.PerformLayout()
        gbEnumerate.ResumeLayout(False)
        gbEnumerate.PerformLayout()
        gbAppearance.ResumeLayout(False)
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
