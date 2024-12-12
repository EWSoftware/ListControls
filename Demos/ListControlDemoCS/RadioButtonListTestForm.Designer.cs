namespace ListControlDemoCS
{
    partial class RadioButtonListTestForm
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
            if (disposing)
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(RadioButtonListTestForm));
            pgProps = new PropertyGrid();
            grpOptions = new GroupBox();
            cboColumns = new ComboBox();
            btnGetValue = new Button();
            udcRowNumber = new NumericUpDown();
            label6 = new Label();
            label5 = new Label();
            txtValue = new TextBox();
            cboDataSource = new ComboBox();
            label1 = new Label();
            rblDemo = new RadioButtonList();
            chkShowImages = new CheckBox();
            ilImages = new ImageList(components);
            grpOptions.SuspendLayout();
            ((ISupportInitialize)udcRowNumber).BeginInit();
            ((ISupportInitialize)rblDemo).BeginInit();
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
            grpOptions.Controls.Add(cboColumns);
            grpOptions.Controls.Add(btnGetValue);
            grpOptions.Controls.Add(udcRowNumber);
            grpOptions.Controls.Add(label6);
            grpOptions.Controls.Add(label5);
            grpOptions.Controls.Add(txtValue);
            grpOptions.Controls.Add(cboDataSource);
            grpOptions.Controls.Add(label1);
            grpOptions.Location = new Point(513, 585);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new Size(485, 120);
            grpOptions.TabIndex = 1;
            grpOptions.TabStop = false;
            grpOptions.Text = "Options";
            // 
            // cboColumns
            // 
            cboColumns.DropDownStyle = ComboBoxStyle.DropDownList;
            cboColumns.Location = new Point(104, 56);
            cboColumns.Name = "cboColumns";
            cboColumns.Size = new Size(132, 28);
            cboColumns.TabIndex = 3;
            // 
            // btnGetValue
            // 
            btnGetValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnGetValue.Location = new Point(350, 52);
            btnGetValue.Name = "btnGetValue";
            btnGetValue.Size = new Size(75, 28);
            btnGetValue.TabIndex = 6;
            btnGetValue.Text = "&Get";
            btnGetValue.Click += this.btnGetValue_Click;
            // 
            // udcRowNumber
            // 
            udcRowNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            udcRowNumber.Location = new Point(288, 56);
            udcRowNumber.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            udcRowNumber.Name = "udcRowNumber";
            udcRowNumber.Size = new Size(56, 27);
            udcRowNumber.TabIndex = 5;
            udcRowNumber.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.Location = new Point(240, 56);
            label6.Name = "label6";
            label6.Size = new Size(48, 23);
            label6.TabIndex = 4;
            label6.Text = "at row";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.Location = new Point(9, 56);
            label5.Name = "label5";
            label5.Size = new Size(89, 23);
            label5.TabIndex = 2;
            label5.Text = "G&et column";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtValue
            // 
            txtValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtValue.Location = new Point(8, 88);
            txtValue.Name = "txtValue";
            txtValue.ReadOnly = true;
            txtValue.Size = new Size(469, 27);
            txtValue.TabIndex = 7;
            txtValue.TabStop = false;
            // 
            // cboDataSource
            // 
            cboDataSource.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cboDataSource.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDataSource.Items.AddRange(new object[] { "Demo Data (List<DemoData>)", "Product Info (List<ProductInfo>)", "Array List", "Strings" });
            cboDataSource.Location = new Point(104, 24);
            cboDataSource.Name = "cboDataSource";
            cboDataSource.Size = new Size(321, 28);
            cboDataSource.TabIndex = 1;
            cboDataSource.SelectedIndexChanged += this.cboDataSource_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.Location = new Point(6, 24);
            label1.Name = "label1";
            label1.Size = new Size(92, 23);
            label1.TabIndex = 0;
            label1.Text = "&Data Source";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rblDemo
            // 
            rblDemo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rblDemo.Location = new Point(513, 8);
            rblDemo.Name = "rblDemo";
            rblDemo.Size = new Size(485, 561);
            rblDemo.TabIndex = 0;
            rblDemo.TitleFont = new Font("Segoe UI", 9F);
            rblDemo.SelectedIndexChanged += this.rblDemo_SelectedIndexChanged;
            // 
            // chkShowImages
            // 
            chkShowImages.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkShowImages.Location = new Point(8, 693);
            chkShowImages.Name = "chkShowImages";
            chkShowImages.Size = new Size(136, 24);
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
            // RadioButtonListTestForm
            // 
            this.ClientSize = new Size(1006, 721);
            this.Controls.Add(chkShowImages);
            this.Controls.Add(rblDemo);
            this.Controls.Add(grpOptions);
            this.Controls.Add(pgProps);
            this.MaximumSize = new Size(2000, 2000);
            this.MinimumSize = new Size(840, 360);
            this.Name = "RadioButtonListTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = SizeGripStyle.Show;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Radio Button List Test";
            grpOptions.ResumeLayout(false);
            grpOptions.PerformLayout();
            ((ISupportInitialize)udcRowNumber).EndInit();
            ((ISupportInitialize)rblDemo).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.PropertyGrid pgProps;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Label label1;
        private EWSoftware.ListControls.RadioButtonList rblDemo;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.CheckBox chkShowImages;
        private System.Windows.Forms.ImageList ilImages;
        private System.Windows.Forms.ComboBox cboColumns;
        private System.Windows.Forms.Button btnGetValue;
        private System.Windows.Forms.NumericUpDown udcRowNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboDataSource;
    }
}
