namespace ListControlDemoCS
{
    partial class ExtendedTreeViewTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ExtendedTreeViewTestForm));
            ilSmall = new ImageList(components);
            chkLargeImages = new CheckBox();
            chkFormDrawNode = new CheckBox();
            ilLarge = new ImageList(components);
            pgProps = new PropertyGrid();
            ilState = new ImageList(components);
            chkStateImages = new CheckBox();
            chkExpandoImages = new CheckBox();
            ilExpando = new ImageList(components);
            gbAppearance = new GroupBox();
            gbEnumerate = new GroupBox();
            txtEnumResults = new TextBox();
            btnEnumNodeSibs = new Button();
            btnEnumNode = new Button();
            btnEnumTree = new Button();
            toolTip1 = new ToolTip(components);
            btnFindNode = new Button();
            btnCheckedNames = new Button();
            btnCheckedText = new Button();
            gbFindByName = new GroupBox();
            txtFindName = new TextBox();
            tvExtTree = new ExtendedTreeView();
            gbAppearance.SuspendLayout();
            gbEnumerate.SuspendLayout();
            gbFindByName.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilSmall
            // 
            ilSmall.ColorDepth = ColorDepth.Depth24Bit;
            ilSmall.ImageStream = (ImageListStreamer)resources.GetObject("ilSmall.ImageStream");
            ilSmall.TransparentColor = Color.Magenta;
            ilSmall.Images.SetKeyName(0, "FolderClosed.bmp");
            ilSmall.Images.SetKeyName(1, "FolderOpen.bmp");
            ilSmall.Images.SetKeyName(2, "BookClosed.bmp");
            ilSmall.Images.SetKeyName(3, "BookOpen.bmp");
            ilSmall.Images.SetKeyName(4, "Document.bmp");
            // 
            // chkLargeImages
            // 
            chkLargeImages.Location = new Point(11, 22);
            chkLargeImages.Margin = new Padding(4);
            chkLargeImages.Name = "chkLargeImages";
            chkLargeImages.Size = new Size(149, 24);
            chkLargeImages.TabIndex = 0;
            chkLargeImages.Text = "&Large Images";
            toolTip1.SetToolTip(chkLargeImages, "Use large images");
            chkLargeImages.UseVisualStyleBackColor = true;
            chkLargeImages.CheckedChanged += this.chkLargeImages_CheckedChanged;
            // 
            // chkFormDrawNode
            // 
            chkFormDrawNode.Location = new Point(11, 118);
            chkFormDrawNode.Margin = new Padding(4);
            chkFormDrawNode.Name = "chkFormDrawNode";
            chkFormDrawNode.Size = new Size(190, 24);
            chkFormDrawNode.TabIndex = 3;
            chkFormDrawNode.Text = "Form &DrawNode Events";
            toolTip1.SetToolTip(chkFormDrawNode, "Use the forms After and Before DrawNode events");
            chkFormDrawNode.UseVisualStyleBackColor = true;
            chkFormDrawNode.CheckedChanged += this.chkFormDrawNode_CheckedChanged;
            // 
            // ilLarge
            // 
            ilLarge.ColorDepth = ColorDepth.Depth8Bit;
            ilLarge.ImageStream = (ImageListStreamer)resources.GetObject("ilLarge.ImageStream");
            ilLarge.TransparentColor = Color.Magenta;
            ilLarge.Images.SetKeyName(0, "FolderClosed.bmp");
            ilLarge.Images.SetKeyName(1, "FolderOpen.bmp");
            ilLarge.Images.SetKeyName(2, "BookClosed.bmp");
            ilLarge.Images.SetKeyName(3, "BookOpen.bmp");
            ilLarge.Images.SetKeyName(4, "Document.bmp");
            // 
            // pgProps
            // 
            pgProps.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pgProps.Location = new Point(268, 12);
            pgProps.Name = "pgProps";
            pgProps.Size = new Size(442, 620);
            pgProps.TabIndex = 1;
            pgProps.PropertyValueChanged += this.pgProps_PropertyValueChanged;
            // 
            // ilState
            // 
            ilState.ColorDepth = ColorDepth.Depth24Bit;
            ilState.ImageStream = (ImageListStreamer)resources.GetObject("ilState.ImageStream");
            ilState.TransparentColor = Color.Magenta;
            ilState.Images.SetKeyName(0, "StateNo.bmp");
            ilState.Images.SetKeyName(1, "StateYes.bmp");
            ilState.Images.SetKeyName(2, "StateSerious.bmp");
            ilState.Images.SetKeyName(3, "StateWarning.bmp");
            ilState.Images.SetKeyName(4, "StateCritical.bmp");
            // 
            // chkStateImages
            // 
            chkStateImages.Location = new Point(11, 54);
            chkStateImages.Margin = new Padding(4);
            chkStateImages.Name = "chkStateImages";
            chkStateImages.Size = new Size(149, 24);
            chkStateImages.TabIndex = 1;
            chkStateImages.Text = "&State Images";
            toolTip1.SetToolTip(chkStateImages, "Show state images");
            chkStateImages.UseVisualStyleBackColor = true;
            chkStateImages.CheckedChanged += this.chkStateImages_CheckedChanged;
            // 
            // chkExpandoImages
            // 
            chkExpandoImages.Location = new Point(11, 86);
            chkExpandoImages.Margin = new Padding(4);
            chkExpandoImages.Name = "chkExpandoImages";
            chkExpandoImages.Size = new Size(160, 24);
            chkExpandoImages.TabIndex = 2;
            chkExpandoImages.Text = "&Expando Images";
            toolTip1.SetToolTip(chkExpandoImages, "Use custom expando (+/-) images");
            chkExpandoImages.UseVisualStyleBackColor = true;
            chkExpandoImages.CheckedChanged += this.chkExpandoImages_CheckedChanged;
            // 
            // ilExpando
            // 
            ilExpando.ColorDepth = ColorDepth.Depth24Bit;
            ilExpando.ImageStream = (ImageListStreamer)resources.GetObject("ilExpando.ImageStream");
            ilExpando.TransparentColor = Color.Magenta;
            ilExpando.Images.SetKeyName(0, "Collapse.bmp");
            ilExpando.Images.SetKeyName(1, "Expand.bmp");
            // 
            // gbAppearance
            // 
            gbAppearance.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gbAppearance.Controls.Add(chkLargeImages);
            gbAppearance.Controls.Add(chkExpandoImages);
            gbAppearance.Controls.Add(chkFormDrawNode);
            gbAppearance.Controls.Add(chkStateImages);
            gbAppearance.Location = new Point(716, 12);
            gbAppearance.Name = "gbAppearance";
            gbAppearance.Size = new Size(208, 153);
            gbAppearance.TabIndex = 2;
            gbAppearance.TabStop = false;
            gbAppearance.Text = "Appearance";
            // 
            // gbEnumerate
            // 
            gbEnumerate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            gbEnumerate.Controls.Add(txtEnumResults);
            gbEnumerate.Controls.Add(btnEnumNodeSibs);
            gbEnumerate.Controls.Add(btnEnumNode);
            gbEnumerate.Controls.Add(btnEnumTree);
            gbEnumerate.Location = new Point(716, 171);
            gbEnumerate.Name = "gbEnumerate";
            gbEnumerate.Size = new Size(208, 330);
            gbEnumerate.TabIndex = 3;
            gbEnumerate.TabStop = false;
            gbEnumerate.Text = "Enumerate";
            // 
            // txtEnumResults
            // 
            txtEnumResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtEnumResults.Location = new Point(6, 83);
            txtEnumResults.Multiline = true;
            txtEnumResults.Name = "txtEnumResults";
            txtEnumResults.ReadOnly = true;
            txtEnumResults.ScrollBars = ScrollBars.Both;
            txtEnumResults.Size = new Size(195, 241);
            txtEnumResults.TabIndex = 3;
            // 
            // btnEnumNodeSibs
            // 
            btnEnumNodeSibs.Location = new Point(6, 52);
            btnEnumNodeSibs.Name = "btnEnumNodeSibs";
            btnEnumNodeSibs.Size = new Size(195, 25);
            btnEnumNodeSibs.TabIndex = 2;
            btnEnumNodeSibs.Text = "N&ode and Siblings";
            toolTip1.SetToolTip(btnEnumNodeSibs, "Enumerate the selected node and its following siblings");
            btnEnumNodeSibs.UseVisualStyleBackColor = true;
            btnEnumNodeSibs.Click += this.btnEnumNode_Click;
            // 
            // btnEnumNode
            // 
            btnEnumNode.Location = new Point(87, 21);
            btnEnumNode.Name = "btnEnumNode";
            btnEnumNode.Size = new Size(115, 25);
            btnEnumNode.TabIndex = 1;
            btnEnumNode.Text = "&Node Only";
            toolTip1.SetToolTip(btnEnumNode, "Enumerate the selected node");
            btnEnumNode.UseVisualStyleBackColor = true;
            btnEnumNode.Click += this.btnEnumNode_Click;
            // 
            // btnEnumTree
            // 
            btnEnumTree.Location = new Point(6, 21);
            btnEnumTree.Name = "btnEnumTree";
            btnEnumTree.Size = new Size(75, 25);
            btnEnumTree.TabIndex = 0;
            btnEnumTree.Text = "&Tree";
            toolTip1.SetToolTip(btnEnumTree, "Enumerate the entire tree");
            btnEnumTree.UseVisualStyleBackColor = true;
            btnEnumTree.Click += this.btnEnumTree_Click;
            // 
            // btnFindNode
            // 
            btnFindNode.Location = new Point(6, 54);
            btnFindNode.Name = "btnFindNode";
            btnFindNode.Size = new Size(75, 32);
            btnFindNode.TabIndex = 1;
            btnFindNode.Text = "&Find";
            toolTip1.SetToolTip(btnFindNode, "Find a node by its name using the indexer");
            btnFindNode.UseVisualStyleBackColor = true;
            btnFindNode.Click += this.btnFindNode_Click;
            // 
            // btnCheckedNames
            // 
            btnCheckedNames.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCheckedNames.Location = new Point(716, 605);
            btnCheckedNames.Name = "btnCheckedNames";
            btnCheckedNames.Size = new Size(75, 32);
            btnCheckedNames.TabIndex = 5;
            btnCheckedNames.Text = "N&ames";
            toolTip1.SetToolTip(btnCheckedNames, "Display the names of all checked nodes");
            btnCheckedNames.UseVisualStyleBackColor = true;
            btnCheckedNames.Click += this.btnCheckedNames_Click;
            // 
            // btnCheckedText
            // 
            btnCheckedText.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCheckedText.Location = new Point(849, 605);
            btnCheckedText.Name = "btnCheckedText";
            btnCheckedText.Size = new Size(75, 32);
            btnCheckedText.TabIndex = 6;
            btnCheckedText.Text = "Te&xt";
            toolTip1.SetToolTip(btnCheckedText, "Display the text for all checked nodes");
            btnCheckedText.UseVisualStyleBackColor = true;
            btnCheckedText.Click += this.btnCheckedText_Click;
            // 
            // gbFindByName
            // 
            gbFindByName.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            gbFindByName.Controls.Add(btnFindNode);
            gbFindByName.Controls.Add(txtFindName);
            gbFindByName.Location = new Point(716, 507);
            gbFindByName.Name = "gbFindByName";
            gbFindByName.Size = new Size(208, 92);
            gbFindByName.TabIndex = 4;
            gbFindByName.TabStop = false;
            gbFindByName.Text = "Find &by Node Name";
            // 
            // txtFindName
            // 
            txtFindName.Location = new Point(6, 21);
            txtFindName.Name = "txtFindName";
            txtFindName.Size = new Size(196, 27);
            txtFindName.TabIndex = 0;
            txtFindName.Text = "Fld2Bk1Pg3";
            // 
            // tvExtTree
            // 
            tvExtTree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvExtTree.DrawDefaultImages = false;
            tvExtTree.ImageIndex = 0;
            tvExtTree.ImageList = ilSmall;
            tvExtTree.Location = new Point(12, 12);
            tvExtTree.Name = "tvExtTree";
            tvExtTree.SelectedImageIndex = 1;
            tvExtTree.ShowNodeToolTips = true;
            tvExtTree.Size = new Size(250, 620);
            tvExtTree.TabIndex = 0;
            tvExtTree.ChangeStateImage += this.tvExtTree_ChangeStateImage;
            tvExtTree.TreeNodeDrawing += this.tvExtTree_TreeNodeDrawing;
            tvExtTree.TreeNodeDrawn += this.tvExtTree_TreeNodeDrawn;
            // 
            // ExtendedTreeViewTestForm
            // 
            this.ClientSize = new Size(936, 644);
            this.Controls.Add(btnCheckedText);
            this.Controls.Add(btnCheckedNames);
            this.Controls.Add(gbFindByName);
            this.Controls.Add(gbEnumerate);
            this.Controls.Add(gbAppearance);
            this.Controls.Add(pgProps);
            this.Controls.Add(tvExtTree);
            this.Margin = new Padding(4);
            this.MinimumSize = new Size(865, 500);
            this.Name = "ExtendedTreeViewTestForm";
            this.SizeGripStyle = SizeGripStyle.Show;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Test Extended Tree View Control";
            gbAppearance.ResumeLayout(false);
            gbEnumerate.ResumeLayout(false);
            gbEnumerate.PerformLayout();
            gbFindByName.ResumeLayout(false);
            gbFindByName.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ImageList ilSmall;
        private EWSoftware.ListControls.ExtendedTreeView tvExtTree;
        private System.Windows.Forms.CheckBox chkLargeImages;
        private System.Windows.Forms.CheckBox chkFormDrawNode;
        private System.Windows.Forms.ImageList ilLarge;
        private System.Windows.Forms.PropertyGrid pgProps;
        private System.Windows.Forms.ImageList ilState;
        private System.Windows.Forms.CheckBox chkStateImages;
        private System.Windows.Forms.CheckBox chkExpandoImages;
        private System.Windows.Forms.ImageList ilExpando;
        private System.Windows.Forms.GroupBox gbAppearance;
        private System.Windows.Forms.GroupBox gbEnumerate;
        private System.Windows.Forms.TextBox txtEnumResults;
        private System.Windows.Forms.Button btnEnumNodeSibs;
        private System.Windows.Forms.Button btnEnumNode;
        private System.Windows.Forms.Button btnEnumTree;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbFindByName;
        private System.Windows.Forms.TextBox txtFindName;
        private System.Windows.Forms.Button btnFindNode;
        private System.Windows.Forms.Button btnCheckedNames;
        private System.Windows.Forms.Button btnCheckedText;
    }
}

