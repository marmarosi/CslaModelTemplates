using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.MySql.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.MySql.ComplexSet
{
    /// <summary>
    /// Implements the data access functions of the editable player object.
    /// </summary>
    public class TeamSetPlayerDal : ITeamSetPlayerDal
    {
        #region Insert

        /// <summary>
        /// Creates a new player using the specified data.
        /// </summary>
        /// <param name="dao">The data of the player.</param>
        public void Insert(
            TeamSetPlayerDao dao
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Check unique player code.
                Player player = ctx.DbContext.Players
                    .Where(e =>
                        e.TeamKey == dao.TeamKey &&
                        e.PlayerCode == dao.PlayerCode
                    )
                    .FirstOrDefault();
                if (player != null)
                    throw new DataExistException(DalText.TeamSetPlayer_PlayerCodeExists
                        .With(dao.__teamCode, dao.PlayerCode));

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
                    throw new InsertFailedException(DalText.TeamSetPlayer_InsertFailed
                        .With(dao.__teamCode, dao.PlayerCode));

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
            TeamSetPlayerDao dao
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified player.
                Player player = ctx.DbContext.Players
                    .Where(e =>
                        e.PlayerKey == dao.PlayerKey
                    )
                    .FirstOrDefault();
                if (player == null)
                    throw new DataNotFoundException(DalText.TeamSetPlayer_NotFound
                            .With(dao.__teamCode, dao.PlayerCode));

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
                        throw new DataExistException(DalText.TeamSetPlayer_PlayerCodeExists
                            .With(dao.__teamCode, dao.PlayerCode));
                }

                // Update the player.
                player.PlayerCode = dao.PlayerCode;
                player.PlayerName = dao.PlayerName;

                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new UpdateFailedException(DalText.TeamSetPlayer_UpdateFailed
                        .With(dao.__teamCode, dao.PlayerCode));

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
            TeamSetPlayerCriteria criteria
            )
        {
            using (var ctx = DbContextManager<MySqlContext>.GetManager())
            {
                // Get the specified player.
                Player player = ctx.DbContext.Players
                    .Where(e =>
                        e.PlayerKey == criteria.PlayerKey
                     )
                    .AsNoTracking()
                    .FirstOrDefault();
                if (player == null)
                    throw new DataNotFoundException(DalText.TeamSetPlayer_NotFound
                        .With(criteria.__teamCode, criteria.__playerCode));

                // Delete the player.
                ctx.DbContext.Players.Remove(player);
                int count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new DeleteFailedException(DalText.TeamSetPlayer_DeleteFailed
                        .With(criteria.__teamCode, criteria.__playerCode));
            }
        }

        #endregion Delete
    }
}
