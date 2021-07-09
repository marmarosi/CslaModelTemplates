using Csla.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Oracle.ManagedDataAccess.Client;

namespace CslaModelTemplates.Dal.Oracle
{
    /// <summary>
    /// Represents the data access manager object for Oracle databases.
    /// </summary>
    public sealed class OracleManager : DalManagerBase<OracleContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OracleManager"/> class.
        /// </summary>
        public OracleManager()
        {
            SetTypes<OracleManager>();
            ContextManager = DbContextManager<OracleContext>.GetManager(DAL.Oracle);
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
            services.AddDbContext<OracleContext>(options =>
                options.UseOracle(
                    configuration.GetConnectionString(DAL.Oracle)
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
            if (ex is OracleException)
            {
                switch ((ex as OracleException).Number)
                {
                    //case -2: /* Client Timeout */
                    //case 701: /* Out of Memory */
                    //case 1204: /* Lock Issue */
                    //case 1205: /* Deadlock Victim */
                    //case 1222: /* Lock Request Timeout */
                    //case 8645: /* Timeout waiting for memory resource */
                    //case 8651: /* Low memory condition */

                    case 104:  /* Deadlock detected; all public servers blocked waiting for resources */
                    case 1013: /* User requested cancel of current operation */
                    case 2087: /* Object locked by another process in same transaction */
                    case 60:   /* Deadlock detected while waiting for resource */
                        return true;
                    default:
                        break;
                }
            }
            return false;
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
            OracleSeeder.Run(contentRootPath, false);
        }

        /// <summary>
        /// Ensures the database schema and fills it with demo data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public override void SeedDevelopmentData(
            string contentRootPath
            )
        {
            OracleSeeder.Run(contentRootPath, true);
        }

        #endregion
    }
}
