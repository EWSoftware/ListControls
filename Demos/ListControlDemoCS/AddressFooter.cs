//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : AddressFooter.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/01/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This is a sample footer template control for the DataList demo
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

using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is a sample footer template control for the DataList demo
	/// </summary>
	public partial class AddressFooter : EWSoftware.ListControls.TemplateControl
	{
        #region Private data members
        //=====================================================================

        // Used to track the current data source for totaling
        private IBindingList bl;
        private DataTable tblItems;
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public AddressFooter()
		{
            // Since there is only one instance of the footer it is created when assigned so we don't need to
            // delay initialization.
			InitializeComponent();
		}
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// We still need to override this even if there are no bound controls.  It's also the place to hook up
        /// event handlers if creating such things as footer totals, etc.
        /// </summary>
        protected override void Bind()
        {
            // The demo uses a data set so we'll get a reference to the table through the list manager
            CurrencyManager cm = this.TemplateParent.ListManager;
            DataTable newSource = ((DataView)cm.List).Table;

            // Hook up the events on the data source to keep the total current
            if(newSource != tblItems)
            {
                // Disconnect from the old source if necessary
                if(tblItems != null)
                {
                    bl.ListChanged -= DataSource_ListChanged;
                    tblItems.RowChanged -= DataSource_RowChgDel;
                    tblItems.RowDeleted -= DataSource_RowChgDel;
                }

                tblItems = newSource;

                if(tblItems != null)
                {
                    // For the total, we'll sum it whenever a row is added, changed, or deleted
                    bl = (IBindingList)cm.List;

                    bl.ListChanged += DataSource_ListChanged;
                    tblItems.RowChanged += DataSource_RowChgDel;
                    tblItems.RowDeleted += DataSource_RowChgDel;

                    // Show the initial total
                    lblTotal.Text = tblItems.Compute("Sum(SumValue)", null).ToString();
                }
                else
                    lblTotal.Text = null;
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Update the total when a row is added
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemAdded)
                lblTotal.Text = tblItems.Compute("Sum(SumValue)", null).ToString();
        }

        /// <summary>
        /// Update the total when a row is changed or deleted
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataSource_RowChgDel(object sender, DataRowChangeEventArgs e)
        {
            lblTotal.Text = tblItems.Compute("Sum(SumValue)", null).ToString();
        }
        #endregion
    }
}
