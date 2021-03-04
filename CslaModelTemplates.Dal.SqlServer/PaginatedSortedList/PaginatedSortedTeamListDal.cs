using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common.DataTransfer;
using CslaModelTemplates.Contracts.PaginatedSortedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.PaginatedSortedList
{
    /// <summary>
    /// Implements the data access functions of the read-only paginated sorted team collection.
    /// </summary>
    public class PaginatedSortedTeamListDal : IPaginatedSortedTeamListDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified page of sorted teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested page of the sorted team list.</returns>
        public PaginatedList<PaginatedSortedTeamListItemDao> Fetch(
            PaginatedSortedTeamListCriteria criteria
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Filter the teams.
                var query = ctx.DbContext.Teams
                    .Where(e =>
                        criteria.TeamName == null || e.TeamName.Contains(criteria.TeamName)
                    );

                // Sort the items.
                var sorted = query
                    .Select(e => new PaginatedSortedTeamListItemDao
                    {
                        TeamKey = e.TeamKey,
                        TeamCode = e.TeamCode,
                        TeamName = e.TeamName
                    });

                switch (criteria.SortBy)
                {
                    case PaginatedSortedTeamListSortBy.TeamCode:
                        sorted = criteria.SortDirection == SortDirection.Ascending
                            ? sorted.OrderBy(e => e.TeamCode)
                            : sorted.OrderByDescending(e => e.TeamCode);
                        break;
                    case PaginatedSortedTeamListSortBy.TeamName:
                    default:
                        sorted = criteria.SortDirection == SortDirection.Ascending
                            ? sorted.OrderBy(e => e.TeamName)
                            : sorted.OrderByDescending(e => e.TeamName);
                        break;
                }

                // Get the requested page.
                List<PaginatedSortedTeamListItemDao> list = sorted
                    .Skip(criteria.PageIndex * criteria.PageSize)
                    .Take(criteria.PageSize)
                    .AsNoTracking()
                    .ToList();

                // Count the matching teams.
                int totalCount = query.Count();

                // Return the result.
                return new PaginatedList<PaginatedSortedTeamListItemDao>
                {
                    Data = list,
                    TotalCount = totalCount
                };
            }
        }

        #endregion GetList
    }
}
