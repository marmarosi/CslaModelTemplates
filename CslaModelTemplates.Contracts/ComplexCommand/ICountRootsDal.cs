using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexCommand
{
    /// <summary>
    /// Defines the data access functions of the count roots by item count command.
    /// </summary>
    public interface ICountRootsDal
    {
        List<CountRootsListItemDao> Execute(CountRootsCriteria criteria);
    }
}
