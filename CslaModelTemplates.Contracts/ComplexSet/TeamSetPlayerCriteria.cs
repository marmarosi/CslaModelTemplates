using Csla;
using System;
using System.Text.Json.Serialization;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Represents the criteria of the editable player object.
    /// </summary>
    [Serializable]
    public class TeamSetPlayerCriteria : CriteriaBase<TeamSetPlayerCriteria>
    {
        public long PlayerKey { get; set; }
        [JsonIgnore]
        public string __teamCode { get; set; } // for error messages
        [JsonIgnore]
        public string __playerCode { get; set; } // for error messages

        public TeamSetPlayerCriteria()
        { }

        public TeamSetPlayerCriteria(
            long playerKey
            )
        {
            PlayerKey = playerKey;
        }
    }
}
