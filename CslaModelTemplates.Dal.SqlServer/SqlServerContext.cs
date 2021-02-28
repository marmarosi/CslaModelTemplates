using CslaModelTemplates.Dal.SqlServer.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CslaModelTemplates.Dal.SqlServer
{
    /// <summary>
    /// Represents a session with the database.
    /// </summary>
    public class SqlServerContext : DbContextBase
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public SqlServerContext() : base()
        {
            ConnectionString = DalFactory.GetConnectionString(DAL.SQLServer);
        }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="dalName">The name of the data access layer.</param>
        public SqlServerContext(
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
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        #region Query results

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
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
            #region Team

            modelBuilder.Entity<Team>()
                .HasIndex(e => e.TeamCode)
                .IsUnique();
            modelBuilder.Entity<Team>()
                .Property(e => e.Timestamp)
                .HasDefaultValue(DateTime.Now);

            #endregion

            #region Player

            modelBuilder.Entity<Player>()
                .HasIndex(e => new { e.TeamKey, e.PlayerCode })
                .IsUnique();

            #endregion

            #region Folder

            modelBuilder.Entity<Folder>()
                .HasIndex(e => new { e.ParentKey, e.FolderOrder });
            modelBuilder.Entity<Folder>()
                .Property(e => e.Timestamp)
                .HasDefaultValue(DateTime.Now);

            #endregion
        }
    }
}
