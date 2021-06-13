using System.Collections.Generic;

namespace CslaModelTemplates.Common.DataTransfer
{
    /// <summary>
    /// Defines the properties of read-only paginated collections.
    /// </summary>
    public interface IPaginatedList<T>
        where T : class
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
