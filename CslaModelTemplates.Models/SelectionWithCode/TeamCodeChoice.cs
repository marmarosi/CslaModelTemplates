using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionWithCode;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SelectionWithCode
{
    /// <summary>
    /// Represents a read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamCodeChoice : ReadOnlyListBase<TeamCodeChoice, CodeNameOption>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamCodeChoice),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private TeamCodeChoice()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets a choice of team options that match the criteria.
        /// </summary>
        /// <param name="criteria">The criteria team choice.</param>
        /// <returns>The requested team choice instance.</returns>
        public static async Task<TeamCodeChoice> Get(
            TeamCodeChoiceCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<TeamCodeChoice>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            TeamCodeChoiceCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamCodeChoiceDal dal = dm.GetProvider<ITeamCodeChoiceDal>();
                List<CodeNameOptionDao> choice = dal.Fetch(criteria);

                foreach (CodeNameOptionDao dao in choice)
                    Add(CodeNameOption.Get(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
