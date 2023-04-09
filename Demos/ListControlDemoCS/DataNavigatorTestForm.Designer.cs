namespace ListControlDemoCS
{
    partial class DataNavigatorTestForm
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
            if(disposing)
            {
                dbConn?.Dispose();
                daAddresses.Dispose();
                dsAddresses.Dispose();
                components?.Dispose();
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
            components = new System.ComponentModel.Container();
            btnLoad = new System.Windows.Forms.Button();
            dnNav = new EWSoftware.ListControls.DataNavigator();
            btnSave = new System.Windows.Forms.Button();
            pgProps = new System.Windows.Forms.PropertyGrid();
            btnAddDSRow = new System.Windows.Forms.Button();
            btnDelDSRow = new System.Windows.Forms.Button();
            btnModRow = new System.Windows.Forms.Button();
            udcRowNumber = new System.Windows.Forms.NumericUpDown();
            label6 = new System.Windows.Forms.Label();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            btnGetValue = new System.Windows.Forms.Button();
            cboColumns = new System.Windows.Forms.ComboBox();
            txtValue = new System.Windows.Forms.TextBox();
            txtRowNumber = new System.Windows.Forms.NumericUpDown();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            pnlData = new System.Windows.Forms.Panel();
            txtZip = new System.Windows.Forms.TextBox();
            cboState = new EWSoftware.ListControls.MultiColumnComboBox();
            txtCity = new System.Windows.Forms.TextBox();
            txtAddress = new System.Windows.Forms.TextBox();
            txtLName = new System.Windows.Forms.TextBox();
            txtFName = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            lblKey = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            lblAddRow = new System.Windows.Forms.Label();
            epErrors = new System.Windows.Forms.ErrorProvider(components);
            txtFindName = new System.Windows.Forms.TextBox();
            clickableLabel1 = new EWSoftware.ListControls.ClickableLabel();
            ((System.ComponentModel.ISupportInitialize)udcRowNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtRowNumber).BeginInit();
            pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cboState).BeginInit();
            ((System.ComponentModel.ISupportInitialize)epErrors).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnLoad.Location = new System.Drawing.Point(12, 375);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new System.Drawing.Size(104, 28);
            btnLoad.TabIndex = 8;
            btnLoad.Text = "L&oad Data";
            toolTip1.SetToolTip(btnLoad, "Load data into the control");
            btnLoad.Click += this.btnLoad_Click;
            // 
            // dnNav
            // 
            dnNav.Location = new System.Drawing.Point(12, 184);
            dnNav.Name = "dnNav";
            dnNav.Size = new System.Drawing.Size(282, 22);
            dnNav.TabIndex = 1;
            dnNav.AddedRow += this.dnNav_AddedRow;
            dnNav.DeletingRow += this.dnNav_DeletingRow;
            dnNav.CanceledEdits += this.dnNav_CanceledEdits;
            dnNav.NoRows += this.dnNav_NoRows;
            dnNav.ChangePolicyModified += this.dnNav_ChangePolicyModified;
            dnNav.Validating += this.dnNav_Validating;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnSave.Location = new System.Drawing.Point(122, 375);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(104, 28);
            btnSave.TabIndex = 9;
            btnSave.Text = "&Save Data";
            toolTip1.SetToolTip(btnSave, "Save all changes");
            btnSave.Click += this.btnSave_Click;
            // 
            // pgProps
            // 
            pgProps.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            pgProps.LineColor = System.Drawing.SystemColors.ScrollBar;
            pgProps.Location = new System.Drawing.Point(512, 12);
            pgProps.Name = "pgProps";
            pgProps.Size = new System.Drawing.Size(308, 327);
            pgProps.TabIndex = 15;
            pgProps.PropertyValueChanged += this.pgProps_PropertyValueChanged;
            // 
            // btnAddDSRow
            // 
            btnAddDSRow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnAddDSRow.Location = new System.Drawing.Point(284, 375);
            btnAddDSRow.Name = "btnAddDSRow";
            btnAddDSRow.Size = new System.Drawing.Size(104, 28);
            btnAddDSRow.TabIndex = 10;
            btnAddDSRow.Text = "Add DS Row";
            toolTip1.SetToolTip(btnAddDSRow, "Add row directly to the data source");
            btnAddDSRow.Click += this.btnAddDSRow_Click;
            // 
            // btnDelDSRow
            // 
            btnDelDSRow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnDelDSRow.Location = new System.Drawing.Point(565, 375);
            btnDelDSRow.Name = "btnDelDSRow";
            btnDelDSRow.Size = new System.Drawing.Size(104, 28);
            btnDelDSRow.TabIndex = 13;
            btnDelDSRow.Text = "Del DS Row";
            toolTip1.SetToolTip(btnDelDSRow, "Delete row directly from data source");
            btnDelDSRow.Click += this.btnDelDSRow_Click;
            // 
            // btnModRow
            // 
            btnModRow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnModRow.Location = new System.Drawing.Point(675, 375);
            btnModRow.Name = "btnModRow";
            btnModRow.Size = new System.Drawing.Size(104, 28);
            btnModRow.TabIndex = 14;
            btnModRow.Text = "Modify Row";
            toolTip1.SetToolTip(btnModRow, "Modify row directly in data source");
            btnModRow.Click += this.btnModRow_Click;
            // 
            // udcRowNumber
            // 
            udcRowNumber.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            udcRowNumber.Location = new System.Drawing.Point(495, 377);
            udcRowNumber.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            udcRowNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            udcRowNumber.Name = "udcRowNumber";
            udcRowNumber.Size = new System.Drawing.Size(64, 31);
            udcRowNumber.TabIndex = 12;
            udcRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            udcRowNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label6.Location = new System.Drawing.Point(390, 377);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(99, 23);
            label6.TabIndex = 11;
            label6.Text = "Del/Mod Row";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGetValue
            // 
            btnGetValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnGetValue.Location = new System.Drawing.Point(346, 341);
            btnGetValue.Name = "btnGetValue";
            btnGetValue.Size = new System.Drawing.Size(75, 28);
            btnGetValue.TabIndex = 6;
            btnGetValue.Text = "&Get";
            toolTip1.SetToolTip(btnGetValue, "Get the specified column from the specified row");
            btnGetValue.Click += this.btnGetValue_Click;
            // 
            // cboColumns
            // 
            cboColumns.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboColumns.Items.AddRange(new object[] { "ID", "FirstName", "LastName", "Address", "City", "State", "Zip", "SumValue" });
            cboColumns.Location = new System.Drawing.Point(100, 345);
            cboColumns.Name = "cboColumns";
            cboColumns.Size = new System.Drawing.Size(132, 33);
            cboColumns.TabIndex = 3;
            // 
            // txtValue
            // 
            txtValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtValue.Location = new System.Drawing.Point(427, 345);
            txtValue.Name = "txtValue";
            txtValue.ReadOnly = true;
            txtValue.Size = new System.Drawing.Size(352, 31);
            txtValue.TabIndex = 7;
            txtValue.TabStop = false;
            // 
            // txtRowNumber
            // 
            txtRowNumber.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            txtRowNumber.Location = new System.Drawing.Point(284, 345);
            txtRowNumber.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            txtRowNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtRowNumber.Name = "txtRowNumber";
            txtRowNumber.Size = new System.Drawing.Size(56, 31);
            txtRowNumber.TabIndex = 5;
            txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            txtRowNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label7.Location = new System.Drawing.Point(236, 345);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(48, 23);
            label7.TabIndex = 4;
            label7.Text = "at row";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label8.Location = new System.Drawing.Point(16, 344);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(80, 23);
            label8.TabIndex = 2;
            label8.Text = "G&et column";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlData
            // 
            pnlData.Controls.Add(txtZip);
            pnlData.Controls.Add(cboState);
            pnlData.Controls.Add(txtCity);
            pnlData.Controls.Add(txtAddress);
            pnlData.Controls.Add(txtLName);
            pnlData.Controls.Add(txtFName);
            pnlData.Controls.Add(label1);
            pnlData.Controls.Add(lblKey);
            pnlData.Controls.Add(label5);
            pnlData.Controls.Add(label4);
            pnlData.Controls.Add(label3);
            pnlData.Controls.Add(label2);
            pnlData.Location = new System.Drawing.Point(12, 40);
            pnlData.Name = "pnlData";
            pnlData.Size = new System.Drawing.Size(494, 144);
            pnlData.TabIndex = 0;
            // 
            // txtZip
            // 
            txtZip.Location = new System.Drawing.Point(296, 108);
            txtZip.MaxLength = 10;
            txtZip.Name = "txtZip";
            txtZip.Size = new System.Drawing.Size(77, 31);
            txtZip.TabIndex = 11;
            // 
            // cboState
            // 
            cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboState.Location = new System.Drawing.Point(228, 108);
            cboState.MaxDropDownItems = 16;
            cboState.Name = "cboState";
            cboState.Size = new System.Drawing.Size(57, 34);
            cboState.TabIndex = 10;
            // 
            // txtCity
            // 
            txtCity.Location = new System.Drawing.Point(92, 108);
            txtCity.MaxLength = 20;
            txtCity.Name = "txtCity";
            txtCity.Size = new System.Drawing.Size(125, 31);
            txtCity.TabIndex = 9;
            // 
            // txtAddress
            // 
            txtAddress.Location = new System.Drawing.Point(92, 76);
            txtAddress.MaxLength = 50;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new System.Drawing.Size(389, 31);
            txtAddress.TabIndex = 7;
            // 
            // txtLName
            // 
            txtLName.Location = new System.Drawing.Point(92, 44);
            txtLName.MaxLength = 30;
            txtLName.Name = "txtLName";
            txtLName.Size = new System.Drawing.Size(160, 31);
            txtLName.TabIndex = 3;
            // 
            // txtFName
            // 
            txtFName.Location = new System.Drawing.Point(344, 44);
            txtFName.MaxLength = 20;
            txtFName.Name = "txtFName";
            txtFName.Size = new System.Drawing.Size(137, 31);
            txtFName.TabIndex = 5;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(62, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(24, 23);
            label1.TabIndex = 0;
            label1.Text = "ID";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKey
            // 
            lblKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            lblKey.Location = new System.Drawing.Point(92, 12);
            lblKey.Name = "lblKey";
            lblKey.Size = new System.Drawing.Size(64, 23);
            lblKey.TabIndex = 1;
            lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.Location = new System.Drawing.Point(4, 108);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(82, 22);
            label5.TabIndex = 8;
            label5.Text = "&City/St/Zip";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Location = new System.Drawing.Point(10, 76);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(76, 22);
            label4.TabIndex = 6;
            label4.Text = "&Address";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Location = new System.Drawing.Point(3, 44);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(83, 22);
            label3.TabIndex = 2;
            label3.Text = "&Last Name";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Location = new System.Drawing.Point(262, 44);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(76, 22);
            label2.TabIndex = 4;
            label2.Text = "First Name";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAddRow
            // 
            lblAddRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            lblAddRow.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            lblAddRow.Location = new System.Drawing.Point(12, 216);
            lblAddRow.Name = "lblAddRow";
            lblAddRow.Size = new System.Drawing.Size(494, 24);
            lblAddRow.TabIndex = 16;
            lblAddRow.Text = "Please click the Add button to add a new row";
            lblAddRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblAddRow.Visible = false;
            // 
            // epErrors
            // 
            epErrors.ContainerControl = this;
            // 
            // txtFindName
            // 
            txtFindName.Location = new System.Drawing.Point(126, 12);
            txtFindName.MaxLength = 30;
            txtFindName.Name = "txtFindName";
            txtFindName.Size = new System.Drawing.Size(136, 31);
            txtFindName.TabIndex = 18;
            txtFindName.TextChanged += this.txtFindName_TextChanged;
            // 
            // clickableLabel1
            // 
            clickableLabel1.Location = new System.Drawing.Point(7, 12);
            clickableLabel1.Name = "clickableLabel1";
            clickableLabel1.Size = new System.Drawing.Size(113, 23);
            clickableLabel1.TabIndex = 17;
            clickableLabel1.Text = "&Find Last Name";
            clickableLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DataNavigatorTestForm
            // 
            this.ClientSize = new System.Drawing.Size(832, 415);
            this.Controls.Add(txtFindName);
            this.Controls.Add(txtValue);
            this.Controls.Add(clickableLabel1);
            this.Controls.Add(lblAddRow);
            this.Controls.Add(pnlData);
            this.Controls.Add(cboColumns);
            this.Controls.Add(btnGetValue);
            this.Controls.Add(txtRowNumber);
            this.Controls.Add(label7);
            this.Controls.Add(label8);
            this.Controls.Add(udcRowNumber);
            this.Controls.Add(label6);
            this.Controls.Add(btnModRow);
            this.Controls.Add(btnDelDSRow);
            this.Controls.Add(btnAddDSRow);
            this.Controls.Add(pgProps);
            this.Controls.Add(btnSave);
            this.Controls.Add(dnNav);
            this.Controls.Add(btnLoad);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(840, 336);
            this.Name = "DataNavigatorTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Data Navigator Control";
            this.Closing += this.DataNavigatorTestForm_Closing;
            ((System.ComponentModel.ISupportInitialize)udcRowNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtRowNumber).EndInit();
            pnlData.ResumeLayout(false);
            pnlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cboState).EndInit();
            ((System.ComponentModel.ISupportInitialize)epErrors).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Button btnLoad;
        private EWSoftware.ListControls.DataNavigator dnNav;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PropertyGrid pgProps;
        private System.Windows.Forms.Button btnAddDSRow;
        private System.Windows.Forms.Button btnDelDSRow;
        private System.Windows.Forms.Button btnModRow;
        private System.Windows.Forms.NumericUpDown udcRowNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cboColumns;
        private System.Windows.Forms.Button btnGetValue;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.NumericUpDown txtRowNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.Label lblKey;
        private EWSoftware.ListControls.MultiColumnComboBox cboState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAddRow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider epErrors;
        private System.Windows.Forms.TextBox txtFindName;
        private EWSoftware.ListControls.ClickableLabel clickableLabel1;
    }
}
