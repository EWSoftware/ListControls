//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : NativeToolTipWindow.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/19/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a native tool tip window used by the DataList control as the tracking tool tip when
// dragging its vertical scrollbar.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 02/20/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EWSoftware.ListControls.UnsafeNative
{
	/// <summary>
	/// This represents a native tool tip window used by the <see cref="DataList"/> control as the tracking tool
    /// tip when dragging its vertical scrollbar.
	/// </summary>
	internal sealed class NativeToolTipWindow : NativeWindow
	{
        #region Private class constants and data members
        //=====================================================================

		private const string TOOLTIP_CLASS = "tooltips_class32";

        private const int TTS_ALWAYSTIP = 0x01;
        private const int TTS_NOPREFIX  = 0x02;

		private const int TTF_IDISHWND = 0x0001;
        private const int TTF_TRACK    = 0x0020;
        private const int TTF_ABSOLUTE = 0x0080;

		private const int TTM_ADDTOOLA      = 1028;     // Win9X
		private const int TTM_ADDTOOLW      = 1074;     // NT/XP

		private const int TTM_UPDATETIPTEXTA = 1036;    // Win9X
		private const int TTM_UPDATETIPTEXTW = 1081;    // NT/XP

		private const int TTM_TRACKACTIVATE = 1041;
        private const int TTM_TRACKPOSITION = 1042;

        // These are used to get the correct message ID based on the OS
        private readonly int TTM_ADDTOOL;
        private readonly int TTM_UPDATETIPTEXT;

        // The owning window, visible flag, and tool tip info object
        private IWin32Window owner;
        private bool isVisible;
        private TOOLINFO ti;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This read-only property returns true if visible, false if not
        /// </summary>
        internal bool IsVisible
        {
            get { return isVisible; }
        }
        #endregion

        #region Constructor
        //=====================================================================

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="window">The parent window for the tool tip window</param>
		internal NativeToolTipWindow(IWin32Window window)
		{
            // Determine the correct messages to use
            if(Marshal.SystemDefaultCharSize == 1)
            {
                // Win9x
                TTM_ADDTOOL = TTM_ADDTOOLA;
                TTM_UPDATETIPTEXT = TTM_UPDATETIPTEXTA;
            }
            else
            {
                // NT/XP
                TTM_ADDTOOL = TTM_ADDTOOLW;
                TTM_UPDATETIPTEXT = TTM_UPDATETIPTEXTW;
            }

            owner = window;

			CreateParams cp = new CreateParams();
			cp.Parent = owner.Handle;
			cp.ClassName = TOOLTIP_CLASS;
			cp.Style = TTS_ALWAYSTIP | TTS_NOPREFIX;

			this.CreateHandle(cp);

			// Make the window topmost
			UnsafeNativeMethods.SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0,
                19 /*SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE*/);

            ti = new TOOLINFO(TTF_IDISHWND | TTF_TRACK | TTF_ABSOLUTE, owner.Handle);
            UnsafeNativeMethods.SendMessage(this.Handle, TTM_ADDTOOL, IntPtr.Zero, ref ti);

            // All subsequent uses just need this flag
            ti.flags = TTF_IDISHWND;
		}
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Show the tool tip with the specified text if any.  It will appear under the mouse cursor
        /// </summary>
        /// <param name="tipText">The new tool tip text or null to use the existing text</param>
        internal void ShowTooltip(string tipText)
        {
            if(tipText != null)
            {
                ti.text = tipText;
                UnsafeNativeMethods.SendMessage(this.Handle, TTM_UPDATETIPTEXT, IntPtr.Zero, ref ti);
            }

            UnsafeNativeMethods.SendMessage(this.Handle, TTM_TRACKPOSITION, IntPtr.Zero,
                new IntPtr(Cursor.Position.X + ((Cursor.Position.Y + 20) << 16)));

            if(!isVisible)
            {
                UnsafeNativeMethods.SendMessage(this.Handle, TTM_TRACKACTIVATE, new IntPtr(1), ref ti);
                isVisible = true;
            }
        }

        /// <summary>
        /// Hide the tool tip window
        /// </summary>
        internal void HideTooltip()
        {
            UnsafeNativeMethods.SendMessage(this.Handle, TTM_TRACKACTIVATE, IntPtr.Zero, ref ti);
            isVisible = false;
        }
        #endregion
    }
}
