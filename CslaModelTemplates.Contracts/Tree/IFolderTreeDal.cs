using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.Tree
{
    /// <summary>
    /// Defines the data access functions of the read-only folder tree object.
    /// </summary>
    public interface IFolderTreeDal
    {
        List<FolderNodeDao> Fetch(FolderTreeCriteria criteria);
    }
}
