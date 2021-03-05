using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Contracts.LookUpView;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.LookUpView
{
    /// <summary>
    /// Implements the data access functions of the read-only group object.
    /// </summary>
    public class GroupViewDal : IGroupViewDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified group view.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        /// <returns>The requested group view.</returns>
        public GroupViewDao Fetch(
            GroupViewCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified group.
                GroupViewDao group = ctx.DbContext.Groups
                    .Include(e => e.Members)
                    .Where(e =>
                        e.GroupKey == criteria.GroupKey
                     )
                    .Select(e => new GroupViewDao
                    {
                        GroupKey = e.GroupKey,
                        GroupCode = e.GroupCode,
                        GroupName = e.GroupName,
                        Members = ctx.DbContext.Groups
                            .Where(g =>
                                g.GroupKey == criteria.GroupKey
                            )
                            .SelectMany(g => g.Members)
                            .Select(p => new MemberViewDao
                            {
                                PersonKey = p.PersonKey,
                                PersonName = p.PersonName
                            })
                            .ToList()
                    })
                    .AsNoTracking()
                    .FirstOrDefault();

                if (group == null)
                    throw new DataNotFoundException(DalText.Group_NotFound);

                return group;
            }
        }

        #endregion Fetch
    }
}
