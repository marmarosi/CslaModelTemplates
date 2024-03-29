using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace CslaModelTemplates.Endpoints.Extensions
{
    /// <summary>
    /// Provides methods to handle exceptions.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Captures pipeline exceptions and generates HTML error response.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="environment"></param>
        public static void ShowExceptionDetails(
            this IApplicationBuilder app,
            IHostEnvironment environment
            )
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this
                // for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
        }
    }
}
