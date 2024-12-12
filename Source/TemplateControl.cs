//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : TemplateControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/11/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a template control used as the source for creating rows in the DataList control.
// Note that this control has a small initial size to prevent it from overlapping other rows when not yet
// initialized.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/20/2005  EFW  Created the code
// 12/09/2005  EFW  Various improvements, fixes, and modifications
//===============================================================================================================

using System.Data;

namespace EWSoftware.ListControls
{
	/// <summary>
	/// This is a template control used as the source for creating rows in the <see cref="DataList"/> control
	/// </summary>
    /// <remarks>Derive a <see cref="UserControl"/> from this class and add your controls to it for use as a
    /// template in the data list.  Override the <see cref="Bind"/> method to bind your controls to fields in
    /// the row source.  Note that this control has a small initial size to prevent it from overlapping other
    /// rows when not yet initialized.  The derived control's size will take effect once the row becomes visible
    /// and is initialized and bound.</remarks>
	[ToolboxItem(false)]
	public class TemplateControl : UserControl
	{
        #region Private data members
        //=====================================================================

        private object? rowSource;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to set or get the parent DataList internally
        /// </summary>
        internal DataList TemplateParentInternal { get; set; } = null!;

        /// <summary>
        /// This is used to get or set the bound state flag
        /// </summary>
        internal bool HasBeenBound { get; set; }

        /// <summary>
        /// This is used to get or set the initialized state flag
        /// </summary>
        internal bool HasBeenInitialized { get; set; }

        /// <summary>
        /// This is used to get or set the new row flag
        /// </summary>
        internal bool IsNewRowInternal { get; set; }

        /// <summary>
        /// This read-only property can be used to get the template item's parent (the <see cref="DataList"/> in
        /// which it is contained).
        /// </summary>
        [Browsable(false), Description("Get a reference to the item's parent DataList control")]
        public DataList TemplateParent => this.TemplateParentInternal;

        /// <summary>
        /// This read-only property can be used to get the row source to which this control is bound.  The row
        /// source object is editable.
        /// </summary>
        [Browsable(false), Description("The row source to which the item is bound")]
        public object? RowSource => rowSource;

        /// <summary>
        /// This read-only property can be used to determine whether or not the template has been initialized
        /// </summary>
        [Browsable(false), Description("Check to see if the item has been initialized")]
        public bool IsInitialized => this.HasBeenInitialized;

        /// <summary>
        /// This read-only property can be used to determine whether or not the template has been data bound
        /// </summary>
        [Browsable(false), Description("Check to see if the item has been data bound")]
        public bool IsDataBound => this.HasBeenBound;

        /// <summary>
        /// This read-only property can be used to determine whether or not the template is bound to the new row
        /// item in the data list.
        /// </summary>
        /// <remarks>The new row template is bound to a temporary row in the data source.  If no changes are
        /// made, the temporary row is removed when the template loses the focus.  The template ceases to be
        /// the new row when a change is made to any of its bound fields.
        /// </remarks>
        [Browsable(false), Description("Check to see if the item is the new row placeholder")]
        public bool IsNewRow => this.IsNewRowInternal;

        /// <summary>
        /// This read-only property can be overridden to allow querying of a row to see if it is valid
        /// </summary>
        /// <value>Returns true if valid, false if not.  By default, it invokes the control's
        /// <see cref="Control.OnValidating"/> and <see cref="Control.Validated"/> events.</value>
        /// <remarks>This is useful in situations where the normal validating events are fired after certain
        /// other events (i.e. tree view and data grid item selection events).  This property is called on the
        /// current row by <see cref="DataList.IsValid">DataList.IsValid</see>.</remarks>
        [Browsable(false), Description("Check to see if the item is valid")]
        public virtual bool IsValid
        {
            get
            {
                CancelEventArgs e = new();
                this.OnValidating(e);

                if(!e.Cancel)
                    this.OnValidated(EventArgs.Empty);

                return !e.Cancel;
            }
        }

        /// <summary>
        /// This read-only property can be overridden to let a derived template allow or deny a request to delete
        /// a row.
        /// </summary>
        /// <value>Returns true to allow the delete, false to prevent it.  If not overridden, it always returns
        /// true.</value>
        /// <remarks><para>The row being deleted is always given the first chance to deny the deletion.  If the
        /// row allows it, the data list raises its <see cref="DataList.DeletingRow"/> event to let the
        /// containing form have a chance to permit or deny the deletion.</para>
        /// 
        /// <para>There are advantages to putting delete confirmations within the template. It has direct access
        /// to its controls so it is easier to perform any necessary validation prior to allowing the deletion.
        /// If the template is used in many forms, it also saves you from having to add an event handler for the
        /// data list's <c>DeletingRow</c> event in each one too.</para></remarks>
        [Browsable(false), Description("Check to see if the item can be deleted")]
        public virtual bool CanDelete => true;

        /// <summary>
        /// This read-only property can be used to see if the row source has been modified
        /// </summary>
        /// <value>The <see cref="CommitChanges"/> method is called first to commit any pending changes to the
        /// row source.  The template can detect changes only if the row source is a <c>DataRowView</c> or
        /// a <c>DataRow</c>.  In those cases, it returns true if the row source has been modified or false if it
        /// has not.  For all other row source types, it will always returns false.  You may override this
        /// property in order to extend the types that it knows about and detect changes in them.</value>
        [Browsable(false), Description("Check to see if the row source has changes")]
        public virtual bool HasChanges
        {
            get
            {
                if(rowSource != null && !this.IsNewRowInternal)
                {
                    // Commit any pending changes
                    this.CommitChanges();

                    DataRow? r;

                    if(rowSource is DataRowView drv)
                        r = drv.Row;
                    else
                        r = rowSource as DataRow;

                    // If still null, the row source is not of a type we can use to determine modifications
                    if(r != null)
                        return (r.RowState != DataRowState.Unchanged);
                }

                return false;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateControl()
        {
            // Each template needs its own binding context so that controls like combo boxes can be set
            // independently of each other on different rows.
            this.BindingContext = new BindingContext();

            this.Size = new Size(8, 8);
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources, false to just release
        /// unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
                this.SetRowSourceInternal(null);

            base.Dispose(disposing);
        }

        /// <summary>
        /// Handle mnemonics passed to us from the parent data list
        /// </summary>
        /// <param name="charCode">The character code to process as a mnemonic</param>
        internal bool HandleMnemonic(char charCode)
        {
            return this.ProcessMnemonic(charCode);
        }

        /// <summary>
        /// This is used to connect and disconnect the property changed event handlers
        /// </summary>
        /// <param name="connect">True to connect, false to disconnect</param>
        internal void WirePropChangedEvents(bool connect)
        {
            PropertyDescriptor? propInfo = null;
            FieldInfo? fi;

            if(rowSource != null)
            {
                BindingsCollection bc = this.BindingContext![rowSource].Bindings;

                // The bindings don't expose an event or anything else that notifies us when a bound field
                // changes.  As such, we have to hack into the mechanism using Reflection.
                foreach(Binding b in bc)
                {
                    // The backing field differs in the .NET Framework
#if NET40
                    fi = b.GetType().GetField("propInfo", BindingFlags.NonPublic | BindingFlags.Instance);
#else
                    fi = b.GetType().GetField("_propInfo", BindingFlags.NonPublic | BindingFlags.Instance);
#endif
                    if(fi != null)
                        propInfo = (PropertyDescriptor?)fi.GetValue(b);
                    else
                    {
                        // See above.  If it stops here, there's a problem.
                        Debugger.Break();
                    }

                    if(propInfo != null)
                    {
                        if(connect)
                            propInfo.AddValueChanged(b.Control, Target_PropertyChanged);
                        else
                            propInfo.RemoveValueChanged(b.Control, Target_PropertyChanged);
                    }
                }
            }
        }

        /// <summary>
        /// This is called when a bound control on the new row changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        /// <remarks>When a bound control on the new row changes, we can turn off the <see cref="IsNewRowInternal"/>
        /// flag so that the row will be committed when it loses the focus.</remarks>
        private void Target_PropertyChanged(object? sender, EventArgs e)
        {
            // Occasionally, this gets called again after we've already seen it and added the row.  Ignore those
            // calls.
            if(!this.IsNewRowInternal)
                return;

            // See if the user wants to allow the addition
            DataListCancelEventArgs ce = new(-1, null);
            this.TemplateParent.OnAddingRow(ce);

            if(ce.Cancel)
                this.CancelChanges();
            else
            {
                this.IsNewRowInternal = false;
                this.WirePropChangedEvents(false);
                this.TemplateParent.AddNewRowTemplate();

                // Tell the user the new row has been added
                this.TemplateParent.OnAddedRow(new DataListEventArgs(this.TemplateParent.CurrentRow, this));
            }
        }

        /// <summary>
        /// This is called by the parent during construction to set the item's row source
        /// </summary>
        /// <param name="rowSrc">The row to which this item is bound</param>
        /// <remarks>To save time, the item is not physically bound to the row source until it is scrolled into
        /// view.</remarks>
        internal void SetRowSourceInternal(object? rowSrc)
        {
            rowSource = rowSrc;

            // Existing rows are physically bound when they scroll into view
            if(!this.IsNewRowInternal || rowSrc == null)
            {
                this.HasBeenBound = false;

                // Clear all bindings in all controls in the template.  If not, they appear to hang around after
                // the template is disposed and cause problems in certain circumstances.
                if(rowSrc == null)
                    foreach(Control c in this.Controls)
                        c.DataBindings.Clear();
            }
            else
            {
                // Bind the new row now and hook up the events to find out when something really changes so that
                // we can commit the row.
                this.Bind();
                this.HasBeenBound = true;

                this.WirePropChangedEvents(true);

                this.TemplateParent.OnItemDataBound(new DataListEventArgs(
                    this.TemplateParent.ListManager!.Count - 1, this));
            }
        }

        /// <summary>
        /// This is overridden to keep mnemonic processing within the template so that the focus doesn't go to
        /// another row if past the control associated with the mnemonic.
        /// </summary>
        /// <param name="charCode">The dialog character to process</param>
        /// <returns>True if handled, false if not</returns>
        protected override bool ProcessDialogChar(char charCode)
        {
            if(charCode != ' ' && this.ProcessMnemonic(charCode))
                return true;

            return base.ProcessDialogChar(charCode);
        }

        /// <summary>
        /// This is overridden to suppress scaling.  If not done, it tends to do odd things with the row template
        /// size if bound before the containing form has had a chance to scale itself.
        /// </summary>
        /// <param name="dx">The ratio by which to scale the control horizontally</param>
        /// <param name="dy">The ratio by which to scale the control vertically</param>
        protected override void ScaleCore(float dx, float dy)
        {
        }

        /// <summary>
        /// This is overridden to suppress validation until initialized and bound
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnValidating(CancelEventArgs e)
        {
            if(this.HasBeenInitialized && this.HasBeenBound && !this.IsNewRowInternal)
                base.OnValidating(e);
        }

        /// <summary>
        /// This is overridden to suppress validation until initialized and bound
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnValidated(EventArgs e)
        {
            if(this.HasBeenInitialized && this.HasBeenBound && !this.IsNewRowInternal)
                base.OnValidated(e);
        }

        /// <summary>
        /// This is overridden to focus the row when clicked
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(!this.Focused)
                this.Focus();

            base.OnMouseDown(e);
        }

        /// <summary>
        /// This can be overridden to create and/or initialize controls in the template
        /// </summary>
        /// <remarks><para>This will not be called until the template is scrolled into view.  This saves time and
        /// resources during the initial startup phase of a form that contains a data list control.</para>
        /// 
        /// <para>Overriding this method is optional.  However, deferring initialization until it is required
        /// will save time and resources.  The base version does nothing.</para>
        /// 
        /// <para>To be used, you should defer initialization in the constructor and call the
        /// <c>InitializeComponent</c> method in this override.</para>
        /// </remarks>
        /// <example>
        /// <code language="cs">
        /// // Template control constructor
        /// public RowTemplate()
        /// {
        ///     // This call is required by the Windows.Forms Form Designer.
        ///     // At runtime, actual initialization is deferred until needed.
        ///     if(this.DesignMode)
        ///         InitializeComponent();
        /// }
        ///
        /// // Designer-generated initialization code
        /// private void InitializeComponent()
        /// {
        ///     // ... Designer code goes here
        /// }
        ///
        /// // Deferred initialization
        /// protected override void InitializeTemplate()
        /// {
        ///     // Create the contained controls
        ///     this.InitializeComponent();
        ///
        ///     // Use the shared data source for the combo box
        ///     cboState.DisplayMember = cboState.ValueMember = "State";
        ///     cboState.DataSource = dvStates;
        ///
        ///     // Update control states based on the parent's change policy.
        ///     // This can be omitted if you do not need it.
        ///     this.ChangePolicyModified();
        /// }
        /// </code>
        /// <code language="vbnet">
        /// ' Template control constructor
        /// Public Sub New()
        ///     ' This call is required by the Windows.Forms Form Designer.
        ///     ' At runtime, actual initialization is deferred until needed.
        ///     If Me.DesignMode = True Then
        ///         InitializeComponent()
        ///     End If
        /// End Sub
        ///
        /// ' Designer-generated initialization code
        /// Private Sub InitializeComponent()
        ///     ' ... Designer code goes here
        /// End Sub
        ///
        /// ' Deferred initialization
        /// Protected Overrides Sub InitializeTemplate()
        ///     ' Create the contained controls
        ///     Me.InitializeComponent()
        ///
        ///     ' Use the shared data source for the combo box
        ///     cboState.DisplayMember = "State"
        ///     cboState.ValueMember = "State"
        ///     cboState.DataSource = dvStates
        ///
        ///     ' Update control states based on the parent's change policy.
        ///     ' This can be omitted if you do not need it.
        ///     Me.ChangePolicyModified()
        /// End Sub
        /// </code>
        /// </example>
        protected internal virtual void InitializeTemplate()
        {
        }

        /// <summary>
        /// This is used to bind controls to the row source.  Derived controls must override this to provide data
        /// binding support even if they contain no bound controls.
        /// </summary>
        /// <remarks>This will not be called until the template is scrolled into view.  This saves time and
        /// resources during the initial startup phase of a form that contains a data list control.  Templates
        /// are re-bound under many conditions.  If adding bindings to controls manually, be sure to clear the
        /// <c>DataBindings</c> collection on all bound controls first.  A better alternative is to use the
        /// overloaded <see cref="AddBinding(Control, String, String)"/> method as it will take care of that for
        /// you.</remarks>
        /// <exception cref="NotImplementedException">This is thrown if the method is not overridden and the base
        /// implementation is called.</exception>
        /// <example>
        /// <code language="cs">
        /// protected override void Bind()
        /// {
        ///     this.AddBinding(txtFName, "Text", "FirstName");
        ///     this.AddBinding(txtLName, "Text", "LastName");
        ///     this.AddBinding(txtAddress, "Text", "Address");
        ///     this.AddBinding(txtCity, "Text", "City");
        ///     this.AddBinding(cboState, "SelectedValue", "State");
        ///     this.AddBinding(txtZip, "Text", "Zip");
        ///     this.AddBinding(udcSumValue, "Text", "SumValue");
        /// }
        /// </code>
        /// <code language="vbnet">
        /// Protected Overrides Sub Bind()
        ///     Me.AddBinding(txtFName, "Text", "FirstName")
        ///     Me.AddBinding(txtLName, "Text", "LastName")
        ///     Me.AddBinding(txtAddress, "Text", "Address")
        ///     Me.AddBinding(txtCity, "Text", "City")
        ///     Me.AddBinding(cboState, "SelectedValue", "State")
        ///     Me.AddBinding(txtZip, "Text", "Zip")
        ///     Me.AddBinding(udcSumValue, "Text", "SumValue")
        /// End Sub
        /// </code>
        /// </example>
        protected internal virtual void Bind()
        {
            throw new NotImplementedException(LR.GetString("TCBindNotOverridden", this.GetType().Name));
        }

        /// <summary>
        /// This can be overridden to handle modifications in the change policy in the parent <see cref="DataList"/>
        /// control.
        /// </summary>
        /// <remarks>The base class implementation does nothing.  You can override this in derived template
        /// controls so that they can handle modifications in the change policy such as disabling editable
        /// controls when edits are disabled, etc.  Overriding this method saves you from hooking up an event
        /// handler in the template.  This method is only called on controls that have been initialized and bound
        /// when the change policy is modified in the parent <c>DataList</c> control.</remarks>
        /// <example>
        /// <code language="cs">
        /// // Enable or disable the controls based on the edit policy.
        /// protected override void ChangePolicyModified()
        /// {
        ///     if(this.TemplateParent.AllowEdits != txtFName.Enabled &amp;&amp; !this.IsNewRow)
        ///        txtFName.Enabled = txtLName.Enabled = txtAddress.Enabled =
        ///          txtCity.Enabled = cboState.Enabled = txtZip.Enabled =
        ///            this.TemplateParent.AllowEdits;
        /// }
        /// </code>
        /// <code language="vbnet">
        /// ' Enable or disable the controls based on the edit policy.
        /// Protected Overrides Sub ChangePolicyModified()
        ///     Dim allowEdits As Boolean = Me.TemplateParent.AllowEdits
        ///
        ///     If allowEdits &lt;&gt; txtFName.Enabled And Me.IsNewRow = False Then
        ///         txtFName.Enabled = allowEdits
        ///         txtLName.Enabled = allowEdits
        ///         txtAddress.Enabled = allowEdits
        ///         txtCity.Enabled = allowEdits
        ///         cboState.Enabled = allowEdits
        ///         txtZip.Enabled = allowEdits
        ///     End If
        /// End Sub
        /// </code>
        /// </example>
        protected internal virtual void ChangePolicyModified()
        {
        }

        /// <summary>
        /// This is used to manually commit pending changes to the template's row source
        /// </summary>
        /// <remarks>This can be overridden to provide additional processing or perform other tasks when manually
        /// committing changes to a row.  By default, it simply calls the <see cref="DataList.CommitChanges"/>
        /// method on its parent <c>DataList</c> control.</remarks>
        public virtual void CommitChanges()
        {
            this.TemplateParent.CommitChanges();
        }

        /// <summary>
        /// This is used to manually cancel pending changes to the template's row source
        /// </summary>
        /// <remarks>This can be overridden to provide additional processing or perform other tasks when manually
        /// canceling changes to a row.  By default, it simply calls the <see cref="DataList.CancelChanges"/>
        /// method on its parent <c>DataList</c> control.</remarks>
        public virtual void CancelChanges()
        {
            this.TemplateParent.CancelChanges();
        }

        /// <summary>
        /// This can be used to delete the row currently bound to the template
        /// </summary>
        public void DeleteRow()
        {
            int row = this.Parent!.Controls.IndexOf(this);

            if(row != -1)
            {
                if(this.TemplateParent.SeparatorsVisible)
                    row /= 2;

                this.TemplateParent.DeleteRow(row);
            }
        }

        /// <summary>
        /// This method and its overloads can be used to simplify adding data bindings to controls contained in
        /// the template.
        /// </summary>
        /// <param name="control">The control to which to add the data binding</param>
        /// <param name="controlProperty">The name of the control property to bind</param>
        /// <param name="dataMember">The property or list to which to bind</param>
        /// <returns>The newly created data binding</returns>
        /// <remarks>This version will clear all existing bindings and add the requested binding</remarks>
        /// <overloads>There are four overloads for this method</overloads>
        public Binding AddBinding(Control control, string controlProperty, string dataMember)
        {
            return this.AddBinding(control, controlProperty, dataMember, true, null, null);
        }

        /// <summary>
        /// This method and its overloads can be used to simplify adding data bindings to controls contained in
        /// the template.
        /// </summary>
        /// <param name="control">The control to which to add the data binding</param>
        /// <param name="controlProperty">The name of the control property to bind</param>
        /// <param name="dataMember">The property or list to which to bind</param>
        /// <param name="clearBindings">Specify true to clear all existing bindings before adding the new
        /// binding or false to keep all existing bindings.</param>
        /// <remarks>This version will only clear the existing data bindings if the <c>clearBindings</c>
        /// parameter is true.  This version should be used with it set to false if you bind multiple properties
        /// on the same control after the first one has been bound.</remarks>
        /// <returns>The newly created data binding</returns>
        public Binding AddBinding(Control control, string controlProperty, string dataMember, bool clearBindings)
        {
            return this.AddBinding(control, controlProperty, dataMember, clearBindings, null, null);
        }

        /// <summary>
        /// This method and its overloads can be used to simplify adding data bindings to controls contained in
        /// the template.
        /// </summary>
        /// <param name="control">The control to which to add the data binding</param>
        /// <param name="controlProperty">The name of the control property to bind</param>
        /// <param name="dataMember">The property or list to which to bind</param>
        /// <param name="clearBindings">Specify true to clear all existing bindings before adding the new binding
        /// or false to keep all existing bindings.</param>
        /// <param name="formatEvent">An event handler for the binding's <c>Format</c> event</param>
        /// <remarks>This version will only clear the existing data bindings if the <c>clearBindings</c>
        /// parameter is true.  It will also add the specified format event handler to the binding.</remarks>
        /// <returns>The newly created data binding</returns>
        public Binding AddBinding(Control control, string controlProperty, string dataMember, bool clearBindings,
          ConvertEventHandler? formatEvent)
        {
            return this.AddBinding(control, controlProperty, dataMember, clearBindings, formatEvent, null);
        }

        /// <summary>
        /// This method and its overloads can be used to simplify adding data bindings to controls contained in
        /// the template.
        /// </summary>
        /// <param name="control">The control to which to add the data binding</param>
        /// <param name="controlProperty">The name of the control property to bind</param>
        /// <param name="dataMember">The property or list to which to bind</param>
        /// <param name="clearBindings">Specify true to clear all existing bindings before adding the new binding
        /// or false to keep all existing bindings.</param>
        /// <param name="formatEvent">An event handler for the binding's <c>Format</c> event if wanted</param>
        /// <param name="parseEvent">An event handler for the binding's <c>Parse</c> event if wanted</param>
        /// <remarks>This version will only clear the existing data bindings if the <c>clearBindings</c>
        /// parameter is true.  It will also add the specified format and parse event handlers to the binding if
        /// they are specified.  Use null if you do not want a particular handler added.</remarks>
        /// <returns>The newly created data binding</returns>
        /// <exception cref="ArgumentNullException">This is thrown if the control parameter is null</exception>
        public Binding AddBinding(Control control, string controlProperty, string dataMember, bool clearBindings,
          ConvertEventHandler? formatEvent, ConvertEventHandler? parseEvent)
        {
            if(control == null)
                throw new ArgumentNullException(nameof(control), LR.GetString("ExNullParameter"));

            Binding b = new(controlProperty, rowSource, dataMember);

            if(formatEvent != null)
                b.Format += formatEvent;

            if(parseEvent != null)
                b.Parse += parseEvent;

            if(clearBindings)
                control.DataBindings.Clear();

            control.DataBindings.Add(b);
            return b;
        }
        #endregion
    }
}
