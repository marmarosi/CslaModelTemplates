using CslaModelTemplates.Contracts.ComplexCommand;
using CslaModelTemplates.Models.Command;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace CslaModelTemplates.WebApiTests
{
    public class CountTeams_Tests
    {
        [Fact]
        public async Task CountTeams_ReturnsList()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<ComplexController>();
            var sut = new ComplexController(logger);

            // Act
            IActionResult actionResult;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                CountTeamsCriteria criteria = new CountTeamsCriteria();
                actionResult = await sut.CountTeamsCommand(criteria);

                scope.Dispose();
            }

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            CountTeamsList list = okObjectResult.Value as CountTeamsList;
            Assert.NotNull(list);

            // Count list must contain 4 items.
            Assert.Equal(4, list.Count);

            CountTeamsListItem item1 = list[0];
            Assert.Equal(4, item1.ItemCount);
            Assert.True(item1.CountOfTeams > 0);

            CountTeamsListItem item2 = list[1];
            Assert.Equal(3, item2.ItemCount);
            Assert.True(item2.CountOfTeams > 0);

            CountTeamsListItem item3 = list[2];
            Assert.Equal(2, item3.ItemCount);
            Assert.True(item3.CountOfTeams > 0);

            CountTeamsListItem item4 = list[3];
            Assert.Equal(1, item4.ItemCount);
            Assert.True(item4.CountOfTeams > 0);
        }
    }
}
