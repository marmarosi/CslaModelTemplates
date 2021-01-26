using Csla;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Contracts.SimpleList;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.SimpleList
{
    /// <summary>
    /// Represents a read-only root collection.
    /// </summary>
    [Serializable]
    public class SimpleRootList : ReadOnlyListBase<SimpleRootList, SimpleRootListItem>
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
        public static SimpleRootList Get(
            SimpleRootListCriteria criteria
            )
        {
            return DataPortal.Fetch<SimpleRootList>(criteria);
        }

        private SimpleRootList()
        { /* require use of factory methods */ }

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
