'===============================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : DemoInfo.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/02/2024
' Note    : Copyright 2024, Eric Woodruff, All rights reserved
'
' This is class is used to contain demo info from the test database
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
    ''' This is used to contain demo info from the database
    ''' </summary>
    Friend NotInheritable Class DemoInfo

        ''' <summary>
        ''' The demo order
        ''' </summary>
        Public Property DemoOrder As Integer

        ''' <summary>
        ''' The demo name
        ''' </summary>
        Public Property DemoName As String

        ''' <summary>
        ''' True if the entry has a demo form, false if not
        ''' </summary>
        Public Property HasDemoYN As Boolean

        ''' <summary>
        ''' True to use the control image for the menu form, false if not
        ''' </summary>
        Public Property UseControlImageYN As Boolean

        ''' <summary>
        ''' The demo description
        ''' </summary>
        Public Property DemoDesc As String
    
    End Class
End Namespace
