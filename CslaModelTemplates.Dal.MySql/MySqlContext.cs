using CslaModelTemplates.Dal.MySql.Entities;
using Microsoft.EntityFrameworkCore;

namespace CslaModelTemplates.Dal.MySql
{
    /// <summary>
    /// Represents a session with tha database.
    /// </summary>
    public class MySqlContext : DbContextBase
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public MySqlContext() : base()
        {
            ConnectionString = DalFactory.GetConnectionString(DAL.MySQL);
        }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="dalName">The name of the data access layer.</param>
        public MySqlContext(
            string dalName
            ) : base(dalName)
        { }

        #endregion

        /// <summary>
        /// Configure the database to be used for this context.
        /// </summary>
        /// <param name="optionsBuilder">The builder used to create or modify options for this context.</param>
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder
            )
        {
            optionsBuilder.UseMySQL(ConnectionString);
        }

        #region Query results

        public DbSet<Root> Roots { get; set; }
        public DbSet<RootItem> RootItems { get; set; }
        public DbSet<Folder> Folders { get; set; }

        #endregion

        /// <summary>
        /// Configure the model discovered by convention from the entity type..
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(
            ModelBuilder modelBuilder
            )
        {
            #region Root

            modelBuilder.Entity<Root>()
                .HasIndex(e => e.RootCode)
                .IsUnique();

            #endregion

            #region RootItem

            modelBuilder.Entity<RootItem>()
                .HasIndex(e => new { e.RootKey, e.RootItemCode })
                .IsUnique();

            #endregion

            #region Node

            modelBuilder.Entity<Folder>()
                .HasIndex(e => new { e.ParentKey, e.FolderOrder });

            #endregion
        }
    }
}
