using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexView
{
    /// <summary>
    /// Defines the read-only root data.
    /// </summary>
    public class RootViewData
    {
        public long? RootKey { get; set; }
        public string RootCode { get; set; }
        public string RootName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only root object.
    /// </summary>
    public class RootViewDao : RootViewData
    {
        public List<RootItemViewDao> Items { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only root object.
    /// </summary>
    public class RootViewDto : RootViewData
    {
        public List<RootItemViewDto> Items { get; set; }
    }
}
