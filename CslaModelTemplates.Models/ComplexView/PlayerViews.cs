using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.ComplexView;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.ComplexView
{
    /// <summary>
    /// Represents a read-only player collection.
    /// </summary>
    [Serializable]
    public class PlayerViews : ReadOnlyList<PlayerViews, PlayerView>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PlayerViews),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private PlayerViews()
        { /* require use of factory methods */ }

        internal static PlayerViews Get(
            List<PlayerViewDao> list
            )
        {
            return DataPortal.FetchChild<PlayerViews>(list);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<PlayerViewDao> list
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (PlayerViewDao dao in list)
                Add(PlayerView.Get(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
