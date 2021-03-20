using Csla;
using System;

namespace CslaModelTemplates.Contracts.LookUp
{
    /// <summary>
    /// Represents the criteria of the editable group object.
    /// </summary>
    [Serializable]
    public class GroupCriteria : CriteriaBase<GroupCriteria>
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
