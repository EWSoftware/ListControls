'===============================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : Address.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/06/2024
' Note    : Copyright 2024, Eric Woodruff, All rights reserved
'
' This is class is used to contain name and address information
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

Imports System.Runtime.CompilerServices

Namespace Database

    ''' <summary>
    ''' This is used to contain name and address information
    ''' </summary>
    Friend NotInheritable Class Address
      Implements INotifyPropertyChanging, INotifyPropertyChanged

        #Region "Private data members"

        Dim idValue As Integer
        Dim firstNameValue, lastNameValue, streetAddressValue, cityValue, stateValue, zipValue As String
        Dim sum As Integer?
        Dim domesticValue, internationalValue, postalValue, parcelValue, homeValue, businessValue As Boolean
        Dim contactTypeValue As Char?

        #End Region

        #Region "Properties"
        '=====================================================================

        ''' <summary>
        ''' This is used to get or set the ID
        ''' </summary>
        <Key>
        Public Property ID As Integer
            Get
                Return idValue
            End Get
            Set(ByVal value As Integer)
                Me.SetWithNotify(value, idValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the first name
        ''' </summary>
        public Property FirstName As String
            Get
                Return firstNameValue
            End Get
            Set(ByVal value As String)
                Me.SetWithNotify(value, firstNameValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the last name
        ''' </summary>
        Public Property LastName As String
            Get
                Return lastNameValue
            End Get
            Set(ByVal value As String)
                Me.SetWithNotify(value, lastNameValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the street address
        ''' </summary>
        Public Property StreetAddress As String
            Get
                Return streetAddressValue
            End Get
            Set(ByVal value As String)
                If String.IsNullOrWhiteSpace(value) Then
                    value = Nothing
                End If

                Me.SetWithNotify(value, streetAddressValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the city
        ''' </summary>
        Public Property City As String
            Get
                Return cityValue
            End Get
            Set(ByVal value As String)
                If String.IsNullOrWhiteSpace(value) Then
                    value = Nothing
                End If

                Me.SetWithNotify(value, cityValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the state
        ''' </summary>
        Public Property State As String
            Get
                Return stateValue
            End Get
            Set(ByVal value As String)
                If String.IsNullOrWhiteSpace(value) Then
                    value = Nothing
                End If

                Me.SetWithNotify(value, stateValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the ZIP code
        ''' </summary>
        Public Property Zip As String
            Get
                Return zipValue
            End Get
            Set(ByVal value As String)
                If String.IsNullOrWhiteSpace(value) Then
                    value = Nothing
                End If

                Me.SetWithNotify(value, zipValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the sum value
        ''' </summary>
        Public Property SumValue As Integer?
            Get
                Return sum
            End Get
            Set(ByVal value As Integer?)
                Me.SetWithNotify(value, sum)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the domestic address flag
        ''' </summary>
        Public Property Domestic As Boolean
            Get
                Return domesticValue
            End Get
            Set(ByVal value As Boolean)
                Me.SetWithNotify(value, domesticValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the international address flag
        ''' </summary>
        Public Property International As Boolean
            Get
                Return internationalValue
            End Get
            Set(ByVal value As Boolean)
                Me.SetWithNotify(value, internationalValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the postal address flag
        ''' </summary>
        Public Property Postal As Boolean
            Get
                Return postalValue
            End Get
            Set(ByVal value As Boolean)
                Me.SetWithNotify(value, postalValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the parcel address flag
        ''' </summary>
        Public Property Parcel As Boolean
            Get
                Return parcelValue
            End Get
            Set(ByVal value As Boolean)
                Me.SetWithNotify(value, parcelValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the home address flag
        ''' </summary>
        Public Property Home As Boolean
            Get
                Return homeValue
            End Get
            Set(ByVal value As Boolean)
                Me.SetWithNotify(value, homeValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the business address flag
        ''' </summary>
        Public Property Business As Boolean
            Get
                Return businessValue
            End Get
            Set(ByVal value As Boolean)
                Me.SetWithNotify(value, businessValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the contact type
        ''' </summary>
        Public Property ContactType As Char?
            Get
                Return contactTypeValue
            End Get
            Set(ByVal value As Char?)
                Me.SetWithNotify(value, contactTypeValue)
            End Set
        End Property

        ''' <summary>
        ''' This is used to get or set the row time stamp (row version)
        ''' </summary>
        <Timestamp>
        Public Property LastModified As Byte()

        ''' <summary>
        ''' The phone numbers related to this address
        ''' </summary>
        Public Property PhoneNumbers As ICollection(Of Phone) = New List(Of Phone)()

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
