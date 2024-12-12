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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            dlMenu = new DataList();
            this.SuspendLayout();
            // 
            // dlMenu
            // 
            dlMenu.AllowAdditions = false;
            dlMenu.AllowDeletes = false;
            dlMenu.AllowEdits = false;
            dlMenu.BackColor = SystemColors.Window;
            dlMenu.Dock = DockStyle.Fill;
            dlMenu.Location = new Point(0, 0);
            dlMenu.Name = "dlMenu";
            dlMenu.NavigationControlsVisible = false;
            dlMenu.RowHeadersVisible = false;
            dlMenu.SeparatorsVisible = false;
            dlMenu.Size = new Size(982, 721);
            dlMenu.TabIndex = 0;
            // 
            // MainForm
            // 
            this.ClientSize = new Size(982, 721);
            this.Controls.Add(dlMenu);
            this.Icon = (Icon)resources.GetObject("$this.Icon");
            this.MaximumSize = new Size(1000, 2048);
            this.MinimumSize = new Size(1000, 615);
            this.Name = "MainForm";
            this.SizeGripStyle = SizeGripStyle.Show;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "EWSoftware List Controls C# Demo";
            this.ResumeLayout(false);
        }
        #endregion

        private EWSoftware.ListControls.DataList dlMenu;
    }
}
