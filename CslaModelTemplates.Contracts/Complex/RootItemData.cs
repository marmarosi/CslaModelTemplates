namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Defines the editable root item data.
    /// </summary>
    public class RootItemData
    {
        public long? RootItemKey;
        public long? RootKey;
        public string RootItemCode;
        public string RootItemName;
    }

    /// <summary>
    /// Defines the data access object of the editable root item object.
    /// </summary>
    public class RootItemDao : RootItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable root item object.
    /// </summary>
    public class RootItemDto : RootItemData
    {
        public RootItemDao ToDao()
        {
            return new RootItemDao
            {
                RootItemKey = RootItemKey,
                RootKey = RootKey,
                RootItemCode = RootItemCode,
                RootItemName = RootItemName
            };
        }

    }
}
