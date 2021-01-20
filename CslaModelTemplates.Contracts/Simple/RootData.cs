using System;

namespace CslaModelTemplates.Contracts.Simple
{
    /// <summary>
    /// Defines the editable root data.
    /// </summary>
    public class RootData
    {
        public long? RootKey { get; set; }
        public string RootName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable root object.
    /// </summary>
    public class RootDao : RootData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable root object.
    /// </summary>
    public class RootDto : RootData
    { }
}
