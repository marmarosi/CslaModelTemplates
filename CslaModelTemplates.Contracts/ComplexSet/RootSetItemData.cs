using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the editable root item data.
    /// </summary>
    public class RootSetItemData
    {
        public long? RootKey;
        public string RootCode;
        public string RootName;
        public DateTime? Timestamp;
    }

    /// <summary>
    /// Defines the data access object of the editable root item object.
    /// </summary>
    public class RootSetItemDao : RootSetItemData
    {
        public List<RootSetRootItemDao> Items;
    }

    /// <summary>
    /// Defines the data transfer object of the editable root item object.
    /// </summary>
    public class RootSetItemDto : RootSetItemData
    {
        public List<RootSetRootItemDto> Items;

        public RootSetItemDao ToDao()
        {
            return new RootSetItemDao
            {
                RootKey = RootKey,
                RootCode = RootCode,
                RootName = RootName,
                Items = ItemsToDao(),
                Timestamp = Timestamp
            };
        }

        protected List<RootSetRootItemDao> ItemsToDao()
        {
            List<RootSetRootItemDao> list = new List<RootSetRootItemDao>();

            foreach (RootSetRootItemDto item in Items)
                list.Add(item.ToDao());

            return list;
        }
    }
}
