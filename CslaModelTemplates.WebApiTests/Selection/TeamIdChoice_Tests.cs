using CslaModelTemplates.Contracts.SelectionWithId;
using CslaModelTemplates.Dal.Contracts;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests.Selection
{
    public class TeamIdChoice_Tests
    {
        [Fact]
        public async Task GetTeamChoiceWithId_ReturnsAChoice()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SelectionController>();
            var sut = new SelectionController(logger);

            // Act
            TeamIdChoiceCriteria criteria = new TeamIdChoiceCriteria { TeamName = "0" };
            ActionResult<List<IdNameOptionDto>> actionResult = await sut.GetTeamChoiceWithId(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            IList<IdNameOptionDto> choice = okObjectResult.Value as IList<IdNameOptionDto>;
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
