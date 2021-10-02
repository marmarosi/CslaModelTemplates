using CslaModelTemplates.Contracts.JunctionView;
using CslaModelTemplates.Endpoints.JunctionEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Junction
{
    public class GroupView_Tests
    {
        [Fact]
        public async Task GetGroupView_ReturnsAView()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<View>();
            var sut = new View(logger);

            // Act
            GroupViewParams criteria = new GroupViewParams { GroupId = "oQLOyK85x6g" };
            ActionResult<GroupViewDto> actionResult = await sut.HandleAsync(criteria, new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            GroupViewDto group = okObjectResult.Value as GroupViewDto;
            Assert.NotNull(group);

            // The code and name must end with 17.
            Assert.Equal("G-08", group.GroupCode);
            Assert.EndsWith("8", group.GroupName);
            Assert.True(group.Persons.Count > 0);

            // The code and name must end with 17.
            GroupPersonViewDto groupPerson = group.Persons[0];
            Assert.StartsWith("Person #", groupPerson.PersonName);
        }
    }
}
