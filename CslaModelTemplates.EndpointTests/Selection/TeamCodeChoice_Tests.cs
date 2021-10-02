using CslaModelTemplates.Contracts.SelectionWithCode;
using CslaModelTemplates.Dal.Contracts;
using CslaModelTemplates.Endpoints.SelectionEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Selection
{
    public class TeamCodeChoice_Tests
    {
        [Fact]
        public async Task GetTeamChoiceWithCode_ReturnsAChoice()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<ChoiceWithCode>();
            var sut = new ChoiceWithCode(logger);

            // Act
            TeamCodeChoiceCriteria criteria = new TeamCodeChoiceCriteria { TeamName = "9" };
            ActionResult<IList<CodeNameOptionDto>> actionResult = await sut.HandleAsync(criteria, new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<CodeNameOptionDto> choice = okObjectResult.Value as List<CodeNameOptionDto>;
            Assert.NotNull(choice);

            // The choice must have 5 items.
            Assert.Equal(5, choice.Count);

            // The codes and names must end with 9.
            foreach (var option in choice)
            {
                Assert.EndsWith("9", option.Code);
                Assert.EndsWith("9", option.Name);
            }
        }
    }
}
