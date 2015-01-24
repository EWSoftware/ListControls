//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DataGridHelper.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/25/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This is a static class that contains various helper methods for use with DataGrid objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/01/2005  EFW  Created the code
// 05/01/2006  EFW  Added ScrollDown helper method
//===============================================================================================================

using System;
using System.Reflection;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
	/// <summary>
    /// This is a static class that contains various helper methods for use with
    /// <see cref="System.Windows.Forms.DataGrid"/> objects.
	/// </summary>
	/// <remarks>Many of these methods use reflection to access internal members of the
    /// <see cref="System.Windows.Forms.DataGrid"/> class that aren't exposed as public items.</remarks>
	internal static class DataGridHelper
	{
        /// <summary>
        /// This is used to get a reference to the DataGrid type on the passed data grid-derived object
        /// </summary>
        /// <param  name="dg">The data grid to use</param>
        private static Type DataGridType(DataGrid dg)
        {
            Type type = dg.GetType();

            while(type != typeof(DataGrid))
                type = type.BaseType;

            return type;
        }

        /// <summary>
        /// This can be used to turn on double-buffering in the specified <see cref="System.Windows.Forms.DataGrid"/>
        /// control to help reduce flickering during redraws.
        /// </summary>
        /// <param name="dg">The data grid in which to enable double-buffering</param>
        internal static void DoubleBuffer(DataGrid dg)
        {
            MethodInfo mi = DataGridHelper.DataGridType(dg).GetMethod("SetStyle", BindingFlags.NonPublic |
                BindingFlags.Instance);

            mi.Invoke(dg, new object[] { ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true });
        }

        /// <summary>
        /// Get the current table style in use for the specified <see cref="System.Windows.Forms.DataGrid"/>
        /// control.
        /// </summary>
        /// <param  name="dg">The data grid to use</param>
        /// <returns>The current table style object in use by the data grid</returns>
        internal static DataGridTableStyle CurrentTableStyle(DataGrid dg)
        {
            FieldInfo fi = DataGridHelper.DataGridType(dg).GetField("myGridTable", BindingFlags.NonPublic |
                BindingFlags.Instance);

            return (DataGridTableStyle)fi.GetValue(dg);
        }

        /// <summary>
        /// This can be used to auto-size the specified column in the specified
        /// <see cref="System.Windows.Forms.DataGrid"/> control.
        /// </summary>
        /// <param name="dg">The data grid in which to size the column</param>
        /// <param name="col">The column to resize</param>
        internal static void AutoSizeColumn(DataGrid dg, int col)
        {
            MethodInfo mi = DataGridHelper.DataGridType(dg).GetMethod("ColAutoResize", BindingFlags.NonPublic |
                BindingFlags.Instance);

            mi.Invoke(dg, new object[] { col });
        }

        /// <summary>
        /// This can be used to auto-size all columns in the specified <see cref="System.Windows.Forms.DataGrid"/>
        /// control.
        /// </summary>
        /// <param name="dg">The data grid in which to size the columns</param>
        internal static void AutoSizeColumns(DataGrid dg)
        {
            MethodInfo mi = DataGridHelper.DataGridType(dg).GetMethod("ColAutoResize", BindingFlags.NonPublic |
                BindingFlags.Instance);

            DataGridTableStyle dgs = DataGridHelper.CurrentTableStyle(dg);

            for(int idx = 0; idx < dgs.GridColumnStyles.Count; idx++)
                mi.Invoke(dg, new object[] { idx });
        }

        /// <summary>
        /// This can be used to get the number of rows in the specified <see cref="System.Windows.Forms.DataGrid"/>
        /// control.
        /// </summary>
        /// <param name="dg">The data grid from which to get the row count</param>
        /// <returns>The number of rows in the data grid</returns>
        internal static int RowCount(DataGrid dg)
        {
            Type type = DataGridHelper.DataGridType(dg);

            PropertyInfo pi = type.GetProperty("ListManager", BindingFlags.NonPublic | BindingFlags.Instance);

            CurrencyManager cm = (CurrencyManager)pi.GetValue(dg, null);

            return (cm != null) ? cm.Count : 0;
        }

        /// <summary>
        /// This can be used to force the current edit column in the data grid to concede the focus.  Changes
        /// will be saved if possible.
        /// </summary>
        /// <param name="dg">The data grid to use</param>
        internal static void ConcedeFocus(DataGrid dg)
        {
            Type type = DataGridHelper.DataGridType(dg);

            FieldInfo fi = type.GetField("editColumn", BindingFlags.NonPublic | BindingFlags.Instance);

            dg.EndEdit(null, 0, false);

            DataGridColumnStyle dgc = (DataGridColumnStyle)fi.GetValue(dg);

            if(dgc != null)
            {
                MethodInfo mi = dgc.GetType().GetMethod("ConcedeFocus", BindingFlags.NonPublic |
                    BindingFlags.Instance);

                mi.Invoke(dgc, null);
            }
        }

        /// <summary>
        /// This can be used to scroll the data grid up or down
        /// </summary>
        /// <param name="dg">The data grid to scroll</param>
        /// <param name="rows">The number of rows to scroll (negative to scroll up, positive to scroll down)</param>
        internal static void ScrollDown(DataGrid dg, int rows)
        {
            int firstVisibleRow, visibleRows, lastRow, newEndRow;

            Type type = DataGridHelper.DataGridType(dg);

            FieldInfo fr = type.GetField("firstVisibleRow", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo vr = type.GetField("numTotallyVisibleRows", BindingFlags.NonPublic | BindingFlags.Instance);

            firstVisibleRow = (int)fr.GetValue(dg);
            visibleRows = (int)vr.GetValue(dg);
            lastRow = DataGridHelper.RowCount(dg);

            if(visibleRows <= rows)
                newEndRow = firstVisibleRow + rows;
            else
                newEndRow = firstVisibleRow + rows + visibleRows;

            if(newEndRow >= lastRow)
                rows = lastRow - visibleRows - firstVisibleRow;

            MethodInfo mi = type.GetMethod("ScrollDown", BindingFlags.NonPublic | BindingFlags.Instance);

            mi.Invoke(dg, new object[] { rows });
        }
    }
}
