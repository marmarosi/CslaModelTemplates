using System;

namespace CslaModelTemplates.Contracts.SimpleList
{
    /// <summary>
    /// Represents the criteria of the read-only team collection.
    /// </summary>
    [Serializable]
    public class SimpleTeamListCriteria
    {
        public string TeamName { get; set; }
    }
}
