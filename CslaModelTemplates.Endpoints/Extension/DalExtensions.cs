using CslaModelTemplates.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CslaModelTemplates.Endpoints.Extension
{
    /// <summary>
    /// Provides methods to configure data access layers.
    /// </summary>
    public static class DalExtensions
    {
        /// <summary>
        /// Add configuration for data access layers.
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
        /// Run seeders of persistent storages.
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
                DalFactory.DevelopmentSeed(environment.ContentRootPath);
            }
            else
            {
                DalFactory.ProductionSeed(environment.ContentRootPath);
            }
        }
    }
}
