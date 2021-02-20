using CslaModelTemplates.Contracts.SelectionWithCode;
using CslaModelTemplates.Models.SelectionWithCode;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests
{
    public class TeamCodeChoice_Tests
    {
        [Fact]
        public async Task GetTeamChoiceWithCode_ReturnsAChoice()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SelectionController>();
            var sut = new SelectionController(logger);

            // Act
            TeamCodeChoiceCriteria criteria = new TeamCodeChoiceCriteria { TeamName = "9" };
            IActionResult actionResult = await sut.GetTeamChoiceWithCode(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            TeamCodeChoice choice = okObjectResult.Value as TeamCodeChoice;
            Assert.NotNull(choice);

            // The choice must have 5 items.
            Assert.Equal(5, choice.Count);

            // The codes and names must end with 9.
            foreach (var item in choice)
            {
                Assert.EndsWith("9", item.Code);
                Assert.EndsWith("9", item.Name);
            }
        }
    }
}
