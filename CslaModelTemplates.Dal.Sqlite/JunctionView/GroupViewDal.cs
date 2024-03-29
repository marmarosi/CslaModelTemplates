using CslaModelTemplates.Contracts.JunctionView;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.Sqlite.JunctionView
{
    /// <summary>
    /// Implements the data access functions of the read-only group object.
    /// </summary>
    public class GroupViewDal : SqliteDal, IGroupViewDal
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
            // Get the specified group.
            GroupViewDao group = DbContext.Groups
                .Include(e => e.Persons)
                .Where(e =>
                    e.GroupKey == criteria.GroupKey
                 )
                .Select(e => new GroupViewDao
                {
                    GroupKey = e.GroupKey,
                    GroupCode = e.GroupCode,
                    GroupName = e.GroupName,
                    Persons = e.Persons
                        .Select(m => new GroupPersonViewDao
                        {
                            PersonKey = m.PersonKey,
                            PersonName = m.Person.PersonName
                        })
                        .ToList()
                })
                .AsNoTracking()
                .FirstOrDefault();

            if (group == null)
                throw new DataNotFoundException(DalText.Group_NotFound);

            return group;
        }

        #endregion Fetch
    }
}
