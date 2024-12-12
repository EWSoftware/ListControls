//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : AddressFooter.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/01/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
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

using System.Globalization;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is a sample footer template control for the DataList demo
	/// </summary>
	public partial class AddressFooter : TemplateControl
	{
        #region Private data members
        //=====================================================================

        // Used to track the current data source for totaling
        private BindingList<Address>? addresses;

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
            // We'll get a reference to the table through the list manager
            CurrencyManager cm = this.TemplateParent.ListManager!;
            var newSource = (BindingList<Address>)cm.List;

            // Hook up the events on the data source to keep the total current
            if(newSource != addresses)
            {
                // Disconnect from the old source if necessary
                if(addresses != null)
                    addresses.ListChanged -= DataSource_ListChanged;

                if(newSource != null)
                {
                    // For the total, we'll sum it whenever a row is added, changed, or deleted
                    addresses = newSource;
                    addresses.ListChanged += DataSource_ListChanged;

                    // Show the initial total
                    this.DataSource_ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, 0));
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
        private void DataSource_ListChanged(object? sender, ListChangedEventArgs e)
        {
            lblTotal.Text = addresses!.Sum(a => a.SumValue ?? 0).ToString(CultureInfo.InvariantCulture);
        }
        #endregion
    }
}
