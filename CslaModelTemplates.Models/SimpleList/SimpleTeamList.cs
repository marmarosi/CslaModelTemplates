using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SimpleList;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SimpleList
{
    /// <summary>
    /// Represents a read-only team collection.
    /// </summary>
    [Serializable]
    public class SortedTeamList : ReadOnlyList<SortedTeamList, SortedTeamListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SimpleTeamList),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private SortedTeamList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets a read-only team collection that match the criteria..
        /// </summary>
        /// <param name="criteria">The criteria of the read-only team collection.</param>
        /// <returns>The requested read-only team collection.</returns>
        public static async Task<SortedTeamList> Get(
            SimpleTeamListCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<SortedTeamList>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            SimpleTeamListCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleTeamListDal dal = dm.GetProvider<ISimpleTeamListDal>();
                List<SimpleTeamListItemDao> list = dal.Fetch(criteria);

                // Create items from data access objects.
                foreach (SimpleTeamListItemDao dao in list)
                    Add(SortedTeamListItem.Get(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
