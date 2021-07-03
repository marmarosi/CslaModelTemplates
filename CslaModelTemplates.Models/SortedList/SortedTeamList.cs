using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.SortedList;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SortedList
{
    /// <summary>
    /// Represents a read-only sorted team collection.
    /// </summary>
    [Serializable]
    public class SortedTeamList : ReadOnlyList<SortedTeamList, SortedTeamListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SortedTeamList),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private SortedTeamList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets a read-only sorted team collection that match the criteria..
        /// </summary>
        /// <param name="criteria">The criteria of the read-only team collection.</param>
        /// <returns>The requested read-only sorted team collection.</returns>
        public static async Task<SortedTeamList> Get(
            SortedTeamListCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<SortedTeamList>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            SortedTeamListCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISortedTeamListDal dal = dm.GetProvider<ISortedTeamListDal>();
                List<SortedTeamListItemDao> list = dal.Fetch(criteria);

                // Create items from data access objects.
                foreach (SortedTeamListItemDao dao in list)
                    Add(SortedTeamListItem.Get(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
