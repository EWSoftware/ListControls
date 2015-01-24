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
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.epErrors = new System.Windows.Forms.ErrorProvider();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(4, 9);
            this.txtPhoneNumber.MaxLength = 20;
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(156, 22);
            this.txtPhoneNumber.TabIndex = 0;
            this.txtPhoneNumber.Text = "";
            // 
            // epErrors
            // 
            this.epErrors.ContainerControl = this;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.Location = new System.Drawing.Point(176, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // PhoneRow
            // 
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtPhoneNumber);
            this.Name = "PhoneRow";
            this.Size = new System.Drawing.Size(256, 40);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.PhoneRow_Validating);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ErrorProvider epErrors;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Button btnDelete;
    }
}

