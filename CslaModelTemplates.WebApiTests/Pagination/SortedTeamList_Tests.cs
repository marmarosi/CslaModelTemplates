using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.SortedList;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests.Pagination
{
    public class SortedTeamList_Tests
    {
        [Fact]
        public async Task GetSortedTeamList_ReturnsAList()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<PaginationController>();
            var sut = new PaginationController(logger);

            // Act
            SortedTeamListCriteria criteria = new SortedTeamListCriteria
            {
                TeamName = "5",
                SortBy = SortedTeamListSortBy.TeamCode,
                SortDirection = SortDirection.Descending
            };
            ActionResult<List<SortedTeamListItemDto>> actionResult = await sut.GetSortedTeamList(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            IList<SortedTeamListItemDto> list = okObjectResult.Value as IList<SortedTeamListItemDto>;
            Assert.NotNull(list);

            // The list must have 6 items.
            Assert.Equal(6, list.Count);

            // The code and names must end with 5 or 50.
            foreach (var item in list)
            {
                Assert.True(item.TeamCode.EndsWith("5") || item.TeamCode.EndsWith("50"));
                Assert.True(item.TeamName.EndsWith("5") || item.TeamName.EndsWith("50"));
            }
        }
    }
}
