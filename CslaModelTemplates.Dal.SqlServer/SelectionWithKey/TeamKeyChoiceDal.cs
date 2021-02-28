using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionWithKey;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.SelectionWithKey
{
    /// <summary>
    /// Implements the data access functions of the read-only team choice collection.
    /// </summary>
    public class TeamKeyChoiceDal : ITeamKeyChoiceDal
    {
        #region Fetch

        /// <summary>
        /// Gets the choice of the managers.
        /// </summary>
        /// <param name="criteria">The criteria of the manager choice.</param>
        /// <returns>The data transfer object of the requested manager choice.</returns>
        public List<KeyNameOptionDao> Fetch(
            TeamKeyChoiceCriteria criteria
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                List<KeyNameOptionDao> choice = ctx.DbContext.Teams
                    .Where(e => criteria.TeamName == null || e.TeamName.Contains(criteria.TeamName))
                    .Select(e => new KeyNameOptionDao
                    {
                        Key = e.TeamKey,
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
