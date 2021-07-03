using CslaModelTemplates.Dal;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the data access functions of the editable player object.
    /// </summary>
    public interface ITeamSetPlayerDal : IDal
    {
        void Insert(TeamSetPlayerDao dao);
        void Update(TeamSetPlayerDao dao);
        void Delete(TeamSetPlayerCriteria criteria);
    }
}
