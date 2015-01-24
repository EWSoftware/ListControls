//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DataListEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/30/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the event argument class used for various data list events (item bound, added, deleted,
// current, etc.).
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

namespace EWSoftware.ListControls
{
	/// <summary>
	/// This is used for various data list events (item bound, added, deleted, current, etc.).
	/// </summary>
	public class DataListEventArgs : EventArgs
	{
        /// <summary>
        /// Get the index of the row affected
        /// </summary>
        /// <remarks>This may be -1 if the item does not yet exist or if the item has been deleted.  Also be
        /// aware that this index value is for use with the control.  Due to additions and deletions, the given
        /// index may not refer to the same row indexed in the original data source.</remarks>
        public int Index { get; private set; }

        /// <summary>
        /// Get a reference to the item related to the event
        /// </summary>
        /// <remarks>This may be null if the item does not yet exist or if it has been deleted</remarks>
        public TemplateControl Item { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idx">The row index of the template item</param>
        /// <param name="templateItem">The item related to the event</param>
		public DataListEventArgs(int idx, TemplateControl templateItem)
		{
            this.Index = idx;
			this.Item = templateItem;
		}
	}
}
