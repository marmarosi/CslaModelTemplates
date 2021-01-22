using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionByCode;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.SelectionByCode
{
    /// <summary>
    /// Implements the data access functions of the read-only root choice collection.
    /// </summary>
    public class RootChoiceDal : IRootChoiceDal
    {
        #region Fetch

        /// <summary>
        /// Gets the choice of the roots.
        /// </summary>
        /// <param name="criteria">The criteria of the root choice.</param>
        /// <returns>The data transfer object of the requested root choice.</returns>
        public List<CodeNameOptionDao> Fetch(
            RootChoiceCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                List<CodeNameOptionDao> choice = ctx.DbContext.Roots
                    .Where(e => e.RootName.Contains(criteria.RootName))
                    .Select(e => new CodeNameOptionDao
                    {
                        Code = e.RootCode,
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
