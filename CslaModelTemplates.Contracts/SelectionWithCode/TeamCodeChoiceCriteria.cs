using System;

namespace CslaModelTemplates.Contracts.SelectionWithCode
{
    /// <summary>
    /// Represents the criteria of the read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamCodeChoiceCriteria : ChoiceCriteria
    {
        public string TeamName { get; set; }
    }
}
