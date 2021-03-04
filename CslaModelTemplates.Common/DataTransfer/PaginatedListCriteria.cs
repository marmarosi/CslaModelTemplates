using Csla;
using System;

namespace CslaModelTemplates.Common.DataTransfer
{
    /// <summary>
    /// Represents the base criteria of the read-only paginated collections.
    /// </summary>
    [Serializable]
    public class PaginatedListCriteria : CriteriaBase<PaginatedListCriteria>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
