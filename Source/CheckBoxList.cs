//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : CheckBoxList.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This file contains a multi-selection checkbox list that supports data binding, layout options, and data
// source indexers.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/17/2005  EFW  Created the code
// 05/01/2006  EFW  Added support for UseMnemonic
// 01/30/2007  EFW  Updated checked item collections to use generics
// 05/05/2007  EFW  Added the properties BindingMembersBindingContext, BindingMembersDataSource and
//                  BindingMembers.
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This control is a multi-selection checkbox list that supports data binding, layout options, and data
    /// source indexers.
    /// </summary>
    [Description("A multi-selection checkbox list that supports data binding, layout options, and data " +
      "source indexers"), DefaultEvent("ItemCheckStateChanged")]
    public class CheckBoxList : BaseButtonList
    {
        #region Private data members
        //====================================================================

        private bool inSelectedIndex, threeState;

        // Checkbox data source and data binding members
        private BindingContext bindingContext;
        private object bindingDataSource;
        private StringCollection bindingMembers;

        #endregion

        #region Properties
        //====================================================================

        /// <summary>
        /// Gets or sets the index specifying the currently selected item.  Unlike the
        /// <see cref="RadioButtonList"/>, this only selects the item.  It will not change it to a checked state.
        /// </summary>
        /// <value><para>This is a zero-based index into the items collection.  A value of -1 indicates that
        /// there is no current selection.</para>
        /// 
        /// <para>Setting a new index value will raise the <see cref="BaseListControl.SelectedIndexChanged"/>
        /// event.</para>
        /// 
        /// <para>If <see cref="BaseListControl.EnforceDefaultSelection"/> is true, a <see cref="SelectedIndex"/>
        /// of -1 (no selection) is not allowed.  Instead, the index specified by the
        /// <see cref="BaseListControl.DefaultSelection"/> property is used instead.  If the default value is
        /// outside the range of the data source, the last item is selected.</para></value>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index is less than -1 or greater
        /// than the number of items in the collection.</exception>
        public override int SelectedIndex
        {
            get => base.SelectedIndex;
            set
            {
                CheckBox cb;
                int oldValue;

                if(base.SelectedIndex != value)
                {
                    if(inSelectedIndex)
                        return;

                    if(value < -1 || value >= this.Items.Count)
                        throw new ArgumentOutOfRangeException(nameof(value), value, LR.GetString("ExItemIndexOutOfRange"));

                    // If a default selection is being enforced, use it as long as it is valid
                    if(value == -1 && this.EnforceDefaultSelection)
                    {
                        if(this.DefaultSelection < this.Items.Count && (this.DataManager == null ||
                          this.DataManager.Count != 0))
                        {
                            value = this.DefaultSelection;
                        }
                        else
                        {
                            if(this.DataManager != null)
                                value = this.DataManager.Count - 1;
                            else
                                value = this.Items.Count - 1;
                        }
                    }

                    inSelectedIndex = true;
                    oldValue = base.SelectedIndex;

                    base.SelectedIndex = value;

                    this.OnSelectedItemChanged(EventArgs.Empty);
                    this.OnSelectedIndexChanged(EventArgs.Empty);

                    // Give focus to the selected checkbox
                    if(!this.IsInitializing)
                    {
                        if(value != -1)
                        {
                            cb = (CheckBox)this.ButtonPanel.Controls[value];
                            this.ButtonPanel.ScrollControlIntoView(cb);

                            if(this.ContainsFocus && !cb.Focused)
                                cb.Focus();
                        }
                        else
                        {
                            if(oldValue != -1 && oldValue < this.Items.Count)
                            {
                                cb = (CheckBox)this.ButtonPanel.Controls[oldValue];

                                if(this.ContainsFocus && !cb.Focused)
                                    cb.Focus();
                            }
                        }
                    }

                    inSelectedIndex = false;
                }
            }
        }

        /// <summary>
        /// This is used to set or get the checkbox appearance
        /// </summary>
        /// <value>The default is to show them as normal checkboxes</value>
        public override Appearance Appearance
        {
            get => base.Appearance;
            set
            {
                foreach(Control c in this.ButtonPanel.Controls)
                    ((CheckBox)c).Appearance = value;

                base.Appearance = value;
            }
        }

        /// <summary>
        /// This is used to set or get the alignment on the checkbox checkmark
        /// </summary>
        /// <value>The default alignment is <c>MiddleLeft</c></value>
        public override ContentAlignment CheckAlign
        {
            get => base.CheckAlign;
            set
            {
                foreach(Control c in this.ButtonPanel.Controls)
                    ((CheckBox)c).CheckAlign = value;

                base.CheckAlign = value;
            }
        }

        /// <summary>
        /// This is used to set or get whether or not the checkboxes support three states rather than two
        /// </summary>
        /// <value>The default is to use only two states (checked and unchecked).  Setting this to true enables
        /// the third state (indeterminate).</value>
		[Category("Appearance"), DefaultValue(false),
          Bindable(true), Description("Whether or not the checkboxes support three states instead of two")]
        public bool ThreeState
        {
            get => threeState;
            set
            {
                CheckBox cb;

                foreach(Control c in this.ButtonPanel.Controls)
                {
                    cb = (CheckBox)c;
                    cb.ThreeState = value;

                    // If not using three states, reset indeterminate checkboxes to unchecked
                    if(!value && cb.CheckState == CheckState.Indeterminate)
                        cb.CheckState = CheckState.Unchecked;
                }

                threeState = value;

                OnThreeStateChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// This is used to specify the binding context for use with the <see cref="BindingMembersDataSource"/>
        /// property.
        /// </summary>
        /// <value>This binding context will be used when binding each checkbox in the list to a member specified
        /// in the <see cref="BindingMembers"/> property.  This allows you to update the data source members as
        /// the checked state changes on each of the checkboxes in the list.</value>
        [Browsable(false), Description("The binding context to use when binding checkboxes to members in " +
          "BindingMembersDataSource")]
        public BindingContext BindingMembersBindingContext
        {
            get => bindingContext;
            set
            {
                if(bindingContext != value)
                {
                    bindingContext = value;
                    this.RefreshSubControls();
                }
            }
        }

        /// <summary>
        /// This is used to specify the data source for use with the <see cref="BindingMembers"/> property
        /// </summary>
        /// <value>This data source will be used to bind each checkbox in the list to a member specified in the
        /// <see cref="BindingMembers"/> property.  This allows you to update the data source members as the
        /// checked state changes on each of the checkboxes in the list.</value>
        [Category("Data"), DefaultValue(null), RefreshProperties(RefreshProperties.Repaint),
          AttributeProvider(typeof(IListSource)),
          TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
          Description("Specifies a data source for use with BindingMembers to bind each checkbox to a data member")]
        public object BindingMembersDataSource
        {
            get => bindingDataSource;
            set
            {
                if(bindingDataSource != value)
                {
                    bindingDataSource = value;
                    this.RefreshSubControls();
                }
            }
        }

        /// <summary>
        /// This is used to specify the members in the <see cref="BindingMembersDataSource"/> that should be
        /// bound to the <see cref="CheckBox.CheckState"/> property of each checkbox in the list.
        /// </summary>
        /// <value>This allows you to update the data source members as the checked state changes on each of the
        /// checkboxes in the list.  One checkbox will be bound to each member in the list.  If there are more
        /// members in the list than checkboxes, the excess members are ignored and will not be bound.</value>
        [Category("Data"), Description("Specifies a list of members in BindingMembersDataSource that will be " +
          "bound to each checkbox"),
          Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection BindingMembers
        {
            get
            {
                if(bindingMembers == null)
                {
                    bindingMembers = new StringCollection();
                    bindingMembers.ListChanged += BindingMembers_ListChanged;
                }

                return bindingMembers;
            }
        }

	    /// <summary>
        /// This property will return a collection containing the currently selected items in the checkbox list
	    /// </summary>
        /// <remarks>The collection is read-only but the items in it are not</remarks>
		[Category("Data"), Browsable(false), Description("The currently selected items in the checkbox list")]
		public CheckedItemsCollection CheckedItems
		{
			get
            {
                CheckBox cb;
                List<object> list = new List<object>();
                ControlCollection checkboxes = this.ButtonPanel.Controls;

                for(int idx = 0; idx < checkboxes.Count; idx++)
                {
                    cb = checkboxes[idx] as CheckBox;

                    if(cb.CheckState == CheckState.Checked || cb.CheckState == CheckState.Indeterminate)
                        list.Add(this.Items[idx]);
                }

                return new CheckedItemsCollection(this, list);
            }
		}

	    /// <summary>
        /// This property will return a collection containing the currently selected indices in the checkbox list
	    /// </summary>
        /// <remarks>The collection is read-only.</remarks>
		[Category("Data"), Browsable(false), Description("The currently selected indices in the checkbox list")]
		public CheckedIndicesCollection CheckedIndices
		{
			get
            {
                CheckBox cb;
                List<int> list = new List<int>();
                ControlCollection checkboxes = this.ButtonPanel.Controls;

                for(int idx = 0; idx < checkboxes.Count; idx++)
                {
                    cb = checkboxes[idx] as CheckBox;

                    if(cb.CheckState == CheckState.Checked || cb.CheckState == CheckState.Indeterminate)
                        list.Add(idx);
                }

                return new CheckedIndicesCollection(list);
            }
		}
        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when the check state of a list item changes
        /// </summary>
		[Category("Action"), Description("Occurs when the check state of a list item changes")]
        public event EventHandler<ItemCheckStateEventArgs> ItemCheckStateChanged;

        /// <summary>
        /// This raises the <see cref="ItemCheckStateChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnItemCheckStateChanged(ItemCheckStateEventArgs e)
        {
            ItemCheckStateChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="ThreeState"/> property changes
        /// </summary>
		[Category("Action"), Description("Occurs when the ThreeState property changes")]
        public event EventHandler ThreeStateChanged;

        /// <summary>
        /// This raises the <see cref="ThreeStateChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnThreeStateChanged(System.EventArgs e)
        {
            ThreeStateChanged?.Invoke(this, e);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>By default, the checkbox list will display all items in the data source in a single column,
        /// item parts are left aligned and the control will have an etched border.  A default selection (item
        /// zero) is enforced.</remarks>
        public CheckBoxList()
        {
        }
        #endregion

        #region Private class methods and event handlers
        //=====================================================================

        /// <summary>
        /// This is used to translate a binding member value to a true, false, or indeterminate state
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void BindingMember_Format(object sender, ConvertEventArgs e)
        {
            // If null and three state checkboxes are allowed, use Indeterminate.  If two state, use false.
            if(e.Value == null || e.Value == DBNull.Value)
                e.Value = (threeState) ? CheckState.Indeterminate : CheckState.Unchecked;
            else
                e.Value = (Convert.ToBoolean(e.Value, CultureInfo.CurrentCulture)) ? CheckState.Checked :
                    CheckState.Unchecked;
        }

        /// <summary>
        /// This is used to convert a checked state value to a boolean or null value to store in the data source
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void BindingMember_Parse(object sender, ConvertEventArgs e)
        {
            CheckState state = (CheckState)e.Value;

            if(state == CheckState.Indeterminate)
                e.Value = threeState ? (object)DBNull.Value : false;
            else
                e.Value = state == CheckState.Checked;
        }

        /// <summary>
        /// This is used to refresh the controls when the binding members list is changed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void BindingMembers_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.RefreshSubControls();
        }

        /// <summary>
        /// This is handled to update the selected index whenever a checkbox gains the focus
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void CheckBox_Enter(object sender, EventArgs e)
        {
            this.SelectedIndex = this.ButtonPanel.Controls.IndexOf((CheckBox)sender);
        }

        /// <summary>
        /// This is handled to raise the ItemCheckStateChanged event when a checkbox's check state changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            this.OnItemCheckStateChanged(new ItemCheckStateEventArgs(this.ButtonPanel.Controls.IndexOf(cb),
                cb.CheckState));
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is called to force the control to refresh the checkboxes with information from the data source
        /// </summary>
        /// <exception cref="InvalidOperationException">This is thrown if a <see cref="BindingMembersDataSource"/>
        /// has been specified and there are more checkbox list items that members specified in
        /// <see cref="BindingMembers"/>.</exception>
        public override void RefreshSubControls()
        {
            Panel bp = this.ButtonPanel;
            Binding b;
            CheckBox cb;
            CheckState[] states;
            int idx = 0, imageIdx = 0, memberIdx = 0;

            ControlCollection cc = bp.Controls;

            states = new CheckState[cc.Count];

            // Dispose of any prior checkboxes
            while(cc.Count > 0)
            {
                cb = (CheckBox)cc[0];
                states[idx] = cb.CheckState;
                cb.Enter -= CheckBox_Enter;
                cb.CheckStateChanged -= CheckBox_CheckStateChanged;
                cc.RemoveAt(0);
                cb.Dispose();
                idx++;
            }

            idx = 0;

            foreach(object oItem in this.Items)
            {
                cb = new CheckBox();

                // In .NET 2.0, we could use the UseMnemonic property but it doesn't work with FlatStyle.System
                // so we'll modify the text which works with it and with .NET 1.1.
                if(this.UseMnemonic)
                    cb.Text = this.GetItemText(oItem).Replace("&&", "&");
                else
                    cb.Text = this.GetItemText(oItem).Replace("&", "&&");

                cb.Appearance = this.Appearance;
                cb.FlatStyle = this.FlatStyle;
                cb.ThreeState = threeState;
                cb.CheckAlign = this.CheckAlign;
                cb.TextAlign = this.TextAlign;
                cb.ImageAlign = this.ImageAlign;
                cb.ImageList = this.ImageList;

                // Don't hook up the event in design mode.  They are clickable.  We'll also skip hooking up the
                // data bindings.
                if(!this.DesignMode)
                {
                    cb.Enter += CheckBox_Enter;
                    cb.CheckStateChanged += CheckBox_CheckStateChanged;

                    // If we have a data source and available members, bind the checkbox's CheckState to the
                    // member.
                    if(bindingDataSource != null)
                        if(memberIdx < bindingMembers.Count)
                        {
                            if(bindingContext != null)
                                cb.BindingContext = bindingContext;

                            b = new Binding("CheckState", bindingDataSource, bindingMembers[memberIdx]);
                            b.Format += BindingMember_Format;
                            b.Parse += BindingMember_Parse;
                            cb.DataBindings.Add(b);
                            memberIdx++;
                        }
                        else
                            throw new InvalidOperationException(LR.GetString("ExTooFewBindingMembers"));
                }

                if(this.ImageList != null)
                {
                    cb.ImageIndex = imageIdx;
                    imageIdx++;

                    // Wrap the index if there are more items than images
                    if(imageIdx == this.ImageList.Images.Count)
                        imageIdx = 0;
                }

                // Restore the state if necessary
                if(this.SelectedIndex != -1 && idx < states.Length)
                    cb.CheckState = states[idx];

                bp.Controls.Add(cb);
                idx++;
            }

            base.RefreshSubControls();
            this.PerformLayout();
        }

        /// <summary>
        /// Returns a value indicating whether the specified item is checked
        /// </summary>
        /// <param name="index">The index of the item to examine</param>
        /// <returns>True if the item is checked, false if it is not</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
        /// bounds of the item collection.</exception>
        /// <overloads>There are two overloads for this method</overloads>
        public bool GetItemChecked(int index)
        {
            if(index < 0 || index >= this.Items.Count)
                throw new ArgumentOutOfRangeException(nameof(index), index, LR.GetString("ExItemIndexOutOfRange"));

            return ((CheckBox)this.ButtonPanel.Controls[index]).Checked;
        }

        /// <summary>
        /// Returns a value indicating whether the item with the specified value is checked
        /// </summary>
        /// <param name="key">The item to examine in the data source.  The <see cref="BaseListControl.ValueMember"/>
        /// is searched for this value.</param>
        /// <returns>True if the item is checked, false if it is not</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the value is not found in the
        /// collection.</exception>
        public bool GetItemChecked(object key)
        {
            int index = this.Find(key);

            if(index == -1)
                throw new ArgumentOutOfRangeException(nameof(key), key, LR.GetString("ExItemIndexOutOfRange"));

            return ((CheckBox)this.ButtonPanel.Controls[index]).Checked;
        }

        /// <summary>
        /// Returns a value indicating the current check state of the item
        /// </summary>
        /// <param name="index">The index of the item to examine</param>
        /// <returns>The current check state of the specified item</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
        /// bounds of the item collection.</exception>
        /// <overloads>There are two overloads for this method</overloads>
        public CheckState GetItemCheckState(int index)
        {
            if(index < 0 || index >= this.Items.Count)
                throw new ArgumentOutOfRangeException(nameof(index), index, LR.GetString("ExItemIndexOutOfRange"));

            return ((CheckBox)this.ButtonPanel.Controls[index]).CheckState;
        }

        /// <summary>
        /// Returns a value indicating the current check state of the item with the specified value
        /// </summary>
        /// <param name="key">The item in the data source to examine.  The <see cref="BaseListControl.ValueMember"/>
        /// is searched for this value.</param>
        /// <returns>The current check state of the specified item</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the value is not found in the
        /// collection.</exception>
        public CheckState GetItemCheckState(object key)
        {
            int index = this.Find(key);

            if(index == -1)
                throw new ArgumentOutOfRangeException(nameof(key), key, LR.GetString("ExItemIndexOutOfRange"));

            return ((CheckBox)this.ButtonPanel.Controls[index]).CheckState;
        }

        /// <summary>
        /// Sets the check state of the item at the specified index to <c>Checked</c> or <c>Unchecked</c>
        /// </summary>
        /// <param name="index">The index of the item to check or uncheck</param>
        /// <param name="check">True to check the item, false to uncheck it</param>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
        /// bounds of the item collection.</exception>
        /// <overloads>There are two overloads for this method</overloads>
        public void SetItemChecked(int index, bool check)
        {
            if(index < 0 || index >= this.Items.Count)
                throw new ArgumentOutOfRangeException(nameof(index), index, LR.GetString("ExItemIndexOutOfRange"));

            ((CheckBox)this.ButtonPanel.Controls[index]).Checked = check;
        }

        /// <summary>
        /// Sets the check state of the item with the specified key to <c>Checked</c> or <c>Unchecked</c>
        /// </summary>
        /// <param name="key">The item to check or uncheck in the data source.  The
        /// <see cref="BaseListControl.ValueMember"/> is searched for this value.</param>
        /// <param name="check">True to check the item, false to uncheck it.</param>
        /// <returns>The index of the item with the specified key or -1 if the item could not be found</returns>
        public int SetItemChecked(object key, bool check)
        {
            int index = this.Find(key);

            if(index != -1)
                ((CheckBox)this.ButtonPanel.Controls[index]).Checked = check;

            return index;
        }

        /// <summary>
        /// Sets the check state of the item at the specified index to the specified value
        /// </summary>
        /// <param name="index">The index of the item to change</param>
        /// <param name="state">The new check state of the item</param>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
        /// bounds of the item collection.</exception>
        /// <overloads>There are two overloads for this method</overloads>
        public void SetItemCheckState(int index, CheckState state)
        {
            if(index < 0 || index >= this.Items.Count)
                throw new ArgumentOutOfRangeException(nameof(index), index, LR.GetString("ExItemIndexOutOfRange"));

            ((CheckBox)this.ButtonPanel.Controls[index]).CheckState = state;
        }

        /// <summary>
        /// Sets the check state of the item with the specified key to the specified value
        /// </summary>
        /// <param name="key">The item to change in the data source.  The <see cref="BaseListControl.ValueMember"/>
        /// is searched for this value.</param>
        /// <param name="state">The new check state of the item</param>
        /// <returns>The index of the item with the specified key or -1 if the item could not be found</returns>
        public int SetItemCheckState(object key, CheckState state)
        {
            int index = this.Find(key);

            if(index != -1)
                ((CheckBox)this.ButtonPanel.Controls[index]).CheckState = state;

            return index;
        }

        /// <summary>
        /// Clear all currently checked items by setting them to an unchecked state
        /// </summary>
        public void ClearSelections()
        {
            foreach(Control c in this.ButtonPanel.Controls)
                ((CheckBox)c).Checked = false;
        }

        /// <summary>
        /// Select all items by setting their state to checked
        /// </summary>
        public void SelectAll()
        {
            foreach(Control c in this.ButtonPanel.Controls)
                ((CheckBox)c).Checked = true;
        }
        #endregion
    }
}
