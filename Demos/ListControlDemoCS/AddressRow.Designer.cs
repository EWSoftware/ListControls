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
            components = new Container();
            cboState = new MultiColumnComboBox();
            txtZip = new TextBox();
            label4 = new ClickableLabel();
            txtCity = new TextBox();
            label3 = new ClickableLabel();
            txtAddress = new TextBox();
            label2 = new ClickableLabel();
            txtLName = new TextBox();
            label1 = new ClickableLabel();
            txtFName = new TextBox();
            epErrors = new ErrorProvider(components);
            label5 = new ClickableLabel();
            udcSumValue = new NumericUpDown();
            ((ISupportInitialize)cboState).BeginInit();
            ((ISupportInitialize)epErrors).BeginInit();
            ((ISupportInitialize)udcSumValue).BeginInit();
            this.SuspendLayout();
            // 
            // cboState
            // 
            cboState.DropDownStyle = ComboBoxStyle.DropDownList;
            cboState.Location = new Point(266, 73);
            cboState.MaxDropDownItems = 16;
            cboState.Name = "cboState";
            cboState.Size = new Size(66, 29);
            cboState.TabIndex = 8;
            // 
            // txtZip
            // 
            txtZip.Location = new Point(338, 73);
            txtZip.MaxLength = 10;
            txtZip.Name = "txtZip";
            txtZip.Size = new Size(92, 27);
            txtZip.TabIndex = 9;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(3, 73);
            label4.Name = "label4";
            label4.Size = new Size(93, 22);
            label4.TabIndex = 6;
            label4.Text = "&City/St/Zip";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(102, 73);
            txtCity.MaxLength = 20;
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(158, 27);
            txtCity.TabIndex = 7;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(20, 42);
            label3.Name = "label3";
            label3.Size = new Size(76, 22);
            label3.TabIndex = 4;
            label3.Text = "&Address";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(102, 40);
            txtAddress.MaxLength = 50;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(451, 27);
            txtAddress.TabIndex = 5;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(3, 9);
            label2.Name = "label2";
            label2.Size = new Size(93, 22);
            label2.TabIndex = 0;
            label2.Text = "&Last Name";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtLName
            // 
            txtLName.Location = new Point(102, 7);
            txtLName.MaxLength = 30;
            txtLName.Name = "txtLName";
            txtLName.Size = new Size(177, 27);
            txtLName.TabIndex = 1;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(285, 9);
            label1.Name = "label1";
            label1.Size = new Size(92, 22);
            label1.TabIndex = 2;
            label1.Text = "First Name";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtFName
            // 
            txtFName.Location = new Point(383, 7);
            txtFName.MaxLength = 20;
            txtFName.Name = "txtFName";
            txtFName.Size = new Size(170, 27);
            txtFName.TabIndex = 3;
            // 
            // epErrors
            // 
            epErrors.ContainerControl = this;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(438, 74);
            label5.Name = "label5";
            label5.Size = new Size(53, 22);
            label5.TabIndex = 10;
            label5.Text = "&Value";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // udcSumValue
            // 
            udcSumValue.Location = new Point(497, 73);
            udcSumValue.Name = "udcSumValue";
            udcSumValue.Size = new Size(56, 27);
            udcSumValue.TabIndex = 11;
            udcSumValue.TextAlign = HorizontalAlignment.Right;
            // 
            // AddressRow
            // 
            this.Controls.Add(udcSumValue);
            this.Controls.Add(label5);
            this.Controls.Add(cboState);
            this.Controls.Add(txtZip);
            this.Controls.Add(label4);
            this.Controls.Add(txtCity);
            this.Controls.Add(label3);
            this.Controls.Add(txtAddress);
            this.Controls.Add(label2);
            this.Controls.Add(txtLName);
            this.Controls.Add(label1);
            this.Controls.Add(txtFName);
            this.Name = "AddressRow";
            this.Size = new Size(580, 108);
            this.Validating += this.AddressRow_Validating;
            ((ISupportInitialize)cboState).EndInit();
            ((ISupportInitialize)epErrors).EndInit();
            ((ISupportInitialize)udcSumValue).EndInit();
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
