using CslaModelTemplates.Contracts.SimpleCommand;
using CslaModelTemplates.Endpoints.SimpleEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Simple
{
    public class RenameTeam_Tests
    {
        [Fact]
        public async Task RenameTeam_ReturnsTrue()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<Command>();
            var sut = new Command(logger);

            // Act
            ActionResult<bool> actionResult;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                RenameTeamDto dto = new RenameTeamDto { TeamKey = 37, TeamName = "Team Thirty Seven" };
                actionResult = await sut.HandleAsync(dto, new CancellationToken());

                scope.Dispose();
            }

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            bool success = (bool)okObjectResult.Value;
            Assert.True(success);
        }
    }
}
