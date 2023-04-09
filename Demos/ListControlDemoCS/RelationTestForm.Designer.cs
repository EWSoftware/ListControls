namespace ListControlDemoCS
{
    partial class RelationTestForm
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
                dbConn?.Dispose();
                daAddresses?.Dispose();
                dsAddresses?.Dispose();
                daPhones?.Dispose();
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.dnNav = new EWSoftware.ListControls.DataNavigator();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlData = new System.Windows.Forms.Panel();
            this.rblContactType = new EWSoftware.ListControls.RadioButtonList();
            this.cblAddressTypes = new EWSoftware.ListControls.CheckBoxList();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.cboState = new EWSoftware.ListControls.MultiColumnComboBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAddRow = new System.Windows.Forms.Label();
            this.epErrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.dlPhones = new EWSoftware.ListControls.DataList();
            this.txtFindName = new System.Windows.Forms.TextBox();
            this.clickableLabel1 = new EWSoftware.ListControls.ClickableLabel();
            this.pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rblContactType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cblAddressTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point(12, 387);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(104, 28);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "L&oad Data";
            this.toolTip1.SetToolTip(this.btnLoad, "Load data into the control");
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dnNav
            // 
            this.dnNav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dnNav.Location = new System.Drawing.Point(12, 327);
            this.dnNav.Name = "dnNav";
            this.dnNav.Size = new System.Drawing.Size(282, 22);
            this.dnNav.TabIndex = 1;
            this.dnNav.DeletingRow += new System.EventHandler<EWSoftware.ListControls.DataNavigatorCancelEventArgs>(this.dnNav_DeletingRow);
            this.dnNav.Validating += new System.ComponentModel.CancelEventHandler(this.dnNav_Validating);
            this.dnNav.AddedRow += new System.EventHandler<EWSoftware.ListControls.DataNavigatorEventArgs>(this.dnNav_AddedRow);
            this.dnNav.CanceledEdits += new System.EventHandler<EWSoftware.ListControls.DataNavigatorEventArgs>(this.dnNav_CanceledEdits);
            this.dnNav.NoRows += new System.EventHandler(this.dnNav_NoRows);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(122, 387);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 28);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&Save Data";
            this.toolTip1.SetToolTip(this.btnSave, "Save all changes");
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlData
            // 
            this.pnlData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlData.Controls.Add(this.rblContactType);
            this.pnlData.Controls.Add(this.cblAddressTypes);
            this.pnlData.Controls.Add(this.txtZip);
            this.pnlData.Controls.Add(this.cboState);
            this.pnlData.Controls.Add(this.txtCity);
            this.pnlData.Controls.Add(this.txtAddress);
            this.pnlData.Controls.Add(this.txtLName);
            this.pnlData.Controls.Add(this.txtFName);
            this.pnlData.Controls.Add(this.label1);
            this.pnlData.Controls.Add(this.lblKey);
            this.pnlData.Controls.Add(this.label5);
            this.pnlData.Controls.Add(this.label4);
            this.pnlData.Controls.Add(this.label3);
            this.pnlData.Controls.Add(this.label2);
            this.pnlData.Location = new System.Drawing.Point(12, 40);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(494, 281);
            this.pnlData.TabIndex = 0;
            // 
            // rblContactType
            // 
            this.rblContactType.ListPadding = new EWSoftware.ListControls.ListPadding(5, 40, 4, 8, 4, 5);
            this.rblContactType.Location = new System.Drawing.Point(310, 151);
            this.rblContactType.Name = "rblContactType";
            this.rblContactType.Size = new System.Drawing.Size(178, 123);
            this.rblContactType.TabIndex = 13;
            this.rblContactType.TitleBackColor = System.Drawing.Color.Gainsboro;
            this.rblContactType.TitleText = "Contact Type";
            // 
            // cblAddressTypes
            // 
            this.cblAddressTypes.BindingMembers.AddRange(new string[] {
            "Addresses.Domestic",
            "Addresses.International",
            "Addresses.Postal",
            "Addresses.Parcel",
            "Addresses.Home",
            "Addresses.Business"});
            this.cblAddressTypes.BindingMembersBindingContext = null;
            this.cblAddressTypes.Items.AddRange(new object[] {
            "Domestic",
            "International",
            "Postal",
            "Parcel",
            "Home",
            "Business"});
            this.cblAddressTypes.LayoutMethod = EWSoftware.ListControls.LayoutMethod.DownThenAcross;
            this.cblAddressTypes.ListPadding = new EWSoftware.ListControls.ListPadding(8, 30, 4, 8, 20, 5);
            this.cblAddressTypes.Location = new System.Drawing.Point(12, 151);
            this.cblAddressTypes.Name = "cblAddressTypes";
            this.cblAddressTypes.Size = new System.Drawing.Size(292, 123);
            this.cblAddressTypes.TabIndex = 12;
            this.cblAddressTypes.TitleBackColor = System.Drawing.Color.Gainsboro;
            this.cblAddressTypes.TitleText = "Address Types";
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(296, 108);
            this.txtZip.MaxLength = 10;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(77, 22);
            this.txtZip.TabIndex = 11;
            // 
            // cboState
            // 
            this.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboState.Location = new System.Drawing.Point(228, 108);
            this.cboState.MaxDropDownItems = 16;
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(57, 24);
            this.cboState.TabIndex = 10;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(92, 108);
            this.txtCity.MaxLength = 20;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(125, 22);
            this.txtCity.TabIndex = 9;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(92, 76);
            this.txtAddress.MaxLength = 50;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(384, 22);
            this.txtAddress.TabIndex = 7;
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(92, 44);
            this.txtLName.MaxLength = 30;
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(160, 22);
            this.txtLName.TabIndex = 3;
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(344, 44);
            this.txtFName.MaxLength = 20;
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(132, 22);
            this.txtFName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(62, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKey
            // 
            this.lblKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblKey.Location = new System.Drawing.Point(92, 12);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(64, 23);
            this.lblKey.TabIndex = 1;
            this.lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(2, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "&City/St/Zip";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(10, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "&Address";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(9, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "&Last Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(262, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "First Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAddRow
            // 
            this.lblAddRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAddRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblAddRow.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblAddRow.Location = new System.Drawing.Point(12, 352);
            this.lblAddRow.Name = "lblAddRow";
            this.lblAddRow.Size = new System.Drawing.Size(492, 24);
            this.lblAddRow.TabIndex = 3;
            this.lblAddRow.Text = "Please click the Add button to add a new row";
            this.lblAddRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAddRow.Visible = false;
            // 
            // epErrors
            // 
            this.epErrors.ContainerControl = this;
            // 
            // dlPhones
            // 
            this.dlPhones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dlPhones.CaptionText = "Phone Numbers";
            this.dlPhones.CaptionVisible = true;
            this.dlPhones.Location = new System.Drawing.Point(510, 12);
            this.dlPhones.Name = "dlPhones";
            this.dlPhones.Size = new System.Drawing.Size(306, 364);
            this.dlPhones.TabIndex = 2;
            this.dlPhones.AddingRow += new System.EventHandler<EWSoftware.ListControls.DataListCancelEventArgs>(this.dlPhones_AddingRow);
            // 
            // txtFindName
            // 
            this.txtFindName.Location = new System.Drawing.Point(127, 12);
            this.txtFindName.MaxLength = 30;
            this.txtFindName.Name = "txtFindName";
            this.txtFindName.Size = new System.Drawing.Size(136, 22);
            this.txtFindName.TabIndex = 7;
            this.txtFindName.TextChanged += new System.EventHandler(this.txtFindName_TextChanged);
            // 
            // clickableLabel1
            // 
            this.clickableLabel1.Location = new System.Drawing.Point(6, 12);
            this.clickableLabel1.Name = "clickableLabel1";
            this.clickableLabel1.Size = new System.Drawing.Size(115, 23);
            this.clickableLabel1.TabIndex = 6;
            this.clickableLabel1.Text = "&Find Last Name";
            this.clickableLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RelationTestForm
            // 
            this.ClientSize = new System.Drawing.Size(828, 427);
            this.Controls.Add(this.txtFindName);
            this.Controls.Add(this.dlPhones);
            this.Controls.Add(this.clickableLabel1);
            this.Controls.Add(this.lblAddRow);
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dnNav);
            this.Controls.Add(this.btnLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RelationTestForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relationship Test Form";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.RelationTestForm_Closing);
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rblContactType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cblAddressTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button btnLoad;
		private EWSoftware.ListControls.DataNavigator dnNav;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip1;
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
        private EWSoftware.ListControls.DataList dlPhones;
        private System.Windows.Forms.TextBox txtFindName;
        private EWSoftware.ListControls.ClickableLabel clickableLabel1;
        private EWSoftware.ListControls.CheckBoxList cblAddressTypes;
        private EWSoftware.ListControls.RadioButtonList rblContactType;
    }
}
