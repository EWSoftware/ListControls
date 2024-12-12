//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : RowPanel.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a derived Panel control used to display rows in the DataList control
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/25/2005  EFW  Created the code
// 02/19/2006  EFW  Reworked scrolling to make redraws much smoother
//===============================================================================================================

using System.Globalization;

using EWSoftware.ListControls.UnsafeNative;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is a derived <see cref="Panel"/> control used to display rows in the <see cref="DataList"/> control
    /// </summary>
    [ToolboxItem(false)]
	internal sealed class RowPanel : Panel
	{
        #region Private data members
        //=====================================================================

        private readonly MethodInfo syncScrollbars = null!;
#if NET40
        private readonly FieldInfo eventMouseWheel = null!, isLayoutSuspended = null!;
#else
        private readonly FieldInfo eventMouseWheel = null!;
        private readonly PropertyInfo isLayoutSuspended = null!;
#endif
        private NativeToolTipWindow scrollTip = null!;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        internal RowPanel()
        {
            syncScrollbars = typeof(ScrollableControl).GetMethod("SyncScrollbars", BindingFlags.NonPublic |
                BindingFlags.Instance)!;

            // The backing fields differs in the .NET Framework
#if NET40
            eventMouseWheel = typeof(Control).GetField("EventMouseWheel", BindingFlags.NonPublic |
                BindingFlags.Static);
            isLayoutSuspended = typeof(Control).GetField("layoutSuspendCount", BindingFlags.NonPublic |
                BindingFlags.Instance);
#else
            eventMouseWheel = typeof(Control).GetField("s_mouseWheelEvent", BindingFlags.NonPublic |
                BindingFlags.Static)!;
            isLayoutSuspended = typeof(Control).GetProperty("IsLayoutSuspended", BindingFlags.NonPublic |
                BindingFlags.Instance)!;
#endif
            // See above.  If it stops here, there's a problem.
            if(syncScrollbars == null || eventMouseWheel == null || isLayoutSuspended == null)
                Debugger.Break();

            // Turn on resize on redraw
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            this.AutoScroll = true;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is used to determine if layout is currently suspended
        /// </summary>
        /// <remarks>When themes are enabled and the data list is embedded in certain custom controls, it gets
        /// extra paint events while adjusting the row controls which causes an exception.  This is used by the
        /// data list to see if it should skip painting in those instances.
        /// </remarks>
#if NET40
        internal bool LayoutSuspended => ((byte?)isLayoutSuspended?.GetValue(this) ?? 0) != 0;
#else
        internal bool LayoutSuspended => (bool?)isLayoutSuspended?.GetValue(this) ?? false;
#endif

        /// <summary>
        /// This is called by the parent DataList to make a control visible and ensure that all visible rows are
        /// bound and initialized.
        /// </summary>
        /// <param name="control">The control to make visible</param>
        internal void MakeControlVisible(Control control)
        {
            this.ScrollControlIntoView(control);

            ((DataList?)this.Parent)?.InitializeAndBindVisibleRows();
        }

        /// <summary>
        /// This is called by <see cref="DataList.MoveTo(Int32)"/> to scroll a row into view smoothly
        /// </summary>
        /// <param name="newRow">The row to make visible</param>
        internal void ScrollRowIntoView(int newRow)
        {
            int rowHeight = ((DataList)this.Parent!).RowHeight;

            Message m = Message.Create(this.Handle, 0x0115, new IntPtr(32767), new IntPtr(rowHeight * newRow));

            this.HandleVerticalScroll(ref m);
        }

        /// <summary>
        /// Handle vertical scroll to make it smoother
        /// </summary>
        /// <remarks>The default scrolling increments are quite small and the frequent redraws cause a messy
        /// display when the thumb is dragged.  By handling it ourselves, we can initialize and bind the rows
        /// before they are displayed and can also control the increments to provide a smoother scroll with a
        /// cleaner visual appearance.</remarks>
        /// <param name="m">The vertical scroll message parameters</param>
        private void HandleVerticalScroll(ref Message m)
        {
            TemplateControl? tc;
            DataList owner = (DataList)this.Parent!;

            Rectangle clientRect = this.ClientRectangle, displayRect = this.DisplayRectangle;

            int row, fillRange, maxRows, multiplier, rowHeight = owner.RowHeight, curPos = -displayRect.Y;
            int maxPos = -(clientRect.Height - displayRect.Height - rowHeight), scrollEvent = (int)m.WParam & 0xFFFF;

            switch(scrollEvent)
            {
                case 0:     // Line up
                    if(curPos <= 0)
                        curPos = 0;
                    else
                        curPos -= rowHeight;
                    break;

                case 1:     // Line down
                    if(curPos >= maxPos - rowHeight)
                        curPos = maxPos;
                    else
                        curPos += rowHeight;
                    break;

                case 2:     // Page up
                    if(curPos <= clientRect.Height)
                        curPos = 0;
                    else
                        curPos -= clientRect.Height;
                    break;

                case 3:     // Page down
                    if(curPos >= maxPos - clientRect.Height)
                        curPos = maxPos;
                    else
                        curPos += clientRect.Height;
                    break;

                case 4:     // Thumb position (end tracking)
                case 5:     // Thumb track (drag)
                    scrollTip ??= new NativeToolTipWindow(this);

                    // Round it to the nearest row start and show the position in a tool tip
                    curPos = UnsafeNativeMethods.ScrollThumbPosition(this.Handle, 1) / rowHeight * rowHeight;
                    scrollTip.ShowTooltip(String.Format(CultureInfo.InvariantCulture, LR.GetString("DLScrollPos"),
                        (curPos / rowHeight) + 1, owner.RowCount));
                    break;

                case 6:     // Top
                    curPos = 0;
                    break;

                case 7:     // Bottom
                    curPos = maxPos;
                    break;

                case 8:     // End scroll
                    if(scrollTip != null && scrollTip.IsVisible)
                        scrollTip.HideTooltip();
                    break;

                case 32767: // Custom call to scroll row into view
                    curPos = m.LParam.ToInt32();

                    if(curPos < 0)
                        curPos = 0;
                    else
                        if(curPos >= maxPos)
                            curPos = maxPos;
                    break;
            }

            // Ignore if not displaying content while scrolling and it was a thumb tracking event
            if(this.GetScrollState(0x10) || scrollEvent != 5)
            {
                // Initialize and bind the rows before they are displayed to smooth out the scrolling
                multiplier = (owner.SeparatorsVisible) ? 2 : 1;

                if(owner.ListManager != null && this.Controls.Count != 0)
                {
                    maxRows = owner.ListManager.Count;

                    // Get the range of visible rows
                    row = curPos / rowHeight;
                    fillRange = ((curPos + rowHeight + this.Height) / rowHeight) + 1;

                    if(fillRange < maxRows)
                        maxRows = fillRange;
                }
                else
                    row = maxRows = 0;

                while(row < maxRows)
                {
                    if(row * multiplier < this.Controls.Count)
                        tc = (TemplateControl)this.Controls[row * multiplier];
                    else
                        tc = null;

                    // By waiting until the templates scroll into view, it saves some time and resources during
                    // the initial load and only binds what is actually shown.
                    if(tc != null && !tc.IsNewRowInternal && !tc.HasBeenBound)
                    {
                        if(!tc.HasBeenInitialized)
                        {
                            tc.InitializeTemplate();
                            tc.HasBeenInitialized = true;
                        }

                        tc.Bind();
                        tc.HasBeenBound = true;
                        owner.OnItemDataBound(new DataListEventArgs(row, tc));
                    }

                    row++;
                }

                // Set "user scrolled" flag and update the display rectangle
                this.SetScrollState(8, true);
                this.SetDisplayRectLocation(displayRect.X, -curPos);

                syncScrollbars?.Invoke(this, [this.AutoScroll]);

                // Refresh the row headers if they are visible
                if(owner.RowHeadersVisible)
                    owner.Invalidate(new Rectangle(0, 0, owner.RowHeaderWidth, owner.Height), false);
            }

            if(scrollEvent != 8)
            {
                ScrollEventArgs se = new((ScrollEventType)scrollEvent, -displayRect.Y, curPos,
                    ScrollOrientation.VerticalScroll);
                this.OnScroll(se);
            }
        }

        /// <summary>
        /// Create a custom row collection
        /// </summary>
        /// <returns>The row collection objects</returns>
        protected override ControlCollection CreateControlsInstance()
        {
            return new RowControlCollection(this);
        }

        /// <summary>
        /// This is overridden to focus a row when the mouse is clicked in a panel area outside of one of the
        /// templates.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            DataList owner = (DataList)this.Parent!;

            if(owner.ListManager != null)
            {
                owner.Select(-1, -1, -1);

                int nRow = (e.Y - this.AutoScrollPosition.Y) / owner.RowHeight;

                if(nRow < 0)
                    nRow = 0;

                if(owner.CurrentRow != nRow || !this.Parent!.Focused)
                {
                    // Give the selected row the focus
                    if(owner.SeparatorsVisible)
                        nRow *= 2;

                    if(nRow < this.Controls.Count && (!this.Controls[nRow].ContainsFocus || !this.Parent!.Focused))
                        this.Controls[nRow].Focus();
                }
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// This is overridden to invalidate the row header area so that the control is redrawn correctly when
        /// scrolled vertically.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // If scrolling vertically, scroll one row at a time
            if(this.VScroll)
            {
                int row = -this.DisplayRectangle.Y / ((DataList)this.Parent!).RowHeight;

                if(e.Delta < 0)
                    row++;
                else
                    row--;

                if(row < 0)
                    row = 0;

                this.ScrollRowIntoView(row);

                if(e is HandledMouseEventArgs mea)
                    mea.Handled = true;

                // Bypass ScrollableControl's version and let other handlers get called
                if(eventMouseWheel != null)
                {
                    ((MouseEventHandler?)this.Events[eventMouseWheel])?.Invoke(this, e);
                }
            }
            else
                base.OnMouseWheel(e);
        }

        /// <summary>
        /// This is overridden to suppress scaling.
        /// </summary>
        /// <remarks>If not done, it tends to do odd things with the row template size if bound before the
        /// containing form has had a chance to scale itself and there is a difference between the font size
        /// used during development and the one in effect at runtime.</remarks>
        /// <param name="dx">The ratio by which to scale the control horizontally</param>
        /// <param name="dy">The ratio by which to scale the control vertically</param>
        protected override void ScaleCore(float dx, float dy)
        {
        }

        /// <summary>
        /// This is overridden to handle vertical scrolling manually so that we can smooth it out to make it
        /// better looking.
        /// </summary>
        /// <param name="m">The message</param>
        protected override void WndProc(ref Message m)
        {
            // Handle WM_VSCROLL to smooth out scrolling
            if(m.Msg == 0x0115 && m.LParam == IntPtr.Zero)
                HandleVerticalScroll(ref m);
            else
                base.WndProc(ref m);
        }
        #endregion
    }
}
