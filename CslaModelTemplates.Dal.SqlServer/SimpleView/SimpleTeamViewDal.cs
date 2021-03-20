using CslaModelTemplates.Contracts.SimpleView;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.SimpleView
{
    /// <summary>
    /// Implements the data access functions of the read-only team object.
    /// </summary>
    public class SimpleTeamViewDal : SqlServerDal, ISimpleTeamViewDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified team view.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <returns>The requested team view.</returns>
        public SimpleTeamViewDao Fetch(
            SimpleTeamViewCriteria criteria
            )
        {
            // Get the specified team.
            SimpleTeamViewDao team = DbContext.Teams
                .Where(e =>
                    e.TeamKey == criteria.TeamKey
                 )
                .Select(e => new SimpleTeamViewDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName
                })
                .AsNoTracking()
                .FirstOrDefault();

            if (team == null)
                throw new DataNotFoundException(DalText.SimpleTeam_NotFound);

            return team;
        }

        #endregion Fetch
    }
}
