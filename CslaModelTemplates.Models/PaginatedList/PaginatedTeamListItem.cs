using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.PaginatedList;
using CslaModelTemplates.CslaExtensions.Models;
using System;

namespace CslaModelTemplates.Models.PaginatedList
{
    /// <summary>
    /// Represents an item in a read-only paginated team collection.
    /// </summary>
    [Serializable]
    public class PaginatedTeamListItem : ReadOnlyModel<PaginatedTeamListItem>
    {
        #region Properties

        public static readonly PropertyInfo<string> TeamIdProperty = RegisterProperty<string>(c => c.TeamId, RelationshipTypes.PrivateField);
        private long? TeamKey = null;
        public string TeamId
        {
            get { return GetProperty(TeamIdProperty, KeyHash.Encode(ID.Team, TeamKey)); }
            private set { TeamKey = KeyHash.Decode(ID.Team, value); }
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
        //        typeof(PaginatedTeamListItem),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private PaginatedTeamListItem()
        { /* require use of factory methods */ }

        internal static PaginatedTeamListItem Get(
            PaginatedTeamListItemDao dao
            )
        {
            return DataPortal.FetchChild<PaginatedTeamListItem>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            PaginatedTeamListItemDao dao
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
