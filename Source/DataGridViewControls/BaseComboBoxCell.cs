//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : BaseComboBoxCell.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/22/2014
// Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains an data grid view cell object that acts as an abstract base class for the combo box cells
// derived from it.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 06/07/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view cell type acts as an abstract base class for the combo box cells derived from it
    /// </summary>
    public abstract class BaseComboBoxCell : BaseDataGridViewCell
    {
        #region Nested ItemComparer class
        //=====================================================================

        /// <summary>
        /// This is a custom comparer class for the object collection so that items in the collection can be
        /// sorted in ascending or descending order.
        /// </summary>
        private sealed class ItemComparer : IComparer
        {
            //=================================================================

            private BaseComboBoxCell owner;

            //=================================================================

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="cell">The cell to which the comparer belongs.</param>
            public ItemComparer(BaseComboBoxCell cell)
            {
                owner = cell;
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
                ListSortOrder order = owner.SortOrder;

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

                string text1 = owner.GetItemDisplayText(x);
                string text2 = owner.GetItemDisplayText(y);

                if(order == ListSortOrder.Ascending)
                    return Application.CurrentCulture.CompareInfo.Compare(text1, text2, CompareOptions.StringSort);

                return Application.CurrentCulture.CompareInfo.Compare(text2, text1, CompareOptions.StringSort);
            }
        }
        #endregion

        #region Nested ObjectCollection class
        //=====================================================================

        /// <summary>
        /// This object collection is used to hold items for the list controls and is sortable in ascending or
        /// descending order.
        /// </summary>
        [ListBindable(false)]
        public sealed class ObjectCollection : IList, ICollection, IEnumerable
        {
            #region Private data members
            //=================================================================

            private IComparer comparer;
            private ArrayList innerList;
            private BaseComboBoxCell owner;
            #endregion

            #region Properties

            /// <summary>
            /// This is used to get whether or not the collection is synchronized
            /// </summary>
            /// <value>Always returns false (not synchronized)</value>
            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            /// <summary>
            /// Returns an object that can be used to synchronize the collection
            /// </summary>
            /// <value>Always returns a reference to itself.</value>
            object ICollection.SyncRoot
            {
                get { return this; }
            }

            /// <summary>
            /// This is used to get whether or not the collection is of a fixed size
            /// </summary>
            /// <value>Always returns false as the collection size varies</value>
            bool IList.IsFixedSize
            {
                get { return false; }
            }

            /// <summary>
            /// This is used to get an <see cref="IComparer"/> instance that can be used to sort the collection
            /// </summary>
            private IComparer Comparer
            {
                get
                {
                    if(comparer == null)
                        comparer = new BaseComboBoxCell.ItemComparer(owner);

                    return comparer;
                }
            }

            /// <summary>
            /// This is used to get a count of the items in the collection
            /// </summary>
            public int Count
            {
                get { return this.InnerList.Count; }
            }

            /// <summary>
            /// This is used to get a reference to the inner list
            /// </summary>
            /// <value>The collection uses an <see cref="ArrayList"/> to hold the collection objects</value>
            internal ArrayList InnerList
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
            /// <value>Always returns false as the collection is always
            /// editable</value>
            public bool IsReadOnly
            {
                get { return false; }
            }

            /// <summary>
            /// This is used to set or get items by index position
            /// </summary>
            /// <param name="index">The index position of the item to set or get</param>
            /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within
            /// the bounds of the collection.</exception>
            /// <exception cref="ArgumentException">This is thrown if an attempt is made to set an item when a
            /// data source is in use.</exception>
            /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within
            /// the bounds of the collection.</exception>
            public object this[int index]
            {
                get
                {
                    if(index < 0 || index >= this.InnerList.Count)
                        throw new ArgumentOutOfRangeException("index", index, LR.GetString("ExItemIndexOutOfRange"));

                    return this.InnerList[index];
                }
                set
                {
                    this.CheckNoDataSource();

                    if(value == null)
                        throw new ArgumentNullException("value", LR.GetString("ExNullParameter"));

                    if(index < 0 || index >= this.InnerList.Count)
                        throw new ArgumentOutOfRangeException("index", index, LR.GetString("ExItemIndexOutOfRange"));

                    this.InnerList[index] = value;
                    this.owner.OnItemsCollectionChanged();
                }
            }
            #endregion

            #region Private methods
            //=================================================================

            /// <summary>
            /// This is used to confirm that the owner does not have a data source assigned to it
            /// </summary>
            /// <exception cref="ArgumentException">This is thrown if the owner does have a data source assigned
            /// to it.</exception>
            private void CheckNoDataSource()
            {
                if(owner.DataSource != null)
                    throw new ArgumentException(LR.GetString("ExDataSourceLocksItems"));
            }

            /// <summary>
            /// This is used to sort the collection
            /// </summary>
            internal void SortInternal()
            {
                this.InnerList.Sort(this.Comparer);
            }
            #endregion

            #region Constructor
            //=====================================================================

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="cell">The cell to which the collection belongs</param>
            public ObjectCollection(BaseComboBoxCell cell)
            {
                owner = cell;
            }
            #endregion

            #region Methods
            //=====================================================================

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
                int index;

                this.CheckNoDataSource();

                if(item == null)
                    throw new ArgumentNullException("item", LR.GetString("ExNullParameter"));

                this.InnerList.Add(item);

                if(owner.SortOrder != ListSortOrder.None)
                {
                    this.InnerList.Sort(this.Comparer);
                    index = this.InnerList.IndexOf(item);
                }
                else
                    index = this.InnerList.Count - 1;

                this.owner.OnItemsCollectionChanged();
                return index;
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
            public void AddRange(params object[] items)
            {
                this.CheckNoDataSource();
                this.AddRangeInternal(items);
                this.owner.OnItemsCollectionChanged();
            }

            /// <summary>
            /// Add a range of items to the collection from another collection
            /// </summary>
            /// <param name="value">The items to add to the collection</param>
            public void AddRange(BaseComboBoxCell.ObjectCollection value)
            {
                this.CheckNoDataSource();
                this.AddRangeInternal(value);
                this.owner.OnItemsCollectionChanged();
            }

            /// <summary>
            /// Add a range of items to the collection from an <see cref="IList"/>
            /// </summary>
            /// <exception cref="ArgumentNullException">This is thrown if the items reference is null or any
            /// item in the collection is null.</exception>
            internal void AddRangeInternal(IList items)
            {
                if(items == null)
                    throw new ArgumentNullException("items", LR.GetString("ExNullItems"));

                foreach(object o in items)
                    if(o == null)
                        throw new ArgumentNullException("items", LR.GetString("ExNullItems"));

                this.InnerList.AddRange(items);

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
                if(this.InnerList.Count > 0)
                {
                    this.CheckNoDataSource();
                    this.InnerList.Clear();
                    this.owner.OnItemsCollectionChanged();
                }
            }

            /// <summary>
            /// Clear all items from the collection
            /// </summary>
            internal void ClearInternal()
            {
                this.InnerList.Clear();
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
            /// <param name="destination">The destination array</param>
            /// <param name="startIndex">The starting index from which to copy</param>
            public void CopyTo(object[] destination, int startIndex)
            {
                this.InnerList.CopyTo(destination, startIndex);
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
            /// Get the index of the specified value in the collection
            /// </summary>
            /// <param name="value">The value for which to get the index</param>
            /// <returns>The index of the item or -1 if not found</returns>
            /// <exception cref="ArgumentNullException">This is thrown if the item is null</exception>
            public int IndexOf(object value)
            {
                if(value == null)
                    throw new ArgumentNullException("value", LR.GetString("ExNullParameter"));

                return this.InnerList.IndexOf(value);
            }

            /// <summary>
            /// Insert a value into the collection at the specified index
            /// </summary>
            /// <param name="index">The index at which to insert the item</param>
            /// <param name="value">The value to insert</param>
            /// <remarks>If the collection is sorted, the value is added to the collection and the collection is
            /// resorted.  The value may or may not end up at the requested index.</remarks>
            /// <exception cref="ArgumentNullException">This is thrown if the value is null</exception>
            /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index value is not within the
            /// bounds of the collection.</exception>
            /// <exception cref="ArgumentException">This is thrown if an attempt is made to add a value to the
            /// collection when a data source is in use.</exception>
            public void Insert(int index, object value)
            {
                this.CheckNoDataSource();

                if(value == null)
                    throw new ArgumentNullException("value", LR.GetString("ExNullParameter"));

                if(index < 0 || index > this.InnerList.Count)
                    throw new ArgumentOutOfRangeException("index", index, LR.GetString("ExItemIndexOutOfRange"));

                if(owner.SortOrder != ListSortOrder.None)
                    this.Add(value);
                else
                {
                    this.InnerList.Insert(index, value);
                    this.owner.OnItemsCollectionChanged();
                }
            }

            /// <summary>
            /// Remove the specified value from the collection
            /// </summary>
            /// <param name="value">The value to remove</param>
            public void Remove(object value)
            {
                int index = this.InnerList.IndexOf(value);

                if(index != -1)
                    this.RemoveAt(index);
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
                this.CheckNoDataSource();

                if(index < 0 || index >= this.InnerList.Count)
                    throw new ArgumentOutOfRangeException("index", index, LR.GetString("ExItemIndexOutOfRange"));

                this.InnerList.RemoveAt(index);
                this.owner.OnItemsCollectionChanged();
            }

            /// <summary>
            /// <see cref="ICollection"/> implementation to copy items to an array
            /// </summary>
            /// <param name="destination">The destination array</param>
            /// <param name="index">The index at which to start copying</param>
            void ICollection.CopyTo(Array destination, int index)
            {
                this.InnerList.CopyTo(destination, index);
            }
            #endregion
        }
        #endregion

        #region Private data members
        //=====================================================================

        // Static type values and flags
        private static Type defaultFormattedValueType = typeof(string);
        private static Type defaultValueType = typeof(object);

        private static bool mouseInDropDownButtonBounds;

        // Combo box properties
        private bool createItemsFromDataSource, dataSourceNeedsInitializing, ignoreNextClick,
            dispStyleCurrentCellOnly;
        private int maxDropDownItems;
        private FlatStyle flatStyle;
        private ListSortOrder sortOrder;
        private DataGridViewComboBoxDisplayStyle displayStyle;

        // Data binding stuff
        private object dataSource;
        private CurrencyManager dataManager;
        private string displayMember, valueMember;
        private PropertyDescriptor displayPropDesc, valuePropDesc;
        private ObjectCollection cellItems;

        // This is used to call an internal method in the base class
        private MethodInfo miParseFormattedValueInternal;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get a reference to the <see cref="CurrencyManager"/> associated with this control
        /// </summary>
        /// <remarks>This property is valid if the <see cref="DataSource"/> property is set. If this is not a
        /// data-bound control, the default is a null reference. </remarks>
        protected CurrencyManager DataManager
        {
            get { return this.GetDataManager(base.DataGridView); }
            set { dataManager = value; }
        }

        /// <summary>
        /// This is used to get or set the editing combo box control.
        /// </summary>
        protected BaseComboBox EditingComboBox { get; set; }

        /// <summary>
        /// Gets or sets a string that specifies the property of the data source whose contents you want to
        /// display.
        /// </summary>
        /// <value>The default is an empty string.  If not set, the object's <c>ToString()</c> method is used to
        /// get the display value.</value>
        /// <exception cref="ArgumentException">This is thrown if the display member cannot be found in the data
        /// source.</exception>
        public string DisplayMember
        {
            get { return displayMember; }
            set
            {
                this.DisplayMemberInternal = value;

                if(this.OwnsEditingComboBox(base.RowIndex))
                {
                    this.EditingComboBox.DisplayMember = displayMember;
                    this.InitializeComboBoxText();
                }
                else
                    base.OnCommonChange();
            }
        }

        /// <summary>
        /// Gets or sets a string that specifies the property of the data source from which to draw the value
        /// </summary>
        /// <value>The default value is an empty string.  Specify a value to bind the data to a property.  Clear
        /// it by setting it to an empty string or null.</value>
        /// <exception cref="ArgumentException">This is thrown if the value member cannot be found in the data
        /// source.</exception>
        public string ValueMember
        {
            get { return valueMember; }
            set
            {
                this.ValueMemberInternal = value;

                if(this.OwnsEditingComboBox(base.RowIndex))
                {
                    this.EditingComboBox.ValueMember = valueMember;
                    this.InitializeComboBoxText();
                }
                else
                    base.OnCommonChange();
            }
        }

        /// <summary>
        /// This gets or sets the data source for the list control
        /// </summary>
        /// <value><para>The data source object must support the <see cref="IList"/> interface such as a
        /// <see cref="System.Data.DataSet"/> or an <see cref="Array"/>.  The default is null and it will use
        /// whatever is in the <see cref="Items"/> collection instead.  If using a data source, set the
        /// <see cref="DisplayMember"/> and <see cref="ValueMember"/> properties too.  Setting this property
        /// to null also clears the <see cref="DisplayMember"/> property.</para>
        /// 
        /// <para>A data source cannot be used with the <see cref="SortOrder"/> property.  If a non-null data
        /// source is set, the <c>SortOrder</c> property is set to <c>None</c> automatically.</para></value>
        /// <exception cref="ArgumentException">This is thrown if the data source does not support the
        /// <see cref="IList"/> interface.</exception>
        public object DataSource
        {
            get { return dataSource; }
            set
            {
                if(value != null && !(value is IList) && !(value is IListSource))
                    throw new ArgumentException(LR.GetString("ExBadDataSource"));

                if(dataSource != value)
                {
                    dataManager = null;
                    this.UnwireDataSource();
                    dataSource = value;
                    this.WireDataSource(value);
                    createItemsFromDataSource = true;

                    try
                    {
                        this.InitializeDisplayMemberPropertyDescriptor(displayMember);
                    }
                    catch(Exception)
                    {
                        // Eat the exception.  It could be that the user changed the data source before setting
                        // the new display member.
                        this.DisplayMemberInternal = null;
                    }

                    try
                    {
                        this.InitializeValueMemberPropertyDescriptor(valueMember);
                    }
                    catch(Exception)
                    {
                        // As above
                        this.ValueMemberInternal = null;
                    }

                    if(value == null)
                        this.DisplayMemberInternal = this.ValueMemberInternal = null;

                    if(this.OwnsEditingComboBox(base.RowIndex))
                    {
                        this.EditingComboBox.DataSource = value;
                        this.InitializeComboBoxText();
                    }
                    else
                        base.OnCommonChange();
                }
            }
        }

        /// <summary>
        /// This can be used to determine whether or not the cell has an items collection
        /// </summary>
        public bool HasItemCollection
        {
            get { return cellItems != null; }
        }

        /// <summary>
        /// Gets the objects that represent the selection displayed in the drop-down list
        /// </summary>
        public ObjectCollection Items
        {
            get { return this.GetItems(base.DataGridView); }
        }

        /// <summary>
        /// Gets or sets a value that determines how the combo box is displayed when it is not in edit mode
        /// </summary>
        /// <value>The default is to show a drop-down button</value>
        public DataGridViewComboBoxDisplayStyle DisplayStyle
        {
            get { return displayStyle; }
            set
            {
                if(displayStyle != value)
                {
                    displayStyle = value;

                    if(base.DataGridView != null)
                        if(base.RowIndex != -1)
                            base.DataGridView.InvalidateCell(this);
                        else
                            base.DataGridView.InvalidateColumn(base.ColumnIndex);
                }
            }
        }

        /// <summary>
        /// Get or set whether the display style applies only to the current cell
        /// </summary>
        /// <value>The default is false and the style applies to all cells.</value>
        public bool DisplayStyleForCurrentCellOnly
        {
            get { return dispStyleCurrentCellOnly; }
            set
            {
                if(dispStyleCurrentCellOnly != value)
                {
                    dispStyleCurrentCellOnly = value;

                    if(base.DataGridView != null)
                        if(base.RowIndex != -1)
                            base.DataGridView.InvalidateCell(this);
                        else
                            base.DataGridView.InvalidateColumn(base.ColumnIndex);
                }
            }
        }

        /// <summary>
        /// Gets or sets the flat style appearance of the cell
        /// </summary>
        /// <value>The default is <c>Standard</c></value>
        public FlatStyle FlatStyle
        {
            get { return flatStyle; }
            set
            {
                if(flatStyle != value)
                {
                    flatStyle = value;
                    base.OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of items to be shown in the drop-down portion of the combo box when
        /// first displayed.  It also controls how many items are skipped when paging up and down.
        /// </summary>
        /// <value>The value must be between 1 and 100.  The value is only used the first time the drop-down
        /// portion is shown.  The drop-down is resizable and it will remember the last used size until it is
        /// disposed.</value>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the value is not between 1 and 100.</exception>
        public int MaxDropDownItems
        {
            get { return maxDropDownItems; }
            set
            {
                if(value < 1 || value > 100)
                    throw new ArgumentOutOfRangeException("value", value, LR.GetString("ExInvalidMaxDropDownItems"));

                maxDropDownItems = value;

                if(this.OwnsEditingComboBox(base.RowIndex))
                    this.EditingComboBox.MaxDropDownItems = value;
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
        public ListSortOrder SortOrder
        {
            get { return sortOrder; }
            set
            {
                if(sortOrder != value)
                {
                    if(value != ListSortOrder.None)
                    {
                        if(dataSource != null)
                            throw new ArgumentException(LR.GetString("ExNoSortWithDataSource"));

                        sortOrder = value;
                        this.Items.SortInternal();
                    }
                    else
                        sortOrder = value;

                    if(this.OwnsEditingComboBox(base.RowIndex))
                        this.EditingComboBox.SortOrder = value;
                }
            }
        }

        /// <summary>
        /// Gets the class type of the formatted value associated with the cell
        /// </summary>
        /// <value>This always returns <see cref="String"/>.</value>
        public override Type FormattedValueType
        {
            get { return defaultFormattedValueType; }
        }

        /// <summary>
        /// Gets or sets the data type of the values in the cell
        /// </summary>
        /// <value>This returns a <see cref="Type"/> representing the data type of the value in the cell</value>
        public override Type ValueType
        {
            get
            {
                if(valuePropDesc != null)
                    return valuePropDesc.PropertyType;

                if(displayPropDesc != null)
                    return displayPropDesc.PropertyType;

                Type valueType = base.ValueType;

                if(valueType != null)
                    return valueType;

                return defaultValueType;
            }
        }
        #endregion

        #region Private properties
        //=====================================================================

        /// <summary>
        /// This is used to set the display member without raising the change event
        /// </summary>
        private string DisplayMemberInternal
        {
            get { return displayMember; }
            set
            {
                if(value == null)
                    value = String.Empty;

                displayMember = value;
                this.InitializeDisplayMemberPropertyDescriptor(displayMember);
            }
        }

        /// <summary>
        /// This is used internally to set the value member without raising the change event
        /// </summary>
        private string ValueMemberInternal
        {
            get { return valueMember; }
            set
            {
                if(value == null)
                    value = String.Empty;

                valueMember = value;
                this.InitializeValueMemberPropertyDescriptor(valueMember);
            }
        }

        /// <summary>
        /// This is used to set the display style member without raising the change event
        /// </summary>
        internal DataGridViewComboBoxDisplayStyle DisplayStyleInternal
        {
            get { return displayStyle; }
            set { displayStyle = value; }
        }

        /// <summary>
        /// This is used to set the "display style for current cell only" member without raising the change event
        /// </summary>
        internal bool DisplayStyleForCurrentCellOnlyInternal
        {
            get { return dispStyleCurrentCellOnly; }
            set { dispStyleCurrentCellOnly = value; }
        }

        /// <summary>
        /// This is used to set the flat style internally so that it doesn't raise the change event
        /// </summary>
        internal FlatStyle FlatStyleInternal
        {
            get { return flatStyle; }
            set { flatStyle = value; }
        }

        /// <summary>
        /// This is used to get the display type using the display member or value member based on which is set
        /// </summary>
        private Type DisplayType
        {
            get
            {
                if(displayPropDesc != null)
                    return displayPropDesc.PropertyType;

                if(valuePropDesc != null)
                    return valuePropDesc.PropertyType;

                return defaultFormattedValueType;
            }
        }

        /// <summary>
        /// This is used to get the display type converter using the display member or value member based on
        /// which is set.
        /// </summary>
        private TypeConverter DisplayTypeConverter
        {
            get
            {
                if(base.DataGridView != null)
                    return DataGridViewHelper.GetCachedTypeConverter(base.DataGridView, this.DisplayType);

                return TypeDescriptor.GetConverter(this.DisplayType);
            }
        }

        /// <summary>
        /// This is used to get or set the column for the cell template
        /// </summary>
        internal BaseComboBoxColumn TemplateComboBoxColumn { get; set; }

        #endregion

        #region Private methods
        //=====================================================================

        /// <summary>
        /// This is used to see if the drop-down list should be shown and, if so, it will show it
        /// </summary>
        /// <param name="x">The mouse click X coordinate</param>
        /// <param name="y">The mouse click coordinate</param>
        /// <param name="rowIndex">The current row index</param>
        private void CheckDropDownList(int x, int y, int rowIndex)
        {
            DataGridViewAdvancedBorderStyle absPlaceholder = new DataGridViewAdvancedBorderStyle();
            DataGridViewAdvancedBorderStyle abs = base.AdjustCellBorderStyle(
                base.DataGridView.AdvancedCellBorderStyle, absPlaceholder, false, false, false, false);
            DataGridViewCellStyle cellStyle = base.GetInheritedStyle(null, rowIndex, false);
            Rectangle r = base.BorderWidths(abs);

            r.X += cellStyle.Padding.Left;
            r.Y += cellStyle.Padding.Top;
            r.Width += cellStyle.Padding.Right;
            r.Height += cellStyle.Padding.Bottom;

            Size size = base.GetSize(rowIndex);
            Size size2 = new Size((size.Width - r.X) - r.Width, (size.Height - r.Y) - r.Height);

            int num = Math.Min(this.GetDropDownButtonHeight(DataGridViewHelper.CachedGraphics(base.DataGridView),
                cellStyle), size2.Height - 2);
            int num2 = Math.Min(SystemInformation.HorizontalScrollBarThumbWidth, (size2.Width - 6) - 1);

            if(num > 0 && num2 > 0 && y >= r.Y + 1 && y <= r.Y + num + 1)
                if(base.DataGridView.RightToLeft == RightToLeft.Yes)
                {
                    if(x >= r.X + 1 && x <= r.X + num2 + 1)
                        this.EditingComboBox.DroppedDown = true;
                }
                else
                    if(x >= size.Width - r.Width - num2 - 1 && x <= size.Width - r.Width - 1)
                        this.EditingComboBox.DroppedDown = true;
        }

        /// <summary>
        /// This is used to clear the data source when it is disposed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataSource_Disposed(object sender, EventArgs e)
        {
            this.DataSource = null;
        }

        /// <summary>
        /// This is used to set the display and value member property descriptors once the data source is
        /// initialized.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataSource_Initialized(object sender, EventArgs e)
        {
            ISupportInitializeNotification notification = dataSource as ISupportInitializeNotification;

            if(notification != null)
                notification.Initialized -= this.DataSource_Initialized;

            dataSourceNeedsInitializing = false;

            this.InitializeDisplayMemberPropertyDescriptor(displayMember);
            this.InitializeValueMemberPropertyDescriptor(valueMember);
        }

        /// <summary>
        /// This is used to get a currency manager for the currently defined data source
        /// </summary>
        /// <param name="dataGridView">The owning data grid view</param>
        /// <returns>The currency manager if there is one or null</returns>
        private CurrencyManager GetDataManager(DataGridView dataGridView)
        {
            if(dataManager == null && dataSource != null && dataGridView != null &&
              dataGridView.BindingContext != null && dataSource != Convert.DBNull)
            {
                ISupportInitializeNotification notification = dataSource as ISupportInitializeNotification;

                if(notification != null && !notification.IsInitialized)
                {
                    if(!dataSourceNeedsInitializing)
                    {
                        notification.Initialized += this.DataSource_Initialized;
                        dataSourceNeedsInitializing = true;
                    }

                    return dataManager;
                }

                dataManager = (CurrencyManager)dataGridView.BindingContext[dataSource];
            }

            return dataManager;
        }

        /// <summary>
        /// This is used to get the height of the drop-down button
        /// </summary>
        /// <param name="graphics">The graphics object</param>
        /// <param name="cellStyle">The current cell style</param>
        /// <returns>The height of the drop-down button in pixels</returns>
        private int GetDropDownButtonHeight(Graphics graphics, DataGridViewCellStyle cellStyle)
        {
            int height = 4;

            if(flatStyle != FlatStyle.Flat && flatStyle != FlatStyle.Popup && Application.RenderWithVisualStyles)
                height = 6;

            return (DataGridViewCell.MeasureTextHeight(graphics, " ", cellStyle.Font, Int32.MaxValue,
                TextFormatFlags.GlyphOverhangPadding) + height);
        }

        /// <summary>
        /// This is used to get the display text for an item
        /// </summary>
        /// <param name="item">The item for which to get display text</param>
        /// <returns>The display text if there is any or an empty string if there is not</returns>
        internal string GetItemDisplayText(object item)
        {
            object dispValue = this.GetItemDisplayValue(item);

            if(dispValue == null)
                return String.Empty;

            return Convert.ToString(dispValue, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// This is used to get an item display value based on either the display member property descriptor or
        /// the value member property descriptor based on what has been specified.
        /// </summary>
        /// <param name="item">The item for which to get a display value</param>
        /// <returns>The object representing the display value</returns>
        internal object GetItemDisplayValue(object item)
        {
            PropertyDescriptor pd;

            if(displayPropDesc != null)
                return displayPropDesc.GetValue(item);

            if(valuePropDesc != null)
                return valuePropDesc.GetValue(item);

            if(!String.IsNullOrEmpty(displayMember))
            {
                pd = TypeDescriptor.GetProperties(item).Find(displayMember, true);

                if(pd != null)
                    return pd.GetValue(item);
            }

            if(!String.IsNullOrEmpty(valueMember))
            {
                pd = TypeDescriptor.GetProperties(item).Find(valueMember, true);

                if(pd != null)
                    return pd.GetValue(item);
            }

            return item;
        }

        /// <summary>
        /// This is used to get an item value based on either the value member property descriptor or the display
        /// member property descriptor based on what has been specified.
        /// </summary>
        /// <param name="item">The item for which to get a value</param>
        /// <returns>The object representing the value</returns>
        internal object GetItemValue(object item)
        {
            PropertyDescriptor pd;

            if(valuePropDesc != null)
                return valuePropDesc.GetValue(item);

            if(displayPropDesc != null)
                return displayPropDesc.GetValue(item);

            if(!String.IsNullOrEmpty(valueMember))
            {
                pd = TypeDescriptor.GetProperties(item).Find(valueMember, true);

                if(pd != null)
                    return pd.GetValue(item);
            }

            if(!String.IsNullOrEmpty(displayMember))
            {
                pd = TypeDescriptor.GetProperties(item).Find(displayMember, true);

                if(pd != null)
                    return pd.GetValue(item);
            }

            return item;
        }

        /// <summary>
        /// Get the items from the data source when needed
        /// </summary>
        /// <param name="dataGridView">The owning data grid view</param>
        /// <returns>an object collection containing the items</returns>
        internal ObjectCollection GetItems(DataGridView dataGridView)
        {
            if(cellItems == null)
                cellItems = new ObjectCollection(this);

            if(createItemsFromDataSource)
            {
                cellItems.ClearInternal();

                CurrencyManager manager = this.GetDataManager(dataGridView);

                if(manager != null && manager.Count > 0)
                {
                    object[] items = new object[manager.Count];

                    for(int i = 0; i < items.Length; i++)
                        items[i] = manager.List[i];

                    cellItems.AddRangeInternal(items);
                }

                if(manager == null && dataSourceNeedsInitializing)
                    return cellItems;

                createItemsFromDataSource = false;
            }

            return cellItems;
        }

        /// <summary>
        /// This is used to initialize the combo box text
        /// </summary>
        private void InitializeComboBoxText()
        {
            IDataGridViewEditingControl editControl = (IDataGridViewEditingControl)this.EditingComboBox;

            editControl.EditingControlValueChanged = false;

            int rowIndex = editControl.EditingControlRowIndex;

            DataGridViewCellStyle cellStyle = base.GetInheritedStyle(null, rowIndex, false);

            this.EditingComboBox.Text = (string)this.GetFormattedValue(this.GetValue(rowIndex), rowIndex,
                ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
        }

        /// <summary>
        /// This is used to initialize the display member property descriptor
        /// </summary>
        /// <param name="displayMember">The display member field</param>
        private void InitializeDisplayMemberPropertyDescriptor(string displayMember)
        {
            BindingMemberInfo bmi;
            PropertyDescriptor pd;

            if(this.DataManager != null)
                if(String.IsNullOrEmpty(displayMember))
                    displayPropDesc = null;
                else
                {
                    bmi = new BindingMemberInfo(displayMember);
                    this.DataManager = base.DataGridView.BindingContext[dataSource, bmi.BindingPath] as CurrencyManager;
                    pd = this.DataManager.GetItemProperties().Find(bmi.BindingField, true);

                    if(pd == null)
                        throw new ArgumentException(LR.GetString("ExFieldNotFound", displayMember));

                    displayPropDesc = pd;
                }
        }

        /// <summary>
        /// This is used to initialize the value member property descriptor
        /// </summary>
        /// <param name="valueMember">The value member field</param>
        private void InitializeValueMemberPropertyDescriptor(string valueMember)
        {
            BindingMemberInfo bmi;
            PropertyDescriptor pd;

            if(this.DataManager != null)
                if(String.IsNullOrEmpty(valueMember))
                    valuePropDesc = null;
                else
                {
                    bmi = new BindingMemberInfo(valueMember);
                    this.DataManager = base.DataGridView.BindingContext[dataSource, bmi.BindingPath] as CurrencyManager;
                    pd = this.DataManager.GetItemProperties().Find(bmi.BindingField, true);

                    if(pd == null)
                        throw new ArgumentException(LR.GetString("ExFieldNotFound", valueMember));

                    valuePropDesc = pd;
                }
        }

        /// <summary>
        /// This is used to get an item from the combo box's data source
        /// </summary>
        /// <param name="property">The property descriptor for the field to retrieve</param>
        /// <param name="key">The key value to find</param>
        /// <returns>The item found in the data source using the key or null if it was not found</returns>
        private object ItemFromComboBoxDataSource(PropertyDescriptor property, object key)
        {
            CurrencyManager cm;
            IBindingList bl;
            object item, value;
            int idx;

            if(key == null)
                throw new ArgumentNullException("key");

            cm = this.DataManager;
            bl = cm.List as IBindingList;

            if(bl != null && bl.SupportsSearching)
            {
                idx = bl.Find(property, key);

                // Don't know why but on lists with new rows added, the above occasionally returns a weird
                // negative number.  If so, just search the list manually which does find it.
                if(idx >= -1)
                {
                    if(idx != -1)
                        return bl[idx];

                    return null;
                }
            }

            for(idx = 0; idx < cm.List.Count; idx++)
            {
                item = cm.List[idx];
                value = property.GetValue(item);

                if(key.Equals(value))
                    return item;
            }

            return null;
        }

        /// <summary>
        /// This is used to get an item from the combo box's item collection
        /// </summary>
        /// <param name="rowIndex">The current row index</param>
        /// <param name="field">The field to search</param>
        /// <param name="key">The key to find</param>
        /// <returns>The item found in the collection using the key or null if it was not found</returns>
        private object ItemFromComboBoxItems(int rowIndex, string field, object key)
        {
            PropertyDescriptor pd;
            object value, item = null;

            // If we own the combo box, check its selected item for the key value in the specified field
            if(this.OwnsEditingComboBox(rowIndex))
            {
                item = this.EditingComboBox.SelectedItem;
                value = null;

                pd = TypeDescriptor.GetProperties(item).Find(field, true);

                if(pd != null)
                    value = pd.GetValue(item);

                if(value == null || !value.Equals(key))
                    item = null;
            }

            // If not found, check our items for the value in the specified field
            if(item == null)
                foreach(object obj in this.Items)
                {
                    value = null;
                    pd = TypeDescriptor.GetProperties(obj).Find(field, true);

                    if(pd != null)
                        value = pd.GetValue(obj);

                    if(value != null && value.Equals(key))
                    {
                        item = obj;
                        break;
                    }
                }

            // If still not found, compare the selected item if we own the combo box
            if(item == null)
            {
                if(this.OwnsEditingComboBox(rowIndex))
                {
                    item = this.EditingComboBox.SelectedItem;

                    if(item == null || !item.Equals(key))
                        item = null;
                }

                // Use the key if all else fails and its in the collection
                if(item == null && this.Items.Contains(key))
                    item = key;
            }

            return item;
        }

        /// <summary>
        /// This is used to look up the display value in the data source or the items collection using the
        /// display or value property descriptor based on the current settings.
        /// </summary>
        /// <param name="rowIndex">The current row index</param>
        /// <param name="value">The value to find</param>
        /// <param name="displayValue">The object that receives the found display value</param>
        /// <returns>True if found, false if not</returns>
        private bool LookupDisplayValue(int rowIndex, object value, out object displayValue)
        {
            object item = null;

            if(displayPropDesc != null || valuePropDesc != null)
                item = this.ItemFromComboBoxDataSource(valuePropDesc != null ? valuePropDesc : displayPropDesc, value);
            else
                item = this.ItemFromComboBoxItems(rowIndex, String.IsNullOrEmpty(valueMember) ? displayMember :
                        valueMember, value);

            if(item == null)
            {
                displayValue = null;
                return false;
            }

            displayValue = this.GetItemDisplayValue(item);
            return true;
        }

        /// <summary>
        /// This is used by the object collection to notify use when it has changed so that we set the combo box
        /// text and can notify the grid that it may need to resize the column and its rows.
        /// </summary>
        private void OnItemsCollectionChanged()
        {
            if(this.TemplateComboBoxColumn != null)
                this.TemplateComboBoxColumn.OnItemsCollectionChanged();

            if(this.OwnsEditingComboBox(base.RowIndex))
                this.InitializeComboBoxText();
            else
                base.OnCommonChange();
        }

        /// <summary>
        /// This is used to paint the combo box cell when it is not in edit mode
        /// </summary>
        /// <param name="g">The graphics object.</param>
        /// <param name="clipBounds">The cell's clipping bounds.</param>
        /// <param name="cellBounds">The overall cell bounds.</param>
        /// <param name="rowIndex">The row index.</param>
        /// <param name="elementState">The current element state.</param>
        /// <param name="formattedValue">The formatted value.</param>
        /// <param name="errorText">The error text, if any.</param>
        /// <param name="cellStyle">The current cell style.</param>
        /// <param name="advancedBorderStyle">The current border style.</param>
        /// <param name="dropDownButtonRect">A rectangle object that will receive the bounds of the drop-down
        /// button rectangle.</param>
        /// <param name="paintParts">The parts of the cell that should be painted.</param>
        /// <param name="computeContentBounds">True if this should compute the content bounds, false if not.  If
        /// true, the content bounds are returned as the result.</param>
        /// <param name="computeErrorIconBounds">True if this should compute the error icon bounds, false if not.
        /// If true, the content bounds are returned as the result.  The overrides <c>computeContentBounds</c> if
        /// both are set to true.</param>
        /// <param name="paint">True to paint the parts or false if just computing the various part bounds.</param>
        /// <returns>The bounding rectangle</returns>
        private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
          DataGridViewElementStates elementState, object formattedValue, string errorText,
          DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
          out Rectangle dropDownButtonRect, DataGridViewPaintParts paintParts, bool computeContentBounds,
          bool computeErrorIconBounds, bool paint)
        {
            Pen penBorder;
            SolidBrush br;
            Rectangle rect;
            Point p1, p2, p3;
            Point[] points;

            string text;
            int width, height;

            Rectangle resultBounds = Rectangle.Empty;
            ComboBoxState state = ComboBoxState.Normal;
            Point mouseCell = DataGridViewHelper.MouseEnteredCellAddress(base.DataGridView);

            dropDownButtonRect = Rectangle.Empty;

            if(mouseCell.Y == rowIndex && mouseCell.X == base.ColumnIndex && mouseInDropDownButtonBounds)
                state = ComboBoxState.Hot;

            // Calculate some helper values
            Rectangle bw = base.BorderWidths(advancedBorderStyle);
            Rectangle bounds = cellBounds;
            bounds.Offset(bw.X, bw.Y);
            bounds.Width -= bw.Right;
            bounds.Height -= bw.Bottom;
            Point currentCell = base.DataGridView.CurrentCellAddress;

            bool isFlatOrPopup = (flatStyle == FlatStyle.Flat || flatStyle == FlatStyle.Popup);
            bool isPopupAndHot = (flatStyle == FlatStyle.Popup && mouseCell.Y == rowIndex && mouseCell.X == base.ColumnIndex);
            bool isThemed = !isFlatOrPopup && Application.RenderWithVisualStyles;
            bool isCurrentCell = (currentCell.X == base.ColumnIndex) && (currentCell.Y == rowIndex);
            bool isSelected = (elementState & DataGridViewElementStates.Selected) != DataGridViewElementStates.None;
            bool hasEditCtl = isCurrentCell && (base.DataGridView.EditingControl != null);
            bool useComboBoxStyle = (displayStyle == DataGridViewComboBoxDisplayStyle.ComboBox) &&
                ((dispStyleCurrentCellOnly && isCurrentCell) || !dispStyleCurrentCellOnly);
            bool useDisplayStyle = (displayStyle != DataGridViewComboBoxDisplayStyle.Nothing) &&
                ((dispStyleCurrentCellOnly && isCurrentCell) ||
                !dispStyleCurrentCellOnly);

            // Paint the border
            if(paint && (paintParts & DataGridViewPaintParts.Border) != DataGridViewPaintParts.None)
                base.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);

            // Paint the cell padding
            if((paintParts & DataGridViewPaintParts.SelectionBackground) != DataGridViewPaintParts.None && isSelected && !hasEditCtl)
                br = DataGridViewHelper.GetCachedBrush(base.DataGridView, cellStyle.SelectionBackColor);
            else
                br = DataGridViewHelper.GetCachedBrush(base.DataGridView, cellStyle.BackColor);

            if(paint && (paintParts & DataGridViewPaintParts.Background) != DataGridViewPaintParts.None &&
              br.Color.A == 0xFF && bounds.Width > 0 && bounds.Height > 0)
                BaseDataGridViewCell.PaintPadding(g, bounds, cellStyle, br, (base.DataGridView.RightToLeft == RightToLeft.Yes));

            if(cellStyle.Padding != Padding.Empty)
            {
                if(base.DataGridView.RightToLeft == RightToLeft.Yes)
                    bounds.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
                else
                    bounds.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);

                bounds.Width -= cellStyle.Padding.Horizontal;
                bounds.Height -= cellStyle.Padding.Vertical;
            }

            // Draw the control background
            if(paint && bounds.Width > 0 && bounds.Height > 0)
                if(isThemed && useComboBoxStyle)
                {
                    if((paintParts & DataGridViewPaintParts.ContentBackground) != DataGridViewPaintParts.None)
                        ComboBoxRenderer.DrawTextBox(g, bounds, state);

                    if((paintParts & DataGridViewPaintParts.Background) != DataGridViewPaintParts.None &&
                      br.Color.A == 0xFF && bounds.Width > 2 && bounds.Height > 2)
                        g.FillRectangle(br, (int)(bounds.Left + 1), (int)(bounds.Top + 1), (int)(bounds.Width - 2),
                            (int)(bounds.Height - 2));
                }
                else
                    if((paintParts & DataGridViewPaintParts.Background) != DataGridViewPaintParts.None &&
                      br.Color.A == 0xFF)
                        g.FillRectangle(br, bounds.Left, bounds.Top, bounds.Width, bounds.Height);

            width = Math.Min(SystemInformation.HorizontalScrollBarThumbWidth, (bounds.Width - 6) - 1);

            // Draw the control if the cell doesn't contain the editing control
            if(!hasEditCtl)
            {
                if(isThemed || isFlatOrPopup)
                    height = Math.Min(this.GetDropDownButtonHeight(g, cellStyle), bounds.Height - 2);
                else
                    height = Math.Min(this.GetDropDownButtonHeight(g, cellStyle), bounds.Height - 4);

                if(width > 0 && height > 0)
                {
                    if(isThemed || isFlatOrPopup)
                    {
                        rect = new Rectangle((base.DataGridView.RightToLeft == RightToLeft.Yes ? bounds.Left + 1 :
                            bounds.Right - width - 1), bounds.Top + 1, width, height);
                    }
                    else
                        rect = new Rectangle((base.DataGridView.RightToLeft == RightToLeft.Yes ? bounds.Left + 2 :
                            bounds.Right - width - 2), bounds.Top + 2, width, height);

                    dropDownButtonRect = rect;

                    // Draw the content background including the drop-down button and borders
                    if(paint && (paintParts & DataGridViewPaintParts.ContentBackground) != DataGridViewPaintParts.None)
                    {
                        if(useDisplayStyle)
                            if(isFlatOrPopup || !isThemed)
                                g.FillRectangle(SystemBrushes.Control, rect);
                            else
                                ComboBoxRenderer.DrawDropDownButton(g, rect, state);

                        if(!isFlatOrPopup && !isThemed && (useComboBoxStyle || useDisplayStyle))
                        {
                            if(SystemInformation.HighContrast)
                                penBorder = SystemPens.ControlLight;
                            else
                                penBorder = SystemPens.Control;

                            if(useDisplayStyle)
                            {
                                g.DrawLine(penBorder, rect.X, rect.Y, rect.X + rect.Width - 1, rect.Y);
                                g.DrawLine(penBorder, rect.X, rect.Y, rect.X, rect.Y + rect.Height - 1);
                            }

                            if(useComboBoxStyle)
                            {
                                g.DrawLine(penBorder, bounds.X, bounds.Y + bounds.Height - 1,
                                    bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
                                g.DrawLine(penBorder, bounds.X + bounds.Width - 1, bounds.Y,
                                    bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
                            }

                            penBorder = SystemPens.ControlDarkDark;

                            if(useDisplayStyle)
                            {
                                g.DrawLine(penBorder, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width - 1,
                                    rect.Y + rect.Height - 1);
                                g.DrawLine(penBorder, rect.X + rect.Width - 1, rect.Y, rect.X + rect.Width - 1,
                                    rect.Y + rect.Height - 1);
                            }

                            if(useComboBoxStyle)
                            {
                                g.DrawLine(penBorder, bounds.X, bounds.Y, bounds.X + bounds.Width - 2, bounds.Y);
                                g.DrawLine(penBorder, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
                            }

                            penBorder = SystemPens.ControlLightLight;

                            if(useDisplayStyle)
                            {
                                g.DrawLine(penBorder, rect.X + 1, rect.Y + 1, rect.X + rect.Width - 2, rect.Y + 1);
                                g.DrawLine(penBorder, rect.X + 1, rect.Y + 1, rect.X + 1, rect.Y + rect.Height - 2);
                            }

                            penBorder = SystemPens.ControlDark;

                            if(useDisplayStyle)
                            {
                                g.DrawLine(penBorder, rect.X + 1, rect.Y + rect.Height - 2, rect.X + rect.Width - 2,
                                    rect.Y + rect.Height - 2);
                                g.DrawLine(penBorder, rect.X + rect.Width - 2, rect.Y + 1, rect.X + rect.Width - 2,
                                    rect.Y + rect.Height - 2);
                            }
                        }

                        // Drop the drop-down arrow
                        if(width >= 5 && height >= 3 && useDisplayStyle && (isFlatOrPopup || !isThemed))
                            if(isFlatOrPopup)
                            {
                                p1 = new Point(rect.Left + (rect.Width / 2), rect.Top + (rect.Height / 2));
                                p1.X += rect.Width % 2;
                                p1.Y += rect.Height % 2;

                                points = new[]
                                {
                                    new Point(p1.X - 2, p1.Y - 1),
                                    new Point(p1.X + 3, p1.Y - 1),
                                    new Point(p1.X, p1.Y + 2)
                                };

                                g.FillPolygon(SystemBrushes.ControlText, points);
                            }
                            else
                            {
                                rect.X--;
                                rect.Width++;
                                p1 = new Point(rect.Left + ((rect.Width - 1) / 2), rect.Top + ((rect.Height + 4) / 2));
                                p1.X += (rect.Width + 1) % 2;
                                p1.Y += rect.Height % 2;
                                p2 = new Point(p1.X - 3, p1.Y - 4);
                                p3 = new Point(p1.X + 3, p1.Y - 4);

                                points = new Point[] { p2, p3, p1 };
                                g.FillPolygon(SystemBrushes.ControlText, points);
                                g.DrawLine(SystemPens.ControlText, p2.X, p2.Y, p3.X, p3.Y);
                                rect.X++;
                                rect.Width--;
                            }

                        if(isPopupAndHot && useComboBoxStyle)
                        {
                            rect.Y--;
                            rect.Height++;
                            g.DrawRectangle(SystemPens.ControlDark, rect);
                        }
                    }
                }
            }

            // Calculate the value bounds
            Rectangle cellValueBounds = bounds;
            Rectangle textBounds = Rectangle.Inflate(bounds, -2, -2);

            if(useDisplayStyle)
            {
                if(isThemed || isFlatOrPopup)
                {
                    cellValueBounds.Width -= width;
                    textBounds.Width -= width;

                    if(base.DataGridView.RightToLeft == RightToLeft.Yes)
                    {
                        cellValueBounds.X += width;
                        textBounds.X += width;
                    }
                }
                else
                {
                    cellValueBounds.Width -= width + 1;
                    textBounds.Width -= width + 1;

                    if(base.DataGridView.RightToLeft == RightToLeft.Yes)
                    {
                        cellValueBounds.X += width + 1;
                        textBounds.X += width + 1;
                    }
                }
            }

            if(textBounds.Width > 1 && textBounds.Height > 1)
            {
                // Draw the focus rectangle if needed
                if(paint && isCurrentCell && !hasEditCtl &&
                  (paintParts & DataGridViewPaintParts.Focus) != DataGridViewPaintParts.None &&
                  DataGridViewHelper.ShowFocusCues(base.DataGridView) && base.DataGridView.Focused)
                {
                    if(isFlatOrPopup)
                    {
                        rect = textBounds;

                        if(base.DataGridView.RightToLeft == RightToLeft.No)
                            rect.X--;

                        rect.Width++;
                        rect.Y--;
                        rect.Height += 2;
                        ControlPaint.DrawFocusRectangle(g, rect, Color.Empty, br.Color);
                    }
                    else
                        ControlPaint.DrawFocusRectangle(g, textBounds, Color.Empty, br.Color);
                }

                if(isPopupAndHot)
                {
                    bounds.Width--;
                    bounds.Height--;

                    if(!hasEditCtl && paint &&
                      (paintParts & DataGridViewPaintParts.ContentBackground) != DataGridViewPaintParts.None &&
                      useComboBoxStyle)
                    {
                        g.DrawRectangle(SystemPens.ControlDark, bounds);
                    }
                }

                // Draw the text if there is any
                text = formattedValue as string;

                if(text != null)
                {
                    height = (cellStyle.WrapMode == DataGridViewTriState.True) ? 0 : 1;

                    if(base.DataGridView.RightToLeft == RightToLeft.Yes)
                    {
                        textBounds.Offset(0, height);
                        textBounds.Width += 2;
                    }
                    else
                    {
                        textBounds.Offset(-1, height);
                        textBounds.Width++;
                    }

                    textBounds.Height -= height;

                    if(textBounds.Width > 0 && textBounds.Height > 0)
                    {
                        TextFormatFlags flags = BaseDataGridViewCell.ComputeTextFormatFlagsForCellStyleAlignment(
                            base.DataGridView.RightToLeft == RightToLeft.Yes, cellStyle.Alignment, cellStyle.WrapMode);

                        if(!hasEditCtl && paint)
                        {
                            if((paintParts & DataGridViewPaintParts.ContentForeground) != DataGridViewPaintParts.None)
                            {
                                if((flags & TextFormatFlags.SingleLine) != TextFormatFlags.GlyphOverhangPadding)
                                    flags |= TextFormatFlags.EndEllipsis;

                                TextRenderer.DrawText(g, text, cellStyle.Font,
                                    textBounds, isSelected ? cellStyle.SelectionForeColor : cellStyle.ForeColor, flags);
                            }
                        }
                        else
                            if(computeContentBounds)
                                resultBounds = BaseDataGridViewCell.GetTextBounds(textBounds, text, flags,
                                    cellStyle, cellStyle.Font);
                    }
                }

                // Paint the error icon if needed
                if(base.DataGridView.ShowCellErrors && paint &&
                  (paintParts & DataGridViewPaintParts.ErrorIcon) != DataGridViewPaintParts.None)
                {
                    base.PaintErrorIcon(g, clipBounds, cellValueBounds, errorText);

                    if(hasEditCtl)
                        return Rectangle.Empty;
                }
            }

            // Compute and return the requested bounds if necessary
            if(!computeErrorIconBounds)
                return resultBounds;

            if(!String.IsNullOrEmpty(errorText))
                return base.ComputeErrorIconBounds(cellValueBounds);

            return Rectangle.Empty;
        }

        /// <summary>
        /// This is used to wire up the data source events
        /// </summary>
        /// <param name="dataSource">The data source that is in use</param>
        private void WireDataSource(object dataSource)
        {
            IComponent component = dataSource as IComponent;

            if(component != null)
                component.Disposed += this.DataSource_Disposed;
        }

        /// <summary>
        /// This is used to unwire the data source events
        /// </summary>
        private void UnwireDataSource()
        {
            IComponent component = dataSource as IComponent;

            if(component != null)
                component.Disposed -= this.DataSource_Disposed;

            ISupportInitializeNotification notification = dataSource as ISupportInitializeNotification;

            if(notification != null && dataSourceNeedsInitializing)
            {
                notification.Initialized -= this.DataSource_Initialized;
                dataSourceNeedsInitializing = false;
            }
        }

        /// <summary>
        /// This is used internally to get or set whether or not to create items from the data source
        /// </summary>
        private bool CreateItemsFromDataSource
        {
            get { return createItemsFromDataSource; }
            set { createItemsFromDataSource = value; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseComboBoxCell()
        {
            maxDropDownItems = 8;
            displayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            flatStyle = FlatStyle.Standard;
            displayMember = valueMember = String.Empty;

            miParseFormattedValueInternal = base.GetType().GetMethod("ParseFormattedValueInternal",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Clone the cell
        /// </summary>
        /// <returns>A clone of the cell</returns>
        public override object Clone()
        {
            BaseComboBoxCell clone = (BaseComboBoxCell)base.Clone();
            clone.MaxDropDownItems = maxDropDownItems;
            clone.CreateItemsFromDataSource = false;
            clone.DataSource = dataSource;
            clone.DisplayMember = displayMember;
            clone.ValueMember = valueMember;

            if(cellItems != null && dataSource == null && cellItems.Count > 0)
                clone.Items.AddRangeInternal(this.Items.InnerList.ToArray());

            clone.SortOrder = sortOrder;
            clone.FlatStyleInternal = flatStyle;
            clone.DisplayStyleInternal = displayStyle;
            clone.DisplayStyleForCurrentCellOnlyInternal = dispStyleCurrentCellOnly;

            return clone;
        }

        /// <summary>
        /// This removes the cell's editing control from the data grid view
        /// </summary>
        public override void DetachEditingControl()
        {
            this.EditingComboBox = null;
            base.DetachEditingControl();
        }

        /// <summary>
        /// This returns the bounding rectangle that encloses the cell's content area calculated by using the
        /// specified graphics object and cell style.
        /// </summary>
        /// <param name="graphics">The graphics context for the cell</param>
        /// <param name="cellStyle">The cell style</param>
        /// <param name="rowIndex">The index of the parent row</param>
        /// <returns>A rectangle representing the cell content bounds</returns>
        protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle,
          int rowIndex)
        {
            DataGridViewAdvancedBorderStyle dgvabsEffective = new DataGridViewAdvancedBorderStyle();
            DataGridViewElementStates cellState = DataGridViewElementStates.Displayed |
                DataGridViewElementStates.Visible;
            Rectangle cellBounds;
            Rectangle dropDownButtonRect;

            if(cellStyle == null)
                throw new ArgumentNullException("cellStyle");

            if(base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null)
                return Rectangle.Empty;

            object formattedValue = base.GetEditedFormattedValue(rowIndex, DataGridViewDataErrorContexts.Formatting);
            this.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dgvabsEffective, out cellState, out cellBounds);

            return this.PaintPrivate(graphics, cellBounds, cellBounds, rowIndex, cellState, formattedValue, null,
                cellStyle, dgvabsEffective, out dropDownButtonRect, DataGridViewPaintParts.ContentForeground,
                true, false, false);
        }

        /// <summary>
        /// This is used to determine whether or not the given row owns the editing combo box control
        /// </summary>
        /// <param name="rowIndex">The index of the row to check</param>
        /// <returns>True if it owns the control, false if it does not</returns>
        protected bool OwnsEditingComboBox(int rowIndex)
        {
            IDataGridViewEditingControl editControl = this.EditingComboBox as IDataGridViewEditingControl;

            return (rowIndex != -1 && editControl != null && rowIndex == editControl.EditingControlRowIndex);
        }

        /// <summary>
        /// Get the cell state based on the column row states
        /// </summary>
        /// <param name="rowState">The row state</param>
        /// <returns>The combination of the cell and row states</returns>
        protected DataGridViewElementStates CellStateFromColumnRowStates(DataGridViewElementStates rowState)
        {
            DataGridViewElementStates states = DataGridViewElementStates.Selected |
                DataGridViewElementStates.Resizable | DataGridViewElementStates.ReadOnly;
            DataGridViewElementStates states2 = DataGridViewElementStates.Visible |
                DataGridViewElementStates.Frozen | DataGridViewElementStates.Displayed;
            DataGridViewElementStates states3 = base.OwningColumn.State & states;

            states3 |= rowState & states;

            return (states3 | ((base.OwningColumn.State & states2) & (rowState & states2)));
        }

        /// <summary>
        /// Compute the border style, cell state, and cell bounds
        /// </summary>
        /// <param name="rowIndex">The row state</param>
        /// <param name="dgvabsEffective">The effective border style</param>
        /// <param name="cellState">The cell state</param>
        /// <param name="cellBounds">The cell bounds</param>
        protected void ComputeBorderStyleCellStateAndCellBounds(int rowIndex,
            out DataGridViewAdvancedBorderStyle dgvabsEffective, out DataGridViewElementStates cellState,
            out Rectangle cellBounds)
        {
            bool singleVerticalBorderAdded = !base.DataGridView.RowHeadersVisible &&
                (base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single);
            bool singleHorizontalBorderAdded = !base.DataGridView.ColumnHeadersVisible &&
                (base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single);
            DataGridViewAdvancedBorderStyle borderStylePlaceholder = new DataGridViewAdvancedBorderStyle();

            if(rowIndex > -1 && base.OwningColumn != null)
            {
                dgvabsEffective = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle,
                    borderStylePlaceholder, singleVerticalBorderAdded, singleHorizontalBorderAdded,
                    rowIndex == base.FirstDisplayedRowIndex, base.ColumnIndex == base.FirstDisplayedColumnIndex);

                DataGridViewElementStates rowState = base.DataGridView.Rows.GetRowState(rowIndex);
                cellState = this.CellStateFromColumnRowStates(rowState);
                cellState |= base.State;
            }
            else if(base.OwningColumn != null)
            {
                DataGridViewColumn lastColumn = base.DataGridView.Columns.GetLastColumn(
                    DataGridViewElementStates.Visible, DataGridViewElementStates.None);
                bool isLastVisibleColumn = (lastColumn != null) && (lastColumn.Index == base.ColumnIndex);
                dgvabsEffective = base.DataGridView.AdjustColumnHeaderBorderStyle(
                    base.DataGridView.AdvancedColumnHeadersBorderStyle, borderStylePlaceholder,
                    base.ColumnIndex == base.FirstDisplayedColumnIndex, isLastVisibleColumn);
                cellState = base.OwningColumn.State | base.State;
            }
            else if(base.OwningRow != null)
            {
                dgvabsEffective = base.OwningRow.AdjustRowHeaderBorderStyle(
                    base.DataGridView.AdvancedRowHeadersBorderStyle, borderStylePlaceholder,
                    singleVerticalBorderAdded, singleHorizontalBorderAdded,
                    rowIndex == base.FirstDisplayedRowIndex,
                    rowIndex == base.DataGridView.Rows.GetLastRow(DataGridViewElementStates.Visible));
                cellState = base.OwningRow.GetState(rowIndex) | base.State;
            }
            else
            {
                dgvabsEffective = base.DataGridView.AdjustedTopLeftHeaderBorderStyle;
                cellState = base.State;
            }

            cellBounds = new Rectangle(new Point(0, 0), base.GetSize(rowIndex));
        }

        /// <summary>
        /// Returns the bounding rectangle that encloses the cell's error icon, if one is displayed
        /// </summary>
        /// <param name="graphics">The graphics context for the cell</param>
        /// <param name="cellStyle">The cell style</param>
        /// <param name="rowIndex">The index of the cell's parent row</param>
        /// <returns>The bounds of the cell's error icon if displayed or <c>Rectangle.Empty</c> if not</returns>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle,
          int rowIndex)
        {
            DataGridViewAdvancedBorderStyle dgvabsEffective = new DataGridViewAdvancedBorderStyle();
            DataGridViewElementStates cellState = DataGridViewElementStates.Displayed | DataGridViewElementStates.Visible;
            Rectangle cellBounds;
            Rectangle dropDownButtonRect;

            if(cellStyle == null)
                throw new ArgumentNullException("cellStyle");

            if(base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null ||
              !base.DataGridView.ShowCellErrors || String.IsNullOrEmpty(base.GetErrorText(rowIndex)))
                return Rectangle.Empty;

            this.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dgvabsEffective, out cellState, out cellBounds);

            return this.PaintPrivate(graphics, cellBounds, cellBounds, rowIndex, cellState, null,
                base.GetErrorText(rowIndex), cellStyle, dgvabsEffective, out dropDownButtonRect,
                DataGridViewPaintParts.ContentForeground, false, true, false);
        }

        /// <summary>
        /// Gets the formatted value of the cell's data
        /// </summary>
        /// <param name="value">The value to be formatted.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="valueTypeConverter">A <see cref="TypeConverter"/> associated with the value type that
        /// provides custom conversion to the formatted value type, or null if no such custom conversion is
        /// needed.</param>
        /// <param name="formattedValueTypeConverter">A <see cref="TypeConverter"/> associated with the formatted
        /// value type that provides custom conversion from the value type, or null if no such custom conversion
        /// is needed.</param>
        /// <param name="context">A bitwise combination of <see cref="DataGridViewDataErrorContexts"/> values
        /// describing the context in which the formatted value is needed.</param>
        /// <returns>The value of the cell's data after formatting has been applied or null if the cell is not
        /// part of a <see cref="DataGridView"/> control.</returns>
        protected override object GetFormattedValue(object value, int rowIndex,
          ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter,
          TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            string text;
            object displayValue;

            if(valueTypeConverter == null)
                if(valuePropDesc != null)
                    valueTypeConverter = valuePropDesc.Converter;
                else
                    if(displayPropDesc != null)
                        valueTypeConverter = displayPropDesc.Converter;

            if(value == null || (this.ValueType != null && !this.ValueType.IsAssignableFrom(value.GetType()) &&
              value != DBNull.Value))
            {
                if(value == null)
                    return base.GetFormattedValue(null, rowIndex, ref cellStyle, valueTypeConverter,
                        formattedValueTypeConverter, context);

                if(base.DataGridView != null)
                {
                    DataGridViewDataErrorEventArgs e = new DataGridViewDataErrorEventArgs(new FormatException(
                        LR.GetString("ExInvalidCellValue")), base.ColumnIndex, rowIndex, context);

                    base.RaiseDataError(e);

                    if(e.ThrowException)
                        throw e.Exception;
                }

                return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter,
                    formattedValueTypeConverter, context);
            }

            text = value as string;

            if((this.DataManager != null && (valuePropDesc != null || displayPropDesc != null)) ||
              !String.IsNullOrEmpty(valueMember) || !String.IsNullOrEmpty(displayMember))
            {
                if(!this.LookupDisplayValue(rowIndex, value, out displayValue))
                {
                    if(value == DBNull.Value)
                        displayValue = DBNull.Value;
                    else
                        if(text != null && String.IsNullOrEmpty(text) && this.DisplayType == typeof(string))
                            displayValue = String.Empty;
                        else
                            if(base.DataGridView != null)
                            {
                                DataGridViewDataErrorEventArgs e = new DataGridViewDataErrorEventArgs(
                                    new ArgumentException(LR.GetString("ExInvalidCellValue")), base.ColumnIndex,
                                    rowIndex, context);

                                base.RaiseDataError(e);

                                if(e.ThrowException)
                                    throw e.Exception;

                                if(this.OwnsEditingComboBox(rowIndex))
                                {
                                    IDataGridViewEditingControl editControl = (IDataGridViewEditingControl)this.EditingComboBox;
                                    editControl.EditingControlValueChanged = true;
                                    base.DataGridView.NotifyCurrentCellDirty(true);
                                }
                            }
                }

                return base.GetFormattedValue(displayValue, rowIndex, ref cellStyle, this.DisplayTypeConverter,
                    formattedValueTypeConverter, context);
            }

            if(!this.Items.Contains(value) && value != DBNull.Value && (!(value is string) ||
              !String.IsNullOrEmpty(text)))
            {
                if(base.DataGridView != null)
                {
                    DataGridViewDataErrorEventArgs e = new DataGridViewDataErrorEventArgs(
                        new ArgumentException(LR.GetString("ExInvalidCellValue")), base.ColumnIndex, rowIndex,
                        context);

                    base.RaiseDataError(e);

                    if(e.ThrowException)
                        throw e.Exception;
                }

                if(this.Items.Count > 0)
                    value = this.Items[0];
                else
                    value = String.Empty;
            }

            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter,
                formattedValueTypeConverter, context);
        }

        /// <summary>
        /// Calculates the preferred size, in pixels, of the cell
        /// </summary>
        /// <param name="graphics">The graphics context for the cell</param>
        /// <param name="cellStyle">The cell style</param>
        /// <param name="rowIndex">The index of the cell's parent row</param>
        /// <param name="constraintSize">The cell's maximum allowable size</param>
        /// <returns>The preferred cell size in pixels</returns>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle,
          int rowIndex, Size constraintSize)
        {
            TextFormatFlags flags;
            Size size;
            int widthOffset, heightOffset, freeDimension;
            string text;

            if(base.DataGridView == null)
                return new Size(-1, -1);

            if(cellStyle == null)
                throw new ArgumentNullException("cellStyle");

            if(constraintSize.Width == 0)
            {
                if(constraintSize.Height == 0)
                    freeDimension = 0;      // Both free
                else
                    freeDimension = 2;      // Width is free
            }
            else
                freeDimension = 1;          // Height is free

            DataGridViewAdvancedBorderStyle borderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
            DataGridViewAdvancedBorderStyle advancedBorderStyle = this.AdjustCellBorderStyle(
                base.DataGridView.AdvancedCellBorderStyle, borderStylePlaceholder, false, false, false, false);
            Rectangle borderWidths = base.BorderWidths(advancedBorderStyle);

            widthOffset = (borderWidths.Left + borderWidths.Width) + cellStyle.Padding.Horizontal;
            heightOffset = (borderWidths.Top + borderWidths.Height) + cellStyle.Padding.Vertical;
            flags = BaseDataGridViewCell.ComputeTextFormatFlagsForCellStyleAlignment(
                base.DataGridView.RightToLeft == RightToLeft.Yes, cellStyle.Alignment, cellStyle.WrapMode);
            text = this.GetFormattedValue(this.GetValue(rowIndex), rowIndex, ref cellStyle, null, null,
                DataGridViewDataErrorContexts.PreferredSize | DataGridViewDataErrorContexts.Formatting) as string;

            if(!String.IsNullOrEmpty(text))
                size = DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, flags);
            else
                size = DataGridViewCell.MeasureTextSize(graphics, " ", cellStyle.Font, flags);

            switch(freeDimension)
            {
                case 1:
                    size.Width = 0;
                    break;

                case 2:
                    size.Height = 0;
                    break;
            }

            if(freeDimension != 1)
            {
                size.Width += SystemInformation.HorizontalScrollBarThumbWidth + widthOffset + 7;

                if(base.DataGridView.ShowCellErrors)
                    size.Width = Math.Max(size.Width, widthOffset +
                        SystemInformation.HorizontalScrollBarThumbWidth + 21);
            }

            if(freeDimension != 2)
            {
                if(flatStyle == FlatStyle.Flat || flatStyle == FlatStyle.Popup)
                    size.Height += 6;
                else
                    size.Height += 8;

                size.Height += heightOffset;

                if(base.DataGridView.ShowCellErrors)
                    size.Height = Math.Max(size.Height, heightOffset + 19);
            }

            return size;
        }

        /// <summary>
        /// Determines if edit mode should be started based on the given key
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <returns>True to enter edit mode, false to not enter edit mode</returns>
        public override bool KeyEntersEditMode(KeyEventArgs e)
        {
            return (((Char.IsLetterOrDigit((char)((ushort)e.KeyCode)) &&
                (e.KeyCode < Keys.F1 || e.KeyCode > Keys.F24)) ||
                (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide) ||
                (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.OemBackslash) ||
                (e.KeyCode == Keys.Space && !e.Shift) || e.KeyCode == Keys.F4 ||
                ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && e.Alt)) &&
                (!e.Alt || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) &&
                !e.Control) || base.KeyEntersEditMode(e);
        }

        /// <summary>
        /// This is used to reinitialize the data source display and value members when the
        /// <see cref="DataGridView"/> property changes.
        /// </summary>
        protected override void OnDataGridViewChanged()
        {
            if(base.DataGridView != null)
            {
                this.InitializeDisplayMemberPropertyDescriptor(displayMember);
                this.InitializeValueMemberPropertyDescriptor(valueMember);
            }

            base.OnDataGridViewChanged();
        }

        /// <summary>
        /// This is overridden to set the Ignore Next Click flag based on the data grid's
        /// <see cref="DataGridView.EditMode"/> setting.
        /// </summary>
        /// <param name="rowIndex">The row index</param>
        /// <param name="throughMouseClick">True if user action moved the focus, false if it was programmatic</param>
        /// <remarks>If not editing on enter, the next mouse click is ignored as it is the one that will make it
        /// enter edit mode and we don't want it to drop down the list if the button is clicked.</remarks>
        protected override void OnEnter(int rowIndex, bool throughMouseClick)
        {
            if(base.DataGridView != null && throughMouseClick &&
              base.DataGridView.EditMode != DataGridViewEditMode.EditOnEnter)
                ignoreNextClick = true;
        }

        /// <summary>
        /// This is overridden to clear the Ignore Next Click flag when leaving the cell
        /// </summary>
        /// <param name="rowIndex">The row index.</param>
        /// <param name="throughMouseClick">True if user action moved the focus, false if it was programmatic</param>
        protected override void OnLeave(int rowIndex, bool throughMouseClick)
        {
            if(base.DataGridView != null)
                ignoreNextClick = false;
        }

        /// <summary>
        /// This is used to drop down the list if the button is clicked and we are in edit mode
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            if(base.DataGridView != null)
            {
                Point currentCellAddress = base.DataGridView.CurrentCellAddress;

                if(currentCellAddress.X == e.ColumnIndex && currentCellAddress.Y == e.RowIndex)
                    if(ignoreNextClick)
                        ignoreNextClick = false;
                    else
                        if((this.EditingComboBox == null || !this.EditingComboBox.DroppedDown) &&
                          base.DataGridView.EditMode != DataGridViewEditMode.EditProgrammatically &&
                          base.DataGridView.BeginEdit(true) && this.EditingComboBox != null &&
                          displayStyle != DataGridViewComboBoxDisplayStyle.Nothing)
                            this.CheckDropDownList(e.X, e.Y, e.RowIndex);
            }
        }

        /// <summary>
        /// This is overridden to invalidate the cell when the mouse enters it when using the <c>Popup</c> flat
        /// style and the <c>ComboBox</c> display style.
        /// </summary>
        /// <param name="rowIndex">The row index</param>
        protected override void OnMouseEnter(int rowIndex)
        {
            if(base.DataGridView != null)
            {
                if(displayStyle == DataGridViewComboBoxDisplayStyle.ComboBox && flatStyle == FlatStyle.Popup)
                    base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);

                base.OnMouseEnter(rowIndex);
            }
        }

        /// <summary>
        /// This is overridden to invalidate the cell when the mouse leaves it when themed and the mouse is over
        /// the drop-down button or when using the pop-up combo box style.
        /// </summary>
        /// <param name="rowIndex">The row index</param>
        protected override void OnMouseLeave(int rowIndex)
        {
            if(base.DataGridView != null)
            {
                if(mouseInDropDownButtonBounds)
                {
                    mouseInDropDownButtonBounds = false;

                    if(base.ColumnIndex >= 0 && rowIndex >= 0 && (flatStyle == FlatStyle.Standard ||
                      flatStyle == FlatStyle.System) && Application.RenderWithVisualStyles)
                        base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
                }

                if(displayStyle == DataGridViewComboBoxDisplayStyle.ComboBox && flatStyle == FlatStyle.Popup)
                    base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);

                base.OnMouseLeave(rowIndex);
            }
        }

        /// <summary>
        /// This is overridden to invalidate the cell as needed when the mouse moves around within it to show the
        /// drop-down button as hot or cold.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            if(base.DataGridView != null)
            {
                if((flatStyle == FlatStyle.Standard || flatStyle == FlatStyle.System) &&
                  Application.RenderWithVisualStyles)
                {
                    Rectangle buttonRect;
                    int rowIndex = e.RowIndex;

                    DataGridViewAdvancedBorderStyle borderStylePlaceHolder = new DataGridViewAdvancedBorderStyle();

                    DataGridViewAdvancedBorderStyle borderStyle = this.AdjustCellBorderStyle(
                            base.DataGridView.AdvancedCellBorderStyle, borderStylePlaceHolder,
                            !base.DataGridView.RowHeadersVisible &&
                                base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single,
                            !base.DataGridView.ColumnHeadersVisible &&
                                base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single,
                            base.OwningColumn.Index == base.FirstDisplayedColumnIndex,
                            rowIndex == base.DataGridView.FirstDisplayedScrollingRowIndex);

                    Rectangle clipBounds = base.DataGridView.GetCellDisplayRectangle(base.OwningColumn.Index,
                        rowIndex, false);

                    if(base.OwningColumn.Index == base.DataGridView.FirstDisplayedScrollingColumnIndex)
                    {
                        clipBounds.X -= base.DataGridView.FirstDisplayedScrollingColumnHiddenWidth;
                        clipBounds.Width += base.DataGridView.FirstDisplayedScrollingColumnHiddenWidth;
                    }

                    DataGridViewElementStates rowState = base.DataGridView.Rows.GetRowState(rowIndex);
                    DataGridViewElementStates elementState = this.CellStateFromColumnRowStates(rowState);

                    this.PaintPrivate(DataGridViewHelper.CachedGraphics(base.DataGridView), clipBounds,
                        clipBounds, rowIndex, elementState, null, null,
                        base.GetInheritedStyle(null, rowIndex, false), borderStyle, out buttonRect,
                        DataGridViewPaintParts.ContentForeground, false, false, false);

                    bool inButton = buttonRect.Contains(base.DataGridView.PointToClient(Control.MousePosition));

                    if(inButton != mouseInDropDownButtonBounds)
                    {
                        mouseInDropDownButtonBounds = inButton;
                        base.DataGridView.InvalidateCell(e.ColumnIndex, rowIndex);
                    }
                }

                base.OnMouseMove(e);
            }
        }

        /// <summary>
        /// This is overridden to paint the cell
        /// </summary>
        /// <param name="graphics">The graphics object</param>
        /// <param name="clipBounds">The clip bounds</param>
        /// <param name="cellBounds">The cell bounds</param>
        /// <param name="rowIndex">The row index of the cell</param>
        /// <param name="cellState">The cell state</param>
        /// <param name="value">The value of the cell</param>
        /// <param name="formattedValue">The formatted value for the cell</param>
        /// <param name="errorText">The error text associated with the cell, if any</param>
        /// <param name="cellStyle">The cell style</param>
        /// <param name="advancedBorderStyle">The advanced border style</param>
        /// <param name="paintParts">The parts of the cell to paint</param>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds,
          int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue,
          string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
          DataGridViewPaintParts paintParts)
        {
            Rectangle buttonRect;

            if(cellStyle == null)
                throw new ArgumentNullException("cellStyle");

            this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText,
                cellStyle, advancedBorderStyle, out buttonRect, paintParts, false, false, true);
        }

        /// <summary>
        /// This converts a value formatted for display to an actual cell value
        /// </summary>
        /// <param name="formattedValue">The formatted display value of the cell.</param>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="formattedValueTypeConverter">A <see cref="TypeConverter"/> for the display value type,
        /// or null to use the default converter.</param>
        /// <param name="valueTypeConverter">A <see cref="TypeConverter"/> for the cell value type, or null to
        /// use the default converter.</param>
        /// <returns>The cell value found based on the formatted value.</returns>
        public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, 
          TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            object cellValue, item;

            if(valueTypeConverter == null)
            {
                if(valuePropDesc != null)
                    valueTypeConverter = valuePropDesc.Converter;
                else
                    if(displayPropDesc != null)
                        valueTypeConverter = displayPropDesc.Converter;
            }

            // If we don't have a data source or value/display members, the base implementation will do
            if((this.DataManager == null || (displayPropDesc == null && valuePropDesc == null)) &&
              String.IsNullOrEmpty(displayMember) && String.IsNullOrEmpty(valueMember))
                return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter,
                    valueTypeConverter);

            // Parse the value
            cellValue = miParseFormattedValueInternal.Invoke(this, new[] { this.DisplayType, formattedValue,
                cellStyle, formattedValueTypeConverter, this.DisplayTypeConverter });

            // Look it up and return the found value
            if(cellValue == null)
                return cellValue;

            if(displayPropDesc != null || valuePropDesc != null)
            {
                item = this.ItemFromComboBoxDataSource(displayPropDesc != null ? displayPropDesc : valuePropDesc,
                    cellValue);
            }
            else
                item = this.ItemFromComboBoxItems(base.RowIndex,
                    String.IsNullOrEmpty(displayMember) ? valueMember : displayMember, cellValue);

            if(item == null)
            {
                if(cellValue != DBNull.Value)
                    throw new FormatException(LR.GetString("ExFormatterCannotConvert", cellValue, this.DisplayType));

                return DBNull.Value;
            }

            return this.GetItemValue(item);
        }
        #endregion
    }
}
