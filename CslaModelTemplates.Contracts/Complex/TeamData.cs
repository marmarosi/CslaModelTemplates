using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Defines the editable team data.
    /// </summary>
    public class TeamData
    {
        public long? TeamKey { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable team object.
    /// </summary>
    public class TeamDao : TeamData
    {
        public List<PlayerDao> Players { get; set; }

        public TeamDao()
        {
            Players = new List<PlayerDao>();
        }
    }

    /// <summary>
    /// Defines the data transfer object of the editable team object.
    /// </summary>
    public class TeamDto : TeamData
    {
        public List<PlayerDto> Players { get; set; }

        public TeamDto()
        {
            Players = new List<PlayerDto>();
        }
    }
}
