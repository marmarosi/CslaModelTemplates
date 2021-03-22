namespace CslaModelTemplates.Contracts.Junction
{
    /// <summary>
    /// Defines the editable member data.
    /// </summary>
    public class MemberData
    {
        public long? PersonKey { get; set; }
        public string PersonName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable member object.
    /// </summary>
    public class MemberDao : MemberData
    {
        public long? GroupKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable member object.
    /// </summary>
    public class MemberDto : MemberData
    { }
}
