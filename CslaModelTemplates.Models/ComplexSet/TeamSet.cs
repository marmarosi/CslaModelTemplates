using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.ComplexSet
{
    /// <summary>
    /// Represents an editable team collection.
    /// </summary>
    [Serializable]
    public class TeamSet : EditableList<TeamSet, TeamSetItem, TeamSetItemDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamSet),
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
            List<TeamSetItemDto> list
            )
        {
            await Update(list, "TeamId");
        }

        #endregion

        #region Factory Methods

        private TeamSet()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates a new editable team collection.
        /// </summary>
        /// <returns>The new editable team collection.</returns>
        public static async Task<TeamSet> Create()
        {
            return await DataPortal.CreateAsync<TeamSet>();
        }

        /// <summary>
        /// Gets an editable team collection that match the criteria..
        /// </summary>
        /// <param name="criteria">The criteria of the editable team collection.</param>
        /// <returns>The requested editable team collection.</returns>
        public static async Task<TeamSet> Get(
            TeamSetCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<TeamSet>(criteria);
        }

        /// <summary>
        /// Rebuilds an editable team instance from the data transfer object.
        /// </summary>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable team instance.</returns>
        public static async Task<TeamSet> FromDto(
            TeamSetCriteria criteria,
            List<TeamSetItemDto> list
            )
        {
            TeamSet set = await DataPortal.FetchAsync<TeamSet>(criteria);
            await set.Update(list);
            return set;
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            TeamSetCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamSetDal dal = dm.GetProvider<ITeamSetDal>();
                List<TeamSetItemDao> list = dal.Fetch(criteria);

                // Create items from data access objects.
                foreach (TeamSetItemDao dao in list)
                    Add(TeamSetItem.Get(dao));
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
