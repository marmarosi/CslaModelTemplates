using System;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Represents the criteria of the editable team set item object.
    /// </summary>
    [Serializable]
    public class SimpleTeamSetItemCriteria
    {
        public long TeamKey { get; set; }

        public SimpleTeamSetItemCriteria()
        { }

        public SimpleTeamSetItemCriteria(
            long teamKey
            )
        {
            TeamKey = teamKey;
        }
    }
}
