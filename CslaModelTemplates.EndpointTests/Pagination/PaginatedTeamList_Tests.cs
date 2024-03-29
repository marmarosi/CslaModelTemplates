using CslaModelTemplates.Contracts.PaginatedList;
using CslaModelTemplates.Dal.Contracts;
using CslaModelTemplates.Endpoints.PaginationEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Pagination
{
    public class PaginatedTeamList_Tests
    {
        [Fact]
        public async Task GetPaginatedTeamList_ReturnsAList()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<PaginatedList>();
            var sut = new PaginatedList(logger);

            // Act
            PaginatedTeamListCriteria criteria = new PaginatedTeamListCriteria
            {
                TeamName = "1",
                PageIndex = 1,
                PageSize = 10
            };
            ActionResult<IPaginatedList<PaginatedTeamListItemDto>> actionResult = await sut.HandleAsync(criteria, new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
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
