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
                if(dbConn != null)
                    dbConn.Dispose();

                if(components != null)
                    components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dnNav = new EWSoftware.ListControls.DataNavigator();
            this.btnSave = new System.Windows.Forms.Button();
            this.pgProps = new System.Windows.Forms.PropertyGrid();
            this.btnAddDSRow = new System.Windows.Forms.Button();
            this.btnDelDSRow = new System.Windows.Forms.Button();
            this.btnModRow = new System.Windows.Forms.Button();
            this.udcRowNumber = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGetValue = new System.Windows.Forms.Button();
            this.cboColumns = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtRowNumber = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlData = new System.Windows.Forms.Panel();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.cboState = new EWSoftware.ListControls.MultiColumnComboBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAddRow = new System.Windows.Forms.Label();
            this.epErrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtFindName = new System.Windows.Forms.TextBox();
            this.clickableLabel1 = new EWSoftware.ListControls.ClickableLabel();
            ((System.ComponentModel.ISupportInitialize)(this.udcRowNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNumber)).BeginInit();
            this.pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point(12, 375);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(104, 28);
            this.btnLoad.TabIndex = 8;
            this.btnLoad.Text = "L&oad Data";
            this.toolTip1.SetToolTip(this.btnLoad, "Load data into the control");
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dnNav
            // 
            this.dnNav.Location = new System.Drawing.Point(12, 184);
            this.dnNav.Name = "dnNav";
            this.dnNav.Size = new System.Drawing.Size(282, 22);
            this.dnNav.TabIndex = 1;
            this.dnNav.AddedRow += new System.EventHandler<EWSoftware.ListControls.DataNavigatorEventArgs>(this.dnNav_AddedRow);
            this.dnNav.DeletingRow += new System.EventHandler<EWSoftware.ListControls.DataNavigatorCancelEventArgs>(this.dnNav_DeletingRow);
            this.dnNav.CanceledEdits += new System.EventHandler<EWSoftware.ListControls.DataNavigatorEventArgs>(this.dnNav_CanceledEdits);
            this.dnNav.NoRows += new System.EventHandler(this.dnNav_NoRows);
            this.dnNav.ChangePolicyModified += new System.EventHandler<EWSoftware.ListControls.ChangePolicyEventArgs>(this.dnNav_ChangePolicyModified);
            this.dnNav.Validating += new System.ComponentModel.CancelEventHandler(this.dnNav_Validating);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(122, 375);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 28);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "&Save Data";
            this.toolTip1.SetToolTip(this.btnSave, "Save all changes");
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pgProps
            // 
            this.pgProps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgProps.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pgProps.Location = new System.Drawing.Point(512, 12);
            this.pgProps.Name = "pgProps";
            this.pgProps.Size = new System.Drawing.Size(308, 327);
            this.pgProps.TabIndex = 15;
            this.pgProps.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgProps_PropertyValueChanged);
            // 
            // btnAddDSRow
            // 
            this.btnAddDSRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddDSRow.Location = new System.Drawing.Point(284, 375);
            this.btnAddDSRow.Name = "btnAddDSRow";
            this.btnAddDSRow.Size = new System.Drawing.Size(104, 28);
            this.btnAddDSRow.TabIndex = 10;
            this.btnAddDSRow.Text = "Add DS Row";
            this.toolTip1.SetToolTip(this.btnAddDSRow, "Add row directly to the data source");
            this.btnAddDSRow.Click += new System.EventHandler(this.btnAddDSRow_Click);
            // 
            // btnDelDSRow
            // 
            this.btnDelDSRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelDSRow.Location = new System.Drawing.Point(565, 375);
            this.btnDelDSRow.Name = "btnDelDSRow";
            this.btnDelDSRow.Size = new System.Drawing.Size(104, 28);
            this.btnDelDSRow.TabIndex = 13;
            this.btnDelDSRow.Text = "Del DS Row";
            this.toolTip1.SetToolTip(this.btnDelDSRow, "Delete row directly from data source");
            this.btnDelDSRow.Click += new System.EventHandler(this.btnDelDSRow_Click);
            // 
            // btnModRow
            // 
            this.btnModRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnModRow.Location = new System.Drawing.Point(675, 375);
            this.btnModRow.Name = "btnModRow";
            this.btnModRow.Size = new System.Drawing.Size(104, 28);
            this.btnModRow.TabIndex = 14;
            this.btnModRow.Text = "Modify Row";
            this.toolTip1.SetToolTip(this.btnModRow, "Modify row directly in data source");
            this.btnModRow.Click += new System.EventHandler(this.btnModRow_Click);
            // 
            // udcRowNumber
            // 
            this.udcRowNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udcRowNumber.Location = new System.Drawing.Point(495, 377);
            this.udcRowNumber.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.udcRowNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udcRowNumber.Name = "udcRowNumber";
            this.udcRowNumber.Size = new System.Drawing.Size(64, 26);
            this.udcRowNumber.TabIndex = 12;
            this.udcRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udcRowNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(390, 377);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 23);
            this.label6.TabIndex = 11;
            this.label6.Text = "Del/Mod Row";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGetValue
            // 
            this.btnGetValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetValue.Location = new System.Drawing.Point(346, 341);
            this.btnGetValue.Name = "btnGetValue";
            this.btnGetValue.Size = new System.Drawing.Size(75, 28);
            this.btnGetValue.TabIndex = 6;
            this.btnGetValue.Text = "&Get";
            this.toolTip1.SetToolTip(this.btnGetValue, "Get the specified column from the specified row");
            this.btnGetValue.Click += new System.EventHandler(this.btnGetValue_Click);
            // 
            // cboColumns
            // 
            this.cboColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumns.Items.AddRange(new object[] {
            "ID",
            "FirstName",
            "LastName",
            "Address",
            "City",
            "State",
            "Zip",
            "SumValue"});
            this.cboColumns.Location = new System.Drawing.Point(100, 345);
            this.cboColumns.Name = "cboColumns";
            this.cboColumns.Size = new System.Drawing.Size(132, 28);
            this.cboColumns.TabIndex = 3;
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(427, 345);
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.Size = new System.Drawing.Size(352, 26);
            this.txtValue.TabIndex = 7;
            this.txtValue.TabStop = false;
            // 
            // txtRowNumber
            // 
            this.txtRowNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRowNumber.Location = new System.Drawing.Point(284, 345);
            this.txtRowNumber.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtRowNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtRowNumber.Name = "txtRowNumber";
            this.txtRowNumber.Size = new System.Drawing.Size(56, 26);
            this.txtRowNumber.TabIndex = 5;
            this.txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRowNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Location = new System.Drawing.Point(236, 345);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 23);
            this.label7.TabIndex = 4;
            this.label7.Text = "at row";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.Location = new System.Drawing.Point(16, 344);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 23);
            this.label8.TabIndex = 2;
            this.label8.Text = "G&et column";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.txtZip);
            this.pnlData.Controls.Add(this.cboState);
            this.pnlData.Controls.Add(this.txtCity);
            this.pnlData.Controls.Add(this.txtAddress);
            this.pnlData.Controls.Add(this.txtLName);
            this.pnlData.Controls.Add(this.txtFName);
            this.pnlData.Controls.Add(this.label1);
            this.pnlData.Controls.Add(this.lblKey);
            this.pnlData.Controls.Add(this.label5);
            this.pnlData.Controls.Add(this.label4);
            this.pnlData.Controls.Add(this.label3);
            this.pnlData.Controls.Add(this.label2);
            this.pnlData.Location = new System.Drawing.Point(12, 40);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(494, 144);
            this.pnlData.TabIndex = 0;
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(296, 108);
            this.txtZip.MaxLength = 10;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(77, 26);
            this.txtZip.TabIndex = 11;
            // 
            // cboState
            // 
            this.cboState.DropDownFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboState.Location = new System.Drawing.Point(228, 108);
            this.cboState.MaxDropDownItems = 16;
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(57, 28);
            this.cboState.TabIndex = 10;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(92, 108);
            this.txtCity.MaxLength = 20;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(125, 26);
            this.txtCity.TabIndex = 9;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(92, 76);
            this.txtAddress.MaxLength = 50;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(389, 26);
            this.txtAddress.TabIndex = 7;
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(92, 44);
            this.txtLName.MaxLength = 30;
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(160, 26);
            this.txtLName.TabIndex = 3;
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(344, 44);
            this.txtFName.MaxLength = 20;
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(137, 26);
            this.txtFName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(62, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKey
            // 
            this.lblKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblKey.Location = new System.Drawing.Point(92, 12);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(64, 23);
            this.lblKey.TabIndex = 1;
            this.lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(4, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "&City/St/Zip";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(10, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "&Address";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "&Last Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(262, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "First Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAddRow
            // 
            this.lblAddRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblAddRow.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblAddRow.Location = new System.Drawing.Point(12, 216);
            this.lblAddRow.Name = "lblAddRow";
            this.lblAddRow.Size = new System.Drawing.Size(494, 24);
            this.lblAddRow.TabIndex = 16;
            this.lblAddRow.Text = "Please click the Add button to add a new row";
            this.lblAddRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAddRow.Visible = false;
            // 
            // epErrors
            // 
            this.epErrors.ContainerControl = this;
            // 
            // txtFindName
            // 
            this.txtFindName.Location = new System.Drawing.Point(126, 12);
            this.txtFindName.MaxLength = 30;
            this.txtFindName.Name = "txtFindName";
            this.txtFindName.Size = new System.Drawing.Size(136, 26);
            this.txtFindName.TabIndex = 18;
            this.txtFindName.TextChanged += new System.EventHandler(this.txtFindName_TextChanged);
            // 
            // clickableLabel1
            // 
            this.clickableLabel1.Location = new System.Drawing.Point(7, 12);
            this.clickableLabel1.Name = "clickableLabel1";
            this.clickableLabel1.Size = new System.Drawing.Size(113, 23);
            this.clickableLabel1.TabIndex = 17;
            this.clickableLabel1.Text = "&Find Last Name";
            this.clickableLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DataNavigatorTestForm
            // 
            this.ClientSize = new System.Drawing.Size(832, 415);
            this.Controls.Add(this.txtFindName);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.clickableLabel1);
            this.Controls.Add(this.lblAddRow);
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.cboColumns);
            this.Controls.Add(this.btnGetValue);
            this.Controls.Add(this.txtRowNumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.udcRowNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnModRow);
            this.Controls.Add(this.btnDelDSRow);
            this.Controls.Add(this.btnAddDSRow);
            this.Controls.Add(this.pgProps);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dnNav);
            this.Controls.Add(this.btnLoad);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(840, 336);
            this.Name = "DataNavigatorTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Data Navigator Control";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.DataNavigatorTestForm_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.udcRowNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNumber)).EndInit();
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).EndInit();
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
