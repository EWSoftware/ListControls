namespace ListControlDemoCS
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dlMenu = new EWSoftware.ListControls.DataList();
            this.SuspendLayout();
            // 
            // dlMenu
            // 
            this.dlMenu.AllowAdditions = false;
            this.dlMenu.AllowDeletes = false;
            this.dlMenu.AllowEdits = false;
            this.dlMenu.BackColor = System.Drawing.SystemColors.Window;
            this.dlMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dlMenu.Location = new System.Drawing.Point(0, 0);
            this.dlMenu.Name = "dlMenu";
            this.dlMenu.NavigationControlsVisible = false;
            this.dlMenu.RowHeadersVisible = false;
            this.dlMenu.SeparatorsVisible = false;
            this.dlMenu.Size = new System.Drawing.Size(732, 582);
            this.dlMenu.TabIndex = 0;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(732, 582);
            this.Controls.Add(this.dlMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(750, 2048);
            this.MinimumSize = new System.Drawing.Size(750, 615);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EWSoftware List Controls Demo";
            this.ResumeLayout(false);

        }
        #endregion

        private EWSoftware.ListControls.DataList dlMenu;
    }
}
