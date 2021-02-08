namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the data access functions of the editable child object.
    /// </summary>
    public interface IRootSetRootItemDal
    {
        void Insert(RootSetRootItemDao dao);
        void Update(RootSetRootItemDao dao);
        void Delete(RootSetRootItemCriteria criteria);
    }
}
