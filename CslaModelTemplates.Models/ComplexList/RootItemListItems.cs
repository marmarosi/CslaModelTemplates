using Csla;
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
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(FoodOrderItemViewList), "Role");
        }

        #endregion

        #region Factory Methods

        internal static RootItemListItems Get(
            List<RootItemListItemDao> list
            )
        {
            return DataPortal.FetchChild<RootItemListItems>(list);
        }

        private RootItemListItems()
        { /* require use of factory methods */ }

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
