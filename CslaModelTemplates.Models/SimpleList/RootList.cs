using Csla;
using CslaModelTemplates.Common.Dal;
using CslaModelTemplates.Contracts.SimpleList;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.SimpleList
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
        /// Gets a list of root items that match the criteria.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only root collection.</param>
        /// <returns>The list of the root items.</returns>
        public static RootList GetList(
            RootListCriteria criteria
            )
        {
            return DataPortal.Fetch<RootList>(criteria);
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

            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootListDal dal = dm.GetProvider<IRootListDal>();
                List<RootListItemDao> list = dal.Get(criteria);

                foreach (RootListItemDao dao in list)
                    Add(RootListItem.Get(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
