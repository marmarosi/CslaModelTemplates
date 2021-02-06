using System;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Defines the editable root item data.
    /// </summary>
    public class SimpleRootSetItemData
    {
        public long? RootKey;
        public string RootCode;
        public string RootName;
        public DateTime? Timestamp;
    }

    /// <summary>
    /// Defines the data access object of the editable root item object.
    /// </summary>
    public class SimpleRootSetItemDao : SimpleRootSetItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable root item object.
    /// </summary>
    public class SimpleRootSetItemDto : SimpleRootSetItemData
    {
        public SimpleRootSetItemDao ToDao()
        {
            return new SimpleRootSetItemDao
            {
                RootKey = RootKey,
                RootCode = RootCode,
                RootName = RootName,
                Timestamp = Timestamp
            };
        }
    }
}
