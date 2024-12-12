namespace ListControlDemoCS
{
    partial class UserControlComboTestForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(UserControlComboTestForm));
            cboAutoComp = new AutoCompleteComboBox();
            label2 = new Label();
            label3 = new Label();
            cboUCCombo = new UserControlComboBox();
            pgProps = new PropertyGrid();
            grpOptions = new GroupBox();
            cboColumns = new ComboBox();
            label6 = new Label();
            btnGetValue = new Button();
            txtValue = new TextBox();
            udcRowNumber = new NumericUpDown();
            label5 = new Label();
            cboDataSource = new ComboBox();
            label1 = new Label();
            ilImages = new ImageList(components);
            ((ISupportInitialize)cboUCCombo).BeginInit();
            grpOptions.SuspendLayout();
            ((ISupportInitialize)udcRowNumber).BeginInit();
            this.SuspendLayout();
            // 
            // cboAutoComp
            // 
            cboAutoComp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboAutoComp.Location = new Point(519, 12);
            cboAutoComp.Name = "cboAutoComp";
            cboAutoComp.Size = new Size(475, 28);
            cboAutoComp.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new Point(383, 12);
            label2.Name = "label2";
            label2.Size = new Size(130, 23);
            label2.TabIndex = 0;
            label2.Text = "&Auto-Complete";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new Point(383, 160);
            label3.Name = "label3";
            label3.Size = new Size(130, 23);
            label3.TabIndex = 2;
            label3.Text = "&User Ctl Combo";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboUCCombo
            // 
            cboUCCombo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cboUCCombo.Location = new Point(519, 160);
            cboUCCombo.Name = "cboUCCombo";
            cboUCCombo.Size = new Size(475, 29);
            cboUCCombo.TabIndex = 3;
            cboUCCombo.DropDownControlCreated += this.cboUCCombo_DropDownControlCreated;
            cboUCCombo.DrawItemImage += this.cboUCCombo_DrawItemImage;
            cboUCCombo.SelectedIndexChanged += this.cboUCCombo_SelectedIndexChanged;
            // 
            // pgProps
            // 
            pgProps.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pgProps.LineColor = SystemColors.ScrollBar;
            pgProps.Location = new Point(6, 0);
            pgProps.Name = "pgProps";
            pgProps.Size = new Size(368, 709);
            pgProps.TabIndex = 5;
            pgProps.PropertyValueChanged += this.pgProps_PropertyValueChanged;
            // 
            // grpOptions
            // 
            grpOptions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpOptions.Controls.Add(cboColumns);
            grpOptions.Controls.Add(label6);
            grpOptions.Controls.Add(btnGetValue);
            grpOptions.Controls.Add(txtValue);
            grpOptions.Controls.Add(udcRowNumber);
            grpOptions.Controls.Add(label5);
            grpOptions.Controls.Add(cboDataSource);
            grpOptions.Controls.Add(label1);
            grpOptions.Location = new Point(380, 582);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new Size(614, 127);
            grpOptions.TabIndex = 4;
            grpOptions.TabStop = false;
            grpOptions.Text = "Options";
            // 
            // cboColumns
            // 
            cboColumns.DropDownStyle = ComboBoxStyle.DropDownList;
            cboColumns.Location = new Point(135, 60);
            cboColumns.Name = "cboColumns";
            cboColumns.Size = new Size(132, 28);
            cboColumns.TabIndex = 3;
            // 
            // label6
            // 
            label6.Location = new Point(270, 60);
            label6.Name = "label6";
            label6.Size = new Size(48, 23);
            label6.TabIndex = 4;
            label6.Text = "at row";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnGetValue
            // 
            btnGetValue.Location = new Point(381, 60);
            btnGetValue.Name = "btnGetValue";
            btnGetValue.Size = new Size(75, 28);
            btnGetValue.TabIndex = 6;
            btnGetValue.Text = "&Get";
            btnGetValue.Click += this.btnGetValue_Click;
            // 
            // txtValue
            // 
            txtValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtValue.Location = new Point(16, 95);
            txtValue.Name = "txtValue";
            txtValue.ReadOnly = true;
            txtValue.Size = new Size(590, 27);
            txtValue.TabIndex = 7;
            txtValue.TabStop = false;
            // 
            // udcRowNumber
            // 
            udcRowNumber.Location = new Point(319, 60);
            udcRowNumber.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            udcRowNumber.Name = "udcRowNumber";
            udcRowNumber.Size = new Size(56, 27);
            udcRowNumber.TabIndex = 5;
            udcRowNumber.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.Location = new Point(37, 60);
            label5.Name = "label5";
            label5.Size = new Size(92, 23);
            label5.TabIndex = 2;
            label5.Text = "G&et column";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboDataSource
            // 
            cboDataSource.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDataSource.Items.AddRange(new object[] { "Demo Data (List<DemoData>)", "Product Info (List<ProductInfo>)", "Array List", "Strings" });
            cboDataSource.Location = new Point(135, 26);
            cboDataSource.Name = "cboDataSource";
            cboDataSource.Size = new Size(321, 28);
            cboDataSource.TabIndex = 1;
            cboDataSource.SelectedIndexChanged += this.cboDataSource_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Location = new Point(27, 26);
            label1.Name = "label1";
            label1.Size = new Size(102, 23);
            label1.TabIndex = 0;
            label1.Text = "&Data Source";
            label1.TextAlign = ContentAlignment.MiddleRight;
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
            // UserControlComboTestForm
            // 
            this.ClientSize = new Size(1006, 721);
            this.Controls.Add(grpOptions);
            this.Controls.Add(pgProps);
            this.Controls.Add(label3);
            this.Controls.Add(cboUCCombo);
            this.Controls.Add(label2);
            this.Controls.Add(cboAutoComp);
            this.MinimumSize = new Size(835, 512);
            this.Name = "UserControlComboTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = SizeGripStyle.Show;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "AutoComplete/User Control Combo Test";
            ((ISupportInitialize)cboUCCombo).EndInit();
            grpOptions.ResumeLayout(false);
            grpOptions.PerformLayout();
            ((ISupportInitialize)udcRowNumber).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private EWSoftware.ListControls.AutoCompleteComboBox cboAutoComp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PropertyGrid pgProps;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDataSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udcRowNumber;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnGetValue;
        private System.Windows.Forms.ComboBox cboColumns;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ImageList ilImages;
        private EWSoftware.ListControls.UserControlComboBox cboUCCombo;
    }
}
