using Csla;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionByCode;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.SelectionByCode
{
    /// <summary>
    /// Represents a read-only root choice collection.
    /// </summary>
    [Serializable]
    public class RootChoice : ReadOnlyListBase<RootChoice, CodeNameOption>
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
        public static RootChoice Get(
            RootChoiceCriteria criteria
            )
        {
            return DataPortal.Fetch<RootChoice>(criteria);
        }

        private RootChoice()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            RootChoiceCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootChoiceDal dal = dm.GetProvider<IRootChoiceDal>();
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
