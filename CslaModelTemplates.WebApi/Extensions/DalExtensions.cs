using CslaModelTemplates.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CslaModelTemplates.WebApi.Extensions
{
    /// <summary>
    /// Provides methods to configure data access layers.
    /// </summary>
    public static class DalExtensions
    {
        /// <summary>
        /// Adds configuration for data access layers.
        /// </summary>
        /// <param name="services">The container of the application services.</param>
        /// <param name="configuration">The configuration of the application.</param>
        public static void AddDalConfig(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            DalFactory.Configure(configuration, services);
        }

        /// <summary>
        /// Runs seeders of persistent storages.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="environment">The hosting environment.</param>
        public static void RunSeeders(
            this IApplicationBuilder app,
            IWebHostEnvironment environment
            )
        {
            if ((environment as IHostEnvironment).IsDevelopment())
            {
                DalFactory.SeedDevelopmentData(environment.ContentRootPath);
            }
            else
            {
                DalFactory.SeedProductionData(environment.ContentRootPath);
            }
        }
    }
}
