using CslaModelTemplates.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CslaModelTemplates.WebApi
{
    /// <summary>
    /// The application launcher.
    /// </summary>
    public class Startup
    {
        private IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Application startup constructor.
        /// </summary>
        /// <param name="environment">The hosting environment.</param>
        /// <param name="configuration">The application configuration.</param>
        public Startup(
            IWebHostEnvironment environment,
            IConfiguration configuration
            )
        {
            Environment = environment;
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The container of the application services.</param>
        public void ConfigureServices(
            IServiceCollection services
            )
        {
            services.AddDalConfig(Configuration);
            services.AddEndpointServices();
            services.AddSwaggerDocuments(Environment);
        }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public void Configure(
            IApplicationBuilder app
            )
        {
            app.ShowExceptionDetails(Environment);
            app.RunSeeders(Environment);
            app.UseSwaggerEndpoint(Environment);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpointServices();
        }
    }
}
