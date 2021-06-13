using CslaModelTemplates.Contracts.Junction;
using CslaModelTemplates.Endpoints.JunctionEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace CslaModelTemplates.EndpointTests.Junction
{
    public class Group_Tests
    {
        #region New

        [Fact]
        public async Task CreateGroup_ReturnsNewModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<New>();
            var sut = new New(logger);

            // Act
            ActionResult<GroupDto> actionResult = await sut.HandleAsync(new CancellationToken());

            // Assert
            OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
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
            var logger = setup.GetLogger<Create>();
            var sut = new Create(logger);

            // Act
            ActionResult<GroupDto> actionResult;
            GroupDto pristineGroup;
            GroupPersonDto pristineMember1;
            GroupPersonDto pristineMember2;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
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
                actionResult = await sut.HandleAsync(pristineGroup, new CancellationToken());

                scope.Dispose();
            }

            // Assert
            CreatedResult createdResult = actionResult.Result as CreatedResult;
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

        #region Read & Update

        [Fact]
        public async Task UpdateGroup_ReturnsUpdatedModel()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var loggerRead = setup.GetLogger<Read>();
            var sutRead = new Read(loggerRead);
            var loggerUpdate = setup.GetLogger<Update>();
            var sutUpdate = new Update(loggerUpdate);

            using (var uow = setup.UnitOfWork())
            {
                // --- READ
                ActionResult<GroupDto> actionResult;
                OkObjectResult okObjectResult;

                // Act
                GroupCriteria criteria = new GroupCriteria { GroupKey = 12 };
                actionResult = await sutRead.HandleAsync(criteria, new CancellationToken());

                // Assert
                okObjectResult = actionResult.Result as OkObjectResult;
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

                // --- UPDATE
                GroupPersonDto pristineMemberNew;

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    pristineGroup.GroupCode = "G-1212";
                    pristineGroup.GroupName = "Group No. 1212";

                    pristineMemberNew = new GroupPersonDto
                    {
                        PersonKey = 1,
                        PersonName = "New member",
                    };
                    pristineGroup.Persons.Add(pristineMemberNew);
                    actionResult = await sutUpdate.HandleAsync(pristineGroup, new CancellationToken());

                    scope.Dispose();
                }

                // Assert
                okObjectResult = actionResult.Result as OkObjectResult;
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
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteGroup_ReturnsNothing()
        {
            // Arrange
            SetupService setup = SetupService.GetInstance();
            var logger = setup.GetLogger<Delete>();
            var sut = new Delete(logger);

            // Act
            IActionResult actionResult;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                GroupCriteria criteria = new GroupCriteria { GroupKey = 4 };
                actionResult = await sut.HandleAsync(criteria, new CancellationToken());

                scope.Dispose();
            }

            // Assert
            NoContentResult noContentResult = actionResult as NoContentResult;
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        #endregion
    }
}
