using CslaModelTemplates.Contracts.Tree;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Models.Tree;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests
{
    public class FolderTree_Tests
    {
        [Fact]
        public async Task GetFolderTree_ReturnsATree()
        {
            // Arrange
            var logger = Setup();
            var sut = new TreeController(logger);

            // Act
            FolderTreeCriteria criteria = new FolderTreeCriteria { RootKey = 1 };
            IActionResult actionResult = await sut.GetFolderTreeAsync(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            FolderTree tree = okObjectResult.Value as FolderTree;
            Assert.NotNull(tree);

            Assert.Single(tree);
        }

        #region Setup

        private ILogger<TreeController> Setup()
        {
            var configuration = GetConfig();

            // Configure strongly typed settings object.
            var dalSettingsSection = configuration.GetSection("DalSettings");
            DalSettings dalSettings = dalSettingsSection.Get<DalSettings>();

            // Create configured DAL manager types.
            DalFactory.Configure(dalSettings);

            // Create logger.
            return new NullLogger<TreeController>();
        }

        private IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true) // use appsettings.json in current folder
                .AddEnvironmentVariables();

            return builder.Build();
        }

        #endregion
    }
}
