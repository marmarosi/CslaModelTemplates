using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the editable team set item data.
    /// </summary>
    public class TeamSetItemData
    {
        public long? TeamKey { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable team set item object.
    /// </summary>
    public class TeamSetItemDao : TeamSetItemData
    {
        public List<TeamSetPlayerDao> Players { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable team set item object.
    /// </summary>
    public class TeamSetItemDto : TeamSetItemData
    {
        public List<TeamSetPlayerDto> Players { get; set; }

        public TeamSetItemDao ToDao()
        {
            return new TeamSetItemDao
            {
                TeamKey = TeamKey,
                TeamCode = TeamCode,
                TeamName = TeamName,
                Players = PlayersToDao(),
                Timestamp = Timestamp
            };
        }

        protected List<TeamSetPlayerDao> PlayersToDao()
        {
            List<TeamSetPlayerDao> list = new List<TeamSetPlayerDao>();

            foreach (TeamSetPlayerDto player in Players)
                list.Add(player.ToDao());

            return list;
        }
    }
}
