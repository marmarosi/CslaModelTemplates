using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexView
{
    /// <summary>
    /// Defines the read-only team data.
    /// </summary>
    public class TeamViewData
    {
        public long? TeamKey { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only team object.
    /// </summary>
    public class TeamViewDao : TeamViewData
    {
        public List<PlayerViewDao> Players { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only team object.
    /// </summary>
    public class TeamViewDto : TeamViewData
    {
        public List<PlayerViewDto> Players { get; set; }
    }
}
