using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the data access functions of the editable root collection.
    /// </summary>
    public interface IRootSetDal
    {
        List<RootSetItemDao> Fetch(RootSetCriteria criteria);
    }
}
