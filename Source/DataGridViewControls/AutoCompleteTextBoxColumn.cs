//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : AutoCompleteTextBoxColumn.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/09/2024
// Note    : Copyright 2008-2024, Eric Woodruff, All rights reserved
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

using System.Drawing.Design;

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
        protected AutoCompleteTextBoxCell AutoCompleteTextBoxCellTemplate => (AutoCompleteTextBoxCell)base.CellTemplate;

        /// <summary>
        /// Gets or sets the template used to create new cells
        /// </summary>
        /// <exception cref="InvalidCastException">This is thrown if the specified cell template is not an
        /// <see cref="AutoCompleteTextBoxCell"/>.</exception>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;
            set
            {
                AutoCompleteTextBoxCell? cell = value as AutoCompleteTextBoxCell;

                if(value != null && cell == null)
                    throw new InvalidCastException(LR.GetString("ExWrongCellTemplateType", "AutoCompleteTextBoxCell"));

                base.CellTemplate = value!;
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
                int count, idx;

                if(this.AutoCompleteTextBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.AutoCompleteTextBoxCellTemplate.AutoCompleteMode = value;

                if(this.DataGridView != null)
                {
                    rows = this.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        if(rows.SharedRow(idx).Cells[this.Index] is AutoCompleteTextBoxCell cell)
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
                int count, idx;

                if(this.AutoCompleteTextBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.AutoCompleteTextBoxCellTemplate.AutoCompleteSource = value;

                if(this.DataGridView != null)
                {
                    rows = this.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        if(rows.SharedRow(idx).Cells[this.Index] is AutoCompleteTextBoxCell cell)
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
        public AutoCompleteStringCollection? AutoCompleteCustomSource
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
                int count, idx;

                if(this.AutoCompleteTextBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.AutoCompleteTextBoxCellTemplate.AutoCompleteCustomSource = value;

                if(this.DataGridView != null)
                {
                    rows = this.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        if(rows.SharedRow(idx).Cells[this.Index] is AutoCompleteTextBoxCell cell)
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
