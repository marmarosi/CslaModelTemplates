using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Dal.MySql;
using CslaModelTemplates.Dal.MySql.Entities;
using CslaModelTemplates.Resources;
using System.Linq;

namespace CslaModelTemplates.Dal.MsSql.Simple
{
    /// <summary>
    /// Implements the data access functions of the editable root object.
    /// </summary>
    public class RootDal : IRootDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified root.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        /// <returns>The requested root.</returns>
        public RootDao Fetch(
            RootCriteria criteria
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
                    throw new DataNotFoundException(DalText.Root_NotFound);

                return new RootDao
                {
                    RootKey = root.RootKey,
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
            RootDao dao
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Check unique root name.
                Root root = ctx.DbContext.Roots
                    .Where(e => e.RootName == dao.RootName)
                    .FirstOrDefault()
                    ;
                if (root != null)
                    throw new DataExistException(DalText.Root_RootNameExists.With(dao.RootName));

                // Create the new root.
                root = new Root
                {
                    RootName = dao.RootName
                };
                ctx.DbContext.Roots.Add(root);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new InsertFailedException(DalText.Root_InsertFailed);

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
            RootDao dao
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified root.
                Root root = ctx.DbContext.Roots
                    .Where(e => e.RootKey == dao.RootKey)
                    .FirstOrDefault()
                    ;
                if (root == null)
                    throw new DataNotFoundException(DalText.Root_NotFound);
                if (root.Timestamp != dao.Timestamp)
                    throw new ConcurrencyException(DalText.Root_Concurrency);

                // Check unique root name.
                if (root.RootName != dao.RootName)
                {
                    int exist = ctx.DbContext.Roots
                    .Where(e => e.RootName == dao.RootName && e.RootKey != root.RootKey)
                    .Count()
                    ;
                    if (exist > 0)
                        throw new DataExistException(DalText.Root_RootNameExists.With(dao.RootName));
                }

                // Update the root.
                root.RootName = dao.RootName;

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.Root_UpdateFailed);

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
            RootCriteria criteria
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
                    throw new DataNotFoundException(DalText.Root_NotFound);

                // Check or delete references
                //int dependents = 0;

                //dependents = ctx.DbContext.Others.Count(e => e.RootKey == criteria.RootKey);
                //if (dependents > 0)
                //    throw new DeleteFailedException(DalText.Root_Delete_Others);

                // Delete the root.
                ctx.DbContext.Roots.Remove(root);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new DeleteFailedException(DalText.Root_DeleteFailed);
            }
        }

        #endregion Delete
    }
}
