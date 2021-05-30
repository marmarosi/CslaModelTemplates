using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.ComplexCommand;
using System;

namespace CslaModelTemplates.Models.ComplexCommand
{
    /// <summary>
    /// Represents an item in a read-only count teams collection.
    /// </summary>
    [Serializable]
    public class CountTeamsListItem : ReadOnlyModel<CountTeamsListItem>
    {
        #region Properties

        public static readonly PropertyInfo<int> ItemCountProperty = RegisterProperty<int>(c => c.ItemCount);
        public int ItemCount
        {
            get { return GetProperty(ItemCountProperty); }
            private set { LoadProperty(ItemCountProperty, value); }
        }

        public static readonly PropertyInfo<int> CountOfTeamsProperty = RegisterProperty<int>(c => c.CountOfTeams);
        public int CountOfTeams
        {
            get { return GetProperty(CountOfTeamsProperty); }
            private set { LoadProperty(CountOfTeamsProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.WriteProperty, ItemCountProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountTeamsListItem),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private CountTeamsListItem()
        { /* require use of factory methods */ }

        internal static CountTeamsListItem Get(
            CountTeamsListItemDao dao
            )
        {
            return DataPortal.FetchChild<CountTeamsListItem>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            CountTeamsListItemDao dao
            )
        {
            // Set values from data access object.
            ItemCount = dao.ItemCount;
            CountOfTeams = dao.CountOfTeams;
        }

        #endregion
    }
}
