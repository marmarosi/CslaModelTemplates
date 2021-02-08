using Csla;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.Complex;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.Complex
{
    /// <summary>
    /// Represents an editable root item collection.
    /// </summary>
    [Serializable]
    public class RootItems : EditableList<RootItems, RootItem>
    {
        #region Business Methods

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
                {
                    item.Update(dto);
                    list.Remove(dto);
                }
            }
            foreach (RootItemDto dto in list)
                Items.Add(RootItem.FromDto(dto));
        }

        #endregion

        #region Factory Methods

        internal static RootItems Create()
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
