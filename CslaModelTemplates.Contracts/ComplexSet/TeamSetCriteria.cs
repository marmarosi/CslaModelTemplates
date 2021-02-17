using Csla;
using System;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Represents the criteria of the editable team collection.
    /// </summary>
    [Serializable]
    public class TeamSetCriteria : CriteriaBase<TeamSetCriteria>
    {
        public string TeamName { get; set; }
    }
}
