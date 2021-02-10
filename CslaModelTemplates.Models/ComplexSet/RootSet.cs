using Csla;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.ComplexSet
{
    /// <summary>
    /// Represents an editable root collection.
    /// </summary>
    [Serializable]
    public class RootSet : EditableList<RootSet, RootSetItem>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(RootSet), "Role");
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Creates a new editable root collection.
        /// </summary>
        /// <returns>The new editable root collection.</returns>
        public static async Task<RootSet> Create()
        {
            return await DataPortal.CreateAsync<RootSet>();
        }

        /// <summary>
        /// Gets an editable root collection that match the criteria..
        /// </summary>
        /// <param name="criteria">The criteria of the editable root collection.</param>
        /// <returns>The requested editable root collection.</returns>
        public static async Task<RootSet> Get(
            RootSetCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<RootSet>(criteria);
        }

        private RootSet()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Rebuilds an editable root instance from the data transfer object.
        /// </summary>
        /// <param name="criteria">The criteria of the root set.</param>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable root instance.</returns>
        public static async Task<RootSet> FromDto(
            RootSetCriteria criteria,
            List<RootSetItemDto> list
            )
        {
            RootSet set = await DataPortal.FetchAsync<RootSet>(criteria);

            foreach (RootSetItem item in set.Items)
            {
                RootSetItemDto dto = list.Find(o => o.RootKey == item.RootKey);
                if (dto == null)
                    item.Delete();
                else
                {
                    item.Update(dto);
                    list.Remove(dto);
                }
            }
            foreach (RootSetItemDto dto in list)
                set.Add(await RootSetItem.Create(dto));

            return set;
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            RootSetCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootSetDal dal = dm.GetProvider<IRootSetDal>();
                List<RootSetItemDao> list = dal.Fetch(criteria);

                // Create items from data access objects.
                foreach (RootSetItemDao dao in list)
                    Add(RootSetItem.Get(dao));
            }
            RaiseListChangedEvents = rlce;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            //// Force update root timestamps when their items changed only.
            //foreach (RootSetItem item in Items)
            //    if (item.IsDirty && !item.IsSelfDirty)
            //        item.Timestamp = DateTime.Now;

            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                base.Child_Update();
            }
        }

        #endregion
    }
}
