namespace ListControlDemoCS
{
    partial class TreeViewDropDown
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
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tvItems = new System.Windows.Forms.TreeView();
            this.chkExcludeDiscontinued = new System.Windows.Forms.CheckBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tvItems
            // 
            this.tvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvItems.HideSelection = false;
            this.tvItems.Location = new System.Drawing.Point(8, 8);
            this.tvItems.Name = "tvItems";
            this.tvItems.Size = new System.Drawing.Size(234, 208);
            this.tvItems.TabIndex = 0;
            this.tvItems.DoubleClick += new System.EventHandler(this.tvItems_DoubleClick);
            this.tvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvItems_AfterSelect);
            // 
            // chkExcludeDiscontinued
            // 
            this.chkExcludeDiscontinued.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkExcludeDiscontinued.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkExcludeDiscontinued.Location = new System.Drawing.Point(8, 218);
            this.chkExcludeDiscontinued.Name = "chkExcludeDiscontinued";
            this.chkExcludeDiscontinued.Size = new System.Drawing.Size(216, 24);
            this.chkExcludeDiscontinued.TabIndex = 1;
            this.chkExcludeDiscontinued.Text = "&Exclude discontinued products";
            this.chkExcludeDiscontinued.CheckedChanged += new System.EventHandler(this.chkExcludeDiscontinued_CheckedChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSelect.Location = new System.Drawing.Point(108, 244);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(64, 25);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "&Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(178, 244);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TreeViewDropDown
            // 
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.chkExcludeDiscontinued);
            this.Controls.Add(this.tvItems);
            this.MinimumSize = new System.Drawing.Size(240, 150);
            this.Name = "TreeViewDropDown";
            this.Size = new System.Drawing.Size(250, 272);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.CheckBox chkExcludeDiscontinued;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TreeView tvItems;
        private System.Windows.Forms.Button btnCancel;
    }
}
