using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.LookUpView
{
    /// <summary>
    /// Defines the read-only group data.
    /// </summary>
    public class GroupViewData
    {
        public long? GroupKey { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only group object.
    /// </summary>
    public class GroupViewDao : GroupViewData
    {
        public List<MemberViewDao> Members { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only group object.
    /// </summary>
    public class GroupViewDto : GroupViewData
    {
        public List<MemberViewDto> Members { get; set; }
    }
}
