//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : TOOLINFO.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/19/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the class that defines the TOOLINFO structure for the tool tip window class
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

namespace EWSoftware.ListControls.UnsafeNative
{
    /// <summary>
    /// This defines the TOOLINFO structure for the tool tip window class
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct TOOLINFO
    {
        internal int cbSize;
        internal int flags;
        internal IntPtr hwnd;
        internal IntPtr uId;
        internal RECT rect;
        internal IntPtr hinst;

        [MarshalAs(UnmanagedType.LPTStr)]
        internal string text;

        internal IntPtr lparam;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="toolFlags">Tool tip flags</param>
        /// <param name="handle">The handle of the owner</param>
        internal TOOLINFO(int toolFlags, IntPtr handle)
        {
            cbSize = Marshal.SizeOf(typeof(TOOLINFO));
            flags = toolFlags;
            uId = handle;

            // Not needed but it shuts the compiler up
            hwnd = hinst = lparam = IntPtr.Zero;
            rect = new RECT(0, 0, 0, 0);
            text = null;
        }
    }
}
