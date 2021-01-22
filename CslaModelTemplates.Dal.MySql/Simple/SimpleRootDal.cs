using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.MySql;
using CslaModelTemplates.Dal.MySql.Entities;
using CslaModelTemplates.Resources;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.Simple
{
    /// <summary>
    /// Implements the data access functions of the editable root object.
    /// </summary>
    public class SimpleRootDal : ISimpleRootDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified root.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        /// <returns>The requested root.</returns>
        public SimpleRootDao Fetch(
            SimpleRootCriteria criteria
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
                    throw new DataNotFoundException(DalText.SimpleRoot_NotFound);

                return new SimpleRootDao
                {
                    RootKey = root.RootKey,
                    RootCode = root.RootCode,
                    RootName = root.RootName,
                    Timestamp = root.Timestamp
                };
            }
        }

        #endregion Fetch

        #region Insert

        /// <summary>
        /// Creates a new root using the specified data.
        /// </summary>
        /// <param name="dao">The data of the root.</param>
        public void Insert(
            SimpleRootDao dao
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Check unique root code.
                Root root = ctx.DbContext.Roots
                    .Where(e =>
                        e.RootCode == dao.RootCode
                    )
                    .FirstOrDefault()
                    ;
                if (root != null)
                    throw new DataExistException(DalText.SimpleRoot_RootCodeExists.With(dao.RootCode));

                // Create the new root.
                root = new Root
                {
                    RootCode = dao.RootCode,
                    RootName = dao.RootName
                };
                ctx.DbContext.Roots.Add(root);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new InsertFailedException(DalText.SimpleRoot_InsertFailed);

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
            SimpleRootDao dao
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified root.
                Root root = ctx.DbContext.Roots
                    .Where(e =>
                        e.RootKey == dao.RootKey
                    )
                    .FirstOrDefault()
                    ;
                if (root == null)
                    throw new DataNotFoundException(DalText.SimpleRoot_NotFound);
                if (root.Timestamp != dao.Timestamp)
                    throw new ConcurrencyException(DalText.SimpleRoot_Concurrency);

                // Check unique root code.
                if (root.RootCode != dao.RootCode)
                {
                    int exist = ctx.DbContext.Roots
                        .Where(e => e.RootCode == dao.RootCode && e.RootKey != root.RootKey)
                        .Count()
                        ;
                    if (exist > 0)
                        throw new DataExistException(DalText.SimpleRoot_RootCodeExists.With(dao.RootCode));
                }

                // Update the root.
                root.RootCode = dao.RootCode;
                root.RootName = dao.RootName;

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.SimpleRoot_UpdateFailed);

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
            SimpleRootCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified root.
                Root root = ctx.DbContext.Roots
                    .Where(e =>
                        e.RootKey == criteria.RootKey
                     )
                    .FirstOrDefault()
                    ;
                if (root == null)
                    throw new DataNotFoundException(DalText.SimpleRoot_NotFound);

                // Check or delete references
                //int dependents = 0;

                //dependents = ctx.DbContext.Others.Count(e => e.RootKey == criteria.RootKey);
                //if (dependents > 0)
                //    throw new DeleteFailedException(DalText.Root_Delete_Others);

                // Delete the root.
                ctx.DbContext.Roots.Remove(root);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new DeleteFailedException(DalText.SimpleRoot_DeleteFailed);
            }
        }

        #endregion Delete
    }
}