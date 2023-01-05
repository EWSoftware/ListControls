//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MultiColumnDropDown.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This file contains a multi-column combo box drop-down form that handles the display of the multiple columns
// in the data source.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/01/2005  EFW  Created the code
// 05/01/2006  EFW  Implemented the IDropDown.Scroll method
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using EWSoftware.ListControls.UnsafeNative;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is a drop-down list form that shows when the down arrow is clicked on the multi-column combo box
    /// </summary>
    [ToolboxItem(false)]
    internal class MultiColumnDropDown : UserControl, IDropDown
    {
        #region Value item class
        //=====================================================================

        /// <summary>
        /// This class acts as a wrapper for value data types so that they can be displayed in the data grid
        /// </summary>
        /// <remarks>Since value types and strings do not have a property that the data grid can use to obtain a
        /// value for display, this class acts as a surrogate for them.</remarks>
        internal class ValueItem
        {
            /// <summary>
            /// This property is used to return the value
            /// </summary>
            public object Value { get; private set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="value">The value to use</param>
            public ValueItem(object value)
            {
                this.Value = value;
            }
        }
        #endregion

        #region Private data members
        //=====================================================================

        private DropDownDataGrid dgDropDown;
        private readonly MultiColumnComboBox owner;
        private Point dragOffset;
        private Cursor priorCursor;
        private bool transferringFocus, isCreating, hasInitialized;
        private int startIndex;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is overridden to add the border style to the window styles when created
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.Style |= 0x00800000;   // WS_BORDER

                // WS_EX_TOPMOST | WS_EX_TOOLWINDOW
                if(owner != null && owner.DropDownStyle != ComboBoxStyle.Simple)
                    cp.ExStyle = cp.ExStyle | 0x00000008 | 0x00000080;

                return cp;
            }
        }

        /// <summary>
        /// This returns the index in effect when the drop-down is shown
        /// </summary>
        /// <value>The combo box uses this to determine whether to fire the selection change committed or
        /// selection change canceled event when the drop-down is closed.</value>
        public int StartIndex => startIndex;

        /// <summary>
        /// This returns a flag indicating whether or not the drop-down is currently being created
        /// </summary>
        /// <value>The combo box uses this to determine whether or not to refresh the sub-controls in certain
        /// situations.</value>
        public bool IsCreating => isCreating;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cb">The owner of the drop-down</param>
        internal MultiColumnDropDown(MultiColumnComboBox cb)
        {
            owner = cb;

            // Set the value of the double-buffering style bits to true
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            InitializeComponent();

            dgDropDown.MouseTracking = owner.MouseTracking;

            if(owner.DropDownStyle == ComboBoxStyle.Simple)
            {
                this.TabStop = false;
                dgDropDown.IsSimpleStyle = true;
                dgDropDown.Height = this.Height - 1;
                dgDropDown.TabStop = false;
            }
        }
        #endregion

        #region Windows Form Designer generated code
        //=====================================================================

        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
        {
            this.dgDropDown = new DropDownDataGrid();
            ((ISupportInitialize)(this.dgDropDown)).BeginInit();
            this.SuspendLayout();
            //
            // dgDropDown
            //
            this.dgDropDown.AllowNavigation = false;
            this.dgDropDown.AllowSorting = false;
            this.dgDropDown.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgDropDown.BorderStyle = BorderStyle.None;
            this.dgDropDown.CaptionVisible = false;
            this.dgDropDown.DataMember = "";
            this.dgDropDown.HeaderForeColor = SystemColors.ControlText;
            this.dgDropDown.Location = new Point(0, 0);
            this.dgDropDown.Name = "dgDropDown";
            this.dgDropDown.ParentRowsVisible = false;
            this.dgDropDown.ReadOnly = true;
            this.dgDropDown.Size = new Size(326, 192);
            this.dgDropDown.TabIndex = 0;
            this.dgDropDown.MouseUp += this.dgDropDown_MouseUp;
            // 
            // MultiColumnDropDown
            // 
            this.Controls.Add(this.dgDropDown);
            this.Name = "MultiColumnDropDown";
            this.Size = new Size(328, 208);
            ((ISupportInitialize)(this.dgDropDown)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Clean up any resources being used
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources, false to just release
        /// unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                // Note that this will clear all column definitions.  The columns in the table style do not
                // completely disconnect from the grid when it is disposed.  Unfortunately, there is no way to
                // do this so the columns must be disposed of as well and recreated.
                if(dgDropDown.TableStyles.Count != 0)
                {
                    DataGridTableStyle dgs = dgDropDown.TableStyles[0];
                    dgs.GridColumnStyles.Clear();

                    if(dgDropDown.TableStyles.Count != 0)
                        dgDropDown.TableStyles.RemoveAt(0);

                    dgs.DataGrid = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// This is used to initialize the drop-down styles and data source
        /// </summary>
        private void InitDropDown()
        {
            DropDownTableStyle ddts = owner.DropDownFormat;
            int rowCount, rowHeight, headerHeight = 0, totalSize = 0, rowHeaderWidth = ddts.RowHeaderWidth;

            // Must check this before we set the data source otherwise, columns are added by default
            bool autoSize = (ddts.GridColumnStyles.Count == 0);

            dgDropDown.Font = owner.DropDownFont;
            dgDropDown.RightToLeft = owner.RightToLeft;
            this.BackColor = dgDropDown.BackgroundColor = owner.DropDownBackColor;
            this.Cursor = owner.Cursor;

            // Set the data source and apply style settings.  If there is a data source, use it.
            ddts.MappingName = owner.MappingName;
            dgDropDown.DataMember = owner.BindingPath;

            if(owner.DataSource != null)
            {
                // If bound to a relationship, use the object collection
                if(ddts.MappingName == dgDropDown.DataMember && ddts.MappingName.IndexOf('.') != -1)
                {
                    ddts.MappingName = "ObjectCollection";
                    dgDropDown.TableStyles.Add(ddts);
                    dgDropDown.DataSource = owner.Items;
                }
                else
                {
                    dgDropDown.TableStyles.Add(ddts);
                    dgDropDown.DataSource = owner.DataSource;
                }
            }
            else
            {
                if(ddts.MappingName == "ValueType" || ddts.MappingName == "String")
                {
                    // Value and string types require a wrapper or they don't show up correctly
                    ValueItem[] viArray = new ValueItem[owner.Items.Count];

                    for(int collIdx = 0; collIdx < owner.Items.Count; collIdx++)
                        viArray[collIdx] = new ValueItem(owner.Items[collIdx]);

                    ddts.MappingName = "ValueItem[]";
                    dgDropDown.TableStyles.Add(ddts);
                    dgDropDown.DataSource = viArray;
                }
                else    // It must be an object collection
                    dgDropDown.DataSource = owner.Items;
            }

            // Filter the columns if necessary and clear the headers if auto-sizing and they aren't visible.
            // Clearing the header text is necessary so that we get an accurate size.
            StringCollection filter = owner.ColumnFilter;
            int idx;

            if(filter.Count != 0 || (autoSize && !ddts.ColumnHeadersVisible))
            {
                for(idx = 0; idx < ddts.GridColumnStyles.Count; idx++)
                {
                    if(filter.Count != 0 && !filter.Contains(ddts.GridColumnStyles[idx].MappingName))
                    {
                        ddts.GridColumnStyles.RemoveAt(idx);
                        idx--;
                    }
                    else
                    {
                        if(autoSize && !ddts.ColumnHeadersVisible)
                            ddts.GridColumnStyles[idx].HeaderText = String.Empty;
                    }
                }
            }

            // Row Header Width and Column Headers Visible seem to get overridden so restore them
            ddts.RowHeaderWidth = rowHeaderWidth;
            dgDropDown.ColumnHeadersVisible = ddts.ColumnHeadersVisible;

            // Only auto-size if the user didn't specify any columns
            if(autoSize)
                DataGridHelper.AutoSizeColumns(dgDropDown);

            // Sum the width of the columns and take the opportunity to auto-size columns with a width of zero.
            // This only happens if the user specified column definitions.
            idx = 0;

            foreach(DataGridColumnStyle col in ddts.GridColumnStyles)
            {
                // Replace default null text where needed
                if(col.NullText == "(null)")
                    col.NullText = ddts.DefaultNullText;

                if(col.Width == 0)
                    DataGridHelper.AutoSizeColumn(dgDropDown, idx);

                totalSize += col.Width;
                idx++;
            }

            // Use the specified width or figure out the initial width
            if(owner.DropDownWidth != 0)
                totalSize = owner.DropDownWidth;
            else
            {
                if(ddts.RowHeadersVisible)
                    totalSize += rowHeaderWidth;

                if(idx != 0 && totalSize < owner.Width - 2)
                {
                    // Stretch the last column to fit the width
                    ddts.GridColumnStyles[idx - 1].Width += owner.Width - totalSize - 2;
                    totalSize = owner.Width;
                }
                else
                {
                    if(idx == 0)
                        totalSize = owner.Width;
                    else
                        totalSize += 2;
                }
            }

            // Enforce a minimum width
            if(totalSize < 30)
                totalSize = 30;

            this.Width = totalSize;

            // Set default height
            rowCount = DataGridHelper.RowCount(dgDropDown);

            if(rowCount != 0 && ddts.GridColumnStyles.Count > 0)
                rowHeight = dgDropDown.GetCellBounds(0, 0).Height + 1;
            else
                rowHeight = dgDropDown.PreferredRowHeight;

            // Figure out column header height if visible.  This is a little ugly but it works.
            if(ddts.ColumnHeadersVisible)
            {
                while(dgDropDown.HitTest(rowHeaderWidth + 2, headerHeight).Type == DataGrid.HitTestType.ColumnHeader)
                    headerHeight++;
            }

            if(rowCount > owner.MaxDropDownItems)
            {
                rowHeight = (rowHeight * owner.MaxDropDownItems) + headerHeight;

                // Adjust width to account for the scrollbar
                this.Width += BaseComboBox.DropDownButtonWidth;
            }
            else
                rowHeight = (rowHeight * rowCount) + headerHeight;

            this.Height = rowHeight + this.Height - dgDropDown.Height;
            hasInitialized = true;
        }

        /// <summary>
        /// This is used to set the default selected item, size and position the drop-down, and show it
        /// </summary>
        public void ShowDropDown()
        {
            int idx;

            if(!hasInitialized)
                this.CreateHandle();

            idx = startIndex = owner.SelectedIndex;

            dgDropDown.ClearSelection();
            dragOffset = Point.Empty;

            // Sometimes it shows the edit control so clear it
            DataGridHelper.ConcedeFocus(dgDropDown);

            if(idx != -1 && DataGridHelper.RowCount(dgDropDown) > idx)
            {
                dgDropDown.Select(idx);
                dgDropDown.CurrentRowIndex = idx;
            }

            // The owner positions us when using the simple style.  There's also an odd sequence of events
            // related to updates to bound controls that can cause this control to get disposed after setting
            // the data grid's row index above so we'll stop in that case too.
            if(this.IsDisposed || owner.DropDownStyle == ComboBoxStyle.Simple)
                return;

            if(this.Width < owner.Width)
                this.Width = owner.Width;

            // Make sure we are within the screen bounds.  Take into account the working area of all screens.
            // Note that in certain setups, the leftmost/uppermost screen(s) may have negative coordinates.
            Rectangle workingArea, screen = new Rectangle();

            foreach(Screen s in Screen.AllScreens)
            {
                workingArea = s.WorkingArea;

                if(workingArea.X < screen.X)
                    screen.X = workingArea.X;

                if(workingArea.Y < screen.Y)
                    screen.Y = workingArea.Y;

                if(Math.Abs(workingArea.X) + workingArea.Width > screen.Width)
                    screen.Width = Math.Abs(workingArea.X) + workingArea.Width;

                if(Math.Abs(workingArea.Y) + workingArea.Height > screen.Height)
                    screen.Height = Math.Abs(workingArea.Y) + workingArea.Height;
            }

            Point pOwner = owner.Parent.PointToScreen(owner.Location);
            pOwner.Y += owner.Height;

            Point p = pOwner;

            if(this.Width > screen.Width)
                this.Width = screen.Width;

            if(this.Height > screen.Height)
                this.Height = screen.Height;

            if(p.X < screen.X)
                p.X = screen.X;

            if(p.Y < screen.Y)
                p.Y = screen.Y;

            if(p.X + this.Width > screen.X + screen.Width)
                p.X = screen.X + screen.Width - this.Width - 1;

            if(p.Y + this.Height > screen.Y + screen.Height)
                p.Y = screen.Y + screen.Height - this.Height - 1;

            // If we are covering the combo box, move the location above the combo box
            if(p.Y < pOwner.Y)
            {
                idx = pOwner.Y - this.Height - owner.Height;

                // If we would go off the top of the screen, figure out whether we would have more rows visible
                // if we moved the form above the combo box or left it where it is at below the combo box and
                // shrink it to fit.
                if(idx < screen.Y)
                {
                    if(this.Height + idx > this.Height - (pOwner.Y - p.Y))
                    {
                        this.Height += idx;
                        idx = screen.Y;
                    }
                    else
                    {
                        this.Height -= (pOwner.Y - p.Y);
                        idx = pOwner.Y;
                    }
                }

                p.Y = idx;
            }

            this.Location = p;

            UnsafeNativeMethods.SetParent(this.Handle, IntPtr.Zero);
            this.Show();

            // NOTE: We lose capture pretty quickly as the mouse isn't inside the drop-down.  MouseLeave grabs it
            // for us again.
            this.Capture = true;
        }

        /// <summary>
        /// Scroll the drop-down the specified number of rows
        /// </summary>
        /// <param name="rows">The number of rows to scroll</param>
        public void ScrollDropDown(int rows)
        {
            DataGridHelper.ScrollDown(dgDropDown, rows);
            dgDropDown.Invalidate(true);
        }

        /// <summary>
        /// This is used to select an item in the drop-down as the current item
        /// </summary>
        /// <param name="selIdx">The index of the selected item</param>
        internal void SelectItem(int selIdx)
        {
            dgDropDown.ClearSelection();

            if(selIdx != -1)
            {
                DataGridHelper.ConcedeFocus(dgDropDown);
                dgDropDown.CurrentRowIndex = selIdx;
                dgDropDown.Select(selIdx);
            }
        }

        /// <summary>
        /// This is overridden to release the mouse capture and hide the drop-down when another desktop window is
        /// activated.
        /// </summary>
        /// <param name="m">The message to process</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // WM_ACTIVATEAPP
            if(m.Msg == 0x001C && m.WParam == IntPtr.Zero && this.Visible && owner.DropDownStyle != ComboBoxStyle.Simple)
            {
                this.Capture = false;
                owner.DroppedDown = false;
            }
        }

        /// <summary>
        /// This is overridden to initialize the drop-down control when the handle is created
        /// </summary>
        protected override void CreateHandle()
        {
            try
            {
                isCreating = true;
                base.CreateHandle();

                if(!hasInitialized)
                    this.InitDropDown();
            }
            finally
            {
                isCreating = false;
            }
        }

        /// <summary>
        /// This is overridden to ensure it redraws when resized
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Invalidate();
            this.Update();
        }

        /// <summary>
        /// This is overridden to draw a sizing grip in the lower right corner of the window
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if(owner.DropDownStyle != ComboBoxStyle.Simple)
                ControlPaint.DrawSizeGrip(e.Graphics, Color.White, this.Width - 17, this.Height - 17, 16, 16);
        }

        /// <summary>
        /// This is overridden to get the offset used for resizing if necessary and to hide the drop-down if
        /// clicked outside of its bounds.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(owner.DropDownStyle != ComboBoxStyle.Simple)
            {
                Point p = this.PointToClient(Cursor.Position);

                // Hide if the mouse is clicked outside the bounds of the drop-down
                if(!this.ClientRectangle.Contains(p))
                {
                    this.Capture = false;
                    owner.DroppedDown = false;
                    return;
                }

                Rectangle r = new Rectangle(this.Size.Width - 16, this.Size.Height - 16, 16, 16);

                if(e.Button == MouseButtons.Left && r.Contains(p))
                    dragOffset = new Point(this.Width - p.X, this.Height - p.Y);
                else
                    dragOffset = Point.Empty;
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// This is overridden to resize the window when necessary
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int width, height;

            if(owner.DropDownStyle != ComboBoxStyle.Simple)
            {
                Point p = this.PointToClient(Cursor.Position);

                // Only resize if dragging and enforce a minimum size
                if(e.Button == MouseButtons.Left && dragOffset != Point.Empty)
                {
                    width = Cursor.Position.X - this.Location.X + dragOffset.X;
                    height = Cursor.Position.Y - this.Location.Y + dragOffset.Y;

                    if(width < owner.Width)
                        width = owner.Width;
                    else
                    {
                        if(width < 30)
                            width = 30;
                    }

                    if(height < 30)
                        height = 30;

                    this.Size = new Size(width, height);
                }
                else
                {
                    if(dgDropDown.ClientRectangle.Contains(p))
                    {
                        base.OnMouseMove(e);

                        // Let the grid handle mouse events
                        transferringFocus = true;
                        this.Capture = false;
                        dgDropDown.Focus();
                        return;
                    }
                }

                Rectangle r = new Rectangle(this.Size.Width - 16, this.Size.Height - 16, 16, 16);

                if(r.Contains(p))
                {
                    if(this.Cursor != Cursors.SizeNWSE)
                    {
                        priorCursor = this.Cursor;
                        this.Cursor = Cursors.SizeNWSE;
                    }
                }
                else
                {
                    if(this.Cursor == Cursors.SizeNWSE)
                        this.Cursor = priorCursor;
                }
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// This is overridden to reset the mouse cursor if the mouse leaves the drop-down
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <remarks>This also sets the capture again if necessary as the mouse is not inside the window to begin
        /// with and it loses the capture shortly after it is set in <see cref="ShowDropDown"/>.
        /// </remarks>
        protected override void OnMouseLeave(EventArgs e)
        {
            if(this.Cursor == Cursors.SizeNWSE)
                this.Cursor = priorCursor;

            base.OnMouseLeave(e);

            // Don't recapture the mouse if transferring focus to the grid
            if(owner.DropDownStyle != ComboBoxStyle.Simple)
            {
                if(!transferringFocus)
                    this.Capture = this.Visible;
                else
                    transferringFocus = false;
            }
        }

        /// <summary>
        /// Select an item if the mouse is clicked on an item in the drop-down
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dgDropDown_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                var hti = dgDropDown.HitTest(e.X, e.Y);

                if(hti.Type == DataGrid.HitTestType.Cell)
                    owner.CommitSelection(hti.Row);

                if(owner.DropDownStyle == ComboBoxStyle.Simple)
                    owner.FocusTextBox();
            }
        }
        #endregion
    }
}
