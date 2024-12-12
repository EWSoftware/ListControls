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
                components?.Dispose();

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(CheckBoxListTestForm));
            pgProps = new PropertyGrid();
            grpOptions = new GroupBox();
            btnCheckedItemsText = new Button();
            btnCheckedIndices = new Button();
            btnCheckedItems = new Button();
            cboColumns = new ComboBox();
            btnGetValue = new Button();
            udcRowNumber = new NumericUpDown();
            label6 = new Label();
            label5 = new Label();
            txtValue = new TextBox();
            cboDataSource = new ComboBox();
            label1 = new Label();
            cblDemo = new CheckBoxList();
            chkShowImages = new CheckBox();
            ilImages = new ImageList(components);
            grpOptions.SuspendLayout();
            ((ISupportInitialize)udcRowNumber).BeginInit();
            ((ISupportInitialize)cblDemo).BeginInit();
            this.SuspendLayout();
            // 
            // pgProps
            // 
            pgProps.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pgProps.LineColor = SystemColors.ScrollBar;
            pgProps.Location = new Point(6, 0);
            pgProps.Name = "pgProps";
            pgProps.Size = new Size(501, 693);
            pgProps.TabIndex = 3;
            pgProps.PropertyValueChanged += this.pgProps_PropertyValueChanged;
            // 
            // grpOptions
            // 
            grpOptions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpOptions.Controls.Add(btnCheckedItemsText);
            grpOptions.Controls.Add(btnCheckedIndices);
            grpOptions.Controls.Add(btnCheckedItems);
            grpOptions.Controls.Add(cboColumns);
            grpOptions.Controls.Add(btnGetValue);
            grpOptions.Controls.Add(udcRowNumber);
            grpOptions.Controls.Add(label6);
            grpOptions.Controls.Add(label5);
            grpOptions.Controls.Add(txtValue);
            grpOptions.Controls.Add(cboDataSource);
            grpOptions.Controls.Add(label1);
            grpOptions.Location = new Point(513, 547);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new Size(485, 158);
            grpOptions.TabIndex = 1;
            grpOptions.TabStop = false;
            grpOptions.Text = "Options";
            // 
            // btnCheckedItemsText
            // 
            btnCheckedItemsText.Location = new Point(180, 58);
            btnCheckedItemsText.Name = "btnCheckedItemsText";
            btnCheckedItemsText.Size = new Size(70, 28);
            btnCheckedItemsText.TabIndex = 3;
            btnCheckedItemsText.Text = "DispT&xt";
            btnCheckedItemsText.Click += this.btnCheckedItemsText_Click;
            // 
            // btnCheckedIndices
            // 
            btnCheckedIndices.Location = new Point(255, 58);
            btnCheckedIndices.Name = "btnCheckedIndices";
            btnCheckedIndices.Size = new Size(70, 28);
            btnCheckedIndices.TabIndex = 4;
            btnCheckedIndices.Text = "I&ndices";
            btnCheckedIndices.Click += this.btnCheckedIndices_Click;
            // 
            // btnCheckedItems
            // 
            btnCheckedItems.Location = new Point(104, 58);
            btnCheckedItems.Name = "btnCheckedItems";
            btnCheckedItems.Size = new Size(70, 28);
            btnCheckedItems.TabIndex = 2;
            btnCheckedItems.Text = "I&tems";
            btnCheckedItems.Click += this.btnCheckedItems_Click;
            // 
            // cboColumns
            // 
            cboColumns.DropDownStyle = ComboBoxStyle.DropDownList;
            cboColumns.Location = new Point(104, 92);
            cboColumns.Name = "cboColumns";
            cboColumns.Size = new Size(132, 28);
            cboColumns.TabIndex = 6;
            // 
            // btnGetValue
            // 
            btnGetValue.Location = new Point(362, 89);
            btnGetValue.Name = "btnGetValue";
            btnGetValue.Size = new Size(70, 28);
            btnGetValue.TabIndex = 9;
            btnGetValue.Text = "&Get";
            btnGetValue.Click += this.btnGetValue_Click;
            // 
            // udcRowNumber
            // 
            udcRowNumber.Location = new Point(288, 92);
            udcRowNumber.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            udcRowNumber.Name = "udcRowNumber";
            udcRowNumber.Size = new Size(56, 27);
            udcRowNumber.TabIndex = 8;
            udcRowNumber.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.Location = new Point(240, 92);
            label6.Name = "label6";
            label6.Size = new Size(48, 23);
            label6.TabIndex = 7;
            label6.Text = "at row";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Location = new Point(9, 92);
            label5.Name = "label5";
            label5.Size = new Size(89, 23);
            label5.TabIndex = 5;
            label5.Text = "G&et column";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtValue
            // 
            txtValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtValue.Location = new Point(8, 126);
            txtValue.Name = "txtValue";
            txtValue.ReadOnly = true;
            txtValue.Size = new Size(469, 27);
            txtValue.TabIndex = 10;
            txtValue.TabStop = false;
            // 
            // cboDataSource
            // 
            cboDataSource.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDataSource.Items.AddRange(new object[] { "Demo Data (List<DemoData>)", "Product Info (List<ProductInfo>)", "Array List", "Strings" });
            cboDataSource.Location = new Point(104, 24);
            cboDataSource.Name = "cboDataSource";
            cboDataSource.Size = new Size(328, 28);
            cboDataSource.TabIndex = 1;
            cboDataSource.SelectedIndexChanged += this.cboDataSource_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Location = new Point(6, 24);
            label1.Name = "label1";
            label1.Size = new Size(92, 23);
            label1.TabIndex = 0;
            label1.Text = "&Data Source";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cblDemo
            // 
            cblDemo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cblDemo.BindingMembersBindingContext = null;
            cblDemo.Location = new Point(513, 8);
            cblDemo.Name = "cblDemo";
            cblDemo.Size = new Size(485, 533);
            cblDemo.TabIndex = 0;
            cblDemo.TitleFont = new Font("Microsoft Sans Serif", 8F);
            cblDemo.ItemCheckStateChanged += this.cblDemo_ItemCheckStateChanged;
            cblDemo.SelectedIndexChanged += this.cblDemo_SelectedIndexChanged;
            // 
            // chkShowImages
            // 
            chkShowImages.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkShowImages.Location = new Point(8, 693);
            chkShowImages.Name = "chkShowImages";
            chkShowImages.Size = new Size(171, 24);
            chkShowImages.TabIndex = 2;
            chkShowImages.Text = "Use &Image List";
            chkShowImages.CheckedChanged += this.chkShowImages_CheckedChanged;
            // 
            // ilImages
            // 
            ilImages.ColorDepth = ColorDepth.Depth32Bit;
            ilImages.ImageStream = (ImageListStreamer)resources.GetObject("ilImages.ImageStream");
            ilImages.TransparentColor = Color.Magenta;
            ilImages.Images.SetKeyName(0, "Audio.bmp");
            ilImages.Images.SetKeyName(1, "Bitmap.bmp");
            ilImages.Images.SetKeyName(2, "Disk.bmp");
            ilImages.Images.SetKeyName(3, "Folder.bmp");
            ilImages.Images.SetKeyName(4, "Waste.bmp");
            // 
            // CheckBoxListTestForm
            // 
            this.ClientSize = new Size(1006, 721);
            this.Controls.Add(chkShowImages);
            this.Controls.Add(cblDemo);
            this.Controls.Add(grpOptions);
            this.Controls.Add(pgProps);
            this.MaximumSize = new Size(2000, 2000);
            this.MinimumSize = new Size(840, 360);
            this.Name = "CheckBoxListTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = SizeGripStyle.Show;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Checkbox List Test";
            grpOptions.ResumeLayout(false);
            grpOptions.PerformLayout();
            ((ISupportInitialize)udcRowNumber).EndInit();
            ((ISupportInitialize)cblDemo).EndInit();
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
        private System.Windows.Forms.NumericUpDown udcRowNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCheckedItems;
        private System.Windows.Forms.Button btnCheckedIndices;
        private System.Windows.Forms.Button btnCheckedItemsText;
        private System.Windows.Forms.ComboBox cboDataSource;
    }
}
