using Csla;
using System;

namespace CslaModelTemplates.Contracts.Tree
{
    /// <summary>
    /// Represents the criteria of the read-only folder tree object.
    /// </summary>
    [Serializable]
    public class FolderTreeCriteria : CriteriaBase<FolderTreeCriteria>
    {
        public long RootKey { get; set; }
    }
}
