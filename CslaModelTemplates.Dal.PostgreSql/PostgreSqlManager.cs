using Csla.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;

namespace CslaModelTemplates.Dal.PostgreSql
{
    /// <summary>
    /// Represents the data access manager object for SQL Server databases.
    /// </summary>
    public sealed class PostgreSqlManager : DalManagerBase<PostgreSqlContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSqlManager"/> class.
        /// </summary>
        public PostgreSqlManager()
        {
            SetTypes<PostgreSqlManager>();
            ContextManager = DbContextManager<PostgreSqlContext>.GetManager(DAL.PostgreSQL);
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
            services.AddDbContext<PostgreSqlContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString(DAL.PostgreSQL)
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
            //return ex is PostgresException && (ex as PostgresException).Message == "deadlock detected";
            return ex is PostgresException && (ex as PostgresException).SqlState == "40P01";
        }

        #region ISeeder

        /// <summary>
        /// Ensures the database schema and fills it with initial data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public override void SeedProductionData(
            string contentRootPath
            )
        {
            PostgreSqlSeeder.Run(contentRootPath, false);
        }

        /// <summary>
        /// Ensures the database schema and fills it with demo data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public override void SeedDevelopmentData(
            string contentRootPath
            )
        {
            PostgreSqlSeeder.Run(contentRootPath, true);
        }

        #endregion
    }
}
