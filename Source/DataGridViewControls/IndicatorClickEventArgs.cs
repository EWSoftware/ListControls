//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : IndicatorClickEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/09/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This file contains an event arguments class used to provide information about an image index that was clicked
// in an indicator column.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 05/31/2007  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This is used provide information about the image index that was clicked in an <see cref="IndicatorColumn"/>
    /// cell.
    /// </summary>
    public class IndicatorClickEventArgs : EventArgs
    {
        /// <summary>
        /// This read-only property can be used to retrieve the column in which the event occurred.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// This read-only property can be used to retrieve the row in which the event occurred
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// This read-only property can be used to retrieve the index of the image that was clicked in the cell
        /// </summary>
        public int ImageIndex { get; }

        /// <summary>
        /// This can be used to get the cell value related to the event.  It can also be used to pass back a new
        /// cell value if the cell is editable.
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnIndex">The column in which the event occurred</param>
        /// <param name="rowIndex">The row in which the event occurred</param>
        /// <param name="clickedIndex">The image index that was clicked</param>
        /// <param name="value">The cell value</param>
        public IndicatorClickEventArgs(int columnIndex, int rowIndex, int clickedIndex, object? value)
        {
            this.Column = columnIndex;
            this.Row = rowIndex;
            this.ImageIndex = clickedIndex;
            this.Value = value;
        }
    }
}
