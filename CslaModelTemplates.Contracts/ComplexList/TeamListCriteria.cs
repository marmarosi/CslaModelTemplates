using Csla;
using System;

namespace CslaModelTemplates.Contracts.ComplexList
{
    /// <summary>
    /// Represents the criteria of the read-only team collection.
    /// </summary>
    [Serializable]
    public class TeamListCriteria : CriteriaBase<TeamListCriteria>
    {
        public string TeamName { get; set; }
    }
}
