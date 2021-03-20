using System;

namespace CslaModelTemplates.Contracts.Simple
{
    /// <summary>
    /// Defines the editable team data.
    /// </summary>
    public class SimpleTeamData
    {
        public long? TeamKey { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable team object.
    /// </summary>
    public class SimpleTeamDao : SimpleTeamData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable team object.
    /// </summary>
    public class SimpleTeamDto : SimpleTeamData
    { }
}
