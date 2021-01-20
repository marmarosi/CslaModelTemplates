using System.Collections.Generic;

namespace CslaModelTemplates.Common.Models
{
    /// <summary>
    /// Represents the client transfer object of the paginated lists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedListCto<T>
    {
        /// <summary>
        /// The total count of items that match the criteria.
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// The list items on the current page.
        /// </summary>
        public T[] Items { get; set; }

        /// <summary>
        /// Creates a new instance of paginated list result.
        /// </summary>
        /// <param name="totalCount">The total count of items that match the criteria.</param>
        /// <param name="count">The count of the items on the current page.</param>
        /// <param name="items">The list items on the current page.</param>
        public PaginatedListCto(
            long totalCount,
            long count,
            IList<T> items
            )
        {
            TotalCount = totalCount;
            Items = new T[count];
            items.CopyTo(Items, 0);
        }
    }
}
