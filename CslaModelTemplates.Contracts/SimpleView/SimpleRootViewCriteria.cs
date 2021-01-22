using Csla;
using System;

namespace CslaModelTemplates.Contracts.SimpleView
{
    /// <summary>
    /// Represents the criteria of the read-only root object.
    /// </summary>
    [Serializable]
    public class SimpleRootViewCriteria : CriteriaBase<SimpleRootViewCriteria>
    {
        public long RootKey { get; set; }
    }
}
