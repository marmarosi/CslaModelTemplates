using CslaModelTemplates.Contracts.Junction;
using CslaModelTemplates.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace CslaModelTemplates.WebApiTests
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
            Assert.Empty(group.Members);
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
            IActionResult actionResult;
            GroupDto pristineGroup;
            MemberDto pristineMember1;
            MemberDto pristineMember2;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                pristineGroup = new GroupDto
                {
                    GroupKey = null,
                    GroupCode = "T-9201",
                    GroupName = "Test group number 9201",
                    Timestamp = null
                };
                pristineMember1 = new MemberDto
                {
                    PersonKey = 11,
                    PersonName = "Person #11"
                };
                pristineGroup.Members.Add(pristineMember1);
                pristineMember2 = new MemberDto
                {
                    PersonKey = 17,
                    PersonName = "Person #17"
                };
                pristineGroup.Members.Add(pristineMember2);
                actionResult = await sut.CreateGroup(pristineGroup);

                scope.Dispose();
            }

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
            Assert.Equal(2, createdGroup.Members.Count);

            MemberDto createdMember1 = createdGroup.Members[0];
            Assert.Equal(pristineMember1.PersonKey, createdMember1.PersonKey);
            Assert.Equal(pristineMember1.PersonName, createdMember1.PersonName);

            MemberDto createdMember2 = createdGroup.Members[1];
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
            var logger = setup.GetLogger<JunctionController>();
            var sut = new JunctionController(logger);

            using (var uow = setup.UnitOfWork())
            {
                // --- READ
                IActionResult actionResult;
                OkObjectResult okObjectResult;

                // Act
                GroupCriteria criteria = new GroupCriteria { GroupKey = 12 };
                actionResult = await sut.GetGroup(criteria);

                // Assert
                okObjectResult = actionResult as OkObjectResult;
                Assert.NotNull(okObjectResult);

                GroupDto pristineGroup = okObjectResult.Value as GroupDto;
                Assert.NotNull(pristineGroup);

                // The group code and name must end with 12.
                Assert.Equal(12, pristineGroup.GroupKey);
                Assert.Equal("G-12", pristineGroup.GroupCode);
                Assert.EndsWith("12", pristineGroup.GroupName);
                Assert.NotNull(pristineGroup.Timestamp);

                // The person name must start with Person.
                Assert.True(pristineGroup.Members.Count > 0);
                MemberDto pristineMember1 = pristineGroup.Members[0];
                foreach (MemberDto member in pristineGroup.Members)
                {
                    Assert.StartsWith("Person", member.PersonName);
                }

                // --- UPDATE
                MemberDto pristineMemberNew;

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    pristineGroup.GroupCode = "G-1212";
                    pristineGroup.GroupName = "Group No. 1212";

                    pristineMemberNew = new MemberDto
                    {
                        PersonKey = 1,
                        PersonName = "New member",
                    };
                    pristineGroup.Members.Add(pristineMemberNew);
                    actionResult = await sut.UpdateGroup(pristineGroup);

                    scope.Dispose();
                }

                // Assert
                okObjectResult = actionResult as OkObjectResult;
                Assert.NotNull(okObjectResult);

                GroupDto updatedGroup = okObjectResult.Value as GroupDto;
                Assert.NotNull(updatedGroup);

                // The group must have new values.
                Assert.Equal(pristineGroup.GroupKey, updatedGroup.GroupKey);
                Assert.Equal(pristineGroup.GroupCode, updatedGroup.GroupCode);
                Assert.Equal(pristineGroup.GroupName, updatedGroup.GroupName);
                Assert.NotEqual(pristineGroup.Timestamp, updatedGroup.Timestamp);

                Assert.Equal(pristineGroup.Members.Count, updatedGroup.Members.Count);

                // Persons must reflect the changes.
                MemberDto updatedMember1 = updatedGroup.Members[0];
                Assert.Equal(pristineMember1.PersonKey, updatedMember1.PersonKey);
                Assert.Equal(pristineMember1.PersonName, updatedMember1.PersonName);

                MemberDto createdMemberNew = updatedGroup.Members[pristineGroup.Members.Count - 1];
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
            var logger = setup.GetLogger<JunctionController>();
            var sut = new JunctionController(logger);

            // Act
            IActionResult actionResult;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                GroupCriteria criteria = new GroupCriteria { GroupKey = 4 };
                actionResult = await sut.DeleteGroup(criteria);

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
