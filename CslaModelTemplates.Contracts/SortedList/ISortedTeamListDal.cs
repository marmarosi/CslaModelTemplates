using CslaModelTemplates.Common.Dal;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SortedList
{
    /// <summary>
    /// Defines the data access functions of the read-only sorted team collection.
    /// </summary>
    public interface ISortedTeamListDal : IDal
    {
        List<SortedTeamListItemDao> Fetch(SortedTeamListCriteria criteria);
    }
}
