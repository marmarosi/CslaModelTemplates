using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SimpleSet
{
    /// <summary>
    /// Represents an editable root collection.
    /// </summary>
    [Serializable]
    public class SimpleRootSet : EditableList<SimpleRootSet, SimpleRootSetItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SimpleRootSet),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private SimpleRootSet()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates a new editable root collection.
        /// </summary>
        /// <returns>The new editable root collection.</returns>
        public static async Task<SimpleRootSet> Create()
        {
            return await DataPortal.CreateAsync<SimpleRootSet>();
        }

        /// <summary>
        /// Gets an editable root collection that match the criteria..
        /// </summary>
        /// <param name="criteria">The criteria of the editable root collection.</param>
        /// <returns>The requested editable root collection.</returns>
        public static async Task<SimpleRootSet> Get(
            SimpleRootSetCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<SimpleRootSet>(criteria);
        }

        /// <summary>
        /// Rebuilds an editable root instance from the data transfer object.
        /// </summary>
        /// <param name="criteria">The criteria of the editable root collection.</param>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable root instance.</returns>
        public static async Task<SimpleRootSet> FromDto(
            SimpleRootSetCriteria criteria,
            List<SimpleRootSetItemDto> list
            )
        {
            SimpleRootSet set = await DataPortal.FetchAsync<SimpleRootSet>(criteria);

            foreach (SimpleRootSetItem item in set.Items)
            {
                SimpleRootSetItemDto dto = list.Find(o => o.RootKey == item.RootKey);
                if (dto == null)
                    item.Delete();
                else
                {
                    item.Update(dto);
                    list.Remove(dto);
                }
            }
            foreach (SimpleRootSetItemDto dto in list)
                set.Add(await SimpleRootSetItem.Create(dto));

            return set;
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            SimpleRootSetCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleRootSetDal dal = dm.GetProvider<ISimpleRootSetDal>();
                List<SimpleRootSetItemDao> list = dal.Fetch(criteria);

                // Create items from data access objects.
                foreach (SimpleRootSetItemDao dao in list)
                    Add(SimpleRootSetItem.Get(dao));
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
