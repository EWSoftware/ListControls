//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DataGridViewHelper.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/09/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This is a static class that contains various helper methods for use with DataGridView objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/21/2007  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This is a static class that contains various helper methods for use with <see cref="DataGridView"/>
    /// objects.
    /// </summary>
    /// <remarks>Many of these methods use reflection to access internal members of the <see cref="DataGridView"/>
    /// class that are not exposed as public items.</remarks>
    internal static class DataGridViewHelper
    {
        private static readonly Dictionary<Color, SolidBrush> brushes = new(10);

        /// <summary>
        /// This is used to get a reference to the DataGridView type on the passed data grid view-derived object
        /// </summary>
        /// <param  name="dgv">The data grid view to use</param>
        private static Type DataGridViewType(DataGridView dgv)
        {
            Type type = dgv.GetType();

            while(type != typeof(DataGridView))
                type = type.BaseType!;

            return type;
        }

        /// <summary>
        /// This can be used to get a cached type converter from the specified data grid view
        /// </summary>
        /// <param name="dgv">The data grid from which to get the cached type converter</param>
        /// <param name="type">The type for which to get a converter</param>
        /// <returns>The type converter for the specified type</returns>
        internal static TypeConverter GetCachedTypeConverter(DataGridView dgv, Type type)
        {
            MethodInfo mi = DataGridViewType(dgv).GetMethod("GetCachedTypeConverter",
                BindingFlags.NonPublic | BindingFlags.Instance) ??
                throw new InvalidOperationException("GetCachedTypeConverter method not found");

            return (TypeConverter)mi.Invoke(dgv, [type])!;
        }

        /// <summary>
        /// This can be used to get a cached solid brush
        /// </summary>
        /// <param name="color">The color of the brush</param>
        /// <returns>The brush to use</returns>
        internal static SolidBrush GetCachedBrush(Color color)
        {
            // The GetCachedBrush method doesn't exist in .NET anymore so we implement our own cache
            if(!brushes.TryGetValue(color, out SolidBrush? cachedBrush))
            {
                cachedBrush = new SolidBrush(color);
                brushes.Add(color, cachedBrush);
            }

            return cachedBrush;
        }

        /// <summary>
        /// This can be used to get the address of the cell that the mouse is currently over
        /// </summary>
        /// <param name="dgv">The data grid view from which to get the point</param>
        /// <returns>The point at which the mouse entered the cell</returns>
        internal static Point MouseEnteredCellAddress(DataGridView dgv)
        {
            PropertyInfo pi = DataGridViewType(dgv).GetProperty("MouseEnteredCellAddress",
                BindingFlags.NonPublic | BindingFlags.Instance) ??
                throw new InvalidOperationException("MouseEnteredCellAddress property not found");

            return (Point)pi.GetValue(dgv, null)!;
        }

        /// <summary>
        /// This can be used to get the address of the cell in which the mouse button was pressed
        /// </summary>
        /// <param name="dgv">The data grid view from which to get the point</param>
        /// <returns>The cell in which the mouse button was pressed</returns>
        internal static Point MouseDownCellAddress(DataGridView dgv)
        {
            PropertyInfo pi = DataGridViewType(dgv).GetProperty("MouseDownCellAddress",
                BindingFlags.NonPublic | BindingFlags.Instance) ??
                throw new InvalidOperationException("MouseDownCellAddress property not found");

            return (Point)pi.GetValue(dgv, null)!;
        }

        /// <summary>
        /// This can be used to notify the data grid view that it may need to resize the specified column
        /// </summary>
        /// <param name="dgv">The data grid view to notify</param>
        /// <param name="column">The column to resize</param>
        internal static void OnColumnCommonChange(DataGridView dgv, int column)
        {
            MethodInfo mi = DataGridViewType(dgv).GetMethod("OnColumnCommonChange",
                BindingFlags.NonPublic | BindingFlags.Instance) ??
                throw new InvalidOperationException("OnColumnCommonChange method not found");

            mi.Invoke(dgv, [column]);
        }

        /// <summary>
        /// This can be used to notify the data grid view that it may need to resize the specified column and row
        /// </summary>
        /// <param name="dgv">The data grid view to notify</param>
        /// <param name="column">The column to resize</param>
        /// <param name="row">The row to resize</param>
        internal static void OnCellCommonChange(DataGridView dgv, int column, int row)
        {
            MethodInfo mi = DataGridViewType(dgv).GetMethod("OnCellCommonChange",
                BindingFlags.NonPublic | BindingFlags.Instance) ??
                throw new InvalidOperationException("OnCellCommonChange method not found");

            mi.Invoke(dgv, [column, row]);
        }

        /// <summary>
        /// This can be used to get whether or not the grid is showing focus cues
        /// </summary>
        /// <param name="dgv">The data grid view from which to get the point</param>
        internal static bool ShowFocusCues(DataGridView dgv)
        {
            PropertyInfo pi = DataGridViewType(dgv).GetProperty("ShowFocusCues",
                BindingFlags.NonPublic | BindingFlags.Instance) ??
                throw new InvalidOperationException("ShowFocusCues property not found");

            return (bool)pi.GetValue(dgv, null)!;
        }

        /// <summary>
        /// This can be used to show or hide the tool tip for the given cell
        /// </summary>
        /// <param name="dgv">The data grid view in which to show or hide the tool tip</param>
        /// <param name="activate">True to activate the tool tip, false to hide it</param>
        /// <param name="toolTipText">The tool tip text to show</param>
        /// <param name="columnIndex">The column index of the cell</param>
        /// <param name="rowIndex">The row index of the cell</param>
        internal static void ActivateToolTip(DataGridView dgv, bool activate, string? toolTipText,
          int columnIndex, int rowIndex)
        {
            MethodInfo mi = DataGridViewType(dgv).GetMethod("ActivateToolTip",
                BindingFlags.NonPublic | BindingFlags.Instance) ??
                throw new InvalidOperationException("ActivateToolTip method not found");

            mi.Invoke(dgv, [activate, toolTipText, columnIndex, rowIndex]);
        }
    }
}
