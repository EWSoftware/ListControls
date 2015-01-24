'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : TreeViewDropDown.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 10/02/2014
' Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft Visual Basic .NET
'
' This is a sample drop-down control for the UserControlComboBox demo
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 10/27/2005  EFW  Created the code
'================================================================================================================

Imports System
Imports System.Collections
Imports System.Data
Imports System.Windows.Forms

Imports EWSoftware.ListControls

Public Partial Class TreeViewDropDown
    Inherits EWSoftware.ListControls.DropDownControl

    Private dvItems As DataView
    Private tblItems As DataTable
    Private excludeVisible As Boolean

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    ' Get or set whether or not to show the "Exclude discontinued" checkbox.  We need a variable to track the
    ' state as the checkbox always reports false for visibility if the parent isn't visible (i.e. during
    ' initialization).
    Public Property ShowExcludeDiscontinued As Boolean
        Get
            Return excludeVisible
        End Get
        Set
            excludeVisible = Value
            chkExcludeDiscontinued.Visible = Value
        End Set
    End Property

    ' This loads items from the row collection into the tree view
    ' <param name="row">The starting row</param>
    ' <param name="tnRoot">The root node to which items are added</param>
    ' <returns>The index of the next row that starts a new node</returns>
    Private Function LoadItems(row As Integer, tnRoot As TreeNode) As Integer
        Dim tnChild As TreeNode

        Do While row < dvItems.Count AndAlso CType(dvItems(row)("CategoryName"), String) = tnRoot.Text
            tnChild = New TreeNode(CType(dvItems(row)("ProductName"), String))
            tnChild.Tag = dvItems(row)("ProductID")

            If CType(dvItems(row)("Discontinued"), Boolean) = True Then
                tnChild.Text &= " (Discontinued)"
            End If

            tnRoot.Nodes.Add(tnChild)
            row += 1
        Loop

        Return row
    End Function

    ' Clone the combo box's data source and load the tree view
    Public Overrides Sub InitializeDropDown()
        Dim tn As TreeNode, s As String, li As ListItem, idx As Integer
        Dim ds As Object = Me.ComboBox.DataSource

        ' Determine the data source type in use by the demo form
        If ds Is Nothing Then
            For Each s In Me.ComboBox.Items
                tn = New TreeNode(s)
                tn.Tag = s
                tvItems.Nodes.Add(tn)
            Next
        ElseIf TypeOf(ds) Is ArrayList Then
            For Each li In Me.ComboBox.Items
                tn = New TreeNode(li.Display)
                tn.Tag = li.Value
                tvItems.Nodes.Add(tn)
            Next
        Else
            ' Data set, view, or table
            If TypeOf(ds) is DataSet Then
                tblItems = CType(ds, DataSet).Tables("ProductInfo")
            ElseIf TypeOf(ds) is DataView Then
                tblItems = CType(ds, DataView).Table
            Else
                tblItems = CType(ds, DataTable)
            End If

            dvItems = tblItems.DefaultView

            If excludeVisible = True
                ' Sort and load by category name
                dvItems.Sort = "CategoryName, ProductName"
                chkExcludeDiscontinued_CheckedChanged(Me, EventArgs.Empty)
            Else
                ' This is the one that doesn't contain categories
                For idx = 0 To dvItems.Count - 1
                    tn = New TreeNode(CType(dvItems(idx)("Label"), String))
                    tn.Tag = dvItems(idx)("ListKey")
                    tvItems.Nodes.Add(tn)
                Next idx
            End If
        End If

        ' In simple mode, hide the cancel button
        If Me.ComboBox.DropDownStyle = ComboBoxStyle.Simple Then
            btnSelect.Left = btnCancel.Left
            btnCancel.Visible = False
        End If
    End Sub

    ' When shown, select the current product as the default row
    Public Overrides Sub ShowDropDown()
        Dim tn, child As TreeNode

        If Me.ComboBox.SelectedIndex <> -1 Then
            ' If not categorized, just select by index
            If excludeVisible = False Then
                If Me.ComboBox.SelectedIndex < tvItems.Nodes.Count Then
                    tvItems.SelectedNode = tvItems.Nodes(Me.ComboBox.SelectedIndex)
                End If
            Else
                ' There doesn't appear to be an easy way to search for a node so do it the hard way
                Dim currentValue As Object = Me.ComboBox.SelectedValue

                For Each tn In tvItems.Nodes
                    For Each child In tn.Nodes
                        If child.Tag.Equals(currentValue) Then
                            tvItems.SelectedNode = child
                            Exit For
                        End If
                    Next
                Next
            End If
        End If
    End Sub

    ' Refresh the list based on the filter
    Public Sub chkExcludeDiscontinued_CheckedChanged(sender As Object, e As System.EventArgs) _
        Handles chkExcludeDiscontinued.CheckedChanged

        Dim tnNode As TreeNode
        Dim row As Integer = 0

        If chkExcludeDiscontinued.Checked = True Then
            dvItems.RowFilter = "Discontinued = false"
        Else
            dvItems.RowFilter = Nothing
        End If

        tvItems.Nodes.Clear()

        Do While row < dvItems.Count
            tnNode = New TreeNode(CType(dvItems(row)("CategoryName"), String))
            tvItems.Nodes.Add(tnNode)
            row = LoadItems(row, tnNode)
        Loop
    End Sub

    ' Select the specified item and hide the drop-down.
    Private Sub btnSelect_Click(sender As Object, e As System.EventArgs) _
      Handles btnSelect.Click
        If excludeVisible = False Then
            Me.CommitSelection(tvItems.Nodes.IndexOf(tvItems.SelectedNode))
        Else
            Me.CommitSelection(tvItems.SelectedNode.Tag)
        End If
    End Sub

    ' Disable selection if it is a parent node
    Private Sub tvItems_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) _
      Handles tvItems.AfterSelect
        btnSelect.Enabled = (tvItems.SelectedNode.Nodes.Count = 0)

        ' If this is the one without the categories, track the selection as each node is selected
        If excludeVisible = False Then
            Me.ComboBox.SelectedIndex = tvItems.Nodes.IndexOf( _
                tvItems.SelectedNode)
        End If
    End Sub

    ' Select the current item when double clicked if it has no children
    Private Sub tvItems_DoubleClick(sender As Object, e As System.EventArgs) _
      Handles tvItems.DoubleClick
        If tvItems.SelectedNode.Nodes.Count = 0 Then
            btnSelect_Click(sender, e)
        End If
    End Sub

    ' Close the drop-down without making a selection.  Hitting Escape works too.
    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) _
      Handles btnCancel.Click
        Me.ComboBox.DroppedDown = False
    End Sub

End Class
