using CslaModelTemplates.Common;
using System.Text.Json.Serialization;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Defines the editable root item data.
    /// </summary>
    public class RootItemData
    {
        public long? RootItemKey { get; set; }
        public long? RootKey { get; set; }
        public string RootItemCode { get; set; }
        public string RootItemName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable root item object.
    /// </summary>
    public class RootItemDao : RootItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable root item object.
    /// </summary>
    public class RootItemDto : RootItemData, IChildDto
    {
        [JsonIgnore]
        public bool __Processed { get; set; }
    }
}
