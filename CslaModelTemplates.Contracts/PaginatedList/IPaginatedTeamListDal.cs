using CslaModelTemplates.Common.DataTransfer;

namespace CslaModelTemplates.Contracts.PaginatedList
{
    /// <summary>
    /// Defines the data access functions of the read-only paginated team collection.
    /// </summary>
    public interface IPaginatedTeamListDal
    {
        PaginatedList<PaginatedTeamListItemDao> Fetch(PaginatedTeamListCriteria criteria);
    }
}
