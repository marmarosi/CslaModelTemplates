using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.SelectionWithId;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Dal.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SelectionWithId
{
    /// <summary>
    /// Represents a read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamIdChoice : ReadOnlyList<TeamIdChoice, IdNameOption>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamIdChoice),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private TeamIdChoice()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets a choice of team options that match the criteria.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The requested team choice instance.</returns>
        public static async Task<TeamIdChoice> Get(
            TeamIdChoiceCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<TeamIdChoice>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            TeamIdChoiceCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamIdChoiceDal dal = dm.GetProvider<ITeamIdChoiceDal>();
                List<IdNameOptionDao> choice = dal.Fetch(criteria);

                foreach (IdNameOptionDao dao in choice)
                    Add(IdNameOption.Get(dao, ID.Team));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
