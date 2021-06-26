using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Endpoints.SimpleEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Simple
{
    public class SimpleTeamSet_Tests
    {
        [Fact]
        public async Task UpdateTeamSet_ReturnsUpdatedModels()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var loggerR = setup.GetLogger<ReadSet>();
            var loggerU = setup.GetLogger<UpdateSet>();
            var sutRead = new ReadSet(loggerR);
            var sutUpdate = new UpdateSet(loggerU);

            using (var uow = setup.UnitOfWork())
            {
                // --- READ
                ActionResult<IList<SimpleTeamSetItemDto>> actionResult;
                OkObjectResult okObjectResult;

                // Act
                SimpleTeamSetCriteria criteria = new SimpleTeamSetCriteria { TeamName = "8" };
                actionResult = await sutRead.HandleAsync(criteria, new CancellationToken());

                // Assert
                okObjectResult = actionResult.Result as OkObjectResult;
                Assert.NotNull(okObjectResult);

                List<SimpleTeamSetItemDto> pristineList = okObjectResult.Value as List<SimpleTeamSetItemDto>;
                Assert.NotNull(pristineList);

                // List must contain 5 items.
                Assert.InRange(pristineList.Count, 4, 5);

                // --- UPDATE
                SimpleTeamSetItemDto pristine;
                SimpleTeamSetItemDto pristineNew;
                long? deletedKey;

                using (var trx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Modify an item.
                    pristine = pristineList[0];
                    pristine.TeamCode = "T-9101";
                    pristine.TeamName = "Test team number 9101";

                    // Create new item.
                    pristineNew = new SimpleTeamSetItemDto
                    {
                        TeamKey = null,
                        TeamCode = "T-9102",
                        TeamName = "Test team number 9102",
                        Timestamp = null
                    };
                    pristineList.Add(pristineNew);

                    // Delete an item.
                    SimpleTeamSetItemDto pristine3 = pristineList[3];
                    deletedKey = pristine3.TeamKey;
                    pristineList.Remove(pristine3);

                    // Act
                    SimpleTeamSetRequest request = new SimpleTeamSetRequest
                    {
                        Criteria = criteria,
                        Dto = pristineList
                    };
                    actionResult = await sutUpdate.HandleAsync(request, new CancellationToken());

                    trx.Dispose();
                }

                // Assert
                okObjectResult = actionResult.Result as OkObjectResult;
                Assert.NotNull(okObjectResult);

                List<SimpleTeamSetItemDto> updatedList = okObjectResult.Value as List<SimpleTeamSetItemDto>;
                Assert.NotNull(updatedList);

                // The updated team must have new values.
                SimpleTeamSetItemDto updated = updatedList[0];

                Assert.Equal(pristine.TeamKey, updated.TeamKey);
                Assert.Equal(pristine.TeamCode, updated.TeamCode);
                Assert.Equal(pristine.TeamName, updated.TeamName);
                Assert.NotEqual(pristine.Timestamp, updated.Timestamp);

                // The created team must have new values.
                SimpleTeamSetItemDto created = updatedList
                    .FirstOrDefault(o => o.TeamCode == pristineNew.TeamCode);
                Assert.NotNull(created);

                Assert.NotNull(created.TeamKey);
                Assert.Equal(pristineNew.TeamCode, created.TeamCode);
                Assert.Equal(pristineNew.TeamName, created.TeamName);
                Assert.NotNull(created.Timestamp);

                // The deleted team must have gone.
                SimpleTeamSetItemDto deleted = updatedList
                    .FirstOrDefault(o => o.TeamKey == deletedKey);
                Assert.Null(deleted);
            }
        }
    }
}
