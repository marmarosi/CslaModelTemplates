using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts.ComplexView;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.ComplexView
{
    /// <summary>
    /// Represents a read-only root item collection.
    /// </summary>
    [Serializable]
    public class RootItemViews : ReadOnlyListBase<RootItemViews, RootItemView>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(RootItemViews),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private RootItemViews()
        { /* require use of factory methods */ }

        internal static RootItemViews Get(
            List<RootItemViewDao> list
            )
        {
            return DataPortal.FetchChild<RootItemViews>(list);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<RootItemViewDao> list
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (RootItemViewDao dao in list)
                Add(RootItemView.Get(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
