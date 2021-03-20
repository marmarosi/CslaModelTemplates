using CslaModelTemplates.Common.Models;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.LookUp
{
    /// <summary>
    /// Defines the editable group data.
    /// </summary>
    public class GroupData
    {
        public long? GroupKey { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable group object.
    /// </summary>
    public class GroupDao : GroupData
    {
        public List<MemberDao> Members { get; set; }

        public GroupDao()
        {
            Members = new List<MemberDao>();
        }
    }

    /// <summary>
    /// Defines the data transfer object of the editable group object.
    /// </summary>
    public class GroupDto : GroupData
    {
        public List<MemberDto> Members { get; set; }

        public GroupDto()
        {
            Members = new List<MemberDto>();
        }
    }
}
