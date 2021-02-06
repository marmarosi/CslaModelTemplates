using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.MySql.Entities;
using CslaModelTemplates.Resources;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.SimpleSet
{
    /// <summary>
    /// Implements the data access functions of the editable root set item object.
    /// </summary>
    public class SimpleRootSetItemDal : ISimpleRootSetItemDal
    {
        #region Insert

        /// <summary>
        /// Creates a new root using the specified data.
        /// </summary>
        /// <param name="dao">The data of the root.</param>
        public void Insert(
            SimpleRootSetItemDao dao
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Check unique root code.
                Root root = ctx.DbContext.Roots
                    .Where(e =>
                        e.RootCode == dao.RootCode
                    )
                    .FirstOrDefault();
                if (root != null)
                    throw new DataExistException(DalText.SimpleRootSetItem_RootCodeExists.With(dao.RootCode));

                // Create the new root.
                root = new Root
                {
                    RootCode = dao.RootCode,
                    RootName = dao.RootName
                };
                ctx.DbContext.Roots.Add(root);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new InsertFailedException(DalText.SimpleRootSetItem_InsertFailed.With(root.RootCode));

                // Return new data.
                dao.RootKey = root.RootKey;
                dao.Timestamp = root.Timestamp;
            }
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing root using the specified data.
        /// </summary>
        /// <param name="dao">The data of the root.</param>
        public void Update(
            SimpleRootSetItemDao dao
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified root.
                Root root = ctx.DbContext.Roots
                    .Where(e =>
                        e.RootKey == dao.RootKey
                    )
                    .FirstOrDefault();
                if (root == null)
                    throw new DataNotFoundException(DalText.SimpleRootSetItem_NotFound.With(dao.RootCode));
                if (root.Timestamp != dao.Timestamp)
                    throw new ConcurrencyException(DalText.SimpleRootSetItem_Concurrency.With(dao.RootCode));

                // Check unique root code.
                if (root.RootCode != dao.RootCode)
                {
                    int exist = ctx.DbContext.Roots
                        .Where(e =>
                            e.RootCode == dao.RootCode &&
                            e.RootKey != root.RootKey
                        )
                        .Count();
                    if (exist > 0)
                        throw new DataExistException(DalText.SimpleRootSetItem_RootCodeExists.With(dao.RootCode));
                }

                // Update the root.
                root.RootCode = dao.RootCode;
                root.RootName = dao.RootName;

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.SimpleRootSetItem_UpdateFailed.With(root.RootCode));

                // Return new data.
            }
        }
        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified root.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        public void Delete(
            SimpleRootSetItemCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified root.
                Root root = ctx.DbContext.Roots
                    .Where(e =>
                        e.RootKey == criteria.RootKey
                     )
                    .FirstOrDefault();
                if (root == null)
                    throw new DataNotFoundException(DalText.SimpleRootSetItem_NotFound.With(root.RootCode));

                // Check or delete references
                //int dependents = 0;

                //dependents = ctx.DbContext.Others.Count(e => e.RootKey == criteria.RootKey);
                //if (dependents > 0)
                //    throw new DeleteFailedException(DalText.SimpleRootSetItem_Delete_Others);

                // Delete the root.
                ctx.DbContext.Roots.Remove(root);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new DeleteFailedException(DalText.SimpleRootSetItem_DeleteFailed.With(root.RootCode));
            }
        }

        #endregion Delete
    }
}
