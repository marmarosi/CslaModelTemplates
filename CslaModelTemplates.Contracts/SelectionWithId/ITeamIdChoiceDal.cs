using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SelectionWithId
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamIdChoiceDal : IIdNameChoiceDal<TeamIdChoiceCriteria>
    {
        new List<IdNameOptionDao> Fetch(TeamIdChoiceCriteria criteria);
    }
}
