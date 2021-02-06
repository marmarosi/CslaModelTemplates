namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Defines the data access functions of the editable child object.
    /// </summary>
    public interface ISimpleRootSetItemDal
    {
        void Insert(SimpleRootSetItemDao dao);
        void Update(SimpleRootSetItemDao dao);
        void Delete(SimpleRootSetItemCriteria criteria);
    }
}
