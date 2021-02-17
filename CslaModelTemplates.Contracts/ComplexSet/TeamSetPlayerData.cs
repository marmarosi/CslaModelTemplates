using System.Text.Json.Serialization;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the editable team set player data.
    /// </summary>
    public class TeamSetPlayerData
    {
        public long? PlayerKey { get; set; }
        public long? TeamKey { get; set; }
        public string PlayerCode { get; set; }
        public string PlayerName { get; set; }
        [JsonIgnore]
        public string __teamCode; // for error messages
    }

    /// <summary>
    /// Defines the data access object of the editable team set player object.
    /// </summary>
    public class TeamSetPlayerDao : TeamSetPlayerData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable team set player object.
    /// </summary>
    public class TeamSetPlayerDto : TeamSetPlayerData
    {
        public TeamSetPlayerDao ToDao()
        {
            return new TeamSetPlayerDao
            {
                PlayerKey = PlayerKey,
                TeamKey = TeamKey,
                PlayerCode = PlayerCode,
                PlayerName = PlayerName,
                __teamCode = __teamCode
            };
        }

    }
}
