//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MapIndexEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/24/2014
// Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
//
// This file contains an event arguments class used to map a cell value to an image list index and vice versa
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 05/04/2007  EFW  Created the code
//===============================================================================================================

using System;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This is used to map a cell value to an image list index and vice versa for the <see cref="ImageListColumn"/>
    /// </summary>
    public class MapIndexEventArgs : EventArgs
    {
        /// <summary>
        /// This read-only property can be used to retrieve the column in which the event occurred
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// This read-only property can be used to retrieve the row in which the event occurred
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// The cell value related to the event
        /// </summary>
        /// <value>For the <see cref="ImageListColumn.MapValueToIndex"/> event, this is used to pass the value
        /// that needs mapping to an image index.  For the <see cref="ImageListColumn.MapIndexToValue"/> event,
        /// it is used to return the value that should be stored in the cell for the specified
        /// <see cref="Index"/>.</value>
        public object Value { get; set; }

        /// <summary>
        /// The image list index related to the event
        /// </summary>
        /// <value>For the <see cref="ImageListColumn.MapIndexToValue"/> event, this is used to pass the image
        /// index that needs mapping to a cell value.  For the <see cref="ImageListColumn.MapValueToIndex"/>
        /// event, it is used to return the image index that should be shown in the cell for the specified
        /// <see cref="Value"/>.</value>
        public int Index { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnIndex">The column in which the event occurred</param>
        /// <param name="rowIndex">The row in which the event occurred</param>
        /// <param name="value">The cell value</param>
        /// <param name="imageIndex">The image index</param>
        public MapIndexEventArgs(int columnIndex, int rowIndex, object value, int imageIndex)
        {
            this.Column = columnIndex;
            this.Row = rowIndex;
            this.Value = value;
            this.Index = imageIndex;
        }
    }
}
