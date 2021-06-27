using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Endpoints.ComplexEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Complex
{
    public class Team_Tests
    {
        #region New

        [Fact]
        public async Task NewTeam_ReturnsNewModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<New>();
            var sut = new New(logger);

            // Act
            ActionResult<TeamDto> actionResult = await sut.HandleAsync(new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            TeamDto team = okObjectResult.Value as TeamDto;
            Assert.NotNull(team);

            // The code and name must miss.
            Assert.Empty(team.TeamCode);
            Assert.Empty(team.TeamName);
            Assert.Null(team.Timestamp);
            Assert.Empty(team.Players);
        }

        #endregion

        #region Create

        [Fact]
        public async Task CreateTeam_ReturnsCreatedModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<Create>();
            var sut = new Create(logger);

            // Act
            TeamDto pristineTeam = null;
            PlayerDto pristinePlayer1 = null;
            PlayerDto pristinePlayer2 = null;
            var actionResult = await Call<TeamDto>.RetryOnDeadlock(async () =>
            {
                pristineTeam = new TeamDto
                {
                    TeamKey = null,
                    TeamCode = "T-9201",
                    TeamName = "Test team number 9201",
                    Timestamp = null
                };
                pristinePlayer1 = new PlayerDto
                {
                    PlayerKey = null,
                    TeamKey = null,
                    PlayerCode = "P-9201-1",
                    PlayerName = "Test player #1"
                };
                pristineTeam.Players.Add(pristinePlayer1);
                pristinePlayer2 = new PlayerDto
                {
                    PlayerKey = null,
                    TeamKey = null,
                    PlayerCode = "P-9201-2",
                    PlayerName = "Test player #2"
                };
                pristineTeam.Players.Add(pristinePlayer2);

                return await sut.HandleAsync(pristineTeam, new CancellationToken());
            });

            // Assert
            CreatedResult createdResult = actionResult.Result as CreatedResult;
            Assert.NotNull(createdResult);

            TeamDto createdTeam = createdResult.Value as TeamDto;
            Assert.NotNull(createdTeam);

            // The team must have new values.
            Assert.NotNull(createdTeam.TeamKey);
            Assert.Equal(pristineTeam.TeamCode, createdTeam.TeamCode);
            Assert.Equal(pristineTeam.TeamName, createdTeam.TeamName);
            Assert.NotNull(createdTeam.Timestamp);

            // The players must have new values.
            Assert.Equal(2, createdTeam.Players.Count);

            PlayerDto createdPlayer1 = createdTeam.Players[0];
            Assert.NotNull(createdPlayer1.PlayerKey);
            Assert.Equal(createdTeam.TeamKey, createdPlayer1.TeamKey);
            Assert.Equal(pristinePlayer1.PlayerCode, createdPlayer1.PlayerCode);
            Assert.Equal(pristinePlayer1.PlayerName, createdPlayer1.PlayerName);

            PlayerDto createdPlayer2 = createdTeam.Players[1];
            Assert.NotNull(createdPlayer2.PlayerKey);
            Assert.Equal(createdTeam.TeamKey, createdPlayer2.TeamKey);
            Assert.Equal(pristinePlayer2.PlayerCode, createdPlayer2.PlayerCode);
            Assert.Equal(pristinePlayer2.PlayerName, createdPlayer2.PlayerName);
        }

        #endregion

        #region Read

        [Fact]
        public async Task ReadTeam_ReturnsCurrentModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<Read>();
            var sut = new Read(logger);

            // Act
            TeamCriteria criteria = new TeamCriteria { TeamKey = 19 };
            ActionResult<TeamDto> actionResult = await sut.HandleAsync(criteria, new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            TeamDto pristineTeam = okObjectResult.Value as TeamDto;
            Assert.NotNull(pristineTeam);

            // The team code and name must end with 19.
            Assert.Equal(19, pristineTeam.TeamKey);
            Assert.Equal("T-0019", pristineTeam.TeamCode);
            Assert.EndsWith("19", pristineTeam.TeamName);
            Assert.NotNull(pristineTeam.Timestamp);

            // The player codes and names must contain 19.
            Assert.True(pristineTeam.Players.Count > 0);
            PlayerDto pristinePlayer1 = pristineTeam.Players[0];
            foreach (PlayerDto player in pristineTeam.Players)
            {
                Assert.Equal(19, player.TeamKey);
                Assert.Contains("19", player.PlayerCode);
                Assert.Contains("19", player.PlayerName);
            }
        }

        #endregion

        #region Update

        [Fact]
        public async Task UpdateTeam_ReturnsUpdatedModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var loggerR = setup.GetLogger<Read>();
            var loggerU = setup.GetLogger<Update>();
            var sutR = new Read(loggerR);
            var sutU = new Update(loggerU);

            // Act
            TeamDto pristineTeam = null;
            PlayerDto pristinePlayer1 = null;
            PlayerDto pristinePlayerNew = null;
            var actionResult = await Call<TeamDto>.RetryOnDeadlock(async () =>
            {
                TeamCriteria criteria = new TeamCriteria { TeamKey = 19 };
                ActionResult<TeamDto> actionResult = await sutR.HandleAsync(criteria, new CancellationToken());
                OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
                pristineTeam = okObjectResult.Value as TeamDto;
                pristinePlayer1 = pristineTeam.Players[0];

                pristineTeam.TeamCode = "T-9202";
                pristineTeam.TeamName = "Test team number 9202";
                pristinePlayer1.PlayerCode = "P-9202-1";
                pristinePlayer1.PlayerName = "Test player #9202.1";

                pristinePlayerNew = new PlayerDto
                {
                    PlayerKey = null,
                    TeamKey = null,
                    PlayerCode = "P-9202-X",
                    PlayerName = "Test player #9202.X"
                };
                pristineTeam.Players.Add(pristinePlayerNew);

                return await sutU.HandleAsync(pristineTeam, new CancellationToken());
            });

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            TeamDto updatedTeam = okObjectResult.Value as TeamDto;
            Assert.NotNull(updatedTeam);

            // The team must have new values.
            Assert.Equal(pristineTeam.TeamKey, updatedTeam.TeamKey);
            Assert.Equal(pristineTeam.TeamCode, updatedTeam.TeamCode);
            Assert.Equal(pristineTeam.TeamName, updatedTeam.TeamName);
            Assert.NotEqual(pristineTeam.Timestamp, updatedTeam.Timestamp);

            Assert.Equal(pristineTeam.Players.Count, updatedTeam.Players.Count);

            // Players must reflect the changes.
            PlayerDto updatedPlayer1 = updatedTeam.Players[0];
            Assert.Equal(pristinePlayer1.PlayerCode, updatedPlayer1.PlayerCode);
            Assert.Equal(pristinePlayer1.PlayerName, updatedPlayer1.PlayerName);

            PlayerDto createdPlayerNew = updatedTeam.Players[pristineTeam.Players.Count - 1];
            Assert.Equal(pristinePlayerNew.PlayerCode, createdPlayerNew.PlayerCode);
            Assert.Equal(pristinePlayerNew.PlayerName, createdPlayerNew.PlayerName);
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteTeam_ReturnsNothing()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<Delete>();
            var sut = new Delete(logger);

            // Act
            ActionResult actionResult = await Run.RetryOnDeadlock(async () =>
            {
                TeamCriteria criteria = new TeamCriteria { TeamKey = 8 };
                return await sut.HandleAsync(criteria, new CancellationToken());
            });

            // Assert
            NoContentResult noContentResult = actionResult as NoContentResult;
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        #endregion
    }
}
