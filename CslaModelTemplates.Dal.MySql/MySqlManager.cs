using Csla.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;

namespace CslaModelTemplates.Dal.MySql
{
    /// <summary>
    /// Represents the data access manager object for MySQL databases.
    /// </summary>
    public sealed class MySqlManager : DalManagerBase<MySqlContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlManager"/> class.
        /// </summary>
        public MySqlManager()
        {
            SetTypes<MySqlManager>();
            ContextManager = DbContextManager<MySqlContext>.GetManager(DAL.MySQL);
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
            services.AddDbContext<MySqlContext>(options =>
                options.UseMySQL(
                    configuration.GetConnectionString(DAL.MySQL)
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
            return ex is MySqlException && (ex as MySqlException).Number == 1213;
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
            MySqlSeeder.Run(contentRootPath, false);
        }

        /// <summary>
        /// Ensures the database schema and fills it with demo data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public override void SeedDevelopmentData(
            string contentRootPath
            )
        {
            MySqlSeeder.Run(contentRootPath, true);
        }

        #endregion
    }
}
