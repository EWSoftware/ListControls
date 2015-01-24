//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ItemCheckStateEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/30/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
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

using System;
using System.Windows.Forms;

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
        public int Index { get; private set; }

        /// <summary>
        /// Get the check state of the item
        /// </summary>
        public CheckState CheckState { get; private set; }

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
