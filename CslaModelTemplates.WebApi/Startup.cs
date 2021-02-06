using CslaModelTemplates.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO;

namespace CslaModelTemplates.WebApi
{
    /// <summary>
    /// The application launcher.
    /// </summary>
    public class Startup
    {
        private readonly IWebHostEnvironment Environment;

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
            DalFactory.Configure(Configuration, services);

            services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.IncludeFields = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo {
                        Title = "CslaModelTemplates.WebApi",
                        Version = "v1"
                    });
                string xmlFile = $"{Environment.ApplicationName}.xml";
                string xmlPath = Path.Combine(Environment.ContentRootPath, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DalFactory.TestSeedForAll(Environment.ContentRootPath);
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
            else
            {
                DalFactory.ProductionSeedForAll(Environment.ContentRootPath);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
