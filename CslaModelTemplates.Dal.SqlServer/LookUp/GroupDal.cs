using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.LookUp;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.SqlServer.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.LookUp
{
    /// <summary>
    /// Implements the data access functions of the editable group object.
    /// </summary>
    public class GroupDal : SqlServerDal, IGroupDal
    {
        #region Fetch

                /// <summary>
                /// Gets the specified group.
                /// </summary>
                /// <param name="criteria">The criteria of the group.</param>
                /// <returns>The requested group.</returns>
                public GroupDao Fetch(
            GroupCriteria criteria
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Get the specified group.
                GroupDao group = ctx.DbContext.Groups
                    .Where(e =>
                        e.GroupKey == criteria.GroupKey
                     )
                    .Select(e => new GroupDao
                    {
                        GroupKey = e.GroupKey,
                        GroupCode = e.GroupCode,
                        GroupName = e.GroupName,
                        Members = e.Members.Select(m => new MemberDao
                            {
                                GroupKey = m.GroupKey,
                                PersonKey = m.PersonKey,
                                PersonName = m.Person.PersonName
                            }).ToList(),
                        Timestamp = e.Timestamp
                    })
                    .AsNoTracking()
                    .FirstOrDefault();

                if (group == null)
                    throw new DataNotFoundException(DalText.Group_NotFound);

                return group;
            }
        }

        #endregion Fetch

        #region Insert

        /// <summary>
        /// Creates a new group using the specified data.
        /// </summary>
        /// <param name="dao">The data of the group.</param>
        public void Insert(
            GroupDao dao
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Check unique group code.
                Group group = ctx.DbContext.Groups
                    .Where(e =>
                        e.GroupCode == dao.GroupCode
                    )
                    .FirstOrDefault();
                if (group != null)
                    throw new DataExistException(DalText.Group_GroupCodeExists.With(dao.GroupCode));

                // Create the new group.
                group = new Group
                {
                    GroupCode = dao.GroupCode,
                    GroupName = dao.GroupName
                };
                ctx.DbContext.Groups.Add(group);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new InsertFailedException(DalText.Group_InsertFailed);

                //// Create group members.
                //List<long> memberKeys = KeyList.ToList(dao.Members);
                //if (memberKeys.Count > 0)
                //{
                //    // Check person keys.
                //    memberKeys = ctx.DbContext.Persons
                //        .Where(e => memberKeys.Contains(e.PersonKey.Value))
                //        .Select(e => e.PersonKey.Value)
                //        .ToList();

                //    foreach (long memberKey in memberKeys)
                //    {
                //        Membership membership = new Membership
                //        {
                //            GroupKey = group.GroupKey,
                //            PersonKey = memberKey
                //        };
                //        ctx.DbContext.Memberships.Add(membership);
                //    }
                //    count = ctx.DbContext.SaveChanges();
                //    if (count == 0)
                //        throw new InsertFailedException(DalText.Membership_InsertFailed);
                //}

                // Return new data.
                dao.GroupKey = group.GroupKey;
                dao.Timestamp = group.Timestamp;
            }
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing group using the specified data.
        /// </summary>
        /// <param name="dao">The data of the group.</param>
        public void Update(
            GroupDao dao
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Get the specified group.
                Group group = Db.Groups
                    .Where(e =>
                        e.GroupKey == dao.GroupKey
                    )
                    .FirstOrDefault();
                if (group == null)
                    throw new DataNotFoundException(DalText.Group_NotFound);
                if (group.Timestamp != dao.Timestamp)
                    throw new ConcurrencyException(DalText.Group_Concurrency);

                // Check unique group code.
                if (group.GroupCode != dao.GroupCode)
                {
                    int exist = ctx.DbContext.Groups
                        .Where(e => e.GroupCode == dao.GroupCode && e.GroupKey != group.GroupKey)
                        .Count();
                    if (exist > 0)
                        throw new DataExistException(DalText.Group_GroupCodeExists.With(dao.GroupCode));
                }

                // Update the group.
                group.GroupCode = dao.GroupCode;
                group.GroupName = dao.GroupName;

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.Group_UpdateFailed);

                //// Update group members.
                //bool hasChanged = false;
                //List<long> memberKeys = KeyList.ToList(dao.Members);
                //List<Membership> memberships = ctx.DbContext.Memberships
                //    .Where(e => e.GroupKey == dao.GroupKey)
                //    .ToList();

                //// --- remove deleted items
                //for (int i = memberships.Count; i > 0; i--)
                //{
                //    Membership membership = memberships.ElementAt(i - 1);
                //    if (!memberKeys.Contains(membership.PersonKey.Value))
                //    {
                //        ctx.DbContext.Memberships.Remove(membership);
                //        hasChanged = true;
                //    }
                //}
                //// --- add new items
                //foreach (long memberKey in memberKeys)
                //{
                //    if (!memberships.Exists(e => e.PersonKey == memberKey))
                //    {
                //        Membership membership = new Membership
                //        {
                //            GroupKey = group.GroupKey,
                //            PersonKey = memberKey
                //        };
                //        ctx.DbContext.Memberships.Add(membership);
                //        hasChanged = true;
                //    }
                //}
                //if (hasChanged)
                //    count += ctx.DbContext.SaveChanges();

                if (count == 0)
                    throw new UpdateFailedException(DalText.Group_UpdateFailed);

                // Return new data.
                dao.Timestamp = group.Timestamp;
            }
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified group.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        public void Delete(
            GroupCriteria criteria
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Get the specified group.
                Group group = ctx.DbContext.Groups
                    .Where(e =>
                        e.GroupKey == criteria.GroupKey
                     )
                    .AsNoTracking()
                    .FirstOrDefault();

                if (group == null)
                    throw new DataNotFoundException(DalText.Group_NotFound);

                // Check or delete references
                //int dependents = 0;

                //dependents = ctx.DbContext.Others.Count(e => e.GroupKey == criteria.GroupKey);
                //if (dependents > 0)
                //    throw new DeleteFailedException(DalText.Group_Delete_Others);

                // Delete the group.
                ctx.DbContext.Groups.Remove(group);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new DeleteFailedException(DalText.Group_DeleteFailed);
            }
        }

        #endregion Delete
    }
}
