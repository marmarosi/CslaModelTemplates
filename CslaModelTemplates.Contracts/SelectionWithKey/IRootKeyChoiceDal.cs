using CslaModelTemplates.Common.Models;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SelectionWithKey
{
    /// <summary>
    /// Defines the data access functions of the read-only root choice collection.
    /// </summary>
    public interface IRootKeyChoiceDal : IKeyNameChoiceDal<RootKeyChoiceCriteria>
    {
        new List<KeyNameOptionDao> Fetch(RootKeyChoiceCriteria criteria);
    }
}
