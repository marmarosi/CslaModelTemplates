using CslaModelTemplates.Contracts.Junction;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace CslaModelTemplates.WebApiTests.Junction
{
    public class Group_Tests
    {
        #region New

        [Fact]
        public async Task CreateGroup_ReturnsNewModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<JunctionController>();
            var sut = new JunctionController(logger);

            // Act
            IActionResult actionResult = await sut.GetNewGroup();

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            GroupDto group = okObjectResult.Value as GroupDto;
            Assert.NotNull(group);

            // The code and name must miss.
            Assert.Empty(group.GroupCode);
            Assert.Empty(group.GroupName);
            Assert.Null(group.Timestamp);
            Assert.Empty(group.Persons);
        }

        #endregion

        #region Create

        [Fact]
        public async Task CreateGroup_ReturnsCreatedModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<JunctionController>();
            var sut = new JunctionController(logger);

            // Act
            GroupDto pristineGroup = null;
            GroupPersonDto pristineMember1 = null;
            GroupPersonDto pristineMember2 = null;
            IActionResult actionResult = await setup.RetryOnDeadlock(async () =>
            {
                pristineGroup = new GroupDto
                {
                    GroupKey = null,
                    GroupCode = "T-9201",
                    GroupName = "Test group number 9201",
                    Timestamp = null
                };
                pristineMember1 = new GroupPersonDto
                {
                    PersonKey = 11,
                    PersonName = "Person #11"
                };
                pristineGroup.Persons.Add(pristineMember1);
                pristineMember2 = new GroupPersonDto
                {
                    PersonKey = 17,
                    PersonName = "Person #17"
                };
                pristineGroup.Persons.Add(pristineMember2);

                return await sut.CreateGroup(pristineGroup);
            });

            // Assert
            CreatedResult createdResult = actionResult as CreatedResult;
            Assert.NotNull(createdResult);

            GroupDto createdGroup = createdResult.Value as GroupDto;
            Assert.NotNull(createdGroup);

            // The group must have new values.
            Assert.NotNull(createdGroup.GroupKey);
            Assert.Equal(pristineGroup.GroupCode, createdGroup.GroupCode);
            Assert.Equal(pristineGroup.GroupName, createdGroup.GroupName);
            Assert.NotNull(createdGroup.Timestamp);

            // The persons must have new values.
            Assert.Equal(2, createdGroup.Persons.Count);

            GroupPersonDto createdMember1 = createdGroup.Persons[0];
            Assert.Equal(pristineMember1.PersonKey, createdMember1.PersonKey);
            Assert.Equal(pristineMember1.PersonName, createdMember1.PersonName);

            GroupPersonDto createdMember2 = createdGroup.Persons[1];
            Assert.Equal(pristineMember2.PersonKey, createdMember2.PersonKey);
            Assert.Equal(pristineMember2.PersonName, createdMember2.PersonName);
        }

        #endregion

        #region Read

        [Fact]
        public async Task ReadGroup_ReturnsCurrentModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<JunctionController>();
            var sut = new JunctionController(logger);

            // Act
            GroupCriteria criteria = new GroupCriteria { GroupKey = 12 };
            IActionResult actionResult = await sut.GetGroup(criteria);

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            GroupDto pristineGroup = okObjectResult.Value as GroupDto;
            Assert.NotNull(pristineGroup);

            // The group code and name must end with 12.
            Assert.Equal(12, pristineGroup.GroupKey);
            Assert.Equal("G-12", pristineGroup.GroupCode);
            Assert.EndsWith("12", pristineGroup.GroupName);
            Assert.NotNull(pristineGroup.Timestamp);

            // The person name must start with Person.
            Assert.True(pristineGroup.Persons.Count > 0);
            GroupPersonDto pristineMember1 = pristineGroup.Persons[0];
            foreach (GroupPersonDto groupPerson in pristineGroup.Persons)
            {
                Assert.StartsWith("Person", groupPerson.PersonName);
            }
        }

        #endregion

        #region Update

        [Fact]
        public async Task UpdateGroup_ReturnsUpdatedModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<JunctionController>();
            var sutR = new JunctionController(logger);
            var sutU = new JunctionController(logger);

            // Act
            GroupDto pristineGroup = null;
            GroupPersonDto pristineMember1 = null;
            GroupPersonDto pristineMemberNew = null;
            IActionResult actionResult = await setup.RetryOnDeadlock(async () =>
            {
                GroupCriteria criteria = new GroupCriteria { GroupKey = 12 };
                IActionResult actionResult = await sutR.GetGroup(criteria);
                OkObjectResult okObjectResult = actionResult as OkObjectResult;
                pristineGroup = okObjectResult.Value as GroupDto;
                pristineMember1 = pristineGroup.Persons[0];

                pristineGroup.GroupCode = "G-1212";
                pristineGroup.GroupName = "Group No. 1212";

                pristineMemberNew = new GroupPersonDto
                {
                    PersonKey = 1,
                    PersonName = "New member",
                };
                pristineGroup.Persons.Add(pristineMemberNew);
                return await sutU.UpdateGroup(pristineGroup);
            });

            // Assert
            OkObjectResult okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            GroupDto updatedGroup = okObjectResult.Value as GroupDto;
            Assert.NotNull(updatedGroup);

            // The group must have new values.
            Assert.Equal(pristineGroup.GroupKey, updatedGroup.GroupKey);
            Assert.Equal(pristineGroup.GroupCode, updatedGroup.GroupCode);
            Assert.Equal(pristineGroup.GroupName, updatedGroup.GroupName);
            Assert.NotEqual(pristineGroup.Timestamp, updatedGroup.Timestamp);

            Assert.Equal(pristineGroup.Persons.Count, updatedGroup.Persons.Count);

            // Persons must reflect the changes.
            GroupPersonDto updatedMember1 = updatedGroup.Persons[0];
            Assert.Equal(pristineMember1.PersonKey, updatedMember1.PersonKey);
            Assert.Equal(pristineMember1.PersonName, updatedMember1.PersonName);

            GroupPersonDto createdMemberNew = updatedGroup.Persons[pristineGroup.Persons.Count - 1];
            Assert.Equal(pristineMemberNew.PersonKey, createdMemberNew.PersonKey);
            Assert.StartsWith("Person", createdMemberNew.PersonName);
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteGroup_ReturnsNothing()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<JunctionController>();
            var sut = new JunctionController(logger);

            // Act
            IActionResult actionResult = await setup.RetryOnDeadlock(async () =>
            {
                GroupCriteria criteria = new GroupCriteria { GroupKey = 4 };
                return await sut.DeleteGroup(criteria);
            });

            // Assert
            NoContentResult noContentResult = actionResult as NoContentResult;
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        #endregion
    }
}
