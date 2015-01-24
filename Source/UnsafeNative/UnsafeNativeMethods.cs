//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : UnsafeNativeMethods.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/19/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the class that is used for access to some Win32 API functions and for access to the
// Windows XP theme API.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/11/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace EWSoftware.ListControls.UnsafeNative
{
    /// <summary>
    /// This internal class is used for access to some Win32 API functions and for access to the Windows XP theme
    /// API.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class UnsafeNativeMethods
    {
        #region Constants
        //=====================================================================

        internal const string ComboBoxClass = "ComboBox";

        // <summary>Theme changed message ID</summary>
        internal const int ThemeChanged = 794;

        // <summary>Combo box text part</summary>
        internal const int ComboBoxText = 0;

        // <summary>Combo box drop-down button part</summary>
        internal const int DropDownButton = 1;

        // <summary>Combo box normal state</summary>
        internal const int ComboBoxNormal = 1;

        // <summary>Combo box hot state</summary>
        internal const int ComboBoxHot = 2;

        // <summary>Combo box disabled state</summary>
        internal const int ComboBoxDisabled = 4;
        #endregion

        #region Native Win32 functions used by the controls and this class
        //=====================================================================

        // Set window parent
        [DllImport("user32.dll")]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        // Set window position
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx,
            int cy, int flags);

        // SendMessage method for tool tip windows
        [DllImport("user32")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TOOLINFO lParam);

        // General SendMessage method
        [DllImport("user32")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        // Get scrollbar info
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetScrollInfo(IntPtr hWnd, int fnBar, [In, Out]SCROLLINFO si);
        #endregion

        #region Native Windows XP theme functions used by this class
        //=====================================================================

        /// <summary>
        /// Check to see if the parent background needs to be drawn.
        /// </summary>
        [DllImport("uxtheme.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int partId, int stateId);

        /// <summary>
        /// Opens the theme data for a window and its associated class.
        /// </summary>
        [DllImport("uxtheme.dll")]
        internal static extern IntPtr OpenThemeData(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string classList);

        /// <summary>
        /// Closes the theme data handle.
        /// </summary>
        [DllImport("uxtheme.dll")]
        internal static extern int CloseThemeData(IntPtr hTheme);

        /// <summary>
        /// Draw the part of the parent control that is covered by a
        /// partially transparent or alpha-blended child control.
        /// </summary>
        [DllImport("uxtheme.dll")]
        internal static extern int DrawThemeParentBackground(IntPtr hWnd, IntPtr hDC, ref RECT rect);

        /// <summary>
        /// Draw the background image defined by the visual style for the
        /// specified control part.
        /// </summary>
        [DllImport("uxtheme.dll")]
        internal static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hDC, int partId, int stateId,
            ref RECT rect, ref RECT clipRect);
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Get the 32-bit thumb position of the specified scrollbar in the specified control
        /// </summary>
        /// <param name="handle">The handle to the control containing the scrollbar</param>
        /// <param name="scrollBar">0 for horizontal, 1 for vertical</param>
        /// <returns>The 32-bit thumb position of the requested scrollbar</returns>
        internal static int ScrollThumbPosition(IntPtr handle, int scrollBar)
        {
            SCROLLINFO scrollInfo = new SCROLLINFO();
            scrollInfo.fMask = 0x10;
            UnsafeNativeMethods.GetScrollInfo(handle, scrollBar, scrollInfo);
            return scrollInfo.nTrackPos;
        }
        #endregion
    }
}
