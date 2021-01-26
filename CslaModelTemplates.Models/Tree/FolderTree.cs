using Csla;
using CslaModelTemplates.Contracts.Tree;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.Tree
{
    /// <summary>
    /// Represents a read-only folder tree.
    /// </summary>
    [Serializable]
    public class FolderTree : ReadOnlyListBase<FolderTree, FolderNode>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // Add authorization rules.
            //AuthorizationRules.AllowGet(typeof(FolderTree), "Role");
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified read-only folder tree.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only folder tree.</param>
        /// <returns>The requested read-only folder tree.</returns>
        public static FolderTree Get(FolderTreeCriteria criteria)
        {
            return DataPortal.Fetch<FolderTree>(criteria);
        }

        private FolderTree()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(FolderTreeCriteria criteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IFolderTreeDal dal = dm.GetProvider<IFolderTreeDal>();
                List<FolderNodeDao> tree = dal.Fetch(criteria);

                foreach (FolderNodeDao dao in tree)
                    Add(FolderNode.Fetch(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
