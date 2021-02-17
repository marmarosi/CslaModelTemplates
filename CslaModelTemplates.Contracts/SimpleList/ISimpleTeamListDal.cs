using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SimpleList
{
    /// <summary>
    /// Defines the data access functions of the read-only team collection.
    /// </summary>
    public interface ISimpleTeamListDal
    {
        List<SimpleTeamListItemDao> Fetch(SimpleTeamListCriteria criteria);
    }
}
