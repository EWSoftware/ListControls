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
            this.lblDemoImage = new EWSoftware.ListControls.ClickableLabel();
            this.lblDemoName = new EWSoftware.ListControls.ClickableLabel();
            this.btnDemo = new System.Windows.Forms.Button();
            this.lblDemoDesc = new EWSoftware.ListControls.ClickableLabel();
            this.SuspendLayout();
            // 
            // lblDemoImage
            // 
            this.lblDemoImage.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblDemoImage.Location = new System.Drawing.Point(6, 62);
            this.lblDemoImage.Name = "lblDemoImage";
            this.lblDemoImage.Size = new System.Drawing.Size(32, 32);
            this.lblDemoImage.TabIndex = 1;
            // 
            // lblDemoName
            // 
            this.lblDemoName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblDemoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblDemoName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDemoName.Location = new System.Drawing.Point(0, 0);
            this.lblDemoName.Name = "lblDemoName";
            this.lblDemoName.Size = new System.Drawing.Size(700, 23);
            this.lblDemoName.TabIndex = 0;
            this.lblDemoName.Text = "Demo Name";
            this.lblDemoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDemo
            // 
            this.btnDemo.BackColor = System.Drawing.SystemColors.Control;
            this.btnDemo.Location = new System.Drawing.Point(618, 62);
            this.btnDemo.Name = "btnDemo";
            this.btnDemo.Size = new System.Drawing.Size(75, 32);
            this.btnDemo.TabIndex = 3;
            this.btnDemo.Text = "&Demo";
            this.btnDemo.Click += new System.EventHandler(this.btnDemo_Click);
            // 
            // lblDemoDesc
            // 
            this.lblDemoDesc.Location = new System.Drawing.Point(45, 25);
            this.lblDemoDesc.Name = "lblDemoDesc";
            this.lblDemoDesc.Size = new System.Drawing.Size(565, 112);
            this.lblDemoDesc.TabIndex = 2;
            this.lblDemoDesc.Text = "Demo Description";
            this.lblDemoDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // MenuRow
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.lblDemoDesc);
            this.Controls.Add(this.btnDemo);
            this.Controls.Add(this.lblDemoName);
            this.Controls.Add(this.lblDemoImage);
            this.Name = "MenuRow";
            this.Size = new System.Drawing.Size(700, 141);
            this.ResumeLayout(false);

        }
        #endregion

        private EWSoftware.ListControls.ClickableLabel lblDemoImage;
        private EWSoftware.ListControls.ClickableLabel lblDemoName;
        private System.Windows.Forms.Button btnDemo;
        private EWSoftware.ListControls.ClickableLabel lblDemoDesc;
    }
}
