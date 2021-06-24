using System;

namespace CslaModelTemplates.Contracts.SimpleUpsert
{
    /// <summary>
    /// Defines the editable team data.
    /// </summary>
    public class SimpleTeamUpsertData
    {
        public long? TeamKey { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable team object.
    /// </summary>
    public class SimpleTeamUpsertDao : SimpleTeamUpsertData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable team object.
    /// </summary>
    public class SimpleTeamUpsertDto : SimpleTeamUpsertData
    { }
}
