using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionWithCode;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SelectionWithCode
{
    /// <summary>
    /// Represents a read-only root choice collection.
    /// </summary>
    [Serializable]
    public class RootCodeChoice : ReadOnlyListBase<RootCodeChoice, CodeNameOption>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(RootCodeChoice),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private RootCodeChoice()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets a choice of root options that match the criteria.
        /// </summary>
        /// <param name="criteria">The criteria root choice.</param>
        /// <returns>The requested root choice instance.</returns>
        public static async Task<RootCodeChoice> Get(
            RootCodeChoiceCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<RootCodeChoice>(criteria);
        }

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
