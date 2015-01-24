namespace ListControlDemoCS
{
    partial class AddressHeader
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
            this.label1 = new EWSoftware.ListControls.ClickableLabel();
            this.lblKey = new EWSoftware.ListControls.ClickableLabel();
            this.label2 = new EWSoftware.ListControls.ClickableLabel();
            this.txtFindName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKey
            // 
            this.lblKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblKey.Location = new System.Drawing.Point(40, 9);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(64, 23);
            this.lblKey.TabIndex = 1;
            this.lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(114, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "&Find Last Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFindName
            // 
            this.txtFindName.Location = new System.Drawing.Point(232, 9);
            this.txtFindName.MaxLength = 30;
            this.txtFindName.Name = "txtFindName";
            this.txtFindName.Size = new System.Drawing.Size(136, 22);
            this.txtFindName.TabIndex = 3;
            this.txtFindName.TextChanged += new System.EventHandler(this.txtFindName_TextChanged);
            // 
            // AddressHeader
            // 
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.txtFindName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblKey);
            this.Name = "AddressHeader";
            this.Size = new System.Drawing.Size(504, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EWSoftware.ListControls.ClickableLabel lblKey;
        private EWSoftware.ListControls.ClickableLabel label1;
        private EWSoftware.ListControls.ClickableLabel label2;
        private System.Windows.Forms.TextBox txtFindName;
    }
}
