using Csla;
using System;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Represents the criteria of the editable team set item object.
    /// </summary>
    [Serializable]
    public class TeamSetItemCriteria : CriteriaBase<TeamSetItemCriteria>
    {
        public long TeamKey { get; set; }

        public TeamSetItemCriteria()
        { }

        public TeamSetItemCriteria(
            long teamKey
            )
        {
            TeamKey = teamKey;
        }
    }
}
