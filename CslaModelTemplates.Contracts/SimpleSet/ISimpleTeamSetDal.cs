using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Defines the data access functions of the editable team collection.
    /// </summary>
    public interface ISimpleTeamSetDal
    {
        List<SimpleTeamSetItemDao> Fetch(SimpleTeamSetCriteria criteria);
    }
}
