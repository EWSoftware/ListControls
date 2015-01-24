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
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RadioButtonListTestForm));
            this.pgProps = new System.Windows.Forms.PropertyGrid();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.cboColumns = new System.Windows.Forms.ComboBox();
            this.btnGetValue = new System.Windows.Forms.Button();
            this.txtRowNumber = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cboDataSource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rblDemo = new EWSoftware.ListControls.RadioButtonList();
            this.chkShowImages = new System.Windows.Forms.CheckBox();
            this.ilImages = new System.Windows.Forms.ImageList(this.components);
            this.grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rblDemo)).BeginInit();
            this.SuspendLayout();
            // 
            // pgProps
            // 
            this.pgProps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pgProps.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pgProps.Location = new System.Drawing.Point(6, 0);
            this.pgProps.Name = "pgProps";
            this.pgProps.Size = new System.Drawing.Size(368, 436);
            this.pgProps.TabIndex = 3;
            this.pgProps.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgProps_PropertyValueChanged);
            // 
            // grpOptions
            // 
            this.grpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOptions.Controls.Add(this.cboColumns);
            this.grpOptions.Controls.Add(this.btnGetValue);
            this.grpOptions.Controls.Add(this.txtRowNumber);
            this.grpOptions.Controls.Add(this.label6);
            this.grpOptions.Controls.Add(this.label5);
            this.grpOptions.Controls.Add(this.txtValue);
            this.grpOptions.Controls.Add(this.cboDataSource);
            this.grpOptions.Controls.Add(this.label1);
            this.grpOptions.Location = new System.Drawing.Point(384, 328);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(440, 120);
            this.grpOptions.TabIndex = 1;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // cboColumns
            // 
            this.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumns.Location = new System.Drawing.Point(104, 56);
            this.cboColumns.Name = "cboColumns";
            this.cboColumns.Size = new System.Drawing.Size(132, 24);
            this.cboColumns.TabIndex = 3;
            // 
            // btnGetValue
            // 
            this.btnGetValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGetValue.Location = new System.Drawing.Point(350, 52);
            this.btnGetValue.Name = "btnGetValue";
            this.btnGetValue.Size = new System.Drawing.Size(75, 28);
            this.btnGetValue.TabIndex = 6;
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
            this.txtRowNumber.Size = new System.Drawing.Size(56, 22);
            this.txtRowNumber.TabIndex = 5;
            this.txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(240, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 23);
            this.label6.TabIndex = 4;
            this.label6.Text = "at row";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(9, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 23);
            this.label5.TabIndex = 2;
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
            this.txtValue.Size = new System.Drawing.Size(424, 22);
            this.txtValue.TabIndex = 7;
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
            this.cboDataSource.Size = new System.Drawing.Size(132, 24);
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
            // rblDemo
            // 
            this.rblDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rblDemo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rblDemo.Location = new System.Drawing.Point(384, 8);
            this.rblDemo.Name = "rblDemo";
            this.rblDemo.Size = new System.Drawing.Size(440, 304);
            this.rblDemo.TabIndex = 0;
            this.rblDemo.SelectedIndexChanged += new System.EventHandler(this.rblDemo_SelectedIndexChanged);
            // 
            // chkShowImages
            // 
            this.chkShowImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowImages.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkShowImages.Location = new System.Drawing.Point(8, 436);
            this.chkShowImages.Name = "chkShowImages";
            this.chkShowImages.Size = new System.Drawing.Size(136, 24);
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
            // RadioButtonListTestForm
            // 
            this.ClientSize = new System.Drawing.Size(832, 464);
            this.Controls.Add(this.chkShowImages);
            this.Controls.Add(this.rblDemo);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.pgProps);
            this.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.MinimumSize = new System.Drawing.Size(840, 360);
            this.Name = "RadioButtonListTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radio Button List Test";
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rblDemo)).EndInit();
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
        private System.Windows.Forms.NumericUpDown txtRowNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboDataSource;
    }
}
