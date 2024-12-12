//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : TOOLINFO.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
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
        internal string? text;

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
        }
    }
}
