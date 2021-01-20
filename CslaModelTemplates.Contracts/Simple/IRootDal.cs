namespace CslaModelTemplates.Contracts.Simple
{
    /// <summary>
    /// Defines the data access functions of the editable root object.
    /// </summary>
    public interface IRootDal
    {
        RootDao Fetch(RootCriteria criteria);
        void Insert(RootDao dao);
        void Update(RootDao dao);
        void Delete(RootCriteria criteria);
    }
}
