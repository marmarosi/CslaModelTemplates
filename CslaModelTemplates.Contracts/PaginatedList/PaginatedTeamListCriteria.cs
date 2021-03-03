using System;

namespace CslaModelTemplates.Contracts.PaginatedList
{
    /// <summary>
    /// Represents the criteria of the read-only paginated team collection.
    /// </summary>
    [Serializable]
    public class PaginatedTeamListCriteria : PaginatedListCriteria
    {
        public string TeamName { get; set; }
    }
}
