using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common.DataTransfer;
using CslaModelTemplates.Contracts.PaginatedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.PaginatedList
{
    /// <summary>
    /// Implements the data access functions of the read-only paginated team collection.
    /// </summary>
    public class PaginatedTeamListDal : IPaginatedTeamListDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified page of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested page of the team list.</returns>
        public PaginatedList<PaginatedTeamListItemDao> Fetch(
            PaginatedTeamListCriteria criteria
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Filter the teams.
                var query = ctx.DbContext.Teams
                    .Where(e =>
                        criteria.TeamName == null || e.TeamName.Contains(criteria.TeamName)
                    );

                // Get the requested page.
                List<PaginatedTeamListItemDao> list = query
                    .Select(e => new PaginatedTeamListItemDao
                    {
                        TeamKey = e.TeamKey,
                        TeamCode = e.TeamCode,
                        TeamName = e.TeamName
                    })
                    .OrderBy(o => o.TeamName)
                    .Skip(criteria.PageIndex * criteria.PageSize)
                    .Take(criteria.PageSize)
                    .AsNoTracking()
                    .ToList();

                // Count the matching teams.
                int totalCount = query.Count();

                // Return the result.
                return new PaginatedList<PaginatedTeamListItemDao>
                {
                    Data = list,
                    TotalCount = totalCount
                };
            }
        }

        #endregion GetList
    }
}
