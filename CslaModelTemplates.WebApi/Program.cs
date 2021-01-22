using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CslaModelTemplates.WebApi
{
    /// <summary>
    /// Application bootstrap.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">The startup arguments.</param>
        public static void Main(
            string[] args
            )
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Calls the application startup.
        /// </summary>
        /// <param name = "args" > The startup arguments.</param>
        /// <returns>The web host builder.</returns>
        public static IHostBuilder CreateHostBuilder(
            string[] args
            ) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
    }
}
