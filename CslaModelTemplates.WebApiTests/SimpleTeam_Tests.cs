using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace CslaModelTemplates.WebApiTests
{
    public class SimpleTeam_Tests
    {
        #region New

        [Fact]
        public async Task CreateTeam_ReturnsNewModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // Act
            IActionResult actionResult = await sut.GetNewTeam();

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
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
            IActionResult actionResult;
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
                actionResult = await sut.CreateTeam(pristineTeam);

                scope.Dispose();
            }

            // Assert
            CreatedResult createdResult = actionResult as CreatedResult;
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
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            using (var uow = setup.UnitOfWork())
            {
                // --- READ
                IActionResult actionResult;
                OkObjectResult okObjectResult;

                // Act
                SimpleTeamCriteria criteria = new SimpleTeamCriteria { TeamKey = 22 };
                actionResult = await sut.GetTeam(criteria);

                // Assert
                okObjectResult = actionResult as OkObjectResult;
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
                    actionResult = await sut.UpdateTeam(pristine);

                    scope.Dispose();
                }

                // Assert
                okObjectResult = actionResult as OkObjectResult;
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
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // Act
            IActionResult actionResult;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                SimpleTeamCriteria criteria = new SimpleTeamCriteria { TeamKey = 44 };
                actionResult = await sut.DeleteTeam(criteria);

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
