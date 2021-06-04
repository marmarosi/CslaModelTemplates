using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.JunctionView;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.JunctionView
{
    /// <summary>
    /// Represents a read-only group-person collection.
    /// </summary>
    [Serializable]
    public class GroupPersonViews : ReadOnlyList<GroupPersonViews, GroupPersonView>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(GroupPersonViews),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private GroupPersonViews()
        { /* require use of factory methods */ }

        internal static GroupPersonViews Get(
            List<GroupPersonViewDao> list
            )
        {
            return DataPortal.FetchChild<GroupPersonViews>(list);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<GroupPersonViewDao> list
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (GroupPersonViewDao dao in list)
                Add(GroupPersonView.Get(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
