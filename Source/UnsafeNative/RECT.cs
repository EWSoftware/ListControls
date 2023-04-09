//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : RECT.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/19/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
//
// This file contains the class that defines the rectangle object passed to the theme API
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

using System.Drawing;
using System.Runtime.InteropServices;

namespace EWSoftware.ListControls.UnsafeNative
{
    /// <summary>
    /// This defines the rectangle object passed to the theme API
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        internal int left;
        internal int top;
        internal int right;
        internal int bottom;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rectLeft">The left coordinate</param>
        /// <param name="rectTop">The top coordinate</param>
        /// <param name="rectRight">The right coordinate</param>
        /// <param name="rectBottom">The bottom coordinate</param>
        internal RECT(int rectLeft, int rectTop, int rectRight, int rectBottom)
        {
            left = rectLeft;
            top = rectTop;
            right = rectRight;
            bottom = rectBottom;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        internal RECT(Rectangle r)
        {
            left = r.Left;
            top = r.Top;
            right = r.Right;
            bottom = r.Bottom;
        }
    }
}
