using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionWithKey;
using CslaModelTemplates.Endpoints.SelectionEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Selection
{
    public class TeamKeyChoice_Tests
    {
        [Fact]
        public async Task GetTeamChoiceWithKey_ReturnsAChoice()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<ChoiceWithKey>();
            var sut = new ChoiceWithKey(logger);

            // Act
            TeamKeyChoiceCriteria criteria = new TeamKeyChoiceCriteria { TeamName = "7" };
            ActionResult<IList<KeyNameOptionDto>> actionResult = await sut.HandleAsync(criteria, new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<KeyNameOptionDto> choice = okObjectResult.Value as List<KeyNameOptionDto>;
            Assert.NotNull(choice);

            // The choice must have 5 items.
            Assert.Equal(5, choice.Count);

            // The names must end with 7.
            foreach (var item in choice)
            {
                Assert.EndsWith("7", item.Name);
            }
        }
    }
}
