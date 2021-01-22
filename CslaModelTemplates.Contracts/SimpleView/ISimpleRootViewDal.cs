namespace CslaModelTemplates.Contracts.SimpleView
{
    /// <summary>
    /// Defines the data access functions of the read-only root object.
    /// </summary>
    public interface ISimpleRootViewDal
    {
        SimpleRootViewDao Fetch(SimpleRootViewCriteria criteria);
    }
}
