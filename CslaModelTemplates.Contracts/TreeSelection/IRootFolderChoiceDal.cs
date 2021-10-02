using CslaModelTemplates.Dal.Contracts;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.TreeSelection
{
    /// <summary>
    /// Defines the data access functions of the read-only tree choice collection.
    /// </summary>
    public interface IRootFolderChoiceDal : IIdNameChoiceDal<RootFolderChoiceCriteria>
    {
        new List<IdNameOptionDao> Fetch(RootFolderChoiceCriteria criteria);
    }
}
