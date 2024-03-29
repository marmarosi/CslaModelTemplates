using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests.Simple
{
    public class SimpleTeam_Tests
    {
        #region New

        [Fact]
        public async Task NewTeam_ReturnsNewModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // Act
            ActionResult<SimpleTeamDto> actionResult = await sut.GetNewTeam();

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            SimpleTeamDto team = okObjectResult.Value as SimpleTeamDto;
            Assert.NotNull(team);

            // The code and name must miss.
            Assert.Empty(team.TeamCode);
            Assert.Empty(team.TeamName);
            Assert.Null(team.Timestamp);
        }

        #endregion

        #region Create

        [Fact]
        public async Task CreateTeam_ReturnsCreatedModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // Act
            SimpleTeamDto pristineTeam = null;
            ActionResult<SimpleTeamDto> actionResult = await Call<SimpleTeamDto>.RetryOnDeadlock(async () =>
            {
                pristineTeam = new SimpleTeamDto
                {
                    TeamId = null,
                    TeamCode = "T-9001",
                    TeamName = "Test team number 9001",
                    Timestamp = null
                };
                return await sut.CreateTeam(pristineTeam);
            });

            // Assert
            CreatedResult createdResult = actionResult.Result as CreatedResult;
            Assert.NotNull(createdResult);

            SimpleTeamDto createdTeam = createdResult.Value as SimpleTeamDto;
            Assert.NotNull(createdTeam);

            // The model must have new values.
            Assert.NotNull(createdTeam.TeamId);
            Assert.Equal(pristineTeam.TeamCode, createdTeam.TeamCode);
            Assert.Equal(pristineTeam.TeamName, createdTeam.TeamName);
            Assert.NotNull(createdTeam.Timestamp);
        }

        #endregion

        #region Read

        [Fact]
        public async Task ReadTeam_ReturnsCurrentModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // Act
            SimpleTeamParams criteria = new SimpleTeamParams { TeamId = "zXayGQW0bZv" };
            ActionResult<SimpleTeamDto> actionResult = await sut.GetTeam(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            SimpleTeamDto pristine = okObjectResult.Value as SimpleTeamDto;
            Assert.NotNull(pristine);

            // The code and name must end with 22.
            Assert.Equal("zXayGQW0bZv", pristine.TeamId);
            Assert.Equal("T-0022", pristine.TeamCode);
            Assert.EndsWith("22", pristine.TeamName);
            Assert.NotNull(pristine.Timestamp);
        }

        #endregion

        #region Update

        [Fact]
        public async Task UpdateTeam_ReturnsUpdatedModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sutR = new SimpleController(logger);
            var sutU = new SimpleController(logger);

            // Act
            SimpleTeamDto pristine = null;
            ActionResult<SimpleTeamDto> actionResult = await Call<SimpleTeamDto>.RetryOnDeadlock(async () =>
            {
                SimpleTeamParams criteria = new SimpleTeamParams { TeamId = "zXayGQW0bZv" };
                ActionResult<SimpleTeamDto> actionResult = await sutR.GetTeam(criteria);
                OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
                pristine = okObjectResult.Value as SimpleTeamDto;

                pristine.TeamCode = "T-9002";
                pristine.TeamName = "Test team number 9002";

                return await sutU.UpdateTeam(pristine);
            });

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            SimpleTeamDto updated = okObjectResult.Value as SimpleTeamDto;
            Assert.NotNull(updated);

            // The team must have new values.
            Assert.Equal(pristine.TeamId, updated.TeamId);
            Assert.Equal(pristine.TeamCode, updated.TeamCode);
            Assert.Equal(pristine.TeamName, updated.TeamName);
            Assert.NotEqual(pristine.Timestamp, updated.Timestamp);
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteTeam_ReturnsNothing()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // Act
            ActionResult actionResult = await Run.RetryOnDeadlock(async () =>
            {
                SimpleTeamParams criteria = new SimpleTeamParams { TeamId = "rWqG7KpG5Qo" };
                return await sut.DeleteTeam(criteria);
            });

            // Assert
            NoContentResult noContentResult = actionResult as NoContentResult;
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        #endregion
    }
}
