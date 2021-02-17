using Csla;
using System;

namespace CslaModelTemplates.Contracts.SimpleView
{
    /// <summary>
    /// Represents the criteria of the read-only team object.
    /// </summary>
    [Serializable]
    public class SimpleTeamViewCriteria : CriteriaBase<SimpleTeamViewCriteria>
    {
        public long TeamKey { get; set; }
    }
}
