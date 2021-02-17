using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.ComplexList;
using System;

namespace CslaModelTemplates.Models.ComplexList
{
    /// <summary>
    /// Represents an item in a read-only team collection.
    /// </summary>
    [Serializable]
    public class TeamListItem : ReadOnlyModel<TeamListItem>
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

        public static readonly PropertyInfo<PlayerListItems> PlayersProperty = RegisterProperty<PlayerListItems>(c => c.Players);
        public PlayerListItems Players
        {
            get { return ReadProperty(PlayersProperty); }
            private set { LoadProperty(PlayersProperty, value); }
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
        //        typeof(TeamListItem),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private TeamListItem()
        { /* require use of factory methods */ }

        internal static TeamListItem Get(
            TeamListItemDao dao
            )
        {
            return DataPortal.FetchChild<TeamListItem>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            TeamListItemDao dao
            )
        {
            // Set values from data access object.
            TeamKey = dao.TeamKey;
            TeamCode = dao.TeamCode;
            TeamName = dao.TeamName;
            Players = PlayerListItems.Get(dao.Players);
        }

        #endregion
    }
}
