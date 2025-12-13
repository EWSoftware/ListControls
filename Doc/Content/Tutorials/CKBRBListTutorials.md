---
uid: 3af969ee-6757-4660-9cc9-5bd518c24e05
alt-uid: CKBRBListTutorials
title: CheckBoxList and RadioButtonList Tutorial
keywords: "tutorials, CheckBoxList", "tutorials, RadioButtonList"
---

<autoOutline lead="This topic describes the Radio Button List control and its features." excludeRelatedTopics="true" />


## Common Features

The `CheckBoxList` and `RadioButtonList` are quite
similar with regard to creation and usage.  Simply drag the control from the toolbox, drop it on the form, and
use the **Properties** window to adjust its settings.  For simple lists, you can enter text
strings in the designer by selecting the `Items` property and entering the values one per
line in the collection editor.  When items are added directly to the `Items` collection,
the `SortOrder` property can be used to sort the items in ascending or descending order.
The default sort order is `None`.


A simple `ListItem` object is supplied that can be used with an array list
as the data source for the list controls.  It contains a `Display` property for the
display text and a `Value` property for the value of the item.  The demo application
contains examples of its use.


The controls also support the same complex data sources that any other standard .NET control can
use (i.e. data sets, data views, data tables, arrays, etc).  To assign a more complex data source, you can set
the `DisplayMember`, `ValueMember`, and `DataSource`
properties.  If assigning these three properties in code, always assign the `DataSource`
property last to improve performance.  This is true of all .NET controls as assigning a new display or value
member forces the control to reevaluate its data source.  By assigning the data source last, it will only have to
evaluate the data source once.


The properties in the **Appearance** category can be used to alter the visual
style of the control.  The `LayoutMethod` property allows you to define how the buttons
within the control are laid out (single column, single row, down then across, or across and then down).  You can
also specify an image list and define how the buttons, text, and images are aligned.  The
`RadioButtonList` and `CheckBoxList` controls are Windows XP
theme-aware.


The properties in the **Title** category can be used to specify a title that
appears on the top border of the control.  You can adjust its font and the colors used to display the title text.


Both controls have an `EnforceDefaultSelection` property that allows you to
specify whether a default selection will be set if an attempt is made to set the selected index to -1.  When set
to true, the item specified by the `DefaultSelection` property will be used instead.  By
default, this feature is enabled and the first entry in the data source is used as the default selection.  This
ensures that for the radio button list, it is never shown without any radio button selected.  For the checkbox
list, the check state of the specified item is unaffected when the selected index changes.  It simply lets you
specify which item should get the focus if the selection is cleared.


Unlike the standard .NET list controls, all of the controls in the `EWSoftware.ListControls`
namespace allow you to index their data source to extract values.  This allows you to obtain the value of a field
in the current selection or a field in any row even if it is not the selected item and regardless of whether or
not the field is used as the display or value member.


``` cs{title="C#"}
// Get the vendor name from the radio button list's current selection
string vendorName = (string)rblVendor["VendorName"];

// Get the item quantity from the sixth row of the checkbox list
int itemQty = (int)cblItems[5, "ItemQty"];
```

``` vbnet{title="VB.NET"}
' Get the vendor name from the radio button list's current selection
Dim vendorName As String = CType(rblVendor("VendorName"), String)

' Get the item quantity from the sixth row of the checkbox list
Dim itemQty As Integer = CType(cblItems(5, "ItemQty"), Integer)
```


## RadioButtonList Usage

The `RadioButtonList` allows only a single selection and provides a set of
properties and events similar to the standard `ListBox` control such as
`SelectedIndex`, `SelectedValue`, `SelectedIndexChanged`,
etc.  As such, you can use it in a similar fashion but can take advantage of its extra features such as the
layout options, etc.



## CheckBoxList Usage

The `CheckBoxList` is similar in nature to the standard
`CheckedListBox` control but is much more flexible as it allows better data binding
support and many more options with regard to its appearance and layout.  The `CheckBoxList`
allows you to select one or more items from a data source. By default, it uses two-state checkboxes (checked and
unchecked).  By setting the `ThreeState` property to true, you can make it use three-state
checkboxes (checked, unchecked, and indeterminate).


The `CheckBoxList` control provides a set of properties and events similar
to those of the `RadioButtonList` such as `SelectedIndex`,
`SelectedValue`, `SelectedIndexChanged`, etc.  The
`SelectedIndexChanged` event is raised when a new item is selected in the list.  Setting
the `SelectedIndex` property will give the item at the specified index the focus but does
not change its check state. To detect changes in an item's check state, use the
`ItemCheckStateChanged` event.  The checkbox list also provides the
`GetItemChecked`/`SetItemChecked` and
`GetItemCheckState`/`SetItemCheckState` methods to get or set the
check state of items in the list.  All of those methods are overloaded to accept either an integer specifying the
item's index in the collection or an item key which corresponds to the `ValueMember` value
of an item in the collection.


``` cs{title="C#"}
// Get an array of events from a comma-separated list of codes retrieved from a
// database field.
string[] events = eventCodes.Split(',');

// Mark each of the selected event codes as checked
foreach(string s in eventCodes)
    cblEventCodes.SetItemChecked(s.Trim(), true);
```

``` vbnet{title="VB.NET"}
' Get an array of events from a comma-separated list of codes retrieved from a
' database field.
Dim events() As String = eventCodes.Split(",")

' Mark each of the selected event codes as checked
Dim s As String

For Each s In eventCodes
    cblEventCodes.SetItemChecked(s.Trim(), True)
Next
```

The `CheckedIndices` property can be used to obtain a collection that
contains the indices of all items in the list that have a check state of `Checked` or
`Indeterminate`.


The `CheckedItems` property can be used to obtain a collection that contains
the actual items in the list that have a check state of `Checked` or
`Indeterminate`.  This collection has some helper methods that you will find useful.


<table>
  <thead>
    <tr>
      <th>Method</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>`ValueOf`</td>
      <td>This method can be used to obtain the value of the item in the collection.  The return value
corresponds to the `ValueMember` value of the item in the list.</td>
    </tr>
    <tr>
      <td>`DisplayTextOf`</td>
      <td>This method can be used to obtain the display text of the item in the collection.  The return
value corresponds to the `DisplayMember` value of the item in the list.</td>
    </tr>
    <tr>
      <td>`CheckStateOf`</td>
      <td>This method can be used to get the check state of the item in the collection.</td>
    </tr>
    <tr>
      <td>`ToString`</td>
      <td>This method has been overridden so that it returns a comma-separated list of the values from
the collection.  The values corresponds to the `ValueMember` values of the items in the
list.</td>
    </tr>
    <tr>
      <td>`ToDisplayTextString`</td>
      <td>This method returns a comma-separated list of the display text values from the collection.
The values corresponds to the `DisplayMember` values of the items in the list.</td>
    </tr>
  </tbody>
</table>

``` cs{title="C#"}
CheckedItemsCollection checkedItems = cblEventCodes.CheckedItems;

// Display the value of each checked item
for(int idx = 0; idx < checkedItems.Count; idx++)
    Debug.WriteLine(checkedItems.ValueOf(idx));

// Display the display text of each checked item
for(int idx = 0; idx < checkedItems.Count; idx++)
    Debug.WriteLine(checkedItems.DisplayTextOf(idx));

// Display the check state of each checked item
for(int idx = 0; idx < checkedItems.Count; idx++)
    Debug.WriteLine(checkedItems.CheckStateOf(idx));

// Get the checked values into a string ready to store in
// a database field.
string eventCodes = checkedItems.ToString();

// Show the display text values in a message box
MessageBox.Show("You chose: " + checkedItems.ToDisplayTextString());
```

``` vbnet{title="VB.NET"}
Dim idx As Integer
Dim checkedItems As CheckedItemsCollection = cblEventCodes.CheckedItems

' Display the value of each checked item
For idx = 0 To checkedItems.Count - 1
    Debug.WriteLine(checkedItems.ValueOf(idx))
Next idx

' Display the display text of each checked item
For idx = 0 To checkedItems.Count - 1
    Debug.WriteLine(checkedItems.DisplayTextOf(idx))
Next idx

' Display the check state of each checked item
For idx = 0 To checkedItems.Count - 1
    Debug.WriteLine(checkedItems.CheckStateOf(idx))
Next idx

' Get the checked values into a string ready to store in
' a database field.
Dim eventCodes As String = checkedItems.ToString()

' Show the display text values in a message box
MessageBox.Show("You chose: " & checkedItems.ToDisplayTextString())
```


## RadioButtonList Data Binding

You have a choice of three different control properties that can be bound to a data source in order
to edit fields in the data source.


<table>
  <thead>
    <tr>
      <th>Property</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>`SelectedValue`</td>
      <td>Use this when list items are specified using the `DisplayMember`,
`ValueMember`, and `DataSource` properties.</td>
    </tr>
    <tr>
      <td>`SelectedItem`</td>
      <td>Use this when list items are specified by adding them to the control's
`Items` property.</td>
    </tr>
    <tr>
      <td>`SelectedIndex`</td>
      <td>You may use this if the field values corresponds to an index from zero to the number of items
in the list minus one regardless of how the list items are defined.</td>
    </tr>
  </tbody>
</table>

When using the `DataSource` property to specify the items that appear in the
radio button list, you will add a binding on the `SelectedValue` property.  As the
selection changes in the list, the `SelectedValue` property also changes thus updating the
data source.  Below is an example of adding a data binding through code.  The bindings can also be done using the
Visual Studio designer.


``` cs{title="C# - RadioButtonList Data Binding"}
// Assign a data source for the list items displayed by the radio button list
rblContactType.DisplayMember = "Display";
rblContactType.ValueMember = "Value";
rblContactType.DataSource = contactTypeList;

// Bind the ContactType field in the addresses data source to the SelectedValue property
rblContactType.DataBindings.Add(new Binding("SelectedValue", addressList, "ContactType");
```

``` vbnet{title="VB.NET - RadioButtonList Data Binding"}
' Assign a data source for the list items displayed by the radio button list
rblContactType.DisplayMember = "Display"
rblContactType.ValueMember = "Value"
rblContactType.DataSource = contactTypeList

' Bind the ContactType field in the addresses data source to the SelectedValue property
rblContactType.DataBindings.Add(New Binding("SelectedValue", addressList, "ContactType")
```

The example above assumes that the field to which the property is bound will never be null or that
the list contains a null value entry.  If the bound field can contain null but your radio button list does not
contain an item with a null value, you can use a `Format` event handler on the data
binding to enforce a default value. For example:


``` cs{title="C# - Enforce a Default Value on Null Fields"}
// Create the binding and connect the event handler
Binding b = new Binding("SelectedValue", dsAddresses, "ContactType");
b.Format += ContactType_Format;

// Add the binding to the radio button list
rblContactType.DataBindings.Add(b);

// This event handler converts null values to a default radio button list value
private void ContactType_Format(object sender, ConvertEventArgs e)
{
    if(e.Value == null || e.Value == DBNull.Value)
        e.Value = "B";
}
```

``` vbnet{title="VB.NET - Enforce a Default Value on Null Fields"}
' Create the binding and connect the event handler
Dim b As New Binding("SelectedValue", dsAddresses, "ContactType")
AddHandler b.Format, AddressOf ContactType_Format

' Add the binding to the radio button list
rblContactType.DataBindings.Add(b)

' This event handler converts null values to a default radio button list value
Private Sub ContactType_Format(sender As Object, e As ConvertEventArgs)
    If e.Value Is Nothing OrElse e.Value Is DBNull.Value Then
        e.Value = "B"
    End If
End Sub
```

If you specify the items to display in the radio button list by adding them to the
`Items` property rather than using the `DataSource` property, you
will need to bind the field to the `SelectedItem` property instead.  If the value in the
data source corresponds directly to a radio button list index from zero to the number of items in the radio
button list minus one, you may bind the field to the `SelectedIndex` property.  The
results are the same.  As the selection changes, the field will be updated with the value from the bound
property.  The **Relationship Test** example in the demo application contains an example of
binding a radio button list to a data source.



## CheckBoxList Data Binding

Unlike the radio button list, the checkbox list is a multi-valued control.  As such, it does not
make sense to a bind field to the `SelectedValue`, `SelectedItem`,
or `SelectedIndex` property as it would only reflect one of the selected values.  The
`CheckBoxList` control provides three additional properties that allow you to easily bind
one field to each checkbox in the list thus allowing you to edit multiple fields with little effort.


<table>
  <thead>
    <tr>
      <th>Property</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>`BindingMembersDataSource`</td>
      <td>This property is used to specify the data source that contains the fields to which each
checkbox will be bound.</td>
    </tr>
    <tr>
      <td>`BindingMembers`</td>
      <td>This string collection contains a list of each field in the
`BindingMembersDataSource` that will be bound to one checkbox in the list.  There should
be at least one field for each checkbox in the list.  Any excess fields that do not have a corresponding checkbox
will be ignored.  The data type of the fields should be Boolean or convertible to a Boolean value.  Nulls are
supported.</td>
    </tr>
    <tr>
      <td>`BindingMembersBindingContext`</td>
      <td>This property can be used to specify the binding context to use when binding the checkboxes
to the data source.  Normally, you can ignore this property.  However, if the data source is used by another
control on the form and that control has a binding context other than the default, you should assign its binding
context to this property so that changes to the data source will be reflected in both controls.</td>
    </tr>
  </tbody>
</table>

As noted above, the data type of the fields specified in `BindingMembers`
should be Boolean or convertible to a Boolean value.  Null values are also supported and the behavior of the
control when a null value is encountered depends on the setting of the `ThreeState`
property.  If set to false, null values are converted to unchecked (false).  If `ThreeState`
is set to true, null values are converted to indeterminate and the checkbox will display using the grayed
indeterminate state.  Likewise, if a checkbox is set to the indeterminate state, the bound field will be set to
null.


Below is an example of adding data bindings through code.  The bindings can also be done using the
Visual Studio designer.


``` cs{title="C# - CheckBoxList Data Binding"}
// Add items to use for the checkbox item text.  We could also assign them using
// the DisplayMember, ValueMember, and DataSource too.
cblAddressTypes.Items.AddRange(new object[] {
    "Domestic Address", "International Address", "Postal Address",
    "Parcel Address", "Home Address", "Business Address" });

// Specify to which field each checkbox will be bound
cblAddressTypes.BindingMembers.AddRange(new string[] {
    "Domestic", "International", "Postal", "Parcel", "Home", "Business" });

// Specify the data source containing the binding members
cblAddressTypes.BindingMembersDataSource = addressList;
```

``` vbnet{title="VB.NET - CheckBoxList Data Binding"}
' Add items to use for the checkbox item text.  We could also assign them using
' the DisplayMember, ValueMember, and DataSource too.
cblAddressTypes.Items.AddRange(New Object() {
    "Domestic Address", "International Address", "Postal Address",
    "Parcel Address", "Home Address", "Business Address" });

' Specify to which field each checkbox will be bound
cblAddressTypes.BindingMembers.AddRange(New String() {
    "Domestic", "International", "Postal", "Parcel", "Home", "Business" })

' Specify the data source containing the binding members
cblAddressTypes.BindingMembersDataSource = addressList
```

The **Relationship Test** example in the demo application contains an example
of binding a checkbox list to a data source.



## See Also


**Other Resources**  
[](@ce5cb77a-44a9-4629-aec3-6a8435cd8e53)  
