using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Contracts.ComplexView;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.SqlServer.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.ComplexView
{
    /// <summary>
    /// Implements the data access functions of the read-only team object.
    /// </summary>
    public class TeamViewDal : ITeamViewDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified team view.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <returns>The requested team view.</returns>
        public TeamViewDao Fetch(
            TeamViewCriteria criteria
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Get the specified team.
                Team team = ctx.DbContext.Teams
                    .Include(e => e.Players)
                    .Where(e =>
                        e.TeamKey == criteria.TeamKey
                     )
                    .AsNoTracking()
                    .FirstOrDefault();
                if (team == null)
                    throw new DataNotFoundException(DalText.Team_NotFound);

                return new TeamViewDao
                {
                    TeamKey = team.TeamKey,
                    TeamCode = team.TeamCode,
                    TeamName = team.TeamName,
                    Players = team.Players
                        .Select(item => new PlayerViewDao
                        {
                            PlayerKey = item.PlayerKey,
                            PlayerCode = item.PlayerCode,
                            PlayerName = item.PlayerName
                        })
                        .OrderBy(io => io.PlayerName)
                        .ToList()
                };
            }
        }

        #endregion Fetch
    }
}
