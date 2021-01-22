using Csla;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionWithKey;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.SelectionWithKey
{
    /// <summary>
    /// Represents a read-only root choice collection.
    /// </summary>
    [Serializable]
    public class RootKeyChoice : ReadOnlyListBase<RootKeyChoice, KeyNameOption>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // Add authorization rules.
            //AuthorizationRules.AllowGet(typeof(RootList), "Role");
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a choice of root options that match the criteria.
        /// </summary>
        /// <param name="criteria">The criteria root choice.</param>
        /// <returns>The requested root choice instance.</returns>
        public static RootKeyChoice Get(
            RootKeyChoiceCriteria criteria
            )
        {
            return DataPortal.Fetch<RootKeyChoice>(criteria);
        }

        private RootKeyChoice()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            RootKeyChoiceCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootKeyChoiceDal dal = dm.GetProvider<IRootKeyChoiceDal>();
                List<KeyNameOptionDao> choice = dal.Fetch(criteria);

                foreach (var dao in choice)
                    Add(KeyNameOption.Get(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
