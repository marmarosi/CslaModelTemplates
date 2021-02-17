namespace CslaModelTemplates.Contracts.SimpleView
{
    /// <summary>
    /// Defines the read-only team data.
    /// </summary>
    public class SimpleTeamViewData
    {
        public long? TeamKey { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only team object.
    /// </summary>
    public class SimpleTeamViewDao : SimpleTeamViewData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only team object.
    /// </summary>
    public class SimpleTeamViewDto : SimpleTeamViewData
    { }
}
