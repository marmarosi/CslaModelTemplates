using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
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
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountRootsList),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private CountRootsList()
        { /* require use of factory methods */ }

        internal static CountRootsList Get(
            List<CountRootsListItemDao> list
            )
        {
            return DataPortal.FetchChild<CountRootsList>(list);
        }

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
