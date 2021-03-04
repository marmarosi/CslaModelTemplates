using CslaModelTemplates.Contracts.SimpleList;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests
{
    public class SimpleTeamList_Tests
    {
        [Fact]
        public async Task GetTeamList_ReturnsAList()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // Act
            SimpleTeamListCriteria criteria = new SimpleTeamListCriteria { TeamName = "9" };
            IActionResult actionResult = await sut.GetTeamList(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
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
