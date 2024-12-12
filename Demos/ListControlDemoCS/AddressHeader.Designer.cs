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
            label1 = new ClickableLabel();
            lblKey = new ClickableLabel();
            label2 = new ClickableLabel();
            txtFindName = new TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(13, 9);
            label1.Name = "label1";
            label1.Size = new Size(31, 23);
            label1.TabIndex = 0;
            label1.Text = "ID";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblKey
            // 
            lblKey.BorderStyle = BorderStyle.Fixed3D;
            lblKey.Location = new Point(50, 9);
            lblKey.Name = "lblKey";
            lblKey.Size = new Size(64, 23);
            lblKey.TabIndex = 1;
            lblKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Location = new Point(120, 9);
            label2.Name = "label2";
            label2.Size = new Size(122, 23);
            label2.TabIndex = 2;
            label2.Text = "&Find Last Name";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtFindName
            // 
            txtFindName.Location = new Point(248, 7);
            txtFindName.MaxLength = 30;
            txtFindName.Name = "txtFindName";
            txtFindName.Size = new Size(212, 27);
            txtFindName.TabIndex = 3;
            txtFindName.TextChanged += this.txtFindName_TextChanged;
            // 
            // AddressHeader
            // 
            this.BackColor = Color.Gainsboro;
            this.Controls.Add(txtFindName);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(lblKey);
            this.Name = "AddressHeader";
            this.Size = new Size(580, 40);
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
