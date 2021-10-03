using CslaModelTemplates.Dal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.IO;
using Xunit;

[assembly: CollectionBehavior(MaxParallelThreads = 1)]
namespace CslaModelTemplates.WebApiTests
{
    internal class SetupService
    {
        private const int MAX_RETRIES = 1;
        private const int MIN_DELAY_MS = 100;
        private const int MAX_DELAY_MS = 200;

        private static readonly SetupService _setupServiceInstance = new SetupService();
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);

        private readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private readonly IServiceProvider _serviceProvider;

        private SetupService()
        {
            // Get the configuration.
            IConfiguration configuration = GetConfig();

            // Initializes a new instance of ServiceProvider class.
            _serviceProvider = _serviceCollection.BuildServiceProvider();

            // Configure data access layers.
            DalFactory.Configure(configuration, _serviceCollection);
            if (DalFactory.ActiveLayer == DAL.SQLite)
                DalFactory.SeedDevelopmentData(null);
        }

        //public static SetupService GetInstance() => _setupServiceInstance;
        public static SetupService GetInstance()
        {
            return _setupServiceInstance;
        }

        private IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("SharedSettings.json", true, true)
                .AddJsonFile("appsettings.json", true, true) // use appsettings.json in current folder
                .AddEnvironmentVariables();

            return builder.Build();
        }

        public ILogger<T> GetLogger<T>() where T : class
        {
            // Create logger.
            return new NullLogger<T>();
        }

        public IServiceScope GetScope()
        {
            return _serviceProvider.CreateScope();
        }
    }
}
