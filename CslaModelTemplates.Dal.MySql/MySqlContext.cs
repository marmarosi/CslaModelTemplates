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
        public DbSet<Node> Nodes { get; set; }

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

            #region Node

            modelBuilder.Entity<Node>()
                .HasIndex(e => new { e.ParentKey, e.NodeOrder });

            #endregion
        }
    }
}
