'===============================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : ProductInfo.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/12/2024
' Note    : Copyright 2024, Eric Woodruff, All rights reserved
'
' This is class is used to contain demo data from the test database for some of the example forms
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ==============================================================================================================
' 12/02/2024  EFW  Created the code
'===============================================================================================================

' This class is used but is created dynamically when needed
#Disable Warning CA1812

Namespace Database

    ''' <summary>
    ''' This is used to contain demo data for some of the example forms
    ''' </summary>
    Friend NotInheritable Class ProductInfo

        ''' <summary>
        ''' The primary key
        ''' </summary>
        <Key>
        Public Property ProductID As Integer

        ''' <summary>
        ''' The product name
        ''' </summary>
        Public Property ProductName As String

        ''' <summary>
        ''' The category name
        ''' </summary>
        Public Property CategoryName As String

        ''' <summary>
        ''' The company name
        ''' </summary>
        Public Property CompanyName As String

        ''' <summary>
        ''' The quantity per unit
        ''' </summary>
        Public Property QuantityPerUnit As String

        ''' <summary>
        ''' The unit price
        ''' </summary>
        Public Property UnitPrice As Decimal

        ''' <summary>
        ''' The units in stock
        ''' </summary>
        Public Property UnitsInStock As Short

        ''' <summary>
        ''' The units on order
        ''' </summary>
        Public Property UnitsOnOrder As Short

        ''' <summary>
        ''' The reorder level
        ''' </summary>
        Public Property ReorderLevel As Short

        ''' <summary>
        ''' The discontinued flag
        ''' </summary>
        <DisplayName("Discontinued?")>
        Public Property Discontinued As Boolean

        ''' <summary>
        ''' This is just a test column use to demonstrate the Browsable(false) attribute support
        ''' </summary>
        <Browsable(False)>
        Public Property HiddenColumn As Boolean

        ''' <summary>
        ''' This is used to get or set the row time stamp (row version)
        ''' </summary>
        <Timestamp, Browsable(False)>
        Public Property LastModified As Byte()

    End Class
End Namespace
