//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : RowControlCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a derived ControlCollection class that hooks up a LocationChanged event to the first
// control so that the overall containing DataList control can scroll the header and footer horizontally in
// unison with the rows.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 06/20/2005  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls
{
	/// <summary>
    /// This file contains a derived <see cref="Control.ControlCollection"/> class that hooks up a
    /// <c>LocationChanged</c> event to the first control so that the overall containing <see cref="DataList"/>
    /// control can scroll the header and footer horizontally in unison with the rows.
	/// </summary>
	internal sealed class RowControlCollection : Control.ControlCollection
	{
        #region Private data members
        //=====================================================================

        private Control? locationControl;
        private readonly RowPanel rowPanel;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="c">The owning control</param>
        internal RowControlCollection(Control c) : base(c)
        {
            rowPanel = (RowPanel)c;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Track location changes on the first control added
        /// </summary>
        /// <param name="value">The control to add</param>
        public override void Add(Control? value)
        {
            if(value != null)
            {
                base.Add(value);

                if(locationControl == null)
                {
                    locationControl = value;
                    locationControl.LocationChanged += Control_LocationChanged;
                }
            }
        }

        /// <summary>
        /// Clear the location tracking control on clear
        /// </summary>
        public override void Clear()
        {
            if(locationControl != null)
                locationControl.LocationChanged -= Control_LocationChanged;

            locationControl = null;
            base.Clear();
        }

        /// <summary>
        /// Connect a new location tracker if our original goes away
        /// </summary>
        /// <param name="value">The control to remove</param>
        public override void Remove(Control? value)
        {
            if(value != null)
            {
                base.Remove(value);

                if(locationControl == value)
                {
                    locationControl.LocationChanged -= Control_LocationChanged;

                    if(this.Count != 0)
                    {
                        locationControl = this[0];
                        locationControl.LocationChanged += Control_LocationChanged;
                    }
                    else
                        locationControl = null;
                }
            }
        }

        /// <summary>
        /// This is handled to scroll any footer and header in the containing DataList control when the row
        /// controls are scrolled.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void Control_LocationChanged(object? sender, EventArgs e)
        {
            ((DataList?)rowPanel.Parent)?.AdjustHeaderFooterPosition(locationControl!.Left);
        }
        #endregion
    }
}
