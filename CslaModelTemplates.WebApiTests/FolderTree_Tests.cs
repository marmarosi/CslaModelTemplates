using CslaModelTemplates.Contracts.Tree;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<TreeController>();
            var sut = new TreeController(logger);

            // Act
            FolderTreeCriteria criteria = new FolderTreeCriteria { RootKey = 1 };
            IActionResult actionResult = await sut.GetFolderTreeAsync(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<FolderNodeDto> tree = okObjectResult.Value as List<FolderNodeDto>;
            Assert.NotNull(tree);

            // The tree must have one root node.
            Assert.Single(tree);

            // Level 1 - root node
            FolderNodeDto nodeLevel1 = tree[0];
            Assert.Equal(1, nodeLevel1.Level);
            Assert.True(nodeLevel1.Children.Count > 0);

            // Level 2
            FolderNodeDto nodeLevel2 = nodeLevel1.Children[0];
            Assert.Equal(2, nodeLevel2.Level);
            Assert.True(nodeLevel2.Children.Count > 0);

            // Level 3
            FolderNodeDto nodeLevel3 = nodeLevel2.Children[0];
            Assert.Equal(3, nodeLevel3.Level);
            Assert.True(nodeLevel3.Children.Count > 0);

            // Level 4
            FolderNodeDto nodeLevel4 = nodeLevel3.Children[0];
            Assert.Equal(4, nodeLevel4.Level);
            Assert.Empty(nodeLevel4.Children);
        }
    }
}
