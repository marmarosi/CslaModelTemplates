using CslaModelTemplates.Dal;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SimpleList
{
    /// <summary>
    /// Defines the data access functions of the read-only team collection.
    /// </summary>
    public interface ISimpleTeamListDal : IDal
    {
        List<SimpleTeamListItemDao> Fetch(SimpleTeamListCriteria criteria);
    }
}
