namespace CslaModelTemplates.Contracts.SimpleList
{
    /// <summary>
    /// Defines the read-only team list item data.
    /// </summary>
    public class SimpleTeamListItemData
    {
        public long? TeamKey { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only team list item object.
    /// </summary>
    public class SimpleTeamListItemDao : SimpleTeamListItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only team list item object.
    /// </summary>
    public class SimpleTeamListItemDto : SimpleTeamListItemData
    { }
}
