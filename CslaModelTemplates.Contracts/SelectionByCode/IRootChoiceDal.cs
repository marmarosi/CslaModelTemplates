using CslaModelTemplates.Common.Models;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SelectionByCode
{
    /// <summary>
    /// Defines the data access functions of the read-only root choice collection.
    /// </summary>
    public interface IRootChoiceDal : ICodeNameChoiceDal<RootChoiceCriteria>
    {
        new List<CodeNameOptionDao> Fetch(RootChoiceCriteria criteria);
    }
}
