using Csla.Data.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CslaModelTemplates.Dal.Sqlite
{
    /// <summary>
    /// Represents the data access manager object for SQL Server databases.
    /// </summary>
    public sealed class DalManager : DalManagerBase<SqliteContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DalManager"/> class.
        /// </summary>
        public DalManager()
        {
            SetTypes<DalManager>();
            ContextManager = DbContextManager<SqliteContext>.GetManager(DAL.SQLite);
        }

        /// <summary>
        /// Registers the data access layer constext as a service.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="services">The container of the application services.</param>
        public override void AddDalContext(
            IConfiguration configuration,
            IServiceCollection services
            )
        {
            services.AddDbContext<SqliteContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString(DAL.SQLite)
                    )
                );
        }

        /// <summary>
        /// CHecks whether the reason of the exception is a deadlock.
        /// </summary>
        /// <param name="ex">The original exception thrown.</param>
        /// <returns>True when the reason is a deadlock; otherwise false;</returns>
        public override bool HasDeadlock(Exception ex)
        {
            return ex is SqliteException && (ex as SqliteException).ErrorCode == 6; // SQLITE_LOCKED
        }

        #region ISeeder

        /// <summary>
        /// Ensures the database schema and fills it with initial data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public override void ProductionSeed(
            string contentRootPath
            )
        {
            SqliteSeeder.Run(contentRootPath, false);
        }

        /// <summary>
        /// Ensures the database schema and fills it with demo data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public override void DevelopmentSeed(
            string contentRootPath
            )
        {
            SqliteSeeder.Run(contentRootPath, true);
        }

        #endregion
    }
}
