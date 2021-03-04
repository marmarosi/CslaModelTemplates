namespace CslaModelTemplates.Contracts.PaginatedList
{
    /// <summary>
    /// Defines the read-only paginated team list item data.
    /// </summary>
    public class SortedTeamListItemData
    {
        public long? TeamKey { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only paginated team list item object.
    /// </summary>
    public class PaginatedTeamListItemDao : SortedTeamListItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only paginated team list item object.
    /// </summary>
    public class PaginatedTeamListItemDto : SortedTeamListItemData
    { }
}
