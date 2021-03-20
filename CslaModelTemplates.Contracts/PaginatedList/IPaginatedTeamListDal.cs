using CslaModelTemplates.Common.Dal;
using CslaModelTemplates.Common.DataTransfer;

namespace CslaModelTemplates.Contracts.PaginatedList
{
    /// <summary>
    /// Defines the data access functions of the read-only paginated team collection.
    /// </summary>
    public interface IPaginatedTeamListDal : IDal
    {
        PaginatedList<PaginatedTeamListItemDao> Fetch(PaginatedTeamListCriteria criteria);
    }
}
