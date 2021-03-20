using CslaModelTemplates.Common.Dal;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the data access functions of the editable team collection.
    /// </summary>
    public interface ITeamSetDal : IDal
    {
        List<TeamSetItemDao> Fetch(TeamSetCriteria criteria);
    }
}
