using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Contracts.ComplexList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.ComplexList
{
    /// <summary>
    /// Implements the data access functions of the read-only root collection.
    /// </summary>
    public class RootListDal : IRootListDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified roots.
        /// </summary>
        /// <param name="criteria">The criteria of the root list.</param>
        /// <returns>The requested root items.</returns>
        public List<RootListItemDao> Fetch(
            RootListCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                List<RootListItemDao> list = ctx.DbContext.Roots
                    .Include(e => e.Items)
                    .Where(e =>
                        criteria.RootName == null || e.RootName.Contains(criteria.RootName)
                    )
                    .Select(e => new RootListItemDao
                    {
                        RootKey = e.RootKey,
                        RootCode = e.RootCode,
                        RootName = e.RootName,
                        Items = e.Items.Select(i => new RootItemListItemDao
                        {
                            RootItemKey = i.RootItemKey,
                            RootItemCode = i.RootItemCode,
                            RootItemName = i.RootItemName
                        })
                        .OrderBy(io => io.RootItemName)
                        .ToList()
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
