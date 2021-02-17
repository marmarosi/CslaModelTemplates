using System;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Defines the editable team set item data.
    /// </summary>
    public class SimpleTeamSetItemData
    {
        public long? TeamKey { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable team set item object.
    /// </summary>
    public class SimpleTeamSetItemDao : SimpleTeamSetItemData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable team set item object.
    /// </summary>
    public class SimpleTeamSetItemDto : SimpleTeamSetItemData
    {
        public SimpleTeamSetItemDao ToDao()
        {
            return new SimpleTeamSetItemDao
            {
                TeamKey = TeamKey,
                TeamCode = TeamCode,
                TeamName = TeamName,
                Timestamp = Timestamp
            };
        }
    }
}
