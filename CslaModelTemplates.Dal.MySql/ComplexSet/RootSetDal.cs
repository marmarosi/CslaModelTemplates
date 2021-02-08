using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Contracts.ComplexSet;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.ComplexSet
{
    /// <summary>
    /// Implements the data access functions of the editable root collection.
    /// </summary>
    public class RootSetDal : IRootSetDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified root set.
        /// </summary>
        /// <param name="criteria">The criteria of the root set.</param>
        /// <returns>The requested root set.</returns>
        public List<RootSetItemDao> Fetch(
            RootSetCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                List<RootSetItemDao> list = ctx.DbContext.Roots
                    .Include(e => e.Items)
                    .Where(e =>
                        criteria.RootName == null || e.RootName.Contains(criteria.RootName)
                    )
                    .Select(e => new RootSetItemDao
                    {
                        RootKey = e.RootKey,
                        RootCode = e.RootCode,
                        RootName = e.RootName,
                        Items = e.Items
                            .Select(i => new RootSetRootItemDao
                            {
                                RootItemKey = i.RootItemKey,
                                RootKey = i.RootKey,
                                RootItemCode = i.RootItemCode,
                                RootItemName = i.RootItemName
                            })
                            .OrderBy(io => io.RootItemName)
                            .ToList(),
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
