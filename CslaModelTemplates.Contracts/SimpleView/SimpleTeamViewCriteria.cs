using System;

namespace CslaModelTemplates.Contracts.SimpleView
{
    /// <summary>
    /// Represents the criteria of the read-only team object.
    /// </summary>
    [Serializable]
    public class SimpleTeamViewParams
    {
        public string TeamId { get; set; }

        public SimpleTeamViewCriteria Decode()
        {
            return new SimpleTeamViewCriteria
            {
                TeamKey = KeyHash.Decode(ID.Team, TeamId) ?? 0
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the read-only team object.
    /// </summary>
    [Serializable]
    public class SimpleTeamViewCriteria
{
        public long TeamKey { get; set; }
    }
}
