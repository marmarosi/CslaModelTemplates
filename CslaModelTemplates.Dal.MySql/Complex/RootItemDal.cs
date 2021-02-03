using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.MySql.Entities;
using CslaModelTemplates.Resources;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.Complex
{
    /// <summary>
    /// Implements the data access functions of the editable root item object.
    /// </summary>
    public class RootItemDal : IRootItemDal
    {
        #region Insert

        /// <summary>
        /// Creates a new root item using the specified data.
        /// </summary>
        /// <param name="dao">The data of the root item.</param>
        public void Insert(
            RootItemDao dao
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Check unique root item code.
                RootItem item = ctx.DbContext.RootItems
                    .Where(e =>
                        e.RootKey == dao.RootKey &&
                        e.RootItemCode == dao.RootItemCode
                    )
                    .FirstOrDefault();
                if (item != null)
                    throw new DataExistException(DalText.RootItem_RootItemCodeExists.With(dao.RootItemCode));

                // Create the new root item.
                item = new RootItem
                {
                    RootKey = dao.RootKey,
                    RootItemCode = dao.RootItemCode,
                    RootItemName = dao.RootItemName
                };
                ctx.DbContext.RootItems.Add(item);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new InsertFailedException(DalText.RootItem_InsertFailed.With(item.RootItemCode));

                // Return new data.
                dao.RootItemKey = item.RootItemKey;
            }
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing root item using the specified data.
        /// </summary>
        /// <param name="dao">The data of the root item.</param>
        public void Update(
            RootItemDao dao
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified root item.
                RootItem item = ctx.DbContext.RootItems
                    .Where(e =>
                        e.RootItemKey == dao.RootItemKey
                    )
                    .FirstOrDefault();
                if (item == null)
                    throw new DataNotFoundException(DalText.RootItem_NotFound);

                // Check unique root item code.
                if (item.RootItemCode != dao.RootItemCode)
                {
                    int exist = ctx.DbContext.RootItems
                        .Where(e =>
                            e.RootKey == dao.RootKey &&
                            e.RootItemCode == dao.RootItemCode &&
                            e.RootItemKey != item.RootItemKey
                        )
                        .Count();
                    if (exist > 0)
                        throw new DataExistException(DalText.RootItem_RootItemCodeExists.With(dao.RootItemCode));
                }

                // Update the root item.
                item.RootItemCode = dao.RootItemCode;
                item.RootItemName = dao.RootItemName;

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.RootItem_UpdateFailed.With(item.RootItemCode));

                // Return new data.
            }
        }
        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified root item.
        /// </summary>
        /// <param name="criteria">The criteria of the root item.</param>
        public void Delete(
            RootItemCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified root item.
                RootItem item = ctx.DbContext.RootItems
                    .Where(e =>
                        e.RootItemKey == criteria.RootItemKey
                     )
                    .FirstOrDefault();
                if (item == null)
                    throw new DataNotFoundException(DalText.RootItem_NotFound);

                // Delete the root item.
                ctx.DbContext.RootItems.Remove(item);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new DeleteFailedException(DalText.RootItem_DeleteFailed.With(item.RootItemCode));
            }
        }

        #endregion Delete
    }
}
