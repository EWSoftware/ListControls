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
            dnNav = new DataNavigator();
            btnSave = new Button();
            toolTip1 = new ToolTip(components);
            pnlData = new Panel();
            rblContactType = new RadioButtonList();
            cblAddressTypes = new CheckBoxList();
            txtZip = new TextBox();
            cboState = new MultiColumnComboBox();
            txtCity = new TextBox();
            txtAddress = new TextBox();
            txtLName = new TextBox();
            txtFName = new TextBox();
            label1 = new Label();
            lblKey = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            lblAddRow = new Label();
            epErrors = new ErrorProvider(components);
            dlPhones = new DataList();
            txtFindName = new TextBox();
            clickableLabel1 = new ClickableLabel();
            pnlData.SuspendLayout();
            ((ISupportInitialize)rblContactType).BeginInit();
            ((ISupportInitialize)cblAddressTypes).BeginInit();
            ((ISupportInitialize)cboState).BeginInit();
            ((ISupportInitialize)epErrors).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLoad.Location = new Point(12, 388);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(104, 32);
            btnLoad.TabIndex = 4;
            btnLoad.Text = "L&oad Data";
            toolTip1.SetToolTip(btnLoad, "Load data into the control");
            btnLoad.Click += this.btnLoad_Click;
            // 
            // dnNav
            // 
            dnNav.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            dnNav.Location = new Point(12, 336);
            dnNav.Name = "dnNav";
            dnNav.Size = new Size(600, 22);
            dnNav.TabIndex = 1;
            dnNav.AddedRow += this.dnNav_AddedRow;
            dnNav.DeletingRow += this.dnNav_DeletingRow;
            dnNav.CanceledEdits += this.dnNav_CanceledEdits;
            dnNav.Current += this.dnNav_Current;
            dnNav.NoRows += this.dnNav_NoRows;
            dnNav.Validating += this.dnNav_Validating;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.Location = new Point(122, 388);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(104, 32);
            btnSave.TabIndex = 5;
            btnSave.Text = "&Save Data";
            toolTip1.SetToolTip(btnSave, "Save all changes");
            btnSave.Click += this.btnSave_Click;
            // 
            // pnlData
            // 
            pnlData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlData.Controls.Add(rblContactType);
            pnlData.Controls.Add(cblAddressTypes);
            pnlData.Controls.Add(txtZip);
            pnlData.Controls.Add(cboState);
            pnlData.Controls.Add(txtCity);
            pnlData.Controls.Add(txtAddress);
            pnlData.Controls.Add(txtLName);
            pnlData.Controls.Add(txtFName);
            pnlData.Controls.Add(label1);
            pnlData.Controls.Add(lblKey);
            pnlData.Controls.Add(label5);
            pnlData.Controls.Add(label4);
            pnlData.Controls.Add(label3);
            pnlData.Controls.Add(label2);
            pnlData.Location = new Point(12, 45);
            pnlData.Name = "pnlData";
            pnlData.Size = new Size(600, 285);
            pnlData.TabIndex = 0;
            // 
            // rblContactType
            // 
            rblContactType.ListPadding = new ListPadding(5, 40, 4, 8, 4, 5);
            rblContactType.Location = new Point(399, 151);
            rblContactType.Name = "rblContactType";
            rblContactType.Size = new Size(198, 123);
            rblContactType.TabIndex = 13;
            rblContactType.TitleBackColor = Color.Gainsboro;
            rblContactType.TitleFont = new Font("Segoe UI", 9F);
            rblContactType.TitleText = "Contact Type";
            // 
            // cblAddressTypes
            // 
            cblAddressTypes.BindingMembers.Add("Domestic");
            cblAddressTypes.BindingMembers.Add("International");
            cblAddressTypes.BindingMembers.Add("Postal");
            cblAddressTypes.BindingMembers.Add("Parcel");
            cblAddressTypes.BindingMembers.Add("Home");
            cblAddressTypes.BindingMembers.Add("Business");
            cblAddressTypes.BindingMembersBindingContext = null;
            cblAddressTypes.Items.AddRange(new object[] { "Domestic", "International", "Postal", "Parcel", "Home", "Business" });
            cblAddressTypes.LayoutMethod = LayoutMethod.DownThenAcross;
            cblAddressTypes.ListPadding = new ListPadding(8, 30, 4, 8, 20, 5);
            cblAddressTypes.Location = new Point(12, 151);
            cblAddressTypes.Name = "cblAddressTypes";
            cblAddressTypes.Size = new Size(381, 123);
            cblAddressTypes.TabIndex = 12;
            cblAddressTypes.TitleBackColor = Color.Gainsboro;
            cblAddressTypes.TitleFont = new Font("Segoe UI", 9F);
            cblAddressTypes.TitleText = "Address Types";
            // 
            // txtZip
            // 
            txtZip.Location = new Point(347, 110);
            txtZip.MaxLength = 10;
            txtZip.Name = "txtZip";
            txtZip.Size = new Size(77, 27);
            txtZip.TabIndex = 11;
            // 
            // cboState
            // 
            cboState.DropDownStyle = ComboBoxStyle.DropDownList;
            cboState.Location = new Point(275, 110);
            cboState.MaxDropDownItems = 16;
            cboState.Name = "cboState";
            cboState.Size = new Size(66, 29);
            cboState.TabIndex = 10;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(109, 110);
            txtCity.MaxLength = 20;
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(160, 27);
            txtCity.TabIndex = 9;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(109, 77);
            txtAddress.MaxLength = 50;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(407, 27);
            txtAddress.TabIndex = 7;
            // 
            // txtLName
            // 
            txtLName.Location = new Point(109, 44);
            txtLName.MaxLength = 30;
            txtLName.Name = "txtLName";
            txtLName.Size = new Size(160, 27);
            txtLName.TabIndex = 3;
            // 
            // txtFName
            // 
            txtFName.Location = new Point(379, 44);
            txtFName.MaxLength = 20;
            txtFName.Name = "txtFName";
            txtFName.Size = new Size(137, 27);
            txtFName.TabIndex = 5;
            // 
            // label1
            // 
            label1.Location = new Point(71, 12);
            label1.Name = "label1";
            label1.Size = new Size(32, 23);
            label1.TabIndex = 0;
            label1.Text = "ID";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblKey
            // 
            lblKey.BorderStyle = BorderStyle.Fixed3D;
            lblKey.Location = new Point(109, 12);
            lblKey.Name = "lblKey";
            lblKey.Size = new Size(64, 23);
            lblKey.TabIndex = 1;
            lblKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(6, 112);
            label5.Name = "label5";
            label5.Size = new Size(97, 22);
            label5.TabIndex = 8;
            label5.Text = "&City/St/Zip";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(27, 79);
            label4.Name = "label4";
            label4.Size = new Size(76, 22);
            label4.TabIndex = 6;
            label4.Text = "&Address";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(8, 46);
            label3.Name = "label3";
            label3.Size = new Size(95, 22);
            label3.TabIndex = 2;
            label3.Text = "&Last Name";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(280, 46);
            label2.Name = "label2";
            label2.Size = new Size(93, 22);
            label2.TabIndex = 4;
            label2.Text = "First Name";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblAddRow
            // 
            lblAddRow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblAddRow.BackColor = SystemColors.ActiveCaption;
            lblAddRow.ForeColor = SystemColors.ActiveCaptionText;
            lblAddRow.Location = new Point(12, 361);
            lblAddRow.Name = "lblAddRow";
            lblAddRow.Size = new Size(600, 24);
            lblAddRow.TabIndex = 3;
            lblAddRow.Text = "Please click the Add button to add a new row";
            lblAddRow.TextAlign = ContentAlignment.MiddleCenter;
            lblAddRow.Visible = false;
            // 
            // epErrors
            // 
            epErrors.ContainerControl = this;
            // 
            // dlPhones
            // 
            dlPhones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dlPhones.CaptionText = "Phone Numbers";
            dlPhones.CaptionVisible = true;
            dlPhones.Location = new Point(618, 12);
            dlPhones.Name = "dlPhones";
            dlPhones.Size = new Size(376, 373);
            dlPhones.TabIndex = 2;
            dlPhones.AddedRow += this.dlPhones_AddedRow;
            dlPhones.DeletingRow += this.dlPhones_DeletingRow;
            // 
            // txtFindName
            // 
            txtFindName.Location = new Point(154, 12);
            txtFindName.MaxLength = 30;
            txtFindName.Name = "txtFindName";
            txtFindName.Size = new Size(160, 27);
            txtFindName.TabIndex = 7;
            txtFindName.TextChanged += this.txtFindName_TextChanged;
            // 
            // clickableLabel1
            // 
            clickableLabel1.Location = new Point(12, 14);
            clickableLabel1.Name = "clickableLabel1";
            clickableLabel1.Size = new Size(136, 23);
            clickableLabel1.TabIndex = 6;
            clickableLabel1.Text = "&Find Last Name";
            clickableLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // RelationTestForm
            // 
            this.ClientSize = new Size(1006, 432);
            this.Controls.Add(txtFindName);
            this.Controls.Add(dlPhones);
            this.Controls.Add(clickableLabel1);
            this.Controls.Add(lblAddRow);
            this.Controls.Add(pnlData);
            this.Controls.Add(btnSave);
            this.Controls.Add(dnNav);
            this.Controls.Add(btnLoad);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RelationTestForm";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Relationship Test Form";
            this.Closing += this.RelationTestForm_Closing;
            pnlData.ResumeLayout(false);
            pnlData.PerformLayout();
            ((ISupportInitialize)rblContactType).EndInit();
            ((ISupportInitialize)cblAddressTypes).EndInit();
            ((ISupportInitialize)cboState).EndInit();
            ((ISupportInitialize)epErrors).EndInit();
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
