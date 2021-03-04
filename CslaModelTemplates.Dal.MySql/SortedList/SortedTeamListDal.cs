using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common.DataTransfer;
using CslaModelTemplates.Contracts.SortedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.SortedList
{
    /// <summary>
    /// Implements the data access functions of the read-only paginated team collection.
    /// </summary>
    public class SortedTeamListDal : ISortedTeamListDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested team list.</returns>
        public List<SortedTeamListItemDao> Fetch(
            SortedTeamListCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Filter the teams.
                var query = ctx.DbContext.Teams
                    .Where(e =>
                        criteria.TeamName == null || e.TeamName.Contains(criteria.TeamName)
                    )
                    .Select(e => new SortedTeamListItemDao
                    {
                        TeamKey = e.TeamKey,
                        TeamCode = e.TeamCode,
                        TeamName = e.TeamName
                    });

                // Sort the items.
                switch (criteria.SortBy)
                {
                    case SortedTeamListSortBy.TeamCode:
                        query = criteria.SortDirection == SortDirection.Ascending
                            ? query.OrderBy(e => e.TeamCode)
                            : query.OrderByDescending(e => e.TeamCode);
                        break;
                    case SortedTeamListSortBy.TeamName:
                    default:
                        break;
                }

                // Return the result.
                List<SortedTeamListItemDao> list = query
                    .AsNoTracking()
                    .ToList();

                return list;
            }
        }

        #endregion GetList
    }
}
