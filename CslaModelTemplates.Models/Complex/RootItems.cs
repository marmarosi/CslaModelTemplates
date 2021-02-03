using Csla;
using CslaModelTemplates.Contracts.Complex;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Models.Complex
{
    [Serializable]
    public class RootItems : BusinessListBase<RootItems, RootItem>
    {
        #region Business Methods

        /// <summary>
        /// Gets the data transfer object of the editable root item collection.
        /// </summary>
        /// <returns>The data transfer object of the editable root item collection.</returns>
        internal List<RootItemDto> AsDto()
        {
            List<RootItemDto> list = new List<RootItemDto>();

            foreach (RootItem item in Items)
                list.Add(item.AsDto());

            return list;
        }

        /// <summary>
        /// Rebuilds an editable root item collection from the data transfer objects.
        /// </summary>
        /// <param name="list">The list of data transfer objects.</param>
        /// <returns>The rebuilt editable root item collection.</returns>
        internal void FromDto(
            List<RootItemDto> list
            )
        {
            for (int i = Items.Count-1; i > -1; i--)
            {
                RootItem item = Items[i];
                RootItemDto dto = list.Find(o => o.RootItemKey == item.RootItemKey);
                if (dto == null)
                    RemoveItem(i);
                else
                    item.Update(dto);
            }

            List<RootItemDto> listToAdd = list.Where(item => !item.__Processed).ToList();
            foreach (RootItemDto dto in listToAdd)
            {
                Items.Add(RootItem.FromDto(dto));
            }
        }

        #endregion

        #region Factory Methods

        internal static RootItems NewRootItems()
        {
            return DataPortal.CreateChild<RootItems>();
        }

        private RootItems()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<RootItemDao> items
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // Create items from data access objects.
            foreach (RootItemDao dao in items)
                Add(DataPortal.FetchChild<RootItem>(dao));

            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
