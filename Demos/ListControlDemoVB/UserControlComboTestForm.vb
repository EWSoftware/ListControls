'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : UserControlComboBoxTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 04/09/2023
' Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
'
' This is used to demonstrate the UserControlComboBox control
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

Imports System.Data
Imports System.Data.OleDb

Imports EWSoftware.ListControls

Public Partial Class UserControlComboTestForm
    Inherits Form

    Private demoData, productData As DataSet

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' This demo uses the same data source for both combo boxes so give each their own binding context
        cboAutoComp.BindingContext = New BindingContext()
        cboUCCombo.BindingContext = New BindingContext()

        demoData = New DataSet()
        productData = New DataSet()

        Try
            Using dbConn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\TestData.mdb")
                ' Load some data for the demo
                Using cmd As New OleDbCommand("Select * From DemoTable Order By Label", dbConn)
                    cmd.CommandType = CommandType.Text
                    
                    Using adapter As New OleDbDataAdapter(cmd)
                        adapter.Fill(demoData)

                        ' Use a named table for this one
                        adapter.TableMappings.Add("Table", "ProductInfo")
                        cmd.CommandText = "Select * From ProductInfo Order By ProductName"
                        adapter.Fill(productData)
                    End Using
                End Using
            End Using

        Catch ex As OleDbException
            MessageBox.Show(ex.Message)

        End Try

        cboUCCombo.DropDownControl = GetType(TreeViewDropDown)

        ' Start with the data set as it is grouped by category in the drop-down
        cboDataSource.SelectedIndex = 2

        pgProps.SelectedObject = cboUCCombo
        pgProps.Refresh()
    End Sub

    ' Load data from the selected source and return the binding info.
    ' <param name="collection">The combo box item collection used by a couple of the data sources</param>
    ' <param name="dataSource">The variable used to receive the data source</param>
    ' <param name="displayMember">The variable used to receive the display member name</param>
    ' <param name="valueMember">The variable used to receive the value member name</param>
	Private Sub LoadData(ByRef collection As IList, ByRef dataSource As Object, ByRef displayMember As String,
      ByRef valueMember As String)
        Dim dr As DataRow, c As DataColumn

        ' We must have the data
        If demoData.Tables.Count = 0 Or productData.Tables.Count = 0 Then
            MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop)

            dataSource = Nothing
            displayMember = String.Empty
            valueMember = String.Empty
            Return
        End If

        Select Case cboDataSource.SelectedIndex
            Case 0      ' Data Table
                dataSource = demoData.Tables(0)
                displayMember = "Label"
                valueMember = "ListKey"

            Case 1      ' Data View
                dataSource = demoData.Tables(0).DefaultView
                displayMember = "Label"
                valueMember = "ListKey"

            Case 2      ' Data Set
                dataSource = productData

                ' Use a named table for this one
                displayMember = "ProductInfo.ProductName"
                valueMember = "ProductInfo.ProductID"

            Case 3      ' Array List
                Dim al As New ArrayList(100)

                For Each dr In productData.Tables(0).Rows
                    al.Add(New ListItem(dr("ProductID"), CType(dr("ProductName"), String)))
                Next

                dataSource = al
                displayMember = "Display"      ' ListItem description
                valueMember = "Value"          ' ListItem value

            Case 4      ' Combo box strings
                ' Like the above but we add the strings directly to the combo box's Items collection
                For Each dr In productData.Tables(0).Rows
                    collection.Add(dr("ProductName"))
                Next

                ' The item collection is the data source for this one.  It's a simple string list so there are no
                ' display or value members.
                dataSource = Nothing
                displayMember = String.Empty
                valueMember = String.Empty

            Case Else
                ' Unknown.  Won't happen but it shuts the compiler up.
                dataSource = Nothing
                displayMember = String.Empty
                valueMember = String.Empty

        End Select

        ' Load the column names
        If Not (dataSource Is Nothing) And cboColumns.Items.Count = 0 Then
            If TypeOf(dataSource) Is ArrayList Then
                cboColumns.Items.Add("Display")
                cboColumns.Items.Add("Value")
            Else
                Dim tbl As DataTable

                If TypeOf(dataSource) Is DataSet Then
                    tbl = productData.Tables(0)
                Else
                    tbl = demoData.Tables(0)
                End If

                For Each c In tbl.Columns
                    cboColumns.Items.Add(c.ColumnName)
                Next
            End If
        End If
    End Sub

    ' Refresh the display and the combo box drop-down settings after they have changed
    Private Sub pgProps_PropertyValueChanged(s As Object, e As System.Windows.Forms.PropertyValueChangedEventArgs) _
      Handles pgProps.PropertyValueChanged
        cboUCCombo.Invalidate()
        cboUCCombo.Update()

        ' We'll also force the drop-down to refresh its settings.
        cboUCCombo.RefreshSubControls()

        If cboUCCombo.DropDownStyle <> cboAutoComp.DropDownStyle Then
            cboAutoComp.DropDownStyle = cboUCCombo.DropDownStyle

            If cboUCCombo.DropDownStyle = ComboBoxStyle.Simple Then
                cboAutoComp.Height = 150
                cboUCCombo.Height = grpOptions.Top - cboUCCombo.Top - 5
            End If
        End If

        If cboUCCombo.Enabled <> cboAutoComp.Enabled Then
            cboAutoComp.Enabled = cboUCCombo.Enabled
        End if

        cboAutoComp.Enabled = cboUCCombo.Enabled
        cboAutoComp.BackColor = cboUCCombo.BackColor
        cboAutoComp.ForeColor = cboUCCombo.ForeColor
        cboAutoComp.RightToLeft = cboUCCombo.RightToLeft
        cboAutoComp.FlatStyle = cboUCCombo.FlatStyle
    End Sub

    ' Change the data source for the combo boxes
    Private Sub cboDataSource_SelectedIndexChanged(sender As Object, e As System.EventArgs) _
        Handles cboDataSource.SelectedIndexChanged

        Dim dataSource As Object = Nothing
        Dim displayMember As String = Nothing, valueMember As String = Nothing

        ' Clear out the prior definitions
        cboColumns.Items.Clear()
        cboColumns.SelectedIndex = -1

        cboAutoComp.DataSource = Nothing
        cboAutoComp.DisplayMember = String.Empty
        cboAutoComp.ValueMember = String.Empty
        cboAutoComp.Items.Clear()

        cboUCCombo.DataSource = Nothing
        cboUCCombo.DisplayMember = String.Empty
        cboUCCombo.ValueMember = String.Empty
        cboUCCombo.Items.Clear()

        ' This will dispose of the drop-down portion and clear out all existing definitions ready for the new
        ' stuff.
        cboUCCombo.RefreshSubControls()

        ' Keep it simple.  We'll bind them to the same data but using different instances so no need for binding
        ' contexts.
        LoadData(cboAutoComp.Items, dataSource, displayMember, valueMember)

        If Not (dataSource Is Nothing) Then
            cboAutoComp.DisplayMember = displayMember
            cboAutoComp.ValueMember = valueMember
            cboAutoComp.DataSource = dataSource
        End If

        ' Suspend updates to the combo box to speed it up and prevent flickering
        cboUCCombo.BeginInit()
        LoadData(cboUCCombo.Items, dataSource, displayMember, valueMember)
        cboUCCombo.EndInit()

        If Not (dataSource Is Nothing) Then
            cboUCCombo.DisplayMember = displayMember
            cboUCCombo.ValueMember = valueMember
            cboUCCombo.DataSource = dataSource
        End If

        If cboColumns.Items.Count = 0 Then
            cboColumns.Enabled = False
            txtRowNumber.Enabled = False
            btnGetValue.Enabled = False
        Else
            cboColumns.Enabled = True
            txtRowNumber.Enabled = True
            btnGetValue.Enabled = True
        End If
    End Sub

    ' Get a value from the combo box
    Private Sub btnGetValue_Click(sender As Object, e As System.EventArgs) _
        Handles btnGetValue.Click
        ' This can be any column from the data source regardless of whether or not it is displayed.  Note that
        ' you can also use cboUCCombo("ColName") to get a column value from the item indicated by the
        ' SelectedIndex property.
        txtValue.Text = $"{cboColumns.Text} = {cboUCCombo(CType(txtRowNumber.Value, Integer), cboColumns.Text)}"
    End Sub

    ' Show the current item info when the selected index changes
    Private Sub cboUCCombo_SelectedIndexChanged(sender As Object, e As System.EventArgs) _
        Handles cboUCCombo.SelectedIndexChanged
        ' Note that SelectedValue is only valid if there is a data source
        txtValue.Text = $"Index = {cboUCCombo.SelectedIndex}, Value = {cboUCCombo.SelectedValue}, Text = {cboUCCombo.Text}"
    End Sub

    ' For this demo, the drop-down has to determine the data source for itself so it could figure out whether or
    ' not to hide the checkbox.  This just demonstrates one possible use of this event.
    Private Sub cboUCCombo_DropDownControlCreated(sender As Object, e As System.EventArgs) _
        Handles cboUCCombo.DropDownControlCreated

        Dim ddc As TreeViewDropDown = CType(sender, TreeViewDropDown)

        ddc.ShowExcludeDiscontinued = (cboDataSource.SelectedIndex = 2)
    End Sub

    ' Draw an image for the demo.  They aren't representative of the items, they're just something to show.
    Private Sub cboUCCombo_DrawItemImage(sender As Object, e As System.Windows.Forms.DrawItemEventArgs) _
      Handles cboUCCombo.DrawItemImage
        If e.Index = -1 Then
            e.DrawBackground()
        Else
            If (e.State And DrawItemState.Disabled) <> 0 Then
                ControlPaint.DrawImageDisabled(e.Graphics, ilImages.Images(e.Index Mod ilImages.Images.Count),
                    e.Bounds.X, e.Bounds.Y, e.BackColor)
            Else
                e.Graphics.DrawImage(ilImages.Images(e.Index Mod ilImages.Images.Count), e.Bounds.X, e.Bounds.Y)
            End If
        End If
    End Sub
End Class
