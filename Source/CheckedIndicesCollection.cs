//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : CheckedIndicesCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/08/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a collection class used by the checkbox list control to return a list of indices for the
// currently checked items.
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
	/// This is a derived <see cref="ReadOnlyCollection{T}"/> class that contains a set of indices from a
    /// <see cref="CheckBoxList"/> control.  Each entry represents the index of an item that has a check state of
    /// <c>Checked</c> or <c>Indeterminate</c>.
	/// </summary>
	/// <remarks>The collection itself cannot be modified</remarks>
    public class CheckedIndicesCollection : ReadOnlyCollection<int>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        internal CheckedIndicesCollection(IList<int> list) : base(list)
        {
        }

        /// <summary>
        /// Convert the checked indices to a comma-separated list
        /// </summary>
        /// <returns>Returns a string containing the indices separated by commas</returns>
        public override string ToString()
        {
            StringBuilder sb = new(1024);

            foreach(int index in this)
            {
                if(sb.Length > 0)
                    sb.Append(", ");

                sb.Append(index);
            }

            return sb.ToString();
        }
    }
}
