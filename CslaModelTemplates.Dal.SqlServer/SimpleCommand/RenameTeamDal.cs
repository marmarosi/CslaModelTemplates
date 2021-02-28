using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.SimpleCommand;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.SqlServer.Entities;
using CslaModelTemplates.Resources;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.SimpleCommand
{
    /// <summary>
    /// Implements the data access functions of the rename team command.
    /// </summary>
    public class RenameTeamDal : IRenameTeamDal
    {
        private string COMMAND = typeof(RenameTeamDal).Name.CutEnd(3);

        #region Execute

        /// <summary>
        /// Sets the new name of the specified team.
        /// </summary>
        /// <param name="dao">The data of the command.</param>
        public void Execute(
            RenameTeamDao dao
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Get the specified team.
                Team team = ctx.DbContext.Teams
                    .Where(e =>
                        e.TeamKey == dao.TeamKey
                    )
                    .FirstOrDefault();
                if (team == null)
                    throw new DataNotFoundException(DalText.RenameTeam_NotFound);

                // Update the team.
                team.TeamName = dao.TeamName;

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.RenameTeam_RenameFailed);
            }
        }

        #endregion
    }
}
