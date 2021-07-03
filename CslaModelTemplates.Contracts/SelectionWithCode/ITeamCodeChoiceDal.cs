using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SelectionWithCode
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamCodeChoiceDal : ICodeNameChoiceDal<TeamCodeChoiceCriteria>
    {
        new List<CodeNameOptionDao> Fetch(TeamCodeChoiceCriteria criteria);
    }
}
