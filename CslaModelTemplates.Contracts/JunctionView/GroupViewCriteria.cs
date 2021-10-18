using System;

namespace CslaModelTemplates.Contracts.JunctionView
{
    /// <summary>
    /// Represents the criteria of the read-only group object.
    /// </summary>
    [Serializable]
    public class GroupViewParams
    {
        public string GroupId { get; set; }

        public GroupViewCriteria Decode()
        {
            return new GroupViewCriteria
            {
                GroupKey = KeyHash.Decode(ID.Group, GroupId) ?? 0
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the read-only group object.
    /// </summary>
    [Serializable]
    public class GroupViewCriteria
    {
        public long GroupKey { get; set; }
    }
}
