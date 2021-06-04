namespace CslaModelTemplates.Contracts.JunctionView
{
    /// <summary>
    /// Defines the read-only group-person data.
    /// </summary>
    public class GroupPersonViewData
    {
        public long? PersonKey { get; set; }
        public string PersonName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only group-person object.
    /// </summary>
    public class GroupPersonViewDao : GroupPersonViewData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only group-person object.
    /// </summary>
    public class GroupPersonViewDto : GroupPersonViewData
    { }
}
