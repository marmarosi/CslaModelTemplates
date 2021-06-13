using CslaModelTemplates.Contracts.SimpleView;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests.Simple
{
    public class SimpleTeamView_Tests
    {
        [Fact]
        public async Task GetTeamView_ReturnsAView()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // Act
            SimpleTeamViewCriteria criteria = new SimpleTeamViewCriteria { TeamKey = 31 };
            ActionResult<SimpleTeamViewDto> actionResult = await sut.GetTeamView(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            SimpleTeamViewDto team = okObjectResult.Value as SimpleTeamViewDto;
            Assert.NotNull(team);

            // The code and name must end with 31.
            Assert.Equal("T-0031", team.TeamCode);
            Assert.EndsWith("31", team.TeamName);
        }
    }
}
