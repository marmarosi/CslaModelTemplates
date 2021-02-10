using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Defines the editable root data.
    /// </summary>
    public class RootData
    {
        public long? RootKey { get; set; }
        public string RootCode { get; set; }
        public string RootName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable root object.
    /// </summary>
    public class RootDao : RootData
    {
        public List<RootItemDao> Items { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable root object.
    /// </summary>
    public class RootDto : RootData
    {
        public List<RootItemDto> Items { get; set; }
    }
}
