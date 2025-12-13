---
uid: 7cf6fc2e-bab8-4bf4-bf76-c1db9e2d342a
alt-uid: ExtTreeViewTutorial
title: ExtendedTreeView Control Tutorial
keywords: "tutorials, ExtendedTreeView"
---

<autoOutline lead="This topic describes the Extended Tree View control and its features." excludeRelatedTopics="true" />


## General Information

The `ExtendedTreeView` control is a fully owner-drawn tree view control that
overcomes some limitations in the way the default tree view is drawn.  It also provides several additional
features to make it easier to use than the standard tree view control.  Below is a list of the additional
features and differences:


- The node background color draws all the way across the node, not just behind the text.
- `FullRowSelect` works with `ShowLines` enabled.
- Unthemed checkboxes do not have the thick black border and look like normal unthemed flat
checkboxes.
- The checkboxes are correctly vertically centered based on the item height.
- Hot tracking works on nodes with their own font.
- The tree view can be set to not draw the default images for nodes without an image.  When it
does not draw the default image, the text is flush left with no gap.
- When using a state image list without checkboxes, a new `ChangeStateImage`
event is raised when the image is clicked or the space bar is hit.  This allows you to implement custom check
states on the tree nodes by using different images for the various states.
- The `DrawNode` event is suppressed.  In its place two new events are
raised that give better control over how the node is drawn.  `TreeNodeDrawing` is raised
prior to the node being drawn and lets you customize the default appearance.  `TreeNodeDrawn`
is raised after the node has been drawn and lets you customize the appearance of the fully drawn node.
- When `LabelEdit` is true, hitting F2 initiates label editing on the
selected node.
- **Ctrl+E** can be used to expand all child nodes of the selected node.
**Ctrl+K** can be used to collapse the selected node.  **Ctrl+Shift+E**
can be used to expand all nodes in the tree.  **Ctrl+Shift+K** can be used to collapse all
nodes in the tree.
- The `SelectOnRightOfLabelClick` property allows you to specify whether
or not a node is selected when the blank area to the right of its label is clicked.
- The expando (+/-) images can be replaced with custom images.
- The `AllowCollapse` property can be used to specify whether or not nodes
can be collapsed when `ShowPlusMinus` is false.
- The control implements `IEnumerable` and `IEnumerable<T>`
so that you can enumerate all of its nodes recursively using a simple for/each loop.  You can also use
`TreeNodeEnumerator` to enumerate the nodes in one branch and, optionally, its subsequent
siblings.  This saves you from having to write recursive methods to handle child nodes.
- The `SyncParentChildCheckedState` property can be set to true to ensure
that the checked state of parent and child nodes is synchronized.  If a child node is checked, all parent nodes
are marked as checked too.  If a parent is checked or unchecked, all child nodes are checked or unchecked as
well.  The checkbox on parent nodes with a mix of checked and unchecked children are drawn as a mixed checkbox to
make them easy to pick out when collapsed.
- An item indexer property is available to retrieve nodes from the tree view by node name
including child nodes at any level.
- Methods have been added to allow setting or getting the check state of a node by name as well
as for checking and unchecking all nodes in the tree view.
- The `CheckedNodes` property can be used to obtain a collection
containing the current set of checked nodes.



## Clearing The Tree Node Collection

Under certain circumstances, it is possible for the tree view to throw a null reference exception
when clearing the tree node collection.  This is caused by a redraw event that occurs as the tree nodes are
cleared.  The base tree view will only suspend redrawing if there are more than two hundred items.
Unfortunately, there is no way to tell whether or not a node is in the process of being removed.  As such, it may
be necessary to manually suspend updates when calling the node collection's `Clear` method
and resume updates afterwards.  For example:


``` cs{title="C#"}
// Suspend updates while clearing the tree nodes
tvExtTree.BeginUpdate();

// Clear the nodes
tvExtTree.Nodes.Clear();

// Resume updates
tvExtTree.EndUpdate();
```

``` vbnet{title="VB.NET"}
' Suspend updates while clearing the tree nodes
tvExtTree.BeginUpdate()

' Clear the nodes
tvExtTree.Nodes.Clear()

' Resume updates
tvExtTree.EndUpdate()
```


## ExtendedTreeView Usage

The `ExtendedTreeView` is similar in nature to the standard
`TreeView` control but is much more flexible as it allows better support for customizing
its appearance and has more options for node retrieval and enumeration.  It provides a set of properties and
events similar to those of the `TreeView`.  The extended tree view also provides the
`GetNodeChecked`, `GetNodeCheckedState`, and
`SetNodeChecked` methods to get or set the check state of nodes in the tree.  Each method
identifies the node by the name assigned to the node's `Name` property.  If the
`SyncParentChildCheckedState` property is set to true, the checked state of parent nodes
is kept in synch with the child nodes.  For example, if a child node is checked, all parent nodes are marked as
checked too.  If a parent is checked or unchecked, all child nodes are checked or unchecked as well.  The
checkbox on parent nodes with a mix of checked and unchecked children are drawn as a mixed checkbox to make them
easy to pick out when collapsed.


``` cs{title="C#"}
// Get a list of IDs to mark as checked.  Each node's Name property will match one of the IDs
string[] ids = currentItems.Split(',');

// Turn off synchronization while setting the check states
tvMenu.SyncParentChildCheckedState = false;

// Check each named node
foreach(string id in ids)
    tvMenu.SetNodeChecked(id.Trim(), true);

// Turn check state synchronization back on
tvMenu.SyncParentChildCheckedState = true;
```

``` vbnet{title="VB.NET"}
' Get a list of IDs to mark as checked.  Each node's Name property will match one of the IDs
Dim ids As String() = currentItems.Split(",")

' Turn off synchronization while setting the check states
tvMenu.SyncParentChildCheckedState = False

' Check each named node
For Each id in ids
    tvMenu.SetNodeChecked(id.Trim(), True)
Next

' Turn check state synchronization back on
tvMenu.SyncParentChildCheckedState = True
```

If you assign a name to each node using its `Name` property, you can use the
extended tree view's item indexer to retrieve the nodes.  This will return the named node regardless of whether
it is a root node or a child nested at any level within the tree.


``` cs{title="C#"}
// Set the named node's text
tvGroups["Folder2Book3Page5"].Text = "New Text Value";
```

``` vbnet{title="VB.NET"}
// Set the named node's text
tvGroups("Folder2Book3Page5").Text = "New Text Value";
```

If the tree view's `CheckBoxes` property is set to true, the
`CheckedNodes` property can be used to obtain a collection that contains the actual nodes
in the tree that have a check state of `Checked` or `Mixed`.  This
collection has some helper methods that you will find useful.


<table>
  <thead>
    <tr>
      <th>Method</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>`CheckStateOf`</td>
      <td>This method can be used to get the check state of the node in the collection.</td>
    </tr>
    <tr>
      <td>`ContainsName`</td>
      <td>This method can be used to determine whether or not the collection contains a node with the
specified name.</td>
    </tr>
    <tr>
      <td>`ToNameString`</td>
      <td>This method returns the checked nodes' names in a comma-separated list.</td>
    </tr>
    <tr>
      <td>`ToTextValueString`</td>
      <td>This method returns the checked nodes' text values in a comma-separated list.</td>
    </tr>
  </tbody>
</table>

``` cs{title="C#"}
CheckedNodesCollection checkedNodes = tvGroups.CheckedNodes;

// Display the check state of each checked node
for(int idx = 0; idx < checkedNodes.Count; idx++)
    Debug.WriteLine(checkedNodes.CheckStateOf(idx));

// See if the collection contains a node named "Folder1Book2Page5"
Debug.WriteLine(checkedNodes.Contains("Folder1Book2Page5"));

// Get the checked node names into a string ready to store in a database field
string groupNames = checkedNodes.ToNameString();

// Show the node text values in a message box
MessageBox.Show("You chose: " + checkedNodes.ToTextValueString());
```

``` vbnet{title="VB.NET"}
Dim idx As Integer
Dim checkedNodes As CheckedNodesCollection = tvGroups.CheckedNodes

' Display the check state of each checked node
For idx = 0 To checkedNodes.Count
    Debug.WriteLine(checkedNodes.CheckStateOf(idx))
Next idx

' See if the collection contains a node named "Folder1Book2Page5"
Debug.WriteLine(checkedNodes.Contains("Folder1Book2Page5"))

' Get the checked node names into a string ready to store in a database field
Dim groupNames As String = checkedNodes.ToNameString()

' Show the node text values in a message box
MessageBox.Show("You chose: " & checkedNodes.ToTextValueString())
```


## Node Enumeration

One of the most useful features of the extended tree view is its support for node enumeration.
With a simple for/each loop, you can enumerate all nodes in the tree view including all child nodes to however
deep they are nested.  This saves you from having to write recursive functions to handle child node collections.


When enumerating the entire tree, enumeration starts at the first root node.  If it has children,
they are enumerated.  As each is enumerated, if they have children, they are enumerated as well.  This process
repeats until all nodes have been enumerated.


``` cs{source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs" region="Enumerate ExtendedTreeView control" title="C# - Enumerate the entire tree"}
```

``` vbnet{source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb" region="Enumerate ExtendedTreeView control" title="VB.NET - Enumerate the entire tree"}
```

You can also use the `TreeNodeEnumerator` directly to enumerate a single
branch of the tree and all of its children.  The option is also given to enumerate all subsequent sibling nodes
of the starting node if so desired.


``` cs{source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs" region="TreeNodeEnumerator Example" title="C# - Enumerate starting at a selected node"}
```

``` vbnet{source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb" region="TreeNodeEnumerator Example" title="VB.NET - Enumerate starting at a selected node"}
```


## Customizing the Tree View's Appearance

The following additional properties allow you to customize the tree view's appearance without
having to handle the drawing events.


<table>
  <thead>
    <tr>
      <th>Property</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>`DrawDefaultImages`</td>
      <td>This property is used to set or get whether or not the tree view will draw default images for
tree nodes without images.  If not drawn, the node text is drawn flush left with no gap.  The default setting is
true to match the standard tree view control.</td>
    </tr>
    <tr>
      <td>`ExpandoImageList`</td>
      <td>This property is used to set or get the expando image list used to show the +/- images when
the `ShowPlusMinus` property is set to true.  See the property help for information on how
to set up the image list.</td>
    </tr>
    <tr>
      <td>`UseThemedImages`</td>
      <td>This is used to set or get whether or not to use the themed versions of the expando and
checkbox images even when themes are not being used.  The default is false and the images are based on the
system's current visual style setting.</td>
    </tr>
  </tbody>
</table>

To further customize the tree view's appearance you can handle the
[](@E:EWSoftware.ListControls.ExtendedTreeView.TreeNodeDrawing) and/or
[](@E:EWSoftware.ListControls.ExtendedTreeView.TreeNodeDrawn)
events.  Each event receives a [](@T:EWSoftware.ListControls.DrawTreeNodeExtendedEventArgs)
instance that contains drawing state information for the node.  The `TreeNodeDrawing`
event occurs prior to the node being drawn.  In your event handler, you can choose to alter the various
properties of the event arguments object and let the tree view draw the node based on your changes or you can
choose to draw the node yourself and suppress the default drawing of one or more parts of the node.  The
`TreeNodeDrawn` event occurs after the node has been fully drawn and allows you to
customize its appearance after all other drawing has taken place.


See the [](@T:EWSoftware.ListControls.DrawTreeNodeExtendedEventArgs)
property help topics for information on what each of them does.  The demo application supplied with the library
contains a simple example of handling each event.



## See Also


**Other Resources**  
[](@ce5cb77a-44a9-4629-aec3-6a8435cd8e53)  
