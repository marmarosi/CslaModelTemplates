namespace CslaModelTemplates.Contracts.Simple
{
    /// <summary>
    /// Defines the data access functions of the editable root object.
    /// </summary>
    public interface ISimpleRootDal
    {
        SimpleRootDao Fetch(SimpleRootCriteria criteria);
        void Insert(SimpleRootDao dao);
        void Update(SimpleRootDao dao);
        void Delete(SimpleRootCriteria criteria);
    }
}
