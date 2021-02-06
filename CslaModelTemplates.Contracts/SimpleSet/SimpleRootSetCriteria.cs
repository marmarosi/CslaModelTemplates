using Csla;
using System;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Represents the criteria of the editable root collection.
    /// </summary>
    [Serializable]
    public class SimpleRootSetCriteria : CriteriaBase<SimpleRootSetCriteria>
    {
        public string RootName { get; set; }
    }
}
