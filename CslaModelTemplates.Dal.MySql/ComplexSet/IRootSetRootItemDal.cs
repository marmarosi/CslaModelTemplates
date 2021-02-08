using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.MySql.Entities;
using CslaModelTemplates.Resources;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.ComplexSet
{
    /// <summary>
    /// Implements the data access functions of the editable root item object.
    /// </summary>
    public class RootSetRootItemDal : IRootSetRootItemDal
    {
        #region Insert

        /// <summary>
        /// Creates a new root item using the specified data.
        /// </summary>
        /// <param name="dao">The data of the root item.</param>
        public void Insert(
            RootSetRootItemDao dao
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
                    throw new DataExistException(DalText.RootSetRootItem_RootItemCodeExists
                        .With(dao.__rootCode, dao.RootItemCode));

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
                    throw new InsertFailedException(DalText.RootSetRootItem_InsertFailed
                        .With(dao.__rootCode, dao.RootItemCode));

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
            RootSetRootItemDao dao
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
                    throw new DataNotFoundException(DalText.RootSetRootItem_NotFound
                            .With(dao.__rootCode, dao.RootItemCode));

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
                        throw new DataExistException(DalText.RootSetRootItem_RootItemCodeExists
                            .With(dao.__rootCode, dao.RootItemCode));
                }

                // Update the root item.
                item.RootItemCode = dao.RootItemCode;
                item.RootItemName = dao.RootItemName;

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.RootSetRootItem_UpdateFailed
                        .With(dao.__rootCode, dao.RootItemCode));

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
            RootSetRootItemCriteria criteria
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
                    throw new DataNotFoundException(DalText.RootSetRootItem_NotFound
                        .With(criteria.__rootCode, criteria.__rootItemCode));

                // Delete the root item.
                ctx.DbContext.RootItems.Remove(item);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new DeleteFailedException(DalText.RootSetRootItem_DeleteFailed
                        .With(criteria.__rootCode, criteria.__rootItemCode));
            }
        }

        #endregion Delete
    }
}
