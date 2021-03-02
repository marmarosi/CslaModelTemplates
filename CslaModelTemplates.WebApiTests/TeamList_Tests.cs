using CslaModelTemplates.Contracts.ComplexList;
using CslaModelTemplates.Models.ComplexList;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests
{
    public class TeamList_Tests
    {
        [Fact]
        public async Task GetTeamList_ReturnsAList()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<ComplexController>();
            var sut = new ComplexController(logger);

            // Act
            TeamListCriteria criteria = new TeamListCriteria { TeamName = "6" };
            IActionResult actionResult = await sut.GetTeamList(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<TeamListItemDto> list = okObjectResult.Value as List<TeamListItemDto>;
            Assert.NotNull(list);

            // The choice must have 5 items.
            Assert.Equal(5, list.Count);

            // The team code and names must end with 6.
            foreach (var team in list)
            {
                Assert.EndsWith("6", team.TeamCode);
                Assert.EndsWith("6", team.TeamName);
                Assert.True(team.Players.Count > 0);

                // The player code and names must contain 6.
                PlayerListItemDto player = team.Players[0];
                Assert.Contains("6", player.PlayerCode);
                Assert.Contains("6.", player.PlayerName);
            }
        }
    }
}
