using Csla;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.ComplexSet;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.ComplexSet
{
    /// <summary>
    /// Represents an editable root item collection.
    /// </summary>
    [Serializable]
    public class RootSetRootItems : EditableList<RootSetRootItems, RootSetRootItem>
    {
        #region Business Methods

        /// <summary>
        /// Rebuilds an editable root item collection from the data transfer objects.
        /// </summary>
        /// <param name="list">The list of data transfer objects.</param>
        /// <returns>The rebuilt editable root item collection.</returns>
        internal void Update(
            List<RootSetRootItemDto> list
            )
        {
            for (int i = Items.Count-1; i >= 0; i--)
            {
                RootSetRootItem item = Items[i];
                RootSetRootItemDto dto = list.Find(o => o.RootItemKey == item.RootItemKey);
                if (dto == null)
                    RemoveItem(i);
                else
                {
                    item.Update(dto);
                    list.Remove(dto);
                }
            }
            foreach (RootSetRootItemDto dto in list)
                Items.Add(RootSetRootItem.FromDto(dto));
        }

        #endregion

        #region Factory Methods

        internal static RootSetRootItems Create()
        {
            return DataPortal.CreateChild<RootSetRootItems>();
        }

        private RootSetRootItems()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<RootSetRootItemDao> items
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // Create items from data access objects.
            foreach (RootSetRootItemDao dao in items)
                Add(DataPortal.FetchChild<RootSetRootItem>(dao));

            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
