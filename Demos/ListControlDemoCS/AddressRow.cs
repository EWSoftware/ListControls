//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : AddressRow.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/06/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This is a sample row template control for the DataList demo
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/17/2005  EFW  Created the code
//===============================================================================================================

namespace ListControlDemoCS
{
	/// <summary>
	/// This is a sample row template control for the DataList demo
	/// </summary>
	public partial class AddressRow : TemplateControl
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public AddressRow()
		{
            // At runtime, actual initialization is deferred until needed
            if(this.DesignMode)
		        InitializeComponent();
		}
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// This is overridden to confirm the deletion
        /// </summary>
        /// <returns>True to allow the delete, false if not</returns>
        public override bool CanDelete
        {
            get
            {
                if(MessageBox.Show($"Are you sure you want to delete the name '{txtFName.Text} {txtLName.Text}'?",
                  "Data List Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Actual initialization is deferred until needed to save time and resources
        /// </summary>
        protected override void InitializeTemplate()
        {
            // Create the contained controls
            this.InitializeComponent();

            // Use the shared data source for the combo box
            cboState.DisplayMember = cboState.ValueMember = nameof(StateCode.State);
            cboState.DataSource = (List<StateCode>?)this.TemplateParent.SharedDataSources["States"];

            // Update control states based on the parent's change policy.  This can be omitted if you do not
            // need it.
            this.ChangePolicyModified();
        }

        /// <summary>
        /// Bind the controls to their data source
        /// </summary>
        protected override void Bind()
        {
            this.AddBinding(txtFName, nameof(Control.Text), nameof(Address.FirstName));
            this.AddBinding(txtLName, nameof(Control.Text), nameof(Address.LastName));
            this.AddBinding(txtAddress, nameof(Control.Text), nameof(Address.StreetAddress));
            this.AddBinding(txtCity, nameof(Control.Text), nameof(Address.City));
            this.AddBinding(txtZip, nameof(Control.Text), nameof(Address.Zip));

            // We must enable formatting or the bound values for these control types won't get updated for
            // some reason.  The sum value is also nullable so it needs a format event handler.
            this.AddBinding(cboState, nameof(MultiColumnComboBox.SelectedValue), nameof(Address.State)).FormattingEnabled = true;
            this.AddBinding(udcSumValue, nameof(NumericUpDown.Value), nameof(Address.SumValue), true,
                (s, e) => e.Value ??= 0).FormattingEnabled = true;

            // They also need to update on the property value changing in case the mouse wheel is used
            udcSumValue.DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
            cboState.DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
        }

        /// <summary>
        /// Enable or disable the controls based on the edit policy.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void ChangePolicyModified()
        {
            if(this.TemplateParent.AllowEdits != txtFName.Enabled && !this.IsNewRow)
            {
                txtFName.Enabled = txtLName.Enabled = txtAddress.Enabled = txtCity.Enabled = cboState.Enabled =
                    txtZip.Enabled = udcSumValue.Enabled = this.TemplateParent.AllowEdits;
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Require a first and last name
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void AddressRow_Validating(object sender, CancelEventArgs e)
        {
            epErrors.Clear();

            if(txtFName.Text.Trim().Length == 0)
            {
                epErrors.SetError(txtFName, "A first name is required");
                e.Cancel = true;
            }

            if(txtLName.Text.Trim().Length == 0)
            {
                epErrors.SetError(txtLName, "A last name is required");
                e.Cancel = true;
            }
        }
        #endregion
    }
}
