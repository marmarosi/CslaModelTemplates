using CslaModelTemplates.Common.DataTransfer;

namespace CslaModelTemplates.Contracts.PaginatedSortedList
{
    /// <summary>
    /// Defines the data access functions of the read-only paginated sorted  team collection.
    /// </summary>
    public interface IPaginatedSortedTeamListDal
    {
        PaginatedList<PaginatedSortedTeamListItemDao> Fetch(PaginatedSortedTeamListCriteria criteria);
    }
}
