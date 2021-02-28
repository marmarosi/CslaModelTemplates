using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.SqlServer.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.Complex
{
    /// <summary>
    /// Implements the data access functions of the editable player object.
    /// </summary>
    public class PlayerDal : IPlayerDal
    {
        #region Insert

        /// <summary>
        /// Creates a new player using the specified data.
        /// </summary>
        /// <param name="dao">The data of the player.</param>
        public void Insert(
            PlayerDao dao
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Check unique player code.
                Player player = ctx.DbContext.Players
                    .Where(e =>
                        e.TeamKey == dao.TeamKey &&
                        e.PlayerCode == dao.PlayerCode
                    )
                    .FirstOrDefault();
                if (player != null)
                    throw new DataExistException(DalText.Player_PlayerCodeExists.With(dao.PlayerCode));

                // Create the new player.
                player = new Player
                {
                    TeamKey = dao.TeamKey,
                    PlayerCode = dao.PlayerCode,
                    PlayerName = dao.PlayerName
                };
                ctx.DbContext.Players.Add(player);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new InsertFailedException(DalText.Player_InsertFailed.With(player.PlayerCode));

                // Return new data.
                dao.PlayerKey = player.PlayerKey;
            }
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing player using the specified data.
        /// </summary>
        /// <param name="dao">The data of the player.</param>
        public void Update(
            PlayerDao dao
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Get the specified player.
                Player player = ctx.DbContext.Players
                    .Where(e =>
                        e.PlayerKey == dao.PlayerKey
                    )
                    .FirstOrDefault();
                if (player == null)
                    throw new DataNotFoundException(DalText.Player_NotFound);

                // Check unique player code.
                if (player.PlayerCode != dao.PlayerCode)
                {
                    int exist = ctx.DbContext.Players
                        .Where(e =>
                            e.TeamKey == dao.TeamKey &&
                            e.PlayerCode == dao.PlayerCode &&
                            e.PlayerKey != player.PlayerKey
                        )
                        .Count();
                    if (exist > 0)
                        throw new DataExistException(DalText.Player_PlayerCodeExists.With(dao.PlayerCode));
                }

                // Update the player.
                player.PlayerCode = dao.PlayerCode;
                player.PlayerName = dao.PlayerName;

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.Player_UpdateFailed.With(player.PlayerCode));

                // Return new data.
            }
        }
        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified player.
        /// </summary>
        /// <param name="criteria">The criteria of the player.</param>
        public void Delete(
            PlayerCriteria criteria
            )
        {
            using (var ctx = DbContextManager<SqlServerContext>.GetManager())
            {
                // Get the specified player.
                Player player = ctx.DbContext.Players
                    .Where(e =>
                        e.PlayerKey == criteria.PlayerKey
                     )
                    .AsNoTracking()
                    .FirstOrDefault();
                if (player == null)
                    throw new DataNotFoundException(DalText.Player_NotFound);

                // Delete the player.
                ctx.DbContext.Players.Remove(player);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new DeleteFailedException(DalText.Player_DeleteFailed.With(player.PlayerCode));
            }
        }

        #endregion Delete
    }
}
