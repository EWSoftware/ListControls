'===============================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : DemoTable.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/02/2024
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
    Friend NotInheritable Class DemoTable

        ''' <summary>
        ''' The primary key
        ''' </summary>
        <Key>
        Public Property ListKey As Integer

        ''' <summary>
        ''' A label value
        ''' </summary>
        Public Property Label As String

        ''' <summary>
        ''' A text value
        ''' </summary>
        Public Property TextValue As String

        ''' <summary>
        ''' A date value
        ''' </summary>
        Public Property DateValue As DateTime

        ''' <summary>
        ''' A Boolean value
        ''' </summary>
        Public Property BoolValue As Boolean
    
    End Class
End Namespace
