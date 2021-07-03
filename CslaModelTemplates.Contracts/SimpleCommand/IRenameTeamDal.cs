using CslaModelTemplates.Dal;

namespace CslaModelTemplates.Contracts.SimpleCommand
{
    /// <summary>
    /// Defines the data access functions of the rename team command.
    /// </summary>
    public interface IRenameTeamDal : IDal
    {
        void Execute(RenameTeamDao dao);
    }
}
