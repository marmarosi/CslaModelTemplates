using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.PaginatedSortedList;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.PaginatedSortedList
{
    /// <summary>
    /// Represents a page of read-only sorted team collection.
    /// </summary>
    [Serializable]
    public class PaginatedSortedTeamListItems : ReadOnlyList<PaginatedSortedTeamListItems, PaginatedSortedTeamListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PaginatedSortedTeamListItems),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private PaginatedSortedTeamListItems()
        { /* require use of factory methods */ }

        internal static PaginatedSortedTeamListItems Get(
            List<PaginatedSortedTeamListItemDao> list
            )
        {
            return DataPortal.FetchChild<PaginatedSortedTeamListItems>(list);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<PaginatedSortedTeamListItemDao> list
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (PaginatedSortedTeamListItemDao dao in list)
                Add(PaginatedSortedTeamListItem.Get(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
