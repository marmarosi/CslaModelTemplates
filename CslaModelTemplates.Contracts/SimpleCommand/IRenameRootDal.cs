namespace CslaModelTemplates.Contracts.SimpleCommand
{
    /// <summary>
    /// Defines the data access functions of the rename root command.
    /// </summary>
    public interface IRenameRootDal
    {
        void Execute(RenameRootDao dao);
    }
}
