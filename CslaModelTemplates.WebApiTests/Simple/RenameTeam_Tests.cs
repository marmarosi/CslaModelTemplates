using CslaModelTemplates.Contracts.SimpleCommand;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace CslaModelTemplates.WebApiTests.Simple
{
    public class RenameTeam_Tests
    {
        [Fact]
        public async Task RenameTeam_ReturnsTrue()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // Act
            IActionResult actionResult;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                RenameTeamDto dto = new RenameTeamDto { TeamKey = 37, TeamName = "Team Thirty Seven" };
                actionResult = await sut.RenameTeamCommand(dto);

                scope.Dispose();
            }

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            bool success = (bool)okObjectResult.Value;
            Assert.True(success);
        }
    }
}
