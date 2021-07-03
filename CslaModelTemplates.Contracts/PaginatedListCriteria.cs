using Csla;
using System;

namespace CslaModelTemplates.Contracts
{
    /// <summary>
    /// Represents the base criteria of the read-only paginated collections.
    /// </summary>
    [Serializable]
    public class PaginatedListCriteria : CriteriaBase<PaginatedListCriteria>
    {
        /// <summary>
        /// Specifies the index of a page.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Specifies the count of items on a page.
        /// </summary>
        public int PageSize { get; set; }
    }
}
