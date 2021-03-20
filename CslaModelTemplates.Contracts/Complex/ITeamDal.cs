using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Defines the data access functions of the editable team object.
    /// </summary>
    public interface ITeamDal : IDal
    {
        TeamDao Fetch(TeamCriteria criteria);
        void Insert(TeamDao dao);
        void Update(TeamDao dao);
        void Delete(TeamCriteria criteria);
    }
}
