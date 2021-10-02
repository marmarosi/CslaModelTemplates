using Csla;
using System;

namespace CslaModelTemplates.Contracts.ComplexView
{
    /// <summary>
    /// Represents the criteria of the read-only team object.
    /// </summary>
    [Serializable]
    public class TeamViewParams
    {
        public string TeamId { get; set; }

        public TeamViewCriteria Decode()
        {
            return new TeamViewCriteria
            {
                TeamKey = KeyHash.Decode(ID.Team, TeamId) ?? 0
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the read-only team object.
    /// </summary>
    [Serializable]
    public class TeamViewCriteria : CriteriaBase<TeamViewCriteria>
    {
        public long TeamKey { get; set; }
    }
}
