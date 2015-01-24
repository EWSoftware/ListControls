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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtendedTreeViewTestForm));
            this.ilSmall = new System.Windows.Forms.ImageList(this.components);
            this.chkLargeImages = new System.Windows.Forms.CheckBox();
            this.chkFormDrawNode = new System.Windows.Forms.CheckBox();
            this.ilLarge = new System.Windows.Forms.ImageList(this.components);
            this.pgProps = new System.Windows.Forms.PropertyGrid();
            this.ilState = new System.Windows.Forms.ImageList(this.components);
            this.chkStateImages = new System.Windows.Forms.CheckBox();
            this.chkExpandoImages = new System.Windows.Forms.CheckBox();
            this.ilExpando = new System.Windows.Forms.ImageList(this.components);
            this.gbAppearance = new System.Windows.Forms.GroupBox();
            this.gbEnumerate = new System.Windows.Forms.GroupBox();
            this.txtEnumResults = new System.Windows.Forms.TextBox();
            this.btnEnumNodeSibs = new System.Windows.Forms.Button();
            this.btnEnumNode = new System.Windows.Forms.Button();
            this.btnEnumTree = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnFindNode = new System.Windows.Forms.Button();
            this.btnCheckedNames = new System.Windows.Forms.Button();
            this.btnCheckedText = new System.Windows.Forms.Button();
            this.gbFindByName = new System.Windows.Forms.GroupBox();
            this.txtFindName = new System.Windows.Forms.TextBox();
            this.tvExtTree = new EWSoftware.ListControls.ExtendedTreeView();
            this.gbAppearance.SuspendLayout();
            this.gbEnumerate.SuspendLayout();
            this.gbFindByName.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilSmall
            // 
            this.ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmall.ImageStream")));
            this.ilSmall.TransparentColor = System.Drawing.Color.Magenta;
            this.ilSmall.Images.SetKeyName(0, "FolderClosed.bmp");
            this.ilSmall.Images.SetKeyName(1, "FolderOpen.bmp");
            this.ilSmall.Images.SetKeyName(2, "BookClosed.bmp");
            this.ilSmall.Images.SetKeyName(3, "BookOpen.bmp");
            this.ilSmall.Images.SetKeyName(4, "Document.bmp");
            // 
            // chkLargeImages
            // 
            this.chkLargeImages.Location = new System.Drawing.Point(11, 22);
            this.chkLargeImages.Margin = new System.Windows.Forms.Padding(4);
            this.chkLargeImages.Name = "chkLargeImages";
            this.chkLargeImages.Size = new System.Drawing.Size(149, 21);
            this.chkLargeImages.TabIndex = 0;
            this.chkLargeImages.Text = "&Large Images";
            this.toolTip1.SetToolTip(this.chkLargeImages, "Use large images");
            this.chkLargeImages.UseVisualStyleBackColor = true;
            this.chkLargeImages.CheckedChanged += new System.EventHandler(this.chkLargeImages_CheckedChanged);
            // 
            // chkFormDrawNode
            // 
            this.chkFormDrawNode.Location = new System.Drawing.Point(11, 109);
            this.chkFormDrawNode.Margin = new System.Windows.Forms.Padding(4);
            this.chkFormDrawNode.Name = "chkFormDrawNode";
            this.chkFormDrawNode.Size = new System.Drawing.Size(190, 21);
            this.chkFormDrawNode.TabIndex = 3;
            this.chkFormDrawNode.Text = "Form &DrawNode Events";
            this.toolTip1.SetToolTip(this.chkFormDrawNode, "Use the forms After and Before DrawNode events");
            this.chkFormDrawNode.UseVisualStyleBackColor = true;
            this.chkFormDrawNode.CheckedChanged += new System.EventHandler(this.chkFormDrawNode_CheckedChanged);
            // 
            // ilLarge
            // 
            this.ilLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLarge.ImageStream")));
            this.ilLarge.TransparentColor = System.Drawing.Color.Magenta;
            this.ilLarge.Images.SetKeyName(0, "FolderClosed.bmp");
            this.ilLarge.Images.SetKeyName(1, "FolderOpen.bmp");
            this.ilLarge.Images.SetKeyName(2, "BookClosed.bmp");
            this.ilLarge.Images.SetKeyName(3, "BookOpen.bmp");
            this.ilLarge.Images.SetKeyName(4, "Document.bmp");
            // 
            // pgProps
            // 
            this.pgProps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pgProps.Location = new System.Drawing.Point(268, 12);
            this.pgProps.Name = "pgProps";
            this.pgProps.Size = new System.Drawing.Size(442, 620);
            this.pgProps.TabIndex = 1;
            this.pgProps.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgProps_PropertyValueChanged);
            // 
            // ilState
            // 
            this.ilState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilState.ImageStream")));
            this.ilState.TransparentColor = System.Drawing.Color.Magenta;
            this.ilState.Images.SetKeyName(0, "StateNo.bmp");
            this.ilState.Images.SetKeyName(1, "StateYes.bmp");
            this.ilState.Images.SetKeyName(2, "StateSerious.bmp");
            this.ilState.Images.SetKeyName(3, "StateWarning.bmp");
            this.ilState.Images.SetKeyName(4, "StateCritical.bmp");
            // 
            // chkStateImages
            // 
            this.chkStateImages.Location = new System.Drawing.Point(11, 51);
            this.chkStateImages.Margin = new System.Windows.Forms.Padding(4);
            this.chkStateImages.Name = "chkStateImages";
            this.chkStateImages.Size = new System.Drawing.Size(149, 21);
            this.chkStateImages.TabIndex = 1;
            this.chkStateImages.Text = "&State Images";
            this.toolTip1.SetToolTip(this.chkStateImages, "Show state images");
            this.chkStateImages.UseVisualStyleBackColor = true;
            this.chkStateImages.CheckedChanged += new System.EventHandler(this.chkStateImages_CheckedChanged);
            // 
            // chkExpandoImages
            // 
            this.chkExpandoImages.Location = new System.Drawing.Point(11, 80);
            this.chkExpandoImages.Margin = new System.Windows.Forms.Padding(4);
            this.chkExpandoImages.Name = "chkExpandoImages";
            this.chkExpandoImages.Size = new System.Drawing.Size(160, 21);
            this.chkExpandoImages.TabIndex = 2;
            this.chkExpandoImages.Text = "&Expando Images";
            this.toolTip1.SetToolTip(this.chkExpandoImages, "Use custom expando (+/-) images");
            this.chkExpandoImages.UseVisualStyleBackColor = true;
            this.chkExpandoImages.CheckedChanged += new System.EventHandler(this.chkExpandoImages_CheckedChanged);
            // 
            // ilExpando
            // 
            this.ilExpando.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilExpando.ImageStream")));
            this.ilExpando.TransparentColor = System.Drawing.Color.Magenta;
            this.ilExpando.Images.SetKeyName(0, "Collapse.bmp");
            this.ilExpando.Images.SetKeyName(1, "Expand.bmp");
            // 
            // gbAppearance
            // 
            this.gbAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAppearance.Controls.Add(this.chkLargeImages);
            this.gbAppearance.Controls.Add(this.chkExpandoImages);
            this.gbAppearance.Controls.Add(this.chkFormDrawNode);
            this.gbAppearance.Controls.Add(this.chkStateImages);
            this.gbAppearance.Location = new System.Drawing.Point(716, 12);
            this.gbAppearance.Name = "gbAppearance";
            this.gbAppearance.Size = new System.Drawing.Size(208, 139);
            this.gbAppearance.TabIndex = 2;
            this.gbAppearance.TabStop = false;
            this.gbAppearance.Text = "Appearance";
            // 
            // gbEnumerate
            // 
            this.gbEnumerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEnumerate.Controls.Add(this.txtEnumResults);
            this.gbEnumerate.Controls.Add(this.btnEnumNodeSibs);
            this.gbEnumerate.Controls.Add(this.btnEnumNode);
            this.gbEnumerate.Controls.Add(this.btnEnumTree);
            this.gbEnumerate.Location = new System.Drawing.Point(716, 157);
            this.gbEnumerate.Name = "gbEnumerate";
            this.gbEnumerate.Size = new System.Drawing.Size(208, 357);
            this.gbEnumerate.TabIndex = 3;
            this.gbEnumerate.TabStop = false;
            this.gbEnumerate.Text = "Enumerate";
            // 
            // txtEnumResults
            // 
            this.txtEnumResults.Location = new System.Drawing.Point(6, 83);
            this.txtEnumResults.Multiline = true;
            this.txtEnumResults.Name = "txtEnumResults";
            this.txtEnumResults.ReadOnly = true;
            this.txtEnumResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtEnumResults.Size = new System.Drawing.Size(195, 268);
            this.txtEnumResults.TabIndex = 3;
            // 
            // btnEnumNodeSibs
            // 
            this.btnEnumNodeSibs.Location = new System.Drawing.Point(6, 52);
            this.btnEnumNodeSibs.Name = "btnEnumNodeSibs";
            this.btnEnumNodeSibs.Size = new System.Drawing.Size(195, 25);
            this.btnEnumNodeSibs.TabIndex = 2;
            this.btnEnumNodeSibs.Text = "N&ode and Siblings";
            this.toolTip1.SetToolTip(this.btnEnumNodeSibs, "Enumerate the selected node and its following siblings");
            this.btnEnumNodeSibs.UseVisualStyleBackColor = true;
            this.btnEnumNodeSibs.Click += new System.EventHandler(this.btnEnumNode_Click);
            // 
            // btnEnumNode
            // 
            this.btnEnumNode.Location = new System.Drawing.Point(87, 21);
            this.btnEnumNode.Name = "btnEnumNode";
            this.btnEnumNode.Size = new System.Drawing.Size(115, 25);
            this.btnEnumNode.TabIndex = 1;
            this.btnEnumNode.Text = "&Node Only";
            this.toolTip1.SetToolTip(this.btnEnumNode, "Enumerate the selected node");
            this.btnEnumNode.UseVisualStyleBackColor = true;
            this.btnEnumNode.Click += new System.EventHandler(this.btnEnumNode_Click);
            // 
            // btnEnumTree
            // 
            this.btnEnumTree.Location = new System.Drawing.Point(6, 21);
            this.btnEnumTree.Name = "btnEnumTree";
            this.btnEnumTree.Size = new System.Drawing.Size(75, 25);
            this.btnEnumTree.TabIndex = 0;
            this.btnEnumTree.Text = "&Tree";
            this.toolTip1.SetToolTip(this.btnEnumTree, "Enumerate the entire tree");
            this.btnEnumTree.UseVisualStyleBackColor = true;
            this.btnEnumTree.Click += new System.EventHandler(this.btnEnumTree_Click);
            // 
            // btnFindNode
            // 
            this.btnFindNode.Location = new System.Drawing.Point(6, 48);
            this.btnFindNode.Name = "btnFindNode";
            this.btnFindNode.Size = new System.Drawing.Size(75, 25);
            this.btnFindNode.TabIndex = 1;
            this.btnFindNode.Text = "&Find";
            this.toolTip1.SetToolTip(this.btnFindNode, "Find a node by its name using the indexer");
            this.btnFindNode.UseVisualStyleBackColor = true;
            this.btnFindNode.Click += new System.EventHandler(this.btnFindNode_Click);
            // 
            // btnCheckedNames
            // 
            this.btnCheckedNames.Location = new System.Drawing.Point(716, 605);
            this.btnCheckedNames.Name = "btnCheckedNames";
            this.btnCheckedNames.Size = new System.Drawing.Size(75, 25);
            this.btnCheckedNames.TabIndex = 5;
            this.btnCheckedNames.Text = "N&ames";
            this.toolTip1.SetToolTip(this.btnCheckedNames, "Display the names of all checked nodes");
            this.btnCheckedNames.UseVisualStyleBackColor = true;
            this.btnCheckedNames.Click += new System.EventHandler(this.btnCheckedNames_Click);
            // 
            // btnCheckedText
            // 
            this.btnCheckedText.Location = new System.Drawing.Point(849, 605);
            this.btnCheckedText.Name = "btnCheckedText";
            this.btnCheckedText.Size = new System.Drawing.Size(75, 25);
            this.btnCheckedText.TabIndex = 6;
            this.btnCheckedText.Text = "Te&xt";
            this.toolTip1.SetToolTip(this.btnCheckedText, "Display the text for all checked nodes");
            this.btnCheckedText.UseVisualStyleBackColor = true;
            this.btnCheckedText.Click += new System.EventHandler(this.btnCheckedText_Click);
            // 
            // gbFindByName
            // 
            this.gbFindByName.Controls.Add(this.btnFindNode);
            this.gbFindByName.Controls.Add(this.txtFindName);
            this.gbFindByName.Location = new System.Drawing.Point(716, 520);
            this.gbFindByName.Name = "gbFindByName";
            this.gbFindByName.Size = new System.Drawing.Size(208, 79);
            this.gbFindByName.TabIndex = 4;
            this.gbFindByName.TabStop = false;
            this.gbFindByName.Text = "Find &by Node Name";
            // 
            // txtFindName
            // 
            this.txtFindName.Location = new System.Drawing.Point(6, 21);
            this.txtFindName.Name = "txtFindName";
            this.txtFindName.Size = new System.Drawing.Size(196, 22);
            this.txtFindName.TabIndex = 0;
            this.txtFindName.Text = "Fld2Bk1Pg3";
            // 
            // tvExtTree
            // 
            this.tvExtTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvExtTree.DrawDefaultImages = false;
            this.tvExtTree.ImageIndex = 0;
            this.tvExtTree.ImageList = this.ilSmall;
            this.tvExtTree.Location = new System.Drawing.Point(12, 12);
            this.tvExtTree.Name = "tvExtTree";
            this.tvExtTree.SelectedImageIndex = 1;
            this.tvExtTree.ShowNodeToolTips = true;
            this.tvExtTree.Size = new System.Drawing.Size(250, 620);
            this.tvExtTree.TabIndex = 0;
            this.tvExtTree.TreeNodeDrawing += new System.EventHandler<EWSoftware.ListControls.DrawTreeNodeExtendedEventArgs>(this.tvExtTree_TreeNodeDrawing);
            this.tvExtTree.TreeNodeDrawn += new System.EventHandler<EWSoftware.ListControls.DrawTreeNodeExtendedEventArgs>(this.tvExtTree_TreeNodeDrawn);
            this.tvExtTree.ChangeStateImage += new System.Windows.Forms.TreeViewEventHandler(this.tvExtTree_ChangeStateImage);
            // 
            // ExtendedTreeViewTestForm
            // 
            this.ClientSize = new System.Drawing.Size(936, 644);
            this.Controls.Add(this.btnCheckedText);
            this.Controls.Add(this.btnCheckedNames);
            this.Controls.Add(this.gbFindByName);
            this.Controls.Add(this.gbEnumerate);
            this.Controls.Add(this.gbAppearance);
            this.Controls.Add(this.pgProps);
            this.Controls.Add(this.tvExtTree);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(740, 209);
            this.Name = "ExtendedTreeViewTestForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Extended Tree View Control";
            this.gbAppearance.ResumeLayout(false);
            this.gbEnumerate.ResumeLayout(false);
            this.gbEnumerate.PerformLayout();
            this.gbFindByName.ResumeLayout(false);
            this.gbFindByName.PerformLayout();
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

