'===============================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : DemoDataContext.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/08/2024
' Note    : Copyright 2024, Eric Woodruff, All rights reserved
'
' This is class contains the demo data context
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

Namespace Database

    ''' <summary>
    ''' This is the demo data context
    ''' </summary>
    Friend NotInheritable Class DemoDataContext
        Inherits DbContext

        #Region "Properties"
        '=====================================================================

        ''' <summary>
        ''' This is used to set the database location for the demo
        ''' </summary>
        Public Shared Property DatabaseLocation As String

        #End Region

        #Region "Data entities"
        '=====================================================================

        ''' <summary>
        ''' The demo information
        ''' </summary>
        Public Property DemoInfo As DbSet(Of DemoInfo)

        ''' <summary>
        ''' Some demo data for the example forms
        ''' </summary>
        Public Property DemoTable As DbSet(Of DemoTable)

        ''' <summary>
        ''' Some demo data for the example forms
        ''' </summary>
        Public Property ProductInfo As DbSet(Of ProductInfo)

        ''' <summary>
        ''' Address data for the example forms
        ''' </summary>
        Public Property Addresses As DbSet(Of Address)

        ''' <summary>
        ''' Phone number data for the example forms
        ''' </summary>
        Public Property PhoneNumbers As DbSet(Of Phone)

        ''' <summary>
        ''' State codes for the example forms
        ''' </summary>
        public Property StateCodes As DbSet(Of StateCode)

        #End Region

        #Region "Method overrides"
        '=====================================================================

        ''' <inheritdoc />
        Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
        
            modelBuilder.Entity(Of DemoInfo)().HasKey(Function(di) New With { di.DemoOrder, di.DemoName })

            ' Make the table names match the entity types not the DbSet<T> property names
            For Each entityType In modelBuilder.Model.GetEntityTypes()
                If entityType.ClrType IsNot Nothing AndAlso entityType.GetTableName() <> entityType.ClrType.Name Then
                    entityType.SetTableName(entityType.ClrType.Name)
                End If
            Next

            modelBuilder.Entity(Of Address)().HasMany(Function (e) e.PhoneNumbers).WithOne( _
                Function (e) e.Address).HasForeignKey(Function (e) e.ID).HasPrincipalKey(Function (e) e.ID)

            MyBase.OnModelCreating(modelBuilder)
        End Sub

        ''' <inheritdoc />
        ''' <remarks>For this demo, the database is assumed to be in the root project folder and is accessed
        ''' using LocalDB</remarks>
        Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)

            If String.IsNullOrWhiteSpace(DatabaseLocation) Then
                Throw New InvalidOperationException("The database location has not been set")
            End If

            optionsBuilder.UseSqlServer($"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog={DatabaseLocation};Integrated Security=True")

            MyBase.OnConfiguring(optionsBuilder)
        End Sub
        #End Region

    End Class
End Namespace
