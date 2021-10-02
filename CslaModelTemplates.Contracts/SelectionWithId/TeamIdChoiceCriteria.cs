using CslaModelTemplates.Dal.Contracts;
using System;

namespace CslaModelTemplates.Contracts.SelectionWithId
{
    /// <summary>
    /// Represents the criteria of the read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamIdChoiceCriteria : ChoiceCriteria
    {
        public string TeamName { get; set; }
    }
}
