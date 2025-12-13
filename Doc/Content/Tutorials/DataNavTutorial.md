---
uid: 8fbf556a-9edc-4071-85ef-6df0d4d13950
alt-uid: DataNavTutorial
title: DataNavigator Control Tutorial
keywords: "tutorials, DataNavigator"
---

<autoOutline lead="This topic describes the Data Navigator control and its features." excludeRelatedTopics="true" />


## General Information

The `DataNavigator` control allows you to navigate through a data source
using a set of VCR-style buttons displaying one row at a time.  To use it, simply drag the control from the
toolbox, drop it on the form, and use the **Properties** window to adjust its settings.  In
addition, you will need to add other controls to the form and bind them to fields in the data source used by the
data navigator control.


The properties in the **Appearance** category allow you to change its
appearance and show or hide the Add and Delete buttons.


The properties in the **Behavior** category can be used to specify whether or
not additions, edits, or deletions are allowed to the data source, the shortcut keys to use for deleting a row,
moving to the new row, or jumping to the row number navigation text box, and setting the repeat speed for the
Next and Previous navigation buttons.


The control supports the same complex data sources that any other standard .NET control can use
(i.e. data sets, data views, data tables, arrays, etc).  To assign a data source and edit its contents, you must
set the `DataSource` property.  You may also need to set the `DataMember`
property if the source contains multiple items to which the data navigator can bind (i.e. a
`DataSet`).  If you assign these properties in code, always assign the
`DataSource` property last to improve performance.  This is true of all .NET controls as
assigning a new display or value member forces the control to reevaluate its data source.  By assigning the data
source last, it will only have to evaluate the data source once.  You can also assign the data source properties
using the `SetDataBinding` method.


``` cs{title="C#"}
// Set the data source properties individually
dnNav.DataMember = "Addresses";
dnNav.DataSource = dsAddresses;

// Set the data source using the SetDataBinding method
dnNav.SetDataBinding(dsAddresses, "Addresses");

// Specify the table directly without going through the data set
dnNav.SetDataBinding(dsAddresses.Tables[0], null);
```

``` vbnet{title="VB.NET"}
' Set the data source properties individually
dnNav.DataMember = "Addresses"
dnNav.DataSource = dsAddresses

' Set the data source using the SetDataBinding method
dnNav.SetDataBinding(dsAddresses, "Addresses")

' Specify the table directly without going through the data set
dnNav.SetDataBinding(dsAddresses.Tables(0), Nothing)
```

Unlike the standard .NET list controls, all of the controls in the `EWSoftware.ListControls`
namespace allow you to index their data source to extract values.  This allows you to obtain the value of a field
in the currently selected row or a field in any row even if it is not the selected row and regardless of whether
or not the field is displayed on the form or user control containing the data navigator.


``` cs{title="C#"}
// Get the last name from the current row
string lastName = (string)dnPeople["LastName"];

// Get the age from the sixth row
int age = (int)dnPeople[5, "Age"];
```

``` vbnet{title="VB.NET"}
' Get the last name from the current row
Dim lastName As String = CType(dnPeople("LastName"), String)

' Get the age from the sixth row
Dim age As Integer = CType(dnPeople(5, "Age"), Integer)
```


## The Data Navigator's Change Policy

The `DataNavigator` control has three properties that allow you to specify
whether additions, edits, or deletes are allowed to be made to the data source (`AllowAdditions`,
`AllowEdits`, and `AllowDeletes`).  All three properties default to
true.  However, when a data source is assigned to the data navigator, these properties may be updated to reflect
the change policy of the data source.  For example, if the data source does not allow additions, the
`AllowAdditions` property will also be set to false.  Likewise, you can set the properties
to false to disallow the associated modification even if the underlying data source allows it.


The data navigator will automatically adjust its behavior when additions are not allowed by
disabling the Add button in the navigation controls.  When deletions are not allowed, it disables the Delete
button in the navigation controls.  The shortcut keys for both operations are also ignored when their associated
behavior is not allowed.  The data navigator control cannot handle the `AllowEdits`
setting by itself.  It is up to you to add code to your form to check the change policy as needed and disable the
controls if additions or edits are not allowed.  The demo application contains an example of this.


The data navigator also raises a `NoRows` event when a data source is
assigned that contains no rows or when the last row is deleted.  You can use this event to disable edit controls
until a new row is added and/or display a message asking the user to add a new row.  The demo application shows
one way of doing this.



## Using Validation with the Data Navigator

If you need to perform validation on the form controls that are bound to fields in the data
navigator's data source, you should handle the `Validating` event and (optionally) the
`Validated` event on the `DataNavigator` control.  It will fire the
`Validating` and `Validated` events on itself whenever an attempt
is made to move off of the current row using the navigation controls.


The `Validating` event is fired first. If not canceled, it raises the
`Validated` event and allows the position change to occur.  If you need to check to see if
the current row is valid at any other time (i.e. prior to saving changes to the data source), you can check the
`IsValid` property on the data navigator.  This causes it to fire the validation events
and returns true if the row is valid (the `Validating` event was not canceled) or false if
the row is not valid.  The demo application contains an example.



## See Also


**Other Resources**  
[](@ce5cb77a-44a9-4629-aec3-6a8435cd8e53)  
