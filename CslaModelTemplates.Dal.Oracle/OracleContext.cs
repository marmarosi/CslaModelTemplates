using CslaModelTemplates.Dal.Oracle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace CslaModelTemplates.Dal.Oracle
{
    /// <summary>
    /// Represents a session with the database.
    /// </summary>
    public class OracleContext : DbContextBase
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public OracleContext() : base()
        {
            ConnectionString = DalFactory.GetConnectionString(DAL.Oracle);
        }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="dalName">The name of the data access layer.</param>
        public OracleContext(
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
            optionsBuilder.UseOracle(ConnectionString);
        }

        #region Auto update timestamps

        void SubscribeStateChangeEvents()
        {
            ChangeTracker.Tracked += OnEntityTracked;
            ChangeTracker.StateChanged += OnEntityStateChanged;
        }

        void OnEntityStateChanged(object sender, EntityStateChangedEventArgs e)
        {
            ProcessLastModified(e.Entry);
        }

        void OnEntityTracked(object sender, EntityTrackedEventArgs e)
        {
            if (!e.FromQuery)
                ProcessLastModified(e.Entry);
        }

        void ProcessLastModified(EntityEntry entry)
        {
            if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
            {
                var property = entry.Metadata.FindProperty("Timestamp");
                if (property != null && property.ClrType == typeof(DateTime))
                    entry.CurrentValues[property] = DateTime.UtcNow;
            }
        }

        #endregion

        #region Query results

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<GroupPerson> GroupPersons { get; set; }

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

            #region Group

            modelBuilder.Entity<Group>()
                .HasIndex(e => e.GroupCode)
                .IsUnique();
            modelBuilder.Entity<Group>()
                .Property(e => e.Timestamp)
                .HasDefaultValue(DateTime.Now);

            #endregion

            #region Person

            modelBuilder.Entity<Person>()
                .HasIndex(e => e.PersonCode)
                .IsUnique();
            modelBuilder.Entity<Person>()
                .Property(e => e.Timestamp)
                .HasDefaultValue(DateTime.Now);

            #endregion

            #region GroupPerson

            modelBuilder.Entity<GroupPerson>()
                .HasKey(e => new { e.GroupKey, e.PersonKey });
            modelBuilder.Entity<GroupPerson>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(e => e.GroupKey);
            modelBuilder.Entity<GroupPerson>()
                .HasOne(e => e.Person)
                .WithMany(p => p.Memberships)
                .HasForeignKey(e => e.PersonKey);

            #endregion
        }
    }
}
