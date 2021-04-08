using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.Junction;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.Oracle.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CslaModelTemplates.Dal.Oracle.Junction
{
    /// <summary>
    /// Implements the data access functions of the editable group object.
    /// </summary>
    public class GroupDal : OracleDal, IGroupDal
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
            // Get the specified group.
            GroupDao group = DbContext.Groups
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
            // Check unique group code.
            Group group = DbContext.Groups
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
            DbContext.Groups.Add(group);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new InsertFailedException(DalText.Group_InsertFailed);

            // Return new data.
            dao.GroupKey = group.GroupKey;
            dao.Timestamp = group.Timestamp;
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
            // Get the specified group.
            Group group = DbContext.Groups
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
                int exist = DbContext.Groups
                    .Where(e => e.GroupCode == dao.GroupCode && e.GroupKey != group.GroupKey)
                    .Count();
                if (exist > 0)
                    throw new DataExistException(DalText.Group_GroupCodeExists.With(dao.GroupCode));
            }

            // Update the group.
            group.GroupCode = dao.GroupCode;
            group.GroupName = dao.GroupName;
            group.Timestamp = DateTime.Now; // Force update timestamp.

            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new UpdateFailedException(DalText.Group_UpdateFailed);

            if (count == 0)
                throw new UpdateFailedException(DalText.Group_UpdateFailed);

            // Return new data.
            dao.Timestamp = group.Timestamp;
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
            // Get the specified group.
            Group group = DbContext.Groups
                .Where(e =>
                    e.GroupKey == criteria.GroupKey
                 )
                .AsNoTracking()
                .FirstOrDefault();

            if (group == null)
                throw new DataNotFoundException(DalText.Group_NotFound);

            // Check or delete references
            //int dependents = 0;

            //dependents = DbContext.Others.Count(e => e.GroupKey == criteria.GroupKey);
            //if (dependents > 0)
            //    throw new DeleteFailedException(DalText.Group_Delete_Others);

            // Delete the group.
            DbContext.Groups.Remove(group);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new DeleteFailedException(DalText.Group_DeleteFailed);
        }

        #endregion Delete
    }
}
