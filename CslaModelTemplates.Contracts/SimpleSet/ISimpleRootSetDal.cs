using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Defines the data access functions of the editable root collection.
    /// </summary>
    public interface ISimpleRootSetDal
    {
        List<SimpleRootSetItemDao> Fetch(SimpleRootSetCriteria criteria);
    }
}
