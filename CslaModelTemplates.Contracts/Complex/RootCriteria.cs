using Csla;
using System;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Represents the criteria of the editable root object.
    /// </summary>
    [Serializable]
    public class RootCriteria : CriteriaBase<RootCriteria>
    {
        public long RootKey { get; set; }

        public RootCriteria() { }

        public RootCriteria(
            long rootKey
            )
        {
            RootKey = rootKey;
        }
    }
}
