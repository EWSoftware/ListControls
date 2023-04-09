//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : PhoneRow.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/06/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This is a sample row template control for the DataList relationship demo
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/23/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is a sample row template control for the DataList relationship demo
	/// </summary>
	public partial class PhoneRow : EWSoftware.ListControls.TemplateControl
	{
        #region Private data members
        //=====================================================================

        // A simple edit for the phone number format
        private static readonly Regex rePhone = new Regex(@"^\(\d{3}\) \d{3}-\d{4}$");

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public PhoneRow()
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
                if(MessageBox.Show($"Are you sure you want to delete the phone number '{txtPhoneNumber.Text}'?",
                  "Relationship Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Actual initialization is deferred until needed to save time and resources
        /// </summary>
        protected override void InitializeTemplate()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Bind the controls to their data source
        /// </summary>
        protected override void Bind()
        {
            this.AddBinding(txtPhoneNumber, "Text", "PhoneNumber");
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Require a phone number in a specific format
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void PhoneRow_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            epErrors.Clear();

            if(txtPhoneNumber.Text.Trim().Length == 0)
            {
                epErrors.SetError(txtPhoneNumber, "A phone number is required");
                e.Cancel = true;
            }
            else
                if(!rePhone.IsMatch(txtPhoneNumber.Text))
                {
                    epErrors.SetError(txtPhoneNumber, "Please enter a phone number in the format (###) ###-####");
                    e.Cancel = true;
                }
                else
                    this.CommitChanges();
        }

        /// <summary>
        /// Delete this row
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            // There appears to be a bug with the Button control.  The click event can fire if it's in another
            // container even if validation prevents it getting the focus.  As such, ignore the click if we
            // aren't focused.
            if(this.ContainsFocus)
                this.DeleteRow();
        }
        #endregion
    }
}
