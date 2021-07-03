using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.ComplexList;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.ComplexList
{
    /// <summary>
    /// Represents a read-only player collection.
    /// </summary>
    [Serializable]
    public class PlayerListItems : ReadOnlyList<PlayerListItems, PlayerListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PlayerListItems),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private PlayerListItems()
        { /* require use of factory methods */ }

        internal static PlayerListItems Get(
            List<PlayerListItemDao> list
            )
        {
            return DataPortal.FetchChild<PlayerListItems>(list);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<PlayerListItemDao> list
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (PlayerListItemDao dao in list)
                Add(PlayerListItem.Get(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
