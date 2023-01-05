//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : BaseListControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This file contains a base list control class used to contain the common item collection and data binding
// elements for many of the list controls in this library.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/16/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is the abstract base list control class used to contain the common item collection and data binding
    /// elements for many of the list controls in this library.
    /// </summary>
    /// <remarks>This is a complete reimplementation of the standard .NET <c>System.Windows.Forms.ListControl</c>
    /// class.  Its main differences are that it is derived from <c>UserControl</c> instead of <c>Control</c>, it
    /// fixes several bugs present in the Windows Forms base list control class, and it also exposes more
    /// functionality that is hidden in the Windows Forms base list control class so that it is easier to derive
    /// new list control types from this one.</remarks>
    [DefaultEvent("SelectedIndexChanged"), DefaultProperty("Items")]
    public abstract class BaseListControl : UserControl, ISupportInitialize
    {
        #region Item comparer class
        //=====================================================================

        /// <summary>
        /// This is a custom comparer class for the object collection so that items in the collection can be
        /// sorted in ascending or descending order.
        /// </summary>
        private sealed class ItemComparer : IComparer
        {
            //=================================================================

            private readonly BaseListControl listControl;

            //=================================================================

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="lc">The list control to which the comparer belongs</param>
            public ItemComparer(BaseListControl lc)
            {
                listControl = lc;
            }

            /// <summary>
            /// Compare two list control entries
            /// </summary>
            /// <param name="x">The first object</param>
            /// <param name="y">The second object</param>
            /// <returns>Returns a negative value if x is less than y, zero if they are equal, or a positive
            /// value if x is greater than y.  The results are inverted if the sort order is descending.  If the
            /// sort order is set to <c>None</c> all items are treated as equal.</returns>
            public int Compare(object x, object y)
            {
                ListSortOrder order = listControl.SortOrder;

                if(order == ListSortOrder.None)
                    return 0;

                if(x == null)
                {
                    if(y == null)
                        return 0;

                    return (order == ListSortOrder.Ascending) ? -1 : 1;
                }

                if(y == null)
                    return (order == ListSortOrder.Ascending) ? 1 : -1;

                string textX = listControl.GetItemText(x), textY = listControl.GetItemText(y);

                if(order == ListSortOrder.Ascending)
                    return Application.CurrentCulture.CompareInfo.Compare(textX, textY, CompareOptions.StringSort);

                return Application.CurrentCulture.CompareInfo.Compare(textY, textX, CompareOptions.StringSort);
            }
        }
        #endregion

        #region Object collection class
        //=====================================================================

        /// <summary>
        /// This object collection is used to hold items for the list controls and is sortable in ascending or
        /// descending order.
        /// </summary>
        [ListBindable(false)]
        public sealed class ObjectCollection : IList, ICollection, IEnumerable
        {
            //=================================================================

            private IComparer comparer;
            private ArrayList innerList;
            private readonly BaseListControl owner;

            //=================================================================

            /// <summary>
            /// This is used to get whether or not the collection is synchronized
            /// </summary>
            /// <value>Always returns false (not synchronized)</value>
            bool ICollection.IsSynchronized => false;

            /// <summary>
            /// Returns an object that can be used to synchronize the collection
            /// </summary>
            /// <value>Always returns a reference to itself</value>
            object ICollection.SyncRoot => this;

            /// <summary>
            /// This is used to get whether or not the collection is of a fixed size
            /// </summary>
            /// <value>Always returns false as the collection size varies</value>
            bool IList.IsFixedSize => false;

            /// <summary>
            /// This is used to get an <see cref="IComparer"/> instance that can be used to sort the collection
            /// </summary>
            private IComparer Comparer
            {
                get
                {
                    if(comparer == null)
                        comparer = new ItemComparer(owner);

                    return comparer;
                }
            }

            /// <summary>
            /// This is used to get a count of the items in the collection
            /// </summary>
            public int Count => this.InnerList.Count;

            /// <summary>
            /// This is used to get a reference to the inner list
            /// </summary>
            /// <value>The collection uses an <see cref="ArrayList"/> to hold the collection objects</value>
            private ArrayList InnerList
            {
                get
                {
                    if(innerList == null)
                        innerList = new ArrayList();

                    return innerList;
                }
            }

            /// <summary>
            /// This is used to get whether or not the collection is read-only
            /// </summary>
            /// <value>Always returns false as the collection is always editable</value>
            public bool IsReadOnly => false;

            /// <summary>
            /// This is used to set or get items by index position
            /// </summary>
            /// <param name="index">The index position of the item to set or get</param>
            /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
            /// bounds of the collection.</exception>
            /// <exception cref="ArgumentException">This is thrown if an attempt is made to set an item when a
            /// data source is in use.</exception>
            /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
            /// bounds of the collection.</exception>
            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public object this[int index]
            {
                get
                {
                    if(index < 0 && index >= this.InnerList.Count)
                        throw new ArgumentOutOfRangeException(nameof(index), index, LR.GetString("ExItemIndexOutOfRange"));

                    return this.InnerList[index];
                }
                set
                {
                    if(owner.DataSource != null)
                        throw new ArgumentException(LR.GetString("ExDataSourceLocksItems"));

                    this.SetItemInternal(index, value);
                }
            }

            //=================================================================

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="lc">The list control to which the collection belongs</param>
            public ObjectCollection(BaseListControl lc)
            {
                owner = lc;
            }

            /// <summary>
            /// Add an item to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            /// <exception cref="ArgumentNullException">This is thrown if the item is null</exception>
            /// <exception cref="ArgumentException">This is thrown if an attempt is made to add an item to the
            /// collection when a data source is in use.</exception>
            /// <returns>The index of the added item</returns>
            public int Add(object item)
            {
                int idx;

                if(item == null)
                    throw new ArgumentNullException(nameof(item), LR.GetString("ExNullParameter"));

                if(owner.DataSource != null)
                    throw new ArgumentException(LR.GetString("ExDataSourceLocksItems"));

                this.InnerList.Add(item);

                if(!owner.IsInitializing)
                    owner.RefreshSubControls();

                if(owner.SortOrder != ListSortOrder.None)
                {
                    this.InnerList.Sort(this.Comparer);
                    idx = this.InnerList.IndexOf(item);
                }
                else
                    idx = this.InnerList.Count - 1;

                return idx;
            }

            /// <summary>
            /// <see cref="IList"/> implementation to add an item to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            int IList.Add(object item)
            {
                return this.Add(item);
            }

            /// <summary>
            /// Add a range of items to the collection from an array
            /// </summary>
            /// <param name="items">The items to add to the collection</param>
            /// <exception cref="ArgumentException">This is thrown if an attempt is made to add an item to the
            /// collection when a data source is in use.</exception>
            public void AddRange(object[] items)
            {
                if(owner.DataSource != null)
                    throw new ArgumentException(LR.GetString("ExDataSourceLocksItems"));

                this.AddRangeInternal(items);
            }

            /// <summary>
            /// Add a range of items to the collection from an <see cref="IList"/>
            /// </summary>
            /// <exception cref="ArgumentNullException">This is thrown if the items reference is null or any item
            /// in the collection is null.</exception>
            internal void AddRangeInternal(IList items)
            {
                if(items == null)
                    throw new ArgumentNullException(nameof(items), LR.GetString("ExNullItems"));

                foreach(object o in items)
                    if(o == null)
                        throw new ArgumentNullException(nameof(items), LR.GetString("ExNullItems"));

                this.InnerList.AddRange(items);

                if(!owner.IsInitializing)
                    owner.RefreshSubControls();

                if(owner.SortOrder != ListSortOrder.None)
                    this.InnerList.Sort(this.Comparer);
            }

            /// <summary>
            /// Clear all items from the collection
            /// </summary>
            /// <exception cref="ArgumentException">This is thrown if an attempt is made to clear items in the
            /// collection when a data source is in use.</exception>
            public void Clear()
            {
                if(owner.DataSource != null)
                    throw new ArgumentException(LR.GetString("ExDataSourceLocksItems"));

                this.ClearInternal(true);
            }

            /// <summary>
            /// Clear all items from the collection and reset the selected item
            /// </summary>
            /// <param name="resetSelIdx">True to reset the selected index, false to leave it alone</param>
            internal void ClearInternal(bool resetSelIdx)
            {
                this.InnerList.Clear();

                if(!owner.IsInitializing)
                    owner.RefreshSubControls();

                if(resetSelIdx)
                    owner.SelectedIndex = -1;
            }

            /// <summary>
            /// Check to see if a value exists in the collection
            /// </summary>
            /// <param name="value">The value for which to look</param>
            /// <returns>True if it is in the collection, false if not</returns>
            public bool Contains(object value)
            {
                return (this.IndexOf(value) != -1);
            }

            /// <summary>
            /// Copy the collection items to an array
            /// </summary>
            /// <param name="dest">The destination array</param>
            /// <param name="startIndex">The starting index from which to copy</param>
            public void CopyTo(object[] dest, int startIndex)
            {
                this.InnerList.CopyTo(dest, startIndex);
            }

            /// <summary>
            /// Get an enumerator for the collection
            /// </summary>
            /// <returns>An enumerator for the collection</returns>
            public IEnumerator GetEnumerator()
            {
                return this.InnerList.GetEnumerator();
            }

            /// <summary>
            /// Get the index of the specified object in the collection
            /// </summary>
            /// <param name="value">The value for which to get the index</param>
            /// <returns>The index of the item or -1 if not found</returns>
            /// <exception cref="ArgumentNullException">This is thrown if the item is null</exception>
            public int IndexOf(object value)
            {
                if(value == null)
                    throw new ArgumentNullException(nameof(value), LR.GetString("ExNullParameter"));

                return this.InnerList.IndexOf(value);
            }

            /// <summary>
            /// Insert a value into the collection at the specified index
            /// </summary>
            /// <param name="index">The index at which to insert the item</param>
            /// <param name="value">The value to insert</param>
            /// <remarks>If the collection is sorted, the value is added to the collection and the collection is
            /// resorted.  The value may or may not end up at the requested index.</remarks>
            /// <exception cref="ArgumentNullException">This is thrown if the value is null.</exception>
            /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
            /// bounds of the collection.</exception>
            /// <exception cref="ArgumentException">This is thrown if an attempt is made to add a value to the
            /// collection when a data source is in use.</exception>
            public void Insert(int index, object value)
            {
                if(owner.DataSource != null)
                    throw new ArgumentException(LR.GetString("ExDataSourceLocksItems"));

                if(value == null)
                    throw new ArgumentNullException(nameof(value), LR.GetString("ExNullParameter"));

                if(index < 0 || index > this.InnerList.Count)
                    throw new ArgumentOutOfRangeException(nameof(index), index, LR.GetString("ExItemIndexOutOfRange"));

                if(owner.SortOrder != ListSortOrder.None)
                    this.Add(value);
                else
                    this.InnerList.Insert(index, value);

                if(!owner.IsInitializing)
                    owner.RefreshSubControls();
            }

            /// <summary>
            /// Remove the specified value from the collection
            /// </summary>
            /// <param name="value">The value to remove</param>
            public void Remove(object value)
            {
                int idx = this.InnerList.IndexOf(value);

                if(idx != -1)
                {
                    this.RemoveAt(idx);

                    if(!owner.IsInitializing)
                        owner.RefreshSubControls();
                }
            }

            /// <summary>
            /// Remove the item at the specified index from the collection
            /// </summary>
            /// <param name="index">The index of the item to remove</param>
            /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
            /// bounds of the collection.</exception>
            /// <exception cref="ArgumentException">This is thrown if an attempt is made to remove an item from
            /// the collection when a data source is in use.</exception>
            public void RemoveAt(int index)
            {
                if(owner.DataSource != null)
                    throw new ArgumentException(LR.GetString("ExDataSourceLocksItems"));

                if(index < 0 || index >= this.InnerList.Count)
                    throw new ArgumentOutOfRangeException(nameof(index), index, LR.GetString("ExItemIndexOutOfRange"));

                this.InnerList.RemoveAt(index);

                if(!owner.IsInitializing)
                    owner.RefreshSubControls();

                if(!owner.IsHandleCreated && index < owner.SelectedIndex)
                    owner.SelectedIndex--;
            }

            /// <summary>
            /// Store an item in the collection at the specified index
            /// </summary>
            /// <param name="index">The index at which to insert the item</param>
            /// <param name="item">The item to insert</param>
            /// <exception cref="ArgumentNullException">This is thrown if the item is null.</exception>
            /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
            /// bounds of the collection.</exception>
            internal void SetItemInternal(int index, object item)
            {
                if(index < 0 || index >= this.InnerList.Count)
                    throw new ArgumentOutOfRangeException(nameof(index), index, LR.GetString("ExItemIndexOutOfRange"));

                this.InnerList[index] = item ?? throw new ArgumentNullException(nameof(item), LR.GetString("ExNullParameter"));

                if(!owner.IsInitializing)
                {
                    owner.RefreshSubControls();

                    if(owner.IsHandleCreated && index == owner.SelectedIndex)
                        owner.UpdateText();
                }
            }

            /// <summary>
            /// <see cref="ICollection"/> implementation to copy items to an array
            /// </summary>
            /// <param name="dest">The destination array</param>
            /// <param name="index">The index at which to start copying</param>
            void ICollection.CopyTo(Array dest, int index)
            {
                this.InnerList.CopyTo(dest, index);
            }
        }
        #endregion

        #region Private data members
        //====================================================================

        // List control items and state information
        private BaseListControl.ObjectCollection itemsCollection;
        private ListSortOrder sortOrder;
        private FlatStyle flatStyle;

        // Internal state flags
        private bool selectedValueChangedFired, enforceDefaultSelection, suppressRefreshItems;
        private int  initCount, defaultSelection, selectedIndex;

        // Data binding stuff
        private object dataSource;
        private CurrencyManager dataManager;
        private BindingMemberInfo displayMember;
        private BindingMemberInfo valueMember;

        #endregion

        #region Properties
        //====================================================================

        /// <summary>
        /// This helper property is used to see whether or not the display member property value is valid for the
        /// data source.
        /// </summary>
        protected bool BindingFieldEmpty => (displayMember.BindingField.Length < 1);

        /// <summary>
        /// This is used to get a reference to the <see cref="CurrencyManager"/> associated with this control
        /// </summary>
        /// <remarks>This property is valid if the <see cref="DataSource"/> property is set. If this is not a
        /// data-bound control, the default is a null reference. </remarks>
        [Browsable(false), Description("Returns the CurrencyManager that the list control is currently using " +
          "to get data from the data source.")]
        public CurrencyManager DataManager => dataManager;

        /// <summary>
        /// This is used to set or get the text for the list control
        /// </summary>
        /// <remarks>When set to null, the <see cref="SelectedIndex"/> is set to -1.  If not null, an attempt is
        /// made to set the selected index to the item matching the specified text.  If no match is found, the
        /// selected index is set to -1.</remarks>
        [Bindable(true), DefaultValue(""), EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get
            {
                if(base.DesignMode)
                    return base.Text;

                if(this.SelectedIndex == -1)
                    return null;

                return this.GetItemText(this.SelectedItem);
            }
            set
            {
                base.Text = value;

                if(!base.DesignMode)
                    if(value == null)
                        this.SelectedIndex = -1;
                    else
                        if((this.SelectedItem == null || !String.Equals(value,
                          this.GetItemText(this.SelectedItem), StringComparison.Ordinal)))
                        {
                            for(int nIdx = 0; nIdx < this.Items.Count; nIdx++)
                            {
                                if(String.Equals(value, this.GetItemText(this.Items[nIdx]), StringComparison.Ordinal))
                                {
                                    this.SelectedIndex = nIdx;
                                    return;
                                }

                                if(String.Equals(value, this.GetItemText(this.Items[nIdx]),
                                  StringComparison.OrdinalIgnoreCase))
                                {
                                    this.SelectedIndex = nIdx;
                                    return;
                                }
                            }

                            // Value was null or no match was found
                            this.SelectedIndex = -1;
                        }
            }
        }

        /// <summary>
        /// This is used to set or get the flat drawing style to use for the control
        /// </summary>
        [Category("Appearance"), Bindable(true), DefaultValue(FlatStyle.Standard),
          Description("The flat drawing style to use for the control")]
        public virtual FlatStyle FlatStyle
        {
            get => flatStyle;
            set
            {
                flatStyle = value;
                base.PerformLayout();
            }
        }

        /// <summary>
        /// This gets or sets the data source for the list control
        /// </summary>
        /// <value><para>The data source object must support the <see cref="IList"/> interface such as a
        /// <see cref="System.Data.DataSet"/> or an <see cref="Array"/>.  The default is null and it will use
        /// whatever is in the <see cref="Items"/> collection instead.  If using a data source, set the
        /// <see cref="DisplayMember"/> and <see cref="ValueMember"/> properties too.  Setting this property to
        /// null also clears the <see cref="DisplayMember"/> property.</para>
        /// 
        /// <para>A data source cannot be used with the <see cref="SortOrder"/> property.  If a non-null data
        /// source is set, the <c>SortOrder</c> property is set to <c>None</c> automatically.</para></value>
        /// <exception cref="ArgumentException">This is thrown if the data source does not support the
        /// <see cref="IList"/> interface.</exception>
        [Category("Data"), DefaultValue(null), RefreshProperties(RefreshProperties.Repaint),
          AttributeProvider(typeof(IListSource)),
          TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
          Description("Indicates the list that the control will use to get its items")]
        public object DataSource
        {
            get => dataSource;
            set
            {
                if(value != null && !(value is IList) && !(value is IListSource))
                    throw new ArgumentException(LR.GetString("ExBadDataSource"));

                if(dataSource != value)
                {
                    if(value != null && sortOrder != ListSortOrder.None)
                        sortOrder = ListSortOrder.None;

                    if(value == null)
                        displayMember = new BindingMemberInfo(String.Empty);

                    try
                    {
                        selectedValueChangedFired = false;
                        this.SetDataConnection(value, displayMember, false);
                    }
                    catch(ArgumentException)
                    {
                        // Eat the exception.  It could be that the user changed the data source before setting
                        // the new display member.
                        this.DisplayMember = String.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a string that specifies the property of the data source whose contents you want to
        /// display.
        /// </summary>
        /// <value>The default is an empty string.  If not set, the object's <c>ToString()</c> method is used to
        /// get the display value.  If the new display member cannot be set, the old display member is retained.</value>
        [Category("Data"), DefaultValue(""),
          TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
          Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
          Description("Indicates the property to display for items in this collection")]
        public string DisplayMember
        {
            get => displayMember.BindingMember;
            set
            {
                BindingMemberInfo bmi = new BindingMemberInfo(value);

                if(!bmi.Equals(displayMember))
                    this.SetDataConnection(dataSource, bmi, false);
            }
        }

        /// <summary>
        /// Gets or sets a string that specifies the property of the data source from which to draw the value
        /// </summary>
        /// <value><para>The default value is an empty string.  Specify a value to bind the data to a property.
        /// Clear it by setting it to an empty string or null.</para>
        /// 
        /// <para>Setting a new value member will raise the <see cref="ValueMemberChanged"/> and
        /// <see cref="SelectedValueChanged"/> events.</para></value>
        /// <exception cref="ArgumentException">This is thrown if the value member cannot be found in the data
        /// source.</exception>
        [Category("Data"), DefaultValue(""),
          Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
          Description("Indicates the property to use for the value of items in this collection")]
        public string ValueMember
        {
            get => valueMember.BindingMember;
            set
            {
                BindingMemberInfo bmi = new BindingMemberInfo(value);

                if(!bmi.Equals(valueMember))
                {
                    if(this.DisplayMember.Length == 0)
                        this.SetDataConnection(dataSource, bmi, false);

                    if(dataManager != null && bmi.BindingMember.Length != 0 && !this.BindingMemberInfoInDataManager(bmi))
                        throw new ArgumentException(LR.GetString("ExWrongValueMember"));

                    valueMember = bmi;
                    this.OnValueMemberChanged(EventArgs.Empty);

                    // Don't fire SelectedValueChanged unless we have data
                    if(dataManager != null && dataManager.Count != 0)
                        this.OnSelectedValueChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets an object representing the collection of the items contained in this list control
        /// </summary>
        /// <remarks>When the list control does not have a <see cref="DataSource"/>, this property allows you to
        /// add and remove items.  If there is a data source, the collection is read-only.</remarks>
        [Category("Data"), Localizable(true), Description("The items in the list control"),
          Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObjectCollection Items
        {
            get
            {
                if(itemsCollection == null)
                    itemsCollection = new ObjectCollection(this);

                return itemsCollection;
            }
        }

        /// <summary>
        /// Gets or sets the index specifying the currently selected item
        /// </summary>
        /// <value>This is a zero-based index into the items collection.  A value of -1 indicates that there is
        /// no current selection.</value>
        [Browsable(false), Description("The index of the currently selected item in the list"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SelectedIndex
        {
            get => selectedIndex;
            set => selectedIndex = value;
        }

        /// <summary>
        /// Gets or sets the currently selected item in the list control
        /// </summary>
        /// <value><para>If there is no current selection, this property returns null.</para>
        /// 
        /// <para>When you set this property to an object, the list control attempts to make that object the
        /// currently selected one in the list. If the object is found in the list, it is displayed as the
        /// selected item and the <see cref="SelectedIndex"/> property is set to the corresponding index. If the
        /// object does not exist in the list the <see cref="SelectedIndex"/> property is left at its current
        /// value.</para></value>
        [Browsable(false), Bindable(true), Description("The currently selected item in the list control"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get
            {
                if(this.SelectedIndex != -1)
                    return this.Items[this.SelectedIndex];

                return null;
            }
            set
            {
                int nIdx;

                if(itemsCollection != null)
                {
                    if(value != null)
                    {
                        nIdx = itemsCollection.IndexOf(value);

                        if(nIdx != -1)
                            this.SelectedIndex = nIdx;
                    }
                    else
                        this.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the member property specified by the <see cref="ValueMember"/> property in
        /// the currently selected item.
        /// </summary>
        /// <value><para>This returns an object containing the value of the member of the data source specified
        /// by the <see cref="ValueMember"/> property.</para>
        /// 
        /// <para>If a property is not specified in <see cref="ValueMember"/>, this property returns the results
        /// of the <c>ToString</c> method of the object.</para></value>
        [Category("Data"), DefaultValue(null), Bindable(true), Browsable(false),
          Description("The current value of the ValueMember property of the currently selected item"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue
        {
            get
            {
                if(this.SelectedIndex != -1 && dataManager != null)
                    return this.FilterItemOnProperty(dataManager.List[this.SelectedIndex], valueMember.BindingField);

                return null;
            }
            set
            {
                if(dataManager != null)
                    this.SelectedIndex = this.Find(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the items in the list control are sorted and, if so, in what
        /// order.
        /// </summary>
        /// <value>If set to <c>None</c> (the default), the items in the list control are not sorted.  If set to
        /// <c>Ascending</c> or <c>Descending</c> the list control sorts existing entries and adds new entries to
        /// the appropriate sorted position in the list. You can use this property to automatically sort items in
        /// a list control.  As items are added to a sorted list control, the items are moved to the appropriate
        /// location in the sorted list. When you set the property to <c>None</c>, new items are added to the end
        /// of the existing list. The sort is case-insensitive.</value>
        /// <exception cref="ArgumentException">This is thrown if an attempt is made to set a sort order other
        /// than <c>None</c> when a data source is in use.</exception>
        [Category("Behavior"), DefaultValue(ListSortOrder.None),
          Description("This defines the sort order for the list control items")]
        public ListSortOrder SortOrder
        {
            get => sortOrder;
            set
            {
                if(sortOrder != value)
                {
                    if(dataSource != null && value != ListSortOrder.None)
                        throw new ArgumentException(LR.GetString("ExNoSortWithDataSource"));

                    sortOrder = value;

                    this.RefreshItems();
                    this.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// This property is used to set or get the default selection behavior
        /// </summary>
        /// <value>If true (the default), a <see cref="SelectedIndex"/> of -1 (no selection) is not allowed.
        /// Instead, the index specified by the <see cref="DefaultSelection"/> property is used instead.</value>
		[Category("Behavior"), Bindable(true), DefaultValue(true),
          Description("Specify whether or not to enforce a default selection")]
        public virtual bool EnforceDefaultSelection
        {
            get => enforceDefaultSelection;
            set
            {
                enforceDefaultSelection = value;
                OnEnforceDefaultSelectionChanged(EventArgs.Empty);

                if(value && this.SelectedIndex == -1 && this.Items.Count != 0 && !base.DesignMode)
                    this.SelectedIndex = this.DefaultSelection;
            }
        }

        /// <summary>
        /// This property is used to set or get the default selection's index
        /// </summary>
        /// <value>If <see cref="EnforceDefaultSelection"/> is true, a <see cref="SelectedIndex"/> of -1 (no
        /// selection) is not allowed.  Instead, the index specified by this property is used.</value>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index is less than zero</exception>
		[Category("Behavior"), Bindable(true), DefaultValue(0),
          Description("The index to use as a default selection")]
        public virtual int DefaultSelection
        {
            get => defaultSelection;
            set
            {
                if(value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, LR.GetString("ExNegativeDefSel"));

                defaultSelection = value;

                OnDefaultSelectionChanged(EventArgs.Empty);

                if(dataSource != null && this.EnforceDefaultSelection && this.SelectedIndex == -1 &&
                  this.Items.Count != 0 && !base.DesignMode)
                    this.SelectedIndex = defaultSelection;
            }
        }

        /// <summary>
        /// This property returns the mapping name used for the item list
        /// </summary>
        [Browsable(false), Description("Get the mapping name for the control's item list")]
        public string MappingName
        {
            get
            {
                string strName = String.Empty;

                // If the value member has a binding path, use that
                if(valueMember.BindingPath.Length != 0)
                    return valueMember.BindingPath;

                // Use the Items collection
                if(dataSource == null)
                {
                    if(this.Items.Count > 0)
                        if(this.Items[0] is ValueType)
                            strName = "ValueType";
                        else
                            strName = this.Items[0].GetType().Name;

                    return strName;
                }

                // Return the type of the data source
                object oSource = dataSource;

                if(oSource is IListSource)
                    oSource = ((IListSource)dataSource).GetList();

                ITypedList itl = (oSource as ITypedList);

                if(itl != null)
                    strName = itl.GetListName(null);
                else
                    if((oSource is Array) || (oSource is IList))
                        strName = oSource.GetType().Name;
                    else
                        strName = oSource.GetType().Name;

                return strName;
            }
        }

        /// <summary>
        /// This property returns the binding path used for the item list
        /// </summary>
        [Browsable(false), Description("Get the binding path for the control's item list")]
        public string BindingPath => valueMember.BindingPath;

        /// <summary>
        /// This can be used to get the value of the specified column in the currently selected item
        /// </summary>
        /// <param name="colName">The column name of the item to get.  This can be any column in the data source,
        /// not just those displayed by the control.</param>
        /// <value>Returns the entry at the specified column in the currently selected item.  If
        /// <see cref="SelectedIndex"/> equals -1 (no selection) or the column cannot be found, this returns
        /// null.</value>
        /// <overloads>There are two overloads for this property</overloads>
        [Browsable(false), Description("Get the specified column from the current row")]
        public object this[string colName] => this[this.SelectedIndex, colName];

        /// <summary>
        /// This can be used to get the value of the specified column in the specified row of the list control's
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
                if(rowIdx < 0 || rowIdx >= this.Items.Count || colName == null || colName.Length == 0)
                    return null;

                object oItem = this.Items[rowIdx];

                if(oItem != null)
                {
                    PropertyDescriptor pd;

                    if(dataManager != null)
                        pd = dataManager.GetItemProperties().Find(colName, true);
                    else
                        pd = TypeDescriptor.GetProperties(oItem).Find(colName, true);

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
        /// List controls do not use this property so it is hidden.  It always returns null
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
          Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Image BackgroundImage => null;

        /// <summary>
        /// List controls do not use this property so it is hidden.  It always returns the base class's value.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
          Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AutoScroll => base.AutoScroll;

        /// <summary>
        /// List controls do not use this property so it is hidden.  It always returns the base margin.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
          Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Size AutoScrollMargin => base.AutoScrollMargin;

        /// <summary>
        /// List controls do not use this property so it is hidden.  It always returns the base size.
        /// </summary>
        /// <exclude/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
          Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Size AutoScrollMinSize => base.AutoScrollMinSize;

        /// <summary>
        /// List controls do not use the background image so this event is hidden
        /// </summary>
        /// <exclude/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackgroundImageChanged;

        /// <summary>
        /// List controls do not use the load event so it is hidden
        /// </summary>
        /// <exclude/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler Load;

#pragma warning restore 0067
        #endregion

        #region Other Events
        //=====================================================================

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
            if(dataSource == null && !suppressRefreshItems)
                this.Items.ClearInternal(true);

            if(this.SortOrder == ListSortOrder.None && this.Created)
                DataSourceChanged?.Invoke(this, e);

            if(!suppressRefreshItems)
                this.RefreshItems();
        }

        /// <summary>
        /// This event is raised when the <see cref="DisplayMember"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the control's display member changes")]
        public event EventHandler DisplayMemberChanged;

        /// <summary>
        /// This raises the <see cref="DisplayMemberChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDisplayMemberChanged(EventArgs e)
        {
            DisplayMemberChanged?.Invoke(this, e);

            if(!suppressRefreshItems)
                this.RefreshItems();
        }

        /// <summary>
        /// This event is raised when the <see cref="ValueMember"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the control's value member changes")]
        public event EventHandler ValueMemberChanged;

        /// <summary>
        /// This raises the <see cref="ValueMemberChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnValueMemberChanged(EventArgs e)
        {
            ValueMemberChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="SelectedIndex"/> changes
        /// </summary>
        [Category("Behavior"), Description("Occurs when the selected index changes")]
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// This raises the <see cref="SelectedIndexChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            this.OnSelectedValueChanged(EventArgs.Empty);

            SelectedIndexChanged?.Invoke(this, e);

            if(dataManager != null && this.SelectedIndex != -1 && dataManager.Position != this.SelectedIndex)
                dataManager.Position = this.SelectedIndex;
        }

        /// <summary>
        /// This event is raised when the <see cref="SelectedValue"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the control's selected value changes")]
        public event EventHandler SelectedValueChanged;

        /// <summary>
        /// This raises the <see cref="SelectedValueChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSelectedValueChanged(EventArgs e)
        {
            SelectedValueChanged?.Invoke(this, e);

            selectedValueChangedFired = true;
        }

        /// <summary>
        /// This event is raised when the <see cref="SelectedItem"/> changes
        /// </summary>
        [Category("Behavior"), Description("Occurs when the selected item changes")]
        public event EventHandler SelectedItemChanged;

        /// <summary>
        /// This raises the <see cref="SelectedItemChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSelectedItemChanged(EventArgs e)
        {
            SelectedItemChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the sub-controls of the list control are refreshed after it has changed in
        /// some way that affects them.
        /// </summary>
        /// <remarks>This event can be handled to do such things as reload the column collection with your
        /// preferred defaults, adjust the radio button controls, etc.  See the <see cref="RefreshSubControls"/>
        /// method in the derived classes for more information.
        /// </remarks>
        [Category("Behavior"),
          Description("Occurs when the control has changed in some way that requires sub-controls to be recreated")]
        public event EventHandler SubControlsRefreshed;

        /// <summary>
        /// This raises the <see cref="SubControlsRefreshed"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSubControlsRefreshed(EventArgs e)
        {
            SubControlsRefreshed?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="EnforceDefaultSelection"/> property changes
        /// </summary>
        [Category("Behavior"), Description("Occurs when Enforce Default Selection changes")]
        public event EventHandler EnforceDefaultSelectionChanged;

        /// <summary>
        /// This raises the <see cref="EnforceDefaultSelectionChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnEnforceDefaultSelectionChanged(EventArgs e)
        {
            EnforceDefaultSelectionChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="DefaultSelection"/> changes
        /// </summary>
        [Category("Behavior"), Description("Occurs when the default selection changes")]
        public event EventHandler DefaultSelectionChanged;

        /// <summary>
        /// This raises the <see cref="DefaultSelectionChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDefaultSelectionChanged(EventArgs e)
        {
            DefaultSelectionChanged?.Invoke(this, e);
        }
        #endregion

        #region ISupportInitialize interface implementation
        //=====================================================================

        /// <summary>
        /// This is not part of the <c>ISupportInitialize</c> interface but can be used to find out if
        /// initialization is in progress.
        /// </summary>
        /// <value>It returns true if initializing, false if not</value>
        /// <seealso cref="BeginInit"/>
        /// <seealso cref="EndInit"/>
        [Browsable(false)]
        public bool IsInitializing => (initCount != 0);

        /// <summary>
        /// Begin initialization
        /// </summary>
        /// <remarks>Call this before performing an update on the control that will affect many items in it.
        /// This prevents unnecessary layout updates.  Calls to this method must be balanced with a corresponding
        /// call to the <see cref="EndInit"/> method.</remarks>
        /// <seealso cref="IsInitializing"/>
        /// <seealso cref="EndInit"/>
        public void BeginInit()
        {
            if(initCount == 0)
                this.SuspendLayout();

            initCount++;
        }

        /// <summary>
        /// End initialization
        /// </summary>
        /// <remarks>Call this to resume layout changes that have been suspended by a call to
        /// <see cref="BeginInit"/>.</remarks>
        /// <seealso cref="BeginInit"/>
        /// <seealso cref="IsInitializing"/>
        public void EndInit()
        {
            if(initCount > 0)
                initCount--;

            if(initCount == 0)
            {
                this.RefreshSubControls();
                this.ResumeLayout();
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseListControl()
        {
            // Set the value of the double-buffering style bits to true
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            selectedIndex = -1;
            flatStyle = FlatStyle.Standard;
            enforceDefaultSelection = true;
        }
        #endregion

        #region Private class methods and event handlers
        //=====================================================================

        /// <summary>
        /// This is called when the selected item in the data source changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataManager_ItemChanged(object sender, ItemChangedEventArgs e)
        {
            DataManager_ItemChanged(sender, e.Index);
        }

        /// <summary>
        /// Same as above but passed an integer because <c>ItemChangedEventArgs</c> is brain dead and declares
        /// its constructor "internal" so we can't create it the one time we need to in here.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="index">The index of the item being changed in the list</param>
        private void DataManager_ItemChanged(object sender, int index)
        {
            if(sender is CurrencyManager dm)
            {
                if(index == -1)
                    this.SetItemsCore(dm.List);
                else
                    this.SetItemCore(index, dm.List[index]);
            }
        }

        /// <summary>
        /// This is called when the position in the data source changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataManager_PositionChanged(object sender, EventArgs e)
        {
            if(dataManager != null && dataManager.Position < this.Items.Count)
                this.SelectedIndex = dataManager.Position;
        }

        /// <summary>
        /// This is called when the data source is disposed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataSource_Disposed(object sender, EventArgs e)
        {
            this.SetDataConnection(null, new BindingMemberInfo(String.Empty), true);
        }

        /// <summary>
        /// This is called to check whether or not the binding member info can be found in the data source and is
        /// bindable.
        /// </summary>
        /// <param name="bmi">The binding member info</param>
        /// <returns>True if found, false if not found</returns>
        /// <remarks>A case-sensitive search is tried first.  If that fails, a case-insensitive search is tried.</remarks>
        private bool BindingMemberInfoInDataManager(BindingMemberInfo bmi)
        {
            if(dataManager == null)
                return false;

            PropertyDescriptorCollection pdc = dataManager.GetItemProperties();
            int idx, count = pdc.Count;
            bool found = false;

            // Try case-sensitive first
            for(idx = 0; idx < count; idx++)
                if(!typeof(IList).IsAssignableFrom(pdc[idx].PropertyType) && pdc[idx].Name.Equals(bmi.BindingField,
                  StringComparison.Ordinal))
                {
                    found = true;
                    break;
                }

            // If not there, try for a case-insensitive match
            if(!found)
            {
                for(idx = 0; idx < count; idx++)
                {
                    if(!typeof(IList).IsAssignableFrom(pdc[idx].PropertyType) && String.Equals(pdc[idx].Name,
                      bmi.BindingField, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }
            }

            return found;
        }

        /// <summary>
        /// This is called to get information about the data source and the display member and to hook up the
        /// necessary event handlers.
        /// </summary>
        /// <param name="newDataSource">The new data source.</param>
        /// <param name="newDisplayMember">The new display member in the data source.</param>
        /// <param name="force">True to force the information to be updated, false to only update the info if
        /// something really changed.</param>
        /// <exception cref="ArgumentException">This is thrown if the display member cannot be found in the data
        /// source.</exception>
        private void SetDataConnection(object newDataSource, BindingMemberInfo newDisplayMember, bool force)
        {
            bool dataSourceChanged = dataSource != newDataSource;
            bool displayMemberChanged = !displayMember.Equals(newDisplayMember);

            if(force || dataSourceChanged || displayMemberChanged)
            {
                if(dataSource is IComponent c1)
                    c1.Disposed -= DataSource_Disposed;

                dataSource = newDataSource;
                displayMember = newDisplayMember;

                if(dataSource is IComponent c2)
                    c2.Disposed += DataSource_Disposed;

                CurrencyManager cm = null;

                // Touching the BindingContext for the first time creates one and we don't want to come here
                // again yet.
                suppressRefreshItems = true;

                if(newDataSource != null && this.Parent != null && this.BindingContext != null &&
                  newDataSource != Convert.DBNull)
                    cm = (CurrencyManager)this.BindingContext[newDataSource, newDisplayMember.BindingPath];

                suppressRefreshItems = false;

                if(dataManager != cm)
                {
                    if(dataManager != null)
                    {
                        dataManager.ItemChanged -= DataManager_ItemChanged;
                        dataManager.PositionChanged -= DataManager_PositionChanged;
                    }

                    dataManager = cm;

                    if(dataManager != null)
                    {
                        dataManager.ItemChanged += DataManager_ItemChanged;
                        dataManager.PositionChanged += DataManager_PositionChanged;
                    }
                }

                if(dataManager != null && (displayMemberChanged ||dataSourceChanged) &&
                  displayMember.BindingMember.Length != 0 && !this.BindingMemberInfoInDataManager(displayMember))
                    throw new ArgumentException(LR.GetString("ExWrongDisplayMember"));

                if(dataManager != null && (dataSourceChanged || displayMemberChanged || force))
                {
                    DataManager_ItemChanged(dataManager, -1);
                    suppressRefreshItems = true;
                }
            }

            if(dataSourceChanged)
            {
                this.OnDataSourceChanged(EventArgs.Empty);
                suppressRefreshItems = true;
            }

            if(displayMemberChanged)
                this.OnDisplayMemberChanged(EventArgs.Empty);

            suppressRefreshItems = false;
        }

        /// <summary>
        /// This is used to perform item searches
        /// </summary>
        /// <param name="searchString">The string for which to search</param>
        /// <param name="items">The item collection to search</param>
        /// <param name="startIndex">The item after which to start the search</param>
        /// <param name="exact">True for an exact match search, false to find the first item that starts with the
        /// specified text.</param>
        private int FindStringInternal(string searchString, IList items, int startIndex, bool exact)
        {
            bool found;

            if(searchString == null || items == null)
                return -1;

            int length = searchString.Length, idx = startIndex;

            if(startIndex < -1 || startIndex >= items.Count - 1)
                idx = startIndex = -1;

            while(true)
            {
                idx++;

                if(exact)
                    found = String.Equals(searchString, this.GetItemText(items[idx]), StringComparison.OrdinalIgnoreCase);
                else
                {
                    found = String.Compare(searchString, 0, this.GetItemText(items[idx]), 0, length,
                        StringComparison.OrdinalIgnoreCase) == 0;
                }

                if(found)
                    return idx;

                // If we hit the end loop back to the start
                if(idx == items.Count - 1)
                    idx = -1;

                // If we've been all the way through, give up
                if(idx == startIndex)
                    return -1;
            }
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to refresh the data source information when the binding context is changed
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnBindingContextChanged(EventArgs e)
        {
            if(!suppressRefreshItems)
                this.SetDataConnection(dataSource, displayMember, true);

            base.OnBindingContextChanged(e);
        }

        /// <summary>
        /// This is used to return the property value for the display member
        /// </summary>
        /// <param name="item">The item from which to get the info</param>
        /// <returns>The found property value or the item itself if not found</returns>
        /// <overloads>There are two overloads for this method</overloads>
        protected object FilterItemOnProperty(object item)
        {
            return this.FilterItemOnProperty(item, displayMember.BindingField);
        }

        /// <summary>
        /// This is used to return the property value for the specified property name
        /// </summary>
        /// <param name="item">The item from which to get the info</param>
        /// <param name="field">The field on which to get the info</param>
        /// <returns>The found property value or the item itself if not found</returns>
        protected object FilterItemOnProperty(object item, string field)
        {
            if(item != null && field != null && field.Length > 0)
            {
                try
                {
                    PropertyDescriptor pd;

                    if(dataManager != null)
                        pd = dataManager.GetItemProperties().Find(field, true);
                    else
                        pd = TypeDescriptor.GetProperties(item).Find(field, true);

                    if(pd != null)
                        item = pd.GetValue(item);
                }
                catch(Exception ex)
                {
                    // Should this really eat exceptions?  The .NET ListControl class does.  We'll log them for
                    // debugging purposes for the time being.
                    if(System.Diagnostics.Debugger.IsAttached)
                    {
                        // Can't use System.Diagnostics.Debug as it gets excluded from the release build.  This
                        // does the same thing though but is compiled into the release build.
                        using(var dtl = new System.Diagnostics.DefaultTraceListener())
                        {
                            dtl.WriteLine(LR.GetString("ExFilterItemOnProp", this.Name, ex.Message));
                        }
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// This is used to refresh an item in the collection from the data source
        /// </summary>
        /// <param name="index">The index of the item to refresh</param>
        protected virtual void RefreshItem(int index)
        {
            this.Items.SetItemInternal(index, this.Items[index]);
        }

        /// <summary>
        /// This is called to refresh the items in the collection
        /// </summary>
        protected virtual void RefreshItems()
        {
            int currentIdx = this.SelectedIndex;
            BaseListControl.ObjectCollection collection = itemsCollection;

            this.BeginInit();

            object[] items = null;

            if(dataManager != null && dataManager.Count != -1)
            {
                items = new object[dataManager.Count];

                for(int idx = 0; idx < items.Length; idx++)
                    items[idx] = dataManager.List[idx];
            }
            else
                if(collection != null)
                {
                    items = new object[collection.Count];
                    collection.CopyTo(items, 0);
                }

            if(itemsCollection != null)
            {
                itemsCollection.ClearInternal(false);

                if(items != null)
                    itemsCollection.AddRangeInternal(items);
            }

            if(dataManager != null)
            {
                if(this.SelectedIndex != -1)
                    this.SelectedIndex = dataManager.Position;
            }
            else
                this.SelectedIndex = currentIdx;

            this.EndInit();
        }

        /// <summary>
        /// This is used to store an item in the collection from the data source
        /// </summary>
        /// <param name="index">The index of the item to store</param>
        /// <param name="item">The item to store</param>
        protected virtual void SetItemCore(int index, object item)
        {
            this.Items.SetItemInternal(index, item);
        }

        /// <summary>
        /// This is used to store items in the collection from the data source
        /// </summary>
        /// <param name="items">The items to store</param>
        protected virtual void SetItemsCore(IList items)
        {
            this.BeginInit();
            this.Items.ClearInternal(false);
            this.Items.AddRangeInternal(items);

            if(dataManager != null && this.SelectedIndex != -1)
            {
                this.SelectedIndex = dataManager.Position;

                if(!selectedValueChangedFired)
                    this.OnSelectedValueChanged(EventArgs.Empty);
            }
            else
                this.SelectedIndex = -1;

            this.UpdateText();
            this.EndInit();
        }

        /// <summary>
        /// This is called to update the text in the list control
        /// </summary>
        /// <remarks>By default, it does nothing.</remarks>
        protected virtual void UpdateText()
        {
            if(this.SelectedIndex != -1)
            {
                object oItem = this.Items[this.SelectedIndex];

                if(oItem != null)
                    this.Text = this.GetItemText(oItem);
            }
            else
                this.Text = null;

            this.Invalidate();
        }

        /// <summary>
        /// This can be called to force the control to refresh any related sub-controls when the data source,
        /// items within it, or other properties change that may affect them.
        /// </summary>
        /// <remarks>The base class implementation does nothing except raise the <see cref="SubControlsRefreshed"/>
        /// event.</remarks>
        public virtual void RefreshSubControls()
        {
            OnSubControlsRefreshed(EventArgs.Empty);
        }

        /// <summary>
        /// This is called to find an item in the data source
        /// </summary>
        /// <param name="key">The item to find in the data source.  The <see cref="ValueMember"/> is searched for
        /// this value if one is specified.  If not, the item text is searched for the value.</param>
        /// <returns>The index of the found item or -1 if not found</returns>
        /// <exception cref="ArgumentNullException">This is thrown if the item to find is null</exception>
        public int Find(object key)
        {
            int idx;

            if(key == null)
                throw new ArgumentNullException(nameof(key), LR.GetString("ExNullParameter"));

            string valueMemberField = valueMember.BindingField;

            if(valueMemberField.Length == 0)
                return this.FindStringInternal(key.ToString(), this.Items, -1, true);

            PropertyDescriptorCollection coll = dataManager.GetItemProperties();
            PropertyDescriptor prop = coll.Find(valueMemberField, true);

            if(prop == null)
                return -1;

            if(dataManager.List is IBindingList bl && bl.SupportsSearching)
            {
                idx = bl.Find(prop, key);

                // Don't know why but on lists with new rows added, the above occasionally returns a weird
                // negative number.  If so, just search the list manually which does find it.
                if(idx >= -1)
                    return idx;
            }

            for(idx = 0; idx < dataManager.List.Count; idx++)
                if(key.Equals(prop.GetValue(dataManager.List[idx])))
                    return idx;

            return -1;
        }

        /// <summary>
        /// Finds the first item in the list control that starts with the specified string.  The search is not
        /// case-sensitive.
        /// </summary>
        /// <param name="searchText">The string for which to search</param>
        /// <returns>The zero-based index of the first item found.  Returns -1 if no match is found.</returns>
        /// <overloads>There are two overloads for this method</overloads>
        public int FindString(string searchText)
        {
            return this.FindString(searchText, -1);
        }

        /// <summary>
        /// Finds the first item after the given index which starts with the given string. The search is not
        /// case-sensitive.
        /// </summary>
        /// <param name="searchText">The string for which to search.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched.  Set
        /// to -1 to search from the beginning of the control.</param>
        /// <returns>The zero-based index of the first item found.  Returns -1 if no match is found.</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
        /// bounds of the item collection and it is not -1.</exception>
        public int FindString(string searchText, int startIndex)
        {
            if(searchText == null || itemsCollection == null || itemsCollection.Count == 0)
                return -1;

            if(startIndex < -1 || startIndex >= itemsCollection.Count)
                throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, LR.GetString("ExInvalidItemIndex"));

            return FindStringInternal(searchText, this.Items, startIndex, false);
        }

        /// <summary>
        /// Finds the first item in the list control that matches the specified string. The search is not
        /// case-sensitive.
        /// </summary>
        /// <param name="searchText">The string for which to search</param>
        /// <returns>The zero-based index of the first item found.  Returns -1 if no match is found.</returns>
        /// <overloads>There are two overloads for this method</overloads>
        public int FindStringExact(string searchText)
        {
            return this.FindStringExact(searchText, -1);
        }

        /// <summary>
        /// Finds the first item after the given index that matches the given string. The search is not
        /// case-sensitive.
        /// </summary>
        /// <param name="searchText">The string for which to search.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched.  Set
        /// to -1 to search from the beginning of the control.</param>
        /// <returns>The zero-based index of the first item found.  Returns -1 if no match is found.</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
        /// bounds of the item collection and it is not -1.</exception>
        public int FindStringExact(string searchText, int startIndex)
        {
            if(searchText == null || itemsCollection == null || itemsCollection.Count == 0)
                return -1;

            if(startIndex < -1 || startIndex >= itemsCollection.Count)
                throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, LR.GetString("ExInvalidItemIndex"));

            return FindStringInternal(searchText, this.Items, startIndex, true);
        }

        /// <summary>
        /// This is used to get the display member value for the specified item
        /// </summary>
        /// <param name="item">The item for which to get the display value</param>
        /// <returns>The display text for the specified item</returns>
        public string GetItemText(object item)
        {
            item = this.FilterItemOnProperty(item, displayMember.BindingField);

            if(item == null)
                return String.Empty;

            return Convert.ToString(item, CultureInfo.CurrentCulture);
        }
        #endregion
    }
}
