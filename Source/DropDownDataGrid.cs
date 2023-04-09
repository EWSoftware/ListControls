//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DropDownDataGrid.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/07/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This file contains a custom data grid control that provides some extra features needed by the multi-column
// combo box drop-down form.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/08/2005  EFW  Created the code
// 05/01/2006  EFW  Added support for the mouse tracking property
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is a custom grid control that provides some extra features needed by the multi-column combo box
    /// drop-down form.
    /// </summary>
    [ToolboxItem(false)]
    internal class DropDownDataGrid : DataGridView
    {
        #region Private data members
        //=====================================================================

        // This holds the last position of the moved mouse cursor
        private Point lastMousePos;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// Get or set the simple style flag
        /// </summary>
        /// <value>This is true when the combo box is using the simple rather than drop-down style</value>
        internal bool IsSimpleStyle { get; set; }

        /// <summary>
        /// Get or set the mouse tracking flag
        /// </summary>
        /// <value>This is true if the selection should track with the mouse in modes other than simple</value>
        internal bool MouseTracking { get; set; }

        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Highlight the item under the mouse when it moves
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if((lastMousePos.X != e.X || lastMousePos.Y != e.Y) && ((!this.IsSimpleStyle && this.MouseTracking) ||
              e.Button == MouseButtons.Left))
            {
                lastMousePos = new Point(e.X, e.Y);

                HitTestInfo hti = this.HitTest(e.X, e.Y);

                if(hti.Type == DataGridViewHitTestType.Cell)
                {
                    // Don't auto-scroll unless the left mouse button is down if out of view otherwise it tends
                    // to scroll too far too fast.
                    if(hti.RowIndex <= this.FirstDisplayedScrollingRowIndex + this.DisplayedRowCount(false) - 1 ||
                      e.Button == MouseButtons.Left)
                    {
                        this.SelectCell(hti.ColumnIndex, hti.RowIndex);
                    }
                }
                else
                {
                    // Auto-scroll if the left mouse button is down and we're outside the bounds
                    if(hti.Type == DataGridViewHitTestType.None && e.Button == MouseButtons.Left)
                    {
                        int newRow = this.CurrentCellAddress.Y;

                        // Scroll when out of bounds
                        if(e.Y < this.Top && newRow > 0)
                            newRow--;
                        else
                        {
                            if(e.Y > this.Bottom && newRow < this.RowCount - 1)
                                newRow++;
                        }

                        if(newRow != this.CurrentCellAddress.Y)
                            this.SelectCell(this.CurrentCellAddress.X, newRow);
                    }
                }
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Set mouse capture in the parent when we lose the mouse focus and the parent isn't using the
        /// <c>Simple</c> style.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if(!this.IsSimpleStyle && this.Parent.Visible)
                this.Parent.Capture = true;
        }

        /// <summary>
        /// This is used to select a specific cell in the grid and scroll it into view if necessary
        /// </summary>
        /// <param name="column">The column to select</param>
        /// <param name="row">The row to select</param>
        public void SelectCell(int column, int row)
        {
            if(column < 0 || row < 0 || column >= this.ColumnCount || row >= this.RowCount)
                return;

            try
            {
                if(!this.Rows[row].Selected)
                {
                    this.CurrentCell = this[column, row];
                    this.Rows[row].Selected = true;

                    if(row < this.FirstDisplayedScrollingRowIndex)
                        this.FirstDisplayedScrollingRowIndex = row;
                    else
                    {
                        if(row > this.FirstDisplayedScrollingRowIndex + this.DisplayedRowCount(false) - 1)
                            this.FirstDisplayedScrollingRowIndex += row - this.DisplayedRowCount(false) + 1;
                    }
                }
            }
            catch(InvalidOperationException)
            {
                // There's an odd problem with this getting called while CurrentCell is already being set which
                // causes an exception.  Ignore it in such cases.
            }
        }
        #endregion
    }
}
