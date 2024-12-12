namespace ListControlDemoCS
{
    partial class AddressFooter
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
            label1 = new Label();
            lblTotal = new Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(384, 9);
            label1.Name = "label1";
            label1.Size = new Size(107, 23);
            label1.TabIndex = 0;
            label1.Text = "Demo Total";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            lblTotal.BorderStyle = BorderStyle.Fixed3D;
            lblTotal.Location = new Point(497, 9);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(56, 23);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "0";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AddressFooter
            // 
            this.BackColor = Color.Gainsboro;
            this.Controls.Add(lblTotal);
            this.Controls.Add(label1);
            this.Name = "AddressFooter";
            this.Size = new Size(580, 40);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotal;
    }
}
