//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : AddressHeader.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/06/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This is a sample header template control for the DataList demo
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
    /// This is a sample header template control for the DataList demo
    /// </summary>
    public partial class AddressHeader : TemplateControl
    {
        #region Private data members
        //=====================================================================

        private string lastSearch;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public AddressHeader()
        {
            // Since there is only one instance of the header it is created when assigned so we don't need to
            // delay initialization.
            InitializeComponent();

            lastSearch = String.Empty;
        }
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// Bind the ID label to the field in the data source
        /// </summary>
        protected override void Bind()
        {
            // If the DataList uses a DataSet you must use the fully qualified field name in header and footer
            // controls as they are bound to the data source as a whole.
            this.AddBinding(lblKey, nameof(Control.Text), nameof(Address.ID));
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Find an entry by last name (incremental search)
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtFindName_TextChanged(object sender, EventArgs e)
        {
            if(txtFindName.Text.Length > 0)
            {
                int startRow = (txtFindName.Text.Length <= lastSearch.Length) ? -1 : this.TemplateParent.CurrentRow - 1;

                lastSearch = txtFindName.Text;
                
                int row = this.TemplateParent.FindString(nameof(Address.LastName), txtFindName.Text, startRow);

                if(row != -1)
                {
                    this.TemplateParent.MoveTo(row);
                    txtFindName.Focus();
                }
            }
        }
        #endregion
    }
}
