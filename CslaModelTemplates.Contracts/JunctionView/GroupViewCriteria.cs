using Csla;
using System;

namespace CslaModelTemplates.Contracts.JunctionView
{
    /// <summary>
    /// Represents the criteria of the read-only group object.
    /// </summary>
    [Serializable]
    public class GroupViewCriteria : CriteriaBase<GroupViewCriteria>
    {
        public long GroupKey { get; set; }
    }
}
