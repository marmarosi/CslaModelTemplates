using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.Oracle.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.Oracle.Complex
{
    /// <summary>
    /// Implements the data access functions of the editable player object.
    /// </summary>
    public class PlayerDal : OracleDal, IPlayerDal
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
            // Check unique player code.
            Player player = DbContext.Players
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
            DbContext.Players.Add(player);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new InsertFailedException(DalText.Player_InsertFailed.With(player.PlayerCode));

            // Return new data.
            dao.PlayerKey = player.PlayerKey;
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
            // Get the specified player.
            Player player = DbContext.Players
                .Where(e =>
                    e.PlayerKey == dao.PlayerKey
                )
                .FirstOrDefault();
            if (player == null)
                throw new DataNotFoundException(DalText.Player_NotFound);

            // Check unique player code.
            if (player.PlayerCode != dao.PlayerCode)
            {
                int exist = DbContext.Players
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

            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new UpdateFailedException(DalText.Player_UpdateFailed.With(player.PlayerCode));

            // Return new data.
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
            // Get the specified player.
            Player player = DbContext.Players
                .Where(e =>
                    e.PlayerKey == criteria.PlayerKey
                 )
                .AsNoTracking()
                .FirstOrDefault();
            if (player == null)
                throw new DataNotFoundException(DalText.Player_NotFound);

            // Delete the player.
            DbContext.Players.Remove(player);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new DeleteFailedException(DalText.Player_DeleteFailed.With(player.PlayerCode));
        }

        #endregion Delete
    }
}
