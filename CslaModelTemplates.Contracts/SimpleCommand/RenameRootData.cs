namespace CslaModelTemplates.Contracts.SimpleCommand
{
    /// <summary>
    /// Defines the data access object of the rename root command.
    /// </summary>
    public class RenameRootData
    {
        public long? RootKey { get; set; }
        public string RootName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the rename root command.
    /// </summary>
    public class RenameRootDao : RenameRootData
    {
        public RenameRootDao(
            long? rootKey,
            string rootName
            )
        {
            RootKey = rootKey;
            RootName = rootName;
        }
    }

    /// <summary>
    /// Defines the data transfer object of the rename root command.
    /// </summary>
    public class RenameRootDto : RenameRootData
    { }
}
