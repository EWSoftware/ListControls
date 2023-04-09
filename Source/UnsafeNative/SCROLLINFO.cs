//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : SCROLLINFO.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/19/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
//
// This file contains the class that defines the scrollbar info object passed to the Win32 API
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
    /// This defines the scrollbar info object passed to the Win32 API
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class SCROLLINFO
    {
        internal int cbSize;
        internal int fMask;
        internal int nMin;
        internal int nMax;
        internal int nPage;
        internal int nPos;
        internal int nTrackPos;

        /// <summary>
        /// Constructor
        /// </summary>
        internal SCROLLINFO()
        {
            this.cbSize = Marshal.SizeOf(typeof(SCROLLINFO));

            // Not needed but it shuts the compiler up
            nMin = nMax = nPage = nPos = nTrackPos = 0;
        }
    }
}
