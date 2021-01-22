using Csla;
using System;

namespace CslaModelTemplates.Contracts.SimpleList
{
    /// <summary>
    /// Represents the criteria of the read-only root collection.
    /// </summary>
    [Serializable]
    public class SimpleRootListCriteria : CriteriaBase<SimpleRootListCriteria>
    {
        public string RootName { get; set; }
    }
}
