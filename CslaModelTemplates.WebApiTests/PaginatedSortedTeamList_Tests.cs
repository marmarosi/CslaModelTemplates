using CslaModelTemplates.Common.DataTransfer;
using CslaModelTemplates.Contracts.PaginatedSortedList;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests
{
    public class PaginatedSortedTeamList_Tests
    {
        [Fact]
        public async Task GetPaginatedSortedTeamList_ReturnsAList()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<PaginationController>();
            var sut = new PaginationController(logger);

            // Act
            PaginatedSortedTeamListCriteria criteria = new PaginatedSortedTeamListCriteria
            {
                TeamName = "1",
                PageIndex = 1,
                PageSize = 10,
                SortBy = PaginatedSortedTeamListSortBy.TeamCode,
                SortDirection = SortDirection.Descending
            };
            IActionResult actionResult = await sut.GetPaginatedSortedTeamList(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            PaginatedList<PaginatedSortedTeamListItemDto> list = okObjectResult.Value as PaginatedList<PaginatedSortedTeamListItemDto>;
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
