using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.Tree;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.Tree
{
    /// <summary>
    /// Represents a read-only folder node collection.
    /// </summary>
    [Serializable]
    public class FolderNodeList : ReadOnlyList<FolderNodeList, FolderNode>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(FolderNodeList),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private FolderNodeList()
        { /* require use of factory methods */ }

        internal static FolderNodeList Get(List<FolderNodeDao> list)
        {
            return DataPortal.FetchChild<FolderNodeList>(list);
        }

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
