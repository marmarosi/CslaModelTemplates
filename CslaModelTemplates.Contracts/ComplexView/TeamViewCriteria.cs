using Csla;
using System;

namespace CslaModelTemplates.Contracts.ComplexView
{
    /// <summary>
    /// Represents the criteria of the read-only team object.
    /// </summary>
    [Serializable]
    public class TeamViewCriteria : CriteriaBase<TeamViewCriteria>
    {
        public long TeamKey { get; set; }
    }
}
