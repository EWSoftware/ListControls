namespace ListControlDemoCS
{
    partial class MenuRow
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
            lblDemoImage = new ClickableLabel();
            lblDemoName = new ClickableLabel();
            btnDemo = new Button();
            lblDemoDesc = new ClickableLabel();
            this.SuspendLayout();
            // 
            // lblDemoImage
            // 
            lblDemoImage.BackColor = SystemColors.ControlLight;
            lblDemoImage.Location = new Point(26, 65);
            lblDemoImage.Name = "lblDemoImage";
            lblDemoImage.Size = new Size(32, 32);
            lblDemoImage.TabIndex = 1;
            // 
            // lblDemoName
            // 
            lblDemoName.BackColor = SystemColors.ActiveCaption;
            lblDemoName.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDemoName.ForeColor = SystemColors.ActiveCaptionText;
            lblDemoName.Location = new Point(0, 0);
            lblDemoName.Name = "lblDemoName";
            lblDemoName.Size = new Size(950, 23);
            lblDemoName.TabIndex = 0;
            lblDemoName.Text = "Demo Name";
            lblDemoName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnDemo
            // 
            btnDemo.BackColor = SystemColors.Control;
            btnDemo.Location = new Point(859, 65);
            btnDemo.Name = "btnDemo";
            btnDemo.Size = new Size(75, 32);
            btnDemo.TabIndex = 3;
            btnDemo.Text = "&Demo";
            btnDemo.UseVisualStyleBackColor = false;
            btnDemo.Click += this.btnDemo_Click;
            // 
            // lblDemoDesc
            // 
            lblDemoDesc.Location = new Point(100, 25);
            lblDemoDesc.Name = "lblDemoDesc";
            lblDemoDesc.Size = new Size(753, 112);
            lblDemoDesc.TabIndex = 2;
            lblDemoDesc.Text = "Demo Description";
            lblDemoDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MenuRow
            // 
            this.BackColor = SystemColors.Window;
            this.Controls.Add(lblDemoDesc);
            this.Controls.Add(btnDemo);
            this.Controls.Add(lblDemoName);
            this.Controls.Add(lblDemoImage);
            this.Name = "MenuRow";
            this.Size = new Size(950, 141);
            this.ResumeLayout(false);
        }
        #endregion

        private EWSoftware.ListControls.ClickableLabel lblDemoImage;
        private EWSoftware.ListControls.ClickableLabel lblDemoName;
        private System.Windows.Forms.Button btnDemo;
        private EWSoftware.ListControls.ClickableLabel lblDemoDesc;
    }
}
