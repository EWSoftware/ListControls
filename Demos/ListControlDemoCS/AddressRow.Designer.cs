namespace ListControlDemoCS
{
    partial class AddressRow
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
            this.components = new System.ComponentModel.Container();
            this.cboState = new EWSoftware.ListControls.MultiColumnComboBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.label4 = new EWSoftware.ListControls.ClickableLabel();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label3 = new EWSoftware.ListControls.ClickableLabel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label2 = new EWSoftware.ListControls.ClickableLabel();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.label1 = new EWSoftware.ListControls.ClickableLabel();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.epErrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new EWSoftware.ListControls.ClickableLabel();
            this.udcSumValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.cboState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcSumValue)).BeginInit();
            this.SuspendLayout();
            // 
            // cboState
            // 
            this.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboState.Location = new System.Drawing.Point(223, 72);
            this.cboState.MaxDropDownItems = 16;
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(57, 24);
            this.cboState.TabIndex = 8;
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(288, 72);
            this.txtZip.MaxLength = 10;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(77, 22);
            this.txtZip.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(3, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "&City/St/Zip";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(88, 72);
            this.txtCity.MaxLength = 20;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(120, 22);
            this.txtCity.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(6, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "&Address";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(88, 40);
            this.txtAddress.MaxLength = 50;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(408, 22);
            this.txtAddress.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(0, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "&Last Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(88, 9);
            this.txtLName.MaxLength = 30;
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(160, 22);
            this.txtLName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(255, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "First Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(341, 9);
            this.txtFName.MaxLength = 20;
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(155, 22);
            this.txtFName.TabIndex = 3;
            // 
            // epErrors
            // 
            this.epErrors.ContainerControl = this;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(381, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 22);
            this.label5.TabIndex = 10;
            this.label5.Text = "&Value";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // udcSumValue
            // 
            this.udcSumValue.Location = new System.Drawing.Point(440, 72);
            this.udcSumValue.Name = "udcSumValue";
            this.udcSumValue.Size = new System.Drawing.Size(56, 22);
            this.udcSumValue.TabIndex = 11;
            this.udcSumValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // AddressRow
            // 
            this.Controls.Add(this.udcSumValue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboState);
            this.Controls.Add(this.txtZip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFName);
            this.Name = "AddressRow";
            this.Size = new System.Drawing.Size(504, 106);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.AddressRow_Validating);
            ((System.ComponentModel.ISupportInitialize)(this.cboState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcSumValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private EWSoftware.ListControls.MultiColumnComboBox cboState;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtLName;
        private EWSoftware.ListControls.ClickableLabel label1;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.ErrorProvider epErrors;
        private EWSoftware.ListControls.ClickableLabel label4;
        private EWSoftware.ListControls.ClickableLabel label3;
        private EWSoftware.ListControls.ClickableLabel label2;
        private EWSoftware.ListControls.ClickableLabel label5;
        private System.Windows.Forms.NumericUpDown udcSumValue;
    }
}
