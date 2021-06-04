using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.Junction
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
        public List<GroupPersonDao> Persons { get; set; }

        public GroupDao()
        {
            Persons = new List<GroupPersonDao>();
        }
    }

    /// <summary>
    /// Defines the data transfer object of the editable group object.
    /// </summary>
    public class GroupDto : GroupData
    {
        public List<GroupPersonDto> Persons { get; set; }

        public GroupDto()
        {
            Persons = new List<GroupPersonDto>();
        }
    }
}
