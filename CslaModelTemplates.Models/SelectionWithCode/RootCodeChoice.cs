using Csla;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionWithCode;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.SelectionWithCode
{
    /// <summary>
    /// Represents a read-only root choice collection.
    /// </summary>
    [Serializable]
    public class RootCodeChoice : ReadOnlyListBase<RootCodeChoice, CodeNameOption>
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
        public static RootCodeChoice Get(
            RootCodeChoiceCriteria criteria
            )
        {
            return DataPortal.Fetch<RootCodeChoice>(criteria);
        }

        private RootCodeChoice()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            RootCodeChoiceCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootCodeChoiceDal dal = dm.GetProvider<IRootCodeChoiceDal>();
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
