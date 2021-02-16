using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts.ComplexList;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.ComplexList
{
    /// <summary>
    /// Represents a read-only root item collection.
    /// </summary>
    [Serializable]
    public class RootItemListItems : ReadOnlyListBase<RootItemListItems, RootItemListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(RootItemListItems),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private RootItemListItems()
        { /* require use of factory methods */ }

        internal static RootItemListItems Get(
            List<RootItemListItemDao> list
            )
        {
            return DataPortal.FetchChild<RootItemListItems>(list);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<RootItemListItemDao> list
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (RootItemListItemDao dao in list)
                Add(RootItemListItem.Get(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
