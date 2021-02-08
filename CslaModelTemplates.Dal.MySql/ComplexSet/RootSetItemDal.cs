using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.MySql.Entities;
using CslaModelTemplates.Resources;
using System;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.ComplexSet
{
    /// <summary>
    /// Implements the data access functions of the editable root set item object.
    /// </summary>
    public class RootSetItemDal : IRootSetItemDal
    {
        #region Insert

        /// <summary>
        /// Creates a new root using the specified data.
        /// </summary>
        /// <param name="dao">The data of the root.</param>
        public void Insert(
            RootSetItemDao dao
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
                    throw new DataExistException(DalText.RootSetItem_RootCodeExists.With(dao.RootCode));

                // Create the new root.
                root = new Root
                {
                    RootCode = dao.RootCode,
                    RootName = dao.RootName
                };
                ctx.DbContext.Roots.Add(root);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new InsertFailedException(DalText.RootSetItem_InsertFailed.With(root.RootCode));

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
            RootSetItemDao dao
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
                    throw new DataNotFoundException(DalText.RootSetItem_NotFound.With(dao.RootCode));
                if (root.Timestamp != dao.Timestamp)
                    throw new ConcurrencyException(DalText.RootSetItem_Concurrency.With(dao.RootCode));

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
                        throw new DataExistException(DalText.RootSetItem_RootCodeExists.With(dao.RootCode));
                }

                // Update the root.
                root.RootCode = dao.RootCode;
                root.RootName = dao.RootName;
                root.Timestamp = DateTime.Now; // Force update timestamp.

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.RootSetItem_UpdateFailed.With(root.RootCode));

                // Return new data.
                dao.Timestamp = root.Timestamp;
            }
        }
        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified root.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        public void Delete(
            RootSetItemCriteria criteria
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
                    throw new DataNotFoundException(DalText.RootSetItem_NotFound.With(root.RootCode));

                // Check or delete references
                //int dependents = 0;

                //dependents = ctx.DbContext.Others.Count(e => e.RootKey == criteria.RootKey);
                //if (dependents > 0)
                //    throw new DeleteFailedException(DalText.RootSetItem_Delete_Others);

                // Delete the root.
                ctx.DbContext.Roots.Remove(root);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new DeleteFailedException(DalText.RootSetItem_DeleteFailed.With(root.RootCode));
            }
        }

        #endregion Delete
    }
}
