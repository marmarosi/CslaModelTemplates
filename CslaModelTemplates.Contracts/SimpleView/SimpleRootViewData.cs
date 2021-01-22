namespace CslaModelTemplates.Contracts.SimpleView
{
    /// <summary>
    /// Defines the read-only root data.
    /// </summary>
    public class SimpleRootViewData
    {
        public long? RootKey { get; set; }
        public string RootCode { get; set; }
        public string RootName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only root object.
    /// </summary>
    public class SimpleRootViewDao : SimpleRootViewData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only root object.
    /// </summary>
    public class SimpleRootViewDto : SimpleRootViewData
    { }
}
