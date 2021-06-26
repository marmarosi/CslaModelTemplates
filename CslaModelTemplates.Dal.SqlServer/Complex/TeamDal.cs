using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.SqlServer.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.Complex
{
    /// <summary>
    /// Implements the data access functions of the editable team object.
    /// </summary>
    public class TeamDal : SqlServerDal, ITeamDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified team.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <returns>The requested team.</returns>
        public TeamDao Fetch(
            TeamCriteria criteria
            )
        {
            // Get the specified team.
            TeamDao team = DbContext.Teams
                .Include(e => e.Players)
                .Where(e =>
                    e.TeamKey == criteria.TeamKey
                 )
                .Select(e => new TeamDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName,
                    Players = e.Players
                        .Select(p => new PlayerDao
                        {
                            PlayerKey = p.PlayerKey,
                            TeamKey = p.TeamKey,
                            PlayerCode = p.PlayerCode,
                            PlayerName = p.PlayerName
                        })
                        .OrderBy(p => p.PlayerName)
                        .ToList(),
                    Timestamp = e.Timestamp
                })
                .FirstOrDefault();

            if (team == null)
                throw new DataNotFoundException(DalText.Team_NotFound);

            return team;
        }

        #endregion Fetch

        #region Insert

        /// <summary>
        /// Creates a new team using the specified data.
        /// </summary>
        /// <param name="dao">The data of the team.</param>
        public void Insert(
            TeamDao dao
            )
        {
            // Check unique team code.
            Team team = DbContext.Teams
                .Where(e =>
                    e.TeamCode == dao.TeamCode
                )
                .FirstOrDefault();
            if (team != null)
                throw new DataExistException(DalText.Team_TeamCodeExists.With(dao.TeamCode));

            // Create the new team.
            team = new Team
            {
                TeamCode = dao.TeamCode,
                TeamName = dao.TeamName
            };
            DbContext.Teams.Add(team);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new InsertFailedException(DalText.Team_InsertFailed);

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
            TeamDao dao
            )
        {
            // Get the specified team.
            Team team = DbContext.Teams
                .Where(e =>
                    e.TeamKey == dao.TeamKey
                )
                .FirstOrDefault();
            if (team == null)
                throw new DataNotFoundException(DalText.Team_NotFound);
            if (team.Timestamp != dao.Timestamp)
                throw new ConcurrencyException(DalText.Team_Concurrency);

            // Check unique team code.
            if (team.TeamCode != dao.TeamCode)
            {
                int exist = DbContext.Teams
                    .Where(e => e.TeamCode == dao.TeamCode && e.TeamKey != team.TeamKey)
                    .Count();
                if (exist > 0)
                    throw new DataExistException(DalText.Team_TeamCodeExists.With(dao.TeamCode));
            }

            // Update the team.
            team.TeamCode = dao.TeamCode;
            team.TeamName = dao.TeamName;
            team.Timestamp = DateTime.Now; // Force update timestamp.

            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new UpdateFailedException(DalText.Team_UpdateFailed);

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
            TeamCriteria criteria
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
                throw new DataNotFoundException(DalText.Team_NotFound);

            // Check or delete references
            //int dependents = 0;

            //dependents = DbContext.Others.Count(e => e.TeamKey == criteria.TeamKey);
            //if (dependents > 0)
            //    throw new DeleteFailedException(DalText.Team_Delete_Others);

            // Delete the team.
            DbContext.Teams.Remove(team);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new DeleteFailedException(DalText.Team_DeleteFailed);
        }

        #endregion Delete
    }
}
