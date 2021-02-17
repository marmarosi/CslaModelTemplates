using Csla;
using System;

namespace CslaModelTemplates.Contracts.ComplexCommand
{
    /// <summary>
    /// Represents the criteria of the count teams by player count command.
    /// </summary>
    [Serializable]
    public class CountTeamsCriteria : CriteriaBase<CountTeamsCriteria>
    {
        public string TeamName { get; set; }

        public CountTeamsCriteria(
            string teamName
            )
        {
            TeamName = teamName;
        }
    }
}
