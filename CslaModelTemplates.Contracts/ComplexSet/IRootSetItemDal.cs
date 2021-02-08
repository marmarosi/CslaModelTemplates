namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Defines the data access functions of the editable child object.
    /// </summary>
    public interface IRootSetItemDal
    {
        void Insert(RootSetItemDao dao);
        void Update(RootSetItemDao dao);
        void Delete(RootSetItemCriteria criteria);
    }
}
