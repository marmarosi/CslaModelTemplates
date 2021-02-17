namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Defines the data access functions of the editable player object.
    /// </summary>
    public interface IPlayerDal
    {
        void Insert(PlayerDao dao);
        void Update(PlayerDao dao);
        void Delete(PlayerCriteria criteria);
    }
}
