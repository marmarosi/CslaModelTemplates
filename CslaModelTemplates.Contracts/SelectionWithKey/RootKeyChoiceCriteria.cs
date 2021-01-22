using CslaModelTemplates.Common.Models;
using System;

namespace CslaModelTemplates.Contracts.SelectionWithKey
{
    /// <summary>
    /// Represents the criteria of the read-only root choice collection.
    /// </summary>
    [Serializable]
    public class RootKeyChoiceCriteria : ChoiceCriteria
    {
        public string RootName { get; set; }
    }
}
