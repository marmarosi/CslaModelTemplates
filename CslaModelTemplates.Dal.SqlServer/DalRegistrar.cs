using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CslaModelTemplates.Dal.SqlServer
{
    /// <summary>
    /// Provides method to configure the data access layer context to use SQL Server.
    /// </summary>
    public class DalRegistrar : IDalRegistrar
    {
        /// <summary>
        /// Registers the data access layer constext as a service.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="services">The container of the application services.</param>
        public void AddDalContext(
            IConfiguration configuration,
            IServiceCollection services
            )
        {
            services.AddDbContext<SqlServerContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(DAL.SQLServer)
                    )
                );
        }
    }
}
