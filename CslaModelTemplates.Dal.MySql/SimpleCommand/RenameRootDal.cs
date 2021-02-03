using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.SimpleCommand;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.MySql.Entities;
using CslaModelTemplates.Resources;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.SimpleCommand
{
    /// <summary>
    /// Implements the data access functions of the rename root command.
    /// </summary>
    public class RenameRootDal : IRenameRootDal
    {
        private string COMMAND = typeof(RenameRootDal).Name.CutEnd(3);

        #region Execute

        /// <summary>
        /// Sets the new name of the specified root.
        /// </summary>
        /// <param name="dao">The data of the command.</param>
        public void Execute(
            RenameRootDao dao
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
                    throw new DataNotFoundException(DalText.RenameRoot_NotFound);

                // Update the root.
                root.RootName = dao.RootName;

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.RenameRoot_RenameFailed);
            }
        }

        #endregion
    }
}
