using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Contracts.SimpleView;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.MySql.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.SimpleView
{
    /// <summary>
    /// Implements the data access functions of the read-only team object.
    /// </summary>
    public class SimpleTeamViewDal : ISimpleTeamViewDal
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
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified team.
                Team team = ctx.DbContext.Teams
                    .Where(e =>
                        e.TeamKey == criteria.TeamKey
                     )
                    .AsNoTracking()
                    .FirstOrDefault();
                if (team == null)
                    throw new DataNotFoundException(DalText.SimpleTeam_NotFound);

                return new SimpleTeamViewDao
                {
                    TeamKey = team.TeamKey,
                    TeamCode = team.TeamCode,
                    TeamName = team.TeamName
                };
            }
        }

        #endregion Fetch
    }
}
