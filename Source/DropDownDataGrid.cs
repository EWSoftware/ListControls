//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DropDownDataGrid.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/16/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
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
    internal class DropDownDataGrid : System.Windows.Forms.DataGrid
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

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        internal DropDownDataGrid()
        {
            // Double-buffer to prevent flickering
            DataGridHelper.DoubleBuffer(this);

            base.UpdateStyles();
            base.Scroll += DropDownDataGrid_Scroll;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Redraw when resized
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Invalidate();
            base.Update();
        }

        /// <summary>
        /// Highlight the item under the mouse when clicked
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if(e.Button == MouseButtons.Left)
            {
                DataGrid.HitTestInfo hti = base.HitTest(e.X, e.Y);

                if(hti.Type == DataGrid.HitTestType.Cell)
                {
                    base.ResetSelection();
                    base.CurrentRowIndex = hti.Row;
                    base.Select(hti.Row);
                }
            }
        }

        /// <summary>
        /// Highlight the item under the mouse when it moves
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // We tend to get lots of these even if the mouse doesn't move.  It's an OS thing apparently.
            if(lastMousePos.X != e.X || lastMousePos.Y != e.Y)
            {
                if((!this.IsSimpleStyle && this.MouseTracking) || e.Button == MouseButtons.Left)
                {
                    lastMousePos = new Point(e.X, e.Y);

                    DataGrid.HitTestInfo hti = base.HitTest(e.X, e.Y);

                    if(hti.Type == DataGrid.HitTestType.Cell)
                    {
                        base.ResetSelection();
                        base.CurrentRowIndex = hti.Row;
                        base.Select(hti.Row);
                    }
                    else
                        if(hti.Type == DataGrid.HitTestType.None)
                        {
                            int newRow = base.CurrentRowIndex;

                            // Scroll when out of bounds
                            if(e.Y < base.Top && newRow > 0)
                                newRow--;
                            else
                                if(e.Y > base.Bottom && newRow < DataGridHelper.RowCount(this) - 1)
                                    newRow++;

                            if(newRow != base.CurrentRowIndex)
                            {
                                base.ResetSelection();
                                base.CurrentRowIndex = newRow;
                                base.Select(newRow);
                            }
                        }
                }

                base.OnMouseMove(e);
            }
        }

        /// <summary>
        /// Set mouse capture in the parent when we lose the mouse focus and the parent isn't using the
        /// <c>Simple</c> style.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if(!this.IsSimpleStyle && base.Parent.Visible)
                base.Parent.Capture = true;
        }

        /// <summary>
        /// This lets the owner clear the selected row
        /// </summary>
        internal void ClearSelection()
        {
            base.ResetSelection();
        }

        /// <summary>
        /// Hide any edit control when the grid is scrolled
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DropDownDataGrid_Scroll(object sender, EventArgs e)
        {
            DataGridHelper.ConcedeFocus(this);
        }
        #endregion
    }
}
