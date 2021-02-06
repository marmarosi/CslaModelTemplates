using Csla;
using System;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Represents the criteria of the editable root item object.
    /// </summary>
    [Serializable]
    public class SimpleRootSetItemCriteria : CriteriaBase<SimpleRootSetItemCriteria>
    {
        public long RootKey { get; set; }

        public SimpleRootSetItemCriteria()
        { }

        public SimpleRootSetItemCriteria(
            long rootKey
            )
        {
            RootKey = rootKey;
        }
    }
}
