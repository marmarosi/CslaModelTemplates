using System;

namespace CslaModelTemplates.Contracts.ComplexCommand
{
    /// <summary>
    /// Represents the criteria of the count teams by player count command.
    /// </summary>
    [Serializable]
    public class CountTeamsCriteria
    {
        public string TeamName { get; set; }

        public CountTeamsCriteria()
        { }

        public CountTeamsCriteria(
            string teamName
            )
        {
            TeamName = teamName;
        }
    }
}
