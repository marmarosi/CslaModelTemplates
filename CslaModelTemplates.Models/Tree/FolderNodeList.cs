using Csla;
using CslaModelTemplates.Contracts.Tree;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.Tree
{
    /// <summary>
    /// Represents a read-only folder node collection.
    /// </summary>
    [Serializable]
    public class FolderNodeList : ReadOnlyListBase<FolderNodeList, FolderNode>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(FolderNodeList), "Role");
        }

        #endregion

        #region Factory Methods

        internal static FolderNodeList Get(List<FolderNodeDao> list)
        {
            return DataPortal.FetchChild<FolderNodeList>(list);
        }

        private FolderNodeList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(List<FolderNodeDao> list)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (FolderNodeDao dao in list)
                Add(FolderNode.Fetch(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
