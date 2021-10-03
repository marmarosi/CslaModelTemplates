using CslaModelTemplates.Dal;
using CslaModelTemplates.Dal.Contracts;

namespace CslaModelTemplates.Contracts.PaginatedList
{
    /// <summary>
    /// Defines the data access functions of the read-only paginated team collection.
    /// </summary>
    public interface IPaginatedTeamListDal : IDal
    {
        IPaginatedList<PaginatedTeamListItemDao> Fetch(PaginatedTeamListCriteria criteria);
    }
}
