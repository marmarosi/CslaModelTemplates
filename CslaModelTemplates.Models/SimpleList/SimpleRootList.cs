using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts.SimpleList;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SimpleList
{
    /// <summary>
    /// Represents a read-only root collection.
    /// </summary>
    [Serializable]
    public class SimpleRootList : ReadOnlyListBase<SimpleRootList, SimpleRootListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SimpleRootList),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private SimpleRootList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets a read-only root collection that match the criteria..
        /// </summary>
        /// <param name="criteria">The criteria of the read-only root collection.</param>
        /// <returns>The requested read-only root collection.</returns>
        public static async Task<SimpleRootList> Get(
            SimpleRootListCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<SimpleRootList>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            SimpleRootListCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleRootListDal dal = dm.GetProvider<ISimpleRootListDal>();
                List<SimpleRootListItemDao> list = dal.Fetch(criteria);

                // Create items from data access objects.
                foreach (SimpleRootListItemDao dao in list)
                    Add(SimpleRootListItem.Get(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
