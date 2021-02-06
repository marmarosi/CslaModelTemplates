using Csla;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.SimpleSet
{
    /// <summary>
    /// Represents an editable root collection.
    /// </summary>
    [Serializable]
    public class SimpleRootSet : EditableList<SimpleRootSet, SimpleRootSetItem>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(SimpleRootSet), "Role");
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Creates a new editable root collection.
        /// </summary>
        /// <returns>The new editable root collection.</returns>
        public static SimpleRootSet Create()
        {
            return DataPortal.Create<SimpleRootSet>();
        }

        /// <summary>
        /// Gets an editable root collection that match the criteria..
        /// </summary>
        /// <param name="criteria">The criteria of the editable root collection.</param>
        /// <returns>The requested editable root collection.</returns>
        public static SimpleRootSet Get(
            SimpleRootSetCriteria criteria
            )
        {
            return DataPortal.Fetch<SimpleRootSet>(criteria);
        }

        private SimpleRootSet()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Rebuilds an editable root instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable root instance.</returns>
        public static SimpleRootSet FromDto(
            List<SimpleRootSetItemDto> list
            )
        {
            SimpleRootSet set = DataPortal.Create<SimpleRootSet>();

            foreach (SimpleRootSetItemDto dto in list)
                set.Add(SimpleRootSetItem.FromDto(dto));

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
