using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.Junction;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.Oracle.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.Oracle.Junction
{
    /// <summary>
    /// Implements the data access functions of the editable member object.
    /// </summary>
    public class MemberDal : OracleDal, IMemberDal
    {
        #region Insert

        /// <summary>
        /// Creates a new member using the specified data.
        /// </summary>
        /// <param name="dao">The data of the member.</param>
        public void Insert(
            MemberDao dao
            )
        {
            // Check unique membership.
            GroupPerson member = DbContext.GroupPersons
                .Where(e =>
                    e.GroupKey == dao.GroupKey &&
                    e.PersonKey == dao.PersonKey
                )
                .AsNoTracking()
                .FirstOrDefault();
            if (member != null)
                throw new DataExistException(DalText.Member_Exists.With(dao.PersonName));

            // Create the new member.
            Person person = DbContext.Persons.Find(dao.PersonKey);
            if (person == null)
                throw new DataExistException(DalText.Member_NotFound.With(dao.PersonName));

            member = new GroupPerson
            {
                GroupKey = dao.GroupKey,
                PersonKey = dao.PersonKey
            };
            DbContext.GroupPersons.Add(member);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new InsertFailedException(DalText.Member_InsertFailed.With(dao.PersonName));

            // Return new data.
            dao.PersonName = person.PersonName;
        }

        #endregion Insert

        #region Delete

        /// <summary>
        /// Deletes the specified member.
        /// </summary>
        /// <param name="criteria">The criteria of the member.</param>
        public void Delete(
            MemberDao dao
            )
        {
            // Get the specified player.
            GroupPerson member = DbContext.GroupPersons
                .Where(e =>
                    e.GroupKey == dao.GroupKey &&
                    e.PersonKey == dao.PersonKey
                    )
                .AsNoTracking()
                .FirstOrDefault();
            if (member == null)
                throw new DataNotFoundException(DalText.Member_NotFound.With(dao.PersonName));

            // Delete the member.
            DbContext.GroupPersons.Remove(member);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new DeleteFailedException(DalText.Member_DeleteFailed.With(dao.PersonName));
        }

        #endregion Delete
    }
}
