//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MapIndicatorEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/27/2015
// Note    : Copyright 2007-2015, Eric Woodruff, All rights reserved
//
// This file contains an event arguments class used to map a cell value to a set of on/off states for drawing
// the indicator column cell value.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 05/30/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.Collections.Generic;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This is used to map a cell value to a set of on/off states for drawing an <see cref="IndicatorColumn"/>
    /// cell value.
    /// </summary>
    public class MapIndicatorEventArgs : EventArgs
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
        /// <value>This represents the current cell value and can be used to determine the image states to return
        /// in <see cref="ImageStates"/>.
        /// </value>
        public object Value { get; set; }

        /// <summary>
        /// The on/off states of each image index
        /// </summary>
        /// <value>This will contain one entry for each image in the image list.  By default, they are all false.
        /// Set an entry to true to draw the corresponding image index in the on/enable state.</value>
        public IList<bool> ImageStates { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnIndex">The column in which the event occurred</param>
        /// <param name="rowIndex">The row in which the event occurred</param>
        /// <param name="value">The cell value</param>
        /// <param name="states">An enumerable list of the image index states</param>
        public MapIndicatorEventArgs(int columnIndex, int rowIndex, object value, IList<bool> states)
        {
            this.Column = columnIndex;
            this.Row = rowIndex;
            this.Value = value;
            this.ImageStates = states;
        }
    }
}
