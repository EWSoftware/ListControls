'===============================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : StateCode.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/06/2024
' Note    : Copyright 2024, Eric Woodruff, All rights reserved
'
' This is class is used to contain state code values
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ==============================================================================================================
' 12/06/2024  EFW  Created the code
'===============================================================================================================

' This class is used but is created dynamically when needed
#Disable Warning CA1812

Namespace Database

    ''' <summary>
    ''' This is used to contain state code values
    ''' </summary>
    Friend NotInheritable Class StateCode
        ''' <summary>
        ''' This is used to get or set the state code
        ''' </summary>
        <Key>
        Public Property State As String

        ''' <summary>
        ''' This is used to get or set the state description
        ''' </summary>
        public Property StateDesc As String
    
    End Class

End Namespace