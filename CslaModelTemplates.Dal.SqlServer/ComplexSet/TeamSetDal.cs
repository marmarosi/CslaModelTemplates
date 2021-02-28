using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Contracts.ComplexSet;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.ComplexSet
{
    /// <summary>
    /// Implements the data access functions of the editable team collection.
    /// </summary>
    public class TeamSetDal : ITeamSetDal
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
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                List<TeamSetItemDao> list = ctx.DbContext.Teams
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
                            .Select(i => new TeamSetPlayerDao
                            {
                                PlayerKey = i.PlayerKey,
                                TeamKey = i.TeamKey,
                                PlayerCode = i.PlayerCode,
                                PlayerName = i.PlayerName
                            })
                            .OrderBy(io => io.PlayerName)
                            .ToList(),
                        Timestamp = e.Timestamp
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
