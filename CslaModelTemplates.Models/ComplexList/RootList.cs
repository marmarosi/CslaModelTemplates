using Csla;
using CslaModelTemplates.Contracts.ComplexList;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.ComplexList
{
    /// <summary>
    /// Represents a read-only root collection.
    /// </summary>
    [Serializable]
    public class RootList : ReadOnlyListBase<RootList, RootListItem>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // Add authorization rules.
            //AuthorizationRules.AllowGet(typeof(ManagerList), "Role");
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a read-only root collection that match the criteria..
        /// </summary>
        /// <param name="criteria">The criteria of the read-only root collection.</param>
        /// <returns>The requested read-only root collection.</returns>
        public static async Task<RootList> Get(
            RootListCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<RootList>(criteria);
        }

        private RootList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            RootListCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootListDal dal = dm.GetProvider<IRootListDal>();
                List<RootListItemDao> list = dal.Fetch(criteria);

                // Create items from data access objects.
                foreach (RootListItemDao dao in list)
                    Add(RootListItem.Get(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
