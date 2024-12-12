'===============================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : Phone.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/08/2024
' Note    : Copyright 2024, Eric Woodruff, All rights reserved
'
' This is class is used to contain phone number information
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ==============================================================================================================
' 12/08/2024  EFW  Created the code
'===============================================================================================================

' This class is used but is created dynamically when needed
#Disable Warning CA1812

Imports System.Runtime.CompilerServices

Namespace Database

    ''' <summary>
    ''' This is used to contain phone number information
    ''' </summary>
    Friend NotInheritable Class Phone
      Implements INotifyPropertyChanging, INotifyPropertyChanged

        #Region "Private data members"

        Dim phoneKeyValue, idValue As Integer
        Dim phoneNumberValue As String

        #End Region

        #Region "Properties"
        '=====================================================================

        ''' <summary>
        ''' This is used to get or set the phone key
        ''' </summary>
        <Key>
        Public Property PhoneKey As Integer
            Get
                Return phoneKeyValue
            End Get
            Set(ByVal value As Integer)
                Me.SetWithNotify(value, phoneKeyValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the related address key
        ''' </summary>
        Public Property ID As Integer
            Get
                Return idValue
            End Get
            Set(ByVal value As Integer)
                Me.SetWithNotify(value, idValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the phone number
        ''' </summary>
        public Property PhoneNumber As String
            Get
                Return phoneNumberValue
            End Get
            Set(ByVal value As String)
                Me.SetWithNotify(value, phoneNumberValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the row time stamp (row version)
        ''' </summary>
        <Timestamp>
        Public Property LastModified As Byte()

        ''' <summary>
        ''' The parent address of this phone number
        ''' </summary>
        public Property Address As Address

        #End Region

        #Region "Change notification interface implementations"
        '=====================================================================

        ''' <inheritdoc />
        Public Event PropertyChanging As PropertyChangingEventHandler _
            Implements INotifyPropertyChanging.PropertyChanging

        ''' <inheritdoc />
        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

        ''' <summary>
        ''' This is used to raise the <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> events
        ''' if the new value does not equal the current property's value
        ''' </summary>
        ''' <typeparam name="T">The property type</typeparam>
        ''' <param name="value">The new value</param>
        ''' <param name="field">A reference to the field containing the current value that will receive the
        ''' new value</param>
        ''' <param name="propertyName">The property name that changed.  This defaults to the calling member's
        ''' name if not specified</param>
        Private Sub SetWithNotify(Of T)(value As T, ByRef field As T, <CallerMemberName> Optional propertyName As String = "")
            If Not Equals(field, value) Then
                RaiseEvent PropertyChanging(Me, New PropertyChangingEventArgs(propertyName))
                field = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
            End If
        End Sub
        #End Region
    
    End Class
End Namespace
