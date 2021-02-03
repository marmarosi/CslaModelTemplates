namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Defines the data access functions of the editable child object.
    /// </summary>
    public interface IRootItemDal
    {
        void Insert(RootItemDao dao);
        void Update(RootItemDao dao);
        void Delete(RootItemCriteria criteria);
    }
}
