//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : IndicatorCell.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/09/2024
// Note    : Copyright 2014-2024, Eric Woodruff, All rights reserved
//
// This file contains a data grid view cell object that shows images from an image list in an on/off state based
// on the cell value.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 05/11/2007  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view cell type displays images from an image list in an on/off state based on the cell
    /// value.
    /// </summary>
    public class IndicatorCell : BaseImageCell
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get or set whether the images are clickable and will raise the
        /// <see cref="IndicatorColumn.IndicatorClicked"/> event.
        /// </summary>
        /// <value>If true, the default, the images will raise the <see cref="IndicatorColumn.IndicatorClicked"/>
        /// event and will display the hand cursor as the mouse moves over each image.  If false, the event is
        /// not raised when an image is clicked and the mouse cursor is not changed.</value>
        public bool IsClickable { get; set; }

        /// <summary>
        /// This read-only property returns the index of the image currently under the mouse
        /// </summary>
        /// <value>Returns -1 if the mouse is not over an image or the index of the image</value>
        public int MouseImageIndex { get; private set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <remarks>The <see cref="ValueType"/> and <see cref="BaseImageCell.FormattedValueType"/> are both
        /// <see cref="Object"/>.  The bits in a 16-bit or 32-bit integer value can be used as the on/off states
        /// for each image in the image list.</remarks>
        public IndicatorCell()
        {
            this.MouseImageIndex = -1;
            this.IsClickable = true;
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
            IndicatorCell clone = (IndicatorCell)base.Clone();
            clone.IsClickable = this.IsClickable;

            return clone;
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
            DataGridViewAdvancedBorderStyle advancedBorderStyle = this.AdjustCellBorderStyle(
                this.DataGridView.AdvancedCellBorderStyle, borderStylePlaceholder, false, false, false, false);
            Rectangle borderWidths = this.BorderWidths(advancedBorderStyle);

            widthOffset = (borderWidths.Left + borderWidths.Width) + cellStyle.Padding.Horizontal;
            heightOffset = (borderWidths.Top + borderWidths.Height) + cellStyle.Padding.Vertical;

            IndicatorColumn owner = (IndicatorColumn)this.OwningColumn;

            if(owner.ImageList == null)
                size = new Size(16, 16);
            else
                size = new Size((owner.ImageList.ImageSize.Width + owner.ImageSpacing) *
                    owner.ImageList.Images.Count, owner.ImageList.ImageSize.Height);

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
                size.Width += widthOffset + owner.ImageSpacing;

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
            Size cellSize, imageSize;
            int width, height;

            cellSize = base.GetSize(rowIndex);

            if(this.OwningColumn is not IndicatorColumn owner || owner.ImageList == null)
                r = Rectangle.Empty;
            else
            {
                imageSize = owner.ImageList.ImageSize;
                width = ((imageSize.Height + owner.ImageSpacing) * owner.ImageList.Images.Count) - owner.ImageSpacing;
                height = imageSize.Height;

                r = new Rectangle((cellSize.Width - width) / 2, (cellSize.Height - height) / 2, width, height);
            }

            return r;
        }

        /// <summary>
        /// Indicates whether the parent row is unshared if the user presses a key while the focus is on the cell
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        /// <returns>True if a digit 0 through 9 is hit and the Alt, Control, and Shift keys are not held down,
        /// otherwise false.</returns>
        protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return (e != null && e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !e.Alt && !e.Control && !e.Shift);
        }

        /// <summary>
        /// Indicates whether the parent row is unshared if the user releases a key while the focus is on the
        /// cell.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        /// <returns>True if a digit 0 through 9 is released, otherwise false</returns>
        protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
        {
            return (e != null && e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9);
        }

        /// <summary>
        /// This is overridden to raise the <see cref="IndicatorColumn.IndicatorClicked"/> event when an image
        /// is clicked.  The event is not raised if <see cref="IsClickable"/> is set to false.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnContentClick(DataGridViewCellEventArgs e)
        {
            if(e != null && this.IsClickable && this.DataGridView != null && this.OwningColumn is IndicatorColumn owner &&
              this.MouseImageIndex != -1 && this.MouseImageIndex < (owner.ImageList?.Images.Count ?? 0))
            {
                IndicatorClickEventArgs clickArgs = new(e.ColumnIndex, e.RowIndex, this.MouseImageIndex, this.NewValue);

                owner.OnIndicatorClicked(clickArgs);

                if(!base.ReadOnly && ((clickArgs.Value == null && this.NewValue != null) ||
                  (clickArgs.Value != null && !clickArgs.Value.Equals(this.NewValue))))
                {
                    this.NewValue = clickArgs.Value;
                    this.DataGridView.NotifyCurrentCellDirty(true);
                    this.DataGridView.InvalidateCell(this);
                }
            }
        }

        /// <summary>
        /// This is overridden to handle digit 0 through 9 key presses
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            if(e != null)
            {
                e.Handled = this.DataGridView != null && e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 &&
                    !e.Alt && !e.Control && !e.Shift;
            }
        }

        /// <summary>
        /// This is handled to raise the <see cref="DataGridView.CellClick"/> and
        /// <see cref="DataGridView.CellContentClick"/> events when one of the digit keys 0 thorough 9 is
        /// released.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        /// <remarks>To be more intuitive, one through nine equate to the first nine images and zero equates to
        /// the tenth image.</remarks>
        protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
        {
            if(e != null && this.DataGridView != null && e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !e.Alt &&
              !e.Control && !e.Shift)
            {
                this.MouseImageIndex = e.KeyCode - Keys.D0;

                if(this.MouseImageIndex == 0)
                    this.MouseImageIndex = 10;
                else
                    this.MouseImageIndex--;

                DataGridViewCellEventArgs args = new(this.ColumnIndex, rowIndex);
                this.RaiseCellClick(args);

                if(this.ColumnIndex < this.DataGridView.Columns.Count && rowIndex < this.DataGridView.Rows.Count)
                    this.RaiseCellContentClick(args);

                e.Handled = true;
                this.MouseImageIndex = -1;
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
            Cursor? newCursor = null;

            int index, width, spacing, lastMouseIndex = this.MouseImageIndex;

            base.OnMouseMove(e);

            this.MouseImageIndex = -1;

            // Figure out if the mouse is actually over an image
            if(this.OwningColumn is IndicatorColumn owner && owner.ImageList != null)
            {
                newCursor = owner.OriginalCursor;

                if(content.Contains(e.Location))
                {
                    width = owner.ImageList.ImageSize.Width;
                    spacing = owner.ImageSpacing;
                    index = (e.Location.X - content.X) / (width + spacing);

                    if(e.Location.X - content.X - (index * (width + spacing)) < width)
                    {
                        newCursor = Cursors.Hand;
                        this.MouseImageIndex = index;
                    }
                }
            }

            // Update the cursor and the cell tool tip if necessary
            if(this.DataGridView != null)
            {
                if(this.IsClickable && newCursor != null && this.DataGridView.Cursor != newCursor)
                    this.DataGridView.Cursor = newCursor;

                if(this.MouseImageIndex != lastMouseIndex)
                {
                    if(this.MouseImageIndex != -1)
                    {
                        DataGridViewHelper.ActivateToolTip(this.DataGridView, true, this.ToolTipText,
                            e.ColumnIndex, e.RowIndex);
                    }
                    else
                        DataGridViewHelper.ActivateToolTip(this.DataGridView, false, null, e.ColumnIndex, e.RowIndex);
                }
            }
        }

        /// <summary>
        /// Change the mouse cursor back to the default when leaving the cell
        /// </summary>
        /// <param name="rowIndex">The row index of the cell</param>
        protected override void OnMouseLeave(int rowIndex)
        {
            if(this.IsClickable && this.OwningColumn is IndicatorColumn owner &&
              this.DataGridView!.Cursor != owner.OriginalCursor)
            {
                this.DataGridView.Cursor = owner.OriginalCursor;
            }

            this.MouseImageIndex = -1;

            base.OnMouseLeave(rowIndex);
        }

        /// <summary>
        /// Gets the image to display in the cell
        /// </summary>
        /// <param name="value">The value to be use in determining the image</param>
        /// <param name="rowIndex">The index of the cell's parent row</param>
        /// <returns>The image that should be displayed in the cell</returns>
        protected override object? GetCellImage(object? value, int rowIndex)
        {
            if(this.OwningColumn is IndicatorColumn owner && owner.ImageList != null)
                return owner.DrawImage(value, rowIndex);

            return null;
        }
        #endregion
    }
}
