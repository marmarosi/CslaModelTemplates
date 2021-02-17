using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts.ComplexList;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.ComplexList
{
    /// <summary>
    /// Represents a read-only team collection.
    /// </summary>
    [Serializable]
    public class TeamList : ReadOnlyListBase<TeamList, TeamListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamList),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private TeamList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets a read-only team collection that match the criteria..
        /// </summary>
        /// <param name="criteria">The criteria of the read-only team collection.</param>
        /// <returns>The requested read-only team collection.</returns>
        public static async Task<TeamList> Get(
            TeamListCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<TeamList>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            TeamListCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamListDal dal = dm.GetProvider<ITeamListDal>();
                List<TeamListItemDao> list = dal.Fetch(criteria);

                // Create items from data access objects.
                foreach (TeamListItemDao dao in list)
                    Add(TeamListItem.Get(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
