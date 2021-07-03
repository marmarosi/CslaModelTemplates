using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.SortedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.Oracle.SortedList
{
    /// <summary>
    /// Implements the data access functions of the read-only paginated team collection.
    /// </summary>
    public class SortedTeamListDal : OracleDal, ISortedTeamListDal
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
            // Filter the teams.
            var query = DbContext.Teams
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
                    query = criteria.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(e => e.TeamName)
                        : query.OrderByDescending(e => e.TeamName);
                    break;
            }

            // Return the result.
            List<SortedTeamListItemDao> list = query
                .AsNoTracking()
                .ToList();

            return list;
        }

        #endregion GetList
    }
}
