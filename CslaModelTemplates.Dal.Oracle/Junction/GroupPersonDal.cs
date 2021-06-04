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
    /// Implements the data access functions of the editable group-person object.
    /// </summary>
    public class GroupPersonDal : OracleDal, IGroupPersonDal
    {
        #region Insert

        /// <summary>
        /// Creates a new group-person using the specified data.
        /// </summary>
        /// <param name="dao">The data of the group-person.</param>
        public void Insert(
            GroupPersonDao dao
            )
        {
            // Check unique group-person.
            GroupPerson groupPerson = DbContext.GroupPersons
                .Where(e =>
                    e.GroupKey == dao.GroupKey &&
                    e.PersonKey == dao.PersonKey
                )
                .AsNoTracking()
                .FirstOrDefault();
            if (groupPerson != null)
                throw new DataExistException(DalText.GroupPerson_Exists.With(dao.PersonName));

            // Create the new group-person.
            Person person = DbContext.Persons.Find(dao.PersonKey);
            if (person == null)
                throw new DataExistException(DalText.GroupPerson_NotFound.With(dao.PersonName));

            groupPerson = new GroupPerson
            {
                GroupKey = dao.GroupKey,
                PersonKey = dao.PersonKey
            };
            DbContext.GroupPersons.Add(groupPerson);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new InsertFailedException(DalText.GroupPerson_InsertFailed.With(dao.PersonName));

            // Return new data.
            dao.PersonName = person.PersonName;
        }

        #endregion Insert

        #region Delete

        /// <summary>
        /// Deletes the specified group-person.
        /// </summary>
        /// <param name="criteria">The criteria of the group-person.</param>
        public void Delete(
            GroupPersonDao dao
            )
        {
            // Get the specified group-person.
            GroupPerson groupPerson = DbContext.GroupPersons
                .Where(e =>
                    e.GroupKey == dao.GroupKey &&
                    e.PersonKey == dao.PersonKey
                    )
                .AsNoTracking()
                .FirstOrDefault();
            if (groupPerson == null)
                throw new DataNotFoundException(DalText.GroupPerson_NotFound.With(dao.PersonName));

            // Delete the group-person.
            DbContext.GroupPersons.Remove(groupPerson);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new DeleteFailedException(DalText.GroupPerson_DeleteFailed.With(dao.PersonName));
        }

        #endregion Delete
    }
}
