namespace ListControlDemoCS
{
    partial class DataListTestForm
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
            dlList = new DataList();
            btnSave = new Button();
            pgProps = new PropertyGrid();
            btnAddDSRow = new Button();
            btnDelDSRow = new Button();
            btnModRow = new Button();
            toolTip1 = new ToolTip(components);
            btnGetValue = new Button();
            label1 = new Label();
            udcRowNumber = new NumericUpDown();
            cboColumns = new ComboBox();
            txtValue = new TextBox();
            txtRowNumber = new NumericUpDown();
            label6 = new Label();
            label5 = new Label();
            chkShowHeader = new CheckBox();
            chkShowFooter = new CheckBox();
            ((ISupportInitialize)udcRowNumber).BeginInit();
            ((ISupportInitialize)txtRowNumber).BeginInit();
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
            // dlList
            // 
            dlList.AllowDrop = true;
            dlList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dlList.CaptionText = "Names and Addresses";
            dlList.CaptionVisible = true;
            dlList.Location = new Point(15, 12);
            dlList.Name = "dlList";
            dlList.Size = new Size(652, 626);
            dlList.TabIndex = 0;
            dlList.ItemDataBound += this.dlList_ItemDataBound;
            dlList.BeginDrag += this.dlList_BeginDrag;
            dlList.DragDrop += this.dlList_DragDrop;
            dlList.DragEnter += this.dlList_DragEnter;
            dlList.DragOver += this.dlList_DragOver;
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
            pgProps.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pgProps.LineColor = SystemColors.ScrollBar;
            pgProps.Location = new Point(673, 12);
            pgProps.Name = "pgProps";
            pgProps.Size = new Size(321, 626);
            pgProps.TabIndex = 1;
            pgProps.PropertyValueChanged += this.pgProps_PropertyValueChanged;
            // 
            // btnAddDSRow
            // 
            btnAddDSRow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddDSRow.Location = new Point(280, 681);
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
            btnDelDSRow.Location = new Point(572, 681);
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
            btnModRow.Location = new Point(682, 681);
            btnModRow.Name = "btnModRow";
            btnModRow.Size = new Size(104, 32);
            btnModRow.TabIndex = 14;
            btnModRow.Text = "Modify Row";
            toolTip1.SetToolTip(btnModRow, "Modify row directly in data source");
            btnModRow.Click += this.btnModRow_Click;
            // 
            // btnGetValue
            // 
            btnGetValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnGetValue.Location = new Point(354, 644);
            btnGetValue.Name = "btnGetValue";
            btnGetValue.Size = new Size(75, 32);
            btnGetValue.TabIndex = 6;
            btnGetValue.Text = "&Get";
            toolTip1.SetToolTip(btnGetValue, "Get the specified column from the specified row");
            btnGetValue.Click += this.btnGetValue_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.Location = new Point(390, 685);
            label1.Name = "label1";
            label1.Size = new Size(106, 23);
            label1.TabIndex = 11;
            label1.Text = "Del/Mod Row";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // udcRowNumber
            // 
            udcRowNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            udcRowNumber.Location = new Point(502, 685);
            udcRowNumber.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            udcRowNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            udcRowNumber.Name = "udcRowNumber";
            udcRowNumber.Size = new Size(64, 27);
            udcRowNumber.TabIndex = 12;
            udcRowNumber.TextAlign = HorizontalAlignment.Right;
            udcRowNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cboColumns
            // 
            cboColumns.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cboColumns.DropDownStyle = ComboBoxStyle.DropDownList;
            cboColumns.Items.AddRange(new object[] { "ID", "FirstName", "LastName", "StreetAddress", "City", "State", "Zip", "SumValue" });
            cboColumns.Location = new Point(107, 647);
            cboColumns.Name = "cboColumns";
            cboColumns.Size = new Size(132, 28);
            cboColumns.TabIndex = 3;
            // 
            // txtValue
            // 
            txtValue.AllowDrop = true;
            txtValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtValue.Location = new Point(435, 647);
            txtValue.Name = "txtValue";
            txtValue.ReadOnly = true;
            txtValue.Size = new Size(350, 27);
            txtValue.TabIndex = 7;
            txtValue.TabStop = false;
            txtValue.DragDrop += this.txtValue_DragDrop;
            txtValue.DragEnter += this.txtValue_DragEnter;
            // 
            // txtRowNumber
            // 
            txtRowNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtRowNumber.Location = new Point(291, 647);
            txtRowNumber.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            txtRowNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtRowNumber.Name = "txtRowNumber";
            txtRowNumber.Size = new Size(56, 27);
            txtRowNumber.TabIndex = 5;
            txtRowNumber.TextAlign = HorizontalAlignment.Right;
            txtRowNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.Location = new Point(243, 647);
            label6.Name = "label6";
            label6.Size = new Size(48, 23);
            label6.TabIndex = 4;
            label6.Text = "at row";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.Location = new Point(11, 649);
            label5.Name = "label5";
            label5.Size = new Size(90, 23);
            label5.TabIndex = 2;
            label5.Text = "G&et column";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkShowHeader
            // 
            chkShowHeader.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            chkShowHeader.Checked = true;
            chkShowHeader.CheckState = CheckState.Checked;
            chkShowHeader.Location = new Point(813, 648);
            chkShowHeader.Name = "chkShowHeader";
            chkShowHeader.Size = new Size(142, 24);
            chkShowHeader.TabIndex = 15;
            chkShowHeader.Text = "Show header";
            chkShowHeader.CheckedChanged += this.chkShowHeader_CheckedChanged;
            // 
            // chkShowFooter
            // 
            chkShowFooter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            chkShowFooter.Checked = true;
            chkShowFooter.CheckState = CheckState.Checked;
            chkShowFooter.Location = new Point(813, 678);
            chkShowFooter.Name = "chkShowFooter";
            chkShowFooter.Size = new Size(142, 24);
            chkShowFooter.TabIndex = 16;
            chkShowFooter.Text = "Show footer";
            chkShowFooter.CheckedChanged += this.chkShowFooter_CheckedChanged;
            // 
            // DataListTestForm
            // 
            this.ClientSize = new Size(1006, 721);
            this.Controls.Add(chkShowFooter);
            this.Controls.Add(chkShowHeader);
            this.Controls.Add(cboColumns);
            this.Controls.Add(btnGetValue);
            this.Controls.Add(txtValue);
            this.Controls.Add(txtRowNumber);
            this.Controls.Add(label6);
            this.Controls.Add(label5);
            this.Controls.Add(udcRowNumber);
            this.Controls.Add(label1);
            this.Controls.Add(btnModRow);
            this.Controls.Add(btnDelDSRow);
            this.Controls.Add(btnAddDSRow);
            this.Controls.Add(pgProps);
            this.Controls.Add(btnSave);
            this.Controls.Add(dlList);
            this.Controls.Add(btnLoad);
            this.MinimumSize = new Size(795, 190);
            this.Name = "DataListTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = SizeGripStyle.Show;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Test Data List Control";
            this.Closing += this.DataListTestForm_Closing;
            ((ISupportInitialize)udcRowNumber).EndInit();
            ((ISupportInitialize)txtRowNumber).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
		private EWSoftware.ListControls.DataList dlList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PropertyGrid pgProps;
        private System.Windows.Forms.Button btnAddDSRow;
        private System.Windows.Forms.Button btnDelDSRow;
        private System.Windows.Forms.Button btnModRow;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udcRowNumber;
        private System.Windows.Forms.ComboBox cboColumns;
        private System.Windows.Forms.Button btnGetValue;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.NumericUpDown txtRowNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkShowHeader;
        private System.Windows.Forms.CheckBox chkShowFooter;
    }
}
