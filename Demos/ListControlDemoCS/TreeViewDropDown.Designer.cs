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
            tvItems = new TreeView();
            chkExcludeDiscontinued = new CheckBox();
            btnSelect = new Button();
            btnCancel = new Button();
            this.SuspendLayout();
            // 
            // tvItems
            // 
            tvItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tvItems.HideSelection = false;
            tvItems.Location = new Point(8, 3);
            tvItems.Name = "tvItems";
            tvItems.Size = new Size(284, 228);
            tvItems.TabIndex = 0;
            tvItems.AfterSelect += this.tvItems_AfterSelect;
            tvItems.DoubleClick += this.tvItems_DoubleClick;
            // 
            // chkExcludeDiscontinued
            // 
            chkExcludeDiscontinued.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkExcludeDiscontinued.Location = new Point(8, 237);
            chkExcludeDiscontinued.Name = "chkExcludeDiscontinued";
            chkExcludeDiscontinued.Size = new Size(284, 24);
            chkExcludeDiscontinued.TabIndex = 1;
            chkExcludeDiscontinued.Text = "&Exclude discontinued products";
            chkExcludeDiscontinued.CheckedChanged += this.chkExcludeDiscontinued_CheckedChanged;
            // 
            // btnSelect
            // 
            btnSelect.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSelect.Location = new Point(146, 267);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(70, 30);
            btnSelect.TabIndex = 2;
            btnSelect.Text = "&Select";
            btnSelect.Click += this.btnSelect_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(222, 267);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(70, 30);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "&Cancel";
            btnCancel.Click += this.btnCancel_Click;
            // 
            // TreeViewDropDown
            // 
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnSelect);
            this.Controls.Add(chkExcludeDiscontinued);
            this.Controls.Add(tvItems);
            this.MinimumSize = new Size(300, 150);
            this.Name = "TreeViewDropDown";
            this.Size = new Size(300, 300);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.CheckBox chkExcludeDiscontinued;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TreeView tvItems;
        private System.Windows.Forms.Button btnCancel;
    }
}
