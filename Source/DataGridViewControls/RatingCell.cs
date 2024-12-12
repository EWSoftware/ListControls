//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : RatingCell.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This file contains a data grid view cell object that shows a set of images (stars by default) that represent
// a rating similar to the one found in Windows Media Player.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 06/04/2007  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view cell type shows a set of images (stars by default) that represent a rating similar
    /// to the one found in Windows Media Player.
    /// </summary>
    public class RatingCell : BaseImageCell
    {
        #region Private data members
        //=====================================================================

        private int keyRating;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This read-only property returns the rating currently under the mouse
        /// </summary>
        /// <value>Returns -1 if the mouse is not over a rating or the rating value</value>
        public int MouseRating { get; private set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>The <see cref="ValueType"/> and <see cref="BaseImageCell.FormattedValueType"/> are both
        /// <see cref="Object"/>.  16-bit and 32-bit integer values are assumed to be actual rating values
        /// between zero and <see cref="RatingColumn.MaximumRating"/> unless mapped to a different rating value.
        /// All other types must be mapped to a rating using the <see cref="RatingColumn.MapValueToRating"/>
        /// event.</remarks>
        public RatingCell()
        {
            this.MouseRating = keyRating = -1;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Clone the cell
        /// </summary>
        /// <returns>A clone of the cell</returns>
        public override object Clone()
        {
            return (RatingCell)base.Clone();
        }

        /// <summary>
        /// Calculates the preferred size, in pixels, of the cell
        /// </summary>
        /// <param name="graphics">The graphics context for the cell</param>
        /// <param name="cellStyle">The cell style</param>
        /// <param name="rowIndex">The index of the cell's parent row</param>
        /// <param name="constraintSize">The cell's maximum allowable size</param>
        /// <returns>The preferred cell size in pixels</returns>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle,
          int rowIndex, Size constraintSize)
        {
            Size size;
            int widthOffset, heightOffset, freeDimension;

            if(this.DataGridView == null || this.OwningColumn == null)
                return new Size(-1, -1);

            if(cellStyle == null)
                throw new ArgumentNullException(nameof(cellStyle));

            if(constraintSize.Width == 0)
            {
                if(constraintSize.Height == 0)
                    freeDimension = 0;      // Both free
                else
                    freeDimension = 2;      // Width is free
            }
            else
                freeDimension = 1;          // Height is free

            DataGridViewAdvancedBorderStyle borderStylePlaceholder = new();
            DataGridViewAdvancedBorderStyle advancedBorderStyle = base.AdjustCellBorderStyle(
                this.DataGridView.AdvancedCellBorderStyle, borderStylePlaceholder, false, false, false, false);
            Rectangle borderWidths = base.BorderWidths(advancedBorderStyle);

            widthOffset = (borderWidths.Left + borderWidths.Width) + cellStyle.Padding.Horizontal;
            heightOffset = (borderWidths.Top + borderWidths.Height) + cellStyle.Padding.Vertical;

            var owner = (RatingColumn)this.OwningColumn;

            size = new Size(owner.ImageListInternal.ImageSize.Width * owner.MaximumRating,
                owner.ImageListInternal.ImageSize.Height);

            switch(freeDimension)
            {
                case 1:
                    size.Width = 0;
                    break;

                case 2:
                    size.Height = 0;
                    break;
            }

            if(freeDimension != 1)
            {
                size.Width += widthOffset;

                if(this.DataGridView.ShowCellErrors)
                    size.Width = Math.Max(size.Width, widthOffset + 16);
            }

            if(freeDimension != 2)
            {
                size.Height += heightOffset;

                if(this.DataGridView.ShowCellErrors)
                    size.Height = Math.Max(size.Height, heightOffset + 16);
            }

            return size;
        }

        /// <summary>
        /// Returns the bounding rectangle that encloses the cell's content area, which is calculated using the
        /// specified graphics context and cell style.
        /// </summary>
        /// <param name="graphics">The graphics context</param>
        /// <param name="cellStyle">The cell style to apply to the cell</param>
        /// <param name="rowIndex">The index of the cell's parent row</param>
        /// <returns>The <see cref="Rectangle"/> that bounds the cell's contents</returns>
        /// <remarks>For null cells, a default rectangle is returned so that mouse clicks near its center will
        /// initiate editing.</remarks>
        protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle,
          int rowIndex)
        {
            Rectangle r;

            Size cellSize = base.GetSize(rowIndex);

            if(this.OwningColumn is not RatingColumn owner)
                r = Rectangle.Empty;
            else
            {
                Size imageSize = owner.ImageListInternal.ImageSize;
                int width = imageSize.Height * owner.MaximumRating;
                int height = imageSize.Height;

                r = new Rectangle((cellSize.Width - width) / 2, (cellSize.Height - height) / 2, width, height);
            }

            return r;
        }

        /// <summary>
        /// Indicates whether the parent row is unshared if the user presses a key while the focus is on the cell
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        /// <returns>True if a digit 0 through 9 or + or - is hit and the Alt, Control, and Shift keys are not
        /// held down, otherwise false.</returns>
        protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return e != null && ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || e.KeyCode == Keys.Add ||
                e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.OemMinus) &&
                !e.Alt && !e.Control && (!e.Shift || (e.Shift && e.KeyCode == Keys.Oemplus));
        }

        /// <summary>
        /// Indicates whether the parent row is unshared if the user releases a key while the focus is on the
        /// cell.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        /// <returns>True if a digit 0 through 9 or + or - is released, otherwise false</returns>
        protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return e != null && ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || e.KeyCode == Keys.Add ||
                e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.OemMinus);
        }

        /// <summary>
        /// This is overridden to update the cell value with a new rating value when it is clicked
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnContentClick(DataGridViewCellEventArgs e)
        {
            int rating;
            object? newValue = this.NewValue;

            if(keyRating != -1)
                rating = keyRating;
            else
            {
                if(this.MouseRating != -1)
                    rating = this.MouseRating + 1;
                else
                    rating = -1;
            }

            if(e != null && !this.ReadOnly && this.DataGridView != null &&
              this.OwningColumn is RatingColumn owner && rating != -1 && rating <= owner.MaximumRating)
            {
                // Let the user map the rating to a cell value
                MapRatingEventArgs mapArgs = new(e.ColumnIndex, e.RowIndex, rating, rating);
                owner.OnMapRatingToValue(mapArgs);

                // If the value changed, use that.  If the value didn't change, use the new index value but
                // convert it to the matching type.
                if(mapArgs.Value == null || !mapArgs.Value.Equals(rating))
                    this.NewValue = mapArgs.Value;
                else
                    if(newValue is int)
                        this.NewValue = rating;
                    else
                        if(newValue is short)
                            this.NewValue = (short)rating;
                        else
                            this.NewValue = mapArgs.Value;

                this.DataGridView.NotifyCurrentCellDirty(true);
                this.DataGridView.InvalidateCell(this);
            }
        }

        /// <summary>
        /// This is overridden to handle digit 0 through 9 and +/- key presses
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            if(e != null && this.DataGridView != null && ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
              e.KeyCode == Keys.Add || e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Oemplus ||
              e.KeyCode == Keys.OemMinus) && !e.Alt && !e.Control &&
              (!e.Shift || (e.Shift && e.KeyCode == Keys.Oemplus)))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// This is handled to raise the <see cref="DataGridView.CellClick"/> and
        /// <see cref="DataGridView.CellContentClick"/> events when one of the digit keys 0 thorough 9
        /// or + or - is released.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        /// <remarks>Digits 0 through 9 can be used to set a rating in that range.  The plus (+) and minus (-)
        /// keys can be used to adjust the current rating up or down by one.</remarks>
        protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
        {
            object? cellValue;

            if(e != null && this.DataGridView != null && ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
              e.KeyCode == Keys.Add || e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Oemplus ||
              e.KeyCode == Keys.OemMinus) && !e.Alt && !e.Control &&
              (!e.Shift || (e.Shift && e.KeyCode == Keys.Oemplus)))
            {
                if(e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                    keyRating = (int)(e.KeyCode - Keys.D0);
                else
                {
                    if(this.IsInEditMode)
                        cellValue = this.NewValue;
                    else
                        cellValue = this.GetValue(rowIndex);

                    if(cellValue is int iv)
                        keyRating = iv;
                    else
                    {
                        if(cellValue is short sv)
                            keyRating = sv;
                    }

                    // Let the user map the value to a rating
                    MapRatingEventArgs mapArgs = new(this.ColumnIndex, rowIndex, cellValue, keyRating);
                    ((RatingColumn)this.OwningColumn).OnMapValueToRating(mapArgs);

                    if(e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
                        keyRating = mapArgs.Rating + 1;
                    else
                        keyRating = mapArgs.Rating - 1;
                }

                DataGridViewCellEventArgs args = new(this.ColumnIndex, rowIndex);
                this.RaiseCellClick(args);

                if(this.ColumnIndex < this.DataGridView.Columns.Count && rowIndex < this.DataGridView.Rows.Count)
                    this.RaiseCellContentClick(args);

                e.Handled = true;
                keyRating = -1;
            }
        }

        /// <summary>
        /// Change the mouse pointer to a hand when the mouse moves over one of the images
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            Rectangle content = this.GetContentBounds(e.RowIndex);
            int index, width, lastMouseRating = this.MouseRating;

            base.OnMouseMove(e);

            this.MouseRating = -1;

            // Figure out if the mouse is actually over an image
            if(this.OwningColumn is RatingColumn owner && !this.ReadOnly)
            {
                Cursor newCursor = owner.OriginalCursor;

                if(content.Contains(e.Location))
                {
                    width = owner.ImageListInternal.ImageSize.Width;
                    index = (e.Location.X - content.X) / width;

                    if(e.Location.X - content.X - (index * width) < width)
                    {
                        newCursor = Cursors.Hand;
                        this.MouseRating = index;
                    }
                }

                // Update the cursor if necessary and redraw the cell
                if(this.DataGridView != null)
                {
                    if(newCursor != null && this.DataGridView.Cursor != newCursor)
                        this.DataGridView.Cursor = newCursor;

                    if(this.MouseRating != lastMouseRating)
                        this.DataGridView.InvalidateCell(e.ColumnIndex, e.RowIndex);
                }
            }
        }

        /// <summary>
        /// Change the mouse cursor back to the default when leaving the cell
        /// </summary>
        /// <param name="rowIndex">The row index of the cell</param>
        protected override void OnMouseLeave(int rowIndex)
        {
            if(!this.ReadOnly && this.OwningColumn is RatingColumn owner && this.DataGridView!.Cursor != owner.OriginalCursor)
                this.DataGridView.Cursor = owner.OriginalCursor;

            this.MouseRating = -1;
            
            base.OnMouseLeave(rowIndex);
            
            this.DataGridView!.InvalidateCell(this.ColumnIndex, rowIndex);
        }

        /// <summary>
        /// Gets the image to display in the cell
        /// </summary>
        /// <param name="value">The value to be use in determining the image</param>
        /// <param name="rowIndex">The index of the cell's parent row</param>
        /// <returns>The image that should be displayed in the cell</returns>
        protected override object? GetCellImage(object? value, int rowIndex)
        {
            // If this is a shared cell, we don't want to draw hot images as they may end up in several places.
            // As such, only draw them if the mouse is in the cell being drawn.
            Point mouseCell = DataGridViewHelper.MouseEnteredCellAddress(this.DataGridView!);

            if(this.OwningColumn is RatingColumn owner)
            {
                return owner.DrawImage(value, rowIndex,
                    (mouseCell.X == owner.Index && mouseCell.Y == rowIndex) ? this.MouseRating : -1);
            }

            return null;
        }
        #endregion
    }
}
