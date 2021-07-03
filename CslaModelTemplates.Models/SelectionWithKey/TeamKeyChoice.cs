using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.SelectionWithKey;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CslaModelTemplates.Contracts;

namespace CslaModelTemplates.Models.SelectionWithKey
{
    /// <summary>
    /// Represents a read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamKeyChoice : ReadOnlyList<TeamKeyChoice, KeyNameOption>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamKeyChoice),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private TeamKeyChoice()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets a choice of team options that match the criteria.
        /// </summary>
        /// <param name="criteria">The criteria team choice.</param>
        /// <returns>The requested team choice instance.</returns>
        public static async Task<TeamKeyChoice> Get(
            TeamKeyChoiceCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<TeamKeyChoice>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            TeamKeyChoiceCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamKeyChoiceDal dal = dm.GetProvider<ITeamKeyChoiceDal>();
                List<KeyNameOptionDao> choice = dal.Fetch(criteria);

                foreach (KeyNameOptionDao dao in choice)
                    Add(KeyNameOption.Get(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
