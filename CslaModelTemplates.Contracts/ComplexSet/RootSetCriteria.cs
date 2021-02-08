using Csla;
using System;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Represents the criteria of the editable root collection.
    /// </summary>
    [Serializable]
    public class RootSetCriteria : CriteriaBase<RootSetCriteria>
    {
        public string RootName { get; set; }
    }
}
