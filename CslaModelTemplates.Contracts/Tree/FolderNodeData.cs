using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.Tree
{
    /// <summary>
    /// Defines the read-only folder tree data.
    /// </summary>
    public class FolderNodeData
    {
        public long? FolderKey;
        public long? ParentKey;
        public int? FolderOrder;
        public string FolderName;
        public int? Level;
    }

    /// <summary>
    /// Defines the data access object of the read-only folder tree object.
    /// </summary>
    public class FolderNodeDao : FolderNodeData
    {
        public List<FolderNodeDao> Children;
    }

    /// <summary>
    /// Defines the data transfer object of the read-only folder tree object.
    /// </summary>
    public class FolderNodeDto : FolderNodeData
    {
        public List<FolderNodeDto> Children;
    }
}
