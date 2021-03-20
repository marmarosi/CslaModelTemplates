using CslaModelTemplates.Contracts.Tree;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.Tree
{
    /// <summary>
    /// Implements the data access functions of the read-only folder tree.
    /// </summary>
    public class FolderTreeDal : MySqlDal, IFolderTreeDal
    {
        #region Fetch

        private List<FolderNodeDao> AllFolders { get; set; }

        /// <summary>
        /// Gets the specified folder tree.
        /// </summary>
        /// <param name="criteria">The criteria of the folder tree.</param>
        /// <returns>The requested folder tree.</returns>
        public List<FolderNodeDao> Fetch(
            FolderTreeCriteria criteria
            )
        {
            List<FolderNodeDao> tree = new List<FolderNodeDao>(); ;

            // Get all subfolders of the root foolder.
            AllFolders = DbContext.Folders
                .Where(e =>
                    e.RootKey == criteria.RootKey
                )
                .Select(e => new FolderNodeDao
                {
                    FolderKey = e.FolderKey,
                    ParentKey = e.ParentKey,
                    FolderOrder = e.FolderOrder,
                    FolderName = e.FolderName
                })
                .AsNoTracking()
                .ToList();

            // Populate the tree.
            PopulateLevel(1, null, tree);

            // Return the result.
            return tree;
        }

        private void PopulateLevel(
            int level,
            long? parentKey,
            List<FolderNodeDao> parentChildren
            )
        {
            // Get the folders of the level.
            List<FolderNodeDao> folders = AllFolders
                .Where(o => o.ParentKey == parentKey)
                .OrderBy(o => o.FolderOrder)
                .ToList();

            foreach (FolderNodeDao folder in folders)
            {
                // Create folder node.
                FolderNodeDao folderNode = new FolderNodeDao
                {
                    FolderKey = folder.FolderKey,
                    ParentKey = folder.ParentKey,
                    FolderOrder = folder.FolderOrder,
                    FolderName = folder.FolderName,
                    Level = level,
                    Children = new List<FolderNodeDao>()
                };

                // Add folder to the parent's children.
                parentChildren.Add(folderNode);

                // Get the subfolders of this folder.
                PopulateLevel(
                    level + 1,
                    folder.FolderKey,
                    folderNode.Children
                    );
            }
        }

        #endregion GetList
    }
}
