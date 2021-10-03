using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;

namespace CslaModelTemplates.BuildDemoDb.Configuration
{
    /// <summary>
    /// Provides method to add shared settings to configuration.
    /// </summary>
    public static class WebHostBuilderSharedSettings
    {
        /// <summary>
        /// Adds shared settings to configuration.
        /// </summary>
        /// <param name="builder">The host builder.</param>
        /// <param name="sharedFileName">The path of the shared file.</param>
        /// <returns></returns>
        public static IHostBuilder AddSharedSettings(
            this IHostBuilder builder,
            string sharedFileName
            )
        {
            if (builder == null || String.IsNullOrEmpty(sharedFileName))
                return builder;

            // Modify the config files being used.
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var fileStub = Path.GetFileNameWithoutExtension(sharedFileName);
                var fileExt = Path.GetExtension(sharedFileName);

                var fileNames = new List<string>
                {
                    sharedFileName,
                    $"{fileStub}.{hostingContext.HostingEnvironment.EnvironmentName}{fileExt}"
                };

                var sharedConfigs = new List<JsonConfigurationSource>();

                // First settings files are the ones in the shared folder
                // that get found when run via dotnet run.
                var parentDir = Directory.GetParent(hostingContext.HostingEnvironment.ContentRootPath);

                foreach (var fileName in fileNames)
                {
                    var filePath = Path.Combine(parentDir.FullName, fileName);

                    if (File.Exists(filePath))
                    {
                        sharedConfigs.Add(new JsonConfigurationSource()
                        {
                            Path = filePath,
                            Optional = true,
                            ReloadOnChange = true
                        });
                    }
                }

                // Second settings files are the linked shared settings files found when
                // the site is published.
                foreach (var fileName in fileNames)
                {
                    var filePath = Path.Combine(hostingContext.HostingEnvironment.ContentRootPath, fileName);

                    if (File.Exists(filePath))
                    {
                        sharedConfigs.Add(new JsonConfigurationSource()
                        {
                            Path = filePath,
                            Optional = true,
                            ReloadOnChange = true
                        });
                    }
                }

                // Create the file providers, since we didn't specify one explicitly.
                sharedConfigs.ForEach(x => x.ResolveFileProvider());

                if (config.Sources.Count > 0)
                {
                    for (var idx = 0; idx < sharedConfigs.Count; idx++)
                    {
                        config.Sources.Insert(idx, sharedConfigs[idx]);
                    }
                }
                else sharedConfigs.ForEach(x => { config.Add(x); });

                // all other setting files (e.g., appsettings.json) appear afterwards
                config
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
            });

            return builder;
        }
    }
}
