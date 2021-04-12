using CslaModelTemplates.Contracts.ComplexSet;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.Sqlite.ComplexSet
{
    /// <summary>
    /// Implements the data access functions of the editable team collection.
    /// </summary>
    public class TeamSetDal : SqliteDal, ITeamSetDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified team set.
        /// </summary>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <returns>The requested team set.</returns>
        public List<TeamSetItemDao> Fetch(
            TeamSetCriteria criteria
            )
        {
            List<TeamSetItemDao> list = DbContext.Teams
                .Include(e => e.Players)
                .Where(e =>
                    criteria.TeamName == null || e.TeamName.Contains(criteria.TeamName)
                )
                .Select(e => new TeamSetItemDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName,
                    Players = e.Players
                        .Select(p => new TeamSetPlayerDao
                        {
                            PlayerKey = p.PlayerKey,
                            TeamKey = p.TeamKey,
                            PlayerCode = p.PlayerCode,
                            PlayerName = p.PlayerName
                        })
                        .OrderBy(p => p.PlayerName)
                        .ToList(),
                    Timestamp = e.Timestamp
                })
                .OrderBy(o => o.TeamName)
                .AsNoTracking()
                .ToList();

            return list;
        }

        #endregion GetList
    }
}
