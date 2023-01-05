//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ListPadding.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This defines a structure used to specify the padding for the radio button and checkbox list controls
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/18/2005  EFW  Created the code
// 12/09/2005  EFW  Renamed to avoid conflict with .NET 2.0 class
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

using EWSoftware.ListControls.Design;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This structure is used to specify the padding (in pixels) for the <see cref="RadioButtonList"/> and
    /// <see cref="CheckBoxList"/> controls.
    /// </summary>
	[Serializable, StructLayout(LayoutKind.Sequential), TypeConverter(typeof(ListPaddingTypeConverter))]
    public struct ListPadding
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// Get or set the top padding
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Get or set the left side padding
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Get or set the bottom padding
        /// </summary>
        public int Bottom { get; set; }

        /// <summary>
        /// Get or set the right side padding
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// Get or set the padding between columns
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Get or set the padding between rows
        /// </summary>
        public int Row { get; set; }
        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Constructor.  Set all padding to the same value.
        /// </summary>
        /// <param name="padding">The padding to use for all sides and between columns and rows</param>
        /// <overloads>There are two overloads for the constructor</overloads>
        public ListPadding(int padding) : this()
        {
            this.Top = this.Left = this.Bottom = this.Right = this.Column = this.Row = padding;
        }

        /// <summary>
        /// Constructor.  Set padding on each side and between columns and rows to the specified values.
        /// </summary>
        /// <param name="top">The top padding</param>
        /// <param name="left">The left side padding</param>
        /// <param name="bottom">The bottom padding</param>
        /// <param name="right">The right side padding</param>
        /// <param name="column">The padding between columns</param>
        /// <param name="row">The padding between rows</param>
        public ListPadding(int top, int left, int bottom, int right, int column, int row) : this()
        {
            this.Top = top;
            this.Left = left;
            this.Bottom = bottom;
            this.Right = right;
            this.Column = column;
            this.Row = row;
        }
        #endregion

        #region Method overrides, equality checks, etc.
        //=====================================================================

        /// <summary>
        /// This is overridden to allow proper comparison of <c>ListPadding</c> objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is ListPadding))
                return false;

            ListPadding lp = (ListPadding)obj;

            return (this.Top == lp.Top && this.Left == lp.Left && this.Bottom == lp.Bottom &&
                this.Right == lp.Right && this.Column == lp.Column && this.Row == lp.Row);
        }

        /// <summary>
        /// Overload for equal operator
        /// </summary>
        /// <param name="lp1">The first object</param>
        /// <param name="lp2">The second object</param>
        /// <returns>True if equal, false if not</returns>
        public static bool operator == (ListPadding lp1, ListPadding lp2)
        {
            return lp1.Equals(lp2);
        }

        /// <summary>
        /// Overload for not equal operator
        /// </summary>
        /// <param name="lp1">The first object</param>
        /// <param name="lp2">The second object</param>
        /// <returns>True if not equal, false if they are equal</returns>
        public static bool operator != (ListPadding  lp1, ListPadding lp2)
        {
            return !lp1.Equals(lp2);
        }

        /// <summary>
        /// Get a hash code for the <c>ListPadding</c> object
        /// </summary>
        /// <remarks>To compute the hash code, it uses the string form of the object</remarks>
        /// <returns>Returns the hash code for the <c>ListPadding</c> object</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// This is overridden to get a string representation of the <c>ListPadding</c> object
        /// </summary>
        /// <returns>The object as a string</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "List Padding: Top: {0} Left: {1} " +
                "Bottom: {2} Right: {3} Column: {4} Row: {5}", this.Top, this.Left, this.Bottom, this.Right,
                this.Column, this.Row);
        }
        #endregion
    }
}
