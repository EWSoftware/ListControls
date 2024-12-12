//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : CheckedItemsCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a collection class used by the checkbox list control to return a list of the currently
// checked items.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/16/2002  EFW  Created the code
// 01/30/2007  EFW  Rewrote to use ReadOnlyCollection<T> as base class
//===============================================================================================================

using System.Collections.ObjectModel;
using System.Text;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is a derived <see cref="ReadOnlyCollection{T}"/> class that contains a set of items from a
    /// <see cref="CheckBoxList"/> control.  Each entry represents an item that has a check state of
    /// <c>Checked</c> or <c>Indeterminate</c>.
    /// </summary>
    /// <remarks>The collection itself cannot be modified, but the items in it can</remarks>
    public class CheckedItemsCollection : ReadOnlyCollection<object>
    {
        #region Private data members
        //=====================================================================

        private readonly CheckBoxList owner;
        private readonly string valueMember, displayMember;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="control">The checkbox list control</param>
        /// <param name="list">The list to wrap</param>
        internal CheckedItemsCollection(CheckBoxList control, IList<object> list) : base(list)
        {
            string[] memberParts;

            owner = control;

            if(owner.DataSource != null)
            {
                memberParts = owner.ValueMember.Split('.');
                valueMember = memberParts[memberParts.Length - 1];
                memberParts = owner.DisplayMember.Split('.');
                displayMember = memberParts[memberParts.Length - 1];
            }
            else
                valueMember = displayMember = String.Empty;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Get the value of the specified item from the checkbox list
        /// </summary>
        /// <param name="index">The index of the item in this collection</param>
        /// <returns>The value of the checked item from the checkbox list.  The value will be extracted from the
        /// data source based on the <see cref="BaseListControl.ValueMember">CheckBoxList.ValueMember</see>
        /// setting.</returns>
        public object ValueOf(int index)
        {
            // If it has a data source, return the value from the data source.  If not, return the item itself.
            if(owner.DataSource != null)
                return owner[owner.Items.IndexOf(this[index]), valueMember]!;

            return this[index];
        }

        /// <summary>
        /// Get the display text of the specified item from the checkbox list
        /// </summary>
        /// <param name="index">The index of the item in this collection</param>
        /// <returns>The display text value of the checked item from the checkbox list.  The value will be
        /// extracted from the data source based on the
        /// <see cref="BaseListControl.DisplayMember">CheckBoxList.DisplayMember</see> setting.</returns>
        public object DisplayTextOf(int index)
        {
            // If it has a data source, return the value from the data source.  If not, return the item itself.
            if(owner.DataSource != null)
                return owner[owner.Items.IndexOf(this[index]), displayMember]!;

            return this[index];
        }

        /// <summary>
        /// Get the check state of the specified item from the checkbox list
        /// </summary>
        /// <param name="index">The index of the item in this collection</param>
        /// <returns>The check state of the checked item from the checkbox list</returns>
        public CheckState CheckStateOf(int index)
        {
            return ((CheckBox)owner.ButtonPanel.Controls[owner.Items.IndexOf(this[index])]).CheckState;
        }

        /// <summary>
        /// Convert the checked item values to a comma-separated list
        /// </summary>
        /// <returns>Returns a string containing the values separated by commas.  The values will be those
        /// extracted from the data source based on the
        /// <see cref="BaseListControl.ValueMember">CheckBoxList.ValueMember</see> setting.</returns>
        public override string ToString()
        {
            StringBuilder sb = new(1024);

            for(int idx = 0; idx < this.Count; idx++)
            {
                if(idx != 0)
                    sb.Append(", ");

                sb.Append(this.ValueOf(idx));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Convert the checked item display text values to a comma-separated list
        /// </summary>
        /// <returns>Returns a string containing the display values separated by commas.  The values will be
        /// those extracted from the data source based on the
        /// <see cref="BaseListControl.DisplayMember">CheckBoxList.DisplayMember</see> setting.</returns>
        public string ToDisplayTextString()
        {
            StringBuilder sb = new(1024);

            for(int idx = 0; idx < this.Count; idx++)
            {
                if(idx != 0)
                    sb.Append(", ");

                sb.Append(this.DisplayTextOf(idx));
            }

            return sb.ToString();
        }
        #endregion
    }
}
