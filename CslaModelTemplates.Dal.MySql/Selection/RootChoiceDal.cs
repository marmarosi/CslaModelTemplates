using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.Selection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.Selection
{
    /// <summary>
    /// Implements the data access functions of the read-only root choice collection.
    /// </summary>
    public class RootChoiceDal : IRootChoiceDal
    {
        #region Fetch

        /// <summary>
        /// Gets the choice of the managers.
        /// </summary>
        /// <param name="criteria">The criteria of the manager choice.</param>
        /// <returns>The data transfer object of the requested manager choice.</returns>
        public List<KeyNameOptionDao> Fetch(
            RootChoiceCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                List<KeyNameOptionDao> choice = ctx.DbContext.Roots
                    .Where(e => e.RootName.Contains(criteria.RootName))
                    .Select(e => new KeyNameOptionDao
                    {
                        Key = e.RootKey,
                        Name = e.RootName
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
