using CslaModelTemplates.Common.Models;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SelectionByKey
{
    /// <summary>
    /// Defines the data access functions of the read-only root choice collection.
    /// </summary>
    public interface IRootChoiceDal : IKeyNameChoiceDal<RootChoiceCriteria>
    {
        new List<KeyNameOptionDao> Fetch(RootChoiceCriteria criteria);
    }
}
