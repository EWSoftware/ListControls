'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : ExtendedTreeViewDemo.cs
' Author  : Eric Woodruff
' Updated : 10/02/2014
' Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft Visual C#
'
' This form is used to demonstrate the extended tree view control
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 03/02/2007  EFW  Created the code
'================================================================================================================

Imports System.Drawing.Drawing2D

Imports EWSoftware.ListControls

Public Class ExtendedTreeViewTestForm
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        Dim tnFolder, tnBook, tnPage As TreeNode
        Dim i, j, k As Integer

        InitializeComponent()

        tvExtTree.BeginUpdate()

        ' Only even numbered nodes gets an image
        For i = 0 To 2
            tnFolder = tvExtTree.Nodes.Add("Fld" & i.ToString(), "Folder " & i.ToString(),
                CType(IIf(i Mod 2 <> 0, -1, 0), Integer), CType(IIf(i Mod 2 <> 0, -1, 1), Integer))
            tnFolder.ToolTipText = tnFolder.Name

            For j = 0 To 2
                tnBook = tnFolder.Nodes.Add(String.Format("Fld{0}Bk{1}", i, j), "Book " & j.ToString(),
                    CType(IIf(j Mod 2 <> 0, -1, 2), Integer), CType(IIf(j Mod 2 <> 0, -1, 3), Integer))
                tnBook.ToolTipText = tnBook.Name

                ' These also get a state image
                For k = 0 To 4
                    tnPage = tnBook.Nodes.Add(String.Format("Fld{0}Bk{1}Pg{2}", i, j, k),
                        "Page " & k.ToString(), CType(IIf(k Mod 2 <> 0, -1, 4), Integer),
                        CType(IIf(k Mod 2 <> 0, -1, 4), Integer))
                    tnPage.StateImageIndex = k
                    tnPage.ToolTipText = tnPage.Name
                Next k
            Next j
        Next i

        tvExtTree.EndUpdate()
        tvExtTree.Nodes(0).ExpandAll()

        pgProps.SelectedObject = tvExtTree
    End Sub

    ''' <summary>
    ''' Refresh the tree view when a property value changes
    ''' </summary>
    ''' <param name="s">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub pgProps_PropertyValueChanged(ByVal s As System.Object, _
      ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) _
      Handles pgProps.PropertyValueChanged
        tvExtTree.Invalidate()
        tvExtTree.Update()
    End Sub

    ''' <summary>
    ''' Switch between small and large images
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub chkLargeImages_CheckedChanged(ByVal sender As System.Object, _
       ByVal e As System.EventArgs) Handles chkLargeImages.CheckedChanged
        Dim newHeight As Integer

        tvExtTree.BeginUpdate()

        If chkLargeImages.Checked Then
            tvExtTree.ImageList = ilLarge
        Else
            tvExtTree.ImageList = ilSmall
        End If

        ' Reset the item height and indent
        newHeight = tvExtTree.ImageList.Images(0).Height + 2

        If newHeight < tvExtTree.Font.Height + 3 Then
            newHeight = tvExtTree.Font.Height + 3
        End If

        tvExtTree.ItemHeight = newHeight
        tvExtTree.Indent = 0

        tvExtTree.EndUpdate()
        pgProps.Refresh()
    End Sub

    ''' <summary>
    ''' Enable or disable state images
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub chkStateImages_CheckedChanged(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles chkStateImages.CheckedChanged
        tvExtTree.BeginUpdate()

        If chkStateImages.Checked Then
            tvExtTree.StateImageList = ilState
        Else
            tvExtTree.StateImageList = Nothing
        End If

        tvExtTree.EndUpdate()
        pgProps.Refresh()
    End Sub

    ''' <summary>
    ''' Enable or disable the custom expando images
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub chkExpandoImages_CheckedChanged(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles chkExpandoImages.CheckedChanged
        tvExtTree.BeginUpdate()

        If chkExpandoImages.Checked Then
            tvExtTree.ExpandoImageList = ilExpando
        Else
            tvExtTree.ExpandoImageList = Nothing
        End If

        tvExtTree.ShowPlusMinus = True
        tvExtTree.EndUpdate()
        pgProps.Refresh()
    End Sub

    ''' <summary>
    ''' Enable or disable the form-level OnDrawNode handler
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub chkFormDrawNode_CheckedChanged(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles chkFormDrawNode.CheckedChanged
        ' When enabled, clicks on the extra text drawn by the event will cause the node to get selected too
        tvExtTree.SelectOnRightOfLabelClick = chkFormDrawNode.Checked

        tvExtTree.Invalidate()
        tvExtTree.Update()
    End Sub

    ''' <summary>
    ''' Rotate through the state images when they are clicked or the space bar is hit
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub tvExtTree_ChangeStateImage(ByVal sender As System.Object, _
      ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvExtTree.ChangeStateImage
        Dim idx As Integer = e.Node.StateImageIndex

        idx += 1

        If idx > 4 Then
            idx = 0
        End If

        e.Node.StateImageIndex = idx
    End Sub

    #Region "TreeNodeDrawing Example"
    ''' <summary>
    ''' Test TreeNodeDrawing event handler.  This occurs before the tree
    ''' view draws the node.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub tvExtTree_TreeNodeDrawing(ByVal sender As System.Object, _
      ByVal e As EWSoftware.ListControls.DrawTreeNodeExtendedEventArgs) _
      Handles tvExtTree.TreeNodeDrawing
        If Not chkFormDrawNode.Checked Then
            Return
        End If

        ' Use solid 2px lines
        e.LinePen.DashStyle = DashStyle.Solid
        e.LinePen.Width = 2

        ' Change the text by adding the node name.  Assigning new text
        ' automatically recalculates the text and focus bounds. We could
        ' draw the text, but we'll let the base class handle it. Note that
        ' since the tree view doesn't know about the extra text, it won't
        ' show the horizontal scrollbar if it goes off the right edge.
        e.Text += " (" & e.Node.Name & ")"

        ' If the item height is larger than 35, wrap the text too
        If tvExtTree.ItemHeight > 35 Then
            e.StringFormat.FormatFlags = StringFormatFlags.NoClip

            ' Limit the text to the right edge of the node bounds.  Note
            ' that this won't stop the tree view from showing a horizontal
            ' scrollbar.
            e.TextBounds = new Rectangle(e.TextBounds.Left, e.NodeBounds.Top,
                e.NodeBounds.Width - e.TextBounds.Left, e.NodeBounds.Height)
        End If

        ' NOTE:
        ' If you choose to draw one or more parts of the node, you should
        ' draw the background first, then the node parts, and then turn
        ' off the NodeParts.Background flag along with the other node part
        ' flags in the e.NodeParts property before returning.
    End Sub
    #End Region

    #Region "TreeNodeDrawn Example"
    ''' <summary>
    ''' Test TreeNodeDrawn event handler.  This occurs after the tree view
    ''' has drawn the node.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub tvExtTree_TreeNodeDrawn(ByVal sender As System.Object, _
      ByVal e As EWSoftware.ListControls.DrawTreeNodeExtendedEventArgs) _
      Handles tvExtTree.TreeNodeDrawn
        Dim pen As Pen
        Dim top As Integer

        If chkFormDrawNode.Checked And e.Node.Level = 0 And e.ImageIndex <> -1 Then
            ' Nothing exciting, we'll just draw a line through top level nodes
            ' that have an image index set.
            If (e.State And TreeNodeStates.Focused) <> 0 And tvExtTree.FullRowSelect
                pen = SystemPens.ControlLightLight
            Else
                pen = SystemPens.ControlDarkDark
            End If

            top = CType(e.NodeBounds.Top + (e.NodeBounds.Height / 2), Integer)

            e.Graphics.DrawLine(pen, e.NodeBounds.Left, top, _
                e.NodeBounds.Left + e.NodeBounds.Width, top)
        End If
    End Sub
    #End Region

    ''' <summary>
    ''' Enumerate the tree nodes using the built-in TreeNodeEnumerator
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnEnumTree_Click(ByVal sender As System.Object, _
      ByVal e As System.EventArgs) Handles btnEnumTree.Click
        '#Region "Enumerate ExtendedTreeView control"
        Dim node As TreeNode
        txtEnumResults.Text = Nothing

        ' Use foreach() on the ExtendedTreeView control itself to
        ' enumerate all of its nodes recursively.
        For Each node in tvExtTree
            txtEnumResults.AppendText(String.Format("{0}{1}" & Environment.NewLine,
                New String(" "C, node.Level * 4), node.Text))
        Next
        '#End Region
    End Sub

    ''' <summary>
    ''' Enumerate just the selected node (branch) and, based on the sender, the subsequent siblings
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnEnumNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnEnumNode.Click, btnEnumNodeSibs.Click

        '#Region "TreeNodeEnumerator Example"
        Dim enumerateSiblings As Boolean = False
        Dim node As TreeNode
        Dim startNode As TreeNode = tvExtTree.SelectedNode

        If sender Is btnEnumNodeSibs Then
            enumerateSiblings = True
        End If

        If startNode Is Nothing Then
            txtEnumResults.Text = "Select a starting node first"
            Return
        End If

        txtEnumResults.Text = Nothing

        ' For this, we create the enumerator manually and pass it the starting
        ' node and a flag indicating whether or not to enumerate the siblings
        ' of the starting node as well.
        Dim enumerator As TreeNodeEnumerator = New TreeNodeEnumerator(startNode,
            enumerateSiblings)

        ' Call the MoveNext() method to move through each node.  Use the Current
        ' property to access the current node.
        Do While enumerator.MoveNext()
            node = enumerator.Current

            txtEnumResults.AppendText(String.Format("Manual Enum: {0}{1}" &
                Environment.NewLine, New String(" "C, node.Level * 4), node.Text))
        Loop

        txtEnumResults.AppendText(Environment.NewLine & Environment.NewLine)

        ' We can also use the helper method to simplify it
        For Each node In TreeNodeEnumerator.Enumerate(startNode, enumerateSiblings)
            txtEnumResults.AppendText(String.Format("Enum Helper: {0}{1}" &
                Environment.NewLine, New String(" "C, node.Level * 4), node.Text))
        Next

        '#End Region
    End Sub

    ''' <summary>
    ''' Find a node by its name using the tree view's indexer
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event argument</param>
    Private Sub btnFindNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnFindNode.Click

        '#Region "Tree Node Indexer Example"
        Dim name As String = txtFindName.Text.Trim()

        If name.Length = 0 Then
            MessageBox.Show("Enter a node name to find", "Tree View Demo",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Simply use the indexer with the name of the node.  Note that node names
        ' aren't necessarily unique (they are in the demo) so it returns the first
        ' node found with the given name.
        Dim node As TreeNode = tvExtTree(txtFindName.Text)

        If node IsNot Nothing Then
            tvExtTree.SelectedNode = node
            node.EnsureVisible()
            tvExtTree.Focus()
        Else
            MessageBox.Show("Unable to find node named '" & name & "'.  " & _
                "The name is case-sensitive.  Use the tool tips or enable " & _
                "the form DrawNode events to verify the node names and " & _
                "try again.", "Tree View Demo", MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
        End If
        '#End Region
    End Sub

    ''' <summary>
    ''' Display the names of all currently checked nodes in the tree view.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnCheckedNames_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckedNames.Click

        If tvExtTree.CheckBoxes = False Then
            MessageBox.Show("The Checkboxes property must be set to true for this to work", "Tree View Demo",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim checkedNodes As CheckedNodesCollection = tvExtTree.CheckedNodes

        ' The collection's ToNameString() method will return a comma separated list of the Name property values
        MessageBox.Show("Currently checked node names:" & Environment.NewLine & checkedNodes.ToNameString(),
            "Tree View Demo", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Display the names of all currently checked nodes in the tree view.
    ''' </summary>
    ''' <param name="sender">The sender of the event</param>
    ''' <param name="e">The event arguments</param>
    Private Sub btnCheckedText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckedText.Click

        If tvExtTree.CheckBoxes = False Then
            MessageBox.Show("The Checkboxes property must be set to true for this to work", "Tree View Demo",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim checkedNodes As CheckedNodesCollection = tvExtTree.CheckedNodes

        ' The collection's ToTextValueString() method will return a comma separated list of the Text property
        ' values.
        MessageBox.Show("Currently checked node text values:" & Environment.NewLine &
            checkedNodes.ToTextValueString(), "Tree View Demo", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
