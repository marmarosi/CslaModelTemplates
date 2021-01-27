using Csla;
using System;

namespace CslaModelTemplates.Contracts.ComplexView
{
    /// <summary>
    /// Represents the criteria of the read-only root object.
    /// </summary>
    [Serializable]
    public class RootViewCriteria : CriteriaBase<RootViewCriteria>
    {
        public long RootKey { get; set; }
    }
}
