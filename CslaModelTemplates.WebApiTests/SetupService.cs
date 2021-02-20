using CslaModelTemplates.Dal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.IO;

namespace CslaModelTemplates.WebApiTests
{
    internal class SetupService
    {
        private static readonly SetupService _setupServiceInstance = new SetupService();

        private SetupService()
        {
            // Get the configuration.
            IConfiguration configuration = GetConfig();

            // Configure strongly typed settings object.
            IConfigurationSection dalSettingsSection = configuration.GetSection(DalFactory.Section);
            DalSettings dalSettings = dalSettingsSection.Get<DalSettings>();

            // Create configured DAL manager types.
            DalFactory.Configure(dalSettings);
        }

        public static SetupService GetInstance() => _setupServiceInstance;

        private IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true) // use appsettings.json in current folder
                .AddEnvironmentVariables();

            return builder.Build();
        }

        public ILogger<T> GetLogger<T>() where T : class
        {
            // Create logger.
            return new NullLogger<T>();
        }
    }
}
