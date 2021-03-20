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
    /// Implements the data access functions of the editable member object.
    /// </summary>
    public class MemberDal : IMemberDal
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
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Check unique membership.
                Membership member = ctx.DbContext.Memberships
                    .Where(e =>
                        e.GroupKey == dao.GroupKey &&
                        e.PersonKey == dao.PersonKey
                    )
                    .AsNoTracking()
                    .FirstOrDefault();
                if (member != null)
                    throw new DataExistException(DalText.Member_Exists.With(dao.PersonName));

                // Create the new member.
                member = new Membership
                {
                    GroupKey = dao.GroupKey,
                    PersonKey = dao.PersonKey
                };
                ctx.DbContext.Memberships.Add(member);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new InsertFailedException(DalText.Member_InsertFailed.With(dao.PersonName));

                // Return new data.
            }
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
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Get the specified player.
                Membership member = ctx.DbContext.Memberships
                    .Where(e =>
                        e.GroupKey == dao.GroupKey &&
                        e.PersonKey == dao.PersonKey
                        )
                    .AsNoTracking()
                    .FirstOrDefault();
                if (member == null)
                    throw new DataNotFoundException(DalText.Member_NotFound.With(dao.PersonName));

                // Delete the member.
                ctx.DbContext.Memberships.Remove(member);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new DeleteFailedException(DalText.Member_DeleteFailed.With(dao.PersonName));
            }
        }

        #endregion Delete
    }
}
