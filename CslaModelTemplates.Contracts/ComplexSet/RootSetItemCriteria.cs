using Csla;
using System;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Represents the criteria of the editable root item object.
    /// </summary>
    [Serializable]
    public class RootSetItemCriteria : CriteriaBase<RootSetItemCriteria>
    {
        public long RootKey { get; set; }

        public RootSetItemCriteria()
        { }

        public RootSetItemCriteria(
            long rootKey
            )
        {
            RootKey = rootKey;
        }
    }
}
