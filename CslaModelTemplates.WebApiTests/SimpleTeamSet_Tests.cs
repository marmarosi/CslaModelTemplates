using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace CslaModelTemplates.WebApiTests
{
    public class SimpleTeamSet_Tests
    {
        [Fact]
        public async Task UpdateTeamSet_ReturnsUpdatedModels()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // --- READ
            IActionResult actionResult;
            OkObjectResult okObjectResult;

            // Act
            SimpleTeamSetCriteria criteria = new SimpleTeamSetCriteria { TeamName = "8" };
            actionResult = await sut.GetTeamSet(criteria);

            // Assert
            okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<SimpleTeamSetItemDto> pristineList = okObjectResult.Value as List<SimpleTeamSetItemDto>;
            Assert.NotNull(pristineList);

            // List must contain 5 items.
            Assert.Equal(5, pristineList.Count);

            // --- UPDATE
            SimpleTeamSetItemDto pristine;
            SimpleTeamSetItemDto pristineNew;
            long? deletedKey;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
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
                actionResult = await sut.UpdateTeamSet(criteria, pristineList);

                scope.Dispose();
            }

            // Assert
            okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<SimpleTeamSetItemDto> updatedList = okObjectResult.Value as List<SimpleTeamSetItemDto>;
            Assert.NotNull(updatedList);

            // The updated model must have new values.
            SimpleTeamSetItemDto updated = updatedList[0];

            Assert.Equal(pristine.TeamKey, updated.TeamKey);
            Assert.Equal("T-9101", updated.TeamCode);
            Assert.Equal("Test team number 9101", updated.TeamName);
            Assert.NotEqual(pristine.Timestamp, updated.Timestamp);

            // The created model must have new values.
            SimpleTeamSetItemDto created = updatedList.FirstOrDefault(o => o.TeamCode == "T-9102");

            Assert.NotNull(created);
            Assert.NotNull(created.TeamKey);
            Assert.Equal("Test team number 9102", created.TeamName);
            Assert.NotNull(created.Timestamp);

            // The deleted model must have gone.
            SimpleTeamSetItemDto deleted = updatedList.FirstOrDefault(o => o.TeamKey == deletedKey);
            Assert.Null(deleted);
        }
    }
}
