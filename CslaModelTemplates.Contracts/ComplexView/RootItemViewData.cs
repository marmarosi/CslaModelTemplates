namespace CslaModelTemplates.Contracts.ComplexView
{
    /// <summary>
    /// Defines the read-only root item data.
    /// </summary>
    public class RootItemViewData
    {
        public long? RootItemKey { get; set; }
        public string RootItemCode { get; set; }
        public string RootItemName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only root item object.
    /// </summary>
    public class RootItemViewDao : RootItemViewData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only root item object.
    /// </summary>
    public class RootItemViewDto : RootItemViewData
    { }
}
