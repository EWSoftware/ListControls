//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DataListHitTestInfo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a hit test information class for the DataList control
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/25/2005  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is used to contain information about a location within a <see cref="DataList"/> control such as the
    /// row and/or area at a specified location.
    /// </summary>
    public sealed class DataListHitTestInfo
    {
        #region Public constants
        //=====================================================================

        /// <summary>
        /// This can be used to compare a hit test result to see if it was in an undefined location
        /// </summary>
        public static readonly DataListHitTestInfo Nowhere = new(DataListHitType.None, -1);

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This read-only property returns the area of the data list at the location or <c>None</c> if there is
        /// nothing at the location.
        /// </summary>
        public DataListHitType Type { get; }

        /// <summary>
        /// This read-only property returns the zero-based row number at the location or -1 if there is no row at
        /// the location.
        /// </summary>
        /// <remarks>This will only be valid if <see cref="Type"/> is set to <c>Row</c> or <c>RowHeader</c></remarks>
        public int Row { get; }

        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hitType">The area type at the location</param>
        /// <overloads>There are two overloads for the constructor</overloads>
        internal DataListHitTestInfo(DataListHitType hitType) : this(hitType, -1)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hitType">The area type at the location</param>
        /// <param name="hitRow">The row at the location</param>
        internal DataListHitTestInfo(DataListHitType hitType, int hitRow)
        {
            this.Type = hitType;
            this.Row = hitRow;
        }
        #endregion

        #region Equality members
        //=====================================================================

        /// <summary>
        /// This is overridden to compare hit test objects correctly
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object? obj)
        {
            return obj is DataListHitTestInfo info && this.Type == info.Type && this.Row == info.Row;
        }

        /// <summary>
        /// This is overridden to get a hash code for a hit test object
        /// </summary>
        /// <returns>Returns a hash code for the object</returns>
        public override int GetHashCode()
        {
            return (int)this.Type + (this.Row << 8);
        }

        /// <summary>
        /// This is overridden to convert a hit test object to a string
        /// </summary>
        /// <returns>Returns a string representation of the object.</returns>
        public override string ToString()
        {
            return $"{{{this.Type}, {this.Row}}}";
        }
        #endregion
    }
}
