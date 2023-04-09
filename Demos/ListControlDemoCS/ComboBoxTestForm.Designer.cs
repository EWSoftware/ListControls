namespace ListControlDemoCS
{
    partial class ComboBoxTestForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComboBoxTestForm));
            cboAutoComp = new EWSoftware.ListControls.AutoCompleteComboBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            cboMultiCol = new EWSoftware.ListControls.MultiColumnComboBox();
            pgProps = new System.Windows.Forms.PropertyGrid();
            grpOptions = new System.Windows.Forms.GroupBox();
            cboColumns = new System.Windows.Forms.ComboBox();
            label4 = new System.Windows.Forms.Label();
            btnGetValue = new System.Windows.Forms.Button();
            txtValue = new System.Windows.Forms.TextBox();
            txtRowNumber = new System.Windows.Forms.NumericUpDown();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            cboDataSource = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            ilImages = new System.Windows.Forms.ImageList(components);
            ((System.ComponentModel.ISupportInitialize)cboMultiCol).BeginInit();
            grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtRowNumber).BeginInit();
            this.SuspendLayout();
            // 
            // cboAutoComp
            // 
            cboAutoComp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cboAutoComp.Location = new System.Drawing.Point(496, 16);
            cboAutoComp.Name = "cboAutoComp";
            cboAutoComp.Size = new System.Drawing.Size(323, 33);
            cboAutoComp.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(386, 16);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(104, 23);
            label2.TabIndex = 0;
            label2.Text = "&Auto-Complete";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(386, 164);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(104, 23);
            label3.TabIndex = 2;
            label3.Text = "&Multi-Column";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMultiCol
            // 
            cboMultiCol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cboMultiCol.Location = new System.Drawing.Point(496, 164);
            cboMultiCol.Name = "cboMultiCol";
            cboMultiCol.Size = new System.Drawing.Size(323, 34);
            cboMultiCol.TabIndex = 3;
            cboMultiCol.FormatDropDownColumn += this.cboMultiCol_FormatDropDownColumn;
            cboMultiCol.DrawItemImage += this.cboMultiCol_DrawItemImage;
            cboMultiCol.SelectedIndexChanged += this.cboMultiCol_SelectedIndexChanged;
            // 
            // pgProps
            // 
            pgProps.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            pgProps.LineColor = System.Drawing.SystemColors.ScrollBar;
            pgProps.Location = new System.Drawing.Point(6, 0);
            pgProps.Name = "pgProps";
            pgProps.Size = new System.Drawing.Size(368, 467);
            pgProps.TabIndex = 5;
            pgProps.PropertyValueChanged += this.pgProps_PropertyValueChanged;
            // 
            // grpOptions
            // 
            grpOptions.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            grpOptions.Controls.Add(cboColumns);
            grpOptions.Controls.Add(label4);
            grpOptions.Controls.Add(btnGetValue);
            grpOptions.Controls.Add(txtValue);
            grpOptions.Controls.Add(txtRowNumber);
            grpOptions.Controls.Add(label6);
            grpOptions.Controls.Add(label5);
            grpOptions.Controls.Add(cboDataSource);
            grpOptions.Controls.Add(label1);
            grpOptions.Location = new System.Drawing.Point(384, 304);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new System.Drawing.Size(435, 160);
            grpOptions.TabIndex = 4;
            grpOptions.TabStop = false;
            grpOptions.Text = "Options";
            // 
            // cboColumns
            // 
            cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboColumns.Location = new System.Drawing.Point(104, 103);
            cboColumns.Name = "cboColumns";
            cboColumns.Size = new System.Drawing.Size(132, 33);
            cboColumns.TabIndex = 4;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label4.Location = new System.Drawing.Point(8, 16);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(424, 56);
            label4.TabIndex = 0;
            label4.Text = "NOTE: To apply changes to the ColumnFilter property you must change one other property because the property grid does not notify us of changes to collection objects.";
            // 
            // btnGetValue
            // 
            btnGetValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnGetValue.Location = new System.Drawing.Point(352, 99);
            btnGetValue.Name = "btnGetValue";
            btnGetValue.Size = new System.Drawing.Size(75, 28);
            btnGetValue.TabIndex = 7;
            btnGetValue.Text = "&Get";
            btnGetValue.Click += this.btnGetValue_Click;
            // 
            // txtValue
            // 
            txtValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtValue.Location = new System.Drawing.Point(16, 132);
            txtValue.Name = "txtValue";
            txtValue.ReadOnly = true;
            txtValue.Size = new System.Drawing.Size(411, 31);
            txtValue.TabIndex = 8;
            txtValue.TabStop = false;
            // 
            // txtRowNumber
            // 
            txtRowNumber.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            txtRowNumber.Location = new System.Drawing.Point(288, 103);
            txtRowNumber.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            txtRowNumber.Name = "txtRowNumber";
            txtRowNumber.Size = new System.Drawing.Size(56, 31);
            txtRowNumber.TabIndex = 6;
            txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label6.Location = new System.Drawing.Point(240, 103);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(48, 23);
            label6.TabIndex = 5;
            label6.Text = "at row";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label5.Location = new System.Drawing.Point(9, 103);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(89, 23);
            label5.TabIndex = 3;
            label5.Text = "G&et column";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDataSource
            // 
            cboDataSource.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            cboDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboDataSource.Items.AddRange(new object[] { "Data Table", "Data View", "Data Set", "Array List", "Strings" });
            cboDataSource.Location = new System.Drawing.Point(104, 75);
            cboDataSource.Name = "cboDataSource";
            cboDataSource.Size = new System.Drawing.Size(132, 33);
            cboDataSource.TabIndex = 2;
            cboDataSource.SelectedIndexChanged += this.cboDataSource_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label1.Location = new System.Drawing.Point(6, 75);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(92, 23);
            label1.TabIndex = 1;
            label1.Text = "&Data Source";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ilImages
            // 
            ilImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            ilImages.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ilImages.ImageStream");
            ilImages.TransparentColor = System.Drawing.Color.Magenta;
            ilImages.Images.SetKeyName(0, "Audio.bmp");
            ilImages.Images.SetKeyName(1, "Bitmap.bmp");
            ilImages.Images.SetKeyName(2, "Disk.bmp");
            ilImages.Images.SetKeyName(3, "Folder.bmp");
            ilImages.Images.SetKeyName(4, "Waste.bmp");
            // 
            // ComboBoxTestForm
            // 
            this.ClientSize = new System.Drawing.Size(827, 484);
            this.Controls.Add(grpOptions);
            this.Controls.Add(pgProps);
            this.Controls.Add(label3);
            this.Controls.Add(cboMultiCol);
            this.Controls.Add(label2);
            this.Controls.Add(cboAutoComp);
            this.MinimumSize = new System.Drawing.Size(835, 512);
            this.Name = "ComboBoxTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoComplete/Multi-Column Combo Test";
            ((System.ComponentModel.ISupportInitialize)cboMultiCol).EndInit();
            grpOptions.ResumeLayout(false);
            grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtRowNumber).EndInit();
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown txtRowNumber;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnGetValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboColumns;
        private System.Windows.Forms.ImageList ilImages;
        private EWSoftware.ListControls.MultiColumnComboBox cboMultiCol;

    }
}
