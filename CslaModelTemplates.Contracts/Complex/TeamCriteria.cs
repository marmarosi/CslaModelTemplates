using Csla;
using System;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Represents the criteria of the editable team object.
    /// </summary>
    [Serializable]
    public class TeamCriteria : CriteriaBase<TeamCriteria>
    {
        public long TeamKey { get; set; }

        public TeamCriteria() { }

        public TeamCriteria(
            long teamKey
            )
        {
            TeamKey = teamKey;
        }
    }
}
