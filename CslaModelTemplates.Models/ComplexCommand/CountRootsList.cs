using Csla;
using CslaModelTemplates.Contracts.ComplexCommand;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.Command
{
    /// <summary>
    /// Represents a read-only root item collection.
    /// </summary>
    [Serializable]
    public class CountRootsList : ReadOnlyListBase<CountRootsList, CountRootsListItem>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(FoodOrderItemViewList), "Role");
        }

        #endregion

        #region Factory Methods

        internal static CountRootsList Get(
            List<CountRootsListItemDao> list
            )
        {
            return DataPortal.FetchChild<CountRootsList>(list);
        }

        private CountRootsList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<CountRootsListItemDao> list
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (CountRootsListItemDao dao in list)
                Add(CountRootsListItem.Get(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
