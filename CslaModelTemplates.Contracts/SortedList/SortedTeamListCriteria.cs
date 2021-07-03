using System;

namespace CslaModelTemplates.Contracts.SortedList
{
    /// <summary>
    /// Represents the criteria of the read-only sorted team collection.
    /// </summary>
    [Serializable]
    public class SortedTeamListCriteria : SortedListCriteria
    {
        public string TeamName { get; set; }
    }
}
