//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DropDownTableStyle.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/16/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a custom table style class that omits several fields and modifies the defaults for some
// properties for use with the multi-column combo box control's drop-down.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/09/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is a custom table style class that omits several fields and modifies the defaults for some
    /// properties for use with the <see cref="MultiColumnComboBox"/> control's drop-down.
    /// </summary>
    public class DropDownTableStyle : System.Windows.Forms.DataGridTableStyle
    {
        #region Modified properties
        //=====================================================================

        // The following properties have different default values

        /// <summary>
        /// Set or get whether column headers are visible
        /// </summary>
        /// <value>They are not visible by default</value>
        [DefaultValue(false)]
        public new bool ColumnHeadersVisible
        {
            get { return base.ColumnHeadersVisible; }
            set { base.ColumnHeadersVisible = value; }
        }

        /// <summary>
        /// Set or get whether row headers are visible
        /// </summary>
        /// <value>They are not visible by default</value>
        [DefaultValue(false)]
        public new bool RowHeadersVisible
        {
            get { return base.RowHeadersVisible; }
            set { base.RowHeadersVisible = value; }
        }

        /// <summary>
        /// Set or get the header foreground color
        /// </summary>
        /// <value>This defines the default color so that it isn't serialized unnecessarily</value>
        [DefaultValue(typeof(Color), "ControlText")]
        public new Color HeaderForeColor
        {
            get { return base.HeaderForeColor; }
            set { base.HeaderForeColor = value; }
        }

        /// <summary>
        /// Set or get the width of the row headers
        /// </summary>
        /// <value>They are set to 20 pixels wide by default</value>
        [DefaultValue(20)]
        public new int RowHeaderWidth
        {
            get { return base.RowHeaderWidth; }
            set { base.RowHeaderWidth = value; }
        }
        #endregion

        #region Hidden properties and events
        //=====================================================================

        // The following properties are not used by the combo box

        /// <summary>
        /// Do not allow sorting
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false),
          EditorBrowsable(EditorBrowsableState.Never)]
        public new bool AllowSorting
        {
            get { return false; }
        }

        /// <summary>
        /// The data grid is settable but is not visible to the user
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false),
          EditorBrowsable(EditorBrowsableState.Never), DefaultValue(null)]
        public override DataGrid DataGrid
        {
            get { return base.DataGrid; }
            set { base.DataGrid = value; }
        }

        /// <summary>
        /// Links are not used
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false),
          EditorBrowsable(EditorBrowsableState.Never)]
        public new Color LinkColor
        {
            get { return SystemColors.HotTrack; }
        }

        /// <summary>
        /// Links are not used
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false),
          EditorBrowsable(EditorBrowsableState.Never)]
        public new Color LinkHoverColor
        {
            get { return SystemColors.HotTrack; }
        }

        /// <summary>
        /// The mapping name is settable but isn't visible to the user
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false),
          EditorBrowsable(EditorBrowsableState.Never), DefaultValue("")]
        public new string MappingName
        {
            get { return base.MappingName; }
            set { base.MappingName = value; }
        }

        /// <summary>
        /// The drop-down table style is always read-only
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false),
          EditorBrowsable(EditorBrowsableState.Never)]
        public override bool ReadOnly
        {
            get { return true; }
            set { }
        }
        #endregion

        #region New properties
        //=====================================================================

        /// <summary>
        /// Set or get the default null text for all columns
        /// </summary>
        /// <value>The default is an empty string.  The value is applied at runtime and its effect is not visible
        /// on columns during design time.  It replaces the null text for any column with its <c>NullText</c>
        /// property set to "(null)".</value>
        [Category("Appearance"), DefaultValue(""), Localizable(true),
          Description("The default text to display for columns with null values")]
        public string DefaultNullText { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>Column and row headers are made invisible by default</remarks>
        public DropDownTableStyle()
        {
            base.ColumnHeadersVisible = base.RowHeadersVisible = base.AllowSorting = false;
            base.ReadOnly = true;
            base.RowHeaderWidth = 20;

            this.DefaultNullText = String.Empty;
        }
        #endregion
    }
}
