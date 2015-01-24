//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : BaseImageCell.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/22/2014
// Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains an abstract base data grid view image cell object that supports editing and contains
// common properties and methods used by its derived types.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 06/06/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This abstract base data grid view image cell object supports editing and contains common properties and
    /// methods used by its derived types.
    /// </summary>
    public abstract class BaseImageCell : DataGridViewImageCell, IDataGridViewEditingCell
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is overridden to return <see cref="Object"/> as the formatted value type
        /// </summary>
        public override Type FormattedValueType
        {
            get { return base.ValueType; }
        }

        /// <summary>
        /// This read-only property returns the original cell value prior to editing
        /// </summary>
        public object OriginalValue { get; private set; }

        /// <summary>
        /// This is used to set or get the edited cell value
        /// </summary>
        public object NewValue { get; set; }

        #endregion

        #region IDataGridViewEditingCell members
        //=====================================================================

        /// <summary>
        /// Gets or sets the formatted value of the cell
        /// </summary>
        public object EditingCellFormattedValue
        {
            get
            {
                object cellValue = null;

                if(base.RowIndex != -1)
                    if(base.IsInEditMode)
                        cellValue = this.NewValue;
                    else
                        cellValue = base.GetValue(base.RowIndex);

                return this.GetCellImage(cellValue, base.RowIndex);
            }
            set
            {
                this.OriginalValue = this.NewValue = base.GetValue(base.RowIndex);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the value of the cell has changed
        /// </summary>
        public bool EditingCellValueChanged
        {
            get { return !this.OriginalValue.Equals(this.NewValue); }
            set
            {
                if(!value)
                    this.NewValue = this.OriginalValue;
            }
        }

        /// <summary>
        /// Retrieves the formatted value of the cell
        /// </summary>
        /// <param name="context">A bitwise combination of <see cref="DataGridViewDataErrorContexts"/> values
        /// that specifies the context in which the data is needed.</param>
        /// <returns>An <see cref="Object"/> that represents the formatted version of the cell contents</returns>
        /// <remarks>If the context contains <c>ClipboardContent</c>, the underlying cell value is returned
        /// instead of the image.</remarks>
        public object GetEditingCellFormattedValue(DataGridViewDataErrorContexts context)
        {
            // Return clipboard representation?
            if((context & DataGridViewDataErrorContexts.ClipboardContent) != 0)
                return base.GetValue(base.RowIndex);

            return this.EditingCellFormattedValue;
        }

        /// <summary>
        /// For this cell type, this method does nothing
        /// </summary>
        /// <param name="selectAll">Ignored as this method does nothing</param>
        public void PrepareEditingCellForEdit(bool selectAll)
        {
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>The <see cref="ValueType"/> and <see cref="BaseImageCell.FormattedValueType"/> are both
        /// <see cref="Object"/>.  This allows the bound cell value to be edited.</remarks>
        protected BaseImageCell()
        {
            base.ValueType = typeof(object);
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to paint the cell value based on whether or not it is in edit mode
        /// </summary>
        /// <param name="graphics">The graphics object</param>
        /// <param name="clipBounds">The clip bounds</param>
        /// <param name="cellBounds">The cell bounds</param>
        /// <param name="rowIndex">The row index of the cell</param>
        /// <param name="elementState">The cell state</param>
        /// <param name="value">The value of the cell</param>
        /// <param name="formattedValue">The formatted value for the cell</param>
        /// <param name="errorText">The error text associated with the cell, if any</param>
        /// <param name="cellStyle">The cell style</param>
        /// <param name="advancedBorderStyle">The advanced border style</param>
        /// <param name="paintParts">The parts of the cell to paint</param>
        /// <remarks>This also fixes a bug in the base image cell class by painting the cell background when
        /// <see cref="ImageLayout"/> is set to <c>Stretch</c>.</remarks>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds,
          int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue,
          string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
          DataGridViewPaintParts paintParts)
        {
            object cellImage;

            if(base.RowIndex != -1 && base.IsInEditMode && base.DataGridView.IsCurrentCellDirty)
                cellImage = this.GetCellImage(this.NewValue, rowIndex);
            else
                cellImage = this.GetCellImage(value, rowIndex);

            if(base.ImageLayout == DataGridViewImageCellLayout.Stretch)
            {
                SolidBrush br = DataGridViewHelper.GetCachedBrush(base.DataGridView,
                    ((paintParts & DataGridViewPaintParts.SelectionBackground) != 0 &&
                    (elementState & DataGridViewElementStates.Selected) != DataGridViewElementStates.None) ?
                        cellStyle.SelectionBackColor : cellStyle.BackColor);

                if((paintParts & DataGridViewPaintParts.Background) != 0 && br.Color.A == 0xFF)
                    graphics.FillRectangle(br, cellBounds);
            }


            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, cellImage, errorText,
                cellStyle, advancedBorderStyle, paintParts);
        }

        /// <summary>
        /// Gets the formatted value of the cell's data
        /// </summary>
        /// <param name="value">The value to be formatted.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="valueTypeConverter">A <see cref="TypeConverter"/> associated with the value type that
        /// provides custom conversion to the formatted value type, or null if no such custom conversion is
        /// needed.</param>
        /// <param name="formattedValueTypeConverter">A <see cref="TypeConverter"/> associated with the formatted
        /// value type that provides custom conversion from the value type, or null if no such custom conversion
        /// is needed.</param>
        /// <param name="context">A bitwise combination of <see cref="DataGridViewDataErrorContexts"/> values
        /// describing the context in which the formatted value is needed.</param>
        /// <returns>The value of the cell's data after formatting has been applied or null if the cell is not
        /// part of a <see cref="DataGridView"/> control.</returns>
        protected override object GetFormattedValue(object value, int rowIndex,
          ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter,
          TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if(base.RowIndex != -1 && base.IsInEditMode && base.DataGridView.IsCurrentCellDirty)
                return this.NewValue;

            return value;
        }

        /// <summary>
        /// This is overridden to return the appropriate value for the cell when it is being edited
        /// </summary>
        /// <param name="formattedValue">The display value of the cell.</param>
        /// <param name="cellStyle">The cell style in effect.</param>
        /// <param name="formattedValueTypeConverter">A <see cref="TypeConverter"/> for the display value type,
        /// or null to use the default converter.</param>
        /// <param name="valueTypeConverter">A <see cref="TypeConverter"/> for the cell value type, or null to
        /// use the default converter.</param>
        /// <returns>The cell value.</returns>
        public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle,
          TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            return this.NewValue;
        }

        /// <summary>
        /// Indicates whether the cell's row will be unshared when the cell's content is clicked
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <returns>True if this is the current cell and it is in edit mode, otherwise false</returns>
        protected override bool ContentClickUnsharesRow(DataGridViewCellEventArgs e)
        {
            Point currentCell = base.DataGridView.CurrentCellAddress;

            if(currentCell.X == base.ColumnIndex && currentCell.Y == e.RowIndex)
                return base.DataGridView.IsCurrentCellInEditMode;

            return false;
        }

        /// <summary>
        /// Indicates whether the cell's row will be unshared when the cell's content is double-clicked
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <returns>True if this is the current cell and it is in edit mode, otherwise false</returns>
        protected override bool ContentDoubleClickUnsharesRow(DataGridViewCellEventArgs e)
        {
            return this.ContentClickUnsharesRow(e);
        }

        /// <summary>
        /// Indicates whether a row will be unshared when the user holds down a mouse button while the pointer is
        /// on a cell in the row.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <returns>True if the left mouse button is pressed, otherwise false</returns>
        protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
        {
            return (e.Button == MouseButtons.Left);
        }

        /// <summary>
        /// Indicates whether a row will be unshared when the user releases a mouse button while the pointer is
        /// on a cell in the row.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <returns>True if the left mouse button is pressed, otherwise false</returns>
        protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
        {
            return (e.Button == MouseButtons.Left);
        }

        /// <summary>
        /// Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row
        /// </summary>
        /// <param name="rowIndex">The index of the row containing the cell</param>
        /// <returns>True if the mouse is in the cell in which the mouse button was pressed, otherwise false</returns>
        protected override bool MouseEnterUnsharesRow(int rowIndex)
        {
            Point cell = DataGridViewHelper.MouseDownCellAddress(base.DataGridView);

            return (base.ColumnIndex == cell.X && rowIndex == cell.Y);
        }

        /// <summary>
        /// Gets the image to display in the cell
        /// </summary>
        /// <param name="value">The value to be use in determining the image</param>
        /// <param name="rowIndex">The index of the cell's parent row</param>
        /// <returns>The image that should be displayed in the cell</returns>
        protected abstract object GetCellImage(object value, int rowIndex);

        #endregion
    }
}
