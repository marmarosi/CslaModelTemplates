using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Endpoints.SimpleEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Simple
{
    public class SimpleTeam_Tests
    {
        #region New

        [Fact]
        public async Task CreateTeam_ReturnsNewModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<New>();
            var sut = new New(logger);

            // Act
            ActionResult<SimpleTeamDto> actionResult = await sut.HandleAsync(new CancellationToken());

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
            var logger = setup.GetLogger<Create>();
            var sut = new Create(logger);

            // Act
            ActionResult<SimpleTeamDto> actionResult;
            SimpleTeamDto pristineTeam;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                pristineTeam = new SimpleTeamDto
                {
                    TeamKey = null,
                    TeamCode = "T-9001",
                    TeamName = "Test team number 9001",
                    Timestamp = null
                };
                actionResult = await sut.HandleAsync(pristineTeam, new CancellationToken());

                scope.Dispose();
            }

            // Assert
            CreatedResult createdResult = actionResult.Result as CreatedResult;
            Assert.NotNull(createdResult);

            SimpleTeamDto createdTeam = createdResult.Value as SimpleTeamDto;
            Assert.NotNull(createdTeam);

            // The model must have new values.
            Assert.NotNull(createdTeam.TeamKey);
            Assert.Equal(pristineTeam.TeamCode, createdTeam.TeamCode);
            Assert.Equal(pristineTeam.TeamName, createdTeam.TeamName);
            Assert.NotNull(createdTeam.Timestamp);
        }

        #endregion

        #region Read & Update

        [Fact]
        public async Task UpdateTeam_ReturnsUpdatedModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var loggerR = setup.GetLogger<Read>();
            var loggerU = setup.GetLogger<Update>();
            var sutRead = new Read(loggerR);
            var sutUpdate = new Update(loggerU);

            using (var uow = setup.UnitOfWork())
            {
                // --- READ
                ActionResult<SimpleTeamDto> actionResult;
                OkObjectResult okObjectResult;

                // Act
                SimpleTeamCriteria criteria = new SimpleTeamCriteria { TeamKey = 22 };
                actionResult = await sutRead.HandleAsync(criteria, new CancellationToken());

                // Assert
                okObjectResult = actionResult.Result as OkObjectResult;
                Assert.NotNull(okObjectResult);

                SimpleTeamDto pristine = okObjectResult.Value as SimpleTeamDto;
                Assert.NotNull(pristine);

                // The code and name must end with 22.
                Assert.Equal(22, pristine.TeamKey);
                Assert.Equal("T-0022", pristine.TeamCode);
                Assert.EndsWith("22", pristine.TeamName);
                Assert.NotNull(pristine.Timestamp);

                // --- UPDATE
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    pristine.TeamCode = "T-9002";
                    pristine.TeamName = "Test team number 9002";
                    actionResult = await sutUpdate.HandleAsync(pristine, new CancellationToken());

                    scope.Dispose();
                }

                // Assert
                okObjectResult = actionResult.Result as OkObjectResult;
                Assert.NotNull(okObjectResult);

                SimpleTeamDto updated = okObjectResult.Value as SimpleTeamDto;
                Assert.NotNull(updated);

                // The team must have new values.
                Assert.Equal(pristine.TeamKey, updated.TeamKey);
                Assert.Equal(pristine.TeamCode, updated.TeamCode);
                Assert.Equal(pristine.TeamName, updated.TeamName);
                Assert.NotEqual(pristine.Timestamp, updated.Timestamp);
            }
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
            ActionResult actionResult;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                SimpleTeamCriteria criteria = new SimpleTeamCriteria { TeamKey = 44 };
                actionResult = await sut.HandleAsync(criteria, new CancellationToken());

                scope.Dispose();
            }

            // Assert
            NoContentResult noContentResult = actionResult as NoContentResult;
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        #endregion
    }
}
