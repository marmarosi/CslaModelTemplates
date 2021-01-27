using Csla;
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
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(FoodOrderItemViewList), "Role");
        }

        #endregion

        #region Factory Methods

        internal static RootItemViews Get(
            List<RootItemViewDao> list
            )
        {
            return DataPortal.FetchChild<RootItemViews>(list);
        }

        private RootItemViews()
        { /* require use of factory methods */ }

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
