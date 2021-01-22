namespace CslaModelTemplates.Contracts.SimpleList
{
    /// <summary>
    /// Defines the read-only root list item data.
    /// </summary>
    public class SimpleRootListItemData
    {
        public long? RootKey { get; set; }
        public string RootCode { get; set; }
        public string RootName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only root list item object.
    /// </summary>
    public class SimpleRootListItemDao : SimpleRootListItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only root list item object.
    /// </summary>
    public class SimpleRootListItemDto : SimpleRootListItemData
    { }
}
