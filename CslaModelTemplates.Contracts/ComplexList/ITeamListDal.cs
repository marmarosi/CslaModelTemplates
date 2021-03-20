using CslaModelTemplates.Common.Dal;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexList
{
    /// <summary>
    /// Defines the data access functions of the read-only team collection.
    /// </summary>
    public interface ITeamListDal : IDal
    {
        List<TeamListItemDao> Fetch(TeamListCriteria criteria);
    }
}
