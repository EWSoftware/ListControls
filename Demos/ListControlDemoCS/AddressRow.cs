//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : AddressRow.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/01/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
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

using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is a sample row template control for the DataList demo
	/// </summary>
	public partial class AddressRow : EWSoftware.ListControls.TemplateControl
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
                if(MessageBox.Show(String.Format("Are you sure you want to delete the name '{0} {1}'?",
                  txtFName.Text, txtLName.Text), "Data List Test", MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return false;

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
            cboState.DisplayMember = cboState.ValueMember = "State";
            cboState.DataSource = (DataView)this.TemplateParent.SharedDataSources["States"];

            // Update control states based on the parent's change policy.  This can be omitted if you do not
            // need it.
            this.ChangePolicyModified();
        }

        /// <summary>
        /// Bind the controls to their data source
        /// </summary>
        protected override void Bind()
        {
            this.AddBinding(txtFName, "Text", "FirstName");
            this.AddBinding(txtLName, "Text", "LastName");
            this.AddBinding(txtAddress, "Text", "Address");
            this.AddBinding(txtCity, "Text", "City");
            this.AddBinding(cboState, "SelectedValue", "State");
            this.AddBinding(txtZip, "Text", "Zip");
            this.AddBinding(udcSumValue, "Text", "SumValue");
        }

        /// <summary>
        /// Enable or disable the controls based on the edit policy.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void ChangePolicyModified()
        {
            if(this.TemplateParent.AllowEdits != txtFName.Enabled && !this.IsNewRow)
                txtFName.Enabled = txtLName.Enabled = txtAddress.Enabled = txtCity.Enabled = cboState.Enabled =
                    txtZip.Enabled = udcSumValue.Enabled = this.TemplateParent.AllowEdits;
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
