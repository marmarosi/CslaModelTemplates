using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionWithCode;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.SelectionWithCode
{
    /// <summary>
    /// Implements the data access functions of the read-only team choice collection.
    /// </summary>
    public class TeamCodeChoiceDal : ITeamCodeChoiceDal
    {
        #region Fetch

        /// <summary>
        /// Gets the choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The data transfer object of the requested team choice.</returns>
        public List<CodeNameOptionDao> Fetch(
            TeamCodeChoiceCriteria criteria
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                List<CodeNameOptionDao> choice = ctx.DbContext.Teams
                    .Where(e =>
                        criteria.TeamName == null || e.TeamName.Contains(criteria.TeamName)
                    )
                    .Select(e => new CodeNameOptionDao
                    {
                        Code = e.TeamCode,
                        Name = e.TeamName
                    })
                    .OrderBy(o => o.Name)
                    .AsNoTracking()
                    .ToList();

                return choice;
            }
        }

        #endregion GetChoice
    }
}
