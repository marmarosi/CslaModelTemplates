using Csla;
using System;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Represents the criteria of the editable team collection.
    /// </summary>
    [Serializable]
    public class SimpleTeamSetCriteria : CriteriaBase<SimpleTeamSetCriteria>
    {
        public string TeamName { get; set; }
    }
}
