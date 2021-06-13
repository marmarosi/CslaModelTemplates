using CslaModelTemplates.Contracts.JunctionView;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests.Junction
{
    public class GroupView_Tests
    {
        [Fact]
        public async Task GetGroupView_ReturnsAView()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<JunctionController>();
            var sut = new JunctionController(logger);

            // Act
            GroupViewCriteria criteria = new GroupViewCriteria { GroupKey = 8 };
            IActionResult actionResult = await sut.GetGroupView(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
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
