using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SimpleSet
{
    /// <summary>
    /// Represents an editable team collection.
    /// </summary>
    [Serializable]
    public class SimpleTeamSet : EditableList<SimpleTeamSet, SimpleTeamSetItem, SimpleTeamSetItemDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SimpleTeamSet),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Rebuilds an editable team collection from the data transfer objects.
        /// </summary>
        /// <param name="list">The list of data transfer objects.</param>
        internal async Task Update(
            List<SimpleTeamSetItemDto> list
            )
        {
            await Update(list, "TeamId");
        }

        #endregion

        #region Factory Methods

        private SimpleTeamSet()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates a new editable team collection.
        /// </summary>
        /// <returns>The new editable team collection.</returns>
        public static async Task<SimpleTeamSet> Create()
        {
            return await DataPortal.CreateAsync<SimpleTeamSet>();
        }

        /// <summary>
        /// Gets an editable team collection that match the criteria..
        /// </summary>
        /// <param name="criteria">The criteria of the editable team collection.</param>
        /// <returns>The requested editable team collection.</returns>
        public static async Task<SimpleTeamSet> Get(
            SimpleTeamSetCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<SimpleTeamSet>(criteria);
        }

        /// <summary>
        /// Rebuilds an editable team instance from the data transfer object.
        /// </summary>
        /// <param name="criteria">The criteria of the editable team collection.</param>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable team instance.</returns>
        public static async Task<SimpleTeamSet> FromDto(
            SimpleTeamSetCriteria criteria,
            List<SimpleTeamSetItemDto> list
            )
        {
            SimpleTeamSet set = await DataPortal.FetchAsync<SimpleTeamSet>(criteria);
            await set.Update(list);
            return set;
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            SimpleTeamSetCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleTeamSetDal dal = dm.GetProvider<ISimpleTeamSetDal>();
                List<SimpleTeamSetItemDao> list = dal.Fetch(criteria);

                // Create items from data access objects.
                foreach (SimpleTeamSetItemDao dao in list)
                    Add(SimpleTeamSetItem.Get(dao));
            }
            RaiseListChangedEvents = rlce;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                base.Child_Update();
            }
        }

        #endregion
    }
}
