'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : TreeViewDropDown.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/03/2024
' Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
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

Public Partial Class TreeViewDropDown
    Inherits DropDownControl

    Private demoData As List(Of DemoTable)
    Private productInfo As List(Of ProductInfo)
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

    ' Clone the combo box's data source and load the tree view
    Public Overrides Sub InitializeDropDown()
        Dim tn As TreeNode, s As String, li As ListItem, idx As Integer
        Dim ds As Object = Me.ComboBox.DataSource

        ' Determine the data source type in use by the demo form
        If ds Is Nothing Then
            For Each s In Me.ComboBox.Items
                tn = New TreeNode(s) With {
                    .Tag = s
                }
                tvItems.Nodes.Add(tn)
            Next
        ElseIf TypeOf(ds) Is ArrayList Then
            For Each li In Me.ComboBox.Items
                tn = New TreeNode(li.Display) With {
                    .Tag = li.Value
                }
                tvItems.Nodes.Add(tn)
            Next
        Else
            ' List of items
            If TypeOf(ds) Is List(Of ProductInfo) Then
                productInfo = CType(ds, List(Of ProductInfo))
            Else
                demoData = CType(ds, List(Of DemoTable))
            End If

            If excludeVisible = True Then
                ' Sort and load by category name
                productInfo = productInfo.OrderBy(Function (p) p.CategoryName).ThenBy(Function (p) p.ProductName).ToList()

                chkExcludeDiscontinued_CheckedChanged(Me, EventArgs.Empty)
            Else
                ' This is the one that doesn't contain categories
                For idx = 0 To demoData.Count - 1
                    tn = New TreeNode(demoData(idx).Label) With {
                        .Tag = demoData(idx).ListKey
                    }
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
        If Me.ComboBox.SelectedIndex <> -1 Then
            ' If not categorized, just select by index
            If excludeVisible = False Then
                If Me.ComboBox.SelectedIndex < tvItems.Nodes.Count Then
                    tvItems.SelectedNode = tvItems.Nodes(Me.ComboBox.SelectedIndex)
                End If
            Else
                ' Select the node by its key value stored in the tag
                Dim currentValue As Object = Me.ComboBox.SelectedValue

                Dim tne As New TreeNodeEnumerator(tvItems.Nodes(0), True)

                Do While tne.MoveNext()
                    If tne.Current.Tag IsNot Nothing AndAlso tne.Current.Tag.Equals(currentValue)
                        tvItems.SelectedNode = tne.Current
                        Exit Do
                    End If
                Loop
            End If
        End If
    End Sub

    ' Refresh the list based on the filter
    Private Sub chkExcludeDiscontinued_CheckedChanged(sender As Object, e As EventArgs) _
        Handles chkExcludeDiscontinued.CheckedChanged

        Dim root As TreeNode
        Dim row As Integer = 0
        Dim products As List(Of ProductInfo) = productInfo

        If chkExcludeDiscontinued.Checked = True Then
            products = products.Where(Function (p) Not p.Discontinued).ToList()
        End If

        tvItems.Nodes.Clear()

        Do While row < products.Count
            root = New TreeNode(products(row).CategoryName)
            tvItems.Nodes.Add(root)

            Do While row < products.Count AndAlso products(row).CategoryName = root.Text
                Dim child As New TreeNode(products(row).ProductName) With {
                    .Tag = products(row).ProductID
                }

                If products(row).Discontinued Then
                    child.Text += " (Discontinued)"
                End If

                root.Nodes.Add(child)
                row += 1
            Loop
        Loop

        tvItems.ExpandAll()
        tvItems.Nodes(0).EnsureVisible()

        btnSelect.Enabled = False
    End Sub

    ' Select the specified item and hide the drop-down.
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) _
      Handles btnSelect.Click
        If excludeVisible = False Then
            Me.CommitSelection(tvItems.Nodes.IndexOf(tvItems.SelectedNode))
        Else
            Me.CommitSelection(tvItems.SelectedNode.Tag)
        End If
    End Sub

    ' Disable selection if it is a parent node
    Private Sub tvItems_AfterSelect(sender As Object, e As TreeViewEventArgs) _
      Handles tvItems.AfterSelect
        btnSelect.Enabled = (tvItems.SelectedNode.Nodes.Count = 0)

        ' If this is the one without the categories, track the selection as each node is selected
        If excludeVisible = False Then
            Me.ComboBox.SelectedIndex = tvItems.Nodes.IndexOf( _
                tvItems.SelectedNode)
        End If
    End Sub

    ' Select the current item when double clicked if it has no children
    Private Sub tvItems_DoubleClick(sender As Object, e As EventArgs) _
      Handles tvItems.DoubleClick
        If tvItems.SelectedNode.Nodes.Count = 0 Then
            btnSelect_Click(sender, e)
        End If
    End Sub

    ' Close the drop-down without making a selection.  Hitting Escape works too.
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) _
      Handles btnCancel.Click
        Me.ComboBox.DroppedDown = False
    End Sub

End Class
