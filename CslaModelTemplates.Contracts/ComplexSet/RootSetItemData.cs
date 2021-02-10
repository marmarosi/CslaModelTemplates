using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the editable root item data.
    /// </summary>
    public class RootSetItemData
    {
        public long? RootKey { get; set; }
        public string RootCode { get; set; }
        public string RootName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable root item object.
    /// </summary>
    public class RootSetItemDao : RootSetItemData
    {
        public List<RootSetRootItemDao> Items { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable root item object.
    /// </summary>
    public class RootSetItemDto : RootSetItemData
    {
        public List<RootSetRootItemDto> Items { get; set; }

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
