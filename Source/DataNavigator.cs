//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DataNavigator.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/11/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a control that is used to navigate through a specified data source and perform operations
// on it such as editing, inserting, or deleting records, etc. along with other controls on the form that are
// bound to the same data source.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/20/2005  EFW  Created the code
//===============================================================================================================

using System.Collections;
using System.Data;
using System.Drawing.Design;
using System.Globalization;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This control is used to navigate through a specified data source and perform operations on it such as
    /// editing, inserting, or deleting records, etc. along with other controls on the form that are bound to
    /// the same data source.
    /// </summary>
    [DefaultEvent("Current"), DefaultProperty("DataSource"),
      Description("Allows navigation through a data source one row at a time")]
	public class DataNavigator : UserControl
	{
        #region Private data members
        //=====================================================================

        private ImageList ilButtons = null!;
        private Button btnFirst = null!;
        private Button btnPrev = null!;
        private Button btnNext = null!;
        private Button btnLast = null!;
        internal Button btnAdd = null!;
        private NumericTextBox txtRowNum = null!;
        private Label lblRowCount = null!;
        internal Button btnDelete = null!;
        private System.Windows.Forms.Timer tmrRepeat = null!;
        private Container components = null!;

        // Add/Delete properties
        private bool showAddDel;
        private Shortcut shortcutAdd, shortcutDel, shortcutRowNum;

        // Auto-repeat properties
        private Button repeatButton = null!;
        private int repeatWait, repeatInterval;

        // Data source information
        private object? dataSource;
        private string dataMember;
        private CurrencyManager? listManager;

        private bool inSetListManager, inAddRow, inDelRow;

        private int currentRow;

        // Change policy.  These settings determine how the data source can be modified
        private readonly ChangePolicy changePolicy;
        private bool allowAdditions, allowEdits, allowDeletes;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This returns the <see cref="CurrencyManager"/> that the data navigator is currently using to get data
        /// from the <see cref="DataSource"/>/<see cref="DataMember"/> pair.
        /// </summary>
        [Browsable(false), Description("Returns the CurrencyManager that the data navigator is currently using " +
          "to get data from the DataSource/DataMember pair.")]
        public CurrencyManager? ListManager
        {
            get
            {
                if(listManager == null && this.Parent != null && this.BindingContext != null && dataSource != null)
                    return (CurrencyManager)this.BindingContext[dataSource, dataMember];

                return listManager;
            }
        }

        /// <summary>
        /// This property is used to set or get whether or not the add and delete buttons are displayed
        /// </summary>
        /// <value>The buttons are shown by default.  They can be hidden if you would prefer to handle add and
        /// delete operations via some other means such as other buttons on the parent form.</value>
        [Category("Appearance"), Bindable(true), DefaultValue(true),
          Description("Show or hide the add and delete buttons")]
        public bool AddDeleteButtonsVisible
        {
            get => showAddDel;
            set
            {
                if(showAddDel != value)
                {
                    btnAdd.Visible = btnDelete.Visible = showAddDel = value;

                    if(value)
                        lblRowCount.Left = btnDelete.Left + btnDelete.Width + 5;
                    else
                        lblRowCount.Left = btnLast.Left + btnLast.Width + 5;

                    OnAddDeleteButtonsVisibleChanged(EventArgs.Empty);
                }
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
                allowAdditions = value;
                changePolicy.UpdatePolicy(value, allowEdits, allowDeletes);
            }
        }

        /// <summary>
        /// This is used to determine whether or not edits are allowed to be made to the data source
        /// </summary>
        /// <value>If set to true, the data source may override it if it does not allow edits.  Note that it is
        /// up to the parent control or form to check this property and disable editing in its controls if this
        /// is set to false.</value>
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
        /// This is used to set or get the shortcut key to use for adding a row
        /// </summary>
        /// <remarks>The default is <c>Ctrl+Shift+A</c>.  It is ignored if additions are not allowed</remarks>
        [Category("Behavior"), DefaultValue(typeof(Shortcut), "CtrlShiftA"),
         Description("The shortcut key to use for adding a row")]
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
        /// <remarks>The default is <c>F5</c>.</remarks>
        [Category("Behavior"), DefaultValue(typeof(Shortcut), "F5"),
          Description("The shortcut key to use for jumping to the row number navigation text box")]
        public Shortcut RowNumberNavShortcut
        {
            get => shortcutRowNum;
            set => shortcutRowNum = value;
        }

        /// <summary>
        /// This property is used to set or get the initial wait in milliseconds before the <c>Next</c> and
        /// <c>Previous</c> buttons auto-repeat to navigate through the data source.
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
        /// <c>Previous</c> buttons when they auto-repeat to navigate through the data source.
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
        /// This gets or sets the data source for the data navigator
        /// </summary>
        /// <value>The data source object must support the <see cref="IList"/> interface such as a
        /// <see cref="System.Data.DataSet"/> or an <see cref="Array"/>.  This property must be set in order for
        /// the control to display information.  If the data source contains multiple items to which the control
        /// can bind, use the <see cref="DataMember"/> property to specify the sub-list to use.
        /// </value>
        /// <exception cref="ArgumentException">This is thrown if the data source does not support the
        /// <see cref="IList"/> interface.</exception>
        [Category("Data"), DefaultValue(null), RefreshProperties(RefreshProperties.Repaint),
          AttributeProvider(typeof(IListSource)),
          TypeConverter("Design.DataSourceConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
          Description("Set or get the data source for the data navigator")]
        public object? DataSource
        {
            get => dataSource;
            set
            {
                if(value != null && value is not IList && value is not IListSource)
                    throw new ArgumentException(LR.GetString("ExBadDataSource"));

                if(dataSource != value)
                {
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
        /// This indicates the sub-list (if any) of the <see cref="DataSource"/> to show in the data navigator
        /// </summary>
        [Category("Data"), DefaultValue(""),
          Editor("Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
          Description("Indicates a sub-list of the data source to show in the data navigator")]
        public string DataMember
        {
            get => dataMember;
            set
            {
                if(dataMember != value)
                {
                    dataMember = value;
                    this.EnforceValidDataMember(dataSource, value);
                    this.SetListManager(dataSource, value, false);
                }
            }
        }

        /// <summary>
        /// This read-only property can be used to get the current row count from the data source
        /// </summary>
        [Browsable(false), Description("Get the row count from the data source")]
        public int RowCount => (listManager == null) ? 0 : listManager.Count;

        /// <summary>
        /// This read-only property is used to get the zero-based row number of the currently selected row
        /// </summary>
        /// <value>If there is no data source or there are no rows in the data source, it returns -1</value>
        /// <remarks>To set the current row, use the <see cref="MoveTo(Int32)"/> method</remarks>
        [Browsable(false), Description("Get the currently selected row index")]
        public int CurrentRow
        {
            get
            {
                if(listManager == null)
                    return -1;

                return currentRow;
            }
        }

        /// <summary>
        /// This property can be used to query the current row to see if it is valid
        /// </summary>
        /// <value>Returns true if it is valid or there are no items, false if it is not</value>
        /// <remarks>This is useful in situations where the validating events are fired after certain other
        /// events (i.e. tree view and data grid item selection events).  This raises the
        /// <see cref="Control.OnValidating"/> and <see cref="Control.OnValidated"/> events on itself.</remarks>
        /// <example>
        /// <code language="cs">
        /// // Prevent a change of the tree view node if the current row in the
        /// // data navigator is not valid.
        /// private void tree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        /// {
        ///     e.Cancel = !dataNav.IsValid;
        /// }
        /// </code>
        /// <code language="vbnet">
        /// ' Prevent a change of the tree view node if the current row in the
        /// ' data navigator is not valid.
        /// Private Sub tree_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) _
        ///   Handles tree.BeforeSelect
        ///     e.Cancel = Not dataNav.IsValid
        /// End Sub
        /// </code>
        /// </example>
        [Browsable(false), Description("Check to see if the current row is valid")]
        public bool IsValid
        {
            get
            {
                CancelEventArgs e = new();
                base.OnValidating(e);

                if(!e.Cancel)
                    base.OnValidated(EventArgs.Empty);

                return !e.Cancel;
            }
        }

        /// <summary>
        /// This read-only property can be used to see if the data source has been modified
        /// </summary>
        /// <value>The <see cref="CommitChanges"/> method is called first to commit any pending changes to the
        /// data source.  The data navigator can detect changes only if the data source is a <c>DataSet</c>,
        /// <c>DataView</c>, or a <c>DataTable</c>.  In those cases, it returns true if the data source has been
        /// modified or false if it has not.  For all other data source types, it will always returns false.
        /// You may override this property in order to extend the types that it knows about and detect changes
        /// in them.</value>
        /// <example>
        /// <code language="cs">
        /// // Assume daItems is a data adapter, dsItems is a DataSet and
        /// // dsItems is the data source for dnNav (a data navigator control).
        /// // If the data navigator has changes, save them.
        /// if(dnNav.HasChanges)
        ///     daItems.Update(dsItems);
        /// </code>
        /// <code language="vbnet">
        /// ' Assume daItems is a data adapter, dsItems is a DataSet and
        /// ' dsItems is the data source for dnNav (a data navigator control).
        /// ' If the data navigator has changes, save them.
        /// If dnNav.HasChanges Then
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

                    DataTable? tbl;

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
        /// This can be used to get the value of the specified column in the currently selected item
        /// </summary>
        /// <param name="colName">The column name of the item to get.  This can be any column in the data source,
        /// not just those displayed by the bound controls in the parent.</param>
        /// <value>This returns the entry at the specified column in the currently selected item.  If there is no
        /// data source or the column cannot be found, this returns null.</value>
        /// <overloads>There are two overloads for this property</overloads>
        [Browsable(false), Description("Get the specified column from the current row")]
        public object? this[string colName] => this[currentRow, colName];

        /// <summary>
        /// This can be used to get the value of the specified column in the specified row of the data
        /// navigator's data source.
        /// </summary>
        /// <param name="rowIdx">The row index of the item.</param>
        /// <param name="colName">The column name of the item to get.  This can be any column in the data source,
        /// not just those displayed by the bound controls in the parent.</param>
        /// <value>This returns the entry at the specified column in the specified row.  If the row is out of
        /// bounds or if the column cannot be found, this will return null.</value>
        [Browsable(false), Description("Get the specified column from the specified row")]
        public object? this[int rowIdx, string colName]
        {
            get
            {
                if(rowIdx < 0 || listManager == null || rowIdx >= listManager.Count || colName == null ||
                  colName.Length == 0)
                {
                    return null;
                }

                object? oItem = listManager.List[rowIdx];

                if(oItem != null)
                {
                    PropertyDescriptor? pd = listManager.GetItemProperties().Find(colName, true);

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
        /// The data navigator does not use this property so it is hidden.  It always returns null.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false),
          EditorBrowsable(EditorBrowsableState.Never)]
        public override Image? BackgroundImage => null;

        /// <summary>
        /// The data navigator does not use this property so it is hidden.  It always returns false.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false),
          EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AutoScroll => false;

        /// <summary>
        /// The data navigator does not use this property so it is hidden.  It always returns the base margin.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false),
          EditorBrowsable(EditorBrowsableState.Never)]
        public new Size AutoScrollMargin => base.AutoScrollMargin;

        /// <summary>
        /// The data navigator does not use this property so it is hidden.  It always returns the base size.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false),
          EditorBrowsable(EditorBrowsableState.Never)]
        public new Size AutoScrollMinSize => base.AutoScrollMinSize;

        /// <summary>
        /// The data navigator does not use the background image so this event is hidden
        /// </summary>
        /// <exclude/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler? BackgroundImageChanged;

#pragma warning restore 0067
        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when the <see cref="AddDeleteButtonsVisible"/> property changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the add/delete button visibility changes")]
        public event EventHandler? AddDeleteButtonsVisibleChanged;

        /// <summary>
        /// This raises the <see cref="AddDeleteButtonsVisibleChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnAddDeleteButtonsVisibleChanged(EventArgs e)
        {
            AddDeleteButtonsVisibleChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="RepeatWait"/> value changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the auto-repeat initial wait interval changes")]
        public event EventHandler? RepeatWaitChanged;

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
        public event EventHandler? RepeatIntervalChanged;

        /// <summary>
        /// This raises the <see cref="RepeatIntervalChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnRepeatIntervalChanged(EventArgs e)
        {
            RepeatIntervalChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised just prior to adding an item to the data source
        /// </summary>
        [Category("Data"), Description("Occurs just prior to adding an item to the data source")]
        public event EventHandler<DataNavigatorCancelEventArgs>? AddingRow;

        /// <summary>
        /// This raises the <see cref="AddingRow"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnAddingRow(DataNavigatorCancelEventArgs e)
        {
            AddingRow?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised after adding an item to the data source.
        /// </summary>
        [Category("Data"), Description("Occurs after adding an item to the data source")]
        public event EventHandler<DataNavigatorEventArgs>? AddedRow;

        /// <summary>
        /// This raises the <see cref="AddedRow"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnAddedRow(DataNavigatorEventArgs e)
        {
            AddedRow?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised just prior to deleting an item from the data source
        /// </summary>
        [Category("Data"), Description("Occurs just prior to deleting an item from the data source")]
        public event EventHandler<DataNavigatorCancelEventArgs>? DeletingRow;

        /// <summary>
        /// This raises the <see cref="DeletingRow"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDeletingRow(DataNavigatorCancelEventArgs e)
        {
            DeletingRow?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised after deleting an item from the data source
        /// </summary>
        /// <remarks>If there are no more rows after the deletion, the <see cref="NoRows"/> event is also raised</remarks>
        [Category("Data"), Description("Occurs after deleting an item from the data source")]
        public event EventHandler<DataNavigatorEventArgs>? DeletedRow;

        /// <summary>
        /// This raises the <see cref="DeletedRow"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDeletedRow(DataNavigatorEventArgs e)
        {
            DeletedRow?.Invoke(this, e);

            if(listManager != null && listManager.Count == 0)
                OnNoRows(EventArgs.Empty);
        }

        /// <summary>
        /// This event is raised just prior to canceling edits to a row via the Escape key
        /// </summary>
        [Category("Data"), Description("Occurs just prior to canceling edits via the Escape key")]
        public event EventHandler<DataNavigatorCancelEventArgs>? CancelingEdits;

        /// <summary>
        /// This raises the <see cref="CancelingEdits"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnCancelingEdits(DataNavigatorCancelEventArgs e)
        {
            CancelingEdits?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised after canceling edits via the Escape key
        /// </summary>
        [Category("Data"), Description("Occurs after canceling edits via the Escape key")]
        public event EventHandler<DataNavigatorEventArgs>? CanceledEdits;

        /// <summary>
        /// This raises the <see cref="CanceledEdits"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnCanceledEdits(DataNavigatorEventArgs e)
        {
            CanceledEdits?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when a row is made the current row
        /// </summary>
        [Category("Data"), Description("Occurs when a row is made the current row")]
        public event EventHandler<DataNavigatorEventArgs>? Current;

        /// <summary>
        /// This raises the <see cref="Current"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnCurrent(DataNavigatorEventArgs e)
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
        public event EventHandler? NoRows;

        /// <summary>
        /// This raises the <see cref="NoRows"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnNoRows(EventArgs e)
        {
            NoRows?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="DataSource"/> is changed
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the control's data source changes")]
        public event EventHandler? DataSourceChanged;

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
        public event EventHandler<ChangePolicyEventArgs>? ChangePolicyModified;

        /// <summary>
        /// This raises the <see cref="ChangePolicyModified"/> event for the control
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnChangePolicyModified(ChangePolicyEventArgs e)
        {
            // Enable or disable Add and Delete based on the policy
            btnAdd.Enabled = (changePolicy.AllowAdditions && listManager != null);
            btnDelete.Enabled = (changePolicy.AllowDeletes && listManager != null && listManager.Count > 0);

            ChangePolicyModified?.Invoke(this, e);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public DataNavigator()
		{
            this.BorderStyle = BorderStyle.None;
            showAddDel = allowAdditions = allowEdits = allowDeletes = true;
            dataMember = String.Empty;
            changePolicy = new ChangePolicy(this);
            shortcutAdd = Shortcut.CtrlShiftA;
            shortcutDel = Shortcut.CtrlShiftD;
            shortcutRowNum = Shortcut.F5;
            repeatWait = 500;
            repeatInterval = 50;

			// This call is required by the Windows.Forms Form Designer
			InitializeComponent();
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
            this.components = new Container();
            System.Resources.ResourceManager resources = new(typeof(DataNavigator));
            this.btnFirst = new Button();
            this.ilButtons = new ImageList(this.components);
            this.btnPrev = new Button();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.btnAdd = new Button();
            this.txtRowNum = new NumericTextBox();
            this.lblRowCount = new Label();
            this.btnDelete = new Button();
            this.tmrRepeat = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnFirst
            // 
            this.btnFirst.Enabled = false;
            this.btnFirst.ImageIndex = 0;
            this.btnFirst.ImageList = this.ilButtons;
            this.btnFirst.Location = new Point(1, 1);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new Size(22, 22);
            this.btnFirst.TabIndex = 0;
            this.btnFirst.Click += new EventHandler(this.btnFirst_Click);
            // 
            // ilButtons
            // 
            this.ilButtons.ImageSize = new Size(15, 11);
            this.ilButtons.ImageStream = ((ImageListStreamer)(resources.GetObject("ilButtons.ImageStream")!));
            this.ilButtons.TransparentColor = Color.Lime;
            // 
            // btnPrev
            // 
            this.btnPrev.Enabled = false;
            this.btnPrev.ImageIndex = 1;
            this.btnPrev.ImageList = this.ilButtons;
            this.btnPrev.Location = new Point(23, 1);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new Size(22, 22);
            this.btnPrev.TabIndex = 1;
            this.btnPrev.Click += new EventHandler(this.btnPrev_Click);
            this.btnPrev.MouseUp += new MouseEventHandler(this.btnPrev_MouseUp);
            this.btnPrev.MouseDown += new MouseEventHandler(this.btnPrev_MouseDown);
            //
            // btnNext
            //
            this.btnNext.Enabled = false;
            this.btnNext.ImageIndex = 2;
            this.btnNext.ImageList = this.ilButtons;
            this.btnNext.Location = new Point(108, 1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(22, 22);
            this.btnNext.TabIndex = 3;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnNext.MouseUp += new MouseEventHandler(this.btnNext_MouseUp);
            this.btnNext.MouseDown += new MouseEventHandler(this.btnNext_MouseDown);
            // 
            // btnLast
            // 
            this.btnLast.Enabled = false;
            this.btnLast.ImageIndex = 3;
            this.btnLast.ImageList = this.ilButtons;
            this.btnLast.Location = new Point(130, 1);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(22, 22);
            this.btnLast.TabIndex = 4;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.ImageIndex = 4;
            this.btnAdd.ImageList = this.ilButtons;
            this.btnAdd.Location = new Point(152, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(22, 22);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            // 
            // txtRowNum
            // 
            this.txtRowNum.Location = new Point(48, 1);
            this.txtRowNum.MaxLength = 7;
            this.txtRowNum.Name = "txtRowNum";
            this.txtRowNum.Size = new Size(57, 22);
            this.txtRowNum.TabIndex = 2;
            this.txtRowNum.Text = "0";
            this.txtRowNum.TextAlign = HorizontalAlignment.Right;
            this.txtRowNum.Leave += new EventHandler(this.txtRowNum_Leave);
            //
            // lblRowCount
            // 
            this.lblRowCount.Location = new Point(201, 1);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new Size(88, 22);
            this.lblRowCount.TabIndex = 7;
            this.lblRowCount.Text = "of 0";
            this.lblRowCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnDelete
            // 
            this.btnDelete.CausesValidation = false;
            this.btnDelete.Enabled = false;
            this.btnDelete.ImageIndex = 5;
            this.btnDelete.ImageList = this.ilButtons;
            this.btnDelete.Location = new Point(174, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(22, 22);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            // 
            // tmrRepeat
            // 
            this.tmrRepeat.Tick += new EventHandler(this.tmrRepeat_Tick);
            // 
            // DataNavigator
            // 
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblRowCount);
            this.Controls.Add(this.txtRowNum);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnFirst);
            this.Name = "DataNavigator";
            this.Size = new Size(296, 24);
            this.ResumeLayout(false);

        }
		#endregion

        #region Private designer methods
        //=====================================================================

        // Private designer methods.  These are used because the default values for these properties don't work
        // with the DefaultValue attribute.

        /// <summary>
        /// The designer uses this to determine whether or not to serialize changes to the AllowAdditions property
        /// </summary>
        /// <returns>True to serialize it, false if not</returns>
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
        /// <returns>True to serialize it, false if not</returns>
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
        /// <returns>True to serialize it, false if not</returns>
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
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// This is called when the data source's meta data changes in some way
        /// </summary>
        private void DataSource_MetaDataChanged(object? sender, EventArgs e)
        {
            if(!inSetListManager && !inAddRow && !inDelRow)
                SetListManager(dataSource, dataMember, true);
        }

        /// <summary>
        /// This is called when the data source is changed in some way
        /// </summary>
        private void DataSource_ListChanged(object? sender, ListChangedEventArgs e)
        {
            // Ignore calls in the following cases or it tends to get recursive in a hurry
            if(inSetListManager || inAddRow || inDelRow)
                return;

////            System.Diagnostics.Debug.WriteLine(this.Name + ": List Changed: " +
////                e.ListChangedType.ToString() + "  Old Idx: " + e.OldIndex.ToString() +
////                "  New Idx: " + e.NewIndex.ToString() +
////                "  List Count: " + listManager.Count);

            switch(e.ListChangedType)
            {
                case ListChangedType.Reset:
                    changePolicy.UpdatePolicy(allowAdditions, allowEdits, allowDeletes);
                    this.BindData();
                    break;

                case ListChangedType.ItemAdded:
                    this.BindData();
                    this.OnAddedRow(new DataNavigatorEventArgs(e.NewIndex));
                    btnDelete.Enabled = changePolicy.AllowDeletes;
                    break;

                case ListChangedType.ItemDeleted:
                    this.BindData();
                    this.OnDeletedRow(new DataNavigatorEventArgs(e.OldIndex));
                    btnDelete.Enabled = (changePolicy.AllowDeletes && listManager!.Count != 0);
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
        private void DataSource_ItemChanged(object? sender, ItemChangedEventArgs ea)
        {
            if(ea.Index == -1 && !inSetListManager && !inAddRow && !inDelRow)
            {
////                System.Diagnostics.Debug.WriteLine(this.Name +
////                    ": Item Changed: " + "Index: " + ea.Index.ToString());

                changePolicy.UpdatePolicy(allowAdditions, allowEdits,
                    allowDeletes);
                this.BindData();
            }
        }

        /// <summary>
        /// This is called when the position in the data source changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataSource_PositionChanged(object? sender, EventArgs e)
        {
            bool hasList = (listManager != null);
            int  curRow = (hasList) ? listManager!.Position : -1;

            // Ignore calls in the following cases.  It'll refresh when it's all done.
            if(inAddRow || inDelRow)
                return;

            if(curRow != currentRow)
            {
                if(curRow < 0)
                    curRow = 0;

                currentRow = curRow;
                txtRowNum.Text = (curRow + 1).ToString(CultureInfo.InvariantCulture);

                if(hasList)
                    OnCurrent(new DataNavigatorEventArgs(curRow));

                // Enable or disable buttons based on the row
                btnFirst.Enabled = btnPrev.Enabled = (curRow > 0);
                btnNext.Enabled = btnLast.Enabled = hasList && (curRow < listManager!.Count - 1);
            }
        }

        /// <summary>
        /// This handles the <c>KeyDown</c> event in the parent form and processes our shortcut keys
        /// </summary>
        private void Parent_KeyDown(object? sender, KeyEventArgs e)
        {
            // Don't do anything if we are nested inside another control and it doesn't contain the focus
            if(sender == this.ParentForm && (this.Parent == this.ParentForm || this.Parent!.ContainsFocus))
            {
                if(e.KeyData == Keys.Escape && listManager != null && listManager.Count > 0)
                {
                    // Cancel changes to the current row. Row may not be the same afterwards if canceling changes
                    // on the new row.
                    int row = currentRow;

                    DataNavigatorCancelEventArgs ce = new(row);
                    OnCancelingEdits(ce);

                    if(!ce.Cancel)
                    {
                        this.CancelChanges();

                        // If the current row changed, pass -1 to indicate that the old row went away
                        if(currentRow != row)
                            OnCanceledEdits(new DataNavigatorEventArgs(-1));
                        else
                            OnCanceledEdits(new DataNavigatorEventArgs(row));
                    }
                }
                else
                    if(e.KeyData == (Keys)shortcutRowNum)
                        txtRowNum.Focus();  // Set focus to row number text box
                    else
                        if(e.KeyData == (Keys)shortcutAdd && changePolicy.AllowAdditions && listManager != null)
                            this.MoveTo(RowPosition.NewRow);    // Add a row
                        else
                            if(e.KeyData == (Keys)shortcutDel && changePolicy.AllowDeletes &&
                              listManager != null && listManager.Count != 0)
                                this.DeleteRow(currentRow); // Delete current row
            }
        }

        /// <summary>
        /// Set the focus to the specified row when the row text box loses focus
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtRowNum_Leave(object? sender, EventArgs e)
        {
            bool updateText;
            int curRow = currentRow;

            if(listManager != null && listManager.Count != 0)
            {
                if(txtRowNum.Text.Length == 0 || !this.IsValid)
                    updateText = true;
                else
                {
                    curRow = Convert.ToInt32(txtRowNum.Text, CultureInfo.InvariantCulture) - 1;

                    if(curRow < 0)
                    {
                        curRow = 0;
                        updateText = true;
                    }
                    else
                        if(curRow >= listManager.Count)
                        {
                            curRow = listManager.Count - 1;
                            updateText = true;
                        }
                        else
                            updateText = false;
                }

                if(currentRow != curRow)
                {
                    updateText = false;
                    listManager.Position = curRow;
                }

                if(updateText)
                    txtRowNum.Text = (currentRow + 1).ToString(CultureInfo.InvariantCulture);
            }
            else
                txtRowNum.Text = "0";
        }

        /// <summary>
        /// Go to the first row unless validation failed somewhere
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnFirst_Click(object? sender, EventArgs e)
        {
            if(this.ContainsFocus && this.IsValid)
                listManager!.Position = 0;
        }

        /// <summary>
        /// Go to the previous row unless validation failed somewhere
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnPrev_Click(object? sender, EventArgs e)
        {
            if(this.ContainsFocus && this.IsValid && listManager!.Position > 0)
                listManager.Position--;
        }

        /// <summary>
        /// Set the timer for auto-repeat unless validation fails and the button doesn't have the focus
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnPrev_MouseDown(object? sender, MouseEventArgs e)
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
        private void btnPrev_MouseUp(object? sender, MouseEventArgs e)
        {
            tmrRepeat.Enabled = false;
        }

        /// <summary>
        /// Go to the next row unless validation failed somewhere
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnNext_Click(object? sender, EventArgs e)
        {
            if(this.ContainsFocus && this.IsValid && listManager!.Position < listManager.Count - 1)
                listManager.Position++;
        }

        /// <summary>
        /// Set the timer for auto-repeat unless validation fails and the button doesn't have the focus
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnNext_MouseDown(object? sender, MouseEventArgs e)
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
        private void btnNext_MouseUp(object? sender, MouseEventArgs e)
        {
            tmrRepeat.Enabled = false;
        }

        /// <summary>
        /// Go to the last row unless validation failed somewhere
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnLast_Click(object? sender, EventArgs e)
        {
            if(this.ContainsFocus && this.IsValid)
                listManager!.Position = listManager.Count - 1;
        }

        /// <summary>
        /// This raises the <see cref="AddingRow"/> event and, if not canceled, adds a row to the data source,
        /// makes it the current row, and raises the <see cref="AddedRow"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnAdd_Click(object? sender, EventArgs e)
        {
            if(this.ContainsFocus && this.IsValid)
                this.AddRowInternal();
        }

        /// <summary>
        /// This raises the <see cref="DeletingRow"/> event and, if not canceled, deletes the current row from
        /// the data source, and raises the <see cref="DeletedRow"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if(this.ContainsFocus && this.IsValid)
                this.DeleteRow(currentRow);
        }

        /// <summary>
        /// Fire the appropriate event for next/previous button auto-repeat
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event parameters</param>
        private void tmrRepeat_Tick(object? sender, EventArgs e)
        {
            // Only repeat while the cursor is in the button
            Point p = repeatButton.PointToClient(Cursor.Position);

            if(repeatButton.ClientRectangle.Contains(p))
            {
                // Shorten the interval after the first tick
                tmrRepeat.Interval = repeatInterval;

                if(repeatButton == btnPrev)
                    btnPrev_Click(sender, e);
                else
                    btnNext_Click(sender, e);

                // If the start or end of the set is reached, turn the timer off
                if(!repeatButton.Enabled)
                    tmrRepeat.Enabled = false;
            }
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is called to ensure that the specified data member is valid for the given data source.  If not
        /// valid, it is cleared.
        /// </summary>
        /// <param name="ds">The data source to check</param>
        /// <param name="member">The data member to check</param>
        private void EnforceValidDataMember(object? ds, string? member)
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
        /// <param name="newDataSource">The new data source</param>
        /// <param name="newDataMember">The new data member in the data source</param>
        /// <param name="force">True to force the information to be updated, false to only update the info if
        /// something really changed.</param>
        /// <exception cref="ArgumentException">This is thrown if the data member cannot be found in the data
        /// source.</exception>
        private void SetListManager(object? newDataSource, string? newDataMember, bool force)
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

                        var bl = listManager.List as IBindingList;

                        if(bl != null)
                            bl.ListChanged -= DataSource_ListChanged;

                        if(bl == null || dataMember.IndexOf('.') != -1)
                            listManager.ItemChanged -= DataSource_ItemChanged;
                    }

                    if(newDataSource != null && this.Parent != null && this.BindingContext != null &&
                      newDataSource != Convert.DBNull)
                    {
                        listManager = (CurrencyManager)this.BindingContext[newDataSource, newDataMember];
                    }
                    else
                        listManager = null;

                    dataSource = newDataSource;
                    dataMember = newDataMember ?? String.Empty;

                    if(this.listManager != null)
                    {
                        listManager.MetaDataChanged += DataSource_MetaDataChanged;
                        listManager.PositionChanged += DataSource_PositionChanged;

                        var bl = listManager.List as IBindingList;

                        // ListChanged happens less frequently and is more efficient with regard to resets
                        if(bl != null)
                            bl.ListChanged += DataSource_ListChanged;

                        // However, if the data member refers to a relationship, we must hook up the ItemChanged
                        // event so that this list is refreshed when the position changes in the related data
                        // source.
                        if(bl == null || dataMember.IndexOf('.') != -1)
                            listManager.ItemChanged += DataSource_ItemChanged;
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
        /// This is used to update the current position when bound to a data source
        /// </summary>
		private void BindData()
		{
            lblRowCount.Text = LR.GetString("DLNavRowCount", (listManager != null) ? listManager.Count : 0);
            currentRow = -1;
            DataSource_PositionChanged(this, EventArgs.Empty);

            if(listManager != null && listManager.Count == 0)
                OnNoRows(EventArgs.Empty);
		}

        /// <summary>
        /// This is used to add a row to the data source
        /// </summary>
        /// <returns>True if the row was added, false if it was not due to validation failure or cancellation</returns>
        private bool AddRowInternal()
        {
            if(!changePolicy.AllowAdditions)
                throw new NotSupportedException(LR.GetString("ExAddNotAllowed"));

            if(!this.IsValid)
                return false;

            try
            {
                inAddRow = true;
                listManager!.EndCurrentEdit();

                // See if the user wants to allow the addition
                DataNavigatorCancelEventArgs ce = new(-1);

                OnAddingRow(ce);

                if(ce.Cancel)
                    return false;

                listManager.AddNew();
            }
            finally
            {
                inAddRow = false;
            }

            lblRowCount.Text = LR.GetString("DLNavRowCount", listManager.Count);
            currentRow = -1;
            DataSource_PositionChanged(this, EventArgs.Empty);
            btnDelete.Enabled = changePolicy.AllowDeletes;

            OnAddedRow(new DataNavigatorEventArgs(currentRow));
            return true;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources, false to just release
        /// unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
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
        /// This is overridden to hook up an event for the hot key processing
        /// </summary>
        /// <remarks>In order to find out when the hot keys are pressed, this control needs to handle the
        /// <see cref="Control.KeyDown"/> event in the parent form.  This is overridden to hook that event and
        /// turn on <see cref="Form.KeyPreview"/> in the parent form the first time this control is created.  It
        /// cannot use <c>OnParentChanged</c> as the immediate parent may not be the form and it may not have a
        /// parent itself when this control is added to it.  This works around that problem.</remarks>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            var parentForm = this.ParentForm;

            if(parentForm != null)
            {
                parentForm.KeyDown += new KeyEventHandler(Parent_KeyDown);
                parentForm.KeyPreview = true;
            }
        }

        /// <summary>
        /// This is used to reposition the controls when the control attributes change
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnLayout(LayoutEventArgs e)
        {
            int borderWidth;

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

            this.Height = btnFirst.Height + (borderWidth * 2);
            btnFirst.Top = btnPrev.Top = txtRowNum.Top = btnNext.Top = btnLast.Top = btnAdd.Top = btnDelete.Top =
                lblRowCount.Top = 0;

            base.OnLayout(e);
        }

        /// <summary>
        /// This is overridden to suppress the normal validation method
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnValidating(CancelEventArgs e)
        {
        }

        /// <summary>
        /// This is overridden to suppress the normal validation method
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnValidated(EventArgs e)
        {
        }

        /// <summary>
        /// Calling this method is the equivalent of setting the <see cref="DataSource"/> and <see cref="DataMember"/>
        /// properties individually.
        /// </summary>
        /// <param name="dataSource">The data source to use</param>
        /// <param name="member">The data member in the data source to use, if any</param>
        public void SetDataBinding(object dataSource, string? member)
        {
            this.SetListManager(dataSource, member, false);
        }

        /// <summary>
        /// This method is used to move the focus to the specified row in the data source
        /// </summary>
        /// <param name="newRow">The row to which the focus is moved</param>
        /// <returns>True if the specified row now has the focus or false if the focus could not be set due to
        /// validation failure on the current row.</returns>
        /// <remarks>Before moving to the specified row, the <see cref="IsValid"/> property is checked to ensure
        /// that it is safe to move.  If it returns false, the focus will stay on the current row.</remarks>
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

                listManager.Position = newRow;
            }

            return true;
        }

        /// <summary>
        /// This method is used to move the focus to the specified fixed row position in the data source
        /// </summary>
        /// <param name="position">The position to which the focus is moved</param>
        /// <returns>True if the specified position now has the focus or false if the focus could not be set due
        /// to validation failure on the current row.</returns>
        /// <remarks>Before moving to the specified position, the <see cref="IsValid"/> property is checked to
        /// ensure that it is safe to move.  If it returns false, the focus will stay on the current row.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if there is no data source or there are
        /// no rows.</exception>
        /// <exception cref="NotSupportedException">This is thrown if additions // are not currently allowed as
        /// defined by the current <see cref="AllowAdditions"/> property setting and an attempt is made to move
        /// the <c>NewRow</c> position.</exception>
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
                    return this.AddRowInternal();
            }

            // This will throw an exception if the new row number isn't valid
            return this.MoveTo(newRow);
        }

        /// <summary>
        /// This can be used as an alternative to the <c>Delete</c> button to delete a row from the data source
        /// </summary>
        /// <param name="delRow">The row to delete</param>
        /// <returns>True if the row was deleted, false if not deleted due to cancellation</returns>
        /// <remarks>This is equivalent to clicking the <c>Delete</c> button but it allows you to put a button on
        /// the parent form or to handle the delete operation in some other fashion.  The <see cref="DeletingRow"/>
        /// event is fired prior to deleting the specified row.  If the <c>DeletingRow</c> event is not canceled,
        /// the specified row is deleted and the <see cref="DeletedRow"/> event is fired.</remarks>
        /// <exception cref="NotSupportedException">This is thrown if deletions are not currently allowed as
        /// defined by the current <see cref="AllowDeletes"/> property setting.</exception>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the specified row is outside the
        /// bounds of the list or if there is no data source.</exception>
        public bool DeleteRow(int delRow)
        {
            if(!changePolicy.AllowDeletes)
                throw new NotSupportedException(LR.GetString("ExDeleteNotAllowed"));

            if(delRow < 0 || listManager == null || delRow >= listManager.Count)
                throw new ArgumentOutOfRangeException(nameof(delRow), delRow, LR.GetString("ExInvalidRowNumber"));

            // Nested deletes aren't supported.
            if(inDelRow)
                return false;

            try
            {
                inDelRow = true;

                // See if the user will allow the delete to occur
                DataNavigatorCancelEventArgs ce = new(delRow);

                OnDeletingRow(ce);

                // Ignore the request if canceled
                if(ce.Cancel)
                    return false;

                // If deleting the current row, cancel any pending changes
                if(delRow == currentRow)
                    this.CancelChanges();

                // Delete the row from the data source if it is there
                if(delRow < listManager.Count)
                    listManager.RemoveAt(delRow);
            }
            finally
            {
                inDelRow = false;
            }

            lblRowCount.Text = LR.GetString("DLNavRowCount", listManager.Count);

            // Force the focus to the proper row
            currentRow = -1;
            DataSource_PositionChanged(this, EventArgs.Empty);

            OnDeletedRow(new DataNavigatorEventArgs(-1));
            btnDelete.Enabled = (changePolicy.AllowDeletes && listManager.Count > 0);
            return true;
        }

        /// <summary>
        /// This is used to manually commit pending changes to the current row in the data source
        /// </summary>
        /// <remarks><para>This will call the <c>EndCurrentEdit</c> method for the data source in the current
        /// binding context.</para>
        /// 
        /// <para>Due to the way data binding works in .NET, pending changes to the current row may not have been
        /// committed to the data source when you are ready to save changes to the underlying data source.  As
        /// such, you should always call the <c>CommitChanges</c> or <see cref="CancelChanges"/> method on the
        /// data navigator control prior to checking for or saving changes in its underlying data source.</para>
        /// </remarks>
        /// <example>
        /// Assume <c>dataSet</c> has been assigned to <c>dataNav</c> as its
        /// data source.
        /// <code language="cs">
        /// // Commit any pending edits in the data source
        /// dataNav.CommitChanges();
        ///
        /// // If changes were made to the data source, save them
        /// if(dataSet.HasChanges())
        ///     dataAdapter.Update(dataSet);
        /// </code>
        /// <code language="vbnet">
        /// ' Commit any pending edits in the data source
        /// dataNav.CommitChanges()
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
                listManager.EndCurrentEdit();
        }

        /// <summary>
        /// This is used to manually cancel pending changes to the current row in the data source
        /// </summary>
        /// <remarks><para>This will call the <c>CancelCurrentEdit</c> method for the data source in the current
        /// binding context.</para>
        /// 
        /// <para>Due to the way data binding works in .NET, pending changes to the current row may not have been
        /// committed to the data source when you are ready to save changes to the underlying data source.   As
        /// such, you should always call the <see cref="CommitChanges"/> or <c>CancelChanges</c> method on the
        /// data navigator control prior to checking for or saving changes in its underlying data source.</para>
        /// </remarks>
        public void CancelChanges()
        {
            if(listManager != null && listManager.Count != 0)
                listManager.CancelCurrentEdit();
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

            PropertyDescriptorCollection coll = listManager!.GetItemProperties();
            PropertyDescriptor prop = coll.Find(member, true) ??
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
        /// <param name="member">The member in the data source to search for the string value</param>
        /// <param name="key">The string for which to search</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set
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
        /// <param name="member">The member in the data source to search for the string value</param>
        /// <param name="key">The string for which to search</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched.  Set
        /// to -1 to search from the beginning of the list.</param>
        /// <returns>The zero-based index of the first item found.  Returns -1 if no match is found.</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the start index value is not within
        /// the bounds of the list and it is not -1 or if the specified member could not be found in the
        /// data source.</exception>
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
        /// <param name="member">The member in the data source to search for the string value</param>
        /// <param name="key">The string for which to search</param>
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

            if(listManager == null || listManager.Count == 0)
                return -1;

            if(startIndex < -1 || startIndex >= listManager.Count)
                throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, LR.GetString("ExInvalidItemIndex"));

            if(member == null || member.Length == 0)
                throw new ArgumentNullException(nameof(member), LR.GetString("ExNullFindParam"));

            if(key == null)
                throw new ArgumentNullException(nameof(key), LR.GetString("ExNullFindParam"));

            PropertyDescriptorCollection coll = listManager.GetItemProperties();
            PropertyDescriptor prop = coll.Find(member, true) ??
                throw new ArgumentOutOfRangeException(nameof(member), member, LR.GetString("ExInvalidMember"));

            length = key.Length;
            idx = startIndex;

            while(true)
            {
                idx++;
                string? propValue = prop.GetValue(listManager.List[idx])?.ToString();

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
