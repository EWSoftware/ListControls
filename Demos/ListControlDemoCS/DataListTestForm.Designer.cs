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
            this.dlList = new EWSoftware.ListControls.DataList();
            this.btnSave = new System.Windows.Forms.Button();
            this.pgProps = new System.Windows.Forms.PropertyGrid();
            this.btnAddDSRow = new System.Windows.Forms.Button();
            this.btnDelDSRow = new System.Windows.Forms.Button();
            this.btnModRow = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGetValue = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.udcRowNumber = new System.Windows.Forms.NumericUpDown();
            this.cboColumns = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtRowNumber = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkShowHeader = new System.Windows.Forms.CheckBox();
            this.chkShowFooter = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.udcRowNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point(12, 445);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(104, 28);
            this.btnLoad.TabIndex = 8;
            this.btnLoad.Text = "L&oad Data";
            this.toolTip1.SetToolTip(this.btnLoad, "Load data into the control");
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dlList
            // 
            this.dlList.AllowDrop = true;
            this.dlList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dlList.CaptionText = "Names and Addresses";
            this.dlList.CaptionVisible = true;
            this.dlList.Location = new System.Drawing.Point(15, 12);
            this.dlList.Name = "dlList";
            this.dlList.Size = new System.Drawing.Size(590, 390);
            this.dlList.TabIndex = 0;
            this.dlList.DragDrop += new System.Windows.Forms.DragEventHandler(this.dlList_DragDrop);
            this.dlList.DragEnter += new System.Windows.Forms.DragEventHandler(this.dlList_DragEnter);
            this.dlList.BeginDrag += new System.EventHandler<EWSoftware.ListControls.DataListBeginDragEventArgs>(this.dlList_BeginDrag);
            this.dlList.DragOver += new System.Windows.Forms.DragEventHandler(this.dlList_DragOver);
            this.dlList.ItemDataBound += new System.EventHandler<EWSoftware.ListControls.DataListEventArgs>(this.dlList_ItemDataBound);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(122, 445);
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
            this.pgProps.Location = new System.Drawing.Point(611, 12);
            this.pgProps.Name = "pgProps";
            this.pgProps.Size = new System.Drawing.Size(321, 390);
            this.pgProps.TabIndex = 1;
            this.pgProps.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgProps_PropertyValueChanged);
            // 
            // btnAddDSRow
            // 
            this.btnAddDSRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddDSRow.Location = new System.Drawing.Point(280, 445);
            this.btnAddDSRow.Name = "btnAddDSRow";
            this.btnAddDSRow.Size = new System.Drawing.Size(104, 28);
            this.btnAddDSRow.TabIndex = 10;
            this.btnAddDSRow.Text = "Add Row";
            this.toolTip1.SetToolTip(this.btnAddDSRow, "Add row directly to the data source");
            this.btnAddDSRow.Click += new System.EventHandler(this.btnAddDSRow_Click);
            // 
            // btnDelDSRow
            // 
            this.btnDelDSRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelDSRow.Location = new System.Drawing.Point(572, 445);
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
            this.btnModRow.Location = new System.Drawing.Point(682, 445);
            this.btnModRow.Name = "btnModRow";
            this.btnModRow.Size = new System.Drawing.Size(104, 28);
            this.btnModRow.TabIndex = 14;
            this.btnModRow.Text = "Modify Row";
            this.toolTip1.SetToolTip(this.btnModRow, "Modify row directly in data source");
            this.btnModRow.Click += new System.EventHandler(this.btnModRow_Click);
            // 
            // btnGetValue
            // 
            this.btnGetValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetValue.Location = new System.Drawing.Point(343, 408);
            this.btnGetValue.Name = "btnGetValue";
            this.btnGetValue.Size = new System.Drawing.Size(75, 28);
            this.btnGetValue.TabIndex = 6;
            this.btnGetValue.Text = "&Get";
            this.toolTip1.SetToolTip(this.btnGetValue, "Get the specified column from the specified row");
            this.btnGetValue.Click += new System.EventHandler(this.btnGetValue_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(390, 449);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Del/Mod Row";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // udcRowNumber
            // 
            this.udcRowNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udcRowNumber.Location = new System.Drawing.Point(502, 449);
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
            this.udcRowNumber.Size = new System.Drawing.Size(64, 22);
            this.udcRowNumber.TabIndex = 12;
            this.udcRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udcRowNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            this.cboColumns.Location = new System.Drawing.Point(96, 411);
            this.cboColumns.Name = "cboColumns";
            this.cboColumns.Size = new System.Drawing.Size(132, 24);
            this.cboColumns.TabIndex = 3;
            // 
            // txtValue
            // 
            this.txtValue.AllowDrop = true;
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(424, 411);
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.Size = new System.Drawing.Size(288, 22);
            this.txtValue.TabIndex = 7;
            this.txtValue.TabStop = false;
            this.txtValue.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtValue_DragDrop);
            this.txtValue.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtValue_DragEnter);
            // 
            // txtRowNumber
            // 
            this.txtRowNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRowNumber.Location = new System.Drawing.Point(280, 411);
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
            this.txtRowNumber.Size = new System.Drawing.Size(56, 22);
            this.txtRowNumber.TabIndex = 5;
            this.txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRowNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(232, 411);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 23);
            this.label6.TabIndex = 4;
            this.label6.Text = "at row";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(12, 411);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "G&et column";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkShowHeader
            // 
            this.chkShowHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowHeader.Checked = true;
            this.chkShowHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowHeader.Location = new System.Drawing.Point(720, 411);
            this.chkShowHeader.Name = "chkShowHeader";
            this.chkShowHeader.Size = new System.Drawing.Size(104, 24);
            this.chkShowHeader.TabIndex = 15;
            this.chkShowHeader.Text = "Show header";
            this.chkShowHeader.CheckedChanged += new System.EventHandler(this.chkShowHeader_CheckedChanged);
            // 
            // chkShowFooter
            // 
            this.chkShowFooter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowFooter.Checked = true;
            this.chkShowFooter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowFooter.Location = new System.Drawing.Point(832, 411);
            this.chkShowFooter.Name = "chkShowFooter";
            this.chkShowFooter.Size = new System.Drawing.Size(104, 24);
            this.chkShowFooter.TabIndex = 16;
            this.chkShowFooter.Text = "Show footer";
            this.chkShowFooter.CheckedChanged += new System.EventHandler(this.chkShowFooter_CheckedChanged);
            // 
            // DataListTestForm
            // 
            this.ClientSize = new System.Drawing.Size(944, 485);
            this.Controls.Add(this.chkShowFooter);
            this.Controls.Add(this.chkShowHeader);
            this.Controls.Add(this.cboColumns);
            this.Controls.Add(this.btnGetValue);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtRowNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.udcRowNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnModRow);
            this.Controls.Add(this.btnDelDSRow);
            this.Controls.Add(this.btnAddDSRow);
            this.Controls.Add(this.pgProps);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dlList);
            this.Controls.Add(this.btnLoad);
            this.MinimumSize = new System.Drawing.Size(795, 190);
            this.Name = "DataListTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Data List Control";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.DataListTestForm_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.udcRowNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNumber)).EndInit();
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
