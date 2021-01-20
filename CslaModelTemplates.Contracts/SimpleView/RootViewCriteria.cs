using Csla;
using System;

namespace CslaModelTemplates.Contracts.SimpleView
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
