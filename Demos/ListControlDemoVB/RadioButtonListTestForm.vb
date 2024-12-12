'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : RadioButtonListTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/02/2024
' Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
'
' This is used to demonstrate the RadioButtonList control
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


Public Partial Class RadioButtonListTestForm
    Inherits Form

    Private ReadOnly demoData As List(Of DemoTable)
    Private ReadOnly productInfo As List(Of ProductInfo)

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        Try
            Using dc As New DemoDataContext()
                demoData = dc.DemoTable.OrderBy(Function (d) d.Label).ToList()
                productInfo = dc.ProductInfo.OrderBy(Function (p) p.ProductName).ToList()
            End Using

        Catch ex As SqlException
            MessageBox.Show(ex.Message)

        End Try

        cboDataSource.SelectedIndex = 0

        pgProps.SelectedObject = rblDemo
        pgProps.Refresh()

    End Sub

    ' Refresh the display and the radio button list settings after they have changed
    Private Sub pgProps_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) _
      Handles pgProps.PropertyValueChanged
        rblDemo.Invalidate()
        rblDemo.Update()
    End Sub

    ' Change the data source for the radio button list
    Private Sub cboDataSource_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboDataSource.SelectedIndexChanged

        ' Suspend updates to the radio button list to speed it up and prevent flickering
        rblDemo.BeginInit()

        ' Clear out the prior definitions
        cboColumns.Items.Clear()
        cboColumns.SelectedIndex = -1

        rblDemo.DataSource = Nothing
        rblDemo.DisplayMember = String.Empty
        rblDemo.ValueMember = String.Empty
        rblDemo.Items.Clear()

        ' We must have the data
        If demoData.Count = 0 Or productInfo.Count = 0 Then
            MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop)

            rblDemo.EndInit()
            Return
        End If

        ' Data tables, views, and sets are also supported but we won't demonstrate that here
        Select Case cboDataSource.SelectedIndex
            Case 0      ' Demo data (List(Of DemoData))
                rblDemo.DisplayMember = nameof(DemoTable.Label)
                rblDemo.ValueMember = nameof(DemoTable.ListKey)
                rblDemo.DataSource = demoData

            Case 1      ' Product info (List(Of ProductInfo))
                rblDemo.DisplayMember = nameof(DataBase.ProductInfo.ProductName)
                rblDemo.ValueMember = nameof(Database.ProductInfo.ProductID)
                rblDemo.DataSource = productInfo

            Case 2      ' Array List
                Dim al As New ArrayList(100)

                For Each p In productInfo
                    al.Add(New ListItem(p.ProductID, p.ProductName))
                Next

                rblDemo.DisplayMember = nameof(ListItem.Display)
                rblDemo.ValueMember = nameof(ListItem.Value)
                rblDemo.DataSource = al

            Case 3      ' Item collection strings
                ' Like the above but we add the strings directly to the radio button list's Items collection
                For Each p In productInfo
                    rblDemo.Items.Add(p.ProductName)
                Next

                ' The item collection is the data source for this one.  It's a simple string list so there are no
                ' display or value members.

        End Select

        ' Resume updates to the radio button list and display the new set of radio buttons
        rblDemo.EndInit()

        ' Load the column names
        If Not (rblDemo.DataSource Is Nothing) Then
            If TypeOf(rblDemo.DataSource) Is ArrayList Then
                cboColumns.Items.Add(nameof(ListItem.Display))
                cboColumns.Items.Add(nameof(ListItem.Value))
            Else
                Dim dataSourceType As Type

                If cboDataSource.SelectedIndex = 0
                    dataSourceType = GetType(DemoTable)
                Else
                    dataSourceType = GetType(ProductInfo)
                End If

                For Each p in dataSourceType.GetProperties()
                    cboColumns.Items.Add(p.Name)
                Next
            End If
        End If

        If cboColumns.Items.Count = 0 Then
            cboColumns.Enabled = False
            udcRowNumber.Enabled = False
            btnGetValue.Enabled = False
        Else
            cboColumns.Enabled = True
            udcRowNumber.Enabled = True
            btnGetValue.Enabled = True
        End If
    End Sub

    ' Get a value from the radio button list
    Private Sub btnGetValue_Click(sender As Object, e As EventArgs) _
      Handles btnGetValue.Click
        ' This can be any column from the data source regardless of whether or not it is displayed.  Note that
        ' you can also use rblDemo("ColName") to get a column value from the item indicated by the SelectedIndex
        ' property.
        txtValue.Text = $"{cboColumns.Text} = {rblDemo(CType(udcRowNumber.Value, Integer), cboColumns.Text)}"
    End Sub

    ' Show the current item info when the selected index changes
    Private Sub rblDemo_SelectedIndexChanged(sender As Object, e As EventArgs) _
      Handles rblDemo.SelectedIndexChanged
        ' Note that SelectedValue is only valid if there is a data source
        txtValue.Text = $"Index = {rblDemo.SelectedIndex}, Value = {rblDemo.SelectedValue}, Text = {rblDemo.Text}"
    End Sub

    ' Use or clear the image list
    Private Sub chkShowImages_CheckedChanged(sender As Object, e As EventArgs) _
      Handles chkShowImages.CheckedChanged
        If chkShowImages.Checked Then
            ' The image list is not usable with themed radio buttons under .NET 1.1
            If rblDemo.FlatStyle = FlatStyle.System Then
                rblDemo.FlatStyle = FlatStyle.Standard
            End If

            rblDemo.ImageList = ilImages
        Else
            rblDemo.ImageList = Nothing
        End If
    End Sub

End Class
