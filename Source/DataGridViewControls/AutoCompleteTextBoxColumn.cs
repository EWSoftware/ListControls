//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : AutoCompleteTextBoxColumn.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/22/2014
// Note    : Copyright 2008-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a data grid view column that hosts a textbox control and exposes its auto-complete
// features.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 02/07/2008  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view column hosts a textbox control and exposes its auto-complete features
    /// </summary>
    public class AutoCompleteTextBoxColumn : DataGridViewTextBoxColumn
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used internally to get a reference to the cell template as the actual type
        /// </summary>
        protected AutoCompleteTextBoxCell AutoCompleteTextBoxCellTemplate
        {
            get { return (AutoCompleteTextBoxCell)base.CellTemplate; }
        }

        /// <summary>
        /// Gets or sets the template used to create new cells
        /// </summary>
        /// <exception cref="InvalidCastException">This is thrown if the specified cell template is not an
        /// <see cref="AutoCompleteTextBoxCell"/>.</exception>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                AutoCompleteTextBoxCell cell = value as AutoCompleteTextBoxCell;

                if(value != null && cell == null)
                    throw new InvalidCastException(LR.GetString("ExWrongCellTemplateType", "AutoCompleteTextBoxCell"));

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that determines the auto-complete mode
        /// </summary>
        /// <value>The default is <c>None</c>.</value>
        [Category("AutoComplete"), DefaultValue(AutoCompleteMode.None),
          Description("Gets or sets the auto-complete mode")]
        public AutoCompleteMode AutoCompleteMode
        {
            get
            {
                if(this.AutoCompleteTextBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.AutoCompleteTextBoxCellTemplate.AutoCompleteMode;
            }
            set
            {
                DataGridViewRowCollection rows;
                AutoCompleteTextBoxCell cell;
                int count, idx;

                if(this.AutoCompleteTextBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.AutoCompleteTextBoxCellTemplate.AutoCompleteMode = value;

                if(base.DataGridView != null)
                {
                    rows = base.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        cell = rows.SharedRow(idx).Cells[base.Index] as AutoCompleteTextBoxCell;

                        if(cell != null)
                            cell.AutoCompleteMode = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that determines the auto-complete source
        /// </summary>
        /// <value>The default is <c>None</c>.</value>
        [Category("AutoComplete"), DefaultValue(AutoCompleteSource.None),
          Description("Gets or sets the auto-complete source")]
        public AutoCompleteSource AutoCompleteSource
        {
            get
            {
                if(this.AutoCompleteTextBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.AutoCompleteTextBoxCellTemplate.AutoCompleteSource;
            }
            set
            {
                DataGridViewRowCollection rows;
                AutoCompleteTextBoxCell cell;
                int count, idx;

                if(this.AutoCompleteTextBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.AutoCompleteTextBoxCellTemplate.AutoCompleteSource = value;

                if(base.DataGridView != null)
                {
                    rows = base.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        cell = rows.SharedRow(idx).Cells[base.Index] as AutoCompleteTextBoxCell;

                        if(cell != null)
                            cell.AutoCompleteSource = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a custom auto-completion source.
        /// </summary>
        [Category("AutoComplete"), Localizable(true), Browsable(true),
          Description("Gets or sets a custom auto-completion source"),
          Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
          EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get
            {
                if(this.AutoCompleteTextBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.AutoCompleteTextBoxCellTemplate.AutoCompleteCustomSource;
            }
            set
            {
                DataGridViewRowCollection rows;
                AutoCompleteTextBoxCell cell;
                int count, idx;

                if(this.AutoCompleteTextBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.AutoCompleteTextBoxCellTemplate.AutoCompleteCustomSource = value;

                if(base.DataGridView != null)
                {
                    rows = base.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        cell = rows.SharedRow(idx).Cells[base.Index] as AutoCompleteTextBoxCell;

                        if(cell != null)
                            cell.AutoCompleteCustomSource = value;
                    }
                }
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public AutoCompleteTextBoxColumn() : base()
        {
            base.CellTemplate = new AutoCompleteTextBoxCell();
        }
        #endregion
    }
}
