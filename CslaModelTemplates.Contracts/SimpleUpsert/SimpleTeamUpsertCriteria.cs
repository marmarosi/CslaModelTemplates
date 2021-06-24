using Csla;
using System;

namespace CslaModelTemplates.Contracts.SimpleUpsert
{
    /// <summary>
    /// Represents the criteria of the editable team object.
    /// </summary>
    [Serializable]
    public class SimpleTeamUpsertCriteria : CriteriaBase<SimpleTeamUpsertCriteria>
    {
        public string TeamCode { get; set; }

        public SimpleTeamUpsertCriteria() { }

        public SimpleTeamUpsertCriteria(
            string teamCode
            )
        {
            TeamCode = teamCode;
        }
    }
}
