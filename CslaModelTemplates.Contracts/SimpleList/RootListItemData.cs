namespace CslaModelTemplates.Contracts.SimpleList
{
    /// <summary>
    /// Defines the read-only root list item data.
    /// </summary>
    public class RootListItemData
    {
        public long? RootKey { get; set; }
        public string RootCode { get; set; }
        public string RootName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only root list item object.
    /// </summary>
    public class RootListItemDao : RootListItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only root list item object.
    /// </summary>
    public class RootListItemDto : RootListItemData
    { }
}
