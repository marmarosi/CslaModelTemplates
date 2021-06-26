using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests.Simple
{
    public class SimpleTeamSet_Tests
    {
        #region Read

        [Fact]
        public async Task ReadTeamSet_ReturnsCurrentModels()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sut = new SimpleController(logger);

            // Act
            SimpleTeamSetCriteria criteria = new SimpleTeamSetCriteria { TeamName = "8" };
            IActionResult actionResult = await sut.GetTeamSet(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            List<SimpleTeamSetItemDto> pristineList = okObjectResult.Value as List<SimpleTeamSetItemDto>;
            Assert.NotNull(pristineList);

            // List must contain 5 items.
            Assert.True(pristineList.Count > 3);
        }

        #endregion

        #region Update

        [Fact]
        public async Task UpdateTeamSet_ReturnsUpdatedModels()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<SimpleController>();
            var sutR = new SimpleController(logger);
            var sutU = new SimpleController(logger);

            // Act
            SimpleTeamSetItemDto pristine = null;
            SimpleTeamSetItemDto pristineNew = null;
            long? deletedKey = null;
            IActionResult actionResult = await setup.RetryOnDeadlock(async () =>
            {
                SimpleTeamSetCriteria criteria = new SimpleTeamSetCriteria { TeamName = "8" };
                IActionResult actionResult = await sutR.GetTeamSet(criteria);
                OkObjectResult okObjectResult = actionResult as OkObjectResult;
                List<SimpleTeamSetItemDto> pristineList = okObjectResult.Value as List<SimpleTeamSetItemDto>;

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

                return actionResult = await sutU.UpdateTeamSet(criteria, pristineList);
            });

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
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

        #endregion
    }
}
