using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the data access functions of the editable team collection.
    /// </summary>
    public interface ITeamSetDal
    {
        List<TeamSetItemDao> Fetch(TeamSetCriteria criteria);
    }
}
