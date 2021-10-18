using System;

namespace CslaModelTemplates.Contracts.Junction
{
    /// <summary>
    /// Represents the criteria of the editable group object.
    /// </summary>
    [Serializable]
    public class GroupParams
    {
        public string GroupId { get; set; }

        public GroupCriteria Decode()
        {
            return new GroupCriteria
            {
                GroupKey = KeyHash.Decode(ID.Group, GroupId) ?? 0
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the editable group object.
    /// </summary>
    [Serializable]
    public class GroupCriteria
    {
        public long GroupKey { get; set; }

        public GroupCriteria() { }

        public GroupCriteria(
            long groupKey
            )
        {
            GroupKey = groupKey;
        }
    }
}
