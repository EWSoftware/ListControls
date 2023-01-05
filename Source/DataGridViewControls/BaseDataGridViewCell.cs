//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MultiColumnComboBoxCell.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2023
// Note    : Copyright 2007-2023, Eric Woodruff, All rights reserved
//
// This file contains a base data grid view cell class that contains various common properties and methods used
// by its derived classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/21/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.Drawing;
using System.Windows.Forms;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This is a base <see cref="DataGridViewCell"/> class that contains various common properties and methods
    /// used by its derived classes.
    /// </summary>
    public class BaseDataGridViewCell : DataGridViewCell
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to determine the first displayed column in the associated data grid view
        /// </summary>
        protected int FirstDisplayedColumnIndex
        {
            get
            {
                DataGridViewColumn col = base.DataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Visible);

                if(col == null)
                    return -1;

                if(col.Frozen)
                    return col.Index;

                return base.DataGridView.FirstDisplayedScrollingColumnIndex;
            }
        }

        /// <summary>
        /// This is used to determine the first displayed row in the associated data grid view
        /// </summary>
        protected int FirstDisplayedRowIndex
        {
            get
            {
                int rowIndex = base.DataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible);

                if(rowIndex != -1 && (base.DataGridView.Rows.GetRowState(rowIndex) &
                  DataGridViewElementStates.Frozen) == DataGridViewElementStates.None &&
                  base.DataGridView.FirstDisplayedScrollingRowIndex >= 0)
                    return base.DataGridView.FirstDisplayedScrollingRowIndex;

                return rowIndex;
            }
        }
        #endregion

        #region Static methods
        //=====================================================================

        /// <summary>
        /// This is used to compute the text formatting flags based on the cell style alignment settings
        /// </summary>
        /// <param name="rightToLeft">True if right to left formatting is in effect</param>
        /// <param name="alignment">The alignment</param>
        /// <param name="wrapMode">The wrapping mode</param>
        /// <returns>The text formatting flags to use</returns>
        protected internal static TextFormatFlags ComputeTextFormatFlagsForCellStyleAlignment(bool rightToLeft,
          DataGridViewContentAlignment alignment, DataGridViewTriState wrapMode)
        {
            TextFormatFlags flags;

            switch(alignment)
            {
                case DataGridViewContentAlignment.TopLeft:
                    flags = TextFormatFlags.GlyphOverhangPadding;

                    if(rightToLeft)
                        flags |= TextFormatFlags.Right;
                    break;

                case DataGridViewContentAlignment.TopCenter:
                    flags = TextFormatFlags.HorizontalCenter;
                    break;

                case DataGridViewContentAlignment.TopRight:
                    flags = TextFormatFlags.GlyphOverhangPadding;

                    if(!rightToLeft)
                        flags |= TextFormatFlags.Right;
                    break;

                case DataGridViewContentAlignment.MiddleLeft:
                    flags = TextFormatFlags.VerticalCenter;

                    if(rightToLeft)
                        flags |= TextFormatFlags.Right;
                    break;

                case DataGridViewContentAlignment.MiddleCenter:
                    flags = TextFormatFlags.VerticalCenter |
                        TextFormatFlags.HorizontalCenter;
                    break;

                case DataGridViewContentAlignment.BottomCenter:
                    flags = TextFormatFlags.Bottom |
                        TextFormatFlags.HorizontalCenter;
                    break;

                case DataGridViewContentAlignment.BottomRight:
                    flags = TextFormatFlags.Bottom;

                    if(!rightToLeft)
                        flags |= TextFormatFlags.Right;
                    break;

                case DataGridViewContentAlignment.MiddleRight:
                    flags = TextFormatFlags.VerticalCenter;

                    if(!rightToLeft)
                        flags |= TextFormatFlags.Right;
                    break;

                case DataGridViewContentAlignment.BottomLeft:
                    flags = TextFormatFlags.Bottom;

                    if(rightToLeft)
                        flags |= TextFormatFlags.Right;
                    break;

                default:
                    flags = TextFormatFlags.VerticalCenter |
                        TextFormatFlags.HorizontalCenter;
                    break;
            }

            if(wrapMode == DataGridViewTriState.False)
                flags |= TextFormatFlags.SingleLine;
            else
                flags |= TextFormatFlags.WordBreak;

            flags |= TextFormatFlags.NoPrefix |
                TextFormatFlags.PreserveGraphicsClipping;

            if(rightToLeft)
                flags |= TextFormatFlags.RightToLeft;

            return flags;
        }

        /// <summary>
        /// This is used to compute the text bounds for the cell based on the cell bounds, cell style, and the
        /// formatting flags in effect.
        /// </summary>
        /// <param name="cellBounds">The cell bounds</param>
        /// <param name="text">The cell text</param>
        /// <param name="flags">The text formatting flags</param>
        /// <param name="cellStyle">The cell style</param>
        /// <param name="font">The font</param>
        /// <returns>The bounds of the cell's text</returns>
        protected static Rectangle GetTextBounds(Rectangle cellBounds, string text, TextFormatFlags flags,
          DataGridViewCellStyle cellStyle, Font font)
        {
            if((flags & TextFormatFlags.SingleLine) != TextFormatFlags.GlyphOverhangPadding &&
              TextRenderer.MeasureText(text, font, new Size(Int32.MaxValue, Int32.MaxValue), flags).Width > cellBounds.Width)
                flags |= TextFormatFlags.EndEllipsis;

            Size proposedSize = new Size(cellBounds.Width, cellBounds.Height);
            Size size = TextRenderer.MeasureText(text, font, proposedSize, flags);

            if(size.Width > proposedSize.Width)
                size.Width = proposedSize.Width;

            if(size.Height > proposedSize.Height)
                size.Height = proposedSize.Height;

            if(size == proposedSize)
                return cellBounds;

            return new Rectangle(BaseDataGridViewCell.GetTextLocation(cellBounds, size, flags, cellStyle), size);
        }

        /// <summary>
        /// This is used to get the text location based on the text size, cell style, and formatting flags in
        /// effect.
        /// </summary>
        /// <param name="cellBounds">The cell bounds</param>
        /// <param name="sizeText">The text size</param>
        /// <param name="flags">The text formatting flags</param>
        /// <param name="cellStyle">The cell style</param>
        /// <returns>The location at which to draw the text</returns>
        protected internal static Point GetTextLocation(Rectangle cellBounds, Size sizeText,
          TextFormatFlags flags, DataGridViewCellStyle cellStyle)
        {
            if(cellStyle == null)
                throw new ArgumentNullException(nameof(cellStyle));

            Point point = new Point(0, 0);
            DataGridViewContentAlignment alignment = cellStyle.Alignment;

            if((flags & TextFormatFlags.RightToLeft) != TextFormatFlags.GlyphOverhangPadding)
                switch(alignment)
                {
                    case DataGridViewContentAlignment.MiddleRight:
                        alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;

                    case DataGridViewContentAlignment.BottomLeft:
                        alignment = DataGridViewContentAlignment.BottomRight;
                        break;

                    case DataGridViewContentAlignment.BottomRight:
                        alignment = DataGridViewContentAlignment.BottomLeft;
                        break;

                    case DataGridViewContentAlignment.TopLeft:
                        alignment = DataGridViewContentAlignment.TopRight;
                        break;

                    case DataGridViewContentAlignment.TopRight:
                        alignment = DataGridViewContentAlignment.TopLeft;
                        break;

                    case DataGridViewContentAlignment.MiddleLeft:
                        alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                }

            if(alignment <= DataGridViewContentAlignment.MiddleCenter)
            {
                switch(alignment)
                {
                    case DataGridViewContentAlignment.TopLeft:
                        point.X = cellBounds.X;
                        point.Y = cellBounds.Y;
                        return point;

                    case DataGridViewContentAlignment.TopCenter:
                        point.X = cellBounds.X + ((cellBounds.Width - sizeText.Width) / 2);
                        point.Y = cellBounds.Y;
                        return point;

                    case (DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.TopLeft):
                        return point;

                    case DataGridViewContentAlignment.TopRight:
                        point.X = cellBounds.Right - sizeText.Width;
                        point.Y = cellBounds.Y;
                        return point;

                    case DataGridViewContentAlignment.MiddleLeft:
                        point.X = cellBounds.X;
                        point.Y = cellBounds.Y + ((cellBounds.Height - sizeText.Height) / 2);
                        return point;

                    case DataGridViewContentAlignment.MiddleCenter:
                        point.X = cellBounds.X + ((cellBounds.Width - sizeText.Width) / 2);
                        point.Y = cellBounds.Y + ((cellBounds.Height - sizeText.Height) / 2);
                        return point;
                }

                return point;
            }

            if(alignment <= DataGridViewContentAlignment.BottomLeft)
            {
                switch(alignment)
                {
                    case DataGridViewContentAlignment.MiddleRight:
                        point.X = cellBounds.Right - sizeText.Width;
                        point.Y = cellBounds.Y + ((cellBounds.Height - sizeText.Height) / 2);
                        return point;

                    case DataGridViewContentAlignment.BottomLeft:
                        point.X = cellBounds.X;
                        point.Y = cellBounds.Bottom - sizeText.Height;
                        return point;
                }

                return point;
            }

            switch(alignment)
            {
                case DataGridViewContentAlignment.BottomCenter:
                    point.X = cellBounds.X + ((cellBounds.Width - sizeText.Width) / 2);
                    point.Y = cellBounds.Bottom - sizeText.Height;
                    return point;

                case DataGridViewContentAlignment.BottomRight:
                    point.X = cellBounds.Right - sizeText.Width;
                    point.Y = cellBounds.Bottom - sizeText.Height;
                    return point;
            }

            return point;
        }

        /// <summary>
        /// This is used to paint the cell's padding based on the current bounds and cell style
        /// </summary>
        /// <param name="g">The graphics object</param>
        /// <param name="bounds">The cell bounds</param>
        /// <param name="cellStyle">The cell style</param>
        /// <param name="br">The background brush</param>
        /// <param name="rightToLeft">The right-to-left setting currently in effect</param>
        protected static void PaintPadding(Graphics g, Rectangle bounds, DataGridViewCellStyle cellStyle,
          Brush br, bool rightToLeft)
        {
            Rectangle rect;

            if(g == null)
                throw new ArgumentNullException(nameof(g));

            if(cellStyle == null)
                throw new ArgumentNullException(nameof(cellStyle));

            if(rightToLeft)
            {
                rect = new Rectangle(bounds.X, bounds.Y, cellStyle.Padding.Right, bounds.Height);
                g.FillRectangle(br, rect);

                rect.X = bounds.Right - cellStyle.Padding.Left;
                rect.Width = cellStyle.Padding.Left;
                g.FillRectangle(br, rect);

                rect.X = bounds.Left + cellStyle.Padding.Right;
            }
            else
            {
                rect = new Rectangle(bounds.X, bounds.Y, cellStyle.Padding.Left, bounds.Height);
                g.FillRectangle(br, rect);

                rect.X = bounds.Right - cellStyle.Padding.Right;
                rect.Width = cellStyle.Padding.Right;
                g.FillRectangle(br, rect);

                rect.X = bounds.Left + cellStyle.Padding.Left;
            }

            rect.Y = bounds.Y;
            rect.Width = bounds.Width - cellStyle.Padding.Horizontal;
            rect.Height = cellStyle.Padding.Top;
            g.FillRectangle(br, rect);

            rect.Y = bounds.Bottom - cellStyle.Padding.Bottom;
            rect.Height = cellStyle.Padding.Bottom;
            g.FillRectangle(br, rect);
        }
        #endregion

        #region Instance methods
        //=====================================================================

        /// <summary>
        /// This is used to notify the data grid view of a change that may require it to resize the column and
        /// possibly the row.
        /// </summary>
        protected void OnCommonChange()
        {
            if(base.DataGridView != null && !base.DataGridView.IsDisposed && !base.DataGridView.Disposing)
                if(base.RowIndex == -1)
                    DataGridViewHelper.OnColumnCommonChange(base.DataGridView, base.ColumnIndex);
                else
                    DataGridViewHelper.OnCellCommonChange(base.DataGridView, base.ColumnIndex, base.RowIndex);
        }

        /// <summary>
        /// This is used to compute the cell's error icon bounds
        /// </summary>
        /// <param name="cellValueBounds">The cell value bounds</param>
        /// <returns>The bounds of the cell's error icon</returns>
        protected Rectangle ComputeErrorIconBounds(Rectangle cellValueBounds)
        {
            if(cellValueBounds.Width >= 20 && cellValueBounds.Height >= 19)
                return new Rectangle(base.DataGridView.RightToLeft == RightToLeft.Yes ?
                    (cellValueBounds.Left + 4) : cellValueBounds.Right - 16,
                    cellValueBounds.Y + ((cellValueBounds.Height - 11) / 2), 12, 11);

            return Rectangle.Empty;
        }
        #endregion
    }
}
