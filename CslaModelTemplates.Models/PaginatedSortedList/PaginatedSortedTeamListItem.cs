using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.PaginatedSortedList;
using System;

namespace CslaModelTemplates.Models.PaginatedSortedList
{
    /// <summary>
    /// Represents an item in a read-only paginated sorted team collection.
    /// </summary>
    [Serializable]
    public class PaginatedSortedTeamListItem : ReadOnlyModel<PaginatedSortedTeamListItem>
    {
        #region Properties

        public static readonly PropertyInfo<long?> TeamKeyProperty = RegisterProperty<long?>(c => c.TeamKey);
        public long? TeamKey
        {
            get { return GetProperty(TeamKeyProperty); }
            private set { LoadProperty(TeamKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> TeamCodeProperty = RegisterProperty<string>(c => c.TeamCode);
        public string TeamCode
        {
            get { return GetProperty(TeamCodeProperty); }
            private set { LoadProperty(TeamCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> TeamNameProperty = RegisterProperty<string>(c => c.TeamName);
        public string TeamName
        {
            get { return GetProperty(TeamNameProperty); }
            private set { LoadProperty(TeamNameProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty, TeamNameProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PaginatedSortedTeamListItem),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private PaginatedSortedTeamListItem()
        { /* require use of factory methods */ }

        internal static PaginatedSortedTeamListItem Get(
            PaginatedSortedTeamListItemDao dao
            )
        {
            return DataPortal.FetchChild<PaginatedSortedTeamListItem>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            PaginatedSortedTeamListItemDao dao
            )
        {
            // Set values from data access object.
            TeamKey = dao.TeamKey;
            TeamCode = dao.TeamCode;
            TeamName = dao.TeamName;
        }

        #endregion
    }
}
