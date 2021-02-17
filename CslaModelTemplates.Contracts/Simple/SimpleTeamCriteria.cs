using Csla;
using System;

namespace CslaModelTemplates.Contracts.Simple
{
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
