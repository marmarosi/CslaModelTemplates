using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Endpoints.SimpleEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Simple
{
    public class SimpleTeamSet_Tests
    {
        #region Read

        [Fact]
        public async Task ReadTeamSet_ReturnsCurrentModels()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<ReadSet>();
            var sut = new ReadSet(logger);

            // Act
            SimpleTeamSetCriteria criteria = new SimpleTeamSetCriteria { TeamName = "8" };
            ActionResult<IList<SimpleTeamSetItemDto>> actionResult =
                await sut.HandleAsync(criteria, new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<SimpleTeamSetItemDto> pristineList = okObjectResult.Value as List<SimpleTeamSetItemDto>;
            Assert.NotNull(pristineList);

            // List must contain 5 items.
            Assert.InRange(pristineList.Count, 1, 10);
        }

        #endregion

        #region Update

        [Fact]
        public async Task UpdateTeamSet_ReturnsUpdatedModels()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var loggerR = setup.GetLogger<ReadSet>();
            var loggerU = setup.GetLogger<UpdateSet>();
            var sutR = new ReadSet(loggerR);
            var sutU = new UpdateSet(loggerU);

            // Act
            SimpleTeamSetItemDto pristine = null;
            SimpleTeamSetItemDto pristineNew = null;
            string deletedId = null;
            var actionResult = await Call<IList<SimpleTeamSetItemDto>>.RetryOnDeadlock(async () =>
            {
                SimpleTeamSetCriteria criteria = new SimpleTeamSetCriteria { TeamName = "8" };
                ActionResult<IList<SimpleTeamSetItemDto>> actionResult =
                    await sutR.HandleAsync(criteria, new CancellationToken());
                OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
                List<SimpleTeamSetItemDto> pristineList = okObjectResult.Value as List<SimpleTeamSetItemDto>;

                // Modify an item.
                pristine = pristineList[0];
                pristine.TeamCode = "T-9101";
                pristine.TeamName = "Test team number 9101";

                // Create new item.
                pristineNew = new SimpleTeamSetItemDto
                {
                    TeamId = null,
                    TeamCode = "T-9102",
                    TeamName = "Test team number 9102",
                    Timestamp = null
                };
                pristineList.Add(pristineNew);

                // Delete an item.
                SimpleTeamSetItemDto pristine3 = pristineList[3];
                deletedId = pristine3.TeamId;
                pristineList.Remove(pristine3);

                // Act
                SimpleTeamSetRequest request = new SimpleTeamSetRequest
                {
                    Criteria = criteria,
                    Dto = pristineList
                };
                return await sutU.HandleAsync(request, new CancellationToken());
            });

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<SimpleTeamSetItemDto> updatedList = okObjectResult.Value as List<SimpleTeamSetItemDto>;
            Assert.NotNull(updatedList);

            // The updated team must have new values.
            SimpleTeamSetItemDto updated = updatedList[0];

            Assert.Equal(pristine.TeamId, updated.TeamId);
            Assert.Equal(pristine.TeamCode, updated.TeamCode);
            Assert.Equal(pristine.TeamName, updated.TeamName);
            Assert.NotEqual(pristine.Timestamp, updated.Timestamp);

            // The created team must have new values.
            SimpleTeamSetItemDto created = updatedList
                .FirstOrDefault(o => o.TeamCode == pristineNew.TeamCode);
            Assert.NotNull(created);

            Assert.NotNull(created.TeamId);
            Assert.Equal(pristineNew.TeamCode, created.TeamCode);
            Assert.Equal(pristineNew.TeamName, created.TeamName);
            Assert.NotNull(created.Timestamp);

            // The deleted team must have gone.
            SimpleTeamSetItemDto deleted = updatedList
                .FirstOrDefault(o => o.TeamId == deletedId);
            Assert.Null(deleted);
        }

        #endregion
    }
}
