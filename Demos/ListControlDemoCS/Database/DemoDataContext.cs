//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : DemoDataContext.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/08/2024
// Note    : Copyright 2024, Eric Woodruff, All rights reserved
//
// This is class contains the demo data context
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/02/2024  EFW  Created the code
//===============================================================================================================

namespace ListControlDemoCS.Database
{
    /// <summary>
    /// This is the demo data context
    /// </summary>
    internal sealed class DemoDataContext : DbContext
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to set the database location for the demo
        /// </summary>
        public static string? DatabaseLocation { get; set; }

        #endregion

        #region Data entities
        //=====================================================================

        /// <summary>
        /// The demo information
        /// </summary>
        public DbSet<DemoInfo> DemoInfo { get; set; } = null!;

        /// <summary>
        /// Some demo data for the example forms
        /// </summary>
        public DbSet<DemoTable> DemoTable { get; set; } = null!;

        /// <summary>
        /// Some demo data for the example forms
        /// </summary>
        public DbSet<ProductInfo> ProductInfo { get; set; } = null!;

        /// <summary>
        /// Address data for the example forms
        /// </summary>
        public DbSet<Address> Addresses { get; set; } = null!;

        /// <summary>
        /// Phone number data for the example forms
        /// </summary>
        public DbSet<Phone> PhoneNumbers { get; set; } = null!;

        /// <summary>
        /// State codes for the example forms
        /// </summary>
        public DbSet<StateCode> StateCodes { get; set; } = null!;

        #endregion

        #region Method overrides
        //=====================================================================

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoInfo>().HasKey(di => new { di.DemoOrder, di.DemoName });

            // Make the table names match the entity types not the DbSet<T> property names
            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if(entityType.ClrType != null && entityType.GetTableName() != entityType.ClrType.Name)
                    entityType.SetTableName(entityType.ClrType.Name);
            }

            modelBuilder.Entity<Address>()
                .HasMany(e => e.PhoneNumbers)
                .WithOne(e => e.Address)
                .HasForeignKey(e => e.ID)
                .HasPrincipalKey(e => e.ID);

            base.OnModelCreating(modelBuilder);
        }

        /// <inheritdoc />
        /// <remarks>For this demo, the database is assumed to be in the root project folder and is accessed
        /// using LocalDB</remarks>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(String.IsNullOrWhiteSpace(DatabaseLocation))
                throw new InvalidOperationException("The database location has not been set");

            optionsBuilder.UseSqlServer($@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog={DatabaseLocation};Integrated Security=True");

            base.OnConfiguring(optionsBuilder);
        }
        #endregion
    }
}
