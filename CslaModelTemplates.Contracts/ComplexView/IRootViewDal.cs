namespace CslaModelTemplates.Contracts.ComplexView
{
    /// <summary>
    /// Defines the data access functions of the read-only root object.
    /// </summary>
    public interface IRootViewDal
    {
        RootViewDao Fetch(RootViewCriteria criteria);
    }
}
