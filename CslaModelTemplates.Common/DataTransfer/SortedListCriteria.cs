using Csla;
using System;

namespace CslaModelTemplates.Common.DataTransfer
{
    /// <summary>
    /// Represents the base criteria of the read-only sorted collections.
    /// </summary>
    [Serializable]
    public class SortedListCriteria : CriteriaBase<SortedListCriteria>
    {
        /// <summary>
        /// Specifies the properties to sort a list of items by.
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Specifies the direction in which to sort a list of items.
        /// </summary>
        public SortDirection SortDirection { get; set; }
    }
}
