using CslaModelTemplates.Dal;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Defines the data access functions of the editable player object.
    /// </summary>
    public interface IPlayerDal : IDal
    {
        void Insert(PlayerDao dao);
        void Update(PlayerDao dao);
        void Delete(PlayerCriteria criteria);
    }
}
