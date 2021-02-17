namespace CslaModelTemplates.Contracts.SimpleCommand
{
    /// <summary>
    /// Defines the data access object of the rename team command.
    /// </summary>
    public class RenameTeamData
    {
        public long TeamKey { get; set; }
        public string TeamName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the rename team command.
    /// </summary>
    public class RenameTeamDao : RenameTeamData
    {
        public RenameTeamDao(
            long teamKey,
            string teamName
            )
        {
            TeamKey = teamKey;
            TeamName = teamName;
        }
    }

    /// <summary>
    /// Defines the data transfer object of the rename team command.
    /// </summary>
    public class RenameTeamDto : RenameTeamData
    { }
}
