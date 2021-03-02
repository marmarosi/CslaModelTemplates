using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.Tree;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.Tree
{
    /// <summary>
    /// Represents a read-only folder tree.
    /// </summary>
    [Serializable]
    public class FolderTree : ReadOnlyList<FolderTree, FolderNode>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(FolderTree),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private FolderTree()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets the specified read-only folder tree.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only folder tree.</param>
        /// <returns>The requested read-only folder tree.</returns>
        public static async Task<FolderTree> Get(FolderTreeCriteria criteria)
        {
            return await DataPortal.FetchAsync<FolderTree>(criteria);
        }

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
