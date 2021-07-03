using CslaModelTemplates.Dal;

namespace CslaModelTemplates.Contracts.PaginatedSortedList
{
    /// <summary>
    /// Defines the data access functions of the read-only paginated sorted  team collection.
    /// </summary>
    public interface IPaginatedSortedTeamListDal : IDal
    {
        IPaginatedList<PaginatedSortedTeamListItemDao> Fetch(PaginatedSortedTeamListCriteria criteria);
    }
}
