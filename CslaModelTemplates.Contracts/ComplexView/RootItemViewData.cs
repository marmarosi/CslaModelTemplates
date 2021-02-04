namespace CslaModelTemplates.Contracts.ComplexView
{
    /// <summary>
    /// Defines the read-only root item data.
    /// </summary>
    public class RootItemViewData
    {
        public long? RootItemKey;
        public string RootItemCode;
        public string RootItemName;
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
