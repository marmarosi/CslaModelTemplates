using CslaModelTemplates.Contracts.SelectionWithId;
using CslaModelTemplates.Dal.Contracts;
using CslaModelTemplates.Endpoints.SelectionEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Selection
{
    public class TeamIdChoice_Tests
    {
        [Fact]
        public async Task GetTeamChoiceWithId_ReturnsAChoice()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<ChoiceWithId>();
            var sut = new ChoiceWithId(logger);

            // Act
            TeamIdChoiceCriteria criteria = new TeamIdChoiceCriteria { TeamName = "0" };
            ActionResult<IList<IdNameOptionDto>> actionResult = await sut.HandleAsync(criteria, new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<IdNameOptionDto> choice = okObjectResult.Value as List<IdNameOptionDto>;
            Assert.NotNull(choice);

            // The choice must have 5 items.
            Assert.Equal(5, choice.Count);

            // The names must end with 7.
            foreach (var item in choice)
            {
                Assert.EndsWith("0", item.Name);
            }
        }
    }
}
