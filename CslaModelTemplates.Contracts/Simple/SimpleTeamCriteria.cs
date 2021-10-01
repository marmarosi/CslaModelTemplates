using Csla;
using System;

namespace CslaModelTemplates.Contracts.Simple
{
    /// <summary>
    /// Represents the criteria of the read-only team object.
    /// </summary>
    [Serializable]
    public class SimpleTeamParams
    {
        public string TeamId { get; set; }

        public SimpleTeamCriteria Decode(
            string model
            )
        {
            return new SimpleTeamCriteria
            {
                TeamKey = KeyHash.Decode(model, TeamId) ?? 0
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the editable team object.
    /// </summary>
    [Serializable]
    public class SimpleTeamCriteria : CriteriaBase<SimpleTeamCriteria>
    {
        public long TeamKey { get; set; }

        public SimpleTeamCriteria() { }

        public SimpleTeamCriteria(
            long teamKey
            )
        {
            TeamKey = teamKey;
        }
    }
}
