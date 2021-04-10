using Csla.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CslaModelTemplates.Dal.PostgreSql
{
    /// <summary>
    /// Represents the data access manager object for SQL Server databases.
    /// </summary>
    public sealed class DalManager : DalManagerBase<PostgreSqlContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DalManager"/> class.
        /// </summary>
        public DalManager()
        {
            SetTypes<DalManager>();
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

        #region ISeeder

        /// <summary>
        /// Ensures the database schema and fills it with initial data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public override void ProductionSeed(
            string contentRootPath
            )
        {
            PostgreSqlSeeder.Run(contentRootPath, false);
        }

        /// <summary>
        /// Ensures the database schema and fills it with demo data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public override void DevelopmentSeed(
            string contentRootPath
            )
        {
            PostgreSqlSeeder.Run(contentRootPath, true);
        }

        #endregion
    }
}
