using Csla;
using System;

namespace CslaModelTemplates.Contracts.Simple
{
    /// <summary>
    /// Represents the criteria of the editable root object.
    /// </summary>
    [Serializable]
    public class SimpleRootCriteria : CriteriaBase<SimpleRootCriteria>
    {
        public long RootKey { get; set; }

        public SimpleRootCriteria() { }

        public SimpleRootCriteria(
            long rootKey
            )
        {
            RootKey = rootKey;
        }
    }
}
