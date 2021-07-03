using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.PaginatedList;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.PaginatedList
{
    /// <summary>
    /// Represents a page of read-only team collection.
    /// </summary>
    [Serializable]
    public class PaginatedTeamListItems : ReadOnlyList<PaginatedTeamListItems, PaginatedTeamListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PaginatedTeamListItems),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private PaginatedTeamListItems()
        { /* require use of factory methods */ }

        internal static PaginatedTeamListItems Get(
            List<PaginatedTeamListItemDao> list
            )
        {
            return DataPortal.FetchChild<PaginatedTeamListItems>(list);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<PaginatedTeamListItemDao> list
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (PaginatedTeamListItemDao dao in list)
                Add(PaginatedTeamListItem.Get(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
