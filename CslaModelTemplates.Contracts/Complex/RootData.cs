using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Defines the editable root data.
    /// </summary>
    public class RootData
    {
        public long? RootKey;
        public string RootCode;
        public string RootName;
        public DateTime? Timestamp;
    }

    /// <summary>
    /// Defines the data access object of the editable root object.
    /// </summary>
    public class RootDao : RootData
    {
        public List<RootItemDao> Items;
    }

    /// <summary>
    /// Defines the data transfer object of the editable root object.
    /// </summary>
    public class RootDto : RootData
    {
        public List<RootItemDto> Items;
    }
}
