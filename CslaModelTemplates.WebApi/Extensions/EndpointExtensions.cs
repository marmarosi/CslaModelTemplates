using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CslaModelTemplates.WebApi.Extensions
{
    /// <summary>
    /// Provides methods to configure endpoints.
    /// </summary>
    public static class EndpointExtensions
    {
        /// <summary>
        /// Adds services for controllers.
        /// </summary>
        /// <param name="services">The container of the application services.</param>
        public static void AddEndpointServices(
            this IServiceCollection services
            )
        {
            services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.IncludeFields = true;
                });
        }

        /// <summary>
        /// Adds endpoints to the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void UseEndpointServices(
            this IApplicationBuilder app
            )
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
