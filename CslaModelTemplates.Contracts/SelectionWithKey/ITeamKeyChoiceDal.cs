using CslaModelTemplates.Common.Models;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SelectionWithKey
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamKeyChoiceDal : IKeyNameChoiceDal<TeamKeyChoiceCriteria>
    {
        new List<KeyNameOptionDao> Fetch(TeamKeyChoiceCriteria criteria);
    }
}
