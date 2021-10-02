using Csla;
using System;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Represents the criteria of the editable team set item object.
    /// </summary>
    [Serializable]
    public class SimpleTeamSetItemParams : CriteriaBase<SimpleTeamSetItemParams>
    {
        public string TeamId { get; set; }

        public SimpleTeamSetItemCriteria Decode()
        {
            return new SimpleTeamSetItemCriteria
            {
                TeamKey = KeyHash.Decode(ID.Team, TeamId).Value
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the editable team set item object.
    /// </summary>
    [Serializable]
    public class SimpleTeamSetItemCriteria : CriteriaBase<SimpleTeamSetItemCriteria>
    {
        public long TeamKey { get; set; }

        public SimpleTeamSetItemCriteria()
        { }

        public SimpleTeamSetItemCriteria(
            long teamKey
            )
        {
            TeamKey = teamKey;
        }
    }
}
