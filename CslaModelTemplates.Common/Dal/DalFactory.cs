using CslaModelTemplates.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CslaModelTemplates.Common.Dal
{
    /// <summary>
    /// Factory of the CSLA data access objects.
    /// </summary>
    public static class DalFactory
    {
        private static StringDictionary Connections = new StringDictionary();
        private static Dictionary<string, Type> DalTypes = new Dictionary<string, Type>();
        private static string ActiveLayer;

        /// <summary>
        /// The name of the configuration section holding the DAL manager types.
        /// </summary>
        public const string Section = "DalSettings";

        #region Configure

        /// <summary>
        /// Configures and registers the data access layers.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="services">The container of the application services.</param>
        /// <param name="settings">The data access layer settings.</param>
        public static void Configure(
            IConfiguration configuration,
            IServiceCollection services
            )
        {
            var settings = configuration.GetSection(Section).Get<DalSettings>();
            Configure(settings);

            List<IDalRegistrar> dalRegistrars = GetRegistrars();
            foreach (var dalRegistrar in dalRegistrars)
                dalRegistrar.AddDalContext(configuration, services);
        }

        /// <summary>
        /// Configures the data access layers.
        /// </summary>
        /// <param name="settings">The data access layer settings.</param>
        public static void Configure(
            DalSettings settings
            )
        {
            if (settings.Layers.Count == 0)
                throw new ArgumentException(CommonText.DalFactory_DalManager_NoDatabases);

            foreach (KeyValuePair<string, LayerSettings> entry in settings.Layers)
            {
                if (string.IsNullOrEmpty(entry.Value.ConnectionString))
                    throw new NullReferenceException(CommonText.DalFactory_DalManager_NoConnStr.With(entry.Value));

                Connections.Add(entry.Key, entry.Value.ConnectionString);
                ResolveDalType(entry.Key, entry.Value.DalManagerType);

                if (ActiveLayer == null)
                    ActiveLayer = entry.Key;
            }
            if (!string.IsNullOrWhiteSpace(settings.ActiveLayer))
            {
                if (!DalTypes.ContainsKey(settings.ActiveLayer))
                    throw new ArgumentException(CommonText.DalFactory_DalManager_WrongKey.With(settings.ActiveLayer));

                ActiveLayer = settings.ActiveLayer;
            }
        }

        private static void ResolveDalType(
            string dalName,
            string dalTypeName
            )
        {
            Type dalType = null;

            if (!string.IsNullOrEmpty(dalTypeName))
                dalType = Type.GetType(dalTypeName);
            else
                throw new NullReferenceException(CommonText.DalFactory_DalManager_NoDalMngr.With(dalName));

            if (dalType == null)
                throw new ArgumentException(CommonText.DalFactory_DalManager_NotFound.With(dalTypeName));

            DalTypes.Add(dalName, dalType);
        }

        #endregion

        #region Registrars

        /// <summary>
        /// Gets the data access registrar of the active layer.
        /// </summary>
        /// <returns>The data access registrar object.</returns>
        public static IDalRegistrar GetRegistrar()
        {
            return GetRegistrar(ActiveLayer);
        }

        /// <summary>
        /// Gets the data access registrar with the specified name.
        /// </summary>
        /// <param name="dalName">The name of the data access layer.</param>
        /// <returns>The data access registrar object.</returns>
        public static IDalRegistrar GetRegistrar(
            string dalName
            )
        {
            IDalManager dalManager = Activator.CreateInstance(DalTypes[dalName]) as IDalManager;
            return dalManager.GetDalRegistrar();
        }

        /// <summary>
        /// Gets all data access registrars.
        /// </summary>
        /// <returns>A collection of data access registrar objects.</returns>
        public static List<IDalRegistrar> GetRegistrars()
        {
            List<IDalRegistrar> registrars = new List<IDalRegistrar>();
            foreach (KeyValuePair<string, Type> dalType in DalTypes)
            {
                IDalManager dalManager = Activator.CreateInstance(dalType.Value) as IDalManager;
                registrars.Add(dalManager.GetDalRegistrar());
            }
            return registrars;
        }

        #endregion

        #region Managers

        /// <summary>
        /// Gets the data access manager of the active layer.
        /// </summary>
        /// <returns>The data access manager object.</returns>
        public static IDalManager GetManager()
        {
            return GetManager(ActiveLayer);
        }

        /// <summary>
        /// Gets the data access manager with the specified name.
        /// </summary>
        /// <param name="dalName">The name of the data access layer.</param>
        /// <returns>The data access manager object.</returns>
        public static IDalManager GetManager(
            string dalName
            )
        {
            return Activator.CreateInstance(DalTypes[dalName]) as IDalManager;
        }

        #endregion

        #region ConnectionString

        /// <summary>
        /// Gets the connection string with the specified name.
        /// </summary>
        /// <param name="dalName">The name of the data access layer.</param>
        /// <returns>The connection string.</returns>
        public static string GetConnectionString(
            string dalName
            )
        {
            return Connections[dalName];
        }

        #endregion

        #region ProductionSeeds

        /// <summary>
        /// Ensures the initial data of the active layer.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static void ProductionSeed(
            string contentRootPath
            )
        {
            ProductionSeed(ActiveLayer, contentRootPath);
        }

        /// <summary>
        /// Ensures the initial data of the specified layer.
        /// </summary>
        /// <param name="dalName">The name of the data access layer.</param>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static void ProductionSeed(
            string dalName,
            string contentRootPath
            )
        {
            ISeeder seeder = Activator.CreateInstance(DalTypes[dalName]) as ISeeder;
            seeder.ProductionSeed(contentRootPath);
        }

        /// <summary>
        /// Ensures the initial data of all layers.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static void ProductionSeedForAll(
            string contentRootPath
            )
        {
            foreach (KeyValuePair<string, Type> dalType in DalTypes)
                ProductionSeed(dalType.Key, contentRootPath);
        }

        #endregion

        #region TestSeeds

        /// <summary>
        /// Ensures the demo data of the active layer.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static void TestSeed(
            string contentRootPath
            )
        {
            TestSeed(ActiveLayer, contentRootPath);
        }

        /// <summary>
        /// Ensures the demo data of the specified layer.
        /// </summary>
        /// <param name="dalName">The name of the data access layer.</param>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static void TestSeed(
            string dalName,
            string contentRootPath
            )
        {
            ISeeder seeder = Activator.CreateInstance(DalTypes[dalName]) as ISeeder;
            seeder.TestSeed(contentRootPath);
        }

        /// <summary>
        /// Ensures the demo data of all layers.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static void TestSeedForAll(
            string contentRootPath
            )
        {
            foreach (KeyValuePair<string, Type> dalType in DalTypes)
                TestSeed(dalType.Key, contentRootPath);
        }

        #endregion
    }
}
