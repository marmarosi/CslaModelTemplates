using System;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Defines the editable team set item data.
    /// </summary>
    public class SimpleTeamSetItemData
    {
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable team set item object.
    /// </summary>
    public class SimpleTeamSetItemDao : SimpleTeamSetItemData
    {
        public long? TeamKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable team set item object.
    /// </summary>
    public class SimpleTeamSetItemDto : SimpleTeamSetItemData
    {
        public string TeamId { get; set; }
    }
}
