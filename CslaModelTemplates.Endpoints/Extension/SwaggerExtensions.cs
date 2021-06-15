using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CslaModelTemplates.Endpoints.Extension
{
    /// <summary>
    /// Provides methods to configure swagger.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Add configuration for swagger.
        /// </summary>
        /// <param name="services">The container of the application services.</param>
        public static void AddSwaggerConfig(
            this IServiceCollection services
            )
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "CslaModelTemplates.Endpoints",
                        Version = "v1"
                    });
                c.EnableAnnotations();
            });
        }

        /// <summary>
        /// Add swagger to the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="environment">The hosting environment.</param>
        public static void UseSwaggerConfig(
            this IApplicationBuilder app,
            IHostEnvironment environment
            )
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "CslaModelTemplates.WebApi v1"
                        );
                    c.DocExpansion(DocExpansion.None);
                });
            }
        }
    }
}
