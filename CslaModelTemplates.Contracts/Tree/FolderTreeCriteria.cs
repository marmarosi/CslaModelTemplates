using Csla;
using System;

namespace CslaModelTemplates.Contracts.Tree
{
    /// <summary>
    /// Represents the criteria of the read-only folder tree object.
    /// </summary>
    [Serializable]
    public class FolderTreeParams : CriteriaBase<FolderTreeParams>
    {
        public string RootId { get; set; }

        public FolderTreeCriteria Decode(
            string model
            )
        {
            return new FolderTreeCriteria
            {
                RootKey = KeyHash.Decode(model, RootId) ?? 0
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the read-only folder tree object.
    /// </summary>
    [Serializable]
    public class FolderTreeCriteria : CriteriaBase<FolderTreeCriteria>
    {
        public long RootKey { get; set; }
    }
}
