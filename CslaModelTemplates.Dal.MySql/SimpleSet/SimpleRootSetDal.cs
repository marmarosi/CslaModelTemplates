using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Contracts.SimpleSet;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.SimpleSet
{
    /// <summary>
    /// Implements the data access functions of the editable root collection.
    /// </summary>
    public class SimpleRootSetDal : ISimpleRootSetDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified roots.
        /// </summary>
        /// <param name="criteria">The criteria of the root set.</param>
        /// <returns>The requested root items.</returns>
        public List<SimpleRootSetItemDao> Fetch(
            SimpleRootSetCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                List<SimpleRootSetItemDao> list = ctx.DbContext.Roots
                    .Where(e =>
                        criteria.RootName == null || e.RootName.Contains(criteria.RootName)
                    )
                    .Select(e => new SimpleRootSetItemDao
                    {
                        RootKey = e.RootKey,
                        RootCode = e.RootCode,
                        RootName = e.RootName,
                        Timestamp = e.Timestamp
                    })
                    .OrderBy(o => o.RootName)
                    .AsNoTracking()
                    .ToList();

                return list;
            }
        }

        #endregion GetList
    }
}
