using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.ComplexCommand;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.ComplexCommand
{
    /// <summary>
    /// Represents a read-only count teams collection.
    /// </summary>
    [Serializable]
    public class CountTeamsList : ReadOnlyList<CountTeamsList, CountTeamsListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountTeamsList),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private CountTeamsList()
        { /* require use of factory methods */ }

        internal static CountTeamsList Get(
            List<CountTeamsListItemDao> list
            )
        {
            return DataPortal.FetchChild<CountTeamsList>(list);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<CountTeamsListItemDao> list
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Create items from data access objects.
            foreach (CountTeamsListItemDao dao in list)
                Add(CountTeamsListItem.Get(dao));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
