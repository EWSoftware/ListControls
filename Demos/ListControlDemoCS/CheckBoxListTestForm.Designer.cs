namespace ListControlDemoCS
{
    partial class CheckBoxListTestForm
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
                adapter?.Dispose();
                demoData?.Dispose();
                productData?.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckBoxListTestForm));
            this.pgProps = new System.Windows.Forms.PropertyGrid();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.btnCheckedItemsText = new System.Windows.Forms.Button();
            this.btnCheckedIndices = new System.Windows.Forms.Button();
            this.btnCheckedItems = new System.Windows.Forms.Button();
            this.cboColumns = new System.Windows.Forms.ComboBox();
            this.btnGetValue = new System.Windows.Forms.Button();
            this.txtRowNumber = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cboDataSource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cblDemo = new EWSoftware.ListControls.CheckBoxList();
            this.chkShowImages = new System.Windows.Forms.CheckBox();
            this.ilImages = new System.Windows.Forms.ImageList(this.components);
            this.grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cblDemo)).BeginInit();
            this.SuspendLayout();
            // 
            // pgProps
            // 
            this.pgProps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pgProps.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pgProps.Location = new System.Drawing.Point(6, 0);
            this.pgProps.Name = "pgProps";
            this.pgProps.Size = new System.Drawing.Size(368, 444);
            this.pgProps.TabIndex = 3;
            this.pgProps.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgProps_PropertyValueChanged);
            // 
            // grpOptions
            // 
            this.grpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOptions.Controls.Add(this.btnCheckedItemsText);
            this.grpOptions.Controls.Add(this.btnCheckedIndices);
            this.grpOptions.Controls.Add(this.btnCheckedItems);
            this.grpOptions.Controls.Add(this.cboColumns);
            this.grpOptions.Controls.Add(this.btnGetValue);
            this.grpOptions.Controls.Add(this.txtRowNumber);
            this.grpOptions.Controls.Add(this.label6);
            this.grpOptions.Controls.Add(this.label5);
            this.grpOptions.Controls.Add(this.txtValue);
            this.grpOptions.Controls.Add(this.cboDataSource);
            this.grpOptions.Controls.Add(this.label1);
            this.grpOptions.Location = new System.Drawing.Point(384, 336);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(440, 120);
            this.grpOptions.TabIndex = 1;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // btnCheckedItemsText
            // 
            this.btnCheckedItemsText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckedItemsText.Location = new System.Drawing.Point(287, 21);
            this.btnCheckedItemsText.Name = "btnCheckedItemsText";
            this.btnCheckedItemsText.Size = new System.Drawing.Size(70, 28);
            this.btnCheckedItemsText.TabIndex = 3;
            this.btnCheckedItemsText.Text = "DispT&xt";
            this.btnCheckedItemsText.Click += new System.EventHandler(this.btnCheckedItemsText_Click);
            // 
            // btnCheckedIndices
            // 
            this.btnCheckedIndices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckedIndices.Location = new System.Drawing.Point(362, 21);
            this.btnCheckedIndices.Name = "btnCheckedIndices";
            this.btnCheckedIndices.Size = new System.Drawing.Size(70, 28);
            this.btnCheckedIndices.TabIndex = 4;
            this.btnCheckedIndices.Text = "I&ndices";
            this.btnCheckedIndices.Click += new System.EventHandler(this.btnCheckedIndices_Click);
            // 
            // btnCheckedItems
            // 
            this.btnCheckedItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckedItems.Location = new System.Drawing.Point(211, 21);
            this.btnCheckedItems.Name = "btnCheckedItems";
            this.btnCheckedItems.Size = new System.Drawing.Size(70, 28);
            this.btnCheckedItems.TabIndex = 2;
            this.btnCheckedItems.Text = "I&tems";
            this.btnCheckedItems.Click += new System.EventHandler(this.btnCheckedItems_Click);
            // 
            // cboColumns
            // 
            this.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumns.Location = new System.Drawing.Point(104, 56);
            this.cboColumns.Name = "cboColumns";
            this.cboColumns.Size = new System.Drawing.Size(132, 28);
            this.cboColumns.TabIndex = 6;
            // 
            // btnGetValue
            // 
            this.btnGetValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetValue.Location = new System.Drawing.Point(362, 53);
            this.btnGetValue.Name = "btnGetValue";
            this.btnGetValue.Size = new System.Drawing.Size(70, 28);
            this.btnGetValue.TabIndex = 9;
            this.btnGetValue.Text = "&Get";
            this.btnGetValue.Click += new System.EventHandler(this.btnGetValue_Click);
            // 
            // txtRowNumber
            // 
            this.txtRowNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRowNumber.Location = new System.Drawing.Point(288, 56);
            this.txtRowNumber.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txtRowNumber.Name = "txtRowNumber";
            this.txtRowNumber.Size = new System.Drawing.Size(56, 26);
            this.txtRowNumber.TabIndex = 8;
            this.txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(240, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 23);
            this.label6.TabIndex = 7;
            this.label6.Text = "at row";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(9, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "G&et column";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(8, 88);
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.Size = new System.Drawing.Size(424, 26);
            this.txtValue.TabIndex = 10;
            this.txtValue.TabStop = false;
            // 
            // cboDataSource
            // 
            this.cboDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataSource.Items.AddRange(new object[] {
            "Data Table",
            "Data View",
            "Data Set",
            "Array List",
            "Strings"});
            this.cboDataSource.Location = new System.Drawing.Point(104, 24);
            this.cboDataSource.Name = "cboDataSource";
            this.cboDataSource.Size = new System.Drawing.Size(101, 28);
            this.cboDataSource.TabIndex = 1;
            this.cboDataSource.SelectedIndexChanged += new System.EventHandler(this.cboDataSource_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Data Source";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cblDemo
            // 
            this.cblDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cblDemo.BindingMembersBindingContext = null;
            this.cblDemo.Location = new System.Drawing.Point(384, 8);
            this.cblDemo.Name = "cblDemo";
            this.cblDemo.Size = new System.Drawing.Size(440, 320);
            this.cblDemo.TabIndex = 0;
            this.cblDemo.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cblDemo.ItemCheckStateChanged += new System.EventHandler<EWSoftware.ListControls.ItemCheckStateEventArgs>(this.cblDemo_ItemCheckStateChanged);
            this.cblDemo.SelectedIndexChanged += new System.EventHandler(this.cblDemo_SelectedIndexChanged);
            // 
            // chkShowImages
            // 
            this.chkShowImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowImages.Location = new System.Drawing.Point(8, 444);
            this.chkShowImages.Name = "chkShowImages";
            this.chkShowImages.Size = new System.Drawing.Size(171, 24);
            this.chkShowImages.TabIndex = 2;
            this.chkShowImages.Text = "Use &Image List";
            this.chkShowImages.CheckedChanged += new System.EventHandler(this.chkShowImages_CheckedChanged);
            // 
            // ilImages
            // 
            this.ilImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilImages.ImageStream")));
            this.ilImages.TransparentColor = System.Drawing.Color.Magenta;
            this.ilImages.Images.SetKeyName(0, "Audio.bmp");
            this.ilImages.Images.SetKeyName(1, "Bitmap.bmp");
            this.ilImages.Images.SetKeyName(2, "Disk.bmp");
            this.ilImages.Images.SetKeyName(3, "Folder.bmp");
            this.ilImages.Images.SetKeyName(4, "Waste.bmp");
            // 
            // CheckBoxListTestForm
            // 
            this.ClientSize = new System.Drawing.Size(832, 472);
            this.Controls.Add(this.chkShowImages);
            this.Controls.Add(this.cblDemo);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.pgProps);
            this.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.MinimumSize = new System.Drawing.Size(840, 360);
            this.Name = "CheckBoxListTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkbox List Test";
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cblDemo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgProps;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Label label1;
        private EWSoftware.ListControls.CheckBoxList cblDemo;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.CheckBox chkShowImages;
        private System.Windows.Forms.ImageList ilImages;
        private System.Windows.Forms.ComboBox cboColumns;
        private System.Windows.Forms.Button btnGetValue;
        private System.Windows.Forms.NumericUpDown txtRowNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCheckedItems;
        private System.Windows.Forms.Button btnCheckedIndices;
        private System.Windows.Forms.Button btnCheckedItemsText;
        private System.Windows.Forms.ComboBox cboDataSource;
    }
}
