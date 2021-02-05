using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.ComplexCommand;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.ComplexCommand
{
    /// <summary>
    /// Implements the data access functions of the count roots by item count command.
    /// </summary>
    public class CountRootsDal : ICountRootsDal
    {
        private string COMMAND = typeof(CountRootsDal).Name.CutEnd(3);

        #region Execute

        /// <summary>
        /// Counts the roots grouped by the number of their items.
        /// </summary>
        /// <param name="criteria">The criteria of the command.</param>
        public List<CountRootsListItemDao> Execute(
            CountRootsCriteria criteria
            )
        {
            string rootName = criteria.RootName ?? "";
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                List<CountRootsListItemDao> list = ctx.DbContext.Roots
                    .Include(e => e.Items)
                    .Where(e => rootName == "" || e.RootName.Contains(rootName))
                    .GroupBy(
                        e => e.Items.Count,
                        (key, grp) => new CountRootsListItemDao
                        {
                            ItemCount = key,
                            CountOfRoots = grp.Count()
                        })
                    .OrderByDescending(o => o.ItemCount)
                    .AsNoTracking()
                    .ToList();

                return list;
            }
        }

        #endregion
    }
}
