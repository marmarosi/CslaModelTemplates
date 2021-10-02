using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.ComplexView;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Dal;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.ComplexView
{
    /// <summary>
    /// Represents a read-only team object.
    /// </summary>
    [Serializable]
    public class TeamView : ReadOnlyModel<TeamView>
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

        public static readonly PropertyInfo<PlayerViews> PlayersProperty = RegisterProperty<PlayerViews>(c => c.Players);
        public PlayerViews Players
        {
            get { return GetProperty(PlayersProperty); }
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
        //        typeof(TeamView),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private TeamView()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets the specified read-only team instance.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only team.</param>
        /// <returns>The requested read-only team instance.</returns>
        public static async Task<TeamView> Get(
            TeamViewParams criteria
            )
        {
            return await DataPortal.FetchAsync<TeamView>(criteria.Decode());
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            TeamViewCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamViewDal dal = dm.GetProvider<ITeamViewDal>();
                TeamViewDao dao = dal.Fetch(criteria);

                TeamKey = dao.TeamKey;
                TeamCode = dao.TeamCode;
                TeamName = dao.TeamName;
                Players = PlayerViews.Get(dao.Players);
            }
        }

        #endregion
    }
}
