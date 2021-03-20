using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.ComplexCommand;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.ComplexCommand
{
    /// <summary>
    /// Implements the data access functions of the count teams by player count command.
    /// </summary>
    public class CountTeamsDal : SqlServerDal, ICountTeamsDal
    {
        private string COMMAND = typeof(CountTeamsDal).Name.CutEnd(3);

        #region Execute

        /// <summary>
        /// Counts the teams grouped by the number of their players.
        /// </summary>
        /// <param name="criteria">The criteria of the command.</param>
        public List<CountTeamsListItemDao> Execute(
            CountTeamsCriteria criteria
            )
        {
            string teamName = criteria.TeamName ?? "";

            var counts = DbContext.Teams
                .Include(e => e.Players)
                .Where(e => teamName == "" || e.TeamName.Contains(teamName))
                .Select(e => new { e.TeamKey, Count = e.Players.Count })
                .AsNoTracking()
                .ToList();

            List<CountTeamsListItemDao> list = counts
                .GroupBy(
                    e => e.Count,
                    (key, grp) => new CountTeamsListItemDao
                    {
                        ItemCount = key,
                        CountOfTeams = grp.Count()
                    })
                .OrderByDescending(o => o.ItemCount)
                .ToList();

            return list;
        }

        #endregion
    }
}
