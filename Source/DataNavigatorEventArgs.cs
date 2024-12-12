//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DataNavigatorEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains the event argument class used for various data navigator events (added, deleted, current,
// etc.)
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
	/// This is used for various data navigator events (added, deleted, current, etc.)
	/// </summary>
	public class DataNavigatorEventArgs : EventArgs
	{
        /// <summary>
        /// Get the index of the row affected
        /// </summary>
        /// <remarks>This may be -1 if the item does not yet exist or if the item has been deleted.  Also be
        /// aware that this index value is for use with the control.  Due to additions and deletions, the given
        /// index may not refer to the same row indexed in the original data source.</remarks>
        public int Index { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idx">The row index of the item</param>
		public DataNavigatorEventArgs(int idx)
		{
            this.Index = idx;
		}
	}
}
