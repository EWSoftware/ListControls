---
uid: ca3b503d-654e-4fe6-8ca3-ec3ea8fb9a1a
alt-uid: Welcome
title: Welcome
keywords: feature overview, welcome
---

<autoOutline lead="Welcome to the EWSoftware List Controls Library.  This topic contains an overview of the controls and their features." />


## Common Features

All of the list controls in this library share a common set of features:


- The controls supports various data source types just like other .NET controls such as the
standard combo box (data sets, data views, data tables, array lists or any other collection object that implement
the `IList` interface, and simple strings).
- The controls have item indexer properties that allow you to retrieve any column value by name
from the data source for the currently selected item or any row in the data source.  Any column value from the
data source can be retrieved, not just those used as the display and value members.
- A simple `ListItem` object is supplied that can be used with an array
list as the data source for the list controls.
- For the combo box controls, the `Items` collection is sortable in
ascending or descending order if not using a data source (i.e. items are added directly to the
`Items` collection).
- All of the controls are Windows XP theme-aware and will utilize the current theme by default if
the application has a manifest file or if `Application.EnableVisualStyles()` has been
called.
- Design-time support is included for defining formatting, layout, data source, and many other
control properties.



## The Radio Button List Control and Its Features

The `RadioButtonList` control provides a single-selection radio button
group that supports data binding, layout options, and data source indexers.  It provides a similar set of
properties as the combo box control for working with the selected item (i.e. `SelectedIndex`,
`SelectedValue`, etc.).  It also provides the following features:


- The radio button list supports four different layout methods (single column, single row, down
then across, and across then down).  For the last two, the control will automatically lay out the radio buttons
in columns using a best fit algorithm. Properties are also provided to let you modify the padding around the list
and between columns and rows.
- The radio button list is resizable at runtime and can scroll both vertically and horizontally
when necessary to display items that exceed the bounds of the control.
- The control supports an image list to display images for each radio button entry.  Images can
be displayed with or without text.
- Alignment properties are provided for the checkmark, image, and text so that you can define the
layout of the radio button entries in the list.
- The control supports displaying entries as normal radio buttons or as button controls without
the checkmark.
- An optional title can be displayed on the border above the list.



## The Checkbox List Control and Its Features

The `CheckBoxList` control provides a multi-selection checkbox group that
supports data binding, layout options, and data source indexers. It provides the same set of properties and
features as the `RadioButtonList` control shown above (using checkboxes instead) and
provides the following additional features:


- The `Checked` and `CheckState` properties of any
checkbox in the list can be retrieved or set by index (numeric) position or by key (`ValueMember`)
value.
- The indices of the checked items or the values of the checked items from the data source can be
returned using the `CheckedIndices` and `CheckedItems` properties.
- The checkbox list can use three-state checkboxes (checked, unchecked, and indeterminate).
- Each checkbox in the list can be bound to an individual field in another data source using the
`BindingMembers` and `BindingMembersDataSource` properties.
- By setting the `BorderStyle` to `Sunken`, the
`ListBackColor` to `Window`, and the `FlatStyle`
to `Flat` you can simulate a `CheckedListBox` control's appearance
but gain access to all of the additional features of the `CheckBoxList` control such as
data binding, layout options, formatting, an image list, data source indexers, setting the check state of items
by key, etc.



## The Combo Box Controls and Their Features

The `AutoCompleteComboBox` is identical to the standard combo box control
with the addition of auto-completion to select matching entries as the user types text. The auto-complete feature
is available in all combo box styles (`DropDown`, `DropDownList`,
and `Simple`).


The `MultiColumnComboBox` provides the same features as the standard combo
box control but with a drop-down list that is capable of showing multiple columns of information.  It also
provides the following features:


- The control supports auto-completion in all combo box styles to select matching entries as the
user types text.
- Various new events have been added and behavior changes made to fix problems that are present
in the standard combo box control.  See the tutorial for more information.
- Unlike other implementations of multi-column combo boxes, the form hosting the control will not
lose focus.  Instead, the control acts like the normal combo box allowing the parent form to retain focus and the
text box portion to retain the keyboard cursor.
- The drop-down portion auto-sizes itself when first displayed based on the defined settings.  It
also positions itself correctly on the screen either below or above the combo box to show the maximum number of
items based on its defined settings.
- The drop-down portion is resizable at runtime and can scroll both vertically and horizontally
when necessary.  The `Simple` combo box style is also supported.
- If no column settings are defined, the drop-down will display all columns from the data source
with basic formatting.
- If type information is available in the data source, the drop-down will automatically show
Boolean values using a checkbox column.
- A `ColumnFilter` property allows you to let the drop-down add the data
source columns automatically but limits them to the ones that you specify.
- For finer control over the columns displayed as well as their formatting options, column style
definitions can be used to specify the columns to display in the drop-down as well as their format (alignment,
column header text, format string, null text, width, etc).
- When defining column styles, you can use the standard text box or boolean data grid columns
supplied with .NET or you can use custom third-party column types such as label columns, image columns, progress
bar columns, etc (all drop-down data is read-only).
- If you define drop-down column styles, setting the `Width` property to
-1 will cause the drop-down to size the column to the preferred width.  Setting the width to zero will cause the
drop-down to size the column to the longest value in the data source for the column. Setting the width to any
other positive value will size the column to the specified width.


The `UserControlComboBox` provides the same features as the standard combo
box control but with a drop-down that is capable of showing any user control that you define.  It also provides
the following features:


- Various new events have been added and behavior changes made to fix problems that are present
in the standard combo box control.  See below for more information.
- The control supports auto-completion in all combo box styles to select matching entries as the
user types text.
- The drop-down portion displays a user control that you create and derive from the supplied
`DropDownControl` class.  This allows it to show non-standard lists such as tree views,
list views, or any combination of controls that you can imagine.  For this control, the parent form will lose the
focus when the drop-down is displayed so that controls within the drop-down can gain the focus.
- The drop-down portion auto-sizes itself when first displayed based on the defined user control.
It also positions itself correctly on the screen either below or above the combo box to show as much of the user
control as possible.
- The drop-down portion is resizable at runtime and can scroll both vertically and horizontally
when necessary.  The `Simple` combo box style is also supported.



## The Data List Controls and Their Features

The `DataList` control allows you to specify a user control template that is
used to display and edit information from a data source similar in nature to the `DataList`
web server control, the subform control or continuous forms detail section in a Microsoft Access form, or the
`DataRepeater` control from Visual Basic 6.  It provides the following features:


- Add, update, and delete behavior is detected based on the data source and each behavior can
also be disabled via properties on the control.
- If additions are allowed, the data list will display a "new row" placeholder at the end of the
list.  A new row is automatically added to the data source if you modify any of the controls in the placeholder
row template.
- The control will refresh the displayed rows automatically if it detects changes to its data
source such as rows directly added to or removed from the data source or direct changes made to fields in a row
in the data source rather than through the row template.
- The control supports data set relationships so you can use it to edit information displayed in
a master/detail format.
- Navigation controls are provided that let you move from record to record, jump to a specific
record, delete a row, and jump to the new row item.
- A caption area can be displayed at the top of the data list similar to the caption option in
the `DataGrid` control.
- A customizable separator can be shown to provide a dividing line between rows in the list.
- A row header can be shown to indicate the currently selected row.  An indicator is also
displayed on the new row item when it does not have the focus.
- If row headers are visible, one or more rows can be selected by clicking on a row header and
dragging the mouse.  Shift-clicking the mouse on a row header can be used to extend the selection up or down.
Methods and properties are also provided to set the row selection range.
- Clicking and dragging in the row headers within the current selection will fire the
`BeginDrag` event so that you can initiate a drag and drop operation.
- Various events are provided to let you add functionality for occurrences such as rows being
data bound, adding a row, deleting a row, a row being made the current row, etc.
- A user control derived from the supplied `TemplateControl` class is used
to provide a means for displaying and editing rows from the data source.  This template can contain any controls
in any layout that you need to display and edit information (text boxes, combo boxes, list boxes, radio buttons,
etc).
- The template controls for each row are initialized and bound as they are scrolled into view to
save time and resources.  This makes it ideal for small data sets and still makes it responsive when dealing with
much larger data sets.  It also lets you customize the formatting of elements on a row-by-row basis (i.e.
color-code fields in each row based on their value, etc).
- User controls derived from the `TemplateControl` class can also be
specified to provide a header and/or footer for the data list.  The header and footer are placed in fixed
positions vertically but will scroll horizontally with the data rows if necessary.  As with the data rows, the
header and footer templates can contain any controls in any layout that you need to display and edit information
(text boxes, combo boxes, list boxes, radio buttons, etc). Since the header and footer templates are fixed, they
are bound to the data source as a whole rather than to individual rows within the data source.  Information in
bound fields in the header and footer will update as you move from row to row in the detail section.
- Definable shortcut keys are provided to let you delete rows in the data source, jump to the new
row item, jump to the row number navigation text box, and to switch between the header, detail, and footer
sections.
- Hitting the Escape key will cancel changes to the current row.
- Ctrl+Tab and Ctrl+Shift+Tab let you tab out of the data list to the following or preceding
control on the form.  When tabbing back into the data list, the last focused row will regain the focus.
- A `ClickableLabel` control is provided so that clicking a label in a row
template will give it the focus.  This can also be used in other forms to focus a control when its associated
label is clicked.



## The Data Navigator Control and Its Features

The `DataNavigator` control allows you to navigate through a data source
using a set of VCR-style buttons displaying one row at a time.


- Add, update, and delete behavior is detected based on the data source and each behavior can
also be disabled via properties on the control.
- The control supports data set relationships so you can use it to edit information displayed in
a master/detail format.
- Navigation controls are provided that let you move from record to record, jump to a specific
record, delete a row, and add a new row.
- Various events are provided to let you add functionality for occurrences such as adding a row,
deleting a row, a row being made the current row, etc.
- Definable shortcut keys are provided to let you delete rows in the data source, add a new row
to the data source, and jump to the row number navigation text box.
- Hitting the Escape key will cancel changes to the current row.



## The Extended Tree View Control and Its Features

The `ExtendedTreeView` control is a fully owner-drawn tree view control that
overcomes some limitations in the way the default tree view is drawn.  It also provides several additional
features to make it easier to use than the standard tree view control.


- Several visual changes have been made to make it look better (the node background color draws
all the way across, unthemed checkboxes do not have the thick black border, hot tracking works on nodes with
their own font, and many more).
- Two new draw events, `TreeNodeDrawing` and `TreeNodeDrawn`
are raised in place of the standard `DrawNode` event to give you more options to customize
the tree node appearance before and after it has been drawn.
- When using a state image list without checkboxes, a new `ChangeStateImage`
event is raised when the image is clicked or the space bar is hit.
- When `LabelEdit` is set to true, F2 initiates editing on the selected
node.  Additional hot keys are also supported to expand and collapse the nodes in the tree.
- The `AllowCollapse` property can be used to specify whether or not nodes
can be collapsed when `ShowPlusMinus` is false.
- The control implements `IEnumerable` and `IEnumerable<T>`
so that you can enumerate all of its nodes recursively using a simple for/each loop.  You can also use
`TreeNodeEnumerator` to enumerate the nodes in one branch and, optionally, its subsequent
siblings.  This saves you from having to write recursive methods to handle child nodes.
- The `SyncParentChildCheckedState` property can be set to true to ensure
that the checked state of parent and child nodes is synchronized.
- An item indexer property is available to retrieve nodes from the tree view by node name.
- Methods have been added to allow setting or getting the check state of a node by name as well
as for checking and unchecking all nodes in the tree view.
- The `CheckedNodes` property can be used to obtain a collection
containing the current set of checked nodes.



## See Also


**Other Resources**  
[](@a3a3d71f-2d20-4506-bab1-638af4a36f4b)  
[](@ce5cb77a-44a9-4629-aec3-6a8435cd8e53)  
[](@31916411-7c9d-40b1-a1f5-fef539e21783)  
[](@4466344c-0933-476c-9743-63008a2d8659)  
