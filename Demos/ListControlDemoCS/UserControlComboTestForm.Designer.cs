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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlComboTestForm));
            this.cboAutoComp = new EWSoftware.ListControls.AutoCompleteComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboUCCombo = new EWSoftware.ListControls.UserControlComboBox();
            this.pgProps = new System.Windows.Forms.PropertyGrid();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.cboColumns = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGetValue = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtRowNumber = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cboDataSource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ilImages = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cboUCCombo)).BeginInit();
            this.grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // cboAutoComp
            // 
            this.cboAutoComp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAutoComp.Location = new System.Drawing.Point(496, 16);
            this.cboAutoComp.Name = "cboAutoComp";
            this.cboAutoComp.Size = new System.Drawing.Size(323, 24);
            this.cboAutoComp.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(379, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "&Auto-Complete";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(376, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "&User Ctl Combo";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboUCCombo
            // 
            this.cboUCCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUCCombo.Location = new System.Drawing.Point(496, 184);
            this.cboUCCombo.Name = "cboUCCombo";
            this.cboUCCombo.Size = new System.Drawing.Size(323, 24);
            this.cboUCCombo.TabIndex = 3;
            this.cboUCCombo.DropDownControlCreated += new System.EventHandler(this.cboUCCombo_DropDownControlCreated);
            this.cboUCCombo.SelectedIndexChanged += new System.EventHandler(this.cboUCCombo_SelectedIndexChanged);
            this.cboUCCombo.DrawItemImage += new System.Windows.Forms.DrawItemEventHandler(this.cboUCCombo_DrawItemImage);
            // 
            // pgProps
            // 
            this.pgProps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pgProps.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pgProps.Location = new System.Drawing.Point(6, 0);
            this.pgProps.Name = "pgProps";
            this.pgProps.Size = new System.Drawing.Size(368, 467);
            this.pgProps.TabIndex = 5;
            this.pgProps.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgProps_PropertyValueChanged);
            // 
            // grpOptions
            // 
            this.grpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOptions.Controls.Add(this.cboColumns);
            this.grpOptions.Controls.Add(this.label6);
            this.grpOptions.Controls.Add(this.btnGetValue);
            this.grpOptions.Controls.Add(this.txtValue);
            this.grpOptions.Controls.Add(this.txtRowNumber);
            this.grpOptions.Controls.Add(this.label5);
            this.grpOptions.Controls.Add(this.cboDataSource);
            this.grpOptions.Controls.Add(this.label1);
            this.grpOptions.Location = new System.Drawing.Point(384, 344);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(435, 120);
            this.grpOptions.TabIndex = 4;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // cboColumns
            // 
            this.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumns.Location = new System.Drawing.Point(104, 59);
            this.cboColumns.Name = "cboColumns";
            this.cboColumns.Size = new System.Drawing.Size(132, 24);
            this.cboColumns.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(239, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 23);
            this.label6.TabIndex = 4;
            this.label6.Text = "at row";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGetValue
            // 
            this.btnGetValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetValue.Location = new System.Drawing.Point(350, 55);
            this.btnGetValue.Name = "btnGetValue";
            this.btnGetValue.Size = new System.Drawing.Size(75, 28);
            this.btnGetValue.TabIndex = 6;
            this.btnGetValue.Text = "&Get";
            this.btnGetValue.Click += new System.EventHandler(this.btnGetValue_Click);
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(16, 91);
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.Size = new System.Drawing.Size(411, 22);
            this.txtValue.TabIndex = 7;
            this.txtValue.TabStop = false;
            // 
            // txtRowNumber
            // 
            this.txtRowNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRowNumber.Location = new System.Drawing.Point(288, 59);
            this.txtRowNumber.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txtRowNumber.Name = "txtRowNumber";
            this.txtRowNumber.Size = new System.Drawing.Size(56, 22);
            this.txtRowNumber.TabIndex = 5;
            this.txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(6, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "G&et column";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.cboDataSource.Location = new System.Drawing.Point(104, 27);
            this.cboDataSource.Name = "cboDataSource";
            this.cboDataSource.Size = new System.Drawing.Size(132, 24);
            this.cboDataSource.TabIndex = 1;
            this.cboDataSource.SelectedIndexChanged += new System.EventHandler(this.cboDataSource_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Data Source";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // UserControlComboTestForm
            // 
            this.ClientSize = new System.Drawing.Size(827, 484);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.pgProps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboUCCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboAutoComp);
            this.MinimumSize = new System.Drawing.Size(835, 512);
            this.Name = "UserControlComboTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoComplete/User Control Combo Test";
            ((System.ComponentModel.ISupportInitialize)(this.cboUCCombo)).EndInit();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNumber)).EndInit();
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
        private System.Windows.Forms.NumericUpDown txtRowNumber;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnGetValue;
        private System.Windows.Forms.ComboBox cboColumns;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ImageList ilImages;
        private EWSoftware.ListControls.UserControlComboBox cboUCCombo;
    }
}
