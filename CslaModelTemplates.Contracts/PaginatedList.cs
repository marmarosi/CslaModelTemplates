using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts
{
    /// <summary>
    /// Represents the base class of the read-only paginated collections.
    /// </summary>
    [Serializable]
    public class PaginatedList<T> : IPaginatedList<T>
        where T : class
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
