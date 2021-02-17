using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SimpleView;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SimpleView
{
    /// <summary>
    /// Represents a read-only team object.
    /// </summary>
    [Serializable]
    public class SimpleTeamView : ReadOnlyModel<SimpleTeamView>
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
        //        typeof(SimpleTeamView),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private SimpleTeamView()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets the specified read-only team instance.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only team.</param>
        /// <returns>The requested read-only team instance.</returns>
        public static async Task<SimpleTeamView> Get(
            SimpleTeamViewCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<SimpleTeamView>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            SimpleTeamViewCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleTeamViewDal dal = dm.GetProvider<ISimpleTeamViewDal>();
                SimpleTeamViewDao dao = dal.Fetch(criteria);

                TeamKey = dao.TeamKey;
                TeamCode = dao.TeamCode;
                TeamName = dao.TeamName;
            }
        }

        #endregion
    }
}
