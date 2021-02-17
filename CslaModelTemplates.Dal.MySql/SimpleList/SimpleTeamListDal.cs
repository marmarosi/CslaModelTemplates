using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Contracts.SimpleList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.SimpleList
{
    /// <summary>
    /// Implements the data access functions of the read-only team collection.
    /// </summary>
    public class SimpleTeamListDal : ISimpleTeamListDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested team items.</returns>
        public List<SimpleTeamListItemDao> Fetch(
            SimpleTeamListCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                List<SimpleTeamListItemDao> list = ctx.DbContext.Teams
                    .Where(e =>
                        criteria.TeamName == null || e.TeamName.Contains(criteria.TeamName)
                    )
                    .Select(e => new SimpleTeamListItemDao
                    {
                        TeamKey = e.TeamKey,
                        TeamCode = e.TeamCode,
                        TeamName = e.TeamName
                    })
                    .OrderBy(o => o.TeamName)
                    .AsNoTracking()
                    .ToList();

                return list;
            }
        }

        #endregion GetList
    }
}
