using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.PaginatedSortedList;
using CslaModelTemplates.Endpoints.PaginationEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Pagination
{
    public class PaginatedSortedTeamList_Tests
    {
        [Fact]
        public async Task GetPaginatedSortedTeamList_ReturnsAList()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<PaginatedSortedList>();
            var sut = new PaginatedSortedList(logger);

            // Act
            PaginatedSortedTeamListCriteria criteria = new PaginatedSortedTeamListCriteria
            {
                TeamName = "1",
                PageIndex = 1,
                PageSize = 10,
                SortBy = PaginatedSortedTeamListSortBy.TeamCode,
                SortDirection = SortDirection.Descending
            };
            ActionResult<IPaginatedList<PaginatedSortedTeamListItemDto>> actionResult = await sut.HandleAsync(criteria, new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
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
