using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Contracts.SimpleList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.SimpleList
{
    /// <summary>
    /// Implements the data access functions of the read-only root collection.
    /// </summary>
    public class SimpleRootListDal : ISimpleRootListDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified roots.
        /// </summary>
        /// <param name="criteria">The criteria of the root list.</param>
        /// <returns>The requested root items.</returns>
        public List<SimpleRootListItemDao> Fetch(
            SimpleRootListCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                List<SimpleRootListItemDao> list = ctx.DbContext.Roots
                    .Where(e =>
                        criteria.RootName == null || e.RootName.Contains(criteria.RootName)
                    )
                    .Select(e => new SimpleRootListItemDao
                    {
                        RootKey = e.RootKey,
                        RootCode = e.RootCode,
                        RootName = e.RootName
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
