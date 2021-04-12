using CslaModelTemplates.Contracts.ComplexView;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.Sqlite.ComplexView
{
    /// <summary>
    /// Implements the data access functions of the read-only team object.
    /// </summary>
    public class TeamViewDal : SqliteDal, ITeamViewDal
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
            // Get the specified team.
            TeamViewDao team = DbContext.Teams
                .Include(e => e.Players)
                .Where(e =>
                    e.TeamKey == criteria.TeamKey
                 )
                .Select(e => new TeamViewDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName,
                    Players = e.Players
                        .Select(p => new PlayerViewDao
                        {
                            PlayerKey = p.PlayerKey,
                            PlayerCode = p.PlayerCode,
                            PlayerName = p.PlayerName
                        })
                    .OrderBy(p => p.PlayerName)
                    .ToList()
                })
                .AsNoTracking()
                .FirstOrDefault();

            if (team == null)
                throw new DataNotFoundException(DalText.Team_NotFound);

            return team;
        }

        #endregion Fetch
    }
}
