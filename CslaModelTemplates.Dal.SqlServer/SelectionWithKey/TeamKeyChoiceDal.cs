using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.SelectionWithKey;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.SelectionWithKey
{
    /// <summary>
    /// Implements the data access functions of the read-only team choice collection.
    /// </summary>
    public class TeamKeyChoiceDal : SqlServerDal, ITeamKeyChoiceDal
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
            List<KeyNameOptionDao> choice = DbContext.Teams
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

        #endregion GetChoice
    }
}
