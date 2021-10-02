using CslaModelTemplates.Dal.Contracts;
using System;

namespace CslaModelTemplates.Contracts.SelectionWithKey
{
    /// <summary>
    /// Represents the criteria of the read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamKeyChoiceCriteria : ChoiceCriteria
    {
        public string TeamName { get; set; }
    }
}
