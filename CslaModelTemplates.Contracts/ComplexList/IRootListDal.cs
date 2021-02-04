using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexList
{
    /// <summary>
    /// Defines the data access functions of the read-only root collection.
    /// </summary>
    public interface IRootListDal
    {
        List<RootListItemDao> Fetch(RootListCriteria criteria);
    }
}
