namespace CslaModelTemplates.Contracts.ComplexCommand
{
    /// <summary>
    /// Defines the count roots list item data.
    /// </summary>
    public class CountRootsListItemData
    {
        public int ItemCount { get; set; }
        public int CountOfRoots { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the count roots list item object.
    /// </summary>
    public class CountRootsListItemDao : CountRootsListItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the count roots list item object.
    /// </summary>
    public class CountRootsListItemDto : CountRootsListItemData
    { }
}
