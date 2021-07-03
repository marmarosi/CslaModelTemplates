using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.Sqlite.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CslaModelTemplates.Dal.Sqlite.SimpleSet
{
    /// <summary>
    /// Implements the data access functions of the editable team set item object.
    /// </summary>
    public class SimpleTeamSetItemDal : SqliteDal, ISimpleTeamSetItemDal
    {
        #region Insert

        /// <summary>
        /// Creates a new team using the specified data.
        /// </summary>
        /// <param name="dao">The data of the team.</param>
        public void Insert(
            SimpleTeamSetItemDao dao
            )
        {
            // Check unique team code.
            Team team = DbContext.Teams
                .Where(e =>
                    e.TeamCode == dao.TeamCode
                )
                .FirstOrDefault();
            if (team != null)
                throw new DataExistException(DalText.SimpleTeamSetItem_TeamCodeExists.With(dao.TeamCode));

            // Create the new team.
            team = new Team
            {
                TeamCode = dao.TeamCode,
                TeamName = dao.TeamName
            };
            DbContext.Teams.Add(team);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new InsertFailedException(DalText.SimpleTeamSetItem_InsertFailed.With(team.TeamCode));

            // Return new data.
            dao.TeamKey = team.TeamKey;
            dao.Timestamp = team.Timestamp;
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing team using the specified data.
        /// </summary>
        /// <param name="dao">The data of the team.</param>
        public void Update(
            SimpleTeamSetItemDao dao
            )
        {
            // Get the specified team.
            Team team = DbContext.Teams
                .Where(e =>
                    e.TeamKey == dao.TeamKey
                )
                .FirstOrDefault();
            if (team == null)
                throw new DataNotFoundException(DalText.SimpleTeamSetItem_NotFound.With(dao.TeamCode));
            if (team.Timestamp != dao.Timestamp)
                throw new ConcurrencyException(DalText.SimpleTeamSetItem_Concurrency.With(dao.TeamCode));

            // Check unique team code.
            if (team.TeamCode != dao.TeamCode)
            {
                int exist = DbContext.Teams
                    .Where(e =>
                        e.TeamCode == dao.TeamCode &&
                        e.TeamKey != team.TeamKey
                    )
                    .Count();
                if (exist > 0)
                    throw new DataExistException(DalText.SimpleTeamSetItem_TeamCodeExists.With(dao.TeamCode));
            }

            // Update the team.
            team.TeamCode = dao.TeamCode;
            team.TeamName = dao.TeamName;

            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new UpdateFailedException(DalText.SimpleTeamSetItem_UpdateFailed.With(team.TeamCode));

            // Return new data.
            dao.Timestamp = team.Timestamp;
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified team.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        public void Delete(
            SimpleTeamSetItemCriteria criteria
            )
        {
            // Get the specified team.
            Team team = DbContext.Teams
                .Where(e =>
                    e.TeamKey == criteria.TeamKey
                 )
                .AsNoTracking()
                .FirstOrDefault();
            if (team == null)
                throw new DataNotFoundException(DalText.SimpleTeamSetItem_NotFound.With(team.TeamCode));

            // Check or delete references
            //int dependents = 0;

            //dependents = DbContext.Others.Count(e => e.TeamKey == criteria.TeamKey);
            //if (dependents > 0)
            //    throw new DeleteFailedException(DalText.SimpleTeamSetItem_Delete_Others);

            List<Player> players = DbContext.Players
                .Where(e => e.TeamKey == criteria.TeamKey)
                .ToList();
            foreach (Player player in players)
                DbContext.Players.Remove(player);
            DbContext.SaveChanges();

            // Delete the team.
            DbContext.Teams.Remove(team);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new DeleteFailedException(DalText.SimpleTeamSetItem_DeleteFailed.With(team.TeamCode));
        }

        #endregion Delete
    }
}
