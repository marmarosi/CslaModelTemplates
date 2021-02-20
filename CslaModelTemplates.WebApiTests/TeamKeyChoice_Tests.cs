using CslaModelTemplates.Contracts.SelectionWithKey;
using CslaModelTemplates.Models.SelectionWithKey;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests
{
    public class TeamKeyChoice_Tests
    {
        [Fact]
        public async Task GetTeamChoiceWithKey_ReturnsAChoice()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SelectionController>();
            var sut = new SelectionController(logger);

            // Act
            TeamKeyChoiceCriteria criteria = new TeamKeyChoiceCriteria { TeamName = "7" };
            IActionResult actionResult = await sut.GetTeamChoiceWithKey(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            TeamKeyChoice choice = okObjectResult.Value as TeamKeyChoice;
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
