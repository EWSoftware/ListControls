//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DataListCancelEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/30/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the event argument class used for various cancelable data list events (adding, deleting,
// etc.).
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
	/// This is used for various cancelable data list events (adding, deleting, etc.)
	/// </summary>
	public class DataListCancelEventArgs : DataListEventArgs
	{
        /// <summary>
        /// Get or set whether or not the event is canceled
        /// </summary>
        /// <remarks>Set this to true to cancel the event</remarks>
        public bool Cancel { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idx">The row index of the template item</param>
        /// <param name="templateItem">The item related to the event</param>
		public DataListCancelEventArgs(int idx, TemplateControl templateItem) : base(idx, templateItem)
		{
		}
	}
}
