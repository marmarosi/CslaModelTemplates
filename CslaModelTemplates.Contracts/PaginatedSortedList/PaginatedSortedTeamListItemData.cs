namespace CslaModelTemplates.Contracts.PaginatedSortedList
{
    /// <summary>
    /// Defines the read-only paginated sorted team list item data.
    /// </summary>
    public class PaginatedSortedTeamListItemData
    {
        public long? TeamKey { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only paginated sorted team list item object.
    /// </summary>
    public class PaginatedSortedTeamListItemDao : PaginatedSortedTeamListItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only paginated sorted team list item object.
    /// </summary>
    public class PaginatedSortedTeamListItemDto : PaginatedSortedTeamListItemData
    { }
}
