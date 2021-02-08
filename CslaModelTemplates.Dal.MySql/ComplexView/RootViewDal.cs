using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Contracts.ComplexView;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.MySql.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.ComplexView
{
    /// <summary>
    /// Implements the data access functions of the read-only root object.
    /// </summary>
    public class RootViewDal : IRootViewDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified root view.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        /// <returns>The requested root view.</returns>
        public RootViewDao Fetch(
            RootViewCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified root.
                Root root = ctx.DbContext.Roots
                    .Include(e => e.Items)
                    .Where(e =>
                        e.RootKey == criteria.RootKey
                     )
                    .AsNoTracking()
                    .FirstOrDefault();
                if (root == null)
                    throw new DataNotFoundException(DalText.Root_NotFound);

                return new RootViewDao
                {
                    RootKey = root.RootKey,
                    RootCode = root.RootCode,
                    RootName = root.RootName,
                    Items = root.Items
                        .Select(item => new RootItemViewDao
                        {
                            RootItemKey = item.RootItemKey,
                            RootItemCode = item.RootItemCode,
                            RootItemName = item.RootItemName
                        })
                        .OrderBy(io => io.RootItemName)
                        .ToList()
                };
            }
        }

        #endregion Fetch
    }
}
