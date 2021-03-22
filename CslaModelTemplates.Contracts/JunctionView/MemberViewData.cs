namespace CslaModelTemplates.Contracts.JunctionView
{
    /// <summary>
    /// Defines the read-only member data.
    /// </summary>
    public class MemberViewData
    {
        public long? PersonKey { get; set; }
        public string PersonName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only member object.
    /// </summary>
    public class MemberViewDao : MemberViewData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only member object.
    /// </summary>
    public class MemberViewDto : MemberViewData
    { }
}
