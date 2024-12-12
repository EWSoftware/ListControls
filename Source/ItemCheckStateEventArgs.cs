//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ItemCheckStateEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains the event argument class used to provide information about a <see cref="CheckBoxList"/>
// item that had a change in its check state.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/20/2005  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls
{
	/// <summary>
	/// This is used to provide information about a <see cref="CheckBoxList"/> item that had a change in its
    /// check state.
	/// </summary>
	public class ItemCheckStateEventArgs : EventArgs
	{
        /// <summary>
        /// Get the index of the item affected
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Get the check state of the item
        /// </summary>
        public CheckState CheckState { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idx">The index of the item</param>
        /// <param name="checkState">The check state of the item</param>
		public ItemCheckStateEventArgs(int idx, CheckState checkState)
		{
            this.Index = idx;
			this.CheckState = checkState;
		}
	}
}
