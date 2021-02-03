using Csla;
using System;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Represents the criteria of the editable root item object.
    /// </summary>
    [Serializable]
    public class RootItemCriteria : CriteriaBase<RootItemCriteria>
    {
        public long RootItemKey { get; set; }
    }
}
