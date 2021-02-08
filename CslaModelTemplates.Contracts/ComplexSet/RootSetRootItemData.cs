using System.Text.Json.Serialization;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the editable root item data.
    /// </summary>
    public class RootSetRootItemData
    {
        public long? RootItemKey;
        public long? RootKey;
        public string RootItemCode;
        public string RootItemName;
        [JsonIgnore]
        public string __rootCode; // for error messages
    }

    /// <summary>
    /// Defines the data access object of the editable root item object.
    /// </summary>
    public class RootSetRootItemDao : RootSetRootItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable root item object.
    /// </summary>
    public class RootSetRootItemDto : RootSetRootItemData
    {
        public RootSetRootItemDao ToDao()
        {
            return new RootSetRootItemDao
            {
                RootItemKey = RootItemKey,
                RootKey = RootKey,
                RootItemCode = RootItemCode,
                RootItemName = RootItemName,
                __rootCode = __rootCode
            };
        }

    }
}
