using CslaModelTemplates.Common.Models;
using System;

namespace CslaModelTemplates.Contracts.SelectionWithCode
{
    /// <summary>
    /// Represents the criteria of the read-only root choice collection.
    /// </summary>
    [Serializable]
    public class RootCodeChoiceCriteria : ChoiceCriteria
    {
        public string RootName { get; set; }
    }
}
