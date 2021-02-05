using Csla;
using System;

namespace CslaModelTemplates.Contracts.ComplexCommand
{
    /// <summary>
    /// Represents the criteria of the count roots by item count command.
    /// </summary>
    [Serializable]
    public class CountRootsCriteria : CriteriaBase<CountRootsCriteria>
    {
        public string RootName { get; set; }

        public CountRootsCriteria(
            string rootName
            )
        {
            RootName = rootName;
        }
    }
}
