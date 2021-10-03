using CslaModelTemplates.Dal.Contracts;
using System;

namespace CslaModelTemplates.Contracts.PaginatedSortedList
{
    /// <summary>
    /// Represents the criteria of the read-only paginated sorted team collection.
    /// </summary>
    [Serializable]
    public class PaginatedSortedTeamListCriteria : PaginatedSortedListCriteria
    {
        public string TeamName { get; set; }
    }
}
