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
    public class RootListDal : IRootListDal
    {
        #region Get

        /// <summary>
        /// Gets the list of the roots.
        /// </summary>
        /// <param name="criteria">The criteria of the root list.</param>
        /// <returns>The requested root collection.</returns>
        public List<RootListItemDao> Get(
            RootListCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                List<RootListItemDao> list = ctx.DbContext.Roots
                    .Where(e => e.RootName.Contains(criteria.RootName))
                    .Select(e => new RootListItemDao
                    {
                        RootKey = e.RootKey,
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
