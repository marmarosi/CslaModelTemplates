using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.Tree
{
    /// <summary>
    /// Defines the read-only folder tree data.
    /// </summary>
    public class FolderNodeData
    {
        public long? FolderKey { get; set; }
        public long? ParentKey { get; set; }
        public int? FolderOrder { get; set; }
        public string FolderName { get; set; }
        public int? Level { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only folder tree object.
    /// </summary>
    public class FolderNodeDao : FolderNodeData
    {
        public List<FolderNodeDao> Children { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only folder tree object.
    /// </summary>
    public class FolderNodeDto : FolderNodeData
    {
        public List<FolderNodeDto> Children { get; set; }
    }
}
