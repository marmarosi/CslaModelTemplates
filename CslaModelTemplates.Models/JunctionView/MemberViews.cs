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
    /// Represents a read-only player collection.
    /// </summary>
    [Serializable]
    public class MemberViews : ReadOnlyList<MemberViews, MemberView>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(MemberViews),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private MemberViews()
        { /* require use of factory methods */ }

        internal static MemberViews Get(
            List<MemberViewDao> list
            )
        {
            return DataPortal.FetchChild<MemberViews>(list);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<MemberViewDao> list
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (MemberViewDao dao in list)
                Add(MemberView.Get(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
