using CslaModelTemplates.Contracts.TreeSelection;
using CslaModelTemplates.Dal.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.Oracle.TreeSelection
{
    /// <summary>
    /// Implements the data access functions of the read-only tree choice collection.
    /// </summary>
    public class RootFolderChoiceDal : OracleDal, IRootFolderChoiceDal
    {
        #region Fetch

        /// <summary>
        /// Gets the choice of the trees.
        /// </summary>
        /// <param name="criteria">The criteria of the tree choice.</param>
        /// <returns>The data transfer object of the requested tree choice.</returns>
        public List<IdNameOptionDao> Fetch(
            RootFolderChoiceCriteria criteria
            )
        {
            List<IdNameOptionDao> choice = DbContext.Folders
                .Where(e => e.ParentKey == null)
                .Select(e => new IdNameOptionDao
                {
                    Key = e.FolderKey,
                    Name = e.FolderName
                })
                .OrderBy(o => o.Name)
                .AsNoTracking()
                .ToList();

            return choice;
        }

        #endregion GetChoice
    }
}
