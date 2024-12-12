//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MapRatingEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This file contains an event arguments class used to map a cell value to a rating value and vice versa
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 06/05/2007  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This is used to map a cell value to a rating value and vice versa for the <see cref="RatingColumn"/>
    /// </summary>
    public class MapRatingEventArgs : EventArgs
    {
        /// <summary>
        /// This read-only property can be used to retrieve the column in which the event occurred
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// This read-only property can be used to retrieve the row in which the event occurred
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// The cell value related to the event
        /// </summary>
        /// <value>For the <see cref="RatingColumn.MapValueToRating"/> event, this is used to pass the value that
        /// needs mapping to a rating.  For the <see cref="RatingColumn.MapRatingToValue"/> event, it is used to
        /// return the value that should be stored in the cell for the specified <see cref="Rating"/>.</value>
        public object? Value { get; set; }

        /// <summary>
        /// The rating related to the event
        /// </summary>
        /// <value>For the <see cref="RatingColumn.MapRatingToValue"/> event, this is used to pass the rating
        /// that needs mapping to a cell value.  For the <see cref="RatingColumn.MapValueToRating"/> event, it is
        /// used to return the rating that should be shown in the cell for the specified <see cref="Value"/>.</value>
        public int Rating { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnIndex">The column in which the event occurred</param>
        /// <param name="rowIndex">The row in which the event occurred</param>
        /// <param name="cellValue">The cell value</param>
        /// <param name="ratingValue">The rating value</param>
        public MapRatingEventArgs(int columnIndex, int rowIndex, object? cellValue, int ratingValue)
        {
            this.Column = columnIndex;
            this.Row = rowIndex;
            this.Value = cellValue;
            this.Rating = ratingValue;
        }
    }
}
