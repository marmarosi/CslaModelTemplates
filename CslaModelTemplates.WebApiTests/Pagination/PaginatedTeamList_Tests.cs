using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.PaginatedList;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests.Pagination
{
    public class PaginatedTeamList_Tests
    {
        [Fact]
        public async Task GetPaginatedTeamList_ReturnsAList()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<PaginationController>();
            var sut = new PaginationController(logger);

            // Act
            PaginatedTeamListCriteria criteria = new PaginatedTeamListCriteria
            {
                TeamName = "1",
                PageIndex = 1,
                PageSize = 10
            };
            IActionResult actionResult = await sut.GetPaginatedTeamList(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            PaginatedList<PaginatedTeamListItemDto> list = okObjectResult.Value as PaginatedList<PaginatedTeamListItemDto>;
            Assert.NotNull(list);

            // The list must have 4 items and 14 total items.
            Assert.Equal(4, list.Data.Count);
            Assert.Equal(14, list.TotalCount);

            // The code and names must contain 1.
            foreach (var item in list.Data)
            {
                Assert.Contains("1", item.TeamCode);
                Assert.Contains("1", item.TeamName);
            }
        }
    }
}
