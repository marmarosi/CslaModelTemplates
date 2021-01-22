using CslaModelTemplates.Common.Models;
using System;

namespace CslaModelTemplates.Contracts.SelectionByCode
{
    /// <summary>
    /// Represents the criteria of the read-only root choice collection.
    /// </summary>
    [Serializable]
    public class RootChoiceCriteria : ChoiceCriteria
    {
        public string RootName { get; set; }
    }
}
