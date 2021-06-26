using CslaModelTemplates.Contracts.SimpleSet;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.Sqlite.SimpleSet
{
    /// <summary>
    /// Implements the data access functions of the editable team collection.
    /// </summary>
    public class SimpleTeamSetDal : SqliteDal, ISimpleTeamSetDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified team set.
        /// </summary>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <returns>The requested team set.</returns>
        public List<SimpleTeamSetItemDao> Fetch(
            SimpleTeamSetCriteria criteria
            )
        {
            List<SimpleTeamSetItemDao> list = DbContext.Teams
                .Where(e =>
                    criteria.TeamName == null || e.TeamName.Contains(criteria.TeamName)
                )
                .Select(e => new SimpleTeamSetItemDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName,
                    Timestamp = e.Timestamp
                })
                .OrderBy(o => o.TeamName)
                .ToList();

            return list;
        }

        #endregion GetList
    }
}
