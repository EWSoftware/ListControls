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
                dc?.Dispose();
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
            components = new Container();
            btnLoad = new Button();
            dnNav = new DataNavigator();
            btnSave = new Button();
            pgProps = new PropertyGrid();
            btnAddDSRow = new Button();
            btnDelDSRow = new Button();
            btnModRow = new Button();
            udcRowNumber = new NumericUpDown();
            label6 = new Label();
            toolTip1 = new ToolTip(components);
            btnGetValue = new Button();
            cboColumns = new ComboBox();
            txtValue = new TextBox();
            txtRowNumber = new NumericUpDown();
            label7 = new Label();
            label8 = new Label();
            pnlData = new Panel();
            txtZip = new TextBox();
            cboState = new MultiColumnComboBox();
            txtCity = new TextBox();
            txtAddress = new TextBox();
            txtLName = new TextBox();
            txtFName = new TextBox();
            label1 = new Label();
            lblKey = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            lblAddRow = new Label();
            epErrors = new ErrorProvider(components);
            txtFindName = new TextBox();
            clickableLabel1 = new ClickableLabel();
            ((ISupportInitialize)udcRowNumber).BeginInit();
            ((ISupportInitialize)txtRowNumber).BeginInit();
            pnlData.SuspendLayout();
            ((ISupportInitialize)cboState).BeginInit();
            ((ISupportInitialize)epErrors).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLoad.Location = new Point(12, 681);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(104, 32);
            btnLoad.TabIndex = 8;
            btnLoad.Text = "L&oad Data";
            toolTip1.SetToolTip(btnLoad, "Load data into the control");
            btnLoad.Click += this.btnLoad_Click;
            // 
            // dnNav
            // 
            dnNav.Location = new Point(12, 195);
            dnNav.Name = "dnNav";
            dnNav.Size = new Size(547, 22);
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
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.Location = new Point(122, 681);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(104, 32);
            btnSave.TabIndex = 9;
            btnSave.Text = "&Save Data";
            toolTip1.SetToolTip(btnSave, "Save all changes");
            btnSave.Click += this.btnSave_Click;
            // 
            // pgProps
            // 
            pgProps.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pgProps.LineColor = SystemColors.ScrollBar;
            pgProps.Location = new Point(565, 12);
            pgProps.Name = "pgProps";
            pgProps.Size = new Size(429, 628);
            pgProps.TabIndex = 15;
            pgProps.PropertyValueChanged += this.pgProps_PropertyValueChanged;
            // 
            // btnAddDSRow
            // 
            btnAddDSRow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddDSRow.Location = new Point(284, 681);
            btnAddDSRow.Name = "btnAddDSRow";
            btnAddDSRow.Size = new Size(104, 32);
            btnAddDSRow.TabIndex = 10;
            btnAddDSRow.Text = "Add Row";
            toolTip1.SetToolTip(btnAddDSRow, "Add row directly to the data source");
            btnAddDSRow.Click += this.btnAddDSRow_Click;
            // 
            // btnDelDSRow
            // 
            btnDelDSRow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelDSRow.Location = new Point(595, 681);
            btnDelDSRow.Name = "btnDelDSRow";
            btnDelDSRow.Size = new Size(104, 32);
            btnDelDSRow.TabIndex = 13;
            btnDelDSRow.Text = "Del DS Row";
            toolTip1.SetToolTip(btnDelDSRow, "Delete row directly from data source");
            btnDelDSRow.Click += this.btnDelDSRow_Click;
            // 
            // btnModRow
            // 
            btnModRow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnModRow.Location = new Point(705, 681);
            btnModRow.Name = "btnModRow";
            btnModRow.Size = new Size(104, 32);
            btnModRow.TabIndex = 14;
            btnModRow.Text = "Modify Row";
            toolTip1.SetToolTip(btnModRow, "Modify row directly in data source");
            btnModRow.Click += this.btnModRow_Click;
            // 
            // udcRowNumber
            // 
            udcRowNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            udcRowNumber.Location = new Point(525, 683);
            udcRowNumber.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            udcRowNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            udcRowNumber.Name = "udcRowNumber";
            udcRowNumber.Size = new Size(64, 27);
            udcRowNumber.TabIndex = 12;
            udcRowNumber.TextAlign = HorizontalAlignment.Right;
            udcRowNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.Location = new Point(394, 686);
            label6.Name = "label6";
            label6.Size = new Size(125, 23);
            label6.TabIndex = 11;
            label6.Text = "Del/Mod Row";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnGetValue
            // 
            btnGetValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnGetValue.Location = new Point(377, 643);
            btnGetValue.Name = "btnGetValue";
            btnGetValue.Size = new Size(75, 32);
            btnGetValue.TabIndex = 6;
            btnGetValue.Text = "&Get";
            toolTip1.SetToolTip(btnGetValue, "Get the specified column from the specified row");
            btnGetValue.Click += this.btnGetValue_Click;
            // 
            // cboColumns
            // 
            cboColumns.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cboColumns.DropDownStyle = ComboBoxStyle.DropDownList;
            cboColumns.Items.AddRange(new object[] { "ID", "FirstName", "LastName", "StreetAddress", "City", "State", "Zip", "SumValue" });
            cboColumns.Location = new Point(122, 647);
            cboColumns.Name = "cboColumns";
            cboColumns.Size = new Size(132, 28);
            cboColumns.TabIndex = 3;
            // 
            // txtValue
            // 
            txtValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtValue.Location = new Point(458, 646);
            txtValue.Name = "txtValue";
            txtValue.ReadOnly = true;
            txtValue.Size = new Size(536, 27);
            txtValue.TabIndex = 7;
            txtValue.TabStop = false;
            // 
            // txtRowNumber
            // 
            txtRowNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtRowNumber.Location = new Point(315, 647);
            txtRowNumber.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            txtRowNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtRowNumber.Name = "txtRowNumber";
            txtRowNumber.Size = new Size(56, 27);
            txtRowNumber.TabIndex = 5;
            txtRowNumber.TextAlign = HorizontalAlignment.Right;
            txtRowNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label7.Location = new Point(261, 649);
            label7.Name = "label7";
            label7.Size = new Size(48, 23);
            label7.TabIndex = 4;
            label7.Text = "at row";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label8.Location = new Point(18, 649);
            label8.Name = "label8";
            label8.Size = new Size(98, 23);
            label8.TabIndex = 2;
            label8.Text = "G&et column";
            label8.TextAlign = ContentAlignment.MiddleRight;
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
            pnlData.Location = new Point(12, 45);
            pnlData.Name = "pnlData";
            pnlData.Size = new Size(547, 144);
            pnlData.TabIndex = 0;
            // 
            // txtZip
            // 
            txtZip.Location = new Point(347, 110);
            txtZip.MaxLength = 10;
            txtZip.Name = "txtZip";
            txtZip.Size = new Size(77, 27);
            txtZip.TabIndex = 11;
            // 
            // cboState
            // 
            cboState.DropDownStyle = ComboBoxStyle.DropDownList;
            cboState.Location = new Point(275, 110);
            cboState.MaxDropDownItems = 16;
            cboState.Name = "cboState";
            cboState.Size = new Size(66, 29);
            cboState.TabIndex = 10;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(109, 110);
            txtCity.MaxLength = 20;
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(160, 27);
            txtCity.TabIndex = 9;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(109, 77);
            txtAddress.MaxLength = 50;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(407, 27);
            txtAddress.TabIndex = 7;
            // 
            // txtLName
            // 
            txtLName.Location = new Point(109, 44);
            txtLName.MaxLength = 30;
            txtLName.Name = "txtLName";
            txtLName.Size = new Size(160, 27);
            txtLName.TabIndex = 3;
            // 
            // txtFName
            // 
            txtFName.Location = new Point(379, 44);
            txtFName.MaxLength = 20;
            txtFName.Name = "txtFName";
            txtFName.Size = new Size(137, 27);
            txtFName.TabIndex = 5;
            // 
            // label1
            // 
            label1.Location = new Point(71, 12);
            label1.Name = "label1";
            label1.Size = new Size(32, 23);
            label1.TabIndex = 0;
            label1.Text = "ID";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblKey
            // 
            lblKey.BorderStyle = BorderStyle.Fixed3D;
            lblKey.Location = new Point(109, 12);
            lblKey.Name = "lblKey";
            lblKey.Size = new Size(64, 23);
            lblKey.TabIndex = 1;
            lblKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(6, 112);
            label5.Name = "label5";
            label5.Size = new Size(97, 22);
            label5.TabIndex = 8;
            label5.Text = "&City/St/Zip";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(27, 79);
            label4.Name = "label4";
            label4.Size = new Size(76, 22);
            label4.TabIndex = 6;
            label4.Text = "&Address";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(8, 46);
            label3.Name = "label3";
            label3.Size = new Size(95, 22);
            label3.TabIndex = 2;
            label3.Text = "&Last Name";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(280, 46);
            label2.Name = "label2";
            label2.Size = new Size(93, 22);
            label2.TabIndex = 4;
            label2.Text = "First Name";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblAddRow
            // 
            lblAddRow.BackColor = SystemColors.ActiveCaption;
            lblAddRow.ForeColor = SystemColors.ActiveCaptionText;
            lblAddRow.Location = new Point(12, 220);
            lblAddRow.Name = "lblAddRow";
            lblAddRow.Size = new Size(547, 24);
            lblAddRow.TabIndex = 16;
            lblAddRow.Text = "Please click the Add button to add a new row";
            lblAddRow.TextAlign = ContentAlignment.MiddleCenter;
            lblAddRow.Visible = false;
            // 
            // epErrors
            // 
            epErrors.ContainerControl = this;
            // 
            // txtFindName
            // 
            txtFindName.Location = new Point(154, 12);
            txtFindName.MaxLength = 30;
            txtFindName.Name = "txtFindName";
            txtFindName.Size = new Size(160, 27);
            txtFindName.TabIndex = 18;
            txtFindName.TextChanged += this.txtFindName_TextChanged;
            // 
            // clickableLabel1
            // 
            clickableLabel1.Location = new Point(12, 14);
            clickableLabel1.Name = "clickableLabel1";
            clickableLabel1.Size = new Size(136, 23);
            clickableLabel1.TabIndex = 17;
            clickableLabel1.Text = "&Find Last Name";
            clickableLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DataNavigatorTestForm
            // 
            this.ClientSize = new Size(1006, 721);
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
            this.MinimumSize = new Size(850, 375);
            this.Name = "DataNavigatorTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = SizeGripStyle.Show;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Test Data Navigator Control";
            this.Closing += this.DataNavigatorTestForm_Closing;
            ((ISupportInitialize)udcRowNumber).EndInit();
            ((ISupportInitialize)txtRowNumber).EndInit();
            pnlData.ResumeLayout(false);
            pnlData.PerformLayout();
            ((ISupportInitialize)cboState).EndInit();
            ((ISupportInitialize)epErrors).EndInit();
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
