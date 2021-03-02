using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Dal.MySql;
using CslaModelTemplates.Dal.SqlServer;
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

            //// Configure strongly typed settings object.
            //IConfigurationSection dalSettingsSection = configuration.GetSection(DalFactory.Section);
            //DalSettings dalSettings = dalSettingsSection.Get<DalSettings>();

            //// Create configured DAL manager types.
            //DalFactory.Configure(dalSettings);
            DalFactory.Configure(configuration, _serviceCollection);
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

        public IServiceScope GetScope()
        {
            return _serviceProvider.CreateScope();
        }

        public IDisposable UnitOfWork()
        {
            return UnitOfWork(DalFactory.ActiveLayer);
        }

        public IDisposable UnitOfWork(
            string dalName
            )
        {
            switch (dalName)
            {
                case DAL.MySQL:
                    return DbContextManager<MySqlContext>.GetManager();
                case DAL.SQLServer:
                default:
                    return DbContextManager<SqlServerContext>.GetManager();
            }
        }
    }
}
