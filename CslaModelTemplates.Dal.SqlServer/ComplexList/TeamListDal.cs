using CslaModelTemplates.Contracts.ComplexList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.ComplexList
{
    /// <summary>
    /// Implements the data access functions of the read-only team collection.
    /// </summary>
    public class TeamListDal : SqlServerDal, ITeamListDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested team items.</returns>
        public List<TeamListItemDao> Fetch(
            TeamListCriteria criteria
            )
        {
            List<TeamListItemDao> list = DbContext.Teams
                .Include(e => e.Players)
                .Where(e =>
                    criteria.TeamName == null || e.TeamName.Contains(criteria.TeamName)
                )
                .Select(e => new TeamListItemDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName,
                    Players = e.Players.Select(i => new PlayerListItemDao
                    {
                        PlayerKey = i.PlayerKey,
                        PlayerCode = i.PlayerCode,
                        PlayerName = i.PlayerName
                    })
                    .OrderBy(io => io.PlayerName)
                    .ToList()
                })
                .OrderBy(o => o.TeamName)
                .AsNoTracking()
                .ToList();

            return list;
        }

        #endregion GetList
    }
}
