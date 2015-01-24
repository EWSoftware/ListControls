//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ImageListCell.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/24/2014
// Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a data grid view cell object that shows images from an image list based on the index
// retrieved from the cell value.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 05/03/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.Drawing;
using System.Windows.Forms;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view cell type displays an image from an image list based on the index retrieved from the
    /// cell value.
    /// </summary>
    public class ImageListCell : BaseImageCell
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This gets the default value for a cell in the new row placeholder
        /// </summary>
        /// <value>Returns the image index from <see cref="NewRowImageIndex"/> to use for the new row cell or
        /// null if set to -1.</value>
        public override object DefaultNewRowValue
        {
            get
            {
                if(this.NewRowImageIndex != -1)
                    return this.NewRowImageIndex;

                return null;
            }
        }

        /// <summary>
        /// This is used to get or set the image index to show for the new row cells
        /// </summary>
        /// <value>If set to -1 (the default), new rows will show the <see cref="ImageListColumn.NullImage"/> if
        /// it has been set or a blank cell if not.  If set to a value other than -1, the image at the specified
        /// index will be shown in the new row.</value>
        public int NewRowImageIndex { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>The <see cref="ValueType"/> and <see cref="BaseImageCell.FormattedValueType"/> are both
        /// <see cref="Object"/>.  16-bit and 32-bit integer values are assumed to be actual index values unless
        /// mapped to a different index.  Boolean values are mapped to 0 (false) and 1 (true) unless mapped to a
        /// different index.  All other types must be mapped to an index using the
        /// <see cref="ImageListColumn.MapValueToIndex"/> event.</remarks>
        public ImageListCell()
        {
            this.NewRowImageIndex = -1;
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
            ImageListCell clone = (ImageListCell)base.Clone();
            clone.NewRowImageIndex = this.NewRowImageIndex;
            return clone;
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
            ImageListColumn owner;
            Rectangle r = base.GetContentBounds(graphics, cellStyle, rowIndex);
            Size cellSize, imageSize;

            // For null cells, use a default rectangle so that it enters edit mode if the middle area of the
            // empty cell is clicked.
            if(r == Rectangle.Empty)
            {
                TextFormatFlags flags = BaseDataGridViewCell.ComputeTextFormatFlagsForCellStyleAlignment(
                    base.DataGridView.RightToLeft == RightToLeft.Yes, cellStyle.Alignment, cellStyle.WrapMode);
                cellSize = base.GetSize(rowIndex);

                owner = base.OwningColumn as ImageListColumn;

                if(owner == null || owner.ImageList == null)
                    imageSize = cellSize;
                else
                    imageSize = owner.ImageList.ImageSize;

                r = new Rectangle(new Point(0, 0), cellSize);

                // When stretched, the image covers the whole cell so it doesn't need to be limited
                if(base.ImageLayout != DataGridViewImageCellLayout.Stretch)
                    r = new Rectangle(BaseDataGridViewCell.GetTextLocation(r, imageSize, flags, cellStyle),
                        imageSize);
            }

            return r;
        }

        /// <summary>
        /// Indicates whether the parent row is unshared if the user presses a key while the focus is on the cell
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        /// <returns>True if Space is hit and the Alt, Control, and Shift keys are not held down, otherwise
        /// false.</returns>
        protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return (e.KeyCode == Keys.Space && !e.Alt && !e.Control && !e.Shift);
        }

        /// <summary>
        /// Indicates whether the parent row is unshared if the user releases a key while the focus is on the
        /// cell.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        /// <returns>True if Space is released, otherwise false</returns>
        protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return (e.KeyCode == Keys.Space);
        }

        /// <summary>
        /// This is overridden to cycle through the image index values when the cell is clicked and the cell is
        /// editable.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnContentClick(DataGridViewCellEventArgs e)
        {
            ImageListColumn owner = base.OwningColumn as ImageListColumn;
            int cellValue = -1;
            object newValue = base.NewValue;

            if(!base.ReadOnly && base.DataGridView != null && owner != null && owner.ImageList != null)
            {
                if(newValue is int)
                    cellValue = (int)newValue;
                else
                    if(newValue is short)
                        cellValue = (short)newValue;
                    else
                        if(newValue is bool)
                            cellValue = ((bool)newValue) ? 1 : 0;
                        else
                            if(newValue == null || newValue == DBNull.Value)
                                cellValue = owner.NullImageIndex;

                // Let the user map the value to an index
                MapIndexEventArgs mapArgs = new MapIndexEventArgs(e.ColumnIndex, e.RowIndex, newValue, cellValue);
                owner.OnMapValueToIndex(mapArgs);
                cellValue = mapArgs.Index + 1;

                // Cycle back to zero?
                if(cellValue < 0 || cellValue >= owner.ImageList.Images.Count)
                    cellValue = 0;

                // If set to the null image index, set it to null.  Otherwise, use the value.
                if(cellValue != owner.NullImageIndex)
                {
                    // Let the user map the index back to a value
                    mapArgs.Index = cellValue;
                    mapArgs.Value = cellValue;
                    owner.OnMapIndexToValue(mapArgs);

                    // If the value changed, use that.  If the value didn't change, use the new index value but
                    // convert it to the matching type.
                    if(mapArgs.Value == null || !mapArgs.Value.Equals(cellValue))
                        base.NewValue = mapArgs.Value;
                    else
                        if(newValue is int)
                            base.NewValue = cellValue;
                        else
                            if(newValue is short)
                                base.NewValue = (short)cellValue;
                            else
                                if(newValue is bool)
                                {
                                    if(cellValue == 1)
                                        base.NewValue = true;
                                    else
                                        base.NewValue = false;
                                }
                                else
                                    base.NewValue = mapArgs.Value;
                }
                else
                    base.NewValue = null;

                base.DataGridView.NotifyCurrentCellDirty(true);
                base.DataGridView.InvalidateCell(this);
            }
        }

        /// <summary>
        /// This is overridden to handle Space key presses when editable
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            if(base.DataGridView != null && !base.ReadOnly && e.KeyCode == Keys.Space && !e.Alt && !e.Control &&
              !e.Shift)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// This is handled to raise the <see cref="DataGridView.CellClick"/> and <see cref="DataGridView.CellContentClick"/>
        /// events when editable and the Space key is released.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
        {
            if(base.DataGridView != null && !base.ReadOnly && e.KeyCode == Keys.Space && !e.Alt && !e.Control &&
              !e.Shift)
            {
                DataGridViewCellEventArgs args = new DataGridViewCellEventArgs(base.ColumnIndex, rowIndex);
                base.RaiseCellClick(args);

                if(base.ColumnIndex < base.DataGridView.Columns.Count && rowIndex < base.DataGridView.Rows.Count)
                    base.RaiseCellContentClick(args);

                e.Handled = true;
            }
        }

        /// <summary>
        /// Change the mouse pointer to a hand when the mouse moves over the image
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            ImageListColumn owner = base.OwningColumn as ImageListColumn;
            Rectangle content = base.GetContentBounds(e.RowIndex);
            Cursor newCursor;

            base.OnMouseMove(e);

            // Figure out if the mouse is actually over the image
            if(owner != null && owner.ImageList != null && !base.ReadOnly)
            {
                newCursor = owner.OriginalCursor;

                if(content.Contains(e.Location))
                    newCursor = Cursors.Hand;

                // Update the cursor if necessary
                if(newCursor != null && base.DataGridView != null && base.DataGridView.Cursor != newCursor)
                    base.DataGridView.Cursor = newCursor;
            }
        }

        /// <summary>
        /// Change the mouse cursor back to the default when leaving the cell
        /// </summary>
        /// <param name="rowIndex">The row index of the cell</param>
        protected override void OnMouseLeave(int rowIndex)
        {
            ImageListColumn owner = base.OwningColumn as ImageListColumn;

            if(owner != null && base.DataGridView.Cursor != owner.OriginalCursor)
                base.DataGridView.Cursor = owner.OriginalCursor;

            base.OnMouseLeave(rowIndex);
        }

        /// <summary>
        /// Gets the image to display in the cell
        /// </summary>
        /// <param name="value">The value to be use in determining the image</param>
        /// <param name="rowIndex">The index of the cell's parent row</param>
        /// <returns>The image that should be displayed in the cell</returns>
        protected override object GetCellImage(object value, int rowIndex)
        {
            // Use the image from the image list
            ImageListColumn owner = base.OwningColumn as ImageListColumn;
            Image cellImage = null;
            int cellValue = -1;

            if(owner != null && owner.ImageList != null)
            {
                if(value is int)
                    cellValue = (int)value;
                else
                    if(value is short)
                        cellValue = (short)value;
                    else
                        if(value is bool)
                            cellValue = ((bool)value) ? 1 : 0;

                // Let the user map the value to an index
                MapIndexEventArgs mapArgs = new MapIndexEventArgs(owner.Index, rowIndex, value, cellValue);
                owner.OnMapValueToIndex(mapArgs);
                cellValue = mapArgs.Index;

                if(cellValue < 0 || cellValue >= owner.ImageList.Images.Count)
                {
                    cellValue = owner.NullImageIndex;

                    if(cellValue >= owner.ImageList.Images.Count)
                        cellValue = -1;
                }

                if(cellValue != -1)
                    cellImage = owner.ImageList.Images[cellValue];
                else
                    cellImage = owner.NullImage;
            }

            return cellImage;
        }
        #endregion
    }
}
