//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DataListBeginDragEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/30/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the event argument class used for the data list BeginDrag event
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
	/// This is used for the data list <see cref="DataList.BeginDrag"/> event
	/// </summary>
	public class DataListBeginDragEventArgs : EventArgs
	{
        /// <summary>
        /// Get the source data list for the drag and drop operation
        /// </summary>
        public DataList Source { get; private set; }

        /// <summary>
        /// Get the starting row of the selection in the data list when the drag operation started
        /// </summary>
        public int SelectionStart { get; private set; }

        /// <summary>
        /// Get the ending row of the selection in the data list when the drag operation started
        /// </summary>
        public int SelectionEnd { get; private set; }

        /// <summary>
        /// Get the mouse button clicked to start the drag operation
        /// </summary>
        public MouseButtons Button { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="source">The source data list for the drag and drop operation</param>
        /// <param name="start">The selection start</param>
        /// <param name="end">The selection end</param>
        /// <param name="mouseButtons">The mouse button used</param>
		public DataListBeginDragEventArgs(DataList source, int start, int end, MouseButtons mouseButtons)
		{
            this.Source = source;
            this.SelectionStart = start;
            this.SelectionEnd = end;
            this.Button = mouseButtons;
		}
	}
}
