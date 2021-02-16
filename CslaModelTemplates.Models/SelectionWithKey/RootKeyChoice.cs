using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionWithKey;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SelectionWithKey
{
    /// <summary>
    /// Represents a read-only root choice collection.
    /// </summary>
    [Serializable]
    public class RootKeyChoice : ReadOnlyListBase<RootKeyChoice, KeyNameOption>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(RootKeyChoice),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private RootKeyChoice()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets a choice of root options that match the criteria.
        /// </summary>
        /// <param name="criteria">The criteria root choice.</param>
        /// <returns>The requested root choice instance.</returns>
        public static async Task<RootKeyChoice> Get(
            RootKeyChoiceCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<RootKeyChoice>(criteria);
        }

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

                foreach (KeyNameOptionDao dao in choice)
                    Add(KeyNameOption.Get(dao));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
