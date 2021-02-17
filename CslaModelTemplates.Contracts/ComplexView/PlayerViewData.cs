namespace CslaModelTemplates.Contracts.ComplexView
{
    /// <summary>
    /// Defines the read-only player data.
    /// </summary>
    public class PlayerViewData
    {
        public long? PlayerKey { get; set; }
        public string PlayerCode { get; set; }
        public string PlayerName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only player object.
    /// </summary>
    public class PlayerViewDao : PlayerViewData
    { }

    /// <summary>
    /// Defines the data transfer object of the read-only player object.
    /// </summary>
    public class PlayerViewDto : PlayerViewData
    { }
}
