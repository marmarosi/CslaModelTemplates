using System;

namespace CslaModelTemplates.Contracts.Simple
{
    /// <summary>
    /// Defines the editable root data.
    /// </summary>
    public class SimpleRootData
    {
        public long? RootKey { get; set; }
        public string RootCode { get; set; }
        public string RootName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable root object.
    /// </summary>
    public class SimpleRootDao : SimpleRootData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable root object.
    /// </summary>
    public class SimpleRootDto : SimpleRootData
    { }
}
