//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DataList.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This file contains a control that allows you to specify a user control template to display and edit
// information from a data source similar in nature to the DataList web server control, the sub-form control or
// continuous forms detail section in a Microsoft Access form, or the DataRepeater control from Visual Basic 6.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/20/2005  EFW  Created the code
// 12/19/2005  EFW  Various updates and fixes
// 02/19/2005  EFW  Improved scrolling, added page up/down support, and added find methods
//===============================================================================================================

// Ignore Spelling: typeof

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
	/// <summary>
	/// This control allows you to specify a user control template to display and edit information from a data
    /// source similar in nature to the <c>DataList</c> web server control, the sub-form control or continuous
    /// forms detail section in a Microsoft Access form, or the <c>DataRepeater</c> control from Visual Basic 6.
	/// </summary>
    [DefaultEvent("Current"), DefaultProperty("DataSource"),
      Description("A template-based list control used for displaying or editing information in a data source")]
	public class DataList : UserControl
	{
        #region Private data members
        //=====================================================================

        private RowPanel pnlRows;
        private ImageList ilButtons;
        private Button btnFirst;
        private Button btnPrev;
        private Button btnNext;
        private Button btnLast;
        internal Button btnAdd;
        private NumericTextBox txtRowNum;
        private Label lblRowCount;
        internal Button btnDelete;
        private Timer tmrRepeat;
        private ClickableLabel lblCaption;
        private IContainer components;

        // Border properties
        private int borderWidth;

        // Separator properties
        private Color sepColor;
        private int sepHeight;
        private bool showSep;

        // Add/Delete properties
        private bool showNav, showAddDel, showCaption;
        private Shortcut shortcutAdd, shortcutDel, shortcutRowNum, shortcutSwitchSection;

        // Auto-repeat and selection properties
        private Button repeatButton;
        private Point lastMousePos;
        private DragMode dragMode;
        private int repeatWait, repeatInterval, selStart, selEnd, lastMouseRow;
        private bool autoRepeating;

        // Template and data source information
        private Type rowTemplate, headerTemplate, footerTemplate;
        private TemplateControl header, footer;

        private object dataSource;
        private string dataMember;
        private CurrencyManager listManager;

        private bool inSetListManager, inAddRow, inDelRow, inBindData, isUndoing, isBinding;

        private Hashtable sharedDataSources;

        // Display properties
        private readonly SolidBrush brHeaderBack, brHeaderFore, brSelBack, brSelFore;
        private bool rowHeadersVisible, rowHeadersFlat, suppressLayout;
        private int rowHeight, rowHeaderWidth, currentRow, headerFooterLeft;
        private readonly Point[] ptsCurrent, ptsNewRow;

        // Change policy.  These settings determine how the data source can be modified.
        private readonly ChangePolicy changePolicy;
        private bool allowAdditions, allowEdits, allowDeletes;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This property is used to set or get the border style.
        /// </summary>
        /// <remarks>The default is to use a fixed 3D border.</remarks>
        [DefaultValue(BorderStyle.Fixed3D)]
        public new BorderStyle BorderStyle
        {
            get => base.BorderStyle;
            set => base.BorderStyle = value;
        }

        /// <summary>
        /// This returns the <see cref="CurrencyManager"/> that the data list is currently using to get data from
        /// the <see cref="DataSource"/>/<see cref="DataMember"/> pair.
        /// </summary>
        [Browsable(false), Description("Returns the CurrencyManager that the data list is currently using to " +
          "get data from the DataSource/DataMember pair")]
        public CurrencyManager ListManager
        {
            get
            {
                if(listManager == null && this.Parent != null && this.BindingContext != null && dataSource != null)
                    return (CurrencyManager)this.BindingContext[dataSource, dataMember];

                return this.listManager;
            }
        }

        /// <summary>
        /// This property is used to set or get whether or not the caption is displayed
        /// </summary>
        /// <value>The caption is not visible by default</value>
        [Category("Appearance"), DefaultValue(false), Description("Show or hide the caption")]
        public bool CaptionVisible
        {
            get => showCaption;
            set
            {
                if(showCaption != value)
                {
                    showCaption = lblCaption.Visible = value;
                    this.PerformLayout();
                    this.Invalidate();
                    this.Update();
                }
            }
        }

        /// <summary>
        /// This property is used to set or get the caption text
        /// </summary>
        [Category("Appearance"), DefaultValue(""), Description("Get or set the caption text")]
        public string CaptionText
        {
            get => lblCaption.Text;
            set
            {
                lblCaption.Text = value;
                this.Invalidate();
                this.Update();
            }
        }

        /// <summary>
        /// This property is used to set or get the caption background color
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "ActiveCaption"),
          Description("Get or set the caption background color")]
        public Color CaptionBackColor
        {
            get => lblCaption.BackColor;
            set
            {
                lblCaption.BackColor = value;
                this.Invalidate();
                this.Update();
            }
        }

        /// <summary>
        /// This property is used to set or get the caption foreground color
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "ActiveCaptionText"),
          Description("Get or set the caption foreground color")]
        public Color CaptionForeColor
        {
            get => lblCaption.ForeColor;
            set
            {
                lblCaption.ForeColor = value;
                this.Invalidate();
                this.Update();
            }
        }

        /// <summary>
        /// This property is used to set or get the caption font
        /// </summary>
        [Category("Appearance"), Description("The font to use for the caption text")]
        public Font CaptionFont
        {
            get => lblCaption.Font;
            set
            {
                if(value == null)
                    lblCaption.Font = new Font(Control.DefaultFont, FontStyle.Bold);
                else
                    lblCaption.Font = value;

                this.PerformLayout();
                this.Invalidate();
                this.Update();
            }
        }

        /// <summary>
        /// This property is used to set or get whether or not the navigation controls are displayed
        /// </summary>
        /// <value>The controls are shown by default</value>
        [Category("Appearance"), Bindable(true), DefaultValue(true),
          Description("Show or hide the navigation controls")]
        public bool NavigationControlsVisible
        {
            get => showNav;
            set
            {
                if(showNav != value)
                {
                    showNav = btnFirst.Visible = btnPrev.Visible = txtRowNum.Visible = btnNext.Visible =
                        btnLast.Visible = lblRowCount.Visible = value;

                    btnAdd.Visible = btnDelete.Visible = (value && showAddDel);

                    OnNavigationControlsVisibleChanged(EventArgs.Empty);
                    this.PerformLayout();
                }
            }
        }

        /// <summary>
        /// This property is used to set or get whether or not the add and delete buttons are displayed
        /// </summary>
        /// <value>The buttons are shown by default.  They can be hidden if you would prefer to handle add and
        /// delete operations via some other means such as other buttons on your form or template.</value>
        [Category("Appearance"), Bindable(true), DefaultValue(true),
          Description("Show or hide the add and delete buttons")]
        public bool AddDeleteButtonsVisible
        {
            get => showAddDel;
            set
            {
                if(showAddDel != value)
                {
                    showAddDel = value;

                    if(value)
                    {
                        btnAdd.Visible = btnDelete.Visible = showNav;
                        lblRowCount.Left = btnDelete.Left + btnDelete.Width + 5;
                    }
                    else
                    {
                        btnAdd.Visible = btnDelete.Visible = false;
                        lblRowCount.Left = btnLast.Left + btnLast.Width + 5;
                    }

                    OnAddDeleteButtonsVisibleChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// This property is used to set or get whether or not separators are displayed between rows
        /// </summary>
        /// <value>Separators are drawn by default</value>
        [Category("Appearance"), Bindable(true), DefaultValue(true),
          Description("Show or hide the separators between rows")]
        public bool SeparatorsVisible
        {
            get => showSep;
            set
            {
                if(showSep != value)
                {
                    showSep = value;
                    ControlCollection cc = pnlRows.Controls;

                    if(cc.Count != 0)
                    {
                        Separator sep;
                        Control c;
                        int idx, top, width;

                        pnlRows.SuspendLayout();

                        if(showSep)
                        {
                            if(cc[0].Width < pnlRows.Width)
                                width = pnlRows.Width;
                            else
                                width = cc[0].Width;

                            width += (pnlRows.AutoScrollPosition.X * -1);
                            top = pnlRows.AutoScrollPosition.Y + rowHeight;
                            rowHeight += sepHeight;

                            // Insert the separators
                            for(idx = 1; idx <= cc.Count; idx += 2, top += rowHeight)
                            {
                                sep = new Separator(sepColor, sepHeight, width, pnlRows.AutoScrollPosition.X, top);
                                cc.Add(sep);
                                cc.SetChildIndex(sep, idx);
                            }

                            // Reposition the row templates
                            for(top = pnlRows.AutoScrollPosition.Y, idx = 0; idx < cc.Count; idx += 2, top += rowHeight)
                                cc[idx].Top = top;
                        }
                        else
                        {
                            rowHeight -= sepHeight;

                            // Remove the separators
                            for(idx = 1; idx < cc.Count; idx++)
                            {
                                c = cc[idx];
                                cc.RemoveAt(idx);
                                c.Dispose();
                            }

                            // Reposition the row templates
                            for(top = pnlRows.AutoScrollPosition.Y, idx = 0; idx < cc.Count; idx++, top += rowHeight)
                                cc[idx].Top = top;
                        }

                        pnlRows.ResumeLayout();
                    }

                    OnSeparatorsVisibleChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// This property is used to set or get the color of the separators when the <see cref="SeparatorsVisible"/>
        /// property is true.
        /// </summary>
        /// <value>The default color is black</value>
        [Category("Appearance"), Bindable(true), DefaultValue(typeof(Color), "Black"),
         Description("The color of the separator between rows")]
        public Color SeparatorColor
        {
            get => sepColor;
            set
            {
                sepColor = value;

                ControlCollection cc = pnlRows.Controls;

                if(showSep && cc.Count != 0)
                    for(int nIdx = 1; nIdx < cc.Count; nIdx += 2)
                        cc[nIdx].BackColor = value;

                OnSeparatorColorChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// This property is used to set or get the height of the separators when the <see cref="SeparatorsVisible"/>
        /// property is true.
        /// </summary>
        /// <value>The default height is 1 pixel</value>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the value is not between 1 and 20</exception>
        [Category("Appearance"), Bindable(true), DefaultValue(1),
          Description("The height of the separator between rows")]
        public int SeparatorHeight
        {
            get => sepHeight;
            set
            {
                int top, idx, templateHeight;

                if(value < 1 || value > 20)
                    throw new ArgumentOutOfRangeException(nameof(value), value, LR.GetString("ExInvalidSepHeight"));

                if(showSep)
                    rowHeight = rowHeight - sepHeight + value;

                sepHeight = value;

                ControlCollection cc = pnlRows.Controls;

                if(showSep && cc.Count != 0)
                {
                    this.CalculateGlyphPoints();

                    pnlRows.SuspendLayout();
                    templateHeight = rowHeight - value;

                    // Reposition the row templates
                    for(top = pnlRows.AutoScrollPosition.Y, idx = 0; idx < cc.Count; idx += 2, top += rowHeight)
                    {
                        cc[idx].Top = top;
                        cc[idx + 1].Top = top + templateHeight;
                        cc[idx + 1].Height = sepHeight;
                    }

                    pnlRows.ResumeLayout();
                }

                OnSeparatorHeightChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// This read-only property can be used to get the row height
        /// including the separator.
        /// </summary>
        /// <remarks>The return value is undefined if no template and/or data source has been specified</remarks>
        [Browsable(false), Description("Get the row height including the separator")]
        public int RowHeight => rowHeight;

        /// <summary>
        /// This property is used to set or get whether or not row headers are displayed in front of each row
        /// </summary>
        [Category("Appearance"), Bindable(true), DefaultValue(true),
          Description("Show or hide the row headers")]
        public bool RowHeadersVisible
        {
            get => rowHeadersVisible;
            set
            {
                rowHeadersVisible = value;
                OnRowHeadersVisibleChanged(EventArgs.Empty);
                this.PerformLayout();
            }
        }

        /// <summary>
        /// This property is used to set or get whether to draw the row headers using a flat style
        /// </summary>
        [Category("Appearance"), Bindable(true), DefaultValue(false),
          Description("Determines whether or not row headers are drawn using a flat style")]
        public bool RowHeadersFlat
        {
            get => rowHeadersFlat;
            set
            {
                rowHeadersFlat = value;
                OnRowHeadersFlatChanged(EventArgs.Empty);

                if(rowHeadersVisible)
                    this.Invalidate();
            }
        }

        /// <summary>
        /// This property is used to set or get the width of the row headers when the <see cref="RowHeadersVisible"/>
        /// property is true.
        /// </summary>
        [Category("Appearance"), Bindable(true), DefaultValue(20), Description("Set the width of the row headers")]
        public int RowHeaderWidth
        {
            get => rowHeaderWidth;
            set
            {
                rowHeaderWidth = value;
                OnRowHeaderWidthChanged(EventArgs.Empty);

                if(pnlRows.Controls.Count != 0)
                    this.CalculateGlyphPoints();

                if(rowHeadersVisible)
                    this.PerformLayout();
            }
        }

        /// <summary>
        /// This property is used to set or get the row header's background color
        /// </summary>
        [Category("Appearance"), Bindable(true), DefaultValue(typeof(Color), "Control"),
         Description("The row header's background color")]
        public Color RowHeaderBackColor
        {
            get => brHeaderBack.Color;
            set
            {
                brHeaderBack.Color = value;
                OnRowHeaderBackColorChanged(EventArgs.Empty);

                if(rowHeadersVisible)
                    this.Invalidate();
            }
        }

        /// <summary>
        /// This property is used to set or get the row header's foreground color
        /// </summary>
        [Category("Appearance"), Bindable(true), DefaultValue(typeof(Color), "ControlDarkDark"),
         Description("The row header's foreground color")]
        public Color RowHeaderForeColor
        {
            get => brHeaderFore.Color;
            set
            {
                brHeaderFore.Color = value;
                OnRowHeaderForeColorChanged(EventArgs.Empty);

                if(rowHeadersVisible)
                    this.Invalidate();
            }
        }

        /// <summary>
        /// This property is used to set or get the background color for selected row headers
        /// </summary>
        [Category("Appearance"), Bindable(true), DefaultValue(typeof(Color), "ControlDark"),
         Description("The background color for selected row headers")]
        public Color SelectionBackColor
        {
            get => brSelBack.Color;
            set
            {
                brSelBack.Color = value;
                OnSelectionBackColorChanged(EventArgs.Empty);

                if(rowHeadersVisible)
                    this.Invalidate();
            }
        }

        /// <summary>
        /// This property is used to set or get the foreground color for selected row headers
        /// </summary>
        [Category("Appearance"), Bindable(true), DefaultValue(typeof(Color), "Control"),
         Description("The foreground color for selected row headers")]
        public Color SelectionForeColor
        {
            get => brSelFore.Color;
            set
            {
                brSelFore.Color = value;
                OnSelectionForeColorChanged(EventArgs.Empty);

                if(rowHeadersVisible)
                    this.Invalidate();
            }
        }

        /// <summary>
        /// This is used to determine whether or not additions are allowed to be made to the data source
        /// </summary>
        /// <value>If set to true, the data source may override it if it does not allow additions</value>
        [Category("Behavior"), Description("Determines whether or not additions can be made to the data source")]
        public bool AllowAdditions
        {
            get => changePolicy.AllowAdditions;
            set
            {
                TemplateControl tc;
                Control c;
                int lastTemplate;

                allowAdditions = value;
                changePolicy.UpdatePolicy(value, allowEdits, allowDeletes);

                if(listManager != null)
                {
                    if(!changePolicy.AllowAdditions)
                    {
                        // Remove the new row template if it is there
                        lastTemplate = pnlRows.Controls.Count - 1;

                        if(showSep)
                            lastTemplate--;

                        if(lastTemplate > -1)
                        {
                            tc = (TemplateControl)pnlRows.Controls[lastTemplate];

                            if(tc.IsNewRowInternal)
                            {
                                // If bound, get rid of the temporary new row
                                if(tc.IsDataBound)
                                {
                                    this.CancelChanges();
                                    tc.WirePropChangedEvents(false);
                                }

                                pnlRows.Controls.RemoveAt(lastTemplate);
                                tc.Dispose();

                                if(showSep)
                                {
                                    c = pnlRows.Controls[lastTemplate];
                                    pnlRows.Controls.RemoveAt(lastTemplate);
                                    c.Dispose();
                                }
                            }
                        }

                        if(listManager.Position == listManager.Count - 1)
                            btnNext.Enabled = false;
                    }
                    else
                        this.AddNewRowTemplate();

                    btnDelete.Enabled = (changePolicy.AllowDeletes && listManager.Count > 0);
                }
            }
        }

        /// <summary>
        /// This is used to determine whether or not edits are allowed to be made to the data source
        /// </summary>
        /// <value>If set to true, the data source may override it if it does not allow edits.  Note that it is
        /// up to the row template to check this property and disable editing in its controls if this is set to
        /// false.</value>
        [Category("Behavior"), Description("Determines whether or not edits can be made to the data source")]
        public bool AllowEdits
        {
            get => changePolicy.AllowEdits;
            set
            {
                allowEdits = value;
                changePolicy.UpdatePolicy(allowAdditions, value, allowDeletes);
            }
        }

        /// <summary>
        /// This is used to determine whether or not deletes are allowed to be made to the data source
        /// </summary>
        /// <value>If set to true, the data source may override it if it does not allow deletions</value>
        [Category("Behavior"), Description("Determines whether or not deletes can be made to the data source")]
        public bool AllowDeletes
        {
            get => changePolicy.AllowDeletes;
            set
            {
                allowDeletes = value;
                changePolicy.UpdatePolicy(allowAdditions, allowEdits, value);
            }
        }

        /// <summary>
        /// This is used to set or get the shortcut key to use for jumping to the "new row" template
        /// </summary>
        /// <remarks>The default is <c>Ctrl+Shift+A</c>.  It is ignored if additions are not allowed</remarks>
        [Category("Behavior"), DefaultValue(typeof(Shortcut), "CtrlShiftA"),
         Description("The shortcut key to use for jumping to the \"new row\" template")]
        public Shortcut AddRowShortcut
        {
            get => shortcutAdd;
            set => shortcutAdd = value;
        }

        /// <summary>
        /// This is used to set or get the shortcut key to use for deleting a row
        /// </summary>
        /// <remarks>The default is <c>Ctrl+Shift+D</c>.  It is ignored if deletes are not allowed</remarks>
        [Category("Behavior"), DefaultValue(typeof(Shortcut), "CtrlShiftD"),
         Description("The shortcut key to use for deleting a row")]
        public Shortcut DeleteRowShortcut
        {
            get => shortcutDel;
            set => shortcutDel = value;
        }

        /// <summary>
        /// This is used to set or get the shortcut key to use for jumping to the row number navigation text box
        /// </summary>
        /// <remarks>The default is <c>F5</c>.  It is ignored if the navigation controls are not visible</remarks>
        [Category("Behavior"), DefaultValue(typeof(Shortcut), "F5"),
         Description("The shortcut key to use for jumping to the row number navigation text box")]
        public Shortcut RowNumberNavShortcut
        {
            get => shortcutRowNum;
            set => shortcutRowNum = value;
        }

        /// <summary>
        /// This is used to set or get the shortcut key to use for jumping between the header, detail, and footer
        /// sections of the control.
        /// </summary>
        /// <remarks>The default is <c>F6</c>.  It is ignored if no header and footer are specified.  A section
        /// will still be given the focus even if there are no controls within it that will accept the focus.</remarks>
        [Category("Behavior"), DefaultValue(typeof(Shortcut), "F6"),
         Description("The shortcut key to use for jumping between the header, detail, and footer sections")]
        public Shortcut SwitchSectionShortcut
        {
            get => shortcutSwitchSection;
            set => shortcutSwitchSection = value;
        }

        /// <summary>
        /// This property is used to set or get the initial wait in milliseconds before the <c>Next</c> and
        /// <c>Previous</c> buttons auto-repeat when clicked and held to navigate through the data source.
        /// </summary>
        /// <value>The default is 500 milliseconds.  The delay cannot be set to less than 100 milliseconds</value>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the value is less than 100</exception>
        [Category("Behavior"), Bindable(true), DefaultValue(500),
          Description("The initial delay before the Next and Previous buttons auto-repeat")]
        public int RepeatWait
        {
            get => repeatWait;
            set
            {
                if(value < 100)
                    throw new ArgumentOutOfRangeException(nameof(value), value, LR.GetString("ExInvalidRepeatWait"));

                repeatWait = value;
                OnRepeatWaitChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// This property is used to set or get the repeat delay in milliseconds for the <c>Next</c> and
        /// <c>Previous</c> buttons when they are clicked and held down to navigate through the data source.
        /// </summary>
        /// <value>The default is 50 milliseconds.  The delay cannot be set to less than 20 milliseconds</value>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the value is less than 20</exception>
        [Category("Behavior"), Bindable(true), DefaultValue(50),
          Description("The auto-repeat delay for the Next and Previous buttons")]
        public int RepeatInterval
        {
            get => repeatInterval;
            set
            {
                if(value < 20)
                    throw new ArgumentOutOfRangeException(nameof(value), value, LR.GetString("ExInvalidRepeatInterval"));

                repeatInterval = value;
                OnRepeatIntervalChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// This property is used to set or get the zero-based starting row number of the currently selected
        /// range of rows.
        /// </summary>
        /// <value>This returns the zero-based starting row number of the selection range. If there is no
        /// selected row range, it returns -1.  If <see cref="SelectionEnd"/> has not been set when this property
        /// is set, the ending selection is set to the same value.  If the value is not within the bounds of the
        /// current row count, it is adjusted to be valid.  If the new value is greater than the current ending
        /// value, the values are swapped so that the range always starts at the lowest row number and goes to
        /// the highest row number.  Setting it to -1 clears the selection.</value>
        [Category("Appearance"), DefaultValue(-1), RefreshProperties(RefreshProperties.Repaint),
          Description("The start of the selected row range")]
        public int SelectionStart
        {
            get => selStart;
            set => this.Select(value, selEnd, value);
        }

        /// <summary>
        /// This property is used to set or get the zero-based ending row number of the currently selected range
        /// of rows.
        /// </summary>
        /// <value>This returns the zero-based ending row number of the selection range. If there is no selected
        /// row range, it returns -1.  If <see cref="SelectionStart"/> has not been set when this property is
        /// set, the starting selection is set to the same value.  If the value is not within the bounds of the
        /// current row count, it is adjusted to be valid.  If the new value is less than the current starting
        /// value, the values are swapped so that the range always starts at the lowest row number and goes to
        /// the highest row number.  Setting it to -1 clears the selection.</value>
        [Category("Appearance"), DefaultValue(-1), RefreshProperties(RefreshProperties.Repaint),
          Description("The end of the selected row range")]
        public int SelectionEnd
        {
            get => selEnd;
            set => this.Select(selStart, value, value);
        }

        /// <summary>
        /// This gets or sets the data source for the data list
        /// </summary>
        /// <value>The data source object must support the <see cref="IList"/> interface such as a
        /// <see cref="System.Data.DataSet"/> or an <see cref="Array"/>.  This property must be set in order for
        /// the control to display information.  If the data source contains multiple items to which the control
        /// can bind, use the <see cref="DataMember"/> property to specify the sub-list to use.</value>
        /// <exception cref="ArgumentException">This is thrown if the data source does not support the
        /// <see cref="IList"/> interface.</exception>
        [Category("Data"), DefaultValue(null), RefreshProperties(RefreshProperties.Repaint),
          AttributeProvider(typeof(IListSource)),
          TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
          Description("Set or get the data source for the data list")]
        public object DataSource
        {
            get => dataSource;
            set
            {
                if(value != null && !(value is IList) && !(value is IListSource))
                    throw new ArgumentException(LR.GetString("ExBadDataSource"));

                if(dataSource != value)
                {
                    if(dataSource != null && listManager != null && dataMember.IndexOf('.') != -1)
                        WireUpRelatedDataSourceEvents(false);

                    if((value == null || value == Convert.DBNull) && dataMember.Length != 0)
                    {
                        dataSource = null;
                        this.DataMember = String.Empty;
                    }
                    else
                    {
                        this.EnforceValidDataMember(value, dataMember);
                        this.SetListManager(value, dataMember, false);
                    }
                }
            }
        }

        /// <summary>
        /// This indicates the sub-list (if any) of the <see cref="DataSource"/> to show in the data list
        /// </summary>
        [Category("Data"), DefaultValue(""),
          Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
          Description("Indicates a sub-list of the data source to show in the data list")]
        public string DataMember
        {
            get => dataMember;
            set
            {
                if(dataMember != value)
                {
                    if(dataSource != null && listManager != null && dataMember.IndexOf('.') != -1)
                        WireUpRelatedDataSourceEvents(false);

                    dataMember = value;
                    this.EnforceValidDataMember(dataSource, value);
                    this.SetListManager(dataSource, value, false);
                }
            }
        }

        /// <summary>
        /// The template control type to use for rows in the data source
        /// </summary>
        /// <remarks>One row template will be created for each row in the data source.  Rows are initialized and
        /// bound as they are scrolled into view to improve performance.  This property must be set in order to
        /// edit information in the data source.</remarks>
        /// <exception cref="ArgumentException">This is thrown if the specified type is not derived from
        /// <see cref="TemplateControl"/>.</exception>
        /// <example>
        /// <code language="cs">
        /// // AddressTemplate is a user control derived from TemplateControl
        /// dataList.RowTemplate = typeof(AddressTemplate);
        /// </code>
        /// <code language="vbnet">
        /// ' AddressTemplate is a user control derived from TemplateControl
        /// dataList.RowTemplate = GetType(AddressTemplate)
        /// </code>
        /// </example>
        [Browsable(false), Description("The template control to use for the rows"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type RowTemplate
        {
            get => rowTemplate;
            set
            {
                if(rowTemplate != value)
                {
                    if(value != null && !value.IsSubclassOf(typeof(TemplateControl)))
                        throw new ArgumentException(LR.GetString("ExInvalidDataListTemplateType"));

                    rowTemplate = value;
                    this.BindData();
                }
            }
        }

        /// <summary>
        /// The template control type to use for the header
        /// </summary>
        /// <remarks>The header template is bound to the data source as a whole rather than an individual row.
        /// If not set, a header section will not be shown.</remarks>
        /// <exception cref="ArgumentException">This is thrown if the specified type is not derived from
        /// <see cref="TemplateControl"/>.</exception>
        /// <example>
        /// <code language="cs">
        /// // AddressHeader is a user control derived from TemplateControl
        /// dataList.HeaderTemplate = typeof(AddressHeader);
        /// </code>
        /// <code language="vbnet">
        /// ' AddressHeader is a user control derived from TemplateControl
        /// dataList.HeaderTemplate = GetType(AddressHeader)
        /// </code>
        /// </example>
        [Browsable(false), Description("The template control to use for the header"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type HeaderTemplate
        {
            get => headerTemplate;
            set
            {
                if(headerTemplate != value)
                {
                    if(value != null && !value.IsSubclassOf(typeof(TemplateControl)))
                        throw new ArgumentException(LR.GetString("ExInvalidDataListTemplateType"));

                    // Dispose of the old header if there is one
                    if(header != null)
                    {
                        this.Controls.Remove(header);
                        header.Dispose();
                        header = null;
                    }

                    headerTemplate = value;

                    if(value != null)
                    {
                        // Create, initialize, and bind the header control
                        ConstructorInfo ctor = headerTemplate.GetConstructor(Type.EmptyTypes);
                        header = (TemplateControl)ctor.Invoke(null);
                        header.TabStop = false;
                        header.TemplateParentInternal = this;

                        // Inherit this control's binding context
                        header.BindingContext = null;

                        this.Controls.Add(header);
                        header.InitializeTemplate();
                        header.HasBeenInitialized = true;

                        if(dataSource != null)
                        {
                            header.SetRowSourceInternal(dataSource);
                            header.Bind();
                            header.HasBeenBound = true;
                            OnHeaderDataBound(new DataListEventArgs(-1, header));
                        }
                    }

                    PerformLayout();
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// The template control type to use for the footer
        /// </summary>
        /// <remarks>The footer template is bound to the data source as a whole rather than an individual row.
        /// If not set, a footer section will not be shown.</remarks>
        /// <exception cref="ArgumentException">This is thrown if the specified type is not derived from
        /// <see cref="TemplateControl"/>.</exception>
        /// <example>
        /// <code language="cs">
        /// // AddressFooter is a user control derived from TemplateControl
        /// dataList.FooterTemplate = typeof(AddressFooter);
        /// </code>
        /// <code language="vbnet">
        /// ' AddressFooter is a user control derived from TemplateControl
        /// dataList.FooterTemplate = GetType(AddressFooter)
        /// </code>
        /// </example>
        [Browsable(false), Description("The template control to use for the footer"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type FooterTemplate
        {
            get => footerTemplate;
            set
            {
                if(footerTemplate != value)
                {
                    if(value != null && !value.IsSubclassOf(typeof(TemplateControl)))
                        throw new ArgumentException(LR.GetString("ExInvalidDataListTemplateType"));

                    // Dispose of the old header if there is one
                    if(footer != null)
                    {
                        this.Controls.Remove(footer);
                        footer.Dispose();
                        footer = null;
                    }

                    footerTemplate = value;

                    if(value != null)
                    {
                        // Create, initialize, and bind the footer control
                        ConstructorInfo ctor = footerTemplate.GetConstructor(Type.EmptyTypes);
                        footer = (TemplateControl)ctor.Invoke(null);
                        footer.TabStop = false;
                        footer.TemplateParentInternal = this;

                        // Inherit this control's binding context
                        footer.BindingContext = null;

                        this.Controls.Add(footer);
                        footer.InitializeTemplate();
                        footer.HasBeenInitialized = true;

                        if(dataSource != null)
                        {
                            footer.SetRowSourceInternal(dataSource);
                            footer.Bind();
                            footer.HasBeenBound = true;
                            OnFooterDataBound(new DataListEventArgs(-1, footer));
                        }
                    }

                    PerformLayout();
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// This read-only property can be used to get the current row count from the data source
        /// </summary>
        [Browsable(false), Description("Get the row count from the data source")]
        public int RowCount => (listManager == null) ? 0 : listManager.Count;

        /// <summary>
        /// This read-only property is used to get the zero-based row number of the currently selected row item
        /// </summary>
        /// <value>If there is no data source or there are no rows in the data source, it returns -1</value>
        /// <remarks>To set the current row, use the <see cref="MoveTo(Int32)"/> method</remarks>
        [Browsable(false), Description("Get the currently selected row index")]
        public int CurrentRow
        {
            get
            {
                if(listManager == null || listManager.Count == 0)
                    return -1;

                return currentRow;
            }
        }

        /// <summary>
        /// This is used to get a reference to the current item's row template
        /// </summary>
        /// <value>This will return null if there is no data source or if there are no rows in the data source</value>
        [Browsable(false), Description("Get a reference to the selected item's row template")]
        public TemplateControl CurrentItem
        {
            get
            {
                if(listManager == null || listManager.Count == 0 || rowTemplate == null)
                    return null;

                int row = currentRow;

                if(showSep)
                    row *= 2;

                if(row >= pnlRows.Controls.Count)
                    return null;

                // During deletion, there might not be a template control in the given position
                return (pnlRows.Controls[row] as TemplateControl);
            }
        }

        /// <summary>
        /// This is used to get a reference to the current header template control if one has been specified
        /// </summary>
        /// <value>This will return null if there is no header template</value>
        [Browsable(false), Description("Get a reference to the header template control")]
        public TemplateControl HeaderControl => header;

        /// <summary>
        /// This is used to get a reference to the current footer template control if one has been specified
        /// </summary>
        /// <value>This will return null if there is no footer template</value>
        [Browsable(false), Description("Get a reference to the footer template control")]
        public TemplateControl FooterControl => footer;

        /// <summary>
        /// This can be used to store data sources that are shared amongst all instances of the row, header, and
        /// footer templates.
        /// </summary>
        /// <returns>The hash table used to store the shared data sources</returns>
        /// <remarks>To conserve resources and speed the loading of the row templates, they can share common data
        /// sources for the controls that they contain such as combo boxes.  This property can be used to store
        /// the shared data sources for easy access and so that you do not have to manage them in the templates.</remarks>
        [Browsable(false), Description("This is used to store data sources that can be shared amongst all " +
            "instances of the row, header, and footer, templates.")]
        public Hashtable SharedDataSources
        {
            get
            {
                if(sharedDataSources == null)
                    sharedDataSources = new Hashtable();

                return sharedDataSources;
            }
        }

        /// <summary>
        /// This property can be used to query the current row template to see if it is valid
        /// </summary>
        /// <value>Returns true if it is valid or there are no items, false if it is not</value>
        /// <remarks>This is useful in situations where the normal validating events are fired after certain
        /// other events (i.e. tree view and data grid item selection events).</remarks>
        /// <example>
        /// <code language="cs">
        /// // Prevent a change of the tree view node if the current row
        /// // template in the data list is not valid.
        /// private void tree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        /// {
        ///     e.Cancel = !dataList.IsValid;
        /// }
        /// </code>
        /// <code language="vbnet">
        /// ' Prevent a change of the tree view node if the current row
        /// ' template in the data list is not valid.
        /// Private Sub tree_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) _
        ///   Handles tree.BeforeSelect
        ///     e.Cancel = Not dataList.IsValid
        /// End Sub
        /// </code>
        /// </example>
        [Browsable(false), Description("Check to see if the current item is valid")]
        public bool IsValid
        {
            get
            {
                TemplateControl tc = this.CurrentItem;
                return (tc == null || tc.IsValid);
            }
        }

        /// <summary>
        /// This read-only property can be used to see if the data source has been modified
        /// </summary>
        /// <value>The <see cref="CommitChanges"/> method is called first to commit any pending changes to the
        /// data source.  The data list can detect changes only if the data source is a <c>DataSet</c>,
        /// <c>DataView</c>, or a <c>DataTable</c>.  In those cases, it returns true if the data source has been
        /// modified or false if it has not.  For all other data source types, it will always return false.  You
        /// may override this property in order to extend the types that it knows about and detect changes in
        /// them.</value>
        /// <example>
        /// <code language="cs">
        /// // Assume daItems is a data adapter, dsItems is a DataSet and
        /// // dsItems is the data source for dlList (a data list control).
        /// // If the data list has changes, save them.
        /// if(dlList.HasChanges)
        ///     daItems.Update(dsItems);
        /// </code>
        /// <code language="vbnet">
        /// ' Assume daItems is a data adapter, dsItems is a DataSet and
        /// ' dsItems is the data source for dlList (a data list control).
        /// ' If the data list has changes, save them.
        /// If dlList.HasChanges Then
        ///     daItems.Update(dsItems)
        /// End If
        /// </code>
        /// </example>
        [Browsable(false), Description("Check to see if the data source has changes")]
        public virtual bool HasChanges
        {
            get
            {
                if(listManager != null)
                {
                    // Commit any pending changes
                    this.CommitChanges();

                    object dataSource = listManager.List;

                    if(dataSource is DataSet ds)
                        return ds.HasChanges();

                    DataTable tbl;

                    if(dataSource is DataView dv)
                        tbl = dv.Table;
                    else
                        tbl = dataSource as DataTable;

                    // If still null, the data source is not of a type we can use to determine modifications
                    if(tbl != null)
                    {
                        foreach(DataRow r in tbl.Rows)
                        {
                            if(r.RowState != DataRowState.Unchanged)
                                return true;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// This read-only property can be used to see if the data list is in the process of binding to its data
        /// source.
        /// </summary>
        /// <value>This will return true if binding is taking place.  This is useful for suppressing event
        /// handlers that may cause undesirable results if executed while binding to the data source.</value>
        [Browsable(false), Description("Check to see if binding is currently happening")]
        public bool IsBinding => isBinding;

        /// <summary>
        /// This read-only property can be used to see if changes are being undone
        /// </summary>
        /// <value>This will return true if changes are being canceled.  This is useful for suppressing event
        /// handlers that may cause undesirable results if executed while undoing or canceling changes to a row.
        /// </value>
        [Browsable(false), Description("Check to see if changes are being undone")]
        public bool IsUndoing => isUndoing;

        /// <summary>
        /// This can be used to get the value of the specified column in the currently selected item
        /// </summary>
        /// <param name="colName">The column name of the item to get.  This can be any column in the data source,
        /// not just those displayed by the control.</param>
        /// <value>Returns the entry at the specified column in the currently selected item.  If there is no data
        /// source or the column cannot be found, this returns null.</value>
        /// <overloads>There are two overloads for this property</overloads>
        [Browsable(false), Description("Get the specified column from the current row")]
        public object this[string colName] => this[currentRow, colName];

        /// <summary>
        /// This can be used to get the value of the specified column in the specified row of the data list's
        /// data source.
        /// </summary>
        /// <param name="rowIdx">The row index of the item.</param>
        /// <param name="colName">The column name of the item to get.  This can be any column in the data source,
        /// not just those displayed by the control.</param>
        /// <value>Returns the entry at the specified column in the specified row.  If the row is out of bounds
        /// or if the column cannot be found, this will return null.</value>
        [Browsable(false), Description("Get the specified column from the specified row")]
        public object this[int rowIdx, string colName]
        {
            get
            {
                if(rowIdx < 0 || listManager == null || rowIdx >= listManager.Count || colName == null ||
                  colName.Length == 0)
                    return null;

                object oItem = listManager.List[rowIdx];

                if(oItem != null)
                {
                    PropertyDescriptor pd = listManager.GetItemProperties().Find(colName, true);

                    if(pd != null)
                        oItem = pd.GetValue(oItem);
                    else
                        oItem = null;
                }

                return oItem;
            }
        }
        #endregion

        #region Hidden properties and events
        //=====================================================================

        // These properties and events do not apply so they are hidden

#pragma warning disable 0067

        /// <summary>
        /// The data list does not use this property so it is hidden.  It always returns null.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
          Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Image BackgroundImage => null;

        /// <summary>
        /// The data list does not use this property so it is hidden.  It always returns false.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
          Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AutoScroll => false;

        /// <summary>
        /// The data list does not use this property so it is hidden.  It always returns the base margin.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
          Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Size AutoScrollMargin => base.AutoScrollMargin;

        /// <summary>
        /// The data list does not use this property so it is hidden.  It always returns the base size.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
          Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Size AutoScrollMinSize => base.AutoScrollMinSize;

        /// <summary>
        /// The data list does not use the background image so this event is hidden.
        /// </summary>
        /// <exclude/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackgroundImageChanged;

#pragma warning restore 0067
        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when a row item is data bound
        /// </summary>
        [Category("Data"), Description("Occurs when a row item is data bound")]
        public event EventHandler<DataListEventArgs> ItemDataBound;

        /// <summary>
        /// This raises the <see cref="ItemDataBound"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnItemDataBound(DataListEventArgs e)
        {
            ItemDataBound?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the header is data bound
        /// </summary>
        [Category("Data"), Description("Occurs when the header is data bound")]
        public event EventHandler<DataListEventArgs> HeaderDataBound;

        /// <summary>
        /// This raises the <see cref="HeaderDataBound"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnHeaderDataBound(DataListEventArgs e)
        {
            HeaderDataBound?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the footer is data bound
        /// </summary>
        [Category("Data"), Description("Occurs when the footer is data bound")]
        public event EventHandler<DataListEventArgs> FooterDataBound;

        /// <summary>
        /// This raises the <see cref="FooterDataBound"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnFooterDataBound(DataListEventArgs e)
        {
            FooterDataBound?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised just prior to adding an item to the data source
        /// </summary>
        [Category("Data"), Description("Occurs just prior to adding an item to the data source")]
        public event EventHandler<DataListCancelEventArgs> AddingRow;

        /// <summary>
        /// This raises the <see cref="AddingRow"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnAddingRow(DataListCancelEventArgs e)
        {
            AddingRow?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised after adding an item to the data source
        /// </summary>
        [Category("Data"), Description("Occurs after adding an item to the data source")]
        public event EventHandler<DataListEventArgs> AddedRow;

        /// <summary>
        /// This raises the <see cref="AddedRow"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnAddedRow(DataListEventArgs e)
        {
            AddedRow?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised just prior to deleting an item from the data source
        /// </summary>
        [Category("Data"), Description("Occurs just prior to deleting an item from the data source")]
        public event EventHandler<DataListCancelEventArgs> DeletingRow;

        /// <summary>
        /// This raises the <see cref="DeletingRow"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDeletingRow(DataListCancelEventArgs e)
        {
            DeletingRow?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised after deleting an item from the data source
        /// </summary>
        /// <remarks>If there are no more rows after the deletion, the <see cref="NoRows"/> event is also raised</remarks>
        [Category("Data"), Description("Occurs after deleting an item from the data source")]
        public event EventHandler<DataListEventArgs> DeletedRow;

        /// <summary>
        /// This raises the <see cref="DeletedRow"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDeletedRow(DataListEventArgs e)
        {
            DeletedRow?.Invoke(this, e);

            if(listManager != null && listManager.Count == 0)
                OnNoRows(EventArgs.Empty);
        }

        /// <summary>
        /// This event is raised just prior to canceling edits to a row via the Escape key
        /// </summary>
        [Category("Data"), Description("Occurs just prior to canceling edits via the Escape key")]
        public event EventHandler<DataListCancelEventArgs> CancelingEdits;

        /// <summary>
        /// This raises the <see cref="CancelingEdits"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnCancelingEdits(DataListCancelEventArgs e)
        {
            CancelingEdits?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised after canceling edits via the Escape key
        /// </summary>
        [Category("Data"), Description("Occurs after canceling edits via the Escape key")]
        public event EventHandler<DataListEventArgs> CanceledEdits;

        /// <summary>
        /// This raises the <see cref="CanceledEdits"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnCanceledEdits(DataListEventArgs e)
        {
            CanceledEdits?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when a row is made the current row
        /// </summary>
        [Category("Data"), Description("Occurs when a row is made the current row")]
        public event EventHandler<DataListEventArgs> Current;

        /// <summary>
        /// This raises the <see cref="Current"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnCurrent(DataListEventArgs e)
        {
            Current?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised after refreshing the data source or after deleting a row and there are no more
        /// rows in the data source.
        /// </summary>
        /// <remarks>This event can be used to disable bound controls and/or display a message asking the user to
        /// add a new row.</remarks>
        [Category("Data"), Description("Occurs after refresh or deletion when there are no rows in the data source")]
        public event EventHandler NoRows;

        /// <summary>
        /// This raises the <see cref="NoRows"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnNoRows(EventArgs e)
        {
            NoRows?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when a drag and drop operation is initiated
        /// </summary>
        /// <remarks>A drag and drop operation is initiated whenever the mouse button is clicked and held within
        /// an existing selection in the row headers and is then dragged.</remarks>
        [Category("Drag Drop"), Description("Occurs when a drag and drop operation is initiated")]
        public event EventHandler<DataListBeginDragEventArgs> BeginDrag;

        /// <summary>
        /// This raises the <see cref="BeginDrag"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnBeginDrag(DataListBeginDragEventArgs e)
        {
            BeginDrag?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="DataSource"/> is changed
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the control's data source changes")]
        public event EventHandler DataSourceChanged;

        /// <summary>
        /// This raises the <see cref="DataSourceChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDataSourceChanged(EventArgs e)
        {
            DataSourceChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the change policy for the data source is modified (i.e. changes to indicate
        /// whether or not adds, edits, or deletes are allowed).
        /// </summary>
        [Category("Data"), Description("Occurs when the control's change policy is modified")]
        public event EventHandler<ChangePolicyEventArgs> ChangePolicyModified;

        /// <summary>
        /// This raises the <see cref="ChangePolicyModified"/> event for the control and calls the
        /// <see cref="TemplateControl.ChangePolicyModified"> TemplateControl.ChangePolicyModified</see> method
        /// in all initialized rows in the control including any header and footer templates.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnChangePolicyModified(ChangePolicyEventArgs e)
        {
            TemplateControl tc;
            int increment = (showSep) ? 2 : 1;

            // Enable or disable Add and Delete based on the policy
            btnAdd.Enabled = (changePolicy.AllowAdditions && listManager != null);
            btnDelete.Enabled = (changePolicy.AllowDeletes && listManager != null && listManager.Count > 0);

            ChangePolicyModified?.Invoke(this, e);

            // Let all templates know about the change too
            header?.ChangePolicyModified();
            footer?.ChangePolicyModified();

            ControlCollection cc = pnlRows.Controls;

            for(int idx = 0; idx < cc.Count; idx += increment)
            {
                tc = (TemplateControl)cc[idx];

                if(tc.IsInitialized && !tc.IsNewRowInternal)
                    tc.ChangePolicyModified();
            }
        }

        /// <summary>
        /// This event is raised when the <see cref="AddDeleteButtonsVisible"/> property changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the add/delete button visibility changes")]
        public event EventHandler AddDeleteButtonsVisibleChanged;

        /// <summary>
        /// This raises the <see cref="AddDeleteButtonsVisibleChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnAddDeleteButtonsVisibleChanged(EventArgs e)
        {
            AddDeleteButtonsVisibleChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="NavigationControlsVisible"/> property changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the navigation button visibility changes")]
        public event EventHandler NavigationControlsVisibleChanged;

        /// <summary>
        /// This raises the <see cref="NavigationControlsVisibleChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnNavigationControlsVisibleChanged(EventArgs e)
        {
            NavigationControlsVisibleChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="SeparatorsVisible"/> property changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the separator visibility changes")]
        public event EventHandler SeparatorsVisibleChanged;

        /// <summary>
        /// This raises the <see cref="SeparatorsVisibleChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSeparatorsVisibleChanged(EventArgs e)
        {
            SeparatorsVisibleChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="SeparatorColor"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the separator color changes")]
        public event EventHandler SeparatorColorChanged;

        /// <summary>
        /// This raises the <see cref="SeparatorColorChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSeparatorColorChanged(EventArgs e)
        {
            SeparatorColorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="SeparatorHeight"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the separator color changes")]
        public event EventHandler SeparatorHeightChanged;

        /// <summary>
        /// This raises the <see cref="SeparatorHeightChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSeparatorHeightChanged(EventArgs e)
        {
            SeparatorHeightChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="RowHeadersVisible"/> property changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the row header visibility changes")]
        public event EventHandler RowHeadersVisibleChanged;

        /// <summary>
        /// This raises the <see cref="RowHeadersVisibleChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnRowHeadersVisibleChanged(EventArgs e)
        {
            RowHeadersVisibleChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="RowHeadersFlat"/> property changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the row header flat style changes")]
        public event EventHandler RowHeadersFlatChanged;

        /// <summary>
        /// This raises the <see cref="RowHeadersFlatChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnRowHeadersFlatChanged(EventArgs e)
        {
            RowHeadersFlatChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="RowHeaderWidth"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the row header width changes")]
        public event EventHandler RowHeaderWidthChanged;

        /// <summary>
        /// This raises the <see cref="RowHeaderWidthChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnRowHeaderWidthChanged(EventArgs e)
        {
            RowHeaderWidthChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="RowHeaderBackColor"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the row header background color changes")]
        public event EventHandler RowHeaderBackColorChanged;

        /// <summary>
        /// This raises the <see cref="RowHeaderBackColorChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnRowHeaderBackColorChanged(EventArgs e)
        {
            RowHeaderBackColorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="RowHeaderForeColor"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the row header foreground color changes")]
        public event EventHandler RowHeaderForeColorChanged;

        /// <summary>
        /// This raises the <see cref="RowHeaderForeColorChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnRowHeaderForeColorChanged(EventArgs e)
        {
            RowHeaderForeColorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="SelectionBackColor"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the row selection background color changes")]
        public event EventHandler SelectionBackColorChanged;

        /// <summary>
        /// This raises the <see cref="SelectionBackColorChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSelectionBackColorChanged(EventArgs e)
        {
            SelectionBackColorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="SelectionForeColor"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the row selection foreground color changes")]
        public event EventHandler SelectionForeColorChanged;

        /// <summary>
        /// This raises the <see cref="SelectionForeColorChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSelectionForeColorChanged(EventArgs e)
        {
            SelectionForeColorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="RepeatWait"/> value changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the auto-repeat initial wait interval changes")]
        public event EventHandler RepeatWaitChanged;

        /// <summary>
        /// This raises the <see cref="RepeatWaitChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnRepeatWaitChanged(EventArgs e)
        {
            RepeatWaitChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="RepeatInterval"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the auto-repeat interval changes")]
        public event EventHandler RepeatIntervalChanged;

        /// <summary>
        /// This raises the <see cref="RepeatIntervalChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnRepeatIntervalChanged(EventArgs e)
        {
            RepeatIntervalChanged?.Invoke(this, e);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public DataList()
		{
            ptsCurrent = new Point[3];
            ptsNewRow = new Point[16];

            this.BorderStyle = BorderStyle.Fixed3D;
            sepColor = Color.Black;
            sepHeight = 1;
            showSep = showNav = showAddDel = allowAdditions = allowEdits = allowDeletes = true;
            dataMember = String.Empty;
            changePolicy = new ChangePolicy(this);
            shortcutAdd = Shortcut.CtrlShiftA;
            shortcutDel = Shortcut.CtrlShiftD;
            shortcutRowNum = Shortcut.F5;
            shortcutSwitchSection = Shortcut.F6;
            rowHeadersVisible = true;
            rowHeaderWidth = 20;
            brHeaderBack = new SolidBrush(SystemColors.Control);
            brHeaderFore = new SolidBrush(SystemColors.ControlDarkDark);
            brSelBack = new SolidBrush(SystemColors.ControlDark);
            brSelFore = new SolidBrush(SystemColors.Control);
            repeatWait = 500;
            repeatInterval = 50;
            selStart = selEnd = -1;

            // Turn on redraw on resize and double-buffering
            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer |
                ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

			// This call is required by the Windows.Forms Form Designer
			InitializeComponent();

            showCaption = lblCaption.Visible = false;
		}
        #endregion

		#region Component Designer generated code
        //=====================================================================

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DataList));
            this.pnlRows = new EWSoftware.ListControls.RowPanel();
            this.btnFirst = new System.Windows.Forms.Button();
            this.ilButtons = new System.Windows.Forms.ImageList(this.components);
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtRowNum = new EWSoftware.ListControls.NumericTextBox();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tmrRepeat = new System.Windows.Forms.Timer(this.components);
            this.lblCaption = new EWSoftware.ListControls.ClickableLabel();
            this.SuspendLayout();
            // 
            // pnlRows
            // 
            this.pnlRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRows.AutoScroll = true;
            this.pnlRows.Location = new System.Drawing.Point(0, 24);
            this.pnlRows.Name = "pnlRows";
            this.pnlRows.Size = new System.Drawing.Size(304, 112);
            this.pnlRows.TabIndex = 1;
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFirst.Enabled = false;
            this.btnFirst.ImageIndex = 0;
            this.btnFirst.ImageList = this.ilButtons;
            this.btnFirst.Location = new System.Drawing.Point(0, 144);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(22, 22);
            this.btnFirst.TabIndex = 2;
            this.btnFirst.TabStop = false;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // ilButtons
            // 
            this.ilButtons.ImageSize = new System.Drawing.Size(15, 11);
            this.ilButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilButtons.ImageStream")));
            this.ilButtons.TransparentColor = System.Drawing.Color.Lime;
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrev.Enabled = false;
            this.btnPrev.ImageIndex = 1;
            this.btnPrev.ImageList = this.ilButtons;
            this.btnPrev.Location = new System.Drawing.Point(22, 144);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(22, 22);
            this.btnPrev.TabIndex = 3;
            this.btnPrev.TabStop = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            this.btnPrev.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPrev_MouseUp);
            this.btnPrev.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPrev_MouseDown);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.Enabled = false;
            this.btnNext.ImageIndex = 2;
            this.btnNext.ImageList = this.ilButtons;
            this.btnNext.Location = new System.Drawing.Point(107, 144);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(22, 22);
            this.btnNext.TabIndex = 5;
            this.btnNext.TabStop = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            this.btnNext.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNext_MouseUp);
            this.btnNext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnNext_MouseDown);
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLast.Enabled = false;
            this.btnLast.ImageIndex = 3;
            this.btnLast.ImageList = this.ilButtons;
            this.btnLast.Location = new System.Drawing.Point(129, 144);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(22, 22);
            this.btnLast.TabIndex = 6;
            this.btnLast.TabStop = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Enabled = false;
            this.btnAdd.ImageIndex = 4;
            this.btnAdd.ImageList = this.ilButtons;
            this.btnAdd.Location = new System.Drawing.Point(151, 144);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(22, 22);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtRowNum
            //
            this.txtRowNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRowNum.Location = new System.Drawing.Point(47, 144);
            this.txtRowNum.MaxLength = 7;
            this.txtRowNum.Name = "txtRowNum";
            this.txtRowNum.Size = new System.Drawing.Size(57, 22);
            this.txtRowNum.TabIndex = 4;
            this.txtRowNum.TabStop = false;
            this.txtRowNum.Text = "0";
            this.txtRowNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRowNum.Leave += new System.EventHandler(this.txtRowNum_Leave);
            // 
            // lblRowCount
            // 
            this.lblRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRowCount.Location = new System.Drawing.Point(200, 144);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(88, 22);
            this.lblRowCount.TabIndex = 9;
            this.lblRowCount.Text = "of 0";
            this.lblRowCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.CausesValidation = false;
            this.btnDelete.Enabled = false;
            this.btnDelete.ImageIndex = 5;
            this.btnDelete.ImageList = this.ilButtons;
            this.btnDelete.Location = new System.Drawing.Point(173, 144);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(22, 22);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.TabStop = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tmrRepeat
            // 
            this.tmrRepeat.Tick += new System.EventHandler(this.tmrRepeat_Tick);
            // 
            // lblCaption
            //
            this.lblCaption.AutoEllipsis = true;
            this.lblCaption.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCaption.Location = new System.Drawing.Point(0, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(304, 23);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DataList
            // 
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblRowCount);
            this.Controls.Add(this.txtRowNum);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.pnlRows);
            this.Name = "DataList";
            this.Size = new System.Drawing.Size(304, 168);
            this.ResumeLayout(false);

        }
		#endregion

        #region Private designer methods
        //=====================================================================

        // Private designer methods.  These are used because the default values for these properties don't work
        // with the DefaultValue attribute.

        /// <summary>
        /// The designer uses this to determine whether or not to serialize changes to the AllowAdditions
        /// property.
        /// </summary>
        /// <returns>True to serialize the property, false if not</returns>
        private bool ShouldSerializeAllowAdditions()
        {
            return !allowAdditions;
        }

        /// <summary>
        /// Reset the AllowAdditions property
        /// </summary>
        private void ResetAllowAdditions()
        {
            this.AllowAdditions = true;
        }

        /// <summary>
        /// The designer uses this to determine whether or not to serialize changes to the AllowEdits property
        /// </summary>
        /// <returns>True to serialize the property, false if not</returns>
        private bool ShouldSerializeAllowEdits()
        {
            return !allowEdits;
        }

        /// <summary>
        /// Reset the AllowEdits property
        /// </summary>
        private void ResetAllowEdits()
        {
            this.AllowEdits = true;
        }

        /// <summary>
        /// The designer uses this to determine whether or not to serialize changes to the AllowDeletes property
        /// </summary>
        /// <returns>True to serialize the property, false if not</returns>
        private bool ShouldSerializeAllowDeletes()
        {
            return !allowDeletes;
        }

        /// <summary>
        /// Reset the AllowAdditions property
        /// </summary>
        private void ResetAllowDeletes()
        {
            this.AllowDeletes = true;
        }

        /// <summary>
        /// The designer uses this to determine whether or not to serialize changes to the CaptionFont property
        /// </summary>
        /// <returns>True to serialize the property, false if not</returns>
        private bool ShouldSerializeCaptionFont()
        {
            Font cf = lblCaption.Font;

            return (cf.Name != "Microsoft Sans Serif" || cf.SizeInPoints != 7.8f || cf.Style != FontStyle.Bold);
        }

        /// <summary>
        /// Reset the CaptionFont property
        /// </summary>
        private void ResetCaptionFont()
        {
            this.CaptionFont = null;
        }
        #endregion

        #region Private methods and event handlers
        //====================================================================

        /// <summary>
        /// This is called to scroll the header and footer controls with the row panel when necessary
        /// </summary>
        /// <param name="left">The new left position</param>
        internal void AdjustHeaderFooterPosition(int left)
        {
            suppressLayout = true;
            headerFooterLeft = pnlRows.Left + left;

            if(header != null && header.Left != headerFooterLeft)
                header.Left = headerFooterLeft;

            if(footer != null && footer.Left != headerFooterLeft)
                footer.Left = headerFooterLeft;

            suppressLayout = false;
        }

        /// <summary>
        /// This is used to calculate the points for the current row and new row glyphs
        /// </summary>
        /// <remarks>We could use icons or bitmaps, but by drawing them we can have user-defined colors for the
        /// row headers.</remarks>
        private void CalculateGlyphPoints()
        {
            Point[] pts = new Point[21];

            double radius, theta = -Math.PI / 2.0;
            int glyphLeft, glyphSize;

            // Calculate current row glyph points
            glyphSize = (int)(rowHeaderWidth * 0.7);

            if(glyphSize > rowHeight - 4)
                glyphSize = rowHeight - 4;

            glyphLeft = (rowHeaderWidth - (glyphSize / 2)) / 2;

            ptsCurrent[0] = new Point(glyphLeft, 0);
            ptsCurrent[1] = new Point(glyphLeft, glyphSize);
            ptsCurrent[2] = new Point(glyphLeft + (glyphSize / 2), glyphSize / 2);

            // Calculate new row glyph points
            glyphLeft = rowHeaderWidth / 2;

            if(rowHeaderWidth > rowHeight)
                radius = (double)rowHeight * 0.4;
            else
                radius = (double)rowHeaderWidth * 0.4;

            for(int i = 0; i < 21; i++)
            {
                pts[i].X = glyphLeft + (int)(radius * Math.Cos(theta));
                pts[i].Y = (int)(radius * Math.Sin(theta));
                theta += (Math.PI * 3.0 / 10.0);
            }

            ptsNewRow[0] = ptsNewRow[3] = ptsNewRow[6] = ptsNewRow[9] = ptsNewRow[12] = ptsNewRow[15] =
                new Point(glyphLeft, 0);

            ptsNewRow[1] = pts[13];
            ptsNewRow[2] = pts[7];
            ptsNewRow[4] = pts[1];
            ptsNewRow[5] = pts[15];
            ptsNewRow[7] = pts[9];
            ptsNewRow[8] = pts[3];
            ptsNewRow[10] = pts[17];
            ptsNewRow[11] = pts[11];
            ptsNewRow[13] = pts[5];
            ptsNewRow[14] = pts[19];
        }

        /// <summary>
        /// This is used to initialize and bind all visible rows when needed
        /// </summary>
        internal void InitializeAndBindVisibleRows()
        {
            TemplateControl tc;

            int row, rowPos, fillRange, maxRows, multiplier = (showSep) ? 2 : 1;

            if(listManager != null && pnlRows.Controls.Count != 0)
            {
                maxRows = listManager.Count;

                if(changePolicy.AllowAdditions && (maxRows * multiplier) < pnlRows.Controls.Count)
                    maxRows++;

                // Get the position of the topmost visible row
                rowPos = pnlRows.AutoScrollPosition.Y - (pnlRows.AutoScrollPosition.Y / rowHeight * rowHeight);

                row = pnlRows.AutoScrollPosition.Y / rowHeight * -1;
                fillRange = rowPos + rowHeight + pnlRows.Height;
            }
            else
            {
                row = rowPos = maxRows = 0;
                fillRange = pnlRows.Height;
            }

            // Suspend layout or a paint event may occur under rare circumstances that cause a second
            // initialize/bind on the affected rows (that's bad).
            pnlRows.SuspendLayout();

            while(rowPos < fillRange && row < maxRows)
            {
                if(row * multiplier < pnlRows.Controls.Count)
                    tc = (TemplateControl)pnlRows.Controls[row * multiplier];
                else
                    tc = null;

                // Initialize the template and bind the controls as the templates scroll into view.  This saves
                // some time and resources during the initial load and only binds what is actually shown.
                if(tc != null && !tc.IsNewRowInternal && !tc.HasBeenBound)
                {
                    if(!tc.HasBeenInitialized)
                    {
                        tc.InitializeTemplate();
                        tc.HasBeenInitialized = true;
                    }

                    tc.Bind();
                    tc.HasBeenBound = true;
                    OnItemDataBound(new DataListEventArgs(row, tc));
                }

                rowPos += rowHeight;
                row++;
            }

            pnlRows.ResumeLayout();
        }

        /// <summary>
        /// This is used to connect or disconnect events on the related data source when this control's data
        /// source refers to a relationship.  This ensures that this control is refreshed whenever the position
        /// changes or information is updated in the related data source.
        /// </summary>
        /// <param name="connect">True to connect, false to disconnect</param>
        private void WireUpRelatedDataSourceEvents(bool connect)
        {
            string[] relationParts = dataMember.Split('.');

            CurrencyManager cm = (CurrencyManager)this.BindingContext[dataSource, relationParts[0]];

            if(cm != null)
            {
                if(!inSetListManager)
                    cm.EndCurrentEdit();

                // ListChanged happens less frequently and is more efficient with regard to resets
                IBindingList bl = cm.List as IBindingList;

                if(connect)
                {
                    cm.PositionChanged += RelatedPosition_Changed;

                    if(bl != null)
                        bl.ListChanged += RelatedList_Changed;
                    else
                        cm.ItemChanged += RelatedItem_Changed;
                }
                else
                {
                    cm.PositionChanged -= RelatedPosition_Changed;

                    if(bl != null)
                        bl.ListChanged -= RelatedList_Changed;
                    else
                        cm.ItemChanged -= RelatedItem_Changed;
                }
            }
        }

        /// <summary>
        /// This is used to rebind the list when the related data source position changes
        /// </summary>
        private void RelatedPosition_Changed(object sender, EventArgs e)
        {
            if(!inSetListManager && !inAddRow && !inDelRow && !inBindData)
            {
//                System.Diagnostics.Debug.WriteLine(this.Name + ": Related Position Changed");

                this.CommitChanges();
                changePolicy.UpdatePolicy(allowAdditions, allowEdits, allowDeletes);
                this.BindData();

                // We seem to lose the data source ListChanged event handler here every so often so ensure it is
                // hooked back up.  I didn't disconnect it so where does it go??
                if(listManager.List is IBindingList bl)
                {
                    // Disconnect it first in case it didn't go away
                    bl.ListChanged -= DataSource_ListChanged;
                    bl.ListChanged += DataSource_ListChanged;
                }
            }
        }

        /// <summary>
        /// This is used to rebind rows when a related data source item changes
        /// </summary>
        private void RelatedList_Changed(object sender, ListChangedEventArgs e)
        {
            TemplateControl tc;
            IList list;
            int row, listRow, listCount, incr = 1;

            if(inSetListManager || inAddRow || inDelRow || inBindData || e.NewIndex == -1)
                return;

//            System.Diagnostics.Debug.WriteLine(this.Name + ": Related List Changed: " +
//                e.ListChangedType.ToString() + "  Old Index: " + e.OldIndex.ToString() +
//                "  New Index: " + e.NewIndex.ToString() +
//                "  List Count: " + listManager.Count);

            switch(e.ListChangedType)
            {
                case ListChangedType.ItemDeleted:
                case ListChangedType.ItemMoved:
                case ListChangedType.Reset:
                    // Rebind all rows
                    changePolicy.UpdatePolicy(allowAdditions, allowEdits, allowDeletes);
                    this.BindData();
                    break;

                case ListChangedType.ItemChanged:
                    list = listManager.List;
                    listCount = list.Count;

                    if(showSep)
                        incr = 2;

                    for(row = listRow = 0; row < listCount; listRow++, row += incr)
                        if(row < pnlRows.Controls.Count)
                        {
                            tc = (TemplateControl)pnlRows.Controls[row];
                            tc.SetRowSourceInternal(list[listRow]);
                            tc.Bind();
                            tc.HasBeenBound = true;
                            OnItemDataBound(new DataListEventArgs(listRow, tc));
                        }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// This is used to rebind rows when a related data source item changes
        /// </summary>
        private void RelatedItem_Changed(object sender, ItemChangedEventArgs ea)
        {
            TemplateControl tc;
            IList list;
            int row, listRow, listCount, incr = 1;

            if(!inSetListManager && !inAddRow && !inDelRow && !inBindData)
            {
                this.CommitChanges();
                changePolicy.UpdatePolicy(allowAdditions, allowEdits, allowDeletes);

//                System.Diagnostics.Debug.WriteLine(this.Name + ": Related Item Changed: " + "Index: " + ea.Index.ToString());

                list = listManager.List;
                listCount = list.Count;

                if(showSep)
                    incr = 2;

                for(row = listRow = 0; row < listCount; listRow++, row += incr)
                    if(row < pnlRows.Controls.Count)
                    {
                        tc = (TemplateControl)pnlRows.Controls[row];
                        tc.SetRowSourceInternal(list[listRow]);
                        tc.Bind();
                        tc.HasBeenBound = true;
                        OnItemDataBound(new DataListEventArgs(listRow, tc));
                    }
            }
        }

        /// <summary>
        /// This is called when the data source's meta data changes in some way
        /// </summary>
        private void DataSource_MetaDataChanged(object sender, EventArgs e)
        {
            if(!inSetListManager && !inAddRow && !inDelRow && !inBindData)
                SetListManager(dataSource, dataMember, true);
        }

        /// <summary>
        /// This is called when the data source is changed in some way
        /// </summary>
        private void DataSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            TemplateControl tc;
            int row;

            // Ignore calls in the following cases or it tends to
            // get recursive in a hurry.
            if(dataSource == null || inSetListManager || inAddRow || inDelRow || inBindData)
                return;

//            System.Diagnostics.Debug.WriteLine(this.Name + ": List Changed: " +
//                e.ListChangedType.ToString() + "  Old Index: " + e.OldIndex.ToString() +
//                "  New Index: " + e.NewIndex.ToString() +
//                "  List Count: " + listManager.Count);

            switch(e.ListChangedType)
            {
                case ListChangedType.Reset:
                    changePolicy.UpdatePolicy(allowAdditions, allowEdits, allowDeletes);
                    this.BindData();
                    break;

                case ListChangedType.ItemAdded:
                    row = e.NewIndex;

                    // Rows inserted anywhere but at the end are treated as
                    // a reset.
                    if(row < listManager.Count - 1)
                        this.BindData();
                    else
                    {
                        if(showSep)
                            row *= 2;

                        // If it's past the end or is the "new row" template, add it.  If not, we added it and
                        // can ignore it.
                        if(row >= pnlRows.Controls.Count || ((TemplateControl)pnlRows.Controls[row]).IsNewRowInternal)
                            AddRowInternal();
                    }
                    break;

                case ListChangedType.ItemDeleted:
                    this.DeleteRowInternal(e.NewIndex, true);
                    btnDelete.Enabled = (changePolicy.AllowDeletes && listManager.Count > 0);
                    break;

                case ListChangedType.ItemMoved:
                    // If a row moves, treat it like a reset
                    this.BindData();
                    break;

                case ListChangedType.ItemChanged:
                    // Rebind the changed row to reflect the changes
                    row = e.NewIndex;

                    if(showSep)
                        row *= 2;

                    if(row < pnlRows.Controls.Count)
                    {
                        tc = (TemplateControl)pnlRows.Controls[row];
                        tc.SetRowSourceInternal(listManager.List[e.NewIndex]);
                        tc.Bind();
                        tc.HasBeenBound = true;
                        OnItemDataBound(new DataListEventArgs(e.NewIndex, tc));
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// This is called when an item in the data source changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="ea">The event arguments</param>
        private void DataSource_ItemChanged(object sender, ItemChangedEventArgs ea)
        {
            if(ea.Index == -1 && !inSetListManager && !inAddRow && !inDelRow && !inBindData)
            {
//                System.Diagnostics.Debug.WriteLine(this.Name + ": Item Changed: " + "Index: " + ea.Index.ToString());

                changePolicy.UpdatePolicy(allowAdditions, allowEdits, allowDeletes);
                this.BindData();
            }
        }

        /// <summary>
        /// This is called when the position in the data source changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataSource_PositionChanged(object sender, EventArgs e)
        {
            bool hasList = (listManager != null);
            int nRow, curRow = (hasList) ? listManager.Position : -1;

            // Ignore calls in the following cases.  It'll refresh when it's all done.
            if(inAddRow || inDelRow || inBindData)
                return;

            if(curRow != currentRow)
            {
                if(curRow < 0)
                    curRow = 0;

                // Focus the selected row accounting for the separators.  This must happen first as enabling and
                // disabling the buttons can cause a change in focus and selection of a different row.
                currentRow = nRow = curRow;

                txtRowNum.Text = (curRow + 1).ToString(CultureInfo.InvariantCulture);

                if(showSep)
                    nRow *= 2;

                if(hasList && nRow < pnlRows.Controls.Count)
                {
                    if(rowHeadersVisible)
                        this.Invalidate(new Rectangle(0, 0, rowHeaderWidth, this.Height), false);

                    TemplateControl c = (TemplateControl)pnlRows.Controls[nRow];

                    // Initialize it first if necessary
                    if(!c.HasBeenInitialized)
                    {
                        c.InitializeTemplate();
                        c.HasBeenInitialized = true;
                    }

                    // Only focus the row template if it isn't already focused or you get some really bizarre
                    // focus issues if a control is clicked.  Also don't focus it if auto-repeating the
                    // next/previous button clicks.
                    if(this.ContainsFocus && !c.ContainsFocus && !autoRepeating)
                        c.Focus();

                    pnlRows.MakeControlVisible(c);
                    OnCurrent(new DataListEventArgs(curRow, c));
                }

                // Enable or disable buttons based on the row
                btnFirst.Enabled = btnPrev.Enabled = (curRow > 0);

                if(!autoRepeating)
                {
                    btnNext.Enabled = hasList && (curRow < listManager.Count - 1 ||
                        (changePolicy.AllowAdditions && listManager.Count != 0));
                }

                btnLast.Enabled = hasList && (curRow < listManager.Count - 1);
            }
        }

        /// <summary>
        /// Set the focus to the specified row when the row text box loses focus
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtRowNum_Leave(object sender, System.EventArgs e)
        {
            Control c;
            bool updateText;
            int row = currentRow;

            if(listManager != null && listManager.Count != 0)
            {
                if(txtRowNum.Text.Length == 0)
                    updateText = true;
                else
                {
                    row = Convert.ToInt32(txtRowNum.Text, CultureInfo.InvariantCulture) - 1;

                    if(row < 0)
                    {
                        row = 0;
                        updateText = true;
                    }
                    else
                        if(row >= listManager.Count)
                        {
                            row = listManager.Count - 1;
                            updateText = true;
                        }
                        else
                            updateText = false;
                }

                // Go to the new row or return focus to the current row
                if(currentRow != row)
                {
                    updateText = false;
                    listManager.Position = row;
                }
                else
                {
                    if(showSep)
                        c = pnlRows.Controls[row * 2];
                    else
                        c = pnlRows.Controls[row];

                    c.Focus();
                }

                if(updateText)
                    txtRowNum.Text = (currentRow + 1).ToString(CultureInfo.InvariantCulture);
            }
            else
                txtRowNum.Text = "0";
        }

        /// <summary>
        /// This updates the row number text box when a row template gains the focus.  It also physically adds a
        /// new row to the data source if the new row template is focused.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void RowTemplate_Enter(object sender, System.EventArgs e)
        {
            // Ignore calls in the following cases.  It'll refresh when it's all done.
            if(inAddRow || inDelRow || inBindData)
                return;

            int row = pnlRows.Controls.IndexOf((Control)sender);

            if(showSep)
                row /= 2;

            if(listManager != null && (currentRow != row || listManager.Count == 0))
            {
                this.Select(-1, -1, -1);

                if(row < listManager.Count)
                    listManager.Position = row;
                else
                {
                    TemplateControl tc = (TemplateControl)sender;

                    // It should be the new row template
                    if(tc.IsNewRowInternal)
                    {
                        try
                        {
                            inAddRow = true;
                            listManager.AddNew();

                            // Set the control's row source
                            tc.SetRowSourceInternal(listManager.List[listManager.Count - 1]);
                        }
                        finally
                        {
                            inAddRow = false;

                            lblRowCount.Text = LR.GetString("DLNavRowCount", listManager.Count);

                            // Force the focus to the row
                            currentRow = -1;
                            DataSource_PositionChanged(this, EventArgs.Empty);

                            // Disable these until they are needed again
                            btnNext.Enabled = btnLast.Enabled = btnAdd.Enabled = btnDelete.Enabled = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This is used to remove the new row added for the new row template when it loses focus and nothing was
        /// changed.
        /// </summary>
        private void RowTemplate_Leave(object sender, System.EventArgs e)
        {
            TemplateControl tc = (TemplateControl)sender;

            if(tc.IsNewRowInternal)
            {
                if(tc.HasBeenBound)
                {
                    this.CancelChanges();
                    tc.WirePropChangedEvents(false);
                    tc.SetRowSourceInternal(null);
                }

                lblRowCount.Text = LR.GetString("DLNavRowCount", listManager.Count);

                btnAdd.Enabled = changePolicy.AllowAdditions;
                btnDelete.Enabled = (changePolicy.AllowDeletes && listManager.Count > 0);
            }
        }

        /// <summary>
        /// Go to the first row unless validation failed somewhere
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            if(this.ContainsFocus)
            {
                listManager.Position = 0;
                this.Select(-1, -1, -1);
            }
        }

        /// <summary>
        /// Go to the previous row unless validation failed somewhere
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            if(this.ContainsFocus && listManager.Position > 0)
            {
                listManager.Position--;
                this.Select(-1, -1, -1);
            }
        }

        /// <summary>
        /// Set the timer for auto-repeat unless validation fails and the button doesn't have the focus
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnPrev_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(this.ContainsFocus)
            {
                repeatButton = btnPrev;
                tmrRepeat.Interval = repeatWait;
                tmrRepeat.Enabled = true;
            }
        }

        /// <summary>
        /// Clear the timer for auto-repeat
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnPrev_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            tmrRepeat.Enabled = false;
        }

        /// <summary>
        /// Go to the next row unless validation failed somewhere
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnNext_Click(object sender, System.EventArgs e)
        {
            if(this.ContainsFocus)
            {
                if(listManager.Position < listManager.Count - 1)
                    listManager.Position++;
                else
                    if(changePolicy.AllowAdditions)
                        this.MoveTo(RowPosition.NewRow);

                this.Select(-1, -1, -1);
            }
        }

        /// <summary>
        /// Set the timer for auto-repeat unless validation fails and the button doesn't have the focus
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnNext_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(this.ContainsFocus)
            {
                repeatButton = btnNext;
                tmrRepeat.Interval = repeatWait;
                tmrRepeat.Enabled = true;
            }
        }

        /// <summary>
        /// Clear the timer for auto-repeat
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnNext_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            tmrRepeat.Enabled = false;
        }

        /// <summary>
        /// Go to the last row unless validation failed somewhere
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnLast_Click(object sender, System.EventArgs e)
        {
            if(this.ContainsFocus)
            {
                listManager.Position = listManager.Count - 1;
                this.Select(-1, -1, -1);
            }
        }

        /// <summary>
        /// Handle the Add button click unless validation failed somewhere
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            if(this.ContainsFocus)
                this.MoveTo(RowPosition.NewRow);
        }

        /// <summary>
        /// Handle the Delete button click
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if(this.ContainsFocus)
                this.DeleteRow(currentRow);
        }

        /// <summary>
        /// Fire the appropriate event for next/previous button auto-repeat
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void tmrRepeat_Tick(object sender, System.EventArgs e)
        {
            // Only repeat while the cursor is in the button
            Point p = repeatButton.PointToClient(Cursor.Position);

            if(repeatButton.ClientRectangle.Contains(p))
            {
                // Shorten the interval after the first tick
                tmrRepeat.Interval = repeatInterval;

                autoRepeating = true;

                if(repeatButton == btnPrev)
                    btnPrev_Click(sender, e);
                else
                    btnNext_Click(sender, e);

                autoRepeating = false;

                // If the start or end of the set is reached, turn the timer off.  When starting the repeat from
                // the "new row" placeholder, the button loses focus so we return focus to it so that when you
                // let go, the right row has the focus.
                if(!repeatButton.Enabled || (!changePolicy.AllowAdditions && currentRow >= listManager.Count - 1))
                    tmrRepeat.Enabled = false;
                else
                    if(!repeatButton.Focused)
                        repeatButton.Focus();
            }
        }

        /// <summary>
        /// This is called to ensure that the specified data member is valid for the given data source.  If not
        /// valid, it is cleared.
        /// </summary>
        /// <param name="ds">The data source to check</param>
        /// <param name="member">The data member to check</param>
        private void EnforceValidDataMember(object ds, string member)
        {
            if(ds != null && member != null && member.Length != 0 && this.Parent != null &&
              this.BindingContext != null)
            {
                try
                {
                    BindingManagerBase bmb = this.BindingContext[ds, member];
                }
                catch
                {
                    dataMember = String.Empty;
                }
            }
        }

        /// <summary>
        /// This is called to get information about the data source and the data member and to hook up the
        /// necessary event handlers.
        /// </summary>
        /// <param name="newDataSource">The new data source.</param>
        /// <param name="newDataMember">The new data member in the data source.</param>
        /// <param name="force">True to force the information to be updated, false to only update the info if
        /// something really changed.</param>
        /// <exception cref="ArgumentException">This is thrown if the data member cannot be found in the data
        /// source.</exception>
        private void SetListManager(object newDataSource, string newDataMember, bool force)
        {
            bool dataSrcChanged = dataSource != newDataSource;
            bool dataMbrChanged = dataMember != newDataMember;

            if(force || dataSrcChanged || dataMbrChanged || !inSetListManager)
            {
                inSetListManager = true;

                try
                {
                    if(listManager != null)
                    {
                        listManager.EndCurrentEdit();
                        listManager.MetaDataChanged -= DataSource_MetaDataChanged;
                        listManager.PositionChanged -= DataSource_PositionChanged;

                        if(listManager.List is IBindingList bl)
                            bl.ListChanged -= DataSource_ListChanged;
                        else
                            listManager.ItemChanged -= DataSource_ItemChanged;

                        if(dataMember.IndexOf('.') != -1)
                            WireUpRelatedDataSourceEvents(false);
                    }

                    if(newDataSource != null && this.Parent != null && this.BindingContext != null &&
                      newDataSource != Convert.DBNull)
                        listManager = (CurrencyManager)this.BindingContext[newDataSource, newDataMember];
                    else
                        listManager = null;

                    dataSource = newDataSource;
                    dataMember = newDataMember ?? String.Empty;

                    if(this.listManager != null)
                    {
                        listManager.MetaDataChanged += DataSource_MetaDataChanged;
                        listManager.PositionChanged += DataSource_PositionChanged;

                        // ListChanged happens less frequently and is more efficient with regard to resets
                        if(listManager.List is IBindingList bl)
                            bl.ListChanged += DataSource_ListChanged;
                        else
                            listManager.ItemChanged += DataSource_ItemChanged;

                        // If the data member refers to a relationship, we must hook up the some events in the
                        // parent data source so that this list is refreshed when the related data source
                        // changes.
                        if(dataMember.IndexOf('.') != -1)
                            WireUpRelatedDataSourceEvents(true);
                    }

                    changePolicy.UpdatePolicy(allowAdditions, allowEdits, allowDeletes);

                    this.BindData();
                    this.OnDataSourceChanged(EventArgs.Empty);
                }
                finally
                {
                    inSetListManager = false;
                }
            }
        }

        /// <summary>
        /// This is used to create the rows and bind each one to a row in the data source
        /// </summary>
		private void BindData()
		{
            TemplateControl ctl;
            Separator sep;
            int idx, top = 0, sepTop = 0, sepWidth = pnlRows.Width;
            bool bindFailed = false;

            if(inBindData)
                return;

            Cursor oldCursor = this.Cursor;

            if(oldCursor != Cursors.WaitCursor)
                this.Cursor = Cursors.WaitCursor;

            inBindData = isBinding = true;

            try
            {
                pnlRows.Visible = false;
                pnlRows.SuspendLayout();
                pnlRows.AutoScrollPosition = new Point(0, 0);
                RemoveRows();
                this.Select(-1, -1, -1);

                // If we don't have both, there's nothing to do
    			if(rowTemplate == null || listManager == null)
                    return;

                // Bind the header and footer controls
                if(header != null)
                {
                    header.SetRowSourceInternal(dataSource);
                    header.Bind();
                    header.HasBeenBound = true;
                    OnHeaderDataBound(new DataListEventArgs(-1, header));
                }

                if(footer != null)
                {
                    footer.SetRowSourceInternal(dataSource);
                    footer.Bind();
                    footer.HasBeenBound = true;
                    OnFooterDataBound(new DataListEventArgs(-1, footer));
                }

                // Create the row template items
                ConstructorInfo ctor = rowTemplate.GetConstructor(Type.EmptyTypes);
                rowHeight = -1;

    			for(idx = 0; idx < listManager.Count; idx++, top += rowHeight, sepTop += rowHeight)
    			{
                    ctl = (TemplateControl)ctor.Invoke(null);
                    ctl.Enter += RowTemplate_Enter;
                    ctl.TemplateParentInternal = this;

                    if(rowHeight == -1)
                    {
                        ctl.InitializeTemplate();
                        ctl.HasBeenInitialized = true;

                        sepTop = rowHeight = ctl.Height;
                        if(showSep)
                            rowHeight += sepHeight;

                        if(ctl.Width > sepWidth)
                            sepWidth = ctl.Width;
                    }

    				ctl.Location = new Point(0, top);
    				pnlRows.Controls.Add(ctl);

                    if(showSep)
                    {
                        sep = new Separator(sepColor, sepHeight, sepWidth, 0, sepTop);
                        pnlRows.Controls.Add(sep);
                    }

                    // Set the control's row source
                    ctl.SetRowSourceInternal(listManager.List[idx]);
    			}
            }
            catch(Exception ex)
            {
                bindFailed = true;

                // Something in the data binding hierarchy tends to eat exceptions that occur during binding and
                // you never see them (i.e. bad field names in a binding).  This logs them to the debugger if one
                // is attached.
                if(System.Diagnostics.Debugger.IsAttached)
                {
                    // Can't use System.Diagnostics.Debug as it gets excluded from the release build.  This does
                    // the same thing though but is compiled into the release build.
                    using(var dtl = new System.Diagnostics.DefaultTraceListener())
                    {
                        dtl.WriteLine(LR.GetString("ExBindData1", this.Name, ex.Message));
                    }
                }
            }
            finally
            {
                this.CalculateGlyphPoints();

                if(!bindFailed)
                    if(!changePolicy.AllowAdditions)
                    {
                        this.Invalidate(new Rectangle(0, 0, rowHeaderWidth, this.Height), false);
                        this.Update();
                    }
                    else
                        this.AddNewRowTemplate();

                lblRowCount.Text = LR.GetString("DLNavRowCount", (listManager != null) ? listManager.Count : 0);

                if(oldCursor != Cursors.WaitCursor)
                    this.Cursor = oldCursor;

                inBindData = false;

                // Problems with the bindings will manifest themselves here (i.e. bad field names, etc).
                try
                {
                    pnlRows.ResumeLayout();
                    pnlRows.Visible = true;
                    this.Invalidate();
                    this.Update();
                }
                catch(Exception ex)
                {
                    // As above.  Log the exception so that we know it occurred.
                    if(System.Diagnostics.Debugger.IsAttached)
                        using(var dtl = new System.Diagnostics.DefaultTraceListener())
                        {
                            dtl.WriteLine(LR.GetString("ExBindData2", this.Name, ex.Message));
                        }
                }
                finally
                {
                    isBinding = false;
                }

                // Force the focus to the proper row
                currentRow = (dataSource == null) ? 0 : -1;
                DataSource_PositionChanged(this, EventArgs.Empty);

                if(listManager != null && listManager.Count == 0)
                    OnNoRows(EventArgs.Empty);
            }
		}

        /// <summary>
        /// The DataList supports having a blank new row template like MS-Access does.  This row isn't bound
        /// until it is actually focused.  See RowTemplate_Enter for details.
        /// </summary>
        internal void AddNewRowTemplate()
        {
            int width, top = 0;

            if(!changePolicy.AllowAdditions || rowTemplate == null || listManager == null)
                return;

            if(pnlRows.Controls.Count != 0)
                top = pnlRows.Controls[pnlRows.Controls.Count - 1].Top +
                    pnlRows.Controls[pnlRows.Controls.Count - 1].Height;

            ConstructorInfo ctor = rowTemplate.GetConstructor(Type.EmptyTypes);
            TemplateControl tc = (TemplateControl)ctor.Invoke(null);

            // This is going to get used right away so initialize it now
            tc.IsNewRowInternal = true;
            tc.TemplateParentInternal = this;
            tc.InitializeTemplate();
            tc.HasBeenInitialized = true;
            tc.Enter += RowTemplate_Enter;
            tc.Location = new Point(pnlRows.AutoScrollPosition.X, top);

            if(rowHeight == -1)
            {
                rowHeight = tc.Height;

                if(showSep)
                    rowHeight += sepHeight;

                this.CalculateGlyphPoints();
            }

            pnlRows.Controls.Add(tc);

            if(showSep)
            {
                if(tc.Width < pnlRows.Width)
                    width = pnlRows.Width;
                else
                    width = tc.Width;

                width += (pnlRows.AutoScrollPosition.X * -1);
                Separator sep = new Separator(sepColor, sepHeight, width, pnlRows.AutoScrollPosition.X,
                    top + tc.Height);
                pnlRows.Controls.Add(sep);
            }

            tc.Leave += RowTemplate_Leave;

            btnNext.Enabled = true;
            btnAdd.Enabled = changePolicy.AllowAdditions;
            btnDelete.Enabled = (changePolicy.AllowDeletes && listManager.Count > 0);

            if(rowHeadersVisible)
            {
                this.Invalidate(new Rectangle(0, 0, rowHeaderWidth, this.Height), false);
                this.Update();
            }
        }

        /// <summary>
        /// This is called when a row is added to the data source externally
        /// </summary>
        /// <remarks>If additions are allowed, the new row template is bound to the new row and the new row
        /// template is recreated.  If additions are not allowed, it creates a new template and bind it to the
        /// new row.</remarks>
        private void AddRowInternal()
        {
            TemplateControl tc;
            int idx, width, top = 0;

            pnlRows.SuspendLayout();

            if(changePolicy.AllowAdditions)
            {
                idx = pnlRows.Controls.Count - 1;

                if(showSep)
                    idx--;

                tc = (TemplateControl)pnlRows.Controls[idx];

                // If bound, get rid of the temporary new row
                if(tc.IsDataBound)
                {
                    if(!isUndoing)
                        this.CancelChanges();

                    tc.WirePropChangedEvents(false);
                }

                // Set the control's row source and add a new "new row" template
                tc.IsNewRowInternal = false;
                tc.SetRowSourceInternal(listManager.List[listManager.Count - 1]);
                AddNewRowTemplate();
            }
            else
            {
                idx = listManager.Count - 1;

                if(pnlRows.Controls.Count != 0)
                    top = pnlRows.Controls[pnlRows.Controls.Count - 1].Top +
                        pnlRows.Controls[pnlRows.Controls.Count - 1].Height - 1;

                ConstructorInfo ctor = rowTemplate.GetConstructor(Type.EmptyTypes);
                tc = (TemplateControl)ctor.Invoke(null);
                tc.TemplateParentInternal = this;
                tc.InitializeTemplate();
                tc.HasBeenInitialized = true;
                tc.Enter += RowTemplate_Enter;
                tc.Location = new Point(pnlRows.AutoScrollPosition.X, top);
                pnlRows.Controls.Add(tc);

                if(rowHeight == -1)
                {
                    rowHeight = tc.Height;

                    if(showSep)
                        rowHeight += sepHeight;

                    this.CalculateGlyphPoints();
                }

                if(showSep)
                {
                    if(tc.Width < pnlRows.Width)
                        width = pnlRows.Width;
                    else
                        width = tc.Width;

                    width += (pnlRows.AutoScrollPosition.X * -1);
                    Separator sep = new Separator(sepColor, sepHeight, width, pnlRows.AutoScrollPosition.X,
                        top + tc.Height);
                    pnlRows.Controls.Add(sep);
                }

                // Set the control's row source
                tc.SetRowSourceInternal(listManager.List[idx]);
            }

            lblRowCount.Text = LR.GetString("DLNavRowCount", listManager.Count);
            btnDelete.Enabled = (changePolicy.AllowDeletes && listManager.Count > 0);
            pnlRows.ResumeLayout();

            // Force the focus to the new row if necessary
            currentRow = -1;
            DataSource_PositionChanged(this, EventArgs.Empty);

            OnAddedRow(new DataListEventArgs(currentRow, tc));
        }

        /// <summary>
        /// This is called to handle the actual deletion of rows when requested by the user and also when a row
        /// is deleted externally from the data source other than by us.
        /// </summary>
        /// <param name="delRow">The row to delete</param>
        /// <param name="externalRequest">True if the request didn't come from us, false if it was requested by
        /// us.</param>
        private bool DeleteRowInternal(int delRow, bool externalRequest)
        {
            Control c;
            ControlCollection cc;
            TemplateControl tc;
            int ctlRow;

            // Nested deletes are not supported
            if(inDelRow)
                return false;

            ctlRow = delRow;

            if(showSep)
                ctlRow *= 2;

            cc = pnlRows.Controls;
            tc = (TemplateControl)cc[ctlRow];

            // Ignore the request if on the new row template
            if(tc.IsNewRowInternal)
                return false;

            // If someone besides us removed the row, remove the row template without asking
            if(!externalRequest)
                try
                {
                    inDelRow = true;

                    // See if the user will allow the delete to occur
                    DataListCancelEventArgs ce = new DataListCancelEventArgs(delRow, tc);

                    // Allow the row template a chance to allow or deny the deletion.  It has the current row
                    // values in its controls which may or may not be in the data source yet due to the way data
                    // binding works.
                    if(tc.CanDelete)
                        OnDeletingRow(ce);
                    else
                        ce.Cancel = true;

                    // Ignore the request if canceled
                    if(ce.Cancel)
                    {
                        // Return focus to the row template to avoid some weird data binding issues
                        if(!tc.ContainsFocus)
                            tc.Focus();

                        return false;
                    }

                    // If deleting the current row, cancel any pending changes
                    if(delRow == currentRow)
                        this.CancelChanges();
                }
                finally
                {
                    inDelRow = false;
                }

            Cursor oldCursor = this.Cursor;

            if(oldCursor != Cursors.WaitCursor)
                this.Cursor = Cursors.WaitCursor;

            try
            {
                inDelRow = true;

                // Remove the row template and separator
                pnlRows.SuspendLayout();
                cc.RemoveAt(ctlRow);
                tc.Dispose();

                if(showSep)
                {
                    c = cc[ctlRow];
                    cc.RemoveAt(ctlRow);
                    c.Dispose();
                }

                // Delete the row from the data source if it is there
                if(!externalRequest && delRow < listManager.Count)
                    listManager.RemoveAt(delRow);

                // Adjust the row selection if necessary
                if(selStart != -1)
                    if(delRow >= selStart && delRow <= selEnd)
                    {
                        selEnd--;

                        if(selEnd < selStart)
                            selStart = selEnd = -1;
                    }
                    else
                        if(delRow < selStart)
                        {
                            selStart--;
                            selEnd--;
                        }

                // Adjust the position of the other controls and rebind them to their new row
                while(ctlRow < cc.Count)
                {
                    c = cc[ctlRow];
                    c.Top -= rowHeight;

                    tc = c as TemplateControl;

                    if(tc != null && !tc.IsNewRowInternal)
                    {
                        tc.SetRowSourceInternal(listManager.List[delRow]);
                        delRow++;
                    }

                    ctlRow++;
                }
            }
            finally
            {
                if(oldCursor != Cursors.WaitCursor)
                    this.Cursor = oldCursor;

                inDelRow = false;
                pnlRows.ResumeLayout();
            }

            lblRowCount.Text = LR.GetString("DLNavRowCount", listManager.Count);

            if(rowHeadersVisible)
            {
                this.Invalidate(new Rectangle(0, 0, rowHeaderWidth, this.Height), false);
                this.Update();
            }

            // Force the focus to the proper row
            currentRow = -1;
            DataSource_PositionChanged(this, EventArgs.Empty);

            OnDeletedRow(new DataListEventArgs(-1, null));

            // If there are no rows and additions are allowed, focus the new row template
            if(currentRow == -1 && changePolicy.AllowAdditions)
                this.RowTemplate_Enter(pnlRows.Controls[0], EventArgs.Empty);

            btnDelete.Enabled = (changePolicy.AllowDeletes &&
                listManager.Count > 0);

            return true;
        }

        /// <summary>
        /// This is used to clear out and disposed of the row controls
        /// </summary>
        private void RemoveRows()
        {
            Control c;
            ControlCollection cc = pnlRows.Controls;

            while(cc.Count > 0)
            {
                c = cc[0];
                cc.RemoveAt(0);
                c.Dispose();
            }
        }

        /// <summary>
        /// Handle the escape key press to cancel changes
        /// </summary>
        /// <returns>Always returns true</returns>
        private bool HandleEscapeKey()
        {
            TemplateControl tc;
            int row;

            tc = this.CurrentItem;

            if(tc != null && !tc.IsNewRowInternal)
            {
                // Row may not be the same afterwards if canceling changes on the new row
                row = currentRow;

                DataListCancelEventArgs ce = new DataListCancelEventArgs(row, tc);

                OnCancelingEdits(ce);

                if(!ce.Cancel)
                {
                    this.CancelChanges();

                    // If the row changed, pass null and -1 to indicate that the old row went away
                    if(currentRow != row)
                        OnCanceledEdits(new DataListEventArgs(-1, null));
                    else
                        OnCanceledEdits(new DataListEventArgs(row, tc));
                }
            }

            return true;
        }

        /// <summary>
        /// Handle the page up and page down keys to scroll through the list
        /// </summary>
        /// <param name="key">The key that was pressed</param>
        /// <param name="ctrlPressed">True if Control is pressed, false if not</param>
        /// <returns>True if handled, false if not</returns>
        /// <remarks>If the active control is a multi-line textbox without all of its text selected, the data
        /// list lets it handle the key instead so that you can page up/down through its text.</remarks>
        private bool HandlePageUpPageDownKeys(Keys key, bool ctrlPressed)
        {
            ContainerControl cc;
            TemplateControl tc;
            TextBoxBase tb;
            Control ctl;
            int  row;
            bool handled = false;

            tc = this.CurrentItem;

            if(tc != null && listManager.Count != 0)
            {
                // If Control is pressed, jump to the first or last row
                if(key == Keys.PageUp)
                {
                    if(ctrlPressed)
                        row = -1;
                    else
                        row = currentRow - (pnlRows.Height / rowHeight);
                }
                else
                    if(ctrlPressed)
                        row = listManager.Count;
                    else
                        row = currentRow + (pnlRows.Height / rowHeight);

                if(tc.IsNewRowInternal)
                {
                    tb = null;

                    // If it's the new row, get rid of it first if paging up.  Ignore it if paging down.
                    if(key == Keys.PageUp)
                        RowTemplate_Leave(tc, EventArgs.Empty);
                    else
                        row = currentRow;
                }
                else
                {
                    // Get the active control
                    ctl = tc.ActiveControl;

                    while(ctl != null)
                    {
                        cc = ctl as ContainerControl;

                        if(cc == null)
                            break;

                        ctl = cc.ActiveControl;
                    }

                    // If it's a multi-line textbox without a full selection, let the textbox process the page
                    // up/down instead.
                    tb = ctl as TextBoxBase;
                }

                if(tb == null || tb.Multiline == false || tb.SelectionLength == tb.Text.Length)
                {
                    if(row < 0)
                        this.MoveTo(RowPosition.FirstRow);
                    else
                        if(row >= listManager.Count)
                        {
                            if(changePolicy.AllowAdditions)
                                this.MoveTo(RowPosition.NewRow);
                            else
                                this.MoveTo(RowPosition.LastRow);
                        }
                        else
                            this.MoveTo(row);

                    handled = true;
                }
            }

            return handled;
        }

        /// <summary>
        /// Handle the Switch Section shortcut key
        /// </summary>
        /// <returns>Always returns true</returns>
        private bool HandleSwitchSectionKey()
        {
            bool focusHeader = false, focusDetail = false, focusFooter = false;

            // Figure out which section to focus
            if(header != null && header.ContainsFocus)
            {
                if(listManager != null && listManager.Count != 0)
                    focusDetail = true;
                else
                    focusFooter = (footer != null);
            }
            else
            {
                if(footer == null || !footer.ContainsFocus)
                {
                    if(footer != null)
                        focusFooter = true;
                    else
                        focusHeader = (header != null);
                }
                else
                {
                    if(footer.ContainsFocus)
                    {
                        if(header != null)
                            focusHeader = true;
                        else
                            focusDetail = (listManager != null && listManager.Count != 0);
                    }
                }
            }

            if(focusHeader)
                header.Focus();
            else
            {
                if(focusFooter)
                    footer.Focus();
                else
                {
                    if(focusDetail)
                    {
                        int nRow = currentRow;

                        if(showSep)
                            nRow *= 2;

                        Control c = pnlRows.Controls[nRow];
                        pnlRows.MakeControlVisible(c);
                        c.Focus();

                        if(rowHeadersVisible)
                            this.Invalidate(new Rectangle(0, 0, rowHeaderWidth, this.Height), false);
                    }
                }
            }

            return true;
        }
        #endregion

        #region Protected and public methods
        //=====================================================================

        /// <summary>
        /// Clean up any resources being used
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources, false to just release
        /// unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                brHeaderBack?.Dispose();
                brHeaderFore?.Dispose();
                brSelBack?.Dispose();
                brSelFore?.Dispose();
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// This is overridden to handle the various extra keys recognized by this control
        /// </summary>
        /// <param name="msg">The command key message</param>
        /// <param name="keyData">The key to process</param>
        /// <returns>True if the key was handled, false if not</returns>
        /// <remarks>This handles the various extra shortcut keys, Ctrl+Tab and Ctrl+Shift+Tab to shift the focus
        /// out of the data list, Escape to cancel changes, and Page Up/Down and Ctrl+Page Up/Down to scroll
        /// through the list.</remarks>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key;
            bool keyHandled, ctrlPressed;

            // We only care about WM_KEYDOWN
            if(msg.Msg == 0x100)
            {
                key = keyData & Keys.KeyCode;
                ctrlPressed = (keyData & Keys.Control) != Keys.None;
                keyHandled = false;

                // Cancel changes to the current row?
                if(keyData == Keys.Escape)
                    keyHandled = this.HandleEscapeKey();

                // Change the focus to the next or prior control?
                if(key == Keys.Tab)
                {
                    if(ctrlPressed && this.Parent.SelectNextControl(this, (keyData & Keys.Shift) == Keys.None,
                      true, true, true))
                    {
                        keyHandled = true;
                    }
                }

                // Scroll up or down the list?
                if(key == Keys.PageUp || key == Keys.PageDown)
                    keyHandled = this.HandlePageUpPageDownKeys(key, ctrlPressed);

                // Set focus to row number text box?
                if(keyData == (Keys)shortcutRowNum && showNav)
                {
                    txtRowNum.Focus();
                    keyHandled = txtRowNum.Focused;
                }

                // Switch section?
                if(keyData == (Keys)shortcutSwitchSection)
                    keyHandled = this.HandleSwitchSectionKey();

                // Add new row?
                if(keyData == (Keys)shortcutAdd && changePolicy.AllowAdditions && listManager != null)
                    keyHandled = this.MoveTo(RowPosition.NewRow);

                // Delete the current row?
                if(keyData == (Keys)shortcutDel && changePolicy.AllowDeletes && listManager != null &&
                  listManager.Count != 0)
                    keyHandled = this.DeleteRow(currentRow);

                if(keyHandled)
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// This is overridden so that the current row gets a chance to process the mnemonic first
        /// </summary>
        /// <param name="charCode">The character code to process as a mnemonic</param>
        /// <returns>True if the character code was processed as a mnemonic or false if it was not</returns>
        /// <remarks>The current row is given a chance to handle it first.  If not handled, then the header and
        /// footer controls (if any) are given a chance to process it.</remarks>
        protected override bool ProcessMnemonic(char charCode)
        {
            bool processed = false;

            TemplateControl tc = this.CurrentItem;

            if(tc != null)
            {
                processed = tc.HandleMnemonic(charCode);

                if(processed && rowHeadersVisible)
                {
                    this.Invalidate(new Rectangle(0, 0, rowHeaderWidth, this.Height), false);
                    this.Update();
                }
            }

            if(!processed && header != null)
                processed = header.HandleMnemonic(charCode);

            if(!processed && footer != null)
                processed = footer.HandleMnemonic(charCode);

            return processed;
        }

        /// <summary>
        /// This is overridden to ensure that the current row regains the focus when tabbing into the control
        /// </summary>
        /// <param name="directed">True to specify the direction of the control to select, false if not.</param>
        /// <param name="forward">The direction of the selection.  True for forward, false for backwards.</param>
        /// <overloads>There are two extra overloads for this method</overloads>
        protected override void Select(bool directed, bool forward)
        {
            bool selectionMade = false;

            if(!this.DesignMode && !this.Focused && this.Parent != null)
            {
                IContainerControl container = null;
                Control parent = this.Parent;

                // Find the container so that we can set its active control to ourself
                while(container == null && parent != null)
                {
                    container = parent as IContainerControl;
                    parent = parent.Parent;
                }

                if(container != null)
                {
                    TemplateControl tc = this.CurrentItem;

                    // Select the current item, the header, the footer, or the row number control
                    if(tc != null)
                    {
                        container.ActiveControl = this;

                        if(tc.Visible && tc.Enabled)
                        {
                            this.ActiveControl = tc;

                            pnlRows.MakeControlVisible(tc);
                            selectionMade = tc.SelectNextControl(null, true, true, true, false);
                            this.Invalidate(new Rectangle(0, 0, rowHeaderWidth, this.Height), false);
                        }
                    }
                    else
                        if(changePolicy.AllowAdditions && pnlRows.Controls.Count != 0)
                        {
                            // If additions are allowed, focus the new row template
                            tc = (TemplateControl)pnlRows.Controls[0];
                            container.ActiveControl = this;

                            if(tc.Visible && tc.Enabled)
                            {
                                this.ActiveControl = tc;
                                pnlRows.MakeControlVisible(tc);
                                selectionMade = tc.SelectNextControl(null, true, true, true, false);
                                this.Invalidate(new Rectangle(0, 0, rowHeaderWidth, this.Height), false);
                            }
                        }

                    if(!selectionMade && header != null)
                    {
                        container.ActiveControl = this;
                        this.ActiveControl = header;
                        selectionMade = header.SelectNextControl(null, true, true, true, false);
                    }

                    if(!selectionMade && footer != null)
                    {
                        container.ActiveControl = this;
                        this.ActiveControl = footer;
                        selectionMade = footer.SelectNextControl(null, true, true, true, false);
                    }

                    if(!selectionMade)
                    {
                        container.ActiveControl = this;
                        this.ActiveControl = txtRowNum;
                        selectionMade = true;
                    }
                }
            }

            if(!selectionMade)
                base.Select(directed, forward);
        }

        /// <summary>
        /// This is overridden to refresh the data source when the binding context changes
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnBindingContextChanged(EventArgs e)
        {
            if(dataSource != null && !inSetListManager)
                SetListManager(dataSource, dataMember, true);

            base.OnBindingContextChanged(e);
        }

        /// <summary>
        /// This is overridden to scroll the data list as needed during drag and drop operations
        /// </summary>
        /// <param name="drgevent">The event arguments</param>
        /// <remarks>The list will scroll up if within 20 pixels of the top of the row panel.  It will scroll
        /// down if within 20 pixels of the bottom of the row panel.</remarks>
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            int row;

            Point p = this.PointToClient(Cursor.Position);

            if(lastMousePos != p)
            {
                lastMousePos = p;

                if(p.Y >= pnlRows.Top && p.Y <= pnlRows.Bottom)
                    row = (p.Y - pnlRows.Top - pnlRows.AutoScrollPosition.Y) / rowHeight;
                else
                    row = lastMouseRow;

                // Auto-scroll?
                if(p.Y < pnlRows.Top + 20)
                    row--;
                else
                    if(p.Y > pnlRows.Bottom - 20)
                        row++;

                if(row < 0)
                    row = 0;
                else
                    if(row >= listManager.Count)
                        row = listManager.Count - 1;

                if(row != lastMouseRow)
                {
                    this.EnsureVisible(row);
                    lastMouseRow = row;
                }
            }

            base.OnDragOver(drgevent);
        }

        /// <summary>
        /// This is overridden to provide validation support
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnValidating(CancelEventArgs e)
        {
            if(e != null)
            {
                e.Cancel = !this.IsValid;

                if(!e.Cancel)
                    base.OnValidating(e);
            }
        }

        /// <summary>
        /// This is overridden to initialize and bind any newly visible rows after the size changes
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.InitializeAndBindVisibleRows();
        }

        /// <summary>
        /// This is used to reposition the controls when the control attributes change
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnLayout(LayoutEventArgs e)
        {
            int detailHeight, top;

            if(suppressLayout)
                return;

            switch(this.BorderStyle)
            {
                case BorderStyle.Fixed3D:
                    borderWidth = 2;
                    break;

                case BorderStyle.FixedSingle:
                    borderWidth = 1;
                    break;


                default:
                    borderWidth = 0;
                    break;
            }

            btnFirst.Top = btnPrev.Top = txtRowNum.Top = btnNext.Top = btnLast.Top = btnAdd.Top = btnDelete.Top =
                lblRowCount.Top = this.Height - btnFirst.Height - (borderWidth * 2);

            if(rowHeadersVisible)
            {
                pnlRows.Left = headerFooterLeft = rowHeaderWidth;
                pnlRows.Width = this.Width - (borderWidth * 2) - rowHeaderWidth;
            }
            else
            {
                pnlRows.Left = headerFooterLeft = 0;
                pnlRows.Width = this.Width - (borderWidth * 2);
            }

            if(header != null)
                header.Left = headerFooterLeft;

            if(footer != null)
                footer.Left = headerFooterLeft;

            // Position the header, footer, and details sections
            if(showNav)
                detailHeight = btnFirst.Top - 1;
            else
                detailHeight = this.Height - (borderWidth * 2);

            if(showCaption)
            {
                // Size the caption based on the font
                Graphics g = Graphics.FromHwnd(this.Handle);
                SizeF size = g.MeasureString("0", lblCaption.Font);
                g.Dispose();

                lblCaption.Height = (int)size.Height + 6;
                detailHeight -= lblCaption.Height;
            }

            if(header != null)
            {
                header.Top = (showCaption) ? lblCaption.Height : 0;
                pnlRows.Top = header.Top + header.Height + 1;
                detailHeight -= header.Height + 1;
            }
            else
            {
                if(showCaption)
                {
                    pnlRows.Top = lblCaption.Height + 1;
                    detailHeight--;
                }
                else
                    pnlRows.Top = 0;
            }

            if(footer != null)
            {
                if(showNav)
                {
                    top = btnFirst.Top - footer.Height;
                    detailHeight -= footer.Height;
                }
                else
                {
                    top = this.Height - (borderWidth * 2) - footer.Height;
                    detailHeight -= footer.Height + 1;
                }

                if(top < pnlRows.Top)
                    footer.Top = pnlRows.Top;
                else
                    footer.Top = top;
            }

            if(detailHeight > 0)
                pnlRows.Height = detailHeight;
            else
                pnlRows.Height = 0;

            base.OnLayout(e);
        }

        /// <summary>
        /// This is overridden to handle selecting rows by clicking and/or dragging the mouse on the row headers
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            TemplateControl tc;
            int idx;

            if(e == null)
                throw new ArgumentNullException(nameof(e));

            // Figure out the row before calling the base class as we might have scrolled and resetting the focus
            // may shift the scrolled position.  Ignore it if there is no template defined.
            if(rowHeight != 0)
                lastMouseRow = (e.Y - pnlRows.Top - pnlRows.AutoScrollPosition.Y) / rowHeight;
            else
                lastMouseRow = -1;

            base.OnMouseDown(e);

            // Was it a click on a row header?
            if(this.ContainsFocus && listManager != null && listManager.Count != 0 && e.Y > pnlRows.Top &&
              e.Y < pnlRows.Bottom)
            {
                tc = this.CurrentItem;

                // Don't move if the current row is not valid
                if(tc != null && !tc.IsValid)
                {
                    this.Select(-1, -1, -1);
                    lastMouseRow = -1;
                    return;
                }

                if(lastMouseRow < 0)
                    lastMouseRow = 0;

                if(showSep)
                    idx = lastMouseRow * 2;
                else
                    idx = lastMouseRow;

                // Prepare for drag select?
                if(lastMouseRow < listManager.Count)
                {
                    tc = (TemplateControl)pnlRows.Controls[idx];

                    // Can't select the new row
                    if(!tc.IsNewRowInternal)
                    {
                        lastMousePos = new Point(e.X, e.Y);

                        // Extend selection?
                        if(selStart != -1 && (Control.ModifierKeys & Keys.Shift) != 0)
                        {
                            if(lastMouseRow <= selStart)
                                this.Select(lastMouseRow, selStart, lastMouseRow);
                            else
                                this.Select(selStart, lastMouseRow, lastMouseRow);
                        }
                        else
                        {
                            if(lastMouseRow >= selStart && lastMouseRow <= selEnd)
                            {
                                // If clicking within the current selection, assume it's a drag about to begin.
                                // The selection is left alone.  This also allows for a right click in the
                                // selection to bring up a context menu with options that work on the currently
                                // selected range.
                                dragMode = DragMode.DragAndDrop;
                                return;
                            }

                            this.Select(lastMouseRow, lastMouseRow, lastMouseRow);
                        }

                        dragMode = DragMode.Select;
                        txtRowNum.Text = (lastMouseRow + 1).ToString(CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    this.Select(-1, -1, -1);

                    // Allow for focusing the new row
                    if(idx < pnlRows.Controls.Count && !pnlRows.Controls[idx].ContainsFocus)
                        pnlRows.Controls[idx].Focus();
                }
            }
        }

        /// <summary>
        /// This is overridden to handle selecting rows by dragging the mouse on the row headers
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            TemplateControl tc;
            int row;

            if(e == null)
                throw new ArgumentNullException(nameof(e));

            // Drag and drop starting?
            if(dragMode == DragMode.DragAndDrop)
            {
                OnBeginDrag(new DataListBeginDragEventArgs(this, selStart, selEnd, e.Button));
                dragMode = DragMode.None;
                base.OnMouseMove(e);

                if(this.CurrentItem != null)
                    this.EnsureVisible(currentRow);

                return;
            }

            // We tend to get lots of these even if the mouse doesn't move.  It's an OS thing apparently.
            if(dragMode == DragMode.Select && (lastMousePos.X != e.X || lastMousePos.Y != e.Y))
            {
                lastMousePos = new Point(e.X, e.Y);

                if(e.Y >= pnlRows.Top && e.Y <= pnlRows.Bottom)
                    row = (e.Y - pnlRows.Top - pnlRows.AutoScrollPosition.Y) / rowHeight;
                else
                    if(e.Y < pnlRows.Top)
                        row = lastMouseRow - 1;
                    else
                        row = lastMouseRow + 1;

                if(row < 0)
                    row = 0;
                else
                    if(row >= listManager.Count)
                        row = listManager.Count - 1;

                if(showSep)
                    tc = (TemplateControl)pnlRows.Controls[row * 2];
                else
                    tc = (TemplateControl)pnlRows.Controls[row];

                // Can't select the new row
                if(tc.IsNewRowInternal)
                    row = lastMouseRow;

                if(row != lastMouseRow)
                {
                    if(lastMouseRow == selEnd)
                        this.Select(selStart, row, row);
                    else
                        this.Select(row, selEnd, row);

                    lastMouseRow = row;
                    txtRowNum.Text = (row + 1).ToString(CultureInfo.InvariantCulture);
                }
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// This is overridden to handle selecting rows by clicking and/or dragging the mouse on the row headers
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if(this.ContainsFocus && dragMode != DragMode.None)
            {
                if(currentRow != lastMouseRow && lastMouseRow < listManager.Count)
                {
                    TemplateControl tc = this.CurrentItem;

                    // Changing the position commits the new row so get rid of it if nothing has changed
                    if(tc != null && tc.IsNewRowInternal)
                        this.CancelChanges();

                    listManager.Position = lastMouseRow;
                }

                // If the drag and drop didn't happen, just select the row
                if(dragMode == DragMode.DragAndDrop)
                    this.Select(currentRow, currentRow, -1);

                dragMode = DragMode.None;
            }

            base.OnMouseUp(e);
        }

        /// <summary>
        /// This is overridden to ignore the row header area to help prevent flickering
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            Rectangle r = e.ClipRectangle;

            if(rowHeadersVisible && r.Left < borderWidth + rowHeaderWidth)
            {
                r.Location = new Point(borderWidth + rowHeaderWidth, r.Top);

                // Do not dispose of the PaintEventArgs instance as it isn't our graphics context
                base.OnPaintBackground(new PaintEventArgs(e.Graphics, r));
            }
            else
                base.OnPaintBackground(e);
        }

        /// <summary>
        /// This is overridden to draw row headers, a separator between the row panel and the header and
        /// footer/navigation button section, and to bind the rows as they scroll into view.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            base.OnPaint(e);

            // Don't bother if the layout is changing
            if(pnlRows.LayoutSuspended)
                return;

            Graphics g = e.Graphics;

            TemplateControl tc;
            Point[] pts;

            int row, rowPos, fillRange, maxRows, glyphPosY, multiplier = (showSep) ? 2 : 1;

            if(listManager != null && pnlRows.Controls.Count != 0)
            {
                maxRows = listManager.Count;

                if(changePolicy.AllowAdditions && (maxRows * multiplier) < pnlRows.Controls.Count)
                    maxRows++;

                // Get the position of the topmost visible row
                rowPos = pnlRows.AutoScrollPosition.Y - (pnlRows.AutoScrollPosition.Y / rowHeight * rowHeight);

                row = pnlRows.AutoScrollPosition.Y / rowHeight * -1;
                fillRange = rowPos + rowHeight + pnlRows.Height;
            }
            else
            {
                row = rowPos = maxRows = 0;
                fillRange = pnlRows.Height;
            }

            if(showCaption)
            {
                rowPos += lblCaption.Height;
                fillRange += lblCaption.Height;

                if(header == null)
                {
                    rowPos++;
                    fillRange++;
                }
            }

            if(header != null)
            {
                rowPos += header.Height + 1;
                fillRange += header.Height + 1;
            }

            // Erase the area to the left of the header and footer
            if(header != null)
                g.FillRectangle(brHeaderBack, 0, header.Top, rowHeaderWidth, header.Height);

            if(footer != null)
                g.FillRectangle(brHeaderBack, 0, footer.Top, rowHeaderWidth, footer.Height);

            // Restrict row header drawing to the area to the left of the row panel
            Region clip = g.Clip;
            g.Clip = new Region(new Rectangle(0, pnlRows.Top, rowHeaderWidth, pnlRows.Height));

            while(rowPos < fillRange && row < maxRows)
            {
                if(row * multiplier < pnlRows.Controls.Count)
                    tc = (TemplateControl)pnlRows.Controls[row * multiplier];
                else
                    tc = null;

                if(rowHeadersVisible)
                {
                    if(row >= selStart && row <= selEnd)
                        g.FillRectangle(brSelBack, 0, rowPos, rowHeaderWidth, rowHeight);
                    else
                        g.FillRectangle(brHeaderBack, 0, rowPos, rowHeaderWidth, rowHeight);

                    if(!rowHeadersFlat)
                    {
                        g.DrawLine(SystemPens.ControlLightLight, 0, rowPos, rowHeaderWidth, rowPos);
                        g.DrawLine(SystemPens.ControlLightLight, 0, rowPos, 0, rowPos + rowHeight - 1);
                        g.DrawLine(SystemPens.ControlDarkDark, rowHeaderWidth - 1, rowPos, rowHeaderWidth - 1,
                            rowPos + rowHeight - 1);
                        g.DrawLine(SystemPens.ControlDarkDark, 0, rowPos + rowHeight - 1, rowHeaderWidth,
                            rowPos + rowHeight - 1);
                    }
                    else
                    {
                        g.DrawLine(SystemPens.ControlDark, 0, rowPos, 0, rowPos + rowHeight);
                        g.DrawLine(SystemPens.ControlDark, rowHeaderWidth - 1, rowPos, rowHeaderWidth - 1,
                            rowPos + rowHeight);
                        g.DrawLine(SystemPens.ControlDark, 0, rowPos + rowHeight - 1, rowHeaderWidth,
                            rowPos + rowHeight - 1);
                    }

                    if(row == currentRow || (tc != null && tc.IsNewRowInternal))
                    {
                        glyphPosY = rowPos + 2;

                        if(row == currentRow)
                        {
                            pts = new Point[3];

                            for(int p = 0; p < 3; p++)
                            {
                                pts[p] = ptsCurrent[p];
                                pts[p].Y += glyphPosY;
                            }
                        }
                        else
                        {
                            if(rowHeaderWidth < rowHeight)
                                glyphPosY += rowHeaderWidth / 2;
                            else
                                glyphPosY += rowHeight / 2;

                            pts = new Point[16];

                            for(int p = 0; p < 16; p++)
                            {
                                pts[p] = ptsNewRow[p];
                                pts[p].Y += glyphPosY;
                            }
                        }

                        if(row >= selStart && row <= selEnd)
                            g.FillPolygon(brSelFore, pts);
                        else
                            g.FillPolygon(brHeaderFore, pts);
                    }
                }

                // Initialize the template and bind the controls as the templates scroll into view.  This saves
                // some time and resources during the initial load and only binds what is actually shown.
                if(tc != null && !tc.IsNewRowInternal && !tc.HasBeenBound)
                {
                    if(!tc.HasBeenInitialized)
                    {
                        tc.InitializeTemplate();
                        tc.HasBeenInitialized = true;
                    }

                    tc.Bind();
                    tc.HasBeenBound = true;
                    OnItemDataBound(new DataListEventArgs(row, tc));
                }

                rowPos += rowHeight;
                row++;
            }

            if(rowPos < fillRange)
                g.FillRectangle(brHeaderBack, 0, rowPos, rowHeaderWidth, fillRange - rowPos);

            g.Clip = clip;

            if(header != null || showCaption)
                g.DrawLine(SystemPens.ControlDark, 0, pnlRows.Top - 1, this.Width, pnlRows.Top - 1);

            if(footer != null)
                g.DrawLine(SystemPens.ControlDark, 0, footer.Top - 1, this.Width, footer.Top - 1);
            else
                if(showNav)
                    g.DrawLine(SystemPens.ControlDark, 0, btnFirst.Top - 1, this.Width, btnFirst.Top - 1);
        }

        /// <summary>
        /// This is overridden to handle gaining the focus correctly
        /// </summary>
        /// <param name="m">The message</param>
        /// <remarks>Normal focusing behavior returns the focus to the first row or last row.  This is overridden
        /// to keep the focus on the last selected row.</remarks>
        protected override void WndProc(ref Message m)
        {
            // WM_SETFOCUS
            if(m.Msg == 0x0007 && listManager != null && listManager.Count != 0)
            {
                TemplateControl tc = this.CurrentItem;

                if(tc != null)
                {
                    pnlRows.MakeControlVisible(tc);
                    tc.Focus();
                }

                return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Calling this method is the equivalent of setting the <see cref="DataSource"/>, <see cref="DataMember"/>,
        /// and <see cref="RowTemplate"/> properties individually.
        /// </summary>
        /// <param name="dataSource">The data source to use</param>
        /// <param name="member">The data member in the data source to use, if any</param>
        /// <param name="rowTemplateType">The template control type to use for creating the rows</param>
        /// <overloads>There are two overloads for this method</overloads>
        public void SetDataBinding(object dataSource, string member, Type rowTemplateType)
        {
            this.RowTemplate = null;
            this.SetListManager(dataSource, member, false);
            this.RowTemplate = rowTemplateType;
        }

        /// <summary>
        /// Calling this method is the equivalent of setting the <see cref="DataSource"/>, <see cref="DataMember"/>,
        /// <see cref="RowTemplate"/>, <see cref="HeaderTemplate"/>, and <see cref="FooterTemplate"/> properties
        /// individually.
        /// </summary>
        /// <param name="dataSource">The data source to use</param>
        /// <param name="member">The data member in the data source to use, if any</param>
        /// <param name="rowTemplateType">The template control type to use for creating the rows</param>
        /// <param name="headerTemplateType">The template control type to use for creating the header, if any</param>
        /// <param name="footerTemplateType">The template control type to use for creating the footer, if any</param>
        public void SetDataBinding(object dataSource, string member, Type rowTemplateType, Type headerTemplateType,
          Type footerTemplateType)
        {
            this.RowTemplate = this.HeaderTemplate = this.FooterTemplate = null;
            this.SetListManager(dataSource, member, false);
            this.HeaderTemplate = headerTemplateType;
            this.FooterTemplate = footerTemplateType;
            this.RowTemplate = rowTemplateType;
        }

        /// <summary>
        /// This method is used to move the focus to the specified row in the data source
        /// </summary>
        /// <param name="newRow">The zero-based row number to which the focus is moved</param>
        /// <returns>True if the specified row now has the focus or false if the focus could not be set due to
        /// validation failure on the current row.</returns>
        /// <remarks>Before moving to the specified row, the <see cref="TemplateControl.IsValid"/> property is
        /// checked on the current row template to ensure that it is safe to move.  If it returns false, the
        /// focus will stay on the current row.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the value is outside the bounds of
        /// the list or if there is no data source.</exception>
        /// <overloads>There are two overloads for this method</overloads>
        public bool MoveTo(int newRow)
        {
            if(currentRow != newRow)
            {
                if(newRow < 0 || listManager == null || newRow >= listManager.Count)
                    throw new ArgumentOutOfRangeException(nameof(newRow), newRow, LR.GetString("ExInvalidRowNumber"));

                if(!this.IsValid)
                    return false;

                // Scroll the row into view first.  This smooths out scrolling if called repeatedly (i.e.
                // Page Up/Down).
                pnlRows.ScrollRowIntoView(newRow);

                listManager.Position = newRow;

                if(rowHeadersVisible)
                    this.Invalidate(new Rectangle(0, 0, rowHeaderWidth, this.Height), false);
            }

            this.Select(-1, -1, -1);
            return true;
        }

        /// <summary>
        /// This method is used to move the focus to the specified fixed row position in the data source
        /// </summary>
        /// <param name="position">The position to which the focus is moved</param>
        /// <returns>True if the specified position now has the focus or false if the focus could not be set due
        /// to validation failure on the current row.</returns>
        /// <remarks>Before moving to the specified position, the <see cref="TemplateControl.IsValid"/> property
        /// is checked on the current row template to ensure that it is safe to move.  If it returns false, the
        /// focus will stay on the current row.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if there is no data source or there are
        /// no rows.</exception>
        /// <exception cref="NotSupportedException">This is thrown if additions are not currently allowed as
        /// defined by the current <see cref="AllowAdditions"/> property setting and an attempt is made to move
        /// to the <c>NewRow</c> position.</exception>
        public bool MoveTo(RowPosition position)
        {
            int newRow;

            if(listManager == null || (listManager.Count == 0 && position != RowPosition.NewRow))
                throw new ArgumentOutOfRangeException(nameof(position), position, LR.GetString("ExInvalidRowNumber"));

            switch(position)
            {
                case RowPosition.FirstRow:
                    newRow = 0;
                    break;

                case RowPosition.LastRow:
                    newRow = listManager.Count - 1;
                    break;

                case RowPosition.NextRow:
                    newRow = listManager.Position + 1;
                    break;

                case RowPosition.PreviousRow:
                    newRow = listManager.Position - 1;
                    break;

                default:    // New row
                    if(!changePolicy.AllowAdditions)
                        throw new NotSupportedException(LR.GetString("ExAddNotAllowed"));

                    if(!this.IsValid)
                        return false;

                    newRow = pnlRows.Controls.Count - 1;

                    if(showSep)
                        newRow--;

                    if(!pnlRows.Controls[newRow].ContainsFocus)
                        pnlRows.Controls[newRow].Focus();

                    this.Select(-1, -1, -1);
                    return true;
            }

            // This will throw an exception if the new row number isn't valid
            return this.MoveTo(newRow);
        }

        /// <summary>
        /// This can be used as an alternative to the <c>Delete</c> button to delete a row from the list
        /// </summary>
        /// <param name="delRow">The row to delete</param>
        /// <returns>True if the row was deleted, false if not deleted due to cancellation</returns>
        /// <remarks>This is equivalent to clicking the <c>Delete</c> button but it allows you to put a button on
        /// each row's template, or in the header or footer template, or handle the delete operation in some
        /// other fashion.  The specified row's <see cref="TemplateControl.CanDelete"/> method is called and, if
        /// it allows the delete to continue, the <see cref="DeletingRow"/> event is fired prior to deleting the
        /// specified row.  If the template will allow the deletion and the <c>DeletingRow</c> event is not
        /// canceled, the specified row is deleted and the <see cref="DeletedRow"/> event is fired.</remarks>
        /// <exception cref="NotSupportedException">This is thrown if there is no row template defined or if
        /// deletions are not currently allowed as defined by the current <see cref="AllowDeletes"/> property
        /// setting.</exception>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the specified row is outside the
        /// bounds of the list or if there is no data source.</exception>
        public bool DeleteRow(int delRow)
        {
            if(rowTemplate == null)
                throw new NotSupportedException(LR.GetString("ExNoTemplate"));

            if(!changePolicy.AllowDeletes)
                throw new NotSupportedException(LR.GetString("ExDeleteNotAllowed"));

            if(delRow < 0 || listManager == null || delRow >= listManager.Count)
                throw new ArgumentOutOfRangeException(nameof(delRow), delRow, LR.GetString("ExInvalidRowNumber"));

            return this.DeleteRowInternal(delRow, false);
        }

        /// <summary>
        /// This is used to manually commit pending changes to the current row in the data source
        /// </summary>
        /// <remarks><para>This will call the <see cref="TemplateControl.CommitChanges"/> method on the current
        /// row template.  It will also call the <c>EndCurrentEdit</c> method for the data source on the current
        /// binding context.</para>
        /// 
        /// <para>Due to the way data binding works in .NET, pending changes to the current row may not have been
        /// committed to the data source when you are ready to save changes to the underlying data source.  As
        /// such, you should always call the <c>CommitChanges</c> or <see cref="CancelChanges"/> method on the
        /// data list control prior to checking for or saving changes in its underlying data source.</para>
        /// </remarks>
        /// <example>
        /// Assume <c>dataSet</c> has been assigned to <c>dataList</c> as its data source.
        /// <code language="cs">
        /// // Commit any pending edits in the data list
        /// dataList.CommitChanges();
        ///
        /// // If changes were made to the data source, save them
        /// if(dataSet.HasChanges())
        ///     dataAdapter.Update(dataSet);
        /// </code>
        /// <code language="vbnet">
        /// ' Commit any pending edits in the data list
        /// dataList.CommitChanges()
        ///
        /// ' If changes were made to the data source, save them
        /// If dataSet.HasChanges() Then
        ///     dataAdapter.Update(dataSet)
        /// End If
        /// </code>
        /// </example>
        public void CommitChanges()
        {
            if(listManager != null && listManager.Count != 0)
            {
                TemplateControl tc = this.CurrentItem;

                if(tc != null)
                {
                    // If it's the new row place holder, cancel the changes on the data source instead
                    if(tc.IsNewRowInternal)
                        listManager.CancelCurrentEdit();
                    else
                        if(tc.RowSource != null)
                            tc.BindingContext[tc.RowSource].EndCurrentEdit();
                }

                listManager.EndCurrentEdit();
            }
        }

        /// <summary>
        /// This is used to manually cancel pending changes to the current row in the data source
        /// </summary>
        /// <remarks><para>This will call the <see cref="TemplateControl.CancelChanges"/> method on the current
        /// row template.  It will also call the <c>CancelCurrentEdit</c> method for the data source in the
        /// current binding context.</para>
        /// 
        /// <para>Due to the way data binding works in .NET, pending changes to the current row may not have been
        /// committed to the data source when you are ready to save changes to the underlying data source.  As
        /// such, you should always call the <see cref="CommitChanges"/> or <c>CancelChanges</c> method on the
        /// data list control prior to checking for or saving changes in its underlying data source.</para>
        /// </remarks>
        public void CancelChanges()
        {
            if(listManager != null && listManager.Count != 0)
            {
                try
                {
                    isUndoing = true;
                    TemplateControl tc = this.CurrentItem;

                    if(tc != null && tc.RowSource != null)
                    {
                        DataRowView drv = tc.RowSource as DataRowView;

                        // A DataRowView object will not exist in the data source if it is the new row.  If so,
                        // ignore the request.
                        if((drv == null || !drv.IsNew) && !tc.IsNewRowInternal)
                            tc.BindingContext[tc.RowSource].CancelCurrentEdit();
                    }

                    listManager.CancelCurrentEdit();
                }
                finally
                {
                    isUndoing = false;
                }
            }
        }

        /// <summary>
        /// This is used to set the selected range of rows
        /// </summary>
        /// <param name="start">The zero-based starting row number</param>
        /// <param name="end">The zero-based ending row number</param>
        /// <param name="ensureVisible">The row to ensure is visible after setting the selection.  Specify -1 to
        /// not change the visible row.</param>
        /// <remarks>If either value is outside the current row count, it will be forced to a valid value.  The
        /// range will also be swapped if necessary to start from the lowest value and end at the highest
        /// value.</remarks>
        public void Select(int start, int end, int ensureVisible)
        {
            if(selStart != start || selEnd != end)
            {
                if(listManager == null || start < 0 || end < 0)
                    selStart = selEnd = -1;
                else
                {
                    if(start >= listManager.Count)
                        start = listManager.Count - 1;

                    if(end >= listManager.Count)
                        end = listManager.Count - 1;

                    if(start < end)
                    {
                        selStart = start;
                        selEnd = end;
                    }
                    else
                    {
                        selStart = end;
                        selEnd = start;
                    }
                }

                if(selStart != -1 && ensureVisible != -1)
                    this.EnsureVisible(ensureVisible);
                else
                    if(rowHeadersVisible)
                    {
                        this.Invalidate(new Rectangle(0, 0, rowHeaderWidth, this.Height), false);
                        this.Update();
                    }
            }
        }

        /// <summary>
        /// This is used to ensure that the specified row is visible in the data list
        /// </summary>
        /// <param name="row">The zero-based row number to ensure is visible</param>
        /// <remarks>If not visible, the specified row is scrolled into view.  If the row number is outside the
        /// bounds of the current rows, it is forced to a valid value.</remarks>
        public void EnsureVisible(int row)
        {
            ControlCollection cc = pnlRows.Controls;

            if(row < 0)
                row = 0;
            else
                if(showSep)
                    row *= 2;

            if(row >= cc.Count)
                row = cc.Count - 1;

            if(row > -1 && row < cc.Count)
            {
                TemplateControl c = (TemplateControl)cc[row];

                // Initialize it first if necessary
                if(!c.HasBeenInitialized)
                {
                    c.InitializeTemplate();
                    c.HasBeenInitialized = true;
                }

                pnlRows.MakeControlVisible(c);
                this.Invalidate();
                this.Update();
            }
        }

        /// <summary>
        /// Test a point within the data list control to see within which part it falls
        /// </summary>
        /// <param name="p">The point to test</param>
        /// <returns>Information about the location</returns>
        public DataListHitTestInfo HitTest(Point p)
        {
            Rectangle r;
            int row;

            if(p.X < 0 || p.X > this.Width || p.Y < 0 || p.Y > this.Height)
                return DataListHitTestInfo.Nowhere;

            if(showNav && p.Y >= btnFirst.Top)
                return new DataListHitTestInfo(DataListHitType.Navigation);

            if(header != null)
            {
                r = new Rectangle(header.Location, header.Size);

                if(r.Contains(p))
                    return new DataListHitTestInfo(DataListHitType.Header);
            }

            if(footer != null)
            {
                r = new Rectangle(footer.Location, footer.Size);

                if(r.Contains(p))
                    return new DataListHitTestInfo(DataListHitType.Footer);
            }

            if(listManager != null && listManager.Count != 0 && p.Y >= pnlRows.Top && p.Y < pnlRows.Bottom)
            {
                row = (p.Y - pnlRows.Top - pnlRows.AutoScrollPosition.Y) / rowHeight;

                if(row > -1 && row < listManager.Count)
                {
                    if(rowHeadersVisible && p.X < rowHeaderWidth)
                        return new DataListHitTestInfo(DataListHitType.RowHeader, row);

                    return new DataListHitTestInfo(DataListHitType.Row, row);
                }
            }

            return DataListHitTestInfo.Nowhere;
        }

        /// <summary>
        /// Find an item value in the specified member of the data source
        /// </summary>
        /// <param name="member">The member in the data source to search for the key value</param>
        /// <param name="key">The item to find in the data source</param>
        /// <returns>The zero-based index of the found item or -1 if not found</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if specified member could not be found
        /// in the data source.</exception>
        /// <exception cref="ArgumentNullException">This is thrown if the member name or the item to find is
        /// null.</exception>
        public int Find(string member, object key)
        {
            int idx;

            if(member == null || member.Length == 0)
                throw new ArgumentNullException(nameof(member), LR.GetString("ExNullFindParam"));

            if(key == null)
                throw new ArgumentNullException(nameof(key), LR.GetString("ExNullFindParam"));

            PropertyDescriptorCollection coll = listManager.GetItemProperties();
            PropertyDescriptor prop = coll.Find(member, true);

            if(prop == null)
                throw new ArgumentOutOfRangeException(nameof(member), member, LR.GetString("ExInvalidMember"));

            if(listManager.List is IBindingList bl && bl.SupportsSearching)
            {
                idx = bl.Find(prop, key);

                // Don't know why but on lists with new rows added, the above occasionally returns a weird
                // negative number.  If so, just search the list manually which does find it.
                if(idx >= -1)
                    return idx;
            }

            for(idx = 0; idx < listManager.List.Count; idx++)
                if(key.Equals(prop.GetValue(listManager.List[idx])))
                    return idx;

            return -1;
        }

        /// <summary>
        /// Finds the first item whose given member matches the specified string. The search is not
        /// case-sensitive.
        /// </summary>
        /// <param name="member">The member in the data source to search for the string value</param>
        /// <param name="key">The string for which to search</param>
        /// <returns>The zero-based index of the found item or -1 if not found</returns>
        /// <overloads>There are two overloads for this method</overloads>
        /// <exception cref="ArgumentNullException">This is thrown if the member name or the item to find is
        /// null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if specified member could not be found
        /// in the data source.</exception>
        public int FindStringExact(string member, string key)
        {
            return this.FindString(member, key, -1, true, true);
        }

        /// <summary>
        /// Finds the first item after the given index whose given data member matches the given string. The
        /// search is not case-sensitive.
        /// </summary>
        /// <param name="member">The member in the data source to search for the string value.</param>
        /// <param name="key">The string for which to search.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched.  Set
        /// to -1 to search from the beginning of the list.</param>
        /// <returns>The zero-based index of the first item found.  Returns -1 if no match is found.</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the start index value is not within
        /// the bounds of the list and it is not -1 or if the specified member could not be found in the data
        /// source.</exception>
        /// <exception cref="ArgumentNullException">This is thrown if the member name or the item to find is
        /// null.</exception>
        public int FindStringExact(string member, string key, int startIndex)
        {
            return FindString(member, key, startIndex, true, true);
        }

        /// <summary>
        /// Finds the first item whose given member starts with the given string. The search is not
        /// case-sensitive.
        /// </summary>
        /// <param name="member">The member in the data source to search for the string value</param>
        /// <param name="key">The string for which to search</param>
        /// <returns>The zero-based index of the found item or -1 if not found</returns>
        /// <overloads>There are three overloads for this method</overloads>
        /// <exception cref="ArgumentNullException">This is thrown if the member name or the item to find is
        /// null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if specified member could not be found
        /// in the data source.</exception>
        public int FindString(string member, string key)
        {
            return this.FindString(member, key, -1, false, true);
        }

        /// <summary>
        /// Finds the first item after the given index whose given data member starts with the given string.  The
        /// search is not case-sensitive.
        /// </summary>
        /// <param name="member">The member in the data source to search for the string value.</param>
        /// <param name="key">The string for which to search.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched.  Set
        /// to -1 to search from the beginning of the list.</param>
        /// <returns>The zero-based index of the first item found.  Returns -1 if no match is found.</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the start index value is not within
        /// the bounds of the list and it is not -1 or if the specified member could not be found in the data
        /// source.</exception>
        /// <exception cref="ArgumentNullException">This is thrown if the member name or the item to find is
        /// null.</exception>
        public int FindString(string member, string key, int startIndex)
        {
            return FindString(member, key, startIndex, false, true);
        }

        /// <summary>
        /// Finds the first item after the given index whose given data member matches the given string.  Partial
        /// matches and case-sensitivity are optional.
        /// </summary>
        /// <param name="member">The member in the data source to search for the string value.</param>
        /// <param name="key">The string for which to search.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched.  Set
        /// to -1 to search from the beginning of the list.</param>
        /// <param name="exactMatch">Specify true to find an exact match or false to find the first item starting
        /// with the specified string.</param>
        /// <param name="ignoreCase">Specify true for a case-insensitive search or false for a case-sensitive
        /// search.</param>
        /// <returns>The zero-based index of the first item found.  Returns -1 if no match is found.</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the start index value is not within
        /// the bounds of the list and it is not -1 or if the specified member could not be found in the data
        /// source.</exception>
        /// <exception cref="ArgumentNullException">This is thrown if the member name or the item to find is
        /// null.</exception>
        public int FindString(string member, string key, int startIndex, bool exactMatch, bool ignoreCase)
        {
            bool found;
            int length, idx;
            string propValue;

            if(listManager == null || listManager.Count == 0)
                return -1;

            if(startIndex < -1 || startIndex >= listManager.Count)
                throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, LR.GetString("ExInvalidItemIndex"));

            if(member == null || member.Length == 0)
                throw new ArgumentNullException(nameof(member), LR.GetString("ExNullFindParam"));

            if(key == null)
                throw new ArgumentNullException(nameof(key), LR.GetString("ExNullFindParam"));

            PropertyDescriptorCollection coll = listManager.GetItemProperties();
            PropertyDescriptor prop = coll.Find(member, true);

            if(prop == null)
                throw new ArgumentOutOfRangeException(nameof(member), member, LR.GetString("ExInvalidMember"));

            length = key.Length;
            idx = startIndex;

            while(true)
            {
                idx++;
                propValue = prop.GetValue(listManager.List[idx]).ToString();

                if(exactMatch)
                    found = String.Equals(key, propValue, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
                else
                {
                    found = String.Compare(key, 0, propValue, 0, length,
                        ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
                }

                if(found)
                    return idx;

                // If we hit the end loop back to the start
                if(idx == listManager.Count - 1)
                    idx = -1;

                // If we've been all the way through, give up
                if(idx == startIndex)
                    return -1;
            }
        }
        #endregion
    }
}
