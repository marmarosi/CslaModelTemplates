using CslaModelTemplates.Contracts.SimpleList;
using CslaModelTemplates.Endpoints.SimpleEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Simple
{
    public class SimpleTeamList_Tests
    {
        [Fact]
        public async Task GetTeamList_ReturnsAList()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<List>();
            var sut = new List(logger);

            // Act
            SimpleTeamListCriteria criteria = new SimpleTeamListCriteria { TeamName = "9" };
            ActionResult<IList<SimpleTeamListItemDto>> actionResult = await sut.HandleAsync(criteria, new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<SimpleTeamListItemDto> list = okObjectResult.Value as List<SimpleTeamListItemDto>;
            Assert.NotNull(list);

            // The list must have 5 items.
            Assert.Equal(5, list.Count);

            // The code and names must end with 9.
            foreach (var item in list)
            {
                Assert.EndsWith("9", item.TeamCode);
                Assert.EndsWith("9", item.TeamName);
            }
        }
    }
}
