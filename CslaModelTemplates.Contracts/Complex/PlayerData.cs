namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Defines the editable player data.
    /// </summary>
    public class PlayerData
    {
        public long? PlayerKey { get; set; }
        public long? TeamKey { get; set; }
        public string PlayerCode { get; set; }
        public string PlayerName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable player object.
    /// </summary>
    public class PlayerDao : PlayerData
    { }

    /// <summary>
    /// Defines the data transfer object of the editable player object.
    /// </summary>
    public class PlayerDto : PlayerData
    {
        public PlayerDao ToDao()
        {
            return new PlayerDao
            {
                PlayerKey = PlayerKey,
                TeamKey = TeamKey,
                PlayerCode = PlayerCode,
                PlayerName = PlayerName
            };
        }

    }
}
