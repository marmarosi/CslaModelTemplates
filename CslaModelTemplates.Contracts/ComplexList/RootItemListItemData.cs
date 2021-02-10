namespace CslaModelTemplates.Contracts.ComplexList
{
    /// <summary>
    /// Defines the read-only root list item data.
    /// </summary>
    public class RootItemListItemData
    {
        public long? RootItemKey { get; set; }
        public string RootItemCode { get; set; }
        public string RootItemName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only root list item object.
    /// </summary>
    public class RootItemListItemDao : RootItemListItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only root list item object.
    /// </summary>
    public class RootItemListItemDto : RootItemListItemData
    { }
}
