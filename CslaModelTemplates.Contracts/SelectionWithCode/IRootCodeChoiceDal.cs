using CslaModelTemplates.Common.Models;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SelectionWithCode
{
    /// <summary>
    /// Defines the data access functions of the read-only root choice collection.
    /// </summary>
    public interface IRootCodeChoiceDal : ICodeNameChoiceDal<RootCodeChoiceCriteria>
    {
        new List<CodeNameOptionDao> Fetch(RootCodeChoiceCriteria criteria);
    }
}
