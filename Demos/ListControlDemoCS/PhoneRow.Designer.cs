namespace ListControlDemoCS
{
    partial class PhoneRow
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
            components = new Container();
            txtPhoneNumber = new MaskedTextBox();
            epErrors = new ErrorProvider(components);
            btnDelete = new Button();
            ((ISupportInitialize)epErrors).BeginInit();
            this.SuspendLayout();
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(4, 9);
            txtPhoneNumber.Mask = "(000) 000-0000";
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(156, 27);
            txtPhoneNumber.TabIndex = 0;
            // 
            // epErrors
            // 
            epErrors.ContainerControl = this;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(178, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 32);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "&Delete";
            btnDelete.Click += this.btnDelete_Click;
            // 
            // PhoneRow
            // 
            this.Controls.Add(btnDelete);
            this.Controls.Add(txtPhoneNumber);
            this.Name = "PhoneRow";
            this.Size = new Size(256, 40);
            this.Validating += this.PhoneRow_Validating;
            ((ISupportInitialize)epErrors).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.ErrorProvider epErrors;
        private System.Windows.Forms.MaskedTextBox txtPhoneNumber;
        private System.Windows.Forms.Button btnDelete;
    }
}

